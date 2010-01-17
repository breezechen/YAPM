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

    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _first As Boolean
    Private _capture As Native.Api.Enums.LogItemType = Native.Api.Enums.LogItemType.AllItems
    Private _display As Native.Api.Enums.LogItemType = Native.Api.Enums.LogItemType.AllItems
    Private _dico As New Dictionary(Of String, cLogItem)

#Region "Properties"

    ' ========================================
    ' Properties
    ' ========================================
    Public Property CaptureItems() As Native.Api.Enums.LogItemType
        Get
            Return _capture
        End Get
        Set(ByVal value As Native.Api.Enums.LogItemType)
            _capture = value
        End Set
    End Property
    Public Property DisplayItems() As Native.Api.Enums.LogItemType
        Get
            Return _display
        End Get
        Set(ByVal value As Native.Api.Enums.LogItemType)
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

        ' Create buffer 
        Me.CreateSubItemsBuffer()

        ' Set handlers
        AddHandler LogProvider.GotRefreshed, AddressOf Me.GotRefreshed  ' We will add/remove/refresh using this handler
        AddHandler Program.Connection.Connected, AddressOf impConnected
        AddHandler Program.Connection.Disconnected, AddressOf impDisConnected
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _dico.Clear()
        Me.Items.Clear()
    End Sub

    ' Add IF NECESSARY to listview
    ' No protection by sem here cause of safe context when called
    Private Sub conditionalAdd(ByVal item As cLogItem)
        Try
            Dim b As Boolean = False
            If item.Infos.State = logItemInfos.CreatedOrDeleted.Created Then
                b = ((_display And Native.Api.Enums.LogItemType.CreatedItems) = Native.Api.Enums.LogItemType.CreatedItems)
            Else
                b = ((_display And Native.Api.Enums.LogItemType.DeletedItems) = Native.Api.Enums.LogItemType.DeletedItems)
            End If
            If ((item.Infos.TypeMask And _display) = item.Infos.TypeMask) AndAlso b Then

                ' Add to listview
                Dim x As Integer
                Dim _subItems() As String
                ReDim _subItems(Me.Columns.Count - 1)
                For x = 1 To _subItems.Length - 1
                    _subItems(x) = ""
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
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    ' Clear log
    Public Sub ClearLog()
        Try
            generalLvSemaphore.WaitOne()
            Me.BeginUpdate()
            Me.Items.Clear()
            _firstItemUpdate = True
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            generalLvSemaphore.Release()
            Me.EndUpdate()
        End Try
    End Sub

    ' Call this to redraw all items
    Public Sub ReAddItems()

        Try
            generalLvSemaphore.WaitOne()
            Me.BeginUpdate()
            Me.Items.Clear()
            _firstItemUpdate = True

            For Each pair As System.Collections.Generic.KeyValuePair(Of String, cLogItem) In _dico
                Call conditionalAdd(pair.Value)
            Next

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            generalLvSemaphore.Release()
            Me.EndUpdate()
        End Try

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

    ' Called when connected
    Public Sub impConnected()
        ' Nothing special here
    End Sub

    ' Called when disconnected
    Public Sub impDisConnected()
        _timeToDisplayNewItemsGreen = False
        _firstItemUpdate = True
    End Sub

    ' GotNewProcesses, GotDeletedProcesses and GotRefreshed are ALWAYS called
    ' sequentially, and are protected by a semaphore -> no need to reprotect it here
    Private Delegate Sub degGotNewItems(ByVal news As List(Of String), ByVal newItems As Dictionary(Of String, logItemInfos))
    Private Sub GotNewItems(ByVal news As List(Of String), ByVal newItems As Dictionary(Of String, logItemInfos))

        ' Lock lv if necesary
        Dim _hasToLock As Boolean = (_firstItemUpdate OrElse newItems.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE)
        If _hasToLock Then
            Me.BeginUpdate()
        End If
        For Each var As String In news
            If _dico.ContainsKey(var) = False Then

                ' Don't add the first time (as there are all items)
                If _firstItemUpdate = False Then
                    Dim envvar As New cLogItem(newItems(var))
                    _dico.Add(var, envvar)

                    Call conditionalAdd(envvar)
                End If

            End If
        Next

        ' Unlock lv if necesary
        If _hasToLock Then
            Me.EndUpdate()
        End If

    End Sub

    Private Delegate Sub degGotRefreshed(ByVal _dicoNew As List(Of String), ByVal _dicoDels As Dictionary(Of String, logItemInfos), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Private Sub GotRefreshed(ByVal _dicoNew As List(Of String), ByVal _dicoDels As Dictionary(Of String, logItemInfos), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
        ' Have to call a delegate as will refresh the listview
        ' Only update if this is the good instanceId
        If instanceId = Me.InstanceId Then
            If Me.InvokeRequired Then
                Dim d As New degGotRefreshed(AddressOf GotRefreshed)
                Try
                    Me.Invoke(d, _dicoNew, _dicoDels, _dicoDel, Dico, instanceId, res)
                Catch ex As Exception
                    ' Won't catch this...
                End Try
            Else

                If res.Success Then

                    ' Create buffer if necessary
                    If _columnsName.Length = 0 Then
                        Me.CreateSubItemsBuffer()
                    End If

                    ' NEW ITEMS (created items)
                    If _dicoNew.Count > 0 Then
                        ' Set items as created
                        For Each it As String In _dicoNew
                            Dico(it).State = logItemInfos.CreatedOrDeleted.Created
                        Next
                        Me.GotNewItems(_dicoNew, Dico)
                    End If

                    ' DELETED ITEMS
                    If _dicoDel.Count > 0 Then
                        ' Deleted items are ADDED to the list :-)
                        ' CreatedOrDeleted.Deleted is the default value
                        Me.GotNewItems(_dicoDel, _dicoDels)
                    End If


                    ' Sort items
                    Me.Sort()

                    _firstItemUpdate = False

                Else
                    RaiseEvent GotAnError("Log enumeration", res.ErrorMessage)
                End If
            End If
        End If
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
        item.ForeColor = _foreColor
        item.Tag = key
        'item.Group = Me.Groups(0)

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
