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
Imports System.Management

Public Class asyncCallbackServiceSetStartType

    Private Shared _syncLockObj As New Object
    Private con As cServiceConnection
    Private _deg As HasChangedStartType

    Public Delegate Sub HasChangedStartType(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasChangedStartType, ByRef procConnection As cServiceConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public name As String
        Public type As API.SERVICE_START_TYPE
        Public newAction As Integer
        Public Sub New(ByVal nam As String, _
                       ByVal typ As API.SERVICE_START_TYPE, _
                       ByVal act As Integer)
            name = nam
            newAction = act
            type = typ
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        SyncLock _syncLockObj
            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If con.ConnectionObj.IsConnected = False Then
                Exit Sub
            End If

            Select Case con.ConnectionObj.ConnectionType
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ServiceChangeServiceStartType, pObj.name, pObj.type)
                        con.ConnectionObj.Socket.Send(cDat)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    Try
                        Dim res As Integer = 2        ' Access denied
                        For Each srv As ManagementObject In con.wmiSearcher.Get
                            If CStr(srv.GetPropertyValue(API.WMI_INFO_SERVICE.Name.ToString)) = pObj.name Then
                                Dim inParams As ManagementBaseObject = srv.GetMethodParameters("ChangeStartMode")
                                inParams("StartMode") = getWMIStartMode(pObj.type)
                                Dim outParams As ManagementBaseObject = srv.InvokeMethod("ChangeStartMode", inParams, Nothing)
                                res = CInt(outParams("ReturnValue"))
                                Exit For
                            End If
                        Next
                        _deg.Invoke(res = 0, pObj.name, CType(res, API.SERVICE_RETURN_CODE_WMI).ToString, pObj.newAction)
                    Catch ex As Exception
                        _deg.Invoke(False, pObj.name, ex.Message, pObj.newAction)
                    End Try

                Case Else
                    ' Local
                    Dim hLockSCManager As IntPtr
                    Dim hSCManager As IntPtr = con.SCManagerLocalHandle
                    Dim lServ As IntPtr = API.OpenService(hSCManager, pObj.name, API.SERVICE_RIGHTS.SERVICE_CHANGE_CONFIG)
                    Dim ret As Boolean = False

                    hLockSCManager = API.LockServiceDatabase(hSCManager)

                    If hSCManager <> IntPtr.Zero Then
                        If lServ <> IntPtr.Zero Then
                            ret = API.ChangeServiceConfig(lServ, API.SERVICE_TYPE.NoChange, _
                                            pObj.type, _
                                            API.SERVICE_ERROR_CONTROL.NoChange, _
                                            Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing)
                            API.CloseServiceHandle(lServ)
                        End If
                        API.UnlockServiceDatabase(hLockSCManager)
                    End If

                    _deg.Invoke(ret, pObj.name, Native.Api.Functions.GetError, pObj.newAction)
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
