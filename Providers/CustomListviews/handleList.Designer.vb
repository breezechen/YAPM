<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class handleList
    Inherits Providers.customLV

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
        Me.SuspendLayout()
        '
        'processList
        '
        '        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        '        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "processList"
        Me.Size = New System.Drawing.Size(233, 288)
        Me.ResumeLayout(False)

    End Sub

End Class
