' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Imports System.Runtime.InteropServices


Public Class frmMain

    Public cInfo As New cSystemInfo
    Private WithEvents creg As cRegMonitor
    Public log As New cLog
    Private curProc As cProcess

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
    End Function

    Private Enum LVM
        LVM_FIRST = &H1000
        LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54)
        LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55)
    End Enum

    Private Enum LVS_EX
        LVS_EX_GRIDLINES = &H1
        LVS_EX_SUBITEMIMAGES = &H2
        LVS_EX_CHECKBOXES = &H4
        LVS_EX_TRACKSELECT = &H8
        LVS_EX_HEADERDRAGDROP = &H10
        LVS_EX_FULLROWSELECT = &H20
        LVS_EX_ONECLICKACTIVATE = &H40
        LVS_EX_TWOCLICKACTIVATE = &H80
        LVS_EX_FLATSB = &H100
        LVS_EX_REGIONAL = &H200
        LVS_EX_INFOTIP = &H400
        LVS_EX_UNDERLINEHOT = &H800
        LVS_EX_UNDERLINECOLD = &H1000
        LVS_EX_MULTIWORKAREAS = &H2000
        LVS_EX_LABELTIP = &H4000
        LVS_EX_BORDERSELECT = &H8000
        LVS_EX_DOUBLEBUFFER = &H10000
        LVS_EX_HIDELABELS = &H20000
        LVS_EX_SINGLEROW = &H40000
        LVS_EX_SNAPTOGRID = &H80000
        LVS_EX_SIMPLESELECT = &H100000
    End Enum

    ' ========================================
    ' Private attributes
    ' ========================================
    Private m_SortingColumn As ColumnHeader
    Private bProcessHover As Boolean = True
    Private bServiceHover As Boolean = False
    Private bEnableJobs As Boolean = True
    Private _stopOnlineRetrieving As Boolean = False
    Private handlesToRefresh() As Integer
    Private threadsToRefresh() As Integer
    Private modulesToRefresh() As Integer
    Private windowsToRefresh() As Integer
    Private isAdmin As Boolean = False
    Private cSelFile As cFile


    ' ========================================
    ' Public attributes
    ' ========================================
    Public handles_Renamed As New clsOpenedHandles
    Public bAlwaysDisplay As Boolean = False
    Public Pref As New Pref


    ' ========================================
    ' Some API declaration
    ' ========================================
    Private Declare Function InvalidateRect Lib "user32" (ByVal hWnd As Integer, ByVal t As Integer, ByVal bErase As Integer) As Boolean
    Private Declare Function ValidateRect Lib "user32" (ByVal hWnd As Integer, ByVal t As Integer) As Boolean
    Private Declare Function GetTickCount Lib "kernel32" () As Integer
    'Private Declare Unicode Function SetWindowTheme Lib "uxtheme.dll" (ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function


    ' ========================================
    ' Constants
    ' ========================================

    ' NOT UP TO DATE : There is a Config.xml file for for each user, but in IDE the file should be located in Config dir
#If Not (CONFIG_INTO_APPDATA) Then
    Public PREF_PATH As String = My.Application.Info.DirectoryPath & "\config.xml"
#Else
    Public PREF_PATH As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\config.xml"
#End If

    Public HELP_PATH As String = My.Application.Info.DirectoryPath & "\Help\help.htm"
    Public Const DEFAULT_TIMER_INTERVAL_PROCESSES As Integer = 2500
    Private Const NO_INFO_RETRIEVED As String = "N/A"
    Public Const DEFAULT_TIMER_INTERVAL_SERVICES As Integer = 15000
    Public Const MSGFIRSTTIME As String = "This is the first time you run YAPM. Please remember that it is a beta3 version so there are some bugs and some missing functionnalities :-)" & vbNewLine & vbNewLine & "You should run YAPM as an administrator in order to fully control your processes. Please take care using this YAPM because you will be able to do some irreversible things if you kill or modify some system processes... Use it at your own risks !" & vbNewLine & vbNewLine & "Please let me know any of your ideas of improvement or new functionnalities in YAPM's sourceforge.net project page ('Help' pannel) :-)" & vbNewLine & vbNewLine & "This message won't be shown anymore :-)"


    ' ========================================
    ' Form functions
    ' ========================================

    ' Set double buffer property to a listview
    Public Sub DoubleBufferListView(ByRef lv As ListView)
        Dim styles As Integer = CInt(SendMessage(lv.Handle, LVM.LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0))
        styles += LVS_EX.LVS_EX_DOUBLEBUFFER Or LVS_EX.LVS_EX_BORDERSELECT
        SendMessage(lv.Handle, LVM.LVM_SETEXTENDEDLISTVIEWSTYLE, 0, styles)
    End Sub

    ' Refresh File informations
    Private Sub refreshFileInfos(ByVal file As String)

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
            Me.lstFileString.Items.Clear()
            Me.lstFileString.Items.Add("Click on 'Others->Show file strings' to retrieve file strings")

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

        Dim lvi As ListViewItem
        Dim exist As Boolean = False
        Dim serv() As cService
        Dim p As cService

        ReDim serv(0)
        cService.EnumServicesEx(serv)

        ' Refresh (or suppress) all services displayed in listview
        For Each lvi In Me.lvServices.Items

            ' Test if process exist
            For Each p In serv
                If p.Name = lvi.Text Then
                    exist = True
                    p.IsDisplayed = True
                    Exit For
                End If
            Next

            If exist = False Then
                ' Process no longer exists
                lvi.Remove()
            Else

                ' Refresh process informations
                exist = exist
            End If
            exist = False
        Next


        ' Add all non displayed services (new services)
        For Each p In serv

            If p.IsDisplayed = False Then

                p.IsDisplayed = True

                Dim it As New ListViewItem

                it.Text = p.Name
                it.ImageKey = "service"

                Dim lsub1 As New ListViewItem.ListViewSubItem
                Dim lsub2 As New ListViewItem.ListViewSubItem
                Dim lsub3 As New ListViewItem.ListViewSubItem
                Dim lsub4 As New ListViewItem.ListViewSubItem
                Dim lsub5 As New ListViewItem.ListViewSubItem
                Dim lsub6 As New ListViewItem.ListViewSubItem

                Dim path As String
                Try
                    path = p.ImagePath
                    path = Replace(path, Chr(34), vbNullString)
                Catch ex As Exception
                    path = ex.Message
                End Try


                lsub4.Text = path
                lsub2.Text = p.Status.ToString
                lsub6.Text = p.Type
                Try
                    lsub3.Text = p.ServiceStartType
                Catch ex As Exception
                    lsub3.Text = ex.Message
                End Try
                lsub1.Text = p.LongName
                'lsub5.Text = CStr(IIf(p.CanPauseAndContinue, "Pause/Continue ", "")) & _
                '            CStr(IIf(p.CanShutdown, "Shutdown ", "")) & _
                '            CStr(IIf(p.CanStop, "Stop ", ""))
                If p.ProcessID > 0 Then
                    lsub5.Text = CStr(p.ProcessID) & " -- " & cFile.GetFileName(cProcess.GetPath(p.ProcessID))
                End If

                it.SubItems.Add(lsub1)
                it.SubItems.Add(lsub2)
                it.SubItems.Add(lsub3)
                it.SubItems.Add(lsub4)
                it.SubItems.Add(lsub5)
                it.SubItems.Add(lsub6)

                it.Tag = p

                lvServices.Items.Add(it)
            End If

        Next

        ' Here we retrieve some informations for all our displayed services
        For Each lvi In Me.lvServices.Items
            Try
                Dim cS As cService = CType(lvi.Tag, cService)
                cS.Refresh()

                lvi.SubItems(2).Text = cS.Status.ToString
                'lvi.SubItems(5).Text = CStr(IIf(cS.CanPauseAndContinue, "Pause/Continue ", "")) & _
                '    CStr(IIf(cS.CanShutdown, "Shutdown ", "")) & _
                '    CStr(IIf(cS.CanStop, "Stop ", ""))
                Try
                    lvi.SubItems(3).Text = cS.ServiceStartType
                Catch ex As Exception
                    lvi.SubItems(3).Text = ex.Message
                End Try

            Catch ex As Exception
                '
            End Try
        Next

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            If Me.Ribbon.ActiveTab.Text = "Services" Then
                Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvServices.Items.Count) & " services running"
            End If
        End If
    End Sub

    ' Refresh process list in listview
    Public Sub refreshProcessList()

        Dim p As cProcess
        Dim proc() As cProcess
        Dim lvi As ListViewItem
        Dim x As Integer = 0
        Dim exist As Boolean = False

        Dim test As Integer = GetTickCount

        ' Here is the list of the differents columns :
        ' Name
        ' PID
        ' User
        ' Processor time
        ' Memory
        ' Threads
        ' Priority
        ' Path

        'ValidateRect(Me.lvProcess.Handle.ToInt32, 0)
        'Me.lvProcess.OverriddenDoubleBuffered = False
        ' Me.lvProcess.BeginUpdate()

        ReDim proc(0)
        cProcess.Enumerate(proc)

        ' Refresh (or suppress) all processes displayed in listview
        For Each lvi In Me.lvProcess.Items

            ' Test if process exist
            Dim cP As cProcess = CType(lvi.Tag, cProcess)
            For Each p In proc
                If p.Pid = cP.Pid And p.Name = cP.Name Then
                    exist = True
                    p.isDisplayed = True
                    Exit For
                End If
            Next

            If exist = False Then
                ' Process no longer exists
                log.AppendLine("Process " & CStr(cP.Pid) & " (" & cP.Path & ") killed")
                lvi.Remove()
            Else

                ' Refresh process informations
                exist = exist
            End If
            exist = False
        Next

        ' Add all non displayed processe (new processes)
        For Each p In proc

            If p.isDisplayed = False Then

                ' Add to log
                'If p.IntTag1 = 0 Then
                '    p.IntTag1 = 1
                'Else
                log.AppendLine("Process " & CStr(p.Pid) & " (" & p.Path & ") created")
                'End If

                p.isDisplayed = True

                ' Get the process name
                Dim o As String = p.Name
                Dim it As New ListViewItem

                If Len(o) > 0 Then

                    it.Text = o

                    Dim lsub1 As New ListViewItem.ListViewSubItem
                    lsub1.Text = CStr(p.Pid)

                    Dim lsub2 As New ListViewItem.ListViewSubItem
                    Dim lsub3 As New ListViewItem.ListViewSubItem
                    Dim lsub4 As New ListViewItem.ListViewSubItem
                    Dim lsub6 As New ListViewItem.ListViewSubItem
                    Dim lsub7 As New ListViewItem.ListViewSubItem
                    Dim lsub8 As New ListViewItem.ListViewSubItem

                    If p.Pid > 4 Then

                        lsub2.Text = p.UserName

                        Dim cp As New cProcess(p.Pid)
                        lsub8.Text = cp.StartTime.ToLongDateString & " -- " & cp.StartTime.ToLongTimeString


                        ' Add icon
                        Try

                            Dim fName As String = p.Path

                            If IO.File.Exists(fName) Then
                                Dim img As System.Drawing.Icon = GetIcon(fName, True)
                                imgProcess.Images.Add(fName, img)
                                it.ImageKey = fName
                                lsub7.Text = fName
                            Else
                                it.ImageKey = "noicon"
                                lsub7.Text = NO_INFO_RETRIEVED
                                it.ForeColor = Drawing.Color.Gray
                                lsub8.Text = NO_INFO_RETRIEVED
                            End If

                        Catch ex As Exception
                            it.ImageKey = "noicon"
                            lsub7.Text = NO_INFO_RETRIEVED
                            it.ForeColor = Drawing.Color.Gray
                            lsub8.Text = NO_INFO_RETRIEVED
                        End Try

                        ' Add new node to treeview
                        addNewProcessNode(p, it.ImageKey)

                    Else
                        lsub2.Text = "SYSTEM"
                        lsub3.Text = NO_INFO_RETRIEVED
                        lsub4.Text = NO_INFO_RETRIEVED
                        lsub6.Text = NO_INFO_RETRIEVED
                        lsub7.Text = NO_INFO_RETRIEVED
                        lsub8.Text = NO_INFO_RETRIEVED
                        it.ImageKey = "noIcon"
                        it.ForeColor = Drawing.Color.Gray
                    End If

                    it.SubItems.Add(lsub1)
                    it.SubItems.Add(lsub2)
                    it.SubItems.Add(lsub3)
                    it.SubItems.Add(lsub4)
                    it.SubItems.Add(lsub6)
                    it.SubItems.Add(lsub7)
                    it.SubItems.Add(lsub8)

                    it.Group = lvProcess.Groups(0)

                    it.Tag = New cProcess(p)

                    ' Choose color
                    Dim col As Color = Color.White

                    'If p.IsDebugged Then
                    '    Color = ColorBeingDebugged
                    '    'ElseIf Properties.Settings.[Default].VerifySignatures AndAlso Properties.Settings.[Default].ImposterNames.Contains(p.Name.ToLower()) AndAlso p.VerifyResult <> Win32.VerifyResult.Trusted AndAlso p.VerifyResult <> Win32.VerifyResult.TrustedInstaller Then
                    '    '    Return Properties.Settings.[Default].ColorPackedProcesses
                    '    'ElseIf Properties.Settings.[Default].VerifySignatures AndAlso p.VerifyResult <> Win32.VerifyResult.Trusted AndAlso p.VerifyResult <> Win32.VerifyResult.TrustedInstaller AndAlso p.VerifyResult <> Win32.VerifyResult.NoSignature Then
                    '    '    Return Properties.Settings.[Default].ColorPackedProcesses
                    'ElseIf p.IsDotNet Then
                    '    Color = ColorDotNetProcesses
                    'ElseIf p.IsPacked Then
                    '    Color = ColorPackedProcesses
                    '    'ElseIf Program.HackerWindow.ProcessServices.ContainsKey(p.Pid) AndAlso Program.HackerWindow.ProcessServices(p.Pid).Count > 0 Then
                    '    '    Return Properties.Settings.[Default].ColorServiceProcesses
                    '    'ElseIf p.ElevationType = Win32.TOKEN_ELEVATION_TYPE.TokenElevationTypeFull Then
                    '    '    Return Properties.Settings.[Default].ColorElevatedProcesses
                    'ElseIf p.UserName = "NT AUTHORITY\SYSTEM" Then
                    '    Color = colorSystemProcesses
                    '    'ElseIf p.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name Then
                    '    '    Return Properties.Settings.[Default].ColorOwnProcesses
                    'ElseIf p.IsInJob Then
                    '    Color = ColorJobProcesses
                    'Else
                    '    Color = SystemColors.Window
                    'End If


                    'If p.IsDotNet Then
                    '    col = Color.FromArgb(200, 222, 255, 0)
                    'ElseIf p.IsInJob Then
                    '    col = Color.FromArgb(200, 205, 133, 63)
                    'ElseIf p.IsDebugged Then
                    '    col = Color.FromArgb(200, 204, 187, 255)
                    'End If

                    it.BackColor = col

                    If Me.chkDisplayNAProcess.Checked = True OrElse p.Path <> NO_INFO_RETRIEVED Then
                        lvProcess.Items.Add(it)
                    End If
                End If
            End If

        Next

        test = GetTickCount
        ' Here we retrieve some informations for all our displayed processes
        For Each lvi In Me.lvProcess.Items

            Try
                Dim cP As cProcess = CType(lvi.Tag, cProcess)

                If cP.ProcessorCount = -1 Then
                    cP.ProcessorCount = Me.cInfo.ProcessorCount
                End If

                Dim isub As ListViewItem.ListViewSubItem
                Dim xxx As Integer = 0
                For Each isub In lvi.SubItems
                    Dim colName As String = Me.lvProcess.Columns.Item(xxx).Text
                    colName = colName.Replace("< ", "")
                    colName = colName.Replace("> ", "")
                    isub.Text = cP.GetInformation(colName)
                    xxx += 1
                Next

            Catch ex As Exception
                ' Access denied or ?
            End Try

        Next

        'InvalidateRect(Me.lvProcess.Handle.ToInt32, 0, 0)
        'Me.lvProcess.OverriddenDoubleBuffered = True
        '    Me.lvProcess.EndUpdate()


        ' Refresh informations about process
        If Not (Me.tabProcess.SelectedTab.Text = "Informations" Or _
            Me.tabProcess.SelectedTab.Text = "Token" Or _
            Me.tabProcess.SelectedTab.Text = "Services" Or _
            Me.tabProcess.SelectedTab.Text = "Memory") Then _
            Call lvProcess_SelectedIndexChanged(Nothing, Nothing)

        test = GetTickCount - test

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Processes" Or ss = "Monitor" Or ss = "Misc" Or ss = "Help" Or ss = "File" Then
                Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
            End If
        End If

    End Sub

    Private Sub timerProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcess.Tick
        refreshProcessList()
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            handles_Renamed.Close()
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Visible = True
        Call TakeFullPower()
        refreshProcessList()
        refreshServiceList()

        Application.EnableVisualStyles()
        'DoubleBufferListView(Me.lvProcess)
        'DoubleBufferListView(Me.lvHandles)
        'DoubleBufferListView(Me.lvJobs)
        'DoubleBufferListView(Me.lvModules)
        'DoubleBufferListView(Me.lvSearchResults)
        'DoubleBufferListView(Me.lvServices)
        'DoubleBufferListView(Me.lvThreads)
        'DoubleBufferListView(Me.lvWindows)
        SetWindowTheme(Me.lvProcess.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvProcMem.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvProcServices.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvHandles.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvJobs.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvPrivileges.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvWindows.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvSearchResults.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvModules.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvThreads.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvServices.Handle, "explorer", Nothing)
        SetWindowTheme(Me.tv.Handle, "explorer", Nothing)
        SetWindowTheme(Me.tv2.Handle, "explorer", Nothing)
        SetWindowTheme(Me.tvMonitor.Handle, "explorer", Nothing)

        With Me
            .lblServicePath.BackColor = .BackColor
        End With

        isAdmin = mdlPrivileges.IsAdministrator
        If isAdmin = False Then
            MsgBox("You are not logged as an administrator. You cannot retrieve informations for system processes.", MsgBoxStyle.Critical, "You are not part of administrator group")
        End If

        With Me.graphMonitor
            .ColorMemory1 = Color.Yellow
            .ColorMemory2 = Color.Red
            .ColorMemory3 = Color.Orange
        End With

        ' Create tooltips
        SetToolTip(Me.lblResCount, "Number of results. Click on the number to view results.")
        SetToolTip(Me.lblResCount2, "Number of results. Click on the number to view results.")
        SetToolTip(Me.txtSearch, "Enter text here to search a process.")
        SetToolTip(Me.txtServiceSearch, "Enter text here to search a service.")
        SetToolTip(Me.chkModules, "Check if you want to retrieve modules and threads infos when you click on listview.")
        SetToolTip(Me.chkModules, "Check if you want to retrieve online infos when you click on listview.")
        SetToolTip(Me.cmdInfosToClipB, "Copy process informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style).")
        SetToolTip(Me.cmdCopyServiceToCp, "Copy services informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style).")
        SetToolTip(Me.lblServicePath, "Path of the main executable of the service.")
        SetToolTip(Me.tv, "Selected service depends on these services.")
        SetToolTip(Me.tv2, "This services depend on selected service.")
        SetToolTip(Me.cmdTray, "Hide YAPM (double click on icon on tray to show main form).")
        SetToolTip(Me.chkSearchProcess, "Search in processes list.")
        SetToolTip(Me.chkSearchServices, "Search in services list.")
        SetToolTip(Me.chkSearchWindows, "Search in windows list.")
        SetToolTip(Me.chkSearchCase, "Case sensitive.")
        SetToolTip(Me.chkSearchModules, "Check also for processes modules.")
        SetToolTip(Me.chkHandles, "Check if you want to retrieve handles infos when you click on listview.")
        SetToolTip(Me.lstFileString, "List of strings in file. Right click to copy to clipboard. Middle click to refresh the list.")


        ' Load preferences
        Try
            Pref.Load()
            If Pref.firstTime Then
                MsgBox(MSGFIRSTTIME, MsgBoxStyle.Information, "Please read this")
                Pref.firstTime = False
                Pref.Save()
            End If
            Pref.Apply()
        Catch ex As Exception
            ' Preference file corrupted/missing
            MsgBox("Preference file is missing or corrupted and will be now recreated.", MsgBoxStyle.Critical, "Startup error")
            With Pref
                .lang = "English"
                .procIntervall = DEFAULT_TIMER_INTERVAL_PROCESSES
                .serviceIntervall = DEFAULT_TIMER_INTERVAL_SERVICES
                .startFullPower = False
                .startHidden = False
                .startJobs = True
                .startup = False
                .topmost = False
                MsgBox(MSGFIRSTTIME, MsgBoxStyle.Information, "Please read this")
                .Save()
                .Apply()
            End With
        End Try

        Me.timerMonitoring.Enabled = True
        Me.timerProcess.Enabled = True
        Me.timerProcPerf.Enabled = True
        Me.timerServices.Enabled = True

        Call Me.lvProcess.Focus()
        'System.Windows.Forms.SendKeys.Send("yapm.")
        Try
            Me.lvProcess.Items(Me.lvProcess.Items.Count - 1).Selected = True
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Call txtSearch_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
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

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.panelMain.Left = 5
        Me.panelMain.Top = 145
        Me.panelMain2.Left = 5
        Me.panelMain2.Top = 145
        Me.panelMain3.Left = 5
        Me.panelMain3.Top = 120
        Me.panelMain4.Left = 5
        Me.panelMain4.Top = 120
        Me.panelMain5.Left = 5
        Me.panelMain5.Top = 120
        Me.panelMain6.Left = 5
        Me.panelMain6.Top = 120
        Me.panelMain7.Left = 5
        Me.panelMain7.Top = 120
        Me.panelMain8.Left = 5
        Me.panelMain8.Top = 120
        Me.panelMain9.Left = 5
        Me.panelMain9.Top = 120
        Me.panelMain10.Left = 5
        Me.panelMain10.Top = 120
        Me.panelMain11.Left = 5
        Me.panelMain11.Top = 120

        Me.panelMenu.Top = 117
        Me.panelMenu.Left = 5
        Me.panelMenu2.Top = 117
        Me.panelMenu2.Left = 5

        ' Help resizement
        Me.panelMain4.Height = Me.Height - panelMain4.Top - 41
        Me.panelMain4.Width = Me.Width - panelMain4.Left - 20

        ' Jobs resizement
        Me.panelMain3.Height = Me.panelMain4.Height
        Me.panelMain3.Width = Me.panelMain4.Width - 2

        ' Search resizement
        Me.panelMain6.Height = Me.panelMain3.Height
        Me.panelMain6.Width = Me.panelMain3.Width

        ' Handles resizement
        Me.panelMain7.Height = Me.panelMain3.Height
        Me.panelMain7.Width = Me.panelMain3.Width

        ' Monitor resizement
        Me.panelMain8.Height = Me.panelMain3.Height
        Me.panelMain8.Width = Me.panelMain3.Width

        ' Threads resizement
        Me.panelMain9.Height = Me.panelMain3.Height
        Me.panelMain9.Width = Me.panelMain3.Width

        ' Windows resizement
        Me.panelMain10.Height = Me.panelMain3.Height
        Me.panelMain10.Width = Me.panelMain3.Width

        ' Modules resizement
        Me.panelMain11.Height = Me.panelMain3.Height
        Me.panelMain11.Width = Me.panelMain3.Width

        ' Process
        Me.panelMain.Height = Me.panelMain3.Height - 23
        Me.panelMain.Width = Me.panelMain3.Width

        Dim i As Integer = CInt((Me.Height - 250) / 2)
        Dim MepanelInfosHeight As Integer = CInt(IIf(i < 340, i, 340))
        Dim MepanelInfonWidth As Integer = Me.panelMain.Width


        ' File resizement
        Me.panelMain5.Height = Me.panelMain3.Height
        Me.panelMain5.Width = Me.panelMain3.Width
        Me.pctFileBig.Left = MepanelInfonWidth - 35
        Me.pctFileSmall.Left = MepanelInfonWidth - 57
        Me.cmdFileClipboard.Left = MepanelInfonWidth - 165
        Me.txtFile.Width = MepanelInfonWidth - 175
        Me.fileSplitContainer.Width = MepanelInfonWidth - 3
        Me.fileSplitContainer.Height = Me.panelMain5.Height - 42
        Me.lstFileString.Width = Me.fileSplitContainer.Width - Me.gpFileAttributes.Width - Me.gpFileDates.Width - 10

        ' Services

        Me.panelMain2.Height = Me.panelMain3.Height - 27 '- CInt(IIf(i < 210, i, 210)) - 187
        '  Me.panelInfos2.Top = Me.panelMain2.Top + Me.panelMain2.Height + 3
        Me.panelMain2.Width = Me.panelMain3.Width
        '  Me.panelInfos2.Width = MepanelInfonWidth

        'Me.lblServiceName.Width = Me.panelInfos2.Width - 140
        'Me.lblServicePath.Width = Me.lblServiceName.Width
        'Me.cmdCopyServiceToCp.Left = Me.panelInfos2.Width - 107
        'Me.tv2.Height = CInt((Me.panelInfos2.Height - 48) / 2)
        'Me.tv.Height = Me.tv2.Height
        'Me.tv.Top = Me.tv2.Top + 3 + Me.tv2.Height
        'Me.tv2.Left = Me.panelInfos2.Width - 151
        'Me.tv.Left = Me.tv2.Left
        'Me.rtb2.Height = Me.panelInfos2.Height - 45
        'Me.rtb2.Width = Me.panelInfos2.Width - 157

        ' Economize CPU :-)
        'If Me.WindowState = FormWindowState.Minimized Then
        '    Me.timerProcess.Enabled = False
        '    Me.timerServices.Enabled = False
        'Else
        '    Me.timerProcess.Enabled = True
        '    Me.timerServices.Enabled = True
        'End If

    End Sub

    Private Sub timerServices_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerServices.Tick
        refreshServiceList()
    End Sub

#Region "Powerfull recursives methods for treeviews"
    ' Recursive method to add items in our treeview
    Private Sub addDependentServices(ByVal o As System.ServiceProcess.ServiceController, ByVal n As TreeNode)
        Dim o1 As System.ServiceProcess.ServiceController
        For Each o1 In o.DependentServices
            Dim n2 As New TreeNode
            With n2
                .ImageKey = "service"
                .SelectedImageKey = "service"
                .Text = o1.ServiceName
            End With
            n.Nodes.Add(n2)
            addDependentServices(o1, n2)
        Next o1
    End Sub
    ' Recursive method to add items in our treeview
    Private Sub addServicesDependedOn(ByVal o As System.ServiceProcess.ServiceController, ByVal n As TreeNode)
        Dim o1 As System.ServiceProcess.ServiceController
        For Each o1 In o.ServicesDependedOn
            Dim n2 As New TreeNode
            With n2
                .ImageKey = "service"
                .SelectedImageKey = "service"
                .Text = o1.ServiceName
            End With
            n.Nodes.Add(n2)
            addServicesDependedOn(o1, n2)
        Next o1
    End Sub
#End Region

    Private Sub Tray_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Tray.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.Visible = True
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.Visible = True
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Me.butAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        My.Computer.Clipboard.SetImage(Me.pctBigIcon.Image)
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        My.Computer.Clipboard.SetImage(Me.pctSmallIcon.Image)
    End Sub

    Private Sub KillToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KillToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).Kill()
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SuspendProcess()
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub ResumeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResumeToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).ResumeProcess()
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub IdleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IdleToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.Idle)
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub BelowNormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BelowNormalToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.BelowNormal)
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub NormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.Normal)
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub AboveNormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboveNormalToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.AboveNormal)
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub HighToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HighToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.High)
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub RealTimeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RealTimeToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Try
                CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.RealTime)
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertiesToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Dim cp As cProcess = CType(it.Tag, cProcess)
            If IO.File.Exists(cp.Path) Then
                cFile.ShowFileProperty(cp.Path)
            End If
        Next
    End Sub

    Private Sub OpenFirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFirectoryToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Dim cp As cProcess = CType(it.Tag, cProcess)
            If cp.Path <> NO_INFO_RETRIEVED Then
                cFile.OpenDirectory(cp.Path)
            End If
        Next
    End Sub

    Private Sub lblResCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblResCount.Click
        Me.lvProcess.Focus()
        Try
            Me.lvProcess.EnsureVisible(Me.lvProcess.Groups(1).Items(0).Index)
            Me.lvProcess.SelectedItems.Clear()
            Me.lvProcess.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetToolTip(ByVal ctrl As Control, ByVal text As String)
        Dim tToolTip As ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        With tToolTip
            .SetToolTip(ctrl, text)
            .IsBalloon = True
            .Active = True
        End With
    End Sub

    Private Sub ToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem20.Click
        Dim it As ListViewItem
        Dim s As String = vbNullString
        For Each it In Me.lvServices.SelectedItems
            If it.SubItems(4).Text <> NO_INFO_RETRIEVED Then
                s = cService.GetFileNameFromSpecial(it.SubItems(4).Text)
                If IO.File.Exists(s) Then
                    cFile.ShowFileProperty(s)
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
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            If IO.File.Exists(.MsgResult2) Then _
                                cFile.ShowFileProperty(.MsgResult2)
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub ToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem21.Click
        Dim it As ListViewItem
        Dim s As String = vbNullString
        For Each it In Me.lvServices.SelectedItems
            If it.SubItems(4).Text <> NO_INFO_RETRIEVED Then
                s = cFile.GetParentDir(it.SubItems(4).Text)
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

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        Dim it As ListViewItem

        For Each it In Me.lvServices.SelectedItems
            Try
                If Me.ToolStripMenuItem9.Text = "Pause" Then
                    CType(it.Tag, cService).PauseService()
                Else
                    CType(it.Tag, cService).ResumeService()
                End If
            Catch ex As Exception
                '
            End Try
        Next

        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)

    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).StopService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).StartService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).SetServiceStartType(cService.TypeServiceStartType.SERVICE_DISABLED)
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).SetServiceStartType(cService.TypeServiceStartType.SERVICE_AUTO_START)
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).SetServiceStartType(cService.TypeServiceStartType.SERVICE_DEMAND_START)
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub ShutdownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutdownToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).ShutDownService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    'Private Sub timerJobs_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerJobs.Tick
    '    ' Job processing
    '    Dim it As ListViewItem
    '    Dim p As ListViewItem
    '    Dim tAction As String = vbNullString
    '    Dim tTime As String = vbNullString
    '    Dim tPid As Integer = 0
    '    Dim tProcess As String = vbNullString
    '    Dim tName As String = vbNullString

    '    For Each it In Me.lvJobs.Items
    '        ' Name
    '        ' ProcessID
    '        ' ProcessName
    '        ' Action
    '        ' Time
    '        With it
    '            tPid = CInt(.SubItems(1).Text)
    '            tProcess = .SubItems(2).Text
    '            tAction = .SubItems(3).Text
    '            tTime = .SubItems(4).Text
    '        End With

    '        ' Firstly, we check if time implies to process job now
    '        If tTime = "Every second" Or tTime = DateTime.Now.ToLongDateString & "-" & DateTime.Now.ToLongTimeString Then

    '            If tPid > 0 Then
    '                ' Check PID
    '                If Len(tProcess) > 0 Then
    '                    ' Check process name too
    '                    For Each p In lvProcess.Items
    '                        Dim cp As cProcess = CType(p.Tag, cProcess)
    '                        If cp.Name = tName And CInt(cp.Pid) = tPid Then
    '                            ProcessJob(tPid, tAction)
    '                        End If
    '                    Next
    '                Else
    '                    ' Check only pid
    '                    For Each p In lvProcess.Items
    '                        Dim cp As cProcess = CType(p.Tag, cProcess)
    '                        If CInt(cp.Pid) = tPid Then
    '                            ProcessJob(tPid, tAction)
    '                        End If
    '                    Next
    '                End If
    '            Else
    '                ' Check only process name
    '                For Each p In lvProcess.Items
    '                    Dim cp As cProcess = CType(p.Tag, cProcess)
    '                    If cp.Name = tProcess Then
    '                        ProcessJob(CInt(cp.Pid), tAction)
    '                    End If
    '                Next
    '            End If
    '        End If
    '    Next
    'End Sub

    '' Process a job
    'Private Sub ProcessJob(ByVal pid As Integer, ByVal action As String)
    '    Select Case action
    '        Case "Kill"
    '            'mdlProcess.Kill(pid)
    '        Case "Pause"
    '            'mdlProcess.SuspendProcess(pid)
    '        Case "Resume"
    '            'mdlProcess.ResumeProcess(pid)
    '    End Select
    'End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Static first As Boolean = True
        If first Then
            first = False
            If Pref.startHidden Then
                Me.Hide()
                Me.WindowState = FormWindowState.Minimized
            Else
                Me.Show()
                Me.WindowState = FormWindowState.Normal
            End If
        End If
    End Sub

    Private Sub butKill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butKillProcess.Click
        ' Kill selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).Kill()
        Next
    End Sub

    Private Sub butAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub butProcessRerfresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessRerfresh.Click
        Me.refreshProcessList()
    End Sub

    Private Sub butServiceRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceRefresh.Click
        Me.refreshServiceList()
    End Sub

    Private Sub butDonate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDonate.Click
        MsgBox("You will be redirected on my sourceforge.net donation page.", MsgBoxStyle.Information, "Donation procedure")
        cFile.ShellOpenFile("https://sourceforge.net/donate/index.php?user_id=1590933#donate")
    End Sub

    Private Sub butWebite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWebite.Click
        cFile.ShellOpenFile("http://yaprocmon.sourceforge.net/")
    End Sub

    Private Sub butProjectPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProjectPage.Click
        cFile.ShellOpenFile("http://sourceforge.net/projects/yaprocmon")
    End Sub

    Private Sub butServiceFileProp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceFileProp.Click
        Call ToolStripMenuItem20_Click(Nothing, Nothing)
    End Sub

    Private Sub butServiceOpenDir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceOpenDir.Click
        Call ToolStripMenuItem21_Click(Nothing, Nothing)
    End Sub

    Private Sub butStopProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStopProcess.Click
        ' Stop selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SuspendProcess()
        Next
    End Sub

    Private Sub butProcessAffinity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessAffinity.Click
        ' Choose affinity for selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            'INSERT CODE HERE
            'If cP.Path <> NO_INFO_RETRIEVED Then _
            'http://www.vbfrance.com/codes/AFFINITE-PROCESSUS-THREADS_42365.aspx
        Next
    End Sub

    Private Sub butResumeProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butResumeProcess.Click
        ' Resume selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).ResumeProcess()
        Next
    End Sub

    Private Sub butIdle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butIdle.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.Idle)
        Next
    End Sub

    Private Sub butHigh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHigh.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub butNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNormal.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub butRealTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRealTime.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.RealTime)
        Next
    End Sub

    Private Sub butBelowNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butBelowNormal.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub butAboveNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAboveNormal.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority(ProcessPriorityClass.AboveNormal)
        Next
    End Sub

    Private Sub butStopService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStopService.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).StopService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butStartService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStartService.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).StartService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butShutdownService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShutdownService.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).ShutDownService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butPauseService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPauseService.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).PauseService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butAutomaticStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAutomaticStart.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).SetServiceStartType(cService.TypeServiceStartType.SERVICE_AUTO_START)
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butDisabledStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDisabledStart.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).SetServiceStartType(cService.TypeServiceStartType.SERVICE_DISABLED)
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butOnDemandStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOnDemandStart.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).SetServiceStartType(cService.TypeServiceStartType.SERVICE_DEMAND_START)
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butResumeService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butResumeService.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Try
                CType(it.Tag, cService).ResumeService()
            Catch ex As Exception
                '
            End Try
        Next
        Call Me.refreshServiceList()
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub Ribbon_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Ribbon.MouseMove
        Static currentText As String = vbNullString
        Static bHelpShown As Boolean = False

        If Not (Ribbon.ActiveTab.Text = currentText) Then
            currentText = Ribbon.ActiveTab.Text
            Select Case currentText
                Case "Services"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvServices.Items.Count) & " services running"
                    Me.bProcessHover = False
                    Me.bServiceHover = True
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = True
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMain5.Visible = False
                    Me.chkModules.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = True
                    Me.panelMain2.BringToFront()
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                Case "Processes"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                    Me.bProcessHover = True
                    Me.bServiceHover = False
                    Me.panelMain.Visible = True
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.chkModules.Visible = True
                    Me.panelMenu.Visible = True
                    Me.panelMain.BringToFront()
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                Case "Jobs"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvJobs.Items.Count) & " jobs in list"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = True
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMain3.BringToFront()
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                Case "Help"

                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                    If Not (bHelpShown) Then
                        bHelpShown = True
                        ' Load help file
                        Dim path As String = HELP_PATH
                        If IO.File.Exists(path) = False Then
                            WBHelp.Document.Write("<body link=blue vlink=purple><span>Help file cannot be found. <p></span><span>Please download help file at <a href=" & Chr(34) & "http://sourceforge.net/projects/yaprocmon/" & Chr(34) & ">http://sourceforge.net/projects/yaprocmon</a> and save it in the Help directory.</span></body>")
                        Else
                            WBHelp.Navigate(path)
                        End If
                    End If

                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = True
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                    Me.panelMain4.BringToFront()
                Case "File"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = True
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain11.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain5.BringToFront()
                Case "Search"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvSearchResults.Items.Count) & " search results"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = True
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                    Me.panelMain6.BringToFront()
                Case "Handles"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvHandles.Items.Count) & " handles"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = True
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                    Me.panelMain7.BringToFront()
                Case "Monitor"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = True
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain8.BringToFront()
                    Me.panelMain11.Visible = False
                Case "Threads"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvThreads.Items.Count) & " threads"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.BringToFront()
                    Me.panelMain9.Visible = True
                    Me.panelMain10.Visible = False
                    Me.panelMain11.Visible = False
                Case "Windows"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvWindows.Items.Count) & " windows"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = True
                    Me.panelMain10.BringToFront()
                    Me.panelMain11.Visible = False
                Case "Modules"
                    Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMain8.BringToFront()
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                    Me.panelMain9.Visible = False
                    Me.panelMain10.Visible = False
                    Me.panelMain11.BringToFront()
                    Me.panelMain11.Visible = True
            End Select
        End If
    End Sub

    Private Sub cmdTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTray.Click
        Me.Hide()
        Me.Visible = False
    End Sub

    Private Sub butNewProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNewProcess.Click
        cFile.ShowRunBox(Me.Handle.ToInt32, "Start a new process", "Enter the path of the process you want to start.")
    End Sub

    Private Sub butDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDownload.Click
        cFile.ShellOpenFile("http://sourceforge.net/project/showfiles.php?group_id=244697")
    End Sub

    Private Sub frmMain_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        'Me.timerServices.Enabled = Me.Visible
        'Me.timerProcess.Enabled = Me.Visible
    End Sub

    Private Sub butProcessOnlineDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessOnlineDesc.Click
        Dim it As ListViewItem
        Static b As Boolean = False

        If b Then
            _stopOnlineRetrieving = True
        Else
            _stopOnlineRetrieving = False
        End If

        b = True
        For Each it In Me.lvProcess.SelectedItems
            Dim cp As cProcess = CType(it.Tag, cProcess)
            My.Application.DoEvents()

            If _stopOnlineRetrieving Then
                b = False
                Exit Sub
            End If

            Try
                Select Case mdlInternet.GetSecurityRisk(cp.Name)
                    Case 0
                        it.BackColor = Color.LightGreen
                    Case 1
                        it.BackColor = Color.LightPink
                    Case 2
                        it.BackColor = Color.Orange
                    Case 3
                        it.BackColor = Color.Red
                    Case 4
                        it.BackColor = Color.Red
                    Case 5
                        it.BackColor = Color.Red
                End Select
            Catch ex As Exception
                '
            End Try
        Next

        b = False
        _stopOnlineRetrieving = False
    End Sub

    Private Sub butProcessGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessGoogle.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Dim cp As cProcess = CType(it.Tag, cProcess)
            My.Application.DoEvents()
            Try
                cFile.ShellOpenFile("http://www.google.com/search?hl=en&q=%22" & cp.Name & "%22")
            Catch ex As Exception
                '
            End Try
        Next
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

    Private Sub lblResCount2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResCount2.Click
        Me.lvServices.Focus()
        Try
            Me.lvServices.EnsureVisible(Me.lvServices.Groups(1).Items(0).Index)
            Me.lvServices.SelectedItems.Clear()
            Me.lvServices.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetSecurityRiskOnlineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetSecurityRiskOnlineToolStripMenuItem.Click
        Call Me.butProcessOnlineDesc_Click(Nothing, Nothing)
    End Sub

    Private Sub GoogleSearchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleSearchToolStripMenuItem.Click
        Call Me.butProcessGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub butServiceGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceGoogle.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            My.Application.DoEvents()
            Try
                cFile.ShellOpenFile("http://www.google.com/search?hl=en&q=%22" & it.Text & "%22")
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub GoogleSearchToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleSearchToolStripMenuItem1.Click
        Call Me.butServiceGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub FileDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileDetailsToolStripMenuItem.Click
        Call Me.butServiceFileDetails_Click(Nothing, Nothing)
    End Sub

    Private Sub FileDetailsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileDetailsToolStripMenuItem1.Click
        Call Me.cmdShowFileDetails_Click(Nothing, Nothing)
    End Sub

    Private Sub butServiceFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceFileDetails.Click
        If Me.lvServices.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvServices.SelectedItems.Item(0).SubItems(4).Text
            If IO.File.Exists(s) = False Then
                s = cFile.IntelligentPathRetrieving2(s)
            End If
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub DisplayDetailsFile(ByVal file As String)
        Me.txtFile.Text = file
        refreshFileInfos(file)
        Me.Ribbon.ActiveTab = Me.FileTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
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

    Private Sub rtb3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb3.TextChanged
        Me.cmdFileClipboard.Enabled = (rtb3.Rtf.Length > 0)
    End Sub

    Private Sub ToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem16.Click
        My.Computer.Clipboard.SetImage(Me.pctFileBig.Image)
    End Sub

    Private Sub ToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem17.Click
        My.Computer.Clipboard.SetImage(Me.pctFileSmall.Image)
    End Sub

    Private Sub butUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butUpdate.Click
        Dim frm As New frmPreferences
        With frm
            .TabControl.SelectedTab = .TabPage2
            .ShowDialog()
            .Dispose()
        End With
        frm = Nothing
    End Sub

    Private Sub butSearchGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSearchGo.Click
        ' Launch search
        Dim sToSearch As String = Me.txtSearchString.TextBoxText
        If (sToSearch Is Nothing) OrElse sToSearch.Length < 1 Then Exit Sub

        If Me.chkSearchCase.Checked = False Then
            sToSearch = sToSearch.ToLower
        End If

        Me.lvSearchResults.Items.Clear()
        Me.lvSearchResults.BeginUpdate()

        ' Refresh services and processes lists (easy way to have up to date informations)
        Call Me.refreshProcessList()
        Call Me.refreshServiceList()

        ' Lock timers so we won't refresh
        Me.timerProcess.Enabled = False
        Me.timerServices.Enabled = False

        Dim it As ListViewItem
        Dim subit As ListViewItem.ListViewSubItem
        Dim c As Integer
        Dim sComp As String
        Dim i As Integer = 0
        Dim id As Integer = 0

        If Me.chkSearchProcess.Checked Then
            For Each it In Me.lvProcess.Items
                Dim cp As cProcess = CType(it.Tag, cProcess)
                c = -1
                For Each subit In it.SubItems
                    If Me.chkSearchCase.Checked = False Then
                        sComp = subit.Text.ToLower
                    Else
                        sComp = subit.Text
                    End If
                    c += 1
                    If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                        ' So we've found a result
                        Dim newIt As New ListViewItem
                        Dim n2 As New ListViewItem.ListViewSubItem
                        Dim n3 As New ListViewItem.ListViewSubItem
                        Dim n4 As New ListViewItem.ListViewSubItem
                        newIt.Text = "Process"
                        n3.Text = Me.lvProcess.Columns.Item(c).Text
                        n2.Text = newIt.Text & " -- " & n3.Text & " -- " & cp.Name & " -- " & subit.Text
                        n4.Text = CStr(cp.Pid) & " -- " & cp.Name
                        newIt.SubItems.Add(n2)
                        newIt.SubItems.Add(n3)
                        newIt.SubItems.Add(n4)
                        newIt.Tag = "process"
                        newIt.Group = Me.lvSearchResults.Groups(0)
                        Try
                            Dim fName As String = cp.Path
                            imgSearch.Images.Add(fName, imgProcess.Images.Item(fName))
                            newIt.ImageKey = fName
                        Catch ex As Exception
                            newIt.ImageKey = "noicon"
                        End Try
                        Me.lvSearchResults.Items.Add(newIt)
                    End If
                Next

                ' Check for modules
                Try
                    If Me.chkSearchModules.Checked Then
                        Dim p As ProcessModuleCollection = cp.Modules
                        Dim m As ProcessModule
                        For Each m In p
                            If Me.chkSearchCase.Checked = False Then
                                sComp = m.FileVersionInfo.FileName.ToLower
                            Else
                                sComp = m.FileVersionInfo.FileName
                            End If
                            If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                                ' So we've found a result
                                Dim newIt As New ListViewItem
                                Dim n2 As New ListViewItem.ListViewSubItem
                                Dim n3 As New ListViewItem.ListViewSubItem
                                Dim n4 As New ListViewItem.ListViewSubItem
                                newIt.Text = "Module"
                                newIt.Tag = New cModule(cp.Pid, m)
                                n3.Text = "Module"
                                n2.Text = newIt.Text & " -- " & cp.Name & " -- " & m.FileVersionInfo.FileName
                                n4.Text = CStr(cp.Pid) & " -- " & cp.Name
                                newIt.SubItems.Add(n2)
                                newIt.SubItems.Add(n3)
                                newIt.SubItems.Add(n4)
                                newIt.ImageKey = "dll"
                                newIt.Group = Me.lvSearchResults.Groups(0)
                                Me.lvSearchResults.Items.Add(newIt)
                            End If
                        Next
                    End If
                Catch ex As Exception
                    '
                End Try
            Next
        End If
        If Me.chkSearchServices.Checked Then
            For Each it In Me.lvServices.Items
                c = -1
                For Each subit In it.SubItems
                    If Me.chkSearchCase.Checked = False Then
                        sComp = subit.Text.ToLower
                    Else
                        sComp = subit.Text
                    End If
                    c += 1
                    If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                        ' So we've found a result
                        Dim newIt As New ListViewItem
                        Dim n2 As New ListViewItem.ListViewSubItem
                        Dim n3 As New ListViewItem.ListViewSubItem
                        Dim n4 As New ListViewItem.ListViewSubItem
                        newIt.Text = "Service"
                        newIt.Tag = "service"
                        n3.Text = Me.lvServices.Columns.Item(c).Text
                        n2.Text = newIt.Text & " -- " & n3.Text & " -- " & it.Text & " -- " & subit.Text
                        n4.Text = it.Text
                        newIt.SubItems.Add(n2)
                        newIt.SubItems.Add(n3)
                        newIt.SubItems.Add(n4)
                        newIt.ImageKey = "service"
                        newIt.Group = Me.lvSearchResults.Groups(0)
                        Me.lvSearchResults.Items.Add(newIt)
                    End If
                Next
            Next
        End If

        If Me.chkSearchHandles.Checked Then
            handles_Renamed.Refresh()
            For i = 0 To handles_Renamed.Count - 1
                With handles_Renamed
                    If (Len(.GetObjectName(i)) > 0) Then
                        If Me.chkSearchCase.Checked = False Then
                            sComp = .GetObjectName(i).ToLower
                        Else
                            sComp = .GetObjectName(i)
                        End If
                        If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                            ' So we've found a result
                            Dim newIt As New ListViewItem
                            Dim n2 As New ListViewItem.ListViewSubItem
                            Dim n3 As New ListViewItem.ListViewSubItem
                            Dim n4 As New ListViewItem.ListViewSubItem
                            newIt.Text = "Handle"
                            newIt.Tag = .GetHandle(i)
                            n3.Text = .GetNameInformation(i)
                            n2.Text = newIt.Text & " -- " & n3.Text & " -- " & .GetObjectName(i)
                            n4.Text = .GetProcessID(i) & " -- " & cFile.GetFileName(cProcess.GetPath(.GetProcessID(i)))
                            newIt.SubItems.Add(n2)
                            newIt.SubItems.Add(n3)
                            newIt.SubItems.Add(n4)
                            newIt.ImageKey = "handle"
                            newIt.Group = Me.lvSearchResults.Groups(0)
                            Me.lvSearchResults.Items.Add(newIt)
                        End If
                    End If
                End With
            Next
        End If

        If Me.chkSearchWindows.Checked Then
            Dim w() As cWindow = Nothing
            Dim ww As cWindow
            Call cWindow.EnumerateAll(w)
            For Each ww In w
                With ww
                    If (Len(.Caption) > 0) Then
                        If Me.chkSearchCase.Checked = False Then
                            sComp = .Caption.ToLower
                        Else
                            sComp = .Caption
                        End If
                        'type, result, field, process
                        If InStr(sComp, sToSearch, CompareMethod.Binary) > 0 Then
                            ' So we've found a result
                            Dim newIt As New ListViewItem
                            Dim n2 As New ListViewItem.ListViewSubItem
                            Dim n3 As New ListViewItem.ListViewSubItem
                            Dim n4 As New ListViewItem.ListViewSubItem
                            newIt.Text = "Window"
                            newIt.Tag = "window"
                            n3.Text = "Window -- " & CStr(.Handle)
                            n2.Text = newIt.Text & " -- " & .Caption
                            n4.Text = .ParentProcessId & " -- " & .ParentProcessName
                            newIt.SubItems.Add(n2)
                            newIt.SubItems.Add(n3)
                            newIt.SubItems.Add(n4)
                            newIt.ImageKey = "window"
                            newIt.Group = Me.lvSearchResults.Groups(0)
                            Me.lvSearchResults.Items.Add(newIt)
                        End If
                    End If
                End With
            Next
        End If

        Me.lvSearchResults.EndUpdate()

        Me.timerServices.Enabled = True
        Me.timerProcess.Enabled = True
        Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvSearchResults.Items.Count) & " search results"
    End Sub

    Private Sub txtSearchString_TextBoxTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchString.TextBoxTextChanged
        Dim b As Boolean = (txtSearchString.TextBoxText IsNot Nothing)
        If b Then
            b = b And txtSearchString.TextBoxText.Length > 0
        End If
        Me.butSearchGo.Enabled = b
    End Sub

    Private Sub chkSearchProcess_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSearchProcess.CheckedChanged
        Me.chkSearchModules.Enabled = (Me.chkSearchProcess.Checked)
    End Sub

    Private Sub butSearchSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSearchSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "search"
            Call My.Application.DoEvents()
            .ShowDialog()
        End With
    End Sub

    Private Sub ShowHandles(Optional ByVal showTab As Boolean = True)
        ' Display handles of desired processes (handlesToRefresh)
        Dim id As Integer
        Dim i As Integer
        Dim it As ListViewItem

        If Me.handlesToRefresh Is Nothing Then Exit Sub

        handles_Renamed.Refresh()
        Me.lvHandles.Items.Clear()
        Me.lvHandles.BeginUpdate()

        For Each id In Me.handlesToRefresh
            For i = 0 To handles_Renamed.Count - 1
                With handles_Renamed
                    If (.GetProcessID(i) = id) And (Len(.GetObjectName(i)) > 0) Then
                        it = lvHandles.Items.Add(.GetNameInformation(i))
                        it.SubItems.Add(.GetObjectName(i))
                        it.SubItems.Add(CStr(.GetHandleCount(i)))
                        it.SubItems.Add(CStr(.GetPointerCount(i)))
                        it.SubItems.Add(CStr(.GetObjectCount(i)))
                        it.SubItems.Add(CStr(.GetHandle(i)))
                        it.SubItems.Add(CStr(id) & " -- " & cFile.GetFileName(cProcess.GetPath(id)))
                        it.Tag = .GetHandle(i)
                        Select Case it.Text
                            Case "Key"
                                it.ImageKey = "key"
                            Case "File", "Directory"
                                ' Have to retrieve the icon of file/directory
                                Dim fName As String = .GetObjectName(i)
                                If IO.File.Exists(fName) Or IO.Directory.Exists(fName) Then
                                    Dim img As System.Drawing.Icon = GetIcon2(fName, True)
                                    If img IsNot Nothing Then
                                        imgServices.Images.Add(fName, img)
                                        it.ImageKey = fName
                                    Else
                                        it.ImageKey = "noicon"
                                    End If
                                Else
                                    it.ImageKey = "noicon"
                                End If
                            Case Else
                                it.ImageKey = "service"
                        End Select
                    End If
                End With
            Next
        Next

        Me.lvHandles.EndUpdate()

        If showTab Then
            Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvHandles.Items.Count) & " handles"
            My.Application.DoEvents()
            Me.Ribbon.ActiveTab = Me.HandlesTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub butHandleRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandleRefresh.Click
        Call ShowHandles()
    End Sub

    Private Sub butHandleClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandleClose.Click
        Dim it As ListViewItem
        Dim pid As Integer
        Dim handle As Integer
        For Each it In Me.lvHandles.SelectedItems
            pid = CInt(Val(it.SubItems(6).Text))
            handle = CInt(Val(it.SubItems(5).Text))
            Call handles_Renamed.CloseProcessLocalHandle(pid, handle)
            it.Remove()
        Next
    End Sub

    Private Sub SelectAssociatedProcessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAssociatedProcessToolStripMenuItem.Click
        ' Select processes associated to selected search results
        Dim it As ListViewItem
        If Me.lvSearchResults.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvSearchResults.SelectedItems
            Try
                If it.Tag.ToString = "service" Then
                    ' Select service
                    Dim sp As String = it.SubItems(3).Text
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvServices.Items
                        Dim cp As cService = CType(it2.Tag, cService)
                        If cp.Name = sp Then
                            it2.Selected = True
                        End If
                    Next
                    Me.Ribbon.ActiveTab = Me.ServiceTab
                Else
                    ' Select process
                    Dim sp As String = it.SubItems(3).Text
                    Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                    If i > 0 Then
                        Dim pid As String = sp.Substring(0, i - 1)
                        Dim it2 As ListViewItem
                        For Each it2 In Me.lvProcess.Items
                            Dim cp As cProcess = CType(it2.Tag, cProcess)
                            If CStr(cp.Pid) = pid Then
                                it2.Selected = True
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

    Private Sub ShowHandlesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowHandlesToolStripMenuItem.Click
        Call Me.butShowProcHandles_Click(Nothing, Nothing)
    End Sub

    Private Sub butFileProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileProperties.Click
        Call cFile.ShowFileProperty(Me.txtFile.Text)
    End Sub

    Private Sub butFileShowFolderProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileShowFolderProperties.Click
        Call cFile.ShowFileProperty(IO.Directory.GetParent(Me.txtFile.Text).FullName)
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

    Private Sub txtFile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFile.TextChanged
        Dim b As Boolean = IO.File.Exists(Me.txtFile.Text)
        Me.RBFileDelete.Enabled = b
        Me.RBFileKillProcess.Enabled = b
        Me.RBFileOnline.Enabled = b
        Me.RBFileOther.Enabled = b
        Me.RBFileOthers.Enabled = b
        Me.butShreddFile.Enabled = False    'TOCHANGE
    End Sub

    Private Sub butFileRelease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRelease.Click
        Dim frm As New frmFileRelease
        With frm
            .file = Me.txtFile.Text
            Call .ShowDialog()
        End With
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        ' Close selected items
        Dim it As ListViewItem
        For Each it In Me.lvSearchResults.SelectedItems
            Select Case it.Tag.ToString
                Case "process"
                    Dim sp As String = it.SubItems(3).Text
                    Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                    If i > 0 Then
                        Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                        Call cProcess.Kill(pid)
                    End If
                Case "service"
                    cService.StopService(it.SubItems(3).Text)
                Case "window"
                    Dim sp As String = it.SubItems(2).Text
                    Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                    If i > 0 Then
                        Dim hand As Integer = CInt(Val(sp.Substring(i + 3, sp.Length - i - 3)))
                        cWindow.CloseWindow(hand)
                    End If
                Case Else
                    If TypeOf it.Tag Is cModule Then
                        ' Then it is a module
                        Dim sp As String = it.SubItems(3).Text
                        Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                        If i > 0 Then
                            Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                            sp = it.SubItems(1).Text
                            i = InStrRev(sp, " ", , CompareMethod.Binary)
                            Dim sMod As String = sp.Substring(i, sp.Length - i)
                            Call CType(it.Tag, cModule).UnloadModule()
                        End If
                    Else
                        ' Handle
                        Dim sp As String = it.SubItems(3).Text
                        Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                        Dim handle As Integer = CInt(it.Tag)
                        If i > 0 Then
                            Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                            Call handles_Renamed.CloseProcessLocalHandle(pid, handle)
                        End If
                    End If
            End Select
        Next
    End Sub

    Private Sub lvJobs_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvJobs.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvJobs.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvJobs.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvJobs.Sort()
    End Sub

    Private Sub lvSearchResults_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvSearchResults.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvSearchResults.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvSearchResults.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvSearchResults.Sort()
    End Sub



    Private Sub butFileGoogleSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileGoogleSearch.Click
        My.Application.DoEvents()
        Try
            cFile.ShellOpenFile("http://www.google.com/search?hl=en&q=%22" & cFile.GetFileName(Me.txtFile.Text) & "%22")
        Catch ex As Exception
            '
        End Try
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

    Private Sub cmdSetFileDates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetFileDates.Click
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

    Private Sub butFileSeeStrings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileSeeStrings.Click
        Call DisplayFileStrings(Me.lstFileString, Me.txtFile.Text)
    End Sub

    Private Sub lstFileString_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstFileString.MouseDown
        Call mdlMisc.CopyLstToClip(e, Me.lstFileString)
    End Sub

    Private Function RemoveAttribute(ByVal file As String, ByVal attributesToRemove As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.Attributes()
        Return attributes And Not (attributesToRemove)
    End Function
    Private Function AddAttribute(ByVal file As String, ByVal attributesToAdd As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.Attributes
        Return attributes Or attributesToAdd
    End Function

    Private Sub chkFileArchive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileArchive.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileArchive.Checked Then
                cSelFile.Attributes = AddAttribute(Me.txtFile.Text, IO.FileAttributes.Archive)
            Else
                cSelFile.Attributes = RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.Archive)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileArchive.Checked = Not (Me.chkFileArchive.Checked)
        End Try
    End Sub

    Private Sub chkFileHidden_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileHidden.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileHidden.Checked Then
                cSelFile.Attributes = AddAttribute(Me.txtFile.Text, IO.FileAttributes.Hidden)
            Else
                cSelFile.Attributes = RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.Hidden)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileHidden.Checked = Not (Me.chkFileHidden.Checked)
        End Try
    End Sub

    Private Sub chkFileReadOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileReadOnly.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileReadOnly.Checked Then
                cSelFile.Attributes = AddAttribute(Me.txtFile.Text, IO.FileAttributes.ReadOnly)
            Else
                cSelFile.Attributes = RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.ReadOnly)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileReadOnly.Checked = Not (Me.chkFileReadOnly.Checked)
        End Try
    End Sub

    Private Sub chkFileContentNotIndexed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileContentNotIndexed.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileContentNotIndexed.Checked Then
                cSelFile.Attributes = AddAttribute(Me.txtFile.Text, IO.FileAttributes.NotContentIndexed)
            Else
                cSelFile.Attributes = RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.NotContentIndexed)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileContentNotIndexed.Checked = Not (Me.chkFileContentNotIndexed.Checked)
        End Try
    End Sub

    Private Sub chkFileNormal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileNormal.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileNormal.Checked Then
                cSelFile.Attributes = AddAttribute(Me.txtFile.Text, IO.FileAttributes.Normal)
            Else
                cSelFile.Attributes = RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.Normal)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileNormal.Checked = Not (Me.chkFileNormal.Checked)
        End Try
    End Sub

    Private Sub chkFileSystem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileSystem.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileSystem.Checked Then
                cSelFile.Attributes = AddAttribute(Me.txtFile.Text, IO.FileAttributes.System)
            Else
                cSelFile.Attributes = RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.System)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileSystem.Checked = Not (Me.chkFileSystem.Checked)
        End Try
    End Sub

    Private Sub ToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem22.Click
        Call Me.butHandleClose_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem19.Click
        ' Select processes associated to selected handles results
        Dim it As ListViewItem
        If Me.lvHandles.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvHandles.SelectedItems
            Try
                Dim sp As String = it.SubItems(6).Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As String = sp.Substring(0, i - 1)
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvProcess.Items
                        Dim cp As cProcess = CType(it2.Tag, cProcess)
                        If CStr(cp.Pid) = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
            Catch ex As Exception
                '
            End Try
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub butFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileOpen.Click
        Call cFile.ShellOpenFile(Me.txtFile.Text)
    End Sub

    ' Display file strings
    Public Sub DisplayFileStrings(ByVal lst As ListBox, ByVal file As String)
        Dim s As String = vbNullString
        Dim sCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim bTaille As Integer
        Dim lLen As Integer
        Dim iAsc As Integer

        If IO.File.Exists(file) Then

            lst.Items.Clear()

            ' Retrieve entire file in memory
            ' Warn user if file is up to 2MB
            Try
                s = IO.File.ReadAllText(file)

                If cFile.GetFileSize(file) > 2000000 Then
                    If MsgBox("File size is greater than 2MB. It is not recommended to open a large file, do you want to continue ?", _
                        MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Large file") = MsgBoxResult.No Then
                        lstFileString.Items.Add("Click on 'Others->Show file strings' to retrieve file strings")
                        Exit Sub
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            End Try


            ' Desired minimum size for a string
            bTaille = 4

            ' A char is considered as part of a string if its value is between 32 and 122
            lLen = Len(s)

            ' Lock listbox
            ValidateRect(lst.Handle.ToInt32, 0)

            ' Ok, parse file
            Do Until x > lLen

                iAsc = Asc(Mid$(s, x, 1))

                If iAsc >= 32 And iAsc <= 122 Then
                    ' Valid char
                    sCtemp = sCtemp & Chr(iAsc)
                Else
                    sCtemp = LTrim$(sCtemp)
                    sCtemp = RTrim$(sCtemp)
                    If Len(sCtemp) > bTaille Then
                        lst.Items.Add(sCtemp)
                    End If
                    sCtemp = vbNullString
                End If

                'If (x Mod 131072) = 0 Then
                '    My.Application.DoEvents()
                'End If

                x += 1
            Loop

            ' Last item
            If Len(sCtemp) > bTaille Then
                lst.Items.Add(sCtemp)
            End If

            ' Unlock listbox
            InvalidateRect(lst.Handle.ToInt32, 0, 0)
        End If

    End Sub

    Private Sub butMonitoringAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitoringAdd.Click
        Dim frm As New frmAddProcessMonitor
        frm.ShowDialog()
    End Sub

    ' Add a monitoring item
    Public Sub AddMonitoringItem(ByVal it As cMonitor)

        ' Check if a node with same category and instance exists
        Dim nExistingItem As TreeNode = Nothing
        Dim n As TreeNode
        For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
            If CStr(IIf(Len(it.GetInstanceName) > 0, it.GetInstanceName & " - ", vbNullString)) & _
                it.CategoryName = n.Text Then

                nExistingItem = n
                Exit For
            End If
        Next

        If nExistingItem Is Nothing Then
            ' New sub item
            Dim n1 As New TreeNode
            With n1
                .Text = CStr(IIf(Len(it.GetInstanceName) > 0, it.GetInstanceName & " - ", _
                   vbNullString)) & it.CategoryName
                .ImageKey = "exe"
                .ImageIndex = 0
                .SelectedImageIndex = 0
            End With

            Dim ncpu As New TreeNode
            With ncpu
                .Text = it.CounterName
                .ImageKey = "sub"
                .SelectedImageIndex = 2
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
                .SelectedImageIndex = 2
                .Tag = it
            End With

            nExistingItem.Nodes.Add(ncpu)
        End If

        Call updateMonitoringLog()
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
                Me.graphMonitor.CreateGraphics.DrawString("Select in the treeview a monitor item.", Me.Font, Brushes.White, 0, 0)
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
            Me.graphMonitor.CreateGraphics.DrawString("Select in the treeview a process and then a monitor item.", Me.Font, Brushes.White, 0, 0)
        End If

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

    ' Update monitoring log
    Private Sub updateMonitoringLog()
        Dim s As String

        If Me.tvMonitor.Nodes.Item(0).Nodes.Count > 0 Then

            ' Count counters :-)
            Dim iCount As Integer = 0
            Dim n As TreeNode
            Dim n2 As TreeNode
            For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
                For Each n2 In n.Nodes
                    iCount += 1
                Next
            Next

            s = "Monitoring log" & vbNewLine
            s &= vbNewLine & vbNewLine & "Monitoring " & CStr(iCount) & " item(s)" & vbNewLine

            For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
                For Each n2 In n.Nodes

                    Dim it As cMonitor = CType(n2.Tag, cMonitor)
                    s &= vbNewLine & "* Category  : " & it.CategoryName & " -- Instance : " & it.GetInstanceName & " -- Counter : " & it.CounterName
                    s &= vbNewLine & "      Monitoring creation : " & it.GetMonitorCreationDate.ToLongDateString & " -- " & it.GetMonitorCreationDate.ToLongTimeString
                    If it.GetLastStarted.Ticks > 0 Then
                        s &= vbNewLine & "      Last start : " & it.GetLastStarted.ToLongDateString & " -- " & it.GetLastStarted.ToLongTimeString
                    Else
                        s &= vbNewLine & "      Not yet started"
                    End If
                    s &= vbNewLine & "      State : " & it.Enabled
                    s &= vbNewLine & "      Interval : " & it.Interval

                    s &= vbNewLine
                Next
            Next
            s = s.Substring(0, s.Length - 2)

            Me.txtMonitoringLog.Text = s
            Me.txtMonitoringLog.SelectionLength = 0
            Me.txtMonitoringLog.SelectionStart = 0

        Else
            Me.txtMonitoringLog.Text = "No process monitored." & vbNewLine & "Click on 'Add' button to monitor a process."
            Me.txtMonitoringLog.SelectionLength = 0
            Me.txtMonitoringLog.SelectionStart = 0
        End If

    End Sub

    ' Display stats in graph
    Private Sub ShowMonitorStats(ByVal it As cMonitor, ByVal key1 As String, ByVal key2 As String, _
        ByVal key3 As String)

        Me.timerMonitoring.Interval = it.Interval

        If it.Enabled = False Then
            Me.graphMonitor.CreateGraphics.Clear(Color.Black)
            Me.graphMonitor.CreateGraphics.DrawString("You sould start the monitoring.", Me.Font, Brushes.White, 0, 0)
            Exit Sub
        End If

        ' Get values from monitor item
        Dim v() As Graph.ValueItem
        Dim cCol As New Collection
        cCol = it.GetMonitorItems()

        ' Limit DT pickers
        Me.dtMonitorL.MaxDate = Date.Now
        Me.dtMonitorL.MinDate = it.GetMonitorCreationDate
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
                    .ViewMin = findViewLast(v, .ViewMax)
                Else
                    ' Then both fixed
                    .ViewMax = findViewIntegerFromDate(Me.dtMonitorR.Value, v, it)
                    .ViewMin = findViewIntegerFromDate(Me.dtMonitorL.Value, v, it)
                End If

                .SetValues(v)
                .dDate = it.GetMonitorCreationDate
                .EnableGraph = True
                Call .Refresh()
            End With
        End If
    End Sub

    Private Sub timerMonitoring_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerMonitoring.Tick
        Call tvMonitor_AfterSelect(Nothing, Nothing)
    End Sub

    Private Sub chkMonitorLeftAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMonitorLeftAuto.CheckedChanged
        Me.dtMonitorL.Enabled = Not (Me.chkMonitorLeftAuto.Checked)
        Me.txtMonitorNumber.Enabled = Not (Me.chkMonitorLeftAuto.Checked = False And Me.chkMonitorRightAuto.Checked = False)
        Me.lblMonitorMaxNumber.Enabled = Me.txtMonitorNumber.Enabled
    End Sub

    Private Sub chkMonitorRightAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMonitorRightAuto.CheckedChanged
        Me.dtMonitorR.Enabled = Not (Me.chkMonitorRightAuto.Checked)
        Me.txtMonitorNumber.Enabled = Not (Me.chkMonitorLeftAuto.Checked = False And Me.chkMonitorRightAuto.Checked = False)
        Me.lblMonitorMaxNumber.Enabled = Me.txtMonitorNumber.Enabled
    End Sub

    ' Return an integer that corresponds to a time in a monitor from a date
    Private Function findViewIntegerFromDate(ByVal d As Date, ByVal v() As Graph.ValueItem, _
        ByVal monitor As cMonitor) As Integer

        Dim it As Graph.ValueItem
        Dim l As Long = d.Ticks
        Dim start As Long = monitor.GetMonitorCreationDate.Ticks
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
    Private Function findViewLast(ByVal v() As Graph.ValueItem, ByVal max As Integer) As Integer
        Dim lMax As Integer = CInt(Val(Me.txtMonitorNumber.Text))
        Return Math.Max(0, max - lMax)
    End Function

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

    Private Sub graphMonitor_OnZoom(ByVal leftVal As Integer, ByVal rightVal As Integer) Handles graphMonitor.OnZoom
        ' Change dates and set view as fixed (left and right)
        Try
            Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
            Dim l As New Date(it.GetMonitorCreationDate.Ticks + leftVal * 10000)
            Dim r As New Date(it.GetMonitorCreationDate.Ticks + rightVal * 10000)
            Me.dtMonitorL.Value = l
            Me.dtMonitorR.Value = r
            Me.chkMonitorLeftAuto.Checked = False
            Me.chkMonitorRightAuto.Checked = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MonitorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonitorToolStripMenuItem.Click
        Call Me.butProcessMonitor_Click(Nothing, Nothing)
    End Sub

    Private Sub butProcessMonitor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessMonitor.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Dim frm As New frmAddProcessMonitor
            frm._selProcess = CType(it.Tag, cProcess).Pid
            frm.ShowDialog()
        Next
    End Sub

    Private Sub ShowThreadsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowThreadsToolStripMenuItem.Click
        Call Me.butProcessThreads_Click(Nothing, Nothing)
    End Sub


    ' Show threads of selected processes (threadsToRefresh)
    Private Sub ShowThreads(Optional ByVal showTab As Boolean = True)
        Dim t() As cThread = Nothing
        Dim tCt As cThread

        ' Delete existing items
        Dim it2 As ListViewItem
        For Each it2 In Me.lvThreads.Items
            Dim tt As cThread = CType(it2.Tag, cThread)
            tt.Dispose()
        Next
        Me.lvThreads.Items.Clear()
        Me.lvThreads.BeginUpdate()

        For x As Integer = 0 To UBound(threadsToRefresh)
            cThread.Enumerate(threadsToRefresh(x), t)

            For Each tCt In t
                ' Add threads to listview
                Try
                    Dim it As New ListViewItem
                    it.Text = CStr(tCt.Id)
                    Dim n1 As New ListViewItem.ListViewSubItem
                    n1.Text = CStr(tCt.ProcessId) & " -- " & tCt.ProcessName
                    it.SubItems.Add(n1)
                    Dim n2 As New ListViewItem.ListViewSubItem
                    n2.Text = CStr(tCt.PriorityString)
                    it.SubItems.Add(n2)
                    Dim n3 As New ListViewItem.ListViewSubItem
                    n3.Text = tCt.ThreadState
                    it.SubItems.Add(n3)
                    Dim n6 As New ListViewItem.ListViewSubItem
                    n6.Text = CStr(tCt.WaitReason)
                    it.SubItems.Add(n6)
                    Dim n4 As New ListViewItem.ListViewSubItem
                    n4.Text = CStr(tCt.StartTime.ToLongDateString & " -- " & tCt.StartTime.ToLongTimeString)
                    it.SubItems.Add(n4)
                    Dim n5 As New ListViewItem.ListViewSubItem
                    n5.Text = CStr(tCt.TotalProcessorTime.ToString)
                    it.SubItems.Add(n5)

                    it.Tag = tCt
                    it.Group = Me.lvThreads.Groups(0)
                    it.ImageKey = "thread"
                    Me.lvThreads.Items.Add(it)
                Catch ex As Exception
                    '
                End Try
            Next
        Next

        Me.lvThreads.EndUpdate()

        If showTab Then _
            Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvThreads.Items.Count) & " threads"

    End Sub

    Private Sub butThreadRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadRefresh.Click
        If threadsToRefresh IsNot Nothing Then Call ShowThreads()
    End Sub

    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem23.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.ThreadTerminate()
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem24.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.ThreadSuspend()
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem25.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.ThreadResume()
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem27.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.Idle
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub butThreadKill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadKill.Click
        Call ToolStripMenuItem23_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadResume_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadResume.Click
        Call ToolStripMenuItem25_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadStop.Click
        Call ToolStripMenuItem24_Click(Nothing, Nothing)
    End Sub

    Private Sub SelectedAssociatedProcessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedAssociatedProcessToolStripMenuItem.Click
        ' Select processes associated to selected handles results
        Dim it As ListViewItem
        If Me.lvThreads.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvThreads.SelectedItems
            Try
                Dim sp As String = it.SubItems(1).Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As String = sp.Substring(0, i - 1)
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvProcess.Items
                        Dim cp As cProcess = CType(it2.Tag, cProcess)
                        If CStr(cp.Pid) = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
            Catch ex As Exception
                '
            End Try
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub LowestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LowestToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.Lowest
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem28.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.BelowNormal
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem29.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.Normal
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem30.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.AboveNormal
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem31.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.Highest
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub ToolStripMenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem32.Click
        Dim it As ListViewItem
        For Each it In Me.lvThreads.SelectedItems
            Dim t As cThread = CType(it.Tag, cThread)
            Try
                t.Priority = ThreadPriorityLevel.TimeCritical
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub butThreadPabove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPabove.Click
        Call ToolStripMenuItem30_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPbelow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPbelow.Click
        Call ToolStripMenuItem28_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPcritical_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPcritical.Click
        Call ToolStripMenuItem32_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPhighest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPhighest.Click
        Call ToolStripMenuItem31_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPidle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPidle.Click
        Call ToolStripMenuItem27_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPlowest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPlowest.Click
        Call LowestToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub butThreadPnormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadPnormal.Click
        Call ToolStripMenuItem29_Click(Nothing, Nothing)
    End Sub

    ' Show windows of selected processes (windowsToRefresh)
    Private Sub ShowWindows(Optional ByVal showTab As Boolean = True)
        Dim t() As cWindow = Nothing
        Dim tCt As cWindow

        ' Delete existing items
        'Dim it2 As ListViewItem
        'For Each it2 In Me.lvWindows.Items
        '    Dim tt As cWindow = CType(it2.Tag, cWindow)
        '    tt.Dispose()
        'Next
        Me.lvWindows.Items.Clear()
        Me.lvWindows.BeginUpdate()

        ' List once
        cWindow.Enumerate(windowsToRefresh, t)

        If t IsNot Nothing Then

            For Each tCt In t
                ' Add threads to listview
                Try
                    If Me.chkAllWindows.Checked Or Len(tCt.Caption) > 0 Then
                        Dim it As New ListViewItem
                        it.Text = CStr(tCt.Handle)
                        Dim n1 As New ListViewItem.ListViewSubItem
                        n1.Text = CStr(tCt.ParentProcessId) & " -- " & tCt.ParentProcessName
                        it.SubItems.Add(n1)
                        Dim n2 As New ListViewItem.ListViewSubItem
                        n2.Text = tCt.Caption
                        it.SubItems.Add(n2)
                        Dim n3 As New ListViewItem.ListViewSubItem
                        n3.Text = CStr(tCt.IsTask)
                        it.SubItems.Add(n3)
                        Dim n4 As New ListViewItem.ListViewSubItem
                        n4.Text = CStr(tCt.Enabled)
                        it.SubItems.Add(n4)
                        Dim n5 As New ListViewItem.ListViewSubItem
                        n5.Text = CStr(tCt.Visible)
                        it.SubItems.Add(n5)

                        'Dim key As String = CStr(tCt.ParentProcessId) & "|" & CStr(tCt.Handle)
                        'Me.imgWindows.Images.Add(key, tCt.SmallIcon)
                        'it.ImageKey = key

                        it.Tag = tCt
                        it.Group = Me.lvWindows.Groups(0)
                        Me.lvWindows.Items.Add(it)
                    End If
                Catch ex As Exception
                    'MsgBox(ex.Message)
                End Try
            Next

        End If

        Me.lvWindows.EndUpdate()

        If showTab Then _
            Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvWindows.Items.Count) & " windows"

    End Sub

    Private Sub ShowWindowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowWindowsToolStripMenuItem.Click
        Call butProcessWindows_Click(Nothing, Nothing)
    End Sub

    Private Sub butWindowRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowRefresh.Click
        If windowsToRefresh IsNot Nothing Then Call ShowWindows()
    End Sub

    Private Sub butProcessThreads_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessThreads.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim it As ListViewItem

            Dim x As Integer = 0
            ReDim threadsToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it In Me.lvProcess.SelectedItems
                Dim cp As cProcess = CType(it.Tag, cProcess)
                threadsToRefresh(x) = cp.Pid
                x += 1
            Next

            Call ShowThreads()
            Me.Ribbon.ActiveTab = Me.ThreadTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub butProcessWindows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessWindows.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim it As ListViewItem

            Dim x As Integer = 0
            ReDim windowsToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it In Me.lvProcess.SelectedItems
                Dim cp As cProcess = CType(it.Tag, cProcess)
                windowsToRefresh(x) = cp.Pid
                x += 1
            Next

            Call ShowWindows()
            Me.Ribbon.ActiveTab = Me.WindowTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub butShowProcHandles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowProcHandles.Click
        Dim it As ListViewItem
        Dim x As Integer = 0
        ReDim handlesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
        For Each it In Me.lvProcess.SelectedItems
            handlesToRefresh(x) = CInt(Val(it.SubItems(1).Text))
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
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).BringToFront(True)
        Next
    End Sub

    Private Sub butWindowCaption_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowCaption.Click
        Dim z As String = ""

        If Me.lvWindows.SelectedItems.Count > 0 Then
            z = CType(Me.lvWindows.SelectedItems.Item(0).Tag, cWindow).Caption
        End If

        Dim sres As String = InputBox("Set a new caption.", "New caption", z)

        If sres = Nothing Then Exit Sub

        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            CType(it.Tag, cWindow).Caption = sres
        Next
    End Sub

    Private Sub butWindowClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowClose.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).Close()
        Next
    End Sub

    Private Sub butWindowFlash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowFlash.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).Flash()
        Next
    End Sub

    Private Sub butWindowHide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowHide.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).Hide()
        Next
    End Sub

    Private Sub butWindowMaximize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowMaximize.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).Maximize()
        Next
    End Sub

    Private Sub butWindowMinimize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowMinimize.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).Minimize()
        Next
    End Sub

    Private Sub butWindowOpacity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowOpacity.Click
        Dim i As Byte
        Dim z As Integer = 0

        If Me.lvWindows.SelectedItems.Count > 0 Then
            z = CType(Me.lvWindows.SelectedItems.Item(0).Tag, cWindow).Opacity
        End If

        Dim sres As String = InputBox("Set a new opacity [0 to 255, 255 = minimum transparency]", "New opacity", CStr(z))

        If sres = Nothing Then Exit Sub
        i = CByte(Val(sres))
        If i < 0 Or i > 255 Then Exit Sub

        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            CType(it.Tag, cWindow).Opacity = i
        Next
    End Sub

    Private Sub butWindowSetAsActive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowSetAsActive.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).SetAsActiveWindow()
        Next
    End Sub

    Private Sub butWindowSetAsForeground_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowSetAsForeground.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).SetAsForegroundWindow()
        Next
    End Sub

    Private Sub butWindowShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowShow.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).Show()
        Next
    End Sub

    Private Sub butWindowDoNotBringToFront_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowDoNotBringToFront.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            Call CType(it.Tag, cWindow).BringToFront(False)
        Next
    End Sub

    Private Sub butWindowPositionSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowPositionSize.Click
        Dim r As cWindow.RECT

        If Me.lvWindows.SelectedItems.Count > 0 Then

            Dim frm As New frmWindowPosition
            With frm
                .SetCurrentPositions(CType(Me.lvWindows.SelectedItems(0).Tag, cWindow).Positions)

                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    r = .NewRect
                    Dim it As ListViewItem
                    For Each it In Me.lvWindows.SelectedItems
                        Call CType(it.Tag, cWindow).SetPositions(r)
                    Next
                End If
            End With
        End If
    End Sub

    Private Sub butWindowEnable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowEnable.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            CType(it.Tag, cWindow).Enabled = True
        Next
    End Sub

    Private Sub butWindowDisable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowDisable.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            CType(it.Tag, cWindow).Enabled = False
        Next
    End Sub

    Private Sub butWindowStopFlashing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowStopFlashing.Click
        Dim it As ListViewItem
        For Each it In Me.lvWindows.SelectedItems
            CType(it.Tag, cWindow).StopFlashing()
        Next
    End Sub

    Private Sub ToolStripMenuItem34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem34.Click
        ' Select processes associated to selected windows
        Dim it As ListViewItem
        If Me.lvWindows.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvWindows.SelectedItems
            Try
                Dim sp As String = it.SubItems(1).Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As String = sp.Substring(0, i - 1)
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvProcess.Items
                        Dim cp As cProcess = CType(it2.Tag, cProcess)
                        If CStr(cp.Pid) = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
            Catch ex As Exception
                '
            End Try
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub butHandlesSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandlesSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "handles"
            Call My.Application.DoEvents()
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
        Dim s As String = InputBox("New name (name+extension) ?", "Select a new file name", cFile.GetFileName(cSelFile.Path))
        If s = Nothing Then Exit Sub
        Me.txtFile.Text = cSelFile.WindowsRename(s)
        Call Me.refreshFileInfos(cSelFile.Path)
    End Sub

    Private Sub ShowModulesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowModulesToolStripMenuItem.Click
        Call butProcessShowModules_Click(Nothing, Nothing)
    End Sub

    Private Sub butProcessShowModules_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessShowModules.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim it As ListViewItem

            Dim x As Integer = 0
            ReDim modulesToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it In Me.lvProcess.SelectedItems
                Dim cp As cProcess = CType(it.Tag, cProcess)
                modulesToRefresh(x) = cp.Pid
                x += 1
            Next

            Call ShowModules()
            Me.Ribbon.ActiveTab = Me.ModulesTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    ' Show modules of selected processes (modulesToRefresh)
    Private Sub ShowModules(Optional ByVal showTab As Boolean = True)
        Dim t() As cModule = Nothing
        Dim tCt As cModule

        ' Delete existing items
        'Dim it2 As ListViewItem
        'For Each it2 In Me.lvModules.Items
        '    Dim tt As cModule = CType(it2.Tag, cModule)
        '    tt.Dispose()
        'Next
        Me.lvModules.Items.Clear()
        Me.lvModules.BeginUpdate()

        For x As Integer = 0 To UBound(modulesToRefresh)
            cModule.Enumerate(modulesToRefresh(x), t)

            If t IsNot Nothing Then

                For Each tCt In t
                    ' Add modules to listview
                    Try
                        Dim it As New ListViewItem
                        it.Text = cFile.GetFileName(tCt.FileName)
                        Dim n2 As New ListViewItem.ListViewSubItem
                        n2.Text = tCt.FileVersion
                        it.SubItems.Add(n2)
                        Dim n3 As New ListViewItem.ListViewSubItem
                        n3.Text = tCt.FileDescription
                        it.SubItems.Add(n3)
                        Dim n6 As New ListViewItem.ListViewSubItem
                        n6.Text = tCt.CompanyName
                        it.SubItems.Add(n6)
                        Dim n5 As New ListViewItem.ListViewSubItem
                        n5.Text = tCt.FileName
                        it.SubItems.Add(n5)
                        Dim n7 As New ListViewItem.ListViewSubItem
                        n7.Text = tCt.ProcessId & " -- " & cFile.GetFileName(cProcess.GetPath(tCt.ProcessId))
                        it.SubItems.Add(n7)

                        it.Tag = tCt
                        it.Group = Me.lvModules.Groups(0)
                        If tCt.FileName IsNot Nothing AndAlso tCt.FileName.Length > 3 Then
                            If tCt.FileName.Substring(tCt.FileName.Length - 3, 3).ToLower = "exe" Then

                                ' Add icon
                                Try

                                    Dim fName As String = tCt.FileName

                                    If IO.File.Exists(fName) Then
                                        Dim img As System.Drawing.Icon = GetIcon(fName, True)
                                        imgSearch.Images.Add(fName, img)
                                        it.ImageKey = fName
                                    Else
                                        it.ImageKey = "noicon"
                                    End If

                                Catch ex As Exception
                                    it.ImageKey = "noicon"
                                End Try

                            Else
                                it.ImageKey = "dll"
                            End If
                            Me.lvModules.Items.Add(it)
                        End If
                    Catch ex As Exception
                        '
                    End Try
                Next
            End If
        Next

        Me.lvModules.EndUpdate()

        If showTab Then _
            Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"

    End Sub

    Private Sub butModuleRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleRefresh.Click
        If modulesToRefresh IsNot Nothing Then Call ShowModules()
    End Sub

    Private Sub butModulesSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModulesSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "modules"
            Call My.Application.DoEvents()
            .ShowDialog()
        End With
    End Sub

    Private Sub butModuleUnload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleUnload.Click
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            Call CType(it.Tag, cModule).UnloadModule()
            it.Remove()
        Next
        Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"
    End Sub

    Private Sub lvJobs_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvJobs.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvJobs)
    End Sub

    Private Sub lvSearchResults_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSearchResults.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvSearchResults)
    End Sub

    Private Sub ToolStripMenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem36.Click
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            Call CType(it.Tag, cModule).UnloadModule()
            it.Remove()
        Next
        Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvModules.Items.Count) & " modules"
    End Sub

    Private Sub ToolStripMenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem35.Click
        ' Select processes associated to selected windows
        Dim it As ListViewItem
        If Me.lvModules.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvModules.SelectedItems
            Try
                Dim sp As String = it.SubItems(5).Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As String = sp.Substring(0, i - 1)
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvProcess.Items
                        Dim cp As cProcess = CType(it2.Tag, cProcess)
                        If CStr(cp.Pid) = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
            Catch ex As Exception
                '
            End Try
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub ShowFileDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowFileDetailsToolStripMenuItem.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.SelectedItems.Item(0).SubItems(4).Text
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub lvModules_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvModules.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvModules.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvModules.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvModules.Sort()
    End Sub

    Private Sub lvModules_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvModules.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvModules)
    End Sub

    Private Sub lvModules_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvModules.SelectedIndexChanged
        If lvModules.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvModules.SelectedItems.Item(0)

            If TypeOf it.Tag Is cModule Then

                Try
                    Dim cP As cModule = CType(it.Tag, cModule)

                    ' Description
                    Dim s As String = ""
                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Module properties\b0\par"
                    s = s & "\tab Module name :\tab\tab\tab " & cFile.GetFileName(cP.FileName) & "\par"
                    s = s & "\tab Process owner :\tab\tab\tab " & CStr(cP.ProcessId) & " -- " & cFile.GetFileName(cProcess.GetPath(cP.ProcessId)) & "\par"
                    s = s & "\tab Path :\tab\tab\tab\tab " & Replace(cP.FileName, "\", "\\") & "\par"
                    s = s & "\tab Version :\tab\tab\tab " & cP.FileVersion & "\par"
                    s = s & "\tab Comments :\tab\tab\tab " & cP.Comments & "\par"
                    s = s & "\tab CompanyName :\tab\tab\tab " & cP.CompanyName & "\par"
                    s = s & "\tab LegalCopyright :\tab\tab\tab " & cP.LegalCopyright & "\par"
                    s = s & "\tab ProductName :\tab\tab\tab " & cP.ProductName & "\par"
                    s = s & "\tab Language :\tab\tab\tab " & cP.Language & "\par"
                    s = s & "\tab InternalName :\tab\tab\tab " & cP.InternalName & "\par"
                    s = s & "\tab LegalTrademarks :\tab\tab " & cP.LegalTrademarks & "\par"
                    s = s & "\tab OriginalFilename :\tab\tab\tab " & cP.OriginalFilename & "\par"
                    s = s & "\tab FileBuildPart :\tab\tab\tab " & CStr(cP.FileBuildPart) & "\par"
                    s = s & "\tab FileDescription :\tab\tab\tab " & CStr(cP.FileDescription) & "\par"
                    s = s & "\tab FileMajorPart :\tab\tab\tab " & CStr(cP.FileMajorPart) & "\par"
                    s = s & "\tab FileMinorPart :\tab\tab\tab " & CStr(cP.FileMinorPart) & "\par"
                    s = s & "\tab FilePrivatePart :\tab\tab\tab " & CStr(cP.FilePrivatePart) & "\par"
                    s = s & "\tab IsDebug :\tab\tab\tab " & CStr(cP.IsDebug) & "\par"
                    s = s & "\tab IsPatched :\tab\tab\tab " & CStr(cP.IsPatched) & "\par"
                    s = s & "\tab IsPreRelease :\tab\tab\tab " & CStr(cP.IsPreRelease) & "\par"
                    s = s & "\tab IsPrivateBuild :\tab\tab\tab " & CStr(cP.IsPrivateBuild) & "\par"
                    s = s & "\tab IsSpecialBuild :\tab\tab\tab " & CStr(cP.IsSpecialBuild) & "\par"
                    s = s & "\tab PrivateBuild :\tab\tab\tab " & CStr(cP.PrivateBuild) & "\par"
                    s = s & "\tab ProductBuildPart :\tab\tab " & CStr(cP.ProductBuildPart) & "\par"
                    s = s & "\tab ProductMajorPart :\tab\tab " & CStr(cP.ProductMajorPart) & "\par"
                    s = s & "\tab ProductMinorPart :\tab\tab " & CStr(cP.ProductMinorPart) & "\par"
                    s = s & "\tab ProductPrivatePart :\tab\tab " & CStr(cP.ProductPrivatePart) & "\par"
                    s = s & "\tab ProductVersion :\tab\tab\tab " & CStr(cP.ProductVersion) & "\par"
                    s = s & "\tab SpecialBuild :\tab\tab\tab " & CStr(cP.SpecialBuild) & "\par"

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

            Else
                ' Error
                'Dim s As String = ""
                'Dim er As Exception = ex

                's = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                's = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                's = s & "\tab Message :\tab " & er.Message & "\par"
                's = s & "\tab Source :\tab\tab " & er.Source & "\par"
                'If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                's = s & "}"

                'rtb6.Rtf = s
            End If

        End If
    End Sub

    Private Sub txtSearchModule_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchModule.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvModules.Items
            If InStr(LCase(it.Text), LCase(Me.txtSearchModule.Text)) = 0 And _
                    InStr(LCase(it.SubItems(1).Text), LCase(Me.txtSearchModule.Text)) = 0 And _
                    InStr(LCase(it.SubItems(2).Text), LCase(Me.txtSearchModule.Text)) = 0 And _
                    InStr(LCase(it.SubItems(3).Text), LCase(Me.txtSearchModule.Text)) = 0 Then
                it.Group = lvModules.Groups(0)
            Else
                it.Group = lvModules.Groups(1)
            End If
        Next
        Me.lblModulesCount.Text = CStr(lvModules.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblModulesCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblModulesCount.Click
        Me.lvModules.Focus()
        Try
            Me.lvModules.EnsureVisible(Me.lvModules.Groups(1).Items(0).Index)
            Me.lvModules.SelectedItems.Clear()
            Me.lvModules.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lvThreads_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvThreads.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvThreads.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvThreads.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvThreads.Sort()
    End Sub

    Private Sub lvThreads_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvThreads.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvThreads)
    End Sub

    Private Sub lvThreads_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvThreads.SelectedIndexChanged
        ' New thread selected
        If lvThreads.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvThreads.SelectedItems.Item(0)

            If TypeOf it.Tag Is cThread Then

                Try
                    Dim cP As cThread = CType(it.Tag, cThread)

                    ' Description
                    Dim s As String = ""
                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Thread properties\b0\par"
                    s = s & "\tab Thread ID :\tab\tab\tab " & CStr(cP.Id) & "\par"
                    s = s & "\tab Process owner :\tab\tab\tab " & CStr(cP.ProcessId) & " -- " & cP.ProcessName & "\par"

                    s = s & "\tab Priority :\tab\tab\tab\tab " & cP.PriorityString & "\par"
                    s = s & "\tab Base priority :\tab\tab\tab " & CStr(cP.BasePriority) & "\par"
                    s = s & "\tab State :\tab\tab\tab\tab " & cP.ThreadState & "\par"
                    s = s & "\tab Wait reason :\tab\tab\tab " & cP.WaitReason & "\par"
                    s = s & "\tab Start address :\tab\tab\tab " & CStr(cP.StartAddress) & "\par"
                    s = s & "\tab PriorityBoostEnabled :\tab\tab " & CStr(cP.PriorityBoostEnabled) & "\par"
                    s = s & "\tab Start time :\tab\tab\tab " & cP.StartTime.ToLongDateString & " -- " & cP.StartTime.ToLongTimeString & "\par"
                    s = s & "\tab TotalProcessorTime :\tab\tab " & cP.TotalProcessorTime.ToString & "\par"
                    s = s & "\tab PrivilegedProcessorTime :\tab\tab " & cP.PrivilegedProcessorTime.ToString & "\par"
                    s = s & "\tab UserProcessorTime :\tab\tab " & CStr(cP.UserProcessorTime.ToString) & "\par"
                    s = s & "\tab ProcessorAffinity :\tab\tab " & CStr(cP.ProcessorAffinity) & "\par"

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

            Else
                ' Error
                'Dim s As String = ""
                'Dim er As Exception = ex

                's = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                's = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                's = s & "\tab Message :\tab " & er.Message & "\par"
                's = s & "\tab Source :\tab\tab " & er.Source & "\par"
                'If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                's = s & "}"

                'rtb4.Rtf = s
            End If

        End If
    End Sub

    Private Sub txtSearchThread_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchThread.TextChanged
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

    Private Sub lblThreadResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblThreadResults.Click
        Me.lvThreads.Focus()
        Try
            Me.lvThreads.EnsureVisible(Me.lvThreads.Groups(1).Items(0).Index)
            Me.lvThreads.SelectedItems.Clear()
            Me.lvThreads.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchHandle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchHandle.TextChanged
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

    Private Sub lblHandlesCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHandlesCount.Click
        Me.lvHandles.Focus()
        Try
            Me.lvHandles.EnsureVisible(Me.lvHandles.Groups(1).Items(0).Index)
            Me.lvHandles.SelectedItems.Clear()
            Me.lvHandles.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lvHandles_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvHandles.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvHandles.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvHandles.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvHandles.Sort()
    End Sub

    Private Sub lvHandles_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHandles.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvHandles)
    End Sub

    Private Sub txtSearchWindow_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchWindow.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvWindows.Items
            If InStr(LCase(it.SubItems(2).Text), LCase(Me.txtSearchWindow.Text)) = 0 Then
                it.Group = lvWindows.Groups(0)
            Else
                it.Group = lvWindows.Groups(1)
            End If
        Next
        Me.lblWindowsCount.Text = CStr(lvWindows.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblWindowsCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblWindowsCount.Click
        Me.lvWindows.Focus()
        Try
            Me.lvWindows.EnsureVisible(Me.lvWindows.Groups(1).Items(0).Index)
            Me.lvWindows.SelectedItems.Clear()
            Me.lvWindows.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lvWindows_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvWindows.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvWindows.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvWindows.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvWindows.Sort()
    End Sub

    Private Sub lvWindows_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvWindows.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvWindows)
    End Sub

    Private Sub lvWindows_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvWindows.SelectedIndexChanged
        ' New window selected
        If lvWindows.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvWindows.SelectedItems.Item(0)

            If TypeOf it.Tag Is cWindow Then

                Try
                    Dim cP As cWindow = CType(it.Tag, cWindow)

                    ' Description
                    Dim s As String = ""
                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Window properties\b0\par"
                    s = s & "\tab Window ID :\tab\tab\tab " & CStr(cP.Handle) & "\par"
                    s = s & "\tab Process owner :\tab\tab\tab " & CStr(cP.ParentProcessId) & " -- " & cP.ParentProcessName & " -- thread : " & CStr(cP.ParentThreadId) & "\par"
                    s = s & "\tab Caption :\tab\tab\tab " & cP.Caption & "\par"
                    s = s & "\tab Enabled :\tab\tab\tab " & CStr(cP.Enabled) & "\par"
                    s = s & "\tab Visible :\tab\tab\tab\tab " & CStr(cP.Visible) & "\par"
                    s = s & "\tab IsTask :\tab\tab\tab\tab " & CStr(cP.IsTask) & "\par"
                    s = s & "\tab Opacity :\tab\tab\tab " & CStr(cP.Opacity) & "\par"
                    s = s & "\tab Height :\tab\tab\tab\tab " & CStr(cP.Height) & "\par"
                    s = s & "\tab Left :\tab\tab\tab\tab " & CStr(cP.Left) & "\par"
                    s = s & "\tab Top :\tab\tab\tab\tab " & CStr(cP.Top) & "\par"
                    s = s & "\tab Width :\tab\tab\tab\tab " & CStr(cP.Width) & "\par"

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

            Else
                ' Error
                'Dim s As String = ""
                'Dim er As Exception = ex

                's = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                's = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                's = s & "\tab Message :\tab " & er.Message & "\par"
                's = s & "\tab Source :\tab\tab " & er.Source & "\par"
                'If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                's = s & "}"

                'rtb5.Rtf = s
            End If

        End If
    End Sub

    Private Sub txtSearchResults_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchResults.TextChanged
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

    Private Sub lblResultsCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblResultsCount.Click
        Me.lvSearchResults.Focus()
        Try
            Me.lvSearchResults.EnsureVisible(Me.lvSearchResults.Groups(1).Items(0).Index)
            Me.lvSearchResults.SelectedItems.Clear()
            Me.lvSearchResults.Groups(1).Items(0).Selected = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub chkAllWindows_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllWindows.CheckedChanged
        If windowsToRefresh IsNot Nothing Then Call ShowWindows()
    End Sub

    Private Sub butThreadSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butThreadSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "threads"
            Call My.Application.DoEvents()
            .ShowDialog()
        End With
    End Sub

    Private Sub butWindowSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindowSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "windows"
            Call My.Application.DoEvents()
            .ShowDialog()
        End With
    End Sub

    Private Sub butServiceReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceReport.Click
        Dim frm As New frmSaveReport
        With frm
            .ReportType = "services"
            Call My.Application.DoEvents()
            .ShowDialog()
        End With
    End Sub

    Private Sub butProcessShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessShowAll.Click

        ' We refresh all informations for the selected processes
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim it As ListViewItem

            Dim x As Integer = 0
            ReDim modulesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
            ReDim windowsToRefresh(Me.lvProcess.SelectedItems.Count - 1)
            ReDim handlesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
            ReDim threadsToRefresh(Me.lvProcess.SelectedItems.Count - 1)

            For Each it In Me.lvProcess.SelectedItems
                Dim cp As cProcess = CType(it.Tag, cProcess)
                modulesToRefresh(x) = cp.Pid
                windowsToRefresh(x) = cp.Pid
                handlesToRefresh(x) = cp.Pid
                threadsToRefresh(x) = cp.Pid
                x += 1
            Next

            Call ShowModules(False)
            Call ShowThreads(False)
            Call ShowWindows(False)
            Call ShowHandles(False)
        End If

    End Sub

    Private Sub ShowAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAllToolStripMenuItem.Click
        Call butProcessShowAll_Click(Nothing, Nothing)
    End Sub

    Private Sub ReadWriteMemoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadWriteMemoryToolStripMenuItem.Click
        Dim frm As New frmProcessMemRW
        With frm
            ' .SetProcess(5768)
            .ShowDialog()
        End With
    End Sub

    Private Sub butModuleGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butModuleGoogle.Click
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            My.Application.DoEvents()
            Try
                cFile.ShellOpenFile("http://www.google.com/search?hl=en&q=%22" & it.Text & "%22")
            Catch ex As Exception
                '
            End Try
        Next
    End Sub

    Private Sub GoogleSearchToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleSearchToolStripMenuItem2.Click
        Call butModuleGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub butAlwaysDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAlwaysDisplay.Click
        Me.butAlwaysDisplay.Checked = Not (Me.butAlwaysDisplay.Checked)
        Me.bAlwaysDisplay = Me.butAlwaysDisplay.Checked
        Me.TopMost = Me.bAlwaysDisplay
    End Sub

    Private Sub butPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPreferences.Click
        frmPreferences.ShowDialog()
    End Sub

    Public Sub TakeFullPower()
        clsOpenedHandles.EnableDebug()
        Me.lvProcess.Items.Clear()
        Me.tvProc.Nodes.Clear()
        Dim nn As New TreeNode
        nn.Text = "[System process]"
        nn.Tag = "0"
        Dim n2 As New TreeNode
        n2.Text = "System"
        n2.Tag = "4"
        nn.Nodes.Add(n2)
        Me.tvProc.Nodes.Add(nn)
        refreshProcessList()
    End Sub

    Private Sub rtb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtb.TextChanged
        Me.cmdInfosToClipB.Enabled = (Me.rtb.TextLength > 0)
    End Sub

    Private Sub chkDisplayNAProcess_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDisplayNAProcess.CheckedChanged
        If chkDisplayNAProcess.Checked = False Then
            Dim it As ListViewItem
            For Each it In Me.lvProcess.Items
                Dim cp As cProcess = CType(it.Tag, cProcess)
                If cp.Path = NO_INFO_RETRIEVED Then
                    it.Remove()
                End If
            Next
        Else
            Call Me.refreshProcessList()
        End If
    End Sub

    Private Sub pctBigIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctBigIcon.MouseDown
        Me.ToolStripMenuItem6.Enabled = (Me.pctBigIcon.Image IsNot Nothing)
    End Sub

    Private Sub pctSmallIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctSmallIcon.MouseDown
        Me.ToolStripMenuItem7.Enabled = (Me.pctSmallIcon.Image IsNot Nothing)
    End Sub

    Private Sub tabProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabProcess.SelectedIndexChanged
        Call lvProcess_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub refreshProcessTab(ByRef it As ListViewItem, ByRef cP As cProcess)

        ' General informations
        Select Case Me.tabProcess.SelectedTab.Text

            Case "General"
                Me.txtProcessPath.Text = cP.Path
                Me.txtProcessId.Text = CStr(cP.Pid)
                Me.txtParentProcess.Text = CStr(cP.ParentProcessId) & " -- " & cP.ParentProcessName
                Me.txtProcessStarted.Text = cP.StartTime.ToLongDateString & " -- " & cP.StartTime.ToLongTimeString
                Me.txtProcessUser.Text = cP.UserName
                Me.txtImageVersion.Text = cP.MainModule.FileVersionInfo.FileVersion
                Me.lblCopyright.Text = cP.MainModule.FileVersionInfo.LegalCopyright
                Me.lblDescription.Text = cP.MainModule.FileVersionInfo.FileDescription

            Case "Statistics"

                Me.lblProcOther.Text = GetFormatedSize(cP.GetIOvalues.OtherOperationCount)
                Me.lblProcOtherBytes.Text = GetFormatedSize(cP.GetIOvalues.OtherTransferCount)
                Me.lblProcReads.Text = GetFormatedSize(cP.GetIOvalues.ReadOperationCount)
                Me.lblProcReadBytes.Text = GetFormatedSize(cP.GetIOvalues.ReadTransferCount)
                Me.lblProcWriteBytes.Text = GetFormatedSize(cP.GetIOvalues.WriteTransferCount)
                Me.lblProcWrites.Text = GetFormatedSize(cP.GetIOvalues.WriteOperationCount)
                Me.lblGDIcount.Text = CStr(cP.GDIObjectsCount)
                Me.lblUserObjectsCount.Text = CStr(cP.UserObjectsCount)

                Dim mem As cProcess.PROCESS_MEMORY_COUNTERS = cP.MemoryInfos
                Me.lblHandles.Text = "00000000000"
                Dim ts As Date = cP.KernelTime
                Dim s As String = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblKernelTime.Text = s
                Me.lblPageFaults.Text = CStr(mem.PageFaultCount)
                Me.lblPageFileUsage.Text = GetFormatedSize(mem.PagefileUsage)
                Me.lblPeakPageFileUsage.Text = GetFormatedSize(mem.PeakPagefileUsage)
                Me.lblPeakWorkingSet.Text = GetFormatedSize(mem.PeakWorkingSetSize)
                ts = cP.ProcessorTime
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblTotalTime.Text = s
                ts = cP.UserTime
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblUserTime.Text = s
                Me.lblPriority.Text = cP.PriorityClass.ToString
                Me.lblWorkingSet.Text = GetFormatedSize(mem.WorkingSetSize)
                Me.lblQuotaNPP.Text = GetFormatedSize(mem.QuotaNonPagedPoolUsage)
                Me.lblQuotaPNPP.Text = GetFormatedSize(mem.QuotaPeakNonPagedPoolUsage)
                Me.lblQuotaPP.Text = GetFormatedSize(mem.QuotaPagedPoolUsage)
                Me.lblQuotaPPP.Text = GetFormatedSize(mem.QuotaPeakPagedPoolUsage)

            Case "Memory"

                Dim cRW As New cProcessMemRW(curProc.Pid)
                Dim reg() As cProcessMemRW.MEMORY_BASIC_INFORMATION = Nothing
                cRW.RetrieveMemRegions(reg)

                Me.lvProcMem.Items.Clear()
                ' Name / address / size / protection

                For Each mbi As cProcessMemRW.MEMORY_BASIC_INFORMATION In reg
                    If mbi.RegionSize > 0 Then
                        Dim newit As New ListViewItem("name")
                        Dim sub1 As New ListViewItem.ListViewSubItem
                        Dim sub2 As New ListViewItem.ListViewSubItem
                        Dim sub3 As New ListViewItem.ListViewSubItem
                        Dim sub4 As New ListViewItem.ListViewSubItem
                        Dim sub5 As New ListViewItem.ListViewSubItem
                        sub1.Text = CStr(mbi.BaseAddress)
                        sub2.Text = CStr(mbi.RegionSize)
                        sub3.Text = cProcessMemRW.GetProtectionType(mbi.Protect)
                        sub4.Text = cProcessMemRW.GetStateType(mbi.State)
                        sub5.Text = cProcessMemRW.GetTypeType(mbi.lType)
                        newit.SubItems.Add(sub1)
                        newit.SubItems.Add(sub2)
                        newit.SubItems.Add(sub3)
                        newit.SubItems.Add(sub4)
                        newit.SubItems.Add(sub5)
                        Me.lvProcMem.Items.Add(newit)
                    End If
                Next


            Case "Services"
                ' Associated services
                Dim bServRef As Boolean = Me.timerServices.Enabled
                Me.timerServices.Enabled = False

                Me.lvProcServices.Items.Clear()
                For Each lvi As ListViewItem In Me.lvServices.Items
                    Dim cServ As cService = CType(lvi.Tag, cService)
                    Dim pid As Integer = cServ.ProcessID
                    If pid = cP.Pid And pid > 0 Then
                        Dim newIt As New ListViewItem(cServ.Name)
                        Dim sub1 As New ListViewItem.ListViewSubItem
                        Dim sub2 As New ListViewItem.ListViewSubItem
                        Dim sub3 As New ListViewItem.ListViewSubItem
                        sub1.Text = cServ.Status.ToString
                        sub2.Text = cServ.LongName
                        sub3.Text = cServ.ImagePath
                        newIt.SubItems.Add(sub1)
                        newIt.SubItems.Add(sub2)
                        newIt.SubItems.Add(sub3)
                        newIt.ImageIndex = 7

                        Me.lvProcServices.Items.Add(newIt)
                    End If
                Next


                Me.timerServices.Enabled = bServRef

            Case "Token"

                ' Privileges
                Dim cPriv As New cPrivileges(cP.Pid)
                Dim lPriv() As cPrivileges.PrivilegeInfo = cPriv.GetPrivilegesList

                Me.lvPrivileges.Items.Clear()

                For Each l As cPrivileges.PrivilegeInfo In lPriv
                    Dim newIt As New ListViewItem(l.Name)
                    Dim sub1 As New ListViewItem.ListViewSubItem
                    sub1.Text = cPrivileges.PrivilegeStatusToString(l.Status)
                    Dim sub2 As New ListViewItem.ListViewSubItem
                    sub2.Text = cPrivileges.GetPrivilegeDescription(l.Name)
                    newIt.SubItems.Add(sub1)
                    newIt.SubItems.Add(sub2)
                    newIt.BackColor = cPrivileges.GetColorFromStatus(l.Status)
                    Me.lvPrivileges.Items.Add(newIt)
                Next


            Case "Informations"

                ' Description
                Try
                    Dim mainModule As System.Diagnostics.ProcessModule = cP.MainModule
                    Dim pmc As cProcess.PROCESS_MEMORY_COUNTERS = cP.MemoryInfos
                    Dim pid As Integer = cP.Pid
                    Dim s As String = ""
                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b File properties\b0\par"
                    s = s & "\tab File name :\tab\tab\tab " & cP.Name & "\par"
                    s = s & "\tab Path :\tab\tab\tab\tab " & Replace(cP.Path, "\", "\\") & "\par"
                    s = s & "\tab Description :\tab\tab\tab " & mainModule.FileVersionInfo.FileDescription & "\par"
                    s = s & "\tab Company name :\tab\tab\tab " & mainModule.FileVersionInfo.CompanyName & "\par"
                    s = s & "\tab Version :\tab\tab\tab " & mainModule.FileVersionInfo.FileVersion & "\par"
                    s = s & "\tab Copyright :\tab\tab\tab " & mainModule.FileVersionInfo.LegalCopyright & "\par"
                    s = s & "\par"
                    s = s & "  \b Process description\b0\par"
                    s = s & "\tab PID :\tab\tab\tab\tab " & CStr(cP.Pid) & "\par"
                    s = s & "\tab Start time :\tab\tab\tab " & cP.StartTime.ToLongDateString & " -- " & cP.StartTime.ToLongTimeString & "\par"
                    s = s & "\tab Priority :\tab\tab\tab\tab " & cP.PriorityClass.ToString & "\par"
                    s = s & "\tab User :\tab\tab\tab\tab " & cP.UserName & "\par"
                    Dim ts As Date = cP.ProcessorTime
                    Dim proctime As String = String.Format("{0:00}", ts.Hour) & ":" & _
                        String.Format("{0:00}", ts.Minute) & ":" & _
                        String.Format("{0:00}", ts.Second) & ":" & _
                        String.Format("{000}", ts.Millisecond)
                    s = s & "\tab Processor time :\tab\tab\tab " & proctime & "\par"
                    s = s & "\tab Memory :\tab\tab\tab " & CStr(pmc.WorkingSetSize / 1024) & " Kb" & "\par"
                    s = s & "\tab Memory peak :\tab\tab\tab " & CStr(pmc.PeakWorkingSetSize / 1024) & " Kb" & "\par"
                    s = s & "\tab Page faults :\tab\tab\tab " & CStr(pmc.PageFaultCount) & "\par"
                    s = s & "\tab Page file usage :\tab\tab\tab " & CStr(pmc.PagefileUsage / 1024) & " Kb" & "\par"
                    s = s & "\tab Peak page file usage :\tab\tab " & CStr(pmc.PeakPagefileUsage / 1024) & " Kb" & "\par"
                    s = s & "\tab QuotaPagedPoolUsage :\tab\tab " & CStr(Math.Round(pmc.QuotaPagedPoolUsage / 1024, 3)) & " Kb" & "\par"
                    s = s & "\tab QuotaPeakPagedPoolUsage :\tab " & CStr(Math.Round(pmc.QuotaPeakPagedPoolUsage / 1024, 3)) & " Kb" & "\par"
                    s = s & "\tab QuotaNonPagedPoolUsage :\tab " & CStr(Math.Round(pmc.QuotaNonPagedPoolUsage / 1024, 3)) & " Kb" & "\par"
                    s = s & "\tab QuotaPeakNonPagedPoolUsage :\tab " & CStr(Math.Round(pmc.QuotaPeakNonPagedPoolUsage / 1024, 3)) & " Kb" & "\par"

                    If chkOnline.Checked Then
                        ' Retrieve online description
                        s = s & "\par"
                        s = s & "  \b On line informations\b0\par"

                        Dim ipi As InternetProcessInfo = mdlInternet.GetInternetInfos(cP.Name)

                        s = s & "\tab Security risk (0-5) :\tab\tab " & CStr(ipi._Risk) & "\par"
                        s = s & "\tab Description :\tab\tab\tab " & Replace$(ipi._Description, vbNewLine, "\par") & "\par"
                    End If

                    If chkModules.Checked Then
                        ' Retrieve modules
                        s = s & "\par"
                        s = s & "  \b Loaded modules\b0\par"
                        Dim m As ProcessModule
                        Dim mdl As ProcessModuleCollection = cP.Modules
                        s = s & "\tab " & CStr(mdl.Count) & " modules loaded" & "\par"
                        For Each m In mdl
                            s = s & "\tab " & Replace(m.FileVersionInfo.FileName, "\", "\\") & "\par"
                        Next

                        ' Retrieve threads infos
                        s = s & "\par"
                        s = s & "  \b Threads\b0\par"
                        Dim pt As ProcessThread
                        Dim thr As System.Diagnostics.ProcessThreadCollection = cP.Threads
                        s = s & "\tab " & CStr(thr.Count) & " threads \par"
                        For Each pt In thr
                            s = s & "\tab " & CStr(pt.Id) & "\par"
                            s = s & "\tab\tab " & "Priority level : " & CStr(pt.PriorityLevel.ToString) & "\par"
                            Dim tsp As TimeSpan = pt.TotalProcessorTime
                            Dim s2 As String = String.Format("{0:00}", tsp.TotalHours) & ":" & _
                                String.Format("{0:00}", tsp.Minutes) & ":" & _
                                String.Format("{0:00}", tsp.Seconds)
                            s = s & "\tab\tab " & "Start address : " & CStr(pt.StartAddress) & "\par"
                            s = s & "\tab\tab " & "Start time : " & pt.StartTime.ToLongDateString & " -- " & pt.StartTime.ToLongTimeString & "\par"
                            s = s & "\tab\tab " & "State : " & CStr(pt.ThreadState.ToString) & "\par"
                            s = s & "\tab\tab " & "Processor time : " & s2 & "\par"
                        Next
                    End If

                    If chkHandles.Checked Then
                        ' Retrieve handles
                        s = s & "\par"
                        s = s & "  \b Loaded handles\b0\par"
                        Dim i As Integer
                        handles_Renamed.Refresh()
                        For i = 0 To handles_Renamed.Count - 1
                            With handles_Renamed
                                If (.GetProcessID(i) = pid) And (Len(.GetObjectName(i)) > 0) Then
                                    s = s & "\tab " & .GetNameInformation(i) & " : " & Replace(.GetObjectName(i), "\", "\\") & "\par"
                                End If
                            End With
                        Next
                    End If

                    s = s & "}"

                    rtb.Rtf = s

                Catch ex As Exception

                    Dim s As String = ""
                    Dim er As Exception = ex

                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                    s = s & "\tab Message :\tab " & er.Message & "\par"
                    s = s & "\tab Source :\tab\tab " & er.Source & "\par"
                    If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                    s = s & "}"

                    rtb.Rtf = s

                    pctSmallIcon.Image = Me.imgProcess.Images("noicon")
                    pctBigIcon.Image = Me.imgMain.Images("noicon32")

                End Try

        End Select
    End Sub

    Private Sub cmdInfosToClipB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInfosToClipB.Click
        If Me.rtb.Text.Length > 0 Then
            My.Computer.Clipboard.SetText(Me.rtb.Text, TextDataFormat.Text)
        End If
    End Sub

    Private Sub cmdInfosToClipB_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdInfosToClipB.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.rtb.Rtf.Length > 0 Then
                My.Computer.Clipboard.SetText(Me.rtb.Rtf, TextDataFormat.Rtf)
            End If
        End If
    End Sub

    Private Sub butProcessPermuteLvTv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessPermuteLvTv.Click
        Static _oldProcessColumnWidth As Integer = 100
        If butProcessPermuteLvTv.Text = "Listview" Then
            Me.SplitContainerTvLv.Panel1Collapsed = True
            'Me.lvProcess.ShowGroups = True
            Me.lvProcess.Columns(0).Width = _oldProcessColumnWidth
            butProcessPermuteLvTv.Image = My.Resources.tv2
            butProcessPermuteLvTv.Text = "Treeview"
        Else
            Me.SplitContainerTvLv.Panel1Collapsed = False
            ' Me.lvProcess.ShowGroups = False
            _oldProcessColumnWidth = Me.lvProcess.Columns(0).Width
            Me.lvProcess.Columns(0).Width = 0
            butProcessPermuteLvTv.Text = "Listview"
            butProcessPermuteLvTv.Image = My.Resources.lv3
        End If
    End Sub

    Private Sub lvProcess_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvProcess.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvProcess.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvProcess.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvProcess.Sort()
    End Sub

    Private Sub lvProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim frm As New frmChooseProcessColumns
            With frm
                '.SetAutoScrollMargin()
                Call .ShowDialog()
            End With
        End If
    End Sub

    Private Sub lvProcess_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvProcess)
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As Integer = -1
            If Me.lvProcess.SelectedItems Is Nothing Then
                Me.IdleToolStripMenuItem.Checked = False
                Me.NormalToolStripMenuItem.Checked = False
                Me.AboveNormalToolStripMenuItem.Checked = False
                Me.BelowNormalToolStripMenuItem.Checked = False
                Me.HighToolStripMenuItem.Checked = False
                Me.RealTimeToolStripMenuItem.Checked = False
                Exit Sub
            End If
            If Me.lvProcess.SelectedItems.Count = 1 Then
                p = CType(Me.lvProcess.SelectedItems(0).Tag, cProcess).PriorityClassConstant
            End If
            Me.IdleToolStripMenuItem.Checked = (p = ProcessPriorityClass.Idle)
            Me.NormalToolStripMenuItem.Checked = (p = ProcessPriorityClass.Normal)
            Me.AboveNormalToolStripMenuItem.Checked = (p = ProcessPriorityClass.AboveNormal)
            Me.BelowNormalToolStripMenuItem.Checked = (p = ProcessPriorityClass.BelowNormal)
            Me.HighToolStripMenuItem.Checked = (p = ProcessPriorityClass.High)
            Me.RealTimeToolStripMenuItem.Checked = (p = ProcessPriorityClass.RealTime)
        End If

    End Sub

    Private Sub lvProcess_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseUp
        Call lvProcess_MouseDown(Nothing, e)
    End Sub

    Private Sub lvProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvProcess.SelectedIndexChanged
        ' New process selected
        Static _path As String = ""

        If lvProcess.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvProcess.SelectedItems.Item(0)

            If TypeOf it.Tag Is cProcess Then

                Try
                    Dim cP As cProcess = CType(it.Tag, cProcess)
                    Dim pid As Integer = cP.Pid

                    If curProc Is Nothing OrElse cP.Pid <> curProc.Pid Then
                        curProc = cP
                        Me.graphCPU.ClearValue()
                        Me.graphIO.ClearValue()
                        Me.graphMemory.ClearValue()
                    End If

                    ' Icons
                    If pctBigIcon.Image Is Nothing Or Not (_path = cP.Path) Then
                        Try
                            _path = cP.Path
                            pctBigIcon.Image = GetIcon(cP.Path, False).ToBitmap
                            pctSmallIcon.Image = GetIcon(cP.Path, True).ToBitmap
                        Catch ex As Exception
                            pctSmallIcon.Image = Me.imgProcess.Images("noicon")
                            pctBigIcon.Image = Me.imgMain.Images("noicon32")
                        End Try
                    End If


                    Call refreshProcessTab(it, cP)

                Catch ex As Exception
                    Me.txtProcessPath.Text = "Unable to retrieve path"

                End Try

            Else
                ' Error
                Me.txtProcessPath.Text = "Unable to retrieve path"
            End If

        End If
    End Sub

    Private Sub butProcessDisplayDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessDisplayDetails.Click
        If butProcessDisplayDetails.Text = "Hide details" Then
            Me.SplitContainerProcess.Panel2Collapsed = True
            butProcessDisplayDetails.Image = My.Resources.showDetails
            butProcessDisplayDetails.Text = "Show details"
        Else
            Me.SplitContainerProcess.Panel2Collapsed = False
            butProcessDisplayDetails.Text = "Hide details"
            butProcessDisplayDetails.Image = My.Resources.hideDetails
        End If
    End Sub

    ' Add a process node
    Private Sub addNewProcessNode(ByRef p As cProcess, ByVal imgkey As String)
        Dim parent As Integer = p.ParentProcessId
        Dim pid As Integer = p.Pid

        Dim n As TreeNode = findNode(Me.tvProc.Nodes(0).Nodes, parent)

        Dim nn As New TreeNode
        nn.Text = p.Name
        nn.Tag = CStr(pid)
        nn.ImageKey = imgkey
        nn.SelectedImageKey = imgkey

        If n Is Nothing Then
            ' New node (parent was killed)
            Me.tvProc.Nodes(0).Nodes.Add(nn)
            Me.tvProc.Nodes(0).ExpandAll()
        Else
            ' Found parent
            n.Nodes.Add(nn)
            n.ExpandAll()
        End If

    End Sub

    Private Function findNode(ByRef nodes As TreeNodeCollection, ByVal pid As Integer) As TreeNode
        Dim n As TreeNode
        For Each n In nodes
            If n.Tag.ToString = CStr(pid) Then
                Return n
            Else
                findNode(n.Nodes, pid)
            End If
        Next
        Return Nothing
    End Function

    Private Sub tvProc_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvProc.AfterCollapse
        Me.lvProcess.Items(0).Group = Me.lvProcess.Groups(1)
    End Sub

    Private Sub cmdShowFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFileDetails.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim cp As cProcess = CType(Me.lvProcess.SelectedItems.Item(0).Tag, cProcess)
            Dim s As String = cp.Path
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub cmdShowFileProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFileProperties.Click
        ' File properties for selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Dim cp As cProcess = CType(it.Tag, cProcess)
            If IO.File.Exists(cp.Path) Then
                cFile.ShowFileProperty(cp.Path)
            End If
        Next
    End Sub

    Private Sub cmdOpenDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenDirectory.Click
        ' Open directory of selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            Dim cp As cProcess = CType(it.Tag, cProcess)
            If cp.Path <> NO_INFO_RETRIEVED Then
                cFile.OpenDirectory(cp.Path)
            End If
        Next
    End Sub

    Private Sub tvProc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvProc.DoubleClick
        Dim i As Integer = Me.lvProcess.View
        i += 1
        If i = 5 Then i = 0
        Me.lvProcess.View = CType(i, View)
    End Sub

    Private Sub tvProc_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvProc.AfterSelect
        creg = New cRegMonitor(cRegMonitor.KEY_TYPE.HKEY_LOCAL_MACHINE, "SYSTEM\CurrentControlSet\Services", _
                cRegMonitor.KEY_MONITORING_TYPE.REG_NOTIFY_CHANGE_NAME)
    End Sub

    Private Sub creg_KeyAdded(ByVal key As cRegMonitor.KeyDefinition) Handles creg.KeyAdded
        log.AppendLine("Service added : " & key.name)
        With Me.Tray
            .BalloonTipText = key.name
            .BalloonTipIcon = ToolTipIcon.Info
            .BalloonTipTitle = "A new service has been created"
            .ShowBalloonTip(3000)
        End With
    End Sub

    Private Sub creg_KeyDeleted(ByVal key As cRegMonitor.KeyDefinition) Handles creg.KeyDeleted
        log.AppendLine("Service deleted : " & key.name)
        With Me.Tray
            .BalloonTipText = key.name
            .BalloonTipIcon = ToolTipIcon.Info
            .BalloonTipTitle = "A service has been deleted"
            .ShowBalloonTip(3000)
        End With
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub MinimizeToTrayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizeToTrayToolStripMenuItem.Click
        Me.Hide()
        Me.Visible = False
    End Sub

    Private Sub ShowLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowLogToolStripMenuItem.Click
        frmLog.Show()
    End Sub

    Private Sub EnableProcessRefreshingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableProcessRefreshingToolStripMenuItem.Click
        Me.EnableProcessRefreshingToolStripMenuItem.Checked = Not (Me.EnableProcessRefreshingToolStripMenuItem.Checked)
        Me.timerProcess.Enabled = Me.EnableProcessRefreshingToolStripMenuItem.Checked
    End Sub

    Private Sub EnableServiceRefreshingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableServiceRefreshingToolStripMenuItem.Click
        Me.EnableServiceRefreshingToolStripMenuItem.Checked = Not (Me.EnableServiceRefreshingToolStripMenuItem.Checked)
        Me.timerServices.Enabled = Me.EnableServiceRefreshingToolStripMenuItem.Checked
    End Sub

    Private Sub ToolStripMenuItem44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem44.Click
        Dim pid As Integer = 0
        If lvProcess.SelectedItems.Count = 1 Then
            Dim ite As ListViewItem = lvProcess.SelectedItems.Item(0)
            If TypeOf ite.Tag Is cProcess Then
                Dim cP As cProcess = CType(ite.Tag, cProcess)
                pid = cP.Pid
            End If
        End If

        If pid < 4 Then Exit Sub

        Dim cPriv As New cPrivileges(pid)
        Dim it As ListViewItem
        For Each it In Me.lvPrivileges.SelectedItems
            cPriv.Privilege(it.Text) = cPrivileges.PrivilegeStatus.PRIVILEGE_ENABLED
            it.SubItems(1).Text = cPrivileges.PrivilegeStatusToString(cPriv.Privilege(it.Text))
            it.BackColor = cPrivileges.GetColorFromStatus(cPriv.Privilege(it.Text))
        Next
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        Dim pid As Integer = 0
        If lvProcess.SelectedItems.Count = 1 Then
            Dim ite As ListViewItem = lvProcess.SelectedItems.Item(0)
            If TypeOf ite.Tag Is cProcess Then
                Dim cP As cProcess = CType(ite.Tag, cProcess)
                pid = cP.Pid
            End If
        End If

        If pid < 4 Then Exit Sub

        Dim cPriv As New cPrivileges(pid)
        Dim it As ListViewItem
        For Each it In Me.lvPrivileges.SelectedItems
            cPriv.Privilege(it.Text) = cPrivileges.PrivilegeStatus.PRIVILEGE_DISBALED
            it.SubItems(1).Text = cPrivileges.PrivilegeStatusToString(cPriv.Privilege(it.Text))
            it.BackColor = cPrivileges.GetColorFromStatus(cPriv.Privilege(it.Text))
        Next
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        Dim pid As Integer = 0
        If lvProcess.SelectedItems.Count = 1 Then
            Dim ite As ListViewItem = lvProcess.SelectedItems.Item(0)
            If TypeOf ite.Tag Is cProcess Then
                Dim cP As cProcess = CType(ite.Tag, cProcess)
                pid = cP.Pid
            End If
        End If

        If pid < 4 Then Exit Sub

        Dim cPriv As New cPrivileges(pid)
        Dim it As ListViewItem
        For Each it In Me.lvPrivileges.SelectedItems
            cPriv.Privilege(it.Text) = cPrivileges.PrivilegeStatus.PRIVILEGE_REMOVED
            it.SubItems(1).Text = cPrivileges.PrivilegeStatusToString(cPriv.Privilege(it.Text))
            it.BackColor = cPrivileges.GetColorFromStatus(cPriv.Privilege(it.Text))
        Next
    End Sub
	
	Private Sub lvPrivileges_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvPrivileges.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvPrivileges.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvPrivileges.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvPrivileges.Sort()
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

    Private Sub rtb2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb2.TextChanged
        Me.cmdCopyServiceToCp.Enabled = (rtb2.Rtf.Length > 0)
    End Sub

    Private Sub lvServices_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvServices.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lvServices.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lvServices.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lvServices.Sort()
    End Sub

    Private Sub lvServices_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseDown
        Call mdlMisc.CopyLvToClip(e, Me.lvServices)
    End Sub

    Private Sub lvServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseUp
        'If lvServices.SelectedItems.Count = -1 Then
        '    Dim s As String = lvServices.SelectedItems.Item(0).SubItems(2).Text
        '    Dim s2 As String = lvServices.SelectedItems.Item(0).SubItems(3).Text
        '    Dim s3 As String = lvServices.SelectedItems.Item(0).SubItems(5).Text
        '    ToolStripMenuItem9.Text = CStr(IIf(s = "Running", "Pause", "Resume"))
        '    ToolStripMenuItem9.Enabled = ((InStr(s3, "Pause") + InStr(s3, "Resume")) > 0)
        '    ToolStripMenuItem11.Enabled = (s3.Length = 0)
        '    ToolStripMenuItem10.Enabled = (InStr(s3, "Stop") > 0)
        '    ShutdownToolStripMenuItem.Enabled = (InStr(s3, "Shutdown") > 0)
        '    ToolStripMenuItem13.Checked = (s2 = "Disabled")
        '    ToolStripMenuItem13.Enabled = Not (ToolStripMenuItem13.Checked)
        '    ToolStripMenuItem14.Checked = (s2 = "Auto Start")
        '    ToolStripMenuItem14.Enabled = Not (ToolStripMenuItem14.Checked)
        '    ToolStripMenuItem15.Checked = (s2 = "Demand Start")
        '    ToolStripMenuItem15.Enabled = Not (ToolStripMenuItem15.Checked)
        'ElseIf lvServices.SelectedItems.Count > 1 Then
        ToolStripMenuItem9.Text = "Pause"
        ToolStripMenuItem9.Enabled = True
        ToolStripMenuItem11.Enabled = True
        ToolStripMenuItem10.Enabled = True
        ShutdownToolStripMenuItem.Enabled = True
        ToolStripMenuItem13.Checked = True
        ToolStripMenuItem13.Enabled = True
        ToolStripMenuItem14.Checked = True
        ToolStripMenuItem14.Enabled = True
        ToolStripMenuItem15.Checked = True
        ToolStripMenuItem15.Enabled = True
        'End If
    End Sub

    Private Sub lvServices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvServices.SelectedIndexChanged
        ' New service selected
        If lvServices.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvServices.SelectedItems.Item(0)
            Try
                Dim cS As cService = CType(it.Tag, cService)

                Me.lblServiceName.Text = "Service name : " & cS.Name
                Me.lblServicePath.Text = "Service path : " & cS.ImagePath

                ' Description
                Dim s As String = vbNullString
                Dim description As String = vbNullString
                Dim diagnosticsMessageFile As String = cS.DiagnosticsMessageFile
                Dim group As String = cS.Group
                Dim objectName As String = cS.ObjectName
                Dim tag As String = vbNullString

                ' OK it's not the best way to retrive the description...
                s = cS.ImagePath
                Dim sTemp As String = cS.Description
                If InStr(sTemp, "@", CompareMethod.Binary) > 0 Then
                    description = cFile.IntelligentPathRetrieving(sTemp)
                Else
                    description = sTemp
                End If

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Service properties\b0\par"
                s = s & "\tab Name :\tab\tab\tab " & cS.Name & "\par"
                s = s & "\tab Common name :\tab\tab " & cS.LongName & "\par"
                If Len(it.SubItems(4).Text) > 0 Then s = s & "\tab Path :\tab\tab\tab " & Replace(cS.ImagePath, "\", "\\") & "\par"
                If Len(description) > 0 Then s = s & "\tab Description :\tab\tab " & description & "\par"
                If Len(group) > 0 Then s = s & "\tab Group :\tab\tab\tab " & group & "\par"
                If Len(objectName) > 0 Then s = s & "\tab ObjectName :\tab\tab " & objectName & "\par"
                If Len(diagnosticsMessageFile) > 0 Then s = s & "\tab DiagnosticsMessageFile :\tab\tab " & diagnosticsMessageFile & "\par"
                s = s & "\tab State :\tab\tab\tab " & cS.Status.ToString & "\par"
                s = s & "\tab Startup :\tab\tab " & cS.ServiceStartType.ToString & "\par"
                If cS.ProcessID > 0 Then s = s & "\tab Owner process :\tab\tab " & cS.ProcessID & "-- " & cFile.GetFileName(cProcess.GetPath(cS.ProcessID)) & "\par"
                s = s & "\tab Service type :\tab\tab " & cS.Type & "\par"

                s = s & "}"

                rtb2.Rtf = s

                ' Treeviews stuffs
                Dim n As New TreeNode
                Dim n3 As New TreeNode
                n.Text = "Dependencies"
                n3.Text = "Depends on"

                tv.Nodes.Clear()
                tv.Nodes.Add(n)
                tv2.Nodes.Clear()
                tv2.Nodes.Add(n3)

                n.Expand()
                n3.Expand()

                Dim o As System.ServiceProcess.ServiceController() = System.ServiceProcess.ServiceController.GetServices()
                Dim o1 As System.ServiceProcess.ServiceController

                For Each o1 In o
                    If o1.ServiceName = it.Text Then
                        ' Here we have 2 recursive methods to add nodes to treeview
                        addDependentServices(o1, n)
                        addServicesDependedOn(o1, n3)
                        Exit For
                    End If
                Next

                If n.Nodes.Count > 0 Then
                    n.ImageKey = "ko"
                    n.SelectedImageKey = "ko"
                Else
                    n.ImageKey = "ok"
                    n.SelectedImageKey = "ok"
                End If
                If n3.Nodes.Count > 0 Then
                    n3.ImageKey = "ko"
                    n3.SelectedImageKey = "ko"
                Else
                    n3.ImageKey = "ok"
                    n3.SelectedImageKey = "ok"
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

    Private Sub SelectedAssociatedProcessToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedAssociatedProcessToolStripMenuItem1.Click
        ' Select processes associated to selected handles results
        Dim it As ListViewItem
        If Me.lvServices.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvServices.SelectedItems
            Try
                Dim sp As String = it.SubItems(5).Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As String = sp.Substring(0, i - 1)
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvProcess.Items
                        Dim cp As cProcess = CType(it2.Tag, cProcess)
                        If CStr(cp.Pid) = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
            Catch ex As Exception
                '
            End Try
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem43.Click
        ' Select services associated to selected process
        Dim it As ListViewItem
        If Me.lvProcServices.SelectedItems.Count > 0 Then Me.lvServices.SelectedItems.Clear()
        For Each it In Me.lvProcServices.SelectedItems
            Dim it2 As ListViewItem
            For Each it2 In Me.lvServices.Items
                Dim cp As cService = CType(it2.Tag, cService)
                If cp.Name = it.Text Then
                    it2.Selected = True
                End If
            Next
        Next
        Me.Ribbon.ActiveTab = Me.ServiceTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub timerProcPerf_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timerProcPerf.Tick
        Dim z As Double = curProc.CpuPercentageUsage
        If Double.IsNegativeInfinity(z) Then z = 0
        Me.graphCPU.AddValue(z * 100)
        Me.graphMemory.AddValue(curProc.MemoryInfos.WorkingSetSize)
        Me.graphIO.AddValue(curProc.GetIOvalues.ReadTransferCount)
        Me.graphCPU.Refresh()
        Me.graphIO.Refresh()
        Me.graphMemory.Refresh()
    End Sub

    Private Sub ShowSystemInformatoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowSystemInformatoToolStripMenuItem.Click
        frmSystemInfo.Show()
    End Sub
End Class
