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
    Private _envCon As New cEnvVariableConnection(Me, theConnection)
    Private _handleCon As New cHandleConnection(Me, theConnection)
    Private _memoryCon As New cMemRegionConnection(Me, theConnection)
    Private _moduleCon As New cModuleConnection(Me, theConnection)
    Private _networkCon As New cNetworkConnection(Me, theConnection)
    Private _serviceCon As New cServiceConnection(Me, theConnection)
    Private _priviCon As New cPrivilegeConnection(Me, theConnection)
    Private _taskCon As New cTaskConnection(Me, theConnection)
    Private _threadCon As New cThreadConnection(Me, theConnection)
    Private _windowCon As New cWindowConnection(Me, theConnection)

    ' Connect to local machine
    Private Sub connectLocal()

        ' Set handlers
        _procCon.HasEnumerated = New cProcessConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedProcess)
        _networkCon.HasEnumerated = New cNetworkConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedNetwork)
        _serviceCon.HasEnumerated = New cServiceConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedService)
        _moduleCon.HasEnumerated = New cModuleConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedModule)
        _threadCon.HasEnumerated = New cThreadConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedThread)
        _handleCon.HasEnumerated = New cHandleConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedHandle)

        ' Set connection
        With theConnection
            .ConnectionType = cConnection.TypeOfConnection.LocalConnection
            .Connect()
        End With
        _procCon.ConnectionObj = theConnection
        _networkCon.ConnectionObj = theConnection
        _serviceCon.ConnectionObj = theConnection
        _networkCon.Connect()
        _moduleCon.Connect()
        _serviceCon.Connect()
        _threadCon.Connect()
        _handleCon.Connect()
        _procCon.Connect()

    End Sub

#Region "Has enumerated lists"

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

    Private Sub HasEnumeratedService(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestServiceList)
                cDat.SetServiceList(Dico)
                Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                sock.Send(buff, buff.Length)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            ' Send an error
        End If

    End Sub

    Private Sub HasEnumeratedThread(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, threadInfos), ByVal errorMessage As String)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestThreadList)
                cDat.SetThreadList(Dico)
                Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                sock.Send(buff, buff.Length)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            ' Send an error
        End If

    End Sub

    Private Sub HasEnumeratedModule(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, moduleInfos), ByVal errorMessage As String)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestModuleList)
                cDat.SetModuleList(Dico)
                Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                sock.Send(buff, buff.Length)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            ' Send an error
        End If

    End Sub

    Private Sub HasEnumeratedHandle(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, handleInfos), ByVal errorMessage As String)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestHandleList)
                cDat.SethandleList(Dico)
                Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                sock.Send(buff, buff.Length)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            ' Send an error
        End If

    End Sub

    Private Sub HasEnumeratedNetwork(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, networkInfos), ByVal errorMessage As String)

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestNetworkConnectionList)
                cDat.SetNetworkList(Dico)
                Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                sock.Send(buff, buff.Length)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            ' Send an error
        End If

    End Sub

#End Region

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

            If cData Is Nothing Then
                Trace.WriteLine("Serialization error")
                Exit Sub
            End If

            ' Extract the type of information we have to send
            If cData.Type = cSocketData.DataType.Order Then

                Select Case cData.Order
                    Case cSocketData.OrderType.RequestProcessList
                        Call _procCon.Enumerate(True)
                    Case cSocketData.OrderType.RequestNetworkConnectionList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Dim all As Boolean = CBool(cData.Param2)
                        Call _networkCon.Enumerate(True, pid, all)
                    Case cSocketData.OrderType.RequestServiceList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim all As Boolean = CBool(cData.Param2)
                        Call _serviceCon.Enumerate(True, pid, all)
                    Case cSocketData.OrderType.RequestModuleList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Call _moduleCon.Enumerate(True, pid)
                    Case cSocketData.OrderType.RequestThreadList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Call _threadCon.Enumerate(True, pid)
                    Case cSocketData.OrderType.RequestHandleList
                        Dim pid() As Integer = CType(cData.Param1, Integer())
                        Dim unn As Boolean = CBool(cData.Param2)
                        Call _handleCon.Enumerate(True, pid, unn)
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