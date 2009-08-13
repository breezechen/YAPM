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

Public Class asyncCallbackWindowGetNonFixedInfos

    Private _handle As IntPtr
    Private _connection As cWindowConnection

    Public Structure TheseInfos
        Public enabled As Boolean
        Public height As Integer
        Public isTask As Boolean
        Public left As Integer
        Public opacity As Byte
        Public top As Integer
        Public visible As Boolean
        Public width As Integer
        Public theRect As Native.Api.NativeStructs.Rect
        Public caption As String
        Public Sub New(ByVal enab As Boolean, ByVal isTas As Boolean, _
                       ByVal opac As Byte, ByRef r As Native.Api.NativeStructs.Rect, ByVal scap As String, ByVal isV As Boolean)
            enabled = enab
            isTask = isTas
            opacity = opac
            height = r.Bottom - r.Top
            width = r.Right - r.Left
            top = r.Top
            left = r.Left
            theRect = r
            caption = scap
            visible = isV
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal handle As IntPtr, ByRef procConnection As cWindowConnection)
        _handle = handle
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim enabled As Boolean = Native.Api.NativeFunctions.IsWindowEnabled(_handle)
                Dim visible As Boolean = Native.Api.NativeFunctions.IsWindowVisible(_handle)
                Dim opacity As Byte = GetWindowOpacity()
                Dim isTask As Boolean = _isTask(_handle)
                Dim r As Native.Api.NativeStructs.Rect = GetWindowPosition()
                Dim s As String = asyncCallbackWindowEnumerate.GetCaption(_handle)

                RaiseEvent GatheredInfos(New TheseInfos(enabled, isTask, opacity, r, s, visible))
        End Select
    End Sub


    Private Sub EnableWindowOpacity()
        Native.Api.NativeFunctions.SetWindowLongPtr(_handle, _
                    Native.Api.NativeEnums.GetWindowLongOffset.ExStyle, _
                    New IntPtr(Native.Api.NativeFunctions.GetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle).ToInt32 Or Native.Api.NativeConstants.WS_EX_LAYERED))
    End Sub
    Private Function ChangeWindowOpacity(ByVal Alpha As Byte) As Boolean
        Return Native.Api.NativeFunctions.SetLayeredWindowAttributes(_handle, 0, Alpha, Native.Api.NativeConstants.LWA_ALPHA)
    End Function
    Private Function GetWindowOpacity() As Byte
        Dim alpha As Byte
        Native.Api.NativeFunctions.GetLayeredWindowAttributes(_handle, 0, alpha, Native.Api.NativeConstants.LWA_ALPHA)
        Return alpha
    End Function
    Private Sub DisableWindowOpacity()
        Native.Api.NativeFunctions.SetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle, CType(CInt(Native.Api.NativeFunctions.GetWindowLongPtr(_handle, Native.Api.NativeEnums.GetWindowLongOffset.ExStyle)) - Native.Api.NativeConstants.WS_EX_LAYERED, IntPtr))
    End Sub
    ' Get position/size
    Private Function GetWindowPosition() As Native.Api.NativeStructs.Rect
        Dim tmpRect As Native.Api.NativeStructs.Rect
        Native.Api.NativeFunctions.GetWindowRect(_handle, tmpRect)
        Return tmpRect
    End Function

    Private Shared Function _isTask(ByVal hwnd As IntPtr) As Boolean
        ' Window must be visible
        If Native.Api.NativeFunctions.IsWindowVisible(hwnd) AndAlso _
                Native.Api.NativeFunctions.GetWindowLongPtr(hwnd, _
                                    Native.Api.NativeEnums.GetWindowLongOffset.HwndParent) = IntPtr.Zero AndAlso Not _
            (Native.Api.NativeFunctions.GetWindowTextLength(hwnd) = 0) Then
            ' Must not be taskmgr
            If GetWindowClass(hwnd) <> "Progman" Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Shared Function GetWindowClass(ByVal hWnd As IntPtr) As String
        Dim _class As New StringBuilder(Space(255))
        Native.Api.NativeFunctions.GetClassName(hWnd, _class, 255)
        Return _class.ToString
    End Function
End Class
