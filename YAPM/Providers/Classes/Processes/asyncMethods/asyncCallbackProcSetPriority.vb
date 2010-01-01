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
Imports System.Management

Public Class asyncCallbackProcSetPriority

    Private _deg As HasSetPriority

    Public Delegate Sub HasSetPriority(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasSetPriority)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public lvl As ProcessPriorityClass
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal level As ProcessPriorityClass, ByVal act As Integer)
            newAction = act
            pid = pi
            lvl = level
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessChangePriority, pObj.pid, pObj.lvl)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim msg As String = ""
                Dim ret As Boolean = _
                        Wmi.Objects.Process.SetProcessPriorityById(pObj.pid, pObj.lvl, ProcessProvider.wmiSearcher, msg)
                Try
                    _deg.Invoke(ret, msg, pObj.newAction)
                Catch ex As Exception
                    _deg.Invoke(False, ex.Message, pObj.newAction)
                End Try

            Case Else
                ' Local
                Dim r As Boolean = Native.Objects.Process.SetProcessPriorityById(pObj.pid, _
                                                                                 pObj.lvl)
                _deg.Invoke(r, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
