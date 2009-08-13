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
' - Declaration of some structures used by NtQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Net

<Serializable()> Public Class networkInfos
    Inherits generalInfos

    Public Shared Operator =(ByVal m1 As networkInfos, ByVal m2 As networkInfos) As Boolean
        Return (m1.ProcessId = m2.ProcessId AndAlso _
            m2.State = m1.State AndAlso _
            m2.Protocol = m1.Protocol AndAlso _
            ((m1.Local Is Nothing AndAlso m2.Local Is Nothing) _
            OrElse (m1.Local IsNot Nothing AndAlso m2.Local IsNot Nothing AndAlso m2.Local.Equals(m1.Local))) AndAlso _
            ((m1.Remote Is Nothing AndAlso m2.Remote Is Nothing) OrElse (m1.Remote IsNot Nothing AndAlso m2.Remote IsNot Nothing AndAlso m2.Remote.Equals(m1.Remote))))
    End Operator
    Public Shared Operator <>(ByVal m1 As networkInfos, ByVal m2 As networkInfos) As Boolean
        Return Not (m1 = m2)
    End Operator


#Region "Private attributes"

    Private _pid As Integer
    Private _Protocol As Native.Api.Enums.NetworkProtocol
    Friend _Local As IPEndPoint
    Friend _remote As IPEndPoint
    Private _key As String
    Private _State As API.MIB_TCP_STATE
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
    Public ReadOnly Property Protocol() As Native.Api.Enums.NetworkProtocol
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
    Public Sub Merge(ByRef newI As networkInfos)
        With newI
            _State = .State
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
        Dim s(5) As String

        s(0) = "Remote"
        s(1) = "Protocol"
        s(2) = "ProcessId"
        s(3) = "State"
        s(4) = "LocalPortDescription"
        s(5) = "RemotePortDescription"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Local"
            s = s2
        End If

        Return s
    End Function

End Class
