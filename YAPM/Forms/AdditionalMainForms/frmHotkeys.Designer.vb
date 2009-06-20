<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHotkeys
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHotkeys))
        Me.timerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.gp = New System.Windows.Forms.GroupBox
        Me.cbAction = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmdKO = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.lblKey = New System.Windows.Forms.Label
        Me.txtKey = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkAlt = New System.Windows.Forms.CheckBox
        Me.chkShift = New System.Windows.Forms.CheckBox
        Me.chkCtrl = New System.Windows.Forms.CheckBox
        Me.lv = New YAPM.DoubleBufferedLV
        Me.ColumnHeader52 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.mnuRemoveFolder = New System.Windows.Forms.MenuItem
        Me.MenuItem = New System.Windows.Forms.MenuItem
        Me.TheContextMenu = New System.Windows.Forms.ContextMenu
        Me.menuItem12 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.gp.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timerRefresh
        '
        Me.timerRefresh.Enabled = True
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "default")
        '
        'gp
        '
        Me.gp.Controls.Add(Me.cbAction)
        Me.gp.Controls.Add(Me.Label3)
        Me.gp.Controls.Add(Me.cmdKO)
        Me.gp.Controls.Add(Me.cmdAdd)
        Me.gp.Controls.Add(Me.lblKey)
        Me.gp.Controls.Add(Me.txtKey)
        Me.gp.Controls.Add(Me.Label1)
        Me.gp.Controls.Add(Me.chkAlt)
        Me.gp.Controls.Add(Me.chkShift)
        Me.gp.Controls.Add(Me.chkCtrl)
        Me.gp.Location = New System.Drawing.Point(121, 130)
        Me.gp.Name = "gp"
        Me.gp.Size = New System.Drawing.Size(300, 140)
        Me.gp.TabIndex = 6
        Me.gp.TabStop = False
        Me.gp.Text = "Add an emergency hotkey"
        Me.gp.Visible = False
        '
        'cbAction
        '
        Me.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAction.FormattingEnabled = True
        Me.cbAction.Location = New System.Drawing.Point(56, 111)
        Me.cbAction.Name = "cbAction"
        Me.cbAction.Size = New System.Drawing.Size(238, 21)
        Me.cbAction.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Action"
        '
        'cmdKO
        '
        Me.cmdKO.Location = New System.Drawing.Point(239, 49)
        Me.cmdKO.Name = "cmdKO"
        Me.cmdKO.Size = New System.Drawing.Size(55, 23)
        Me.cmdKO.TabIndex = 7
        Me.cmdKO.Text = "Cancel"
        Me.cmdKO.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Location = New System.Drawing.Point(239, 20)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(55, 23)
        Me.cmdAdd.TabIndex = 6
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'lblKey
        '
        Me.lblKey.AutoSize = True
        Me.lblKey.Location = New System.Drawing.Point(113, 82)
        Me.lblKey.Name = "lblKey"
        Me.lblKey.Size = New System.Drawing.Size(62, 13)
        Me.lblKey.TabIndex = 5
        Me.lblKey.Text = "Press a key"
        '
        'txtKey
        '
        Me.txtKey.AcceptsReturn = True
        Me.txtKey.AcceptsTab = True
        Me.txtKey.BackColor = System.Drawing.Color.White
        Me.txtKey.Location = New System.Drawing.Point(128, 47)
        Me.txtKey.MaxLength = 0
        Me.txtKey.Multiline = True
        Me.txtKey.Name = "txtKey"
        Me.txtKey.ReadOnly = True
        Me.txtKey.Size = New System.Drawing.Size(32, 32)
        Me.txtKey.TabIndex = 4
        Me.txtKey.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(83, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "+"
        '
        'chkAlt
        '
        Me.chkAlt.AutoSize = True
        Me.chkAlt.Location = New System.Drawing.Point(12, 90)
        Me.chkAlt.Name = "chkAlt"
        Me.chkAlt.Size = New System.Drawing.Size(40, 17)
        Me.chkAlt.TabIndex = 2
        Me.chkAlt.Text = "Alt"
        Me.chkAlt.UseVisualStyleBackColor = True
        '
        'chkShift
        '
        Me.chkShift.AutoSize = True
        Me.chkShift.Checked = True
        Me.chkShift.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShift.Location = New System.Drawing.Point(13, 56)
        Me.chkShift.Name = "chkShift"
        Me.chkShift.Size = New System.Drawing.Size(50, 17)
        Me.chkShift.TabIndex = 1
        Me.chkShift.Text = "Shift"
        Me.chkShift.UseVisualStyleBackColor = True
        '
        'chkCtrl
        '
        Me.chkCtrl.AutoSize = True
        Me.chkCtrl.Checked = True
        Me.chkCtrl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCtrl.Location = New System.Drawing.Point(12, 26)
        Me.chkCtrl.Name = "chkCtrl"
        Me.chkCtrl.Size = New System.Drawing.Size(65, 17)
        Me.chkCtrl.TabIndex = 0
        Me.chkCtrl.Text = "Control"
        Me.chkCtrl.UseVisualStyleBackColor = True
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
        Me.lv.Size = New System.Drawing.Size(543, 401)
        Me.lv.SmallImageList = Me.imgList
        Me.lv.TabIndex = 5
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader52
        '
        Me.ColumnHeader52.Text = "Hotkeys"
        Me.ColumnHeader52.Width = 210
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Action"
        Me.ColumnHeader1.Width = 327
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'mnuRemoveFolder
        '
        Me.VistaMenu.SetImage(Me.mnuRemoveFolder, Global.YAPM.My.Resources.Resources.cross)
        Me.mnuRemoveFolder.Index = 1
        Me.mnuRemoveFolder.Text = "Remove"
        '
        'MenuItem
        '
        Me.VistaMenu.SetImage(Me.MenuItem, Global.YAPM.My.Resources.Resources.plus_circle)
        Me.MenuItem.Index = 0
        Me.MenuItem.Text = "Add"
        '
        'TheContextMenu
        '
        Me.TheContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem, Me.mnuRemoveFolder, Me.menuItem12, Me.MenuItem2, Me.MenuItem3})
        '
        'menuItem12
        '
        Me.menuItem12.Index = 2
        Me.menuItem12.Text = "-"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 3
        Me.MenuItem2.Text = "Enable"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 4
        Me.MenuItem3.Text = "Disable"
        '
        'frmHotkeys
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(543, 401)
        Me.Controls.Add(Me.gp)
        Me.Controls.Add(Me.lv)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmHotkeys"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Emergency hotkeys list"
        Me.gp.ResumeLayout(False)
        Me.gp.PerformLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lv As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader52 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerRefresh As System.Windows.Forms.Timer
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents gp As System.Windows.Forms.GroupBox
    Friend WithEvents txtKey As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkAlt As System.Windows.Forms.CheckBox
    Friend WithEvents chkShift As System.Windows.Forms.CheckBox
    Friend WithEvents chkCtrl As System.Windows.Forms.CheckBox
    Friend WithEvents cmdKO As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents lblKey As System.Windows.Forms.Label
    Friend WithEvents cbAction As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Private WithEvents TheContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem As System.Windows.Forms.MenuItem
    Private WithEvents mnuRemoveFolder As System.Windows.Forms.MenuItem
    Private WithEvents menuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
End Class
