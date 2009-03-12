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

Public Class frmLog

    Private lCount As Integer = 0

    Private Sub timerRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerRefresh.Tick
        If lCount <> frmMain.log.LineCount Then
            Dim i As Integer = Me.txtLog.SelectionStart
            Dim z As Integer = frmMain.log.LineCount

            ' Add new lines
            For x As Integer = lCount + 1 To z
                Me.txtLog.Text &= frmMain.log.Line(x)
            Next x

            lCount = z

            Me.txtLog.SelectionStart = i
            Me.txtLog.ScrollToCaret()
        End If
    End Sub

    Private Sub butSaveLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveLog.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "log"
            Call My.Application.DoEvents()
            .ShowDialog()
        End With
    End Sub

    Private Sub frmLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mybase.Load
        Call timerRefresh_Tick(Nothing, Nothing)
    End Sub

    Private Sub butConfig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butConfig.Click
        '   Dim c As New cPrivileges(2364)
        '        Call cService.EnumServicesEx(Nothing)
    End Sub

End Class