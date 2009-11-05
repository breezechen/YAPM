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

Public Class frmPreferences

    Private _newcolor As Integer
    Private _deletedcolor As Integer

    Private Sub cmdQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        _frmMain.timerProcess.Interval = My.Settings.ProcessInterval
        _frmMain.timerTask.Interval = My.Settings.TaskInterval
        _frmMain.timerNetwork.Interval = My.Settings.NetworkInterval
        _frmMain.timerServices.Interval = My.Settings.ServiceInterval
        _frmMain.timerTrayIcon.Interval = My.Settings.TrayInterval
        _frmMain.timerJobs.Interval = My.Settings.JobInterval
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        ' Save
        Dim _oldRibbonStyle As Boolean = My.Settings.UseRibbonStyle
        My.Settings.ServiceInterval = CInt(Val(Me.txtServiceIntervall.Text))
        My.Settings.ProcessInterval = CInt(Val(Me.txtProcessIntervall.Text))
        My.Settings.WindowsStartup = Me.chkStart.Checked
        My.Settings.StartHidden = Me.chkStartTray.Checked
        My.Settings.ReplaceTaskmgr = Me.chkReplaceTaskmgr.Checked
        My.Settings.TopMost = Me.chkTopMost.Checked
        My.Settings.NewItemColor = _newcolor
        My.Settings.HideWhenClosed = Me.chkHideClosed.Checked
        My.Settings.DeletedItemColor = _deletedcolor
        My.Settings.ShowTrayIcon = Me.chkTrayIcon.Checked
        My.Settings.Priority = Me.cbPriority.SelectedIndex
        My.Settings.TaskInterval = CInt(Val(Me.txtTaskInterval.Text))
        My.Settings.NetworkInterval = CInt(Val(Me.txtNetworkInterval.Text))
        My.Settings.JobInterval = CInt(Val(Me.txtJobInterval.Text))
        My.Settings.UseRibbonStyle = Me.chkRibbon.Checked
        My.Settings.SearchEngine = Me.txtSearchEngine.Text
        My.Settings.CloseYAPMWithCloseButton = Me.chkCloseButton.Checked
        My.Settings.WarnDangerousActions = Me.chkWarn.Checked
        My.Settings.HideWhenMinimized = Me.chkHideMinimized.Checked
        My.Settings.TrayInterval = CInt(Val(Me.txtTrayInterval.Text))
        My.Settings.SystemInterval = CInt(Val(Me.txtSysInfoInterval.Text))
        My.Settings.AutomaticInternetInfos = Me.chkAutoOnline.Checked
        My.Settings.AutomaticWintrust = Me.chkWintrust.Checked
        My.Settings.ShowUserGroupDomain = Me.chkUserGroup.Checked
        My.Settings.ShowStatusBar = Me.chkStatusBar.Checked
        My.Settings.ShowFixedTab = Me.chkFixedTab.Checked
        My.Settings.FixedTab = Me.cbShownTab.Text
        My.Settings.UpdateAlpha = Me.chkUpdateAlpha.Checked
        My.Settings.UpdateBeta = Me.chkUpdateBeta.Checked
        My.Settings.UpdateAuto = Me.chkUpdateAuto.Checked
        My.Settings.UpdateServer = Me.txtUpdateServer.Text
        My.Settings.ShowClassicMessageBoxes = Me.chkClassicMsgbox.Checked
        My.Settings.CoefTimeMul = CInt(Me.valCoefRemote.Value)

        If Me.chkUnlimitedBuf.Checked Then
            My.Settings.HistorySize = -1
        Else
            My.Settings.HistorySize = CInt(Me.bufferSize.Value * 1024)
        End If

        Common.Misc.StartWithWindows(My.Settings.WindowsStartup)
        Common.Misc.ReplaceTaskmgr(My.Settings.ReplaceTaskmgr)

        ' Highlightings
        For Each it As ListViewItem In Me.lvHighlightingOther.Items
            If it.Text = "Suspended thread" Then
                My.Settings.HighlightingColorSuspendedThread = it.BackColor
                'My.Settings.HighlightingPrioritySuspendedThread = CByte(it.Index + 1)
                My.Settings.EnableHighlightingSuspendedThread = it.Checked
            ElseIf it.Text = "Relocated module" Then
                My.Settings.HighlightingColorRelocatedModule = it.BackColor
                'My.Settings.HighlightingPriorityRelocatedModule = CByte(it.Index + 1)
                My.Settings.EnableHighlightingRelocatedModule = it.Checked
            End If
        Next
        For Each it As ListViewItem In Me.lvHighlightingProcess.Items
            If it.Text = "Process being debugged" Then
                My.Settings.HighlightingColorBeingDebugged = it.BackColor
                My.Settings.HighlightingPriorityBeingDebugged = CByte(it.Index + 1)
                My.Settings.EnableHighlightingBeingDebugged = it.Checked
            ElseIf it.Text = "Critical process" Then
                My.Settings.HighlightingColorCriticalProcess = it.BackColor
                My.Settings.HighlightingPriorityCriticalProcess = CByte(it.Index + 1)
                My.Settings.EnableHighlightingCriticalProcess = it.Checked
            ElseIf it.Text = "Elevated process" Then
                My.Settings.HighlightingColorElevatedProcess = it.BackColor
                My.Settings.HighlightingPriorityElevatedProcess = CByte(it.Index + 1)
                My.Settings.EnableHighlightingElevated = it.Checked
            ElseIf it.Text = "Process in job" Then
                My.Settings.HighlightingColorJobProcess = it.BackColor
                My.Settings.HighlightingPriorityJobProcess = CByte(it.Index + 1)
                My.Settings.EnableHighlightingJobProcess = it.Checked
            ElseIf it.Text = "Service process" Then
                My.Settings.HighlightingColorServiceProcess = it.BackColor
                My.Settings.HighlightingPriorityServiceProcess = CByte(it.Index + 1)
                My.Settings.EnableHighlightingServiceProcess = it.Checked
            ElseIf it.Text = "Owned process" Then
                My.Settings.HighlightingColorOwnedProcess = it.BackColor
                My.Settings.HighlightingPriorityOwnedProcess = CByte(it.Index + 1)
                My.Settings.EnableHighlightingOwnedProcess = it.Checked
            ElseIf it.Text = "System process" Then
                My.Settings.HighlightingColorSystemProcess = it.BackColor
                My.Settings.HighlightingPrioritySystemProcess = CByte(it.Index + 1)
                My.Settings.EnableHighlightingSystemProcess = it.Checked
            End If
        Next

        Program.Preferences.Save()
        Program.Preferences.Apply()

        If Not (_oldRibbonStyle = My.Settings.UseRibbonStyle) Then
            Dim ret As Integer
            ret = Misc.ShowMsg("Menu style has changed", "The new menu style will be displayed next time you start YAPM.", "Do you want to exit YAPM now ?", MessageBoxButtons.YesNo, TaskDialogIcon.Information, True)
            If ret = DialogResult.Yes Then
                Program.ExitYAPM()
                Exit Sub
            End If
        End If

        Misc.ShowMsg("Preferences", Nothing, "Saved preferences sucessfully.", MessageBoxButtons.OK, TaskDialogIcon.Information)

    End Sub

    Private Sub frmPreferences_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        Native.Functions.Misc.SetTheme(Me.lvHighlightingProcess.Handle)
        Native.Functions.Misc.SetTheme(Me.lvHighlightingOther.Handle)

        SetToolTip(Me.chkReplaceTaskmgr, "Replace taskmgr (do not forget to uncheck this option before you delete/move YAPM executable !!")
        SetToolTip(Me.chkStart, "Start YAPM on Windows startup.")
        SetToolTip(Me.chkStartTray, "Start YAPM hidden (only in tray system).")
        SetToolTip(Me.txtProcessIntervall, "Set interval (milliseconds) between two refreshments of process list.")
        SetToolTip(Me.txtServiceIntervall, "Set interval (milliseconds) between two refreshments of service list.")
        SetToolTip(Me.cmdSave, "Save configuration.")
        SetToolTip(Me.cmdQuit, "Quit without saving.")
        SetToolTip(Me.cmdDefaut, "Set default configuration.")
        SetToolTip(Me.chkTopMost, "Start YAPM topmost.")
        SetToolTip(Me.pctDeletedItems, "Color of deleted items.")
        SetToolTip(Me.pctNewitems, "Color of new items.")
        SetToolTip(Me.chkTrayIcon, "Show tray icon.")
        SetToolTip(Me.cbPriority, "Priority of YAPM.")
        SetToolTip(Me.chkWintrust, "Verify the signature of processes when opening process' detailed window.")
        SetToolTip(Me.txtTaskInterval, "Set interval (milliseconds) between two refreshments of task list.")
        SetToolTip(Me.txtNetworkInterval, "Set interval (milliseconds) between two refreshments of network connections list.")
        SetToolTip(Me.txtJobInterval, "Set interval (milliseconds) between two refreshments of job list.")
        SetToolTip(Me.chkRibbon, "Show ribbon style menu.")
        SetToolTip(Me.txtSearchEngine, "Search engine for 'Internet search'. Use the keyword ITEM to specify the item name to search.")
        SetToolTip(Me.chkCloseButton, "Close YAPM when close button is pressed (minimize to tray if not checked).")
        SetToolTip(Me.chkWarn, "Warn user for all (potentially) dangerous actions.")
        SetToolTip(Me.chkHideMinimized, "Hide main form when minimized.")
        SetToolTip(Me.txtTrayInterval, "Set interval (milliseconds) between two refreshments of tray icon.")
        SetToolTip(Me.txtSysInfoInterval, "Set interval (milliseconds) between two refreshments of system/network informations.")
        SetToolTip(Me.chkHideClosed, "Hide YAPM when user click on 'close' button")
        SetToolTip(Me.chkUnlimitedBuf, "No size limit for history")
        SetToolTip(Me.bufferSize, "Size of the buffer used to save history of statistics of one process (KB). The change of this value will be applied on the next start of YAPM.")
        SetToolTip(Me.chkAutoOnline, "Automatically retrieve online description of a process/service when detailed form is showned.")
        SetToolTip(Me.lvHighlightingProcess, "Enabled or not highlighting of items in listviews. Double click on a category to change its color.")
        SetToolTip(Me.lvHighlightingOther, "Enabled or not highlighting of items in listviews. Double click on a category to change its color.")
        SetToolTip(Me.cmdMoveDownProcess, "Decrease priority of selected category.")
        SetToolTip(Me.cmdMoveUpProcess, "Increase priority of selected category.")
        SetToolTip(Me.chkUserGroup, "Show or not user group/domain in process listview.")
        SetToolTip(Me.chkStatusBar, "Show or not status bar on main form.")
        SetToolTip(Me.chkFixedTab, "Show always the same tab when YAPM starts.")
        SetToolTip(Me.cbShownTab, "Tab to show when YAPM starts.")
        SetToolTip(Me.chkUpdateAlpha, "Check for alpha releases.")
        SetToolTip(Me.chkUpdateBeta, "Check for beta releases.")
        SetToolTip(Me.chkUpdateAuto, "Check for updates at startup.")
        SetToolTip(Me.cmdUpdateCheckNow, "Check for updates now.")
        SetToolTip(Me.txtUpdateServer, "Update server.")
        SetToolTip(Me.chkClassicMsgbox, "Display classical messageboxes (Windows XP style)")
        SetToolTip(Me.valCoefRemote, "Coefficient for update interval in case of remote monitoring." & vbNewLine & "For example, if you set 200, all refreshment intervals for remote monitoring will" & vbNewLine & "be 2 times greater than intervals for local monitoring.")


        ' Set control's values
        Me.txtServiceIntervall.Text = My.Settings.ServiceInterval.ToString
        Me.txtProcessIntervall.Text = My.Settings.ProcessInterval.ToString
        Me.chkStart.Checked = My.Settings.WindowsStartup
        Me.chkStartTray.Checked = My.Settings.StartHidden
        Me.chkReplaceTaskmgr.Checked = My.Settings.ReplaceTaskmgr
        Me.chkTopMost.Checked = My.Settings.TopMost
        Me.pctNewitems.BackColor = Color.FromArgb(My.Settings.NewItemColor)
        Me.pctDeletedItems.BackColor = Color.FromArgb(My.Settings.DeletedItemColor)
        _newcolor = My.Settings.NewItemColor
        _deletedcolor = My.Settings.DeletedItemColor
        Me.chkTrayIcon.Checked = My.Settings.ShowTrayIcon
        Me.cbPriority.SelectedIndex = My.Settings.Priority
        Me.txtTaskInterval.Text = My.Settings.TaskInterval.ToString
        Me.txtNetworkInterval.Text = My.Settings.NetworkInterval.ToString
        Me.chkRibbon.Checked = My.Settings.UseRibbonStyle
        Me.txtSearchEngine.Text = My.Settings.SearchEngine
        Me.chkCloseButton.Checked = My.Settings.CloseYAPMWithCloseButton
        Me.chkWarn.Checked = My.Settings.WarnDangerousActions
        Me.chkHideMinimized.Checked = My.Settings.HideWhenMinimized
        Me.txtTrayInterval.Text = My.Settings.TrayInterval.ToString
        Me.txtSysInfoInterval.Text = My.Settings.SystemInterval.ToString
        Me.chkWintrust.Checked = My.Settings.AutomaticWintrust
        Me.chkHideClosed.Checked = My.Settings.HideWhenClosed
        Me.chkAutoOnline.Checked = My.Settings.AutomaticInternetInfos
        Me.chkUserGroup.Checked = My.Settings.ShowUserGroupDomain
        Me.chkStatusBar.Checked = My.Settings.ShowStatusBar
        Me.txtJobInterval.Text = My.Settings.JobInterval.ToString
        Me.chkFixedTab.Checked = My.Settings.ShowFixedTab
        Me.chkUpdateAlpha.Checked = My.Settings.UpdateAlpha
        Me.chkUpdateBeta.Checked = My.Settings.UpdateBeta
        Me.chkUpdateAuto.Checked = My.Settings.UpdateAuto
        Me.txtUpdateServer.Text = My.Settings.UpdateServer
        Me.chkClassicMsgbox.Checked = My.Settings.ShowClassicMessageBoxes
        Me.chkClassicMsgbox.Enabled = False ' cEnvironment.SupportsTaskDialog
        Me.valCoefRemote.Value = My.Settings.CoefTimeMul

        If My.Settings.HistorySize > 0 Then
            Me.bufferSize.Value = CInt(My.Settings.HistorySize / 1024)
            Me.chkUnlimitedBuf.Checked = False
        Else
            Me.bufferSize.Value = 0
            Me.chkUnlimitedBuf.Checked = True
        End If

        ' Fill in list of main tabs
        For Each t As TabPage In _frmMain._tab.TabPages
            Me.cbShownTab.Items.Add(t.Text)
        Next
        Me.cbShownTab.Text = My.Settings.FixedTab
        Me.cbShownTab.Enabled = Me.chkFixedTab.Checked

        ' Add items of "Highlighting listviews" in saved order
        Me.lvHighlightingOther.Items.Clear()
        Dim s() As ListViewItem
        ReDim s(1)
        s(0) = New ListViewItem("Suspended thread") ' index = My.Settings.HighlightingPrioritySuspendedThread - 1
        s(1) = New ListViewItem("Relocated module")
        Me.lvHighlightingOther.Items.AddRange(s)
        '
        Me.lvHighlightingProcess.Items.Clear()
        Dim s2() As ListViewItem
        ReDim s2(6)
        s2(My.Settings.HighlightingPriorityCriticalProcess - 1) = New ListViewItem("Critical process")
        s2(My.Settings.HighlightingPriorityElevatedProcess - 1) = New ListViewItem("Elevated process")
        s2(My.Settings.HighlightingPriorityJobProcess - 1) = New ListViewItem("Process in job")
        s2(My.Settings.HighlightingPriorityServiceProcess - 1) = New ListViewItem("Service process")
        s2(My.Settings.HighlightingPriorityOwnedProcess - 1) = New ListViewItem("Owned process")
        s2(My.Settings.HighlightingPrioritySystemProcess - 1) = New ListViewItem("System process")
        s2(My.Settings.HighlightingPriorityBeingDebugged - 1) = New ListViewItem("Process being debugged")
        Me.lvHighlightingProcess.Items.AddRange(s2)

        ' Set colors of "Highlighting items"
        Call setColorOfHighlightingItems()

        ' Set checkboxes of "Highlighting items"
        Me.lvHighlightingOther.Items(0).Checked = My.Settings.EnableHighlightingSuspendedThread
        Me.lvHighlightingOther.Items(1).Checked = My.Settings.EnableHighlightingRelocatedModule
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPrioritySystemProcess - 1).Checked = My.Settings.EnableHighlightingSystemProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityServiceProcess - 1).Checked = My.Settings.EnableHighlightingServiceProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityOwnedProcess - 1).Checked = My.Settings.EnableHighlightingOwnedProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityJobProcess - 1).Checked = My.Settings.EnableHighlightingJobProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityElevatedProcess - 1).Checked = My.Settings.EnableHighlightingElevated
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityCriticalProcess - 1).Checked = My.Settings.EnableHighlightingCriticalProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityBeingDebugged - 1).Checked = My.Settings.EnableHighlightingBeingDebugged

        ' If not elevated under Vista or above, we cannot change 'replace taskmgr' state
        ' without elevation -> set cmdChangeTaskmgr as visible
        If cEnvironment.SupportsUac AndAlso Program.IsElevated = False Then
            Me.chkReplaceTaskmgr.Enabled = False
            Call cEnvironment.AddShieldToButton(Me.cmdChangeTaskmgr)
            Call SetToolTip(Me.cmdChangeTaskmgr, "This action requires elevation, and will automatically save settings")
            Me.cmdChangeTaskmgr.Visible = True
        End If

    End Sub

    ' Set colors of "Highlighting items"
    Private Sub setColorOfHighlightingItems()
        ' lvProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityBeingDebugged - 1).BackColor = My.Settings.HighlightingColorBeingDebugged
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityCriticalProcess - 1).BackColor = My.Settings.HighlightingColorCriticalProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityElevatedProcess - 1).BackColor = My.Settings.HighlightingColorElevatedProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityJobProcess - 1).BackColor = My.Settings.HighlightingColorJobProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityOwnedProcess - 1).BackColor = My.Settings.HighlightingColorOwnedProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPriorityServiceProcess - 1).BackColor = My.Settings.HighlightingColorServiceProcess
        Me.lvHighlightingProcess.Items(My.Settings.HighlightingPrioritySystemProcess - 1).BackColor = My.Settings.HighlightingColorSystemProcess
        ' lvThread
        Me.lvHighlightingOther.Items(0).BackColor = My.Settings.HighlightingColorSuspendedThread
        Me.lvHighlightingOther.Items(1).BackColor = My.Settings.HighlightingColorRelocatedModule
    End Sub

    Private Sub cmdDefaut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefaut.Click
        ' Defaut settings
        Me.chkStartTray.Checked = False
        Me.chkStart.Checked = False
        Me.chkReplaceTaskmgr.Checked = False
        Me.txtProcessIntervall.Value = 1000
        Me.txtServiceIntervall.Value = 2500
        Me.txtJobInterval.Value = 2000
        Me.chkTopMost.Checked = False
        Me.chkUserGroup.Checked = True
        Me.pctNewitems.BackColor = Color.FromArgb(-8323328)
        Me.pctDeletedItems.BackColor = Color.FromArgb(-49104)
        _newcolor = -8323328
        _deletedcolor = -49104
        Me.chkTrayIcon.Checked = True
        Me.chkHideMinimized.Checked = False
        Me.cbPriority.SelectedIndex = 3
        Me.txtTaskInterval.Value = 1000
        Me.txtNetworkInterval.Value = 1000
        Me.txtTrayInterval.Value = 1000
        Me.txtSysInfoInterval.Value = 1000
        Me.chkRibbon.Checked = True
        Me.txtSearchEngine.Text = "http://www.google.com/search?hl=en&q=ITEM"
        Me.chkCloseButton.Checked = True
        Me.chkWarn.Checked = True
        Me.chkHideClosed.Checked = False
        Me.chkUnlimitedBuf.Checked = False
        Me.chkAutoOnline.Checked = False
        Me.bufferSize.Value = 100
        Me.chkStatusBar.Checked = True
        Me.chkFixedTab.Checked = False
        Me.chkUpdateAlpha.Checked = False
        Me.chkUpdateBeta.Checked = False
        Me.chkUpdateAuto.Checked = False
        Me.txtUpdateServer.Text = "http://yaprocmon.sourceforge.net/update.xml"
        Me.valCoefRemote.Value = 250
        If Me.chkClassicMsgbox.Enabled Then
            Me.chkClassicMsgbox.Checked = True
        End If

        ' Now empty highlightings listviews, re-add items in default order and check them all
        Me.lvHighlightingProcess.Items.Clear()
        Me.lvHighlightingProcess.Items.Add("Process being debugged").BackColor = Color.FromArgb(255, 192, 255)
        Me.lvHighlightingProcess.Items.Add("Critical process").BackColor = Color.FromArgb(255, 128, 0)
        Me.lvHighlightingProcess.Items.Add("Elevated process").BackColor = Color.FromArgb(255, 192, 128)
        Me.lvHighlightingProcess.Items.Add("Process in job").BackColor = Color.FromArgb(192, 255, 192)
        Me.lvHighlightingProcess.Items.Add("Service process").BackColor = Color.FromArgb(192, 255, 255)
        Me.lvHighlightingProcess.Items.Add("Owned process").BackColor = Color.FromArgb(255, 255, 192)
        Me.lvHighlightingProcess.Items.Add("System process").BackColor = Color.FromArgb(192, 192, 255)
        Me.lvHighlightingOther.Items.Clear()
        Me.lvHighlightingOther.Items.Add("Suspended thread").BackColor = Color.FromArgb(255, 255, 192)
        Me.lvHighlightingOther.Items.Add("Relocated module").BackColor = Color.FromArgb(192, 255, 192)
        For Each it As ListViewItem In Me.lvHighlightingProcess.Items
            it.Checked = True
        Next
        For Each it As ListViewItem In Me.lvHighlightingOther.Items
            it.Checked = True
        Next

        ' Re-set column properties
        Pref.LoadListViewColumns(_frmMain.lvProcess, "COLmain_process")
        Pref.LoadListViewColumns(_frmMain.lvTask, "COLmain_task")
        Pref.LoadListViewColumns(_frmMain.lvServices, "COLmain_service")
        Pref.LoadListViewColumns(_frmMain.lvNetwork, "COLmain_network")

        ' Set colors of "Highlighting items"
        Call setColorOfHighlightingItems()
    End Sub

    Private Sub pctNewitems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pctNewitems.Click
        colDial.Color = Me.pctNewitems.BackColor
        colDial.ShowDialog()
        Me.pctNewitems.BackColor = colDial.Color
        _newcolor = colDial.Color.ToArgb
    End Sub

    Private Sub pctDeletedItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pctDeletedItems.Click
        colDial.Color = Me.pctDeletedItems.BackColor
        colDial.ShowDialog()
        Me.pctDeletedItems.BackColor = colDial.Color
        _deletedcolor = colDial.Color.ToArgb
    End Sub

    Private Sub chkTrayIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTrayIcon.CheckedChanged
        Me.chkHideMinimized.Enabled = chkTrayIcon.Checked
        If chkTrayIcon.Checked = False Then
            Me.chkHideMinimized.Checked = False
        End If
    End Sub

    Private Sub chkStartTray_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStartTray.CheckedChanged
        If Me.chkStartTray.Checked Then
            Me.chkTrayIcon.Enabled = False
            Me.chkTrayIcon.Checked = True
        Else
            Me.chkTrayIcon.Enabled = True
        End If
    End Sub

    Private Sub cmdResetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResetAll.Click
        If Misc.ShowMsg("Reset settings", Nothing, "Are you sure ?", MessageBoxButtons.YesNo, TaskDialogIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            My.Settings.Reset()
            Call cmdDefaut_Click(Nothing, Nothing)
            My.Settings.Save()
        End If
    End Sub

    Private Sub cmdMoveUpProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUpProcess.Click
        If Me.lvHighlightingProcess.SelectedItems Is Nothing OrElse Me.lvHighlightingProcess.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        If Me.lvHighlightingProcess.SelectedItems(0).Index = 0 Then
            Exit Sub
        End If

        Me.lvHighlightingProcess.BeginUpdate()
        MoveListViewItem(Me.lvHighlightingProcess, True)
        Me.lvHighlightingProcess.EndUpdate()
        Me.lvHighlightingProcess.Update()
    End Sub

    Private Sub cmdMoveDownProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDownProcess.Click
        If Me.lvHighlightingProcess.SelectedItems Is Nothing OrElse Me.lvHighlightingProcess.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        If Me.lvHighlightingProcess.SelectedItems(0).Index = Me.lvHighlightingProcess.Items.Count - 1 Then
            Exit Sub
        End If

        Me.lvHighlightingProcess.BeginUpdate()
        MoveListViewItem(Me.lvHighlightingProcess, False)
        Me.lvHighlightingProcess.EndUpdate()
        Me.lvHighlightingProcess.Update()
    End Sub

    Private Sub lvHighlightingProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHighlightingProcess.MouseDoubleClick
        If Me.lvHighlightingProcess.SelectedItems IsNot Nothing AndAlso Me.lvHighlightingProcess.SelectedItems.Count = 1 Then
            colDial.Color = Me.lvHighlightingProcess.SelectedItems(0).BackColor
            colDial.ShowDialog()
            Me.lvHighlightingProcess.SelectedItems(0).BackColor = colDial.Color
        End If
    End Sub

    Private Sub lvHighlightingThread_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHighlightingOther.MouseDoubleClick
        If Me.lvHighlightingOther.SelectedItems IsNot Nothing AndAlso Me.lvHighlightingOther.SelectedItems.Count = 1 Then
            colDial.Color = Me.lvHighlightingOther.SelectedItems(0).BackColor
            colDial.ShowDialog()
            Me.lvHighlightingOther.SelectedItems(0).BackColor = colDial.Color
        End If
    End Sub

    Private Sub cmdChangeTaskmgr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangeTaskmgr.Click
        ' Here we start YAPM elevated in order to change replace taskmgr or not
        Dim sCommandLine As String = PARAM_CHANGE_TASKMGR & _
                    CInt(Not (Me.chkReplaceTaskmgr.Checked)).ToString
        Call StartYapmElevated(sCommandLine)
        Program.Preferences.Save()
    End Sub

    ' Start YAPM elevated with a specific command line
    Private Function StartYapmElevated(ByVal cmdLine As String) As Boolean
        Dim startInfo As New ProcessStartInfo()
        With startInfo
            .UseShellExecute = True
            .WorkingDirectory = Environment.CurrentDirectory
            .FileName = Application.ExecutablePath
            .Verb = "runas"
            .Arguments = cmdLine
            .WindowStyle = ProcessWindowStyle.Normal
        End With

        Try
            Dim cP As Process = Process.Start(startInfo)
            If cP IsNot Nothing Then

                ' Wait than the process ended
                Native.Api.NativeFunctions.WaitForSingleObject(cP.Handle, Native.Api.NativeConstants.WAIT_INFINITE)

                ' Here we know that the process has ended, we retrieve the ExitCode
                Dim exCode As Program.RequestReplaceTaskMgrResult
                exCode = CType(cP.ExitCode, RequestReplaceTaskMgrResult)
                If exCode = RequestReplaceTaskMgrResult.NotReplaceSuccess Then
                    Me.chkReplaceTaskmgr.Checked = False
                ElseIf exCode = RequestReplaceTaskMgrResult.ReplaceSuccess Then
                    Me.chkReplaceTaskmgr.Checked = True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub chkFixedTab_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFixedTab.CheckedChanged
        Me.cbShownTab.Enabled = Me.chkFixedTab.Checked
    End Sub

    Private Sub cmdUpdateCheckNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateCheckNow.Click
        ' Check for updates manually
        ' No silent mode, so it will cause a messagebox to be displayed
        My.Settings.UpdateAlpha = Me.chkUpdateAlpha.Checked
        My.Settings.UpdateBeta = Me.chkUpdateBeta.Checked
        Program.Updater.CheckUpdates(False)
    End Sub
End Class