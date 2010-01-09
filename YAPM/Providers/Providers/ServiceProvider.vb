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

Public Class ServiceProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' For WMI connection
    Friend Shared wmiSearcher As Management.ManagementObjectSearcher

    ' Store new services names
    Friend Shared _dicoNewServices As New List(Of String)
    Friend Shared _semNewServices As New Threading.Semaphore(1, 1)

    ' Current services running (updated after each enumeration)
    Private Shared _currentServices As New Dictionary(Of String, serviceInfos)
    Friend Shared _semServices As New Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False

    ' Handle to service manager
    Private Shared _hSCM As IntPtr = IntPtr.Zero

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

    ' List of current processes
    Public Shared ReadOnly Property CurrentServices() As Dictionary(Of String, serviceInfos)
        Get
            Return _currentServices
        End Get
    End Property
    Public Shared Sub SetCurrentServices(ByVal value As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, serviceInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Try
            _semServices.WaitOne()

            ' Get deleted items
            For Each name As String In _currentServices.Keys
                If Not (value.ContainsKey(name)) Then
                    _dicoDel.Add(name, _currentServices(name))  ' name <-> service
                    _dicoDelSimp.Add(name)                      ' only name
                    ' Will be a 'new service' next time
                    RemoveServiceFromListOfNewServices(name)
                End If
            Next

            ' Get new items
            For Each name As String In value.Keys
                If Not (_currentServices.ContainsKey(name)) Then
                    _dicoNew.Add(name)
                End If
            Next

            ' Re-assign dico
            _currentServices = value

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semServices.Release()
        End Try

        RaiseEvent GotDeletedItems(_dicoDel, instanceId)
        RaiseEvent GotNewItems(_dicoNew, value, instanceId)
        RaiseEvent GotRefreshed(_dicoNew, _dicoDelSimp, value, instanceId)

        _firstRefreshDone = True
    End Sub

    ' Handle to service control manager
    Public Shared ReadOnly Property ServiceControlManaherHandle() As IntPtr
        Get
            Return _hSCM
        End Get
    End Property


    ' ========================================
    ' Other public
    ' ========================================

    ' Shared events
    Public Shared Event GotNewItems(ByVal names As List(Of String), ByVal newItems As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer)
    Public Shared Event GotDeletedItems(ByVal names As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer)
    Public Shared Event GotRefreshed(ByVal newServices As List(Of String), ByVal delServices As List(Of String), ByVal Dico As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public complete As Boolean
        Public instId As Integer
        Public Sub New(ByVal comp As Boolean, ByVal instanceId As Integer)
            complete = comp
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

    ' Get a service by its name
    ' Thread safe
    Public Shared Function GetServiceByName(ByVal name As String) As cService

        Dim tt As cService = Nothing

        Try
            _semServices.WaitOne()
            If _currentServices IsNot Nothing Then
                If _currentServices.ContainsKey(name) Then
                    Try
                        tt = New cService(_currentServices.Item(name))
                    Catch ex As Exception

                    End Try
                End If
            End If
        Catch ex As Exception
            ' Item was removed just after ContainsKey... bad luck :-(
            Misc.ShowDebugError(ex)
        Finally
            _semServices.Release()
        End Try

        Return tt

    End Function

    ' Remove some services from new service list
    ' Remove a service from new services list
    Public Shared Sub RemoveServiceFromListOfNewServices(ByVal name As String)
        Try
            _semNewServices.WaitOne()
            If _dicoNewServices.Contains(name) Then
                _dicoNewServices.Remove(name)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semNewServices.Release()
        End Try
    End Sub

    ' Clear list of new services
    Public Shared Sub ClearNewServicesList()
        Try
            _semNewServices.WaitOne()
            _dicoNewServices.Clear()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semNewServices.Release()
        End Try
    End Sub

    ' Get services which depends from a specific service
    Public Shared Function GetServiceWhichDependFromByServiceName(ByVal serviceName As String) As Dictionary(Of String, serviceInfos)

        Dim _d As New Dictionary(Of String, serviceInfos)
        Dim dep() As String = Nothing

        Try
            _semNewServices.WaitOne()

            For Each serv As serviceInfos In _currentServices.Values
                dep = serv.Dependencies
                If dep IsNot Nothing Then
                    For Each s As String In dep
                        If s.ToLowerInvariant = serviceName.ToLowerInvariant Then
                            _d.Add(serv.Name, serv)
                            Exit For
                        End If
                    Next
                End If
            Next

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semNewServices.Release()
        End Try

        Return _d
    End Function

    ' Get dependencies of a service
    Public Shared Function GetServiceDependencies(ByVal serviceName As String) As Dictionary(Of String, serviceInfos)

        Dim _d As New Dictionary(Of String, serviceInfos)
        Dim dep() As String = Nothing

        Try
            _semNewServices.WaitOne()

            For Each serv As serviceInfos In _currentServices.Values
                If serv.Name.ToLowerInvariant = serviceName.ToLowerInvariant Then
                    dep = serv.Dependencies
                    Exit For
                End If
            Next

            If dep Is Nothing OrElse dep.Length = 0 Then
                Return _d
            End If

            For Each servName As String In dep
                For Each serv As serviceInfos In _currentServices.Values
                    If servName.ToLowerInvariant = serv.Name.ToLowerInvariant Then
                        _d.Add(servName, serv)
                        Exit For
                    End If
                Next
            Next

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semNewServices.Release()
        End Try

        Return _d
    End Function

    ' Refresh list of services depending on the connection NOW
    Public Shared Sub Update(ByVal forceAllInfos As Boolean, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf ServiceProvider.ProcessEnumeration), _
                New ServiceProvider.asyncEnumPoolObj(forceAllInfos, instanceId))
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
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Service")
                    wmiSearcher.Scope = New Management.ManagementScope("\\" & Program.Connection.WmiParameters.serverName & "\root\cimv2", __con)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Nothing special here

            Case cConnection.TypeOfConnection.LocalConnection
                ' Get handle to service control manager
                If _hSCM.IsNotNull Then
                    Native.Objects.Service.CloseSCManagerHandle(_hSCM)
                End If
                _hSCM = Native.Objects.Service.GetSCManagerHandle(Native.Security.ServiceManagerAccess.EnumerateService)

        End Select

    End Sub

    ' Called when disconnected
    Private Sub eventDisConnected()
        ' Close handle to service control manager (if necessary)
        If _hSCM.IsNotNull Then
            Native.Objects.Service.CloseSCManagerHandle(_hSCM)
        End If
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
                data.Order = cSocketData.OrderType.RequestServiceList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of processes !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, serviceInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), serviceInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        ServiceProvider.SetCurrentServices(_dico, instanceId)

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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestServiceList)
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                        ' Save current collection
                        Dim _dico As New Dictionary(Of String, serviceInfos)
                        Dim res As Boolean
                        Dim msg As String = ""

                        res = Wmi.Objects.Service.EnumerateServices(wmiSearcher, _dico, msg)

                        ' Save service list into a dictionary
                        ServiceProvider.SetCurrentServices(_dico, pObj.instId)

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot file

                        Dim snap As cSnapshot = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            ' Save service list into a dictionary
                            ServiceProvider.SetCurrentServices(snap.Services, pObj.instId)
                        End If

                    Case Else
                        ' Local

                        Dim _dico As New Dictionary(Of String, serviceInfos)

                        Native.Objects.Service.EnumerateServices(ServiceProvider.ServiceControlManaherHandle, _dico, pObj.complete)

                        ' Save service list into a dictionary
                        ServiceProvider.SetCurrentServices(_dico, pObj.instId)

                End Select
            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

End Class
