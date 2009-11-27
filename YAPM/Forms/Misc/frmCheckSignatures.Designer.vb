<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckSignatures
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheckSignatures))
        Me.sb = New System.Windows.Forms.StatusStrip
        Me.lblOK = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUnknown = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblFailed = New System.Windows.Forms.ToolStripStatusLabel
        Me.pgb = New System.Windows.Forms.ToolStripProgressBar
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lvProcess = New processList
        Me.c1 = New System.Windows.Forms.ColumnHeader
        Me.c2 = New System.Windows.Forms.ColumnHeader
        Me.c8 = New System.Windows.Forms.ColumnHeader
        Me.lvResult = New DoubleBufferedLV
        Me.pathCol = New System.Windows.Forms.ColumnHeader
        Me.resCol = New System.Windows.Forms.ColumnHeader
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItemShow = New System.Windows.Forms.MenuItem
        Me.MenuItemClose = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.TheContextMenu = New System.Windows.Forms.ContextMenu
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItemCAll = New System.Windows.Forms.MenuItem
        Me.MenuItemCNone = New System.Windows.Forms.MenuItem
        Me.cmdCheck = New System.Windows.Forms.Button
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.sb.SuspendLayout()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sb
        '
        Me.sb.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblOK, Me.lblUnknown, Me.lblFailed, Me.pgb})
        Me.sb.Location = New System.Drawing.Point(0, 352)
        Me.sb.Name = "sb"
        Me.sb.Size = New System.Drawing.Size(591, 22)
        Me.sb.TabIndex = 5
        Me.sb.Text = "StatusStrip1"
        '
        'lblOK
        '
        Me.lblOK.ForeColor = System.Drawing.Color.Green
        Me.lblOK.Name = "lblOK"
        Me.lblOK.Size = New System.Drawing.Size(62, 17)
        Me.lblOK.Text = "Trusted : 0"
        '
        'lblUnknown
        '
        Me.lblUnknown.Name = "lblUnknown"
        Me.lblUnknown.Size = New System.Drawing.Size(73, 17)
        Me.lblUnknown.Text = "Unknown : 0"
        '
        'lblFailed
        '
        Me.lblFailed.ForeColor = System.Drawing.Color.Red
        Me.lblFailed.Name = "lblFailed"
        Me.lblFailed.Size = New System.Drawing.Size(82, 17)
        Me.lblFailed.Text = "Not trusted : 0"
        '
        'pgb
        '
        Me.pgb.Name = "pgb"
        Me.pgb.Size = New System.Drawing.Size(100, 16)
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer.Panel2Collapsed = True
        Me.SplitContainer.Size = New System.Drawing.Size(591, 352)
        Me.SplitContainer.SplitterDistance = 211
        Me.SplitContainer.TabIndex = 6
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lvProcess)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lvResult)
        Me.SplitContainer1.Size = New System.Drawing.Size(591, 352)
        Me.SplitContainer1.SplitterDistance = 334
        Me.SplitContainer1.TabIndex = 0
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.CatchErrors = False
        Me.lvProcess.CheckBoxes = True
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c8})
        CConnection1.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection1.Snapshot = Nothing
        CConnection1.SnapshotFile = Nothing
        Me.lvProcess.ConnectionObj = CConnection1
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.EnumMethod = asyncCallbackProcEnumerate.ProcessEnumMethode.VisibleProcesses
        Me.lvProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcess.FullRowSelect = True
        ListViewGroup1.Header = "Processes"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.IsConnected = False
        Me.lvProcess.Location = New System.Drawing.Point(0, 0)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.ReorganizeColumns = True
        Me.lvProcess.ShowObjectDetailsOnDoubleClick = True
        Me.lvProcess.Size = New System.Drawing.Size(334, 352)
        Me.lvProcess.TabIndex = 6
        Me.lvProcess.UseCompatibleStateImageBehavior = False
        Me.lvProcess.View = System.Windows.Forms.View.Details
        '
        'c1
        '
        Me.c1.Text = "Name"
        Me.c1.Width = 100
        '
        'c2
        '
        Me.c2.Text = "PID"
        Me.c2.Width = 50
        '
        'c8
        '
        Me.c8.Text = "Path"
        Me.c8.Width = 156
        '
        'lvResult
        '
        Me.lvResult.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.pathCol, Me.resCol})
        Me.lvResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvResult.FullRowSelect = True
        Me.lvResult.Location = New System.Drawing.Point(0, 0)
        Me.lvResult.Name = "lvResult"
        Me.lvResult.OverriddenDoubleBuffered = False
        Me.lvResult.Size = New System.Drawing.Size(253, 352)
        Me.lvResult.TabIndex = 0
        Me.lvResult.UseCompatibleStateImageBehavior = False
        Me.lvResult.View = System.Windows.Forms.View.Details
        '
        'pathCol
        '
        Me.pathCol.Text = "File"
        Me.pathCol.Width = 171
        '
        'resCol
        '
        Me.resCol.Text = "Result"
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItemShow
        '
        Me.VistaMenu.SetImage(Me.MenuItemShow, Global.My.Resources.Resources.document_text)
        Me.MenuItemShow.Index = 0
        Me.MenuItemShow.Text = "File properties"
        '
        'MenuItemClose
        '
        Me.VistaMenu.SetImage(Me.MenuItemClose, Global.My.Resources.Resources.folder_open)
        Me.MenuItemClose.Index = 1
        Me.MenuItemClose.Text = "Open directory"
        '
        'MenuItem1
        '
        Me.VistaMenu.SetImage(Me.MenuItem1, Global.My.Resources.Resources.magnifier)
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "File details"
        '
        'MenuItem2
        '
        Me.VistaMenu.SetImage(Me.MenuItem2, Global.My.Resources.Resources.globe)
        Me.MenuItem2.Index = 3
        Me.MenuItem2.Text = "Internet search"
        '
        'TheContextMenu
        '
        Me.TheContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemShow, Me.MenuItemClose, Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItemCAll, Me.MenuItemCNone})
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 4
        Me.MenuItem3.Text = "-"
        '
        'MenuItemCAll
        '
        Me.MenuItemCAll.Index = 5
        Me.MenuItemCAll.Text = "Check all"
        '
        'MenuItemCNone
        '
        Me.MenuItemCNone.Index = 6
        Me.MenuItemCNone.Text = "Check none"
        '
        'cmdCheck
        '
        Me.cmdCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCheck.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCheck.Location = New System.Drawing.Point(467, 353)
        Me.cmdCheck.Name = "cmdCheck"
        Me.cmdCheck.Size = New System.Drawing.Size(93, 21)
        Me.cmdCheck.TabIndex = 7
        Me.cmdCheck.Text = "Check now"
        Me.cmdCheck.UseVisualStyleBackColor = True
        '
        'Timer
        '
        Me.Timer.Interval = 1000
        '
        'frmCheckSignatures
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 374)
        Me.Controls.Add(Me.cmdCheck)
        Me.Controls.Add(Me.SplitContainer)
        Me.Controls.Add(Me.sb)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCheckSignatures"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Check signatures"
        Me.sb.ResumeLayout(False)
        Me.sb.PerformLayout()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sb As System.Windows.Forms.StatusStrip
    Friend WithEvents lblOK As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUnknown As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblFailed As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItemShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemClose As System.Windows.Forms.MenuItem
    Private WithEvents TheContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents pgb As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents cmdCheck As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lvProcess As processList
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvResult As DoubleBufferedLV
    Friend WithEvents pathCol As System.Windows.Forms.ColumnHeader
    Friend WithEvents resCol As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCNone As System.Windows.Forms.MenuItem
    Friend WithEvents Timer As System.Windows.Forms.Timer

End Class
