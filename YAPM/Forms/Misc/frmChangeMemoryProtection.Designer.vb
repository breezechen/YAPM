<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeMemoryProtection
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
        Me.ChooseFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdChangeProtection = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.chkGuard = New System.Windows.Forms.CheckBox
        Me.optE = New System.Windows.Forms.RadioButton
        Me.optER = New System.Windows.Forms.RadioButton
        Me.optEWC = New System.Windows.Forms.RadioButton
        Me.optERW = New System.Windows.Forms.RadioButton
        Me.optWC = New System.Windows.Forms.RadioButton
        Me.optRW = New System.Windows.Forms.RadioButton
        Me.optRO = New System.Windows.Forms.RadioButton
        Me.optNA = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.chkNoCache = New System.Windows.Forms.CheckBox
        Me.chkWriteCombine = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Choose new protection type"
        '
        'cmdChangeProtection
        '
        Me.cmdChangeProtection.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdChangeProtection.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdChangeProtection.Image = Global.YAPM.My.Resources.Resources.locked
        Me.cmdChangeProtection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdChangeProtection.Location = New System.Drawing.Point(15, 238)
        Me.cmdChangeProtection.Name = "cmdChangeProtection"
        Me.cmdChangeProtection.Size = New System.Drawing.Size(148, 23)
        Me.cmdChangeProtection.TabIndex = 5
        Me.cmdChangeProtection.Text = "    Change protection"
        Me.cmdChangeProtection.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.Location = New System.Drawing.Point(217, 238)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 6
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'chkGuard
        '
        Me.chkGuard.AutoSize = True
        Me.chkGuard.Location = New System.Drawing.Point(15, 160)
        Me.chkGuard.Name = "chkGuard"
        Me.chkGuard.Size = New System.Drawing.Size(58, 17)
        Me.chkGuard.TabIndex = 7
        Me.chkGuard.Text = "Guard"
        Me.chkGuard.UseVisualStyleBackColor = True
        '
        'optE
        '
        Me.optE.AutoSize = True
        Me.optE.Location = New System.Drawing.Point(15, 34)
        Me.optE.Name = "optE"
        Me.optE.Size = New System.Drawing.Size(64, 17)
        Me.optE.TabIndex = 9
        Me.optE.TabStop = True
        Me.optE.Text = "Execute"
        Me.optE.UseVisualStyleBackColor = True
        '
        'optER
        '
        Me.optER.AutoSize = True
        Me.optER.Location = New System.Drawing.Point(15, 57)
        Me.optER.Name = "optER"
        Me.optER.Size = New System.Drawing.Size(90, 17)
        Me.optER.TabIndex = 10
        Me.optER.TabStop = True
        Me.optER.Text = "ExecuteRead"
        Me.optER.UseVisualStyleBackColor = True
        '
        'optEWC
        '
        Me.optEWC.AutoSize = True
        Me.optEWC.Location = New System.Drawing.Point(15, 103)
        Me.optEWC.Name = "optEWC"
        Me.optEWC.Size = New System.Drawing.Size(118, 17)
        Me.optEWC.TabIndex = 12
        Me.optEWC.TabStop = True
        Me.optEWC.Text = "ExecuteWriteCopy"
        Me.optEWC.UseVisualStyleBackColor = True
        '
        'optERW
        '
        Me.optERW.AutoSize = True
        Me.optERW.Location = New System.Drawing.Point(15, 80)
        Me.optERW.Name = "optERW"
        Me.optERW.Size = New System.Drawing.Size(118, 17)
        Me.optERW.TabIndex = 11
        Me.optERW.TabStop = True
        Me.optERW.Text = "ExecuteReadWrite"
        Me.optERW.UseVisualStyleBackColor = True
        '
        'optWC
        '
        Me.optWC.AutoSize = True
        Me.optWC.Location = New System.Drawing.Point(144, 103)
        Me.optWC.Name = "optWC"
        Me.optWC.Size = New System.Drawing.Size(79, 17)
        Me.optWC.TabIndex = 16
        Me.optWC.TabStop = True
        Me.optWC.Text = "WriteCopy"
        Me.optWC.UseVisualStyleBackColor = True
        '
        'optRW
        '
        Me.optRW.AutoSize = True
        Me.optRW.Location = New System.Drawing.Point(144, 80)
        Me.optRW.Name = "optRW"
        Me.optRW.Size = New System.Drawing.Size(79, 17)
        Me.optRW.TabIndex = 15
        Me.optRW.TabStop = True
        Me.optRW.Text = "ReadWrite"
        Me.optRW.UseVisualStyleBackColor = True
        '
        'optRO
        '
        Me.optRO.AutoSize = True
        Me.optRO.Location = New System.Drawing.Point(144, 57)
        Me.optRO.Name = "optRO"
        Me.optRO.Size = New System.Drawing.Size(75, 17)
        Me.optRO.TabIndex = 14
        Me.optRO.TabStop = True
        Me.optRO.Text = "ReadOnly"
        Me.optRO.UseVisualStyleBackColor = True
        '
        'optNA
        '
        Me.optNA.AutoSize = True
        Me.optNA.Location = New System.Drawing.Point(144, 34)
        Me.optNA.Name = "optNA"
        Me.optNA.Size = New System.Drawing.Size(73, 17)
        Me.optNA.TabIndex = 13
        Me.optNA.TabStop = True
        Me.optNA.Text = "NoAccess"
        Me.optNA.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(158, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "You can combine it with flags"
        '
        'chkNoCache
        '
        Me.chkNoCache.AutoSize = True
        Me.chkNoCache.Location = New System.Drawing.Point(15, 183)
        Me.chkNoCache.Name = "chkNoCache"
        Me.chkNoCache.Size = New System.Drawing.Size(72, 17)
        Me.chkNoCache.TabIndex = 18
        Me.chkNoCache.Text = "NoCache"
        Me.chkNoCache.UseVisualStyleBackColor = True
        '
        'chkWriteCombine
        '
        Me.chkWriteCombine.AutoSize = True
        Me.chkWriteCombine.Location = New System.Drawing.Point(15, 206)
        Me.chkWriteCombine.Name = "chkWriteCombine"
        Me.chkWriteCombine.Size = New System.Drawing.Size(100, 17)
        Me.chkWriteCombine.TabIndex = 19
        Me.chkWriteCombine.Text = "WriteCombine"
        Me.chkWriteCombine.UseVisualStyleBackColor = True
        '
        'frmChangeMemoryProtection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 273)
        Me.Controls.Add(Me.chkWriteCombine)
        Me.Controls.Add(Me.chkNoCache)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.optWC)
        Me.Controls.Add(Me.optRW)
        Me.Controls.Add(Me.optRO)
        Me.Controls.Add(Me.optNA)
        Me.Controls.Add(Me.optEWC)
        Me.Controls.Add(Me.optERW)
        Me.Controls.Add(Me.optER)
        Me.Controls.Add(Me.optE)
        Me.Controls.Add(Me.chkGuard)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdChangeProtection)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmChangeMemoryProtection"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change memory protection type"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ChooseFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdChangeProtection As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents chkGuard As System.Windows.Forms.CheckBox
    Friend WithEvents optE As System.Windows.Forms.RadioButton
    Friend WithEvents optER As System.Windows.Forms.RadioButton
    Friend WithEvents optEWC As System.Windows.Forms.RadioButton
    Friend WithEvents optERW As System.Windows.Forms.RadioButton
    Friend WithEvents optWC As System.Windows.Forms.RadioButton
    Friend WithEvents optRW As System.Windows.Forms.RadioButton
    Friend WithEvents optRO As System.Windows.Forms.RadioButton
    Friend WithEvents optNA As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkNoCache As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriteCombine As System.Windows.Forms.CheckBox
End Class
