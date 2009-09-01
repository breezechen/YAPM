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
Imports YAPM.Program
Imports YAPM.Common.Misc

Public Class frmMain

    ' ========================================
    ' Private attributes
    ' ========================================
    Private WithEvents creg As cRegMonitor
    Private _ribbonStyle As Boolean = True
    Private _local As Boolean = True
    Private _notWMI As Boolean = True
    Private _callExitWhenExitYAPM As Boolean = True
    Private handlesToRefresh() As Integer
    Private threadsToRefresh() As Integer
    Private modulesToRefresh() As Integer
    Private windowsToRefresh() As Integer
    Private cSelFile As cFile


    ' ========================================
    ' Public & properties
    ' ========================================
    Public Property CallExitWhenExitYAPM() As Boolean
        Get
            Return _callExitWhenExitYAPM
        End Get
        Set(ByVal value As Boolean)
            _callExitWhenExitYAPM = value
        End Set
    End Property

    Public _shutdownConnection As New cShutdownConnection(Me, Program.Connection)


    ' ========================================
    ' Constants
    ' ========================================
    Private Const SIZE_FOR_STRING As Integer = 4


    ' ========================================
    ' Form functions
    ' ========================================

    ' Refresh File informations
    Public Sub refreshFileInfos(ByVal file As String)

        Dim s As String = ""

        cSelFile = New cFile(file, True)

        If IO.File.Exists(file) Then

            ' Set dates to datepickers
            Me.DTcreation.Value = cSelFile.CreationTime
            Me.DTlastAccess.Value = cSelFile.LastAccessTime
            Me.DTlastModification.Value = cSelFile.LastWriteTime

            ' Set attributes
            Dim att As System.IO.FileAttributes = cSelFile.Attributes()
            Me.chkFileArchive.Checked = ((att And IO.FileAttributes.Archive) = IO.FileAttributes.Archive)
            Me.chkFileCompressed.Checked = ((att And IO.FileAttributes.Compressed) = IO.FileAttributes.Compressed)
            Me.chkFileHidden.Checked = ((att And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden)
            Me.chkFileReadOnly.Checked = ((att And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
            Me.chkFileSystem.Checked = ((att And IO.FileAttributes.System) = IO.FileAttributes.System)
            Me.chkFileNormal.Checked = ((att And IO.FileAttributes.Normal) = IO.FileAttributes.Normal)
            Me.chkFileContentNotIndexed.Checked = ((att And IO.FileAttributes.NotContentIndexed) = IO.FileAttributes.NotContentIndexed)
            Me.chkFileEncrypted.Checked = ((att And IO.FileAttributes.Encrypted) = IO.FileAttributes.Encrypted)

            ' Clean string list
            Me.lvFileString.Items.Clear()
            Me.lvFileString.Items.Add("Click on 'Others->Show file strings' to retrieve file strings")

            s &= "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}{\f1\fswiss\fcharset0 Arial;}}"
            s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   "
            s &= "\b File basic properties\b0\par"
            s &= "\tab File name :\tab\tab " & cSelFile.Name & "\par"
            s &= "\tab Parent directory :\tab " & cSelFile.ParentDirectory & "\par"
            s &= "\tab Extension :\tab\tab " & cSelFile.FileExtension & "\par"
            s &= "\tab Creation date :\tab\tab " & cSelFile.DateCreated & "\par"
            s &= "\tab Last access date :\tab " & cSelFile.DateLastAccessed & "\par"
            s &= "\tab Last modification date :\tab " & cSelFile.DateLastModified & "\par"
            s &= "\tab Size :\tab\tab\tab " & cSelFile.FileSize & " Bytes -- " & Math.Round(cSelFile.FileSize / 1024, 3) & " KB" & " -- " & Math.Round(cSelFile.FileSize / 1024 / 1024, 3) & "MB\par"
            s &= "\tab Compressed size :\tab " & cSelFile.CompressedFileSize & " Bytes -- " & Math.Round(cSelFile.CompressedFileSize / 1024, 3) & " KB" & " -- " & Math.Round(cSelFile.CompressedFileSize / 1024 / 1024, 3) & "MB\par\par"
            s &= "\b File advances properties\b0\par"
            s &= "\tab File type :\tab\tab " & cSelFile.FileType & "\par"
            s &= "\tab Associated program :\tab " & cSelFile.FileAssociatedProgram & "\par"
            s &= "\tab Short name :\tab\tab " & cSelFile.ShortName & "\par"
            s &= "\tab Short path :\tab\tab " & cSelFile.ShortPath & "\par"
            s &= "\tab Directory depth :\tab\tab " & cSelFile.DirectoryDepth & "\par"
            s &= "\tab File available for read :\tab " & cSelFile.FileAvailableForWrite & "\par"
            s &= "\tab File available for write :\tab " & cSelFile.FileAvailableForWrite & "\par\par"
            s &= "\b Attributes\b0\par"
            s &= "\tab Archive :\tab\tab " & cSelFile.IsArchive & "\par"
            s &= "\tab Compressed :\tab\tab " & cSelFile.IsCompressed & "\par"
            s &= "\tab Device :\tab\tab\tab " & cSelFile.IsDevice & "\par"
            s &= "\tab Directory :\tab\tab " & cSelFile.IsDirectory & "\par"
            s &= "\tab Encrypted :\tab\tab " & cSelFile.IsEncrypted & "\par"
            s &= "\tab Hidden :\tab\tab\tab " & cSelFile.IsHidden & "\par"
            s &= "\tab Normal :\tab\tab\tab " & cSelFile.IsNormal & "\par"
            s &= "\tab Not content indexed :\tab " & cSelFile.IsNotContentIndexed & "\par"
            s &= "\tab Offline :\tab\tab\tab " & cSelFile.IsOffline & "\par"
            s &= "\tab Read only :\tab\tab " & cSelFile.IsReadOnly & "\par"
            s &= "\tab Reparse file :\tab\tab " & cSelFile.IsReparsePoint & "\par"
            s &= "\tab Fragmented :\tab\tab " & cSelFile.IsSparseFile & "\par"
            s &= "\tab System :\tab\tab " & cSelFile.IsSystem & "\par"
            s &= "\tab Temporary :\tab\tab " & cSelFile.IsTemporary & "\par\par"
            s &= "\b File version infos\b0\par"

            If cSelFile.FileVersion IsNot Nothing Then
                If cSelFile.FileVersion.Comments IsNot Nothing AndAlso cSelFile.FileVersion.Comments.Length > 0 Then _
                    s &= "\tab Comments :\tab\tab " & cSelFile.FileVersion.Comments & "\par"
                If cSelFile.FileVersion.CompanyName IsNot Nothing AndAlso cSelFile.FileVersion.CompanyName.Length > 0 Then _
                    s &= "\tab CompanyName :\tab\tab " & cSelFile.FileVersion.CompanyName & "\par"
                If CStr(cSelFile.FileVersion.FileBuildPart).Length > 0 Then _
                    s &= "\tab FileBuildPart :\tab\tab " & CStr(cSelFile.FileVersion.FileBuildPart) & "\par"
                If cSelFile.FileVersion.FileDescription IsNot Nothing AndAlso cSelFile.FileVersion.FileDescription.Length > 0 Then _
                    s &= "\tab FileDescription :\tab\tab " & cSelFile.FileVersion.FileDescription & "\par"
                If CStr(cSelFile.FileVersion.FileMajorPart).Length > 0 Then _
                    s &= "\tab FileMajorPart :\tab\tab " & CStr(cSelFile.FileVersion.FileMajorPart) & "\par"
                If CStr(cSelFile.FileVersion.FileMinorPart).Length > 0 Then _
                    s &= "\tab FileMinorPart :\tab\tab " & cSelFile.FileVersion.FileMinorPart & "\par"
                If CStr(cSelFile.FileVersion.FilePrivatePart).Length > 0 Then _
                    s &= "\tab FilePrivatePart :\tab\tab " & cSelFile.FileVersion.FilePrivatePart & "\par"
                If cSelFile.FileVersion.FileVersion IsNot Nothing AndAlso cSelFile.FileVersion.FileVersion.Length > 0 Then _
                    s &= "\tab FileVersion :\tab\tab " & cSelFile.FileVersion.FileVersion & "\par"
                If cSelFile.FileVersion.InternalName IsNot Nothing AndAlso cSelFile.FileVersion.InternalName.Length > 0 Then _
                    s &= "\tab InternalName :\tab\tab " & cSelFile.FileVersion.InternalName & "\par"
                If CStr(cSelFile.FileVersion.IsDebug).Length > 0 Then _
                    s &= "\tab IsDebug :\tab\tab " & cSelFile.FileVersion.IsDebug & "\par"
                If CStr(cSelFile.FileVersion.IsPatched).Length > 0 Then _
                    s &= "\tab IsPatched :\tab\tab " & cSelFile.FileVersion.IsPatched & "\par"
                If CStr(cSelFile.FileVersion.IsPreRelease).Length > 0 Then _
                    s &= "\tab IsPreRelease :\tab\tab " & cSelFile.FileVersion.IsPreRelease & "\par"
                If CStr(cSelFile.FileVersion.IsPrivateBuild).Length > 0 Then _
                    s &= "\tab IsPrivateBuild :\tab\tab " & cSelFile.FileVersion.IsPrivateBuild & "\par"
                If CStr(cSelFile.FileVersion.IsSpecialBuild).Length > 0 Then _
                    s &= "\tab IsSpecialBuild :\tab\tab " & cSelFile.FileVersion.IsSpecialBuild & "\par"
                If cSelFile.FileVersion.Language IsNot Nothing AndAlso cSelFile.FileVersion.Language.Length > 0 Then _
                    s &= "\tab Language :\tab\tab " & cSelFile.FileVersion.Language & "\par"
                If cSelFile.FileVersion.LegalCopyright IsNot Nothing AndAlso cSelFile.FileVersion.LegalCopyright.Length > 0 Then _
                    s &= "\tab LegalCopyright :\tab\tab " & cSelFile.FileVersion.LegalCopyright & "\par"
                If cSelFile.FileVersion.LegalTrademarks IsNot Nothing AndAlso cSelFile.FileVersion.LegalTrademarks.Length > 0 Then _
                    s &= "\tab LegalTrademarks :\tab " & cSelFile.FileVersion.LegalTrademarks & "\par"
                If cSelFile.FileVersion.OriginalFilename IsNot Nothing AndAlso cSelFile.FileVersion.OriginalFilename.Length > 0 Then _
                    s &= "\tab OriginalFilename :\tab\tab " & cSelFile.FileVersion.OriginalFilename & "\par"
                If cSelFile.FileVersion.PrivateBuild IsNot Nothing AndAlso cSelFile.FileVersion.PrivateBuild.Length > 0 Then _
                    s &= "\tab PrivateBuild :\tab\tab " & cSelFile.FileVersion.PrivateBuild & "\par"
                If CStr(cSelFile.FileVersion.ProductBuildPart).Length > 0 Then _
                    s &= "\tab ProductBuildPart :\tab " & cSelFile.FileVersion.ProductBuildPart & "\par"
                If CStr(cSelFile.FileVersion.ProductMajorPart).Length > 0 Then _
                    s &= "\tab ProductMajorPart :\tab " & cSelFile.FileVersion.ProductMajorPart & "\par"
                If CStr(cSelFile.FileVersion.ProductMinorPart).Length > 0 Then _
                    s &= "\tab Comments :\tab\tab " & cSelFile.FileVersion.ProductMinorPart & "\par"
                If cSelFile.FileVersion.ProductName IsNot Nothing AndAlso cSelFile.FileVersion.ProductName.Length > 0 Then _
                    s &= "\tab ProductName :\tab\tab " & cSelFile.FileVersion.ProductName & "\par"
                If CStr(cSelFile.FileVersion.ProductPrivatePart).Length > 0 Then _
                    s &= "\tab ProductPrivatePart :\tab " & cSelFile.FileVersion.ProductPrivatePart & "\par"
                If cSelFile.FileVersion.ProductVersion IsNot Nothing AndAlso cSelFile.FileVersion.ProductVersion.Length > 0 Then _
                    s &= "\tab ProductVersion :\tab\tab " & cSelFile.FileVersion.ProductVersion & "\par"
                If cSelFile.FileVersion.SpecialBuild IsNot Nothing AndAlso cSelFile.FileVersion.SpecialBuild.Length > 0 Then _
                    s &= "\tab SpecialBuild :\tab\tab " & cSelFile.FileVersion.SpecialBuild & "\par"
            End If

            ' Icons
            Try
                pctFileBig.Image = GetIcon(file, False).ToBitmap
                pctFileSmall.Image = GetIcon(file, True).ToBitmap
            Catch ex As Exception
                pctFileSmall.Image = Me.imgProcess.Images("noicon")
                pctFileBig.Image = Me.imgMain.Images("noicon32")
            End Try


            s &= "\f1\fs20\par"
            s &= "}"
        Else
            s &= "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}{\f1\fswiss\fcharset0 Arial;}}"
            s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b " & "File does not exist !\b0\par"
            s &= "\f1\fs20\par"
            s &= "}"
        End If


        rtb3.Rtf = s

    End Sub

    ' Refresh service list
    Public Sub refreshServiceList()

        ' Update list
        Me.lvServices.ShowAll = True
        Me.lvServices.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            If Me.Ribbon.ActiveTab.Text = "Services" Then
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvServices.Items.Count) & " services running"
            End If
        End If

    End Sub

    ' Refresh job list
    Public Sub refreshJobList()

        ' Update list
        Me.lvJob.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            If Me.Ribbon.ActiveTab.Text = "Jobs" Then
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvJob.Items.Count) & " jobs running"
            End If
        End If

    End Sub

    ' Refresh process list in listview
    Public Sub refreshProcessList()

        ' Update list
        Me.lvProcess.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Processes" Or ss = "Monitor" Or ss = "Misc" Or ss = "Help" Or ss = "File" Then
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
            End If
        End If

    End Sub

    Private Sub timerProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcess.Tick
        Call refreshProcessList()
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Static bFirst As Boolean = True
        If bFirst Then
            bFirst = False
            Native.Functions.Misc.SetTheme(Me.lvProcess.Handle)
            Native.Functions.Misc.SetTheme(Me.lvMonitorReport.Handle)
            Native.Functions.Misc.SetTheme(Me.lvNetwork.Handle)
            Native.Functions.Misc.SetTheme(Me.lvTask.Handle)
            Native.Functions.Misc.SetTheme(Me.lvHandles.Handle)
            Native.Functions.Misc.SetTheme(Me.lvWindows.Handle)
            Native.Functions.Misc.SetTheme(Me.lvSearchResults.Handle)
            Native.Functions.Misc.SetTheme(Me.lvModules.Handle)
            Native.Functions.Misc.SetTheme(Me.lvThreads.Handle)
            Native.Functions.Misc.SetTheme(Me.lvServices.Handle)
            Native.Functions.Misc.SetTheme(Me.tv.Handle)
            Native.Functions.Misc.SetTheme(Me.tv2.Handle)
            Native.Functions.Misc.SetTheme(Me.tvMonitor.Handle)
            Native.Functions.Misc.SetTheme(Me.lvFileString.Handle)
            Native.Functions.Misc.SetTheme(Me.lvJob.Handle)
        End If
        Call frmMain_Resize(Nothing, Nothing)
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If My.Settings.HideWhenClosed Then
            Me.Hide()
            e.Cancel = True
            Exit Sub
        End If
        ' This avoid to call ExitYAPM recursively when exiting
        Me.CallExitWhenExitYAPM = False
        If Me.CallExitWhenExitYAPM Then
            Call ExitYAPM()
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Program.Parameters.ModeHidden Then
            Me.Left = Pref.LEFT_POSITION_HIDDEN
            Me.ShowInTaskbar = False
        End If
        If My.Settings.StartHidden Then
            ' If Ribbon is displayed, there is a problem : Form is automatically shown...
            ' So we have to remoe it from taskbar and move it to the left to hide it
            ' If Ribbon is not displayed, we can't hide from taskbar else icons won't
            ' be displayed in MainMenu...
            If My.Settings.UseRibbonStyle Then
                Me.Left = Pref.LEFT_POSITION_HIDDEN
                Me.ShowInTaskbar = False
            End If
            Me.Hide()
        End If

        ' For now, SBA is removed from menu...
        Me.Ribbon.OrbDropDown.MenuItems.Remove(Me.orbMenuSBA)

        ' Disable 'start as admin' if we are not on Vista or above
        If cEnvironment.IsWindowsVistaOrAbove = False _
                OrElse cEnvironment.GetElevationType <> Native.Api.Enums.ElevationType.Limited Then
            Me.Ribbon.OrbDropDown.MenuItems.Remove(Me.orbStartElevated)
            Me.MenuItemSYSTEMFILE.MenuItems.Remove(Me.MenuItemRunAsAdmin)
        Else
            ' If we display standard menus, we add the icon
            If My.Settings.UseRibbonStyle = False Then
                Me.VistaMenu.SetImage(Me.MenuItemRunAsAdmin, cEnvironment.GetUacShieldImage)
            End If
        End If

        Me.timerProcess.Enabled = False
        Dim t As Integer = Native.Api.Win32.GetElapsedTime

        Me.containerSystemMenu.Panel1Collapsed = True
        Me.Tray.Visible = True
        Me.Tray.ContextMenu = Me.mnuMain
        Me.rtb3.AllowDrop = True

        ' Set tray icon counters
        TrayIcon.SetCounter(1, Color.Red, Color.FromArgb(120, 0, 0))
        TrayIcon.SetCounter(2, Color.LightGreen, Color.FromArgb(0, 120, 0))

        PROCESSOR_COUNT = Program.SystemInfo.ProcessorCount

        creg = New cRegMonitor(Native.Api.NativeEnums.KEY_TYPE.HKEY_LOCAL_MACHINE, "SYSTEM\CurrentControlSet\Services", _
                Native.Api.NativeEnums.KEY_MONITORING_TYPE.REG_NOTIFY_CHANGE_NAME)

        With Me.graphMonitor
            .ColorMemory1 = Color.Yellow
            .ColorMemory2 = Color.Red
            .ColorMemory3 = Color.Orange
        End With

        ' Create tooltips
        SetToolTip(Me.lblResCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.lblResCount2, "Number of results. Click on the number to view results")
        SetToolTip(Me.lblTaskCountResult, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearchTask, "Enter text here to search a task")
        SetToolTip(Me.txtSearch, "Enter text here to search a process")
        SetToolTip(Me.lblModulesCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearchModule, "Enter text here to search a module")
        SetToolTip(Me.lblThreadResults, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearchThread, "Enter text here to search a thread")
        SetToolTip(Me.lblHandlesCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearchHandle, "Enter text here to search a handle")
        SetToolTip(Me.lblWindowsCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearchWindow, "Enter text here to search a window")
        SetToolTip(Me.chkAllWindows, "Display all windows (including windows without caption)")
        SetToolTip(Me.txtSearchResults, "Enter text here to search into the results")
        SetToolTip(Me.lblResultsCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.tvMonitor, "Monitoring items")
        SetToolTip(Me.chkMonitorLeftAuto, "Setting to display graph. See help for details")
        SetToolTip(Me.chkMonitorRightAuto, "Setting to display graph. See help for details")
        SetToolTip(Me.dtMonitorL, "Setting to display graph. See help for details")
        SetToolTip(Me.dtMonitorR, "Setting to display graph. See help for details")
        SetToolTip(Me.txtMonitorNumber, "Setting to display graph. See help for details")
        SetToolTip(Me.cmdFileClipboard, "Copy file informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style)")
        SetToolTip(Me.DTcreation, "Date of creation")
        SetToolTip(Me.DTlastAccess, "Date of last access")
        SetToolTip(Me.DTlastModification, "Date of last modification")
        SetToolTip(Me.cmdSetFileDates, "Set these dates")
        SetToolTip(Me.chkFileArchive, "File is archive")
        SetToolTip(Me.chkFileCompressed, "File is compressed")
        SetToolTip(Me.chkFileContentNotIndexed, "File is indexed")
        SetToolTip(Me.chkFileEncrypted, "File is encrypted")
        SetToolTip(Me.chkFileHidden, "File is hidden")
        SetToolTip(Me.chkFileNormal, "File is normal")
        SetToolTip(Me.chkFileReadOnly, "File is read only")
        SetToolTip(Me.chkFileSystem, "File is system")
        SetToolTip(Me.txtServiceSearch, "Enter text here to search a service")
        SetToolTip(Me.cmdCopyServiceToCp, "Copy services informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style)")
        SetToolTip(Me.lblServicePath, "Path of the main executable of the service")
        SetToolTip(Me.tv, "Selected service depends on these services")
        SetToolTip(Me.tv2, "This services depend on selected service")
        SetToolTip(Me.chkSearchProcess, "Include processes in search")
        SetToolTip(Me.chkSearchServices, "Include services in search")
        SetToolTip(Me.chkSearchWindows, "Include windows in search")
        SetToolTip(Me.chkSearchCase, "Search is case sensitive or not")
        SetToolTip(Me.chkSearchEnvVar, "Include environement variables in search")
        SetToolTip(Me.chkSearchHandles, "Include handles in search")
        SetToolTip(Me.chkSearchModules, "Include modules in search")
        SetToolTip(Me.lvFileString, "List of strings in file. Middle click to copy to clipboard.")

        ' Init columns
        Pref.LoadListViewColumns(Me.lvProcess, "COLmain_process")
        Pref.LoadListViewColumns(Me.lvTask, "COLmain_task")
        Pref.LoadListViewColumns(Me.lvModules, "COLmain_module")
        Pref.LoadListViewColumns(Me.lvThreads, "COLmain_thread")
        Pref.LoadListViewColumns(Me.lvHandles, "COLmain_handle")
        Pref.LoadListViewColumns(Me.lvWindows, "COLmain_window")
        Pref.LoadListViewColumns(Me.lvServices, "COLmain_service")
        Pref.LoadListViewColumns(Me.lvNetwork, "COLmain_network")


        ' Connect to the local machine
        Program.Connection.ConnectionType = cConnection.TypeOfConnection.LocalConnection
        Call ConnectToMachine()

        Me.timerMonitoring.Enabled = True
        Me.timerProcess.Enabled = True
        Me.timerTask.Enabled = True
        Me.timerNetwork.Enabled = True
        Me.timerStateBasedActions.Enabled = True
        Me.timerTrayIcon.Enabled = True
        Me.timerServices.Enabled = True
        Me.timerStatus.Enabled = True
        Me.timerJobs.Enabled = True

        If Me.lvProcess.Items.Count > 1 Then
            Call Me.lvProcess.Focus()
            Me.lvProcess.Items(Me.lvProcess.Items.Count - 1).Selected = True
            Me.lvProcess.Items(Me.lvProcess.Items.Count - 1).EnsureVisible()
        End If

        t = Native.Api.Win32.GetElapsedTime - t

        Trace.WriteLine("Loaded in " & CStr(t) & " ms.")
        Call refreshTaskList()


        ' Add some submenus (Copy to clipboard)
        For Each ss As String In handleInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyHandle.MenuItems.Add(ss, AddressOf MenuItemCopyHandle_Click)
        Next
        For Each ss As String In moduleInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyModule.MenuItems.Add(ss, AddressOf MenuItemCopyModule_Click)
        Next
        For Each ss As String In jobInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyJob.MenuItems.Add(ss, AddressOf MenuItemCopyJob_Click)
        Next
        For Each ss As String In networkInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyNetwork.MenuItems.Add(ss, AddressOf MenuItemCopyNetwork_Click)
        Next
        For Each ss As String In searchInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopySearch.MenuItems.Add(ss, AddressOf MenuItemCopySearch_Click)
        Next
        For Each ss As String In serviceInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyService.MenuItems.Add(ss, AddressOf MenuItemCopyService_Click)
        Next
        For Each ss As String In threadInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyThread.MenuItems.Add(ss, AddressOf MenuItemCopyThread_Click)
        Next
        For Each ss As String In windowInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyWindow.MenuItems.Add(ss, AddressOf MenuItemCopyWindow_Click)
        Next
        For Each ss As String In taskInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyTask.MenuItems.Add(ss, AddressOf MenuItemCopyTask_Click)
        Next
        For Each ss As String In processInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyProcess.MenuItems.Add(ss, AddressOf MenuItemCopyProcess_Click)
        Next

#If RELEASE_MODE = 0 Then
        Dim frm As New frmServer
        frm.TopMost = _frmMain.TopMost
        frm.Show()
#End If

    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Try
            ' If we do not use Ribbon mode, we hide Ribbon. Else, we add tabs
            If My.Settings.UseRibbonStyle Then
                ' The only way to hide tabs is to change region of tabcontrol
                ' But we also have to set Dock to None to avoid displaying a blank area
                ' We change manually height/width, that's why this piece of code is located
                ' in Resize()
                With _tab
                    _tab.Dock = DockStyle.None
                    _tab.Top = -24
                    _tab.Left = -2
                    _tab.Width = Me.Width - 12
                    _tab.Height = Me.Height - Me.Ribbon.Height - Me.StatusBar.Height - 15
                    _tab.Region = New Region(New RectangleF(_tab.Left, _tab.SelectedTab.Top, _tab.SelectedTab.Width + 5, _tab.SelectedTab.Height))
                    _tab.Refresh()
                End With
            Else
                Me._main.Panel1Collapsed = True
            End If

            If My.Settings.HideWhenMinimized AndAlso Me.WindowState = FormWindowState.Minimized Then
                Me.Hide()
            End If

            For Each t As TabPage In _tab.TabPages
                t.Hide()
            Next

            'Dim i As Integer = CInt((Me.Height - 250) / 2)
            'Dim MepanelInfosHeight As Integer = CInt(IIf(i < 340, i, 340))
            'Dim MepanelInfonWidth As Integer = Me.panelMain.Width

            ' File resizement
            Me.txtFile.Width = Me.Width - 260

            _tab.SelectedTab.Show()

        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub timerServices_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerServices.Tick
        refreshServiceList()
    End Sub

    Private Sub Tray_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Tray.MouseDoubleClick
        ' If ribbon is used, then the main form is to the left of the screen and
        ' not shown in taskbar
        If My.Settings.UseRibbonStyle Then
            If Me.Left = Pref.LEFT_POSITION_HIDDEN Then
                Me.CenterToScreen()
            End If
            Me.ShowInTaskbar = True
        End If
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.Show()
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        'Static first As Boolean = True
        'If first Then
        '    first = False
        '    If My.Settings.StartHidden Then
        '        Me.Hide()
        '        Me.WindowState = FormWindowState.Minimized
        '    Else
        '        Me.Show()
        '        Me.WindowState = FormWindowState.Normal
        '    End If
        'End If
        Select Case My.Settings.SelectedTab
            Case "Tasks"
                Me.Ribbon.ActiveTab = Me.TaskTab
            Case "Processes"
                Me.Ribbon.ActiveTab = Me.ProcessTab
            Case "Modules"
                Me.Ribbon.ActiveTab = Me.ModulesTab
            Case "Threads"
                Me.Ribbon.ActiveTab = Me.ThreadTab
            Case "Handles"
                Me.Ribbon.ActiveTab = Me.HandlesTab
            Case "Windows"
                Me.Ribbon.ActiveTab = Me.WindowTab
            Case "Monitor"
                Me.Ribbon.ActiveTab = Me.MonitorTab
            Case "Services"
                Me.Ribbon.ActiveTab = Me.ServiceTab
            Case "Network"
                Me.Ribbon.ActiveTab = Me.NetworkTab
            Case "File"
                Me.Ribbon.ActiveTab = Me.FileTab
            Case "Search"
                Me.Ribbon.ActiveTab = Me.SearchTab
            Case "Help"
                Me.Ribbon.ActiveTab = Me.HelpTab
        End Select
        Call Ribbon_MouseMove(Nothing, Nothing)
        Call Me.frmMain_Resize(Nothing, Nothing)
    End Sub

    Private Sub butKill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butKillProcess.Click
        ' Kill selected processes
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.Kill()
        Next
    End Sub

    Private Sub butAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAbout.Click
        Dim frm As New frmAboutG
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub butProcessRerfresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessRerfresh.Click
        Me.refreshProcessList()
    End Sub

    Private Sub butServiceRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceRefresh.Click
        Me.refreshServiceList()
    End Sub

    Private Sub butDonate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDonate.Click
        MsgBox("You will be redirected on my sourceforge.net donation page.", MsgBoxStyle.Information, "Donation procedure")
        cFile.ShellOpenFile("https://sourceforge.net/donate/index.php?user_id=1590933#donate", Me.Handle)
    End Sub

    Private Sub butWebite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWebite.Click
        cFile.ShellOpenFile("http://yaprocmon.sourceforge.net/", Me.Handle)
    End Sub

    Private Sub butProjectPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProjectPage.Click
        cFile.ShellOpenFile("http://sourceforge.net/projects/yaprocmon", Me.Handle)
    End Sub

    Private Sub butServiceFileProp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceFileProp.Click
        Call Me.MenuItemServFileProp_Click(Nothing, Nothing)
    End Sub

    Private Sub butServiceOpenDir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceOpenDir.Click
        Call Me.MenuItemServOpenDir_Click(Nothing, Nothing)
    End Sub

    Private Sub butStopProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStopProcess.Click
        ' Stop selected processes
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to suspend these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SuspendProcess()
        Next
    End Sub

    Private Sub butProcessAffinity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessAffinity.Click
        Call Me.MenuItemProcAff_Click(Nothing, Nothing)
    End Sub

    Private Sub butResumeProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butResumeProcess.Click
        ' Resume selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.ResumeProcess()
        Next
    End Sub

    Private Sub butIdle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butIdle.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.Idle)
        Next
    End Sub

    Private Sub butHigh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHigh.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub butNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNormal.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub butRealTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRealTime.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.RealTime)
        Next
    End Sub

    Private Sub butBelowNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butBelowNormal.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub butAboveNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAboveNormal.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.AboveNormal)
        Next
    End Sub

    Private Sub butStopService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStopService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StopService()
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butStartService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStartService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StartService()
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butPauseService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPauseService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.PauseService()
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butAutomaticStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAutomaticStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.AutoStart)
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butDisabledStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDisabledStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.StartDisabled)
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butOnDemandStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOnDemandStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.DemandStart)
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butResumeService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butResumeService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.ResumeService()
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub Ribbon_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Ribbon.MouseClick
        If Me.lvServices.Items.Count = 0 Then
            If Me.Ribbon.ActiveTab.Text = "Services" Then
                ' First display of service tab
                Call refreshServiceList()
            End If
        ElseIf Me.lvProcess.Items.Count = 0 Then
            If Me.Ribbon.ActiveTab.Text = "Processes" Then
                ' First display of process tab
                Call refreshProcessList()
            End If
        End If
    End Sub

    Public Sub Ribbon_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Ribbon.MouseMove
        ' Static currentText As String = vbNullString
        Static bHelpShown As Boolean = False

        'If Not (Ribbon.ActiveTab.Text = currentText) Then
        'currentText = Ribbon.ActiveTab.Text

        Select Case Ribbon.ActiveTab.Text
            Case "Jobs"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvJob.Items.Count) & " jobs running"
                _tab.SelectedTab = Me.pageJobs
            Case "Services"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvServices.Items.Count) & " services running"
                _tab.SelectedTab = Me.pageServices
            Case "Processes"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageProcesses
            Case "Help"

                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                If Not (bHelpShown) Then
                    bHelpShown = True
                    ' Load help file
                    Dim path As String = HELP_PATH
                    'If IO.File.Exists(path) = False Then
                    '    WBHelp.Document.Write("<body link=blue vlink=purple><span>Help file cannot be found. <p></span><span>Please download help file at <a href=" & Chr(34) & "http://sourceforge.net/projects/yaprocmon/" & Chr(34) & ">http://sourceforge.net/projects/yaprocmon</a> and save it in the Help directory.</span></body>")
                    'Else
                    WBHelp.Navigate(path)
                    'End If
                End If

                _tab.SelectedTab = Me.pageHelp
            Case "File"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageFile
            Case "Search"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvSearchResults.Items.Count) & " search results"
                _tab.SelectedTab = Me.pageSearch
            Case "Handles"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvHandles.Items.Count) & " handles"
                _tab.SelectedTab = Me.pageHandles
            Case "Monitor"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageMonitor
            Case "Threads"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvThreads.Items.Count) & " threads"
                _tab.SelectedTab = Me.pageThreads
            Case "Windows"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvWindows.Items.Count) & " windows"
                _tab.SelectedTab = Me.pageWindows
            Case "Modules"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"
                _tab.SelectedTab = Me.pageModules
            Case "Tasks"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvTask.Items.Count) & " tasks running"
                _tab.SelectedTab = Me.pageTasks
            Case "Network"
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvNetwork.Items.Count) & " connections opened"
                _tab.SelectedTab = Me.pageNetwork
        End Select
        'End If

    End Sub

    Private Sub butDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDownload.Click
        cFile.ShellOpenFile("http://sourceforge.net/project/showfiles.php?group_id=244697", Me.Handle)
    End Sub

    Private Sub frmMain_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged
        'Me.timerServices.Enabled = Me.Visible
        'Me.timerProcess.Enabled = Me.Visible
    End Sub

    Private Sub butProcessGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessGoogle.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Application.DoEvents()
            Call SearchInternet(cp.Infos.Name, Me.Handle)
        Next
    End Sub

    Private Sub butServiceGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceGoogle.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
    End Sub

    Private Sub butServiceFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceFileDetails.Click
        If Me.lvServices.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvServices.GetSelectedItem.GetInformation("ImagePath")
            If IO.File.Exists(s) = False Then
                s = cFile.IntelligentPathRetrieving2(s)
            End If
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub butUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butUpdate.Click
        Dim frm As New frmPreferences
        With frm
            .TabControl.SelectedTab = .TabPage2
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub butSearchGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSearchGo.Click
        Call goSearch(Me.txtSearchString.TextBoxText)
    End Sub

    Private Sub txtSearchString_TextBoxTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchString.TextBoxTextChanged
        Dim b As Boolean = (txtSearchString.TextBoxText IsNot Nothing)
        If b Then
            b = b And txtSearchString.TextBoxText.Length > 0
        End If
        Me.butSearchGo.Enabled = b
    End Sub

    Private Sub butSearchSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSearchSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "search"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub ShowHandles(Optional ByVal showTab As Boolean = True)

        Me.lvHandles.ShowUnnamed = Me.MenuItemShowUnnamedHandles.Checked
        Me.lvHandles.ProcessId = Me.handlesToRefresh
        Me.lvHandles.UpdateTheItems()

        If showTab Then
            Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvHandles.Items.Count) & " handles"
            Application.DoEvents()
            Me.Ribbon.ActiveTab = Me.HandlesTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub butHandleRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandleRefresh.Click
        Call ShowHandles()
    End Sub

    Private Sub butHandleClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandleClose.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to close these handles ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each ch As cHandle In Me.lvHandles.GetSelectedItems
            ch.CloseHandle()
        Next
    End Sub

    Private Sub butFileProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileProperties.Click
        Call cFile.ShowFileProperty(Me.txtFile.Text, Me.Handle)
    End Sub

    Private Sub butFileShowFolderProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileShowFolderProperties.Click
        Call cFile.ShowFileProperty(IO.Directory.GetParent(Me.txtFile.Text).FullName, Me.Handle)
    End Sub

    Private Sub butFileOpenDir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileOpenDir.Click
        Call cFile.OpenDirectory(Me.txtFile.Text)
    End Sub

    Private Sub butOpenFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOpenFile.Click
        ' Open a file
        openDial.Filter = "All files (*.*)|*.*"
        openDial.Title = "Open a file to retrieve details"
        If openDial.ShowDialog = Windows.Forms.DialogResult.OK Then
            If IO.File.Exists(openDial.FileName) Then
                DisplayDetailsFile(openDial.FileName)
            End If
        End If
    End Sub

    Private Sub butFileRelease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRelease.Click
        Dim frm As New frmFileRelease
        With frm
            .file = Me.txtFile.Text
            .TopMost = _frmMain.TopMost
            Call .ShowDialog()
        End With
    End Sub

    Private Sub butFileGoogleSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileGoogleSearch.Click
        Application.DoEvents()
        Call SearchInternet(cFile.GetFileName(Me.txtFile.Text), Me.Handle)
    End Sub

    Private Sub butFileEncrypt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileEncrypt.Click
        Try
            cSelFile.Encrypt()
            MsgBox("Done.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Encryption ok")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Encryption failed")
        End Try
    End Sub

    Private Sub butFileDecrypt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileDecrypt.Click
        Try
            cSelFile.Decrypt()
            MsgBox("Done.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Decryption ok")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Decryption failed")
        End Try
    End Sub

    Private Sub butFileRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRefresh.Click
        Call DisplayDetailsFile(Me.txtFile.Text)
    End Sub

    Private Sub butMoveFileToTrash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMoveFileToTrash.Click
        cSelFile.MoveToTrash()
    End Sub

    Private Sub butFileSeeStrings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileSeeStrings.Click
        Call DisplayFileStrings(Me.lvFileString, Me.txtFile.Text)
    End Sub

    Private Function RemoveAttribute(ByVal attributesToRemove As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.Attributes()
        Return attributes And Not (attributesToRemove)
    End Function
    Private Function AddAttribute(ByVal attributesToAdd As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.Attributes
        Return attributes Or attributesToAdd
    End Function

    Private Sub butFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileOpen.Click
        Call cFile.ShellOpenFile(Me.txtFile.Text, Me.Handle)
    End Sub

    ' Display file strings
    Public Sub DisplayFileStrings(ByVal lst As ListView, ByVal file As String)
        Dim s As String = vbNullString
        Dim sCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim bTaille As Integer
        Dim lLen As Integer

        If IO.File.Exists(file) Then

            lst.Items.Clear()

            ' Retrieve entire file in memory
            ' Warn user if file is up to 2MB
            Try

                If FileLen(file) > 2000000 Then
                    If MsgBox("File size is greater than 2MB. It is not recommended to open a large file, do you want to continue ?", _
                        MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Large file") = MsgBoxResult.No Then
                        lvFileString.Items.Add("Click on 'Others->Show file strings' to retrieve file strings")
                        Exit Sub
                    End If
                End If

                s = IO.File.ReadAllText(file)

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            End Try


            ' Desired minimum size for a string
            bTaille = SIZE_FOR_STRING

            ' A char is considered as part of a string if its value is between 32 and 122
            lLen = Len(s)

            ' Lock listbox
            lst.BeginUpdate()

            ' Ok, parse file
            Do Until x > lLen

                If Char.IsLetterOrDigit(s.Chars(x - 1)) Then
                    ' Valid char
                    sCtemp &= s.Chars(x - 1)
                Else
                    'sCtemp = LTrim$(sCtemp)
                    'sCtemp = RTrim$(sCtemp)
                    If Len(sCtemp) > bTaille Then
                        lst.Items.Add(sCtemp)
                    End If
                    sCtemp = vbNullString
                End If

                x += 1
            Loop

            ' Last item
            If Len(sCtemp) > bTaille Then
                lst.Items.Add(sCtemp)
            End If

            ' Unlock listbox
            lst.EndUpdate()
        End If

    End Sub

    Private Sub butMonitoringAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitoringAdd.Click
        Dim frm As New frmAddProcessMonitor(Program.Connection)
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    ' Add a monitoring item
    Public Sub AddMonitoringItem(ByVal it As cMonitor)

        ' Check if a node with same category and instance exists
        Dim nExistingItem As TreeNode = Nothing
        Dim n As TreeNode
        For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
            If CStr(IIf(Len(it.InstanceName) > 0, it.InstanceName & " - ", vbNullString)) & _
                        it.CategoryName = n.Text Then
                nExistingItem = n
                Exit For
            End If
        Next

        If nExistingItem Is Nothing Then
            ' New sub item
            Dim n1 As New TreeNode
            With n1
                .Text = CStr(IIf(Len(it.InstanceName) > 0, it.InstanceName & " - ", _
                   vbNullString)) & it.CategoryName
                .ImageIndex = 0
                .SelectedImageIndex = 0
            End With

            Dim ncpu As New TreeNode
            With ncpu
                .Text = it.CounterName
                .ImageKey = "sub"
                .SelectedImageKey = "sub"
                .Tag = it
            End With
            n1.Nodes.Add(ncpu)

            Me.tvMonitor.Nodes.Item(0).Nodes.Add(n1)
        Else
            ' Use existing sub item
            Dim ncpu As New TreeNode
            With ncpu
                .Text = it.CounterName
                .ImageKey = "sub"
                .SelectedImageKey = "sub"
                .Tag = it
            End With

            nExistingItem.Nodes.Add(ncpu)
        End If

        Call updateMonitoringLog()
    End Sub

    Private Sub butMonitorStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorStart.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                If tvMonitor.SelectedNode.Parent.Parent IsNot Nothing Then
                    ' Subsub item
                    Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                    it.StartMonitoring()
                    Call tvMonitor_AfterSelect(Nothing, Nothing)
                Else
                    ' Sub item
                    Dim n As TreeNode
                    For Each n In tvMonitor.SelectedNode.Nodes
                        Dim it As cMonitor = CType(n.Tag, cMonitor)
                        it.StartMonitoring()
                    Next
                End If
            Else
                ' All items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    Dim n2 As TreeNode
                    For Each n2 In n.Nodes
                        Dim it As cMonitor = CType(n2.Tag, cMonitor)
                        it.StartMonitoring()
                    Next
                Next
            End If
        End If
        Call updateMonitoringLog()
    End Sub

    Private Sub butMonitorStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorStop.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                If tvMonitor.SelectedNode.Parent.Parent IsNot Nothing Then
                    ' Subsub item
                    Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                    it.StopMonitoring()
                    Call tvMonitor_AfterSelect(Nothing, Nothing)
                Else
                    ' Sub item
                    Dim n As TreeNode
                    For Each n In tvMonitor.SelectedNode.Nodes
                        Dim it As cMonitor = CType(n.Tag, cMonitor)
                        it.StopMonitoring()
                    Next
                End If
            Else
                ' All items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    Dim n2 As TreeNode
                    For Each n2 In n.Nodes
                        Dim it As cMonitor = CType(n2.Tag, cMonitor)
                        it.StopMonitoring()
                    Next
                Next
            End If
        End If
        Call updateMonitoringLog()
    End Sub

    ' Powerful recursive method to unload all cMonitor items in subnodes
    Private Sub RemoveSubNode(ByRef nod As TreeNode, ByRef n As TreeNodeCollection)
        Dim subn As TreeNode
        For Each subn In n
            RemoveSubNode(subn, subn.Nodes)
        Next
        ' It's a monitor sub item
        If nod.ImageKey = "sub" Then
            Dim it As cMonitor = CType(nod.Tag, cMonitor)
            If it IsNot Nothing Then it.Dispose()
            it = Nothing
        End If
    End Sub

    Private Sub butMonitoringRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitoringRemove.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            RemoveSubNode(tvMonitor.SelectedNode, tvMonitor.SelectedNode.Nodes)
            Dim nn As TreeNodeCollection = tvMonitor.SelectedNode.Nodes
            Dim cnn As Integer = nn.Count
            For i As Integer = cnn - 1 To 0 Step -1
                nn.Item(i).Remove()
            Next
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                tvMonitor.SelectedNode.Remove()
            End If

            ' Remove all single items (no sub)
            nn = Me.tvMonitor.Nodes.Item(0).Nodes
            cnn = nn.Count
            For i As Integer = cnn - 1 To 0 Step -1
                If nn.Item(i).Nodes.Count = 0 Then
                    nn.Item(i).Remove()
                End If
            Next
        End If
        Call updateMonitoringLog()
    End Sub

    ' Display stats in graph
    Private Sub ShowMonitorStats(ByVal it As cMonitor, ByVal key1 As String, ByVal key2 As String, _
        ByVal key3 As String)

        Me.timerMonitoring.Interval = it.Interval

        If it.Enabled = False Then
            Me.graphMonitor.CreateGraphics.Clear(Color.Black)
            Me.graphMonitor.CreateGraphics.DrawString("You have to start monitoring.", Me.Font, Brushes.White, 0, 0)
            Exit Sub
        End If

        ' Get values from monitor item
        Dim v() As Graph.ValueItem
        Dim cCol As New Collection
        cCol = it.GetMonitorItems()

        ' Limit DT pickers
        Me.dtMonitorL.MaxDate = Date.Now
        Me.dtMonitorL.MinDate = it.MonitorCreationDate
        Me.dtMonitorR.MaxDate = Me.dtMonitorL.MaxDate
        Me.dtMonitorR.MinDate = Me.dtMonitorL.MinDate

        If cCol.Count > 0 Then

            ReDim v(cCol.Count)
            Dim c As cMonitor.MonitorStructure
            Dim i As Integer = 0

            For Each c In cCol
                If i < v.Length Then
                    v(i).y = CLng(c.value)
                    v(i).x = c.time
                    i += 1
                End If
            Next

            ReDim Preserve v(cCol.Count - 1)

            With Me.graphMonitor

                ' Set max and min (depends and dates chosen)
                If Me.chkMonitorLeftAuto.Checked And Me.chkMonitorRightAuto.Checked Then
                    ' Then no one fixed
                    .ViewMin = CInt(Math.Max(0, i - CInt(Val(Me.txtMonitorNumber.Text))))
                    .ViewMax = i - 1
                ElseIf Me.chkMonitorRightAuto.Checked Then
                    ' Then left fixed
                    .ViewMin = findViewIntegerFromDate(Me.dtMonitorL.Value, v, it)
                    .ViewMax = findViewMaxFromMin(.ViewMin, v)
                ElseIf Me.chkMonitorLeftAuto.Checked Then
                    ' Then right fixed
                    .ViewMax = findViewIntegerFromDate(Me.dtMonitorR.Value, v, it)
                    .ViewMin = findViewLast(.ViewMax)
                Else
                    ' Then both fixed
                    .ViewMax = findViewIntegerFromDate(Me.dtMonitorR.Value, v, it)
                    .ViewMin = findViewIntegerFromDate(Me.dtMonitorL.Value, v, it)
                End If

                .SetValues(v)
                .dDate = it.MonitorCreationDate
                .EnableGraph = True
                Call .Refresh()
            End With
        End If
    End Sub

    Private Sub timerMonitoring_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerMonitoring.Tick
        Call tvMonitor_AfterSelect(Nothing, Nothing)
    End Sub

    ' Return an integer that corresponds to a time in a monitor from a date
    Private Function findViewIntegerFromDate(ByVal d As Date, ByVal v() As Graph.ValueItem, _
        ByVal monitor As cMonitor) As Integer

        Dim it As Graph.ValueItem
        Dim l As Long = d.Ticks
        Dim start As Long = monitor.MonitorCreationDate.Ticks
        Dim o As Integer = 0
        For Each it In v
            If (start + 10000 * it.x) >= l Then
                Return o
            End If
            o += 1
        Next

        Return CInt(v.Length - 1)
    End Function

    ' Return an integer that corresponds to min + txtMAX.value iterations
    Private Function findViewMaxFromMin(ByVal min As Integer, ByVal v() As Graph.ValueItem) As Integer
        Return Math.Min(v.Length - 1, min + CInt(Val(Me.txtMonitorNumber.Text)))
    End Function

    ' Return element of array with a distance of txtMAX.value to the end of the array
    Private Function findViewLast(ByVal max As Integer) As Integer
        Dim lMax As Integer = CInt(Val(Me.txtMonitorNumber.Text))
        Return Math.Max(0, max - lMax)
    End Function

    ' Show threads of selected processes (threadsToRefresh)
    Private Sub ShowThreads(Optional ByVal showTab As Boolean = True)

        Me.lvThreads.ProcessId = Me.threadsToRefresh
        Me.lvThreads.UpdateTheItems()

        If showTab Then _
            Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvThreads.Items.Count) & " threads"

    End Sub

    Private Sub butThreadRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadRefresh.Click
        If threadsToRefresh IsNot Nothing Then Call ShowThreads()
    End Sub

    Private Sub butThreadKill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadKill.Click
        Call Me.MenuItemThTerm_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadResume_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadResume.Click
        Call Me.MenuItemThResu_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadStop.Click
        Call Me.MenuItemThSelProc_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPabove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPabove.Click
        Call Me.MenuItemThANorm_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPbelow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPbelow.Click
        Call Me.MenuItemThBNormal_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPcritical_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPcritical.Click
        Call Me.MenuItemThTimeCr_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPhighest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPhighest.Click
        Call Me.MenuItemThHighest_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPidle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPidle.Click
        Call Me.MenuItemThIdle_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPlowest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPlowest.Click
        Call Me.MenuItemThLowest_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPnormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPnormal.Click
        Call Me.MenuItemThNorm_Click(Nothing, Nothing)
    End Sub

    ' Show windows of selected processes (windowsToRefresh)
    Public Sub ShowWindows(Optional ByVal showTab As Boolean = True)

        Me.lvWindows.ProcessId = Me.windowsToRefresh
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowUnNamed = Me.chkAllWindows.Checked
        Me.lvWindows.UpdateTheItems()

        If showTab Then _
            Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvWindows.Items.Count) & " windows"

    End Sub

    Private Sub butWindowRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowRefresh.Click
        If windowsToRefresh IsNot Nothing Then Call ShowWindows()
    End Sub

    Private Sub butProcessThreads_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessThreads.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then

            Dim x As Integer = 0
            ReDim threadsToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                threadsToRefresh(x) = it.Infos.Pid
                x += 1
            Next

            Call ShowThreads()
            Me.Ribbon.ActiveTab = Me.ThreadTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub butProcessWindows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessWindows.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then

            Dim x As Integer = 0
            ReDim windowsToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                windowsToRefresh(x) = it.Infos.Pid
                x += 1
            Next

            Call ShowWindows()
            Me.Ribbon.ActiveTab = Me.WindowTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub butShowProcHandles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowProcHandles.Click
        Dim x As Integer = 0
        ReDim handlesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            handlesToRefresh(x) = it.Infos.Pid
            x += 1
        Next
        Call ShowHandles()
    End Sub

    Private Sub butProcessShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessShow.Click
        'Call butProcessThreads_Click(Nothing, Nothing)
        'Call butProcessWindows_Click(Nothing, Nothing)
        'Call butShowProcHandles_Click(Nothing, Nothing)
        'Me.Ribbon.ActiveTab = Me.ProcessTab
        'Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub butWindowBringToFront_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowBringToFront.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.BringToFront(True)
        Next
    End Sub

    Private Sub butWindowCaption_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowCaption.Click
        Dim z As String = ""

        If Me.lvWindows.SelectedItems.Count > 0 Then
            z = Me.lvWindows.GetSelectedItem.Caption
        End If

        Dim sres As String = CInputBox("Set a new caption.", "New caption", z)

        If sres Is Nothing OrElse sres.Equals(String.Empty) Then Exit Sub

        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Caption = sres
        Next
    End Sub

    Private Sub butWindowClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowClose.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Close()
        Next
    End Sub

    Private Sub butWindowFlash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowFlash.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Flash()
        Next
    End Sub

    Private Sub butWindowHide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowHide.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Hide()
        Next
    End Sub

    Private Sub butWindowMaximize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowMaximize.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Maximize()
        Next
    End Sub

    Private Sub butWindowMinimize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowMinimize.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Minimize()
        Next
    End Sub

    Private Sub butWindowOpacity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowOpacity.Click
        Dim i As Byte
        Dim z As Integer = 0

        If Me.lvWindows.SelectedItems.Count > 0 Then
            z = Me.lvWindows.GetSelectedItem.Infos.Opacity
        End If

        Dim sres As String = CInputBox("Set a new opacity [0 to 255, 255 = minimum transparency]", "New opacity", CStr(z))

        If sres Is Nothing OrElse sres.Equals(String.Empty) Then Exit Sub

        i = CByte(Val(sres))
        If i < 0 Or i > 255 Then Exit Sub

        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Opacity = i
        Next
    End Sub

    Private Sub butWindowSetAsActive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowSetAsActive.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.SetAsActiveWindow()
        Next
    End Sub

    Private Sub butWindowSetAsForeground_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowSetAsForeground.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.SetAsForegroundWindow()
        Next
    End Sub

    Private Sub butWindowShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowShow.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Show()
        Next
    End Sub

    Private Sub butWindowDoNotBringToFront_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowDoNotBringToFront.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.BringToFront(False)
        Next
    End Sub

    Private Sub butWindowPositionSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowPositionSize.Click
        Dim r As Native.Api.NativeStructs.Rect

        If Me.lvWindows.SelectedItems.Count > 0 Then

            Dim frm As New frmWindowPosition
            With frm
                .SetCurrentPositions(Me.lvWindows.GetSelectedItem.Infos.Positions)
                .TopMost = _frmMain.TopMost

                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    r = .NewRect
                    For Each it As cWindow In Me.lvWindows.GetSelectedItems
                        it.SetPositions(r)
                    Next
                End If
            End With
        End If
    End Sub

    Private Sub butWindowEnable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowEnable.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Enabled = True
        Next
    End Sub

    Private Sub butWindowDisable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowDisable.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Enabled = False
        Next
    End Sub

    Private Sub butWindowStopFlashing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowStopFlashing.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.StopFlashing()
        Next
    End Sub

    Private Sub butHandlesSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandlesSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "handles"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub butDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDeleteFile.Click
        cSelFile.WindowsKill()
    End Sub

    Private Sub butFileMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileMove.Click
        With Me.FolderChooser
            .Description = "Select new location"
            .SelectedPath = cFile.GetParentDir(cSelFile.Path)
            .ShowNewFolderButton = True
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFile.Text = cSelFile.WindowsMove(.SelectedPath)
                Call Me.refreshFileInfos(cSelFile.Path)
            End If
        End With
    End Sub

    Private Sub butFileCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileCopy.Click
        With Me.saveDial
            .AddExtension = True
            .FileName = cSelFile.Name
            .Filter = "All (*.*)|*.*"
            .InitialDirectory = cSelFile.GetParentDir
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                cSelFile.WindowCopy(.FileName)
            End If
        End With
    End Sub

    Private Sub butFileRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRename.Click
        Dim s As String = CInputBox("New name (name+extension) ?", "Select a new file name", cFile.GetFileName(cSelFile.Path))
        If s Is Nothing OrElse s.Equals(String.Empty) Then Exit Sub
        Me.txtFile.Text = cSelFile.WindowsRename(s)
        Call Me.refreshFileInfos(cSelFile.Path)
    End Sub

    Private Sub butProcessShowModules_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessShowModules.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then

            Dim x As Integer = 0
            ReDim modulesToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                modulesToRefresh(x) = it.Infos.Pid
                x += 1
            Next

            Call ShowModules()
            Me.Ribbon.ActiveTab = Me.ModulesTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    ' Show modules of selected processes (modulesToRefresh)
    Private Sub ShowModules(Optional ByVal showTab As Boolean = True)

        Me.lvModules.ProcessId = Me.modulesToRefresh
        Me.lvModules.UpdateTheItems()

        If showTab Then _
            Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"

    End Sub

    Private Sub butModuleRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleRefresh.Click
        If modulesToRefresh IsNot Nothing Then Call ShowModules()
    End Sub

    Private Sub butModulesSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModulesSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "modules"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub butModuleUnload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleUnload.Click
        For Each it As cModule In Me.lvModules.GetSelectedItems
            it.UnloadModule()
            'it.Remove()
        Next
        Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"
    End Sub

    Private Sub butThreadSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "threads"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub butWindowSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "windows"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub butServiceReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "services"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    Private Sub butProcessShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessShowAll.Click

        ' We refresh all informations for the selected processes
        If Me.lvProcess.SelectedItems.Count > 0 Then

            Dim x As Integer = 0
            ReDim modulesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
            ReDim windowsToRefresh(Me.lvProcess.SelectedItems.Count - 1)
            ReDim handlesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
            ReDim threadsToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each cp As cProcess In Me.lvProcess.GetSelectedItems
                modulesToRefresh(x) = cp.Infos.Pid
                windowsToRefresh(x) = cp.Infos.Pid
                handlesToRefresh(x) = cp.Infos.Pid
                threadsToRefresh(x) = cp.Infos.Pid
                x += 1
            Next

            Call ShowModules(False)
            Call ShowThreads(False)
            Call ShowWindows(False)
            Call ShowHandles(False)
        End If

    End Sub

    Private Sub butModuleGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleGoogle.Click
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
    End Sub

    Private Sub butAlwaysDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAlwaysDisplay.Click
        Call changeTopMost()
    End Sub

    Private Sub butPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPreferences.Click
        Dim frm As New frmPreferences
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    'Private Sub butProcessPermuteLvTv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessPermuteLvTv.Click
    '    Static _oldProcessColumnWidth As Integer = 100
    '    If butProcessPermuteLvTv.Text = "Listview" Then
    '        Me.SplitContainerTvLv.Panel1Collapsed = True
    '        'Me.lvProcess.ShowGroups = True
    '        Me.lvProcess.Columns(0).Width = _oldProcessColumnWidth
    '        butProcessPermuteLvTv.Image = My.Resources.tv2
    '        butProcessPermuteLvTv.Text = "Treeview"
    '    Else
    '        Me.SplitContainerTvLv.Panel1Collapsed = False
    '        ' Me.lvProcess.ShowGroups = False
    '        _oldProcessColumnWidth = Me.lvProcess.Columns(0).Width
    '        Me.lvProcess.Columns(0).Width = 0
    '        butProcessPermuteLvTv.Text = "Listview"
    '        butProcessPermuteLvTv.Image = My.Resources.lv3
    '    End If
    'End Sub

    Private Sub butProcessDisplayDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessDisplayDetails.Click
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            Dim frm As New frmProcessInfo
            frm.SetProcess(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    ' Add a process node
    'Private Sub addNewProcessNode(ByRef p As cProcess, ByVal imgkey As String)
    '    Dim parent As Integer = p.ParentProcessId
    '    Dim pid As Integer = p.Pid

    '    Dim n As TreeNode = findNode(Me.tvProc.Nodes(0).Nodes, parent)

    '    Dim nn As New TreeNode
    '    nn.Text = p.Name
    '    nn.Tag = CStr(pid)
    '    nn.ImageKey = imgkey
    '    nn.SelectedImageKey = imgkey

    '    If n Is Nothing Then
    '        ' New node (parent was killed)
    '        Me.tvProc.Nodes(0).Nodes.Add(nn)
    '        Me.tvProc.Nodes(0).ExpandAll()
    '    Else
    '        ' Found parent
    '        n.Nodes.Add(nn)
    '        n.ExpandAll()
    '    End If

    'End Sub

    'Private Function findNode(ByRef nodes As TreeNodeCollection, ByVal pid As Integer) As TreeNode
    '    Dim n As TreeNode
    '    For Each n In nodes
    '        If n.Tag.ToString = CStr(pid) Then
    '            Return n
    '        Else
    '            findNode(n.Nodes, pid)
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    Private Sub creg_KeyAdded(ByVal key As cRegMonitor.KeyDefinition) Handles creg.KeyAdded
        'log.AppendLine("Service added : " & key.name)
        If My.Settings.NotifyNewServices Then
            With Me.Tray
                .BalloonTipText = key.name
                .BalloonTipIcon = ToolTipIcon.Info
                .BalloonTipTitle = "A new service has been created"
                .ShowBalloonTip(3000)
            End With
        End If
    End Sub

    Private Sub creg_KeyDeleted(ByVal key As cRegMonitor.KeyDefinition) Handles creg.KeyDeleted
        'log.AppendLine("Service deleted : " & key.name)
        If My.Settings.NotifyDeletedServices Then
            With Me.Tray
                .BalloonTipText = key.name
                .BalloonTipIcon = ToolTipIcon.Info
                .BalloonTipTitle = "A service has been deleted"
                .ShowBalloonTip(3000)
            End With
        End If
    End Sub

    ' Refresh  task list in listview
    Public Sub refreshTaskList()

        ' Update list
        Me.lvTask.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Tasks" Then
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvTask.Items.Count) & " tasks running"
            End If
        End If

    End Sub

    Private Sub timerTask_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timerTask.Tick
        Call refreshTaskList()
    End Sub

    Private Sub butTaskRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTaskRefresh.Click
        Call refreshTaskList()
    End Sub

    Private Sub butTaskShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTaskShow.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.SetAsForegroundWindow()
        Next
    End Sub

    Private Sub butTaskEndTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTaskEndTask.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            ' Close task
            it.Close()
        Next
    End Sub

    Private Sub butWindowFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowFind.Click
        frmFindWindow.TopMost = _frmMain.TopMost
        frmFindWindow.Show()
    End Sub

    Private Sub refreshNetworkList()

        Me.lvNetwork.ShowAllPid = True
        Me.lvNetwork.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Network" Then
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvNetwork.Items.Count) & " connections opened"
            End If
        End If
    End Sub

    Private Sub butNetworkRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNetworkRefresh.Click
        Call refreshNetworkList()
    End Sub

    Private Sub timerTrayIcon_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerTrayIcon.Tick
        ' Refresh infos
        Call Program.SystemInfo.RefreshInfo()

        Dim _cpuUsage As Double = Program.SystemInfo.CpuUsage
        Dim _physMemUsage As Double = Program.SystemInfo.PhysicalMemoryPercentageUsage
        Dim d As New Decimal(Decimal.Multiply(Program.SystemInfo.TotalPhysicalMemory, New Decimal(_physMemUsage)))

        If _cpuUsage > 1 Then _cpuUsage = 1

        Dim s As String = "CPU usage : " & CStr(Math.Round(100 * _cpuUsage, 3)) & " %"
        s &= vbNewLine & "Phys. mem. usage : " & GetFormatedSize(d) & " (" & CStr(Math.Round(100 * _physMemUsage, 3)) & " %)"

        Me.Tray.Text = s

        Program.TrayIcon.AddValue(1, _cpuUsage)
        Program.TrayIcon.AddValue(2, _physMemUsage)
    End Sub

    ' Update monitoring log
    Private Sub updateMonitoringLog()
        '        Dim s As String

        If Me.tvMonitor.Nodes.Item(0).Nodes.Count > 0 Then

            ' Count counters :-)
            'Dim iCount As Integer = 0
            'Dim n As TreeNode
            'Dim n2 As TreeNode
            'For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
            '    For Each n2 In n.Nodes
            '        iCount += 1
            '    Next
            'Next

            's = "Monitoring log" & vbNewLine
            's &= vbNewLine & vbNewLine & "Monitoring " & CStr(iCount) & " item(s)" & vbNewLine

            'For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
            '    For Each n2 In n.Nodes

            '        Dim it As cMonitor = CType(n2.Tag, cMonitor)
            '        s &= vbNewLine & "* Category  : " & it.CategoryName & " -- Instance : " & it.InstanceName & " -- Counter : " & it.CounterName
            '        s &= vbNewLine & "      Monitoring creation : " & it.MonitorCreationDate.ToLongDateString & " -- " & it.MonitorCreationDate.ToLongTimeString
            '        If it.LastStarted.Ticks > 0 Then
            '            s &= vbNewLine & "      Last start : " & it.LastStarted.ToLongDateString & " -- " & it.LastStarted.ToLongTimeString
            '        Else
            '            s &= vbNewLine & "      Not yet started"
            '        End If
            '        s &= vbNewLine & "      State : " & it.Enabled
            '        s &= vbNewLine & "      Interval : " & it.Interval

            '        s &= vbNewLine
            '    Next
            'Next
            's = s.Substring(0, s.Length - 2)

            'Me.txtMonitoringLog.Text = s
            'Me.txtMonitoringLog.SelectionLength = 0
            'Me.txtMonitoringLog.SelectionStart = 0


            Me.lvMonitorReport.Items.Clear()
            Me.lvMonitorReport.BeginUpdate()
            For Each n As TreeNode In Me.tvMonitor.Nodes.Item(0).Nodes
                For Each n2 As TreeNode In n.Nodes

                    Dim it As cMonitor = CType(n2.Tag, cMonitor)

                    Dim k As String = n.Text
                    Try
                        Dim g As New ListViewGroup(k, k)
                        g.HeaderAlignment = HorizontalAlignment.Center
                        Me.lvMonitorReport.Groups.Add(g)
                    Catch ex As Exception
                        '
                    End Try

                    Dim lvit As New ListViewItem(it.CounterName)
                    lvit.SubItems.Add(it.MachineName)
                    lvit.SubItems.Add(it.MonitorCreationDate.ToLongDateString & " -- " & it.MonitorCreationDate.ToLongTimeString)
                    If it.LastStarted.Ticks > 0 Then
                        lvit.SubItems.Add(it.LastStarted.ToLongDateString & " -- " & it.LastStarted.ToLongTimeString)
                    Else
                        lvit.SubItems.Add("Not yet started")
                    End If
                    lvit.SubItems.Add(it.Enabled.ToString)
                    lvit.SubItems.Add(it.Interval.ToString)

                    lvit.Group = Me.lvMonitorReport.Groups.Item(k)
                    Me.lvMonitorReport.Items.Add(lvit)

                Next
            Next

            Me.lvMonitorReport.EndUpdate()
            Me.lvMonitorReport.BringToFront()

        Else
            Me.txtMonitoringLog.Text = "No process monitored." & vbNewLine & "Click on 'Add' button to monitor a process."
            Me.txtMonitoringLog.SelectionLength = 0
            Me.txtMonitoringLog.SelectionStart = 0
            Me.txtMonitoringLog.BringToFront()
        End If

    End Sub

    Private Sub butSaveProcessReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveProcessReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "processes"
            Call Application.DoEvents()
            .TopMost = _frmMain.TopMost
            .ShowDialog()
        End With
    End Sub

    ' Permute style of menus
    Public Sub permuteMenuStyle(ByVal ribbonStyle As Boolean)
        '=============== ' _tab.Region = New Region(New RectangleF(_tab.TabPages(0).Left, _tab.TabPages(0).Top, _tab.TabPages(0).Width, _tab.TabPages(0).Height))

        ' Change selected tab of tabStrip
        _ribbonStyle = ribbonStyle

        _main.Panel1Collapsed = Not (_ribbonStyle)

        Me.MenuItemSYSTEMFILE.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMOPT.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMTOOLS.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMSYSTEM.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMHEL.Visible = Not (_ribbonStyle)

        Call Me.frmMain_Resize(Nothing, Nothing)
    End Sub

    Private Sub txtServiceSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServiceSearch.Click
        Call txtServiceSearch_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub txtServiceSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServiceSearch.TextChanged
        Dim it As ListViewItem
        For Each it In lvServices.Items
            If InStr(LCase(it.Text), LCase(txtServiceSearch.Text)) = 0 And _
                    InStr(LCase(it.SubItems.Item(1).Text), LCase(txtServiceSearch.Text)) = 0 Then
                it.Group = lvServices.Groups(0)
            Else
                it.Group = lvServices.Groups(1)
            End If
        Next
        Me.lblResCount2.Text = CStr(lvServices.Groups(1).Items.Count) & " results(s)"
    End Sub

    Private Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Call txtSearch_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim it As ListViewItem
        For Each it In lvProcess.Items
            If InStr(LCase(it.Text), LCase(txtSearch.Text)) = 0 Then
                it.Group = lvProcess.Groups(0)
            Else
                it.Group = lvProcess.Groups(1)
            End If
        Next
        Me.lblResCount.Text = CStr(lvProcess.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub txtFile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFile.TextChanged
        Dim b As Boolean = IO.File.Exists(Me.txtFile.Text)
        Me.RBFileDelete.Enabled = b
        Me.RBFileKillProcess.Enabled = b
        Me.RBFileOnline.Enabled = b
        Me.RBFileOther.Enabled = b
        Me.RBFileOthers.Enabled = b
    End Sub

    Private Sub tvMonitor_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvMonitor.AfterSelect

        Me.graphMonitor.EnableGraph = False

        If tvMonitor.SelectedNode Is Nothing Then Exit Sub

        If tvMonitor.SelectedNode.Parent IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent.Name = tvMonitor.Nodes.Item(0).Name Then
                ' Then we have selected a process
                Me.butMonitorStart.Enabled = True
                Me.butMonitorStop.Enabled = True
                Me.graphMonitor.CreateGraphics.Clear(Color.Black)
                Me.graphMonitor.CreateGraphics.DrawString("Select in the treeview a counter.", Me.Font, Brushes.White, 0, 0)
            Else
                Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                Me.butMonitorStart.Enabled = Not (it.Enabled)
                Me.butMonitorStop.Enabled = it.Enabled

                ' We have selected a sub item -> display values in graph
                Dim sKey As String = tvMonitor.SelectedNode.Text
                Call ShowMonitorStats(it, sKey, "", "")
            End If
        Else
            ' The we can start/stop all items
            Me.butMonitorStart.Enabled = True
            Me.butMonitorStop.Enabled = True
            Me.graphMonitor.CreateGraphics.Clear(Color.Black)
            Me.graphMonitor.CreateGraphics.DrawString("Select in the treeview an item and then a counter.", Me.Font, Brushes.White, 0, 0)
        End If

        Me.MenuItemMonitorStart.Enabled = Me.butMonitorStart.Enabled
        Me.MenuItemMonitorStop.Enabled = Me.butMonitorStop.Enabled
    End Sub

    Private Sub tvProc_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvProc.AfterCollapse
        Me.lvProcess.Items(0).Group = Me.lvProcess.Groups(1)
    End Sub

    Private Sub rtb2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb2.TextChanged
        Me.cmdCopyServiceToCp.Enabled = (rtb2.Rtf.Length > 0)
    End Sub

    Private Sub lvTask_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTask.DoubleClick
        If MenuItemTaskSelProc.Enabled Then _
        Call Me.MenuItemTaskSelProc_Click(Nothing, Nothing)
    End Sub

    Private Sub lvTask_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvTask.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvTask)
    End Sub

    Private Sub lvServices_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvServices.KeyDown
        If e.KeyCode = Keys.Enter And Me.lvServices.SelectedItems.Count > 0 Then
            Call Me.butServiceDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvServices_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            For Each it As cService In Me.lvServices.GetSelectedItems
                Dim frm As New frmServiceInfo
                frm.SetService(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        End If
    End Sub

    Private Sub lvServices_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvServices)
    End Sub

    Private Sub lvServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvServices.SelectedItems IsNot Nothing _
                    AndAlso Me.lvServices.SelectedItems.Count > 0)

            If lvServices.SelectedItems.Count = 1 Then
                Dim cSe As cService = Me.lvServices.GetSelectedItem
                Dim start As Native.Api.NativeEnums.ServiceStartType = cSe.Infos.StartType
                Dim state As Native.Api.NativeEnums.ServiceState = cSe.Infos.State
                Dim acc As Native.Api.NativeEnums.ServiceAccept = cSe.Infos.AcceptedControl

                Me.MenuItemServPause.Text = IIf(state = Native.Api.NativeEnums.ServiceState.Running, "Pause", "Resume").ToString
                MenuItemServPause.Enabled = (acc And Native.Api.NativeEnums.ServiceAccept.PauseContinue) = Native.Api.NativeEnums.ServiceAccept.PauseContinue
                MenuItemServStart.Enabled = Not (state = Native.Api.NativeEnums.ServiceState.Running)
                Me.MenuItemServStop.Enabled = (acc And Native.Api.NativeEnums.ServiceAccept.Stop) = Native.Api.NativeEnums.ServiceAccept.Stop

                Me.MenuItemServDisabled.Checked = (start = Native.Api.NativeEnums.ServiceStartType.StartDisabled)
                MenuItemServDisabled.Enabled = Not (MenuItemServDisabled.Checked)
                MenuItemServAutoStart.Checked = (start = Native.Api.NativeEnums.ServiceStartType.AutoStart)
                MenuItemServAutoStart.Enabled = Not (MenuItemServAutoStart.Checked)
                MenuItemServOnDemand.Checked = (start = Native.Api.NativeEnums.ServiceStartType.DemandStart)
                MenuItemServOnDemand.Enabled = Not (MenuItemServOnDemand.Checked)
                MenuItem8.Enabled = True
            ElseIf lvServices.SelectedItems.Count > 1 Then
                MenuItemServPause.Text = "Pause"
                MenuItemServPause.Enabled = True
                MenuItemServStart.Enabled = True
                MenuItemServStop.Enabled = True
                MenuItemServDisabled.Checked = True
                MenuItemServDisabled.Enabled = True
                MenuItemServAutoStart.Checked = True
                MenuItemServAutoStart.Enabled = True
                MenuItemServOnDemand.Checked = True
                MenuItemServOnDemand.Enabled = True
                MenuItem8.Enabled = True
            ElseIf lvServices.SelectedItems.Count = 0 Then
                MenuItemServPause.Text = "Pause"
                MenuItemServPause.Enabled = False
                MenuItemServStart.Enabled = False
                MenuItemServStop.Enabled = False
                MenuItemServDisabled.Checked = False
                MenuItemServDisabled.Enabled = False
                MenuItemServAutoStart.Checked = False
                MenuItemServAutoStart.Enabled = False
                MenuItemServOnDemand.Checked = False
                MenuItemServOnDemand.Enabled = False
                MenuItem8.Enabled = False
            End If

            Me.MenuItemServFileDetails.Enabled = selectionIsNotNothing AndAlso _local AndAlso Me.lvServices.SelectedItems.Count = 1
            Me.MenuItemServFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServSearch.Enabled = selectionIsNotNothing
            Me.MenuItemServDepe.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServSelService.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServReanalize.Enabled = selectionIsNotNothing
            Me.MenuItemCopyService.Enabled = selectionIsNotNothing

            Me.mnuService.Show(Me.lvServices, e.Location)
        End If
    End Sub

    Private Sub lvServices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvServices.SelectedIndexChanged
        ' New service selected
        If lvServices.SelectedItems.Count = 1 Then
            Try
                Dim cS As cService = Me.lvServices.GetSelectedItem

                Me.lblServiceName.Text = "Service name : " & cS.Infos.Name
                Me.lblServicePath.Text = "Service path : " & cS.GetInformation("ImagePath")

                ' Description
                Dim s As String = vbNullString
                Dim description As String = vbNullString
                Dim diagnosticsMessageFile As String = cS.Infos.DiagnosticMessageFile
                Dim group As String = cS.Infos.LoadOrderGroup
                Dim objectName As String = cS.Infos.ObjectName
                Dim sp As String = cS.GetInformation("ImagePath")

                ' OK it's not the best way to retrive the description...
                ' (if @ -> extract string to retrieve description)
                Dim sTemp As String = cS.Infos.Description
                If InStr(sTemp, "@", CompareMethod.Binary) > 0 Then
                    description = Native.Objects.File.GetResourceStringFromFile(sTemp)
                Else
                    description = sTemp
                End If
                description = Replace(cS.Infos.Description, "\", "\\")


                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Service properties\b0\par"
                s = s & "\tab Name :\tab\tab\tab " & cS.Infos.Name & "\par"
                s = s & "\tab Common name :\tab\tab " & cS.Infos.DisplayName & "\par"
                If Len(sp) > 0 Then s = s & "\tab Path :\tab\tab\tab " & Replace(cS.GetInformation("ImagePath"), "\", "\\") & "\par"
                If Len(description) > 0 Then s = s & "\tab Description :\tab\tab " & description & "\par"
                If Len(group) > 0 Then s = s & "\tab Group :\tab\tab\tab " & group & "\par"
                If Len(objectName) > 0 Then s = s & "\tab ObjectName :\tab\tab " & objectName & "\par"
                If Len(diagnosticsMessageFile) > 0 Then s = s & "\tab DiagnosticsMessageFile :\tab\tab " & diagnosticsMessageFile & "\par"
                s = s & "\tab State :\tab\tab\tab " & cS.Infos.State.ToString & "\par"
                s = s & "\tab Startup :\tab\tab " & cS.Infos.StartType.ToString & "\par"
                If cS.Infos.ProcessId > 0 Then s = s & "\tab Owner process :\tab\tab " & cS.Infos.ProcessId & "-- " & cProcess.GetProcessName(cS.Infos.ProcessId) & "\par"
                s = s & "\tab Service type :\tab\tab " & cS.Infos.ServiceType.ToString & "\par"

                s = s & "}"

                rtb2.Rtf = s

                ' Treeviews stuffs
                ' Only if we are in local mode...
                If Program.Connection.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
                    With tv
                        .RootService = cS.Infos.Name
                        .InfosToGet = cServDepConnection.DependenciesToget.DependenciesOfMe
                        .UpdateItems()
                    End With
                    With tv2
                        .RootService = cS.Infos.Name
                        .InfosToGet = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
                        .UpdateItems()
                    End With
                Else
                    tv.ClearItems()
                    tv.SafeAdd("No auto refresh for remote monitoring")
                    tv2.ClearItems()
                    tv2.SafeAdd("No auto refresh for remote monitoring")
                End If

            Catch ex As Exception
                Dim s As String = ""
                Dim er As Exception = ex

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                s = s & "\tab Message :\tab " & er.Message & "\par"
                s = s & "\tab Source :\tab\tab " & er.Source & "\par"
                If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                s = s & "}"

                rtb2.Rtf = s
            End Try

        End If
    End Sub

    Private Sub lvSearchResults_HasRefreshed() Handles lvSearchResults.HasRefreshed
        Me.butSearchGo.Enabled = True
        Me.MenuItemSearchNew.Enabled = True
    End Sub

    Private Sub lvSearchResults_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSearchResults.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvSearchResults)
    End Sub

    Private Sub lvProcess_GotAnError(ByVal origin As String, ByVal msg As String) Handles lvProcess.GotAnError
        MsgBox("Error : " & msg & vbNewLine & "Origin : " & origin & vbNewLine & vbNewLine & "YAPM will be disconnected from the machine.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Error")
        Call Me.DisconnectFromMachine()
    End Sub

    Private Sub lvProcess_ItemAdded(ByRef item As cProcess) Handles lvProcess.ItemAdded
        If item IsNot Nothing Then
            Program.Log.AppendLine("Process created : " & item.Infos.Name & " (" & item.Infos.Pid & ")")
            If Me.MenuItemTaskSelProc.Enabled = False Then
                MenuItemTaskSelProc.Enabled = True
            End If
            If My.Settings.NotifyNewProcesses AndAlso Me.lvProcess.FirstRefreshDone Then
                Dim text As String = "Name : " & item.Infos.Name & " (" & item.Infos.Pid.ToString & ")"
                If item.Infos.ParentProcessId > 0 Then
                    text &= vbNewLine & "Parent : " & _
                        cProcess.GetProcessName(item.Infos.ParentProcessId) & " (" & _
                        cProcess.GetProcessName(item.Infos.ParentProcessId) & ")"
                End If
                If item.Infos.FileInfo IsNot Nothing Then
                    text &= vbNewLine & "Company : " & item.Infos.FileInfo.CompanyName & _
                        vbNewLine & "Description : " & item.Infos.FileInfo.FileDescription
                End If
                With Me.Tray
                    .BalloonTipText = text
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "A new process has been started"
                    .ShowBalloonTip(3000)
                End With
            End If
        End If
    End Sub

    Private Sub lvProcess_ItemDeleted(ByRef item As cProcess) Handles lvProcess.ItemDeleted
        If item IsNot Nothing Then
            Program.Log.AppendLine("Process deleted : " & item.Infos.Name & " (" & item.Infos.Pid & ")")
            If My.Settings.NotifyTerminatedProcesses Then
                Dim text As String = "Name : " & item.Infos.Name & " (" & item.Infos.Pid.ToString & ")"
                If item.Infos.ParentProcessId > 0 Then
                    text &= vbNewLine & "Parent : " & _
                        cProcess.GetProcessName(item.Infos.ParentProcessId) & " (" & _
                        cProcess.GetProcessName(item.Infos.ParentProcessId) & ")"
                End If
                If item.Infos.FileInfo IsNot Nothing Then
                    text &= vbNewLine & "Company : " & item.Infos.FileInfo.CompanyName & _
                        vbNewLine & "Description : " & item.Infos.FileInfo.FileDescription
                End If
                With Me.Tray
                    .BalloonTipText = text
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "A process has been terminated"
                    .ShowBalloonTip(3000)
                End With
            End If
        End If
    End Sub

    Private Sub lvProcess_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcess.KeyDown
        If e.KeyCode = Keys.Delete And Me.lvProcess.SelectedItems.Count > 0 Then
            If My.Settings.WarnDangerousActions Then
                If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            End If
            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                it.Kill()
            Next
        ElseIf e.KeyCode = Keys.Enter And Me.lvProcess.SelectedItems.Count > 0 Then
            Call Me.butProcessDisplayDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Call Me.butProcessDisplayDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvProcess_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvProcess)
    End Sub

    Private Sub lvProcess_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As Integer = -1
            If Me.lvProcess.SelectedItems Is Nothing Then
                Me.MenuItemProcPIdle.Checked = False
                Me.MenuItemProcPN.Checked = False
                Me.MenuItemProcPAN.Checked = False
                Me.MenuItemProcPBN.Checked = False
                Me.MenuItemProcPH.Checked = False
                Me.MenuItemProcPRT.Checked = False
                Exit Sub
            End If
            If Me.lvProcess.SelectedItems.Count = 1 Then
                p = Me.lvProcess.GetSelectedItem.Infos.Priority
            End If
            Me.MenuItemProcPIdle.Checked = (p = ProcessPriorityClass.Idle)
            Me.MenuItemProcPN.Checked = (p = ProcessPriorityClass.Normal)
            Me.MenuItemProcPAN.Checked = (p = ProcessPriorityClass.AboveNormal)
            Me.MenuItemProcPBN.Checked = (p = ProcessPriorityClass.BelowNormal)
            Me.MenuItemProcPH.Checked = (p = ProcessPriorityClass.High)
            Me.MenuItemProcPRT.Checked = (p = ProcessPriorityClass.RealTime)

            Dim selectionIsNotNothing As Boolean = (Me.lvProcess.SelectedItems IsNot Nothing AndAlso Me.lvProcess.SelectedItems.Count > 0)
            Me.MenuItem27.Enabled = selectionIsNotNothing
            Me.MenuItem35.Enabled = selectionIsNotNothing
            Me.MenuItemProcKill.Enabled = selectionIsNotNothing
            Me.MenuItemProcPriority.Enabled = selectionIsNotNothing
            Me.MenuItemProcReanalize.Enabled = selectionIsNotNothing
            Me.MenuItemProcResume.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcKillT.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcStop.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcResume.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcSFileDetails.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSSearch.Enabled = selectionIsNotNothing
            Me.MenuItemProcSDep.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemCopyProcess.Enabled = selectionIsNotNothing
            Me.MenuItemProcSFileDetails.Enabled = (selectionIsNotNothing AndAlso Me.lvProcess.SelectedItems.Count = 1)
            Me.MenuItemProcDump.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcAff.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcSAll.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcSHandles.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcSServices.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcSWindows.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcWSS.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemProcKillByMethod.Enabled = selectionIsNotNothing AndAlso _notWMI

            ' Job menuitems
            Me.MenuItemProcJob.Enabled = selectionIsNotNothing AndAlso _local
            If Me.lvProcess.SelectedItems.Count <> 1 Then
                Me.MenuItemJobMng.Enabled = True
            Else
                Me.MenuItemJobMng.Enabled = (cJob.GetProcessJobById(Me.lvProcess.GetSelectedItem.Infos.Pid) IsNot Nothing)
                Me.MenuItemProcAddToJob.Enabled = Not (Me.MenuItemJobMng.Enabled)
            End If

            Me.mnuProcess.Show(Me.lvProcess, e.Location)
        End If
    End Sub

    Private Sub lvModules_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvModules.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvModules)
    End Sub

    Private Sub lvModules_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvModules.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvModules.SelectedItems IsNot Nothing _
                                        AndAlso Me.lvModules.SelectedItems.Count > 0)
            Me.MenuItemModuleDependencies.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemModuleFileDetails.Enabled = selectionIsNotNothing AndAlso _local AndAlso Me.lvModules.SelectedItems.Count = 1
            Me.MenuItemModuleFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemModuleOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemModuleSearch.Enabled = selectionIsNotNothing
            Me.MenuItemModuleSelProc.Enabled = selectionIsNotNothing
            Me.MenuItemUnloadModule.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemCopyModule.Enabled = selectionIsNotNothing
            Me.mnuModule.Show(Me.lvModules, e.Location)
        End If
    End Sub

    Private Sub lvModules_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvModules.SelectedIndexChanged
        If lvModules.SelectedItems.Count = 1 Then
            Try

                Dim cP As cModule = lvModules.GetSelectedItem

                ' Description
                Dim s As String = ""
                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Module properties\b0\par"
                s = s & "\tab Module name :\tab\tab\tab " & cFile.GetFileName(cP.Infos.Path) & "\par"
                s = s & "\tab Process owner :\tab\tab\tab " & CStr(cP.Infos.ProcessId) & " -- " & cProcess.GetProcessName(cP.Infos.ProcessId) & "\par"
                s = s & "\tab Path :\tab\tab\tab\tab " & Replace(cP.Infos.Path, "\", "\\") & "\par"
                If cP.Infos.FileInfo IsNot Nothing Then
                    s = s & "\tab Version :\tab\tab\tab " & cP.Infos.FileInfo.FileVersion & "\par"
                    s = s & "\tab Comments :\tab\tab\tab " & cP.Infos.FileInfo.Comments & "\par"
                    s = s & "\tab CompanyName :\tab\tab\tab " & cP.Infos.FileInfo.CompanyName & "\par"
                    s = s & "\tab LegalCopyright :\tab\tab\tab " & cP.Infos.FileInfo.LegalCopyright & "\par"
                    s = s & "\tab ProductName :\tab\tab\tab " & cP.Infos.FileInfo.ProductName & "\par"
                    s = s & "\tab Language :\tab\tab\tab " & cP.Infos.FileInfo.Language & "\par"
                    s = s & "\tab InternalName :\tab\tab\tab " & cP.Infos.FileInfo.InternalName & "\par"
                    s = s & "\tab LegalTrademarks :\tab\tab " & cP.Infos.FileInfo.LegalTrademarks & "\par"
                    s = s & "\tab OriginalFilename :\tab\tab\tab " & cP.Infos.FileInfo.OriginalFilename & "\par"
                    s = s & "\tab FileBuildPart :\tab\tab\tab " & CStr(cP.Infos.FileInfo.FileBuildPart) & "\par"
                    s = s & "\tab FileDescription :\tab\tab\tab " & CStr(cP.Infos.FileInfo.FileDescription) & "\par"
                    s = s & "\tab FileMajorPart :\tab\tab\tab " & CStr(cP.Infos.FileInfo.FileMajorPart) & "\par"
                    s = s & "\tab FileMinorPart :\tab\tab\tab " & CStr(cP.Infos.FileInfo.FileMinorPart) & "\par"
                    s = s & "\tab FilePrivatePart :\tab\tab\tab " & CStr(cP.Infos.FileInfo.FilePrivatePart) & "\par"
                    s = s & "\tab IsDebug :\tab\tab\tab " & CStr(cP.Infos.FileInfo.IsDebug) & "\par"
                    s = s & "\tab IsPatched :\tab\tab\tab " & CStr(cP.Infos.FileInfo.IsPatched) & "\par"
                    s = s & "\tab IsPreRelease :\tab\tab\tab " & CStr(cP.Infos.FileInfo.IsPreRelease) & "\par"
                    s = s & "\tab IsPrivateBuild :\tab\tab\tab " & CStr(cP.Infos.FileInfo.IsPrivateBuild) & "\par"
                    s = s & "\tab IsSpecialBuild :\tab\tab\tab " & CStr(cP.Infos.FileInfo.IsSpecialBuild) & "\par"
                    s = s & "\tab PrivateBuild :\tab\tab\tab " & CStr(cP.Infos.FileInfo.PrivateBuild) & "\par"
                    s = s & "\tab ProductBuildPart :\tab\tab " & CStr(cP.Infos.FileInfo.ProductBuildPart) & "\par"
                    s = s & "\tab ProductMajorPart :\tab\tab " & CStr(cP.Infos.FileInfo.ProductMajorPart) & "\par"
                    s = s & "\tab ProductMinorPart :\tab\tab " & CStr(cP.Infos.FileInfo.ProductMinorPart) & "\par"
                    s = s & "\tab ProductPrivatePart :\tab\tab " & CStr(cP.Infos.FileInfo.ProductPrivatePart) & "\par"
                    s = s & "\tab ProductVersion :\tab\tab\tab " & CStr(cP.Infos.FileInfo.ProductVersion) & "\par"
                    s = s & "\tab SpecialBuild :\tab\tab\tab " & CStr(cP.Infos.FileInfo.SpecialBuild) & "\par"
                End If

                s = s & "}"

                rtb6.Rtf = s

            Catch ex As Exception
                Dim s As String = ""
                Dim er As Exception = ex

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                s = s & "\tab Message :\tab " & er.Message & "\par"
                s = s & "\tab Source :\tab\tab " & er.Source & "\par"
                If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                s = s & "}"

                rtb6.Rtf = s

            End Try

        End If
    End Sub

    Private Sub lstFileString_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvFileString.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Dim s As String = vbNullString
            Dim it As ListViewItem
            Dim x As Integer = 0
            For Each it In Me.lvFileString.SelectedItems
                s &= it.Text
                x += 1
                If Not (x = Me.lvFileString.SelectedItems.Count) Then s &= vbNewLine
            Next
            If Not (s = vbNullString) Then My.Computer.Clipboard.SetText(s, TextDataFormat.UnicodeText)
        End If
    End Sub

    Private Sub lblTaskCountResult_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblTaskCountResult.MouseDown
        If Me.lvTask.Groups(1).Items.Count > 0 Then
            Me.lvTask.Focus()
            Me.lvTask.EnsureVisible(Me.lvTask.Groups(1).Items(0).Index)
            Me.lvTask.SelectedItems.Clear()
            Me.lvTask.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub txtSearchTask_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchTask.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvTask.Items
            If InStr(LCase(it.Text), LCase(Me.txtSearchTask.Text)) = 0 Then
                it.Group = lvTask.Groups(0)
            Else
                it.Group = lvTask.Groups(1)
            End If
        Next
        Me.lblTaskCountResult.Text = CStr(Me.lvTask.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblResCount2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResCount2.Click
        If Me.lvServices.Groups(1).Items.Count > 0 Then
            Me.lvServices.Focus()
            Me.lvServices.EnsureVisible(Me.lvServices.Groups(1).Items(0).Index)
            Me.lvServices.SelectedItems.Clear()
            Me.lvServices.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub lblResCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResCount.Click
        If Me.lvProcess.Groups(1).Items.Count > 0 Then
            Me.lvProcess.Focus()
            Me.lvProcess.EnsureVisible(Me.lvProcess.Groups(1).Items(0).Index)
            Me.lvProcess.SelectedItems.Clear()
            Me.lvProcess.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub lvWindows_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvWindows.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvWindows)
    End Sub

    Private Sub lvWindows_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvWindows.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvWindows.SelectedItems IsNot Nothing _
                    AndAlso Me.lvWindows.SelectedItems.Count > 0)
            Me.MenuItemWindowSelProc.Enabled = selectionIsNotNothing
            Me.MenuItemCopyWindow.Enabled = selectionIsNotNothing

            Me.MenuItemWClose.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemWDisab.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemWEnable.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemWHide.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemWShow.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemWVisibility.Enabled = selectionIsNotNothing AndAlso _notWMI

            Me.mnuWindow.Show(Me.lvWindows, e.Location)
        End If
    End Sub

    Private Sub lvWindows_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvWindows.SelectedIndexChanged
        ' New window selected
        If lvWindows.SelectedItems.Count = 1 Then

            Try
                Dim cP As cWindow = Me.lvWindows.GetSelectedItem

                ' Description
                Dim s As String = ""
                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Window properties\b0\par"
                s = s & "\tab Window ID :\tab\tab\tab " & CStr(cP.Infos.Handle) & "\par"
                s = s & "\tab Process owner :\tab\tab\tab " & CStr(cP.Infos.ProcessId) & " -- " & cP.Infos.ProcessName & " -- thread : " & CStr(cP.Infos.ThreadId) & "\par"
                s = s & "\tab Caption :\tab\tab\tab " & cP.Caption & "\par"
                s = s & "\tab Enabled :\tab\tab\tab " & CStr(cP.Infos.Enabled) & "\par"
                s = s & "\tab Visible :\tab\tab\tab\tab " & CStr(cP.Infos.Visible) & "\par"
                s = s & "\tab IsTask :\tab\tab\tab\tab " & CStr(cP.Infos.IsTask) & "\par"
                s = s & "\tab Opacity :\tab\tab\tab " & CStr(cP.Infos.Opacity) & "\par"
                s = s & "\tab Height :\tab\tab\tab\tab " & CStr(cP.Infos.Height) & "\par"
                s = s & "\tab Left :\tab\tab\tab\tab " & CStr(cP.Infos.Left) & "\par"
                s = s & "\tab Top :\tab\tab\tab\tab " & CStr(cP.Infos.Top) & "\par"
                s = s & "\tab Width :\tab\tab\tab\tab " & CStr(cP.Infos.Width) & "\par"

                s = s & "}"

                rtb5.Rtf = s

            Catch ex As Exception
                Dim s As String = ""
                Dim er As Exception = ex

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                s = s & "\tab Message :\tab " & er.Message & "\par"
                s = s & "\tab Source :\tab\tab " & er.Source & "\par"
                If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                s = s & "}"

                rtb5.Rtf = s

            End Try

        End If
    End Sub

    Private Sub lvThreads_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvThreads.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvThreads)
    End Sub

    Private Sub lvThreads_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvThreads.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As System.Diagnostics.ThreadPriorityLevel

            If Me.lvThreads.SelectedItems.Count > 0 Then
                p = Me.lvThreads.GetSelectedItem.PriorityMod
            End If
            Me.MenuItemThIdle.Checked = (p = ThreadPriorityLevel.Idle)
            Me.MenuItemThLowest.Checked = (p = ThreadPriorityLevel.Lowest)
            Me.MenuItemThBNormal.Checked = (p = ThreadPriorityLevel.BelowNormal)
            Me.MenuItemThBNormal.Checked = (p = ThreadPriorityLevel.Normal)
            Me.MenuItemThANorm.Checked = (p = ThreadPriorityLevel.AboveNormal)
            Me.MenuItemThHighest.Checked = (p = ThreadPriorityLevel.Highest)
            Me.MenuItemThTimeCr.Checked = (p = ThreadPriorityLevel.TimeCritical)

            Dim selectionIsNotNothing As Boolean = (Me.lvThreads.SelectedItems IsNot Nothing _
                            AndAlso Me.lvThreads.SelectedItems.Count > 0)

            Me.MenuItemThAffinity.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThSelProc.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThSuspend.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThTerm.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThResu.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThPriority.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemCopyThread.Enabled = selectionIsNotNothing

            Me.mnuThread.Show(Me.lvThreads, e.Location)
        End If

    End Sub

    Private Sub lvThreads_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvThreads.SelectedIndexChanged
        ' New thread selected
        If lvThreads.SelectedItems.Count = 1 Then
            Dim cp As cThread = Me.lvThreads.GetSelectedItem

            Try

                ' Description
                Dim s As String = ""
                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Thread properties\b0\par"
                s = s & "\tab Thread ID :\tab\tab\tab " & cp.Infos.Id.ToString & "\par"
                s = s & "\tab Process owner :\tab\tab\tab " & cp.Infos.ProcessId.ToString & "\par" '& " -- " & cp.ProcessName & "\par"

                s = s & "\tab Priority :\tab\tab\tab\tab " & cp.Infos.Priority.ToString & "\par"
                s = s & "\tab Base priority :\tab\tab\tab " & CStr(cp.Infos.BasePriority) & "\par"
                s = s & "\tab State :\tab\tab\tab\tab " & cp.Infos.State.ToString & "\par"
                s = s & "\tab Wait reason :\tab\tab\tab " & cp.Infos.WaitReason.ToString & "\par"
                s = s & "\tab Start address :\tab\tab\tab " & "0x" & cp.Infos.StartAddress.ToString("x") & "\par"
                s = s & "\tab Start time :\tab\tab\tab " & cp.GetInformation("CreateTime") & "\par"
                s = s & "\tab TotalProcessorTime :\tab\tab " & cp.GetInformation("TotalTime") & "\par"
                s = s & "\tab KernelTime :\tab\tab\tab " & cp.GetInformation("KernelTime") & "\par"
                s = s & "\tab UserTime :\tab\tab\tab " & cp.GetInformation("UserTime") & "\par"
                s = s & "\tab ProcessorAffinity :\tab\tab " & CStr(cp.Infos.AffinityMask) & "\par"

                s = s & "}"

                rtb4.Rtf = s

            Catch ex As Exception
                Dim s As String = ""
                Dim er As Exception = ex

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                s = s & "\tab Message :\tab " & er.Message & "\par"
                s = s & "\tab Source :\tab\tab " & er.Source & "\par"
                If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                s = s & "}"

                rtb4.Rtf = s

            End Try

        End If
    End Sub

    Private Sub graphMonitor_OnZoom(ByVal leftVal As Integer, ByVal rightVal As Integer) Handles graphMonitor.OnZoom
        ' Change dates and set view as fixed (left and right)
        Try
            Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
            Dim l As New Date(it.MonitorCreationDate.Ticks + leftVal * 10000)
            Dim r As New Date(it.MonitorCreationDate.Ticks + rightVal * 10000)
            Me.dtMonitorL.Value = l
            Me.dtMonitorR.Value = r
            Me.chkMonitorLeftAuto.Checked = False
            Me.chkMonitorRightAuto.Checked = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub chkAllWindows_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllWindows.CheckedChanged
        Me.MenuItemWSUnnamed.Checked = Me.chkAllWindows.Checked
        If windowsToRefresh IsNot Nothing Then Call ShowWindows()
    End Sub

    Private Sub chkFileArchive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileArchive.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileArchive.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.Archive)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.Archive)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileArchive.Checked = Not (Me.chkFileArchive.Checked)
        End Try
    End Sub

    Private Sub chkFileHidden_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileHidden.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileHidden.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.Hidden)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.Hidden)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileHidden.Checked = Not (Me.chkFileHidden.Checked)
        End Try
    End Sub

    Private Sub chkFileReadOnly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileReadOnly.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileReadOnly.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.ReadOnly)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.ReadOnly)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileReadOnly.Checked = Not (Me.chkFileReadOnly.Checked)
        End Try
    End Sub

    Private Sub chkFileContentNotIndexed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileContentNotIndexed.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileContentNotIndexed.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.NotContentIndexed)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.NotContentIndexed)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileContentNotIndexed.Checked = Not (Me.chkFileContentNotIndexed.Checked)
        End Try
    End Sub

    Private Sub chkFileNormal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileNormal.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileNormal.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.Normal)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.Normal)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileNormal.Checked = Not (Me.chkFileNormal.Checked)
        End Try
    End Sub

    Private Sub chkFileSystem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileSystem.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileSystem.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.System)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.System)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileSystem.Checked = Not (Me.chkFileSystem.Checked)
        End Try
    End Sub

    Private Sub chkMonitorLeftAuto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMonitorLeftAuto.CheckedChanged
        Me.dtMonitorL.Enabled = Not (Me.chkMonitorLeftAuto.Checked)
        Me.txtMonitorNumber.Enabled = Not (Me.chkMonitorLeftAuto.Checked = False And Me.chkMonitorRightAuto.Checked = False)
        Me.lblMonitorMaxNumber.Enabled = Me.txtMonitorNumber.Enabled
    End Sub

    Private Sub chkMonitorRightAuto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMonitorRightAuto.CheckedChanged
        Me.dtMonitorR.Enabled = Not (Me.chkMonitorRightAuto.Checked)
        Me.txtMonitorNumber.Enabled = Not (Me.chkMonitorLeftAuto.Checked = False And Me.chkMonitorRightAuto.Checked = False)
        Me.lblMonitorMaxNumber.Enabled = Me.txtMonitorNumber.Enabled
    End Sub

    Private Sub chkSearchProcess_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSearchProcess.CheckedChanged
        Me.chkSearchModules.Enabled = (Me.chkSearchProcess.Checked)
        Me.chkSearchEnvVar.Enabled = (Me.chkSearchProcess.Checked)
    End Sub

    Private Sub cmdCopyServiceToCp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopyServiceToCp.Click
        If Me.rtb2.Text.Length > 0 Then
            My.Computer.Clipboard.SetText(Me.rtb2.Text, TextDataFormat.Text)
        End If
    End Sub

    Private Sub cmdCopyServiceToCp_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdCopyServiceToCp.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.rtb2.Rtf.Length > 0 Then
                My.Computer.Clipboard.SetText(Me.rtb2.Rtf, TextDataFormat.Rtf)
            End If
        End If
    End Sub

    Private Sub cmdFileClipboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileClipboard.Click
        If Me.rtb3.Text.Length > 0 Then
            My.Computer.Clipboard.SetText(Me.rtb3.Text, TextDataFormat.Text)
        End If
    End Sub

    Private Sub cmdFileClipboard_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdFileClipboard.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.rtb3.Rtf.Length > 0 Then
                My.Computer.Clipboard.SetText(Me.rtb3.Rtf, TextDataFormat.Rtf)
            End If
        End If
    End Sub

    Private Sub cmdSetFileDates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSetFileDates.Click
        ' Set new dates
        Try
            cSelFile.CreationTime = Me.DTcreation.Value
            cSelFile.LastAccessTime = Me.DTlastAccess.Value
            cSelFile.LastWriteTime = Me.DTlastModification.Value
            MsgBox("Done.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Date change ok")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Date change failed")
        End Try
    End Sub

    Private Sub dtMonitorL_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtMonitorL.ValueChanged
        If Me.chkMonitorLeftAuto.Checked = False Then
            Call tvMonitor_AfterSelect(Nothing, Nothing)
        End If
    End Sub

    Private Sub dtMonitorR_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtMonitorR.ValueChanged
        If Me.chkMonitorRightAuto.Checked = False Then
            Call tvMonitor_AfterSelect(Nothing, Nothing)
        End If
    End Sub

    Private Sub txtSearchThread_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchThread.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvThreads.Items
            If InStr(LCase(it.Text), LCase(Me.txtSearchThread.Text)) = 0 Then
                it.Group = lvThreads.Groups(0)
            Else
                it.Group = lvThreads.Groups(1)
            End If
        Next
        Me.lblThreadResults.Text = CStr(lvThreads.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblThreadResults_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblThreadResults.Click
        If Me.lvThreads.Groups(1).Items.Count > 0 Then
            Me.lvThreads.Focus()
            Me.lvThreads.EnsureVisible(Me.lvThreads.Groups(1).Items(0).Index)
            Me.lvThreads.SelectedItems.Clear()
            Me.lvThreads.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub txtSearchHandle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchHandle.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvHandles.Items
            If InStr(LCase(it.SubItems(1).Text), LCase(Me.txtSearchHandle.Text)) = 0 Then
                it.Group = lvHandles.Groups(0)
            Else
                it.Group = lvHandles.Groups(1)
            End If
        Next
        Me.lblHandlesCount.Text = CStr(Me.lvHandles.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblHandlesCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblHandlesCount.Click
        If Me.lvHandles.Groups(1).Items.Count > 0 Then
            Me.lvHandles.Focus()
            Me.lvHandles.EnsureVisible(Me.lvHandles.Groups(1).Items(0).Index)
            Me.lvHandles.SelectedItems.Clear()
            Me.lvHandles.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub txtSearchModule_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchModule.TextChanged
        For Each it As ListViewItem In Me.lvModules.Items
            Dim cM As cModule = Me.lvModules.GetItemByKey(it.Name)
            If cM IsNot Nothing Then
                If cM.Infos.FileInfo IsNot Nothing AndAlso InStr(LCase(cM.Infos.FileInfo.FileName), LCase(Me.txtSearchModule.Text)) = 0 And _
                        InStr(LCase(cM.Infos.FileInfo.FileVersion), LCase(Me.txtSearchModule.Text)) = 0 And _
                        InStr(LCase(cM.Infos.FileInfo.FileDescription), LCase(Me.txtSearchModule.Text)) = 0 And _
                        InStr(LCase(cM.Infos.FileInfo.CompanyName), LCase(Me.txtSearchModule.Text)) = 0 Then
                    it.Group = lvModules.Groups(0)
                Else
                    it.Group = lvModules.Groups(1)
                End If
            End If
        Next
        Me.lblModulesCount.Text = CStr(lvModules.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblModulesCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblModulesCount.Click
        If Me.lvModules.Groups(1).Items.Count > 0 Then
            Me.lvModules.Focus()
            Me.lvModules.EnsureVisible(Me.lvModules.Groups(1).Items(0).Index)
            Me.lvModules.SelectedItems.Clear()
            Me.lvModules.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub lvHandles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHandles.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvHandles)
    End Sub

    Private Sub txtSearchWindow_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchWindow.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvWindows.Items
            Dim cW As cWindow = Me.lvWindows.GetItemByKey(it.Name)
            If InStr(LCase(cW.Infos.Caption), LCase(Me.txtSearchWindow.Text)) = 0 Then
                it.Group = lvWindows.Groups(0)
            Else
                it.Group = lvWindows.Groups(1)
            End If
        Next
        Me.lblWindowsCount.Text = CStr(lvWindows.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblWindowsCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblWindowsCount.Click
        If Me.lvWindows.Groups(1).Items.Count > 0 Then
            Me.lvWindows.Focus()
            Me.lvWindows.EnsureVisible(Me.lvWindows.Groups(1).Items(0).Index)
            Me.lvWindows.SelectedItems.Clear()
            Me.lvWindows.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub txtSearchResults_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSearchResults.MouseDown
        Dim it As ListViewItem
        For Each it In Me.lvSearchResults.Items
            If InStr(LCase(it.SubItems(1).Text), LCase(Me.txtSearchResults.Text)) = 0 Then
                it.Group = lvSearchResults.Groups(0)
            Else
                it.Group = lvSearchResults.Groups(1)
            End If
        Next
        Me.lblResultsCount.Text = CStr(lvSearchResults.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub txtSearchResults_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchResults.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvSearchResults.Items
            If InStr(LCase(it.SubItems(1).Text), LCase(Me.txtSearchResults.Text)) = 0 Then
                it.Group = lvSearchResults.Groups(0)
            Else
                it.Group = lvSearchResults.Groups(1)
            End If
        Next
        Me.lblResultsCount.Text = CStr(lvSearchResults.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblResultsCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResultsCount.Click
        If Me.lvSearchResults.Groups(1).Items.Count > 0 Then
            Me.lvSearchResults.Focus()
            Me.lvSearchResults.EnsureVisible(Me.lvSearchResults.Groups(1).Items(0).Index)
            Me.lvSearchResults.SelectedItems.Clear()
            Me.lvSearchResults.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub _tab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tab.SelectedIndexChanged
        Static bHelpShown As Boolean = False

        ' Hide specific menus
        If My.Settings.UseRibbonStyle = False Then
            Me.MenuItemProcesses.Visible = False
            Me.MenuItemThreads.Visible = False
            Me.MenuItemModules.Visible = False
            Me.MenuItemHandles.Visible = False
            Me.MenuItemWindows.Visible = False
            Me.MenuItemMonitor.Visible = False
            Me.MenuItemServices.Visible = False
            Me.MenuItemFiles.Visible = False
            Me.MenuItemSearch.Visible = False
            Me.MenuItemJobs.Visible = False
        End If

        ' Change current tab of ribbon
        Dim theTab As RibbonTab = Me.HelpTab
        Select Case _tab.SelectedIndex
            Case 0
                My.Settings.SelectedTab = "Tasks"
                theTab = Me.TaskTab
            Case 1
                My.Settings.SelectedTab = "Processes"
                theTab = Me.ProcessTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemProcesses.Visible = True
                End If
            Case 2
                My.Settings.SelectedTab = "Jobs"
                theTab = Me.JobTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemJobs.Visible = True
                End If
            Case 3
                My.Settings.SelectedTab = "Modules"
                theTab = Me.ModulesTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemModules.Visible = True
                End If
            Case 4
                My.Settings.SelectedTab = "Threads"
                theTab = Me.ThreadTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemThreads.Visible = True
                End If
            Case 5
                My.Settings.SelectedTab = "Handles"
                theTab = Me.HandlesTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemHandles.Visible = True
                End If
            Case 6
                My.Settings.SelectedTab = "Windows"
                theTab = Me.WindowTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemWindows.Visible = True
                End If
            Case 7
                My.Settings.SelectedTab = "Monitor"
                theTab = Me.MonitorTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemMonitor.Visible = True
                End If
            Case 8
                My.Settings.SelectedTab = "Services"
                theTab = Me.ServiceTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemServices.Visible = True
                End If
            Case 9
                My.Settings.SelectedTab = "Network"
                theTab = Me.NetworkTab
            Case 10
                My.Settings.SelectedTab = "File"
                theTab = Me.FileTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemFiles.Visible = True
                End If
            Case 11
                My.Settings.SelectedTab = "Search"
                theTab = Me.SearchTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemSearch.Visible = True
                End If
            Case 12
                My.Settings.SelectedTab = "Help"
                theTab = Me.HelpTab
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                If Not (bHelpShown) Then
                    bHelpShown = True
                    ' Load help file
                    Dim path As String = HELP_PATH
                    'If IO.File.Exists(path) = False Then
                    '    WBHelp.Document.Write("<body link=blue vlink=purple><span>Help file cannot be found. <p></span><span>Please download help file at <a href=" & Chr(34) & "http://sourceforge.net/projects/yaprocmon/" & Chr(34) & ">http://sourceforge.net/projects/yaprocmon</a> and save it in the Help directory.</span></body>")
                    'Else
                    WBHelp.Navigate(path)
                    'End If
                End If
                _tab.SelectedTab = Me.pageHelp
        End Select
        Me.Ribbon.ActiveTab = theTab
    End Sub

    Private Sub goSearch(ByVal ssearch As String)
        If ssearch IsNot Nothing AndAlso ssearch.Length > 0 Then
            With Me.lvSearchResults
                .CaseSensitive = Me.chkSearchCase.Checked
                .SearchString = ssearch
                Dim t As searchInfos.SearchInclude
                If Me.chkSearchEnvVar.Checked And Me.chkSearchEnvVar.Enabled Then
                    t = t Or searchInfos.SearchInclude.SearchEnvVar
                End If
                If Me.chkSearchHandles.Checked Then
                    t = t Or searchInfos.SearchInclude.SearchHandles
                End If
                If Me.chkSearchModules.Checked And Me.chkSearchModules.Enabled Then
                    t = t Or searchInfos.SearchInclude.SearchModules
                End If
                If Me.chkSearchProcess.Checked Then
                    t = t Or searchInfos.SearchInclude.SearchProcesses
                End If
                If Me.chkSearchServices.Checked Then
                    t = t Or searchInfos.SearchInclude.SearchServices
                End If
                If Me.chkSearchWindows.Checked Then
                    t = t Or searchInfos.SearchInclude.SearchWindows
                End If
                .Includes = t
                .ClearItems()
                Me.butSearchGo.Enabled = False
                Me.MenuItemSearchNew.Enabled = False
                .UpdateItems()
            End With
        End If
    End Sub

    Private Sub timerNetwork_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerNetwork.Tick
        Call refreshNetworkList()
    End Sub

    Private Sub timerStateBasedActions_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerStateBasedActions.Tick
        'TODO_ (sba)
        'Me.emStateBasedActions.ProcessActions(lvProcess.GetAllItems)
    End Sub

    'Private Sub emStateBasedActions_ExitRequested() Handles emStateBasedActions.ExitRequested
    '    Me.Close()
    'End Sub

    'Private Sub emStateBasedActions_LogRequested(ByRef process As cLocalProcess) Handles emStateBasedActions.LogRequested
    '    'Dim frm As New frmProcessInfo
    '    'frm.SetProcess(process)
    '    'frm.WindowState = FormWindowState.Minimized
    '    'frm.StartLog()
    '    'frm.tabProcess.SelectedTab = frm.TabPage14
    '    'frm.Show()
    '    'TODO_  (sba)
    'End Sub

    'Private Sub emStateBasedActions_NotifyAction(ByRef action As cBasedStateActionState, ByRef process As cLocalProcess) Handles emStateBasedActions.NotifyAction
    '    Dim proc As String = process.Name & " (" & process.Pid.ToString & ")"
    '    If action.Notify Then
    '        Me.Tray.ShowBalloonTip(2000, "State based action was raised", "Rule : " & action.RuleText & " , process : " & proc, ToolTipIcon.Info)
    '    End If

    '    ' Add to log
    '    Program.Log.AppendLine("State based action was raised -- Rule : " & action.RuleText & " , process : " & proc)
    'End Sub

    'Private Sub emStateBasedActions_SaveProcessListRequested(ByVal path As String) Handles emStateBasedActions.SaveProcessListRequested
    '    Try
    '        ' Create file report
    '        Dim c As String = vbNullString
    '        Dim stream As New System.IO.StreamWriter(path, False)
    '        Dim _count As Integer = Me.lvProcess.GetAllItems.Count
    '        For Each it As cProcess In Me.lvProcess.GetAllItems
    '            c = "Process : " & it.Name
    '            c &= vbTab & "PID : " & it.Pid.ToString
    '            c &= vbTab & "Path : " & it.Path
    '            c &= vbNewLine
    '            stream.Write(c)
    '        Next
    '        c = CStr(_count) & " result(s)"
    '        stream.Write(c)
    '        stream.Close()
    '    Catch
    '        '
    '    End Try
    'End Sub

    'Private Sub emStateBasedActions_SaveServiceListRequested(ByVal path As String) Handles emStateBasedActions.SaveServiceListRequested
    '    Try
    '        ' Create file report
    '        Dim c As String = vbNullString
    '        Dim stream As New System.IO.StreamWriter(path, False)
    '        Dim _count As Integer = Me.lvServices.GetAllItems.Count
    '        For Each it As cService In Me.lvServices.GetAllItems
    '            c = "Service : " & it.Name
    '            c &= vbTab & "Long name : " & it.LongName
    '            c &= vbTab & "Path : " & it.ImagePath
    '            c &= vbTab & "Process : " & it.ProcessName & " (" & it.ProcessID & ")"
    '            c &= vbNewLine
    '            stream.Write(c)
    '        Next
    '        c = CStr(_count) & " result(s)"
    '        stream.Write(c)
    '        stream.Close()
    '    Catch
    '        '
    '    End Try
    'End Sub

    Private Sub butNewProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNewProcess.Click
        If Program.Connection.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            cFile.ShowRunBox(Me.Handle, "Start a new process", "Enter the path of the process you want to start.")
        Else
            Dim sres As String = CInputBox("Enter the path of the process you want to start.", "Start a new process", "")
            If sres Is Nothing OrElse sres.Equals(String.Empty) Then Exit Sub
            cProcess.SharedRLStartNewProcess(sres)
        End If
    End Sub

    Private Sub butLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butLog.Click
        Call Me.MenuItemMainLog_Click(Nothing, Nothing)
    End Sub

    Private Sub butWindows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindows.Click
        Call Me.MenuItemSystemOpenedWindows_Click(Nothing, Nothing)
    End Sub

    Private Sub butSystemInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSystemInfo.Click
        Call Me.MenuItemMainSysInfo_Click(Nothing, Nothing)
    End Sub

    Private Sub butFindWindow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFindWindow.Click
        Call Me.MenuItemMainFindWindow_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuAbout.Click
        Call Me.MenuItemMainAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuEmergency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuEmergency.Click
        Call Me.MenuItemMainEmergencyH_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuSaveReport.Click
        Call Me.MenuItemMainReport_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuSBA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuSBA.Click
        Call Me.MenuItemMainSBA_Click(Nothing, Nothing)
    End Sub

    Private Sub butNetwork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNetwork.Click
        Call orbMenuNetwork_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuNetwork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuNetwork.Click
        If ConnectionForm.Visible Then
            ConnectionForm.Hide()
        Else
            ConnectionForm.TopMost = _frmMain.TopMost
            ConnectionForm.Show()
        End If
    End Sub

    Public Sub ConnectToMachine()

        _local = (Program.Connection.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (Program.Connection.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        ' Disable all refreshments
        Me.timerProcess.Enabled = False
        Me.timerServices.Enabled = False
        Me.timerMonitoring.Enabled = False
        Me.timerTask.Enabled = False
        Me.timerNetwork.Enabled = False
        Me.timerJobs.Enabled = False

        ' Clear all lvItems
        Me.lvProcess.ClearItems()
        Me.lvModules.ClearItems()
        Me.lvThreads.ClearItems()
        Me.lvSearchResults.ClearItems()
        Me.lvHandles.ClearItems()
        Me.lvWindows.ClearItems()
        Me.tv.ClearItems()
        Me.tv2.ClearItems()
        Me.lvTask.ClearItems()
        Me.lvServices.ClearItems()
        Me.lvNetwork.ClearItems()
        Me.lvJob.ClearItems()
        Me.rtb6.Text = ""

        ' Connect all lvItems
        Me.lvProcess.ConnectionObj = Program.Connection
        Me.lvThreads.ConnectionObj = Program.Connection
        Me.lvModules.ConnectionObj = Program.Connection
        Me.lvHandles.ConnectionObj = Program.Connection
        Me.lvServices.ConnectionObj = Program.Connection
        Me.lvWindows.ConnectionObj = Program.Connection
        Me.lvNetwork.ConnectionObj = Program.Connection
        Me.lvTask.ConnectionObj = Program.Connection
        Me.tv.ConnectionObj = Program.Connection
        Me.tv2.ConnectionObj = Program.Connection
        Me.lvJob.ConnectionObj = Program.Connection
        Me.lvSearchResults.ConnectionObj = Program.Connection
        _shutdownConnection.ConnectionObj = Program.Connection
        Try
            Program.Connection.Connect()
            _shutdownConnection.Connect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Can not connect")
            Exit Sub
        End Try

        Me.butServiceFileDetails.Enabled = _local
        Me.butServiceFileProp.Enabled = _local
        Me.butModuleViewModuleDep.Enabled = _local
        Me.butServiceOpenDir.Enabled = _local
        Me.butResumeProcess.Enabled = Me._notWMI
        Me.butStopProcess.Enabled = Me._notWMI
        Me.butProcessAffinity.Enabled = Me._notWMI
        Me.butProcessShowAll.Enabled = Me._notWMI
        Me.butProcessWindows.Enabled = Me._notWMI
        Me.butProcessAffinity.Enabled = Me._notWMI
        Me.butShowProcHandles.Enabled = Me._notWMI
        Me.butStopProcess.Enabled = Me._notWMI
        Me.butResumeProcess.Enabled = Me._notWMI
        Me.butModuleUnload.Enabled = Me._notWMI
        Me.RBHandlesActions.Enabled = Me._notWMI
        Me.RBHandlesReport.Enabled = Me._notWMI
        Me.RBWindowActions.Enabled = Me._notWMI
        Me.RBWindowCapture.Enabled = Me._notWMI
        Me.RBWindowRefresh.Enabled = Me._notWMI
        Me.RBWindowReport.Enabled = Me._notWMI
        Me.pageJobs.Enabled = _local AndAlso (cEnvironment.HasYAPMDebugPrivilege)
        Me.RBJobActions.Enabled = Me.pageJobs.Enabled
        Me.RBJobDisplay.Enabled = Me.pageJobs.Enabled
        Me.pageHandles.Enabled = _notWMI
        Me.pageNetwork.Enabled = _notWMI
        Me.pageTasks.Enabled = _notWMI
        Me.pageWindows.Enabled = _notWMI
        Me.pageSearch.Enabled = _notWMI
        Me.RBNetworkRefresh.Enabled = _notWMI
        Me.RBSearchMain.Enabled = _notWMI
        Me.RBTaskActions.Enabled = _notWMI
        Me.RBTaskDisplay.Enabled = _notWMI
        Me.RBServiceFile.Enabled = _notWMI
        Me.butProcessOtherActions.Enabled = _notWMI

        Me.lvProcess.CatchErrors = Not (_local)
        Me.lvModules.CatchErrors = Not (_local)
        Me.lvThreads.CatchErrors = Not (_local)
        Me.lvHandles.CatchErrors = Not (_local)
        Me.lvServices.CatchErrors = Not (_local)
        Me.lvSearchResults.CatchErrors = Not (_local)
        Me.lvTask.CatchErrors = Not (_local)
        Me.lvNetwork.CatchErrors = Not (_local)
        Me.lvJob.CatchErrors = Not (_local)

        ' Enable all refreshments
        Me.timerProcess.Enabled = _local
        Me.timerServices.Enabled = _local
        Me.timerMonitoring.Enabled = _local
        Me.timerNetwork.Enabled = _local
        Me.timerTask.Enabled = _local
        Me.timerJobs.Enabled = _local
    End Sub

    Public Sub DisconnectFromMachine()
        ' Close all frmInfo forms
        ' No ForEach but a simple For

        ' Disable all refreshments
        Me.timerProcess.Enabled = False
        Me.timerServices.Enabled = False
        Me.timerMonitoring.Enabled = False
        Me.timerTask.Enabled = False
        Me.timerNetwork.Enabled = False
        Me.timerJobs.Enabled = False

        ' Clear all lvItems
        Me.lvProcess.ClearItems()
        Me.lvModules.ClearItems()
        Me.lvSearchResults.ClearItems()
        Me.lvThreads.ClearItems()
        Me.lvHandles.ClearItems()
        Me.lvWindows.ClearItems()
        Me.lvTask.ClearItems()
        Me.lvServices.ClearItems()
        Me.lvNetwork.ClearItems()
        Me.lvJob.ClearItems()
        Me.rtb6.Text = ""

        For x As Integer = Application.OpenForms.Count - 1 To 0 Step -1
            Dim frm As Form = Application.OpenForms(x)
            If TypeOf frm Is frmProcessInfo OrElse TypeOf frm Is frmServiceInfo OrElse TypeOf frm Is frmJobInfo Then
                Try
                    frm.Close()
                Catch ex As Exception
                    '
                End Try
            End If
        Next
        Try
            Program.Connection.Disconnect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Can not disconnect")
            Program.Connection.DisconnectForce()
            Exit Sub
        End Try
    End Sub

    Private Sub butFeedBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFeedBack.Click
        frmTracker.TopMost = _frmMain.TopMost
        frmTracker.Show()
    End Sub

    Private Sub butHiddenProcesses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHiddenProcesses.Click
        frmHiddenProcesses.TopMost = _frmMain.TopMost
        frmHiddenProcesses.Show()
    End Sub

    Private Sub butServiceDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceDetails.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            Dim frm As New frmServiceInfo
            frm.SetService(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub butShowPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowPreferences.Click
        Dim frm As New frmPreferences
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub butExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butExit.Click
        Call Me.MenuItemMainExit_Click(Nothing, Nothing)
    End Sub

    Private Sub rtb3_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtb3.DragDrop
        Dim strFile As String = Nothing
        For Each file As String In CType(e.Data.GetData(DataFormats.FileDrop), String())
            If IO.File.Exists(file) Then
                strFile = file
            End If
        Next
        If strFile IsNot Nothing Then
            Call DisplayDetailsFile(strFile)
        End If
    End Sub

    Private Sub rtb3_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtb3.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub rtb3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb3.TextChanged
        Me.cmdFileClipboard.Enabled = (rtb3.Rtf.Length > 0)
    End Sub

    Private Sub butShowDepViewer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowDepViewer.Click
        Dim _depFrm As New frmDepViewerMain
        _depFrm.TopMost = _frmMain.TopMost
        _depFrm.Show()
    End Sub

    Private Sub butViewModuleDep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleViewModuleDep.Click
        For Each it As cModule In Me.lvModules.GetSelectedItems
            If System.IO.File.Exists(it.Infos.Path) Then
                Dim _depForm As New frmDepViewerMain
                With _depForm
                    .OpenReferences(it.Infos.Path)
                    .HideOpenMenu()
                    .TopMost = _frmMain.TopMost
                    .Show()
                End With
            End If
        Next
    End Sub

    Private Sub MenuItemHSelectAssociatedProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemHSelectAssociatedProcess.Click
        ' Select processes associated to selected handles results
        If Me.lvHandles.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it As cHandle In Me.lvHandles.GetSelectedItems
            Dim pid As Integer = it.Infos.ProcessID
            Dim it2 As ListViewItem
            For Each it2 In Me.lvProcess.Items
                Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                If cp IsNot Nothing AndAlso cp.Infos.Pid = pid Then
                    it2.Selected = True
                    it2.EnsureVisible()
                End If
            Next
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub MenuItemCloseHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCloseHandle.Click
        Call Me.butHandleClose_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemShowUnnamedHandles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemShowUnnamedHandles.Click
        MenuItemShowUnnamedHandles.Checked = Not (MenuItemShowUnnamedHandles.Checked)
    End Sub

    Private Sub MenuItemChooseColumnsHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemChooseColumnsHandle.Click
        Me.lvHandles.ChooseColumns()
    End Sub

    Private Sub lvHandles_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHandles.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvHandles.SelectedItems IsNot Nothing _
                AndAlso Me.lvHandles.SelectedItems.Count > 0)

            Me.MenuItemNavigateHandle.Enabled = (selectionIsNotNothing AndAlso _
                                                 Me.lvHandles.SelectedItems.Count = 1 AndAlso _
                                                 (Me.lvHandles.GetSelectedItem.Infos.Type = "Key" Or Me.lvHandles.GetSelectedItem.Infos.Type = "File"))
            Me.MenuItemCloseHandle.Enabled = selectionIsNotNothing
            Me.MenuItemHSelectAssociatedProcess.Enabled = selectionIsNotNothing
            Me.MenuItemCopyHandle.Enabled = selectionIsNotNothing

            Me.MenuItemNavigateHandle.Enabled = (Me.lvHandles.SelectedItems.Count = 1 _
                                         AndAlso _local AndAlso (Me.lvHandles.GetSelectedItem.Infos.Type = "Key" _
                                                 Or Me.lvHandles.GetSelectedItem.Infos.Type = "File"))

            Me.mnuHandle.Show(Me.lvHandles, e.Location)
        End If
    End Sub

    Private Sub MenuItemTaskShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskShow.Click
        Call butTaskShow_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemTaskMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskMax.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.Maximize()
        Next
    End Sub

    Private Sub MenuItemTaskMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskMin.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.Minimize()
        Next
    End Sub

    Private Sub MenuItemTaskEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskEnd.Click
        Call butTaskEndTask_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemTaskSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskSelProc.Click
        ' Select processes associated to selected windows
        If Me.lvTask.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it As cTask In Me.lvTask.GetSelectedItems
            Dim pid As Integer = it.Infos.ProcessId
            Dim it2 As ListViewItem
            For Each it2 In Me.lvProcess.Items
                Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                If cp IsNot Nothing AndAlso cp.Infos.Pid = pid Then
                    it2.Selected = True
                    it2.EnsureVisible()
                End If
            Next
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
        Me.lvProcess.Focus()
    End Sub

    Private Sub MenuItemTaskSelWin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskSelWin.Click
        If Me.lvTask.SelectedItems.Count > 0 Then
            'Dim it As ListViewItem
            'Dim it2 As ListViewItem

            Dim x As Integer = 0
            ReDim windowsToRefresh(Me.lvTask.SelectedItems.Count - 1)

            For Each cw As cTask In Me.lvTask.GetSelectedItems
                ' May be some doublons in list, but don't care about that
                windowsToRefresh(x) = cw.Infos.ProcessId
                x += 1
            Next

            Call ShowWindows()

            ' Select windows
            ' TODO_
            'For Each it In Me.lvTask.SelectedItems
            '    Dim _h As IntPtr = Me.lvTask.GetItemByKey(it.Name).Handle
            '    For Each it2 In Me.lvWindows.Items
            '        If Me.lvWindows.GetItemByKey(it2.Name).Handle = _h Then
            '            it2.Selected = True
            '            it2.EnsureVisible()
            '        End If
            '    Next
            'Next

            Me.Ribbon.ActiveTab = Me.WindowTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemTaskColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskColumns.Click
        Me.lvTask.ChooseColumns()
    End Sub

    Private Sub MenuItemMonitorAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorAdd.Click
        Call butMonitoringAdd_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMonitorRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorRemove.Click
        Call butMonitoringRemove_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMonitorStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorStart.Click
        Call butMonitorStart_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMonitorStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorStop.Click
        Call butMonitorStop_Click(Nothing, Nothing)
    End Sub

    Private Sub lvTask_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvTask.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvTask.SelectedItems IsNot Nothing _
                                                    AndAlso Me.lvTask.SelectedItems.Count > 0)
            Me.MenuItemTaskEnd.Enabled = selectionIsNotNothing
            Me.MenuItemTaskSelProc.Enabled = selectionIsNotNothing AndAlso Me.lvProcess.SelectedItems.Count > 0
            Me.MenuItemTaskSelWin.Enabled = selectionIsNotNothing AndAlso Me.lvTask.SelectedItems.Count = 1
            Me.MenuItemTaskShow.Enabled = selectionIsNotNothing
            Me.MenuItemTaskMax.Enabled = selectionIsNotNothing
            Me.MenuItemTaskMin.Enabled = selectionIsNotNothing
            Me.MenuItemCopyTask.Enabled = selectionIsNotNothing
            Me.mnuTask.Show(Me.lvTask, e.Location)
        End If
    End Sub

    Private Sub tvMonitor_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvMonitor.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuMonitor.Show(Me.tvMonitor, e.Location)
        End If
    End Sub

    Private Sub MenuItemCopySmall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopySmall.Click
        My.Computer.Clipboard.SetImage(Me.pctFileSmall.Image)
    End Sub

    Private Sub MenuItemCopyBig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyBig.Click
        My.Computer.Clipboard.SetImage(Me.pctFileBig.Image)
    End Sub

    Private Sub pctFileBig_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctFileBig.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuFileCpPctBig.Show(Me.pctFileBig, e.Location)
        End If
    End Sub

    Private Sub pctFileSmall_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctFileSmall.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuFileCpPctSmall.Show(Me.pctFileSmall, e.Location)
        End If
    End Sub

    Private Sub MenuItemMainShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainShow.Click
        ' If ribbon is used, then the main form is to the left of the screen and
        ' not shown in taskbar
        If My.Settings.UseRibbonStyle Then
            If Me.Left = Pref.LEFT_POSITION_HIDDEN Then
                Me.CenterToScreen()
            End If
            Me.ShowInTaskbar = True
        End If
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.Show()
    End Sub

    Private Sub MenuItemMainToTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainToTray.Click
        Me.Hide()
        Me.Visible = False
    End Sub

    Private Sub MenuItemMainAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainAbout.Click
        Me.butAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMainAlwaysVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainAlwaysVisible.Click
        Call changeTopMost()
    End Sub

    Private Sub MenuItemMainRestart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainRestart.Click
        Call cSystem.Restart()
    End Sub

    Private Sub MenuItemMainShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainShutdown.Click
        Call cSystem.Shutdown()
    End Sub

    Private Sub MenuItemMainPowerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainPowerOff.Click
        Call cSystem.Poweroff()
    End Sub

    Private Sub MenuItemMainSleep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainSleep.Click
        Call cSystem.Sleep()
    End Sub

    Private Sub MenuItemMainHibernate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainHibernate.Click
        Call cSystem.Hibernate()
    End Sub

    Private Sub MenuItemMainLogOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainLogOff.Click
        Call cSystem.Logoff()
    End Sub

    Private Sub MenuItemMainLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainLock.Click
        Call cSystem.Lock()
    End Sub

    Private Sub MenuItemMainLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainLog.Click
        Program.Log.ShowForm = True
    End Sub

    Private Sub MenuItemMainReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainReport.Click
        frmGlobalReport.TopMost = _frmMain.TopMost
        frmGlobalReport.ShowDialog()
    End Sub

    Private Sub MenuItemMainSysInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainSysInfo.Click
        frmSystemInfo.TopMost = _frmMain.TopMost
        frmSystemInfo.Show()
    End Sub

    Private Sub MenuItemMainOpenedW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainOpenedW.Click
        frmWindowsList.BringToFront()
        frmWindowsList.WindowState = FormWindowState.Normal
        frmWindowsList.TopMost = _frmMain.TopMost
        frmWindowsList.Show()
    End Sub

    Private Sub MenuItemMainEmergencyH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainEmergencyH.Click
        frmHotkeys.BringToFront()
        frmHotkeys.WindowState = FormWindowState.Normal
        frmHotkeys.TopMost = _frmMain.TopMost
        frmHotkeys.Show()
    End Sub

    Private Sub MenuItemMainFindWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainFindWindow.Click
        frmFindWindow.TopMost = _frmMain.TopMost
        frmFindWindow.Show()
    End Sub

    Private Sub MenuItemMainSBA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainSBA.Click
        frmBasedStateAction.BringToFront()
        frmBasedStateAction.WindowState = FormWindowState.Normal
        frmBasedStateAction.TopMost = _frmMain.TopMost
        frmBasedStateAction.Show()
    End Sub

    Private Sub MenuItemRefProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRefProc.Click
        Me.MenuItemRefProc.Checked = Not (Me.MenuItemRefProc.Checked)
        Me.MenuItemSystemRefProc.Checked = Me.MenuItemRefProc.Checked
        Me.timerProcess.Enabled = Me.MenuItemRefProc.Checked
    End Sub

    Private Sub MenuItemMainRefServ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainRefServ.Click
        Me.MenuItemMainRefServ.Checked = Not (Me.MenuItemMainRefServ.Checked)
        Me.MenuItemSystemRefServ.Checked = Me.MenuItemMainRefServ.Checked
        Me.timerServices.Enabled = Me.MenuItemMainRefServ.Checked
    End Sub

    Private Sub MenuItemMainExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainExit.Click
        Call ExitYAPM()
    End Sub

    Private Sub MenuItemServSelService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSelService.Click
        ' Select processes associated to selected services results
        Dim it As ListViewItem
        Dim bOne As Boolean = False
        If Me.lvServices.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvServices.SelectedItems
            Dim tmp As cService = Me.lvServices.GetItemByKey(it.Name)
            If tmp IsNot Nothing Then
                Dim pid As Integer = tmp.Infos.ProcessId
                Dim it2 As ListViewItem
                For Each it2 In Me.lvProcess.Items
                    Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                    If cp IsNot Nothing AndAlso cp.Infos.Pid = pid And pid > 0 Then
                        it2.Selected = True
                        bOne = True
                        it2.EnsureVisible()
                    End If
                Next
            End If
        Next
        If bOne Then
            Me.Ribbon.ActiveTab = Me.ProcessTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemServFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServFileProp.Click
        Dim s As String = vbNullString
        For Each it As cService In Me.lvServices.GetSelectedItems
            Dim sP As String = it.GetInformation("ImagePath")
            If sP <> NO_INFO_RETRIEVED Then
                'TODO_
                s = sP  'cService.GetFileNameFromSpecial(sP)
                If IO.File.Exists(s) Then
                    cFile.ShowFileProperty(s, Me.Handle)
                Else
                    ' Cannot retrieve a good path
                    Dim box As New frmBox
                    With box
                        .txtMsg1.Text = "The file path cannot be extracted. Please edit it and then click 'OK' to open file properties box, or click 'Cancel' to cancel."
                        .txtMsg1.Height = 35
                        .txtMsg2.Top = 50
                        .txtMsg2.Height = 25
                        .txtMsg2.Text = s
                        .txtMsg2.ReadOnly = False
                        .txtMsg2.BackColor = Drawing.Color.White
                        .Text = "Show file properties box"
                        .Height = 150
                        .ShowDialog()
                        .TopMost = _frmMain.TopMost
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            If IO.File.Exists(.MsgResult2) Then _
                                cFile.ShowFileProperty(.MsgResult2, Me.Handle)
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub MenuItemServOpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServOpenDir.Click
        Dim s As String = vbNullString
        For Each it As cService In Me.lvServices.GetSelectedItems
            Dim sP As String = it.GetInformation("ImagePath")
            If sP <> NO_INFO_RETRIEVED Then
                s = cFile.GetParentDir(sP)
                If IO.Directory.Exists(s) Then
                    cFile.OpenADirectory(s)
                Else
                    ' Cannot retrieve a good path
                    Dim box As New frmBox
                    With box
                        .txtMsg1.Text = "The file directory cannot be extracted. Please edit it and then click 'OK' to open directory, or click 'Cancel' to cancel."
                        .txtMsg1.Height = 35
                        .txtMsg2.Top = 50
                        .txtMsg2.Height = 25
                        .txtMsg2.Text = s
                        .txtMsg2.ReadOnly = False
                        .txtMsg2.BackColor = Drawing.Color.White
                        .Text = "Open directory"
                        .Height = 150
                        .ShowDialog()
                        .TopMost = _frmMain.TopMost
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            If IO.Directory.Exists(.MsgResult2) Then
                                cFile.OpenADirectory(.MsgResult2)
                            End If
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub MenuItemServFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServFileDetails.Click
        Call Me.butServiceFileDetails_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSearch.Click
        Call Me.butServiceGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServDepe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDepe.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            If System.IO.File.Exists(it.Infos.ImagePath) Then
                Dim frm As New frmDepViewerMain
                frm.HideOpenMenu()
                frm.OpenReferences(it.Infos.ImagePath)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemServPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServPause.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            If it.Infos.State = Native.Api.NativeEnums.ServiceState.Running Then
                it.PauseService()
            Else
                it.ResumeService()
            End If
        Next

        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStop.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StopService()
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StartService()
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServAutoStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServAutoStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.AutoStart)
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServOnDemand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServOnDemand.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.DemandStart)
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServDisabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDisabled.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.StartDisabled)
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServReanalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServReanalize.Click
        Me.lvServices.ReAnalizeServices()
    End Sub

    Private Sub MenuItemServColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServColumns.Click
        Me.lvServices.ChooseColumns()
    End Sub

    Private Sub MenuItemServSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSelProc.Click
        ' Select processes associated to selected connections
        Dim it As ListViewItem
        If Me.lvNetwork.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvNetwork.SelectedItems
            Dim tmp As cNetwork = lvNetwork.GetItemByKey(it.Name)
            If tmp IsNot Nothing Then
                Dim pid As Integer = tmp.Infos.ProcessId
                Dim it2 As ListViewItem
                For Each it2 In Me.lvProcess.Items
                    Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                    If cp IsNot Nothing AndAlso cp.Infos.Pid = pid Then
                        it2.Selected = True
                        it2.EnsureVisible()
                    End If
                Next
            End If
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub menuCloseTCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkClose.Click
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            If it.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                it.CloseTCP()
            End If
        Next
    End Sub

    Private Sub MenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkColumns.Click
        Me.lvNetwork.ChooseColumns()
    End Sub

    Private Sub lvNetwork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvNetwork.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvNetwork.SelectedItems IsNot Nothing _
                AndAlso Me.lvNetwork.SelectedItems.Count > 0)

            Dim enable As Boolean = False
            For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
                If it.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                    If it.Infos.State <> Native.Api.Enums.MibTcpState.Listening AndAlso it.Infos.State <> Native.Api.Enums.MibTcpState.TimeWait AndAlso it.Infos.State <> Native.Api.Enums.MibTcpState.CloseWait Then
                        enable = True
                        Exit For
                    End If
                End If
            Next
            Me.MenuItemNetworkClose.Enabled = enable
            Me.MenuItemServSelProc.Enabled = selectionIsNotNothing
            Me.MenuItemCopyNetwork.Enabled = selectionIsNotNothing

            Me.mnuNetwork.Show(Me.lvNetwork, e.Location)
        End If
    End Sub

    Private Sub MenuItemThSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThSelProc.Click
        ' Select processes associated to selected threads results
        Dim it As ListViewItem
        Dim bOne As Boolean = False
        If Me.lvThreads.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvThreads.SelectedItems
            Dim tmp As cThread = Me.lvThreads.GetItemByKey(it.Name)
            If tmp IsNot Nothing Then
                Dim pid As Integer = tmp.Infos.ProcessId
                Dim it2 As ListViewItem
                For Each it2 In Me.lvProcess.Items
                    Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                    If cp IsNot Nothing AndAlso cp.Infos.Pid = pid And pid > 0 Then
                        it2.Selected = True
                        it2.EnsureVisible()
                        bOne = True
                    End If
                Next
            End If
        Next

        If bOne Then
            Me.Ribbon.ActiveTab = Me.ProcessTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemThTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThTerm.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadTerminate()
        Next
    End Sub

    Private Sub MenuItemThSuspend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThSuspend.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadSuspend()
        Next
    End Sub

    Private Sub MenuItemThResu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThResu.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadResume()
        Next
    End Sub

    Private Sub MenuItemThIdle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThIdle.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Idle)
        Next
    End Sub

    Private Sub MenuItemThLowest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThLowest.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Lowest)
        Next
    End Sub

    Private Sub MenuItemThBNormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThBNormal.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.BelowNormal)
        Next
    End Sub

    Private Sub MenuItemThNorm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThNorm.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Normal)
        Next
    End Sub

    Private Sub MenuItemThANorm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThANorm.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.AboveNormal)
        Next
    End Sub

    Private Sub MenuItemThHighest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThHighest.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Highest)
        Next
    End Sub

    Private Sub MenuItemThTimeCr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThTimeCr.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.TimeCritical)
        Next
    End Sub

    Private Sub MenuItemThAffinity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThAffinity.Click
        If Me.lvThreads.SelectedItems.Count = 0 Then Exit Sub

        Dim c() As cThread
        ReDim c(Me.lvThreads.SelectedItems.Count - 1)
        Dim x As Integer = 0
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            c(x) = it
            x += 1
        Next

        Dim frm As New frmThreadAffinity
        frm.Thread = c
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub MenuItemThColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThColumns.Click
        Me.lvThreads.ChooseColumns()
    End Sub

    Private Sub MenuItemModuleSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemModuleSelProc.Click
        ' Select processes associated to selected modules
        Dim it As ListViewItem
        Dim bOne As Boolean = False
        If Me.lvModules.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvModules.SelectedItems
            Dim tmp As cModule = Me.lvModules.GetItemByKey(it.Name)
            If tmp IsNot Nothing Then
                Dim pid As Integer = tmp.Infos.ProcessId
                Dim it2 As ListViewItem
                For Each it2 In Me.lvProcess.Items
                    Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                    If cp IsNot Nothing AndAlso cp.Infos.Pid = pid And pid > 0 Then
                        it2.Selected = True
                        bOne = True
                        it2.EnsureVisible()
                    End If
                Next
            End If
        Next

        If bOne Then
            Me.Ribbon.ActiveTab = Me.ProcessTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemModuleFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemModuleFileProp.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.GetSelectedItem.Infos.Path
            If IO.File.Exists(s) Then
                cFile.ShowFileProperty(s, Me.Handle)
            End If
        End If
    End Sub

    Private Sub MenuItemModuleOpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemModuleOpenDir.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.GetSelectedItem.Infos.Path
            If IO.File.Exists(s) Then
                cFile.OpenDirectory(s)
            End If
        End If
    End Sub

    Private Sub MenuItemModuleFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemModuleFileDetails.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.GetSelectedItem.Infos.Path
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub MenuItemModuleSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemModuleSearch.Click
        Call butModuleGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemModuleDependencies_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemModuleDependencies.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.GetSelectedItem.Infos.Path
            If System.IO.File.Exists(s) Then
                Dim _depForm As New frmDepViewerMain
                With _depForm
                    .OpenReferences(s)
                    .HideOpenMenu()
                    .TopMost = _frmMain.TopMost
                    .Show()
                End With
            End If
        End If
    End Sub

    Private Sub MenuItemUnloadModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemUnloadModule.Click
        For Each it As cModule In Me.lvModules.GetSelectedItems
            it.UnloadModule()
        Next
        Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"
    End Sub

    Private Sub MenuItemColumnsModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemColumnsModule.Click
        Me.lvModules.ChooseColumns()
    End Sub

    Private Sub MenuItemWindowSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWindowSelProc.Click
        ' Select processes associated to selected windows
        Dim bOne As Boolean = False
        If Me.lvWindows.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            Dim pid As Integer = it.Infos.ProcessId
            Dim it2 As ListViewItem
            For Each it2 In Me.lvProcess.Items
                Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                If cp IsNot Nothing AndAlso cp.Infos.Pid = pid Then
                    it2.Selected = True
                    bOne = True
                    it2.EnsureVisible()
                End If
            Next
        Next

        If bOne Then
            Me.Ribbon.ActiveTab = Me.ProcessTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemWindowColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWindowColumns.Click
        Me.lvWindows.ChooseColumns()
    End Sub

    Private Sub MenuItemSearchNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSearchNew.Click
        Dim r As String = CInputBox("Enter the string you want to search", "String search")
        If r IsNot Nothing AndAlso Not (r.Equals(String.Empty)) Then
            Call goSearch(r)
        End If
    End Sub

    Private Sub MenuItemSearchSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSearchSel.Click
        ' Select processes associated to selected search results
        If Me.lvSearchResults.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it As searchInfos In Me.lvSearchResults.GetSelectedItems
            Try
                If it.Type = searchInfos.ResultType.Service Then
                    ' Select service
                    Dim sp As String = it.Service
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvServices.Items
                        Dim cp As cService = Me.lvServices.GetItemByKey(it2.Name)
                        If cp IsNot Nothing AndAlso cp.Infos.Name = sp Then
                            it2.Selected = True
                            it2.EnsureVisible()
                        End If
                    Next
                    Me.Ribbon.ActiveTab = Me.ServiceTab
                Else
                    ' Select process
                    Dim i As Integer = it.ProcessId
                    If i > 0 Then
                        Dim it2 As ListViewItem
                        For Each it2 In Me.lvProcess.Items
                            Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                            If cp IsNot Nothing AndAlso cp.Infos.Pid = i Then
                                it2.Selected = True
                                it2.EnsureVisible()
                            End If
                        Next
                    End If
                    Me.Ribbon.ActiveTab = Me.ProcessTab
                End If
            Catch ex As Exception
                '
            End Try
        Next

        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSearchClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSearchClose.Click
        ' Close selected items
        If My.Settings.WarnDangerousActions Then
            If IsWindowsVistaOrAbove() Then
                If ShowVistaMessage(Me.Handle, "Dangerous action", _
                                    "Are you sure you want to close these items ?", _
                                    "This will close handles, unload module, stop service, kill process or close window depending on the selected object.", _
                                    TaskDialogCommonButtons.Yes Or _
                                    TaskDialogCommonButtons.No, _
                                    TaskDialogIcon.ShieldWarning) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            Else
                If MsgBox("Are you sure you want to close these items ?", _
                          MsgBoxStyle.Information Or MsgBoxStyle.YesNo, _
                          "Dangerous action") <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
            End If
        End If
        For Each it As searchInfos In Me.lvSearchResults.GetSelectedItems

            If it.Type = searchInfos.ResultType.Process Then
                cProcess.SharedLRKill(it.ProcessId)

            ElseIf it.Type = searchInfos.ResultType.Service Then
                Native.Objects.Service.GetServiceByName(it.Service).StopService()

            ElseIf it.Type = searchInfos.ResultType.Window Then
                'TODO_

            ElseIf it.Type = searchInfos.ResultType.Module Then
                cModule.SharedLRUnloadModule(it.ProcessId, it.ModuleName, it.PebAddress) 'OK

            ElseIf it.Type = searchInfos.ResultType.Handle Then
                cHandle.SharedLRCloseHandle(it.ProcessId, it.Handle)
            End If

        Next
    End Sub

    Private Sub lvSearchResults_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSearchResults.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvSearchResults.SelectedItems IsNot Nothing _
                    AndAlso Me.lvSearchResults.SelectedItems.Count > 0)

            Me.MenuItemSearchClose.Enabled = selectionIsNotNothing
            Me.MenuItemSearchSel.Enabled = selectionIsNotNothing
            Me.MenuItemCopySearch.Enabled = selectionIsNotNothing

            Me.mnuSearch.Show(Me.lvSearchResults, e.Location)
        End If
    End Sub

    Private Sub MenuItemProcKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKill.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.Kill()
        Next
    End Sub

    Private Sub MenuItemProcKillT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKillT.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to kill these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.KillProcessTree()
        Next
    End Sub

    Private Sub MenuItemProcStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcStop.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to suspend these processes ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SuspendProcess()
        Next
    End Sub

    Private Sub MenuItemProcResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcResume.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.ResumeProcess()
        Next
    End Sub

    Private Sub MenuItemProcPIdle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPIdle.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.Idle)
        Next
    End Sub

    Private Sub MenuItemProcPBN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPBN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub MenuItemProcPN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub MenuItemProcPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPAN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.AboveNormal)
        Next
    End Sub

    Private Sub MenuItemProcPH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPH.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub MenuItemProcPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPRT.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.RealTime)
        Next
    End Sub

    Private Sub MenuItemProcWorkingSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcWSS.Click
        For Each _p As cProcess In Me.lvProcess.GetSelectedItems
            _p.EmptyWorkingSetSize()
        Next
    End Sub

    Private Sub MenuItemProcDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcDump.Click
        Dim _frm As New frmDumpFile
        _frm.TopMost = _frmMain.TopMost
        If _frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each cp As cProcess In Me.lvProcess.GetSelectedItems
                Dim _file As String = _frm.TargetDir & "\" & Date.Now.Ticks.ToString & "_" & cp.Infos.Name & ".dmp"
                Call cp.CreateDumpFile(_file, _frm.DumpOption)
            Next
        End If
    End Sub

    Private Sub MenuItemProcReanalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcReanalize.Click
        Me.lvProcess.ReAnalizeProcesses()
    End Sub

    Private Sub MenuItemProcSModules_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSModules.Click
        Call butProcessShowModules_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSThrea_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSThreads.Click
        Call Me.butProcessThreads_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSHandles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSHandles.Click
        Call Me.butShowProcHandles_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSWindows.Click
        Call butProcessWindows_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSServices.Click

        ' Refresh service list if necessary
        If Me.lvServices.Items.Count = 0 Then Call Me.refreshServiceList()

        ' Get selected processes pids
        Dim pid() As Integer
        ReDim pid(0)
        Dim x As Integer = -1
        For Each lvi As cProcess In Me.lvProcess.GetSelectedItems
            x += 1
            ReDim Preserve pid(x)
            pid(x) = lvi.Infos.Pid
        Next

        ' Get services names of all associated services
        'Dim name() As String
        'ReDim name(0)
        'x = -1
        Dim bAddedOneService As Boolean = False
        Dim bServRef As Boolean = Me.timerServices.Enabled
        Me.timerServices.Enabled = False            ' Lock service timer

        For Each lvi As ListViewItem In Me.lvServices.Items
            Dim cServ As cService = Me.lvServices.GetItemByKey(lvi.Name)
            Dim bToAdd As Boolean = False
            If cServ IsNot Nothing Then
                For Each _pid As Integer In pid
                    If cServ.Infos.ProcessId = _pid And _pid > 0 Then
                        bToAdd = True
                        Exit For
                    End If
                Next
            End If

            ' Then we select service
            If bToAdd Then
                If bAddedOneService = False Then
                    Me.lvServices.SelectedItems.Clear()
                    bAddedOneService = True
                End If
                lvi.Selected = True
                lvi.EnsureVisible()
            End If
        Next

        ' Unlock timer
        Me.timerServices.Enabled = bServRef

        If bAddedOneService Then
            Me.Ribbon.ActiveTab = Me.ServiceTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemProcSAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSAll.Click
        Call butProcessShowAll_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSFileProp.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If IO.File.Exists(cp.Infos.Path) Then
                cFile.ShowFileProperty(cp.Infos.Path, Me.Handle)
            End If
        Next
    End Sub

    Private Sub MenuItemProcSOpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSOpenDir.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If cp.Infos.Path <> NO_INFO_RETRIEVED Then
                cFile.OpenDirectory(cp.Infos.Path)
            End If
        Next
    End Sub

    Private Sub MenuItemProcSFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSFileDetails.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim cp As cProcess = Me.lvProcess.GetSelectedItem
            Dim s As String = cp.Infos.Path
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub MenuItemProcSSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSSearch.Click
        Call Me.butProcessGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSDep.Click
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            If System.IO.File.Exists(it.Infos.Path) Then
                Dim frm As New frmDepViewerMain
                frm.HideOpenMenu()
                frm.OpenReferences(it.Infos.Path)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemProcColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcColumns.Click
        Me.lvProcess.ChooseColumns()
    End Sub

    Private Sub MenuItemSystemRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRefresh.Click
        Select Case _tab.SelectedIndex
            Case 0
                Call Me.butTaskRefresh_Click(Nothing, Nothing)
            Case 1
                Call Me.butProcessRerfresh_Click(Nothing, Nothing)
            Case 2
                Call Me.butModuleRefresh_Click(Nothing, Nothing)
            Case 3
                Call Me.butThreadRefresh_Click(Nothing, Nothing)
            Case 4
                Call Me.butHandleRefresh_Click(Nothing, Nothing)
            Case 5
                Call Me.butWindowRefresh_Click(Nothing, Nothing)
            Case 7
                Call Me.butServiceRefresh_Click(Nothing, Nothing)
            Case 8
                Call Me.butNetworkRefresh_Click(Nothing, Nothing)
            Case 9
                Call Me.butFileRefresh_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub MenuItemSystemConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemConnection.Click
        Call orbMenuNetwork_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemLog.Click
        Call Me.MenuItemMainLog_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemReport.Click
        Call Me.MenuItemMainReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemInfos.Click
        Call Me.MenuItemMainSysInfo_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemOpenedWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemOpenedWindows.Click
        Call Me.MenuItemMainOpenedW_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemFindWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemFindWindow.Click
        Call Me.MenuItemMainFindWindow_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemEmergency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemEmergency.Click
        Call Me.MenuItemMainEmergencyH_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemSBA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemSBA.Click
        Call Me.MenuItemMainSBA_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemToTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemToTray.Click
        Call Me.MenuItemMainToTray_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemExit.Click
        Call Me.MenuItemMainExit_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemAlwaysVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemAlwaysVisible.Click
        Me.MenuItemMainAlwaysVisible_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemRefProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRefProc.Click
        Me.MenuItemRefProc_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemRefServ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRefServ.Click
        Call Me.MenuItemMainRefServ_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemOptions.Click
        Call Me.butPreferences_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemShowHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemShowHidden.Click
        Call butHiddenProcesses_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemDependency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemDependency.Click
        Call butShowDepViewer_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemRestart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRestart.Click
        Call cSystem.Restart()
    End Sub

    Private Sub MenuItemSystemShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemShutdown.Click
        Call cSystem.Shutdown()
    End Sub

    Private Sub MenuItemSystemPowerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemPowerOff.Click
        Call cSystem.Poweroff()
    End Sub

    Private Sub MenuItemSystemSleep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemSleep.Click
        Call cSystem.Sleep()
    End Sub

    Private Sub MenuItemSystemHIbernate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemHIbernate.Click
        Call cSystem.Hibernate()
    End Sub

    Private Sub MenuItemSystemLogoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemLogoff.Click
        Call cSystem.Logoff()
    End Sub

    Private Sub MenuItemSystemLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemLock.Click
        Call cSystem.Lock()
    End Sub

    Private Sub MenuItemSystemUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemUpdate.Click
        Call Me.butUpdate_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemDonation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemDonation.Click
        Call Me.butDonate_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemFeedBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemFeedBack.Click
        Call butFeedBack_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemSF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemSF.Click
        Call Me.butProjectPage_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemWebsite.Click
        Call Me.butWebite_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemDownloads_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemDownloads.Click
        Call Me.butDownload_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemHelp.Click
        Me._tab.SelectedIndex = 11    ' Help
    End Sub

    Private Sub MenuItemSystemAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemAbout.Click
        Call Me.butAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcAff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcAff.Click
        If Me.lvProcess.SelectedItems.Count = 0 Then Exit Sub

        Dim c() As cProcess
        ReDim c(Me.lvProcess.SelectedItems.Count - 1)
        Dim x As Integer = 0
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            c(x) = it
            x += 1
        Next

        Dim frm As New frmProcessAffinity(c)
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub MenuItemNavigateHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNavigateHandle.Click
        If Me.lvHandles.SelectedItems.Count > 0 Then
            Dim _tmp As cHandle = Me.lvHandles.GetSelectedItem
            If _tmp.Infos.Type = "File" Then
                Dim _sPath As String = _tmp.Infos.Name
                If IO.Directory.Exists(_sPath) Then
                    Shell("explorer.exe " & _sPath, AppWinStyle.NormalFocus)
                Else
                    _sPath = cFile.GetParentDir(_sPath)
                    If IO.Directory.Exists(_sPath) Then
                        Shell("explorer.exe " & _sPath, AppWinStyle.NormalFocus)
                    End If
                End If
            ElseIf _tmp.Infos.Type = "Key" Then
                Common.Misc.NavigateToRegedit(_tmp.Infos.Name)
            End If
        End If
    End Sub

#Region "Copy to clipboard menus"

    Private Sub MenuItemCopyHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cHandle In Me.lvHandles.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cService In Me.lvServices.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyThread_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cModule In Me.lvModules.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cJob In Me.lvJob.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyNetwork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As searchInfos In Me.lvSearchResults.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cTask In Me.lvTask.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

#End Region

    Private Sub orbStartElevated_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbStartElevated.Click
        ' Restart elevated
        Call cEnvironment.RestartElevated()
    End Sub

    Private Sub MenuItemRunAsAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRunAsAdmin.Click
        ' Restart elevated
        Call cEnvironment.RestartElevated()
    End Sub

    Private Sub timerStatus_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerStatus.Tick
        ' Update panels of status bar
        Try
            ' /!\ Here we refresh the informations about system
            ' RefreshInfo should not be called elsewhere
            Call Program.SystemInfo.RefreshInfo()

            Me.sbPanelConnection.Text = Program.Connection.ToString
            Me.sbPanelCpu.Text = "CPU : " & Common.Misc.GetFormatedPercentage(Program.SystemInfo.CpuUsage, 3, True) & " %"
            Me.sbPanelMemory.Text = "Phys. Memory : " & Common.Misc.GetFormatedPercentage(Program.SystemInfo.PhysicalMemoryPercentageUsage, 3, True) & " %"
            Me.sbPanelProcesses.Text = Me.lvProcess.Items.Count & " processes"
            Me.sbPanelServices.Text = Me.lvServices.Items.Count & " services"

        Catch ex As Exception
            '
        End Try

    End Sub

    Private Sub butShowAllPendingTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowAllPendingTasks.Click
        Dim frm As New frmPendingTasks
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub changeTopMost()
        Me.butAlwaysDisplay.Checked = Not (Me.butAlwaysDisplay.Checked)
        Me.MenuItemMainAlwaysVisible.Checked = Me.butAlwaysDisplay.Checked
        Me.TopMost = Me.butAlwaysDisplay.Checked

        For Each frm As Form In Application.OpenForms
            frm.TopMost = Me.TopMost
        Next
    End Sub

    Private Sub MenuItemShowPendingTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemShowPendingTasks.Click
        Call butShowAllPendingTasks_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportProcesses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportProcesses.Click
        Call Me.butSaveProcessReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportModules_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportModules.Click
        Call Me.butModulesSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportThreads_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportThreads.Click
        Call Me.butThreadSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportHandles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportHandles.Click
        Call Me.butHandlesSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportMonitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportMonitor.Click
        Call Me.butMonitorSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportWindows.Click
        Call Me.butWindowSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportServices.Click
        Call Me.butServiceReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemNewSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNewSearch.Click
        Call Me.MenuItemSearchNew_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportSearch.Click
        Call Me.butSearchSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileOpen.Click
        Call Me.butOpenFile_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileRelease.Click
        Call Me.butFileRelease_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileDelete.Click
        Call Me.butDeleteFile_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileTrash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileTrash.Click
        Call Me.butMoveFileToTrash_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileRename.Click
        Call Me.butFileRename_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileShellOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileShellOpen.Click
        Call Me.butFileOpen_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileMove.Click
        Call Me.butFileMove_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileCopy.Click
        Call Me.butFileCopy_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileEncrypt.Click
        Call Me.butFileEncrypt_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileDecrypt.Click
        Call Me.butFileDecrypt_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileStrings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileStrings.Click
        Call Me.butFileSeeStrings_Click(Nothing, Nothing)
    End Sub

    Private Sub butMonitorSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorSaveReport.Click
        'TODO
    End Sub

    Private Sub MenuItemWShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWShow.Click
        Call Me.butWindowShow_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWUnnamed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWSUnnamed.Click
        Me.chkAllWindows.Checked = Not (Me.chkAllWindows.Checked)
    End Sub

    Private Sub MenuItemWHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWHide.Click
        Call Me.butWindowHide_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWClose.Click
        Call Me.butWindowClose_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWEnalbe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWEnable.Click
        Call Me.butWindowEnable_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWDisable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWDisab.Click
        Call Me.butWindowDisable_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWFront.Click
        Call Me.butWindowBringToFront_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWNotFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWNotFront.Click
        Call Me.butWindowDoNotBringToFront_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWActive.Click
        Call Me.butWindowSetAsActive_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWForeground_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWForeground.Click
        Call Me.butWindowSetAsForeground_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWMin.Click
        Call Me.butWindowMinimize_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWMax.Click
        Call Me.butWindowMaximize_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWPos.Click
        Call Me.butWindowPositionSize_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWFlash.Click
        Call Me.butWindowFlash_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWStopFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWStopFlash.Click
        Call Me.butWindowStopFlashing_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWOpacity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWOpacity.Click
        Call Me.butWindowOpacity_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemWCaption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWCaption.Click
        Call Me.butWindowCaption_Click(Nothing, Nothing)
    End Sub

    Private Sub butProcessReduceWS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessReduceWS.Click
        Call Me.MenuItemProcWorkingSS_Click(Nothing, Nothing)
    End Sub

    Private Sub butProcessDumpF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessDumpF.Click
        Call Me.MenuItemProcDump_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemNotifAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifAll.Click
        Me.MenuItemNotifDS.Checked = True
        Me.MenuItemNotifNS.Checked = True
        Me.MenuItemNotifNP.Checked = True
        Me.MenuItemNotifTP.Checked = True
        My.Settings.NotifyNewProcesses = True
        My.Settings.NotifyNewServices = True
        My.Settings.NotifyDeletedServices = True
        My.Settings.NotifyTerminatedProcesses = True
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifNone.Click
        Me.MenuItemNotifDS.Checked = False
        Me.MenuItemNotifNS.Checked = False
        Me.MenuItemNotifNP.Checked = False
        Me.MenuItemNotifTP.Checked = False
        My.Settings.NotifyNewProcesses = False
        My.Settings.NotifyNewServices = False
        My.Settings.NotifyDeletedServices = False
        My.Settings.NotifyTerminatedProcesses = False
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifDS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifDS.Click
        Me.MenuItemNotifDS.Checked = Not (Me.MenuItemNotifDS.Checked)
        My.Settings.NotifyDeletedServices = Me.MenuItemNotifDS.Checked
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifNP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifNP.Click
        Me.MenuItemNotifNP.Checked = Not (Me.MenuItemNotifNP.Checked)
        My.Settings.NotifyNewProcesses = Me.MenuItemNotifNP.Checked
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifNS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifNS.Click
        Me.MenuItemNotifNS.Checked = Not (Me.MenuItemNotifNS.Checked)
        My.Settings.NotifyNewServices = Me.MenuItemNotifNS.Checked
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifTP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifTP.Click
        Me.MenuItemNotifTP.Checked = Not (Me.MenuItemNotifTP.Checked)
        My.Settings.NotifyTerminatedProcesses = Me.MenuItemNotifTP.Checked
        My.Settings.Save()
    End Sub

    Private Sub timerJobs_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerJobs.Tick

        Me.lvJob.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Jobs" Then
                Me.Text = "Yet Another (remote) Process Monitor -- " & CStr(Me.lvJob.Items.Count) & " jobs running"
            End If
        End If
    End Sub

    Private Sub lvJob_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvJob.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call butJobDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvJob_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvJob.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Call butJobDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvJob_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvJob.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvJob.SelectedItems IsNot Nothing _
                                        AndAlso Me.lvJob.SelectedItems.Count > 0)
            Me.MenuItemJobTerminate.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemCopyJob.Enabled = selectionIsNotNothing
            Me.mnuJob.Show(Me.lvJob, e.Location)
        End If
    End Sub

    Private Sub MenuItemJobTerminate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemJobTerminate.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to terminate these jobs ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each cJ As cJob In Me.lvJob.GetSelectedItems
            cJ.TerminateJob()
        Next
    End Sub

    Private Sub butJobRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobRefresh.Click
        Me.refreshJobList()
    End Sub

    Private Sub butJobTerminate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobTerminate.Click
        Call MenuItemJobTerminate_Click(Nothing, Nothing)
    End Sub

    Private Sub butJobDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobDetails.Click
        For Each it As cJob In Me.lvJob.GetSelectedItems
            Dim frm As New frmJobInfo
            frm.SetJob(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemJobMng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemJobMng.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Dim theJob As cJob = cJob.GetProcessJobById(cp.Infos.Pid)
            If theJob IsNot Nothing Then
                Dim frm As New frmJobInfo
                frm.SetJob(theJob)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemProcAddToJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcAddToJob.Click
        ' Add to job

        ' Get list of PIDs
        Dim pid As New List(Of Integer)
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            pid.Add(cp.Infos.Pid)
        Next

        Dim frm As New frmAddToJob(pid)
        frm.ShowDialog()

    End Sub

    Private Sub MenuItemProcKillByMethod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKillByMethod.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Dim frm As New frmKillProcessByMethod
            frm.ProcessToKill = cp
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub
End Class