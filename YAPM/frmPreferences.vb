Option Strict On

Public Class frmPreferences

    '<!--This file is the config file of YAPM. You should not manually edit it.-->
    '<yapm>
    '	<config>
    '		<procintervall>2000</procintervall>
    '		<serviceintervall>10000</serviceintervall>
    '		<startup>false</startup>
    '		<starthidden>false</starthidden>
    '		<lang>english</lang>
    '		<startjobs>true</startjobs>
    '		<startchkmodules>true</startchkmodules>
    '       <topmost>false</topmost>
    '	</config>
    '</yapm>

    Private Sub cmdQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        frmMain.timerProcess.Interval = frmMain.Pref.procIntervall
        frmMain.timerServices.Interval = frmMain.Pref.serviceIntervall
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        ' Save
        With frmMain.Pref
            .serviceIntervall = CInt(Val(Me.txtServiceIntervall.Text))
            .procIntervall = CInt(Val(Me.txtProcessIntervall.Text))
            .startJobs = CBool(Me.chkJobs.Checked)
            .startChkModules = CBool(Me.chkModules.Checked)
            .startup = CBool(Me.chkStart.Checked)
            .startHidden = CBool(Me.chkStartTray.Checked)
            .topmost = CBool(Me.chkTopMost.Checked)
            .Apply()
        End With

        ' Save XML
        Try
            Call frmMain.Pref.Save()
            MsgBox("Save is done.", MsgBoxStyle.Information, "Preferences")
        Catch ex As Exception
            '
        End Try

    End Sub

    Private Sub frmPreferences_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        With frmMain
            .SetToolTip(Me.chkJobs, "Start jobs processing on YAPM startup")
            .SetToolTip(Me.chkModules, "Check 'Retrieve modules/threads' option on YAPM startup")
            .SetToolTip(Me.chkStart, "Start YAPM on Windows startup")
            .SetToolTip(Me.chkStartTray, "Start YAPM hidden (only in tray system)")
            .SetToolTip(Me.txtProcessIntervall, "Set intervall (milliseconds) between two refreshments of processes list")
            .SetToolTip(Me.txtServiceIntervall, "Set intervall (milliseconds) between two refreshments of services list")
            .SetToolTip(Me.cmdSave, "Save configuration")
            .SetToolTip(Me.cmdQuit, "Quit without saving")
            .SetToolTip(Me.cmdDefaut, "Set default configuration")
            .SetToolTip(Me.chkTopMost, "Start YAPM topmost")
        End With

        ' Set control's values
        With frmMain.Pref
            Me.txtServiceIntervall.Text = CStr(.serviceIntervall)
            Me.txtProcessIntervall.Text = CStr(.procIntervall)
            Me.chkJobs.Checked = .startJobs
            Me.chkModules.Checked = .startChkModules
            Me.chkStart.Checked = .startup
            Me.chkStartTray.Checked = .startHidden
            Me.chkTopMost.Checked = .topmost
        End With

    End Sub

    Private Sub cmdDefaut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefaut.Click
        ' Defaut settings
        Me.chkStartTray.Checked = False
        Me.chkStart.Checked = False
        Me.chkModules.Checked = True
        Me.chkJobs.Checked = True
        Me.txtProcessIntervall.Text = CStr(frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES)
        Me.txtServiceIntervall.Text = CStr(frmMain.DEFAULT_TIMER_INTERVAL_SERVICES)
        Me.chkTopMost.Checked = False
    End Sub

End Class