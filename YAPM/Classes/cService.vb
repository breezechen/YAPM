' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Imports System.Runtime.InteropServices

Public Class cService

#Region "API"
    ' ========================================
    ' API declarations
    ' ========================================
    Private Declare Function apiStartService Lib "advapi32.dll" Alias "StartServiceA" (ByVal hService As IntPtr, ByVal dwNumServiceArgs As Integer, ByVal lpServiceArgVectors As Integer) As Integer
    Private Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As IntPtr

    <DllImport("advapi32.dll", CharSet:=CharSet.Auto, entrypoint:="ChangeServiceConfigA", SetLastError:=True)> _
    Private Shared Function ChangeServiceConfig(ByVal hService As Integer, ByVal dwServiceType As ServiceType, ByVal dwStartType As TypeServiceStartType, ByVal dwErrorControl As ServiceErrorControl, ByVal lpBinaryPathName As String, ByVal lpLoadOrderGroup As String, ByVal lpdwTagId As Integer, ByVal lpDependencies As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpServiceStartName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpPassword As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpDisplayName As String) As Boolean
    End Function

    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function LockServiceDatabase(ByVal hSCManager As Integer) As Integer
    End Function
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function UnlockServiceDatabase(ByVal hSCManager As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Private Shared Function ControlService(ByVal hService As IntPtr, ByVal dwControl As SERVICE_CONTROL, ByRef lpServiceStatus As SERVICE_STATUS) As Boolean
    End Function

    Private Declare Function OpenService Lib "advapi32.dll" Alias "OpenServiceA" (ByVal hSCManager As IntPtr, ByVal lpServiceName As String, ByVal dwDesiredAccess As Integer) As IntPtr

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Private Shared Function CloseServiceHandle(ByVal serviceHandle As IntPtr) As Boolean
    End Function



    Private Const ERROR_MORE_DATA As Integer = 234
    Private Const SC_ENUM_PROCESS_INFO As Integer = &H0
    Private Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4

    Private Const SERVICE_ACTIVE As Integer = &H1
    Private Const SERVICE_INACTIVE As Integer = &H2
    Private Const SERVICE_STATE_ALL As Integer = (SERVICE_ACTIVE Or SERVICE_INACTIVE)

    Private Const SERVICE_WIN32_OWN_PROCESS As Integer = &H10
    Private Const SERVICE_WIN32_SHARE_PROCESS As Integer = &H20
    Private Const SERVICE_WIN32 As Integer = SERVICE_WIN32_OWN_PROCESS + SERVICE_WIN32_SHARE_PROCESS

    Public Structure SERVICE_STATUS_PROCESS
        Dim dwServiceType As Integer
        Dim dwCurrentState As Integer
        Dim dwControlsAccepted As Integer
        Dim dwWin32ExitCode As Integer
        Dim dwServiceSpecificExitCode As Integer
        Dim dwCheckPoint As Integer
        Dim dwWaitHint As Integer
        Dim dwProcessId As Integer
        Dim dwServiceFlags As Integer
    End Structure

    Private Structure ENUM_SERVICE_STATUS_PROCESS
        Dim lpServiceName As Integer
        Dim lpDisplayName As Integer
        Dim ServiceStatus As SERVICE_STATUS_PROCESS
    End Structure

    Private Declare Function EnumServicesStatusEx Lib "advapi32.dll" Alias "EnumServicesStatusExA" (ByVal hSCManager As IntPtr, ByVal InfoLevel As Integer, ByVal dwServiceType As Integer, ByVal dwServiceState As Integer, ByRef lpServices As ENUM_SERVICE_STATUS_PROCESS, ByVal cbBufSize As Integer, ByRef pcbBytesNeeded As Integer, ByRef lpServicesReturned As Integer, ByRef lpResumeHandle As Integer, ByRef pszGroupName As String) As Integer
    Private Declare Function lstrlenA Lib "kernel32" (ByVal Ptr As Integer) As Integer
    Private Declare Function lstrcpyA Lib "kernel32" (ByVal RetVal As String, ByVal Ptr As Integer) As Integer
    Private Declare Function CloseServiceHandle Lib "advapi32.dll" (ByVal hSCObject As Integer) As Integer

    Private Const SERVICE_DRIVER As Integer = &HB
    Private Const SERVICE_INTERACTIVE_PROCESS As Integer = &H100
    Private Const SERVICE_ALL As Integer = SERVICE_DRIVER Or SERVICE_WIN32_OWN_PROCESS Or _
        SERVICE_WIN32_SHARE_PROCESS Or SERVICE_WIN32 Or SERVICE_INTERACTIVE_PROCESS

    ' ========================================
    ' Enums
    ' ========================================
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
    Private Enum ServiceType As Integer
        SERVICE_KERNEL_DRIVER = &H1
        SERVICE_FILE_SYSTEM_DRIVER = &H2
        SERVICE_WIN32_OWN_PROCESS = &H10
        SERVICE_WIN32_SHARE_PROCESS = &H20
        SERVICE_INTERACTIVE_PROCESS = &H100
        SERVICETYPE_NO_CHANGE = SERVICE_NO_CHANGE
    End Enum
    Private Enum ServiceErrorControl As Integer
        SERVICE_ERROR_IGNORE = &H0
        SERVICE_ERROR_NORMAL = &H1
        SERVICE_ERROR_SEVERE = &H2
        SERVICE_ERROR_CRITICAL = &H3
        SERVICEERRORCONTROL_NO_CHANGE = SERVICE_NO_CHANGE
    End Enum

    ' ========================================
    ' Constants
    ' ========================================
    Private Const SERVICE_CONTROL_STOP As Integer = &H1
    Private Const SERVICE_CONTROL_CONTINUE As Integer = &H3
    Private Const SERVICE_CONTROL_INTERROGATE As Integer = &H4
    Private Const SERVICE_CONTROL_SHUTDOWN As Integer = &H5
    Private Const SERVICE_CONTROL_PAUSE As Integer = &H2
    Private Const SERVICE_NO_CHANGE As Integer = &HFFFFFFFF
    Private Const SERVICE_QUERY_CONFIG As Integer = &H1
    Private Const SERVICE_CHANGE_CONFIG As Integer = &H2
    Private Const SERVICE_QUERY_STATUS As Integer = &H4
    Private Const SERVICE_ENUMERATE_DEPENDENTS As Integer = &H8
    Private Const SERVICE_START As Integer = &H10
    Private Const SERVICE_STOP As Integer = &H20
    Private Const SERVICE_PAUSE_CONTINUE As Integer = &H40
    Private Const SERVICE_INTERROGATE As Integer = &H80
    Private Const SERVICE_USER_DEFINED_CONTROL As Integer = &H100
    Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Private Const SERVICE_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED Or SERVICE_QUERY_CONFIG Or SERVICE_CHANGE_CONFIG Or SERVICE_QUERY_STATUS Or SERVICE_ENUMERATE_DEPENDENTS Or SERVICE_START Or SERVICE_STOP Or SERVICE_PAUSE_CONTINUE Or SERVICE_INTERROGATE Or SERVICE_USER_DEFINED_CONTROL)
    Private Const SC_MANAGER_CONNECT As Integer = &H1
    Private Const SC_MANAGER_CREATE_SERVICE As Integer = &H2
    ' Private Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4
    Private Const SC_MANAGER_LOCK As Integer = &H8
    Private Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
    Private Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
    Private Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

    ' ========================================
    ' Structures
    ' ========================================
    Public Structure SERVICE_STATUS
        Dim dwServiceType As Integer
        Dim dwCurrentState As Integer
        Dim dwControlsAccepted As Integer
        Dim dwWin32ExitCode As Integer
        Dim dwServiceSpecificExitCode As Integer
        Dim dwCheckPoint As Integer
        Dim dwWaitHint As Integer
    End Structure
#End Region

    ' ========================================
    ' Exposed enums
    ' ========================================
    Public Enum TypeServiceStartType As Integer
        SERVICE_BOOT_START = &H0
        SERVICE_SYSTEM_START = &H1
        SERVICE_AUTO_START = &H2
        SERVICE_DEMAND_START = &H3
        SERVICE_DISABLED = &H4
        SERVICESTARTTYPE_NO_CHANGE = SERVICE_NO_CHANGE
    End Enum

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _Name As String
    Private _LongName As String
    'Private _CanPauseAndContinue As Boolean
    'Private _CanShutdown As Boolean
    'Private _CanStop As Boolean
    Private _imagePath As String = vbNullString
    Private _description As String = vbNullString
    Private _diagnosticsMessageFile As String = vbNullString
    Private _group As String = vbNullString
    Private _objectName As String = vbNullString

    Public IsDisplayed As Boolean

    Private _dwServiceType As Integer
    Private _dwCurrentState As Integer
    Private _dwControlsAccepted As Integer
    'Private _dwWin32ExitCode As Integer
    'Private _dwServiceSpecificExitCode As Integer
    'Private _dwCheckPoint As Integer
    'Private _dwWaitHint As Integer
    Private _dwProcessId As Integer
    'Private _dwServiceFlags As Integer

    ' ========================================
    ' Getter & Setter
    ' ========================================
#Region "Getter & setter"
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property
    Public ReadOnly Property LongName() As String
        Get
            Return _LongName
        End Get
    End Property
    Public ReadOnly Property Status() As System.ServiceProcess.ServiceControllerStatus
        Get
            Dim oo As System.ServiceProcess.ServiceControllerStatus = _
                CType(_dwCurrentState, ServiceProcess.ServiceControllerStatus)
            Return oo
        End Get
    End Property
    Public ReadOnly Property Type() As String
        Get
            Select Case _dwServiceType
                Case ServiceProcess.ServiceType.Adapter
                    Return "Adapter"
                Case ServiceProcess.ServiceType.FileSystemDriver
                    Return "FileSystemDriver"
                Case ServiceProcess.ServiceType.InteractiveProcess
                    Return "InteractiveProcess"
                Case ServiceProcess.ServiceType.KernelDriver
                    Return "KernelDriver"
                Case ServiceProcess.ServiceType.RecognizerDriver
                    Return "RecognizerDriver"
                Case ServiceProcess.ServiceType.Win32OwnProcess
                    Return "Win32OwnProcess"
                Case ServiceProcess.ServiceType.Win32ShareProcess
                    Return "Win32ShareProcess"
                Case Else
                    Return "N/A"
            End Select
        End Get
    End Property
    'Public ReadOnly Property CanPauseAndContinue() As Boolean
    '    Get
    '        Return ((_dwControlsAccepted And 2) = 2)
    '    End Get
    'End Property
    'Public ReadOnly Property CanShutdown() As Boolean
    '    Get
    '        Return ((_dwControlsAccepted And 4) = 4)
    '    End Get
    'End Property
    'Public ReadOnly Property CanStop() As Boolean
    '    Get
    '        Return ((_dwControlsAccepted And 1) = 1)
    '    End Get
    'End Property
    Public ReadOnly Property ImagePath(Optional ByVal forceRefresh As Boolean = False) As String
        Get
            If _imagePath = vbNullString Or forceRefresh Then
                _imagePath = GetServiceInfo("ImagePath")
            End If

            Return _imagePath
        End Get
    End Property
    Public ReadOnly Property Description(Optional ByVal forceRefresh As Boolean = False) As String
        Get
            If _description = vbNullString Or forceRefresh Then
                _description = GetServiceInfo("Description")
            End If

            Return _description
        End Get
    End Property
    Public ReadOnly Property DiagnosticsMessageFile(Optional ByVal forceRefresh As Boolean = False) As String
        Get
            If _diagnosticsMessageFile = vbNullString Or forceRefresh Then
                _diagnosticsMessageFile = GetServiceInfo("DiagnosticsMessageFile")
            End If

            Return _diagnosticsMessageFile
        End Get
    End Property
    Public ReadOnly Property Group(Optional ByVal forceRefresh As Boolean = False) As String
        Get
            If _group = vbNullString Or forceRefresh Then
                _group = GetServiceInfo("Group")
            End If

            Return _group
        End Get
    End Property
    Public ReadOnly Property ObjectName(Optional ByVal forceRefresh As Boolean = False) As String
        Get
            If _objectName = vbNullString Or forceRefresh Then
                _objectName = GetServiceInfo("ObjectName")
            End If

            Return _objectName
        End Get
    End Property
    Public ReadOnly Property ServiceStartType() As String
        Get
            Select Case GetServiceInfo("Start")
                Case "0"  'SERVICE_BOOT_START
                    Return "Boot Start"
                Case "1"  'SERVICE_SYSTEM_START
                    Return "System Start"
                Case "2"  'SERVICE_AUTO_START
                    Return "Auto Start"
                Case "3"  'SERVICE_DEMAND_START
                    Return "Demand Start"
                Case "4"  'SERVICE_DISABLED
                    Return "Disabled"
                Case Else
                    Return vbNullString
            End Select
        End Get
    End Property
    Public ReadOnly Property ProcessID() As Integer
        Get
            Return _dwProcessId
        End Get
    End Property
#End Region

    ' ========================================
    ' Constructor
    ' ========================================
    Public Sub New(ByVal name As String, ByVal dispName As String, ByVal serv As SERVICE_STATUS_PROCESS)
        MyBase.New()
        _dwServiceType = serv.dwServiceType
        _dwCurrentState = serv.dwCurrentState
        '_dwControlsAccepted = serv.dwControlsAccepted
        '_dwWin32ExitCode = serv.dwWin32ExitCode
        '_dwServiceSpecificExitCode = serv.dwServiceSpecificExitCode
        '_dwCheckPoint = serv.dwCheckPoint
        '_dwWaitHint = serv.dwWaitHint
        _dwProcessId = serv.dwProcessId
        '_dwServiceFlags = serv.dwServiceFlags
        _Name = name
        _LongName = dispName
        IsDisplayed = False
    End Sub


    ' ========================================
    ' Public functions
    ' ========================================

    ' Refresh informations
    Public Sub Refresh()

        'TOCHANGE --> BAD WAY TO RETRIEVE INFOS
        Dim o As System.ServiceProcess.ServiceController() = System.ServiceProcess.ServiceController.GetServices()
        Dim o1 As System.ServiceProcess.ServiceController

        For Each o1 In o
            If o1.ServiceName = _Name Then
                _Name = o1.ServiceName
                _LongName = o1.DisplayName
                _dwServiceType = o1.ServiceType
                _dwCurrentState = o1.Status
                Exit Sub
            End If
        Next
    End Sub

    ' Start a service
    Public Sub StartService()
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                apiStartService(lServ, 0, 0)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If

    End Sub

    ' Pause a service
    Public Sub PauseService()
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._PAUSE, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Resume a service
    Public Sub ResumeService()
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._CONTINUE, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Stop a service
    Public Sub StopService()
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._STOP, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Shutdown a service
    Public Sub ShutDownService()
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._SHUTDOWN, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Set service start type
    Public Sub SetServiceStartType(ByVal type As TypeServiceStartType)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim hLockSCManager As Integer

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        hLockSCManager = LockServiceDatabase(CInt(hSCManager))

        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                Dim ret As Boolean = ChangeServiceConfig(CInt(lServ), ServiceType.SERVICETYPE_NO_CHANGE, type, _
                    ServiceErrorControl.SERVICEERRORCONTROL_NO_CHANGE, vbNullString, vbNullString, Nothing, _
                    vbNullString, vbNullString, vbNullString, vbNullString)
                CloseServiceHandle(lServ)
            End If
            UnlockServiceDatabase(hLockSCManager)
            CloseServiceHandle(hSCManager)
        End If

    End Sub





    ' ========================================
    ' Shared functions
    ' ========================================

    ' Enumetare all services
    Public Shared Function EnumServicesEx(ByRef p() As cService) As Integer
        Dim hSCM As IntPtr
        Dim lR As Integer
        Dim lBytesNeeded As Integer
        Dim lServicesReturned As Integer
        Dim tServiceStatus() As ENUM_SERVICE_STATUS_PROCESS
        ReDim tServiceStatus(0)
        Dim lStructsNeeded As Integer
        Dim lServiceStatusInfoBuffer As Integer
        Dim idx As Integer

        ReDim p(0)

        hSCM = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ENUMERATE_SERVICE)

        If Not (hSCM = IntPtr.Zero) Then
            lR = EnumServicesStatusEx(hSCM, _
                                      SC_ENUM_PROCESS_INFO, _
                                      SERVICE_ALL, _
                                      SERVICE_STATE_ALL, _
                                      Nothing, _
                                      0, _
                                      lBytesNeeded, _
                                      lServicesReturned, _
                                      0, _
                                      vbNullString)

            If (lR = 0 And Err.LastDllError = ERROR_MORE_DATA) Then

                lStructsNeeded = CInt(lBytesNeeded / Marshal.SizeOf(tServiceStatus(0)) + 1)
                ReDim tServiceStatus(lStructsNeeded - 1)
                lServiceStatusInfoBuffer = lStructsNeeded * (Marshal.SizeOf(tServiceStatus(0)))

                lR = EnumServicesStatusEx(hSCM, _
                                          SC_ENUM_PROCESS_INFO, _
                                          SERVICE_ALL, _
                                          SERVICE_STATE_ALL, _
                                          tServiceStatus(0), _
                                          lServiceStatusInfoBuffer, _
                                          lBytesNeeded, _
                                          lServicesReturned, _
                                          0, _
                                          vbNullString)

                If Not (lR = 0) Then
                    ReDim p(lServicesReturned - 1)
                    For idx = 0 To lServicesReturned - 1
                        p(idx) = New cService(GetStrFromPtrA(tServiceStatus(idx).lpServiceName), _
                            GetStrFromPtrA(tServiceStatus(idx).lpDisplayName), _
                            tServiceStatus(idx).ServiceStatus)
                    Next idx
                End If
            End If

            CloseServiceHandle(hSCM)

            ' Add others services from registry
            Call EnumerateFromReg(p)
        End If
    End Function

    ' Retrieve file name from a path with arguments
    Public Shared Function GetFileNameFromSpecial(ByVal path As String) As String
        Dim s As String = path
        Dim i As Integer = 0

        ' Get lower case
        s.ToLowerInvariant()

        ' Get the left part of the exe (look for ".exe" substring)
        i = InStr(s, ".exe", CompareMethod.Binary)

        If i > 0 Then
            Return s.Substring(0, i + 3)
        Else
            Return path
        End If

    End Function

    Public Shared Sub StopService(ByVal serviceName As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, serviceName, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._STOP, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================

    Private Shared Function GetStrFromPtrA(ByVal lpszA As Integer) As String
        Dim s As String = Space(lstrlenA(lpszA))
        Call lstrcpyA(s, lpszA)
        Return s
    End Function

    ' Enumerate services installed on computer from registry
    Private Shared Function EnumerateFromReg(ByRef p() As cService) As Integer

        Dim key As Microsoft.Win32.RegistryKey = _
            My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services")

        Dim _s() As String = key.GetSubKeyNames

        For Each k As String In _s

            Dim present As Boolean = False
            For Each c As cService In p
                If c.Name = k Then
                    present = True
                    Exit For
                End If
            Next

            If Not (present) Then
                ReDim Preserve p(p.Length)
                Dim KK As cService.SERVICE_STATUS_PROCESS
                With KK
                    .dwCurrentState = 1
                    .dwServiceType = CInt(Val(GetServiceInfo("Type", k)))
                    .dwProcessId = 0
                End With
                p(p.Length - 1) = New cService(k, GetServiceInfo("DisplayName", k), KK)
            End If

        Next

        Return p.Length

    End Function

    ' Retrieve information about a service from registry
    Private Shared Function GetServiceInfo(ByVal info As String, ByVal servName As String) As String
        Try
            Return CStr(My.Computer.Registry.GetValue( _
                        "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & servName, _
                        info, ""))
        Catch ex As Exception
            Return vbNullString
        End Try
    End Function

    ' Retrieve information about a service from registry
    Private Function GetServiceInfo(ByVal info As String) As String
        Try
            Return CStr(My.Computer.Registry.GetValue( _
                        "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & _Name, _
                        info, ""))
        Catch ex As Exception
            Return vbNullString
        End Try
    End Function

End Class
