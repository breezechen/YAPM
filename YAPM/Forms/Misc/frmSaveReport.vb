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

Public Class frmSaveReport

    Private _path As String
    Private _lv As ListView

    Public Property ReportPath() As String
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
    Public Property ListviewToSave() As ListView
        Get
            Return _lv
        End Get
        Set(ByVal value As ListView)
            _lv = value
        End Set
    End Property

    ' Report has been succesfully saved
    Private Sub ReportSaved()
        Me.lblProgress.Text = "Saving done."
        Me.pgb.Value = Me.pgb.Maximum
        Me.cmdOK.Enabled = True
        Me.cmdOpenReport.Enabled = True
        Me.cmdGO.Enabled = True
        Misc.ShowMsg("Report", Nothing, "Saved report sucessfully.", MessageBoxButtons.OK, TaskDialogIcon.ShieldOk)
    End Sub

    ' Report saving failed
    Private Sub ReportFailed(ByVal ex As Exception)
        Me.pgb.Value = Me.pgb.Maximum
        Me.cmdOK.Enabled = True
        Me.cmdOK.Text = "Quit"
        Me.lblProgress.Text = "Saving failed"
        Me.cmdOpenReport.Enabled = False
        Me.cmdGO.Enabled = True
        Misc.ShowError(ex, "Could not save report")
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub cmdOpenReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenReport.Click
        Call cFile.ShellOpenFile(Me.ReportPath, Me.Handle)
    End Sub

    Private Sub frmSaveReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdGO, "Save the report")
        SetToolTip(Me.cmdOK, "Close this window")
        SetToolTip(Me.cmdOpenReport, "Open the report which was saved")

        Me.lblProgress.Text = "Waiting for save..."
        With Me.pgb
            .Maximum = 1
            .Minimum = 0
            .Value = 0
        End With
    End Sub

    Private Sub UpdateProgress(ByVal value As Integer)
        Me.pgb.Value = value
        Me.lblProgress.Text = "Computing item " & CStr(value) & "/" & CStr(Me.pgb.Maximum)
        Application.DoEvents()
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click
        Me.cmdGO.Enabled = False
        Me.cmdOK.Enabled = False
        Me.cmdOpenReport.Enabled = False
        Me.genericSaveReport(Me.ListviewToSave)
    End Sub

    ' Save what's displayed on a listview as a textfile
    Private Sub SaveListviewContentAsTxtFile(ByVal file As String, ByVal lv As ListView)

        ' New stream
        Dim stream As New System.IO.StreamWriter(file, False)

        Dim c As String = ""
        Dim it As ListViewItem
        Dim x As Integer = 0

        ' Add column names
        For Each col As ColumnHeader In lv.Columns
            c &= col.Text & vbTab
        Next
        stream.WriteLine(c)

        ' Add each item in listview
        For Each it In lv.Items
            Dim colNumber As Integer = 0
            c = ""
            For Each col As ColumnHeader In lv.Columns
                c &= it.SubItems(colNumber).Text & vbTab
                colNumber += 1
            Next
            stream.WriteLine(c)
            x += 1
            UpdateProgress(x)
        Next

        ' Close stream
        stream.Close()
        stream.Dispose()
    End Sub

    ' Save what's displayed on a listview as a csv file
    Private Sub SaveListviewContentAsCsvFile(ByVal file As String, ByVal lv As ListView)

        Dim tabCar As String = ";"

        ' New stream
        Dim stream As New System.IO.StreamWriter(file, False)

        Dim c As String = ""
        Dim it As ListViewItem
        Dim x As Integer = 0

        ' Add column names
        For Each col As ColumnHeader In lv.Columns
            c &= col.Text & tabCar
        Next
        stream.WriteLine(c)

        ' Add each item in listview
        For Each it In lv.Items
            Dim colNumber As Integer = 0
            c = ""
            For Each col As ColumnHeader In lv.Columns
                Dim theText As String = it.SubItems(colNumber).Text
                If InStr(theText, ";", CompareMethod.Binary) > 0 Then
                    theText = CChar(ChrW(34)) & theText & CChar(ChrW(34))
                End If
                c &= theText & tabCar
                colNumber += 1
            Next
            stream.WriteLine(c)
            x += 1
            UpdateProgress(x)
        Next

        ' Close stream
        stream.Close()
        stream.Dispose()
    End Sub

    ' Save what's displayed on a listview as a html file
    Private Sub SaveListviewContentAsHtmlFile(ByVal file As String, ByVal lv As ListView)

        ' Defines columns
        Dim totalWidth As Integer = 0
        For Each c As ColumnHeader In lv.Columns
            totalWidth += c.Width
        Next
        Dim col(lv.Columns.Count) As cHTML.HtmlColumnStructure
        Dim colCount As Integer = 0
        For Each c As ColumnHeader In lv.Columns
            With col(colCount)
                .sizePercent = CInt(100 * c.Width / totalWidth)
                .title = c.Text
            End With
            colCount += 1
        Next

        ' Defines title
        Dim title As String = lv.Items.Count.ToString & " result(s)"
        Dim _html As New cHTML(col, file, title)

        ' Write items
        Dim it As ListViewItem
        Dim x As Integer = 0
        For Each it In lv.Items
            Dim _lin(lv.Columns.Count) As String
            colCount = 0
            For Each c As ColumnHeader In lv.Columns
                _lin(colCount) = it.SubItems(colCount).Text
                colCount += 1
            Next
            _html.AppendLine(_lin)
            x += 1
            UpdateProgress(x)
        Next

        _html.ExportHTML()

    End Sub

    ' Save a report
    Private Sub genericSaveReport(ByVal lv As ListView)
        Try
            If Me.SaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = Me.SaveFileDialog.FileName

                ' Create file report
                If Len(s) > 0 Then

                    Me.ReportPath = s
                    With Me.pgb
                        .Maximum = lv.Items.Count
                        .Minimum = 0
                        .Value = 0
                    End With

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Call Me.SaveListviewContentAsTxtFile(s, lv)
                    ElseIf s.Substring(s.Length - 3, 3).ToLower = "csv" Then
                        Call Me.SaveListviewContentAsCsvFile(s, lv)
                    Else
                        Call Me.SaveListviewContentAsHtmlFile(s, lv)
                    End If

                    ReportSaved()
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub

End Class