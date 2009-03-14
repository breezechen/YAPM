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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Tasks", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("System")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("[System process]", New System.Windows.Forms.TreeNode() {TreeNode1})
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Modules", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Threads", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup9 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Handles", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup11 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Windows", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup12 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Processes", 1, 1)
        Dim ListViewGroup13 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup14 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup15 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup16 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
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
        Me.GoogleSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem37 = New System.Windows.Forms.ToolStripSeparator
        Me.chooseColumns = New System.Windows.Forms.ToolStripMenuItem
        Me.timerProcess = New System.Windows.Forms.Timer(Me.components)
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
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.timerServices = New System.Windows.Forms.Timer(Me.components)
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.mainMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowYAPMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MinimizeToTrayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutYAPMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AlwaysVisibleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.ShutdownToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShutdownToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.PoweroffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem51 = New System.Windows.Forms.ToolStripSeparator
        Me.SleepToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HibernateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem52 = New System.Windows.Forms.ToolStripSeparator
        Me.LogoffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem50 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveSystemReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowSystemInformatoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WindowManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EmergencyHotkeysToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FindAWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StateBasedActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem40 = New System.Windows.Forms.ToolStripSeparator
        Me.EnableProcessRefreshingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshServiceListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EnableServiceRefreshingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
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
        Me.RibbonSeparator1 = New System.Windows.Forms.RibbonSeparator
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
        Me.mnuFileCopyPctSmall = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFileCopyPctBig = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem
        Me.menuSearch = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NewSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem64 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAssociatedProcessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imgSearch = New System.Windows.Forms.ImageList(Me.components)
        Me.menuHandles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem22 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.DisplayUnnamedHandlesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem49 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.imgMonitor = New System.Windows.Forms.ImageList(Me.components)
        Me.timerMonitoring = New System.Windows.Forms.Timer(Me.components)
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
        Me.ToolStripMenuItem43 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.menuWindow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem34 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.FolderChooser = New System.Windows.Forms.FolderBrowserDialog
        Me.menuModule = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem35 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowFileDetailsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem36 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem39 = New System.Windows.Forms.ToolStripSeparator
        Me.GoogleSearchToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem41 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.menuTasks = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MaximizeWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MinimizeWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem46 = New System.Windows.Forms.ToolStripSeparator
        Me.EndTaskToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem47 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem45 = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectWindowInWindowsTabToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.timerTask = New System.Windows.Forms.Timer(Me.components)
        Me.menuNetwork = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem48 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem44 = New System.Windows.Forms.ToolStripSeparator
        Me.ChooseColumnsToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.timerTrayIcon = New System.Windows.Forms.Timer(Me.components)
        Me.butProcessPermuteLvTv = New System.Windows.Forms.RibbonButton
        Me._main = New System.Windows.Forms.SplitContainer
        Me.cmdTray = New System.Windows.Forms.Button
        Me.containerSystemMenu = New System.Windows.Forms.SplitContainer
        Me.menuSystem = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem60 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowlogToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemreportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem53 = New System.Windows.Forms.ToolStripSeparator
        Me.SysteminfosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenedWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem54 = New System.Windows.Forms.ToolStripSeparator
        Me.FindAWindowToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.EmergencyHotkeysToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.StateBasedActionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem55 = New System.Windows.Forms.ToolStripSeparator
        Me.MinimizeToTrayToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AlwaysVisibleToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem56 = New System.Windows.Forms.ToolStripSeparator
        Me.RefreshprocessListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshserviceListToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem57 = New System.Windows.Forms.ToolStripSeparator
        Me.OptionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.SystemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShutdownToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.RestartToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ShutdownToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.PoweroffToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem58 = New System.Windows.Forms.ToolStripSeparator
        Me.SleepToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.HibernateToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem59 = New System.Windows.Forms.ToolStripSeparator
        Me.LogoffToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.LockToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CheckupdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem85 = New System.Windows.Forms.ToolStripSeparator
        Me.MakeAdonationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WebsiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ProjectPageOnSourceforgenetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DownloadsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem86 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
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
        Me.menuMonitor = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem61 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem62 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem63 = New System.Windows.Forms.ToolStripSeparator
        Me.menuMonitorStart = New System.Windows.Forms.ToolStripMenuItem
        Me.menuMonitorStop = New System.Windows.Forms.ToolStripMenuItem
        Me.splitMonitor2 = New System.Windows.Forms.SplitContainer
        Me.txtMonitoringLog = New System.Windows.Forms.TextBox
        Me.lvMonitorReport = New YAPM.DoubleBufferedLV
        Me.ColumnHeader22 = New System.Windows.Forms.ColumnHeader
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
        Me.tv2 = New System.Windows.Forms.TreeView
        Me.tv = New System.Windows.Forms.TreeView
        Me.pageNetwork = New System.Windows.Forms.TabPage
        Me.panelMain14 = New System.Windows.Forms.Panel
        Me.lvNetwork = New YAPM.networkList
        Me.ColumnHeader66 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader67 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader68 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader69 = New System.Windows.Forms.ColumnHeader
        Me.pageFile = New System.Windows.Forms.TabPage
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
        Me.pctFileBig = New System.Windows.Forms.PictureBox
        Me.pageSearch = New System.Windows.Forms.TabPage
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
        Me.pageHelp = New System.Windows.Forms.TabPage
        Me.panelMain4 = New System.Windows.Forms.Panel
        Me.WBHelp = New System.Windows.Forms.WebBrowser
        Me.imgProcessTab = New System.Windows.Forms.ImageList(Me.components)
        Me.timerNetwork = New System.Windows.Forms.Timer(Me.components)
        Me.timerStateBasedActions = New System.Windows.Forms.Timer(Me.components)
        Me.ReduceWorkingSetSizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.menuProc.SuspendLayout()
        Me.menuService.SuspendLayout()
        Me.mainMenu.SuspendLayout()
        Me.mnuFileCopyPctSmall.SuspendLayout()
        Me.mnuFileCopyPctBig.SuspendLayout()
        Me.menuSearch.SuspendLayout()
        Me.menuHandles.SuspendLayout()
        Me.menuThread.SuspendLayout()
        Me.menuWindow.SuspendLayout()
        Me.menuModule.SuspendLayout()
        Me.menuTasks.SuspendLayout()
        Me.menuNetwork.SuspendLayout()
        Me._main.Panel1.SuspendLayout()
        Me._main.Panel2.SuspendLayout()
        Me._main.SuspendLayout()
        Me.containerSystemMenu.Panel1.SuspendLayout()
        Me.containerSystemMenu.Panel2.SuspendLayout()
        Me.containerSystemMenu.SuspendLayout()
        Me.menuSystem.SuspendLayout()
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
        Me.menuMonitor.SuspendLayout()
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
        Me.fileSplitContainer.Panel1.SuspendLayout()
        Me.fileSplitContainer.Panel2.SuspendLayout()
        Me.fileSplitContainer.SuspendLayout()
        Me.gpFileAttributes.SuspendLayout()
        Me.gpFileDates.SuspendLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageSearch.SuspendLayout()
        Me.panelMain6.SuspendLayout()
        Me.SplitContainerSearch.Panel1.SuspendLayout()
        Me.SplitContainerSearch.Panel2.SuspendLayout()
        Me.SplitContainerSearch.SuspendLayout()
        Me.pageHelp.SuspendLayout()
        Me.panelMain4.SuspendLayout()
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
        'menuProc
        '
        Me.menuProc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KillToolStripMenuItem, Me.KillProcessTreeToolStripMenuItem, Me.StopToolStripMenuItem, Me.ResumeToolStripMenuItem, Me.PriotiyToolStripMenuItem, Me.ReduceWorkingSetSizeToolStripMenuItem, Me.SetAffinityToolStripMenuItem, Me.ToolStripMenuItem38, Me.ShowModulesToolStripMenuItem, Me.ShowThreadsToolStripMenuItem, Me.ShowHandlesToolStripMenuItem, Me.ShowWindowsToolStripMenuItem, Me.ShowAllToolStripMenuItem, Me.SelectedServicesToolStripMenuItem, Me.ToolStripMenuItem8, Me.PropertiesToolStripMenuItem, Me.OpenFirectoryToolStripMenuItem, Me.FileDetailsToolStripMenuItem1, Me.GoogleSearchToolStripMenuItem, Me.ToolStripMenuItem37, Me.chooseColumns})
        Me.menuProc.Name = "menuProc"
        Me.menuProc.Size = New System.Drawing.Size(200, 440)
        '
        'KillToolStripMenuItem
        '
        Me.KillToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KillToolStripMenuItem.Image = CType(resources.GetObject("KillToolStripMenuItem.Image"), System.Drawing.Image)
        Me.KillToolStripMenuItem.Name = "KillToolStripMenuItem"
        Me.KillToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.KillToolStripMenuItem.Text = "Kill"
        '
        'KillProcessTreeToolStripMenuItem
        '
        Me.KillProcessTreeToolStripMenuItem.Image = CType(resources.GetObject("KillProcessTreeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.KillProcessTreeToolStripMenuItem.Name = "KillProcessTreeToolStripMenuItem"
        Me.KillProcessTreeToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.KillProcessTreeToolStripMenuItem.Text = "Kill process tree"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.control_stop_square
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'ResumeToolStripMenuItem
        '
        Me.ResumeToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.control
        Me.ResumeToolStripMenuItem.Name = "ResumeToolStripMenuItem"
        Me.ResumeToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ResumeToolStripMenuItem.Text = "Resume"
        '
        'PriotiyToolStripMenuItem
        '
        Me.PriotiyToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IdleToolStripMenuItem, Me.BelowNormalToolStripMenuItem, Me.NormalToolStripMenuItem, Me.AboveNormalToolStripMenuItem, Me.HighToolStripMenuItem, Me.RealTimeToolStripMenuItem})
        Me.PriotiyToolStripMenuItem.Name = "PriotiyToolStripMenuItem"
        Me.PriotiyToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.PriotiyToolStripMenuItem.Text = "Priority"
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
        Me.SetAffinityToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.SetAffinityToolStripMenuItem.Text = "Set affinity..."
        '
        'ToolStripMenuItem38
        '
        Me.ToolStripMenuItem38.Name = "ToolStripMenuItem38"
        Me.ToolStripMenuItem38.Size = New System.Drawing.Size(196, 6)
        '
        'ShowModulesToolStripMenuItem
        '
        Me.ShowModulesToolStripMenuItem.Image = CType(resources.GetObject("ShowModulesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowModulesToolStripMenuItem.Name = "ShowModulesToolStripMenuItem"
        Me.ShowModulesToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ShowModulesToolStripMenuItem.Text = "Show modules"
        '
        'ShowThreadsToolStripMenuItem
        '
        Me.ShowThreadsToolStripMenuItem.Image = CType(resources.GetObject("ShowThreadsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowThreadsToolStripMenuItem.Name = "ShowThreadsToolStripMenuItem"
        Me.ShowThreadsToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ShowThreadsToolStripMenuItem.Text = "Show threads"
        '
        'ShowHandlesToolStripMenuItem
        '
        Me.ShowHandlesToolStripMenuItem.Image = CType(resources.GetObject("ShowHandlesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowHandlesToolStripMenuItem.Name = "ShowHandlesToolStripMenuItem"
        Me.ShowHandlesToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ShowHandlesToolStripMenuItem.Text = "Show handles"
        '
        'ShowWindowsToolStripMenuItem
        '
        Me.ShowWindowsToolStripMenuItem.Image = CType(resources.GetObject("ShowWindowsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowWindowsToolStripMenuItem.Name = "ShowWindowsToolStripMenuItem"
        Me.ShowWindowsToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ShowWindowsToolStripMenuItem.Text = "Show windows"
        '
        'ShowAllToolStripMenuItem
        '
        Me.ShowAllToolStripMenuItem.Image = CType(resources.GetObject("ShowAllToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowAllToolStripMenuItem.Name = "ShowAllToolStripMenuItem"
        Me.ShowAllToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ShowAllToolStripMenuItem.Text = "Show all"
        '
        'SelectedServicesToolStripMenuItem
        '
        Me.SelectedServicesToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.gear
        Me.SelectedServicesToolStripMenuItem.Name = "SelectedServicesToolStripMenuItem"
        Me.SelectedServicesToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.SelectedServicesToolStripMenuItem.Text = "Selected services"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(196, 6)
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.document_text
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.PropertiesToolStripMenuItem.Text = "File properties"
        '
        'OpenFirectoryToolStripMenuItem
        '
        Me.OpenFirectoryToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.folder_open
        Me.OpenFirectoryToolStripMenuItem.Name = "OpenFirectoryToolStripMenuItem"
        Me.OpenFirectoryToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.OpenFirectoryToolStripMenuItem.Text = "Open directory"
        '
        'FileDetailsToolStripMenuItem1
        '
        Me.FileDetailsToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.magnifier
        Me.FileDetailsToolStripMenuItem1.Name = "FileDetailsToolStripMenuItem1"
        Me.FileDetailsToolStripMenuItem1.Size = New System.Drawing.Size(199, 22)
        Me.FileDetailsToolStripMenuItem1.Text = "File details"
        '
        'GoogleSearchToolStripMenuItem
        '
        Me.GoogleSearchToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.globe
        Me.GoogleSearchToolStripMenuItem.Name = "GoogleSearchToolStripMenuItem"
        Me.GoogleSearchToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.GoogleSearchToolStripMenuItem.Text = "Internet search"
        '
        'ToolStripMenuItem37
        '
        Me.ToolStripMenuItem37.Name = "ToolStripMenuItem37"
        Me.ToolStripMenuItem37.Size = New System.Drawing.Size(196, 6)
        '
        'chooseColumns
        '
        Me.chooseColumns.Name = "chooseColumns"
        Me.chooseColumns.Size = New System.Drawing.Size(199, 22)
        Me.chooseColumns.Text = "Choose columns..."
        '
        'timerProcess
        '
        Me.timerProcess.Interval = 1000
        '
        'menuService
        '
        Me.menuService.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem9, Me.ToolStripMenuItem10, Me.ShutdownToolStripMenuItem, Me.ToolStripMenuItem11, Me.ToolStripMenuItem12, Me.ToolStripSeparator2, Me.ToolStripMenuItem20, Me.ToolStripMenuItem21, Me.FileDetailsToolStripMenuItem, Me.ToolStripMenuItem2, Me.GoogleSearchToolStripMenuItem1, Me.ToolStripMenuItem42, Me.SelectedAssociatedProcessToolStripMenuItem1, Me.ToolStripMenuItem5, Me.ChooseColumnsToolStripMenuItem})
        Me.menuService.Name = "menuProc"
        Me.menuService.Size = New System.Drawing.Size(220, 270)
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Image = Global.YAPM.My.Resources.Resources.control_pause
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem9.Text = "Pause"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem10.Image = Global.YAPM.My.Resources.Resources.control_stop_square
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem10.Text = "Stop"
        '
        'ShutdownToolStripMenuItem
        '
        Me.ShutdownToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.cross
        Me.ShutdownToolStripMenuItem.Name = "ShutdownToolStripMenuItem"
        Me.ShutdownToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ShutdownToolStripMenuItem.Text = "Shutdown"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem11.Image = Global.YAPM.My.Resources.Resources.control
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
        Me.ToolStripMenuItem20.Image = Global.YAPM.My.Resources.Resources.document_text
        Me.ToolStripMenuItem20.Name = "ToolStripMenuItem20"
        Me.ToolStripMenuItem20.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem20.Text = "File properties"
        '
        'ToolStripMenuItem21
        '
        Me.ToolStripMenuItem21.Image = Global.YAPM.My.Resources.Resources.folder_open
        Me.ToolStripMenuItem21.Name = "ToolStripMenuItem21"
        Me.ToolStripMenuItem21.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem21.Text = "Open directory"
        '
        'FileDetailsToolStripMenuItem
        '
        Me.FileDetailsToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.magnifier
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
        Me.GoogleSearchToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.globe
        Me.GoogleSearchToolStripMenuItem1.Name = "GoogleSearchToolStripMenuItem1"
        Me.GoogleSearchToolStripMenuItem1.Size = New System.Drawing.Size(219, 22)
        Me.GoogleSearchToolStripMenuItem1.Text = "Internet search"
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
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(216, 6)
        '
        'ChooseColumnsToolStripMenuItem
        '
        Me.ChooseColumnsToolStripMenuItem.Name = "ChooseColumnsToolStripMenuItem"
        Me.ChooseColumnsToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ChooseColumnsToolStripMenuItem.Text = "Choose columns..."
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
        Me.Tray.ContextMenuStrip = Me.mainMenu
        Me.Tray.Icon = CType(resources.GetObject("Tray.Icon"), System.Drawing.Icon)
        Me.Tray.Text = "Yet Another Process Monitor"
        Me.Tray.Visible = True
        '
        'mainMenu
        '
        Me.mainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowYAPMToolStripMenuItem, Me.MinimizeToTrayToolStripMenuItem, Me.AboutYAPMToolStripMenuItem, Me.AlwaysVisibleToolStripMenuItem, Me.ToolStripMenuItem3, Me.ShutdownToolStripMenuItem1, Me.ToolStripMenuItem50, Me.ShowLogToolStripMenuItem, Me.SaveSystemReportToolStripMenuItem, Me.ShowSystemInformatoToolStripMenuItem, Me.WindowManagementToolStripMenuItem, Me.EmergencyHotkeysToolStripMenuItem, Me.FindAWindowToolStripMenuItem, Me.StateBasedActionsToolStripMenuItem, Me.ToolStripMenuItem40, Me.EnableProcessRefreshingToolStripMenuItem, Me.RefreshServiceListToolStripMenuItem, Me.ToolStripMenuItem4, Me.ExitToolStripMenuItem})
        Me.mainMenu.Name = "mainMenu"
        Me.mainMenu.Size = New System.Drawing.Size(192, 358)
        '
        'ShowYAPMToolStripMenuItem
        '
        Me.ShowYAPMToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowYAPMToolStripMenuItem.Image = CType(resources.GetObject("ShowYAPMToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowYAPMToolStripMenuItem.Name = "ShowYAPMToolStripMenuItem"
        Me.ShowYAPMToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.ShowYAPMToolStripMenuItem.Text = "Show YAPM"
        '
        'MinimizeToTrayToolStripMenuItem
        '
        Me.MinimizeToTrayToolStripMenuItem.Image = CType(resources.GetObject("MinimizeToTrayToolStripMenuItem.Image"), System.Drawing.Image)
        Me.MinimizeToTrayToolStripMenuItem.Name = "MinimizeToTrayToolStripMenuItem"
        Me.MinimizeToTrayToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.MinimizeToTrayToolStripMenuItem.Text = "Minimize to tray"
        '
        'AboutYAPMToolStripMenuItem
        '
        Me.AboutYAPMToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.information_frame
        Me.AboutYAPMToolStripMenuItem.Name = "AboutYAPMToolStripMenuItem"
        Me.AboutYAPMToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.AboutYAPMToolStripMenuItem.Text = "About YAPM"
        '
        'AlwaysVisibleToolStripMenuItem
        '
        Me.AlwaysVisibleToolStripMenuItem.Name = "AlwaysVisibleToolStripMenuItem"
        Me.AlwaysVisibleToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.AlwaysVisibleToolStripMenuItem.Text = "Always visible"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(188, 6)
        '
        'ShutdownToolStripMenuItem1
        '
        Me.ShutdownToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartToolStripMenuItem, Me.ShutdownToolStripMenuItem2, Me.PoweroffToolStripMenuItem, Me.ToolStripMenuItem51, Me.SleepToolStripMenuItem, Me.HibernateToolStripMenuItem, Me.ToolStripMenuItem52, Me.LogoffToolStripMenuItem, Me.LockToolStripMenuItem})
        Me.ShutdownToolStripMenuItem1.Name = "ShutdownToolStripMenuItem1"
        Me.ShutdownToolStripMenuItem1.Size = New System.Drawing.Size(191, 22)
        Me.ShutdownToolStripMenuItem1.Text = "Shutdown"
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.RestartToolStripMenuItem.Text = "Restart"
        '
        'ShutdownToolStripMenuItem2
        '
        Me.ShutdownToolStripMenuItem2.Name = "ShutdownToolStripMenuItem2"
        Me.ShutdownToolStripMenuItem2.Size = New System.Drawing.Size(128, 22)
        Me.ShutdownToolStripMenuItem2.Text = "Shutdown"
        '
        'PoweroffToolStripMenuItem
        '
        Me.PoweroffToolStripMenuItem.Name = "PoweroffToolStripMenuItem"
        Me.PoweroffToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.PoweroffToolStripMenuItem.Text = "Poweroff"
        '
        'ToolStripMenuItem51
        '
        Me.ToolStripMenuItem51.Name = "ToolStripMenuItem51"
        Me.ToolStripMenuItem51.Size = New System.Drawing.Size(125, 6)
        '
        'SleepToolStripMenuItem
        '
        Me.SleepToolStripMenuItem.Name = "SleepToolStripMenuItem"
        Me.SleepToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.SleepToolStripMenuItem.Text = "Sleep"
        '
        'HibernateToolStripMenuItem
        '
        Me.HibernateToolStripMenuItem.Name = "HibernateToolStripMenuItem"
        Me.HibernateToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.HibernateToolStripMenuItem.Text = "Hibernate"
        '
        'ToolStripMenuItem52
        '
        Me.ToolStripMenuItem52.Name = "ToolStripMenuItem52"
        Me.ToolStripMenuItem52.Size = New System.Drawing.Size(125, 6)
        '
        'LogoffToolStripMenuItem
        '
        Me.LogoffToolStripMenuItem.Name = "LogoffToolStripMenuItem"
        Me.LogoffToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.LogoffToolStripMenuItem.Text = "Logoff"
        '
        'LockToolStripMenuItem
        '
        Me.LockToolStripMenuItem.Name = "LockToolStripMenuItem"
        Me.LockToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.LockToolStripMenuItem.Text = "Lock"
        '
        'ToolStripMenuItem50
        '
        Me.ToolStripMenuItem50.Name = "ToolStripMenuItem50"
        Me.ToolStripMenuItem50.Size = New System.Drawing.Size(188, 6)
        '
        'ShowLogToolStripMenuItem
        '
        Me.ShowLogToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.document_text
        Me.ShowLogToolStripMenuItem.Name = "ShowLogToolStripMenuItem"
        Me.ShowLogToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.ShowLogToolStripMenuItem.Text = "Show log"
        '
        'SaveSystemReportToolStripMenuItem
        '
        Me.SaveSystemReportToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveSystemReportToolStripMenuItem.Name = "SaveSystemReportToolStripMenuItem"
        Me.SaveSystemReportToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.SaveSystemReportToolStripMenuItem.Text = "Save system report..."
        '
        'ShowSystemInformatoToolStripMenuItem
        '
        Me.ShowSystemInformatoToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowSystemInformatoToolStripMenuItem.Image = CType(resources.GetObject("ShowSystemInformatoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ShowSystemInformatoToolStripMenuItem.Name = "ShowSystemInformatoToolStripMenuItem"
        Me.ShowSystemInformatoToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.ShowSystemInformatoToolStripMenuItem.Text = "System information"
        '
        'WindowManagementToolStripMenuItem
        '
        Me.WindowManagementToolStripMenuItem.Image = CType(resources.GetObject("WindowManagementToolStripMenuItem.Image"), System.Drawing.Image)
        Me.WindowManagementToolStripMenuItem.Name = "WindowManagementToolStripMenuItem"
        Me.WindowManagementToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.WindowManagementToolStripMenuItem.Text = "Opened windows"
        '
        'EmergencyHotkeysToolStripMenuItem
        '
        Me.EmergencyHotkeysToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmergencyHotkeysToolStripMenuItem.Name = "EmergencyHotkeysToolStripMenuItem"
        Me.EmergencyHotkeysToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.EmergencyHotkeysToolStripMenuItem.Text = "Emergency hotkeys"
        '
        'FindAWindowToolStripMenuItem
        '
        Me.FindAWindowToolStripMenuItem.Image = CType(resources.GetObject("FindAWindowToolStripMenuItem.Image"), System.Drawing.Image)
        Me.FindAWindowToolStripMenuItem.Name = "FindAWindowToolStripMenuItem"
        Me.FindAWindowToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.FindAWindowToolStripMenuItem.Text = "Find a window"
        '
        'StateBasedActionsToolStripMenuItem
        '
        Me.StateBasedActionsToolStripMenuItem.Name = "StateBasedActionsToolStripMenuItem"
        Me.StateBasedActionsToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.StateBasedActionsToolStripMenuItem.Text = "State based actions..."
        '
        'ToolStripMenuItem40
        '
        Me.ToolStripMenuItem40.Name = "ToolStripMenuItem40"
        Me.ToolStripMenuItem40.Size = New System.Drawing.Size(188, 6)
        '
        'EnableProcessRefreshingToolStripMenuItem
        '
        Me.EnableProcessRefreshingToolStripMenuItem.Checked = True
        Me.EnableProcessRefreshingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableProcessRefreshingToolStripMenuItem.Name = "EnableProcessRefreshingToolStripMenuItem"
        Me.EnableProcessRefreshingToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.EnableProcessRefreshingToolStripMenuItem.Text = "Refresh process list"
        '
        'RefreshServiceListToolStripMenuItem
        '
        Me.RefreshServiceListToolStripMenuItem.Checked = True
        Me.RefreshServiceListToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RefreshServiceListToolStripMenuItem.Name = "RefreshServiceListToolStripMenuItem"
        Me.RefreshServiceListToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.RefreshServiceListToolStripMenuItem.Text = "Refresh service list"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(188, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.cross
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
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
        Me.Ribbon.Size = New System.Drawing.Size(866, 115)
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
        Me.RBProcessOnline.Items.Add(Me.butProcessGoogle)
        Me.RBProcessOnline.Tag = Nothing
        Me.RBProcessOnline.Text = "Online"
        '
        'butProcessGoogle
        '
        Me.butProcessGoogle.AltKey = Nothing
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
        Me.RBServiceAction.Items.Add(Me.RibbonSeparator1)
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
        Me.butShutdownService.Image = Global.YAPM.My.Resources.Resources.delete2
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
        'menuSearch
        '
        Me.menuSearch.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewSearchToolStripMenuItem, Me.ToolStripMenuItem64, Me.SelectAssociatedProcessToolStripMenuItem, Me.ToolStripMenuItem18, Me.CloseToolStripMenuItem})
        Me.menuSearch.Name = "menuProc"
        Me.menuSearch.Size = New System.Drawing.Size(248, 82)
        '
        'NewSearchToolStripMenuItem
        '
        Me.NewSearchToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewSearchToolStripMenuItem.Name = "NewSearchToolStripMenuItem"
        Me.NewSearchToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.NewSearchToolStripMenuItem.Text = "New search..."
        '
        'ToolStripMenuItem64
        '
        Me.ToolStripMenuItem64.Name = "ToolStripMenuItem64"
        Me.ToolStripMenuItem64.Size = New System.Drawing.Size(244, 6)
        '
        'SelectAssociatedProcessToolStripMenuItem
        '
        Me.SelectAssociatedProcessToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.CloseToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.cross
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.CloseToolStripMenuItem.Text = "Close item"
        '
        'imgSearch
        '
        Me.imgSearch.ImageStream = CType(resources.GetObject("imgSearch.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgSearch.TransparentColor = System.Drawing.Color.Transparent
        Me.imgSearch.Images.SetKeyName(0, "handle")
        Me.imgSearch.Images.SetKeyName(1, "dll")
        Me.imgSearch.Images.SetKeyName(2, "window")
        Me.imgSearch.Images.SetKeyName(3, "noicon")
        Me.imgSearch.Images.SetKeyName(4, "service")
        '
        'menuHandles
        '
        Me.menuHandles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem19, Me.ToolStripSeparator3, Me.ToolStripMenuItem22, Me.ToolStripMenuItem1, Me.DisplayUnnamedHandlesToolStripMenuItem, Me.ToolStripMenuItem49, Me.ChooseColumnsToolStripMenuItem6})
        Me.menuHandles.Name = "menuProc"
        Me.menuHandles.Size = New System.Drawing.Size(211, 110)
        '
        'ToolStripMenuItem19
        '
        Me.ToolStripMenuItem19.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem19.Name = "ToolStripMenuItem19"
        Me.ToolStripMenuItem19.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem19.Text = "&Select associated process"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(207, 6)
        '
        'ToolStripMenuItem22
        '
        Me.ToolStripMenuItem22.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem22.Image = Global.YAPM.My.Resources.Resources.cross
        Me.ToolStripMenuItem22.Name = "ToolStripMenuItem22"
        Me.ToolStripMenuItem22.Size = New System.Drawing.Size(210, 22)
        Me.ToolStripMenuItem22.Text = "Close item"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(207, 6)
        '
        'DisplayUnnamedHandlesToolStripMenuItem
        '
        Me.DisplayUnnamedHandlesToolStripMenuItem.Name = "DisplayUnnamedHandlesToolStripMenuItem"
        Me.DisplayUnnamedHandlesToolStripMenuItem.Size = New System.Drawing.Size(210, 22)
        Me.DisplayUnnamedHandlesToolStripMenuItem.Text = "Display unnamed handles"
        '
        'ToolStripMenuItem49
        '
        Me.ToolStripMenuItem49.Name = "ToolStripMenuItem49"
        Me.ToolStripMenuItem49.Size = New System.Drawing.Size(207, 6)
        '
        'ChooseColumnsToolStripMenuItem6
        '
        Me.ChooseColumnsToolStripMenuItem6.Name = "ChooseColumnsToolStripMenuItem6"
        Me.ChooseColumnsToolStripMenuItem6.Size = New System.Drawing.Size(210, 22)
        Me.ChooseColumnsToolStripMenuItem6.Text = "Choose columns..."
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
        'menuThread
        '
        Me.menuThread.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem23, Me.ToolStripMenuItem24, Me.ToolStripMenuItem25, Me.ToolStripMenuItem26, Me.ToolStripMenuItem33, Me.SelectedAssociatedProcessToolStripMenuItem, Me.ToolStripMenuItem43, Me.ChooseColumnsToolStripMenuItem4})
        Me.menuThread.Name = "menuProc"
        Me.menuThread.Size = New System.Drawing.Size(220, 164)
        '
        'ToolStripMenuItem23
        '
        Me.ToolStripMenuItem23.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem23.Image = Global.YAPM.My.Resources.Resources.cross
        Me.ToolStripMenuItem23.Name = "ToolStripMenuItem23"
        Me.ToolStripMenuItem23.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem23.Text = "Terminate"
        '
        'ToolStripMenuItem24
        '
        Me.ToolStripMenuItem24.Image = Global.YAPM.My.Resources.Resources.control_pause
        Me.ToolStripMenuItem24.Name = "ToolStripMenuItem24"
        Me.ToolStripMenuItem24.Size = New System.Drawing.Size(219, 22)
        Me.ToolStripMenuItem24.Text = "Suspend"
        '
        'ToolStripMenuItem25
        '
        Me.ToolStripMenuItem25.Image = Global.YAPM.My.Resources.Resources.control
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
        'ToolStripMenuItem43
        '
        Me.ToolStripMenuItem43.Name = "ToolStripMenuItem43"
        Me.ToolStripMenuItem43.Size = New System.Drawing.Size(216, 6)
        '
        'ChooseColumnsToolStripMenuItem4
        '
        Me.ChooseColumnsToolStripMenuItem4.Name = "ChooseColumnsToolStripMenuItem4"
        Me.ChooseColumnsToolStripMenuItem4.Size = New System.Drawing.Size(219, 22)
        Me.ChooseColumnsToolStripMenuItem4.Text = "Choose columns..."
        '
        'menuWindow
        '
        Me.menuWindow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem34, Me.ToolStripMenuItem7, Me.ChooseColumnsToolStripMenuItem2})
        Me.menuWindow.Name = "menuProc"
        Me.menuWindow.Size = New System.Drawing.Size(207, 54)
        '
        'ToolStripMenuItem34
        '
        Me.ToolStripMenuItem34.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem34.Name = "ToolStripMenuItem34"
        Me.ToolStripMenuItem34.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem34.Text = "&Select associated process"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(203, 6)
        '
        'ChooseColumnsToolStripMenuItem2
        '
        Me.ChooseColumnsToolStripMenuItem2.Name = "ChooseColumnsToolStripMenuItem2"
        Me.ChooseColumnsToolStripMenuItem2.Size = New System.Drawing.Size(206, 22)
        Me.ChooseColumnsToolStripMenuItem2.Text = "Choose columns..."
        '
        'menuModule
        '
        Me.menuModule.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem35, Me.ToolStripSeparator4, Me.ShowFileDetailsToolStripMenuItem, Me.ToolStripMenuItem36, Me.ToolStripMenuItem39, Me.GoogleSearchToolStripMenuItem2, Me.ToolStripMenuItem41, Me.ChooseColumnsToolStripMenuItem3})
        Me.menuModule.Name = "menuProc"
        Me.menuModule.Size = New System.Drawing.Size(207, 132)
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
        Me.ShowFileDetailsToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.magnifier
        Me.ShowFileDetailsToolStripMenuItem.Name = "ShowFileDetailsToolStripMenuItem"
        Me.ShowFileDetailsToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.ShowFileDetailsToolStripMenuItem.Text = "Show file details"
        '
        'ToolStripMenuItem36
        '
        Me.ToolStripMenuItem36.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem36.Image = Global.YAPM.My.Resources.Resources.cross
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
        Me.GoogleSearchToolStripMenuItem2.Image = Global.YAPM.My.Resources.Resources.globe
        Me.GoogleSearchToolStripMenuItem2.Name = "GoogleSearchToolStripMenuItem2"
        Me.GoogleSearchToolStripMenuItem2.Size = New System.Drawing.Size(206, 22)
        Me.GoogleSearchToolStripMenuItem2.Text = "Internet search"
        '
        'ToolStripMenuItem41
        '
        Me.ToolStripMenuItem41.Name = "ToolStripMenuItem41"
        Me.ToolStripMenuItem41.Size = New System.Drawing.Size(203, 6)
        '
        'ChooseColumnsToolStripMenuItem3
        '
        Me.ChooseColumnsToolStripMenuItem3.Name = "ChooseColumnsToolStripMenuItem3"
        Me.ChooseColumnsToolStripMenuItem3.Size = New System.Drawing.Size(206, 22)
        Me.ChooseColumnsToolStripMenuItem3.Text = "Choose columns..."
        '
        'menuTasks
        '
        Me.menuTasks.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowWindowToolStripMenuItem, Me.MaximizeWindowToolStripMenuItem, Me.MinimizeWindowToolStripMenuItem, Me.ToolStripMenuItem46, Me.EndTaskToolStripMenuItem, Me.ToolStripMenuItem47, Me.ToolStripMenuItem45, Me.SelectWindowInWindowsTabToolStripMenuItem, Me.ToolStripMenuItem6, Me.ChooseColumnsToolStripMenuItem1})
        Me.menuTasks.Name = "menuProc"
        Me.menuTasks.Size = New System.Drawing.Size(236, 176)
        '
        'ShowWindowToolStripMenuItem
        '
        Me.ShowWindowToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.EndTaskToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EndTaskToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.cross
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
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(232, 6)
        '
        'ChooseColumnsToolStripMenuItem1
        '
        Me.ChooseColumnsToolStripMenuItem1.Name = "ChooseColumnsToolStripMenuItem1"
        Me.ChooseColumnsToolStripMenuItem1.Size = New System.Drawing.Size(235, 22)
        Me.ChooseColumnsToolStripMenuItem1.Text = "Choose columns..."
        '
        'timerTask
        '
        Me.timerTask.Interval = 1000
        '
        'menuNetwork
        '
        Me.menuNetwork.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem48, Me.ToolStripMenuItem44, Me.ChooseColumnsToolStripMenuItem5})
        Me.menuNetwork.Name = "menuProc"
        Me.menuNetwork.Size = New System.Drawing.Size(207, 54)
        '
        'ToolStripMenuItem48
        '
        Me.ToolStripMenuItem48.Image = Global.YAPM.My.Resources.Resources.exe
        Me.ToolStripMenuItem48.Name = "ToolStripMenuItem48"
        Me.ToolStripMenuItem48.Size = New System.Drawing.Size(206, 22)
        Me.ToolStripMenuItem48.Text = "&Select associated process"
        '
        'ToolStripMenuItem44
        '
        Me.ToolStripMenuItem44.Name = "ToolStripMenuItem44"
        Me.ToolStripMenuItem44.Size = New System.Drawing.Size(203, 6)
        '
        'ChooseColumnsToolStripMenuItem5
        '
        Me.ChooseColumnsToolStripMenuItem5.Name = "ChooseColumnsToolStripMenuItem5"
        Me.ChooseColumnsToolStripMenuItem5.Size = New System.Drawing.Size(206, 22)
        Me.ChooseColumnsToolStripMenuItem5.Text = "Choose columns..."
        '
        'timerTrayIcon
        '
        Me.timerTrayIcon.Enabled = True
        Me.timerTrayIcon.Interval = 1000
        '
        'butProcessPermuteLvTv
        '
        Me.butProcessPermuteLvTv.AltKey = Nothing
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
        Me._main.Panel1.Controls.Add(Me.cmdTray)
        Me._main.Panel1.Controls.Add(Me.Ribbon)
        '
        '_main.Panel2
        '
        Me._main.Panel2.Controls.Add(Me.containerSystemMenu)
        Me._main.Size = New System.Drawing.Size(866, 594)
        Me._main.SplitterDistance = 113
        Me._main.TabIndex = 57
        '
        'cmdTray
        '
        Me.cmdTray.ContextMenuStrip = Me.mainMenu
        Me.cmdTray.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTray.Image = Global.YAPM.My.Resources.Resources.down
        Me.cmdTray.Location = New System.Drawing.Point(7, 2)
        Me.cmdTray.Name = "cmdTray"
        Me.cmdTray.Size = New System.Drawing.Size(38, 20)
        Me.cmdTray.TabIndex = 46
        Me.cmdTray.UseVisualStyleBackColor = True
        '
        'containerSystemMenu
        '
        Me.containerSystemMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerSystemMenu.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerSystemMenu.IsSplitterFixed = True
        Me.containerSystemMenu.Location = New System.Drawing.Point(0, 0)
        Me.containerSystemMenu.Name = "containerSystemMenu"
        Me.containerSystemMenu.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'containerSystemMenu.Panel1
        '
        Me.containerSystemMenu.Panel1.Controls.Add(Me.menuSystem)
        '
        'containerSystemMenu.Panel2
        '
        Me.containerSystemMenu.Panel2.Controls.Add(Me._tab)
        Me.containerSystemMenu.Size = New System.Drawing.Size(866, 477)
        Me.containerSystemMenu.SplitterDistance = 25
        Me.containerSystemMenu.TabIndex = 0
        '
        'menuSystem
        '
        Me.menuSystem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.SystemToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.menuSystem.Location = New System.Drawing.Point(0, 0)
        Me.menuSystem.Name = "menuSystem"
        Me.menuSystem.Size = New System.Drawing.Size(866, 24)
        Me.menuSystem.TabIndex = 61
        Me.menuSystem.Text = "MenuStrip1"
        Me.menuSystem.Visible = False
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem, Me.ToolStripMenuItem60, Me.ShowlogToolStripMenuItem1, Me.SystemreportToolStripMenuItem, Me.ToolStripMenuItem53, Me.SysteminfosToolStripMenuItem, Me.OpenedWindowsToolStripMenuItem, Me.ToolStripMenuItem54, Me.FindAWindowToolStripMenuItem1, Me.EmergencyHotkeysToolStripMenuItem1, Me.StateBasedActionsToolStripMenuItem1, Me.ToolStripMenuItem55, Me.MinimizeToTrayToolStripMenuItem1, Me.ExitToolStripMenuItem1})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.RefreshToolStripMenuItem.Text = "&Refresh"
        '
        'ToolStripMenuItem60
        '
        Me.ToolStripMenuItem60.Name = "ToolStripMenuItem60"
        Me.ToolStripMenuItem60.Size = New System.Drawing.Size(181, 6)
        '
        'ShowlogToolStripMenuItem1
        '
        Me.ShowlogToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.document_text
        Me.ShowlogToolStripMenuItem1.Name = "ShowlogToolStripMenuItem1"
        Me.ShowlogToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.ShowlogToolStripMenuItem1.Text = "Show &log..."
        '
        'SystemreportToolStripMenuItem
        '
        Me.SystemreportToolStripMenuItem.Name = "SystemreportToolStripMenuItem"
        Me.SystemreportToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.SystemreportToolStripMenuItem.Text = "Save system &report..."
        '
        'ToolStripMenuItem53
        '
        Me.ToolStripMenuItem53.Name = "ToolStripMenuItem53"
        Me.ToolStripMenuItem53.Size = New System.Drawing.Size(181, 6)
        '
        'SysteminfosToolStripMenuItem
        '
        Me.SysteminfosToolStripMenuItem.Image = CType(resources.GetObject("SysteminfosToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SysteminfosToolStripMenuItem.Name = "SysteminfosToolStripMenuItem"
        Me.SysteminfosToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.SysteminfosToolStripMenuItem.Text = "System &infos"
        '
        'OpenedWindowsToolStripMenuItem
        '
        Me.OpenedWindowsToolStripMenuItem.Image = CType(resources.GetObject("OpenedWindowsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenedWindowsToolStripMenuItem.Name = "OpenedWindowsToolStripMenuItem"
        Me.OpenedWindowsToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.OpenedWindowsToolStripMenuItem.Text = "&Opened windows"
        '
        'ToolStripMenuItem54
        '
        Me.ToolStripMenuItem54.Name = "ToolStripMenuItem54"
        Me.ToolStripMenuItem54.Size = New System.Drawing.Size(181, 6)
        '
        'FindAWindowToolStripMenuItem1
        '
        Me.FindAWindowToolStripMenuItem1.Image = CType(resources.GetObject("FindAWindowToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.FindAWindowToolStripMenuItem1.Name = "FindAWindowToolStripMenuItem1"
        Me.FindAWindowToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.FindAWindowToolStripMenuItem1.Text = "&Find a window"
        '
        'EmergencyHotkeysToolStripMenuItem1
        '
        Me.EmergencyHotkeysToolStripMenuItem1.Name = "EmergencyHotkeysToolStripMenuItem1"
        Me.EmergencyHotkeysToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.EmergencyHotkeysToolStripMenuItem1.Text = "Emergency &hotkeys"
        '
        'StateBasedActionsToolStripMenuItem1
        '
        Me.StateBasedActionsToolStripMenuItem1.Name = "StateBasedActionsToolStripMenuItem1"
        Me.StateBasedActionsToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.StateBasedActionsToolStripMenuItem1.Text = "State based actions..."
        '
        'ToolStripMenuItem55
        '
        Me.ToolStripMenuItem55.Name = "ToolStripMenuItem55"
        Me.ToolStripMenuItem55.Size = New System.Drawing.Size(181, 6)
        '
        'MinimizeToTrayToolStripMenuItem1
        '
        Me.MinimizeToTrayToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.down
        Me.MinimizeToTrayToolStripMenuItem1.Name = "MinimizeToTrayToolStripMenuItem1"
        Me.MinimizeToTrayToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.MinimizeToTrayToolStripMenuItem1.Text = "Minimize to tray"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Image = Global.YAPM.My.Resources.Resources.cross
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(184, 22)
        Me.ExitToolStripMenuItem1.Text = "&Exit"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AlwaysVisibleToolStripMenuItem1, Me.ToolStripMenuItem56, Me.RefreshprocessListToolStripMenuItem, Me.RefreshserviceListToolStripMenuItem1, Me.ToolStripMenuItem57, Me.OptionsToolStripMenuItem1})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'AlwaysVisibleToolStripMenuItem1
        '
        Me.AlwaysVisibleToolStripMenuItem1.Name = "AlwaysVisibleToolStripMenuItem1"
        Me.AlwaysVisibleToolStripMenuItem1.Size = New System.Drawing.Size(174, 22)
        Me.AlwaysVisibleToolStripMenuItem1.Text = "&Always visible"
        '
        'ToolStripMenuItem56
        '
        Me.ToolStripMenuItem56.Name = "ToolStripMenuItem56"
        Me.ToolStripMenuItem56.Size = New System.Drawing.Size(171, 6)
        '
        'RefreshprocessListToolStripMenuItem
        '
        Me.RefreshprocessListToolStripMenuItem.Checked = True
        Me.RefreshprocessListToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RefreshprocessListToolStripMenuItem.Name = "RefreshprocessListToolStripMenuItem"
        Me.RefreshprocessListToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RefreshprocessListToolStripMenuItem.Text = "Refresh &process list"
        '
        'RefreshserviceListToolStripMenuItem1
        '
        Me.RefreshserviceListToolStripMenuItem1.Checked = True
        Me.RefreshserviceListToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.RefreshserviceListToolStripMenuItem1.Name = "RefreshserviceListToolStripMenuItem1"
        Me.RefreshserviceListToolStripMenuItem1.Size = New System.Drawing.Size(174, 22)
        Me.RefreshserviceListToolStripMenuItem1.Text = "Refresh &service list"
        '
        'ToolStripMenuItem57
        '
        Me.ToolStripMenuItem57.Name = "ToolStripMenuItem57"
        Me.ToolStripMenuItem57.Size = New System.Drawing.Size(171, 6)
        '
        'OptionsToolStripMenuItem1
        '
        Me.OptionsToolStripMenuItem1.Image = CType(resources.GetObject("OptionsToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.OptionsToolStripMenuItem1.Name = "OptionsToolStripMenuItem1"
        Me.OptionsToolStripMenuItem1.Size = New System.Drawing.Size(174, 22)
        Me.OptionsToolStripMenuItem1.Text = "&Options..."
        '
        'SystemToolStripMenuItem
        '
        Me.SystemToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShutdownToolStripMenuItem3})
        Me.SystemToolStripMenuItem.Name = "SystemToolStripMenuItem"
        Me.SystemToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.SystemToolStripMenuItem.Text = "&System"
        '
        'ShutdownToolStripMenuItem3
        '
        Me.ShutdownToolStripMenuItem3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartToolStripMenuItem1, Me.ShutdownToolStripMenuItem4, Me.PoweroffToolStripMenuItem1, Me.ToolStripMenuItem58, Me.SleepToolStripMenuItem1, Me.HibernateToolStripMenuItem1, Me.ToolStripMenuItem59, Me.LogoffToolStripMenuItem1, Me.LockToolStripMenuItem1})
        Me.ShutdownToolStripMenuItem3.Name = "ShutdownToolStripMenuItem3"
        Me.ShutdownToolStripMenuItem3.Size = New System.Drawing.Size(128, 22)
        Me.ShutdownToolStripMenuItem3.Text = "&Shutdown"
        '
        'RestartToolStripMenuItem1
        '
        Me.RestartToolStripMenuItem1.Name = "RestartToolStripMenuItem1"
        Me.RestartToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.RestartToolStripMenuItem1.Text = "&Restart"
        '
        'ShutdownToolStripMenuItem4
        '
        Me.ShutdownToolStripMenuItem4.Name = "ShutdownToolStripMenuItem4"
        Me.ShutdownToolStripMenuItem4.Size = New System.Drawing.Size(128, 22)
        Me.ShutdownToolStripMenuItem4.Text = "Shut&down"
        '
        'PoweroffToolStripMenuItem1
        '
        Me.PoweroffToolStripMenuItem1.Name = "PoweroffToolStripMenuItem1"
        Me.PoweroffToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.PoweroffToolStripMenuItem1.Text = "&Poweroff"
        '
        'ToolStripMenuItem58
        '
        Me.ToolStripMenuItem58.Name = "ToolStripMenuItem58"
        Me.ToolStripMenuItem58.Size = New System.Drawing.Size(125, 6)
        '
        'SleepToolStripMenuItem1
        '
        Me.SleepToolStripMenuItem1.Name = "SleepToolStripMenuItem1"
        Me.SleepToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.SleepToolStripMenuItem1.Text = "&Sleep"
        '
        'HibernateToolStripMenuItem1
        '
        Me.HibernateToolStripMenuItem1.Name = "HibernateToolStripMenuItem1"
        Me.HibernateToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.HibernateToolStripMenuItem1.Text = "&Hibernate"
        '
        'ToolStripMenuItem59
        '
        Me.ToolStripMenuItem59.Name = "ToolStripMenuItem59"
        Me.ToolStripMenuItem59.Size = New System.Drawing.Size(125, 6)
        '
        'LogoffToolStripMenuItem1
        '
        Me.LogoffToolStripMenuItem1.Name = "LogoffToolStripMenuItem1"
        Me.LogoffToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.LogoffToolStripMenuItem1.Text = "Log&off"
        '
        'LockToolStripMenuItem1
        '
        Me.LockToolStripMenuItem1.Name = "LockToolStripMenuItem1"
        Me.LockToolStripMenuItem1.Size = New System.Drawing.Size(128, 22)
        Me.LockToolStripMenuItem1.Text = "&Lock"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckupdatesToolStripMenuItem, Me.ToolStripMenuItem85, Me.MakeAdonationToolStripMenuItem, Me.WebsiteToolStripMenuItem, Me.ProjectPageOnSourceforgenetToolStripMenuItem, Me.DownloadsToolStripMenuItem, Me.ToolStripMenuItem86, Me.HelpToolStripMenuItem1, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'CheckupdatesToolStripMenuItem
        '
        Me.CheckupdatesToolStripMenuItem.Image = CType(resources.GetObject("CheckupdatesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CheckupdatesToolStripMenuItem.Name = "CheckupdatesToolStripMenuItem"
        Me.CheckupdatesToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.CheckupdatesToolStripMenuItem.Text = "Check &updates..."
        '
        'ToolStripMenuItem85
        '
        Me.ToolStripMenuItem85.Name = "ToolStripMenuItem85"
        Me.ToolStripMenuItem85.Size = New System.Drawing.Size(238, 6)
        '
        'MakeAdonationToolStripMenuItem
        '
        Me.MakeAdonationToolStripMenuItem.Name = "MakeAdonationToolStripMenuItem"
        Me.MakeAdonationToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.MakeAdonationToolStripMenuItem.Text = "Make a &donation..."
        '
        'WebsiteToolStripMenuItem
        '
        Me.WebsiteToolStripMenuItem.Name = "WebsiteToolStripMenuItem"
        Me.WebsiteToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.WebsiteToolStripMenuItem.Text = "&Website"
        '
        'ProjectPageOnSourceforgenetToolStripMenuItem
        '
        Me.ProjectPageOnSourceforgenetToolStripMenuItem.Name = "ProjectPageOnSourceforgenetToolStripMenuItem"
        Me.ProjectPageOnSourceforgenetToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.ProjectPageOnSourceforgenetToolStripMenuItem.Text = "&Project page on Sourceforgenet"
        '
        'DownloadsToolStripMenuItem
        '
        Me.DownloadsToolStripMenuItem.Name = "DownloadsToolStripMenuItem"
        Me.DownloadsToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.DownloadsToolStripMenuItem.Text = "&Downloads"
        '
        'ToolStripMenuItem86
        '
        Me.ToolStripMenuItem86.Name = "ToolStripMenuItem86"
        Me.ToolStripMenuItem86.Size = New System.Drawing.Size(238, 6)
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpToolStripMenuItem1.Image = CType(resources.GetObject("HelpToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(241, 22)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.information_frame
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(241, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
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
        Me._tab.ImageList = Me.imgProcessTab
        Me._tab.Location = New System.Drawing.Point(0, 0)
        Me._tab.Name = "_tab"
        Me._tab.SelectedIndex = 0
        Me._tab.Size = New System.Drawing.Size(866, 448)
        Me._tab.TabIndex = 3
        '
        'pageTasks
        '
        Me.pageTasks.BackColor = System.Drawing.Color.Transparent
        Me.pageTasks.Controls.Add(Me.panelMain13)
        Me.pageTasks.ImageIndex = 5
        Me.pageTasks.Location = New System.Drawing.Point(4, 23)
        Me.pageTasks.Name = "pageTasks"
        Me.pageTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.pageTasks.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain13.Size = New System.Drawing.Size(852, 415)
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
        Me.SplitContainerTask.Size = New System.Drawing.Size(852, 415)
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
        Me.lvTask.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader62, Me.ColumnHeader63, Me.ColumnHeader64})
        Me.lvTask.ContextMenuStrip = Me.menuTasks
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
        Me.lvTask.Size = New System.Drawing.Size(852, 386)
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
        Me.pageProcesses.ImageIndex = 18
        Me.pageProcesses.Location = New System.Drawing.Point(4, 23)
        Me.pageProcesses.Name = "pageProcesses"
        Me.pageProcesses.Padding = New System.Windows.Forms.Padding(3)
        Me.pageProcesses.Size = New System.Drawing.Size(858, 421)
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
        Me.containerProcessPage.Size = New System.Drawing.Size(852, 415)
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
        Me.panelMain.Size = New System.Drawing.Size(852, 386)
        Me.panelMain.TabIndex = 4
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
        Me.SplitContainerProcess.Size = New System.Drawing.Size(852, 386)
        Me.SplitContainerProcess.SplitterDistance = 237
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
        Me.SplitContainerTvLv.Size = New System.Drawing.Size(852, 237)
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
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c7, Me.c8, Me.c9, Me.c10, Me.ColumnHeader20})
        Me.lvProcess.ContextMenuStrip = Me.menuProc
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.lvProcess.Size = New System.Drawing.Size(852, 237)
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
        Me.pageModules.ImageIndex = 11
        Me.pageModules.Location = New System.Drawing.Point(4, 23)
        Me.pageModules.Name = "pageModules"
        Me.pageModules.Padding = New System.Windows.Forms.Padding(3)
        Me.pageModules.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain11.Size = New System.Drawing.Size(852, 415)
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
        Me.splitModule.Size = New System.Drawing.Size(852, 415)
        Me.splitModule.SplitterDistance = 255
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
        Me.SplitContainerModules.Size = New System.Drawing.Size(852, 255)
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
        Me.lvModules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader29, Me.ColumnHeader43, Me.ColumnHeader44, Me.ColumnHeader45, Me.ColumnHeader46, Me.ColumnHeader18})
        Me.lvModules.ContextMenuStrip = Me.menuModule
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
        Me.lvModules.ProcessId = 0
        Me.lvModules.Size = New System.Drawing.Size(852, 226)
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
        Me.rtb6.Size = New System.Drawing.Size(852, 156)
        Me.rtb6.TabIndex = 8
        Me.rtb6.Text = "Click on an item to get additionnal informations"
        '
        'pageThreads
        '
        Me.pageThreads.BackColor = System.Drawing.Color.Transparent
        Me.pageThreads.Controls.Add(Me.panelMain9)
        Me.pageThreads.ImageIndex = 12
        Me.pageThreads.Location = New System.Drawing.Point(4, 23)
        Me.pageThreads.Name = "pageThreads"
        Me.pageThreads.Padding = New System.Windows.Forms.Padding(3)
        Me.pageThreads.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain9.Size = New System.Drawing.Size(852, 415)
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
        Me.splitThreads.Size = New System.Drawing.Size(852, 415)
        Me.splitThreads.SplitterDistance = 279
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
        Me.SplitContainerThreads.Size = New System.Drawing.Size(852, 279)
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
        Me.lvThreads.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader32, Me.ColumnHeader33, Me.ColumnHeader34, Me.ColumnHeader35, Me.ColumnHeader36, Me.ColumnHeader37, Me.ColumnHeader38})
        Me.lvThreads.ContextMenuStrip = Me.menuThread
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
        Me.lvThreads.ShowUnNamed = False
        Me.lvThreads.Size = New System.Drawing.Size(852, 250)
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
        Me.rtb4.Size = New System.Drawing.Size(852, 132)
        Me.rtb4.TabIndex = 7
        Me.rtb4.Text = "Click on a thread to get additionnal informations"
        '
        'pageHandles
        '
        Me.pageHandles.BackColor = System.Drawing.Color.Transparent
        Me.pageHandles.Controls.Add(Me.panelMain7)
        Me.pageHandles.ImageIndex = 13
        Me.pageHandles.Location = New System.Drawing.Point(4, 23)
        Me.pageHandles.Name = "pageHandles"
        Me.pageHandles.Padding = New System.Windows.Forms.Padding(3)
        Me.pageHandles.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain7.Size = New System.Drawing.Size(852, 415)
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
        Me.SplitContainerHandles.Size = New System.Drawing.Size(852, 415)
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
        Me.lvHandles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader24, Me.ColumnHeader25, Me.ColumnHeader26, Me.ColumnHeader27, Me.ColumnHeader28, Me.ColumnHeader15, Me.ColumnHeader16})
        Me.lvHandles.ContextMenuStrip = Me.menuHandles
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
        Me.lvHandles.ShowUnNamed = False
        Me.lvHandles.Size = New System.Drawing.Size(852, 386)
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
        Me.pageWindows.ImageIndex = 14
        Me.pageWindows.Location = New System.Drawing.Point(4, 23)
        Me.pageWindows.Name = "pageWindows"
        Me.pageWindows.Padding = New System.Windows.Forms.Padding(3)
        Me.pageWindows.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain10.Size = New System.Drawing.Size(852, 415)
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
        Me.splitContainerWindows.Size = New System.Drawing.Size(852, 415)
        Me.splitContainerWindows.SplitterDistance = 255
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
        Me.SplitContainerWindows2.Size = New System.Drawing.Size(852, 255)
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
        Me.lvWindows.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader30, Me.ColumnHeader31, Me.ColumnHeader39, Me.ColumnHeader40, Me.ColumnHeader41, Me.ColumnHeader42})
        Me.lvWindows.ContextMenuStrip = Me.menuWindow
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
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowUnNamed = False
        Me.lvWindows.Size = New System.Drawing.Size(852, 226)
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
        Me.rtb5.Size = New System.Drawing.Size(852, 156)
        Me.rtb5.TabIndex = 8
        Me.rtb5.Text = "Click on an item to get additionnal informations"
        '
        'pageMonitor
        '
        Me.pageMonitor.BackColor = System.Drawing.Color.Transparent
        Me.pageMonitor.Controls.Add(Me.panelMain8)
        Me.pageMonitor.ImageIndex = 16
        Me.pageMonitor.Location = New System.Drawing.Point(4, 23)
        Me.pageMonitor.Name = "pageMonitor"
        Me.pageMonitor.Padding = New System.Windows.Forms.Padding(3)
        Me.pageMonitor.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain8.Size = New System.Drawing.Size(852, 415)
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
        Me.splitMonitor.Size = New System.Drawing.Size(852, 415)
        Me.splitMonitor.SplitterDistance = 281
        Me.splitMonitor.TabIndex = 0
        '
        'tvMonitor
        '
        Me.tvMonitor.ContextMenuStrip = Me.menuMonitor
        Me.tvMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvMonitor.FullRowSelect = True
        Me.tvMonitor.ImageIndex = 0
        Me.tvMonitor.ImageList = Me.imgMonitor
        Me.tvMonitor.Location = New System.Drawing.Point(0, 0)
        Me.tvMonitor.Name = "tvMonitor"
        TreeNode3.ImageIndex = 1
        TreeNode3.Name = "processes"
        TreeNode3.SelectedImageIndex = 1
        TreeNode3.Text = "Processes"
        Me.tvMonitor.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode3})
        Me.tvMonitor.SelectedImageIndex = 0
        Me.tvMonitor.Size = New System.Drawing.Size(281, 415)
        Me.tvMonitor.TabIndex = 0
        '
        'menuMonitor
        '
        Me.menuMonitor.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem61, Me.ToolStripMenuItem62, Me.ToolStripMenuItem63, Me.menuMonitorStart, Me.menuMonitorStop})
        Me.menuMonitor.Name = "menuProc"
        Me.menuMonitor.Size = New System.Drawing.Size(168, 98)
        '
        'ToolStripMenuItem61
        '
        Me.ToolStripMenuItem61.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem61.Image = CType(resources.GetObject("ToolStripMenuItem61.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem61.Name = "ToolStripMenuItem61"
        Me.ToolStripMenuItem61.Size = New System.Drawing.Size(167, 22)
        Me.ToolStripMenuItem61.Text = "Add..."
        '
        'ToolStripMenuItem62
        '
        Me.ToolStripMenuItem62.Image = Global.YAPM.My.Resources.Resources.cross
        Me.ToolStripMenuItem62.Name = "ToolStripMenuItem62"
        Me.ToolStripMenuItem62.Size = New System.Drawing.Size(167, 22)
        Me.ToolStripMenuItem62.Text = "Remove selection"
        '
        'ToolStripMenuItem63
        '
        Me.ToolStripMenuItem63.Name = "ToolStripMenuItem63"
        Me.ToolStripMenuItem63.Size = New System.Drawing.Size(164, 6)
        '
        'menuMonitorStart
        '
        Me.menuMonitorStart.Enabled = False
        Me.menuMonitorStart.Image = Global.YAPM.My.Resources.Resources.control
        Me.menuMonitorStart.Name = "menuMonitorStart"
        Me.menuMonitorStart.Size = New System.Drawing.Size(167, 22)
        Me.menuMonitorStart.Text = "Start"
        '
        'menuMonitorStop
        '
        Me.menuMonitorStop.Enabled = False
        Me.menuMonitorStop.Image = Global.YAPM.My.Resources.Resources.control_stop_square
        Me.menuMonitorStop.Name = "menuMonitorStop"
        Me.menuMonitorStop.Size = New System.Drawing.Size(167, 22)
        Me.menuMonitorStop.Text = "Stop"
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
        Me.splitMonitor2.Size = New System.Drawing.Size(567, 415)
        Me.splitMonitor2.SplitterDistance = 171
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
        Me.txtMonitoringLog.Size = New System.Drawing.Size(567, 171)
        Me.txtMonitoringLog.TabIndex = 0
        Me.txtMonitoringLog.Text = "No process monitored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click on 'Add' button to monitor a process."
        '
        'lvMonitorReport
        '
        Me.lvMonitorReport.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader22, Me.ColumnHeader23, Me.ColumnHeader47, Me.ColumnHeader48, Me.ColumnHeader49})
        Me.lvMonitorReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMonitorReport.FullRowSelect = True
        Me.lvMonitorReport.Location = New System.Drawing.Point(0, 0)
        Me.lvMonitorReport.Name = "lvMonitorReport"
        Me.lvMonitorReport.OverriddenDoubleBuffered = False
        Me.lvMonitorReport.Size = New System.Drawing.Size(567, 171)
        Me.lvMonitorReport.TabIndex = 1
        Me.lvMonitorReport.UseCompatibleStateImageBehavior = False
        Me.lvMonitorReport.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Counter"
        Me.ColumnHeader22.Width = 200
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
        Me.splitMonitor3.Size = New System.Drawing.Size(567, 240)
        Me.splitMonitor3.SplitterDistance = 211
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
        Me.splitMonitor4.Size = New System.Drawing.Size(567, 211)
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
        Me.graphMonitor.Size = New System.Drawing.Size(567, 211)
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
        Me.pageServices.ImageIndex = 7
        Me.pageServices.Location = New System.Drawing.Point(4, 23)
        Me.pageServices.Name = "pageServices"
        Me.pageServices.Padding = New System.Windows.Forms.Padding(3)
        Me.pageServices.Size = New System.Drawing.Size(858, 421)
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
        Me.containerServicesPage.Size = New System.Drawing.Size(852, 415)
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
        Me.panelMain2.Size = New System.Drawing.Size(852, 386)
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
        Me.splitServices.Size = New System.Drawing.Size(852, 386)
        Me.splitServices.SplitterDistance = 242
        Me.splitServices.TabIndex = 0
        '
        'lvServices
        '
        Me.lvServices.AllowColumnReorder = True
        Me.lvServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader19})
        Me.lvServices.ContextMenuStrip = Me.menuService
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
        Me.lvServices.ShowAll = False
        Me.lvServices.Size = New System.Drawing.Size(852, 242)
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
        Me.splitServices2.Size = New System.Drawing.Size(852, 140)
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
        Me.lblServicePath.BackColor = System.Drawing.SystemColors.ButtonFace
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
        Me.splitServices3.Size = New System.Drawing.Size(852, 101)
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
        Me.rtb2.Size = New System.Drawing.Size(629, 101)
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
        Me.splitServices4.Size = New System.Drawing.Size(219, 101)
        Me.splitServices4.SplitterDistance = 40
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
        Me.tv2.Size = New System.Drawing.Size(219, 40)
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
        Me.tv.Size = New System.Drawing.Size(219, 57)
        Me.tv.TabIndex = 14
        '
        'pageNetwork
        '
        Me.pageNetwork.BackColor = System.Drawing.Color.Transparent
        Me.pageNetwork.Controls.Add(Me.panelMain14)
        Me.pageNetwork.ImageIndex = 8
        Me.pageNetwork.Location = New System.Drawing.Point(4, 23)
        Me.pageNetwork.Name = "pageNetwork"
        Me.pageNetwork.Padding = New System.Windows.Forms.Padding(3)
        Me.pageNetwork.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain14.Size = New System.Drawing.Size(852, 415)
        Me.panelMain14.TabIndex = 57
        '
        'lvNetwork
        '
        Me.lvNetwork.AllowColumnReorder = True
        Me.lvNetwork.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader66, Me.ColumnHeader67, Me.ColumnHeader68, Me.ColumnHeader69})
        Me.lvNetwork.ContextMenuStrip = Me.menuNetwork
        Me.lvNetwork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvNetwork.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvNetwork.FullRowSelect = True
        Me.lvNetwork.HideSelection = False
        Me.lvNetwork.Location = New System.Drawing.Point(0, 0)
        Me.lvNetwork.Name = "lvNetwork"
        Me.lvNetwork.OverriddenDoubleBuffered = True
        Me.lvNetwork.ProcessId = Nothing
        Me.lvNetwork.ShowAllPid = False
        Me.lvNetwork.Size = New System.Drawing.Size(852, 415)
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
        Me.pageFile.ImageIndex = 6
        Me.pageFile.Location = New System.Drawing.Point(4, 23)
        Me.pageFile.Name = "pageFile"
        Me.pageFile.Padding = New System.Windows.Forms.Padding(3)
        Me.pageFile.Size = New System.Drawing.Size(858, 421)
        Me.pageFile.TabIndex = 4
        Me.pageFile.Text = "File"
        Me.pageFile.UseVisualStyleBackColor = True
        '
        'panelMain5
        '
        Me.panelMain5.Controls.Add(Me.fileSplitContainer)
        Me.panelMain5.Controls.Add(Me.txtFile)
        Me.panelMain5.Controls.Add(Me.cmdFileClipboard)
        Me.panelMain5.Controls.Add(Me.pctFileSmall)
        Me.panelMain5.Controls.Add(Me.pctFileBig)
        Me.panelMain5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain5.Location = New System.Drawing.Point(3, 3)
        Me.panelMain5.Name = "panelMain5"
        Me.panelMain5.Size = New System.Drawing.Size(852, 415)
        Me.panelMain5.TabIndex = 48
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
        Me.cmdFileClipboard.Image = Global.YAPM.My.Resources.Resources.copy16
        Me.cmdFileClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdFileClipboard.Location = New System.Drawing.Point(360, 10)
        Me.cmdFileClipboard.Name = "cmdFileClipboard"
        Me.cmdFileClipboard.Size = New System.Drawing.Size(130, 24)
        Me.cmdFileClipboard.TabIndex = 12
        Me.cmdFileClipboard.Text = "Copy to clipboard"
        Me.cmdFileClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'pctFileBig
        '
        Me.pctFileBig.ContextMenuStrip = Me.mnuFileCopyPctBig
        Me.pctFileBig.Location = New System.Drawing.Point(525, 3)
        Me.pctFileBig.Name = "pctFileBig"
        Me.pctFileBig.Size = New System.Drawing.Size(32, 32)
        Me.pctFileBig.TabIndex = 8
        Me.pctFileBig.TabStop = False
        '
        'pageSearch
        '
        Me.pageSearch.BackColor = System.Drawing.Color.Transparent
        Me.pageSearch.Controls.Add(Me.panelMain6)
        Me.pageSearch.ImageIndex = 17
        Me.pageSearch.Location = New System.Drawing.Point(4, 23)
        Me.pageSearch.Name = "pageSearch"
        Me.pageSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pageSearch.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain6.Size = New System.Drawing.Size(852, 415)
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
        Me.SplitContainerSearch.Size = New System.Drawing.Size(852, 415)
        Me.SplitContainerSearch.SplitterDistance = 55
        Me.SplitContainerSearch.TabIndex = 2
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
        Me.chkSearchWindows.Location = New System.Drawing.Point(510, 7)
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
        Me.chkSearchHandles.Location = New System.Drawing.Point(441, 7)
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
        Me.lvSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader17})
        Me.lvSearchResults.ContextMenuStrip = Me.menuSearch
        Me.lvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSearchResults.FullRowSelect = True
        ListViewGroup15.Header = "Results"
        ListViewGroup15.Name = "gpResults"
        ListViewGroup16.Header = "Search results"
        ListViewGroup16.Name = "gpSearchResults"
        Me.lvSearchResults.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup15, ListViewGroup16})
        Me.lvSearchResults.HideSelection = False
        Me.lvSearchResults.Location = New System.Drawing.Point(0, 0)
        Me.lvSearchResults.Name = "lvSearchResults"
        Me.lvSearchResults.OverriddenDoubleBuffered = True
        Me.lvSearchResults.Size = New System.Drawing.Size(852, 356)
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
        'pageHelp
        '
        Me.pageHelp.BackColor = System.Drawing.Color.Transparent
        Me.pageHelp.Controls.Add(Me.panelMain4)
        Me.pageHelp.ImageIndex = 15
        Me.pageHelp.Location = New System.Drawing.Point(4, 23)
        Me.pageHelp.Name = "pageHelp"
        Me.pageHelp.Padding = New System.Windows.Forms.Padding(3)
        Me.pageHelp.Size = New System.Drawing.Size(858, 421)
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
        Me.panelMain4.Size = New System.Drawing.Size(852, 415)
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
        Me.WBHelp.Size = New System.Drawing.Size(852, 415)
        Me.WBHelp.TabIndex = 0
        Me.WBHelp.Url = New System.Uri("", System.UriKind.Relative)
        '
        'imgProcessTab
        '
        Me.imgProcessTab.ImageStream = CType(resources.GetObject("imgProcessTab.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcessTab.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcessTab.Images.SetKeyName(0, "exe")
        Me.imgProcessTab.Images.SetKeyName(1, "stats")
        Me.imgProcessTab.Images.SetKeyName(2, "monitor.png")
        Me.imgProcessTab.Images.SetKeyName(3, "locked")
        Me.imgProcessTab.Images.SetKeyName(4, "memory")
        Me.imgProcessTab.Images.SetKeyName(5, "exe2")
        Me.imgProcessTab.Images.SetKeyName(6, "document_text.png")
        Me.imgProcessTab.Images.SetKeyName(7, "service")
        Me.imgProcessTab.Images.SetKeyName(8, "network")
        Me.imgProcessTab.Images.SetKeyName(9, "font")
        Me.imgProcessTab.Images.SetKeyName(10, "environment")
        Me.imgProcessTab.Images.SetKeyName(11, "dll")
        Me.imgProcessTab.Images.SetKeyName(12, "thread")
        Me.imgProcessTab.Images.SetKeyName(13, "handle")
        Me.imgProcessTab.Images.SetKeyName(14, "windows")
        Me.imgProcessTab.Images.SetKeyName(15, "help16.png")
        Me.imgProcessTab.Images.SetKeyName(16, "icon2.gif")
        Me.imgProcessTab.Images.SetKeyName(17, "magnify16.gif")
        Me.imgProcessTab.Images.SetKeyName(18, "process16.gif")
        '
        'timerNetwork
        '
        Me.timerNetwork.Enabled = True
        Me.timerNetwork.Interval = 1000
        '
        'timerStateBasedActions
        '
        Me.timerStateBasedActions.Enabled = True
        Me.timerStateBasedActions.Interval = 1000
        '
        'ReduceWorkingSetSizeToolStripMenuItem
        '
        Me.ReduceWorkingSetSizeToolStripMenuItem.Name = "ReduceWorkingSetSizeToolStripMenuItem"
        Me.ReduceWorkingSetSizeToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ReduceWorkingSetSizeToolStripMenuItem.Text = "Reduce working set size"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ClientSize = New System.Drawing.Size(866, 594)
        Me.Controls.Add(Me._main)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(882, 589)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Yet Another Process Monitor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.menuProc.ResumeLayout(False)
        Me.menuService.ResumeLayout(False)
        Me.mainMenu.ResumeLayout(False)
        Me.mnuFileCopyPctSmall.ResumeLayout(False)
        Me.mnuFileCopyPctBig.ResumeLayout(False)
        Me.menuSearch.ResumeLayout(False)
        Me.menuHandles.ResumeLayout(False)
        Me.menuThread.ResumeLayout(False)
        Me.menuWindow.ResumeLayout(False)
        Me.menuModule.ResumeLayout(False)
        Me.menuTasks.ResumeLayout(False)
        Me.menuNetwork.ResumeLayout(False)
        Me._main.Panel1.ResumeLayout(False)
        Me._main.Panel2.ResumeLayout(False)
        Me._main.ResumeLayout(False)
        Me.containerSystemMenu.Panel1.ResumeLayout(False)
        Me.containerSystemMenu.Panel1.PerformLayout()
        Me.containerSystemMenu.Panel2.ResumeLayout(False)
        Me.containerSystemMenu.ResumeLayout(False)
        Me.menuSystem.ResumeLayout(False)
        Me.menuSystem.PerformLayout()
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
        Me.menuMonitor.ResumeLayout(False)
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
        Me.panelMain5.PerformLayout()
        Me.fileSplitContainer.Panel1.ResumeLayout(False)
        Me.fileSplitContainer.Panel2.ResumeLayout(False)
        Me.fileSplitContainer.ResumeLayout(False)
        Me.gpFileAttributes.ResumeLayout(False)
        Me.gpFileAttributes.PerformLayout()
        Me.gpFileDates.ResumeLayout(False)
        Me.gpFileDates.PerformLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageSearch.ResumeLayout(False)
        Me.panelMain6.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.PerformLayout()
        Me.SplitContainerSearch.Panel2.ResumeLayout(False)
        Me.SplitContainerSearch.ResumeLayout(False)
        Me.pageHelp.ResumeLayout(False)
        Me.panelMain4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timerProcess As System.Windows.Forms.Timer
    Friend WithEvents imgProcess As System.Windows.Forms.ImageList
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents timerServices As System.Windows.Forms.Timer
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
    Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
    Friend WithEvents saveDial As System.Windows.Forms.SaveFileDialog
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
    Friend WithEvents RBProcessOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butProcessGoogle As System.Windows.Forms.RibbonButton
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
    Friend WithEvents butFileGoogleSearch As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileProperties As System.Windows.Forms.RibbonButton
    Friend WithEvents butFileOpenDir As System.Windows.Forms.RibbonButton
    Friend WithEvents FileDetailsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butServiceFileDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents FileDetailsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents imgSearch As System.Windows.Forms.ImageList
    Friend WithEvents RBHandlesReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butHandlesSaveReport As System.Windows.Forms.RibbonButton
    Friend WithEvents menuSearch As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAssociatedProcessToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowHandlesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBFileOpenFile As System.Windows.Forms.RibbonPanel
    Friend WithEvents butOpenFile As System.Windows.Forms.RibbonButton
    Friend WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butFileRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents menuHandles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem22 As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents ShowThreadsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents menuWindow As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem34 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FolderChooser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ModulesTab As System.Windows.Forms.RibbonTab
    Friend WithEvents ShowModulesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butProcessShowModules As System.Windows.Forms.RibbonButton
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
    Friend WithEvents RBServiceReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butServiceReport As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessShowAll As System.Windows.Forms.RibbonButton
    Friend WithEvents ToolStripMenuItem38 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem37 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chooseColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBModuleOnline As System.Windows.Forms.RibbonPanel
    Friend WithEvents butModuleGoogle As System.Windows.Forms.RibbonButton
    Friend WithEvents ToolStripMenuItem39 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GoogleSearchToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBOptions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butPreferences As System.Windows.Forms.RibbonButton
    Friend WithEvents butAlwaysDisplay As System.Windows.Forms.RibbonButton
    Friend WithEvents butProcessDisplayDetails As System.Windows.Forms.RibbonButton
    Friend WithEvents mainMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MinimizeToTrayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem40 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnableServiceRefreshingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem42 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectedAssociatedProcessToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowSystemInformatoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents timerTask As System.Windows.Forms.Timer
    Friend WithEvents SelectWindowInWindowsTabToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KillProcessTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RBWindowCapture As System.Windows.Forms.RibbonPanel
    Friend WithEvents butWindowFind As System.Windows.Forms.RibbonButton
    Friend WithEvents NetworkTab As System.Windows.Forms.RibbonTab
    Friend WithEvents RBNetworkRefresh As System.Windows.Forms.RibbonPanel
    Friend WithEvents butNetworkRefresh As System.Windows.Forms.RibbonButton
    Friend WithEvents menuNetwork As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem48 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timerTrayIcon As System.Windows.Forms.Timer
    Friend WithEvents mnuFileCopyPctSmall As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFileCopyPctBig As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowManagementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmergencyHotkeysToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindAWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowYAPMToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutYAPMToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EnableProcessRefreshingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshServiceListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AlwaysVisibleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents butProcessPermuteLvTv As System.Windows.Forms.RibbonButton
    Friend WithEvents RibbonSeparator1 As System.Windows.Forms.RibbonSeparator
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DisplayUnnamedHandlesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem41 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem43 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem44 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem49 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChooseColumnsToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PoweroffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem51 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SleepToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HibernateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem52 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LogoffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LockToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem50 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents panelProcessReport As System.Windows.Forms.RibbonPanel
    Friend WithEvents butSaveProcessReport As System.Windows.Forms.RibbonButton
    Friend WithEvents SaveSystemReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _main As System.Windows.Forms.SplitContainer
    Friend WithEvents containerSystemMenu As System.Windows.Forms.SplitContainer
    Friend WithEvents menuSystem As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem60 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowlogToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemreportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem53 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SysteminfosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenedWindowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem54 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FindAWindowToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmergencyHotkeysToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem55 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlwaysVisibleToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem56 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RefreshprocessListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshserviceListToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem57 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OptionsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SystemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PoweroffToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem58 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SleepToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HibernateToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem59 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LogoffToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LockToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckupdatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem85 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MakeAdonationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WebsiteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProjectPageOnSourceforgenetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownloadsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem86 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents _tab As System.Windows.Forms.TabControl
    Friend WithEvents pageTasks As System.Windows.Forms.TabPage
    Friend WithEvents panelMain13 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerTask As System.Windows.Forms.SplitContainer
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblTaskCountResult As System.Windows.Forms.Label
    Friend WithEvents txtSearchTask As System.Windows.Forms.TextBox
    Friend WithEvents lvTask As YAPM.taskList
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
    Friend WithEvents lvProcess As YAPM.processList
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
    Friend WithEvents lvModules As YAPM.moduleList
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
    Friend WithEvents lvThreads As YAPM.threadList
    Friend WithEvents ColumnHeader32 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader33 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader34 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader35 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader36 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader38 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rtb4 As System.Windows.Forms.RichTextBox
    Friend WithEvents pageHandles As System.Windows.Forms.TabPage
    Friend WithEvents panelMain7 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerHandles As System.Windows.Forms.SplitContainer
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblHandlesCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchHandle As System.Windows.Forms.TextBox
    Friend WithEvents lvHandles As YAPM.handleList
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
    Friend WithEvents lvWindows As YAPM.windowList
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
    Friend WithEvents lvServices As YAPM.serviceList
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
    Friend WithEvents tv2 As System.Windows.Forms.TreeView
    Friend WithEvents tv As System.Windows.Forms.TreeView
    Friend WithEvents pageNetwork As System.Windows.Forms.TabPage
    Friend WithEvents panelMain14 As System.Windows.Forms.Panel
    Friend WithEvents lvNetwork As YAPM.networkList
    Friend WithEvents ColumnHeader66 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader67 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader68 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader69 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageFile As System.Windows.Forms.TabPage
    Friend WithEvents panelMain5 As System.Windows.Forms.Panel
    Friend WithEvents fileSplitContainer As System.Windows.Forms.SplitContainer
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
    Friend WithEvents lstFileString As System.Windows.Forms.ListBox
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdFileClipboard As System.Windows.Forms.Button
    Friend WithEvents pctFileSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pctFileBig As System.Windows.Forms.PictureBox
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
    Friend WithEvents lvSearchResults As YAPM.DoubleBufferedLV
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageHelp As System.Windows.Forms.TabPage
    Friend WithEvents panelMain4 As System.Windows.Forms.Panel
    Friend WithEvents WBHelp As System.Windows.Forms.WebBrowser
    Friend WithEvents cmdTray As System.Windows.Forms.Button
    Friend WithEvents HelpToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuMonitor As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem61 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem62 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem63 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuMonitorStart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuMonitorStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem64 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents imgProcessTab As System.Windows.Forms.ImageList
    Friend WithEvents MinimizeToTrayToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timerNetwork As System.Windows.Forms.Timer
    Friend WithEvents StateBasedActionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StateBasedActionsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timerStateBasedActions As System.Windows.Forms.Timer
    Friend WithEvents ReduceWorkingSetSizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
