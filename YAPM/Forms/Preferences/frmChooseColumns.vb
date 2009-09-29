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

Public Class frmChooseColumns

    Private theListview As customLV

    Public Property ConcernedListView() As customLV
        Get
            Return theListview
        End Get
        Set(ByVal value As customLV)
            theListview = value
        End Set
    End Property


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        theListview.generalLvSemaphore.WaitOne()
        theListview.ReorganizeColumns = True
        theListview.BeginUpdate()

        ' Remove all columns
        For x As Integer = theListview.Columns.Count - 1 To 1 Step -1
            theListview.Columns.Remove(theListview.Columns(x))
        Next

        ' Add new columns
        For Each it As ListViewItem In Me.lv.CheckedItems
            Dim width As Integer = CInt(Val(it.SubItems(1).Text))
            If width <= 0 Then
                width = 90        ' Default size
            End If
            theListview.Columns.Add(it.Text, width)
        Next

        ' Add items which are selected
        For Each it As ListViewItem In theListview.Items

            ' Can not use .Clear because it also remove the Item
            For i As Integer = it.SubItems.Count - 1 To 1 Step -1
                it.SubItems.RemoveAt(i)
            Next

            Dim subit() As String
            ReDim subit(Me.lv.CheckedItems.Count)

            For z As Integer = 0 To UBound(subit) - 1
                subit(z) = ""
            Next

            it.SubItems.AddRange(subit)
        Next

        ' Refresh all items & subitems
        ConcernedListView.CreateSubItemsBuffer()
        ConcernedListView.ForceRefreshingOfAllItems()

        theListview.ReorganizeColumns = False
        theListview.EndUpdate()
        theListview.generalLvSemaphore.Release()
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = True
        Next
    End Sub

    Private Sub btnUnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelAll.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = False
        Next
    End Sub

    Private Sub frmChooseProcessColumns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call closeWithEchapKey(Me)

        Native.Functions.Misc.SetTheme(Me.lv.Handle)

        Dim ss() As String
        ReDim ss(-1)

        ' This is some kind of shit.
        ' But as I can't write a MustOverride Shared Function...
        If TypeOf (ConcernedListView) Is handleList Then
            ss = handleInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is memoryList Then
            ss = memRegionInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is moduleList Then
            ss = moduleInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is networkList Then
            ss = networkInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is processList Then
            ss = processInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is serviceList Then
            ss = serviceInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is taskList Then
            ss = taskInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is threadList Then
            ss = threadInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is windowList Then
            ss = windowInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is privilegeList Then
            ss = privilegeInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is envVariableList Then
            ss = envVariableInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is jobLimitList Then
            ss = jobLimitInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is jobList Then
            ss = jobInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is processesInJobList Then
            ss = processInfos.GetAvailableProperties
        End If

        ReDim Preserve ss(ss.Length + 1)
        ss(ss.Length - 2) = "ObjectCreationDate"
        ss(ss.Length - 1) = "PendingTaskCount"

        ' Now add displayed columns names to list
        ' Add this columns by DisplayIndex order

        ' Start from 1 because item 0 is fixed and not added in our list
        For x As Integer = 1 To ConcernedListView.Columns.Count - 1
            Dim col As ColumnHeader = Common.Misc.GetColumnHeaderByDisplayIndex(ConcernedListView, x)
            Dim sss As String = col.Text.Replace("< ", "").Replace("> ", "")
            Dim it As New ListViewItem(sss)
            it.Checked = True
            it.Name = sss
            it.SubItems.Add(col.Width.ToString)
            Me.lv.Items.Add(it)
        Next

        ' Add other columns (which are not displayed)
        For Each s As String In ss
            If Me.lv.Items.ContainsKey(s) = False Then
                Dim it As New ListViewItem(s)
                it.SubItems.Add("")
                Me.lv.Items.Add(it)
            End If
        Next

    End Sub

    Private Sub cmdInvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInvert.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = Not (it.Checked)
        Next
    End Sub

    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        If Me.lv.SelectedItems Is Nothing OrElse Me.lv.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        If Me.lv.SelectedItems(0).Index = 0 Then
            Exit Sub
        End If

        Me.lv.BeginUpdate()
        MoveListViewItem(Me.lv, True)
        Me.lv.EndUpdate()
    End Sub

    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        If Me.lv.SelectedItems Is Nothing OrElse Me.lv.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        If Me.lv.SelectedItems(0).Index = Me.lv.Items.Count - 1 Then
            Exit Sub
        End If

        Me.lv.BeginUpdate()
        MoveListViewItem(Me.lv, False)
        Me.lv.EndUpdate()
    End Sub

    Private Sub cmdDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefault.Click
        ' Set default columns

    End Sub
End Class