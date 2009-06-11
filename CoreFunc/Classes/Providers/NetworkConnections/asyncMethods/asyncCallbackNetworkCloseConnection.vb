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

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackNetworkCloseConnection

    Private con As cNetworkConnection
    Private _deg As HasClosedConnection

    Public Delegate Sub HasClosedConnection(ByVal Success As Boolean, ByVal localAddress As UInteger, ByVal localPort As Integer, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasClosedConnection, ByRef netConnection As cNetworkConnection)
        _deg = deg
        con = netConnection
    End Sub

    Public Structure poolObj
        Public localAddress As UInteger
        Public localPort As Integer
        Public remoteAddress As UInteger
        Public remotePort As Integer
        Public newAction As Integer
        Public Sub New(ByVal locAdd As UInteger, _
                       ByVal locPor As Integer, _
                       ByVal remAdd As UInteger, _
                       ByVal remPor As Integer, _
                       ByVal act As Integer)
            localAddress = locAdd
            localPort = locPor
            newAction = act
            remotePort = remPor
            remoteAddress = remAdd
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.TcpClose, pObj.localAddress, pObj.localPort, pObj.remoteAddress, pObj.remotePort)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim ret As Integer = CloseTcpConnection(pObj.localAddress, pObj.localPort, pObj.remoteAddress, pObj.remotePort)
                _deg.Invoke(ret = 0, pObj.localAddress, pObj.localPort, API.GetError, pObj.newAction)

        End Select
    End Sub

    Public Shared Function CloseTcpConnection(ByVal locAdd As UInteger, ByVal locPort As Integer, ByVal remAdd As UInteger, ByVal remPort As Integer) As Integer
        Dim row As New API.MibTcpRow
        With row
            .LocalAddress = locAdd
            .LocalPort = locPort
            .RemoteAddress = remAdd
            .RemotePort = remPort
            .State = API.MibTcpState.DeleteTcb
        End With

        Return API.SetTcpEntry(row)
    End Function

End Class
