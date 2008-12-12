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
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Past jobs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Future jobs", System.Windows.Forms.HorizontalAlignment.Left)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
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
        Me.FileDetailsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.GetSecurityRiskOnlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GoogleSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
        Me.panelMenu = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.chkOnline = New System.Windows.Forms.CheckBox
        Me.lblResCount = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.chkModules = New System.Windows.Forms.CheckBox
        Me.timerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.panelMain2 = New System.Windows.Forms.Panel
        Me.lvServices = New System.Windows.Forms.ListView
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.menuService = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.ShutdownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem20 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem21 = New System.Windows.Forms.ToolStripMenuItem
        Me.FileDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.GoogleSearchToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.panelMain4 = New System.Windows.Forms.Panel
        Me.WBHelp = New System.Windows.Forms.WebBrowser
        Me.panelMain3 = New System.Windows.Forms.Panel
        Me.lvJobs = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.timerServices = New System.Windows.Forms.Timer(Me.components)
        Me.lblProcessName = New System.Windows.Forms.Label
        Me.menuCopyPctbig = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.menuCopyPctSmall = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.panelInfos = New System.Windows.Forms.Panel
        Me.lblProcessPath = New System.Windows.Forms.TextBox
        Me.cmdInfosToClipB = New System.Windows.Forms.Button
        Me.pctSmallIcon = New System.Windows.Forms.PictureBox
        Me.pctBigIcon = New System.Windows.Forms.PictureBox
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.menuTooltip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.saveDial = New System.Windows.Forms.SaveFileDialog
        Me.panelInfos2 = New System.Windows.Forms.Panel
        Me.cmdCopyServiceToCp = New System.Windows.Forms.Button
        Me.lblServicePath = New System.Windows.Forms.TextBox
        Me.tv2 = New System.Windows.Forms.TreeView
        Me.tv = New System.Windows.Forms.TreeView
        Me.rtb2 = New System.Windows.Forms.RichTextBox
        Me.lblServiceName = New System.Windows.Forms.Label
        Me.openDial = New System.Windows.Forms.OpenFileDialog
        Me.timerJobs = New System.Windows.Forms.Timer(Me.components)
        Me.Ribbon = New System.Windows.Forms.Ribbon
        Me.ProcessTab = New System.Windows.Forms.RibbonTab
        Me.RBProcessDisplay = New System.Windows.Forms.RibbonPanel
        Me.butProcessRerfresh = New System.Windows.Forms.RibbonButton
        Me.RBProcessActions = New System.Windows.Forms.RibbonPanel
        Me.butNewProcess = New System.Windows.Forms.RibbonButton
        Me.butKillProcess = New System.Windows.Forms.RibbonButton
        Me.butStopProcess = New System.Windows.Forms.RibbonButton
        Me.butResumeProcess = New System.Windows.Forms.RibbonButton
        Me.butProcessOtherActions = New System.Windows.Forms.RibbonButton
        Me.butProcessAffinity = New System.Windows.Forms.RibbonButton
        Me.butProcessLimitCPU = New System.Windows.Forms.RibbonButton
        Me.RBProcessPriority = New System.Windows.Forms.RibbonPanel
        Me.butProcessPriority = New System.Windows.Forms.RibbonButton
        Me.butIdle = New System.Windows.Forms.RibbonButton
        Me.butBelowNormal = New System.Windows.Forms.RibbonButton
        Me.butNormal = New System.Windows.Forms.RibbonButton
        Me.butAboveNormal = New System.Windows.Forms.RibbonButton
        Me.butHigh = New System.Windows.Forms.RibbonButton
        Me.butRealTime = New System.Windows.Forms.RibbonButton
        Me.RBProcessExecutable = New System.Windows.Forms.RibbonPanel
        Me.butProcessFileProp = New System.Windows.Forms.RibbonButton
        Me.butProcessDirOpen = New System.Windows.Forms.RibbonButton
        Me.butProcessFileDetails = New System.Windows.Forms.RibbonButton
        Me.RBProcessOnline = New System.Windows.Forms.RibbonPanel
        Me.butProcessOnlineDesc = New System.Windows.Forms.RibbonButton
        Me.butProcessGoogle = New System.Windows.Forms.RibbonButton
        Me.ServiceTab = New System.Windows.Forms.RibbonTab
        Me.RBServiceDisplay = New System.Windows.Forms.RibbonPanel
        Me.butServiceRefresh = New System.Windows.Forms.RibbonButton
        Me.RBServiceAction = New System.Windows.Forms.RibbonPanel
        Me.butStopService = New System.Windows.Forms.RibbonButton
        Me.butStartService = New System.Windows.Forms.RibbonButton
        Me.butPauseService = New System.Windows.Forms.RibbonButton
        Me.butResumeService = New System.Windows.Forms.RibbonButton
        Me.butShutdownService = New System.Windows.Forms.RibbonButton
        Me.RBServiceStartType = New System.Windows.Forms.RibbonPanel
        Me.butServiceStartType = New System.Windows.Forms.RibbonButton
        Me.butAutomaticStart = New System.Windows.Forms.RibbonButton
        Me.butOnDemandStart = New System.Windows.Forms.RibbonButton
        Me.butDisabledStart = New System.Windows.Forms.RibbonButton
        Me.RBServiceFile = New System.Windows.Forms.RibbonPanel
        Me.butServiceFileProp = New System.Windows.Forms.RibbonButton
        Me.butServiceOpenDir = New System.Windows.Forms.RibbonButton
        Me.butServiceFileDetails = New System.Windows.Forms.RibbonButton
        Me.RBServiceOnline = New System.Windows.Forms.RibbonPanel
        Me.butServiceGoogle = New System.Windows.Forms.RibbonButton
        Me.FileTab = New System.Windows.Forms.RibbonTab
        Me.RBFileDelete = New System.Windows.Forms.RibbonPanel
        Me.butMoveFileToTrash = New System.Windows.Forms.RibbonButton
        Me.butDeleteFile = New System.Windows.Forms.RibbonButton
        Me.butShreddFile = New System.Windows.Forms.RibbonButton
        Me.RBFileOnline = New System.Windows.Forms.RibbonPanel
        Me.butFileGoogleSearch = New System.Windows.Forms.RibbonButton
        Me.RBFileOther = New System.Windows.Forms.RibbonPanel
        Me.butFileProperties = New System.Windows.Forms.RibbonButton
        Me.butFileOpenDir = New System.Windows.Forms.RibbonButton
        Me.JobsTab = New System.Windows.Forms.RibbonTab
        Me.RBAdd = New System.Windows.Forms.RibbonPanel
        Me.butAddJob = New System.Windows.Forms.RibbonButton
        Me.RBJobsOpenSave = New System.Windows.Forms.RibbonPanel
        Me.butOpenJobList = New System.Windows.Forms.RibbonButton
        Me.butSaveJobList = New System.Windows.Forms.RibbonButton
        Me.SearchTab = New System.Windows.Forms.RibbonTab
        Me.ReportTab = New System.Windows.Forms.RibbonTab
        Me.RBSaveReport = New System.Windows.Forms.RibbonPanel
        Me.butSaveProcessReport = New System.Windows.Forms.RibbonButton
        Me.butSaveServiceReport = New System.Windows.Forms.RibbonButton
        Me.RBOthers = New System.Windows.Forms.RibbonPanel
        Me.butTakeFullPower = New System.Windows.Forms.RibbonButton
        Me.butOptions = New System.Windows.Forms.RibbonButton
        Me.RBDisplay = New System.Windows.Forms.RibbonPanel
        Me.butTopMost = New System.Windows.Forms.RibbonButton
        Me.HelpTab = New System.Windows.Forms.RibbonTab
        Me.RBHelpAction = New System.Windows.Forms.RibbonPanel
        Me.butDonate = New System.Windows.Forms.RibbonButton
        Me.butAbout = New System.Windows.Forms.RibbonButton
        Me.RBHelpWeb = New System.Windows.Forms.RibbonPanel
        Me.butWebite = New System.Windows.Forms.RibbonButton
        Me.butProjectPage = New System.Windows.Forms.RibbonButton
        Me.butDownload = New System.Windows.Forms.RibbonButton
        Me.cmdTray = New System.Windows.Forms.Button
        Me.RibbonButton1 = New System.Windows.Forms.RibbonButton
        Me.RibbonButtonList1 = New System.Windows.Forms.RibbonButtonList
        Me.panelMenu2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblResCount2 = New System.Windows.Forms.Label
        Me.txtServiceSearch = New System.Windows.Forms.TextBox
        Me.panelMain5 = New System.Windows.Forms.Panel
        Me.fileSplitContainer = New System.Windows.Forms.SplitContainer
        Me.rtb3 = New System.Windows.Forms.RichTextBox
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.cmdFileClipboard = New System.Windows.Forms.Button
        Me.pctFileSmall = New System.Windows.Forms.PictureBox
        Me.pctFileBig = New System.Windows.Forms.PictureBox
        Me.butFileShowFolderProperties = New System.Windows.Forms.RibbonButton
        Me.RBFileOthers = New System.Windows.Forms.RibbonPanel
        Me.butFileOthersActions = New System.Windows.Forms.RibbonButton
        Me.sepFile1 = New System.Windows.Forms.RibbonSeparator
        Me.butFileRename = New System.Windows.Forms.RibbonButton
        Me.butFileCopy = New System.Windows.Forms.RibbonButton
        Me.butFileMove = New System.Windows.Forms.RibbonButton
        Me.butFileOpen = New System.Windows.Forms.RibbonButton
        Me.sepFile2 = New System.Windows.Forms.RibbonSeparator
        Me.butFileSeeStrings = New System.Windows.Forms.RibbonButton
        Me.sepFile3 = New System.Windows.Forms.RibbonSeparator
        Me.butFileEncrypt = New System.Windows.Forms.RibbonButton
        Me.butFileDecrypt = New System.Windows.Forms.RibbonButton
        Me.panelMain.SuspendLayout()
        Me.menuProc.SuspendLayout()
        Me.panelMenu.SuspendLayout()
        Me.panelMain2.SuspendLayout()
        Me.menuService.SuspendLayout()
        Me.panelMain4.SuspendLayout()
        Me.panelMain3.SuspendLayout()
        Me.menuCopyPctbig.SuspendLayout()
        Me.menuCopyPctSmall.SuspendLayout()
        Me.panelInfos.SuspendLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuTooltip.SuspendLayout()
        Me.panelInfos2.SuspendLayout()
        Me.panelMenu2.SuspendLayout()
        Me.panelMain5.SuspendLayout()
        Me.fileSplitContainer.Panel1.SuspendLayout()
        Me.fileSplitContainer.SuspendLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgMain
        '
        Me.imgMain.ImageStream = CType(resources.GetObject("imgMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMain.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMain.Images.SetKeyName(0, "main_big2.ico")
        Me.imgMain.Images.SetKeyName(1, "noicon32")
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
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.FullRowSelect = True
        ListViewGroup7.Header = "Processes"
        ListViewGroup7.Name = "gpOther"
        ListViewGroup8.Header = "Search result"
        ListViewGroup8.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup7, ListViewGroup8})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.Location = New System.Drawing.Point(0, 0)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.Size = New System.Drawing.Size(560, 240)
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
        Me.menuProc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KillToolStripMenuItem, Me.StopToolStripMenuItem, Me.ResumeToolStripMenuItem, Me.PriotiyToolStripMenuItem, Me.SetAffinityToolStripMenuItem, Me.ToolStripMenuItem8, Me.PropertiesToolStripMenuItem, Me.OpenFirectoryToolStripMenuItem, Me.FileDetailsToolStripMenuItem1, Me.ToolStripMenuItem1, Me.GetSecurityRiskOnlineToolStripMenuItem, Me.GoogleSearchToolStripMenuItem})
        Me.menuProc.Name = "menuProc"
        Me.menuProc.Size = New System.Drawing.Size(194, 236)
        '
        'KillToolStripMenuItem
        '
        Me.KillToolStripMenuItem.Name = "KillToolStripMenuItem"
        Me.KillToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.KillToolStripMenuItem.Text = "Kill"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'ResumeToolStripMenuItem
        '
        Me.ResumeToolStripMenuItem.Name = "ResumeToolStripMenuItem"
        Me.ResumeToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ResumeToolStripMenuItem.Text = "Resume"
        '
        'PriotiyToolStripMenuItem
        '
        Me.PriotiyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IdleToolStripMenuItem, Me.BelowNormalToolStripMenuItem, Me.NormalToolStripMenuItem, Me.AboveNormalToolStripMenuItem, Me.HighToolStripMenuItem, Me.RealTimeToolStripMenuItem})
        Me.PriotiyToolStripMenuItem.Name = "PriotiyToolStripMenuItem"
        Me.PriotiyToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
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
        Me.SetAffinityToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SetAffinityToolStripMenuItem.Text = "Set affinity..."
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(190, 6)
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.PropertiesToolStripMenuItem.Text = "File properties"
        '
        'OpenFirectoryToolStripMenuItem
        '
        Me.OpenFirectoryToolStripMenuItem.Name = "OpenFirectoryToolStripMenuItem"
        Me.OpenFirectoryToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.OpenFirectoryToolStripMenuItem.Text = "Open directory"
        '
        'FileDetailsToolStripMenuItem1
        '
        Me.FileDetailsToolStripMenuItem1.Name = "FileDetailsToolStripMenuItem1"
        Me.FileDetailsToolStripMenuItem1.Size = New System.Drawing.Size(193, 22)
        Me.FileDetailsToolStripMenuItem1.Text = "File details"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(190, 6)
        '
        'GetSecurityRiskOnlineToolStripMenuItem
        '
        Me.GetSecurityRiskOnlineToolStripMenuItem.Name = "GetSecurityRiskOnlineToolStripMenuItem"
        Me.GetSecurityRiskOnlineToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.GetSecurityRiskOnlineToolStripMenuItem.Text = "Get security risk online"
        '
        'GoogleSearchToolStripMenuItem
        '
        Me.GoogleSearchToolStripMenuItem.Name = "GoogleSearchToolStripMenuItem"
        Me.GoogleSearchToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.GoogleSearchToolStripMenuItem.Text = "Google search"
        '
        'imgProcess
        '
        Me.imgProcess.ImageStream = CType(resources.GetObject("imgProcess.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcess.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcess.Images.SetKeyName(0, "noIcon")
        '
        'panelMenu
        '
        Me.panelMenu.Controls.Add(Me.Label3)
        Me.panelMenu.Controls.Add(Me.chkOnline)
        Me.panelMenu.Controls.Add(Me.lblResCount)
        Me.panelMenu.Controls.Add(Me.txtSearch)
        Me.panelMenu.Controls.Add(Me.chkModules)
        Me.panelMenu.Location = New System.Drawing.Point(12, 121)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(766, 28)
        Me.panelMenu.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(281, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Search process"
        '
        'chkOnline
        '
        Me.chkOnline.AutoSize = True
        Me.chkOnline.Location = New System.Drawing.Point(157, 6)
        Me.chkOnline.Name = "chkOnline"
        Me.chkOnline.Size = New System.Drawing.Size(118, 17)
        Me.chkOnline.TabIndex = 3
        Me.chkOnline.Text = "Retrive online infos"
        Me.chkOnline.UseVisualStyleBackColor = True
        '
        'lblResCount
        '
        Me.lblResCount.AutoSize = True
        Me.lblResCount.Location = New System.Drawing.Point(596, 6)
        Me.lblResCount.Name = "lblResCount"
        Me.lblResCount.Size = New System.Drawing.Size(56, 13)
        Me.lblResCount.TabIndex = 2
        Me.lblResCount.Text = "0 result(s)"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(367, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(223, 21)
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
        Me.lvServices.ContextMenuStrip = Me.menuService
        Me.lvServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvServices.FullRowSelect = True
        ListViewGroup1.Header = "Services"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvServices.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvServices.HideSelection = False
        Me.lvServices.Location = New System.Drawing.Point(0, 0)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.Size = New System.Drawing.Size(560, 240)
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
        Me.ColumnHeader7.Text = "Common name"
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
        Me.ColumnHeader9.Width = 70
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Path"
        Me.ColumnHeader10.Width = 250
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Actions"
        Me.ColumnHeader11.Width = 150
        '
        'menuService
        '
        Me.menuService.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem9, Me.ToolStripMenuItem10, Me.ShutdownToolStripMenuItem, Me.ToolStripMenuItem11, Me.ToolStripMenuItem12, Me.ToolStripSeparator2, Me.ToolStripMenuItem20, Me.ToolStripMenuItem21, Me.FileDetailsToolStripMenuItem, Me.ToolStripMenuItem2, Me.GoogleSearchToolStripMenuItem1})
        Me.menuService.Name = "menuProc"
        Me.menuService.Size = New System.Drawing.Size(154, 214)
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem9.Text = "Pause"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem10.Text = "Stop"
        '
        'ShutdownToolStripMenuItem
        '
        Me.ShutdownToolStripMenuItem.Name = "ShutdownToolStripMenuItem"
        Me.ShutdownToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ShutdownToolStripMenuItem.Text = "Shutdown"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem11.Text = "Start"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem13, Me.ToolStripMenuItem14, Me.ToolStripMenuItem15})
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem12.Text = "Type of start"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem13.Text = "Disabled"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem14.Text = "Auto start"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(137, 22)
        Me.ToolStripMenuItem15.Text = "On demand"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(150, 6)
        '
        'ToolStripMenuItem20
        '
        Me.ToolStripMenuItem20.Name = "ToolStripMenuItem20"
        Me.ToolStripMenuItem20.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem20.Text = "File properties"
        '
        'ToolStripMenuItem21
        '
        Me.ToolStripMenuItem21.Name = "ToolStripMenuItem21"
        Me.ToolStripMenuItem21.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripMenuItem21.Text = "Open directory"
        '
        'FileDetailsToolStripMenuItem
        '
        Me.FileDetailsToolStripMenuItem.Name = "FileDetailsToolStripMenuItem"
        Me.FileDetailsToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.FileDetailsToolStripMenuItem.Text = "File details"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(150, 6)
        '
        'GoogleSearchToolStripMenuItem1
        '
        Me.GoogleSearchToolStripMenuItem1.Name = "GoogleSearchToolStripMenuItem1"
        Me.GoogleSearchToolStripMenuItem1.Size = New System.Drawing.Size(153, 22)
        Me.GoogleSearchToolStripMenuItem1.Text = "Google search"
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
        Me.panelMain4.Controls.Add(Me.WBHelp)
        Me.panelMain4.Location = New System.Drawing.Point(130, 182)
        Me.panelMain4.Name = "panelMain4"
        Me.panelMain4.Size = New System.Drawing.Size(560, 240)
        Me.panelMain4.TabIndex = 16
        Me.panelMain4.Visible = False
        '
        'WBHelp
        '
        Me.WBHelp.AllowWebBrowserDrop = False
        Me.WBHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WBHelp.IsWebBrowserContextMenuEnabled = False
        Me.WBHelp.Location = New System.Drawing.Point(0, 0)
        Me.WBHelp.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WBHelp.Name = "WBHelp"
        Me.WBHelp.Size = New System.Drawing.Size(560, 240)
        Me.WBHelp.TabIndex = 0
        Me.WBHelp.Url = New System.Uri("", System.UriKind.Relative)
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
        Me.lvJobs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvJobs.FullRowSelect = True
        ListViewGroup3.Header = "Past jobs"
        ListViewGroup3.Name = "gp1"
        ListViewGroup4.Header = "Future jobs"
        ListViewGroup4.Name = "gp2"
        Me.lvJobs.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lvJobs.HideSelection = False
        Me.lvJobs.Location = New System.Drawing.Point(0, 0)
        Me.lvJobs.Name = "lvJobs"
        Me.lvJobs.Size = New System.Drawing.Size(560, 240)
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
        'timerServices
        '
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
        'pctSmallIcon
        '
        Me.pctSmallIcon.ContextMenuStrip = Me.menuCopyPctSmall
        Me.pctSmallIcon.Location = New System.Drawing.Point(503, 19)
        Me.pctSmallIcon.Name = "pctSmallIcon"
        Me.pctSmallIcon.Size = New System.Drawing.Size(16, 16)
        Me.pctSmallIcon.TabIndex = 2
        Me.pctSmallIcon.TabStop = False
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
        'saveDial
        '
        Me.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        Me.saveDial.SupportMultiDottedExtensions = True
        '
        'panelInfos2
        '
        Me.panelInfos2.Controls.Add(Me.cmdCopyServiceToCp)
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
        'cmdCopyServiceToCp
        '
        Me.cmdCopyServiceToCp.Enabled = False
        Me.cmdCopyServiceToCp.Location = New System.Drawing.Point(453, 17)
        Me.cmdCopyServiceToCp.Name = "cmdCopyServiceToCp"
        Me.cmdCopyServiceToCp.Size = New System.Drawing.Size(99, 21)
        Me.cmdCopyServiceToCp.TabIndex = 10
        Me.cmdCopyServiceToCp.Text = "Copy to clipboard"
        Me.cmdCopyServiceToCp.UseVisualStyleBackColor = True
        '
        'lblServicePath
        '
        Me.lblServicePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblServicePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblServicePath.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblServicePath.Location = New System.Drawing.Point(7, 24)
        Me.lblServicePath.Name = "lblServicePath"
        Me.lblServicePath.ReadOnly = True
        Me.lblServicePath.Size = New System.Drawing.Size(440, 14)
        Me.lblServicePath.TabIndex = 9
        '
        'tv2
        '
        Me.tv2.ImageIndex = 0
        Me.tv2.ImageList = Me.imgServices
        Me.tv2.Location = New System.Drawing.Point(407, 41)
        Me.tv2.Name = "tv2"
        Me.tv2.SelectedImageIndex = 2
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
        'openDial
        '
        Me.openDial.CheckFileExists = False
        Me.openDial.CheckPathExists = False
        Me.openDial.SupportMultiDottedExtensions = True
        '
        'timerJobs
        '
        Me.timerJobs.Enabled = True
        Me.timerJobs.Interval = 90
        '
        'Ribbon
        '
        Me.Ribbon.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon.Minimized = False
        Me.Ribbon.Name = "Ribbon"
        Me.Ribbon.Size = New System.Drawing.Size(795, 115)
        Me.Ribbon.TabIndex = 44
        Me.Ribbon.Tabs.Add(Me.ProcessTab)
        Me.Ribbon.Tabs.Add(Me.ServiceTab)
        Me.Ribbon.Tabs.Add(Me.FileTab)
        Me.Ribbon.Tabs.Add(Me.JobsTab)
        Me.Ribbon.Tabs.Add(Me.SearchTab)
        Me.Ribbon.Tabs.Add(Me.ReportTab)
        Me.Ribbon.Tabs.Add(Me.HelpTab)
        Me.Ribbon.TabSpacing = 6
        '
        'ProcessTab
        '
        Me.ProcessTab.Panels.Add(Me.RBProcessDisplay)
        Me.ProcessTab.Panels.Add(Me.RBProcessActions)
        Me.ProcessTab.Panels.Add(Me.RBProcessPriority)
        Me.ProcessTab.Panels.Add(Me.RBProcessExecutable)
        Me.ProcessTab.Panels.Add(Me.RBProcessOnline)
        Me.ProcessTab.Tag = Nothing
        Me.ProcessTab.Text = "Processes"
        '
        'RBProcessDisplay
        '
        Me.RBProcessDisplay.ButtonMoreEnabled = False
        Me.RBProcessDisplay.ButtonMoreVisible = False
        Me.RBProcessDisplay.Items.Add(Me.butProcessRerfresh)
        Me.RBProcessDisplay.Tag = Nothing
        Me.RBProcessDisplay.Text = "Display"
        '
        'butProcessRerfresh
        '
        Me.butProcessRerfresh.AltKey = Nothing
        Me.butProcessRerfresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessRerfresh.Image = CType(resources.GetObject("butProcessRerfresh.Image"), System.Drawing.Image)
        Me.butProcessRerfresh.SmallImage = CType(resources.GetObject("butProcessRerfresh.SmallImage"), System.Drawing.Image)
        Me.butProcessRerfresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessRerfresh.Tag = Nothing
        Me.butProcessRerfresh.Text = "Refresh"
        Me.butProcessRerfresh.ToolTip = Nothing
        Me.butProcessRerfresh.ToolTipImage = Nothing
        Me.butProcessRerfresh.ToolTipTitle = Nothing
        '
        'RBProcessActions
        '
        Me.RBProcessActions.ButtonMoreEnabled = False
        Me.RBProcessActions.ButtonMoreVisible = False
        Me.RBProcessActions.Items.Add(Me.butNewProcess)
        Me.RBProcessActions.Items.Add(Me.butKillProcess)
        Me.RBProcessActions.Items.Add(Me.butStopProcess)
        Me.RBProcessActions.Items.Add(Me.butResumeProcess)
        Me.RBProcessActions.Items.Add(Me.butProcessOtherActions)
        Me.RBProcessActions.Tag = Nothing
        Me.RBProcessActions.Text = "Process actions"
        '
        'butNewProcess
        '
        Me.butNewProcess.AltKey = Nothing
        Me.butNewProcess.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butNewProcess.Image = CType(resources.GetObject("butNewProcess.Image"), System.Drawing.Image)
        Me.butNewProcess.SmallImage = CType(resources.GetObject("butNewProcess.SmallImage"), System.Drawing.Image)
        Me.butNewProcess.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butNewProcess.Tag = Nothing
        Me.butNewProcess.Text = "New..."
        Me.butNewProcess.ToolTip = Nothing
        Me.butNewProcess.ToolTipImage = Nothing
        Me.butNewProcess.ToolTipTitle = Nothing
        '
        'butKillProcess
        '
        Me.butKillProcess.AltKey = Nothing
        Me.butKillProcess.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butKillProcess.Image = CType(resources.GetObject("butKillProcess.Image"), System.Drawing.Image)
        Me.butKillProcess.SmallImage = CType(resources.GetObject("butKillProcess.SmallImage"), System.Drawing.Image)
        Me.butKillProcess.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butKillProcess.Tag = Nothing
        Me.butKillProcess.Text = "Kill"
        Me.butKillProcess.ToolTip = Nothing
        Me.butKillProcess.ToolTipImage = Nothing
        Me.butKillProcess.ToolTipTitle = Nothing
        '
        'butStopProcess
        '
        Me.butStopProcess.AltKey = Nothing
        Me.butStopProcess.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butStopProcess.Image = CType(resources.GetObject("butStopProcess.Image"), System.Drawing.Image)
        Me.butStopProcess.SmallImage = CType(resources.GetObject("butStopProcess.SmallImage"), System.Drawing.Image)
        Me.butStopProcess.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butStopProcess.Tag = Nothing
        Me.butStopProcess.Text = "Pause"
        Me.butStopProcess.ToolTip = Nothing
        Me.butStopProcess.ToolTipImage = Nothing
        Me.butStopProcess.ToolTipTitle = Nothing
        '
        'butResumeProcess
        '
        Me.butResumeProcess.AltKey = Nothing
        Me.butResumeProcess.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butResumeProcess.Image = CType(resources.GetObject("butResumeProcess.Image"), System.Drawing.Image)
        Me.butResumeProcess.SmallImage = CType(resources.GetObject("butResumeProcess.SmallImage"), System.Drawing.Image)
        Me.butResumeProcess.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butResumeProcess.Tag = Nothing
        Me.butResumeProcess.Text = "Resume"
        Me.butResumeProcess.ToolTip = Nothing
        Me.butResumeProcess.ToolTipImage = Nothing
        Me.butResumeProcess.ToolTipTitle = Nothing
        '
        'butProcessOtherActions
        '
        Me.butProcessOtherActions.AltKey = Nothing
        Me.butProcessOtherActions.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessOtherActions.DropDownItems.Add(Me.butProcessAffinity)
        Me.butProcessOtherActions.DropDownItems.Add(Me.butProcessLimitCPU)
        Me.butProcessOtherActions.Image = CType(resources.GetObject("butProcessOtherActions.Image"), System.Drawing.Image)
        Me.butProcessOtherActions.SmallImage = CType(resources.GetObject("butProcessOtherActions.SmallImage"), System.Drawing.Image)
        Me.butProcessOtherActions.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butProcessOtherActions.Tag = Nothing
        Me.butProcessOtherActions.Text = "Other actions"
        Me.butProcessOtherActions.ToolTip = Nothing
        Me.butProcessOtherActions.ToolTipImage = Nothing
        Me.butProcessOtherActions.ToolTipTitle = Nothing
        '
        'butProcessAffinity
        '
        Me.butProcessAffinity.AltKey = Nothing
        Me.butProcessAffinity.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessAffinity.Image = CType(resources.GetObject("butProcessAffinity.Image"), System.Drawing.Image)
        Me.butProcessAffinity.SmallImage = CType(resources.GetObject("butProcessAffinity.SmallImage"), System.Drawing.Image)
        Me.butProcessAffinity.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessAffinity.Tag = Nothing
        Me.butProcessAffinity.Text = "Affinity"
        Me.butProcessAffinity.ToolTip = Nothing
        Me.butProcessAffinity.ToolTipImage = Nothing
        Me.butProcessAffinity.ToolTipTitle = Nothing
        '
        'butProcessLimitCPU
        '
        Me.butProcessLimitCPU.AltKey = Nothing
        Me.butProcessLimitCPU.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessLimitCPU.Image = CType(resources.GetObject("butProcessLimitCPU.Image"), System.Drawing.Image)
        Me.butProcessLimitCPU.SmallImage = CType(resources.GetObject("butProcessLimitCPU.SmallImage"), System.Drawing.Image)
        Me.butProcessLimitCPU.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessLimitCPU.Tag = Nothing
        Me.butProcessLimitCPU.Text = "Limit CPU usage"
        Me.butProcessLimitCPU.ToolTip = Nothing
        Me.butProcessLimitCPU.ToolTipImage = Nothing
        Me.butProcessLimitCPU.ToolTipTitle = Nothing
        '
        'RBProcessPriority
        '
        Me.RBProcessPriority.ButtonMoreEnabled = False
        Me.RBProcessPriority.ButtonMoreVisible = False
        Me.RBProcessPriority.Items.Add(Me.butProcessPriority)
        Me.RBProcessPriority.Tag = Nothing
        Me.RBProcessPriority.Text = "Priority"
        '
        'butProcessPriority
        '
        Me.butProcessPriority.AltKey = Nothing
        Me.butProcessPriority.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessPriority.DropDownItems.Add(Me.butIdle)
        Me.butProcessPriority.DropDownItems.Add(Me.butBelowNormal)
        Me.butProcessPriority.DropDownItems.Add(Me.butNormal)
        Me.butProcessPriority.DropDownItems.Add(Me.butAboveNormal)
        Me.butProcessPriority.DropDownItems.Add(Me.butHigh)
        Me.butProcessPriority.DropDownItems.Add(Me.butRealTime)
        Me.butProcessPriority.Image = CType(resources.GetObject("butProcessPriority.Image"), System.Drawing.Image)
        Me.butProcessPriority.SmallImage = CType(resources.GetObject("butProcessPriority.SmallImage"), System.Drawing.Image)
        Me.butProcessPriority.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butProcessPriority.Tag = Nothing
        Me.butProcessPriority.Text = "Priority"
        Me.butProcessPriority.ToolTip = Nothing
        Me.butProcessPriority.ToolTipImage = Nothing
        Me.butProcessPriority.ToolTipTitle = Nothing
        '
        'butIdle
        '
        Me.butIdle.AltKey = Nothing
        Me.butIdle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butIdle.Image = CType(resources.GetObject("butIdle.Image"), System.Drawing.Image)
        Me.butIdle.SmallImage = CType(resources.GetObject("butIdle.SmallImage"), System.Drawing.Image)
        Me.butIdle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butIdle.Tag = Nothing
        Me.butIdle.Text = "Idle"
        Me.butIdle.ToolTip = Nothing
        Me.butIdle.ToolTipImage = Nothing
        Me.butIdle.ToolTipTitle = Nothing
        '
        'butBelowNormal
        '
        Me.butBelowNormal.AltKey = Nothing
        Me.butBelowNormal.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butBelowNormal.Image = CType(resources.GetObject("butBelowNormal.Image"), System.Drawing.Image)
        Me.butBelowNormal.SmallImage = CType(resources.GetObject("butBelowNormal.SmallImage"), System.Drawing.Image)
        Me.butBelowNormal.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butBelowNormal.Tag = Nothing
        Me.butBelowNormal.Text = "Below Normal"
        Me.butBelowNormal.ToolTip = Nothing
        Me.butBelowNormal.ToolTipImage = Nothing
        Me.butBelowNormal.ToolTipTitle = Nothing
        '
        'butNormal
        '
        Me.butNormal.AltKey = Nothing
        Me.butNormal.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butNormal.Image = CType(resources.GetObject("butNormal.Image"), System.Drawing.Image)
        Me.butNormal.SmallImage = CType(resources.GetObject("butNormal.SmallImage"), System.Drawing.Image)
        Me.butNormal.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butNormal.Tag = Nothing
        Me.butNormal.Text = "Normal"
        Me.butNormal.ToolTip = Nothing
        Me.butNormal.ToolTipImage = Nothing
        Me.butNormal.ToolTipTitle = Nothing
        '
        'butAboveNormal
        '
        Me.butAboveNormal.AltKey = Nothing
        Me.butAboveNormal.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butAboveNormal.Image = CType(resources.GetObject("butAboveNormal.Image"), System.Drawing.Image)
        Me.butAboveNormal.SmallImage = CType(resources.GetObject("butAboveNormal.SmallImage"), System.Drawing.Image)
        Me.butAboveNormal.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butAboveNormal.Tag = Nothing
        Me.butAboveNormal.Text = "Above Normal"
        Me.butAboveNormal.ToolTip = Nothing
        Me.butAboveNormal.ToolTipImage = Nothing
        Me.butAboveNormal.ToolTipTitle = Nothing
        '
        'butHigh
        '
        Me.butHigh.AltKey = Nothing
        Me.butHigh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butHigh.Image = CType(resources.GetObject("butHigh.Image"), System.Drawing.Image)
        Me.butHigh.SmallImage = CType(resources.GetObject("butHigh.SmallImage"), System.Drawing.Image)
        Me.butHigh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butHigh.Tag = Nothing
        Me.butHigh.Text = "High"
        Me.butHigh.ToolTip = Nothing
        Me.butHigh.ToolTipImage = Nothing
        Me.butHigh.ToolTipTitle = Nothing
        '
        'butRealTime
        '
        Me.butRealTime.AltKey = Nothing
        Me.butRealTime.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butRealTime.Image = CType(resources.GetObject("butRealTime.Image"), System.Drawing.Image)
        Me.butRealTime.SmallImage = CType(resources.GetObject("butRealTime.SmallImage"), System.Drawing.Image)
        Me.butRealTime.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butRealTime.Tag = Nothing
        Me.butRealTime.Text = "Real Time"
        Me.butRealTime.ToolTip = Nothing
        Me.butRealTime.ToolTipImage = Nothing
        Me.butRealTime.ToolTipTitle = Nothing
        '
        'RBProcessExecutable
        '
        Me.RBProcessExecutable.ButtonMoreEnabled = False
        Me.RBProcessExecutable.ButtonMoreVisible = False
        Me.RBProcessExecutable.Items.Add(Me.butProcessFileProp)
        Me.RBProcessExecutable.Items.Add(Me.butProcessDirOpen)
        Me.RBProcessExecutable.Items.Add(Me.butProcessFileDetails)
        Me.RBProcessExecutable.Tag = Nothing
        Me.RBProcessExecutable.Text = "Executable"
        '
        'butProcessFileProp
        '
        Me.butProcessFileProp.AltKey = Nothing
        Me.butProcessFileProp.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessFileProp.Image = CType(resources.GetObject("butProcessFileProp.Image"), System.Drawing.Image)
        Me.butProcessFileProp.SmallImage = CType(resources.GetObject("butProcessFileProp.SmallImage"), System.Drawing.Image)
        Me.butProcessFileProp.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessFileProp.Tag = Nothing
        Me.butProcessFileProp.Text = "Show file properties"
        Me.butProcessFileProp.ToolTip = Nothing
        Me.butProcessFileProp.ToolTipImage = Nothing
        Me.butProcessFileProp.ToolTipTitle = Nothing
        '
        'butProcessDirOpen
        '
        Me.butProcessDirOpen.AltKey = Nothing
        Me.butProcessDirOpen.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessDirOpen.Image = CType(resources.GetObject("butProcessDirOpen.Image"), System.Drawing.Image)
        Me.butProcessDirOpen.SmallImage = CType(resources.GetObject("butProcessDirOpen.SmallImage"), System.Drawing.Image)
        Me.butProcessDirOpen.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessDirOpen.Tag = Nothing
        Me.butProcessDirOpen.Text = "Open file      directory"
        Me.butProcessDirOpen.ToolTip = Nothing
        Me.butProcessDirOpen.ToolTipImage = Nothing
        Me.butProcessDirOpen.ToolTipTitle = Nothing
        '
        'butProcessFileDetails
        '
        Me.butProcessFileDetails.AltKey = Nothing
        Me.butProcessFileDetails.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessFileDetails.Image = CType(resources.GetObject("butProcessFileDetails.Image"), System.Drawing.Image)
        Me.butProcessFileDetails.SmallImage = CType(resources.GetObject("butProcessFileDetails.SmallImage"), System.Drawing.Image)
        Me.butProcessFileDetails.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessFileDetails.Tag = Nothing
        Me.butProcessFileDetails.Text = "Details"
        Me.butProcessFileDetails.ToolTip = Nothing
        Me.butProcessFileDetails.ToolTipImage = Nothing
        Me.butProcessFileDetails.ToolTipTitle = Nothing
        '
        'RBProcessOnline
        '
        Me.RBProcessOnline.ButtonMoreEnabled = False
        Me.RBProcessOnline.ButtonMoreVisible = False
        Me.RBProcessOnline.Items.Add(Me.butProcessOnlineDesc)
        Me.RBProcessOnline.Items.Add(Me.butProcessGoogle)
        Me.RBProcessOnline.Tag = Nothing
        Me.RBProcessOnline.Text = "Online"
        '
        'butProcessOnlineDesc
        '
        Me.butProcessOnlineDesc.AltKey = Nothing
        Me.butProcessOnlineDesc.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessOnlineDesc.Image = CType(resources.GetObject("butProcessOnlineDesc.Image"), System.Drawing.Image)
        Me.butProcessOnlineDesc.SmallImage = CType(resources.GetObject("butProcessOnlineDesc.SmallImage"), System.Drawing.Image)
        Me.butProcessOnlineDesc.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessOnlineDesc.Tag = Nothing
        Me.butProcessOnlineDesc.Text = "Security risk"
        Me.butProcessOnlineDesc.ToolTip = Nothing
        Me.butProcessOnlineDesc.ToolTipImage = Nothing
        Me.butProcessOnlineDesc.ToolTipTitle = Nothing
        '
        'butProcessGoogle
        '
        Me.butProcessGoogle.AltKey = Nothing
        Me.butProcessGoogle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessGoogle.Image = CType(resources.GetObject("butProcessGoogle.Image"), System.Drawing.Image)
        Me.butProcessGoogle.SmallImage = CType(resources.GetObject("butProcessGoogle.SmallImage"), System.Drawing.Image)
        Me.butProcessGoogle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessGoogle.Tag = Nothing
        Me.butProcessGoogle.Text = "Google search"
        Me.butProcessGoogle.ToolTip = Nothing
        Me.butProcessGoogle.ToolTipImage = Nothing
        Me.butProcessGoogle.ToolTipTitle = Nothing
        '
        'ServiceTab
        '
        Me.ServiceTab.Panels.Add(Me.RBServiceDisplay)
        Me.ServiceTab.Panels.Add(Me.RBServiceAction)
        Me.ServiceTab.Panels.Add(Me.RBServiceStartType)
        Me.ServiceTab.Panels.Add(Me.RBServiceFile)
        Me.ServiceTab.Panels.Add(Me.RBServiceOnline)
        Me.ServiceTab.Tag = Nothing
        Me.ServiceTab.Text = "Services"
        '
        'RBServiceDisplay
        '
        Me.RBServiceDisplay.ButtonMoreEnabled = False
        Me.RBServiceDisplay.ButtonMoreVisible = False
        Me.RBServiceDisplay.Items.Add(Me.butServiceRefresh)
        Me.RBServiceDisplay.Tag = Nothing
        Me.RBServiceDisplay.Text = "Display"
        '
        'butServiceRefresh
        '
        Me.butServiceRefresh.AltKey = Nothing
        Me.butServiceRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceRefresh.Image = CType(resources.GetObject("butServiceRefresh.Image"), System.Drawing.Image)
        Me.butServiceRefresh.SmallImage = CType(resources.GetObject("butServiceRefresh.SmallImage"), System.Drawing.Image)
        Me.butServiceRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceRefresh.Tag = Nothing
        Me.butServiceRefresh.Text = "Refresh"
        Me.butServiceRefresh.ToolTip = Nothing
        Me.butServiceRefresh.ToolTipImage = Nothing
        Me.butServiceRefresh.ToolTipTitle = Nothing
        '
        'RBServiceAction
        '
        Me.RBServiceAction.ButtonMoreEnabled = False
        Me.RBServiceAction.ButtonMoreVisible = False
        Me.RBServiceAction.Items.Add(Me.butStopService)
        Me.RBServiceAction.Items.Add(Me.butStartService)
        Me.RBServiceAction.Items.Add(Me.butPauseService)
        Me.RBServiceAction.Items.Add(Me.butResumeService)
        Me.RBServiceAction.Items.Add(Me.butShutdownService)
        Me.RBServiceAction.Tag = Nothing
        Me.RBServiceAction.Text = "Service actions"
        '
        'butStopService
        '
        Me.butStopService.AltKey = Nothing
        Me.butStopService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butStopService.Image = CType(resources.GetObject("butStopService.Image"), System.Drawing.Image)
        Me.butStopService.SmallImage = CType(resources.GetObject("butStopService.SmallImage"), System.Drawing.Image)
        Me.butStopService.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butStopService.Tag = Nothing
        Me.butStopService.Text = "Stop"
        Me.butStopService.ToolTip = Nothing
        Me.butStopService.ToolTipImage = Nothing
        Me.butStopService.ToolTipTitle = Nothing
        '
        'butStartService
        '
        Me.butStartService.AltKey = Nothing
        Me.butStartService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butStartService.Image = CType(resources.GetObject("butStartService.Image"), System.Drawing.Image)
        Me.butStartService.SmallImage = CType(resources.GetObject("butStartService.SmallImage"), System.Drawing.Image)
        Me.butStartService.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butStartService.Tag = Nothing
        Me.butStartService.Text = "Start"
        Me.butStartService.ToolTip = Nothing
        Me.butStartService.ToolTipImage = Nothing
        Me.butStartService.ToolTipTitle = Nothing
        '
        'butPauseService
        '
        Me.butPauseService.AltKey = Nothing
        Me.butPauseService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butPauseService.Image = CType(resources.GetObject("butPauseService.Image"), System.Drawing.Image)
        Me.butPauseService.SmallImage = CType(resources.GetObject("butPauseService.SmallImage"), System.Drawing.Image)
        Me.butPauseService.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butPauseService.Tag = Nothing
        Me.butPauseService.Text = "Pause"
        Me.butPauseService.ToolTip = Nothing
        Me.butPauseService.ToolTipImage = Nothing
        Me.butPauseService.ToolTipTitle = Nothing
        '
        'butResumeService
        '
        Me.butResumeService.AltKey = Nothing
        Me.butResumeService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butResumeService.Image = CType(resources.GetObject("butResumeService.Image"), System.Drawing.Image)
        Me.butResumeService.SmallImage = CType(resources.GetObject("butResumeService.SmallImage"), System.Drawing.Image)
        Me.butResumeService.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butResumeService.Tag = Nothing
        Me.butResumeService.Text = "Resume"
        Me.butResumeService.ToolTip = Nothing
        Me.butResumeService.ToolTipImage = Nothing
        Me.butResumeService.ToolTipTitle = Nothing
        '
        'butShutdownService
        '
        Me.butShutdownService.AltKey = Nothing
        Me.butShutdownService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butShutdownService.Image = CType(resources.GetObject("butShutdownService.Image"), System.Drawing.Image)
        Me.butShutdownService.SmallImage = CType(resources.GetObject("butShutdownService.SmallImage"), System.Drawing.Image)
        Me.butShutdownService.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butShutdownService.Tag = Nothing
        Me.butShutdownService.Text = "Shutdown"
        Me.butShutdownService.ToolTip = Nothing
        Me.butShutdownService.ToolTipImage = Nothing
        Me.butShutdownService.ToolTipTitle = Nothing
        '
        'RBServiceStartType
        '
        Me.RBServiceStartType.ButtonMoreEnabled = False
        Me.RBServiceStartType.ButtonMoreVisible = False
        Me.RBServiceStartType.Items.Add(Me.butServiceStartType)
        Me.RBServiceStartType.Tag = Nothing
        Me.RBServiceStartType.Text = "Start type"
        '
        'butServiceStartType
        '
        Me.butServiceStartType.AltKey = Nothing
        Me.butServiceStartType.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceStartType.DropDownItems.Add(Me.butAutomaticStart)
        Me.butServiceStartType.DropDownItems.Add(Me.butOnDemandStart)
        Me.butServiceStartType.DropDownItems.Add(Me.butDisabledStart)
        Me.butServiceStartType.Image = CType(resources.GetObject("butServiceStartType.Image"), System.Drawing.Image)
        Me.butServiceStartType.SmallImage = CType(resources.GetObject("butServiceStartType.SmallImage"), System.Drawing.Image)
        Me.butServiceStartType.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butServiceStartType.Tag = Nothing
        Me.butServiceStartType.Text = "Start Type"
        Me.butServiceStartType.ToolTip = Nothing
        Me.butServiceStartType.ToolTipImage = Nothing
        Me.butServiceStartType.ToolTipTitle = Nothing
        '
        'butAutomaticStart
        '
        Me.butAutomaticStart.AltKey = Nothing
        Me.butAutomaticStart.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butAutomaticStart.Image = Nothing
        Me.butAutomaticStart.SmallImage = CType(resources.GetObject("butAutomaticStart.SmallImage"), System.Drawing.Image)
        Me.butAutomaticStart.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butAutomaticStart.Tag = Nothing
        Me.butAutomaticStart.Text = "Automatic"
        Me.butAutomaticStart.ToolTip = Nothing
        Me.butAutomaticStart.ToolTipImage = Nothing
        Me.butAutomaticStart.ToolTipTitle = Nothing
        '
        'butOnDemandStart
        '
        Me.butOnDemandStart.AltKey = Nothing
        Me.butOnDemandStart.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butOnDemandStart.Image = CType(resources.GetObject("butOnDemandStart.Image"), System.Drawing.Image)
        Me.butOnDemandStart.SmallImage = CType(resources.GetObject("butOnDemandStart.SmallImage"), System.Drawing.Image)
        Me.butOnDemandStart.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butOnDemandStart.Tag = Nothing
        Me.butOnDemandStart.Text = "On Demand"
        Me.butOnDemandStart.ToolTip = Nothing
        Me.butOnDemandStart.ToolTipImage = Nothing
        Me.butOnDemandStart.ToolTipTitle = Nothing
        '
        'butDisabledStart
        '
        Me.butDisabledStart.AltKey = Nothing
        Me.butDisabledStart.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butDisabledStart.Image = CType(resources.GetObject("butDisabledStart.Image"), System.Drawing.Image)
        Me.butDisabledStart.SmallImage = CType(resources.GetObject("butDisabledStart.SmallImage"), System.Drawing.Image)
        Me.butDisabledStart.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butDisabledStart.Tag = Nothing
        Me.butDisabledStart.Text = "Disabled"
        Me.butDisabledStart.ToolTip = Nothing
        Me.butDisabledStart.ToolTipImage = Nothing
        Me.butDisabledStart.ToolTipTitle = Nothing
        '
        'RBServiceFile
        '
        Me.RBServiceFile.ButtonMoreEnabled = False
        Me.RBServiceFile.ButtonMoreVisible = False
        Me.RBServiceFile.Items.Add(Me.butServiceFileProp)
        Me.RBServiceFile.Items.Add(Me.butServiceOpenDir)
        Me.RBServiceFile.Items.Add(Me.butServiceFileDetails)
        Me.RBServiceFile.Tag = Nothing
        Me.RBServiceFile.Text = "Executable"
        '
        'butServiceFileProp
        '
        Me.butServiceFileProp.AltKey = Nothing
        Me.butServiceFileProp.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceFileProp.Image = CType(resources.GetObject("butServiceFileProp.Image"), System.Drawing.Image)
        Me.butServiceFileProp.SmallImage = CType(resources.GetObject("butServiceFileProp.SmallImage"), System.Drawing.Image)
        Me.butServiceFileProp.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceFileProp.Tag = Nothing
        Me.butServiceFileProp.Text = "Show file properties"
        Me.butServiceFileProp.ToolTip = Nothing
        Me.butServiceFileProp.ToolTipImage = Nothing
        Me.butServiceFileProp.ToolTipTitle = Nothing
        '
        'butServiceOpenDir
        '
        Me.butServiceOpenDir.AltKey = Nothing
        Me.butServiceOpenDir.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceOpenDir.Image = CType(resources.GetObject("butServiceOpenDir.Image"), System.Drawing.Image)
        Me.butServiceOpenDir.SmallImage = CType(resources.GetObject("butServiceOpenDir.SmallImage"), System.Drawing.Image)
        Me.butServiceOpenDir.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceOpenDir.Tag = Nothing
        Me.butServiceOpenDir.Text = "Open file   directory"
        Me.butServiceOpenDir.ToolTip = Nothing
        Me.butServiceOpenDir.ToolTipImage = Nothing
        Me.butServiceOpenDir.ToolTipTitle = Nothing
        '
        'butServiceFileDetails
        '
        Me.butServiceFileDetails.AltKey = Nothing
        Me.butServiceFileDetails.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceFileDetails.Image = CType(resources.GetObject("butServiceFileDetails.Image"), System.Drawing.Image)
        Me.butServiceFileDetails.SmallImage = CType(resources.GetObject("butServiceFileDetails.SmallImage"), System.Drawing.Image)
        Me.butServiceFileDetails.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceFileDetails.Tag = Nothing
        Me.butServiceFileDetails.Text = "Details"
        Me.butServiceFileDetails.ToolTip = Nothing
        Me.butServiceFileDetails.ToolTipImage = Nothing
        Me.butServiceFileDetails.ToolTipTitle = Nothing
        '
        'RBServiceOnline
        '
        Me.RBServiceOnline.ButtonMoreEnabled = False
        Me.RBServiceOnline.ButtonMoreVisible = False
        Me.RBServiceOnline.Items.Add(Me.butServiceGoogle)
        Me.RBServiceOnline.Tag = Nothing
        Me.RBServiceOnline.Text = "Online"
        '
        'butServiceGoogle
        '
        Me.butServiceGoogle.AltKey = Nothing
        Me.butServiceGoogle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceGoogle.Image = CType(resources.GetObject("butServiceGoogle.Image"), System.Drawing.Image)
        Me.butServiceGoogle.SmallImage = CType(resources.GetObject("butServiceGoogle.SmallImage"), System.Drawing.Image)
        Me.butServiceGoogle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceGoogle.Tag = Nothing
        Me.butServiceGoogle.Text = "Google search"
        Me.butServiceGoogle.ToolTip = Nothing
        Me.butServiceGoogle.ToolTipImage = Nothing
        Me.butServiceGoogle.ToolTipTitle = Nothing
        '
        'FileTab
        '
        Me.FileTab.Panels.Add(Me.RBFileDelete)
        Me.FileTab.Panels.Add(Me.RBFileOnline)
        Me.FileTab.Panels.Add(Me.RBFileOther)
        Me.FileTab.Panels.Add(Me.RBFileOthers)
        Me.FileTab.Tag = Nothing
        Me.FileTab.Text = "File"
        '
        'RBFileDelete
        '
        Me.RBFileDelete.ButtonMoreEnabled = False
        Me.RBFileDelete.ButtonMoreVisible = False
        Me.RBFileDelete.Items.Add(Me.butMoveFileToTrash)
        Me.RBFileDelete.Items.Add(Me.butDeleteFile)
        Me.RBFileDelete.Items.Add(Me.butShreddFile)
        Me.RBFileDelete.Tag = Nothing
        Me.RBFileDelete.Text = "Delete"
        '
        'butMoveFileToTrash
        '
        Me.butMoveFileToTrash.AltKey = Nothing
        Me.butMoveFileToTrash.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMoveFileToTrash.Image = CType(resources.GetObject("butMoveFileToTrash.Image"), System.Drawing.Image)
        Me.butMoveFileToTrash.SmallImage = CType(resources.GetObject("butMoveFileToTrash.SmallImage"), System.Drawing.Image)
        Me.butMoveFileToTrash.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butMoveFileToTrash.Tag = Nothing
        Me.butMoveFileToTrash.Text = "Trash"
        Me.butMoveFileToTrash.ToolTip = Nothing
        Me.butMoveFileToTrash.ToolTipImage = Nothing
        Me.butMoveFileToTrash.ToolTipTitle = Nothing
        '
        'butDeleteFile
        '
        Me.butDeleteFile.AltKey = Nothing
        Me.butDeleteFile.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butDeleteFile.Image = CType(resources.GetObject("butDeleteFile.Image"), System.Drawing.Image)
        Me.butDeleteFile.SmallImage = CType(resources.GetObject("butDeleteFile.SmallImage"), System.Drawing.Image)
        Me.butDeleteFile.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butDeleteFile.Tag = Nothing
        Me.butDeleteFile.Text = "Delete"
        Me.butDeleteFile.ToolTip = Nothing
        Me.butDeleteFile.ToolTipImage = Nothing
        Me.butDeleteFile.ToolTipTitle = Nothing
        '
        'butShreddFile
        '
        Me.butShreddFile.AltKey = Nothing
        Me.butShreddFile.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butShreddFile.Image = CType(resources.GetObject("butShreddFile.Image"), System.Drawing.Image)
        Me.butShreddFile.SmallImage = CType(resources.GetObject("butShreddFile.SmallImage"), System.Drawing.Image)
        Me.butShreddFile.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butShreddFile.Tag = Nothing
        Me.butShreddFile.Text = "Shredd"
        Me.butShreddFile.ToolTip = Nothing
        Me.butShreddFile.ToolTipImage = Nothing
        Me.butShreddFile.ToolTipTitle = Nothing
        '
        'RBFileOnline
        '
        Me.RBFileOnline.ButtonMoreEnabled = False
        Me.RBFileOnline.ButtonMoreVisible = False
        Me.RBFileOnline.Items.Add(Me.butFileGoogleSearch)
        Me.RBFileOnline.Tag = Nothing
        Me.RBFileOnline.Text = "Online"
        '
        'butFileGoogleSearch
        '
        Me.butFileGoogleSearch.AltKey = Nothing
        Me.butFileGoogleSearch.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileGoogleSearch.Image = CType(resources.GetObject("butFileGoogleSearch.Image"), System.Drawing.Image)
        Me.butFileGoogleSearch.SmallImage = CType(resources.GetObject("butFileGoogleSearch.SmallImage"), System.Drawing.Image)
        Me.butFileGoogleSearch.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileGoogleSearch.Tag = Nothing
        Me.butFileGoogleSearch.Text = "Google search"
        Me.butFileGoogleSearch.ToolTip = Nothing
        Me.butFileGoogleSearch.ToolTipImage = Nothing
        Me.butFileGoogleSearch.ToolTipTitle = Nothing
        '
        'RBFileOther
        '
        Me.RBFileOther.ButtonMoreEnabled = False
        Me.RBFileOther.ButtonMoreVisible = False
        Me.RBFileOther.Items.Add(Me.butFileProperties)
        Me.RBFileOther.Items.Add(Me.butFileOpenDir)
        Me.RBFileOther.Items.Add(Me.butFileShowFolderProperties)
        Me.RBFileOther.Tag = Nothing
        Me.RBFileOther.Text = "Properties"
        '
        'butFileProperties
        '
        Me.butFileProperties.AltKey = Nothing
        Me.butFileProperties.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileProperties.Image = CType(resources.GetObject("butFileProperties.Image"), System.Drawing.Image)
        Me.butFileProperties.SmallImage = CType(resources.GetObject("butFileProperties.SmallImage"), System.Drawing.Image)
        Me.butFileProperties.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileProperties.Tag = Nothing
        Me.butFileProperties.Text = "Show file properties"
        Me.butFileProperties.ToolTip = Nothing
        Me.butFileProperties.ToolTipImage = Nothing
        Me.butFileProperties.ToolTipTitle = Nothing
        '
        'butFileOpenDir
        '
        Me.butFileOpenDir.AltKey = Nothing
        Me.butFileOpenDir.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileOpenDir.Image = CType(resources.GetObject("butFileOpenDir.Image"), System.Drawing.Image)
        Me.butFileOpenDir.SmallImage = CType(resources.GetObject("butFileOpenDir.SmallImage"), System.Drawing.Image)
        Me.butFileOpenDir.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileOpenDir.Tag = Nothing
        Me.butFileOpenDir.Text = "Open file   directory"
        Me.butFileOpenDir.ToolTip = Nothing
        Me.butFileOpenDir.ToolTipImage = Nothing
        Me.butFileOpenDir.ToolTipTitle = Nothing
        '
        'JobsTab
        '
        Me.JobsTab.Panels.Add(Me.RBAdd)
        Me.JobsTab.Panels.Add(Me.RBJobsOpenSave)
        Me.JobsTab.Tag = Nothing
        Me.JobsTab.Text = "Jobs"
        '
        'RBAdd
        '
        Me.RBAdd.ButtonMoreEnabled = False
        Me.RBAdd.ButtonMoreVisible = False
        Me.RBAdd.Items.Add(Me.butAddJob)
        Me.RBAdd.Tag = Nothing
        Me.RBAdd.Text = "Job management"
        '
        'butAddJob
        '
        Me.butAddJob.AltKey = Nothing
        Me.butAddJob.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butAddJob.Image = CType(resources.GetObject("butAddJob.Image"), System.Drawing.Image)
        Me.butAddJob.SmallImage = CType(resources.GetObject("butAddJob.SmallImage"), System.Drawing.Image)
        Me.butAddJob.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butAddJob.Tag = Nothing
        Me.butAddJob.Text = "New job"
        Me.butAddJob.ToolTip = Nothing
        Me.butAddJob.ToolTipImage = Nothing
        Me.butAddJob.ToolTipTitle = Nothing
        '
        'RBJobsOpenSave
        '
        Me.RBJobsOpenSave.ButtonMoreEnabled = False
        Me.RBJobsOpenSave.ButtonMoreVisible = False
        Me.RBJobsOpenSave.Items.Add(Me.butOpenJobList)
        Me.RBJobsOpenSave.Items.Add(Me.butSaveJobList)
        Me.RBJobsOpenSave.Tag = Nothing
        Me.RBJobsOpenSave.Text = "Job list"
        '
        'butOpenJobList
        '
        Me.butOpenJobList.AltKey = Nothing
        Me.butOpenJobList.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butOpenJobList.Image = CType(resources.GetObject("butOpenJobList.Image"), System.Drawing.Image)
        Me.butOpenJobList.SmallImage = CType(resources.GetObject("butOpenJobList.SmallImage"), System.Drawing.Image)
        Me.butOpenJobList.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butOpenJobList.Tag = Nothing
        Me.butOpenJobList.Text = "Open"
        Me.butOpenJobList.ToolTip = Nothing
        Me.butOpenJobList.ToolTipImage = Nothing
        Me.butOpenJobList.ToolTipTitle = Nothing
        '
        'butSaveJobList
        '
        Me.butSaveJobList.AltKey = Nothing
        Me.butSaveJobList.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSaveJobList.Image = CType(resources.GetObject("butSaveJobList.Image"), System.Drawing.Image)
        Me.butSaveJobList.SmallImage = CType(resources.GetObject("butSaveJobList.SmallImage"), System.Drawing.Image)
        Me.butSaveJobList.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSaveJobList.Tag = Nothing
        Me.butSaveJobList.Text = "Save"
        Me.butSaveJobList.ToolTip = Nothing
        Me.butSaveJobList.ToolTipImage = Nothing
        Me.butSaveJobList.ToolTipTitle = Nothing
        '
        'SearchTab
        '
        Me.SearchTab.Tag = Nothing
        Me.SearchTab.Text = "Search"
        '
        'ReportTab
        '
        Me.ReportTab.Panels.Add(Me.RBSaveReport)
        Me.ReportTab.Panels.Add(Me.RBOthers)
        Me.ReportTab.Panels.Add(Me.RBDisplay)
        Me.ReportTab.Tag = Nothing
        Me.ReportTab.Text = "Misc"
        '
        'RBSaveReport
        '
        Me.RBSaveReport.ButtonMoreEnabled = False
        Me.RBSaveReport.ButtonMoreVisible = False
        Me.RBSaveReport.Items.Add(Me.butSaveProcessReport)
        Me.RBSaveReport.Items.Add(Me.butSaveServiceReport)
        Me.RBSaveReport.Tag = Nothing
        Me.RBSaveReport.Text = "Save report"
        '
        'butSaveProcessReport
        '
        Me.butSaveProcessReport.AltKey = Nothing
        Me.butSaveProcessReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSaveProcessReport.Image = CType(resources.GetObject("butSaveProcessReport.Image"), System.Drawing.Image)
        Me.butSaveProcessReport.SmallImage = CType(resources.GetObject("butSaveProcessReport.SmallImage"), System.Drawing.Image)
        Me.butSaveProcessReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSaveProcessReport.Tag = Nothing
        Me.butSaveProcessReport.Text = "Process report"
        Me.butSaveProcessReport.ToolTip = Nothing
        Me.butSaveProcessReport.ToolTipImage = Nothing
        Me.butSaveProcessReport.ToolTipTitle = Nothing
        '
        'butSaveServiceReport
        '
        Me.butSaveServiceReport.AltKey = Nothing
        Me.butSaveServiceReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSaveServiceReport.Image = CType(resources.GetObject("butSaveServiceReport.Image"), System.Drawing.Image)
        Me.butSaveServiceReport.SmallImage = CType(resources.GetObject("butSaveServiceReport.SmallImage"), System.Drawing.Image)
        Me.butSaveServiceReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSaveServiceReport.Tag = Nothing
        Me.butSaveServiceReport.Text = "Service report"
        Me.butSaveServiceReport.ToolTip = Nothing
        Me.butSaveServiceReport.ToolTipImage = Nothing
        Me.butSaveServiceReport.ToolTipTitle = Nothing
        '
        'RBOthers
        '
        Me.RBOthers.ButtonMoreEnabled = False
        Me.RBOthers.ButtonMoreVisible = False
        Me.RBOthers.Items.Add(Me.butTakeFullPower)
        Me.RBOthers.Items.Add(Me.butOptions)
        Me.RBOthers.Tag = Nothing
        Me.RBOthers.Text = "Other"
        '
        'butTakeFullPower
        '
        Me.butTakeFullPower.AltKey = Nothing
        Me.butTakeFullPower.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butTakeFullPower.Image = CType(resources.GetObject("butTakeFullPower.Image"), System.Drawing.Image)
        Me.butTakeFullPower.SmallImage = CType(resources.GetObject("butTakeFullPower.SmallImage"), System.Drawing.Image)
        Me.butTakeFullPower.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butTakeFullPower.Tag = Nothing
        Me.butTakeFullPower.Text = "Full power"
        Me.butTakeFullPower.ToolTip = Nothing
        Me.butTakeFullPower.ToolTipImage = Nothing
        Me.butTakeFullPower.ToolTipTitle = Nothing
        '
        'butOptions
        '
        Me.butOptions.AltKey = Nothing
        Me.butOptions.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butOptions.Image = CType(resources.GetObject("butOptions.Image"), System.Drawing.Image)
        Me.butOptions.SmallImage = CType(resources.GetObject("butOptions.SmallImage"), System.Drawing.Image)
        Me.butOptions.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butOptions.Tag = Nothing
        Me.butOptions.Text = "Preferences"
        Me.butOptions.ToolTip = Nothing
        Me.butOptions.ToolTipImage = Nothing
        Me.butOptions.ToolTipTitle = Nothing
        '
        'RBDisplay
        '
        Me.RBDisplay.ButtonMoreEnabled = False
        Me.RBDisplay.ButtonMoreVisible = False
        Me.RBDisplay.Items.Add(Me.butTopMost)
        Me.RBDisplay.Tag = Nothing
        Me.RBDisplay.Text = "Display settings"
        '
        'butTopMost
        '
        Me.butTopMost.AltKey = Nothing
        Me.butTopMost.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butTopMost.Image = CType(resources.GetObject("butTopMost.Image"), System.Drawing.Image)
        Me.butTopMost.SmallImage = CType(resources.GetObject("butTopMost.SmallImage"), System.Drawing.Image)
        Me.butTopMost.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butTopMost.Tag = Nothing
        Me.butTopMost.Text = "Always display"
        Me.butTopMost.ToolTip = Nothing
        Me.butTopMost.ToolTipImage = Nothing
        Me.butTopMost.ToolTipTitle = Nothing
        '
        'HelpTab
        '
        Me.HelpTab.Panels.Add(Me.RBHelpAction)
        Me.HelpTab.Panels.Add(Me.RBHelpWeb)
        Me.HelpTab.Tag = Nothing
        Me.HelpTab.Text = "Help"
        '
        'RBHelpAction
        '
        Me.RBHelpAction.ButtonMoreEnabled = False
        Me.RBHelpAction.ButtonMoreVisible = False
        Me.RBHelpAction.Items.Add(Me.butDonate)
        Me.RBHelpAction.Items.Add(Me.butAbout)
        Me.RBHelpAction.Tag = Nothing
        Me.RBHelpAction.Text = "Actions"
        '
        'butDonate
        '
        Me.butDonate.AltKey = Nothing
        Me.butDonate.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butDonate.Image = CType(resources.GetObject("butDonate.Image"), System.Drawing.Image)
        Me.butDonate.SmallImage = CType(resources.GetObject("butDonate.SmallImage"), System.Drawing.Image)
        Me.butDonate.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butDonate.Tag = Nothing
        Me.butDonate.Text = "Donate"
        Me.butDonate.ToolTip = Nothing
        Me.butDonate.ToolTipImage = Nothing
        Me.butDonate.ToolTipTitle = Nothing
        '
        'butAbout
        '
        Me.butAbout.AltKey = Nothing
        Me.butAbout.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butAbout.Image = CType(resources.GetObject("butAbout.Image"), System.Drawing.Image)
        Me.butAbout.SmallImage = CType(resources.GetObject("butAbout.SmallImage"), System.Drawing.Image)
        Me.butAbout.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butAbout.Tag = Nothing
        Me.butAbout.Text = "About"
        Me.butAbout.ToolTip = Nothing
        Me.butAbout.ToolTipImage = Nothing
        Me.butAbout.ToolTipTitle = Nothing
        '
        'RBHelpWeb
        '
        Me.RBHelpWeb.ButtonMoreEnabled = False
        Me.RBHelpWeb.ButtonMoreVisible = False
        Me.RBHelpWeb.Items.Add(Me.butWebite)
        Me.RBHelpWeb.Items.Add(Me.butProjectPage)
        Me.RBHelpWeb.Items.Add(Me.butDownload)
        Me.RBHelpWeb.Tag = Nothing
        Me.RBHelpWeb.Text = "YAPM on Internet"
        '
        'butWebite
        '
        Me.butWebite.AltKey = Nothing
        Me.butWebite.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWebite.Image = CType(resources.GetObject("butWebite.Image"), System.Drawing.Image)
        Me.butWebite.SmallImage = CType(resources.GetObject("butWebite.SmallImage"), System.Drawing.Image)
        Me.butWebite.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWebite.Tag = Nothing
        Me.butWebite.Text = "Website"
        Me.butWebite.ToolTip = Nothing
        Me.butWebite.ToolTipImage = Nothing
        Me.butWebite.ToolTipTitle = Nothing
        '
        'butProjectPage
        '
        Me.butProjectPage.AltKey = Nothing
        Me.butProjectPage.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProjectPage.Image = CType(resources.GetObject("butProjectPage.Image"), System.Drawing.Image)
        Me.butProjectPage.SmallImage = CType(resources.GetObject("butProjectPage.SmallImage"), System.Drawing.Image)
        Me.butProjectPage.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProjectPage.Tag = Nothing
        Me.butProjectPage.Text = "Project page on Sourceforge.net"
        Me.butProjectPage.ToolTip = Nothing
        Me.butProjectPage.ToolTipImage = Nothing
        Me.butProjectPage.ToolTipTitle = Nothing
        '
        'butDownload
        '
        Me.butDownload.AltKey = Nothing
        Me.butDownload.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butDownload.Image = CType(resources.GetObject("butDownload.Image"), System.Drawing.Image)
        Me.butDownload.SmallImage = CType(resources.GetObject("butDownload.SmallImage"), System.Drawing.Image)
        Me.butDownload.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butDownload.Tag = Nothing
        Me.butDownload.Text = "Downloads"
        Me.butDownload.ToolTip = Nothing
        Me.butDownload.ToolTipImage = Nothing
        Me.butDownload.ToolTipTitle = Nothing
        '
        'cmdTray
        '
        Me.cmdTray.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTray.Image = Global.YAPM.My.Resources.Resources.down
        Me.cmdTray.Location = New System.Drawing.Point(5, 2)
        Me.cmdTray.Name = "cmdTray"
        Me.cmdTray.Size = New System.Drawing.Size(38, 20)
        Me.cmdTray.TabIndex = 45
        Me.cmdTray.UseVisualStyleBackColor = True
        '
        'RibbonButton1
        '
        Me.RibbonButton1.AltKey = Nothing
        Me.RibbonButton1.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.RibbonButton1.Image = CType(resources.GetObject("RibbonButton1.Image"), System.Drawing.Image)
        Me.RibbonButton1.SmallImage = CType(resources.GetObject("RibbonButton1.SmallImage"), System.Drawing.Image)
        Me.RibbonButton1.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.RibbonButton1.Tag = Nothing
        Me.RibbonButton1.Text = "Stop"
        Me.RibbonButton1.ToolTip = Nothing
        Me.RibbonButton1.ToolTipImage = Nothing
        Me.RibbonButton1.ToolTipTitle = Nothing
        '
        'RibbonButtonList1
        '
        Me.RibbonButtonList1.AltKey = Nothing
        Me.RibbonButtonList1.ButtonsSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large
        Me.RibbonButtonList1.Image = Nothing
        Me.RibbonButtonList1.ItemsSizeInDropwDownMode = New System.Drawing.Size(7, 5)
        Me.RibbonButtonList1.Tag = Nothing
        Me.RibbonButtonList1.Text = Nothing
        Me.RibbonButtonList1.ToolTip = Nothing
        Me.RibbonButtonList1.ToolTipImage = Nothing
        Me.RibbonButtonList1.ToolTipTitle = Nothing
        '
        'panelMenu2
        '
        Me.panelMenu2.Controls.Add(Me.Label2)
        Me.panelMenu2.Controls.Add(Me.lblResCount2)
        Me.panelMenu2.Controls.Add(Me.txtServiceSearch)
        Me.panelMenu2.Location = New System.Drawing.Point(14, 287)
        Me.panelMenu2.Name = "panelMenu2"
        Me.panelMenu2.Size = New System.Drawing.Size(766, 28)
        Me.panelMenu2.TabIndex = 46
        Me.panelMenu2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Search service"
        '
        'lblResCount2
        '
        Me.lblResCount2.AutoSize = True
        Me.lblResCount2.Location = New System.Drawing.Point(596, 6)
        Me.lblResCount2.Name = "lblResCount2"
        Me.lblResCount2.Size = New System.Drawing.Size(56, 13)
        Me.lblResCount2.TabIndex = 2
        Me.lblResCount2.Text = "0 result(s)"
        '
        'txtServiceSearch
        '
        Me.txtServiceSearch.Location = New System.Drawing.Point(89, 3)
        Me.txtServiceSearch.Name = "txtServiceSearch"
        Me.txtServiceSearch.Size = New System.Drawing.Size(501, 21)
        Me.txtServiceSearch.TabIndex = 1
        '
        'panelMain5
        '
        Me.panelMain5.Controls.Add(Me.fileSplitContainer)
        Me.panelMain5.Controls.Add(Me.txtFile)
        Me.panelMain5.Controls.Add(Me.cmdFileClipboard)
        Me.panelMain5.Controls.Add(Me.pctFileSmall)
        Me.panelMain5.Controls.Add(Me.pctFileBig)
        Me.panelMain5.Location = New System.Drawing.Point(117, 181)
        Me.panelMain5.Name = "panelMain5"
        Me.panelMain5.Size = New System.Drawing.Size(640, 317)
        Me.panelMain5.TabIndex = 47
        Me.panelMain5.Visible = False
        '
        'fileSplitContainer
        '
        Me.fileSplitContainer.IsSplitterFixed = True
        Me.fileSplitContainer.Location = New System.Drawing.Point(3, 43)
        Me.fileSplitContainer.Name = "fileSplitContainer"
        Me.fileSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'fileSplitContainer.Panel1
        '
        Me.fileSplitContainer.Panel1.Controls.Add(Me.rtb3)
        Me.fileSplitContainer.Size = New System.Drawing.Size(634, 271)
        Me.fileSplitContainer.SplitterDistance = 180
        Me.fileSplitContainer.TabIndex = 0
        '
        'rtb3
        '
        Me.rtb3.AutoWordSelection = True
        Me.rtb3.BackColor = System.Drawing.Color.White
        Me.rtb3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb3.HideSelection = False
        Me.rtb3.Location = New System.Drawing.Point(0, 0)
        Me.rtb3.Name = "rtb3"
        Me.rtb3.ReadOnly = True
        Me.rtb3.Size = New System.Drawing.Size(634, 180)
        Me.rtb3.TabIndex = 12
        Me.rtb3.Text = ""
        '
        'txtFile
        '
        Me.txtFile.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtFile.Location = New System.Drawing.Point(8, 12)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(240, 16)
        Me.txtFile.TabIndex = 13
        Me.txtFile.Text = "No selected file"
        '
        'cmdFileClipboard
        '
        Me.cmdFileClipboard.Enabled = False
        Me.cmdFileClipboard.Location = New System.Drawing.Point(395, 14)
        Me.cmdFileClipboard.Name = "cmdFileClipboard"
        Me.cmdFileClipboard.Size = New System.Drawing.Size(99, 21)
        Me.cmdFileClipboard.TabIndex = 12
        Me.cmdFileClipboard.Text = "Copy to clipboard"
        Me.cmdFileClipboard.UseVisualStyleBackColor = True
        '
        'pctFileSmall
        '
        Me.pctFileSmall.ContextMenuStrip = Me.menuCopyPctSmall
        Me.pctFileSmall.Location = New System.Drawing.Point(503, 19)
        Me.pctFileSmall.Name = "pctFileSmall"
        Me.pctFileSmall.Size = New System.Drawing.Size(16, 16)
        Me.pctFileSmall.TabIndex = 9
        Me.pctFileSmall.TabStop = False
        '
        'pctFileBig
        '
        Me.pctFileBig.ContextMenuStrip = Me.menuCopyPctbig
        Me.pctFileBig.Location = New System.Drawing.Point(525, 3)
        Me.pctFileBig.Name = "pctFileBig"
        Me.pctFileBig.Size = New System.Drawing.Size(32, 32)
        Me.pctFileBig.TabIndex = 8
        Me.pctFileBig.TabStop = False
        '
        'butFileShowFolderProperties
        '
        Me.butFileShowFolderProperties.AltKey = Nothing
        Me.butFileShowFolderProperties.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileShowFolderProperties.Image = CType(resources.GetObject("butFileShowFolderProperties.Image"), System.Drawing.Image)
        Me.butFileShowFolderProperties.SmallImage = CType(resources.GetObject("butFileShowFolderProperties.SmallImage"), System.Drawing.Image)
        Me.butFileShowFolderProperties.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileShowFolderProperties.Tag = Nothing
        Me.butFileShowFolderProperties.Text = "Show dir. properties"
        Me.butFileShowFolderProperties.ToolTip = Nothing
        Me.butFileShowFolderProperties.ToolTipImage = Nothing
        Me.butFileShowFolderProperties.ToolTipTitle = Nothing
        '
        'RBFileOthers
        '
        Me.RBFileOthers.ButtonMoreEnabled = False
        Me.RBFileOthers.ButtonMoreVisible = False
        Me.RBFileOthers.Items.Add(Me.butFileOthersActions)
        Me.RBFileOthers.Tag = Nothing
        Me.RBFileOthers.Text = "Others"
        '
        'butFileOthersActions
        '
        Me.butFileOthersActions.AltKey = Nothing
        Me.butFileOthersActions.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileOthersActions.DropDownItems.Add(Me.sepFile1)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileRename)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileCopy)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileMove)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileOpen)
        Me.butFileOthersActions.DropDownItems.Add(Me.sepFile2)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileSeeStrings)
        Me.butFileOthersActions.DropDownItems.Add(Me.sepFile3)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileEncrypt)
        Me.butFileOthersActions.DropDownItems.Add(Me.butFileDecrypt)
        Me.butFileOthersActions.Image = CType(resources.GetObject("butFileOthersActions.Image"), System.Drawing.Image)
        Me.butFileOthersActions.SmallImage = CType(resources.GetObject("butFileOthersActions.SmallImage"), System.Drawing.Image)
        Me.butFileOthersActions.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butFileOthersActions.Tag = Nothing
        Me.butFileOthersActions.Text = "Others"
        Me.butFileOthersActions.ToolTip = Nothing
        Me.butFileOthersActions.ToolTipImage = Nothing
        Me.butFileOthersActions.ToolTipTitle = Nothing
        '
        'sepFile1
        '
        Me.sepFile1.AltKey = Nothing
        Me.sepFile1.Image = Nothing
        Me.sepFile1.Tag = Nothing
        Me.sepFile1.Text = "Explorer actions"
        Me.sepFile1.ToolTip = Nothing
        Me.sepFile1.ToolTipImage = Nothing
        Me.sepFile1.ToolTipTitle = Nothing
        '
        'butFileRename
        '
        Me.butFileRename.AltKey = Nothing
        Me.butFileRename.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileRename.Image = CType(resources.GetObject("butFileRename.Image"), System.Drawing.Image)
        Me.butFileRename.SmallImage = CType(resources.GetObject("butFileRename.SmallImage"), System.Drawing.Image)
        Me.butFileRename.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileRename.Tag = Nothing
        Me.butFileRename.Text = "Rename"
        Me.butFileRename.ToolTip = Nothing
        Me.butFileRename.ToolTipImage = Nothing
        Me.butFileRename.ToolTipTitle = Nothing
        '
        'butFileCopy
        '
        Me.butFileCopy.AltKey = Nothing
        Me.butFileCopy.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileCopy.Image = CType(resources.GetObject("butFileCopy.Image"), System.Drawing.Image)
        Me.butFileCopy.SmallImage = CType(resources.GetObject("butFileCopy.SmallImage"), System.Drawing.Image)
        Me.butFileCopy.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileCopy.Tag = Nothing
        Me.butFileCopy.Text = "Copy"
        Me.butFileCopy.ToolTip = Nothing
        Me.butFileCopy.ToolTipImage = Nothing
        Me.butFileCopy.ToolTipTitle = Nothing
        '
        'butFileMove
        '
        Me.butFileMove.AltKey = Nothing
        Me.butFileMove.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileMove.Image = CType(resources.GetObject("butFileMove.Image"), System.Drawing.Image)
        Me.butFileMove.SmallImage = CType(resources.GetObject("butFileMove.SmallImage"), System.Drawing.Image)
        Me.butFileMove.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileMove.Tag = Nothing
        Me.butFileMove.Text = "Move"
        Me.butFileMove.ToolTip = Nothing
        Me.butFileMove.ToolTipImage = Nothing
        Me.butFileMove.ToolTipTitle = Nothing
        '
        'butFileOpen
        '
        Me.butFileOpen.AltKey = Nothing
        Me.butFileOpen.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileOpen.Image = CType(resources.GetObject("butFileOpen.Image"), System.Drawing.Image)
        Me.butFileOpen.SmallImage = CType(resources.GetObject("butFileOpen.SmallImage"), System.Drawing.Image)
        Me.butFileOpen.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileOpen.Tag = Nothing
        Me.butFileOpen.Text = "Open"
        Me.butFileOpen.ToolTip = Nothing
        Me.butFileOpen.ToolTipImage = Nothing
        Me.butFileOpen.ToolTipTitle = Nothing
        '
        'sepFile2
        '
        Me.sepFile2.AltKey = Nothing
        Me.sepFile2.Image = Nothing
        Me.sepFile2.Tag = Nothing
        Me.sepFile2.Text = "File content"
        Me.sepFile2.ToolTip = Nothing
        Me.sepFile2.ToolTipImage = Nothing
        Me.sepFile2.ToolTipTitle = Nothing
        '
        'butFileSeeStrings
        '
        Me.butFileSeeStrings.AltKey = Nothing
        Me.butFileSeeStrings.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileSeeStrings.Image = CType(resources.GetObject("butFileSeeStrings.Image"), System.Drawing.Image)
        Me.butFileSeeStrings.SmallImage = CType(resources.GetObject("butFileSeeStrings.SmallImage"), System.Drawing.Image)
        Me.butFileSeeStrings.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileSeeStrings.Tag = Nothing
        Me.butFileSeeStrings.Text = "Show file strings"
        Me.butFileSeeStrings.ToolTip = Nothing
        Me.butFileSeeStrings.ToolTipImage = Nothing
        Me.butFileSeeStrings.ToolTipTitle = Nothing
        '
        'sepFile3
        '
        Me.sepFile3.AltKey = Nothing
        Me.sepFile3.Image = Nothing
        Me.sepFile3.Tag = Nothing
        Me.sepFile3.Text = "Encryption"
        Me.sepFile3.ToolTip = Nothing
        Me.sepFile3.ToolTipImage = Nothing
        Me.sepFile3.ToolTipTitle = Nothing
        '
        'butFileEncrypt
        '
        Me.butFileEncrypt.AltKey = Nothing
        Me.butFileEncrypt.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileEncrypt.Image = CType(resources.GetObject("butFileEncrypt.Image"), System.Drawing.Image)
        Me.butFileEncrypt.SmallImage = CType(resources.GetObject("butFileEncrypt.SmallImage"), System.Drawing.Image)
        Me.butFileEncrypt.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileEncrypt.Tag = Nothing
        Me.butFileEncrypt.Text = "Encrypt"
        Me.butFileEncrypt.ToolTip = Nothing
        Me.butFileEncrypt.ToolTipImage = Nothing
        Me.butFileEncrypt.ToolTipTitle = Nothing
        '
        'butFileDecrypt
        '
        Me.butFileDecrypt.AltKey = Nothing
        Me.butFileDecrypt.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileDecrypt.Image = CType(resources.GetObject("butFileDecrypt.Image"), System.Drawing.Image)
        Me.butFileDecrypt.SmallImage = CType(resources.GetObject("butFileDecrypt.SmallImage"), System.Drawing.Image)
        Me.butFileDecrypt.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileDecrypt.Tag = Nothing
        Me.butFileDecrypt.Text = "Decrypt"
        Me.butFileDecrypt.ToolTip = Nothing
        Me.butFileDecrypt.ToolTipImage = Nothing
        Me.butFileDecrypt.ToolTipTitle = Nothing
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(795, 603)
        Me.Controls.Add(Me.panelMain5)
        Me.Controls.Add(Me.panelInfos)
        Me.Controls.Add(Me.panelMenu2)
        Me.Controls.Add(Me.panelMenu)
        Me.Controls.Add(Me.cmdTray)
        Me.Controls.Add(Me.Ribbon)
        Me.Controls.Add(Me.panelMain3)
        Me.Controls.Add(Me.panelMain4)
        Me.Controls.Add(Me.panelInfos2)
        Me.Controls.Add(Me.panelMain2)
        Me.Controls.Add(Me.panelMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(787, 589)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Yet Another Process Monitor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.panelMain.ResumeLayout(False)
        Me.menuProc.ResumeLayout(False)
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        Me.panelMain2.ResumeLayout(False)
        Me.menuService.ResumeLayout(False)
        Me.panelMain4.ResumeLayout(False)
        Me.panelMain3.ResumeLayout(False)
        Me.menuCopyPctbig.ResumeLayout(False)
        Me.menuCopyPctSmall.ResumeLayout(False)
        Me.panelInfos.ResumeLayout(False)
        Me.panelInfos.PerformLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuTooltip.ResumeLayout(False)
        Me.panelInfos2.ResumeLayout(False)
        Me.panelInfos2.PerformLayout()
        Me.panelMenu2.ResumeLayout(False)
        Me.panelMenu2.PerformLayout()
        Me.panelMain5.ResumeLayout(False)
        Me.panelMain5.PerformLayout()
        Me.fileSplitContainer.Panel1.ResumeLayout(False)
        Me.fileSplitContainer.ResumeLayout(False)
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
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
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
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
    Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
    Friend WithEvents saveDial As System.Windows.Forms.SaveFileDialog
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
    Friend WithEvents WBHelp As System.Windows.Forms.WebBrowser
    Friend WithEvents menuService As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem20 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem21 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdCopyServiceToCp As System.Windows.Forms.Button
    Friend WithEvents openDial As System.Windows.Forms.OpenFileDialog
    Friend WithEvents timerJobs As System.Windows.Forms.Timer
    Friend WithEvents Ribbon As System.Windows.Forms.Ribbon
    Friend WithEvents ProcessTab As System.Windows.Forms.RibbonTab
    Friend WithEvents ServiceTab As System.Windows.Forms.RibbonTab
    Friend WithEvents JobsTab As System.Windows.Forms.RibbonTab
    Friend WithEvents HelpTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBProcessActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBProcessExecutable As System.Windows.Forms.RibbonPanel
    Friend WithEvents butStopProcess As System.Windows.Forms.RibbonButton
    Friend WithEvents butResumeProcess As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessOtherActions As System.Windows.Forms.RibbonButton
    Friend WithEvents RBProcessPriority As System.Windows.Forms.RibbonPanel
    Friend WithEvents butProcessFileProp As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessDirOpen As System.Windows.Forms.RibbonButton
    Friend WithEvents butKillProcess As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButton1 As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessPriority As System.Windows.Forms.RibbonButton
    Friend WithEvents butNewProcess As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessAffinity As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessLimitCPU As System.Windows.Forms.RibbonButton
    Friend WithEvents butIdle As System.Windows.Forms.RibbonButton
    Friend WithEvents butBelowNormal As System.Windows.Forms.RibbonButton
    Friend WithEvents butNormal As System.Windows.Forms.RibbonButton
    Friend WithEvents butAboveNormal As System.Windows.Forms.RibbonButton
    Friend WithEvents butHigh As System.Windows.Forms.RibbonButton
    Friend WithEvents butRealTime As System.Windows.Forms.RibbonButton
    Friend WithEvents RBServiceAction As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBServiceFile As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBServiceStartType As System.Windows.Forms.RibbonPanel
    Friend WithEvents butStopService As System.Windows.Forms.RibbonButton
    Friend WithEvents butStartService As System.Windows.Forms.RibbonButton
    Friend WithEvents butPauseService As System.Windows.Forms.RibbonButton
    Friend WithEvents butResumeService As System.Windows.Forms.RibbonButton
    Friend WithEvents butShutdownService As System.Windows.Forms.RibbonButton
    Friend WithEvents butServiceStartType As System.Windows.Forms.RibbonButton
    Friend WithEvents butAutomaticStart As System.Windows.Forms.RibbonButton
    Friend WithEvents butOnDemandStart As System.Windows.Forms.RibbonButton
    Friend WithEvents butDisabledStart As System.Windows.Forms.RibbonButton
    Friend WithEvents butServiceFileProp As System.Windows.Forms.RibbonButton
    Friend WithEvents butServiceOpenDir As System.Windows.Forms.RibbonButton
    Friend WithEvents RBAdd As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBJobsOpenSave As System.Windows.Forms.RibbonPanel
    Friend WithEvents butAddJob As System.Windows.Forms.RibbonButton
    Friend WithEvents butOpenJobList As System.Windows.Forms.RibbonButton
    Friend WithEvents butSaveJobList As System.Windows.Forms.RibbonButton
    Friend WithEvents RBProcessDisplay As System.Windows.Forms.RibbonPanel
    Friend WithEvents butProcessRerfresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBServiceDisplay As System.Windows.Forms.RibbonPanel
    Friend WithEvents butServiceRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBHelpAction As System.Windows.Forms.RibbonPanel
    Friend WithEvents butDonate As System.Windows.Forms.RibbonButton
    Friend WithEvents butAbout As System.Windows.Forms.RibbonButton
    Friend WithEvents RBHelpActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBHelpWeb As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWebite As System.Windows.Forms.RibbonButton
    Friend WithEvents butProjectPage As System.Windows.Forms.RibbonButton
    Friend WithEvents butDownload As System.Windows.Forms.RibbonButton
    Friend WithEvents ReportTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBSaveReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butSaveProcessReport As System.Windows.Forms.RibbonButton
    Friend WithEvents butSaveServiceReport As System.Windows.Forms.RibbonButton
    Friend WithEvents RBOthers As System.Windows.Forms.RibbonPanel
    Friend WithEvents butTakeFullPower As System.Windows.Forms.RibbonButton
    Friend WithEvents butOptions As System.Windows.Forms.RibbonButton
    Friend WithEvents cmdTray As System.Windows.Forms.Button
    Friend WithEvents RBDisplay As System.Windows.Forms.RibbonPanel
    Friend WithEvents butTopMost As System.Windows.Forms.RibbonButton
    Friend WithEvents RBProcessOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butProcessOnlineDesc As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonButtonList1 As System.Windows.Forms.RibbonButtonList
    Friend WithEvents butProcessGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents chkOnline As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents panelMenu2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblResCount2 As System.Windows.Forms.Label
    Friend WithEvents txtServiceSearch As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GetSecurityRiskOnlineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoogleSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GoogleSearchToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBServiceOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butServiceGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents FileTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBFileDelete As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBFileOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBFileOther As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMoveFileToTrash As System.Windows.Forms.RibbonButton
    Friend WithEvents butDeleteFile As System.Windows.Forms.RibbonButton
    Friend WithEvents butShreddFile As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileGoogleSearch As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileProperties As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileOpenDir As System.Windows.Forms.RibbonButton
    Friend WithEvents panelMain5 As System.Windows.Forms.Panel
    Friend WithEvents cmdFileClipboard As System.Windows.Forms.Button
    Friend WithEvents pctFileSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pctFileBig As System.Windows.Forms.PictureBox
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents FileDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butProcessFileDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents butServiceFileDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents FileDetailsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SearchTab As System.Windows.Forms.RibbonTab
    Friend WithEvents fileSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb3 As System.Windows.Forms.RichTextBox
    Friend WithEvents butFileShowFolderProperties As System.Windows.Forms.RibbonButton
    Friend WithEvents RBFileOthers As System.Windows.Forms.RibbonPanel
    Friend WithEvents butFileOthersActions As System.Windows.Forms.RibbonButton
    Friend WithEvents sepFile1 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents butFileRename As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileCopy As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileMove As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileOpen As System.Windows.Forms.RibbonButton
    Friend WithEvents sepFile2 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents butFileSeeStrings As System.Windows.Forms.RibbonButton
    Friend WithEvents sepFile3 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents butFileEncrypt As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileDecrypt As System.Windows.Forms.RibbonButton

End Class
