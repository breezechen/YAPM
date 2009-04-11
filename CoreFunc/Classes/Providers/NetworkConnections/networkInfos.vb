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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Net

<Serializable()> Public Class networkInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _pid As Integer
    Private _Protocol As API.NetworkProtocol
    Private _dwLocalAddr As Integer
    Private _dwLocalPort As Integer
    Private _dwRemoteAddr As Integer
    Private _dwRemotePort As Integer
    Friend _Local As IPEndPoint
    Friend _remote As IPEndPoint
    Private _key As String
    Private _State As API.MIB_TCP_STATE
    Private _localPort As Integer
    Private _procName As String
    Friend _localString As String
    Friend _remoteString As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property ProcessName() As String
        Get
            Return _procName
        End Get
    End Property
    Public ReadOnly Property Protocol() As API.NetworkProtocol
        Get
            Return _Protocol
        End Get
    End Property
    Public ReadOnly Property State() As API.MIB_TCP_STATE
        Get
            Return _State
        End Get
    End Property
    Public ReadOnly Property Local() As IPEndPoint
        Get
            Return _Local
        End Get
    End Property
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public ReadOnly Property LocalPort() As Integer
        Get
            Return _localPort
        End Get
    End Property
    Public ReadOnly Property Remote() As IPEndPoint
        Get
            Return _remote
        End Get
    End Property
    Public ReadOnly Property RemoteString() As String
        Get
            Return _remoteString
        End Get
    End Property
    Public ReadOnly Property LocalString() As String
        Get
            Return _localString
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef lc As API.LightConnection)

        _pid = lc.dwOwningPid
        _Protocol = lc.dwType
        _State = CType(lc.dwState, API.MIB_TCP_STATE)
        _Local = lc.local
        _remote = lc.remote
        _procName = cProcess.GetProcessName(_pid)


    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As API.LightConnection)
        With newI
            _State = CType(.dwState, API.MIB_TCP_STATE)
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(3) As String

        s(0) = "Remote"
        s(1) = "Protocol"
        s(2) = "ProcessId"
        s(3) = "State"

        Return s
    End Function

End Class
