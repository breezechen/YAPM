Option Strict On

Public Class frmMain

    ' ========================================
    ' Private attributes
    ' ========================================
    Private m_SortingColumn As ColumnHeader
    Private bProcessHover As Boolean = True
    Private bServiceHover As Boolean = False
    Private bEnableJobs As Boolean = True
    Private _stopOnlineRetrieving As Boolean = False
    Private handlesToRefresh() As Integer
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
    Private Declare Sub InvalidateRect Lib "user32" (ByVal hWnd As Integer, ByVal t As Integer, ByVal bErase As Integer)
    Private Declare Sub ValidateRect Lib "user32" (ByVal hWnd As Integer, ByVal t As Integer)
    Private Declare Function GetTickCount Lib "kernel32" () As Integer
    Private Declare Unicode Function SetWindowTheme Lib "uxtheme.dll" (ByVal hWnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer

    ' ========================================
    ' Constants
    ' ========================================
    Public HELP_PATH As String = My.Application.Info.DirectoryPath & "\Help\help.htm"
    Public PREF_PATH As String = My.Application.Info.DirectoryPath & "\Config\config.xml"
    Public Const DEFAULT_TIMER_INTERVAL_PROCESSES As Integer = 2500
    Public Const DEFAULT_TIMER_INTERVAL_SERVICES As Integer = 15000
    Public Const MSGFIRSTTIME As String = "This is the first time you run YAPM. Please remember that it is a beta1 version so there are some bugs and some missing functionnalities :-)" & vbNewLine & vbNewLine & "You should run YAPM as an administrator in order to fully control your processes. If you are an administrator, you can enable all functionnalities of YAPM by clicking on the 'Take full power' button in the 'Misc' pannel." & vbNewLine & "This enable you to fully control all your processes, including system processes. Please take care using this function because you will be able to do some irreversible things if you kill or modify some system processes... Use it at your own risks !" & vbNewLine & vbNewLine & "Please let me know any of your ideas of improvement or new functionnalities in YAPM's sourceforge.net project page ('Help' pannel) :-)" & vbNewLine & vbNewLine & "This message won't be shown anymore :-)"


    ' ========================================
    ' Form functions
    ' ========================================

    ' Refresh File informations
    Private Sub refreshFileInfos(ByVal file As String)

        Dim s As String = ""

        cSelFile = New cFile(file, True)

        If IO.File.Exists(file) Then

            ' Set dates to datepickers
            Me.DTcreation.Value = cSelFile.GetCreationTime
            Me.DTlastAccess.Value = cSelFile.GetLastAccessTime
            Me.DTlastModification.Value = cSelFile.GetLastWriteTime

            ' Set attributes
            Dim att As System.IO.FileAttributes = cSelFile.GetAttributes()
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
            s &= "\tab File name :\tab\tab " & cSelFile.GetName & "\par"
            s &= "\tab Parent directory :\tab " & cSelFile.GetParentDirectory & "\par"
            s &= "\tab Extension :\tab\tab " & cSelFile.GetFileExtension & "\par"
            s &= "\tab Creation date :\tab\tab " & cSelFile.GetDateCreated & "\par"
            s &= "\tab Last access date :\tab " & cSelFile.GetDateLastAccessed & "\par"
            s &= "\tab Last modification date :\tab " & cSelFile.GetDateLastModified & "\par"
            s &= "\tab Size :\tab\tab\tab " & cSelFile.GetFileSize & " Bytes -- " & Math.Round(cSelFile.GetFileSize / 1024, 3) & " KB" & " -- " & Math.Round(cSelFile.GetFileSize / 1024 / 1024, 3) & "MB\par"
            s &= "\tab Compressed size :\tab " & cSelFile.GetCompressedFileSize & " Bytes -- " & Math.Round(cSelFile.GetCompressedFileSize / 1024, 3) & " KB" & " -- " & Math.Round(cSelFile.GetCompressedFileSize / 1024 / 1024, 3) & "MB\par\par"
            s &= "\b File advances properties\b0\par"
            s &= "\tab File type :\tab\tab " & cSelFile.GetFileType & "\par"
            s &= "\tab Associated program :\tab " & cSelFile.GetFileAssociatedProgram & "\par"
            s &= "\tab Short name :\tab\tab " & cSelFile.GetShortName & "\par"
            s &= "\tab Short path :\tab\tab " & cSelFile.GetShortPath & "\par"
            s &= "\tab Directory depth :\tab\tab " & cSelFile.GetDirectoryDepth & "\par"
            s &= "\tab File available for read :\tab " & cSelFile.GetFileAvailableForWrite & "\par"
            s &= "\tab File available for write :\tab " & cSelFile.GetFileAvailableForWrite & "\par\par"
            s &= "\b Attributes\b0\par"
            s &= "\tab Archive :\tab\tab " & cSelFile.GetIsArchive & "\par"
            s &= "\tab Compressed :\tab\tab " & cSelFile.GetIsCompressed & "\par"
            s &= "\tab Device :\tab\tab\tab " & cSelFile.getisDevice & "\par"
            s &= "\tab Directory :\tab\tab " & cSelFile.GetIsDirectory & "\par"
            s &= "\tab Encrypted :\tab\tab " & cSelFile.GetIsEncrypted & "\par"
            s &= "\tab Hidden :\tab\tab\tab " & cSelFile.GetIsHidden & "\par"
            s &= "\tab Normal :\tab\tab\tab " & cSelFile.GetIsNormal & "\par"
            s &= "\tab Not content indexed :\tab " & cSelFile.GetIsNotContentIndexed & "\par"
            s &= "\tab Offline :\tab\tab\tab " & cSelFile.GetIsOffline & "\par"
            s &= "\tab Read only :\tab\tab " & cSelFile.GetIsReadOnly & "\par"
            s &= "\tab Reparse file :\tab\tab " & cSelFile.GetIsReparsePoint & "\par"
            s &= "\tab Fragmented :\tab\tab " & cSelFile.GetIsSparseFile & "\par"
            s &= "\tab System :\tab\tab " & cSelFile.GetIsSystem & "\par"
            s &= "\tab Temporary :\tab\tab " & cSelFile.GetIsTemporary & "\par\par"
            s &= "\b File version infos\b0\par"

            If cSelFile.GetFileVersion IsNot Nothing Then
                If cSelFile.GetFileVersion.Comments IsNot Nothing AndAlso cSelFile.GetFileVersion.Comments.Length > 0 Then _
                    s &= "\tab Comments :\tab\tab " & cSelFile.GetFileVersion.Comments & "\par"
                If cSelFile.GetFileVersion.CompanyName IsNot Nothing AndAlso cSelFile.GetFileVersion.CompanyName.Length > 0 Then _
                    s &= "\tab CompanyName :\tab\tab " & cSelFile.GetFileVersion.CompanyName & "\par"
                If CStr(cSelFile.GetFileVersion.FileBuildPart).Length > 0 Then _
                    s &= "\tab FileBuildPart :\tab\tab " & CStr(cSelFile.GetFileVersion.FileBuildPart) & "\par"
                If cSelFile.GetFileVersion.FileDescription IsNot Nothing AndAlso cSelFile.GetFileVersion.FileDescription.Length > 0 Then _
                    s &= "\tab FileDescription :\tab\tab " & cSelFile.GetFileVersion.FileDescription & "\par"
                If CStr(cSelFile.GetFileVersion.FileMajorPart).Length > 0 Then _
                    s &= "\tab FileMajorPart :\tab\tab " & CStr(cSelFile.GetFileVersion.FileMajorPart) & "\par"
                If CStr(cSelFile.GetFileVersion.FileMinorPart).Length > 0 Then _
                    s &= "\tab FileMinorPart :\tab\tab " & cSelFile.GetFileVersion.FileMinorPart & "\par"
                If CStr(cSelFile.GetFileVersion.FilePrivatePart).Length > 0 Then _
                    s &= "\tab FilePrivatePart :\tab\tab " & cSelFile.GetFileVersion.FilePrivatePart & "\par"
                If cSelFile.GetFileVersion.FileVersion IsNot Nothing AndAlso cSelFile.GetFileVersion.FileVersion.Length > 0 Then _
                    s &= "\tab FileVersion :\tab\tab " & cSelFile.GetFileVersion.FileVersion & "\par"
                If cSelFile.GetFileVersion.InternalName IsNot Nothing AndAlso cSelFile.GetFileVersion.InternalName.Length > 0 Then _
                    s &= "\tab InternalName :\tab\tab " & cSelFile.GetFileVersion.InternalName & "\par"
                If CStr(cSelFile.GetFileVersion.IsDebug).Length > 0 Then _
                    s &= "\tab IsDebug :\tab\tab " & cSelFile.GetFileVersion.IsDebug & "\par"
                If CStr(cSelFile.GetFileVersion.IsPatched).Length > 0 Then _
                    s &= "\tab IsPatched :\tab\tab " & cSelFile.GetFileVersion.IsPatched & "\par"
                If CStr(cSelFile.GetFileVersion.IsPreRelease).Length > 0 Then _
                    s &= "\tab IsPreRelease :\tab\tab " & cSelFile.GetFileVersion.IsPreRelease & "\par"
                If CStr(cSelFile.GetFileVersion.IsPrivateBuild).Length > 0 Then _
                    s &= "\tab IsPrivateBuild :\tab\tab " & cSelFile.GetFileVersion.IsPrivateBuild & "\par"
                If CStr(cSelFile.GetFileVersion.IsSpecialBuild).Length > 0 Then _
                    s &= "\tab IsSpecialBuild :\tab\tab " & cSelFile.GetFileVersion.IsSpecialBuild & "\par"
                If cSelFile.GetFileVersion.Language IsNot Nothing AndAlso cSelFile.GetFileVersion.Language.Length > 0 Then _
                    s &= "\tab Language :\tab\tab " & cSelFile.GetFileVersion.Language & "\par"
                If cSelFile.GetFileVersion.LegalCopyright IsNot Nothing AndAlso cSelFile.GetFileVersion.LegalCopyright.Length > 0 Then _
                    s &= "\tab LegalCopyright :\tab\tab " & cSelFile.GetFileVersion.LegalCopyright & "\par"
                If cSelFile.GetFileVersion.LegalTrademarks IsNot Nothing AndAlso cSelFile.GetFileVersion.LegalTrademarks.Length > 0 Then _
                    s &= "\tab LegalTrademarks :\tab " & cSelFile.GetFileVersion.LegalTrademarks & "\par"
                If cSelFile.GetFileVersion.OriginalFilename IsNot Nothing AndAlso cSelFile.GetFileVersion.OriginalFilename.Length > 0 Then _
                    s &= "\tab OriginalFilename :\tab\tab " & cSelFile.GetFileVersion.OriginalFilename & "\par"
                If cSelFile.GetFileVersion.PrivateBuild IsNot Nothing AndAlso cSelFile.GetFileVersion.PrivateBuild.Length > 0 Then _
                    s &= "\tab PrivateBuild :\tab\tab " & cSelFile.GetFileVersion.PrivateBuild & "\par"
                If CStr(cSelFile.GetFileVersion.ProductBuildPart).Length > 0 Then _
                    s &= "\tab ProductBuildPart :\tab " & cSelFile.GetFileVersion.ProductBuildPart & "\par"
                If CStr(cSelFile.GetFileVersion.ProductMajorPart).Length > 0 Then _
                    s &= "\tab ProductMajorPart :\tab " & cSelFile.GetFileVersion.ProductMajorPart & "\par"
                If CStr(cSelFile.GetFileVersion.ProductMinorPart).Length > 0 Then _
                    s &= "\tab Comments :\tab\tab " & cSelFile.GetFileVersion.ProductMinorPart & "\par"
                If cSelFile.GetFileVersion.ProductName IsNot Nothing AndAlso cSelFile.GetFileVersion.ProductName.Length > 0 Then _
                    s &= "\tab ProductName :\tab\tab " & cSelFile.GetFileVersion.ProductName & "\par"
                If CStr(cSelFile.GetFileVersion.ProductPrivatePart).Length > 0 Then _
                    s &= "\tab ProductPrivatePart :\tab " & cSelFile.GetFileVersion.ProductPrivatePart & "\par"
                If cSelFile.GetFileVersion.ProductVersion IsNot Nothing AndAlso cSelFile.GetFileVersion.ProductVersion.Length > 0 Then _
                    s &= "\tab ProductVersion :\tab\tab " & cSelFile.GetFileVersion.ProductVersion & "\par"
                If cSelFile.GetFileVersion.SpecialBuild IsNot Nothing AndAlso cSelFile.GetFileVersion.SpecialBuild.Length > 0 Then _
                    s &= "\tab SpecialBuild :\tab\tab " & cSelFile.GetFileVersion.SpecialBuild & "\par"
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
        cService.Enumerate(serv)

        ' Refresh (or suppress) all services displayed in listview
        For Each lvi In Me.lvServices.Items

            ' Test if process exist
            For Each p In serv
                If p.GetName = lvi.Text Then
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

                it.Text = p.GetName
                it.ImageKey = "service"

                Dim lsub1 As New ListViewItem.ListViewSubItem
                Dim lsub2 As New ListViewItem.ListViewSubItem
                Dim lsub3 As New ListViewItem.ListViewSubItem
                Dim lsub4 As New ListViewItem.ListViewSubItem
                Dim lsub5 As New ListViewItem.ListViewSubItem

                Dim path As String
                Try
                    path = p.GetImagePath
                    path = Replace(path, Chr(34), vbNullString)
                Catch ex As Exception
                    path = ex.Message
                End Try


                lsub4.Text = path
                lsub2.Text = p.GetStatus.ToString
                Try
                    lsub3.Text = p.GetServiceStartType
                Catch ex As Exception
                    lsub3.Text = ex.Message
                End Try
                lsub1.Text = p.GetLongName
                lsub5.Text = CStr(IIf(p.GetCanPauseAndContinue, "Pause/Continue ", "")) & _
                            CStr(IIf(p.GetCanShutdown, "Shutdown ", "")) & _
                            CStr(IIf(p.GetCanStop, "Stop ", ""))

                it.SubItems.Add(lsub1)
                it.SubItems.Add(lsub2)
                it.SubItems.Add(lsub3)
                it.SubItems.Add(lsub4)
                it.SubItems.Add(lsub5)

                it.Tag = p

                lvServices.Items.Add(it)
            End If

        Next

        ' Here we retrieve some informations for all our displayed services
        For Each lvi In Me.lvServices.Items
            Try
                Dim cS As cService = CType(lvi.Tag, cService)
                cS.Refresh()
              
                lvi.SubItems(2).Text = cS.GetStatus.ToString
                lvi.SubItems(5).Text = CStr(IIf(cS.GetCanPauseAndContinue, "Pause/Continue ", "")) & _
                    CStr(IIf(cS.GetCanShutdown, "Shutdown ", "")) & _
                    CStr(IIf(cS.GetCanStop, "Stop ", ""))
                Try
                    lvi.SubItems(3).Text = cS.GetServiceStartType
                Catch ex As Exception
                    lvi.SubItems(3).Text = ex.Message
                End Try

            Catch ex As Exception
                '
            End Try
        Next

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

        ReDim proc(0)
        cProcess.Enumerate(proc)
        
        ' Refresh (or suppress) all processes displayed in listview
        For Each lvi In Me.lvProcess.Items

            ' Test if process exist
            For Each p In proc
                Dim cP As cProcess = CType(lvi.Tag, cProcess)
                If p.GetPid = cP.GetPid And p.GetName = cP.GetName Then
                    exist = True
                    p.isDisplayed = True
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

        ' Add all non displayed processe (new processes)
        For Each p In proc

            If p.IsDisplayed = False Then

                p.IsDisplayed = True

                ' Get the process name
                Dim o As String = p.GetName
                Dim it As New ListViewItem

                If Len(o) > 0 Then

                    it.Text = o

                    Dim lsub1 As New ListViewItem.ListViewSubItem
                    lsub1.Text = CStr(p.GetPid)

                    Dim lsub2 As New ListViewItem.ListViewSubItem
                    Dim lsub3 As New ListViewItem.ListViewSubItem
                    Dim lsub4 As New ListViewItem.ListViewSubItem
                    Dim lsub5 As New ListViewItem.ListViewSubItem
                    Dim lsub6 As New ListViewItem.ListViewSubItem
                    Dim lsub7 As New ListViewItem.ListViewSubItem
                    Dim lsub8 As New ListViewItem.ListViewSubItem

                    If p.GetPid > 4 Then

                        lsub2.Text = p.GetUserName

                        ' Add icon
                        Try

                            Dim fName As String = p.GetPath

                            If IO.File.Exists(fName) Then
                                Dim img As System.Drawing.Icon = GetIcon(fName, True)
                                imgProcess.Images.Add(fName, img)
                                it.ImageKey = fName
                                lsub7.Text = fName
                            Else
                                it.ImageKey = "noicon"
                                lsub7.Text = "N/A"
                                it.ForeColor = Drawing.Color.Gray
                                lsub8.Text = "N/A"
                            End If

                        Catch ex As Exception
                            it.ImageKey = "noicon"
                            lsub7.Text = "N/A"
                            it.ForeColor = Drawing.Color.Gray
                            lsub8.Text = "N/A"
                        End Try

                    Else
                        lsub2.Text = "SYSTEM"
                        lsub3.Text = "N/A"
                        lsub4.Text = "N/A"
                        lsub5.Text = "N/A"
                        lsub6.Text = "N/A"
                        lsub7.Text = "N/A"
                        lsub8.Text = "N/A"
                        it.ImageKey = "noIcon"
                    End If

                    it.SubItems.Add(lsub1)
                    it.SubItems.Add(lsub2)
                    it.SubItems.Add(lsub3)
                    it.SubItems.Add(lsub4)
                    it.SubItems.Add(lsub5)
                    it.SubItems.Add(lsub6)
                    it.SubItems.Add(lsub7)
                    it.SubItems.Add(lsub8)

                    it.Group = lvProcess.Groups(0)

                    it.Tag = New cProcess(p)

                    'it.BackColor = Color.LightGreen

                    lvProcess.Items.Add(it)
                End If
            End If

        Next

        test = GetTickCount
        ' Here we retrieve some informations for all our displayed processes
        For Each lvi In Me.lvProcess.Items

            Try
                Dim cP As cProcess = CType(lvi.Tag, cProcess)

                Dim id As Integer = cP.GetPid

                ' Processor time
                ' Memory
                ' Peak memory
                ' Priority
                ' Path

                Dim ts As TimeSpan = cP.GetProcessorTime
                Dim fName As String = cP.GetPath
                Dim s As String = String.Format("{0:00}", ts.TotalHours) & ":" & _
                    String.Format("{0:00}", ts.Minutes) & ":" & _
                    String.Format("{0:00}", ts.Seconds)

                With lvi
                    .SubItems(3).Text = s
                    Dim mc As cProcess.PROCESS_MEMORY_COUNTERS = cP.GetMemoryInfos
                    .SubItems(4).Text = CStr(mc.WorkingSetSize / 1024) & " Kb"
                    .SubItems(5).Text = CStr(mc.PeakWorkingSetSize / 1024) & " Kb"
                    .SubItems(6).Text = cP.GetPriorityClass
                    .SubItems(8).Text = cP.GetStartTime.ToString ' & " -- " & cP.GetStartTime.ToLongTimeString
                End With

            Catch ex As Exception
                ' Access denied or ?
            End Try

        Next

        test = GetTickCount - test
        ' Me.Text = CStr(test)

    End Sub

    Private Sub timerProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcess.Tick
        refreshProcessList()
        Me.Text = "Yet Another Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
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

    Private Sub lvProcess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvProcess.SelectedIndexChanged
        ' New process selected
        If lvProcess.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvProcess.SelectedItems.Item(0)

            Me.lblProcessName.Text = "Process name : " & it.Text
            Me.lblProcessPath.Text = "Unable to retrieve path"

            If TypeOf it.Tag Is cProcess Then

                Try
                    Dim cP As cProcess = CType(it.Tag, cProcess)
                    Dim pid As Integer = cP.GetPid

                    Dim mainModule As System.Diagnostics.ProcessModule = cP.GetMainModule

                    Dim pmc As cProcess.PROCESS_MEMORY_COUNTERS = cP.GetMemoryInfos

                    ' Description
                    Dim s As String = ""
                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b File properties\b0\par"
                    s = s & "\tab File name :\tab\tab\tab " & it.Text & "\par"
                    s = s & "\tab Path :\tab\tab\tab\tab " & Replace(it.SubItems(7).Text, "\", "\\") & "\par"
                    s = s & "\tab Description :\tab\tab\tab " & mainModule.FileVersionInfo.FileDescription & "\par"
                    s = s & "\tab Company name :\tab\tab\tab " & mainModule.FileVersionInfo.CompanyName & "\par"
                    s = s & "\tab Version :\tab\tab\tab " & mainModule.FileVersionInfo.FileVersion & "\par"
                    s = s & "\tab Copyright :\tab\tab\tab " & mainModule.FileVersionInfo.LegalCopyright & "\par"
                    s = s & "\par"
                    s = s & "  \b Process description\b0\par"
                    s = s & "\tab PID :\tab\tab\tab\tab " & it.SubItems(1).Text & "\par"
                    s = s & "\tab Start time :\tab\tab\tab " & it.SubItems(8).Text & "\par"
                    s = s & "\tab Priority :\tab\tab\tab\tab " & it.SubItems(6).Text & "\par"
                    s = s & "\tab User :\tab\tab\tab\tab " & it.SubItems(2).Text & "\par"
                    s = s & "\tab Processor time :\tab\tab\tab " & it.SubItems(3).Text & "\par"
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

                        Dim ipi As InternetProcessInfo = mdlInternet.GetInternetInfos(it.Text)

                        s = s & "\tab Security risk (0-5) :\tab\tab " & CStr(ipi._Risk) & "\par"
                        s = s & "\tab Description :\tab\tab\tab " & Replace$(ipi._Description, vbNewLine, "\par") & "\par"
                    End If


                    If chkModules.Checked Then
                        ' Retrieve modules
                        s = s & "\par"
                        s = s & "  \b Loaded modules\b0\par"
                        Dim m As ProcessModule
                        Dim mdl As ProcessModuleCollection = cP.GetModules
                        s = s & "\tab " & CStr(mdl.Count) & " modules loaded" & "\par"
                        For Each m In mdl
                            s = s & "\tab " & Replace(m.FileVersionInfo.FileName, "\", "\\") & "\par"
                        Next

                        ' Retrieve threads infos
                        s = s & "\par"
                        s = s & "  \b Threads\b0\par"
                        Dim pt As ProcessThread
                        Dim thr As System.Diagnostics.ProcessThreadCollection = cP.GetThreads
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

                    ' Icons
                    Try
                        pctBigIcon.Image = GetIcon(it.SubItems(7).Text, False).ToBitmap
                        pctSmallIcon.Image = GetIcon(it.SubItems(7).Text, True).ToBitmap
                    Catch ex As Exception
                        pctSmallIcon.Image = Me.imgProcess.Images("noicon")
                        pctBigIcon.Image = Me.imgMain.Images("noicon32")
                    End Try

                    Me.lblProcessName.Text = "Process name : " & it.Text
                    Me.lblProcessPath.Text = "Process path : " & it.SubItems(7).Text

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

            Else
                ' Error
                'Dim s As String = ""
                'Dim er As Exception = CType(it.Tag, Exception)

                's = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                's = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                's = s & "\tab Message :\tab " & er.Message & "\par"
                's = s & "\tab Source :\tab\tab " & er.Source & "\par"
                'If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                's = s & "}"

                'rtb.Rtf = s

                'pctSmallIcon.Image = Me.imgProcess.Images("noicon")
                'pctBigIcon.Image = Me.imgMain.Images("noicon32")
            End If

        End If

    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            handles_Renamed.Close()
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        refreshProcessList()
        refreshServiceList()

        Application.EnableVisualStyles()
        'SetWindowTheme(Me.lvProcess.Handle, "explorer", "")

        With Me
            .lblServicePath.BackColor = .BackColor
            .lblProcessPath.BackColor = .BackColor
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
        SetToolTip(Me.lblProcessPath, "Path of the main executable.")
        SetToolTip(Me.lblServicePath, "Path of the main executable of the service.")
        SetToolTip(Me.tv, "Selected service depends on these services.")
        SetToolTip(Me.tv2, "This services depend on selected service.")
        SetToolTip(Me.cmdTray, "Hide YAPM (double click on icon on tray to show main form).")
        SetToolTip(Me.chkSearchProcess, "Search in processes list.")
        SetToolTip(Me.chkSearchServices, "Search in services list.")
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
                .startChkModules = True
                .startHidden = False
                .startJobs = True
                .startup = False
                .topmost = False
                MsgBox(MSGFIRSTTIME, MsgBoxStyle.Information, "Please read this")
                .Save()
                .Apply()
            End With
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
        Me.panelInfos.Left = 5
        Me.panelInfos.Top = 307
        Me.panelInfos2.Left = 5
        Me.panelInfos2.Top = 307
        Me.panelMain6.Left = 5
        Me.panelMain6.Top = 120
        Me.panelMain7.Left = 5
        Me.panelMain7.Top = 120
        Me.panelMain8.Left = 5
        Me.panelMain8.Top = 120

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

        ' Process
        Dim i As Integer = CInt((Me.Height - 250) / 2)
        Me.panelInfos.Height = CInt(IIf(i < 340, i, 340))
        Me.panelMain.Height = Me.Height - Me.panelInfos.Height - 187
        Me.panelInfos.Top = Me.panelMain.Top + Me.panelMain.Height + 3
        Me.panelMain.Width = Me.Width - Me.panelMain.Left - 21
        Me.panelInfos.Width = Me.panelMain.Width

        Me.rtb.Width = Me.panelInfos.Width - 5
        Me.rtb.Height = Me.panelInfos.Height - 45
        Me.pctBigIcon.Left = Me.panelInfos.Width - 35
        Me.pctSmallIcon.Left = Me.panelInfos.Width - 57
        Me.cmdInfosToClipB.Left = Me.panelInfos.Width - 165
        Me.lblProcessPath.Width = Me.panelInfos.Width - 175
        Me.lblProcessName.Width = Me.panelInfos.Width - 175

        ' File resizement
        Me.panelMain5.Height = Me.panelMain3.Height
        Me.panelMain5.Width = Me.panelMain3.Width
        Me.pctFileBig.Left = Me.panelInfos.Width - 35
        Me.pctFileSmall.Left = Me.panelInfos.Width - 57
        Me.cmdFileClipboard.Left = Me.panelInfos.Width - 165
        Me.txtFile.Width = Me.panelInfos.Width - 175
        Me.fileSplitContainer.Width = Me.rtb.Width
        Me.fileSplitContainer.Height = Me.panelMain5.Height - 42
        Me.lstFileString.Width = Me.fileSplitContainer.Width - Me.gpFileAttributes.Width - Me.gpFileDates.Width - 10

        ' Services
        Me.panelInfos2.Height = CInt(IIf(i < 210, i, 210))
        Me.panelMain2.Height = Me.Height - Me.panelInfos2.Height - 187
        Me.panelInfos2.Top = Me.panelMain2.Top + Me.panelMain2.Height + 3
        Me.panelMain2.Width = Me.panelMain.Width
        Me.panelInfos2.Width = Me.panelInfos.Width

        Me.lblServiceName.Width = Me.panelInfos2.Width - 140
        Me.lblServicePath.Width = Me.lblServiceName.Width
        Me.cmdCopyServiceToCp.Left = Me.panelInfos2.Width - 107
        Me.tv2.Height = CInt((Me.panelInfos2.Height - 48) / 2)
        Me.tv.Height = Me.tv2.Height
        Me.tv.Top = Me.tv2.Top + 3 + Me.tv2.Height
        Me.tv2.Left = Me.panelInfos2.Width - 151
        Me.tv.Left = Me.tv2.Left
        Me.rtb2.Height = Me.panelInfos2.Height - 45
        Me.rtb2.Width = Me.panelInfos2.Width - 157

        ' Economize CPU :-)
        If Me.WindowState = FormWindowState.Minimized Then
            Me.timerProcess.Enabled = False
            Me.timerServices.Enabled = False
        Else
            Me.timerProcess.Enabled = True
            Me.timerServices.Enabled = True
        End If

    End Sub

    Private Sub timerServices_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerServices.Tick
        refreshServiceList()
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

    Private Sub lvServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseUp
        If lvServices.SelectedItems.Count = 1 Then
            Dim s As String = lvServices.SelectedItems.Item(0).SubItems(2).Text
            Dim s2 As String = lvServices.SelectedItems.Item(0).SubItems(3).Text
            Dim s3 As String = lvServices.SelectedItems.Item(0).SubItems(5).Text
            ToolStripMenuItem9.Text = CStr(IIf(s = "Running", "Pause", "Resume"))
            ToolStripMenuItem9.Enabled = ((InStr(s3, "Pause") + InStr(s3, "Resume")) > 0)
            ToolStripMenuItem11.Enabled = (s3.Length = 0)
            ToolStripMenuItem10.Enabled = (InStr(s3, "Stop") > 0)
            ShutdownToolStripMenuItem.Enabled = (InStr(s3, "Shutdown") > 0)
            ToolStripMenuItem13.Checked = (s2 = "Disabled")
            ToolStripMenuItem13.Enabled = Not (ToolStripMenuItem13.Checked)
            ToolStripMenuItem14.Checked = (s2 = "Auto Start")
            ToolStripMenuItem14.Enabled = Not (ToolStripMenuItem14.Checked)
            ToolStripMenuItem15.Checked = (s2 = "Demand Start")
            ToolStripMenuItem15.Enabled = Not (ToolStripMenuItem15.Checked)
        ElseIf lvServices.SelectedItems.Count > 1 Then
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
        End If
    End Sub

    Private Sub lvServices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvServices.SelectedIndexChanged
        ' New service selected
        If lvServices.SelectedItems.Count = 1 Then
            Dim it As ListViewItem = lvServices.SelectedItems.Item(0)
            Try
                Dim cS As cService = CType(it.Tag, cService)

                Me.lblServiceName.Text = "Service name : " & it.Text
                Me.lblServicePath.Text = "Service path : " & it.SubItems(4).Text

                ' Description
                Dim s As String = vbNullString
                Dim description As String = vbNullString
                Dim diagnosticsMessageFile As String = cS.GetDiagnosticsMessageFile
                Dim group As String = cS.GetGroup
                Dim objectName As String = cS.GetObjectName
                Dim tag As String = vbNullString

                ' OK it's not the best way to retrive the description...
                s = cS.GetImagePath
                Dim sTemp As String = cS.GetDescription
                If InStr(sTemp, "@", CompareMethod.Binary) > 0 Then
                    description = cFile.IntelligentPathRetrieving(sTemp)
                Else
                    description = sTemp
                End If

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Service properties\b0\par"
                s = s & "\tab Name :\tab\tab\tab " & it.Text & "\par"
                s = s & "\tab Common name :\tab\tab " & it.SubItems(1).Text & "\par"
                If Len(it.SubItems(4).Text) > 0 Then s = s & "\tab Path :\tab\tab\tab " & Replace(it.SubItems(4).Text, "\", "\\") & "\par"
                If Len(description) > 0 Then s = s & "\tab Description :\tab\tab " & description & "\par"
                If Len(group) > 0 Then s = s & "\tab Group :\tab\tab\tab " & group & "\par"
                If Len(objectName) > 0 Then s = s & "\tab ObjectName :\tab\tab " & objectName & "\par"
                If Len(diagnosticsMessageFile) > 0 Then s = s & "\tab DiagnosticsMessageFile :\tab\tab " & diagnosticsMessageFile & "\par"
                If Len(it.SubItems(2).Text) > 0 Then s = s & "\tab State :\tab\tab\tab " & it.SubItems(2).Text & "\par"
                If Len(it.SubItems(3).Text) > 0 Then s = s & "\tab Startup :\tab\tab " & it.SubItems(3).Text & "\par"
                If Len(it.SubItems(5).Text) > 0 Then s = s & "\tab Availables actions :\tab " & it.SubItems(5).Text & "\par"

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

    Private Sub panelInfos2_Resize(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Resize controls rtb2,tv and tv2
        Try
            Dim h As Integer = CInt(panelInfos2.Height - 47)
            tv.Height = h
            tv2.Height = h
            tv2.Top = 41 + tv.Height + 6
            rtb2.Width = panelInfos2.Width - 162
            rtb2.Height = panelInfos2.Height - 44
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Tray_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Tray.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.Visible = True
    End Sub

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
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

    Private Sub pctBigIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctBigIcon.MouseDown
        Me.ToolStripMenuItem6.Enabled = (Me.pctBigIcon.Image IsNot Nothing)
    End Sub

    Private Sub pctSmallIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctSmallIcon.MouseDown
        Me.ToolStripMenuItem7.Enabled = (Me.pctSmallIcon.Image IsNot Nothing)
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

    Private Sub rtb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtb.TextChanged
        Me.cmdInfosToClipB.Enabled = (rtb.Rtf.Length > 0)
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
            If IO.File.Exists(it.SubItems(7).Text) Then
                cFile.ShowFileProperty(it.SubItems(7).Text)
            End If
        Next
    End Sub

    Private Sub OpenFirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenFirectoryToolStripMenuItem.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            If it.SubItems(7).Text <> "N/A" Then _
            cFile.OpenDirectory(it.SubItems(7).Text)
        Next
    End Sub

    Private Sub lblResCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblResCount.Click
        Me.lvProcess.Focus()
        Try
            System.Windows.Forms.SendKeys.Send(Me.lvProcess.Groups(1).Items(0).Text)
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
            If it.SubItems(4).Text <> "N/A" Then
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
            If it.SubItems(4).Text <> "N/A" Then
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
                CType(it.Tag, cService).SetServiceStartType(cService.ServiceStartType.SERVICE_DISABLED)
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
                CType(it.Tag, cService).SetServiceStartType(cService.ServiceStartType.SERVICE_AUTO_START)
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
                CType(it.Tag, cService).SetServiceStartType(cService.ServiceStartType.SERVICE_DEMAND_START)
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

    Private Sub cmdCopyServiceToCp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyServiceToCp.Click
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

    Private Sub rtb2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rtb2.TextChanged
        Me.cmdCopyServiceToCp.Enabled = (rtb2.Rtf.Length > 0)
    End Sub

    Private Sub timerJobs_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerJobs.Tick
        ' Job processing
        Dim it As ListViewItem
        Dim p As ListViewItem
        Dim tAction As String = vbNullString
        Dim tTime As String = vbNullString
        Dim tPid As Integer = 0
        Dim tProcess As String = vbNullString
        Dim tName As String = vbNullString

        For Each it In Me.lvJobs.Items
            ' Name
            ' ProcessID
            ' ProcessName
            ' Action
            ' Time
            With it
                tPid = CInt(.SubItems(1).Text)
                tProcess = .SubItems(2).Text
                tAction = .SubItems(3).Text
                tTime = .SubItems(4).Text
            End With

            ' Firstly, we check if time implies to process job now
            If tTime = "Every second" Or tTime = DateTime.Now.ToLongDateString & "-" & DateTime.Now.ToLongTimeString Then

                If tPid > 0 Then
                    ' Check PID
                    If Len(tProcess) > 0 Then
                        ' Check process name too
                        For Each p In lvProcess.Items
                            If p.Text = tName And CInt(p.SubItems(1).Text) = tPid Then
                                ProcessJob(tPid, tAction)
                            End If
                        Next
                    Else
                        ' Check only pid
                        For Each p In lvProcess.Items
                            If CInt(p.SubItems(1).Text) = tPid Then
                                ProcessJob(tPid, tAction)
                            End If
                        Next
                    End If
                Else
                    ' Check only process name
                    For Each p In lvProcess.Items
                        If p.Text = tProcess And CInt(p.SubItems(1).Text) = tPid Then
                            ProcessJob(CInt(p.SubItems(1).Text), tAction)
                        End If
                    Next
                End If
            End If
        Next
    End Sub

    ' Process a job
    Private Sub ProcessJob(ByVal pid As Integer, ByVal action As String)
        Select Case action
            Case "Kill"
                'mdlProcess.Kill(pid)
            Case "Pause"
                'mdlProcess.SuspendProcess(pid)
            Case "Resume"
                'mdlProcess.ResumeProcess(pid)
        End Select
    End Sub

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
            If it.SubItems(7).Text <> "N/A" Then _
            Process.GetProcessById(CInt(it.SubItems(1).Text)).Kill()
        Next
    End Sub

    Private Sub butSaveProcessReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveProcessReport.Click
        saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        saveDial.Title = "Save report"
        saveDial.ShowDialog()
        Dim s As String = saveDial.FileName
        If Len(s) > 0 Then
            MsgBox(s)
        End If
    End Sub

    Private Sub butSaveServiceReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveServiceReport.Click
        saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        saveDial.Title = "Save report"
        saveDial.ShowDialog()
        Dim s As String = saveDial.FileName
        If Len(s) > 0 Then
            MsgBox(s)
        End If
    End Sub

    Private Sub butAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub butOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOptions.Click
        frmPreferences.ShowDialog()
    End Sub

    Private Sub butProcessRerfresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessRerfresh.Click
        Me.refreshProcessList()
    End Sub

    Private Sub butServiceRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceRefresh.Click
        Me.refreshServiceList()
    End Sub

    Private Sub butTakeFullPower_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTakeFullPower.Click
        Me.Visible = False
        'MsgBox(mdlPrivileges.SetDebuPrivilege())
        clsOpenedHandles.EnableDebug()
        Me.lvProcess.Items.Clear()
        refreshProcessList()
        Me.Visible = True
    End Sub

    Private Sub butOpenJobList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOpenJobList.Click
        ' Here we open a Job file
        openDial.Filter = "Job File (*.job)|*.job"
        openDial.Title = "Open job file"
        openDial.ShowDialog()
        Dim s As String = openDial.FileName
        If Len(s) > 0 Then
            MsgBox(s)
        End If
    End Sub

    Private Sub butSaveJobList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveJobList.Click
        ' Save job list
        Me.saveDial.Filter = "Job File (*.job)|*.job"
        openDial.Title = "Save job file"
        saveDial.ShowDialog()
        Dim s As String = saveDial.FileName
        If Len(s) > 0 Then
            MsgBox(s)
        End If
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

    Private Sub butProcessFileProp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessFileProp.Click
        ' File properties for selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            If IO.File.Exists(it.SubItems(7).Text) Then
                If it.SubItems(7).Text <> "N/A" Then _
                cFile.ShowFileProperty(it.SubItems(7).Text)
            End If
        Next
    End Sub

    Private Sub butProcessDirOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessDirOpen.Click
        ' Open directory of selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            If it.SubItems(7).Text <> "N/A" Then _
            cFile.OpenDirectory(it.SubItems(7).Text)
        Next
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
            'If it.SubItems(7).Text <> "N/A" Then _
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
            CType(it.Tag, cProcess).SetProcessPriority( ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub butNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNormal.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority( ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub butRealTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRealTime.Click
        ' Set priority to selected processes
        Dim it As ListViewItem
        For Each it In Me.lvProcess.SelectedItems
            CType(it.Tag, cProcess).SetProcessPriority( ProcessPriorityClass.RealTime)
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
                CType(it.Tag, cService).SetServiceStartType(cService.ServiceStartType.SERVICE_AUTO_START)
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
                CType(it.Tag, cService).SetServiceStartType(cService.ServiceStartType.SERVICE_DISABLED)
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
                CType(it.Tag, cService).SetServiceStartType(cService.ServiceStartType.SERVICE_DEMAND_START)
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
                    Me.bProcessHover = False
                    Me.bServiceHover = True
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = True
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelInfos.Visible = False
                    Me.panelInfos2.Visible = True
                    Me.chkModules.Visible = False
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = True
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                Case "Processes"
                    Me.bProcessHover = True
                    Me.bServiceHover = False
                    Me.panelMain.Visible = True
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelInfos.Visible = True
                    Me.panelInfos2.Visible = False
                    Me.chkModules.Visible = True
                    Me.panelMenu.Visible = True
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                Case "Jobs"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = True
                    Me.panelMain4.Visible = False
                    Me.panelMain3.BringToFront()
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                Case "Help"

                    If Not (bhelpshown) Then
                        bhelpshown = True
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
                    Me.panelMain4.BringToFront()
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                Case "File"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMain5.BringToFront()
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = True
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                Case "Search"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMain6.BringToFront()
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = True
                    Me.panelMain7.Visible = False
                    Me.panelMain8.Visible = False
                Case "Handles"
                    Me.bProcessHover = False
                    Me.bServiceHover = False
                    Me.panelMain.Visible = False
                    Me.panelMain2.Visible = False
                    Me.panelMain3.Visible = False
                    Me.panelMain4.Visible = False
                    Me.panelMain7.BringToFront()
                    Me.panelMenu.Visible = False
                    Me.panelMenu2.Visible = False
                    Me.panelMain5.Visible = False
                    Me.panelMain6.Visible = False
                    Me.panelMain7.Visible = True
                    Me.panelMain8.Visible = False
                Case "Monitor"
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
                    Me.panelMain8.Visible = True
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

    Private Sub butTopMost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTopMost.Click
        Me.butTopMost.Checked = Not (Me.butTopMost.Checked)
        Me.bAlwaysDisplay = Me.butTopMost.Checked
        Me.TopMost = Me.bAlwaysDisplay
    End Sub

    Private Sub frmMain_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        Me.timerServices.Enabled = Me.Visible
        Me.timerProcess.Enabled = Me.Visible
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
            My.Application.DoEvents()

            If _stopOnlineRetrieving Then
                b = False
                Exit Sub
            End If

            Try
                Select Case mdlInternet.GetSecurityRisk(it.Text)
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
            My.Application.DoEvents()
            Try
                cFile.ShellOpenFile("http://www.google.com/search?hl=en&q=%22" & it.Text & "%22")
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
            System.Windows.Forms.SendKeys.Send(Me.lvServices.Groups(1).Items(0).Text)
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
        Call Me.butProcessFileDetails_Click(Nothing, Nothing)
    End Sub

    Private Sub butProcessFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessFileDetails.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvProcess.SelectedItems.Item(0).SubItems(7).Text
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
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
                        n2.Text = newIt.Text & " -- " & n3.Text & " -- " & it.Text & " -- " & subit.Text
                        n4.Text = it.SubItems(1).Text & " -- " & it.Text
                        newIt.SubItems.Add(n2)
                        newIt.SubItems.Add(n3)
                        newIt.SubItems.Add(n4)
                        newIt.Tag = "process"
                        Try
                            Dim fName As String = it.SubItems(7).Text
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
                        Dim proc As Process = Process.GetProcessById(CInt(Val(it.SubItems(1).Text)))
                        Dim p As ProcessModuleCollection = proc.Modules
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
                                newIt.Tag = "module"
                                n3.Text = "Module"
                                n2.Text = newIt.Text & " -- " & it.Text & " -- " & m.FileVersionInfo.FileName
                                n4.Text = it.SubItems(1).Text & " -- " & it.Text
                                newIt.SubItems.Add(n2)
                                newIt.SubItems.Add(n3)
                                newIt.SubItems.Add(n4)
                                newIt.ImageKey = "dll"
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
                            Me.lvSearchResults.Items.Add(newIt)
                        End If
                    End If
                End With
            Next
        End If


        Me.timerServices.Enabled = True
        Me.timerProcess.Enabled = True

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
        saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        saveDial.Title = "Save report"
        Try
            If saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = saveDial.FileName
                If Len(s) > 0 Then
                    Dim stream As New System.IO.StreamWriter(s, False)
                    ' Create file report
                    Dim c As String = vbNullString

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        ' txt
                        Dim it As ListViewItem
                        For Each it In Me.lvSearchResults.Items
                            c = "Type : " & it.Text
                            c &= "  Result : " & it.SubItems(1).Text
                            c &= "  Field : " & it.SubItems(2).Text & vbNewLine
                            stream.Write(c)
                        Next
                        c = CStr(Me.lvSearchResults.Items.Count) & " result(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        c = "<head><style type=" & Chr(34) & "text/css" & Chr(34) & ">BODY{font-family : Calibri, Times, courrier;}</style></head>"
                        c &= "<html><table width=100% border=0 cellspacing=1 cellpadding=0 bgcolor=" & Chr(34) & "#C5C5C5" & Chr(34) & ">"
                        c &= "<tr bgcolor=" & Chr(34) & "#C5C5C5" & Chr(34) & ">"
                        c &= "<td width=20% align=center><b>Type</b></td>"
                        c &= "<td width=60% align=center><b>Result</b></td>"
                        c &= "<td width=20% align=center><b>Field</b></td>"
                        c &= "</tr>"
                        stream.Write(c)

                        Dim it As ListViewItem
                        For Each it In Me.lvSearchResults.Items
                            c = "<tr bgcolor=" & Chr(34) & "white" & Chr(34) & ">"
                            c &= "<td>" & it.Text & "</td>"
                            c &= "<td>" & it.SubItems(1).Text & "</td>"
                            c &= "<td>" & it.SubItems(2).Text & "</td>"
                            c &= "</tr>"
                            stream.Write(c)
                        Next
                        stream.Write("</table></html>")
                        stream.Close()
                    End If

                    MsgBox("Save is ok !", MsgBoxStyle.Information, "Save report")
                End If
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub butShowProcHandles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowProcHandles.Click
        Dim it As ListViewItem
        Dim x As Integer = 0
        ReDim handlesToRefresh(Me.lvProcess.SelectedItems.Count - 1)
        For Each it In Me.lvProcess.SelectedItems
            handlesToRefresh(x) = CInt(Val(it.SubItems(1).Text))
            x += 1
        Next
        Call showHandles()
    End Sub

    Private Sub showHandles()
        ' Display handles of desired processes (handlesToRefresh)
        Dim id As Integer
        Dim i As Integer
        Dim it As ListViewItem

        If Me.handlesToRefresh Is Nothing Then Exit Sub

        handles_Renamed.Refresh()
        Me.lvHandles.Items.Clear()

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

        My.Application.DoEvents()
        Me.Ribbon.ActiveTab = Me.HandlesTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub butHandleRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHandleRefresh.Click
        Call showHandles()
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
                Dim sp As String = it.SubItems(3).Text
                Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                If i > 0 Then
                    Dim pid As String = sp.Substring(0, i - 1)
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvProcess.Items
                        If it2.SubItems(1).Text = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
                Me.Ribbon.ActiveTab = Me.ProcessTab
                Call Me.Ribbon_MouseMove(Nothing, Nothing)
            Catch ex As Exception
                '
            End Try
        Next
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
        ' Me.RBFileDelete.Enabled = b
        Me.RBFileKillProcess.Enabled = b
        Me.RBFileOnline.Enabled = b
        Me.RBFileOther.Enabled = b
        Me.RBFileOthers.Enabled = b
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
            Select Case CStr(it.Tag)
                Case "process"
                    Dim sp As String = it.SubItems(3).Text
                    Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                    If i > 0 Then
                        Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                        Call cProcess.Kill(pid)
                    End If
                Case "module"
                    Dim sp As String = it.SubItems(3).Text
                    Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                    If i > 0 Then
                        Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                        sp = it.SubItems(1).Text
                        i = InStrRev(sp, " ", , CompareMethod.Binary)
                        Dim sMod As String = sp.Substring(i, sp.Length - i)
                        Call cProcess.UnLoadModuleFromProcess(pid, sMod)
                    End If
                Case "service"
                    cService.StopService(it.SubItems(3).Text)
                Case Else
                    ' Handle
                    Dim sp As String = it.SubItems(3).Text
                    Dim i As Integer = InStr(sp, " ", CompareMethod.Binary)
                    Dim handle As Integer = CInt(it.Tag)
                    If i > 0 Then
                        Dim pid As Integer = CInt(Val(sp.Substring(0, i - 1)))
                        Call handles_Renamed.CloseProcessLocalHandle(pid, handle)
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
            cSelFile.decrypt()
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
            cSelFile.SetCreationTime(Me.DTcreation.Value)
            cSelFile.SetLastAccessTime(Me.DTlastAccess.Value)
            cSelFile.SetLastWriteTime(Me.DTlastModification.Value)
            MsgBox("Done.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Date change ok")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Date change failed")
        End Try
    End Sub

    Private Sub butFileSeeStrings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileSeeStrings.Click
        Call DisplayFileStrings(Me.lstFileString, Me.txtFile.Text)
    End Sub

    Private Sub lstFileString_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstFileString.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ' Copy items to clipboard
            Dim s As String = vbNullString
            Dim i As String
            For Each i In Me.lstFileString.SelectedItems
                s = s & i & vbNewLine
            Next
            My.Computer.Clipboard.SetText(s.Substring(0, s.Length - 2))
        ElseIf e.Button = Windows.Forms.MouseButtons.Middle Then
            Call DisplayFileStrings(Me.lstFileString, Me.txtFile.Text)
        End If
    End Sub

    Private Function RemoveAttribute(ByVal file As String, ByVal attributesToRemove As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.GetAttributes()
        Return attributes And Not (attributesToRemove)
    End Function
    Private Function AddAttribute(ByVal file As String, ByVal attributesToAdd As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.GetAttributes
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
                cSelFile.SetAttributes(AddAttribute(Me.txtFile.Text, IO.FileAttributes.Archive))
            Else
                cSelFile.SetAttributes(RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.Archive))
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
                cSelFile.SetAttributes(AddAttribute(Me.txtFile.Text, IO.FileAttributes.Hidden))
            Else
                cSelFile.SetAttributes(RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.Hidden))
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
                cSelFile.SetAttributes(AddAttribute(Me.txtFile.Text, IO.FileAttributes.ReadOnly))
            Else
                cSelFile.SetAttributes(RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.ReadOnly))
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
                cSelFile.SetAttributes(AddAttribute(Me.txtFile.Text, IO.FileAttributes.NotContentIndexed))
            Else
                cSelFile.SetAttributes(RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.NotContentIndexed))
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
                cSelFile.SetAttributes(AddAttribute(Me.txtFile.Text, IO.FileAttributes.Normal))
            Else
                cSelFile.SetAttributes(RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.Normal))
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
                cSelFile.SetAttributes(AddAttribute(Me.txtFile.Text, IO.FileAttributes.System))
            Else
                cSelFile.SetAttributes(RemoveAttribute(Me.txtFile.Text, IO.FileAttributes.System))
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
                        If it2.SubItems(1).Text = pid Then
                            it2.Selected = True
                        End If
                    Next
                End If
                Me.Ribbon.ActiveTab = Me.ProcessTab
                Call Me.Ribbon_MouseMove(Nothing, Nothing)
            Catch ex As Exception
                '
            End Try
        Next
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

        Dim n1 As New TreeNode
        n1.Text = it.GetName
        n1.ImageKey = "exe"
        n1.ImageIndex = 0
        n1.SelectedImageIndex = 0

        Me.tvMonitor.Nodes.Item(0).Nodes.Add(n1)
        n1.Tag = it

        With it
            If .getCheckCPU Then
                Dim ncpu As New TreeNode
                ncpu.Text = "CPU percentage"
                ncpu.ImageKey = "sub"
                ncpu.SelectedImageIndex = 2
                n1.Nodes.Add(ncpu)
            End If
            If .getCheckCPUTime Then
                Dim ncpu As New TreeNode
                ncpu.Text = "CPU time"
                ncpu.ImageKey = "sub"
                ncpu.SelectedImageIndex = 2
                n1.Nodes.Add(ncpu)
            End If
            If .GetCheckMemory Then
                Dim ncpu As New TreeNode
                ncpu.Text = "Memory"
                ncpu.ImageKey = "sub"
                ncpu.SelectedImageIndex = 2
                n1.Nodes.Add(ncpu)
            End If
            If .GetCheckPriority Then
                Dim ncpu As New TreeNode
                ncpu.Text = "Priority"
                ncpu.ImageKey = "sub"
                ncpu.SelectedImageIndex = 2
                n1.Nodes.Add(ncpu)
            End If
            If .GetCheckThreads Then
                Dim ncpu As New TreeNode
                ncpu.Text = "Thread count"
                ncpu.ImageKey = "sub"
                ncpu.SelectedImageIndex = 2
                n1.Nodes.Add(ncpu)
            End If
        End With

        Call updateMonitoringLog()
    End Sub

    Private Sub tvMonitor_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvMonitor.AfterSelect

        'tvMonitor.SelectedImageKey = tvMonitor.SelectedNode.ImageKey

        Me.graphMonitor.EnableGraph = False

        If tvMonitor.SelectedNode Is Nothing Then Exit Sub

        If tvMonitor.SelectedNode.Parent IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent.Name = tvMonitor.Nodes.Item(0).Name Then
                ' Then we have selected a process
                Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                Dim b As Boolean = it.GetEnabled
                Me.butMonitorStart.Enabled = Not (b)
                Me.butMonitorStop.Enabled = b
                Me.graphMonitor.CreateGraphics.Clear(Color.Black)
                Me.graphMonitor.CreateGraphics.DrawString("Select in the treeview a monitor item.", Me.Font, Brushes.White, 0, 0)
            Else
                Me.butMonitorStart.Enabled = False
                Me.butMonitorStop.Enabled = False

                ' We have selected a sub item -> display values in graph
                Dim it As cMonitor = CType(tvMonitor.SelectedNode.Parent.Tag, cMonitor)
                Dim sKey As String = tvMonitor.SelectedNode.Text
                If sKey = "Memory" Then
                    ' 3 differentes values
                    Me.splitMonitor4.Panel1.Enabled = True
                    Call ShowMonitorStats(it, "Memory." & Me.cbMon1.Text, "Memory." & Me.cbMon2.Text, "Memory." & Me.cbMon3.Text)
                Else
                    Me.splitMonitor4.Panel1.Enabled = False
                    Call ShowMonitorStats(it, sKey, "", "")
                End If
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
                Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                it.StartMonitoring()
                Call tvMonitor_AfterSelect(Nothing, Nothing)
            Else
                ' All items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    Dim it As cMonitor = CType(n.Tag, cMonitor)
                    it.StartMonitoring()
                Next
            End If
        End If
        Call updateMonitoringLog()
    End Sub

    Private Sub butMonitorStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorStop.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                it.StopMonitoring()
                Call tvMonitor_AfterSelect(Nothing, Nothing)
            Else
                ' All items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    Dim it As cMonitor = CType(n.Tag, cMonitor)
                    it.StopMonitoring()
                Next
            End If
        End If
    End Sub

    Private Sub butMonitoringRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitoringRemove.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent Is Nothing Then
                ' Delete all items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    n.Remove()
                Next
            Else
                If tvMonitor.SelectedNode.Parent.Name = tvMonitor.Nodes.Item(0).Name Then
                    ' Process, so we kill monitor
                    tvMonitor.SelectedNode.Remove()
                Else
                    ' Sub item of monitor, so we disable it from monitor
                    Dim it As cMonitor = CType(tvMonitor.SelectedNode.Parent.Tag, cMonitor)
                    it.UnMonitorItem(tvMonitor.SelectedNode.Text)
                    tvMonitor.SelectedNode.Remove()
                End If
            End If
        End If
        Call updateMonitoringLog()
    End Sub

    ' Update monitoring log
    Private Sub updateMonitoringLog()
        Dim s As String

        If Me.tvMonitor.Nodes.Item(0).Nodes.Count > 0 Then

            s = "Monitoring log" & vbNewLine
            s &= vbNewLine & vbNewLine & "Monitoring " & CStr(Me.tvMonitor.Nodes.Item(0).Nodes.Count) & " processe(s)" & vbNewLine

            Dim n As TreeNode
            For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
                Dim it As cMonitor = CType(n.Tag, cMonitor)
                s &= vbNewLine & "* Process  : " & it.GetName & " -- " & CStr(it.GetProcess.GetPid)
                s &= vbNewLine & "      Monitoring creation : " & it.GetMonitorCreationDate.ToLongDateString & " -- " & it.GetMonitorCreationDate.ToLongTimeString
                If it.GetLastStarted.Ticks > 0 Then
                    s &= vbNewLine & "      Last start : " & it.GetLastStarted.ToLongDateString & " -- " & it.GetLastStarted.ToLongTimeString
                Else
                    s &= vbNewLine & "      Not yet started"
                End If
                s &= vbNewLine & "      State : " & it.GetEnabled
                s &= vbNewLine & "      Interval : " & it.GetInterval
                s &= vbNewLine & "      Items to check : " & CStr(IIf(it.getCheckCPU, "CPU percentage + ", _
                        vbNullString)) & CStr(IIf(it.getCheckCPUTime, "CPU time + ", vbNullString)) & _
                        CStr(IIf(it.GetCheckMemory, "Memory + ", vbNullString)) & _
                        CStr(IIf(it.GetCheckPriority, "Priority + ", vbNullString)) & _
                        CStr(IIf(it.GetCheckThreads, "Thread count + ", vbNullString))
                s = s.Substring(0, s.Length - 2)

                s &= vbNewLine
            Next

            Me.txtMonitoringLog.Text = s
            Me.txtMonitoringLog.SelectionLength = 0
            Me.txtMonitoringLog.SelectionStart = 0

        End If

    End Sub

    ' Display stats in graph
    Private Sub ShowMonitorStats(ByVal it As cMonitor, ByVal key1 As String, ByVal key2 As String, _
        ByVal key3 As String)

        Me.timerMonitoring.Interval = it.GetInterval

        If it.GetEnabled = False Then
            Me.graphMonitor.CreateGraphics.Clear(Color.Black)
            Me.graphMonitor.CreateGraphics.DrawString("You sould start the monitoring.", Me.Font, Brushes.White, 0, 0)
            Exit Sub
        End If

        ' Get values from monitor item
        Dim v() As Graph.ValueItem
        Dim v2() As Graph.ValueItem
        Dim v3() As Graph.ValueItem
        Dim cCol As New Collection
        cCol = it.GetMonitorItems()

        ' Limit DT pickers
        Me.dtMonitorL.MaxDate = Date.Now
        Me.dtMonitorL.MinDate = it.GetMonitorCreationDate
        Me.dtMonitorR.MaxDate = Me.dtMonitorL.MaxDate
        Me.dtMonitorR.MinDate = Me.dtMonitorL.MinDate

        If cCol.Count > 0 Then

            ReDim v(cCol.Count)
            ReDim v2(cCol.Count)
            ReDim v3(cCol.Count)
            Dim c As cMonitor.MonitorStructure
            Dim i As Integer = 0

            For Each c In cCol
                If i < v.Length Then
                    Select Case key1
                        Case "CPU percentage"
                            v(i).y = CLng(c.cpuCounter * 10000)
                        Case "CPU time"
                            v(i).y = c.cpuTime
                        Case "Priority"
                            v(i).y = c.priority
                        Case "Thread count"
                            v(i).y = c.threadsCount
                        Case "Memory.PageFaultCount"
                            v(i).y = CInt(c.mem.PageFaultCount)
                        Case "Memory.PeakWorkingSetSize"
                            v(i).y = CInt(c.mem.PeakWorkingSetSize / 1024 / 1024)
                        Case "Memory.WorkingSetSize"
                            v(i).y = CInt(c.mem.WorkingSetSize / 1024 / 1024)
                        Case "Memory.QuotaPeakPagedPoolUsage"
                            v(i).y = CInt(c.mem.QuotaPeakPagedPoolUsage / 1024)
                        Case "TMemory.QuotaPagedPoolUsage"
                            v(i).y = CInt(c.mem.QuotaPagedPoolUsage / 1024)
                        Case "Memory.QuotaPeakNonPagedPoolUsage"
                            v(i).y = CInt(c.mem.QuotaPeakNonPagedPoolUsage / 1024)
                        Case "Memory.QuotaNonPagedPoolUsage"
                            v(i).y = CInt(c.mem.QuotaNonPagedPoolUsage / 1024)
                        Case "Memory.PagefileUsage"
                            v(i).y = CInt(c.mem.PagefileUsage / 1024 / 1024)
                        Case "Memory.PeakPagefileUsage"
                            v(i).y = CInt(c.mem.PeakPagefileUsage / 1024 / 1024)
                    End Select
                    Select Case key2
                        Case "Memory.PageFaultCount"
                            v2(i).y = CInt(c.mem.PageFaultCount)
                        Case "Memory.PeakWorkingSetSize"
                            v2(i).y = CInt(c.mem.PeakWorkingSetSize / 1024 / 1024)
                        Case "Memory.WorkingSetSize"
                            v2(i).y = CInt(c.mem.WorkingSetSize / 1024 / 1024)
                        Case "Memory.QuotaPeakPagedPoolUsage"
                            v2(i).y = CInt(c.mem.QuotaPeakPagedPoolUsage / 1024)
                        Case "TMemory.QuotaPagedPoolUsage"
                            v2(i).y = CInt(c.mem.QuotaPagedPoolUsage / 1024)
                        Case "Memory.QuotaPeakNonPagedPoolUsage"
                            v2(i).y = CInt(c.mem.QuotaPeakNonPagedPoolUsage / 1024)
                        Case "Memory.QuotaNonPagedPoolUsage"
                            v2(i).y = CInt(c.mem.QuotaNonPagedPoolUsage / 1024)
                        Case "Memory.PagefileUsage"
                            v2(i).y = CInt(c.mem.PagefileUsage / 1024 / 1024)
                        Case "Memory.PeakPagefileUsage"
                            v2(i).y = CInt(c.mem.PeakPagefileUsage / 1024 / 1024)
                    End Select
                    Select Case key3
                        Case "Memory.PageFaultCount"
                            v3(i).y = CInt(c.mem.PageFaultCount)
                        Case "Memory.PeakWorkingSetSize"
                            v3(i).y = CInt(c.mem.PeakWorkingSetSize / 1024 / 1024)
                        Case "Memory.WorkingSetSize"
                            v3(i).y = CInt(c.mem.WorkingSetSize / 1024 / 1024)
                        Case "Memory.QuotaPeakPagedPoolUsage"
                            v3(i).y = CInt(c.mem.QuotaPeakPagedPoolUsage / 1024)
                        Case "TMemory.QuotaPagedPoolUsage"
                            v3(i).y = CInt(c.mem.QuotaPagedPoolUsage / 1024)
                        Case "Memory.QuotaPeakNonPagedPoolUsage"
                            v3(i).y = CInt(c.mem.QuotaPeakNonPagedPoolUsage / 1024)
                        Case "Memory.QuotaNonPagedPoolUsage"
                            v3(i).y = CInt(c.mem.QuotaNonPagedPoolUsage / 1024)
                        Case "Memory.PagefileUsage"
                            v3(i).y = CInt(c.mem.PagefileUsage / 1024 / 1024)
                        Case "Memory.PeakPagefileUsage"
                            v3(i).y = CInt(c.mem.PeakPagefileUsage / 1024 / 1024)
                    End Select
                    v(i).x = c.time
                    i += 1
                End If
            Next

            ReDim Preserve v(cCol.Count - 1)
            ReDim Preserve v2(cCol.Count - 1)
            ReDim Preserve v3(cCol.Count - 1)

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
                .SetValues2(v2)
                .SetValues3(v3)
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
            Dim it As cMonitor = CType(tvMonitor.SelectedNode.Parent.Tag, cMonitor)
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
            frm._selProcess = CType(it.Tag, cProcess).GetPid
            frm.ShowDialog()
        Next
    End Sub
End Class
