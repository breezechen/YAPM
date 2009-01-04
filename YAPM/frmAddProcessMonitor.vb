Option Strict On

Public Class frmAddProcessMonitor

    ' Process to select by default
    Public _selProcess As Integer

    Private Sub frmAddProcessMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With frmMain
            .SetToolTip(Me.chkCPUCount, "Check if you want to monitor CPU usage (percentage).")
            .SetToolTip(Me.chkCPUtime, "Check if you want to monitor CPU time (processor time).")
            .SetToolTip(Me.chkMemoryInfos, "Check if you want to monitor memory informations.")
            .SetToolTip(Me.chkPrioirty, "Check if you want to monitor priority.")
            .SetToolTip(Me.chkThreadsCount, "Check if you want to monitor thread count.")
            .SetToolTip(Me.cbProcess, "List of active processes.")
            .SetToolTip(Me.cmdRefresh, "Refresh processes list.")
            .SetToolTip(Me.butAdd, "Monitor the selected process.")
            .SetToolTip(Me.txtInterval, "Set the refresh interval (milliseconds).")
        End With

        Call Me.cmdRefresh_Click(Nothing, Nothing)

        ' Select desired process (_selProcess)
        Dim s As String
        For Each s In Me.cbProcess.Items
            Dim i As Integer = InStr(s, " -- ", CompareMethod.Binary)
            Dim _name As String = s.Substring(0, i - 1)
            Dim _pid As Integer = CInt(Val(s.Substring(i + 3, s.Length - i - 3)))
            If _pid = _selProcess Then
                Me.cbProcess.Text = s
                Exit For
            End If
        Next

    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Dim cP() As cProcess = Nothing
        Dim p As cProcess
        Me.cbProcess.Items.Clear()
        cProcess.Enumerate(cP)
        For Each p In cP
            If p.GetPid > 0 Then
                Me.cbProcess.Items.Add(p.GetName & " -- " & CStr(p.GetPid))
            End If
        Next
        If Me.cbProcess.Text.Length = 0 Then
            Me.cbProcess.SelectedIndex = 0
        End If
    End Sub

    Private Sub butAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAdd.Click
        ' Monitor our process
        Dim _pid As Integer = 0
        Dim _name As String = vbNullString

        ' Format : NAME -- PID
        If Me.cbProcess.Text.Length > 0 Then
            Dim i As Integer = InStr(Me.cbProcess.Text, " -- ", CompareMethod.Binary)

            _name = Me.cbProcess.Text.Substring(0, i - 1)
            _pid = CInt(Val(Me.cbProcess.Text.Substring(i + 3, Me.cbProcess.Text.Length - i - 3)))

            Dim it As New cMonitor(_pid, _name)
            With it
                .SetCheckCPU(Me.chkCPUCount.Checked)
                .SetCheckCPUTime(Me.chkCPUtime.Checked)
                .SetCheckMemory(Me.chkMemoryInfos.Checked)
                .SetCheckPriority(Me.chkPrioirty.Checked)
                .SetCheckThreads(Me.chkThreadsCount.Checked)
                .SetInterval(CInt(Val(Me.txtInterval.Text)))
            End With
            frmMain.AddMonitoringItem(it)

            Me.Close()
        End If

    End Sub

End Class