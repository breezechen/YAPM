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
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Runtime.Remoting.Channels.Ipc
Imports System.Runtime.Serialization.Formatters.Binary
Imports RemotingServerClient
Imports MsdnMag.Remoting

Public Class AsynchronousSocketListener

    Public Delegate Sub ReceivedDataEventHandler(ByRef data As cSocketData)
    Public Delegate Sub SentDataEventHandler()
    Public Delegate Sub DisconnectedEventHandler()
    Public Delegate Sub ConnectedEventHandler()
    Public Delegate Sub SocketErrorHandler()
    Public Delegate Sub WaitingForConnectionHandler()

    Public Event ReceivedData As ReceivedDataEventHandler
    Public Event SentData As SentDataEventHandler
    Public Event Disconnected As DisconnectedEventHandler
    Public Event Connected As ConnectedEventHandler
    Public Event WaitingForConnection As WaitingForConnectionHandler
    Public Event SocketError As SocketErrorHandler


    'Private listener As Socket
    Private _dicoClient As New Dictionary(Of String, Socket)
    Public Shared allDone As New ManualResetEvent(False)


    Public Sub Connect(ByVal port As Integer)
        ThreadPool.QueueUserWorkItem(AddressOf pvtConnect, CObj(port))
    End Sub 'Main


    Private Sub pvtConnect(ByVal context As Object)
        Try
            Dim port As Integer = CInt(context)
            ' Register a server channel on the Server where we 
            ' will listen for clients who wish to communicate
            RegisterChannel(port)
            ' Register callbacks to the static properties on the ServerTalk object
            ServerTalk.NewUser = New delUserInfo(AddressOf NewUser)
            ServerTalk.ClientToHost = New delCommsInfo(AddressOf ClientToHost)
            Dim t As New Thread(New ThreadStart(AddressOf CheckClientToServerQueue))
            t.Start()
        Catch ex As Exception
            RaiseEvent Disconnected()
        End Try
    End Sub

    ' The method that will be called when a new User registers.
    Private Sub NewUser(ByVal UserID As String)
        ' since it originated from a different thread we need to marshal this back to the current UI thread.
        '
    End Sub
    ' A loop invoked by a worker-thread which will monitor the static tread-safe ClientToServer 
    ' Queue on the ServerTalk class and passes on any CommsInfo objects that are placed here.
    ' If the variable _FormClosing turns true (when the form closes) it will stop the loop and
    ' subsequently the life of the worker-thread.
    Private _FormClosing As Boolean = False
    Private Sub CheckClientToServerQueue()
        While Not _FormClosing
            Thread.Sleep(50)
            ' allow rest of the system to continue whilst waiting...
            If ServerTalk.ClientToServerQueue.Count > 0 Then
                Dim message As CommsInfo = DirectCast(ServerTalk.ClientToServerQueue.Dequeue(), CommsInfo)
                If message.Message = "clientDisconnect" Then
                    ' Oh, the client has disconnected !
                    ServerTalk.ClientToServerQueue.Clear()
                    ' Let's empty the dictionnary of current processes/services
                    ProcessProvider.ClearNewProcessesDico()
                    ServiceProvider.ClearNewServicesList()
                    EnvVariableProvider.ClearList()
                Else
                    ClientToHost(message)
                End If
            End If
        End While
    End Sub
    ' A helper method that will marshal a CommsInfo from the client to 
    ' our UI thread.
    Private Sub ClientToHost(ByVal Info As CommsInfo)
        ' since it originated from a different thread we need to marshal this back to the current UI thread.
        Dim cDat As cSocketData = cSerialization.DeserializeObject(Info.Data)
        RaiseEvent ReceivedData(CDat)
    End Sub

    Private Sub RegisterChannel(ByVal port As Integer)
        ' Set the TypeFilterLevel to Full since callbacks require additional security 
        ' requirements
        Dim serverFormatter As New BinaryServerFormatterSinkProvider
        serverFormatter.Next = New SecureServerChannelSinkProvider
        serverFormatter.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full

        ' we have to change the name since we can't have two channels with the same name.
        Dim ht As New Hashtable()
        ht("name") = "ServerChannel"
        ht("port") = port

        ' now create and register our custom TcpChannel 
        Dim channel As New TcpChannel(ht, Nothing, serverFormatter)
        ChannelServices.RegisterChannel(channel, False)

        ' register a WKO type in Singleton mode
        Dim identifier As String = "TalkIsGood"
        Dim mode As WellKnownObjectMode = WellKnownObjectMode.Singleton

        Dim entry As New WellKnownServiceTypeEntry(GetType(ServerTalk), identifier, mode)
        RemotingConfiguration.RegisterWellKnownServiceType(entry)
    End Sub

    Public Sub Disconnect()
        _FormClosing = True
    End Sub

    Public Sub Send(ByVal dat As cSocketData)
        ThreadPool.QueueUserWorkItem(AddressOf pvtSend, CObj(dat))
    End Sub

    Private Sub pvtSend(ByVal dat As Object)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = cSerialization.GetSerializedObject(CType(dat, cSocketData))

        Try
            ServerTalk.RaiseHostToClient("*", byteData)
            RaiseEvent SentData()
        Catch ex As Exception
            RaiseEvent Disconnected()
        End Try
    End Sub

End Class