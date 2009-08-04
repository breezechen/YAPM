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

Public Class logList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cLogItem)
    Public Event ItemDeleted(ByRef item As cLogItem)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer
    Private _first As Boolean
    Private _capture As asyncCallbackLogEnumerate.LogItemType = asyncCallbackLogEnumerate.LogItemType.AllItems
    Private _display As asyncCallbackLogEnumerate.LogItemType = asyncCallbackLogEnumerate.LogItemType.AllItems
    Private _dico As New Dictionary(Of String, cLogItem)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _logConnection As New cLogConnection(Me, _connectionObject, New cLogConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEventHandler))

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
    Public Property CaptureItems() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _capture
        End Get
        Set(ByVal value As asyncCallbackLogEnumerate.LogItemType)
            _capture = value
        End Set
    End Property
    Public Property DisplayItems() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _display
        End Get
        Set(ByVal value As asyncCallbackLogEnumerate.LogItemType)
            _display = value
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
        _logConnection.Disconnected = New cLogConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _logConnection.Connected = New cLogConnection.ConnectedEventHandler(AddressOf HasConnected)
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _dico.Clear()
        Me.Items.Clear()
    End Sub

    ' Call this to update items in listview
    Public Overrides Sub UpdateItems()

        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If

        If _logConnection.IsConnected Then

            ' Now enumerate items
            _logConnection.Enumerate(_capture, _pid)

        End If

    End Sub

    ' Add IF NECESSARY to listview
    Private Sub conditionalAdd(ByVal item As cLogItem)
        Dim b As Boolean = False
        If item.Infos.State = logItemInfos.CREATED_OR_DELETED.created Then
            b = ((_display And asyncCallbackLogEnumerate.LogItemType.CreatedItems) = asyncCallbackLogEnumerate.LogItemType.CreatedItems)
        Else
            b = ((_display And asyncCallbackLogEnumerate.LogItemType.DeletedItems) = asyncCallbackLogEnumerate.LogItemType.DeletedItems)
        End If
        If ((item.Infos.TypeMask And _display) = item.Infos.TypeMask) AndAlso b Then

            ' Add to listview
            Dim x As Integer
            Dim _subItems() As ListViewItem.ListViewSubItem
            ReDim _subItems(Me.Columns.Count - 1)
            For x = 1 To _subItems.Length - 1
                _subItems(x) = New ListViewItem.ListViewSubItem
            Next
            Dim it As ListViewItem = AddItemWithStyle(item.Infos.Key)
            it.SubItems.AddRange(_subItems)

            ' Refresh subitems
            x = 0
            For Each isub As ListViewItem.ListViewSubItem In it.SubItems
                isub.Text = item.GetInformation(_columnsName(x))
                x += 1
            Next

        End If
    End Sub

    ' Call this to redraw all items
    Public Sub ReAddItems()

        sem.WaitOne()
        Me.BeginUpdate()
        Me.Items.Clear()

        For Each pair As System.Collections.Generic.KeyValuePair(Of String, cLogItem) In _dico
            Call conditionalAdd(pair.Value)
        Next


        ' The fuck*ing BUG -> need to refresh all subitems
        For Each it As ListViewItem In Me.Items
            For Each isub As ListViewItem.ListViewSubItem In it.SubItems
                isub.Text = isub.Text
            Next
        Next

        Me.EndUpdate()
        sem.Release()

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cLogItem).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cLogItem
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cLogItem
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cLogItem).ValueCollection
        Dim res As New Dictionary(Of String, cLogItem)

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
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, logItemInfos), ByVal errorMessage As String, ByVal InstanceId As Integer)

        sem.WaitOne()

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Log enumeration", errorMessage)
            sem.Release()
            Exit Sub
        End If

        ' We won't enumerate next time with all informations (included fixed infos)
        _first = False


        '' Now add all items with isKilled = true to _dicoDel dictionnary
        'For Each z As cLogItem In _dico.Values
        '    If z.IsKilledItem Then
        '        _dicoDel.Add(z.Infos.BaseAddress.ToString, Nothing)
        '    End If
        'Next


        ' Now add new items to dictionnary
        Dim _dicoNew As New Dictionary(Of String, cLogItem)
        For Each pair As System.Collections.Generic.KeyValuePair(Of String, logItemInfos) In Dico
            If Not (_dico.ContainsKey(pair.Key)) Then
                ' Add to dico
                _dicoNew.Add(pair.Key, New cLogItem(pair.Value))
                _dico.Add(pair.Key, _dicoNew(pair.Key))
            End If
        Next


        '' Now remove deleted items from dictionnary
        'For Each z As String In _dico.Keys
        '    If Dico.ContainsKey(z) = False Then
        '        ' Remove from dico
        '        _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
        '    End If
        'Next


        '' Now remove all deleted items from listview and _dico
        'For Each z As String In _dicoDel.Keys
        '    Me.Items.RemoveByKey(z)
        '    RaiseEvent ItemDeleted(_dico.Item(z))
        '    _dico.Remove(z)
        'Next
        '_dicoDel.Clear()


        '' Merge _dico and _dicoNew
        'For Each z As String In _dicoNew.Keys
        '    Dim _it As cLogItem = _dicoNew.Item(z)
        '    RaiseEvent ItemAdded(_it)
        '    _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
        '    _dico.Add(z.ToString, _it)
        'Next


        ' Now add all new items to listview
        ' If first time, lock listview
        'If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.BeginUpdate()
        For Each z As String In _dicoNew.Keys
            Call conditionalAdd(_dicoNew(z))
        Next
        'If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE orelse _dicodel.count>EMPIRIC_MINIMAL_NUMBER_OF_deleted_ITEMS_TO_BEGIN_UPDATE Then Me.EndUpdate()
        '_dicoNew.Clear()


        ' Now refresh all subitems of the listview
        'Dim isub As ListViewItem.ListViewSubItem
        'Dim it As ListViewItem
        'For Each it In Me.Items
        '    Dim x As Integer = 0
        '    Dim _item As cLogItem = _dico.Item(it.Name)
        '    'If Dico.ContainsKey(it.Name) Then
        '    '    _item.Merge(Dico.Item(it.Name))
        '    'End If
        '    For Each isub In it.SubItems
        '        isub.Text = _item.GetInformation(_columnsName(x))
        '        x += 1
        '    Next
        '    'If _item.IsNewItem Then
        '    '    _item.IsNewItem = False
        '    '    it.BackColor = NEW_ITEM_COLOR
        '    'ElseIf _item.IsKilledItem Then
        '    '    it.BackColor = DELETED_ITEM_COLOR
        '    'Else
        '    '    it.BackColor = Color.White
        '    'End If
        'Next

        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh log list.")

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
        _logConnection.ConnectionObj = _connectionObject
        _logConnection.Connect()
        cLogItem.Connection = _logConnection
    End Sub

    Private Sub Disconnect()
        _logConnection.Disconnect()
    End Sub

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_logConnection.Disconnect()
        '     _logConnection.Con()
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
