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

Public Class handleList
    Inherits YAPM.DoubleBufferedLV

    Private Declare Function GetTickCount Lib "kernel32" () As Integer


    ' ========================================
    ' Private
    ' ========================================
    Private _dicoNew As New Dictionary(Of String, cHandle)
    Private _dicoDel As New Dictionary(Of String, cHandle)
    Private _buffDico As New Dictionary(Of String, cHandle.LightHandle)
    Private _dico As New Dictionary(Of String, cHandle)

    Private _firstItemUpdate As Boolean = True
    Private _columnsName() As String
    Private _unnamed As Boolean = False

    Private _pid As Integer()
    Private _IMG As ImageList
    Private m_SortingColumn As ColumnHeader

    Private _foreColor As Color = Color.FromArgb(30, 30, 30)

    Private NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Private DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================
    Public Property ProcessId() As Integer()
        Get
            Return _pid
        End Get
        Set(ByVal value As Integer())
            _pid = value
        End Set
    End Property
    Public Property ShowUnNamed() As Boolean
        Get
            Return _unnamed
        End Get
        Set(ByVal value As Boolean)
            _unnamed = value
        End Set
    End Property

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
        _IMG.Images.Add("key", My.Resources.key)
        _IMG.Images.Add("thread", My.Resources.thread)
        _IMG.Images.Add("service", My.Resources.gear)
        _IMG.Images.Add("noicon", My.Resources.application_blue)

    End Sub

    ' Call this to update items in listview
    Public Sub UpdateItems()

        Dim _test As Integer = GetTickCount


        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If


        ' Now enumerate items
        Dim _itemId() As String
        ReDim _itemId(0)
        Call cHandle.Enumerate(_pid, _unnamed, _itemId, _buffDico)


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cHandle In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Key.ToString, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each z As String In _itemId
            If Not (_dico.ContainsKey(z)) Then
                ' Add to dico
                _dicoNew.Add(z.ToString, Nothing)
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
        For Each z As String In _dicoNew.Keys
            Dim _it As cHandle = New cHandle(z, _buffDico.Item(z))
            _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
            _dico.Add(z, _it)
        Next


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
            Dim _item As cHandle = _dico.Item(it.Name)
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
        Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh handle list.")

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cHandle).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cHandle
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cHandle
        Return _dico.Item(key)
    End Function

    ' Get selected items
    Public Function GetSelectedItems() As Dictionary(Of String, cHandle).ValueCollection
        Dim res As New Dictionary(Of String, cHandle)

        For Each it As ListViewItem In Me.SelectedItems
            res.Add(it.Name, _dico.Item(it.Name))
        Next

        Return res.Values
    End Function

    ' Choose column
    Public Sub ChooseColumns()

        Dim frm As New frmChooseColumns
        frm.ConcernedListView = Me
        frm.ShowDialog()

        ' Recreate subitem buffer and get columns name again
        Call CreateSubItemsBuffer()

        If Me.Items.Count = 0 Then
            Exit Sub
        End If

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
        item.ForeColor = _foreColor

        ' Icon
        Select Case _dico.Item(key).Type
            Case "Key"
                item.ImageKey = "key"
            Case "File", "Directory"
                ' Have to retrieve the icon of file/directory
                Dim fName As String = _dico.Item(key).Name
                If IO.File.Exists(fName) Or IO.Directory.Exists(fName) Then
                    Dim img As System.Drawing.Icon = GetIcon2(fName, True)
                    If img IsNot Nothing Then
                        Me.SmallImageList.Images.Add(fName, img)
                        item.ImageKey = fName
                    Else
                        item.ImageKey = "noicon"
                    End If
                Else
                    item.ImageKey = "noicon"
                End If
            Case "Thread", "Process"
                item.ImageKey = "thread"
            Case Else
                item.ImageKey = "service"
        End Select

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
