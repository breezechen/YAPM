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


Namespace Common

    Public Class Misc



        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Return a path without double-slashs
        Public Shared Function FormatPathWithoutDoubleSlashs(ByVal path As String) As String
            If path IsNot Nothing Then
                Return path.Replace("\\", "\")
            Else
                Return Nothing
            End If
        End Function

        ' Return a path with double-slashs
        Public Shared Function FormatPathWithDoubleSlashs(ByVal path As String) As String
            If path IsNot Nothing Then
                Return path.Replace("\", "\\")
            Else
                Return Nothing
            End If
        End Function


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
