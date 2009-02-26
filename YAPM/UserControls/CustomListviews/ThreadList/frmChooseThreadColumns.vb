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

Public Class frmChooseThreadColumns

    Private theListview As threadList

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Public Sub SetLv(ByRef aLv As threadList)
        theListview = aLv
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        ' Remove all columns
        For x As Integer = theListview.Columns.Count - 1 To 1 Step -1
            theListview.Columns.Remove(theListview.Columns(x))
        Next

        For Each it As ListViewItem In theListview.Items
            it.SubItems.Clear()
            Dim subit() As ListViewItem.ListViewSubItem
            ReDim subit(Me.lv.CheckedItems.Count)

            For z As Integer = 0 To UBound(subit) - 1
                subit(z) = New ListViewItem.ListViewSubItem
            Next

            it.SubItems.AddRange(subit)
        Next

        ' Add new columns
        For Each it As ListViewItem In Me.lv.CheckedItems
            theListview.Columns.Add(it.Text, 90)
        Next

        frmMain.timerProcess.Enabled = True
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        frmMain.timerProcess.Enabled = True
        Me.Close()
    End Sub

    Private Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = True
        Next
    End Sub

    Private Sub btnUnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelAll.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = False
        Next
    End Sub

    Private Sub frmChooseProcessColumns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetWindowTheme(Me.lv.Handle, "explorer", Nothing)

        frmMain.timerProcess.Enabled = False

        For Each s As String In cThread.GetAvailableProperties
            Dim it As New ListViewItem(s)

            ' Checked displayed columns
            For x As Integer = 0 To theListview.Columns.Count - 1
                If s = theListview.Columns(x).Text.Replace("< ", "").Replace("> ", "") Then
                    it.Checked = True
                    Exit For
                End If
            Next

            Me.lv.Items.Add(it)
        Next
    End Sub
End Class