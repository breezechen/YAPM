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

Public Class frmAboutG

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub lnklblSF_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnklblSF.LinkClicked
        cFile.ShellOpenFile("http://sourceforge.net/projects/yaprocmon/", Me.Handle)
    End Sub

    Private Sub frmAboutG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetWindowTheme(Me.lv.Handle, "explorer", Nothing)

        Try
            Me.lblVersion.Text = My.Application.Info.Version.ToString
            Dim it As New ListViewItem("YAPM.exe")
            it.SubItems.Add(System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion)
            Dim it2 As New ListViewItem("System.Windows.Forms.Ribbon.dll")
            it2.SubItems.Add(System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\System.Windows.Forms.Ribbon.dll").FileVersion)
            Dim it3 As New ListViewItem("MemoryHexEditor.dll")
            it3.SubItems.Add(System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\MemoryHexEditor.dll").FileVersion)
            Dim it4 As New ListViewItem("CoreFunc.dll")
            it4.SubItems.Add(System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\CoreFunc.dll").FileVersion)
            Dim it5 As New ListViewItem("Providers.dll")
            it5.SubItems.Add(System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\Providers.dll").FileVersion)
            Dim it6 As New ListViewItem("TaskDialog.dll")
            it6.SubItems.Add(System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\TaskDialog.dll").FileVersion)
            Me.lv.Items.Add(it)
            Me.lv.Items.Add(it2)
            Me.lv.Items.Add(it3)
            Me.lv.Items.Add(it4)
            Me.lv.Items.Add(it5)
            Me.lv.Items.Add(it6)
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub lblMe_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblMe.LinkClicked
        cFile.ShellOpenFile("mailto:YetAnotherProcessMonitor@gmail.com", Me.Handle)
    End Sub

    Private Sub cmdLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLicense.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub lblRibbon_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblRibbon.LinkClicked
        cFile.ShellOpenFile("http://www.codeproject.com/KB/toolbars/WinFormsRibbon.aspx", Me.Handle)
    End Sub

    Private Sub lblFugueIcons_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblFugueIcons.LinkClicked
        cFile.ShellOpenFile("http://www.pinvoke.com/", Me.Handle)
    End Sub

    Private Sub lblShareVB_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblShareVB.LinkClicked
        cFile.ShellOpenFile("http://www.vbfrance.com/codes/LISTER-HANDLES-FICHIERS-CLE-REGISTRES-OUVERTS-PROGRAMME-NT_39333.aspx", Me.Handle)
    End Sub

    Private Sub lblTaskDialog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblTaskDialog.LinkClicked
        cFile.ShellOpenFile("http://www.codeproject.com/KB/vista/TaskDialogWinForms.aspx", Me.Handle)
    End Sub
End Class