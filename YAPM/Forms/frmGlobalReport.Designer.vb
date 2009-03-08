<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGlobalReport
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkAllWindows = New System.Windows.Forms.CheckBox
        Me.chkAllHandles = New System.Windows.Forms.CheckBox
        Me.chkFull = New System.Windows.Forms.CheckBox
        Me.chkModules = New System.Windows.Forms.CheckBox
        Me.chkThreads = New System.Windows.Forms.CheckBox
        Me.chkHandles = New System.Windows.Forms.CheckBox
        Me.chkMemory = New System.Windows.Forms.CheckBox
        Me.chkWindows = New System.Windows.Forms.CheckBox
        Me.chkServices = New System.Windows.Forms.CheckBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.pgb = New System.Windows.Forms.ProgressBar
        Me.lblState = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkAllWindows)
        Me.GroupBox1.Controls.Add(Me.chkAllHandles)
        Me.GroupBox1.Controls.Add(Me.chkFull)
        Me.GroupBox1.Controls.Add(Me.chkModules)
        Me.GroupBox1.Controls.Add(Me.chkThreads)
        Me.GroupBox1.Controls.Add(Me.chkHandles)
        Me.GroupBox1.Controls.Add(Me.chkMemory)
        Me.GroupBox1.Controls.Add(Me.chkWindows)
        Me.GroupBox1.Controls.Add(Me.chkServices)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 163)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Informations to save"
        '
        'chkAllWindows
        '
        Me.chkAllWindows.AutoSize = True
        Me.chkAllWindows.Location = New System.Drawing.Point(9, 90)
        Me.chkAllWindows.Name = "chkAllWindows"
        Me.chkAllWindows.Size = New System.Drawing.Size(126, 17)
        Me.chkAllWindows.TabIndex = 9
        Me.chkAllWindows.Text = "Unnamed windows"
        Me.chkAllWindows.UseVisualStyleBackColor = True
        '
        'chkAllHandles
        '
        Me.chkAllHandles.AutoSize = True
        Me.chkAllHandles.Location = New System.Drawing.Point(9, 113)
        Me.chkAllHandles.Name = "chkAllHandles"
        Me.chkAllHandles.Size = New System.Drawing.Size(120, 17)
        Me.chkAllHandles.TabIndex = 8
        Me.chkAllHandles.Text = "Unnamed handles"
        Me.chkAllHandles.UseVisualStyleBackColor = True
        '
        'chkFull
        '
        Me.chkFull.AutoSize = True
        Me.chkFull.Location = New System.Drawing.Point(9, 136)
        Me.chkFull.Name = "chkFull"
        Me.chkFull.Size = New System.Drawing.Size(80, 17)
        Me.chkFull.TabIndex = 7
        Me.chkFull.Text = "Full report"
        Me.chkFull.UseVisualStyleBackColor = True
        '
        'chkModules
        '
        Me.chkModules.AutoSize = True
        Me.chkModules.Checked = True
        Me.chkModules.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModules.Location = New System.Drawing.Point(118, 67)
        Me.chkModules.Name = "chkModules"
        Me.chkModules.Size = New System.Drawing.Size(71, 17)
        Me.chkModules.TabIndex = 6
        Me.chkModules.Text = "Modules"
        Me.chkModules.UseVisualStyleBackColor = True
        '
        'chkThreads
        '
        Me.chkThreads.AutoSize = True
        Me.chkThreads.Checked = True
        Me.chkThreads.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkThreads.Location = New System.Drawing.Point(118, 44)
        Me.chkThreads.Name = "chkThreads"
        Me.chkThreads.Size = New System.Drawing.Size(66, 17)
        Me.chkThreads.TabIndex = 5
        Me.chkThreads.Text = "Threads"
        Me.chkThreads.UseVisualStyleBackColor = True
        '
        'chkHandles
        '
        Me.chkHandles.AutoSize = True
        Me.chkHandles.Checked = True
        Me.chkHandles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHandles.Location = New System.Drawing.Point(118, 21)
        Me.chkHandles.Name = "chkHandles"
        Me.chkHandles.Size = New System.Drawing.Size(68, 17)
        Me.chkHandles.TabIndex = 4
        Me.chkHandles.Text = "Handles"
        Me.chkHandles.UseVisualStyleBackColor = True
        '
        'chkMemory
        '
        Me.chkMemory.AutoSize = True
        Me.chkMemory.Checked = True
        Me.chkMemory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMemory.Location = New System.Drawing.Point(9, 67)
        Me.chkMemory.Name = "chkMemory"
        Me.chkMemory.Size = New System.Drawing.Size(96, 17)
        Me.chkMemory.TabIndex = 3
        Me.chkMemory.Text = "Mem. regions"
        Me.chkMemory.UseVisualStyleBackColor = True
        '
        'chkWindows
        '
        Me.chkWindows.AutoSize = True
        Me.chkWindows.Checked = True
        Me.chkWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWindows.Location = New System.Drawing.Point(9, 44)
        Me.chkWindows.Name = "chkWindows"
        Me.chkWindows.Size = New System.Drawing.Size(75, 17)
        Me.chkWindows.TabIndex = 2
        Me.chkWindows.Text = "Windows"
        Me.chkWindows.UseVisualStyleBackColor = True
        '
        'chkServices
        '
        Me.chkServices.AutoSize = True
        Me.chkServices.Checked = True
        Me.chkServices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkServices.Location = New System.Drawing.Point(9, 21)
        Me.chkServices.Name = "chkServices"
        Me.chkServices.Size = New System.Drawing.Size(66, 17)
        Me.chkServices.TabIndex = 0
        Me.chkServices.Text = "Services"
        Me.chkServices.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(21, 230)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save..."
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(130, 230)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'pgb
        '
        Me.pgb.Location = New System.Drawing.Point(12, 178)
        Me.pgb.Name = "pgb"
        Me.pgb.Size = New System.Drawing.Size(200, 23)
        Me.pgb.TabIndex = 4
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Location = New System.Drawing.Point(18, 209)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(0, 13)
        Me.lblState.TabIndex = 5
        '
        'frmGlobalReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(227, 264)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.pgb)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmGlobalReport"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "General system report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents pgb As System.Windows.Forms.ProgressBar
    Friend WithEvents chkServices As System.Windows.Forms.CheckBox
    Friend WithEvents chkModules As System.Windows.Forms.CheckBox
    Friend WithEvents chkThreads As System.Windows.Forms.CheckBox
    Friend WithEvents chkHandles As System.Windows.Forms.CheckBox
    Friend WithEvents chkMemory As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindows As System.Windows.Forms.CheckBox
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents chkFull As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllWindows As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllHandles As System.Windows.Forms.CheckBox
End Class
