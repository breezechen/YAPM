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

Imports Native.Api.Enums

Public Class LogProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Current processes running PID <-> (string <-> envVar)
    Private Shared _currentLog As New Dictionary(Of Integer, Dictionary(Of String, logItemInfos))
    Friend Shared _semLog As New System.Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False

    ' Sempahore to protect async ProcessEnumeration
    Friend Shared _semProcessEnumeration As New System.Threading.Semaphore(1, 1)


    ' ========================================
    ' Public properties
    ' ========================================

    ' First refresh done ?
    Public Shared Property FirstRefreshDone() As Boolean
        Get
            Return _firstRefreshDone
        End Get
        Friend Set(ByVal value As Boolean)
            _firstRefreshDone = value
        End Set
    End Property

    ' Clear list of env variables
    Public Shared Sub ClearList()
        Try
            _semLog.WaitOne()
            _currentLog.Clear()
        Finally
            _semLog.Release()
        End Try
    End Sub

    ' Clear list for a specific processID
    Public Shared Sub ClearListForAnId(ByVal pid As Integer)
        Try
            _semLog.WaitOne()
            If _currentLog.ContainsKey(pid) Then
                _currentLog(pid).Clear()
            End If
        Finally
            _semLog.Release()
        End Try
    End Sub

    ' Set list of current processes
    Public Shared Sub SetCurrentLog(ByVal pid As Integer, ByVal value As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, logItemInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semLog.WaitOne()

            ' Add a new entry
            If _currentLog.ContainsKey(pid) = False Then
                _currentLog.Add(pid, New Dictionary(Of String, logItemInfos))
            End If

            ' Get deleted items
            For Each vars As String In _currentLog(pid).Keys
                If Not (value.ContainsKey(vars)) Then
                    Dim ii As logItemInfos = _currentLog(pid)(vars)
                    ii.State = logItemInfos.CreatedOrDeleted.Deleted
                    _dicoDel.Add(vars & "d", ii)
                    _dicoDelSimp.Add(vars & "d")
                End If
            Next

            ' Get new items
            For Each vars As String In value.Keys
                If Not (_currentLog(pid).ContainsKey(vars)) Then
                    _dicoNew.Add(vars)
                End If
            Next

            ' Re-assign dico
            _currentLog(pid) = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semLog.Release()
        End Try

        ' Raise events
        RaiseEvent GotDeletedItems(_dicoDel, instanceId, res)
        RaiseEvent GotNewItems(_dicoNew, value, instanceId, res)
        RaiseEvent GotRefreshed(_dicoNew, _dicoDel, _dicoDelSimp, value, instanceId, res)
        _firstRefreshDone = True

    End Sub


    ' ========================================
    ' Other public
    ' ========================================

    ' Shared events
    Public Shared Event GotNewItems(ByVal keys As List(Of String), ByVal newItems As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal keys As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal newItems As List(Of String), ByVal dicoDels As Dictionary(Of String, logItemInfos), ByVal delItems As List(Of String), ByVal Dico As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public pid As Integer
        Public infos As LogItemType
        Public newItems As Boolean
        Public deletedItems As Boolean
        Public instId As Integer
        Public Sub New(ByVal procId As Integer, ByVal _infos As LogItemType, ByVal instanceId As Integer)
            pid = procId
            infos = _infos
            newItems = ((_infos And LogItemType.CreatedItems) = LogItemType.CreatedItems)
            deletedItems = ((_infos And LogItemType.DeletedItems) = LogItemType.DeletedItems)
            instId = instanceId
        End Sub
    End Structure



    ' ========================================
    ' Public functions
    ' ========================================

    ' Constructor
    Public Sub New()
        ' Add handler for general connection/deconnection
        AddHandler Program.Connection.Connected, AddressOf eventConnected
        AddHandler Program.Connection.Disconnected, AddressOf eventDisConnected
        AddHandler Program.Connection.Socket.ReceivedData, AddressOf eventSockReceiveData
    End Sub

    ' Refresh list of env variables by processId depending on the connection NOW
    Public Shared Sub Update(ByVal pid As Integer, ByVal infos As LogItemType, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf LogProvider.ProcessEnumeration), _
                New LogProvider.asyncEnumPoolObj(pid, infos, instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal pid As Integer, ByVal infos As LogItemType, ByVal instanceId As Integer)
        ' This is of course sync
        LogProvider.ProcessEnumeration(New LogProvider.asyncEnumPoolObj(pid, infos, instanceId))
    End Sub


    ' ========================================
    ' Private functions
    ' ========================================

    ' Called when connected
    Private Sub eventConnected()

        ' Connect
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' Nothing special here

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' Nothing special here

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Nothing special here

            Case cConnection.TypeOfConnection.LocalConnection
                ' Nothing special here
        End Select

    End Sub

    ' Called when disconnected
    Private Sub eventDisConnected()
        ' Nothing special here
    End Sub

    ' Called when socket receive data
    Private Sub eventSockReceiveData(ByRef data As cSocketData)

        ' Exit immediately if not connected
        If Program.Connection.IsConnected AndAlso _
            Program.Connection.Type = cConnection.TypeOfConnection.RemoteConnectionViaSocket Then

            If data Is Nothing Then
                Trace.WriteLine("Serialization error")
                Exit Sub
            End If

            If data.Type = cSocketData.DataType.RequestedList AndAlso _
                data.Order = cSocketData.OrderType.RequestLogList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of env variables !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, logItemInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), logItemInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary.
        ' Have to get the processId of the current list of processes, as there might
        ' be log enumeration for more than one process.
        ' So we retrieve the informations by enumerating the variables and getting
        ' the first PID
        Dim pid As Integer
        For Each it As logItemInfos In _dico.Values
            pid = it.ProcessId
            Exit For
        Next
        LogProvider.SetCurrentLog(pid, _dico, instanceId)

    End Sub

    ' Enumeration of log items
    Private Shared Sub ProcessEnumeration(ByVal thePoolObj As Object)

        Try
            ' Synchronisation
            _semProcessEnumeration.WaitOne()

            If Program.Connection.IsConnected Then

                Dim pObj As asyncEnumPoolObj = DirectCast(thePoolObj, asyncEnumPoolObj)
                Select Case Program.Connection.Type

                    Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                        ' Send cDat
                        Try
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestLogList, pObj.pid, pObj.infos)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' Not available

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Not available

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of String, logItemInfos)

                        _dico = SharedLocalSyncEnumerate(pObj)

                        ' Save current processes into a dictionary
                        LogProvider.SetCurrentLog(pObj.pid, _dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub


    ' Shared, local and sync enumeration
    Private Shared Function SharedLocalSyncEnumerate(ByVal pObj As asyncEnumPoolObj) As Dictionary(Of String, logItemInfos)

        Try
            ' Synchro
            _semLog.WaitOne()

            Dim _dico As New Dictionary(Of String, logItemInfos)


            ' Network list
            If (pObj.infos And LogItemType.NetworkItem) = LogItemType.NetworkItem Then

                ' Get list
                Dim tmp As New Dictionary(Of String, networkInfos)
                Native.Objects.Network.EnumerateTcpUdpConnections(tmp, False, pObj.pid)

                ' Convert it into logItemInfos list
                For Each it As networkInfos In tmp.Values
                    Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                    _dico.Add(logIt.Key, logIt)
                Next

            End If



            ' Handle list
            If (pObj.infos And LogItemType.HandleItem) = LogItemType.HandleItem Then

                ' Get list
                Dim tmp As New Dictionary(Of String, handleInfos)
                Native.Objects.Handle.EnumerateHandleByProcessId(pObj.pid, True, tmp)

                ' Convert it into logItemInfos list
                For Each it As handleInfos In tmp.Values
                    Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                    _dico.Add(logIt.Key, logIt)
                Next

            End If



            ' Mem regions list
            If (pObj.infos And LogItemType.MemoryItem) = LogItemType.MemoryItem Then

                ' Get list
                Dim tmp As New Dictionary(Of String, memRegionInfos)
                Dim pid(0) As Integer
                pid(0) = pObj.pid
                Native.Objects.MemRegion.EnumerateMemoryRegionsByProcessId(pid(0), tmp)

                ' Convert it into logItemInfos list
                For Each it As memRegionInfos In tmp.Values
                    Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                    _dico.Add(logIt.Key, logIt)
                Next

            End If



            ' Modules list
            If (pObj.infos And LogItemType.ModuleItem) = LogItemType.ModuleItem Then

                ' Get list
                Dim tmp As Dictionary(Of String, moduleInfos) = Native.Objects.Module.EnumerateModulesByProcessId(pObj.pid, True)

                ' Convert it into logItemInfos list
                For Each it As moduleInfos In tmp.Values
                    Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                    _dico.Add(logIt.Key, logIt)
                Next

            End If



            ' Services list
            If (pObj.infos And LogItemType.ServiceItem) = LogItemType.ServiceItem Then

                ' Get list
                Dim tmp As New Dictionary(Of String, serviceInfos)
                Dim pid(0) As Integer
                pid(0) = pObj.pid
                Native.Objects.Service.EnumerateServices(ServiceProvider.ServiceControlManaherHandle, tmp, False)

                ' Convert it into logItemInfos list
                For Each it As serviceInfos In tmp.Values
                    If it.ProcessId = pObj.pid Then
                        Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                        _dico.Add(logIt.Key, logIt)
                    End If
                Next
            End If



            ' Threads list
            If (pObj.infos And LogItemType.ThreadItem) = LogItemType.ThreadItem Then

                ' Get list
                Dim tmp As New Dictionary(Of String, threadInfos)
                Native.Objects.Thread.EnumerateThreadsByProcessId(tmp, pObj.pid)

                ' Convert it into logItemInfos list
                For Each it As threadInfos In tmp.Values
                    Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                    _dico.Add(logIt.Key, logIt)
                Next

            End If



            ' Windows list
            If (pObj.infos And LogItemType.WindowItem) = LogItemType.WindowItem Then

                ' Get list
                Dim tmp As New Dictionary(Of String, windowInfos)
                Native.Objects.Window.EnumerateWindowsByProcessId(pObj.pid, False, True, tmp, False)

                ' Convert it into logItemInfos list
                For Each it As windowInfos In tmp.Values
                    Dim logIt As New logItemInfos(it, logItemInfos.CreatedOrDeleted.Created)
                    _dico.Add(logIt.Key, logIt)
                Next

            End If

            Return _dico

        Finally
            _semLog.Release()
        End Try

    End Function

End Class
