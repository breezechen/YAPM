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

    Private Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Integer, ByVal bRevert As Integer) As Integer
    Private Declare Function EnableMenuItem Lib "user32" (ByVal hMenu As Integer, ByVal wIDEnableItem As Integer, ByVal wEnable As Integer) As Integer
    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
    End Function

    Private Enum LVM
        LVM_FIRST = &H1000
        LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54)
        LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55)
    End Enum

    Private Const SC_CLOSE As Integer = &HF060
    Private Const MF_GRAYED As Integer = &H1
    Private Const LVS_EX_BORDERSELECT As Integer = &H8000
    Private Const LVS_EX_DOUBLEBUFFER As Integer = &H10000

    Private Sub DoubleBufferListView(ByRef lvH As IntPtr)
        Dim styles As Integer = CInt(SendMessage(lvH, LVM.LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0))
        styles += LVS_EX_DOUBLEBUFFER Or LVS_EX_BORDERSELECT
        SendMessage(lv.Handle, LVM.LVM_SETEXTENDEDLISTVIEWSTYLE, 0, styles)
    End Sub

    Private Sub frmSBASimulationConsole_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub

    Private Sub frmSBASimulationConsole_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        EnableMenuItem(GetSystemMenu(Me.Handle.ToInt32, 0), SC_CLOSE, MF_GRAYED)
        SetWindowTheme(lv.Handle, "explorer", Nothing)
        DoubleBufferListView(Me.lv.Handle)
    End Sub

    Private Sub ClearConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearConsoleToolStripMenuItem.Click
        Me.lv.Items.Clear()
    End Sub
End Class