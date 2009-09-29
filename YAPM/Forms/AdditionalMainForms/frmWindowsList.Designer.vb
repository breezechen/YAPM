<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWindowsList
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
        Me.timerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.lv = New DoubleBufferedLV
        Me.ColumnHeader52 = New System.Windows.Forms.ColumnHeader
        Me.TheContextMenu = New System.Windows.Forms.ContextMenu
        Me.MenuItemShow = New System.Windows.Forms.MenuItem
        Me.MenuItemClose = New System.Windows.Forms.MenuItem
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timerRefresh
        '
        Me.timerRefresh.Enabled = True
        '
        'imgList
        '
        Me.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imgList.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        '
        'lv
        '
        Me.lv.AllowColumnReorder = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader52})
        Me.lv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv.FullRowSelect = True
        Me.lv.HideSelection = False
        Me.lv.Location = New System.Drawing.Point(0, 0)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = True
        Me.lv.Size = New System.Drawing.Size(320, 287)
        Me.lv.SmallImageList = Me.imgList
        Me.lv.TabIndex = 5
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader52
        '
        Me.ColumnHeader52.Text = "Caption"
        Me.ColumnHeader52.Width = 295
        '
        'TheContextMenu
        '
        Me.TheContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemShow, Me.MenuItemClose})
        '
        'MenuItemShow
        '
        Me.MenuItemShow.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemShow, Global.My.Resources.Resources.display16)
        Me.MenuItemShow.Index = 0
        Me.MenuItemShow.Text = "Show"
        '
        'MenuItemClose
        '
        Me.VistaMenu.SetImage(Me.MenuItemClose, Global.My.Resources.Resources.close)
        Me.MenuItemClose.Index = 1
        Me.MenuItemClose.Text = "Close"
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'frmWindowsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(320, 287)
        Me.Controls.Add(Me.lv)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmWindowsList"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Window list"
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lv As DoubleBufferedLV
    Friend WithEvents ColumnHeader52 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerRefresh As System.Windows.Forms.Timer
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Private WithEvents TheContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemShow As System.Windows.Forms.MenuItem
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItemClose As System.Windows.Forms.MenuItem
End Class
