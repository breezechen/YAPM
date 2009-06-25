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

Public Class frmSystemInfo

    Private _processors As Integer = 1

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick

        ' Get some date and date diff
        Static oldDate As Date = Date.Now
        Dim newDate As Date = Date.Now
        Dim diff As Date = New Date(newDate.Ticks - oldDate.Ticks)
        oldDate = newDate

        ' Refresh informations
        Call Program.SystemInfo.RefreshInfo()

        With Program.SystemInfo

            ' Highest values are Decimals

            Dim bi As API.SYSTEM_BASIC_INFORMATION = .BasicInformations
            Dim ci As API.SYSTEM_CACHE_INFORMATION = .CacheInformations
            Dim pi As API.SYSTEM_PERFORMANCE_INFORMATION = .PerformanceInformations
            Dim ppi() As API.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION = .ProcessorPerformanceInformations
            Dim _pagesize As Integer = bi.PageSize
            _processors = bi.NumberOfProcessors

            ' Cache
            Me.lblCacheCurrent.Text = GetFormatedSize(ci.SystemCacheWsSize)
            Me.lblCacheErrors.Text = CStr(ci.SystemCacheWsFaults)
            Me.lblCacheMaximum.Text = GetFormatedSize(Decimal.Multiply(ci.SystemCacheWsMaximum, _pagesize))
            Me.lblCacheMinimum.Text = GetFormatedSize(ci.SystemCacheWsMinimum * _pagesize)
            Me.lblCachePeak.Text = GetFormatedSize(ci.SystemCacheWsPeakSize)

            ' Total
            Me.lblHandles.Text = CStr(.HandleCount)
            Me.lblProcesses.Text = CStr(.ProcessCount)
            Me.lblThreads.Text = CStr(.ThreadCount)

            ' Commit charge
            Me.lblCCC.Text = GetFormatedSize(pi.CommittedPages * _pagesize)
            Me.lblCCP.Text = GetFormatedSize(Decimal.Multiply(pi.PeakCommitment, _pagesize))
            Me.lblCCL.Text = GetFormatedSize(Decimal.Multiply(pi.CommitLimit, _pagesize))

            ' Physical memory
            Me.lblPMF.Text = GetFormatedSize(Decimal.Multiply(pi.AvailablePages, _pagesize))
            Me.lblPMT.Text = GetFormatedSize(Decimal.Multiply(bi.NumberOfPhysicalPages, _pagesize))
            Me.lblPMU.Text = GetFormatedSize(Decimal.Multiply(Decimal.Subtract(bi.NumberOfPhysicalPages, pi.AvailablePages), _pagesize))

            ' I/O
            Me.lblIOotherBytes.Text = GetFormatedSize(pi.IoOtherTransferCount)
            Me.lblIOothers.Text = CStr(pi.IoOtherOperationCount)
            Me.lblIOreadBytes.Text = GetFormatedSize(pi.IoReadTransferCount)
            Me.lblIOreads.Text = CStr(pi.IoReadOperationCount)
            Me.lblIOwriteBytes.Text = GetFormatedSize(pi.IoWriteTransferCount)
            Me.lblIOwrites.Text = CStr(pi.IoWriteOperationCount)

            ' Page faults
            Me.lblPFcache.Text = CStr(ci.SystemCacheWsFaults)
            Me.lblPFcacheTransition.Text = CStr(pi.CacheTransitionFaults)
            Me.lblPFcopyOnWrite.Text = CStr(pi.CopyOnWriteFaults)
            Me.lblPFdemandZero.Text = CStr(pi.DemandZeroFaults)
            Me.lblPFtotal.Text = CStr(pi.PageFaults)
            Me.lblPFtransition.Text = CStr(pi.TransitionFaults)

            ' CPU
            Me.lblCPUcontextSwitches.Text = CStr(pi.ContextSwitches)
            Dim zres0 As Long = 0
            Dim zres1 As Long = 0
            Dim zres2 As Long = 0
            Dim zres3 As Long = 0
            Dim zres4 As Long = 0
            Dim zres5 As Long = 0
            For x As Integer = 0 To ppi.Length - 1
                zres0 += ppi(x).InterruptCount
                zres1 += CLng(ppi(x).IdleTime / _processors)
                zres2 += CLng(ppi(x).InterruptTime / _processors)
                zres3 += CLng(ppi(x).UserTime / _processors)
                zres5 += CLng(ppi(x).DpcTime / _processors)
                zres4 += CLng(ppi(x).KernelTime / _processors)
            Next
            zres4 = zres4 - zres5 - zres1 - zres2
            Me.lblCPUinterrupts.Text = CStr(zres0)
            Me.lblCPUprocessors.Text = CStr(_processors)
            Me.lblCPUsystemCalls.Text = CStr(pi.SystemCalls)
            Dim zTotal As Double = zres4 + zres3 + zres1

            Dim ts As Date = New Date(zres1)
            Me.lblCPUidleTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & vbTab & " (" & _
                Math.Round(zres1 / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zres2)
            Me.lblCPUinterruptTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & vbTab & " (" & _
                Math.Round(zres2 / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zres3)
            Me.lblCPUuserTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & vbTab & " (" & _
                Math.Round(zres3 / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zres4)
            Me.lblCPUkernelTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & vbTab & " (" & _
                Math.Round(zres4 / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zres5)
            Me.lblCPUdpcTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & vbTab & " (" & _
                Math.Round(zres5 / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zres2 + zres3 + zres4 + zres5)
            Me.lblCPUTotalTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & vbTab & " (100 %)"


            ' Kernel pools
            Me.lblKnpa.Text = CStr(pi.NonPagedPoolAllocs)
            Me.lblKnpf.Text = CStr(pi.NonPagedPoolFrees)
            Me.lblKnpu.Text = GetFormatedSize(Decimal.Multiply(pi.NonPagedPoolUsage, _pagesize))
            Me.lblKpf.Text = CStr(pi.PagedPoolFrees)
            Me.lblKpp.Text = GetFormatedSize(Decimal.Multiply(pi.PagedPoolPages, _pagesize))
            Me.lblKpa.Text = CStr(pi.PagedPoolAllocs)


            ' ======== Graphics
            ' g1 (CPU)
            Call showCpuUsage(zres3, zres4, ppi, diff)


            ' g2 (physical memory)
            Dim ggg2 As Double = 100 * (bi.NumberOfPhysicalPages - pi.AvailablePages) / bi.NumberOfPhysicalPages
            Me.g2.AddValue(ggg2)
            Me.g2.Refresh()


            ' g3 (I/O read)
            Static oldIOr As Long = 0
            Dim newIOr As Long = pi.IoReadTransferCount
            Dim diffIOr As Long = newIOr - oldIOr
            oldIOr = newIOr

            Dim v3 As Double
            If diff.Ticks > 0 Then
                v3 = diffIOr / diff.Ticks
            Else
                v3 = 0
            End If

            Me.g3.AddValue(v3 * 100)
            Me.g3.Refresh()


            ' g4 (I/O write)
            Static oldIOw As Long = 0
            Dim newIOw As Long = pi.IoWriteTransferCount
            Dim diffIOw As Long = newIOw - oldIOw
            oldIOw = newIOw

            Dim v4 As Double
            If diff.Ticks > 0 Then
                v4 = diffIOw / diff.Ticks
            Else
                v4 = 0
            End If

            Me.g4.AddValue(v4 * 100)
            Me.g4.Refresh()

        End With
    End Sub

    Private Sub frmSystemInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        Me.timerRefresh.Interval = My.Settings.SystemInterval
        Call chkOneGraphPerCpu_CheckedChanged(Nothing, Nothing) ' Add graphs
        Call timerRefresh_Tick(Nothing, Nothing)
        Me.chkOneGraphPerCpu.Enabled = (Program.SystemInfo.ProcessorCount > 1)
    End Sub

    Private Sub frmSystemInfo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.g2.Refresh()
        Me.g3.Refresh()
        Me.g4.Refresh()
    End Sub

    Private Sub chkOneGraphPerCpu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOneGraphPerCpu.CheckedChanged
        ' Delete graphs
        Me.SplitContainer1.Panel1.Controls.Clear()

        ' Add graphs
        If chkOneGraphPerCpu.Checked = False Then
            ' One graph
            Dim _g As New Graph2
            _g.Dock = DockStyle.Fill
            _g.Visible = True
            _g.ColorGrid = Color.DarkGreen
            _g.BackColor = Color.Black
            _g.Name = "_graphCpuAll"
            _g.EnableGraph = True
            _g.Fixedheight = True
            _g.ShowSecondGraph = False
            _g.Color = Color.LimeGreen
            _g.Color2 = Color.Green
            Me.SplitContainer1.Panel1.Controls.Add(_g)
            _g.Refresh()
        Else
            ' One graph per CPU
            For x As Integer = 0 To _processors - 1
                Dim _g As New Graph2
                _g.Dock = DockStyle.Left
                _g.Visible = True
                _g.ColorGrid = Color.DarkGreen
                _g.BackColor = Color.Black
                _g.Name = "_graph" & x.ToString
                _g.Tag = x
                _g.EnableGraph = True
                _g.Fixedheight = True
                _g.ShowSecondGraph = False
                _g.Color = Color.LimeGreen
                _g.Color2 = Color.Green
                Me.SplitContainer1.Panel1.Controls.Add(_g)
                If x < _processors - 1 Then
                    Dim _p As New PictureBox
                    _p.BackColor = Color.Transparent
                    _p.Width = 2
                    _p.Dock = DockStyle.Left
                    _p.Name = "_pct" & x.ToString
                    Me.SplitContainer1.Panel1.Controls.Add(_p)
                End If
                _g.Refresh()
            Next
            SplitContainer2_Resize(Nothing, Nothing)
        End If

    End Sub

    Private Sub SplitContainer2_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitContainer2.Resize
        ' Recalculate width if one graph per CPU
        If Me.chkOneGraphPerCpu.Checked Then
            For Each ct As Control In Me.SplitContainer1.Panel1.Controls
                If TypeOf ct Is Graph2 Then
                    ct.Width = CInt((Me.SplitContainer1.Panel1.Width - 2 * (_processors - 1)) / _processors)
                    ct.Refresh()
                End If
            Next
        End If
    End Sub

    Private Sub showCpuUsage(ByVal zres3 As Long, ByVal zres4 As Long, ByRef ppi() As  _
                             API.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION, _
                             ByVal diff As Date)

        Static oldProcTime As Long = 0
        Static _old() As Long = Nothing

        If chkOneGraphPerCpu.Checked = False Then
            ' One graph for all CPUs
            _old = Nothing
            Dim newProcTime As Long = zres3 + zres4
            Dim diffProcTime As Long = newProcTime - oldProcTime

            Dim v1 As Double
            If diff.Ticks > 0 AndAlso oldProcTime > 0 Then
                v1 = diffProcTime / diff.Ticks / _processors
            Else
                v1 = 0
            End If
            oldProcTime = newProcTime

            Dim _g1 As Graph2 = CType(Me.SplitContainer1.Panel1.Controls(0), Graph2)

            Me.lblCPUUsage.Text = GetFormatedPercentage(v1, 3) & " %"
            _g1.AddValue(v1 * 100)
            _g1.Refresh()
        Else
            ' One graph per CPU
            oldProcTime = 0
            If _old Is Nothing Then
                ReDim _old(_processors - 1)
            End If

            Dim _new() As Long
            ReDim _new(_processors - 1)
            For x As Integer = 0 To _processors - 1
                _new(x) = ppi(x).UserTime + ppi(x).KernelTime - ppi(x).IdleTime - ppi(x).InterruptTime - ppi(x).DpcTime
            Next

            Dim _diff() As Long
            ReDim _diff(_processors - 1)
            If diff.Ticks > 0 AndAlso _old(0) > 0 Then
                For x As Integer = 0 To _processors - 1
                    _diff(x) = _new(x) - _old(x)
                Next
            Else
                For x As Integer = 0 To _processors - 1
                    _diff(x) = 0
                Next
            End If
            _old = _new

            ' Refresh graphs
            Dim _totalCpuDiff As Double = 0
            For Each ct As Control In Me.SplitContainer1.Panel1.Controls
                If TypeOf ct Is Graph2 Then
                    Dim _g1 As Graph2 = CType(ct, Graph2)
                    Dim _i As Integer = CInt(_g1.Tag)
                    _g1.AddValue(100 * _diff(_i) / diff.Ticks / _processors)
                    _totalCpuDiff += _diff(_i) / diff.Ticks / _processors
                    _g1.Refresh()
                End If
            Next

            Me.lblCPUUsage.Text = GetFormatedPercentage(_totalCpuDiff, 3) & " %"


        End If
    End Sub

End Class