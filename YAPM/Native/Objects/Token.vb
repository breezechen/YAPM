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

Imports System.Runtime.InteropServices
Imports YAPM.Native.Api
Imports System.Text

Namespace Native.Objects

    Public Class Token

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Get privileges list of process
        Public Shared Function GetPrivilegesList(ByVal pid As Integer) As Native.Api.NativeStructs.PrivilegeInfo()

            Dim ListPrivileges(-1) As Native.Api.NativeStructs.PrivilegeInfo
            Dim hProcessToken As IntPtr
            Dim hProcess As IntPtr
            Dim RetLen As Integer
            Dim TokenPriv As New Native.Api.NativeStructs.TokenPrivileges

            hProcess = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.QueryInformation, False, pid)
            If hProcess <> IntPtr.Zero Then
                Native.Api.NativeFunctions.OpenProcessToken(hProcess, Native.Security.TokenAccess.Query, hProcessToken)
                If hProcessToken <> IntPtr.Zero Then

                    ' Get tokeninfo length
                    Native.Api.NativeFunctions.GetTokenInformation(hProcessToken, Native.Api.NativeEnums.TokenInformationClass.TokenPrivileges, IntPtr.Zero, 0, RetLen)

                    'PERFISSUE (do not alloc each time)
                    Dim memAlloc As New Native.Memory.MemoryAlloc(RetLen)

                    ' Get token information
                    Native.Api.NativeFunctions.GetTokenInformation(hProcessToken, Native.Api.NativeEnums.TokenInformationClass.TokenPrivileges, memAlloc.Pointer, memAlloc.Size, RetLen)

                    ' Get number of privileges
                    Dim count As Integer = CInt(memAlloc.ReadUInt32(0))
                    ReDim ListPrivileges(count - 1)

                    ' Retrieve list of privileges
                    For i As Integer = 0 To count - 1
                        Dim struct As NativeStructs.LuidAndAttributes = _
                            memAlloc.ReadStruct(Of NativeStructs.LuidAndAttributes)(4, i)   ' 4 first bytes are used for the size
                        ListPrivileges(i) = New NativeStructs.PrivilegeInfo
                        With ListPrivileges(i)
                            .pLuid = struct.pLuid
                            .Status = struct.Attributes
                            ' Get name
                            Dim sb As New StringBuilder
                            Dim size As Integer = 0
                            ' Get size required for name
                            If NativeFunctions.LookupPrivilegeName("", struct.pLuid, sb, size) = False Then
                                ' Redim capacity
                                sb.EnsureCapacity(size)
                                NativeFunctions.LookupPrivilegeName("", struct.pLuid, sb, size)
                            End If
                            .Name = sb.ToString

                        End With

                    Next

                    memAlloc.Free()
                    NativeFunctions.CloseHandle(hProcessToken)

                End If
                NativeFunctions.CloseHandle(hProcess)
            End If

            Return ListPrivileges

        End Function


        ' Set privilege status
        Public Shared Function SetPrivilege(ByVal processId As Integer, _
                                            ByVal seName As String, _
                                            ByVal seStatus As NativeEnums.SePrivilegeAttributes) As Boolean

            Dim hProcess As IntPtr
            Dim fRet As Boolean
            Dim lngToken As IntPtr
            Dim typLUID As NativeStructs.Luid
            Dim typTokenPriv As New NativeStructs.TokenPrivileges
            Dim newTokenPriv As IntPtr
            Dim ret2 As IntPtr

            ' Open handle to process
            hProcess = NativeFunctions.OpenProcess(Security.ProcessAccess.QueryInformation, _
                                                   False, processId)

            If hProcess <> IntPtr.Zero Then
                ' Get token handle
                NativeFunctions.OpenProcessToken(hProcess, Security.TokenAccess.Query Or Security.TokenAccess.AdjustPrivileges, lngToken)

                If lngToken <> IntPtr.Zero Then

                    ' Retrieve Luid from PrivilegeName
                    If NativeFunctions.LookupPrivilegeValue(Nothing, seName, typLUID) Then

                        ' Adjust privilege
                        typTokenPriv.PrivilegeCount = 1
                        ReDim typTokenPriv.Privileges(0)
                        typTokenPriv.Privileges(0) = New NativeStructs.LuidAndAttributes
                        typTokenPriv.Privileges(0).Attributes = seStatus
                        typTokenPriv.Privileges(0).pLuid = typLUID
                        fRet = NativeFunctions.AdjustTokenPrivileges(lngToken, False, typTokenPriv, _
                                                         0, newTokenPriv, ret2)
                    End If
                    NativeFunctions.CloseHandle(lngToken)
                End If
                NativeFunctions.CloseHandle(hProcess)
            End If

            Return fRet
        End Function

        ' Get description of a privilege
        Public Shared Function GetPrivilegeDescription(ByVal privilegeName As String) As String

            Dim sb As New StringBuilder(&H100)
            Dim size As Integer = sb.Capacity
            Dim languageId As Integer = 0
            If Not NativeFunctions.LookupPrivilegeDisplayName(Nothing, privilegeName, _
                                                              sb, size, languageId) Then
                sb.EnsureCapacity(size)

                NativeFunctions.LookupPrivilegeDisplayName(Nothing, privilegeName, _
                                                           sb, size, languageId)
            End If
            Return sb.ToString()

        End Function


        ' ========================================
        ' Private functions
        ' ========================================

    End Class

End Namespace
