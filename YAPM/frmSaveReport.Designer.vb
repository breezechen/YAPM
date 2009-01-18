<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaveReport
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.pgb = New System.Windows.Forms.ProgressBar
        Me.lblProgress = New System.Windows.Forms.Label
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdOpenReport = New System.Windows.Forms.Button
        Me.cmdGO = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Saving report..."
        '
        'pgb
        '
        Me.pgb.Location = New System.Drawing.Point(15, 34)
        Me.pgb.Name = "pgb"
        Me.pgb.Size = New System.Drawing.Size(257, 23)
        Me.pgb.TabIndex = 1
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(12, 60)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(84, 13)
        Me.lblProgress.TabIndex = 2
        Me.lblProgress.Text = "Saved 0/0 items"
        '
        'cmdOK
        '
        Me.cmdOK.Enabled = False
        Me.cmdOK.Location = New System.Drawing.Point(196, 84)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdOpenReport
        '
        Me.cmdOpenReport.Enabled = False
        Me.cmdOpenReport.Location = New System.Drawing.Point(115, 84)
        Me.cmdOpenReport.Name = "cmdOpenReport"
        Me.cmdOpenReport.Size = New System.Drawing.Size(75, 23)
        Me.cmdOpenReport.TabIndex = 4
        Me.cmdOpenReport.Text = "Open report"
        Me.cmdOpenReport.UseVisualStyleBackColor = True
        '
        'cmdGO
        '
        Me.cmdGO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGO.Location = New System.Drawing.Point(12, 84)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(97, 23)
        Me.cmdGO.TabIndex = 5
        Me.cmdGO.Text = "Save"
        Me.cmdGO.UseVisualStyleBackColor = True
        '
        'frmSaveReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 116)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.cmdOpenReport)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.pgb)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSaveReport"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Save report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pgb As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdOpenReport As System.Windows.Forms.Button
    Friend WithEvents cmdGO As System.Windows.Forms.Button
End Class
