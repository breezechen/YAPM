<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScripting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScripting))
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItemOpen = New System.Windows.Forms.MenuItem
        Me.MenuItemSave = New System.Windows.Forms.MenuItem
        Me.MenuItemSaveAs = New System.Windows.Forms.MenuItem
        Me.MenuItemExit = New System.Windows.Forms.MenuItem
        Me.MenuItemVerify = New System.Windows.Forms.MenuItem
        Me.MenuItemExecute = New System.Windows.Forms.MenuItem
        Me.mnuSystem = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItemFile = New System.Windows.Forms.MenuItem
        Me.MenuItem56 = New System.Windows.Forms.MenuItem
        Me.MenuItemScripts = New System.Windows.Forms.MenuItem
        Me.StatusBar = New System.Windows.Forms.StatusBar
        Me.sbPanelLines = New System.Windows.Forms.StatusBarPanel
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.cmdOpen = New System.Windows.Forms.ToolStripButton
        Me.cmdSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdCheckScript = New System.Windows.Forms.ToolStripButton
        Me.cmdExecute = New System.Windows.Forms.ToolStripButton
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelLines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItemOpen
        '
        Me.MenuItemOpen.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemOpen, Global.My.Resources.Resources.folder_open_document)
        Me.MenuItemOpen.Index = 0
        Me.MenuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.MenuItemOpen.Text = "&Open script..."
        '
        'MenuItemSave
        '
        Me.VistaMenu.SetImage(Me.MenuItemSave, Global.My.Resources.Resources.save16)
        Me.MenuItemSave.Index = 1
        Me.MenuItemSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.MenuItemSave.Text = "&Save script"
        '
        'MenuItemSaveAs
        '
        Me.VistaMenu.SetImage(Me.MenuItemSaveAs, Global.My.Resources.Resources.save16)
        Me.MenuItemSaveAs.Index = 2
        Me.MenuItemSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS
        Me.MenuItemSaveAs.Text = "&Save as..."
        '
        'MenuItemExit
        '
        Me.VistaMenu.SetImage(Me.MenuItemExit, Global.My.Resources.Resources.cross16)
        Me.MenuItemExit.Index = 4
        Me.MenuItemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4
        Me.MenuItemExit.Text = "E&xit"
        '
        'MenuItemVerify
        '
        Me.VistaMenu.SetImage(Me.MenuItemVerify, Global.My.Resources.Resources.ok16)
        Me.MenuItemVerify.Index = 0
        Me.MenuItemVerify.Shortcut = System.Windows.Forms.Shortcut.F3
        Me.MenuItemVerify.Text = "&Verify"
        '
        'MenuItemExecute
        '
        Me.MenuItemExecute.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemExecute, Global.My.Resources.Resources.right16)
        Me.MenuItemExecute.Index = 1
        Me.MenuItemExecute.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.MenuItemExecute.Text = "&Execute"
        '
        'mnuSystem
        '
        Me.mnuSystem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFile, Me.MenuItemScripts})
        '
        'MenuItemFile
        '
        Me.MenuItemFile.Index = 0
        Me.MenuItemFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemOpen, Me.MenuItemSave, Me.MenuItemSaveAs, Me.MenuItem56, Me.MenuItemExit})
        Me.MenuItemFile.Text = "&File"
        '
        'MenuItem56
        '
        Me.MenuItem56.Index = 3
        Me.MenuItem56.Text = "-"
        '
        'MenuItemScripts
        '
        Me.MenuItemScripts.Index = 1
        Me.MenuItemScripts.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemVerify, Me.MenuItemExecute})
        Me.MenuItemScripts.Text = "&Script"
        '
        'StatusBar
        '
        Me.StatusBar.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.Location = New System.Drawing.Point(0, 358)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbPanelLines})
        Me.StatusBar.ShowPanels = True
        Me.StatusBar.Size = New System.Drawing.Size(644, 20)
        Me.StatusBar.TabIndex = 63
        '
        'sbPanelLines
        '
        Me.sbPanelLines.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelLines.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.sbPanelLines.MinWidth = 100
        Me.sbPanelLines.Name = "sbPanelLines"
        Me.sbPanelLines.Text = "Lines : 1"
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdOpen, Me.cmdSave, Me.ToolStripSeparator1, Me.cmdCheckScript, Me.cmdExecute})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(644, 25)
        Me.ToolStrip.TabIndex = 64
        '
        'cmdOpen
        '
        Me.cmdOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdOpen.Image = Global.My.Resources.Resources.folder_open_document
        Me.cmdOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(23, 22)
        Me.cmdOpen.Text = "Open script"
        Me.cmdOpen.ToolTipText = "Open script"
        '
        'cmdSave
        '
        Me.cmdSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSave.Image = Global.My.Resources.Resources.save16
        Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(23, 22)
        Me.cmdSave.Text = "Save script"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmdCheckScript
        '
        Me.cmdCheckScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdCheckScript.Image = Global.My.Resources.Resources.ok16
        Me.cmdCheckScript.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCheckScript.Name = "cmdCheckScript"
        Me.cmdCheckScript.Size = New System.Drawing.Size(23, 22)
        Me.cmdCheckScript.Text = "Verify script"
        Me.cmdCheckScript.ToolTipText = "Verify script"
        '
        'cmdExecute
        '
        Me.cmdExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdExecute.Image = Global.My.Resources.Resources.right16
        Me.cmdExecute.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExecute.Name = "cmdExecute"
        Me.cmdExecute.Size = New System.Drawing.Size(23, 22)
        Me.cmdExecute.Text = "Execute script"
        Me.cmdExecute.ToolTipText = "Execute script"
        '
        'rtb
        '
        Me.rtb.AcceptsTab = True
        Me.rtb.AutoWordSelection = True
        Me.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb.DetectUrls = False
        Me.rtb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtb.Location = New System.Drawing.Point(0, 25)
        Me.rtb.Name = "rtb"
        Me.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical
        Me.rtb.ShowSelectionMargin = True
        Me.rtb.Size = New System.Drawing.Size(644, 333)
        Me.rtb.TabIndex = 65
        Me.rtb.Text = ""
        '
        'SaveFileDialog
        '
        Me.SaveFileDialog.DefaultExt = "txt"
        Me.SaveFileDialog.Filter = "Text file (*.txt)|*.txt|All (*.*)|*.*"
        Me.SaveFileDialog.SupportMultiDottedExtensions = True
        Me.SaveFileDialog.Title = "Save script"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.DefaultExt = "txt"
        Me.OpenFileDialog.Filter = "Text file (*.txt)|*.txt|All (*.*)|*.*"
        Me.OpenFileDialog.ShowReadOnly = True
        Me.OpenFileDialog.SupportMultiDottedExtensions = True
        Me.OpenFileDialog.Title = "Open script"
        '
        'frmScripting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 378)
        Me.Controls.Add(Me.rtb)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.StatusBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mnuSystem
        Me.MinimumSize = New System.Drawing.Size(660, 359)
        Me.Name = "frmScripting"
        Me.Text = "Script editor"
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelLines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Private WithEvents mnuSystem As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItemFile As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSave As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSaveAs As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem56 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemScripts As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemVerify As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemExecute As System.Windows.Forms.MenuItem
    Friend WithEvents StatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents sbPanelLines As System.Windows.Forms.StatusBarPanel
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdCheckScript As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdExecute As System.Windows.Forms.ToolStripButton
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
End Class
