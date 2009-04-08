' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 3 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, see http://www.gnu.org/licenses/.

Option Strict On

Public Class cThread
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _infos As API.SYSTEM_THREAD_INFORMATION
    Private _threadInfos As threadInfos
    Private Shared WithEvents _connection As cThreadConnection

    Private _handleQueryInfo As Integer

#Region "Properties"

    Public Shared Property Connection() As cThreadConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cThreadConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As threadInfos)
        _threadInfos = infos
        _connection = Connection
        ' Get a handle if local
        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            _handleQueryInfo = API.OpenThread(API.THREAD_RIGHTS.THREAD_QUERY_INFORMATION, 0, infos.Id)
        End If
    End Sub

    Protected Overrides Sub Finalize()
        ' Close a handle if local
        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            If _handleQueryInfo > 0 Then
                API.CloseHandle(_handleQueryInfo)
            End If
        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As threadInfos
        Get
            Return _threadInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As threadInfos)
        _threadInfos.Merge(Thr)
        Call RefreshSpecialInformations()
    End Sub

#Region "Special informations (affinity)"

    ' Refresh some non fixed infos
    ' For now IT IS NOT ASYNC
    ' Because create ~50 threads/sec is not really cool
    ' TOCHANGE
    Private WithEvents asyncNonFixed As asyncCallbackThreadGetOtherInfos
    Private Sub RefreshSpecialInformations()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                If asyncNonFixed Is Nothing Then
                    asyncNonFixed = New asyncCallbackThreadGetOtherInfos(Me.Infos.Id, _connection, _handleQueryInfo)
                End If
                asyncNonFixed.Process()
        End Select
    End Sub
    Private Sub nonFixedInfosGathered(ByVal infos As asyncCallbackThreadGetOtherInfos.TheseInfos) Handles asyncNonFixed.GatheredInfos
        Me.Infos.AffinityMask = infos.affinity
    End Sub

#End Region

#Region "All actions on thread (kill, ...)"

    ' Set priority
    Public Function SetPriority(ByVal level As System.Diagnostics.ThreadPriorityLevel) As Integer
        Dim deg As New asyncCallbackThreadSetPriority.HasSetPriority(AddressOf setPriorityDone)
        Dim asyncSetPriority As New asyncCallbackThreadSetPriority(deg, Me.Infos.Id, level, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "SetPriority (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub setPriorityDone(ByVal success As Boolean, ByVal msg As String)
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to thread " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

    ' Kill a process
    Public Function ThreadTerminate() As Integer
        Dim deg As New asyncCallbackThreadKill.HasKilled(AddressOf killDone)
        Dim asyncKill As New asyncCallbackThreadKill(deg, Me.Infos.Id, _connection)
        Dim t As New Threading.Thread(AddressOf asyncKill.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "Kill (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill thread " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

    ' Decrease priority
    Public Function DecreasePriority() As Integer
        Dim deg As New asyncCallbackThreadDecreasePriority.HasDecreasedPriority(AddressOf decreasePriorityDone)
        Dim asyncDecPriority As New asyncCallbackThreadDecreasePriority(deg, Me.Infos.Id, Me.Infos.Priority, _connection)
        Dim t As New Threading.Thread(AddressOf asyncDecPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "DecreasePriority (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub decreasePriorityDone(ByVal success As Boolean, ByVal msg As String)
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to thread " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

    ' Increase priority
    Public Function IncreasePriority() As Integer
        Dim deg As New asyncCallbackThreadIncreasePriority.HasIncreasedPriority(AddressOf increasePriorityDone)
        Dim asyncInPriority As New asyncCallbackThreadIncreasePriority(deg, Me.Infos.Id, Me.Infos.Priority, _connection)
        Dim t As New Threading.Thread(AddressOf asyncInPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "IncreasePriority (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub increasePriorityDone(ByVal success As Boolean, ByVal msg As String)
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

    ' Suspend a process
    Public Function ThreadSuspend() As Integer
        Dim deg As New asyncCallbackThreadSuspend.HasSuspended(AddressOf suspendDone)
        Dim asyncSuspend As New asyncCallbackThreadSuspend(deg, Me.Infos.Id, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSuspend.Process)
        t.Name = "SuspendThread (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub suspendDone(ByVal success As Boolean, ByVal msg As String)
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not suspend thread " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

    ' Resume a process
    Public Function ThreadResume() As Integer
        Dim deg As New asyncCallbackThreadResume.HasResumed(AddressOf resumeDone)
        Dim asyncResume As New asyncCallbackThreadResume(deg, Me.Infos.Id, _connection)
        Dim t As New Threading.Thread(AddressOf asyncResume.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "ResumeThread (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub resumeDone(ByVal success As Boolean, ByVal msg As String)
        If success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume process " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

    ' Change affinity
    Public Function SetAffinity(ByVal affinity As Integer) As Integer
        Dim deg As New asyncCallbackThreadSetAffinity.HasSetAffinity(AddressOf setAffinityDone)
        Dim asyncSetAffinity As New asyncCallbackThreadSetAffinity(deg, Me.Infos.Id, affinity, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetAffinity.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "SetAffinity (" & Me.Infos.Id.ToString & ")" & "  -- " & Date.Now.Ticks.ToString
        AddPendingTask(t)
        t.Start()
    End Function
    Private Sub setAffinityDone(ByVal success As Boolean, ByVal msg As String)
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set affinity " & Me.Infos.Id.ToString)
        End If
        RemoveDeadTasks()
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            Return PendingTaskCount.ToString
        End If

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Priority"
                res = CInt(Me.Infos.Priority).ToString  'threadInfos.getPriorityClass(Me.Infos.Priority).ToString
            Case "State"
                res = Me.Infos.State.ToString
            Case "WaitReason"
                res = Me.Infos.WaitReason.ToString
            Case "ContextSwitchDelta"
                res = Me.Infos.ContextSwitchDelta.ToString
            Case "CreateTime"
                Dim ts As Date = New Date(Me.Infos.CreateTime)
                res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
            Case "KernelTime"
                Dim ts As Date = New Date(Me.Infos.KernelTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                res = res
            Case "UserTime"
                Dim ts As Date = New Date(Me.Infos.UserTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                res = res
            Case "WaitTime"
                Dim ts As Date = New Date(Me.Infos.WaitTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                res = res
            Case "TotalTime"
                Dim ts As Date = New Date(Me.Infos.TotalTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                res = res
            Case "StartAddress"
                res = "0x" & Me.Infos.StartAddress.ToString("x")
            Case "BasePriority"
                res = CInt(Me.Infos.BasePriority).ToString ' threadInfos.getPriorityClass(Me.Infos.BasePriority).ToString
            Case "ContextSwitchCount"
                res = Me.Infos.ContextSwitchCount.ToString
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "Id"
                res = Me.Infos.Id.ToString
            Case "AffinityMask"
                res = Me.Infos.AffinityMask.ToString
        End Select

        Return res
    End Function


#End Region

End Class
