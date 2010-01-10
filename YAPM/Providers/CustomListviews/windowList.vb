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

Public Class windowList
    Inherits customLV

    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer
    Private _unNamed As Boolean
    Private _first As Boolean
    Private _dico As New Dictionary(Of String, cWindow)

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
    Public Property ShowUnNamed() As Boolean
        Get
            Return _unNamed
        End Get
        Set(ByVal value As Boolean)
            _unNamed = value
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
        _IMG.Images.Add("noIcon", My.Resources.application_blue16)

        _first = True

        ' Create buffer 
        Me.CreateSubItemsBuffer()

    End Sub

    ' Get an item from listview
    Public Function GetImageFromImageList(ByVal key As String) As System.Drawing.Image
        Return _IMG.Images.Item(key)
    End Function

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _dico.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("noIcon", My.Resources.application_blue16)
        Me.Items.Clear()
    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cWindow).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cWindow
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cWindow
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cWindow).ValueCollection
        Dim res As New Dictionary(Of String, cWindow)

        Try
            generalLvSemaphore.WaitOne()
            For Each it As ListViewItem In Me.SelectedItems
                res.Add(it.Name, _dico.Item(it.Name))
            Next
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            generalLvSemaphore.Release()
        End Try

        Return res.Values
    End Function


    ' ========================================
    ' Private properties
    ' ========================================


#Region "Update methods & callbacks"

    Public Overrides Sub UpdateTheItems()

        Try
            generalLvSemaphore.WaitOne()

            ' This should not be used elsewhere...
            WindowProvider._semWindows.WaitOne()

            ' Get current services
            Dim Dico As Dictionary(Of String, windowInfos) = WindowProvider.CurrentWindows

            ' We won't enumerate next time with all informations (included fixed infos)
            _first = False


            ' Now add all items with isKilled = true to _dicoDel dictionnary
            Dim _dicoDel As New List(Of String)
            Dim _dicoNew As New List(Of String)
            For Each z As cWindow In _dico.Values
                If z.IsKilledItem Then
                    _dicoDel.Add(z.Infos.Key)
                End If
            Next


            ' Now add new items to dictionnary
            For Each pair As System.Collections.Generic.KeyValuePair(Of String, windowInfos) In Dico
                ' Windows for ONLY one process (and also check UnNamed)
                If pair.Value.ProcessId = Me.ProcessId AndAlso Not (_dico.ContainsKey(pair.Key)) Then
                    If Me.ShowUnNamed OrElse (pair.Value.Caption IsNot Nothing AndAlso pair.Value.Caption.Length > 0) Then
                        ' Add to dico
                        _dicoNew.Add(pair.Key)
                    End If
                End If
            Next


            ' Now remove deleted items from dictionnary
            For Each z As String In _dico.Keys
                If Dico.ContainsKey(z) = False Then
                    ' Remove from dico
                    _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
                Else
                    ' If don't want to show unnamed, we remove it from list
                    If Me.ShowUnNamed = False AndAlso (_dico(z).Infos.Caption Is Nothing OrElse _dico(z).Infos.Caption.Length = 0) Then
                        _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
                    End If
                End If
            Next


            ' Now remove all deleted items from listview and _dico
            For Each z As String In _dicoDel
                Me.Items.RemoveByKey(z)
                _dico.Remove(z)
            Next


            ' Merge _dico and _dicoNew
            For Each z As String In _dicoNew
                Dim _it As New cWindow(Dico(z))
                _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
                _dico.Add(z, _it)
            Next


            ' Create buffer if necessary
            If _columnsName.Length = 0 Then
                Me.CreateSubItemsBuffer()
            End If


            ' Now add all new items to listview
            ' If first time, lock listview
            If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.BeginUpdate()
            For Each z As String In _dicoNew

                ' Add to listview
                Dim _subItems() As String
                ReDim _subItems(Me.Columns.Count - 1)
                For x As Integer = 1 To _subItems.Length - 1
                    _subItems(x) = ""
                Next
                AddItemWithStyle(z).SubItems.AddRange(_subItems)
            Next
            If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.EndUpdate()


            ' Now refresh all subitems of the listview
            Dim isub As ListViewItem.ListViewSubItem
            Dim it As ListViewItem
            For Each it In Me.Items
                Dim x As Integer = 0
                Dim _item As cWindow = _dico.Item(it.Name)

                ' Refresh non-fixed infos
                If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
                    _item.Refresh()
                Else
                    If Dico.ContainsKey(it.Name) Then
                        _item.Merge(Dico.Item(it.Name))
                    End If
                End If

                Dim __info As String = Nothing
                For Each isub In it.SubItems
                    If _item.GetInformation(_columnsName(x), __info) Then
                        isub.Text = __info
                    End If
                    x += 1
                Next
                If _item.IsNewItem Then
                    _item.IsNewItem = False
                    it.BackColor = NEW_ITEM_COLOR
                ElseIf _item.IsKilledItem Then
                    it.BackColor = DELETED_ITEM_COLOR
                Else
                    it.BackColor = _item.GetBackColor
                End If
            Next

            ' Sort items
            Me.Sort()

            _firstItemUpdate = False

            'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh windows list.")

            MyBase.UpdateItems()

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            WindowProvider._semWindows.Release()
            generalLvSemaphore.Release()
        End Try

    End Sub

    ' Force item refreshing
    Public Overrides Sub ForceRefreshingOfAllItems()    ' Always called in a safe protected context
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            If _dico.ContainsKey(it.Name) Then
                Dim _item As cGeneralObject = _dico.Item(it.Name)
                For Each isub In it.SubItems
                    _item.GetInformation(_columnsName(x), isub.Text)
                    x += 1
                Next
                If _item.IsNewItem Then
                    _item.IsNewItem = False
                    it.BackColor = NEW_ITEM_COLOR
                ElseIf _item.IsKilledItem Then
                    it.BackColor = DELETED_ITEM_COLOR
                Else
                    it.BackColor = Color.White
                End If
            End If
        Next
    End Sub

    ' Add an item (specific to type of list)
    Friend Overrides Function AddItemWithStyle(ByVal key As String) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        item.Name = key
        'item.Group = Me.Groups(0)

        ' Add icon
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            Try
                Dim icon As System.Drawing.Icon = _dico(key).SmallIcon
                If icon IsNot Nothing Then
                    Me.SmallImageList.Images.Add(key, icon)
                    item.ImageKey = key
                Else
                    item.ImageKey = "noIcon"
                End If
            Catch ex As Exception
                item.ImageKey = "noIcon"
            End Try
        Else
            item.ImageKey = "noIcon"
        End If

        item.Tag = key

        Return item

    End Function

#End Region


    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.Shift AndAlso e.Control Then
            ' OK, show thread management
            For Each obj As cGeneralObject In Me.GetSelectedItems
                Dim frm As New frmPendingTasks(obj)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        ElseIf e.KeyCode = Keys.F7 Then
            Me.showObjectProperties()
        End If
    End Sub

    ' Display properties form
    Protected Overrides Sub OnMouseDoubleClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        If Me.ShowObjectDetailsOnDoubleClick Then
           Me.showObjectProperties()
        End If
    End Sub

    Private Sub showObjectProperties()
        For Each obj As cGeneralObject In Me.GetSelectedItems
            Dim frm As New frmObjDetails
            frm.TopMost = _frmMain.TopMost
            frm.TheObject = obj
            frm.Show()
        Next
    End Sub

End Class
