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

    Public Const MSGFIRSTTIME As String = "This is the first time you run YAPM. Please remember that it is a beta5 version so there are some bugs and some missing functionnalities :-)" & vbNewLine & vbNewLine & "You should run YAPM as an administrator in order to fully control your processes. Please take care using this YAPM because you will be able to do some irreversible things if you kill or modify some system processes... Use it at your own risks !" & vbNewLine & vbNewLine & "Please let me know any of your ideas of improvement or new functionnalities in YAPM's sourceforge.net project page ('Help' pannel) :-)" & vbNewLine & vbNewLine & "This message won't be shown anymore :-)"

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
        _frmMain.Tray.Visible = My.Settings.ShowTrayIcon
        If first Then
            Call _frmMain.permuteMenuStyle(My.Settings.UseRibbonStyle)
            first = False
            _frmMain.TopMost = My.Settings.TopMost
            _frmMain.butAlwaysDisplay.Checked = My.Settings.TopMost
            If My.Settings.StartHidden Then
                _frmMain.WindowState = FormWindowState.Minimized
                _frmMain.Hide()
            End If
        End If
    End Sub

End Class
