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

Imports CoreFunc.cNetworkConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management
Imports System.Net

Public Class asyncCallbackNetworkEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cNetworkConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cNetworkConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid() As Integer
        Public all As Boolean
        Public forInstanceId As Integer
        Public Sub New(ByRef pi() As Integer, ByVal al As Boolean, ByVal ii As Integer)
            forInstanceId = ii
            pid = pi
            all = al
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, networkInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), networkInfos))
            Next
        End If
        ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestNetworkConnectionList, pObj.pid, pObj.all)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI


            Case Else
                ' Local

                Dim _dico As New Dictionary(Of String, networkInfos)
                Dim length As Integer = 0


                ' --------- TCP
                API.GetExtendedTcpTable(IntPtr.Zero, length, False, 2, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0)
                Dim pt As IntPtr = Marshal.AllocHGlobal(length)
                API.GetExtendedTcpTable(pt, length, False, 2, API.TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0)

                If length = 0 Then
                    sem.Release()
                    Exit Sub
                End If

                Dim count As Integer = Marshal.ReadInt32(pt, 0)
                Dim intOffset As Integer = 0
                Dim z As Integer = 0

                For i As Integer = 0 To count - 1
                    Dim tcp_item As API.MIB_TCPROW_OWNER_PID

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
                    Dim bOkToAdd As Boolean = pObj.all
                    If pObj.all = False Then
                        For Each pid As Integer In pObj.pid
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

                        Dim res As New API.LightConnection
                        With res
                            .dwOwningPid = tcp_item.dwOwningPid
                            .dwState = tcp_item.dwState
                            .local = n
                            .remote = n2
                            .dwType = API.NetworkProtocol.Tcp
                        End With
                        Dim key As String = res.dwOwningPid.ToString & "-" & API.NetworkProtocol.Tcp.ToString & "-" & res.local.ToString
                        If _dico.ContainsKey(key) = False Then
                            _dico.Add(key, New networkInfos(res))
                        End If
                    End If
                Next

                Marshal.FreeHGlobal(pt)

                count = z         ' Real size



                ' --------- UDP
                API.GetExtendedUdpTable(IntPtr.Zero, length, False, 2, API.UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0)
                pt = Marshal.AllocHGlobal(length)
                API.GetExtendedUdpTable(pt, length, False, 2, API.UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0)

                Dim count2 As Integer = Marshal.ReadInt32(pt, 0)
                intOffset = 0

                For i As Integer = 0 To count2 - 1
                    Dim udp_item As API.MIB_UDPROW_OWNER_PID

                    With udp_item
                        .dwLocalAddr = Marshal.ReadInt32(pt, intOffset + 4)
                        .dwLocalPort = Marshal.ReadInt32(pt, intOffset + 8)
                        .dwOwningPid = Marshal.ReadInt32(pt, intOffset + 12)
                    End With
                    intOffset += 12

                    ' Test if belongs to PID list
                    Dim bOkToAdd As Boolean = pObj.all
                    If pObj.all = False Then
                        For Each pid As Integer In pObj.pid
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

                        Dim res As New API.LightConnection
                        With res
                            .dwOwningPid = udp_item.dwOwningPid
                            .dwState = 0
                            .local = n
                            .dwType = API.NetworkProtocol.Udp
                            .remote = Nothing
                        End With

                        Dim key As String = res.dwOwningPid.ToString & "-" & API.NetworkProtocol.Udp.ToString & "-" & res.local.ToString
                        If _dico.ContainsKey(key) = False Then
                            _dico.Add(key, New networkInfos(res))
                        End If
                    End If
                Next

                Marshal.FreeHGlobal(pt)

                ctrl.Invoke(deg, True, _dico, API.GetError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub

End Class
