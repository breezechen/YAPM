Imports System.Windows.Forms

Public Class frmDownload

    Private WithEvents _download As cDownload
    Public Property DownloadObject() As cDownload
        Get
            Return _download
        End Get
        Set(ByVal value As cDownload)
            _download = value
        End Set
    End Property

    Private Sub _download_CompleteCallback(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs) Handles _download.CompleteCallback
        MsgBox("Complete !", MsgBoxStyle.Information, "Download done.")
        Me.Cancel_Button.Text = "OK"
        cFile.OpenDirectory(Me._download.Destination)
    End Sub

    Private Sub _download_ProgressCallback(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles _download.ProgressCallback
        Try
            Dim s As String = "Downloaded " & CStr(Math.Round(e.BytesReceived / 1024, 3)) & "KB out of " & CStr(Math.Round(e.TotalBytesToReceive / 1024, 3)) & "KB"
            Me.Text = "Downloading last update....  " & CStr(e.ProgressPercentage) & " %"
            With pgb
                .Maximum = 100
                .Minimum = 0
                .Value = e.ProgressPercentage
            End With
            Me.lblProgress.Text = s
            My.Application.DoEvents()

            If e.ProgressPercentage = 100 Then
                Call _download_CompleteCallback(Nothing, Nothing)
            End If

        Catch ex As Exception
            '
        End Try
    End Sub

    Public Sub StartDownload(ByVal dest As String)
        Me.Text = "Downloading in preparation..."
        Me.pgb.Value = 0
        Me.txtPath.Text = dest
        Me.pgb.Maximum = 1
        Me.Text = "Waiting for download..."
        My.Application.DoEvents()
        _download.StartDownload()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            _download.Cancel()
        Catch ex As Exception
            '
        End Try
        Me.Close()
    End Sub
End Class
