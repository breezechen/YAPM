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

    Private con As cThreadConnection
    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal id As Integer, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasKilled, ByRef procConnection As cThreadConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public newAction As Integer
        Public Sub New(ByVal _id As Integer, _
                       ByVal action As Integer)
            newAction = action
            id = _id
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As Integer
                Dim ret As UInteger = -1
                hProc = API.OpenThread(API.THREAD_RIGHTS.THREAD_TERMINATE, 0, pObj.id)
                If hProc > 0 Then
                    ret = API.TerminateThread(New IntPtr(hProc), 0)
                    API.CloseHandle(hProc)
                    _deg.Invoke(ret <> 0, 0, API.GetError, pObj.newAction)
                Else
                    _deg.Invoke(False, pObj.id, API.GetError, pObj.newAction)
                End If
        End Select
    End Sub

End Class
