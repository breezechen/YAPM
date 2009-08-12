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
Imports YAPM.Native.Api
Imports System.Net

Namespace Native.Objects

    Public Class Network


        ' Local enumeration of network informations
        Public Shared Sub EnumerateTcpUdpConnections(ByRef _dico As Dictionary(Of String, networkInfos), _
                                ByVal allProcesses As Boolean, _
                                Optional ByVal processIds() As Integer = Nothing)

            Dim length As Integer = 0

            ' --------- TCP
            ' Get needed size of memory to allocate
            NativeFunctions.GetExtendedTcpTable(IntPtr.Zero, length, False, 2, Enums.TcpTableClass.OwnerPidAll, 0)
            ' Allocate memory
            Dim pt As IntPtr = Marshal.AllocHGlobal(length)
            ' Get table
            If NativeFunctions.GetExtendedTcpTable(pt, length, False, 2, Enums.TcpTableClass.OwnerPidAll, 0) = 0 Then

                Dim count As Integer = Marshal.ReadInt32(pt, 0)
                Dim intOffset As Integer = 0

                For i As Integer = 0 To count - 1
                    Dim tcp_item As NativeStructs.MIB_TCPROW_OWNER_PID

                    ' Read struct with an offset of 4 bytes (these bytes contains item count)
                    ' 64TODO
                    tcp_item = CType(Marshal.PtrToStructure(New IntPtr(pt.ToInt32 + _
                                                                       4 + i * Marshal.SizeOf(tcp_item)), _
                                                            GetType(NativeStructs.MIB_TCPROW_OWNER_PID)), NativeStructs.MIB_TCPROW_OWNER_PID)

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

                Marshal.FreeHGlobal(pt)

            End If


            ' --------- UDP
            ' Get needed size of memory to allocate
            NativeFunctions.GetExtendedUdpTable(IntPtr.Zero, length, False, 2, Enums.UdpTableClass.OwnerPid, 0)
            ' Allocate memory
            pt = Marshal.AllocHGlobal(length)
            ' Get table
            If NativeFunctions.GetExtendedUdpTable(pt, length, False, 2, Enums.UdpTableClass.OwnerPid, 0) = 0 Then

                Dim count2 As Integer = Marshal.ReadInt32(pt, 0)

                For i As Integer = 0 To count2 - 1
                    Dim udp_item As NativeStructs.MIB_UDPROW_OWNER_PID

                    ' 64TODO
                    ' Read struct with an offset of 4 bytes (these bytes contains item count)
                    udp_item = CType(Marshal.PtrToStructure(New IntPtr(pt.ToInt32 + _
                                                                       4 + i * Marshal.SizeOf(udp_item)), _
                                                            GetType(NativeStructs.MIB_UDPROW_OWNER_PID)), NativeStructs.MIB_UDPROW_OWNER_PID)

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

                Marshal.FreeHGlobal(pt)
            End If
        End Sub


    End Class

End Namespace
