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

Public Class ServiceProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Store new services names
    Friend Shared _dicoNewServices As New List(Of String)
    Friend Shared _semNewServices As New Threading.Semaphore(1, 1)

    ' Current services running (updated after each enumeration)
    Private Shared _currentServices As New Dictionary(Of String, serviceInfos)
    Private Shared _semServices As New Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False


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
    Public Shared Property CurrentServices() As Dictionary(Of String, serviceInfos)
        Get
            Return _currentServices
        End Get
        Friend Set(ByVal value As Dictionary(Of String, serviceInfos))

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
    Public Shared Event GotNewItems(ByVal names As List(Of String), ByVal newItems As Dictionary(Of String, serviceInfos))
    Public Shared Event GotDeletedItems(ByVal names As Dictionary(Of String, serviceInfos))
    Public Shared Event GotRefreshed(ByVal newServices As List(Of String), ByVal delServices As List(Of String), ByVal Dico As Dictionary(Of String, serviceInfos))


    ' ========================================
    ' Public functions
    ' ========================================


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



    ' ========================================
    ' Private functions
    ' ========================================


End Class
