Option Strict On

Public Class frmAddProcessMonitor

    Private Sub frmAddProcessMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Me.cmdRefresh_Click(Nothing, Nothing)
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