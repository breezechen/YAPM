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

Public Class cAsyncSocketServer

    ' Public events
    Public Delegate Sub ReceivedDataEventHandler(ByRef data As Byte(), ByVal length As Integer)
    Public Delegate Sub SentDataEventHandler()
    Public Delegate Sub DisconnectedEventHandler()
    Public Delegate Sub ConnexionAcceptedEventHandle()

    Public Event ReceivedData As ReceivedDataEventHandler
    Public SentData As SentDataEventHandler
    Public Disconnected As DisconnectedEventHandler
    Public ConnexionAccepted As ConnexionAcceptedEventHandle

    ' Private attributes
    Private sock As Socket
    Private buffLength As Integer
    Private bytes() As Byte
    Private _frm As Form

    ' Constructor
    Public Sub New(ByVal [Form] As Form)
        buffLength = 65536
        _frm = [Form]
        ReDim bytes(buffLength - 1)
    End Sub
    Public Sub New(ByVal [BufferSize] As Integer, ByVal [Form] As Form)
        buffLength = [BufferSize]
        _frm = [Form]
        ReDim bytes([BufferSize] - 1)
    End Sub

    ' Connect
    Public Sub Connect(ByVal [IpAddress] As IPAddress, ByVal [Port] As Integer)
        ' New socket
        Trace.WriteLine("Server Creating socket...")
        sock = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ' Bind
        Trace.WriteLine("Server Binding...")
        sock.Bind(New System.Net.IPEndPoint([IpAddress], [Port]))
        ' Accept only one connexion
        Trace.WriteLine("Server Listening...")
        sock.Listen(1)
        ' OK, set accept callback method
        Trace.WriteLine("Server BeginAccept...")
        sock.BeginAccept(AddressOf acceptCallback, Nothing)
    End Sub

    ' Disconnect
    Public Sub Disconnect()
        ' Do not accept anymore send/receive
        Trace.WriteLine("Server Shudown connection...")
        sock.Shutdown(SocketShutdown.Both)
        ' Disconnect
        Trace.WriteLine("Server BeginDisconnect...")
        sock.BeginDisconnect(False, AddressOf disconnectCallback, Nothing)
    End Sub

    ' Send something
    Public Sub Send(ByRef [Bytes]() As Byte, ByVal [Size] As Integer)
        ' OK let's begin to send
        Trace.WriteLine("Server Sending...")
        sock.BeginSend([Bytes], 0, [Size], SocketFlags.None, AddressOf sendCallback, Nothing)
    End Sub


    ' Callback for send
    Private Sub sendCallback(ByVal asyncResult As IAsyncResult)
        ' OK validate send
        Trace.WriteLine("Server EndSend...")
        Dim result As Integer = sock.EndSend(asyncResult)
        Trace.WriteLine("Server sent data...")
        _frm.Invoke(SentData)
    End Sub

    ' Callback for disconnect
    Private Sub disconnectCallback(ByVal asyncResult As IAsyncResult)
        ' OK we are now disconnected
        Trace.WriteLine("Server disconnected...")
        _frm.Invoke(Disconnected)
    End Sub

    ' Callback for connexion accept
    Private Sub acceptCallback(ByVal asyncResult As IAsyncResult)
        ' OK, accept
        Trace.WriteLine("Server EndAccept...")
        sock = sock.EndAccept(asyncResult)
        ' Ready to receive
        ReDim bytes(buffLength - 1)
        Trace.WriteLine("Server receiving...")
        sock.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, AddressOf receiveCallback, Nothing)
        _frm.Invoke(ConnexionAccepted)
    End Sub

    ' Callback for receive
    Private Sub receiveCallback(ByVal asyncResult As IAsyncResult)
        ' OK validate reception
        Trace.WriteLine("Server EndReceive...")
        Dim result As Integer = -1
        Try
            result = sock.EndReceive(asyncResult)
        Catch ex2 As SocketException
            MsgBox(ex2.Message, MsgBoxStyle.Critical, "Socket Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        Trace.WriteLine("Server received data...")
        If result > 0 Then
            sock.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, AddressOf receiveCallback, Nothing)
        End If
        RaiseEvent ReceivedData(bytes, result)
    End Sub

    ' Set KeepAlive option
    'Private Sub SetTcpKeepAlive(ByVal keepaliveTime As UInteger, ByVal keepaliveInterval As UInteger)
    '    ' the native structure
    '    '                struct tcp_keepalive {
    '    '                ULONG onoff;
    '    '                ULONG keepalivetime;
    '    '                ULONG keepaliveinterval;
    '    '                };
    '    '                


    '    ' marshal the equivalent of the native structure into a byte array
    '    Dim dummy As UInteger = 0
    '    Dim inOptionValues As Byte() = New Byte(Marshal.SizeOf(dummy) * 3 - 1) {}
    '    BitConverter.GetBytes(CUInt((keepaliveTime))).CopyTo(inOptionValues, 0)
    '    BitConverter.GetBytes(CUInt(keepaliveTime)).CopyTo(inOptionValues, Marshal.SizeOf(dummy))
    '    BitConverter.GetBytes(CUInt(keepaliveInterval)).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2)

    '    ' write SIO_VALS to Socket IOControl
    '    sock.IOControl(IOControlCode.KeepAliveValues, inOptionValues, Nothing)
    'End Sub
    Const bytesperlong As Int32 = 4 ' // 32 / 8

    Const bitsperbyte As Int32 = 8

    Private Function SetKeepAlive(ByVal sock As Socket, ByVal time As ULong, ByVal interval As ULong) As Boolean

        Try

            ' resulting structure

            Dim SIO_KEEPALIVE_VALS((3 * bytesperlong) - 1) As Byte

            ' array to hold input values

            Dim input(2) As ULong

            ' put input arguments in input array

            If (time = 0 Or interval = 0) Then ' enable disable keep-alive

                input(0) = CType(0, ULong) ' off

            Else

                input(0) = CType(1, ULong) ' on

            End If

            input(1) = (time) ' time millis

            input(2) = (interval) ' interval millis

            ' pack input into byte struct

            For i As Int32 = 0 To input.Length - 1

                SIO_KEEPALIVE_VALS(i * bytesperlong + 3) = CByte((CLng(input(i) >> ((bytesperlong - 1) * bitsperbyte)) And &HFF))

                SIO_KEEPALIVE_VALS(i * bytesperlong + 2) = CByte((CLng(input(i) >> ((bytesperlong - 2) * bitsperbyte)) And &HFF))

                SIO_KEEPALIVE_VALS(i * bytesperlong + 1) = CByte((CLng(input(i) >> ((bytesperlong - 3) * bitsperbyte)) And &HFF))

                SIO_KEEPALIVE_VALS(i * bytesperlong + 0) = CByte((CLng(input(i) >> ((bytesperlong - 4) * bitsperbyte)) And &HFF))

            Next

            ' create bytestruct for result (bytes pending on server socket)

            Dim result() As Byte = BitConverter.GetBytes(0)

            ' write SIO_VALS to Socket IOControl

            sock.IOControl(IOControlCode.KeepAliveValues, SIO_KEEPALIVE_VALS, result) '

        Catch es As Exception

            Return False

        End Try

        Return True

    End Function
End Class
