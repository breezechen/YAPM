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
' aInteger with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Public Class cShortcut

    ' Yeah, all properties are public variables...
    Public Tag As String
    Public Key As String
    Public Key1 As Integer
    Public Key2 As Integer
    Public Key3 As Integer
    Public FormEvent As Long        ' Pointer

    Public Sub New(ByVal _key1 As Integer, ByVal _key2 As Integer, ByVal _key3 As Integer)
        Key1 = _key3
        Key2 = _key2
        Key3 = _key1
    End Sub

End Class
