<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogOptions
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
        Me.logInterval = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Line1 = New YAPM.HorizontalLine
        Me.captureDeleted = New System.Windows.Forms.CheckBox
        Me.captureCreated = New System.Windows.Forms.CheckBox
        Me.captureWindows = New System.Windows.Forms.CheckBox
        Me.captureThreads = New System.Windows.Forms.CheckBox
        Me.captureServices = New System.Windows.Forms.CheckBox
        Me.captureNetwork = New System.Windows.Forms.CheckBox
        Me.captureModules = New System.Windows.Forms.CheckBox
        Me.captureMemoryRegions = New System.Windows.Forms.CheckBox
        Me.captureHandles = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.showDeleted = New System.Windows.Forms.CheckBox
        Me.showCreated = New System.Windows.Forms.CheckBox
        Me.showWindows = New System.Windows.Forms.CheckBox
        Me.showThreads = New System.Windows.Forms.CheckBox
        Me.showServices = New System.Windows.Forms.CheckBox
        Me.showNetwork = New System.Windows.Forms.CheckBox
        Me.showModules = New System.Windows.Forms.CheckBox
        Me.showMemoryRegions = New System.Windows.Forms.CheckBox
        Me.showHandles = New System.Windows.Forms.CheckBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me._autoScroll = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.Line2 = New YAPM.HorizontalLine
        CType(Me.logInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'logInterval
        '
        Me.logInterval.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.logInterval.Location = New System.Drawing.Point(61, 264)
        Me.logInterval.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.logInterval.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.logInterval.Name = "logInterval"
        Me.logInterval.Size = New System.Drawing.Size(52, 22)
        Me.logInterval.TabIndex = 6
        Me.logInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.logInterval.Value = New Decimal(New Integer() {1000, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 266)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Interval"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Line1)
        Me.GroupBox1.Controls.Add(Me.captureDeleted)
        Me.GroupBox1.Controls.Add(Me.captureCreated)
        Me.GroupBox1.Controls.Add(Me.captureWindows)
        Me.GroupBox1.Controls.Add(Me.captureThreads)
        Me.GroupBox1.Controls.Add(Me.captureServices)
        Me.GroupBox1.Controls.Add(Me.captureNetwork)
        Me.GroupBox1.Controls.Add(Me.captureModules)
        Me.GroupBox1.Controls.Add(Me.captureMemoryRegions)
        Me.GroupBox1.Controls.Add(Me.captureHandles)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(128, 240)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Capture"
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Line1.Location = New System.Drawing.Point(14, 181)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(100, 1)
        Me.Line1.TabIndex = 12
        '
        'captureDeleted
        '
        Me.captureDeleted.AutoSize = True
        Me.captureDeleted.Checked = True
        Me.captureDeleted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureDeleted.Location = New System.Drawing.Point(6, 215)
        Me.captureDeleted.Name = "captureDeleted"
        Me.captureDeleted.Size = New System.Drawing.Size(66, 17)
        Me.captureDeleted.TabIndex = 11
        Me.captureDeleted.Text = "Deleted"
        Me.captureDeleted.UseVisualStyleBackColor = True
        '
        'captureCreated
        '
        Me.captureCreated.AutoSize = True
        Me.captureCreated.Checked = True
        Me.captureCreated.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureCreated.Location = New System.Drawing.Point(6, 192)
        Me.captureCreated.Name = "captureCreated"
        Me.captureCreated.Size = New System.Drawing.Size(66, 17)
        Me.captureCreated.TabIndex = 10
        Me.captureCreated.Text = "Created"
        Me.captureCreated.UseVisualStyleBackColor = True
        '
        'captureWindows
        '
        Me.captureWindows.AutoSize = True
        Me.captureWindows.Checked = True
        Me.captureWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureWindows.Location = New System.Drawing.Point(8, 157)
        Me.captureWindows.Name = "captureWindows"
        Me.captureWindows.Size = New System.Drawing.Size(75, 17)
        Me.captureWindows.TabIndex = 7
        Me.captureWindows.Text = "Windows"
        Me.captureWindows.UseVisualStyleBackColor = True
        '
        'captureThreads
        '
        Me.captureThreads.AutoSize = True
        Me.captureThreads.Checked = True
        Me.captureThreads.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureThreads.Location = New System.Drawing.Point(8, 134)
        Me.captureThreads.Name = "captureThreads"
        Me.captureThreads.Size = New System.Drawing.Size(66, 17)
        Me.captureThreads.TabIndex = 6
        Me.captureThreads.Text = "Threads"
        Me.captureThreads.UseVisualStyleBackColor = True
        '
        'captureServices
        '
        Me.captureServices.AutoSize = True
        Me.captureServices.Checked = True
        Me.captureServices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureServices.Location = New System.Drawing.Point(8, 111)
        Me.captureServices.Name = "captureServices"
        Me.captureServices.Size = New System.Drawing.Size(66, 17)
        Me.captureServices.TabIndex = 5
        Me.captureServices.Text = "Services"
        Me.captureServices.UseVisualStyleBackColor = True
        '
        'captureNetwork
        '
        Me.captureNetwork.AutoSize = True
        Me.captureNetwork.Checked = True
        Me.captureNetwork.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureNetwork.Location = New System.Drawing.Point(8, 88)
        Me.captureNetwork.Name = "captureNetwork"
        Me.captureNetwork.Size = New System.Drawing.Size(70, 17)
        Me.captureNetwork.TabIndex = 4
        Me.captureNetwork.Text = "Network"
        Me.captureNetwork.UseVisualStyleBackColor = True
        '
        'captureModules
        '
        Me.captureModules.AutoSize = True
        Me.captureModules.Checked = True
        Me.captureModules.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureModules.Location = New System.Drawing.Point(8, 65)
        Me.captureModules.Name = "captureModules"
        Me.captureModules.Size = New System.Drawing.Size(71, 17)
        Me.captureModules.TabIndex = 3
        Me.captureModules.Text = "Modules"
        Me.captureModules.UseVisualStyleBackColor = True
        '
        'captureMemoryRegions
        '
        Me.captureMemoryRegions.AutoSize = True
        Me.captureMemoryRegions.Checked = True
        Me.captureMemoryRegions.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureMemoryRegions.Location = New System.Drawing.Point(8, 42)
        Me.captureMemoryRegions.Name = "captureMemoryRegions"
        Me.captureMemoryRegions.Size = New System.Drawing.Size(109, 17)
        Me.captureMemoryRegions.TabIndex = 2
        Me.captureMemoryRegions.Text = "Memory regions"
        Me.captureMemoryRegions.UseVisualStyleBackColor = True
        '
        'captureHandles
        '
        Me.captureHandles.AutoSize = True
        Me.captureHandles.Checked = True
        Me.captureHandles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.captureHandles.Location = New System.Drawing.Point(8, 19)
        Me.captureHandles.Name = "captureHandles"
        Me.captureHandles.Size = New System.Drawing.Size(68, 17)
        Me.captureHandles.TabIndex = 1
        Me.captureHandles.Text = "Handles"
        Me.captureHandles.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Line2)
        Me.GroupBox2.Controls.Add(Me.showDeleted)
        Me.GroupBox2.Controls.Add(Me.showCreated)
        Me.GroupBox2.Controls.Add(Me.showWindows)
        Me.GroupBox2.Controls.Add(Me.showThreads)
        Me.GroupBox2.Controls.Add(Me.showServices)
        Me.GroupBox2.Controls.Add(Me.showNetwork)
        Me.GroupBox2.Controls.Add(Me.showModules)
        Me.GroupBox2.Controls.Add(Me.showMemoryRegions)
        Me.GroupBox2.Controls.Add(Me.showHandles)
        Me.GroupBox2.Location = New System.Drawing.Point(146, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(128, 240)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Show"
        '
        'showDeleted
        '
        Me.showDeleted.AutoSize = True
        Me.showDeleted.Checked = True
        Me.showDeleted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.showDeleted.Location = New System.Drawing.Point(8, 215)
        Me.showDeleted.Name = "showDeleted"
        Me.showDeleted.Size = New System.Drawing.Size(66, 17)
        Me.showDeleted.TabIndex = 9
        Me.showDeleted.Text = "Deleted"
        Me.showDeleted.UseVisualStyleBackColor = True
        '
        'showCreated
        '
        Me.showCreated.AutoSize = True
        Me.showCreated.Checked = True
        Me.showCreated.CheckState = System.Windows.Forms.CheckState.Checked
        Me.showCreated.Location = New System.Drawing.Point(8, 192)
        Me.showCreated.Name = "showCreated"
        Me.showCreated.Size = New System.Drawing.Size(66, 17)
        Me.showCreated.TabIndex = 8
        Me.showCreated.Text = "Created"
        Me.showCreated.UseVisualStyleBackColor = True
        '
        'showWindows
        '
        Me.showWindows.AutoSize = True
        Me.showWindows.Location = New System.Drawing.Point(8, 157)
        Me.showWindows.Name = "showWindows"
        Me.showWindows.Size = New System.Drawing.Size(75, 17)
        Me.showWindows.TabIndex = 7
        Me.showWindows.Text = "Windows"
        Me.showWindows.UseVisualStyleBackColor = True
        '
        'showThreads
        '
        Me.showThreads.AutoSize = True
        Me.showThreads.Location = New System.Drawing.Point(8, 134)
        Me.showThreads.Name = "showThreads"
        Me.showThreads.Size = New System.Drawing.Size(66, 17)
        Me.showThreads.TabIndex = 6
        Me.showThreads.Text = "Threads"
        Me.showThreads.UseVisualStyleBackColor = True
        '
        'showServices
        '
        Me.showServices.AutoSize = True
        Me.showServices.Location = New System.Drawing.Point(8, 111)
        Me.showServices.Name = "showServices"
        Me.showServices.Size = New System.Drawing.Size(66, 17)
        Me.showServices.TabIndex = 5
        Me.showServices.Text = "Services"
        Me.showServices.UseVisualStyleBackColor = True
        '
        'showNetwork
        '
        Me.showNetwork.AutoSize = True
        Me.showNetwork.Location = New System.Drawing.Point(8, 88)
        Me.showNetwork.Name = "showNetwork"
        Me.showNetwork.Size = New System.Drawing.Size(70, 17)
        Me.showNetwork.TabIndex = 4
        Me.showNetwork.Text = "Network"
        Me.showNetwork.UseVisualStyleBackColor = True
        '
        'showModules
        '
        Me.showModules.AutoSize = True
        Me.showModules.Location = New System.Drawing.Point(8, 65)
        Me.showModules.Name = "showModules"
        Me.showModules.Size = New System.Drawing.Size(71, 17)
        Me.showModules.TabIndex = 3
        Me.showModules.Text = "Modules"
        Me.showModules.UseVisualStyleBackColor = True
        '
        'showMemoryRegions
        '
        Me.showMemoryRegions.AutoSize = True
        Me.showMemoryRegions.Location = New System.Drawing.Point(8, 42)
        Me.showMemoryRegions.Name = "showMemoryRegions"
        Me.showMemoryRegions.Size = New System.Drawing.Size(109, 17)
        Me.showMemoryRegions.TabIndex = 2
        Me.showMemoryRegions.Text = "Memory regions"
        Me.showMemoryRegions.UseVisualStyleBackColor = True
        '
        'showHandles
        '
        Me.showHandles.AutoSize = True
        Me.showHandles.Location = New System.Drawing.Point(8, 19)
        Me.showHandles.Name = "showHandles"
        Me.showHandles.Size = New System.Drawing.Size(68, 17)
        Me.showHandles.TabIndex = 1
        Me.showHandles.Text = "Handles"
        Me.showHandles.UseVisualStyleBackColor = True
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(40, 295)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 9
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        '_autoScroll
        '
        Me._autoScroll.AutoSize = True
        Me._autoScroll.Checked = True
        Me._autoScroll.CheckState = System.Windows.Forms.CheckState.Checked
        Me._autoScroll.Location = New System.Drawing.Point(154, 265)
        Me._autoScroll.Name = "_autoScroll"
        Me._autoScroll.Size = New System.Drawing.Size(81, 17)
        Me._autoScroll.TabIndex = 10
        Me._autoScroll.Text = "Auto scroll"
        Me._autoScroll.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(171, 295)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Line2.Location = New System.Drawing.Point(14, 181)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(100, 1)
        Me.Line2.TabIndex = 13
        '
        'frmLogOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(287, 326)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me._autoScroll)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.logInterval)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogOptions"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Options"
        CType(Me.logInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents logInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents captureHandles As System.Windows.Forms.CheckBox
    Friend WithEvents captureWindows As System.Windows.Forms.CheckBox
    Friend WithEvents captureThreads As System.Windows.Forms.CheckBox
    Friend WithEvents captureServices As System.Windows.Forms.CheckBox
    Friend WithEvents captureNetwork As System.Windows.Forms.CheckBox
    Friend WithEvents captureModules As System.Windows.Forms.CheckBox
    Friend WithEvents captureMemoryRegions As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents showWindows As System.Windows.Forms.CheckBox
    Friend WithEvents showThreads As System.Windows.Forms.CheckBox
    Friend WithEvents showServices As System.Windows.Forms.CheckBox
    Friend WithEvents showNetwork As System.Windows.Forms.CheckBox
    Friend WithEvents showModules As System.Windows.Forms.CheckBox
    Friend WithEvents showMemoryRegions As System.Windows.Forms.CheckBox
    Friend WithEvents showHandles As System.Windows.Forms.CheckBox
    Friend WithEvents showDeleted As System.Windows.Forms.CheckBox
    Friend WithEvents showCreated As System.Windows.Forms.CheckBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents captureDeleted As System.Windows.Forms.CheckBox
    Friend WithEvents captureCreated As System.Windows.Forms.CheckBox
    Friend WithEvents _autoScroll As System.Windows.Forms.CheckBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents Line1 As HorizontalLine
    Friend WithEvents Line2 As YAPM.HorizontalLine
End Class
