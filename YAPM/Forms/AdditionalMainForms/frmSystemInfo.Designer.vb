<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSystemInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemInfo))
        Me.timerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.mainSplit = New System.Windows.Forms.SplitContainer
        Me.chkOneGraphPerCpu = New System.Windows.Forms.CheckBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.lblKnpf = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.lblKnpa = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.lblKnpu = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.lblKpf = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.lblKpa = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.lblKpp = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.lblCPUdpcTime = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.lblCPUidleTime = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.lblCPUuserTime = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.lblCPUkernelTime = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.lblCPUinterruptTime = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.lblCPUcontextSwitches = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblCPUsystemCalls = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblCPUinterrupts = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.lblCPUprocessors = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.lblPFcache = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblPFdemandZero = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblPFcacheTransition = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.lblPFtransition = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.lblPFcopyOnWrite = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.lblPFtotal = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblIOotherBytes = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblIOothers = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblIOwriteBytes = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblIOwrites = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblIOreadBytes = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblIOreads = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lblPMT = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblPMU = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblPMF = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblCCL = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblCCP = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblCCC = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblHandles = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblThreads = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblProcesses = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblCacheErrors = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCacheMaximum = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblCacheMinimum = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblCachePeak = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblCacheCurrent = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.g2 = New Graph2
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.g3 = New Graph2
        Me.g4 = New Graph2
        Me.lblCPUTotalTime = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblCPUUsage = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.mainSplit.Panel1.SuspendLayout()
        Me.mainSplit.Panel2.SuspendLayout()
        Me.mainSplit.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.g2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.g3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.g4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timerRefresh
        '
        Me.timerRefresh.Enabled = True
        Me.timerRefresh.Interval = 1000
        '
        'mainSplit
        '
        Me.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.mainSplit.IsSplitterFixed = True
        Me.mainSplit.Location = New System.Drawing.Point(0, 0)
        Me.mainSplit.Name = "mainSplit"
        '
        'mainSplit.Panel1
        '
        Me.mainSplit.Panel1.Controls.Add(Me.chkOneGraphPerCpu)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox8)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox7)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox6)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox5)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox4)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox3)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox2)
        Me.mainSplit.Panel1.Controls.Add(Me.GroupBox1)
        '
        'mainSplit.Panel2
        '
        Me.mainSplit.Panel2.Controls.Add(Me.SplitContainer2)
        Me.mainSplit.Size = New System.Drawing.Size(735, 477)
        Me.mainSplit.SplitterDistance = 425
        Me.mainSplit.TabIndex = 11
        '
        'chkOneGraphPerCpu
        '
        Me.chkOneGraphPerCpu.AutoSize = True
        Me.chkOneGraphPerCpu.Location = New System.Drawing.Point(168, 448)
        Me.chkOneGraphPerCpu.Name = "chkOneGraphPerCpu"
        Me.chkOneGraphPerCpu.Size = New System.Drawing.Size(124, 17)
        Me.chkOneGraphPerCpu.TabIndex = 16
        Me.chkOneGraphPerCpu.Text = "One graph per cpu"
        Me.chkOneGraphPerCpu.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.lblKnpf)
        Me.GroupBox8.Controls.Add(Me.Label49)
        Me.GroupBox8.Controls.Add(Me.lblKnpa)
        Me.GroupBox8.Controls.Add(Me.Label35)
        Me.GroupBox8.Controls.Add(Me.lblKnpu)
        Me.GroupBox8.Controls.Add(Me.Label39)
        Me.GroupBox8.Controls.Add(Me.lblKpf)
        Me.GroupBox8.Controls.Add(Me.Label41)
        Me.GroupBox8.Controls.Add(Me.lblKpa)
        Me.GroupBox8.Controls.Add(Me.Label43)
        Me.GroupBox8.Controls.Add(Me.lblKpp)
        Me.GroupBox8.Controls.Add(Me.Label47)
        Me.GroupBox8.Location = New System.Drawing.Point(158, 317)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(259, 125)
        Me.GroupBox8.TabIndex = 15
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Kernel pools"
        '
        'lblKnpf
        '
        Me.lblKnpf.AutoSize = True
        Me.lblKnpf.Location = New System.Drawing.Point(121, 98)
        Me.lblKnpf.Name = "lblKnpf"
        Me.lblKnpf.Size = New System.Drawing.Size(13, 13)
        Me.lblKnpf.TabIndex = 13
        Me.lblKnpf.Text = "0"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(15, 98)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(94, 13)
        Me.Label49.TabIndex = 12
        Me.Label49.Text = "Non-paged frees"
        '
        'lblKnpa
        '
        Me.lblKnpa.AutoSize = True
        Me.lblKnpa.Location = New System.Drawing.Point(121, 83)
        Me.lblKnpa.Name = "lblKnpa"
        Me.lblKnpa.Size = New System.Drawing.Size(13, 13)
        Me.lblKnpa.TabIndex = 11
        Me.lblKnpa.Text = "0"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(15, 83)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(98, 13)
        Me.Label35.TabIndex = 10
        Me.Label35.Text = "Non-paged allocs"
        '
        'lblKnpu
        '
        Me.lblKnpu.AutoSize = True
        Me.lblKnpu.Location = New System.Drawing.Point(121, 68)
        Me.lblKnpu.Name = "lblKnpu"
        Me.lblKnpu.Size = New System.Drawing.Size(13, 13)
        Me.lblKnpu.TabIndex = 9
        Me.lblKnpu.Text = "0"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(15, 68)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(100, 13)
        Me.Label39.TabIndex = 8
        Me.Label39.Text = "Non-paged usage"
        '
        'lblKpf
        '
        Me.lblKpf.AutoSize = True
        Me.lblKpf.Location = New System.Drawing.Point(121, 53)
        Me.lblKpf.Name = "lblKpf"
        Me.lblKpf.Size = New System.Drawing.Size(13, 13)
        Me.lblKpf.TabIndex = 7
        Me.lblKpf.Text = "0"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(15, 53)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(67, 13)
        Me.Label41.TabIndex = 6
        Me.Label41.Text = "Paged frees"
        '
        'lblKpa
        '
        Me.lblKpa.AutoSize = True
        Me.lblKpa.Location = New System.Drawing.Point(121, 38)
        Me.lblKpa.Name = "lblKpa"
        Me.lblKpa.Size = New System.Drawing.Size(13, 13)
        Me.lblKpa.TabIndex = 5
        Me.lblKpa.Text = "0"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(15, 38)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(71, 13)
        Me.Label43.TabIndex = 4
        Me.Label43.Text = "Paged allocs"
        '
        'lblKpp
        '
        Me.lblKpp.AutoSize = True
        Me.lblKpp.Location = New System.Drawing.Point(121, 23)
        Me.lblKpp.Name = "lblKpp"
        Me.lblKpp.Size = New System.Drawing.Size(13, 13)
        Me.lblKpp.TabIndex = 1
        Me.lblKpp.Text = "0"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(15, 23)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(73, 13)
        Me.Label47.TabIndex = 0
        Me.Label47.Text = "Paged usage"
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblCPUUsage)
        Me.GroupBox7.Controls.Add(Me.Label40)
        Me.GroupBox7.Controls.Add(Me.lblCPUTotalTime)
        Me.GroupBox7.Controls.Add(Me.Label37)
        Me.GroupBox7.Controls.Add(Me.lblCPUdpcTime)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Controls.Add(Me.lblCPUidleTime)
        Me.GroupBox7.Controls.Add(Me.Label23)
        Me.GroupBox7.Controls.Add(Me.lblCPUuserTime)
        Me.GroupBox7.Controls.Add(Me.Label33)
        Me.GroupBox7.Controls.Add(Me.lblCPUkernelTime)
        Me.GroupBox7.Controls.Add(Me.Label36)
        Me.GroupBox7.Controls.Add(Me.lblCPUinterruptTime)
        Me.GroupBox7.Controls.Add(Me.Label38)
        Me.GroupBox7.Controls.Add(Me.lblCPUcontextSwitches)
        Me.GroupBox7.Controls.Add(Me.Label34)
        Me.GroupBox7.Controls.Add(Me.lblCPUsystemCalls)
        Me.GroupBox7.Controls.Add(Me.Label19)
        Me.GroupBox7.Controls.Add(Me.lblCPUinterrupts)
        Me.GroupBox7.Controls.Add(Me.Label27)
        Me.GroupBox7.Controls.Add(Me.lblCPUprocessors)
        Me.GroupBox7.Controls.Add(Me.Label31)
        Me.GroupBox7.Location = New System.Drawing.Point(158, 3)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(259, 177)
        Me.GroupBox7.TabIndex = 14
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "CPU"
        '
        'lblCPUdpcTime
        '
        Me.lblCPUdpcTime.AutoSize = True
        Me.lblCPUdpcTime.Location = New System.Drawing.Point(113, 143)
        Me.lblCPUdpcTime.Name = "lblCPUdpcTime"
        Me.lblCPUdpcTime.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUdpcTime.TabIndex = 17
        Me.lblCPUdpcTime.Text = "0"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(15, 143)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(53, 13)
        Me.Label29.TabIndex = 16
        Me.Label29.Text = "DPC time"
        '
        'lblCPUidleTime
        '
        Me.lblCPUidleTime.AutoSize = True
        Me.lblCPUidleTime.Location = New System.Drawing.Point(113, 128)
        Me.lblCPUidleTime.Name = "lblCPUidleTime"
        Me.lblCPUidleTime.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUidleTime.TabIndex = 15
        Me.lblCPUidleTime.Text = "0"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(15, 128)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(51, 13)
        Me.Label23.TabIndex = 14
        Me.Label23.Text = "Idle time"
        '
        'lblCPUuserTime
        '
        Me.lblCPUuserTime.AutoSize = True
        Me.lblCPUuserTime.Location = New System.Drawing.Point(113, 113)
        Me.lblCPUuserTime.Name = "lblCPUuserTime"
        Me.lblCPUuserTime.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUuserTime.TabIndex = 13
        Me.lblCPUuserTime.Text = "0"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(15, 113)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(55, 13)
        Me.Label33.TabIndex = 12
        Me.Label33.Text = "User time"
        '
        'lblCPUkernelTime
        '
        Me.lblCPUkernelTime.AutoSize = True
        Me.lblCPUkernelTime.Location = New System.Drawing.Point(113, 98)
        Me.lblCPUkernelTime.Name = "lblCPUkernelTime"
        Me.lblCPUkernelTime.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUkernelTime.TabIndex = 11
        Me.lblCPUkernelTime.Text = "0"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(15, 98)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(64, 13)
        Me.Label36.TabIndex = 10
        Me.Label36.Text = "Kernel time"
        '
        'lblCPUinterruptTime
        '
        Me.lblCPUinterruptTime.AutoSize = True
        Me.lblCPUinterruptTime.Location = New System.Drawing.Point(113, 83)
        Me.lblCPUinterruptTime.Name = "lblCPUinterruptTime"
        Me.lblCPUinterruptTime.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUinterruptTime.TabIndex = 9
        Me.lblCPUinterruptTime.Text = "0"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(15, 83)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(78, 13)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "Interrupt time"
        '
        'lblCPUcontextSwitches
        '
        Me.lblCPUcontextSwitches.AutoSize = True
        Me.lblCPUcontextSwitches.Location = New System.Drawing.Point(113, 68)
        Me.lblCPUcontextSwitches.Name = "lblCPUcontextSwitches"
        Me.lblCPUcontextSwitches.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUcontextSwitches.TabIndex = 7
        Me.lblCPUcontextSwitches.Text = "0"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(15, 68)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(94, 13)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "Context switches"
        '
        'lblCPUsystemCalls
        '
        Me.lblCPUsystemCalls.AutoSize = True
        Me.lblCPUsystemCalls.Location = New System.Drawing.Point(113, 53)
        Me.lblCPUsystemCalls.Name = "lblCPUsystemCalls"
        Me.lblCPUsystemCalls.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUsystemCalls.TabIndex = 5
        Me.lblCPUsystemCalls.Text = "0"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(15, 53)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(67, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "System calls"
        '
        'lblCPUinterrupts
        '
        Me.lblCPUinterrupts.AutoSize = True
        Me.lblCPUinterrupts.Location = New System.Drawing.Point(113, 38)
        Me.lblCPUinterrupts.Name = "lblCPUinterrupts"
        Me.lblCPUinterrupts.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUinterrupts.TabIndex = 3
        Me.lblCPUinterrupts.Text = "0"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(15, 38)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(58, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Interrupts"
        '
        'lblCPUprocessors
        '
        Me.lblCPUprocessors.AutoSize = True
        Me.lblCPUprocessors.Location = New System.Drawing.Point(113, 23)
        Me.lblCPUprocessors.Name = "lblCPUprocessors"
        Me.lblCPUprocessors.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUprocessors.TabIndex = 1
        Me.lblCPUprocessors.Text = "0"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(15, 23)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(61, 13)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Processors"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblPFcache)
        Me.GroupBox6.Controls.Add(Me.Label15)
        Me.GroupBox6.Controls.Add(Me.lblPFdemandZero)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.lblPFcacheTransition)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.lblPFtransition)
        Me.GroupBox6.Controls.Add(Me.Label28)
        Me.GroupBox6.Controls.Add(Me.lblPFcopyOnWrite)
        Me.GroupBox6.Controls.Add(Me.Label30)
        Me.GroupBox6.Controls.Add(Me.lblPFtotal)
        Me.GroupBox6.Controls.Add(Me.Label32)
        Me.GroupBox6.Location = New System.Drawing.Point(158, 186)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(259, 125)
        Me.GroupBox6.TabIndex = 13
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Page faults"
        '
        'lblPFcache
        '
        Me.lblPFcache.AutoSize = True
        Me.lblPFcache.Location = New System.Drawing.Point(121, 98)
        Me.lblPFcache.Name = "lblPFcache"
        Me.lblPFcache.Size = New System.Drawing.Size(13, 13)
        Me.lblPFcache.TabIndex = 11
        Me.lblPFcache.Text = "0"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(15, 98)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(38, 13)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Cache"
        '
        'lblPFdemandZero
        '
        Me.lblPFdemandZero.AutoSize = True
        Me.lblPFdemandZero.Location = New System.Drawing.Point(121, 83)
        Me.lblPFdemandZero.Name = "lblPFdemandZero"
        Me.lblPFdemandZero.Size = New System.Drawing.Size(13, 13)
        Me.lblPFdemandZero.TabIndex = 9
        Me.lblPFdemandZero.Text = "0"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(15, 83)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(81, 13)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "Demande zero"
        '
        'lblPFcacheTransition
        '
        Me.lblPFcacheTransition.AutoSize = True
        Me.lblPFcacheTransition.Location = New System.Drawing.Point(121, 68)
        Me.lblPFcacheTransition.Name = "lblPFcacheTransition"
        Me.lblPFcacheTransition.Size = New System.Drawing.Size(13, 13)
        Me.lblPFcacheTransition.TabIndex = 7
        Me.lblPFcacheTransition.Text = "0"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(15, 68)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(91, 13)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "Cache transition"
        '
        'lblPFtransition
        '
        Me.lblPFtransition.AutoSize = True
        Me.lblPFtransition.Location = New System.Drawing.Point(121, 53)
        Me.lblPFtransition.Name = "lblPFtransition"
        Me.lblPFtransition.Size = New System.Drawing.Size(13, 13)
        Me.lblPFtransition.TabIndex = 5
        Me.lblPFtransition.Text = "0"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(15, 53)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(58, 13)
        Me.Label28.TabIndex = 4
        Me.Label28.Text = "Transition"
        '
        'lblPFcopyOnWrite
        '
        Me.lblPFcopyOnWrite.AutoSize = True
        Me.lblPFcopyOnWrite.Location = New System.Drawing.Point(121, 38)
        Me.lblPFcopyOnWrite.Name = "lblPFcopyOnWrite"
        Me.lblPFcopyOnWrite.Size = New System.Drawing.Size(13, 13)
        Me.lblPFcopyOnWrite.TabIndex = 3
        Me.lblPFcopyOnWrite.Text = "0"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(15, 38)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(81, 13)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Copy-on-write"
        '
        'lblPFtotal
        '
        Me.lblPFtotal.AutoSize = True
        Me.lblPFtotal.Location = New System.Drawing.Point(121, 23)
        Me.lblPFtotal.Name = "lblPFtotal"
        Me.lblPFtotal.Size = New System.Drawing.Size(13, 13)
        Me.lblPFtotal.TabIndex = 1
        Me.lblPFtotal.Text = "0"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(15, 23)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(32, 13)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Total"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblIOotherBytes)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Controls.Add(Me.lblIOothers)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.lblIOwriteBytes)
        Me.GroupBox5.Controls.Add(Me.Label18)
        Me.GroupBox5.Controls.Add(Me.lblIOwrites)
        Me.GroupBox5.Controls.Add(Me.Label20)
        Me.GroupBox5.Controls.Add(Me.lblIOreadBytes)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.lblIOreads)
        Me.GroupBox5.Controls.Add(Me.Label24)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 349)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(149, 120)
        Me.GroupBox5.TabIndex = 12
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "I/O"
        '
        'lblIOotherBytes
        '
        Me.lblIOotherBytes.AutoSize = True
        Me.lblIOotherBytes.Location = New System.Drawing.Point(80, 98)
        Me.lblIOotherBytes.Name = "lblIOotherBytes"
        Me.lblIOotherBytes.Size = New System.Drawing.Size(13, 13)
        Me.lblIOotherBytes.TabIndex = 11
        Me.lblIOotherBytes.Text = "0"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(15, 98)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(67, 13)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "Other bytes"
        '
        'lblIOothers
        '
        Me.lblIOothers.AutoSize = True
        Me.lblIOothers.Location = New System.Drawing.Point(80, 83)
        Me.lblIOothers.Name = "lblIOothers"
        Me.lblIOothers.Size = New System.Drawing.Size(13, 13)
        Me.lblIOothers.TabIndex = 9
        Me.lblIOothers.Text = "0"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 83)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Others"
        '
        'lblIOwriteBytes
        '
        Me.lblIOwriteBytes.AutoSize = True
        Me.lblIOwriteBytes.Location = New System.Drawing.Point(80, 68)
        Me.lblIOwriteBytes.Name = "lblIOwriteBytes"
        Me.lblIOwriteBytes.Size = New System.Drawing.Size(13, 13)
        Me.lblIOwriteBytes.TabIndex = 7
        Me.lblIOwriteBytes.Text = "0"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(15, 68)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 13)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "Write bytes"
        '
        'lblIOwrites
        '
        Me.lblIOwrites.AutoSize = True
        Me.lblIOwrites.Location = New System.Drawing.Point(80, 53)
        Me.lblIOwrites.Name = "lblIOwrites"
        Me.lblIOwrites.Size = New System.Drawing.Size(13, 13)
        Me.lblIOwrites.TabIndex = 5
        Me.lblIOwrites.Text = "0"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(15, 53)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(40, 13)
        Me.Label20.TabIndex = 4
        Me.Label20.Text = "Writes"
        '
        'lblIOreadBytes
        '
        Me.lblIOreadBytes.AutoSize = True
        Me.lblIOreadBytes.Location = New System.Drawing.Point(80, 38)
        Me.lblIOreadBytes.Name = "lblIOreadBytes"
        Me.lblIOreadBytes.Size = New System.Drawing.Size(13, 13)
        Me.lblIOreadBytes.TabIndex = 3
        Me.lblIOreadBytes.Text = "0"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(15, 38)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(63, 13)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "Read bytes"
        '
        'lblIOreads
        '
        Me.lblIOreads.AutoSize = True
        Me.lblIOreads.Location = New System.Drawing.Point(80, 23)
        Me.lblIOreads.Name = "lblIOreads"
        Me.lblIOreads.Size = New System.Drawing.Size(13, 13)
        Me.lblIOreads.TabIndex = 1
        Me.lblIOreads.Text = "0"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(15, 23)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(38, 13)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "Reads"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lblPMT)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.lblPMU)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.lblPMF)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 161)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(149, 73)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Physical memory"
        '
        'lblPMT
        '
        Me.lblPMT.AutoSize = True
        Me.lblPMT.Location = New System.Drawing.Point(70, 53)
        Me.lblPMT.Name = "lblPMT"
        Me.lblPMT.Size = New System.Drawing.Size(13, 13)
        Me.lblPMT.TabIndex = 5
        Me.lblPMT.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Total"
        '
        'lblPMU
        '
        Me.lblPMU.AutoSize = True
        Me.lblPMU.Location = New System.Drawing.Point(70, 38)
        Me.lblPMU.Name = "lblPMU"
        Me.lblPMU.Size = New System.Drawing.Size(13, 13)
        Me.lblPMU.TabIndex = 3
        Me.lblPMU.Text = "0"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(15, 38)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(33, 13)
        Me.Label13.TabIndex = 2
        Me.Label13.Text = "Used"
        '
        'lblPMF
        '
        Me.lblPMF.AutoSize = True
        Me.lblPMF.Location = New System.Drawing.Point(70, 23)
        Me.lblPMF.Name = "lblPMF"
        Me.lblPMF.Size = New System.Drawing.Size(13, 13)
        Me.lblPMF.TabIndex = 1
        Me.lblPMF.Text = "0"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(15, 23)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(29, 13)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Free"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblCCL)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lblCCP)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblCCC)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 82)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(149, 73)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Commit charge"
        '
        'lblCCL
        '
        Me.lblCCL.AutoSize = True
        Me.lblCCL.Location = New System.Drawing.Point(70, 53)
        Me.lblCCL.Name = "lblCCL"
        Me.lblCCL.Size = New System.Drawing.Size(13, 13)
        Me.lblCCL.TabIndex = 5
        Me.lblCCL.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Limit"
        '
        'lblCCP
        '
        Me.lblCCP.AutoSize = True
        Me.lblCCP.Location = New System.Drawing.Point(70, 38)
        Me.lblCCP.Name = "lblCCP"
        Me.lblCCP.Size = New System.Drawing.Size(13, 13)
        Me.lblCCP.TabIndex = 3
        Me.lblCCP.Text = "0"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(31, 13)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Peak"
        '
        'lblCCC
        '
        Me.lblCCC.AutoSize = True
        Me.lblCCC.Location = New System.Drawing.Point(70, 23)
        Me.lblCCC.Name = "lblCCC"
        Me.lblCCC.Size = New System.Drawing.Size(13, 13)
        Me.lblCCC.TabIndex = 1
        Me.lblCCC.Text = "0"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(46, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Current"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblHandles)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.lblThreads)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.lblProcesses)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(149, 73)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Total"
        '
        'lblHandles
        '
        Me.lblHandles.AutoSize = True
        Me.lblHandles.Location = New System.Drawing.Point(80, 53)
        Me.lblHandles.Name = "lblHandles"
        Me.lblHandles.Size = New System.Drawing.Size(13, 13)
        Me.lblHandles.TabIndex = 5
        Me.lblHandles.Text = "0"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 53)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Handles"
        '
        'lblThreads
        '
        Me.lblThreads.AutoSize = True
        Me.lblThreads.Location = New System.Drawing.Point(80, 38)
        Me.lblThreads.Name = "lblThreads"
        Me.lblThreads.Size = New System.Drawing.Size(13, 13)
        Me.lblThreads.TabIndex = 3
        Me.lblThreads.Text = "0"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(15, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(47, 13)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Threads"
        '
        'lblProcesses
        '
        Me.lblProcesses.AutoSize = True
        Me.lblProcesses.Location = New System.Drawing.Point(80, 23)
        Me.lblProcesses.Name = "lblProcesses"
        Me.lblProcesses.Size = New System.Drawing.Size(13, 13)
        Me.lblProcesses.TabIndex = 1
        Me.lblProcesses.Text = "0"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(15, 23)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(56, 13)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Processes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblCacheErrors)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblCacheMaximum)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblCacheMinimum)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblCachePeak)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblCacheCurrent)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 240)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(149, 103)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cache"
        '
        'lblCacheErrors
        '
        Me.lblCacheErrors.AutoSize = True
        Me.lblCacheErrors.Location = New System.Drawing.Point(80, 83)
        Me.lblCacheErrors.Name = "lblCacheErrors"
        Me.lblCacheErrors.Size = New System.Drawing.Size(13, 13)
        Me.lblCacheErrors.TabIndex = 9
        Me.lblCacheErrors.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Faults"
        '
        'lblCacheMaximum
        '
        Me.lblCacheMaximum.AutoSize = True
        Me.lblCacheMaximum.Location = New System.Drawing.Point(80, 68)
        Me.lblCacheMaximum.Name = "lblCacheMaximum"
        Me.lblCacheMaximum.Size = New System.Drawing.Size(13, 13)
        Me.lblCacheMaximum.TabIndex = 7
        Me.lblCacheMaximum.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Maximum"
        '
        'lblCacheMinimum
        '
        Me.lblCacheMinimum.AutoSize = True
        Me.lblCacheMinimum.Location = New System.Drawing.Point(80, 53)
        Me.lblCacheMinimum.Name = "lblCacheMinimum"
        Me.lblCacheMinimum.Size = New System.Drawing.Size(13, 13)
        Me.lblCacheMinimum.TabIndex = 5
        Me.lblCacheMinimum.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Minimum"
        '
        'lblCachePeak
        '
        Me.lblCachePeak.AutoSize = True
        Me.lblCachePeak.Location = New System.Drawing.Point(80, 38)
        Me.lblCachePeak.Name = "lblCachePeak"
        Me.lblCachePeak.Size = New System.Drawing.Size(13, 13)
        Me.lblCachePeak.TabIndex = 3
        Me.lblCachePeak.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Peak"
        '
        'lblCacheCurrent
        '
        Me.lblCacheCurrent.AutoSize = True
        Me.lblCacheCurrent.Location = New System.Drawing.Point(80, 23)
        Me.lblCacheCurrent.Name = "lblCacheCurrent"
        Me.lblCacheCurrent.Size = New System.Drawing.Size(13, 13)
        Me.lblCacheCurrent.TabIndex = 1
        Me.lblCacheCurrent.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Current"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer2.Size = New System.Drawing.Size(306, 477)
        Me.SplitContainer2.SplitterDistance = 239
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.g2)
        Me.SplitContainer1.Size = New System.Drawing.Size(306, 239)
        Me.SplitContainer1.SplitterDistance = 119
        Me.SplitContainer1.TabIndex = 1
        '
        'g2
        '
        Me.g2.BackColor = System.Drawing.Color.Black
        Me.g2.Color2 = System.Drawing.Color.Olive
        Me.g2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.g2.EnableGraph = True
        Me.g2.Fixedheight = True
        Me.g2.GridStep = 10
        Me.g2.Location = New System.Drawing.Point(0, 0)
        Me.g2.Name = "g2"
        Me.g2.ShowSecondGraph = False
        Me.g2.Size = New System.Drawing.Size(306, 116)
        Me.g2.TabIndex = 11
        Me.g2.TabStop = False
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.g3)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.g4)
        Me.SplitContainer3.Size = New System.Drawing.Size(306, 234)
        Me.SplitContainer3.SplitterDistance = 117
        Me.SplitContainer3.TabIndex = 0
        '
        'g3
        '
        Me.g3.BackColor = System.Drawing.Color.Black
        Me.g3.Color = System.Drawing.Color.Red
        Me.g3.Color2 = System.Drawing.Color.Maroon
        Me.g3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.g3.EnableGraph = True
        Me.g3.Fixedheight = False
        Me.g3.GridStep = 10
        Me.g3.Location = New System.Drawing.Point(0, 0)
        Me.g3.Name = "g3"
        Me.g3.ShowSecondGraph = False
        Me.g3.Size = New System.Drawing.Size(306, 117)
        Me.g3.TabIndex = 10
        Me.g3.TabStop = False
        '
        'g4
        '
        Me.g4.BackColor = System.Drawing.Color.Black
        Me.g4.Color = System.Drawing.Color.Red
        Me.g4.Color2 = System.Drawing.Color.Maroon
        Me.g4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.g4.EnableGraph = True
        Me.g4.Fixedheight = False
        Me.g4.GridStep = 10
        Me.g4.Location = New System.Drawing.Point(0, 0)
        Me.g4.Name = "g4"
        Me.g4.ShowSecondGraph = False
        Me.g4.Size = New System.Drawing.Size(306, 113)
        Me.g4.TabIndex = 11
        Me.g4.TabStop = False
        '
        'lblCPUTotalTime
        '
        Me.lblCPUTotalTime.AutoSize = True
        Me.lblCPUTotalTime.Location = New System.Drawing.Point(113, 158)
        Me.lblCPUTotalTime.Name = "lblCPUTotalTime"
        Me.lblCPUTotalTime.Size = New System.Drawing.Size(13, 13)
        Me.lblCPUTotalTime.TabIndex = 19
        Me.lblCPUTotalTime.Text = "0"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(15, 158)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(57, 13)
        Me.Label37.TabIndex = 18
        Me.Label37.Text = "Total time"
        '
        'lblCPUUsage
        '
        Me.lblCPUUsage.AutoSize = True
        Me.lblCPUUsage.Location = New System.Drawing.Point(188, 23)
        Me.lblCPUUsage.Name = "lblCPUUsage"
        Me.lblCPUUsage.Size = New System.Drawing.Size(52, 13)
        Me.lblCPUUsage.TabIndex = 21
        Me.lblCPUUsage.Text = "00,000 %"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(143, 23)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(39, 13)
        Me.Label40.TabIndex = 20
        Me.Label40.Text = "Usage"
        '
        'frmSystemInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 477)
        Me.Controls.Add(Me.mainSplit)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(656, 513)
        Me.Name = "frmSystemInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "System information"
        Me.mainSplit.Panel1.ResumeLayout(False)
        Me.mainSplit.Panel1.PerformLayout()
        Me.mainSplit.Panel2.ResumeLayout(False)
        Me.mainSplit.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.g2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.g3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.g4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timerRefresh As System.Windows.Forms.Timer
    Friend WithEvents mainSplit As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents lblKnpf As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents lblKnpa As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents lblKnpu As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents lblKpf As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblKpa As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblKpp As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCPUdpcTime As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblCPUidleTime As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblCPUuserTime As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblCPUkernelTime As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblCPUinterruptTime As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblCPUcontextSwitches As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblCPUsystemCalls As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblCPUinterrupts As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblCPUprocessors As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPFcache As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblPFdemandZero As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblPFcacheTransition As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblPFtransition As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblPFcopyOnWrite As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblPFtotal As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblIOotherBytes As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblIOothers As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblIOwriteBytes As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblIOwrites As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblIOreadBytes As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblIOreads As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lblPMT As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblPMU As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblPMF As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCCL As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCCP As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblCCC As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHandles As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblThreads As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblProcesses As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCacheErrors As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCacheMaximum As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblCacheMinimum As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblCachePeak As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCacheCurrent As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents g2 As Graph2
    Friend WithEvents g3 As Graph2
    Friend WithEvents g4 As Graph2
    Friend WithEvents chkOneGraphPerCpu As System.Windows.Forms.CheckBox
    Friend WithEvents lblCPUTotalTime As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblCPUUsage As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
End Class
