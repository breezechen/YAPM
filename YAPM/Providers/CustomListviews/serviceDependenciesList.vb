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

Public Class serviceDependenciesList
    Inherits TreeView

    Friend _IMG As ImageList

    Public Event ItemAdded(ByRef item As cGeneralObject)
    Public Event ItemDeleted(ByRef item As cGeneralObject)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)
    Public Event Connected()

    ' ========================================
    ' Private
    ' ========================================
    Private _isConnected As Boolean = False
    Private _dico As New Dictionary(Of String, serviceInfos)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _servDepConnection As New cServDepConnection(Me, _connectionObject, New cServDepConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEventHandler))
    Private _rootService As String
    Private _infoToGet As cServDepConnection.DependenciesToget

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
    Public Property RootService() As String
        Get
            Return _rootService
        End Get
        Set(ByVal value As String)
            _rootService = value
        End Set
    End Property
    Public Property InfosToGet() As cServDepConnection.DependenciesToget
        Get
            Return _infoToGet
        End Get
        Set(ByVal value As cServDepConnection.DependenciesToget)
            _infoToGet = value
        End Set
    End Property
    Public Property IsConnected() As Boolean
        Get
            Return _isConnected
        End Get
        Set(ByVal value As Boolean)
            _isConnected = value
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

        Me.ImageList = _IMG
        _IMG.Images.Add("ok", My.Resources.gear)   ' Icon is specific
        _IMG.Images.Add("ko", My.Resources.exe)
        _IMG.Images.Add("service", My.Resources.gear)


        ' Set handlers
        _servDepConnection.Disconnected = New cServDepConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _servDepConnection.Connected = New cServDepConnection.ConnectedEventHandler(AddressOf HasConnected)
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _dico.Clear()
        Me.Nodes.Clear()
    End Sub

    ' Call this to update items in listview
    Public Sub UpdateItems()

        If _servDepConnection.IsConnected Then

            ' Now enumerate items
            _servDepConnection.Enumerate(_rootService, _infoToGet)

        End If

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, serviceInfos).ValueCollection
        Return _dico.Values
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As serviceInfos
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Safe add (manage icon stuff)
    Public Sub SafeAdd(ByVal text As String)
        Me.ClearItems()
        Dim n As New TreeNode(text)
        n.ImageKey = "ok"
        n.SelectedImageKey = n.ImageKey
        Me.Nodes.Add(n)
        Me.Update()
    End Sub


    ' ========================================
    ' Private properties
    ' ========================================

    ' Executed when enumeration is done
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String, ByVal instanceId As Integer, ByVal type As cServDepConnection.DependenciesToget)
        If Not (type = _infoToGet) Then
            Exit Sub
        End If

        sem.WaitOne()

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Service dependencies connection enumeration", errorMessage)
            sem.Release()
            Exit Sub
        End If

        _dico = Dico

        ' Now add all items to listview
        Me.BeginUpdate()

        Me.Nodes.Clear()
        Dim nn As TreeNode = Me.Nodes.Add(Native.Objects.Service.GetServiceByName(_rootService).Infos.DisplayName)
        nn.Tag = New servTag(_rootService, _rootService)
        addChilds(_dico, nn)
        nn.Expand()

        If nn.Nodes.Count > 0 Then
            nn.ImageKey = "ko"
            nn.SelectedImageKey = "ko"
        Else
            nn.ImageKey = "ok"
            nn.SelectedImageKey = "ok"
        End If

        Me.EndUpdate()

        sem.Release()

        RaiseEvent HasRefreshed()
    End Sub

    Public Structure servTag
        Public tag As String
        Public name As String
        Public Sub New(ByVal t As String, ByVal n As String)
            tag = t
            name = n
        End Sub
    End Structure
    Private Sub addChilds(ByVal _dico As Dictionary(Of String, serviceInfos), ByRef n As TreeNode)
        For Each pair As System.Collections.Generic.KeyValuePair(Of String, serviceInfos) In _dico
            If pair.Key.StartsWith(CType(n.Tag, servTag).tag & "->") AndAlso pair.Value.Tag = False Then  ' We use ObjectName to store parent name
                Dim nn As TreeNode = n.Nodes.Add(pair.Value.DisplayName)
                nn.Name = pair.Value.DisplayName
                nn.ExpandAll()
                RaiseEvent ItemAdded(Native.Objects.Service.GetServiceByName(pair.Value.DisplayName))
                nn.Tag = New servTag(pair.Key, pair.Value.Name)
                nn.ImageKey = "service"
                nn.SelectedImageKey = "service"
                pair.Value.Tag = True
                addChilds(_dico, nn)
            End If
        Next
    End Sub

#Region "Connection stuffs"

    Private Sub _connectionObject_Connected() Handles _connectionObject.Connected
        Call Connect()
    End Sub

    Private Sub _connectionObject_Disconnected() Handles _connectionObject.Disconnected
        Call Disconnect()
    End Sub

    Protected Function Connect() As Boolean
        If Me.isconnected = False Then
            Me.IsConnected = True
            _servDepConnection.ConnectionObj = _connectionObject
            _servDepConnection.Connect()
            'cGeneralObject.Connection = _servDepConnection
        End If
    End Function

    Protected Function Disconnect() As Boolean
        If Me.IsConnected Then
            Me.IsConnected = False
            _servDepConnection.Disconnect()
        End if
    End Function

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_servDepConnection.Disconnect()
        '     _servDepConnection.Con()
    End Sub

    Private Sub HasConnected(ByVal Success As Boolean)
        If Success Then
            RaiseEvent Connected()
        End If
    End Sub

#End Region

End Class
