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
        Me.cmdDetails = New System.Windows.Forms.Button
        Me.lblFileExists = New System.Windows.Forms.Label
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
        'cmdDetails
        '
        Me.cmdDetails.Image = Global.My.Resources.Resources.folder_open
        Me.cmdDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDetails.Location = New System.Drawing.Point(3, 34)
        Me.cmdDetails.Name = "cmdDetails"
        Me.cmdDetails.Size = New System.Drawing.Size(137, 25)
        Me.cmdDetails.TabIndex = 1
        Me.cmdDetails.Text = "    Open directory"
        Me.cmdDetails.UseVisualStyleBackColor = True
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
        'HFileProp
        '
        Me.Controls.Add(Me.lblFileExists)
        Me.Controls.Add(Me.cmdDetails)
        Me.Controls.Add(Me.cmdOpen)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "HFileProp"
        Me.Size = New System.Drawing.Size(338, 196)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdDetails As System.Windows.Forms.Button
    Friend WithEvents lblFileExists As System.Windows.Forms.Label

End Class
