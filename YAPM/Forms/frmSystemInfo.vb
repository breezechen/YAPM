' =======================================================
' Yet Another Process Monitor (YAPM)
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

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick

        ' Get some date and date diff
        Static oldDate As Date = Date.Now
        Dim newDate As Date = Date.Now
        Dim diff As Date = New Date(newDate.Ticks - oldDate.Ticks)
        oldDate = newDate

        ' Refresh informations
        Call frmMain.cInfo.RefreshInfo()

        With frmMain.cInfo

            ' Highest values are Decimals

            Dim bi As cSystemInfo.SYSTEM_BASIC_INFORMATION = .BasicInformations
            Dim ci As cSystemInfo.SYSTEM_CACHE_INFORMATION = .CacheInformations
            Dim pi As cSystemInfo.SYSTEM_PERFORMANCE_INFORMATION = .PerformanceInformations
            Dim ppi() As cSystemInfo.SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION = .ProcessorPerformanceInformations
            Dim _pagesize As Integer = bi.PageSize
            Dim _processors As Integer = bi.NumberOfProcessors

            ' Cache
            Me.lblCacheCurrent.Text = mdlMisc.GetFormatedSize(ci.SystemCacheWsSize)
            Me.lblCacheErrors.Text = CStr(ci.SystemCacheWsFaults)
            Me.lblCacheMaximum.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(ci.SystemCacheWsMaximum, _pagesize))
            Me.lblCacheMinimum.Text = mdlMisc.GetFormatedSize(ci.SystemCacheWsMinimum * _pagesize)
            Me.lblCachePeak.Text = mdlMisc.GetFormatedSize(ci.SystemCacheWsPeakSize)

            ' Total
            Me.lblHandles.Text = CStr(.HandleCount)
            Me.lblProcesses.Text = CStr(.ProcessCount)
            Me.lblThreads.Text = CStr(.ThreadCount)

            ' Commit charge
            Me.lblCCC.Text = mdlMisc.GetFormatedSize(pi.CommittedPages * _pagesize)
            Me.lblCCP.Text = mdlMisc.GetFormatedSize(pi.PeakCommitment * _pagesize)
            Me.lblCCL.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(pi.CommitLimit, _pagesize))

            ' Physical memory
            Me.lblPMF.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(pi.AvailablePages, _pagesize))
            Me.lblPMT.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(bi.NumberOfPhysicalPages, _pagesize))
            Me.lblPMU.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(Decimal.Subtract(bi.NumberOfPhysicalPages, pi.AvailablePages), _pagesize))

            ' I/O
            Me.lblIOotherBytes.Text = mdlMisc.GetFormatedSize(pi.IoOtherTransferCount)
            Me.lblIOothers.Text = CStr(pi.IoOtherOperationCount)
            Me.lblIOreadBytes.Text = mdlMisc.GetFormatedSize(pi.IoReadTransferCount)
            Me.lblIOreads.Text = CStr(pi.IoReadOperationCount)
            Me.lblIOwriteBytes.Text = mdlMisc.GetFormatedSize(pi.IoWriteTransferCount)
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
                zres4 += CLng(ppi(x).KernelTime / _processors)
                zres5 += CLng(ppi(x).DpcTime / _processors)
            Next
            Me.lblCPUinterrupts.Text = CStr(zres0)
            Me.lblCPUprocessors.Text = CStr(_processors)
            Me.lblCPUsystemCalls.Text = CStr(pi.SystemCalls)

            Dim ts As Date = New Date(zres1)
            Me.lblCPUidleTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond)
            ts = New Date(zres2)
            Me.lblCPUinterruptTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond)
            ts = New Date(zres3)
            Me.lblCPUuserTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond)
            ts = New Date(zres4)
            Me.lblCPUkernelTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond)
            ts = New Date(zres5)
            Me.lblCPUdpcTime.Text = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond)

            ' Kernel pools
            Me.lblKnpa.Text = CStr(pi.NonPagedPoolAllocs)
            Me.lblKnpf.Text = CStr(pi.NonPagedPoolFrees)
            Me.lblKnpu.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(pi.NonPagedPoolUsage, _pagesize))
            Me.lblKpf.Text = CStr(pi.PagedPoolFrees)
            Me.lblKpp.Text = mdlMisc.GetFormatedSize(Decimal.Multiply(pi.PagedPoolPages, _pagesize))
            Me.lblKpa.Text = CStr(pi.PagedPoolAllocs)


            ' ======== Graphics
            ' g1 (CPU)
            Static oldProcTime As Long = 0
            Dim newProcTime As Long = zres3 + zres4
            Dim diffProcTime As Long = newProcTime - oldProcTime
            oldProcTime = newProcTime

            Dim v1 As Double
            If diff.Ticks > 0 Then
                v1 = diffProcTime / diff.Ticks / bi.NumberOfProcessors
            Else
                v1 = 0
            End If
            Trace.WriteLine(v1)
            Me.g1.AddValue(v1 * 100)
            Me.g1.Refresh()


            ' g2 (physical memory)
            Dim ggg2 As Double = (pi.CommittedPages - pi.AvailablePages) * (_pagesize / 1024)
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
        Call timerRefresh_Tick(Nothing, Nothing)
    End Sub

    Private Sub frmSystemInfo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.g1.Refresh()
        Me.g2.Refresh()
        Me.g3.Refresh()
        Me.g4.Refresh()
    End Sub
End Class