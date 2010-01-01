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
        Dim CConnection1 As cConnection = New cConnection
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFileRelease))
        Me.cmdCheck = New System.Windows.Forms.Button
        Me.lv = New searchList
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.IMG = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdFix = New System.Windows.Forms.Button
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
        Me.lv.CaseSensitive = False
        Me.lv.CatchErrors = False
        Me.lv.CheckBoxes = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        CConnection1.Type = cConnection.TypeOfConnection.LocalConnection
        Me.lv.ConnectionObj = CConnection1
        Me.lv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lv.FullRowSelect = True
        Me.lv.GridLines = True
        Me.lv.Includes = Native.Api.Enums.GeneralObjectType.Process
        Me.lv.IsConnected = False
        Me.lv.Location = New System.Drawing.Point(12, 40)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = False
        Me.lv.ReorganizeColumns = True
        Me.lv.SearchString = Nothing
        Me.lv.Size = New System.Drawing.Size(260, 143)
        Me.lv.SmallImageList = Me.IMG
        Me.lv.TabIndex = 1
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Result"
        Me.ColumnHeader2.Width = 220
        '
        'IMG
        '
        Me.IMG.ImageStream = CType(resources.GetObject("IMG.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IMG.TransparentColor = System.Drawing.Color.Transparent
        Me.IMG.Images.SetKeyName(0, "module")
        Me.IMG.Images.SetKeyName(1, "handle")
        '
        'cmdFix
        '
        Me.cmdFix.Location = New System.Drawing.Point(12, 189)
        Me.cmdFix.Name = "cmdFix"
        Me.cmdFix.Size = New System.Drawing.Size(260, 22)
        Me.cmdFix.TabIndex = 2
        Me.cmdFix.Text = "Fix checked items"
        Me.cmdFix.UseVisualStyleBackColor = True
        '
        'frmFileRelease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 224)
        Me.Controls.Add(Me.cmdFix)
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
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdFix As System.Windows.Forms.Button
    Friend WithEvents IMG As System.Windows.Forms.ImageList
    Friend WithEvents lv As searchList
End Class
