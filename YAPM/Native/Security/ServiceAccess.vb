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
    Public Enum ServiceAccess As UInteger
        QueryConfig = &H1
        ChangeConfig = &H2
        QueryStatus = &H4
        EnumerateDependents = &H8
        Start = &H10
        [Stop] = &H20
        PauseContinue = &H40
        Interrogate = &H80
        UserDefinedControl = &H100
        All = StandardRights.Required Or QueryConfig Or ChangeConfig Or QueryStatus Or EnumerateDependents Or Start Or [Stop] Or PauseContinue Or Interrogate Or UserDefinedControl
    End Enum

End Namespace