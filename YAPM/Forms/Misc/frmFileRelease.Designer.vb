<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileRelease
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFileRelease))
        Me.cmdCheck = New System.Windows.Forms.Button
        Me.lv = New YAPM.DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.IMG = New System.Windows.Forms.ImageList(Me.components)
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdCheck
        '
        Me.cmdCheck.Location = New System.Drawing.Point(12, 12)
        Me.cmdCheck.Name = "cmdCheck"
        Me.cmdCheck.Size = New System.Drawing.Size(260, 22)
        Me.cmdCheck.TabIndex = 0
        Me.cmdCheck.Text = "Check if file is locked by a process"
        Me.cmdCheck.UseVisualStyleBackColor = True
        '
        'lv
        '
        Me.lv.CheckBoxes = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lv.FullRowSelect = True
        Me.lv.GridLines = True
        Me.lv.Location = New System.Drawing.Point(12, 40)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = False
        Me.lv.Size = New System.Drawing.Size(260, 143)
        Me.lv.SmallImageList = Me.IMG
        Me.lv.TabIndex = 1
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Process"
        Me.ColumnHeader1.Width = 128
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Field"
        Me.ColumnHeader2.Width = 128
        '
        'IMG
        '
        Me.IMG.ImageStream = CType(resources.GetObject("IMG.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IMG.TransparentColor = System.Drawing.Color.Transparent
        Me.IMG.Images.SetKeyName(0, "module")
        Me.IMG.Images.SetKeyName(1, "handle")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 189)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(260, 22)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Fix checked items"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmFileRelease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 224)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lv)
        Me.Controls.Add(Me.cmdCheck)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmFileRelease"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Release file"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdCheck As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents IMG As System.Windows.Forms.ImageList
    Friend WithEvents lv As YAPM.DoubleBufferedLV
End Class