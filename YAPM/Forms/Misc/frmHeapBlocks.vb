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
Imports Common.Misc

Public Class frmHeapBlocks

    Private _pid As Integer
    Private _nodeAdd As IntPtr

    Public Sub New(ByVal processId As Integer, ByVal nodeAddress As IntPtr)
        InitializeComponent()
        _pid = processId
        _nodeAdd = nodeAddress
        Me.Text = "Heap blocks (node address : 0x" & nodeAddress.ToString("x") & ")"
    End Sub

    Private Sub frmFileRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdOK, "OK")
        Native.Functions.Misc.SetTheme(Me.lv.Handle)
        Me.Invoke(New [Enum](AddressOf asyncEnum))
    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Common.Misc.CopyLvToClip(e, Me.lv)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    ' Start the enumeration
    Private Delegate Sub [Enum]()
    Private Sub asyncEnum()
        Me.lv.Enabled = False
        Me.lv.BeginUpdate()
        Me.lv.Items.Clear()

        ' Enumerate
        Dim _dico As Dictionary(Of String, Native.Api.NativeStructs.HeapBlock) = _
            Native.Objects.Heap.EnumerateBlocksByNodeAddress(_pid, _nodeAdd)

        ' Add items
        For Each bl As Native.Api.NativeStructs.HeapBlock In _dico.Values
            Dim it As New ListViewItem("0x" & bl.Address.ToString("x"))
            With it
                .SubItems.Add("0x" & bl.Size.ToString("x"))
                .SubItems.Add(bl.Flags.ToString)
                .Tag = bl
                Me.lv.Items.Add(it)
            End With
        Next

        Me.lv.EndUpdate()
        Me.lv.Enabled = True
    End Sub

    Private Sub MenuItemCpAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCpAddress.Click
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As ListViewItem In Me.lv.SelectedItems
            toCopy &= it.Text
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCpSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCpSize.Click
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As ListViewItem In Me.lv.SelectedItems
            toCopy &= it.SubItems(1).Text
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCpStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCpStatus.Click
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As ListViewItem In Me.lv.SelectedItems
            toCopy &= it.SubItems(2).Text
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub lv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lv.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Me.MenuItemViewMemory_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lv_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseDoubleClick
        Call Me.MenuItemViewMemory_Click(Nothing, Nothing)
    End Sub

    Private Sub lv_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.MenuItemCopyObj.Enabled = (Me.lv.SelectedItems.Count > 0)
            Me.MenuItemViewMemory.Enabled = (Me.lv.SelectedItems.Count > 0)
            Me.mnuPopup.Show(Me.lv, e.Location)
        End If
    End Sub

    Private Sub MenuItemViewMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemViewMemory.Click
        ' View memory
        For Each it As ListViewItem In Me.lv.SelectedItems
            Dim bl As Native.Api.NativeStructs.HeapBlock = CType(it.Tag, Native.Api.NativeStructs.HeapBlock)
            Dim frm As New frmHexEditor
            Dim reg As New MemoryHexEditor.MemoryRegion(bl.Address, New IntPtr(bl.Size))
            frm.SetPidAndRegion(_pid, reg)
            frm.Show()
        Next
    End Sub

End Class