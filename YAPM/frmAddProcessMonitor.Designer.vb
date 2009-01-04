<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProcessMonitor
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cbProcess = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.gpOptions = New System.Windows.Forms.GroupBox
        Me.chkCPUCount = New System.Windows.Forms.CheckBox
        Me.chkPrioirty = New System.Windows.Forms.CheckBox
        Me.chkThreadsCount = New System.Windows.Forms.CheckBox
        Me.chkMemoryInfos = New System.Windows.Forms.CheckBox
        Me.chkCPUtime = New System.Windows.Forms.CheckBox
        Me.butAdd = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInterval = New System.Windows.Forms.TextBox
        Me.gpOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbProcess
        '
        Me.cbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbProcess.FormattingEnabled = True
        Me.cbProcess.Location = New System.Drawing.Point(62, 12)
        Me.cbProcess.Name = "cbProcess"
        Me.cbProcess.Size = New System.Drawing.Size(131, 21)
        Me.cbProcess.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Process"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(199, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(68, 21)
        Me.cmdRefresh.TabIndex = 2
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'gpOptions
        '
        Me.gpOptions.Controls.Add(Me.chkCPUCount)
        Me.gpOptions.Controls.Add(Me.chkPrioirty)
        Me.gpOptions.Controls.Add(Me.chkThreadsCount)
        Me.gpOptions.Controls.Add(Me.chkMemoryInfos)
        Me.gpOptions.Controls.Add(Me.chkCPUtime)
        Me.gpOptions.Location = New System.Drawing.Point(14, 39)
        Me.gpOptions.Name = "gpOptions"
        Me.gpOptions.Size = New System.Drawing.Size(251, 92)
        Me.gpOptions.TabIndex = 3
        Me.gpOptions.TabStop = False
        Me.gpOptions.Text = "Monitor options"
        '
        'chkCPUCount
        '
        Me.chkCPUCount.AutoSize = True
        Me.chkCPUCount.Enabled = False
        Me.chkCPUCount.Location = New System.Drawing.Point(6, 66)
        Me.chkCPUCount.Name = "chkCPUCount"
        Me.chkCPUCount.Size = New System.Drawing.Size(104, 17)
        Me.chkCPUCount.TabIndex = 4
        Me.chkCPUCount.Text = "CPU percentage"
        Me.chkCPUCount.UseVisualStyleBackColor = True
        '
        'chkPrioirty
        '
        Me.chkPrioirty.AutoSize = True
        Me.chkPrioirty.Location = New System.Drawing.Point(110, 20)
        Me.chkPrioirty.Name = "chkPrioirty"
        Me.chkPrioirty.Size = New System.Drawing.Size(60, 17)
        Me.chkPrioirty.TabIndex = 3
        Me.chkPrioirty.Text = "Priority"
        Me.chkPrioirty.UseVisualStyleBackColor = True
        '
        'chkThreadsCount
        '
        Me.chkThreadsCount.AutoSize = True
        Me.chkThreadsCount.Location = New System.Drawing.Point(110, 43)
        Me.chkThreadsCount.Name = "chkThreadsCount"
        Me.chkThreadsCount.Size = New System.Drawing.Size(90, 17)
        Me.chkThreadsCount.TabIndex = 2
        Me.chkThreadsCount.Text = "Thread count"
        Me.chkThreadsCount.UseVisualStyleBackColor = True
        '
        'chkMemoryInfos
        '
        Me.chkMemoryInfos.AutoSize = True
        Me.chkMemoryInfos.Checked = True
        Me.chkMemoryInfos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMemoryInfos.Location = New System.Drawing.Point(6, 43)
        Me.chkMemoryInfos.Name = "chkMemoryInfos"
        Me.chkMemoryInfos.Size = New System.Drawing.Size(90, 17)
        Me.chkMemoryInfos.TabIndex = 1
        Me.chkMemoryInfos.Text = "Memory infos"
        Me.chkMemoryInfos.UseVisualStyleBackColor = True
        '
        'chkCPUtime
        '
        Me.chkCPUtime.AutoSize = True
        Me.chkCPUtime.Checked = True
        Me.chkCPUtime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCPUtime.Location = New System.Drawing.Point(6, 20)
        Me.chkCPUtime.Name = "chkCPUtime"
        Me.chkCPUtime.Size = New System.Drawing.Size(69, 17)
        Me.chkCPUtime.TabIndex = 0
        Me.chkCPUtime.Text = "CPU time"
        Me.chkCPUtime.UseVisualStyleBackColor = True
        '
        'butAdd
        '
        Me.butAdd.Location = New System.Drawing.Point(60, 168)
        Me.butAdd.Name = "butAdd"
        Me.butAdd.Size = New System.Drawing.Size(158, 24)
        Me.butAdd.TabIndex = 4
        Me.butAdd.Text = "Monitor process"
        Me.butAdd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 137)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Refresh interval"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(107, 134)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(158, 21)
        Me.txtInterval.TabIndex = 6
        Me.txtInterval.Text = "1000"
        '
        'frmAddProcessMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(278, 204)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.butAdd)
        Me.Controls.Add(Me.gpOptions)
        Me.Controls.Add(Me.cmdRefresh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbProcess)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddProcessMonitor"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add process monitoring"
        Me.gpOptions.ResumeLayout(False)
        Me.gpOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbProcess As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents gpOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkPrioirty As System.Windows.Forms.CheckBox
    Friend WithEvents chkThreadsCount As System.Windows.Forms.CheckBox
    Friend WithEvents chkMemoryInfos As System.Windows.Forms.CheckBox
    Friend WithEvents chkCPUtime As System.Windows.Forms.CheckBox
    Friend WithEvents butAdd As System.Windows.Forms.Button
    Friend WithEvents chkCPUCount As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
End Class
