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

Public Class asyncCallbackThreadGetOtherInfos

    Private _id As Integer
    Private _handle As IntPtr
    Private _connection As cThreadConnection
    '  Private _deg As GatheredInfos

    Public Structure TheseInfos
        Public affinity As IntPtr
        Public Sub New(ByVal _affinity As IntPtr)
            affinity = _affinity
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cThreadConnection, ByVal handle As IntPtr)
        _id = pid
        ' _deg = deg
        _handle = handle
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _affinity As IntPtr = Native.Objects.Thread.GetThreadAffinityByHandle(_handle)

                RaiseEvent GatheredInfos(New TheseInfos(_affinity))
        End Select
    End Sub

End Class
