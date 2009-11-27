<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConnection
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
        Dim SecureString1 As System.Security.SecureString = New System.Security.SecureString
        Me.optLocal = New System.Windows.Forms.RadioButton
        Me.optWMI = New System.Windows.Forms.RadioButton
        Me.optServer = New System.Windows.Forms.RadioButton
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.gpServer = New System.Windows.Forms.GroupBox
        Me.cmdShowDatas = New System.Windows.Forms.Button
        Me.txtPort = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtServerIP = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.gpWMI = New System.Windows.Forms.GroupBox
        Me.txtServerPassword = New SecurePasswordTextBox.SecureTextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtServerUser = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtServerMachine = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.gpShutdown = New System.Windows.Forms.GroupBox
        Me.cmdShutdown = New System.Windows.Forms.Button
        Me.cbShutdown = New System.Windows.Forms.ComboBox
        Me.chkForceShutdown = New System.Windows.Forms.CheckBox
        Me.cmdTerminal = New System.Windows.Forms.Button
        Me.optSnapshot = New System.Windows.Forms.RadioButton
        Me.gpSnapshot = New System.Windows.Forms.GroupBox
        Me.cmdBrowseSSFile = New System.Windows.Forms.Button
        Me.txtSSFile = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.openFile = New System.Windows.Forms.OpenFileDialog
        Me.lvData = New DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.cmdSSFileInfos = New System.Windows.Forms.Button
        Me.gpServer.SuspendLayout()
        Me.gpWMI.SuspendLayout()
        Me.gpShutdown.SuspendLayout()
        Me.gpSnapshot.SuspendLayout()
        Me.SuspendLayout()
        '
        'optLocal
        '
        Me.optLocal.AutoSize = True
        Me.optLocal.Checked = True
        Me.optLocal.Location = New System.Drawing.Point(11, 12)
        Me.optLocal.Name = "optLocal"
        Me.optLocal.Size = New System.Drawing.Size(51, 17)
        Me.optLocal.TabIndex = 0
        Me.optLocal.TabStop = True
        Me.optLocal.Text = "Local"
        Me.optLocal.UseVisualStyleBackColor = True
        '
        'optWMI
        '
        Me.optWMI.AutoSize = True
        Me.optWMI.Location = New System.Drawing.Point(219, 12)
        Me.optWMI.Name = "optWMI"
        Me.optWMI.Size = New System.Drawing.Size(108, 17)
        Me.optWMI.TabIndex = 1
        Me.optWMI.Text = "Remote via WMI"
        Me.optWMI.UseVisualStyleBackColor = True
        '
        'optServer
        '
        Me.optServer.AutoSize = True
        Me.optServer.Location = New System.Drawing.Point(68, 12)
        Me.optServer.Name = "optServer"
        Me.optServer.Size = New System.Drawing.Size(145, 17)
        Me.optServer.TabIndex = 2
        Me.optServer.Text = "Remote via YAPM server"
        Me.optServer.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(11, 66)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(316, 81)
        Me.txtDesc.TabIndex = 3
        '
        'cmdConnect
        '
        Me.cmdConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdConnect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConnect.Location = New System.Drawing.Point(43, 293)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(103, 28)
        Me.cmdConnect.TabIndex = 4
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Image = Global.My.Resources.Resources.down16
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCancel.Location = New System.Drawing.Point(189, 293)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(107, 28)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "      Hide window"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'gpServer
        '
        Me.gpServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gpServer.Controls.Add(Me.cmdShowDatas)
        Me.gpServer.Controls.Add(Me.txtPort)
        Me.gpServer.Controls.Add(Me.Label1)
        Me.gpServer.Controls.Add(Me.txtServerIP)
        Me.gpServer.Controls.Add(Me.Label17)
        Me.gpServer.Location = New System.Drawing.Point(13, 212)
        Me.gpServer.Name = "gpServer"
        Me.gpServer.Size = New System.Drawing.Size(314, 73)
        Me.gpServer.TabIndex = 6
        Me.gpServer.TabStop = False
        '
        'cmdShowDatas
        '
        Me.cmdShowDatas.Location = New System.Drawing.Point(159, 44)
        Me.cmdShowDatas.Name = "cmdShowDatas"
        Me.cmdShowDatas.Size = New System.Drawing.Size(146, 23)
        Me.cmdShowDatas.TabIndex = 14
        Me.cmdShowDatas.Text = "Show received data"
        Me.cmdShowDatas.UseVisualStyleBackColor = True
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(43, 45)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(73, 22)
        Me.txtPort.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Port"
        '
        'txtServerIP
        '
        Me.txtServerIP.Location = New System.Drawing.Point(64, 17)
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.Size = New System.Drawing.Size(87, 22)
        Me.txtServerIP.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(7, 20)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(51, 13)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "Machine"
        '
        'gpWMI
        '
        Me.gpWMI.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gpWMI.Controls.Add(Me.txtServerPassword)
        Me.gpWMI.Controls.Add(Me.Label14)
        Me.gpWMI.Controls.Add(Me.txtServerUser)
        Me.gpWMI.Controls.Add(Me.Label13)
        Me.gpWMI.Controls.Add(Me.txtServerMachine)
        Me.gpWMI.Controls.Add(Me.Label12)
        Me.gpWMI.Location = New System.Drawing.Point(13, 212)
        Me.gpWMI.Name = "gpWMI"
        Me.gpWMI.Size = New System.Drawing.Size(314, 73)
        Me.gpWMI.TabIndex = 7
        Me.gpWMI.TabStop = False
        '
        'txtServerPassword
        '
        Me.txtServerPassword.Location = New System.Drawing.Point(221, 45)
        Me.txtServerPassword.Name = "txtServerPassword"
        Me.txtServerPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtServerPassword.SecureText = SecureString1
        Me.txtServerPassword.Size = New System.Drawing.Size(87, 22)
        Me.txtServerPassword.TabIndex = 15
        Me.txtServerPassword.UseSystemPasswordChar = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(159, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 13)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "Password"
        '
        'txtServerUser
        '
        Me.txtServerUser.Location = New System.Drawing.Point(43, 45)
        Me.txtServerUser.Name = "txtServerUser"
        Me.txtServerUser.Size = New System.Drawing.Size(108, 22)
        Me.txtServerUser.TabIndex = 13
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(30, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "User"
        '
        'txtServerMachine
        '
        Me.txtServerMachine.Location = New System.Drawing.Point(64, 17)
        Me.txtServerMachine.Name = "txtServerMachine"
        Me.txtServerMachine.Size = New System.Drawing.Size(87, 22)
        Me.txtServerMachine.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(7, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Machine"
        '
        'Timer
        '
        Me.Timer.Enabled = True
        '
        'gpShutdown
        '
        Me.gpShutdown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gpShutdown.Controls.Add(Me.cmdShutdown)
        Me.gpShutdown.Controls.Add(Me.cbShutdown)
        Me.gpShutdown.Controls.Add(Me.chkForceShutdown)
        Me.gpShutdown.Location = New System.Drawing.Point(13, 153)
        Me.gpShutdown.Name = "gpShutdown"
        Me.gpShutdown.Size = New System.Drawing.Size(308, 53)
        Me.gpShutdown.TabIndex = 8
        Me.gpShutdown.TabStop = False
        Me.gpShutdown.Text = "Shutdown local system"
        '
        'cmdShutdown
        '
        Me.cmdShutdown.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShutdown.Location = New System.Drawing.Point(260, 17)
        Me.cmdShutdown.Name = "cmdShutdown"
        Me.cmdShutdown.Size = New System.Drawing.Size(42, 23)
        Me.cmdShutdown.TabIndex = 2
        Me.cmdShutdown.Text = "Go"
        Me.cmdShutdown.UseVisualStyleBackColor = True
        '
        'cbShutdown
        '
        Me.cbShutdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShutdown.FormattingEnabled = True
        Me.cbShutdown.Items.AddRange(New Object() {"Restart", "Shutdown", "Poweroff", "Sleep", "Poweroff", "Logoff", "Lock"})
        Me.cbShutdown.Location = New System.Drawing.Point(70, 19)
        Me.cbShutdown.Name = "cbShutdown"
        Me.cbShutdown.Size = New System.Drawing.Size(184, 21)
        Me.cbShutdown.TabIndex = 1
        '
        'chkForceShutdown
        '
        Me.chkForceShutdown.AutoSize = True
        Me.chkForceShutdown.Location = New System.Drawing.Point(10, 21)
        Me.chkForceShutdown.Name = "chkForceShutdown"
        Me.chkForceShutdown.Size = New System.Drawing.Size(54, 17)
        Me.chkForceShutdown.TabIndex = 0
        Me.chkForceShutdown.Text = "Force"
        Me.chkForceShutdown.UseVisualStyleBackColor = True
        '
        'cmdTerminal
        '
        Me.cmdTerminal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdTerminal.Location = New System.Drawing.Point(172, 228)
        Me.cmdTerminal.Name = "cmdTerminal"
        Me.cmdTerminal.Size = New System.Drawing.Size(146, 23)
        Me.cmdTerminal.TabIndex = 16
        Me.cmdTerminal.Text = "Terminal Services Client"
        Me.cmdTerminal.UseVisualStyleBackColor = True
        '
        'optSnapshot
        '
        Me.optSnapshot.AutoSize = True
        Me.optSnapshot.Location = New System.Drawing.Point(11, 35)
        Me.optSnapshot.Name = "optSnapshot"
        Me.optSnapshot.Size = New System.Drawing.Size(130, 17)
        Me.optSnapshot.TabIndex = 3
        Me.optSnapshot.Text = "System snapshot file"
        Me.optSnapshot.UseVisualStyleBackColor = True
        '
        'gpSnapshot
        '
        Me.gpSnapshot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gpSnapshot.Controls.Add(Me.cmdSSFileInfos)
        Me.gpSnapshot.Controls.Add(Me.cmdBrowseSSFile)
        Me.gpSnapshot.Controls.Add(Me.txtSSFile)
        Me.gpSnapshot.Controls.Add(Me.Label4)
        Me.gpSnapshot.Location = New System.Drawing.Point(13, 219)
        Me.gpSnapshot.Name = "gpSnapshot"
        Me.gpSnapshot.Size = New System.Drawing.Size(314, 51)
        Me.gpSnapshot.TabIndex = 18
        Me.gpSnapshot.TabStop = False
        Me.gpSnapshot.Visible = False
        '
        'cmdBrowseSSFile
        '
        Me.cmdBrowseSSFile.Location = New System.Drawing.Point(252, 15)
        Me.cmdBrowseSSFile.Name = "cmdBrowseSSFile"
        Me.cmdBrowseSSFile.Size = New System.Drawing.Size(25, 23)
        Me.cmdBrowseSSFile.TabIndex = 12
        Me.cmdBrowseSSFile.Text = "..."
        Me.cmdBrowseSSFile.UseVisualStyleBackColor = True
        '
        'txtSSFile
        '
        Me.txtSSFile.Location = New System.Drawing.Point(128, 17)
        Me.txtSSFile.Name = "txtSSFile"
        Me.txtSSFile.Size = New System.Drawing.Size(118, 22)
        Me.txtSSFile.TabIndex = 11
        Me.txtSSFile.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(115, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "System Snapshot File"
        '
        'openFile
        '
        Me.openFile.Filter = "System Snapshot File (*.ssf)|*.ssf|All Files (*.*)|*.*"
        Me.openFile.RestoreDirectory = True
        Me.openFile.SupportMultiDottedExtensions = True
        Me.openFile.Title = "Choose System Snapshot File to open"
        '
        'lvData
        '
        Me.lvData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lvData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvData.FullRowSelect = True
        Me.lvData.Location = New System.Drawing.Point(345, 3)
        Me.lvData.Name = "lvData"
        Me.lvData.OverriddenDoubleBuffered = True
        Me.lvData.Size = New System.Drawing.Size(289, 326)
        Me.lvData.TabIndex = 17
        Me.lvData.UseCompatibleStateImageBehavior = False
        Me.lvData.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Time"
        Me.ColumnHeader1.Width = 143
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Command received"
        Me.ColumnHeader2.Width = 349
        '
        'cmdSSFileInfos
        '
        Me.cmdSSFileInfos.Image = Global.My.Resources.Resources.information_frame
        Me.cmdSSFileInfos.Location = New System.Drawing.Point(283, 14)
        Me.cmdSSFileInfos.Name = "cmdSSFileInfos"
        Me.cmdSSFileInfos.Size = New System.Drawing.Size(25, 25)
        Me.cmdSSFileInfos.TabIndex = 13
        Me.cmdSSFileInfos.UseVisualStyleBackColor = True
        '
        'frmConnection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 330)
        Me.Controls.Add(Me.gpSnapshot)
        Me.Controls.Add(Me.optSnapshot)
        Me.Controls.Add(Me.lvData)
        Me.Controls.Add(Me.cmdTerminal)
        Me.Controls.Add(Me.gpShutdown)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.optServer)
        Me.Controls.Add(Me.optWMI)
        Me.Controls.Add(Me.optLocal)
        Me.Controls.Add(Me.gpWMI)
        Me.Controls.Add(Me.gpServer)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmConnection"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Connected"
        Me.gpServer.ResumeLayout(False)
        Me.gpServer.PerformLayout()
        Me.gpWMI.ResumeLayout(False)
        Me.gpWMI.PerformLayout()
        Me.gpShutdown.ResumeLayout(False)
        Me.gpShutdown.PerformLayout()
        Me.gpSnapshot.ResumeLayout(False)
        Me.gpSnapshot.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents optLocal As System.Windows.Forms.RadioButton
    Friend WithEvents optWMI As System.Windows.Forms.RadioButton
    Friend WithEvents optServer As System.Windows.Forms.RadioButton
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents gpServer As System.Windows.Forms.GroupBox
    Friend WithEvents gpWMI As System.Windows.Forms.GroupBox
    Friend WithEvents txtServerPassword As SecurePasswordTextBox.SecureTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtServerUser As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtServerMachine As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtServerIP As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Timer As System.Windows.Forms.Timer
    Friend WithEvents gpShutdown As System.Windows.Forms.GroupBox
    Friend WithEvents cmdShutdown As System.Windows.Forms.Button
    Friend WithEvents cbShutdown As System.Windows.Forms.ComboBox
    Friend WithEvents chkForceShutdown As System.Windows.Forms.CheckBox
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdTerminal As System.Windows.Forms.Button
    Friend WithEvents cmdShowDatas As System.Windows.Forms.Button
    Friend WithEvents lvData As DoubleBufferedLV
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents optSnapshot As System.Windows.Forms.RadioButton
    Friend WithEvents gpSnapshot As System.Windows.Forms.GroupBox
    Friend WithEvents cmdBrowseSSFile As System.Windows.Forms.Button
    Friend WithEvents txtSSFile As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents openFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdSSFileInfos As System.Windows.Forms.Button
End Class
