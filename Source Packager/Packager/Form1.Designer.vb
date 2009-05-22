<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.txtZipPath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdGo = New System.Windows.Forms.Button
        Me.lv = New System.Windows.Forms.ListView
        Me.ColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.saveDg = New System.Windows.Forms.SaveFileDialog
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSolution = New System.Windows.Forms.TextBox
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.openDg = New System.Windows.Forms.OpenFileDialog
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(566, 11)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(28, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtZipPath
        '
        Me.txtZipPath.Location = New System.Drawing.Point(68, 12)
        Me.txtZipPath.Name = "txtZipPath"
        Me.txtZipPath.Size = New System.Drawing.Size(492, 22)
        Me.txtZipPath.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Zip path"
        '
        'cmdGo
        '
        Me.cmdGo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGo.Location = New System.Drawing.Point(519, 40)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(75, 23)
        Me.cmdGo.TabIndex = 3
        Me.cmdGo.Text = "GO !"
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'lv
        '
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader})
        Me.lv.FullRowSelect = True
        Me.lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lv.Location = New System.Drawing.Point(12, 69)
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(582, 217)
        Me.lv.TabIndex = 4
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader
        '
        Me.ColumnHeader.Text = "Action"
        Me.ColumnHeader.Width = 600
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Solution"
        '
        'txtSolution
        '
        Me.txtSolution.Location = New System.Drawing.Point(68, 42)
        Me.txtSolution.Name = "txtSolution"
        Me.txtSolution.Size = New System.Drawing.Size(411, 22)
        Me.txtSolution.TabIndex = 6
        Me.txtSolution.Text = "C:\Users\Admin\Desktop\YAPM\YAPM.sln"
        '
        'cmdOpen
        '
        Me.cmdOpen.Location = New System.Drawing.Point(485, 40)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(28, 23)
        Me.cmdOpen.TabIndex = 5
        Me.cmdOpen.Text = "..."
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'openDg
        '
        Me.openDg.FileName = "OpenFileDialog1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 298)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSolution)
        Me.Controls.Add(Me.cmdOpen)
        Me.Controls.Add(Me.lv)
        Me.Controls.Add(Me.cmdGo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtZipPath)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create a source code package for YAPM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtZipPath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents lv As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents saveDg As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSolution As System.Windows.Forms.TextBox
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents openDg As System.Windows.Forms.OpenFileDialog

End Class
