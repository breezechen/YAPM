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

Public Class asyncCallbackThreadDecreasePriority

    Private con As cThreadConnection
    Private _deg As HasDecreasedPriority

    Public Delegate Sub HasDecreasedPriority(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasDecreasedPriority, ByRef procConnection As cThreadConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public level As Integer
        Public newAction As Integer
        Public pid As Integer
        Public Sub New(ByVal _id As Integer, _
                        ByVal _level As Integer, _
                       ByVal action As Integer, Optional ByVal processId As Integer = 0)
            newAction = action
            id = _id
            level = _level
            pid = processId
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ThreadDecreasePriority, pObj.pid, pObj.id, pObj.level)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _level2 As System.Diagnostics.ThreadPriorityLevel
                Select Case pObj.level
                    Case ThreadPriorityLevel.AboveNormal
                        _level2 = ThreadPriorityLevel.Normal
                    Case ThreadPriorityLevel.BelowNormal
                        _level2 = ThreadPriorityLevel.Lowest
                    Case ThreadPriorityLevel.Highest
                        _level2 = ThreadPriorityLevel.AboveNormal
                    Case ThreadPriorityLevel.Idle
                        '
                    Case ThreadPriorityLevel.Lowest
                        _level2 = ThreadPriorityLevel.Idle
                    Case ThreadPriorityLevel.Normal
                        _level2 = ThreadPriorityLevel.BelowNormal
                    Case ThreadPriorityLevel.TimeCritical
                        _level2 = ThreadPriorityLevel.Highest
                End Select

                Dim r As Boolean = Native.Objects.Thread.SetThreadPriorityById(pObj.id, _level2)
                _deg.Invoke(r, Native.Api.Win32.GetLastError, pObj.newAction)

        End Select
    End Sub

End Class
