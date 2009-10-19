<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HJobProp
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
        Me.SuspendLayout()
        '
        'cmdOpen
        '
        Me.cmdOpen.Image = Global.My.Resources.Resources.application_blue
        Me.cmdOpen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdOpen.Location = New System.Drawing.Point(3, 3)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(128, 25)
        Me.cmdOpen.TabIndex = 0
        Me.cmdOpen.Text = "      Show job details"
        Me.cmdOpen.UseVisualStyleBackColor = True
        '
        'HJobProp
        '
        Me.Controls.Add(Me.cmdOpen)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "HJobProp"
        Me.Size = New System.Drawing.Size(338, 196)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdOpen As System.Windows.Forms.Button

End Class
