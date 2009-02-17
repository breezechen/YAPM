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

Public Class frmPreferences

    '<!--This file is the config file of YAPM. You should not manually edit it.-->
    '<yapm>
    '	<config>
    '		<procintervall>2000</procintervall>
    '		<serviceintervall>10000</serviceintervall>
    '		<startup>false</startup>
    '		<starthidden>false</starthidden>
    '		<lang>english</lang>
    '       <topmost>false</topmost>
    '       <firsttime>firsttime</firsttime>
    '       <detailshidden>detailshidden</detailshidden>
    '	</config>
    '</yapm>

    Private Declare Function URLDownloadToFile Lib "urlmon" Alias "URLDownloadToFileA" (ByVal pCaller As Integer, ByVal szURL As String, ByVal szFileName As String, ByVal dwReserved As Integer, ByVal lpfnCB As Integer) As Integer
    Private Declare Function DoFileDownload Lib "shdocvw" (ByVal lpszFile As String) As Integer

    Private Sub cmdQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        frmMain.timerProcess.Interval = frmMain.Pref.procIntervall
        frmMain.timerTask.Interval = frmMain.Pref.procIntervall
        frmMain.timerServices.Interval = frmMain.Pref.serviceIntervall
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        ' Save
        With frmMain.Pref
            .serviceIntervall = CInt(Val(Me.txtServiceIntervall.Text))
            .procIntervall = CInt(Val(Me.txtProcessIntervall.Text))
            .startup = CBool(Me.chkStart.Checked)
            .startHidden = CBool(Me.chkStartTray.Checked)
            .replaceTaskMgr = CBool(Me.chkReplaceTaskmgr.Checked)
            .topmost = CBool(Me.chkTopMost.Checked)
            .detailsHidden = CBool(Me.chkHideDetails.Checked)
            .Apply()
            Call mdlMisc.StartWithWindows(.startup)
            Call mdlMisc.ReplaceTaskmgr(.replaceTaskMgr)
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

        Me.txtUpdate.Text = "Click on 'Check if YAPM is up to date' to check if a new version is available."
        With frmMain
            .SetToolTip(Me.chkReplaceTaskmgr, "Replace taskmgr (it is safe).")
            .SetToolTip(Me.chkHideDetails, "Start YAPM with details panel hidden.")
            .SetToolTip(Me.chkStart, "Start YAPM on Windows startup.")
            .SetToolTip(Me.chkStartTray, "Start YAPM hidden (only in tray system).")
            .SetToolTip(Me.txtProcessIntervall, "Set intervall (milliseconds) between two refreshments of processes list.")
            .SetToolTip(Me.txtServiceIntervall, "Set intervall (milliseconds) between two refreshments of services list.")
            .SetToolTip(Me.cmdSave, "Save configuration.")
            .SetToolTip(Me.cmdQuit, "Quit without saving.")
            .SetToolTip(Me.cmdDefaut, "Set default configuration.")
            .SetToolTip(Me.chkTopMost, "Start YAPM topmost.")
            .SetToolTip(Me.cmdCheckUpdate, "Check if new updates are availables.")
            .SetToolTip(Me.cmdDownload, "Download last update of YAPM from sourceforge.net.")
        End With

        ' Set control's values
        With frmMain.Pref
            Me.txtServiceIntervall.Text = CStr(.serviceIntervall)
            Me.txtProcessIntervall.Text = CStr(.procIntervall)
            Me.chkHideDetails.Checked = .detailsHidden
            Me.chkStart.Checked = .startup
            Me.chkStartTray.Checked = .startHidden
            Me.chkReplaceTaskmgr.Checked = .replaceTaskMgr
            Me.chkTopMost.Checked = .topmost
        End With

    End Sub

    Private Sub cmdDefaut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefaut.Click
        ' Defaut settings
        Me.chkStartTray.Checked = False
        Me.chkStart.Checked = False
        Me.chkHideDetails.Checked = True
        Me.chkReplaceTaskmgr.Checked = False
        Me.txtProcessIntervall.Text = CStr(frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES)
        Me.txtServiceIntervall.Text = CStr(frmMain.DEFAULT_TIMER_INTERVAL_SERVICES)
        Me.chkTopMost.Checked = False
    End Sub

    Private Sub cmdCheckUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckUpdate.Click
        MsgBox("YAPM will connect to Internet and will check if new updates are availables.", MsgBoxStyle.Information, "Check for an update")
        If checkUpdate() = False Then
            MsgBox("Cannot connect to Internet or cannot retrieve informations.", MsgBoxStyle.Exclamation, "Error")
        End If
    End Sub

    Private Function checkUpdate() As Boolean
        ' Check if new updates are availables
        ' 1) Download source code of download page on sourceforge.net
        ' 2) Parse code to retrieve last versiob
        ' 3) Display results
        Try
            Dim cVersion As Integer = 0
            With My.Application.Info.Version
                cVersion = .Major * 10000 + .Build * 1000 + .Minor * 100 + .MinorRevision
            End With
            Dim lVersion As Integer = 0
            Dim sInfo As String = vbNullString

            Dim s As String
            s = "Downloading informations on sourceforge.net webpage..."
            Me.txtUpdate.Text = s
            My.Application.DoEvents()

            'download code
            Dim source As String = mdlInternet.DownloadPage("https://sourceforge.net/project/platformdownload.php?group_id=244697")
            If source.Length = 0 Then Return False

            s = "Retrieve last version number from downloaded informations..."
            Me.txtUpdate.Text = Me.txtUpdate.Text & vbNewLine & s
            My.Application.DoEvents()

            ' parse code, retrive last update info and if necessary changelog

            Dim x As Integer = InStr(source, "Last version : ", CompareMethod.Binary)
            Dim x2 As Integer = InStr(x + 1, source, "</p>", CompareMethod.Binary)
            If x = 0 Or x2 = 0 Then Return False

            Dim sVers As String = source.Substring(x + 14, x2 - x - 15)
            Dim sV As String() = Split(sVers, ".")
            lVersion = CInt(Val(sV(0)) * 10000 + Val(sV(1)) * 1000 + Val(sV(2)) * 1000 + Val(sV(3)) * 100 + Val(sV(4)))


            s = "Last version is : " & lVersion & vbNewLine
            s &= "Your version is : " & cVersion & vbNewLine

            If lVersion > cVersion Then
                s &= "Result : A NEW UPDATE IS AVAILABLE" & vbNewLine & vbNewLine
                s &= "Informations about new version : " & vbNewLine & sInfo
            Else
                s &= "Result : YOUR VERSION IS UP TO DATE"
            End If

            Me.txtUpdate.Text = Me.txtUpdate.Text & vbNewLine & s
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click

        ' Download webpage and extract URL
        Dim tofind As String = "<h3 class=" & Chr(34) & "downloadbar" & Chr(34) & ">"
        Dim source As String = mdlInternet.DownloadPage("https://sourceforge.net/project/platformdownload.php?group_id=244697")
        If source.Length = 0 Then
            MsgBox("Failed...", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        Dim x As Integer = InStr(source, tofind, CompareMethod.Binary)
        Dim x2 As Integer = InStr(x + 10, source, "onclick", CompareMethod.Binary)
        If x = 0 Or x2 = 0 Then
            MsgBox("Failed...", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        Dim sUrl As String = source.Substring(x + 32, x2 - x - 35)
        Try
            If Len(sUrl) = 0 Then
                MsgBox("Failed...", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("Failed...", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try


        frmMain.saveDial.Filter = "Zip file (*.zip)|*.zip"
        frmMain.saveDial.Title = "Save last update package"
        Dim r As DialogResult = frmMain.saveDial.ShowDialog()
        Dim s As String = frmMain.saveDial.FileName
        If r = Windows.Forms.DialogResult.OK Then

            Dim down As New cDownload(sUrl, frmMain.saveDial.FileName)
            Dim frm As New frmDownload
            With frm
                .DownloadObject = down
                .StartDownload(frmMain.saveDial.FileName)
                .TopMost = True
                .ShowDialog()
            End With
        End If
    End Sub

    Private Sub chkReplaceTaskmgr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReplaceTaskmgr.Click
        If Me.chkReplaceTaskmgr.Checked Then
            MsgBox("This option simply create a key in registry, that's why it is safe to do it." & vbNewLine & "But remember to disable this option if you decide to move (or delete) YAPM executable.", MsgBoxStyle.Information, "Warning")
        End If
    End Sub
End Class