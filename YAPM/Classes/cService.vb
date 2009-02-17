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

Imports System.Runtime.InteropServices

Public Class cService

#Region "API"
    ' ========================================
    ' API declarations
    ' ========================================
    Private Declare Function apiStartService Lib "advapi32.dll" Alias "StartServiceA" (ByVal hService As IntPtr, ByVal dwNumServiceArgs As Integer, ByVal lpServiceArgVectors As Integer) As Integer
    Private Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As IntPtr

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure QUERY_SERVICE_CONFIG
        Public dwServiceType As Integer
        Public dwStartType As Integer
        Public dwErrorControl As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public lpBinaryPathName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpLoadOrderGroup As String
        Public dwTagId As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public lpDependencies As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpServiceStartName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpDisplayName As String
    End Structure

    <DllImport("advapi32.dll", EntryPoint:="QueryServiceConfigW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function _
        QueryServiceConfig(ByVal hService As IntPtr, _
        ByVal pBuffer As IntPtr, _
        ByVal cbBufSize As Integer, _
        ByRef pcbBytesNeeded As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", EntryPoint:="QueryServiceConfig2W", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function _
        QueryServiceConfig2(ByVal hService As Integer, _
        ByVal dwInfoLevel As Integer, _
        ByVal lpBuffer As IntPtr, _
        ByVal cbBufSize As Integer, _
        ByRef pcbBytesNeeded As Integer) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SERVICE_DESCRIPTION
        Public lpDescription As String
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SERVICE_FAILURE_ACTIONS
        Public dwResetPeriod As Integer
        Public lpRebootMsg As String
        Public lpCommand As String
        Public cActions As Integer
        Public lpsaActions As Integer
    End Structure

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

    Public Structure ENUM_SERVICE_STATUS_PROCESS
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
    Private _imagePath As String = vbNullString
    Private _description As String = vbNullString
    Private _diagnosticsMessageFile As String = vbNullString
    Private _objectName As String = vbNullString
    Private _dwServiceType As Integer
    Private _dwCurrentState As Integer
    Private _dwControlsAccepted As Integer
    Private _dwErrorControl As Integer
    Private _dwStartType As Integer
    Private _lpLoadOrderGroup As String
    Private _lpServiceStartName As String
    Private _dwProcessId As Integer
    Private _processName As String
    Private _command As String
    Private _rebootMessage As String

    Private gotDesc As Boolean = False
    Private gotDiag As Boolean = False
    Private gotObj As Boolean = False

    Private lServ As IntPtr
    Private Shared hSCManager As IntPtr

    Public IsDisplayed As Boolean


    ' ========================================
    ' Getter & Setter
    ' ========================================
#Region "Getter & setter"

    ' No need to refresh OR refresh by Refresh() proc
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
    Public ReadOnly Property ImagePath() As String
        Get
            Return _imagePath
        End Get
    End Property
    Public ReadOnly Property ErrorControl() As Integer
        Get
            Return _dwErrorControl
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
    Public ReadOnly Property ProcessName() As String
        Get
            Return _processName
        End Get
    End Property
    Public ReadOnly Property LoadOrderGroup() As String
        Get
            Return _lpLoadOrderGroup
        End Get
    End Property
    Public ReadOnly Property ServiceStartName() As String
        Get
            Return _lpServiceStartName
        End Get
    End Property

    ' ???
    Public ReadOnly Property State() As System.ServiceProcess.ServiceControllerStatus
        Get
            Dim oo As System.ServiceProcess.ServiceControllerStatus = _
                CType(_dwCurrentState, ServiceProcess.ServiceControllerStatus)
            Return oo
        End Get
    End Property

    ' Constructor refresh
    Public ReadOnly Property Description() As String
        Get
            If Not (gotDesc) Then
                gotDesc = True
                _description = GetServiceInfo("Description")
            End If

            Return _description
        End Get
    End Property
    Public ReadOnly Property DiagnosticsMessageFile() As String
        Get
            If Not (gotDiag) Then
                gotDiag = True
                _diagnosticsMessageFile = GetServiceInfo("DiagnosticMessageFile")
            End If

            Return _diagnosticsMessageFile
        End Get
    End Property
    Public ReadOnly Property ObjectName() As String
        Get
            If Not (gotObj) Then
                gotObj = True
                _objectName = GetServiceInfo("ObjectName")
            End If

            Return _objectName
        End Get
    End Property
    'Public ReadOnly Property RebootMessage() As String
    '    Get
    '        Return _rebootMessage
    '    End Get
    'End Property
    'Public ReadOnly Property Command() As String
    '    Get
    '        Return _command
    '    End Get
    'End Property
#End Region


    ' ========================================
    ' Constructor
    ' ========================================
    Public Sub New(ByVal SCMgr As IntPtr, ByVal name As String, ByVal dispName As String, _
        ByVal serv As SERVICE_STATUS_PROCESS, Optional ByVal noRefresh As Boolean = False)

        MyBase.New()

        ' Get param infos
        _dwServiceType = serv.dwServiceType
        _dwCurrentState = serv.dwCurrentState
        _dwProcessId = serv.dwProcessId
        _Name = name
        _processName = cProcess.GetProcessName(_dwProcessId)
        _LongName = dispName
        hSCManager = SCMgr

        lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)

        ' Refresh some informations
        'If noRefresh = False Then Call Refresh2()

        IsDisplayed = False
    End Sub
    Public Sub New(ByVal name As String)
        MyBase.New()

        ' Get param infos
        _Name = name
        IsDisplayed = False
    End Sub

    Protected Overrides Sub Finalize()
        CloseServiceHandle(lServ)
    End Sub



    ' ========================================
    ' Public functions
    ' ========================================

    ' Get desired information
    Public Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Name"
                res = Me.Name
            Case "DisplayName"
                res = _LongName
            Case "ServiceType"
                res = Me.Type
            Case "ImagePath"
                res = _imagePath
            Case "ErrorControl"
                res = CStr(_dwErrorControl)
            Case "StartType"
                res = Me.ServiceStartType
            Case "ProcessId"
                If _dwProcessId > 0 Then
                    res = CStr(_dwProcessId)
                Else
                    res = ""
                End If
            Case "ProcessName"
                If _dwProcessId > 0 Then
                    res = _processName
                Else
                    res = ""
                End If
            Case "LoadOrderGroup"
                res = Me.LoadOrderGroup
            Case "ServiceStartName"
                res = _lpServiceStartName
            Case "State"
                res = CStr(_dwCurrentState)
            Case "Description"
                res = Me.Description
            Case "DiagnosticMessageFile"
                res = Me.DiagnosticsMessageFile
            Case "ObjectName"
                res = _rebootMessage
                'Case "RebootMessage"
                '    res = _rebootMessage
                'Case "Command"
                '    res = _command
            Case "Process"
                If _dwProcessId > 0 Then
                    res = _processName & " -- " & CStr(_dwProcessId)
                Else
                    res = ""
                End If
        End Select

        Return res
    End Function

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(13) As String

        s(0) = "DisplayName"
        s(1) = "State"
        s(2) = "StartType"
        s(3) = "ImagePath"
        s(4) = "ServiceType"
        s(5) = "Description"
        s(6) = "ErrorControl"
        s(7) = "Process"
        s(8) = "ProcessId"
        s(9) = "ProcessName"
        s(10) = "LoadOrderGroup"
        s(11) = "ServiceStartName"
        s(12) = "DiagnosticMessageFile"
        s(13) = "ObjectName"
        's(15) = "RebootMessage"
        's(16) = "Command"

        Return s
    End Function

    ' Refresh infos
    Public Sub Refresh()

        Dim fResult As Boolean

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then

                ' Get all available informations
                Dim tt As QUERY_SERVICE_CONFIG = Nothing
                Dim bufSize As Integer = Marshal.SizeOf(tt)
                Dim bytesNeeded As Integer = 0

                Dim pt As IntPtr = IntPtr.Zero
                fResult = QueryServiceConfig(lServ, pt, bytesNeeded, bytesNeeded)
                pt = Marshal.AllocHGlobal(bytesNeeded)
                fResult = QueryServiceConfig(lServ, pt, bytesNeeded, bytesNeeded)
                tt = CType(Marshal.PtrToStructure(pt, GetType(QUERY_SERVICE_CONFIG)), QUERY_SERVICE_CONFIG)

                With tt
                    _dwServiceType = .dwServiceType
                    _dwErrorControl = .dwErrorControl
                    _dwStartType = .dwStartType
                    _imagePath = .lpBinaryPathName
                    _LongName = .lpDisplayName
                    _lpLoadOrderGroup = .lpLoadOrderGroup
                    _lpServiceStartName = .lpServiceStartName
                End With

                '_dwCurrentState = ???????

                Marshal.FreeHGlobal(pt)

            End If
        End If

    End Sub

    ' Refresh some onther informations
    'Public Sub Refresh2()

    '    ' Here we retrieve these infos :
    '    '- Description
    '    '- DiagnosticMessageFile
    '    '- ObjectName
    '    '- RebootMessage
    '    '- Command

    '    Dim hSCManager As IntPtr
    '    Dim lServ As IntPtr

    '    hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
    '    If hSCManager <> IntPtr.Zero Then
    '        lServ = OpenService(hSCManager, _Name, SERVICE_ALL_ACCESS)
    '        If lServ <> IntPtr.Zero Then

    '            ' Get informations
    '            Dim tt As SERVICE_FAILURE_ACTIONS = Nothing
    '            Dim bytesNeeded As Integer = 0
    '            Dim pt As IntPtr = IntPtr.Zero
    '            Dim fRes As Integer = QueryServiceConfig2(CInt(lServ), 2, IntPtr.Zero, 0, bytesNeeded)
    '            pt = Marshal.AllocHGlobal(bytesNeeded)
    '            fRes = QueryServiceConfig2(CInt(lServ), 2, pt, bytesNeeded, bytesNeeded)
    '            tt = CType(Marshal.PtrToStructure(pt, GetType(SERVICE_FAILURE_ACTIONS)), SERVICE_FAILURE_ACTIONS)
    '            Marshal.FreeHGlobal(pt)

    '            'Dim tt2 As SERVICE_DESCRIPTION = Nothing
    '            'fRes = QueryServiceConfig2(CInt(lServ), 1, IntPtr.Zero, 0, bytesNeeded)
    '            'pt = Marshal.AllocHGlobal(bytesNeeded)
    '            'fRes = QueryServiceConfig2(CInt(lServ), 1, pt, bytesNeeded, bytesNeeded)
    '            ''Dim dest() As Short
    '            ''ReDim dest(bytesNeeded)
    '            ''Marshal.Copy(pt, dest, 0, bytesNeeded)
    '            ''marshal.Copy(dest,
    '            '_description = "test"
    '            'Marshal.FreeHGlobal(pt)
    '            _description = GetServiceInfo("Description")

    '            With tt
    '                _command = .lpCommand
    '                _rebootMessage = .lpRebootMsg
    '            End With

    '            CloseServiceHandle(lServ)
    '        End If
    '        CloseServiceHandle(hSCManager)
    '    End If

    '    ' Have to get some informations from registry
    '    _objectName = GetServiceInfo("ObjectName")
    '    _diagnosticsMessageFile = GetServiceInfo("DiagnosticMessageFile")

    'End Sub

    ' Start a service
    Public Sub StartService()

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then
                apiStartService(lServ, 0, 0)
            End If
        End If

    End Sub

    ' Pause a service
    Public Sub PauseService()
        Dim lpSS As SERVICE_STATUS

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._PAUSE, lpSS)
            End If
        End If
    End Sub

    ' Resume a service
    Public Sub ResumeService()
        Dim lpSS As SERVICE_STATUS

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._CONTINUE, lpSS)
            End If
        End If
    End Sub

    ' Stop a service
    Public Sub StopService()
        Dim lpSS As SERVICE_STATUS

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._STOP, lpSS)
            End If
        End If
    End Sub

    ' Shutdown a service
    Public Sub ShutDownService()
        Dim lpSS As SERVICE_STATUS

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._SHUTDOWN, lpSS)
            End If
        End If
    End Sub

    ' Set service start type
    Public Sub SetServiceStartType(ByVal type As TypeServiceStartType)
        Dim hLockSCManager As Integer

        hLockSCManager = LockServiceDatabase(CInt(hSCManager))

        If hSCManager <> IntPtr.Zero Then
            If lServ <> IntPtr.Zero Then
                Dim ret As Boolean = ChangeServiceConfig(CInt(lServ), ServiceType.SERVICETYPE_NO_CHANGE, type, _
                    ServiceErrorControl.SERVICEERRORCONTROL_NO_CHANGE, vbNullString, vbNullString, Nothing, _
                    vbNullString, vbNullString, vbNullString, vbNullString)
            End If
            UnlockServiceDatabase(hLockSCManager)
        End If

    End Sub





    ' ========================================
    ' Shared functions
    ' ========================================

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

    ' Stop a service
    Public Shared Sub StopService(ByVal serviceName As String)
        Dim lServ0 As IntPtr
        Dim lpSS As SERVICE_STATUS

        If hSCManager <> IntPtr.Zero Then
            lServ0 = OpenService(hSCManager, serviceName, SERVICE_ALL_ACCESS)
            If lServ0 <> IntPtr.Zero Then
                ControlService(lServ0, SERVICE_CONTROL._STOP, lpSS)
                CloseServiceHandle(lServ0)
            End If
        End If
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================

    ' Retrieve information about a service from registry
    Private Function GetServiceInfo(ByVal info As String) As String
        Try
            Return CStr(My.Computer.Registry.GetValue( _
                        "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & _Name, _
                        info, ""))
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class
