<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSnapshotInfos
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
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.tv = New System.Windows.Forms.TreeView
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdBrowseSSFile = New System.Windows.Forms.Button
        Me.txtSSFile = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmdGo = New System.Windows.Forms.Button
        Me.openFile = New System.Windows.Forms.OpenFileDialog
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer.IsSplitterFixed = True
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.tv)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdGo)
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdBrowseSSFile)
        Me.SplitContainer.Panel2.Controls.Add(Me.txtSSFile)
        Me.SplitContainer.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdOK)
        Me.SplitContainer.Size = New System.Drawing.Size(481, 359)
        Me.SplitContainer.SplitterDistance = 327
        Me.SplitContainer.TabIndex = 0
        '
        'tv
        '
        Me.tv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.Location = New System.Drawing.Point(0, 0)
        Me.tv.Name = "tv"
        Me.tv.Size = New System.Drawing.Size(481, 327)
        Me.tv.TabIndex = 0
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(415, 1)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(54, 23)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "Exit"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdBrowseSSFile
        '
        Me.cmdBrowseSSFile.Location = New System.Drawing.Point(281, 2)
        Me.cmdBrowseSSFile.Name = "cmdBrowseSSFile"
        Me.cmdBrowseSSFile.Size = New System.Drawing.Size(27, 23)
        Me.cmdBrowseSSFile.TabIndex = 15
        Me.cmdBrowseSSFile.Text = "..."
        Me.cmdBrowseSSFile.UseVisualStyleBackColor = True
        '
        'txtSSFile
        '
        Me.txtSSFile.Location = New System.Drawing.Point(133, 4)
        Me.txtSSFile.Name = "txtSSFile"
        Me.txtSSFile.Size = New System.Drawing.Size(142, 22)
        Me.txtSSFile.TabIndex = 14
        Me.txtSSFile.Text = "C:\Users\Admin\Desktop\01.ssf"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "System Snapshot File"
        '
        'cmdGo
        '
        Me.cmdGo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGo.Location = New System.Drawing.Point(314, 2)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(77, 23)
        Me.cmdGo.TabIndex = 16
        Me.cmdGo.Text = "Explore"
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'openFile
        '
        Me.openFile.Filter = "System Snapshot File (*.ssf)|*.ssf|All Files (*.*)|*.*"
        Me.openFile.RestoreDirectory = True
        Me.openFile.SupportMultiDottedExtensions = True
        Me.openFile.Title = "Choose System Snapshot File to open"
        '
        'frmSnapshotInfos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 359)
        Me.Controls.Add(Me.SplitContainer)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(497, 397)
        Me.Name = "frmSnapshotInfos"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "System Snapshot File information"
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        Me.SplitContainer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseSSFile As System.Windows.Forms.Button
    Friend WithEvents txtSSFile As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents openFile As System.Windows.Forms.OpenFileDialog
End Class
