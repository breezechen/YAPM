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

Public Class envVariableList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cEnvVariable)
    Public Event ItemDeleted(ByRef item As cEnvVariable)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer
    Private _first As Boolean
    Private _dico As New Dictionary(Of String, cEnvVariable)

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
        AddHandler EnvVariableProvider.GotRefreshed, AddressOf Me.GotRefreshed  ' We will add/remove/refresh using this handler
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _dico.Clear()
        Me.Items.Clear()
    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cEnvVariable).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cEnvVariable
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cEnvVariable
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cEnvVariable).ValueCollection
        Dim res As New Dictionary(Of String, cEnvVariable)

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

    ' GotNewProcesses, GotDeletedProcesses and GotRefreshed are ALWAYS called
    ' sequentially, and are protected by a semaphore -> no need to reprotect it here
    Private Delegate Sub degGotNewItems(ByVal vars As List(Of String), ByVal newItems As Dictionary(Of String, envVariableInfos))
    Private Sub GotNewItems(ByVal vars As List(Of String), ByVal newItems As Dictionary(Of String, envVariableInfos))

        ' Lock lv if necesary
        Dim _hasToLock As Boolean = (_firstItemUpdate OrElse newItems.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE)
        If _hasToLock Then
            Me.BeginUpdate()
        End If
        For Each var As String In vars
            If _dico.ContainsKey(var) = False Then
                Dim envvar As New cEnvVariable(newItems(var))
                envvar.NewCount = 3
                _dico.Add(var, envvar)

                ' Add new item to lv
                Dim _subItems() As String
                ReDim _subItems(Me.Columns.Count - 1)
                For x As Integer = 1 To _subItems.Length - 1
                    _subItems(x) = ""
                Next
                AddItemWithStyle(var).SubItems.AddRange(_subItems)
            End If
        Next

        ' Unlock lv if necesary
        If _hasToLock Then
            Me.EndUpdate()
        End If

    End Sub
    Private Sub GotDeletedItems(ByVal vars As List(Of String))
        For Each var As String In vars
            Dim cv As cEnvVariable = Nothing
            If _dico.ContainsKey(var) Then
                cv = _dico(var)
                cv.KillCount = 3
            End If
        Next
    End Sub

    Private Delegate Sub degGotRefreshed(ByVal _dicoNew As List(Of String), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, envVariableInfos), ByVal instanceId As Integer)
    Private Sub GotRefreshed(ByVal _dicoNew As List(Of String), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, envVariableInfos), ByVal instanceId As Integer)
        ' Have to call a delegate as will refresh the listview
        ' Only update if this is the good instanceId
        If instanceId = Me.InstanceId Then
            If Me.InvokeRequired Then
                Dim d As New degGotRefreshed(AddressOf GotRefreshed)
                Try
                    Me.Invoke(d, _dicoNew, _dicoDel, Dico, instanceId)
                Catch ex As Exception
                    ' Won't catch this...
                End Try
            Else

                ' Create buffer if necessary
                If _columnsName.Length = 0 Then
                    Me.CreateSubItemsBuffer()
                End If

                ' DELETED ITEMS
                If _dicoDel.Count > 0 Then
                    Me.GotDeletedItems(_dicoDel)
                End If

                ' NEW ITEMS
                If _dicoNew.Count > 0 Then
                    Me.GotNewItems(_dicoNew, Dico)
                End If

                ' We won't enumerate next time with all informations (included fixed infos)
                _first = False

                Try

                    Dim toDel As New List(Of String)   ' Keys of items to remove

                    ' Now refresh all subitems of the listview
                    Dim isub As ListViewItem.ListViewSubItem
                    Dim it As ListViewItem
                    For Each it In Me.Items
                        Dim x As Integer = 0
                        Dim _item As cEnvVariable = _dico.Item(it.Name)
                        If Dico.ContainsKey(_item.Infos.Variable) Then
                            _item.Merge(Dico.Item(_item.Infos.Variable))
                        End If
                        Dim ___info As String = Nothing
                        For Each isub In it.SubItems
                            If _item.GetInformation(_columnsName(x), ___info) Then
                                isub.Text = ___info
                            End If
                            x += 1
                        Next
                        If _item.NewCount > 0 Then
                            _item.NewCount -= 1
                            If _timeToDisplayNewItemsGreen Then
                                it.BackColor = NEW_ITEM_COLOR
                            End If
                        ElseIf _item.KillCount > 0 Then
                            it.BackColor = DELETED_ITEM_COLOR
                            _item.KillCount -= 1
                        ElseIf _item.KillCount = 0 Then
                            toDel.Add(it.Name)
                        Else
                            _timeToDisplayNewItemsGreen = True
                            it.BackColor = _item.GetBackColor
                        End If
                        it.ForeColor = _item.GetForeColor
                    Next


                    ' Now remove all deleted items from listview and _dico
                    ' If first time, lock listview if necessary
                    Dim _hasToLock As Boolean = (_firstItemUpdate _
                                OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE)
                    If _hasToLock Then
                        Me.BeginUpdate()
                    End If
                    For Each key As String In toDel
                        Me.Items.RemoveByKey(key)
                        If _dico.ContainsKey(key) Then
                            _dico.Remove(key)
                        End If
                    Next
                    If _hasToLock Then
                        Me.EndUpdate()
                    End If

                    ' Sort items
                    Me.Sort()

                    _firstItemUpdate = False

                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
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
