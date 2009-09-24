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

Public MustInherit Class cGeneralConnection

    ' We will invoke this control
    Protected _control As Control
    Protected WithEvents _sock As AsynchronousClient

    ' For WMI
    Friend wmiSearcher As Management.ManagementObjectSearcher

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection)
        _control = ControlWhichGetInvoked
        _conObj = Conn
        _sock = Conn.Socket     ' Get a reference to the general socket, so that it si
        ' possible to get an event in every cXXXXConnection when
        ' the socket is (dis)connected
    End Sub

#Region "Description of the type of connection"

    ' Attributes
    Protected _connected As Boolean = False
    Protected _conObj As cConnection

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
    Public ReadOnly Property Socket() As AsynchronousClient
        Get
            Return _sock
        End Get
    End Property

    ' Connection
    Public Sub Connect()
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
            System.Threading.WaitCallback(AddressOf _
            asyncConnect), Nothing)
    End Sub
    Protected MustOverride Sub asyncConnect(ByVal useless As Object)


    ' Disconnect
    Public Sub Disconnect()
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
            System.Threading.WaitCallback(AddressOf _
            asyncDisconnect), Nothing)
    End Sub
    Protected MustOverride Sub asyncDisconnect(ByVal useless As Object)

#End Region

#Region "Sock events"

    Protected MustOverride Sub _sock_Connected() Handles _sock.Connected
    Protected MustOverride Sub _sock_Disconnected() Handles _sock.Disconnected
    'Protected MustOverride Sub _sock_ReceivedData(ByRef data As cSocketData) Handles _sock.ReceivedData
    Protected MustOverride Sub _sock_SentData() Handles _sock.SentData

#End Region

End Class
