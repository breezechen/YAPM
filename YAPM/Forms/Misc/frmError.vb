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

Imports System.Windows.Forms
Imports Common.Misc
Imports System.Net

Public Class frmError

    Private _theExeption As Exception
    Private _canClose As Boolean = False
    Private _bugReportUrl As String
    Private Const NEW_ARTIFACT_URL As String = "http://sourceforge.net/tracker/?atid=1126635&group_id=244697&func=add"

    Public Sub New(ByVal e As Exception)

        InitializeComponent()

        _theExeption = e

        ' Create a log
        Dim s As String = ""
        s &= "System informations : "
        s &= vbNewLine & vbTab & "Name : " & My.Computer.Info.OSFullName
        s &= vbNewLine & vbTab & "Platform : " & My.Computer.Info.OSPlatform
        s &= vbNewLine & vbTab & "Version : " & My.Computer.Info.OSVersion.ToString
        s &= vbNewLine & vbTab & "UICulture : " & My.Computer.Info.InstalledUICulture.ToString
        s &= vbNewLine & vbTab & "Processor count : " & Program.PROCESSOR_COUNT.ToString
        s &= vbNewLine & vbTab & "Physical memory : " & GetFormatedSize(My.Computer.Info.AvailablePhysicalMemory) & "/" & GetFormatedSize(My.Computer.Info.TotalPhysicalMemory)
        s &= vbNewLine & vbTab & "Virtual memory : " & GetFormatedSize(My.Computer.Info.AvailableVirtualMemory) & "/" & GetFormatedSize(My.Computer.Info.TotalVirtualMemory)
        s &= vbNewLine & vbTab & "Screen : " & My.Computer.Screen.Bounds.ToString
        s &= vbNewLine & vbTab & "IntPtr.Size : " & IntPtr.Size.ToString
        s &= vbNewLine & vbNewLine
        s &= "User informations : "
        s &= vbNewLine & vbTab & "Admin : " & Program.IsAdministrator.ToString
        s &= vbNewLine & vbNewLine
        s &= "Application informations : "
        s &= vbNewLine & vbTab & "Path : " & My.Application.Info.DirectoryPath
        s &= vbNewLine & vbTab & "Version : " & My.Application.Info.Version.ToString
        s &= vbNewLine & vbTab & "WorkingSetSize : " & My.Application.Info.WorkingSet.ToString
        s &= vbNewLine & vbNewLine
        s &= "Error informations : "
        s &= vbNewLine & vbTab & "Message : " & e.Message
        s &= vbNewLine & vbTab & "Source : " & e.Source
        s &= vbNewLine & vbTab & "StackTrace : " & e.StackTrace
        s &= vbNewLine & vbTab & "Target : " & e.TargetSite.ToString
        s &= vbNewLine & vbNewLine
        s &= "Other informations : "
        s &= vbNewLine & vbTab & "Connection : " & Program.Connection.Type.ToString
        s &= vbNewLine & vbTab & "Connected : " & Program.Connection.IsConnected.ToString
        s &= vbNewLine & vbTab & "Elapsed time : " & Program.ElapsedTime.ToString
        s &= vbNewLine & vbNewLine
        s &= "Modules : "
        For Each mdl As ProcessModule In System.Diagnostics.Process.GetCurrentProcess.Modules
            s &= vbNewLine & vbTab & mdl.ModuleName
            s &= vbNewLine & vbTab & vbTab & "Path : " & mdl.FileName
            If mdl.FileVersionInfo IsNot Nothing Then
                s &= vbNewLine & vbTab & vbTab & "Version : " & mdl.FileVersionInfo.FileVersion
            End If
        Next

        Me.txtReport.Text = s

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        Native.Api.NativeFunctions.ExitProcess(0)
    End Sub

    Private Sub frmError_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not (_canClose)
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        _canClose = True
        Me.Close()
    End Sub

    Private Sub frmError_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetToolTip(Me.cmdQuit, "Terminate YAPM immediatly")
        SetToolTip(Me.cmdContinue, "Cloe this window and try to continue the execution of YAPM. It's probably gonna crash")
        SetToolTip(Me.cmdIgnore, "Hide this window and ignore the error")
        SetToolTip(Me.cmdSend, "Send the bug report to sourceforge.net. Of course NO PERSONAL INFORMATION will be send.")

        Me.txtCustomMessage.Focus()
    End Sub

    Private Sub cmdIgnore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIgnore.Click
        Me.Hide()
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click

        If Me.cmdSend.Text = "Open the bug report" Then
            ' Open the bug report
            cFile.ShellOpenFile(_bugReportUrl, Me.Handle)
        Else
            ' Send it to sf.net
            Dim bgRep As New BugReporter
            Me.Enabled = False
            Application.DoEvents()

            ' Set the handler (used when download completed)
            AddHandler bgRep.DownloadStringCompleted, AddressOf impDownloadStringCompleted

            ' Create an unique string to prevent some identical error messages to
            ' be added with identical summaries on sourceforge.net
            Dim uId As String = Date.Now.ToFileTimeUtc.ToString

            ' Set summary and description
            With bgRep
                .ValueDetails = Me.txtReport.Text & vbNewLine & vbNewLine & vbNewLine & "CUSTOM MESSAGE : " & vbNewLine & Me.txtCustomMessage.Text
                .ValueSummary = _theExeption.Message & " (" & uId & ")"
            End With

            If bgRep.GoAsync = False Then
                Me.Enabled = True
            End If
        End If

    End Sub

    Private Sub impDownloadStringCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)

        If Me.Enabled Then
            Exit Sub
        End If

        If e.Error IsNot Nothing Then
            Misc.ShowMsg("Could not send bug report", "An error occured when sending bug report.", e.Error.ToString, MessageBoxButtons.OK, TaskDialogIcon.Error)
        Else
            Misc.ShowMsg("Successfully sent the bug report", "Successfully sent the bug report.", "", MessageBoxButtons.OK, TaskDialogIcon.ShieldOk)
        End If

        ' Get the result URL
        _bugReportUrl = BugReporter.GetUrlOfBugReportFromHtml(e.Result)

        Async.Button.ChangeText(Me.cmdSend, "Open the bug report")
        Async.Form.ChangeEnabled(Me, True)

    End Sub

End Class
