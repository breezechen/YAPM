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


Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports YAPM.Native.Api.NativeStructs
Imports YAPM.Native.Api.NativeEnums
Imports YAPM.Native.Api.NativeFunctions

Namespace Native.Api

    Public Class Win32

        ' Return last error as a string
        Public Shared Function GetLastError() As String

            Dim lpMsgBuf As IntPtr = IntPtr.Zero
            Dim nLastError As Integer = Marshal.GetLastWin32Error
            Dim dwChars As UInteger = FormatMessage(FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER _
                                                    Or FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM _
                                                    Or FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS, _
                                        IntPtr.Zero, nLastError, 0, lpMsgBuf, 0, IntPtr.Zero)

            ' Unknown error
            If dwChars = 0 Then
                Return "Unknown error occured (0x" & nLastError.ToString("x") & ")"
            End If

            ' Retrieve string
            Dim sRet As String = Marshal.PtrToStringAnsi(lpMsgBuf)
            lpMsgBuf = LocalFree(lpMsgBuf)
            Return sRet

        End Function

    End Class

End Namespace
