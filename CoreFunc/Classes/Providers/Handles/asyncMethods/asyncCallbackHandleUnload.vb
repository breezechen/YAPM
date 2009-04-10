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

Public Class asyncCallbackHandleUnload

    Private _pid As Integer
    Private _handle As Integer
    Private _connection As cHandleConnection
    Private _deg As HasUnloadedHandle

    Public Delegate Sub HasUnloadedHandle(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As Integer, ByVal msg As String)

    Public Sub New(ByVal deg As HasUnloadedHandle, ByVal pid As Integer, ByVal handle As Integer, ByRef procConnection As cHandleConnection)
        _pid = pid
        _deg = deg
        _handle = handle
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
              
        End Select
    End Sub

End Class
