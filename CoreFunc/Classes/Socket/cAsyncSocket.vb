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

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class cAsyncSocket

    Public Delegate Sub ReceivedDataEventHandler(ByRef data As Byte(), ByVal length As Integer)
    Public Delegate Sub SentDataEventHandler()
    Public Delegate Sub DisconnectedEventHandler()
    Public Delegate Sub ConnectedEventHandler()

    Public Event ReceivedData As ReceivedDataEventHandler
    Public Event SentData As SentDataEventHandler
    Public Event Disconnected As DisconnectedEventHandler
    Public Event Connected As ConnectedEventHandler

    Private sock As Socket
    Private buffLength As Integer
    Private bytes() As Byte
    'Private _frm As Form

    ' Constructor
    Public Sub New() 'ByVal [Form] As Form)
        buffLength = 605536
        '_frm = [Form]
        ReDim bytes(buffLength - 1)
    End Sub
    Public Sub New(ByVal [BufferSize] As Integer) ', ByVal [Form] As Form)
        buffLength = [BufferSize]
        '_frm = [Form]
        ReDim bytes([BufferSize] - 1)
    End Sub

    ' Connect
    Public Sub Connect(ByVal [IpAddress] As IPAddress, ByVal [Port] As Integer)
        ' New socket
        Trace.WriteLine("Client Creating socket...")
        sock = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ' OK, connect
        Trace.WriteLine("Client connecting...")
        sock.BeginConnect(New System.Net.IPEndPoint([IpAddress], [Port]), AddressOf connectCallback, Nothing)
    End Sub

    ' Disconnect
    Public Sub Disconnect()
        ' Do not accept anymore send/receive
        Trace.WriteLine("Client Shudown connection...")
        sock.Shutdown(SocketShutdown.Both)
        ' Disconnect
        Trace.WriteLine("Client BeginDisconnect...")
        sock.BeginDisconnect(False, AddressOf disconnectCallback, Nothing)
    End Sub

    ' Send something
    Public Sub Send(ByRef [Bytes]() As Byte, ByVal [Size] As Integer)
        ' OK let's begin to send
        Trace.WriteLine("Client Sending...")
        sock.BeginSend([Bytes], 0, [Size], SocketFlags.None, AddressOf sendCallback, Nothing)
    End Sub
    Public Sub Send(ByRef [Data] As cSocketData)
        ' OK let's begin to send
        Trace.WriteLine("Client Sending...")
        Dim buff() As Byte = cSerialization.GetSerializedObject([Data])
        sock.BeginSend(buff, 0, buff.Length, SocketFlags.None, AddressOf sendCallback, Nothing)
    End Sub

    ' Callback for send
    Private Sub sendCallback(ByVal asyncResult As IAsyncResult)
        ' OK validate send
        Trace.WriteLine("Client EndSend...")
        Dim result As Integer = sock.EndSend(asyncResult)
        Trace.WriteLine("Client sent data...")
        RaiseEvent SentData()
    End Sub

    ' Callback for disconnect
    Private Sub disconnectCallback(ByVal asyncResult As IAsyncResult)
        ' OK we are now disconnected
        Trace.WriteLine("Client disconnected...")
        RaiseEvent Disconnected()
    End Sub

    ' Callback for connexion accept
    Private Sub connectCallback(ByVal asyncResult As IAsyncResult)
        ' OK, accept
        Trace.WriteLine("Client EndConnect...")
        Call sock.EndConnect(asyncResult)
        Trace.WriteLine("Client connected...")
        ' Ready to receive
        ReDim bytes(buffLength - 1)
        Trace.WriteLine("Client BeginReceive...")
        sock.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, AddressOf receiveCallback, Nothing)
        RaiseEvent Connected()
    End Sub

    ' Callback for receive
    Private Sub receiveCallback(ByVal asyncResult As IAsyncResult)
        ' OK validate reception
        Trace.WriteLine("Client EndReceive...")
        Dim result As Integer = sock.EndReceive(asyncResult)
        Trace.WriteLine("Client received data...")
        If result > 0 Then
            sock.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, AddressOf receiveCallback, Nothing)
        End If
        RaiseEvent ReceivedData(bytes, result)
    End Sub

End Class
