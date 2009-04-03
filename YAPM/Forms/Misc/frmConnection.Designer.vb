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
        Me.optLocal = New System.Windows.Forms.RadioButton
        Me.optWMI = New System.Windows.Forms.RadioButton
        Me.optServer = New System.Windows.Forms.RadioButton
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.cmdConnect = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.gpServer = New System.Windows.Forms.GroupBox
        Me.txtServerIP = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.gpWMI = New System.Windows.Forms.GroupBox
        Me.txtServerPassword = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtServerUser = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtServerMachine = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.gpServer.SuspendLayout()
        Me.gpWMI.SuspendLayout()
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
        Me.txtDesc.Location = New System.Drawing.Point(11, 35)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(316, 81)
        Me.txtDesc.TabIndex = 3
        '
        'cmdConnect
        '
        Me.cmdConnect.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConnect.Location = New System.Drawing.Point(42, 206)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(103, 28)
        Me.cmdConnect.TabIndex = 4
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(188, 206)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(103, 28)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Hide"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'gpServer
        '
        Me.gpServer.Controls.Add(Me.txtServerIP)
        Me.gpServer.Controls.Add(Me.Label17)
        Me.gpServer.Location = New System.Drawing.Point(13, 125)
        Me.gpServer.Name = "gpServer"
        Me.gpServer.Size = New System.Drawing.Size(314, 73)
        Me.gpServer.TabIndex = 6
        Me.gpServer.TabStop = False
        '
        'txtServerIP
        '
        Me.txtServerIP.Location = New System.Drawing.Point(29, 17)
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.Size = New System.Drawing.Size(87, 22)
        Me.txtServerIP.TabIndex = 11
        Me.txtServerIP.Text = "192.168.0.4"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(7, 20)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(16, 13)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "IP"
        '
        'gpWMI
        '
        Me.gpWMI.Controls.Add(Me.txtServerPassword)
        Me.gpWMI.Controls.Add(Me.Label14)
        Me.gpWMI.Controls.Add(Me.txtServerUser)
        Me.gpWMI.Controls.Add(Me.Label13)
        Me.gpWMI.Controls.Add(Me.txtServerMachine)
        Me.gpWMI.Controls.Add(Me.Label12)
        Me.gpWMI.Location = New System.Drawing.Point(13, 125)
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
        Me.txtServerMachine.Text = "localhost"
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
        'frmConnection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 243)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.optServer)
        Me.Controls.Add(Me.optWMI)
        Me.Controls.Add(Me.optLocal)
        Me.Controls.Add(Me.gpServer)
        Me.Controls.Add(Me.gpWMI)
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
    Friend WithEvents txtServerPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtServerUser As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtServerMachine As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtServerIP As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
