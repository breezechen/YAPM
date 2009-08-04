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

Public Class memoryList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cMemRegion)
    Public Event ItemDeleted(ByRef item As cMemRegion)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer
    Private _first As Boolean
    Private _dicoNew As New Dictionary(Of String, cMemRegion)
    Private _dicoDel As New Dictionary(Of String, cMemRegion)
    Private _buffDico As New Dictionary(Of String, cMemRegion)
    Private _dico As New Dictionary(Of String, cMemRegion)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _memregionConnection As New cMemRegionConnection(Me, _connectionObject, New cMemRegionConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEventHandler))

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================
    Public Property ConnectionObj() As cConnection
        Get
            Return _connectionObject
        End Get
        Set(ByVal value As cConnection)
            _connectionObject = value
        End Set
    End Property
    Public Property ProcessId() As Integer
        Get
            Return _pid
        End Get
        Set(ByVal value As Integer)
            _pid = value
        End Set
    End Property

#End Region

    ' ========================================
    ' Public functions
    ' ========================================

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        _first = True

        ' Set handlers
        _memregionConnection.Disconnected = New cMemRegionConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _memregionConnection.Connected = New cMemRegionConnection.ConnectedEventHandler(AddressOf HasConnected)
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _buffDico.Clear()
        _dico.Clear()
        _dicoDel.Clear()
        _dicoNew.Clear()
        Me.Items.Clear()
    End Sub

    ' Call this to update items in listview
    Public Overrides Sub UpdateItems()

        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If

        If _memregionConnection.IsConnected Then

            ' Now enumerate items
            _memregionConnection.Enumerate(_first, _pid)

        End If

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cMemRegion).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cMemRegion
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cMemRegion
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cMemRegion).ValueCollection
        Dim res As New Dictionary(Of String, cMemRegion)

        For Each it As ListViewItem In Me.SelectedItems
            res.Add(it.Name, _dico.Item(it.Name))
        Next

        Return res.Values
    End Function


    ' ========================================
    ' Private properties
    ' ========================================

    ' Executed when enumeration is done
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, memRegionInfos), ByVal errorMessage As String, ByVal InstanceId As Integer)

        sem.WaitOne()

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Mem region enumeration", errorMessage)
            sem.Release()
            Exit Sub
        End If

        ' We won't enumerate next time with all informations (included fixed infos)
        _first = False


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cMemRegion In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Infos.BaseAddress.ToString, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each pair As System.Collections.Generic.KeyValuePair(Of String, memRegionInfos) In Dico
            If Not (_dico.ContainsKey(pair.Key)) Then
                ' Add to dico
                _dicoNew.Add(pair.Key, New cMemRegion(pair.Value))
            End If

        Next


        ' Now remove deleted items from dictionnary
        For Each z As String In _dico.Keys
            If Dico.ContainsKey(z) = False Then
                ' Remove from dico
                _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
            End If
        Next


        ' Now remove all deleted items from listview and _dico
        For Each z As String In _dicoDel.Keys
            Me.Items.RemoveByKey(z)
            RaiseEvent ItemDeleted(_dico.Item(z))
            _dico.Remove(z)
        Next
        _dicoDel.Clear()


        ' Merge _dico and _dicoNew
        For Each z As String In _dicoNew.Keys
            Dim _it As cMemRegion = _dicoNew.Item(z)
            RaiseEvent ItemAdded(_it)
            _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
            _dico.Add(z.ToString, _it)
        Next


        ' Now add all new items to listview
        ' If first time, lock listview
        If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.BeginUpdate()
        For Each z As String In _dicoNew.Keys

            ' Add to listview
            Dim _subItems() As String
            ReDim _subItems(Me.Columns.Count - 1)
            For x As Integer = 1 To _subItems.Length - 1
                _subItems(x) = ""
            Next
            AddItemWithStyle(z).SubItems.AddRange(_subItems)
        Next
        If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.EndUpdate()
        _dicoNew.Clear()


        ' Now refresh all subitems of the listview
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            Dim _item As cMemRegion = _dico.Item(it.Name)
            If Dico.ContainsKey(it.Name) Then
                _item.Merge(Dico.Item(it.Name))
            End If
            If _item.ItemHasChanged Then
                For Each isub In it.SubItems
                    isub.Text = _item.GetInformation(_columnsName(x))
                    x += 1
                Next
            End If
            If _item.IsNewItem Then
                _item.IsNewItem = False
                it.BackColor = NEW_ITEM_COLOR
            ElseIf _item.IsKilledItem Then
                it.BackColor = DELETED_ITEM_COLOR
            Else
                it.BackColor = Color.White
            End If
        Next

        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh thread list.")

        MyBase.UpdateItems()

        sem.Release()

    End Sub


    ' Add an item (specific to type of list)
    Friend Overrides Function AddItemWithStyle(ByVal key As String) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        item.Name = key
        item.ForeColor = _foreColor
        item.Tag = key
        'item.Group = Me.Groups(0)

        Return item

    End Function

#Region "Connection stuffs"

    Private Sub _connectionObject_Connected() Handles _connectionObject.Connected
        Call Connect()
    End Sub

    Private Sub _connectionObject_Disconnected() Handles _connectionObject.Disconnected
        Call Disconnect()
    End Sub

    Private Sub Connect()
        _first = True
        _memregionConnection.ConnectionObj = _connectionObject
        _memregionConnection.Connect()
        cMemRegion.Connection = _memregionConnection
    End Sub

    Private Sub Disconnect()
        _memregionConnection.Disconnect()
    End Sub

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_memregionConnection.Disconnect()
        '     _memregionConnection.Con()
    End Sub

    Private Sub HasConnected(ByVal Success As Boolean)
        '
    End Sub

#End Region

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
