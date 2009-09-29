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

Imports Common.Misc

Public Class frmChooseClientIp

    Private _chosenIp As String = ""
    Private _nics As List(Of Native.Api.Structs.NicDescription)

    Public Sub New(ByVal nics As List(Of Native.Api.Structs.NicDescription))
        InitializeComponent()
        _nics = nics
    End Sub

    Public Property ChosenIp() As String
        Get
            Return _chosenIp
        End Get
        Set(ByVal value As String)
            _chosenIp = value
        End Set
    End Property

    Private Sub frmChooseClientIp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.closeWithEchapKey(Me)
        SetToolTip(Me.lvNIC, "List of available netword card interface")
        SetToolTip(Me.cmdExit, "Cancel")
        SetToolTip(Me.cmdOk, "Use selected netword card interface")
        Native.Functions.Misc.SetTheme(Me.lvNIC.Handle)

        ' Display NICs
        For Each nic As Native.Api.Structs.NicDescription In _nics
            Dim it As New ListViewItem(nic.Name)
            it.SubItems.Add(nic.Ip)
            it.SubItems.Add(nic.Description)
            it.Tag = nic
            Me.lvNIC.Items.Add(it)
        Next

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.ChosenIp = CType(Me.lvNIC.SelectedItems(0).Tag, Native.Api.Structs.NicDescription).Ip
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub lvNIC_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvNIC.MouseDoubleClick
        If Me.lvNIC.SelectedItems.Count = 1 Then
            Call cmdOk_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvNIC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvNIC.SelectedIndexChanged
        Me.cmdOk.Enabled = (Me.lvNIC.SelectedItems.Count = 1)
    End Sub
End Class