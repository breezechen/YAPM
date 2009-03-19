' =======================================================
' Yet Another Process Monitor (YAPM)
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
Imports System.Net

Public Class cNetwork
    Inherits cGeneralObject

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
    Public Structure LightConnection
        Dim dwState As Integer
        Dim local As IPEndPoint
        Dim remote As IPEndPoint
        Dim dwOwningPid As Integer
        Dim dwType As NetworkProtocol
        Dim key As String
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
    Private nullAddress As New IPAddress(0)     ' For address comparison
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
    Private _localPort As Integer
    Private _procName As String
    Private _localString As String
    Private _remoteString As String

    ' ========================================
    ' Properties
    ' ========================================
#Region "Properties"
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
    ' Public functions
    ' ========================================
    Public Sub New(ByRef lc As LightConnection)
        MyBase.New()
        _pid = lc.dwOwningPid
        _Protocol = lc.dwType
        _State = CType(lc.dwState, MIB_TCP_STATE)
        _Local = lc.local
        _remote = lc.remote
        _procName = cProcess.GetProcessName(_pid)
        _key = lc.key

        ' Solve DNS
        Try
            If _Local.Address.Equals(nullAddress) = False Then
                Dim callback As System.AsyncCallback = AddressOf ProcessLocalDnsInformation
                Dns.BeginGetHostEntry(_Local.Address, callback, Nothing)
            End If
        Catch ex As Exception
            '
        End Try
        Try
            If _remote IsNot Nothing AndAlso _remote.Address.Equals(nullAddress) = False Then
                Dim callback2 As System.AsyncCallback = AddressOf ProcessRemoteDnsInformation
                Dns.BeginGetHostEntry(_remote.Address, callback2, Nothing)
            End If
        Catch ex As Exception
            '
        End Try

    End Sub



    ' Get all active connections
    Public Shared Function Enumerate(ByVal all As Boolean, ByRef pidList() As Integer, _
                                     ByRef key() As String, ByRef _dico As  _
                                     Dictionary(Of String, LightConnection)) As Integer
        ReDim key(0)
        _dico.Clear()
        Dim length As Integer = 0


        ' --------- TCP
        GetExtendedTcpTable(IntPtr.Zero, length, False, 2, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0)
        Dim pt As IntPtr = Marshal.AllocHGlobal(length)
        GetExtendedTcpTable(pt, length, False, 2, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0)

        Dim count As Integer = Marshal.ReadInt32(pt, 0)
        ReDim key(count)        ' Temporary size
        Dim intOffset As Integer = 0
        Dim z As Integer = 0

        For i As Integer = 0 To count - 1
            Dim tcp_item As MIB_TCPROW_OWNER_PID

            With tcp_item
                .dwState = Marshal.ReadInt32(pt, intOffset + 4)
                .dwLocalAddr = Marshal.ReadInt32(pt, intOffset + 8)
                .dwLocalPort = Marshal.ReadInt32(pt, intOffset + 12)
                .dwRemoteAddr = Marshal.ReadInt32(pt, intOffset + 16)
                .dwRemotePort = Marshal.ReadInt32(pt, intOffset + 20)
                .dwOwningPid = Marshal.ReadInt32(pt, intOffset + 24)
            End With
            intOffset += 24

            ' Test if belongs to PID list
            Dim bOkToAdd As Boolean = all
            If all = False Then
                For Each pid As Integer In pidList
                    If pid = tcp_item.dwOwningPid Then
                        bOkToAdd = True
                        Exit For
                    End If
                Next
            End If

            If bOkToAdd Then
                Dim n As IPEndPoint = Nothing
                If tcp_item.dwLocalAddr > 0 Then
                    n = New IPEndPoint(tcp_item.dwLocalAddr, tcp_item.dwLocalPort)
                Else
                    n = New IPEndPoint(0, tcp_item.dwLocalPort)
                End If
                Dim n2 As IPEndPoint
                If tcp_item.dwRemoteAddr > 0 Then
                    n2 = New IPEndPoint(tcp_item.dwRemoteAddr, tcp_item.dwRemotePort)
                Else
                    n2 = Nothing
                End If

                key(z) = CStr(tcp_item.dwOwningPid) & "|" & tcp_item.dwLocalAddr.ToString & "|TCP|" & tcp_item.dwLocalPort.ToString
                Dim res As New LightConnection
                With res
                    .dwOwningPid = tcp_item.dwOwningPid
                    .dwState = tcp_item.dwState
                    .key = key(z)
                    .local = n
                    .remote = n2
                End With
                If _dico.ContainsKey(key(z)) = False Then
                    _dico.Add(key(z), res)
                    z += 1
                End If
            End If
        Next

        Marshal.FreeHGlobal(pt)

        count = z         ' Real size



        ' --------- UDP
        GetExtendedUdpTable(IntPtr.Zero, length, False, 2, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0)
        pt = Marshal.AllocHGlobal(length)
        GetExtendedUdpTable(pt, length, False, 2, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0)

        Dim count2 As Integer = Marshal.ReadInt32(pt, 0)
        ReDim Preserve key(count + count2 - 1)
        intOffset = 0

        For i As Integer = 0 To count2 - 1
            Dim udp_item As MIB_UDPROW_OWNER_PID

            With udp_item
                .dwLocalAddr = Marshal.ReadInt32(pt, intOffset + 4)
                .dwLocalPort = Marshal.ReadInt32(pt, intOffset + 8)
                .dwOwningPid = Marshal.ReadInt32(pt, intOffset + 12)
            End With
            intOffset += 12

            ' Test if belongs to PID list
            Dim bOkToAdd As Boolean = all
            If all = False Then
                For Each pid As Integer In pidList
                    If pid = udp_item.dwOwningPid Then
                        bOkToAdd = True
                        Exit For
                    End If
                Next
            End If

            If bOkToAdd Then
                Dim n As IPEndPoint = Nothing
                If udp_item.dwLocalAddr > 0 Then
                    n = New IPEndPoint(udp_item.dwLocalAddr, udp_item.dwLocalPort)
                Else
                    n = New IPEndPoint(0, udp_item.dwLocalPort)
                End If


                key(z) = CStr(udp_item.dwOwningPid) & "|" & udp_item.dwLocalAddr.ToString & "|UDP|" & udp_item.dwLocalPort.ToString
                Dim res As New LightConnection
                With res
                    .dwOwningPid = udp_item.dwOwningPid
                    .dwState = 0
                    .key = key(z)
                    .local = n
                    .remote = Nothing
                End With
                If _dico.ContainsKey(key(z)) = False Then
                    _dico.Add(key(z), res)
                    z += 1
                End If
            End If
        Next

        Marshal.FreeHGlobal(pt)


        ' Resize array
        count2 = z - 1     ' Real size
        ReDim Preserve key(count2)
    End Function

    ' Get informations
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Local"
                If Local IsNot Nothing Then
                    If Len(_localString) > 0 Then
                        res = Me.Local.ToString & "  ----  " & _localString
                    Else
                        res = Me.Local.ToString
                    End If
                Else
                    res = "0.0.0.0"
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
                If Me.State <= 0 Then
                    res = ""
                Else
                    res = Me.State.ToString
                End If
        End Select

        Return res
    End Function

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(3) As String

        s(0) = "Remote"
        s(1) = "Protocol"
        s(2) = "ProcessId"
        s(3) = "State"

        Return s
    End Function

    ' ========================================
    ' Private functions
    ' ========================================

    Private Sub ProcessLocalDnsInformation(ByVal result As IAsyncResult)
        Try
            Dim host As IPHostEntry = Dns.EndGetHostEntry(result)
            _localString = host.HostName
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub ProcessRemoteDnsInformation(ByVal result As IAsyncResult)
        Try
            Dim host As IPHostEntry = Dns.EndGetHostEntry(result)
            _remoteString = host.HostName
        Catch ex As Exception
            '
        End Try
    End Sub

End Class
