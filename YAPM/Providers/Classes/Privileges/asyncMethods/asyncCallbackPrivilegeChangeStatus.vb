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
Imports System.Text

Public Class asyncCallbackPrivilegeChangeStatus

    Private con As cPrivilegeConnection
    Private _deg As HasChangedStatus

    Public Delegate Sub HasChangedStatus(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasChangedStatus, ByRef procConnection As cPrivilegeConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public name As String
        Public status As Native.Api.NativeEnums.SePrivilegeAttributes
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal nam As String, _
                       ByVal stat As Native.Api.NativeEnums.SePrivilegeAttributes, _
                       ByVal act As Integer)
            name = nam
            newAction = act
            status = stat
            pid = pi
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If Program.Connection.IsConnected = False Then
            Exit Sub
        End If

        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.PrivilegeChangeStatus, pObj.pid, pObj.name, pObj.status)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Token.SetPrivilegeStatusByProcessId(pObj.pid, _
                                                                       pObj.name, _
                                                                       pObj.status)
                _deg.Invoke(ret, pObj.pid, pObj.name, Native.Api.Win32.GetLastError, _
                            pObj.newAction)
        End Select
    End Sub
    
End Class
