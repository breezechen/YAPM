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

Public Class cSystem

    ' ========================================
    ' Public
    ' ========================================
    Public Shared Function Hibernate(Optional ByVal force As Boolean = False) As Boolean
        Return API.SetSuspendState(True, force, False)
    End Function

    Public Shared Function Sleep(Optional ByVal force As Boolean = False) As Boolean
        Return API.SetSuspendState(False, force, False)
    End Function

    Public Shared Function Logoff(Optional ByVal force As Boolean = False) As Boolean
        If force Then
            Return API.ExitWindowsEx(API.ExitFlags.Logoff Or API.ExitFlags.Force, 0)
        Else
            Return API.ExitWindowsEx(API.ExitFlags.Logoff, 0)
        End If
    End Function

    Public Shared Function Lock() As Boolean
        Return API.LockWorkStation()
    End Function

    Public Shared Function Shutdown(Optional ByVal force As Boolean = False) As Boolean
        If force Then
            Return API.ExitWindowsEx(API.ExitFlags.Shutdown Or API.ExitFlags.Force, 0)
        Else
            Return API.ExitWindowsEx(API.ExitFlags.Shutdown, 0)
        End If
    End Function

    Public Shared Function Restart(Optional ByVal force As Boolean = False) As Boolean
        If force Then
            Return API.ExitWindowsEx(API.ExitFlags.Reboot Or API.ExitFlags.Force, 0)
        Else
            Return API.ExitWindowsEx(API.ExitFlags.Reboot, 0)
        End If
    End Function

    Public Shared Function Poweroff(Optional ByVal force As Boolean = False) As Boolean
        If force Then
            Return API.ExitWindowsEx(API.ExitFlags.Poweroff Or API.ExitFlags.Force, 0)
        Else
            Return API.ExitWindowsEx(API.ExitFlags.Poweroff, 0)
        End If
    End Function
End Class
