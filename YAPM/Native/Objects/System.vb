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
Imports Native.Api

Public Class cSystem

    ' ========================================
    ' Public
    ' ========================================
    Public Shared Function Hibernate(Optional ByVal force As Boolean = True) As Boolean
        Return NativeFunctions.SetSuspendState(True, force, False)
    End Function

    Public Shared Function Sleep(Optional ByVal force As Boolean = True) As Boolean
        Return NativeFunctions.SetSuspendState(False, force, False)
    End Function

    Public Shared Function Logoff(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Logoff Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Logoff, 0)
        End If
    End Function

    Public Shared Function Lock() As Boolean
        Return NativeFunctions.LockWorkStation()
    End Function

    Public Shared Function Shutdown(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Shutdown Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Shutdown, 0)
        End If
    End Function

    Public Shared Function Restart(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Reboot Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Reboot, 0)
        End If
    End Function

    Public Shared Function Poweroff(Optional ByVal force As Boolean = True) As Boolean
        If force Then
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Poweroff Or NativeEnums.ExitWindowsFlags.Force, 0)
        Else
            Return NativeFunctions.ExitWindowsEx(NativeEnums.ExitWindowsFlags.Poweroff, 0)
        End If
    End Function
End Class
