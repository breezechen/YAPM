﻿' =======================================================
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

Public Class asyncCallbackProcKill

    Private _pid As Integer
    Private _connection As cProcessConnection
    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String)

    Public Sub New(ByVal deg As HasKilled, ByVal pid As Integer, ByRef procConnection As cProcessConnection)
        _pid = pid
        _deg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim _theProcess As Management.ManagementObject = Nothing
                    For Each pp As Management.ManagementObject In _connection.wmiSearcher.Get
                        If CInt(pp("ProcessId")) = _pid Then
                            _theProcess = pp
                            Exit For
                        End If
                    Next
                    If _theProcess IsNot Nothing Then
                        Dim ret As Integer = 0
                        ret = CInt(_theProcess.InvokeMethod("Terminate", Nothing))
                        _deg.Invoke(ret = 0, _pid, CType(ret, API.PROCESS_RETURN_CODE_WMI).ToString)
                    Else
                        _deg.Invoke(False, _pid, "Internal error")
                    End If
                Catch ex As Exception
                    _deg.Invoke(False, _pid, ex.Message)
                End Try

            Case Else
                ' Local
                Dim hProc As Integer
                Dim ret As Integer = -1
                hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_TERMINATE, 0, _pid)
                If hProc > 0 Then
                    ret = API.TerminateProcess(hProc, 0)
                    API.CloseHandle(hProc)
                    _deg.Invoke(ret <> 0, 0, API.GetError)
                Else
                    _deg.Invoke(False, _pid, API.GetError)
                End If
        End Select
    End Sub

End Class
