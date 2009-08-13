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

        ' Set privilege status
        Public Shared Function SetPrivilege(ByVal processId As Integer, _
                                            ByVal seName As String, _
                                            ByVal seStatus As NativeEnums.SePrivilegeAttributes) As Boolean

            Dim hProcess As IntPtr
            Dim Ret As Boolean
            Dim fRet As Boolean
            Dim lngToken As IntPtr
            Dim typLUID As NativeStructs.Luid
            Dim typTokenPriv As NativeStructs.TokenPrivileges
            Dim newTokenPriv As IntPtr
            Dim ret2 As IntPtr

            ' Open handle to process
            hProcess = NativeFunctions.OpenProcess(Api.Security.ProcessAccess.QueryInformation, _
                                                   False, processId)

            If hProcess <> IntPtr.Zero Then
                ' Get token handle
                NativeFunctions.OpenProcessToken(hProcess, Api.Security.TokenAccess.Query Or Api.Security.TokenAccess.AdjustPrivileges, lngToken)

                If lngToken <> IntPtr.Zero Then
                    If NativeFunctions.LookupPrivilegeValue(Nothing, seName, typLUID) Then
                        typTokenPriv.PrivilegeCount = 1
                        typTokenPriv.Privileges.Attributes = seStatus
                        typTokenPriv.Privileges.pLuid = typLUID
                        ' 64TODO
                        Dim size As Integer = 4 + typTokenPriv.PrivilegeCount * 12
                        fRet = NativeFunctions.AdjustTokenPrivileges(lngToken, False, typTokenPriv, _
                                                         size, newTokenPriv, ret2)
                    End If
                    NativeFunctions.CloseHandle(lngToken)
                End If
                NativeFunctions.CloseHandle(hProcess)
            End If

            Return fRet
        End Function


        ' Get privilege status
        Public Shared Function GetPrivilege(ByVal processId As Integer, _
                            ByVal seName As String) As NativeEnums.SePrivilegeAttributes

            Dim hProcessToken As IntPtr
            Dim hProcess As IntPtr
            Dim Ret As Boolean
            Dim RetLen As Integer
            Dim TokenPriv As Native.Api.NativeStructs.TOKEN_PRIVILEGES = Nothing
            Dim i As Integer
            Dim typLUID As Native.Api.NativeStructs.Luid
            Dim res As Native.Api.NativeEnums.SePrivilegeAttributes

            hProcess = NativeFunctions.OpenProcess(Api.Security.ProcessAccess.QueryInformation, _
                                       False, processId)
            If hProcess <> IntPtr.Zero Then
                NativeFunctions.OpenProcessToken(hProcess, Api.Security.TokenAccess.Query, hProcessToken)
                If hProcessToken <> IntPtr.Zero Then
                    Ret = NativeFunctions.LookupPrivilegeValue(Nothing, seName, typLUID)

                    ' Get tokeninfo length
                    NativeFunctions.GetTokenInformation(hProcessToken, NativeEnums.TokenInformationClass.TokenPrivileges, IntPtr.Zero, 0, RetLen)
                    Dim TokenInformation As IntPtr = Marshal.AllocHGlobal(RetLen)
                    ' Get token ingo
                    NativeFunctions.GetTokenInformation(hProcessToken, NativeEnums.TokenInformationClass.TokenPrivileges, TokenInformation, RetLen, 0)
                    TokenPriv = getTokenPrivilegeStructureFromPointer(TokenInformation, RetLen)

                    For i = 0 To TokenPriv.PrivilegeCount - 1
                        If TokenPriv.Privileges(i).pLuid.lowpart = typLUID.lowpart Then
                            res = TokenPriv.Privileges(i).Attributes
                        End If
                    Next i
                    NativeFunctions.CloseHandle(hProcessToken)
                    Marshal.FreeHGlobal(TokenInformation)
                End If
                NativeFunctions.CloseHandle(hProcess)
            End If

            Return res

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



        Private Shared Function getTokenPrivilegeStructureFromPointer(ByVal ptr As IntPtr, _
            ByVal RetLen As Integer) As Native.Api.NativeStructs.TOKEN_PRIVILEGES

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

            Dim ret As New Native.Api.NativeStructs.TOKEN_PRIVILEGES

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

End Namespace
