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

Public Class processList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cProcess)
    Public Event ItemDeleted(ByRef item As cProcess)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)


    ' ========================================
    ' Private
    ' ========================================
    Private _first As Boolean
    Private _enumMethod As asyncCallbackProcEnumerate.ProcessEnumMethode = asyncCallbackProcEnumerate.ProcessEnumMethode.VisibleProcesses
    Private _dicoNew As New Dictionary(Of String, cProcess)
    Private _dicoDel As New Dictionary(Of String, cProcess)
    Private _buffDico As New Dictionary(Of String, cProcess)
    Private _dico As New Dictionary(Of String, cProcess)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _processConnection As New cProcessConnection(Me, _connectionObject, New cProcessConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEventHandler))

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
    Public Property EnumMethod() As asyncCallbackProcEnumerate.ProcessEnumMethode
        Get
            Return _enumMethod
        End Get
        Set(ByVal value As asyncCallbackProcEnumerate.ProcessEnumMethode)
            _enumMethod = value
            _processConnection.EnumMethod = _enumMethod
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

        _first = True

        ' Set handlers
        _processConnection.Disconnected = New cProcessConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _processConnection.Connected = New cProcessConnection.ConnectedEventHandler(AddressOf HasConnected)
    End Sub

    ' Get an item from listview
    Public Function GetImageFromImageList(ByVal key As String) As System.Drawing.Image
        Return _IMG.Images.Item(key)
    End Function

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _firstItemUpdate = True
        _buffDico.Clear()
        _dico.Clear()
        _dicoDel.Clear()
        _dicoNew.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("noIcon", My.Resources.application_blue)
        Me.Items.Clear()
    End Sub

    ' Reanalize a process
    Public Sub ReAnalizeProcesses()
        Dim pid() As Integer
        ReDim pid(Me.GetSelectedItems.Count - 1)
        Dim x As Integer = 0
        For Each cp As cProcess In Me.GetSelectedItems
            pid(x) = cp.Infos.Pid
            x += 1
        Next
        asyncCallbackProcEnumerate.ReanalizeProcess(New asyncCallbackProcEnumerate.ReanalizeProcessObj(pid, _processConnection))
    End Sub

    ' Call this to update items in listview
    Public Overrides Sub UpdateItems()

        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If

        If _processConnection.IsConnected Then

            ' Now enumerate items
            _processConnection.Enumerate(_first, enumMethod:=_enumMethod)

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
        Return _dico.Item(key)
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cProcess).ValueCollection
        Dim res As New Dictionary(Of String, cProcess)

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
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, processInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        sem.WaitOne()

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Process enumeration", errorMessage)
            sem.Release()
            Exit Sub
        End If

        ' We won't enumerate next time with all informations (included fixed infos)
        _first = False


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cProcess In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Infos.Pid.ToString, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each pair As System.Collections.Generic.KeyValuePair(Of String, processInfos) In Dico
            If Not (_dico.ContainsKey(pair.Key)) Then
                ' Add to dico
                _dicoNew.Add(pair.Key, New cProcess(pair.Value))
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


        cProcess.SemCurrentProcesses.WaitOne()
        cProcess.CurrentProcesses = New Dictionary(Of String, cProcess)((_dico))
        cProcess.SemCurrentProcesses.Release()


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
            Dim _item As cProcess = _dico.Item(it.Name)
            If Dico.ContainsKey(it.Name) Then
                _item.Merge(Dico.Item(it.Name))
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

        ' This piece of code is needed. Strange behavior, the Text attribute must
        ' be set twice to be properly displayed.
        If _firstItemUpdate Then
            For Each it In Me.Items
                For Each isub In it.SubItems
                    isub.Text = isub.Text
                Next
            Next
        End If


        ' Set processes to task class
        cTask.ProcessCollection = _dico


        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh process list.")

        MyBase.UpdateItems()

        sem.Release()
    End Sub


    ' Add an item (specific to type of list)
    Friend Overrides Function AddItemWithStyle(ByVal key As String) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        Dim proc As cProcess = _dico.Item(key)
        item.Name = key

        ' Add to global dico
        cProcess.AssociatePidAndName(key, _dico.Item(key).Infos.Name)

        If _connectionObject.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            If proc.Infos.Pid > 4 Then

                ' Forecolor
                item.ForeColor = _foreColor

                ' Add icon
                Try

                    Dim fName As String = proc.Infos.Path

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

    Private Sub Connect()
        _first = True
        _processConnection.ConnectionObj = _connectionObject
        asyncCallbackProcEnumerate.ClearDico()
        _processConnection.Connect()
        cProcess.Connection = _processConnection
    End Sub

    Private Sub Disconnect()
        _processConnection.Disconnect()
        asyncCallbackProcEnumerate.ClearDico()
    End Sub

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_processConnection.Disconnect()
        '     _processConnection.Con()
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
