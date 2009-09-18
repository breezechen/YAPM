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


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Contains NtStatus <-> Description
        Private Shared _dicoNtStatus As New Dictionary(Of UInt32, String)


        ' ========================================
        ' Public properties
        ' ========================================


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Return last error as a string
        Public Shared Function GetLastError() As String

            Dim nLastError As Integer = Marshal.GetLastWin32Error

            If nLastError = 0 Then
                ' Error occured
                Return ""
            Else

                Dim lpMsgBuf As New System.Text.StringBuilder(&H100)
                Dim dwChars As UInteger = FormatMessage(FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER _
                                                        Or FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM _
                                                        Or FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS, _
                                            IntPtr.Zero, nLastError, 0, lpMsgBuf, lpMsgBuf.Capacity, IntPtr.Zero)

                ' Unknown error
                If dwChars = 0 Then
                    Return "Unknown error occured (0x" & nLastError.ToString("x") & ")"
                End If

                ' Retrieve string
                Return lpMsgBuf.ToString
            End If

        End Function


        ' Get elapsed time since Windows started
        Public Shared Function GetElapsedTime() As Integer
            Return Native.Api.NativeFunctions.GetTickCount
        End Function


        ' Return message associated to a NtStatus
        Public Shared Function GetNtStatusMessageAsString(ByVal status As UInt32) As String

            Dim sRes As String
            If status = 0 Then

                sRes = "Success"

            Else

                ' If the status has already been retrieved, return result immediately
                If _dicoNtStatus.ContainsKey(status) Then
                    Return _dicoNtStatus.Item(status)
                End If

                Dim lpMessageBuffer As New StringBuilder(&H200)
                Dim Hand As IntPtr = LoadLibrary("NTDLL.DLL")

                ' Get the buffer
                FormatMessage(FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER Or _
                            FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM Or _
                            FormatMessageFlags.FORMAT_MESSAGE_FROM_HMODULE, _
                            Hand, _
                            status, _
                            MAKELANGID(NativeConstants.LANG_NEUTRAL, _
                                     NativeConstants.SUBLANG_DEFAULT), _
                            lpMessageBuffer, _
                            0, _
                            Nothing)

                ' Now get the string
                sRes = lpMessageBuffer.ToString
                FreeLibrary(Hand)

                ' Add to dico
                If _dicoNtStatus.ContainsKey(status) = False Then
                    _dicoNtStatus.Add(status, sRes)
                End If

            End If

            Return sRes
        End Function



        ' ========================================
        ' Private functions
        ' ========================================

        Private Shared Function MAKELANGID(ByVal primary As Integer, ByVal [sub] As Integer) As Integer
            Return (CUShort([sub]) << 10) Or CUShort(primary)
        End Function
        Private Shared Function PRIMARYLANGID(ByVal lcid As Integer) As Integer
            Return CUShort(lcid) And &H3FF
        End Function
        Private Shared Function SUBLANGID(ByVal lcid As Integer) As Integer
            Return CUShort(lcid) >> 10
        End Function

    End Class

End Namespace
