<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAboutG
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
        Me.lnklblSF = New System.Windows.Forms.LinkLabel
        Me.btnOK = New System.Windows.Forms.Button
        Me.pctIcon = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lv = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblRibbon = New System.Windows.Forms.LinkLabel
        Me.lblFugueIcons = New System.Windows.Forms.LinkLabel
        Me.lblShareVB = New System.Windows.Forms.LinkLabel
        Me.lblMe = New System.Windows.Forms.LinkLabel
        Me.cmdLicense = New System.Windows.Forms.Button
        CType(Me.pctIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lnklblSF
        '
        Me.lnklblSF.AutoSize = True
        Me.lnklblSF.Location = New System.Drawing.Point(18, 246)
        Me.lnklblSF.Name = "lnklblSF"
        Me.lnklblSF.Size = New System.Drawing.Size(138, 13)
        Me.lnklblSF.TabIndex = 2
        Me.lnklblSF.TabStop = True
        Me.lnklblSF.Text = "YAPM on Sourceforge.net"
        '
        'btnOK
        '
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(317, 249)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(73, 25)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'pctIcon
        '
        Me.pctIcon.Image = Global.YAPM.My.Resources.Resources.process
        Me.pctIcon.Location = New System.Drawing.Point(12, 12)
        Me.pctIcon.Name = "pctIcon"
        Me.pctIcon.Size = New System.Drawing.Size(64, 64)
        Me.pctIcon.TabIndex = 4
        Me.pctIcon.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(94, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Yet Another Process Monitor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(95, 46)
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
        Me.lblVersion.Location = New System.Drawing.Point(155, 46)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(43, 13)
        Me.lblVersion.TabIndex = 7
        Me.lblVersion.Text = "1.0.0.0"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(155, 63)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(63, 13)
        Me.lblDate.TabIndex = 9
        Me.lblDate.Text = "01/01/2009"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Build date :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(303, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Yet Another Process Monitor is under GNU GPL 3.0 license"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Copyright 2008-2009 (c) violent_ken"
        '
        'lv
        '
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lv.FullRowSelect = True
        Me.lv.GridLines = True
        Me.lv.Location = New System.Drawing.Point(19, 155)
        Me.lv.MultiSelect = False
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(171, 79)
        Me.lv.TabIndex = 12
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "File"
        Me.ColumnHeader1.Width = 87
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Version"
        Me.ColumnHeader2.Width = 76
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 139)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "File versions"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(205, 139)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Thanks a lot to :"
        '
        'lblRibbon
        '
        Me.lblRibbon.AutoSize = True
        Me.lblRibbon.Location = New System.Drawing.Point(216, 155)
        Me.lblRibbon.Name = "lblRibbon"
        Me.lblRibbon.Size = New System.Drawing.Size(198, 13)
        Me.lblRibbon.TabIndex = 15
        Me.lblRibbon.TabStop = True
        Me.lblRibbon.Text = "Jose Manuel Menéndez Poó (Ribbon)"
        '
        'lblFugueIcons
        '
        Me.lblFugueIcons.AutoSize = True
        Me.lblFugueIcons.Location = New System.Drawing.Point(216, 171)
        Me.lblFugueIcons.Name = "lblFugueIcons"
        Me.lblFugueIcons.Size = New System.Drawing.Size(181, 13)
        Me.lblFugueIcons.TabIndex = 16
        Me.lblFugueIcons.TabStop = True
        Me.lblFugueIcons.Text = "Yusuke Kamiyamane (Fugue Icons)"
        '
        'lblShareVB
        '
        Me.lblShareVB.AutoSize = True
        Me.lblShareVB.Location = New System.Drawing.Point(216, 187)
        Me.lblShareVB.Name = "lblShareVB"
        Me.lblShareVB.Size = New System.Drawing.Size(164, 13)
        Me.lblShareVB.TabIndex = 17
        Me.lblShareVB.TabStop = True
        Me.lblShareVB.Text = "ShareVB (KernelMemory driver)"
        '
        'lblMe
        '
        Me.lblMe.AutoSize = True
        Me.lblMe.Location = New System.Drawing.Point(18, 262)
        Me.lblMe.Name = "lblMe"
        Me.lblMe.Size = New System.Drawing.Size(83, 13)
        Me.lblMe.TabIndex = 18
        Me.lblMe.TabStop = True
        Me.lblMe.Text = "Send feedback"
        '
        'cmdLicense
        '
        Me.cmdLicense.Location = New System.Drawing.Point(230, 249)
        Me.cmdLicense.Name = "cmdLicense"
        Me.cmdLicense.Size = New System.Drawing.Size(73, 25)
        Me.cmdLicense.TabIndex = 19
        Me.cmdLicense.Text = "Licenses..."
        Me.cmdLicense.UseVisualStyleBackColor = True
        '
        'frmAboutG
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnOK
        Me.ClientSize = New System.Drawing.Size(416, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdLicense)
        Me.Controls.Add(Me.lblMe)
        Me.Controls.Add(Me.lblShareVB)
        Me.Controls.Add(Me.lblFugueIcons)
        Me.Controls.Add(Me.lblRibbon)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lv)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pctIcon)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lnklblSF)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmAboutG"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About YAPM"
        CType(Me.pctIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lnklblSF As System.Windows.Forms.LinkLabel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents pctIcon As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lv As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblRibbon As System.Windows.Forms.LinkLabel
    Friend WithEvents lblFugueIcons As System.Windows.Forms.LinkLabel
    Friend WithEvents lblShareVB As System.Windows.Forms.LinkLabel
    Friend WithEvents lblMe As System.Windows.Forms.LinkLabel
    Friend WithEvents cmdLicense As System.Windows.Forms.Button
End Class
