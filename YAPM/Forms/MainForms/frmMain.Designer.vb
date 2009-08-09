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
        Dim CConnection1 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Tasks", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("System")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("[System process]", New System.Windows.Forms.TreeNode() {TreeNode1})
        Dim CConnection2 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection3 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Modules", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection4 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Threads", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection5 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup9 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Handles", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection6 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup11 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Windows", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup12 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Items", 1, 1)
        Dim CConnection7 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup13 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup14 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection8 As YAPM.cConnection = New YAPM.cConnection
        Dim CConnection9 As YAPM.cConnection = New YAPM.cConnection
        Dim CConnection10 As YAPM.cConnection = New YAPM.cConnection
        Dim CConnection11 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup15 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup16 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
        Me.timerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.timerServices = New System.Windows.Forms.Timer(Me.components)
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.EnableServiceRefreshingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.saveDial = New System.Windows.Forms.SaveFileDialog
        Me.openDial = New System.Windows.Forms.OpenFileDialog
        Me.Ribbon = New System.Windows.Forms.Ribbon
        Me.orbStartElevated = New System.Windows.Forms.RibbonOrbMenuItem
        Me.orbMenuNetwork = New System.Windows.Forms.RibbonOrbMenuItem
        Me.RibbonSeparator4 = New System.Windows.Forms.RibbonSeparator
        Me.orbMenuEmergency = New System.Windows.Forms.RibbonOrbMenuItem
        Me.orbMenuSBA = New System.Windows.Forms.RibbonOrbMenuItem
        Me.RibbonSeparator2 = New System.Windows.Forms.RibbonSeparator
        Me.orbMenuSaveReport = New System.Windows.Forms.RibbonOrbMenuItem
        Me.RibbonSeparator5 = New System.Windows.Forms.RibbonSeparator
        Me.orbMenuAbout = New System.Windows.Forms.RibbonOrbMenuItem
        Me.butExit = New System.Windows.Forms.RibbonOrbOptionButton
        Me.butShowPreferences = New System.Windows.Forms.RibbonOrbOptionButton
        Me.butLog = New System.Windows.Forms.RibbonButton
        Me.butSystemInfo = New System.Windows.Forms.RibbonButton
        Me.butWindows = New System.Windows.Forms.RibbonButton
        Me.butFindWindow = New System.Windows.Forms.RibbonButton
        Me.butNetwork = New System.Windows.Forms.RibbonButton
        Me.butFeedBack = New System.Windows.Forms.RibbonButton
        Me.butHiddenProcesses = New System.Windows.Forms.RibbonButton
        Me.butShowDepViewer = New System.Windows.Forms.RibbonButton
        Me.butShowAllPendingTasks = New System.Windows.Forms.RibbonButton
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
        Me.RBProcessActions = New System.Windows.Forms.RibbonPanel
        Me.butNewProcess = New System.Windows.Forms.RibbonButton
        Me.butKillProcess = New System.Windows.Forms.RibbonButton
        Me.butStopProcess = New System.Windows.Forms.RibbonButton
        Me.butResumeProcess = New System.Windows.Forms.RibbonButton
        Me.butProcessOtherActions = New System.Windows.Forms.RibbonButton
        Me.butProcessAffinity = New System.Windows.Forms.RibbonButton
        Me.butProcessReduceWS = New System.Windows.Forms.RibbonButton
        Me.butProcessDumpF = New System.Windows.Forms.RibbonButton
        Me.butProcessLimitCPU = New System.Windows.Forms.RibbonButton
        Me.butProcessShow = New System.Windows.Forms.RibbonButton
        Me.butProcessShowModules = New System.Windows.Forms.RibbonButton
        Me.butProcessThreads = New System.Windows.Forms.RibbonButton
        Me.butShowProcHandles = New System.Windows.Forms.RibbonButton
        Me.butProcessWindows = New System.Windows.Forms.RibbonButton
        Me.butProcessShowAll = New System.Windows.Forms.RibbonButton
        Me.RBProcessPriority = New System.Windows.Forms.RibbonPanel
        Me.butProcessPriority = New System.Windows.Forms.RibbonButton
        Me.butIdle = New System.Windows.Forms.RibbonButton
        Me.butBelowNormal = New System.Windows.Forms.RibbonButton
        Me.butNormal = New System.Windows.Forms.RibbonButton
        Me.butAboveNormal = New System.Windows.Forms.RibbonButton
        Me.butHigh = New System.Windows.Forms.RibbonButton
        Me.butRealTime = New System.Windows.Forms.RibbonButton
        Me.RBProcessOnline = New System.Windows.Forms.RibbonPanel
        Me.butProcessGoogle = New System.Windows.Forms.RibbonButton
        Me.panelProcessReport = New System.Windows.Forms.RibbonPanel
        Me.butSaveProcessReport = New System.Windows.Forms.RibbonButton
        Me.ModulesTab = New System.Windows.Forms.RibbonTab
        Me.RBModuleActions = New System.Windows.Forms.RibbonPanel
        Me.butModuleRefresh = New System.Windows.Forms.RibbonButton
        Me.butModuleUnload = New System.Windows.Forms.RibbonButton
        Me.butModuleViewModuleDep = New System.Windows.Forms.RibbonButton
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
        Me.butServiceDetails = New System.Windows.Forms.RibbonButton
        Me.RBServiceAction = New System.Windows.Forms.RibbonPanel
        Me.butStopService = New System.Windows.Forms.RibbonButton
        Me.butStartService = New System.Windows.Forms.RibbonButton
        Me.RibbonSeparator1 = New System.Windows.Forms.RibbonSeparator
        Me.butPauseService = New System.Windows.Forms.RibbonButton
        Me.butResumeService = New System.Windows.Forms.RibbonButton
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
        Me.imgMonitor = New System.Windows.Forms.ImageList(Me.components)
        Me.timerMonitoring = New System.Windows.Forms.Timer(Me.components)
        Me.FolderChooser = New System.Windows.Forms.FolderBrowserDialog
        Me.timerTask = New System.Windows.Forms.Timer(Me.components)
        Me.timerTrayIcon = New System.Windows.Forms.Timer(Me.components)
        Me.butProcessPermuteLvTv = New System.Windows.Forms.RibbonButton
        Me._main = New System.Windows.Forms.SplitContainer
        Me.containerSystemMenu = New System.Windows.Forms.SplitContainer
        Me._tab = New System.Windows.Forms.TabControl
        Me.pageTasks = New System.Windows.Forms.TabPage
        Me.panelMain13 = New System.Windows.Forms.Panel
        Me.SplitContainerTask = New System.Windows.Forms.SplitContainer
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblTaskCountResult = New System.Windows.Forms.Label
        Me.txtSearchTask = New System.Windows.Forms.TextBox
        Me.lvTask = New YAPM.taskList
        Me.ColumnHeader62 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader63 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader64 = New System.Windows.Forms.ColumnHeader
        Me.pageProcesses = New System.Windows.Forms.TabPage
        Me.containerProcessPage = New System.Windows.Forms.SplitContainer
        Me.panelMenu = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblResCount = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.panelMain = New System.Windows.Forms.Panel
        Me.SplitContainerProcess = New System.Windows.Forms.SplitContainer
        Me.SplitContainerTvLv = New System.Windows.Forms.SplitContainer
        Me.tvProc = New System.Windows.Forms.TreeView
        Me.lvProcess = New YAPM.processList
        Me.c1 = New System.Windows.Forms.ColumnHeader
        Me.c2 = New System.Windows.Forms.ColumnHeader
        Me.c3 = New System.Windows.Forms.ColumnHeader
        Me.c4 = New System.Windows.Forms.ColumnHeader
        Me.c5 = New System.Windows.Forms.ColumnHeader
        Me.c7 = New System.Windows.Forms.ColumnHeader
        Me.c8 = New System.Windows.Forms.ColumnHeader
        Me.c9 = New System.Windows.Forms.ColumnHeader
        Me.c10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader20 = New System.Windows.Forms.ColumnHeader
        Me.pageModules = New System.Windows.Forms.TabPage
        Me.panelMain11 = New System.Windows.Forms.Panel
        Me.splitModule = New System.Windows.Forms.SplitContainer
        Me.SplitContainerModules = New System.Windows.Forms.SplitContainer
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblModulesCount = New System.Windows.Forms.Label
        Me.txtSearchModule = New System.Windows.Forms.TextBox
        Me.lvModules = New YAPM.moduleList
        Me.ColumnHeader29 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader43 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader44 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader45 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader46 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader18 = New System.Windows.Forms.ColumnHeader
        Me.rtb6 = New System.Windows.Forms.RichTextBox
        Me.pageThreads = New System.Windows.Forms.TabPage
        Me.panelMain9 = New System.Windows.Forms.Panel
        Me.splitThreads = New System.Windows.Forms.SplitContainer
        Me.SplitContainerThreads = New System.Windows.Forms.SplitContainer
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblThreadResults = New System.Windows.Forms.Label
        Me.txtSearchThread = New System.Windows.Forms.TextBox
        Me.lvThreads = New YAPM.threadList
        Me.ColumnHeader32 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader33 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader34 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader35 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader36 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader37 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader38 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.rtb4 = New System.Windows.Forms.RichTextBox
        Me.pageHandles = New System.Windows.Forms.TabPage
        Me.panelMain7 = New System.Windows.Forms.Panel
        Me.SplitContainerHandles = New System.Windows.Forms.SplitContainer
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblHandlesCount = New System.Windows.Forms.Label
        Me.txtSearchHandle = New System.Windows.Forms.TextBox
        Me.lvHandles = New YAPM.handleList
        Me.ColumnHeader24 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader25 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader26 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader27 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader28 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader15 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader16 = New System.Windows.Forms.ColumnHeader
        Me.pageWindows = New System.Windows.Forms.TabPage
        Me.panelMain10 = New System.Windows.Forms.Panel
        Me.splitContainerWindows = New System.Windows.Forms.SplitContainer
        Me.SplitContainerWindows2 = New System.Windows.Forms.SplitContainer
        Me.chkAllWindows = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblWindowsCount = New System.Windows.Forms.Label
        Me.txtSearchWindow = New System.Windows.Forms.TextBox
        Me.lvWindows = New YAPM.windowList
        Me.ColumnHeader30 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader31 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader39 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader40 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader41 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader42 = New System.Windows.Forms.ColumnHeader
        Me.rtb5 = New System.Windows.Forms.RichTextBox
        Me.pageMonitor = New System.Windows.Forms.TabPage
        Me.panelMain8 = New System.Windows.Forms.Panel
        Me.splitMonitor = New System.Windows.Forms.SplitContainer
        Me.tvMonitor = New System.Windows.Forms.TreeView
        Me.splitMonitor2 = New System.Windows.Forms.SplitContainer
        Me.txtMonitoringLog = New System.Windows.Forms.TextBox
        Me.lvMonitorReport = New YAPM.DoubleBufferedLV
        Me.ColumnHeader22 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader23 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader47 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader48 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader49 = New System.Windows.Forms.ColumnHeader
        Me.splitMonitor3 = New System.Windows.Forms.SplitContainer
        Me.splitMonitor4 = New System.Windows.Forms.SplitContainer
        Me.graphMonitor = New YAPM.Graph
        Me.txtMonitorNumber = New System.Windows.Forms.TextBox
        Me.lblMonitorMaxNumber = New System.Windows.Forms.Label
        Me.chkMonitorRightAuto = New System.Windows.Forms.CheckBox
        Me.chkMonitorLeftAuto = New System.Windows.Forms.CheckBox
        Me.dtMonitorR = New System.Windows.Forms.DateTimePicker
        Me.dtMonitorL = New System.Windows.Forms.DateTimePicker
        Me.pageServices = New System.Windows.Forms.TabPage
        Me.containerServicesPage = New System.Windows.Forms.SplitContainer
        Me.panelMenu2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblResCount2 = New System.Windows.Forms.Label
        Me.txtServiceSearch = New System.Windows.Forms.TextBox
        Me.panelMain2 = New System.Windows.Forms.Panel
        Me.splitServices = New System.Windows.Forms.SplitContainer
        Me.lvServices = New YAPM.serviceList
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader19 = New System.Windows.Forms.ColumnHeader
        Me.splitServices2 = New System.Windows.Forms.SplitContainer
        Me.cmdCopyServiceToCp = New System.Windows.Forms.Button
        Me.lblServicePath = New System.Windows.Forms.TextBox
        Me.lblServiceName = New System.Windows.Forms.Label
        Me.splitServices3 = New System.Windows.Forms.SplitContainer
        Me.rtb2 = New System.Windows.Forms.RichTextBox
        Me.splitServices4 = New System.Windows.Forms.SplitContainer
        Me.tv2 = New YAPM.serviceDependenciesList
        Me.tv = New YAPM.serviceDependenciesList
        Me.pageNetwork = New System.Windows.Forms.TabPage
        Me.panelMain14 = New System.Windows.Forms.Panel
        Me.lvNetwork = New YAPM.networkList
        Me.ColumnHeader66 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader67 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader68 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader69 = New System.Windows.Forms.ColumnHeader
        Me.pageFile = New System.Windows.Forms.TabPage
        Me.panelMain5 = New System.Windows.Forms.Panel
        Me.SplitContainerFilexx = New System.Windows.Forms.SplitContainer
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.cmdFileClipboard = New System.Windows.Forms.Button
        Me.pctFileSmall = New System.Windows.Forms.PictureBox
        Me.pctFileBig = New System.Windows.Forms.PictureBox
        Me.SplitContainerFile = New System.Windows.Forms.SplitContainer
        Me.SplitContainerFile2 = New System.Windows.Forms.SplitContainer
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
        Me.lvFileString = New YAPM.DoubleBufferedLV
        Me.headerString = New System.Windows.Forms.ColumnHeader
        Me.pageSearch = New System.Windows.Forms.TabPage
        Me.panelMain6 = New System.Windows.Forms.Panel
        Me.SplitContainerSearch = New System.Windows.Forms.SplitContainer
        Me.chkSearchEnvVar = New System.Windows.Forms.CheckBox
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
        Me.lvSearchResults = New YAPM.searchList
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.pageHelp = New System.Windows.Forms.TabPage
        Me.panelMain4 = New System.Windows.Forms.Panel
        Me.WBHelp = New System.Windows.Forms.WebBrowser
        Me.timerNetwork = New System.Windows.Forms.Timer(Me.components)
        Me.timerStateBasedActions = New System.Windows.Forms.Timer(Me.components)
        Me.mnuHandle = New System.Windows.Forms.ContextMenu
        Me.MenuItemHSelectAssociatedProcess = New System.Windows.Forms.MenuItem
        Me.MenuItemCloseHandle = New System.Windows.Forms.MenuItem
        Me.MenuItemNavigateHandle = New System.Windows.Forms.MenuItem
        Me.MenuItem12 = New System.Windows.Forms.MenuItem
        Me.MenuItemShowUnnamedHandles = New System.Windows.Forms.MenuItem
        Me.MenuItem14 = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyHandle = New System.Windows.Forms.MenuItem
        Me.MenuItemChooseColumnsHandle = New System.Windows.Forms.MenuItem
        Me.MenuItemTaskShow = New System.Windows.Forms.MenuItem
        Me.MenuItemTaskEnd = New System.Windows.Forms.MenuItem
        Me.MenuItemTaskSelProc = New System.Windows.Forms.MenuItem
        Me.MenuItemMonitorAdd = New System.Windows.Forms.MenuItem
        Me.MenuItemMonitorRemove = New System.Windows.Forms.MenuItem
        Me.MenuItemMonitorStart = New System.Windows.Forms.MenuItem
        Me.MenuItemMonitorStop = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyBig = New System.Windows.Forms.MenuItem
        Me.MenuItemCopySmall = New System.Windows.Forms.MenuItem
        Me.MenuItemMainShow = New System.Windows.Forms.MenuItem
        Me.MenuItemMainToTray = New System.Windows.Forms.MenuItem
        Me.MenuItemMainAbout = New System.Windows.Forms.MenuItem
        Me.MenuItemMainLog = New System.Windows.Forms.MenuItem
        Me.MenuItemMainOpenedW = New System.Windows.Forms.MenuItem
        Me.MenuItemMainExit = New System.Windows.Forms.MenuItem
        Me.MenuItemMainSysInfo = New System.Windows.Forms.MenuItem
        Me.MenuItemServSelService = New System.Windows.Forms.MenuItem
        Me.MenuItemServFileProp = New System.Windows.Forms.MenuItem
        Me.MenuItemServOpenDir = New System.Windows.Forms.MenuItem
        Me.MenuItemServFileDetails = New System.Windows.Forms.MenuItem
        Me.MenuItemServSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemServPause = New System.Windows.Forms.MenuItem
        Me.MenuItemServStop = New System.Windows.Forms.MenuItem
        Me.MenuItemServStart = New System.Windows.Forms.MenuItem
        Me.MenuItemServAutoStart = New System.Windows.Forms.MenuItem
        Me.MenuItemServOnDemand = New System.Windows.Forms.MenuItem
        Me.MenuItemServDisabled = New System.Windows.Forms.MenuItem
        Me.MenuItemServDepe = New System.Windows.Forms.MenuItem
        Me.MenuItemNetworkClose = New System.Windows.Forms.MenuItem
        Me.MenuItemServSelProc = New System.Windows.Forms.MenuItem
        Me.MenuItemThTerm = New System.Windows.Forms.MenuItem
        Me.MenuItemThSuspend = New System.Windows.Forms.MenuItem
        Me.MenuItemThResu = New System.Windows.Forms.MenuItem
        Me.MenuItemThIdle = New System.Windows.Forms.MenuItem
        Me.MenuItemThLowest = New System.Windows.Forms.MenuItem
        Me.MenuItemThBNormal = New System.Windows.Forms.MenuItem
        Me.MenuItemThNorm = New System.Windows.Forms.MenuItem
        Me.MenuItemThANorm = New System.Windows.Forms.MenuItem
        Me.MenuItemThHighest = New System.Windows.Forms.MenuItem
        Me.MenuItemThTimeCr = New System.Windows.Forms.MenuItem
        Me.MenuItemThSelProc = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleFileProp = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleOpenDir = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleFileDetails = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleDependencies = New System.Windows.Forms.MenuItem
        Me.MenuItemUnloadModule = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleSelProc = New System.Windows.Forms.MenuItem
        Me.MenuItemWindowSelProc = New System.Windows.Forms.MenuItem
        Me.MenuItemSearchSel = New System.Windows.Forms.MenuItem
        Me.MenuItemSearchClose = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSFileProp = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSOpenDir = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSFileDetails = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSDep = New System.Windows.Forms.MenuItem
        Me.MenuItemProcKill = New System.Windows.Forms.MenuItem
        Me.MenuItemProcKillT = New System.Windows.Forms.MenuItem
        Me.MenuItemProcStop = New System.Windows.Forms.MenuItem
        Me.MenuItemProcResume = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPIdle = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPBN = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPN = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPAN = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPH = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPRT = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemRefresh = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemLog = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemInfos = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemOpenedWindows = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemToTray = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemExit = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemUpdate = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemWebsite = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemAbout = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemFindWindow = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemHelp = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemOptions = New System.Windows.Forms.MenuItem
        Me.MenuItemMainFindWindow = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyTask = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyService = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyNetwork = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyThread = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyModule = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyWindow = New System.Windows.Forms.MenuItem
        Me.MenuItemCopySearch = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyProcess = New System.Windows.Forms.MenuItem
        Me.mnuTask = New System.Windows.Forms.ContextMenu
        Me.MenuItemTaskMax = New System.Windows.Forms.MenuItem
        Me.MenuItemTaskMin = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.MenuItemTaskSelWin = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MenuItemTaskColumns = New System.Windows.Forms.MenuItem
        Me.mnuMonitor = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuFileCpPctBig = New System.Windows.Forms.ContextMenu
        Me.mnuFileCpPctSmall = New System.Windows.Forms.ContextMenu
        Me.mnuMain = New System.Windows.Forms.ContextMenu
        Me.MenuItemMainAlwaysVisible = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItemMainRestart = New System.Windows.Forms.MenuItem
        Me.MenuItemMainShutdown = New System.Windows.Forms.MenuItem
        Me.MenuItemMainPowerOff = New System.Windows.Forms.MenuItem
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.MenuItemMainSleep = New System.Windows.Forms.MenuItem
        Me.MenuItemMainHibernate = New System.Windows.Forms.MenuItem
        Me.MenuItem17 = New System.Windows.Forms.MenuItem
        Me.MenuItemMainLogOff = New System.Windows.Forms.MenuItem
        Me.MenuItemMainLock = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItemMainReport = New System.Windows.Forms.MenuItem
        Me.MenuItemMainEmergencyH = New System.Windows.Forms.MenuItem
        Me.MenuItemMainSBA = New System.Windows.Forms.MenuItem
        Me.MenuItem15 = New System.Windows.Forms.MenuItem
        Me.MenuItemRefProc = New System.Windows.Forms.MenuItem
        Me.MenuItemMainRefServ = New System.Windows.Forms.MenuItem
        Me.MenuItem18 = New System.Windows.Forms.MenuItem
        Me.mnuService = New System.Windows.Forms.ContextMenu
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.MenuItem20 = New System.Windows.Forms.MenuItem
        Me.MenuItem8 = New System.Windows.Forms.MenuItem
        Me.MenuItem25 = New System.Windows.Forms.MenuItem
        Me.MenuItemServReanalize = New System.Windows.Forms.MenuItem
        Me.MenuItem24 = New System.Windows.Forms.MenuItem
        Me.MenuItemServColumns = New System.Windows.Forms.MenuItem
        Me.mnuNetwork = New System.Windows.Forms.ContextMenu
        Me.MenuItem16 = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MenuItemNetworkColumns = New System.Windows.Forms.MenuItem
        Me.mnuThread = New System.Windows.Forms.ContextMenu
        Me.MenuItem22 = New System.Windows.Forms.MenuItem
        Me.MenuItemThPriority = New System.Windows.Forms.MenuItem
        Me.MenuItemThAffinity = New System.Windows.Forms.MenuItem
        Me.MenuItem19 = New System.Windows.Forms.MenuItem
        Me.MenuItemThColumns = New System.Windows.Forms.MenuItem
        Me.mnuModule = New System.Windows.Forms.ContextMenu
        Me.MenuItem26 = New System.Windows.Forms.MenuItem
        Me.MenuItem13 = New System.Windows.Forms.MenuItem
        Me.MenuItem21 = New System.Windows.Forms.MenuItem
        Me.MenuItemColumnsModule = New System.Windows.Forms.MenuItem
        Me.mnuWindow = New System.Windows.Forms.ContextMenu
        Me.MenuItem23 = New System.Windows.Forms.MenuItem
        Me.MenuItemWShow = New System.Windows.Forms.MenuItem
        Me.MenuItemWSUnnamed = New System.Windows.Forms.MenuItem
        Me.MenuItemWHide = New System.Windows.Forms.MenuItem
        Me.MenuItemWClose = New System.Windows.Forms.MenuItem
        Me.MenuItem36 = New System.Windows.Forms.MenuItem
        Me.MenuItemWVisibility = New System.Windows.Forms.MenuItem
        Me.MenuItemWFront = New System.Windows.Forms.MenuItem
        Me.MenuItemWNotFront = New System.Windows.Forms.MenuItem
        Me.MenuItemWActive = New System.Windows.Forms.MenuItem
        Me.MenuItemWForeground = New System.Windows.Forms.MenuItem
        Me.MenuItem50 = New System.Windows.Forms.MenuItem
        Me.MenuItemWMin = New System.Windows.Forms.MenuItem
        Me.MenuItemWMax = New System.Windows.Forms.MenuItem
        Me.MenuItemWPos = New System.Windows.Forms.MenuItem
        Me.MenuItem57 = New System.Windows.Forms.MenuItem
        Me.MenuItemWFlash = New System.Windows.Forms.MenuItem
        Me.MenuItemWStopFlash = New System.Windows.Forms.MenuItem
        Me.MenuItem63 = New System.Windows.Forms.MenuItem
        Me.MenuItemWOpacity = New System.Windows.Forms.MenuItem
        Me.MenuItemWCaption = New System.Windows.Forms.MenuItem
        Me.MenuItem69 = New System.Windows.Forms.MenuItem
        Me.MenuItemWEnable = New System.Windows.Forms.MenuItem
        Me.MenuItemWDisab = New System.Windows.Forms.MenuItem
        Me.MenuItem74 = New System.Windows.Forms.MenuItem
        Me.MenuItemWindowColumns = New System.Windows.Forms.MenuItem
        Me.mnuSearch = New System.Windows.Forms.ContextMenu
        Me.MenuItemSearchNew = New System.Windows.Forms.MenuItem
        Me.MenuItem28 = New System.Windows.Forms.MenuItem
        Me.MenuItem30 = New System.Windows.Forms.MenuItem
        Me.mnuProcess = New System.Windows.Forms.ContextMenu
        Me.MenuItemProcPriority = New System.Windows.Forms.MenuItem
        Me.MenuItem44 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcReanalize = New System.Windows.Forms.MenuItem
        Me.MenuItem27 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSModules = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSThreads = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSHandles = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSWindows = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSServices = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSAll = New System.Windows.Forms.MenuItem
        Me.MenuItem35 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcWSS = New System.Windows.Forms.MenuItem
        Me.MenuItemProcAff = New System.Windows.Forms.MenuItem
        Me.MenuItemProcDump = New System.Windows.Forms.MenuItem
        Me.MenuItem51 = New System.Windows.Forms.MenuItem
        Me.MenuItem38 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcColumns = New System.Windows.Forms.MenuItem
        Me.mnuSystem = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItemSYSTEMFILE = New System.Windows.Forms.MenuItem
        Me.MenuItem54 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemConnection = New System.Windows.Forms.MenuItem
        Me.MenuItemRunAsAdmin = New System.Windows.Forms.MenuItem
        Me.MenuItem56 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemReport = New System.Windows.Forms.MenuItem
        Me.MenuItem59 = New System.Windows.Forms.MenuItem
        Me.MenuItemShowPendingTasks = New System.Windows.Forms.MenuItem
        Me.MenuItem62 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemEmergency = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemSBA = New System.Windows.Forms.MenuItem
        Me.MenuItem66 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcesses = New System.Windows.Forms.MenuItem
        Me.MenuItemReportProcesses = New System.Windows.Forms.MenuItem
        Me.MenuItemModules = New System.Windows.Forms.MenuItem
        Me.MenuItemReportModules = New System.Windows.Forms.MenuItem
        Me.MenuItemThreads = New System.Windows.Forms.MenuItem
        Me.MenuItemReportThreads = New System.Windows.Forms.MenuItem
        Me.MenuItemHandles = New System.Windows.Forms.MenuItem
        Me.MenuItemReportHandles = New System.Windows.Forms.MenuItem
        Me.MenuItemMonitor = New System.Windows.Forms.MenuItem
        Me.MenuItemReportMonitor = New System.Windows.Forms.MenuItem
        Me.MenuItemWindows = New System.Windows.Forms.MenuItem
        Me.MenuItem67 = New System.Windows.Forms.MenuItem
        Me.MenuItemWEnalbe = New System.Windows.Forms.MenuItem
        Me.MenuItemWDisable = New System.Windows.Forms.MenuItem
        Me.MenuItem71 = New System.Windows.Forms.MenuItem
        Me.MenuItemReportWindows = New System.Windows.Forms.MenuItem
        Me.MenuItemServices = New System.Windows.Forms.MenuItem
        Me.MenuItemReportServices = New System.Windows.Forms.MenuItem
        Me.MenuItemSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemNewSearch = New System.Windows.Forms.MenuItem
        Me.MenuItem61 = New System.Windows.Forms.MenuItem
        Me.MenuItemReportSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemFiles = New System.Windows.Forms.MenuItem
        Me.MenuItemFileOpen = New System.Windows.Forms.MenuItem
        Me.MenuItem68 = New System.Windows.Forms.MenuItem
        Me.MenuItemFileRelease = New System.Windows.Forms.MenuItem
        Me.MenuItemFileDelete = New System.Windows.Forms.MenuItem
        Me.MenuItemFileTrash = New System.Windows.Forms.MenuItem
        Me.MenuItem72 = New System.Windows.Forms.MenuItem
        Me.MenuItemFileRename = New System.Windows.Forms.MenuItem
        Me.MenuItemFileShellOpen = New System.Windows.Forms.MenuItem
        Me.MenuItemFileMove = New System.Windows.Forms.MenuItem
        Me.MenuItemFileCopy = New System.Windows.Forms.MenuItem
        Me.MenuItem77 = New System.Windows.Forms.MenuItem
        Me.MenuItemFileEncrypt = New System.Windows.Forms.MenuItem
        Me.MenuItemFileDecrypt = New System.Windows.Forms.MenuItem
        Me.MenuItem80 = New System.Windows.Forms.MenuItem
        Me.MenuItemFileStrings = New System.Windows.Forms.MenuItem
        Me.MenuItemSYSTEMOPT = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemAlwaysVisible = New System.Windows.Forms.MenuItem
        Me.MenuItem37 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemRefProc = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemRefServ = New System.Windows.Forms.MenuItem
        Me.MenuItem42 = New System.Windows.Forms.MenuItem
        Me.MenuItemSYSTEMTOOLS = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemShowHidden = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemDependency = New System.Windows.Forms.MenuItem
        Me.MenuItemSYSTEMSYSTEM = New System.Windows.Forms.MenuItem
        Me.MenuItem34 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemRestart = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemShutdown = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemPowerOff = New System.Windows.Forms.MenuItem
        Me.MenuItem40 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemSleep = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemHIbernate = New System.Windows.Forms.MenuItem
        Me.MenuItem43 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemLogoff = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemLock = New System.Windows.Forms.MenuItem
        Me.MenuItemSYSTEMHEL = New System.Windows.Forms.MenuItem
        Me.MenuItem39 = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemDonation = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemFeedBack = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemSF = New System.Windows.Forms.MenuItem
        Me.MenuItemSystemDownloads = New System.Windows.Forms.MenuItem
        Me.MenuItem49 = New System.Windows.Forms.MenuItem
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.StatusBar = New System.Windows.Forms.StatusBar
        Me.sbPanelConnection = New System.Windows.Forms.StatusBarPanel
        Me.sbPanelProcesses = New System.Windows.Forms.StatusBarPanel
        Me.sbPanelServices = New System.Windows.Forms.StatusBarPanel
        Me.sbPanelCpu = New System.Windows.Forms.StatusBarPanel
        Me.sbPanelMemory = New System.Windows.Forms.StatusBarPanel
        Me.timerStatus = New System.Windows.Forms.Timer(Me.components)
        Me._main.Panel1.SuspendLayout()
        Me._main.Panel2.SuspendLayout()
        Me._main.SuspendLayout()
        Me.containerSystemMenu.Panel2.SuspendLayout()
        Me.containerSystemMenu.SuspendLayout()
        Me._tab.SuspendLayout()
        Me.pageTasks.SuspendLayout()
        Me.panelMain13.SuspendLayout()
        Me.SplitContainerTask.Panel1.SuspendLayout()
        Me.SplitContainerTask.Panel2.SuspendLayout()
        Me.SplitContainerTask.SuspendLayout()
        Me.pageProcesses.SuspendLayout()
        Me.containerProcessPage.Panel1.SuspendLayout()
        Me.containerProcessPage.Panel2.SuspendLayout()
        Me.containerProcessPage.SuspendLayout()
        Me.panelMenu.SuspendLayout()
        Me.panelMain.SuspendLayout()
        Me.SplitContainerProcess.Panel1.SuspendLayout()
        Me.SplitContainerProcess.SuspendLayout()
        Me.SplitContainerTvLv.Panel1.SuspendLayout()
        Me.SplitContainerTvLv.Panel2.SuspendLayout()
        Me.SplitContainerTvLv.SuspendLayout()
        Me.pageModules.SuspendLayout()
        Me.panelMain11.SuspendLayout()
        Me.splitModule.Panel1.SuspendLayout()
        Me.splitModule.Panel2.SuspendLayout()
        Me.splitModule.SuspendLayout()
        Me.SplitContainerModules.Panel1.SuspendLayout()
        Me.SplitContainerModules.Panel2.SuspendLayout()
        Me.SplitContainerModules.SuspendLayout()
        Me.pageThreads.SuspendLayout()
        Me.panelMain9.SuspendLayout()
        Me.splitThreads.Panel1.SuspendLayout()
        Me.splitThreads.Panel2.SuspendLayout()
        Me.splitThreads.SuspendLayout()
        Me.SplitContainerThreads.Panel1.SuspendLayout()
        Me.SplitContainerThreads.Panel2.SuspendLayout()
        Me.SplitContainerThreads.SuspendLayout()
        Me.pageHandles.SuspendLayout()
        Me.panelMain7.SuspendLayout()
        Me.SplitContainerHandles.Panel1.SuspendLayout()
        Me.SplitContainerHandles.Panel2.SuspendLayout()
        Me.SplitContainerHandles.SuspendLayout()
        Me.pageWindows.SuspendLayout()
        Me.panelMain10.SuspendLayout()
        Me.splitContainerWindows.Panel1.SuspendLayout()
        Me.splitContainerWindows.Panel2.SuspendLayout()
        Me.splitContainerWindows.SuspendLayout()
        Me.SplitContainerWindows2.Panel1.SuspendLayout()
        Me.SplitContainerWindows2.Panel2.SuspendLayout()
        Me.SplitContainerWindows2.SuspendLayout()
        Me.pageMonitor.SuspendLayout()
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
        Me.pageServices.SuspendLayout()
        Me.containerServicesPage.Panel1.SuspendLayout()
        Me.containerServicesPage.Panel2.SuspendLayout()
        Me.containerServicesPage.SuspendLayout()
        Me.panelMenu2.SuspendLayout()
        Me.panelMain2.SuspendLayout()
        Me.splitServices.Panel1.SuspendLayout()
        Me.splitServices.Panel2.SuspendLayout()
        Me.splitServices.SuspendLayout()
        Me.splitServices2.Panel1.SuspendLayout()
        Me.splitServices2.Panel2.SuspendLayout()
        Me.splitServices2.SuspendLayout()
        Me.splitServices3.Panel1.SuspendLayout()
        Me.splitServices3.Panel2.SuspendLayout()
        Me.splitServices3.SuspendLayout()
        Me.splitServices4.Panel1.SuspendLayout()
        Me.splitServices4.Panel2.SuspendLayout()
        Me.splitServices4.SuspendLayout()
        Me.pageNetwork.SuspendLayout()
        Me.panelMain14.SuspendLayout()
        Me.pageFile.SuspendLayout()
        Me.panelMain5.SuspendLayout()
        Me.SplitContainerFilexx.Panel1.SuspendLayout()
        Me.SplitContainerFilexx.Panel2.SuspendLayout()
        Me.SplitContainerFilexx.SuspendLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerFile.Panel1.SuspendLayout()
        Me.SplitContainerFile.Panel2.SuspendLayout()
        Me.SplitContainerFile.SuspendLayout()
        Me.SplitContainerFile2.Panel1.SuspendLayout()
        Me.SplitContainerFile2.Panel2.SuspendLayout()
        Me.SplitContainerFile2.SuspendLayout()
        Me.gpFileAttributes.SuspendLayout()
        Me.gpFileDates.SuspendLayout()
        Me.pageSearch.SuspendLayout()
        Me.panelMain6.SuspendLayout()
        Me.SplitContainerSearch.Panel1.SuspendLayout()
        Me.SplitContainerSearch.Panel2.SuspendLayout()
        Me.SplitContainerSearch.SuspendLayout()
        Me.pageHelp.SuspendLayout()
        Me.panelMain4.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelConnection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelProcesses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelCpu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelMemory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgMain
        '
        Me.imgMain.ImageStream = CType(resources.GetObject("imgMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMain.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMain.Images.SetKeyName(0, "main_big2.ico")
        Me.imgMain.Images.SetKeyName(1, "noicon32")
        '
        'imgProcess
        '
        Me.imgProcess.ImageStream = CType(resources.GetObject("imgProcess.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcess.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcess.Images.SetKeyName(0, "noIcon")
        '
        'timerProcess
        '
        Me.timerProcess.Interval = 1000
        '
        'imgServices
        '
        Me.imgServices.ImageStream = CType(resources.GetObject("imgServices.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgServices.TransparentColor = System.Drawing.Color.Transparent
        Me.imgServices.Images.SetKeyName(0, "ok")
        Me.imgServices.Images.SetKeyName(1, "ko")
        Me.imgServices.Images.SetKeyName(2, "key")
        Me.imgServices.Images.SetKeyName(3, "thread")
        Me.imgServices.Images.SetKeyName(4, "noicon")
        Me.imgServices.Images.SetKeyName(5, "service")
        '
        'timerServices
        '
        Me.timerServices.Interval = 6000
        '
        'Tray
        '
        Me.Tray.Icon = CType(resources.GetObject("Tray.Icon"), System.Drawing.Icon)
        Me.Tray.Text = "Yet Another (remote) Process Monitor"
        '
        'EnableServiceRefreshingToolStripMenuItem
        '
        Me.EnableServiceRefreshingToolStripMenuItem.Checked = True
        Me.EnableServiceRefreshingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableServiceRefreshingToolStripMenuItem.Name = "EnableServiceRefreshingToolStripMenuItem"
        Me.EnableServiceRefreshingToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.EnableServiceRefreshingToolStripMenuItem.Text = "Enable service refreshing"
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
        '
        '
        '
        Me.Ribbon.OrbDropDown.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.orbStartElevated)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.orbMenuNetwork)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.RibbonSeparator4)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.orbMenuEmergency)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.orbMenuSBA)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.RibbonSeparator2)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.orbMenuSaveReport)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.RibbonSeparator5)
        Me.Ribbon.OrbDropDown.MenuItems.Add(Me.orbMenuAbout)
        Me.Ribbon.OrbDropDown.Name = ""
        Me.Ribbon.OrbDropDown.NextPopup = Nothing
        Me.Ribbon.OrbDropDown.OptionItems.Add(Me.butExit)
        Me.Ribbon.OrbDropDown.OptionItems.Add(Me.butShowPreferences)
        Me.Ribbon.OrbDropDown.PreviousPopup = Nothing
        Me.Ribbon.OrbDropDown.Size = New System.Drawing.Size(527, 345)
        Me.Ribbon.OrbDropDown.TabIndex = 0
        Me.Ribbon.OrbDropDown.ToolStripDropDown = Nothing
        Me.Ribbon.OrbImage = CType(resources.GetObject("Ribbon.OrbImage"), System.Drawing.Image)
        '
        '
        '
        Me.Ribbon.QuickAcessToolbar.AltKey = Nothing
        Me.Ribbon.QuickAcessToolbar.Checked = True
        Me.Ribbon.QuickAcessToolbar.Image = Nothing
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butLog)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butSystemInfo)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butWindows)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butFindWindow)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butNetwork)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butFeedBack)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butHiddenProcesses)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butShowDepViewer)
        Me.Ribbon.QuickAcessToolbar.Items.Add(Me.butShowAllPendingTasks)
        Me.Ribbon.QuickAcessToolbar.Tag = Nothing
        Me.Ribbon.QuickAcessToolbar.Text = Nothing
        Me.Ribbon.QuickAcessToolbar.ToolTip = Nothing
        Me.Ribbon.QuickAcessToolbar.ToolTipImage = Nothing
        Me.Ribbon.QuickAcessToolbar.ToolTipTitle = Nothing
        Me.Ribbon.Size = New System.Drawing.Size(866, 138)
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
        'orbStartElevated
        '
        Me.orbStartElevated.AltKey = Nothing
        Me.orbStartElevated.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.orbStartElevated.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.orbStartElevated.Image = Global.YAPM.My.Resources.Resources.shield_32
        Me.orbStartElevated.SmallImage = Global.YAPM.My.Resources.Resources.shield_32
        Me.orbStartElevated.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.orbStartElevated.Tag = Nothing
        Me.orbStartElevated.Text = "Restart with privileges"
        Me.orbStartElevated.ToolTip = Nothing
        Me.orbStartElevated.ToolTipImage = Nothing
        Me.orbStartElevated.ToolTipTitle = Nothing
        '
        'orbMenuNetwork
        '
        Me.orbMenuNetwork.AltKey = Nothing
        Me.orbMenuNetwork.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.orbMenuNetwork.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.orbMenuNetwork.Image = CType(resources.GetObject("orbMenuNetwork.Image"), System.Drawing.Image)
        Me.orbMenuNetwork.SmallImage = CType(resources.GetObject("orbMenuNetwork.SmallImage"), System.Drawing.Image)
        Me.orbMenuNetwork.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.orbMenuNetwork.Tag = Nothing
        Me.orbMenuNetwork.Text = "Change connection type"
        Me.orbMenuNetwork.ToolTip = Nothing
        Me.orbMenuNetwork.ToolTipImage = Nothing
        Me.orbMenuNetwork.ToolTipTitle = Nothing
        '
        'RibbonSeparator4
        '
        Me.RibbonSeparator4.AltKey = Nothing
        Me.RibbonSeparator4.Image = Nothing
        Me.RibbonSeparator4.Tag = Nothing
        Me.RibbonSeparator4.Text = Nothing
        Me.RibbonSeparator4.ToolTip = Nothing
        Me.RibbonSeparator4.ToolTipImage = Nothing
        Me.RibbonSeparator4.ToolTipTitle = Nothing
        '
        'orbMenuEmergency
        '
        Me.orbMenuEmergency.AltKey = Nothing
        Me.orbMenuEmergency.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.orbMenuEmergency.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.orbMenuEmergency.Image = CType(resources.GetObject("orbMenuEmergency.Image"), System.Drawing.Image)
        Me.orbMenuEmergency.SmallImage = CType(resources.GetObject("orbMenuEmergency.SmallImage"), System.Drawing.Image)
        Me.orbMenuEmergency.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.orbMenuEmergency.Tag = Nothing
        Me.orbMenuEmergency.Text = "Emergency hotkeys  "
        Me.orbMenuEmergency.ToolTip = Nothing
        Me.orbMenuEmergency.ToolTipImage = Nothing
        Me.orbMenuEmergency.ToolTipTitle = Nothing
        '
        'orbMenuSBA
        '
        Me.orbMenuSBA.AltKey = Nothing
        Me.orbMenuSBA.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.orbMenuSBA.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.orbMenuSBA.Image = Global.YAPM.My.Resources.Resources.monitoring2
        Me.orbMenuSBA.SmallImage = Global.YAPM.My.Resources.Resources.monitoring2
        Me.orbMenuSBA.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.orbMenuSBA.Tag = Nothing
        Me.orbMenuSBA.Text = "State based actions  "
        Me.orbMenuSBA.ToolTip = Nothing
        Me.orbMenuSBA.ToolTipImage = Nothing
        Me.orbMenuSBA.ToolTipTitle = Nothing
        '
        'RibbonSeparator2
        '
        Me.RibbonSeparator2.AltKey = Nothing
        Me.RibbonSeparator2.Image = Nothing
        Me.RibbonSeparator2.Tag = Nothing
        Me.RibbonSeparator2.Text = Nothing
        Me.RibbonSeparator2.ToolTip = Nothing
        Me.RibbonSeparator2.ToolTipImage = Nothing
        Me.RibbonSeparator2.ToolTipTitle = Nothing
        '
        'orbMenuSaveReport
        '
        Me.orbMenuSaveReport.AltKey = Nothing
        Me.orbMenuSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.orbMenuSaveReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.orbMenuSaveReport.Image = CType(resources.GetObject("orbMenuSaveReport.Image"), System.Drawing.Image)
        Me.orbMenuSaveReport.SmallImage = CType(resources.GetObject("orbMenuSaveReport.SmallImage"), System.Drawing.Image)
        Me.orbMenuSaveReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.orbMenuSaveReport.Tag = Nothing
        Me.orbMenuSaveReport.Text = "Save report"
        Me.orbMenuSaveReport.ToolTip = Nothing
        Me.orbMenuSaveReport.ToolTipImage = Nothing
        Me.orbMenuSaveReport.ToolTipTitle = Nothing
        '
        'RibbonSeparator5
        '
        Me.RibbonSeparator5.AltKey = Nothing
        Me.RibbonSeparator5.Image = Nothing
        Me.RibbonSeparator5.Tag = Nothing
        Me.RibbonSeparator5.Text = Nothing
        Me.RibbonSeparator5.ToolTip = Nothing
        Me.RibbonSeparator5.ToolTipImage = Nothing
        Me.RibbonSeparator5.ToolTipTitle = Nothing
        '
        'orbMenuAbout
        '
        Me.orbMenuAbout.AltKey = Nothing
        Me.orbMenuAbout.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.orbMenuAbout.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.orbMenuAbout.Image = CType(resources.GetObject("orbMenuAbout.Image"), System.Drawing.Image)
        Me.orbMenuAbout.SmallImage = CType(resources.GetObject("orbMenuAbout.SmallImage"), System.Drawing.Image)
        Me.orbMenuAbout.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.orbMenuAbout.Tag = Nothing
        Me.orbMenuAbout.Text = "About"
        Me.orbMenuAbout.ToolTip = Nothing
        Me.orbMenuAbout.ToolTipImage = Nothing
        Me.orbMenuAbout.ToolTipTitle = Nothing
        '
        'butExit
        '
        Me.butExit.AltKey = Nothing
        Me.butExit.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butExit.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butExit.Image = Global.YAPM.My.Resources.Resources.cross_circle
        Me.butExit.SmallImage = Global.YAPM.My.Resources.Resources.cross_circle
        Me.butExit.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butExit.Tag = Nothing
        Me.butExit.Text = "Quit"
        Me.butExit.ToolTip = Nothing
        Me.butExit.ToolTipImage = Nothing
        Me.butExit.ToolTipTitle = Nothing
        '
        'butShowPreferences
        '
        Me.butShowPreferences.AltKey = Nothing
        Me.butShowPreferences.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.butShowPreferences.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butShowPreferences.Image = CType(resources.GetObject("butShowPreferences.Image"), System.Drawing.Image)
        Me.butShowPreferences.SmallImage = CType(resources.GetObject("butShowPreferences.SmallImage"), System.Drawing.Image)
        Me.butShowPreferences.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butShowPreferences.Tag = Nothing
        Me.butShowPreferences.Text = "Preferences"
        Me.butShowPreferences.ToolTip = Nothing
        Me.butShowPreferences.ToolTipImage = Nothing
        Me.butShowPreferences.ToolTipTitle = Nothing
        '
        'butLog
        '
        Me.butLog.AltKey = Nothing
        Me.butLog.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butLog.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butLog.Image = Nothing
        Me.butLog.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butLog.SmallImage = Global.YAPM.My.Resources.Resources.document_text
        Me.butLog.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butLog.Tag = Nothing
        Me.butLog.Text = "Show log"
        Me.butLog.ToolTip = "Show log"
        Me.butLog.ToolTipImage = Nothing
        Me.butLog.ToolTipTitle = Nothing
        '
        'butSystemInfo
        '
        Me.butSystemInfo.AltKey = Nothing
        Me.butSystemInfo.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butSystemInfo.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSystemInfo.Image = CType(resources.GetObject("butSystemInfo.Image"), System.Drawing.Image)
        Me.butSystemInfo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butSystemInfo.SmallImage = CType(resources.GetObject("butSystemInfo.SmallImage"), System.Drawing.Image)
        Me.butSystemInfo.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSystemInfo.Tag = Nothing
        Me.butSystemInfo.Text = "Show system infos"
        Me.butSystemInfo.ToolTip = "Show system infos"
        Me.butSystemInfo.ToolTipImage = Nothing
        Me.butSystemInfo.ToolTipTitle = Nothing
        '
        'butWindows
        '
        Me.butWindows.AltKey = Nothing
        Me.butWindows.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butWindows.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindows.Image = CType(resources.GetObject("butWindows.Image"), System.Drawing.Image)
        Me.butWindows.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butWindows.SmallImage = CType(resources.GetObject("butWindows.SmallImage"), System.Drawing.Image)
        Me.butWindows.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butWindows.Tag = Nothing
        Me.butWindows.Text = "Show opened windows"
        Me.butWindows.ToolTip = "Show opened windows"
        Me.butWindows.ToolTipImage = Nothing
        Me.butWindows.ToolTipTitle = Nothing
        '
        'butFindWindow
        '
        Me.butFindWindow.AltKey = Nothing
        Me.butFindWindow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butFindWindow.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFindWindow.Image = CType(resources.GetObject("butFindWindow.Image"), System.Drawing.Image)
        Me.butFindWindow.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butFindWindow.SmallImage = CType(resources.GetObject("butFindWindow.SmallImage"), System.Drawing.Image)
        Me.butFindWindow.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFindWindow.Tag = Nothing
        Me.butFindWindow.Text = "Find a process by window"
        Me.butFindWindow.ToolTip = "Find a process by window"
        Me.butFindWindow.ToolTipImage = Nothing
        Me.butFindWindow.ToolTipTitle = Nothing
        '
        'butNetwork
        '
        Me.butNetwork.AltKey = Nothing
        Me.butNetwork.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butNetwork.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butNetwork.Image = Nothing
        Me.butNetwork.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butNetwork.SmallImage = CType(resources.GetObject("butNetwork.SmallImage"), System.Drawing.Image)
        Me.butNetwork.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butNetwork.Tag = Nothing
        Me.butNetwork.Text = "Change connection type"
        Me.butNetwork.ToolTip = "Change connection type"
        Me.butNetwork.ToolTipImage = Nothing
        Me.butNetwork.ToolTipTitle = Nothing
        '
        'butFeedBack
        '
        Me.butFeedBack.AltKey = Nothing
        Me.butFeedBack.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butFeedBack.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFeedBack.Image = CType(resources.GetObject("butFeedBack.Image"), System.Drawing.Image)
        Me.butFeedBack.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butFeedBack.SmallImage = Global.YAPM.My.Resources.Resources.information_frame
        Me.butFeedBack.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFeedBack.Tag = Nothing
        Me.butFeedBack.Text = "Feed back"
        Me.butFeedBack.ToolTip = "Feed back"
        Me.butFeedBack.ToolTipImage = Nothing
        Me.butFeedBack.ToolTipTitle = Nothing
        '
        'butHiddenProcesses
        '
        Me.butHiddenProcesses.AltKey = Nothing
        Me.butHiddenProcesses.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butHiddenProcesses.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butHiddenProcesses.Image = CType(resources.GetObject("butHiddenProcesses.Image"), System.Drawing.Image)
        Me.butHiddenProcesses.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butHiddenProcesses.SmallImage = CType(resources.GetObject("butHiddenProcesses.SmallImage"), System.Drawing.Image)
        Me.butHiddenProcesses.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butHiddenProcesses.Tag = Nothing
        Me.butHiddenProcesses.Text = "Show hidden processes"
        Me.butHiddenProcesses.ToolTip = "Show hidden processes"
        Me.butHiddenProcesses.ToolTipImage = Nothing
        Me.butHiddenProcesses.ToolTipTitle = Nothing
        '
        'butShowDepViewer
        '
        Me.butShowDepViewer.AltKey = Nothing
        Me.butShowDepViewer.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butShowDepViewer.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butShowDepViewer.Image = CType(resources.GetObject("butShowDepViewer.Image"), System.Drawing.Image)
        Me.butShowDepViewer.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butShowDepViewer.SmallImage = Global.YAPM.My.Resources.Resources.dllIcon
        Me.butShowDepViewer.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butShowDepViewer.Tag = Nothing
        Me.butShowDepViewer.Text = "Dependencies viewer"
        Me.butShowDepViewer.ToolTip = "Dependencies viewer"
        Me.butShowDepViewer.ToolTipImage = Nothing
        Me.butShowDepViewer.ToolTipTitle = Nothing
        '
        'butShowAllPendingTasks
        '
        Me.butShowAllPendingTasks.AltKey = Nothing
        Me.butShowAllPendingTasks.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butShowAllPendingTasks.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butShowAllPendingTasks.Image = CType(resources.GetObject("butShowAllPendingTasks.Image"), System.Drawing.Image)
        Me.butShowAllPendingTasks.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact
        Me.butShowAllPendingTasks.SmallImage = Global.YAPM.My.Resources.Resources.thread
        Me.butShowAllPendingTasks.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butShowAllPendingTasks.Tag = Nothing
        Me.butShowAllPendingTasks.Text = "Display pending tasks"
        Me.butShowAllPendingTasks.ToolTip = "Display pending tasks"
        Me.butShowAllPendingTasks.ToolTipImage = Nothing
        Me.butShowAllPendingTasks.ToolTipTitle = Nothing
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
        Me.butTaskRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butTaskShow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butTaskEndTask.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butTaskEndTask.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butTaskEndTask.Image = Global.YAPM.My.Resources.Resources.delete2
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
        Me.ProcessTab.Panels.Add(Me.panelProcessReport)
        Me.ProcessTab.Tag = Nothing
        Me.ProcessTab.Text = "Processes"
        '
        'RBProcessDisplay
        '
        Me.RBProcessDisplay.ButtonMoreEnabled = False
        Me.RBProcessDisplay.ButtonMoreVisible = False
        Me.RBProcessDisplay.Items.Add(Me.butProcessRerfresh)
        Me.RBProcessDisplay.Items.Add(Me.butProcessDisplayDetails)
        Me.RBProcessDisplay.Tag = Nothing
        Me.RBProcessDisplay.Text = "Display"
        '
        'butProcessRerfresh
        '
        Me.butProcessRerfresh.AltKey = Nothing
        Me.butProcessRerfresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butProcessDisplayDetails.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butProcessDisplayDetails.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessDisplayDetails.Image = Global.YAPM.My.Resources.Resources.showDetails
        Me.butProcessDisplayDetails.SmallImage = CType(resources.GetObject("butProcessDisplayDetails.SmallImage"), System.Drawing.Image)
        Me.butProcessDisplayDetails.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessDisplayDetails.Tag = Nothing
        Me.butProcessDisplayDetails.Text = "Show details"
        Me.butProcessDisplayDetails.ToolTip = Nothing
        Me.butProcessDisplayDetails.ToolTipImage = Nothing
        Me.butProcessDisplayDetails.ToolTipTitle = Nothing
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
        Me.RBProcessActions.Tag = Nothing
        Me.RBProcessActions.Text = "Common processes actions"
        '
        'butNewProcess
        '
        Me.butNewProcess.AltKey = Nothing
        Me.butNewProcess.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butKillProcess.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butKillProcess.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butKillProcess.Image = Global.YAPM.My.Resources.Resources.delete2
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
        Me.butStopProcess.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butStopProcess.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butStopProcess.Image = Global.YAPM.My.Resources.Resources.pause_32
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
        Me.butResumeProcess.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butProcessOtherActions.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butProcessOtherActions.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessOtherActions.DropDownItems.Add(Me.butProcessAffinity)
        Me.butProcessOtherActions.DropDownItems.Add(Me.butProcessReduceWS)
        Me.butProcessOtherActions.DropDownItems.Add(Me.butProcessDumpF)
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
        Me.butProcessAffinity.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        'butProcessReduceWS
        '
        Me.butProcessReduceWS.AltKey = Nothing
        Me.butProcessReduceWS.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.butProcessReduceWS.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessReduceWS.Image = CType(resources.GetObject("butProcessReduceWS.Image"), System.Drawing.Image)
        Me.butProcessReduceWS.SmallImage = CType(resources.GetObject("butProcessReduceWS.SmallImage"), System.Drawing.Image)
        Me.butProcessReduceWS.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessReduceWS.Tag = Nothing
        Me.butProcessReduceWS.Text = "Reduce working set size"
        Me.butProcessReduceWS.ToolTip = Nothing
        Me.butProcessReduceWS.ToolTipImage = Nothing
        Me.butProcessReduceWS.ToolTipTitle = Nothing
        '
        'butProcessDumpF
        '
        Me.butProcessDumpF.AltKey = Nothing
        Me.butProcessDumpF.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
        Me.butProcessDumpF.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessDumpF.Image = CType(resources.GetObject("butProcessDumpF.Image"), System.Drawing.Image)
        Me.butProcessDumpF.SmallImage = CType(resources.GetObject("butProcessDumpF.SmallImage"), System.Drawing.Image)
        Me.butProcessDumpF.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessDumpF.Tag = Nothing
        Me.butProcessDumpF.Text = "Create dump file..."
        Me.butProcessDumpF.ToolTip = Nothing
        Me.butProcessDumpF.ToolTipImage = Nothing
        Me.butProcessDumpF.ToolTipTitle = Nothing
        '
        'butProcessLimitCPU
        '
        Me.butProcessLimitCPU.AltKey = Nothing
        Me.butProcessLimitCPU.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        Me.butProcessShow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butProcessShowModules.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        Me.butProcessThreads.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        Me.butShowProcHandles.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        Me.butProcessWindows.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        Me.butProcessShowAll.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left
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
        Me.butProcessPriority.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butIdle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butBelowNormal.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butNormal.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butAboveNormal.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butHigh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butRealTime.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.RBProcessOnline.Items.Add(Me.butProcessGoogle)
        Me.RBProcessOnline.Tag = Nothing
        Me.RBProcessOnline.Text = "Online"
        '
        'butProcessGoogle
        '
        Me.butProcessGoogle.AltKey = Nothing
        Me.butProcessGoogle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butProcessGoogle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessGoogle.Image = CType(resources.GetObject("butProcessGoogle.Image"), System.Drawing.Image)
        Me.butProcessGoogle.SmallImage = CType(resources.GetObject("butProcessGoogle.SmallImage"), System.Drawing.Image)
        Me.butProcessGoogle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessGoogle.Tag = Nothing
        Me.butProcessGoogle.Text = "Internet search"
        Me.butProcessGoogle.ToolTip = Nothing
        Me.butProcessGoogle.ToolTipImage = Nothing
        Me.butProcessGoogle.ToolTipTitle = Nothing
        '
        'panelProcessReport
        '
        Me.panelProcessReport.ButtonMoreEnabled = False
        Me.panelProcessReport.ButtonMoreVisible = False
        Me.panelProcessReport.Items.Add(Me.butSaveProcessReport)
        Me.panelProcessReport.Tag = Nothing
        Me.panelProcessReport.Text = "Report"
        '
        'butSaveProcessReport
        '
        Me.butSaveProcessReport.AltKey = Nothing
        Me.butSaveProcessReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butSaveProcessReport.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSaveProcessReport.Image = CType(resources.GetObject("butSaveProcessReport.Image"), System.Drawing.Image)
        Me.butSaveProcessReport.SmallImage = CType(resources.GetObject("butSaveProcessReport.SmallImage"), System.Drawing.Image)
        Me.butSaveProcessReport.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSaveProcessReport.Tag = Nothing
        Me.butSaveProcessReport.Text = "Save report"
        Me.butSaveProcessReport.ToolTip = Nothing
        Me.butSaveProcessReport.ToolTipImage = Nothing
        Me.butSaveProcessReport.ToolTipTitle = Nothing
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
        Me.RBModuleActions.Items.Add(Me.butModuleViewModuleDep)
        Me.RBModuleActions.Tag = Nothing
        Me.RBModuleActions.Text = "Actions"
        '
        'butModuleRefresh
        '
        Me.butModuleRefresh.AltKey = Nothing
        Me.butModuleRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butModuleUnload.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        'butModuleViewModuleDep
        '
        Me.butModuleViewModuleDep.AltKey = Nothing
        Me.butModuleViewModuleDep.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butModuleViewModuleDep.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butModuleViewModuleDep.Image = CType(resources.GetObject("butModuleViewModuleDep.Image"), System.Drawing.Image)
        Me.butModuleViewModuleDep.SmallImage = CType(resources.GetObject("butModuleViewModuleDep.SmallImage"), System.Drawing.Image)
        Me.butModuleViewModuleDep.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butModuleViewModuleDep.Tag = Nothing
        Me.butModuleViewModuleDep.Text = "View dependencies"
        Me.butModuleViewModuleDep.ToolTip = Nothing
        Me.butModuleViewModuleDep.ToolTipImage = Nothing
        Me.butModuleViewModuleDep.ToolTipTitle = Nothing
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
        Me.butModulesSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butModuleGoogle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butModuleGoogle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butModuleGoogle.Image = CType(resources.GetObject("butModuleGoogle.Image"), System.Drawing.Image)
        Me.butModuleGoogle.SmallImage = CType(resources.GetObject("butModuleGoogle.SmallImage"), System.Drawing.Image)
        Me.butModuleGoogle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butModuleGoogle.Tag = Nothing
        Me.butModuleGoogle.Text = "Internet search"
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
        Me.butThreadRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadKill.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butThreadKill.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadKill.Image = Global.YAPM.My.Resources.Resources.delete2
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
        Me.butThreadStop.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butThreadStop.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butThreadStop.Image = Global.YAPM.My.Resources.Resources.stop32
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
        Me.butThreadResume.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPriority.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPidle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPlowest.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPbelow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPnormal.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPabove.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPhighest.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadPcritical.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butThreadSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butHandleRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butHandleClose.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butHandlesSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowFind.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowVisibility.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowShow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowHide.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowEnable.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowDisable.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowBringToFront.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowDoNotBringToFront.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowSetAsForeground.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowSetAsActive.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowMinimize.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowMaximize.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowFlash.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowStopFlashing.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowCaption.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowOpacity.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowClose.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butWindowClose.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butWindowClose.Image = Global.YAPM.My.Resources.Resources.delete2
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
        Me.butWindowPositionSize.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWindowSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butMonitoringAdd.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butMonitoringRemove.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butMonitorStart.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butMonitorStop.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butMonitorStop.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butMonitorStop.Enabled = False
        Me.butMonitorStop.Image = Global.YAPM.My.Resources.Resources.stop32
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
        Me.butMonitorSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.RBServiceDisplay.Items.Add(Me.butServiceDetails)
        Me.RBServiceDisplay.Tag = Nothing
        Me.RBServiceDisplay.Text = "Display"
        '
        'butServiceRefresh
        '
        Me.butServiceRefresh.AltKey = Nothing
        Me.butServiceRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        'butServiceDetails
        '
        Me.butServiceDetails.AltKey = Nothing
        Me.butServiceDetails.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butServiceDetails.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceDetails.Image = Global.YAPM.My.Resources.Resources.showDetails
        Me.butServiceDetails.SmallImage = CType(resources.GetObject("butServiceDetails.SmallImage"), System.Drawing.Image)
        Me.butServiceDetails.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceDetails.Tag = Nothing
        Me.butServiceDetails.Text = "Show details"
        Me.butServiceDetails.ToolTip = Nothing
        Me.butServiceDetails.ToolTipImage = Nothing
        Me.butServiceDetails.ToolTipTitle = Nothing
        '
        'RBServiceAction
        '
        Me.RBServiceAction.ButtonMoreEnabled = False
        Me.RBServiceAction.ButtonMoreVisible = False
        Me.RBServiceAction.Items.Add(Me.butStopService)
        Me.RBServiceAction.Items.Add(Me.butStartService)
        Me.RBServiceAction.Items.Add(Me.RibbonSeparator1)
        Me.RBServiceAction.Items.Add(Me.butPauseService)
        Me.RBServiceAction.Items.Add(Me.butResumeService)
        Me.RBServiceAction.Tag = Nothing
        Me.RBServiceAction.Text = "Service actions"
        '
        'butStopService
        '
        Me.butStopService.AltKey = Nothing
        Me.butStopService.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butStopService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butStopService.Image = Global.YAPM.My.Resources.Resources.stop32
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
        Me.butStartService.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        'RibbonSeparator1
        '
        Me.RibbonSeparator1.AltKey = Nothing
        Me.RibbonSeparator1.Image = Nothing
        Me.RibbonSeparator1.Tag = Nothing
        Me.RibbonSeparator1.Text = Nothing
        Me.RibbonSeparator1.ToolTip = Nothing
        Me.RibbonSeparator1.ToolTipImage = Nothing
        Me.RibbonSeparator1.ToolTipTitle = Nothing
        '
        'butPauseService
        '
        Me.butPauseService.AltKey = Nothing
        Me.butPauseService.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butPauseService.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butPauseService.Image = Global.YAPM.My.Resources.Resources.pause_32
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
        Me.butResumeService.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butServiceStartType.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butAutomaticStart.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butOnDemandStart.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butDisabledStart.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butServiceFileProp.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butServiceOpenDir.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butServiceFileDetails.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butServiceGoogle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butServiceGoogle.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butServiceGoogle.Image = CType(resources.GetObject("butServiceGoogle.Image"), System.Drawing.Image)
        Me.butServiceGoogle.SmallImage = CType(resources.GetObject("butServiceGoogle.SmallImage"), System.Drawing.Image)
        Me.butServiceGoogle.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butServiceGoogle.Tag = Nothing
        Me.butServiceGoogle.Text = "Internet search"
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
        Me.butServiceReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butNetworkRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butOpenFile.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileRefresh.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileRelease.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.RBFileDelete.Tag = Nothing
        Me.RBFileDelete.Text = "Delete"
        '
        'butMoveFileToTrash
        '
        Me.butMoveFileToTrash.AltKey = Nothing
        Me.butMoveFileToTrash.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butDeleteFile.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileGoogleSearch.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butFileGoogleSearch.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butFileGoogleSearch.Enabled = False
        Me.butFileGoogleSearch.Image = CType(resources.GetObject("butFileGoogleSearch.Image"), System.Drawing.Image)
        Me.butFileGoogleSearch.SmallImage = CType(resources.GetObject("butFileGoogleSearch.SmallImage"), System.Drawing.Image)
        Me.butFileGoogleSearch.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butFileGoogleSearch.Tag = Nothing
        Me.butFileGoogleSearch.Text = "Internet search"
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
        Me.butFileProperties.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileOpenDir.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileShowFolderProperties.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileOthersActions.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileRename.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileCopy.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileMove.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileOpen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileSeeStrings.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileEncrypt.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butFileDecrypt.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butSearchGo.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butSearchSaveReport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butUpdate.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butDonate.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butAbout.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butWebite.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butProjectPage.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butDownload.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butPreferences.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        Me.butAlwaysDisplay.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
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
        'imgMonitor
        '
        Me.imgMonitor.ImageStream = CType(resources.GetObject("imgMonitor.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMonitor.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMonitor.Images.SetKeyName(0, "down")
        Me.imgMonitor.Images.SetKeyName(1, "sub")
        Me.imgMonitor.Images.SetKeyName(2, "exe")
        '
        'timerMonitoring
        '
        Me.timerMonitoring.Interval = 1000
        '
        'timerTask
        '
        Me.timerTask.Interval = 1000
        '
        'timerTrayIcon
        '
        Me.timerTrayIcon.Interval = 1000
        '
        'butProcessPermuteLvTv
        '
        Me.butProcessPermuteLvTv.AltKey = Nothing
        Me.butProcessPermuteLvTv.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Down
        Me.butProcessPermuteLvTv.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butProcessPermuteLvTv.Enabled = False
        Me.butProcessPermuteLvTv.Image = Global.YAPM.My.Resources.Resources.tv2
        Me.butProcessPermuteLvTv.SmallImage = CType(resources.GetObject("butProcessPermuteLvTv.SmallImage"), System.Drawing.Image)
        Me.butProcessPermuteLvTv.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butProcessPermuteLvTv.Tag = Nothing
        Me.butProcessPermuteLvTv.Text = "Listview"
        Me.butProcessPermuteLvTv.ToolTip = Nothing
        Me.butProcessPermuteLvTv.ToolTipImage = Nothing
        Me.butProcessPermuteLvTv.ToolTipTitle = Nothing
        '
        '_main
        '
        Me._main.Dock = System.Windows.Forms.DockStyle.Fill
        Me._main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me._main.IsSplitterFixed = True
        Me._main.Location = New System.Drawing.Point(0, 0)
        Me._main.Name = "_main"
        Me._main.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '_main.Panel1
        '
        Me._main.Panel1.Controls.Add(Me.Ribbon)
        '
        '_main.Panel2
        '
        Me._main.Panel2.Controls.Add(Me.containerSystemMenu)
        Me._main.Size = New System.Drawing.Size(866, 533)
        Me._main.SplitterDistance = 138
        Me._main.TabIndex = 57
        '
        'containerSystemMenu
        '
        Me.containerSystemMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerSystemMenu.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerSystemMenu.IsSplitterFixed = True
        Me.containerSystemMenu.Location = New System.Drawing.Point(0, 0)
        Me.containerSystemMenu.Name = "containerSystemMenu"
        Me.containerSystemMenu.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.containerSystemMenu.Panel1Collapsed = True
        '
        'containerSystemMenu.Panel2
        '
        Me.containerSystemMenu.Panel2.Controls.Add(Me._tab)
        Me.containerSystemMenu.Size = New System.Drawing.Size(866, 391)
        Me.containerSystemMenu.SplitterDistance = 25
        Me.containerSystemMenu.TabIndex = 0
        '
        '_tab
        '
        Me._tab.Controls.Add(Me.pageTasks)
        Me._tab.Controls.Add(Me.pageProcesses)
        Me._tab.Controls.Add(Me.pageModules)
        Me._tab.Controls.Add(Me.pageThreads)
        Me._tab.Controls.Add(Me.pageHandles)
        Me._tab.Controls.Add(Me.pageWindows)
        Me._tab.Controls.Add(Me.pageMonitor)
        Me._tab.Controls.Add(Me.pageServices)
        Me._tab.Controls.Add(Me.pageNetwork)
        Me._tab.Controls.Add(Me.pageFile)
        Me._tab.Controls.Add(Me.pageSearch)
        Me._tab.Controls.Add(Me.pageHelp)
        Me._tab.Dock = System.Windows.Forms.DockStyle.Fill
        Me._tab.Location = New System.Drawing.Point(0, 0)
        Me._tab.Name = "_tab"
        Me._tab.SelectedIndex = 0
        Me._tab.Size = New System.Drawing.Size(866, 391)
        Me._tab.TabIndex = 3
        '
        'pageTasks
        '
        Me.pageTasks.BackColor = System.Drawing.Color.Transparent
        Me.pageTasks.Controls.Add(Me.panelMain13)
        Me.pageTasks.Location = New System.Drawing.Point(4, 22)
        Me.pageTasks.Name = "pageTasks"
        Me.pageTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.pageTasks.Size = New System.Drawing.Size(858, 365)
        Me.pageTasks.TabIndex = 11
        Me.pageTasks.Text = "Tasks"
        Me.pageTasks.UseVisualStyleBackColor = True
        '
        'panelMain13
        '
        Me.panelMain13.Controls.Add(Me.SplitContainerTask)
        Me.panelMain13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain13.Location = New System.Drawing.Point(3, 3)
        Me.panelMain13.Name = "panelMain13"
        Me.panelMain13.Size = New System.Drawing.Size(852, 359)
        Me.panelMain13.TabIndex = 56
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
        Me.SplitContainerTask.Size = New System.Drawing.Size(852, 359)
        Me.SplitContainerTask.SplitterDistance = 25
        Me.SplitContainerTask.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(65, 13)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "Search task"
        '
        'lblTaskCountResult
        '
        Me.lblTaskCountResult.AutoSize = True
        Me.lblTaskCountResult.Location = New System.Drawing.Point(393, 6)
        Me.lblTaskCountResult.Name = "lblTaskCountResult"
        Me.lblTaskCountResult.Size = New System.Drawing.Size(56, 13)
        Me.lblTaskCountResult.TabIndex = 12
        Me.lblTaskCountResult.Text = "0 result(s)"
        '
        'txtSearchTask
        '
        Me.txtSearchTask.Location = New System.Drawing.Point(72, 1)
        Me.txtSearchTask.Name = "txtSearchTask"
        Me.txtSearchTask.Size = New System.Drawing.Size(312, 22)
        Me.txtSearchTask.TabIndex = 11
        '
        'lvTask
        '
        Me.lvTask.AllowColumnReorder = True
        Me.lvTask.CatchErrors = False
        Me.lvTask.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader62, Me.ColumnHeader63, Me.ColumnHeader64})
        CConnection1.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvTask.ConnectionObj = CConnection1
        Me.lvTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTask.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvTask.FullRowSelect = True
        ListViewGroup1.Header = "Tasks"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvTask.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvTask.HideSelection = False
        Me.lvTask.Location = New System.Drawing.Point(0, 0)
        Me.lvTask.Name = "lvTask"
        Me.lvTask.OverriddenDoubleBuffered = True
        Me.lvTask.ReorganizeColumns = True
        Me.lvTask.Size = New System.Drawing.Size(852, 330)
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
        'pageProcesses
        '
        Me.pageProcesses.BackColor = System.Drawing.Color.Transparent
        Me.pageProcesses.Controls.Add(Me.containerProcessPage)
        Me.pageProcesses.Location = New System.Drawing.Point(4, 22)
        Me.pageProcesses.Name = "pageProcesses"
        Me.pageProcesses.Padding = New System.Windows.Forms.Padding(3)
        Me.pageProcesses.Size = New System.Drawing.Size(858, 365)
        Me.pageProcesses.TabIndex = 0
        Me.pageProcesses.Text = "Processes"
        Me.pageProcesses.UseVisualStyleBackColor = True
        '
        'containerProcessPage
        '
        Me.containerProcessPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerProcessPage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerProcessPage.IsSplitterFixed = True
        Me.containerProcessPage.Location = New System.Drawing.Point(3, 3)
        Me.containerProcessPage.Name = "containerProcessPage"
        Me.containerProcessPage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'containerProcessPage.Panel1
        '
        Me.containerProcessPage.Panel1.Controls.Add(Me.panelMenu)
        '
        'containerProcessPage.Panel2
        '
        Me.containerProcessPage.Panel2.Controls.Add(Me.panelMain)
        Me.containerProcessPage.Size = New System.Drawing.Size(852, 359)
        Me.containerProcessPage.SplitterDistance = 25
        Me.containerProcessPage.TabIndex = 0
        '
        'panelMenu
        '
        Me.panelMenu.Controls.Add(Me.Label3)
        Me.panelMenu.Controls.Add(Me.lblResCount)
        Me.panelMenu.Controls.Add(Me.txtSearch)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(852, 25)
        Me.panelMenu.TabIndex = 48
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Search process"
        '
        'lblResCount
        '
        Me.lblResCount.AutoSize = True
        Me.lblResCount.Location = New System.Drawing.Point(394, 6)
        Me.lblResCount.Name = "lblResCount"
        Me.lblResCount.Size = New System.Drawing.Size(56, 13)
        Me.lblResCount.TabIndex = 2
        Me.lblResCount.Text = "0 result(s)"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(90, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(298, 22)
        Me.txtSearch.TabIndex = 1
        '
        'panelMain
        '
        Me.panelMain.Controls.Add(Me.SplitContainerProcess)
        Me.panelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain.Location = New System.Drawing.Point(0, 0)
        Me.panelMain.Name = "panelMain"
        Me.panelMain.Size = New System.Drawing.Size(852, 330)
        Me.panelMain.TabIndex = 4
        '
        'SplitContainerProcess
        '
        Me.SplitContainerProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerProcess.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerProcess.IsSplitterFixed = True
        Me.SplitContainerProcess.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerProcess.Name = "SplitContainerProcess"
        Me.SplitContainerProcess.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerProcess.Panel1
        '
        Me.SplitContainerProcess.Panel1.Controls.Add(Me.SplitContainerTvLv)
        Me.SplitContainerProcess.Panel2Collapsed = True
        Me.SplitContainerProcess.Size = New System.Drawing.Size(852, 330)
        Me.SplitContainerProcess.SplitterDistance = 285
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
        Me.SplitContainerTvLv.Size = New System.Drawing.Size(852, 330)
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
        TreeNode1.Name = "4"
        TreeNode1.Tag = "4"
        TreeNode1.Text = "System"
        TreeNode2.Name = "0"
        TreeNode2.Tag = "0"
        TreeNode2.Text = "[System process]"
        Me.tvProc.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2})
        Me.tvProc.SelectedImageIndex = 0
        Me.tvProc.Size = New System.Drawing.Size(149, 100)
        Me.tvProc.TabIndex = 4
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.CatchErrors = False
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c7, Me.c8, Me.c9, Me.c10, Me.ColumnHeader20})
        CConnection2.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvProcess.ConnectionObj = CConnection2
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.EnumMethod = YAPM.asyncCallbackProcEnumerate.ProcessEnumMethode.VisibleProcesses
        Me.lvProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcess.FullRowSelect = True
        ListViewGroup3.Header = "Processes"
        ListViewGroup3.Name = "gpOther"
        ListViewGroup4.Header = "Search result"
        ListViewGroup4.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.Location = New System.Drawing.Point(0, 0)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.ReorganizeColumns = True
        Me.lvProcess.Size = New System.Drawing.Size(852, 330)
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
        Me.c4.DisplayIndex = 5
        Me.c4.Text = "TotalCpuTime"
        Me.c4.Width = 80
        '
        'c5
        '
        Me.c5.DisplayIndex = 6
        Me.c5.Text = "WorkingSet"
        Me.c5.Width = 80
        '
        'c7
        '
        Me.c7.DisplayIndex = 7
        Me.c7.Text = "Priority"
        Me.c7.Width = 70
        '
        'c8
        '
        Me.c8.DisplayIndex = 8
        Me.c8.Text = "Path"
        Me.c8.Width = 350
        '
        'c9
        '
        Me.c9.DisplayIndex = 9
        Me.c9.Text = "StartTime"
        Me.c9.Width = 250
        '
        'c10
        '
        Me.c10.DisplayIndex = 3
        Me.c10.Text = "CpuUsage"
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.DisplayIndex = 4
        Me.ColumnHeader20.Text = "AverageCpuUsage"
        '
        'pageModules
        '
        Me.pageModules.BackColor = System.Drawing.Color.Transparent
        Me.pageModules.Controls.Add(Me.panelMain11)
        Me.pageModules.Location = New System.Drawing.Point(4, 22)
        Me.pageModules.Name = "pageModules"
        Me.pageModules.Padding = New System.Windows.Forms.Padding(3)
        Me.pageModules.Size = New System.Drawing.Size(858, 365)
        Me.pageModules.TabIndex = 10
        Me.pageModules.Text = "Modules"
        Me.pageModules.UseVisualStyleBackColor = True
        '
        'panelMain11
        '
        Me.panelMain11.Controls.Add(Me.splitModule)
        Me.panelMain11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain11.Location = New System.Drawing.Point(3, 3)
        Me.panelMain11.Name = "panelMain11"
        Me.panelMain11.Size = New System.Drawing.Size(852, 359)
        Me.panelMain11.TabIndex = 54
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
        Me.splitModule.Size = New System.Drawing.Size(852, 359)
        Me.splitModule.SplitterDistance = 214
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
        Me.SplitContainerModules.Size = New System.Drawing.Size(852, 214)
        Me.SplitContainerModules.SplitterDistance = 25
        Me.SplitContainerModules.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Search module"
        '
        'lblModulesCount
        '
        Me.lblModulesCount.AutoSize = True
        Me.lblModulesCount.Location = New System.Drawing.Point(393, 6)
        Me.lblModulesCount.Name = "lblModulesCount"
        Me.lblModulesCount.Size = New System.Drawing.Size(56, 13)
        Me.lblModulesCount.TabIndex = 6
        Me.lblModulesCount.Text = "0 result(s)"
        '
        'txtSearchModule
        '
        Me.txtSearchModule.Location = New System.Drawing.Point(86, 1)
        Me.txtSearchModule.Name = "txtSearchModule"
        Me.txtSearchModule.Size = New System.Drawing.Size(298, 22)
        Me.txtSearchModule.TabIndex = 5
        '
        'lvModules
        '
        Me.lvModules.AllowColumnReorder = True
        Me.lvModules.CatchErrors = False
        Me.lvModules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader29, Me.ColumnHeader43, Me.ColumnHeader44, Me.ColumnHeader45, Me.ColumnHeader46, Me.ColumnHeader18})
        CConnection3.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvModules.ConnectionObj = CConnection3
        Me.lvModules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvModules.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvModules.FullRowSelect = True
        ListViewGroup5.Header = "Modules"
        ListViewGroup5.Name = "gpOther"
        ListViewGroup6.Header = "Search result"
        ListViewGroup6.Name = "gpSearchResults"
        Me.lvModules.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup5, ListViewGroup6})
        Me.lvModules.HideSelection = False
        Me.lvModules.Location = New System.Drawing.Point(0, 0)
        Me.lvModules.Name = "lvModules"
        Me.lvModules.OverriddenDoubleBuffered = True
        Me.lvModules.ProcessId = Nothing
        Me.lvModules.ReorganizeColumns = True
        Me.lvModules.Size = New System.Drawing.Size(852, 185)
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
        Me.ColumnHeader18.Text = "ProcessId"
        Me.ColumnHeader18.Width = 100
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
        Me.rtb6.Size = New System.Drawing.Size(852, 141)
        Me.rtb6.TabIndex = 8
        Me.rtb6.Text = "Click on an item to get additionnal informations"
        '
        'pageThreads
        '
        Me.pageThreads.BackColor = System.Drawing.Color.Transparent
        Me.pageThreads.Controls.Add(Me.panelMain9)
        Me.pageThreads.Location = New System.Drawing.Point(4, 22)
        Me.pageThreads.Name = "pageThreads"
        Me.pageThreads.Padding = New System.Windows.Forms.Padding(3)
        Me.pageThreads.Size = New System.Drawing.Size(858, 365)
        Me.pageThreads.TabIndex = 8
        Me.pageThreads.Text = "Threads"
        Me.pageThreads.UseVisualStyleBackColor = True
        '
        'panelMain9
        '
        Me.panelMain9.Controls.Add(Me.splitThreads)
        Me.panelMain9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain9.Location = New System.Drawing.Point(3, 3)
        Me.panelMain9.Name = "panelMain9"
        Me.panelMain9.Size = New System.Drawing.Size(852, 359)
        Me.panelMain9.TabIndex = 52
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
        Me.splitThreads.Size = New System.Drawing.Size(852, 359)
        Me.splitThreads.SplitterDistance = 235
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
        Me.SplitContainerThreads.Size = New System.Drawing.Size(852, 235)
        Me.SplitContainerThreads.SplitterDistance = 25
        Me.SplitContainerThreads.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 13)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Search thread"
        '
        'lblThreadResults
        '
        Me.lblThreadResults.AutoSize = True
        Me.lblThreadResults.Location = New System.Drawing.Point(393, 6)
        Me.lblThreadResults.Name = "lblThreadResults"
        Me.lblThreadResults.Size = New System.Drawing.Size(56, 13)
        Me.lblThreadResults.TabIndex = 9
        Me.lblThreadResults.Text = "0 result(s)"
        '
        'txtSearchThread
        '
        Me.txtSearchThread.Location = New System.Drawing.Point(86, 1)
        Me.txtSearchThread.Name = "txtSearchThread"
        Me.txtSearchThread.Size = New System.Drawing.Size(298, 22)
        Me.txtSearchThread.TabIndex = 8
        '
        'lvThreads
        '
        Me.lvThreads.AllowColumnReorder = True
        Me.lvThreads.CatchErrors = False
        Me.lvThreads.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader32, Me.ColumnHeader33, Me.ColumnHeader34, Me.ColumnHeader35, Me.ColumnHeader36, Me.ColumnHeader37, Me.ColumnHeader38, Me.ColumnHeader6, Me.ColumnHeader1})
        CConnection4.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvThreads.ConnectionObj = CConnection4
        Me.lvThreads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvThreads.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvThreads.FullRowSelect = True
        ListViewGroup7.Header = "Threads"
        ListViewGroup7.Name = "gpOther"
        ListViewGroup8.Header = "Search results"
        ListViewGroup8.Name = "gpSearchResults"
        Me.lvThreads.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup7, ListViewGroup8})
        Me.lvThreads.HideSelection = False
        Me.lvThreads.Location = New System.Drawing.Point(0, 0)
        Me.lvThreads.Name = "lvThreads"
        Me.lvThreads.OverriddenDoubleBuffered = True
        Me.lvThreads.ProcessId = Nothing
        Me.lvThreads.ReorganizeColumns = True
        Me.lvThreads.Size = New System.Drawing.Size(852, 206)
        Me.lvThreads.TabIndex = 5
        Me.lvThreads.UseCompatibleStateImageBehavior = False
        Me.lvThreads.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader32
        '
        Me.ColumnHeader32.Text = "Id"
        '
        'ColumnHeader33
        '
        Me.ColumnHeader33.Text = "ProcessId"
        Me.ColumnHeader33.Width = 78
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
        Me.ColumnHeader37.Text = "CreateTime"
        Me.ColumnHeader37.Width = 119
        '
        'ColumnHeader38
        '
        Me.ColumnHeader38.Text = "TotalTime"
        Me.ColumnHeader38.Width = 200
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "StartAddress"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ContextSwitchCount"
        Me.ColumnHeader1.Width = 200
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
        Me.rtb4.Size = New System.Drawing.Size(852, 120)
        Me.rtb4.TabIndex = 7
        Me.rtb4.Text = "Click on a thread to get additionnal informations"
        '
        'pageHandles
        '
        Me.pageHandles.BackColor = System.Drawing.Color.Transparent
        Me.pageHandles.Controls.Add(Me.panelMain7)
        Me.pageHandles.Location = New System.Drawing.Point(4, 22)
        Me.pageHandles.Name = "pageHandles"
        Me.pageHandles.Padding = New System.Windows.Forms.Padding(3)
        Me.pageHandles.Size = New System.Drawing.Size(858, 365)
        Me.pageHandles.TabIndex = 6
        Me.pageHandles.Text = "Handles"
        Me.pageHandles.UseVisualStyleBackColor = True
        '
        'panelMain7
        '
        Me.panelMain7.Controls.Add(Me.SplitContainerHandles)
        Me.panelMain7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain7.Location = New System.Drawing.Point(3, 3)
        Me.panelMain7.Name = "panelMain7"
        Me.panelMain7.Size = New System.Drawing.Size(852, 359)
        Me.panelMain7.TabIndex = 50
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
        Me.SplitContainerHandles.Size = New System.Drawing.Size(852, 359)
        Me.SplitContainerHandles.SplitterDistance = 25
        Me.SplitContainerHandles.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(3, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Search handle"
        '
        'lblHandlesCount
        '
        Me.lblHandlesCount.AutoSize = True
        Me.lblHandlesCount.Location = New System.Drawing.Point(393, 6)
        Me.lblHandlesCount.Name = "lblHandlesCount"
        Me.lblHandlesCount.Size = New System.Drawing.Size(56, 13)
        Me.lblHandlesCount.TabIndex = 12
        Me.lblHandlesCount.Text = "0 result(s)"
        '
        'txtSearchHandle
        '
        Me.txtSearchHandle.Location = New System.Drawing.Point(86, 1)
        Me.txtSearchHandle.Name = "txtSearchHandle"
        Me.txtSearchHandle.Size = New System.Drawing.Size(298, 22)
        Me.txtSearchHandle.TabIndex = 11
        '
        'lvHandles
        '
        Me.lvHandles.AllowColumnReorder = True
        Me.lvHandles.CatchErrors = False
        Me.lvHandles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader24, Me.ColumnHeader25, Me.ColumnHeader26, Me.ColumnHeader27, Me.ColumnHeader28, Me.ColumnHeader15, Me.ColumnHeader16})
        CConnection5.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvHandles.ConnectionObj = CConnection5
        Me.lvHandles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvHandles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvHandles.FullRowSelect = True
        ListViewGroup9.Header = "Handles"
        ListViewGroup9.Name = "gpOther"
        ListViewGroup10.Header = "Search result"
        ListViewGroup10.Name = "gpSearch"
        Me.lvHandles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup9, ListViewGroup10})
        Me.lvHandles.HideSelection = False
        Me.lvHandles.Location = New System.Drawing.Point(0, 0)
        Me.lvHandles.Name = "lvHandles"
        Me.lvHandles.OverriddenDoubleBuffered = True
        Me.lvHandles.ProcessId = Nothing
        Me.lvHandles.ReorganizeColumns = True
        Me.lvHandles.ShowUnnamed = False
        Me.lvHandles.Size = New System.Drawing.Size(852, 330)
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
        'pageWindows
        '
        Me.pageWindows.BackColor = System.Drawing.Color.Transparent
        Me.pageWindows.Controls.Add(Me.panelMain10)
        Me.pageWindows.Location = New System.Drawing.Point(4, 22)
        Me.pageWindows.Name = "pageWindows"
        Me.pageWindows.Padding = New System.Windows.Forms.Padding(3)
        Me.pageWindows.Size = New System.Drawing.Size(858, 365)
        Me.pageWindows.TabIndex = 9
        Me.pageWindows.Text = "Windows"
        Me.pageWindows.UseVisualStyleBackColor = True
        '
        'panelMain10
        '
        Me.panelMain10.Controls.Add(Me.splitContainerWindows)
        Me.panelMain10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain10.Location = New System.Drawing.Point(3, 3)
        Me.panelMain10.Name = "panelMain10"
        Me.panelMain10.Size = New System.Drawing.Size(852, 359)
        Me.panelMain10.TabIndex = 53
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
        Me.splitContainerWindows.Size = New System.Drawing.Size(852, 359)
        Me.splitContainerWindows.SplitterDistance = 214
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
        Me.SplitContainerWindows2.Size = New System.Drawing.Size(852, 214)
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
        Me.chkAllWindows.Size = New System.Drawing.Size(199, 17)
        Me.chkAllWindows.TabIndex = 11
        Me.chkAllWindows.Text = "Display windows without caption"
        Me.chkAllWindows.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Search window"
        '
        'lblWindowsCount
        '
        Me.lblWindowsCount.AutoSize = True
        Me.lblWindowsCount.Location = New System.Drawing.Point(393, 6)
        Me.lblWindowsCount.Name = "lblWindowsCount"
        Me.lblWindowsCount.Size = New System.Drawing.Size(56, 13)
        Me.lblWindowsCount.TabIndex = 9
        Me.lblWindowsCount.Text = "0 result(s)"
        '
        'txtSearchWindow
        '
        Me.txtSearchWindow.Location = New System.Drawing.Point(90, 1)
        Me.txtSearchWindow.Name = "txtSearchWindow"
        Me.txtSearchWindow.Size = New System.Drawing.Size(294, 22)
        Me.txtSearchWindow.TabIndex = 8
        '
        'lvWindows
        '
        Me.lvWindows.AllowColumnReorder = True
        Me.lvWindows.CatchErrors = False
        Me.lvWindows.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader30, Me.ColumnHeader31, Me.ColumnHeader39, Me.ColumnHeader40, Me.ColumnHeader41, Me.ColumnHeader42})
        CConnection6.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvWindows.ConnectionObj = CConnection6
        Me.lvWindows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvWindows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvWindows.FullRowSelect = True
        ListViewGroup11.Header = "Windows"
        ListViewGroup11.Name = "gpOther"
        ListViewGroup12.Header = "Search results"
        ListViewGroup12.Name = "gpSearchResults"
        Me.lvWindows.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup11, ListViewGroup12})
        Me.lvWindows.HideSelection = False
        Me.lvWindows.Location = New System.Drawing.Point(0, 0)
        Me.lvWindows.Name = "lvWindows"
        Me.lvWindows.OverriddenDoubleBuffered = True
        Me.lvWindows.ProcessId = Nothing
        Me.lvWindows.ReorganizeColumns = True
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowUnNamed = False
        Me.lvWindows.Size = New System.Drawing.Size(852, 185)
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
        Me.rtb5.Size = New System.Drawing.Size(852, 141)
        Me.rtb5.TabIndex = 8
        Me.rtb5.Text = "Click on an item to get additionnal informations"
        '
        'pageMonitor
        '
        Me.pageMonitor.BackColor = System.Drawing.Color.Transparent
        Me.pageMonitor.Controls.Add(Me.panelMain8)
        Me.pageMonitor.Location = New System.Drawing.Point(4, 22)
        Me.pageMonitor.Name = "pageMonitor"
        Me.pageMonitor.Padding = New System.Windows.Forms.Padding(3)
        Me.pageMonitor.Size = New System.Drawing.Size(858, 365)
        Me.pageMonitor.TabIndex = 7
        Me.pageMonitor.Text = "Monitor"
        Me.pageMonitor.UseVisualStyleBackColor = True
        '
        'panelMain8
        '
        Me.panelMain8.Controls.Add(Me.splitMonitor)
        Me.panelMain8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain8.Location = New System.Drawing.Point(3, 3)
        Me.panelMain8.Name = "panelMain8"
        Me.panelMain8.Size = New System.Drawing.Size(852, 359)
        Me.panelMain8.TabIndex = 51
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
        Me.splitMonitor.Size = New System.Drawing.Size(852, 359)
        Me.splitMonitor.SplitterDistance = 281
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
        TreeNode3.ImageIndex = 1
        TreeNode3.Name = "processes"
        TreeNode3.SelectedImageIndex = 1
        TreeNode3.Text = "Items"
        Me.tvMonitor.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode3})
        Me.tvMonitor.SelectedImageIndex = 0
        Me.tvMonitor.Size = New System.Drawing.Size(281, 359)
        Me.tvMonitor.TabIndex = 0
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
        Me.splitMonitor2.Panel1.Controls.Add(Me.lvMonitorReport)
        '
        'splitMonitor2.Panel2
        '
        Me.splitMonitor2.Panel2.Controls.Add(Me.splitMonitor3)
        Me.splitMonitor2.Size = New System.Drawing.Size(567, 359)
        Me.splitMonitor2.SplitterDistance = 142
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
        Me.txtMonitoringLog.Size = New System.Drawing.Size(567, 142)
        Me.txtMonitoringLog.TabIndex = 0
        Me.txtMonitoringLog.Text = "No process monitored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click on 'Add' button to monitor a process."
        '
        'lvMonitorReport
        '
        Me.lvMonitorReport.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader22, Me.ColumnHeader2, Me.ColumnHeader23, Me.ColumnHeader47, Me.ColumnHeader48, Me.ColumnHeader49})
        Me.lvMonitorReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMonitorReport.FullRowSelect = True
        Me.lvMonitorReport.Location = New System.Drawing.Point(0, 0)
        Me.lvMonitorReport.Name = "lvMonitorReport"
        Me.lvMonitorReport.OverriddenDoubleBuffered = False
        Me.lvMonitorReport.Size = New System.Drawing.Size(567, 142)
        Me.lvMonitorReport.TabIndex = 1
        Me.lvMonitorReport.UseCompatibleStateImageBehavior = False
        Me.lvMonitorReport.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Counter"
        Me.ColumnHeader22.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "MachineName"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "Creation date"
        Me.ColumnHeader23.Width = 100
        '
        'ColumnHeader47
        '
        Me.ColumnHeader47.Text = "Last start"
        Me.ColumnHeader47.Width = 100
        '
        'ColumnHeader48
        '
        Me.ColumnHeader48.Text = "State"
        '
        'ColumnHeader49
        '
        Me.ColumnHeader49.Text = "Interval"
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
        Me.splitMonitor3.Size = New System.Drawing.Size(567, 213)
        Me.splitMonitor3.SplitterDistance = 184
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
        Me.splitMonitor4.Panel1Collapsed = True
        '
        'splitMonitor4.Panel2
        '
        Me.splitMonitor4.Panel2.Controls.Add(Me.graphMonitor)
        Me.splitMonitor4.Size = New System.Drawing.Size(567, 184)
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
        Me.graphMonitor.Size = New System.Drawing.Size(567, 184)
        Me.graphMonitor.TabIndex = 3
        Me.graphMonitor.TabStop = False
        Me.graphMonitor.ViewMax = 0
        Me.graphMonitor.ViewMin = 0
        '
        'txtMonitorNumber
        '
        Me.txtMonitorNumber.Location = New System.Drawing.Point(241, 0)
        Me.txtMonitorNumber.Name = "txtMonitorNumber"
        Me.txtMonitorNumber.Size = New System.Drawing.Size(33, 22)
        Me.txtMonitorNumber.TabIndex = 11
        Me.txtMonitorNumber.Text = "200"
        '
        'lblMonitorMaxNumber
        '
        Me.lblMonitorMaxNumber.AutoSize = True
        Me.lblMonitorMaxNumber.Location = New System.Drawing.Point(171, 6)
        Me.lblMonitorMaxNumber.Name = "lblMonitorMaxNumber"
        Me.lblMonitorMaxNumber.Size = New System.Drawing.Size(66, 13)
        Me.lblMonitorMaxNumber.TabIndex = 10
        Me.lblMonitorMaxNumber.Text = "Max. values"
        '
        'chkMonitorRightAuto
        '
        Me.chkMonitorRightAuto.AutoSize = True
        Me.chkMonitorRightAuto.Checked = True
        Me.chkMonitorRightAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonitorRightAuto.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkMonitorRightAuto.Location = New System.Drawing.Point(428, 0)
        Me.chkMonitorRightAuto.Name = "chkMonitorRightAuto"
        Me.chkMonitorRightAuto.Size = New System.Drawing.Size(50, 25)
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
        Me.chkMonitorLeftAuto.Size = New System.Drawing.Size(78, 17)
        Me.chkMonitorLeftAuto.TabIndex = 8
        Me.chkMonitorLeftAuto.Text = "Automatic"
        Me.chkMonitorLeftAuto.UseVisualStyleBackColor = True
        '
        'dtMonitorR
        '
        Me.dtMonitorR.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtMonitorR.Enabled = False
        Me.dtMonitorR.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonitorR.Location = New System.Drawing.Point(478, 0)
        Me.dtMonitorR.Name = "dtMonitorR"
        Me.dtMonitorR.Size = New System.Drawing.Size(89, 22)
        Me.dtMonitorR.TabIndex = 7
        '
        'dtMonitorL
        '
        Me.dtMonitorL.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtMonitorL.Enabled = False
        Me.dtMonitorL.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonitorL.Location = New System.Drawing.Point(0, 0)
        Me.dtMonitorL.Name = "dtMonitorL"
        Me.dtMonitorL.Size = New System.Drawing.Size(89, 22)
        Me.dtMonitorL.TabIndex = 6
        '
        'pageServices
        '
        Me.pageServices.BackColor = System.Drawing.Color.Transparent
        Me.pageServices.Controls.Add(Me.containerServicesPage)
        Me.pageServices.Location = New System.Drawing.Point(4, 22)
        Me.pageServices.Name = "pageServices"
        Me.pageServices.Padding = New System.Windows.Forms.Padding(3)
        Me.pageServices.Size = New System.Drawing.Size(858, 365)
        Me.pageServices.TabIndex = 1
        Me.pageServices.Text = "Services"
        Me.pageServices.UseVisualStyleBackColor = True
        '
        'containerServicesPage
        '
        Me.containerServicesPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerServicesPage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerServicesPage.IsSplitterFixed = True
        Me.containerServicesPage.Location = New System.Drawing.Point(3, 3)
        Me.containerServicesPage.Name = "containerServicesPage"
        Me.containerServicesPage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'containerServicesPage.Panel1
        '
        Me.containerServicesPage.Panel1.Controls.Add(Me.panelMenu2)
        '
        'containerServicesPage.Panel2
        '
        Me.containerServicesPage.Panel2.Controls.Add(Me.panelMain2)
        Me.containerServicesPage.Size = New System.Drawing.Size(852, 359)
        Me.containerServicesPage.SplitterDistance = 25
        Me.containerServicesPage.TabIndex = 0
        '
        'panelMenu2
        '
        Me.panelMenu2.Controls.Add(Me.Label2)
        Me.panelMenu2.Controls.Add(Me.lblResCount2)
        Me.panelMenu2.Controls.Add(Me.txtServiceSearch)
        Me.panelMenu2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMenu2.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu2.Name = "panelMenu2"
        Me.panelMenu2.Size = New System.Drawing.Size(852, 25)
        Me.panelMenu2.TabIndex = 49
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
        Me.lblResCount2.Size = New System.Drawing.Size(56, 13)
        Me.lblResCount2.TabIndex = 2
        Me.lblResCount2.Text = "0 result(s)"
        '
        'txtServiceSearch
        '
        Me.txtServiceSearch.Location = New System.Drawing.Point(89, 1)
        Me.txtServiceSearch.Name = "txtServiceSearch"
        Me.txtServiceSearch.Size = New System.Drawing.Size(501, 22)
        Me.txtServiceSearch.TabIndex = 1
        '
        'panelMain2
        '
        Me.panelMain2.Controls.Add(Me.splitServices)
        Me.panelMain2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain2.Location = New System.Drawing.Point(0, 0)
        Me.panelMain2.Name = "panelMain2"
        Me.panelMain2.Size = New System.Drawing.Size(852, 330)
        Me.panelMain2.TabIndex = 17
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
        Me.splitServices.Size = New System.Drawing.Size(852, 330)
        Me.splitServices.SplitterDistance = 201
        Me.splitServices.TabIndex = 0
        '
        'lvServices
        '
        Me.lvServices.AllowColumnReorder = True
        Me.lvServices.CatchErrors = False
        Me.lvServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader19})
        CConnection7.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvServices.ConnectionObj = CConnection7
        Me.lvServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvServices.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvServices.FullRowSelect = True
        ListViewGroup13.Header = "Services"
        ListViewGroup13.Name = "gpOther"
        ListViewGroup14.Header = "Search result"
        ListViewGroup14.Name = "gpSearch"
        Me.lvServices.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup13, ListViewGroup14})
        Me.lvServices.HideSelection = False
        Me.lvServices.Location = New System.Drawing.Point(0, 0)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.OverriddenDoubleBuffered = True
        Me.lvServices.ProcessId = 0
        Me.lvServices.ReorganizeColumns = True
        Me.lvServices.ShowAll = True
        Me.lvServices.Size = New System.Drawing.Size(852, 201)
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
        Me.splitServices2.Size = New System.Drawing.Size(852, 125)
        Me.splitServices2.SplitterDistance = 35
        Me.splitServices2.TabIndex = 15
        '
        'cmdCopyServiceToCp
        '
        Me.cmdCopyServiceToCp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyServiceToCp.Enabled = False
        Me.cmdCopyServiceToCp.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.cmdCopyServiceToCp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCopyServiceToCp.Location = New System.Drawing.Point(720, 8)
        Me.cmdCopyServiceToCp.Name = "cmdCopyServiceToCp"
        Me.cmdCopyServiceToCp.Size = New System.Drawing.Size(130, 24)
        Me.cmdCopyServiceToCp.TabIndex = 19
        Me.cmdCopyServiceToCp.Text = "Copy to clipboard"
        Me.cmdCopyServiceToCp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCopyServiceToCp.UseVisualStyleBackColor = True
        '
        'lblServicePath
        '
        Me.lblServicePath.BackColor = System.Drawing.SystemColors.Control
        Me.lblServicePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblServicePath.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblServicePath.Location = New System.Drawing.Point(7, 19)
        Me.lblServicePath.Name = "lblServicePath"
        Me.lblServicePath.ReadOnly = True
        Me.lblServicePath.Size = New System.Drawing.Size(440, 15)
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
        Me.splitServices3.Size = New System.Drawing.Size(852, 86)
        Me.splitServices3.SplitterDistance = 629
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
        Me.rtb2.Size = New System.Drawing.Size(629, 86)
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
        Me.splitServices4.Size = New System.Drawing.Size(219, 86)
        Me.splitServices4.SplitterDistance = 36
        Me.splitServices4.TabIndex = 0
        '
        'tv2
        '
        CConnection8.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.tv2.ConnectionObj = CConnection8
        Me.tv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tv2.ImageIndex = 0
        Me.tv2.ImageList = Me.imgServices
        Me.tv2.InfosToGet = YAPM.cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
        Me.tv2.Location = New System.Drawing.Point(0, 0)
        Me.tv2.Name = "tv2"
        Me.tv2.RootService = Nothing
        Me.tv2.SelectedImageIndex = 2
        Me.tv2.Size = New System.Drawing.Size(219, 36)
        Me.tv2.TabIndex = 15
        '
        'tv
        '
        CConnection9.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.tv.ConnectionObj = CConnection9
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tv.ImageIndex = 0
        Me.tv.ImageList = Me.imgServices
        Me.tv.InfosToGet = YAPM.cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
        Me.tv.Location = New System.Drawing.Point(0, 0)
        Me.tv.Name = "tv"
        Me.tv.RootService = Nothing
        Me.tv.SelectedImageIndex = 0
        Me.tv.Size = New System.Drawing.Size(219, 46)
        Me.tv.TabIndex = 14
        '
        'pageNetwork
        '
        Me.pageNetwork.BackColor = System.Drawing.Color.Transparent
        Me.pageNetwork.Controls.Add(Me.panelMain14)
        Me.pageNetwork.Location = New System.Drawing.Point(4, 22)
        Me.pageNetwork.Name = "pageNetwork"
        Me.pageNetwork.Padding = New System.Windows.Forms.Padding(3)
        Me.pageNetwork.Size = New System.Drawing.Size(858, 365)
        Me.pageNetwork.TabIndex = 12
        Me.pageNetwork.Text = "Network"
        Me.pageNetwork.UseVisualStyleBackColor = True
        '
        'panelMain14
        '
        Me.panelMain14.Controls.Add(Me.lvNetwork)
        Me.panelMain14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain14.Location = New System.Drawing.Point(3, 3)
        Me.panelMain14.Name = "panelMain14"
        Me.panelMain14.Size = New System.Drawing.Size(852, 359)
        Me.panelMain14.TabIndex = 57
        '
        'lvNetwork
        '
        Me.lvNetwork.AllowColumnReorder = True
        Me.lvNetwork.CatchErrors = False
        Me.lvNetwork.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader66, Me.ColumnHeader67, Me.ColumnHeader68, Me.ColumnHeader69})
        CConnection10.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvNetwork.ConnectionObj = CConnection10
        Me.lvNetwork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvNetwork.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvNetwork.FullRowSelect = True
        Me.lvNetwork.HideSelection = False
        Me.lvNetwork.Location = New System.Drawing.Point(0, 0)
        Me.lvNetwork.Name = "lvNetwork"
        Me.lvNetwork.OverriddenDoubleBuffered = True
        Me.lvNetwork.ProcessId = Nothing
        Me.lvNetwork.ReorganizeColumns = True
        Me.lvNetwork.ShowAllPid = False
        Me.lvNetwork.Size = New System.Drawing.Size(852, 359)
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
        'pageFile
        '
        Me.pageFile.BackColor = System.Drawing.Color.Transparent
        Me.pageFile.Controls.Add(Me.panelMain5)
        Me.pageFile.Location = New System.Drawing.Point(4, 22)
        Me.pageFile.Name = "pageFile"
        Me.pageFile.Padding = New System.Windows.Forms.Padding(3)
        Me.pageFile.Size = New System.Drawing.Size(858, 365)
        Me.pageFile.TabIndex = 4
        Me.pageFile.Text = "File"
        Me.pageFile.UseVisualStyleBackColor = True
        '
        'panelMain5
        '
        Me.panelMain5.Controls.Add(Me.SplitContainerFilexx)
        Me.panelMain5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain5.Location = New System.Drawing.Point(3, 3)
        Me.panelMain5.Name = "panelMain5"
        Me.panelMain5.Size = New System.Drawing.Size(852, 359)
        Me.panelMain5.TabIndex = 48
        '
        'SplitContainerFilexx
        '
        Me.SplitContainerFilexx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerFilexx.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerFilexx.IsSplitterFixed = True
        Me.SplitContainerFilexx.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerFilexx.Name = "SplitContainerFilexx"
        Me.SplitContainerFilexx.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerFilexx.Panel1
        '
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.txtFile)
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.cmdFileClipboard)
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.pctFileSmall)
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.pctFileBig)
        '
        'SplitContainerFilexx.Panel2
        '
        Me.SplitContainerFilexx.Panel2.Controls.Add(Me.SplitContainerFile)
        Me.SplitContainerFilexx.Size = New System.Drawing.Size(852, 359)
        Me.SplitContainerFilexx.SplitterDistance = 35
        Me.SplitContainerFilexx.TabIndex = 0
        '
        'txtFile
        '
        Me.txtFile.BackColor = System.Drawing.SystemColors.Control
        Me.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtFile.Location = New System.Drawing.Point(5, 9)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(240, 16)
        Me.txtFile.TabIndex = 22
        Me.txtFile.Text = "No selected file"
        '
        'cmdFileClipboard
        '
        Me.cmdFileClipboard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFileClipboard.Enabled = False
        Me.cmdFileClipboard.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.cmdFileClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdFileClipboard.Location = New System.Drawing.Point(651, 7)
        Me.cmdFileClipboard.Name = "cmdFileClipboard"
        Me.cmdFileClipboard.Size = New System.Drawing.Size(130, 24)
        Me.cmdFileClipboard.TabIndex = 21
        Me.cmdFileClipboard.Text = "Copy to clipboard"
        Me.cmdFileClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdFileClipboard.UseVisualStyleBackColor = True
        '
        'pctFileSmall
        '
        Me.pctFileSmall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctFileSmall.Location = New System.Drawing.Point(794, 16)
        Me.pctFileSmall.Name = "pctFileSmall"
        Me.pctFileSmall.Size = New System.Drawing.Size(16, 16)
        Me.pctFileSmall.TabIndex = 20
        Me.pctFileSmall.TabStop = False
        '
        'pctFileBig
        '
        Me.pctFileBig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctFileBig.Location = New System.Drawing.Point(816, 0)
        Me.pctFileBig.Name = "pctFileBig"
        Me.pctFileBig.Size = New System.Drawing.Size(32, 32)
        Me.pctFileBig.TabIndex = 19
        Me.pctFileBig.TabStop = False
        '
        'SplitContainerFile
        '
        Me.SplitContainerFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerFile.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerFile.Name = "SplitContainerFile"
        '
        'SplitContainerFile.Panel1
        '
        Me.SplitContainerFile.Panel1.Controls.Add(Me.SplitContainerFile2)
        '
        'SplitContainerFile.Panel2
        '
        Me.SplitContainerFile.Panel2.Controls.Add(Me.lvFileString)
        Me.SplitContainerFile.Size = New System.Drawing.Size(852, 320)
        Me.SplitContainerFile.SplitterDistance = 581
        Me.SplitContainerFile.TabIndex = 15
        '
        'SplitContainerFile2
        '
        Me.SplitContainerFile2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerFile2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerFile2.IsSplitterFixed = True
        Me.SplitContainerFile2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerFile2.Name = "SplitContainerFile2"
        Me.SplitContainerFile2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerFile2.Panel1
        '
        Me.SplitContainerFile2.Panel1.Controls.Add(Me.rtb3)
        '
        'SplitContainerFile2.Panel2
        '
        Me.SplitContainerFile2.Panel2.Controls.Add(Me.gpFileAttributes)
        Me.SplitContainerFile2.Panel2.Controls.Add(Me.gpFileDates)
        Me.SplitContainerFile2.Size = New System.Drawing.Size(581, 320)
        Me.SplitContainerFile2.SplitterDistance = 204
        Me.SplitContainerFile2.TabIndex = 3
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
        Me.rtb3.Size = New System.Drawing.Size(581, 204)
        Me.rtb3.TabIndex = 21
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
        Me.gpFileAttributes.Size = New System.Drawing.Size(173, 112)
        Me.gpFileAttributes.TabIndex = 19
        Me.gpFileAttributes.TabStop = False
        Me.gpFileAttributes.Text = "File attributes"
        '
        'chkFileEncrypted
        '
        Me.chkFileEncrypted.AutoSize = True
        Me.chkFileEncrypted.Enabled = False
        Me.chkFileEncrypted.Location = New System.Drawing.Point(91, 51)
        Me.chkFileEncrypted.Name = "chkFileEncrypted"
        Me.chkFileEncrypted.Size = New System.Drawing.Size(77, 17)
        Me.chkFileEncrypted.TabIndex = 7
        Me.chkFileEncrypted.Text = "Encrypted"
        Me.chkFileEncrypted.UseVisualStyleBackColor = True
        '
        'chkFileContentNotIndexed
        '
        Me.chkFileContentNotIndexed.AutoSize = True
        Me.chkFileContentNotIndexed.Location = New System.Drawing.Point(6, 88)
        Me.chkFileContentNotIndexed.Name = "chkFileContentNotIndexed"
        Me.chkFileContentNotIndexed.Size = New System.Drawing.Size(133, 17)
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
        Me.chkFileNormal.Size = New System.Drawing.Size(63, 17)
        Me.chkFileNormal.TabIndex = 5
        Me.chkFileNormal.Text = "Normal"
        Me.chkFileNormal.UseVisualStyleBackColor = True
        '
        'chkFileSystem
        '
        Me.chkFileSystem.AutoSize = True
        Me.chkFileSystem.Location = New System.Drawing.Point(91, 33)
        Me.chkFileSystem.Name = "chkFileSystem"
        Me.chkFileSystem.Size = New System.Drawing.Size(61, 17)
        Me.chkFileSystem.TabIndex = 4
        Me.chkFileSystem.Text = "System"
        Me.chkFileSystem.UseVisualStyleBackColor = True
        '
        'chkFileReadOnly
        '
        Me.chkFileReadOnly.AutoSize = True
        Me.chkFileReadOnly.Location = New System.Drawing.Point(6, 70)
        Me.chkFileReadOnly.Name = "chkFileReadOnly"
        Me.chkFileReadOnly.Size = New System.Drawing.Size(77, 17)
        Me.chkFileReadOnly.TabIndex = 3
        Me.chkFileReadOnly.Text = "Read only"
        Me.chkFileReadOnly.UseVisualStyleBackColor = True
        '
        'chkFileHidden
        '
        Me.chkFileHidden.AutoSize = True
        Me.chkFileHidden.Location = New System.Drawing.Point(6, 52)
        Me.chkFileHidden.Name = "chkFileHidden"
        Me.chkFileHidden.Size = New System.Drawing.Size(64, 17)
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
        Me.chkFileCompressed.Size = New System.Drawing.Size(89, 17)
        Me.chkFileCompressed.TabIndex = 1
        Me.chkFileCompressed.Text = "Compressed"
        Me.chkFileCompressed.UseVisualStyleBackColor = True
        '
        'chkFileArchive
        '
        Me.chkFileArchive.AutoSize = True
        Me.chkFileArchive.Location = New System.Drawing.Point(6, 17)
        Me.chkFileArchive.Name = "chkFileArchive"
        Me.chkFileArchive.Size = New System.Drawing.Size(63, 17)
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
        Me.gpFileDates.Size = New System.Drawing.Size(203, 112)
        Me.gpFileDates.TabIndex = 18
        Me.gpFileDates.TabStop = False
        Me.gpFileDates.Text = "File dates"
        '
        'cmdSetFileDates
        '
        Me.cmdSetFileDates.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdSetFileDates.Location = New System.Drawing.Point(3, 87)
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
        Me.DTlastModification.Size = New System.Drawing.Size(84, 22)
        Me.DTlastModification.TabIndex = 5
        '
        'DTlastAccess
        '
        Me.DTlastAccess.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTlastAccess.Location = New System.Drawing.Point(102, 35)
        Me.DTlastAccess.Name = "DTlastAccess"
        Me.DTlastAccess.Size = New System.Drawing.Size(84, 22)
        Me.DTlastAccess.TabIndex = 4
        '
        'DTcreation
        '
        Me.DTcreation.CustomFormat = ""
        Me.DTcreation.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTcreation.Location = New System.Drawing.Point(102, 13)
        Me.DTcreation.Name = "DTcreation"
        Me.DTcreation.Size = New System.Drawing.Size(84, 22)
        Me.DTcreation.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Last access"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Last modification"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Creation"
        '
        'lvFileString
        '
        Me.lvFileString.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.headerString})
        Me.lvFileString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFileString.FullRowSelect = True
        Me.lvFileString.Location = New System.Drawing.Point(0, 0)
        Me.lvFileString.Name = "lvFileString"
        Me.lvFileString.OverriddenDoubleBuffered = True
        Me.lvFileString.Size = New System.Drawing.Size(267, 320)
        Me.lvFileString.TabIndex = 22
        Me.lvFileString.UseCompatibleStateImageBehavior = False
        Me.lvFileString.View = System.Windows.Forms.View.Details
        '
        'headerString
        '
        Me.headerString.Text = "Strings"
        Me.headerString.Width = 250
        '
        'pageSearch
        '
        Me.pageSearch.BackColor = System.Drawing.Color.Transparent
        Me.pageSearch.Controls.Add(Me.panelMain6)
        Me.pageSearch.Location = New System.Drawing.Point(4, 22)
        Me.pageSearch.Name = "pageSearch"
        Me.pageSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pageSearch.Size = New System.Drawing.Size(858, 365)
        Me.pageSearch.TabIndex = 5
        Me.pageSearch.Text = "Search"
        Me.pageSearch.UseVisualStyleBackColor = True
        '
        'panelMain6
        '
        Me.panelMain6.Controls.Add(Me.SplitContainerSearch)
        Me.panelMain6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain6.Location = New System.Drawing.Point(3, 3)
        Me.panelMain6.Name = "panelMain6"
        Me.panelMain6.Size = New System.Drawing.Size(852, 359)
        Me.panelMain6.TabIndex = 49
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
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchEnvVar)
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
        Me.SplitContainerSearch.Size = New System.Drawing.Size(852, 359)
        Me.SplitContainerSearch.SplitterDistance = 55
        Me.SplitContainerSearch.TabIndex = 2
        '
        'chkSearchEnvVar
        '
        Me.chkSearchEnvVar.AutoSize = True
        Me.chkSearchEnvVar.Checked = True
        Me.chkSearchEnvVar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchEnvVar.Location = New System.Drawing.Point(376, 7)
        Me.chkSearchEnvVar.Name = "chkSearchEnvVar"
        Me.chkSearchEnvVar.Size = New System.Drawing.Size(134, 17)
        Me.chkSearchEnvVar.TabIndex = 14
        Me.chkSearchEnvVar.Text = "environment variable"
        Me.chkSearchEnvVar.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 13)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Search result"
        '
        'lblResultsCount
        '
        Me.lblResultsCount.AutoSize = True
        Me.lblResultsCount.Location = New System.Drawing.Point(396, 34)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(56, 13)
        Me.lblResultsCount.TabIndex = 12
        Me.lblResultsCount.Text = "0 result(s)"
        '
        'txtSearchResults
        '
        Me.txtSearchResults.Location = New System.Drawing.Point(89, 31)
        Me.txtSearchResults.Name = "txtSearchResults"
        Me.txtSearchResults.Size = New System.Drawing.Size(298, 22)
        Me.txtSearchResults.TabIndex = 11
        '
        'chkSearchWindows
        '
        Me.chkSearchWindows.AutoSize = True
        Me.chkSearchWindows.Checked = True
        Me.chkSearchWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchWindows.Location = New System.Drawing.Point(656, 8)
        Me.chkSearchWindows.Name = "chkSearchWindows"
        Me.chkSearchWindows.Size = New System.Drawing.Size(73, 17)
        Me.chkSearchWindows.TabIndex = 6
        Me.chkSearchWindows.Text = "windows"
        Me.chkSearchWindows.UseVisualStyleBackColor = True
        '
        'chkSearchHandles
        '
        Me.chkSearchHandles.AutoSize = True
        Me.chkSearchHandles.Checked = True
        Me.chkSearchHandles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchHandles.Location = New System.Drawing.Point(587, 8)
        Me.chkSearchHandles.Name = "chkSearchHandles"
        Me.chkSearchHandles.Size = New System.Drawing.Size(67, 17)
        Me.chkSearchHandles.TabIndex = 5
        Me.chkSearchHandles.Text = "handles"
        Me.chkSearchHandles.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(162, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
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
        Me.chkSearchModules.Size = New System.Drawing.Size(70, 17)
        Me.chkSearchModules.TabIndex = 3
        Me.chkSearchModules.Text = "modules"
        Me.chkSearchModules.UseVisualStyleBackColor = True
        '
        'chkSearchServices
        '
        Me.chkSearchServices.AutoSize = True
        Me.chkSearchServices.Checked = True
        Me.chkSearchServices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchServices.Location = New System.Drawing.Point(516, 8)
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
        Me.chkSearchProcess.Size = New System.Drawing.Size(76, 17)
        Me.chkSearchProcess.TabIndex = 1
        Me.chkSearchProcess.Text = "processes"
        Me.chkSearchProcess.UseVisualStyleBackColor = True
        '
        'chkSearchCase
        '
        Me.chkSearchCase.AutoSize = True
        Me.chkSearchCase.Location = New System.Drawing.Point(8, 7)
        Me.chkSearchCase.Name = "chkSearchCase"
        Me.chkSearchCase.Size = New System.Drawing.Size(97, 17)
        Me.chkSearchCase.TabIndex = 0
        Me.chkSearchCase.Text = "Case sensitive"
        Me.chkSearchCase.UseVisualStyleBackColor = True
        '
        'lvSearchResults
        '
        Me.lvSearchResults.AllowColumnReorder = True
        Me.lvSearchResults.CaseSensitive = False
        Me.lvSearchResults.CatchErrors = False
        Me.lvSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader17})
        CConnection11.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
        Me.lvSearchResults.ConnectionObj = CConnection11
        Me.lvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSearchResults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvSearchResults.FullRowSelect = True
        ListViewGroup15.Header = "Results"
        ListViewGroup15.Name = "gpResults"
        ListViewGroup16.Header = "Search results"
        ListViewGroup16.Name = "gpSearchResults"
        Me.lvSearchResults.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup15, ListViewGroup16})
        Me.lvSearchResults.HideSelection = False
        Me.lvSearchResults.Includes = YAPM.searchInfos.SearchInclude.SearchProcesses
        Me.lvSearchResults.Location = New System.Drawing.Point(0, 0)
        Me.lvSearchResults.Name = "lvSearchResults"
        Me.lvSearchResults.OverriddenDoubleBuffered = True
        Me.lvSearchResults.ReorganizeColumns = True
        Me.lvSearchResults.SearchString = Nothing
        Me.lvSearchResults.Size = New System.Drawing.Size(852, 300)
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
        'pageHelp
        '
        Me.pageHelp.BackColor = System.Drawing.Color.Transparent
        Me.pageHelp.Controls.Add(Me.panelMain4)
        Me.pageHelp.Location = New System.Drawing.Point(4, 22)
        Me.pageHelp.Name = "pageHelp"
        Me.pageHelp.Padding = New System.Windows.Forms.Padding(3)
        Me.pageHelp.Size = New System.Drawing.Size(858, 365)
        Me.pageHelp.TabIndex = 3
        Me.pageHelp.Text = "Help"
        Me.pageHelp.UseVisualStyleBackColor = True
        '
        'panelMain4
        '
        Me.panelMain4.Controls.Add(Me.WBHelp)
        Me.panelMain4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain4.Location = New System.Drawing.Point(3, 3)
        Me.panelMain4.Name = "panelMain4"
        Me.panelMain4.Size = New System.Drawing.Size(852, 359)
        Me.panelMain4.TabIndex = 17
        '
        'WBHelp
        '
        Me.WBHelp.AllowWebBrowserDrop = False
        Me.WBHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WBHelp.IsWebBrowserContextMenuEnabled = False
        Me.WBHelp.Location = New System.Drawing.Point(0, 0)
        Me.WBHelp.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WBHelp.Name = "WBHelp"
        Me.WBHelp.Size = New System.Drawing.Size(852, 359)
        Me.WBHelp.TabIndex = 0
        Me.WBHelp.Url = New System.Uri("", System.UriKind.Relative)
        '
        'timerNetwork
        '
        Me.timerNetwork.Interval = 1000
        '
        'timerStateBasedActions
        '
        Me.timerStateBasedActions.Interval = 1000
        '
        'mnuHandle
        '
        Me.mnuHandle.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemHSelectAssociatedProcess, Me.MenuItemCloseHandle, Me.MenuItemNavigateHandle, Me.MenuItem12, Me.MenuItemShowUnnamedHandles, Me.MenuItem14, Me.MenuItemCopyHandle, Me.MenuItemChooseColumnsHandle})
        '
        'MenuItemHSelectAssociatedProcess
        '
        Me.VistaMenu.SetImage(Me.MenuItemHSelectAssociatedProcess, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemHSelectAssociatedProcess.Index = 0
        Me.MenuItemHSelectAssociatedProcess.Text = "Select associated process"
        '
        'MenuItemCloseHandle
        '
        Me.MenuItemCloseHandle.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemCloseHandle, Global.YAPM.My.Resources.Resources.close)
        Me.MenuItemCloseHandle.Index = 1
        Me.MenuItemCloseHandle.Text = "Close item"
        '
        'MenuItemNavigateHandle
        '
        Me.VistaMenu.SetImage(Me.MenuItemNavigateHandle, Global.YAPM.My.Resources.Resources.arrow_000_medium)
        Me.MenuItemNavigateHandle.Index = 2
        Me.MenuItemNavigateHandle.Text = "Navigate..."
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 3
        Me.MenuItem12.Text = "-"
        '
        'MenuItemShowUnnamedHandles
        '
        Me.MenuItemShowUnnamedHandles.Index = 4
        Me.MenuItemShowUnnamedHandles.Text = "Show unnamed handles"
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 5
        Me.MenuItem14.Text = "-"
        '
        'MenuItemCopyHandle
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyHandle, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyHandle.Index = 6
        Me.MenuItemCopyHandle.Text = "Copy to clipboard"
        '
        'MenuItemChooseColumnsHandle
        '
        Me.MenuItemChooseColumnsHandle.Index = 7
        Me.MenuItemChooseColumnsHandle.Text = "Choose columns..."
        '
        'MenuItemTaskShow
        '
        Me.MenuItemTaskShow.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemTaskShow, Global.YAPM.My.Resources.Resources.display16)
        Me.MenuItemTaskShow.Index = 0
        Me.MenuItemTaskShow.Text = "Show window"
        '
        'MenuItemTaskEnd
        '
        Me.VistaMenu.SetImage(Me.MenuItemTaskEnd, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemTaskEnd.Index = 4
        Me.MenuItemTaskEnd.Text = "End task"
        '
        'MenuItemTaskSelProc
        '
        Me.MenuItemTaskSelProc.Enabled = False
        Me.VistaMenu.SetImage(Me.MenuItemTaskSelProc, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemTaskSelProc.Index = 6
        Me.MenuItemTaskSelProc.Text = "Select associated process"
        '
        'MenuItemMonitorAdd
        '
        Me.VistaMenu.SetImage(Me.MenuItemMonitorAdd, Global.YAPM.My.Resources.Resources.plus_circle)
        Me.MenuItemMonitorAdd.Index = 0
        Me.MenuItemMonitorAdd.Text = "Add..."
        '
        'MenuItemMonitorRemove
        '
        Me.VistaMenu.SetImage(Me.MenuItemMonitorRemove, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemMonitorRemove.Index = 1
        Me.MenuItemMonitorRemove.Text = "Remove selection"
        '
        'MenuItemMonitorStart
        '
        Me.MenuItemMonitorStart.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemMonitorStart, Global.YAPM.My.Resources.Resources.control)
        Me.MenuItemMonitorStart.Index = 3
        Me.MenuItemMonitorStart.Text = "Start"
        '
        'MenuItemMonitorStop
        '
        Me.VistaMenu.SetImage(Me.MenuItemMonitorStop, Global.YAPM.My.Resources.Resources.control_stop_square)
        Me.MenuItemMonitorStop.Index = 4
        Me.MenuItemMonitorStop.Text = "Stop"
        '
        'MenuItemCopyBig
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyBig, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyBig.Index = 0
        Me.MenuItemCopyBig.Text = "Copy to clipboard"
        '
        'MenuItemCopySmall
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopySmall, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopySmall.Index = 0
        Me.MenuItemCopySmall.Text = "Copy to clipboard"
        '
        'MenuItemMainShow
        '
        Me.MenuItemMainShow.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemMainShow, Global.YAPM.My.Resources.Resources.display16)
        Me.MenuItemMainShow.Index = 0
        Me.MenuItemMainShow.Text = "Show YAPM"
        '
        'MenuItemMainToTray
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainToTray, Global.YAPM.My.Resources.Resources.down)
        Me.MenuItemMainToTray.Index = 1
        Me.MenuItemMainToTray.Text = "Minimize to tray"
        '
        'MenuItemMainAbout
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainAbout, Global.YAPM.My.Resources.Resources.information_frame)
        Me.MenuItemMainAbout.Index = 2
        Me.MenuItemMainAbout.Text = "About YAPM"
        '
        'MenuItemMainLog
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainLog, Global.YAPM.My.Resources.Resources.document_text)
        Me.MenuItemMainLog.Index = 7
        Me.MenuItemMainLog.Text = "Show log"
        '
        'MenuItemMainOpenedW
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainOpenedW, Global.YAPM.My.Resources.Resources.display16)
        Me.MenuItemMainOpenedW.Index = 10
        Me.MenuItemMainOpenedW.Text = "Opened windows"
        '
        'MenuItemMainExit
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainExit, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemMainExit.Index = 18
        Me.MenuItemMainExit.Text = "Exit YAPM"
        '
        'MenuItemMainSysInfo
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainSysInfo, Global.YAPM.My.Resources.Resources.taskmgr)
        Me.MenuItemMainSysInfo.Index = 9
        Me.MenuItemMainSysInfo.Text = "System information"
        '
        'MenuItemServSelService
        '
        Me.MenuItemServSelService.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemServSelService, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemServSelService.Index = 0
        Me.MenuItemServSelService.Text = "Show selected process"
        '
        'MenuItemServFileProp
        '
        Me.VistaMenu.SetImage(Me.MenuItemServFileProp, Global.YAPM.My.Resources.Resources.document_text)
        Me.MenuItemServFileProp.Index = 2
        Me.MenuItemServFileProp.Text = "File properties"
        '
        'MenuItemServOpenDir
        '
        Me.VistaMenu.SetImage(Me.MenuItemServOpenDir, Global.YAPM.My.Resources.Resources.folder_open)
        Me.MenuItemServOpenDir.Index = 3
        Me.MenuItemServOpenDir.Text = "Open directory"
        '
        'MenuItemServFileDetails
        '
        Me.VistaMenu.SetImage(Me.MenuItemServFileDetails, Global.YAPM.My.Resources.Resources.magnifier)
        Me.MenuItemServFileDetails.Index = 4
        Me.MenuItemServFileDetails.Text = "File details"
        '
        'MenuItemServSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemServSearch, Global.YAPM.My.Resources.Resources.globe)
        Me.MenuItemServSearch.Index = 5
        Me.MenuItemServSearch.Text = "Internet search"
        '
        'MenuItemServPause
        '
        Me.VistaMenu.SetImage(Me.MenuItemServPause, Global.YAPM.My.Resources.Resources.control_pause)
        Me.MenuItemServPause.Index = 8
        Me.MenuItemServPause.Text = "Pause"
        '
        'MenuItemServStop
        '
        Me.VistaMenu.SetImage(Me.MenuItemServStop, Global.YAPM.My.Resources.Resources.control_stop_square)
        Me.MenuItemServStop.Index = 9
        Me.MenuItemServStop.Text = "Stop"
        '
        'MenuItemServStart
        '
        Me.VistaMenu.SetImage(Me.MenuItemServStart, Global.YAPM.My.Resources.Resources.control)
        Me.MenuItemServStart.Index = 10
        Me.MenuItemServStart.Text = "Start"
        '
        'MenuItemServAutoStart
        '
        Me.VistaMenu.SetImage(Me.MenuItemServAutoStart, Global.YAPM.My.Resources.Resources.p6)
        Me.MenuItemServAutoStart.Index = 0
        Me.MenuItemServAutoStart.Text = "Auto start"
        '
        'MenuItemServOnDemand
        '
        Me.VistaMenu.SetImage(Me.MenuItemServOnDemand, Global.YAPM.My.Resources.Resources.p3)
        Me.MenuItemServOnDemand.Index = 1
        Me.MenuItemServOnDemand.Text = "On demand"
        '
        'MenuItemServDisabled
        '
        Me.VistaMenu.SetImage(Me.MenuItemServDisabled, Global.YAPM.My.Resources.Resources.p0)
        Me.MenuItemServDisabled.Index = 2
        Me.MenuItemServDisabled.Text = "Disabled"
        '
        'MenuItemServDepe
        '
        Me.VistaMenu.SetImage(Me.MenuItemServDepe, Global.YAPM.My.Resources.Resources.dllIcon)
        Me.MenuItemServDepe.Index = 6
        Me.MenuItemServDepe.Text = "Show dependencies..."
        '
        'MenuItemNetworkClose
        '
        Me.VistaMenu.SetImage(Me.MenuItemNetworkClose, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemNetworkClose.Index = 2
        Me.MenuItemNetworkClose.Text = "Close TCP connection"
        '
        'MenuItemServSelProc
        '
        Me.MenuItemServSelProc.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemServSelProc, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemServSelProc.Index = 0
        Me.MenuItemServSelProc.Text = "Select associated process"
        '
        'MenuItemThTerm
        '
        Me.MenuItemThTerm.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemThTerm, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemThTerm.Index = 2
        Me.MenuItemThTerm.Text = "Terminate"
        '
        'MenuItemThSuspend
        '
        Me.VistaMenu.SetImage(Me.MenuItemThSuspend, Global.YAPM.My.Resources.Resources.control_pause)
        Me.MenuItemThSuspend.Index = 3
        Me.MenuItemThSuspend.Text = "Suspend"
        '
        'MenuItemThResu
        '
        Me.VistaMenu.SetImage(Me.MenuItemThResu, Global.YAPM.My.Resources.Resources.control)
        Me.MenuItemThResu.Index = 4
        Me.MenuItemThResu.Text = "Resume"
        '
        'MenuItemThIdle
        '
        Me.VistaMenu.SetImage(Me.MenuItemThIdle, Global.YAPM.My.Resources.Resources.p0)
        Me.MenuItemThIdle.Index = 0
        Me.MenuItemThIdle.Text = "Idle"
        '
        'MenuItemThLowest
        '
        Me.VistaMenu.SetImage(Me.MenuItemThLowest, Global.YAPM.My.Resources.Resources.p1)
        Me.MenuItemThLowest.Index = 1
        Me.MenuItemThLowest.Text = "Lowest"
        '
        'MenuItemThBNormal
        '
        Me.VistaMenu.SetImage(Me.MenuItemThBNormal, Global.YAPM.My.Resources.Resources.p2)
        Me.MenuItemThBNormal.Index = 2
        Me.MenuItemThBNormal.Text = "Below normal"
        '
        'MenuItemThNorm
        '
        Me.VistaMenu.SetImage(Me.MenuItemThNorm, Global.YAPM.My.Resources.Resources.p3)
        Me.MenuItemThNorm.Index = 3
        Me.MenuItemThNorm.Text = "Normal"
        '
        'MenuItemThANorm
        '
        Me.VistaMenu.SetImage(Me.MenuItemThANorm, Global.YAPM.My.Resources.Resources.p4)
        Me.MenuItemThANorm.Index = 4
        Me.MenuItemThANorm.Text = "Above normal"
        '
        'MenuItemThHighest
        '
        Me.VistaMenu.SetImage(Me.MenuItemThHighest, Global.YAPM.My.Resources.Resources.p5)
        Me.MenuItemThHighest.Index = 5
        Me.MenuItemThHighest.Text = "Highest"
        '
        'MenuItemThTimeCr
        '
        Me.VistaMenu.SetImage(Me.MenuItemThTimeCr, Global.YAPM.My.Resources.Resources.p6)
        Me.MenuItemThTimeCr.Index = 6
        Me.MenuItemThTimeCr.Text = "Time critical"
        '
        'MenuItemThSelProc
        '
        Me.VistaMenu.SetImage(Me.MenuItemThSelProc, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemThSelProc.Index = 0
        Me.MenuItemThSelProc.Text = "Select associated process"
        '
        'MenuItemModuleFileProp
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleFileProp, Global.YAPM.My.Resources.Resources.document_text)
        Me.MenuItemModuleFileProp.Index = 2
        Me.MenuItemModuleFileProp.Text = "File properties"
        '
        'MenuItemModuleOpenDir
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleOpenDir, Global.YAPM.My.Resources.Resources.folder_open)
        Me.MenuItemModuleOpenDir.Index = 3
        Me.MenuItemModuleOpenDir.Text = "Open directory"
        '
        'MenuItemModuleFileDetails
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleFileDetails, Global.YAPM.My.Resources.Resources.magnifier)
        Me.MenuItemModuleFileDetails.Index = 4
        Me.MenuItemModuleFileDetails.Text = "File details"
        '
        'MenuItemModuleSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleSearch, Global.YAPM.My.Resources.Resources.globe)
        Me.MenuItemModuleSearch.Index = 5
        Me.MenuItemModuleSearch.Text = "Internet search"
        '
        'MenuItemModuleDependencies
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleDependencies, Global.YAPM.My.Resources.Resources.dllIcon)
        Me.MenuItemModuleDependencies.Index = 6
        Me.MenuItemModuleDependencies.Text = "View dependencies..."
        '
        'MenuItemUnloadModule
        '
        Me.MenuItemUnloadModule.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemUnloadModule, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemUnloadModule.Index = 8
        Me.MenuItemUnloadModule.Text = "Unload module"
        '
        'MenuItemModuleSelProc
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleSelProc, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemModuleSelProc.Index = 0
        Me.MenuItemModuleSelProc.Text = "Select associated process"
        '
        'MenuItemWindowSelProc
        '
        Me.MenuItemWindowSelProc.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemWindowSelProc, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemWindowSelProc.Index = 0
        Me.MenuItemWindowSelProc.Text = "Select associated process"
        '
        'MenuItemSearchSel
        '
        Me.VistaMenu.SetImage(Me.MenuItemSearchSel, Global.YAPM.My.Resources.Resources.exe)
        Me.MenuItemSearchSel.Index = 2
        Me.MenuItemSearchSel.Text = "Select associated process/service"
        '
        'MenuItemSearchClose
        '
        Me.VistaMenu.SetImage(Me.MenuItemSearchClose, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemSearchClose.Index = 3
        Me.MenuItemSearchClose.Text = "Close item"
        '
        'MenuItemProcSFileProp
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSFileProp, Global.YAPM.My.Resources.Resources.document_text)
        Me.MenuItemProcSFileProp.Index = 10
        Me.MenuItemProcSFileProp.Text = "File properties"
        '
        'MenuItemProcSOpenDir
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSOpenDir, Global.YAPM.My.Resources.Resources.folder_open)
        Me.MenuItemProcSOpenDir.Index = 11
        Me.MenuItemProcSOpenDir.Text = "Open directory"
        '
        'MenuItemProcSFileDetails
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSFileDetails, Global.YAPM.My.Resources.Resources.magnifier)
        Me.MenuItemProcSFileDetails.Index = 12
        Me.MenuItemProcSFileDetails.Text = "File details"
        '
        'MenuItemProcSSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSSearch, Global.YAPM.My.Resources.Resources.globe)
        Me.MenuItemProcSSearch.Index = 13
        Me.MenuItemProcSSearch.Text = "Internet search"
        '
        'MenuItemProcSDep
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSDep, Global.YAPM.My.Resources.Resources.dllIcon)
        Me.MenuItemProcSDep.Index = 14
        Me.MenuItemProcSDep.Text = "View dependencies..."
        '
        'MenuItemProcKill
        '
        Me.MenuItemProcKill.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemProcKill, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemProcKill.Index = 0
        Me.MenuItemProcKill.Text = "Kill"
        '
        'MenuItemProcKillT
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcKillT, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemProcKillT.Index = 1
        Me.MenuItemProcKillT.Text = "Kill process tree"
        '
        'MenuItemProcStop
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcStop, Global.YAPM.My.Resources.Resources.control_stop_square)
        Me.MenuItemProcStop.Index = 2
        Me.MenuItemProcStop.Text = "Stop"
        '
        'MenuItemProcResume
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcResume, Global.YAPM.My.Resources.Resources.control)
        Me.MenuItemProcResume.Index = 3
        Me.MenuItemProcResume.Text = "Resume"
        '
        'MenuItemProcPIdle
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPIdle, Global.YAPM.My.Resources.Resources.p0)
        Me.MenuItemProcPIdle.Index = 0
        Me.MenuItemProcPIdle.Text = "Idle"
        '
        'MenuItemProcPBN
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPBN, Global.YAPM.My.Resources.Resources.p1)
        Me.MenuItemProcPBN.Index = 1
        Me.MenuItemProcPBN.Text = "Below normal"
        '
        'MenuItemProcPN
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPN, Global.YAPM.My.Resources.Resources.p2)
        Me.MenuItemProcPN.Index = 2
        Me.MenuItemProcPN.Text = "Normal"
        '
        'MenuItemProcPAN
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPAN, Global.YAPM.My.Resources.Resources.p3)
        Me.MenuItemProcPAN.Index = 3
        Me.MenuItemProcPAN.Text = "Above normal"
        '
        'MenuItemProcPH
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPH, Global.YAPM.My.Resources.Resources.p4)
        Me.MenuItemProcPH.Index = 4
        Me.MenuItemProcPH.Text = "High"
        '
        'MenuItemProcPRT
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPRT, Global.YAPM.My.Resources.Resources.p6)
        Me.MenuItemProcPRT.Index = 5
        Me.MenuItemProcPRT.Text = "Real time"
        '
        'MenuItemSystemRefresh
        '
        Me.MenuItemSystemRefresh.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemSystemRefresh, Global.YAPM.My.Resources.Resources.refresh16)
        Me.MenuItemSystemRefresh.Index = 0
        Me.MenuItemSystemRefresh.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.MenuItemSystemRefresh.Text = "&Refresh"
        '
        'MenuItemSystemLog
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemLog, Global.YAPM.My.Resources.Resources.document_text)
        Me.MenuItemSystemLog.Index = 5
        Me.MenuItemSystemLog.Text = "Show &log"
        '
        'MenuItemSystemInfos
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemInfos, Global.YAPM.My.Resources.Resources.taskmgr)
        Me.MenuItemSystemInfos.Index = 8
        Me.MenuItemSystemInfos.Text = "System &infos"
        '
        'MenuItemSystemOpenedWindows
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemOpenedWindows, Global.YAPM.My.Resources.Resources.display16)
        Me.MenuItemSystemOpenedWindows.Index = 9
        Me.MenuItemSystemOpenedWindows.Text = "Opened &windows"
        '
        'MenuItemSystemToTray
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemToTray, Global.YAPM.My.Resources.Resources.down)
        Me.MenuItemSystemToTray.Index = 16
        Me.MenuItemSystemToTray.Text = "Minimize to &tray"
        '
        'MenuItemSystemExit
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemExit, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemSystemExit.Index = 17
        Me.MenuItemSystemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4
        Me.MenuItemSystemExit.Text = "E&xit"
        '
        'MenuItemSystemUpdate
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemUpdate, Global.YAPM.My.Resources.Resources.refresh16)
        Me.MenuItemSystemUpdate.Index = 0
        Me.MenuItemSystemUpdate.Text = "Check &updates"
        '
        'MenuItemSystemWebsite
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemWebsite, Global.YAPM.My.Resources.Resources.globe)
        Me.MenuItemSystemWebsite.Index = 5
        Me.MenuItemSystemWebsite.Text = "&Website"
        '
        'MenuItemSystemAbout
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemAbout, Global.YAPM.My.Resources.Resources.information_frame)
        Me.MenuItemSystemAbout.Index = 9
        Me.MenuItemSystemAbout.Text = "&About"
        '
        'MenuItemSystemFindWindow
        '
        Me.VistaMenu.SetImage(Me.MenuItemSystemFindWindow, Global.YAPM.My.Resources.Resources.target16)
        Me.MenuItemSystemFindWindow.Index = 12
        Me.MenuItemSystemFindWindow.Text = "&Find a window"
        '
        'MenuItemSystemHelp
        '
        Me.MenuItemSystemHelp.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemSystemHelp, Global.YAPM.My.Resources.Resources.help16)
        Me.MenuItemSystemHelp.Index = 8
        Me.MenuItemSystemHelp.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.MenuItemSystemHelp.Text = "&Help"
        '
        'MenuItemSystemOptions
        '
        Me.MenuItemSystemOptions.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemSystemOptions, Global.YAPM.My.Resources.Resources.options16)
        Me.MenuItemSystemOptions.Index = 5
        Me.MenuItemSystemOptions.Text = "&Options..."
        '
        'MenuItemMainFindWindow
        '
        Me.VistaMenu.SetImage(Me.MenuItemMainFindWindow, Global.YAPM.My.Resources.Resources.target16)
        Me.MenuItemMainFindWindow.Index = 12
        Me.MenuItemMainFindWindow.Text = "Find a window"
        '
        'MenuItemCopyTask
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyTask, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyTask.Index = 9
        Me.MenuItemCopyTask.Text = "Copy to clipboard"
        '
        'MenuItemCopyService
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyService, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyService.Index = 15
        Me.MenuItemCopyService.Text = "Copy to clipboard"
        '
        'MenuItemCopyNetwork
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyNetwork, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyNetwork.Index = 4
        Me.MenuItemCopyNetwork.Text = "Copy to clipboard"
        '
        'MenuItemCopyThread
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyThread, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyThread.Index = 8
        Me.MenuItemCopyThread.Text = "Copy to clipboard"
        '
        'MenuItemCopyModule
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyModule, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyModule.Index = 10
        Me.MenuItemCopyModule.Text = "Copy to clipboard"
        '
        'MenuItemCopyWindow
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyWindow, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyWindow.Index = 12
        Me.MenuItemCopyWindow.Text = "Copy to clipboard"
        '
        'MenuItemCopySearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopySearch, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopySearch.Index = 5
        Me.MenuItemCopySearch.Text = "Copy to clipboard"
        '
        'MenuItemCopyProcess
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyProcess, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyProcess.Index = 16
        Me.MenuItemCopyProcess.Text = "Copy to clipboard"
        '
        'mnuTask
        '
        Me.mnuTask.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemTaskShow, Me.MenuItemTaskMax, Me.MenuItemTaskMin, Me.MenuItem4, Me.MenuItemTaskEnd, Me.MenuItem6, Me.MenuItemTaskSelProc, Me.MenuItemTaskSelWin, Me.MenuItem9, Me.MenuItemCopyTask, Me.MenuItemTaskColumns})
        '
        'MenuItemTaskMax
        '
        Me.MenuItemTaskMax.Index = 1
        Me.MenuItemTaskMax.Text = "Maximize"
        '
        'MenuItemTaskMin
        '
        Me.MenuItemTaskMin.Index = 2
        Me.MenuItemTaskMin.Text = "Minimize"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.Text = "-"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 5
        Me.MenuItem6.Text = "-"
        '
        'MenuItemTaskSelWin
        '
        Me.MenuItemTaskSelWin.Index = 7
        Me.MenuItemTaskSelWin.Text = "Select  in 'Window tab'"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 8
        Me.MenuItem9.Text = "-"
        '
        'MenuItemTaskColumns
        '
        Me.MenuItemTaskColumns.Index = 10
        Me.MenuItemTaskColumns.Text = "Choose columns..."
        '
        'mnuMonitor
        '
        Me.mnuMonitor.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemMonitorAdd, Me.MenuItemMonitorRemove, Me.MenuItem1, Me.MenuItemMonitorStart, Me.MenuItemMonitorStop})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "-"
        '
        'mnuFileCpPctBig
        '
        Me.mnuFileCpPctBig.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyBig})
        '
        'mnuFileCpPctSmall
        '
        Me.mnuFileCpPctSmall.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopySmall})
        '
        'mnuMain
        '
        Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemMainShow, Me.MenuItemMainToTray, Me.MenuItemMainAbout, Me.MenuItemMainAlwaysVisible, Me.MenuItem2, Me.MenuItem3, Me.MenuItem5, Me.MenuItemMainLog, Me.MenuItemMainReport, Me.MenuItemMainSysInfo, Me.MenuItemMainOpenedW, Me.MenuItemMainEmergencyH, Me.MenuItemMainFindWindow, Me.MenuItemMainSBA, Me.MenuItem15, Me.MenuItemRefProc, Me.MenuItemMainRefServ, Me.MenuItem18, Me.MenuItemMainExit})
        '
        'MenuItemMainAlwaysVisible
        '
        Me.MenuItemMainAlwaysVisible.Index = 3
        Me.MenuItemMainAlwaysVisible.Text = "Always visible"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 4
        Me.MenuItem2.Text = "-"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 5
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemMainRestart, Me.MenuItemMainShutdown, Me.MenuItemMainPowerOff, Me.MenuItem11, Me.MenuItemMainSleep, Me.MenuItemMainHibernate, Me.MenuItem17, Me.MenuItemMainLogOff, Me.MenuItemMainLock})
        Me.MenuItem3.Text = "Shutdown"
        '
        'MenuItemMainRestart
        '
        Me.MenuItemMainRestart.Index = 0
        Me.MenuItemMainRestart.Text = "Restart"
        '
        'MenuItemMainShutdown
        '
        Me.MenuItemMainShutdown.Index = 1
        Me.MenuItemMainShutdown.Text = "Shutdown"
        '
        'MenuItemMainPowerOff
        '
        Me.MenuItemMainPowerOff.Index = 2
        Me.MenuItemMainPowerOff.Text = "Power off"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 3
        Me.MenuItem11.Text = "-"
        '
        'MenuItemMainSleep
        '
        Me.MenuItemMainSleep.Index = 4
        Me.MenuItemMainSleep.Text = "Sleep"
        '
        'MenuItemMainHibernate
        '
        Me.MenuItemMainHibernate.Index = 5
        Me.MenuItemMainHibernate.Text = "Hibernate"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 6
        Me.MenuItem17.Text = "-"
        '
        'MenuItemMainLogOff
        '
        Me.MenuItemMainLogOff.Index = 7
        Me.MenuItemMainLogOff.Text = "Log off"
        '
        'MenuItemMainLock
        '
        Me.MenuItemMainLock.Index = 8
        Me.MenuItemMainLock.Text = "Lock"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 6
        Me.MenuItem5.Text = "-"
        '
        'MenuItemMainReport
        '
        Me.MenuItemMainReport.Index = 8
        Me.MenuItemMainReport.Text = "Save system report..."
        '
        'MenuItemMainEmergencyH
        '
        Me.MenuItemMainEmergencyH.Index = 11
        Me.MenuItemMainEmergencyH.Text = "Emergency hotkeys..."
        '
        'MenuItemMainSBA
        '
        Me.MenuItemMainSBA.Enabled = False
        Me.MenuItemMainSBA.Index = 13
        Me.MenuItemMainSBA.Text = "State based actions..."
        Me.MenuItemMainSBA.Visible = False
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 14
        Me.MenuItem15.Text = "-"
        '
        'MenuItemRefProc
        '
        Me.MenuItemRefProc.Checked = True
        Me.MenuItemRefProc.Index = 15
        Me.MenuItemRefProc.Text = "Refresh process list"
        '
        'MenuItemMainRefServ
        '
        Me.MenuItemMainRefServ.Checked = True
        Me.MenuItemMainRefServ.Index = 16
        Me.MenuItemMainRefServ.Text = "Refresh service list"
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = 17
        Me.MenuItem18.Text = "-"
        '
        'mnuService
        '
        Me.mnuService.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServSelService, Me.MenuItem7, Me.MenuItemServFileProp, Me.MenuItemServOpenDir, Me.MenuItemServFileDetails, Me.MenuItemServSearch, Me.MenuItemServDepe, Me.MenuItem20, Me.MenuItemServPause, Me.MenuItemServStop, Me.MenuItemServStart, Me.MenuItem8, Me.MenuItem25, Me.MenuItemServReanalize, Me.MenuItem24, Me.MenuItemCopyService, Me.MenuItemServColumns})
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 1
        Me.MenuItem7.Text = "-"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 7
        Me.MenuItem20.Text = "-"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 11
        Me.MenuItem8.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServAutoStart, Me.MenuItemServOnDemand, Me.MenuItemServDisabled})
        Me.MenuItem8.Text = "Start type"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 12
        Me.MenuItem25.Text = "-"
        '
        'MenuItemServReanalize
        '
        Me.MenuItemServReanalize.Index = 13
        Me.MenuItemServReanalize.Text = "Reanalize"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 14
        Me.MenuItem24.Text = "-"
        '
        'MenuItemServColumns
        '
        Me.MenuItemServColumns.Index = 16
        Me.MenuItemServColumns.Text = "Choose columns..."
        '
        'mnuNetwork
        '
        Me.mnuNetwork.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServSelProc, Me.MenuItem16, Me.MenuItemNetworkClose, Me.MenuItem10, Me.MenuItemCopyNetwork, Me.MenuItemNetworkColumns})
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 1
        Me.MenuItem16.Text = "-"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 3
        Me.MenuItem10.Text = "-"
        '
        'MenuItemNetworkColumns
        '
        Me.MenuItemNetworkColumns.Index = 5
        Me.MenuItemNetworkColumns.Text = "Choose columns..."
        '
        'mnuThread
        '
        Me.mnuThread.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemThSelProc, Me.MenuItem22, Me.MenuItemThTerm, Me.MenuItemThSuspend, Me.MenuItemThResu, Me.MenuItemThPriority, Me.MenuItemThAffinity, Me.MenuItem19, Me.MenuItemCopyThread, Me.MenuItemThColumns})
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 1
        Me.MenuItem22.Text = "-"
        '
        'MenuItemThPriority
        '
        Me.MenuItemThPriority.Index = 5
        Me.MenuItemThPriority.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemThIdle, Me.MenuItemThLowest, Me.MenuItemThBNormal, Me.MenuItemThNorm, Me.MenuItemThANorm, Me.MenuItemThHighest, Me.MenuItemThTimeCr})
        Me.MenuItemThPriority.Text = "Priority"
        '
        'MenuItemThAffinity
        '
        Me.MenuItemThAffinity.Index = 6
        Me.MenuItemThAffinity.Text = "Set affinity..."
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 7
        Me.MenuItem19.Text = "-"
        '
        'MenuItemThColumns
        '
        Me.MenuItemThColumns.Index = 9
        Me.MenuItemThColumns.Text = "Choose columns..."
        '
        'mnuModule
        '
        Me.mnuModule.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemModuleSelProc, Me.MenuItem26, Me.MenuItemModuleFileProp, Me.MenuItemModuleOpenDir, Me.MenuItemModuleFileDetails, Me.MenuItemModuleSearch, Me.MenuItemModuleDependencies, Me.MenuItem13, Me.MenuItemUnloadModule, Me.MenuItem21, Me.MenuItemCopyModule, Me.MenuItemColumnsModule})
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = 1
        Me.MenuItem26.Text = "-"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 7
        Me.MenuItem13.Text = "-"
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 9
        Me.MenuItem21.Text = "-"
        '
        'MenuItemColumnsModule
        '
        Me.MenuItemColumnsModule.Index = 11
        Me.MenuItemColumnsModule.Text = "Choose columns..."
        '
        'mnuWindow
        '
        Me.mnuWindow.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemWindowSelProc, Me.MenuItem23, Me.MenuItemWShow, Me.MenuItemWSUnnamed, Me.MenuItemWHide, Me.MenuItemWClose, Me.MenuItem36, Me.MenuItemWVisibility, Me.MenuItem69, Me.MenuItemWEnable, Me.MenuItemWDisab, Me.MenuItem74, Me.MenuItemCopyWindow, Me.MenuItemWindowColumns})
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 1
        Me.MenuItem23.Text = "-"
        '
        'MenuItemWShow
        '
        Me.VistaMenu.SetImage(Me.MenuItemWShow, Global.YAPM.My.Resources.Resources.display16)
        Me.MenuItemWShow.Index = 2
        Me.MenuItemWShow.Text = "Show"
        '
        'MenuItemWSUnnamed
        '
        Me.MenuItemWSUnnamed.Index = 3
        Me.MenuItemWSUnnamed.Text = "Show unnamed"
        '
        'MenuItemWHide
        '
        Me.MenuItemWHide.Index = 4
        Me.MenuItemWHide.Text = "Hide"
        '
        'MenuItemWClose
        '
        Me.VistaMenu.SetImage(Me.MenuItemWClose, Global.YAPM.My.Resources.Resources.close)
        Me.MenuItemWClose.Index = 5
        Me.MenuItemWClose.Text = "Close"
        '
        'MenuItem36
        '
        Me.MenuItem36.Index = 6
        Me.MenuItem36.Text = "-"
        '
        'MenuItemWVisibility
        '
        Me.MenuItemWVisibility.Index = 7
        Me.MenuItemWVisibility.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemWFront, Me.MenuItemWNotFront, Me.MenuItemWActive, Me.MenuItemWForeground, Me.MenuItem50, Me.MenuItemWMin, Me.MenuItemWMax, Me.MenuItemWPos, Me.MenuItem57, Me.MenuItemWFlash, Me.MenuItemWStopFlash, Me.MenuItem63, Me.MenuItemWOpacity, Me.MenuItemWCaption})
        Me.MenuItemWVisibility.Text = "Visibility"
        '
        'MenuItemWFront
        '
        Me.MenuItemWFront.Index = 0
        Me.MenuItemWFront.Text = "Bring to front"
        '
        'MenuItemWNotFront
        '
        Me.MenuItemWNotFront.Index = 1
        Me.MenuItemWNotFront.Text = "Do not bring to front"
        '
        'MenuItemWActive
        '
        Me.MenuItemWActive.Index = 2
        Me.MenuItemWActive.Text = "Set as active window"
        '
        'MenuItemWForeground
        '
        Me.MenuItemWForeground.Index = 3
        Me.MenuItemWForeground.Text = "Set as foreground window"
        '
        'MenuItem50
        '
        Me.MenuItem50.Index = 4
        Me.MenuItem50.Text = "-"
        '
        'MenuItemWMin
        '
        Me.MenuItemWMin.Index = 5
        Me.MenuItemWMin.Text = "Minimize"
        '
        'MenuItemWMax
        '
        Me.MenuItemWMax.Index = 6
        Me.MenuItemWMax.Text = "Maximize"
        '
        'MenuItemWPos
        '
        Me.MenuItemWPos.Index = 7
        Me.MenuItemWPos.Text = "Position && size"
        '
        'MenuItem57
        '
        Me.MenuItem57.Index = 8
        Me.MenuItem57.Text = "-"
        '
        'MenuItemWFlash
        '
        Me.MenuItemWFlash.Index = 9
        Me.MenuItemWFlash.Text = "Flash"
        '
        'MenuItemWStopFlash
        '
        Me.MenuItemWStopFlash.Index = 10
        Me.MenuItemWStopFlash.Text = "Stop flashing"
        '
        'MenuItem63
        '
        Me.MenuItem63.Index = 11
        Me.MenuItem63.Text = "-"
        '
        'MenuItemWOpacity
        '
        Me.MenuItemWOpacity.Index = 12
        Me.MenuItemWOpacity.Text = "Change opacity..."
        '
        'MenuItemWCaption
        '
        Me.MenuItemWCaption.Index = 13
        Me.MenuItemWCaption.Text = "Change caption..."
        '
        'MenuItem69
        '
        Me.MenuItem69.Index = 8
        Me.MenuItem69.Text = "-"
        '
        'MenuItemWEnable
        '
        Me.MenuItemWEnable.Index = 9
        Me.MenuItemWEnable.Text = "Enable"
        '
        'MenuItemWDisab
        '
        Me.MenuItemWDisab.Index = 10
        Me.MenuItemWDisab.Text = "Disable"
        '
        'MenuItem74
        '
        Me.MenuItem74.Index = 11
        Me.MenuItem74.Text = "-"
        '
        'MenuItemWindowColumns
        '
        Me.MenuItemWindowColumns.Index = 13
        Me.MenuItemWindowColumns.Text = "Choose columns..."
        '
        'mnuSearch
        '
        Me.mnuSearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSearchNew, Me.MenuItem28, Me.MenuItemSearchSel, Me.MenuItemSearchClose, Me.MenuItem30, Me.MenuItemCopySearch})
        '
        'MenuItemSearchNew
        '
        Me.MenuItemSearchNew.DefaultItem = True
        Me.MenuItemSearchNew.Index = 0
        Me.MenuItemSearchNew.Text = "New search..."
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 1
        Me.MenuItem28.Text = "-"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 4
        Me.MenuItem30.Text = "-"
        '
        'mnuProcess
        '
        Me.mnuProcess.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcKill, Me.MenuItemProcKillT, Me.MenuItemProcStop, Me.MenuItemProcResume, Me.MenuItemProcPriority, Me.MenuItem44, Me.MenuItemProcReanalize, Me.MenuItem27, Me.MenuItem35, Me.MenuItem51, Me.MenuItemProcSFileProp, Me.MenuItemProcSOpenDir, Me.MenuItemProcSFileDetails, Me.MenuItemProcSSearch, Me.MenuItemProcSDep, Me.MenuItem38, Me.MenuItemCopyProcess, Me.MenuItemProcColumns})
        '
        'MenuItemProcPriority
        '
        Me.MenuItemProcPriority.Index = 4
        Me.MenuItemProcPriority.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcPIdle, Me.MenuItemProcPBN, Me.MenuItemProcPN, Me.MenuItemProcPAN, Me.MenuItemProcPH, Me.MenuItemProcPRT})
        Me.MenuItemProcPriority.Text = "Priority"
        '
        'MenuItem44
        '
        Me.MenuItem44.Index = 5
        Me.MenuItem44.Text = "-"
        '
        'MenuItemProcReanalize
        '
        Me.MenuItemProcReanalize.Index = 6
        Me.MenuItemProcReanalize.Text = "Reanalize"
        '
        'MenuItem27
        '
        Me.MenuItem27.Index = 7
        Me.MenuItem27.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcSModules, Me.MenuItemProcSThreads, Me.MenuItemProcSHandles, Me.MenuItemProcSWindows, Me.MenuItemProcSServices, Me.MenuItemProcSAll})
        Me.MenuItem27.Text = "Show"
        '
        'MenuItemProcSModules
        '
        Me.MenuItemProcSModules.Index = 0
        Me.MenuItemProcSModules.Text = "Show modules"
        '
        'MenuItemProcSThreads
        '
        Me.MenuItemProcSThreads.Index = 1
        Me.MenuItemProcSThreads.Text = "Show threads"
        '
        'MenuItemProcSHandles
        '
        Me.MenuItemProcSHandles.Index = 2
        Me.MenuItemProcSHandles.Text = "Show handles"
        '
        'MenuItemProcSWindows
        '
        Me.MenuItemProcSWindows.Index = 3
        Me.MenuItemProcSWindows.Text = "Show windows"
        '
        'MenuItemProcSServices
        '
        Me.MenuItemProcSServices.Index = 4
        Me.MenuItemProcSServices.Text = "Show services"
        '
        'MenuItemProcSAll
        '
        Me.MenuItemProcSAll.Index = 5
        Me.MenuItemProcSAll.Text = "Show all"
        '
        'MenuItem35
        '
        Me.MenuItem35.Index = 8
        Me.MenuItem35.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcWSS, Me.MenuItemProcAff, Me.MenuItemProcDump})
        Me.MenuItem35.Text = "Other"
        '
        'MenuItemProcWSS
        '
        Me.MenuItemProcWSS.Index = 0
        Me.MenuItemProcWSS.Text = "Reduce working set size"
        '
        'MenuItemProcAff
        '
        Me.MenuItemProcAff.Index = 1
        Me.MenuItemProcAff.Text = "Set affinity..."
        '
        'MenuItemProcDump
        '
        Me.MenuItemProcDump.Index = 2
        Me.MenuItemProcDump.Text = "Create dump file..."
        '
        'MenuItem51
        '
        Me.MenuItem51.Index = 9
        Me.MenuItem51.Text = "-"
        '
        'MenuItem38
        '
        Me.MenuItem38.Index = 15
        Me.MenuItem38.Text = "-"
        '
        'MenuItemProcColumns
        '
        Me.MenuItemProcColumns.Index = 17
        Me.MenuItemProcColumns.Text = "Choose columns..."
        '
        'mnuSystem
        '
        Me.mnuSystem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSYSTEMFILE, Me.MenuItemProcesses, Me.MenuItemModules, Me.MenuItemThreads, Me.MenuItemHandles, Me.MenuItemMonitor, Me.MenuItemWindows, Me.MenuItemServices, Me.MenuItemSearch, Me.MenuItemFiles, Me.MenuItemSYSTEMOPT, Me.MenuItemSYSTEMTOOLS, Me.MenuItemSYSTEMSYSTEM, Me.MenuItemSYSTEMHEL})
        '
        'MenuItemSYSTEMFILE
        '
        Me.MenuItemSYSTEMFILE.Index = 0
        Me.MenuItemSYSTEMFILE.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemRefresh, Me.MenuItem54, Me.MenuItemSystemConnection, Me.MenuItemRunAsAdmin, Me.MenuItem56, Me.MenuItemSystemLog, Me.MenuItemSystemReport, Me.MenuItem59, Me.MenuItemSystemInfos, Me.MenuItemSystemOpenedWindows, Me.MenuItemShowPendingTasks, Me.MenuItem62, Me.MenuItemSystemFindWindow, Me.MenuItemSystemEmergency, Me.MenuItemSystemSBA, Me.MenuItem66, Me.MenuItemSystemToTray, Me.MenuItemSystemExit})
        Me.MenuItemSYSTEMFILE.Text = "&File"
        '
        'MenuItem54
        '
        Me.MenuItem54.Index = 1
        Me.MenuItem54.Text = "-"
        '
        'MenuItemSystemConnection
        '
        Me.MenuItemSystemConnection.Index = 2
        Me.MenuItemSystemConnection.Text = "&Connection..."
        '
        'MenuItemRunAsAdmin
        '
        Me.MenuItemRunAsAdmin.Index = 3
        Me.MenuItemRunAsAdmin.Text = "&Restart with privileges"
        '
        'MenuItem56
        '
        Me.MenuItem56.Index = 4
        Me.MenuItem56.Text = "-"
        '
        'MenuItemSystemReport
        '
        Me.MenuItemSystemReport.Index = 6
        Me.MenuItemSystemReport.Text = "Save &system report..."
        '
        'MenuItem59
        '
        Me.MenuItem59.Index = 7
        Me.MenuItem59.Text = "-"
        '
        'MenuItemShowPendingTasks
        '
        Me.MenuItemShowPendingTasks.Index = 10
        Me.MenuItemShowPendingTasks.Text = "&Pending tasks..."
        '
        'MenuItem62
        '
        Me.MenuItem62.Index = 11
        Me.MenuItem62.Text = "-"
        '
        'MenuItemSystemEmergency
        '
        Me.MenuItemSystemEmergency.Index = 13
        Me.MenuItemSystemEmergency.Text = "Emergency &hotkeys..."
        '
        'MenuItemSystemSBA
        '
        Me.MenuItemSystemSBA.Enabled = False
        Me.MenuItemSystemSBA.Index = 14
        Me.MenuItemSystemSBA.Text = "State &based actions..."
        Me.MenuItemSystemSBA.Visible = False
        '
        'MenuItem66
        '
        Me.MenuItem66.Index = 15
        Me.MenuItem66.Text = "-"
        '
        'MenuItemProcesses
        '
        Me.MenuItemProcesses.Index = 1
        Me.MenuItemProcesses.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportProcesses})
        Me.MenuItemProcesses.Text = "&Processes"
        Me.MenuItemProcesses.Visible = False
        '
        'MenuItemReportProcesses
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportProcesses, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportProcesses.Index = 0
        Me.MenuItemReportProcesses.Text = "&Save report..."
        '
        'MenuItemModules
        '
        Me.MenuItemModules.Index = 2
        Me.MenuItemModules.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportModules})
        Me.MenuItemModules.Text = "&Modules"
        Me.MenuItemModules.Visible = False
        '
        'MenuItemReportModules
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportModules, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportModules.Index = 0
        Me.MenuItemReportModules.Text = "&Save report..."
        '
        'MenuItemThreads
        '
        Me.MenuItemThreads.Index = 3
        Me.MenuItemThreads.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportThreads})
        Me.MenuItemThreads.Text = "&Threads"
        Me.MenuItemThreads.Visible = False
        '
        'MenuItemReportThreads
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportThreads, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportThreads.Index = 0
        Me.MenuItemReportThreads.Text = "&Save report..."
        '
        'MenuItemHandles
        '
        Me.MenuItemHandles.Index = 4
        Me.MenuItemHandles.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportHandles})
        Me.MenuItemHandles.Text = "&Handles"
        Me.MenuItemHandles.Visible = False
        '
        'MenuItemReportHandles
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportHandles, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportHandles.Index = 0
        Me.MenuItemReportHandles.Text = "&Save report..."
        '
        'MenuItemMonitor
        '
        Me.MenuItemMonitor.Index = 5
        Me.MenuItemMonitor.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportMonitor})
        Me.MenuItemMonitor.Text = "&Monitor"
        Me.MenuItemMonitor.Visible = False
        '
        'MenuItemReportMonitor
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportMonitor, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportMonitor.Index = 0
        Me.MenuItemReportMonitor.Text = "&Save report..."
        '
        'MenuItemWindows
        '
        Me.MenuItemWindows.Index = 6
        Me.MenuItemWindows.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem67, Me.MenuItemWEnalbe, Me.MenuItemWDisable, Me.MenuItem71, Me.MenuItemReportWindows})
        Me.MenuItemWindows.Text = "&Windows"
        Me.MenuItemWindows.Visible = False
        '
        'MenuItem67
        '
        Me.MenuItem67.Index = 0
        Me.MenuItem67.Text = "-"
        '
        'MenuItemWEnalbe
        '
        Me.MenuItemWEnalbe.Index = 1
        Me.MenuItemWEnalbe.Text = "Enable"
        '
        'MenuItemWDisable
        '
        Me.MenuItemWDisable.Index = 2
        Me.MenuItemWDisable.Text = "Disable"
        '
        'MenuItem71
        '
        Me.MenuItem71.Index = 3
        Me.MenuItem71.Text = "-"
        '
        'MenuItemReportWindows
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportWindows, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportWindows.Index = 4
        Me.MenuItemReportWindows.Text = "&Save report..."
        '
        'MenuItemServices
        '
        Me.MenuItemServices.Index = 7
        Me.MenuItemServices.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportServices})
        Me.MenuItemServices.Text = "&Services"
        Me.MenuItemServices.Visible = False
        '
        'MenuItemReportServices
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportServices, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportServices.Index = 0
        Me.MenuItemReportServices.Text = "&Save report..."
        '
        'MenuItemSearch
        '
        Me.MenuItemSearch.Index = 8
        Me.MenuItemSearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemNewSearch, Me.MenuItem61, Me.MenuItemReportSearch})
        Me.MenuItemSearch.Text = "&Search"
        Me.MenuItemSearch.Visible = False
        '
        'MenuItemNewSearch
        '
        Me.MenuItemNewSearch.DefaultItem = True
        Me.MenuItemNewSearch.Index = 0
        Me.MenuItemNewSearch.Text = "&New search..."
        '
        'MenuItem61
        '
        Me.MenuItem61.Index = 1
        Me.MenuItem61.Text = "-"
        '
        'MenuItemReportSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemReportSearch, Global.YAPM.My.Resources.Resources._096)
        Me.MenuItemReportSearch.Index = 2
        Me.MenuItemReportSearch.Text = "&Save report..."
        '
        'MenuItemFiles
        '
        Me.MenuItemFiles.Index = 9
        Me.MenuItemFiles.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFileOpen, Me.MenuItem68, Me.MenuItemFileRelease, Me.MenuItemFileDelete, Me.MenuItemFileTrash, Me.MenuItem72, Me.MenuItemFileRename, Me.MenuItemFileShellOpen, Me.MenuItemFileMove, Me.MenuItemFileCopy, Me.MenuItem77, Me.MenuItemFileEncrypt, Me.MenuItemFileDecrypt, Me.MenuItem80, Me.MenuItemFileStrings})
        Me.MenuItemFiles.Text = "&Files"
        Me.MenuItemFiles.Visible = False
        '
        'MenuItemFileOpen
        '
        Me.MenuItemFileOpen.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemFileOpen, Global.YAPM.My.Resources.Resources.folder_open)
        Me.MenuItemFileOpen.Index = 0
        Me.MenuItemFileOpen.Text = "&Open file..."
        '
        'MenuItem68
        '
        Me.MenuItem68.Index = 1
        Me.MenuItem68.Text = "-"
        '
        'MenuItemFileRelease
        '
        Me.VistaMenu.SetImage(Me.MenuItemFileRelease, Global.YAPM.My.Resources.Resources.locked)
        Me.MenuItemFileRelease.Index = 2
        Me.MenuItemFileRelease.Text = "&Release file..."
        '
        'MenuItemFileDelete
        '
        Me.VistaMenu.SetImage(Me.MenuItemFileDelete, Global.YAPM.My.Resources.Resources.cross)
        Me.MenuItemFileDelete.Index = 3
        Me.MenuItemFileDelete.Text = "&Delete file..."
        '
        'MenuItemFileTrash
        '
        Me.VistaMenu.SetImage(Me.MenuItemFileTrash, Global.YAPM.My.Resources.Resources.cross_circle)
        Me.MenuItemFileTrash.Index = 4
        Me.MenuItemFileTrash.Text = "&Move to trash..."
        '
        'MenuItem72
        '
        Me.MenuItem72.Index = 5
        Me.MenuItem72.Text = "-"
        '
        'MenuItemFileRename
        '
        Me.MenuItemFileRename.Index = 6
        Me.MenuItemFileRename.Text = "&Rename..."
        '
        'MenuItemFileShellOpen
        '
        Me.MenuItemFileShellOpen.Index = 7
        Me.MenuItemFileShellOpen.Text = "&Open"
        '
        'MenuItemFileMove
        '
        Me.MenuItemFileMove.Index = 8
        Me.MenuItemFileMove.Text = "&Move..."
        '
        'MenuItemFileCopy
        '
        Me.MenuItemFileCopy.Index = 9
        Me.MenuItemFileCopy.Text = "&Copy..."
        '
        'MenuItem77
        '
        Me.MenuItem77.Index = 10
        Me.MenuItem77.Text = "-"
        '
        'MenuItemFileEncrypt
        '
        Me.MenuItemFileEncrypt.Index = 11
        Me.MenuItemFileEncrypt.Text = "&Encrypt"
        '
        'MenuItemFileDecrypt
        '
        Me.MenuItemFileDecrypt.Index = 12
        Me.MenuItemFileDecrypt.Text = "&Decrypt"
        '
        'MenuItem80
        '
        Me.MenuItem80.Index = 13
        Me.MenuItem80.Text = "-"
        '
        'MenuItemFileStrings
        '
        Me.MenuItemFileStrings.Index = 14
        Me.MenuItemFileStrings.Text = "&Show file strings"
        '
        'MenuItemSYSTEMOPT
        '
        Me.MenuItemSYSTEMOPT.Index = 10
        Me.MenuItemSYSTEMOPT.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemAlwaysVisible, Me.MenuItem37, Me.MenuItemSystemRefProc, Me.MenuItemSystemRefServ, Me.MenuItem42, Me.MenuItemSystemOptions})
        Me.MenuItemSYSTEMOPT.Text = "&Options"
        '
        'MenuItemSystemAlwaysVisible
        '
        Me.MenuItemSystemAlwaysVisible.Index = 0
        Me.MenuItemSystemAlwaysVisible.Text = "&Always visible"
        '
        'MenuItem37
        '
        Me.MenuItem37.Index = 1
        Me.MenuItem37.Text = "-"
        '
        'MenuItemSystemRefProc
        '
        Me.MenuItemSystemRefProc.Checked = True
        Me.MenuItemSystemRefProc.Index = 2
        Me.MenuItemSystemRefProc.Text = "Refresh &process list"
        '
        'MenuItemSystemRefServ
        '
        Me.MenuItemSystemRefServ.Checked = True
        Me.MenuItemSystemRefServ.Index = 3
        Me.MenuItemSystemRefServ.Text = "Refresh &service list"
        '
        'MenuItem42
        '
        Me.MenuItem42.Index = 4
        Me.MenuItem42.Text = "-"
        '
        'MenuItemSYSTEMTOOLS
        '
        Me.MenuItemSYSTEMTOOLS.Index = 11
        Me.MenuItemSYSTEMTOOLS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemShowHidden, Me.MenuItemSystemDependency})
        Me.MenuItemSYSTEMTOOLS.Text = "&Tools"
        '
        'MenuItemSystemShowHidden
        '
        Me.MenuItemSystemShowHidden.Index = 0
        Me.MenuItemSystemShowHidden.Text = "Show &hidden processes..."
        '
        'MenuItemSystemDependency
        '
        Me.MenuItemSystemDependency.Index = 1
        Me.MenuItemSystemDependency.Text = "&Dependency viewer..."
        '
        'MenuItemSYSTEMSYSTEM
        '
        Me.MenuItemSYSTEMSYSTEM.Index = 12
        Me.MenuItemSYSTEMSYSTEM.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem34})
        Me.MenuItemSYSTEMSYSTEM.Text = "&System"
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 0
        Me.MenuItem34.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemRestart, Me.MenuItemSystemShutdown, Me.MenuItemSystemPowerOff, Me.MenuItem40, Me.MenuItemSystemSleep, Me.MenuItemSystemHIbernate, Me.MenuItem43, Me.MenuItemSystemLogoff, Me.MenuItemSystemLock})
        Me.MenuItem34.Text = "Shutdown"
        '
        'MenuItemSystemRestart
        '
        Me.MenuItemSystemRestart.Index = 0
        Me.MenuItemSystemRestart.Text = "Restart"
        '
        'MenuItemSystemShutdown
        '
        Me.MenuItemSystemShutdown.Index = 1
        Me.MenuItemSystemShutdown.Text = "Shutdown"
        '
        'MenuItemSystemPowerOff
        '
        Me.MenuItemSystemPowerOff.Index = 2
        Me.MenuItemSystemPowerOff.Text = "Power off"
        '
        'MenuItem40
        '
        Me.MenuItem40.Index = 3
        Me.MenuItem40.Text = "-"
        '
        'MenuItemSystemSleep
        '
        Me.MenuItemSystemSleep.Index = 4
        Me.MenuItemSystemSleep.Text = "Sleep"
        '
        'MenuItemSystemHIbernate
        '
        Me.MenuItemSystemHIbernate.Index = 5
        Me.MenuItemSystemHIbernate.Text = "Hibernate"
        '
        'MenuItem43
        '
        Me.MenuItem43.Index = 6
        Me.MenuItem43.Text = "-"
        '
        'MenuItemSystemLogoff
        '
        Me.MenuItemSystemLogoff.Index = 7
        Me.MenuItemSystemLogoff.Text = "Log off"
        '
        'MenuItemSystemLock
        '
        Me.MenuItemSystemLock.Index = 8
        Me.MenuItemSystemLock.Text = "Lock"
        '
        'MenuItemSYSTEMHEL
        '
        Me.MenuItemSYSTEMHEL.Index = 13
        Me.MenuItemSYSTEMHEL.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemUpdate, Me.MenuItem39, Me.MenuItemSystemDonation, Me.MenuItemSystemFeedBack, Me.MenuItemSystemSF, Me.MenuItemSystemWebsite, Me.MenuItemSystemDownloads, Me.MenuItem49, Me.MenuItemSystemHelp, Me.MenuItemSystemAbout})
        Me.MenuItemSYSTEMHEL.Text = "&Help"
        '
        'MenuItem39
        '
        Me.MenuItem39.Index = 1
        Me.MenuItem39.Text = "-"
        '
        'MenuItemSystemDonation
        '
        Me.MenuItemSystemDonation.Index = 2
        Me.MenuItemSystemDonation.Text = "Make a &donation..."
        '
        'MenuItemSystemFeedBack
        '
        Me.MenuItemSystemFeedBack.Index = 3
        Me.MenuItemSystemFeedBack.Text = "Send &feed back..."
        '
        'MenuItemSystemSF
        '
        Me.MenuItemSystemSF.Index = 4
        Me.MenuItemSystemSF.Text = "&Project on Sourceforge.net"
        '
        'MenuItemSystemDownloads
        '
        Me.MenuItemSystemDownloads.Index = 6
        Me.MenuItemSystemDownloads.Text = "&Downloads"
        '
        'MenuItem49
        '
        Me.MenuItem49.Index = 7
        Me.MenuItem49.Text = "-"
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'StatusBar
        '
        Me.StatusBar.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.Location = New System.Drawing.Point(0, 533)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbPanelConnection, Me.sbPanelProcesses, Me.sbPanelServices, Me.sbPanelCpu, Me.sbPanelMemory})
        Me.StatusBar.ShowPanels = True
        Me.StatusBar.Size = New System.Drawing.Size(866, 20)
        Me.StatusBar.TabIndex = 62
        '
        'sbPanelConnection
        '
        Me.sbPanelConnection.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelConnection.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.sbPanelConnection.MinWidth = 100
        Me.sbPanelConnection.Name = "sbPanelConnection"
        Me.sbPanelConnection.Text = "Localhost"
        '
        'sbPanelProcesses
        '
        Me.sbPanelProcesses.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelProcesses.MinWidth = 80
        Me.sbPanelProcesses.Name = "sbPanelProcesses"
        Me.sbPanelProcesses.Text = "0 processes"
        Me.sbPanelProcesses.Width = 80
        '
        'sbPanelServices
        '
        Me.sbPanelServices.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelServices.MinWidth = 80
        Me.sbPanelServices.Name = "sbPanelServices"
        Me.sbPanelServices.Text = "0 services"
        Me.sbPanelServices.Width = 80
        '
        'sbPanelCpu
        '
        Me.sbPanelCpu.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelCpu.MinWidth = 80
        Me.sbPanelCpu.Name = "sbPanelCpu"
        Me.sbPanelCpu.Text = "CPU : 0%"
        Me.sbPanelCpu.Width = 80
        '
        'sbPanelMemory
        '
        Me.sbPanelMemory.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelMemory.MinWidth = 120
        Me.sbPanelMemory.Name = "sbPanelMemory"
        Me.sbPanelMemory.Text = "Phys. Memory : 0%"
        Me.sbPanelMemory.Width = 120
        '
        'timerStatus
        '
        Me.timerStatus.Enabled = True
        Me.timerStatus.Interval = 1000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(866, 553)
        Me.Controls.Add(Me._main)
        Me.Controls.Add(Me.StatusBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mnuSystem
        Me.MinimumSize = New System.Drawing.Size(882, 589)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Yet Another (remote) Process Monitor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me._main.Panel1.ResumeLayout(False)
        Me._main.Panel2.ResumeLayout(False)
        Me._main.ResumeLayout(False)
        Me.containerSystemMenu.Panel2.ResumeLayout(False)
        Me.containerSystemMenu.ResumeLayout(False)
        Me._tab.ResumeLayout(False)
        Me.pageTasks.ResumeLayout(False)
        Me.panelMain13.ResumeLayout(False)
        Me.SplitContainerTask.Panel1.ResumeLayout(False)
        Me.SplitContainerTask.Panel1.PerformLayout()
        Me.SplitContainerTask.Panel2.ResumeLayout(False)
        Me.SplitContainerTask.ResumeLayout(False)
        Me.pageProcesses.ResumeLayout(False)
        Me.containerProcessPage.Panel1.ResumeLayout(False)
        Me.containerProcessPage.Panel2.ResumeLayout(False)
        Me.containerProcessPage.ResumeLayout(False)
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        Me.panelMain.ResumeLayout(False)
        Me.SplitContainerProcess.Panel1.ResumeLayout(False)
        Me.SplitContainerProcess.ResumeLayout(False)
        Me.SplitContainerTvLv.Panel1.ResumeLayout(False)
        Me.SplitContainerTvLv.Panel2.ResumeLayout(False)
        Me.SplitContainerTvLv.ResumeLayout(False)
        Me.pageModules.ResumeLayout(False)
        Me.panelMain11.ResumeLayout(False)
        Me.splitModule.Panel1.ResumeLayout(False)
        Me.splitModule.Panel2.ResumeLayout(False)
        Me.splitModule.ResumeLayout(False)
        Me.SplitContainerModules.Panel1.ResumeLayout(False)
        Me.SplitContainerModules.Panel1.PerformLayout()
        Me.SplitContainerModules.Panel2.ResumeLayout(False)
        Me.SplitContainerModules.ResumeLayout(False)
        Me.pageThreads.ResumeLayout(False)
        Me.panelMain9.ResumeLayout(False)
        Me.splitThreads.Panel1.ResumeLayout(False)
        Me.splitThreads.Panel2.ResumeLayout(False)
        Me.splitThreads.ResumeLayout(False)
        Me.SplitContainerThreads.Panel1.ResumeLayout(False)
        Me.SplitContainerThreads.Panel1.PerformLayout()
        Me.SplitContainerThreads.Panel2.ResumeLayout(False)
        Me.SplitContainerThreads.ResumeLayout(False)
        Me.pageHandles.ResumeLayout(False)
        Me.panelMain7.ResumeLayout(False)
        Me.SplitContainerHandles.Panel1.ResumeLayout(False)
        Me.SplitContainerHandles.Panel1.PerformLayout()
        Me.SplitContainerHandles.Panel2.ResumeLayout(False)
        Me.SplitContainerHandles.ResumeLayout(False)
        Me.pageWindows.ResumeLayout(False)
        Me.panelMain10.ResumeLayout(False)
        Me.splitContainerWindows.Panel1.ResumeLayout(False)
        Me.splitContainerWindows.Panel2.ResumeLayout(False)
        Me.splitContainerWindows.ResumeLayout(False)
        Me.SplitContainerWindows2.Panel1.ResumeLayout(False)
        Me.SplitContainerWindows2.Panel1.PerformLayout()
        Me.SplitContainerWindows2.Panel2.ResumeLayout(False)
        Me.SplitContainerWindows2.ResumeLayout(False)
        Me.pageMonitor.ResumeLayout(False)
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
        Me.pageServices.ResumeLayout(False)
        Me.containerServicesPage.Panel1.ResumeLayout(False)
        Me.containerServicesPage.Panel2.ResumeLayout(False)
        Me.containerServicesPage.ResumeLayout(False)
        Me.panelMenu2.ResumeLayout(False)
        Me.panelMenu2.PerformLayout()
        Me.panelMain2.ResumeLayout(False)
        Me.splitServices.Panel1.ResumeLayout(False)
        Me.splitServices.Panel2.ResumeLayout(False)
        Me.splitServices.ResumeLayout(False)
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
        Me.pageNetwork.ResumeLayout(False)
        Me.panelMain14.ResumeLayout(False)
        Me.pageFile.ResumeLayout(False)
        Me.panelMain5.ResumeLayout(False)
        Me.SplitContainerFilexx.Panel1.ResumeLayout(False)
        Me.SplitContainerFilexx.Panel1.PerformLayout()
        Me.SplitContainerFilexx.Panel2.ResumeLayout(False)
        Me.SplitContainerFilexx.ResumeLayout(False)
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerFile.Panel1.ResumeLayout(False)
        Me.SplitContainerFile.Panel2.ResumeLayout(False)
        Me.SplitContainerFile.ResumeLayout(False)
        Me.SplitContainerFile2.Panel1.ResumeLayout(False)
        Me.SplitContainerFile2.Panel2.ResumeLayout(False)
        Me.SplitContainerFile2.ResumeLayout(False)
        Me.gpFileAttributes.ResumeLayout(False)
        Me.gpFileAttributes.PerformLayout()
        Me.gpFileDates.ResumeLayout(False)
        Me.gpFileDates.PerformLayout()
        Me.pageSearch.ResumeLayout(False)
        Me.panelMain6.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.PerformLayout()
        Me.SplitContainerSearch.Panel2.ResumeLayout(False)
        Me.SplitContainerSearch.ResumeLayout(False)
        Me.pageHelp.ResumeLayout(False)
        Me.panelMain4.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelConnection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelProcesses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelCpu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelMemory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timerProcess As System.Windows.Forms.Timer
    Friend WithEvents imgProcess As System.Windows.Forms.ImageList
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents timerServices As System.Windows.Forms.Timer
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
    Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
    Friend WithEvents saveDial As System.Windows.Forms.SaveFileDialog
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
    Friend WithEvents RBProcessOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butProcessGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents RBServiceOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butServiceGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents FileTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBFileDelete As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBFileOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBFileOther As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMoveFileToTrash As System.Windows.Forms.RibbonButton
    Friend WithEvents butDeleteFile As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileGoogleSearch As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileProperties As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileOpenDir As System.Windows.Forms.RibbonButton
    Friend WithEvents butServiceFileDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents SearchTab As System.Windows.Forms.RibbonTab
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
    Friend WithEvents RBUpdate As System.Windows.Forms.RibbonPanel
    Friend WithEvents butUpdate As System.Windows.Forms.RibbonButton
    Friend WithEvents RBSearchMain As System.Windows.Forms.RibbonPanel
    Friend WithEvents butSearchGo As System.Windows.Forms.RibbonButton
    Friend WithEvents butSearchSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents txtSearchString As System.Windows.Forms.RibbonTextBox
    Friend WithEvents HandlesTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBHandlesActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butHandleRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents butHandleClose As System.Windows.Forms.RibbonButton
    Friend WithEvents RBHandlesReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butHandlesSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents RBFileOpenFile As System.Windows.Forms.RibbonPanel
    Friend WithEvents butOpenFile As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents MonitorTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBMonitor As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMonitoringAdd As System.Windows.Forms.RibbonButton
    Friend WithEvents butMonitoringRemove As System.Windows.Forms.RibbonButton
    Friend WithEvents RBMonitoringControl As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMonitorStart As System.Windows.Forms.RibbonButton
    Friend WithEvents butMonitorStop As System.Windows.Forms.RibbonButton
    Friend WithEvents butSaveMonitorReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butMonitorSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents imgMonitor As System.Windows.Forms.ImageList
    Friend WithEvents timerMonitoring As System.Windows.Forms.Timer
    Friend WithEvents ThreadTab As System.Windows.Forms.RibbonTab
    Friend WithEvents WindowTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBThreadsRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butThreadRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBThreadAction As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBThreadPriority As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBThreadReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butThreadKill As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadStop As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadResume As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPriority As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPidle As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPlowest As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPbelow As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPnormal As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPabove As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPhighest As System.Windows.Forms.RibbonButton
    Friend WithEvents butThreadPcritical As System.Windows.Forms.RibbonButton
    Friend WithEvents RBWindowRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBWindowActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents RBWindowReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowSaveReport As System.Windows.Forms.RibbonButton
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
    Friend WithEvents butWindowStopFlashing As System.Windows.Forms.RibbonButton
    Friend WithEvents FolderChooser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ModulesTab As System.Windows.Forms.RibbonTab
    Friend WithEvents butProcessShowModules As System.Windows.Forms.RibbonButton
    Friend WithEvents RBModuleActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModuleRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents butModuleUnload As System.Windows.Forms.RibbonButton
    Friend WithEvents RBModuleReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModulesSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents RBServiceReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butServiceReport As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessShowAll As System.Windows.Forms.RibbonButton
    Friend WithEvents RBModuleOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModuleGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents RBOptions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butPreferences As System.Windows.Forms.RibbonButton
    Friend WithEvents butAlwaysDisplay As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessDisplayDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents EnableServiceRefreshingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TaskTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBTaskDisplay As System.Windows.Forms.RibbonPanel
    Friend WithEvents butTaskRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents RBTaskActions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butTaskShow As System.Windows.Forms.RibbonButton
    Friend WithEvents butTaskEndTask As System.Windows.Forms.RibbonButton
    Friend WithEvents timerTask As System.Windows.Forms.Timer
    Friend WithEvents RBWindowCapture As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowFind As System.Windows.Forms.RibbonButton
    Friend WithEvents NetworkTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBNetworkRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butNetworkRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents timerTrayIcon As System.Windows.Forms.Timer
    Friend WithEvents butProcessPermuteLvTv As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonSeparator1 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents panelProcessReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butSaveProcessReport As System.Windows.Forms.RibbonButton
    Friend WithEvents _main As System.Windows.Forms.SplitContainer
    Friend WithEvents containerSystemMenu As System.Windows.Forms.SplitContainer
    Friend WithEvents _tab As System.Windows.Forms.TabControl
    Friend WithEvents pageTasks As System.Windows.Forms.TabPage
    Friend WithEvents panelMain13 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerTask As System.Windows.Forms.SplitContainer
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblTaskCountResult As System.Windows.Forms.Label
    Friend WithEvents txtSearchTask As System.Windows.Forms.TextBox
    Friend WithEvents lvTask As taskList
    Friend WithEvents ColumnHeader62 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader63 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader64 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageProcesses As System.Windows.Forms.TabPage
    Friend WithEvents containerProcessPage As System.Windows.Forms.SplitContainer
    Friend WithEvents panelMenu As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblResCount As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents panelMain As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerProcess As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerTvLv As System.Windows.Forms.SplitContainer
    Friend WithEvents tvProc As System.Windows.Forms.TreeView
    Friend WithEvents lvProcess As processList
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageModules As System.Windows.Forms.TabPage
    Friend WithEvents panelMain11 As System.Windows.Forms.Panel
    Friend WithEvents splitModule As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerModules As System.Windows.Forms.SplitContainer
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblModulesCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchModule As System.Windows.Forms.TextBox
    Friend WithEvents lvModules As moduleList
    Friend WithEvents ColumnHeader29 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader43 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader44 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader45 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader46 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rtb6 As System.Windows.Forms.RichTextBox
    Friend WithEvents pageThreads As System.Windows.Forms.TabPage
    Friend WithEvents panelMain9 As System.Windows.Forms.Panel
    Friend WithEvents splitThreads As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerThreads As System.Windows.Forms.SplitContainer
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblThreadResults As System.Windows.Forms.Label
    Friend WithEvents txtSearchThread As System.Windows.Forms.TextBox
    Friend WithEvents rtb4 As System.Windows.Forms.RichTextBox
    Friend WithEvents pageHandles As System.Windows.Forms.TabPage
    Friend WithEvents panelMain7 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerHandles As System.Windows.Forms.SplitContainer
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblHandlesCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchHandle As System.Windows.Forms.TextBox
    Friend WithEvents lvHandles As handleList
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader27 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader28 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageWindows As System.Windows.Forms.TabPage
    Friend WithEvents panelMain10 As System.Windows.Forms.Panel
    Friend WithEvents splitContainerWindows As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerWindows2 As System.Windows.Forms.SplitContainer
    Friend WithEvents chkAllWindows As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblWindowsCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchWindow As System.Windows.Forms.TextBox
    Friend WithEvents lvWindows As windowList
    Friend WithEvents ColumnHeader30 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader31 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader39 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader40 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader41 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader42 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rtb5 As System.Windows.Forms.RichTextBox
    Friend WithEvents pageMonitor As System.Windows.Forms.TabPage
    Friend WithEvents panelMain8 As System.Windows.Forms.Panel
    Friend WithEvents splitMonitor As System.Windows.Forms.SplitContainer
    Friend WithEvents tvMonitor As System.Windows.Forms.TreeView
    Friend WithEvents splitMonitor2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtMonitoringLog As System.Windows.Forms.TextBox
    Friend WithEvents lvMonitorReport As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader47 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader48 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader49 As System.Windows.Forms.ColumnHeader
    Friend WithEvents splitMonitor3 As System.Windows.Forms.SplitContainer
    Friend WithEvents splitMonitor4 As System.Windows.Forms.SplitContainer
    Friend WithEvents graphMonitor As YAPM.Graph
    Friend WithEvents txtMonitorNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblMonitorMaxNumber As System.Windows.Forms.Label
    Friend WithEvents chkMonitorRightAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonitorLeftAuto As System.Windows.Forms.CheckBox
    Friend WithEvents dtMonitorR As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtMonitorL As System.Windows.Forms.DateTimePicker
    Friend WithEvents pageServices As System.Windows.Forms.TabPage
    Friend WithEvents containerServicesPage As System.Windows.Forms.SplitContainer
    Friend WithEvents panelMenu2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblResCount2 As System.Windows.Forms.Label
    Friend WithEvents txtServiceSearch As System.Windows.Forms.TextBox
    Friend WithEvents panelMain2 As System.Windows.Forms.Panel
    Friend WithEvents splitServices As System.Windows.Forms.SplitContainer
    Friend WithEvents lvServices As serviceList
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents splitServices2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdCopyServiceToCp As System.Windows.Forms.Button
    Friend WithEvents lblServicePath As System.Windows.Forms.TextBox
    Friend WithEvents lblServiceName As System.Windows.Forms.Label
    Friend WithEvents splitServices3 As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb2 As System.Windows.Forms.RichTextBox
    Friend WithEvents splitServices4 As System.Windows.Forms.SplitContainer
    Friend WithEvents tv2 As serviceDependenciesList
    Friend WithEvents tv As serviceDependenciesList
    Friend WithEvents pageNetwork As System.Windows.Forms.TabPage
    Friend WithEvents panelMain14 As System.Windows.Forms.Panel
    Friend WithEvents lvNetwork As networkList
    Friend WithEvents ColumnHeader66 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader67 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader68 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader69 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageFile As System.Windows.Forms.TabPage
    Friend WithEvents panelMain5 As System.Windows.Forms.Panel
    Friend WithEvents pageSearch As System.Windows.Forms.TabPage
    Friend WithEvents panelMain6 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerSearch As System.Windows.Forms.SplitContainer
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchResults As System.Windows.Forms.TextBox
    Friend WithEvents chkSearchWindows As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchHandles As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSearchModules As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchServices As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchProcess As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchCase As System.Windows.Forms.CheckBox
    Friend WithEvents lvSearchResults As searchList
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageHelp As System.Windows.Forms.TabPage
    Friend WithEvents panelMain4 As System.Windows.Forms.Panel
    Friend WithEvents WBHelp As System.Windows.Forms.WebBrowser
    Friend WithEvents timerNetwork As System.Windows.Forms.Timer
    Friend WithEvents timerStateBasedActions As System.Windows.Forms.Timer
    Friend WithEvents butLog As System.Windows.Forms.RibbonButton
    Friend WithEvents butSystemInfo As System.Windows.Forms.RibbonButton
    Friend WithEvents butWindows As System.Windows.Forms.RibbonButton
    Friend WithEvents butFindWindow As System.Windows.Forms.RibbonButton
    Friend WithEvents orbMenuEmergency As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents orbMenuSBA As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents RibbonSeparator2 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents orbMenuSaveReport As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents orbMenuAbout As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents orbMenuNetwork As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents RibbonSeparator4 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents butNetwork As System.Windows.Forms.RibbonButton
    Friend WithEvents lvThreads As threadList
    Friend WithEvents ColumnHeader32 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader33 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader34 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader35 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader36 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader38 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents butFeedBack As System.Windows.Forms.RibbonButton
    Friend WithEvents butHiddenProcesses As System.Windows.Forms.RibbonButton
    Friend WithEvents SplitContainerFilexx As System.Windows.Forms.SplitContainer
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdFileClipboard As System.Windows.Forms.Button
    Friend WithEvents pctFileSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pctFileBig As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainerFile2 As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb3 As System.Windows.Forms.RichTextBox
    Friend WithEvents gpFileAttributes As System.Windows.Forms.GroupBox
    Friend WithEvents chkFileEncrypted As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileContentNotIndexed As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileNormal As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileSystem As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileReadOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileHidden As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileCompressed As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileArchive As System.Windows.Forms.CheckBox
    Friend WithEvents gpFileDates As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetFileDates As System.Windows.Forms.Button
    Friend WithEvents DTlastModification As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTlastAccess As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTcreation As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SplitContainerFile As System.Windows.Forms.SplitContainer
    Friend WithEvents lvFileString As DoubleBufferedLV
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents butServiceDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents chkSearchEnvVar As System.Windows.Forms.CheckBox
    Friend WithEvents RibbonSeparator5 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents butExit As System.Windows.Forms.RibbonOrbOptionButton
    Friend WithEvents butShowPreferences As System.Windows.Forms.RibbonOrbOptionButton
    Friend WithEvents butShowDepViewer As System.Windows.Forms.RibbonButton
    Friend WithEvents butModuleViewModuleDep As System.Windows.Forms.RibbonButton
    Private WithEvents mnuHandle As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemHSelectAssociatedProcess As System.Windows.Forms.MenuItem
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItemCloseHandle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemShowUnnamedHandles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemChooseColumnsHandle As System.Windows.Forms.MenuItem
    Private WithEvents mnuTask As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemTaskShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskMax As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskMin As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskEnd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskSelWin As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuMonitor As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemMonitorAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitorRemove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitorStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitorStop As System.Windows.Forms.MenuItem
    Private WithEvents mnuFileCpPctBig As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyBig As System.Windows.Forms.MenuItem
    Private WithEvents mnuFileCpPctSmall As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopySmall As System.Windows.Forms.MenuItem
    Private WithEvents mnuMain As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemMainShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainToTray As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainAbout As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainAlwaysVisible As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainLog As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainSysInfo As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainOpenedW As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainEmergencyH As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainFindWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainSBA As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemRefProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainRefServ As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainRestart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainShutdown As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainPowerOff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainSleep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainHibernate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainLogOff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainLock As System.Windows.Forms.MenuItem
    Private WithEvents mnuService As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemServSelService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDepe As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServPause As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStop As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServAutoStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServOnDemand As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDisabled As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServReanalize As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuNetwork As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemServSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuThread As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemThSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThTerm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThSuspend As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThResu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThPriority As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThIdle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThLowest As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThBNormal As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThNorm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThANorm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThHighest As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThTimeCr As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThAffinity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuModule As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemModuleSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleDependencies As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemUnloadModule As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemColumnsModule As System.Windows.Forms.MenuItem
    Private WithEvents mnuWindow As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemWindowSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWindowColumns As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Private WithEvents mnuSearch As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemSearchNew As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSearchClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSearchSel As System.Windows.Forms.MenuItem
    Private WithEvents mnuProcess As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemProcSFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSDep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem38 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcColumns As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcKill As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcKillT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcStop As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcResume As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPriority As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcReanalize As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem44 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem51 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPIdle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPBN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPAN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPH As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPRT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem27 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSModules As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSThreads As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSHandles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSWindows As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSServices As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem35 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcWSS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcAff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcDump As System.Windows.Forms.MenuItem
    Private WithEvents mnuSystem As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItemSYSTEMFILE As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMOPT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMTOOLS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMSYSTEM As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMHEL As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRestart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemShutdown As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemPowerOff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem40 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemSleep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemHIbernate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem43 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemLogoff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemLock As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemShowHidden As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemDependency As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemAlwaysVisible As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem37 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRefProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRefServ As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem42 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemOptions As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem54 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemConnection As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem56 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemLog As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem59 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemInfos As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemOpenedWindows As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem62 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemFindWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemEmergency As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemSBA As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem66 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemToTray As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem39 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemDonation As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemFeedBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemSF As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemWebsite As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemDownloads As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem49 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemAbout As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNavigateHandle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopySearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyProcess As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyHandle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyTask As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyNetwork As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyThread As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyModule As System.Windows.Forms.MenuItem
    Friend WithEvents orbStartElevated As System.Windows.Forms.RibbonOrbMenuItem
    Friend WithEvents MenuItemRunAsAdmin As System.Windows.Forms.MenuItem
    Friend WithEvents headerString As System.Windows.Forms.ColumnHeader
    Friend WithEvents StatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents sbPanelConnection As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelProcesses As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelServices As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelCpu As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelMemory As System.Windows.Forms.StatusBarPanel
    Friend WithEvents timerStatus As System.Windows.Forms.Timer
    Friend WithEvents butShowAllPendingTasks As System.Windows.Forms.RibbonButton
    Friend WithEvents MenuItemShowPendingTasks As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcesses As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportProcesses As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModules As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportModules As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThreads As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportThreads As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemHandles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportHandles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitor As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportMonitor As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWindows As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportWindows As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServices As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportServices As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNewSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem61 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFiles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem68 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileRelease As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileTrash As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem72 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileRename As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileShellOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileMove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem77 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileEncrypt As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileDecrypt As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem80 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileStrings As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem67 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWEnalbe As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWDisable As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem71 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWSUnnamed As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWHide As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem36 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWVisibility As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWFront As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWNotFront As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWActive As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWForeground As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem50 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWMin As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWMax As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWPos As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem57 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWFlash As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWStopFlash As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem63 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWOpacity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWCaption As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem69 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWEnable As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWDisab As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem74 As System.Windows.Forms.MenuItem
    Friend WithEvents butProcessReduceWS As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessDumpF As System.Windows.Forms.RibbonButton

End Class
