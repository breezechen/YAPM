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
    Private _machineName As String
    Private _lastStarted As Date


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "getter & setter"
    Public ReadOnly Property MachineName() As String
        Get
            If _machineName Is Nothing Then
                Return "localhost"
            Else
                Return _machineName
            End If
        End Get
    End Property
    Public ReadOnly Property CategoryName() As String
        Get
            Return _categoryName
        End Get
    End Property
    Public ReadOnly Property CounterName() As String
        Get
            Return _counterName
        End Get
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
    Public ReadOnly Property MonitorCreationDate() As Date
        Get
            Return _monitorCreated
        End Get
    End Property
    Public ReadOnly Property LastStarted() As Date
        Get
            Return _lastStarted
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _procName
        End Get
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
    Public ReadOnly Property InstanceName() As String
        Get
            Return _instanceName
        End Get
    End Property
#End Region


    ' ========================================
    ' Constructor & destructor
    ' ========================================
    Public Sub New(ByVal category As String, ByVal counter As String, _
        ByVal instance As String, Optional ByVal machine As String = Nothing)

        MyBase.New()
        timer.Stop()
        _enabled = False
        _categoryName = category
        _counterName = counter
        _instanceName = instance
        _machineName = machine
        Try
            If machine IsNot Nothing Then
                _pc = New System.Diagnostics.PerformanceCounter(category, counter, instance, machine)
            Else
                _pc = New System.Diagnostics.PerformanceCounter(category, counter, instance)
            End If
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to create performance counter")
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
            Dim tmp As Long = Date.Now.Ticks - Me.MonitorCreationDate.Ticks
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
