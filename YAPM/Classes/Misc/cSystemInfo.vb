' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Dim License as published by
' the Free Software Foundation; either version 3 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Dim License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, see http://www.gnu.org/licenses/.
'
'
' Some structures defined below come from Process Hacker by wj32


Option Strict On

Imports System.Runtime.InteropServices

Public Class cSystemInfo


    ' ========================================
    ' Private
    ' ========================================
    Private _handles As Integer
    Private _processes As Integer
    Private _threads As Integer
    Private _processors As Integer
    Private _physicalPagesCount As Integer
    Private _timerResolution As Integer
    Private _maxCache As Integer
    Private _currentCache As Integer
    Private _minCache As Integer
    Private _peakCache As Integer
    Private _cacheErrors As Integer
    Private _spi As API.SYSTEM_PERFORMANCE_INFORMATION
    Private _sbi As API.SYSTEM_BASIC_INFORMATION
    Private _ci As API.SYSTEM_CACHE_INFORMATION
    Private _ppi() As API.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION
    Private _totPhysMem As Decimal

    ' ========================================
    ' Properties
    ' ========================================
    Public ReadOnly Property TotalPhysicalMemory() As Decimal
        Get
            If _totPhysMem = 0 Then
                _totPhysMem = Decimal.Multiply(_sbi.NumberOfPhysicalPages, _sbi.PageSize)
            End If
            Return _totPhysMem
        End Get
    End Property
    Public ReadOnly Property HandleCount() As Integer
        Get
            Return _handles
        End Get
    End Property
    Public ReadOnly Property ProcessCount() As Integer
        Get
            Return _processes
        End Get
    End Property
    Public ReadOnly Property ThreadCount() As Integer
        Get
            Return _threads
        End Get
    End Property
    Public ReadOnly Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
    End Property
    Public ReadOnly Property PhysicalPagesCount() As Integer
        Get
            Return _physicalPagesCount
        End Get
    End Property
    Public ReadOnly Property TimerResolution() As Integer
        Get
            Return _timerResolution
        End Get
    End Property
    Public ReadOnly Property CacheErrors() As Integer
        Get
            Return _cacheErrors
        End Get
    End Property
    Public ReadOnly Property CachePeak() As Integer
        Get
            Return _peakCache
        End Get
    End Property
    Public ReadOnly Property CacheMin() As Integer
        Get
            Return _minCache
        End Get
    End Property
    Public ReadOnly Property CacheCurrent() As Integer
        Get
            Return _currentCache
        End Get
    End Property
    Public ReadOnly Property CacheMax() As Integer
        Get
            Return _maxCache
        End Get
    End Property
    Public ReadOnly Property PerformanceInformations() As API.SYSTEM_PERFORMANCE_INFORMATION
        Get
            Return _spi
        End Get
    End Property
    Public ReadOnly Property BasicInformations() As API.SYSTEM_BASIC_INFORMATION
        Get
            Return _sbi
        End Get
    End Property
    Public ReadOnly Property CacheInformations() As API.SYSTEM_CACHE_INFORMATION
        Get
            Return _ci
        End Get
    End Property
    Public ReadOnly Property ProcessorPerformanceInformations() As API.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION()
        Get
            Return _ppi
        End Get
    End Property
    Public ReadOnly Property PhysicalMemoryPercentageUsage() As Double
        Get
            Dim totalMem As Long = _sbi.NumberOfPhysicalPages
            Dim usedMem As Long = _sbi.NumberOfPhysicalPages - _spi.AvailablePages
            If totalMem > 0 Then
                Return usedMem / totalMem
            Else
                Return 0
            End If
        End Get
    End Property
    Public ReadOnly Property CpuUsage() As Double
        Get
            Static oldDate As Date = Date.Now
            Dim newDate As Date = Date.Now
            Dim diff As Date = New Date(newDate.Ticks - oldDate.Ticks)
            oldDate = newDate

            Dim zres0 As Long = 0
            Dim zres1 As Long = 0
            Dim zres2 As Long = 0
            Dim zres3 As Long = 0
            Dim zres4 As Long = 0
            Dim zres5 As Long = 0
            For x As Integer = 0 To _ppi.Length - 1
                zres0 += _ppi(x).InterruptCount
                zres1 += CLng(_ppi(x).IdleTime / _processors)
                zres2 += CLng(_ppi(x).InterruptTime / _processors)
                zres3 += CLng(_ppi(x).UserTime / _processors)
                zres4 += CLng(_ppi(x).DpcTime / _processors)
                zres5 += CLng(_ppi(x).KernelTime / _processors)
            Next

            Static oldProcTime As Long = 0
            Dim newProcTime As Long = zres3 + zres5 - zres1
            Dim diffProcTime As Long = newProcTime - oldProcTime

            If oldProcTime > 0 Then
                oldProcTime = newProcTime
                If diff.Ticks > 0 And _processors > 0 Then
                    Return diffProcTime / diff.Ticks '/ _processors
                Else
                    Return 0
                End If
            Else
                oldProcTime = newProcTime
                Return 0
            End If

        End Get
    End Property


    ' ========================================
    ' Public functions
    ' ========================================

    ' Get number of processors
    Public Shared Function GetProcessorCount() As Integer
        Static _count As Integer = 0
        If _count = 0 Then
            Dim bi As New API.SYSTEM_BASIC_INFORMATION
            Dim ret As Integer
            API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemBasicInformation, bi, Marshal.SizeOf(bi), ret)
            _count = bi.NumberOfProcessors
        End If
        Return _count
    End Function

    Public Sub New()
        MyBase.New()

        ' Basic informations (do not change)
        Dim bi As New API.SYSTEM_BASIC_INFORMATION
        Dim ret As Integer
        API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemBasicInformation, bi, Marshal.SizeOf(bi), ret)
        With bi
            _physicalPagesCount = .NumberOfPhysicalPages
            _processors = .NumberOfProcessors
            _timerResolution = .TimerResolution
        End With
        _sbi = bi

        Call RefreshInfo()
    End Sub
    Public Sub RefreshInfo()
        Dim ret As Integer

        Dim pi As New API.PERFORMANCE_INFORMATION
        API.GetPerformanceInfo(pi, Marshal.SizeOf(pi))
        With pi
            _threads = .ThreadCount
            _handles = .HandlesCount
            _processes = .ProcessCount
        End With

        Dim ci As New API.SYSTEM_CACHE_INFORMATION
        API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemCacheInformation, ci, Marshal.SizeOf(ci), ret)
        With ci
            _currentCache = .SystemCacheWsSize
            _peakCache = .SystemCacheWsPeakSize
            _cacheErrors = .SystemCacheWsFaults
            _minCache = .SystemCacheWsMinimum
            _maxCache = .SystemCacheWsMaximum
        End With
        _ci = ci

        Dim spi As New API.SYSTEM_PERFORMANCE_INFORMATION
        API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemPerformanceInformation, spi, Marshal.SizeOf(spi), ret)
        _spi = spi

        If _processors > 0 Then
            ReDim _ppi(_processors - 1)
            Dim __size As Integer = _processors * Marshal.SizeOf(_ppi(0))
            Dim pt As IntPtr = Marshal.AllocHGlobal(__size)
            API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessorTimes, pt, __size, ret)

            ' Conversion from unmanaged memory to valid array
            For x As Integer = 0 To _processors - 1
                Dim pt2 As IntPtr = CType(pt.ToInt32 + 48 * x, IntPtr)
                _ppi(x) = CType(Marshal.PtrToStructure(pt2, GetType(API.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION)), API.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION)
            Next

            'Dim dest() As Long
            'ReDim dest(11)
            'Marshal.Copy(pt, dest, 0, 12)
            'Marshal.FreeHGlobal(pt)
            'For x As Integer = 0 To _processors - 1
            '    With _ppi(x)
            '        .IdleTime = dest(x * 6)
            '        .KernelTime = dest(x * 6 + 1)
            '        .UserTime = dest(x * 6 + 2)
            '        .DpcTime = dest(x * 6 + 3)
            '        .InterruptTime = dest(x * 6 + 4)
            '        .InterruptCount = dest(x * 6 + 5)
            '    End With
            'Next


        Else
            ReDim _ppi(0)
        End If

    End Sub

End Class
