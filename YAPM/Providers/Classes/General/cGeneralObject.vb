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

    Private Shared _sharedObj As New Object
    Private Shared _actionCount As Integer = 1
    Private _newItem As Boolean = False
    Private _killedItem As Boolean = False
    Private _isDisplayed As Boolean = False
    Friend _objectCreationDate As Date
    Friend Shared _pendingTasks As New List(Of Threading.Thread)
    Friend Shared _pendingTasks2 As New Dictionary(Of Integer, System.Threading.WaitCallback)

    Public Sub New()
        _objectCreationDate = Date.Now
    End Sub

    Public Shared Function GetActionCount() As Integer
        SyncLock _sharedObj
            _actionCount += 1
            Return _actionCount
        End SyncLock
    End Function

    Public Shared Sub AddPendingTask2(ByVal actionCount As Integer, ByRef thr As System.Threading.WaitCallback)
        _pendingTasks2.Add(actionCount, thr)
    End Sub
    Public Shared Sub RemovePendingTask(ByVal actionCount As Integer)
        If _pendingTasks2.ContainsKey(actionCount) Then
            _pendingTasks2.Remove(actionCount)
        End If
    End Sub

    Public Sub AddPendingTask(ByRef thr As Threading.Thread)
        _pendingTasks.Add(thr)
    End Sub

    Public Sub RemoveDeadTasks()
    End Sub

    Public ReadOnly Property GetPendingTasks() As List(Of Threading.Thread)
        Get
            Return _pendingTasks
        End Get
    End Property
    Public ReadOnly Property GetPendingTasks2() As Dictionary(Of Integer, System.Threading.WaitCallback)
        Get
            Return _pendingTasks2
        End Get
    End Property
    Public ReadOnly Property PendingTaskCount() As Integer
        Get
            Dim _cout As Integer = 0
            For Each th As System.Threading.WaitCallback In _pendingTasks2.Values
                If th IsNot Nothing Then
                    _cout += 1
                End If
            Next
            Return _cout
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

    ' Return true if an item has changed
    ' If item has changed, all subitems of the listviewitem in the lv associated
    ' with this object will be refreshed
    Public Overridable ReadOnly Property ItemHasChanged() As Boolean
        Get
            Return True
        End Get
    End Property

    ' Get information by name
    Public MustOverride Function GetInformation(ByVal info As String) As String

    ' Return backcolor of the item, when displayed in a listview
    Public Overridable Function GetBackColor() As System.Drawing.Color
        Return Drawing.Color.White
    End Function

    ' Return forecolor of the item, when displayed in a listview
    Public Overridable Function GetForeColor() As System.Drawing.Color
        Return Drawing.Color.Black
    End Function


End Class
