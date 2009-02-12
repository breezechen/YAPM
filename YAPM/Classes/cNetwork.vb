' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Imports System.Runtime.InteropServices
Imports System.Net

Public Class cNetwork

    ' ========================================
    ' API declarations
    ' ========================================
#Region "API"
    Public Enum NetworkProtocol As Integer
        Tcp
        Udp
    End Enum
    Public Enum TCP_TABLE_CLASS As Integer
        TCP_TABLE_BASIC_LISTENER
        TCP_TABLE_BASIC_CONNECTIONS
        TCP_TABLE_BASIC_ALL
        TCP_TABLE_OWNER_PID_LISTENER
        TCP_TABLE_OWNER_PID_CONNECTIONS
        TCP_TABLE_OWNER_PID_ALL
        TCP_TABLE_OWNER_MODULE_LISTENER
        TCP_TABLE_OWNER_MODULE_CONNECTIONS
        TCP_TABLE_OWNER_MODULE_ALL
    End Enum
    Public Enum UDP_TABLE_CLASS As Integer
        UDP_TABLE_BASIC
        UDP_TABLE_OWNER_PID
        UDP_TABLE_OWNER_MODULE
    End Enum
    Public Structure MIB_TCPROW_OWNER_PID
        Dim dwState As Integer
        Dim dwLocalAddr As Integer
        Dim dwLocalPort As Integer
        Dim dwRemoteAddr As Integer
        Dim dwRemotePort As Integer
        Dim dwOwningPid As Integer
    End Structure
    Public Structure MIB_UDPROW_OWNER_PID
        'Dim dwState As Integer
        Dim dwLocalAddr As Integer
        Dim dwLocalPort As Integer
        'Dim dwRemoteAddr As Integer
        'Dim dwRemotePort As Integer
        Dim dwOwningPid As Integer
    End Structure
    Public Enum MIB_TCP_STATE As Integer
        Closed = 1
        Listening
        SynSent
        SynReceived
        Established
        FinWait1
        FinWait2
        CloseWait
        Closing
        LastAck
        TimeWait
        DeleteTcb
    End Enum

    <DllImport("iphlpapi.dll", SetLastError:=True)> _
    Private Shared Function GetExtendedTcpTable(ByVal Table As IntPtr, ByRef Size As Integer, _
        ByVal Order As Boolean, ByVal IpVersion As Integer, _
        ByVal TableClass As TCP_TABLE_CLASS, ByVal Reserved As Integer) As Integer
    End Function
    <DllImport("iphlpapi.dll", SetLastError:=True)> _
    Private Shared Function GetExtendedUdpTable(ByVal Table As IntPtr, ByRef Size As Integer, _
        ByVal Order As Boolean, ByVal IpVersion As Integer, _
        ByVal TableClass As UDP_TABLE_CLASS, ByVal Reserved As Integer) As Integer
    End Function

#End Region


    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer
    Private _Protocol As NetworkProtocol
    Private _dwLocalAddr As Integer
    Private _dwLocalPort As Integer
    Private _dwRemoteAddr As Integer
    Private _dwRemotePort As Integer
    Private _Local As IPEndPoint
    Private _remote As IPEndPoint
    Private _key As String
    Private _State As MIB_TCP_STATE
    Private _isDisplayed As Boolean
    Private _localPort As Integer
    Private _procName As String
    Private _localString As String
    Private _remoteString As String

    Private _newItem As Boolean = False
    Private _killedItem As Boolean = False

    ' ========================================
    ' Properties
    ' ========================================
    Public Property IsKilledItem() As Boolean
        Get
            Return _killedItem
        End Get
        Set(ByVal value As Boolean)
            _killedItem = value
        End Set
    End Property
    Public Property IsNewItem() As Boolean
        Get
            Return _newItem
        End Get
        Set(ByVal value As Boolean)
            _newItem = value
        End Set
    End Property
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
    Public ReadOnly Property Protocol() As NetworkProtocol
        Get
            Return _Protocol
        End Get
    End Property
    Public ReadOnly Property State() As MIB_TCP_STATE
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
    Public Property isDisplayed() As Boolean
        Get
            Return _isDisplayed
        End Get
        Set(ByVal value As Boolean)
            _isDisplayed = value
        End Set
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


    ' ========================================
    ' Public functions
    ' ========================================
    Public Sub New(ByVal tcp As MIB_TCPROW_OWNER_PID, ByVal protocol As NetworkProtocol, ByVal local As IPEndPoint, ByVal remote As IPEndPoint)
        MyBase.New()
        _pid = tcp.dwOwningPid
        _Protocol = protocol
        _State = CType(tcp.dwState, MIB_TCP_STATE)
        _Local = local
        _remote = remote
        _localPort = tcp.dwLocalPort
        _procName = cProcess.GetProcessName(_pid)
        _key = CStr(_pid) & "|" & local.Address.ToString & "|" & protocol.ToString & "|" & CStr(local.Port) & "|" & CStr(tcp.GetHashCode)
    End Sub
    Public Sub New(ByVal udp As MIB_UDPROW_OWNER_PID, ByVal protocol As NetworkProtocol, ByVal local As IPEndPoint)
        MyBase.New()
        _pid = udp.dwOwningPid
        _Protocol = protocol
        _localPort = udp.dwLocalPort
        _State = CType(-1, MIB_TCP_STATE)
        _Local = local
        _remote = Nothing
        _procName = cProcess.GetProcessName(_pid)
        _key = CStr(_pid) & "|" & local.Address.ToString & "|" & protocol.ToString & "|" & CStr(local.Port) & "|" & CStr(udp.GetHashCode)
    End Sub

    Public Sub New(ByVal nw As cNetwork)
        MyBase.New()
        _pid = nw.ProcessId
        _Protocol = nw.Protocol
        _State = nw.State
        _Local = nw.Local
        _remote = nw.Remote
        _newItem = nw.IsNewItem
        _killedItem = nw.IsKilledItem

        Try
            Dim callback As System.AsyncCallback = AddressOf ProcessLocalDnsInformation
            Dns.BeginGetHostEntry(_Local.Address, callback, Nothing)
        Catch ex As Exception
            ' null address
        End Try
        Try
            Dim callback As System.AsyncCallback = AddressOf ProcessRemoteDnsInformation
            Dns.BeginGetHostEntry(_remote.Address, callback, Nothing)
        Catch ex As Exception
            ' null address
        End Try

        _localPort = nw.LocalPort
        _key = nw.Key
        _procName = nw.ProcessName
    End Sub

    Private Sub ProcessLocalDnsInformation(ByVal result As IAsyncResult)
        Dim host As IPHostEntry = Dns.EndGetHostEntry(result)
        _localString = host.HostName
    End Sub
    Private Sub ProcessRemoteDnsInformation(ByVal result As IAsyncResult)
        Dim host As IPHostEntry = Dns.EndGetHostEntry(result)
        _remoteString = host.HostName
    End Sub
    

    ' Get all active connections
    Public Shared Function EnumerateAll(ByRef net() As cNetwork) As Integer
        Dim res() As cNetwork
        ReDim res(0)
        Dim length As Integer = 0

        ' --------- TCP
        GetExtendedTcpTable(IntPtr.Zero, length, False, 2, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0)
        Dim pt As IntPtr = Marshal.AllocHGlobal(length)
        GetExtendedTcpTable(pt, length, False, 2, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0)

        Dim count As Integer = Marshal.ReadInt32(pt, 0)
        ReDim res(count)
        Dim intOffset As Integer = 0

        For i As Integer = 0 To count - 1
            Dim TCP_ROW As MIB_TCPROW_OWNER_PID

            With TCP_ROW
                .dwState = Marshal.ReadInt32(pt, intOffset + 4)
                .dwLocalAddr = Marshal.ReadInt32(pt, intOffset + 8)
                .dwLocalPort = Marshal.ReadInt32(pt, intOffset + 12)
                .dwRemoteAddr = Marshal.ReadInt32(pt, intOffset + 16)
                .dwRemotePort = Marshal.ReadInt32(pt, intOffset + 20)
                .dwOwningPid = Marshal.ReadInt32(pt, intOffset + 24)
            End With
            intOffset += 24

            Dim n As New IPEndPoint(TCP_ROW.dwLocalAddr, TCP_ROW.dwLocalPort)
            Dim n2 As IPEndPoint
            If TCP_ROW.dwRemoteAddr > 0 Then
                n2 = New IPEndPoint(TCP_ROW.dwRemoteAddr, TCP_ROW.dwRemotePort)
            Else
                n2 = Nothing
            End If
            res(i) = New cNetwork(TCP_ROW, NetworkProtocol.Tcp, n, n2)
        Next

        Marshal.FreeHGlobal(pt)


        ' --------- UDP
        GetExtendedUdpTable(IntPtr.Zero, length, False, 2, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0)
        pt = Marshal.AllocHGlobal(length)
        GetExtendedUdpTable(pt, length, False, 2, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0)

        Dim count2 As Integer = Marshal.ReadInt32(pt, 0)
        ReDim Preserve res(count + count2 - 1)
        intOffset = 0

        For i As Integer = 0 To count2 - 1
            Dim UDP_ROW As MIB_UDPROW_OWNER_PID

            With UDP_ROW
                .dwLocalAddr = Marshal.ReadInt32(pt, intOffset + 4)
                .dwLocalPort = Marshal.ReadInt32(pt, intOffset + 8)
                .dwOwningPid = Marshal.ReadInt32(pt, intOffset + 12)
            End With
            intOffset += 12

            Dim n As New IPEndPoint(UDP_ROW.dwLocalAddr, UDP_ROW.dwLocalPort)
            res(i + count) = New cNetwork(UDP_ROW, NetworkProtocol.Udp, n)
        Next

        Marshal.FreeHGlobal(pt)

        net = res
        Return res.Length
    End Function

    ' 
    Public Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Local"
                If Len(_localString) > 0 Then
                    res = Me.Local.ToString & "  ----  " & _localString
                Else
                    res = Me.Local.ToString
                End If
            Case "Remote"
                If Me.Remote IsNot Nothing Then
                    If Len(_remoteString) > 0 Then
                        res = Me.Remote.ToString & "  ----  " & _remoteString
                    Else
                        res = Me.Remote.ToString
                    End If
                Else
                    res = ""
                End If
            Case "Protocol"
                    res = Me.Protocol.ToString.ToUpperInvariant
            Case "ProcessId"
                    res = Me.ProcessId.ToString
            Case "State"
                    If Me.State = -1 Then
                        res = ""
                    Else
                        res = Me.State.ToString
                    End If
        End Select

        Return res
    End Function

End Class
