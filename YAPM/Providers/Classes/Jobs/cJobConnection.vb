﻿' =======================================================
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

Public Class cJobConnection
    Inherits cGeneralConnection

    Friend Shared instanceId As Integer = 1
    Private _instanceId As Integer = 1
    Private _jobEnum As asyncCallbackJobEnumerate
    Private _procInJobEnum As asyncCallbackProcessesInJobEnumerate

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection, ByRef de As HasEnumeratedEventHandler)
        MyBase.New(ControlWhichGetInvoked, Conn)
        instanceId += 1
        _instanceId = instanceId
        _jobEnum = New asyncCallbackJobEnumerate(_control, de, Me, _instanceId)
    End Sub

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection, ByRef de As HasEnumeratedProcInJobEventHandler)
        MyBase.New(ControlWhichGetInvoked, Conn)
        instanceId += 1
        _instanceId = instanceId
        _procInJobEnum = New asyncCallbackProcessesInJobEnumerate(_control, de, Me, _instanceId)
    End Sub


#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, cJob), ByVal errorMessage As String, ByVal instanceId As Integer)
    Public Delegate Sub HasEnumeratedProcInJobEventHandler(ByVal Success As Boolean, ByVal List As List(Of Integer), ByVal errorMessage As String, ByVal instanceId As Integer)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler
    Public HasEnumerated As HasEnumeratedEventHandler
    Public HasEnumeratedProcessesInJob As HasEnumeratedProcInJobEventHandler

#End Region

#Region "Description of the type of connection"

    ' Connection
    Protected Overrides Sub asyncConnect(ByVal useless As Object)

        ' Connect
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' When we are here, the socket IS CONNECTED
                _sock = ConnectionObj.Socket
                _connected = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                _connected = True
                Try
                    If Connected IsNot Nothing Then
                        _control.Invoke(Connected, True)
                    End If
                Catch ex As Exception
                    '
                End Try
        End Select

    End Sub

    ' Disconnect
    Protected Overrides Sub asyncDisconnect(ByVal useless As Object)
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
        End Select
    End Sub

#End Region

#Region "Enumerate jobs"

    ' Enumerate jobs
    Public Function Enumerate(ByVal getFixedInfos As Boolean, Optional ByVal forInstanceId As Integer = -1) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
                System.Threading.WaitCallback(AddressOf _
                _jobEnum.Process), New  _
                asyncCallbackJobEnumerate.poolObj(forInstanceId))
    End Function

    ' Enumerate processes in a job
    Public Function EnumerateProcessesInJob(ByVal hJob As IntPtr, Optional ByVal forInstanceId As Integer = -1) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
                System.Threading.WaitCallback(AddressOf _
                _procInJobEnum.Process), New  _
                asyncCallbackProcessesInJobEnumerate.poolObj(hJob, forInstanceId))
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
            data.Order = cSocketData.OrderType.RequestNetworkConnectionList Then
            If _instanceId = data.InstanceId Then
                ' OK it is for me
                _jobEnum.GotListFromSocket(data.GetList, data.GetKeys)
            End If
        End If
    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class