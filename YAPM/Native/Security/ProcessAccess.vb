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

Namespace Native.Api.Security

    <Flags()> _
    Public Enum ProcessAccess As UInteger
        Terminate = &H1
        CreateThread = &H2
        SetSessionId = &H4
        VmOperation = &H8
        VmRead = &H10
        VmWrite = &H20
        DupHandle = &H40
        CreateProcess = &H80
        SetQuota = &H100
        SetInformation = &H200
        QueryInformation = &H400
        SetPort = &H800
        SuspendResume = &H800
        QueryLimitedInformation = &H1000
        ' should be 0xffff on Vista, but is 0xfff for backwards compatibility
        All = StandardRights.Required Or StandardRights.Synchronize Or &HFFF
    End Enum

End Namespace
