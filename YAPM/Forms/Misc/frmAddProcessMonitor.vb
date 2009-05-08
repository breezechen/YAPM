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

Public Class frmAddProcessMonitor

    ' Process to select by default
    Public _selProcess As Integer

    Private _con As cConnection

    Private Structure monCounter
        Dim instanceName As String
        Dim counterTypeName As String
        Dim categoryName As String
    End Structure

    Public Sub New(ByRef connection As cConnection)
        InitializeComponent()
        _con = connection
    End Sub

    Private Sub frmAddProcessMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        With frmMain
            .SetToolTip(Me.butAdd, "Monitor the selected process.")
            .SetToolTip(Me.txtInterval, "Set the refresh interval (milliseconds).")
            .SetToolTip(Me.cmdAddToList, "Add counters from list.")
            .SetToolTip(Me.cmdRemoveFromList, "Remove counters from list.")
            .SetToolTip(Me.lstToAdd, "Counters to monitor.")
            .SetToolTip(Me.lstCounterType, "Available counters.")
            .SetToolTip(Me.lstInstance, "Available instances.")
            .SetToolTip(Me.lstCategory, "Available categories.")
        End With

        API.SetWindowTheme(Me.lstToAdd.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lstInstance.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lstCounterType.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lstCategory.Handle, "explorer", Nothing)

        'Call Me.cmdRefresh_Click(Nothing, Nothing)

        '' Select desired process (_selProcess)
        'Dim s As String
        'For Each s In Me.cbProcess.Items
        '    Dim i As Integer = InStr(s, " -- ", CompareMethod.Binary)
        '    Dim _name As String = s.Substring(0, i - 1)
        '    Dim _pid As Integer = CInt(Val(s.Substring(i + 3, s.Length - i - 3)))
        '    If _pid = _selProcess Then
        '        Me.cbProcess.Text = s
        '        Exit For
        '    End If
        'Next
        Try
            Dim myCat2 As PerformanceCounterCategory()
            Dim i As Integer
            Me.lstCategory.Items.Clear()
            If _con.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat2 = PerformanceCounterCategory.GetCategories(_con.WmiParameters.serverName)
            Else    ' Local
                myCat2 = PerformanceCounterCategory.GetCategories
            End If
            For i = 0 To myCat2.Length - 1
                Me.lstCategory.Items.Add(myCat2(i).CategoryName)
            Next
        Catch ex As Exception
            ' Cannot connect to network or ??
            If IsWindowsVista() Then
                ShowVistaMessage(Me.Handle, "Error", "An error occured", ex.Message, TaskDialogCommonButtons.Ok, TaskDialogIcon.ShieldError)
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        End Try

    End Sub

    Private Sub butAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAdd.Click
        ' Monitor our process
        Dim lstIt As ListViewItem

        For Each lstIt In Me.lstToAdd.Items

            Dim obj As monCounter = CType(lstIt.Tag, Global.YAPM.frmAddProcessMonitor.monCounter)

            With obj
                Dim _name As String = .instanceName
                Dim _cat As String = .categoryName
                Dim _count As String = .counterTypeName

                Dim it As cMonitor
                If _con.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                    it = New cMonitor(_cat, _count, _name, _con.WmiParameters.serverName)
                Else
                    it = New cMonitor(_cat, _count, _name)
                End If
                it.Interval = CInt(Val(Me.txtInterval.Text))
                _frmMain.AddMonitoringItem(it)
            End With

        Next

        Me.Close()

    End Sub

    Private Sub cmdAddToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddToList.Click
        ' Add selected counters to wish list
        Dim listIt As ListViewItem

        Dim _name As String = vbNullString
        Dim _cat As String = vbNullString
        Dim _count As String = vbNullString
        If Me.lstCategory.SelectedItems Is Nothing Then Exit Sub

        For Each listIt In Me.lstCounterType.SelectedItems

            _count = listIt.Text
            If Me.lstInstance.SelectedItems IsNot Nothing AndAlso Me.lstInstance.SelectedItems.Count > 0 Then _name = Me.lstInstance.SelectedItems(0).Text
            If _count = vbNullString And _name = vbNullString Then Exit Sub
            _cat = Me.lstCategory.SelectedItems(0).Text

            Dim it As New monCounter

            With it
                .categoryName = _cat
                .counterTypeName = _count
                .instanceName = _name
            End With

            Dim sName As String = _cat & " -- " & CStr(IIf(_name = vbNullString, vbNullString, _name & " -- ")) & _count
            Dim bPresent As Boolean = False

            ' Check if item is already added or not
            Dim lvIt As ListViewItem
            For Each lvIt In Me.lstToAdd.Items
                If lvIt.Text = sName Then
                    bPresent = True
                    Exit For
                End If
            Next

            If bPresent = False Then
                Dim itList As New ListViewItem
                itList.Text = sName
                itList.Tag = it
                Me.lstToAdd.Items.Add(itList)
            End If
        Next

        Me.butAdd.Enabled = (Me.lstCounterType.Items.Count > 0)
    End Sub

    Private Sub cmdRemoveFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveFromList.Click
        Dim it As ListViewItem
        For Each it In Me.lstToAdd.SelectedItems
            it.Remove()
        Next
        Me.butAdd.Enabled = (Me.lstCounterType.Items.Count > 0)
    End Sub

    Private Sub lstCategory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCategory.MouseDown
        Call mdlMisc.CopyLvToClip(e, lstCategory)
    End Sub

    Private Sub lstCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCategory.SelectedIndexChanged
        Dim mypc() As String
        Dim i As Integer
        If lstCategory.SelectedItems IsNot Nothing AndAlso lstCategory.SelectedItems.Count > 0 Then
            Dim myCat As PerformanceCounterCategory
            If _con.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text)
            End If
            txtHelp.Text = myCat.CategoryHelp
            Me.lstInstance.Items.Clear()
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetInstanceNames
                For i = 0 To mypc.Length - 1
                    Me.lstInstance.Items.Add(mypc(i))
                Next
            Catch ex As Exception
            End Try
            Call lstInstance_SelectedIndexChanged(Nothing, Nothing)
        End If

    End Sub

    Private Sub lstInstance_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstInstance.MouseDown
        Call mdlMisc.CopyLvToClip(e, lstInstance)
    End Sub

    Private Sub lstInstance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstInstance.SelectedIndexChanged
        Dim mypc() As PerformanceCounter
        Dim i As Integer
        Me.lstCounterType.Items.Clear()
        If lstInstance.SelectedItems.Count = 0 Then
            Dim myCat As PerformanceCounterCategory
            If _con.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text)
            End If
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetCounters()
                For i = 0 To mypc.Length - 1
                    Me.lstCounterType.Items.Add(mypc(i).CounterName)
                Next
            Catch ex As Exception
            End Try
        Else
            Dim myCat As PerformanceCounterCategory
            If _con.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text)
            End If
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetCounters(lstInstance.SelectedItems(0).Text)
                For i = 0 To mypc.Length - 1
                    Me.lstCounterType.Items.Add(mypc(i).CounterName)
                Next
            Catch ex As Exception
            End Try
        End If
        Me.butAdd.Enabled = (Me.lstCounterType.SelectedItems.Count > 0)
    End Sub

    Private Sub lstCounterType_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCounterType.MouseDown
        Call mdlMisc.CopyLvToClip(e, lstCounterType)
    End Sub

    Private Sub lstCounterType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCounterType.SelectedIndexChanged
        If lstCounterType.SelectedItems IsNot Nothing AndAlso lstCounterType.SelectedItems.Count > 0 Then
            Dim myCat As PerformanceCounter
            If _con.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounter(Me.lstCategory.SelectedItems(0).Text, lstCounterType.SelectedItems(0).Text, Nothing, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounter(Me.lstCategory.SelectedItems(0).Text, lstCounterType.SelectedItems(0).Text)
            End If
            txtHelp.Text = myCat.CounterHelp
        End If
    End Sub

    Private Sub lstToAdd_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstToAdd.MouseDown
        Call mdlMisc.CopyLvToClip(e, lstToAdd)
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        frmSearchMonitor.ShowDialog()
    End Sub
End Class