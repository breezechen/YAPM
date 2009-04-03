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
    End Enum

    ' Parameters for a socket connection
    Public Structure SocketConnectionParameters
        Public port As Integer
        Public address As IPAddress
        Public Sub New(ByVal theAddress As IPAddress, ByVal thePort As Integer)
            port = thePort
            address = theAddress
        End Sub
    End Structure

    ' Parameters for a socket connection
    Public Structure WMIConnectionParameters
        Public userName As String
        Public password As String
        Public serverName As String
        Public Sub New(ByVal server As String, ByVal user As String, ByVal pass As String)
            userName = user
            password = pass
            serverName = server
        End Sub
    End Structure


    Private _conType As TypeOfConnection
    Private _conSocket As SocketConnectionParameters
    Private _conWMI As WMIConnectionParameters
    Private _isConnected As Boolean

    Public Property ConnectionType() As TypeOfConnection
        Get
            Return _conType
        End Get
        Set(ByVal value As TypeOfConnection)
            _conType = value
        End Set
    End Property

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

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _isConnected
        End Get
    End Property


    ' BAD WAY (because of withevents, this is raised JUST WHEN frmMain.theConnection.Connect
    ' is call. BAD THING (should wait asyncMethod, but there are LOTS of asyncMethids
    ' (one for each lvItem).
    Public Sub Connect()
        _isConnected = True
        RaiseEvent Connected()
    End Sub
    Public Sub Disconnect()
        _isConnected = False
        RaiseEvent Disconnected()
    End Sub

End Class
