' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
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
Imports YAPM.Common.Misc

Public Class frmFileRelease

    Public file As String

    Private Sub cmdCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheck.Click
        ' Check if the file is locked (search file as handle/process/module/service)
        Call checkFile(file)
    End Sub

    Private Sub checkFile(ByVal sToSearch As String)
        ' Launch search
        If (sToSearch Is Nothing) OrElse sToSearch.Length < 1 Then
            Exit Sub
        End If
        sToSearch = sToSearch.ToLower

        Me.lv.ClearItems()
        Me.lv.ConnectionObj = Program.Connection
        Try
            Program.Connection.Connect()
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to connect")
            Exit Sub
        End Try

        With Me.lv
            .CaseSensitive = False
            .SearchString = sToSearch
            .Includes = Native.Api.Enums.GeneralObjectType.Handle Or _
                    Native.Api.Enums.GeneralObjectType.Module Or _
                    Native.Api.Enums.GeneralObjectType.Process Or _
                    Native.Api.Enums.GeneralObjectType.Service
            .CheckBoxes = False
            .UpdateItems()
        End With

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFix.Click
        ' Here we kick checked items
        ' Unload modules & handles
        If WarnDangerousAction("Closing the checked items could make the system unstable.", Me.Handle) = Windows.Forms.DialogResult.Yes Then
            ' Ok, proceed
            For Each it As ListViewItem In Me.lv.CheckedItems
                Dim cIt As cSearchItem = Me.lv.GetItemByKey(it.Name)
                If cIt IsNot Nothing Then
                    cIt.CloseTerminate()
                End If
            Next
        End If
    End Sub

    Private Sub frmFileRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)
        SetToolTip(Me.cmdCheck, "Check if a handle to the file in opened by a process")
        SetToolTip(Me.cmdFix, "Close the selected handles")

        Native.Functions.Misc.SetTheme(Me.lv.Handle)
    End Sub

    Private Sub lv_HasRefreshed() Handles lv.HasRefreshed
        Me.lv.CheckBoxes = True
    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lv)
    End Sub

End Class