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

Public Class ProcessProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Current processes running
    Private Shared _currentProcesses As New Dictionary(Of Integer, processInfos)
    Friend Shared _semProcess As New System.Threading.Semaphore(1, 1)

    ' New processes
    Friend Shared _dicoNewProcesses As New List(Of Integer)
    Friend Shared _semNewProceses As New System.Threading.Semaphore(1, 1)

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
    Public Shared Property CurrentProcesses() As Dictionary(Of Integer, processInfos)
        Get
            Return _currentProcesses
        End Get
        Friend Set(ByVal value As Dictionary(Of Integer, processInfos))

            Dim _dicoDel As New Dictionary(Of Integer, processInfos)
            Dim _dicoDelSimp As New List(Of Integer)
            Dim _dicoNew As New List(Of Integer)

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

            Catch ex As Exception
                Misc.ShowDebugError(ex)
            Finally
                _semProcess.Release()
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
    Public Shared Event GotNewItems(ByVal pids As List(Of Integer), ByVal newItems As Dictionary(Of Integer, processInfos))
    Public Shared Event GotDeletedItems(ByVal pids As Dictionary(Of Integer, processInfos))
    Public Shared Event GotRefreshed(ByVal newPids As List(Of Integer), ByVal delPids As List(Of Integer), ByVal Dico As Dictionary(Of Integer, processInfos))


    ' ========================================
    ' Public functions
    ' ========================================


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



    ' ========================================
    ' Private functions
    ' ========================================


End Class
