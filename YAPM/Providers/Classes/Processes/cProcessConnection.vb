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

Public Class cProcessConnection
    Inherits cGeneralConnection

    Friend Shared instanceId As Integer = 1
    Private _instanceId As Integer = 1
    Dim _procEnum As asyncCallbackProcEnumerate
    Dim _enumMethod As asyncCallbackProcEnumerate.ProcessEnumMethode = asyncCallbackProcEnumerate.ProcessEnumMethode.VisibleProcesses

    ' For processor count
    Private Shared _processors As Integer = 1

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection, ByRef de As HasEnumeratedEventHandler)
        MyBase.New(ControlWhichGetInvoked, Conn)
        instanceId += 1
        _instanceId = instanceId
        _procEnum = New asyncCallbackProcEnumerate(_control, de, Me, _instanceId)
    End Sub

#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, processInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler
    Public HasEnumerated As HasEnumeratedEventHandler

#End Region

#Region "Properties"

    Public Shared ReadOnly Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
    End Property
    Public Property EnumMethod() As asyncCallbackProcEnumerate.ProcessEnumMethode
        Get
            Return _enumMethod
        End Get
        Set(ByVal value As asyncCallbackProcEnumerate.ProcessEnumMethode)
            _enumMethod = value
        End Set
    End Property

#End Region

#Region "Description of the type of connection"


    ' Connection
    Protected Overrides Sub asyncConnect(ByVal useless As Object)

        _processors = 0         ' Reinit processor count

        ' Connect
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' When we are here, the socket IS CONNECTED
                _sock = ConnectionObj.Socket
                _connected = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.SecurePassword = _conObj.WmiParameters.password
                __con.Username = _conObj.WmiParameters.userName

                Try
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Process")
                    wmiSearcher.Scope = New Management.ManagementScope("\\" & _conObj.WmiParameters.serverName & "\root\cimv2", __con)
                    _connected = True
                Catch ex As Exception
                    '
                End Try

            Case Else
                ' Local
                _connected = True
                If Connected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Connected, True)
        End Select


        ' Get processor count
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' We will try to retrieve processor count each time we GET data
                ' from server (if procCount is still 0), because if we do it
                ' HERE, the connection is not well initialized at this point
                ' (i.e. _idToSend has not been sent by the server)

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim objSearcherSystem As ManagementObjectSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")
                    objSearcherSystem.Scope = wmiSearcher.Scope
                    Dim _count As Integer = 0
                    For Each res As Management.ManagementObject In objSearcherSystem.Get
                        _count += 1
                    Next
                    _processors = _count
                Catch ex As Exception
                    MsgBox("Cannot get informations about system : " & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "WMI connection")
                    _processors = 1
                End Try

            Case Else
                ' Local
                _processors = cSystemInfo.GetProcessorCount
        End Select

    End Sub

    ' Disconnect
    Protected Overrides Sub asyncDisconnect(ByVal useless As Object)

        _processors = 0     ' Reinit processor count

        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
            Case Else
                ' Local
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
        End Select
    End Sub

#End Region

#Region "Enumerate processes"

    ' Enumerate processes
    Public Function Enumerate(ByVal getFixedInfos As Boolean, Optional ByVal forInstanceId As Integer = -1, Optional ByVal enumMethod As asyncCallbackProcEnumerate.ProcessEnumMethode = asyncCallbackProcEnumerate.ProcessEnumMethode.VisibleProcesses) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
                System.Threading.WaitCallback(AddressOf _
                _procEnum.Process), New  _
                asyncCallbackProcEnumerate.poolObj(forInstanceId, _enumMethod))
    End Function

#End Region

#Region "Sock events"

    Protected Overrides Sub _sock_Connected() Handles _sock.Connected
        _connected = True
    End Sub

    Protected Overrides Sub _sock_Disconnected() Handles _sock.Disconnected
        _connected = False
    End Sub

    Protected Overrides Sub _sock_ReceivedData(ByRef data As cSocketData) Handles _sock.ReceivedData

        If _processors = 0 Then
            ' Send the request
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestProcessorCount)
                _conObj.Socket.Send(cDat)
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            If data.Type = cSocketData.DataType.Order AndAlso _
                data.Order = cSocketData.OrderType.ReturnProcessorCount Then
                _processors = CInt(data.Param1)
            End If
        End If

        ' OK, THIS IS NOT THE BEST WAY TO AVOID THE BUG
        Static _antiEcho As Boolean = False
        _antiEcho = Not (_antiEcho)
        If _antiEcho Then
            Exit Sub
        End If
        ' OK, THIS IS NOT THE BEST WAY TO AVOID THE BUG

        If data Is Nothing Then
            Trace.WriteLine("Serialization error")
            Exit Sub
        End If

        If data.Type = cSocketData.DataType.RequestedList AndAlso _
            data.Order = cSocketData.OrderType.RequestProcessList Then
            If _instanceId = data.InstanceId Then
                ' OK it is for me
                _procEnum.GotListFromSocket(data.GetList, data.GetKeys)
            End If
        End If

    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
