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

Public Class asyncCallbackWindowAction

    Private _theDeg As HasMadeAction
    Private con As cWindowConnection

    Public Enum ASYNC_WINDOW_ACTION
        Close
        Flash
        StopFlashing
        BringToFront
        SetAsForegroundWindow
        SetAsActiveWindow
        Minimize
        Maximize
        Show
        Hide
        SendMessage
        SetOpacity
        SetEnabled
        SetPosition
        SetCaption
    End Enum

    Public Delegate Sub HasMadeAction(ByVal Success As Boolean, ByVal action As ASYNC_WINDOW_ACTION, ByVal handle As Integer, ByVal msg As String, ByVal actionNumber As Integer)

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
        Public action As ASYNC_WINDOW_ACTION
        Public newAction As Integer
        Public Sub New(ByVal _action As ASYNC_WINDOW_ACTION, ByVal _handle As IntPtr, _
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
                        Case ASYNC_WINDOW_ACTION.BringToFront
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowBringToFront, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.Close
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowClose, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.Flash
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowFlash, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.Hide
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowHide, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.Maximize
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowMaximize, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.Minimize
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowMinimize, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.SendMessage
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowShow, CInt(pObj.handle), pObj.o1, pObj.o2, pObj.o3)
                        Case ASYNC_WINDOW_ACTION.SetAsActiveWindow
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetAsActiveWindow, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.SetAsForegroundWindow
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetAsForegroundWindow, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.SetEnabled
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowEnable, CInt(pObj.handle), CBool(pObj.o1))
                        Case ASYNC_WINDOW_ACTION.SetOpacity
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowEnable, CInt(pObj.handle), CByte(pObj.o1))
                        Case ASYNC_WINDOW_ACTION.Show
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowShow, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.StopFlashing
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowStopFlashing, CInt(pObj.handle))
                        Case ASYNC_WINDOW_ACTION.SetPosition
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetPositions, CInt(pObj.handle), pObj.r)
                        Case ASYNC_WINDOW_ACTION.SetCaption
                            cDat = New cSocketData(cSocketData.DataType.Order, _
                             cSocketData.OrderType.WindowSetCaption, CInt(pObj.handle), pObj.s)
                    End Select

                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim res As Integer = 0
                Select Case pObj.action
                    Case ASYNC_WINDOW_ACTION.BringToFront
                        res = BringToFront(pObj.handle, CBool(pObj.o1))
                    Case ASYNC_WINDOW_ACTION.Close
                        res = CInt(Close(pObj.handle))
                    Case ASYNC_WINDOW_ACTION.Flash
                        res = Flash(pObj.handle)
                    Case ASYNC_WINDOW_ACTION.Hide
                        res = CInt(Hide(pObj.handle))
                    Case ASYNC_WINDOW_ACTION.Maximize
                        res = CInt(Maximize(pObj.handle))
                    Case ASYNC_WINDOW_ACTION.Minimize
                        res = CInt(Minimize(pObj.handle))
                    Case ASYNC_WINDOW_ACTION.SendMessage
                        res = SendMessage(pObj.handle, CType(pObj.o1, Native.Api.NativeEnums.WindowMessage), New IntPtr(pObj.o2), New IntPtr(pObj.o3)).ToInt32
                    Case ASYNC_WINDOW_ACTION.SetAsActiveWindow
                        res = SetAsActiveWindow(pObj.handle).ToInt32
                    Case ASYNC_WINDOW_ACTION.SetAsForegroundWindow
                        res = CInt(SetAsForegroundWindow(pObj.handle))
                    Case ASYNC_WINDOW_ACTION.SetEnabled
                        res = CInt(SetEnabled(pObj.handle, CBool(pObj.o1)))
                    Case ASYNC_WINDOW_ACTION.SetOpacity
                        If pObj.o1 = 255 Then
                            Call DisableWindowOpacity(pObj.handle)
                        Else
                            Call EnableWindowOpacity(pObj.handle)
                            Call SetOpacity(pObj.handle, CByte(pObj.o1))
                        End If
                    Case ASYNC_WINDOW_ACTION.Show
                        res = CInt(Show(pObj.handle))
                    Case ASYNC_WINDOW_ACTION.StopFlashing
                        res = StopFlashing(pObj.handle)
                    Case ASYNC_WINDOW_ACTION.SetPosition
                        res = CInt(SetPosition(pObj.handle, pObj.r))
                    Case ASYNC_WINDOW_ACTION.SetCaption
                        res = Native.Api.NativeFunctions.SetWindowText(pObj.handle, New StringBuilder(pObj.s))
                End Select

                _theDeg.Invoke(Err.LastDllError = 0, pObj.action, pObj.handle.ToInt32, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub


#Region "Local methods"

    Private Function Close(ByVal _handle As IntPtr) As IntPtr
        Return Native.Api.NativeFunctions.SendMessage(_handle, Native.Api.NativeConstants.WM_CLOSE, IntPtr.Zero, IntPtr.Zero)
    End Function
    Private Function Flash(ByVal _handle As IntPtr) As Integer
        Dim FlashInfo As Native.Api.NativeStructs.FlashWInfo

        With FlashInfo
            .cbSize = CUInt(Marshal.SizeOf(FlashInfo))
            .dwFlags = &H3            ' Flash caption & taskbar
            .dwTimeout = 0
            .hwnd = _handle
            .uCount = Integer.MaxValue
        End With

        Return CInt(Native.Api.NativeFunctions.FlashWindowEx(FlashInfo))
    End Function
    Private Function StopFlashing(ByVal _handle As IntPtr) As Integer
        Dim FlashInfo As Native.Api.NativeStructs.FlashWInfo

        With FlashInfo
            .cbSize = CUInt(Marshal.SizeOf(FlashInfo))
            .dwFlags = 0            ' Stop flashing
            .dwTimeout = 0
            .hwnd = _handle
            .uCount = 0
        End With

        Return CInt(Native.Api.NativeFunctions.FlashWindowEx(FlashInfo))
    End Function
    Private Function BringToFront(ByVal _handle As IntPtr, ByVal val As Boolean) As Integer
        If val Then
            Return CInt(Native.Api.NativeFunctions.SetWindowPos(_handle, New IntPtr(Native.Api.NativeConstants.HWND_TOPMOST), 0, 0, 0, 0, _
                Native.Api.NativeConstants.SWP_NOACTIVATE Or Native.Api.NativeConstants.SWP_SHOWWINDOW Or Native.Api.NativeConstants.SWP_NOMOVE Or Native.Api.NativeConstants.SWP_NOSIZE))
        Else
            Return CInt(Native.Api.NativeFunctions.SetWindowPos(_handle, New IntPtr(Native.Api.NativeConstants.HWND_NOTOPMOST), 0, 0, 0, 0, _
                Native.Api.NativeConstants.SWP_NOACTIVATE Or Native.Api.NativeConstants.SWP_SHOWWINDOW Or Native.Api.NativeConstants.SWP_NOMOVE Or Native.Api.NativeConstants.SWP_NOSIZE))
        End If
    End Function
    Private Function SetAsForegroundWindow(ByVal _handle As IntPtr) As Boolean
        Return Native.Api.NativeFunctions.SetForegroundWindow(_handle)
    End Function
    Private Function SetAsActiveWindow(ByVal _handle As IntPtr) As IntPtr
        Return Native.Api.NativeFunctions.SetActiveWindow(_handle)
    End Function
    Private Function Minimize(ByVal _handle As IntPtr) As Boolean
        Return Native.Api.NativeFunctions.ShowWindow(_handle, Native.Api.NativeEnums.ShowWindowType.Minimize)
    End Function
    Private Function Maximize(ByVal _handle As IntPtr) As Boolean
        Return Native.Api.NativeFunctions.ShowWindow(_handle, Native.Api.NativeEnums.ShowWindowType.Maximize)
    End Function
    Private Function Show(ByVal _handle As IntPtr) As Boolean
        Return Native.Api.NativeFunctions.ShowWindow(_handle, Native.Api.NativeEnums.ShowWindowType.Show)
    End Function
    Private Function Hide(ByVal _handle As IntPtr) As Boolean
        Return Native.Api.NativeFunctions.ShowWindow(_handle, Native.Api.NativeEnums.ShowWindowType.Hide)
    End Function
    Private Function SendMessage(ByVal _handle As IntPtr, ByVal val As Native.Api.NativeEnums.WindowMessage, ByVal o1 As IntPtr, ByVal o2 As IntPtr) As IntPtr
        Return Native.Api.NativeFunctions.SendMessage(_handle, val, o1, o2)
    End Function
    Private Function SetOpacity(ByVal _handle As IntPtr, ByVal val As Byte) As Boolean
        Return Native.Api.NativeFunctions.SetLayeredWindowAttributes(_handle, 0, val, Native.Api.NativeConstants.LWA_ALPHA)
    End Function
    Private Function SetEnabled(ByVal _handle As IntPtr, ByVal val As Boolean) As Boolean
        Return Native.Api.NativeFunctions.EnableWindow(_handle, val)
    End Function
    Private Function DisableWindowOpacity(ByVal _handle As IntPtr) As IntPtr
        Return Native.Api.NativeFunctions.SetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle, Native.Api.NativeFunctions.GetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle).Decrement(Native.Api.NativeConstants.WS_EX_LAYERED))
    End Function
    Private Function EnableWindowOpacity(ByVal _handle As IntPtr) As IntPtr
        Return Native.Api.NativeFunctions.SetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle, (Native.Api.NativeFunctions.GetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle).Increment(Native.Api.NativeConstants.WS_EX_LAYERED)))
    End Function
    Private Function SetPosition(ByVal _handle As IntPtr, ByRef r As Native.Api.NativeStructs.Rect) As Boolean
        Dim WndPl As Native.Api.NativeStructs.WindowPlacement
        WndPl.Length = System.Runtime.InteropServices.Marshal.SizeOf(WndPl)
        Native.Api.NativeFunctions.GetWindowPlacement(_handle, WndPl)
        WndPl.NormalPosition = r
        Return Native.Api.NativeFunctions.SetWindowPlacement(_handle, WndPl)
    End Function

#End Region

End Class
