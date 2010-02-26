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

Public Class PrivilegeProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Current processes running PID <-> (string <-> privileges)
    Private Shared _currentPrivileges As New Dictionary(Of Integer, Dictionary(Of String, privilegeInfos))
    Friend Shared _semPrivileges As New System.Threading.Semaphore(1, 1)

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
            _semPrivileges.WaitOne()
            _currentPrivileges.Clear()
        Finally
            _semPrivileges.Release()
        End Try
    End Sub

    ' Clear list for a specific processID
    Public Shared Sub ClearListForAnId(ByVal pid As Integer)
        Try
            _semPrivileges.WaitOne()
            If _currentPrivileges.ContainsKey(pid) Then
                _currentPrivileges(pid).Clear()
            End If
        Finally
            _semPrivileges.Release()
        End Try
    End Sub

    ' List of current processes
    Public Shared ReadOnly Property CurrentPrivileges(ByVal pid As Integer) As Dictionary(Of String, privilegeInfos)
        Get
            Try
                _semPrivileges.WaitOne()
                If _currentPrivileges.ContainsKey(pid) Then
                    Return _currentPrivileges(pid)
                Else
                    Return New Dictionary(Of String, privilegeInfos)
                End If
            Finally
                _semPrivileges.Release()
            End Try
        End Get
    End Property

    Public Shared Sub SetCurrentPrivileges(ByVal pid As Integer, ByVal value As Dictionary(Of String, privilegeInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, privilegeInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semPrivileges.WaitOne()

            ' Add a new entry
            If _currentPrivileges.ContainsKey(pid) = False Then
                _currentPrivileges.Add(pid, New Dictionary(Of String, privilegeInfos))
            End If

            ' Get deleted items
            For Each vars As String In _currentPrivileges(pid).Keys
                If Not (value.ContainsKey(vars)) Then
                    _dicoDel.Add(vars, _currentPrivileges(pid)(vars))
                    _dicoDelSimp.Add(vars)
                End If
            Next

            ' Get new items
            For Each vars As String In value.Keys
                If Not (_currentPrivileges(pid).ContainsKey(vars)) Then
                    _dicoNew.Add(vars)
                End If
            Next

            ' Re-assign dico
            _currentPrivileges(pid) = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semPrivileges.Release()
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
    Public Shared Event GotNewItems(ByVal keys As List(Of String), ByVal newItems As Dictionary(Of String, privilegeInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal keys As Dictionary(Of String, privilegeInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal newItems As List(Of String), ByVal delItems As List(Of String), ByVal Dico As Dictionary(Of String, privilegeInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

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
                New System.Threading.WaitCallback(AddressOf PrivilegeProvider.ProcessEnumeration), _
                New PrivilegeProvider.asyncEnumPoolObj(pid, instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal pid As Integer, ByVal instanceId As Integer)
        ' This is of course sync
        PrivilegeProvider.ProcessEnumeration(New PrivilegeProvider.asyncEnumPoolObj(pid, instanceId))
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
                data.Order = cSocketData.OrderType.RequestPrivilegesList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of privileges !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, privilegeInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), privilegeInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary.
        ' Have to get the processId of the current list of processes, as there might
        ' be heap enumeration for more than one process.
        ' So we retrieve the informations by enumerating the variables and getting
        ' the first PID
        Dim pid As Integer
        For Each it As privilegeInfos In _dico.Values
            pid = it.ProcessId
            Exit For
        Next
        PrivilegeProvider.SetCurrentPrivileges(pid, _dico, instanceId)

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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestPrivilegesList, pObj.pid)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' Not available

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of String, privilegeInfos)
                        Dim snap As cSnapshot250 = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.PrivilegesByProcessId(pObj.pid)
                        End If

                        ' Save current processes into a dictionary
                        PrivilegeProvider.SetCurrentPrivileges(pObj.pid, _dico, pObj.instId)

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of String, privilegeInfos)

                        _dico = SharedLocalSyncEnumerate(pObj)

                        ' Save current processes into a dictionary
                        PrivilegeProvider.SetCurrentPrivileges(pObj.pid, _dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

    ' Shared, local and sync enumeration
    Private Shared Function SharedLocalSyncEnumerate(ByVal pObj As asyncEnumPoolObj) As Dictionary(Of String, privilegeInfos)
        Dim _dico As New Dictionary(Of String, privilegeInfos)

        Dim ret As Native.Api.NativeStructs.PrivilegeInfo() = _
            Native.Objects.Token.GetPrivilegesListByProcessId(pObj.pid)

        For Each tmp As Native.Api.NativeStructs.PrivilegeInfo In ret
            _dico.Add(tmp.Name, New privilegeInfos(tmp.Name, pObj.pid, tmp.Status))
        Next
        Return _dico
    End Function

End Class
