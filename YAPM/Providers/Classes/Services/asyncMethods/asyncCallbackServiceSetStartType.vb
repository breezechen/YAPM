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
    Private _deg As HasChangedStartType

    Public Delegate Sub HasChangedStartType(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasChangedStartType)
        _deg = deg
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
            If Program.Connection.IsConnected = False Then
                Exit Sub
            End If

            Select Case Program.Connection.Type
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ServiceChangeServiceStartType, pObj.name, pObj.type)
                        Program.Connection.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    Dim res As Boolean
                    Dim msg As String = ""
                    res = Wmi.Objects.Service.SetServiceStartTypeByName(pObj.name, pObj.type, _
                                                                        ServiceProvider.wmiSearcher, msg)

                    Try
                        _deg.Invoke(res, pObj.name, msg, pObj.newAction)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try

                Case Else
                    ' Local
                    Dim res As Boolean = Native.Objects.Service.SetServiceStartTypeByName(pObj.name, _
                                                pObj.type, _
                                                ServiceProvider.ServiceControlManaherHandle)
                    _deg.Invoke(res, pObj.name, Native.Api.Win32.GetLastError, pObj.newAction)
            End Select
        End SyncLock
    End Sub

End Class
