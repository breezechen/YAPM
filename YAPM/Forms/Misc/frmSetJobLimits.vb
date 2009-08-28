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
Imports YAPM.Common.Misc

Public Class frmSetJobLimits

    Private _jobName As String

    Public Property JobName() As String
        Get
            Return _jobName
        End Get
        Set(ByVal value As String)
            _jobName = value
        End Set
    End Property

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        closeWithEchapKey(Me)
        ' Tooltips
        '  SetToolTip(Me.cmdChangeProtection, "Change protection type now")

        ' Get current limits and fill in form controls
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetLimits.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Checkboxes management"

    ' This is NOT possible to combile all flags together
    ' See http://msdn.microsoft.com/en-us/library/ms684147(VS.85).aspx
    ' for details about what is permitted and what is not

    Private Sub chkPreserveJobTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPreserveJobTime.CheckedChanged
        Me.chkUserModePerJ.Enabled = Not (Me.chkPreserveJobTime.Checked)
        If Me.chkPreserveJobTime.Enabled = False Then
            Me.chkPreserveJobTime.Checked = False
        End If
    End Sub

    Private Sub chkUserModePerJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUserModePerJ.CheckedChanged
        Me.chkPreserveJobTime.Enabled = Not (Me.chkUserModePerJ.Checked)
        If Me.chkPreserveJobTime.Enabled = False Then
            Me.chkPreserveJobTime.Checked = False
        End If
    End Sub

#End Region

    Private Sub linkMSDN_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        cFile.ShellOpenFile("http://msdn.microsoft.com/en-us/library/ms684147(VS.85).aspx", Me.Handle)
    End Sub

    Private Sub lnkMSDN2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        cFile.ShellOpenFile("http://msdn.microsoft.com/en-us/library/ms684152(VS.85).aspx", Me.Handle)
    End Sub

End Class