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
Imports Native.Api

Namespace Native.Objects

    Public Class Window


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Get all windows
        Public Shared Function EnumerateAllWindows() As Dictionary(Of String, cWindow)
            ' Local
            Dim currWnd As IntPtr
            Dim cpt As Integer

            Dim _dico As New Dictionary(Of String, cWindow)

            currWnd = GetWindow(GetDesktopWindow(), NativeEnums.GetWindowCmd.Child)
            cpt = 0
            Do While currWnd.IsNotNull

                ' Get procId from hwnd
                Dim pid As Integer = GetProcessIdFromWindowHandle(currWnd)
                Dim tid As Integer = GetThreadIdFromWindowHandle(currWnd)
                Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString
                If _dico.ContainsKey(key) = False Then
                    _dico.Add(key, New cWindow(New windowInfos(pid, tid, currWnd, GetWindowCaption(currWnd))))
                End If

                currWnd = NativeFunctions.GetWindow(currWnd, NativeEnums.GetWindowCmd.[Next])
            Loop

            Return _dico
        End Function

        ' Enumerate windows (local)
        Public Shared Sub EnumerateWindowsByProcessId(ByVal processId As Integer, _
                                      ByVal allProcesses As Boolean, _
                                      ByVal showUnnamed As Boolean, _
                                      ByRef _dico As Dictionary(Of String, windowInfos), _
                                      ByVal refreshAllInfos As Boolean)
            Dim currWnd As IntPtr
            Dim cpt As Integer

            currWnd = NativeFunctions.GetWindow(NativeFunctions.GetDesktopWindow(), _
                                                NativeEnums.GetWindowCmd.Child)
            cpt = 0
            Do While currWnd.IsNotNull

                ' Get procId from hwnd
                Dim pid As Integer = GetProcessIdFromWindowHandle(currWnd)
                If allProcesses OrElse pid = processId Then
                    ' Then this window belongs to one of our processes
                    Dim sCap As String = GetWindowCaption(currWnd)
                    If showUnnamed OrElse sCap.Length > 0 Then
                        Dim tid As Integer = GetThreadIdFromWindowHandle(currWnd)
                        Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString
                        If _dico.ContainsKey(key) = False Then
                            If refreshAllInfos Then
                                ' Then we need to retrieve all informations
                                ' (this is server mode)
                                Dim wInfo As windowInfos
                                wInfo = New windowInfos(pid, tid, currWnd, sCap)
                                wInfo.SetNonFixedInfos(asyncCallbackWindowGetNonFixedInfos.ProcessAndReturnLocal(currWnd))
                                _dico.Add(key, wInfo)
                            Else
                                _dico.Add(key, New windowInfos(pid, tid, currWnd, sCap))
                            End If
                        End If
                    End If
                End If

                currWnd = NativeFunctions.GetWindow(currWnd, NativeEnums.GetWindowCmd.[Next])
            Loop
        End Sub

        ' Return process id from a handle
        Public Shared Function GetProcessIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
            Dim id As Integer = 0
            NativeFunctions.GetWindowThreadProcessId(hwnd, id)
            Return id
        End Function

        ' Return caption
        Public Shared Function GetWindowCaption(ByVal hWnd As IntPtr) As String
            Dim length As Integer
            length = NativeFunctions.GetWindowTextLength(hWnd)
            If length > 0 Then
                Dim _cap As New System.Text.StringBuilder(Space(length + 1))
                length = NativeFunctions.GetWindowText(hWnd, _cap, length + 1)
                Return _cap.ToString.Substring(0, Len(_cap.ToString))
            Else
                Return ""
            End If
        End Function

        ' Return thread id from a handle
        Public Shared Function GetThreadIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
            Return NativeFunctions.GetWindowThreadProcessId(hwnd, 0)
        End Function

        ' Is enabled ?
        Public Shared Function IsWindowEnabled(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.IsWindowEnabled(handle)
        End Function

        ' Is visible ?
        Public Shared Function IsWindowVisible(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.IsWindowVisible(handle)
        End Function

        ' Is a task ?
        Public Shared Function IsWindowATask(ByVal hwnd As IntPtr) As Boolean
            ' Window must be visible
            If NativeFunctions.IsWindowVisible(hwnd) AndAlso _
                    NativeFunctions.GetWindowLongPtr(hwnd, _
                                        NativeEnums.GetWindowLongOffset.HwndParent).IsNull AndAlso Not _
                (NativeFunctions.GetWindowTextLength(hwnd) = 0) Then
                ' Must not be taskmgr
                If GetWindowClass(hwnd) <> "Progman" Then
                    Return True
                End If
            End If

            Return False
        End Function

        ' Return window opacity
        Public Shared Function GetWindowOpacityByHandle(ByVal handle As IntPtr) As Byte
            Dim alpha As Byte
            NativeFunctions.GetLayeredWindowAttributes(handle, 0, alpha, NativeConstants.LWA_ALPHA)
            Return alpha
        End Function

        ' Get position/size
        Public Shared Function GetWindowPositionAndSizeByHandle(ByVal handle As IntPtr) As NativeStructs.Rect
            Dim tmpRect As NativeStructs.Rect
            NativeFunctions.GetWindowRect(handle, tmpRect)
            Return tmpRect
        End Function

        ' Return window class
        Public Shared Function GetWindowClass(ByVal hWnd As IntPtr) As String
            Dim _class As New System.Text.StringBuilder(Space(255))
            NativeFunctions.GetClassName(hWnd, _class, 255)
            Return _class.ToString
        End Function

        ' Wrapper to GetWindow Win32 function
        Public Shared Function GetWindow(ByVal hWnd As IntPtr, ByVal cmd As NativeEnums.GetWindowCmd) As IntPtr
            Return NativeFunctions.GetWindow(hWnd, cmd)
        End Function

        ' Wrapper to GetDesktopWindow
        Public Shared Function GetDesktopWindow() As IntPtr
            Return NativeFunctions.GetDesktopWindow()
        End Function

        ' Wrapper to SetForegroundWindow
        Public Shared Function SetForegroundWindowByHandle(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.SetForegroundWindow(handle)
        End Function

        ' Close a window
        Public Shared Function CloseWindowByHandle(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.SendMessage(handle, _
                                            NativeEnums.WindowMessage.Close, _
                                            IntPtr.Zero, IntPtr.Zero).IsNull
        End Function

        ' Wrapper to GetForegroundWindow Win32 function
        Public Shared Function GetForegroundWindow() As IntPtr
            Return NativeFunctions.GetForegroundWindow
        End Function

        ' Get small icon handle of window
        Public Shared Function GetWindowSmallIconHandleByHandle(ByVal handle As IntPtr) As IntPtr
            Dim res As IntPtr
            Dim out As IntPtr

            ' Time we wait if we can not return SendMessage immediately
            Const WaitingTime As Integer = 50

            res = NativeFunctions.SendMessageTimeout(handle, NativeEnums.WindowMessage.GetIcon, _
                                         New IntPtr(NativeEnums.IconSize.Small), IntPtr.Zero, _
                                         NativeEnums.SendMessageTimeoutFlags.AbortIfHung, WaitingTime, out)

            If out.IsNull Then
                out = NativeFunctions.GetClassLongPtr(handle, NativeConstants.GCL_HICONSM)
            End If

            If out.IsNull Then
                res = NativeFunctions.SendMessageTimeout(NativeFunctions.GetWindowLongPtr(handle, _
                                      NativeEnums.GetWindowLongOffset.HwndParent), _
                                      NativeEnums.WindowMessage.GetIcon, _
                                      New IntPtr(NativeEnums.IconSize.Small), IntPtr.Zero, _
                                      NativeEnums.SendMessageTimeoutFlags.AbortIfHung, WaitingTime, out)
            End If

            Return out
        End Function

        ' Flash a window
        Public Shared Function FlashWindowByHandle(ByVal handle As IntPtr, _
                            ByVal flag As NativeEnums.FlashWInfoFlags, _
                            Optional ByVal count As Integer = Integer.MaxValue) As Boolean
            Dim FlashInfo As NativeStructs.FlashWInfo

            With FlashInfo
                .cbSize = CUInt(Marshal.SizeOf(FlashInfo))
                .dwFlags = flag
                .dwTimeout = 0
                .hwnd = handle
                .uCount = CUInt(count)
            End With

            Return NativeFunctions.FlashWindowEx(FlashInfo)
        End Function

        ' Hide window
        Public Shared Function HideWindowByHandle(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.ShowWindow(handle, _
                            NativeEnums.ShowWindowType.Hide)
        End Function

        ' Show window
        Public Shared Function ShowWindowByHandle(ByVal handle As IntPtr, _
                    Optional ByVal flag As NativeEnums.ShowWindowType = _
                    NativeEnums.ShowWindowType.Show) As Boolean
            Return Not (NativeFunctions.ShowWindow(handle, flag))
        End Function

        ' Maximize window
        Public Shared Function MaximizeWindowByHandle(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.ShowWindow(handle, _
                            NativeEnums.ShowWindowType.ShowMaximized)
        End Function

        ' Minimize window
        Public Shared Function MinimizeWindowByHandle(ByVal handle As IntPtr) As Boolean
            Return NativeFunctions.ShowWindow(handle, _
                            NativeEnums.ShowWindowType.ShowMinimized)
        End Function

        ' Bring to front (or not)
        Public Shared Function BringWindowToFrontByHandle(ByVal handle As IntPtr, ByVal val As Boolean) As Boolean
            Dim flag As IntPtr
            If val Then
                flag = New IntPtr(NativeConstants.HWND_TOPMOST)
            Else
                flag = New IntPtr(NativeConstants.HWND_NOTOPMOST)
            End If

            Return NativeFunctions.SetWindowPos(handle, flag, 0, 0, 0, 0, _
                NativeConstants.SWP_NOACTIVATE Or NativeConstants.SWP_SHOWWINDOW Or _
                NativeConstants.SWP_NOMOVE Or NativeConstants.SWP_NOSIZE)

        End Function

        ' Wrapper for SetActiveWindow
        Public Shared Function SetActiveWindowByHandle(ByVal handle As IntPtr) As Boolean
            Return (NativeFunctions.SetActiveWindow(handle).IsNotNull)
        End Function

        ' Enable a window
        Public Shared Function EnableWindowByHandle(ByVal handle As IntPtr, _
                                                ByVal val As Boolean) As Boolean
            Return NativeFunctions.EnableWindow(handle, val)
        End Function

        ' Set opacity
        Public Shared Function SetWindowOpacityByHandle(ByVal handle As IntPtr, _
                                                        ByVal alpha As Byte) As Boolean
            If alpha = 255 Then
                Return (DisableWindowOpacity(handle).IsNotNull)
            Else
                EnableWindowOpacity(handle)
                Return SetOpacity(handle, alpha)
            End If
        End Function

        ' Wrapper for SendMessage
        Public Shared Function SendMessage(ByVal handle As IntPtr, _
                                ByVal val As NativeEnums.WindowMessage, _
                                ByVal o1 As IntPtr, _
                                ByVal o2 As IntPtr) As IntPtr
            Return NativeFunctions.SendMessage(handle, val, o1, o2)
        End Function

        ' Set position/size
        Public Shared Function SetWindowPositionAndSizeByHandle(ByVal handle As IntPtr, _
                                        ByVal r As NativeStructs.Rect) As Boolean
            Dim WndPl As NativeStructs.WindowPlacement
            WndPl.Length = Marshal.SizeOf(WndPl)
            NativeFunctions.GetWindowPlacement(handle, WndPl)
            WndPl.NormalPosition = r
            Return NativeFunctions.SetWindowPlacement(handle, WndPl)
        End Function

        ' Set caption
        Public Shared Function SetWindowCaptionByHandle(ByVal handle As IntPtr, _
                                                        ByVal caption As String) As Boolean
            Return (Native.Api.NativeFunctions.SetWindowText(handle, _
                                            New System.Text.StringBuilder(caption)) <> 0)
        End Function




        ' ========================================
        ' Private functions
        ' ========================================

        ' Set opacity
        Private Shared Function SetOpacity(ByVal handle As IntPtr, ByVal val As Byte) As Boolean
            Return NativeFunctions.SetLayeredWindowAttributes(handle, 0, val, NativeConstants.LWA_ALPHA)
        End Function

        ' Disable opacity
        Private Shared Function DisableWindowOpacity(ByVal handle As IntPtr) As IntPtr
            Return NativeFunctions.SetWindowLongPtr(handle, _
                    NativeEnums.GetWindowLongOffset.ExStyle, _
                    NativeFunctions.GetWindowLongPtr(handle, NativeEnums.GetWindowLongOffset.ExStyle).Decrement(NativeConstants.WS_EX_LAYERED))
        End Function

        ' Enable opacity
        Private Shared Function EnableWindowOpacity(ByVal handle As IntPtr) As IntPtr
            Return NativeFunctions.SetWindowLongPtr(handle, _
                    NativeEnums.GetWindowLongOffset.ExStyle, _
                    (NativeFunctions.GetWindowLongPtr(handle, NativeEnums.GetWindowLongOffset.ExStyle).Increment(NativeConstants.WS_EX_LAYERED)))
        End Function

    End Class

End Namespace
