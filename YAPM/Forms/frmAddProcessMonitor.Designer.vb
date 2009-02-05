<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddProcessMonitor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddProcessMonitor))
        Me.butAdd = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtInterval = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmdAddToList = New System.Windows.Forms.Button
        Me.cmdRemoveFromList = New System.Windows.Forms.Button
        Me.lstToAdd = New YAPM.DoubleBufferedLV
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.txtHelp = New System.Windows.Forms.TextBox
        Me.lstCounterType = New YAPM.DoubleBufferedLV
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.lstInstance = New YAPM.DoubleBufferedLV
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.lstCategory = New YAPM.DoubleBufferedLV
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'butAdd
        '
        Me.butAdd.Enabled = False
        Me.butAdd.Location = New System.Drawing.Point(834, 203)
        Me.butAdd.Name = "butAdd"
        Me.butAdd.Size = New System.Drawing.Size(271, 24)
        Me.butAdd.TabIndex = 7
        Me.butAdd.Text = "Monitor counters"
        Me.butAdd.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(831, 176)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Refresh interval"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(921, 176)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(122, 21)
        Me.txtInterval.TabIndex = 6
        Me.txtInterval.Text = "1000"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Category"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(472, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Counter type"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(262, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Instance to monitor"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(830, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Counters to monitor"
        '
        'cmdAddToList
        '
        Me.cmdAddToList.Location = New System.Drawing.Point(768, 114)
        Me.cmdAddToList.Name = "cmdAddToList"
        Me.cmdAddToList.Size = New System.Drawing.Size(41, 23)
        Me.cmdAddToList.TabIndex = 4
        Me.cmdAddToList.Text = ">>"
        Me.cmdAddToList.UseVisualStyleBackColor = True
        '
        'cmdRemoveFromList
        '
        Me.cmdRemoveFromList.Location = New System.Drawing.Point(768, 143)
        Me.cmdRemoveFromList.Name = "cmdRemoveFromList"
        Me.cmdRemoveFromList.Size = New System.Drawing.Size(41, 23)
        Me.cmdRemoveFromList.TabIndex = 8
        Me.cmdRemoveFromList.Text = "<<"
        Me.cmdRemoveFromList.UseVisualStyleBackColor = True
        '
        'lstToAdd
        '
        Me.lstToAdd.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lstToAdd.FullRowSelect = True
        Me.lstToAdd.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstToAdd.Location = New System.Drawing.Point(833, 23)
        Me.lstToAdd.Name = "lstToAdd"
        Me.lstToAdd.OverriddenDoubleBuffered = False
        Me.lstToAdd.ShowGroups = False
        Me.lstToAdd.ShowItemToolTips = True
        Me.lstToAdd.Size = New System.Drawing.Size(272, 147)
        Me.lstToAdd.TabIndex = 5
        Me.lstToAdd.UseCompatibleStateImageBehavior = False
        Me.lstToAdd.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Counter item"
        Me.ColumnHeader1.Width = 1000
        '
        'txtHelp
        '
        Me.txtHelp.BackColor = System.Drawing.Color.White
        Me.txtHelp.Location = New System.Drawing.Point(9, 177)
        Me.txtHelp.Multiline = True
        Me.txtHelp.Name = "txtHelp"
        Me.txtHelp.ReadOnly = True
        Me.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHelp.Size = New System.Drawing.Size(738, 50)
        Me.txtHelp.TabIndex = 9
        Me.txtHelp.Text = "Description de l'objet sélectionné"
        '
        'lstCounterType
        '
        Me.lstCounterType.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2})
        Me.lstCounterType.FullRowSelect = True
        Me.lstCounterType.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstCounterType.Location = New System.Drawing.Point(475, 24)
        Me.lstCounterType.Name = "lstCounterType"
        Me.lstCounterType.OverriddenDoubleBuffered = False
        Me.lstCounterType.ShowGroups = False
        Me.lstCounterType.ShowItemToolTips = True
        Me.lstCounterType.Size = New System.Drawing.Size(272, 147)
        Me.lstCounterType.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstCounterType.TabIndex = 3
        Me.lstCounterType.UseCompatibleStateImageBehavior = False
        Me.lstCounterType.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Counter item"
        Me.ColumnHeader2.Width = 1000
        '
        'lstInstance
        '
        Me.lstInstance.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.lstInstance.FullRowSelect = True
        Me.lstInstance.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstInstance.Location = New System.Drawing.Point(265, 24)
        Me.lstInstance.MultiSelect = False
        Me.lstInstance.Name = "lstInstance"
        Me.lstInstance.OverriddenDoubleBuffered = False
        Me.lstInstance.ShowGroups = False
        Me.lstInstance.ShowItemToolTips = True
        Me.lstInstance.Size = New System.Drawing.Size(204, 147)
        Me.lstInstance.TabIndex = 2
        Me.lstInstance.UseCompatibleStateImageBehavior = False
        Me.lstInstance.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Counter item"
        Me.ColumnHeader3.Width = 1000
        '
        'lstCategory
        '
        Me.lstCategory.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4})
        Me.lstCategory.FullRowSelect = True
        Me.lstCategory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lstCategory.Location = New System.Drawing.Point(9, 24)
        Me.lstCategory.MultiSelect = False
        Me.lstCategory.Name = "lstCategory"
        Me.lstCategory.OverriddenDoubleBuffered = False
        Me.lstCategory.ShowGroups = False
        Me.lstCategory.ShowItemToolTips = True
        Me.lstCategory.Size = New System.Drawing.Size(250, 147)
        Me.lstCategory.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstCategory.TabIndex = 1
        Me.lstCategory.UseCompatibleStateImageBehavior = False
        Me.lstCategory.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Counter item"
        Me.ColumnHeader4.Width = 1000
        '
        'cmdSearch
        '
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSearch.Location = New System.Drawing.Point(753, 23)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(74, 76)
        Me.cmdSearch.TabIndex = 14
        Me.cmdSearch.Text = "Search a monitor"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.UseVisualStyleBackColor = True
        '
        'frmAddProcessMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1111, 235)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.lstCategory)
        Me.Controls.Add(Me.lstInstance)
        Me.Controls.Add(Me.lstCounterType)
        Me.Controls.Add(Me.txtHelp)
        Me.Controls.Add(Me.lstToAdd)
        Me.Controls.Add(Me.cmdRemoveFromList)
        Me.Controls.Add(Me.cmdAddToList)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtInterval)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.butAdd)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddProcessMonitor"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add monitoring"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butAdd As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdAddToList As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveFromList As System.Windows.Forms.Button
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtHelp As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdSearch As System.Windows.Forms.Button
    Friend WithEvents lstToAdd As YAPM.DoubleBufferedLV
    Friend WithEvents lstCounterType As YAPM.DoubleBufferedLV
    Friend WithEvents lstInstance As YAPM.DoubleBufferedLV
    Friend WithEvents lstCategory As YAPM.DoubleBufferedLV
End Class
