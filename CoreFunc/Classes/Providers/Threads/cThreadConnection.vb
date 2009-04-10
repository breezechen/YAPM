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

Public Class cThreadConnection
    Inherits cGeneralConnection

    ' Rights to query infos with a handle
    Private Shared _minRights As API.THREAD_RIGHTS = API.THREAD_RIGHTS.THREAD_QUERY_INFORMATION

    Public Shared ReadOnly Property ThreadMinRights() As API.THREAD_RIGHTS
        Get
            Return _minRights
        End Get
    End Property

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection)
        MyBase.New(ControlWhichGetInvoked, Conn)
        If IsWindowsVista() Then
            _minRights = API.THREAD_RIGHTS.THREAD_SET_LIMITED_INFORMATION
        End If
    End Sub

#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, threadInfos), ByVal errorMessage As String)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler
    Public HasEnumerated As HasEnumeratedEventHandler

#End Region

#Region "Description of the type of connection"

    ' Connection
    Protected Overrides Sub asyncConnect()

        ' Connect
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.Password = _conObj.WmiParameters.password
                __con.Username = _conObj.WmiParameters.userName

                Try
                    'TOCHANGE
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Thread")
                    wmiSearcher.Scope = New Management.ManagementScope("\\" & _conObj.WmiParameters.serverName & "\root\cimv2", __con)
                    _connected = True
                Catch ex As Exception
                    '
                End Try

            Case Else
                ' Local
                _connected = True
                _control.Invoke(Connected, True)
        End Select

    End Sub

    ' Disconnect
    Protected Overrides Sub asyncDisconnect()
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                _connected = False
                _control.Invoke(Disconnected, True)
            Case Else
                ' Local
                _connected = False
                _control.Invoke(Disconnected, True)
        End Select
    End Sub

#End Region

#Region "Enumerate threads"

    ' Enumerate threads
    Public Function Enumerate(ByVal getFixedInfos As Boolean, ByRef pid() As Integer) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
                System.Threading.WaitCallback(AddressOf _
                asyncCallbackThreadEnumerate.Process), New  _
                asyncCallbackThreadEnumerate.poolObj(_control, HasEnumerated, Me, pid))
    End Function

#End Region

#Region "Sock events"

    Protected Overrides Sub _sock_Connected() Handles _sock.Connected
        _connected = True
    End Sub

    Protected Overrides Sub _sock_Disconnected() Handles _sock.Disconnected
        _connected = False
    End Sub

    Protected Overrides Sub _sock_ReceivedData(ByRef data() As Byte, ByVal length As Integer) Handles _sock.ReceivedData
        '
    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
