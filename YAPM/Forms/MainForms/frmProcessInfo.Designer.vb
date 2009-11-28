<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessInfo
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

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim CConnection1 As cConnection = New cConnection
        Dim CConnection2 As cConnection = New cConnection
        Dim CConnection3 As cConnection = New cConnection
        Dim CConnection4 As cConnection = New cConnection
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Strings", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection5 As cConnection = New cConnection
        Dim CConnection6 As cConnection = New cConnection
        Dim CConnection7 As cConnection = New cConnection
        Dim CConnection8 As cConnection = New cConnection
        Dim CConnection9 As cConnection = New cConnection
        Dim CConnection10 As cConnection = New cConnection
        Dim CConnection11 As cConnection = New cConnection
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcessInfo))
        Me.timerProcPerf = New System.Windows.Forms.Timer(Me.components)
        Me.timerLog = New System.Windows.Forms.Timer(Me.components)
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItemRefresh = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyBig = New System.Windows.Forms.MenuItem
        Me.MenuItemCopySmall = New System.Windows.Forms.MenuItem
        Me.MenuItemPriEnable = New System.Windows.Forms.MenuItem
        Me.MenuItemViewMemory = New System.Windows.Forms.MenuItem
        Me.MenuItemCloseHandle = New System.Windows.Forms.MenuItem
        Me.menuCloseTCP = New System.Windows.Forms.MenuItem
        Me.MenuItemPriDisable = New System.Windows.Forms.MenuItem
        Me.MenuItemPriRemove = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleFileProp = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleOpenDir = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleFileDetails = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemModuleDependencies = New System.Windows.Forms.MenuItem
        Me.MenuItemUnloadModule = New System.Windows.Forms.MenuItem
        Me.MenuItemViewModuleMemory = New System.Windows.Forms.MenuItem
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
        Me.MenuItemWShow = New System.Windows.Forms.MenuItem
        Me.MenuItemWClose = New System.Windows.Forms.MenuItem
        Me.MenuItemWDisa = New System.Windows.Forms.MenuItem
        Me.MenuItemLogGoto = New System.Windows.Forms.MenuItem
        Me.menuViewMemory = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyPrivilege = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyMemory = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyModule = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyThread = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyWindow = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyHandle = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyNetwork = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyService = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyLog = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyEnvVariable = New System.Windows.Forms.MenuItem
        Me.menuCopyPctbig = New System.Windows.Forms.ContextMenu
        Me.menuCopyPctSmall = New System.Windows.Forms.ContextMenu
        Me.mnuString = New System.Windows.Forms.ContextMenu
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyString = New System.Windows.Forms.MenuItem
        Me.mnuPrivileges = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuProcMem = New System.Windows.Forms.ContextMenu
        Me.MenuItemMemoryDump = New System.Windows.Forms.MenuItem
        Me.MenuItemPEBAddress = New System.Windows.Forms.MenuItem
        Me.MenuItem13 = New System.Windows.Forms.MenuItem
        Me.MenuItemMemoryRelease = New System.Windows.Forms.MenuItem
        Me.MenuItemMemoryDecommit = New System.Windows.Forms.MenuItem
        Me.MenuItemMemoryChangeProtection = New System.Windows.Forms.MenuItem
        Me.MenuItem22 = New System.Windows.Forms.MenuItem
        Me.MenuItemColumnsMemory = New System.Windows.Forms.MenuItem
        Me.mnuModule = New System.Windows.Forms.ContextMenu
        Me.MenuItem16 = New System.Windows.Forms.MenuItem
        Me.MenuItem19 = New System.Windows.Forms.MenuItem
        Me.MenuItemColumnsModule = New System.Windows.Forms.MenuItem
        Me.mnuThread = New System.Windows.Forms.ContextMenu
        Me.MenuItem8 = New System.Windows.Forms.MenuItem
        Me.MenuItemThAffinity = New System.Windows.Forms.MenuItem
        Me.MenuItem15 = New System.Windows.Forms.MenuItem
        Me.MenuItemThColumns = New System.Windows.Forms.MenuItem
        Me.mnuWindow = New System.Windows.Forms.ContextMenu
        Me.MenuItemWShowUn = New System.Windows.Forms.MenuItem
        Me.MenuItemWHide = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MenuItemWVisiblity = New System.Windows.Forms.MenuItem
        Me.MenuItemWFront = New System.Windows.Forms.MenuItem
        Me.MenuItemWNotFront = New System.Windows.Forms.MenuItem
        Me.MenuItemWActive = New System.Windows.Forms.MenuItem
        Me.MenuItemWForeground = New System.Windows.Forms.MenuItem
        Me.MenuItem26 = New System.Windows.Forms.MenuItem
        Me.MenuItemWMin = New System.Windows.Forms.MenuItem
        Me.MenuItemWMax = New System.Windows.Forms.MenuItem
        Me.MenuItemWPosSize = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItemWFlash = New System.Windows.Forms.MenuItem
        Me.MenuItemWStopFlash = New System.Windows.Forms.MenuItem
        Me.MenuItem21 = New System.Windows.Forms.MenuItem
        Me.MenuItemWOpacity = New System.Windows.Forms.MenuItem
        Me.MenuItemWCaption = New System.Windows.Forms.MenuItem
        Me.MenuItem30 = New System.Windows.Forms.MenuItem
        Me.MenuItemWEna = New System.Windows.Forms.MenuItem
        Me.MenuItem33 = New System.Windows.Forms.MenuItem
        Me.MenuItemWColumns = New System.Windows.Forms.MenuItem
        Me.mnuHandle = New System.Windows.Forms.ContextMenu
        Me.MenuItemHandleDetails = New System.Windows.Forms.MenuItem
        Me.MenuItem12 = New System.Windows.Forms.MenuItem
        Me.MenuItemShowUnnamedHandles = New System.Windows.Forms.MenuItem
        Me.MenuItem14 = New System.Windows.Forms.MenuItem
        Me.MenuItemChooseColumnsHandle = New System.Windows.Forms.MenuItem
        Me.mnuNetwork = New System.Windows.Forms.ContextMenu
        Me.MenuItemNetworkTools = New System.Windows.Forms.MenuItem
        Me.MenuItemNetworkPing = New System.Windows.Forms.MenuItem
        Me.MenuItemNetworkRoute = New System.Windows.Forms.MenuItem
        Me.MenuItemNetworkWhoIs = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MenuItemColumnsNetwork = New System.Windows.Forms.MenuItem
        Me.mnuService = New System.Windows.Forms.ContextMenu
        Me.MenuItemServDetails = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItemServDepe = New System.Windows.Forms.MenuItem
        Me.MenuItem20 = New System.Windows.Forms.MenuItem
        Me.MenuItem17 = New System.Windows.Forms.MenuItem
        Me.MenuItemServDelete = New System.Windows.Forms.MenuItem
        Me.MenuItem25 = New System.Windows.Forms.MenuItem
        Me.MenuItemServReanalize = New System.Windows.Forms.MenuItem
        Me.MenuItem24 = New System.Windows.Forms.MenuItem
        Me.MenuItemServColumns = New System.Windows.Forms.MenuItem
        Me.mnuLog = New System.Windows.Forms.ContextMenu
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.mnuEnv = New System.Windows.Forms.ContextMenu
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItemCopyHeaps = New System.Windows.Forms.MenuItem
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.mnuHeaps = New System.Windows.Forms.ContextMenu
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.MenuItemColumnsHeap = New System.Windows.Forms.MenuItem
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.tabProcess = New System.Windows.Forms.TabControl
        Me.TabPageGeneral = New System.Windows.Forms.TabPage
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.cmdSet = New System.Windows.Forms.Button
        Me.cbPriority = New System.Windows.Forms.ComboBox
        Me.cmdAffinity = New System.Windows.Forms.Button
        Me.cmdPause = New System.Windows.Forms.Button
        Me.cmdResume = New System.Windows.Forms.Button
        Me.cmdKill = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.SplitContainerOnlineInfo = New System.Windows.Forms.SplitContainer
        Me.lblSecurityRisk = New System.Windows.Forms.Label
        Me.cmdGetOnlineInfos = New System.Windows.Forms.Button
        Me.rtbOnlineInfos = New System.Windows.Forms.RichTextBox
        Me.GroupBoxProcessInfos = New System.Windows.Forms.GroupBox
        Me.cmdGoProcess = New System.Windows.Forms.Button
        Me.txtRunTime = New System.Windows.Forms.TextBox
        Me.txtProcessStarted = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtParentProcess = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtPriority = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCommandLine = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtProcessUser = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtProcessId = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.gpProcGeneralFile = New System.Windows.Forms.GroupBox
        Me.cmdInspectExe = New System.Windows.Forms.Button
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
        Me.pctBigIcon = New System.Windows.Forms.PictureBox
        Me.TabPageStats = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblThreads = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.lblUserObjectsCount = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblGDIcount = New System.Windows.Forms.Label
        Me.lbl789 = New System.Windows.Forms.Label
        Me.lblHandles = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lblOthersBD = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblOtherD = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblWBD = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblWD = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblRBD = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblRD = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
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
        Me.lblAverageCPUusage = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblTotalTime = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.lblUserTime = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblKernelTime = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblPriority = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.TabPagePerf = New System.Windows.Forms.TabPage
        Me.splitPerformances = New System.Windows.Forms.SplitContainer
        Me.graphCPU = New Graph2
        Me.splitPerformance2 = New System.Windows.Forms.SplitContainer
        Me.graphMemory = New Graph2
        Me.graphIO = New Graph2
        Me.TabPageToken = New System.Windows.Forms.TabPage
        Me.tabProcessToken = New System.Windows.Forms.TabControl
        Me.tabProcessTokenPagePrivileges = New System.Windows.Forms.TabPage
        Me.lvPrivileges = New privilegeList
        Me.ColumnHeader50 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader51 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader52 = New System.Windows.Forms.ColumnHeader
        Me.TabPageMemory = New System.Windows.Forms.TabPage
        Me.lvProcMem = New memoryList
        Me.ColumnHeader53 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader54 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader55 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader56 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.TabPageInfos = New System.Windows.Forms.TabPage
        Me.SplitContainerInfoProcess = New System.Windows.Forms.SplitContainer
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.cmdInfosToClipB = New System.Windows.Forms.Button
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.TabPageServices = New System.Windows.Forms.TabPage
        Me.lvProcServices = New serviceList
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader19 = New System.Windows.Forms.ColumnHeader
        Me.TabPageNetwork = New System.Windows.Forms.TabPage
        Me.lvProcNetwork = New networkList
        Me.ColumnHeader49 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader57 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader58 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader59 = New System.Windows.Forms.ColumnHeader
        Me.TabPageString = New System.Windows.Forms.TabPage
        Me.SplitContainerStrings = New System.Windows.Forms.SplitContainer
        Me.lvProcString = New DoubleBufferedLV
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
        Me.TabPageEnv = New System.Windows.Forms.TabPage
        Me.lvProcEnv = New envVariableList
        Me.ColumnHeader60 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader61 = New System.Windows.Forms.ColumnHeader
        Me.TabPageModules = New System.Windows.Forms.TabPage
        Me.lvModules = New moduleList
        Me.ColumnHeader29 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader43 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader44 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader45 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader46 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.TabPageThreads = New System.Windows.Forms.TabPage
        Me.lvThreads = New threadList
        Me.ColumnHeader32 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader34 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader35 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader36 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader37 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader38 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.TabPageWindows = New System.Windows.Forms.TabPage
        Me.lvWindows = New windowList
        Me.ColumnHeader30 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader39 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader40 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader41 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader42 = New System.Windows.Forms.ColumnHeader
        Me.TabPageHandles = New System.Windows.Forms.TabPage
        Me.lvHandles = New handleList
        Me.ColumnHeader24 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader25 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader26 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader27 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader28 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader15 = New System.Windows.Forms.ColumnHeader
        Me.TabPageLog = New System.Windows.Forms.TabPage
        Me.SplitContainerLog = New System.Windows.Forms.SplitContainer
        Me.cmdLogOptions = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdClearLog = New System.Windows.Forms.Button
        Me.chkLog = New System.Windows.Forms.CheckBox
        Me.lvLog = New logList
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.TabPageHistory = New System.Windows.Forms.TabPage
        Me.containerHistory = New System.Windows.Forms.SplitContainer
        Me.Label2 = New System.Windows.Forms.Label
        Me.lstHistoryCat = New System.Windows.Forms.CheckedListBox
        Me.TabPageHeaps = New System.Windows.Forms.TabPage
        Me.cmdActivateHeapEnumeration = New System.Windows.Forms.Button
        Me.lvHeaps = New heapList
        Me.ColumnHeader16 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader18 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader20 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.cmdHideFindPanel = New System.Windows.Forms.Button
        Me.chkFreeze = New System.Windows.Forms.CheckBox
        Me.lblSearchItemCaption = New System.Windows.Forms.Label
        Me.lblResCount = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.tabProcess.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SplitContainerOnlineInfo.Panel1.SuspendLayout()
        Me.SplitContainerOnlineInfo.Panel2.SuspendLayout()
        Me.SplitContainerOnlineInfo.SuspendLayout()
        Me.GroupBoxProcessInfos.SuspendLayout()
        Me.gpProcGeneralFile.SuspendLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageStats.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPagePerf.SuspendLayout()
        Me.splitPerformances.Panel1.SuspendLayout()
        Me.splitPerformances.Panel2.SuspendLayout()
        Me.splitPerformances.SuspendLayout()
        CType(Me.graphCPU, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitPerformance2.Panel1.SuspendLayout()
        Me.splitPerformance2.Panel2.SuspendLayout()
        Me.splitPerformance2.SuspendLayout()
        CType(Me.graphMemory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.graphIO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageToken.SuspendLayout()
        Me.tabProcessToken.SuspendLayout()
        Me.tabProcessTokenPagePrivileges.SuspendLayout()
        Me.TabPageMemory.SuspendLayout()
        Me.TabPageInfos.SuspendLayout()
        Me.SplitContainerInfoProcess.Panel1.SuspendLayout()
        Me.SplitContainerInfoProcess.Panel2.SuspendLayout()
        Me.SplitContainerInfoProcess.SuspendLayout()
        Me.TabPageServices.SuspendLayout()
        Me.TabPageNetwork.SuspendLayout()
        Me.TabPageString.SuspendLayout()
        Me.SplitContainerStrings.Panel1.SuspendLayout()
        Me.SplitContainerStrings.Panel2.SuspendLayout()
        Me.SplitContainerStrings.SuspendLayout()
        Me.TabPageEnv.SuspendLayout()
        Me.TabPageModules.SuspendLayout()
        Me.TabPageThreads.SuspendLayout()
        Me.TabPageWindows.SuspendLayout()
        Me.TabPageHandles.SuspendLayout()
        Me.TabPageLog.SuspendLayout()
        Me.SplitContainerLog.Panel1.SuspendLayout()
        Me.SplitContainerLog.Panel2.SuspendLayout()
        Me.SplitContainerLog.SuspendLayout()
        Me.TabPageHistory.SuspendLayout()
        Me.containerHistory.Panel1.SuspendLayout()
        Me.containerHistory.SuspendLayout()
        Me.TabPageHeaps.SuspendLayout()
        Me.SuspendLayout()
        '
        'timerProcPerf
        '
        Me.timerProcPerf.Enabled = True
        Me.timerProcPerf.Interval = 1000
        '
        'timerLog
        '
        Me.timerLog.Interval = 1000
        '
        'mainMenu
        '
        Me.mainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemRefresh})
        Me.MenuItem3.Text = "main"
        Me.MenuItem3.Visible = False
        '
        'MenuItemRefresh
        '
        Me.MenuItemRefresh.DefaultItem = True
        Me.MenuItemRefresh.Index = 0
        Me.MenuItemRefresh.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.MenuItemRefresh.Text = "Refresh"
        '
        'MenuItemCopyBig
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyBig, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyBig.Index = 0
        Me.MenuItemCopyBig.Text = "Copy to clipboard"
        '
        'MenuItemCopySmall
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopySmall, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopySmall.Index = 0
        Me.MenuItemCopySmall.Text = "Copy to clipboard"
        '
        'MenuItemPriEnable
        '
        Me.VistaMenu.SetImage(Me.MenuItemPriEnable, Global.My.Resources.Resources.ok16)
        Me.MenuItemPriEnable.Index = 0
        Me.MenuItemPriEnable.Text = "Enable"
        '
        'MenuItemViewMemory
        '
        Me.MenuItemViewMemory.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemViewMemory, Global.My.Resources.Resources.magnifier)
        Me.MenuItemViewMemory.Index = 0
        Me.MenuItemViewMemory.Text = "View memory"
        '
        'MenuItemCloseHandle
        '
        Me.VistaMenu.SetImage(Me.MenuItemCloseHandle, Global.My.Resources.Resources.close)
        Me.MenuItemCloseHandle.Index = 1
        Me.MenuItemCloseHandle.Text = "Close item"
        '
        'menuCloseTCP
        '
        Me.menuCloseTCP.DefaultItem = True
        Me.VistaMenu.SetImage(Me.menuCloseTCP, Global.My.Resources.Resources.cross16)
        Me.menuCloseTCP.Index = 0
        Me.menuCloseTCP.Text = "Close TCP connection"
        '
        'MenuItemPriDisable
        '
        Me.VistaMenu.SetImage(Me.MenuItemPriDisable, Global.My.Resources.Resources.close)
        Me.MenuItemPriDisable.Index = 1
        Me.MenuItemPriDisable.Text = "Disable"
        '
        'MenuItemPriRemove
        '
        Me.VistaMenu.SetImage(Me.MenuItemPriRemove, Global.My.Resources.Resources.cross16)
        Me.MenuItemPriRemove.Index = 2
        Me.MenuItemPriRemove.Text = "Remove"
        '
        'MenuItemModuleFileProp
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleFileProp, Global.My.Resources.Resources.document_text)
        Me.MenuItemModuleFileProp.Index = 0
        Me.MenuItemModuleFileProp.Text = "File properties"
        '
        'MenuItemModuleOpenDir
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleOpenDir, Global.My.Resources.Resources.folder_open)
        Me.MenuItemModuleOpenDir.Index = 1
        Me.MenuItemModuleOpenDir.Text = "Open directory"
        '
        'MenuItemModuleFileDetails
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleFileDetails, Global.My.Resources.Resources.magnifier)
        Me.MenuItemModuleFileDetails.Index = 2
        Me.MenuItemModuleFileDetails.Text = "File details"
        '
        'MenuItemModuleSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleSearch, Global.My.Resources.Resources.globe)
        Me.MenuItemModuleSearch.Index = 3
        Me.MenuItemModuleSearch.Text = "Internet search"
        '
        'MenuItemModuleDependencies
        '
        Me.VistaMenu.SetImage(Me.MenuItemModuleDependencies, Global.My.Resources.Resources.dllIcon16)
        Me.MenuItemModuleDependencies.Index = 4
        Me.MenuItemModuleDependencies.Text = "View dependencies..."
        '
        'MenuItemUnloadModule
        '
        Me.MenuItemUnloadModule.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemUnloadModule, Global.My.Resources.Resources.cross16)
        Me.MenuItemUnloadModule.Index = 7
        Me.MenuItemUnloadModule.Text = "Unload module"
        '
        'MenuItemViewModuleMemory
        '
        Me.VistaMenu.SetImage(Me.MenuItemViewModuleMemory, Global.My.Resources.Resources.magnifier)
        Me.MenuItemViewModuleMemory.Index = 5
        Me.MenuItemViewModuleMemory.Text = "View memory"
        '
        'MenuItemServSelService
        '
        Me.VistaMenu.SetImage(Me.MenuItemServSelService, Global.My.Resources.Resources.exe16)
        Me.MenuItemServSelService.Index = 1
        Me.MenuItemServSelService.Text = "Select service"
        '
        'MenuItemServFileProp
        '
        Me.VistaMenu.SetImage(Me.MenuItemServFileProp, Global.My.Resources.Resources.document_text)
        Me.MenuItemServFileProp.Index = 3
        Me.MenuItemServFileProp.Text = "File properties"
        '
        'MenuItemServOpenDir
        '
        Me.VistaMenu.SetImage(Me.MenuItemServOpenDir, Global.My.Resources.Resources.folder_open)
        Me.MenuItemServOpenDir.Index = 4
        Me.MenuItemServOpenDir.Text = "Open directory"
        '
        'MenuItemServFileDetails
        '
        Me.VistaMenu.SetImage(Me.MenuItemServFileDetails, Global.My.Resources.Resources.magnifier)
        Me.MenuItemServFileDetails.Index = 5
        Me.MenuItemServFileDetails.Text = "File details"
        '
        'MenuItemServSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemServSearch, Global.My.Resources.Resources.globe)
        Me.MenuItemServSearch.Index = 6
        Me.MenuItemServSearch.Text = "Internet search"
        '
        'MenuItemServPause
        '
        Me.VistaMenu.SetImage(Me.MenuItemServPause, Global.My.Resources.Resources.control_pause)
        Me.MenuItemServPause.Index = 9
        Me.MenuItemServPause.Text = "Pause"
        '
        'MenuItemServStop
        '
        Me.VistaMenu.SetImage(Me.MenuItemServStop, Global.My.Resources.Resources.control_stop_square)
        Me.MenuItemServStop.Index = 10
        Me.MenuItemServStop.Text = "Stop"
        '
        'MenuItemServStart
        '
        Me.VistaMenu.SetImage(Me.MenuItemServStart, Global.My.Resources.Resources.control)
        Me.MenuItemServStart.Index = 11
        Me.MenuItemServStart.Text = "Start"
        '
        'MenuItemServAutoStart
        '
        Me.VistaMenu.SetImage(Me.MenuItemServAutoStart, Global.My.Resources.Resources.p6)
        Me.MenuItemServAutoStart.Index = 0
        Me.MenuItemServAutoStart.Text = "Auto start"
        '
        'MenuItemServOnDemand
        '
        Me.VistaMenu.SetImage(Me.MenuItemServOnDemand, Global.My.Resources.Resources.p3)
        Me.MenuItemServOnDemand.Index = 1
        Me.MenuItemServOnDemand.Text = "On demand"
        '
        'MenuItemServDisabled
        '
        Me.VistaMenu.SetImage(Me.MenuItemServDisabled, Global.My.Resources.Resources.p0)
        Me.MenuItemServDisabled.Index = 2
        Me.MenuItemServDisabled.Text = "Disabled"
        '
        'MenuItemThTerm
        '
        Me.MenuItemThTerm.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemThTerm, Global.My.Resources.Resources.cross16)
        Me.MenuItemThTerm.Index = 0
        Me.MenuItemThTerm.Text = "Terminate"
        '
        'MenuItemThSuspend
        '
        Me.VistaMenu.SetImage(Me.MenuItemThSuspend, Global.My.Resources.Resources.control_pause)
        Me.MenuItemThSuspend.Index = 1
        Me.MenuItemThSuspend.Text = "Suspend"
        '
        'MenuItemThResu
        '
        Me.VistaMenu.SetImage(Me.MenuItemThResu, Global.My.Resources.Resources.control)
        Me.MenuItemThResu.Index = 2
        Me.MenuItemThResu.Text = "Resume"
        '
        'MenuItemThIdle
        '
        Me.VistaMenu.SetImage(Me.MenuItemThIdle, Global.My.Resources.Resources.p0)
        Me.MenuItemThIdle.Index = 0
        Me.MenuItemThIdle.Text = "Idle"
        '
        'MenuItemThLowest
        '
        Me.VistaMenu.SetImage(Me.MenuItemThLowest, Global.My.Resources.Resources.p1)
        Me.MenuItemThLowest.Index = 1
        Me.MenuItemThLowest.Text = "Lowest"
        '
        'MenuItemThBNormal
        '
        Me.VistaMenu.SetImage(Me.MenuItemThBNormal, Global.My.Resources.Resources.p2)
        Me.MenuItemThBNormal.Index = 2
        Me.MenuItemThBNormal.Text = "Below normal"
        '
        'MenuItemThNorm
        '
        Me.VistaMenu.SetImage(Me.MenuItemThNorm, Global.My.Resources.Resources.p3)
        Me.MenuItemThNorm.Index = 3
        Me.MenuItemThNorm.Text = "Normal"
        '
        'MenuItemThANorm
        '
        Me.VistaMenu.SetImage(Me.MenuItemThANorm, Global.My.Resources.Resources.p4)
        Me.MenuItemThANorm.Index = 4
        Me.MenuItemThANorm.Text = "Above normal"
        '
        'MenuItemThHighest
        '
        Me.VistaMenu.SetImage(Me.MenuItemThHighest, Global.My.Resources.Resources.p5)
        Me.MenuItemThHighest.Index = 5
        Me.MenuItemThHighest.Text = "Highest"
        '
        'MenuItemThTimeCr
        '
        Me.VistaMenu.SetImage(Me.MenuItemThTimeCr, Global.My.Resources.Resources.p6)
        Me.MenuItemThTimeCr.Index = 6
        Me.MenuItemThTimeCr.Text = "Time critical"
        '
        'MenuItemWShow
        '
        Me.MenuItemWShow.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemWShow, Global.My.Resources.Resources.monitor16)
        Me.MenuItemWShow.Index = 0
        Me.MenuItemWShow.Text = "Show"
        '
        'MenuItemWClose
        '
        Me.VistaMenu.SetImage(Me.MenuItemWClose, Global.My.Resources.Resources.cross16)
        Me.MenuItemWClose.Index = 3
        Me.MenuItemWClose.Text = "Close"
        '
        'MenuItemWDisa
        '
        Me.MenuItemWDisa.Index = 8
        Me.MenuItemWDisa.Text = "Disable"
        '
        'MenuItemLogGoto
        '
        Me.VistaMenu.SetImage(Me.MenuItemLogGoto, Global.My.Resources.Resources.right16)
        Me.MenuItemLogGoto.Index = 0
        Me.MenuItemLogGoto.Text = "Go to item"
        '
        'menuViewMemory
        '
        Me.menuViewMemory.DefaultItem = True
        Me.VistaMenu.SetImage(Me.menuViewMemory, Global.My.Resources.Resources.magnifier)
        Me.menuViewMemory.Index = 0
        Me.menuViewMemory.Text = "View memory"
        '
        'MenuItemCopyPrivilege
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyPrivilege, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyPrivilege.Index = 4
        Me.MenuItemCopyPrivilege.Text = "Copy to clipboard"
        '
        'MenuItemCopyMemory
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyMemory, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyMemory.Index = 8
        Me.MenuItemCopyMemory.Text = "Copy to clipboard"
        '
        'MenuItemCopyModule
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyModule, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyModule.Index = 9
        Me.MenuItemCopyModule.Text = "Copy to clipboard"
        '
        'MenuItemCopyThread
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyThread, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyThread.Index = 6
        Me.MenuItemCopyThread.Text = "Copy to clipboard"
        '
        'MenuItemCopyWindow
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyWindow, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyWindow.Index = 10
        Me.MenuItemCopyWindow.Text = "Copy to clipboard"
        '
        'MenuItemCopyHandle
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyHandle, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyHandle.Index = 5
        Me.MenuItemCopyHandle.Text = "Copy to clipboard"
        '
        'MenuItemCopyNetwork
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyNetwork, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyNetwork.Index = 3
        Me.MenuItemCopyNetwork.Text = "Copy to clipboard"
        '
        'MenuItemCopyService
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyService, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyService.Index = 17
        Me.MenuItemCopyService.Text = "Copy to clipboard"
        '
        'MenuItemCopyLog
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyLog, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyLog.Index = 2
        Me.MenuItemCopyLog.Text = "Copy to clipboard"
        '
        'MenuItemCopyEnvVariable
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyEnvVariable, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyEnvVariable.Index = 0
        Me.MenuItemCopyEnvVariable.Text = "Copy to clipboard"
        '
        'menuCopyPctbig
        '
        Me.menuCopyPctbig.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyBig})
        '
        'menuCopyPctSmall
        '
        Me.menuCopyPctSmall.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopySmall})
        '
        'mnuString
        '
        Me.mnuString.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuViewMemory, Me.MenuItem2, Me.MenuItemCopyString})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'MenuItemCopyString
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyString, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyString.Index = 2
        Me.MenuItemCopyString.Text = "Copy to clipboard"
        '
        'mnuPrivileges
        '
        Me.mnuPrivileges.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemPriEnable, Me.MenuItemPriDisable, Me.MenuItemPriRemove, Me.MenuItem1, Me.MenuItemCopyPrivilege})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 3
        Me.MenuItem1.Text = "-"
        '
        'mnuProcMem
        '
        Me.mnuProcMem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemViewMemory, Me.MenuItemMemoryDump, Me.MenuItemPEBAddress, Me.MenuItem13, Me.MenuItemMemoryRelease, Me.MenuItemMemoryDecommit, Me.MenuItemMemoryChangeProtection, Me.MenuItem22, Me.MenuItemCopyMemory, Me.MenuItemColumnsMemory})
        '
        'MenuItemMemoryDump
        '
        Me.VistaMenu.SetImage(Me.MenuItemMemoryDump, Global.My.Resources.Resources.save16)
        Me.MenuItemMemoryDump.Index = 1
        Me.MenuItemMemoryDump.Text = "Dump..."
        '
        'MenuItemPEBAddress
        '
        Me.MenuItemPEBAddress.Index = 2
        Me.MenuItemPEBAddress.Text = "Jump to PEB address"
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 3
        Me.MenuItem13.Text = "-"
        '
        'MenuItemMemoryRelease
        '
        Me.VistaMenu.SetImage(Me.MenuItemMemoryRelease, Global.My.Resources.Resources.cross16)
        Me.MenuItemMemoryRelease.Index = 4
        Me.MenuItemMemoryRelease.Text = "Release"
        '
        'MenuItemMemoryDecommit
        '
        Me.VistaMenu.SetImage(Me.MenuItemMemoryDecommit, Global.My.Resources.Resources.close)
        Me.MenuItemMemoryDecommit.Index = 5
        Me.MenuItemMemoryDecommit.Text = "Decommit"
        '
        'MenuItemMemoryChangeProtection
        '
        Me.VistaMenu.SetImage(Me.MenuItemMemoryChangeProtection, Global.My.Resources.Resources.locked)
        Me.MenuItemMemoryChangeProtection.Index = 6
        Me.MenuItemMemoryChangeProtection.Text = "Change protection..."
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 7
        Me.MenuItem22.Text = "-"
        '
        'MenuItemColumnsMemory
        '
        Me.MenuItemColumnsMemory.Index = 9
        Me.MenuItemColumnsMemory.Text = "Choose columns..."
        '
        'mnuModule
        '
        Me.mnuModule.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemModuleFileProp, Me.MenuItemModuleOpenDir, Me.MenuItemModuleFileDetails, Me.MenuItemModuleSearch, Me.MenuItemModuleDependencies, Me.MenuItemViewModuleMemory, Me.MenuItem16, Me.MenuItemUnloadModule, Me.MenuItem19, Me.MenuItemCopyModule, Me.MenuItemColumnsModule})
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 6
        Me.MenuItem16.Text = "-"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 8
        Me.MenuItem19.Text = "-"
        '
        'MenuItemColumnsModule
        '
        Me.MenuItemColumnsModule.Index = 10
        Me.MenuItemColumnsModule.Text = "Choose columns..."
        '
        'mnuThread
        '
        Me.mnuThread.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemThTerm, Me.MenuItemThSuspend, Me.MenuItemThResu, Me.MenuItem8, Me.MenuItemThAffinity, Me.MenuItem15, Me.MenuItemCopyThread, Me.MenuItemThColumns})
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 3
        Me.MenuItem8.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemThIdle, Me.MenuItemThLowest, Me.MenuItemThBNormal, Me.MenuItemThNorm, Me.MenuItemThANorm, Me.MenuItemThHighest, Me.MenuItemThTimeCr})
        Me.MenuItem8.Text = "Priority"
        '
        'MenuItemThAffinity
        '
        Me.MenuItemThAffinity.Index = 4
        Me.MenuItemThAffinity.Text = "Set affinity..."
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 5
        Me.MenuItem15.Text = "-"
        '
        'MenuItemThColumns
        '
        Me.MenuItemThColumns.Index = 7
        Me.MenuItemThColumns.Text = "Choose columns..."
        '
        'mnuWindow
        '
        Me.mnuWindow.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemWShow, Me.MenuItemWShowUn, Me.MenuItemWHide, Me.MenuItemWClose, Me.MenuItem9, Me.MenuItemWVisiblity, Me.MenuItem30, Me.MenuItemWEna, Me.MenuItemWDisa, Me.MenuItem33, Me.MenuItemCopyWindow, Me.MenuItemWColumns})
        '
        'MenuItemWShowUn
        '
        Me.MenuItemWShowUn.Index = 1
        Me.MenuItemWShowUn.Text = "Show unnamed"
        '
        'MenuItemWHide
        '
        Me.MenuItemWHide.Index = 2
        Me.MenuItemWHide.Text = "Hide"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 4
        Me.MenuItem9.Text = "-"
        '
        'MenuItemWVisiblity
        '
        Me.MenuItemWVisiblity.Index = 5
        Me.MenuItemWVisiblity.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemWFront, Me.MenuItemWNotFront, Me.MenuItemWActive, Me.MenuItemWForeground, Me.MenuItem26, Me.MenuItemWMin, Me.MenuItemWMax, Me.MenuItemWPosSize, Me.MenuItem4, Me.MenuItemWFlash, Me.MenuItemWStopFlash, Me.MenuItem21, Me.MenuItemWOpacity, Me.MenuItemWCaption})
        Me.MenuItemWVisiblity.Text = "Visibility"
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
        'MenuItem26
        '
        Me.MenuItem26.Index = 4
        Me.MenuItem26.Text = "-"
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
        'MenuItemWPosSize
        '
        Me.MenuItemWPosSize.Index = 7
        Me.MenuItemWPosSize.Text = "Position && size"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 8
        Me.MenuItem4.Text = "-"
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
        'MenuItem21
        '
        Me.MenuItem21.Index = 11
        Me.MenuItem21.Text = "-"
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
        'MenuItem30
        '
        Me.MenuItem30.Index = 6
        Me.MenuItem30.Text = "-"
        '
        'MenuItemWEna
        '
        Me.MenuItemWEna.Index = 7
        Me.MenuItemWEna.Text = "Enable"
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = 9
        Me.MenuItem33.Text = "-"
        '
        'MenuItemWColumns
        '
        Me.MenuItemWColumns.Index = 11
        Me.MenuItemWColumns.Text = "Choose columns..."
        '
        'mnuHandle
        '
        Me.mnuHandle.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemHandleDetails, Me.MenuItemCloseHandle, Me.MenuItem12, Me.MenuItemShowUnnamedHandles, Me.MenuItem14, Me.MenuItemCopyHandle, Me.MenuItemChooseColumnsHandle})
        '
        'MenuItemHandleDetails
        '
        Me.MenuItemHandleDetails.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemHandleDetails, Global.My.Resources.Resources.monitor16)
        Me.MenuItemHandleDetails.Index = 0
        Me.MenuItemHandleDetails.Text = "Details..."
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 2
        Me.MenuItem12.Text = "-"
        '
        'MenuItemShowUnnamedHandles
        '
        Me.MenuItemShowUnnamedHandles.Index = 3
        Me.MenuItemShowUnnamedHandles.Text = "Show unnamed handles"
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 4
        Me.MenuItem14.Text = "-"
        '
        'MenuItemChooseColumnsHandle
        '
        Me.MenuItemChooseColumnsHandle.Index = 6
        Me.MenuItemChooseColumnsHandle.Text = "Choose columns..."
        '
        'mnuNetwork
        '
        Me.mnuNetwork.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuCloseTCP, Me.MenuItemNetworkTools, Me.MenuItem10, Me.MenuItemCopyNetwork, Me.MenuItemColumnsNetwork})
        '
        'MenuItemNetworkTools
        '
        Me.MenuItemNetworkTools.Index = 1
        Me.MenuItemNetworkTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemNetworkPing, Me.MenuItemNetworkRoute, Me.MenuItemNetworkWhoIs})
        Me.MenuItemNetworkTools.Text = "Tools"
        '
        'MenuItemNetworkPing
        '
        Me.MenuItemNetworkPing.Index = 0
        Me.MenuItemNetworkPing.Text = "Ping"
        '
        'MenuItemNetworkRoute
        '
        Me.MenuItemNetworkRoute.Index = 1
        Me.MenuItemNetworkRoute.Text = "TraceRoute"
        '
        'MenuItemNetworkWhoIs
        '
        Me.MenuItemNetworkWhoIs.Index = 2
        Me.MenuItemNetworkWhoIs.Text = "WhoIs"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 2
        Me.MenuItem10.Text = "-"
        '
        'MenuItemColumnsNetwork
        '
        Me.MenuItemColumnsNetwork.Index = 4
        Me.MenuItemColumnsNetwork.Text = "Choose columns..."
        '
        'mnuService
        '
        Me.mnuService.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServDetails, Me.MenuItemServSelService, Me.MenuItem5, Me.MenuItemServFileProp, Me.MenuItemServOpenDir, Me.MenuItemServFileDetails, Me.MenuItemServSearch, Me.MenuItemServDepe, Me.MenuItem20, Me.MenuItemServPause, Me.MenuItemServStop, Me.MenuItemServStart, Me.MenuItem17, Me.MenuItemServDelete, Me.MenuItem25, Me.MenuItemServReanalize, Me.MenuItem24, Me.MenuItemCopyService, Me.MenuItemServColumns})
        '
        'MenuItemServDetails
        '
        Me.MenuItemServDetails.DefaultItem = True
        Me.MenuItemServDetails.Index = 0
        Me.MenuItemServDetails.Text = "Service details"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 2
        Me.MenuItem5.Text = "-"
        '
        'MenuItemServDepe
        '
        Me.VistaMenu.SetImage(Me.MenuItemServDepe, Global.My.Resources.Resources.dllIcon16)
        Me.MenuItemServDepe.Index = 7
        Me.MenuItemServDepe.Text = "View dependencies..."
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 8
        Me.MenuItem20.Text = "-"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 12
        Me.MenuItem17.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServAutoStart, Me.MenuItemServOnDemand, Me.MenuItemServDisabled})
        Me.MenuItem17.Text = "Start type"
        '
        'MenuItemServDelete
        '
        Me.VistaMenu.SetImage(Me.MenuItemServDelete, Global.My.Resources.Resources.cross16)
        Me.MenuItemServDelete.Index = 13
        Me.MenuItemServDelete.Text = "Delete"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 14
        Me.MenuItem25.Text = "-"
        '
        'MenuItemServReanalize
        '
        Me.MenuItemServReanalize.Index = 15
        Me.MenuItemServReanalize.Text = "Reanalize"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 16
        Me.MenuItem24.Text = "-"
        '
        'MenuItemServColumns
        '
        Me.MenuItemServColumns.Index = 18
        Me.MenuItemServColumns.Text = "Choose columns..."
        '
        'mnuLog
        '
        Me.mnuLog.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemLogGoto, Me.MenuItem6, Me.MenuItemCopyLog})
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 1
        Me.MenuItem6.Text = "-"
        '
        'mnuEnv
        '
        Me.mnuEnv.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyEnvVariable})
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItemCopyHeaps
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyHeaps, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyHeaps.Index = 0
        Me.MenuItemCopyHeaps.Text = "Copy to clipboard"
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(0, 0)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(104, 24)
        Me.RadioButton1.TabIndex = 0
        '
        'mnuHeaps
        '
        Me.mnuHeaps.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyHeaps, Me.MenuItem11, Me.MenuItemColumnsHeap})
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 1
        Me.MenuItem11.Text = "-"
        '
        'MenuItemColumnsHeap
        '
        Me.MenuItemColumnsHeap.Index = 2
        Me.MenuItemColumnsHeap.Text = "Choose columns..."
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitContainer.IsSplitterFixed = True
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.tabProcess)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdHideFindPanel)
        Me.SplitContainer.Panel2.Controls.Add(Me.chkFreeze)
        Me.SplitContainer.Panel2.Controls.Add(Me.lblSearchItemCaption)
        Me.SplitContainer.Panel2.Controls.Add(Me.lblResCount)
        Me.SplitContainer.Panel2.Controls.Add(Me.txtSearch)
        Me.SplitContainer.Size = New System.Drawing.Size(655, 356)
        Me.SplitContainer.SplitterDistance = 326
        Me.SplitContainer.TabIndex = 1
        '
        'tabProcess
        '
        Me.tabProcess.Controls.Add(Me.TabPageGeneral)
        Me.tabProcess.Controls.Add(Me.TabPageStats)
        Me.tabProcess.Controls.Add(Me.TabPagePerf)
        Me.tabProcess.Controls.Add(Me.TabPageToken)
        Me.tabProcess.Controls.Add(Me.TabPageMemory)
        Me.tabProcess.Controls.Add(Me.TabPageInfos)
        Me.tabProcess.Controls.Add(Me.TabPageServices)
        Me.tabProcess.Controls.Add(Me.TabPageNetwork)
        Me.tabProcess.Controls.Add(Me.TabPageString)
        Me.tabProcess.Controls.Add(Me.TabPageEnv)
        Me.tabProcess.Controls.Add(Me.TabPageModules)
        Me.tabProcess.Controls.Add(Me.TabPageThreads)
        Me.tabProcess.Controls.Add(Me.TabPageWindows)
        Me.tabProcess.Controls.Add(Me.TabPageHandles)
        Me.tabProcess.Controls.Add(Me.TabPageLog)
        Me.tabProcess.Controls.Add(Me.TabPageHistory)
        Me.tabProcess.Controls.Add(Me.TabPageHeaps)
        Me.tabProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabProcess.Location = New System.Drawing.Point(0, 0)
        Me.tabProcess.Multiline = True
        Me.tabProcess.Name = "tabProcess"
        Me.tabProcess.SelectedIndex = 0
        Me.tabProcess.Size = New System.Drawing.Size(655, 326)
        Me.tabProcess.TabIndex = 0
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.Controls.Add(Me.GroupBox7)
        Me.TabPageGeneral.Controls.Add(Me.GroupBox6)
        Me.TabPageGeneral.Controls.Add(Me.GroupBoxProcessInfos)
        Me.TabPageGeneral.Controls.Add(Me.gpProcGeneralFile)
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 40)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGeneral.Size = New System.Drawing.Size(647, 282)
        Me.TabPageGeneral.TabIndex = 0
        Me.TabPageGeneral.Text = "General"
        Me.TabPageGeneral.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.cmdSet)
        Me.GroupBox7.Controls.Add(Me.cbPriority)
        Me.GroupBox7.Controls.Add(Me.cmdAffinity)
        Me.GroupBox7.Controls.Add(Me.cmdPause)
        Me.GroupBox7.Controls.Add(Me.cmdResume)
        Me.GroupBox7.Controls.Add(Me.cmdKill)
        Me.GroupBox7.Location = New System.Drawing.Point(391, 179)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(242, 92)
        Me.GroupBox7.TabIndex = 18
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Actions"
        '
        'cmdSet
        '
        Me.cmdSet.Location = New System.Drawing.Point(202, 50)
        Me.cmdSet.Name = "cmdSet"
        Me.cmdSet.Size = New System.Drawing.Size(31, 23)
        Me.cmdSet.TabIndex = 5
        Me.cmdSet.Text = "Set"
        Me.cmdSet.UseVisualStyleBackColor = True
        '
        'cbPriority
        '
        Me.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPriority.FormattingEnabled = True
        Me.cbPriority.Items.AddRange(New Object() {"Idle", "BelowNormal", "Normal", "AboveNormal", "High", "RealTime"})
        Me.cbPriority.Location = New System.Drawing.Point(91, 51)
        Me.cbPriority.Name = "cbPriority"
        Me.cbPriority.Size = New System.Drawing.Size(105, 21)
        Me.cbPriority.TabIndex = 4
        '
        'cmdAffinity
        '
        Me.cmdAffinity.Image = Global.My.Resources.Resources.gear
        Me.cmdAffinity.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAffinity.Location = New System.Drawing.Point(10, 50)
        Me.cmdAffinity.Name = "cmdAffinity"
        Me.cmdAffinity.Size = New System.Drawing.Size(75, 23)
        Me.cmdAffinity.TabIndex = 3
        Me.cmdAffinity.Text = "Affinity "
        Me.cmdAffinity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdAffinity.UseVisualStyleBackColor = True
        '
        'cmdPause
        '
        Me.cmdPause.Image = Global.My.Resources.Resources.control_pause
        Me.cmdPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPause.Location = New System.Drawing.Point(76, 21)
        Me.cmdPause.Name = "cmdPause"
        Me.cmdPause.Size = New System.Drawing.Size(75, 23)
        Me.cmdPause.TabIndex = 2
        Me.cmdPause.Text = "Pause   "
        Me.cmdPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPause.UseVisualStyleBackColor = True
        '
        'cmdResume
        '
        Me.cmdResume.Image = Global.My.Resources.Resources.control
        Me.cmdResume.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdResume.Location = New System.Drawing.Point(158, 21)
        Me.cmdResume.Name = "cmdResume"
        Me.cmdResume.Size = New System.Drawing.Size(75, 23)
        Me.cmdResume.TabIndex = 1
        Me.cmdResume.Text = "Resume "
        Me.cmdResume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdResume.UseVisualStyleBackColor = True
        '
        'cmdKill
        '
        Me.cmdKill.Image = Global.My.Resources.Resources.cross16
        Me.cmdKill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdKill.Location = New System.Drawing.Point(10, 21)
        Me.cmdKill.Name = "cmdKill"
        Me.cmdKill.Size = New System.Drawing.Size(60, 23)
        Me.cmdKill.TabIndex = 0
        Me.cmdKill.Text = "Kill   "
        Me.cmdKill.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdKill.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.SplitContainerOnlineInfo)
        Me.GroupBox6.Location = New System.Drawing.Point(391, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(242, 167)
        Me.GroupBox6.TabIndex = 17
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Online informations"
        '
        'SplitContainerOnlineInfo
        '
        Me.SplitContainerOnlineInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerOnlineInfo.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerOnlineInfo.IsSplitterFixed = True
        Me.SplitContainerOnlineInfo.Location = New System.Drawing.Point(3, 18)
        Me.SplitContainerOnlineInfo.Name = "SplitContainerOnlineInfo"
        Me.SplitContainerOnlineInfo.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerOnlineInfo.Panel1
        '
        Me.SplitContainerOnlineInfo.Panel1.Controls.Add(Me.lblSecurityRisk)
        Me.SplitContainerOnlineInfo.Panel1.Controls.Add(Me.cmdGetOnlineInfos)
        '
        'SplitContainerOnlineInfo.Panel2
        '
        Me.SplitContainerOnlineInfo.Panel2.Controls.Add(Me.rtbOnlineInfos)
        Me.SplitContainerOnlineInfo.Size = New System.Drawing.Size(236, 146)
        Me.SplitContainerOnlineInfo.SplitterDistance = 25
        Me.SplitContainerOnlineInfo.TabIndex = 16
        '
        'lblSecurityRisk
        '
        Me.lblSecurityRisk.AutoSize = True
        Me.lblSecurityRisk.Location = New System.Drawing.Point(124, 6)
        Me.lblSecurityRisk.Name = "lblSecurityRisk"
        Me.lblSecurityRisk.Size = New System.Drawing.Size(74, 13)
        Me.lblSecurityRisk.TabIndex = 2
        Me.lblSecurityRisk.Text = "Not yet rated"
        '
        'cmdGetOnlineInfos
        '
        Me.cmdGetOnlineInfos.Location = New System.Drawing.Point(10, 1)
        Me.cmdGetOnlineInfos.Name = "cmdGetOnlineInfos"
        Me.cmdGetOnlineInfos.Size = New System.Drawing.Size(108, 23)
        Me.cmdGetOnlineInfos.TabIndex = 11
        Me.cmdGetOnlineInfos.Text = "Get online infos"
        Me.cmdGetOnlineInfos.UseVisualStyleBackColor = True
        '
        'rtbOnlineInfos
        '
        Me.rtbOnlineInfos.AutoWordSelection = True
        Me.rtbOnlineInfos.BackColor = System.Drawing.Color.White
        Me.rtbOnlineInfos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbOnlineInfos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbOnlineInfos.HideSelection = False
        Me.rtbOnlineInfos.Location = New System.Drawing.Point(0, 0)
        Me.rtbOnlineInfos.Name = "rtbOnlineInfos"
        Me.rtbOnlineInfos.ReadOnly = True
        Me.rtbOnlineInfos.Size = New System.Drawing.Size(236, 117)
        Me.rtbOnlineInfos.TabIndex = 12
        Me.rtbOnlineInfos.Text = ""
        '
        'GroupBoxProcessInfos
        '
        Me.GroupBoxProcessInfos.Controls.Add(Me.cmdGoProcess)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtRunTime)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtProcessStarted)
        Me.GroupBoxProcessInfos.Controls.Add(Me.Label14)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtParentProcess)
        Me.GroupBoxProcessInfos.Controls.Add(Me.Label15)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtPriority)
        Me.GroupBoxProcessInfos.Controls.Add(Me.Label4)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtCommandLine)
        Me.GroupBoxProcessInfos.Controls.Add(Me.Label1)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtProcessUser)
        Me.GroupBoxProcessInfos.Controls.Add(Me.Label17)
        Me.GroupBoxProcessInfos.Controls.Add(Me.txtProcessId)
        Me.GroupBoxProcessInfos.Controls.Add(Me.Label16)
        Me.GroupBoxProcessInfos.Location = New System.Drawing.Point(6, 129)
        Me.GroupBoxProcessInfos.Name = "GroupBoxProcessInfos"
        Me.GroupBoxProcessInfos.Size = New System.Drawing.Size(376, 142)
        Me.GroupBoxProcessInfos.TabIndex = 16
        Me.GroupBoxProcessInfos.TabStop = False
        Me.GroupBoxProcessInfos.Text = "Process"
        '
        'cmdGoProcess
        '
        Me.cmdGoProcess.Enabled = False
        Me.cmdGoProcess.Image = Global.My.Resources.Resources.down16
        Me.cmdGoProcess.Location = New System.Drawing.Point(344, 43)
        Me.cmdGoProcess.Name = "cmdGoProcess"
        Me.cmdGoProcess.Size = New System.Drawing.Size(26, 22)
        Me.cmdGoProcess.TabIndex = 32
        Me.cmdGoProcess.UseVisualStyleBackColor = True
        '
        'txtRunTime
        '
        Me.txtRunTime.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtRunTime.Location = New System.Drawing.Point(292, 66)
        Me.txtRunTime.Name = "txtRunTime"
        Me.txtRunTime.ReadOnly = True
        Me.txtRunTime.Size = New System.Drawing.Size(79, 22)
        Me.txtRunTime.TabIndex = 31
        '
        'txtProcessStarted
        '
        Me.txtProcessStarted.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessStarted.Location = New System.Drawing.Point(89, 66)
        Me.txtProcessStarted.Name = "txtProcessStarted"
        Me.txtProcessStarted.ReadOnly = True
        Me.txtProcessStarted.Size = New System.Drawing.Size(197, 22)
        Me.txtProcessStarted.TabIndex = 28
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 13)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Start/run time"
        '
        'txtParentProcess
        '
        Me.txtParentProcess.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtParentProcess.Location = New System.Drawing.Point(89, 43)
        Me.txtParentProcess.Name = "txtParentProcess"
        Me.txtParentProcess.ReadOnly = True
        Me.txtParentProcess.Size = New System.Drawing.Size(249, 22)
        Me.txtParentProcess.TabIndex = 27
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 46)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Parent process"
        '
        'txtPriority
        '
        Me.txtPriority.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPriority.Location = New System.Drawing.Point(237, 20)
        Me.txtPriority.Name = "txtPriority"
        Me.txtPriority.ReadOnly = True
        Me.txtPriority.Size = New System.Drawing.Size(134, 22)
        Me.txtPriority.TabIndex = 25
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(188, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Priority"
        '
        'txtCommandLine
        '
        Me.txtCommandLine.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCommandLine.Location = New System.Drawing.Point(89, 114)
        Me.txtCommandLine.Name = "txtCommandLine"
        Me.txtCommandLine.ReadOnly = True
        Me.txtCommandLine.Size = New System.Drawing.Size(282, 22)
        Me.txtCommandLine.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 117)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Command line"
        '
        'txtProcessUser
        '
        Me.txtProcessUser.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessUser.Location = New System.Drawing.Point(89, 90)
        Me.txtProcessUser.Name = "txtProcessUser"
        Me.txtProcessUser.ReadOnly = True
        Me.txtProcessUser.Size = New System.Drawing.Size(282, 22)
        Me.txtProcessUser.TabIndex = 9
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(30, 13)
        Me.Label17.TabIndex = 21
        Me.Label17.Text = "User"
        '
        'txtProcessId
        '
        Me.txtProcessId.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessId.Location = New System.Drawing.Point(89, 19)
        Me.txtProcessId.Name = "txtProcessId"
        Me.txtProcessId.ReadOnly = True
        Me.txtProcessId.Size = New System.Drawing.Size(93, 22)
        Me.txtProcessId.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 22)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(23, 13)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "Pid"
        '
        'gpProcGeneralFile
        '
        Me.gpProcGeneralFile.Controls.Add(Me.cmdInspectExe)
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
        'cmdInspectExe
        '
        Me.cmdInspectExe.Image = Global.My.Resources.Resources.dllIcon16
        Me.cmdInspectExe.Location = New System.Drawing.Point(266, 81)
        Me.cmdInspectExe.Name = "cmdInspectExe"
        Me.cmdInspectExe.Size = New System.Drawing.Size(26, 26)
        Me.cmdInspectExe.TabIndex = 18
        Me.cmdInspectExe.UseVisualStyleBackColor = True
        '
        'cmdShowFileDetails
        '
        Me.cmdShowFileDetails.Image = Global.My.Resources.Resources.magnifier
        Me.cmdShowFileDetails.Location = New System.Drawing.Point(292, 81)
        Me.cmdShowFileDetails.Name = "cmdShowFileDetails"
        Me.cmdShowFileDetails.Size = New System.Drawing.Size(26, 26)
        Me.cmdShowFileDetails.TabIndex = 3
        Me.cmdShowFileDetails.UseVisualStyleBackColor = True
        '
        'cmdShowFileProperties
        '
        Me.cmdShowFileProperties.Image = Global.My.Resources.Resources.document_text
        Me.cmdShowFileProperties.Location = New System.Drawing.Point(318, 81)
        Me.cmdShowFileProperties.Name = "cmdShowFileProperties"
        Me.cmdShowFileProperties.Size = New System.Drawing.Size(26, 26)
        Me.cmdShowFileProperties.TabIndex = 4
        Me.cmdShowFileProperties.UseVisualStyleBackColor = True
        '
        'cmdOpenDirectory
        '
        Me.cmdOpenDirectory.Image = Global.My.Resources.Resources.folder_open_document
        Me.cmdOpenDirectory.Location = New System.Drawing.Point(344, 81)
        Me.cmdOpenDirectory.Name = "cmdOpenDirectory"
        Me.cmdOpenDirectory.Size = New System.Drawing.Size(26, 26)
        Me.cmdOpenDirectory.TabIndex = 5
        Me.cmdOpenDirectory.UseVisualStyleBackColor = True
        '
        'txtProcessPath
        '
        Me.txtProcessPath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcessPath.Location = New System.Drawing.Point(85, 82)
        Me.txtProcessPath.Name = "txtProcessPath"
        Me.txtProcessPath.ReadOnly = True
        Me.txtProcessPath.Size = New System.Drawing.Size(175, 22)
        Me.txtProcessPath.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(5, 85)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(65, 13)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "Image path"
        '
        'txtImageVersion
        '
        Me.txtImageVersion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtImageVersion.Location = New System.Drawing.Point(85, 59)
        Me.txtImageVersion.Name = "txtImageVersion"
        Me.txtImageVersion.ReadOnly = True
        Me.txtImageVersion.Size = New System.Drawing.Size(285, 22)
        Me.txtImageVersion.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 62)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 13)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Image version"
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Location = New System.Drawing.Point(82, 38)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(77, 13)
        Me.lblCopyright.TabIndex = 14
        Me.lblCopyright.Text = "File copyright"
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(82, 19)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(86, 13)
        Me.lblDescription.TabIndex = 13
        Me.lblDescription.Text = "File description"
        '
        'pctSmallIcon
        '
        Me.pctSmallIcon.Location = New System.Drawing.Point(44, 35)
        Me.pctSmallIcon.Name = "pctSmallIcon"
        Me.pctSmallIcon.Size = New System.Drawing.Size(16, 16)
        Me.pctSmallIcon.TabIndex = 12
        Me.pctSmallIcon.TabStop = False
        '
        'pctBigIcon
        '
        Me.pctBigIcon.Location = New System.Drawing.Point(6, 19)
        Me.pctBigIcon.Name = "pctBigIcon"
        Me.pctBigIcon.Size = New System.Drawing.Size(32, 32)
        Me.pctBigIcon.TabIndex = 11
        Me.pctBigIcon.TabStop = False
        '
        'TabPageStats
        '
        Me.TabPageStats.Controls.Add(Me.GroupBox5)
        Me.TabPageStats.Controls.Add(Me.GroupBox4)
        Me.TabPageStats.Controls.Add(Me.GroupBox3)
        Me.TabPageStats.Controls.Add(Me.GroupBox2)
        Me.TabPageStats.Location = New System.Drawing.Point(4, 40)
        Me.TabPageStats.Name = "TabPageStats"
        Me.TabPageStats.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageStats.Size = New System.Drawing.Size(647, 282)
        Me.TabPageStats.TabIndex = 1
        Me.TabPageStats.Text = "Statistics"
        Me.TabPageStats.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblThreads)
        Me.GroupBox5.Controls.Add(Me.Label44)
        Me.GroupBox5.Controls.Add(Me.lblUserObjectsCount)
        Me.GroupBox5.Controls.Add(Me.Label37)
        Me.GroupBox5.Controls.Add(Me.lblGDIcount)
        Me.GroupBox5.Controls.Add(Me.lbl789)
        Me.GroupBox5.Controls.Add(Me.lblHandles)
        Me.GroupBox5.Controls.Add(Me.Label53)
        Me.GroupBox5.Location = New System.Drawing.Point(416, 135)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(180, 122)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Other"
        '
        'lblThreads
        '
        Me.lblThreads.AutoSize = True
        Me.lblThreads.Location = New System.Drawing.Point(102, 72)
        Me.lblThreads.Name = "lblThreads"
        Me.lblThreads.Size = New System.Drawing.Size(19, 13)
        Me.lblThreads.TabIndex = 7
        Me.lblThreads.Text = "00"
        Me.lblThreads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(8, 72)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(47, 13)
        Me.Label44.TabIndex = 6
        Me.Label44.Text = "Threads"
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
        Me.Label37.Size = New System.Drawing.Size(70, 13)
        Me.Label37.TabIndex = 4
        Me.Label37.Text = "User objects"
        '
        'lblGDIcount
        '
        Me.lblGDIcount.AutoSize = True
        Me.lblGDIcount.Location = New System.Drawing.Point(102, 35)
        Me.lblGDIcount.Name = "lblGDIcount"
        Me.lblGDIcount.Size = New System.Drawing.Size(19, 13)
        Me.lblGDIcount.TabIndex = 3
        Me.lblGDIcount.Text = "00"
        Me.lblGDIcount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl789
        '
        Me.lbl789.AutoSize = True
        Me.lbl789.Location = New System.Drawing.Point(8, 35)
        Me.lbl789.Name = "lbl789"
        Me.lbl789.Size = New System.Drawing.Size(66, 13)
        Me.lbl789.TabIndex = 2
        Me.lbl789.Text = "GDI objects"
        '
        'lblHandles
        '
        Me.lblHandles.AutoSize = True
        Me.lblHandles.Location = New System.Drawing.Point(102, 16)
        Me.lblHandles.Name = "lblHandles"
        Me.lblHandles.Size = New System.Drawing.Size(19, 13)
        Me.lblHandles.TabIndex = 1
        Me.lblHandles.Text = "00"
        Me.lblHandles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(8, 16)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(49, 13)
        Me.Label53.TabIndex = 0
        Me.Label53.Text = "Handles"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblOthersBD)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.lblOtherD)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.lblWBD)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.lblWD)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.lblRBD)
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.lblRD)
        Me.GroupBox4.Controls.Add(Me.Label41)
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
        Me.GroupBox4.Size = New System.Drawing.Size(176, 249)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "I/O"
        '
        'lblOthersBD
        '
        Me.lblOthersBD.AutoSize = True
        Me.lblOthersBD.Location = New System.Drawing.Point(102, 217)
        Me.lblOthersBD.Name = "lblOthersBD"
        Me.lblOthersBD.Size = New System.Drawing.Size(19, 13)
        Me.lblOthersBD.TabIndex = 43
        Me.lblOthersBD.Text = "00"
        Me.lblOthersBD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 217)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 13)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "Other bytes delta"
        '
        'lblOtherD
        '
        Me.lblOtherD.AutoSize = True
        Me.lblOtherD.Location = New System.Drawing.Point(102, 199)
        Me.lblOtherD.Name = "lblOtherD"
        Me.lblOtherD.Size = New System.Drawing.Size(19, 13)
        Me.lblOtherD.TabIndex = 41
        Me.lblOtherD.Text = "00"
        Me.lblOtherD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 199)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Other delta"
        '
        'lblWBD
        '
        Me.lblWBD.AutoSize = True
        Me.lblWBD.Location = New System.Drawing.Point(102, 180)
        Me.lblWBD.Name = "lblWBD"
        Me.lblWBD.Size = New System.Drawing.Size(19, 13)
        Me.lblWBD.TabIndex = 39
        Me.lblWBD.Text = "00"
        Me.lblWBD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 180)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Write bytes delta"
        '
        'lblWD
        '
        Me.lblWD.AutoSize = True
        Me.lblWD.Location = New System.Drawing.Point(102, 162)
        Me.lblWD.Name = "lblWD"
        Me.lblWD.Size = New System.Drawing.Size(19, 13)
        Me.lblWD.TabIndex = 37
        Me.lblWD.Text = "00"
        Me.lblWD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 162)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 13)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "Writes delta"
        '
        'lblRBD
        '
        Me.lblRBD.AutoSize = True
        Me.lblRBD.Location = New System.Drawing.Point(102, 144)
        Me.lblRBD.Name = "lblRBD"
        Me.lblRBD.Size = New System.Drawing.Size(19, 13)
        Me.lblRBD.TabIndex = 35
        Me.lblRBD.Text = "00"
        Me.lblRBD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 144)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(92, 13)
        Me.Label34.TabIndex = 34
        Me.Label34.Text = "Read bytes delta"
        '
        'lblRD
        '
        Me.lblRD.AutoSize = True
        Me.lblRD.Location = New System.Drawing.Point(102, 126)
        Me.lblRD.Name = "lblRD"
        Me.lblRD.Size = New System.Drawing.Size(19, 13)
        Me.lblRD.TabIndex = 33
        Me.lblRD.Text = "00"
        Me.lblRD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(6, 126)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(67, 13)
        Me.Label41.TabIndex = 32
        Me.Label41.Text = "Reads delta"
        '
        'lblProcOtherBytes
        '
        Me.lblProcOtherBytes.AutoSize = True
        Me.lblProcOtherBytes.Location = New System.Drawing.Point(102, 107)
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
        Me.Label23.Size = New System.Drawing.Size(67, 13)
        Me.Label23.TabIndex = 30
        Me.Label23.Text = "Other bytes"
        '
        'lblProcOther
        '
        Me.lblProcOther.AutoSize = True
        Me.lblProcOther.Location = New System.Drawing.Point(102, 89)
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
        Me.Label30.Size = New System.Drawing.Size(37, 13)
        Me.Label30.TabIndex = 28
        Me.Label30.Text = "Other"
        '
        'lblProcWriteBytes
        '
        Me.lblProcWriteBytes.AutoSize = True
        Me.lblProcWriteBytes.Location = New System.Drawing.Point(102, 70)
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
        Me.Label36.Size = New System.Drawing.Size(65, 13)
        Me.Label36.TabIndex = 26
        Me.Label36.Text = "Write bytes"
        '
        'lblProcWrites
        '
        Me.lblProcWrites.AutoSize = True
        Me.lblProcWrites.Location = New System.Drawing.Point(102, 52)
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
        Me.Label38.Size = New System.Drawing.Size(40, 13)
        Me.Label38.TabIndex = 24
        Me.Label38.Text = "Writes"
        '
        'lblProcReadBytes
        '
        Me.lblProcReadBytes.AutoSize = True
        Me.lblProcReadBytes.Location = New System.Drawing.Point(102, 34)
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
        Me.Label40.Size = New System.Drawing.Size(63, 13)
        Me.Label40.TabIndex = 22
        Me.Label40.Text = "Read bytes"
        '
        'lblProcReads
        '
        Me.lblProcReads.AutoSize = True
        Me.lblProcReads.Location = New System.Drawing.Point(102, 16)
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
        Me.GroupBox3.Size = New System.Drawing.Size(222, 249)
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
        Me.Label21.Size = New System.Drawing.Size(117, 13)
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
        Me.Label26.Size = New System.Drawing.Size(141, 13)
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
        Me.Label29.Size = New System.Drawing.Size(95, 13)
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
        Me.Label32.Size = New System.Drawing.Size(119, 13)
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
        Me.Label31.Size = New System.Drawing.Size(64, 13)
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
        Me.Label33.Size = New System.Drawing.Size(113, 13)
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
        Me.Label35.Size = New System.Drawing.Size(82, 13)
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
        Me.Label25.Size = New System.Drawing.Size(95, 13)
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
        Me.Label27.Size = New System.Drawing.Size(70, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Working set"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblAverageCPUusage)
        Me.GroupBox2.Controls.Add(Me.Label3)
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
        Me.GroupBox2.Size = New System.Drawing.Size(180, 120)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "CPU"
        '
        'lblAverageCPUusage
        '
        Me.lblAverageCPUusage.AutoSize = True
        Me.lblAverageCPUusage.Location = New System.Drawing.Point(100, 91)
        Me.lblAverageCPUusage.Name = "lblAverageCPUusage"
        Me.lblAverageCPUusage.Size = New System.Drawing.Size(19, 13)
        Me.lblAverageCPUusage.TabIndex = 9
        Me.lblAverageCPUusage.Text = "00"
        Me.lblAverageCPUusage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Average usage"
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
        Me.Label24.Size = New System.Drawing.Size(57, 13)
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
        Me.Label22.Size = New System.Drawing.Size(55, 13)
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
        Me.Label20.Size = New System.Drawing.Size(64, 13)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Kernel time"
        '
        'lblPriority
        '
        Me.lblPriority.AutoSize = True
        Me.lblPriority.Location = New System.Drawing.Point(100, 18)
        Me.lblPriority.Name = "lblPriority"
        Me.lblPriority.Size = New System.Drawing.Size(19, 13)
        Me.lblPriority.TabIndex = 1
        Me.lblPriority.Text = "00"
        Me.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Priority"
        '
        'TabPagePerf
        '
        Me.TabPagePerf.Controls.Add(Me.splitPerformances)
        Me.TabPagePerf.Location = New System.Drawing.Point(4, 40)
        Me.TabPagePerf.Name = "TabPagePerf"
        Me.TabPagePerf.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePerf.Size = New System.Drawing.Size(647, 282)
        Me.TabPagePerf.TabIndex = 2
        Me.TabPagePerf.Text = "Performances"
        Me.TabPagePerf.UseVisualStyleBackColor = True
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
        Me.splitPerformances.Size = New System.Drawing.Size(641, 276)
        Me.splitPerformances.SplitterDistance = 82
        Me.splitPerformances.SplitterWidth = 1
        Me.splitPerformances.TabIndex = 3
        '
        'graphCPU
        '
        Me.graphCPU.BackColor = System.Drawing.Color.Black
        Me.graphCPU.Color2 = System.Drawing.Color.Olive
        Me.graphCPU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphCPU.EnableGraph = True
        Me.graphCPU.Fixedheight = True
        Me.graphCPU.GridStep = 13
        Me.graphCPU.Location = New System.Drawing.Point(0, 0)
        Me.graphCPU.Name = "graphCPU"
        Me.graphCPU.ShowSecondGraph = True
        Me.graphCPU.Size = New System.Drawing.Size(641, 82)
        Me.graphCPU.TabIndex = 1
        Me.graphCPU.TabStop = False
        Me.graphCPU.TextColor = System.Drawing.Color.Lime
        Me.graphCPU.TopText = Nothing
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
        Me.splitPerformance2.Size = New System.Drawing.Size(641, 193)
        Me.splitPerformance2.SplitterDistance = 88
        Me.splitPerformance2.SplitterWidth = 1
        Me.splitPerformance2.TabIndex = 0
        '
        'graphMemory
        '
        Me.graphMemory.BackColor = System.Drawing.Color.Black
        Me.graphMemory.Color = System.Drawing.Color.Red
        Me.graphMemory.Color2 = System.Drawing.Color.Maroon
        Me.graphMemory.Color3 = System.Drawing.Color.LightCoral
        Me.graphMemory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphMemory.EnableGraph = True
        Me.graphMemory.Fixedheight = False
        Me.graphMemory.GridStep = 13
        Me.graphMemory.Location = New System.Drawing.Point(0, 0)
        Me.graphMemory.Name = "graphMemory"
        Me.graphMemory.ShowSecondGraph = True
        Me.graphMemory.Size = New System.Drawing.Size(641, 88)
        Me.graphMemory.TabIndex = 2
        Me.graphMemory.TabStop = False
        Me.graphMemory.TextColor = System.Drawing.Color.Lime
        Me.graphMemory.TopText = Nothing
        '
        'graphIO
        '
        Me.graphIO.BackColor = System.Drawing.Color.Black
        Me.graphIO.Color = System.Drawing.Color.LimeGreen
        Me.graphIO.Color2 = System.Drawing.Color.Green
        Me.graphIO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphIO.EnableGraph = True
        Me.graphIO.Fixedheight = False
        Me.graphIO.GridStep = 13
        Me.graphIO.Location = New System.Drawing.Point(0, 0)
        Me.graphIO.Name = "graphIO"
        Me.graphIO.ShowSecondGraph = False
        Me.graphIO.Size = New System.Drawing.Size(641, 104)
        Me.graphIO.TabIndex = 3
        Me.graphIO.TabStop = False
        Me.graphIO.TextColor = System.Drawing.Color.Lime
        Me.graphIO.TopText = Nothing
        '
        'TabPageToken
        '
        Me.TabPageToken.Controls.Add(Me.tabProcessToken)
        Me.TabPageToken.Location = New System.Drawing.Point(4, 40)
        Me.TabPageToken.Name = "TabPageToken"
        Me.TabPageToken.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageToken.Size = New System.Drawing.Size(647, 282)
        Me.TabPageToken.TabIndex = 3
        Me.TabPageToken.Text = "Token"
        Me.TabPageToken.UseVisualStyleBackColor = True
        '
        'tabProcessToken
        '
        Me.tabProcessToken.Controls.Add(Me.tabProcessTokenPagePrivileges)
        Me.tabProcessToken.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProcessToken.Location = New System.Drawing.Point(3, 3)
        Me.tabProcessToken.Name = "tabProcessToken"
        Me.tabProcessToken.SelectedIndex = 0
        Me.tabProcessToken.Size = New System.Drawing.Size(641, 276)
        Me.tabProcessToken.TabIndex = 0
        '
        'tabProcessTokenPagePrivileges
        '
        Me.tabProcessTokenPagePrivileges.Controls.Add(Me.lvPrivileges)
        Me.tabProcessTokenPagePrivileges.Location = New System.Drawing.Point(4, 22)
        Me.tabProcessTokenPagePrivileges.Name = "tabProcessTokenPagePrivileges"
        Me.tabProcessTokenPagePrivileges.Padding = New System.Windows.Forms.Padding(3)
        Me.tabProcessTokenPagePrivileges.Size = New System.Drawing.Size(633, 250)
        Me.tabProcessTokenPagePrivileges.TabIndex = 0
        Me.tabProcessTokenPagePrivileges.Text = "Privileges"
        Me.tabProcessTokenPagePrivileges.UseVisualStyleBackColor = True
        '
        'lvPrivileges
        '
        Me.lvPrivileges.AllowColumnReorder = True
        Me.lvPrivileges.CatchErrors = False
        Me.lvPrivileges.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader50, Me.ColumnHeader51, Me.ColumnHeader52})
        CConnection1.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection1.Snapshot = Nothing
        CConnection1.SnapshotFile = Nothing
        Me.lvPrivileges.ConnectionObj = CConnection1
        Me.lvPrivileges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvPrivileges.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvPrivileges.FullRowSelect = True
        Me.lvPrivileges.HideSelection = False
        Me.lvPrivileges.IsConnected = False
        Me.lvPrivileges.Location = New System.Drawing.Point(3, 3)
        Me.lvPrivileges.Name = "lvPrivileges"
        Me.lvPrivileges.OverriddenDoubleBuffered = True
        Me.lvPrivileges.ProcessId = 0
        Me.lvPrivileges.ReorganizeColumns = True
        Me.lvPrivileges.ShowObjectDetailsOnDoubleClick = True
        Me.lvPrivileges.Size = New System.Drawing.Size(627, 244)
        Me.lvPrivileges.TabIndex = 13
        Me.lvPrivileges.UseCompatibleStateImageBehavior = False
        Me.lvPrivileges.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader50
        '
        Me.ColumnHeader50.Text = "Name"
        Me.ColumnHeader50.Width = 159
        '
        'ColumnHeader51
        '
        Me.ColumnHeader51.Text = "Status"
        Me.ColumnHeader51.Width = 100
        '
        'ColumnHeader52
        '
        Me.ColumnHeader52.Text = "Description"
        Me.ColumnHeader52.Width = 319
        '
        'TabPageMemory
        '
        Me.TabPageMemory.Controls.Add(Me.lvProcMem)
        Me.TabPageMemory.Location = New System.Drawing.Point(4, 40)
        Me.TabPageMemory.Name = "TabPageMemory"
        Me.TabPageMemory.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageMemory.Size = New System.Drawing.Size(647, 282)
        Me.TabPageMemory.TabIndex = 4
        Me.TabPageMemory.Text = "Memory"
        Me.TabPageMemory.UseVisualStyleBackColor = True
        '
        'lvProcMem
        '
        Me.lvProcMem.AllowColumnReorder = True
        Me.lvProcMem.CatchErrors = False
        Me.lvProcMem.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader53, Me.ColumnHeader54, Me.ColumnHeader55, Me.ColumnHeader56, Me.ColumnHeader13})
        CConnection2.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection2.Snapshot = Nothing
        CConnection2.SnapshotFile = Nothing
        Me.lvProcMem.ConnectionObj = CConnection2
        Me.lvProcMem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcMem.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcMem.FullRowSelect = True
        Me.lvProcMem.HideSelection = False
        Me.lvProcMem.IsConnected = False
        Me.lvProcMem.Location = New System.Drawing.Point(3, 3)
        Me.lvProcMem.Name = "lvProcMem"
        Me.lvProcMem.OverriddenDoubleBuffered = True
        Me.lvProcMem.ProcessId = 0
        Me.lvProcMem.ReorganizeColumns = True
        Me.lvProcMem.ShowObjectDetailsOnDoubleClick = False
        Me.lvProcMem.Size = New System.Drawing.Size(641, 276)
        Me.lvProcMem.TabIndex = 14
        Me.lvProcMem.UseCompatibleStateImageBehavior = False
        Me.lvProcMem.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader53
        '
        Me.ColumnHeader53.Text = "Name"
        Me.ColumnHeader53.Width = 170
        '
        'ColumnHeader54
        '
        Me.ColumnHeader54.Text = "Address"
        Me.ColumnHeader54.Width = 82
        '
        'ColumnHeader55
        '
        Me.ColumnHeader55.Text = "Size"
        Me.ColumnHeader55.Width = 64
        '
        'ColumnHeader56
        '
        Me.ColumnHeader56.Text = "Protection"
        Me.ColumnHeader56.Width = 85
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "File"
        Me.ColumnHeader13.Width = 338
        '
        'TabPageInfos
        '
        Me.TabPageInfos.Controls.Add(Me.SplitContainerInfoProcess)
        Me.TabPageInfos.Location = New System.Drawing.Point(4, 40)
        Me.TabPageInfos.Name = "TabPageInfos"
        Me.TabPageInfos.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageInfos.Size = New System.Drawing.Size(647, 282)
        Me.TabPageInfos.TabIndex = 5
        Me.TabPageInfos.Text = "Informations"
        Me.TabPageInfos.UseVisualStyleBackColor = True
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
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.cmdRefresh)
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.cmdInfosToClipB)
        '
        'SplitContainerInfoProcess.Panel2
        '
        Me.SplitContainerInfoProcess.Panel2.Controls.Add(Me.rtb)
        Me.SplitContainerInfoProcess.Size = New System.Drawing.Size(641, 276)
        Me.SplitContainerInfoProcess.SplitterDistance = 25
        Me.SplitContainerInfoProcess.TabIndex = 0
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = Global.My.Resources.Resources.refresh16
        Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRefresh.Location = New System.Drawing.Point(159, 0)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(85, 24)
        Me.cmdRefresh.TabIndex = 1
        Me.cmdRefresh.Text = "Refresh   "
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'cmdInfosToClipB
        '
        Me.cmdInfosToClipB.Enabled = False
        Me.cmdInfosToClipB.Image = Global.My.Resources.Resources.copy16
        Me.cmdInfosToClipB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdInfosToClipB.Location = New System.Drawing.Point(3, 0)
        Me.cmdInfosToClipB.Name = "cmdInfosToClipB"
        Me.cmdInfosToClipB.Size = New System.Drawing.Size(150, 24)
        Me.cmdInfosToClipB.TabIndex = 0
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
        Me.rtb.Size = New System.Drawing.Size(641, 247)
        Me.rtb.TabIndex = 4
        Me.rtb.Text = ""
        '
        'TabPageServices
        '
        Me.TabPageServices.Controls.Add(Me.lvProcServices)
        Me.TabPageServices.Location = New System.Drawing.Point(4, 40)
        Me.TabPageServices.Name = "TabPageServices"
        Me.TabPageServices.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageServices.Size = New System.Drawing.Size(647, 282)
        Me.TabPageServices.TabIndex = 6
        Me.TabPageServices.Text = "Services"
        Me.TabPageServices.UseVisualStyleBackColor = True
        '
        'lvProcServices
        '
        Me.lvProcServices.AllowColumnReorder = True
        Me.lvProcServices.CatchErrors = False
        Me.lvProcServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader19})
        CConnection3.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection3.Snapshot = Nothing
        CConnection3.SnapshotFile = Nothing
        Me.lvProcServices.ConnectionObj = CConnection3
        Me.lvProcServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcServices.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcServices.FullRowSelect = True
        Me.lvProcServices.HideSelection = False
        Me.lvProcServices.IsConnected = False
        Me.lvProcServices.Location = New System.Drawing.Point(3, 3)
        Me.lvProcServices.Name = "lvProcServices"
        Me.lvProcServices.OverriddenDoubleBuffered = True
        Me.lvProcServices.ProcessId = 0
        Me.lvProcServices.ReorganizeColumns = True
        Me.lvProcServices.ShowAll = False
        Me.lvProcServices.ShowObjectDetailsOnDoubleClick = False
        Me.lvProcServices.Size = New System.Drawing.Size(641, 276)
        Me.lvProcServices.TabIndex = 2
        Me.lvProcServices.UseCompatibleStateImageBehavior = False
        Me.lvProcServices.View = System.Windows.Forms.View.Details
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
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "ServiceType"
        Me.ColumnHeader19.Width = 100
        '
        'TabPageNetwork
        '
        Me.TabPageNetwork.Controls.Add(Me.lvProcNetwork)
        Me.TabPageNetwork.Location = New System.Drawing.Point(4, 40)
        Me.TabPageNetwork.Name = "TabPageNetwork"
        Me.TabPageNetwork.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNetwork.Size = New System.Drawing.Size(647, 282)
        Me.TabPageNetwork.TabIndex = 7
        Me.TabPageNetwork.Text = "Network"
        Me.TabPageNetwork.UseVisualStyleBackColor = True
        '
        'lvProcNetwork
        '
        Me.lvProcNetwork.AllowColumnReorder = True
        Me.lvProcNetwork.CatchErrors = False
        Me.lvProcNetwork.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader49, Me.ColumnHeader57, Me.ColumnHeader58, Me.ColumnHeader59})
        CConnection4.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection4.Snapshot = Nothing
        CConnection4.SnapshotFile = Nothing
        Me.lvProcNetwork.ConnectionObj = CConnection4
        Me.lvProcNetwork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcNetwork.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcNetwork.FullRowSelect = True
        Me.lvProcNetwork.HideSelection = False
        Me.lvProcNetwork.IsConnected = False
        Me.lvProcNetwork.Location = New System.Drawing.Point(3, 3)
        Me.lvProcNetwork.Name = "lvProcNetwork"
        Me.lvProcNetwork.OverriddenDoubleBuffered = True
        Me.lvProcNetwork.ProcessId = Nothing
        Me.lvProcNetwork.ReorganizeColumns = True
        Me.lvProcNetwork.ShowAllPid = False
        Me.lvProcNetwork.ShowConnectionsByProcessesGroup = False
        Me.lvProcNetwork.ShowObjectDetailsOnDoubleClick = True
        Me.lvProcNetwork.Size = New System.Drawing.Size(641, 276)
        Me.lvProcNetwork.TabIndex = 21
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
        Me.ColumnHeader57.Text = "Remote"
        Me.ColumnHeader57.Width = 200
        '
        'ColumnHeader58
        '
        Me.ColumnHeader58.Text = "Protocol"
        '
        'ColumnHeader59
        '
        Me.ColumnHeader59.Text = "State"
        Me.ColumnHeader59.Width = 150
        '
        'TabPageString
        '
        Me.TabPageString.Controls.Add(Me.SplitContainerStrings)
        Me.TabPageString.Location = New System.Drawing.Point(4, 40)
        Me.TabPageString.Name = "TabPageString"
        Me.TabPageString.Size = New System.Drawing.Size(647, 282)
        Me.TabPageString.TabIndex = 8
        Me.TabPageString.Text = "Strings"
        Me.TabPageString.UseVisualStyleBackColor = True
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
        Me.SplitContainerStrings.Size = New System.Drawing.Size(647, 282)
        Me.SplitContainerStrings.SplitterDistance = 242
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
        Me.lvProcString.Name = "lvProcString"
        Me.lvProcString.OverriddenDoubleBuffered = True
        Me.lvProcString.Size = New System.Drawing.Size(647, 242)
        Me.lvProcString.TabIndex = 22
        Me.lvProcString.UseCompatibleStateImageBehavior = False
        Me.lvProcString.View = System.Windows.Forms.View.Details
        Me.lvProcString.VirtualMode = True
        '
        'ColumnHeader76
        '
        Me.ColumnHeader76.Text = "Position"
        Me.ColumnHeader76.Width = 149
        '
        'ColumnHeader77
        '
        Me.ColumnHeader77.Text = "String"
        Me.ColumnHeader77.Width = 447
        '
        'cmdProcSearchR
        '
        Me.cmdProcSearchR.Image = Global.My.Resources.Resources.arrow_000_medium
        Me.cmdProcSearchR.Location = New System.Drawing.Point(499, 1)
        Me.cmdProcSearchR.Name = "cmdProcSearchR"
        Me.cmdProcSearchR.Size = New System.Drawing.Size(19, 23)
        Me.cmdProcSearchR.TabIndex = 29
        Me.cmdProcSearchR.UseVisualStyleBackColor = True
        '
        'cmdProcSearchL
        '
        Me.cmdProcSearchL.Image = Global.My.Resources.Resources.arrow_180_medium
        Me.cmdProcSearchL.Location = New System.Drawing.Point(476, 1)
        Me.cmdProcSearchL.Name = "cmdProcSearchL"
        Me.cmdProcSearchL.Size = New System.Drawing.Size(19, 23)
        Me.cmdProcSearchL.TabIndex = 28
        Me.cmdProcSearchL.UseVisualStyleBackColor = True
        '
        'pgbString
        '
        Me.pgbString.Location = New System.Drawing.Point(248, 1)
        Me.pgbString.Name = "pgbString"
        Me.pgbString.Size = New System.Drawing.Size(102, 23)
        Me.pgbString.TabIndex = 26
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
        Me.txtSearchProcString.Size = New System.Drawing.Size(60, 22)
        Me.txtSearchProcString.TabIndex = 27
        '
        'cmdProcStringSave
        '
        Me.cmdProcStringSave.Image = Global.My.Resources.Resources.save16
        Me.cmdProcStringSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdProcStringSave.Location = New System.Drawing.Point(140, 1)
        Me.cmdProcStringSave.Name = "cmdProcStringSave"
        Me.cmdProcStringSave.Size = New System.Drawing.Size(91, 23)
        Me.cmdProcStringSave.TabIndex = 25
        Me.cmdProcStringSave.Text = "Save..."
        Me.cmdProcStringSave.UseVisualStyleBackColor = True
        '
        'optProcStringMemory
        '
        Me.optProcStringMemory.AutoSize = True
        Me.optProcStringMemory.Location = New System.Drawing.Point(63, 4)
        Me.optProcStringMemory.Name = "optProcStringMemory"
        Me.optProcStringMemory.Size = New System.Drawing.Size(66, 17)
        Me.optProcStringMemory.TabIndex = 24
        Me.optProcStringMemory.Text = "Memory"
        Me.optProcStringMemory.UseVisualStyleBackColor = True
        '
        'optProcStringImage
        '
        Me.optProcStringImage.AutoSize = True
        Me.optProcStringImage.Checked = True
        Me.optProcStringImage.Location = New System.Drawing.Point(3, 4)
        Me.optProcStringImage.Name = "optProcStringImage"
        Me.optProcStringImage.Size = New System.Drawing.Size(56, 17)
        Me.optProcStringImage.TabIndex = 23
        Me.optProcStringImage.TabStop = True
        Me.optProcStringImage.Text = "Image"
        Me.optProcStringImage.UseVisualStyleBackColor = True
        '
        'TabPageEnv
        '
        Me.TabPageEnv.Controls.Add(Me.lvProcEnv)
        Me.TabPageEnv.Location = New System.Drawing.Point(4, 40)
        Me.TabPageEnv.Name = "TabPageEnv"
        Me.TabPageEnv.Size = New System.Drawing.Size(647, 282)
        Me.TabPageEnv.TabIndex = 9
        Me.TabPageEnv.Text = "Environment"
        Me.TabPageEnv.UseVisualStyleBackColor = True
        '
        'lvProcEnv
        '
        Me.lvProcEnv.AllowColumnReorder = True
        Me.lvProcEnv.CatchErrors = False
        Me.lvProcEnv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader60, Me.ColumnHeader61})
        CConnection5.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection5.Snapshot = Nothing
        CConnection5.SnapshotFile = Nothing
        Me.lvProcEnv.ConnectionObj = CConnection5
        Me.lvProcEnv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcEnv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcEnv.FullRowSelect = True
        Me.lvProcEnv.HideSelection = False
        Me.lvProcEnv.IsConnected = False
        Me.lvProcEnv.Location = New System.Drawing.Point(0, 0)
        Me.lvProcEnv.Name = "lvProcEnv"
        Me.lvProcEnv.OverriddenDoubleBuffered = True
        'TODO: La génération de code pour 'Me.lvProcEnv.Peb' a échoué en raison de l'exception 'Type Primitive non valide : System.IntPtr. Si possible, utilisez CodeObjectCreateExpression à la place.'.
        Me.lvProcEnv.ProcessId = 0
        Me.lvProcEnv.ReorganizeColumns = True
        Me.lvProcEnv.ShowObjectDetailsOnDoubleClick = True
        Me.lvProcEnv.Size = New System.Drawing.Size(647, 282)
        Me.lvProcEnv.TabIndex = 30
        Me.lvProcEnv.UseCompatibleStateImageBehavior = False
        Me.lvProcEnv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader60
        '
        Me.ColumnHeader60.Text = "Variable"
        Me.ColumnHeader60.Width = 169
        '
        'ColumnHeader61
        '
        Me.ColumnHeader61.Text = "Value"
        Me.ColumnHeader61.Width = 431
        '
        'TabPageModules
        '
        Me.TabPageModules.Controls.Add(Me.lvModules)
        Me.TabPageModules.Location = New System.Drawing.Point(4, 40)
        Me.TabPageModules.Name = "TabPageModules"
        Me.TabPageModules.Size = New System.Drawing.Size(647, 282)
        Me.TabPageModules.TabIndex = 10
        Me.TabPageModules.Text = "Modules"
        Me.TabPageModules.UseVisualStyleBackColor = True
        '
        'lvModules
        '
        Me.lvModules.AllowColumnReorder = True
        Me.lvModules.CatchErrors = False
        Me.lvModules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader29, Me.ColumnHeader43, Me.ColumnHeader44, Me.ColumnHeader45, Me.ColumnHeader46, Me.ColumnHeader1})
        CConnection6.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection6.Snapshot = Nothing
        CConnection6.SnapshotFile = Nothing
        Me.lvModules.ConnectionObj = CConnection6
        Me.lvModules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvModules.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvModules.FullRowSelect = True
        Me.lvModules.HideSelection = False
        Me.lvModules.IsConnected = False
        Me.lvModules.Location = New System.Drawing.Point(0, 0)
        Me.lvModules.Name = "lvModules"
        Me.lvModules.OverriddenDoubleBuffered = True
        Me.lvModules.ProcessId = Nothing
        Me.lvModules.ReorganizeColumns = True
        Me.lvModules.ShowObjectDetailsOnDoubleClick = True
        Me.lvModules.Size = New System.Drawing.Size(647, 282)
        Me.lvModules.TabIndex = 31
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
        Me.ColumnHeader43.DisplayIndex = 2
        Me.ColumnHeader43.Text = "Version"
        Me.ColumnHeader43.Width = 85
        '
        'ColumnHeader44
        '
        Me.ColumnHeader44.DisplayIndex = 3
        Me.ColumnHeader44.Text = "Description"
        Me.ColumnHeader44.Width = 210
        '
        'ColumnHeader45
        '
        Me.ColumnHeader45.DisplayIndex = 4
        Me.ColumnHeader45.Text = "CompanyName"
        Me.ColumnHeader45.Width = 150
        '
        'ColumnHeader46
        '
        Me.ColumnHeader46.DisplayIndex = 5
        Me.ColumnHeader46.Text = "Path"
        Me.ColumnHeader46.Width = 300
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.DisplayIndex = 1
        Me.ColumnHeader1.Text = "Address"
        Me.ColumnHeader1.Width = 80
        '
        'TabPageThreads
        '
        Me.TabPageThreads.Controls.Add(Me.lvThreads)
        Me.TabPageThreads.Location = New System.Drawing.Point(4, 40)
        Me.TabPageThreads.Name = "TabPageThreads"
        Me.TabPageThreads.Size = New System.Drawing.Size(647, 282)
        Me.TabPageThreads.TabIndex = 11
        Me.TabPageThreads.Text = "Threads"
        Me.TabPageThreads.UseVisualStyleBackColor = True
        '
        'lvThreads
        '
        Me.lvThreads.AllowColumnReorder = True
        Me.lvThreads.CatchErrors = False
        Me.lvThreads.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader32, Me.ColumnHeader12, Me.ColumnHeader34, Me.ColumnHeader35, Me.ColumnHeader36, Me.ColumnHeader37, Me.ColumnHeader38, Me.ColumnHeader6, Me.ColumnHeader11})
        CConnection7.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection7.Snapshot = Nothing
        CConnection7.SnapshotFile = Nothing
        Me.lvThreads.ConnectionObj = CConnection7
        Me.lvThreads.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvThreads.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvThreads.FullRowSelect = True
        Me.lvThreads.HideSelection = False
        Me.lvThreads.IsConnected = False
        Me.lvThreads.Location = New System.Drawing.Point(0, 0)
        Me.lvThreads.Name = "lvThreads"
        Me.lvThreads.OverriddenDoubleBuffered = True
        Me.lvThreads.ProcessId = Nothing
        Me.lvThreads.ReorganizeColumns = True
        Me.lvThreads.ShowObjectDetailsOnDoubleClick = True
        Me.lvThreads.Size = New System.Drawing.Size(647, 282)
        Me.lvThreads.TabIndex = 4
        Me.lvThreads.UseCompatibleStateImageBehavior = False
        Me.lvThreads.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader32
        '
        Me.ColumnHeader32.Text = "Id"
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "ContextSwitchDelta"
        Me.ColumnHeader12.Width = 118
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
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "ContextSwitchCount"
        Me.ColumnHeader11.Width = 200
        '
        'TabPageWindows
        '
        Me.TabPageWindows.Controls.Add(Me.lvWindows)
        Me.TabPageWindows.Location = New System.Drawing.Point(4, 40)
        Me.TabPageWindows.Name = "TabPageWindows"
        Me.TabPageWindows.Size = New System.Drawing.Size(647, 282)
        Me.TabPageWindows.TabIndex = 12
        Me.TabPageWindows.Text = "Windows"
        Me.TabPageWindows.UseVisualStyleBackColor = True
        '
        'lvWindows
        '
        Me.lvWindows.AllowColumnReorder = True
        Me.lvWindows.CatchErrors = False
        Me.lvWindows.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader30, Me.ColumnHeader39, Me.ColumnHeader40, Me.ColumnHeader41, Me.ColumnHeader42})
        CConnection8.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection8.Snapshot = Nothing
        CConnection8.SnapshotFile = Nothing
        Me.lvWindows.ConnectionObj = CConnection8
        Me.lvWindows.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvWindows.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvWindows.FullRowSelect = True
        Me.lvWindows.HideSelection = False
        Me.lvWindows.IsConnected = False
        Me.lvWindows.Location = New System.Drawing.Point(0, 0)
        Me.lvWindows.Name = "lvWindows"
        Me.lvWindows.OverriddenDoubleBuffered = True
        Me.lvWindows.ProcessId = Nothing
        Me.lvWindows.ReorganizeColumns = True
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowObjectDetailsOnDoubleClick = True
        Me.lvWindows.ShowUnNamed = False
        Me.lvWindows.Size = New System.Drawing.Size(647, 282)
        Me.lvWindows.TabIndex = 33
        Me.lvWindows.UseCompatibleStateImageBehavior = False
        Me.lvWindows.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader30
        '
        Me.ColumnHeader30.Text = "Id"
        Me.ColumnHeader30.Width = 100
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
        'TabPageHandles
        '
        Me.TabPageHandles.Controls.Add(Me.lvHandles)
        Me.TabPageHandles.Location = New System.Drawing.Point(4, 40)
        Me.TabPageHandles.Name = "TabPageHandles"
        Me.TabPageHandles.Size = New System.Drawing.Size(647, 282)
        Me.TabPageHandles.TabIndex = 13
        Me.TabPageHandles.Text = "Handles"
        Me.TabPageHandles.UseVisualStyleBackColor = True
        '
        'lvHandles
        '
        Me.lvHandles.AllowColumnReorder = True
        Me.lvHandles.CatchErrors = False
        Me.lvHandles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader24, Me.ColumnHeader25, Me.ColumnHeader26, Me.ColumnHeader27, Me.ColumnHeader28, Me.ColumnHeader15})
        CConnection9.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection9.Snapshot = Nothing
        CConnection9.SnapshotFile = Nothing
        Me.lvHandles.ConnectionObj = CConnection9
        Me.lvHandles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvHandles.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvHandles.FullRowSelect = True
        Me.lvHandles.HideSelection = False
        Me.lvHandles.IsConnected = False
        Me.lvHandles.Location = New System.Drawing.Point(0, 0)
        Me.lvHandles.Name = "lvHandles"
        Me.lvHandles.OverriddenDoubleBuffered = True
        Me.lvHandles.ProcessId = Nothing
        Me.lvHandles.ReorganizeColumns = True
        Me.lvHandles.ShowObjectDetailsOnDoubleClick = False
        Me.lvHandles.ShowUnnamed = False
        Me.lvHandles.Size = New System.Drawing.Size(647, 282)
        Me.lvHandles.TabIndex = 34
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
        'TabPageLog
        '
        Me.TabPageLog.Controls.Add(Me.SplitContainerLog)
        Me.TabPageLog.Location = New System.Drawing.Point(4, 40)
        Me.TabPageLog.Name = "TabPageLog"
        Me.TabPageLog.Size = New System.Drawing.Size(647, 282)
        Me.TabPageLog.TabIndex = 14
        Me.TabPageLog.Text = "Log"
        Me.TabPageLog.UseVisualStyleBackColor = True
        '
        'SplitContainerLog
        '
        Me.SplitContainerLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerLog.IsSplitterFixed = True
        Me.SplitContainerLog.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerLog.Name = "SplitContainerLog"
        Me.SplitContainerLog.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerLog.Panel1
        '
        Me.SplitContainerLog.Panel1.Controls.Add(Me.cmdLogOptions)
        Me.SplitContainerLog.Panel1.Controls.Add(Me.cmdSave)
        Me.SplitContainerLog.Panel1.Controls.Add(Me.cmdClearLog)
        Me.SplitContainerLog.Panel1.Controls.Add(Me.chkLog)
        '
        'SplitContainerLog.Panel2
        '
        Me.SplitContainerLog.Panel2.Controls.Add(Me.lvLog)
        Me.SplitContainerLog.Size = New System.Drawing.Size(647, 282)
        Me.SplitContainerLog.SplitterDistance = 25
        Me.SplitContainerLog.TabIndex = 0
        '
        'cmdLogOptions
        '
        Me.cmdLogOptions.Location = New System.Drawing.Point(280, 1)
        Me.cmdLogOptions.Name = "cmdLogOptions"
        Me.cmdLogOptions.Size = New System.Drawing.Size(75, 23)
        Me.cmdLogOptions.TabIndex = 5
        Me.cmdLogOptions.Text = "Options..."
        Me.cmdLogOptions.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.My.Resources.Resources.save16
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(181, 1)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(93, 23)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "     Save log..."
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdClearLog
        '
        Me.cmdClearLog.Location = New System.Drawing.Point(100, 1)
        Me.cmdClearLog.Name = "cmdClearLog"
        Me.cmdClearLog.Size = New System.Drawing.Size(75, 23)
        Me.cmdClearLog.TabIndex = 1
        Me.cmdClearLog.Text = "Clear log"
        Me.cmdClearLog.UseVisualStyleBackColor = True
        '
        'chkLog
        '
        Me.chkLog.AutoSize = True
        Me.chkLog.Location = New System.Drawing.Point(8, 4)
        Me.chkLog.Name = "chkLog"
        Me.chkLog.Size = New System.Drawing.Size(86, 17)
        Me.chkLog.TabIndex = 0
        Me.chkLog.Text = "Activate log"
        Me.chkLog.UseVisualStyleBackColor = True
        '
        'lvLog
        '
        Me.lvLog.AllowColumnReorder = True
        Me.lvLog.CaptureItems = asyncCallbackLogEnumerate.LogItemType.AllItems
        Me.lvLog.CatchErrors = False
        Me.lvLog.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader5, Me.ColumnHeader4})
        CConnection10.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection10.Snapshot = Nothing
        CConnection10.SnapshotFile = Nothing
        Me.lvLog.ConnectionObj = CConnection10
        Me.lvLog.DisplayItems = asyncCallbackLogEnumerate.LogItemType.AllItems
        Me.lvLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvLog.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvLog.FullRowSelect = True
        Me.lvLog.HideSelection = False
        Me.lvLog.IsConnected = False
        Me.lvLog.Location = New System.Drawing.Point(0, 0)
        Me.lvLog.MultiSelect = False
        Me.lvLog.Name = "lvLog"
        Me.lvLog.OverriddenDoubleBuffered = True
        Me.lvLog.ProcessId = 0
        Me.lvLog.ReorganizeColumns = True
        Me.lvLog.ShowObjectDetailsOnDoubleClick = True
        Me.lvLog.Size = New System.Drawing.Size(647, 253)
        Me.lvLog.TabIndex = 24
        Me.lvLog.UseCompatibleStateImageBehavior = False
        Me.lvLog.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Date & Time"
        Me.ColumnHeader2.Width = 172
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Type"
        Me.ColumnHeader5.Width = 69
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Description"
        Me.ColumnHeader4.Width = 400
        '
        'TabPageHistory
        '
        Me.TabPageHistory.Controls.Add(Me.containerHistory)
        Me.TabPageHistory.Controls.Add(Me.lstHistoryCat)
        Me.TabPageHistory.Location = New System.Drawing.Point(4, 40)
        Me.TabPageHistory.Name = "TabPageHistory"
        Me.TabPageHistory.Size = New System.Drawing.Size(647, 282)
        Me.TabPageHistory.TabIndex = 15
        Me.TabPageHistory.Text = "History"
        Me.TabPageHistory.UseVisualStyleBackColor = True
        '
        'containerHistory
        '
        Me.containerHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerHistory.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerHistory.IsSplitterFixed = True
        Me.containerHistory.Location = New System.Drawing.Point(208, 0)
        Me.containerHistory.Name = "containerHistory"
        Me.containerHistory.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'containerHistory.Panel1
        '
        Me.containerHistory.Panel1.Controls.Add(Me.Label2)
        Me.containerHistory.Size = New System.Drawing.Size(439, 282)
        Me.containerHistory.SplitterDistance = 25
        Me.containerHistory.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(439, 25)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "  Check items to see history graph"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstHistoryCat
        '
        Me.lstHistoryCat.Dock = System.Windows.Forms.DockStyle.Left
        Me.lstHistoryCat.FormattingEnabled = True
        Me.lstHistoryCat.Items.AddRange(New Object() {"CpuUsage", "AverageCpuUsage", "KernelCpuTime", "UserCpuTime", "TotalCpuTime", "GdiObjects", "UserObjects", "WorkingSet", "PeakWorkingSet", "PageFaultCount", "PagefileUsage", "PeakPagefileUsage", "QuotaPeakPagedPoolUsage", "QuotaPagedPoolUsage", "QuotaPeakNonPagedPoolUsage", "QuotaNonPagedPoolUsage", "ReadOperationCount", "WriteOperationCount", "OtherOperationCount", "ReadTransferCount", "WriteTransferCount", "OtherTransferCount", "ReadOperationCountDelta", "WriteOperationCountDelta", "OtherOperationCountDelta", "ReadTransferCountDelta", "WriteTransferCountDelta", "OtherTransferCountDelta", "TotalIoDelta"})
        Me.lstHistoryCat.Location = New System.Drawing.Point(0, 0)
        Me.lstHistoryCat.Name = "lstHistoryCat"
        Me.lstHistoryCat.Size = New System.Drawing.Size(208, 276)
        Me.lstHistoryCat.TabIndex = 0
        '
        'TabPageHeaps
        '
        Me.TabPageHeaps.Controls.Add(Me.cmdActivateHeapEnumeration)
        Me.TabPageHeaps.Controls.Add(Me.lvHeaps)
        Me.TabPageHeaps.Location = New System.Drawing.Point(4, 40)
        Me.TabPageHeaps.Name = "TabPageHeaps"
        Me.TabPageHeaps.Size = New System.Drawing.Size(647, 282)
        Me.TabPageHeaps.TabIndex = 16
        Me.TabPageHeaps.Text = "Heaps"
        Me.TabPageHeaps.UseVisualStyleBackColor = True
        '
        'cmdActivateHeapEnumeration
        '
        Me.cmdActivateHeapEnumeration.Image = Global.My.Resources.Resources.warning16
        Me.cmdActivateHeapEnumeration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdActivateHeapEnumeration.Location = New System.Drawing.Point(8, 39)
        Me.cmdActivateHeapEnumeration.Name = "cmdActivateHeapEnumeration"
        Me.cmdActivateHeapEnumeration.Size = New System.Drawing.Size(289, 23)
        Me.cmdActivateHeapEnumeration.TabIndex = 16
        Me.cmdActivateHeapEnumeration.Text = "     You have to manually unlock heap enumeration"
        Me.cmdActivateHeapEnumeration.UseVisualStyleBackColor = True
        '
        'lvHeaps
        '
        Me.lvHeaps.AllowColumnReorder = True
        Me.lvHeaps.CatchErrors = False
        Me.lvHeaps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader16, Me.ColumnHeader17, Me.ColumnHeader18, Me.ColumnHeader20, Me.ColumnHeader14})
        CConnection11.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        CConnection11.Snapshot = Nothing
        CConnection11.SnapshotFile = Nothing
        Me.lvHeaps.ConnectionObj = CConnection11
        Me.lvHeaps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvHeaps.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvHeaps.FullRowSelect = True
        Me.lvHeaps.HideSelection = False
        Me.lvHeaps.IsConnected = False
        Me.lvHeaps.Location = New System.Drawing.Point(0, 0)
        Me.lvHeaps.Name = "lvHeaps"
        Me.lvHeaps.OverriddenDoubleBuffered = True
        Me.lvHeaps.ProcessId = 0
        Me.lvHeaps.ReorganizeColumns = True
        Me.lvHeaps.ShowObjectDetailsOnDoubleClick = False
        Me.lvHeaps.Size = New System.Drawing.Size(647, 282)
        Me.lvHeaps.TabIndex = 15
        Me.lvHeaps.UseCompatibleStateImageBehavior = False
        Me.lvHeaps.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Address"
        Me.ColumnHeader16.Width = 101
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "BlockCount"
        Me.ColumnHeader17.Width = 101
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "MemCommitted"
        Me.ColumnHeader18.Width = 117
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "MemAllocated"
        Me.ColumnHeader20.Width = 100
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Flags"
        Me.ColumnHeader14.Width = 100
        '
        'cmdHideFindPanel
        '
        Me.cmdHideFindPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdHideFindPanel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdHideFindPanel.Location = New System.Drawing.Point(628, 3)
        Me.cmdHideFindPanel.Name = "cmdHideFindPanel"
        Me.cmdHideFindPanel.Size = New System.Drawing.Size(23, 20)
        Me.cmdHideFindPanel.TabIndex = 18
        Me.cmdHideFindPanel.Text = "X"
        Me.cmdHideFindPanel.UseVisualStyleBackColor = True
        '
        'chkFreeze
        '
        Me.chkFreeze.AutoSize = True
        Me.chkFreeze.Location = New System.Drawing.Point(479, 5)
        Me.chkFreeze.Name = "chkFreeze"
        Me.chkFreeze.Size = New System.Drawing.Size(136, 17)
        Me.chkFreeze.TabIndex = 17
        Me.chkFreeze.Text = "Suspend refreshment"
        Me.chkFreeze.UseVisualStyleBackColor = True
        '
        'lblSearchItemCaption
        '
        Me.lblSearchItemCaption.AutoSize = True
        Me.lblSearchItemCaption.Enabled = False
        Me.lblSearchItemCaption.Location = New System.Drawing.Point(6, 6)
        Me.lblSearchItemCaption.Name = "lblSearchItemCaption"
        Me.lblSearchItemCaption.Size = New System.Drawing.Size(66, 13)
        Me.lblSearchItemCaption.TabIndex = 16
        Me.lblSearchItemCaption.Text = "Search item"
        '
        'lblResCount
        '
        Me.lblResCount.AutoSize = True
        Me.lblResCount.Enabled = False
        Me.lblResCount.Location = New System.Drawing.Point(396, 6)
        Me.lblResCount.Name = "lblResCount"
        Me.lblResCount.Size = New System.Drawing.Size(56, 13)
        Me.lblResCount.TabIndex = 15
        Me.lblResCount.Text = "0 result(s)"
        '
        'txtSearch
        '
        Me.txtSearch.Enabled = False
        Me.txtSearch.Location = New System.Drawing.Point(75, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(312, 22)
        Me.txtSearch.TabIndex = 14
        '
        'frmProcessInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 356)
        Me.Controls.Add(Me.SplitContainer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(660, 392)
        Me.Name = "frmProcessInfo"
        Me.Text = "Process informations"
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        Me.SplitContainer.ResumeLayout(False)
        Me.tabProcess.ResumeLayout(False)
        Me.TabPageGeneral.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.SplitContainerOnlineInfo.Panel1.ResumeLayout(False)
        Me.SplitContainerOnlineInfo.Panel1.PerformLayout()
        Me.SplitContainerOnlineInfo.Panel2.ResumeLayout(False)
        Me.SplitContainerOnlineInfo.ResumeLayout(False)
        Me.GroupBoxProcessInfos.ResumeLayout(False)
        Me.GroupBoxProcessInfos.PerformLayout()
        Me.gpProcGeneralFile.ResumeLayout(False)
        Me.gpProcGeneralFile.PerformLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageStats.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPagePerf.ResumeLayout(False)
        Me.splitPerformances.Panel1.ResumeLayout(False)
        Me.splitPerformances.Panel2.ResumeLayout(False)
        Me.splitPerformances.ResumeLayout(False)
        CType(Me.graphCPU, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitPerformance2.Panel1.ResumeLayout(False)
        Me.splitPerformance2.Panel2.ResumeLayout(False)
        Me.splitPerformance2.ResumeLayout(False)
        CType(Me.graphMemory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.graphIO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageToken.ResumeLayout(False)
        Me.tabProcessToken.ResumeLayout(False)
        Me.tabProcessTokenPagePrivileges.ResumeLayout(False)
        Me.TabPageMemory.ResumeLayout(False)
        Me.TabPageInfos.ResumeLayout(False)
        Me.SplitContainerInfoProcess.Panel1.ResumeLayout(False)
        Me.SplitContainerInfoProcess.Panel2.ResumeLayout(False)
        Me.SplitContainerInfoProcess.ResumeLayout(False)
        Me.TabPageServices.ResumeLayout(False)
        Me.TabPageNetwork.ResumeLayout(False)
        Me.TabPageString.ResumeLayout(False)
        Me.SplitContainerStrings.Panel1.ResumeLayout(False)
        Me.SplitContainerStrings.Panel2.ResumeLayout(False)
        Me.SplitContainerStrings.Panel2.PerformLayout()
        Me.SplitContainerStrings.ResumeLayout(False)
        Me.TabPageEnv.ResumeLayout(False)
        Me.TabPageModules.ResumeLayout(False)
        Me.TabPageThreads.ResumeLayout(False)
        Me.TabPageWindows.ResumeLayout(False)
        Me.TabPageHandles.ResumeLayout(False)
        Me.TabPageLog.ResumeLayout(False)
        Me.SplitContainerLog.Panel1.ResumeLayout(False)
        Me.SplitContainerLog.Panel1.PerformLayout()
        Me.SplitContainerLog.Panel2.ResumeLayout(False)
        Me.SplitContainerLog.ResumeLayout(False)
        Me.TabPageHistory.ResumeLayout(False)
        Me.containerHistory.Panel1.ResumeLayout(False)
        Me.containerHistory.ResumeLayout(False)
        Me.TabPageHeaps.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabProcess As System.Windows.Forms.TabControl
    Friend WithEvents TabPageGeneral As System.Windows.Forms.TabPage
    Friend WithEvents GroupBoxProcessInfos As System.Windows.Forms.GroupBox
    Friend WithEvents txtProcessUser As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtProcessId As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents gpProcGeneralFile As System.Windows.Forms.GroupBox
    Friend WithEvents cmdShowFileDetails As System.Windows.Forms.Button
    Friend WithEvents cmdShowFileProperties As System.Windows.Forms.Button
    Friend WithEvents cmdOpenDirectory As System.Windows.Forms.Button
    Friend WithEvents txtProcessPath As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtImageVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents pctSmallIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pctBigIcon As System.Windows.Forms.PictureBox
    Friend WithEvents TabPageStats As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblUserObjectsCount As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblGDIcount As System.Windows.Forms.Label
    Friend WithEvents lbl789 As System.Windows.Forms.Label
    Friend WithEvents lblHandles As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
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
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblQuotaNPP As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblQuotaPNPP As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblQuotaPP As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblQuotaPPP As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalTime As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblUserTime As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblKernelTime As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabPagePerf As System.Windows.Forms.TabPage
    Friend WithEvents splitPerformances As System.Windows.Forms.SplitContainer
    Friend WithEvents graphCPU As Graph2
    Friend WithEvents splitPerformance2 As System.Windows.Forms.SplitContainer
    Friend WithEvents graphMemory As Graph2
    Friend WithEvents graphIO As Graph2
    Friend WithEvents TabPageToken As System.Windows.Forms.TabPage
    Friend WithEvents tabProcessToken As System.Windows.Forms.TabControl
    Friend WithEvents tabProcessTokenPagePrivileges As System.Windows.Forms.TabPage
    Friend WithEvents lvPrivileges As privilegeList
    Friend WithEvents ColumnHeader50 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader51 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader52 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPageMemory As System.Windows.Forms.TabPage
    Friend WithEvents lvProcMem As memoryList
    Friend WithEvents ColumnHeader53 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader54 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader55 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader56 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPageInfos As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerInfoProcess As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdInfosToClipB As System.Windows.Forms.Button
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents TabPageServices As System.Windows.Forms.TabPage
    Friend WithEvents TabPageNetwork As System.Windows.Forms.TabPage
    Friend WithEvents lvProcNetwork As networkList
    Friend WithEvents ColumnHeader49 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader57 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader58 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader59 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPageString As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerStrings As System.Windows.Forms.SplitContainer
    Friend WithEvents lvProcString As DoubleBufferedLV
    Friend WithEvents ColumnHeader76 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader77 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdProcSearchR As System.Windows.Forms.Button
    Friend WithEvents cmdProcSearchL As System.Windows.Forms.Button
    Friend WithEvents pgbString As System.Windows.Forms.ProgressBar
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtSearchProcString As System.Windows.Forms.TextBox
    Friend WithEvents cmdProcStringSave As System.Windows.Forms.Button
    Friend WithEvents optProcStringMemory As System.Windows.Forms.RadioButton
    Friend WithEvents optProcStringImage As System.Windows.Forms.RadioButton
    Friend WithEvents TabPageEnv As System.Windows.Forms.TabPage
    Friend WithEvents lvProcEnv As envVariableList
    Friend WithEvents ColumnHeader60 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader61 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerProcPerf As System.Windows.Forms.Timer
    Friend WithEvents TabPageModules As System.Windows.Forms.TabPage
    Friend WithEvents TabPageThreads As System.Windows.Forms.TabPage
    Friend WithEvents TabPageWindows As System.Windows.Forms.TabPage
    Friend WithEvents TabPageHandles As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainerOnlineInfo As System.Windows.Forms.SplitContainer
    Friend WithEvents lblSecurityRisk As System.Windows.Forms.Label
    Friend WithEvents cmdGetOnlineInfos As System.Windows.Forms.Button
    Friend WithEvents rtbOnlineInfos As System.Windows.Forms.RichTextBox
    Friend WithEvents lvModules As moduleList
    Friend WithEvents ColumnHeader29 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader43 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader44 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader45 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader46 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvWindows As windowList
    Friend WithEvents ColumnHeader30 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader39 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader40 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader41 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader42 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvHandles As handleList
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader27 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader28 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtCommandLine As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblAverageCPUusage As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lvProcServices As serviceList
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPageLog As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainerLog As System.Windows.Forms.SplitContainer
    Friend WithEvents lvLog As logList
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkLog As System.Windows.Forms.CheckBox
    Friend WithEvents timerLog As System.Windows.Forms.Timer
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdClearLog As System.Windows.Forms.Button
    Friend WithEvents cmdLogOptions As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPageHistory As System.Windows.Forms.TabPage
    Friend WithEvents lstHistoryCat As System.Windows.Forms.CheckedListBox
    Friend WithEvents containerHistory As System.Windows.Forms.SplitContainer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdKill As System.Windows.Forms.Button
    Friend WithEvents cmdSet As System.Windows.Forms.Button
    Friend WithEvents cbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents cmdAffinity As System.Windows.Forms.Button
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents cmdResume As System.Windows.Forms.Button
    Friend WithEvents lvThreads As threadList
    Friend WithEvents ColumnHeader32 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader34 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader35 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader36 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader38 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblOthersBD As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblOtherD As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblWBD As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblWD As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblRBD As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblRD As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblThreads As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtPriority As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRunTime As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessStarted As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtParentProcess As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmdInspectExe As System.Windows.Forms.Button
    Private WithEvents mainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItemCopyBig As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopySmall As System.Windows.Forms.MenuItem
    Private WithEvents menuCopyPctbig As System.Windows.Forms.ContextMenu
    Private WithEvents menuCopyPctSmall As System.Windows.Forms.ContextMenu
    Private WithEvents mnuString As System.Windows.Forms.ContextMenu
    Private WithEvents mnuPrivileges As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemPriEnable As System.Windows.Forms.MenuItem
    Private WithEvents mnuProcMem As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemViewMemory As System.Windows.Forms.MenuItem
    Private WithEvents mnuModule As System.Windows.Forms.ContextMenu
    Private WithEvents mnuThread As System.Windows.Forms.ContextMenu
    Private WithEvents mnuWindow As System.Windows.Forms.ContextMenu
    Private WithEvents mnuHandle As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCloseHandle As System.Windows.Forms.MenuItem
    Private WithEvents mnuNetwork As System.Windows.Forms.ContextMenu
    Friend WithEvents menuCloseTCP As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemColumnsNetwork As System.Windows.Forms.MenuItem
    Private WithEvents mnuService As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemShowUnnamedHandles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemChooseColumnsHandle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPEBAddress As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemColumnsMemory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPriDisable As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemPriRemove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemModuleDependencies As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemUnloadModule As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemViewModuleMemory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemColumnsModule As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServColumns As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServSelService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServPause As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStop As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServReanalize As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServAutoStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServOnDemand As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDisabled As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThTerm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThSuspend As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThResu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThIdle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThLowest As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThAffinity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThColumns As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThBNormal As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThNorm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThANorm As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThHighest As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemThTimeCr As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWShowUn As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWHide As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWFront As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWNotFront As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWActive As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWForeground As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWMin As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWMax As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWPosSize As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWEna As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWDisa As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuLog As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemLogGoto As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyHandle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyPrivilege As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyMemory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyModule As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyThread As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyNetwork As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyService As System.Windows.Forms.MenuItem
    Friend WithEvents menuViewMemory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyLog As System.Windows.Forms.MenuItem
    Private WithEvents mnuEnv As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyEnvVariable As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDepe As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMemoryDump As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyString As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMemoryRelease As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMemoryDecommit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMemoryChangeProtection As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWVisiblity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWFlash As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWStopFlash As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWOpacity As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemWCaption As System.Windows.Forms.MenuItem
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents lblSearchItemCaption As System.Windows.Forms.Label
    Friend WithEvents lblResCount As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents chkFreeze As System.Windows.Forms.CheckBox
    Friend WithEvents cmdHideFindPanel As System.Windows.Forms.Button
    Friend WithEvents MenuItemServDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemHandleDetails As System.Windows.Forms.MenuItem
    Friend WithEvents TabPageHeaps As System.Windows.Forms.TabPage
    Friend WithEvents lvHeaps As heapList
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Private WithEvents mnuHeaps As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyHeaps As System.Windows.Forms.MenuItem
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemColumnsHeap As System.Windows.Forms.MenuItem
    Friend WithEvents cmdGoProcess As System.Windows.Forms.Button
    Friend WithEvents MenuItemNetworkTools As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkPing As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkRoute As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkWhoIs As System.Windows.Forms.MenuItem
    Friend WithEvents cmdActivateHeapEnumeration As System.Windows.Forms.Button
End Class