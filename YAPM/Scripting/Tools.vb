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

Namespace Scripting.Items

    Public Class Tools

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

        ' Remove spaces/tabs from a string
        Public Shared Function RemoveSpaces(ByVal s As String) As String
            If s IsNot Nothing Then
                Return s.Trim
            Else
                Return Nothing
            End If
        End Function


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
