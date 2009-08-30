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

Public Class Pref

    Public Const LEFT_POSITION_HIDDEN As Integer = -20000
    Public Const MSGFIRSTTIME As String = "This is the first time you run YAPM. Please remember that it is still a beta version so there are some bugs and some missing functionnalities :-)" & vbNewLine & vbNewLine & "You should run YAPM as an administrator in order to fully control your processes. Please take care using this YAPM because you will be able to do some irreversible things if you kill or modify some system processes... Use it at your own risks !" & vbNewLine & vbNewLine & "Please let me know any of your ideas of improvement or new functionnalities in YAPM's sourceforge.net project page ('Help' pannel) :-)" & vbNewLine & vbNewLine & "This message won't be shown anymore :-)"

    ' Save
    Public Sub Save()
        My.Settings.Save()
    End Sub

    ' Set to default
    Public Sub SetDefault()
        My.Settings.Reset()
        My.Settings.Save()
        Me.Apply()
    End Sub

    ' Apply pref
    Public Sub Apply()
        Static first As Boolean = True
        _frmMain.timerProcess.Interval = My.Settings.ProcessInterval
        _frmMain.timerServices.Interval = My.Settings.ServiceInterval
        _frmMain.timerNetwork.Interval = My.Settings.NetworkInterval
        _frmMain.timerTask.Interval = My.Settings.TaskInterval
        _frmMain.timerTrayIcon.Interval = My.Settings.TrayInterval
        _frmMain.timerJobs.Interval = My.Settings.JobInterval
        Select Case My.Settings.Priority
            Case 0
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.Idle
            Case 1
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.BelowNormal
            Case 2
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.Normal
            Case 3
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.AboveNormal
            Case 4
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.High
            Case 5
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.RealTime
        End Select
        handleList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        handleList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        memoryList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        memoryList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        windowList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        windowList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        moduleList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        moduleList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        networkList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        networkList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        processList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        processList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        serviceList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        serviceList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        taskList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        taskList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        threadList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        threadList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        jobList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        jobList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        _frmMain.Tray.Visible = My.Settings.ShowTrayIcon
        _frmMain.StatusBar.Visible = My.Settings.ShowStatusBar
        If first Then
            Call _frmMain.permuteMenuStyle(My.Settings.UseRibbonStyle)
            first = False
            _frmMain.TopMost = My.Settings.TopMost
            _frmMain.butAlwaysDisplay.Checked = My.Settings.TopMost
            _frmMain.Visible = Not (My.Settings.StartHidden)
            _frmMain.MenuItemMainAlwaysVisible.Checked = My.Settings.TopMost
            _frmMain.MenuItemNotifNP.Checked = My.Settings.NotifyNewProcesses
            _frmMain.MenuItemNotifDS.Checked = My.Settings.NotifyDeletedServices
            _frmMain.MenuItemNotifNS.Checked = My.Settings.NotifyNewServices
            _frmMain.MenuItemNotifTP.Checked = My.Settings.NotifyTerminatedProcesses
            'If My.Settings.StartHidden Then
            '    _frmMain.Hide()
            '    _frmMain.Left = LEFT_POSITION_HIDDEN
            'End If 'HIDDEN
            '_frmMain.ShowInTaskbar = Not (My.Settings.StartHidden)
        End If

        ' Highlightings
        With My.Settings
            cThread.SetHighlightings(.EnableHighlightingSuspendedThread)
            cThread.SetHighlightingsColor(.HighlightingColorSuspendedThread)
            cModule.SetHighlightings(.EnableHighlightingRelocatedModule)
            cModule.SetHighlightingsColor(.HighlightingColorRelocatedModule)
            cProcess.SetHighlightings(.EnableHighlightingBeingDebugged, _
                                      .EnableHighlightingJobProcess, _
                                      .EnableHighlightingElevated, _
                                      .EnableHighlightingCriticalProcess, _
                                      .EnableHighlightingOwnedProcess, _
                                      .EnableHighlightingSystemProcess, _
                                      .EnableHighlightingServiceProcess)
            cProcess.SetHighlightingsColor(.HighlightingColorBeingDebugged, _
                                           .HighlightingColorJobProcess, _
                                           .HighlightingColorElevatedProcess, _
                                           .HighlightingColorCriticalProcess, _
                                           .HighlightingColorOwnedProcess, _
                                           .HighlightingColorSystemProcess, _
                                           .HighlightingColorServiceProcess)
        End With
    End Sub

    ' Display columns of a listview (previously saved)
    Public Shared Sub LoadListViewColumns(ByVal lv As customLV, ByVal name As String)


        ' Here is an example of column description :
        ' col1?width1?index1$col2?width2?index2$...
        Dim s As String = ""
        Try
            s = CStr(My.Settings(name))
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        If s Is Nothing OrElse s.Length < 3 Then
            Trace.WriteLine("could not read column configuration for a listview" & vbNewLine & name & "  " & getColumnDesc(lv))
            Exit Sub
        End If

        lv.BeginUpdate()
        lv.ReorganizeColumns = True
        lv.Columns.Clear()

        Dim res() As String = Split(s, "$")
        For Each column As String In res
            If Len(column) > 0 Then
                Dim obj() As String = Split(column, "?")
                Dim col As ColumnHeader = lv.Columns.Add(obj(0), CInt(Val((obj(1)))))
            End If
        Next

        For u As Integer = 0 To lv.Columns.Count - 1
            For Each column As String In res
                Dim obj() As String = Split(column, "?")
                If lv.Columns(u).Text = obj(0) Then
                    lv.Columns(u).DisplayIndex = CInt(Val(obj(2)))
                    Exit For
                End If
            Next
        Next

        lv.ReorganizeColumns = False
        lv.EndUpdate()
    End Sub

    ' Save columns list of a listview
    Public Shared Sub SaveListViewColumns(ByVal lv As ListView, ByVal name As String)

        Dim s As String = ""

        For Each it As ColumnHeader In lv.Columns
            s &= it.Text.Replace("< ", "").Replace("> ", "") & "?" & it.Width.ToString & "?" & it.DisplayIndex.ToString & "$"
        Next

        My.Settings(name) = s

    End Sub

    ' Get current configuration of columns of a listview
    ' (only used for debug)
    Private Shared Function getColumnDesc(ByVal lv As ListView) As String
        Dim s As String = ""

        For Each it As ColumnHeader In lv.Columns
            s &= it.Text.Replace("< ", "").Replace("> ", "") & "?" & it.Width.ToString & "$"
        Next

        Return s
    End Function

End Class
