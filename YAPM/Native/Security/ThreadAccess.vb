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

Namespace Native.Security

    <Flags()> _
    Public Enum ThreadAccess As UInteger
        Terminate = &H1
        SuspendResume = &H2
        Alert = &H4
        GetContext = &H8
        SetContext = &H10
        SetInformation = &H20
        QueryInformation = &H40
        SetThreadToken = &H80
        Impersonate = &H100
        DirectImpersonation = &H200
        SetLimitedInformation = &H400
        QueryLimitedInformation = &H800
        ' should be 0xffff on Vista, but is 0xfff for backwards compatibility
        All = StandardRights.Required Or StandardRights.Synchronize Or &HFFF
    End Enum

End Namespace
