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

Public Class frmChooseProcess

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Private _cproc As cProcess

    Public ReadOnly Property SelectedProcess() As cProcess
        Get
            Return _cproc
        End Get
    End Property

    Private Sub timerProcRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcRefresh.Tick
        lvProcess.UpdateTheItems()
    End Sub

    Private Sub lvProcess_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvProcess.DoubleClick
        If lvProcess.SelectedItems.Count > 0 Then
            _cproc = lvProcess.GetSelectedItem
            Me.Close()
        End If
    End Sub

    Private Sub frmChooseProcess_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.timerProcRefresh.Enabled = False
    End Sub

    Private Sub frmChooseProcess_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetWindowTheme(lvProcess.Handle, "explorer", Nothing)
        lvProcess.UpdateTheItems()
    End Sub
End Class