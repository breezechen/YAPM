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

Public Class frmCreateService

    Private Sub frmAddToJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Common.Misc.SetToolTip(Me.OK_Button, "Add to job")
        Common.Misc.SetToolTip(Me.Cancel_Button, "Cancel")
        Common.Misc.SetToolTip(Me.txtArgs, "Arguments used in command line")
        Common.Misc.SetToolTip(Me.txtDisplayName, "Service display name")
        Common.Misc.SetToolTip(Me.txtMachine, "Machine name")
        Common.Misc.SetToolTip(Me.txtPath, "Path of the executable file")
        Common.Misc.SetToolTip(Me.txtServerPassword, "Password")
        Common.Misc.SetToolTip(Me.txtServiceName, "Service name")
        Common.Misc.SetToolTip(Me.txtUser, "User name")
        Common.Misc.SetToolTip(Me.cbErrControl, "Error control")
        Common.Misc.SetToolTip(Me.cbServType, "Service type")
        Common.Misc.SetToolTip(Me.cbStartType, "Service start type")
        Common.Misc.SetToolTip(Me.cmdBrowse, "Browse for executable file")
        Common.Misc.SetToolTip(Me.optLocal, "Create service on the local machine")
        Common.Misc.SetToolTip(Me.optRemote, "Create service on a remote machine")

        Common.Misc.closeWithEchapKey(Me)

        Me.cbErrControl.SelectedIndex = 0
        Me.cbServType.SelectedIndex = 0
        Me.cbStartType.SelectedIndex = 0

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim params As New Native.Api.Structs.ServiceCreationParams

        Me.OK_Button.Enabled = False
        Me.Cancel_Button.Enabled = False

        ' Create service
        With params
            .Arguments = Me.txtArgs.Text
            .DisplayName = Me.txtDisplayName.Text
            .ErrorControl = Native.Functions.Service.GetServiceErrorControlFromString(Me.cbErrControl.Text)
            .FilePath = Me.txtPath.Text
            .ServiceName = Me.txtServiceName.Text
            .StartType = Native.Functions.Service.GetServiceStartTypeFromString(Me.cbStartType.Text)
            .Type = Native.Functions.Service.GetServiceTypeFromString(Me.cbServType.Text)
            If Me.optRemote.Checked Then
                .RegMachine = Me.txtMachine.Text
                .RegPassword = Me.txtServerPassword.SecureText
                .RegUser = Me.txtUser.Text
            End If
        End With
        If Native.Objects.Service.CreateService(params) Then
            ' MsgBox("Success", MsgBoxStyle.Information, "Create service")
            Me.Close()
        Else
            MsgBox("Failed : " & Native.Api.Win32.GetLastError, MsgBoxStyle.Critical, "Create service")
        End If

        Me.OK_Button.Enabled = True
        Me.Cancel_Button.Enabled = True
    End Sub

    Private Sub optLocal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLocal.CheckedChanged
        Me.txtUser.Enabled = optLocal.Checked
        Me.txtMachine.Enabled = optLocal.Checked
        Me.txtServerPassword.Enabled = optLocal.Checked
        Me.lblMachine.Enabled = optLocal.Checked
        Me.lblPwd.Enabled = optLocal.Checked
        Me.lblUser.Enabled = optLocal.Checked
    End Sub

    Private Sub optRemote_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRemote.CheckedChanged
        Call optLocal_CheckedChanged(Nothing, Nothing)
    End Sub
End Class
