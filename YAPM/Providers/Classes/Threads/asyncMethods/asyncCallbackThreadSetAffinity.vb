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

Public Class asyncCallbackThreadSetAffinity

    Private _deg As HasSetAffinity

    Public Delegate Sub HasSetAffinity(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasSetAffinity)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public level As IntPtr
        Public newAction As Integer
        Public pid As Integer
        Public Sub New(ByVal _id As Integer, _
                        ByVal _level As Integer, _
                        ByVal action As Integer, Optional ByVal procId As Integer = 0)
            newAction = action
            id = _id
            level = New IntPtr(_level)
            pid = procId
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ThreadSetAffinity, pObj.pid, pObj.id, pObj.level)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Thread.SetThreadAffinityById(pObj.id, _
                                                                                 pObj.level)
                _deg.Invoke(ret, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
