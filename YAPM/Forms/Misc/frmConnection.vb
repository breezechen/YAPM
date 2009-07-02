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
        Call hideWithEchapKey(Me)
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
        Select Case Program.Connection.ConnectionType
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
            Call _frmMain.DisconnectFromMachine()
        Else
            With Program.Connection
                .ConnectionType = _connType
                If _connType = cConnection.TypeOfConnection.RemoteConnectionViaSocket Then
                    .SocketParameters = New cConnection.SocketConnectionParameters(System.Net.IPAddress.Parse(Me.txtServerIP.Text), REMOTE_PORT)
                ElseIf _connType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                    .WmiParameters = New cConnection.WMIConnectionParameters(Me.txtServerMachine.Text, Me.txtServerUser.Text, Me.txtServerPassword.Text)
                End If
            End With
            Me.Text = "Connecting to machine..."
            Call _frmMain.ConnectToMachine()
        End If

    End Sub

    ' Change the caption of the button 'Connect/Disconnect'
    Private Sub ChangeCaption()
        Static _oldType As cConnection.TypeOfConnection = cConnection.TypeOfConnection.LocalConnection
        If _formConnectionReference.IsConnected Then
            Me.cmdConnect.Text = "Disconnect"
            Me.Text = "Connected"
        Else
            Me.cmdConnect.Text = "Connect"
            Me.Text = "Disconnected"
        End If
        Me.gpShutdown.Enabled = _formConnectionReference.IsConnected

        If _oldType <> _formConnectionReference.ConnectionType Then
            ' Changed connection type --> changed shutdown options
            _oldType = _formConnectionReference.ConnectionType
            Select Case _oldType
                Case cConnection.TypeOfConnection.LocalConnection
                    Me.cbShutdown.Items.Clear()
                    Dim _items() As String = {"Restart", "Shutdown", "Poweroff", "Sleep", "Logoff", "Lock"}
                    Me.cbShutdown.Items.AddRange(_items)
                    Me.gpShutdown.Text = "Shutdown local system"
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    Me.cbShutdown.Items.Clear()
                    Dim _items() As String = {"Restart", "Shutdown", "Poweroff", "Sleep", "Logoff", "Lock"}
                    Me.cbShutdown.Items.AddRange(_items)
                    Me.gpShutdown.Text = "Shutdown remote system via socket"
                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    Me.cbShutdown.Items.Clear()
                    Dim _items() As String = {"Restart", "Shutdown", "Poweroff", "Logoff"}
                    Me.cbShutdown.Items.AddRange(_items)
                    Me.gpShutdown.Text = "Shutdown remote system via WMI"
            End Select
        End If
    End Sub

    ' BAD WAY (because of withevents, this is raised JUST WHEN Program.Connection.Connect
    ' is call. BAD THING (should wait asyncMethod, but there are LOTS of asyncMethids
    ' (one for each lvItem).
    'Private Sub _formConnectionReference_Connected() Handles _formConnectionReference.Connected
    '    Call ChangeCaption()
    'End Sub
    'Private Sub _formConnectionReference_Disconnected() Handles _formConnectionReference.Disconnected
    '    Call ChangeCaption()
    'End Sub

    ' BAD WAY -> should kick all timers from project (see commented lines about delegates
    ' just below)
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Call ChangeCaption()
    End Sub

    Private Sub frmConnection_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        Me.Timer.Enabled = Me.Visible
    End Sub

    'Private Sub _formConnectionReference_Connected() Handles _formConnectionReference.Connected
    '    Try
    '        Dim _deg As New degActivateShutdown(AddressOf ActivateShutdown)
    '        Me.Invoke(_deg, True)
    '    Catch ex As Exception
    '        '
    '    End Try
    'End Sub

    'Private Sub _formConnectionReference_Disconnected() Handles _formConnectionReference.Disconnected
    '    Try
    '        Dim _deg As New degActivateShutdown(AddressOf ActivateShutdown)
    '        Me.Invoke(_deg, False)
    '    Catch ex As Exception
    '        '
    '    End Try
    'End Sub

    'Private Delegate Sub degActivateShutdown(ByVal value As Boolean)
    'Private Sub ActivateShutdown(ByVal value As Boolean)
    '    Me.gpShutdown.Enabled = value
    'End Sub



#Region "Shutdown functions"

    Private _shutdownAction As asyncCallbackShutdownAction
    Public Function ShutdownAction(ByVal type As asyncCallbackShutdownAction.ShutdownType, ByVal force As Boolean) As Integer

        If _shutdownAction Is Nothing Then
            _shutdownAction = New asyncCallbackShutdownAction(New asyncCallbackShutdownAction.HasShutdowned(AddressOf shutdownDone), Program._frmMain._shutdownConnection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _shutdownAction.Process)

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackShutdownAction.poolObj(type, force))

    End Function
    Private Sub shutdownDone(ByVal Success As Boolean, ByVal type As asyncCallbackShutdownAction.ShutdownType, ByVal msg As String)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not send " & type.ToString & " command")
        End If
    End Sub

    Private Sub cmdShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShutdown.Click
        Select Case Me.cbShutdown.Text
            Case "Restart"
                Call ShutdownAction(asyncCallbackShutdownAction.ShutdownType.Restart, Me.chkForceShutdown.Checked)
            Case "Shutdown"
                Call ShutdownAction(asyncCallbackShutdownAction.ShutdownType.Shutdown, Me.chkForceShutdown.Checked)
            Case "Poweroff"
                Call ShutdownAction(asyncCallbackShutdownAction.ShutdownType.Poweroff, Me.chkForceShutdown.Checked)
            Case "Sleep"
                Call ShutdownAction(asyncCallbackShutdownAction.ShutdownType.Sleep, Me.chkForceShutdown.Checked)
            Case "Logoff"
                Call ShutdownAction(asyncCallbackShutdownAction.ShutdownType.Logoff, Me.chkForceShutdown.Checked)
            Case "Lock"
                Call ShutdownAction(asyncCallbackShutdownAction.ShutdownType.Lock, Me.chkForceShutdown.Checked)
        End Select
    End Sub

#End Region

End Class