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

Imports Common.Misc

Public Class frmSystemInfo

    ' Number of processors
    Private _processors As Integer = 1

    ' Proc usage per processor  processorNumber <-> CpuUsage history
    Private _procUsage As New Dictionary(Of Integer, List(Of Double))

    ' Timeline for proc usage
    Private _procTimeLine As New List(Of Long)

    Public Sub New()

        InitializeComponent()

        SetToolTip(Me.chkOneGraphPerCpu, "Display one graph per CPU or one graph for all CPUs")
        SetToolTip(Me.chkTopMost, "Display window always on top")
        CloseWithEchapKey(Me)

        ' Init position & size
        Pref.LoadFormPositionAndSize(Me, "PSfrmSystemInfo")

        Me.timerRefresh.Interval = My.Settings.SystemInterval
        Call timerRefresh_Tick(Nothing, Nothing)
        Me.chkOneGraphPerCpu.Enabled = (Program.SystemInfo.ProcessorCount > 1)

        Me.chkTopMost.Checked = My.Settings.SystemInfoTopMost
        Me.chkOneGraphPerCpu.Checked = Not (My.Settings.SystemInfoOneGraph)

        Call chkOneGraphPerCpu_CheckedChanged(Nothing, Nothing) ' Add graphs

        ' Add handlers for graph tooltips
        Me.gIO.ReturnTooltipText = AddressOf impTooltipIO
        Me.gMemory.ReturnTooltipText = AddressOf impTooltipMem

    End Sub

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick

        ' Get some date and date diff
        Static oldDate As Date = Date.Now
        Dim newDate As Date = Date.Now
        Dim diff As Date = New Date(newDate.Ticks - oldDate.Ticks)
        oldDate = newDate

        With Program.SystemInfo

            ' Highest values are Decimals

            Dim bi As Native.Api.NativeStructs.SystemBasicInformation = .BasicInformations
            Dim ci As Native.Api.NativeStructs.SystemCacheInformation = .CacheInformations
            Dim pi As Native.Api.NativeStructs.SystemPerformanceInformation = .PerformanceInformations
            Dim ppi() As Native.Api.NativeStructs.SystemProcessorPerformanceInformation = .ProcessorPerformanceInformations
            Dim _pagesize As Integer = bi.PageSize
            _processors = bi.NumberOfProcessors

            ' Cache
            Me.lblCacheCurrent.Text = GetFormatedSize(ci.SystemCacheWsSize)
            Me.lblCacheErrors.Text = CStr(ci.SystemCacheWsFaults)
            Me.lblCacheMaximum.Text = GetFormatedSize(Decimal.Multiply(ci.SystemCacheWsMaximum.ToInt64, _pagesize))
            Me.lblCacheMinimum.Text = GetFormatedSize(ci.SystemCacheWsMinimum.ToInt64 * _pagesize)
            Me.lblCachePeak.Text = GetFormatedSize(ci.SystemCacheWsPeakSize)

            ' Total
            Me.lblHandles.Text = CStr(.HandleCount)
            Me.lblProcesses.Text = CStr(.ProcessCount)
            Me.lblThreads.Text = CStr(.ThreadCount)

            ' Commit charge
            Me.lblCCC.Text = GetFormatedSize(Decimal.Multiply(pi.CommittedPages, _pagesize))
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
            Dim zTotInterruptCount As Long = 0
            Dim zTotIdleTime As Long = 0
            Dim zTotInterruptTime As Long = 0
            Dim zTotUserTime As Long = 0
            Dim zTotDpcTime As Long = 0
            Dim zTotKernelTime As Long = 0
            For x As Integer = 0 To ppi.Length - 1
                zTotInterruptCount += ppi(x).InterruptCount
                zTotIdleTime += CLng(ppi(x).IdleTime / _processors)
                zTotInterruptTime += CLng(ppi(x).InterruptTime / _processors)
                zTotUserTime += CLng(ppi(x).UserTime / _processors)
                zTotKernelTime += CLng(ppi(x).DpcTime / _processors)
                zTotDpcTime += CLng(ppi(x).KernelTime / _processors)
            Next
            zTotDpcTime = zTotDpcTime - zTotKernelTime - zTotIdleTime - zTotInterruptTime
            Me.lblCPUinterrupts.Text = CStr(zTotInterruptCount)
            Me.lblCPUprocessors.Text = CStr(_processors)
            Me.lblCPUsystemCalls.Text = CStr(pi.SystemCalls)
            Dim zTotal As Double = zTotDpcTime + zTotUserTime + zTotIdleTime

            Dim ts As Date = New Date(zTotIdleTime)
            Me.lblCPUidleTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & " (" & _
                Math.Round(zTotIdleTime / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zTotInterruptTime)
            Me.lblCPUinterruptTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & " (" & _
                Math.Round(zTotInterruptTime / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zTotUserTime)
            Me.lblCPUuserTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & " (" & _
                Math.Round(zTotUserTime / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zTotDpcTime)
            Me.lblCPUkernelTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & " (" & _
                Math.Round(zTotDpcTime / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zTotKernelTime)
            Me.lblCPUdpcTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & " (" & _
                Math.Round(zTotKernelTime / zTotal * 100, 3).ToString & " %)"
            ts = New Date(zTotInterruptTime + zTotUserTime + zTotDpcTime + zTotKernelTime)
            Me.lblCPUTotalTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond) & " (100 %)"


            ' Kernel pools
            Me.lblKnpa.Text = CStr(pi.NonPagedPoolAllocs)
            Me.lblKnpf.Text = CStr(pi.NonPagedPoolFrees)
            Me.lblKnpu.Text = GetFormatedSize(Decimal.Multiply(pi.NonPagedPoolUsage, _pagesize))
            Me.lblKpf.Text = CStr(pi.PagedPoolFrees)
            Me.lblKpp.Text = GetFormatedSize(Decimal.Multiply(pi.PagedPoolPages, _pagesize))
            Me.lblKpa.Text = CStr(pi.PagedPoolAllocs)


            ' ======== Graphics
            ' g1 (CPU)
            Call showCpuUsage(zTotUserTime, zTotDpcTime, ppi, diff)


            ' g2 (physical memory)
            Dim ggg2 As Double = Decimal.Multiply(bi.NumberOfPhysicalPages - pi.AvailablePages, _pagesize)
            Dim ggg3 As Double = Decimal.Multiply(pi.CommittedPages, _pagesize)
            Me.gMemory.Add2Values(ggg3, ggg2)
            Me.gMemory.TopText = "Phys. memory : " & Misc.GetFormatedSize(ggg2) & vbNewLine & "Commit : " & Misc.GetFormatedSize(ggg3)
            Me.gMemory.Refresh()


            ' g3 (I/O read+other - write)
            Static oldIOr As Long = 0
            Dim newIOr As Long = pi.IoReadTransferCount + pi.IoOtherTransferCount
            Dim diffIOr As Long = newIOr - oldIOr
            oldIOr = newIOr

            Dim v3 As Double
            If diff.Ticks > 0 Then
                v3 = diffIOr / diff.Ticks
            Else
                v3 = 0
            End If

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

            Me.gIO.Add2Values(v3 * 100, v4 * 100)
            Me.gIO.TopText = "R+O : " & Misc.GetFormatedSizePerSecond(diffIOr, forceZeroDisplay:=True) & vbNewLine & "W : " & Misc.GetFormatedSizePerSecond(diffIOw, forceZeroDisplay:=True)
            Me.gIO.Refresh()

        End With
    End Sub

    Private Sub frmSystemInfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Save position & size
        Pref.SaveFormPositionAndSize(Me, "PSfrmSystemInfo")
        ' Hide, and do not close
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub frmSystemInfo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.gMemory.Refresh()
        Me.gIO.Refresh()
    End Sub

    Private Sub chkOneGraphPerCpu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOneGraphPerCpu.CheckedChanged

        If My.Settings.SystemInfoOneGraph <> Not (Me.chkOneGraphPerCpu.Checked) Then
            My.Settings.SystemInfoOneGraph = Not (Me.chkOneGraphPerCpu.Checked)
            Try
                My.Settings.Save()
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If

        ' Delete graphs
        Me.SplitContainer1.Panel1.Controls.Clear()

        ' Add graphs
        If chkOneGraphPerCpu.Checked = False Then
            ' One graph
            Dim _g As New GraphChart
            _g.Dock = DockStyle.Fill
            _g.Visible = True
            _g.ColorGrid = Color.DarkGreen
            _g.BackColor = Color.Black
            _g.Name = "_graphCpuAll"
            _g.EnableGraph = True
            _g.Fixedheight = True
            _g.ShowSecondGraph = False
            _g.Color1 = Color.LimeGreen
            _g.ColorFill1 = Color.Green
            _g.ReturnTooltipText = AddressOf impTooltipCPU
            _g.EvMouseEnterGraph = AddressOf CpuGraphMouseEnter
            Me.SplitContainer1.Panel1.Controls.Add(_g)
            _g.ClearValue()
            Dim historyCount As Integer = _procTimeLine.Count   ' Saved values
            For y As Integer = 0 To historyCount - 1
                Dim sum As Double = 0 ' Total usage for all processors
                For x As Integer = 0 To _processors - 1
                    sum += _procUsage(x)(y)
                Next
                _g.AddValue(sum, _procTimeLine(y))
            Next
            _g.RePaintNow()
        Else
            ' One graph per CPU
            For x As Integer = 0 To _processors - 1
                Dim _g As New GraphChart
                _g.Dock = DockStyle.Left
                _g.Visible = True
                _g.ColorGrid = Color.DarkGreen
                _g.BackColor = Color.Black
                _g.Name = "_graph" & x.ToString
                _g.Tag = x
                _g.EnableGraph = True
                _g.Fixedheight = True
                _g.ShowSecondGraph = False
                _g.Color1 = Color.LimeGreen
                _g.ColorFill1 = Color.Green
                Me.SplitContainer1.Panel1.Controls.Add(_g)
                _g.ReturnTooltipText = AddressOf impTooltipCPU
                _g.EvMouseEnterGraph = AddressOf CpuGraphMouseEnter
                If x < _processors - 1 Then
                    Dim _p As New PictureBox
                    _p.BackColor = Color.Transparent
                    _p.Width = 2
                    _p.Dock = DockStyle.Left
                    _p.Name = "_pct" & x.ToString
                    Me.SplitContainer1.Panel1.Controls.Add(_p)
                End If
                Dim historyCount As Integer = _procTimeLine.Count   ' Saved values
                For y As Integer = 0 To historyCount - 1
                    _g.AddValue(_procUsage(x)(y), _procTimeLine(y))
                Next
                _g.RePaintNow()
            Next
            SplitContainer2_Resize(Nothing, Nothing)
        End If

    End Sub

    Private Sub SplitContainer2_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitContainer2.Resize
        ' Recalculate width if one graph per CPU
        If Me.chkOneGraphPerCpu.Checked Then
            For Each ct As Control In Me.SplitContainer1.Panel1.Controls
                If TypeOf ct Is GraphChart Then
                    ct.Width = CInt((Me.SplitContainer1.Panel1.Width - 2 * (_processors - 1)) / _processors)
                    ct.Refresh()
                End If
            Next
        End If
    End Sub

    Private Sub showCpuUsage(ByVal zres3 As Long, ByVal zres4 As Long, ByRef ppi() As  _
                             Native.Api.NativeStructs.SystemProcessorPerformanceInformation, _
                             ByVal diff As Date)

        Static oldProcTime As Long = 0
        Static _old() As Long = Nothing

        If Me.SplitContainer1.Panel1.Controls.Count = 0 Then
            ' No CPU graph have been added
            ' So we exit for now
            Exit Sub
        End If

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
        If Me.chkOneGraphPerCpu.Checked Then
            ' One graph per CPU
            Dim _totalCpuDiff As Double = 0
            For Each ct As Control In Me.SplitContainer1.Panel1.Controls
                If TypeOf ct Is GraphChart Then
                    Dim _g1 As GraphChart = CType(ct, GraphChart)
                    Dim _i As Integer = CInt(_g1.Tag)
                    Dim z As Double = _diff(_i) / diff.Ticks / _processors
                    _g1.AddValue(100 * z)
                    If _procUsage.ContainsKey(_i) = False Then
                        ' First time we add the value into the dico (create a
                        ' memory space for the processor _i)
                        _procUsage.Add(_i, New List(Of Double))
                    End If
                    _procUsage(_i).Add(100 * z)
                    _totalCpuDiff += z
                    _g1.TopText = "Cpu " & _i.ToString & " : " & Misc.GetFormatedPercentage(z, 3, True) & " %"
                    _g1.Refresh()
                End If
            Next
            Me.lblCPUUsage.Text = GetFormatedPercentage(_totalCpuDiff, 3) & " %"
        Else
            ' Only one graph for all CPUs
            Dim _totalCpuDiff As Double = 0
            For _i As Integer = 0 To _processors - 1
                Dim z As Double = _diff(_i) / diff.Ticks / _processors
                If _procUsage.ContainsKey(_i) = False Then
                    ' First time we add the value into the dico (create a
                    ' memory space for the processor _i)
                    _procUsage.Add(_i, New List(Of Double))
                End If
                _procUsage(_i).Add(100 * z)
                _totalCpuDiff += z
            Next
            Dim _g1 As GraphChart = CType(Me.SplitContainer1.Panel1.Controls(0), GraphChart)
            _g1.AddValue(100 * _totalCpuDiff)
            _g1.TopText = Misc.GetFormatedPercentage(_totalCpuDiff, 3, True) & " %"
            _g1.Refresh()
            Me.lblCPUUsage.Text = GetFormatedPercentage(_totalCpuDiff, 3) & " %"
        End If
        _procTimeLine.Add(Date.Now.Ticks)       ' New time in proc timeline

    End Sub

    Private Sub chkTopMost_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTopMost.CheckedChanged
        If Me.chkTopMost.Checked <> My.Settings.SystemInfoTopMost Then
            My.Settings.SystemInfoTopMost = Me.chkTopMost.Checked
            Try
                My.Settings.Save()
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If
        Me.TopMost = Me.chkTopMost.Checked
    End Sub

#Region "Graph tooltips proc"

    Private Function impTooltipMem(ByVal index As Integer, ByVal time As Long) As String
        Dim s As String = "Memory usage"
        s &= vbNewLine & "Phys. memory : " & Misc.GetFormatedSize(Me.gMemory.Values(index))
        s &= vbNewLine & "Commit : " & Misc.GetFormatedSize(Me.gMemory.Values2(index))
        Dim d As New Date(time)
        s &= vbNewLine & d.ToShortDateString & " " & d.ToLongTimeString
        Return s
    End Function

    Private Function impTooltipIO(ByVal index As Integer, ByVal time As Long) As String
        Dim s As String = "Input/output"
        s &= vbNewLine & "R+O : " & Misc.GetFormatedSizePerSecond(Me.gIO.Values(index), forceZeroDisplay:=True)
        s &= vbNewLine & "W : " & Misc.GetFormatedSizePerSecond(Me.gIO.Values2(index), forceZeroDisplay:=True)
        Dim d As New Date(time)
        s &= vbNewLine & d.ToShortDateString & " " & d.ToLongTimeString
        Return s
    End Function

    Private gCpuGraphSelected As GraphChart
    Private cpuSelected As Integer
    Private Function impTooltipCPU(ByVal index As Integer, ByVal time As Long) As String
        Dim s As String = "CPU usage"
        If Me.chkOneGraphPerCpu.Checked Then
            s &= " (CPU " & cpuSelected.ToString & ")"
        End If
        s &= vbNewLine & "Usage : " & Misc.GetFormatedPercentage(gCpuGraphSelected.Values(index) / 100, forceZeroDisplay:=True) & " %"
        Dim d As New Date(time)
        s &= vbNewLine & d.ToShortDateString & " " & d.ToLongTimeString
        Return s
    End Function

    Private Sub CpuGraphMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Set cpuSelected and gCpuGraphSelected
        gCpuGraphSelected = CType(sender, GraphChart)
        Dim x As Integer = 0
        For Each ctrl As Control In Me.SplitContainer1.Panel1.Controls
            If TypeOf ctrl Is GraphChart Then
                x += 1
                If ctrl.Equals(sender) Then
                    cpuSelected = x - 1
                    Exit For
                End If
            End If
        Next
    End Sub

#End Region

End Class