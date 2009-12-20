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

Public Class frmLog

    'Private lCount As Integer = 0

    'Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick
    '    If lCount <> Program.Log.LineCount Then
    '        Dim i As Integer = Me.txtLog.SelectionStart
    '        Dim z As Integer = Program.Log.LineCount

    '        ' Add new lines
    '        For x As Integer = lCount + 1 To z
    '            Me.txtLog.Text &= Program.Log.Line(x)
    '        Next x

    '        lCount = z

    '        Me.txtLog.SelectionStart = i
    '        Me.txtLog.ScrollToCaret()
    '    End If
    'End Sub

    'Private Sub frmLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mybase.Load
    '    Call timerRefresh_Tick(Nothing, Nothing)
    'End Sub


    Private Sub frmLog_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Program.Log.ShowForm = False
        e.Cancel = True
    End Sub

    Private Sub frmLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Common.Misc.CloseWithEchapKey(Me)

        Call Native.Functions.Misc.SetTheme(Me.lv.Handle)
    End Sub

    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem.Click
        Dim frm As New frmSaveReport
        With frm
            .TopMost = _frmMain.TopMost
            .ListviewToSave = Me.lv
            .ShowDialog()
        End With
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Program.Log.Clear()
    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.TheContextMenu.Show(Me.lv, e.Location)
        End If
    End Sub
End Class