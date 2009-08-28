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

Public Class frmJobInfo

    Private WithEvents curJob As cJob

    Private WithEvents theConnection As cConnection
    Private _local As Boolean = True
    Private _notWMI As Boolean


    ' Refresh current tab
    Private Sub refreshJobTab()

        Me.Text = "Job informations (" & curJob.Infos.Name & ")"

        If curJob Is Nothing Then Exit Sub

        Select Case Me.tabJob.SelectedTab.Text

            Case "General"
                ' Update processes in job
                Me.lvProcess.Job = curJob
                Me.lvProcess.UpdateTheItems()

            Case "Statistics"

                ' Refresh stats
                curJob.Refresh()

                ' CPU
                Dim ts As Date
                Dim s As String
                Me.lblAffinity.Text = curJob.BasicLimitInformation.Affinity.ToString
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.TotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblUserTime.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblUserPeriod.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.TotalKernelTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblKernelTime.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalKernelTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblPeriodKernel.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalKernelTime + curJob.BasicAndIoAccountingInformation.BasicInfo.ThisPeriodTotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblTotalPeriod.Text = s
                ts = New Date(curJob.BasicAndIoAccountingInformation.BasicInfo.TotalKernelTime + curJob.BasicAndIoAccountingInformation.BasicInfo.TotalUserTime)
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblTotalTime.Text = s
                Me.lblPriority.Text = curJob.BasicLimitInformation.PriorityClass.ToString

                ' Others
                Me.lblTotalProcesses.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.TotalProcesses.ToString
                Me.lblActiveProcesses.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.ActiveProcesses.ToString
                Me.lblTotalTerminatedProcesses.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.TotalTerminatedProcesses.ToString
                Me.lblMaxWSS.Text = GetFormatedSize(curJob.BasicLimitInformation.MaximumWorkingSetSize)
                Me.lblMinWSS.Text = GetFormatedSize(curJob.BasicLimitInformation.MinimumWorkingSetSize)
                Me.lblSchedulingClass.Text = curJob.BasicLimitInformation.SchedulingClass.ToString
                Me.lblPageFaultCount.Text = curJob.BasicAndIoAccountingInformation.BasicInfo.TotalPageFaultCount.ToString

                ' IO
                Me.lblProcOther.Text = curJob.BasicAndIoAccountingInformation.IoInfo.OtherOperationCount.ToString
                Me.lblProcOtherBytes.Text = GetFormatedSize(curJob.BasicAndIoAccountingInformation.IoInfo.OtherTransferCount)
                Me.lblProcReads.Text = curJob.BasicAndIoAccountingInformation.IoInfo.WriteOperationCount.ToString
                Me.lblProcReadBytes.Text = GetFormatedSize(curJob.BasicAndIoAccountingInformation.IoInfo.ReadTransferCount)
                Me.lblProcWriteBytes.Text = curJob.BasicAndIoAccountingInformation.IoInfo.ReadOperationCount.ToString
                Me.lblProcWrites.Text = GetFormatedSize(curJob.BasicAndIoAccountingInformation.IoInfo.WriteTransferCount)


            Case "Limitations"


        End Select
    End Sub

    Private Sub frmServiceInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        ' Some tooltips
        Native.Functions.Misc.SetTheme(Me.lvProcess.Handle)
        SetToolTip(Me.cmdAddProcess, "Add processes to the job")
        SetToolTip(Me.cmdTerminateJob, "Terminate the job")

        Pref.LoadListViewColumns(Me.lvProcess, "COLmain_process")

        Select Case My.Settings.JobSelectedTab
            Case "General"
                Me.tabJob.SelectedTab = Me.pageGeneral
            Case "Statistics"
                Me.tabJob.SelectedTab = Me.pageStats
            Case "Limitations"
                Me.tabJob.SelectedTab = Me.pageLimitations
        End Select

        Call Connect()
        Call refreshJobTab()

    End Sub

    ' Get process to monitor
    Public Sub SetJob(ByRef job As cJob)

        curJob = job

        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cProcess.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        Me.Timer.Enabled = _local

    End Sub

    ' Change caption
    Private Sub ChangeCaption()
        'Me.Text = curServ.Infos.Name & " (" & curServ.Infos.DisplayName & ")"
    End Sub

    Private Sub tabProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabJob.SelectedIndexChanged
        Call Me.refreshJobTab()
        Call ChangeCaption()
        My.Settings.JobSelectedTab = Me.tabJob.SelectedTab.Text
    End Sub

    ' Connection
    Public Sub Connect()
        ' Connect providers
        'theConnection.CopyFromInstance(Program.Connection)
        Try
            theConnection = Program.Connection
            Me.lvProcess.ConnectionObj = theConnection
            theConnection.Connect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Can not connect")
        End Try
    End Sub

    Private Sub theConnection_Connected() Handles theConnection.Connected
        '
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        '
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ' Refresh informations about process
        Call Me.refreshJobTab()

        ' Display caption
        Call ChangeCaption()
    End Sub

    Private Sub cmdTerminateJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTerminateJob.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to terminate the job ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        curJob.TerminateJob()
    End Sub

    Private Sub cmdAddProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProcess.Click
        '
    End Sub
End Class