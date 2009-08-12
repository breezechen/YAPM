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
        Public Enum TokenAccess As UInteger
        AllPlusSessionId = &HF01FF
        MaximumAllowed = &H2000000
        AccessSystemSecurity = &H1000000
        AssignPrimary = &H1
        Duplicate = &H2
        Impersonate = &H4
        Query = &H8
        QuerySource = &H10
        AdjustPrivileges = &H20
        AdjustGroups = &H40
        AdjustDefault = &H80
        AdjustSessionId = &H100
        All = StandardRights.Required Or AssignPrimary Or Duplicate Or Impersonate Or Query Or QuerySource Or AdjustPrivileges Or AdjustGroups Or AdjustDefault Or AdjustSessionId
        GenericRead = StandardRights.Read Or Query
        GenericWrite = StandardRights.Write Or AdjustPrivileges Or AdjustGroups Or AdjustDefault
        GenericExecute = StandardRights.Execute
    End Enum

End Namespace
