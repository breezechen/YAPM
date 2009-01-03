Option Strict On

Public Class cMonitor

    ' ========================================
    ' Public declarations
    ' ========================================
    Public Structure MonitorStructure
        Dim cpuTime As Long
        Dim cpuCounter As Double
        Dim mem As cProcess.PROCESS_MEMORY_COUNTERS
        Dim priority As Integer
        Dim threadsCount As Integer
    End Structure


    ' ========================================
    ' Private attributes
    ' ========================================
    Private proc As cProcess
    Private WithEvents timer As New Timers.Timer
    Private _Interval As Integer = 1000
    Private _procName As String
    Private _bCheckCPUtime As Boolean = False
    Private _bCheckCPU As Boolean = False
    Private _bCheckMemory As Boolean = False
    Private _bCheckThreads As Boolean = False
    Private _bCheckPriority As Boolean = False
    Private _colInfos As Collection
    Private _enabled As Boolean = False


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "getter & setter"
    Public Function GetMonitorItem(ByVal Index As Integer) As MonitorStructure
        Return CType(_colInfos.Item(Index), MonitorStructure)
    End Function
    Public Function GetMonitorItem(ByVal Index As Object) As MonitorStructure
        Return CType(_colInfos.Item(Index), MonitorStructure)
    End Function
    Public Function GetMonitorItem(ByVal Key As String) As MonitorStructure
        Return CType(_colInfos.Item(Key), MonitorStructure)
    End Function
    Public Function GetProcess() As cProcess
        Return proc
    End Function
    Public Function GetInterval() As Integer
        Return _Interval
    End Function
    Public Function GetEnabled() As Boolean
        Return _enabled
    End Function
    Public Function GetName() As String
        Return _procName
    End Function
    Public Function GetCheckMemory() As Boolean
        Return _bCheckMemory
    End Function
    Public Function getCheckCPU() As Boolean
        Return _bCheckCPU
    End Function
    Public Function getCheckCPUTime() As Boolean
        Return _bCheckCPUtime
    End Function
    Public Function GetCheckThreads() As Boolean
        Return _bCheckThreads
    End Function
    Public Function GetCheckPriority() As Boolean
        Return _bCheckPriority
    End Function
    Public Function GetMonitorItems() As Collection
        Return _colInfos
    End Function
    Public Sub SetName(ByVal value As String)
        _procName = value
    End Sub
    Public Sub SetCheckMemory(ByVal value As Boolean)
        _bCheckMemory = value
    End Sub
    Public Sub SetCheckCPU(ByVal value As Boolean)
        _bCheckCPU = value
    End Sub
    Public Sub SetCheckCPUTime(ByVal value As Boolean)
        _bCheckCPUtime = value
    End Sub
    Public Sub SetCheckThreads(ByVal value As Boolean)
        _bCheckThreads = value
    End Sub
    Public Sub SetCheckPriority(ByVal value As Boolean)
        _bCheckPriority = value
    End Sub
    Public Sub SetInterval(ByVal value As Integer)
        If value > 0 Then
            _Interval = value
            timer.Interval = value
        End If
    End Sub
    Public Sub SetEnabled(ByVal value As Boolean)
        _enabled = value
    End Sub
#End Region


    ' ========================================
    ' Constructor & destructor
    ' ========================================
    Public Sub New(ByVal pid As Integer, ByVal processName As String)
        MyBase.New()
        timer.Stop()
        _enabled = False
        _procName = processName
        proc = New cProcess(pid)
        _colInfos = New Collection
        proc = Nothing
    End Sub
    Protected Overloads Overrides Sub Finalize()
        _colInfos = Nothing
        Me.StopMonitoring()
        timer = Nothing
        MyBase.Finalize()
    End Sub


    ' ========================================
    ' Public functions
    ' ========================================

    ' Here we retrive desired informations
    Public Sub RetrieveInformationsNow(ByVal Key As String)
        Dim it As New MonitorStructure
        With it
            If _bCheckCPUtime Then .cpuTime = proc.GetProcessorTimeLong
            'If _bCheckCPU Then .cpulong = proc.getcpupercentage ' TODO
            If _bCheckMemory Then .mem = proc.GetMemoryInfos
            If _bCheckPriority Then .priority = proc.GetPriorityClassInt
            If _bCheckThreads Then .threadsCount = proc.GetThreads.Count
        End With

        Try
            _colInfos.Add(it, Key)
        Catch ex As Exception
            '
        End Try
    End Sub

    ' Start monitoring
    Public Sub StartMonitoring()
        _enabled = True
        timer.Start()
    End Sub

    ' Stop monitoring
    Public Sub StopMonitoring()
        _enabled = False
        timer.Stop()
    End Sub

    ' Unmonitor an item
    Public Sub UnMonitorItem(ByVal field As String)
        Select Case field
            Case "CPU percentage"
                _bCheckCPU = False
            Case "CPU time"
                _bCheckCPUtime = False
            Case "Memory"
                _bCheckMemory = False
            Case "Priority"
                _bCheckPriority = False
            Case "Thread count"
                _bCheckThreads = False
            Case Else
                '
        End Select
    End Sub


    ' ========================================
    ' Private functions
    ' ========================================

    ' Event (timer)
    Private Sub TimerIntervalElapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed
        Call RetrieveInformationsNow(CStr(Date.Now.Ticks))
    End Sub

End Class
