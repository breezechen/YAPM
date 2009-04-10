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

Public Class asyncCallbackServiceShutdown

    Private _name As String
    Private _connection As cServiceConnection
    Private _deg As HasShutdowned

    Public Delegate Sub HasShutdowned(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal deg As HasShutdowned, ByVal name As String, ByRef procConnection As cServiceConnection)
        _name = name
        _deg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' ERK
                _deg.Invoke(False, _name, "Shutdown not possible via WMI...")

            Case Else
                ' Local
                Dim hSCManager As IntPtr = _connection.SCManagerLocalHandle
                Dim lServ As IntPtr = API.OpenService(hSCManager, _name, API.SERVICE_RIGHTS.SERVICE_STOP)    'TOCHANGE ?
                Dim res As Boolean = False
                If hSCManager <> IntPtr.Zero Then
                    If lServ <> IntPtr.Zero Then
                        Dim lpss As API.SERVICE_STATUS
                        res = API.ControlService(lServ, API.SERVICE_CONTROL._SHUTDOWN, lpss)
                        API.CloseServiceHandle(lServ)
                    End If
                End If
                _deg.Invoke(res, _name, API.GetError)
        End Select
    End Sub

End Class
