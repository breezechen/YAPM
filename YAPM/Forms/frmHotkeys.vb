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

Imports System.Runtime.InteropServices

Public Class frmHotkeys

    Private atxtKey As Integer = -1

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetWindowTheme(lv.Handle, "explorer", Nothing)
    End Sub

    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
        gp.Visible = True
    End Sub

    Private Sub cmdKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKO.Click
        gp.Visible = False
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        gp.Visible = False

        ' Add shortcut
        Dim i As Integer = Me.cbAction.SelectedIndex + 1

        If i <= 0 Then Exit Sub

        Dim k1 As Integer = -1
        Dim k2 As Integer = -1
        Dim k3 As Integer = atxtKey

        If Me.chkCtrl.Checked Then
            k1 = cShortcut.ShorcutKeys.VK_CONTROL
            If Me.chkShift.Checked Then
                k2 = cShortcut.ShorcutKeys.VK_SHIFT
            ElseIf Me.chkAlt.Checked Then
                k2 = cShortcut.ShorcutKeys.VK_MENU
            End If
        ElseIf Me.chkShift.Checked Then
            k1 = cShortcut.ShorcutKeys.VK_SHIFT
            If Me.chkAlt.Checked Then
                k2 = cShortcut.ShorcutKeys.VK_MENU
            End If
        ElseIf Me.chkAlt.Checked Then
            k1 = cShortcut.ShorcutKeys.VK_MENU
        End If

        If k1 + k2 + k3 = -3 Then Exit Sub

        Dim ht As New cShortcut(i, k3, k2, k1)
        If frmMain.emHotkeys.AddHotkey(ht) Then
            ' Add hotkey
            Dim skeys As String = CType(k1, cShortcut.ShorcutKeys).ToString & " + " & _
                CType(k2, cShortcut.ShorcutKeys).ToString & " + " & _
                CType(k3, cShortcut.ShorcutKeys).ToString
            Dim it As New ListViewItem(skeys)
            it.Tag = ht
            it.SubItems.Add(Me.cbAction.Text)
            it.ImageKey = "default"
            Me.lv.Items.Add(it)
        End If
    End Sub

    Private Sub txtKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKey.KeyDown
        atxtKey = e.KeyCode
        Me.lblKey.Text = CType(atxtKey, cShortcut.ShorcutKeys).ToString
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                ' Remove or ?
                Dim sKey As String = CType(it.Tag, cShortcut).Key
                If frmMain.emHotkeys.RemoveHotKey(sKey) Then
                    it.Remove()
                End If
            End If
        Next
    End Sub

    ' Can't check more than 2 boxes
    Private Sub chkCtrl_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCtrl.CheckedChanged
        If chkCtrl.Checked Then
            If chkAlt.Checked And chkShift.Checked Then
                chkAlt.Checked = False
            End If
        End If
    End Sub
    Private Sub chkShift_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShift.CheckedChanged
        If chkShift.Checked Then
            If chkCtrl.Checked And chkAlt.Checked Then
                chkAlt.Checked = False
            End If
        End If
    End Sub
    Private Sub chkAlt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAlt.CheckedChanged
        If chkAlt.Checked Then
            If chkCtrl.Checked And chkShift.Checked Then
                chkShift.Checked = False
            End If
        End If
    End Sub

    Private Sub EnableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                CType(it.Tag, cShortcut).Enabled = True
                it.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                CType(it.Tag, cShortcut).Enabled = False
                it.ForeColor = Color.Gray
            End If
        Next
    End Sub
End Class