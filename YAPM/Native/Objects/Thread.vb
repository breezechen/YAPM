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

Namespace Native.Objects

    Public Class Thread


        ' Resume a thread
        Public Shared Function ResumeThreadByHandle(ByVal hThread As IntPtr) As Boolean
            If hThread <> IntPtr.Zero Then
                Return NativeFunctions.ResumeThread(hThread) > 0
            Else
                Return False
            End If
        End Function
        Public Shared Function ResumeThreadById(ByVal thread As Integer) As Boolean

            ' Open handle, resume thread and close handle
            Dim hThread As IntPtr = _
                    NativeFunctions.OpenThread(NativeEnums.ThreadAccess.SuspendResume, False, thread)
            Dim ret As Boolean = ResumeThreadByHandle(hThread)
            NativeFunctions.CloseHandle(hThread)

            Return ret
        End Function


        ' Suspend a thread
        Public Shared Function SuspendThreadByHandle(ByVal hThread As IntPtr) As Boolean
            If hThread <> IntPtr.Zero Then
                Return NativeFunctions.SuspendThread(hThread) > 0
            Else
                Return False
            End If
        End Function
        Public Shared Function SuspendThreadById(ByVal thread As Integer) As Boolean

            ' Open handle, suspend thread and close handle
            Dim hThread As IntPtr = _
                    NativeFunctions.OpenThread(NativeEnums.ThreadAccess.SuspendResume, False, thread)
            Dim ret As Boolean = SuspendThreadByHandle(hThread)
            NativeFunctions.CloseHandle(hThread)

            Return ret
        End Function
    End Class

End Namespace
