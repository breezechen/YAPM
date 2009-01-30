<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLog))
        Me.timerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.Ribbon = New System.Windows.Forms.Ribbon
        Me.tabMain = New System.Windows.Forms.RibbonTab
        Me.RBLogSave = New System.Windows.Forms.RibbonPanel
        Me.butSaveLog = New System.Windows.Forms.RibbonButton
        Me.RBOptions = New System.Windows.Forms.RibbonPanel
        Me.butConfig = New System.Windows.Forms.RibbonButton
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'timerRefresh
        '
        Me.timerRefresh.Enabled = True
        Me.timerRefresh.Interval = 1000
        '
        'Ribbon
        '
        Me.Ribbon.Location = New System.Drawing.Point(0, 0)
        Me.Ribbon.Minimized = False
        Me.Ribbon.Name = "Ribbon"
        Me.Ribbon.Size = New System.Drawing.Size(624, 115)
        Me.Ribbon.TabIndex = 45
        Me.Ribbon.Tabs.Add(Me.tabMain)
        Me.Ribbon.TabSpacing = 6
        '
        'tabMain
        '
        Me.tabMain.Panels.Add(Me.RBLogSave)
        Me.tabMain.Panels.Add(Me.RBOptions)
        Me.tabMain.Tag = Nothing
        Me.tabMain.Text = "File"
        '
        'RBLogSave
        '
        Me.RBLogSave.ButtonMoreEnabled = False
        Me.RBLogSave.ButtonMoreVisible = False
        Me.RBLogSave.Items.Add(Me.butSaveLog)
        Me.RBLogSave.Tag = Nothing
        Me.RBLogSave.Text = "Save log file"
        '
        'butSaveLog
        '
        Me.butSaveLog.AltKey = Nothing
        Me.butSaveLog.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butSaveLog.Image = CType(resources.GetObject("butSaveLog.Image"), System.Drawing.Image)
        Me.butSaveLog.SmallImage = CType(resources.GetObject("butSaveLog.SmallImage"), System.Drawing.Image)
        Me.butSaveLog.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butSaveLog.Tag = Nothing
        Me.butSaveLog.Text = "Save log"
        Me.butSaveLog.ToolTip = Nothing
        Me.butSaveLog.ToolTipImage = Nothing
        Me.butSaveLog.ToolTipTitle = Nothing
        '
        'RBOptions
        '
        Me.RBOptions.ButtonMoreEnabled = False
        Me.RBOptions.ButtonMoreVisible = False
        Me.RBOptions.Items.Add(Me.butConfig)
        Me.RBOptions.Tag = Nothing
        Me.RBOptions.Text = "Configuration"
        '
        'butConfig
        '
        Me.butConfig.AltKey = Nothing
        Me.butConfig.DropDownArrowSize = New System.Drawing.Size(5, 3)
        Me.butConfig.Enabled = False
        Me.butConfig.Image = CType(resources.GetObject("butConfig.Image"), System.Drawing.Image)
        Me.butConfig.SmallImage = CType(resources.GetObject("butConfig.SmallImage"), System.Drawing.Image)
        Me.butConfig.Style = System.Windows.Forms.RibbonButtonStyle.Normal
        Me.butConfig.Tag = Nothing
        Me.butConfig.Text = "Preferences"
        Me.butConfig.ToolTip = Nothing
        Me.butConfig.ToolTipImage = Nothing
        Me.butConfig.ToolTipTitle = Nothing
        '
        'txtLog
        '
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.Location = New System.Drawing.Point(0, 115)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(624, 315)
        Me.txtLog.TabIndex = 46
        '
        'frmLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 430)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.Ribbon)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Log file"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents timerRefresh As System.Windows.Forms.Timer
    Friend WithEvents Ribbon As System.Windows.Forms.Ribbon
    Friend WithEvents tabMain As System.Windows.Forms.RibbonTab
    Friend WithEvents RBLogSave As System.Windows.Forms.RibbonPanel
    Friend WithEvents butSaveLog As System.Windows.Forms.RibbonButton
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents RBOptions As System.Windows.Forms.RibbonPanel
    Friend WithEvents butConfig As System.Windows.Forms.RibbonButton
End Class
