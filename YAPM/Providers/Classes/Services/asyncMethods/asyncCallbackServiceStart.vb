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

Public Class asyncCallbackServiceStart

    Private con As cServiceConnection
    Private _deg As HasStarted

    Public Delegate Sub HasStarted(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasStarted, ByRef procConnection As cServiceConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public name As String
        Public newAction As Integer
        Public Sub New(ByVal nam As String, _
                       ByVal act As Integer)
            name = nam
            newAction = act
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ServiceStart, pObj.name)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim res As Integer = 2        ' Access denied
                    For Each srv As ManagementObject In con.wmiSearcher.Get
                        If CStr(srv.GetPropertyValue(API.WMI_INFO_SERVICE.Name.ToString)) = pObj.name Then
                            res = CInt(srv.InvokeMethod("StartService", Nothing))
                            Exit For
                        End If
                    Next
                    _deg.Invoke(res = 0, pObj.name, CType(res, API.SERVICE_RETURN_CODE_WMI).ToString, pObj.newAction)
                Catch ex As Exception
                    _deg.Invoke(False, pObj.name, ex.Message, pObj.newAction)
                End Try

            Case Else
                ' Local
                Dim hSCManager As IntPtr = con.SCManagerLocalHandle
                Dim lServ As IntPtr = API.OpenService(hSCManager, pObj.name, _
                                                      API.SERVICE_RIGHTS.SERVICE_START)
                Dim res As Boolean
                If hSCManager <> IntPtr.Zero Then
                    If lServ <> IntPtr.Zero Then
                        res = API.StartService(lServ, 0, Nothing)
                        API.CloseServiceHandle(lServ)
                    End If
                End If
                _deg.Invoke(res, pObj.name, Native.Api.Functions.GetError, pObj.newAction)
        End Select
    End Sub

End Class
