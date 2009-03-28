Option Strict On

Imports System.Net
Imports System.Net.Sockets
Imports RemoteControl

Public Class frmServeur

    Private WithEvents sock As New cAsyncSocketServer(Me)
    Private Const PORT As Integer = 8081
    Private _readyToLeave As Boolean = True

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
                    Case cSocketData.OrderType.CreateNew
                        Shell("explorer.exe")
                    Case cSocketData.OrderType.Kill
                        CoreFunc.cLocalProcess.Kill(cData.Param1)
                    Case cSocketData.OrderType.RequestProcessList
                        Call sendProcList()
                    Case cSocketData.OrderType.Resume
                        Dim p As New CoreFunc.cLocalProcess(cData.Param1)
                        p.ResumeProcess()
                    Case cSocketData.OrderType.Suspend
                        Dim p As New CoreFunc.cLocalProcess(cData.Param1)
                        p.SuspendProcess()
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' Send the process list
    Private Sub sendProcList()

        ' Get the list
        Dim key() As String = Nothing    ' Unused
        Dim _dico As New Dictionary(Of String, CoreFunc.cProcess.LightProcess)
        CoreFunc.cLocalProcess.Enumerate(key, _dico)

        Dim _theDico() As cSocketData.LightProcess
        ReDim _theDico(_dico.Count - 1)
        Dim x As Integer = 0
        For Each proc As CoreFunc.cProcess.LightProcess In _dico.Values
            _theDico(x) = New cSocketData.LightProcess(proc.name, proc.pid, "OK no need", "OK no need")
            x += 1
        Next

        ' Send data to client
        Try
            Dim cDat As New cSocketData(cSocketData.DataType.DataDictionnaryOfStringObject)
            cDat.SetProcessList(_theDico)
            Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
            sock.Send(buff, buff.Length)
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
    End Sub

    Private Sub sock_ReceivedData1(ByRef data() As Byte, ByVal length As Integer) Handles sock.ReceivedData
        sock_ReceivedData(data, length)
    End Sub
End Class