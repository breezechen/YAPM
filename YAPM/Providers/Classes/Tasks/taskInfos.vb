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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by NtQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices

<Serializable()> Public Class taskInfos
    Inherits windowInfos


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef window As windowInfos)
        MyBase.New(window)
    End Sub

    ' Retrieve all information's names availables
    Public Overloads Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
        Dim s(11) As String

        s(0) = "Caption"
        s(1) = "Process"
        s(2) = "CpuUsage"
        s(3) = "IsTask"
        s(4) = "Enabled"
        s(5) = "Visible"
        s(6) = "ThreadId"
        s(7) = "Height"
        s(8) = "Width"
        s(9) = "Top"
        s(10) = "Left"
        s(11) = "Opacity"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        Return s
    End Function

End Class
