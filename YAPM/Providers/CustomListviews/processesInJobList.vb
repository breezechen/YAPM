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

Public Class processesInJobList
    Inherits customLV

    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _firstRefresh As Boolean
    Private _first As Boolean
    Private _dico As New Dictionary(Of String, cProcess)
    Private _job As cJob

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================
    Public Property Job() As cJob
        Get
            Return _job
        End Get
        Set(ByVal value As cJob)
            _job = value
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

        _firstRefresh = True
        _first = True

        ' Create buffer
        Me.CreateSubItemsBuffer()
        AddHandler Program.Connection.Connected, AddressOf impConnected
        AddHandler Program.Connection.Disconnected, AddressOf impDisConnected
    End Sub

    ' Get an item from listview
    Public Function GetImageFromImageList(ByVal key As String) As System.Drawing.Image
        Return _IMG.Images.Item(key)
    End Function

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _firstRefresh = True
        _firstItemUpdate = True
        _dico.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("noIcon", My.Resources.application_blue16)
        Me.Items.Clear()
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
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cProcess).ValueCollection
        Dim res As New Dictionary(Of String, cProcess)

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

    ' Called when connected
    Public Sub impConnected()
        ' Nothing special here
    End Sub

    ' Called when disconnected
    Public Sub impDisConnected()
        _timeToDisplayNewItemsGreen = False
        _firstItemUpdate = True
    End Sub

    Public Overrides Sub UpdateTheItems()

        Try
            generalLvSemaphore.WaitOne()

            ' This should not be used elsewhere...
            ProcessProvider._semProcess.WaitOne()

            ' Get current processes
            Dim Dico As List(Of Integer) = Job.Infos.PidList

            ' We won't enumerate next time with all informations (included fixed infos)
            _first = False


            ' Now add all items with isKilled = true to _dicoDel dictionnary
            Dim _dicoDel As New List(Of Integer)
            Dim _dicoNew As New List(Of Integer)
            For Each z As cProcess In _dico.Values
                If z.IsKilledItem Then
                    _dicoDel.Add(z.Infos.ProcessId)
                End If
            Next


            ' Now add new items to dictionnary
            For Each pid As Integer In Dico
                ' Add to dico
                If Not (_dico.ContainsKey(pid.ToString)) Then
                    ' Add to dico
                    _dicoNew.Add(pid)
                End If
            Next


            ' Now remove deleted items from dictionnary
            For Each z As String In _dico.Keys
                If Dico.Contains(Integer.Parse(z)) = False Then
                    ' Remove from dico
                    _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
                End If
            Next


            ' Now remove all deleted items from listview and _dico
            For Each z As Integer In _dicoDel
                Me.Items.RemoveByKey(z.ToString)
                _dico.Remove(z.ToString)
            Next


            ' Merge _dico and _dicoNew
            For Each z As Integer In _dicoNew
                Dim _it As cProcess = ProcessProvider.GetProcessByIdNonThreadSafe(z)
                _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
                _dico.Add(z.ToString, _it)
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
                Dim _item As cProcess = _dico.Item(it.Name)
                Dim __info As String = Nothing
                For Each isub In it.SubItems
                    If _item.GetInformation(_columnsName(x), __info) Then
                        isub.Text = __info
                    End If
                    x += 1
                Next
                If _item.IsNewItem Then
                    _item.IsNewItem = False
                    If _timeToDisplayNewItemsGreen Then
                        it.BackColor = NEW_ITEM_COLOR
                    Else
                        it.BackColor = _item.GetBackColor
                    End If
                ElseIf _item.IsKilledItem Then
                    it.BackColor = DELETED_ITEM_COLOR
                Else
                    _timeToDisplayNewItemsGreen = True
                    it.BackColor = _item.GetBackColor
                End If
            Next

            ' Sort items
            Me.Sort()

            _firstItemUpdate = False

            'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh process list.")

            MyBase.UpdateItems()

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            ProcessProvider._semProcess.Release()
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
        Dim proc As cProcess = _dico.Item(key)
        item.Name = key

        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            If proc.Infos.ProcessId > 4 Then

                ' Add icon
                Try

                    Dim fName As String = proc.Infos.Path

                    If IO.File.Exists(fName) Then
                        Me.SmallImageList.Images.Add(fName, Common.Misc.GetIcon(fName, True))
                        item.ImageKey = fName
                    Else
                        item.ImageKey = "noIcon"
                    End If

                Catch ex As Exception
                    item.ImageKey = "noIcon"
                End Try

            Else
                item.ImageKey = "noIcon"
            End If
        Else
            item.ImageKey = "noIcon"
        End If

        item.Tag = key

        Return item

    End Function

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
