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

    Public Event ItemAdded(ByRef item As cProcess)
    Public Event ItemDeleted(ByRef item As cProcess)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _firstRefresh As Boolean
    Private _first As Boolean
    Private _dicoNew As New Dictionary(Of String, cProcess)
    Private _dicoDel As New Dictionary(Of String, cProcess)
    Private _buffDico As New Dictionary(Of String, cProcess)
    Private _dico As New Dictionary(Of String, cProcess)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _jobConnection As New cJobConnection(Me, _connectionObject, New cJobConnection.HasEnumeratedProcInJobEventHandler(AddressOf HasEnumeratedEventHandler))
    Private _job As cJob

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
    Public Property Job() As cJob
        Get
            Return _job
        End Get
        Set(ByVal value As cJob)
            _job = value
        End Set
    End Property
    Public ReadOnly Property FirstRefreshDone() As Boolean
        Get
            Return Not (_firstRefresh)
        End Get
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

        ' Set handlers
        _jobConnection.Disconnected = New cJobConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _jobConnection.Connected = New cJobConnection.ConnectedEventHandler(AddressOf HasConnected)
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
        _buffDico.Clear()
        _dico.Clear()
        _dicoDel.Clear()
        _dicoNew.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("noIcon", My.Resources.application_blue16)
        Me.Items.Clear()
    End Sub


    ' Call this to update items in listview
    Public Overrides Sub UpdateItems()

        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If

        If _jobConnection.IsConnected Then

            ' Now enumerate items
            _jobConnection.EnumerateProcessesInJob(_job.Infos.Name)

        End If

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

        generalLvSemaphore.WaitOne()
        For Each it As ListViewItem In Me.SelectedItems
            res.Add(it.Name, _dico.Item(it.Name))
        Next
        generalLvSemaphore.Release()

        Return res.Values
    End Function


    ' ========================================
    ' Private properties
    ' ========================================

    ' Executed when enumeration is done
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, processInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        generalLvSemaphore.WaitOne()

        Dim _test As Integer = Native.Api.Win32.GetElapsedTime

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Process enumeration", errorMessage)
            generalLvSemaphore.Release()
            Exit Sub
        End If

        ' We won't enumerate next time with all informations (included fixed infos)
        _first = False


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cProcess In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Infos.ProcessId.ToString, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each cp As processInfos In Dico.Values
            If Not (_dico.ContainsKey(cp.ProcessId.ToString)) Then
                ' Add to dico
                _dicoNew.Add(cp.ProcessId.ToString, cProcess.GetProcessById(cp.ProcessId))
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
            cProcess.UnAssociatePidAndName(z)    ' Remove from global dico
        Next
        _dicoDel.Clear()


        ' Merge _dico and _dicoNew
        For Each z As String In _dicoNew.Keys
            Dim _it As cProcess = _dicoNew.Item(z)
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
            Dim _item As cProcess = _dico.Item(it.Name)
            Dim ___info As String = Nothing
            For Each isub In it.SubItems
                'If _item.GetInformation(_columnsName(x), ___info) Then
                '    isub.Text = ___info
                'End If
                isub.Text = _item.GetInformation(_columnsName(x))
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
            it.ForeColor = _item.GetForeColor

            ' If we are in 'show hidden process mode', we have to set color red for
            ' hidden processes
            If _item.Infos.IsHidden Then
                it.ForeColor = Color.Red
            Else
                If it.ForeColor = Color.Red Then
                    it.ForeColor = Color.Black
                End If
            End If
        Next


        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        _test = Native.Api.Win32.GetElapsedTime - _test
        Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh process list.")

        MyBase.UpdateItems()

        generalLvSemaphore.Release()
        _firstRefresh = False

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

        ' Add to global dico
        cProcess.AssociatePidAndName(key, _dico.Item(key).Infos.Name)

        If _connectionObject.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
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

#Region "Connection stuffs"

    Private Sub _connectionObject_Connected() Handles _connectionObject.Connected
        Call Connect()
    End Sub

    Private Sub _connectionObject_Disconnected() Handles _connectionObject.Disconnected
        Call Disconnect()
    End Sub

    Protected Overrides Function Connect() As Boolean
        If MyBase.Connect Then
            Me.IsConnected = True
            _first = True
            _firstRefresh = True
            _jobConnection.ConnectionObj = _connectionObject
            Native.Objects.Process.ClearNewProcessesDico()
            _jobConnection.Connect()
        End If
    End Function

    Protected Overrides Function Disconnect() As Boolean
        If MyBase.Disconnect Then
            Me.IsConnected = False
            _jobConnection.Disconnect()
            Native.Objects.Process.ClearNewProcessesDico()
        End If
    End Function

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_jobConnection.Disconnect()
        '     _jobConnection.Con()
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
