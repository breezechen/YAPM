<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmObjDetails
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
        Me.lv = New YAPM.DoubleBufferedLV
        Me.headerDesc = New System.Windows.Forms.ColumnHeader
        Me.headerValue = New System.Windows.Forms.ColumnHeader
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.mnuPopup = New System.Windows.Forms.ContextMenu
        Me.MenuItemCopyObj = New System.Windows.Forms.MenuItem
        Me.MenuItemCpProperty = New System.Windows.Forms.MenuItem
        Me.MenuItemCpValue = New System.Windows.Forms.MenuItem
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
        Me.SplitContainer.Panel2.Controls.Add(Me.cmdRefresh)
        Me.SplitContainer.Size = New System.Drawing.Size(330, 181)
        Me.SplitContainer.SplitterDistance = 149
        Me.SplitContainer.TabIndex = 0
        '
        'lv
        '
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.headerDesc, Me.headerValue})
        Me.lv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lv.FullRowSelect = True
        Me.lv.Location = New System.Drawing.Point(0, 0)
        Me.lv.Name = "lv"
        Me.lv.OverriddenDoubleBuffered = True
        Me.lv.Size = New System.Drawing.Size(330, 149)
        Me.lv.TabIndex = 2
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'headerDesc
        '
        Me.headerDesc.Text = "Property"
        Me.headerDesc.Width = 136
        '
        'headerValue
        '
        Me.headerValue.Text = "Value"
        Me.headerValue.Width = 159
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(218, 1)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Image = Global.YAPM.My.Resources.Resources.refresh16
        Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRefresh.Location = New System.Drawing.Point(37, 1)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(85, 23)
        Me.cmdRefresh.TabIndex = 0
        Me.cmdRefresh.Text = "     Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = True
        '
        'mnuPopup
        '
        Me.mnuPopup.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyObj})
        '
        'MenuItemCopyObj
        '
        Me.VistaMenu.SetImage(Me.MenuItemCopyObj, Global.YAPM.My.Resources.Resources.copy16)
        Me.MenuItemCopyObj.Index = 0
        Me.MenuItemCopyObj.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCpProperty, Me.MenuItemCpValue})
        Me.MenuItemCopyObj.Text = "Copy to clipboard"
        '
        'MenuItemCpProperty
        '
        Me.MenuItemCpProperty.Index = 0
        Me.MenuItemCpProperty.Text = "Property"
        '
        'MenuItemCpValue
        '
        Me.MenuItemCpValue.Index = 1
        Me.MenuItemCpValue.Text = "Value"
        '
        'VistaMenu
        '
        Me.VistaMenu.ContainerControl = Me
        '
        'frmObjDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 181)
        Me.Controls.Add(Me.SplitContainer)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmObjDetails"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Object informations"
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.VistaMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents lv As YAPM.DoubleBufferedLV
    Friend WithEvents headerDesc As System.Windows.Forms.ColumnHeader
    Friend WithEvents headerValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Private WithEvents mnuPopup As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyObj As System.Windows.Forms.MenuItem
    Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Friend WithEvents MenuItemCpProperty As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCpValue As System.Windows.Forms.MenuItem
End Class
