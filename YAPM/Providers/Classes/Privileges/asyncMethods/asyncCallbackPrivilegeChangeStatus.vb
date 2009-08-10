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

Public Class asyncCallbackPrivilegeChangeStatus

    Private con As cPrivilegeConnection
    Private _deg As HasChangedStatus

    Public Delegate Sub HasChangedStatus(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasChangedStatus, ByRef procConnection As cPrivilegeConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public name As String
        Public status As API.PRIVILEGE_STATUS
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal nam As String, _
                       ByVal stat As API.PRIVILEGE_STATUS, _
                       ByVal act As Integer)
            name = nam
            newAction = act
            status = stat
            pid = pi
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.PrivilegeChangeStatus, pObj.pid, pObj.name, pObj.status)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = SetPrivilege(pObj.pid, pObj.name, pObj.status)
                _deg.Invoke(ret, pObj.pid, pObj.name, API.GetError, pObj.newAction)
        End Select
    End Sub


    ' Set privilege status
    Public Shared Function SetPrivilege(ByVal _pid As Integer, ByVal seName As String, ByVal Status As API.PRIVILEGE_STATUS) As Boolean

        Dim hProcess As IntPtr
        Dim Ret As Integer
        Dim fRet As Boolean = False
        Dim lngToken As IntPtr
        Dim typLUID As API.LUID
        Dim typTokenPriv As API.TOKEN_PRIVILEGES2
        Dim newTokenPriv As API.TOKEN_PRIVILEGES2

        hProcess = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION, False, _pid)
        If hProcess <> IntPtr.Zero Then
            API.OpenProcessToken(hProcess, API.TOKEN_RIGHTS.Query Or API.TOKEN_RIGHTS.AdjustPrivileges, lngToken)
            If lngToken <> IntPtr.Zero Then
                Ret = API.LookupPrivilegeValue(Nothing, seName, typLUID)
                If Ret > 0 Then
                    typTokenPriv.PrivilegeCount = 1
                    typTokenPriv.Privileges.Attributes = Status
                    typTokenPriv.Privileges.pLuid = typLUID
                    Dim size As Integer = 4 + typTokenPriv.PrivilegeCount * 12
                    Dim ret2 As Integer
                    fRet = API.AdjustTokenPrivileges(lngToken, 0, typTokenPriv, _
                                                     size, newTokenPriv, ret2)
                End If
                API.CloseHandle(lngToken)
            End If
            API.CloseHandle(hProcess)
        End If

        Return fRet
    End Function
End Class
