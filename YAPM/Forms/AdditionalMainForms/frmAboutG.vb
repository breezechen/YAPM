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
Imports Common.Misc

Public Class frmAboutG

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub lnklblSF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnklblSF.LinkClicked
        cFile.ShellOpenFile("http://sourceforge.net/projects/yaprocmon/", Me.Handle)
    End Sub

    Private Sub frmAboutG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        SetToolTip(Me.cmdLicense, "Display license of YAPM")
        SetToolTip(Me.btnOK, "Close this window")
        SetToolTip(Me.lnklblSF, "Visit YAPM webpage on sourceforge.net")
        SetToolTip(Me.lnkWebsite,"Visit YAPM website")

        Me.lblVersion.Text = My.Application.Info.Version.ToString

    End Sub

    Private Sub lblMe_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblMe.LinkClicked
        cFile.ShellOpenFile("mailto:YetAnotherProcessMonitor@gmail.com", Me.Handle)
    End Sub

    Private Sub cmdLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLicense.Click
        Dim frm As New frmAbout
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub lblRibbon_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblRibbon.LinkClicked
        cFile.ShellOpenFile("http://www.codeproject.com/KB/toolbars/WinFormsRibbon.aspx", Me.Handle)
    End Sub

    Private Sub lblFugueIcons_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblFugueIcons.LinkClicked
        cFile.ShellOpenFile("http://www.pinvoke.com/", Me.Handle)
    End Sub

    Private Sub lblShareVB_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblShareVB.LinkClicked
        cFile.ShellOpenFile("http://www.sharevb.net/", Me.Handle)
    End Sub

    Private Sub lblTaskDialog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblTaskDialog.LinkClicked
        cFile.ShellOpenFile("http://www.codeproject.com/KB/vista/TaskDialogWinForms.aspx", Me.Handle)
    End Sub

    Private Sub lblVistaMenu_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblVistaMenu.LinkClicked
        cFile.ShellOpenFile("http://wyday.com/opensource.php", Me.Handle)
    End Sub

    Private Sub lnkWebsite_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkWebsite.LinkClicked
        cFile.ShellOpenFile("http://sourceforge.net/projects/yaprocmon/", Me.Handle)
    End Sub

    Private Sub lnkMarcel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkMarcel.LinkClicked
        cFile.ShellOpenFile("http://www.codeproject.com/Members/marcel-heeremans", Me.Handle)
    End Sub
End Class