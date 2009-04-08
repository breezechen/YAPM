<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPendingTasks
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPendingTasks))
        Me.lv = New Providers.DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuStrip22 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TerminateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SuspendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ResumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip22.SuspendLayout()
        Me.SuspendLayout()
        '
        'lv
        '
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lv.ContextMenuStrip = Me.ContextMenuStrip22
        Me.lv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv.FullRowSelect = True
        Me.lv.Location = New System.Drawing.Point(0, 0)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = False
        Me.lv.Size = New System.Drawing.Size(455, 349)
        Me.lv.TabIndex = 0
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 186
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "State"
        Me.ColumnHeader2.Width = 119
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Priority"
        Me.ColumnHeader3.Width = 141
        '
        'ContextMenuStrip22
        '
        Me.ContextMenuStrip22.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TerminateToolStripMenuItem, Me.SuspendToolStripMenuItem, Me.ResumeToolStripMenuItem})
        Me.ContextMenuStrip22.Name = "ContextMenuStrip22"
        Me.ContextMenuStrip22.Size = New System.Drawing.Size(153, 92)
        '
        'TerminateToolStripMenuItem
        '
        Me.TerminateToolStripMenuItem.Image = CType(resources.GetObject("TerminateToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TerminateToolStripMenuItem.Name = "TerminateToolStripMenuItem"
        Me.TerminateToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.TerminateToolStripMenuItem.Text = "Terminate"
        '
        'SuspendToolStripMenuItem
        '
        Me.SuspendToolStripMenuItem.Enabled = False
        Me.SuspendToolStripMenuItem.Name = "SuspendToolStripMenuItem"
        Me.SuspendToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SuspendToolStripMenuItem.Text = "Suspend"
        '
        'ResumeToolStripMenuItem
        '
        Me.ResumeToolStripMenuItem.Enabled = False
        Me.ResumeToolStripMenuItem.Name = "ResumeToolStripMenuItem"
        Me.ResumeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ResumeToolStripMenuItem.Text = "Resume"
        '
        'Timer
        '
        Me.Timer.Enabled = True
        '
        'frmPendingTasks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 349)
        Me.Controls.Add(Me.lv)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmPendingTasks"
        Me.ShowIcon = False
        Me.Text = "Pending tasks for "
        Me.ContextMenuStrip22.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lv As Providers.DoubleBufferedLV
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip22 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TerminateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuspendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResumeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
