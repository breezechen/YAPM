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

    Private _newItem As Boolean = False
    Private _killedItem As Boolean = False
    Private _isDisplayed As Boolean = False
    Friend _objectCreationDate As Date
    Friend _pendingTasks As New List(Of Threading.Thread)

    Public Sub New()
        _objectCreationDate = Date.Now
    End Sub

    Public Sub AddPendingTask(ByRef thr As Threading.Thread)
        _pendingTasks.Add(thr)
    End Sub

    Public Sub RemoveDeadTasks()
        ' NOTHING FOR NOW
        'SyncLock _pendingTasks
        '    Dim _temp As New List(Of Threading.Thread) '= _pendingTasks
        '    _temp = _pendingTasks
        '    For Each task As Threading.Thread In _temp
        '        If task Is Nothing OrElse task.IsAlive = False Then
        '            _pendingTasks.Remove(task)
        '        End If
        '    Next
        '    _pendingTasks = _temp
        'End SyncLock
    End Sub

    Public ReadOnly Property GetPendingTasks() As List(Of Threading.Thread)
        Get
            Return _pendingTasks
        End Get
    End Property

    Public ReadOnly Property PendingTaskCount() As Integer
        Get
            Dim _cout As Integer = 0
            For Each th As System.Threading.Thread In _pendingTasks
                If th IsNot Nothing AndAlso th.IsAlive Then
                    _cout += 1
                End If
            Next
            'If _pendingTasks.Count > 0 Then
            '    RemoveDeadTasks()
            'End If
            Return _cout '_pendingTasks.Count
        End Get
    End Property

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
    Public Property IsNewItem() As Boolean
        Get
            Return _newItem
        End Get
        Set(ByVal value As Boolean)
            _newItem = value
        End Set
    End Property

    ' Get information by name
    Public MustOverride Function GetInformation(ByVal info As String) As String

End Class
