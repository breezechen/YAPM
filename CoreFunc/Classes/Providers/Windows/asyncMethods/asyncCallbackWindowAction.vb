Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackWindowAction

    Private _handle As IntPtr
    Private _o1 As Integer
    Private _o3 As Integer
    Private _o2 As Integer
    Private _theDeg As HasMadeAction
    Private _action As ASYNC_WINDOW_ACTION
    Private _connection As cWindowConnection

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
    End Enum

    Public Delegate Sub HasMadeAction(ByVal Success As Boolean, ByVal action As ASYNC_WINDOW_ACTION, ByVal handle As Integer, ByVal msg As String)

    Public Sub New(ByVal deg As HasMadeAction, ByVal action As ASYNC_WINDOW_ACTION, ByVal handle As IntPtr, _
                   ByVal o1 As Integer, ByVal o2 As Integer, ByVal o3 As Integer, ByRef procConnection As  _
                   cWindowConnection)
        _handle = handle
        _action = action
        _o1 = o1
        _o2 = o2
        _o3 = o3
        _theDeg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim res As Integer = 0
                Select Case _action
                    Case ASYNC_WINDOW_ACTION.BringToFront
                        res = BringToFront(CBool(_o1))
                    Case ASYNC_WINDOW_ACTION.Close
                        res = Close()
                    Case ASYNC_WINDOW_ACTION.Flash
                        res = Flash()
                    Case ASYNC_WINDOW_ACTION.Hide
                        res = Hide()
                    Case ASYNC_WINDOW_ACTION.Maximize
                        res = Maximize()
                    Case ASYNC_WINDOW_ACTION.Minimize
                        res = Minimize()
                    Case ASYNC_WINDOW_ACTION.SendMessage
                        res = SendMessage(_o1, _o2, _o3)
                    Case ASYNC_WINDOW_ACTION.SetAsActiveWindow
                        res = SetAsActiveWindow()
                    Case ASYNC_WINDOW_ACTION.SetAsForegroundWindow
                        res = SetAsForegroundWindow()
                    Case ASYNC_WINDOW_ACTION.SetEnabled
                        res = SetEnabled(CBool(_o1))
                    Case ASYNC_WINDOW_ACTION.SetOpacity
                        If _o1 = 255 Then
                            Call DisableWindowOpacity()
                        Else
                            Call EnableWindowOpacity()
                            Call SetOpacity(CByte(_o1))
                        End If
                    Case ASYNC_WINDOW_ACTION.Show
                        res = Show()
                    Case ASYNC_WINDOW_ACTION.StopFlashing
                        res = StopFlashing()
                End Select

                _theDeg.BeginInvoke(res <> 0, _action, _handle.ToInt32, API.GetError, Nothing, Nothing)
        End Select
    End Sub


#Region "Local methods"

    Private Function Close() As Integer
        Return API.SendMessage(_handle, API.WM_CLOSE, 0, 0).ToInt32
    End Function
    Private Function Flash() As Integer
        Dim FlashInfo As API.FLASHWINFO

        With FlashInfo
            .cbSize = Marshal.SizeOf(FlashInfo)
            .dwFlags = &H3            ' Flash caption & taskbar
            .dwTimeout = 0
            .hWnd = CInt(_handle)
            .uCount = Integer.MaxValue
        End With

        Return CInt(API.FlashWindowEx(FlashInfo))
    End Function
    Private Function StopFlashing() As Integer
        Dim FlashInfo As API.FLASHWINFO

        With FlashInfo
            .cbSize = Marshal.SizeOf(FlashInfo)
            .dwFlags = 0            ' Stop flashing
            .dwTimeout = 0
            .hWnd = CInt(_handle)
            .uCount = 0
        End With

        Return CInt(API.FlashWindowEx(FlashInfo))
    End Function
    Private Function BringToFront(ByVal val As Boolean) As Integer
        If val Then
            Return CInt(API.SetWindowPos(_handle, API.HWND_TOPMOST, 0, 0, 0, 0, _
                API.SWP_NOACTIVATE Or API.SWP_SHOWWINDOW Or API.SWP_NOMOVE Or API.SWP_NOSIZE))
        Else
            Return CInt(API.SetWindowPos(_handle, API.HWND_NOTOPMOST, 0, 0, 0, 0, _
                API.SWP_NOACTIVATE Or API.SWP_SHOWWINDOW Or API.SWP_NOMOVE Or API.SWP_NOSIZE))
        End If
    End Function
    Private Function SetAsForegroundWindow() As Integer
        Return API.SetForegroundWindowAPI(_handle)
    End Function
    Private Function SetAsActiveWindow() As Integer
        Return API.SetActiveWindowAPI(_handle)
    End Function
    Private Function Minimize() As Integer
        Return CInt(API.ShowWindow(_handle, API.SW_MINIMIZE))
    End Function
    Private Function Maximize() As Integer
        Return CInt(API.ShowWindow(_handle, API.SW_MAXIMIZE))
    End Function
    Private Function Show() As Integer
        Return CInt(API.ShowWindow(_handle, API.SW_SHOW))
    End Function
    Private Function Hide() As Integer
        Return CInt(API.ShowWindow(_handle, API.SW_HIDE))
    End Function
    Private Function SendMessage(ByVal val As Integer, ByVal o1 As Integer, ByVal o2 As Integer) As Integer
        Return API.SendMessage(_handle, val, o1, o2).ToInt32
    End Function
    Private Function SetOpacity(ByVal val As Byte) As Integer
        Return CInt(API.SetLayeredWindowAttributes(_handle, 0, val, API.LWA_ALPHA))
    End Function
    Private Function SetEnabled(ByVal val As Boolean) As Integer
        Return API.EnableWindow(_handle, CInt(val))
    End Function
    Private Function DisableWindowOpacity() As Integer
        Return API.SetWindowLong(_handle, API.GWL_EXSTYLE, CType(CInt(API.GetWindowLong(_handle, API.GWL_EXSTYLE)) - API.WS_EX_LAYERED, IntPtr))
    End Function
    Private Function EnableWindowOpacity() As Integer
        Return API.SetWindowLong(_handle, API.GWL_EXSTYLE, CType(CInt(API.GetWindowLong(_handle, API.GWL_EXSTYLE)) Or API.WS_EX_LAYERED, IntPtr))
    End Function

#End Region

End Class
