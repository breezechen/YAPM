Option Strict On

' Here is some unsafe code from VB6

Imports System.Runtime.InteropServices

Module mdlService

    Private Declare Function apiStartService Lib "advapi32.dll" Alias "StartServiceA" (ByVal hService As IntPtr, ByVal dwNumServiceArgs As Integer, ByVal lpServiceArgVectors As Integer) As Integer
    Private Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As IntPtr

    <DllImport("AdvApi32", CharSet:=CharSet.Auto, entrypoint:="ChangeServiceConfigA")> _
        Private Function ChangeServiceConfig(ByVal hService As IntPtr, ByVal dwServiceType As Integer, ByVal dwStartType As Integer, ByVal dwErrorControl As Integer, ByVal lpBinaryPathName As String, ByVal lpLoadOrderGroup As String, ByVal lpdwTagId As Integer, ByVal lpDependencies As String, ByVal lpServiceStartName As String, ByVal lpPassword As String, ByVal lpDisplayName As String) As Boolean
    End Function
    <DllImport("advapi32.dll", SetLastError:=True)> _
    Private Function ControlService(ByVal hService As IntPtr, ByVal dwControl As SERVICE_CONTROL, ByRef lpServiceStatus As SERVICE_STATUS) As Boolean
    End Function
    Private Declare Function OpenService Lib "advapi32.dll" Alias "OpenServiceA" (ByVal hSCManager As IntPtr, ByVal lpServiceName As String, ByVal dwDesiredAccess As Integer) As IntPtr
    <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Function CloseServiceHandle(ByVal serviceHandle As IntPtr) As Boolean
    End Function

    Private Enum SERVICE_CONTROL
        _STOP = 1
        _PAUSE = 2
        _CONTINUE = 3
        _INTERROGATE = 4
        _SHUTDOWN = 5
        _PARAMCHANGE = 6
        _NETBINDADD = 7
        _NETBINDREMOVE = 8
        _NETBINDENABLE = 9
        _NETBINDDISABLE = 10
        _DEVICEEVENT = 11
        _HARDWAREPROFILECHANGE = 12
        _POWEREVENT = 13
        _SESSIONCHANGE = 14
    End Enum

    Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Private Const SERVICE_QUERY_CONFIG As Integer = &H1
    Private Const SERVICE_CHANGE_CONFIG As Integer = &H2
    Private Const SERVICE_QUERY_STATUS As Integer = &H4
    Private Const SERVICE_ENUMERATE_DEPENDENTS As Integer = &H8
    Private Const SERVICE_START As Integer = &H10
    Private Const SERVICE_STOP As Integer = &H20
    Private Const SERVICE_PAUSE_CONTINUE As Integer = &H40
    Private Const SERVICE_INTERROGATE As Integer = &H80
    Private Const SERVICE_USER_DEFINED_CONTROL As Integer = &H100
    Private Const SERVICE_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SERVICE_QUERY_CONFIG + SERVICE_CHANGE_CONFIG + SERVICE_QUERY_STATUS + SERVICE_ENUMERATE_DEPENDENTS + SERVICE_START + SERVICE_STOP + SERVICE_PAUSE_CONTINUE + SERVICE_INTERROGATE + SERVICE_USER_DEFINED_CONTROL)
    Private Const SERVICE_CONTROL_STOP As Integer = &H1
    Private Const SERVICE_CONTROL_CONTINUE As Integer = &H3
    Private Const SERVICE_CONTROL_INTERROGATE As Integer = &H4
    Private Const SERVICE_CONTROL_SHUTDOWN As Integer = &H5
    Private Const SERVICE_CONTROL_PAUSE As Integer = &H2
    Private Const SERVICE_NO_CHANGE As Integer = &HFFFF
    Private Const SC_MANAGER_CONNECT As Integer = &H1
    Private Const SC_MANAGER_CREATE_SERVICE As Integer = &H2
    Private Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4
    Private Const SC_MANAGER_LOCK As Integer = &H8
    Private Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
    Private Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
    Private Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

    Public Structure SERVICE_STATUS
        Dim dwServiceType As Integer
        Dim dwCurrentState As Integer
        Dim dwControlsAccepted As Integer
        Dim dwWin32ExitCode As Integer
        Dim dwServiceSpecificExitCode As Integer
        Dim dwCheckPoint As Integer
        Dim dwWaitHint As Integer
    End Structure

    ' Enumerate services installed on computer
    Public Function Enumerate(ByRef p() As cService) As Integer

        Dim o As System.ServiceProcess.ServiceController() = System.ServiceProcess.ServiceController.GetServices()
        Dim o1 As System.ServiceProcess.ServiceController
        Dim x As Integer

        ReDim p(o.Length - 1)
        x = 0

        For Each o1 In o

            p(x) = New cService
            With p(x)
                .Name = o1.ServiceName
                .LongName = o1.DisplayName
                .Status = o1.Status
                .CanPauseAndContinue = o1.CanPauseAndContinue
                .CanShutdown = o1.CanShutdown
                .CanStop = o1.CanStop
                .IsDisplayed = False
            End With

            x += 1

        Next

        Return x

    End Function

    Public Function GetServiceStartTypeFromInt(ByVal i As Integer) As String
        Dim s As String = vbNullString

        Select Case i
            Case 0  'SERVICE_BOOT_START
                s = "Boot Start"
            Case 1  'SERVICE_SYSTEM_START
                s = "System Start"
            Case 2  'SERVICE_AUTO_START
                s = "Auto Start"
            Case 3  'SERVICE_DEMAND_START
                s = "Demand Start"
            Case 4  'SERVICE_DISABLED
                s = "Disabled"
        End Select

        Return s
    End Function

    ' Retrieve information about a service from registry
    Public Function GetServiceInfo(ByVal service As String, ByVal info As String) As String
        Return CStr(My.Computer.Registry.GetValue( _
                    "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & service, _
                    info, ""))
    End Function

    ' Retrieve file name from a path with arguments
    Public Function GetFileNameFromSpecial(ByVal path As String) As String
        Return ""
    End Function

    ' Start a service
    Public Sub StartService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                apiStartService(lServ, 0, 0)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If

    End Sub

    ' Pause a service
    Public Sub PauseService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._PAUSE, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Resume a service
    Public Sub ResumeService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._CONTINUE, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Stop a service
    Public Sub StopService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._STOP, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Shutdown a service
    Public Sub ShutDownService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._SHUTDOWN, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Set service start type
    Public Sub SetServiceStartType(ByVal service As String, ByVal type As ServiceProcess.ServiceStartMode)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim st As Integer

        Select Case type
            Case ServiceProcess.ServiceStartMode.Automatic
                st = 2
            Case ServiceProcess.ServiceStartMode.Disabled
                st = 4
            Case ServiceProcess.ServiceStartMode.Manual
                st = 3
        End Select

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)

        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ChangeServiceConfig(lServ, SERVICE_NO_CHANGE, st, _
                    SERVICE_NO_CHANGE, vbNullString, vbNullString, 0, _
                    vbNullString, vbNullString, vbNullString, vbNullString)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If

    End Sub
End Module
