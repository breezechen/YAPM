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

    Public Sub New(ByRef infos As API.SYSTEM_THREAD_INFORMATION)
        _infos = infos
        _threadInfos = New threadInfos(_infos)
        _connection = Connection
    End Sub

    Public Sub New(ByRef infos As threadInfos)
        _threadInfos = infos
        _connection = Connection
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
    End Sub
    Public Sub Merge(ByRef Thr As API.SYSTEM_THREAD_INFORMATION)
        _threadInfos.Merge(Thr)
    End Sub

#Region "All actions on process (kill, enum...)"

    ' Set priority
    Private WithEvents asyncSetPriority As asyncCallbackThreadSetPriority
    Public Function SetPriority(ByVal level As System.Diagnostics.ThreadPriorityLevel) As Integer
        asyncSetPriority = New asyncCallbackThreadSetPriority(Me.Infos.Id, level, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "SetPriority"
        t.Start()
    End Function
    Private Sub setPriorityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncSetPriority.HasSetPriority
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to thread " & Me.Infos.Id.ToString)
        End If
    End Sub

    ' Kill a process
    Private WithEvents asyncKill As asyncCallbackThreadKill
    Public Function ThreadTerminate() As Integer
        asyncKill = New asyncCallbackThreadKill(Me.Infos.Id, _connection)
        Dim t As New Threading.Thread(AddressOf asyncKill.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "Kill"
        t.Start()
    End Function
    Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String) Handles asyncKill.HasKilled
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill thread " & Me.Infos.Id.ToString)
        End If
    End Sub

    ' Decrease priority
    Private WithEvents asyncDecPriority As asyncCallbackThreadDecreasePriority
    Public Function DecreasePriority() As Integer
        asyncDecPriority = New asyncCallbackThreadDecreasePriority(Me.Infos.Id, Me.Infos.Priority, _connection)
        Dim t As New Threading.Thread(AddressOf asyncDecPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "DecreasePriority"
        t.Start()
    End Function
    Private Sub decreasePriorityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncDecPriority.HasDecreasedPriority
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to thread " & Me.Infos.Id.ToString)
        End If
    End Sub

    ' Increase priority
    Private WithEvents asyncInPriority As asyncCallbackThreadIncreasePriority
    Public Function IncreasePriority() As Integer
        asyncInPriority = New asyncCallbackThreadIncreasePriority(Me.Infos.Id, Me.Infos.Priority, _connection)
        Dim t As New Threading.Thread(AddressOf asyncInPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "IncreasePriority"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub increasePriorityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncInPriority.HasIncreasedPriority
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Id.ToString)
        End If
    End Sub

    ' Suspend a process
    Private WithEvents asyncSuspend As asyncCallbackThreadSuspend
    Public Function ThreadSuspend() As Integer
        asyncSuspend = New asyncCallbackThreadSuspend(Me.Infos.Id, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSuspend.Process)
        t.Name = "SuspendThread"
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub suspendDone(ByVal success As Boolean, ByVal msg As String) Handles asyncSuspend.HasSuspended
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not suspend thread " & Me.Infos.Id.ToString)
        End If
    End Sub

    ' Resume a process
    Private WithEvents asyncResume As asyncCallbackThreadResume
    Public Function ThreadResume() As Integer
        asyncResume = New asyncCallbackThreadResume(Me.Infos.Id, _connection)
        Dim t As New Threading.Thread(AddressOf asyncResume.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "ResumeThread"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub resumeDone(ByVal success As Boolean, ByVal msg As String) Handles asyncResume.HasResumed
        If success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume process " & Me.Infos.Id.ToString)
        End If
    End Sub

    ' Change affinity
    Private WithEvents asyncSetAffinity As asyncCallbackThreadSetAffinity
    Public Function SetAffinity(ByVal affinity As Integer) As Integer
        asyncSetAffinity = New asyncCallbackThreadSetAffinity(Me.Infos.Id, affinity, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetAffinity.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "SetAffinity"
        t.Start()
    End Function
    Private Sub setAffinityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncSetAffinity.HasSetAffinity
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set affinity " & Me.Infos.Id.ToString)
        End If
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Priority"
                res = threadInfos.getPriorityClass(Me.Infos.Priority).ToString
            Case "State"
                res = Me.Infos.State.ToString
            Case "WaitReason"
                res = Me.Infos.WaitReason.ToString
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
                res = threadInfos.getPriorityClass(Me.Infos.BasePriority).ToString
            Case "ContextSwitchCount"
                res = Me.Infos.ContextSwitchCount.ToString
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "Id"
                res = Me.Infos.Id.ToString
        End Select

        Return res
    End Function


#End Region

End Class
