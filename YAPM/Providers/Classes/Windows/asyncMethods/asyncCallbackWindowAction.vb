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
Imports Native.Api.Enums
Imports Native.Objects

Public Class asyncCallbackWindowAction

    Private _theDeg As HasMadeAction
    Private con As cWindowConnection

    Public Delegate Sub HasMadeAction(ByVal Success As Boolean, ByVal action As AsyncWindowAction, ByVal handle As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasMadeAction, ByRef procConnection As cWindowConnection)
        _theDeg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public handle As IntPtr
        Public o1 As Integer
        Public o3 As Integer
        Public o2 As Integer
        Public s As String
        Public r As Native.Api.NativeStructs.Rect
        Public action As AsyncWindowAction
        Public newAction As Integer
        Public Sub New(ByVal _action As AsyncWindowAction, ByVal _handle As IntPtr, _
                        ByVal _o1 As Integer, ByVal _o2 As Integer, ByVal _o3 As Integer, _
                        ByVal act As Integer, Optional ByVal obj As Object = Nothing, _
                        Optional ByVal ss As String = Nothing)
            newAction = act
            handle = _handle
            action = _action
            o1 = _o1
            o2 = _o2
            o3 = _o3
            s = ss
            If obj IsNot Nothing Then
                r = DirectCast(obj, Native.Api.NativeStructs.Rect)
            End If
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
                    Dim cDat As cSocketData = Nothing
                    Select Case pObj.action
                        Case AsyncWindowAction.BringToFront
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowBringToFront, pObj.handle, pObj.o1)
                        Case AsyncWindowAction.Close
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowClose, pObj.handle)
                        Case AsyncWindowAction.Flash
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowFlash, pObj.handle)
                        Case AsyncWindowAction.Hide
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowHide, pObj.handle)
                        Case AsyncWindowAction.Maximize
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowMaximize, pObj.handle)
                        Case AsyncWindowAction.Minimize
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowMinimize, pObj.handle)
                        Case AsyncWindowAction.SendMessage
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowShow, pObj.handle, pObj.o1, pObj.o2, pObj.o3)
                        Case AsyncWindowAction.SetAsActiveWindow
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetAsActiveWindow, pObj.handle)
                        Case AsyncWindowAction.SetAsForegroundWindow
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetAsForegroundWindow, pObj.handle)
                        Case AsyncWindowAction.SetEnabled
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowEnable, pObj.handle, CBool(pObj.o1))
                        Case AsyncWindowAction.SetOpacity
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowEnable, pObj.handle, CByte(pObj.o1))
                        Case AsyncWindowAction.Show
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowShow, pObj.handle)
                        Case AsyncWindowAction.StopFlashing
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowStopFlashing, pObj.handle)
                        Case AsyncWindowAction.SetPosition
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetPositions, pObj.handle, pObj.r)
                        Case AsyncWindowAction.SetCaption
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetCaption, pObj.handle, pObj.s)
                    End Select

                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim res As Boolean
                Select Case pObj.action
                    Case AsyncWindowAction.BringToFront
                        res = Window.BringWindowToFrontByHandle(pObj.handle, CBool(pObj.o1))
                    Case AsyncWindowAction.Close
                        res = Window.CloseWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.Flash
                        res = Window.FlashWindowByHandle(pObj.handle, Native.Api.NativeEnums.FlashWInfoFlags.All)
                    Case AsyncWindowAction.Hide
                        res = Window.HideWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.Maximize
                        res = Window.MaximizeWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.Minimize
                        res = Window.MinimizeWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.SendMessage
                        res = (Window.SendMessage(pObj.handle, CType(pObj.o1, Native.Api.NativeEnums.WindowMessage), New IntPtr(pObj.o2), New IntPtr(pObj.o3)).IsNotNull)
                    Case AsyncWindowAction.SetAsActiveWindow
                        res = Window.SetActiveWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.SetAsForegroundWindow
                        res = Window.SetForegroundWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.SetEnabled
                        res = Window.EnableWindowByHandle(pObj.handle, CBool(pObj.o1))
                    Case AsyncWindowAction.SetOpacity
                        res = Window.SetWindowOpacityByHandle(pObj.handle, CByte(pObj.o1))
                    Case AsyncWindowAction.Show
                        res = Window.ShowWindowByHandle(pObj.handle)
                    Case AsyncWindowAction.StopFlashing
                        res = Window.FlashWindowByHandle(pObj.handle, Native.Api.NativeEnums.FlashWInfoFlags.[Stop], 0)
                    Case AsyncWindowAction.SetPosition
                        res = Window.SetWindowPositionAndSizeByHandle(pObj.handle, pObj.r)
                    Case AsyncWindowAction.SetCaption
                        res = Window.SetWindowCaptionByHandle(pObj.handle, pObj.s)
                End Select

                _theDeg.Invoke(res, pObj.action, pObj.handle, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
