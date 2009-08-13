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

Public Class frmSBASimulationConsole

    Private Sub frmSBASimulationConsole_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub

    Private Sub frmSBASimulationConsole_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Native.Api.NativeFunctions.EnableMenuItem(Native.Api.NativeFunctions.GetSystemMenu(Me.Handle, False), Native.Api.NativeConstants.SC_CLOSE, Native.Api.NativeConstants.MF_GRAYED)
        Native.Functions.Misc.SetTheme(lv.Handle)
        Native.Functions.Misc.SetListViewAsDoubleBuffered(Me.lv)
    End Sub

    Private Sub ClearConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearConsoleToolStripMenuItem.Click
        Me.lv.Items.Clear()
    End Sub
End Class