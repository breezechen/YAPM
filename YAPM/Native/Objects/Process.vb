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

    Public Class Process


        ' Set affinity to a process
        Public Shared Function SetAffinityByHandle(ByVal hProc As IntPtr, _
                                           ByVal affinity As Integer) As Boolean
            If hProc <> IntPtr.Zero Then
                Return NativeFunctions.SetProcessAffinityMask(hProc, New IntPtr(affinity))
            Else
                Return False
            End If
        End Function
        Public Shared Function SetAffinityById(ByVal process As Integer, _
                                           ByVal affinity As Integer) As Boolean

            ' Open handle, set affinity and close handle
            Dim hProc As IntPtr = _
                    NativeFunctions.OpenProcess(NativeEnums.ProcessAccess.SetInformation, False, process)
            Dim ret As Boolean = SetAffinityByHandle(hProc, affinity)
            NativeFunctions.CloseHandle(hProc)

            Return ret
        End Function


        ' 

    End Class

End Namespace
