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
    Private Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4
    Private Const SC_MANAGER_LOCK As Integer = &H8
    Private Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
    Private Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
    Private Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

    ' ========================================
    ' Structures
    ' ========================================
    Private Structure SERVICE_STATUS
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
    Private _Status As System.ServiceProcess.ServiceControllerStatus
    Private _CanPauseAndContinue As Boolean
    Private _CanShutdown As Boolean
    Private _CanStop As Boolean
    Private _imagePath As String = vbNullString
    Private _description As String = vbNullString
    Private _diagnosticsMessageFile As String = vbNullString
    Private _group As String = vbNullString
    Private _objectName As String = vbNullString

    Public IsDisplayed As Boolean

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
            Return _Status
        End Get
    End Property
    Public ReadOnly Property CanPauseAndContinue() As Boolean
        Get
            Return _CanPauseAndContinue
        End Get
    End Property
    Public ReadOnly Property CanShutdown() As Boolean
        Get
            Return _CanShutdown
        End Get
    End Property
    Public ReadOnly Property CanStop() As Boolean
        Get
            Return _CanStop
        End Get
    End Property
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
#End Region

    ' ========================================
    ' Constructor
    ' ========================================
    Public Sub New(ByVal name As String, ByVal longName As String, ByVal status As _
        System.ServiceProcess.ServiceControllerStatus, ByVal canPause As Boolean, ByVal canShutdown As Boolean, _
        ByVal canStop As Boolean)

        MyBase.New()
        _Name = name
        _LongName = longName
        _Status = status
        _CanPauseAndContinue = canPause
        _CanShutdown = canShutdown
        _CanStop = canStop
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
                _Status = o1.Status
                _CanPauseAndContinue = o1.CanPauseAndContinue
                _CanShutdown = o1.CanShutdown
                _CanStop = o1.CanStop
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

    ' Enumerate services installed on computer
    Public Shared Function Enumerate(ByRef p() As cService) As Integer

        ' NOT GOOD PIECE OF CODE
        Dim key As Microsoft.Win32.RegistryKey = _
            My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services")

        Dim _s() As String = key.GetSubKeyNames
        Dim o As System.ServiceProcess.ServiceController() = System.ServiceProcess.ServiceController.GetServices()

        Dim x As Integer

        ReDim p(_s.Length - 1)
        x = 0
        Dim s As String
        For Each s In _s
            Dim _long As String = ""
            Dim _status As System.ServiceProcess.ServiceControllerStatus
            Dim _pause As Boolean = False
            Dim _shut As Boolean = False
            Dim _stop As Boolean = False

            ' Try to get informations from ServiceController
            Dim o1 As System.ServiceProcess.ServiceController
            For Each o1 In o
                If o1.ServiceName = s Then
                    _long = o1.DisplayName
                    _status = o1.Status
                    _pause = o1.CanPauseAndContinue
                    _shut = o1.CanShutdown
                    _stop = o1.CanStop
                    Exit For
                End If
            Next

            p(x) = New cService(s, _long, _status, _pause, _shut, _stop)
            x += 1
        Next

        Return x

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
