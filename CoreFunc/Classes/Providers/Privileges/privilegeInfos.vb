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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices

Public Class privilegeInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _procId As Integer
    Private _status As API.PRIVILEGE_STATUS
    Private _description As String
    Private _name As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _procId
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return _description
        End Get
    End Property
    Public ReadOnly Property Status() As API.PRIVILEGE_STATUS
        Get
            Return _status
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef name As String, ByVal pid As Integer)

        _procId = pid
        _name = name
        _status = GetPrivilege(name)
        _description = GetPrivilegeDescription(name)

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As privilegeInfos)

        With newI
            _status = newI.Status
            _description = newI.Description
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(1) As String

        s(0) = "Status"
        s(1) = "Description"

        Return s
    End Function


    ' Get description of a privilege
    Private Function GetPrivilegeDescription(ByVal PrivilegeName As String) As String

        Dim sb As String = Nothing
        Dim size As Integer = 0
        Dim languageId As Integer = 0

        API.LookupPrivilegeDisplayName(0, PrivilegeName, Nothing, size, languageId)
        sb = Space(size)
        API.LookupPrivilegeDisplayName(0, PrivilegeName, sb, size, languageId)

        Return sb

    End Function

    ' Get privilege status
    Private Function GetPrivilege(ByVal seName As String) As API.PRIVILEGE_STATUS

        Dim hProcessToken As Integer
        Dim hProcess As Integer
        Dim Ret As Integer
        Dim RetLen As Integer
        Dim TokenPriv As API.TOKEN_PRIVILEGES = Nothing
        Dim i As Integer
        Dim typLUID As API.LUID
        Dim res As API.PRIVILEGE_STATUS

        hProcess = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION, 0, _procId)
        If hProcess > 0 Then
            API.OpenProcessToken(hProcess, API.TOKEN_QUERY, hProcessToken)
            If hProcessToken > 0 Then
                Ret = API.LookupPrivilegeValue(Nothing, seName, typLUID)

                ' Get tokeninfo length
                API.GetTokenInformation(hProcessToken, 3, 0, 0, RetLen)
                Dim TokenInformation As IntPtr = Marshal.AllocHGlobal(RetLen)
                ' Get token ingo
                API.GetTokenInformation(hProcessToken, 3, CInt(TokenInformation), RetLen, 0)
                TokenPriv = getTokenPrivilegeStructureFromPointer(TokenInformation, RetLen)

                For i = 0 To TokenPriv.PrivilegeCount - 1
                    If TokenPriv.Privileges(i).pLuid.lowpart = typLUID.lowpart Then
                        res = CType(TokenPriv.Privileges(i).Attributes, API.PRIVILEGE_STATUS)
                    End If
                Next i
                API.CloseHandle(hProcessToken)
                Marshal.FreeHGlobal(TokenInformation)
            End If
            API.CloseHandle(hProcess)
        End If

        Return res

    End Function

    Private Function getTokenPrivilegeStructureFromPointer(ByVal ptr As IntPtr, _
        ByVal RetLen As Integer) As API.TOKEN_PRIVILEGES

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

        Dim ret As New API.TOKEN_PRIVILEGES

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
