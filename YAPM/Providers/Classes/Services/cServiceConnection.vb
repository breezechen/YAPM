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

    'Friend Shared instanceId As Integer = 1
    'Private _instanceId As Integer = 1
    'Dim _servEnum As asyncCallbackServiceEnumerate

    'Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection)
    '    MyBase.New(ControlWhichGetInvoked, Conn)
    '    instanceId += 1
    '    _instanceId = instanceId
    '    _servEnum = New asyncCallbackServiceEnumerate(Me, _instanceId)
    'End Sub


    '#Region "Events, delegate, invoke..."

    '    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    '    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    '    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String, ByVal forII As Integer)

    '    Public Connected As ConnectedEventHandler
    '    Public Disconnected As DisconnectedEventHandler
    '    ' Public HasEnumerated As HasEnumeratedEventHandler

    '#End Region

#Region "Description of the type of connection"

    ' Attributes
    Private hSCM As IntPtr

    'Public ReadOnly Property SCManagerLocalHandle() As IntPtr
    '    Get
    '        Return hSCM
    '    End Get
    'End Property

    '' Connection
    'Protected Overrides Sub asyncConnect(ByVal useless As Object)

    '    ' Connect
    '    Select Case _conObj.Type
    '        Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
    '            ' When we are here, the socket IS CONNECTED
    '            _sock = ConnectionObj.Socket
    '            _connected = True

    '        Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

    '            'Dim __con As New ConnectionOptions
    '            '__con.Impersonation = ImpersonationLevel.Impersonate
    '            '__con.Password = Common.Misc.SecureStringToCharArray(_conObj.WmiParameters.password)
    '            '__con.Username = _conObj.WmiParameters.userName

    '            'Try
    '            '    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Service")
    '            '    wmiSearcher.Scope = New Management.ManagementScope("\\" & _conObj.WmiParameters.serverName & "\root\cimv2", __con)
    '            '    _connected = True
    '            'Catch ex As Exception
    '            '    Misc.ShowDebugError(ex)
    '            'End Try

    '        Case Else
    '            ' Local
    '            If hSCM.IsNull Then
    '                hSCM = Native.Objects.Service.GetSCManagerHandle(Native.Security.ServiceManagerAccess.EnumerateService)
    '            End If
    '            _connected = True
    '            Try
    '                If Connected IsNot Nothing AndAlso _control.Created Then _
    '                    _control.Invoke(Connected, True)
    '            Catch ex As Exception
    '                Misc.ShowDebugError(ex)
    '            End Try
    '    End Select

    'End Sub

    ' Disconnect
    'Protected Overrides Sub asyncDisconnect(ByVal useless As Object)
    '    Select Case _conObj.Type
    '        Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
    '            _connected = False
    '            If Disconnected IsNot Nothing AndAlso _control.Created Then _
    '                _control.Invoke(Disconnected, True)
    '        Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
    '            _connected = False
    '            If Disconnected IsNot Nothing AndAlso _control.Created Then _
    '                _control.Invoke(Disconnected, True)
    '        Case Else
    '            ' Local
    '            If hSCM.IsNotNull Then
    '                Native.Objects.Service.CloseSCManagerHandle(hSCM)
    '                hSCM = IntPtr.Zero
    '            End If
    '            _connected = False
    '            If Disconnected IsNot Nothing AndAlso _control.Created Then _
    '                _control.Invoke(Disconnected, True)
    '    End Select
    'End Sub

#End Region

    '#Region "Enumerate services"

    '    ' Enumerate services
    '    Public Function Enumerate(ByVal getFixedInfos As Boolean, ByVal complete As Boolean, Optional ByVal forInstanceId As Integer = -1) As Integer
    '        If _connected Then
    '            Call Threading.ThreadPool.QueueUserWorkItem(New  _
    '                    System.Threading.WaitCallback(AddressOf _
    '                    _servEnum.Process), New  _
    '                    asyncCallbackServiceEnumerate.poolObj(complete, forInstanceId))
    '        End If
    '    End Function

    '#End Region


End Class
