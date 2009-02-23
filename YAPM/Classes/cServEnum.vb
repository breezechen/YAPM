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

Public Class cServEnum

#Region "API"

    ' ========================================
    ' API declarations
    ' ========================================
    Private Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As IntPtr

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Private Shared Function CloseServiceHandle(ByVal serviceHandle As IntPtr) As Boolean
    End Function

    Private Declare Function EnumServicesStatusEx Lib "advapi32.dll" Alias "EnumServicesStatusExA" (ByVal hSCManager As IntPtr, ByVal InfoLevel As Integer, ByVal dwServiceType As Integer, ByVal dwServiceState As Integer, ByRef lpServices As cService.ENUM_SERVICE_STATUS_PROCESS, ByVal cbBufSize As Integer, ByRef pcbBytesNeeded As Integer, ByRef lpServicesReturned As Integer, ByRef lpResumeHandle As Integer, ByRef pszGroupName As String) As Integer
    Private Declare Function lstrlenA Lib "kernel32" (ByVal Ptr As Integer) As Integer
    Private Declare Function lstrcpyA Lib "kernel32" (ByVal RetVal As String, ByVal Ptr As Integer) As Integer


    ' ========================================
    ' Constants
    ' ========================================
    Private Const ERROR_MORE_DATA As Integer = 234
    Private Const SC_ENUM_PROCESS_INFO As Integer = &H0
    Private Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4

    Private Const SERVICE_ACTIVE As Integer = &H1
    Private Const SERVICE_INACTIVE As Integer = &H2
    Private Const SERVICE_STATE_ALL As Integer = (SERVICE_ACTIVE Or SERVICE_INACTIVE)

    Private Const SERVICE_WIN32_OWN_PROCESS As Integer = &H10
    Private Const SERVICE_WIN32_SHARE_PROCESS As Integer = &H20
    Private Const SERVICE_WIN32 As Integer = SERVICE_WIN32_OWN_PROCESS + SERVICE_WIN32_SHARE_PROCESS

    Private Const SERVICE_DRIVER As Integer = &HB
    Private Const SERVICE_INTERACTIVE_PROCESS As Integer = &H100
    Private Const SERVICE_ALL As Integer = SERVICE_DRIVER Or SERVICE_WIN32_OWN_PROCESS Or _
            SERVICE_WIN32_SHARE_PROCESS Or SERVICE_WIN32 Or SERVICE_INTERACTIVE_PROCESS

    Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Private Const SC_MANAGER_CONNECT As Integer = &H1
    Private Const SC_MANAGER_CREATE_SERVICE As Integer = &H2
    Private Const SC_MANAGER_LOCK As Integer = &H8
    Private Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
    Private Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
    Private Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

#End Region


    ' ========================================
    ' Private
    ' ========================================
    Private hSCM As IntPtr


    ' ========================================
    ' Public
    ' ========================================

    Public ReadOnly Property SCManagerHandle() As IntPtr
        Get
            Return hSCM
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        ' Get a handle
        hSCM = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ENUMERATE_SERVICE)
    End Sub

    Protected Overrides Sub Finalize()
        ' Close our handle
        MyBase.Finalize()
        Call CloseServiceHandle(hSCM)
    End Sub

    ' Enumerate services installed on computer from registry
    Public Function Enumerate(ByRef p() As String) As Integer

        Dim key As Microsoft.Win32.RegistryKey = _
            My.Computer.Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services")

        p = key.GetSubKeyNames

        Return p.Length

    End Function

    ' Enumetare all services
    Public Function EnumerateApi(ByRef p() As String) As Integer

        Dim lR As Integer
        Dim lBytesNeeded As Integer
        Dim lServicesReturned As Integer
        Dim tServiceStatus() As cService.ENUM_SERVICE_STATUS_PROCESS
        ReDim tServiceStatus(0)
        Dim lStructsNeeded As Integer
        Dim lServiceStatusInfoBuffer As Integer

        ReDim p(0)

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
                    For idx As Integer = 0 To lServicesReturned - 1
                        p(idx) = GetStrFromPtrA(tServiceStatus(idx).lpServiceName) _
                            & "@" & _
                        tServiceStatus(idx).ServiceStatus.dwProcessId.ToString
                    Next idx
                End If
            End If

        End If
    End Function

    ' Enumerate services for only one process
    Public Function EnumerateApi(ByVal pid As Integer, ByRef p() As String) As Integer

        Dim lR As Integer
        Dim lBytesNeeded As Integer
        Dim lServicesReturned As Integer
        Dim tServiceStatus() As cService.ENUM_SERVICE_STATUS_PROCESS
        ReDim tServiceStatus(0)
        Dim lStructsNeeded As Integer
        Dim lServiceStatusInfoBuffer As Integer

        ReDim p(0)

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
                    Dim k As Integer = 0
                    For idx As Integer = 0 To lServicesReturned - 1
                        If pid = tServiceStatus(idx).ServiceStatus.dwProcessId Then
                            p(k) = GetStrFromPtrA(tServiceStatus(idx).lpServiceName) _
                                & "@" & _
                            tServiceStatus(idx).ServiceStatus.dwProcessId.ToString
                            k += 1
                        End If
                    Next idx
                    ReDim Preserve p(k - 1)
                End If
            End If

        End If
    End Function



    ' ========================================
    ' Private
    ' ========================================

    ' Retrieve information about a service from registry
    Private Shared Function GetServiceInfo(ByVal info As String, ByVal servName As String) As String
        Try
            Return CStr(My.Computer.Registry.GetValue( _
                        "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & servName, _
                        info, ""))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Shared Function GetStrFromPtrA(ByVal lpszA As Integer) As String
        Dim s As String = Space(lstrlenA(lpszA))
        Call lstrcpyA(s, lpszA)
        Return s
    End Function

End Class
