<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKillProcessByMethod
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
        Me.cmdKill = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.lstMethods = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'cmdKill
        '
        Me.cmdKill.Enabled = False
        Me.cmdKill.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdKill.Image = Global.YAPM.My.Resources.Resources.cross
        Me.cmdKill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdKill.Location = New System.Drawing.Point(12, 173)
        Me.cmdKill.Name = "cmdKill"
        Me.cmdKill.Size = New System.Drawing.Size(127, 26)
        Me.cmdKill.TabIndex = 1
        Me.cmdKill.Text = "    Kill process"
        Me.cmdKill.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(285, 173)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(88, 26)
        Me.cmdExit.TabIndex = 2
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'lstMethods
        '
        Me.lstMethods.CheckBoxes = True
        Me.lstMethods.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lstMethods.FullRowSelect = True
        Me.lstMethods.Location = New System.Drawing.Point(12, 12)
        Me.lstMethods.MultiSelect = False
        Me.lstMethods.Name = "lstMethods"
        Me.lstMethods.Size = New System.Drawing.Size(361, 155)
        Me.lstMethods.TabIndex = 3
        Me.lstMethods.UseCompatibleStateImageBehavior = False
        Me.lstMethods.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Method"
        Me.ColumnHeader1.Width = 341
        '
        'frmKillProcessByMethod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 211)
        Me.Controls.Add(Me.lstMethods)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdKill)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmKillProcessByMethod"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kill process [NAME] ([PID])"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdKill As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents lstMethods As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
End Class
