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

Public Class EnvVariableProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Current processes running PID <-> (string <-> envVar)
    Private Shared _currentEnvVariables As New Dictionary(Of Integer, Dictionary(Of String, envVariableInfos))
    Friend Shared _semEnvVariables As New System.Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False

    ' Sempahore to protect async ProcessEnumeration
    Friend Shared _semProcessEnumeration As New System.Threading.Semaphore(1, 1)


    ' ========================================
    ' Public properties
    ' ========================================

    ' First refresh done ?
    Public Shared ReadOnly Property FirstRefreshDone() As Boolean
        Get
            Return _firstRefreshDone
        End Get
    End Property

    ' Clear list of env variables
    Public Shared Sub ClearList()
        Try
            _semEnvVariables.WaitOne()
            _currentEnvVariables.Clear()
        Finally
            _semEnvVariables.Release()
        End Try
    End Sub

    ' List of current processes
    Public Shared Property CurrentEnvVariables(ByVal pid As Integer) As Dictionary(Of String, envVariableInfos)
        Get
            Try
                _semEnvVariables.WaitOne()
                If _currentEnvVariables.ContainsKey(pid) Then
                    Return _currentEnvVariables(pid)
                Else
                    Return New Dictionary(Of String, envVariableInfos)
                End If
            Finally
                _semEnvVariables.Release()
            End Try
        End Get
        Friend Set(ByVal value As Dictionary(Of String, envVariableInfos))

            Dim _dicoDel As New Dictionary(Of String, envVariableInfos)
            Dim _dicoDelSimp As New List(Of String)
            Dim _dicoNew As New List(Of String)

            Try
                _semEnvVariables.WaitOne()

                ' Add a new entry
                If _currentEnvVariables.ContainsKey(pid) = False Then
                    _currentEnvVariables.Add(pid, New Dictionary(Of String, envVariableInfos))
                End If

                ' Get deleted items
                For Each vars As String In _currentEnvVariables(pid).Keys
                    If Not (value.ContainsKey(vars)) Then
                        _dicoDel.Add(vars, _currentEnvVariables(pid)(vars))
                        _dicoDelSimp.Add(vars)
                    End If
                Next

                ' Get new items
                For Each vars As String In value.Keys
                    If Not (_currentEnvVariables(pid).ContainsKey(vars)) Then
                        _dicoNew.Add(vars)
                    End If
                Next

                ' Re-assign dico
                _currentEnvVariables(pid) = value

            Catch ex As Exception
                Misc.ShowDebugError(ex)
            Finally
                _semEnvVariables.Release()
            End Try

            RaiseEvent GotDeletedItems(_dicoDel)
            RaiseEvent GotNewItems(_dicoNew, value)
            RaiseEvent GotRefreshed(_dicoNew, _dicoDelSimp, value)

            _firstRefreshDone = True

        End Set
    End Property


    ' ========================================
    ' Other public
    ' ========================================

    ' Shared events
    Public Shared Event GotNewItems(ByVal keys As List(Of String), ByVal newItems As Dictionary(Of String, envVariableInfos))
    Public Shared Event GotDeletedItems(ByVal keys As Dictionary(Of String, envVariableInfos))
    Public Shared Event GotRefreshed(ByVal newItems As List(Of String), ByVal delItems As List(Of String), ByVal Dico As Dictionary(Of String, envVariableInfos))

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public pid As Integer
        Public peb As IntPtr
        Public Sub New(ByVal procId As Integer, ByVal pebA As IntPtr)
            pid = procId
            peb = pebA
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
    Public Shared Sub Update(ByVal pid As Integer, ByVal peb As IntPtr)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf EnvVariableProvider.ProcessEnumeration), _
                New EnvVariableProvider.asyncEnumPoolObj(pid, peb))
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
                data.Order = cSocketData.OrderType.RequestEnvironmentVariableList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys)
            End If

        End If

    End Sub

    ' When socket got a list of processes !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim _dico As New Dictionary(Of String, envVariableInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), envVariableInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        EnvVariableProvider.CurrentEnvVariables(0) = _dico  'TODO (have to retrieve pid)

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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestEnvironmentVariableList, pObj.pid)
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' Not available

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of String, envVariableInfos)
                        Dim snap As cSnapshot = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.EnvironnementVariablesByProcessId(pObj.pid)
                        End If

                        ' Save current processes into a dictionary
                        EnvVariableProvider.CurrentEnvVariables(pObj.pid) = _dico

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of String, envVariableInfos)

                        _dico = SharedLocalSyncEnumerate(pObj)

                        ' Save current processes into a dictionary
                        EnvVariableProvider.CurrentEnvVariables(pObj.pid) = _dico

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub


    ' Shared, local and sync enumeration
    Private Shared Function SharedLocalSyncEnumerate(ByVal pObj As asyncEnumPoolObj) As Dictionary(Of String, envVariableInfos)
        Dim _dico As New Dictionary(Of String, envVariableInfos)

        Dim var() As String = Nothing
        Dim val() As String = Nothing

        ' Get the env variables
        Native.Objects.EnvVariable.GetEnvironmentVariables(pObj.pid, pObj.peb, var, val)

        For x As Integer = 0 To var.Length - 1
            If _dico.ContainsKey(var(x)) = False Then
                _dico.Add(var(x), New envVariableInfos(var(x), val(x), pObj.pid))
            End If
        Next

        Return _dico
    End Function

End Class
