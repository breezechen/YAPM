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

Public Class JobProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' For WMI connection
    Friend Shared wmiSearcher As Management.ManagementObjectSearcher

    ' Current processes running
    Private Shared _currentJobs As New Dictionary(Of String, jobInfos)
    Friend Shared _semJobs As New System.Threading.Semaphore(1, 1)

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

    ' List of current processes
    Public Shared ReadOnly Property CurrentJobs() As Dictionary(Of String, jobInfos)
        Get
            Return _currentJobs
        End Get
    End Property
    Public Shared Sub SetCurrentJobs(ByVal value As Dictionary(Of String, jobInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, jobInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semJobs.WaitOne()

            ' Get deleted items
            For Each pid As String In _currentJobs.Keys
                If Not (value.ContainsKey(pid)) Then
                    _dicoDel.Add(pid, _currentJobs(pid))
                    _dicoDelSimp.Add(pid)
                End If
            Next

            ' Get new items
            For Each pid As String In value.Keys
                If Not (_currentJobs.ContainsKey(pid)) Then
                    _dicoNew.Add(pid)
                End If
            Next

            ' Re-assign dico
            _currentJobs = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semJobs.Release()
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
    Public Shared Event GotNewItems(ByVal news As List(Of String), ByVal newItems As Dictionary(Of String, jobInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal dels As Dictionary(Of String, jobInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal newPids As List(Of String), ByVal dels As List(Of String), ByVal Dico As Dictionary(Of String, jobInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public force As Boolean
        Public instId As Integer
        Public Sub New(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
            force = forceAllInfos
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

    ' Clear list of current elements
    Public Shared Sub ClearList()
        Try
            _semJobs.WaitOne()
            _currentJobs.Clear()
        Finally
            _semJobs.Release()
        End Try
    End Sub

    ' Get a job by its name
    ' Thread safe
    Public Shared Function GetJobByName(ByVal name As String) As cJob

        Dim tt As cJob = Nothing

        Try
            _semJobs.WaitOne()
            If _currentJobs IsNot Nothing Then
                If _currentJobs.ContainsKey(name) Then
                    Try
                        tt = New cJob(_currentJobs.Item(name))
                    Catch ex As Exception

                    End Try
                End If
            End If
        Catch ex As Exception
            ' Item was removed just after ContainsKey... bad luck :-(
            Misc.ShowDebugError(ex)
        Finally
            _semJobs.Release()
        End Try

        Return tt

    End Function

    ' Refresh list of processes depending on the connection NOW
    Public Shared Sub Update(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf JobProvider.ProcessEnumeration), _
                New JobProvider.asyncEnumPoolObj(forceAllInfos, instanceId))
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
                ' Have to connect some things when using WMI

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.Password = Common.Misc.SecureStringToCharArray(Program.Connection.WmiParameters.password)
                __con.Username = Program.Connection.WmiParameters.userName

                Try
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_NamedJobObject")
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
                data.Order = cSocketData.OrderType.RequestJobList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of jobs !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, jobInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), jobInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        JobProvider.SetCurrentJobs(_dico, instanceId)

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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestJobList)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        Dim _dico As New Dictionary(Of String, jobInfos)
                        Dim msg As String = ""
                        Dim res As Boolean = _
                            Wmi.Objects.Job.EnumerateJobs(wmiSearcher, _dico, msg)

                        ' Save current processes into a dictionary
                        JobProvider.SetCurrentJobs(_dico, pObj.instId)

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of String, jobInfos)
                        Dim snap As cSnapshot = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.Jobs
                        End If

                        ' Save current processes into a dictionary
                        JobProvider.SetCurrentJobs(_dico, pObj.instId)

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of String, jobInfos)

                        ' Enumeration
                        _dico = Native.Objects.Job.EnumerateJobs

                        ' Save current processes into a dictionary
                        JobProvider.SetCurrentJobs(_dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

End Class
