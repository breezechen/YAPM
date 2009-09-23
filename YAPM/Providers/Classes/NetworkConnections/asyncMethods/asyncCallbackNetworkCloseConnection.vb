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

Public Class asyncCallbackNetworkCloseConnection

    Private con As cNetworkConnection
    Private _deg As HasClosedConnection

    Public Delegate Sub HasClosedConnection(ByVal Success As Boolean, ByVal local As IPEndPoint, ByVal remote As IPEndPoint, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasClosedConnection, ByRef netConnection As cNetworkConnection)
        _deg = deg
        con = netConnection
    End Sub

    Public Structure poolObj
        Public local As IPEndPoint
        Public remote As IPEndPoint
        Public newAction As Integer
        Public Sub New(ByVal loc As IPEndPoint, _
                       ByVal remo As IPEndPoint, _
                       ByVal act As Integer)
            local = loc
            remote = remo
            newAction = act
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.TcpClose, pObj.local, pObj.remote)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim ret As Integer = Native.Objects.Network.CloseTcpConnectionByIPEndPoints(pObj.local, pObj.remote)
                _deg.Invoke(ret = 0, pObj.local, pObj.remote, Native.Api.Win32.GetLastError, pObj.newAction)

        End Select
    End Sub

End Class
