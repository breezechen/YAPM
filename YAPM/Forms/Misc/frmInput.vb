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

Imports System.Windows.Forms

Public Class frmInput

    Private _res As String = Nothing

    Public ReadOnly Property Result() As String
        Get
            Return _res
        End Get
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        _res = Me.txtRes.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmInput_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Static _once As Boolean = True
        If _once Then
            Me.txtRes.SelectAll()
            Me.txtRes.Focus()
            _once = False
            If lblMessage.Width < 200 Then
                Me.lblMessage.AutoSize = False
                Me.lblMessage.Width = 200
            End If
            Me.Width = lblMessage.Width + 2 * lblMessage.Left
            Me.txtRes.Width = Me.Width - 2 * Me.txtRes.Left
            Me.Height += lblMessage.Height - 13
            Me.Left = CInt((Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2)
            Me.Top = CInt((Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2)
        End If
    End Sub

End Class
