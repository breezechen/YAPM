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

Public Class cService
    Inherits cGeneralObject

    Private Const NO_DEPENDENCIES As String = ""

    ' Current services running
    ' Protected by sem (semaphore)
    Public Shared _currentServices As Dictionary(Of String, cService)

    Private _firstRefresh As Boolean = True
    Private _serviceInfos As serviceInfos
    Private _path As String
    Private __dep As String
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

    Private Shared _semCurrentServ As New System.Threading.Semaphore(1, 1)
    Public Shared Property CurrentServices() As Dictionary(Of String, cService)
        Get
            Return _currentServices
        End Get
        Set(ByVal value As Dictionary(Of String, cService))
            _currentServices = value
        End Set
    End Property
    Public Shared ReadOnly Property SemCurrentServices() As System.Threading.Semaphore
        Get
            Return _semCurrentServ
        End Get
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
    Public Overrides ReadOnly Property ItemHasChanged() As Boolean
        Get
            Static _first As Boolean = True
            If _first Then
                _first = False
                Return True
            Else
                Return _serviceInfos.ItemHasChanged
            End If
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
    ' (used for Reanalize)
    Public Sub Refresh()
        Select Case _connection.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.LocalConnection
                asyncCallbackServiceEnumerate.getServiceConfig(Me.Infos.Name, _
                        _connection.SCManagerLocalHandle, Me.Infos, True)

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket


        End Select
    End Sub

#Region "All actions on services (start...)"

    ' Start service
    Private _startServ As asyncCallbackServiceStart
    Public Function StartService() As Integer

        If _startServ Is Nothing Then
            _startServ = New asyncCallbackServiceStart(New asyncCallbackServiceStart.HasStarted(AddressOf startServiceDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _startServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackServiceStart.poolObj(Me.Infos.Name, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub startServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not start service " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub


    ' Pause a service
    Private _pauseServ As asyncCallbackServicePause
    Public Function PauseService() As Integer

        If _pauseServ Is Nothing Then
            _pauseServ = New asyncCallbackServicePause(New asyncCallbackServicePause.HasPaused(AddressOf pauseServiceDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _pauseServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackServicePause.poolObj(Me.Infos.Name, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub pauseServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not pause service " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Resume a service
    Private _resumeServ As asyncCallbackServiceResume
    Public Function ResumeService() As Integer

        If _resumeServ Is Nothing Then
            _resumeServ = New asyncCallbackServiceResume(New asyncCallbackServiceResume.HasResumed(AddressOf resumeServiceDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _resumeServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackServiceResume.poolObj(Me.Infos.Name, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub resumeServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume service " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Stop a service
    Private _stopServ As asyncCallbackServiceStop
    Public Function StopService() As Integer

        If _stopServ Is Nothing Then
            _stopServ = New asyncCallbackServiceStop(New asyncCallbackServiceStop.HasStopped(AddressOf stopServiceDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _stopServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackServiceStop.poolObj(Me.Infos.Name, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub stopServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not stop service " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Set service start type
    Private _setStartTypeServ As asyncCallbackServiceSetStartType
    Public Sub SetServiceStartType(ByVal type As API.SERVICE_START_TYPE)

        If _setStartTypeServ Is Nothing Then
            _setStartTypeServ = New asyncCallbackServiceSetStartType(New asyncCallbackServiceSetStartType.HasChangedStartType(AddressOf setServiceStartTypeDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setStartTypeServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackServiceSetStartType.poolObj(Me.Infos.Name, type, newAction))

        AddPendingTask2(newAction, t)
    End Sub
    Private Sub setServiceStartTypeDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change start type of service " & name)
        Else
            Call Me.Refresh()
        End If
        RemovePendingTask(actionNumber)
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
                    _path = GetRealPath(Me.Infos.ImagePath)
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
                If __dep Is Nothing Then
                    If Me.Infos.Dependencies IsNot Nothing Then
                        For Each s As String In Me.Infos.Dependencies
                            __dep &= s & "   "
                        Next
                    Else
                        __dep = NO_DEPENDENCIES
                    End If
                    If __dep Is Nothing Then
                        __dep = NO_DEPENDENCIES
                    End If
                End If
                    res = __dep
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

#Region "Shared function"

    Private Shared _sharedstopServ As asyncCallbackServiceStop
    Public Shared Function SharedLRStopService(ByVal name As String) As Integer

        If _sharedstopServ Is Nothing Then
            _sharedstopServ = New asyncCallbackServiceStop(New asyncCallbackServiceStop.HasStopped(AddressOf stopsharedServiceDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedstopServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackServiceStop.poolObj(name, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub stopsharedServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not stop service " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

    ' Get a service by name
    Public Shared Function GetServiceByName(ByVal name As String) As cService

        Dim tt As cService = Nothing
        cService.SemCurrentServices.WaitOne()
        If _currentServices.ContainsKey(name) Then
            tt = _currentServices.Item(name)
        End If
        cService.SemCurrentServices.Release()

        Return tt

    End Function

End Class
