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
Imports System.Management

Public Class asyncCallbackServiceSetStartType

    Private _name As String
    Private _connection As cServiceConnection
    Private _type As API.SERVICE_START_TYPE
    Private _deg As HasChangedStartType

    Private Shared _syncLockObj As New Object

    Public Delegate Sub HasChangedStartType(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal deg As HasChangedStartType, ByVal name As String, ByVal type As API.SERVICE_START_TYPE, ByRef procConnection As cServiceConnection)
        _name = name
        _deg = deg
        _type = type
        _connection = procConnection
    End Sub

    Public Sub Process()
        SyncLock _syncLockObj
            Select Case _connection.ConnectionObj.ConnectionType
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    Try
                        Dim res As Integer = 2        ' Access denied
                        For Each srv As ManagementObject In _connection.wmiSearcher.Get
                            If CStr(srv.GetPropertyValue(API.WMI_INFO_SERVICE.Name.ToString)) = _name Then
                                Dim inParams As ManagementBaseObject = srv.GetMethodParameters("ChangeStartMode")
                                inParams("StartMode") = getWMIStartMode(_type)
                                Dim outParams As ManagementBaseObject = srv.InvokeMethod("ChangeStartMode", inParams, Nothing)
                                res = CInt(outParams("ReturnValue"))
                                Exit For
                            End If
                        Next
                        _deg.Invoke(res = 0, _name, CType(res, API.SERVICE_RETURN_CODE_WMI).ToString)
                    Catch ex As Exception
                        _deg.Invoke(False, _name, ex.Message)
                    End Try

                Case Else
                    ' Local
                    Dim hLockSCManager As Integer
                    Dim hSCManager As IntPtr = _connection.SCManagerLocalHandle
                    Dim lServ As IntPtr = API.OpenService(hSCManager, _name, API.SERVICE_RIGHTS.SERVICE_CHANGE_CONFIG)
                    Dim ret As Boolean = False

                    hLockSCManager = API.LockServiceDatabase(CInt(hSCManager))

                    If hSCManager <> IntPtr.Zero Then
                        If lServ <> IntPtr.Zero Then
                            ret = API.ChangeServiceConfig(CInt(lServ), API.SERVICE_NO_CHANGE, _type, _
                                API.SERVICE_NO_CHANGE, vbNullString, vbNullString, Nothing, _
                                vbNullString, vbNullString, vbNullString, vbNullString)
                            API.CloseServiceHandle(lServ)
                        End If
                        API.UnlockServiceDatabase(hLockSCManager)
                    End If

                    _deg.Invoke(ret, _name, API.GetError)
            End Select
        End SyncLock
    End Sub

    Private Function getWMIStartMode(ByVal s As API.SERVICE_START_TYPE) As String
        Select Case s
            Case API.SERVICE_START_TYPE.AutoStart
                Return "Automatic"
            Case API.SERVICE_START_TYPE.BootStart
                Return "Boot"
            Case API.SERVICE_START_TYPE.DemandStart
                Return "Manual"
            Case API.SERVICE_START_TYPE.StartDisabled
                Return "Disabled"
            Case API.SERVICE_START_TYPE.SystemStart
                Return "System"
            Case Else
                Return Nothing
        End Select
    End Function

End Class
