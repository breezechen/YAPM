' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
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

Imports System.Drawing

Public Class cThread
    Inherits cGeneralObject

    Private _threadInfos As threadInfos
    Private Shared WithEvents _connection As cThreadConnection

    Private _handleQueryInfo As Integer

    Private Shared _hlSuspendedThread As Boolean
    Private Shared _hlSuspendedThreadColor As Color

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

    Public Sub New(ByRef infos As threadInfos, Optional ByVal getPriorityInfo As Boolean = False)
        _threadInfos = infos
        _connection = Connection
        ' Get a handle if local
        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            _handleQueryInfo = API.OpenThread(API.THREAD_RIGHTS.THREAD_QUERY_INFORMATION, 0, infos.Id)
            If getPriorityInfo Then
                ' Here we get priority (used when YAPM is used as a server)
                Call infos.SetPriority(API.GetThreadPriority(_handleQueryInfo))
            End If
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

    Public ReadOnly Property PriorityMod() As ThreadPriorityLevel
        Get
            If _handleQueryInfo > 0 Then
                Dim priority As Integer = API.GetThreadPriority(_handleQueryInfo)
                Return CType(priority, ThreadPriorityLevel)
            Else
                Return Me.Infos.Priority
            End If
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
    Private _setPriority As asyncCallbackThreadSetPriority
    Public Function SetPriority(ByVal level As System.Diagnostics.ThreadPriorityLevel) As Integer

        If _setPriority Is Nothing Then
            _setPriority = New asyncCallbackThreadSetPriority(New asyncCallbackThreadSetPriority.HasSetPriority(AddressOf setPriorityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setPriority.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadSetPriority.poolObj(Me.Infos.Id, level, newAction))

    End Function
    Private Sub setPriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to thread " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub


    ' Kill a thread
    Private _killThread As asyncCallbackThreadKill
    Public Function ThreadTerminate() As Integer

        If _killThread Is Nothing Then
            _killThread = New asyncCallbackThreadKill(New asyncCallbackThreadKill.HasKilled(AddressOf killDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killThread.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadKill.poolObj(Me.Infos.Id, newAction))

    End Function
    Private Sub killDone(ByVal Success As Boolean, ByVal id As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill thread " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Decrease priority
    Private _decP As asyncCallbackThreadDecreasePriority
    Public Function DecreasePriority() As Integer

        If _decP Is Nothing Then
            _decP = New asyncCallbackThreadDecreasePriority(New asyncCallbackThreadDecreasePriority.HasDecreasedPriority(AddressOf decreasePriorityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _decP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadDecreasePriority.poolObj(Me.Infos.Id, Me.PriorityMod, newAction))

    End Function
    Private Sub decreasePriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to thread " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Increase priority
    Private _incP As asyncCallbackThreadIncreasePriority
    Public Function IncreasePriority() As Integer

        If _incP Is Nothing Then
            _incP = New asyncCallbackThreadIncreasePriority(New asyncCallbackThreadIncreasePriority.HasIncreasedPriority(AddressOf increasePriorityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _incP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadIncreasePriority.poolObj(Me.Infos.Id, Me.PriorityMod, newAction))

    End Function
    Private Sub increasePriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Suspend a process
    Private _suspP As asyncCallbackThreadSuspend
    Public Function ThreadSuspend() As Integer

        If _suspP Is Nothing Then
            _suspP = New asyncCallbackThreadSuspend(New asyncCallbackThreadSuspend.HasSuspended(AddressOf suspendDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _suspP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadSuspend.poolObj(Me.Infos.Id, newAction))

    End Function
    Private Sub suspendDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not suspend thread " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Resume a process
    Private _resumeP As asyncCallbackThreadResume
    Public Function ThreadResume() As Integer

        If _resumeP Is Nothing Then
            _resumeP = New asyncCallbackThreadResume(New asyncCallbackThreadResume.HasResumed(AddressOf resumeDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _resumeP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadResume.poolObj(Me.Infos.Id, newAction))

    End Function
    Private Sub resumeDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume process " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Change affinity
    Private _affinityP As asyncCallbackThreadSetAffinity
    Public Function SetAffinity(ByVal affinity As Integer) As Integer

        If _affinityP Is Nothing Then
            _affinityP = New asyncCallbackThreadSetAffinity(New asyncCallbackThreadSetAffinity.HasSetAffinity(AddressOf setAffinityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _affinityP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackThreadSetAffinity.poolObj(Me.Infos.Id, affinity, newAction))

    End Function
    Private Sub setAffinityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set affinity " & Me.Infos.Id.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If


        Select Case info
            Case "Priority"
                res = Me.PriorityMod.ToString
            Case "State"
                res = Me.Infos.State.ToString
            Case "WaitReason"
                res = Me.Infos.WaitReason.ToString
            Case "ContextSwitchDelta"
                res = Me.Infos.ContextSwitchDelta.ToString
            Case "CreateTime"
                If Me.Infos.CreateTime > 0 Then
                    Dim ts As Date = New Date(Me.Infos.CreateTime)
                    res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
                End If
            Case "KernelTime"
                Dim ts As Date = New Date(Me.Infos.KernelTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "UserTime"
                Dim ts As Date = New Date(Me.Infos.UserTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "WaitTime"
                Dim ts As Date = New Date(Me.Infos.WaitTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "TotalTime"
                Dim ts As Date = New Date(Me.Infos.TotalTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
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
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Priority As String = ""
        Static _old_State As String = ""
        Static _old_WaitReason As String = ""
        Static _old_ContextSwitchDelta As String = ""
        Static _old_CreateTime As String = ""
        Static _old_KernelTime As String = ""
        Static _old_UserTime As String = ""
        Static _old_WaitTime As String = ""
        Static _old_TotalTime As String = ""
        Static _old_StartAddress As String = ""
        Static _old_BasePriority As String = ""
        Static _old_ContextSwitchCount As String = ""
        Static _old_ProcessId As String = ""
        Static _old_Id As String = ""
        Static _old_AffinityMask As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If


        Select Case info
            Case "Priority"
                res = Me.PriorityMod.ToString
                If res = _old_Priority Then
                    hasChanged = False
                Else
                    _old_Priority = res
                End If
            Case "State"
                res = Me.Infos.State.ToString
                If res = _old_State Then
                    hasChanged = False
                Else
                    _old_State = res
                End If
            Case "WaitReason"
                res = Me.Infos.WaitReason.ToString
                If res = _old_WaitReason Then
                    hasChanged = False
                Else
                    _old_WaitReason = res
                End If
            Case "ContextSwitchDelta"
                res = Me.Infos.ContextSwitchDelta.ToString
                If res = _old_ContextSwitchDelta Then
                    hasChanged = False
                Else
                    _old_ContextSwitchDelta = res
                End If
            Case "CreateTime"
                If Me.Infos.CreateTime > 0 Then
                    Dim ts As Date = New Date(Me.Infos.CreateTime)
                    res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
                End If
                If res = _old_CreateTime Then
                    hasChanged = False
                Else
                    _old_CreateTime = res
                End If
            Case "KernelTime"
                Dim ts As Date = New Date(Me.Infos.KernelTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_KernelTime Then
                    hasChanged = False
                Else
                    _old_KernelTime = res
                End If
            Case "UserTime"
                Dim ts As Date = New Date(Me.Infos.UserTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_UserTime Then
                    hasChanged = False
                Else
                    _old_UserTime = res
                End If
            Case "WaitTime"
                Dim ts As Date = New Date(Me.Infos.WaitTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_WaitTime Then
                    hasChanged = False
                Else
                    _old_WaitTime = res
                End If
            Case "TotalTime"
                Dim ts As Date = New Date(Me.Infos.TotalTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_TotalTime Then
                    hasChanged = False
                Else
                    _old_TotalTime = res
                End If
            Case "StartAddress"
                res = "0x" & Me.Infos.StartAddress.ToString("x")
                If res = _old_StartAddress Then
                    hasChanged = False
                Else
                    _old_StartAddress = res
                End If
            Case "BasePriority"
                res = CInt(Me.Infos.BasePriority).ToString ' threadInfos.getPriorityClass(Me.Infos.BasePriority).ToString
                If res = _old_BasePriority Then
                    hasChanged = False
                Else
                    _old_BasePriority = res
                End If
            Case "ContextSwitchCount"
                res = Me.Infos.ContextSwitchCount.ToString
                If res = _old_ContextSwitchCount Then
                    hasChanged = False
                Else
                    _old_ContextSwitchCount = res
                End If
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
                If res = _old_ProcessId Then
                    hasChanged = False
                Else
                    _old_ProcessId = res
                End If
            Case "Id"
                res = Me.Infos.Id.ToString
                If res = _old_Id Then
                    hasChanged = False
                Else
                    _old_Id = res
                End If
            Case "AffinityMask"
                res = Me.Infos.AffinityMask.ToString
                If res = _old_AffinityMask Then
                    hasChanged = False
                Else
                    _old_AffinityMask = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

#Region "Highlighting management"

    ' Set highlightings
    Public Shared Sub SetHighlightings(ByVal suspendedThreads As Boolean)
        _hlSuspendedThread = suspendedThreads
    End Sub
    Public Shared Sub SetHighlightingsColor(ByVal suspendedThreads As color)
        _hlSuspendedThreadColor = suspendedThreads
    End Sub

    ' Return backcolor of current item
    Public Overrides Function GetBackColor() As System.Drawing.Color

        If _hlSuspendedThread AndAlso Me.Infos.WaitReason = API.KWAIT_REASON.Suspended Then
            Return _hlSuspendedThreadColor
        End If

        Return MyBase.GetBackColor()
    End Function

#End Region

End Class
