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

Namespace Common.Objects

    Public Class Token

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
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Return current user name
        Public Shared ReadOnly Property CurrentUserName() As String
            Get
                Static retrieved As Boolean = False
                Static value As String = ""
                If retrieved = False Then
                    retrieved = True
                    value = System.Security.Principal.WindowsIdentity.GetCurrent.Name
                End If
                Return value
            End Get
        End Property


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace