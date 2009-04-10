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

Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Windows.Forms
Imports System.Management
Imports System.Net.Sockets
Imports System.Text

Public Class cServiceConnection

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    ' We will invoke this control
    Private _control As Control

    ' For WMI
    Friend wmiSearcher As Management.ManagementObjectSearcher

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection)
        _control = ControlWhichGetInvoked
        _conObj = Conn
    End Sub


#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler
    Public HasEnumerated As HasEnumeratedEventHandler

#End Region

#Region "Description of the type of connection"

    ' Attributes
    Private hSCM As IntPtr
    Private _connected As Boolean = False
    Private _conObj As cConnection
    Private WithEvents _sock As RemoteControl.cAsyncSocket

    Public ReadOnly Property SCManagerLocalHandle() As IntPtr
        Get
            Return hSCM
        End Get
    End Property
    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _connected
        End Get
    End Property
    Public Property ConnectionObj() As cConnection
        Get
            Return _conObj
        End Get
        Set(ByVal value As cConnection)
            If _connected = False Then
                _conObj = value
            End If
        End Set
    End Property

    ' Connection
    Public Sub Connect()
        Dim t As New Threading.Thread(AddressOf asyncConnect)
        t.Priority = Threading.ThreadPriority.Highest
        t.IsBackground = True
        t.Name = "Connect"
        t.Start()
    End Sub
    Public Sub asyncConnect()

        ' Connect
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.Password = _conObj.WmiParameters.password
                __con.Username = _conObj.WmiParameters.userName

                Try
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Service")
                    wmiSearcher.Scope = New Management.ManagementScope("\\" & _conObj.WmiParameters.serverName & "\root\cimv2", __con)
                    _connected = True
                Catch ex As Exception
                    '
                End Try

            Case Else
                ' Local
                If hSCM = IntPtr.Zero Then
                    hSCM = API.OpenSCManager(vbNullString, vbNullString, API.SC_MANAGER_ENUMERATE_SERVICE)
                End If
                _connected = True
                _control.Invoke(Connected, True)
        End Select

    End Sub

    ' Disconnect
    Public Sub Disconnect()
        Dim t As New Threading.Thread(AddressOf asyncDisconnect)
        t.Priority = Threading.ThreadPriority.Highest
        t.Name = "Disconnect"
        t.IsBackground = True
        t.Start()
    End Sub
    Public Sub asyncDisconnect()
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                _connected = False
                _control.Invoke(Disconnected, True)
            Case Else
                ' Local
                If hSCM.ToInt32 > 0 Then
                    Call API.CloseServiceHandle(hSCM)
                End If
                _connected = False
                _control.Invoke(Disconnected, True)
        End Select
    End Sub

#End Region

#Region "Enumerate services"

    ' Enumerate services
    Public Function Enumerate(ByVal getFixedInfos As Boolean, ByVal pid As Integer, ByVal all As Boolean) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
                System.Threading.WaitCallback(AddressOf _
                asyncCallbackServiceEnumerate.Process), New  _
                asyncCallbackServiceEnumerate.poolObj(_control, HasEnumerated, Me, pid, all))
    End Function

#End Region

#Region "Sock events"

    Private Sub _sock_Connected() Handles _sock.Connected
        _connected = True
    End Sub

    Private Sub _sock_Disconnected() Handles _sock.Disconnected
        _connected = False
    End Sub

    Private Sub _sock_ReceivedData(ByRef data() As Byte, ByVal length As Integer) Handles _sock.ReceivedData
        '
    End Sub

    Private Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
