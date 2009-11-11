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
Imports Common.Misc
Imports Native.Api
Imports Native.Objects.Job

Public Class frmSetJobLimits

    Private _jobName As String
    Private _jobInfoForm As frmJobInfo

    Public Property JobName() As String
        Get
            Return _jobName
        End Get
        Set(ByVal value As String)
            _jobName = value
        End Set
    End Property

    Public Sub New(ByRef frmJInfo As frmJobInfo)
        InitializeComponent()
        ' Get the caller form
        _jobInfoForm = frmJInfo
    End Sub

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseWithEchapKey(Me)

        ' Tooltips
        SetToolTip(Me.chkActiveProcesses, "Establishes a maximum number of simultaneously active processes associated with the job")
        SetToolTip(Me.chkAffinity, "Causes all processes associated with the job to use the same processor affinity")
        SetToolTip(Me.chkBreakawayOK, "If any process associated with the job creates a child process using the CREATE_BREAKAWAY_FROM_JOB flag while this limit is in effect, the child process is not associated with the job")
        SetToolTip(Me.chkCommittedMemPerJ, "Causes all processes associated with the job to limit the job-wide sum of their committed memory. When a process attempts to commit memory that would exceed the job-wide limit, it fails. If the job object is associated with a completion port, a JOB_OBJECT_MSG_JOB_MEMORY_LIMIT message is sent to the completion port.")
        SetToolTip(Me.chkCommittedMemPerP, "Causes all processes associated with the job to limit their committed memory. When a process attempts to commit memory that would exceed the per-process limit, it fails. If the job object is associated with a completion port, a JOB_OBJECT_MSG_PROCESS_MEMORY_LIMIT message is sent to the completion port")
        SetToolTip(Me.chkDieOnUnhandledEx, "Forces a call to the SetErrorMode function with the SEM_NOGPFAULTERRORBOX flag for each process associated with the job. " & vbNewLine & "If an exception occurs and the system calls the UnhandledExceptionFilter function, the debugger will be given a chance to act. If there is no debugger, the functions returns EXCEPTION_EXECUTE_HANDLER. Normally, this will cause termination of the process with the exception code as the exit status.")
        SetToolTip(Me.chkKillOnJobClose, "Causes all processes associated with the job to terminate when the last handle to the job is closed")
        SetToolTip(Me.chkMinMaxWS, "Causes all processes associated with the job to use the same minimum and maximum working set sizes.")
        SetToolTip(Me.chkPreserveJobTime, "Preserves any job time limits you previously set. As long as this flag is set, you can establish a per-job time limit once, then alter other limits in subsequent calls. This flag cannot be used with JOB_OBJECT_LIMIT_JOB_TIME")
        SetToolTip(Me.chkPriority, "Causes all processes associated with the job to use the same priority class")
        SetToolTip(Me.chkSchedulingC, "Causes all processes in the job to use the same scheduling class")
        SetToolTip(Me.chkSilentBAOK, "Allows any process associated with the job to create child processes that are not associated with the job")
        SetToolTip(Me.chkUIdesktop, "Prevents processes associated with the job from creating desktops and switching desktops using the CreateDesktop and SwitchDesktop functions")
        SetToolTip(Me.chkUIDisplaySettings, "Prevents processes associated with the job from calling the ChangeDisplaySettings function")
        SetToolTip(Me.chkUIExitW, "Prevents processes associated with the job from calling the ExitWindows or ExitWindowsEx function")
        SetToolTip(Me.chkUIglobalAtoms, "Prevents processes associated with the job from accessing global atoms. When this flag is used, each job has its own atom table")
        SetToolTip(Me.chkUIhandles, "Prevents processes associated with the job from using USER handles owned by processes not associated with the same job")
        SetToolTip(Me.chkUIreadCB, "Prevents processes associated with the job from reading data from the clipboard")
        SetToolTip(Me.chkUIsystemParam, "Prevents processes associated with the job from changing system parameters by using the SystemParametersInfo function")
        SetToolTip(Me.chkUIwriteCB, "Prevents processes associated with the job from writing data to the clipboard")
        SetToolTip(Me.chkUserModePerJ, "Establishes a user-mode execution time limit for the job")
        SetToolTip(Me.chkUserModePerP, "Establishes a user-mode execution time limit for each currently active process and for all future processes associated with the job")
        SetToolTip(Me.valAffinity, "Common affinity for all processes in the job")
        SetToolTip(Me.cbPriority, "Priority class of the job")
        SetToolTip(Me.valActiveProcesses, "Maximum active processes in job")
        SetToolTip(Me.valMaxWS, "Max working set size per process")
        SetToolTip(Me.valMemJ, "Max committed memory for job")
        SetToolTip(Me.valMemP, "Max committed memory for each process")
        SetToolTip(Me.valMinWS, "Min working set size per process")
        SetToolTip(Me.valScheduling, "Common scheduling class for all processes")
        SetToolTip(Me.valUsertimeJ, "Max CPU time in usermode for job")
        SetToolTip(Me.valUsertimeP, "Max CPU time in usermode per process")
        SetToolTip(Me.cmdExit, "Exit without modifiying limits")
        SetToolTip(Me.cmdSetLimits, "Set limits to the job")


        ' Get all limits from the limit's listview (cause it's well refreshed
        ' depending of the type of connection)
        Dim _limits As New Dictionary(Of String, cJobLimit)
        For Each limit As cJobLimit In _jobInfoForm.lvLimits.GetAllItems()
            _limits.Add(limit.Infos.Name, limit)
        Next

        For Each pair As System.Collections.Generic.KeyValuePair(Of String, cJobLimit) In _limits

            ' UI limits
            If pair.Key = "Desktop" Then
                Me.chkUIdesktop.Checked = True
            End If
            If pair.Key = "DisplaySettings" Then
                Me.chkUIDisplaySettings.Checked = True
            End If
            If pair.Key = "ExitWindows" Then
                Me.chkUIExitW.Checked = True
            End If
            If pair.Key = "GlobalAtoms" Then
                Me.chkUIglobalAtoms.Checked = True
            End If
            If pair.Key = "Handles" Then
                Me.chkUIhandles.Checked = True
            End If
            If pair.Key = "ReadClipboard" Then
                Me.chkUIreadCB.Checked = True
            End If
            If pair.Key = "SystemParameters" Then
                Me.chkUIsystemParam.Checked = True
            End If
            If pair.Key = "WriteClipboard" Then
                Me.chkUIwriteCB.Checked = True
            End If

            ' Other limitations
            If pair.Key = "ActiveProcess" Then
                Me.chkActiveProcesses.Checked = True
                Me.valActiveProcesses.Value = CInt(pair.Value.Infos.ValueObject)
            End If
            If pair.Key = "Affinity" Then
                Me.chkAffinity.Checked = True
                Me.valAffinity.Value = CType(pair.Value.Infos.ValueObject, IntPtr).ToInt32
            End If
            If pair.Key = "BreakawayOk" Then
                Me.chkBreakawayOK.Checked = True
            End If
            If pair.Key = "DieOnUnhandledException" Then
                Me.chkDieOnUnhandledEx.Checked = True
            End If
            If pair.Key = "JobMemory" Then
                Me.chkCommittedMemPerJ.Checked = True
                Me.valMemJ.Value = CInt(CInt(pair.Value.Infos.ValueObject) / 1024)
            End If
            If pair.Key = "JobTime" Then
                Me.chkUserModePerJ.Checked = True
                Me.valUsertimeJ.Value = CInt(CInt(pair.Value.Infos.ValueObject) / 10)
            End If
            If pair.Key = "KillOnJobClose" Then
                Me.chkKillOnJobClose.Checked = True
            End If
            If pair.Key = "PreserveJobTime" Then
                Me.chkPreserveJobTime.Checked = True
            End If
            If pair.Key = "PriorityClass" Then
                Me.chkPriority.Checked = True
                Me.cbPriority.Text = CType(pair.Value.Infos.ValueObject, System.Diagnostics.ProcessPriorityClass).ToString
            End If
            If pair.Key = "ProcessMemory" Then
                Me.chkCommittedMemPerP.Checked = True
                Me.valMemP.Value = CInt(CInt(pair.Value.Infos.ValueObject) / 1024)
            End If
            If pair.Key = "ProcessTime" Then
                Me.chkUserModePerJ.Checked = True
                Me.valUsertimeP.Value = CInt(CInt(pair.Value.Infos.ValueObject) / 10)
            End If
            If pair.Key = "SchedulingClass" Then
                Me.chkSchedulingC.Checked = True
                Me.valScheduling.Value = CInt(pair.Value.Infos.ValueObject)
            End If
            If pair.Key = "SilentBreakawayOk" Then
                Me.chkSilentBAOK.Checked = True
            End If
            If pair.Key = "WorkingSetMin" Then
                Me.chkMinMaxWS.Checked = True
                Me.valMinWS.Value = CInt(CInt(pair.Value.Infos.ValueObject) / 1024)
            End If
            If pair.Key = "WorkingSetMax" Then
                Me.chkMinMaxWS.Checked = True
                Me.valMaxWS.Value = CInt(CInt(pair.Value.Infos.ValueObject) / 1024)
            End If
        Next

    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetLimits.Click
        If setLimits() = Windows.Forms.DialogResult.OK Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
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
        Me.valUsertimeJ.Enabled = Me.chkUserModePerJ.Checked
    End Sub

    Private Sub chkAffinity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAffinity.CheckedChanged
        Me.valAffinity.Enabled = Me.chkAffinity.Checked
    End Sub

    Private Sub chkActiveProcesses_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActiveProcesses.CheckedChanged
        Me.valActiveProcesses.Enabled = Me.chkActiveProcesses.Checked
    End Sub

    Private Sub chkSchedulingC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSchedulingC.CheckedChanged
        Me.valScheduling.Enabled = Me.chkSchedulingC.Checked
    End Sub

    Private Sub chkPriority_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPriority.CheckedChanged
        Me.cbPriority.Enabled = Me.chkPriority.Checked
    End Sub

    Private Sub chkCommittedMemPerJ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCommittedMemPerJ.CheckedChanged
        Me.valMemJ.Enabled = Me.chkCommittedMemPerJ.Checked
    End Sub

    Private Sub chkCommittedMemPerP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCommittedMemPerP.CheckedChanged
        Me.valMemP.Enabled = Me.chkCommittedMemPerP.Checked
    End Sub

    Private Sub chkUserModePerP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUserModePerP.CheckedChanged
        Me.valUsertimeP.Enabled = Me.chkUserModePerP.Checked
    End Sub

    Private Sub chkMinMaxWS_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMinMaxWS.CheckedChanged
        Me.valMinWS.Enabled = Me.chkMinMaxWS.Checked
        Me.valMaxWS.Enabled = Me.chkMinMaxWS.Checked
    End Sub
#End Region

    Private Sub linkMSDN_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        cFile.ShellOpenFile("http://msdn.microsoft.com/en-us/library/ms684147(VS.85).aspx", Me.Handle)
    End Sub

    Private Sub lnkMSDN2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        cFile.ShellOpenFile("http://msdn.microsoft.com/en-us/library/ms684152(VS.85).aspx", Me.Handle)
    End Sub

    Private Function setLimits() As DialogResult
        ' Set limits to the job
        Dim struct1 As New NativeStructs.JobObjectBasicUiRestrictions
        Dim struct2 As New NativeStructs.JobObjectExtendedLimitInformation

        ' UI limitations
        Dim flag1 As NativeEnums.JobObjectBasicUiRestrictions

        If Me.chkUIdesktop.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.Desktop
        End If
        If Me.chkUIDisplaySettings.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.DisplaySettings
        End If
        If Me.chkUIExitW.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.ExitWindows
        End If
        If Me.chkUIglobalAtoms.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.GlobalAtoms
        End If
        If Me.chkUIhandles.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.Handles
        End If
        If Me.chkUIreadCB.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.ReadClipboard
        End If
        If Me.chkUIsystemParam.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.SystemParameters
        End If
        If Me.chkUIwriteCB.Checked = True Then
            flag1 = flag1 Or NativeEnums.JobObjectBasicUiRestrictions.WriteClipboard
        End If
        struct1.UIRestrictionsClass = flag1


        ' Other limitations
        Dim flag2 As NativeEnums.JobObjectLimitFlags

        If Me.chkActiveProcesses.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.ActiveProcess
            struct2.BasicLimitInformation.ActiveProcessLimit = CInt(Me.valActiveProcesses.Value)
        End If
        If Me.chkAffinity.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.Affinity
            struct2.BasicLimitInformation.Affinity = New IntPtr(CInt(Me.valAffinity.Value))
        End If
        If Me.chkBreakawayOK.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.BreakawayOk
        End If
        If Me.chkDieOnUnhandledEx.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.DieOnUnhandledException
        End If
        If Me.chkCommittedMemPerJ.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.JobMemory
            struct2.JobMemoryLimit = CInt(1024 * Me.valMemJ.Value)
        End If
        If Me.chkSilentBAOK.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.SilentBreakawayOk
        End If
        If Me.chkUserModePerJ.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.JobTime
            struct2.BasicLimitInformation.PerJobUserTimeLimit = CInt(10 * Me.valUsertimeJ.Value)
        End If
        If Me.chkKillOnJobClose.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.KillOnJobClose
        End If
        If Me.chkPreserveJobTime.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.PreserveJobTime
        End If
        If Me.chkPriority.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.PriorityClass
            struct2.BasicLimitInformation.PriorityClass = Common.Misc.GetPriorityFromString(Me.cbPriority.Text)
        End If
        If Me.chkCommittedMemPerP.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.ProcessMemory
            struct2.ProcessMemoryLimit = CInt(1024 * Me.valMemP.Value)
        End If
        If Me.chkUserModePerJ.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.ProcessTime
            struct2.BasicLimitInformation.PerProcessUserTimeLimit = CInt(1024 * Me.valUsertimeP.Value)
        End If
        If Me.chkSchedulingC.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.SchedulingClass
            struct2.BasicLimitInformation.SchedulingClass = CInt(Me.valScheduling.Value)
        End If
        If Me.chkMinMaxWS.Checked = True Then
            flag2 = flag2 Or NativeEnums.JobObjectLimitFlags.WorkingSet
            struct2.BasicLimitInformation.MinimumWorkingSetSize = New IntPtr(CInt(1024 * Me.valMinWS.Value))
            struct2.BasicLimitInformation.MaximumWorkingSetSize = New IntPtr(CInt(1024 * Me.valMaxWS.Value))
        End If
        struct2.BasicLimitInformation.LimitFlags = flag2

        ' Set limit
        If WarnDangerousAction("Are you sure you want to set the limits you specified ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Return Windows.Forms.DialogResult.Cancel
        End If

        Dim job As cJob = cJob.GetJobByName(_jobName)
        If job IsNot Nothing Then
            job.SetLimits(struct1, struct2)
        End If

        Return Windows.Forms.DialogResult.OK

    End Function

End Class