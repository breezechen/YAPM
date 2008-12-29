Option Strict On

Public Class frmFileRelease

    Public file As String

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        ' Check if the file is locked (search file as handle/process/module)
        Call checkfile(file)
    End Sub

    Private Sub checkFile(ByVal sToSearch As String)
        ' Launch search
        If (sToSearch Is Nothing) OrElse sToSearch.Length < 1 Then Exit Sub
        sToSearch = sToSearch.ToLower

        Me.lv.Items.Clear()

        ' Refresh services and processes lists (easy way to have up to date informations)
        Call frmMain.refreshProcessList()
        Call frmMain.refreshServiceList()

        ' Lock timers so we won't refresh
        frmMain.timerProcess.Enabled = False
        frmMain.timerServices.Enabled = False

        Dim it As ListViewItem
        Dim sComp As String
        Dim i As Integer = 0
        Dim id As Integer = 0


        For Each it In frmMain.lvProcess.Items
            Try
                ' Check for modules
                Dim proc As Process = Process.GetProcessById(CInt(Val(it.SubItems(1).Text)))
                Dim p As ProcessModuleCollection = proc.Modules
                Dim m As ProcessModule
                For Each m In p
                    sComp = m.FileVersionInfo.FileName.ToLower
                    If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                        ' So we've found a result
                        Dim newIt As New ListViewItem
                        Dim n2 As New ListViewItem.ListViewSubItem
                        n2.Text = "Module"
                        newIt.Text = it.SubItems(1).Text & " -- " & it.Text
                        newIt.SubItems.Add(n2)
                        newIt.ImageKey = "module"
                        Me.lv.Items.Add(newIt)
                    End If
                Next
            Catch ex As Exception
                '
            End Try
        Next


        frmMain.handles_Renamed.Refresh()
        For i = 0 To frmMain.handles_Renamed.Count - 1
            With frmMain.handles_Renamed
                If (Len(.GetObjectName(i)) > 0) Then
                    sComp = .GetObjectName(i).ToLower
                    If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                        ' So we've found a result
                        Dim newIt As New ListViewItem
                        Dim n2 As New ListViewItem.ListViewSubItem
                        newIt.Text = .GetProcessID(i) & " -- " & mdlFile.GetFileName(mdlProcess.GetPath(.GetProcessID(i)))
                        n2.Text = .GetNameInformation(i) & " -- " & .GetObjectName(i)
                        newIt.SubItems.Add(n2)
                        newIt.ImageKey = "handle"
                        newIt.Tag = .GetHandle(i)
                        Me.lv.Items.Add(newIt)
                    End If
                End If
            End With
        Next


        frmMain.timerServices.Enabled = True
        frmMain.timerProcess.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Here we kick checked items
        ' Unload modules & handles
        Dim r As MsgBoxResult = MsgBox("Do you really want to unload selected modules/handles ?" & vbNewLine & "This can make your system unstable.", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Release file")
        If r = MsgBoxResult.Yes Then
            ' Ok proceed
            Dim it As ListViewItem
            For Each it In Me.lv.CheckedItems
                Dim sp As String = it.Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                    If pid > 4 Then
                        Select Case it.SubItems(1).Text
                            Case "Module"
                                ' Module
                                Call mdlProcess.UnLoadModuleFromProcess(pid, file)
                            Case Else
                                ' Handle
                                Dim Handle As Integer = CInt(Val(it.Tag))
                                Call frmMain.handles_Renamed.CloseProcessLocalHandle(pid, Handle)
                        End Select
                    End If
                End If
            Next
        End If
    End Sub
End Class