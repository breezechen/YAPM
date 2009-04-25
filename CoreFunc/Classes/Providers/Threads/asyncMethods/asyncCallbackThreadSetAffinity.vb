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

Public Class asyncCallbackThreadSetAffinity

    Private con As cThreadConnection
    Private _deg As HasSetAffinity

    Public Delegate Sub HasSetAffinity(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasSetAffinity, ByRef procConnection As cThreadConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public level As Integer
        Public newAction As Integer
        Public Sub New(ByVal _id As Integer, _
                        ByVal _level As Integer, _
                       ByVal action As Integer)
            newAction = action
            id = _id
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

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim __hProcess As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_INFORMATION, 0, pObj.id)
                If __hProcess > 0 Then
                    Dim ret As Integer = API.SetProcessAffinityMask(__hProcess, pObj.level)
                    API.CloseHandle(__hProcess)
                    _deg.Invoke(ret <> 0, API.GetError, pObj.newAction)
                Else
                    _deg.Invoke(False, API.GetError, pObj.newAction)
                End If
        End Select
    End Sub

End Class
