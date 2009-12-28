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

Public MustInherit Class cGeneralObject

    ' Number of actions executed
    Private Shared _actionCount As Integer = 1

    ' Is it a new item ?
    Private _newItem As Boolean = False

    ' Item is killed ?
    Private _killedItem As Boolean = False

    ' Kill count
    ' -1 => item exists
    ' 0 => have to be removed from list
    ' > 0 => will be decremented
    Private _killCount As Integer = -1

    ' New count
    ' 0 => no more in green
    ' > 0 => will be decremented
    Private _newCount As Integer

    ' Item is displayed ?
    Private _isDisplayed As Boolean = False

    ' Creation date of an item
    Friend _objectCreationDate As Date

    ' Contains list of all pending tasks
    Friend Shared _SharedPendingTasks As New Dictionary(Of Integer, System.Threading.WaitCallback)
    ' Contains list of current pending tasks of the object
    Friend _pendingTasks As New Dictionary(Of Integer, System.Threading.WaitCallback)

    ' Semaphore to protect dico of pendingTasks
    Public Shared globalSemPendingtask As New System.Threading.Semaphore(1, 1)

    ' Type of item
    ' This kind of stuff break the inheritance... but who cares ??
    Friend _TypeOfObject As Native.Api.Enums.GeneralObjectType


    Public Sub New()
        _objectCreationDate = Date.Now
    End Sub

    Public Shared Function GetActionCount() As Integer
        ' This could be considered as a atomic operation...
        _actionCount += 1
        Return _actionCount
    End Function

    ' Add a pending task to the list
    ' "Shared" is called in shared methods
    Public Sub AddPendingTask(ByVal actionCount As Integer, ByRef thr As System.Threading.WaitCallback)
        Try
            globalSemPendingtask.WaitOne()
            _SharedPendingTasks.Add(actionCount, thr)
            _pendingTasks.Add(actionCount, thr)
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            globalSemPendingtask.Release()
        End Try
    End Sub
    Public Shared Sub AddSharedPendingTask(ByVal actionCount As Integer, ByRef thr As System.Threading.WaitCallback)
        Try
            globalSemPendingtask.WaitOne()
            _SharedPendingTasks.Add(actionCount, thr)
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            globalSemPendingtask.Release()
        End Try
    End Sub

    ' Remove a pending task from the list
    ' "Shared" is called in shared methods
    Public Sub RemovePendingTask(ByVal actionCount As Integer)
        Try
            globalSemPendingtask.WaitOne()
            If _SharedPendingTasks.ContainsKey(actionCount) Then
                _SharedPendingTasks.Remove(actionCount)
            End If
            If _pendingTasks.ContainsKey(actionCount) Then
                _pendingTasks.Remove(actionCount)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            globalSemPendingtask.Release()
        End Try
    End Sub
    Public Shared Sub RemoveSharedPendingTask(ByVal actionCount As Integer)
        Try
            globalSemPendingtask.WaitOne()
            If _SharedPendingTasks.ContainsKey(actionCount) Then
                _SharedPendingTasks.Remove(actionCount)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            globalSemPendingtask.Release()
        End Try
    End Sub

    ' Return pending tasks
    Public ReadOnly Property GetPendingTasks() As Dictionary(Of Integer, System.Threading.WaitCallback)
        Get
            Return _pendingTasks
        End Get
    End Property
    Public Shared ReadOnly Property GetAllPendingTasks() As Dictionary(Of Integer, System.Threading.WaitCallback)
        Get
            Return _SharedPendingTasks
        End Get
    End Property

    ' Return count of pending task
    Public ReadOnly Property PendingTaskCount() As Integer
        Get
            Try
                globalSemPendingtask.WaitOne()
                Dim _cout As Integer = 0
                For Each th As System.Threading.WaitCallback In _pendingTasks.Values
                    If th IsNot Nothing Then
                        _cout += 1
                    End If
                Next
                Return _cout
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            Finally
                globalSemPendingtask.Release()
            End Try
        End Get
    End Property
    Public ReadOnly Property AllPendingTaskCount() As Integer
        Get
            Try
                globalSemPendingtask.WaitOne()
                Dim _cout As Integer = 0
                For Each th As System.Threading.WaitCallback In _SharedPendingTasks.Values
                    If th IsNot Nothing Then
                        _cout += 1
                    End If
                Next
                Return _cout
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            Finally
                globalSemPendingtask.Release()
            End Try
        End Get
    End Property

    ' Is item displayed, killed or new ?
    Public Property IsDisplayed() As Boolean
        Get
            Return _isDisplayed
        End Get
        Set(ByVal value As Boolean)
            _isDisplayed = value
        End Set
    End Property
    Public Property IsKilledItem() As Boolean
        Get
            Return _killedItem
        End Get
        Set(ByVal value As Boolean)
            _killedItem = value
        End Set
    End Property
    Public Property KillCount() As Integer
        Get
            Return _killCount
        End Get
        Set(ByVal value As Integer)
            _killCount = value
        End Set
    End Property
    Public Property NewCount() As Integer
        Get
            Return _newCount
        End Get
        Set(ByVal value As Integer)
            _newCount = value
        End Set
    End Property
    Public Property IsNewItem() As Boolean
        Get
            Return _newItem
        End Get
        Set(ByVal value As Boolean)
            _newItem = value
        End Set
    End Property

    ' Type of item
    Public ReadOnly Property TypeOfObject() As Native.Api.Enums.GeneralObjectType
        Get
            Return _TypeOfObject
        End Get
    End Property


    ' Get information by name
    ' The first simply retrieve the information, and the second one
    ' remember last value retrieved, and return True if it has changed
    Public MustOverride Function GetInformation(ByVal info As String) As String
    Public MustOverride Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

    ' List of available properties for the cXXX object
    Public MustOverride Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()

    ' Return backcolor of the item, when displayed in a listview
    Public Overridable Function GetBackColor() As System.Drawing.Color
        Return Drawing.Color.White
    End Function

    ' Return forecolor of the item, when displayed in a listview
    Public Overridable Function GetForeColor() As System.Drawing.Color
        Return Drawing.Color.Black
    End Function

End Class
