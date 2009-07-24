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

Public Class frmPendingTasks

    Private _obj As cGeneralObject

    Private NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Private DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)

    Private Sub frm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call closeWithEchapKey(Me)
        API.SetWindowTheme(lv.Handle, "explorer", Nothing)
        Call RefreshTasksList()
    End Sub

    Public Sub New(ByRef obj As cGeneralObject)
        InitializeComponent()
        _obj = obj
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Call RefreshTasksList()
    End Sub

    ' List all pending tasks
    Private Sub RefreshTasksList()

        Static first As Boolean = True

        Dim _list As Dictionary(Of Integer, System.Threading.WaitCallback) = _obj.GetPendingTasks2

        ' Remove 'red' items (previously deleted)
        For Each it As ListViewItem In Me.lv.Items
            If it.BackColor = DELETED_ITEM_COLOR Then
                it.Remove()
            End If
        Next

        ' Deleted items
        For Each it As ListViewItem In Me.lv.Items
            Dim exist As Boolean = False
            For Each thr As System.Collections.Generic.KeyValuePair(Of Integer, System.Threading.WaitCallback) In _list
                If thr.Key.ToString = it.Text Then
                    ' Still existing -> update infos
                    exist = True
                    Exit For
                End If
            Next

            If exist = False Then
                ' Deleted
                it.BackColor = DELETED_ITEM_COLOR
            End If

        Next


        ' Remove 'green' items (previously deleted)
        For Each it As ListViewItem In Me.lv.Items
            If it.BackColor = NEW_ITEM_COLOR Then
                it.BackColor = Color.White
            End If
        Next


        ' New items
        For Each thr As System.Collections.Generic.KeyValuePair(Of Integer, System.Threading.WaitCallback) In _list
            Dim exist As Boolean = False
            For Each itt As ListViewItem In Me.lv.Items
                If itt.Text = thr.Key.ToString Then
                    exist = True
                    Exit For
                End If
            Next

            If exist = False Then
                ' Have to create
                Dim nene As New ListViewItem(thr.Key.ToString)
                If first = False Then
                    nene.BackColor = NEW_ITEM_COLOR
                End If
                nene.ForeColor = Color.FromArgb(30, 30, 30)
                Dim items(1) As String
                items(0) = thr.Value.Target.ToString
                items(1) = thr.Value.Method.ToString
                nene.SubItems.AddRange(items)
                Me.lv.Items.Add(nene)
            End If

        Next

        ' Change infos
        'For Each it As ListViewItem In Me.lv.Items
        '    Dim theThread As System.Threading.WaitCallback = Nothing
        '    For Each thr As System.Collections.Generic.KeyValuePair(Of Integer, System.Threading.WaitCallback) In _list
        '        If thr.Key.ToString = it.Text Then
        '            theThread = thr.Value
        '            Exit For
        '        End If
        '    Next

        '    'If theThread IsNot Nothing Then
        '    '    it.SubItems(1).Text = theThread.ThreadState.ToString
        '    '    it.SubItems(2).Text = theThread.Priority.ToString
        '    'End If
        'Next

        first = False
    End Sub

End Class