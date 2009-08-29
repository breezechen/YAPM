<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetJobLimits
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
        Me.ChooseFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.cmdSetLimits = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkUIwriteCB = New System.Windows.Forms.CheckBox
        Me.chkUIsystemParam = New System.Windows.Forms.CheckBox
        Me.chkUIreadCB = New System.Windows.Forms.CheckBox
        Me.chkUIhandles = New System.Windows.Forms.CheckBox
        Me.chkUIglobalAtoms = New System.Windows.Forms.CheckBox
        Me.chkUIExitW = New System.Windows.Forms.CheckBox
        Me.chkUIDisplaySettings = New System.Windows.Forms.CheckBox
        Me.chkUIdesktop = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.valAffinity = New System.Windows.Forms.NumericUpDown
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.valMaxWS = New System.Windows.Forms.NumericUpDown
        Me.valMinWS = New System.Windows.Forms.NumericUpDown
        Me.valUsertimeP = New System.Windows.Forms.NumericUpDown
        Me.valMemP = New System.Windows.Forms.NumericUpDown
        Me.valUsertimeJ = New System.Windows.Forms.NumericUpDown
        Me.valMemJ = New System.Windows.Forms.NumericUpDown
        Me.chkMinMaxWS = New System.Windows.Forms.CheckBox
        Me.chkCommittedMemPerP = New System.Windows.Forms.CheckBox
        Me.chkUserModePerP = New System.Windows.Forms.CheckBox
        Me.chkCommittedMemPerJ = New System.Windows.Forms.CheckBox
        Me.chkUserModePerJ = New System.Windows.Forms.CheckBox
        Me.cbPriority = New System.Windows.Forms.ComboBox
        Me.chkPriority = New System.Windows.Forms.CheckBox
        Me.valScheduling = New System.Windows.Forms.NumericUpDown
        Me.valActiveProcesses = New System.Windows.Forms.NumericUpDown
        Me.chkSchedulingC = New System.Windows.Forms.CheckBox
        Me.chkActiveProcesses = New System.Windows.Forms.CheckBox
        Me.chkAffinity = New System.Windows.Forms.CheckBox
        Me.chkSilentBAOK = New System.Windows.Forms.CheckBox
        Me.chkPreserveJobTime = New System.Windows.Forms.CheckBox
        Me.chkKillOnJobClose = New System.Windows.Forms.CheckBox
        Me.chkDieOnUnhandledEx = New System.Windows.Forms.CheckBox
        Me.chkBreakawayOK = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.valAffinity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valMaxWS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valMinWS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valUsertimeP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valMemP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valUsertimeJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valMemJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valScheduling, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.valActiveProcesses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSetLimits
        '
        Me.cmdSetLimits.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdSetLimits.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSetLimits.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSetLimits.Location = New System.Drawing.Point(185, 392)
        Me.cmdSetLimits.Name = "cmdSetLimits"
        Me.cmdSetLimits.Size = New System.Drawing.Size(148, 23)
        Me.cmdSetLimits.TabIndex = 32
        Me.cmdSetLimits.Text = "Set limits"
        Me.cmdSetLimits.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.Location = New System.Drawing.Point(396, 392)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 33
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkUIwriteCB)
        Me.GroupBox1.Controls.Add(Me.chkUIsystemParam)
        Me.GroupBox1.Controls.Add(Me.chkUIreadCB)
        Me.GroupBox1.Controls.Add(Me.chkUIhandles)
        Me.GroupBox1.Controls.Add(Me.chkUIglobalAtoms)
        Me.GroupBox1.Controls.Add(Me.chkUIExitW)
        Me.GroupBox1.Controls.Add(Me.chkUIDisplaySettings)
        Me.GroupBox1.Controls.Add(Me.chkUIdesktop)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(167, 205)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "UI restrictions"
        '
        'chkUIwriteCB
        '
        Me.chkUIwriteCB.AutoSize = True
        Me.chkUIwriteCB.Location = New System.Drawing.Point(6, 182)
        Me.chkUIwriteCB.Name = "chkUIwriteCB"
        Me.chkUIwriteCB.Size = New System.Drawing.Size(105, 17)
        Me.chkUIwriteCB.TabIndex = 7
        Me.chkUIwriteCB.Text = "WriteClipboard"
        Me.chkUIwriteCB.UseVisualStyleBackColor = True
        '
        'chkUIsystemParam
        '
        Me.chkUIsystemParam.AutoSize = True
        Me.chkUIsystemParam.Location = New System.Drawing.Point(6, 159)
        Me.chkUIsystemParam.Name = "chkUIsystemParam"
        Me.chkUIsystemParam.Size = New System.Drawing.Size(117, 17)
        Me.chkUIsystemParam.TabIndex = 6
        Me.chkUIsystemParam.Text = "SystemParameters"
        Me.chkUIsystemParam.UseVisualStyleBackColor = True
        '
        'chkUIreadCB
        '
        Me.chkUIreadCB.AutoSize = True
        Me.chkUIreadCB.Location = New System.Drawing.Point(6, 136)
        Me.chkUIreadCB.Name = "chkUIreadCB"
        Me.chkUIreadCB.Size = New System.Drawing.Size(103, 17)
        Me.chkUIreadCB.TabIndex = 5
        Me.chkUIreadCB.Text = "ReadClipboard"
        Me.chkUIreadCB.UseVisualStyleBackColor = True
        '
        'chkUIhandles
        '
        Me.chkUIhandles.AutoSize = True
        Me.chkUIhandles.Location = New System.Drawing.Point(6, 113)
        Me.chkUIhandles.Name = "chkUIhandles"
        Me.chkUIhandles.Size = New System.Drawing.Size(68, 17)
        Me.chkUIhandles.TabIndex = 4
        Me.chkUIhandles.Text = "Handles"
        Me.chkUIhandles.UseVisualStyleBackColor = True
        '
        'chkUIglobalAtoms
        '
        Me.chkUIglobalAtoms.AutoSize = True
        Me.chkUIglobalAtoms.Location = New System.Drawing.Point(6, 90)
        Me.chkUIglobalAtoms.Name = "chkUIglobalAtoms"
        Me.chkUIglobalAtoms.Size = New System.Drawing.Size(92, 17)
        Me.chkUIglobalAtoms.TabIndex = 3
        Me.chkUIglobalAtoms.Text = "GlobalAtoms"
        Me.chkUIglobalAtoms.UseVisualStyleBackColor = True
        '
        'chkUIExitW
        '
        Me.chkUIExitW.AutoSize = True
        Me.chkUIExitW.Location = New System.Drawing.Point(6, 67)
        Me.chkUIExitW.Name = "chkUIExitW"
        Me.chkUIExitW.Size = New System.Drawing.Size(93, 17)
        Me.chkUIExitW.TabIndex = 2
        Me.chkUIExitW.Text = "ExitWindows"
        Me.chkUIExitW.UseVisualStyleBackColor = True
        '
        'chkUIDisplaySettings
        '
        Me.chkUIDisplaySettings.AutoSize = True
        Me.chkUIDisplaySettings.Location = New System.Drawing.Point(6, 44)
        Me.chkUIDisplaySettings.Name = "chkUIDisplaySettings"
        Me.chkUIDisplaySettings.Size = New System.Drawing.Size(105, 17)
        Me.chkUIDisplaySettings.TabIndex = 1
        Me.chkUIDisplaySettings.Text = "DisplaySettings"
        Me.chkUIDisplaySettings.UseVisualStyleBackColor = True
        '
        'chkUIdesktop
        '
        Me.chkUIdesktop.AutoSize = True
        Me.chkUIdesktop.Location = New System.Drawing.Point(6, 21)
        Me.chkUIdesktop.Name = "chkUIdesktop"
        Me.chkUIdesktop.Size = New System.Drawing.Size(69, 17)
        Me.chkUIdesktop.TabIndex = 0
        Me.chkUIdesktop.Text = "Desktop"
        Me.chkUIdesktop.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.valAffinity)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.valMaxWS)
        Me.GroupBox2.Controls.Add(Me.valMinWS)
        Me.GroupBox2.Controls.Add(Me.valUsertimeP)
        Me.GroupBox2.Controls.Add(Me.valMemP)
        Me.GroupBox2.Controls.Add(Me.valUsertimeJ)
        Me.GroupBox2.Controls.Add(Me.valMemJ)
        Me.GroupBox2.Controls.Add(Me.chkMinMaxWS)
        Me.GroupBox2.Controls.Add(Me.chkCommittedMemPerP)
        Me.GroupBox2.Controls.Add(Me.chkUserModePerP)
        Me.GroupBox2.Controls.Add(Me.chkCommittedMemPerJ)
        Me.GroupBox2.Controls.Add(Me.chkUserModePerJ)
        Me.GroupBox2.Controls.Add(Me.cbPriority)
        Me.GroupBox2.Controls.Add(Me.chkPriority)
        Me.GroupBox2.Controls.Add(Me.valScheduling)
        Me.GroupBox2.Controls.Add(Me.valActiveProcesses)
        Me.GroupBox2.Controls.Add(Me.chkSchedulingC)
        Me.GroupBox2.Controls.Add(Me.chkActiveProcesses)
        Me.GroupBox2.Controls.Add(Me.chkAffinity)
        Me.GroupBox2.Controls.Add(Me.chkSilentBAOK)
        Me.GroupBox2.Controls.Add(Me.chkPreserveJobTime)
        Me.GroupBox2.Controls.Add(Me.chkKillOnJobClose)
        Me.GroupBox2.Controls.Add(Me.chkDieOnUnhandledEx)
        Me.GroupBox2.Controls.Add(Me.chkBreakawayOK)
        Me.GroupBox2.Location = New System.Drawing.Point(185, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(286, 374)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "UI restrictions"
        '
        'valAffinity
        '
        Me.valAffinity.Enabled = False
        Me.valAffinity.Location = New System.Drawing.Point(169, 134)
        Me.valAffinity.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.valAffinity.Name = "valAffinity"
        Me.valAffinity.Size = New System.Drawing.Size(105, 22)
        Me.valAffinity.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(251, 298)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 60
        Me.Label6.Text = "sec"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(251, 253)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 13)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "sec"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(251, 276)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "KB"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(251, 321)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 57
        Me.Label3.Text = "KB"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(251, 344)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "KB"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(251, 232)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 13)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "KB"
        '
        'valMaxWS
        '
        Me.valMaxWS.Enabled = False
        Me.valMaxWS.Location = New System.Drawing.Point(169, 342)
        Me.valMaxWS.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.valMaxWS.Name = "valMaxWS"
        Me.valMaxWS.Size = New System.Drawing.Size(76, 22)
        Me.valMaxWS.TabIndex = 31
        '
        'valMinWS
        '
        Me.valMinWS.Enabled = False
        Me.valMinWS.Location = New System.Drawing.Point(169, 319)
        Me.valMinWS.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.valMinWS.Name = "valMinWS"
        Me.valMinWS.Size = New System.Drawing.Size(76, 22)
        Me.valMinWS.TabIndex = 30
        '
        'valUsertimeP
        '
        Me.valUsertimeP.Enabled = False
        Me.valUsertimeP.Location = New System.Drawing.Point(169, 296)
        Me.valUsertimeP.Maximum = New Decimal(New Integer() {1410065408, 2, 0, 0})
        Me.valUsertimeP.Name = "valUsertimeP"
        Me.valUsertimeP.Size = New System.Drawing.Size(76, 22)
        Me.valUsertimeP.TabIndex = 28
        '
        'valMemP
        '
        Me.valMemP.Enabled = False
        Me.valMemP.Location = New System.Drawing.Point(169, 274)
        Me.valMemP.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.valMemP.Name = "valMemP"
        Me.valMemP.Size = New System.Drawing.Size(76, 22)
        Me.valMemP.TabIndex = 26
        '
        'valUsertimeJ
        '
        Me.valUsertimeJ.Enabled = False
        Me.valUsertimeJ.Location = New System.Drawing.Point(169, 251)
        Me.valUsertimeJ.Maximum = New Decimal(New Integer() {1410065408, 2, 0, 0})
        Me.valUsertimeJ.Name = "valUsertimeJ"
        Me.valUsertimeJ.Size = New System.Drawing.Size(76, 22)
        Me.valUsertimeJ.TabIndex = 24
        '
        'valMemJ
        '
        Me.valMemJ.Enabled = False
        Me.valMemJ.Location = New System.Drawing.Point(169, 226)
        Me.valMemJ.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.valMemJ.Name = "valMemJ"
        Me.valMemJ.Size = New System.Drawing.Size(76, 22)
        Me.valMemJ.TabIndex = 22
        '
        'chkMinMaxWS
        '
        Me.chkMinMaxWS.AutoSize = True
        Me.chkMinMaxWS.Location = New System.Drawing.Point(6, 319)
        Me.chkMinMaxWS.Name = "chkMinMaxWS"
        Me.chkMinMaxWS.Size = New System.Drawing.Size(112, 30)
        Me.chkMinMaxWS.TabIndex = 29
        Me.chkMinMaxWS.Text = "Min and max WS" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "size / process"
        Me.chkMinMaxWS.UseVisualStyleBackColor = True
        '
        'chkCommittedMemPerP
        '
        Me.chkCommittedMemPerP.AutoSize = True
        Me.chkCommittedMemPerP.Location = New System.Drawing.Point(6, 274)
        Me.chkCommittedMemPerP.Name = "chkCommittedMemPerP"
        Me.chkCommittedMemPerP.Size = New System.Drawing.Size(158, 17)
        Me.chkCommittedMemPerP.TabIndex = 25
        Me.chkCommittedMemPerP.Text = "Committed mem / process"
        Me.chkCommittedMemPerP.UseVisualStyleBackColor = True
        '
        'chkUserModePerP
        '
        Me.chkUserModePerP.AutoSize = True
        Me.chkUserModePerP.Location = New System.Drawing.Point(6, 297)
        Me.chkUserModePerP.Name = "chkUserModePerP"
        Me.chkUserModePerP.Size = New System.Drawing.Size(152, 17)
        Me.chkUserModePerP.TabIndex = 27
        Me.chkUserModePerP.Text = "Usermode time / process"
        Me.chkUserModePerP.UseVisualStyleBackColor = True
        '
        'chkCommittedMemPerJ
        '
        Me.chkCommittedMemPerJ.AutoSize = True
        Me.chkCommittedMemPerJ.Location = New System.Drawing.Point(6, 228)
        Me.chkCommittedMemPerJ.Name = "chkCommittedMemPerJ"
        Me.chkCommittedMemPerJ.Size = New System.Drawing.Size(147, 17)
        Me.chkCommittedMemPerJ.TabIndex = 21
        Me.chkCommittedMemPerJ.Text = "Committed mem for job"
        Me.chkCommittedMemPerJ.UseVisualStyleBackColor = True
        '
        'chkUserModePerJ
        '
        Me.chkUserModePerJ.AutoSize = True
        Me.chkUserModePerJ.Location = New System.Drawing.Point(6, 251)
        Me.chkUserModePerJ.Name = "chkUserModePerJ"
        Me.chkUserModePerJ.Size = New System.Drawing.Size(141, 17)
        Me.chkUserModePerJ.TabIndex = 23
        Me.chkUserModePerJ.Text = "Usermode time for job"
        Me.chkUserModePerJ.UseVisualStyleBackColor = True
        '
        'cbPriority
        '
        Me.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPriority.Enabled = False
        Me.cbPriority.FormattingEnabled = True
        Me.cbPriority.Items.AddRange(New Object() {"Idle", "BelowNormal", "Normal", "AboveNormal", "High", "RealTime"})
        Me.cbPriority.Location = New System.Drawing.Point(169, 204)
        Me.cbPriority.Name = "cbPriority"
        Me.cbPriority.Size = New System.Drawing.Size(105, 21)
        Me.cbPriority.TabIndex = 20
        '
        'chkPriority
        '
        Me.chkPriority.AutoSize = True
        Me.chkPriority.Location = New System.Drawing.Point(6, 205)
        Me.chkPriority.Name = "chkPriority"
        Me.chkPriority.Size = New System.Drawing.Size(88, 17)
        Me.chkPriority.TabIndex = 19
        Me.chkPriority.Text = "PriorityClass"
        Me.chkPriority.UseVisualStyleBackColor = True
        '
        'valScheduling
        '
        Me.valScheduling.Enabled = False
        Me.valScheduling.Location = New System.Drawing.Point(169, 181)
        Me.valScheduling.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.valScheduling.Name = "valScheduling"
        Me.valScheduling.Size = New System.Drawing.Size(105, 22)
        Me.valScheduling.TabIndex = 18
        '
        'valActiveProcesses
        '
        Me.valActiveProcesses.Enabled = False
        Me.valActiveProcesses.Location = New System.Drawing.Point(169, 157)
        Me.valActiveProcesses.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.valActiveProcesses.Name = "valActiveProcesses"
        Me.valActiveProcesses.Size = New System.Drawing.Size(105, 22)
        Me.valActiveProcesses.TabIndex = 16
        '
        'chkSchedulingC
        '
        Me.chkSchedulingC.AutoSize = True
        Me.chkSchedulingC.Location = New System.Drawing.Point(6, 182)
        Me.chkSchedulingC.Name = "chkSchedulingC"
        Me.chkSchedulingC.Size = New System.Drawing.Size(110, 17)
        Me.chkSchedulingC.TabIndex = 17
        Me.chkSchedulingC.Text = "SchedulingClass"
        Me.chkSchedulingC.UseVisualStyleBackColor = True
        '
        'chkActiveProcesses
        '
        Me.chkActiveProcesses.AutoSize = True
        Me.chkActiveProcesses.Location = New System.Drawing.Point(6, 159)
        Me.chkActiveProcesses.Name = "chkActiveProcesses"
        Me.chkActiveProcesses.Size = New System.Drawing.Size(109, 17)
        Me.chkActiveProcesses.TabIndex = 15
        Me.chkActiveProcesses.Text = "Active processes"
        Me.chkActiveProcesses.UseVisualStyleBackColor = True
        '
        'chkAffinity
        '
        Me.chkAffinity.AutoSize = True
        Me.chkAffinity.Location = New System.Drawing.Point(6, 136)
        Me.chkAffinity.Name = "chkAffinity"
        Me.chkAffinity.Size = New System.Drawing.Size(63, 17)
        Me.chkAffinity.TabIndex = 13
        Me.chkAffinity.Text = "Affinity"
        Me.chkAffinity.UseVisualStyleBackColor = True
        '
        'chkSilentBAOK
        '
        Me.chkSilentBAOK.AutoSize = True
        Me.chkSilentBAOK.Location = New System.Drawing.Point(6, 113)
        Me.chkSilentBAOK.Name = "chkSilentBAOK"
        Me.chkSilentBAOK.Size = New System.Drawing.Size(125, 17)
        Me.chkSilentBAOK.TabIndex = 12
        Me.chkSilentBAOK.Text = "SilentBreakawayOk"
        Me.chkSilentBAOK.UseVisualStyleBackColor = True
        '
        'chkPreserveJobTime
        '
        Me.chkPreserveJobTime.AutoSize = True
        Me.chkPreserveJobTime.Location = New System.Drawing.Point(6, 90)
        Me.chkPreserveJobTime.Name = "chkPreserveJobTime"
        Me.chkPreserveJobTime.Size = New System.Drawing.Size(109, 17)
        Me.chkPreserveJobTime.TabIndex = 11
        Me.chkPreserveJobTime.Text = "PreserveJobTime"
        Me.chkPreserveJobTime.UseVisualStyleBackColor = True
        '
        'chkKillOnJobClose
        '
        Me.chkKillOnJobClose.AutoSize = True
        Me.chkKillOnJobClose.Location = New System.Drawing.Point(6, 67)
        Me.chkKillOnJobClose.Name = "chkKillOnJobClose"
        Me.chkKillOnJobClose.Size = New System.Drawing.Size(103, 17)
        Me.chkKillOnJobClose.TabIndex = 10
        Me.chkKillOnJobClose.Text = "KillOnJobClose"
        Me.chkKillOnJobClose.UseVisualStyleBackColor = True
        '
        'chkDieOnUnhandledEx
        '
        Me.chkDieOnUnhandledEx.AutoSize = True
        Me.chkDieOnUnhandledEx.Location = New System.Drawing.Point(6, 44)
        Me.chkDieOnUnhandledEx.Name = "chkDieOnUnhandledEx"
        Me.chkDieOnUnhandledEx.Size = New System.Drawing.Size(167, 17)
        Me.chkDieOnUnhandledEx.TabIndex = 9
        Me.chkDieOnUnhandledEx.Text = "DieOnUnhandledException"
        Me.chkDieOnUnhandledEx.UseVisualStyleBackColor = True
        '
        'chkBreakawayOK
        '
        Me.chkBreakawayOK.AutoSize = True
        Me.chkBreakawayOK.Location = New System.Drawing.Point(6, 21)
        Me.chkBreakawayOK.Name = "chkBreakawayOK"
        Me.chkBreakawayOK.Size = New System.Drawing.Size(96, 17)
        Me.chkBreakawayOK.TabIndex = 8
        Me.chkBreakawayOK.Text = "BreakawayOK"
        Me.chkBreakawayOK.UseVisualStyleBackColor = True
        '
        'frmSetJobLimits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 427)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSetLimits)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmSetJobLimits"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change memory protection type"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.valAffinity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valMaxWS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valMinWS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valUsertimeP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valMemP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valUsertimeJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valMemJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valScheduling, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.valActiveProcesses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ChooseFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmdSetLimits As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkUIwriteCB As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIsystemParam As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIreadCB As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIhandles As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIglobalAtoms As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIExitW As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIDisplaySettings As System.Windows.Forms.CheckBox
    Friend WithEvents chkUIdesktop As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkSchedulingC As System.Windows.Forms.CheckBox
    Friend WithEvents chkActiveProcesses As System.Windows.Forms.CheckBox
    Friend WithEvents chkAffinity As System.Windows.Forms.CheckBox
    Friend WithEvents chkSilentBAOK As System.Windows.Forms.CheckBox
    Friend WithEvents chkPreserveJobTime As System.Windows.Forms.CheckBox
    Friend WithEvents chkKillOnJobClose As System.Windows.Forms.CheckBox
    Friend WithEvents chkDieOnUnhandledEx As System.Windows.Forms.CheckBox
    Friend WithEvents chkBreakawayOK As System.Windows.Forms.CheckBox
    Friend WithEvents chkPriority As System.Windows.Forms.CheckBox
    Friend WithEvents valScheduling As System.Windows.Forms.NumericUpDown
    Friend WithEvents valActiveProcesses As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkCommittedMemPerJ As System.Windows.Forms.CheckBox
    Friend WithEvents chkUserModePerJ As System.Windows.Forms.CheckBox
    Friend WithEvents cbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents valMaxWS As System.Windows.Forms.NumericUpDown
    Friend WithEvents valMinWS As System.Windows.Forms.NumericUpDown
    Friend WithEvents valUsertimeP As System.Windows.Forms.NumericUpDown
    Friend WithEvents valMemP As System.Windows.Forms.NumericUpDown
    Friend WithEvents valUsertimeJ As System.Windows.Forms.NumericUpDown
    Friend WithEvents valMemJ As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkMinMaxWS As System.Windows.Forms.CheckBox
    Friend WithEvents chkCommittedMemPerP As System.Windows.Forms.CheckBox
    Friend WithEvents chkUserModePerP As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents valAffinity As System.Windows.Forms.NumericUpDown
End Class
