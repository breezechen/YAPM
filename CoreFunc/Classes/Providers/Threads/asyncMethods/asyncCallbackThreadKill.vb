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

Public Class asyncCallbackThreadKill

    Private _id As Integer
    Private _connection As cThreadConnection
    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal id As Integer, ByVal msg As String)

    Public Sub New(ByVal deg As HasKilled, ByVal id As Integer, ByRef procConnection As cThreadConnection)
        _id = id
        _deg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As Integer
                Dim ret As UInteger = -1
                hProc = API.OpenThread(API.THREAD_RIGHTS.THREAD_TERMINATE, 0, _id)
                If hProc > 0 Then
                    ret = API.TerminateThread(New IntPtr(hProc), 0)
                    API.CloseHandle(hProc)
                    _deg.Invoke(ret <> 0, 0, API.GetError)
                Else
                    _deg.Invoke(False, _id, API.GetError)
                End If
        End Select
    End Sub

End Class
