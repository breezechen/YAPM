﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHiddenProcesses
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHiddenProcesses))
        Dim CConnection1 As CoreFunc.cConnection = New CoreFunc.cConnection
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Me.sb = New System.Windows.Forms.StatusStrip
        Me.lblTotal = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblVisible = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblHidden = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton
        Me.handleMethod = New System.Windows.Forms.ToolStripMenuItem
        Me.bruteforceMethod = New System.Windows.Forms.ToolStripMenuItem
        Me.TimerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.lvProcess = New Providers.processList
        Me.c1 = New System.Windows.Forms.ColumnHeader
        Me.c2 = New System.Windows.Forms.ColumnHeader
        Me.c8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader20 = New System.Windows.Forms.ColumnHeader
        Me.sb.SuspendLayout()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'sb
        '
        Me.sb.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTotal, Me.lblVisible, Me.lblHidden, Me.ToolStripSplitButton1})
        Me.sb.Location = New System.Drawing.Point(0, 484)
        Me.sb.Name = "sb"
        Me.sb.Size = New System.Drawing.Size(633, 22)
        Me.sb.TabIndex = 5
        Me.sb.Text = "StatusStrip1"
        '
        'lblTotal
        '
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(67, 17)
        Me.lblTotal.Text = "0 processes"
        '
        'lblVisible
        '
        Me.lblVisible.Name = "lblVisible"
        Me.lblVisible.Size = New System.Drawing.Size(103, 17)
        Me.lblVisible.Text = "0 visible processes"
        '
        'lblHidden
        '
        Me.lblHidden.Name = "lblHidden"
        Me.lblHidden.Size = New System.Drawing.Size(107, 17)
        Me.lblHidden.Text = "0 hidden processes"
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.handleMethod, Me.bruteforceMethod})
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(32, 20)
        Me.ToolStripSplitButton1.Text = "ToolStripSplitButton1"
        '
        'handleMethod
        '
        Me.handleMethod.Checked = True
        Me.handleMethod.CheckState = System.Windows.Forms.CheckState.Checked
        Me.handleMethod.Name = "handleMethod"
        Me.handleMethod.Size = New System.Drawing.Size(157, 22)
        Me.handleMethod.Text = "Handle method"
        '
        'bruteforceMethod
        '
        Me.bruteforceMethod.Name = "bruteforceMethod"
        Me.bruteforceMethod.Size = New System.Drawing.Size(157, 22)
        Me.bruteforceMethod.Text = "Brute force"
        '
        'TimerProcess
        '
        Me.TimerProcess.Interval = 1000
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.lvProcess)
        Me.SplitContainer.Panel2Collapsed = True
        Me.SplitContainer.Size = New System.Drawing.Size(633, 484)
        Me.SplitContainer.SplitterDistance = 211
        Me.SplitContainer.TabIndex = 6
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.CatchErrors = False
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c8, Me.ColumnHeader20})
        CConnection1.ConnectionType = CoreFunc.cConnection.TypeOfConnection.LocalConnection
        Me.lvProcess.ConnectionObj = CConnection1
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.EnumMethod = CoreFunc.asyncCallbackProcEnumerate.ProcessEnumMethode.HandleMethod
        Me.lvProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcess.FullRowSelect = True
        ListViewGroup1.Header = "Processes"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.Location = New System.Drawing.Point(0, 0)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.Size = New System.Drawing.Size(633, 484)
        Me.lvProcess.TabIndex = 5
        Me.lvProcess.UseCompatibleStateImageBehavior = False
        Me.lvProcess.View = System.Windows.Forms.View.Details
        '
        'c1
        '
        Me.c1.Text = "Name"
        Me.c1.Width = 100
        '
        'c2
        '
        Me.c2.Text = "PID"
        Me.c2.Width = 50
        '
        'c8
        '
        Me.c8.Text = "Path"
        Me.c8.Width = 378
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "ObjectCreationDate"
        Me.ColumnHeader20.Width = 152
        '
        'frmHiddenProcesses
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 506)
        Me.Controls.Add(Me.SplitContainer)
        Me.Controls.Add(Me.sb)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmHiddenProcesses"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Hidden processes"
        Me.sb.ResumeLayout(False)
        Me.sb.PerformLayout()
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sb As System.Windows.Forms.StatusStrip
    Friend WithEvents lblTotal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblVisible As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblHidden As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents handleMethod As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bruteforceMethod As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimerProcess As System.Windows.Forms.Timer
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents lvProcess As Providers.processList
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader

End Class