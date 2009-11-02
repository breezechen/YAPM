<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHeapBlocks
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
        Me.SplitContainer = New System.Windows.Forms.SplitContainer
        Me.lv = New DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.cmdOK = New System.Windows.Forms.Button
        Me.mnuPopup = New System.Windows.Forms.ContextMenu
        Me.MenuItemViewMemory = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItemCopyObj = New System.Windows.Forms.MenuItem
        Me.MenuItemCpAddress = New System.Windows.Forms.MenuItem
        Me.MenuItemCpSize = New System.Windows.Forms.MenuItem
        Me.MenuItemCpStatus = New System.Windows.Forms.MenuItem
        Me.VistaMenu = New wyDay.Controls.VistaMenu(Me.components)
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer.Panel1.Controls.Add(Me.lv)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdOK)
        Me.SplitContainer.Size = New System.Drawing.Size(481, 307)
        Me.SplitContainer.SplitterDistance = 275
        Me.SplitContainer.TabIndex = 0
        '
        'lv
        '
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4})
        Me.lv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lv.FullRowSelect = True
        Me.lv.Location = New System.Drawing.Point(0, 0)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = True
        Me.lv.Size = New System.Drawing.Size(481, 275)
        Me.lv.TabIndex = 2
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Address"
        Me.ColumnHeader1.Width = 155
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Size"
        Me.ColumnHeader2.Width = 91
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Status"
        Me.ColumnHeader4.Width = 97
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(394, 1)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'mnuPopup
        '
        Me.mnuPopup.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemViewMemory, Me.MenuItem2, Me.MenuItemCopyObj})
        '
        'MenuItemViewMemory
        '
        Me.VistaMenu.SetImage(Me.MenuItemViewMemory, Global.My.Resources.Resources.magnifier)
        Me.MenuItemViewMemory.Index = 0
        Me.MenuItemViewMemory.Text = "View memory"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'MenuItemCopyObj
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyObj, Global.My.Resources.Resources.copy16)
        Me.MenuItemCopyObj.Index = 2
        Me.MenuItemCopyObj.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCpAddress, Me.MenuItemCpSize, Me.MenuItemCpStatus})
        Me.MenuItemCopyObj.Text = "Copy to clipboard"
        '
        'MenuItemCpAddress
        '
        Me.MenuItemCpAddress.Index = 0
        Me.MenuItemCpAddress.Text = "Address"
        '
        'MenuItemCpSize
        '
        Me.MenuItemCpSize.Index = 1
        Me.MenuItemCpSize.Text = "Size"
        '
        'MenuItemCpStatus
        '
        Me.MenuItemCpStatus.Index = 2
        Me.MenuItemCpStatus.Text = "Status"
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'frmHeapBlocks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 307)
        Me.Controls.Add(Me.SplitContainer)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmHeapBlocks"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Heap blocks (node address : xx)"
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents lv As DoubleBufferedLV
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Private WithEvents mnuPopup As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyObj As System.Windows.Forms.MenuItem
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItemCpAddress As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCpSize As System.Windows.Forms.MenuItem
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuItemViewMemory As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCpStatus As System.Windows.Forms.MenuItem
End Class
