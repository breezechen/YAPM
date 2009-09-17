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

Imports System.Runtime.InteropServices
Imports YAPM.Common.Misc

Public Class frmJobInfo

    Private WithEvents curJob As cJob

    Private WithEvents theConnection As cConnection
    Private _local As Boolean = True
    Private _notWMI As Boolean


    ' Refresh current tab
    Private Sub refreshJobTab()

        Me.Text = "Job informations (" & curJob.Infos.Name & ")"

        If curJob Is Nothing Then Exit Sub

        Select Case Me.tabJob.SelectedTab.Text

            Case "General"
                ' Update processes in job
                Me.lvProcess.Job = curJob
                Me.lvProcess.UpdateTheItems()

            Case "Statistics"

                ' Refresh stats
                curJob.Refresh()

                ' CPU
                Dim ts As Date
                Dim s As String
                Me.lblAffinity.Text = curJob.BasicLimitInformation.Affinity.ToString
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.TotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblUserTime.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblUserPeriod.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.TotalKernelTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblKernelTime.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalKernelTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblPeriodKernel.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalKernelTime + curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblTotalPeriod.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.TotalKernelTime + curJob.BasicAndIoAccountingInformation.BasicInfo.TotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblTotalTime.Text = s
                Me.lblPriority.Text = curJob.BasicLimitInformation.PriorityClass.ToString

                ' Others
                Me.lblTotalProcesses.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.TotalProcesses.ToString
                Me.lblActiveProcesses.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.ActiveProcesses.ToString
                Me.lblTotalTerminatedProcesses.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.TotalTerminatedProcesses.ToString
                Me.lblMaxWSS.Text = GetFormatedSize(curJob.BasicLimitInformation.MaximumWorkingSetSize)
                Me.lblMinWSS.Text = GetFormatedSize(curJob.BasicLimitInformation.MinimumWorkingSetSize)
                Me.lblSchedulingClass.Text = curJob.BasicLimitInformation.SchedulingClass.ToString
                Me.lblPageFaultCount.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.TotalPageFaultCount.ToString

                ' IO
                Me.lblProcOther.Text = curJob.BasicAndIoAccountingInformation.IoInfo.OtherOperationCount.ToString
                Me.lblProcOtherBytes.Text = GetFormatedSize(curJob.BasicAndIoAccountingInformation.IoInfo.OtherTransferCount)
                Me.lblProcReads.Text = curJob.BasicAndIoAccountingInformation.IoInfo.WriteOperationCount.ToString
                Me.lblProcReadBytes.Text = GetFormatedSize(curJob.BasicAndIoAccountingInformation.IoInfo.ReadTransferCount)
                Me.lblProcWriteBytes.Text = curJob.BasicAndIoAccountingInformation.IoInfo.ReadOperationCount.ToString
                Me.lblProcWrites.Text = GetFormatedSize(curJob.BasicAndIoAccountingInformation.IoInfo.WriteTransferCount)


            Case "Limitations"


        End Select
    End Sub

    Private Sub frmServiceInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            Call tabJob_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        ' Some tooltips
        Native.Functions.Misc.SetTheme(Me.lvProcess.Handle)
        Native.Functions.Misc.SetTheme(Me.lvLimits.Handle)
        SetToolTip(Me.cmdTerminateJob, "Terminate the job")
        SetToolTip(Me.cmdSetLimits, "Add a limit to the job")

        Pref.LoadListViewColumns(Me.lvProcess, "COLmain_process")

        ' Add some submenus (Copy to clipboard)
        For Each ss As String In jobLimitInfos.GetAvailableProperties(True)
            Me.MenuItemCopyLimit.MenuItems.Add(ss, AddressOf MenuItemCopyLimit_Click)
        Next
        For Each ss As String In processInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyProcess.MenuItems.Add(ss, AddressOf MenuItemCopyProcess_Click)
        Next

        Select Case My.Settings.JobSelectedTab
            Case "General"
                Me.tabJob.SelectedTab = Me.pageGeneral
            Case "Statistics"
                Me.tabJob.SelectedTab = Me.pageStats
            Case "Limitations"
                Me.tabJob.SelectedTab = Me.pageLimitations
        End Select

        Call Connect()
        Call refreshJobTab()

    End Sub

#Region "Copy to clipboard menus"

    Private Sub MenuItemCopyProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyLimit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cJobLimit In Me.lvLimits.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

#End Region

    ' Get process to monitor
    Public Sub SetJob(ByRef job As cJob)

        curJob = job

        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cProcess.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        Me.Timer.Enabled = True
        Me.cmdSetLimits.Enabled = _notWMI

    End Sub

    Private Sub tabJob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabJob.SelectedIndexChanged
        Call Me.refreshJobTab()
        My.Settings.JobSelectedTab = Me.tabJob.SelectedTab.Text
    End Sub

    ' Connection
    Public Sub Connect()
        ' Connect providers
        'theConnection.CopyFromInstance(Program.Connection)
        Try
            theConnection = Program.Connection
            Me.lvProcess.ConnectionObj = theConnection
            Me.lvLimits.ConnectionObj = theConnection
            theConnection.Connect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Can not connect")
        End Try
    End Sub

    Private Sub theConnection_Connected() Handles theConnection.Connected
        '
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        '
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ' Refresh informations about process
        Call Me.refreshJobTab()
    End Sub

    Private Sub cmdTerminateJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTerminateJob.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to terminate the job ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        curJob.TerminateJob()
    End Sub

    Private Sub TimerLimits_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerLimits.Tick
        Me.lvLimits.JobName = curJob.Infos.Name
        Me.lvLimits.UpdateTheItems()
    End Sub

    Private Sub lvLimits_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvLimits.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectedAtLeastOnce As Boolean = (Me.lvLimits.GetSelectedItems IsNot Nothing AndAlso Me.lvLimits.GetSelectedItems.Count >= 1)
            Me.MenuItemCopyLimit.Enabled = selectedAtLeastOnce
            Me.mnuLimit.Show(Me.lvLimits, e.Location)
        End If
    End Sub

    Private Sub MenuItemLimitRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '
    End Sub

    Private Sub cmdSetLimits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetLimits.Click
        Dim frm As New frmSetJobLimits
        frm.JobName = curJob.Infos.Name
        frm.ShowDialog()
    End Sub

    Private Sub lvProcess_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcess.KeyDown
        If e.KeyCode = Keys.Delete And Me.lvProcess.SelectedItems.Count > 0 Then
            If My.Settings.WarnDangerousActions Then
                If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            End If
            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                it.Kill()
            Next
        ElseIf e.KeyCode = Keys.Enter And Me.lvProcess.SelectedItems.Count > 0 Then
            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                Dim frm As New frmProcessInfo
                frm.SetProcess(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        End If
    End Sub

    Private Sub lvProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                Dim frm As New frmProcessInfo
                frm.SetProcess(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        End If
    End Sub

    Private Sub lvProcess_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As Integer = -1
            If Me.lvProcess.SelectedItems Is Nothing Then
                Me.MenuItemProcPIdle.Checked = False
                Me.MenuItemProcPN.Checked = False
                Me.MenuItemProcPAN.Checked = False
                Me.MenuItemProcPBN.Checked = False
                Me.MenuItemProcPH.Checked = False
                Me.MenuItemProcPRT.Checked = False
                Exit Sub
            End If
            If Me.lvProcess.SelectedItems.Count = 1 Then
                p = Me.lvProcess.GetSelectedItem.Infos.Priority
            End If
            Me.MenuItemProcPIdle.Checked = (p = ProcessPriorityClass.Idle)
            Me.MenuItemProcPN.Checked = (p = ProcessPriorityClass.Normal)
            Me.MenuItemProcPAN.Checked = (p = ProcessPriorityClass.AboveNormal)
            Me.MenuItemProcPBN.Checked = (p = ProcessPriorityClass.BelowNormal)
            Me.MenuItemProcPH.Checked = (p = ProcessPriorityClass.High)
            Me.MenuItemProcPRT.Checked = (p = ProcessPriorityClass.RealTime)

            Dim selectionIsNotNothing As Boolean = (Me.lvProcess.SelectedItems IsNot Nothing AndAlso Me.lvProcess.SelectedItems.Count > 0)
            Me.MenuItem35.Enabled = selectionIsNotNothing
            Me.MenuItemProcKill.Enabled = selectionIsNotNothing
            Me.MenuItemProcPriority.Enabled = selectionIsNotNothing
            Me.MenuItemProcResume.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcKillT.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcStop.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcResume.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcSFileDetails.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSSearch.Enabled = selectionIsNotNothing
            Me.MenuItemProcSDep.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemCopyProcess.Enabled = selectionIsNotNothing
            Me.MenuItemProcSFileDetails.Enabled = (selectionIsNotNothing AndAlso Me.lvProcess.SelectedItems.Count = 1)
            Me.MenuItemProcDump.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcAff.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcWSS.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcKillByMethod.Enabled = selectionIsNotNothing AndAlso _notWMI

            Me.mnuProcess.Show(Me.lvProcess, e.Location)
        End If
    End Sub

    Private Sub MenuItemProcKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKill.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.Kill()
        Next
    End Sub

    Private Sub MenuItemProcKillT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKillT.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.KillProcessTree()
        Next
    End Sub

    Private Sub MenuItemProcStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcStop.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to suspend these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SuspendProcess()
        Next
    End Sub

    Private Sub MenuItemProcResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcResume.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.ResumeProcess()
        Next
    End Sub

    Private Sub MenuItemProcPIdle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPIdle.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.Idle)
        Next
    End Sub

    Private Sub MenuItemProcPBN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPBN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub MenuItemProcPN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub MenuItemProcPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPAN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.AboveNormal)
        Next
    End Sub

    Private Sub MenuItemProcPH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPH.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub MenuItemProcPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPRT.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.RealTime)
        Next
    End Sub

    Private Sub MenuItemProcWorkingSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcWSS.Click
        For Each _p As cProcess In Me.lvProcess.GetSelectedItems
            _p.EmptyWorkingSetSize()
        Next
    End Sub

    Private Sub MenuItemProcDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcDump.Click
        Dim _frm As New frmDumpFile
        _frm.TopMost = _frmMain.TopMost
        If _frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each cp As cProcess In Me.lvProcess.GetSelectedItems
                Dim _file As String = _frm.TargetDir & "\" & Date.Now.Ticks.ToString & "_" & cp.Infos.Name & ".dmp"
                Call cp.CreateDumpFile(_file, _frm.DumpOption)
            Next
        End If
    End Sub

    Private Sub MenuItemProcAff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcAff.Click
        If Me.lvProcess.SelectedItems.Count = 0 Then Exit Sub

        Dim c() As cProcess
        ReDim c(Me.lvProcess.SelectedItems.Count - 1)
        Dim x As Integer = 0
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            c(x) = it
            x += 1
        Next

        Dim frm As New frmProcessAffinity(c)
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub MenuItemProcSFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSFileProp.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If IO.File.Exists(cp.Infos.Path) Then
                cFile.ShowFileProperty(cp.Infos.Path, Me.Handle)
            End If
        Next
    End Sub

    Private Sub MenuItemProcSOpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSOpenDir.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If cp.Infos.Path <> NO_INFO_RETRIEVED Then
                cFile.OpenDirectory(cp.Infos.Path)
            End If
        Next
    End Sub

    Private Sub MenuItemProcSFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSFileDetails.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim cp As cProcess = Me.lvProcess.GetSelectedItem
            Dim s As String = cp.Infos.Path
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub MenuItemProcSSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSSearch.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Application.DoEvents()
            Call SearchInternet(cp.Infos.Name, Me.Handle)
        Next
    End Sub

    Private Sub MenuItemProcSDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSDep.Click
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            If System.IO.File.Exists(it.Infos.Path) Then
                Dim frm As New frmDepViewerMain
                frm.HideOpenMenu()
                frm.OpenReferences(it.Infos.Path)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemProcColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcColumns.Click
        Me.lvProcess.ChooseColumns()
    End Sub

End Class