Option Strict Off

Public Class frmMain

    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        ' Go !
        Me.cmdGo.Enabled = False
        Me.cmdBrowse.Enabled = False
        Me.cmdOpen.Enabled = False
        Call go(Me.txtSolution.Text, Me.txtZipPath.Text)
        Me.cmdGo.Enabled = True
        Me.cmdBrowse.Enabled = True
        Me.cmdOpen.Enabled = True
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        With Me.saveDg
            .AddExtension = True
            .AutoUpgradeEnabled = True
            .DefaultExt = "zip"
            .CheckPathExists = True
            .FileName = "YAPM.zip"
            .Filter = "Zip file (*.zip)|*.zip"
            .InitialDirectory = My.Application.Info.DirectoryPath
            .Title = "Save zip file"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtZipPath.Text = .FileName
            End If
        End With
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        With Me.openDg
            .AddExtension = True
            .AutoUpgradeEnabled = True
            .CheckFileExists = True
            .CheckPathExists = True
            .DefaultExt = "sln"
            .FileName = SOLUTION_NAME
            .InitialDirectory = GetParentDir(Me.txtSolution.Text)
            .Multiselect = False
            .ReadOnlyChecked = False
            .RestoreDirectory = True
            .ShowReadOnly = False
            .Title = "Choose solution file"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtSolution.Text = .FileName
            End If
        End With
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Static first As Boolean = True
        If first Then
            first = False
            If progParameters.AutoMode Then
                Me.txtZipPath.Text = progParameters.ZipPath
                Me.cmdGo.Enabled = False
                Me.cmdBrowse.Enabled = False
                Me.cmdOpen.Enabled = False
                Application.DoEvents()
                Call go(Me.txtSolution.Text, Me.txtZipPath.Text)
            End If
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If progParameters.AutoMode = False Then
            Me.txtZipPath.Text = My.Application.Info.DirectoryPath & "\YAPM_" & Date.Now.Ticks.ToString & ".zip"
        End If
    End Sub

End Class
