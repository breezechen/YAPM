<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJobInfo
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
        Dim CConnection2 As cConnection = New cConnection
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobInfo))
        Me.tabJob = New System.Windows.Forms.TabControl
        Me.pageGeneral = New System.Windows.Forms.TabPage
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.cmdTerminateJob = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lvProcess = New processesInJobList
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
        Me.pageStats = New System.Windows.Forms.TabPage
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblPageFaultCount = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblSchedulingClass = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.lblMaxWSS = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.lblMinWSS = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblTotalTerminatedProcesses = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblActiveProcesses = New System.Windows.Forms.Label
        Me.lbl789 = New System.Windows.Forms.Label
        Me.lblTotalProcesses = New System.Windows.Forms.Label
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblPriority = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblTotalPeriod = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblUserPeriod = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblPeriodKernel = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.lblTotalTime = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.lblUserTime = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblKernelTime = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblAffinity = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.pageLimitations = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.cmdSetLimits = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lvLimits = New jobLimitList
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader13 = New System.Windows.Forms.ColumnHeader
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItemCopyLimit = New System.Windows.Forms.MenuItem
        Me.MenuItemProcKill = New System.Windows.Forms.MenuItem
        Me.MenuItemProcStop = New System.Windows.Forms.MenuItem
        Me.MenuItemProcResume = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPIdle = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPBN = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPN = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPAN = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPH = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPRT = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSFileProp = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSOpenDir = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSFileDetails = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSSearch = New System.Windows.Forms.MenuItem
        Me.MenuItemProcSDep = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyProcess = New System.Windows.Forms.MenuItem
        Me.TimerLimits = New System.Windows.Forms.Timer(Me.components)
        Me.mnuLimit = New System.Windows.Forms.ContextMenu
        Me.mnuProcess = New System.Windows.Forms.ContextMenu
        Me.MenuItemProcKillT = New System.Windows.Forms.MenuItem
        Me.MenuItemProcKillByMethod = New System.Windows.Forms.MenuItem
        Me.MenuItemProcPriority = New System.Windows.Forms.MenuItem
        Me.MenuItem44 = New System.Windows.Forms.MenuItem
        Me.MenuItem35 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcWSS = New System.Windows.Forms.MenuItem
        Me.MenuItemProcAff = New System.Windows.Forms.MenuItem
        Me.MenuItemProcDump = New System.Windows.Forms.MenuItem
        Me.MenuItem51 = New System.Windows.Forms.MenuItem
        Me.MenuItem38 = New System.Windows.Forms.MenuItem
        Me.MenuItemProcColumns = New System.Windows.Forms.MenuItem
        Me.tabJob.SuspendLayout()
        Me.pageGeneral.SuspendLayout()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pageStats.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pageLimitations.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabJob
        '
        Me.tabJob.Controls.Add(Me.pageGeneral)
        Me.tabJob.Controls.Add(Me.pageStats)
        Me.tabJob.Controls.Add(Me.pageLimitations)
        Me.tabJob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabJob.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabJob.Location = New System.Drawing.Point(0, 0)
        Me.tabJob.Multiline = True
        Me.tabJob.Name = "tabJob"
        Me.tabJob.SelectedIndex = 0
        Me.tabJob.Size = New System.Drawing.Size(703, 337)
        Me.tabJob.TabIndex = 0
        '
        'pageGeneral
        '
        Me.pageGeneral.Controls.Add(Me.SplitContainer)
        Me.pageGeneral.Location = New System.Drawing.Point(4, 22)
        Me.pageGeneral.Name = "pageGeneral"
        Me.pageGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.pageGeneral.Size = New System.Drawing.Size(695, 311)
        Me.pageGeneral.TabIndex = 0
        Me.pageGeneral.Text = "General"
        Me.pageGeneral.UseVisualStyleBackColor = True
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.cmdTerminateJob)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer.Size = New System.Drawing.Size(689, 305)
        Me.SplitContainer.SplitterDistance = 25
        Me.SplitContainer.TabIndex = 19
        '
        'cmdTerminateJob
        '
        Me.cmdTerminateJob.Image = Global.My.Resources.Resources.cross
        Me.cmdTerminateJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTerminateJob.Location = New System.Drawing.Point(5, 0)
        Me.cmdTerminateJob.Name = "cmdTerminateJob"
        Me.cmdTerminateJob.Size = New System.Drawing.Size(105, 23)
        Me.cmdTerminateJob.TabIndex = 19
        Me.cmdTerminateJob.Text = "Terminate job"
        Me.cmdTerminateJob.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTerminateJob.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lvProcess)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(689, 276)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Processes in job"
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.CatchErrors = False
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c7, Me.c8, Me.c9, Me.c10, Me.ColumnHeader20})
        CConnection1.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        Me.lvProcess.ConnectionObj = CConnection1
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcess.FullRowSelect = True
        ListViewGroup1.Header = "Processes"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.IsConnected = False
        Me.lvProcess.Job = Nothing
        Me.lvProcess.Location = New System.Drawing.Point(3, 18)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.ReorganizeColumns = True
        Me.lvProcess.Size = New System.Drawing.Size(683, 255)
        Me.lvProcess.TabIndex = 4
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
        'pageStats
        '
        Me.pageStats.Controls.Add(Me.GroupBox5)
        Me.pageStats.Controls.Add(Me.GroupBox4)
        Me.pageStats.Controls.Add(Me.GroupBox2)
        Me.pageStats.Location = New System.Drawing.Point(4, 22)
        Me.pageStats.Name = "pageStats"
        Me.pageStats.Padding = New System.Windows.Forms.Padding(3)
        Me.pageStats.Size = New System.Drawing.Size(695, 311)
        Me.pageStats.TabIndex = 8
        Me.pageStats.Text = "Statistics"
        Me.pageStats.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblPageFaultCount)
        Me.GroupBox5.Controls.Add(Me.Label21)
        Me.GroupBox5.Controls.Add(Me.lblSchedulingClass)
        Me.GroupBox5.Controls.Add(Me.Label27)
        Me.GroupBox5.Controls.Add(Me.lblMaxWSS)
        Me.GroupBox5.Controls.Add(Me.Label25)
        Me.GroupBox5.Controls.Add(Me.lblMinWSS)
        Me.GroupBox5.Controls.Add(Me.Label19)
        Me.GroupBox5.Controls.Add(Me.lblTotalTerminatedProcesses)
        Me.GroupBox5.Controls.Add(Me.Label37)
        Me.GroupBox5.Controls.Add(Me.lblActiveProcesses)
        Me.GroupBox5.Controls.Add(Me.lbl789)
        Me.GroupBox5.Controls.Add(Me.lblTotalProcesses)
        Me.GroupBox5.Controls.Add(Me.Label53)
        Me.GroupBox5.Location = New System.Drawing.Point(396, 7)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(222, 173)
        Me.GroupBox5.TabIndex = 7
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Other"
        '
        'lblPageFaultCount
        '
        Me.lblPageFaultCount.AutoSize = True
        Me.lblPageFaultCount.Location = New System.Drawing.Point(155, 126)
        Me.lblPageFaultCount.Name = "lblPageFaultCount"
        Me.lblPageFaultCount.Size = New System.Drawing.Size(19, 13)
        Me.lblPageFaultCount.TabIndex = 13
        Me.lblPageFaultCount.Text = "00"
        Me.lblPageFaultCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(8, 126)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(115, 13)
        Me.Label21.TabIndex = 12
        Me.Label21.Text = "TotalPageFaultCount"
        '
        'lblSchedulingClass
        '
        Me.lblSchedulingClass.AutoSize = True
        Me.lblSchedulingClass.Location = New System.Drawing.Point(155, 108)
        Me.lblSchedulingClass.Name = "lblSchedulingClass"
        Me.lblSchedulingClass.Size = New System.Drawing.Size(19, 13)
        Me.lblSchedulingClass.TabIndex = 11
        Me.lblSchedulingClass.Text = "00"
        Me.lblSchedulingClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(8, 108)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(91, 13)
        Me.Label27.TabIndex = 10
        Me.Label27.Text = "SchedulingClass"
        '
        'lblMaxWSS
        '
        Me.lblMaxWSS.AutoSize = True
        Me.lblMaxWSS.Location = New System.Drawing.Point(155, 90)
        Me.lblMaxWSS.Name = "lblMaxWSS"
        Me.lblMaxWSS.Size = New System.Drawing.Size(19, 13)
        Me.lblMaxWSS.TabIndex = 9
        Me.lblMaxWSS.Text = "00"
        Me.lblMaxWSS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(8, 90)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(137, 13)
        Me.Label25.TabIndex = 8
        Me.Label25.Text = "MaximumWorkingSetSize"
        '
        'lblMinWSS
        '
        Me.lblMinWSS.AutoSize = True
        Me.lblMinWSS.Location = New System.Drawing.Point(155, 72)
        Me.lblMinWSS.Name = "lblMinWSS"
        Me.lblMinWSS.Size = New System.Drawing.Size(19, 13)
        Me.lblMinWSS.TabIndex = 7
        Me.lblMinWSS.Text = "00"
        Me.lblMinWSS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(8, 72)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(136, 13)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "MinimumWorkingSetSize"
        '
        'lblTotalTerminatedProcesses
        '
        Me.lblTotalTerminatedProcesses.AutoSize = True
        Me.lblTotalTerminatedProcesses.Location = New System.Drawing.Point(155, 54)
        Me.lblTotalTerminatedProcesses.Name = "lblTotalTerminatedProcesses"
        Me.lblTotalTerminatedProcesses.Size = New System.Drawing.Size(19, 13)
        Me.lblTotalTerminatedProcesses.TabIndex = 5
        Me.lblTotalTerminatedProcesses.Text = "00"
        Me.lblTotalTerminatedProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(8, 54)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(144, 13)
        Me.Label37.TabIndex = 4
        Me.Label37.Text = "Total terminated processes"
        '
        'lblActiveProcesses
        '
        Me.lblActiveProcesses.AutoSize = True
        Me.lblActiveProcesses.Location = New System.Drawing.Point(155, 35)
        Me.lblActiveProcesses.Name = "lblActiveProcesses"
        Me.lblActiveProcesses.Size = New System.Drawing.Size(19, 13)
        Me.lblActiveProcesses.TabIndex = 3
        Me.lblActiveProcesses.Text = "00"
        Me.lblActiveProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl789
        '
        Me.lbl789.AutoSize = True
        Me.lbl789.Location = New System.Drawing.Point(8, 35)
        Me.lbl789.Name = "lbl789"
        Me.lbl789.Size = New System.Drawing.Size(90, 13)
        Me.lbl789.TabIndex = 2
        Me.lbl789.Text = "Active processes"
        '
        'lblTotalProcesses
        '
        Me.lblTotalProcesses.AutoSize = True
        Me.lblTotalProcesses.Location = New System.Drawing.Point(155, 16)
        Me.lblTotalProcesses.Name = "lblTotalProcesses"
        Me.lblTotalProcesses.Size = New System.Drawing.Size(19, 13)
        Me.lblTotalProcesses.TabIndex = 1
        Me.lblTotalProcesses.Text = "00"
        Me.lblTotalProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(8, 16)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(85, 13)
        Me.Label53.TabIndex = 0
        Me.Label53.Text = "Total processes"
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
        Me.GroupBox4.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(176, 265)
        Me.GroupBox4.TabIndex = 6
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblPriority)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lblTotalPeriod)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.lblUserPeriod)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.lblPeriodKernel)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.lblTotalTime)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.lblUserTime)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lblKernelTime)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.lblAffinity)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Location = New System.Drawing.Point(190, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 174)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "CPU"
        '
        'lblPriority
        '
        Me.lblPriority.AutoSize = True
        Me.lblPriority.Location = New System.Drawing.Point(116, 146)
        Me.lblPriority.Name = "lblPriority"
        Me.lblPriority.Size = New System.Drawing.Size(0, 13)
        Me.lblPriority.TabIndex = 15
        Me.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 146)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Priority"
        '
        'lblTotalPeriod
        '
        Me.lblTotalPeriod.AutoSize = True
        Me.lblTotalPeriod.Location = New System.Drawing.Point(116, 128)
        Me.lblTotalPeriod.Name = "lblTotalPeriod"
        Me.lblTotalPeriod.Size = New System.Drawing.Size(70, 13)
        Me.lblTotalPeriod.TabIndex = 13
        Me.lblTotalPeriod.Text = "00:00:00.158"
        Me.lblTotalPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Total time (period)"
        '
        'lblUserPeriod
        '
        Me.lblUserPeriod.AutoSize = True
        Me.lblUserPeriod.Location = New System.Drawing.Point(116, 109)
        Me.lblUserPeriod.Name = "lblUserPeriod"
        Me.lblUserPeriod.Size = New System.Drawing.Size(19, 13)
        Me.lblUserPeriod.TabIndex = 11
        Me.lblUserPeriod.Text = "00"
        Me.lblUserPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 109)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "User time (period)"
        '
        'lblPeriodKernel
        '
        Me.lblPeriodKernel.AutoSize = True
        Me.lblPeriodKernel.Location = New System.Drawing.Point(116, 91)
        Me.lblPeriodKernel.Name = "lblPeriodKernel"
        Me.lblPeriodKernel.Size = New System.Drawing.Size(19, 13)
        Me.lblPeriodKernel.TabIndex = 9
        Me.lblPeriodKernel.Text = "00"
        Me.lblPeriodKernel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 91)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(107, 13)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "Kernel time (period)"
        '
        'lblTotalTime
        '
        Me.lblTotalTime.AutoSize = True
        Me.lblTotalTime.Location = New System.Drawing.Point(114, 73)
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
        Me.lblUserTime.Location = New System.Drawing.Point(114, 54)
        Me.lblUserTime.Name = "lblUserTime"
        Me.lblUserTime.Size = New System.Drawing.Size(19, 13)
        Me.lblUserTime.TabIndex = 5
        Me.lblUserTime.Text = "00"
        Me.lblUserTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "User time"
        '
        'lblKernelTime
        '
        Me.lblKernelTime.AutoSize = True
        Me.lblKernelTime.Location = New System.Drawing.Point(114, 36)
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
        'lblAffinity
        '
        Me.lblAffinity.AutoSize = True
        Me.lblAffinity.Location = New System.Drawing.Point(114, 18)
        Me.lblAffinity.Name = "lblAffinity"
        Me.lblAffinity.Size = New System.Drawing.Size(19, 13)
        Me.lblAffinity.TabIndex = 1
        Me.lblAffinity.Text = "00"
        Me.lblAffinity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(44, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Affinity"
        '
        'pageLimitations
        '
        Me.pageLimitations.Controls.Add(Me.SplitContainer1)
        Me.pageLimitations.Location = New System.Drawing.Point(4, 22)
        Me.pageLimitations.Name = "pageLimitations"
        Me.pageLimitations.Padding = New System.Windows.Forms.Padding(3)
        Me.pageLimitations.Size = New System.Drawing.Size(695, 311)
        Me.pageLimitations.TabIndex = 5
        Me.pageLimitations.Text = "Limitations"
        Me.pageLimitations.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdSetLimits)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox3)
        Me.SplitContainer1.Size = New System.Drawing.Size(689, 305)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 20
        '
        'cmdSetLimits
        '
        Me.cmdSetLimits.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSetLimits.Location = New System.Drawing.Point(5, 1)
        Me.cmdSetLimits.Name = "cmdSetLimits"
        Me.cmdSetLimits.Size = New System.Drawing.Size(72, 23)
        Me.cmdSetLimits.TabIndex = 19
        Me.cmdSetLimits.Text = "Set limits..."
        Me.cmdSetLimits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSetLimits.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lvLimits)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(689, 276)
        Me.GroupBox3.TabIndex = 17
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Active limitations"
        '
        'lvLimits
        '
        Me.lvLimits.AllowColumnReorder = True
        Me.lvLimits.CatchErrors = False
        Me.lvLimits.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader11, Me.ColumnHeader13})
        CConnection2.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        Me.lvLimits.ConnectionObj = CConnection2
        Me.lvLimits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvLimits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvLimits.FullRowSelect = True
        ListViewGroup3.Header = "Processes"
        ListViewGroup3.Name = "gpOther"
        ListViewGroup4.Header = "Search result"
        ListViewGroup4.Name = "gpSearch"
        Me.lvLimits.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lvLimits.HideSelection = False
        Me.lvLimits.IsConnected = False
        Me.lvLimits.JobName = Nothing
        Me.lvLimits.Location = New System.Drawing.Point(3, 18)
        Me.lvLimits.Name = "lvLimits"
        Me.lvLimits.OverriddenDoubleBuffered = True
        Me.lvLimits.ReorganizeColumns = True
        Me.lvLimits.Size = New System.Drawing.Size(683, 255)
        Me.lvLimits.TabIndex = 4
        Me.lvLimits.UseCompatibleStateImageBehavior = False
        Me.lvLimits.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Limit"
        Me.ColumnHeader11.Width = 314
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Value"
        Me.ColumnHeader13.Width = 319
        '
        'Timer
        '
        Me.Timer.Enabled = True
        Me.Timer.Interval = 1000
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItemCopyLimit
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyLimit, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyLimit.Index = 0
        Me.MenuItemCopyLimit.Text = "Copy to clipboard"
        '
        'MenuItemProcKill
        '
        Me.MenuItemProcKill.DefaultItem = True
        Me.VistaMenu.SetImage(Me.MenuItemProcKill, Global.My.Resources.Resources.cross)
        Me.MenuItemProcKill.Index = 0
        Me.MenuItemProcKill.Text = "Kill"
        '
        'MenuItemProcStop
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcStop, Global.My.Resources.Resources.control_stop_square)
        Me.MenuItemProcStop.Index = 3
        Me.MenuItemProcStop.Text = "Stop"
        '
        'MenuItemProcResume
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcResume, Global.My.Resources.Resources.control)
        Me.MenuItemProcResume.Index = 4
        Me.MenuItemProcResume.Text = "Resume"
        '
        'MenuItemProcPIdle
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPIdle, Global.My.Resources.Resources.p0)
        Me.MenuItemProcPIdle.Index = 0
        Me.MenuItemProcPIdle.Text = "Idle"
        '
        'MenuItemProcPBN
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPBN, Global.My.Resources.Resources.p1)
        Me.MenuItemProcPBN.Index = 1
        Me.MenuItemProcPBN.Text = "Below normal"
        '
        'MenuItemProcPN
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPN, Global.My.Resources.Resources.p2)
        Me.MenuItemProcPN.Index = 2
        Me.MenuItemProcPN.Text = "Normal"
        '
        'MenuItemProcPAN
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPAN, Global.My.Resources.Resources.p3)
        Me.MenuItemProcPAN.Index = 3
        Me.MenuItemProcPAN.Text = "Above normal"
        '
        'MenuItemProcPH
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPH, Global.My.Resources.Resources.p4)
        Me.MenuItemProcPH.Index = 4
        Me.MenuItemProcPH.Text = "High"
        '
        'MenuItemProcPRT
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcPRT, Global.My.Resources.Resources.p6)
        Me.MenuItemProcPRT.Index = 5
        Me.MenuItemProcPRT.Text = "Real time"
        '
        'MenuItemProcSFileProp
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSFileProp, Global.My.Resources.Resources.document_text)
        Me.MenuItemProcSFileProp.Index = 9
        Me.MenuItemProcSFileProp.Text = "File properties"
        '
        'MenuItemProcSOpenDir
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSOpenDir, Global.My.Resources.Resources.folder_open)
        Me.MenuItemProcSOpenDir.Index = 10
        Me.MenuItemProcSOpenDir.Text = "Open directory"
        '
        'MenuItemProcSFileDetails
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSFileDetails, Global.My.Resources.Resources.magnifier)
        Me.MenuItemProcSFileDetails.Index = 11
        Me.MenuItemProcSFileDetails.Text = "File details"
        '
        'MenuItemProcSSearch
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSSearch, Global.My.Resources.Resources.globe)
        Me.MenuItemProcSSearch.Index = 12
        Me.MenuItemProcSSearch.Text = "Internet search"
        '
        'MenuItemProcSDep
        '
        Me.VistaMenu.SetImage(Me.MenuItemProcSDep, Global.My.Resources.Resources.dllIcon)
        Me.MenuItemProcSDep.Index = 13
        Me.MenuItemProcSDep.Text = "View dependencies..."
        '
        'MenuItemCopyProcess
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyProcess, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyProcess.Index = 15
        Me.MenuItemCopyProcess.Text = "Copy to clipboard"
        '
        'TimerLimits
        '
        Me.TimerLimits.Enabled = True
        Me.TimerLimits.Interval = 1000
        '
        'mnuLimit
        '
        Me.mnuLimit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyLimit})
        '
        'mnuProcess
        '
        Me.mnuProcess.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcKill, Me.MenuItemProcKillT, Me.MenuItemProcKillByMethod, Me.MenuItemProcStop, Me.MenuItemProcResume, Me.MenuItemProcPriority, Me.MenuItem44, Me.MenuItem35, Me.MenuItem51, Me.MenuItemProcSFileProp, Me.MenuItemProcSOpenDir, Me.MenuItemProcSFileDetails, Me.MenuItemProcSSearch, Me.MenuItemProcSDep, Me.MenuItem38, Me.MenuItemCopyProcess, Me.MenuItemProcColumns})
        '
        'MenuItemProcKillT
        '
        Me.MenuItemProcKillT.Index = 1
        Me.MenuItemProcKillT.Text = "Kill process tree"
        '
        'MenuItemProcKillByMethod
        '
        Me.MenuItemProcKillByMethod.Index = 2
        Me.MenuItemProcKillByMethod.Text = "Kill process by method..."
        '
        'MenuItemProcPriority
        '
        Me.MenuItemProcPriority.Index = 5
        Me.MenuItemProcPriority.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcPIdle, Me.MenuItemProcPBN, Me.MenuItemProcPN, Me.MenuItemProcPAN, Me.MenuItemProcPH, Me.MenuItemProcPRT})
        Me.MenuItemProcPriority.Text = "Priority"
        '
        'MenuItem44
        '
        Me.MenuItem44.Index = 6
        Me.MenuItem44.Text = "-"
        '
        'MenuItem35
        '
        Me.MenuItem35.Index = 7
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
        Me.MenuItem51.Index = 8
        Me.MenuItem51.Text = "-"
        '
        'MenuItem38
        '
        Me.MenuItem38.Index = 14
        Me.MenuItem38.Text = "-"
        '
        'MenuItemProcColumns
        '
        Me.MenuItemProcColumns.Index = 16
        Me.MenuItemProcColumns.Text = "Choose columns..."
        '
        'frmJobInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 337)
        Me.Controls.Add(Me.tabJob)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(660, 359)
        Me.Name = "frmJobInfo"
        Me.Text = "Job informations"
        Me.tabJob.ResumeLayout(False)
        Me.pageGeneral.ResumeLayout(False)
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.pageStats.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pageLimitations.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabJob As System.Windows.Forms.TabControl
    Friend WithEvents pageGeneral As System.Windows.Forms.TabPage
    Friend WithEvents pageLimitations As System.Windows.Forms.TabPage
    Friend WithEvents pageStats As System.Windows.Forms.TabPage
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalTerminatedProcesses As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblActiveProcesses As System.Windows.Forms.Label
    Friend WithEvents lbl789 As System.Windows.Forms.Label
    Friend WithEvents lblTotalProcesses As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTotalTime As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblUserTime As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblKernelTime As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblAffinity As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPeriod As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblUserPeriod As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblPeriodKernel As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblSchedulingClass As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblMaxWSS As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblMinWSS As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblPageFaultCount As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdTerminateJob As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lvProcess As processesInJobList
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
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdSetLimits As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lvLimits As jobLimitList
    Friend WithEvents TimerLimits As System.Windows.Forms.Timer
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Private WithEvents mnuLimit As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyLimit As System.Windows.Forms.MenuItem
    Private WithEvents mnuProcess As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemProcKill As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcKillT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcKillByMethod As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcStop As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcResume As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPriority As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPIdle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPBN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPAN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPH As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPRT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem44 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem35 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcWSS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcAff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcDump As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem51 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSDep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem38 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyProcess As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcColumns As System.Windows.Forms.MenuItem
End Class
