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
        Me.tvDepends = New System.Windows.Forms.TreeView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FichierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuAlwaysVisible = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.statusFile = New System.Windows.Forms.ToolStripStatusLabel
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
        Me.CDO = New System.Windows.Forms.OpenFileDialog
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
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabImports.SuspendLayout()
        Me.tabExports.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvDepends
        '
        Me.tvDepends.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tvDepends.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tvDepends.FullRowSelect = True
        Me.tvDepends.ImageIndex = 0
        Me.tvDepends.ImageList = Me.ImageList1
        Me.tvDepends.Location = New System.Drawing.Point(0, 27)
        Me.tvDepends.Name = "tvDepends"
        Me.tvDepends.SelectedImageIndex = 0
        Me.tvDepends.Size = New System.Drawing.Size(230, 417)
        Me.tvDepends.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PropertiesToolStripMenuItem, Me.OpenFirectoryToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(154, 48)
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Image = Global.DependenciesViewer.My.Resources.Resources.document_text
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.PropertiesToolStripMenuItem.Text = "File properties"
        '
        'OpenFirectoryToolStripMenuItem
        '
        Me.OpenFirectoryToolStripMenuItem.Image = Global.DependenciesViewer.My.Resources.Resources.folder_open
        Me.OpenFirectoryToolStripMenuItem.Name = "OpenFirectoryToolStripMenuItem"
        Me.OpenFirectoryToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.OpenFirectoryToolStripMenuItem.Text = "Open directory"
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
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.FichierToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(711, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.OpenToolStripMenuItem.Text = "&Open..."
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
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabImports)
        Me.TabControl1.Controls.Add(Me.tabExports)
        Me.TabControl1.Location = New System.Drawing.Point(236, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(463, 279)
        Me.TabControl1.TabIndex = 3
        '
        'tabImports
        '
        Me.tabImports.Controls.Add(Me.lvImports)
        Me.tabImports.Location = New System.Drawing.Point(4, 22)
        Me.tabImports.Name = "tabImports"
        Me.tabImports.Padding = New System.Windows.Forms.Padding(3)
        Me.tabImports.Size = New System.Drawing.Size(455, 253)
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
        Me.lvImports.Size = New System.Drawing.Size(449, 247)
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
        Me.tabExports.Size = New System.Drawing.Size(455, 253)
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
        Me.lvExports.Size = New System.Drawing.Size(449, 247)
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
        'CDO
        '
        Me.CDO.Filter = "Executables|*.exe;*.dll|All|*.*"
        '
        'lvAllDeps
        '
        Me.lvAllDeps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvAllDeps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader15, Me.ColumnHeader16, Me.ColumnHeader17, Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader20, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14})
        Me.lvAllDeps.ContextMenuStrip = Me.ContextMenuStrip2
        Me.lvAllDeps.FullRowSelect = True
        Me.lvAllDeps.LargeImageList = Me.ImageList1
        Me.lvAllDeps.Location = New System.Drawing.Point(236, 308)
        Me.lvAllDeps.Name = "lvAllDeps"
        Me.lvAllDeps.OverriddenDoubleBuffered = True
        Me.lvAllDeps.Size = New System.Drawing.Size(463, 136)
        Me.lvAllDeps.SmallImageList = Me.ImageList1
        Me.lvAllDeps.TabIndex = 4
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
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(154, 48)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.DependenciesViewer.My.Resources.Resources.document_text
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem1.Text = "File properties"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.DependenciesViewer.My.Resources.Resources.folder_open
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem2.Text = "Open directory"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 476)
        Me.Controls.Add(Me.lvAllDeps)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.tvDepends)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Native Dependency Viewer"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabImports.ResumeLayout(False)
        Me.tabExports.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvDepends As System.Windows.Forms.TreeView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FichierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAlwaysVisible As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents statusFile As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabImports As System.Windows.Forms.TabPage
    Friend WithEvents tabExports As System.Windows.Forms.TabPage
    Friend WithEvents lvImports As DoubleBufferedLV
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvExports As DoubleBufferedLV
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Private WithEvents CDO As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lvAllDeps As DoubleBufferedLV
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
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFirectoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
End Class