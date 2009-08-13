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

Public Class asyncCallbackThreadSetPriority

    Private con As cThreadConnection
    Private _deg As HasSetPriority

    Public Delegate Sub HasSetPriority(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasSetPriority, ByRef procConnection As cThreadConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public level As System.Diagnostics.ThreadPriorityLevel
        Public newAction As Integer
        Public pid As Integer
        Public Sub New(ByVal _id As Integer, _
                        ByVal _level As System.Diagnostics.ThreadPriorityLevel, _
                       ByVal action As Integer, Optional ByVal procId As Integer = 0)
            newAction = action
            id = _id
            pid = procId
            level = _level
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ThreadSetPriority, pObj.pid, pObj.id, pObj.level)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As IntPtr
                Dim r As Boolean
                hProc = API.OpenThread(Native.Security.ThreadAccess.THREAD_SET_INFORMATION, False, pObj.id)
                If hProc <> IntPtr.Zero Then
                    r = API.SetThreadPriority(hProc, pObj.level)
                    API.CloseHandle(hProc)
                    _deg.Invoke(r, Native.Api.Win32.GetLastError, pObj.newAction)
                Else
                    _deg.Invoke(False, Native.Api.Win32.GetLastError, pObj.newAction)
                End If
        End Select
    End Sub

End Class
