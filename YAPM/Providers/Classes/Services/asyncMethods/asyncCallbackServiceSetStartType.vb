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
        Public type As Native.Api.NativeEnums.ServiceStartType
        Public newAction As Integer
        Public Sub New(ByVal nam As String, _
                       ByVal typ As Native.Api.NativeEnums.ServiceStartType, _
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
                            If CStr(srv.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.Name.ToString)) = pObj.name Then
                                Dim inParams As ManagementBaseObject = srv.GetMethodParameters("ChangeStartMode")
                                inParams("StartMode") = pObj.type.ToString
                                Dim outParams As ManagementBaseObject = srv.InvokeMethod("ChangeStartMode", inParams, Nothing)
                                res = CInt(outParams("ReturnValue"))
                                Exit For
                            End If
                        Next
                        _deg.Invoke(res = 0, pObj.name, CType(res, Native.Api.Enums.SERVICE_RETURN_CODE_WMI).ToString, pObj.newAction)
                    Catch ex As Exception
                        _deg.Invoke(False, pObj.name, ex.Message, pObj.newAction)
                    End Try

                Case Else
                    ' Local
                    Dim hLockSCManager As IntPtr
                    Dim hSCManager As IntPtr = con.SCManagerLocalHandle
                    Dim lServ As IntPtr = Native.Api.NativeFunctions.OpenService(hSCManager, pObj.name, Native.Security.ServiceAccess.ChangeConfig)
                    Dim ret As Boolean = False

                    hLockSCManager = Native.Api.NativeFunctions.LockServiceDatabase(hSCManager)

                    If hSCManager <> IntPtr.Zero Then
                        If lServ <> IntPtr.Zero Then
                            ret = Native.Api.NativeFunctions.ChangeServiceConfig(lServ, Native.Api.NativeEnums.ServiceType.NoChange, _
                                            pObj.type, _
                                            Native.Api.NativeEnums.ServiceErrorControl.NoChange, _
                                            Nothing, Nothing, Nothing, _
                                            Nothing, Nothing, Nothing, Nothing)
                            Native.Api.NativeFunctions.CloseServiceHandle(lServ)
                        End If
                        Native.Api.NativeFunctions.UnlockServiceDatabase(hLockSCManager)
                    End If

                    _deg.Invoke(ret, pObj.name, Native.Api.Win32.GetLastError, pObj.newAction)
            End Select
        End SyncLock
    End Sub

End Class
