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

Public Class asyncCallbackPrivilegeChangeStatus

    Private _pid As Integer
    Private _name As String
    Private _status As API.PRIVILEGE_STATUS
    Private _connection As cPrivilegeConnection
    Private _deg As HasChangedStatus

    Public Delegate Sub HasChangedStatus(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal deg As HasChangedStatus, ByVal pid As Integer, ByVal status As API.PRIVILEGE_STATUS, ByVal name As String, ByRef procConnection As cPrivilegeConnection)
        _pid = pid
        _deg = deg
        _name = name
        _status = status
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = SetPrivilege(_name, _status)
                _deg.Invoke(ret, _pid, _name, API.GetError)
        End Select
    End Sub


    ' Set privilege status
    Private Function SetPrivilege(ByVal seName As String, ByVal Status As API.PRIVILEGE_STATUS) As Boolean

        Dim hProcess As Integer
        Dim Ret As Integer
        Dim fRet As Boolean = False
        Dim lngToken As Integer
        Dim typLUID As API.LUID
        Dim typTokenPriv As API.TOKEN_PRIVILEGES2
        Dim newTokenPriv As API.TOKEN_PRIVILEGES2

        hProcess = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION, 0, _pid)
        If hProcess > 0 Then
            API.OpenProcessToken(hProcess, API.TOKEN_ADJUST_PRIVILEGES Or API.TOKEN_QUERY, lngToken)
            If lngToken > 0 Then
                Ret = API.LookupPrivilegeValue(Nothing, seName, typLUID)
                If Ret > 0 Then
                    typTokenPriv.PrivilegeCount = 1
                    typTokenPriv.Privileges.Attributes = Status
                    typTokenPriv.Privileges.pLuid = typLUID
                    Dim size As Integer = 4 + typTokenPriv.PrivilegeCount * 12
                    Dim ret2 As Integer
                    fRet = API.AdjustTokenPrivileges(lngToken, 0, typTokenPriv, size, newTokenPriv, ret2)
                End If
                API.CloseHandle(lngToken)
            End If
            API.CloseHandle(hProcess)
        End If

        Return fRet
    End Function
End Class
