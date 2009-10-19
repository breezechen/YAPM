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

Public Class HXXXProp
    Inherits UserControl

    ' Concerned handle
    Private _handle As cHandle
    Public Property TheHandle() As cHandle
        Get
            Return _handle
        End Get
        Set(ByVal value As cHandle)
            _handle = value
        End Set
    End Property

    Public Sub New()
        '
    End Sub
    Public Sub New(ByVal handle As cHandle)
        _handle = handle
    End Sub

    ' Refresh displayed informations
    Public Overridable Sub RefreshInfos()
        '
    End Sub

End Class
