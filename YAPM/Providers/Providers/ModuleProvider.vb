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

Public Class ModuleProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' For WMI connection
    Friend Shared wmiSearcher As Management.ManagementObjectSearcher

    ' Current processes running PID <-> (string <-> modules)
    Private Shared _currentModules As New Dictionary(Of Integer, Dictionary(Of String, moduleInfos))
    Friend Shared _semModules As New System.Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False

    ' Sempahore to protect async ProcessEnumeration
    Friend Shared _semProcessEnumeration As New System.Threading.Semaphore(1, 1)

    ' Store module fileinfo (with semaphore)
    Friend Shared fileInformations As New Dictionary(Of String, SerializableFileVersionInfo)
    Friend Shared semDicoFileInfos As New System.Threading.Semaphore(1, 1)


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
            _semModules.WaitOne()
            _currentModules.Clear()
        Finally
            _semModules.Release()
        End Try
    End Sub

    ' Clear list for a specific processID
    Public Shared Sub ClearListForAnId(ByVal pid As Integer)
        Try
            _semModules.WaitOne()
            If _currentModules.ContainsKey(pid) Then
                _currentModules(pid).Clear()
            End If
        Finally
            _semModules.Release()
        End Try
    End Sub

    ' List of current processes
    Public Shared ReadOnly Property CurrentModules(ByVal pid As Integer) As Dictionary(Of String, moduleInfos)
        Get
            Try
                _semModules.WaitOne()
                If _currentModules.ContainsKey(pid) Then
                    Return _currentModules(pid)
                Else
                    Return New Dictionary(Of String, moduleInfos)
                End If
            Finally
                _semModules.Release()
            End Try
        End Get
    End Property

    Public Shared Sub SetCurrentModules(ByVal pid As Integer, ByVal value As Dictionary(Of String, moduleInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, moduleInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semModules.WaitOne()

            ' Add a new entry
            If _currentModules.ContainsKey(pid) = False Then
                _currentModules.Add(pid, New Dictionary(Of String, moduleInfos))
            End If

            ' Get deleted items
            For Each vars As String In _currentModules(pid).Keys
                If Not (value.ContainsKey(vars)) Then
                    _dicoDel.Add(vars, _currentModules(pid)(vars))
                    _dicoDelSimp.Add(vars)
                End If
            Next

            ' Get new items
            For Each vars As String In value.Keys
                If Not (_currentModules(pid).ContainsKey(vars)) Then
                    _dicoNew.Add(vars)
                End If
            Next

            ' Re-assign dico
            _currentModules(pid) = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semModules.Release()
        End Try

        ' Raise events
        RaiseEvent GotDeletedItems(_dicoDel, instanceId, res)
        RaiseEvent GotNewItems(_dicoNew, value, instanceId, res)
        RaiseEvent GotRefreshed(_dicoNew, _dicoDelSimp, value, instanceId, res)
        _firstRefreshDone = True

    End Sub


    ' ========================================
    ' Other public
    ' ========================================

    ' Shared events
    Public Shared Event GotNewItems(ByVal keys As List(Of String), ByVal newItems As Dictionary(Of String, moduleInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal keys As Dictionary(Of String, moduleInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal newItems As List(Of String), ByVal delItems As List(Of String), ByVal Dico As Dictionary(Of String, moduleInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public pid As Integer
        Public instId As Integer
        Public Sub New(ByVal procId As Integer, ByVal instanceId As Integer)
            pid = procId
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
    Public Shared Sub Update(ByVal pid As Integer, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf ModuleProvider.ProcessEnumeration), _
                New ModuleProvider.asyncEnumPoolObj(pid, instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal pid As Integer, ByVal instanceId As Integer)
        ' This is of course sync
        ModuleProvider.ProcessEnumeration(New ModuleProvider.asyncEnumPoolObj(pid, instanceId))
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

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.Password = Common.Misc.SecureStringToCharArray(Program.Connection.WmiParameters.password)
                __con.Username = Program.Connection.WmiParameters.userName

                Try
                    'TOCHANGE
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
                data.Order = cSocketData.OrderType.RequestModuleList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of modules !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, moduleInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), moduleInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary.
        ' Have to get the processId of the current list of processes, as there might
        ' be module enumeration for more than one process.
        ' So we retrieve the informations by enumerating the variables and getting
        ' the first PID
        Dim pid As Integer
        For Each it As moduleInfos In _dico.Values
            pid = it.ProcessId
            Exit For
        Next
        ModuleProvider.SetCurrentModules(pid, _dico, instanceId)

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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestModuleList, pObj.pid)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' WMI
                        Dim _dico As New Dictionary(Of String, moduleInfos)
                        Dim msg As String = ""
                        Dim res As Boolean = _
                                Wmi.Objects.Module.EnumerateModuleById(pObj.pid, _
                                                                        wmiSearcher, _
                                                                        _dico, _
                                                                        msg)
                        ' Save current processes into a dictionary
                        ModuleProvider.SetCurrentModules(pObj.pid, _dico, pObj.instId)

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of String, moduleInfos)
                        Dim snap As cSnapshot = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.ModulesByProcessId(pObj.pid)
                        End If

                        ' Save current processes into a dictionary
                        ModuleProvider.SetCurrentModules(pObj.pid, _dico, pObj.instId)

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of String, moduleInfos)

                        ' Enumeration
                        _dico = SharedLocalSyncEnumerate(pObj)

                        ' Save current processes into a dictionary
                        ModuleProvider.SetCurrentModules(pObj.pid, _dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As asyncEnumPoolObj) As Dictionary(Of String, moduleInfos)
        Dim _dico As Dictionary(Of String, moduleInfos)

        ' If it's a Wow64 process, module enumeration is made using debug functions
        Dim cProc As cProcess = ProcessProvider.GetProcessById(pObj.pid)
        If cProc IsNot Nothing AndAlso cProc.IsWow64Process Then
            _dico = Native.Objects.Module.EnumerateModulesWow64ByProcessId(pObj.pid, False)
        Else
            ' Normal native enumeration
            _dico = Native.Objects.Module.EnumerateModulesByProcessId(pObj.pid, False)
        End If

        Return _dico
    End Function

End Class
