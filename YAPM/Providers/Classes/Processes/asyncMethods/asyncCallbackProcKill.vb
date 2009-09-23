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

Public Class asyncCallbackProcKill

    Private con As cProcessConnection
    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasKilled, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal act As Integer)
            newAction = act
            pid = pi
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessKill, pObj.pid)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim msg As String = ""
                Dim ret As Boolean = _
                    Wmi.Objects.Process.KillProcessById(pObj.pid, con.wmiSearcher, msg)

                Try
                    _deg.Invoke(ret, pObj.pid, msg, pObj.newAction)
                Catch ex As Exception
                    _deg.Invoke(False, pObj.pid, ex.Message, pObj.newAction)
                End Try

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Process.KillProcessById(pObj.pid)
                _deg.Invoke(ret, pObj.pid, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
