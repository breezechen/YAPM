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

Public Class cPrivileges


    ' ========================================
    ' API declarations
    ' ========================================

#Region "API"

    Private Const TOKEN_QUERY As Integer = &H8
    Private Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20

    Private Const SE_PRIVILEGE_ENABLED As Integer = &H2
    Private Const SE_PRIVILEGE_ENABLED_BY_DEFAULT As Integer = &H1
    Private Const SE_PRIVILEGE_DISBALED As Integer = &H0
    Private Const SE_PRIVILEGE_REMOVED As Integer = &H4

    Public Enum PrivilegeStatus
        PRIVILEGE_ENABLED = &H2
        PRIVILEGE_DISBALED = &H0
        PRIVILEGE_REMOVED = &H4
    End Enum

    Private Const SE_AUDIT_NAME As String = "SeAuditPrivilege"
    Private Const SE_BACKUP_NAME As String = "SeBackupPrivilege"
    Private Const SE_CHANGE_NOTIFY_NAME As String = "SeChangeNotifyPrivilege"
    Private Const SE_CREATE_PAGEFILE_NAME As String = "SeCreatePagefilePrivilege"
    Private Const SE_CREATE_PERMANENT_NAME As String = "SeCreatePermanentPrivilege"
    Private Const SE_CREATE_TOKEN_NAME As String = "SeCreateTokenPrivilege"
    Private Const SE_DEBUG_NAME As String = "SeDebugPrivilege"
    Private Const SE_REMOTE_SHUTDOWN_NAME As String = "SeRemoteShutdownPrivilege"
    Private Const SE_PROF_SINGLE_PROCESS_NAME As String = "SeProfileSingleProcessPrivilege"
    Private Const SE_RESTORE_NAME As String = "SeRestorePrivilege"
    Private Const SE_SECURITY_NAME As String = "SeSecurityPrivilege"
    Private Const SE_SHUTDOWN_NAME As String = "SeShutdownPrivilege"
    Private Const SE_SYSTEM_ENVIRONMENT_NAME As String = "SeSystemEnvironmentPrivilege"
    Private Const SE_SYSTEM_PROFILE_NAME As String = "SeSystemProfilePrivilege"
    Private Const SE_SYSTEMTIME_NAME As String = "SeSystemtimePrivilege"
    Private Const SE_TAKE_OWNERSHIP_NAME As String = "SeTakeOwnershipPrivilege"
    Private Const SE_TCB_NAME As String = "SeTcbPrivilege"
    Private Const SE_MANAGE_VOLUME_NAME As String = "SeManageVolumePrivilege"
    Private Const SE_INC_BASE_PRIORITY_NAME As String = "SeIncreaseBasePriorityPrivilege"
    Private Const SE_INCREASE_QUOTA_NAME As String = "SeIncreaseQuotaPrivilege"
    Private Const SE_LOCK_MEMORY_NAME As String = "SeLockMemoryPrivilege"
    Private Const SE_LOAD_DRIVER_NAME As String = "SeLoadDriverPrivilege"
    Private Const SE_MACHINE_ACCOUNT_NAME As String = "SeMachineAccountPrivilege"

    Public Structure LUID
        Dim lowpart As Integer
        Dim highpart As Integer
    End Structure

    Public Structure LUID_AND_ATTRIBUTES
        Dim pLuid As LUID
        Dim Attributes As Integer
    End Structure

    Public Structure PrivilegeInfo
        Dim Name As String
        Dim Status As Integer
        Dim pLuid As LUID
    End Structure

    'structure de privilèges de token
    Public Structure TOKEN_PRIVILEGES2
        Dim PrivilegeCount As Integer
        Dim Privileges As LUID_AND_ATTRIBUTES
    End Structure

    Public Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        '<VBFixedArray(25)> _
        Dim Privileges() As LUID_AND_ATTRIBUTES
    End Structure

    Private Const SYNCHRONIZE As Integer = &H100000
    Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Private Const PROCESS_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED Or SYNCHRONIZE Or &HFFF)
    Private Const PROCESS_QUERY_INFORMATION As Integer = &H400

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Shared Function AdjustTokenPrivileges( _
        ByVal TokenHandle As Integer, _
        ByVal DisableAllPrivileges As Integer, _
        ByRef NewState As TOKEN_PRIVILEGES, _
        ByVal BufferLength As Integer, _
        ByRef PreviousState As TOKEN_PRIVILEGES, _
        ByRef ReturnLength As Integer) As Boolean
    End Function
    <DllImport("advapi32.dll", SetLastError:=True)> _
    Shared Function AdjustTokenPrivileges( _
    ByVal TokenHandle As Integer, _
    ByVal DisableAllPrivileges As Integer, _
    ByRef NewState As TOKEN_PRIVILEGES2, _
    ByVal BufferLength As Integer, _
    ByRef PreviousState As TOKEN_PRIVILEGES2, _
    ByRef ReturnLength As Integer) As Boolean
    End Function

    Private Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer           'Returns a valid LUID which is important when making security changes in NT.
    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    Private Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByVal TokenInformationClass As Integer, ByVal TokenInformation As Integer, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcId As Integer) As Integer
    Private Declare Function LookupPrivilegeNameA Lib "advapi32.dll" (ByVal lpSystemName As String, ByRef lpLuid As LUID, ByVal lpName As String, ByRef cchName As Integer) As Integer                'Used to adjust your program's security privileges, can't restore without it!
    Private Declare Function GetCurrentProcessId Lib "kernel32" () As Integer

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Shared Function LookupPrivilegeDisplayName( _
        ByVal SystemName As Integer, ByVal Name As String, ByVal DisplayName As String, _
        ByRef DisplayNameSize As Integer, ByRef LanguageId As Integer) As Boolean
    End Function

#End Region

    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer


    ' ========================================
    ' Constructor
    ' ========================================
    Public Sub New(ByVal processId As Integer)
        _pid = processId
    End Sub

    ' ========================================
    ' Properties
    ' ========================================
    Public Property Privilege(ByVal privilegeName As String) As PrivilegeStatus
        Get
            Return GetPrivilege(privilegeName)
        End Get
        Set(ByVal value As PrivilegeStatus)
            Call SetPrivilege(privilegeName, value)
        End Set
    End Property


    ' ========================================
    ' Public functions
    ' ========================================

    ' Get privileges list of process
    Public Function GetPrivilegesList() As PrivilegeInfo()

        Dim ListPrivileges() As PrivilegeInfo
        ReDim ListPrivileges(0)
        Dim hProcessToken As Integer
        Dim hProcess As Integer
        Dim RetLen As Integer
        Dim TokenPriv As New TOKEN_PRIVILEGES
        Dim strBuff As String
        Dim lngBuff As Integer
        Dim i As Integer

        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, 0, _pid)
        If hProcess > 0 Then
            OpenProcessToken(hProcess, TOKEN_QUERY, hProcessToken)
            If hProcessToken > 0 Then

                ' Get tokeninfo length
                GetTokenInformation(hProcessToken, 3, 0, 0, RetLen)
                Dim TokenInformation As IntPtr = Marshal.AllocHGlobal(RetLen)
                ' Get token information
                GetTokenInformation(hProcessToken, 3, CInt(TokenInformation), RetLen, 0)
                ' Get a valid structure
                TokenPriv = getTokenPrivilegeStructureFromPointer(TokenInformation, RetLen)

                ReDim ListPrivileges(TokenPriv.PrivilegeCount - 1)
                For i = 0 To TokenPriv.PrivilegeCount - 1
                    LookupPrivilegeNameA("", TokenPriv.Privileges(i).pLuid, "", lngBuff)
                    strBuff = Space$(lngBuff - 1)
                    LookupPrivilegeNameA("", TokenPriv.Privileges(i).pLuid, strBuff, lngBuff)
                    ListPrivileges(i).Name = strBuff
                    ListPrivileges(i).Status = TokenPriv.Privileges(i).Attributes
                    ListPrivileges(i).pLuid = TokenPriv.Privileges(i).pLuid
                    lngBuff = 0
                Next i
                CloseHandle(hProcessToken)
                Marshal.FreeHGlobal(TokenInformation)
            End If
            CloseHandle(hProcess)
        End If

        Return ListPrivileges

    End Function

    ' Return status from an integer
    Public Shared Function PrivilegeStatusToString(ByVal status As Integer) As String
        Select Case status
            Case 0
                Return "Diabled"
            Case 1
                Return "Default Enabled"
            Case 2
                Return "Enabled"
            Case 3
                Return "Default Enabled"
            Case 4
                Return "Removed"
            Case Else
                Return "Error (probably deleted)"
        End Select
    End Function

    ' Return color from status
    Public Shared Function GetColorFromStatus(ByVal status As Integer) As Color
        Select Case status
            Case SE_PRIVILEGE_ENABLED
                Return Color.FromArgb(224, 240, 224)
            Case SE_PRIVILEGE_ENABLED_BY_DEFAULT, 3
                Return Color.FromArgb(192, 240, 192)
            Case SE_PRIVILEGE_DISBALED
                Return Color.FromArgb(240, 224, 224)
            Case SE_PRIVILEGE_REMOVED
                Return Color.FromArgb(240, 192, 192)
            Case Else
                Return Color.White
        End Select
    End Function

    ' Get description of a privilege
    Public Shared Function GetPrivilegeDescription(ByVal PrivilegeName As String) As String

        Dim sb As String = Nothing
        Dim size As Integer = 0
        Dim languageId As Integer = 0

        LookupPrivilegeDisplayName(0, PrivilegeName, Nothing, size, languageId)
        sb = Space(size)
        LookupPrivilegeDisplayName(0, PrivilegeName, sb, size, languageId)

        Return sb

    End Function


    ' ========================================
    ' Private functions
    ' ========================================

    ' Set  privilege status
    Private Function SetPrivilege(ByVal seName As String, ByVal Status As PrivilegeStatus) As Boolean

        Dim hProcess As Integer
        Dim hProcessToken As Integer
        Dim Ret As Integer
        Dim lngToken As Integer
        Dim typLUID As LUID
        Dim typTokenPriv As TOKEN_PRIVILEGES2
        Dim newTokenPriv As TOKEN_PRIVILEGES2

        hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, _pid)
        If hProcess > 0 Then
            hProcessToken = OpenProcessToken(hProcess, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, lngToken)
            If hProcessToken > 0 Then
                Ret = LookupPrivilegeValue(Nothing, seName, typLUID)
                If Ret > 0 Then
                    typTokenPriv.PrivilegeCount = 1
                    typTokenPriv.Privileges.Attributes = Status
                    typTokenPriv.Privileges.pLuid = typLUID
                    Dim size As Integer = 4 + typTokenPriv.PrivilegeCount * 12
                    Dim ret2 As Integer
                    SetPrivilege = AdjustTokenPrivileges(lngToken, 0, typTokenPriv, size, newTokenPriv, ret2)
                End If
                CloseHandle(hProcessToken)
            End If
            CloseHandle(hProcess)
        End If

    End Function

    ' Get privilege status
    Private Function GetPrivilege(ByVal seName As String) As PrivilegeStatus

        Dim hProcessToken As Integer
        Dim hProcess As Integer
        Dim Ret As Integer
        Dim RetLen As Integer
        Dim TokenPriv As TOKEN_PRIVILEGES = Nothing
        Dim i As Integer
        Dim typLUID As LUID
        Dim res As PrivilegeStatus = CType(-1, PrivilegeStatus)

        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, 0, _pid)
        If hProcess > 0 Then
            OpenProcessToken(hProcess, TOKEN_QUERY, hProcessToken)
            If hProcessToken > 0 Then
                Ret = LookupPrivilegeValue(Nothing, seName, typLUID)

                ' Get tokeninfo length
                GetTokenInformation(hProcessToken, 3, 0, 0, RetLen)
                Dim TokenInformation As IntPtr = Marshal.AllocHGlobal(RetLen)
                ' Get token ingo
                GetTokenInformation(hProcessToken, 3, CInt(TokenInformation), RetLen, 0)
                TokenPriv = getTokenPrivilegeStructureFromPointer(TokenInformation, RetLen)

                For i = 0 To TokenPriv.PrivilegeCount - 1
                    If TokenPriv.Privileges(i).pLuid.lowpart = typLUID.lowpart Then
                        res = CType(TokenPriv.Privileges(i).Attributes, PrivilegeStatus)
                    End If
                Next i
                CloseHandle(hProcessToken)
                Marshal.FreeHGlobal(TokenInformation)
            End If
            CloseHandle(hProcess)
        End If

        Return res

    End Function

    Private Function GetStatusLong(ByVal Status As Boolean) As Integer
        If Status = True Then GetStatusLong = SE_PRIVILEGE_ENABLED Else GetStatusLong = SE_PRIVILEGE_DISBALED
    End Function

    Private Function getTokenPrivilegeStructureFromPointer(ByVal ptr As IntPtr, _
        ByVal RetLen As Integer) As TOKEN_PRIVILEGES

        'Public Structure LUID
        '	Dim lowpart As Integer
        '	Dim highpart As Integer
        'End Structure
        'Private Structure LUID_AND_ATTRIBUTES
        '	Dim pLuid As LUID
        '	Dim Attributes As Integer
        'End Structure
        'Private Structure TOKEN_PRIVILEGES
        '	Dim PrivilegeCount As Integer
        '	Dim Privileges() As LUID_AND_ATTRIBUTES
        'End Structure

        Dim ret As New TOKEN_PRIVILEGES

        ' Fill in int array from unmanaged memory
        Dim arr(CInt(RetLen / 4)) As Integer
        Marshal.Copy(ptr, arr, 0, arr.Length - 1)

        ' Get number of privileges
        Dim pCount As Integer = arr(0)     'CInt((RetLen - 4) / 12)
        ReDim ret.Privileges(pCount - 1)
        ret.PrivilegeCount = pCount

        ' Fill Privileges() array of ret
        ' Each item of array composed of three integer (lowPart, highPart and Attributes)
        Dim ep As Integer = 1
        For x As Integer = 0 To pCount - 1
            ret.Privileges(x).pLuid.lowpart = arr(ep)
            ret.Privileges(x).pLuid.highpart = arr(ep + 1)
            ret.Privileges(x).Attributes = arr(ep + 2)
            ep += 3
        Next

        Return ret

    End Function

End Class
