<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HFileProp
    Inherits HXXXProp

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
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
        Me.cmdOpen = New System.Windows.Forms.Button
        Me.cmdOpenDirectory = New System.Windows.Forms.Button
        Me.lblFileExists = New System.Windows.Forms.Label
        Me.cmdFileDetails = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'cmdOpen
        '
        Me.cmdOpen.Image = Global.My.Resources.Resources.document_text
        Me.cmdOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOpen.Location = New System.Drawing.Point(3, 3)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(137, 25)
        Me.cmdOpen.TabIndex = 0
        Me.cmdOpen.Text = "     Open file properties"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'cmdOpenDirectory
        '
        Me.cmdOpenDirectory.Image = Global.My.Resources.Resources.folder_open
        Me.cmdOpenDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOpenDirectory.Location = New System.Drawing.Point(3, 34)
        Me.cmdOpenDirectory.Name = "cmdOpenDirectory"
        Me.cmdOpenDirectory.Size = New System.Drawing.Size(137, 25)
        Me.cmdOpenDirectory.TabIndex = 1
        Me.cmdOpenDirectory.Text = "    Open directory"
        Me.cmdOpenDirectory.UseVisualStyleBackColor = True
        '
        'lblFileExists
        '
        Me.lblFileExists.AutoSize = True
        Me.lblFileExists.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFileExists.Location = New System.Drawing.Point(9, 65)
        Me.lblFileExists.Name = "lblFileExists"
        Me.lblFileExists.Size = New System.Drawing.Size(0, 13)
        Me.lblFileExists.TabIndex = 3
        '
        'cmdFileDetails
        '
        Me.cmdFileDetails.Image = Global.My.Resources.Resources.magnifier
        Me.cmdFileDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdFileDetails.Location = New System.Drawing.Point(146, 3)
        Me.cmdFileDetails.Name = "cmdFileDetails"
        Me.cmdFileDetails.Size = New System.Drawing.Size(106, 25)
        Me.cmdFileDetails.TabIndex = 4
        Me.cmdFileDetails.Text = "     Show details"
        Me.cmdFileDetails.UseVisualStyleBackColor = True
        '
        'HFileProp
        '
        Me.Controls.Add(Me.cmdFileDetails)
        Me.Controls.Add(Me.lblFileExists)
        Me.Controls.Add(Me.cmdOpenDirectory)
        Me.Controls.Add(Me.cmdOpen)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "HFileProp"
        Me.Size = New System.Drawing.Size(338, 196)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdOpenDirectory As System.Windows.Forms.Button
    Friend WithEvents lblFileExists As System.Windows.Forms.Label
    Friend WithEvents cmdFileDetails As System.Windows.Forms.Button

End Class
