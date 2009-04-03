' =======================================================
' Yet Another Process Monitor (YAPM)
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

Public Class frmConnection

    Private Const REMOTE_PORT As Integer = 8081
    Private WithEvents _formConnectionReference As cConnection

    Private _localDesc As String = "Local connection monitors all processes and services running on the local machine."
    Private _wmiDesc As String = "Remote connection via WMI monitors all processes and services running on a remote machine. You will need a username and password of the remote machine, WMI needs to be installed on both machines (your machine and the remote machine), and your firewall have to accept the connection. Furthermore, not all informations and actions on your processes are available. If possible, you should use 'remote via YAPM server' instead."
    Private _serverDes As String = "Remote connection via WMI YAPM server monitors all processes and services running on a remote machine. You will need the IP address of the remote machine, and the associated port must be available (you might need to configure your firewall). You will also need to run yapmserv.exe on the remote machine. This is the best way, if possible, to monitor a remote machine."

    Public Sub New(ByRef connection As cConnection)
        InitializeComponent()
        _formConnectionReference = connection
    End Sub

    Private Sub optLocal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLocal.CheckedChanged
        Call changeInfos()
    End Sub

    Private Sub frmConnection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub frmConnection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call changeInfos()
        'Me.txtServerMachine.Text = My.Computer.Name
    End Sub

    Private Sub optServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optServer.CheckedChanged
        Call changeInfos()
    End Sub

    Private Sub optWMI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWMI.CheckedChanged
        Call changeInfos()
    End Sub

    Private Sub changeInfos()
        Me.gpServer.Visible = optServer.Checked
        Me.gpWMI.Visible = optWMI.Checked
        If optLocal.Checked Then
            Me.txtDesc.Text = _localDesc
        ElseIf optServer.Checked Then
            Me.txtDesc.Text = _serverDes
        Else
            Me.txtDesc.Text = _wmiDesc
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Select Case frmMain.theConnection.ConnectionType
            Case cConnection.TypeOfConnection.LocalConnection
                Me.optLocal.Checked = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Me.optServer.Checked = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Me.optWMI.Checked = True
        End Select
        Me.Hide()
        Call changeInfos()
    End Sub

    ' Here we (dis)connect !
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click

        Dim _connType As cConnection.TypeOfConnection
        If optLocal.Checked Then
            _connType = cConnection.TypeOfConnection.LocalConnection
        ElseIf optServer.Checked Then
            _connType = cConnection.TypeOfConnection.RemoteConnectionViaSocket
        Else
            _connType = cConnection.TypeOfConnection.RemoteConnectionViaWMI
        End If

        If _formConnectionReference.IsConnected Then
            Me.Text = "Disconnecting from current machine..."
            Call frmMain.DisconnectFromMachine()
        Else
            With frmMain.theConnection
                .ConnectionType = _connType
                If _connType = cConnection.TypeOfConnection.RemoteConnectionViaSocket Then
                    .SocketParameters = New cConnection.SocketConnectionParameters(System.Net.IPAddress.Parse(Me.txtServerIP.Text), CInt(Val(Me.txtServerIP.Text)))
                ElseIf _connType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                    .WmiParameters = New cConnection.WMIConnectionParameters(Me.txtServerMachine.Text, Me.txtServerUser.Text, Me.txtServerPassword.Text)
                End If
            End With
            Me.Text = "Connecting to machine..."
            Call frmMain.ConnectToToMachine()
        End If

    End Sub

    ' Change the caption of the button 'Connect/Disconnect'
    Private Sub ChangeCaption()
        If _formConnectionReference.IsConnected Then
            Me.cmdConnect.Text = "Disconnect"
            Me.Text = "Connected"
        Else
            Me.cmdConnect.Text = "Connect"
            Me.Text = "Disconnected"
        End If
    End Sub

    ' BAD WAY (because of withevents, this is raised JUST WHEN frmMain.theConnection.Connect
    ' is call. BAD THING (should wait asyncMethod, but there are LOTS of asyncMethids
    ' (one for each lvItem).
    Private Sub _formConnectionReference_Connected() Handles _formConnectionReference.Connected
        Call ChangeCaption()
    End Sub
    Private Sub _formConnectionReference_Disconnected() Handles _formConnectionReference.Disconnected
        Call ChangeCaption()
    End Sub

End Class