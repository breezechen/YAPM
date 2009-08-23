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
        Me.tabProcess = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.cmdResume = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbStart = New System.Windows.Forms.ComboBox
        Me.cmdSetStartType = New System.Windows.Forms.Button
        Me.cmdStop = New System.Windows.Forms.Button
        Me.cmdPause = New System.Windows.Forms.Button
        Me.cmdStart = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.SplitContainerOnlineInfo = New System.Windows.Forms.SplitContainer
        Me.lblSecurityRisk = New System.Windows.Forms.Label
        Me.cmdGetOnlineInfos = New System.Windows.Forms.Button
        Me.rtbOnlineInfos = New System.Windows.Forms.RichTextBox
        Me.gpProcGeneralFile = New System.Windows.Forms.GroupBox
        Me.cmdInspectExe = New System.Windows.Forms.Button
        Me.cmdShowFileDetails = New System.Windows.Forms.Button
        Me.cmdShowFileProperties = New System.Windows.Forms.Button
        Me.cmdOpenDirectory = New System.Windows.Forms.Button
        Me.txtServicePath = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtImageVersion = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.pctSmallIcon = New System.Windows.Forms.PictureBox
        Me.pctBigIcon = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCommand = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.cmdGoProcess = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtProcess = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtStartType = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtType = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtState = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.imgProcessTab = New System.Windows.Forms.ImageList(Me.components)
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.imgProcess = New System.Windows.Forms.ImageList(Me.components)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.menuCopyPctbig = New System.Windows.Forms.ContextMenu
        Me.MenuItemCopyBig = New System.Windows.Forms.MenuItem
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.MenuItemCopySmall = New System.Windows.Forms.MenuItem
        Me.menuCopyPctSmall = New System.Windows.Forms.ContextMenu
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
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
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblKernelTime = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblPriority = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.tabProcess.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SplitContainerOnlineInfo.Panel1.SuspendLayout()
        Me.SplitContainerOnlineInfo.Panel2.SuspendLayout()
        Me.SplitContainerOnlineInfo.SuspendLayout()
        Me.gpProcGeneralFile.SuspendLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.gpProcGeneralFile)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(647, 296)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.cmdResume)
        Me.GroupBox7.Controls.Add(Me.Label2)
        Me.GroupBox7.Controls.Add(Me.cbStart)
        Me.GroupBox7.Controls.Add(Me.cmdSetStartType)
        Me.GroupBox7.Controls.Add(Me.cmdStop)
        Me.GroupBox7.Controls.Add(Me.cmdPause)
        Me.GroupBox7.Controls.Add(Me.cmdStart)
        Me.GroupBox7.Location = New System.Drawing.Point(391, 155)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(242, 116)
        Me.GroupBox7.TabIndex = 18
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Actions"
        '
        'cmdResume
        '
        Me.cmdResume.Enabled = False
        Me.cmdResume.Image = Global.YAPM.My.Resources.Resources.control
        Me.cmdResume.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdResume.Location = New System.Drawing.Point(94, 21)
        Me.cmdResume.Name = "cmdResume"
        Me.cmdResume.Size = New System.Drawing.Size(66, 23)
        Me.cmdResume.TabIndex = 10
        Me.cmdResume.Text = "Resume"
        Me.cmdResume.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdResume.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Start type"
        '
        'cbStart
        '
        Me.cbStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStart.FormattingEnabled = True
        Me.cbStart.Items.AddRange(New Object() {"BootStart", "SystemStart", "AutoStart", "DemandStart", "StartDisabled"})
        Me.cbStart.Location = New System.Drawing.Point(72, 79)
        Me.cbStart.Name = "cbStart"
        Me.cbStart.Size = New System.Drawing.Size(112, 21)
        Me.cbStart.TabIndex = 8
        '
        'cmdSetStartType
        '
        Me.cmdSetStartType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSetStartType.Location = New System.Drawing.Point(193, 79)
        Me.cmdSetStartType.Name = "cmdSetStartType"
        Me.cmdSetStartType.Size = New System.Drawing.Size(37, 23)
        Me.cmdSetStartType.TabIndex = 7
        Me.cmdSetStartType.Text = "Set"
        Me.cmdSetStartType.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Enabled = False
        Me.cmdStop.Image = Global.YAPM.My.Resources.Resources.control_stop_square
        Me.cmdStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdStop.Location = New System.Drawing.Point(13, 50)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(75, 23)
        Me.cmdStop.TabIndex = 5
        Me.cmdStop.Text = "Stop     "
        Me.cmdStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'cmdPause
        '
        Me.cmdPause.Enabled = False
        Me.cmdPause.Image = Global.YAPM.My.Resources.Resources.control_pause
        Me.cmdPause.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPause.Location = New System.Drawing.Point(13, 21)
        Me.cmdPause.Name = "cmdPause"
        Me.cmdPause.Size = New System.Drawing.Size(75, 23)
        Me.cmdPause.TabIndex = 4
        Me.cmdPause.Text = "Pause   "
        Me.cmdPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdPause.UseVisualStyleBackColor = True
        '
        'cmdStart
        '
        Me.cmdStart.Enabled = False
        Me.cmdStart.Image = Global.YAPM.My.Resources.Resources.control
        Me.cmdStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdStart.Location = New System.Drawing.Point(94, 50)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.Size = New System.Drawing.Size(66, 23)
        Me.cmdStart.TabIndex = 3
        Me.cmdStart.Text = "Start   "
        Me.cmdStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdStart.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.SplitContainerOnlineInfo)
        Me.GroupBox6.Location = New System.Drawing.Point(391, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(242, 143)
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
        Me.SplitContainerOnlineInfo.Size = New System.Drawing.Size(236, 122)
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
        Me.rtbOnlineInfos.Size = New System.Drawing.Size(236, 93)
        Me.rtbOnlineInfos.TabIndex = 12
        Me.rtbOnlineInfos.Text = ""
        '
        'gpProcGeneralFile
        '
        Me.gpProcGeneralFile.Controls.Add(Me.cmdInspectExe)
        Me.gpProcGeneralFile.Controls.Add(Me.cmdShowFileDetails)
        Me.gpProcGeneralFile.Controls.Add(Me.cmdShowFileProperties)
        Me.gpProcGeneralFile.Controls.Add(Me.cmdOpenDirectory)
        Me.gpProcGeneralFile.Controls.Add(Me.txtServicePath)
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
        Me.cmdInspectExe.Image = Global.YAPM.My.Resources.Resources.dllIcon
        Me.cmdInspectExe.Location = New System.Drawing.Point(266, 81)
        Me.cmdInspectExe.Name = "cmdInspectExe"
        Me.cmdInspectExe.Size = New System.Drawing.Size(26, 26)
        Me.cmdInspectExe.TabIndex = 19
        Me.cmdInspectExe.UseVisualStyleBackColor = True
        '
        'cmdShowFileDetails
        '
        Me.cmdShowFileDetails.Enabled = False
        Me.cmdShowFileDetails.Image = Global.YAPM.My.Resources.Resources.magnifier
        Me.cmdShowFileDetails.Location = New System.Drawing.Point(292, 81)
        Me.cmdShowFileDetails.Name = "cmdShowFileDetails"
        Me.cmdShowFileDetails.Size = New System.Drawing.Size(26, 26)
        Me.cmdShowFileDetails.TabIndex = 3
        Me.cmdShowFileDetails.UseVisualStyleBackColor = True
        '
        'cmdShowFileProperties
        '
        Me.cmdShowFileProperties.Enabled = False
        Me.cmdShowFileProperties.Image = Global.YAPM.My.Resources.Resources.document_text
        Me.cmdShowFileProperties.Location = New System.Drawing.Point(318, 81)
        Me.cmdShowFileProperties.Name = "cmdShowFileProperties"
        Me.cmdShowFileProperties.Size = New System.Drawing.Size(26, 26)
        Me.cmdShowFileProperties.TabIndex = 4
        Me.cmdShowFileProperties.UseVisualStyleBackColor = True
        '
        'cmdOpenDirectory
        '
        Me.cmdOpenDirectory.Enabled = False
        Me.cmdOpenDirectory.Image = Global.YAPM.My.Resources.Resources.folder_open_document
        Me.cmdOpenDirectory.Location = New System.Drawing.Point(344, 81)
        Me.cmdOpenDirectory.Name = "cmdOpenDirectory"
        Me.cmdOpenDirectory.Size = New System.Drawing.Size(26, 26)
        Me.cmdOpenDirectory.TabIndex = 5
        Me.cmdOpenDirectory.UseVisualStyleBackColor = True
        '
        'txtServicePath
        '
        Me.txtServicePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtServicePath.Location = New System.Drawing.Point(85, 82)
        Me.txtServicePath.Name = "txtServicePath"
        Me.txtServicePath.ReadOnly = True
        Me.txtServicePath.Size = New System.Drawing.Size(175, 22)
        Me.txtServicePath.TabIndex = 2
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCommand)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.cmdGoProcess)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtProcess)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtStartType)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtType)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.txtState)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 129)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(376, 171)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Service"
        '
        'txtCommand
        '
        Me.txtCommand.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCommand.Location = New System.Drawing.Point(85, 44)
        Me.txtCommand.Name = "txtCommand"
        Me.txtCommand.ReadOnly = True
        Me.txtCommand.Size = New System.Drawing.Size(282, 22)
        Me.txtCommand.TabIndex = 25
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(2, 47)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(59, 13)
        Me.Label22.TabIndex = 26
        Me.Label22.Text = "Command"
        '
        'cmdGoProcess
        '
        Me.cmdGoProcess.Enabled = False
        Me.cmdGoProcess.Image = Global.YAPM.My.Resources.Resources.down
        Me.cmdGoProcess.Location = New System.Drawing.Point(341, 136)
        Me.cmdGoProcess.Name = "cmdGoProcess"
        Me.cmdGoProcess.Size = New System.Drawing.Size(26, 26)
        Me.cmdGoProcess.TabIndex = 24
        Me.cmdGoProcess.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtName.Location = New System.Drawing.Point(85, 21)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(282, 22)
        Me.txtName.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(2, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(77, 13)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Display name"
        '
        'txtProcess
        '
        Me.txtProcess.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProcess.Location = New System.Drawing.Point(85, 136)
        Me.txtProcess.Name = "txtProcess"
        Me.txtProcess.ReadOnly = True
        Me.txtProcess.Size = New System.Drawing.Size(250, 22)
        Me.txtProcess.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Process"
        '
        'txtStartType
        '
        Me.txtStartType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStartType.Location = New System.Drawing.Point(85, 112)
        Me.txtStartType.Name = "txtStartType"
        Me.txtStartType.ReadOnly = True
        Me.txtStartType.Size = New System.Drawing.Size(282, 22)
        Me.txtStartType.TabIndex = 9
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(2, 115)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 13)
        Me.Label17.TabIndex = 21
        Me.Label17.Text = "Start type"
        '
        'txtType
        '
        Me.txtType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtType.Location = New System.Drawing.Point(85, 89)
        Me.txtType.Name = "txtType"
        Me.txtType.ReadOnly = True
        Me.txtType.Size = New System.Drawing.Size(282, 22)
        Me.txtType.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(2, 92)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(30, 13)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "Type"
        '
        'txtState
        '
        Me.txtState.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtState.Location = New System.Drawing.Point(85, 67)
        Me.txtState.Name = "txtState"
        Me.txtState.ReadOnly = True
        Me.txtState.Size = New System.Drawing.Size(282, 22)
        Me.txtState.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(2, 70)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(33, 13)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "State"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(647, 296)
        Me.TabPage2.TabIndex = 8
        Me.TabPage2.Text = "Statistics"
        Me.TabPage2.UseVisualStyleBackColor = True
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
        'menuCopyPctbig
        '
        Me.menuCopyPctbig.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyBig})
        '
        'MenuItemCopyBig
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyBig, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyBig.Index = 0
        Me.MenuItemCopyBig.Text = "Copy to clipboard"
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'MenuItemCopySmall
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopySmall, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopySmall.Index = 0
        Me.MenuItemCopySmall.Text = "Copy to clipboard"
        '
        'menuCopyPctSmall
        '
        Me.menuCopyPctSmall.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopySmall})
        '
        'mainMenu
        '
        Me.mainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4})
        Me.MenuItem3.Text = "main"
        Me.MenuItem3.Visible = False
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.MenuItem4.Text = "Refresh"
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
        Me.GroupBox5.Location = New System.Drawing.Point(438, 151)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(180, 122)
        Me.GroupBox5.TabIndex = 7
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
        Me.GroupBox4.Location = New System.Drawing.Point(256, 24)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(176, 249)
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
        Me.GroupBox3.Location = New System.Drawing.Point(28, 24)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(222, 249)
        Me.GroupBox3.TabIndex = 5
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
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.lblKernelTime)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.lblPriority)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Location = New System.Drawing.Point(438, 24)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(180, 120)
        Me.GroupBox2.TabIndex = 4
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
        'frmJobInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 323)
        Me.Controls.Add(Me.tabProcess)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mainMenu
        Me.MinimumSize = New System.Drawing.Size(660, 359)
        Me.Name = "frmJobInfo"
        Me.Text = "Service informations"
        Me.tabProcess.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.SplitContainerOnlineInfo.Panel1.ResumeLayout(False)
        Me.SplitContainerOnlineInfo.Panel1.PerformLayout()
        Me.SplitContainerOnlineInfo.Panel2.ResumeLayout(False)
        Me.SplitContainerOnlineInfo.ResumeLayout(False)
        Me.gpProcGeneralFile.ResumeLayout(False)
        Me.gpProcGeneralFile.PerformLayout()
        CType(Me.pctSmallIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctBigIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabProcess As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtStartType As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents gpProcGeneralFile As System.Windows.Forms.GroupBox
    Friend WithEvents cmdShowFileDetails As System.Windows.Forms.Button
    Friend WithEvents cmdShowFileProperties As System.Windows.Forms.Button
    Friend WithEvents cmdOpenDirectory As System.Windows.Forms.Button
    Friend WithEvents txtServicePath As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtImageVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents pctSmallIcon As System.Windows.Forms.PictureBox
    Friend WithEvents pctBigIcon As System.Windows.Forms.PictureBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents imgProcess As System.Windows.Forms.ImageList
    Friend WithEvents imgProcessTab As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainerOnlineInfo As System.Windows.Forms.SplitContainer
    Friend WithEvents lblSecurityRisk As System.Windows.Forms.Label
    Friend WithEvents cmdGetOnlineInfos As System.Windows.Forms.Button
    Friend WithEvents rtbOnlineInfos As System.Windows.Forms.RichTextBox
    Friend WithEvents txtProcess As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents cmdSetStartType As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbStart As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdGoProcess As System.Windows.Forms.Button
    Friend WithEvents cmdResume As System.Windows.Forms.Button
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
    Friend WithEvents txtCommand As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmdInspectExe As System.Windows.Forms.Button
    Private WithEvents menuCopyPctbig As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyBig As System.Windows.Forms.MenuItem
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Private WithEvents menuCopyPctSmall As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopySmall As System.Windows.Forms.MenuItem
    Private WithEvents mainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblThreads As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblUserObjectsCount As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblGDIcount As System.Windows.Forms.Label
    Friend WithEvents lbl789 As System.Windows.Forms.Label
    Friend WithEvents lblHandles As System.Windows.Forms.Label
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
    Friend WithEvents lblAverageCPUusage As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTotalTime As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblUserTime As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblKernelTime As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
End Class
