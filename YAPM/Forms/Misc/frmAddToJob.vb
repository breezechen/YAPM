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

Public Class frmAddToJob

    Private _pids As List(Of Integer)
    Private WithEvents theConnection As cConnection
    Private _local As Boolean = True
    Private _notWMI As Boolean

    Public Sub New(ByVal pids As List(Of Integer))
        MyBase.New()
        InitializeComponent()
        _pids = pids
    End Sub

    Private Sub frmAddToJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Connect providers
        'theConnection.CopyFromInstance(Program.Connection)
        Try
            theConnection = Program.Connection
            Me.lvJob.ConnectionObj = theConnection
            theConnection.Connect()
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to connect")
        End Try

        Common.Misc.SetToolTip(Me.optAddGlobal, "Create global job")
        Common.Misc.SetToolTip(Me.optAddLocal, "Create local job")
        Common.Misc.SetToolTip(Me.OK_Button, "Add to job")
        Common.Misc.SetToolTip(Me.Cancel_Button, "Cancel")
        Common.Misc.SetToolTip(Me.txtJobName, "Job name (must be not null)")

        Native.Functions.Misc.SetTheme(Me.lvJob.Handle)
        Common.Misc.closeWithEchapKey(Me)

    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        ' Now add the processes to job
        ' Add to job
        Dim name As String = ""
        If tab.SelectedIndex = 0 Then

            If String.IsNullOrEmpty(Me.txtJobName.Text) Then
                Misc.ShowMsg("Add process to a job", "Could not add process to this job.", "Job name must not be null", MessageBoxButtons.OK, TaskDialogIcon.Error)
                Exit Sub
            End If

            ' New job
            If optAddGlobal.Checked Then
                name = "Global\" & Me.txtJobName.Text
            Else
                name = Me.txtJobName.Text
            End If
        Else
            ' Existing
            Dim _job As cJob = Me.lvJob.GetSelectedItem
            name = _job.Infos.Name
            ' Format name
            If name.StartsWith("\BaseNamedObjects\") Then
                ' Global
                name = name.Replace("\BaseNamedObjects\", "Global\")
            End If
        End If

        Dim job As cJob = cJob.CreateJobByName(name)
        If job IsNot Nothing Then
            ' Then we add the job to the menu
            Dim pid As Integer
            For Each pid In _pids
                job.AddProcess(pid)
            Next
        Else
            Misc.ShowMsg("Add process to job", "Failed to add the process to the job.", "Informations : " & Native.Api.Win32.GetLastError, MessageBoxButtons.OK, TaskDialogIcon.Error)
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Me.lvJob.UpdateTheItems()
    End Sub

    Private Sub lvJob_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvJob.MouseDoubleClick
        If lvJob.SelectedItems.Count > 0 Then
            Call Me.OK_Button_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub txtJobName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtJobName.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Me.OK_Button_Click(Nothing, Nothing)
        End If
    End Sub

End Class
