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

Imports System.Net

Public Class cConnection

    Public Const REMOTE_PORT As Integer = 8081

    Public Event Connected()
    Public Event Disconnected()

    ' Type of connection
    Public Enum TypeOfConnection
        [LocalConnection]
        [RemoteConnectionViaSocket]
        [RemoteConnectionViaWMI]
    End Enum

    ' Parameters for a socket connection
    Public Structure SocketConnectionParameters
        Public port As Integer
        Public address As IPAddress
        Public Sub New(ByVal theAddress As IPAddress, ByVal thePort As Integer)
            port = thePort
            address = theAddress
        End Sub
    End Structure

    ' Parameters for a socket connection
    Public Structure WMIConnectionParameters
        Public userName As String
        Public password As String
        Public serverName As String
        Public Sub New(ByVal server As String, ByVal user As String, ByVal pass As String)
            userName = user
            password = pass
            serverName = server
        End Sub
    End Structure

    Private WithEvents _sock As cAsyncSocket
    Private _conType As TypeOfConnection
    Private _conSocket As SocketConnectionParameters
    Private _conWMI As WMIConnectionParameters
    Private _isConnected As Boolean

    Public ReadOnly Property Socket() As cAsyncSocket
        Get
            Return _sock
        End Get
    End Property

    Public Property ConnectionType() As TypeOfConnection
        Get
            Return _conType
        End Get
        Set(ByVal value As TypeOfConnection)
            _conType = value
        End Set
    End Property

    Public Property SocketParameters() As SocketConnectionParameters
        Get
            Return _conSocket
        End Get
        Set(ByVal value As SocketConnectionParameters)
            If Not (_isConnected) Then
                _conSocket = value
            End If
        End Set
    End Property

    Public Property WmiParameters() As WMIConnectionParameters
        Get
            Return _conWMI
        End Get
        Set(ByVal value As WMIConnectionParameters)
            If Not (_isConnected) Then
                _conWMI = value
            End If
        End Set
    End Property

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _isConnected
        End Get
    End Property


    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef ccon As cConnection)
        _conSocket = ccon.SocketParameters
        _conType = ccon.ConnectionType
        _conWMI = ccon.WmiParameters
    End Sub

    Public Sub CopyFromInstance(ByRef ccon As cConnection)
        _conType = ccon.ConnectionType
        _conSocket = ccon.SocketParameters
        _conWMI = ccon.WmiParameters
    End Sub

    ' BAD WAY (because of withevents, this is raised JUST WHEN frmMain.theConnection.Connect
    ' is called. BAD THING (should wait asyncMethod, but there are LOTS of asyncMethids
    ' (one for each lvItem).
    Public Sub Connect()
        If Me.ConnectionType = TypeOfConnection.RemoteConnectionViaSocket Then
            If _sock Is Nothing Then
                _sock = New cAsyncSocket()
                _sock.Connect(_conSocket)
            Else
                _isConnected = True
                RaiseEvent Connected()
            End If
        Else
            _isConnected = True
            RaiseEvent Connected()
        End If
    End Sub
    Public Sub Disconnect()
        If Me.ConnectionType = TypeOfConnection.RemoteConnectionViaSocket Then
            _sock.Disconnect()
        Else
            _isConnected = False
            RaiseEvent Disconnected()
        End If
    End Sub

    Private Sub _sock_Connected() Handles _sock.Connected
        _isConnected = True
        RaiseEvent Connected()
    End Sub
    Private Sub _sock_Disconnected() Handles _sock.Disconnected
        _isConnected = False
        _sock = Nothing
        RaiseEvent Disconnected()
    End Sub

    Private Sub _sock_SentData() Handles _sock.SentData
        ' When we have sent datas
    End Sub

    ' When we receive datas
    Private Sub _sock_ReceivedData(ByRef data() As Byte, ByVal length As Integer) Handles _sock.ReceivedData
        Dim cDat As cSocketData = cSerialization.DeserializeObject(data)

        If cDat.Type = cSocketData.DataType.RequestedList Then
            ' Here we got a list of items
            Select Case cDat.Order
                Case cSocketData.OrderType.RequestProcessList
                    asyncCallbackProcEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestEnvironmentVariableList
                    asyncCallbackEnvVariableEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestHandleList
                    asyncCallbackHandleEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestMemoryRegionList
                    asyncCallbackMemRegionEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestModuleList
                    asyncCallbackModuleEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestNetworkConnectionList
                    asyncCallbackNetworkEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestPrivilegesList
                    asyncCallbackPrivilegesEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestServiceList
                    asyncCallbackServiceEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestTaskList
                    asyncCallbackTaskEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestThreadList
                    asyncCallbackThreadEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
                Case cSocketData.OrderType.RequestWindowList
                    asyncCallbackWindowEnumerate.GotListFromSocket(cDat.GetList, cDat.GetKeys)
            End Select

        Else
            '
        End If
    End Sub

End Class
