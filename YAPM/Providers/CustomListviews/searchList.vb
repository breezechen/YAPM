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

Public Class searchList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cGeneralObject)
    Public Event ItemDeleted(ByRef item As cGeneralObject)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)

    ' ========================================
    ' Private
    ' ========================================
    Private _dico As New Dictionary(Of String, searchInfos)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _searchConnection As New cSearchConnection(Me, _connectionObject, New cSearchConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEventHandler))
    Private _searchString As String
    Private _caseSensitive As Boolean
    Private _include As searchInfos.SearchInclude = searchInfos.SearchInclude.SearchProcesses

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
    Public Property SearchString() As String
        Get
            Return _searchString
        End Get
        Set(ByVal value As String)
            _searchString = value
        End Set
    End Property
    Public Property CaseSensitive() As Boolean
        Get
            Return _caseSensitive
        End Get
        Set(ByVal value As Boolean)
            _caseSensitive = value
        End Set
    End Property
    Public Property Includes() As searchInfos.SearchInclude
        Get
            Return _include
        End Get
        Set(ByVal value As searchInfos.SearchInclude)
            _include = value
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
        _IMG.Images.Add("service", My.Resources.gear)   ' Icon is specific
        _IMG.Images.Add("exeFile", My.Resources.exe)
        _IMG.Images.Add("handle", My.Resources.handle)
        _IMG.Images.Add("envvar", My.Resources.document_text)
        _IMG.Images.Add("dllIcon", My.Resources.dllIcon)
        _IMG.Images.Add("window", My.Resources.application_blue)


        ' Set handlers
        _searchConnection.Disconnected = New cSearchConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _searchConnection.Connected = New cSearchConnection.ConnectedEventHandler(AddressOf HasConnected)
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _dico.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("service", My.Resources.gear)   ' Icon is specific
        _IMG.Images.Add("exeFile", My.Resources.exe)
        _IMG.Images.Add("handle", My.Resources.handle)
        _IMG.Images.Add("envvar", My.Resources.document_text)
        _IMG.Images.Add("dllIcon", My.Resources.dllIcon)
        _IMG.Images.Add("window", My.Resources.application_blue)
        Me.Items.Clear()
    End Sub

    ' Call this to update items in listview
    Public Overrides Sub UpdateItems()

        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If

        If _searchConnection.IsConnected Then

            ' Now enumerate items
            _searchConnection.Enumerate(_searchString, _caseSensitive, _include)

        End If

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, searchInfos).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As searchInfos
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As searchInfos
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, searchInfos).ValueCollection
        Dim res As New Dictionary(Of String, searchInfos)

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
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, searchInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        generalLvSemaphore.WaitOne()

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Search connection enumeration", errorMessage)
            generalLvSemaphore.Release()
            Exit Sub
        End If

        _dico = Dico

        ' Now add all items to listview
        Me.BeginUpdate()
        For Each z As String In _dico.Keys
            ' Add to listview
            Dim _subItems() As String
            ReDim _subItems(Me.Columns.Count - 1)
            For x As Integer = 1 To _subItems.Length - 1
                _subItems(x) = ""
            Next
            Dim _tmp As searchInfos = _dico.Item(z)
            AddItemWithStyle(z, _tmp).SubItems.AddRange(_subItems)
        Next


        ' Now refresh all subitems of the listview
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            If _dico.ContainsKey(it.Name) Then
                Dim _item As searchInfos = _dico.Item(it.Name)
                For Each isub In it.SubItems
                    isub.Text = _item.GetInformation(_columnsName(x))
                    x += 1
                Next
            End If
        Next


        ' Sort items
        Me.Sort()
        Me.EndUpdate()

        _firstItemUpdate = False

        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh search list.")

        MyBase.UpdateItems()

        generalLvSemaphore.Release()

        RaiseEvent HasRefreshed()
    End Sub

    ' Force item refreshing
    Public Overrides Sub ForceRefreshingOfAllItems()    ' Always called in a safe protected context
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            If _dico.ContainsKey(it.Name) Then
                Dim _item As searchInfos = _dico.Item(it.Name)
                For Each isub In it.SubItems
                    isub.Text = _item.GetInformation(_columnsName(x))
                    x += 1
                Next
            End If
        Next
    End Sub

    ' Add an item (specific to type of list)
    Private Shadows Function AddItemWithStyle(ByVal key As String, ByRef net As searchInfos) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        item.Name = key
        item.ForeColor = _foreColor
        item.Tag = key

        Select Case net.Type
            Case searchInfos.ResultType.EnvironmentVariable
                item.ImageKey = "envvar"
            Case searchInfos.ResultType.Handle
                item.ImageKey = "handle"
            Case searchInfos.ResultType.Module
                item.ImageKey = "dllIcon"
            Case searchInfos.ResultType.Process
                item.ImageKey = "exeFile"
            Case searchInfos.ResultType.Service
                item.ImageKey = "service"
            Case Else
                item.ImageKey = "window"
        End Select

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
            _searchConnection.ConnectionObj = _connectionObject
            _searchConnection.Connect()
            'cGeneralObject.Connection = _searchConnection
        End If
    End Function

    Protected Overrides Function Disconnect() As Boolean
        If MyBase.Disconnect Then
            _searchConnection.Disconnect()
        End If
    End Function

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_searchConnection.Disconnect()
        '     _searchConnection.Con()
    End Sub

    Private Sub HasConnected(ByVal Success As Boolean)
        '
    End Sub

#End Region

End Class
