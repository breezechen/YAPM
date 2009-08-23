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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobInfo))
        Dim CConnection1 As YAPM.cConnection = New YAPM.cConnection
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Me.tabProcess = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.cmdAddProcess = New System.Windows.Forms.Button
        Me.cmdTerminateJob = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.gpProcGeneralFile = New System.Windows.Forms.GroupBox
        Me.lblCreationDate = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblJobId = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
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
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.imgProcessTab = New System.Windows.Forms.ImageList(Me.components)
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.lvProcess = New YAPM.processesInJobList
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
        Me.tabProcess.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gpProcGeneralFile.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabProcess
        '
        Me.tabProcess.Controls.Add(Me.TabPage1)
        Me.tabProcess.Controls.Add(Me.TabPage2)
        Me.tabProcess.Controls.Add(Me.TabPage6)
        Me.tabProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabProcess.ImageList = Me.imgProcessTab
        Me.tabProcess.Location = New System.Drawing.Point(0, 0)
        Me.tabProcess.Multiline = True
        Me.tabProcess.Name = "tabProcess"
        Me.tabProcess.SelectedIndex = 0
        Me.tabProcess.Size = New System.Drawing.Size(655, 323)
        Me.tabProcess.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cmdAddProcess)
        Me.TabPage1.Controls.Add(Me.cmdTerminateJob)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.gpProcGeneralFile)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(647, 296)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'cmdAddProcess
        '
        Me.cmdAddProcess.Image = Global.YAPM.My.Resources.Resources.plus_circle
        Me.cmdAddProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdAddProcess.Location = New System.Drawing.Point(234, 48)
        Me.cmdAddProcess.Name = "cmdAddProcess"
        Me.cmdAddProcess.Size = New System.Drawing.Size(131, 23)
        Me.cmdAddProcess.TabIndex = 18
        Me.cmdAddProcess.Text = "Add process to job"
        Me.cmdAddProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdAddProcess.UseVisualStyleBackColor = True
        '
        'cmdTerminateJob
        '
        Me.cmdTerminateJob.Image = Global.YAPM.My.Resources.Resources.cross
        Me.cmdTerminateJob.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdTerminateJob.Location = New System.Drawing.Point(234, 19)
        Me.cmdTerminateJob.Name = "cmdTerminateJob"
        Me.cmdTerminateJob.Size = New System.Drawing.Size(105, 23)
        Me.cmdTerminateJob.TabIndex = 17
        Me.cmdTerminateJob.Text = "Terminate job"
        Me.cmdTerminateJob.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdTerminateJob.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lvProcess)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 90)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(631, 198)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Processes in job"
        '
        'gpProcGeneralFile
        '
        Me.gpProcGeneralFile.Controls.Add(Me.lblCreationDate)
        Me.gpProcGeneralFile.Controls.Add(Me.Label3)
        Me.gpProcGeneralFile.Controls.Add(Me.lblJobId)
        Me.gpProcGeneralFile.Controls.Add(Me.Label2)
        Me.gpProcGeneralFile.Location = New System.Drawing.Point(6, 6)
        Me.gpProcGeneralFile.Name = "gpProcGeneralFile"
        Me.gpProcGeneralFile.Size = New System.Drawing.Size(213, 78)
        Me.gpProcGeneralFile.TabIndex = 15
        Me.gpProcGeneralFile.TabStop = False
        Me.gpProcGeneralFile.Text = "Main infos"
        '
        'lblCreationDate
        '
        Me.lblCreationDate.AutoSize = True
        Me.lblCreationDate.Location = New System.Drawing.Point(103, 42)
        Me.lblCreationDate.Name = "lblCreationDate"
        Me.lblCreationDate.Size = New System.Drawing.Size(19, 13)
        Me.lblCreationDate.TabIndex = 27
        Me.lblCreationDate.Text = "00"
        Me.lblCreationDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Creation date"
        '
        'lblJobId
        '
        Me.lblJobId.AutoSize = True
        Me.lblJobId.Location = New System.Drawing.Point(103, 23)
        Me.lblJobId.Name = "lblJobId"
        Me.lblJobId.Size = New System.Drawing.Size(19, 13)
        Me.lblJobId.TabIndex = 25
        Me.lblJobId.Text = "00"
        Me.lblJobId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Job ID"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(647, 296)
        Me.TabPage2.TabIndex = 8
        Me.TabPage2.Text = "Statistics"
        Me.TabPage2.UseVisualStyleBackColor = True
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
        Me.GroupBox5.Size = New System.Drawing.Size(200, 173)
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
        'TabPage6
        '
        Me.TabPage6.Location = New System.Drawing.Point(4, 23)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(647, 296)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Limitations"
        Me.TabPage6.UseVisualStyleBackColor = True
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
        'imgProcess
        '
        Me.imgProcess.ImageStream = CType(resources.GetObject("imgProcess.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgProcess.TransparentColor = System.Drawing.Color.Transparent
        Me.imgProcess.Images.SetKeyName(0, "noIcon")
        '
        'imgMain
        '
        Me.imgMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imgMain.ImageSize = New System.Drawing.Size(16, 16)
        Me.imgMain.TransparentColor = System.Drawing.Color.Transparent
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
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.CatchErrors = False
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c7, Me.c8, Me.c9, Me.c10, Me.ColumnHeader20})
        CConnection1.ConnectionType = YAPM.cConnection.TypeOfConnection.LocalConnection
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
        Me.lvProcess.Location = New System.Drawing.Point(3, 18)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.ReorganizeColumns = True
        Me.lvProcess.Size = New System.Drawing.Size(625, 177)
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
        'frmJobInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 323)
        Me.Controls.Add(Me.tabProcess)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(660, 359)
        Me.Name = "frmJobInfo"
        Me.Text = "Service informations"
        Me.tabProcess.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.gpProcGeneralFile.ResumeLayout(False)
        Me.gpProcGeneralFile.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabProcess As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents gpProcGeneralFile As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents imgProcess As System.Windows.Forms.ImageList
    Friend WithEvents imgProcessTab As System.Windows.Forms.ImageList
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCreationDate As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblJobId As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdAddProcess As System.Windows.Forms.Button
    Friend WithEvents cmdTerminateJob As System.Windows.Forms.Button
    Friend WithEvents lvProcess As YAPM.processesInJobList
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
End Class
