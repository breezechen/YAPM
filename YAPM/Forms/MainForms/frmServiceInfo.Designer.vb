<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceInfo
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
        Dim CConnection1 As CoreFunc.cConnection = New CoreFunc.cConnection
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServiceInfo))
        Dim CConnection2 As CoreFunc.cConnection = New CoreFunc.cConnection
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
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtServiceSpecificExitCode = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtDiagnosticMessageFile = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtCheckPoint = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtWaitHint = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtServiceFlags = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rtbDescription = New System.Windows.Forms.RichTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtTagID = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtObjectName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtWin32ExitCode = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtServiceStartName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtLoadOrderGroup = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtErrorControl = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tabDep = New System.Windows.Forms.TabPage
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Label9 = New System.Windows.Forms.Label
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.tv2 = New serviceDependenciesList
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdServDet1 = New System.Windows.Forms.Button
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.Label21 = New System.Windows.Forms.Label
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.tv = New serviceDependenciesList
        Me.cmdServDet2 = New System.Windows.Forms.Button
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.SplitContainerInfoProcess = New System.Windows.Forms.SplitContainer
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.cmdInfosToClipB = New System.Windows.Forms.Button
        Me.rtb = New System.Windows.Forms.RichTextBox
        Me.imgProcessTab = New System.Windows.Forms.ImageList(Me.components)
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
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.tabDep.SuspendLayout()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.SplitContainerInfoProcess.Panel1.SuspendLayout()
        Me.SplitContainerInfoProcess.Panel2.SuspendLayout()
        Me.SplitContainerInfoProcess.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tabProcess
        '
        Me.tabProcess.Controls.Add(Me.TabPage1)
        Me.tabProcess.Controls.Add(Me.TabPage2)
        Me.tabProcess.Controls.Add(Me.tabDep)
        Me.tabProcess.Controls.Add(Me.TabPage6)
        Me.tabProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabProcess.ImageList = Me.imgProcessTab
        Me.tabProcess.Location = New System.Drawing.Point(0, 0)
        Me.tabProcess.Multiline = True
        Me.tabProcess.Name = "tabProcess"
        Me.tabProcess.SelectedIndex = 0
        Me.tabProcess.Size = New System.Drawing.Size(655, 335)
        Me.tabProcess.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.gpProcGeneralFile)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(647, 308)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General - 1"
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
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.ImageIndex = 0
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(647, 308)
        Me.TabPage2.TabIndex = 8
        Me.TabPage2.Text = "General - 2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtServiceSpecificExitCode)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.txtDiagnosticMessageFile)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtCheckPoint)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtWaitHint)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtServiceFlags)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Location = New System.Drawing.Point(329, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(312, 169)
        Me.GroupBox4.TabIndex = 19
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Additional informations"
        '
        'txtServiceSpecificExitCode
        '
        Me.txtServiceSpecificExitCode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtServiceSpecificExitCode.Location = New System.Drawing.Point(144, 114)
        Me.txtServiceSpecificExitCode.Name = "txtServiceSpecificExitCode"
        Me.txtServiceSpecificExitCode.ReadOnly = True
        Me.txtServiceSpecificExitCode.Size = New System.Drawing.Size(158, 22)
        Me.txtServiceSpecificExitCode.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 117)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(132, 13)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "Service specific exit code"
        '
        'txtDiagnosticMessageFile
        '
        Me.txtDiagnosticMessageFile.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDiagnosticMessageFile.Location = New System.Drawing.Point(144, 90)
        Me.txtDiagnosticMessageFile.Name = "txtDiagnosticMessageFile"
        Me.txtDiagnosticMessageFile.ReadOnly = True
        Me.txtDiagnosticMessageFile.Size = New System.Drawing.Size(158, 22)
        Me.txtDiagnosticMessageFile.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 93)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(128, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Diagnostic message file"
        '
        'txtCheckPoint
        '
        Me.txtCheckPoint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCheckPoint.Location = New System.Drawing.Point(89, 67)
        Me.txtCheckPoint.Name = "txtCheckPoint"
        Me.txtCheckPoint.ReadOnly = True
        Me.txtCheckPoint.Size = New System.Drawing.Size(213, 22)
        Me.txtCheckPoint.TabIndex = 8
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 70)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Checkpoint"
        '
        'txtWaitHint
        '
        Me.txtWaitHint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtWaitHint.Location = New System.Drawing.Point(89, 45)
        Me.txtWaitHint.Name = "txtWaitHint"
        Me.txtWaitHint.ReadOnly = True
        Me.txtWaitHint.Size = New System.Drawing.Size(213, 22)
        Me.txtWaitHint.TabIndex = 7
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 48)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(55, 13)
        Me.Label19.TabIndex = 17
        Me.Label19.Text = "Wait hint"
        '
        'txtServiceFlags
        '
        Me.txtServiceFlags.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtServiceFlags.Location = New System.Drawing.Point(89, 22)
        Me.txtServiceFlags.Name = "txtServiceFlags"
        Me.txtServiceFlags.ReadOnly = True
        Me.txtServiceFlags.Size = New System.Drawing.Size(213, 22)
        Me.txtServiceFlags.TabIndex = 6
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(6, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(70, 13)
        Me.Label20.TabIndex = 15
        Me.Label20.Text = "Service flags"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rtbDescription)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 181)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(376, 119)
        Me.GroupBox3.TabIndex = 18
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Description"
        '
        'rtbDescription
        '
        Me.rtbDescription.AutoWordSelection = True
        Me.rtbDescription.BackColor = System.Drawing.Color.White
        Me.rtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbDescription.HideSelection = False
        Me.rtbDescription.Location = New System.Drawing.Point(3, 18)
        Me.rtbDescription.Name = "rtbDescription"
        Me.rtbDescription.ReadOnly = True
        Me.rtbDescription.Size = New System.Drawing.Size(370, 98)
        Me.rtbDescription.TabIndex = 13
        Me.rtbDescription.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtTagID)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtObjectName)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtWin32ExitCode)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtServiceStartName)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtLoadOrderGroup)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtErrorControl)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(312, 169)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Additional informations"
        '
        'txtTagID
        '
        Me.txtTagID.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtTagID.Location = New System.Drawing.Point(110, 138)
        Me.txtTagID.Name = "txtTagID"
        Me.txtTagID.ReadOnly = True
        Me.txtTagID.Size = New System.Drawing.Size(192, 22)
        Me.txtTagID.TabIndex = 24
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 141)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Tag ID"
        '
        'txtObjectName
        '
        Me.txtObjectName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtObjectName.Location = New System.Drawing.Point(110, 114)
        Me.txtObjectName.Name = "txtObjectName"
        Me.txtObjectName.ReadOnly = True
        Me.txtObjectName.Size = New System.Drawing.Size(192, 22)
        Me.txtObjectName.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Object name"
        '
        'txtWin32ExitCode
        '
        Me.txtWin32ExitCode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtWin32ExitCode.Location = New System.Drawing.Point(110, 90)
        Me.txtWin32ExitCode.Name = "txtWin32ExitCode"
        Me.txtWin32ExitCode.ReadOnly = True
        Me.txtWin32ExitCode.Size = New System.Drawing.Size(192, 22)
        Me.txtWin32ExitCode.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Win32 exit code"
        '
        'txtServiceStartName
        '
        Me.txtServiceStartName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtServiceStartName.Location = New System.Drawing.Point(110, 67)
        Me.txtServiceStartName.Name = "txtServiceStartName"
        Me.txtServiceStartName.ReadOnly = True
        Me.txtServiceStartName.Size = New System.Drawing.Size(192, 22)
        Me.txtServiceStartName.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Service start name"
        '
        'txtLoadOrderGroup
        '
        Me.txtLoadOrderGroup.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtLoadOrderGroup.Location = New System.Drawing.Point(110, 45)
        Me.txtLoadOrderGroup.Name = "txtLoadOrderGroup"
        Me.txtLoadOrderGroup.ReadOnly = True
        Me.txtLoadOrderGroup.Size = New System.Drawing.Size(192, 22)
        Me.txtLoadOrderGroup.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Load order group"
        '
        'txtErrorControl
        '
        Me.txtErrorControl.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtErrorControl.Location = New System.Drawing.Point(110, 22)
        Me.txtErrorControl.Name = "txtErrorControl"
        Me.txtErrorControl.ReadOnly = True
        Me.txtErrorControl.Size = New System.Drawing.Size(192, 22)
        Me.txtErrorControl.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Error control"
        '
        'tabDep
        '
        Me.tabDep.Controls.Add(Me.SplitContainer)
        Me.tabDep.ImageIndex = 8
        Me.tabDep.Location = New System.Drawing.Point(4, 23)
        Me.tabDep.Name = "tabDep"
        Me.tabDep.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDep.Size = New System.Drawing.Size(647, 308)
        Me.tabDep.TabIndex = 7
        Me.tabDep.Text = "Dependencies"
        Me.tabDep.UseVisualStyleBackColor = True
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer.Size = New System.Drawing.Size(641, 302)
        Me.SplitContainer.SplitterDistance = 320
        Me.SplitContainer.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label9)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(320, 302)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(200, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "The service depends on these services"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.tv2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cmdServDet1)
        Me.SplitContainer2.Size = New System.Drawing.Size(320, 273)
        Me.SplitContainer2.SplitterDistance = 243
        Me.SplitContainer2.TabIndex = 0
        '
        'tv2
        '
        CConnection1.ConnectionType = CoreFunc.cConnection.TypeOfConnection.LocalConnection
        Me.tv2.ConnectionObj = CConnection1
        Me.tv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tv2.ImageIndex = 0
        Me.tv2.ImageList = Me.imgServices
        Me.tv2.InfosToGet = CoreFunc.cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
        Me.tv2.Location = New System.Drawing.Point(0, 0)
        Me.tv2.Name = "tv2"
        Me.tv2.RootService = Nothing
        Me.tv2.SelectedImageIndex = 2
        Me.tv2.Size = New System.Drawing.Size(320, 243)
        Me.tv2.TabIndex = 16
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
        'cmdServDet1
        '
        Me.cmdServDet1.Location = New System.Drawing.Point(90, 2)
        Me.cmdServDet1.Name = "cmdServDet1"
        Me.cmdServDet1.Size = New System.Drawing.Size(140, 23)
        Me.cmdServDet1.TabIndex = 0
        Me.cmdServDet1.Text = "View services details"
        Me.cmdServDet1.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label21)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitContainer3.Size = New System.Drawing.Size(317, 302)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(195, 13)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "These services depend on the service"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer4.IsSplitterFixed = True
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.tv)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.cmdServDet2)
        Me.SplitContainer4.Size = New System.Drawing.Size(317, 273)
        Me.SplitContainer4.SplitterDistance = 243
        Me.SplitContainer4.TabIndex = 0
        '
        'tv
        '
        CConnection2.ConnectionType = CoreFunc.cConnection.TypeOfConnection.LocalConnection
        Me.tv.ConnectionObj = CConnection2
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tv.ImageIndex = 0
        Me.tv.ImageList = Me.imgServices
        Me.tv.InfosToGet = CoreFunc.cServDepConnection.DependenciesToget.DependenciesOfMe
        Me.tv.Location = New System.Drawing.Point(0, 0)
        Me.tv.Name = "tv"
        Me.tv.RootService = Nothing
        Me.tv.SelectedImageIndex = 0
        Me.tv.Size = New System.Drawing.Size(317, 243)
        Me.tv.TabIndex = 15
        '
        'cmdServDet2
        '
        Me.cmdServDet2.Location = New System.Drawing.Point(88, 2)
        Me.cmdServDet2.Name = "cmdServDet2"
        Me.cmdServDet2.Size = New System.Drawing.Size(140, 23)
        Me.cmdServDet2.TabIndex = 1
        Me.cmdServDet2.Text = "View services details"
        Me.cmdServDet2.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.SplitContainerInfoProcess)
        Me.TabPage6.ImageIndex = 5
        Me.TabPage6.Location = New System.Drawing.Point(4, 23)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(647, 308)
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
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.cmdRefresh)
        Me.SplitContainerInfoProcess.Panel1.Controls.Add(Me.cmdInfosToClipB)
        '
        'SplitContainerInfoProcess.Panel2
        '
        Me.SplitContainerInfoProcess.Panel2.Controls.Add(Me.rtb)
        Me.SplitContainerInfoProcess.Size = New System.Drawing.Size(641, 302)
        Me.SplitContainerInfoProcess.SplitterDistance = 25
        Me.SplitContainerInfoProcess.TabIndex = 0
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
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
        Me.cmdInfosToClipB.Image = Global.YAPM.My.Resources.Resources.copy16
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
        Me.rtb.Size = New System.Drawing.Size(641, 273)
        Me.rtb.TabIndex = 4
        Me.rtb.Text = ""
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
        'frmServiceInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 335)
        Me.Controls.Add(Me.tabProcess)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mainMenu
        Me.MinimumSize = New System.Drawing.Size(660, 359)
        Me.Name = "frmServiceInfo"
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
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.tabDep.ResumeLayout(False)
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.SplitContainerInfoProcess.Panel1.ResumeLayout(False)
        Me.SplitContainerInfoProcess.Panel2.ResumeLayout(False)
        Me.SplitContainerInfoProcess.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents SplitContainerInfoProcess As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdInfosToClipB As System.Windows.Forms.Button
    Friend WithEvents rtb As System.Windows.Forms.RichTextBox
    Friend WithEvents tabDep As System.Windows.Forms.TabPage
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
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents cmdPause As System.Windows.Forms.Button
    Friend WithEvents cmdStart As System.Windows.Forms.Button
    Friend WithEvents cmdSetStartType As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbStart As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdGoProcess As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rtbDescription As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtObjectName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtWin32ExitCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtServiceStartName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLoadOrderGroup As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtErrorControl As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtServiceSpecificExitCode As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDiagnosticMessageFile As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCheckPoint As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtWaitHint As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtServiceFlags As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtTagID As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmdResume As System.Windows.Forms.Button
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents tv2 As serviceDependenciesList
    Friend WithEvents tv As serviceDependenciesList
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
    Friend WithEvents cmdServDet1 As System.Windows.Forms.Button
    Friend WithEvents cmdServDet2 As System.Windows.Forms.Button
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
End Class
