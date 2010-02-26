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

Public Class WindowProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Current windows running (updated after each enumeration)
    Private Shared _currentWindows As New Dictionary(Of String, windowInfos)
    Friend Shared _semWindows As New Threading.Semaphore(1, 1)

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

    ' List of current windows
    Public Shared ReadOnly Property CurrentWindows() As Dictionary(Of String, windowInfos)
        Get
            Return _currentWindows
        End Get
    End Property
    Public Shared Sub SetCurrentWindows(ByVal value As Dictionary(Of String, windowInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, windowInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semWindows.WaitOne()

            ' Get deleted items
            For Each name As String In _currentWindows.Keys
                If Not (value.ContainsKey(name)) Then
                    _dicoDel.Add(name, _currentWindows(name))
                    _dicoDelSimp.Add(name)
                End If
            Next

            ' Get new items
            For Each name As String In value.Keys
                If Not (_currentWindows.ContainsKey(name)) Then
                    _dicoNew.Add(name)
                End If
            Next

            ' Re-assign dico
            _currentWindows = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semWindows.Release()
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
    Public Shared Event GotNewItems(ByVal news As List(Of String), ByVal newItems As Dictionary(Of String, windowInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal dels As Dictionary(Of String, windowInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal news As List(Of String), ByVal dels As List(Of String), ByVal Dico As Dictionary(Of String, windowInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public allInfos As Boolean
        Public instId As Integer
        Public Sub New(ByVal refreshAll As Boolean, ByVal instanceId As Integer)
            allInfos = refreshAll
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
            _semWindows.WaitOne()
            _currentWindows.Clear()
        Finally
            _semWindows.Release()
        End Try
    End Sub

    ' Get a service by its name
    ' Thread safe
    Public Shared Function GetWindowByHandle(ByVal handle As IntPtr) As cWindow

        Dim tt As cWindow = Nothing

        Try
            _semWindows.WaitOne()
            If _currentWindows IsNot Nothing Then
                If _currentWindows.ContainsKey(handle.ToString) Then
                    Try
                        tt = New cWindow(_currentWindows.Item(handle.ToString))
                    Catch ex As Exception

                    End Try
                End If
            End If
        Catch ex As Exception
            ' Item was removed just after ContainsKey... bad luck :-(
            Misc.ShowDebugError(ex)
        Finally
            _semWindows.Release()
        End Try

        Return tt

    End Function

    ' Refresh list of services depending on the connection NOW
    Public Shared Sub Update(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf WindowProvider.ProcessEnumeration), _
                New WindowProvider.asyncEnumPoolObj(forceAllInfos, instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
        ' This is of course async
        WindowProvider.ProcessEnumeration(New WindowProvider.asyncEnumPoolObj(forceAllInfos, instanceId))
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
                data.Order = cSocketData.OrderType.RequestWindowList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of windows !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, windowInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), windowInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        WindowProvider.SetCurrentWindows(_dico, instanceId)

    End Sub

    ' Enumeration of services
    Public Shared Sub ProcessEnumeration(ByVal thePoolObj As Object)

        Try
            ' Synchronisation
            _semProcessEnumeration.WaitOne()

            If Program.Connection.IsConnected Then

                Dim pObj As asyncEnumPoolObj = DirectCast(thePoolObj, asyncEnumPoolObj)
                Select Case Program.Connection.Type

                    Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                        ' Send cDat
                        Try
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestWindowList)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' Not avaialble

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot file

                        Dim snap As cSnapshot250 = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            ' Save service list into a dictionary
                            WindowProvider.SetCurrentWindows(snap.Windows, pObj.instId)
                        End If

                    Case Else
                        ' Local

                        ' Save service list into a dictionary
                        WindowProvider.SetCurrentWindows(SyncLocalEnum(pObj), pObj.instId)

                End Select
            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

    Private Shared Function SyncLocalEnum(ByVal pObj As asyncEnumPoolObj) As Dictionary(Of String, windowInfos)
        Dim _dico As New Dictionary(Of String, windowInfos)

        ' Enumeration
        Native.Objects.Window.EnumerateWindowsByProcessId(0, _
                                                          True, _
                                                          True, _
                                                          _dico, _
                                                          pObj.allInfos)
        Return _dico
    End Function

End Class
