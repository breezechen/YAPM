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

Public Class frmPreferences

    Private _newcolor As Integer
    Private _deletedcolor As Integer

    Private Sub cmdQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        _frmMain.timerProcess.Interval = My.Settings.ProcessInterval
        _frmMain.timerTask.Interval = My.Settings.TaskInterval
        _frmMain.timerNetwork.Interval = My.Settings.NetworkInterval
        _frmMain.timerServices.Interval = My.Settings.ServiceInterval
        _frmMain.timerTrayIcon.Interval = My.Settings.TrayInterval
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
        My.Settings.UseRibbonStyle = Me.chkRibbon.Checked
        My.Settings.SearchEngine = Me.txtSearchEngine.Text
        My.Settings.CloseYAPMWithCloseButton = Me.chkCloseButton.Checked
        My.Settings.WarnDangerousActions = Me.chkWarn.Checked
        My.Settings.HideWhenMinimized = Me.chkHideMinimized.Checked
        My.Settings.TrayInterval = CInt(Val(Me.txtTrayInterval.Text))
        My.Settings.SystemInterval = CInt(Val(Me.txtSysInfoInterval.Text))
        My.Settings.AutomaticInternetInfos = Me.chkAutoOnline.Checked
        If Me.chkUnlimitedBuf.Checked Then
            My.Settings.HistorySize = -1
        Else
            My.Settings.HistorySize = CInt(Me.bufferSize.Value * 1024)
        End If

        Call mdlMisc.StartWithWindows(My.Settings.WindowsStartup)
        Call mdlMisc.ReplaceTaskmgr(My.Settings.ReplaceTaskmgr)

        Program.Preferences.Save()
        Program.Preferences.Apply()

        If Not (_oldRibbonStyle = My.Settings.UseRibbonStyle) Then
            Dim ret As Integer
            If Not (IsWindowsVista()) Then
                ret = MsgBox("The new menu style will be displayed next time you start YAPM. Do you want to exit YAPM now ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Menu style has changed")
            Else
                ret = ShowVistaMessage(Me.Handle, "Menu style has changed", "The new menu style will be displayed next time you start YAPM.", "Do you want to exit YAPM now ?", TaskDialogCommonButtons.Yes Or TaskDialogCommonButtons.No, TaskDialogIcon.Information)
            End If
            If ret = DialogResult.Yes Then
                Program.ExitYAPM()
            End If
        End If

        MsgBox("Save is OK !", MsgBoxStyle.OkOnly, "Preferences")

    End Sub

    Private Sub frmPreferences_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        Me.txtUpdate.Text = "Click on 'Check if YAPM is up to date' to check if a new version is available."
        With _frmMain
            .SetToolTip(Me.chkReplaceTaskmgr, "Replace taskmgr (it is safe).")
            .SetToolTip(Me.chkStart, "Start YAPM on Windows startup.")
            .SetToolTip(Me.chkStartTray, "Start YAPM hidden (only in tray system).")
            .SetToolTip(Me.txtProcessIntervall, "Set interval (milliseconds) between two refreshments of process list.")
            .SetToolTip(Me.txtServiceIntervall, "Set interval (milliseconds) between two refreshments of service list.")
            .SetToolTip(Me.cmdSave, "Save configuration.")
            .SetToolTip(Me.cmdQuit, "Quit without saving.")
            .SetToolTip(Me.cmdDefaut, "Set default configuration.")
            .SetToolTip(Me.chkTopMost, "Start YAPM topmost.")
            .SetToolTip(Me.cmdCheckUpdate, "Check if new updates are availables.")
            .SetToolTip(Me.cmdDownload, "Download last update of YAPM from sourceforge.net.")
            .SetToolTip(Me.pctDeletedItems, "Color of deleted items.")
            .SetToolTip(Me.pctNewitems, "Color of new items.")
            .SetToolTip(Me.chkTrayIcon, "Show tray icon.")
            .SetToolTip(Me.cbPriority, "Priority of YAPM.")
            .SetToolTip(Me.txtTaskInterval, "Set interval (milliseconds) between two refreshments of task list.")
            .SetToolTip(Me.txtNetworkInterval, "Set interval (milliseconds) between two refreshments of network list.")
            .SetToolTip(Me.chkRibbon, "Show ribbon style menu.")
            .SetToolTip(Me.txtSearchEngine, "Search engine for 'Internet search'. Use the keyword ITEM to specify the item name to search.")
            .SetToolTip(Me.chkCloseButton, "Close YAPM when close button is pressed (minimize to tray if not checked).")
            .SetToolTip(Me.chkWarn, "Warn user for all (potentially) dangerous actions.")
            .SetToolTip(Me.chkHideMinimized, "Hide main form when minimized.")
            .SetToolTip(Me.txtTrayInterval, "Set interval (milliseconds) between two refreshments of tray icon.")
            .SetToolTip(Me.txtSysInfoInterval, "Set interval (milliseconds) between two refreshments of system informations.")
            .SetToolTip(Me.chkHideClosed, "Hide YAPM when user click on 'close' button")
            .SetToolTip(Me.chkUnlimitedBuf, "No size limit for history")
            .SetToolTip(Me.bufferSize, "Size of the buffer used to save history of statistics of one process (KB). The change of this value will be applied on the next start of YAPM.")
            .SetToolTip(Me.chkAutoOnline, "Automatically retrieve online description of a process/service when detailed form is showned")
        End With

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
        Me.chkHideClosed.Checked = My.Settings.HideWhenClosed
        Me.chkAutoOnline.Checked = My.Settings.AutomaticInternetInfos
        If My.Settings.HistorySize > 0 Then
            Me.bufferSize.Value = CInt(My.Settings.HistorySize / 1024)
            Me.chkUnlimitedBuf.Checked = False
        Else
            Me.bufferSize.Value = 0
            Me.chkUnlimitedBuf.Checked = True
        End If

    End Sub

    Private Sub cmdDefaut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefaut.Click
        ' Defaut settings
        Me.chkStartTray.Checked = False
        Me.chkStart.Checked = False
        Me.chkReplaceTaskmgr.Checked = False
        Me.txtProcessIntervall.Text = "1000"
        Me.txtServiceIntervall.Text = "2500"
        Me.chkTopMost.Checked = False
        Me.pctNewitems.BackColor = Color.FromArgb(-8323328)
        Me.pctDeletedItems.BackColor = Color.FromArgb(-49104)
        _newcolor = -8323328
        _deletedcolor = -49104
        Me.chkTrayIcon.Checked = True
        Me.chkHideMinimized.Checked = False
        Me.cbPriority.SelectedIndex = 1
        Me.txtTaskInterval.Text = "1000"
        Me.txtNetworkInterval.Text = "1000"
        Me.txtTrayInterval.Text = "1000"
        Me.txtSysInfoInterval.Text = "1000"
        Me.chkRibbon.Checked = True
        Me.txtSearchEngine.Text = "http://www.google.com/search?hl=en&q=ITEM"
        Me.chkCloseButton.Checked = True
        Me.chkWarn.Checked = True
        Me.chkHideClosed.Checked = False
        Me.chkUnlimitedBuf.Checked = False
        Me.chkAutoOnline.Checked = False

        Me.bufferSize.Value = 100
    End Sub

    Private Delegate Sub setUpdateText(ByVal s As String, ByVal b As Boolean, ByVal b2 As Boolean)
    Private Structure degObj
        Public deg As setUpdateText
        Public ctrl As Control
        Public Sub New(ByVal d As setUpdateText, ByVal ctr As Control)
            deg = d
            ctrl = ctr
        End Sub
    End Structure

    Private Sub impSetUpdateText(ByVal s As String, ByVal b As Boolean, ByVal b2 As Boolean)
        If b = False Then
            Me.txtUpdate.Text &= vbNewLine & s & vbNewLine
        Else
            If b2 = False Then
                If Not (IsWindowsVista()) Then
                    MsgBox("Cannot connect to Internet or cannot retrieve informations.", MsgBoxStyle.Exclamation, "Error")
                Else
                    ShowVistaMessage(Me.Handle, "Error while checking update", "Cannot connect to Internet or cannot retrieve informations.", , TaskDialogCommonButtons.Ok, TaskDialogIcon.Error)
                End If
            End If
        End If
    End Sub

    Private Function checkUpdate(ByVal useless As Object) As Boolean
        ' Check if new updates are availables
        ' 1) Download source code of download page on sourceforge.net
        ' 2) Parse code to retrieve last versiob
        ' 3) Display results

        Dim r As Boolean = True
        Dim dObj As degObj = DirectCast(useless, degObj)

        Try
            Dim cVersion As Integer = 0
            With My.Application.Info.Version
                cVersion = .Major * 10000 + .Build * 1000 + .Minor * 100 + .MinorRevision
            End With
            Dim lVersion As Integer = 0
            Dim sInfo As String = vbNullString

            Dim s As String
            s = "Downloading informations on sourceforge.net webpage..."
            If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                dObj.ctrl.Invoke(dObj.deg, s, False, False)
            ' _inv.Invoke(s, False, False)

            'download code
            Dim source As String = mdlInternet.DownloadPage("http://yaprocmon.sourceforge.net/")
            If source.Length = 0 Then Return False

            s = "Retrieve last version number from downloaded informations..."
            If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                dObj.ctrl.Invoke(dObj.deg, s, False, False)

            ' parse code, retrive last update info
            ' we have to get this line : <META content="2.0.1" name=yapm_version>

            Dim x As Integer = InStr(source, "name=yapm_version>", CompareMethod.Binary)
            Dim x2 As Integer
            If x > 30 Then
                x2 = InStr(x - 30, source, "content=", CompareMethod.Binary)
            End If
            If x = 0 Or x2 = 0 Then Return False

            Dim sVers As String = source.Substring(x2 + 8, x - x2 - 11)
            Dim sV As String() = Split(sVers, ".")
            lVersion = CInt(Val(sV(0)) * 10000 + Val(sV(1)) * 1000 + Val(sV(2)) * 1000 + Val(sV(3)) * 100)

            s = "Last version is : " & lVersion & vbNewLine
            s &= "Your version is : " & cVersion & vbNewLine

            If lVersion > cVersion Then
                s &= "Result : A NEW UPDATE IS AVAILABLE" & vbNewLine & vbNewLine
                s &= "Informations about new version : " & vbNewLine & sInfo
            Else
                s &= "Result : YOUR VERSION IS UP TO DATE"
            End If

            If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                dObj.ctrl.Invoke(dObj.deg, s, False, False)
        Catch ex As Exception
            r = False
        End Try

        If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
            dObj.ctrl.Invoke(dObj.deg, Nothing, True, r)

    End Function

    Private Sub cmdCheckUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckUpdate.Click
        If Not (IsWindowsVista()) Then
            MsgBox("YAPM will connect to Internet and will check if new updates are availables.", MsgBoxStyle.Information, "Check for an update")
        Else
            ShowVistaMessage(Me.Handle, "Check for an update", "YAPM will connect to Internet and will check if new updates are availables.", , TaskDialogCommonButtons.Ok, TaskDialogIcon.ShieldOk)
        End If

        Dim _inv As New setUpdateText(AddressOf impSetUpdateText)
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
               System.Threading.WaitCallback(AddressOf checkUpdate), New degObj(_inv, Me))
    End Sub


    Private Delegate Sub downloadUpdate(ByVal tObj As Object)
    Private Delegate Sub msgShowMessage()
    Private Delegate Sub startDownload(ByVal surl As String, ByVal path As String)
    Private Structure degObj2
        Public deg As msgShowMessage
        Public deg2 As startDownload
        Public ctrl As Control
        Public path As String
        Public Sub New(ByVal d As msgShowMessage, ByVal d2 As startDownload, ByVal ctr As Control, ByVal s As String)
            deg = d
            deg2 = d2
            ctrl = ctr
            path = s
        End Sub
    End Structure

    Private Sub impShowMessage()
        MsgBox("Failed...", MsgBoxStyle.Critical, "Error")
    End Sub

    Private Sub impStartDownload(ByVal surl As String, ByVal path As String)
        Dim down As New cDownload(surl, path)
        Dim frm As New frmDownload
        With frm
            .DownloadObject = down
            .StartDownload(path)
            .TopMost = True
            .Show()
        End With
    End Sub

    Private Sub fctDownloadUpdate(ByVal tObj As Object)

        Dim dObj As degObj2 = DirectCast(tObj, degObj2)

        ' Download webpage and extract URL
        Try
            Dim tofind As String = "<LI><A href=" & Chr(34) & "http://downloads"
            Dim source As String = mdlInternet.DownloadPage("http://yaprocmon.sourceforge.net/index.html")
            If source.Length = 0 Then
                If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                    dObj.ctrl.Invoke(dObj.deg)
                Exit Sub
            End If
            Dim x As Integer = InStr(source, tofind, CompareMethod.Binary)
            Dim x2 As Integer = InStr(x + 30, source, Chr(34), CompareMethod.Binary)
            If x = 0 Or x2 = 0 Then
                If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                    dObj.ctrl.Invoke(dObj.deg)
                Exit Sub
            End If

            Dim sUrl As String = source.Substring(x + 12, x2 - x - 13)
            Try
                If Len(sUrl) = 0 Then
                    If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                        dObj.ctrl.Invoke(dObj.deg)
                    Exit Sub
                End If
            Catch ex As Exception
                If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                    dObj.ctrl.Invoke(dObj.deg)
                Exit Sub
            End Try
            If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                dObj.ctrl.Invoke(dObj.deg2, sUrl, dObj.path)
        Catch ex As Exception
            If dObj.deg IsNot Nothing AndAlso dObj.ctrl.Created Then _
                dObj.ctrl.Invoke(dObj.deg)
        End Try

    End Sub

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click

        _frmMain.saveDial.Filter = "Zip file (*.zip)|*.zip"
        _frmMain.saveDial.Title = "Save last update package"
        Dim r As DialogResult = _frmMain.saveDial.ShowDialog()
        Dim s As String = _frmMain.saveDial.FileName

        If r = Windows.Forms.DialogResult.OK Then
            Dim _inv As New msgShowMessage(AddressOf impShowMessage)
            Dim _inv1 As New startDownload(AddressOf impStartDownload)
            Call Threading.ThreadPool.QueueUserWorkItem(New  _
                   System.Threading.WaitCallback(AddressOf fctDownloadUpdate), New degObj2(_inv, _inv1, Me, s))
        End If

    End Sub

    Private Sub chkReplaceTaskmgr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReplaceTaskmgr.Click
        If Me.chkReplaceTaskmgr.Checked Then
            If IsWindowsVista() Then
                ShowVistaMessage(Me.Handle, "Replace taskmgr by YAPM", "This option is safe", "This option simply create a key in registry, that's why it is safe to do it." & vbNewLine & "But remember to disable this option if you decide to move (or delete) YAPM executable.", TaskDialogCommonButtons.Ok, TaskDialogIcon.ShieldOk)
            Else
                MsgBox("This option simply create a key in registry, that's why it is safe to do it." & vbNewLine & "But remember to disable this option if you decide to move (or delete) YAPM executable.", MsgBoxStyle.OkOnly, "Warning")
            End If
        End If
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

    'Private Sub cmdResetLv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If MsgBox("Are you sure ?", MsgBoxStyle.YesNo, "Reset settings") = MsgBoxResult.Yes Then
    '        My.Settings.COLmain_handle = My.MySettings.Default.COLmain_handle
    '        My.Settings.COLmain_module = My.MySettings.Default.COLmain_module
    '        My.Settings.COLmain_network = My.MySettings.Default.COLmain_network
    '        My.Settings.COLmain_process = My.MySettings.Default.COLmain_process
    '        My.Settings.COLmain_service = My.MySettings.Default.COLmain_service
    '        My.Settings.COLmain_task = My.MySettings.Default.COLmain_task
    '        My.Settings.COLmain_thread = My.MySettings.Default.COLmain_thread
    '        My.Settings.COLmain_window = My.MySettings.Default.COLmain_window
    '        My.Settings.COLprocdetail_envvariable = My.MySettings.Default.COLprocdetail_envvariable
    '        My.Settings.COLprocdetail_handle = My.MySettings.Default.COLprocdetail_handle
    '        My.Settings.COLprocdetail_memory = My.MySettings.Default.COLprocdetail_memory
    '        My.Settings.COLprocdetail_module = My.MySettings.Default.COLprocdetail_module
    '        My.Settings.COLprocdetail_network = My.MySettings.Default.COLprocdetail_network
    '        My.Settings.COLprocdetail_privilege = My.MySettings.Default.COLprocdetail_privilege
    '        My.Settings.COLprocdetail_service = My.MySettings.Default.COLprocdetail_service
    '        My.Settings.COLprocdetail_thread = My.MySettings.Default.COLprocdetail_thread
    '        My.Settings.COLprocdetail_window = My.MySettings.Default.COLprocdetail_window
    '        My.Settings.Save()
    '    End If
    'End Sub

    Private Sub cmdResetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResetAll.Click
        If MsgBox("Are you sure ?", MsgBoxStyle.YesNo, "Reset settings") = MsgBoxResult.Yes Then
            My.Settings.Reset()
            Call cmdDefaut_Click(Nothing, Nothing)
            My.Settings.Save()
        End If
    End Sub
End Class