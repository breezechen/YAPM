<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FichierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAlwaysVisible = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.statusFile = New System.Windows.Forms.ToolStripStatusLabel
        Me.CDO = New System.Windows.Forms.OpenFileDialog
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.tvDepends = New System.Windows.Forms.TreeView
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabImports = New System.Windows.Forms.TabPage
        Me.lvImports = New DependenciesViewer.DoubleBufferedLV
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.tabExports = New System.Windows.Forms.TabPage
        Me.lvExports = New DependenciesViewer.DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.lvAllDeps = New DependenciesViewer.DoubleBufferedLV
        Me.ColumnHeader15 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader16 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader18 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader19 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader20 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.cMenu1 = New System.Windows.Forms.ContextMenu
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.cMenu2 = New System.Windows.Forms.ContextMenu
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabImports.SuspendLayout()
        Me.tabExports.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.ImageList1.Images.SetKeyName(0, "unresolved")
        Me.ImageList1.Images.SetKeyName(1, "dll")
        Me.ImageList1.Images.SetKeyName(2, "function")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.FichierToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(711, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.ToolStripMenuItem3, Me.QuitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.OpenToolStripMenuItem.Text = "&Open..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(109, 6)
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.QuitToolStripMenuItem.Text = "&Quit"
        '
        'FichierToolStripMenuItem
        '
        Me.FichierToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAlwaysVisible})
        Me.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem"
        Me.FichierToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.FichierToolStripMenuItem.Text = "&Display"
        '
        'mnuAlwaysVisible
        '
        Me.mnuAlwaysVisible.Name = "mnuAlwaysVisible"
        Me.mnuAlwaysVisible.Size = New System.Drawing.Size(147, 22)
        Me.mnuAlwaysVisible.Text = "&Always visible"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusFile})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 454)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(711, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'statusFile
        '
        Me.statusFile.Name = "statusFile"
        Me.statusFile.Size = New System.Drawing.Size(696, 17)
        Me.statusFile.Spring = True
        Me.statusFile.Text = "-"
        Me.statusFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CDO
        '
        Me.CDO.Filter = "Executables|*.exe;*.dll|All|*.*"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.tvDepends)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(711, 430)
        Me.SplitContainer1.SplitterDistance = 237
        Me.SplitContainer1.TabIndex = 5
        '
        'tvDepends
        '
        Me.tvDepends.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvDepends.FullRowSelect = True
        Me.tvDepends.ImageIndex = 0
        Me.tvDepends.ImageList = Me.ImageList1
        Me.tvDepends.Location = New System.Drawing.Point(0, 0)
        Me.tvDepends.Name = "tvDepends"
        Me.tvDepends.SelectedImageIndex = 0
        Me.tvDepends.Size = New System.Drawing.Size(237, 430)
        Me.tvDepends.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.lvAllDeps)
        Me.SplitContainer2.Size = New System.Drawing.Size(470, 430)
        Me.SplitContainer2.SplitterDistance = 257
        Me.SplitContainer2.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabImports)
        Me.TabControl1.Controls.Add(Me.tabExports)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(470, 257)
        Me.TabControl1.TabIndex = 4
        '
        'tabImports
        '
        Me.tabImports.Controls.Add(Me.lvImports)
        Me.tabImports.Location = New System.Drawing.Point(4, 22)
        Me.tabImports.Name = "tabImports"
        Me.tabImports.Padding = New System.Windows.Forms.Padding(3)
        Me.tabImports.Size = New System.Drawing.Size(462, 231)
        Me.tabImports.TabIndex = 0
        Me.tabImports.Text = "Imports Table"
        Me.tabImports.UseVisualStyleBackColor = True
        '
        'lvImports
        '
        Me.lvImports.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader9})
        Me.lvImports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvImports.FullRowSelect = True
        Me.lvImports.LargeImageList = Me.ImageList1
        Me.lvImports.Location = New System.Drawing.Point(3, 3)
        Me.lvImports.Name = "lvImports"
        Me.lvImports.OverriddenDoubleBuffered = True
        Me.lvImports.Size = New System.Drawing.Size(456, 225)
        Me.lvImports.SmallImageList = Me.ImageList1
        Me.lvImports.TabIndex = 0
        Me.lvImports.UseCompatibleStateImageBehavior = False
        Me.lvImports.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Ordinal"
        Me.ColumnHeader2.Width = 49
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Hint"
        Me.ColumnHeader3.Width = 39
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Dll"
        Me.ColumnHeader4.Width = 67
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Function"
        Me.ColumnHeader5.Width = 106
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Entry Point"
        Me.ColumnHeader9.Width = 101
        '
        'tabExports
        '
        Me.tabExports.Controls.Add(Me.lvExports)
        Me.tabExports.Location = New System.Drawing.Point(4, 22)
        Me.tabExports.Name = "tabExports"
        Me.tabExports.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExports.Size = New System.Drawing.Size(462, 231)
        Me.tabExports.TabIndex = 1
        Me.tabExports.Text = "Exports Table"
        Me.tabExports.UseVisualStyleBackColor = True
        '
        'lvExports
        '
        Me.lvExports.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8})
        Me.lvExports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvExports.FullRowSelect = True
        Me.lvExports.LargeImageList = Me.ImageList1
        Me.lvExports.Location = New System.Drawing.Point(3, 3)
        Me.lvExports.Name = "lvExports"
        Me.lvExports.OverriddenDoubleBuffered = True
        Me.lvExports.Size = New System.Drawing.Size(456, 225)
        Me.lvExports.SmallImageList = Me.ImageList1
        Me.lvExports.TabIndex = 0
        Me.lvExports.UseCompatibleStateImageBehavior = False
        Me.lvExports.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Ordinal"
        Me.ColumnHeader1.Width = 52
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Hint"
        Me.ColumnHeader6.Width = 51
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Function"
        Me.ColumnHeader7.Width = 145
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Entry Point"
        Me.ColumnHeader8.Width = 101
        '
        'lvAllDeps
        '
        Me.lvAllDeps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader15, Me.ColumnHeader16, Me.ColumnHeader17, Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader20, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14})
        Me.lvAllDeps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvAllDeps.FullRowSelect = True
        Me.lvAllDeps.LargeImageList = Me.ImageList1
        Me.lvAllDeps.Location = New System.Drawing.Point(0, 0)
        Me.lvAllDeps.Name = "lvAllDeps"
        Me.lvAllDeps.OverriddenDoubleBuffered = True
        Me.lvAllDeps.Size = New System.Drawing.Size(470, 169)
        Me.lvAllDeps.SmallImageList = Me.ImageList1
        Me.lvAllDeps.TabIndex = 5
        Me.lvAllDeps.UseCompatibleStateImageBehavior = False
        Me.lvAllDeps.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Module"
        Me.ColumnHeader15.Width = 72
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Timestamp"
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "Size"
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Machine"
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "Subsystem"
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "Product"
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Company"
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "File Version"
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Product Version"
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Linker Version"
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Path"
        Me.ColumnHeader14.Width = 200
        '
        'cMenu1
        '
        Me.cMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2})
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItem1
        '
        Me.VistaMenu.SetImage(Me.MenuItem1, Global.DependenciesViewer.My.Resources.Resources.document_text)
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "&File properties"
        '
        'MenuItem2
        '
        Me.VistaMenu.SetImage(Me.MenuItem2, Global.DependenciesViewer.My.Resources.Resources.folder_open)
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "&Open directory"
        '
        'cMenu2
        '
        Me.cMenu2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3, Me.MenuItem4})
        '
        'MenuItem3
        '
        Me.VistaMenu.SetImage(Me.MenuItem3, Global.DependenciesViewer.My.Resources.Resources.document_text)
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "&File properties"
        '
        'MenuItem4
        '
        Me.VistaMenu.SetImage(Me.MenuItem4, Global.DependenciesViewer.My.Resources.Resources.folder_open)
        Me.MenuItem4.Index = 1
        Me.MenuItem4.Text = "&Open directory"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 476)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Native Dependency Viewer"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tabImports.ResumeLayout(False)
        Me.tabExports.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FichierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAlwaysVisible As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents statusFile As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents CDO As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QuitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents tvDepends As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabImports As System.Windows.Forms.TabPage
    Friend WithEvents lvImports As DependenciesViewer.DoubleBufferedLV
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tabExports As System.Windows.Forms.TabPage
    Friend WithEvents lvExports As DependenciesViewer.DoubleBufferedLV
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvAllDeps As DependenciesViewer.DoubleBufferedLV
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Private WithEvents cMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Private WithEvents cMenu2 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
End Class
