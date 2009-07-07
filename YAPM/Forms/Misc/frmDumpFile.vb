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

Public Class frmDumpFile

    Private _dir As String

    Public ReadOnly Property TargetDir() As String
        Get
            Return _dir
        End Get
    End Property
    Public ReadOnly Property DumpOption() As API.MINIDUMPTYPE
        Get
            Select Case Me.cbOption.Text
                Case "MiniDumpNormal"
                    Return API.MINIDUMPTYPE.MiniDumpNormal
                Case "MiniDumpWithDataSegs"
                    Return API.MINIDUMPTYPE.MiniDumpWithDataSegs
                Case "MiniDumpWithFullMemory"
                    Return API.MINIDUMPTYPE.MiniDumpWithFullMemory
                Case "MiniDumpWithHandleData"
                    Return API.MINIDUMPTYPE.MiniDumpWithHandleData
                Case "MiniDumpFilterMemory"
                    Return API.MINIDUMPTYPE.MiniDumpFilterMemory
                Case "MiniDumpScanMemory"
                    Return API.MINIDUMPTYPE.MiniDumpScanMemory
                Case "MiniDumpWithUnloadedModules"
                    Return API.MINIDUMPTYPE.MiniDumpWithUnloadedModules
                Case "MiniDumpWithIndirectlyReferencedMemory"
                    Return API.MINIDUMPTYPE.MiniDumpWithIndirectlyReferencedMemory
                Case "MiniDumpFilterModulePaths"
                    Return API.MINIDUMPTYPE.MiniDumpFilterModulePaths
                Case "MiniDumpWithProcessThreadData"
                    Return API.MINIDUMPTYPE.MiniDumpWithProcessThreadData
                Case "MiniDumpWithPrivateReadWriteMemory"
                    Return API.MINIDUMPTYPE.MiniDumpWithPrivateReadWriteMemory
                Case "MiniDumpWithoutOptionalData"
                    Return API.MINIDUMPTYPE.MiniDumpWithoutOptionalData
                Case "MiniDumpWithFullMemoryInfo"
                    Return API.MINIDUMPTYPE.MiniDumpWithFullMemoryInfo
                Case "MiniDumpWithThreadInfo"
                    Return API.MINIDUMPTYPE.MiniDumpWithThreadInfo
                Case "MiniDumpWithCodeSegs"
                    Return API.MINIDUMPTYPE.MiniDumpWithCodeSegs
            End Select
        End Get
    End Property

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        closeWithEchapKey(Me)
        SetToolTip(Me.cmdBrowse, "Select target directory")
        SetToolTip(Me.txtDir, "Target directory")
        SetToolTip(Me.cmdCreate, "Create the minidump now")
        SetToolTip(Me.cmdExit, "Close this window")
        SetToolTip(Me.cbOption, "Specifies the datas to include into the dump file")
        Me.cbOption.Text = "MiniDumpNormal"
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
        If System.IO.Directory.Exists(_dir) Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub txtDir_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDir.TextChanged
        _dir = Me.txtDir.Text
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        With ChooseFolder
            .Description = "Select target directory"
            .RootFolder = Environment.SpecialFolder.MyComputer
            .ShowNewFolderButton = True
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDir.Text = .SelectedPath
            End If
        End With
    End Sub
End Class