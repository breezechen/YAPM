' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 3 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Imports System.Runtime.InteropServices

Public Class frmFileRelease

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

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

        Dim sComp As String
        Dim i As Integer = 0
        Dim id As Integer = 0
        'TODO_       
        'For Each cProc As cProcess In frmMain.lvProcess.GetAllItems
        '    Try
        '        ' Check for modules
        '        Dim p As ProcessModuleCollection = cProc.Modules
        '        Dim m As ProcessModule
        '        For Each m In p
        '            sComp = m.FileVersionInfo.FileName.ToLower
        '            If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
        '                ' So we've found a result
        '                Dim newIt As New ListViewItem
        '                Dim n2 As New ListViewItem.ListViewSubItem
        '                n2.Text = "Module"
        '                newIt.Text = CStr(cProc.Pid) & " -- " & cProc.Name
        '                newIt.SubItems.Add(n2)
        '                newIt.ImageKey = "module"
        '                Dim _tag As New cModule.MODULEENTRY32
        '                _tag.th32ProcessID = cProc.Pid
        '                _tag.modBaseAddr = m.BaseAddress.ToInt32
        '                newIt.Tag = _tag
        '                Me.lv.Items.Add(newIt)
        '            End If
        '        Next
        '    Catch ex As Exception
        '        '
        '    End Try
        'Next

        frmMain.handles_Renamed.Refresh()
        For i = 0 To frmMain.handles_Renamed.Count - 1
            With frmMain.handles_Renamed
                If (Len(.GetObjectName(i)) > 0) Then
                    sComp = .GetObjectName(i).ToLower
                    If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                        ' So we've found a result
                        Dim newIt As New ListViewItem
                        Dim n2 As New ListViewItem.ListViewSubItem
                        newIt.Text = .GetProcessID(i) & " -- " & cProcess.GetProcessName(.GetProcessID(i))
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
                                Call cProcess.UnLoadModuleFromProcess(CType(it.Tag, cModule.MODULEENTRY32))
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

    Private Sub frmFileRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetWindowTheme(Me.lv.Handle, "explorer", Nothing)
    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lv)
    End Sub

End Class