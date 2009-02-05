<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreferences
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
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtServiceIntervall = New System.Windows.Forms.TextBox
        Me.txtProcessIntervall = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkTopMost = New System.Windows.Forms.CheckBox
        Me.chkHideDetails = New System.Windows.Forms.CheckBox
        Me.chkStartTray = New System.Windows.Forms.CheckBox
        Me.chkReplaceTaskmgr = New System.Windows.Forms.CheckBox
        Me.chkStart = New System.Windows.Forms.CheckBox
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.cmdDownload = New System.Windows.Forms.Button
        Me.cmdCheckUpdate = New System.Windows.Forms.Button
        Me.txtUpdate = New System.Windows.Forms.TextBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdQuit = New System.Windows.Forms.Button
        Me.cmdDefaut = New System.Windows.Forms.Button
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Location = New System.Drawing.Point(9, 9)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(336, 253)
        Me.TabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(328, 227)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtServiceIntervall)
        Me.GroupBox2.Controls.Add(Me.txtProcessIntervall)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 153)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(301, 65)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Intervalls"
        '
        'txtServiceIntervall
        '
        Me.txtServiceIntervall.Location = New System.Drawing.Point(170, 37)
        Me.txtServiceIntervall.Name = "txtServiceIntervall"
        Me.txtServiceIntervall.Size = New System.Drawing.Size(120, 21)
        Me.txtServiceIntervall.TabIndex = 6
        '
        'txtProcessIntervall
        '
        Me.txtProcessIntervall.Location = New System.Drawing.Point(170, 14)
        Me.txtProcessIntervall.Name = "txtProcessIntervall"
        Me.txtProcessIntervall.Size = New System.Drawing.Size(120, 21)
        Me.txtProcessIntervall.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Services refreshment intervall"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Processes refreshment intervall"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkTopMost)
        Me.GroupBox1.Controls.Add(Me.chkHideDetails)
        Me.GroupBox1.Controls.Add(Me.chkStartTray)
        Me.GroupBox1.Controls.Add(Me.chkReplaceTaskmgr)
        Me.GroupBox1.Controls.Add(Me.chkStart)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(301, 135)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Startup"
        '
        'chkTopMost
        '
        Me.chkTopMost.AutoSize = True
        Me.chkTopMost.Location = New System.Drawing.Point(9, 114)
        Me.chkTopMost.Name = "chkTopMost"
        Me.chkTopMost.Size = New System.Drawing.Size(122, 17)
        Me.chkTopMost.TabIndex = 4
        Me.chkTopMost.Text = "Start YAPM topmost"
        Me.chkTopMost.UseVisualStyleBackColor = True
        '
        'chkHideDetails
        '
        Me.chkHideDetails.AutoSize = True
        Me.chkHideDetails.Location = New System.Drawing.Point(9, 91)
        Me.chkHideDetails.Name = "chkHideDetails"
        Me.chkHideDetails.Size = New System.Drawing.Size(182, 17)
        Me.chkHideDetails.TabIndex = 3
        Me.chkHideDetails.Text = "Start with process details hidden"
        Me.chkHideDetails.UseVisualStyleBackColor = True
        '
        'chkStartTray
        '
        Me.chkStartTray.AutoSize = True
        Me.chkStartTray.Location = New System.Drawing.Point(9, 68)
        Me.chkStartTray.Name = "chkStartTray"
        Me.chkStartTray.Size = New System.Drawing.Size(118, 17)
        Me.chkStartTray.TabIndex = 2
        Me.chkStartTray.Text = "Start YAPM on tray"
        Me.chkStartTray.UseVisualStyleBackColor = True
        '
        'chkReplaceTaskmgr
        '
        Me.chkReplaceTaskmgr.AutoSize = True
        Me.chkReplaceTaskmgr.Location = New System.Drawing.Point(9, 45)
        Me.chkReplaceTaskmgr.Name = "chkReplaceTaskmgr"
        Me.chkReplaceTaskmgr.Size = New System.Drawing.Size(105, 17)
        Me.chkReplaceTaskmgr.TabIndex = 1
        Me.chkReplaceTaskmgr.Text = "Replace taskmgr"
        Me.chkReplaceTaskmgr.UseVisualStyleBackColor = True
        '
        'chkStart
        '
        Me.chkStart.AutoSize = True
        Me.chkStart.Location = New System.Drawing.Point(9, 20)
        Me.chkStart.Name = "chkStart"
        Me.chkStart.Size = New System.Drawing.Size(179, 17)
        Me.chkStart.TabIndex = 0
        Me.chkStart.Text = "Start YAPM on Windows startup"
        Me.chkStart.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmdDownload)
        Me.TabPage2.Controls.Add(Me.cmdCheckUpdate)
        Me.TabPage2.Controls.Add(Me.txtUpdate)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(328, 227)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Update"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdDownload
        '
        Me.cmdDownload.Location = New System.Drawing.Point(186, 196)
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(136, 25)
        Me.cmdDownload.TabIndex = 12
        Me.cmdDownload.Text = "Download last update"
        Me.cmdDownload.UseVisualStyleBackColor = True
        '
        'cmdCheckUpdate
        '
        Me.cmdCheckUpdate.Location = New System.Drawing.Point(6, 196)
        Me.cmdCheckUpdate.Name = "cmdCheckUpdate"
        Me.cmdCheckUpdate.Size = New System.Drawing.Size(158, 25)
        Me.cmdCheckUpdate.TabIndex = 11
        Me.cmdCheckUpdate.Text = "Check is YAPM is up to date"
        Me.cmdCheckUpdate.UseVisualStyleBackColor = True
        '
        'txtUpdate
        '
        Me.txtUpdate.BackColor = System.Drawing.Color.White
        Me.txtUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtUpdate.Location = New System.Drawing.Point(6, 6)
        Me.txtUpdate.Multiline = True
        Me.txtUpdate.Name = "txtUpdate"
        Me.txtUpdate.ReadOnly = True
        Me.txtUpdate.Size = New System.Drawing.Size(316, 184)
        Me.txtUpdate.TabIndex = 10
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(12, 270)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(100, 26)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdQuit
        '
        Me.cmdQuit.Location = New System.Drawing.Point(242, 270)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(100, 26)
        Me.cmdQuit.TabIndex = 9
        Me.cmdQuit.Text = "Close"
        Me.cmdQuit.UseVisualStyleBackColor = True
        '
        'cmdDefaut
        '
        Me.cmdDefaut.Location = New System.Drawing.Point(127, 270)
        Me.cmdDefaut.Name = "cmdDefaut"
        Me.cmdDefaut.Size = New System.Drawing.Size(100, 26)
        Me.cmdDefaut.TabIndex = 8
        Me.cmdDefaut.Text = "Default"
        Me.cmdDefaut.UseVisualStyleBackColor = True
        '
        'frmPreferences
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 304)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdDefaut)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.TabControl)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPreferences"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preferences"
        Me.TopMost = True
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdDefaut As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkReplaceTaskmgr As System.Windows.Forms.CheckBox
    Friend WithEvents chkStart As System.Windows.Forms.CheckBox
    Friend WithEvents chkStartTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkHideDetails As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtServiceIntervall As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessIntervall As System.Windows.Forms.TextBox
    Friend WithEvents chkTopMost As System.Windows.Forms.CheckBox
    Friend WithEvents cmdDownload As System.Windows.Forms.Button
    Friend WithEvents cmdCheckUpdate As System.Windows.Forms.Button
    Friend WithEvents txtUpdate As System.Windows.Forms.TextBox
End Class
