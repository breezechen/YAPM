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

Public Class moduleList
    Inherits customLV


    ' ========================================
    ' Private
    ' ========================================
    Private _dicoNew As New Dictionary(Of String, cModule)
    Private _dicoDel As New Dictionary(Of String, cModule)
    Private _buffDico As New Dictionary(Of String, cModule.MODULEENTRY32)
    Private _remoteSpecialDico As New Dictionary(Of String, System.Management.ManagementObject)
    Private _dico As New Dictionary(Of String, cModule)
    Private _local As Boolean = True
    Private _theProc As Management.ManagementObject
    Private _con As cRemoteProcessWMI.RemoteConnectionInfoWMI

    Private _pid As Integer

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================
    Public Property ProcessId() As Integer
        Get
            Return _pid
        End Get
        Set(ByVal value As Integer)
            _pid = value
        End Set
    End Property
    Public Property IsLocalMachine() As Boolean
        Get
            Return _local
        End Get
        Set(ByVal value As Boolean)
            _local = value
        End Set
    End Property
    Public Property RemoteConnectionWMI() As cRemoteProcessWMI.RemoteConnectionInfoWMI
        Get
            Return _con
        End Get
        Set(ByVal value As cRemoteProcessWMI.RemoteConnectionInfoWMI)
            _con = value
        End Set
    End Property
    Public Property MngObjProcess() As Management.ManagementObject
        Get
            Return _theProc
        End Get
        Set(ByVal value As Management.ManagementObject)
            _theProc = value
        End Set
    End Property

#End Region

    ' ========================================
    ' Public properties
    ' ========================================

    ' Delete all items
    Public Sub ClearItems()
        cProcess.ClearProcessDico()
        _buffDico.Clear()
        _dico.Clear()
        _dicoDel.Clear()
        _remoteSpecialDico.Clear()
        _dicoNew.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("dllIcon", My.Resources.dllIcon)
        _IMG.Images.Add("exeFile", My.Resources.application_blue)
        Me.Items.Clear()
    End Sub

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        _IMG = New ImageList
        _IMG.ImageSize = New Size(16, 16)
        _IMG.ColorDepth = ColorDepth.Depth32Bit

        Me.SmallImageList = _IMG
        _IMG.Images.Add("dllIcon", My.Resources.dllIcon)
        _IMG.Images.Add("exeFile", My.Resources.application_blue)

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
            Call cLocalModule.Enumerate(_pid, _itemId, _buffDico)
        Else
            Call cRemoteModuleWMI.Enumerate(_con, _theProc, _itemId, _buffDico, _remoteSpecialDico)
        End If

        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cModule In _dico.Values
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
            Dim _it As cModule
            If _local Then
                _it = New cLocalModule(z, _buffDico.Item(z))
            Else
                _it = New cRemoteModuleWMI(z, _buffDico.Item(z), _con, _remoteSpecialDico(z))
            End If
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
            Dim _item As cModule = _dico.Item(it.Name)
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
        Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh module list.")

        MyBase.UpdateItems()
    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cModule).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cModule
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cModule
        Return _dico.Item(key)
    End Function

    ' Get selected items
    Public Function GetSelectedItems() As Dictionary(Of String, cModule).ValueCollection
        Dim res As New Dictionary(Of String, cModule)

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

        ' Forecolor
        item.ForeColor = _foreColor

        ' Add icon
        If IsLocalMachine Then
            If InStr(key.ToLowerInvariant, "exe|") > 0 Then
                Try

                    Dim fName As String = _dico.Item(key).FilePath

                    If IO.File.Exists(fName) Then
                        Me.SmallImageList.Images.Add(fName, GetIcon(fName, True))
                        item.ImageKey = fName
                    Else
                        item.ImageKey = "dllIcon"
                        item.ForeColor = Drawing.Color.Gray
                    End If

                Catch ex As Exception
                    item.ImageKey = "dllIcon"
                    item.ForeColor = Drawing.Color.Gray
                End Try

            Else
                ' Standard dll file
                item.ImageKey = "dllIcon"
            End If
        Else
            ' Remote file -> standard icons
            If InStr(key.ToLowerInvariant, "exe|") > 0 Then
                item.ImageKey = "exeFile"
            Else
                item.ImageKey = "dllIcon"
            End If
        End If

        item.Tag = key

        Return item

    End Function

End Class
