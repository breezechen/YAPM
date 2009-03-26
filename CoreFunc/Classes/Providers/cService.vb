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
Imports System.Diagnostics

Public MustInherit Class cService
    Inherits cGeneralObject

    Private Const SERVICE_NO_CHANGE As Integer = &HFFFFFFFF
    Public Enum TypeServiceStartType As Integer
        SERVICE_BOOT_START = &H0
        SERVICE_SYSTEM_START = &H1
        SERVICE_AUTO_START = &H2
        SERVICE_DEMAND_START = &H3
        SERVICE_DISABLED = &H4
        SERVICESTARTTYPE_NO_CHANGE = SERVICE_NO_CHANGE
    End Enum
    Public Enum ACCEPTED_CONTROLS As Integer
        SERVICE_ACCEPT_NETBINDCHANGE = &H10
        SERVICE_ACCEPT_PARAMCHANGE = &H8
        SERVICE_ACCEPT_PAUSE_CONTINUE = &H2
        SERVICE_ACCEPT_PRESHUTDOWN = &H100
        SERVICE_ACCEPT_SHUTDOWN = &H4
        SERVICE_ACCEPT_STOP = &H1
    End Enum
    Public Enum SERVIVE_STATE As Integer
        ContinuePending = &H5
        PausePending = &H6
        Paused = &H7
        Running = &H4
        StartPending = &H2
        StopPending = &H3
        Stopped = &H1
    End Enum

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
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ENUM_SERVICE_STATUS_PROCESS
        <MarshalAs(UnmanagedType.LPTStr)> Public ServiceName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public DisplayName As String
        <MarshalAs(UnmanagedType.Struct)> Public ServiceStatusProcess As SERVICE_STATUS_PROCESS
    End Structure
    Public Structure LightService
        Public processId As Integer
        Public state As Integer
        Public name As String
        Public Sub New(ByVal _name As String, ByVal _state As Integer, ByVal _procId As Integer)
            name = _name
            state = _state
            processId = _procId
        End Sub
        Public Sub SetAsChanged()
            processId = -2
        End Sub
    End Structure

    ' ========================================
    ' Getter & Setter
    ' ========================================
    Public MustOverride ReadOnly Property HasChanged(ByVal _serv As LightService) As Boolean
    Public MustOverride ReadOnly Property Key() As String
    Public MustOverride ReadOnly Property Name() As String
    Public MustOverride ReadOnly Property LongName() As String
    Public MustOverride ReadOnly Property Type() As String
    Public MustOverride ReadOnly Property ImagePath() As String
    Public MustOverride ReadOnly Property ErrorControl() As Integer
    Public MustOverride ReadOnly Property ProcessID() As Integer
    Public MustOverride ReadOnly Property ProcessName() As String
    Public MustOverride ReadOnly Property LoadOrderGroup() As String
    Public MustOverride ReadOnly Property ServiceStartName() As String
    Public MustOverride ReadOnly Property AcceptedControls() As ACCEPTED_CONTROLS
    Public MustOverride ReadOnly Property State() As SERVIVE_STATE
    Public MustOverride ReadOnly Property ServiceStartTypeInt() As TypeServiceStartType
    Public MustOverride ReadOnly Property ServiceStartType() As String
    Public MustOverride ReadOnly Property Description() As String
    Public MustOverride ReadOnly Property DiagnosticsMessageFile() As String
    Public MustOverride ReadOnly Property ObjectName() As String



    ' ========================================
    ' Public functions
    ' ========================================

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

        Return s
    End Function

    ' Refresh infos
    Public MustOverride Sub Refresh()

    ' Start a service
    Public MustOverride Sub StartService()

    ' Pause a service
    Public MustOverride Sub PauseService()

    ' Resume a service
    Public MustOverride Sub ResumeService()

    ' Stop a service
    Public MustOverride Sub StopService()

    ' Shutdown a service
    Public MustOverride Sub ShutDownService()

    ' Set service start type
    Public MustOverride Sub SetServiceStartType(ByVal type As TypeServiceStartType)


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

End Class
