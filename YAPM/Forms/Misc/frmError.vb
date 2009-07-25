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

Public Class frmError

    Private _canClose As Boolean = False
    Private Const NEW_ARTIFACT_URL As String = "http://sourceforge.net/tracker/?atid=1126635&group_id=244697&func=add"

    Public Sub New(ByVal e As Exception)

        InitializeComponent()

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
        s &= vbNewLine & vbNewLine
        s &= "User informations : "
        s &= vbNewLine & vbTab & "Admin : " & Program.IsAdministrator.ToString
        s &= vbNewLine & vbNewLine
        s &= "Application informations : "
        s &= vbNewLine & vbTab & "Path : " & My.Application.Info.DirectoryPath
        s &= vbNewLine & vbTab & "Version : " & My.Application.Info.Version.ToString
        s &= vbNewLine & vbTab & "WorkingSetSize : " & My.Application.Info.WorkingSet.ToString
        s &= vbNewLine & vbNewLine
        s &= "Dll informations : "
        Try
            s &= vbNewLine & vbTab & "Ribbon version : " & System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\System.Windows.Forms.Ribbon.dll").FileVersion
        Catch ex As Exception
            s &= vbNewLine & vbTab & "Ribbon version : CANNOT GET VERSION"
        End Try
        Try
            s &= vbNewLine & vbTab & "MemoryHexEditor version : " & System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\MemoryHexEditor.dll").FileVersion
        Catch ex As Exception
            s &= vbNewLine & vbTab & "MemoryHexEditor version : CANNOT GET VERSION"
        End Try
        Try
            s &= vbNewLine & vbTab & "CoreFunc version : " & System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\dll").FileVersion
        Catch ex As Exception
            s &= vbNewLine & vbTab & "CoreFunc version : CANNOT GET VERSION"
        End Try
        Try
            s &= vbNewLine & vbTab & "TaskDialog version : " & System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\TaskDialog.dll").FileVersion
        Catch ex As Exception
            s &= vbNewLine & vbTab & "TaskDialog version : CANNOT GET VERSION"
        End Try
        Try
            s &= vbNewLine & vbTab & "VistaMenu version : " & System.Diagnostics.FileVersionInfo.GetVersionInfo(My.Application.Info.DirectoryPath & "\VistaMenu.dll").FileVersion
        Catch ex As Exception
            s &= vbNewLine & vbTab & "VistaMenu version : CANNOT GET VERSION"
        End Try
        s &= vbNewLine & vbNewLine
        s &= "Error informations : "
        s &= vbNewLine & vbTab & "Message : " & e.Message
        s &= vbNewLine & vbTab & "Source : " & e.Source
        s &= vbNewLine & vbTab & "StackTrace : " & e.StackTrace
        s &= vbNewLine & vbTab & "Target : " & e.TargetSite.ToString
        s &= vbNewLine & vbNewLine
        s &= "Other informations : "
        s &= vbNewLine & vbTab & "Connection : " & Program.Connection.ConnectionType.ToString
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

    Private Sub lnkSFtracker_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSFtracker.LinkClicked
        cFile.ShellOpenFile(NEW_ARTIFACT_URL, Me.Handle)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        API.ExitProcess(0)
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
    End Sub

    Private Sub cmdIgnore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIgnore.Click
        Me.Hide()
    End Sub
End Class
