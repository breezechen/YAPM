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
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackLogEnumerate

    Public Enum LogItemType As Integer
        ModuleItem = 1
        ThreadItem = 2
        ServiceItem = 4
        WindowItem = 8
        HandleItem = 16
        MemoryItem = 32
        NetworkItem = 64
        DeletedItems = 128
        CreatedItems = 256
        AllItems = ModuleItem Or ThreadItem Or ServiceItem Or WindowItem Or HandleItem _
            Or MemoryItem Or NetworkItem Or DeletedItems Or CreatedItems
    End Enum

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cLogConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cLogConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public infos As LogItemType
        Public newItems As Boolean
        Public deletedItems As Boolean
        Public hSCM As IntPtr
        Public forInstanceId As Integer
        Public Sub New(ByVal _infos As LogItemType, ByVal _pid As Integer, ByVal _hSCM As IntPtr, ByVal ii As Integer)
            forInstanceId = ii
            infos = _infos
            pid = _pid
            newItems = ((_infos And LogItemType.CreatedItems) = LogItemType.CreatedItems)
            deletedItems = ((_infos And LogItemType.DeletedItems) = LogItemType.DeletedItems)
            hSCM = _hSCM
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, logItemInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), logItemInfos))
            Next
        End If
        If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        Static firstNetwork As Boolean = True
        Static firstHandles As Boolean = True
        Static firstModules As Boolean = True
        Static firstMemRegions As Boolean = True
        Static firstServices As Boolean = True
        Static firstThreads As Boolean = True
        Static firstWindows As Boolean = True

        Static _dicoNetwork As New Dictionary(Of String, networkInfos)
        Static _dicoHandles As New Dictionary(Of String, handleInfos)
        Static _dicoModules As New Dictionary(Of String, moduleInfos)
        Static _dicoMemRegions As New Dictionary(Of String, memRegionInfos)
        Static _dicoServices As New Dictionary(Of String, serviceInfos)
        Static _dicoThreads As New Dictionary(Of String, threadInfos)
        Static _dicoWindows As New Dictionary(Of String, windowInfos)

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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestLogList, pObj.pid, pObj.infos)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim _dico As New Dictionary(Of String, logItemInfos)

                Dim __dicoNetwork As New Dictionary(Of String, networkInfos)
                Dim __dicoHandles As New Dictionary(Of String, handleInfos)
                Dim __dicoModules As New Dictionary(Of String, moduleInfos)
                Dim __dicoMemRegions As New Dictionary(Of String, memRegionInfos)
                Dim __dicoServices As New Dictionary(Of String, serviceInfos)
                Dim __dicoThreads As New Dictionary(Of String, threadInfos)
                Dim __dicoWindows As New Dictionary(Of String, windowInfos)

                ' Network list
                If (pObj.infos And LogItemType.NetworkItem) = LogItemType.NetworkItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Native.Objects.Network.EnumerateTcpUdpConnections(__dicoNetwork, False, pid)

                    ' Store in static dico if it is first refresh
                    If firstNetwork Then
                        firstNetwork = False
                        _dicoNetwork = __dicoNetwork
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoNetwork.Keys
                            If Not (_dicoNetwork.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoNetwork(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoNetwork.Keys
                            If Not (__dicoNetwork.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoNetwork(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoNetwork = __dicoNetwork

                End If



                ' Handle list
                If (pObj.infos And LogItemType.HandleItem) = LogItemType.HandleItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Native.Objects.Handle.EnumerateHandleByProcessIds(pid, True, __dicoHandles)

                    ' Store in static dico if it is first refresh
                    If firstHandles Then
                        firstHandles = False
                        _dicoHandles = __dicoHandles
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoHandles.Keys
                            If Not (_dicoHandles.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoHandles(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoHandles.Keys
                            If Not (__dicoHandles.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoHandles(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoHandles = __dicoHandles

                End If



                ' Mem regions list
                If (pObj.infos And LogItemType.MemoryItem) = LogItemType.MemoryItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Native.Objects.MemRegion.EnumerateMemoryRegionsByProcessId(pid(0), __dicoMemRegions)

                    ' Store in static dico if it is first refresh
                    If firstMemRegions Then
                        firstMemRegions = False
                        _dicoMemRegions = __dicoMemRegions
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoMemRegions.Keys
                            If Not (_dicoMemRegions.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoMemRegions(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoMemRegions.Keys
                            If Not (__dicoMemRegions.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoMemRegions(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoMemRegions = __dicoMemRegions

                End If



                ' Modules list
                If (pObj.infos And LogItemType.ModuleItem) = LogItemType.ModuleItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Call asyncCallbackModuleEnumerate.enumModules(New asyncCallbackModuleEnumerate.poolObj(pid, 0), __dicoModules, False)

                    ' Store in static dico if it is first refresh
                    If firstModules Then
                        firstModules = False
                        _dicoModules = __dicoModules
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoModules.Keys
                            If Not (_dicoModules.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoModules(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoModules.Keys
                            If Not (__dicoModules.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoModules(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoModules = __dicoModules

                End If



                ' Services list
                If (pObj.infos And LogItemType.ServiceItem) = LogItemType.ServiceItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Native.Objects.Service.EnumerateServices(pObj.hSCM, __dicoServices, False, False, pid(0))

                    ' Store in static dico if it is first refresh
                    If firstServices Then
                        firstServices = False
                        _dicoServices = __dicoServices
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoServices.Keys
                            If Not (_dicoServices.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoServices(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoServices.Keys
                            If Not (__dicoServices.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoServices(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoServices = __dicoServices

                End If



                ' Threads list
                If (pObj.infos And LogItemType.ThreadItem) = LogItemType.ThreadItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Native.Objects.Thread.EnumerateThreadsByProcessId(__dicoThreads, pid)

                    ' Store in static dico if it is first refresh
                    If firstThreads Then
                        firstThreads = False
                        _dicoThreads = __dicoThreads
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoThreads.Keys
                            If Not (_dicoThreads.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoThreads(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoThreads.Keys
                            If Not (__dicoThreads.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoThreads(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoThreads = __dicoThreads

                End If



                ' Windows list
                If (pObj.infos And LogItemType.WindowItem) = LogItemType.WindowItem Then

                    ' Get list
                    Dim pid(0) As Integer
                    pid(0) = pObj.pid
                    Native.Objects.Window.EnumerateWindowsByProcessId(pid, False, True, __dicoWindows)

                    ' Store in static dico if it is first refresh
                    If firstWindows Then
                        firstWindows = False
                        _dicoWindows = __dicoWindows
                    End If

                    ' Make diff between __dicoXXX and _dicoXXX, and add
                    ' difference to _dico

                    ' Check if there are new items
                    If pObj.newItems Then
                        For Each z As String In __dicoWindows.Keys
                            If Not (_dicoWindows.ContainsKey(z)) Then
                                ' New item
                                Dim _tmp As logItemInfos = New logItemInfos(__dicoWindows(z), logItemInfos.CREATED_OR_DELETED.created)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If


                    ' Check if there are deleted items
                    If pObj.deletedItems Then
                        For Each z As String In _dicoWindows.Keys
                            If Not (__dicoWindows.ContainsKey(z)) Then
                                ' Deleted item
                                Dim _tmp As logItemInfos = New logItemInfos(_dicoWindows(z), logItemInfos.CREATED_OR_DELETED.deleted)
                                _dico.Add(_tmp.Key, _tmp)
                            End If
                        Next
                    End If

                    ' Save __dicoXXX into _dicoXXX
                    _dicoWindows = __dicoWindows

                End If



                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub

End Class
