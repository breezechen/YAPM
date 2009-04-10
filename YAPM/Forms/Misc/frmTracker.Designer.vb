<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTracker
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
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdGoFeed = New System.Windows.Forms.Button
        Me.txtFeed = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdGoSug = New System.Windows.Forms.Button
        Me.txtSug = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdGoBug = New System.Windows.Forms.Button
        Me.txtBug = New System.Windows.Forms.TextBox
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
        Me.SplitContainer.Panel1.Controls.Add(Me.rtb)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdGoFeed)
        Me.SplitContainer.Panel2.Controls.Add(Me.txtFeed)
        Me.SplitContainer.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdGoSug)
        Me.SplitContainer.Panel2.Controls.Add(Me.txtSug)
        Me.SplitContainer.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdGoBug)
        Me.SplitContainer.Panel2.Controls.Add(Me.txtBug)
        Me.SplitContainer.Size = New System.Drawing.Size(443, 373)
        Me.SplitContainer.SplitterDistance = 286
        Me.SplitContainer.TabIndex = 1
        '
        'rtb
        '
        Me.rtb.BackColor = System.Drawing.Color.White
        Me.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb.Location = New System.Drawing.Point(0, 0)
        Me.rtb.Name = "rtb"
        Me.rtb.ReadOnly = True
        Me.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtb.Size = New System.Drawing.Size(443, 286)
        Me.rtb.TabIndex = 1
        Me.rtb.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Give feed back"
        '
        'cmdGoFeed
        '
        Me.cmdGoFeed.Location = New System.Drawing.Point(391, 53)
        Me.cmdGoFeed.Name = "cmdGoFeed"
        Me.cmdGoFeed.Size = New System.Drawing.Size(40, 23)
        Me.cmdGoFeed.TabIndex = 7
        Me.cmdGoFeed.Text = "Go"
        Me.cmdGoFeed.UseVisualStyleBackColor = True
        '
        'txtFeed
        '
        Me.txtFeed.Location = New System.Drawing.Point(100, 53)
        Me.txtFeed.Name = "txtFeed"
        Me.txtFeed.ReadOnly = True
        Me.txtFeed.Size = New System.Drawing.Size(285, 22)
        Me.txtFeed.TabIndex = 6
        Me.txtFeed.Text = "YetAnotherProcessMonitor@gmail.com"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Make a suggestion"
        '
        'cmdGoSug
        '
        Me.cmdGoSug.Location = New System.Drawing.Point(391, 30)
        Me.cmdGoSug.Name = "cmdGoSug"
        Me.cmdGoSug.Size = New System.Drawing.Size(40, 23)
        Me.cmdGoSug.TabIndex = 4
        Me.cmdGoSug.Text = "Go"
        Me.cmdGoSug.UseVisualStyleBackColor = True
        '
        'txtSug
        '
        Me.txtSug.Location = New System.Drawing.Point(123, 30)
        Me.txtSug.Name = "txtSug"
        Me.txtSug.ReadOnly = True
        Me.txtSug.Size = New System.Drawing.Size(262, 22)
        Me.txtSug.TabIndex = 3
        Me.txtSug.Text = "https://sourceforge.net/forum/?group_id=244697"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Report a bug"
        '
        'cmdGoBug
        '
        Me.cmdGoBug.Location = New System.Drawing.Point(391, 7)
        Me.cmdGoBug.Name = "cmdGoBug"
        Me.cmdGoBug.Size = New System.Drawing.Size(40, 23)
        Me.cmdGoBug.TabIndex = 1
        Me.cmdGoBug.Text = "Go"
        Me.cmdGoBug.UseVisualStyleBackColor = True
        '
        'txtBug
        '
        Me.txtBug.Location = New System.Drawing.Point(93, 7)
        Me.txtBug.Name = "txtBug"
        Me.txtBug.ReadOnly = True
        Me.txtBug.Size = New System.Drawing.Size(292, 22)
        Me.txtBug.TabIndex = 0
        Me.txtBug.Text = "https://sourceforge.net/tracker/?group_id=244697"
        '
        'frmTracker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 373)
        Me.Controls.Add(Me.SplitContainer)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmTracker"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bug report & feedbacks"
        Me.TopMost = True
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        Me.SplitContainer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdGoBug As System.Windows.Forms.Button
    Friend WithEvents txtBug As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdGoFeed As System.Windows.Forms.Button
    Friend WithEvents txtFeed As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdGoSug As System.Windows.Forms.Button
    Friend WithEvents txtSug As System.Windows.Forms.TextBox
End Class
