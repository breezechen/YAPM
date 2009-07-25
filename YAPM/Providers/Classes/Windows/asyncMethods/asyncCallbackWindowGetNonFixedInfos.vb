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
        Dim enabled As Boolean
        Dim height As Integer
        Dim isTask As Boolean
        Dim left As Integer
        Dim opacity As Byte
        Dim top As Integer
        Dim visible As Boolean
        Dim width As Integer
        Dim theRect As API.RECT
        Public Sub New(ByVal enab As Boolean, ByVal isTas As Boolean, _
                       ByVal opac As Byte, ByRef r As API.RECT)
            enabled = enab
            isTask = isTas
            opacity = opac
            height = r.Bottom - r.Top
            width = r.Right - r.Left
            top = r.Top
            left = r.Left
            theRect = r
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
                Dim enabled As Boolean = API.IsWindowEnabled(_handle)
                Dim opacity As Byte = GetWindowOpacity()
                Dim isTask As Boolean = _isTask(_handle)
                Dim r As API.RECT = GetWindowPosition()

                RaiseEvent GatheredInfos(New TheseInfos(enabled, isTask, opacity, r))
        End Select
    End Sub


    Private Function EnableWindowOpacity() As Integer
        Return API.SetWindowLong(_handle, API.GWL_EXSTYLE, CType(CInt(API.GetWindowLong(_handle, API.GWL_EXSTYLE)) Or API.WS_EX_LAYERED, IntPtr))
    End Function
    Private Function ChangeWindowOpacity(ByVal Alpha As Byte) As Boolean
        Return API.SetLayeredWindowAttributes(_handle, 0, Alpha, API.LWA_ALPHA)
    End Function
    Private Function GetWindowOpacity() As Byte
        Dim alpha As Byte
        API.GetLayeredWindowAttributes(_handle, 0, alpha, API.LWA_ALPHA)
        Return alpha
    End Function
    Private Function DisableWindowOpacity() As Integer
        Return API.SetWindowLong(_handle, API.GWL_EXSTYLE, CType(CInt(API.GetWindowLong(_handle, API.GWL_EXSTYLE)) - API.WS_EX_LAYERED, IntPtr))
    End Function
    ' Get position/size
    Private Function GetWindowPosition() As API.RECT
        Dim tmpRect As API.RECT
        API.GetWindowRect(_handle, tmpRect)
        Return tmpRect
    End Function

    Private Shared Function _isTask(ByVal hwnd As IntPtr) As Boolean
        ' Window must be visible
        If API.IsWindowVisible(hwnd) AndAlso (CInt(API.GetWindowLong(hwnd, API.GWL_HWNDPARENT)) = 0) AndAlso Not _
            (API.GetWindowTextLength(hwnd) = 0) Then
            ' Must not be taskmgr
            If GetWindowClass(hwnd) <> "Progman" Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Shared Function GetWindowClass(ByVal hWnd As IntPtr) As String
        Dim _class As New StringBuilder(Space(255))
        API.GetClassName(hWnd, _class, 255)
        Return _class.ToString
    End Function
End Class
