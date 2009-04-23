﻿' =======================================================
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

Imports System.Windows.Forms

Public Class frmHiddenProcesses

    Private Sub frmHiddenProcesses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call API.SetWindowTheme(Me.lvProcess.Handle, "explorer", Nothing)

        Dim theConnection As New cConnection

        theConnection.ConnectionType = cConnection.TypeOfConnection.LocalConnection

        Me.lvProcess.ClearItems()
        Me.lvProcess.ConnectionObj = theConnection

        theConnection.Connect()
        Me.TimerProcess.Enabled = True

    End Sub

    Private Sub TimerProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerProcess.Tick
        Me.lvProcess.UpdateItems()
        lblTotal.Text = Me.lvProcess.Items.Count.ToString & " processes"
        Dim _hidd As Integer = 0
        For Each p As cProcess In Me.lvProcess.GetAllItems
            If p.Infos.IsHidden Then
                _hidd += 1
            End If
        Next
        lblHidden.Text = _hidd.ToString & " hidden processes"
        lblVisible.Text = (Me.lvProcess.Items.Count - _hidd).ToString & " visible processes"
    End Sub

    Private Sub handleMethod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles handleMethod.Click
        handleMethod.Checked = True
        bruteforceMethod.Checked = False
        Me.lvProcess.ClearItems()
        Me.lvProcess.EnumMethod = asyncCallbackProcEnumerate.ProcessEnumMethode.HandleMethod
    End Sub

    Private Sub bruteforceMethod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bruteforceMethod.Click
        handleMethod.Checked = False
        bruteforceMethod.Checked = True
        Me.lvProcess.ClearItems()
        Me.lvProcess.EnumMethod = asyncCallbackProcEnumerate.ProcessEnumMethode.BruteForce
    End Sub
End Class