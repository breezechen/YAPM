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
Imports System.Net.Sockets

Public Class frmServeur

    Private WithEvents sock As New cAsyncSocketServer(Me)
    Private Const PORT As Integer = 8081
    Private _readyToLeave As Boolean = True

    Private theConnection As New cConnection
    Private _procCon As New cProcessConnection(Me, theConnection)

    ' Connect to local machine
    Private Sub connectLocal()
        ' Set handlers
        With _procCon
            .HasEnumerated = New cProcessConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedProcess)
            '.Disconnected = New cProcessConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
            '.Connected = New cProcessConnection.ConnectedEventHandler(AddressOf HasConnected)
        End With
        ' Set connection
        With theConnection
            .ConnectionType = cConnection.TypeOfConnection.LocalConnection
            .Connect()
        End With
        _procCon.ConnectionObj = theConnection
        _procCon.Connect()
    End Sub

    ' Send process list
    Private Sub HasEnumeratedProcess(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, processInfos), ByVal errorMessage As String)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestProcessList)
                cDat.SetProcessList(Dico)
                Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                sock.Send(buff, buff.Length)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            ' Send an error
        End If

    End Sub

    Private Sub frmServeur_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            sock.Disconnect()
        Catch ex As Exception
            '
        End Try
        'If _readyToLeave = False Then
        '    ' e.Cancel = True
        'End If
    End Sub

    Private Sub sock_ConnexionAccepted()
        '_readyToLeave = False
        Me.Text = "Connected"
    End Sub

    Private Sub sock_Disconnected()
        '_readyToLeave = True
    End Sub

    Private Sub sock_ReceivedData(ByRef data() As Byte, ByVal length As Integer)
        Try
            ' Recreate the data class
            Dim cData As cSocketData = cSerialization.DeserializeObject(data)

            ' Extract the type of information we have to send
            If cData.Type = cSocketData.DataType.Order Then

                Select Case cData.Order

                    Case cSocketData.OrderType.RequestProcessList
                        Call _procCon.Enumerate(True)

                End Select

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Connect the socket (server)
        Try
            sock.Connect(Net.IPAddress.Parse(TextBox1.Text), PORT)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub sock_SentData()
        '   MsgBox("serveur sent")
        Dim oo As Integer = 0
    End Sub

    Private Sub frmServeur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sock.ConnexionAccepted = New cAsyncSocketServer.ConnexionAcceptedEventHandle(AddressOf sock_ConnexionAccepted)
        sock.Disconnected = New cAsyncSocketServer.DisconnectedEventHandler(AddressOf sock_Disconnected)
        ' sock.ReceivedData = New cAsyncSocketServer.ReceivedDataEventHandler(AddressOf sock_ReceivedData)
        sock.SentData = New cAsyncSocketServer.SentDataEventHandler(AddressOf sock_SentData)

        connectLocal()

        Button1_Click(Nothing, Nothing)
    End Sub

    Private Sub sock_ReceivedData1(ByRef data() As Byte, ByVal length As Integer) Handles sock.ReceivedData
        sock_ReceivedData(data, length)
    End Sub
End Class