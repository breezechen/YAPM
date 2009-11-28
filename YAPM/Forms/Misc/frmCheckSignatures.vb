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

Public Class frmCheckSignatures

    Private Sub frmHiddenProcesses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        SetToolTip(Me.cmdCheck, "Check now if processes and modules are trusted")

        Call Native.Functions.Misc.SetTheme(Me.lvResult.Handle)
        Call Native.Functions.Misc.SetTheme(Me.lvProcess.Handle)
        Dim theConnection As New cConnection
        theConnection.ConnectionType = cConnection.TypeOfConnection.LocalConnection

        Me.lvProcess.ClearItems()
        Me.lvProcess.ConnectionObj = theConnection

        Try
            theConnection.Connect()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

        Me.Timer.Enabled = True

    End Sub

    Private Sub lvProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDoubleClick
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            Dim frm As New frmProcessInfo
            frm.SetProcess(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub lvProcess_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.TheContextMenu.Show(Me.lvProcess, e.Location)
        End If
    End Sub

    Private Sub MenuItemShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemShow.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If IO.File.Exists(cp.Infos.Path) Then
                cFile.ShowFileProperty(cp.Infos.Path, Me.Handle)
            End If
        Next
    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If cp.Infos.Path <> NO_INFO_RETRIEVED Then
                cFile.OpenDirectory(cp.Infos.Path)
            End If
        Next
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim cp As cProcess = Me.lvProcess.GetSelectedItem
            Dim s As String = cp.Infos.Path
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Application.DoEvents()
            Call SearchInternet(cp.Infos.Name, Me.Handle)
        Next
    End Sub

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        ' Check if files are trusted

        ' Path <-> trust result
        Dim _dicoTrusted As New Dictionary(Of String, Native.Api.NativeEnums.WinVerifyTrustResult)

        Me.pgb.Minimum = 0
        Me.pgb.Value = 0
        Me.pgb.Maximum = Me.lvProcess.CheckedIndices.Count

        For Each proc As cProcess In Me.lvProcess.GetCheckedItems

            ' Get list of modules
            Dim _modules As Dictionary(Of String, moduleInfos) = asyncCallbackModuleEnumerate.SharedLocalSyncEnumerate(New asyncCallbackModuleEnumerate.poolObj(proc.Infos.ProcessId, 0))

            ' For each module
            If _modules IsNot Nothing Then
                For Each file As moduleInfos In _modules.Values
                    ' If not already verified, check it
                    If _dicoTrusted.ContainsKey(file.Path) = False Then
                        _dicoTrusted(file.Path) = Security.WinTrust.WinTrust.VerifyEmbeddedSignature2(file.Path)
                    End If
                Next
            End If

            Me.pgb.Value += 1

        Next

        ' Now count items and add it to result lv
        Dim _failed As Integer = 0
        Dim _success As Integer = 0
        Dim _unknown As Integer = 0

        Me.lvResult.BeginUpdate()
        Me.lvResult.Items.Clear()
        For Each pair As Generic.KeyValuePair(Of String, Native.Api.NativeEnums.WinVerifyTrustResult) In _dicoTrusted
            Dim it As ListViewItem = Me.lvResult.Items.Add(pair.Key)
            it.SubItems.Add(pair.Value.ToString)
            Select Case pair.Value
                Case Native.Api.NativeEnums.WinVerifyTrustResult.Critical Or Native.Api.NativeEnums.WinVerifyTrustResult.Expired Or Native.Api.NativeEnums.WinVerifyTrustResult.ExplicitDistrust Or Native.Api.NativeEnums.WinVerifyTrustResult.Malformed Or Native.Api.NativeEnums.WinVerifyTrustResult.RevocationFailure Or Native.Api.NativeEnums.WinVerifyTrustResult.Revoked Or Native.Api.NativeEnums.WinVerifyTrustResult.SubjectNotTrusted Or Native.Api.NativeEnums.WinVerifyTrustResult.UntrustedCA Or Native.Api.NativeEnums.WinVerifyTrustResult.UntrustedRoot Or Native.Api.NativeEnums.WinVerifyTrustResult.UntrustedTestRoot Or Native.Api.NativeEnums.WinVerifyTrustResult.ValidityPeriodNesting
                    _failed += 1
                    it.ForeColor = Color.Red
                Case Native.Api.NativeEnums.WinVerifyTrustResult.Trusted
                    _success += 1
                    it.ForeColor = Color.Green
                Case Else
                    _unknown += 1
            End Select
        Next
        Me.lblFailed.Text = "Not trusted : " & _failed.ToString
        Me.lblOK.Text = "Trusted : " & _success.ToString
        Me.lblUnknown.Text = "Unknown : " & _unknown.ToString
        Me.lvResult.EndUpdate()

    End Sub

    Private Sub MenuItemCAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCAll.Click
        For Each it As ListViewItem In Me.lvProcess.Items
            it.Checked = True
        Next
    End Sub

    Private Sub MenuItemCNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCNone.Click
        For Each it As ListViewItem In Me.lvProcess.Items
            it.Checked = False
        Next
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ' Refresh list of processes
        If Me.lvProcess.Items.Count = 0 Then
            Me.lvProcess.UpdateItemsAllInfos()
        End If
    End Sub
End Class
