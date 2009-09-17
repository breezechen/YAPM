<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateService
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
        Dim SecureString2 As System.Security.SecureString = New System.Security.SecureString
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.label5 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.cbErrControl = New System.Windows.Forms.ComboBox
        Me.cbStartType = New System.Windows.Forms.ComboBox
        Me.cbServType = New System.Windows.Forms.ComboBox
        Me.optLocal = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtServerPassword = New SecurePasswordTextBox.SecureTextBox
        Me.lblPwd = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.lblUser = New System.Windows.Forms.Label
        Me.txtMachine = New System.Windows.Forms.TextBox
        Me.lblMachine = New System.Windows.Forms.Label
        Me.optRemote = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtArgs = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtDisplayName = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtServiceName = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 359)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(327, 29)
        Me.TableLayoutPanel1.TabIndex = 30
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(48, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(211, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(9, 176)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(72, 13)
        Me.label5.TabIndex = 42
        Me.label5.Text = "Error control"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(10, 150)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(67, 13)
        Me.label3.TabIndex = 40
        Me.label3.Text = "Service type"
        '
        'cbErrControl
        '
        Me.cbErrControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbErrControl.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbErrControl.FormattingEnabled = True
        Me.cbErrControl.Items.AddRange(New Object() {"Critical", "Ignore", "Normal", "Severe", "Unknown"})
        Me.cbErrControl.Location = New System.Drawing.Point(90, 173)
        Me.cbErrControl.Name = "cbErrControl"
        Me.cbErrControl.Size = New System.Drawing.Size(237, 21)
        Me.cbErrControl.TabIndex = 35
        '
        'cbStartType
        '
        Me.cbStartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbStartType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbStartType.FormattingEnabled = True
        Me.cbStartType.Items.AddRange(New Object() {"BootStart", "SystemStart", "AutoStart", "DemandStart"})
        Me.cbStartType.Location = New System.Drawing.Point(90, 121)
        Me.cbStartType.Name = "cbStartType"
        Me.cbStartType.Size = New System.Drawing.Size(237, 21)
        Me.cbStartType.TabIndex = 34
        '
        'cbServType
        '
        Me.cbServType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbServType.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbServType.FormattingEnabled = True
        Me.cbServType.Items.AddRange(New Object() {"FileSystemDriver", "KernelDriver", "Adapter", "RecognizerDriver", "Win32OwnProcess", "Win32ShareProcess", "InteractiveProcess"})
        Me.cbServType.Location = New System.Drawing.Point(90, 147)
        Me.cbServType.Name = "cbServType"
        Me.cbServType.Size = New System.Drawing.Size(237, 21)
        Me.cbServType.TabIndex = 33
        '
        'optLocal
        '
        Me.optLocal.AutoSize = True
        Me.optLocal.Checked = True
        Me.optLocal.Location = New System.Drawing.Point(12, 21)
        Me.optLocal.Name = "optLocal"
        Me.optLocal.Size = New System.Drawing.Size(51, 17)
        Me.optLocal.TabIndex = 44
        Me.optLocal.TabStop = True
        Me.optLocal.Text = "Local"
        Me.optLocal.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtServerPassword)
        Me.GroupBox1.Controls.Add(Me.lblPwd)
        Me.GroupBox1.Controls.Add(Me.txtUser)
        Me.GroupBox1.Controls.Add(Me.lblUser)
        Me.GroupBox1.Controls.Add(Me.txtMachine)
        Me.GroupBox1.Controls.Add(Me.lblMachine)
        Me.GroupBox1.Controls.Add(Me.optRemote)
        Me.GroupBox1.Controls.Add(Me.optLocal)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 220)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(333, 128)
        Me.GroupBox1.TabIndex = 45
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Machine"
        '
        'txtServerPassword
        '
        Me.txtServerPassword.Enabled = False
        Me.txtServerPassword.Location = New System.Drawing.Point(122, 98)
        Me.txtServerPassword.Name = "txtServerPassword"
        Me.txtServerPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtServerPassword.SecureText = SecureString2
        Me.txtServerPassword.Size = New System.Drawing.Size(205, 22)
        Me.txtServerPassword.TabIndex = 51
        Me.txtServerPassword.UseSystemPasswordChar = True
        '
        'lblPwd
        '
        Me.lblPwd.AutoSize = True
        Me.lblPwd.Enabled = False
        Me.lblPwd.Location = New System.Drawing.Point(60, 101)
        Me.lblPwd.Name = "lblPwd"
        Me.lblPwd.Size = New System.Drawing.Size(56, 13)
        Me.lblPwd.TabIndex = 50
        Me.lblPwd.Text = "Password"
        '
        'txtUser
        '
        Me.txtUser.Enabled = False
        Me.txtUser.Location = New System.Drawing.Point(122, 73)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(205, 22)
        Me.txtUser.TabIndex = 49
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Enabled = False
        Me.lblUser.Location = New System.Drawing.Point(60, 76)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(30, 13)
        Me.lblUser.TabIndex = 48
        Me.lblUser.Text = "User"
        '
        'txtMachine
        '
        Me.txtMachine.Enabled = False
        Me.txtMachine.Location = New System.Drawing.Point(122, 48)
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.Size = New System.Drawing.Size(205, 22)
        Me.txtMachine.TabIndex = 47
        '
        'lblMachine
        '
        Me.lblMachine.AutoSize = True
        Me.lblMachine.Enabled = False
        Me.lblMachine.Location = New System.Drawing.Point(60, 51)
        Me.lblMachine.Name = "lblMachine"
        Me.lblMachine.Size = New System.Drawing.Size(51, 13)
        Me.lblMachine.TabIndex = 46
        Me.lblMachine.Text = "Machine"
        '
        'optRemote
        '
        Me.optRemote.AutoSize = True
        Me.optRemote.Location = New System.Drawing.Point(69, 21)
        Me.optRemote.Name = "optRemote"
        Me.optRemote.Size = New System.Drawing.Size(64, 17)
        Me.optRemote.TabIndex = 45
        Me.optRemote.Text = "Remote"
        Me.optRemote.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmdBrowse)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtArgs)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtPath)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtDisplayName)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtServiceName)
        Me.GroupBox2.Controls.Add(Me.label5)
        Me.GroupBox2.Controls.Add(Me.cbErrControl)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.cbStartType)
        Me.GroupBox2.Controls.Add(Me.label3)
        Me.GroupBox2.Controls.Add(Me.cbServType)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(333, 202)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New service"
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(298, 71)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(29, 23)
        Me.cmdBrowse.TabIndex = 43
        Me.cmdBrowse.Text = "..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 124)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 13)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Start type"
        '
        'txtArgs
        '
        Me.txtArgs.Location = New System.Drawing.Point(90, 96)
        Me.txtArgs.Name = "txtArgs"
        Me.txtArgs.Size = New System.Drawing.Size(237, 22)
        Me.txtArgs.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 99)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(63, 13)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Arguments"
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(90, 71)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(202, 22)
        Me.txtPath.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 74)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(30, 13)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Path"
        '
        'txtDisplayName
        '
        Me.txtDisplayName.Location = New System.Drawing.Point(90, 46)
        Me.txtDisplayName.Name = "txtDisplayName"
        Me.txtDisplayName.Size = New System.Drawing.Size(237, 22)
        Me.txtDisplayName.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Display name"
        '
        'txtServiceName
        '
        Me.txtServiceName.Location = New System.Drawing.Point(90, 20)
        Me.txtServiceName.Name = "txtServiceName"
        Me.txtServiceName.Size = New System.Drawing.Size(237, 22)
        Me.txtServiceName.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Name"
        '
        'frmCreateService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 397)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreateService"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create service"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents cbErrControl As System.Windows.Forms.ComboBox
    Private WithEvents cbStartType As System.Windows.Forms.ComboBox
    Private WithEvents cbServType As System.Windows.Forms.ComboBox
    Friend WithEvents optLocal As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMachine As System.Windows.Forms.TextBox
    Friend WithEvents lblMachine As System.Windows.Forms.Label
    Friend WithEvents optRemote As System.Windows.Forms.RadioButton
    Friend WithEvents lblPwd As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents txtServerPassword As SecurePasswordTextBox.SecureTextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtArgs As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtDisplayName As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtServiceName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label

End Class
