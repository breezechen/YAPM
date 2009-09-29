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


' This code is adapted from here :
' http://www.vb-helper.com/howto_net_listview_sort_clicked_column.html
' Thx to the author :-)

Option Strict On

Imports Common.Misc

' Implements a comparer for ListView columns.
Public Class ListViewComparer
    Implements IComparer

    Private m_ColumnNumber As Integer
    Private m_SortOrder As SortOrder

    Public Sub New(ByVal column_number As Integer, ByVal sort_order As SortOrder)
        m_ColumnNumber = column_number
        m_SortOrder = sort_order
    End Sub

    ' Compare the items in the appropriate column
    ' for objects x and y.
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim item_x As ListViewItem = DirectCast(x, ListViewItem)
        Dim item_y As ListViewItem = DirectCast(y, ListViewItem)

        ' Get the sub-item values.
        Dim string_x As String
        If item_x.SubItems.Count <= m_ColumnNumber Then
            string_x = ""
        Else
            string_x = item_x.SubItems(m_ColumnNumber).Text
        End If

        Dim string_y As String
        If item_y.SubItems.Count <= m_ColumnNumber Then
            string_y = ""
        Else
            string_y = item_y.SubItems(m_ColumnNumber).Text
        End If

        ' Compare them.
        If m_SortOrder = SortOrder.Ascending Then
            If IsNumeric(string_x) And IsNumeric(string_y) Then
                ' Values
                Return Val(string_x).CompareTo(Val(string_y))
            ElseIf IsHex(string_x) AndAlso IsHex(string_y) Then
                ' Hex value
                Return HexToLong(string_x).CompareTo(HexToLong(string_y))
            ElseIf (IsFormatedSize(string_x) AndAlso IsFormatedSize(string_y)) Then
                ' Sizes
                Return GetSizeFromFormatedSize(string_x).CompareTo(GetSizeFromFormatedSize(string_y))
            Else
                ' String
                Return String.Compare(string_x, string_y)
            End If
        Else
            If IsNumeric(string_x) And IsNumeric(string_y) Then
                ' Values
                Return Val(string_y).CompareTo(Val(string_x))
            ElseIf IsHex(string_x) AndAlso IsHex(string_y) Then
                ' Hex value
                Return HexToLong(string_y).CompareTo(HexToLong(string_x))
            ElseIf (IsFormatedSize(string_x) AndAlso IsFormatedSize(string_y)) Then
                ' Sizes
                Return GetSizeFromFormatedSize(string_y).CompareTo(GetSizeFromFormatedSize(string_x))
            Else
                ' Strings
                Return String.Compare(string_y, string_x)
            End If
        End If
    End Function

End Class