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

Public Class mainServiceList
    Inherits customLV

    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _first As Boolean
    Private _dico As New Dictionary(Of String, cService)
    Private WithEvents _connectionObject As New cConnection


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
        _IMG.Images.Add("service", My.Resources.gear)   ' Icon is specific

        _first = True

        ' Create buffer 
        Me.CreateSubItemsBuffer()

        ' Set handlers
        AddHandler ServiceProvider.GotRefreshed, AddressOf Me.GotRefreshed

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
        _IMG.Images.Add("service", My.Resources.gear)   ' Icon is specific
        Me.Items.Clear()
    End Sub

    ' Reanalize a process
    Public Sub ReAnalizeServices()

        ' In local mode, we simply refresh the service
        ' In WMI and Socket mode, there IS NO NEED to reanalize because all informations
        ' are retrieved each time we refresh
        ' In fact, "Reanalize" is only available for Local mode
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            For Each cs As cService In Me.GetSelectedItems
                cs.Refresh()
            Next
        End If
    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cService).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cService
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cService
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cService).ValueCollection
        Dim res As New Dictionary(Of String, cService)

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
    Private Delegate Sub degGotNewItems(ByVal names As List(Of String), ByVal newItems As Dictionary(Of String, serviceInfos))
    Private Sub GotNewItems(ByVal names As List(Of String), ByVal newItems As Dictionary(Of String, serviceInfos))

        ' Lock lv if necesary
        Dim _hasToLock As Boolean = (_firstItemUpdate OrElse newItems.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE)
        If _hasToLock Then
            Me.BeginUpdate()
        End If
        For Each name As String In names
            If _dico.ContainsKey(name) = False Then
                Dim proc As New cService(newItems(name))
                proc.NewCount = 3
                _dico.Add(name, proc)

                ' Add new item to lv
                Dim _subItems() As String
                ReDim _subItems(Me.Columns.Count - 1)
                For x As Integer = 1 To _subItems.Length - 1
                    _subItems(x) = ""
                Next
                AddItemWithStyle(name).SubItems.AddRange(_subItems)
            End If
        Next

        ' Unlock lv if necesary
        If _hasToLock Then
            Me.EndUpdate()
        End If

    End Sub
    Private Sub GotDeletedItems(ByVal names As List(Of String))
        For Each name As String In names
            Dim cp As cService = Nothing
            If _dico.ContainsKey(name) Then
                cp = _dico(name)
                cp.KillCount = 3
            End If
        Next
    End Sub

    Private Delegate Sub degGotRefreshed(ByVal _dicoNew As List(Of String), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Private Sub GotRefreshed(ByVal _dicoNew As List(Of String), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
        ' Have to call a delegate as will refresh the listview
        ' Only for the good instance
        If instanceId = Me.InstanceId Then
            If Me.InvokeRequired Then
                Dim d As New degGotRefreshed(AddressOf GotRefreshed)
                Try
                    Me.Invoke(d, _dicoNew, _dicoDel, Dico, instanceId, res)
                Catch ex As Exception
                    ' Won't catch this...
                End Try
            Else

                If res.Success Then

                    'Dim i As Integer = Native.Api.Win32.GetElapsedTime

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
                    'For iiii As Integer = 1 To 10
                    Try

                        Dim toDel As New List(Of String)   ' Keys of items to remove

                        ' Now refresh all subitems of the listview
                        Dim isub As ListViewItem.ListViewSubItem
                        Dim it As ListViewItem
                        For Each it In Me.Items
                            Dim x As Integer = 0
                            Dim _item As cService = _dico.Item(it.Name)
                            If Dico.ContainsKey(it.Name) Then
                                _item.Merge(Dico.Item(it.Name))
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
                    'Next
                    'i = Native.Api.Win32.GetElapsedTime - i
                    'Trace.WriteLine("TOOK " & i)

                Else
                    RaiseEvent GotAnError("Service enumeration", res.ErrorMessage)
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
        With item
            .Name = key
            .ImageKey = "service"
            .ForeColor = _foreColor
            .Tag = key
            '.Group = Me.Groups(0)
        End With

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
