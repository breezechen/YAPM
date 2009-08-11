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

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackServiceEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cServiceConnection
    Private _instanceId As Integer

#Region "Shared code (for dico of services)"

    Public Shared dicoNewServices As New Dictionary(Of String, Boolean)
    Public Shared Sub ClearDico()
        dicoNewServices.Clear()
    End Sub

#End Region

    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cServiceConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public all As Boolean
        Public forInstanceId As Integer
        Public complete As Boolean
        Public Sub New(ByVal pi As Integer, ByVal al As Boolean, ByVal comp As Boolean, ByVal forII As Integer)
            all = al
            forInstanceId = forII
            pid = pi
            complete = comp
        End Sub
    End Structure


    ' When socket got a list !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, serviceInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), serviceInfos))
            Next
        End If

        Try
            'If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
        Catch ex As Exception
            '
        End Try

    End Sub
    Public Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()
        SyncLock dicoNewServices
            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If con.ConnectionObj.IsConnected = False Then
                sem.Release()
                Exit Sub
            End If

            Select Case con.ConnectionObj.ConnectionType

                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    _poolObj = pObj
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestServiceList, pObj.pid, pObj.all)
                        cDat.InstanceId = _instanceId   ' Instance which request the list
                        con.ConnectionObj.Socket.Send(cDat)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                    ' Save current collection
                    Dim res As ManagementObjectCollection = Nothing
                    Try
                        res = con.wmiSearcher.Get()
                    Catch ex As Exception
                        If deg IsNot Nothing AndAlso ctrl.Created Then _
                            ctrl.Invoke(deg, False, Nothing, ex.Message, pObj.forInstanceId)
                        sem.Release()
                        Exit Sub
                    End Try

                    Dim _dico As New Dictionary(Of String, serviceInfos)
                    For Each refService As Management.ManagementObject In res

                        Dim obj As New API.ENUM_SERVICE_STATUS_PROCESS
                        With obj
                            .DisplayName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.DisplayName.ToString))
                            .ServiceName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.Name.ToString))
                            With .ServiceStatusProcess
                                .CheckPoint = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.CheckPoint.ToString))
                                If CBool(refService.GetPropertyValue(API.WMI_INFO_SERVICE.AcceptPause.ToString)) Then
                                    .ControlsAccepted = .ControlsAccepted Or API.SERVICE_ACCEPT.PauseContinue
                                End If
                                If CBool(refService.GetPropertyValue(API.WMI_INFO_SERVICE.AcceptStop.ToString)) Then
                                    .ControlsAccepted = .ControlsAccepted Or API.SERVICE_ACCEPT.Stop
                                End If
                                .CurrentState = getStateFromString(CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.State.ToString)))
                                .ProcessID = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ProcessId.ToString))
                                '.ServiceFlags
                                .ServiceSpecificExitCode = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ServiceSpecificExitCode.ToString))
                                .ServiceType = getTypeFromString(CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ServiceType.ToString)))
                                .WaitHint = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.WaitHint.ToString))
                                .Win32ExitCode = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ExitCode.ToString))
                            End With
                        End With


                        ' Do we have to get fixed infos ?
                        Dim _servInfos As New serviceInfos(obj, True)

                        Dim conf As New API.QUERY_SERVICE_CONFIG
                        With conf
                            .BinaryPathName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.PathName.ToString))
                            '.Dependencies
                            .DisplayName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.DisplayName.ToString))
                            .ErrorControl = getErrorControlFromString(CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ErrorControl.ToString)))
                            '.LoadOrderGroup 
                            .ServiceStartName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.StartName.ToString))
                            .StartType = getStartModeFromString(CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.StartMode.ToString)))
                            .TagID = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.TagId.ToString))
                        End With
                        _servInfos.SetConfig(conf)

                        _dico.Add(obj.ServiceName, _servInfos)
                    Next

                    Try
                        ctrl.Invoke(deg, True, _dico, Nothing, 0)
                    Catch ex As Exception
                        '
                    End Try

                Case Else
                    ' Local

                    Dim _dico As New Dictionary(Of String, serviceInfos)

                    Call enumServices(con.SCManagerLocalHandle, pObj, _dico)

                    Try
                        'If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Functions.GetError, pObj.forInstanceId)
                    Catch ex As Exception
                        '
                    End Try

            End Select
        End SyncLock

        sem.Release()

    End Sub

    ' Enumerate services (local)
    Private Shared memBufferEnumServics As New MemoryAlloc(&H1000)
    Friend Shared Sub enumServices(ByVal hSCM As IntPtr, ByVal pObj As poolObj, ByRef _dico As Dictionary(Of String, serviceInfos))
        Dim lBytesNeeded As Integer
        Dim lServicesReturned As Integer
        Dim tServiceStatus() As API.ENUM_SERVICE_STATUS_PROCESS
        ReDim tServiceStatus(0)

        If Not (hSCM = IntPtr.Zero) Then
            If Not (API.EnumServicesStatusEx(hSCM, _
                                       API.SC_ENUM_PROCESS_INFO, _
                                       API.SERVICE_ALL, _
                                       API.SERVICE_STATE_ALL, _
                                       memBufferEnumServics.Pointer, _
                                       memBufferEnumServics.Size, _
                                       lBytesNeeded, _
                                       lServicesReturned, _
                                       0, _
                                       Nothing)) Then
                ' Resize buffer
                memBufferEnumServics.IncrementSize(lBytesNeeded)
            End If

            If API.EnumServicesStatusEx(hSCM, _
                                       API.SC_ENUM_PROCESS_INFO, _
                                       API.SERVICE_ALL, _
                                       API.SERVICE_STATE_ALL, _
                                       memBufferEnumServics.Pointer, _
                                       memBufferEnumServics.Size, _
                                       lBytesNeeded, _
                                       lServicesReturned, _
                                       0, _
                                       Nothing) Then

                For idx As Integer = 0 To lServicesReturned - 1

                    ' Get structure from memory
                    Dim obj As API.ENUM_SERVICE_STATUS_PROCESS = _
                            memBufferEnumServics.ReadStruct(Of API.ENUM_SERVICE_STATUS_PROCESS)(idx)

                    If pObj.all OrElse pObj.pid = obj.ServiceStatusProcess.ProcessID Then
                        Dim _servINFO As New serviceInfos(obj, pObj.complete)

                        If pObj.all = False OrElse dicoNewServices.ContainsKey(obj.ServiceName) = False Or pObj.complete Then

                            getRegInfos(obj.ServiceName, _servINFO)

                            'PERFISSUE
                            getServiceConfig(obj.ServiceName, hSCM, _servINFO, True)

                            If pObj.all And pObj.complete = False Then
                                dicoNewServices.Add(obj.ServiceName, False)
                            End If
                        End If

                        _dico.Add(obj.ServiceName, _servINFO)
                    End If
                    If pObj.all Then
                        dicoNewServices(obj.ServiceName) = True
                    End If
                Next idx

            End If
        End If


        ' Remove all services that not exist anymore
        If pObj.all Then
            Dim _dicoTemp As Dictionary(Of String, Boolean) = dicoNewServices
            For Each it As System.Collections.Generic.KeyValuePair(Of String, Boolean) In _dicoTemp
                If it.Value = False Then
                    dicoNewServices.Remove(it.Key)
                End If
            Next
        End If

        ' Here we fill _currentServices if necessary
        'PERFISSUE
        cService.SemCurrentServices.WaitOne()
        If cService._currentServices Is Nothing Then
            cService._currentServices = New Dictionary(Of String, cService)
        End If
        For Each pc As serviceInfos In _dico.Values
            If cService._currentServices.ContainsKey(pc.Name) = False Then
                cService._currentServices.Add(pc.Name, New cService(pc))
            End If
        Next
        cService.SemCurrentServices.Release()
    End Sub


    ' Get config of service
    Public Shared Sub getServiceConfig(ByVal name As String, ByVal hSCManager As IntPtr, ByRef _infos As serviceInfos, ByVal getFileInfo As Boolean)

        Dim lServ As IntPtr = API.OpenService(hSCManager, name, API.SERVICE_RIGHTS.SERVICE_QUERY_CONFIG)

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then

                ' Get all available informations
                Dim tt As New API.QUERY_SERVICE_CONFIG
                Dim bytesNeeded As Integer = 0
                Dim fResult As Boolean

                Dim pt As IntPtr = IntPtr.Zero
                fResult = API.QueryServiceConfig(lServ, pt, bytesNeeded, bytesNeeded)
                pt = Marshal.AllocHGlobal(bytesNeeded)
                fResult = API.QueryServiceConfig(lServ, pt, bytesNeeded, bytesNeeded)
                tt = CType(Marshal.PtrToStructure(pt, GetType(API.QUERY_SERVICE_CONFIG)), API.QUERY_SERVICE_CONFIG)
                Marshal.FreeHGlobal(pt)

                ' Set configuration
                _infos.SetConfig(tt)

                API.CloseServiceHandle(lServ)
            End If
        End If
    End Sub

    ' Get infos from registry
    Private Shared Sub getRegInfos(ByVal name As String, ByRef _infos As serviceInfos)
        Dim desc As String = GetServiceInfo(name, "Description")

        If InStr(desc, "@", CompareMethod.Binary) > 0 Then
            desc = cFile.IntelligentPathRetrieving(desc)
        End If
        desc = Replace(desc, "\", "\\")

        Dim obj As String = GetServiceInfo(name, "ObjectName")
        Dim dmf As String = GetServiceInfo(name, "DiagnosticMessageFile")
        _infos.SetRegInfos(desc, dmf, obj)
    End Sub

    ' Retrieve information about a service from registry
    Private Shared Function GetServiceInfo(ByVal name As String, ByVal info As String) As String
        Try
            Return CStr(My.Computer.Registry.GetValue( _
                        "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & name, _
                        info, ""))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ' Get state/errorcontrol/starttype from a string (returned by wmi) as a type
    Private Function getErrorControlFromString(ByVal s As String) As API.SERVICE_ERROR_CONTROL
        Select Case s
            Case "Ignore"
                Return API.SERVICE_ERROR_CONTROL.Ignore
            Case "Normal"
                Return API.SERVICE_ERROR_CONTROL.Normal
            Case "Severe"
                Return API.SERVICE_ERROR_CONTROL.Severe
            Case "Critical"
                Return API.SERVICE_ERROR_CONTROL.Critical
            Case Else
                Return API.SERVICE_ERROR_CONTROL.Unknown
        End Select
    End Function
    Private Function getStartModeFromString(ByVal s As String) As API.SERVICE_START_TYPE
        Select Case s
            Case "Boot"
                Return API.SERVICE_START_TYPE.BootStart
            Case "System"
                Return API.SERVICE_START_TYPE.SystemStart
            Case "Auto"
                Return API.SERVICE_START_TYPE.AutoStart
            Case "Manual"
                Return API.SERVICE_START_TYPE.DemandStart
            Case "Disabled"
                Return API.SERVICE_START_TYPE.StartDisabled
        End Select
    End Function
    Private Function getStateFromString(ByVal s As String) As API.SERVICE_STATE
        Select Case s
            Case "Stopped"
                Return API.SERVICE_STATE.Stopped
            Case "Start Pending"
                Return API.SERVICE_STATE.StartPending
            Case "Stop Pending"
                Return API.SERVICE_STATE.StopPending
            Case "Running"
                Return API.SERVICE_STATE.Running
            Case "Continue Pending"
                Return API.SERVICE_STATE.ContinuePending
            Case "Pause Pending"
                Return API.SERVICE_STATE.PausePending
            Case "Paused"
                Return API.SERVICE_STATE.Paused
            Case Else
                Return API.SERVICE_STATE.Unknown
        End Select
    End Function
    Private Function getTypeFromString(ByVal s As String) As API.SERVICE_TYPE
        Select Case s
            Case "Kernel Driver"
                Return API.SERVICE_TYPE.KernelDriver
            Case "File System Driver"
                Return API.SERVICE_TYPE.FileSystemDriver
            Case "Adapter"
                Return API.SERVICE_TYPE.Adapter
            Case "Recognizer Driver"
                Return API.SERVICE_TYPE.RecognizerDriver
            Case "Own Process"
                Return API.SERVICE_TYPE.Win32OwnProcess
            Case "Share Process"
                Return API.SERVICE_TYPE.Win32ShareProcess
            Case "Interactive Process"
                Return API.SERVICE_TYPE.InteractiveProcess
        End Select
    End Function
End Class
