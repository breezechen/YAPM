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

Imports System.Runtime.InteropServices
Imports System.Net
Imports YAPM.Native.Api
Imports YAPM.Common.Misc

Namespace Native.Objects

    Public Class Network



        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Protection for enumeration
        Private Shared semEnum As New System.Threading.Semaphore(1, 1)


        ' ========================================
        ' Public properties
        ' ========================================


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Local enumeration of network informations
        Public Shared Sub EnumerateTcpUdpConnections(ByRef _dico As Dictionary(Of String, networkInfos), _
                                ByVal allProcesses As Boolean, _
                                Optional ByVal processIds() As Integer = Nothing)

            Dim length As Integer = 0

            If processIds Is Nothing AndAlso allProcesses = False Then
                Exit Sub
            End If

            semEnum.WaitOne()

            ' --------- TCP
            ' Get needed size of memory to allocate
            NativeFunctions.GetExtendedTcpTable(IntPtr.Zero, length, False, NativeEnums.IpVersion.AfInet, _
                                                Enums.TcpTableClass.OwnerPidAll, 0)
            ' Allocate memory
            Dim memTcp As New Memory.MemoryAlloc(length)
            ' Get table
            If NativeFunctions.GetExtendedTcpTable(memTcp.Pointer, length, False, NativeEnums.IpVersion.AfInet, Enums.TcpTableClass.OwnerPidAll, 0) = 0 Then

                Dim count As Integer = memTcp.ReadInt32(0)

                For i As Integer = 0 To count - 1
                    Dim tcp_item As NativeStructs.MibTcpRowOwnerPid

                    ' Read struct with an offset of 4 bytes (these bytes contains item count)
                    ' 4 first bytes for Count
                    tcp_item = memTcp.ReadStruct(Of NativeStructs.MibTcpRowOwnerPid)(&H4, i)

                    ' Test if belongs to PID list
                    Dim bOkToAdd As Boolean = allProcesses
                    If allProcesses = False Then
                        For Each pid As Integer In processIds
                            If pid = tcp_item.dwOwningPid Then
                                bOkToAdd = True
                                Exit For
                            End If
                        Next
                    End If

                    If bOkToAdd Then
                        Dim n As IPEndPoint = Nothing
                        If tcp_item.dwLocalAddr > 0 Then
                            n = New IPEndPoint(tcp_item.dwLocalAddr, PermuteBytes(tcp_item.dwLocalPort))
                        Else
                            n = New IPEndPoint(0, PermuteBytes(tcp_item.dwLocalPort))
                        End If
                        Dim n2 As IPEndPoint
                        If tcp_item.dwRemoteAddr > 0 Then
                            n2 = New IPEndPoint(tcp_item.dwRemoteAddr, PermuteBytes(tcp_item.dwRemotePort))
                        Else
                            n2 = Nothing
                        End If

                        Dim res As New Structs.LightConnection
                        With res
                            .dwOwningPid = tcp_item.dwOwningPid
                            .dwState = tcp_item.dwState
                            .local = n
                            .remote = n2
                            .dwType = Enums.NetworkProtocol.Tcp
                        End With
                        Dim key As String = res.dwOwningPid.ToString & "-" & Enums.NetworkProtocol.Tcp.ToString & "-" & res.local.ToString
                        If _dico.ContainsKey(key) = False Then
                            _dico.Add(key, New networkInfos(res))
                        End If
                    End If
                Next

                memTcp.Free()

            End If


            ' --------- UDP
            ' Get needed size of memory to allocate
            NativeFunctions.GetExtendedUdpTable(IntPtr.Zero, length, False, NativeEnums.IpVersion.AfInet, Enums.UdpTableClass.OwnerPid, 0)
            ' Allocate memory
            Dim memUdp As New Memory.MemoryAlloc(length)
            ' Get table
            If NativeFunctions.GetExtendedUdpTable(memUdp.Pointer, length, False, NativeEnums.IpVersion.AfInet, Enums.UdpTableClass.OwnerPid, 0) = 0 Then

                Dim count2 As Integer = memUdp.ReadInt32(0)

                For i As Integer = 0 To count2 - 1
                    Dim udp_item As NativeStructs.MibUdpRowOwnerId

                    ' Read struct with an offset of 4 bytes (these bytes contains item count)
                    ' 4 first bytes for Count
                    ' Read struct with an offset of 4 bytes (these bytes contains item count)
                    ' 4 first bytes for Count
                    udp_item = memUdp.ReadStruct(Of NativeStructs.MibUdpRowOwnerId)(&H4, i)

                    ' Test if belongs to PID list
                    Dim bOkToAdd As Boolean = allProcesses
                    If allProcesses = False Then
                        For Each pid As Integer In processIds
                            If pid = udp_item.dwOwningPid Then
                                bOkToAdd = True
                                Exit For
                            End If
                        Next
                    End If

                    If bOkToAdd Then
                        Dim n As IPEndPoint = Nothing
                        If udp_item.dwLocalAddr > 0 Then
                            n = New IPEndPoint(udp_item.dwLocalAddr, PermuteBytes(udp_item.dwLocalPort))
                        Else
                            n = New IPEndPoint(0, PermuteBytes(udp_item.dwLocalPort))
                        End If

                        Dim res As New Structs.LightConnection
                        With res
                            .dwOwningPid = udp_item.dwOwningPid
                            .dwState = 0
                            .local = n
                            .dwType = Enums.NetworkProtocol.Udp
                            .remote = Nothing
                        End With

                        Dim key As String = res.dwOwningPid.ToString & "-" & Enums.NetworkProtocol.Udp.ToString & "-" & res.local.ToString
                        If _dico.ContainsKey(key) = False Then
                            _dico.Add(key, New networkInfos(res))
                        End If
                    End If
                Next

                memUdp.Free()
            End If

            semEnum.Release()
        End Sub

        ' Close a TCP connection
        Public Shared Function CloseTcpConnectionByIPEndPoints(ByVal local As IPEndPoint, _
                                                        ByVal remote As IPEndPoint) As Integer
            Dim row As New NativeStructs.MibTcpRow
            With row
                If remote IsNot Nothing Then
                    .RemotePort = Common.Misc.PermuteBytes(remote.Port)
                    .RemoteAddress = Common.Misc.getAddressAsInteger(remote)
                Else
                    .RemotePort = 0
                    .RemoteAddress = 0
                End If
                .LocalAddress = Common.Misc.getAddressAsInteger(local)
                .LocalPort = Common.Misc.PermuteBytes(local.Port)
                .State = Enums.MibTcpState.DeleteTcb
            End With

            Return NativeFunctions.SetTcpEntry(row)
        End Function

        ' Connect to a remote machine
        Public Shared Function ConnectToRemoteMachine(ByVal machineName As String, _
                                                    ByVal user As String, _
                                                    ByVal password As System.Security.SecureString) As Boolean

            ' We should NOT use password as plain text, but that's the only way
            ' to use it in Win32 functions...
            Dim pass As String = ""
            Dim b() As Char = Common.Misc.SecureStringToCharArray(password)
            For Each ch As Char In b
                pass &= ch
            Next

            Dim Net As New Api.NativeStructs.NetResource
            With Net
                .dwType = NativeEnums.NetResourceType.RESOURCETYPE_ANY
                .lpProvider = Nothing
                .lpLocalName = Nothing
                .lpRemoteName = "\\" & machineName & "\IPC$"
            End With

            Dim ret As Integer
            ret = CancelConnection(machineName, Net, password, user)
            ret = NativeFunctions.WNetAddConnection2(Net, pass, user, _
                        NativeEnums.AddConnectionFlag.ConnectTemporary)
            ret = NativeFunctions.WNetAddConnection2(Net, pass, user, _
                            NativeEnums.AddConnectionFlag.ConnectCommandLine Or _
                            NativeEnums.AddConnectionFlag.ConnectTemporary)

            If (ret <> 0) AndAlso (user <> Nothing) Then
                If ret = 1219 Then
                    ' Connection already created. Disconnecting...
                    ret = CancelConnection(machineName, Net, password, user)
                Else
                    If ret = 1326 Then
                        If InStr(user, "\"c) = 0 Then
                            Dim CurrentUserName As String = "localhost\" & user
                            ret = NativeFunctions.WNetAddConnection2(Net, pass, _
                                CurrentUserName, _
                                NativeEnums.AddConnectionFlag.ConnectTemporary)
                        End If
                    End If
                End If
                If ret <> 0 Then
                    ' Error !
                    Return False
                End If
            End If

            Return True

        End Function

        ' Disconnect from remote machine
        Public Shared Function DisconnectFromRemoteMachine(ByVal machineName As String, _
                                                           Optional ByVal force As Boolean = True) As Boolean
            Dim ret As Integer = _
                NativeFunctions.WNetCancelConnection2(machineName, 0, force)
            Return (ret = 0)
        End Function

        ' Copy a file to system32 dir on a remote machine...
        ' This call is synchronous (blocking)
        Public Shared Function SyncCopyFileToRemoteSystem32(ByVal remoteMachine As String, _
                                            ByVal localPath As String, _
                                            ByVal remoteName As String) As Boolean
            Dim remote As String = "\\" & remoteMachine & "\ADMIN$\System32"
            Return NativeFunctions.CopyFile(localPath, remote & "\" & remoteName, False)
        End Function



        ' ========================================
        ' Private functions
        ' ========================================

        ' Cancel connection
        Private Shared Function CancelConnection(ByVal host As String, _
                                        ByVal net As NativeStructs.NetResource, _
                                        ByVal password As System.Security.SecureString, _
                                        ByVal user As String) As Integer

            ' We should NOT use password as plain text, but that's the only way
            ' to use it in Win32 functions...
            Dim pass As String = ""
            Dim b() As Char = Common.Misc.SecureStringToCharArray(password)
            For Each ch As Char In b
                pass &= ch
            Next

            DisconnectFromRemoteMachine(host)
            Return NativeFunctions.WNetAddConnection2(net, pass, user, _
                                    NativeEnums.AddConnectionFlag.ConnectTemporary)
        End Function

    End Class

End Namespace
