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

Public Class cConnection

    Public Const REMOTE_PORT As Integer = 8081

    Public Event Connected()
    Public Event Disconnected()

    ' Type of connection
    Public Enum TypeOfConnection
        [LocalConnection]
        [RemoteConnectionViaSocket]
        [RemoteConnectionViaWMI]
        [SnapshotFile]
    End Enum

    ' Parameters for a socket connection
    Public Structure SocketConnectionParameters
        Public port As Integer
        Public ServerName As String
        Public ClientIp As String
        Public Sub New(ByVal server As String, ByVal thePort As Integer, ByVal aClientIp As String)
            port = thePort
            ServerName = server
            ClientIp = aClientIp
        End Sub
    End Structure

    ' Parameters for a socket connection
    Public Structure WMIConnectionParameters
        Public userName As String
        Public password As System.Security.SecureString
        Public serverName As String
        Public Sub New(ByVal server As String, ByVal user As String, ByVal pass As System.Security.SecureString)
            userName = user
            password = pass
            serverName = server
        End Sub
    End Structure

    Private WithEvents _sock As AsynchronousClient
    Private _snap As cSnapshot
    Private _conType As TypeOfConnection
    Private _conSocket As SocketConnectionParameters
    Private _conWMI As WMIConnectionParameters
    Private _isConnected As Boolean
    Private _ssFile As String

    ' Snapshot file
    Public Property SnapshotFile() As String
        Get
            Return _ssFile
        End Get
        Set(ByVal value As String)
            _ssFile = value
        End Set
    End Property

    ' Snaphot object
    Public Property Snapshot() As cSnapshot
        Get
            Return _snap
        End Get
        Set(ByVal value As cSnapshot)
            _snap = value
        End Set
    End Property

    ' Socket
    Public ReadOnly Property Socket() As AsynchronousClient
        Get
            Return _sock
        End Get
    End Property

    ' Type of connection
    Public Property ConnectionType() As TypeOfConnection
        Get
            Return _conType
        End Get
        Set(ByVal value As TypeOfConnection)
            _conType = value
        End Set
    End Property

    ' Parameters for the socket
    Public Property SocketParameters() As SocketConnectionParameters
        Get
            Return _conSocket
        End Get
        Set(ByVal value As SocketConnectionParameters)
            If Not (_isConnected) Then
                _conSocket = value
            End If
        End Set
    End Property

    ' Parameters for WMI connection
    Public Property WmiParameters() As WMIConnectionParameters
        Get
            Return _conWMI
        End Get
        Set(ByVal value As WMIConnectionParameters)
            If Not (_isConnected) Then
                _conWMI = value
            End If
        End Set
    End Property

    ' Is connected ?
    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _isConnected
        End Get
    End Property

    ' ToString overriden
    Public Overrides Function ToString() As String
        Select Case _conType
            Case TypeOfConnection.LocalConnection
                Return "Localhost"
            Case TypeOfConnection.RemoteConnectionViaSocket
                If _conSocket.ServerName IsNot Nothing Then
                    Return _conSocket.ServerName.ToString & ":" & _conSocket.port & " (server)"
                Else
                    Return "[NO SERVER] :" & _conSocket.port & " (server)"
                End If
            Case TypeOfConnection.RemoteConnectionViaWMI
                Return _conWMI.serverName & " (WMI)"
            Case TypeOfConnection.SnapshotFile
                Return _snap.ToString
            Case Else
                Return "Unknown"
        End Select
    End Function

    ' Coef of refreshment for intervals
    Public ReadOnly Property RefreshmentCoefficient() As Double
        Get
            Return CDbl(If(_conType = TypeOfConnection.LocalConnection, 1, My.Settings.CoefTimeMul / 100))
        End Get
    End Property


    Public Sub New()
        _snap = New cSnapshot
    End Sub
    Public Sub New(ByRef ccon As cConnection)
        _conSocket = ccon.SocketParameters
        _conType = ccon.ConnectionType
        _conWMI = ccon.WmiParameters
        _snap = ccon.Snapshot
    End Sub

    Public Sub CopyFromInstance(ByRef ccon As cConnection)
        _conType = ccon.ConnectionType
        _conSocket = ccon.SocketParameters
        _conWMI = ccon.WmiParameters
        _snap = ccon.Snapshot
    End Sub

    ' BAD WAY (because of withevents, this is raised JUST WHEN Program.Connection.Connect
    ' is called. BAD THING (should wait asyncMethod, but there are LOTS of asyncMethids
    ' (one for each lvItem).
    Public Sub Connect()
        If Me.ConnectionType = TypeOfConnection.RemoteConnectionViaSocket Then
            If _sock Is Nothing Then
                _sock = New AsynchronousClient()
                _sock.Connect(_conSocket.ServerName, _conSocket.port, _conSocket.ClientIp)
            Else
                _isConnected = True
                RaiseEvent Connected()
            End If
        ElseIf Me.ConnectionType = TypeOfConnection.SnapshotFile Then
            If _isConnected = False Then
                _snap = New cSnapshot(Me.SnapshotFile)
                _isConnected = True
            End If
            RaiseEvent Connected()
        Else
            _isConnected = True
            RaiseEvent Connected()
        End If
    End Sub
    Public Sub Disconnect()
        If Me.ConnectionType = TypeOfConnection.RemoteConnectionViaSocket Then
            _sock.Disconnect()
        Else
            _isConnected = False
            RaiseEvent Disconnected()
        End If
    End Sub
    Public Sub DisconnectForce()        ' Set state as 'disconnected'
        _isConnected = False
        RaiseEvent Disconnected()
    End Sub

    Private Sub _sock_Connected() Handles _sock.Connected
        _isConnected = True
        RaiseEvent Connected()
    End Sub
    Private Sub _sock_Disconnected() Handles _sock.Disconnected
        _isConnected = False
        _sock = Nothing
        RaiseEvent Disconnected()
    End Sub

    Private Sub _sock_SentData() Handles _sock.SentData
        ' When we have sent datas
    End Sub

    ' When we receive datas
    Private Sub _sock_ReceivedData(ByRef cDat As cSocketData) Handles _sock.ReceivedData

        ' If we got an error, we display it
        Try
            If cDat.Type = cSocketData.DataType.ErrorOnServer Then
                Dim cErr As Exception = CType(cDat.Param1, SerializableException).GetException
                _frmMain.Invoke(New frmMain.GotErrorFromServer(AddressOf impGotErrorFromServer), cErr)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

        ' Now we add this entry to the list of received datas
        Try
            Program.ConnectionForm.Invoke(New frmConnection.AddItemToReceivedDataList(AddressOf Program.ConnectionForm.impAddItemToReceivedDataList), cDat)
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    ' Displays error
    Private Sub impGotErrorFromServer(ByVal err As Exception)
        If _isConnected Then
            Misc.ShowError(err, "The server sent a error.")
        End If
    End Sub

End Class
