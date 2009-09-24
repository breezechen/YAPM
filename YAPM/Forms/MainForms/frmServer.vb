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

Imports System.Net
Imports System.Net.Sockets
Imports YAPM.Common.Misc

Public Class frmServer

    Private WithEvents sock As New AsynchronousSocketListener
    Private _state As SOCK_STATE = SOCK_STATE.Disconnected

    Private Enum SOCK_STATE As Integer
        Connected
        WaitingConnection
        Disconnected
    End Enum

    Private theConnection As New cConnection
    Private _procCon As New cProcessConnection(Me, theConnection, New cProcessConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedProcess))
    Private _windowCon As New cWindowConnection(Me, theConnection, New cWindowConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedWindows))
    Private _envCon As New cEnvVariableConnection(Me, theConnection, New cEnvVariableConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEnvVar))
    Private _handleCon As New cHandleConnection(Me, theConnection, New cHandleConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedHandle))
    Private _memoryCon As New cMemRegionConnection(Me, theConnection, New cMemRegionConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedMemoryReg))
    Private _moduleCon As New cModuleConnection(Me, theConnection, New cModuleConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedModule))
    Private _networkCon As New cNetworkConnection(Me, theConnection, New cNetworkConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedNetwork))
    Private _serviceCon As New cServiceConnection(Me, theConnection, New cServiceConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedService))
    Private _servdepCon As New cServDepConnection(Me, theConnection, New cServDepConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedServDep))
    Private _priviCon As New cPrivilegeConnection(Me, theConnection, New cPrivilegeConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedPrivilege))
    Private _taskCon As New cTaskConnection(Me, theConnection, New cTaskConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedTask))
    Private _threadCon As New cThreadConnection(Me, theConnection, New cThreadConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedThread))
    Private _searchCon As New cSearchConnection(Me, theConnection, New cSearchConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedSearch))
    Private _logCon As New cLogConnection(Me, theConnection, New cLogConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedLog))
    Private _jobCon As New cJobConnection(Me, theConnection, New cJobConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedJobs))
    Private _procInJobCon As New cJobConnection(Me, theConnection, New cJobConnection.HasEnumeratedProcInJobEventHandler(AddressOf HasEnumeratedProcessInJob))
    Private _jobLimitsCon As New cJobLimitConnection(Me, theConnection, New cJobLimitConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedJobLimits))


    ' Connect to local machine
    Private Sub connectLocal()

        ' Set connection
        Try
            With theConnection
                .ConnectionType = cConnection.TypeOfConnection.LocalConnection
                .Connect()
            End With

            _procCon.ConnectionObj = theConnection
            _networkCon.ConnectionObj = theConnection
            _serviceCon.ConnectionObj = theConnection
            _windowCon.ConnectionObj = theConnection
            _threadCon.ConnectionObj = theConnection
            _envCon.ConnectionObj = theConnection
            _handleCon.ConnectionObj = theConnection
            _moduleCon.ConnectionObj = theConnection
            _memoryCon.ConnectionObj = theConnection
            _priviCon.ConnectionObj = theConnection
            _taskCon.ConnectionObj = theConnection
            _searchCon.ConnectionObj = theConnection
            _servdepCon.ConnectionObj = theConnection
            _logCon.ConnectionObj = theConnection

            _networkCon.Connect()
            _moduleCon.Connect()
            _searchCon.Connect()
            _serviceCon.Connect()
            _servdepCon.Connect()
            _envCon.Connect()
            _memoryCon.Connect()
            _taskCon.Connect()
            _priviCon.Connect()
            _windowCon.Connect()
            _threadCon.Connect()
            _handleCon.Connect()
            _procCon.Connect()
            _logCon.Connect()

            cWindow.Connection = _windowCon
            cProcess.Connection = _procCon
            cThread.Connection = _threadCon
            cEnvVariable.Connection = _envCon
            cHandle.Connection = _handleCon
            cMemRegion.Connection = _memoryCon
            cModule.Connection = _moduleCon
            cNetwork.Connection = _networkCon
            cService.Connection = _serviceCon
            cPrivilege.Connection = _priviCon
            cTask.Connection = _taskCon
            cLogItem.Connection = _logCon
            cJob.Connection = _jobCon
            cJobLimit.Connection = _jobLimitsCon

        Catch ex As Exception
            Misc.ShowError(ex, "Unable to connect")
        End Try

    End Sub

#Region "Has enumerated lists"

    Private _TheIdToSend As String = ""
    Private Sub HasEnumeratedEnvVar(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, envVariableInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestEnvironmentVariableList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetEnvVarList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate environnement variables")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate environnement variables")
        End If

    End Sub

    Private Sub HasEnumeratedJobLimits(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, jobLimitInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestJobLimits)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetJobLimitsList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate job limits")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate job limits")
        End If

    End Sub

    Private Sub HasEnumeratedProcessInJob(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, processInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestProcessesInJobList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetProcessList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate processes in job")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate processes in job")
        End If

    End Sub

    Private Sub HasEnumeratedJobs(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, jobInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestJobList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetJobList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate jobs")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate jobs")
        End If

    End Sub

    Private Sub HasEnumeratedLog(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, logItemInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestLogList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetLogList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate log items")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate log items")
        End If

    End Sub

    Private Sub HasEnumeratedServDep(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String, ByVal instanceId As Integer, ByVal type As cServDepConnection.DependenciesToget)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestServDepList, Nothing, type)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetServiceList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate service dependencies")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate service dependencies")
        End If

    End Sub

    Private Sub HasEnumeratedMemoryReg(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, memRegionInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestMemoryRegionList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetMemoryRegList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate memory regions")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate memory regions")
        End If

    End Sub

    Private Sub HasEnumeratedProcess(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, processInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestProcessList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetProcessList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate processes")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate processes")
        End If

    End Sub

    Private Sub HasEnumeratedPrivilege(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, privilegeInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestPrivilegesList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetPrivilegeList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate privileges")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate privileges")
        End If

    End Sub

    Private Sub HasEnumeratedService(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String, ByVal forII As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestServiceList)
                cDat.InstanceId = forII   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetServiceList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate services")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate services")
        End If

    End Sub

    Private Sub HasEnumeratedThread(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, threadInfos), ByVal errorMessage As String, ByVal forII As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestThreadList)
                cDat.InstanceId = forII   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetThreadList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate threads")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate threads")
        End If

    End Sub

    Private Sub HasEnumeratedModule(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, moduleInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestModuleList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetModuleList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate modules")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate modules")
        End If

    End Sub

    Private Sub HasEnumeratedHandle(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, handleInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestHandleList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetHandleList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate handles")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate handles")
        End If

    End Sub

    Private Sub HasEnumeratedNetwork(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, networkInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestNetworkConnectionList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetNetworkList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate network connections")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate network connections")
        End If

    End Sub

    Private Sub HasEnumeratedSearch(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, searchInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestSearchList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetSearchList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to search the string")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to search the string")
        End If

    End Sub

    Private Sub HasEnumeratedTask(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, windowInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestTaskList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetWindowsList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate tasks")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate tasks")
        End If

    End Sub

    Private Sub HasEnumeratedWindows(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, windowInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestWindowList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetWindowsList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate windows")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate windows")
        End If

    End Sub
#End Region

    Private Sub frmServeur_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Try
        sock.Disconnect()
        'Catch ex As Exception
        '    '
        'End Try
    End Sub

    Private Delegate Sub ChangeConnectState(ByVal state As SOCK_STATE)
    Private Sub handlerChangeConnectState(ByVal state As SOCK_STATE)
#If RELEASE_MODE Then
        'Select Case state
        '    Case SOCK_STATE.Connected
        '        Me.cmdConnection.Text = "Disconnect !"
        '        Me.Text = "YAPM remote process (connected)"
        '    Case SOCK_STATE.Disconnected
        '        Me.cmdConnection.Text = "Connect !"
        '        Me.Text = "YAPM remote process (disconnected)"
        '    Case SOCK_STATE.WaitingConnection
        '        Me.cmdConnection.Text = "Disconnect !"
        '        Me.Text = "YAPM remote process (waiting for client to connect...)"
        'End Select
#End If
    End Sub
    Private Sub sock_ConnexionAccepted() Handles sock.Connected
        _state = SOCK_STATE.Connected
        Dim h As New ChangeConnectState(AddressOf handlerChangeConnectState)
        h.Invoke(SOCK_STATE.Connected)
    End Sub
    Private Sub sock_Disconnected() Handles sock.Disconnected
        _state = SOCK_STATE.Disconnected
        Dim h As New ChangeConnectState(AddressOf handlerChangeConnectState)
        h.Invoke(SOCK_STATE.Disconnected)
    End Sub
    Private Sub sock_Waiting() Handles sock.WaitingForConnection
        _state = SOCK_STATE.WaitingConnection
        Dim h As New ChangeConnectState(AddressOf handlerChangeConnectState)
        h.Invoke(SOCK_STATE.WaitingConnection)
    End Sub

    Private Sub sock_ReceivedData(ByRef cData As cSocketData) Handles sock.ReceivedData
        Try

            Dim ret As Boolean = True       ' Return for the functions (orders)

            If cData Is Nothing Then
                Trace.WriteLine("Serialization error")
                Exit Sub
            End If

            Dim _forInstanceId As Integer = cData.InstanceId
            Dim _idToSend As String = cData._id
            _TheIdToSend = _idToSend

            ' Add item to history
            Me.Invoke(New addItemHandler(AddressOf addItem), cData)

            ' Extract the type of information we have to send
            If cData.Type = cSocketData.DataType.Order Then

                ' ===== Request lists and informations
                Select Case cData.Order
                    Case cSocketData.OrderType.RequestProcessList
                        Call _procCon.Enumerate(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestNetworkConnectionList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Dim all As Boolean = CBool(cData.Param2)
                        Call _networkCon.Enumerate(True, pid, all, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestJobList
                        Call _jobCon.Enumerate(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestProcessesInJobList
                        Dim name As String = CStr(cData.Param1)
                        Call _procInJobCon.EnumerateProcessesInJob(name, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestJobLimits
                        Dim name As String = CStr(cData.Param1)
                        Call _jobLimitsCon.Enumerate(name, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestServiceList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim all As Boolean = CBool(cData.Param2)
                        Call _serviceCon.Enumerate(True, pid, True, all, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestModuleList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Call _moduleCon.Enumerate(True, pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestThreadList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Call _threadCon.Enumerate(True, pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestHandleList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Dim unn As Boolean = CBool(cData.Param2)
                        Call _handleCon.Enumerate(True, pid, unn, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestWindowList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Dim all As Boolean = CBool(cData.Param3)
                        Dim unn As Boolean = CBool(cData.Param2)
                        Call _windowCon.Enumerate(True, pid, unn, all, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestTaskList
                        Call _taskCon.Enumerate(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestSearchList
                        Dim st As String = CStr(cData.Param1)
                        Dim include As Native.Api.Enums.GeneralObjectType = CType(cData.Param2, Native.Api.Enums.GeneralObjectType)
                        Dim _case As Boolean = CBool(cData.Param3)
                        Call _searchCon.Enumerate(st, _case, include, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestPrivilegesList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call _priviCon.Enumerate(True, pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestEnvironmentVariableList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim peb As IntPtr = CType(cData.Param2, IntPtr)
                        Call _envCon.Enumerate(True, pid, peb, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestMemoryRegionList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call _memoryCon.Enumerate(True, pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestServDepList
                        Dim name As String = CStr(cData.Param1)
                        Dim type As cServDepConnection.DependenciesToget = CType(cData.Param2, cServDepConnection.DependenciesToget)
                        Call _servdepCon.Enumerate(name, type, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestProcessorCount
                        Dim procCount As Integer = cSystemInfo.GetProcessorCount
                        Try
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ReturnProcessorCount, procCount)
                            cDat._id = _idToSend
                            sock.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not request processor count")
                        End Try
                        Exit Sub
                    Case cSocketData.OrderType.RequestLogList
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim infos As asyncCallbackLogEnumerate.LogItemType = CType(cData.Param2, asyncCallbackLogEnumerate.LogItemType)
                        Call _logCon.Enumerate(infos, pid, _forInstanceId)
                        Exit Sub
                End Select



                ' ===== Process functions
                Select Case cData.Order
                    Case cSocketData.OrderType.ProcessCreateNew
                        Try
                            Dim s As String = CStr(cData.Param1)
                            Dim pid As Integer = Shell(s, AppWinStyle.NormalFocus)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not create a new process")
                        End Try
                    Case cSocketData.OrderType.ProcessReanalize
                        asyncCallbackProcEnumerate.ReanalizeLocalAfterSocket(CType(cData.Param1, Integer()))
                    Case cSocketData.OrderType.ProcessChangeAffinity
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim aff As Integer = CType(cData.Param2, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).SetAffinity(aff)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process affinity")
                        End Try
                    Case cSocketData.OrderType.ProcessChangePriority
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim level As System.Diagnostics.ProcessPriorityClass = CType(cData.Param2, ProcessPriorityClass)
                        Try
                            Native.Objects.Process.GetProcessById(pid).SetPriority(level)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process priority")
                        End Try
                    Case cSocketData.OrderType.ProcessDecreasePriority
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).DecreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process priority")
                        End Try
                    Case cSocketData.OrderType.ProcessIncreasePriority
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).IncreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process priority")
                        End Try
                    Case cSocketData.OrderType.ProcessKill
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).Kill()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not kill process")
                        End Try
                    Case cSocketData.OrderType.ProcessKillByMethod
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim method As Native.Api.Enums.KillMethod = CType(cData.Param2, Native.Api.Enums.KillMethod)
                        Try
                            Native.Objects.Process.GetProcessById(pid).KillByMethod(method)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not kill process by method")
                        End Try
                    Case cSocketData.OrderType.ProcessKillTree
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).KillProcessTree()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not kill process tree")
                        End Try
                    Case cSocketData.OrderType.ProcessReduceWorkingSet
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).EmptyWorkingSetSize()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not reduce process' working set size")
                        End Try
                    Case cSocketData.OrderType.ProcessResume
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).ResumeProcess()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not resume process")
                        End Try
                    Case cSocketData.OrderType.ProcessSuspend
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            Native.Objects.Process.GetProcessById(pid).SuspendProcess()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not suspend process")
                        End Try
                End Select



                ' ===== Windows functions
                Select Case cData.Order
                    Case cSocketData.OrderType.WindowBringToFront
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim value As Boolean = CBool(cData.Param2)
                        Call (New cWindow(hWnd)).BringToFront(value)
                    Case cSocketData.OrderType.WindowClose
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Close()
                    Case cSocketData.OrderType.WindowDisable
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Enabled = False
                    Case cSocketData.OrderType.WindowEnable
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Enabled = True
                    Case cSocketData.OrderType.WindowFlash
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Flash()
                    Case cSocketData.OrderType.WindowHide
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Hide()
                    Case cSocketData.OrderType.WindowMaximize
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Maximize()
                    Case cSocketData.OrderType.WindowMinimize
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Minimize()
                    Case cSocketData.OrderType.WindowSetAsActiveWindow
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).SetAsActiveWindow()
                    Case cSocketData.OrderType.WindowSetAsForegroundWindow
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).SetAsForegroundWindow()
                    Case cSocketData.OrderType.WindowSetCaption
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim s As String = CStr(cData.Param2)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Caption = s
                    Case cSocketData.OrderType.WindowSetOpacity
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim o As Byte = CByte(cData.Param2)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Opacity = o
                    Case cSocketData.OrderType.WindowSetPositions
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim r As Native.Api.NativeStructs.Rect = CType(cData.Param2, Native.Api.NativeStructs.Rect)
                        Call (New cWindow(hWnd)).SetPositions(r)
                    Case cSocketData.OrderType.WindowShow
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Show()
                    Case cSocketData.OrderType.WindowStopFlashing
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).StopFlashing()
                End Select



                ' ===== Service functions
                Select Case cData.Order
                    Case cSocketData.OrderType.ServiceDelete
                        Dim name As String = CStr(cData.Param1)
                        Try
                            Native.Objects.Service.GetServiceByName(name).DeleteService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not delete service")
                        End Try
                    Case cSocketData.OrderType.ServicePause
                        Dim name As String = CStr(cData.Param1)
                        Try
                            Native.Objects.Service.GetServiceByName(name).PauseService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not pause service")
                        End Try
                    Case cSocketData.OrderType.ServiceChangeServiceStartType
                        Dim name As String = CStr(cData.Param1)
                        Dim type As Native.Api.NativeEnums.ServiceStartType = CType(cData.Param2, Native.Api.NativeEnums.ServiceStartType)
                        Try
                            Native.Objects.Service.GetServiceByName(name).SetServiceStartType(type)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change service start type")
                        End Try
                    Case cSocketData.OrderType.ServiceResume
                        Dim name As String = CStr(cData.Param1)
                        Try
                            Native.Objects.Service.GetServiceByName(name).ResumeService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not resume service")
                        End Try
                    Case cSocketData.OrderType.ServiceStart
                        Dim name As String = CStr(cData.Param1)
                        Try
                            Native.Objects.Service.GetServiceByName(name).StartService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not start service")
                        End Try
                    Case cSocketData.OrderType.ServiceStop
                        Dim name As String = CStr(cData.Param1)
                        Try
                            Native.Objects.Service.GetServiceByName(name).StopService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not stop service")
                        End Try
                End Select



                ' ===== Thread functions
                Select Case cData.Order
                    Case cSocketData.OrderType.ThreadDecreasePriority
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), True)).DecreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change thread priority")
                        End Try
                    Case cSocketData.OrderType.ThreadIncreasePriority
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), True)).IncreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change thread priority")
                        End Try
                    Case cSocketData.OrderType.ThreadResume
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).ThreadResume()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not resume thread")
                        End Try
                    Case cSocketData.OrderType.ThreadSetAffinity
                        'TODO
                    Case cSocketData.OrderType.ThreadSetPriority
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Dim level As Integer = CInt(cData.Param3)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).SetPriority(CType(level, ThreadPriorityLevel))
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not set thread priority")
                        End Try
                    Case cSocketData.OrderType.ThreadSuspend
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).ThreadSuspend()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not suspend thread")
                        End Try
                    Case cSocketData.OrderType.ThreadTerminate
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).ThreadTerminate()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not terminate thread")
                        End Try
                End Select



                ' ===== Other functions
                Select Case cData.Order
                    Case cSocketData.OrderType.JobTerminate
                        Dim name As String = CType(cData.Param1, String)
                        Try
                            cJob.SharedLRTerminateJob(name)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not terminate job")
                        End Try
                    Case cSocketData.OrderType.JobSetLimits
                        Dim name As String = CType(cData.Param1, String)
                        Dim l1 As Native.Api.NativeStructs.JobObjectBasicUiRestrictions = CType(cData.Param2, Native.Api.NativeStructs.JobObjectBasicUiRestrictions)
                        Dim l2 As Native.Api.NativeStructs.JobObjectExtendedLimitInformation = CType(cData.Param3, Native.Api.NativeStructs.JobObjectExtendedLimitInformation)
                        Try
                            cJob.SharedLRSetLimits(name, l1, l2)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not set job limits")
                        End Try
                    Case cSocketData.OrderType.JobAddProcessToJob
                        Dim name As String = CType(cData.Param1, String)
                        Dim pid() As Integer = CType(cData.Param2, Integer())
                        Try
                            cJob.SharedLRAddProcess(name, pid)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not add process to job")
                        End Try
                    Case cSocketData.OrderType.MemoryFree
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim address As IntPtr = CType(cData.Param2, IntPtr)
                        Dim size As IntPtr = CType(cData.Param3, IntPtr)
                        Dim type As Native.Api.NativeEnums.MemoryState = CType(cData.Param4, Native.Api.NativeEnums.MemoryState)
                        Try
                            cMemRegion.SharedLRFree(pid, address, size, type)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not free memory region")
                        End Try
                    Case cSocketData.OrderType.MemoryChangeProtectionType
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim address As IntPtr = CType(cData.Param2, IntPtr)
                        Dim size As IntPtr = CType(cData.Param3, IntPtr)
                        Dim type As Native.Api.NativeEnums.MemoryProtectionType = CType(cData.Param4, Native.Api.NativeEnums.MemoryProtectionType)
                        Try
                            cMemRegion.SharedLRChangeProtection(pid, address, size, type)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change memory region protection type")
                        End Try
                    Case cSocketData.OrderType.HandleClose
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim handle As IntPtr = CType(cData.Param2, IntPtr)
                        Try
                            cHandle.SharedLRCloseHandle(pid, handle)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not close handle")
                        End Try
                    Case cSocketData.OrderType.ModuleUnload
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim address As IntPtr = CType(cData.Param2, IntPtr)
                        Try
                            cProcess.SharedRLUnLoadModuleFromProcess(pid, address)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not unload module")
                        End Try
                    Case cSocketData.OrderType.PrivilegeChangeStatus
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim name As String = CType(cData.Param2, String)
                        Dim status As Native.Api.NativeEnums.SePrivilegeAttributes = CType(cData.Param3, Native.Api.NativeEnums.SePrivilegeAttributes)
                        Try
                            cPrivilege.LocalChangeStatus(pid, name, status)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change privilege status")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandHibernate
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Hibernate(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not hibernate system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandLock
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Lock()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not lock system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandLogoff
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Logoff(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not logoff system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandPoweroff
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Poweroff(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not poweroff system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandRestart
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Restart(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not restart system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandShutdown
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Shutdown(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not shutdown system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandSleep
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Sleep(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not sleep system")
                        End Try
                    Case cSocketData.OrderType.TcpClose
                        Dim local As IPEndPoint = CType(cData.Param1, IPEndPoint)
                        Dim remote As IPEndPoint = CType(cData.Param2, IPEndPoint)
                        Try
                            cNetwork.LocalCloseTCP(local, remote)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not close TCP connection")
                        End Try
                End Select


            End If

        Catch ex As Exception
            Misc.ShowError(ex, "Could not process client request")
        End Try
    End Sub

    Private Sub frmServeur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Program.Parameters.ModeHidden Then
            Me.Left = Pref.LEFT_POSITION_HIDDEN
            Me.ShowInTaskbar = False
        End If

        Native.Functions.Misc.SetTheme(Me.lvServer.Handle)

        SetToolTip(Me.txtIp, "Available IP of this machine")

        'sock.ConnexionAccepted = New AsynchronousServer.ConnexionAcceptedEventHandle(AddressOf sock_ConnexionAccepted)
        'sock.Disconnected = New AsynchronousServer.DisconnectedEventHandler(AddressOf sock_Disconnected)
        'sock.SentData = New AsynchronousServer.SentDataEventHandler(AddressOf sock_SentData)

        connectLocal()

        Dim s() As String = GetIpv4Ips()
        If (s Is Nothing) OrElse s.Length = 0 Then
            Me.txtIp.Text = "Error while trying to retrieve local IP address."
        ElseIf s.Length = 1 Then
            Me.txtIp.Text = "You will have to configure YAPM with this IP : " & s(0)
        Else
            Me.txtIp.Text = "You have more than one network card, so you will have to use one of these IP addresses to configure YAPM : " & vbNewLine
            For Each x As String In s
                Me.txtIp.Text &= x & vbNewLine
            Next
            Me.txtIp.Text = Me.txtIp.Text.Substring(0, Me.txtIp.Text.Length - 2)
        End If

        ' Connect if automode = true
        If Program.Parameters.AutoConnect Then
            Call cmdConnection_Click(Nothing, Nothing)
        End If

    End Sub

    Private Delegate Sub addItemHandler(ByRef dat As cSocketData)
    Private Sub addItem(ByRef dat As cSocketData)
        Dim it As New ListViewItem(Date.Now.ToLongDateString & " - " & Date.Now.ToLongTimeString)
        it.SubItems.Add(dat.ToString)
        Me.lvServer.Items.Add(it)
    End Sub

    Private Sub cmdConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Connect or disconnect the socket (server)
        Dim t As New System.Threading.WaitCallback(AddressOf conDegCallBack)
        Call Threading.ThreadPool.QueueUserWorkItem(t, Nothing)
    End Sub

    Private Sub conDegCallBack(ByVal obj As Object)
        'Try
        If _state = SOCK_STATE.Disconnected Then
            sock.Connect(Parameters.RemotePort)
        Else
            sock.Disconnect()
        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    ' Send an error message to the client
    Public Sub SendErrorToClient(ByVal _ex As cError)
        Try
            Dim cDat As New cSocketData(cSocketData.DataType.ErrorOnServer, , New SerializableException(_ex))
            sock.Send(cDat)
        Catch ex As Exception
            ' FAILED !!
            Misc.ShowDebugError(ex)
        End Try
    End Sub

End Class
