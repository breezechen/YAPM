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

Public Class frmObjDetails

    Private _TheObject As cGeneralObject

    Public Property TheObject() As cGeneralObject
        Get
            Return _TheObject
        End Get
        Set(ByVal value As cGeneralObject)
            _TheObject = value
        End Set
    End Property

    Private Sub frmFileRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)
        SetToolTip(Me.cmdOK, "OK")
        SetToolTip(Me.cmdRefresh, "Refresh values")

        Native.Functions.Misc.SetTheme(Me.lv.Handle)

        Call RefreshValues()

    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Common.Misc.CopyLvToClip(e, Me.lv)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call RefreshValues()
    End Sub

    ' Refresh values
    Private Sub RefreshValues()
        Me.lv.BeginUpdate()
        Me.lv.Items.Clear()

        For Each sProp As String In _TheObject.GetAvailableProperties(True, True)
            Dim it As New ListViewItem(sProp)
            it.SubItems.Add(_TheObject.GetInformation(sProp))
            Me.lv.Items.Add(it)
        Next

        Me.lv.EndUpdate()
    End Sub

    Private Sub lv_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuPopup.Show(Me.lv, e.Location)
        End If
    End Sub

    Private Sub MenuItemCpProperty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCpProperty.Click
        Dim toCopy As String = ""
        For Each it As ListViewItem In Me.lv.SelectedItems
            toCopy &= it.Text & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCpValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCpValue.Click
        Dim toCopy As String = ""
        For Each it As ListViewItem In Me.lv.SelectedItems
            toCopy &= it.SubItems(1).Text & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub
End Class