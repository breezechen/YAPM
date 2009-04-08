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
    Inherits customLV


    ' ========================================
    ' Private
    ' ========================================
    Private _dicoNew As New Dictionary(Of String, cTask)
    Private _dicoDel As New Dictionary(Of String, cTask)
    Private _buffDico As New Dictionary(Of String, windowInfos.LightWindow)
    Private _dico As New Dictionary(Of String, cTask)


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
    Public Overrides Sub UpdateItems()

        Dim _test As Integer = GetTickCount


        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If


        ' Now enumerate items
        Dim _itemId() As String
        ReDim _itemId(0)
        Call cTask.Enumerate(_itemId, _buffDico)


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cTask In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Key, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each z As String In _itemId
            If Not (_dico.ContainsKey(z)) Then
                ' Add to dico
                _dicoNew.Add(z, Nothing)
            End If
        Next


        ' Now remove deleted items from dictionnary
        For Each z As String In _dico.Keys
            If Array.IndexOf(_itemId, z) < 0 Then
                ' Remove from dico
                _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
            End If
        Next


        ' Now remove all deleted items from listview and _dico
        For Each z As String In _dicoDel.Keys
            Me.Items.RemoveByKey(z)
            _dico.Remove(z)
        Next
        _dicoDel.Clear()


        ' Merge _dico and _dicoNew
        Dim z0 As String = ""
        Try
            For Each z0 In _dicoNew.Keys
                Dim _it As cTask = New cTask(_buffDico.Item(z0))
                _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
                _dico.Add(z0, _it)
            Next
        Catch ex As Exception
            Dim s As String = "Key : " & z0 & vbNewLine & "New count : " & _dicoNew.Keys.Count.ToString & vbNewLine & "Buff count : " & _buffDico.Keys.Count.ToString
            MessageBox.Show(ex.Message, "Error in task refresh", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign, False)
        End Try


        ' Now add all new items to listview
        ' If first time, lock listview
        If _firstItemUpdate Then Me.BeginUpdate()
        For Each z As String In _dicoNew.Keys

            ' Add to listview
            Dim _subItems() As ListViewItem.ListViewSubItem
            ReDim _subItems(Me.Columns.Count - 1)
            For x As Integer = 1 To _subItems.Length - 1
                _subItems(x) = New ListViewItem.ListViewSubItem
            Next
            AddItemWithStyle(z).SubItems.AddRange(_subItems)

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
                it.BackColor = NEW_ITEM_COLOR
            ElseIf _dico.Item(it.Name).IsKilledItem Then
                it.BackColor = DELETED_ITEM_COLOR
            Else
                it.BackColor = Color.White
            End If
        Next

        ' This piece of code is needed. Strange behavior, the Text attribute must
        ' be set twice to be properly displayed.
        If _firstItemUpdate Then
            For Each it In Me.Items
                For Each isub In it.SubItems
                    isub.Text = isub.Text
                Next
            Next
        End If


        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        _test = GetTickCount - _test
        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh task list.")

        MyBase.UpdateItems()
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
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cTask).ValueCollection
        Dim res As New Dictionary(Of String, cTask)

        For Each it As ListViewItem In Me.SelectedItems
            res.Add(it.Name, _dico.Item(it.Name))
        Next

        Return res.Values
    End Function


    ' ========================================
    ' Private properties
    ' ========================================

    ' Add an item (specific to type of list)
    Friend Overrides Function AddItemWithStyle(ByVal key As String) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        item.Name = key
        item.Group = Me.Groups(0)

        ' Add icon
        Try
            Dim i As System.Drawing.Icon = _dico(key).SmallIcon
            If i IsNot Nothing Then
                Me.SmallImageList.Images.Add(key, i)
                item.ImageKey = key
            Else
                'catch ex As Exception
                item.ImageKey = "noIcon"
            End If
        Catch ex As Exception
            item.ImageKey = "noIcon"
        End Try

        item.Tag = key

        Return item

    End Function

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.Shift AndAlso e.Control Then
            ' OK, show thread management
            For Each obj As cGeneralObject In Me.GetSelectedItems
                Dim frm As New frmPendingTasks(obj)
                frm.Show()
            Next
        End If
    End Sub
End Class
