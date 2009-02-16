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
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("System")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("[System process]", New System.Windows.Forms.TreeNode() {TreeNode4})
        Dim ListViewGroup23 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup24 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Strings", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup25 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Strings", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup26 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup27 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup28 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Past jobs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Future jobs", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup9 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup11 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Handles", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup12 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Processes", 1, 1)
        Dim ListViewGroup13 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Threads", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup14 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup15 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Windows", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup16 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup17 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Modules", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup18 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup19 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Tasks", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup20 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.panelMain = New System.Windows.Forms.Panel
        Me.SplitContainerProcess = New System.Windows.Forms.SplitContainer
        Me.SplitContainerTvLv = New System.Windows.Forms.SplitContainer
        Me.tvProc = New System.Windows.Forms.TreeView
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
        Me.lvProcess = New YAPM.DoubleBufferedLV
        Me.c1 = New System.Windows.Forms.ColumnHeader
        Me.c2 = New System.Windows.Forms.ColumnHeader
        Me.c3 = New System.Windows.Forms.ColumnHeader
        Me.c4 = New System.Windows.Forms.ColumnHeader
        Me.c5 = New System.Windows.Forms.ColumnHeader
        Me.c7 = New System.Windows.Forms.ColumnHeader
        Me.c8 = New System.Windows.Forms.ColumnHeader
        Me.c9 = New System.Windows.Forms.ColumnHeader
        Me.c10 = New System.Windows.Forms.ColumnHeader
        Me.menuProc = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.KillToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.KillProcessTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
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
        Me.ToolStripMenuItem38 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowModulesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowThreadsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowHandlesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectedServicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FileDetailsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.GetSecurityRiskOnlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GoogleSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem37 = New System.Windows.Forms.ToolStripSeparator
        Me.ReadWriteMemoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MonitorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tabProcess = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtProcessUser = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtProcessId = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtProcessStarted = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtParentProcess = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.gpProcGeneralFile = New System.Windows.Forms.GroupBox
        Me.cmdShowFileDetails = New System.Windows.Forms.Button
        Me.cmdShowFileProperties = New System.Windows.Forms.Button
        Me.cmdOpenDirectory = New System.Windows.Forms.Button
        Me.txtProcessPath = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtImageVersion = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.pctSmallIcon = New System.Windows.Forms.PictureBox
        Me.menuCopyPctSmall = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.pctBigIcon = New System.Windows.Forms.PictureBox
        Me.menuCopyPctbig = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblUserObjectsCount = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblGDIcount = New System.Windows.Forms.Label
        Me.lbl789 = New System.Windows.Forms.Label
        Me.lblHandles = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lblProcOtherBytes = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.lblProcOther = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.lblProcWriteBytes = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.lblProcWrites = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.lblProcReadBytes = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.lblProcReads = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblQuotaNPP = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblQuotaPNPP = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblQuotaPP = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.lblQuotaPPP = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.lblPageFaults = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.lblPeakPageFileUsage = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.lblPageFileUsage = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.lblPeakWorkingSet = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.lblWorkingSet = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblTotalTime = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.lblUserTime = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblKernelTime = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblPriority = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.splitPerformances = New System.Windows.Forms.SplitContainer
        Me.graphCPU = New YAPM.Graph2
        Me.splitPerformance2 = New System.Windows.Forms.SplitContainer
        Me.graphMemory = New YAPM.Graph2
        Me.graphIO = New YAPM.Graph2
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.tabProcessToken = New System.Windows.Forms.TabControl
        Me.tabProcessTokenPagePrivileges = New System.Windows.Forms.TabPage
        Me.lvPrivileges = New YAPM.DoubleBufferedLV
        Me.ColumnHeader50 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader51 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader52 = New System.Windows.Forms.ColumnHeader
        Me.menuPrivileges = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem44 = New System.Windows.Forms.ToolStripMenuItem
        Me.DisableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.lvProcMem = New YAPM.DoubleBufferedLV
        Me.ColumnHeader53 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader54 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader55 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader56 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader21 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader22 = New System.Windows.Forms.ColumnHeader
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.SplitContainerInfoProcess = New System.Windows.Forms.SplitContainer
        Me.chkDisplayNAProcess = New System.Windows.Forms.CheckBox
        Me.chkHandles = New System.Windows.Forms.CheckBox
        Me.chkOnline = New System.Windows.Forms.CheckBox
        Me.chkModules = New System.Windows.Forms.CheckBox
        Me.cmdInfosToClipB = New System.Windows.Forms.Button
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.lvProcServices = New YAPM.DoubleBufferedLV
        Me.ColumnHeader23 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader47 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader48 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader20 = New System.Windows.Forms.ColumnHeader
        Me.menuProcServ = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem43 = New System.Windows.Forms.ToolStripMenuItem
        Me.imgProcessTab = New System.Windows.Forms.ImageList(Me.components)
        Me.tabNetwork = New System.Windows.Forms.TabPage
        Me.lvProcNetwork = New YAPM.DoubleBufferedLV
        Me.ColumnHeader49 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader57 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader58 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader59 = New System.Windows.Forms.ColumnHeader
        Me.TabPage8 = New System.Windows.Forms.TabPage
        Me.SplitContainerStrings = New System.Windows.Forms.SplitContainer
        Me.lvProcString = New YAPM.DoubleBufferedLV
        Me.ColumnHeader76 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader77 = New System.Windows.Forms.ColumnHeader
        Me.cmdProcSearchR = New System.Windows.Forms.Button
        Me.cmdProcSearchL = New System.Windows.Forms.Button
        Me.pgbString = New System.Windows.Forms.ProgressBar
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtSearchProcString = New System.Windows.Forms.TextBox
        Me.cmdProcStringSave = New System.Windows.Forms.Button
        Me.optProcStringMemory = New System.Windows.Forms.RadioButton
        Me.optProcStringImage = New System.Windows.Forms.RadioButton
        Me.TabPage9 = New System.Windows.Forms.TabPage
        Me.lvProcEnv = New YAPM.DoubleBufferedLV
        Me.ColumnHeader60 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader61 = New System.Windows.Forms.ColumnHeader
        Me.panelMenu = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblResCount = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.timerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.panelMain2 = New System.Windows.Forms.Panel
        Me.splitServices = New System.Windows.Forms.SplitContainer
        Me.lvServices = New YAPM.DoubleBufferedLV
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader19 = New System.Windows.Forms.ColumnHeader
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
        Me.ToolStripMenuItem42 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectedAssociatedProcessToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.splitServices2 = New System.Windows.Forms.SplitContainer
        Me.cmdCopyServiceToCp = New System.Windows.Forms.Button
        Me.lblServicePath = New System.Windows.Forms.TextBox
        Me.lblServiceName = New System.Windows.Forms.Label
        Me.splitServices3 = New System.Windows.Forms.SplitContainer
        Me.rtb2 = New System.Windows.Forms.RichTextBox
        Me.splitServices4 = New System.Windows.Forms.SplitContainer
        Me.tv2 = New System.Windows.Forms.TreeView
        Me.tv = New System.Windows.Forms.TreeView
        Me.panelMain4 = New System.Windows.Forms.Panel
        Me.WBHelp = New System.Windows.Forms.WebBrowser
        Me.panelMain3 = New System.Windows.Forms.Panel
        Me.lvJobs = New YAPM.DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.timerServices = New System.Windows.Forms.Timer(Me.components)
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.menuTooltip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem41 = New System.Windows.Forms.ToolStripSeparator
        Me.EnableProcessRefreshingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EnableServiceRefreshingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.saveDial = New System.Windows.Forms.SaveFileDialog
        Me.openDial = New System.Windows.Forms.OpenFileDialog
        Me.Ribbon = New System.Windows.Forms.Ribbon
        Me.TaskTab = New System.Windows.Forms.RibbonTab
        Me.RBTaskDisplay = New System.Windows.Forms.RibbonPanel
        Me.butTaskRefresh = New System.Windows.Forms.RibbonButton
        Me.RBTaskActions = New System.Windows.Forms.RibbonPanel
        Me.butTaskShow = New System.Windows.Forms.RibbonButton
        Me.butTaskEndTask = New System.Windows.Forms.RibbonButton
        Me.ProcessTab = New System.Windows.Forms.RibbonTab
        Me.RBProcessDisplay = New System.Windows.Forms.RibbonPanel
        Me.butProcessRerfresh = New System.Windows.Forms.RibbonButton
        Me.butProcessDisplayDetails = New System.Windows.Forms.RibbonButton
        Me.butProcessPermuteLvTv = New System.Windows.Forms.RibbonButton
        Me.RBProcessActions = New System.Windows.Forms.RibbonPanel
        Me.butNewProcess = New System.Windows.Forms.RibbonButton
        Me.butKillProcess = New System.Windows.Forms.RibbonButton
        Me.butStopProcess = New System.Windows.Forms.RibbonButton
        Me.butResumeProcess = New System.Windows.Forms.RibbonButton
        Me.butProcessOtherActions = New System.Windows.Forms.RibbonButton
        Me.butProcessAffinity = New System.Windows.Forms.RibbonButton
        Me.butProcessLimitCPU = New System.Windows.Forms.RibbonButton
        Me.butProcessShow = New System.Windows.Forms.RibbonButton
        Me.butProcessShowModules = New System.Windows.Forms.RibbonButton
        Me.butProcessThreads = New System.Windows.Forms.RibbonButton
        Me.butShowProcHandles = New System.Windows.Forms.RibbonButton
        Me.butProcessWindows = New System.Windows.Forms.RibbonButton
        Me.butProcessShowAll = New System.Windows.Forms.RibbonButton
        Me.butProcessMonitor = New System.Windows.Forms.RibbonButton
        Me.RBProcessPriority = New System.Windows.Forms.RibbonPanel
        Me.butProcessPriority = New System.Windows.Forms.RibbonButton
        Me.butIdle = New System.Windows.Forms.RibbonButton
        Me.butBelowNormal = New System.Windows.Forms.RibbonButton
        Me.butNormal = New System.Windows.Forms.RibbonButton
        Me.butAboveNormal = New System.Windows.Forms.RibbonButton
        Me.butHigh = New System.Windows.Forms.RibbonButton
        Me.butRealTime = New System.Windows.Forms.RibbonButton
        Me.RBProcessOnline = New System.Windows.Forms.RibbonPanel
        Me.butProcessOnlineDesc = New System.Windows.Forms.RibbonButton
        Me.butProcessGoogle = New System.Windows.Forms.RibbonButton
        Me.ModulesTab = New System.Windows.Forms.RibbonTab
        Me.RBModuleActions = New System.Windows.Forms.RibbonPanel
        Me.butModuleRefresh = New System.Windows.Forms.RibbonButton
        Me.butModuleUnload = New System.Windows.Forms.RibbonButton
        Me.RBModuleReport = New System.Windows.Forms.RibbonPanel
        Me.butModulesSaveReport = New System.Windows.Forms.RibbonButton
        Me.RBModuleOnline = New System.Windows.Forms.RibbonPanel
        Me.butModuleGoogle = New System.Windows.Forms.RibbonButton
        Me.ThreadTab = New System.Windows.Forms.RibbonTab
        Me.RBThreadsRefresh = New System.Windows.Forms.RibbonPanel
        Me.butThreadRefresh = New System.Windows.Forms.RibbonButton
        Me.RBThreadAction = New System.Windows.Forms.RibbonPanel
        Me.butThreadKill = New System.Windows.Forms.RibbonButton
        Me.butThreadStop = New System.Windows.Forms.RibbonButton
        Me.butThreadResume = New System.Windows.Forms.RibbonButton
        Me.RBThreadPriority = New System.Windows.Forms.RibbonPanel
        Me.butThreadPriority = New System.Windows.Forms.RibbonButton
        Me.butThreadPidle = New System.Windows.Forms.RibbonButton
        Me.butThreadPlowest = New System.Windows.Forms.RibbonButton
        Me.butThreadPbelow = New System.Windows.Forms.RibbonButton
        Me.butThreadPnormal = New System.Windows.Forms.RibbonButton
        Me.butThreadPabove = New System.Windows.Forms.RibbonButton
        Me.butThreadPhighest = New System.Windows.Forms.RibbonButton
        Me.butThreadPcritical = New System.Windows.Forms.RibbonButton
        Me.RBThreadReport = New System.Windows.Forms.RibbonPanel
        Me.butThreadSaveReport = New System.Windows.Forms.RibbonButton
        Me.HandlesTab = New System.Windows.Forms.RibbonTab
        Me.RBHandlesActions = New System.Windows.Forms.RibbonPanel
        Me.butHandleRefresh = New System.Windows.Forms.RibbonButton
        Me.butHandleClose = New System.Windows.Forms.RibbonButton
        Me.RBHandlesReport = New System.Windows.Forms.RibbonPanel
        Me.butHandlesSaveReport = New System.Windows.Forms.RibbonButton
        Me.WindowTab = New System.Windows.Forms.RibbonTab
        Me.RBWindowRefresh = New System.Windows.Forms.RibbonPanel
        Me.butWindowRefresh = New System.Windows.Forms.RibbonButton
        Me.RBWindowCapture = New System.Windows.Forms.RibbonPanel
        Me.butWindowFind = New System.Windows.Forms.RibbonButton
        Me.RBWindowActions = New System.Windows.Forms.RibbonPanel
        Me.butWindowVisibility = New System.Windows.Forms.RibbonButton
        Me.butWindowShow = New System.Windows.Forms.RibbonButton
        Me.butWindowHide = New System.Windows.Forms.RibbonButton
        Me.butWindowEnable = New System.Windows.Forms.RibbonButton
        Me.butWindowDisable = New System.Windows.Forms.RibbonButton
        Me.butWindowBringToFront = New System.Windows.Forms.RibbonButton
        Me.butWindowDoNotBringToFront = New System.Windows.Forms.RibbonButton
        Me.butWindowSetAsForeground = New System.Windows.Forms.RibbonButton
        Me.butWindowSetAsActive = New System.Windows.Forms.RibbonButton
        Me.butWindowMinimize = New System.Windows.Forms.RibbonButton
        Me.butWindowMaximize = New System.Windows.Forms.RibbonButton
        Me.butWindowFlash = New System.Windows.Forms.RibbonButton
        Me.butWindowStopFlashing = New System.Windows.Forms.RibbonButton
        Me.butWindowCaption = New System.Windows.Forms.RibbonButton
        Me.butWindowOpacity = New System.Windows.Forms.RibbonButton
        Me.butWindowClose = New System.Windows.Forms.RibbonButton
        Me.butWindowPositionSize = New System.Windows.Forms.RibbonButton
        Me.RBWindowReport = New System.Windows.Forms.RibbonPanel
        Me.butWindowSaveReport = New System.Windows.Forms.RibbonButton
        Me.MonitorTab = New System.Windows.Forms.RibbonTab
        Me.RBMonitor = New System.Windows.Forms.RibbonPanel
        Me.butMonitoringAdd = New System.Windows.Forms.RibbonButton
        Me.butMonitoringRemove = New System.Windows.Forms.RibbonButton
        Me.RBMonitoringControl = New System.Windows.Forms.RibbonPanel
        Me.butMonitorStart = New System.Windows.Forms.RibbonButton
        Me.butMonitorStop = New System.Windows.Forms.RibbonButton
        Me.butSaveMonitorReport = New System.Windows.Forms.RibbonPanel
        Me.butMonitorSaveReport = New System.Windows.Forms.RibbonButton
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
        Me.RBServiceReport = New System.Windows.Forms.RibbonPanel
        Me.butServiceReport = New System.Windows.Forms.RibbonButton
        Me.NetworkTab = New System.Windows.Forms.RibbonTab
        Me.RBNetworkRefresh = New System.Windows.Forms.RibbonPanel
        Me.butNetworkRefresh = New System.Windows.Forms.RibbonButton
        Me.FileTab = New System.Windows.Forms.RibbonTab
        Me.RBFileOpenFile = New System.Windows.Forms.RibbonPanel
        Me.butOpenFile = New System.Windows.Forms.RibbonButton
        Me.butFileRefresh = New System.Windows.Forms.RibbonButton
        Me.RBFileKillProcess = New System.Windows.Forms.RibbonPanel
        Me.butFileRelease = New System.Windows.Forms.RibbonButton
        Me.RBFileDelete = New System.Windows.Forms.RibbonPanel
        Me.butMoveFileToTrash = New System.Windows.Forms.RibbonButton
        Me.butDeleteFile = New System.Windows.Forms.RibbonButton
        Me.butShreddFile = New System.Windows.Forms.RibbonButton
        Me.RBFileOnline = New System.Windows.Forms.RibbonPanel
        Me.butFileGoogleSearch = New System.Windows.Forms.RibbonButton
        Me.RBFileOther = New System.Windows.Forms.RibbonPanel
        Me.butFileProperties = New System.Windows.Forms.RibbonButton
        Me.butFileOpenDir = New System.Windows.Forms.RibbonButton
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
        Me.SearchTab = New System.Windows.Forms.RibbonTab
        Me.RBSearchMain = New System.Windows.Forms.RibbonPanel
        Me.butSearchGo = New System.Windows.Forms.RibbonButton
        Me.butSearchSaveReport = New System.Windows.Forms.RibbonButton
        Me.txtSearchString = New System.Windows.Forms.RibbonTextBox
        Me.HelpTab = New System.Windows.Forms.RibbonTab
        Me.RBUpdate = New System.Windows.Forms.RibbonPanel
        Me.butUpdate = New System.Windows.Forms.RibbonButton
        Me.RBHelpAction = New System.Windows.Forms.RibbonPanel
        Me.butDonate = New System.Windows.Forms.RibbonButton
        Me.butAbout = New System.Windows.Forms.RibbonButton
        Me.RBHelpWeb = New System.Windows.Forms.RibbonPanel
        Me.butWebite = New System.Windows.Forms.RibbonButton
        Me.butProjectPage = New System.Windows.Forms.RibbonButton
        Me.butDownload = New System.Windows.Forms.RibbonButton
        Me.RBOptions = New System.Windows.Forms.RibbonPanel
        Me.butPreferences = New System.Windows.Forms.RibbonButton
        Me.butAlwaysDisplay = New System.Windows.Forms.RibbonButton
        Me.panelMenu2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblResCount2 = New System.Windows.Forms.Label
        Me.txtServiceSearch = New System.Windows.Forms.TextBox
        Me.panelMain5 = New System.Windows.Forms.Panel
        Me.fileSplitContainer = New System.Windows.Forms.SplitContainer
        Me.rtb3 = New System.Windows.Forms.RichTextBox
        Me.gpFileAttributes = New System.Windows.Forms.GroupBox
        Me.chkFileEncrypted = New System.Windows.Forms.CheckBox
        Me.chkFileContentNotIndexed = New System.Windows.Forms.CheckBox
        Me.chkFileNormal = New System.Windows.Forms.CheckBox
        Me.chkFileSystem = New System.Windows.Forms.CheckBox
        Me.chkFileReadOnly = New System.Windows.Forms.CheckBox
        Me.chkFileHidden = New System.Windows.Forms.CheckBox
        Me.chkFileCompressed = New System.Windows.Forms.CheckBox
        Me.chkFileArchive = New System.Windows.Forms.CheckBox
        Me.gpFileDates = New System.Windows.Forms.GroupBox
        Me.cmdSetFileDates = New System.Windows.Forms.Button
        Me.DTlastModification = New System.Windows.Forms.DateTimePicker
        Me.DTlastAccess = New System.Windows.Forms.DateTimePicker
        Me.DTcreation = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lstFileString = New System.Windows.Forms.ListBox
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.cmdFileClipboard = New System.Windows.Forms.Button
        Me.pctFileSmall = New System.Windows.Forms.PictureBox
        Me.mnuFileCopyPctSmall = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem
        Me.pctFileBig = New System.Windows.Forms.PictureBox
        Me.mnuFileCopyPctBig = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem
        Me.panelMain6 = New System.Windows.Forms.Panel
        Me.SplitContainerSearch = New System.Windows.Forms.SplitContainer
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblResultsCount = New System.Windows.Forms.Label
        Me.txtSearchResults = New System.Windows.Forms.TextBox
        Me.chkSearchWindows = New System.Windows.Forms.CheckBox
        Me.chkSearchHandles = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkSearchModules = New System.Windows.Forms.CheckBox
        Me.chkSearchServices = New System.Windows.Forms.CheckBox
        Me.chkSearchProcess = New System.Windows.Forms.CheckBox
        Me.chkSearchCase = New System.Windows.Forms.CheckBox
        Me.lvSearchResults = New YAPM.DoubleBufferedLV
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.menuSearch = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAssociatedProcessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imgSearch = New System.Windows.Forms.ImageList(Me.components)
        Me.panelMain7 = New System.Windows.Forms.Panel
        Me.SplitContainerHandles = New System.Windows.Forms.SplitContainer
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblHandlesCount = New System.Windows.Forms.Label
        Me.txtSearchHandle = New System.Windows.Forms.TextBox
        Me.lvHandles = New YAPM.DoubleBufferedLV
        Me.ColumnHeader24 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader25 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader26 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader27 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader28 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader15 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader16 = New System.Windows.Forms.ColumnHeader
        Me.menuHandles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem22 = New System.Windows.Forms.ToolStripMenuItem
        Me.panelMain8 = New System.Windows.Forms.Panel
        Me.splitMonitor = New System.Windows.Forms.SplitContainer
        Me.tvMonitor = New System.Windows.Forms.TreeView
        Me.imgMonitor = New System.Windows.Forms.ImageList(Me.components)
        Me.splitMonitor2 = New System.Windows.Forms.SplitContainer
        Me.txtMonitoringLog = New System.Windows.Forms.TextBox
        Me.splitMonitor3 = New System.Windows.Forms.SplitContainer
        Me.splitMonitor4 = New System.Windows.Forms.SplitContainer
        Me.graphMonitor = New YAPM.Graph
        Me.txtMonitorNumber = New System.Windows.Forms.TextBox
        Me.lblMonitorMaxNumber = New System.Windows.Forms.Label
        Me.chkMonitorRightAuto = New System.Windows.Forms.CheckBox
        Me.chkMonitorLeftAuto = New System.Windows.Forms.CheckBox
        Me.dtMonitorR = New System.Windows.Forms.DateTimePicker
        Me.dtMonitorL = New System.Windows.Forms.DateTimePicker
        Me.timerMonitoring = New System.Windows.Forms.Timer(Me.components)
        Me.panelMain9 = New System.Windows.Forms.Panel
        Me.splitThreads = New System.Windows.Forms.SplitContainer
        Me.SplitContainerThreads = New System.Windows.Forms.SplitContainer
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblThreadResults = New System.Windows.Forms.Label
        Me.txtSearchThread = New System.Windows.Forms.TextBox
        Me.lvThreads = New YAPM.DoubleBufferedLV
        Me.ColumnHeader32 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader33 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader34 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader35 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader36 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader37 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader38 = New System.Windows.Forms.ColumnHeader
        Me.menuThread = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem23 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem24 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem25 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem26 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem27 = New System.Windows.Forms.ToolStripMenuItem
        Me.LowestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem28 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem29 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem30 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem31 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem32 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem33 = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectedAssociatedProcessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.rtb4 = New System.Windows.Forms.RichTextBox
        Me.panelMain10 = New System.Windows.Forms.Panel
        Me.splitContainerWindows = New System.Windows.Forms.SplitContainer
        Me.SplitContainerWindows2 = New System.Windows.Forms.SplitContainer
        Me.chkAllWindows = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblWindowsCount = New System.Windows.Forms.Label
        Me.txtSearchWindow = New System.Windows.Forms.TextBox
        Me.lvWindows = New YAPM.DoubleBufferedLV
        Me.ColumnHeader30 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader31 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader39 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader40 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader41 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader42 = New System.Windows.Forms.ColumnHeader
        Me.menuWindow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem34 = New System.Windows.Forms.ToolStripMenuItem
        Me.imgWindows = New System.Windows.Forms.ImageList(Me.components)
        Me.rtb5 = New System.Windows.Forms.RichTextBox
        Me.FolderChooser = New System.Windows.Forms.FolderBrowserDialog
        Me.panelMain11 = New System.Windows.Forms.Panel
        Me.splitModule = New System.Windows.Forms.SplitContainer
        Me.SplitContainerModules = New System.Windows.Forms.SplitContainer
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblModulesCount = New System.Windows.Forms.Label
        Me.txtSearchModule = New System.Windows.Forms.TextBox
        Me.lvModules = New YAPM.DoubleBufferedLV
        Me.ColumnHeader29 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader43 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader44 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader45 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader46 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader18 = New System.Windows.Forms.ColumnHeader
        Me.menuModule = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem35 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowFileDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem36 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem39 = New System.Windows.Forms.ToolStripSeparator
        Me.GoogleSearchToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.rtb6 = New System.Windows.Forms.RichTextBox
        Me.mainMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowSystemInformatoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MinimizeToTrayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem40 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdTray = New System.Windows.Forms.Button
        Me.timerProcPerf = New System.Windows.Forms.Timer(Me.components)
        Me.menuTasks = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MaximizeWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MinimizeWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem46 = New System.Windows.Forms.ToolStripSeparator
        Me.EndTaskToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem47 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem45 = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectWindowInWindowsTabToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.panelMain13 = New System.Windows.Forms.Panel
        Me.SplitContainerTask = New System.Windows.Forms.SplitContainer
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblTaskCountResult = New System.Windows.Forms.Label
        Me.txtSearchTask = New System.Windows.Forms.TextBox
        Me.lvTask = New YAPM.DoubleBufferedLV
        Me.ColumnHeader62 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader63 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader64 = New System.Windows.Forms.ColumnHeader
        Me.imgTask = New System.Windows.Forms.ImageList(Me.components)
        Me.timerTask = New System.Windows.Forms.Timer(Me.components)
        Me.panelMain14 = New System.Windows.Forms.Panel
        Me.lvNetwork = New YAPM.DoubleBufferedLV
        Me.ColumnHeader66 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader67 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader68 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader69 = New System.Windows.Forms.ColumnHeader
        Me.menuNetwork = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem48 = New System.Windows.Forms.ToolStripMenuItem
        Me.timerTrayIcon = New System.Windows.Forms.Timer(Me.components)
        Me.menuProcMem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem49 = New System.Windows.Forms.ToolStripMenuItem
        Me.JumpToPEBAddressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.panelMain.SuspendLayout()
        Me.SplitContainerProcess.Panel1.SuspendLayout()
        Me.SplitContainerProcess.Panel2.SuspendLayout()
        Me.SplitContainerProcess.SuspendLayout()
        Me.SplitContainerTvLv.Panel1.SuspendLayout()
        Me.SplitContainerTvLv.Panel2.SuspendLayout()
        Me.SplitContainerTvLv.SuspendLayout()
        Me.menuProc.SuspendLayout()
        Me.tabProcess.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gpProcGeneralFile.SuspendLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuCopyPctSmall.SuspendLayout()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.menuCopyPctbig.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.splitPerformances.Panel1.SuspendLayout()
        Me.splitPerformances.Panel2.SuspendLayout()
        Me.splitPerformances.SuspendLayout()
        CType(Me.graphCPU, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitPerformance2.Panel1.SuspendLayout()
        Me.splitPerformance2.Panel2.SuspendLayout()
        Me.splitPerformance2.SuspendLayout()
        CType(Me.graphMemory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.graphIO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.tabProcessToken.SuspendLayout()
        Me.tabProcessTokenPagePrivileges.SuspendLayout()
        Me.menuPrivileges.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.SplitContainerInfoProcess.Panel1.SuspendLayout()
        Me.SplitContainerInfoProcess.Panel2.SuspendLayout()
        Me.SplitContainerInfoProcess.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.menuProcServ.SuspendLayout()
        Me.tabNetwork.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.SplitContainerStrings.Panel1.SuspendLayout()
        Me.SplitContainerStrings.Panel2.SuspendLayout()
        Me.SplitContainerStrings.SuspendLayout()
        Me.TabPage9.SuspendLayout()
        Me.panelMenu.SuspendLayout()
        Me.panelMain2.SuspendLayout()
        Me.splitServices.Panel1.SuspendLayout()
        Me.splitServices.Panel2.SuspendLayout()
        Me.splitServices.SuspendLayout()
        Me.menuService.SuspendLayout()
        Me.splitServices2.Panel1.SuspendLayout()
        Me.splitServices2.Panel2.SuspendLayout()
        Me.splitServices2.SuspendLayout()
        Me.splitServices3.Panel1.SuspendLayout()
        Me.splitServices3.Panel2.SuspendLayout()
        Me.splitServices3.SuspendLayout()
        Me.splitServices4.Panel1.SuspendLayout()
        Me.splitServices4.Panel2.SuspendLayout()
        Me.splitServices4.SuspendLayout()
        Me.panelMain4.SuspendLayout()
        Me.panelMain3.SuspendLayout()
        Me.menuTooltip.SuspendLayout()
        Me.panelMenu2.SuspendLayout()
        Me.panelMain5.SuspendLayout()
        Me.fileSplitContainer.Panel1.SuspendLayout()
        Me.fileSplitContainer.Panel2.SuspendLayout()
        Me.fileSplitContainer.SuspendLayout()
        Me.gpFileAttributes.SuspendLayout()
        Me.gpFileDates.SuspendLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuFileCopyPctSmall.SuspendLayout()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuFileCopyPctBig.SuspendLayout()
        Me.panelMain6.SuspendLayout()
        Me.SplitContainerSearch.Panel1.SuspendLayout()
        Me.SplitContainerSearch.Panel2.SuspendLayout()
        Me.SplitContainerSearch.SuspendLayout()
        Me.menuSearch.SuspendLayout()
        Me.panelMain7.SuspendLayout()
        Me.SplitContainerHandles.Panel1.SuspendLayout()
        Me.SplitContainerHandles.Panel2.SuspendLayout()
        Me.SplitContainerHandles.SuspendLayout()
        Me.menuHandles.SuspendLayout()
        Me.panelMain8.SuspendLayout()
        Me.splitMonitor.Panel1.SuspendLayout()
        Me.splitMonitor.Panel2.SuspendLayout()
        Me.splitMonitor.SuspendLayout()
        Me.splitMonitor2.Panel1.SuspendLayout()
        Me.splitMonitor2.Panel2.SuspendLayout()
        Me.splitMonitor2.SuspendLayout()
        Me.splitMonitor3.Panel1.SuspendLayout()
        Me.splitMonitor3.Panel2.SuspendLayout()
        Me.splitMonitor3.SuspendLayout()
        Me.splitMonitor4.Panel2.SuspendLayout()
        Me.splitMonitor4.SuspendLayout()
        CType(Me.graphMonitor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMain9.SuspendLayout()
        Me.splitThreads.Panel1.SuspendLayout()
        Me.splitThreads.Panel2.SuspendLayout()
        Me.splitThreads.SuspendLayout()
        Me.SplitContainerThreads.Panel1.SuspendLayout()
        Me.SplitContainerThreads.Panel2.SuspendLayout()
        Me.SplitContainerThreads.SuspendLayout()
        Me.menuThread.SuspendLayout()
        Me.panelMain10.SuspendLayout()
        Me.splitContainerWindows.Panel1.SuspendLayout()
        Me.splitContainerWindows.Panel2.SuspendLayout()
        Me.splitContainerWindows.SuspendLayout()
        Me.SplitContainerWindows2.Panel1.SuspendLayout()
        Me.SplitContainerWindows2.Panel2.SuspendLayout()
        Me.SplitContainerWindows2.SuspendLayout()
        Me.menuWindow.SuspendLayout()
        Me.panelMain11.SuspendLayout()
        Me.splitModule.Panel1.SuspendLayout()
        Me.splitModule.Panel2.SuspendLayout()
        Me.splitModule.SuspendLayout()
        Me.SplitContainerModules.Panel1.SuspendLayout()
        Me.SplitContainerModules.Panel2.SuspendLayout()
        Me.SplitContainerModules.SuspendLayout()
        Me.menuModule.SuspendLayout()
        Me.mainMenu.SuspendLayout()
        Me.menuTasks.SuspendLayout()
        Me.panelMain13.SuspendLayout()
        Me.SplitContainerTask.Panel1.SuspendLayout()
        Me.SplitContainerTask.Panel2.SuspendLayout()
        Me.SplitContainerTask.SuspendLayout()
        Me.panelMain14.SuspendLayout()
        Me.menuNetwork.SuspendLayout()
        Me.menuProcMem.SuspendLayout()
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
        Me.panelMain.Controls.Add(Me.SplitContainerProcess)
        Me.panelMain.Location = New System.Drawing.Point(218, 64)
        Me.panelMain.Name = "panelMain"
        Me.panelMain.Size = New System.Drawing.Size(560, 449)
        Me.panelMain.TabIndex = 2
        Me.panelMain.Visible = False
        '
        'SplitContainerProcess
        '
        Me.SplitContainerProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerProcess.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerProcess.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerProcess.Name = "SplitContainerProcess"
        Me.SplitContainerProcess.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerProcess.Panel1
        '
        Me.SplitContainerProcess.Panel1.Controls.Add(Me.SplitContainerTvLv)
        '
        'SplitContainerProcess.Panel2
        '
        Me.SplitContainerProcess.Panel2.Controls.Add(Me.tabProcess)
        Me.SplitContainerProcess.Size = New System.Drawing.Size(560, 449)
        Me.SplitContainerProcess.SplitterDistance = 224
        Me.SplitContainerProcess.TabIndex = 0
        '
        'SplitContainerTvLv
        '
        Me.SplitContainerTvLv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTvLv.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerTvLv.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerTvLv.Name = "SplitContainerTvLv"
        '
        'SplitContainerTvLv.Panel1
        '
        Me.SplitContainerTvLv.Panel1.Controls.Add(Me.tvProc)
        Me.SplitContainerTvLv.Panel1Collapsed = True
        '
        'SplitContainerTvLv.Panel2
        '
        Me.SplitContainerTvLv.Panel2.Controls.Add(Me.lvProcess)
        Me.SplitContainerTvLv.Size = New System.Drawing.Size(560, 224)
        Me.SplitContainerTvLv.SplitterDistance = 149
        Me.SplitContainerTvLv.TabIndex = 4
        '
        'tvProc
        '
        Me.tvProc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvProc.FullRowSelect = True
        Me.tvProc.ImageIndex = 0
        Me.tvProc.ImageList = Me.imgProcess
        Me.tvProc.Location = New System.Drawing.Point(0, 0)
        Me.tvProc.Name = "tvProc"
        TreeNode4.Name = "4"
        TreeNode4.Tag = "4"
        TreeNode4.Text = "System"
        TreeNode5.Name = "0"
        TreeNode5.Tag = "0"
        TreeNode5.Text = "[System process]"
        Me.tvProc.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode5})
        Me.tvProc.SelectedImageIndex = 0
        Me.tvProc.Size = New System.Drawing.Size(149, 100)
        Me.tvProc.TabIndex = 4
        '
        'imgProcess
        '
        Me.imgProcess.ImageStream = CType(resources.GetObject("imgProcess.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcess.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcess.Images.SetKeyName(0, "noIcon")
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c7, Me.c8, Me.c9, Me.c10})
        Me.lvProcess.ContextMenuStrip = Me.menuProc
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.FullRowSelect = True
        ListViewGroup23.Header = "Processes"
        ListViewGroup23.Name = "gpOther"
        ListViewGroup24.Header = "Search result"
        ListViewGroup24.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup23, ListViewGroup24})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.Location = New System.Drawing.Point(0, 0)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.Size = New System.Drawing.Size(560, 224)
        Me.lvProcess.SmallImageList = Me.imgProcess
        Me.lvProcess.TabIndex = 3
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
        Me.c3.Text = "UserName"
        Me.c3.Width = 100
        '
        'c4
        '
        Me.c4.DisplayIndex = 4
        Me.c4.Text = "TotalCpuTime"
        Me.c4.Width = 80
        '
        'c5
        '
        Me.c5.DisplayIndex = 5
        Me.c5.Text = "WorkingSet"
        Me.c5.Width = 80
        '
        'c7
        '
        Me.c7.DisplayIndex = 6
        Me.c7.Text = "Priority"
        Me.c7.Width = 70
        '
        'c8
        '
        Me.c8.DisplayIndex = 7
        Me.c8.Text = "Path"
        Me.c8.Width = 350
        '
        'c9
        '
        Me.c9.DisplayIndex = 8
        Me.c9.Text = "StartTime"
        Me.c9.Width = 250
        '
        'c10
        '
        Me.c10.DisplayIndex = 3
        Me.c10.Text = "CpuUsage"
        Me.c10.Width = 80
        '
        'menuProc
        '
        Me.menuProc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KillToolStripMenuItem, Me.KillProcessTreeToolStripMenuItem, Me.StopToolStripMenuItem, Me.ResumeToolStripMenuItem, Me.PriotiyToolStripMenuItem, Me.SetAffinityToolStripMenuItem, Me.ToolStripMenuItem38, Me.ShowModulesToolStripMenuItem, Me.ShowThreadsToolStripMenuItem, Me.ShowHandlesToolStripMenuItem, Me.ShowWindowsToolStripMenuItem, Me.ShowAllToolStripMenuItem, Me.SelectedServicesToolStripMenuItem, Me.ToolStripMenuItem8, Me.PropertiesToolStripMenuItem, Me.OpenFirectoryToolStripMenuItem, Me.FileDetailsToolStripMenuItem1, Me.ToolStripMenuItem1, Me.GetSecurityRiskOnlineToolStripMenuItem, Me.GoogleSearchToolStripMenuItem, Me.ToolStripMenuItem37, Me.ReadWriteMemoryToolStripMenuItem, Me.MonitorToolStripMenuItem})
        Me.menuProc.Name = "menuProc"
        Me.menuProc.Size = New System.Drawing.Size(194, 446)
        '
        'KillToolStripMenuItem
        '
        Me.KillToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.kill
        Me.KillToolStripMenuItem.Name = "KillToolStripMenuItem"
        Me.KillToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.KillToolStripMenuItem.Text = "Kill"
        '
        'KillProcessTreeToolStripMenuItem
        '
        Me.KillProcessTreeToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.kill
        Me.KillProcessTreeToolStripMenuItem.Name = "KillProcessTreeToolStripMenuItem"
        Me.KillProcessTreeToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.KillProcessTreeToolStripMenuItem.Text = "Kill process tree"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Image = CType(resources.GetObject("StopToolStripMenuItem.Image"), System.Drawing.Image)
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'ResumeToolStripMenuItem
        '
        Me.ResumeToolStripMenuItem.Image = CType(resources.GetObject("ResumeToolStripMenuItem.Image"), System.Drawing.Image)
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
        Me.IdleToolStripMenuItem.Image = CType(resources.GetObject("IdleToolStripMenuItem.Image"), System.Drawing.Image)
        Me.IdleToolStripMenuItem.Name = "IdleToolStripMenuItem"
        Me.IdleToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.IdleToolStripMenuItem.Text = "Idle"
        '
        'BelowNormalToolStripMenuItem
        '
        Me.BelowNormalToolStripMenuItem.Image = CType(resources.GetObject("BelowNormalToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BelowNormalToolStripMenuItem.Name = "BelowNormalToolStripMenuItem"
        Me.BelowNormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.BelowNormalToolStripMenuItem.Text = "Below Normal"
        '
        'NormalToolStripMenuItem
        '
        Me.NormalToolStripMenuItem.Image = CType(resources.GetObject("NormalToolStripMenuItem.Image"), System.Drawing.Image)
        Me.NormalToolStripMenuItem.Name = "NormalToolStripMenuItem"
        Me.NormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.NormalToolStripMenuItem.Text = "Normal"
        '
        'AboveNormalToolStripMenuItem
        '
        Me.AboveNormalToolStripMenuItem.Image = CType(resources.GetObject("AboveNormalToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AboveNormalToolStripMenuItem.Name = "AboveNormalToolStripMenuItem"
        Me.AboveNormalToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.AboveNormalToolStripMenuItem.Text = "Above Normal"
        '
        'HighToolStripMenuItem
        '
        Me.HighToolStripMenuItem.Image = CType(resources.GetObject("HighToolStripMenuItem.Image"), System.Drawing.Image)
        Me.HighToolStripMenuItem.Name = "HighToolStripMenuItem"
        Me.HighToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.HighToolStripMenuItem.Text = "High"
        '
        'RealTimeToolStripMenuItem
        '
        Me.RealTimeToolStripMenuItem.Image = CType(resources.GetObject("RealTimeToolStripMenuItem.Image"), System.Drawing.Image)
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
        'ToolStripMenuItem38
        '
        Me.ToolStripMenuItem38.Name = "ToolStripMenuItem38"
        Me.ToolStripMenuItem38.Size = New System.Drawing.Size(190, 6)
        '
        'ShowModulesToolStripMenuItem
        '
        Me.ShowModulesToolStripMenuItem.Image = CType(resources.GetObject("ShowModulesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowModulesToolStripMenuItem.Name = "ShowModulesToolStripMenuItem"
        Me.ShowModulesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ShowModulesToolStripMenuItem.Text = "Show modules"
        '
        'ShowThreadsToolStripMenuItem
        '
        Me.ShowThreadsToolStripMenuItem.Image = CType(resources.GetObject("ShowThreadsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowThreadsToolStripMenuItem.Name = "ShowThreadsToolStripMenuItem"
        Me.ShowThreadsToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ShowThreadsToolStripMenuItem.Text = "Show threads"
        '
        'ShowHandlesToolStripMenuItem
        '
        Me.ShowHandlesToolStripMenuItem.Image = CType(resources.GetObject("ShowHandlesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowHandlesToolStripMenuItem.Name = "ShowHandlesToolStripMenuItem"
        Me.ShowHandlesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ShowHandlesToolStripMenuItem.Text = "Show handles"
        '
        'ShowWindowsToolStripMenuItem
        '
        Me.ShowWindowsToolStripMenuItem.Image = CType(resources.GetObject("ShowWindowsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowWindowsToolStripMenuItem.Name = "ShowWindowsToolStripMenuItem"
        Me.ShowWindowsToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ShowWindowsToolStripMenuItem.Text = "Show windows"
        '
        'ShowAllToolStripMenuItem
        '
        Me.ShowAllToolStripMenuItem.Image = CType(resources.GetObject("ShowAllToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowAllToolStripMenuItem.Name = "ShowAllToolStripMenuItem"
        Me.ShowAllToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ShowAllToolStripMenuItem.Text = "Show all"
        '
        'SelectedServicesToolStripMenuItem
        '
        Me.SelectedServicesToolStripMenuItem.Image = CType(resources.GetObject("SelectedServicesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SelectedServicesToolStripMenuItem.Name = "SelectedServicesToolStripMenuItem"
        Me.SelectedServicesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SelectedServicesToolStripMenuItem.Text = "Selected services"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(190, 6)
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Image = CType(resources.GetObject("PropertiesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.PropertiesToolStripMenuItem.Text = "File properties"
        '
        'OpenFirectoryToolStripMenuItem
        '
        Me.OpenFirectoryToolStripMenuItem.Image = CType(resources.GetObject("OpenFirectoryToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenFirectoryToolStripMenuItem.Name = "OpenFirectoryToolStripMenuItem"
        Me.OpenFirectoryToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.OpenFirectoryToolStripMenuItem.Text = "Open directory"
        '
        'FileDetailsToolStripMenuItem1
        '
        Me.FileDetailsToolStripMenuItem1.Image = CType(resources.GetObject("FileDetailsToolStripMenuItem1.Image"), System.Drawing.Image)
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
        Me.GoogleSearchToolStripMenuItem.Image = CType(resources.GetObject("GoogleSearchToolStripMenuItem.Image"), System.Drawing.Image)
        Me.GoogleSearchToolStripMenuItem.Name = "GoogleSearchToolStripMenuItem"
        Me.GoogleSearchToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.GoogleSearchToolStripMenuItem.Text = "Google search"
        '
        'ToolStripMenuItem37
        '
        Me.ToolStripMenuItem37.Name = "ToolStripMenuItem37"
        Me.ToolStripMenuItem37.Size = New System.Drawing.Size(190, 6)
        '
        'ReadWriteMemoryToolStripMenuItem
        '
        Me.ReadWriteMemoryToolStripMenuItem.Name = "ReadWriteMemoryToolStripMenuItem"
        Me.ReadWriteMemoryToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.ReadWriteMemoryToolStripMenuItem.Text = "Read/Write memory"
        '
        'MonitorToolStripMenuItem
        '
        Me.MonitorToolStripMenuItem.Image = CType(resources.GetObject("MonitorToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MonitorToolStripMenuItem.Name = "MonitorToolStripMenuItem"
        Me.MonitorToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.MonitorToolStripMenuItem.Text = "Monitor"
        '
        'tabProcess
        '
        Me.tabProcess.Controls.Add(Me.TabPage1)
        Me.tabProcess.Controls.Add(Me.TabPage2)
        Me.tabProcess.Controls.Add(Me.TabPage3)
        Me.tabProcess.Controls.Add(Me.TabPage4)
        Me.tabProcess.Controls.Add(Me.TabPage5)
        Me.tabProcess.Controls.Add(Me.TabPage6)
        Me.tabProcess.Controls.Add(Me.TabPage7)
        Me.tabProcess.Controls.Add(Me.tabNetwork)
        Me.tabProcess.Controls.Add(Me.TabPage8)
        Me.tabProcess.Controls.Add(Me.TabPage9)
        Me.tabProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProcess.ImageList = Me.imgProcessTab
        Me.tabProcess.Location = New System.Drawing.Point(0, 0)
        Me.tabProcess.Name = "tabProcess"
        Me.tabProcess.SelectedIndex = 0
        Me.tabProcess.Size = New System.Drawing.Size(560, 221)
        Me.tabProcess.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.gpProcGeneralFile)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(552, 194)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtProcessUser)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtProcessId)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtProcessStarted)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.txtParentProcess)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Location = New System.Drawing.Point(388, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(376, 119)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Process"
        '
        'txtProcessUser
        '
        Me.txtProcessUser.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessUser.Location = New System.Drawing.Point(89, 90)
        Me.txtProcessUser.Name = "txtProcessUser"
        Me.txtProcessUser.ReadOnly = True
        Me.txtProcessUser.Size = New System.Drawing.Size(283, 20)
        Me.txtProcessUser.TabIndex = 22
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(29, 13)
        Me.Label17.TabIndex = 21
        Me.Label17.Text = "User"
        '
        'txtProcessId
        '
        Me.txtProcessId.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessId.Location = New System.Drawing.Point(89, 67)
        Me.txtProcessId.Name = "txtProcessId"
        Me.txtProcessId.ReadOnly = True
        Me.txtProcessId.Size = New System.Drawing.Size(282, 20)
        Me.txtProcessId.TabIndex = 20
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 70)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(22, 13)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "Pid"
        '
        'txtProcessStarted
        '
        Me.txtProcessStarted.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessStarted.Location = New System.Drawing.Point(90, 45)
        Me.txtProcessStarted.Name = "txtProcessStarted"
        Me.txtProcessStarted.ReadOnly = True
        Me.txtProcessStarted.Size = New System.Drawing.Size(281, 20)
        Me.txtProcessStarted.TabIndex = 18
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Started"
        '
        'txtParentProcess
        '
        Me.txtParentProcess.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtParentProcess.Location = New System.Drawing.Point(90, 22)
        Me.txtParentProcess.Name = "txtParentProcess"
        Me.txtParentProcess.ReadOnly = True
        Me.txtParentProcess.Size = New System.Drawing.Size(280, 20)
        Me.txtParentProcess.TabIndex = 16
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 25)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(78, 13)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Parent process"
        '
        'gpProcGeneralFile
        '
        Me.gpProcGeneralFile.Controls.Add(Me.cmdShowFileDetails)
        Me.gpProcGeneralFile.Controls.Add(Me.cmdShowFileProperties)
        Me.gpProcGeneralFile.Controls.Add(Me.cmdOpenDirectory)
        Me.gpProcGeneralFile.Controls.Add(Me.txtProcessPath)
        Me.gpProcGeneralFile.Controls.Add(Me.Label13)
        Me.gpProcGeneralFile.Controls.Add(Me.txtImageVersion)
        Me.gpProcGeneralFile.Controls.Add(Me.Label12)
        Me.gpProcGeneralFile.Controls.Add(Me.lblCopyright)
        Me.gpProcGeneralFile.Controls.Add(Me.lblDescription)
        Me.gpProcGeneralFile.Controls.Add(Me.pctSmallIcon)
        Me.gpProcGeneralFile.Controls.Add(Me.pctBigIcon)
        Me.gpProcGeneralFile.Location = New System.Drawing.Point(6, 6)
        Me.gpProcGeneralFile.Name = "gpProcGeneralFile"
        Me.gpProcGeneralFile.Size = New System.Drawing.Size(376, 117)
        Me.gpProcGeneralFile.TabIndex = 15
        Me.gpProcGeneralFile.TabStop = False
        Me.gpProcGeneralFile.Text = "Image file"
        '
        'cmdShowFileDetails
        '
        Me.cmdShowFileDetails.Image = CType(resources.GetObject("cmdShowFileDetails.Image"), System.Drawing.Image)
        Me.cmdShowFileDetails.Location = New System.Drawing.Point(292, 81)
        Me.cmdShowFileDetails.Name = "cmdShowFileDetails"
        Me.cmdShowFileDetails.Size = New System.Drawing.Size(26, 26)
        Me.cmdShowFileDetails.TabIndex = 21
        Me.cmdShowFileDetails.UseVisualStyleBackColor = True
        '
        'cmdShowFileProperties
        '
        Me.cmdShowFileProperties.Image = CType(resources.GetObject("cmdShowFileProperties.Image"), System.Drawing.Image)
        Me.cmdShowFileProperties.Location = New System.Drawing.Point(318, 81)
        Me.cmdShowFileProperties.Name = "cmdShowFileProperties"
        Me.cmdShowFileProperties.Size = New System.Drawing.Size(26, 26)
        Me.cmdShowFileProperties.TabIndex = 20
        Me.cmdShowFileProperties.UseVisualStyleBackColor = True
        '
        'cmdOpenDirectory
        '
        Me.cmdOpenDirectory.Image = CType(resources.GetObject("cmdOpenDirectory.Image"), System.Drawing.Image)
        Me.cmdOpenDirectory.Location = New System.Drawing.Point(344, 81)
        Me.cmdOpenDirectory.Name = "cmdOpenDirectory"
        Me.cmdOpenDirectory.Size = New System.Drawing.Size(26, 26)
        Me.cmdOpenDirectory.TabIndex = 19
        Me.cmdOpenDirectory.UseVisualStyleBackColor = True
        '
        'txtProcessPath
        '
        Me.txtProcessPath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessPath.Location = New System.Drawing.Point(84, 82)
        Me.txtProcessPath.Name = "txtProcessPath"
        Me.txtProcessPath.ReadOnly = True
        Me.txtProcessPath.Size = New System.Drawing.Size(202, 20)
        Me.txtProcessPath.TabIndex = 18
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 85)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 13)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "Image path"
        '
        'txtImageVersion
        '
        Me.txtImageVersion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtImageVersion.Location = New System.Drawing.Point(85, 59)
        Me.txtImageVersion.Name = "txtImageVersion"
        Me.txtImageVersion.ReadOnly = True
        Me.txtImageVersion.Size = New System.Drawing.Size(285, 20)
        Me.txtImageVersion.TabIndex = 16
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 62)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 13)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Image version"
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Location = New System.Drawing.Point(82, 38)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(69, 13)
        Me.lblCopyright.TabIndex = 14
        Me.lblCopyright.Text = "File copyright"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(82, 19)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(77, 13)
        Me.lblDescription.TabIndex = 13
        Me.lblDescription.Text = "File description"
        '
        'pctSmallIcon
        '
        Me.pctSmallIcon.ContextMenuStrip = Me.menuCopyPctSmall
        Me.pctSmallIcon.Location = New System.Drawing.Point(44, 35)
        Me.pctSmallIcon.Name = "pctSmallIcon"
        Me.pctSmallIcon.Size = New System.Drawing.Size(16, 16)
        Me.pctSmallIcon.TabIndex = 12
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
        Me.ToolStripMenuItem7.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItem7.Text = "Copy to clipboard"
        '
        'pctBigIcon
        '
        Me.pctBigIcon.ContextMenuStrip = Me.menuCopyPctbig
        Me.pctBigIcon.Location = New System.Drawing.Point(6, 19)
        Me.pctBigIcon.Name = "pctBigIcon"
        Me.pctBigIcon.Size = New System.Drawing.Size(32, 32)
        Me.pctBigIcon.TabIndex = 11
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
        Me.ToolStripMenuItem6.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItem6.Text = "Copy to clipboard"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(552, 194)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Statistics"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblUserObjectsCount)
        Me.GroupBox5.Controls.Add(Me.Label37)
        Me.GroupBox5.Controls.Add(Me.lblGDIcount)
        Me.GroupBox5.Controls.Add(Me.lbl789)
        Me.GroupBox5.Controls.Add(Me.lblHandles)
        Me.GroupBox5.Controls.Add(Me.Label53)
        Me.GroupBox5.Location = New System.Drawing.Point(416, 114)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(180, 75)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Other"
        '
        'lblUserObjectsCount
        '
        Me.lblUserObjectsCount.AutoSize = True
        Me.lblUserObjectsCount.Location = New System.Drawing.Point(102, 54)
        Me.lblUserObjectsCount.Name = "lblUserObjectsCount"
        Me.lblUserObjectsCount.Size = New System.Drawing.Size(19, 13)
        Me.lblUserObjectsCount.TabIndex = 5
        Me.lblUserObjectsCount.Text = "00"
        Me.lblUserObjectsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(8, 54)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(66, 13)
        Me.Label37.TabIndex = 4
        Me.Label37.Text = "User objects"
        '
        'lblGDIcount
        '
        Me.lblGDIcount.AutoSize = True
        Me.lblGDIcount.Location = New System.Drawing.Point(101, 35)
        Me.lblGDIcount.Name = "lblGDIcount"
        Me.lblGDIcount.Size = New System.Drawing.Size(19, 13)
        Me.lblGDIcount.TabIndex = 3
        Me.lblGDIcount.Text = "00"
        Me.lblGDIcount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl789
        '
        Me.lbl789.AutoSize = True
        Me.lbl789.Location = New System.Drawing.Point(7, 35)
        Me.lbl789.Name = "lbl789"
        Me.lbl789.Size = New System.Drawing.Size(63, 13)
        Me.lbl789.TabIndex = 2
        Me.lbl789.Text = "GDI objects"
        '
        'lblHandles
        '
        Me.lblHandles.AutoSize = True
        Me.lblHandles.Location = New System.Drawing.Point(100, 16)
        Me.lblHandles.Name = "lblHandles"
        Me.lblHandles.Size = New System.Drawing.Size(19, 13)
        Me.lblHandles.TabIndex = 1
        Me.lblHandles.Text = "00"
        Me.lblHandles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(6, 16)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(46, 13)
        Me.Label53.TabIndex = 0
        Me.Label53.Text = "Handles"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblProcOtherBytes)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.lblProcOther)
        Me.GroupBox4.Controls.Add(Me.Label30)
        Me.GroupBox4.Controls.Add(Me.lblProcWriteBytes)
        Me.GroupBox4.Controls.Add(Me.Label36)
        Me.GroupBox4.Controls.Add(Me.lblProcWrites)
        Me.GroupBox4.Controls.Add(Me.Label38)
        Me.GroupBox4.Controls.Add(Me.lblProcReadBytes)
        Me.GroupBox4.Controls.Add(Me.Label40)
        Me.GroupBox4.Controls.Add(Me.lblProcReads)
        Me.GroupBox4.Controls.Add(Me.Label42)
        Me.GroupBox4.Location = New System.Drawing.Point(234, 8)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(176, 181)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "I/O"
        '
        'lblProcOtherBytes
        '
        Me.lblProcOtherBytes.AutoSize = True
        Me.lblProcOtherBytes.Location = New System.Drawing.Point(99, 107)
        Me.lblProcOtherBytes.Name = "lblProcOtherBytes"
        Me.lblProcOtherBytes.Size = New System.Drawing.Size(19, 13)
        Me.lblProcOtherBytes.TabIndex = 31
        Me.lblProcOtherBytes.Text = "00"
        Me.lblProcOtherBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 107)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(61, 13)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "Other bytes"
        '
        'lblProcOther
        '
        Me.lblProcOther.AutoSize = True
        Me.lblProcOther.Location = New System.Drawing.Point(99, 89)
        Me.lblProcOther.Name = "lblProcOther"
        Me.lblProcOther.Size = New System.Drawing.Size(19, 13)
        Me.lblProcOther.TabIndex = 29
        Me.lblProcOther.Text = "00"
        Me.lblProcOther.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(6, 89)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(33, 13)
        Me.Label30.TabIndex = 28
        Me.Label30.Text = "Other"
        '
        'lblProcWriteBytes
        '
        Me.lblProcWriteBytes.AutoSize = True
        Me.lblProcWriteBytes.Location = New System.Drawing.Point(99, 70)
        Me.lblProcWriteBytes.Name = "lblProcWriteBytes"
        Me.lblProcWriteBytes.Size = New System.Drawing.Size(19, 13)
        Me.lblProcWriteBytes.TabIndex = 27
        Me.lblProcWriteBytes.Text = "00"
        Me.lblProcWriteBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(6, 70)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(60, 13)
        Me.Label36.TabIndex = 26
        Me.Label36.Text = "Write bytes"
        '
        'lblProcWrites
        '
        Me.lblProcWrites.AutoSize = True
        Me.lblProcWrites.Location = New System.Drawing.Point(99, 52)
        Me.lblProcWrites.Name = "lblProcWrites"
        Me.lblProcWrites.Size = New System.Drawing.Size(19, 13)
        Me.lblProcWrites.TabIndex = 25
        Me.lblProcWrites.Text = "00"
        Me.lblProcWrites.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(6, 52)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(37, 13)
        Me.Label38.TabIndex = 24
        Me.Label38.Text = "Writes"
        '
        'lblProcReadBytes
        '
        Me.lblProcReadBytes.AutoSize = True
        Me.lblProcReadBytes.Location = New System.Drawing.Point(99, 34)
        Me.lblProcReadBytes.Name = "lblProcReadBytes"
        Me.lblProcReadBytes.Size = New System.Drawing.Size(19, 13)
        Me.lblProcReadBytes.TabIndex = 23
        Me.lblProcReadBytes.Text = "00"
        Me.lblProcReadBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(6, 34)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(61, 13)
        Me.Label40.TabIndex = 22
        Me.Label40.Text = "Read bytes"
        '
        'lblProcReads
        '
        Me.lblProcReads.AutoSize = True
        Me.lblProcReads.Location = New System.Drawing.Point(99, 16)
        Me.lblProcReads.Name = "lblProcReads"
        Me.lblProcReads.Size = New System.Drawing.Size(19, 13)
        Me.lblProcReads.TabIndex = 21
        Me.lblProcReads.Text = "00"
        Me.lblProcReads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(6, 16)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(38, 13)
        Me.Label42.TabIndex = 20
        Me.Label42.Text = "Reads"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblQuotaNPP)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.lblQuotaPNPP)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.lblQuotaPP)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.lblQuotaPPP)
        Me.GroupBox3.Controls.Add(Me.Label32)
        Me.GroupBox3.Controls.Add(Me.lblPageFaults)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.lblPeakPageFileUsage)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.lblPageFileUsage)
        Me.GroupBox3.Controls.Add(Me.Label35)
        Me.GroupBox3.Controls.Add(Me.lblPeakWorkingSet)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.lblWorkingSet)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 8)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(222, 181)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Memory"
        '
        'lblQuotaNPP
        '
        Me.lblQuotaNPP.AutoSize = True
        Me.lblQuotaNPP.Location = New System.Drawing.Point(146, 145)
        Me.lblQuotaNPP.Name = "lblQuotaNPP"
        Me.lblQuotaNPP.Size = New System.Drawing.Size(19, 13)
        Me.lblQuotaNPP.TabIndex = 23
        Me.lblQuotaNPP.Text = "00"
        Me.lblQuotaNPP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(5, 145)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(108, 13)
        Me.Label21.TabIndex = 22
        Me.Label21.Text = "QuotaNonPagedPool"
        '
        'lblQuotaPNPP
        '
        Me.lblQuotaPNPP.AutoSize = True
        Me.lblQuotaPNPP.Location = New System.Drawing.Point(146, 127)
        Me.lblQuotaPNPP.Name = "lblQuotaPNPP"
        Me.lblQuotaPNPP.Size = New System.Drawing.Size(19, 13)
        Me.lblQuotaPNPP.TabIndex = 21
        Me.lblQuotaPNPP.Text = "00"
        Me.lblQuotaPNPP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(5, 127)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(133, 13)
        Me.Label26.TabIndex = 20
        Me.Label26.Text = "QuotaPeakNonPagedPool"
        '
        'lblQuotaPP
        '
        Me.lblQuotaPP.AutoSize = True
        Me.lblQuotaPP.Location = New System.Drawing.Point(146, 109)
        Me.lblQuotaPP.Name = "lblQuotaPP"
        Me.lblQuotaPP.Size = New System.Drawing.Size(19, 13)
        Me.lblQuotaPP.TabIndex = 19
        Me.lblQuotaPP.Text = "00"
        Me.lblQuotaPP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(5, 109)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(88, 13)
        Me.Label29.TabIndex = 18
        Me.Label29.Text = "QuotaPagedPool"
        '
        'lblQuotaPPP
        '
        Me.lblQuotaPPP.AutoSize = True
        Me.lblQuotaPPP.Location = New System.Drawing.Point(146, 91)
        Me.lblQuotaPPP.Name = "lblQuotaPPP"
        Me.lblQuotaPPP.Size = New System.Drawing.Size(19, 13)
        Me.lblQuotaPPP.TabIndex = 17
        Me.lblQuotaPPP.Text = "00"
        Me.lblQuotaPPP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(5, 91)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(113, 13)
        Me.Label32.TabIndex = 16
        Me.Label32.Text = "QuotaPeakPagedPool"
        '
        'lblPageFaults
        '
        Me.lblPageFaults.AutoSize = True
        Me.lblPageFaults.Location = New System.Drawing.Point(146, 163)
        Me.lblPageFaults.Name = "lblPageFaults"
        Me.lblPageFaults.Size = New System.Drawing.Size(19, 13)
        Me.lblPageFaults.TabIndex = 15
        Me.lblPageFaults.Text = "00"
        Me.lblPageFaults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(5, 163)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(60, 13)
        Me.Label31.TabIndex = 14
        Me.Label31.Text = "Page faults"
        '
        'lblPeakPageFileUsage
        '
        Me.lblPeakPageFileUsage.AutoSize = True
        Me.lblPeakPageFileUsage.Location = New System.Drawing.Point(146, 72)
        Me.lblPeakPageFileUsage.Name = "lblPeakPageFileUsage"
        Me.lblPeakPageFileUsage.Size = New System.Drawing.Size(19, 13)
        Me.lblPeakPageFileUsage.TabIndex = 13
        Me.lblPeakPageFileUsage.Text = "00"
        Me.lblPeakPageFileUsage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(5, 72)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(107, 13)
        Me.Label33.TabIndex = 12
        Me.Label33.Text = "Peak page file usage"
        '
        'lblPageFileUsage
        '
        Me.lblPageFileUsage.AutoSize = True
        Me.lblPageFileUsage.Location = New System.Drawing.Point(146, 54)
        Me.lblPageFileUsage.Name = "lblPageFileUsage"
        Me.lblPageFileUsage.Size = New System.Drawing.Size(19, 13)
        Me.lblPageFileUsage.TabIndex = 11
        Me.lblPageFileUsage.Text = "00"
        Me.lblPageFileUsage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(5, 54)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(77, 13)
        Me.Label35.TabIndex = 10
        Me.Label35.Text = "Pagefile usage"
        '
        'lblPeakWorkingSet
        '
        Me.lblPeakWorkingSet.AutoSize = True
        Me.lblPeakWorkingSet.Location = New System.Drawing.Point(146, 36)
        Me.lblPeakWorkingSet.Name = "lblPeakWorkingSet"
        Me.lblPeakWorkingSet.Size = New System.Drawing.Size(19, 13)
        Me.lblPeakWorkingSet.TabIndex = 5
        Me.lblPeakWorkingSet.Text = "00"
        Me.lblPeakWorkingSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(5, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 13)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "Peak working set"
        '
        'lblWorkingSet
        '
        Me.lblWorkingSet.AutoSize = True
        Me.lblWorkingSet.Location = New System.Drawing.Point(146, 18)
        Me.lblWorkingSet.Name = "lblWorkingSet"
        Me.lblWorkingSet.Size = New System.Drawing.Size(19, 13)
        Me.lblWorkingSet.TabIndex = 3
        Me.lblWorkingSet.Text = "00"
        Me.lblWorkingSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(5, 18)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Working set"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblTotalTime)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.lblUserTime)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.lblKernelTime)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.lblPriority)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Location = New System.Drawing.Point(416, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(180, 100)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "CPU"
        '
        'lblTotalTime
        '
        Me.lblTotalTime.AutoSize = True
        Me.lblTotalTime.Location = New System.Drawing.Point(100, 73)
        Me.lblTotalTime.Name = "lblTotalTime"
        Me.lblTotalTime.Size = New System.Drawing.Size(70, 13)
        Me.lblTotalTime.TabIndex = 7
        Me.lblTotalTime.Text = "00:00:00.158"
        Me.lblTotalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 73)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(53, 13)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "Total time"
        '
        'lblUserTime
        '
        Me.lblUserTime.AutoSize = True
        Me.lblUserTime.Location = New System.Drawing.Point(100, 54)
        Me.lblUserTime.Name = "lblUserTime"
        Me.lblUserTime.Size = New System.Drawing.Size(19, 13)
        Me.lblUserTime.TabIndex = 5
        Me.lblUserTime.Text = "00"
        Me.lblUserTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 54)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(51, 13)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "User time"
        '
        'lblKernelTime
        '
        Me.lblKernelTime.AutoSize = True
        Me.lblKernelTime.Location = New System.Drawing.Point(100, 36)
        Me.lblKernelTime.Name = "lblKernelTime"
        Me.lblKernelTime.Size = New System.Drawing.Size(19, 13)
        Me.lblKernelTime.TabIndex = 3
        Me.lblKernelTime.Text = "00"
        Me.lblKernelTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 36)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(59, 13)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Kernel time"
        '
        'lblPriority
        '
        Me.lblPriority.AutoSize = True
        Me.lblPriority.Location = New System.Drawing.Point(100, 16)
        Me.lblPriority.Name = "lblPriority"
        Me.lblPriority.Size = New System.Drawing.Size(19, 13)
        Me.lblPriority.TabIndex = 1
        Me.lblPriority.Text = "00"
        Me.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(38, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Priority"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.splitPerformances)
        Me.TabPage3.ImageIndex = 2
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(552, 194)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Performances"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'splitPerformances
        '
        Me.splitPerformances.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitPerformances.IsSplitterFixed = True
        Me.splitPerformances.Location = New System.Drawing.Point(3, 3)
        Me.splitPerformances.Name = "splitPerformances"
        Me.splitPerformances.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitPerformances.Panel1
        '
        Me.splitPerformances.Panel1.Controls.Add(Me.graphCPU)
        '
        'splitPerformances.Panel2
        '
        Me.splitPerformances.Panel2.Controls.Add(Me.splitPerformance2)
        Me.splitPerformances.Size = New System.Drawing.Size(546, 188)
        Me.splitPerformances.SplitterDistance = 86
        Me.splitPerformances.SplitterWidth = 1
        Me.splitPerformances.TabIndex = 3
        '
        'graphCPU
        '
        Me.graphCPU.BackColor = System.Drawing.Color.Black
        Me.graphCPU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphCPU.EnableGraph = True
        Me.graphCPU.Fixedheight = True
        Me.graphCPU.GridStep = 10
        Me.graphCPU.Location = New System.Drawing.Point(0, 0)
        Me.graphCPU.Name = "graphCPU"
        Me.graphCPU.Size = New System.Drawing.Size(546, 86)
        Me.graphCPU.TabIndex = 1
        Me.graphCPU.TabStop = False
        '
        'splitPerformance2
        '
        Me.splitPerformance2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitPerformance2.IsSplitterFixed = True
        Me.splitPerformance2.Location = New System.Drawing.Point(0, 0)
        Me.splitPerformance2.Name = "splitPerformance2"
        Me.splitPerformance2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitPerformance2.Panel1
        '
        Me.splitPerformance2.Panel1.Controls.Add(Me.graphMemory)
        '
        'splitPerformance2.Panel2
        '
        Me.splitPerformance2.Panel2.Controls.Add(Me.graphIO)
        Me.splitPerformance2.Size = New System.Drawing.Size(546, 101)
        Me.splitPerformance2.SplitterDistance = 44
        Me.splitPerformance2.SplitterWidth = 1
        Me.splitPerformance2.TabIndex = 0
        '
        'graphMemory
        '
        Me.graphMemory.BackColor = System.Drawing.Color.Black
        Me.graphMemory.Color = System.Drawing.Color.Red
        Me.graphMemory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphMemory.EnableGraph = True
        Me.graphMemory.Fixedheight = False
        Me.graphMemory.GridStep = 10
        Me.graphMemory.Location = New System.Drawing.Point(0, 0)
        Me.graphMemory.Name = "graphMemory"
        Me.graphMemory.Size = New System.Drawing.Size(546, 44)
        Me.graphMemory.TabIndex = 2
        Me.graphMemory.TabStop = False
        '
        'graphIO
        '
        Me.graphIO.BackColor = System.Drawing.Color.Black
        Me.graphIO.Color = System.Drawing.Color.LimeGreen
        Me.graphIO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphIO.EnableGraph = True
        Me.graphIO.Fixedheight = False
        Me.graphIO.GridStep = 10
        Me.graphIO.Location = New System.Drawing.Point(0, 0)
        Me.graphIO.Name = "graphIO"
        Me.graphIO.Size = New System.Drawing.Size(546, 56)
        Me.graphIO.TabIndex = 3
        Me.graphIO.TabStop = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.tabProcessToken)
        Me.TabPage4.ImageIndex = 3
        Me.TabPage4.Location = New System.Drawing.Point(4, 23)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(552, 194)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Token"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'tabProcessToken
        '
        Me.tabProcessToken.Controls.Add(Me.tabProcessTokenPagePrivileges)
        Me.tabProcessToken.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProcessToken.Location = New System.Drawing.Point(3, 3)
        Me.tabProcessToken.Name = "tabProcessToken"
        Me.tabProcessToken.SelectedIndex = 0
        Me.tabProcessToken.Size = New System.Drawing.Size(546, 188)
        Me.tabProcessToken.TabIndex = 0
        '
        'tabProcessTokenPagePrivileges
        '
        Me.tabProcessTokenPagePrivileges.Controls.Add(Me.lvPrivileges)
        Me.tabProcessTokenPagePrivileges.Location = New System.Drawing.Point(4, 22)
        Me.tabProcessTokenPagePrivileges.Name = "tabProcessTokenPagePrivileges"
        Me.tabProcessTokenPagePrivileges.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProcessTokenPagePrivileges.Size = New System.Drawing.Size(538, 162)
        Me.tabProcessTokenPagePrivileges.TabIndex = 0
        Me.tabProcessTokenPagePrivileges.Text = "Privileges"
        Me.tabProcessTokenPagePrivileges.UseVisualStyleBackColor = True
        '
        'lvPrivileges
        '
        Me.lvPrivileges.AllowColumnReorder = True
        Me.lvPrivileges.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader50, Me.ColumnHeader51, Me.ColumnHeader52})
        Me.lvPrivileges.ContextMenuStrip = Me.menuPrivileges
        Me.lvPrivileges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvPrivileges.FullRowSelect = True
        Me.lvPrivileges.HideSelection = False
        Me.lvPrivileges.Location = New System.Drawing.Point(3, 3)
        Me.lvPrivileges.Name = "lvPrivileges"
        Me.lvPrivileges.OverriddenDoubleBuffered = True
        Me.lvPrivileges.Size = New System.Drawing.Size(532, 156)
        Me.lvPrivileges.TabIndex = 4
        Me.lvPrivileges.UseCompatibleStateImageBehavior = False
        Me.lvPrivileges.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader50
        '
        Me.ColumnHeader50.Text = "Name"
        Me.ColumnHeader50.Width = 191
        '
        'ColumnHeader51
        '
        Me.ColumnHeader51.Text = "Status"
        Me.ColumnHeader51.Width = 100
        '
        'ColumnHeader52
        '
        Me.ColumnHeader52.Text = "Description"
        Me.ColumnHeader52.Width = 400
        '
        'menuPrivileges
        '
        Me.menuPrivileges.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem44, Me.DisableToolStripMenuItem, Me.RemoveToolStripMenuItem})
        Me.menuPrivileges.Name = "mainMenu"
        Me.menuPrivileges.Size = New System.Drawing.Size(118, 70)
        '
        'ToolStripMenuItem44
        '
        Me.ToolStripMenuItem44.Enabled = False
        Me.ToolStripMenuItem44.Image = Global.YAPM.My.Resources.Resources.ok
        Me.ToolStripMenuItem44.Name = "ToolStripMenuItem44"
        Me.ToolStripMenuItem44.Size = New System.Drawing.Size(117, 22)
        Me.ToolStripMenuItem44.Text = "Enable"
        '
        'DisableToolStripMenuItem
        '
        Me.DisableToolStripMenuItem.Enabled = False
        Me.DisableToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.close
        Me.DisableToolStripMenuItem.Name = "DisableToolStripMenuItem"
        Me.DisableToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.DisableToolStripMenuItem.Text = "Disable"
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Enabled = False
        Me.RemoveToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.kill
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.lvProcMem)
        Me.TabPage5.ImageIndex = 4
        Me.TabPage5.Location = New System.Drawing.Point(4, 23)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(552, 194)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Memory"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'lvProcMem
        '
        Me.lvProcMem.AllowColumnReorder = True
        Me.lvProcMem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader53, Me.ColumnHeader54, Me.ColumnHeader55, Me.ColumnHeader56, Me.ColumnHeader21, Me.ColumnHeader22})
        Me.lvProcMem.ContextMenuStrip = Me.menuProcMem
        Me.lvProcMem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcMem.FullRowSelect = True
        Me.lvProcMem.HideSelection = False
        Me.lvProcMem.Location = New System.Drawing.Point(3, 3)
        Me.lvProcMem.Name = "lvProcMem"
        Me.lvProcMem.OverriddenDoubleBuffered = True
        Me.lvProcMem.Size = New System.Drawing.Size(546, 188)
        Me.lvProcMem.TabIndex = 5
        Me.lvProcMem.UseCompatibleStateImageBehavior = False
        Me.lvProcMem.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader53
        '
        Me.ColumnHeader53.Text = "Name"
        Me.ColumnHeader53.Width = 198
        '
        'ColumnHeader54
        '
        Me.ColumnHeader54.Text = "Address"
        Me.ColumnHeader54.Width = 100
        '
        'ColumnHeader55
        '
        Me.ColumnHeader55.Text = "Size"
        Me.ColumnHeader55.Width = 100
        '
        'ColumnHeader56
        '
        Me.ColumnHeader56.Text = "Protection"
        Me.ColumnHeader56.Width = 150
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "State"
        Me.ColumnHeader21.Width = 100
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Type"
        Me.ColumnHeader22.Width = 100
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.SplitContainerInfoProcess)
        Me.TabPage6.ImageIndex = 5
        Me.TabPage6.Location = New System.Drawing.Point(4, 23)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(552, 194)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Informations"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'SplitContainerInfoProcess
        '
        Me.SplitContainerInfoProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerInfoProcess.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerInfoProcess.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainerInfoProcess.Name = "SplitContainerInfoProcess"
        Me.SplitContainerInfoProcess.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerInfoProcess.Panel1
        '
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.chkDisplayNAProcess)
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.chkHandles)
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.chkOnline)
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.chkModules)
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.cmdInfosToClipB)
        '
        'SplitContainerInfoProcess.Panel2
        '
        Me.SplitContainerInfoProcess.Panel2.Controls.Add(Me.rtb)
        Me.SplitContainerInfoProcess.Size = New System.Drawing.Size(546, 188)
        Me.SplitContainerInfoProcess.SplitterDistance = 25
        Me.SplitContainerInfoProcess.TabIndex = 0
        '
        'chkDisplayNAProcess
        '
        Me.chkDisplayNAProcess.AutoSize = True
        Me.chkDisplayNAProcess.Checked = True
        Me.chkDisplayNAProcess.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDisplayNAProcess.Location = New System.Drawing.Point(420, 6)
        Me.chkDisplayNAProcess.Name = "chkDisplayNAProcess"
        Me.chkDisplayNAProcess.Size = New System.Drawing.Size(124, 17)
        Me.chkDisplayNAProcess.TabIndex = 18
        Me.chkDisplayNAProcess.Text = "Display all processes"
        Me.chkDisplayNAProcess.UseVisualStyleBackColor = True
        '
        'chkHandles
        '
        Me.chkHandles.AutoSize = True
        Me.chkHandles.Location = New System.Drawing.Point(350, 6)
        Me.chkHandles.Name = "chkHandles"
        Me.chkHandles.Size = New System.Drawing.Size(65, 17)
        Me.chkHandles.TabIndex = 17
        Me.chkHandles.Text = "Handles"
        Me.chkHandles.UseVisualStyleBackColor = True
        '
        'chkOnline
        '
        Me.chkOnline.AutoSize = True
        Me.chkOnline.Location = New System.Drawing.Point(262, 6)
        Me.chkOnline.Name = "chkOnline"
        Me.chkOnline.Size = New System.Drawing.Size(81, 17)
        Me.chkOnline.TabIndex = 16
        Me.chkOnline.Text = "Online infos"
        Me.chkOnline.UseVisualStyleBackColor = True
        '
        'chkModules
        '
        Me.chkModules.AutoSize = True
        Me.chkModules.Location = New System.Drawing.Point(108, 6)
        Me.chkModules.Name = "chkModules"
        Me.chkModules.Size = New System.Drawing.Size(145, 17)
        Me.chkModules.TabIndex = 15
        Me.chkModules.Text = "Retrive modules/trhreads"
        Me.chkModules.UseVisualStyleBackColor = True
        '
        'cmdInfosToClipB
        '
        Me.cmdInfosToClipB.Enabled = False
        Me.cmdInfosToClipB.Location = New System.Drawing.Point(3, 1)
        Me.cmdInfosToClipB.Name = "cmdInfosToClipB"
        Me.cmdInfosToClipB.Size = New System.Drawing.Size(99, 21)
        Me.cmdInfosToClipB.TabIndex = 14
        Me.cmdInfosToClipB.Text = "Copy to clipboard"
        Me.cmdInfosToClipB.UseVisualStyleBackColor = True
        '
        'rtb
        '
        Me.rtb.AutoWordSelection = True
        Me.rtb.BackColor = System.Drawing.Color.White
        Me.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb.HideSelection = False
        Me.rtb.Location = New System.Drawing.Point(0, 0)
        Me.rtb.Name = "rtb"
        Me.rtb.ReadOnly = True
        Me.rtb.Size = New System.Drawing.Size(546, 159)
        Me.rtb.TabIndex = 14
        Me.rtb.Text = ""
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.lvProcServices)
        Me.TabPage7.ImageIndex = 7
        Me.TabPage7.Location = New System.Drawing.Point(4, 23)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(552, 194)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Services"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'lvProcServices
        '
        Me.lvProcServices.AllowColumnReorder = True
        Me.lvProcServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader23, Me.ColumnHeader47, Me.ColumnHeader48, Me.ColumnHeader20})
        Me.lvProcServices.ContextMenuStrip = Me.menuProcServ
        Me.lvProcServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcServices.FullRowSelect = True
        Me.lvProcServices.HideSelection = False
        Me.lvProcServices.Location = New System.Drawing.Point(3, 3)
        Me.lvProcServices.Name = "lvProcServices"
        Me.lvProcServices.OverriddenDoubleBuffered = True
        Me.lvProcServices.Size = New System.Drawing.Size(546, 188)
        Me.lvProcServices.SmallImageList = Me.imgProcessTab
        Me.lvProcServices.TabIndex = 5
        Me.lvProcServices.UseCompatibleStateImageBehavior = False
        Me.lvProcServices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "ServiceName"
        Me.ColumnHeader23.Width = 140
        '
        'ColumnHeader47
        '
        Me.ColumnHeader47.Text = "State"
        Me.ColumnHeader47.Width = 90
        '
        'ColumnHeader48
        '
        Me.ColumnHeader48.Text = "CommonName"
        Me.ColumnHeader48.Width = 220
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "Path"
        Me.ColumnHeader20.Width = 330
        '
        'menuProcServ
        '
        Me.menuProcServ.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem43})
        Me.menuProcServ.Name = "menuProc"
        Me.menuProcServ.Size = New System.Drawing.Size(145, 26)
        '
        'ToolStripMenuItem43
        '
        Me.ToolStripMenuItem43.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem43.Name = "ToolStripMenuItem43"
        Me.ToolStripMenuItem43.Size = New System.Drawing.Size(144, 22)
        Me.ToolStripMenuItem43.Text = "Select service"
        '
        'imgProcessTab
        '
        Me.imgProcessTab.ImageStream = CType(resources.GetObject("imgProcessTab.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcessTab.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcessTab.Images.SetKeyName(0, "exe")
        Me.imgProcessTab.Images.SetKeyName(1, "stats")
        Me.imgProcessTab.Images.SetKeyName(2, "perf")
        Me.imgProcessTab.Images.SetKeyName(3, "locked")
        Me.imgProcessTab.Images.SetKeyName(4, "memory")
        Me.imgProcessTab.Images.SetKeyName(5, "text.gif")
        Me.imgProcessTab.Images.SetKeyName(6, "exe2")
        Me.imgProcessTab.Images.SetKeyName(7, "service")
        Me.imgProcessTab.Images.SetKeyName(8, "network")
        Me.imgProcessTab.Images.SetKeyName(9, "font")
        Me.imgProcessTab.Images.SetKeyName(10, "environment")
        '
        'tabNetwork
        '
        Me.tabNetwork.Controls.Add(Me.lvProcNetwork)
        Me.tabNetwork.ImageIndex = 8
        Me.tabNetwork.Location = New System.Drawing.Point(4, 23)
        Me.tabNetwork.Name = "tabNetwork"
        Me.tabNetwork.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNetwork.Size = New System.Drawing.Size(552, 194)
        Me.tabNetwork.TabIndex = 7
        Me.tabNetwork.Text = "Network"
        Me.tabNetwork.UseVisualStyleBackColor = True
        '
        'lvProcNetwork
        '
        Me.lvProcNetwork.AllowColumnReorder = True
        Me.lvProcNetwork.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader49, Me.ColumnHeader57, Me.ColumnHeader58, Me.ColumnHeader59})
        Me.lvProcNetwork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcNetwork.FullRowSelect = True
        Me.lvProcNetwork.HideSelection = False
        Me.lvProcNetwork.Location = New System.Drawing.Point(3, 3)
        Me.lvProcNetwork.Name = "lvProcNetwork"
        Me.lvProcNetwork.OverriddenDoubleBuffered = True
        Me.lvProcNetwork.Size = New System.Drawing.Size(546, 188)
        Me.lvProcNetwork.TabIndex = 5
        Me.lvProcNetwork.UseCompatibleStateImageBehavior = False
        Me.lvProcNetwork.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader49
        '
        Me.ColumnHeader49.Text = "Local"
        Me.ColumnHeader49.Width = 210
        '
        'ColumnHeader57
        '
        Me.ColumnHeader57.Text = "Port"
        Me.ColumnHeader57.Width = 100
        '
        'ColumnHeader58
        '
        Me.ColumnHeader58.Text = "Protocol"
        Me.ColumnHeader58.Width = 100
        '
        'ColumnHeader59
        '
        Me.ColumnHeader59.Text = "State"
        Me.ColumnHeader59.Width = 150
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.SplitContainerStrings)
        Me.TabPage8.ImageKey = "font"
        Me.TabPage8.Location = New System.Drawing.Point(4, 23)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(552, 194)
        Me.TabPage8.TabIndex = 8
        Me.TabPage8.Text = "Strings"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'SplitContainerStrings
        '
        Me.SplitContainerStrings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerStrings.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerStrings.Name = "SplitContainerStrings"
        Me.SplitContainerStrings.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerStrings.Panel1
        '
        Me.SplitContainerStrings.Panel1.Controls.Add(Me.lvProcString)
        '
        'SplitContainerStrings.Panel2
        '
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.cmdProcSearchR)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.cmdProcSearchL)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.pgbString)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.Label28)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.txtSearchProcString)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.cmdProcStringSave)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.optProcStringMemory)
        Me.SplitContainerStrings.Panel2.Controls.Add(Me.optProcStringImage)
        Me.SplitContainerStrings.Size = New System.Drawing.Size(552, 194)
        Me.SplitContainerStrings.SplitterDistance = 165
        Me.SplitContainerStrings.TabIndex = 0
        '
        'lvProcString
        '
        Me.lvProcString.AllowColumnReorder = True
        Me.lvProcString.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader76, Me.ColumnHeader77})
        Me.lvProcString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcString.FullRowSelect = True
        ListViewGroup1.Header = "Strings"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvProcString.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvProcString.HideSelection = False
        Me.lvProcString.Location = New System.Drawing.Point(0, 0)
        Me.lvProcString.MultiSelect = False
        Me.lvProcString.Name = "lvProcString"
        Me.lvProcString.OverriddenDoubleBuffered = True
        Me.lvProcString.Size = New System.Drawing.Size(552, 165)
        Me.lvProcString.TabIndex = 4
        Me.lvProcString.UseCompatibleStateImageBehavior = False
        Me.lvProcString.View = System.Windows.Forms.View.Details
        Me.lvProcString.VirtualMode = True
        '
        'ColumnHeader76
        '
        Me.ColumnHeader76.Text = "Position"
        Me.ColumnHeader76.Width = 110
        '
        'ColumnHeader77
        '
        Me.ColumnHeader77.Text = "String"
        Me.ColumnHeader77.Width = 508
        '
        'cmdProcSearchR
        '
        Me.cmdProcSearchR.Location = New System.Drawing.Point(499, 1)
        Me.cmdProcSearchR.Name = "cmdProcSearchR"
        Me.cmdProcSearchR.Size = New System.Drawing.Size(17, 23)
        Me.cmdProcSearchR.TabIndex = 19
        Me.cmdProcSearchR.Text = ">"
        Me.cmdProcSearchR.UseVisualStyleBackColor = True
        '
        'cmdProcSearchL
        '
        Me.cmdProcSearchL.Location = New System.Drawing.Point(476, 1)
        Me.cmdProcSearchL.Name = "cmdProcSearchL"
        Me.cmdProcSearchL.Size = New System.Drawing.Size(17, 23)
        Me.cmdProcSearchL.TabIndex = 18
        Me.cmdProcSearchL.Text = "<"
        Me.cmdProcSearchL.UseVisualStyleBackColor = True
        '
        'pgbString
        '
        Me.pgbString.Location = New System.Drawing.Point(230, 1)
        Me.pgbString.Name = "pgbString"
        Me.pgbString.Size = New System.Drawing.Size(120, 23)
        Me.pgbString.TabIndex = 17
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(365, 6)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(41, 13)
        Me.Label28.TabIndex = 16
        Me.Label28.Text = "Search"
        '
        'txtSearchProcString
        '
        Me.txtSearchProcString.Location = New System.Drawing.Point(410, 2)
        Me.txtSearchProcString.Name = "txtSearchProcString"
        Me.txtSearchProcString.Size = New System.Drawing.Size(60, 20)
        Me.txtSearchProcString.TabIndex = 14
        '
        'cmdProcStringSave
        '
        Me.cmdProcStringSave.Location = New System.Drawing.Point(140, 1)
        Me.cmdProcStringSave.Name = "cmdProcStringSave"
        Me.cmdProcStringSave.Size = New System.Drawing.Size(70, 23)
        Me.cmdProcStringSave.TabIndex = 2
        Me.cmdProcStringSave.Text = "Save..."
        Me.cmdProcStringSave.UseVisualStyleBackColor = True
        '
        'optProcStringMemory
        '
        Me.optProcStringMemory.AutoSize = True
        Me.optProcStringMemory.Location = New System.Drawing.Point(63, 4)
        Me.optProcStringMemory.Name = "optProcStringMemory"
        Me.optProcStringMemory.Size = New System.Drawing.Size(62, 17)
        Me.optProcStringMemory.TabIndex = 1
        Me.optProcStringMemory.Text = "Memory"
        Me.optProcStringMemory.UseVisualStyleBackColor = True
        '
        'optProcStringImage
        '
        Me.optProcStringImage.AutoSize = True
        Me.optProcStringImage.Checked = True
        Me.optProcStringImage.Location = New System.Drawing.Point(3, 4)
        Me.optProcStringImage.Name = "optProcStringImage"
        Me.optProcStringImage.Size = New System.Drawing.Size(54, 17)
        Me.optProcStringImage.TabIndex = 0
        Me.optProcStringImage.TabStop = True
        Me.optProcStringImage.Text = "Image"
        Me.optProcStringImage.UseVisualStyleBackColor = True
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.lvProcEnv)
        Me.TabPage9.ImageIndex = 10
        Me.TabPage9.Location = New System.Drawing.Point(4, 23)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Size = New System.Drawing.Size(552, 194)
        Me.TabPage9.TabIndex = 9
        Me.TabPage9.Text = "Environment"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'lvProcEnv
        '
        Me.lvProcEnv.AllowColumnReorder = True
        Me.lvProcEnv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader60, Me.ColumnHeader61})
        Me.lvProcEnv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcEnv.FullRowSelect = True
        ListViewGroup25.Header = "Strings"
        ListViewGroup25.Name = "gpOther"
        ListViewGroup26.Header = "Search result"
        ListViewGroup26.Name = "gpSearch"
        Me.lvProcEnv.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup25, ListViewGroup26})
        Me.lvProcEnv.HideSelection = False
        Me.lvProcEnv.Location = New System.Drawing.Point(0, 0)
        Me.lvProcEnv.Name = "lvProcEnv"
        Me.lvProcEnv.OverriddenDoubleBuffered = True
        Me.lvProcEnv.Size = New System.Drawing.Size(552, 194)
        Me.lvProcEnv.TabIndex = 5
        Me.lvProcEnv.UseCompatibleStateImageBehavior = False
        Me.lvProcEnv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader60
        '
        Me.ColumnHeader60.Text = "Variable"
        Me.ColumnHeader60.Width = 180
        '
        'ColumnHeader61
        '
        Me.ColumnHeader61.Text = "Value"
        Me.ColumnHeader61.Width = 508
        '
        'panelMenu
        '
        Me.panelMenu.Controls.Add(Me.Label3)
        Me.panelMenu.Controls.Add(Me.lblResCount)
        Me.panelMenu.Controls.Add(Me.txtSearch)
        Me.panelMenu.Location = New System.Drawing.Point(12, 121)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(766, 28)
        Me.panelMenu.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Search process"
        '
        'lblResCount
        '
        Me.lblResCount.AutoSize = True
        Me.lblResCount.Location = New System.Drawing.Point(394, 7)
        Me.lblResCount.Name = "lblResCount"
        Me.lblResCount.Size = New System.Drawing.Size(52, 13)
        Me.lblResCount.TabIndex = 2
        Me.lblResCount.Text = "0 result(s)"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(90, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(298, 20)
        Me.txtSearch.TabIndex = 1
        '
        'timerProcess
        '
        Me.timerProcess.Interval = 1000
        '
        'panelMain2
        '
        Me.panelMain2.Controls.Add(Me.splitServices)
        Me.panelMain2.Location = New System.Drawing.Point(40, 174)
        Me.panelMain2.Name = "panelMain2"
        Me.panelMain2.Size = New System.Drawing.Size(642, 315)
        Me.panelMain2.TabIndex = 15
        Me.panelMain2.Visible = False
        '
        'splitServices
        '
        Me.splitServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices.Location = New System.Drawing.Point(0, 0)
        Me.splitServices.Name = "splitServices"
        Me.splitServices.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitServices.Panel1
        '
        Me.splitServices.Panel1.Controls.Add(Me.lvServices)
        '
        'splitServices.Panel2
        '
        Me.splitServices.Panel2.Controls.Add(Me.splitServices2)
        Me.splitServices.Size = New System.Drawing.Size(642, 315)
        Me.splitServices.SplitterDistance = 201
        Me.splitServices.TabIndex = 0
        '
        'lvServices
        '
        Me.lvServices.AllowColumnReorder = True
        Me.lvServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader19})
        Me.lvServices.ContextMenuStrip = Me.menuService
        Me.lvServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvServices.FullRowSelect = True
        ListViewGroup27.Header = "Services"
        ListViewGroup27.Name = "gpOther"
        ListViewGroup28.Header = "Search result"
        ListViewGroup28.Name = "gpSearch"
        Me.lvServices.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup27, ListViewGroup28})
        Me.lvServices.HideSelection = False
        Me.lvServices.Location = New System.Drawing.Point(0, 0)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.OverriddenDoubleBuffered = True
        Me.lvServices.Size = New System.Drawing.Size(642, 201)
        Me.lvServices.SmallImageList = Me.imgServices
        Me.lvServices.TabIndex = 1
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
        Me.ColumnHeader7.Text = "DisplayName"
        Me.ColumnHeader7.Width = 243
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "State"
        Me.ColumnHeader8.Width = 79
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "StartType"
        Me.ColumnHeader9.Width = 70
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "ImagePath"
        Me.ColumnHeader10.Width = 250
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Process"
        Me.ColumnHeader11.Width = 150
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "ServiceType"
        Me.ColumnHeader19.Width = 100
        '
        'menuService
        '
        Me.menuService.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem9, Me.ToolStripMenuItem10, Me.ShutdownToolStripMenuItem, Me.ToolStripMenuItem11, Me.ToolStripMenuItem12, Me.ToolStripSeparator2, Me.ToolStripMenuItem20, Me.ToolStripMenuItem21, Me.FileDetailsToolStripMenuItem, Me.ToolStripMenuItem2, Me.GoogleSearchToolStripMenuItem1, Me.ToolStripMenuItem42, Me.SelectedAssociatedProcessToolStripMenuItem1})
        Me.menuService.Name = "menuProc"
        Me.menuService.Size = New System.Drawing.Size(220, 242)
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Image = CType(resources.GetObject("ToolStripMenuItem9.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem9.Text = "Pause"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Image = CType(resources.GetObject("ToolStripMenuItem10.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem10.Text = "Stop"
        '
        'ShutdownToolStripMenuItem
        '
        Me.ShutdownToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.kill
        Me.ShutdownToolStripMenuItem.Name = "ShutdownToolStripMenuItem"
        Me.ShutdownToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ShutdownToolStripMenuItem.Text = "Shutdown"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Image = CType(resources.GetObject("ToolStripMenuItem11.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem11.Text = "Start"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem13, Me.ToolStripMenuItem14, Me.ToolStripMenuItem15})
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(219, 22)
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
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(216, 6)
        '
        'ToolStripMenuItem20
        '
        Me.ToolStripMenuItem20.Image = CType(resources.GetObject("ToolStripMenuItem20.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem20.Name = "ToolStripMenuItem20"
        Me.ToolStripMenuItem20.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem20.Text = "File properties"
        '
        'ToolStripMenuItem21
        '
        Me.ToolStripMenuItem21.Image = CType(resources.GetObject("ToolStripMenuItem21.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem21.Name = "ToolStripMenuItem21"
        Me.ToolStripMenuItem21.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem21.Text = "Open directory"
        '
        'FileDetailsToolStripMenuItem
        '
        Me.FileDetailsToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.magnify16
        Me.FileDetailsToolStripMenuItem.Name = "FileDetailsToolStripMenuItem"
        Me.FileDetailsToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.FileDetailsToolStripMenuItem.Text = "File details"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(216, 6)
        '
        'GoogleSearchToolStripMenuItem1
        '
        Me.GoogleSearchToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.ie7_16
        Me.GoogleSearchToolStripMenuItem1.Name = "GoogleSearchToolStripMenuItem1"
        Me.GoogleSearchToolStripMenuItem1.Size = New System.Drawing.Size(219, 22)
        Me.GoogleSearchToolStripMenuItem1.Text = "Google search"
        '
        'ToolStripMenuItem42
        '
        Me.ToolStripMenuItem42.Name = "ToolStripMenuItem42"
        Me.ToolStripMenuItem42.Size = New System.Drawing.Size(216, 6)
        '
        'SelectedAssociatedProcessToolStripMenuItem1
        '
        Me.SelectedAssociatedProcessToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.exe
        Me.SelectedAssociatedProcessToolStripMenuItem1.Name = "SelectedAssociatedProcessToolStripMenuItem1"
        Me.SelectedAssociatedProcessToolStripMenuItem1.Size = New System.Drawing.Size(219, 22)
        Me.SelectedAssociatedProcessToolStripMenuItem1.Text = "Selected associated process"
        '
        'imgServices
        '
        Me.imgServices.ImageStream = CType(resources.GetObject("imgServices.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgServices.TransparentColor = System.Drawing.Color.Transparent
        Me.imgServices.Images.SetKeyName(0, "service")
        Me.imgServices.Images.SetKeyName(1, "ok")
        Me.imgServices.Images.SetKeyName(2, "ko")
        Me.imgServices.Images.SetKeyName(3, "key")
        Me.imgServices.Images.SetKeyName(4, "noicon")
        Me.imgServices.Images.SetKeyName(5, "thread")
        '
        'splitServices2
        '
        Me.splitServices2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitServices2.IsSplitterFixed = True
        Me.splitServices2.Location = New System.Drawing.Point(0, 0)
        Me.splitServices2.Name = "splitServices2"
        Me.splitServices2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitServices2.Panel1
        '
        Me.splitServices2.Panel1.Controls.Add(Me.cmdCopyServiceToCp)
        Me.splitServices2.Panel1.Controls.Add(Me.lblServicePath)
        Me.splitServices2.Panel1.Controls.Add(Me.lblServiceName)
        '
        'splitServices2.Panel2
        '
        Me.splitServices2.Panel2.Controls.Add(Me.splitServices3)
        Me.splitServices2.Size = New System.Drawing.Size(642, 110)
        Me.splitServices2.SplitterDistance = 35
        Me.splitServices2.TabIndex = 15
        '
        'cmdCopyServiceToCp
        '
        Me.cmdCopyServiceToCp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyServiceToCp.Enabled = False
        Me.cmdCopyServiceToCp.Location = New System.Drawing.Point(540, 8)
        Me.cmdCopyServiceToCp.Name = "cmdCopyServiceToCp"
        Me.cmdCopyServiceToCp.Size = New System.Drawing.Size(99, 21)
        Me.cmdCopyServiceToCp.TabIndex = 19
        Me.cmdCopyServiceToCp.Text = "Copy to clipboard"
        Me.cmdCopyServiceToCp.UseVisualStyleBackColor = True
        '
        'lblServicePath
        '
        Me.lblServicePath.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lblServicePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblServicePath.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblServicePath.Location = New System.Drawing.Point(7, 19)
        Me.lblServicePath.Name = "lblServicePath"
        Me.lblServicePath.ReadOnly = True
        Me.lblServicePath.Size = New System.Drawing.Size(440, 13)
        Me.lblServicePath.TabIndex = 18
        '
        'lblServiceName
        '
        Me.lblServiceName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblServiceName.Location = New System.Drawing.Point(3, 0)
        Me.lblServiceName.Name = "lblServiceName"
        Me.lblServiceName.Size = New System.Drawing.Size(554, 32)
        Me.lblServiceName.TabIndex = 17
        Me.lblServiceName.Text = "Service name :"
        '
        'splitServices3
        '
        Me.splitServices3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices3.Location = New System.Drawing.Point(0, 0)
        Me.splitServices3.Name = "splitServices3"
        '
        'splitServices3.Panel1
        '
        Me.splitServices3.Panel1.Controls.Add(Me.rtb2)
        '
        'splitServices3.Panel2
        '
        Me.splitServices3.Panel2.Controls.Add(Me.splitServices4)
        Me.splitServices3.Size = New System.Drawing.Size(642, 71)
        Me.splitServices3.SplitterDistance = 475
        Me.splitServices3.TabIndex = 0
        '
        'rtb2
        '
        Me.rtb2.AutoWordSelection = True
        Me.rtb2.BackColor = System.Drawing.Color.White
        Me.rtb2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb2.HideSelection = False
        Me.rtb2.Location = New System.Drawing.Point(0, 0)
        Me.rtb2.Name = "rtb2"
        Me.rtb2.ReadOnly = True
        Me.rtb2.Size = New System.Drawing.Size(475, 71)
        Me.rtb2.TabIndex = 13
        Me.rtb2.Text = ""
        Me.rtb2.WordWrap = False
        '
        'splitServices4
        '
        Me.splitServices4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices4.Location = New System.Drawing.Point(0, 0)
        Me.splitServices4.Name = "splitServices4"
        Me.splitServices4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitServices4.Panel1
        '
        Me.splitServices4.Panel1.Controls.Add(Me.tv2)
        '
        'splitServices4.Panel2
        '
        Me.splitServices4.Panel2.Controls.Add(Me.tv)
        Me.splitServices4.Size = New System.Drawing.Size(163, 71)
        Me.splitServices4.SplitterDistance = 32
        Me.splitServices4.TabIndex = 0
        '
        'tv2
        '
        Me.tv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv2.ImageIndex = 0
        Me.tv2.ImageList = Me.imgServices
        Me.tv2.Location = New System.Drawing.Point(0, 0)
        Me.tv2.Name = "tv2"
        Me.tv2.SelectedImageIndex = 2
        Me.tv2.Size = New System.Drawing.Size(163, 32)
        Me.tv2.TabIndex = 15
        '
        'tv
        '
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.ImageIndex = 0
        Me.tv.ImageList = Me.imgServices
        Me.tv.Location = New System.Drawing.Point(0, 0)
        Me.tv.Name = "tv"
        Me.tv.SelectedImageIndex = 0
        Me.tv.Size = New System.Drawing.Size(163, 35)
        Me.tv.TabIndex = 14
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
        ListViewGroup7.Header = "Past jobs"
        ListViewGroup7.Name = "gp1"
        ListViewGroup8.Header = "Future jobs"
        ListViewGroup8.Name = "gp2"
        Me.lvJobs.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup7, ListViewGroup8})
        Me.lvJobs.HideSelection = False
        Me.lvJobs.Location = New System.Drawing.Point(0, 0)
        Me.lvJobs.Name = "lvJobs"
        Me.lvJobs.OverriddenDoubleBuffered = True
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
        'Tray
        '
        Me.Tray.ContextMenuStrip = Me.menuTooltip
        Me.Tray.Icon = CType(resources.GetObject("Tray.Icon"), System.Drawing.Icon)
        Me.Tray.Text = "Yet Another Process Monitor"
        Me.Tray.Visible = True
        '
        'menuTooltip
        '
        Me.menuTooltip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem3, Me.ToolStripMenuItem41, Me.EnableProcessRefreshingToolStripMenuItem, Me.EnableServiceRefreshingToolStripMenuItem, Me.ToolStripSeparator1, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5})
        Me.menuTooltip.Name = "menuTooltip"
        Me.menuTooltip.Size = New System.Drawing.Size(209, 126)
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem3.Image = CType(resources.GetObject("ToolStripMenuItem3.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(208, 22)
        Me.ToolStripMenuItem3.Text = "Show YAPM"
        '
        'ToolStripMenuItem41
        '
        Me.ToolStripMenuItem41.Name = "ToolStripMenuItem41"
        Me.ToolStripMenuItem41.Size = New System.Drawing.Size(205, 6)
        '
        'EnableProcessRefreshingToolStripMenuItem
        '
        Me.EnableProcessRefreshingToolStripMenuItem.Checked = True
        Me.EnableProcessRefreshingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableProcessRefreshingToolStripMenuItem.Name = "EnableProcessRefreshingToolStripMenuItem"
        Me.EnableProcessRefreshingToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.EnableProcessRefreshingToolStripMenuItem.Text = "Enable process refreshing"
        '
        'EnableServiceRefreshingToolStripMenuItem
        '
        Me.EnableServiceRefreshingToolStripMenuItem.Checked = True
        Me.EnableServiceRefreshingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableServiceRefreshingToolStripMenuItem.Name = "EnableServiceRefreshingToolStripMenuItem"
        Me.EnableServiceRefreshingToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.EnableServiceRefreshingToolStripMenuItem.Text = "Enable service refreshing"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(205, 6)
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Image = CType(resources.GetObject("ToolStripMenuItem4.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(208, 22)
        Me.ToolStripMenuItem4.Text = "About YAPM"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(208, 22)
        Me.ToolStripMenuItem5.Text = "Quitter"
        '
        'saveDial
        '
        Me.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        Me.saveDial.SupportMultiDottedExtensions = True
        '
        'openDial
        '
        Me.openDial.CheckFileExists = False
        Me.openDial.CheckPathExists = False
        Me.openDial.SupportMultiDottedExtensions = True
        '
        'Ribbon
        '
        Me.Ribbon.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon.Minimized = False
        Me.Ribbon.Name = "Ribbon"
        Me.Ribbon.Size = New System.Drawing.Size(843, 115)
        Me.Ribbon.TabIndex = 44
        Me.Ribbon.Tabs.Add(Me.TaskTab)
        Me.Ribbon.Tabs.Add(Me.ProcessTab)
        Me.Ribbon.Tabs.Add(Me.ModulesTab)
        Me.Ribbon.Tabs.Add(Me.ThreadTab)
        Me.Ribbon.Tabs.Add(Me.HandlesTab)
        Me.Ribbon.Tabs.Add(Me.WindowTab)
        Me.Ribbon.Tabs.Add(Me.MonitorTab)
        Me.Ribbon.Tabs.Add(Me.ServiceTab)
        Me.Ribbon.Tabs.Add(Me.NetworkTab)
        Me.Ribbon.Tabs.Add(Me.FileTab)
        Me.Ribbon.Tabs.Add(Me.SearchTab)
        Me.Ribbon.Tabs.Add(Me.HelpTab)
        Me.Ribbon.TabSpacing = 6
        '
        'TaskTab
        '
        Me.TaskTab.Panels.Add(Me.RBTaskDisplay)
        Me.TaskTab.Panels.Add(Me.RBTaskActions)
        Me.TaskTab.Tag = Nothing
        Me.TaskTab.Text = "Tasks"
        '
        'RBTaskDisplay
        '
        Me.RBTaskDisplay.ButtonMoreEnabled = False
        Me.RBTaskDisplay.ButtonMoreVisible = False
        Me.RBTaskDisplay.Items.Add(Me.butTaskRefresh)
        Me.RBTaskDisplay.Tag = Nothing
        Me.RBTaskDisplay.Text = "Display"
        '
        'butTaskRefresh
        '
        Me.butTaskRefresh.AltKey = Nothing
        Me.butTaskRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butTaskRefresh.Image = CType(resources.GetObject("butTaskRefresh.Image"), System.Drawing.Image)
        Me.butTaskRefresh.SmallImage = CType(resources.GetObject("butTaskRefresh.SmallImage"), System.Drawing.Image)
        Me.butTaskRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butTaskRefresh.Tag = Nothing
        Me.butTaskRefresh.Text = "Refresh"
        Me.butTaskRefresh.ToolTip = Nothing
        Me.butTaskRefresh.ToolTipImage = Nothing
        Me.butTaskRefresh.ToolTipTitle = Nothing
        '
        'RBTaskActions
        '
        Me.RBTaskActions.ButtonMoreEnabled = False
        Me.RBTaskActions.ButtonMoreVisible = False
        Me.RBTaskActions.Items.Add(Me.butTaskShow)
        Me.RBTaskActions.Items.Add(Me.butTaskEndTask)
        Me.RBTaskActions.Tag = Nothing
        Me.RBTaskActions.Text = "Common task actions"
        '
        'butTaskShow
        '
        Me.butTaskShow.AltKey = Nothing
        Me.butTaskShow.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butTaskShow.Image = Global.YAPM.My.Resources.Resources.showDetails
        Me.butTaskShow.SmallImage = CType(resources.GetObject("butTaskShow.SmallImage"), System.Drawing.Image)
        Me.butTaskShow.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butTaskShow.Tag = Nothing
        Me.butTaskShow.Text = "Show"
        Me.butTaskShow.ToolTip = Nothing
        Me.butTaskShow.ToolTipImage = Nothing
        Me.butTaskShow.ToolTipTitle = Nothing
        '
        'butTaskEndTask
        '
        Me.butTaskEndTask.AltKey = Nothing
        Me.butTaskEndTask.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butTaskEndTask.Image = CType(resources.GetObject("butTaskEndTask.Image"), System.Drawing.Image)
        Me.butTaskEndTask.SmallImage = CType(resources.GetObject("butTaskEndTask.SmallImage"), System.Drawing.Image)
        Me.butTaskEndTask.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butTaskEndTask.Tag = Nothing
        Me.butTaskEndTask.Text = "End task"
        Me.butTaskEndTask.ToolTip = Nothing
        Me.butTaskEndTask.ToolTipImage = Nothing
        Me.butTaskEndTask.ToolTipTitle = Nothing
        '
        'ProcessTab
        '
        Me.ProcessTab.Panels.Add(Me.RBProcessDisplay)
        Me.ProcessTab.Panels.Add(Me.RBProcessActions)
        Me.ProcessTab.Panels.Add(Me.RBProcessPriority)
        Me.ProcessTab.Panels.Add(Me.RBProcessOnline)
        Me.ProcessTab.Tag = Nothing
        Me.ProcessTab.Text = "Processes"
        '
        'RBProcessDisplay
        '
        Me.RBProcessDisplay.ButtonMoreEnabled = False
        Me.RBProcessDisplay.ButtonMoreVisible = False
        Me.RBProcessDisplay.Items.Add(Me.butProcessRerfresh)
        Me.RBProcessDisplay.Items.Add(Me.butProcessDisplayDetails)
        Me.RBProcessDisplay.Items.Add(Me.butProcessPermuteLvTv)
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
        'butProcessDisplayDetails
        '
        Me.butProcessDisplayDetails.AltKey = Nothing
        Me.butProcessDisplayDetails.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessDisplayDetails.Image = Global.YAPM.My.Resources.Resources.hideDetails
        Me.butProcessDisplayDetails.SmallImage = CType(resources.GetObject("butProcessDisplayDetails.SmallImage"), System.Drawing.Image)
        Me.butProcessDisplayDetails.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessDisplayDetails.Tag = Nothing
        Me.butProcessDisplayDetails.Text = "Hide details"
        Me.butProcessDisplayDetails.ToolTip = Nothing
        Me.butProcessDisplayDetails.ToolTipImage = Nothing
        Me.butProcessDisplayDetails.ToolTipTitle = Nothing
        '
        'butProcessPermuteLvTv
        '
        Me.butProcessPermuteLvTv.AltKey = Nothing
        Me.butProcessPermuteLvTv.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessPermuteLvTv.Image = Global.YAPM.My.Resources.Resources.tv2
        Me.butProcessPermuteLvTv.SmallImage = CType(resources.GetObject("butProcessPermuteLvTv.SmallImage"), System.Drawing.Image)
        Me.butProcessPermuteLvTv.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessPermuteLvTv.Tag = Nothing
        Me.butProcessPermuteLvTv.Text = "Listview"
        Me.butProcessPermuteLvTv.ToolTip = Nothing
        Me.butProcessPermuteLvTv.ToolTipImage = Nothing
        Me.butProcessPermuteLvTv.ToolTipTitle = Nothing
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
        Me.RBProcessActions.Items.Add(Me.butProcessShow)
        Me.RBProcessActions.Items.Add(Me.butProcessMonitor)
        Me.RBProcessActions.Tag = Nothing
        Me.RBProcessActions.Text = "Common processes actions"
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
        Me.butProcessLimitCPU.Enabled = False
        Me.butProcessLimitCPU.Image = CType(resources.GetObject("butProcessLimitCPU.Image"), System.Drawing.Image)
        Me.butProcessLimitCPU.SmallImage = CType(resources.GetObject("butProcessLimitCPU.SmallImage"), System.Drawing.Image)
        Me.butProcessLimitCPU.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessLimitCPU.Tag = Nothing
        Me.butProcessLimitCPU.Text = "Limit CPU usage"
        Me.butProcessLimitCPU.ToolTip = Nothing
        Me.butProcessLimitCPU.ToolTipImage = Nothing
        Me.butProcessLimitCPU.ToolTipTitle = Nothing
        '
        'butProcessShow
        '
        Me.butProcessShow.AltKey = Nothing
        Me.butProcessShow.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessShow.DropDownItems.Add(Me.butProcessShowModules)
        Me.butProcessShow.DropDownItems.Add(Me.butProcessThreads)
        Me.butProcessShow.DropDownItems.Add(Me.butShowProcHandles)
        Me.butProcessShow.DropDownItems.Add(Me.butProcessWindows)
        Me.butProcessShow.DropDownItems.Add(Me.butProcessShowAll)
        Me.butProcessShow.Image = CType(resources.GetObject("butProcessShow.Image"), System.Drawing.Image)
        Me.butProcessShow.SmallImage = CType(resources.GetObject("butProcessShow.SmallImage"), System.Drawing.Image)
        Me.butProcessShow.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butProcessShow.Tag = Nothing
        Me.butProcessShow.Text = "Show"
        Me.butProcessShow.ToolTip = Nothing
        Me.butProcessShow.ToolTipImage = Nothing
        Me.butProcessShow.ToolTipTitle = Nothing
        '
        'butProcessShowModules
        '
        Me.butProcessShowModules.AltKey = Nothing
        Me.butProcessShowModules.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessShowModules.Image = Nothing
        Me.butProcessShowModules.SmallImage = CType(resources.GetObject("butProcessShowModules.SmallImage"), System.Drawing.Image)
        Me.butProcessShowModules.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessShowModules.Tag = Nothing
        Me.butProcessShowModules.Text = "Modules"
        Me.butProcessShowModules.ToolTip = Nothing
        Me.butProcessShowModules.ToolTipImage = Nothing
        Me.butProcessShowModules.ToolTipTitle = Nothing
        '
        'butProcessThreads
        '
        Me.butProcessThreads.AltKey = Nothing
        Me.butProcessThreads.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessThreads.Image = Nothing
        Me.butProcessThreads.SmallImage = CType(resources.GetObject("butProcessThreads.SmallImage"), System.Drawing.Image)
        Me.butProcessThreads.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessThreads.Tag = Nothing
        Me.butProcessThreads.Text = "Threads"
        Me.butProcessThreads.ToolTip = Nothing
        Me.butProcessThreads.ToolTipImage = Nothing
        Me.butProcessThreads.ToolTipTitle = Nothing
        '
        'butShowProcHandles
        '
        Me.butShowProcHandles.AltKey = Nothing
        Me.butShowProcHandles.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butShowProcHandles.Image = Nothing
        Me.butShowProcHandles.SmallImage = CType(resources.GetObject("butShowProcHandles.SmallImage"), System.Drawing.Image)
        Me.butShowProcHandles.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butShowProcHandles.Tag = Nothing
        Me.butShowProcHandles.Text = "Handles"
        Me.butShowProcHandles.ToolTip = Nothing
        Me.butShowProcHandles.ToolTipImage = Nothing
        Me.butShowProcHandles.ToolTipTitle = Nothing
        '
        'butProcessWindows
        '
        Me.butProcessWindows.AltKey = Nothing
        Me.butProcessWindows.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessWindows.Image = Nothing
        Me.butProcessWindows.SmallImage = CType(resources.GetObject("butProcessWindows.SmallImage"), System.Drawing.Image)
        Me.butProcessWindows.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessWindows.Tag = Nothing
        Me.butProcessWindows.Text = "Windows"
        Me.butProcessWindows.ToolTip = Nothing
        Me.butProcessWindows.ToolTipImage = Nothing
        Me.butProcessWindows.ToolTipTitle = Nothing
        '
        'butProcessShowAll
        '
        Me.butProcessShowAll.AltKey = Nothing
        Me.butProcessShowAll.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessShowAll.Image = Nothing
        Me.butProcessShowAll.SmallImage = CType(resources.GetObject("butProcessShowAll.SmallImage"), System.Drawing.Image)
        Me.butProcessShowAll.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessShowAll.Tag = Nothing
        Me.butProcessShowAll.Text = "All"
        Me.butProcessShowAll.ToolTip = Nothing
        Me.butProcessShowAll.ToolTipImage = Nothing
        Me.butProcessShowAll.ToolTipTitle = Nothing
        '
        'butProcessMonitor
        '
        Me.butProcessMonitor.AltKey = Nothing
        Me.butProcessMonitor.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessMonitor.Enabled = False
        Me.butProcessMonitor.Image = CType(resources.GetObject("butProcessMonitor.Image"), System.Drawing.Image)
        Me.butProcessMonitor.SmallImage = CType(resources.GetObject("butProcessMonitor.SmallImage"), System.Drawing.Image)
        Me.butProcessMonitor.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessMonitor.Tag = Nothing
        Me.butProcessMonitor.Text = "Monitor"
        Me.butProcessMonitor.ToolTip = Nothing
        Me.butProcessMonitor.ToolTipImage = Nothing
        Me.butProcessMonitor.ToolTipTitle = Nothing
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
        'ModulesTab
        '
        Me.ModulesTab.Panels.Add(Me.RBModuleActions)
        Me.ModulesTab.Panels.Add(Me.RBModuleReport)
        Me.ModulesTab.Panels.Add(Me.RBModuleOnline)
        Me.ModulesTab.Tag = Nothing
        Me.ModulesTab.Text = "Modules"
        '
        'RBModuleActions
        '
        Me.RBModuleActions.ButtonMoreEnabled = False
        Me.RBModuleActions.ButtonMoreVisible = False
        Me.RBModuleActions.Items.Add(Me.butModuleRefresh)
        Me.RBModuleActions.Items.Add(Me.butModuleUnload)
        Me.RBModuleActions.Tag = Nothing
        Me.RBModuleActions.Text = "Actions"
        '
        'butModuleRefresh
        '
        Me.butModuleRefresh.AltKey = Nothing
        Me.butModuleRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butModuleRefresh.Image = CType(resources.GetObject("butModuleRefresh.Image"), System.Drawing.Image)
        Me.butModuleRefresh.SmallImage = CType(resources.GetObject("butModuleRefresh.SmallImage"), System.Drawing.Image)
        Me.butModuleRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butModuleRefresh.Tag = Nothing
        Me.butModuleRefresh.Text = "Refresh"
        Me.butModuleRefresh.ToolTip = Nothing
        Me.butModuleRefresh.ToolTipImage = Nothing
        Me.butModuleRefresh.ToolTipTitle = Nothing
        '
        'butModuleUnload
        '
        Me.butModuleUnload.AltKey = Nothing
        Me.butModuleUnload.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butModuleUnload.Image = CType(resources.GetObject("butModuleUnload.Image"), System.Drawing.Image)
        Me.butModuleUnload.SmallImage = CType(resources.GetObject("butModuleUnload.SmallImage"), System.Drawing.Image)
        Me.butModuleUnload.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butModuleUnload.Tag = Nothing
        Me.butModuleUnload.Text = "Unload"
        Me.butModuleUnload.ToolTip = Nothing
        Me.butModuleUnload.ToolTipImage = Nothing
        Me.butModuleUnload.ToolTipTitle = Nothing
        '
        'RBModuleReport
        '
        Me.RBModuleReport.ButtonMoreEnabled = False
        Me.RBModuleReport.ButtonMoreVisible = False
        Me.RBModuleReport.Items.Add(Me.butModulesSaveReport)
        Me.RBModuleReport.Tag = Nothing
        Me.RBModuleReport.Text = "Report"
        '
        'butModulesSaveReport
        '
        Me.butModulesSaveReport.AltKey = Nothing
        Me.butModulesSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butModulesSaveReport.Image = CType(resources.GetObject("butModulesSaveReport.Image"), System.Drawing.Image)
        Me.butModulesSaveReport.SmallImage = CType(resources.GetObject("butModulesSaveReport.SmallImage"), System.Drawing.Image)
        Me.butModulesSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butModulesSaveReport.Tag = Nothing
        Me.butModulesSaveReport.Text = "Save report"
        Me.butModulesSaveReport.ToolTip = Nothing
        Me.butModulesSaveReport.ToolTipImage = Nothing
        Me.butModulesSaveReport.ToolTipTitle = Nothing
        '
        'RBModuleOnline
        '
        Me.RBModuleOnline.ButtonMoreEnabled = False
        Me.RBModuleOnline.ButtonMoreVisible = False
        Me.RBModuleOnline.Items.Add(Me.butModuleGoogle)
        Me.RBModuleOnline.Tag = Nothing
        Me.RBModuleOnline.Text = "Online"
        '
        'butModuleGoogle
        '
        Me.butModuleGoogle.AltKey = Nothing
        Me.butModuleGoogle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butModuleGoogle.Image = CType(resources.GetObject("butModuleGoogle.Image"), System.Drawing.Image)
        Me.butModuleGoogle.SmallImage = CType(resources.GetObject("butModuleGoogle.SmallImage"), System.Drawing.Image)
        Me.butModuleGoogle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butModuleGoogle.Tag = Nothing
        Me.butModuleGoogle.Text = "Google search"
        Me.butModuleGoogle.ToolTip = Nothing
        Me.butModuleGoogle.ToolTipImage = Nothing
        Me.butModuleGoogle.ToolTipTitle = Nothing
        '
        'ThreadTab
        '
        Me.ThreadTab.Panels.Add(Me.RBThreadsRefresh)
        Me.ThreadTab.Panels.Add(Me.RBThreadAction)
        Me.ThreadTab.Panels.Add(Me.RBThreadPriority)
        Me.ThreadTab.Panels.Add(Me.RBThreadReport)
        Me.ThreadTab.Tag = Nothing
        Me.ThreadTab.Text = "Threads"
        '
        'RBThreadsRefresh
        '
        Me.RBThreadsRefresh.ButtonMoreEnabled = False
        Me.RBThreadsRefresh.ButtonMoreVisible = False
        Me.RBThreadsRefresh.Items.Add(Me.butThreadRefresh)
        Me.RBThreadsRefresh.Tag = Nothing
        Me.RBThreadsRefresh.Text = "Display"
        '
        'butThreadRefresh
        '
        Me.butThreadRefresh.AltKey = Nothing
        Me.butThreadRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadRefresh.Image = CType(resources.GetObject("butThreadRefresh.Image"), System.Drawing.Image)
        Me.butThreadRefresh.SmallImage = CType(resources.GetObject("butThreadRefresh.SmallImage"), System.Drawing.Image)
        Me.butThreadRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadRefresh.Tag = Nothing
        Me.butThreadRefresh.Text = "Refresh"
        Me.butThreadRefresh.ToolTip = Nothing
        Me.butThreadRefresh.ToolTipImage = Nothing
        Me.butThreadRefresh.ToolTipTitle = Nothing
        '
        'RBThreadAction
        '
        Me.RBThreadAction.ButtonMoreEnabled = False
        Me.RBThreadAction.ButtonMoreVisible = False
        Me.RBThreadAction.Items.Add(Me.butThreadKill)
        Me.RBThreadAction.Items.Add(Me.butThreadStop)
        Me.RBThreadAction.Items.Add(Me.butThreadResume)
        Me.RBThreadAction.Tag = Nothing
        Me.RBThreadAction.Text = "Threads actions"
        '
        'butThreadKill
        '
        Me.butThreadKill.AltKey = Nothing
        Me.butThreadKill.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadKill.Image = CType(resources.GetObject("butThreadKill.Image"), System.Drawing.Image)
        Me.butThreadKill.SmallImage = CType(resources.GetObject("butThreadKill.SmallImage"), System.Drawing.Image)
        Me.butThreadKill.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadKill.Tag = Nothing
        Me.butThreadKill.Text = "Kill"
        Me.butThreadKill.ToolTip = Nothing
        Me.butThreadKill.ToolTipImage = Nothing
        Me.butThreadKill.ToolTipTitle = Nothing
        '
        'butThreadStop
        '
        Me.butThreadStop.AltKey = Nothing
        Me.butThreadStop.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadStop.Image = CType(resources.GetObject("butThreadStop.Image"), System.Drawing.Image)
        Me.butThreadStop.SmallImage = CType(resources.GetObject("butThreadStop.SmallImage"), System.Drawing.Image)
        Me.butThreadStop.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadStop.Tag = Nothing
        Me.butThreadStop.Text = "Stop"
        Me.butThreadStop.ToolTip = Nothing
        Me.butThreadStop.ToolTipImage = Nothing
        Me.butThreadStop.ToolTipTitle = Nothing
        '
        'butThreadResume
        '
        Me.butThreadResume.AltKey = Nothing
        Me.butThreadResume.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadResume.Image = CType(resources.GetObject("butThreadResume.Image"), System.Drawing.Image)
        Me.butThreadResume.SmallImage = CType(resources.GetObject("butThreadResume.SmallImage"), System.Drawing.Image)
        Me.butThreadResume.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadResume.Tag = Nothing
        Me.butThreadResume.Text = "Resume"
        Me.butThreadResume.ToolTip = Nothing
        Me.butThreadResume.ToolTipImage = Nothing
        Me.butThreadResume.ToolTipTitle = Nothing
        '
        'RBThreadPriority
        '
        Me.RBThreadPriority.ButtonMoreEnabled = False
        Me.RBThreadPriority.ButtonMoreVisible = False
        Me.RBThreadPriority.Items.Add(Me.butThreadPriority)
        Me.RBThreadPriority.Tag = Nothing
        Me.RBThreadPriority.Text = "Priority"
        '
        'butThreadPriority
        '
        Me.butThreadPriority.AltKey = Nothing
        Me.butThreadPriority.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPidle)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPlowest)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPbelow)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPnormal)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPabove)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPhighest)
        Me.butThreadPriority.DropDownItems.Add(Me.butThreadPcritical)
        Me.butThreadPriority.Image = CType(resources.GetObject("butThreadPriority.Image"), System.Drawing.Image)
        Me.butThreadPriority.SmallImage = CType(resources.GetObject("butThreadPriority.SmallImage"), System.Drawing.Image)
        Me.butThreadPriority.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butThreadPriority.Tag = Nothing
        Me.butThreadPriority.Text = "Priority"
        Me.butThreadPriority.ToolTip = Nothing
        Me.butThreadPriority.ToolTipImage = Nothing
        Me.butThreadPriority.ToolTipTitle = Nothing
        '
        'butThreadPidle
        '
        Me.butThreadPidle.AltKey = Nothing
        Me.butThreadPidle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPidle.Image = CType(resources.GetObject("butThreadPidle.Image"), System.Drawing.Image)
        Me.butThreadPidle.SmallImage = CType(resources.GetObject("butThreadPidle.SmallImage"), System.Drawing.Image)
        Me.butThreadPidle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPidle.Tag = Nothing
        Me.butThreadPidle.Text = "Idle"
        Me.butThreadPidle.ToolTip = Nothing
        Me.butThreadPidle.ToolTipImage = Nothing
        Me.butThreadPidle.ToolTipTitle = Nothing
        '
        'butThreadPlowest
        '
        Me.butThreadPlowest.AltKey = Nothing
        Me.butThreadPlowest.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPlowest.Image = CType(resources.GetObject("butThreadPlowest.Image"), System.Drawing.Image)
        Me.butThreadPlowest.SmallImage = CType(resources.GetObject("butThreadPlowest.SmallImage"), System.Drawing.Image)
        Me.butThreadPlowest.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPlowest.Tag = Nothing
        Me.butThreadPlowest.Text = "Lowest"
        Me.butThreadPlowest.ToolTip = Nothing
        Me.butThreadPlowest.ToolTipImage = Nothing
        Me.butThreadPlowest.ToolTipTitle = Nothing
        '
        'butThreadPbelow
        '
        Me.butThreadPbelow.AltKey = Nothing
        Me.butThreadPbelow.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPbelow.Image = CType(resources.GetObject("butThreadPbelow.Image"), System.Drawing.Image)
        Me.butThreadPbelow.SmallImage = CType(resources.GetObject("butThreadPbelow.SmallImage"), System.Drawing.Image)
        Me.butThreadPbelow.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPbelow.Tag = Nothing
        Me.butThreadPbelow.Text = "BelowNormal"
        Me.butThreadPbelow.ToolTip = Nothing
        Me.butThreadPbelow.ToolTipImage = Nothing
        Me.butThreadPbelow.ToolTipTitle = Nothing
        '
        'butThreadPnormal
        '
        Me.butThreadPnormal.AltKey = Nothing
        Me.butThreadPnormal.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPnormal.Image = CType(resources.GetObject("butThreadPnormal.Image"), System.Drawing.Image)
        Me.butThreadPnormal.SmallImage = CType(resources.GetObject("butThreadPnormal.SmallImage"), System.Drawing.Image)
        Me.butThreadPnormal.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPnormal.Tag = Nothing
        Me.butThreadPnormal.Text = "Normal"
        Me.butThreadPnormal.ToolTip = Nothing
        Me.butThreadPnormal.ToolTipImage = Nothing
        Me.butThreadPnormal.ToolTipTitle = Nothing
        '
        'butThreadPabove
        '
        Me.butThreadPabove.AltKey = Nothing
        Me.butThreadPabove.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPabove.Image = CType(resources.GetObject("butThreadPabove.Image"), System.Drawing.Image)
        Me.butThreadPabove.SmallImage = CType(resources.GetObject("butThreadPabove.SmallImage"), System.Drawing.Image)
        Me.butThreadPabove.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPabove.Tag = Nothing
        Me.butThreadPabove.Text = "AboveNormal"
        Me.butThreadPabove.ToolTip = Nothing
        Me.butThreadPabove.ToolTipImage = Nothing
        Me.butThreadPabove.ToolTipTitle = Nothing
        '
        'butThreadPhighest
        '
        Me.butThreadPhighest.AltKey = Nothing
        Me.butThreadPhighest.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPhighest.Image = CType(resources.GetObject("butThreadPhighest.Image"), System.Drawing.Image)
        Me.butThreadPhighest.SmallImage = CType(resources.GetObject("butThreadPhighest.SmallImage"), System.Drawing.Image)
        Me.butThreadPhighest.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPhighest.Tag = Nothing
        Me.butThreadPhighest.Text = "Highest"
        Me.butThreadPhighest.ToolTip = Nothing
        Me.butThreadPhighest.ToolTipImage = Nothing
        Me.butThreadPhighest.ToolTipTitle = Nothing
        '
        'butThreadPcritical
        '
        Me.butThreadPcritical.AltKey = Nothing
        Me.butThreadPcritical.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadPcritical.Image = CType(resources.GetObject("butThreadPcritical.Image"), System.Drawing.Image)
        Me.butThreadPcritical.SmallImage = CType(resources.GetObject("butThreadPcritical.SmallImage"), System.Drawing.Image)
        Me.butThreadPcritical.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadPcritical.Tag = Nothing
        Me.butThreadPcritical.Text = "TimeCritical"
        Me.butThreadPcritical.ToolTip = Nothing
        Me.butThreadPcritical.ToolTipImage = Nothing
        Me.butThreadPcritical.ToolTipTitle = Nothing
        '
        'RBThreadReport
        '
        Me.RBThreadReport.ButtonMoreEnabled = False
        Me.RBThreadReport.ButtonMoreVisible = False
        Me.RBThreadReport.Items.Add(Me.butThreadSaveReport)
        Me.RBThreadReport.Tag = Nothing
        Me.RBThreadReport.Text = "Report"
        '
        'butThreadSaveReport
        '
        Me.butThreadSaveReport.AltKey = Nothing
        Me.butThreadSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadSaveReport.Image = CType(resources.GetObject("butThreadSaveReport.Image"), System.Drawing.Image)
        Me.butThreadSaveReport.SmallImage = CType(resources.GetObject("butThreadSaveReport.SmallImage"), System.Drawing.Image)
        Me.butThreadSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butThreadSaveReport.Tag = Nothing
        Me.butThreadSaveReport.Text = "Save report"
        Me.butThreadSaveReport.ToolTip = Nothing
        Me.butThreadSaveReport.ToolTipImage = Nothing
        Me.butThreadSaveReport.ToolTipTitle = Nothing
        '
        'HandlesTab
        '
        Me.HandlesTab.Panels.Add(Me.RBHandlesActions)
        Me.HandlesTab.Panels.Add(Me.RBHandlesReport)
        Me.HandlesTab.Tag = Nothing
        Me.HandlesTab.Text = "Handles"
        '
        'RBHandlesActions
        '
        Me.RBHandlesActions.ButtonMoreEnabled = False
        Me.RBHandlesActions.ButtonMoreVisible = False
        Me.RBHandlesActions.Items.Add(Me.butHandleRefresh)
        Me.RBHandlesActions.Items.Add(Me.butHandleClose)
        Me.RBHandlesActions.Tag = Nothing
        Me.RBHandlesActions.Text = "Actions"
        '
        'butHandleRefresh
        '
        Me.butHandleRefresh.AltKey = Nothing
        Me.butHandleRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butHandleRefresh.Image = CType(resources.GetObject("butHandleRefresh.Image"), System.Drawing.Image)
        Me.butHandleRefresh.SmallImage = CType(resources.GetObject("butHandleRefresh.SmallImage"), System.Drawing.Image)
        Me.butHandleRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butHandleRefresh.Tag = Nothing
        Me.butHandleRefresh.Text = "Refresh"
        Me.butHandleRefresh.ToolTip = Nothing
        Me.butHandleRefresh.ToolTipImage = Nothing
        Me.butHandleRefresh.ToolTipTitle = Nothing
        '
        'butHandleClose
        '
        Me.butHandleClose.AltKey = Nothing
        Me.butHandleClose.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butHandleClose.Image = CType(resources.GetObject("butHandleClose.Image"), System.Drawing.Image)
        Me.butHandleClose.SmallImage = CType(resources.GetObject("butHandleClose.SmallImage"), System.Drawing.Image)
        Me.butHandleClose.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butHandleClose.Tag = Nothing
        Me.butHandleClose.Text = "Close handle(s)"
        Me.butHandleClose.ToolTip = Nothing
        Me.butHandleClose.ToolTipImage = Nothing
        Me.butHandleClose.ToolTipTitle = Nothing
        '
        'RBHandlesReport
        '
        Me.RBHandlesReport.ButtonMoreEnabled = False
        Me.RBHandlesReport.ButtonMoreVisible = False
        Me.RBHandlesReport.Items.Add(Me.butHandlesSaveReport)
        Me.RBHandlesReport.Tag = Nothing
        Me.RBHandlesReport.Text = "Report"
        '
        'butHandlesSaveReport
        '
        Me.butHandlesSaveReport.AltKey = Nothing
        Me.butHandlesSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butHandlesSaveReport.Image = CType(resources.GetObject("butHandlesSaveReport.Image"), System.Drawing.Image)
        Me.butHandlesSaveReport.SmallImage = CType(resources.GetObject("butHandlesSaveReport.SmallImage"), System.Drawing.Image)
        Me.butHandlesSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butHandlesSaveReport.Tag = Nothing
        Me.butHandlesSaveReport.Text = "Save report"
        Me.butHandlesSaveReport.ToolTip = Nothing
        Me.butHandlesSaveReport.ToolTipImage = Nothing
        Me.butHandlesSaveReport.ToolTipTitle = Nothing
        '
        'WindowTab
        '
        Me.WindowTab.Panels.Add(Me.RBWindowRefresh)
        Me.WindowTab.Panels.Add(Me.RBWindowCapture)
        Me.WindowTab.Panels.Add(Me.RBWindowActions)
        Me.WindowTab.Panels.Add(Me.RBWindowReport)
        Me.WindowTab.Tag = Nothing
        Me.WindowTab.Text = "Windows"
        '
        'RBWindowRefresh
        '
        Me.RBWindowRefresh.ButtonMoreEnabled = False
        Me.RBWindowRefresh.ButtonMoreVisible = False
        Me.RBWindowRefresh.Items.Add(Me.butWindowRefresh)
        Me.RBWindowRefresh.Tag = Nothing
        Me.RBWindowRefresh.Text = "Display"
        '
        'butWindowRefresh
        '
        Me.butWindowRefresh.AltKey = Nothing
        Me.butWindowRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowRefresh.Image = CType(resources.GetObject("butWindowRefresh.Image"), System.Drawing.Image)
        Me.butWindowRefresh.SmallImage = CType(resources.GetObject("butWindowRefresh.SmallImage"), System.Drawing.Image)
        Me.butWindowRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowRefresh.Tag = Nothing
        Me.butWindowRefresh.Text = "Refresh"
        Me.butWindowRefresh.ToolTip = Nothing
        Me.butWindowRefresh.ToolTipImage = Nothing
        Me.butWindowRefresh.ToolTipTitle = Nothing
        '
        'RBWindowCapture
        '
        Me.RBWindowCapture.ButtonMoreEnabled = False
        Me.RBWindowCapture.ButtonMoreVisible = False
        Me.RBWindowCapture.Items.Add(Me.butWindowFind)
        Me.RBWindowCapture.Tag = Nothing
        Me.RBWindowCapture.Text = "Find process"
        '
        'butWindowFind
        '
        Me.butWindowFind.AltKey = Nothing
        Me.butWindowFind.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowFind.Image = CType(resources.GetObject("butWindowFind.Image"), System.Drawing.Image)
        Me.butWindowFind.SmallImage = CType(resources.GetObject("butWindowFind.SmallImage"), System.Drawing.Image)
        Me.butWindowFind.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowFind.Tag = Nothing
        Me.butWindowFind.Text = "Find process"
        Me.butWindowFind.ToolTip = Nothing
        Me.butWindowFind.ToolTipImage = Nothing
        Me.butWindowFind.ToolTipTitle = Nothing
        '
        'RBWindowActions
        '
        Me.RBWindowActions.ButtonMoreEnabled = False
        Me.RBWindowActions.ButtonMoreVisible = False
        Me.RBWindowActions.Items.Add(Me.butWindowVisibility)
        Me.RBWindowActions.Items.Add(Me.butWindowClose)
        Me.RBWindowActions.Items.Add(Me.butWindowPositionSize)
        Me.RBWindowActions.Tag = Nothing
        Me.RBWindowActions.Text = "Windows actions"
        '
        'butWindowVisibility
        '
        Me.butWindowVisibility.AltKey = Nothing
        Me.butWindowVisibility.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowShow)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowHide)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowEnable)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowDisable)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowBringToFront)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowDoNotBringToFront)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowSetAsForeground)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowSetAsActive)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowMinimize)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowMaximize)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowFlash)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowStopFlashing)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowCaption)
        Me.butWindowVisibility.DropDownItems.Add(Me.butWindowOpacity)
        Me.butWindowVisibility.Image = CType(resources.GetObject("butWindowVisibility.Image"), System.Drawing.Image)
        Me.butWindowVisibility.SmallImage = CType(resources.GetObject("butWindowVisibility.SmallImage"), System.Drawing.Image)
        Me.butWindowVisibility.Style = System.Windows.Forms.RibbonButtonStyle.DropDown
        Me.butWindowVisibility.Tag = Nothing
        Me.butWindowVisibility.Text = "Visibility"
        Me.butWindowVisibility.ToolTip = Nothing
        Me.butWindowVisibility.ToolTipImage = Nothing
        Me.butWindowVisibility.ToolTipTitle = Nothing
        '
        'butWindowShow
        '
        Me.butWindowShow.AltKey = Nothing
        Me.butWindowShow.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowShow.Image = CType(resources.GetObject("butWindowShow.Image"), System.Drawing.Image)
        Me.butWindowShow.SmallImage = CType(resources.GetObject("butWindowShow.SmallImage"), System.Drawing.Image)
        Me.butWindowShow.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowShow.Tag = Nothing
        Me.butWindowShow.Text = "Show"
        Me.butWindowShow.ToolTip = Nothing
        Me.butWindowShow.ToolTipImage = Nothing
        Me.butWindowShow.ToolTipTitle = Nothing
        '
        'butWindowHide
        '
        Me.butWindowHide.AltKey = Nothing
        Me.butWindowHide.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowHide.Image = CType(resources.GetObject("butWindowHide.Image"), System.Drawing.Image)
        Me.butWindowHide.SmallImage = CType(resources.GetObject("butWindowHide.SmallImage"), System.Drawing.Image)
        Me.butWindowHide.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowHide.Tag = Nothing
        Me.butWindowHide.Text = "Hide"
        Me.butWindowHide.ToolTip = Nothing
        Me.butWindowHide.ToolTipImage = Nothing
        Me.butWindowHide.ToolTipTitle = Nothing
        '
        'butWindowEnable
        '
        Me.butWindowEnable.AltKey = Nothing
        Me.butWindowEnable.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowEnable.Image = CType(resources.GetObject("butWindowEnable.Image"), System.Drawing.Image)
        Me.butWindowEnable.SmallImage = CType(resources.GetObject("butWindowEnable.SmallImage"), System.Drawing.Image)
        Me.butWindowEnable.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowEnable.Tag = Nothing
        Me.butWindowEnable.Text = "Enable"
        Me.butWindowEnable.ToolTip = Nothing
        Me.butWindowEnable.ToolTipImage = Nothing
        Me.butWindowEnable.ToolTipTitle = Nothing
        '
        'butWindowDisable
        '
        Me.butWindowDisable.AltKey = Nothing
        Me.butWindowDisable.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowDisable.Image = CType(resources.GetObject("butWindowDisable.Image"), System.Drawing.Image)
        Me.butWindowDisable.SmallImage = CType(resources.GetObject("butWindowDisable.SmallImage"), System.Drawing.Image)
        Me.butWindowDisable.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowDisable.Tag = Nothing
        Me.butWindowDisable.Text = "Disable"
        Me.butWindowDisable.ToolTip = Nothing
        Me.butWindowDisable.ToolTipImage = Nothing
        Me.butWindowDisable.ToolTipTitle = Nothing
        '
        'butWindowBringToFront
        '
        Me.butWindowBringToFront.AltKey = Nothing
        Me.butWindowBringToFront.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowBringToFront.Image = CType(resources.GetObject("butWindowBringToFront.Image"), System.Drawing.Image)
        Me.butWindowBringToFront.SmallImage = CType(resources.GetObject("butWindowBringToFront.SmallImage"), System.Drawing.Image)
        Me.butWindowBringToFront.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowBringToFront.Tag = Nothing
        Me.butWindowBringToFront.Text = "Bring to front"
        Me.butWindowBringToFront.ToolTip = Nothing
        Me.butWindowBringToFront.ToolTipImage = Nothing
        Me.butWindowBringToFront.ToolTipTitle = Nothing
        '
        'butWindowDoNotBringToFront
        '
        Me.butWindowDoNotBringToFront.AltKey = Nothing
        Me.butWindowDoNotBringToFront.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowDoNotBringToFront.Image = CType(resources.GetObject("butWindowDoNotBringToFront.Image"), System.Drawing.Image)
        Me.butWindowDoNotBringToFront.SmallImage = CType(resources.GetObject("butWindowDoNotBringToFront.SmallImage"), System.Drawing.Image)
        Me.butWindowDoNotBringToFront.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowDoNotBringToFront.Tag = Nothing
        Me.butWindowDoNotBringToFront.Text = "Do not bring to front"
        Me.butWindowDoNotBringToFront.ToolTip = Nothing
        Me.butWindowDoNotBringToFront.ToolTipImage = Nothing
        Me.butWindowDoNotBringToFront.ToolTipTitle = Nothing
        '
        'butWindowSetAsForeground
        '
        Me.butWindowSetAsForeground.AltKey = Nothing
        Me.butWindowSetAsForeground.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowSetAsForeground.Image = CType(resources.GetObject("butWindowSetAsForeground.Image"), System.Drawing.Image)
        Me.butWindowSetAsForeground.SmallImage = CType(resources.GetObject("butWindowSetAsForeground.SmallImage"), System.Drawing.Image)
        Me.butWindowSetAsForeground.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowSetAsForeground.Tag = Nothing
        Me.butWindowSetAsForeground.Text = "Set as foreground window"
        Me.butWindowSetAsForeground.ToolTip = Nothing
        Me.butWindowSetAsForeground.ToolTipImage = Nothing
        Me.butWindowSetAsForeground.ToolTipTitle = Nothing
        '
        'butWindowSetAsActive
        '
        Me.butWindowSetAsActive.AltKey = Nothing
        Me.butWindowSetAsActive.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowSetAsActive.Image = CType(resources.GetObject("butWindowSetAsActive.Image"), System.Drawing.Image)
        Me.butWindowSetAsActive.SmallImage = CType(resources.GetObject("butWindowSetAsActive.SmallImage"), System.Drawing.Image)
        Me.butWindowSetAsActive.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowSetAsActive.Tag = Nothing
        Me.butWindowSetAsActive.Text = "Set as active window"
        Me.butWindowSetAsActive.ToolTip = Nothing
        Me.butWindowSetAsActive.ToolTipImage = Nothing
        Me.butWindowSetAsActive.ToolTipTitle = Nothing
        '
        'butWindowMinimize
        '
        Me.butWindowMinimize.AltKey = Nothing
        Me.butWindowMinimize.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowMinimize.Image = CType(resources.GetObject("butWindowMinimize.Image"), System.Drawing.Image)
        Me.butWindowMinimize.SmallImage = CType(resources.GetObject("butWindowMinimize.SmallImage"), System.Drawing.Image)
        Me.butWindowMinimize.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowMinimize.Tag = Nothing
        Me.butWindowMinimize.Text = "Minimize"
        Me.butWindowMinimize.ToolTip = Nothing
        Me.butWindowMinimize.ToolTipImage = Nothing
        Me.butWindowMinimize.ToolTipTitle = Nothing
        '
        'butWindowMaximize
        '
        Me.butWindowMaximize.AltKey = Nothing
        Me.butWindowMaximize.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowMaximize.Image = CType(resources.GetObject("butWindowMaximize.Image"), System.Drawing.Image)
        Me.butWindowMaximize.SmallImage = CType(resources.GetObject("butWindowMaximize.SmallImage"), System.Drawing.Image)
        Me.butWindowMaximize.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowMaximize.Tag = Nothing
        Me.butWindowMaximize.Text = "Maximize"
        Me.butWindowMaximize.ToolTip = Nothing
        Me.butWindowMaximize.ToolTipImage = Nothing
        Me.butWindowMaximize.ToolTipTitle = Nothing
        '
        'butWindowFlash
        '
        Me.butWindowFlash.AltKey = Nothing
        Me.butWindowFlash.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowFlash.Image = CType(resources.GetObject("butWindowFlash.Image"), System.Drawing.Image)
        Me.butWindowFlash.SmallImage = CType(resources.GetObject("butWindowFlash.SmallImage"), System.Drawing.Image)
        Me.butWindowFlash.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowFlash.Tag = Nothing
        Me.butWindowFlash.Text = "Flash"
        Me.butWindowFlash.ToolTip = Nothing
        Me.butWindowFlash.ToolTipImage = Nothing
        Me.butWindowFlash.ToolTipTitle = Nothing
        '
        'butWindowStopFlashing
        '
        Me.butWindowStopFlashing.AltKey = Nothing
        Me.butWindowStopFlashing.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowStopFlashing.Image = CType(resources.GetObject("butWindowStopFlashing.Image"), System.Drawing.Image)
        Me.butWindowStopFlashing.SmallImage = CType(resources.GetObject("butWindowStopFlashing.SmallImage"), System.Drawing.Image)
        Me.butWindowStopFlashing.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowStopFlashing.Tag = Nothing
        Me.butWindowStopFlashing.Text = "Stop flashing"
        Me.butWindowStopFlashing.ToolTip = Nothing
        Me.butWindowStopFlashing.ToolTipImage = Nothing
        Me.butWindowStopFlashing.ToolTipTitle = Nothing
        '
        'butWindowCaption
        '
        Me.butWindowCaption.AltKey = Nothing
        Me.butWindowCaption.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowCaption.Image = CType(resources.GetObject("butWindowCaption.Image"), System.Drawing.Image)
        Me.butWindowCaption.SmallImage = CType(resources.GetObject("butWindowCaption.SmallImage"), System.Drawing.Image)
        Me.butWindowCaption.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowCaption.Tag = Nothing
        Me.butWindowCaption.Text = "Change caption"
        Me.butWindowCaption.ToolTip = Nothing
        Me.butWindowCaption.ToolTipImage = Nothing
        Me.butWindowCaption.ToolTipTitle = Nothing
        '
        'butWindowOpacity
        '
        Me.butWindowOpacity.AltKey = Nothing
        Me.butWindowOpacity.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowOpacity.Image = CType(resources.GetObject("butWindowOpacity.Image"), System.Drawing.Image)
        Me.butWindowOpacity.SmallImage = CType(resources.GetObject("butWindowOpacity.SmallImage"), System.Drawing.Image)
        Me.butWindowOpacity.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowOpacity.Tag = Nothing
        Me.butWindowOpacity.Text = "Change opacity"
        Me.butWindowOpacity.ToolTip = Nothing
        Me.butWindowOpacity.ToolTipImage = Nothing
        Me.butWindowOpacity.ToolTipTitle = Nothing
        '
        'butWindowClose
        '
        Me.butWindowClose.AltKey = Nothing
        Me.butWindowClose.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowClose.Image = CType(resources.GetObject("butWindowClose.Image"), System.Drawing.Image)
        Me.butWindowClose.SmallImage = CType(resources.GetObject("butWindowClose.SmallImage"), System.Drawing.Image)
        Me.butWindowClose.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowClose.Tag = Nothing
        Me.butWindowClose.Text = "Close"
        Me.butWindowClose.ToolTip = Nothing
        Me.butWindowClose.ToolTipImage = Nothing
        Me.butWindowClose.ToolTipTitle = Nothing
        '
        'butWindowPositionSize
        '
        Me.butWindowPositionSize.AltKey = Nothing
        Me.butWindowPositionSize.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowPositionSize.Image = CType(resources.GetObject("butWindowPositionSize.Image"), System.Drawing.Image)
        Me.butWindowPositionSize.SmallImage = CType(resources.GetObject("butWindowPositionSize.SmallImage"), System.Drawing.Image)
        Me.butWindowPositionSize.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowPositionSize.Tag = Nothing
        Me.butWindowPositionSize.Text = "Position    & size"
        Me.butWindowPositionSize.ToolTip = Nothing
        Me.butWindowPositionSize.ToolTipImage = Nothing
        Me.butWindowPositionSize.ToolTipTitle = Nothing
        '
        'RBWindowReport
        '
        Me.RBWindowReport.ButtonMoreEnabled = False
        Me.RBWindowReport.ButtonMoreVisible = False
        Me.RBWindowReport.Items.Add(Me.butWindowSaveReport)
        Me.RBWindowReport.Tag = Nothing
        Me.RBWindowReport.Text = "Report"
        '
        'butWindowSaveReport
        '
        Me.butWindowSaveReport.AltKey = Nothing
        Me.butWindowSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowSaveReport.Image = CType(resources.GetObject("butWindowSaveReport.Image"), System.Drawing.Image)
        Me.butWindowSaveReport.SmallImage = CType(resources.GetObject("butWindowSaveReport.SmallImage"), System.Drawing.Image)
        Me.butWindowSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindowSaveReport.Tag = Nothing
        Me.butWindowSaveReport.Text = "Save report"
        Me.butWindowSaveReport.ToolTip = Nothing
        Me.butWindowSaveReport.ToolTipImage = Nothing
        Me.butWindowSaveReport.ToolTipTitle = Nothing
        '
        'MonitorTab
        '
        Me.MonitorTab.Panels.Add(Me.RBMonitor)
        Me.MonitorTab.Panels.Add(Me.RBMonitoringControl)
        Me.MonitorTab.Panels.Add(Me.butSaveMonitorReport)
        Me.MonitorTab.Tag = Nothing
        Me.MonitorTab.Text = "Monitor"
        '
        'RBMonitor
        '
        Me.RBMonitor.ButtonMoreEnabled = False
        Me.RBMonitor.ButtonMoreVisible = False
        Me.RBMonitor.Items.Add(Me.butMonitoringAdd)
        Me.RBMonitor.Items.Add(Me.butMonitoringRemove)
        Me.RBMonitor.Tag = Nothing
        Me.RBMonitor.Text = "Monitor a process"
        '
        'butMonitoringAdd
        '
        Me.butMonitoringAdd.AltKey = Nothing
        Me.butMonitoringAdd.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMonitoringAdd.Image = CType(resources.GetObject("butMonitoringAdd.Image"), System.Drawing.Image)
        Me.butMonitoringAdd.SmallImage = CType(resources.GetObject("butMonitoringAdd.SmallImage"), System.Drawing.Image)
        Me.butMonitoringAdd.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butMonitoringAdd.Tag = Nothing
        Me.butMonitoringAdd.Text = "Add"
        Me.butMonitoringAdd.ToolTip = Nothing
        Me.butMonitoringAdd.ToolTipImage = Nothing
        Me.butMonitoringAdd.ToolTipTitle = Nothing
        '
        'butMonitoringRemove
        '
        Me.butMonitoringRemove.AltKey = Nothing
        Me.butMonitoringRemove.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMonitoringRemove.Image = CType(resources.GetObject("butMonitoringRemove.Image"), System.Drawing.Image)
        Me.butMonitoringRemove.SmallImage = CType(resources.GetObject("butMonitoringRemove.SmallImage"), System.Drawing.Image)
        Me.butMonitoringRemove.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butMonitoringRemove.Tag = Nothing
        Me.butMonitoringRemove.Text = "Remove selection"
        Me.butMonitoringRemove.ToolTip = Nothing
        Me.butMonitoringRemove.ToolTipImage = Nothing
        Me.butMonitoringRemove.ToolTipTitle = Nothing
        '
        'RBMonitoringControl
        '
        Me.RBMonitoringControl.ButtonMoreEnabled = False
        Me.RBMonitoringControl.ButtonMoreVisible = False
        Me.RBMonitoringControl.Items.Add(Me.butMonitorStart)
        Me.RBMonitoringControl.Items.Add(Me.butMonitorStop)
        Me.RBMonitoringControl.Tag = Nothing
        Me.RBMonitoringControl.Text = "Monitor"
        '
        'butMonitorStart
        '
        Me.butMonitorStart.AltKey = Nothing
        Me.butMonitorStart.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMonitorStart.Enabled = False
        Me.butMonitorStart.Image = CType(resources.GetObject("butMonitorStart.Image"), System.Drawing.Image)
        Me.butMonitorStart.SmallImage = CType(resources.GetObject("butMonitorStart.SmallImage"), System.Drawing.Image)
        Me.butMonitorStart.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butMonitorStart.Tag = Nothing
        Me.butMonitorStart.Text = "Start"
        Me.butMonitorStart.ToolTip = Nothing
        Me.butMonitorStart.ToolTipImage = Nothing
        Me.butMonitorStart.ToolTipTitle = Nothing
        '
        'butMonitorStop
        '
        Me.butMonitorStop.AltKey = Nothing
        Me.butMonitorStop.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMonitorStop.Enabled = False
        Me.butMonitorStop.Image = CType(resources.GetObject("butMonitorStop.Image"), System.Drawing.Image)
        Me.butMonitorStop.SmallImage = CType(resources.GetObject("butMonitorStop.SmallImage"), System.Drawing.Image)
        Me.butMonitorStop.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butMonitorStop.Tag = Nothing
        Me.butMonitorStop.Text = "Stop"
        Me.butMonitorStop.ToolTip = Nothing
        Me.butMonitorStop.ToolTipImage = Nothing
        Me.butMonitorStop.ToolTipTitle = Nothing
        '
        'butSaveMonitorReport
        '
        Me.butSaveMonitorReport.ButtonMoreEnabled = False
        Me.butSaveMonitorReport.ButtonMoreVisible = False
        Me.butSaveMonitorReport.Items.Add(Me.butMonitorSaveReport)
        Me.butSaveMonitorReport.Tag = Nothing
        Me.butSaveMonitorReport.Text = "Report"
        '
        'butMonitorSaveReport
        '
        Me.butMonitorSaveReport.AltKey = Nothing
        Me.butMonitorSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMonitorSaveReport.Enabled = False
        Me.butMonitorSaveReport.Image = CType(resources.GetObject("butMonitorSaveReport.Image"), System.Drawing.Image)
        Me.butMonitorSaveReport.SmallImage = CType(resources.GetObject("butMonitorSaveReport.SmallImage"), System.Drawing.Image)
        Me.butMonitorSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butMonitorSaveReport.Tag = Nothing
        Me.butMonitorSaveReport.Text = "Save report"
        Me.butMonitorSaveReport.ToolTip = Nothing
        Me.butMonitorSaveReport.ToolTipImage = Nothing
        Me.butMonitorSaveReport.ToolTipTitle = Nothing
        '
        'ServiceTab
        '
        Me.ServiceTab.Panels.Add(Me.RBServiceDisplay)
        Me.ServiceTab.Panels.Add(Me.RBServiceAction)
        Me.ServiceTab.Panels.Add(Me.RBServiceStartType)
        Me.ServiceTab.Panels.Add(Me.RBServiceFile)
        Me.ServiceTab.Panels.Add(Me.RBServiceOnline)
        Me.ServiceTab.Panels.Add(Me.RBServiceReport)
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
        'RBServiceReport
        '
        Me.RBServiceReport.ButtonMoreEnabled = False
        Me.RBServiceReport.ButtonMoreVisible = False
        Me.RBServiceReport.Items.Add(Me.butServiceReport)
        Me.RBServiceReport.Tag = Nothing
        Me.RBServiceReport.Text = "Report"
        '
        'butServiceReport
        '
        Me.butServiceReport.AltKey = Nothing
        Me.butServiceReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceReport.Image = CType(resources.GetObject("butServiceReport.Image"), System.Drawing.Image)
        Me.butServiceReport.SmallImage = CType(resources.GetObject("butServiceReport.SmallImage"), System.Drawing.Image)
        Me.butServiceReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceReport.Tag = Nothing
        Me.butServiceReport.Text = "Save report"
        Me.butServiceReport.ToolTip = Nothing
        Me.butServiceReport.ToolTipImage = Nothing
        Me.butServiceReport.ToolTipTitle = Nothing
        '
        'NetworkTab
        '
        Me.NetworkTab.Panels.Add(Me.RBNetworkRefresh)
        Me.NetworkTab.Tag = Nothing
        Me.NetworkTab.Text = "Network"
        '
        'RBNetworkRefresh
        '
        Me.RBNetworkRefresh.ButtonMoreEnabled = False
        Me.RBNetworkRefresh.ButtonMoreVisible = False
        Me.RBNetworkRefresh.Items.Add(Me.butNetworkRefresh)
        Me.RBNetworkRefresh.Tag = Nothing
        Me.RBNetworkRefresh.Text = "Display"
        '
        'butNetworkRefresh
        '
        Me.butNetworkRefresh.AltKey = Nothing
        Me.butNetworkRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butNetworkRefresh.Image = CType(resources.GetObject("butNetworkRefresh.Image"), System.Drawing.Image)
        Me.butNetworkRefresh.SmallImage = CType(resources.GetObject("butNetworkRefresh.SmallImage"), System.Drawing.Image)
        Me.butNetworkRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butNetworkRefresh.Tag = Nothing
        Me.butNetworkRefresh.Text = "Refresh"
        Me.butNetworkRefresh.ToolTip = Nothing
        Me.butNetworkRefresh.ToolTipImage = Nothing
        Me.butNetworkRefresh.ToolTipTitle = Nothing
        '
        'FileTab
        '
        Me.FileTab.Panels.Add(Me.RBFileOpenFile)
        Me.FileTab.Panels.Add(Me.RBFileKillProcess)
        Me.FileTab.Panels.Add(Me.RBFileDelete)
        Me.FileTab.Panels.Add(Me.RBFileOnline)
        Me.FileTab.Panels.Add(Me.RBFileOther)
        Me.FileTab.Panels.Add(Me.RBFileOthers)
        Me.FileTab.Tag = Nothing
        Me.FileTab.Text = "File"
        '
        'RBFileOpenFile
        '
        Me.RBFileOpenFile.ButtonMoreEnabled = False
        Me.RBFileOpenFile.ButtonMoreVisible = False
        Me.RBFileOpenFile.Items.Add(Me.butOpenFile)
        Me.RBFileOpenFile.Items.Add(Me.butFileRefresh)
        Me.RBFileOpenFile.Tag = Nothing
        Me.RBFileOpenFile.Text = "File"
        '
        'butOpenFile
        '
        Me.butOpenFile.AltKey = Nothing
        Me.butOpenFile.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butOpenFile.Image = CType(resources.GetObject("butOpenFile.Image"), System.Drawing.Image)
        Me.butOpenFile.SmallImage = CType(resources.GetObject("butOpenFile.SmallImage"), System.Drawing.Image)
        Me.butOpenFile.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butOpenFile.Tag = Nothing
        Me.butOpenFile.Text = "Open file"
        Me.butOpenFile.ToolTip = Nothing
        Me.butOpenFile.ToolTipImage = Nothing
        Me.butOpenFile.ToolTipTitle = Nothing
        '
        'butFileRefresh
        '
        Me.butFileRefresh.AltKey = Nothing
        Me.butFileRefresh.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileRefresh.Image = CType(resources.GetObject("butFileRefresh.Image"), System.Drawing.Image)
        Me.butFileRefresh.SmallImage = CType(resources.GetObject("butFileRefresh.SmallImage"), System.Drawing.Image)
        Me.butFileRefresh.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileRefresh.Tag = Nothing
        Me.butFileRefresh.Text = "Refresh infos"
        Me.butFileRefresh.ToolTip = Nothing
        Me.butFileRefresh.ToolTipImage = Nothing
        Me.butFileRefresh.ToolTipTitle = Nothing
        '
        'RBFileKillProcess
        '
        Me.RBFileKillProcess.ButtonMoreEnabled = False
        Me.RBFileKillProcess.ButtonMoreVisible = False
        Me.RBFileKillProcess.Enabled = False
        Me.RBFileKillProcess.Items.Add(Me.butFileRelease)
        Me.RBFileKillProcess.Tag = Nothing
        Me.RBFileKillProcess.Text = "Release file"
        '
        'butFileRelease
        '
        Me.butFileRelease.AltKey = Nothing
        Me.butFileRelease.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileRelease.Enabled = False
        Me.butFileRelease.Image = CType(resources.GetObject("butFileRelease.Image"), System.Drawing.Image)
        Me.butFileRelease.SmallImage = CType(resources.GetObject("butFileRelease.SmallImage"), System.Drawing.Image)
        Me.butFileRelease.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileRelease.Tag = Nothing
        Me.butFileRelease.Text = "Release"
        Me.butFileRelease.ToolTip = Nothing
        Me.butFileRelease.ToolTipImage = Nothing
        Me.butFileRelease.ToolTipTitle = Nothing
        '
        'RBFileDelete
        '
        Me.RBFileDelete.ButtonMoreEnabled = False
        Me.RBFileDelete.ButtonMoreVisible = False
        Me.RBFileDelete.Enabled = False
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
        Me.butMoveFileToTrash.Enabled = False
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
        Me.butDeleteFile.Enabled = False
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
        Me.butShreddFile.Enabled = False
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
        Me.RBFileOnline.Enabled = False
        Me.RBFileOnline.Items.Add(Me.butFileGoogleSearch)
        Me.RBFileOnline.Tag = Nothing
        Me.RBFileOnline.Text = "Online"
        '
        'butFileGoogleSearch
        '
        Me.butFileGoogleSearch.AltKey = Nothing
        Me.butFileGoogleSearch.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileGoogleSearch.Enabled = False
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
        Me.RBFileOther.Enabled = False
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
        Me.butFileProperties.Enabled = False
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
        Me.butFileOpenDir.Enabled = False
        Me.butFileOpenDir.Image = CType(resources.GetObject("butFileOpenDir.Image"), System.Drawing.Image)
        Me.butFileOpenDir.SmallImage = CType(resources.GetObject("butFileOpenDir.SmallImage"), System.Drawing.Image)
        Me.butFileOpenDir.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileOpenDir.Tag = Nothing
        Me.butFileOpenDir.Text = "Open file   directory"
        Me.butFileOpenDir.ToolTip = Nothing
        Me.butFileOpenDir.ToolTipImage = Nothing
        Me.butFileOpenDir.ToolTipTitle = Nothing
        '
        'butFileShowFolderProperties
        '
        Me.butFileShowFolderProperties.AltKey = Nothing
        Me.butFileShowFolderProperties.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileShowFolderProperties.Enabled = False
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
        Me.RBFileOthers.Enabled = False
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
        Me.butFileOthersActions.Enabled = False
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
        'SearchTab
        '
        Me.SearchTab.Panels.Add(Me.RBSearchMain)
        Me.SearchTab.Tag = Nothing
        Me.SearchTab.Text = "Search"
        '
        'RBSearchMain
        '
        Me.RBSearchMain.ButtonMoreEnabled = False
        Me.RBSearchMain.ButtonMoreVisible = False
        Me.RBSearchMain.Items.Add(Me.butSearchGo)
        Me.RBSearchMain.Items.Add(Me.butSearchSaveReport)
        Me.RBSearchMain.Items.Add(Me.txtSearchString)
        Me.RBSearchMain.Tag = Nothing
        Me.RBSearchMain.Text = "Search"
        '
        'butSearchGo
        '
        Me.butSearchGo.AltKey = Nothing
        Me.butSearchGo.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSearchGo.Enabled = False
        Me.butSearchGo.Image = CType(resources.GetObject("butSearchGo.Image"), System.Drawing.Image)
        Me.butSearchGo.SmallImage = CType(resources.GetObject("butSearchGo.SmallImage"), System.Drawing.Image)
        Me.butSearchGo.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSearchGo.Tag = Nothing
        Me.butSearchGo.Text = "Launch"
        Me.butSearchGo.ToolTip = Nothing
        Me.butSearchGo.ToolTipImage = Nothing
        Me.butSearchGo.ToolTipTitle = Nothing
        '
        'butSearchSaveReport
        '
        Me.butSearchSaveReport.AltKey = Nothing
        Me.butSearchSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSearchSaveReport.Image = CType(resources.GetObject("butSearchSaveReport.Image"), System.Drawing.Image)
        Me.butSearchSaveReport.SmallImage = CType(resources.GetObject("butSearchSaveReport.SmallImage"), System.Drawing.Image)
        Me.butSearchSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSearchSaveReport.Tag = Nothing
        Me.butSearchSaveReport.Text = "Save report"
        Me.butSearchSaveReport.ToolTip = Nothing
        Me.butSearchSaveReport.ToolTipImage = Nothing
        Me.butSearchSaveReport.ToolTipTitle = Nothing
        '
        'txtSearchString
        '
        Me.txtSearchString.AltKey = Nothing
        Me.txtSearchString.Image = Nothing
        Me.txtSearchString.Tag = Nothing
        Me.txtSearchString.Text = "String to search ::"
        Me.txtSearchString.TextBoxText = Nothing
        Me.txtSearchString.ToolTip = Nothing
        Me.txtSearchString.ToolTipImage = Nothing
        Me.txtSearchString.ToolTipTitle = Nothing
        '
        'HelpTab
        '
        Me.HelpTab.Panels.Add(Me.RBUpdate)
        Me.HelpTab.Panels.Add(Me.RBHelpAction)
        Me.HelpTab.Panels.Add(Me.RBHelpWeb)
        Me.HelpTab.Panels.Add(Me.RBOptions)
        Me.HelpTab.Tag = Nothing
        Me.HelpTab.Text = "Help"
        '
        'RBUpdate
        '
        Me.RBUpdate.ButtonMoreEnabled = False
        Me.RBUpdate.ButtonMoreVisible = False
        Me.RBUpdate.Items.Add(Me.butUpdate)
        Me.RBUpdate.Tag = Nothing
        Me.RBUpdate.Text = "Update"
        '
        'butUpdate
        '
        Me.butUpdate.AltKey = Nothing
        Me.butUpdate.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butUpdate.Image = CType(resources.GetObject("butUpdate.Image"), System.Drawing.Image)
        Me.butUpdate.SmallImage = CType(resources.GetObject("butUpdate.SmallImage"), System.Drawing.Image)
        Me.butUpdate.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butUpdate.Tag = Nothing
        Me.butUpdate.Text = "Check  update"
        Me.butUpdate.ToolTip = Nothing
        Me.butUpdate.ToolTipImage = Nothing
        Me.butUpdate.ToolTipTitle = Nothing
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
        'RBOptions
        '
        Me.RBOptions.ButtonMoreEnabled = False
        Me.RBOptions.ButtonMoreVisible = False
        Me.RBOptions.Items.Add(Me.butPreferences)
        Me.RBOptions.Items.Add(Me.butAlwaysDisplay)
        Me.RBOptions.Tag = Nothing
        Me.RBOptions.Text = "Options"
        '
        'butPreferences
        '
        Me.butPreferences.AltKey = Nothing
        Me.butPreferences.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butPreferences.Image = CType(resources.GetObject("butPreferences.Image"), System.Drawing.Image)
        Me.butPreferences.SmallImage = CType(resources.GetObject("butPreferences.SmallImage"), System.Drawing.Image)
        Me.butPreferences.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butPreferences.Tag = Nothing
        Me.butPreferences.Text = "Preferences"
        Me.butPreferences.ToolTip = Nothing
        Me.butPreferences.ToolTipImage = Nothing
        Me.butPreferences.ToolTipTitle = Nothing
        '
        'butAlwaysDisplay
        '
        Me.butAlwaysDisplay.AltKey = Nothing
        Me.butAlwaysDisplay.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butAlwaysDisplay.Image = CType(resources.GetObject("butAlwaysDisplay.Image"), System.Drawing.Image)
        Me.butAlwaysDisplay.SmallImage = CType(resources.GetObject("butAlwaysDisplay.SmallImage"), System.Drawing.Image)
        Me.butAlwaysDisplay.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butAlwaysDisplay.Tag = Nothing
        Me.butAlwaysDisplay.Text = "Always display"
        Me.butAlwaysDisplay.ToolTip = Nothing
        Me.butAlwaysDisplay.ToolTipImage = Nothing
        Me.butAlwaysDisplay.ToolTipTitle = Nothing
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
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Search service"
        '
        'lblResCount2
        '
        Me.lblResCount2.AutoSize = True
        Me.lblResCount2.Location = New System.Drawing.Point(596, 6)
        Me.lblResCount2.Name = "lblResCount2"
        Me.lblResCount2.Size = New System.Drawing.Size(52, 13)
        Me.lblResCount2.TabIndex = 2
        Me.lblResCount2.Text = "0 result(s)"
        '
        'txtServiceSearch
        '
        Me.txtServiceSearch.Location = New System.Drawing.Point(89, 3)
        Me.txtServiceSearch.Name = "txtServiceSearch"
        Me.txtServiceSearch.Size = New System.Drawing.Size(501, 20)
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
        Me.fileSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.fileSplitContainer.IsSplitterFixed = True
        Me.fileSplitContainer.Location = New System.Drawing.Point(3, 43)
        Me.fileSplitContainer.Name = "fileSplitContainer"
        Me.fileSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'fileSplitContainer.Panel1
        '
        Me.fileSplitContainer.Panel1.Controls.Add(Me.rtb3)
        '
        'fileSplitContainer.Panel2
        '
        Me.fileSplitContainer.Panel2.Controls.Add(Me.gpFileAttributes)
        Me.fileSplitContainer.Panel2.Controls.Add(Me.gpFileDates)
        Me.fileSplitContainer.Panel2.Controls.Add(Me.lstFileString)
        Me.fileSplitContainer.Panel2MinSize = 109
        Me.fileSplitContainer.Size = New System.Drawing.Size(634, 271)
        Me.fileSplitContainer.SplitterDistance = 158
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
        Me.rtb3.Size = New System.Drawing.Size(634, 158)
        Me.rtb3.TabIndex = 12
        Me.rtb3.Text = ""
        '
        'gpFileAttributes
        '
        Me.gpFileAttributes.Controls.Add(Me.chkFileEncrypted)
        Me.gpFileAttributes.Controls.Add(Me.chkFileContentNotIndexed)
        Me.gpFileAttributes.Controls.Add(Me.chkFileNormal)
        Me.gpFileAttributes.Controls.Add(Me.chkFileSystem)
        Me.gpFileAttributes.Controls.Add(Me.chkFileReadOnly)
        Me.gpFileAttributes.Controls.Add(Me.chkFileHidden)
        Me.gpFileAttributes.Controls.Add(Me.chkFileCompressed)
        Me.gpFileAttributes.Controls.Add(Me.chkFileArchive)
        Me.gpFileAttributes.Dock = System.Windows.Forms.DockStyle.Left
        Me.gpFileAttributes.Location = New System.Drawing.Point(203, 0)
        Me.gpFileAttributes.Name = "gpFileAttributes"
        Me.gpFileAttributes.Size = New System.Drawing.Size(173, 109)
        Me.gpFileAttributes.TabIndex = 2
        Me.gpFileAttributes.TabStop = False
        Me.gpFileAttributes.Text = "File attributes"
        '
        'chkFileEncrypted
        '
        Me.chkFileEncrypted.AutoSize = True
        Me.chkFileEncrypted.Enabled = False
        Me.chkFileEncrypted.Location = New System.Drawing.Point(91, 51)
        Me.chkFileEncrypted.Name = "chkFileEncrypted"
        Me.chkFileEncrypted.Size = New System.Drawing.Size(74, 17)
        Me.chkFileEncrypted.TabIndex = 7
        Me.chkFileEncrypted.Text = "Encrypted"
        Me.chkFileEncrypted.UseVisualStyleBackColor = True
        '
        'chkFileContentNotIndexed
        '
        Me.chkFileContentNotIndexed.AutoSize = True
        Me.chkFileContentNotIndexed.Location = New System.Drawing.Point(6, 88)
        Me.chkFileContentNotIndexed.Name = "chkFileContentNotIndexed"
        Me.chkFileContentNotIndexed.Size = New System.Drawing.Size(121, 17)
        Me.chkFileContentNotIndexed.TabIndex = 6
        Me.chkFileContentNotIndexed.Text = "Content not indexed"
        Me.chkFileContentNotIndexed.UseVisualStyleBackColor = True
        '
        'chkFileNormal
        '
        Me.chkFileNormal.AutoSize = True
        Me.chkFileNormal.Enabled = False
        Me.chkFileNormal.Location = New System.Drawing.Point(91, 15)
        Me.chkFileNormal.Name = "chkFileNormal"
        Me.chkFileNormal.Size = New System.Drawing.Size(59, 17)
        Me.chkFileNormal.TabIndex = 5
        Me.chkFileNormal.Text = "Normal"
        Me.chkFileNormal.UseVisualStyleBackColor = True
        '
        'chkFileSystem
        '
        Me.chkFileSystem.AutoSize = True
        Me.chkFileSystem.Location = New System.Drawing.Point(91, 33)
        Me.chkFileSystem.Name = "chkFileSystem"
        Me.chkFileSystem.Size = New System.Drawing.Size(60, 17)
        Me.chkFileSystem.TabIndex = 4
        Me.chkFileSystem.Text = "System"
        Me.chkFileSystem.UseVisualStyleBackColor = True
        '
        'chkFileReadOnly
        '
        Me.chkFileReadOnly.AutoSize = True
        Me.chkFileReadOnly.Location = New System.Drawing.Point(6, 70)
        Me.chkFileReadOnly.Name = "chkFileReadOnly"
        Me.chkFileReadOnly.Size = New System.Drawing.Size(74, 17)
        Me.chkFileReadOnly.TabIndex = 3
        Me.chkFileReadOnly.Text = "Read only"
        Me.chkFileReadOnly.UseVisualStyleBackColor = True
        '
        'chkFileHidden
        '
        Me.chkFileHidden.AutoSize = True
        Me.chkFileHidden.Location = New System.Drawing.Point(6, 52)
        Me.chkFileHidden.Name = "chkFileHidden"
        Me.chkFileHidden.Size = New System.Drawing.Size(60, 17)
        Me.chkFileHidden.TabIndex = 2
        Me.chkFileHidden.Text = "Hidden"
        Me.chkFileHidden.UseVisualStyleBackColor = True
        '
        'chkFileCompressed
        '
        Me.chkFileCompressed.AutoSize = True
        Me.chkFileCompressed.Enabled = False
        Me.chkFileCompressed.Location = New System.Drawing.Point(6, 34)
        Me.chkFileCompressed.Name = "chkFileCompressed"
        Me.chkFileCompressed.Size = New System.Drawing.Size(84, 17)
        Me.chkFileCompressed.TabIndex = 1
        Me.chkFileCompressed.Text = "Compressed"
        Me.chkFileCompressed.UseVisualStyleBackColor = True
        '
        'chkFileArchive
        '
        Me.chkFileArchive.AutoSize = True
        Me.chkFileArchive.Location = New System.Drawing.Point(6, 17)
        Me.chkFileArchive.Name = "chkFileArchive"
        Me.chkFileArchive.Size = New System.Drawing.Size(62, 17)
        Me.chkFileArchive.TabIndex = 0
        Me.chkFileArchive.Text = "Archive"
        Me.chkFileArchive.UseVisualStyleBackColor = True
        '
        'gpFileDates
        '
        Me.gpFileDates.Controls.Add(Me.cmdSetFileDates)
        Me.gpFileDates.Controls.Add(Me.DTlastModification)
        Me.gpFileDates.Controls.Add(Me.DTlastAccess)
        Me.gpFileDates.Controls.Add(Me.DTcreation)
        Me.gpFileDates.Controls.Add(Me.Label6)
        Me.gpFileDates.Controls.Add(Me.Label5)
        Me.gpFileDates.Controls.Add(Me.Label4)
        Me.gpFileDates.Dock = System.Windows.Forms.DockStyle.Left
        Me.gpFileDates.Location = New System.Drawing.Point(0, 0)
        Me.gpFileDates.Name = "gpFileDates"
        Me.gpFileDates.Size = New System.Drawing.Size(203, 109)
        Me.gpFileDates.TabIndex = 1
        Me.gpFileDates.TabStop = False
        Me.gpFileDates.Text = "File dates"
        '
        'cmdSetFileDates
        '
        Me.cmdSetFileDates.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdSetFileDates.Location = New System.Drawing.Point(3, 84)
        Me.cmdSetFileDates.Name = "cmdSetFileDates"
        Me.cmdSetFileDates.Size = New System.Drawing.Size(197, 22)
        Me.cmdSetFileDates.TabIndex = 6
        Me.cmdSetFileDates.Text = "Set dates"
        Me.cmdSetFileDates.UseVisualStyleBackColor = True
        '
        'DTlastModification
        '
        Me.DTlastModification.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTlastModification.Location = New System.Drawing.Point(102, 57)
        Me.DTlastModification.Name = "DTlastModification"
        Me.DTlastModification.Size = New System.Drawing.Size(84, 20)
        Me.DTlastModification.TabIndex = 5
        '
        'DTlastAccess
        '
        Me.DTlastAccess.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTlastAccess.Location = New System.Drawing.Point(102, 35)
        Me.DTlastAccess.Name = "DTlastAccess"
        Me.DTlastAccess.Size = New System.Drawing.Size(84, 20)
        Me.DTlastAccess.TabIndex = 4
        '
        'DTcreation
        '
        Me.DTcreation.CustomFormat = ""
        Me.DTcreation.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTcreation.Location = New System.Drawing.Point(102, 13)
        Me.DTcreation.Name = "DTcreation"
        Me.DTcreation.Size = New System.Drawing.Size(84, 20)
        Me.DTcreation.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Last access"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Last modification"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Creation"
        '
        'lstFileString
        '
        Me.lstFileString.Dock = System.Windows.Forms.DockStyle.Right
        Me.lstFileString.FormattingEnabled = True
        Me.lstFileString.Location = New System.Drawing.Point(406, 0)
        Me.lstFileString.Name = "lstFileString"
        Me.lstFileString.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstFileString.Size = New System.Drawing.Size(228, 108)
        Me.lstFileString.TabIndex = 0
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
        Me.pctFileSmall.ContextMenuStrip = Me.mnuFileCopyPctSmall
        Me.pctFileSmall.Location = New System.Drawing.Point(503, 19)
        Me.pctFileSmall.Name = "pctFileSmall"
        Me.pctFileSmall.Size = New System.Drawing.Size(16, 16)
        Me.pctFileSmall.TabIndex = 9
        Me.pctFileSmall.TabStop = False
        '
        'mnuFileCopyPctSmall
        '
        Me.mnuFileCopyPctSmall.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem17})
        Me.mnuFileCopyPctSmall.Name = "menuCopyPctbig"
        Me.mnuFileCopyPctSmall.Size = New System.Drawing.Size(170, 26)
        '
        'ToolStripMenuItem17
        '
        Me.ToolStripMenuItem17.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.ToolStripMenuItem17.Name = "ToolStripMenuItem17"
        Me.ToolStripMenuItem17.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItem17.Text = "Copy to clipboard"
        '
        'pctFileBig
        '
        Me.pctFileBig.ContextMenuStrip = Me.mnuFileCopyPctBig
        Me.pctFileBig.Location = New System.Drawing.Point(525, 3)
        Me.pctFileBig.Name = "pctFileBig"
        Me.pctFileBig.Size = New System.Drawing.Size(32, 32)
        Me.pctFileBig.TabIndex = 8
        Me.pctFileBig.TabStop = False
        '
        'mnuFileCopyPctBig
        '
        Me.mnuFileCopyPctBig.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem16})
        Me.mnuFileCopyPctBig.Name = "menuCopyPctbig"
        Me.mnuFileCopyPctBig.Size = New System.Drawing.Size(170, 26)
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        Me.ToolStripMenuItem16.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItem16.Text = "Copy to clipboard"
        '
        'panelMain6
        '
        Me.panelMain6.Controls.Add(Me.SplitContainerSearch)
        Me.panelMain6.Location = New System.Drawing.Point(117, 181)
        Me.panelMain6.Name = "panelMain6"
        Me.panelMain6.Size = New System.Drawing.Size(565, 276)
        Me.panelMain6.TabIndex = 48
        Me.panelMain6.Visible = False
        '
        'SplitContainerSearch
        '
        Me.SplitContainerSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerSearch.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerSearch.IsSplitterFixed = True
        Me.SplitContainerSearch.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerSearch.Name = "SplitContainerSearch"
        Me.SplitContainerSearch.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerSearch.Panel1
        '
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.lblResultsCount)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.txtSearchResults)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchWindows)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchHandles)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchModules)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchServices)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchProcess)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchCase)
        '
        'SplitContainerSearch.Panel2
        '
        Me.SplitContainerSearch.Panel2.Controls.Add(Me.lvSearchResults)
        Me.SplitContainerSearch.Size = New System.Drawing.Size(565, 276)
        Me.SplitContainerSearch.SplitterDistance = 55
        Me.SplitContainerSearch.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Search result"
        '
        'lblResultsCount
        '
        Me.lblResultsCount.AutoSize = True
        Me.lblResultsCount.Location = New System.Drawing.Point(396, 34)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(52, 13)
        Me.lblResultsCount.TabIndex = 12
        Me.lblResultsCount.Text = "0 result(s)"
        '
        'txtSearchResults
        '
        Me.txtSearchResults.Location = New System.Drawing.Point(89, 31)
        Me.txtSearchResults.Name = "txtSearchResults"
        Me.txtSearchResults.Size = New System.Drawing.Size(298, 20)
        Me.txtSearchResults.TabIndex = 11
        '
        'chkSearchWindows
        '
        Me.chkSearchWindows.AutoSize = True
        Me.chkSearchWindows.Checked = True
        Me.chkSearchWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchWindows.Location = New System.Drawing.Point(510, 7)
        Me.chkSearchWindows.Name = "chkSearchWindows"
        Me.chkSearchWindows.Size = New System.Drawing.Size(67, 17)
        Me.chkSearchWindows.TabIndex = 6
        Me.chkSearchWindows.Text = "windows"
        Me.chkSearchWindows.UseVisualStyleBackColor = True
        '
        'chkSearchHandles
        '
        Me.chkSearchHandles.AutoSize = True
        Me.chkSearchHandles.Checked = True
        Me.chkSearchHandles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchHandles.Location = New System.Drawing.Point(441, 7)
        Me.chkSearchHandles.Name = "chkSearchHandles"
        Me.chkSearchHandles.Size = New System.Drawing.Size(63, 17)
        Me.chkSearchHandles.TabIndex = 5
        Me.chkSearchHandles.Text = "handles"
        Me.chkSearchHandles.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(162, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "search in"
        '
        'chkSearchModules
        '
        Me.chkSearchModules.AutoSize = True
        Me.chkSearchModules.Checked = True
        Me.chkSearchModules.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchModules.Location = New System.Drawing.Point(300, 7)
        Me.chkSearchModules.Name = "chkSearchModules"
        Me.chkSearchModules.Size = New System.Drawing.Size(65, 17)
        Me.chkSearchModules.TabIndex = 3
        Me.chkSearchModules.Text = "modules"
        Me.chkSearchModules.UseVisualStyleBackColor = True
        '
        'chkSearchServices
        '
        Me.chkSearchServices.AutoSize = True
        Me.chkSearchServices.Checked = True
        Me.chkSearchServices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchServices.Location = New System.Drawing.Point(370, 7)
        Me.chkSearchServices.Name = "chkSearchServices"
        Me.chkSearchServices.Size = New System.Drawing.Size(65, 17)
        Me.chkSearchServices.TabIndex = 2
        Me.chkSearchServices.Text = "services"
        Me.chkSearchServices.UseVisualStyleBackColor = True
        '
        'chkSearchProcess
        '
        Me.chkSearchProcess.AutoSize = True
        Me.chkSearchProcess.Checked = True
        Me.chkSearchProcess.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchProcess.Location = New System.Drawing.Point(223, 7)
        Me.chkSearchProcess.Name = "chkSearchProcess"
        Me.chkSearchProcess.Size = New System.Drawing.Size(74, 17)
        Me.chkSearchProcess.TabIndex = 1
        Me.chkSearchProcess.Text = "processes"
        Me.chkSearchProcess.UseVisualStyleBackColor = True
        '
        'chkSearchCase
        '
        Me.chkSearchCase.AutoSize = True
        Me.chkSearchCase.Location = New System.Drawing.Point(8, 7)
        Me.chkSearchCase.Name = "chkSearchCase"
        Me.chkSearchCase.Size = New System.Drawing.Size(94, 17)
        Me.chkSearchCase.TabIndex = 0
        Me.chkSearchCase.Text = "Case sensitive"
        Me.chkSearchCase.UseVisualStyleBackColor = True
        '
        'lvSearchResults
        '
        Me.lvSearchResults.AllowColumnReorder = True
        Me.lvSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader17})
        Me.lvSearchResults.ContextMenuStrip = Me.menuSearch
        Me.lvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSearchResults.FullRowSelect = True
        ListViewGroup9.Header = "Results"
        ListViewGroup9.Name = "gpResults"
        ListViewGroup10.Header = "Search results"
        ListViewGroup10.Name = "gpSearchResults"
        Me.lvSearchResults.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup9, ListViewGroup10})
        Me.lvSearchResults.HideSelection = False
        Me.lvSearchResults.Location = New System.Drawing.Point(0, 0)
        Me.lvSearchResults.Name = "lvSearchResults"
        Me.lvSearchResults.OverriddenDoubleBuffered = True
        Me.lvSearchResults.Size = New System.Drawing.Size(565, 217)
        Me.lvSearchResults.SmallImageList = Me.imgSearch
        Me.lvSearchResults.TabIndex = 3
        Me.lvSearchResults.UseCompatibleStateImageBehavior = False
        Me.lvSearchResults.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Type"
        Me.ColumnHeader12.Width = 80
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Result"
        Me.ColumnHeader13.Width = 400
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Field"
        Me.ColumnHeader14.Width = 150
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "Process"
        Me.ColumnHeader17.Width = 150
        '
        'menuSearch
        '
        Me.menuSearch.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAssociatedProcessToolStripMenuItem, Me.ToolStripMenuItem18, Me.CloseToolStripMenuItem})
        Me.menuSearch.Name = "menuProc"
        Me.menuSearch.Size = New System.Drawing.Size(248, 54)
        '
        'SelectAssociatedProcessToolStripMenuItem
        '
        Me.SelectAssociatedProcessToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.exe
        Me.SelectAssociatedProcessToolStripMenuItem.Name = "SelectAssociatedProcessToolStripMenuItem"
        Me.SelectAssociatedProcessToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.SelectAssociatedProcessToolStripMenuItem.Text = "Select associated process/service"
        '
        'ToolStripMenuItem18
        '
        Me.ToolStripMenuItem18.Name = "ToolStripMenuItem18"
        Me.ToolStripMenuItem18.Size = New System.Drawing.Size(244, 6)
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.kill
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.CloseToolStripMenuItem.Text = "Close item"
        '
        'imgSearch
        '
        Me.imgSearch.ImageStream = CType(resources.GetObject("imgSearch.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSearch.TransparentColor = System.Drawing.Color.Transparent
        Me.imgSearch.Images.SetKeyName(0, "service")
        Me.imgSearch.Images.SetKeyName(1, "handle")
        Me.imgSearch.Images.SetKeyName(2, "dll")
        Me.imgSearch.Images.SetKeyName(3, "noicon")
        Me.imgSearch.Images.SetKeyName(4, "window")
        '
        'panelMain7
        '
        Me.panelMain7.Controls.Add(Me.SplitContainerHandles)
        Me.panelMain7.Location = New System.Drawing.Point(116, 163)
        Me.panelMain7.Name = "panelMain7"
        Me.panelMain7.Size = New System.Drawing.Size(565, 276)
        Me.panelMain7.TabIndex = 49
        Me.panelMain7.Visible = False
        '
        'SplitContainerHandles
        '
        Me.SplitContainerHandles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerHandles.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerHandles.IsSplitterFixed = True
        Me.SplitContainerHandles.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerHandles.Name = "SplitContainerHandles"
        Me.SplitContainerHandles.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerHandles.Panel1
        '
        Me.SplitContainerHandles.Panel1.Controls.Add(Me.Label9)
        Me.SplitContainerHandles.Panel1.Controls.Add(Me.lblHandlesCount)
        Me.SplitContainerHandles.Panel1.Controls.Add(Me.txtSearchHandle)
        '
        'SplitContainerHandles.Panel2
        '
        Me.SplitContainerHandles.Panel2.Controls.Add(Me.lvHandles)
        Me.SplitContainerHandles.Size = New System.Drawing.Size(565, 276)
        Me.SplitContainerHandles.SplitterDistance = 25
        Me.SplitContainerHandles.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Search handle"
        '
        'lblHandlesCount
        '
        Me.lblHandlesCount.AutoSize = True
        Me.lblHandlesCount.Location = New System.Drawing.Point(393, 5)
        Me.lblHandlesCount.Name = "lblHandlesCount"
        Me.lblHandlesCount.Size = New System.Drawing.Size(52, 13)
        Me.lblHandlesCount.TabIndex = 12
        Me.lblHandlesCount.Text = "0 result(s)"
        '
        'txtSearchHandle
        '
        Me.txtSearchHandle.Location = New System.Drawing.Point(86, 2)
        Me.txtSearchHandle.Name = "txtSearchHandle"
        Me.txtSearchHandle.Size = New System.Drawing.Size(298, 20)
        Me.txtSearchHandle.TabIndex = 11
        '
        'lvHandles
        '
        Me.lvHandles.AllowColumnReorder = True
        Me.lvHandles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader24, Me.ColumnHeader25, Me.ColumnHeader26, Me.ColumnHeader27, Me.ColumnHeader28, Me.ColumnHeader15, Me.ColumnHeader16})
        Me.lvHandles.ContextMenuStrip = Me.menuHandles
        Me.lvHandles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvHandles.FullRowSelect = True
        ListViewGroup11.Header = "Handles"
        ListViewGroup11.Name = "gpOther"
        ListViewGroup12.Header = "Search result"
        ListViewGroup12.Name = "gpSearch"
        Me.lvHandles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup11, ListViewGroup12})
        Me.lvHandles.HideSelection = False
        Me.lvHandles.Location = New System.Drawing.Point(0, 0)
        Me.lvHandles.Name = "lvHandles"
        Me.lvHandles.OverriddenDoubleBuffered = True
        Me.lvHandles.Size = New System.Drawing.Size(565, 247)
        Me.lvHandles.SmallImageList = Me.imgServices
        Me.lvHandles.TabIndex = 3
        Me.lvHandles.UseCompatibleStateImageBehavior = False
        Me.lvHandles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader24
        '
        Me.ColumnHeader24.Text = "Type"
        Me.ColumnHeader24.Width = 80
        '
        'ColumnHeader25
        '
        Me.ColumnHeader25.Text = "Name"
        Me.ColumnHeader25.Width = 400
        '
        'ColumnHeader26
        '
        Me.ColumnHeader26.Text = "HandleCount"
        Me.ColumnHeader26.Width = 70
        '
        'ColumnHeader27
        '
        Me.ColumnHeader27.Text = "PointerCount"
        Me.ColumnHeader27.Width = 70
        '
        'ColumnHeader28
        '
        Me.ColumnHeader28.Text = "ObjectCount"
        Me.ColumnHeader28.Width = 70
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Handle"
        Me.ColumnHeader15.Width = 70
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Process"
        Me.ColumnHeader16.Width = 170
        '
        'menuHandles
        '
        Me.menuHandles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem19, Me.ToolStripSeparator3, Me.ToolStripMenuItem22})
        Me.menuHandles.Name = "menuProc"
        Me.menuHandles.Size = New System.Drawing.Size(207, 54)
        '
        'ToolStripMenuItem19
        '
        Me.ToolStripMenuItem19.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem19.Name = "ToolStripMenuItem19"
        Me.ToolStripMenuItem19.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem19.Text = "&Select associated process"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(203, 6)
        '
        'ToolStripMenuItem22
        '
        Me.ToolStripMenuItem22.Image = Global.YAPM.My.Resources.Resources.kill
        Me.ToolStripMenuItem22.Name = "ToolStripMenuItem22"
        Me.ToolStripMenuItem22.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem22.Text = "Close item"
        '
        'panelMain8
        '
        Me.panelMain8.Controls.Add(Me.splitMonitor)
        Me.panelMain8.Location = New System.Drawing.Point(123, 171)
        Me.panelMain8.Name = "panelMain8"
        Me.panelMain8.Size = New System.Drawing.Size(634, 297)
        Me.panelMain8.TabIndex = 50
        Me.panelMain8.Visible = False
        '
        'splitMonitor
        '
        Me.splitMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor.Name = "splitMonitor"
        '
        'splitMonitor.Panel1
        '
        Me.splitMonitor.Panel1.Controls.Add(Me.tvMonitor)
        '
        'splitMonitor.Panel2
        '
        Me.splitMonitor.Panel2.Controls.Add(Me.splitMonitor2)
        Me.splitMonitor.Size = New System.Drawing.Size(634, 297)
        Me.splitMonitor.SplitterDistance = 210
        Me.splitMonitor.TabIndex = 0
        '
        'tvMonitor
        '
        Me.tvMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvMonitor.FullRowSelect = True
        Me.tvMonitor.ImageIndex = 0
        Me.tvMonitor.ImageList = Me.imgMonitor
        Me.tvMonitor.Location = New System.Drawing.Point(0, 0)
        Me.tvMonitor.Name = "tvMonitor"
        TreeNode1.ImageIndex = 1
        TreeNode1.Name = "processes"
        TreeNode1.SelectedImageIndex = 1
        TreeNode1.Text = "Processes"
        Me.tvMonitor.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.tvMonitor.SelectedImageIndex = 0
        Me.tvMonitor.Size = New System.Drawing.Size(210, 297)
        Me.tvMonitor.TabIndex = 0
        '
        'imgMonitor
        '
        Me.imgMonitor.ImageStream = CType(resources.GetObject("imgMonitor.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMonitor.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMonitor.Images.SetKeyName(0, "exe")
        Me.imgMonitor.Images.SetKeyName(1, "down")
        Me.imgMonitor.Images.SetKeyName(2, "sub")
        '
        'splitMonitor2
        '
        Me.splitMonitor2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor2.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor2.Name = "splitMonitor2"
        Me.splitMonitor2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMonitor2.Panel1
        '
        Me.splitMonitor2.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.splitMonitor2.Panel1.Controls.Add(Me.txtMonitoringLog)
        '
        'splitMonitor2.Panel2
        '
        Me.splitMonitor2.Panel2.Controls.Add(Me.splitMonitor3)
        Me.splitMonitor2.Size = New System.Drawing.Size(420, 297)
        Me.splitMonitor2.SplitterDistance = 125
        Me.splitMonitor2.TabIndex = 0
        '
        'txtMonitoringLog
        '
        Me.txtMonitoringLog.BackColor = System.Drawing.Color.White
        Me.txtMonitoringLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMonitoringLog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonitoringLog.Location = New System.Drawing.Point(0, 0)
        Me.txtMonitoringLog.Multiline = True
        Me.txtMonitoringLog.Name = "txtMonitoringLog"
        Me.txtMonitoringLog.ReadOnly = True
        Me.txtMonitoringLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMonitoringLog.Size = New System.Drawing.Size(420, 125)
        Me.txtMonitoringLog.TabIndex = 0
        Me.txtMonitoringLog.Text = "No process monitored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click on 'Add' button to monitor a process."
        '
        'splitMonitor3
        '
        Me.splitMonitor3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splitMonitor3.IsSplitterFixed = True
        Me.splitMonitor3.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor3.Name = "splitMonitor3"
        Me.splitMonitor3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMonitor3.Panel1
        '
        Me.splitMonitor3.Panel1.Controls.Add(Me.splitMonitor4)
        '
        'splitMonitor3.Panel2
        '
        Me.splitMonitor3.Panel2.Controls.Add(Me.txtMonitorNumber)
        Me.splitMonitor3.Panel2.Controls.Add(Me.lblMonitorMaxNumber)
        Me.splitMonitor3.Panel2.Controls.Add(Me.chkMonitorRightAuto)
        Me.splitMonitor3.Panel2.Controls.Add(Me.chkMonitorLeftAuto)
        Me.splitMonitor3.Panel2.Controls.Add(Me.dtMonitorR)
        Me.splitMonitor3.Panel2.Controls.Add(Me.dtMonitorL)
        Me.splitMonitor3.Size = New System.Drawing.Size(420, 168)
        Me.splitMonitor3.SplitterDistance = 139
        Me.splitMonitor3.TabIndex = 0
        '
        'splitMonitor4
        '
        Me.splitMonitor4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitMonitor4.IsSplitterFixed = True
        Me.splitMonitor4.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor4.Name = "splitMonitor4"
        Me.splitMonitor4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMonitor4.Panel2
        '
        Me.splitMonitor4.Panel2.Controls.Add(Me.graphMonitor)
        Me.splitMonitor4.Size = New System.Drawing.Size(420, 139)
        Me.splitMonitor4.SplitterDistance = 25
        Me.splitMonitor4.TabIndex = 4
        '
        'graphMonitor
        '
        Me.graphMonitor.BackColor = System.Drawing.Color.Black
        Me.graphMonitor.ColorMemory2 = System.Drawing.Color.Blue
        Me.graphMonitor.ColorMemory3 = System.Drawing.Color.Orange
        Me.graphMonitor.dDate = New Date(CType(0, Long))
        Me.graphMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphMonitor.EnableGraph = False
        Me.graphMonitor.Location = New System.Drawing.Point(0, 0)
        Me.graphMonitor.Name = "graphMonitor"
        Me.graphMonitor.Size = New System.Drawing.Size(420, 110)
        Me.graphMonitor.TabIndex = 3
        Me.graphMonitor.TabStop = False
        Me.graphMonitor.ViewMax = 0
        Me.graphMonitor.ViewMin = 0
        '
        'txtMonitorNumber
        '
        Me.txtMonitorNumber.Location = New System.Drawing.Point(241, 0)
        Me.txtMonitorNumber.Name = "txtMonitorNumber"
        Me.txtMonitorNumber.Size = New System.Drawing.Size(33, 20)
        Me.txtMonitorNumber.TabIndex = 11
        Me.txtMonitorNumber.Text = "200"
        '
        'lblMonitorMaxNumber
        '
        Me.lblMonitorMaxNumber.AutoSize = True
        Me.lblMonitorMaxNumber.Location = New System.Drawing.Point(171, 6)
        Me.lblMonitorMaxNumber.Name = "lblMonitorMaxNumber"
        Me.lblMonitorMaxNumber.Size = New System.Drawing.Size(64, 13)
        Me.lblMonitorMaxNumber.TabIndex = 10
        Me.lblMonitorMaxNumber.Text = "Max. values"
        '
        'chkMonitorRightAuto
        '
        Me.chkMonitorRightAuto.AutoSize = True
        Me.chkMonitorRightAuto.Checked = True
        Me.chkMonitorRightAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonitorRightAuto.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkMonitorRightAuto.Location = New System.Drawing.Point(283, 0)
        Me.chkMonitorRightAuto.Name = "chkMonitorRightAuto"
        Me.chkMonitorRightAuto.Size = New System.Drawing.Size(48, 25)
        Me.chkMonitorRightAuto.TabIndex = 9
        Me.chkMonitorRightAuto.Text = "Now"
        Me.chkMonitorRightAuto.UseVisualStyleBackColor = True
        '
        'chkMonitorLeftAuto
        '
        Me.chkMonitorLeftAuto.AutoSize = True
        Me.chkMonitorLeftAuto.Checked = True
        Me.chkMonitorLeftAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonitorLeftAuto.Location = New System.Drawing.Point(95, 4)
        Me.chkMonitorLeftAuto.Name = "chkMonitorLeftAuto"
        Me.chkMonitorLeftAuto.Size = New System.Drawing.Size(73, 17)
        Me.chkMonitorLeftAuto.TabIndex = 8
        Me.chkMonitorLeftAuto.Text = "Automatic"
        Me.chkMonitorLeftAuto.UseVisualStyleBackColor = True
        '
        'dtMonitorR
        '
        Me.dtMonitorR.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtMonitorR.Enabled = False
        Me.dtMonitorR.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonitorR.Location = New System.Drawing.Point(331, 0)
        Me.dtMonitorR.Name = "dtMonitorR"
        Me.dtMonitorR.Size = New System.Drawing.Size(89, 20)
        Me.dtMonitorR.TabIndex = 7
        '
        'dtMonitorL
        '
        Me.dtMonitorL.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtMonitorL.Enabled = False
        Me.dtMonitorL.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonitorL.Location = New System.Drawing.Point(0, 0)
        Me.dtMonitorL.Name = "dtMonitorL"
        Me.dtMonitorL.Size = New System.Drawing.Size(89, 20)
        Me.dtMonitorL.TabIndex = 6
        '
        'timerMonitoring
        '
        Me.timerMonitoring.Interval = 1000
        '
        'panelMain9
        '
        Me.panelMain9.Controls.Add(Me.splitThreads)
        Me.panelMain9.Location = New System.Drawing.Point(119, 163)
        Me.panelMain9.Name = "panelMain9"
        Me.panelMain9.Size = New System.Drawing.Size(565, 276)
        Me.panelMain9.TabIndex = 51
        Me.panelMain9.Visible = False
        '
        'splitThreads
        '
        Me.splitThreads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitThreads.Location = New System.Drawing.Point(0, 0)
        Me.splitThreads.Name = "splitThreads"
        Me.splitThreads.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitThreads.Panel1
        '
        Me.splitThreads.Panel1.Controls.Add(Me.SplitContainerThreads)
        '
        'splitThreads.Panel2
        '
        Me.splitThreads.Panel2.Controls.Add(Me.rtb4)
        Me.splitThreads.Size = New System.Drawing.Size(565, 276)
        Me.splitThreads.SplitterDistance = 188
        Me.splitThreads.TabIndex = 0
        '
        'SplitContainerThreads
        '
        Me.SplitContainerThreads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerThreads.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerThreads.IsSplitterFixed = True
        Me.SplitContainerThreads.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerThreads.Name = "SplitContainerThreads"
        Me.SplitContainerThreads.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerThreads.Panel1
        '
        Me.SplitContainerThreads.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainerThreads.Panel1.Controls.Add(Me.lblThreadResults)
        Me.SplitContainerThreads.Panel1.Controls.Add(Me.txtSearchThread)
        '
        'SplitContainerThreads.Panel2
        '
        Me.SplitContainerThreads.Panel2.Controls.Add(Me.lvThreads)
        Me.SplitContainerThreads.Size = New System.Drawing.Size(565, 188)
        Me.SplitContainerThreads.SplitterDistance = 25
        Me.SplitContainerThreads.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Search thread"
        '
        'lblThreadResults
        '
        Me.lblThreadResults.AutoSize = True
        Me.lblThreadResults.Location = New System.Drawing.Point(393, 5)
        Me.lblThreadResults.Name = "lblThreadResults"
        Me.lblThreadResults.Size = New System.Drawing.Size(52, 13)
        Me.lblThreadResults.TabIndex = 9
        Me.lblThreadResults.Text = "0 result(s)"
        '
        'txtSearchThread
        '
        Me.txtSearchThread.Location = New System.Drawing.Point(86, 2)
        Me.txtSearchThread.Name = "txtSearchThread"
        Me.txtSearchThread.Size = New System.Drawing.Size(298, 20)
        Me.txtSearchThread.TabIndex = 8
        '
        'lvThreads
        '
        Me.lvThreads.AllowColumnReorder = True
        Me.lvThreads.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader32, Me.ColumnHeader33, Me.ColumnHeader34, Me.ColumnHeader35, Me.ColumnHeader36, Me.ColumnHeader37, Me.ColumnHeader38})
        Me.lvThreads.ContextMenuStrip = Me.menuThread
        Me.lvThreads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvThreads.FullRowSelect = True
        ListViewGroup13.Header = "Threads"
        ListViewGroup13.Name = "gpOther"
        ListViewGroup14.Header = "Search results"
        ListViewGroup14.Name = "gpSearchResults"
        Me.lvThreads.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup13, ListViewGroup14})
        Me.lvThreads.HideSelection = False
        Me.lvThreads.Location = New System.Drawing.Point(0, 0)
        Me.lvThreads.Name = "lvThreads"
        Me.lvThreads.OverriddenDoubleBuffered = True
        Me.lvThreads.Size = New System.Drawing.Size(565, 159)
        Me.lvThreads.SmallImageList = Me.imgServices
        Me.lvThreads.TabIndex = 3
        Me.lvThreads.UseCompatibleStateImageBehavior = False
        Me.lvThreads.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader32
        '
        Me.ColumnHeader32.Text = "Id"
        '
        'ColumnHeader33
        '
        Me.ColumnHeader33.Text = "OwnerProcessId"
        Me.ColumnHeader33.Width = 150
        '
        'ColumnHeader34
        '
        Me.ColumnHeader34.Text = "Priority"
        Me.ColumnHeader34.Width = 100
        '
        'ColumnHeader35
        '
        Me.ColumnHeader35.Text = "State"
        Me.ColumnHeader35.Width = 70
        '
        'ColumnHeader36
        '
        Me.ColumnHeader36.Text = "WaitReason"
        Me.ColumnHeader36.Width = 100
        '
        'ColumnHeader37
        '
        Me.ColumnHeader37.Text = "StartTime"
        Me.ColumnHeader37.Width = 200
        '
        'ColumnHeader38
        '
        Me.ColumnHeader38.Text = "TotalProcessorTime"
        Me.ColumnHeader38.Width = 200
        '
        'menuThread
        '
        Me.menuThread.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem23, Me.ToolStripMenuItem24, Me.ToolStripMenuItem25, Me.ToolStripMenuItem26, Me.ToolStripMenuItem33, Me.SelectedAssociatedProcessToolStripMenuItem})
        Me.menuThread.Name = "menuProc"
        Me.menuThread.Size = New System.Drawing.Size(220, 136)
        '
        'ToolStripMenuItem23
        '
        Me.ToolStripMenuItem23.Image = Global.YAPM.My.Resources.Resources.kill
        Me.ToolStripMenuItem23.Name = "ToolStripMenuItem23"
        Me.ToolStripMenuItem23.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem23.Text = "Terminate"
        '
        'ToolStripMenuItem24
        '
        Me.ToolStripMenuItem24.Image = CType(resources.GetObject("ToolStripMenuItem24.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem24.Name = "ToolStripMenuItem24"
        Me.ToolStripMenuItem24.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem24.Text = "Suspend"
        '
        'ToolStripMenuItem25
        '
        Me.ToolStripMenuItem25.Image = CType(resources.GetObject("ToolStripMenuItem25.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem25.Name = "ToolStripMenuItem25"
        Me.ToolStripMenuItem25.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem25.Text = "Resume"
        '
        'ToolStripMenuItem26
        '
        Me.ToolStripMenuItem26.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem27, Me.LowestToolStripMenuItem, Me.ToolStripMenuItem28, Me.ToolStripMenuItem29, Me.ToolStripMenuItem30, Me.ToolStripMenuItem31, Me.ToolStripMenuItem32})
        Me.ToolStripMenuItem26.Name = "ToolStripMenuItem26"
        Me.ToolStripMenuItem26.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem26.Text = "Priotiy"
        '
        'ToolStripMenuItem27
        '
        Me.ToolStripMenuItem27.Image = CType(resources.GetObject("ToolStripMenuItem27.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem27.Name = "ToolStripMenuItem27"
        Me.ToolStripMenuItem27.Size = New System.Drawing.Size(151, 22)
        Me.ToolStripMenuItem27.Text = "Idle"
        '
        'LowestToolStripMenuItem
        '
        Me.LowestToolStripMenuItem.Image = CType(resources.GetObject("LowestToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LowestToolStripMenuItem.Name = "LowestToolStripMenuItem"
        Me.LowestToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
        Me.LowestToolStripMenuItem.Text = "Lowest"
        '
        'ToolStripMenuItem28
        '
        Me.ToolStripMenuItem28.Image = CType(resources.GetObject("ToolStripMenuItem28.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem28.Name = "ToolStripMenuItem28"
        Me.ToolStripMenuItem28.Size = New System.Drawing.Size(151, 22)
        Me.ToolStripMenuItem28.Text = "Below Normal"
        '
        'ToolStripMenuItem29
        '
        Me.ToolStripMenuItem29.Image = CType(resources.GetObject("ToolStripMenuItem29.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem29.Name = "ToolStripMenuItem29"
        Me.ToolStripMenuItem29.Size = New System.Drawing.Size(151, 22)
        Me.ToolStripMenuItem29.Text = "Normal"
        '
        'ToolStripMenuItem30
        '
        Me.ToolStripMenuItem30.Image = CType(resources.GetObject("ToolStripMenuItem30.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem30.Name = "ToolStripMenuItem30"
        Me.ToolStripMenuItem30.Size = New System.Drawing.Size(151, 22)
        Me.ToolStripMenuItem30.Text = "Above Normal"
        '
        'ToolStripMenuItem31
        '
        Me.ToolStripMenuItem31.Image = CType(resources.GetObject("ToolStripMenuItem31.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem31.Name = "ToolStripMenuItem31"
        Me.ToolStripMenuItem31.Size = New System.Drawing.Size(151, 22)
        Me.ToolStripMenuItem31.Text = "Highest"
        '
        'ToolStripMenuItem32
        '
        Me.ToolStripMenuItem32.Image = CType(resources.GetObject("ToolStripMenuItem32.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem32.Name = "ToolStripMenuItem32"
        Me.ToolStripMenuItem32.Size = New System.Drawing.Size(151, 22)
        Me.ToolStripMenuItem32.Text = "Time Critical"
        '
        'ToolStripMenuItem33
        '
        Me.ToolStripMenuItem33.Enabled = False
        Me.ToolStripMenuItem33.Name = "ToolStripMenuItem33"
        Me.ToolStripMenuItem33.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem33.Text = "Set affinity..."
        '
        'SelectedAssociatedProcessToolStripMenuItem
        '
        Me.SelectedAssociatedProcessToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.exe
        Me.SelectedAssociatedProcessToolStripMenuItem.Name = "SelectedAssociatedProcessToolStripMenuItem"
        Me.SelectedAssociatedProcessToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.SelectedAssociatedProcessToolStripMenuItem.Text = "Selected associated process"
        '
        'rtb4
        '
        Me.rtb4.AutoWordSelection = True
        Me.rtb4.BackColor = System.Drawing.Color.White
        Me.rtb4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb4.HideSelection = False
        Me.rtb4.Location = New System.Drawing.Point(0, 0)
        Me.rtb4.Name = "rtb4"
        Me.rtb4.ReadOnly = True
        Me.rtb4.Size = New System.Drawing.Size(565, 84)
        Me.rtb4.TabIndex = 7
        Me.rtb4.Text = "Click on a thread to get additionnal informations"
        '
        'panelMain10
        '
        Me.panelMain10.Controls.Add(Me.splitContainerWindows)
        Me.panelMain10.Location = New System.Drawing.Point(127, 171)
        Me.panelMain10.Name = "panelMain10"
        Me.panelMain10.Size = New System.Drawing.Size(565, 276)
        Me.panelMain10.TabIndex = 52
        Me.panelMain10.Visible = False
        '
        'splitContainerWindows
        '
        Me.splitContainerWindows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainerWindows.Location = New System.Drawing.Point(0, 0)
        Me.splitContainerWindows.Name = "splitContainerWindows"
        Me.splitContainerWindows.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainerWindows.Panel1
        '
        Me.splitContainerWindows.Panel1.Controls.Add(Me.SplitContainerWindows2)
        '
        'splitContainerWindows.Panel2
        '
        Me.splitContainerWindows.Panel2.Controls.Add(Me.rtb5)
        Me.splitContainerWindows.Size = New System.Drawing.Size(565, 276)
        Me.splitContainerWindows.SplitterDistance = 172
        Me.splitContainerWindows.TabIndex = 0
        '
        'SplitContainerWindows2
        '
        Me.SplitContainerWindows2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerWindows2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerWindows2.IsSplitterFixed = True
        Me.SplitContainerWindows2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerWindows2.Name = "SplitContainerWindows2"
        Me.SplitContainerWindows2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerWindows2.Panel1
        '
        Me.SplitContainerWindows2.Panel1.Controls.Add(Me.chkAllWindows)
        Me.SplitContainerWindows2.Panel1.Controls.Add(Me.Label10)
        Me.SplitContainerWindows2.Panel1.Controls.Add(Me.lblWindowsCount)
        Me.SplitContainerWindows2.Panel1.Controls.Add(Me.txtSearchWindow)
        '
        'SplitContainerWindows2.Panel2
        '
        Me.SplitContainerWindows2.Panel2.Controls.Add(Me.lvWindows)
        Me.SplitContainerWindows2.Size = New System.Drawing.Size(565, 172)
        Me.SplitContainerWindows2.SplitterDistance = 25
        Me.SplitContainerWindows2.TabIndex = 0
        '
        'chkAllWindows
        '
        Me.chkAllWindows.AutoSize = True
        Me.chkAllWindows.Checked = True
        Me.chkAllWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllWindows.Location = New System.Drawing.Point(543, 4)
        Me.chkAllWindows.Name = "chkAllWindows"
        Me.chkAllWindows.Size = New System.Drawing.Size(179, 17)
        Me.chkAllWindows.TabIndex = 11
        Me.chkAllWindows.Text = "Display windows without caption"
        Me.chkAllWindows.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Search window"
        '
        'lblWindowsCount
        '
        Me.lblWindowsCount.AutoSize = True
        Me.lblWindowsCount.Location = New System.Drawing.Point(393, 5)
        Me.lblWindowsCount.Name = "lblWindowsCount"
        Me.lblWindowsCount.Size = New System.Drawing.Size(52, 13)
        Me.lblWindowsCount.TabIndex = 9
        Me.lblWindowsCount.Text = "0 result(s)"
        '
        'txtSearchWindow
        '
        Me.txtSearchWindow.Location = New System.Drawing.Point(86, 2)
        Me.txtSearchWindow.Name = "txtSearchWindow"
        Me.txtSearchWindow.Size = New System.Drawing.Size(298, 20)
        Me.txtSearchWindow.TabIndex = 8
        '
        'lvWindows
        '
        Me.lvWindows.AllowColumnReorder = True
        Me.lvWindows.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader30, Me.ColumnHeader31, Me.ColumnHeader39, Me.ColumnHeader40, Me.ColumnHeader41, Me.ColumnHeader42})
        Me.lvWindows.ContextMenuStrip = Me.menuWindow
        Me.lvWindows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvWindows.FullRowSelect = True
        ListViewGroup15.Header = "Windows"
        ListViewGroup15.Name = "gpOther"
        ListViewGroup16.Header = "Search results"
        ListViewGroup16.Name = "gpSearchResults"
        Me.lvWindows.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup15, ListViewGroup16})
        Me.lvWindows.HideSelection = False
        Me.lvWindows.Location = New System.Drawing.Point(0, 0)
        Me.lvWindows.Name = "lvWindows"
        Me.lvWindows.OverriddenDoubleBuffered = True
        Me.lvWindows.Size = New System.Drawing.Size(565, 143)
        Me.lvWindows.SmallImageList = Me.imgWindows
        Me.lvWindows.TabIndex = 5
        Me.lvWindows.UseCompatibleStateImageBehavior = False
        Me.lvWindows.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader30
        '
        Me.ColumnHeader30.Text = "Id"
        Me.ColumnHeader30.Width = 100
        '
        'ColumnHeader31
        '
        Me.ColumnHeader31.Text = "Process"
        Me.ColumnHeader31.Width = 120
        '
        'ColumnHeader39
        '
        Me.ColumnHeader39.Text = "Caption"
        Me.ColumnHeader39.Width = 350
        '
        'ColumnHeader40
        '
        Me.ColumnHeader40.Text = "IsTask"
        '
        'ColumnHeader41
        '
        Me.ColumnHeader41.Text = "Enabled"
        '
        'ColumnHeader42
        '
        Me.ColumnHeader42.Text = "Visible"
        '
        'menuWindow
        '
        Me.menuWindow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem34})
        Me.menuWindow.Name = "menuProc"
        Me.menuWindow.Size = New System.Drawing.Size(207, 26)
        '
        'ToolStripMenuItem34
        '
        Me.ToolStripMenuItem34.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem34.Name = "ToolStripMenuItem34"
        Me.ToolStripMenuItem34.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem34.Text = "&Select associated process"
        '
        'imgWindows
        '
        Me.imgWindows.ImageStream = CType(resources.GetObject("imgWindows.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgWindows.TransparentColor = System.Drawing.Color.Transparent
        Me.imgWindows.Images.SetKeyName(0, "noIcon")
        '
        'rtb5
        '
        Me.rtb5.AutoWordSelection = True
        Me.rtb5.BackColor = System.Drawing.Color.White
        Me.rtb5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb5.HideSelection = False
        Me.rtb5.Location = New System.Drawing.Point(0, 0)
        Me.rtb5.Name = "rtb5"
        Me.rtb5.ReadOnly = True
        Me.rtb5.Size = New System.Drawing.Size(565, 100)
        Me.rtb5.TabIndex = 8
        Me.rtb5.Text = "Click on an item to get additionnal informations"
        '
        'panelMain11
        '
        Me.panelMain11.Controls.Add(Me.splitModule)
        Me.panelMain11.Location = New System.Drawing.Point(120, 163)
        Me.panelMain11.Name = "panelMain11"
        Me.panelMain11.Size = New System.Drawing.Size(565, 276)
        Me.panelMain11.TabIndex = 53
        Me.panelMain11.Visible = False
        '
        'splitModule
        '
        Me.splitModule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitModule.Location = New System.Drawing.Point(0, 0)
        Me.splitModule.Name = "splitModule"
        Me.splitModule.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitModule.Panel1
        '
        Me.splitModule.Panel1.Controls.Add(Me.SplitContainerModules)
        '
        'splitModule.Panel2
        '
        Me.splitModule.Panel2.Controls.Add(Me.rtb6)
        Me.splitModule.Size = New System.Drawing.Size(565, 276)
        Me.splitModule.SplitterDistance = 172
        Me.splitModule.TabIndex = 0
        '
        'SplitContainerModules
        '
        Me.SplitContainerModules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerModules.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerModules.IsSplitterFixed = True
        Me.SplitContainerModules.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerModules.Name = "SplitContainerModules"
        Me.SplitContainerModules.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerModules.Panel1
        '
        Me.SplitContainerModules.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainerModules.Panel1.Controls.Add(Me.lblModulesCount)
        Me.SplitContainerModules.Panel1.Controls.Add(Me.txtSearchModule)
        '
        'SplitContainerModules.Panel2
        '
        Me.SplitContainerModules.Panel2.Controls.Add(Me.lvModules)
        Me.SplitContainerModules.Size = New System.Drawing.Size(565, 172)
        Me.SplitContainerModules.SplitterDistance = 25
        Me.SplitContainerModules.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Search module"
        '
        'lblModulesCount
        '
        Me.lblModulesCount.AutoSize = True
        Me.lblModulesCount.Location = New System.Drawing.Point(393, 5)
        Me.lblModulesCount.Name = "lblModulesCount"
        Me.lblModulesCount.Size = New System.Drawing.Size(52, 13)
        Me.lblModulesCount.TabIndex = 6
        Me.lblModulesCount.Text = "0 result(s)"
        '
        'txtSearchModule
        '
        Me.txtSearchModule.Location = New System.Drawing.Point(86, 2)
        Me.txtSearchModule.Name = "txtSearchModule"
        Me.txtSearchModule.Size = New System.Drawing.Size(298, 20)
        Me.txtSearchModule.TabIndex = 5
        '
        'lvModules
        '
        Me.lvModules.AllowColumnReorder = True
        Me.lvModules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader29, Me.ColumnHeader43, Me.ColumnHeader44, Me.ColumnHeader45, Me.ColumnHeader46, Me.ColumnHeader18})
        Me.lvModules.ContextMenuStrip = Me.menuModule
        Me.lvModules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvModules.FullRowSelect = True
        ListViewGroup17.Header = "Modules"
        ListViewGroup17.Name = "gpOther"
        ListViewGroup18.Header = "Search result"
        ListViewGroup18.Name = "gpSearchResults"
        Me.lvModules.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup17, ListViewGroup18})
        Me.lvModules.HideSelection = False
        Me.lvModules.Location = New System.Drawing.Point(0, 0)
        Me.lvModules.Name = "lvModules"
        Me.lvModules.OverriddenDoubleBuffered = True
        Me.lvModules.Size = New System.Drawing.Size(565, 143)
        Me.lvModules.SmallImageList = Me.imgSearch
        Me.lvModules.TabIndex = 6
        Me.lvModules.UseCompatibleStateImageBehavior = False
        Me.lvModules.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader29
        '
        Me.ColumnHeader29.Text = "Name"
        Me.ColumnHeader29.Width = 90
        '
        'ColumnHeader43
        '
        Me.ColumnHeader43.Text = "Version"
        Me.ColumnHeader43.Width = 85
        '
        'ColumnHeader44
        '
        Me.ColumnHeader44.Text = "Description"
        Me.ColumnHeader44.Width = 210
        '
        'ColumnHeader45
        '
        Me.ColumnHeader45.Text = "CompanyName"
        Me.ColumnHeader45.Width = 150
        '
        'ColumnHeader46
        '
        Me.ColumnHeader46.Text = "Path"
        Me.ColumnHeader46.Width = 300
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "Process"
        Me.ColumnHeader18.Width = 100
        '
        'menuModule
        '
        Me.menuModule.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem35, Me.ToolStripSeparator4, Me.ShowFileDetailsToolStripMenuItem, Me.ToolStripMenuItem36, Me.ToolStripMenuItem39, Me.GoogleSearchToolStripMenuItem2})
        Me.menuModule.Name = "menuProc"
        Me.menuModule.Size = New System.Drawing.Size(207, 104)
        '
        'ToolStripMenuItem35
        '
        Me.ToolStripMenuItem35.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem35.Name = "ToolStripMenuItem35"
        Me.ToolStripMenuItem35.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem35.Text = "&Select associated process"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(203, 6)
        '
        'ShowFileDetailsToolStripMenuItem
        '
        Me.ShowFileDetailsToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.magnify16
        Me.ShowFileDetailsToolStripMenuItem.Name = "ShowFileDetailsToolStripMenuItem"
        Me.ShowFileDetailsToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.ShowFileDetailsToolStripMenuItem.Text = "Show file details"
        '
        'ToolStripMenuItem36
        '
        Me.ToolStripMenuItem36.Image = Global.YAPM.My.Resources.Resources.kill
        Me.ToolStripMenuItem36.Name = "ToolStripMenuItem36"
        Me.ToolStripMenuItem36.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem36.Text = "Unload module"
        '
        'ToolStripMenuItem39
        '
        Me.ToolStripMenuItem39.Name = "ToolStripMenuItem39"
        Me.ToolStripMenuItem39.Size = New System.Drawing.Size(203, 6)
        '
        'GoogleSearchToolStripMenuItem2
        '
        Me.GoogleSearchToolStripMenuItem2.Image = Global.YAPM.My.Resources.Resources.ie7_16
        Me.GoogleSearchToolStripMenuItem2.Name = "GoogleSearchToolStripMenuItem2"
        Me.GoogleSearchToolStripMenuItem2.Size = New System.Drawing.Size(206, 22)
        Me.GoogleSearchToolStripMenuItem2.Text = "Google search"
        '
        'rtb6
        '
        Me.rtb6.AutoWordSelection = True
        Me.rtb6.BackColor = System.Drawing.Color.White
        Me.rtb6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb6.HideSelection = False
        Me.rtb6.Location = New System.Drawing.Point(0, 0)
        Me.rtb6.Name = "rtb6"
        Me.rtb6.ReadOnly = True
        Me.rtb6.Size = New System.Drawing.Size(565, 100)
        Me.rtb6.TabIndex = 8
        Me.rtb6.Text = "Click on an item to get additionnal informations"
        '
        'mainMenu
        '
        Me.mainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowLogToolStripMenuItem, Me.ShowSystemInformatoToolStripMenuItem, Me.MinimizeToTrayToolStripMenuItem, Me.ToolStripMenuItem40, Me.ExitToolStripMenuItem})
        Me.mainMenu.Name = "mainMenu"
        Me.mainMenu.Size = New System.Drawing.Size(179, 98)
        '
        'ShowLogToolStripMenuItem
        '
        Me.ShowLogToolStripMenuItem.Image = CType(resources.GetObject("ShowLogToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowLogToolStripMenuItem.Name = "ShowLogToolStripMenuItem"
        Me.ShowLogToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.ShowLogToolStripMenuItem.Text = "Show log"
        '
        'ShowSystemInformatoToolStripMenuItem
        '
        Me.ShowSystemInformatoToolStripMenuItem.Image = CType(resources.GetObject("ShowSystemInformatoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowSystemInformatoToolStripMenuItem.Name = "ShowSystemInformatoToolStripMenuItem"
        Me.ShowSystemInformatoToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.ShowSystemInformatoToolStripMenuItem.Text = "System information"
        '
        'MinimizeToTrayToolStripMenuItem
        '
        Me.MinimizeToTrayToolStripMenuItem.Image = CType(resources.GetObject("MinimizeToTrayToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MinimizeToTrayToolStripMenuItem.Name = "MinimizeToTrayToolStripMenuItem"
        Me.MinimizeToTrayToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.MinimizeToTrayToolStripMenuItem.Text = "Minimize to tray"
        '
        'ToolStripMenuItem40
        '
        Me.ToolStripMenuItem40.Name = "ToolStripMenuItem40"
        Me.ToolStripMenuItem40.Size = New System.Drawing.Size(175, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'cmdTray
        '
        Me.cmdTray.ContextMenuStrip = Me.mainMenu
        Me.cmdTray.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTray.Image = Global.YAPM.My.Resources.Resources.down
        Me.cmdTray.Location = New System.Drawing.Point(5, 2)
        Me.cmdTray.Name = "cmdTray"
        Me.cmdTray.Size = New System.Drawing.Size(38, 20)
        Me.cmdTray.TabIndex = 45
        Me.cmdTray.UseVisualStyleBackColor = True
        '
        'timerProcPerf
        '
        Me.timerProcPerf.Interval = 1000
        '
        'menuTasks
        '
        Me.menuTasks.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowWindowToolStripMenuItem, Me.MaximizeWindowToolStripMenuItem, Me.MinimizeWindowToolStripMenuItem, Me.ToolStripMenuItem46, Me.EndTaskToolStripMenuItem, Me.ToolStripMenuItem47, Me.ToolStripMenuItem45, Me.SelectWindowInWindowsTabToolStripMenuItem})
        Me.menuTasks.Name = "menuProc"
        Me.menuTasks.Size = New System.Drawing.Size(236, 148)
        '
        'ShowWindowToolStripMenuItem
        '
        Me.ShowWindowToolStripMenuItem.Image = CType(resources.GetObject("ShowWindowToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowWindowToolStripMenuItem.Name = "ShowWindowToolStripMenuItem"
        Me.ShowWindowToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.ShowWindowToolStripMenuItem.Text = "Show window"
        '
        'MaximizeWindowToolStripMenuItem
        '
        Me.MaximizeWindowToolStripMenuItem.Name = "MaximizeWindowToolStripMenuItem"
        Me.MaximizeWindowToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.MaximizeWindowToolStripMenuItem.Text = "Maximize window"
        '
        'MinimizeWindowToolStripMenuItem
        '
        Me.MinimizeWindowToolStripMenuItem.Name = "MinimizeWindowToolStripMenuItem"
        Me.MinimizeWindowToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.MinimizeWindowToolStripMenuItem.Text = "Minimize window"
        '
        'ToolStripMenuItem46
        '
        Me.ToolStripMenuItem46.Name = "ToolStripMenuItem46"
        Me.ToolStripMenuItem46.Size = New System.Drawing.Size(232, 6)
        '
        'EndTaskToolStripMenuItem
        '
        Me.EndTaskToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.kill
        Me.EndTaskToolStripMenuItem.Name = "EndTaskToolStripMenuItem"
        Me.EndTaskToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.EndTaskToolStripMenuItem.Text = "End task"
        '
        'ToolStripMenuItem47
        '
        Me.ToolStripMenuItem47.Name = "ToolStripMenuItem47"
        Me.ToolStripMenuItem47.Size = New System.Drawing.Size(232, 6)
        '
        'ToolStripMenuItem45
        '
        Me.ToolStripMenuItem45.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem45.Name = "ToolStripMenuItem45"
        Me.ToolStripMenuItem45.Size = New System.Drawing.Size(235, 22)
        Me.ToolStripMenuItem45.Text = "&Select associated process"
        '
        'SelectWindowInWindowsTabToolStripMenuItem
        '
        Me.SelectWindowInWindowsTabToolStripMenuItem.Name = "SelectWindowInWindowsTabToolStripMenuItem"
        Me.SelectWindowInWindowsTabToolStripMenuItem.Size = New System.Drawing.Size(235, 22)
        Me.SelectWindowInWindowsTabToolStripMenuItem.Text = "Select window in Windows tab"
        '
        'panelMain13
        '
        Me.panelMain13.Controls.Add(Me.SplitContainerTask)
        Me.panelMain13.Location = New System.Drawing.Point(139, 163)
        Me.panelMain13.Name = "panelMain13"
        Me.panelMain13.Size = New System.Drawing.Size(565, 276)
        Me.panelMain13.TabIndex = 55
        '
        'SplitContainerTask
        '
        Me.SplitContainerTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTask.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerTask.IsSplitterFixed = True
        Me.SplitContainerTask.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerTask.Name = "SplitContainerTask"
        Me.SplitContainerTask.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerTask.Panel1
        '
        Me.SplitContainerTask.Panel1.Controls.Add(Me.Label19)
        Me.SplitContainerTask.Panel1.Controls.Add(Me.lblTaskCountResult)
        Me.SplitContainerTask.Panel1.Controls.Add(Me.txtSearchTask)
        '
        'SplitContainerTask.Panel2
        '
        Me.SplitContainerTask.Panel2.Controls.Add(Me.lvTask)
        Me.SplitContainerTask.Size = New System.Drawing.Size(565, 276)
        Me.SplitContainerTask.SplitterDistance = 25
        Me.SplitContainerTask.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 5)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 13)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "Search task"
        '
        'lblTaskCountResult
        '
        Me.lblTaskCountResult.AutoSize = True
        Me.lblTaskCountResult.Location = New System.Drawing.Point(393, 5)
        Me.lblTaskCountResult.Name = "lblTaskCountResult"
        Me.lblTaskCountResult.Size = New System.Drawing.Size(52, 13)
        Me.lblTaskCountResult.TabIndex = 12
        Me.lblTaskCountResult.Text = "0 result(s)"
        '
        'txtSearchTask
        '
        Me.txtSearchTask.Location = New System.Drawing.Point(72, 2)
        Me.txtSearchTask.Name = "txtSearchTask"
        Me.txtSearchTask.Size = New System.Drawing.Size(312, 20)
        Me.txtSearchTask.TabIndex = 11
        '
        'lvTask
        '
        Me.lvTask.AllowColumnReorder = True
        Me.lvTask.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader62, Me.ColumnHeader63, Me.ColumnHeader64})
        Me.lvTask.ContextMenuStrip = Me.menuTasks
        Me.lvTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTask.FullRowSelect = True
        ListViewGroup19.Header = "Tasks"
        ListViewGroup19.Name = "gpOther"
        ListViewGroup20.Header = "Search result"
        ListViewGroup20.Name = "gpSearch"
        Me.lvTask.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup19, ListViewGroup20})
        Me.lvTask.HideSelection = False
        Me.lvTask.Location = New System.Drawing.Point(0, 0)
        Me.lvTask.Name = "lvTask"
        Me.lvTask.OverriddenDoubleBuffered = True
        Me.lvTask.Size = New System.Drawing.Size(565, 247)
        Me.lvTask.SmallImageList = Me.imgTask
        Me.lvTask.TabIndex = 3
        Me.lvTask.UseCompatibleStateImageBehavior = False
        Me.lvTask.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader62
        '
        Me.ColumnHeader62.Text = "Name"
        Me.ColumnHeader62.Width = 400
        '
        'ColumnHeader63
        '
        Me.ColumnHeader63.Text = "CpuUsage"
        Me.ColumnHeader63.Width = 170
        '
        'ColumnHeader64
        '
        Me.ColumnHeader64.Text = "Process"
        Me.ColumnHeader64.Width = 170
        '
        'imgTask
        '
        Me.imgTask.ImageStream = CType(resources.GetObject("imgTask.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTask.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTask.Images.SetKeyName(0, "noIcon")
        '
        'timerTask
        '
        Me.timerTask.Interval = 1000
        '
        'panelMain14
        '
        Me.panelMain14.Controls.Add(Me.lvNetwork)
        Me.panelMain14.Location = New System.Drawing.Point(147, 171)
        Me.panelMain14.Name = "panelMain14"
        Me.panelMain14.Size = New System.Drawing.Size(565, 276)
        Me.panelMain14.TabIndex = 56
        Me.panelMain14.Visible = False
        '
        'lvNetwork
        '
        Me.lvNetwork.AllowColumnReorder = True
        Me.lvNetwork.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader66, Me.ColumnHeader67, Me.ColumnHeader68, Me.ColumnHeader69})
        Me.lvNetwork.ContextMenuStrip = Me.menuNetwork
        Me.lvNetwork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvNetwork.FullRowSelect = True
        Me.lvNetwork.HideSelection = False
        Me.lvNetwork.Location = New System.Drawing.Point(0, 0)
        Me.lvNetwork.Name = "lvNetwork"
        Me.lvNetwork.OverriddenDoubleBuffered = True
        Me.lvNetwork.Size = New System.Drawing.Size(565, 276)
        Me.lvNetwork.TabIndex = 4
        Me.lvNetwork.UseCompatibleStateImageBehavior = False
        Me.lvNetwork.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader66
        '
        Me.ColumnHeader66.Text = "Local"
        Me.ColumnHeader66.Width = 200
        '
        'ColumnHeader67
        '
        Me.ColumnHeader67.Text = "Remote"
        Me.ColumnHeader67.Width = 250
        '
        'ColumnHeader68
        '
        Me.ColumnHeader68.Text = "Protocol"
        Me.ColumnHeader68.Width = 100
        '
        'ColumnHeader69
        '
        Me.ColumnHeader69.Text = "State"
        Me.ColumnHeader69.Width = 150
        '
        'menuNetwork
        '
        Me.menuNetwork.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem48})
        Me.menuNetwork.Name = "menuProc"
        Me.menuNetwork.Size = New System.Drawing.Size(207, 26)
        '
        'ToolStripMenuItem48
        '
        Me.ToolStripMenuItem48.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem48.Name = "ToolStripMenuItem48"
        Me.ToolStripMenuItem48.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem48.Text = "&Select associated process"
        '
        'timerTrayIcon
        '
        Me.timerTrayIcon.Enabled = True
        Me.timerTrayIcon.Interval = 1000
        '
        'menuProcMem
        '
        Me.menuProcMem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem49, Me.JumpToPEBAddressToolStripMenuItem})
        Me.menuProcMem.Name = "menuProc"
        Me.menuProcMem.Size = New System.Drawing.Size(184, 70)
        '
        'ToolStripMenuItem49
        '
        Me.ToolStripMenuItem49.Image = Global.YAPM.My.Resources.Resources.magnify16
        Me.ToolStripMenuItem49.Name = "ToolStripMenuItem49"
        Me.ToolStripMenuItem49.Size = New System.Drawing.Size(183, 22)
        Me.ToolStripMenuItem49.Text = "View memory"
        '
        'JumpToPEBAddressToolStripMenuItem
        '
        Me.JumpToPEBAddressToolStripMenuItem.Name = "JumpToPEBAddressToolStripMenuItem"
        Me.JumpToPEBAddressToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.JumpToPEBAddressToolStripMenuItem.Text = "Jump to PEB address"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(843, 603)
        Me.Controls.Add(Me.panelMain)
        Me.Controls.Add(Me.panelMain2)
        Me.Controls.Add(Me.panelMain9)
        Me.Controls.Add(Me.panelMain4)
        Me.Controls.Add(Me.panelMain3)
        Me.Controls.Add(Me.panelMain6)
        Me.Controls.Add(Me.panelMenu)
        Me.Controls.Add(Me.panelMain10)
        Me.Controls.Add(Me.panelMain8)
        Me.Controls.Add(Me.panelMenu2)
        Me.Controls.Add(Me.cmdTray)
        Me.Controls.Add(Me.Ribbon)
        Me.Controls.Add(Me.panelMain11)
        Me.Controls.Add(Me.panelMain5)
        Me.Controls.Add(Me.panelMain7)
        Me.Controls.Add(Me.panelMain14)
        Me.Controls.Add(Me.panelMain13)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(840, 589)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Yet Another Process Monitor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.panelMain.ResumeLayout(False)
        Me.SplitContainerProcess.Panel1.ResumeLayout(False)
        Me.SplitContainerProcess.Panel2.ResumeLayout(False)
        Me.SplitContainerProcess.ResumeLayout(False)
        Me.SplitContainerTvLv.Panel1.ResumeLayout(False)
        Me.SplitContainerTvLv.Panel2.ResumeLayout(False)
        Me.SplitContainerTvLv.ResumeLayout(False)
        Me.menuProc.ResumeLayout(False)
        Me.tabProcess.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gpProcGeneralFile.ResumeLayout(False)
        Me.gpProcGeneralFile.PerformLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuCopyPctSmall.ResumeLayout(False)
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.menuCopyPctbig.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.splitPerformances.Panel1.ResumeLayout(False)
        Me.splitPerformances.Panel2.ResumeLayout(False)
        Me.splitPerformances.ResumeLayout(False)
        CType(Me.graphCPU, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitPerformance2.Panel1.ResumeLayout(False)
        Me.splitPerformance2.Panel2.ResumeLayout(False)
        Me.splitPerformance2.ResumeLayout(False)
        CType(Me.graphMemory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.graphIO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.tabProcessToken.ResumeLayout(False)
        Me.tabProcessTokenPagePrivileges.ResumeLayout(False)
        Me.menuPrivileges.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.SplitContainerInfoProcess.Panel1.ResumeLayout(False)
        Me.SplitContainerInfoProcess.Panel1.PerformLayout()
        Me.SplitContainerInfoProcess.Panel2.ResumeLayout(False)
        Me.SplitContainerInfoProcess.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.menuProcServ.ResumeLayout(False)
        Me.tabNetwork.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.SplitContainerStrings.Panel1.ResumeLayout(False)
        Me.SplitContainerStrings.Panel2.ResumeLayout(False)
        Me.SplitContainerStrings.Panel2.PerformLayout()
        Me.SplitContainerStrings.ResumeLayout(False)
        Me.TabPage9.ResumeLayout(False)
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        Me.panelMain2.ResumeLayout(False)
        Me.splitServices.Panel1.ResumeLayout(False)
        Me.splitServices.Panel2.ResumeLayout(False)
        Me.splitServices.ResumeLayout(False)
        Me.menuService.ResumeLayout(False)
        Me.splitServices2.Panel1.ResumeLayout(False)
        Me.splitServices2.Panel1.PerformLayout()
        Me.splitServices2.Panel2.ResumeLayout(False)
        Me.splitServices2.ResumeLayout(False)
        Me.splitServices3.Panel1.ResumeLayout(False)
        Me.splitServices3.Panel2.ResumeLayout(False)
        Me.splitServices3.ResumeLayout(False)
        Me.splitServices4.Panel1.ResumeLayout(False)
        Me.splitServices4.Panel2.ResumeLayout(False)
        Me.splitServices4.ResumeLayout(False)
        Me.panelMain4.ResumeLayout(False)
        Me.panelMain3.ResumeLayout(False)
        Me.menuTooltip.ResumeLayout(False)
        Me.panelMenu2.ResumeLayout(False)
        Me.panelMenu2.PerformLayout()
        Me.panelMain5.ResumeLayout(False)
        Me.panelMain5.PerformLayout()
        Me.fileSplitContainer.Panel1.ResumeLayout(False)
        Me.fileSplitContainer.Panel2.ResumeLayout(False)
        Me.fileSplitContainer.ResumeLayout(False)
        Me.gpFileAttributes.ResumeLayout(False)
        Me.gpFileAttributes.PerformLayout()
        Me.gpFileDates.ResumeLayout(False)
        Me.gpFileDates.PerformLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuFileCopyPctSmall.ResumeLayout(False)
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuFileCopyPctBig.ResumeLayout(False)
        Me.panelMain6.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.PerformLayout()
        Me.SplitContainerSearch.Panel2.ResumeLayout(False)
        Me.SplitContainerSearch.ResumeLayout(False)
        Me.menuSearch.ResumeLayout(False)
        Me.panelMain7.ResumeLayout(False)
        Me.SplitContainerHandles.Panel1.ResumeLayout(False)
        Me.SplitContainerHandles.Panel1.PerformLayout()
        Me.SplitContainerHandles.Panel2.ResumeLayout(False)
        Me.SplitContainerHandles.ResumeLayout(False)
        Me.menuHandles.ResumeLayout(False)
        Me.panelMain8.ResumeLayout(False)
        Me.splitMonitor.Panel1.ResumeLayout(False)
        Me.splitMonitor.Panel2.ResumeLayout(False)
        Me.splitMonitor.ResumeLayout(False)
        Me.splitMonitor2.Panel1.ResumeLayout(False)
        Me.splitMonitor2.Panel1.PerformLayout()
        Me.splitMonitor2.Panel2.ResumeLayout(False)
        Me.splitMonitor2.ResumeLayout(False)
        Me.splitMonitor3.Panel1.ResumeLayout(False)
        Me.splitMonitor3.Panel2.ResumeLayout(False)
        Me.splitMonitor3.Panel2.PerformLayout()
        Me.splitMonitor3.ResumeLayout(False)
        Me.splitMonitor4.Panel2.ResumeLayout(False)
        Me.splitMonitor4.ResumeLayout(False)
        CType(Me.graphMonitor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMain9.ResumeLayout(False)
        Me.splitThreads.Panel1.ResumeLayout(False)
        Me.splitThreads.Panel2.ResumeLayout(False)
        Me.splitThreads.ResumeLayout(False)
        Me.SplitContainerThreads.Panel1.ResumeLayout(False)
        Me.SplitContainerThreads.Panel1.PerformLayout()
        Me.SplitContainerThreads.Panel2.ResumeLayout(False)
        Me.SplitContainerThreads.ResumeLayout(False)
        Me.menuThread.ResumeLayout(False)
        Me.panelMain10.ResumeLayout(False)
        Me.splitContainerWindows.Panel1.ResumeLayout(False)
        Me.splitContainerWindows.Panel2.ResumeLayout(False)
        Me.splitContainerWindows.ResumeLayout(False)
        Me.SplitContainerWindows2.Panel1.ResumeLayout(False)
        Me.SplitContainerWindows2.Panel1.PerformLayout()
        Me.SplitContainerWindows2.Panel2.ResumeLayout(False)
        Me.SplitContainerWindows2.ResumeLayout(False)
        Me.menuWindow.ResumeLayout(False)
        Me.panelMain11.ResumeLayout(False)
        Me.splitModule.Panel1.ResumeLayout(False)
        Me.splitModule.Panel2.ResumeLayout(False)
        Me.splitModule.ResumeLayout(False)
        Me.SplitContainerModules.Panel1.ResumeLayout(False)
        Me.SplitContainerModules.Panel1.PerformLayout()
        Me.SplitContainerModules.Panel2.ResumeLayout(False)
        Me.SplitContainerModules.ResumeLayout(False)
        Me.menuModule.ResumeLayout(False)
        Me.mainMenu.ResumeLayout(False)
        Me.menuTasks.ResumeLayout(False)
        Me.panelMain13.ResumeLayout(False)
        Me.SplitContainerTask.Panel1.ResumeLayout(False)
        Me.SplitContainerTask.Panel1.PerformLayout()
        Me.SplitContainerTask.Panel2.ResumeLayout(False)
        Me.SplitContainerTask.ResumeLayout(False)
        Me.panelMain14.ResumeLayout(False)
        Me.menuNetwork.ResumeLayout(False)
        Me.menuProcMem.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelMain As System.Windows.Forms.Panel
    Friend WithEvents panelMenu As System.Windows.Forms.Panel
    Friend WithEvents timerProcess As System.Windows.Forms.Timer
    Friend WithEvents imgProcess As System.Windows.Forms.ImageList
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents panelMain2 As System.Windows.Forms.Panel
    Friend WithEvents panelMain4 As System.Windows.Forms.Panel
    Friend WithEvents panelMain3 As System.Windows.Forms.Panel
    Friend WithEvents timerServices As System.Windows.Forms.Timer
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
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
    Friend WithEvents openDial As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Ribbon As System.Windows.Forms.Ribbon
    Friend WithEvents ProcessTab As System.Windows.Forms.RibbonTab
    Friend WithEvents ServiceTab As System.Windows.Forms.RibbonTab
    Friend WithEvents HelpTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBProcessActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butStopProcess As System.Windows.Forms.RibbonButton
    Friend WithEvents butResumeProcess As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessOtherActions As System.Windows.Forms.RibbonButton
    Friend WithEvents RBProcessPriority As System.Windows.Forms.RibbonPanel
    Friend WithEvents butKillProcess As System.Windows.Forms.RibbonButton
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
    Friend WithEvents cmdTray As System.Windows.Forms.Button
    Friend WithEvents RBProcessOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butProcessOnlineDesc As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessGoogle As System.Windows.Forms.RibbonButton
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
    Friend WithEvents RBFileKillProcess As System.Windows.Forms.RibbonPanel
    Friend WithEvents butFileRelease As System.Windows.Forms.RibbonButton
    Friend WithEvents mnuFileCopyPctSmall As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileCopyPctBig As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBUpdate As System.Windows.Forms.RibbonPanel
    Friend WithEvents butUpdate As System.Windows.Forms.RibbonButton
    Friend WithEvents RBSearchMain As System.Windows.Forms.RibbonPanel
    Friend WithEvents butSearchGo As System.Windows.Forms.RibbonButton
    Friend WithEvents butSearchSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents txtSearchString As System.Windows.Forms.RibbonTextBox
    Friend WithEvents panelMain6 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerSearch As System.Windows.Forms.SplitContainer
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSearchModules As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchServices As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchProcess As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchCase As System.Windows.Forms.CheckBox
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents HandlesTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBHandlesActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents panelMain7 As System.Windows.Forms.Panel
    Friend WithEvents butHandleRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents butHandleClose As System.Windows.Forms.RibbonButton
    Friend WithEvents chkSearchHandles As System.Windows.Forms.CheckBox
    Friend WithEvents imgSearch As System.Windows.Forms.ImageList
    Friend WithEvents RBHandlesReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butHandlesSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents menuSearch As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAssociatedProcessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowHandlesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBFileOpenFile As System.Windows.Forms.RibbonPanel
    Friend WithEvents butOpenFile As System.Windows.Forms.RibbonButton
    Friend WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butFileRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents lstFileString As System.Windows.Forms.ListBox
    Friend WithEvents gpFileAttributes As System.Windows.Forms.GroupBox
    Friend WithEvents gpFileDates As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTlastModification As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTlastAccess As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTcreation As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdSetFileDates As System.Windows.Forms.Button
    Friend WithEvents chkFileSystem As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileReadOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileHidden As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileCompressed As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileArchive As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileEncrypted As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileContentNotIndexed As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileNormal As System.Windows.Forms.CheckBox
    Friend WithEvents menuHandles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem22 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MonitorTab As System.Windows.Forms.RibbonTab
    Friend WithEvents panelMain8 As System.Windows.Forms.Panel
    Friend WithEvents RBMonitor As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMonitoringAdd As System.Windows.Forms.RibbonButton
    Friend WithEvents butMonitoringRemove As System.Windows.Forms.RibbonButton
    Friend WithEvents RBMonitoringControl As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMonitorStart As System.Windows.Forms.RibbonButton
    Friend WithEvents butMonitorStop As System.Windows.Forms.RibbonButton
    Friend WithEvents butSaveMonitorReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMonitorSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents splitMonitor As System.Windows.Forms.SplitContainer
    Friend WithEvents splitMonitor2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tvMonitor As System.Windows.Forms.TreeView
    Friend WithEvents imgMonitor As System.Windows.Forms.ImageList
    Friend WithEvents txtMonitoringLog As System.Windows.Forms.TextBox
    Friend WithEvents timerMonitoring As System.Windows.Forms.Timer
    Friend WithEvents butProcessMonitor As System.Windows.Forms.RibbonButton
    Friend WithEvents MonitorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents splitMonitor3 As System.Windows.Forms.SplitContainer
    Friend WithEvents splitMonitor4 As System.Windows.Forms.SplitContainer
    Friend WithEvents graphMonitor As YAPM.Graph
    Friend WithEvents txtMonitorNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblMonitorMaxNumber As System.Windows.Forms.Label
    Friend WithEvents chkMonitorRightAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonitorLeftAuto As System.Windows.Forms.CheckBox
    Friend WithEvents dtMonitorR As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtMonitorL As System.Windows.Forms.DateTimePicker
    Friend WithEvents ThreadTab As System.Windows.Forms.RibbonTab
    Friend WithEvents WindowTab As System.Windows.Forms.RibbonTab
    Friend WithEvents ShowThreadsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panelMain9 As System.Windows.Forms.Panel
    Friend WithEvents splitThreads As System.Windows.Forms.SplitContainer
    Friend WithEvents panelMain10 As System.Windows.Forms.Panel
    Friend WithEvents RBThreadsRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butThreadRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents menuThread As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem23 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem24 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem25 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem26 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem27 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem28 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem29 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem30 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem31 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem32 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem33 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBThreadAction As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBThreadPriority As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBThreadReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butThreadKill As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadStop As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadResume As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPriority As System.Windows.Forms.RibbonButton
    Friend WithEvents rtb4 As System.Windows.Forms.RichTextBox
    Friend WithEvents SelectedAssociatedProcessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butThreadPidle As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPlowest As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPbelow As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPnormal As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPabove As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPhighest As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPcritical As System.Windows.Forms.RibbonButton
    Friend WithEvents LowestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBWindowRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBWindowActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBWindowReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents ShowWindowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents imgWindows As System.Windows.Forms.ImageList
    Friend WithEvents butProcessShow As System.Windows.Forms.RibbonButton
    Friend WithEvents butShowProcHandles As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessThreads As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessWindows As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowVisibility As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowShow As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowHide As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowClose As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowPositionSize As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowBringToFront As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowSetAsForeground As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowSetAsActive As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowMinimize As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowMaximize As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowFlash As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowCaption As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowOpacity As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowDoNotBringToFront As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowEnable As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindowDisable As System.Windows.Forms.RibbonButton
    Friend WithEvents splitContainerWindows As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb5 As System.Windows.Forms.RichTextBox
    Friend WithEvents butWindowStopFlashing As System.Windows.Forms.RibbonButton
    Friend WithEvents chkSearchWindows As System.Windows.Forms.CheckBox
    Friend WithEvents menuWindow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem34 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FolderChooser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ModulesTab As System.Windows.Forms.RibbonTab
    Friend WithEvents ShowModulesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butProcessShowModules As System.Windows.Forms.RibbonButton
    Friend WithEvents splitModule As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb6 As System.Windows.Forms.RichTextBox
    Friend WithEvents panelMain11 As System.Windows.Forms.Panel
    Friend WithEvents RBModuleActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModuleRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents butModuleUnload As System.Windows.Forms.RibbonButton
    Friend WithEvents RBModuleReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModulesSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents menuModule As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem35 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem36 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowFileDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainerModules As System.Windows.Forms.SplitContainer
    Friend WithEvents ColumnHeader29 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader43 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader44 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader45 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader46 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblModulesCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchModule As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainerThreads As System.Windows.Forms.SplitContainer
    Friend WithEvents ColumnHeader32 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader33 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader34 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader35 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader36 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader38 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblThreadResults As System.Windows.Forms.Label
    Friend WithEvents txtSearchThread As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainerHandles As System.Windows.Forms.SplitContainer
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblHandlesCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchHandle As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader27 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader28 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents SplitContainerWindows2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ColumnHeader30 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader31 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader39 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader40 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader41 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader42 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblWindowsCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchWindow As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchResults As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkAllWindows As System.Windows.Forms.CheckBox
    Friend WithEvents RBServiceReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butServiceReport As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessShowAll As System.Windows.Forms.RibbonButton
    Friend WithEvents ToolStripMenuItem38 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem37 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReadWriteMemoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBModuleOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModuleGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents ToolStripMenuItem39 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GoogleSearchToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBOptions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butPreferences As System.Windows.Forms.RibbonButton
    Friend WithEvents butAlwaysDisplay As System.Windows.Forms.RibbonButton
    Friend WithEvents SplitContainerProcess As System.Windows.Forms.SplitContainer
    Friend WithEvents tabProcess As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents imgProcessTab As System.Windows.Forms.ImageList
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerInfoProcess As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdInfosToClipB As System.Windows.Forms.Button
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents gpProcGeneralFile As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents pctSmallIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pctBigIcon As System.Windows.Forms.PictureBox
    Friend WithEvents cmdShowFileProperties As System.Windows.Forms.Button
    Friend WithEvents cmdOpenDirectory As System.Windows.Forms.Button
    Friend WithEvents txtProcessPath As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtImageVersion As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtProcessUser As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtProcessId As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtProcessStarted As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtParentProcess As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkDisplayNAProcess As System.Windows.Forms.CheckBox
    Friend WithEvents chkHandles As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnline As System.Windows.Forms.CheckBox
    Friend WithEvents chkModules As System.Windows.Forms.CheckBox
    Friend WithEvents cmdShowFileDetails As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalTime As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblUserTime As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblKernelTime As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPageFaults As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents lblPeakPageFileUsage As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblPageFileUsage As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents lblPeakWorkingSet As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblWorkingSet As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHandles As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblQuotaNPP As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblQuotaPNPP As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblQuotaPP As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblQuotaPPP As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents butProcessDisplayDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessPermuteLvTv As System.Windows.Forms.RibbonButton
    Friend WithEvents SplitContainerTvLv As System.Windows.Forms.SplitContainer
    Friend WithEvents tvProc As System.Windows.Forms.TreeView
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvProcess As YAPM.DoubleBufferedLV
    Friend WithEvents lvSearchResults As YAPM.DoubleBufferedLV
    Friend WithEvents lvModules As YAPM.DoubleBufferedLV
    Friend WithEvents lvThreads As YAPM.DoubleBufferedLV
    Friend WithEvents lvHandles As YAPM.DoubleBufferedLV
    Friend WithEvents lvWindows As YAPM.DoubleBufferedLV
    Friend WithEvents lvJobs As YAPM.DoubleBufferedLV
    Friend WithEvents mainMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MinimizeToTrayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem40 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem41 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EnableProcessRefreshingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnableServiceRefreshingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabProcessToken As System.Windows.Forms.TabControl
    Friend WithEvents tabProcessTokenPagePrivileges As System.Windows.Forms.TabPage
    Friend WithEvents lvPrivileges As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader50 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader51 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader52 As System.Windows.Forms.ColumnHeader
    Friend WithEvents menuPrivileges As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem44 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents splitServices As System.Windows.Forms.SplitContainer
    Friend WithEvents lvServices As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents splitServices2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdCopyServiceToCp As System.Windows.Forms.Button
    Friend WithEvents lblServicePath As System.Windows.Forms.TextBox
    Friend WithEvents lblServiceName As System.Windows.Forms.Label
    Friend WithEvents splitServices3 As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb2 As System.Windows.Forms.RichTextBox
    Friend WithEvents splitServices4 As System.Windows.Forms.SplitContainer
    Friend WithEvents tv2 As System.Windows.Forms.TreeView
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripMenuItem42 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectedAssociatedProcessToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents lvProcServices As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader47 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader48 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents menuProcServ As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem43 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProcOtherBytes As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblProcOther As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblProcWriteBytes As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblProcWrites As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblProcReadBytes As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents lblProcReads As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents timerProcPerf As System.Windows.Forms.Timer
    Friend WithEvents splitPerformances As System.Windows.Forms.SplitContainer
    Friend WithEvents graphCPU As YAPM.Graph2
    Friend WithEvents splitPerformance2 As System.Windows.Forms.SplitContainer
    Friend WithEvents graphMemory As YAPM.Graph2
    Friend WithEvents graphIO As YAPM.Graph2
    Friend WithEvents lvProcMem As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader53 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader54 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader55 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader56 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ShowSystemInformatoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblUserObjectsCount As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblGDIcount As System.Windows.Forms.Label
    Friend WithEvents lbl789 As System.Windows.Forms.Label
    Friend WithEvents SelectedServicesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TaskTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBTaskDisplay As System.Windows.Forms.RibbonPanel
    Friend WithEvents butTaskRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBTaskActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butTaskShow As System.Windows.Forms.RibbonButton
    Friend WithEvents butTaskEndTask As System.Windows.Forms.RibbonButton
    Friend WithEvents menuTasks As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem45 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MaximizeWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MinimizeWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem46 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EndTaskToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem47 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents panelMain13 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerTask As System.Windows.Forms.SplitContainer
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblTaskCountResult As System.Windows.Forms.Label
    Friend WithEvents txtSearchTask As System.Windows.Forms.TextBox
    Friend WithEvents lvTask As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader62 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader63 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader64 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerTask As System.Windows.Forms.Timer
    Friend WithEvents imgTask As System.Windows.Forms.ImageList
    Friend WithEvents SelectWindowInWindowsTabToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KillProcessTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBWindowCapture As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowFind As System.Windows.Forms.RibbonButton
    Friend WithEvents NetworkTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBNetworkRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butNetworkRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents panelMain14 As System.Windows.Forms.Panel
    Friend WithEvents lvNetwork As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader66 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader67 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader68 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader69 As System.Windows.Forms.ColumnHeader
    Friend WithEvents menuNetwork As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem48 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabNetwork As System.Windows.Forms.TabPage
    Friend WithEvents lvProcNetwork As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader49 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader57 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader58 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader59 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerStrings As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdProcStringSave As System.Windows.Forms.Button
    Friend WithEvents optProcStringMemory As System.Windows.Forms.RadioButton
    Friend WithEvents optProcStringImage As System.Windows.Forms.RadioButton
    Friend WithEvents lvProcString As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader76 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader77 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtSearchProcString As System.Windows.Forms.TextBox
    Friend WithEvents pgbString As System.Windows.Forms.ProgressBar
    Friend WithEvents cmdProcSearchR As System.Windows.Forms.Button
    Friend WithEvents cmdProcSearchL As System.Windows.Forms.Button
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Friend WithEvents lvProcEnv As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader60 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader61 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerTrayIcon As System.Windows.Forms.Timer
    Friend WithEvents menuProcMem As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem49 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JumpToPEBAddressToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
