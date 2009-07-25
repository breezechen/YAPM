﻿Option Strict On

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text



' State object for receiving data from remote device.

Public Class StateObject
    ' Client socket.
    Public workSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BUFFER_SIZE As Integer = 1024
    ' Receive buffer.
    Public buffer(BUFFER_SIZE) As Byte
    ' Received data buffer & size
    Public receivedSize As Integer
    Public receivedBuff() As Byte
End Class 'StateObject




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


    Private client As Socket
    Private _uniqueClientKey As String = "cDat._id"
    Private Shared semQueue As New Semaphore(1, 1)

    Public Sub Connect(ByVal ip As IPAddress, ByVal port As Integer)
        ' Establish the remote endpoint for the socket.
        Dim remoteEP As New IPEndPoint(ip, port)

        ' Create a TCP/IP socket.
        Try
            client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while connecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
        ' Connect to the remote endpoint.
        Trace.WriteLine("Connecting to server...")
        Try
            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while connecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
    End Sub 'Main
    Public Sub Connect(ByVal ipString As String, ByVal port As Integer)
        ' Establish the remote endpoint for the socket.
        Dim ipAddress As IPAddress = ipAddress.Parse(ipString)
        Dim remoteEP As New IPEndPoint(ipAddress, port)

        ' Create a TCP/IP socket.
        Try
            client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while connecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try

        ' Connect to the remote endpoint.
        Trace.WriteLine("Connecting to server...")
        Try
            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while connecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
    End Sub 'Main

    Public Sub Disconnect()
        ' Do not accept anymore send/receive
        Trace.WriteLine("Client Shudown connection...")
        Try
            client.Shutdown(SocketShutdown.Both)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while disconnecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
        ' Disconnect
        Trace.WriteLine("Client BeginDisconnect...")
        Try
            client.Close()
            If client.Connected Then
                'client.BeginDisconnect(True, New AsyncCallback(AddressOf disconnectCallback), client)
            Else
                RaiseEvent Disconnected()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while disconnecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
    End Sub

    ' Callback for disconnect
    Private Sub disconnectCallback(ByVal asyncResult As IAsyncResult)
        ' OK we are now disconnected
        Trace.WriteLine("Client EndDisconnect...")
        Try
            Call client.EndDisconnect(asyncResult)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while disconnecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
        Trace.WriteLine("Client disconnected...")
        RaiseEvent Disconnected()
    End Sub

    Private Sub ConnectCallback(ByVal ar As IAsyncResult)
        ' Retrieve the socket from the state object.
        Dim client As Socket = CType(ar.AsyncState, Socket)

        ' Complete the connection.
        Try
            client.EndConnect(ar)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while connecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
        Trace.WriteLine("CLIENT CONNECTED")
        RaiseEvent Connected()

        ' Create the state object.
        Dim state As New StateObject
        state.workSocket = client

        ' Begin receiving the data from the remote device.
        Trace.WriteLine("Client waiting for data...")
        Try
            client.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while connecting")
            RaiseEvent Disconnected()
            Exit Sub
        End Try

    End Sub 'ConnectCallback


    Private Sub ReceiveCallback(ByVal ar As IAsyncResult)

        ' Retrieve the state object and the client socket 
        ' from the asynchronous state object.
        Dim state As StateObject = CType(ar.AsyncState, StateObject)
        Dim client As Socket = state.workSocket

        If client.Connected = False Then
            Exit Sub
        End If

        ' Read data from the remote device.
        Dim bytesRead As Integer
        Try
            bytesRead = client.EndReceive(ar)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while receiving data")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
        Trace.WriteLine("CLIENT RECEIVED DATA")

        If bytesRead > 0 Then
            ' There might be more data, so store the data received so far.

            If state.receivedBuff Is Nothing Then
                ' Then this is the first part we get
                ReDim state.receivedBuff(bytesRead)
            Else
                ' Redim memory allocated
                ReDim Preserve state.receivedBuff(bytesRead + state.receivedSize)
            End If

            ' Copy buffer to global buffer
            Buffer.BlockCopy(state.buffer, 0, state.receivedBuff, state.receivedSize, bytesRead)
            state.receivedSize += bytesRead

            ' Get the rest of the data.
            Try
                client.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while receiving data")
                RaiseEvent Disconnected()
                Exit Sub
            End Try
        Else
            ' All the data has arrived; put it in response.

            Dim cDat As cSocketData = cSerialization.DeserializeObject(state.receivedBuff)
            If cDat IsNot Nothing Then
                'If cDat.Type = cSocketData.DataType.Identification Then
                ' This is the identification key we receive
                _uniqueClientKey = cDat._id
                'Else
                If cDat.Ack Then
                    Try
                        semQueue.Release(1)
                    Catch ex As Exception
                        '
                    End Try
                End If
                RaiseEvent ReceivedData(cDat)
                Trace.WriteLine("DATA HAS A SIZE OF " & state.receivedSize.ToString)
                ReDim state.receivedBuff(0)
                state.receivedSize = 0
                'End If
            End If
        End If

        If bytesRead < StateObject.BUFFER_SIZE Then
            ' Try to get a socketData from buffer, because it is the last part of
            ' the datas
            Dim cDat As cSocketData = cSerialization.DeserializeObject(state.receivedBuff)
            If cDat IsNot Nothing Then
                'If cDat.Type = cSocketData.DataType.Identification Then
                ' This is the identification key we receive
                _uniqueClientKey = cDat._id
                'Else
                If cDat.Ack Then
                    Try
                        semQueue.Release(1)
                    Catch ex As Exception
                        '
                    End Try
                End If
                RaiseEvent ReceivedData(cDat)
                Trace.WriteLine("DATA HAS A SIZE OF " & state.receivedSize.ToString)
                ReDim state.receivedBuff(0)
                state.receivedSize = 0
                'End If
            End If
        End If

    End Sub 'ReceiveCallback

    ' When we receive an acknowledge, next "send" will be authorized
    Public Sub AckReceived()
        semQueue.Release(1)
    End Sub

    Public Sub Send(ByVal dat As cSocketData)
        ' Add the object to send into the list (queue)
        semQueue.WaitOne()
        dat._id = _uniqueClientKey
        pvtSend(dat)
    End Sub
    Private Sub pvtSend(ByRef dat As cSocketData)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = cSerialization.GetSerializedObject(dat)

        ' Begin sending the data to the remote device.
        Trace.WriteLine("Client sending...")
        Try
            client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while sending data")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
    End Sub 'Send

    Private Sub SendCallback(ByVal ar As IAsyncResult)
        ' Retrieve the socket from the state object.
        Dim client As Socket = CType(ar.AsyncState, Socket)

        ' Complete sending the data to the remote device.
        Try
            Dim bytesSent As Integer = client.EndSend(ar)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error while sending data")
            RaiseEvent Disconnected()
            Exit Sub
        End Try
        Trace.WriteLine("CLIENT HAS SENT")
        RaiseEvent SentData()

    End Sub 'SendCallback
End Class 'AsynchronousClient