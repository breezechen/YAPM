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

Imports System.Management

Public Class ProcessProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' For WMI connection
    Friend Shared wmiSearcher As Management.ManagementObjectSearcher

    ' For processor count
    Private Shared _processors As Integer = 1

    ' Min rights
    Private Shared _processMinRights As Native.Security.ProcessAccess = Native.Security.ProcessAccess.QueryInformation

    ' Current processes running
    Private Shared _currentProcesses As New Dictionary(Of Integer, processInfos)
    Friend Shared _semProcess As New System.Threading.Semaphore(1, 1)

    ' New processes
    Friend Shared _dicoNewProcesses As New List(Of Integer)
    Friend Shared _semNewProceses As New System.Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False

    ' Sempahore to protect async ProcessEnumeration
    Friend Shared _semProcessEnumeration As New System.Threading.Semaphore(1, 1)

    ' Sempahore to protect async reanalize
    Friend Shared _semReanalize As New System.Threading.Semaphore(1, 1)


    ' ========================================
    ' Public properties
    ' ========================================

    ' Min rights
    Public Shared ReadOnly Property ProcessMinRights() As Native.Security.ProcessAccess
        Get
            Return _processMinRights
        End Get
    End Property

    ' First refresh done ?
    Public Shared Property FirstRefreshDone() As Boolean
        Get
            Return _firstRefreshDone
        End Get
        Friend Set(ByVal value As Boolean)
            _firstRefreshDone = value
        End Set
    End Property

    ' List of current processes
    Public Shared ReadOnly Property CurrentProcesses() As Dictionary(Of Integer, processInfos)
        Get
            Return _currentProcesses
        End Get
    End Property
    Public Shared Sub SetCurrentProcesses(ByVal value As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of Integer, processInfos)
        Dim _dicoDelSimp As New List(Of Integer)
        Dim _dicoNew As New List(Of Integer)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semProcess.WaitOne()

            ' Get deleted items
            For Each pid As Integer In _currentProcesses.Keys
                If Not (value.ContainsKey(pid)) Then
                    _dicoDel.Add(pid, _currentProcesses(pid))   ' pid <-> process
                    _dicoDelSimp.Add(pid)                       ' only pid
                    ' Will be a 'new process' next time
                    RemoveProcesseFromListOfNewProcesses(pid)
                End If
            Next

            ' Get new items
            For Each pid As Integer In value.Keys
                If Not (_currentProcesses.ContainsKey(pid)) Then
                    _dicoNew.Add(pid)
                End If
            Next

            ' Re-assign dico
            _currentProcesses = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semProcess.Release()
        End Try

        ' Raise events
        RaiseEvent GotDeletedItems(_dicoDel, instanceId, res)
        RaiseEvent GotNewItems(_dicoNew, value, instanceId, res)
        RaiseEvent GotRefreshed(_dicoNew, _dicoDelSimp, value, instanceId, res)
        _firstRefreshDone = True

    End Sub

    ' Number of processors
    Public Shared ReadOnly Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
    End Property


    ' ========================================
    ' Other public
    ' ========================================

    ' Shared events
    Public Shared Event GotNewItems(ByVal pids As List(Of Integer), ByVal newItems As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal pids As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal newPids As List(Of Integer), ByVal delPids As List(Of Integer), ByVal Dico As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public force As Boolean
        Public instId As Integer
        Public Sub New(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
            force = forceAllInfos
            instId = instanceId
        End Sub
    End Structure

    ' Structure use for process reanalize operation
    Public Structure ReanalizeProcessObj
        Public pid() As Integer
        Public Sub New(ByRef pi() As Integer)
            pid = pi
        End Sub
    End Structure



    ' ========================================
    ' Public functions
    ' ========================================

    ' Constructor
    Public Sub New()
        ' Minrights
        If cEnvironment.SupportsMinRights Then
            _processMinRights = Native.Security.ProcessAccess.QueryLimitedInformation
        End If
        ' Add handler for general connection/deconnection
        AddHandler Program.Connection.Connected, AddressOf eventConnected
        AddHandler Program.Connection.Disconnected, AddressOf eventDisConnected
        AddHandler Program.Connection.Socket.ReceivedData, AddressOf eventSockReceiveData
    End Sub

    ' Clear list of current elements
    Public Shared Sub ClearList()
        Try
            _semProcess.WaitOne()
            _currentProcesses.Clear()
        Finally
            _semProcess.Release()
        End Try
    End Sub

    ' Get a process by its id
    ' Thread safe
    Public Shared Function GetProcessById(ByVal id As Integer) As cProcess

        Dim tt As cProcess = Nothing

        Try
            _semProcess.WaitOne()
            If _currentProcesses IsNot Nothing Then
                If _currentProcesses.ContainsKey(id) Then
                    Try
                        tt = New cProcess(_currentProcesses.Item(id))
                    Catch ex As Exception

                    End Try
                End If
            End If
        Catch ex As Exception
            ' Item was removed just after ContainsKey... bad luck :-(
            Misc.ShowDebugError(ex)
        Finally
            _semProcess.Release()
        End Try

        Return tt

    End Function
    ' No sync protection
    Friend Shared Function GetProcessByIdNonThreadSafe(ByVal id As Integer) As cProcess

        Dim tt As cProcess = Nothing

        Try
            If _currentProcesses IsNot Nothing Then
                If _currentProcesses.ContainsKey(id) Then
                    Try
                        tt = New cProcess(_currentProcesses.Item(id))
                    Catch ex As Exception

                    End Try
                End If
            End If
        Catch ex As Exception
            ' Item was removed just after ContainsKey... bad luck :-(
            Misc.ShowDebugError(ex)
        End Try

        Return tt

    End Function

    ' Remove some PIDs from new processes list
    Public Shared Sub RemoveProcessesFromListOfNewProcesses(ByVal pid() As Integer)
        If pid IsNot Nothing Then
            For Each id As Integer In pid
                Try
                    _semNewProceses.WaitOne()
                    If _dicoNewProcesses.Contains(id) Then
                        _dicoNewProcesses.Remove(id)
                    End If
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                Finally
                    _semNewProceses.Release()
                End Try
            Next
        End If
    End Sub
    Public Shared Sub RemoveProcesseFromListOfNewProcesses(ByVal pid As Integer)
        Try
            _semNewProceses.WaitOne()
            If _dicoNewProcesses.Contains(pid) Then
                _dicoNewProcesses.Remove(pid)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semNewProceses.Release()
        End Try
    End Sub

    ' Reanalize processes
    Public Shared Sub ProcessReanalize(ByVal thePoolObj As Object)

        Try
            _semReanalize.WaitOne()

            If Program.Connection.IsConnected Then
                Dim pObj As ReanalizeProcessObj = DirectCast(thePoolObj,  _
                                    ReanalizeProcessObj)

                Select Case Program.Connection.Type
                    Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                        Try
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessReanalize, pObj.pid)
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.LocalConnection, cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        RemoveProcessesFromListOfNewProcesses(pObj.pid)

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Nothing to do

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semReanalize.Release()
        End Try
    End Sub

    ' Clear new process dico
    Public Shared Sub ClearNewProcessesDico()
        Try
            _semNewProceses.WaitOne()
            _dicoNewProcesses.Clear()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semNewProceses.Release()
        End Try

        _firstRefreshDone = False
    End Sub

    ' Refresh list of processes depending on the connection NOW
    Public Shared Sub Update(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf ProcessProvider.ProcessEnumeration), _
                New ProcessProvider.asyncEnumPoolObj(forceAllInfos, instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
        ' This is of course sync
        ProcessProvider.ProcessEnumeration(New ProcessProvider.asyncEnumPoolObj(forceAllInfos, instanceId))
    End Sub


    ' ========================================
    ' Private functions
    ' ========================================

    ' Called when connected
    Private Sub eventConnected()
        _processors = 0         ' Reinit processor count

        ' Connect
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' Nothing special here

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' Have to connect some things when using WMI

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.Password = Common.Misc.SecureStringToCharArray(Program.Connection.WmiParameters.password)
                __con.Username = Program.Connection.WmiParameters.userName

                Try
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Process")
                    wmiSearcher.Scope = New Management.ManagementScope("\\" & Program.Connection.WmiParameters.serverName & "\root\cimv2", __con)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Nothing special here

            Case cConnection.TypeOfConnection.LocalConnection
                ' Nothing special here
        End Select


        ' Get processor count
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' We will try to retrieve processor count each time we GET data
                ' from server (if procCount is still 0), because if we do it
                ' HERE, the connection is not well initialized at this point
                ' (i.e. _idToSend has not been sent by the server)

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim objSearcherSystem As ManagementObjectSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor")
                    objSearcherSystem.Scope = wmiSearcher.Scope
                    Dim _count As Integer = 0
                    For Each res As Management.ManagementObject In objSearcherSystem.Get
                        _count += 1
                    Next
                    _processors = _count
                Catch ex As Exception
                    Misc.ShowError(ex, "Could not get informations about the remote system")
                    _processors = 1
                End Try

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Nothing special here

            Case cConnection.TypeOfConnection.LocalConnection
                ' Local
                _processors = cSystemInfo.GetProcessorCount
        End Select

    End Sub

    ' Called when disconnected
    Private Sub eventDisConnected()
        ' Reinit processor count
        _processors = 0
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

            If data.Type = cSocketData.DataType.Order AndAlso _
                data.Order = cSocketData.OrderType.ReturnProcessorCount Then
                _processors = CInt(data.Param1)
            End If

            If _processors = 0 Then
                ' Send the request again
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestProcessorCount)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Could not send request to server")
                End Try
            End If

            If data.Type = cSocketData.DataType.RequestedList AndAlso _
                data.Order = cSocketData.OrderType.RequestProcessList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of processes !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of Integer, processInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(Integer.Parse(keys(x))) = False Then
                    _dico.Add(Integer.Parse(keys(x)), DirectCast(lst(x), processInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        ProcessProvider.SetCurrentProcesses(_dico, instanceId)

    End Sub

    ' Enumeration of processes
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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestProcessList)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        Dim _dico As New Dictionary(Of Integer, processInfos)
                        Dim msg As String = ""
                        Dim res As Boolean = _
                            Wmi.Objects.Process.EnumerateProcesses(wmiSearcher, _dico, msg)

                        ' Save current processes into a dictionary
                        ProcessProvider.SetCurrentProcesses(_dico, pObj.instId)

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of Integer, processInfos)
                        Dim snap As cSnapshot250 = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.Processes
                        End If

                        ' Save current processes into a dictionary
                        ProcessProvider.SetCurrentProcesses(_dico, pObj.instId)

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of Integer, processInfos)

                        'Select Case pObj.method
                        '    Case ProcessEnumMethode.BruteForce
                        '        _dico = Native.Objects.Process.EnumerateHiddenProcessesBruteForce
                        '    Case ProcessEnumMethode.HandleMethod
                        '        _dico = Native.Objects.Process.EnumerateHiddenProcessesHandleMethod
                        '    Case Else
                        _dico = Native.Objects.Process.EnumerateVisibleProcesses(pObj.force)
                        'End Select

                        ' Save current processes into a dictionary
                        ProcessProvider.SetCurrentProcesses(_dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

End Class
