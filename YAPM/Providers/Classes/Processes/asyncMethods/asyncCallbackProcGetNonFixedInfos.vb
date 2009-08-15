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

Public Class asyncCallbackProcGetNonFixedInfos

    Private _pid As Integer
    Private _handle As IntPtr
    Private _connection As cProcessConnection

    Public Structure TheseInfos
        Dim gdiO As Integer
        Dim userO As Integer
        Dim affinity As IntPtr
        Public Sub New(ByVal _gdi As Integer, ByVal _user As Integer, _
                       ByVal _affinity As IntPtr)
            gdiO = _gdi
            userO = _user
            affinity = _affinity
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cProcessConnection, _
                   ByVal handle As IntPtr)
        _pid = pid
        _handle = handle
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _gdi As Integer = Native.Objects.Process.GetProcessGuiResourceByHandle(_handle, _
                                                    Native.Api.NativeEnums.GuiResourceType.GdiObjects)
                Dim _user As Integer = Native.Objects.Process.GetProcessGuiResourceByHandle(_handle, _
                                                    Native.Api.NativeEnums.GuiResourceType.UserObjects)
                Dim _affinity As IntPtr = Native.Objects.Process.GetProcessAffinityByHandle(_handle)

                RaiseEvent GatheredInfos(New TheseInfos(_gdi, _user, _affinity))
        End Select
    End Sub

End Class
