<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDumpFile
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
        Me.ChooseFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.txtDir = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbOption = New System.Windows.Forms.ComboBox
        Me.cmdCreate = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Location = New System.Drawing.Point(264, 12)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'txtDir
        '
        Me.txtDir.Location = New System.Drawing.Point(105, 12)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.ReadOnly = True
        Me.txtDir.Size = New System.Drawing.Size(153, 22)
        Me.txtDir.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Target directory"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Dump option"
        '
        'cbOption
        '
        Me.cbOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOption.FormattingEnabled = True
        Me.cbOption.Items.AddRange(New Object() {"MiniDumpNormal", "MiniDumpWithDataSegs", "MiniDumpWithFullMemory", "MiniDumpWithHandleData", "MiniDumpFilterMemory", "MiniDumpScanMemory", "MiniDumpWithUnloadedModules", "MiniDumpWithIndirectlyReferencedMemory", "MiniDumpFilterModulePaths", "MiniDumpWithProcessThreadData", "MiniDumpWithPrivateReadWriteMemory", "MiniDumpWithoutOptionalData", "MiniDumpWithFullMemoryInfo", "MiniDumpWithThreadInfo", "MiniDumpWithCodeSegs"})
        Me.cbOption.Location = New System.Drawing.Point(94, 40)
        Me.cbOption.Name = "cbOption"
        Me.cbOption.Size = New System.Drawing.Size(198, 21)
        Me.cbOption.TabIndex = 4
        '
        'cmdCreate
        '
        Me.cmdCreate.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCreate.Location = New System.Drawing.Point(37, 72)
        Me.cmdCreate.Name = "cmdCreate"
        Me.cmdCreate.Size = New System.Drawing.Size(75, 23)
        Me.cmdCreate.TabIndex = 5
        Me.cmdCreate.Text = "Create"
        Me.cmdCreate.UseVisualStyleBackColor = True
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(192, 72)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 6
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'frmDumpFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 104)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdCreate)
        Me.Controls.Add(Me.cbOption)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDir)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmDumpFile"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create a dump file"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ChooseFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents txtDir As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbOption As System.Windows.Forms.ComboBox
    Friend WithEvents cmdCreate As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
End Class
