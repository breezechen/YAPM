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

Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackMemRegionChangeProtection

    Private con As cMemRegionConnection
    Private _deg As HasChangedProtection

    Public Delegate Sub HasChangedProtection(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasChangedProtection, ByRef memConnection As cMemRegionConnection)
        _deg = deg
        con = memConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public address As IntPtr
        Public size As Integer
        Public protection As Native.Api.NativeEnums.MemoryProtectionType
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal ad As IntPtr, _
                       ByVal siz As Integer, _
                       ByVal protec As Native.Api.NativeEnums.MemoryProtectionType, _
                       ByVal act As Integer)
            address = ad
            newAction = act
            size = siz
            protection = protec
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.MemoryChangeProtectionType, pObj.pid, pObj.address, pObj.size, pObj.protection)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = ChangeProtectionType(pObj)
                _deg.Invoke(ret, pObj.pid, pObj.address, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub


    ' Change protection type
    Private Shared Function ChangeProtectionType(ByVal obj As poolObj) As Boolean

        Dim ret As Boolean
        Dim hProcess As IntPtr
        Dim old As Native.Api.NativeEnums.MemoryProtectionType

        hProcess = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.VmOperation, False, obj.pid)
        If hProcess .IsNotNull Then
            ret = Native.Api.NativeFunctions.VirtualProtectEx(hProcess, obj.address, obj.size, obj.protection, old)
            Call Native.Api.NativeFunctions.CloseHandle(hProcess)
        End If

        Return ret

    End Function

End Class
