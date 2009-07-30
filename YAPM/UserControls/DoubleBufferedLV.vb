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

Imports System.Windows.Forms.ListView

Public Class DoubleBufferedLV
    Inherits System.Windows.Forms.ListView

    Private m_SortingColumn As ColumnHeader

    Private _selectedItemsVMode As New List(Of ListViewItem)

    ' ========================================
    ' Public
    ' ========================================
    Public Sub New()
        MyBase.New()
        ' Set double buffered property to true
        'Me.DoubleBuffered = True
    End Sub

    Public ReadOnly Property SelectedItemsVMode() As List(Of ListViewItem)
        Get
            Return _selectedItemsVMode
        End Get
    End Property

    Public Property OverriddenDoubleBuffered() As Boolean
        Get
            Return Me.DoubleBuffered
        End Get
        Set(ByVal value As Boolean)
            Me.DoubleBuffered = value
        End Set
    End Property

    Protected Overrides Sub OnColumnClick(ByVal e As System.Windows.Forms.ColumnClickEventArgs)
        MyBase.OnColumnClick(e)

        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            Me.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        Me.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        Me.Sort()
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)


        If e.Control Then
            If e.KeyCode = Keys.A Then
                If Me.VirtualMode = False Then
                    For Each _it As ListViewItem In Me.Items
                        _it.Selected = True
                    Next
                Else
                    ' Virtual mode -> Me.Items unusable
                    For i As Integer = 0 To Me.VirtualListSize - 1
                        Me.SelectedIndices.Add(i)
                    Next
                End If
            ElseIf e.KeyCode = Keys.C Then

                Dim _s As String = ""
                Dim x As Integer = 0
                For Each col As ColumnHeader In Me.Columns
                    x += 1
                    _s &= col.Text
                    If x < Me.Columns.Count Then
                        _s &= vbTab
                    End If
                Next
                _s &= vbNewLine
                x = 0
                Dim y As Integer = 0
                If Me.VirtualMode = False Then
                    For Each _it As ListViewItem In Me.SelectedItems
                        For Each _sub As ListViewItem.ListViewSubItem In _it.SubItems
                            _s &= _sub.Text
                            y += 1
                            If y < _it.SubItems.Count Then
                                _s &= vbTab
                            End If
                        Next
                        y = 0
                        If x < Me.Items.Count Then
                            _s &= vbNewLine
                        End If
                        x += 1
                    Next
                    My.Computer.Clipboard.SetText(_s)

                Else
                    ' Virtual mode -> Me.Items unusable
                    ' So we use our custom list of selected items
                    For Each lv As ListViewItem In Me._selectedItemsVMode
                        For Each _sub As ListViewItem.ListViewSubItem In lv.SubItems
                            _s &= _sub.Text
                            y += 1
                            If y < lv.SubItems.Count Then
                                _s &= vbTab
                            End If
                        Next
                        y = 0
                        If x < Me.VirtualListSize Then
                            _s &= vbNewLine
                        End If
                        x += 1
                    Next
                    My.Computer.Clipboard.SetText(_s)

                End If

            End If
        End If

    End Sub

    ' We have to manually save selected items into a private list
    ' in virtual mode, because it's the only way to retrieve SelectedItems
    Protected Overrides Sub OnItemSelectionChanged(ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs)
        MyBase.OnItemSelectionChanged(e)
        If Me.VirtualMode Then
            If e.IsSelected Then
                Me._selectedItemsVMode.Add(e.Item)
            Else
                Me._selectedItemsVMode.Remove(e.Item)
            End If
        End If
    End Sub

    ' Avoid to reorder first column
    Protected Overrides Sub OnColumnReordered(ByVal e As System.Windows.Forms.ColumnReorderedEventArgs)
        If e.OldDisplayIndex = 0 Or e.NewDisplayIndex = 0 Then
            e.Cancel = True
        End If
        MyBase.OnColumnReordered(e)
    End Sub

End Class