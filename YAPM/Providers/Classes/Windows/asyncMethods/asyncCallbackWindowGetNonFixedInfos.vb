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
Imports YAPM.Native.Objects

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
                Dim enabled As Boolean = Window.IsWindowEnabled(_handle)
                Dim visible As Boolean = Window.IsWindowVisible(_handle)
                Dim opacity As Byte = Window.GetWindowOpacityByHandle(_handle)
                Dim isTask As Boolean = Window.IsWindowATask(_handle)
                Dim r As Native.Api.NativeStructs.Rect = Window.GetWindowPositionAndSizeByHandle(_handle)
                Dim s As String = Window.GetWindowCaption(_handle)

                RaiseEvent GatheredInfos(New TheseInfos(enabled, isTask, opacity, r, s, visible))
        End Select
    End Sub

End Class
