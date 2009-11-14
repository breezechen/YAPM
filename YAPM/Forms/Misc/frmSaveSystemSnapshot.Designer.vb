<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaveSystemSnapshot
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
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.txtSSFile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.lblState = New System.Windows.Forms.Label
        Me.txtWarning = New System.Windows.Forms.TextBox
        Me.cmdOptions = New System.Windows.Forms.Button
        Me.lstOptions = New System.Windows.Forms.CheckedListBox
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(264, 10)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtSSFile
        '
        Me.txtSSFile.Location = New System.Drawing.Point(76, 12)
        Me.txtSSFile.Name = "txtSSFile"
        Me.txtSSFile.Size = New System.Drawing.Size(182, 22)
        Me.txtSSFile.TabIndex = 1
        Me.txtSSFile.Text = "C:\Users\Admin\Desktop\01.ssf"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Target file"
        '
        'cmdCreate
        '
        Me.cmdCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCreate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCreate.Image = Global.My.Resources.Resources.save16
        Me.cmdCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCreate.Location = New System.Drawing.Point(37, 171)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(75, 23)
        Me.cmdCreate.TabIndex = 5
        Me.cmdCreate.Text = "    Save"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.Location = New System.Drawing.Point(192, 171)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 6
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.Filter = "System Snapshot File (*.ssf)|*.ssf|All Files (*.*)|*.*"
        Me.SaveFileDialog.RestoreDirectory = True
        Me.SaveFileDialog.SupportMultiDottedExtensions = True
        Me.SaveFileDialog.Title = "Select location for System Snapshot File"
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.Navy
        Me.lblState.Location = New System.Drawing.Point(12, 44)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(42, 13)
        Me.lblState.TabIndex = 7
        Me.lblState.Text = "Ready."
        '
        'txtWarning
        '
        Me.txtWarning.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWarning.Location = New System.Drawing.Point(12, 70)
        Me.txtWarning.Multiline = True
        Me.txtWarning.Name = "txtWarning"
        Me.txtWarning.ReadOnly = True
        Me.txtWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWarning.Size = New System.Drawing.Size(282, 85)
        Me.txtWarning.TabIndex = 8
        '
        'cmdOptions
        '
        Me.cmdOptions.Location = New System.Drawing.Point(219, 39)
        Me.cmdOptions.Name = "cmdOptions"
        Me.cmdOptions.Size = New System.Drawing.Size(75, 23)
        Me.cmdOptions.TabIndex = 9
        Me.cmdOptions.Text = "Options >"
        Me.cmdOptions.UseVisualStyleBackColor = True
        '
        'lstOptions
        '
        Me.lstOptions.CheckOnClick = True
        Me.lstOptions.FormattingEnabled = True
        Me.lstOptions.Items.AddRange(New Object() {"EnvironmentVariables", "Handles", "Heaps", "JobLimits", "Jobs", "MemoryRegions", "Modules", "NetworkConnections", "Privileges", "Services", "Tasks", "Threads", "Windows"})
        Me.lstOptions.Location = New System.Drawing.Point(306, 5)
        Me.lstOptions.Name = "lstOptions"
        Me.lstOptions.Size = New System.Drawing.Size(167, 191)
        Me.lstOptions.Sorted = True
        Me.lstOptions.TabIndex = 10
        '
        'frmSaveSystemSnapshot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 203)
        Me.Controls.Add(Me.lstOptions)
        Me.Controls.Add(Me.cmdOptions)
        Me.Controls.Add(Me.txtWarning)
        Me.Controls.Add(Me.lblState)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSSFile)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmSaveSystemSnapshot"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Save a System Snapshot File"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents txtSSFile As System.Windows.Forms.TextBox
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblState As System.Windows.Forms.Label
    Friend WithEvents txtWarning As System.Windows.Forms.TextBox
    Friend WithEvents cmdOptions As System.Windows.Forms.Button
    Friend WithEvents lstOptions As System.Windows.Forms.CheckedListBox
End Class
