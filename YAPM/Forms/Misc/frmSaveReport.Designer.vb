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
        Me.pgb = New System.Windows.Forms.ProgressBar
        Me.lblProgress = New System.Windows.Forms.Label
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdOpenReport = New System.Windows.Forms.Button
        Me.cmdGO = New System.Windows.Forms.Button
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'pgb
        '
        Me.pgb.Location = New System.Drawing.Point(15, 12)
        Me.pgb.Name = "pgb"
        Me.pgb.Size = New System.Drawing.Size(257, 23)
        Me.pgb.TabIndex = 1
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(12, 38)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(86, 13)
        Me.lblProgress.TabIndex = 2
        Me.lblProgress.Text = "Saved 0/0 items"
        '
        'cmdOK
        '
        Me.cmdOK.Enabled = False
        Me.cmdOK.Location = New System.Drawing.Point(223, 62)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(48, 23)
        Me.cmdOK.TabIndex = 3
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdOpenReport
        '
        Me.cmdOpenReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdOpenReport.Enabled = False
        Me.cmdOpenReport.Image = Global.My.Resources.Resources.folder_open_document
        Me.cmdOpenReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOpenReport.Location = New System.Drawing.Point(104, 62)
        Me.cmdOpenReport.Name = "cmdOpenReport"
        Me.cmdOpenReport.Size = New System.Drawing.Size(113, 23)
        Me.cmdOpenReport.TabIndex = 4
        Me.cmdOpenReport.Text = "Open report"
        Me.cmdOpenReport.UseVisualStyleBackColor = True
        '
        'cmdGO
        '
        Me.cmdGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdGO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGO.Image = Global.My.Resources.Resources.save16
        Me.cmdGO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdGO.Location = New System.Drawing.Point(12, 62)
        Me.cmdGO.Name = "cmdGO"
        Me.cmdGO.Size = New System.Drawing.Size(86, 23)
        Me.cmdGO.TabIndex = 5
        Me.cmdGO.Text = "Save"
        Me.cmdGO.UseVisualStyleBackColor = True
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.Filter = "CSV file (*.csv)|*.csv|HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        Me.SaveFileDialog.SupportMultiDottedExtensions = True
        Me.SaveFileDialog.Title = "Save report"
        '
        'frmSaveReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 94)
        Me.Controls.Add(Me.cmdGO)
        Me.Controls.Add(Me.cmdOpenReport)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.pgb)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
    Friend WithEvents pgb As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdOpenReport As System.Windows.Forms.Button
    Friend WithEvents cmdGO As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
End Class
