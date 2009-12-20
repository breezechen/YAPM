<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchMonitor
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
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Category", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Counter", System.Windows.Forms.HorizontalAlignment.Left)
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtToSearch = New System.Windows.Forms.TextBox
        Me.chkCase = New System.Windows.Forms.CheckBox
        Me.cmdGo = New System.Windows.Forms.Button
        Me.LV = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Text to search"
        '
        'txtToSearch
        '
        Me.txtToSearch.Location = New System.Drawing.Point(93, 6)
        Me.txtToSearch.Name = "txtToSearch"
        Me.txtToSearch.Size = New System.Drawing.Size(131, 22)
        Me.txtToSearch.TabIndex = 1
        '
        'chkCase
        '
        Me.chkCase.AutoSize = True
        Me.chkCase.Location = New System.Drawing.Point(230, 8)
        Me.chkCase.Name = "chkCase"
        Me.chkCase.Size = New System.Drawing.Size(97, 17)
        Me.chkCase.TabIndex = 2
        Me.chkCase.Text = "Case sensitive"
        Me.chkCase.UseVisualStyleBackColor = True
        '
        'cmdGo
        '
        Me.cmdGo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdGo.Image = Global.My.Resources.Resources.magnifier
        Me.cmdGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdGo.Location = New System.Drawing.Point(330, 6)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(48, 23)
        Me.cmdGo.TabIndex = 3
        Me.cmdGo.Text = "Go"
        Me.cmdGo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'LV
        '
        Me.LV.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        ListViewGroup3.Header = "Category"
        ListViewGroup3.Name = "groupCategory"
        ListViewGroup4.Header = "Counter"
        ListViewGroup4.Name = "groupCounter"
        Me.LV.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.LV.Location = New System.Drawing.Point(15, 32)
        Me.LV.Name = "LV"
        Me.LV.Size = New System.Drawing.Size(363, 165)
        Me.LV.TabIndex = 4
        Me.LV.UseCompatibleStateImageBehavior = False
        Me.LV.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 302
        '
        'frmSearchMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 210)
        Me.Controls.Add(Me.LV)
        Me.Controls.Add(Me.cmdGo)
        Me.Controls.Add(Me.chkCase)
        Me.Controls.Add(Me.txtToSearch)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearchMonitor"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Search a counter"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtToSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkCase As System.Windows.Forms.CheckBox
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents LV As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
End Class
