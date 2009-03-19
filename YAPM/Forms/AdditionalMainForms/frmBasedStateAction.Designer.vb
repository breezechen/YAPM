<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBasedStateAction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBasedStateAction))
        Me.timerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.EnableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DisableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.gp = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtParam2Val = New System.Windows.Forms.TextBox
        Me.txtParam2Desc = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtParam1Val = New System.Windows.Forms.TextBox
        Me.txtParam1Desc = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cbAction = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblThresholdDesc = New System.Windows.Forms.TextBox
        Me.cbCounter = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtThreshold = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbOperator = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdBrowseProcessPath = New System.Windows.Forms.Button
        Me.cmdBrowseProcessId = New System.Windows.Forms.Button
        Me.cmdBrowseProcessName = New System.Windows.Forms.Button
        Me.txtProcessPath = New System.Windows.Forms.TextBox
        Me.txtProcessID = New System.Windows.Forms.TextBox
        Me.txtProcessName = New System.Windows.Forms.TextBox
        Me.chkCheckProcessPath = New System.Windows.Forms.CheckBox
        Me.chkCheckProcessID = New System.Windows.Forms.CheckBox
        Me.chkCheckProcessName = New System.Windows.Forms.CheckBox
        Me.cmdKO = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.DisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SimulationConsoleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.lv = New YAPM.DoubleBufferedLV
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ContextMenuStrip1.SuspendLayout()
        Me.gp.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'timerRefresh
        '
        Me.timerRefresh.Enabled = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowToolStripMenuItem, Me.CloseToolStripMenuItem, Me.ToolStripMenuItem1, Me.EnableToolStripMenuItem, Me.DisableToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(118, 98)
        '
        'ShowToolStripMenuItem
        '
        Me.ShowToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.plus_circle
        Me.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem"
        Me.ShowToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.ShowToolStripMenuItem.Text = "Add"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Image = Global.YAPM.My.Resources.Resources.cross_circle1
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.CloseToolStripMenuItem.Text = "Remove"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(114, 6)
        '
        'EnableToolStripMenuItem
        '
        Me.EnableToolStripMenuItem.Name = "EnableToolStripMenuItem"
        Me.EnableToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.EnableToolStripMenuItem.Text = "Enable"
        '
        'DisableToolStripMenuItem
        '
        Me.DisableToolStripMenuItem.Name = "DisableToolStripMenuItem"
        Me.DisableToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.DisableToolStripMenuItem.Text = "Disable"
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "default")
        '
        'gp
        '
        Me.gp.BackColor = System.Drawing.Color.White
        Me.gp.Controls.Add(Me.GroupBox3)
        Me.gp.Controls.Add(Me.GroupBox2)
        Me.gp.Controls.Add(Me.GroupBox1)
        Me.gp.Controls.Add(Me.cmdKO)
        Me.gp.Controls.Add(Me.cmdAdd)
        Me.gp.Location = New System.Drawing.Point(175, 58)
        Me.gp.Name = "gp"
        Me.gp.Size = New System.Drawing.Size(314, 372)
        Me.gp.TabIndex = 6
        Me.gp.TabStop = False
        Me.gp.Text = "Add a state based action"
        Me.gp.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtParam2Val)
        Me.GroupBox3.Controls.Add(Me.txtParam2Desc)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txtParam1Val)
        Me.GroupBox3.Controls.Add(Me.txtParam1Desc)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cbAction)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 230)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(281, 101)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Action"
        '
        'txtParam2Val
        '
        Me.txtParam2Val.Location = New System.Drawing.Point(185, 66)
        Me.txtParam2Val.Name = "txtParam2Val"
        Me.txtParam2Val.Size = New System.Drawing.Size(77, 22)
        Me.txtParam2Val.TabIndex = 17
        '
        'txtParam2Desc
        '
        Me.txtParam2Desc.Location = New System.Drawing.Point(55, 66)
        Me.txtParam2Desc.Name = "txtParam2Desc"
        Me.txtParam2Desc.ReadOnly = True
        Me.txtParam2Desc.Size = New System.Drawing.Size(124, 22)
        Me.txtParam2Desc.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Param2"
        '
        'txtParam1Val
        '
        Me.txtParam1Val.Location = New System.Drawing.Point(185, 43)
        Me.txtParam1Val.Name = "txtParam1Val"
        Me.txtParam1Val.Size = New System.Drawing.Size(77, 22)
        Me.txtParam1Val.TabIndex = 14
        '
        'txtParam1Desc
        '
        Me.txtParam1Desc.Location = New System.Drawing.Point(55, 43)
        Me.txtParam1Desc.Name = "txtParam1Desc"
        Me.txtParam1Desc.ReadOnly = True
        Me.txtParam1Desc.Size = New System.Drawing.Size(124, 22)
        Me.txtParam1Desc.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Param1"
        '
        'cbAction
        '
        Me.cbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAction.FormattingEnabled = True
        Me.cbAction.Location = New System.Drawing.Point(55, 21)
        Me.cbAction.Name = "cbAction"
        Me.cbAction.Size = New System.Drawing.Size(207, 21)
        Me.cbAction.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Action"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblThresholdDesc)
        Me.GroupBox2.Controls.Add(Me.cbCounter)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtThreshold)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cbOperator)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 126)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 98)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "State to monitor"
        '
        'lblThresholdDesc
        '
        Me.lblThresholdDesc.Location = New System.Drawing.Point(113, 42)
        Me.lblThresholdDesc.Name = "lblThresholdDesc"
        Me.lblThresholdDesc.ReadOnly = True
        Me.lblThresholdDesc.Size = New System.Drawing.Size(149, 22)
        Me.lblThresholdDesc.TabIndex = 14
        '
        'cbCounter
        '
        Me.cbCounter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCounter.FormattingEnabled = True
        Me.cbCounter.Items.AddRange(New Object() {"CpuUsage", "AverageCpuUsage"})
        Me.cbCounter.Location = New System.Drawing.Point(70, 18)
        Me.cbCounter.Name = "cbCounter"
        Me.cbCounter.Size = New System.Drawing.Size(192, 21)
        Me.cbCounter.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Counter"
        '
        'txtThreshold
        '
        Me.txtThreshold.Location = New System.Drawing.Point(70, 68)
        Me.txtThreshold.Name = "txtThreshold"
        Me.txtThreshold.Size = New System.Drawing.Size(192, 22)
        Me.txtThreshold.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Threshold"
        '
        'cbOperator
        '
        Me.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOperator.FormattingEnabled = True
        Me.cbOperator.Items.AddRange(New Object() {"<", "<=", "=", ">=", ">", "!="})
        Me.cbOperator.Location = New System.Drawing.Point(70, 43)
        Me.cbOperator.Name = "cbOperator"
        Me.cbOperator.Size = New System.Drawing.Size(37, 21)
        Me.cbOperator.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Operator"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdBrowseProcessPath)
        Me.GroupBox1.Controls.Add(Me.cmdBrowseProcessId)
        Me.GroupBox1.Controls.Add(Me.cmdBrowseProcessName)
        Me.GroupBox1.Controls.Add(Me.txtProcessPath)
        Me.GroupBox1.Controls.Add(Me.txtProcessID)
        Me.GroupBox1.Controls.Add(Me.txtProcessName)
        Me.GroupBox1.Controls.Add(Me.chkCheckProcessPath)
        Me.GroupBox1.Controls.Add(Me.chkCheckProcessID)
        Me.GroupBox1.Controls.Add(Me.chkCheckProcessName)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 96)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Process"
        '
        'cmdBrowseProcessPath
        '
        Me.cmdBrowseProcessPath.Location = New System.Drawing.Point(237, 64)
        Me.cmdBrowseProcessPath.Name = "cmdBrowseProcessPath"
        Me.cmdBrowseProcessPath.Size = New System.Drawing.Size(25, 23)
        Me.cmdBrowseProcessPath.TabIndex = 8
        Me.cmdBrowseProcessPath.Text = "..."
        Me.cmdBrowseProcessPath.UseVisualStyleBackColor = True
        '
        'cmdBrowseProcessId
        '
        Me.cmdBrowseProcessId.Location = New System.Drawing.Point(237, 40)
        Me.cmdBrowseProcessId.Name = "cmdBrowseProcessId"
        Me.cmdBrowseProcessId.Size = New System.Drawing.Size(25, 23)
        Me.cmdBrowseProcessId.TabIndex = 7
        Me.cmdBrowseProcessId.Text = "..."
        Me.cmdBrowseProcessId.UseVisualStyleBackColor = True
        '
        'cmdBrowseProcessName
        '
        Me.cmdBrowseProcessName.Location = New System.Drawing.Point(237, 17)
        Me.cmdBrowseProcessName.Name = "cmdBrowseProcessName"
        Me.cmdBrowseProcessName.Size = New System.Drawing.Size(25, 23)
        Me.cmdBrowseProcessName.TabIndex = 6
        Me.cmdBrowseProcessName.Text = "..."
        Me.cmdBrowseProcessName.UseVisualStyleBackColor = True
        '
        'txtProcessPath
        '
        Me.txtProcessPath.Location = New System.Drawing.Point(142, 65)
        Me.txtProcessPath.Name = "txtProcessPath"
        Me.txtProcessPath.Size = New System.Drawing.Size(89, 22)
        Me.txtProcessPath.TabIndex = 5
        '
        'txtProcessID
        '
        Me.txtProcessID.Location = New System.Drawing.Point(142, 42)
        Me.txtProcessID.Name = "txtProcessID"
        Me.txtProcessID.Size = New System.Drawing.Size(89, 22)
        Me.txtProcessID.TabIndex = 4
        '
        'txtProcessName
        '
        Me.txtProcessName.Location = New System.Drawing.Point(142, 19)
        Me.txtProcessName.Name = "txtProcessName"
        Me.txtProcessName.Size = New System.Drawing.Size(89, 22)
        Me.txtProcessName.TabIndex = 3
        '
        'chkCheckProcessPath
        '
        Me.chkCheckProcessPath.AutoSize = True
        Me.chkCheckProcessPath.Location = New System.Drawing.Point(6, 67)
        Me.chkCheckProcessPath.Name = "chkCheckProcessPath"
        Me.chkCheckProcessPath.Size = New System.Drawing.Size(126, 17)
        Me.chkCheckProcessPath.TabIndex = 2
        Me.chkCheckProcessPath.Text = "Check process path"
        Me.chkCheckProcessPath.UseVisualStyleBackColor = True
        '
        'chkCheckProcessID
        '
        Me.chkCheckProcessID.AutoSize = True
        Me.chkCheckProcessID.Location = New System.Drawing.Point(6, 44)
        Me.chkCheckProcessID.Name = "chkCheckProcessID"
        Me.chkCheckProcessID.Size = New System.Drawing.Size(113, 17)
        Me.chkCheckProcessID.TabIndex = 1
        Me.chkCheckProcessID.Text = "Check process ID"
        Me.chkCheckProcessID.UseVisualStyleBackColor = True
        '
        'chkCheckProcessName
        '
        Me.chkCheckProcessName.AutoSize = True
        Me.chkCheckProcessName.Location = New System.Drawing.Point(6, 21)
        Me.chkCheckProcessName.Name = "chkCheckProcessName"
        Me.chkCheckProcessName.Size = New System.Drawing.Size(130, 17)
        Me.chkCheckProcessName.TabIndex = 0
        Me.chkCheckProcessName.Text = "Check process name"
        Me.chkCheckProcessName.UseVisualStyleBackColor = True
        '
        'cmdKO
        '
        Me.cmdKO.Location = New System.Drawing.Point(172, 337)
        Me.cmdKO.Name = "cmdKO"
        Me.cmdKO.Size = New System.Drawing.Size(55, 23)
        Me.cmdKO.TabIndex = 7
        Me.cmdKO.Text = "Cancel"
        Me.cmdKO.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Enabled = False
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Location = New System.Drawing.Point(88, 337)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(55, 23)
        Me.cmdAdd.TabIndex = 6
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(665, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DisplayToolStripMenuItem
        '
        Me.DisplayToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SimulationConsoleToolStripMenuItem})
        Me.DisplayToolStripMenuItem.Name = "DisplayToolStripMenuItem"
        Me.DisplayToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.DisplayToolStripMenuItem.Text = "&Display"
        '
        'SimulationConsoleToolStripMenuItem
        '
        Me.SimulationConsoleToolStripMenuItem.Name = "SimulationConsoleToolStripMenuItem"
        Me.SimulationConsoleToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.SimulationConsoleToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.SimulationConsoleToolStripMenuItem.Text = "&Simulation console"
        '
        'lv
        '
        Me.lv.AllowColumnReorder = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lv.ContextMenuStrip = Me.ContextMenuStrip1
        Me.lv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv.FullRowSelect = True
        Me.lv.HideSelection = False
        Me.lv.Location = New System.Drawing.Point(0, 24)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = True
        Me.lv.Size = New System.Drawing.Size(665, 464)
        Me.lv.SmallImageList = Me.imgList
        Me.lv.TabIndex = 5
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Process"
        Me.ColumnHeader2.Width = 200
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "State to monitor"
        Me.ColumnHeader3.Width = 200
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Action"
        Me.ColumnHeader4.Width = 350
        '
        'frmBasedStateAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(665, 488)
        Me.Controls.Add(Me.gp)
        Me.Controls.Add(Me.lv)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "frmBasedStateAction"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Based state action"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.gp.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lv As YAPM.DoubleBufferedLV
    Friend WithEvents timerRefresh As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents gp As System.Windows.Forms.GroupBox
    Friend WithEvents cmdKO As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EnableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdBrowseProcessPath As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseProcessId As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseProcessName As System.Windows.Forms.Button
    Friend WithEvents txtProcessPath As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessID As System.Windows.Forms.TextBox
    Friend WithEvents txtProcessName As System.Windows.Forms.TextBox
    Friend WithEvents chkCheckProcessPath As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckProcessID As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckProcessName As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbOperator As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtParam1Val As System.Windows.Forms.TextBox
    Friend WithEvents txtParam1Desc As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbAction As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbCounter As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtThreshold As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtParam2Val As System.Windows.Forms.TextBox
    Friend WithEvents txtParam2Desc As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents DisplayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SimulationConsoleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblThresholdDesc As System.Windows.Forms.TextBox
End Class
