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

Public Class frmNetworkTool

    Private _net As cNetwork
    Private _type As Native.Api.Enums.ToolType

    Public Sub New(ByVal conn As cNetwork, ByVal type As Native.Api.Enums.ToolType)
        InitializeComponent()
        _net = conn
        _type = type
        If _net.Infos.Remote Is Nothing Then
            Me.Text = "No IP address !"
            Me.lv.Enabled = False
        Else
            Me.lv.Enabled = True
            Me.Text = "IP address : " & _net.Infos.Remote.Address.ToString
        End If
    End Sub

    Private Sub frmFileRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdOK, "OK")
        SetToolTip(Me.cmdReop, "Retry the operation")
        Native.Functions.Misc.SetTheme(Me.lv.Handle)
        Me.cmdReop_Click(Nothing, Nothing)
    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Common.Misc.CopyLvToClip(e, Me.lv)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub lv_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.MenuItemCopyObj.Enabled = (Me.lv.SelectedItems.Count > 0)
            Me.mnuPopup.Show(Me.lv, e.Location)
        End If
    End Sub

    Private Sub MenuItemCopyObj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyObj.Click
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

    ' Start operation
    Private Sub asyncGo(ByVal context As Object)
        If _net Is Nothing Then
            Exit Sub
        End If
        Select Case _type
            Case Native.Api.Enums.ToolType.Ping
                _net.Ping(Me.lv)
            Case Native.Api.Enums.ToolType.TraceRoute
                _net.TraceRoute(Me.lv)
            Case Native.Api.Enums.ToolType.WhoIs
                _net.WhoIs(Me.lv)
            Case Else
                '
        End Select
    End Sub

    Private Sub cmdReop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReop.Click
        If _net IsNot Nothing AndAlso _net.Infos.Remote IsNot Nothing Then
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf asyncGo, Nothing)
        End If
    End Sub

End Class