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

Public Class cService
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _firstRefresh As Boolean = True
    Private _serviceInfos As serviceInfos
    Private _path As String
    Private Shared WithEvents _connection As cServiceConnection

#Region "Properties"

    Public Shared Property Connection() As cServiceConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cServiceConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As serviceInfos)
        _serviceInfos = infos
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As serviceInfos
        Get
            Return _serviceInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As serviceInfos)
        _serviceInfos.Merge(Thr)
    End Sub

    ' Return true if the state has changed
    Public Function HasChanged(ByRef infos As serviceInfos) As Boolean
        Dim b As Boolean = (_firstRefresh OrElse Me.Infos.State <> infos.State OrElse Me.Infos.ProcessId <> infos.ProcessId)
        _firstRefresh = False
        Return b
    End Function

    ' Refresh Config
    Public Sub Refresh()
        asyncCallbackServiceEnumerate.getServiceConfig(Me.Infos.Name, _connection.SCManagerLocalHandle, Me.Infos)
    End Sub

#Region "All actions on services (start...)"

    ' Start service
    Public Sub StartService()
        Dim deg As New asyncCallbackServiceStart.HasStarted(AddressOf startServiceDone)
        Dim asyncStart As New asyncCallbackServiceStart(deg, Me.Infos.Name, _connection)
        Dim t As New Threading.Thread(AddressOf asyncStart.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "StartService (" & Me.Infos.Name & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Sub
    Private Sub startServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not start service " & name)
        End If
        RemoveDeadTasks()
    End Sub

    ' Pause a service
    Public Sub PauseService()
        Dim deg As New asyncCallbackServicePause.HasPaused(AddressOf pauseServiceDone)
        Dim asyncPause As New asyncCallbackServicePause(deg, Me.Infos.Name, _connection)
        Dim t As New Threading.Thread(AddressOf asyncPause.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "PauseService (" & Me.Infos.Name & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Sub
    Private Sub pauseServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not pause service " & name)
        End If
        RemoveDeadTasks()
    End Sub

    ' Resume a service
    Public Sub ResumeService()
        Dim deg As New asyncCallbackServiceResume.HasResumed(AddressOf resumeServiceDone)
        Dim asyncResume As New asyncCallbackServiceResume(deg, Me.Infos.Name, _connection)
        Dim t As New Threading.Thread(AddressOf asyncResume.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "ResumeService (" & Me.Infos.Name & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Sub
    Private Sub resumeServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume service " & name)
        End If
        RemoveDeadTasks()
    End Sub

    ' Stop a service
    Public Sub StopService()
        Dim deg As New asyncCallbackServiceStop.HasStopped(AddressOf stopServiceDone)
        Dim asyncStop As New asyncCallbackServiceStop(deg, Me.Infos.Name, _connection)
        Dim t As New Threading.Thread(AddressOf asyncStop.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "StopService (" & Me.Infos.Name & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Sub
    Private Sub stopServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not stop service " & name)
        End If
        RemoveDeadTasks()
    End Sub

    ' Shutdown a service
    Public Sub ShutDownService()
        Dim deg As New asyncCallbackServiceShutdown.HasShutdowned(AddressOf shutdownServiceDone)
        Dim asyncShutdown As New asyncCallbackServiceShutdown(deg, Me.Infos.Name, _connection)
        Dim t As New Threading.Thread(AddressOf asyncShutdown.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "ShutdownService (" & Me.Infos.Name & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Sub
    Private Sub shutdownServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not shutdown service " & name)
        End If
        RemoveDeadTasks()
    End Sub

    ' Set service start type
    Public Sub SetServiceStartType(ByVal type As API.SERVICE_START_TYPE)
        Dim deg As New asyncCallbackServiceSetStartType.HasChangedStartType(AddressOf setServiceStartTypeDone)
        Dim asyncSetStartType As New asyncCallbackServiceSetStartType(deg, Me.Infos.Name, type, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetStartType.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "SetServiceStartType (" & Me.Infos.Name & ")" & "  -- " & Date.Now.Ticks.ToString
        t.IsBackground = True
        AddPendingTask(t)
        t.Start()
    End Sub
    Private Sub setServiceStartTypeDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change start type of service " & name)
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
            Case "Name"
                res = Me.Infos.Name
            Case "DisplayName"
                res = Me.Infos.DisplayName
            Case "ServiceType"
                res = Me.Infos.ServiceType.ToString
            Case "ImagePath"
                If _path = vbNullString Then
                    _path = mdlMisc.GetRealPath(Me.Infos.ImagePath)
                End If
                res = _path
            Case "ErrorControl"
                res = Me.Infos.ErrorControl.ToString
            Case "StartType"
                res = Me.Infos.StartType.ToString
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "ProcessName"
                res = Me.Infos.ProcessName
            Case "LoadOrderGroup"
                res = Me.Infos.LoadOrderGroup
            Case "ServiceStartName"
                res = Me.Infos.ServiceStartName
            Case "State"
                res = Me.Infos.State.ToString
            Case "Description"
                res = Me.Infos.Description
            Case "DiagnosticMessageFile"
                res = Me.Infos.DiagnosticMessageFile
            Case "ObjectName"
                res = Me.Infos.ObjectName
            Case "Process"
                If Me.Infos.ProcessId > 0 Then
                    res = cProcess.GetProcessName(Me.Infos.ProcessId) & " -- " & Me.Infos.ProcessId.ToString
                End If
            Case "Dependencies"
                res = Me.Infos.Dependencies
            Case "TagID"
                res = Me.Infos.TagID.ToString
            Case "ServiceFlags"
                res = Me.Infos.ServiceFlags.ToString
            Case "WaitHint"
                res = Me.Infos.WaitHint.ToString
            Case "CheckPoint"
                res = Me.Infos.CheckPoint.ToString
            Case "Win32ExitCode"
                res = Me.Infos.Win32ExitCode.ToString
            Case "ServiceSpecificExitCode"
                res = Me.Infos.ServiceSpecificExitCode.ToString
        End Select

        Return res
    End Function


#End Region

End Class
