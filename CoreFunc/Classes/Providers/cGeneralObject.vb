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
        SyncLock _pendingTasks
            Dim _temp As List(Of Threading.Thread) = _pendingTasks
            For Each task As Threading.Thread In _temp
                If task Is Nothing OrElse task.IsAlive = False Then
                    _pendingTasks.Remove(task)
                End If
            Next
            _pendingTasks = _temp
        End SyncLock
    End Sub

    Public ReadOnly Property GetPendingTasks() As List(Of Threading.Thread)
        Get
            Return _pendingTasks
        End Get
    End Property

    Public ReadOnly Property PendingTaskCount() As Integer
        Get
            Return _pendingTasks.Count
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
