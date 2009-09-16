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

' This code is based on a work from marcel heeremans : 
' http://www.codeproject.com/KB/IP/TwoWayRemoting.aspx?msg=3199726#xx3199726xx
' which is under the Code Project Open License (CPOL) 1.02
' Please refer to license.rtf for details about this license.

Option Strict On

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Http
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting.Channels.Ipc
Imports System.Runtime.Serialization.Formatters.Binary
Imports YAPM.RemotingServerClient
Imports MsdnMag.Remoting

Public Class AsynchronousClient

    Public Delegate Sub ReceivedDataEventHandler(ByRef data As cSocketData)
    Public Delegate Sub SentDataEventHandler()
    Public Delegate Sub DisconnectedEventHandler()
    Public Delegate Sub ConnectedEventHandler()
    Public Delegate Sub SocketErrorHandler()

    Public Event ReceivedData As ReceivedDataEventHandler
    Public Event SentData As SentDataEventHandler
    Public Event Disconnected As DisconnectedEventHandler
    Public Event Connected As ConnectedEventHandler
    Public Event SocketError As SocketErrorHandler

    Private _uniqueClientKey As String = "cDat._id"
    Private Shared semQueue As New Semaphore(1, 1)


    Private _ServerTalk As ServerTalk = Nothing
    ' this object lives on the server
    Private _CallbackSink As CallbackSink = Nothing
    ' this object lives here on the client

    Private Structure poolObjConnect
        Public ServerName As String
        Public ClientIp As String
        Public Port As Integer
        Public Sub New(ByVal aServer As String, ByVal aPort As Integer, ByVal aClient As String)
            ServerName = aServer
            Port = aPort
            ClientIp = aClient
        End Sub
    End Structure

    Public Sub Connect(ByVal serverName As String, ByVal port As Integer, ByVal clientIp As String)
        ThreadPool.QueueUserWorkItem(AddressOf pvtConnect, New poolObjConnect(serverName, port, clientIp))
    End Sub

    Public Sub Disconnect()
        RaiseEvent Disconnected()
    End Sub

    Public Sub Send(ByVal dat As cSocketData)
        ' Add the object to send into the list (queue)
        'semQueue.WaitOne()
        dat._id = _uniqueClientKey
        ThreadPool.QueueUserWorkItem(AddressOf pvtSend, CObj(dat))
    End Sub


    Private Sub pvtConnect(ByVal context As Object)
        Try
            Dim pObj As poolObjConnect = CType(context, poolObjConnect)
            ' creates a client object that 'lives' here on the client.
            _CallbackSink = New CallbackSink()
            ' hook into the event exposed on the Sink object so we can transfer a server 
            ' message through to this class.
            AddHandler _CallbackSink.OnHostToClient, AddressOf CallbackSink_OnHostToClient
            ' Register a client channel so the server can communicate back - it needs a channel
            ' opened for the callback to the CallbackSink object that is anchored on the client!
            Dim channel As TcpChannel = Nothing
            Try
                ' Now we'll create a channel for each network card interface
                Dim ht As New Hashtable()
                ht("name") = "ClientChannel"
                ht("port") = pObj.Port + 3
                ht("bindTo") = pObj.ClientIp

                ' now create and register our custom TcpChannel 
                Dim serverFormatter As New BinaryServerFormatterSinkProvider
                serverFormatter.Next = New SecureServerChannelSinkProvider
                serverFormatter.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
                channel = New TcpChannel(ht, Nothing, serverFormatter)
                ChannelServices.RegisterChannel(channel, False)

            Catch ex As Exception
                ' Already exists (reconnection)
                ex = ex
            End Try
            ' now create a transparent proxy to the server component
            Dim obj As Object = Activator.GetObject(GetType(ServerTalk), "tcp://" & pObj.ServerName & ":" & pObj.Port.ToString & "/TalkIsGood")
            ' cast returned object
            _ServerTalk = DirectCast(obj, ServerTalk)
            ' Register ourselves to the server with a callback to the client sink.
            _ServerTalk.RegisterHostToClient("client", New delCommsInfo(AddressOf _CallbackSink.HandleToClient))
            RaiseEvent Connected()
        Catch ex As Exception
            RaiseEvent Disconnected()
        End Try
    End Sub

    Private Sub pvtSend(ByVal dat As Object)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = cSerialization.GetSerializedObject(CType(dat, cSocketData))

        Try
            _ServerTalk.SendMessageToServer(New CommsInfo(byteData))
            RaiseEvent SentData()
        Catch ex As Exception
            RaiseEvent Disconnected()
        End Try
    End Sub

    Private Sub CallbackSink_OnHostToClient(ByVal info As CommsInfo)
        ' Received a message
        Dim cDat As cSocketData = cSerialization.DeserializeObject(info.Data)
        RaiseEvent ReceivedData(cDat)
    End Sub
End Class