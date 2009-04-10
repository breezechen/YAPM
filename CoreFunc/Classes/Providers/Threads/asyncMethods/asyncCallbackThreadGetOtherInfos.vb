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

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackThreadGetOtherInfos

    Private _id As Integer
    Private _handle As Integer
    Private _connection As cThreadConnection
    '  Private _deg As GatheredInfos

    Public Structure TheseInfos
        Public affinity As Integer
        Public Sub New(ByVal _affinity As Integer)
            affinity = _affinity
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cThreadConnection, Optional ByVal handle As Integer = 0)
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
                Dim _affinity As Integer = GetAffinity(_id)

                RaiseEvent GatheredInfos(New TheseInfos(_affinity))
        End Select
    End Sub

    ' Return affinity
    Private Function GetAffinity(ByVal _pid As Integer) As Integer
        Dim infos As New API.THREAD_BASIC_INFORMATION
        Dim ret As Integer
        API.ZwQueryInformationThread(_handle, API.THREAD_INFORMATION_CLASS.ThreadBasicInformation, infos, Marshal.SizeOf(infos), ret)
        Return infos.AffinityMask
    End Function

End Class
