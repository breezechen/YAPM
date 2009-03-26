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

Public Class processList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cProcess)
    Public Event ItemDeleted(ByRef item As cProcess)


    ' ========================================
    ' Private
    ' ========================================
    Private _dicoNew As New Dictionary(Of String, cProcess)
    Private _dicoDel As New Dictionary(Of String, cProcess)
    Private _buffDico As New Dictionary(Of String, cProcess.LightProcess)
    Private _remoteSpecialDico As New Dictionary(Of String, System.Management.ManagementObject)
    Private _dico As New Dictionary(Of String, cProcess)
    Private _local As Boolean = True
    Private _con As cRemoteProcess.RemoteConnectionInfo

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================
    Public Property IsLocalMachine() As Boolean
        Get
            Return _local
        End Get
        Set(ByVal value As Boolean)
            _local = value
        End Set
    End Property
    Public Property RemoteConnection() As cRemoteProcess.RemoteConnectionInfo
        Get
            Return _con
        End Get
        Set(ByVal value As cRemoteProcess.RemoteConnectionInfo)
            _con = value
        End Set
    End Property

#End Region

    ' ========================================
    ' Public functions
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

    ' Get an item from listview
    Public Function GetImageFromImageList(ByVal key As String) As System.Drawing.Image
        Return _IMG.Images.Item(key)
    End Function

    ' Delete all items
    Public Sub ClearItems()
        cProcess.ClearProcessDico()
        _buffDico.Clear()
        _dico.Clear()
        _dicoDel.Clear()
        _remoteSpecialDico.Clear()
        _dicoNew.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("noIcon", My.Resources.application_blue)
        Me.Items.Clear()
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
        If _local Then
            Call cLocalProcess.Enumerate(_itemId, _buffDico)
        Else
            Call cRemoteProcess.Enumerate(_con, _itemId, _buffDico, _remoteSpecialDico)
        End If


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cProcess In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Pid.ToString, Nothing)
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
            RaiseEvent ItemDeleted(_dico.Item(z))
            _dico.Remove(z)
            cProcess.UnAssociatePidAndName(z)    ' Remove from global dico
        Next
        _dicoDel.Clear()


        ' Merge _dico and _dicoNew
        For Each z As String In _dicoNew.Keys
            Dim _it As cProcess
            If _local Then
                _it = New cLocalProcess(_buffDico.Item(z))
            Else
                _it = New cRemoteProcess(_buffDico.Item(z), _con)
            End If
            RaiseEvent ItemAdded(_it)
            _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
            _dico.Add(z.ToString, _it)
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

            '' ----------------------
            '' Specific to a process
            'Dim cP As cProcess = _dico(z)
            'If cP.ProcessorCount < 1 Then
            '    cP.ProcessorCount = frmMain.cInfo.ProcessorCount
            'End If
            '' ----------------------
        Next
        If _firstItemUpdate Then Me.EndUpdate()
        _dicoNew.Clear()


        ' Now refresh all subitems of the listview
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            Dim _item As cProcess = _dico.Item(it.Name)
            If _local Then
                _item.Refresh()
            Else
                _item.Refresh(_remoteSpecialDico(it.Name))
            End If
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
        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh process list.")

        MyBase.UpdateItems()
    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cProcess).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cProcess
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cProcess
        Return _dico.Item(key)
    End Function

    ' Get selected items
    Public Function GetSelectedItems() As Dictionary(Of String, cProcess).ValueCollection
        Dim res As New Dictionary(Of String, cProcess)

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
        Dim proc As cProcess = _dico.Item(key)
        item.Name = key

        ' Add to global dico
        cProcess.AssociatePidAndName(key, _buffDico.Item(key).name)

        If proc.Pid > 4 Then

            ' Forecolor
            item.ForeColor = _foreColor

            ' Add icon
            Try

                Dim fName As String = proc.Path

                If IO.File.Exists(fName) Then
                    Me.SmallImageList.Images.Add(fName, GetIcon(fName, True))
                    item.ImageKey = fName
                Else
                    item.ImageKey = "noIcon"
                    item.ForeColor = Drawing.Color.Gray
                End If

            Catch ex As Exception
                item.ImageKey = "noIcon"
                item.ForeColor = Drawing.Color.Gray
            End Try

        Else
            item.ImageKey = "noIcon"
            item.ForeColor = Drawing.Color.Gray
        End If

        item.Tag = key

        Return item

    End Function

End Class
