' =======================================================
' Yet Another Process Monitor (YAPM)
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
    ' API declarations
    ' ========================================
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function ExitWindowsEx(ByVal flags As ExitFlags, ByVal reason As Integer) As Boolean
    End Function
    <DllImport("powrprof.dll", SetLastError:=True)> _
    Private Shared Function SetSuspendState(ByVal Hibernate As Boolean, ByVal ForceCritical As Boolean, ByVal DisableWakeEvent As Boolean) As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function LockWorkStation() As Boolean
    End Function

    Private Enum ExitFlags As Integer
        Logoff = &H0
        Shutdown = &H1
        Reboot = &H2
        Poweroff = &H8
        RestartApps = &H40
        Force = &H4
        ForceIfHung = &H10
    End Enum


    ' ========================================
    ' Public
    ' ========================================
    Public Shared Function Hibernate() As Boolean
        Return SetSuspendState(True, False, False)
    End Function

    Public Shared Function Sleep() As Boolean
        Return SetSuspendState(False, False, False)
    End Function

    Public Shared Function Logoff() As Boolean
        Return ExitWindowsEx(ExitFlags.Logoff, 0)
    End Function

    Public Shared Function Lock() As Boolean
        Return LockWorkStation()
    End Function

    Public Shared Function Shutdown() As Boolean
        Return ExitWindowsEx(ExitFlags.Shutdown, 0)
    End Function

    Public Shared Function Restart() As Boolean
        Return ExitWindowsEx(ExitFlags.Reboot, 0)
    End Function

    Public Shared Function Poweroff() As Boolean
        Return ExitWindowsEx(ExitFlags.Poweroff, 0)
    End Function
End Class
