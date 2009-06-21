<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLog))
        Me.lv = New YAPM.DoubleBufferedLV
        Me.ColumnHeader52 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItem = New System.Windows.Forms.MenuItem
        Me.TheContextMenu = New System.Windows.Forms.ContextMenu
        Me.menuItem12 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lv
        '
        Me.lv.AllowColumnReorder = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader52, Me.ColumnHeader1})
        Me.lv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv.FullRowSelect = True
        Me.lv.HideSelection = False
        Me.lv.Location = New System.Drawing.Point(0, 0)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = True
        Me.lv.Size = New System.Drawing.Size(624, 430)
        Me.lv.TabIndex = 46
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader52
        '
        Me.ColumnHeader52.Text = "Time"
        Me.ColumnHeader52.Width = 202
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Event"
        Me.ColumnHeader1.Width = 385
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItem
        '
        Me.MenuItem.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItem, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItem.Index = 0
        Me.MenuItem.Text = "Save..."
        '
        'TheContextMenu
        '
        Me.TheContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem, Me.menuItem12, Me.MenuItem2})
        '
        'menuItem12
        '
        Me.menuItem12.Index = 1
        Me.menuItem12.Text = "-"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.Text = "Clear"
        '
        'frmLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 430)
        Me.Controls.Add(Me.lv)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Log file"
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Ribbon As System.Windows.Forms.Ribbon
    Friend WithEvents lv As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader52 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItem As System.Windows.Forms.MenuItem
    Private WithEvents TheContextMenu As System.Windows.Forms.ContextMenu
    Private WithEvents menuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
End Class
