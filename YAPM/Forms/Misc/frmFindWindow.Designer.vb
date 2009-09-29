<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFindWindow
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblThread = New System.Windows.Forms.Label
        Me.lblProcess = New System.Windows.Forms.Label
        Me.cmdGoToProcess = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(262, 34)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Click on the target and release the left button of your mouse on the desired wind" & _
            "ow."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(63, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Process :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(63, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Thread :"
        '
        'lblThread
        '
        Me.lblThread.AutoSize = True
        Me.lblThread.Location = New System.Drawing.Point(120, 65)
        Me.lblThread.Name = "lblThread"
        Me.lblThread.Size = New System.Drawing.Size(0, 13)
        Me.lblThread.TabIndex = 5
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.Location = New System.Drawing.Point(120, 46)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(0, 13)
        Me.lblProcess.TabIndex = 4
        '
        'cmdGoToProcess
        '
        Me.cmdGoToProcess.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdGoToProcess.Enabled = False
        Me.cmdGoToProcess.Image = Global.My.Resources.Resources.down
        Me.cmdGoToProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdGoToProcess.Location = New System.Drawing.Point(166, 79)
        Me.cmdGoToProcess.Name = "cmdGoToProcess"
        Me.cmdGoToProcess.Size = New System.Drawing.Size(110, 25)
        Me.cmdGoToProcess.TabIndex = 6
        Me.cmdGoToProcess.Text = "    Go to process"
        Me.cmdGoToProcess.UseVisualStyleBackColor = True
        '
        'frmFindWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(286, 111)
        Me.Controls.Add(Me.cmdGoToProcess)
        Me.Controls.Add(Me.lblThread)
        Me.Controls.Add(Me.lblProcess)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFindWindow"
        Me.Opacity = 0.82
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Find a process by its window"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblThread As System.Windows.Forms.Label
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents cmdGoToProcess As System.Windows.Forms.Button
End Class
