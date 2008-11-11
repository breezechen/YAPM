<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Past jobs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Future jobs", System.Windows.Forms.HorizontalAlignment.Left)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.panelActions1 = New System.Windows.Forms.Panel
        Me.cmdSetPriority = New System.Windows.Forms.Button
        Me.gpProc2 = New System.Windows.Forms.GroupBox
        Me.cmdOpenDir = New System.Windows.Forms.Button
        Me.cmdFileProperties = New System.Windows.Forms.Button
        Me.gpProc1 = New System.Windows.Forms.GroupBox
        Me.cmdAffinity = New System.Windows.Forms.Button
        Me.cmdResume = New System.Windows.Forms.Button
        Me.cmdPause = New System.Windows.Forms.Button
        Me.cmdKill = New System.Windows.Forms.Button
        Me.cbPriority = New System.Windows.Forms.ComboBox
        Me.lblPriority = New System.Windows.Forms.Label
        Me.panelMain = New System.Windows.Forms.Panel
        Me.lvProcess = New System.Windows.Forms.ListView
        Me.c1 = New System.Windows.Forms.ColumnHeader
        Me.c2 = New System.Windows.Forms.ColumnHeader
        Me.c3 = New System.Windows.Forms.ColumnHeader
        Me.c4 = New System.Windows.Forms.ColumnHeader
        Me.c5 = New System.Windows.Forms.ColumnHeader
        Me.c6 = New System.Windows.Forms.ColumnHeader
        Me.c7 = New System.Windows.Forms.ColumnHeader
        Me.c8 = New System.Windows.Forms.ColumnHeader
        Me.c9 = New System.Windows.Forms.ColumnHeader
        Me.menuProc = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.KillToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ResumeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PriotiyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IdleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BelowNormalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NormalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboveNormalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HighToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RealTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SetAffinityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
        Me.panelMenu = New System.Windows.Forms.Panel
        Me.lblResCount = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.chkModules = New System.Windows.Forms.CheckBox
        Me.timerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.panelMain5 = New System.Windows.Forms.Panel
        Me.panelMain2 = New System.Windows.Forms.Panel
        Me.lvServices = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.panelMain4 = New System.Windows.Forms.Panel
        Me.panelMain3 = New System.Windows.Forms.Panel
        Me.lvJobs = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.panelActions2 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.panelActions3 = New System.Windows.Forms.Panel
        Me.label78 = New System.Windows.Forms.Label
        Me.panelActions4 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.panelActions5 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.timerServices = New System.Windows.Forms.Timer(Me.components)
        Me.lblProcessName = New System.Windows.Forms.Label
        Me.pctBigIcon = New System.Windows.Forms.PictureBox
        Me.menuCopyPctbig = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.pctSmallIcon = New System.Windows.Forms.PictureBox
        Me.menuCopyPctSmall = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.panelInfos = New System.Windows.Forms.Panel
        Me.lblProcessPath = New System.Windows.Forms.TextBox
        Me.cmdInfosToClipB = New System.Windows.Forms.Button
        Me.cmdTray = New System.Windows.Forms.Button
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.menuTooltip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.mainMenu = New System.Windows.Forms.MenuStrip
        Me.FdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExecuteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TakeFullPowerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.saveDial = New System.Windows.Forms.SaveFileDialog
        Me.pctHelp = New System.Windows.Forms.PictureBox
        Me.pctService = New System.Windows.Forms.PictureBox
        Me.pctProcess = New System.Windows.Forms.PictureBox
        Me.pctJobs = New System.Windows.Forms.PictureBox
        Me.lblProcess = New System.Windows.Forms.Label
        Me.lblServices = New System.Windows.Forms.Label
        Me.lblAddJobs = New System.Windows.Forms.Label
        Me.lblHelp = New System.Windows.Forms.Label
        Me.panelInfos2 = New System.Windows.Forms.Panel
        Me.lblServicePath = New System.Windows.Forms.TextBox
        Me.tv2 = New System.Windows.Forms.TreeView
        Me.tv = New System.Windows.Forms.TreeView
        Me.rtb2 = New System.Windows.Forms.RichTextBox
        Me.lblServiceName = New System.Windows.Forms.Label
        Me.panelActions1.SuspendLayout()
        Me.gpProc2.SuspendLayout()
        Me.gpProc1.SuspendLayout()
        Me.panelMain.SuspendLayout()
        Me.menuProc.SuspendLayout()
        Me.panelMenu.SuspendLayout()
        Me.panelMain2.SuspendLayout()
        Me.panelMain3.SuspendLayout()
        Me.panelActions2.SuspendLayout()
        Me.panelActions3.SuspendLayout()
        Me.panelActions4.SuspendLayout()
        Me.panelActions5.SuspendLayout()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuCopyPctbig.SuspendLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuCopyPctSmall.SuspendLayout()
        Me.panelInfos.SuspendLayout()
        Me.menuTooltip.SuspendLayout()
        Me.mainMenu.SuspendLayout()
        CType(Me.pctHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelInfos2.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgMain
        '
        Me.imgMain.ImageStream = CType(resources.GetObject("imgMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMain.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMain.Images.SetKeyName(0, "main_big2.ico")
        Me.imgMain.Images.SetKeyName(1, "noicon32")
        '
        'panelActions1
        '
        Me.panelActions1.BackColor = System.Drawing.Color.White
        Me.panelActions1.Controls.Add(Me.cmdSetPriority)
        Me.panelActions1.Controls.Add(Me.gpProc2)
        Me.panelActions1.Controls.Add(Me.gpProc1)
        Me.panelActions1.Controls.Add(Me.cbPriority)
        Me.panelActions1.Controls.Add(Me.lblPriority)
        Me.panelActions1.Location = New System.Drawing.Point(12, 290)
        Me.panelActions1.Name = "panelActions1"
        Me.panelActions1.Size = New System.Drawing.Size(200, 200)
        Me.panelActions1.TabIndex = 1
        '
        'cmdSetPriority
        '
        Me.cmdSetPriority.Location = New System.Drawing.Point(156, 8)
        Me.cmdSetPriority.Name = "cmdSetPriority"
        Me.cmdSetPriority.Size = New System.Drawing.Size(38, 21)
        Me.cmdSetPriority.TabIndex = 9
        Me.cmdSetPriority.Text = "Set"
        Me.cmdSetPriority.UseVisualStyleBackColor = True
        '
        'gpProc2
        '
        Me.gpProc2.Controls.Add(Me.cmdOpenDir)
        Me.gpProc2.Controls.Add(Me.cmdFileProperties)
        Me.gpProc2.Location = New System.Drawing.Point(9, 112)
        Me.gpProc2.Name = "gpProc2"
        Me.gpProc2.Size = New System.Drawing.Size(185, 71)
        Me.gpProc2.TabIndex = 6
        Me.gpProc2.TabStop = False
        Me.gpProc2.Text = "File"
        '
        'cmdOpenDir
        '
        Me.cmdOpenDir.Location = New System.Drawing.Point(6, 43)
        Me.cmdOpenDir.Name = "cmdOpenDir"
        Me.cmdOpenDir.Size = New System.Drawing.Size(172, 21)
        Me.cmdOpenDir.TabIndex = 7
        Me.cmdOpenDir.Text = "Open directory"
        Me.cmdOpenDir.UseVisualStyleBackColor = True
        '
        'cmdFileProperties
        '
        Me.cmdFileProperties.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFileProperties.Location = New System.Drawing.Point(6, 16)
        Me.cmdFileProperties.Name = "cmdFileProperties"
        Me.cmdFileProperties.Size = New System.Drawing.Size(83, 21)
        Me.cmdFileProperties.TabIndex = 5
        Me.cmdFileProperties.Text = "Properties"
        Me.cmdFileProperties.UseVisualStyleBackColor = True
        '
        'gpProc1
        '
        Me.gpProc1.Controls.Add(Me.cmdAffinity)
        Me.gpProc1.Controls.Add(Me.cmdResume)
        Me.gpProc1.Controls.Add(Me.cmdPause)
        Me.gpProc1.Controls.Add(Me.cmdKill)
        Me.gpProc1.Location = New System.Drawing.Point(9, 35)
        Me.gpProc1.Name = "gpProc1"
        Me.gpProc1.Size = New System.Drawing.Size(185, 71)
        Me.gpProc1.TabIndex = 5
        Me.gpProc1.TabStop = False
        Me.gpProc1.Text = "Process"
        '
        'cmdAffinity
        '
        Me.cmdAffinity.Location = New System.Drawing.Point(96, 43)
        Me.cmdAffinity.Name = "cmdAffinity"
        Me.cmdAffinity.Size = New System.Drawing.Size(83, 21)
        Me.cmdAffinity.TabIndex = 8
        Me.cmdAffinity.Text = "Affinity"
        Me.cmdAffinity.UseVisualStyleBackColor = True
        '
        'cmdResume
        '
        Me.cmdResume.Location = New System.Drawing.Point(6, 43)
        Me.cmdResume.Name = "cmdResume"
        Me.cmdResume.Size = New System.Drawing.Size(83, 21)
        Me.cmdResume.TabIndex = 7
        Me.cmdResume.Text = "Resume"
        Me.cmdResume.UseVisualStyleBackColor = True
        '
        'cmdPause
        '
        Me.cmdPause.Location = New System.Drawing.Point(95, 16)
        Me.cmdPause.Name = "cmdPause"
        Me.cmdPause.Size = New System.Drawing.Size(83, 21)
        Me.cmdPause.TabIndex = 6
        Me.cmdPause.Text = "Stop"
        Me.cmdPause.UseVisualStyleBackColor = True
        '
        'cmdKill
        '
        Me.cmdKill.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdKill.Location = New System.Drawing.Point(6, 16)
        Me.cmdKill.Name = "cmdKill"
        Me.cmdKill.Size = New System.Drawing.Size(83, 21)
        Me.cmdKill.TabIndex = 5
        Me.cmdKill.Text = "Kill"
        Me.cmdKill.UseVisualStyleBackColor = True
        '
        'cbPriority
        '
        Me.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPriority.FormattingEnabled = True
        Me.cbPriority.Items.AddRange(New Object() {"Idle", "Below Normal", "Normal", "Above Normal", "High", "Real Time"})
        Me.cbPriority.Location = New System.Drawing.Point(56, 8)
        Me.cbPriority.Name = "cbPriority"
        Me.cbPriority.Size = New System.Drawing.Size(94, 21)
        Me.cbPriority.TabIndex = 1
        '
        'lblPriority
        '
        Me.lblPriority.AutoSize = True
        Me.lblPriority.Location = New System.Drawing.Point(9, 11)
        Me.lblPriority.Name = "lblPriority"
        Me.lblPriority.Size = New System.Drawing.Size(41, 13)
        Me.lblPriority.TabIndex = 0
        Me.lblPriority.Text = "Priority"
        '
        'panelMain
        '
        Me.panelMain.Controls.Add(Me.lvProcess)
        Me.panelMain.Location = New System.Drawing.Point(218, 64)
        Me.panelMain.Name = "panelMain"
        Me.panelMain.Size = New System.Drawing.Size(560, 240)
        Me.panelMain.TabIndex = 2
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c6, Me.c7, Me.c8, Me.c9})
        Me.lvProcess.ContextMenuStrip = Me.menuProc
        Me.lvProcess.FullRowSelect = True
        ListViewGroup1.Header = "Processes"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.Location = New System.Drawing.Point(3, 3)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.Size = New System.Drawing.Size(554, 234)
        Me.lvProcess.SmallImageList = Me.imgProcess
        Me.lvProcess.TabIndex = 1
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
        Me.c2.Width = 40
        '
        'c3
        '
        Me.c3.Text = "User"
        '
        'c4
        '
        Me.c4.Text = "Processor time"
        Me.c4.Width = 80
        '
        'c5
        '
        Me.c5.Text = "Memory"
        Me.c5.Width = 80
        '
        'c6
        '
        Me.c6.Text = "Threads"
        '
        'c7
        '
        Me.c7.Text = "Priority"
        Me.c7.Width = 80
        '
        'c8
        '
        Me.c8.Text = "Path"
        Me.c8.Width = 350
        '
        'c9
        '
        Me.c9.Text = "Start time"
        Me.c9.Width = 250
        '
        'menuProc
        '
        Me.menuProc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KillToolStripMenuItem, Me.StopToolStripMenuItem, Me.ResumeToolStripMenuItem, Me.PriotiyToolStripMenuItem, Me.SetAffinityToolStripMenuItem, Me.ToolStripMenuItem8, Me.PropertiesToolStripMenuItem, Me.OpenFirectoryToolStripMenuItem})
        Me.menuProc.Name = "menuProc"
        Me.menuProc.Size = New System.Drawing.Size(151, 164)
        '
        'KillToolStripMenuItem
        '
        Me.KillToolStripMenuItem.Name = "KillToolStripMenuItem"
        Me.KillToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.KillToolStripMenuItem.Text = "Kill"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'ResumeToolStripMenuItem
        '
        Me.ResumeToolStripMenuItem.Name = "ResumeToolStripMenuItem"
        Me.ResumeToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ResumeToolStripMenuItem.Text = "Resume"
        '
        'PriotiyToolStripMenuItem
        '
        Me.PriotiyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IdleToolStripMenuItem, Me.BelowNormalToolStripMenuItem, Me.NormalToolStripMenuItem, Me.AboveNormalToolStripMenuItem, Me.HighToolStripMenuItem, Me.RealTimeToolStripMenuItem})
        Me.PriotiyToolStripMenuItem.Name = "PriotiyToolStripMenuItem"
        Me.PriotiyToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.PriotiyToolStripMenuItem.Text = "Priotiy"
        '
        'IdleToolStripMenuItem
        '
        Me.IdleToolStripMenuItem.Name = "IdleToolStripMenuItem"
        Me.IdleToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.IdleToolStripMenuItem.Text = "Idle"
        '
        'BelowNormalToolStripMenuItem
        '
        Me.BelowNormalToolStripMenuItem.Name = "BelowNormalToolStripMenuItem"
        Me.BelowNormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.BelowNormalToolStripMenuItem.Text = "Below Normal"
        '
        'NormalToolStripMenuItem
        '
        Me.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem"
        Me.NormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.NormalToolStripMenuItem.Text = "Normal"
        '
        'AboveNormalToolStripMenuItem
        '
        Me.AboveNormalToolStripMenuItem.Name = "AboveNormalToolStripMenuItem"
        Me.AboveNormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.AboveNormalToolStripMenuItem.Text = "Above Normal"
        '
        'HighToolStripMenuItem
        '
        Me.HighToolStripMenuItem.Name = "HighToolStripMenuItem"
        Me.HighToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.HighToolStripMenuItem.Text = "High"
        '
        'RealTimeToolStripMenuItem
        '
        Me.RealTimeToolStripMenuItem.Name = "RealTimeToolStripMenuItem"
        Me.RealTimeToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.RealTimeToolStripMenuItem.Text = "Real Time"
        '
        'SetAffinityToolStripMenuItem
        '
        Me.SetAffinityToolStripMenuItem.Name = "SetAffinityToolStripMenuItem"
        Me.SetAffinityToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.SetAffinityToolStripMenuItem.Text = "Set affinity..."
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(147, 6)
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.PropertiesToolStripMenuItem.Text = "Properties"
        '
        'OpenFirectoryToolStripMenuItem
        '
        Me.OpenFirectoryToolStripMenuItem.Name = "OpenFirectoryToolStripMenuItem"
        Me.OpenFirectoryToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.OpenFirectoryToolStripMenuItem.Text = "Open firectory"
        '
        'imgProcess
        '
        Me.imgProcess.ImageStream = CType(resources.GetObject("imgProcess.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcess.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcess.Images.SetKeyName(0, "noIcon")
        '
        'panelMenu
        '
        Me.panelMenu.Controls.Add(Me.lblResCount)
        Me.panelMenu.Controls.Add(Me.txtSearch)
        Me.panelMenu.Controls.Add(Me.chkModules)
        Me.panelMenu.Location = New System.Drawing.Point(215, 27)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(560, 28)
        Me.panelMenu.TabIndex = 4
        '
        'lblResCount
        '
        Me.lblResCount.AutoSize = True
        Me.lblResCount.Location = New System.Drawing.Point(527, 6)
        Me.lblResCount.Name = "lblResCount"
        Me.lblResCount.Size = New System.Drawing.Size(13, 13)
        Me.lblResCount.TabIndex = 2
        Me.lblResCount.Text = "0"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(157, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(362, 21)
        Me.txtSearch.TabIndex = 1
        '
        'chkModules
        '
        Me.chkModules.AutoSize = True
        Me.chkModules.Location = New System.Drawing.Point(3, 8)
        Me.chkModules.Name = "chkModules"
        Me.chkModules.Size = New System.Drawing.Size(148, 17)
        Me.chkModules.TabIndex = 0
        Me.chkModules.Text = "Retrive modules/trhreads"
        Me.chkModules.UseVisualStyleBackColor = True
        '
        'timerProcess
        '
        Me.timerProcess.Enabled = True
        Me.timerProcess.Interval = 1000
        '
        'panelMain5
        '
        Me.panelMain5.Location = New System.Drawing.Point(114, 166)
        Me.panelMain5.Name = "panelMain5"
        Me.panelMain5.Size = New System.Drawing.Size(560, 240)
        Me.panelMain5.TabIndex = 13
        '
        'panelMain2
        '
        Me.panelMain2.Controls.Add(Me.lvServices)
        Me.panelMain2.Location = New System.Drawing.Point(122, 174)
        Me.panelMain2.Name = "panelMain2"
        Me.panelMain2.Size = New System.Drawing.Size(560, 240)
        Me.panelMain2.TabIndex = 15
        Me.panelMain2.Visible = False
        '
        'lvServices
        '
        Me.lvServices.AllowColumnReorder = True
        Me.lvServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11})
        Me.lvServices.FullRowSelect = True
        ListViewGroup3.Header = "Services"
        ListViewGroup3.Name = "gpOther"
        ListViewGroup4.Header = "Search result"
        ListViewGroup4.Name = "gpSearch"
        Me.lvServices.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lvServices.HideSelection = False
        Me.lvServices.Location = New System.Drawing.Point(3, 3)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.Size = New System.Drawing.Size(554, 234)
        Me.lvServices.SmallImageList = Me.imgServices
        Me.lvServices.TabIndex = 0
        Me.lvServices.UseCompatibleStateImageBehavior = False
        Me.lvServices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Name"
        Me.ColumnHeader3.Width = 121
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Path"
        Me.ColumnHeader7.Width = 243
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "State"
        Me.ColumnHeader8.Width = 79
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Startup"
        Me.ColumnHeader9.Width = 107
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Common name"
        Me.ColumnHeader10.Width = 250
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Actions"
        Me.ColumnHeader11.Width = 150
        '
        'imgServices
        '
        Me.imgServices.ImageStream = CType(resources.GetObject("imgServices.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgServices.TransparentColor = System.Drawing.Color.Transparent
        Me.imgServices.Images.SetKeyName(0, "service")
        Me.imgServices.Images.SetKeyName(1, "ok")
        Me.imgServices.Images.SetKeyName(2, "ko")
        '
        'panelMain4
        '
        Me.panelMain4.Location = New System.Drawing.Point(130, 182)
        Me.panelMain4.Name = "panelMain4"
        Me.panelMain4.Size = New System.Drawing.Size(560, 240)
        Me.panelMain4.TabIndex = 16
        Me.panelMain4.Visible = False
        '
        'panelMain3
        '
        Me.panelMain3.Controls.Add(Me.lvJobs)
        Me.panelMain3.Location = New System.Drawing.Point(138, 190)
        Me.panelMain3.Name = "panelMain3"
        Me.panelMain3.Size = New System.Drawing.Size(560, 240)
        Me.panelMain3.TabIndex = 17
        Me.panelMain3.Visible = False
        '
        'lvJobs
        '
        Me.lvJobs.AllowColumnReorder = True
        Me.lvJobs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvJobs.FullRowSelect = True
        ListViewGroup5.Header = "Past jobs"
        ListViewGroup5.Name = "gp1"
        ListViewGroup6.Header = "Future jobs"
        ListViewGroup6.Name = "gp2"
        Me.lvJobs.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup5, ListViewGroup6})
        Me.lvJobs.HideSelection = False
        Me.lvJobs.Location = New System.Drawing.Point(3, 3)
        Me.lvJobs.Name = "lvJobs"
        Me.lvJobs.Size = New System.Drawing.Size(554, 234)
        Me.lvJobs.SmallImageList = Me.imgProcess
        Me.lvJobs.TabIndex = 0
        Me.lvJobs.UseCompatibleStateImageBehavior = False
        Me.lvJobs.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 132
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Process ID"
        Me.ColumnHeader2.Width = 69
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Process Name"
        Me.ColumnHeader4.Width = 92
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Action"
        Me.ColumnHeader5.Width = 127
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Time"
        Me.ColumnHeader6.Width = 130
        '
        'panelActions2
        '
        Me.panelActions2.BackColor = System.Drawing.Color.White
        Me.panelActions2.Controls.Add(Me.Label1)
        Me.panelActions2.Location = New System.Drawing.Point(92, 507)
        Me.panelActions2.Name = "panelActions2"
        Me.panelActions2.Size = New System.Drawing.Size(200, 200)
        Me.panelActions2.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Priority"
        '
        'panelActions3
        '
        Me.panelActions3.BackColor = System.Drawing.Color.White
        Me.panelActions3.Controls.Add(Me.label78)
        Me.panelActions3.Location = New System.Drawing.Point(24, 518)
        Me.panelActions3.Name = "panelActions3"
        Me.panelActions3.Size = New System.Drawing.Size(200, 200)
        Me.panelActions3.TabIndex = 20
        '
        'label78
        '
        Me.label78.AutoSize = True
        Me.label78.Location = New System.Drawing.Point(9, 11)
        Me.label78.Name = "label78"
        Me.label78.Size = New System.Drawing.Size(41, 13)
        Me.label78.TabIndex = 0
        Me.label78.Text = "Priority"
        '
        'panelActions4
        '
        Me.panelActions4.BackColor = System.Drawing.Color.White
        Me.panelActions4.Controls.Add(Me.Label2)
        Me.panelActions4.Location = New System.Drawing.Point(51, 504)
        Me.panelActions4.Name = "panelActions4"
        Me.panelActions4.Size = New System.Drawing.Size(200, 200)
        Me.panelActions4.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Priority"
        '
        'panelActions5
        '
        Me.panelActions5.BackColor = System.Drawing.Color.White
        Me.panelActions5.Controls.Add(Me.Label3)
        Me.panelActions5.Location = New System.Drawing.Point(54, 496)
        Me.panelActions5.Name = "panelActions5"
        Me.panelActions5.Size = New System.Drawing.Size(200, 200)
        Me.panelActions5.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Priority"
        '
        'timerServices
        '
        Me.timerServices.Enabled = True
        Me.timerServices.Interval = 6000
        '
        'lblProcessName
        '
        Me.lblProcessName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcessName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblProcessName.Location = New System.Drawing.Point(3, 3)
        Me.lblProcessName.Name = "lblProcessName"
        Me.lblProcessName.Size = New System.Drawing.Size(468, 32)
        Me.lblProcessName.TabIndex = 4
        Me.lblProcessName.Text = "Process name :"
        '
        'pctBigIcon
        '
        Me.pctBigIcon.ContextMenuStrip = Me.menuCopyPctbig
        Me.pctBigIcon.Location = New System.Drawing.Point(525, 3)
        Me.pctBigIcon.Name = "pctBigIcon"
        Me.pctBigIcon.Size = New System.Drawing.Size(32, 32)
        Me.pctBigIcon.TabIndex = 1
        Me.pctBigIcon.TabStop = False
        '
        'menuCopyPctbig
        '
        Me.menuCopyPctbig.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem6})
        Me.menuCopyPctbig.Name = "menuCopyPctbig"
        Me.menuCopyPctbig.Size = New System.Drawing.Size(170, 26)
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItem6.Text = "Copy to clipboard"
        '
        'pctSmallIcon
        '
        Me.pctSmallIcon.ContextMenuStrip = Me.menuCopyPctSmall
        Me.pctSmallIcon.Location = New System.Drawing.Point(503, 19)
        Me.pctSmallIcon.Name = "pctSmallIcon"
        Me.pctSmallIcon.Size = New System.Drawing.Size(16, 16)
        Me.pctSmallIcon.TabIndex = 2
        Me.pctSmallIcon.TabStop = False
        '
        'menuCopyPctSmall
        '
        Me.menuCopyPctSmall.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem7})
        Me.menuCopyPctSmall.Name = "menuCopyPctbig"
        Me.menuCopyPctSmall.Size = New System.Drawing.Size(170, 26)
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItem7.Text = "Copy to clipboard"
        '
        'rtb
        '
        Me.rtb.AutoWordSelection = True
        Me.rtb.BackColor = System.Drawing.Color.White
        Me.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb.HideSelection = False
        Me.rtb.Location = New System.Drawing.Point(3, 41)
        Me.rtb.Name = "rtb"
        Me.rtb.ReadOnly = True
        Me.rtb.Size = New System.Drawing.Size(554, 196)
        Me.rtb.TabIndex = 6
        Me.rtb.Text = ""
        Me.rtb.WordWrap = False
        '
        'panelInfos
        '
        Me.panelInfos.Controls.Add(Me.lblProcessPath)
        Me.panelInfos.Controls.Add(Me.cmdInfosToClipB)
        Me.panelInfos.Controls.Add(Me.rtb)
        Me.panelInfos.Controls.Add(Me.pctSmallIcon)
        Me.panelInfos.Controls.Add(Me.pctBigIcon)
        Me.panelInfos.Controls.Add(Me.lblProcessName)
        Me.panelInfos.Location = New System.Drawing.Point(215, 304)
        Me.panelInfos.Name = "panelInfos"
        Me.panelInfos.Size = New System.Drawing.Size(560, 240)
        Me.panelInfos.TabIndex = 3
        Me.panelInfos.Visible = False
        '
        'lblProcessPath
        '
        Me.lblProcessPath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblProcessPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblProcessPath.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblProcessPath.Location = New System.Drawing.Point(7, 24)
        Me.lblProcessPath.Name = "lblProcessPath"
        Me.lblProcessPath.ReadOnly = True
        Me.lblProcessPath.Size = New System.Drawing.Size(380, 14)
        Me.lblProcessPath.TabIndex = 8
        '
        'cmdInfosToClipB
        '
        Me.cmdInfosToClipB.Enabled = False
        Me.cmdInfosToClipB.Location = New System.Drawing.Point(395, 14)
        Me.cmdInfosToClipB.Name = "cmdInfosToClipB"
        Me.cmdInfosToClipB.Size = New System.Drawing.Size(99, 21)
        Me.cmdInfosToClipB.TabIndex = 7
        Me.cmdInfosToClipB.Text = "Copy to clipboard"
        Me.cmdInfosToClipB.UseVisualStyleBackColor = True
        '
        'cmdTray
        '
        Me.cmdTray.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTray.Location = New System.Drawing.Point(17, 504)
        Me.cmdTray.Name = "cmdTray"
        Me.cmdTray.Size = New System.Drawing.Size(180, 29)
        Me.cmdTray.TabIndex = 26
        Me.cmdTray.Text = "Minimize to tray"
        Me.cmdTray.UseVisualStyleBackColor = True
        '
        'Tray
        '
        Me.Tray.ContextMenuStrip = Me.menuTooltip
        Me.Tray.Icon = CType(resources.GetObject("Tray.Icon"), System.Drawing.Icon)
        Me.Tray.Text = "Yet Another Process Monitor"
        Me.Tray.Visible = True
        '
        'menuTooltip
        '
        Me.menuTooltip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem3, Me.ToolStripSeparator1, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5})
        Me.menuTooltip.Name = "menuTooltip"
        Me.menuTooltip.Size = New System.Drawing.Size(144, 76)
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(143, 22)
        Me.ToolStripMenuItem3.Text = "Show YAPM"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(140, 6)
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(143, 22)
        Me.ToolStripMenuItem4.Text = "About YAPM"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(143, 22)
        Me.ToolStripMenuItem5.Text = "Quitter"
        '
        'mainMenu
        '
        Me.mainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FdToolStripMenuItem, Me.ToolStripMenuItem2})
        Me.mainMenu.Location = New System.Drawing.Point(0, 0)
        Me.mainMenu.Name = "mainMenu"
        Me.mainMenu.Size = New System.Drawing.Size(771, 24)
        Me.mainMenu.TabIndex = 27
        Me.mainMenu.Text = "mnu"
        '
        'FdToolStripMenuItem
        '
        Me.FdToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExecuteToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.TakeFullPowerToolStripMenuItem, Me.ToolStripMenuItem1, Me.QuitToolStripMenuItem})
        Me.FdToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FdToolStripMenuItem.Name = "FdToolStripMenuItem"
        Me.FdToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FdToolStripMenuItem.Text = "&File"
        '
        'ExecuteToolStripMenuItem
        '
        Me.ExecuteToolStripMenuItem.Name = "ExecuteToolStripMenuItem"
        Me.ExecuteToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ExecuteToolStripMenuItem.Text = "&Execute..."
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.OptionsToolStripMenuItem.Text = "&Save report..."
        '
        'TakeFullPowerToolStripMenuItem
        '
        Me.TakeFullPowerToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TakeFullPowerToolStripMenuItem.Name = "TakeFullPowerToolStripMenuItem"
        Me.TakeFullPowerToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.TakeFullPowerToolStripMenuItem.Text = "&Take full power"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(158, 6)
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.QuitToolStripMenuItem.Text = "&Quit"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(24, 20)
        Me.ToolStripMenuItem2.Text = "&?"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'saveDial
        '
        Me.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        Me.saveDial.SupportMultiDottedExtensions = True
        '
        'pctHelp
        '
        Me.pctHelp.BackColor = System.Drawing.SystemColors.Control
        Me.pctHelp.Image = Global.YAPM.My.Resources.Resources.help
        Me.pctHelp.Location = New System.Drawing.Point(8, 223)
        Me.pctHelp.Name = "pctHelp"
        Me.pctHelp.Size = New System.Drawing.Size(66, 66)
        Me.pctHelp.TabIndex = 31
        Me.pctHelp.TabStop = False
        '
        'pctService
        '
        Me.pctService.BackColor = System.Drawing.SystemColors.Control
        Me.pctService.Image = Global.YAPM.My.Resources.Resources.application
        Me.pctService.Location = New System.Drawing.Point(8, 91)
        Me.pctService.Name = "pctService"
        Me.pctService.Size = New System.Drawing.Size(66, 66)
        Me.pctService.TabIndex = 32
        Me.pctService.TabStop = False
        '
        'pctProcess
        '
        Me.pctProcess.BackColor = System.Drawing.Color.White
        Me.pctProcess.Location = New System.Drawing.Point(8, 25)
        Me.pctProcess.Name = "pctProcess"
        Me.pctProcess.Size = New System.Drawing.Size(66, 66)
        Me.pctProcess.TabIndex = 34
        Me.pctProcess.TabStop = False
        '
        'pctJobs
        '
        Me.pctJobs.BackColor = System.Drawing.SystemColors.Control
        Me.pctJobs.Image = Global.YAPM.My.Resources.Resources.business_user_add
        Me.pctJobs.Location = New System.Drawing.Point(8, 157)
        Me.pctJobs.Name = "pctJobs"
        Me.pctJobs.Size = New System.Drawing.Size(66, 66)
        Me.pctJobs.TabIndex = 35
        Me.pctJobs.TabStop = False
        '
        'lblProcess
        '
        Me.lblProcess.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblProcess.Location = New System.Drawing.Point(82, 28)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(124, 63)
        Me.lblProcess.TabIndex = 36
        Me.lblProcess.Text = "Display processes runing on system"
        Me.lblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblServices
        '
        Me.lblServices.Enabled = False
        Me.lblServices.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServices.Location = New System.Drawing.Point(82, 91)
        Me.lblServices.Name = "lblServices"
        Me.lblServices.Size = New System.Drawing.Size(124, 63)
        Me.lblServices.TabIndex = 37
        Me.lblServices.Text = "Display services runing on system"
        Me.lblServices.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAddJobs
        '
        Me.lblAddJobs.Enabled = False
        Me.lblAddJobs.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddJobs.Location = New System.Drawing.Point(82, 157)
        Me.lblAddJobs.Name = "lblAddJobs"
        Me.lblAddJobs.Size = New System.Drawing.Size(124, 63)
        Me.lblAddJobs.TabIndex = 39
        Me.lblAddJobs.Text = "Add scheduled jobs"
        Me.lblAddJobs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHelp
        '
        Me.lblHelp.Enabled = False
        Me.lblHelp.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp.Location = New System.Drawing.Point(82, 223)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(124, 63)
        Me.lblHelp.TabIndex = 40
        Me.lblHelp.Text = "Display help"
        Me.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'panelInfos2
        '
        Me.panelInfos2.Controls.Add(Me.lblServicePath)
        Me.panelInfos2.Controls.Add(Me.tv2)
        Me.panelInfos2.Controls.Add(Me.tv)
        Me.panelInfos2.Controls.Add(Me.rtb2)
        Me.panelInfos2.Controls.Add(Me.lblServiceName)
        Me.panelInfos2.Location = New System.Drawing.Point(105, 156)
        Me.panelInfos2.Name = "panelInfos2"
        Me.panelInfos2.Size = New System.Drawing.Size(560, 240)
        Me.panelInfos2.TabIndex = 41
        Me.panelInfos2.Visible = False
        '
        'lblServicePath
        '
        Me.lblServicePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblServicePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblServicePath.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblServicePath.Location = New System.Drawing.Point(7, 24)
        Me.lblServicePath.Name = "lblServicePath"
        Me.lblServicePath.ReadOnly = True
        Me.lblServicePath.Size = New System.Drawing.Size(520, 14)
        Me.lblServicePath.TabIndex = 9
        '
        'tv2
        '
        Me.tv2.ImageIndex = 0
        Me.tv2.ImageList = Me.imgServices
        Me.tv2.Location = New System.Drawing.Point(407, 41)
        Me.tv2.Name = "tv2"
        Me.tv2.SelectedImageIndex = 0
        Me.tv2.Size = New System.Drawing.Size(150, 95)
        Me.tv2.TabIndex = 8
        '
        'tv
        '
        Me.tv.ImageIndex = 0
        Me.tv.ImageList = Me.imgServices
        Me.tv.Location = New System.Drawing.Point(407, 142)
        Me.tv.Name = "tv"
        Me.tv.SelectedImageIndex = 0
        Me.tv.Size = New System.Drawing.Size(150, 95)
        Me.tv.TabIndex = 7
        '
        'rtb2
        '
        Me.rtb2.AutoWordSelection = True
        Me.rtb2.BackColor = System.Drawing.Color.White
        Me.rtb2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb2.HideSelection = False
        Me.rtb2.Location = New System.Drawing.Point(3, 41)
        Me.rtb2.Name = "rtb2"
        Me.rtb2.ReadOnly = True
        Me.rtb2.Size = New System.Drawing.Size(398, 196)
        Me.rtb2.TabIndex = 6
        Me.rtb2.Text = ""
        Me.rtb2.WordWrap = False
        '
        'lblServiceName
        '
        Me.lblServiceName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblServiceName.Location = New System.Drawing.Point(3, 3)
        Me.lblServiceName.Name = "lblServiceName"
        Me.lblServiceName.Size = New System.Drawing.Size(554, 32)
        Me.lblServiceName.TabIndex = 4
        Me.lblServiceName.Text = "Service name :"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(771, 553)
        Me.Controls.Add(Me.panelInfos2)
        Me.Controls.Add(Me.panelInfos)
        Me.Controls.Add(Me.lblServices)
        Me.Controls.Add(Me.lblProcess)
        Me.Controls.Add(Me.pctJobs)
        Me.Controls.Add(Me.pctProcess)
        Me.Controls.Add(Me.pctService)
        Me.Controls.Add(Me.pctHelp)
        Me.Controls.Add(Me.cmdTray)
        Me.Controls.Add(Me.panelActions1)
        Me.Controls.Add(Me.panelActions5)
        Me.Controls.Add(Me.panelActions4)
        Me.Controls.Add(Me.panelActions3)
        Me.Controls.Add(Me.panelActions2)
        Me.Controls.Add(Me.panelMain2)
        Me.Controls.Add(Me.panelMain3)
        Me.Controls.Add(Me.panelMain4)
        Me.Controls.Add(Me.panelMain5)
        Me.Controls.Add(Me.panelMain)
        Me.Controls.Add(Me.panelMenu)
        Me.Controls.Add(Me.mainMenu)
        Me.Controls.Add(Me.lblAddJobs)
        Me.Controls.Add(Me.lblHelp)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mainMenu
        Me.MinimumSize = New System.Drawing.Size(787, 589)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Yet Another Process Monitor"
        Me.panelActions1.ResumeLayout(False)
        Me.panelActions1.PerformLayout()
        Me.gpProc2.ResumeLayout(False)
        Me.gpProc1.ResumeLayout(False)
        Me.panelMain.ResumeLayout(False)
        Me.menuProc.ResumeLayout(False)
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        Me.panelMain2.ResumeLayout(False)
        Me.panelMain3.ResumeLayout(False)
        Me.panelActions2.ResumeLayout(False)
        Me.panelActions2.PerformLayout()
        Me.panelActions3.ResumeLayout(False)
        Me.panelActions3.PerformLayout()
        Me.panelActions4.ResumeLayout(False)
        Me.panelActions4.PerformLayout()
        Me.panelActions5.ResumeLayout(False)
        Me.panelActions5.PerformLayout()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuCopyPctbig.ResumeLayout(False)
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuCopyPctSmall.ResumeLayout(False)
        Me.panelInfos.ResumeLayout(False)
        Me.panelInfos.PerformLayout()
        Me.menuTooltip.ResumeLayout(False)
        Me.mainMenu.ResumeLayout(False)
        Me.mainMenu.PerformLayout()
        CType(Me.pctHelp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctProcess, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelInfos2.ResumeLayout(False)
        Me.panelInfos2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents panelActions1 As System.Windows.Forms.Panel
    Friend WithEvents panelMain As System.Windows.Forms.Panel
    Friend WithEvents panelMenu As System.Windows.Forms.Panel
    Friend WithEvents lvProcess As System.Windows.Forms.ListView
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerProcess As System.Windows.Forms.Timer
    Friend WithEvents imgProcess As System.Windows.Forms.ImageList
    Friend WithEvents chkModules As System.Windows.Forms.CheckBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents cbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents gpProc1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdResume As System.Windows.Forms.Button
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents cmdKill As System.Windows.Forms.Button
    Friend WithEvents gpProc2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdOpenDir As System.Windows.Forms.Button
    Friend WithEvents cmdFileProperties As System.Windows.Forms.Button
    Friend WithEvents cmdAffinity As System.Windows.Forms.Button
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents panelMain5 As System.Windows.Forms.Panel
    Friend WithEvents panelMain2 As System.Windows.Forms.Panel
    Friend WithEvents lvServices As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents panelMain4 As System.Windows.Forms.Panel
    Friend WithEvents panelMain3 As System.Windows.Forms.Panel
    Friend WithEvents lvJobs As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents panelActions2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panelActions3 As System.Windows.Forms.Panel
    Friend WithEvents label78 As System.Windows.Forms.Label
    Friend WithEvents panelActions4 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents panelActions5 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdSetPriority As System.Windows.Forms.Button
    Friend WithEvents timerServices As System.Windows.Forms.Timer
    Friend WithEvents c9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblProcessName As System.Windows.Forms.Label
    Friend WithEvents pctBigIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pctSmallIcon As System.Windows.Forms.PictureBox
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents panelInfos As System.Windows.Forms.Panel
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdTray As System.Windows.Forms.Button
    Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
    Friend WithEvents mainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FdToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExecuteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents QuitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents saveDial As System.Windows.Forms.SaveFileDialog
    Friend WithEvents pctHelp As System.Windows.Forms.PictureBox
    Friend WithEvents pctService As System.Windows.Forms.PictureBox
    Friend WithEvents pctProcess As System.Windows.Forms.PictureBox
    Friend WithEvents pctJobs As System.Windows.Forms.PictureBox
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents lblServices As System.Windows.Forms.Label
    Friend WithEvents lblAddJobs As System.Windows.Forms.Label
    Friend WithEvents lblHelp As System.Windows.Forms.Label
    Friend WithEvents TakeFullPowerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuTooltip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCopyPctbig As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCopyPctSmall As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdInfosToClipB As System.Windows.Forms.Button
    Friend WithEvents lblProcessPath As System.Windows.Forms.TextBox
    Friend WithEvents menuProc As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents KillToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResumeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PriotiyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IdleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BelowNormalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NormalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboveNormalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HighToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RealTimeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetAffinityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFirectoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblResCount As System.Windows.Forms.Label
    Friend WithEvents panelInfos2 As System.Windows.Forms.Panel
    Friend WithEvents lblServicePath As System.Windows.Forms.TextBox
    Friend WithEvents tv2 As System.Windows.Forms.TreeView
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents rtb2 As System.Windows.Forms.RichTextBox
    Friend WithEvents lblServiceName As System.Windows.Forms.Label

End Class
