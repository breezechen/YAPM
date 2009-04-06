Option Strict On

Imports CoreFunc.cServiceConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackServiceEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Shared dicoNewServices As New Dictionary(Of String, Boolean)

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cServiceConnection
        Public pid As Integer
        Public all As Boolean
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cServiceConnection, ByVal pi As Integer, ByVal al As Boolean)
            ctrl = ctr
            deg = de
            all = al
            con = co
            pid = pi
        End Sub
    End Structure

    Public Shared Sub ClearDico()
        dicoNewServices.Clear()
    End Sub

    Public Shared Sub Process(ByVal thePoolObj As Object)

        SyncLock dicoNewServices
            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If pObj.con.ConnectionObj.IsConnected = False Then
                Exit Sub
            End If

            Select Case pObj.con.ConnectionObj.ConnectionType

                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                    ' Save current collection
                    Dim res As ManagementObjectCollection = Nothing
                    Try
                        res = pObj.con.wmiSearcher.Get()
                    Catch ex As Exception
                        pObj.ctrl.Invoke(pObj.deg, False, Nothing, ex.Message)
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
                                '.CurrentState = CType(refService.GetPropertyValue(API.WMI_INFO_SERVICE.State.ToString), API.SERVICE_STATE)
                                .ProcessID = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ProcessId.ToString))
                                '.ServiceFlags
                                .ServiceSpecificExitCode = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ServiceSpecificExitCode.ToString))
                                '.ServiceType = CType(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ServiceType.ToString), API.SERVICE_TYPE)
                                .WaitHint = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.WaitHint.ToString))
                                .Win32ExitCode = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ExitCode.ToString))
                            End With
                        End With


                        ' Do we have to get fixed infos ?
                        Dim _servInfos As New serviceInfos(obj)
                        If dicoNewServices.ContainsKey(obj.ServiceName) = False Then

                            Dim conf As New API.QUERY_SERVICE_CONFIG
                            With conf
                                .BinaryPathName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.PathName.ToString))
                                '.Dependencies
                                .DisplayName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.DisplayName.ToString))
                                '.ErrorControl = CType(refService.GetPropertyValue(API.WMI_INFO_SERVICE.ErrorControl.ToString), API.SERVICE_ERROR_CONTROL)
                                '.LoadOrderGroup 
                                .ServiceStartName = CStr(refService.GetPropertyValue(API.WMI_INFO_SERVICE.StartName.ToString))
                                '.StartType = CType(refService.GetPropertyValue(API.WMI_INFO_SERVICE.StartMode.ToString), API.SERVICE_START_TYPE)
                                .TagID = CInt(refService.GetPropertyValue(API.WMI_INFO_SERVICE.TagId.ToString))
                            End With
                            _servInfos.SetConfig(conf)

                            dicoNewServices.Add(obj.ServiceName, False)

                        End If

                        ' Set true so that the process is marked as existing
                        dicoNewServices(obj.ServiceName) = True
                        _dico.Add(obj.ServiceName, _servInfos)
                    Next

                    ' Remove all services that not exist anymore
                    Dim _dicoTemp As Dictionary(Of String, Boolean) = dicoNewServices
                    For Each it As System.Collections.Generic.KeyValuePair(Of String, Boolean) In _dicoTemp
                        If it.Value = False Then
                            dicoNewServices.Remove(it.Key)
                        End If
                    Next
                    pObj.ctrl.Invoke(pObj.deg, True, _dico, Nothing)

                Case Else
                    ' Local

                    Dim _dico As New Dictionary(Of String, serviceInfos)
                    Dim lR As Integer
                    Dim lBytesNeeded As Integer
                    Dim lServicesReturned As Integer
                    Dim tServiceStatus() As API.ENUM_SERVICE_STATUS_PROCESS
                    ReDim tServiceStatus(0)
                    Dim lStructsNeeded As Integer
                    Dim lServiceStatusInfoBuffer As Integer

                    Dim hSCM As IntPtr = pObj.con.SCManagerLocalHandle

                    If Not (hSCM = IntPtr.Zero) Then
                        lR = API.EnumServicesStatusEx(hSCM, _
                                                  API.SC_ENUM_PROCESS_INFO, _
                                                  API.SERVICE_ALL, _
                                                  API.SERVICE_STATE_ALL, _
                                                  Nothing, _
                                                  0, _
                                                  lBytesNeeded, _
                                                  lServicesReturned, _
                                                  0, _
                                                  0)

                        If (lR = 0 And Err.LastDllError = API.ERROR_MORE_DATA) Then

                            lStructsNeeded = CInt(lBytesNeeded / Marshal.SizeOf(tServiceStatus(0)) + 1)
                            ReDim tServiceStatus(lStructsNeeded - 1)
                            lServiceStatusInfoBuffer = lStructsNeeded * (Marshal.SizeOf(tServiceStatus(0)))

                            Dim pt As IntPtr = Marshal.AllocHGlobal(lServiceStatusInfoBuffer)
                            lR = API.EnumServicesStatusEx(hSCM, _
                                                      API.SC_ENUM_PROCESS_INFO, _
                                                      API.SERVICE_ALL, _
                                                      API.SERVICE_STATE_ALL, _
                                                      pt, _
                                                      lServiceStatusInfoBuffer, _
                                                      lBytesNeeded, _
                                                      lServicesReturned, _
                                                      0, _
                                                      0)

                            If Not (lR = 0) Then
                                Dim k As Integer = 0
                                Dim obj As New API.ENUM_SERVICE_STATUS_PROCESS

                                For idx As Integer = 0 To lServicesReturned - 1
                                    Dim off As Integer = pt.ToInt32 + Marshal.SizeOf(obj) * idx
                                    obj = CType(Marshal.PtrToStructure(CType(off, IntPtr), _
                                            GetType(API.ENUM_SERVICE_STATUS_PROCESS)), API.ENUM_SERVICE_STATUS_PROCESS)

                                    If pObj.all OrElse pObj.pid = obj.ServiceStatusProcess.ProcessID Then
                                        Dim _servINFO As New serviceInfos(obj)

                                        If dicoNewServices.ContainsKey(obj.ServiceName) = False Then
                                            getRegInfos(obj.ServiceName, _servINFO)
                                            dicoNewServices.Add(obj.ServiceName, False)
                                        End If

                                        _dico.Add(obj.ServiceName, _servINFO)
                                    End If
                                    dicoNewServices(obj.ServiceName) = True
                                Next idx

                            End If
                            Marshal.FreeHGlobal(pt)
                        End If

                    End If

                    ' Remove all services that not exist anymore
                    Dim _dicoTemp As Dictionary(Of String, Boolean) = dicoNewServices
                    For Each it As System.Collections.Generic.KeyValuePair(Of String, Boolean) In _dicoTemp
                        If it.Value = False Then
                            dicoNewServices.Remove(it.Key)
                        End If
                    Next

                    pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

            End Select
        End SyncLock
    End Sub


    ' Get config of service
    Public Shared Sub getServiceConfig(ByVal name As String, ByVal hSCManager As IntPtr, ByRef _infos As serviceInfos)

        Dim lServ As IntPtr = API.OpenService(hSCManager, name, API.SERVICE_RIGHTS.SERVICE_QUERY_CONFIG)

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then

                ' Get all available informations
                Dim tt As New API.QUERY_SERVICE_CONFIG
                Dim bufSize As Integer = Marshal.SizeOf(tt)
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
End Class
