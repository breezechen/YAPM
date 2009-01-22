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

Module mdlPrivileges

    Structure LUID
        Public LowPart As Integer
        Public HighPart As Integer
    End Structure

    Structure TOKEN_PRIVILEGES
        Public PrivilegeCount As Integer
        Public TheLuid As LUID
        Public Attributes As Integer
    End Structure

    Private Const SE_PRIVILEGE_ENABLED As Integer = &H2
    Private Const TOKEN_QUERY As Integer = &H8
    Private Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
    Private Const SE_DEBUG_NAME As String = "SeDebugPrivilege"

    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As IntPtr, ByVal DesiredAccess As Integer, ByRef TokenHandle As IntPtr) As Boolean
    Private Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Boolean
    Private Declare Function AdjustTokenPrivileges Lib "advapi32.dll" (ByVal TokenHandle As IntPtr, ByVal DisableAllPrivileges As Boolean, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByRef PreviousState As TOKEN_PRIVILEGES, ByVal ReturnLength As IntPtr) As Boolean
    Private Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As Integer) As Integer


    ' Get the Debug Privilege
    Public Function SetDebuPrivilege() As Boolean

        Dim hProc, hToken As IntPtr
        Dim luid_Restore As LUID
        Dim tp As New TOKEN_PRIVILEGES
        Dim r As Boolean = False

        ' get the current process's token
        hProc = Process.GetCurrentProcess().Handle
        hToken = IntPtr.Zero
        If Not OpenProcessToken(hProc, TOKEN_ADJUST_PRIVILEGES Or TOKEN_QUERY, hToken) Then
            Return False
        End If

        ' get the LUID for the Restore privilege (provided it already exist)
        If Not LookupPrivilegeValue(Nothing, SE_DEBUG_NAME, luid_Restore) Then
            Return False
        End If

        tp.PrivilegeCount = 1
        tp.TheLuid = luid_Restore
        tp.Attributes = SE_PRIVILEGE_ENABLED

        ' enable the privileges
        CloseHandle(CInt(hToken))
        Return AdjustTokenPrivileges(hToken, False, tp, 0, Nothing, IntPtr.Zero)

    End Function

    ' Return true if your are logged as an administrator
    Public Function IsAdministrator() As Boolean
        If My.User.IsAuthenticated Then
            If My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator) Then
                Return True
            End If
        End If
        Return False
    End Function

End Module
