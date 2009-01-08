Option Strict On

Public Class cMonitor

    Implements IDisposable

    ' ========================================
    ' Public declarations
    ' ========================================
    Public Structure MonitorStructure
        Dim value As Object
        Dim time As Integer
    End Structure


    ' ========================================
    ' Private attributes
    ' ========================================
    Private WithEvents timer As New Timers.Timer
    Private _pc As System.Diagnostics.PerformanceCounter
    Private _Interval As Integer = 1000
    Private _procName As String
    Private _categoryName As String
    Private _counterName As String
    Private _instanceName As String
    Private _colInfos As Collection
    Private _enabled As Boolean = False
    Private _monitorCreated As Date
    Private _lastStarted As Date


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "getter & setter"
    Public Property CategoryName() As String
        Get
            Return _categoryName
        End Get
        Set(ByVal value As String)
            _categoryName = value
        End Set
    End Property
    Public Property CounterName() As String
        Get
            Return _counterName
        End Get
        Set(ByVal value As String)
            _counterName = value
        End Set
    End Property
    Public Function GetMonitorItem(ByVal Index As Integer) As MonitorStructure
        Return CType(_colInfos.Item(Index), MonitorStructure)
    End Function
    Public Function GetMonitorItem(ByVal Index As Object) As MonitorStructure
        Return CType(_colInfos.Item(Index), MonitorStructure)
    End Function
    Public Function GetMonitorItem(ByVal Key As String) As MonitorStructure
        Return CType(_colInfos.Item(Key), MonitorStructure)
    End Function
    Public Function GetMonitorCreationDate() As Date
        Return _monitorCreated
    End Function
    Public Function GetLastStarted() As Date
        Return _lastStarted
    End Function
    Public Property Name() As String
        Get
            Return _procName
        End Get
        Set(ByVal value As String)
            _procName = value
        End Set
    End Property
    Public Function GetMonitorItems() As Collection
        Return _colInfos
    End Function
    Public Property Interval() As Integer
        Get
            Return _Interval
        End Get
        Set(ByVal value As Integer)
            If value > 0 Then
                _Interval = value
                timer.Interval = value
            End If
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property
    Public Function GetInstanceName() As String
        Return _instanceName
    End Function
#End Region


    ' ========================================
    ' Constructor & destructor
    ' ========================================
    Public Sub New(ByVal category As String, ByVal counter As String, _
        ByVal instance As String)

        MyBase.New()
        timer.Stop()
        _enabled = False
        _categoryName = category
        _counterName = counter
        _instanceName = instance
        Try
            _pc = New System.Diagnostics.PerformanceCounter(category, counter, instance)
        Catch ex As Exception
            MsgBox("Monitoring failed." & vbNewLine & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        _colInfos = New Collection
        _monitorCreated = Date.Now
    End Sub
    Protected Overloads Overrides Sub Finalize()
        _colInfos = Nothing
        If timer IsNot Nothing Then
            Me.StopMonitoring()
            timer.Dispose()
        End If
        If _pc IsNot Nothing Then
            _pc.Dispose()
        End If
        MyBase.Finalize()
    End Sub
    Public Sub Dispose() Implements System.IDisposable.Dispose
        _colInfos = Nothing
        If timer IsNot Nothing Then
            Me.StopMonitoring()
            timer.Dispose()
        End If
        If _pc IsNot Nothing Then
            _pc.Dispose()
        End If
        _pc = Nothing
    End Sub


    ' ========================================
    ' Public functions
    ' ========================================

    ' Here we retrive desired informations
    Public Sub RetrieveInformationsNow(ByVal Key As String)
        Static locTime As Integer = 0
        locTime += 1
        Dim it As New MonitorStructure
        With it
            Dim tmp As Long = Date.Now.Ticks - Me.GetMonitorCreationDate.Ticks
            .time = CInt(tmp / 10000)   ' milliseconds from start
            Try
                .value = _pc.NextValue
            Catch ex As Exception
                .value = Nothing
            End Try
        End With
        ' Beep()
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
        _lastStarted = Date.Now
    End Sub

    ' Stop monitoring
    Public Sub StopMonitoring()
        _enabled = False
        timer.Stop()
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================

    ' Event (timer)
    Private Sub TimerIntervalElapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed
        Call RetrieveInformationsNow(CStr(Date.Now.Ticks))
    End Sub

End Class
