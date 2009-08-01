Option Strict On

Public Class Form1

    Private WithEvents _hex As MemoryHexEditor.MemoryHexEditor

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        _hex = New MemoryHexEditor.MemoryHexEditor
        _hex.BackColor = Color.White
        _hex.Location = New Point(20, 20)
        _hex.Size = New Size(400, 400)
        _hex.Width = 810

        Me.Controls.Add(_hex)
        _hex.Focus()

        Dim pid As Integer = System.Diagnostics.Process.GetCurrentProcess.Id

        Dim c As New MemoryHexEditor.cProcessMemRW(pid + 1)
        Dim reg() As MemoryHexEditor.MemoryHexEditor.MemoryRegion
        ReDim reg(0)
        ' c.RetrieveMemRegions(reg

        '_hex.NewProc(reg(7), pid)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '  Button1_Click(Nothing, Nothing)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        _hex.Refresh()
    End Sub
End Class
