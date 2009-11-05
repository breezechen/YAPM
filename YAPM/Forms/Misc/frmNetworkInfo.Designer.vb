<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNetworkInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNetworkInfo))
        Me.timerRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.lblNumConns = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblOutRsTs = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblInErrs = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblRetransSegs = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblOutSegs = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblInSegs = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.lblCurEstab = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.lblEstabResets = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.lblAttemptFails = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.lblPassiveOpens = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.lblActiveOpens = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.lblMaxConn = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblRtoMax = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblRtoMin = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.lblRtoAlgo = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblNumAddrs = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblInErrors = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblNoPorts = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblOutDatagrams = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblInDatagrams = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.SplitContainerGraphs = New System.Windows.Forms.SplitContainer
        Me.g1 = New Graph2
        Me.g2 = New Graph2
        Me.chkTopMost = New System.Windows.Forms.CheckBox
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SplitContainerGraphs.Panel1.SuspendLayout()
        Me.SplitContainerGraphs.Panel2.SuspendLayout()
        Me.SplitContainerGraphs.SuspendLayout()
        CType(Me.g1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.g2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timerRefresh
        '
        Me.timerRefresh.Enabled = True
        Me.timerRefresh.Interval = 1000
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.lblNumConns)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Controls.Add(Me.lblOutRsTs)
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Controls.Add(Me.lblInErrs)
        Me.GroupBox7.Controls.Add(Me.Label11)
        Me.GroupBox7.Controls.Add(Me.lblRetransSegs)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.lblOutSegs)
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.lblInSegs)
        Me.GroupBox7.Controls.Add(Me.Label37)
        Me.GroupBox7.Controls.Add(Me.lblCurEstab)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Controls.Add(Me.lblEstabResets)
        Me.GroupBox7.Controls.Add(Me.Label23)
        Me.GroupBox7.Controls.Add(Me.lblAttemptFails)
        Me.GroupBox7.Controls.Add(Me.Label33)
        Me.GroupBox7.Controls.Add(Me.lblPassiveOpens)
        Me.GroupBox7.Controls.Add(Me.Label36)
        Me.GroupBox7.Controls.Add(Me.lblActiveOpens)
        Me.GroupBox7.Controls.Add(Me.Label38)
        Me.GroupBox7.Controls.Add(Me.lblMaxConn)
        Me.GroupBox7.Controls.Add(Me.Label34)
        Me.GroupBox7.Controls.Add(Me.lblRtoMax)
        Me.GroupBox7.Controls.Add(Me.Label19)
        Me.GroupBox7.Controls.Add(Me.lblRtoMin)
        Me.GroupBox7.Controls.Add(Me.Label27)
        Me.GroupBox7.Controls.Add(Me.lblRtoAlgo)
        Me.GroupBox7.Controls.Add(Me.Label31)
        Me.GroupBox7.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(336, 148)
        Me.GroupBox7.TabIndex = 21
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "TCP stats"
        '
        'lblNumConns
        '
        Me.lblNumConns.AutoSize = True
        Me.lblNumConns.Location = New System.Drawing.Point(252, 113)
        Me.lblNumConns.Name = "lblNumConns"
        Me.lblNumConns.Size = New System.Drawing.Size(0, 13)
        Me.lblNumConns.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(179, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "NumConns"
        '
        'lblOutRsTs
        '
        Me.lblOutRsTs.AutoSize = True
        Me.lblOutRsTs.Location = New System.Drawing.Point(252, 98)
        Me.lblOutRsTs.Name = "lblOutRsTs"
        Me.lblOutRsTs.Size = New System.Drawing.Size(0, 13)
        Me.lblOutRsTs.TabIndex = 27
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(179, 98)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "OutRsts"
        '
        'lblInErrs
        '
        Me.lblInErrs.AutoSize = True
        Me.lblInErrs.Location = New System.Drawing.Point(252, 83)
        Me.lblInErrs.Name = "lblInErrs"
        Me.lblInErrs.Size = New System.Drawing.Size(0, 13)
        Me.lblInErrs.TabIndex = 25
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(179, 83)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "InErrs"
        '
        'lblRetransSegs
        '
        Me.lblRetransSegs.AutoSize = True
        Me.lblRetransSegs.Location = New System.Drawing.Point(252, 68)
        Me.lblRetransSegs.Name = "lblRetransSegs"
        Me.lblRetransSegs.Size = New System.Drawing.Size(0, 13)
        Me.lblRetransSegs.TabIndex = 23
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(179, 68)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 13)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "RetransSegs"
        '
        'lblOutSegs
        '
        Me.lblOutSegs.AutoSize = True
        Me.lblOutSegs.Location = New System.Drawing.Point(252, 53)
        Me.lblOutSegs.Name = "lblOutSegs"
        Me.lblOutSegs.Size = New System.Drawing.Size(0, 13)
        Me.lblOutSegs.TabIndex = 21
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(179, 53)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(51, 13)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "OutSegs"
        '
        'lblInSegs
        '
        Me.lblInSegs.AutoSize = True
        Me.lblInSegs.Location = New System.Drawing.Point(252, 38)
        Me.lblInSegs.Name = "lblInSegs"
        Me.lblInSegs.Size = New System.Drawing.Size(0, 13)
        Me.lblInSegs.TabIndex = 19
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(179, 38)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(41, 13)
        Me.Label37.TabIndex = 18
        Me.Label37.Text = "InSegs"
        '
        'lblCurEstab
        '
        Me.lblCurEstab.AutoSize = True
        Me.lblCurEstab.Location = New System.Drawing.Point(252, 23)
        Me.lblCurEstab.Name = "lblCurEstab"
        Me.lblCurEstab.Size = New System.Drawing.Size(0, 13)
        Me.lblCurEstab.TabIndex = 17
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(179, 23)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(53, 13)
        Me.Label29.TabIndex = 16
        Me.Label29.Text = "CurEstab"
        '
        'lblEstabResets
        '
        Me.lblEstabResets.AutoSize = True
        Me.lblEstabResets.Location = New System.Drawing.Point(93, 23)
        Me.lblEstabResets.Name = "lblEstabResets"
        Me.lblEstabResets.Size = New System.Drawing.Size(0, 13)
        Me.lblEstabResets.TabIndex = 15
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(15, 23)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(68, 13)
        Me.Label23.TabIndex = 14
        Me.Label23.Text = "EstabResets"
        '
        'lblAttemptFails
        '
        Me.lblAttemptFails.AutoSize = True
        Me.lblAttemptFails.Location = New System.Drawing.Point(93, 113)
        Me.lblAttemptFails.Name = "lblAttemptFails"
        Me.lblAttemptFails.Size = New System.Drawing.Size(0, 13)
        Me.lblAttemptFails.TabIndex = 13
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(15, 113)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(71, 13)
        Me.Label33.TabIndex = 12
        Me.Label33.Text = "AttemptFails"
        '
        'lblPassiveOpens
        '
        Me.lblPassiveOpens.AutoSize = True
        Me.lblPassiveOpens.Location = New System.Drawing.Point(93, 98)
        Me.lblPassiveOpens.Name = "lblPassiveOpens"
        Me.lblPassiveOpens.Size = New System.Drawing.Size(0, 13)
        Me.lblPassiveOpens.TabIndex = 11
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(15, 98)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(77, 13)
        Me.Label36.TabIndex = 10
        Me.Label36.Text = "PassiveOpens"
        '
        'lblActiveOpens
        '
        Me.lblActiveOpens.AutoSize = True
        Me.lblActiveOpens.Location = New System.Drawing.Point(93, 83)
        Me.lblActiveOpens.Name = "lblActiveOpens"
        Me.lblActiveOpens.Size = New System.Drawing.Size(0, 13)
        Me.lblActiveOpens.TabIndex = 9
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(15, 83)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(71, 13)
        Me.Label38.TabIndex = 8
        Me.Label38.Text = "ActiveOpens"
        '
        'lblMaxConn
        '
        Me.lblMaxConn.AutoSize = True
        Me.lblMaxConn.Location = New System.Drawing.Point(93, 68)
        Me.lblMaxConn.Name = "lblMaxConn"
        Me.lblMaxConn.Size = New System.Drawing.Size(0, 13)
        Me.lblMaxConn.TabIndex = 7
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(15, 68)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(56, 13)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "MaxConn"
        '
        'lblRtoMax
        '
        Me.lblRtoMax.AutoSize = True
        Me.lblRtoMax.Location = New System.Drawing.Point(93, 53)
        Me.lblRtoMax.Name = "lblRtoMax"
        Me.lblRtoMax.Size = New System.Drawing.Size(0, 13)
        Me.lblRtoMax.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(15, 53)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(46, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "RtoMax"
        '
        'lblRtoMin
        '
        Me.lblRtoMin.AutoSize = True
        Me.lblRtoMin.Location = New System.Drawing.Point(93, 38)
        Me.lblRtoMin.Name = "lblRtoMin"
        Me.lblRtoMin.Size = New System.Drawing.Size(0, 13)
        Me.lblRtoMin.TabIndex = 3
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(15, 38)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(45, 13)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "RtoMin"
        '
        'lblRtoAlgo
        '
        Me.lblRtoAlgo.AutoSize = True
        Me.lblRtoAlgo.Location = New System.Drawing.Point(93, 128)
        Me.lblRtoAlgo.Name = "lblRtoAlgo"
        Me.lblRtoAlgo.Size = New System.Drawing.Size(0, 13)
        Me.lblRtoAlgo.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(15, 128)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(76, 13)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "RtoAlgorithm"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblNumAddrs)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblInErrors)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblNoPorts)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblOutDatagrams)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblInDatagrams)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(345, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(203, 103)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "UDP stats"
        '
        'lblNumAddrs
        '
        Me.lblNumAddrs.AutoSize = True
        Me.lblNumAddrs.Location = New System.Drawing.Point(102, 83)
        Me.lblNumAddrs.Name = "lblNumAddrs"
        Me.lblNumAddrs.Size = New System.Drawing.Size(0, 13)
        Me.lblNumAddrs.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "NumAddrs"
        '
        'lblInErrors
        '
        Me.lblInErrors.AutoSize = True
        Me.lblInErrors.Location = New System.Drawing.Point(102, 68)
        Me.lblInErrors.Name = "lblInErrors"
        Me.lblInErrors.Size = New System.Drawing.Size(0, 13)
        Me.lblInErrors.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "InErrors"
        '
        'lblNoPorts
        '
        Me.lblNoPorts.AutoSize = True
        Me.lblNoPorts.Location = New System.Drawing.Point(102, 53)
        Me.lblNoPorts.Name = "lblNoPorts"
        Me.lblNoPorts.Size = New System.Drawing.Size(0, 13)
        Me.lblNoPorts.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "NoPorts"
        '
        'lblOutDatagrams
        '
        Me.lblOutDatagrams.AutoSize = True
        Me.lblOutDatagrams.Location = New System.Drawing.Point(102, 38)
        Me.lblOutDatagrams.Name = "lblOutDatagrams"
        Me.lblOutDatagrams.Size = New System.Drawing.Size(0, 13)
        Me.lblOutDatagrams.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "OutDatagrams"
        '
        'lblInDatagrams
        '
        Me.lblInDatagrams.AutoSize = True
        Me.lblInDatagrams.Location = New System.Drawing.Point(102, 23)
        Me.lblInDatagrams.Name = "lblInDatagrams"
        Me.lblInDatagrams.Size = New System.Drawing.Size(0, 13)
        Me.lblInDatagrams.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "InDatagrams"
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer.IsSplitterFixed = True
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.SplitContainerGraphs)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.chkTopMost)
        Me.SplitContainer.Panel2.Controls.Add(Me.GroupBox7)
        Me.SplitContainer.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer.Size = New System.Drawing.Size(640, 312)
        Me.SplitContainer.SplitterDistance = 151
        Me.SplitContainer.TabIndex = 25
        '
        'SplitContainerGraphs
        '
        Me.SplitContainerGraphs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerGraphs.IsSplitterFixed = True
        Me.SplitContainerGraphs.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerGraphs.Name = "SplitContainerGraphs"
        '
        'SplitContainerGraphs.Panel1
        '
        Me.SplitContainerGraphs.Panel1.Controls.Add(Me.g1)
        '
        'SplitContainerGraphs.Panel2
        '
        Me.SplitContainerGraphs.Panel2.Controls.Add(Me.g2)
        Me.SplitContainerGraphs.Size = New System.Drawing.Size(640, 151)
        Me.SplitContainerGraphs.SplitterDistance = 320
        Me.SplitContainerGraphs.TabIndex = 25
        '
        'g1
        '
        Me.g1.BackColor = System.Drawing.Color.Black
        Me.g1.Color2 = System.Drawing.Color.Olive
        Me.g1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.g1.EnableGraph = True
        Me.g1.Fixedheight = False
        Me.g1.GridStep = 13
        Me.g1.Location = New System.Drawing.Point(0, 0)
        Me.g1.Name = "g1"
        Me.g1.ShowSecondGraph = True
        Me.g1.Size = New System.Drawing.Size(320, 151)
        Me.g1.TabIndex = 25
        Me.g1.TabStop = False
        Me.g1.TextColor = System.Drawing.Color.Lime
        Me.g1.TopText = Nothing
        '
        'g2
        '
        Me.g2.BackColor = System.Drawing.Color.Black
        Me.g2.Color2 = System.Drawing.Color.Olive
        Me.g2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.g2.EnableGraph = True
        Me.g2.Fixedheight = False
        Me.g2.GridStep = 13
        Me.g2.Location = New System.Drawing.Point(0, 0)
        Me.g2.Name = "g2"
        Me.g2.ShowSecondGraph = True
        Me.g2.Size = New System.Drawing.Size(316, 151)
        Me.g2.TabIndex = 24
        Me.g2.TabStop = False
        Me.g2.TextColor = System.Drawing.Color.Lime
        Me.g2.TopText = Nothing
        '
        'chkTopMost
        '
        Me.chkTopMost.AutoSize = True
        Me.chkTopMost.Location = New System.Drawing.Point(345, 112)
        Me.chkTopMost.Name = "chkTopMost"
        Me.chkTopMost.Size = New System.Drawing.Size(99, 17)
        Me.chkTopMost.TabIndex = 23
        Me.chkTopMost.Text = "Always on top"
        Me.chkTopMost.UseVisualStyleBackColor = True
        '
        'frmNetworkInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 312)
        Me.Controls.Add(Me.SplitContainer)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(656, 350)
        Me.Name = "frmNetworkInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Network statistics"
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        Me.SplitContainer.ResumeLayout(False)
        Me.SplitContainerGraphs.Panel1.ResumeLayout(False)
        Me.SplitContainerGraphs.Panel2.ResumeLayout(False)
        Me.SplitContainerGraphs.ResumeLayout(False)
        CType(Me.g1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.g2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timerRefresh As System.Windows.Forms.Timer
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumConns As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblOutRsTs As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblInErrs As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblRetransSegs As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblOutSegs As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblInSegs As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblCurEstab As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblEstabResets As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblAttemptFails As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lblPassiveOpens As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents lblActiveOpens As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblMaxConn As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblRtoMax As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblRtoMin As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents lblRtoAlgo As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumAddrs As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblInErrors As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblNoPorts As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblOutDatagrams As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblInDatagrams As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerGraphs As System.Windows.Forms.SplitContainer
    Friend WithEvents g2 As Graph2
    Friend WithEvents chkTopMost As System.Windows.Forms.CheckBox
    Friend WithEvents g1 As Graph2
End Class
