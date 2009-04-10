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

Public Class asyncCallbackModuleUnload

    Private _pid As Integer
    Private _baseA As Integer
    Private _name As String
    Private _connection As cModuleConnection
    Private _deg As HasUnloadedModule

    Public Delegate Sub HasUnloadedModule(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal deg As HasUnloadedModule, ByVal pid As Integer, ByVal address As Integer, ByVal name As String, ByRef procConnection As cModuleConnection)
        _pid = pid
        _deg = deg
        _name = name
        _baseA = address
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_CREATE_THREAD, 0, _pid)

                If hProc > 0 Then
                    Dim kernel32 As Integer = API.GetModuleHandle("kernel32.dll")
                    Dim freeLibrary As Integer = API.GetProcAddress(kernel32, "FreeLibrary")
                    Dim threadId As Integer
                    Dim ret As Integer = API.CreateRemoteThread(hProc, 0, 0, freeLibrary, _baseA, 0, threadId)
                    _deg.Invoke(ret <> 0, _pid, _name, API.GetError)
                Else
                    _deg.Invoke(False, _pid, _name, API.GetError)
                End If
        End Select
    End Sub

End Class
