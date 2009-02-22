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
' along with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Imports System.Runtime.InteropServices

Public Class taskList
    Inherits YAPM.DoubleBufferedLV

    Private Declare Function GetTickCount Lib "kernel32" () As Integer


    ' ========================================
    ' Private
    ' ========================================
    Private _dicoNew As New Dictionary(Of String, cTask)
    Private _dicoDel As New Dictionary(Of String, cTask)
    Private _buffDico As New Dictionary(Of String, cTask.LightWindow)
    Private _dico As New Dictionary(Of String, cTask)

    Private _firstItemUpdate As Boolean = True
    Private _columnsName() As String

    Private _IMG As ImageList
    Private m_SortingColumn As ColumnHeader

    Private _foreColor As Color = Color.FromArgb(30, 30, 30)

    Private NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Private DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================

#End Region

    ' ========================================
    ' Public properties
    ' ========================================

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        _IMG = New ImageList
        _IMG.ImageSize = New Size(16, 16)
        _IMG.ColorDepth = ColorDepth.Depth32Bit

        Me.SmallImageList = _IMG
        _IMG.Images.Add("noIcon", My.Resources.application_blue)

    End Sub

    ' Call this to update items in listview
    Public Sub UpdateItems()

        Dim _test As Integer = GetTickCount


        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If


        ' Now enumerate items
        Dim _itemId() As Integer
        ReDim _itemId(0)
        Call cTask.Enumerate(_itemId, _buffDico)


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cTask In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Handle.ToString, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each z As Integer In _itemId
            If Not (_dico.ContainsKey(z.ToString)) Then
                ' Add to dico
                _dicoNew.Add(z.ToString, Nothing)
            End If
        Next


        ' Now remove deleted items from dictionnary
        For Each z As Integer In _dico.Keys
            If Array.IndexOf(_itemId, z) < 0 Then
                ' Remove from dico
                _dico.Item(z.ToString).IsKilledItem = True  ' Will be deleted next time
            End If
        Next


        ' Now remove all deleted items from listview and _dico
        For Each z As Integer In _dicoDel.Keys
            Me.Items.RemoveByKey(z.ToString)
            _dico.Remove(z.ToString)
        Next
        _dicoDel.Clear()


        ' Merge _dico and _dicoNew
        For Each z As Integer In _dicoNew.Keys
            Dim _it As cTask = New cTask(_buffDico.Item(z.ToString))
            _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
            _dico.Add(z.ToString, _it)
        Next


        ' Now add all new items to listview
        ' If first time, lock listview
        If _firstItemUpdate Then Me.BeginUpdate()
        For Each z As Integer In _dicoNew.Keys

            ' Add to listview
            Dim _subItems() As ListViewItem.ListViewSubItem
            ReDim _subItems(Me.Columns.Count - 1)
            For x As Integer = 1 To _subItems.Length - 1
                _subItems(x) = New ListViewItem.ListViewSubItem
            Next
            AddItemWithStyle(z.ToString).SubItems.AddRange(_subItems)

        Next
        If _firstItemUpdate Then Me.EndUpdate()
        _dicoNew.Clear()


        ' Now refresh all subitems of the listview
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            Dim _item As cTask = _dico.Item(it.Name)
            For Each isub In it.SubItems
                isub.Text = _item.GetInformation(_columnsName(x))
                x += 1
            Next
            If _dico.Item(it.Name).IsNewItem Then
                _dico.Item(it.Name).IsNewItem = False
                it.BackColor = Me.NEW_ITEM_COLOR
            ElseIf _dico.Item(it.Name).IsKilledItem Then
                it.BackColor = Me.DELETED_ITEM_COLOR
            Else
                it.BackColor = Color.White
            End If
        Next


        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        _test = GetTickCount - _test
        Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh task list.")

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cTask).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cTask
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cTask
        Return _dico.Item(key)
    End Function

    ' Get selected items
    Public Function GetSelectedItems() As Dictionary(Of String, cTask).ValueCollection
        Dim res As New Dictionary(Of String, cTask)

        For Each it As ListViewItem In Me.SelectedItems
            res.Add(it.Name, _dico.Item(it.Name))
        Next

        Return res.Values
    End Function

    ' Choose column
    Public Sub ChooseColumns()

        Dim frm As New frmChooseProcessColumns
        frm.ShowDialog()

        ' Recreate subitem buffer and get columns name again
        Call CreateSubItemsBuffer()

        ' We have to set name to all items again
        For Each it As ListViewItem In Me.Items
            it.Name = it.Tag.ToString
        Next

        ' Refresh items
        _firstItemUpdate = True
        Me.BeginUpdate()
        Call Me.UpdateItems()
        Call Me.UpdateItems()
        Me.EndUpdate()
    End Sub


    ' ========================================
    ' Private properties
    ' ========================================

    ' Add an item (specific to type of list)
    Private Function AddItemWithStyle(ByVal key As String) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        item.Name = key

        ' Add icon
        Try
            Me.SmallImageList.Images.Add(key, _dico(key).SmallIcon)
            item.ImageKey = key
        Catch ex As Exception
            item.ImageKey = "noIcon"
        End Try

        item.Tag = key

        Return item

    End Function

    ' Create some subitems
    Private Sub CreateSubItemsBuffer()

        ' Get column names
        Dim _size As Integer = Me.Columns.Count - 1
        ReDim _columnsName(_size)
        For x As Integer = 0 To _size
            _columnsName(x) = Me.Columns.Item(x).Text.Replace("< ", "").Replace("> ", "")
        Next

    End Sub

End Class
