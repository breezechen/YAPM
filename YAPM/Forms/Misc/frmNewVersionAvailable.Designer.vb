<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewVersionAvailable
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
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblCaption = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtDesc = New System.Windows.Forms.TextBox
        Me.cmdDownload = New System.Windows.Forms.Button
        Me.lblType = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(320, 289)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(73, 25)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaption.Location = New System.Drawing.Point(7, 9)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(325, 23)
        Me.lblCaption.TabIndex = 5
        Me.lblCaption.Text = "Yet Another (remote) Process Monitor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Version :"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.SystemColors.Control
        Me.lblVersion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(73, 41)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(43, 13)
        Me.lblVersion.TabIndex = 7
        Me.lblVersion.Text = "2.2.0.0"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(73, 58)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(108, 13)
        Me.lblDate.TabIndex = 9
        Me.lblDate.Text = "Sept. 23 2009 18h18"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Build date :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Copyright 2008-2009 (c) violent_ken"
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(16, 103)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(400, 179)
        Me.txtDesc.TabIndex = 12
        '
        'cmdDownload
        '
        Me.cmdDownload.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.cmdDownload.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdDownload.Location = New System.Drawing.Point(35, 288)
        Me.cmdDownload.Name = "cmdDownload"
        Me.cmdDownload.Size = New System.Drawing.Size(181, 25)
        Me.cmdDownload.TabIndex = 13
        Me.cmdDownload.Text = "Download the new version"
        Me.cmdDownload.UseVisualStyleBackColor = True
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(256, 41)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(0, 13)
        Me.lblType.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(214, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Type :"
        '
        'frmNewVersionAvailable
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(428, 326)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdDownload)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblCaption)
        Me.Controls.Add(Me.btnOK)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmNewVersionAvailable"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "A new version is available !"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents cmdDownload As System.Windows.Forms.Button
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
