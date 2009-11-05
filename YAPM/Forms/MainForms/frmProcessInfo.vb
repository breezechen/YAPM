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
Imports System.Threading

Public Class frmProcessInfo

    Private WithEvents asyncAllNonFixedInfos As asyncCallbackProcGetAllNonFixedInfos

    Private WithEvents curProc As cProcess
    Private WithEvents _AsyncDownload As cAsyncProcInfoDownload
    Private _asyncDlThread As Threading.Thread

    Private WithEvents theConnection As cConnection

    ' String search (in process image/memory) private attributes
    Private _stringSearchImmediateStop As Boolean   ' Set to true to stop listing of string in process
    Private __sRes() As String
    Private __lRes() As IntPtr
    Private cRW As ProcessRW

    Private Const SIZE_FOR_STRING As Integer = 4

    Private _historyGraphNumber As Integer = 0
    Private _local As Boolean = True
    Private _notWMI As Boolean

    ' Caption to display when process has terminated
    ' This also defines (if not null) that process has terminated
    Private fixedFormCaption As String = Nothing

    ' The listview in which we will search using 'search panel'
    Private listViewForSearch As ListView

    ' ProcessHandle for process termination handler
    Private hProcSync As IntPtr

    ' Debug buffer
    Private _dbgBuffer As Native.Objects.DebugBuffer

    ' Here we finished to download informations from internet
    Private _asyncInfoRes As cAsyncProcInfoDownload.InternetProcessInfo
    Private _asyncDownloadDone As Boolean = False

    ' Some declarations used for "log feature"
    Private _logCaptureMask As asyncCallbackLogEnumerate.LogItemType = asyncCallbackLogEnumerate.LogItemType.AllItems
    Private _logDisplayMask As asyncCallbackLogEnumerate.LogItemType = asyncCallbackLogEnumerate.LogItemType.AllItems
    Private _autoScroll As Boolean = False
    Private _logDico As New Dictionary(Of Integer, LogItem)

    Public Structure LogItem
        Public _date As Date
        Public _desc As String
        Public _type As asyncCallbackLogEnumerate.LogItemType
        Public _created As Boolean
        Public Sub New(ByVal aDesc As String, ByVal aType As asyncCallbackLogEnumerate.LogItemType, _
                       ByVal created As Boolean)
            _date = Date.Now
            _desc = aDesc
            _type = aType
            _created = created
        End Sub
    End Structure
    Public Property LogCaptureMask() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _logCaptureMask
        End Get
        Set(ByVal value As asyncCallbackLogEnumerate.LogItemType)
            _logCaptureMask = value
        End Set
    End Property
    Public Property LogDisplayMask() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _logDisplayMask
        End Get
        Set(ByVal value As asyncCallbackLogEnumerate.LogItemType)
            _logDisplayMask = value
        End Set
    End Property
    Public Property LvAutoScroll() As Boolean
        Get
            Return _autoScroll
        End Get
        Set(ByVal value As Boolean)
            _autoScroll = value
        End Set
    End Property


    ' Refresh current tab
    Private Sub refreshProcessTab()

        If curProc Is Nothing Then Exit Sub

        ' General informations
        Select Case Me.tabProcess.SelectedTab.Text

            Case "Token"
                If _notWMI Then _
                Call ShowPrivileges()

            Case "Modules"
                'If _local Then _
                Call ShowModules()

            Case "Threads"
                'If _local Then _
                Call ShowThreads()

            Case "Windows"
                If _notWMI Then _
                Call ShowWindows()

            Case "Handles"
                If _notWMI Then _
                Call ShowHandles()

            Case "Memory"
                If _notWMI Then _
                Call ShowRegions()

            Case "Environment"
                If _notWMI Then _
                Call ShowEnvVariables()

            Case "Network"
                If _notWMI Then _
                Call ShowNetwork()

            Case "Services"
                'If _local Then _
                Call ShowServices()

            Case "Heaps"
                If _notWMI Then _
                Call ShowHeaps()

            Case "Strings"
                If _local Then
                    ThreadPool.QueueUserWorkItem(AddressOf getProcString, curProc)
                End If

            Case "General"
                Me.txtProcessPath.Text = curProc.Infos.Path
                Me.txtProcessId.Text = curProc.Infos.ProcessId.ToString
                Me.txtParentProcess.Text = curProc.Infos.ParentProcessId.ToString & " -- " & cProcess.GetProcessName(curProc.Infos.ParentProcessId)
                Me.txtProcessStarted.Text = New Date(curProc.Infos.StartTime).ToLongDateString & " -- " & New Date(curProc.Infos.StartTime).ToLongTimeString
                Me.txtProcessUser.Text = curProc.Infos.UserName
                Me.txtCommandLine.Text = curProc.Infos.CommandLine
                Dim sp As TimeSpan = New TimeSpan(curProc.Infos.StartTime)
                Dim d As Date = Date.Now.Subtract(sp)
                Me.txtRunTime.Text = d.ToLongTimeString
                Me.txtPriority.Text = curProc.Infos.Priority.ToString
                If curProc.Infos.FileInfo IsNot Nothing Then
                    Me.txtImageVersion.Text = curProc.Infos.FileInfo.FileVersion
                    Me.lblCopyright.Text = curProc.Infos.FileInfo.LegalCopyright
                    Me.lblDescription.Text = curProc.Infos.FileInfo.FileDescription
                Else
                    Me.txtImageVersion.Text = NO_INFO_RETRIEVED
                    Me.lblCopyright.Text = NO_INFO_RETRIEVED
                    Me.lblDescription.Text = NO_INFO_RETRIEVED
                End If


            Case "Statistics"

                ' OK, here's the deal :
                ' - if we are in local mode, we just display informations that
                '   are available in curproc, because the refreshment is done
                '   by the main form.
                ' - if it is a remote connection, we juste demand a refreshment, and
                '   the refreshment will be done next time we call the refreshProcessTab
                '   method.

                Call refreshStatisticsTab()
                If cProcess.Connection.ConnectionObj.ConnectionType <> _
                        cConnection.TypeOfConnection.LocalConnection Then
                    Call Threading.ThreadPool.QueueUserWorkItem(New  _
                       System.Threading.WaitCallback(AddressOf _
                       asyncAllNonFixedInfos.Process))
                End If


            Case "Informations"

                ' OK, here's the deal :
                ' - if we are in local mode, we just display informations that
                '   are available in curproc, because the refreshment is done
                '   by the main form.
                ' - if it is a remote connection, we juste demand a refreshment, and
                '   the refreshment will be done next time we call the refreshProcessTab
                '   method.

                Call refreshInfosTab()
                If cProcess.Connection.ConnectionObj.ConnectionType <> _
                        cConnection.TypeOfConnection.LocalConnection Then
                    Call Threading.ThreadPool.QueueUserWorkItem(New  _
                       System.Threading.WaitCallback(AddressOf _
                       asyncAllNonFixedInfos.Process))
                End If

        End Select
    End Sub

    ' Refresh statistics tab
    Private Sub refreshStatisticsTab()
        Me.lblProcOther.Text = curProc.Infos.IOValues.OtherOperationCount.ToString
        Me.lblProcOtherBytes.Text = GetFormatedSize(curProc.Infos.IOValues.OtherTransferCount)
        Me.lblProcReads.Text = curProc.Infos.IOValues.ReadOperationCount.ToString
        Me.lblProcReadBytes.Text = GetFormatedSize(curProc.Infos.IOValues.ReadTransferCount)
        Me.lblProcWriteBytes.Text = GetFormatedSize(curProc.Infos.IOValues.WriteTransferCount)
        Me.lblProcWrites.Text = curProc.Infos.IOValues.WriteOperationCount.ToString
        Me.lblGDIcount.Text = curProc.Infos.GdiObjects.ToString
        Me.lblThreads.Text = curProc.Infos.ThreadCount.ToString
        Me.lblUserObjectsCount.Text = curProc.Infos.UserObjects.ToString
        Me.lblAverageCPUusage.Text = curProc.GetInformation("AverageCpuUsage")

        Me.lblRBD.Text = curProc.GetInformation("ReadTransferCountDelta")
        Me.lblRD.Text = curProc.IODelta.ReadOperationCount.ToString
        Me.lblWBD.Text = curProc.GetInformation("WriteTransferCountDelta")
        Me.lblWD.Text = curProc.IODelta.WriteOperationCount.ToString
        Me.lblOtherD.Text = curProc.IODelta.OtherOperationCount.ToString
        Me.lblOthersBD.Text = curProc.GetInformation("OtherTransferCountDelta")

        Dim mem As Native.Api.NativeStructs.VmCountersEx = curProc.Infos.MemoryInfos
        Me.lblHandles.Text = CStr(curProc.Infos.HandleCount)
        Dim ts As Date = New Date(curProc.Infos.KernelTime)
        Dim s As String = String.Format("{0:00}", ts.Hour) & ":" & _
            String.Format("{0:00}", ts.Minute) & ":" & _
            String.Format("{0:00}", ts.Second) & ":" & _
            String.Format("{000}", ts.Millisecond)
        Me.lblKernelTime.Text = s
        Me.lblPageFaults.Text = CStr(mem.PageFaultCount)
        Me.lblPageFileUsage.Text = GetFormatedSize(mem.PagefileUsage)
        Me.lblPeakPageFileUsage.Text = GetFormatedSize(mem.PeakPagefileUsage)
        Me.lblPeakWorkingSet.Text = GetFormatedSize(mem.PeakWorkingSetSize)
        ts = New Date(curProc.Infos.ProcessorTime)
        s = String.Format("{0:00}", ts.Hour) & ":" & _
            String.Format("{0:00}", ts.Minute) & ":" & _
            String.Format("{0:00}", ts.Second) & ":" & _
            String.Format("{000}", ts.Millisecond)
        Me.lblTotalTime.Text = s
        ts = New Date(curProc.Infos.UserTime)
        s = String.Format("{0:00}", ts.Hour) & ":" & _
            String.Format("{0:00}", ts.Minute) & ":" & _
            String.Format("{0:00}", ts.Second) & ":" & _
            String.Format("{000}", ts.Millisecond)
        Me.lblUserTime.Text = s
        Me.lblPriority.Text = curProc.Infos.Priority.ToString
        Me.lblWorkingSet.Text = GetFormatedSize(mem.WorkingSetSize)
        Me.lblQuotaNPP.Text = GetFormatedSize(mem.QuotaNonPagedPoolUsage)
        Me.lblQuotaPNPP.Text = GetFormatedSize(mem.QuotaPeakNonPagedPoolUsage)
        Me.lblQuotaPP.Text = GetFormatedSize(mem.QuotaPagedPoolUsage)
        Me.lblQuotaPPP.Text = GetFormatedSize(mem.QuotaPeakPagedPoolUsage)
    End Sub

    ' Refresh information tab
    Private Sub refreshInfosTab()
        Try
            Dim pmc As Native.Api.NativeStructs.VmCountersEx = curProc.Infos.MemoryInfos
            Dim s As String = ""
            s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
            s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b File properties\b0\par"
            s = s & "\tab File name :\tab\tab\tab " & curProc.Infos.Name & "\par"
            s = s & "\tab Path :\tab\tab\tab\tab " & Replace(curProc.Infos.Path, "\", "\\") & "\par"
            If curProc.Infos.FileInfo IsNot Nothing Then
                s = s & "\tab Description :\tab\tab\tab " & curProc.Infos.FileInfo.FileDescription & "\par"
                s = s & "\tab Company name :\tab\tab\tab " & curProc.Infos.FileInfo.CompanyName & "\par"
                s = s & "\tab Version :\tab\tab\tab " & curProc.Infos.FileInfo.FileVersion & "\par"
                s = s & "\tab Copyright :\tab\tab\tab " & curProc.Infos.FileInfo.LegalCopyright & "\par"
            End If
            s = s & "\par"
            s = s & "  \b Process description\b0\par"
            s = s & "\tab PID :\tab\tab\tab\tab " & CStr(curProc.Infos.ProcessId) & "\par"
            s = s & "\tab Start time :\tab\tab\tab " & New Date(curProc.Infos.StartTime).ToLongDateString & " -- " & New Date(curProc.Infos.StartTime).ToLongTimeString & "\par"
            s = s & "\tab Priority :\tab\tab\tab\tab " & curProc.Infos.Priority.ToString & "\par"
            s = s & "\tab User :\tab\tab\tab\tab " & curProc.Infos.UserName & "\par"
            Dim ts As Date = New Date(curProc.Infos.ProcessorTime)
            Dim proctime As String = String.Format("{0:00}", ts.Hour) & ":" & _
                String.Format("{0:00}", ts.Minute) & ":" & _
                String.Format("{0:00}", ts.Second) & ":" & _
                String.Format("{000}", ts.Millisecond)
            s = s & "\tab Processor time :\tab\tab\tab " & proctime & "\par"
            s = s & "\tab Memory :\tab\tab\tab " & CStr(pmc.WorkingSetSize.ToInt64 / 1024) & " Kb" & "\par"
            s = s & "\tab Memory peak :\tab\tab\tab " & CStr(pmc.PeakWorkingSetSize.ToInt64 / 1024) & " Kb" & "\par"
            s = s & "\tab Page faults :\tab\tab\tab " & CStr(pmc.PageFaultCount) & "\par"
            s = s & "\tab Page file usage :\tab\tab\tab " & CStr(pmc.PagefileUsage.ToInt64 / 1024) & " Kb" & "\par"
            s = s & "\tab Peak page file usage :\tab\tab " & CStr(pmc.PeakPagefileUsage.ToInt64 / 1024) & " Kb" & "\par"
            s = s & "\tab QuotaPagedPoolUsage :\tab\tab " & CStr(Math.Round(pmc.QuotaPagedPoolUsage.ToInt64 / 1024, 3)) & " Kb" & "\par"
            s = s & "\tab QuotaPeakPagedPoolUsage :\tab " & CStr(Math.Round(pmc.QuotaPeakPagedPoolUsage.ToInt64 / 1024, 3)) & " Kb" & "\par"
            s = s & "\tab QuotaNonPagedPoolUsage :\tab " & CStr(Math.Round(pmc.QuotaNonPagedPoolUsage.ToInt64 / 1024, 3)) & " Kb" & "\par"
            s = s & "\tab QuotaPeakNonPagedPoolUsage :\tab " & CStr(Math.Round(pmc.QuotaPeakNonPagedPoolUsage.ToInt64 / 1024, 3)) & " Kb" & "\par"

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

            pctSmallIcon.Image = My.Resources.exe16
            pctBigIcon.Image = My.Resources.exe32

        End Try
    End Sub

    Private Sub frmProcessInfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Save columns infos before closing
        Pref.SaveListViewColumns(Me.lvLog, "COLprocdetail_log")
        Pref.SaveListViewColumns(Me.lvHandles, "COLprocdetail_handle")
        Pref.SaveListViewColumns(Me.lvPrivileges, "COLprocdetail_privilege")
        Pref.SaveListViewColumns(Me.lvProcMem, "COLprocdetail_memory")
        Pref.SaveListViewColumns(Me.lvProcNetwork, "COLprocdetail_network")
        Pref.SaveListViewColumns(Me.lvModules, "COLprocdetail_module")
        Pref.SaveListViewColumns(Me.lvProcEnv, "COLprocdetail_envvariable")
        Pref.SaveListViewColumns(Me.lvHeaps, "COLprocdetail_heaps")

        ' Save position & size
        Pref.SaveFormPositionAndSize(Me, "PSfrmProcessInfo")

        ' Close handles opened
        If _local AndAlso hProcSync.IsNotNull Then
            Native.Objects.General.CloseHandle(hProcSync)
        End If

        ' Empty debug buffer
        If _dbgBuffer IsNot Nothing Then
            _dbgBuffer.Dispose()
        End If
    End Sub

    Private Sub frmProcessInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Hide panel 'Find window' if necessary
        If My.Settings.ShowFindWindowDetailedForm Then
            Call showFindPanel()
        Else
            Call hideFindPanel()
        End If

        CloseWithEchapKey(Me)

        ' Cool theme
        Native.Functions.Misc.SetTheme(Me.lvProcString.Handle)
        Native.Functions.Misc.SetTheme(Me.lvProcEnv.Handle)
        Native.Functions.Misc.SetTheme(Me.lvProcNetwork.Handle)
        Native.Functions.Misc.SetTheme(Me.lvProcMem.Handle)
        Native.Functions.Misc.SetTheme(Me.lvProcServices.Handle)
        Native.Functions.Misc.SetTheme(Me.lvPrivileges.Handle)
        Native.Functions.Misc.SetTheme(Me.lvModules.Handle)
        Native.Functions.Misc.SetTheme(Me.lvHandles.Handle)
        Native.Functions.Misc.SetTheme(Me.lvThreads.Handle)
        Native.Functions.Misc.SetTheme(Me.lvWindows.Handle)
        Native.Functions.Misc.SetTheme(Me.lvLog.Handle)
        Native.Functions.Misc.SetTheme(Me.lvHeaps.Handle)

        ' Some tooltips
        SetToolTip(Me.cmdInfosToClipB, "Copy process informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style)")
        SetToolTip(Me.txtImageVersion, "Version of file")
        SetToolTip(Me.txtProcessPath, "Path of file")
        SetToolTip(Me.cmdShowFileDetails, "Show file details")
        SetToolTip(Me.cmdInspectExe, "Show dependencies")
        SetToolTip(Me.cmdShowFileProperties, "Open file property window")
        SetToolTip(Me.cmdOpenDirectory, "Open directory of file")
        SetToolTip(Me.txtParentProcess, "Parent process")
        SetToolTip(Me.txtProcessStarted, "Start time")
        SetToolTip(Me.txtProcessId, "Process ID")
        SetToolTip(Me.txtProcessUser, "Process user")
        SetToolTip(Me.txtCommandLine, "Command line which launched process")
        SetToolTip(Me.cmdGetOnlineInfos, "Retrieve online information (based on process name)")
        SetToolTip(Me.optProcStringImage, "Get strings from image file on disk")
        SetToolTip(Me.optProcStringMemory, "Get strings from memory")
        SetToolTip(Me.cmdProcStringSave, "Save list in a file")
        SetToolTip(Me.pgbString, "Progression. Click to stop")
        SetToolTip(Me.txtSearchProcString, "Search a specific string")
        SetToolTip(Me.txtRunTime, "Total run time")
        SetToolTip(Me.cmdProcSearchL, "Previous result (F2 on listview also works)")
        SetToolTip(Me.cmdProcSearchR, "Next result (F3 on listview also works)")
        SetToolTip(Me.cmdRefresh, "Refresh informations")
        SetToolTip(Me.chkLog, "Activate or not log")
        SetToolTip(Me.cmdLogOptions, "Log options")
        SetToolTip(Me.cmdSave, "Save log")
        SetToolTip(Me.cmdClearLog, "Clear log")
        SetToolTip(Me.cmdKill, "Kill process")
        SetToolTip(Me.cmdResume, "Resume process")
        SetToolTip(Me.cmdPause, "Suspend process")
        SetToolTip(Me.cmdAffinity, "Set affinity")
        SetToolTip(Me.cmdSet, "Set priority")
        SetToolTip(Me.cbPriority, "Priority of the process")
        SetToolTip(Me.chkFreeze, "Suspend refreshment of informations to let you search into the view")
        SetToolTip(Me.lblResCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearch, "Enter text here to search an item")
        SetToolTip(Me.cmdHideFindPanel, "Hide 'find panel'")

        ' Init columns
        Pref.LoadListViewColumns(Me.lvPrivileges, "COLprocdetail_privilege")
        Pref.LoadListViewColumns(Me.lvProcMem, "COLprocdetail_memory")
        Pref.LoadListViewColumns(Me.lvProcServices, "COLprocdetail_service")
        Pref.LoadListViewColumns(Me.lvProcNetwork, "COLprocdetail_network")
        Pref.LoadListViewColumns(Me.lvHandles, "COLprocdetail_handle")
        Pref.LoadListViewColumns(Me.lvWindows, "COLprocdetail_window")
        Pref.LoadListViewColumns(Me.lvThreads, "COLprocdetail_thread")
        Pref.LoadListViewColumns(Me.lvModules, "COLprocdetail_module")
        Pref.LoadListViewColumns(Me.lvProcEnv, "COLprocdetail_envvariable")
        Pref.LoadListViewColumns(Me.lvLog, "COLprocdetail_log")
        Pref.LoadListViewColumns(Me.lvHeaps, "COLprocdetail_heaps")

        ' Init position & size
        Pref.LoadFormPositionAndSize(Me, "PSfrmProcessInfo")

        Select Case My.Settings.ProcSelectedTab
            Case "Token"
                Me.tabProcess.SelectedTab = Me.TabPageToken
            Case "Modules"
                Me.tabProcess.SelectedTab = Me.TabPageModules
            Case "Threads"
                Me.tabProcess.SelectedTab = Me.TabPageThreads
            Case "Windows"
                Me.tabProcess.SelectedTab = Me.TabPageWindows
            Case "Handles"
                Me.tabProcess.SelectedTab = Me.TabPageHandles
            Case "Memory"
                Me.tabProcess.SelectedTab = Me.TabPageMemory
            Case "Environment"
                Me.tabProcess.SelectedTab = Me.TabPageEnv
            Case "Network"
                Me.tabProcess.SelectedTab = Me.TabPageNetwork
            Case "Services"
                Me.tabProcess.SelectedTab = Me.TabPageServices
            Case "Strings"
                Me.tabProcess.SelectedTab = Me.TabPageString
            Case "General"
                Me.tabProcess.SelectedTab = Me.TabPageGeneral
            Case "Statistics"
                Me.tabProcess.SelectedTab = Me.TabPageStats
            Case "Informations"
                Me.tabProcess.SelectedTab = Me.TabPageInfos
            Case "Performances"
                Me.tabProcess.SelectedTab = Me.TabPagePerf
            Case "Log"
                Me.tabProcess.SelectedTab = Me.TabPageLog
            Case "History"
                Me.tabProcess.SelectedTab = Me.TabPageHistory
            Case "Heaps"
                Me.tabProcess.SelectedTab = Me.TabPageHeaps
        End Select

        ' Icons
        If pctBigIcon.Image Is Nothing Then
            Try
                pctBigIcon.Image = GetIcon(curProc.Infos.Path, False).ToBitmap
                pctSmallIcon.Image = GetIcon(curProc.Infos.Path, True).ToBitmap
            Catch ex As Exception
                pctSmallIcon.Image = My.Resources.exe16
                pctBigIcon.Image = My.Resources.exe32
            End Try
        End If

        Call Connect()
        Call refreshProcessTab()

        If My.Settings.AutomaticInternetInfos Then
            Call cmdGetOnlineInfos_Click(Nothing, Nothing)
        End If

        ' Add some submenus (Copy to clipboard)
        For Each ss As String In handleInfos.GetAvailableProperties(True)
            Me.MenuItemCopyHandle.MenuItems.Add(ss, AddressOf MenuItemCopyHandle_Click)
        Next
        For Each ss As String In memRegionInfos.GetAvailableProperties(True)
            Me.MenuItemCopyMemory.MenuItems.Add(ss, AddressOf MenuItemCopyMemory_Click)
        Next
        For Each ss As String In moduleInfos.GetAvailableProperties(True)
            Me.MenuItemCopyModule.MenuItems.Add(ss, AddressOf MenuItemCopyModule_Click)
        Next
        For Each ss As String In networkInfos.GetAvailableProperties(True)
            Me.MenuItemCopyNetwork.MenuItems.Add(ss, AddressOf MenuItemCopyNetwork_Click)
        Next
        For Each ss As String In privilegeInfos.GetAvailableProperties(True)
            Me.MenuItemCopyPrivilege.MenuItems.Add(ss, AddressOf MenuItemCopyPrivilege_Click)
        Next
        For Each ss As String In serviceInfos.GetAvailableProperties(True)
            Me.MenuItemCopyService.MenuItems.Add(ss, AddressOf MenuItemCopyService_Click)
        Next
        For Each ss As String In threadInfos.GetAvailableProperties(True)
            Me.MenuItemCopyThread.MenuItems.Add(ss, AddressOf MenuItemCopyThread_Click)
        Next
        For Each ss As String In windowInfos.GetAvailableProperties(True)
            Me.MenuItemCopyWindow.MenuItems.Add(ss, AddressOf MenuItemCopyWindow_Click)
        Next
        For Each ss As String In logItemInfos.GetAvailableProperties(True)
            Me.MenuItemCopyLog.MenuItems.Add(ss, AddressOf MenuItemCopyLog_Click)
        Next
        For Each ss As String In envVariableInfos.GetAvailableProperties(True)
            Me.MenuItemCopyEnvVariable.MenuItems.Add(ss, AddressOf MenuItemCopyEnvVariable_Click)
        Next
        For Each ss As String In heapInfos.GetAvailableProperties(True)
            Me.MenuItemCopyHeaps.MenuItems.Add(ss, AddressOf MenuItemCopyHeaps_Click)
        Next
        Me.MenuItemCopyString.MenuItems.Add("Position", AddressOf MenuItemCopyString_Click)
        Me.MenuItemCopyString.MenuItems.Add("String", AddressOf MenuItemCopyString_Click)

    End Sub

    ' Get process to monitor
    Public Sub SetProcess(ByRef process As cProcess)

        curProc = process
        asyncAllNonFixedInfos = New asyncCallbackProcGetAllNonFixedInfos(cProcess.Connection, curProc)

        Me.Text = curProc.Infos.Name & " (" & CStr(curProc.Infos.ProcessId) & ")"
        Me.cbPriority.Text = curProc.Infos.Priority.ToString

        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cProcess.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        Me.cmdAffinity.Enabled = _notWMI
        Me.cmdPause.Enabled = _notWMI
        Me.cmdResume.Enabled = _notWMI
        Me.lvModules.CatchErrors = Not (_local)
        Me.timerProcPerf.Enabled = True
        Me.lvPrivileges.Enabled = _notWMI
        Me.lvHandles.Enabled = _notWMI
        Me.lvLog.Enabled = _notWMI
        Me.lvProcEnv.Enabled = _notWMI
        Me.lvProcMem.Enabled = _notWMI
        Me.lvProcNetwork.Enabled = _notWMI
        Me.lvProcString.Enabled = _notWMI
        Me.lvWindows.Enabled = _notWMI
        Me.lvHeaps.Enabled = _notWMI
        Me.SplitContainerStrings.Enabled = _notWMI
        Me.SplitContainerLog.Enabled = _notWMI
        Me.cmdShowFileDetails.Enabled = _local
        Me.cmdInspectExe.Enabled = _local
        Me.cmdShowFileProperties.Enabled = _local
        Me.cmdOpenDirectory.Enabled = _local

        Me.TabPageString.Enabled = _local

        Me.timerLog.Enabled = Me.timerLog.Enabled And _notWMI

        ' Verify file
        If My.Settings.AutomaticWintrust AndAlso _local Then
            Try
                Dim bVer As Boolean = Security.WinTrust.WinTrust.VerifyEmbeddedSignature(curProc.Infos.Path)
                If bVer Then
                    gpProcGeneralFile.Text = "Image file (successfully verified)"
                Else
                    gpProcGeneralFile.Text = "Image file (not verified)"
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        Else
            gpProcGeneralFile.Text = "Image file (no verification was made)"
        End If

        ' Parent process exists ?
        Me.cmdGoProcess.Enabled = (cProcess.GetProcessById(curProc.Infos.ParentProcessId) IsNot Nothing)


        ' Add values to perf graphs
        ' Memory usage
        For Each _val As Double In curProc.GetHistory("WorkingSet")
            Me.graphMemory.AddValue(_val / 2147483648 * 100)
        Next
        Me.graphMemory.Refresh()

        ' CpuUsage
        Dim _v() As Double = curProc.GetHistory("CpuUsage")
        Dim _v2() As Double = curProc.GetHistory("AverageCpuUsage")
        Dim x As Integer = 0
        For Each _val2 As Double In _v2
            Dim z As Double = _v(x)
            x += 1
            If Double.IsNegativeInfinity(z) Then
                z = 0
            End If
            Me.graphCPU.Add2Values(z, _val2)
        Next
        Me.graphCPU.Refresh()

        ' IO graph
        _v = curProc.GetHistory("ReadTransferCountDelta")
        Dim __v() As Double = curProc.GetHistory("OtherTransferCountDelta")
        _v2 = curProc.GetHistory("WriteTransferCountDelta")
        x = 0
        For Each _val2 As Double In _v2
            Dim z As Double = _v(x) + __v(x)
            x += 1
            Me.graphIO.Add2Values(z, _val2)
        Next
        Me.graphIO.Refresh()



        ' Set handler for process termination
        If _local Then
            hProcSync = Native.Objects.Process.GetProcessHandleById(process.Infos.ProcessId, _
                                            Native.Security.ProcessAccess.Synchronize Or Native.Objects.Process.ProcessQueryMinRights)
            Dim contObj As New Native.Objects.Process.ProcessTerminationStruct(hProcSync, AddressOf ProcessHasTerminatedHandler)
            Threading.ThreadPool.QueueUserWorkItem(AddressOf Native.Objects.Process.WaitForProcessToTerminate, contObj)
        End If
    End Sub

    Private Sub timerProcPerf_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcPerf.Tick

        If Me.chkFreeze.Checked Then
            Exit Sub
        End If

        Static updatedRisk As Boolean = False

        Dim z As Double = curProc.CpuUsage
        Dim z2 As Double = curProc.Infos.AverageCpuUsage
        If Double.IsNegativeInfinity(z) Then
            z = 0
        End If
        Me.graphCPU.Add2Values(z * 100, z2 * 100)
        Me.graphCPU.Refresh()
        Me.graphCPU.TopText = "Cpu : " & Misc.GetFormatedPercentage(z, 3, True) & " %"

        z = curProc.Infos.MemoryInfos.WorkingSetSize.ToInt64 / 2147483648 * 100
        Me.graphMemory.AddValue(z)
        Me.graphMemory.Refresh()
        Me.graphMemory.TopText = "WorkingSet : " & GetFormatedSize(curProc.Infos.MemoryInfos.WorkingSetSize.ToInt64)

        Me.graphIO.Add2Values(curProc.IODelta.ReadTransferCount + curProc.IODelta.OtherTransferCount, curProc.IODelta.WriteTransferCount)
        Me.graphIO.Refresh()
        Me.graphIO.TopText = "R+O : " & Misc.GetFormatedSizePerSecond(curProc.IODelta.ReadTransferCount + curProc.IODelta.OtherTransferCount) & " , W : " & Misc.GetFormatedSizePerSecond(curProc.IODelta.WriteTransferCount)


        ' Parent process exists ?
        Me.cmdGoProcess.Enabled = (cProcess.GetProcessById(curProc.Infos.ParentProcessId) IsNot Nothing)


        ' Refresh informations about process
        If Not (Me.tabProcess.SelectedTab.Text = "Informations" Or _
            Me.tabProcess.SelectedTab.Text = "Strings") Then _
            Call Me.refreshProcessTab()

        ' Display caption
        Call ChangeCaption()

        ' If online infos received, display it
        If _asyncDownloadDone AndAlso updatedRisk = False Then
            Me.lblSecurityRisk.Text = "Risk : " & _asyncInfoRes._Risk.ToString
            Me.rtbOnlineInfos.Text = _asyncInfoRes._Description
            _asyncDlThread.Abort()
            _asyncInfoRes = Nothing
            _asyncDlThread = Nothing
            _asyncDownloadDone = False
            Select Case _asyncInfoRes._Risk
                Case cAsyncProcInfoDownload.SecurityRisk.Alert1, _
                        cAsyncProcInfoDownload.SecurityRisk.Alert2, _
                        cAsyncProcInfoDownload.SecurityRisk.Alert3
                    Me.lblSecurityRisk.ForeColor = Color.DarkRed
                Case cAsyncProcInfoDownload.SecurityRisk.Caution1, _
                        cAsyncProcInfoDownload.SecurityRisk.Caution2
                    Me.lblSecurityRisk.ForeColor = Color.DarkOrange
                Case cAsyncProcInfoDownload.SecurityRisk.Safe
                    Me.lblSecurityRisk.ForeColor = Color.DarkGreen
                Case Else
                    Me.lblSecurityRisk.ForeColor = Color.Black
            End Select
        End If

    End Sub

    ' Change caption
    Private Sub ChangeCaption()

        ' Display form caption
        If String.IsNullOrEmpty(fixedFormCaption) = False Then
            Me.Text = fixedFormCaption
            ' Refresh a last time
            If Me.timerProcPerf.Enabled Then
                If Not (Me.tabProcess.SelectedTab.Text = "Informations" Or _
                         Me.tabProcess.SelectedTab.Text = "Strings") Then _
                         Call Me.refreshProcessTab()
            End If
            ' Stop auto refreshment
            Me.timerLog.Enabled = False
            Me.timerProcPerf.Enabled = False
            Me.chkLog.Enabled = False
            Me.chkLog.Checked = False
            Exit Sub
        Else
            Me.Text = curProc.Infos.Name & " (" & curProc.Infos.ProcessId.ToString & ")"
        End If

        Select Case Me.tabProcess.SelectedTab.Text
            Case "Modules"
                Me.Text &= " - " & Me.lvModules.Items.Count.ToString & " modules"
            Case "Threads"
                Me.Text &= " - " & Me.lvThreads.Items.Count.ToString & " threads"
            Case "Windows"
                Me.Text &= " - " & Me.lvWindows.Items.Count.ToString & " windows"
            Case "Handles"
                Me.Text &= " - " & Me.lvHandles.Items.Count.ToString & " handles"
            Case "Memory"
                Me.Text &= " - " & Me.lvProcMem.Items.Count.ToString & " memory regions"
            Case "Network"
                Me.Text &= " - " & Me.lvProcNetwork.Items.Count.ToString & " connections"
            Case "Services"
                Me.Text &= " - " & Me.lvProcServices.Items.Count.ToString & " services"
            Case "Strings"
                Me.Text &= " - " & Me.lvProcString.Items.Count.ToString & " strings"
            Case "Environment"
                Me.Text &= " - " & Me.lvProcEnv.Items.Count.ToString & " variables"
            Case "Token"
                Me.Text &= " - " & Me.lvPrivileges.Items.Count.ToString & " privileges"
            Case "Log"
                Me.Text &= " - " & Me.lvLog.Items.Count.ToString & " entries in log"
        End Select
    End Sub

    Private Sub getProcString(ByVal cuProc As Object)

        Static reentrance As Boolean = False
        If reentrance Then Exit Sub
        reentrance = True

        Dim curProc As cProcess = DirectCast(cuProc, cProcess)
        Async.SplitContainer.ChangeEnabled(Me.SplitContainerStrings, False)

        Me.lvProcString.Items.Clear()
        If Me.optProcStringImage.Checked Then
            ' Image
            Call DisplayFileStringsImage(curProc)
        Else
            ' Memory
            cRW = New ProcessRW(curProc.Infos.ProcessId)
            Dim lRes() As IntPtr
            ReDim lRes(0)
            Dim sRes() As String
            ReDim sRes(0)
            cRW.SearchEntireStringMemory(lRes, sRes, Me.pgbString)

            __sRes = sRes
            __lRes = lRes

            Async.ListView.ChangeVirtualListSize(Me.lvProcString, sRes.Length)

        End If

        reentrance = False
        Async.SplitContainer.ChangeEnabled(Me.SplitContainerStrings, True)
    End Sub


    ' Display file strings
    Public Sub DisplayFileStringsImage(ByRef cp As cProcess)
        Dim s As String = vbNullString
        Dim x As Integer = 0
        Dim bTaille As Integer
        Dim lLen As Integer
        Dim tRes() As ProcessRW.T_RESULT
        Dim cArraySizeBef As Integer = 0
        Dim strCtemp As String = vbNullString
        '        Dim strBuffer As String

        Const BUF_SIZE As Integer = 2000     ' Size of array

        ReDim tRes(BUF_SIZE)

        Dim file As String = cp.Infos.Path

        _stringSearchImmediateStop = False

        If IO.File.Exists(file) Then

            Me.lvProcString.Items.Clear()

            ' Retrieve entire file in memory
            s = IO.File.ReadAllText(file)

            ' Desired minimum size for a string
            bTaille = SIZE_FOR_STRING

            ' A char is considered as part of a string if its value is between 32 and 122
            lLen = Len(s)
            Async.ProgressBar.ChangeMaximum(Me.pgbString, CInt(lLen / 10000 + 2))
            Async.ProgressBar.ChangeValue(Me.pgbString, 0)

            ' Ok, parse file
            Do Until x >= lLen

                If _stringSearchImmediateStop Then
                    ' Exit
                    Async.ProgressBar.ChangeValue(Me.pgbString, Me.pgbString.Maximum)
                    Exit Sub
                End If

                If isLetter(s(x)) Then
                    strCtemp &= s.Chars(x)
                Else
                    'strCtemp = Trim$(strCtemp)
                    If Len(strCtemp) > SIZE_FOR_STRING Then

                        ' Resize only every BUF times
                        If cArraySizeBef = BUF_SIZE Then
                            cArraySizeBef = 0
                            ReDim Preserve tRes(tRes.Length + BUF_SIZE)
                        End If

                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = New IntPtr(x)
                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp
                        cArraySizeBef += 1

                    End If
                    strCtemp = vbNullString
                End If

                If (x Mod 10000) = 0 Then
                    Async.ProgressBar.ChangeValue(Me.pgbString, Me.pgbString.Value + 1)
                End If

                x += 1
            Loop

            Async.ProgressBar.ChangeValue(Me.pgbString, Me.pgbString.Maximum)

            ' Last item
            If Len(strCtemp) > SIZE_FOR_STRING Then
                ' Resize only every BUF times
                If cArraySizeBef = BUF_SIZE Then
                    cArraySizeBef = 0
                    ReDim Preserve tRes(tRes.Length + BUF_SIZE)
                End If

                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = New IntPtr(lLen)
                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp

            End If


            Dim lngRes() As IntPtr
            Dim strRes() As String
            ReDim lngRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1)
            ReDim strRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1)
            For x = 0 To tRes.Length - BUF_SIZE + cArraySizeBef - 1
                lngRes(x) = tRes(x).curOffset
                strRes(x) = tRes(x).strString
            Next x

            __sRes = strRes
            __lRes = lngRes

            Async.ListView.ChangeVirtualListSize(Me.lvProcString,tRes.Length - BUF_SIZE + cArraySizeBef - 1)

        End If

    End Sub

    ' Return true if c is a valid character
    Private Function isLetter(ByVal c As Char) As Boolean
        Dim i As Integer = Asc(c)
        ' A-Z [/]_^' space a-z {|}
        Return ((i >= 65 And i <= 125) OrElse (i >= 45 And i <= 57) OrElse i = 32)
    End Function

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

    Private Sub cmdOpenDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpenDirectory.Click
        ' Open directory of selected process
        If curProc.Infos.Path <> NO_INFO_RETRIEVED Then
            cFile.OpenDirectory(curProc.Infos.Path)
        End If
    End Sub

    Private Sub pctBigIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctBigIcon.MouseDown
        Me.MenuItemCopyBig.Enabled = (Me.pctBigIcon.Image IsNot Nothing)
    End Sub

    Private Sub pctSmallIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctSmallIcon.MouseDown
        Me.MenuItemCopySmall.Enabled = (Me.pctSmallIcon.Image IsNot Nothing)
    End Sub

    Private Sub tabProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabProcess.SelectedIndexChanged
        Call Me.refreshProcessTab()
        Call ChangeCaption()
        Call updateFindPanel()
        My.Settings.ProcSelectedTab = Me.tabProcess.SelectedTab.Text
    End Sub

    Private Sub cmdProcSearchL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcSearchL.Click
        Dim sSearch As String = Me.txtSearchProcString.Text.ToLowerInvariant
        Dim curIndex As Integer = Me.lvProcString.Items.Count - 1

        If Me.lvProcString.SelectedIndices IsNot Nothing AndAlso _
            Me.lvProcString.SelectedIndices.Count > 0 Then _
                curIndex = Me.lvProcString.SelectedIndices(0)

        For z As Integer = curIndex - 1 To 0 Step -1
            Dim sComp As String = Me.lvProcString.Items(z).SubItems(1).Text.ToLowerInvariant
            If InStr(sComp, sSearch, CompareMethod.Binary) > 0 Then
                Me.lvProcString.Items(curIndex).Selected = False
                Me.lvProcString.Items(z).Selected = True
                Me.lvProcString.Items(z).EnsureVisible()
                Me.lvProcString.Focus()
                Exit Sub
            End If
        Next
    End Sub

    Private Sub cmdProcSearchR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcSearchR.Click
        Dim sSearch As String = Me.txtSearchProcString.Text.ToLowerInvariant
        Dim curIndex As Integer = 0

        If Me.lvProcString.SelectedIndices IsNot Nothing AndAlso _
            Me.lvProcString.SelectedIndices.Count > 0 Then _
                curIndex = Me.lvProcString.SelectedIndices(0)

        For z As Integer = curIndex + 1 To Me.lvProcString.Items.Count - 1
            Dim sComp As String = Me.lvProcString.Items(z).SubItems(1).Text.ToLowerInvariant
            If InStr(sComp, sSearch, CompareMethod.Binary) > 0 Then
                Me.lvProcString.Items(curIndex).Selected = False
                Me.lvProcString.Items(z).Selected = True
                Me.lvProcString.Items(z).EnsureVisible()
                Me.lvProcString.Focus()
                Exit Sub
            End If
        Next
    End Sub

    Private Sub lvProcString_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcString.KeyDown
        If e.KeyCode = Keys.F2 Then
            Call cmdProcSearchL_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.F3 Then
            Call cmdProcSearchR_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub cmdProcStringSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcStringSave.Click

        ' Save list of strings
        With _frmMain.saveDial
            .AddExtension = True
            .Filter = "Txt (*.txt*)|*.txt"
            .InitialDirectory = My.Application.Info.DirectoryPath
            If Not (.ShowDialog = Windows.Forms.DialogResult.OK) Then
                Exit Sub
            End If
        End With

        ' Save our file
        Try
            Dim stream As New System.IO.StreamWriter(_frmMain.saveDial.FileName, False)
            For x As Integer = 0 To Me.lvProcString.Items.Count - 1
                stream.WriteLine(Me.lvProcString.Items(x).SubItems(1).Text)
            Next
            stream.Close()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    ' Add item to virtual listview
    Private Sub lvProcString_RetrieveVirtualItem(ByVal sender As Object, ByVal e As System.Windows.Forms.RetrieveVirtualItemEventArgs) Handles lvProcString.RetrieveVirtualItem
        e.Item = GetListItem(e.ItemIndex)
    End Sub
    ' Return desired item
    Private Function GetListItem(ByVal x As Integer) As ListViewItem
        Dim it As New ListViewItem("0x" & __lRes(x).ToString("x"))
        it.SubItems.Add(__sRes(x))
        it.Tag = __lRes(x)
        Return it
    End Function

    Private Sub lvProcMem_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvProcMem.DoubleClick
        For Each it As cMemRegion In Me.lvProcMem.GetSelectedItems
            Dim frm As New frmHexEditor
            Dim reg As New MemoryHexEditor.MemoryRegion(it.Infos.BaseAddress, it.Infos.RegionSize)
            frm.SetPidAndRegion(it.Infos.ProcessId, reg)
            frm.Show()
        Next
    End Sub

    Private Sub cmdShowFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowFileDetails.Click
        Dim s As String = curProc.Infos.Path
        If IO.File.Exists(s) Then
            DisplayDetailsFile(s)
        End If
    End Sub

    Private Sub cmdShowFileProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowFileProperties.Click
        ' File properties for selected process
        If IO.File.Exists(curProc.Infos.Path) Then
            cFile.ShowFileProperty(curProc.Infos.Path, Me.Handle)
        End If
    End Sub

    ' Stop string listing
    Private Sub pgbString_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pgbString.Click
        If cRW IsNot Nothing Then cRW.StopSearch = True
    End Sub

    Private Sub optProcStringImage_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optProcStringImage.CheckedChanged
        Call Me.refreshProcessTab()
    End Sub

    Private Sub optProcStringMemory_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optProcStringMemory.CheckedChanged
        Call Me.refreshProcessTab()
    End Sub

    Private Sub rtb_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.TextChanged
        Me.cmdInfosToClipB.Enabled = (Me.rtb.TextLength > 0)
    End Sub

    Private Sub cmdGetOnlineInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetOnlineInfos.Click
        If _asyncDlThread IsNot Nothing Then
            ' Already trying to get infos
            Exit Sub
        End If
        _AsyncDownload = New cAsyncProcInfoDownload(curProc.Infos.Name)

        ' Start async download of infos
        _asyncDlThread = New Threading.Thread(AddressOf _AsyncDownload.BeginDownload)
        With _asyncDlThread
            .IsBackground = True
            .Priority = Threading.ThreadPriority.Lowest
            .Start()
        End With
    End Sub

    ' Show services
    Public Sub ShowServices()

        ' Update list
        Me.lvProcServices.ShowAll = False
        Me.lvProcServices.ProcessId = curProc.Infos.ProcessId
        Me.lvProcServices.UpdateTheItems()

    End Sub

    ' Show modules
    Public Sub ShowModules()

        Dim pid() As Integer
        ReDim pid(0)
        pid(0) = curProc.Infos.ProcessId
        lvModules.ProcessId = pid
        lvModules.UpdateTheItems()

    End Sub

    ' Show env variables
    Public Sub ShowEnvVariables()

        lvProcEnv.ProcessId = curProc.Infos.ProcessId
        lvProcEnv.Peb = curProc.Infos.PebAddress
        lvProcEnv.UpdateTheItems()

    End Sub

    ' Show privileges
    Public Sub ShowPrivileges()

        lvPrivileges.ProcessId = curProc.Infos.ProcessId
        lvPrivileges.UpdateTheItems()

    End Sub

    ' Show threads
    Public Sub ShowThreads()

        Dim pid() As Integer
        ReDim pid(0)
        pid(0) = curProc.Infos.ProcessId
        Me.lvThreads.ProcessId = pid
        Me.lvThreads.UpdateTheItems()

    End Sub

    ' Show heaps
    Public Sub ShowHeaps()

        Me.lvHeaps.ProcessId = curProc.Infos.ProcessId
        Me.lvHeaps.UpdateTheItems()

    End Sub

    ' Show network connections
    Public Sub ShowNetwork()

        Dim pid(0) As Integer
        pid(0) = curProc.Infos.ProcessId
        Me.lvProcNetwork.ShowAllPid = False
        Me.lvProcNetwork.ProcessId = pid
        Me.lvProcNetwork.UpdateTheItems()

    End Sub

    ' Show memory regions
    Public Sub ShowRegions()

        Me.lvProcMem.ProcessId = curProc.Infos.ProcessId
        Me.lvProcMem.UpdateTheItems()

    End Sub

    ' Show threads
    Public Sub ShowWindows()

        Dim pid(0) As Integer
        pid(0) = curProc.Infos.ProcessId
        Me.lvWindows.ProcessId = pid
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowUnNamed = Me.MenuItemWShowUn.Checked
        Me.lvWindows.UpdateTheItems()

    End Sub

    ' Display handles of process
    Private Sub ShowHandles()

        Me.lvHandles.ShowUnnamed = Me.MenuItemShowUnnamedHandles.Checked
        Dim pids(0) As Integer
        pids(0) = curProc.Infos.ProcessId
        Me.lvHandles.ProcessId = pids
        Me.lvHandles.UpdateTheItems()

    End Sub

    ' Update log items
    Private Sub ShowLogItems()

        Me.lvLog.ProcessId = curProc.Infos.ProcessId
        Me.lvLog.CaptureItems = Me.LogCaptureMask
        Me.lvLog.DisplayItems = Me.LogDisplayMask

        Me.lvLog.UpdateTheItems()

    End Sub

    Private Sub lvProcString_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcString.MouseDown
        Me.menuViewMemory.Enabled = optProcStringMemory.Checked
    End Sub

    Private Sub chkLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLog.CheckedChanged
        If String.IsNullOrEmpty(fixedFormCaption) Then
            ' Then process has not yet terminated -> allow log
            Me.timerLog.Enabled = Me.chkLog.Checked
        Else
            Me.chkLog.Checked = False
            Me.chkLog.Enabled = False
        End If
    End Sub

    Private Sub timerLog_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerLog.Tick

        If Me.chkFreeze.Checked Then
            Exit Sub
        End If

        Dim _i As Integer = Native.Api.Win32.GetElapsedTime

        Me.lvLog.BeginUpdate()
        Call ShowLogItems()
        Me.lvLog.EndUpdate()

        If _autoScroll AndAlso Me.lvLog.Items.Count > 0 Then
            Me.lvLog.Items(Me.lvLog.Items.Count - 1).EnsureVisible()
        End If

        Call ChangeCaption()
        Trace.WriteLine("Log updated in " & (Native.Api.Win32.GetElapsedTime - _i).ToString & " ms")
    End Sub

    Private Sub cmdClearLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearLog.Click
        Me.lvLog.Items.Clear()
        _logDico.Clear()
        Call ChangeCaption()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        ' Save log
        _frmMain.saveDial.Filter = "Text file (*.txt)|*.txt"
        _frmMain.saveDial.Title = "Save log"
        Try
            If _frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = _frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    Dim stream As New System.IO.StreamWriter(s, False)
                    'Dim x As Integer = 0
                    For Each cm As ListViewItem In Me.lvLog.Items
                        stream.WriteLine(cm.Text & vbTab & cm.SubItems(1).Text & vbTab & cm.SubItems(2).Text)
                        'x += 1
                        '  UpdateProgress(x)
                    Next
                    stream.WriteLine()
                    stream.WriteLine(CStr(Me.lvLog.Items.Count) & " entries(s)")
                    stream.Close()
                    Misc.ShowMsg("Save log", Nothing, "Saved file successfully.", MessageBoxButtons.OK, TaskDialogIcon.ShieldOk)
                End If
            End If
        Catch ex As Exception
            Misc.ShowError(ex, "Could not save file")
        End Try

    End Sub

    Private Sub cmdLogOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogOptions.Click
        Dim frm As New frmLogOptions
        frm.LogCaptureMask = _logCaptureMask
        frm.LogDisplayMask = _logDisplayMask
        frm.Form = Me
        frm._autoScroll.Checked = _autoScroll
        frm.TopMost = _frmMain.TopMost

        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' Redisplay items
            Me.lvLog.CaptureItems = Me.LogCaptureMask
            Me.lvLog.DisplayItems = Me.LogDisplayMask
            Me.lvLog.ReAddItems()
        End If
    End Sub

    Private Sub _AsyncDownload_GotInformations(ByRef result As cAsyncProcInfoDownload.InternetProcessInfo) Handles _AsyncDownload.GotInformations
        _asyncInfoRes = result
        _asyncDownloadDone = True
    End Sub

    Private Sub lstHistoryCat_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstHistoryCat.ItemCheck
        If e.NewValue = CheckState.Checked Then
            _historyGraphNumber += 1
            Dim _g As New Graph2
            _g.Dock = DockStyle.Top
            _g.Height = CInt((Me.containerHistory.Panel2.Height - _historyGraphNumber) / _historyGraphNumber)
            _g.Visible = True
            _g.ColorGrid = Color.DarkGreen
            _g.BackColor = Color.Black
            _g.Name = lstHistoryCat.Items.Item(e.Index).ToString
            _g.EnableGraph = True
            _g.Fixedheight = (InStr(_g.Name, "CpuUsage") > 0)
            _g.ShowSecondGraph = False
            If InStr(_g.Name, "Cpu") > 0 Then
                _g.Color = Color.LimeGreen
                _g.Color2 = Color.Green
            ElseIf InStr(_g.Name, "Transfer") + InStr(_g.Name, "Operation") + InStr(_g.Name, "Delta") > 0 Then
                _g.Color = Color.Red
                _g.Color2 = Color.Maroon
            Else
                _g.Color = Color.Yellow
                _g.Color2 = Color.Olive
            End If
            Me.containerHistory.Panel2.Controls.Add(_g)
            Dim _p As New PictureBox
            _p.BackColor = Color.Transparent
            _p.Height = 1
            _p.Dock = DockStyle.Top
            _p.Name = "_" & lstHistoryCat.Items.Item(e.Index).ToString

            ' Now we add all available values to the graph
            For Each _val As Double In curProc.GetHistory(_g.Name)
                _g.AddValue(_val)
            Next
            _g.Refresh()

            Me.containerHistory.Panel2.Controls.Add(_p)
        Else
            _historyGraphNumber -= 1
            Me.containerHistory.Panel2.Controls.RemoveByKey(lstHistoryCat.Items.Item(e.Index).ToString)
            Me.containerHistory.Panel2.Controls.RemoveByKey("_" & lstHistoryCat.Items.Item(e.Index).ToString)
        End If

        Me.containerHistory.Panel1Collapsed = (_historyGraphNumber > 0)

        ' Recalculate heights
        For Each ct As Control In Me.containerHistory.Panel2.Controls
            If TypeOf ct Is Graph2 Then
                ct.Height = CInt((Me.containerHistory.Panel2.Height - _historyGraphNumber) / _historyGraphNumber)
                CType(ct, Graph2).TopText = ct.Name
            End If
        Next
    End Sub

    Private Sub containerHistory_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles containerHistory.Resize
        ' Recalculate heights
        For Each ct As Control In Me.containerHistory.Panel2.Controls
            If TypeOf ct Is Graph2 Then
                ct.Height = CInt((Me.containerHistory.Panel2.Height - 2 * _historyGraphNumber) / _historyGraphNumber)
            End If
        Next
    End Sub

    Private Sub curProc_Refreshed() Handles curProc.HasMerged
        ' curProc has been merged, so we have to add a value to the different
        ' graphs in containerHistory
        For Each ct As Control In Me.containerHistory.Panel2.Controls
            If TypeOf ct Is Graph2 Then
                Dim _tempG As Graph2 = CType(ct, Graph2)
                _tempG.AddValue(curProc.GetInformationNumerical(ct.Name))
                _tempG.Refresh()
            End If
        Next
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call Me.tabProcess_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cmdKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKill.Click
        If WarnDangerousAction("Are you sure you want to kill this process ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        Call curProc.Kill()
    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        If WarnDangerousAction("Are you sure you want to suspend this process ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        Call curProc.SuspendProcess()
    End Sub

    Private Sub cmdResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResume.Click
        Call curProc.ResumeProcess()
    End Sub

    Private Sub cmdSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSet.Click
        Select Case cbPriority.Text
            Case "Idle"
                Call curProc.SetPriority(ProcessPriorityClass.Idle)
            Case "BelowNormal"
                Call curProc.SetPriority(ProcessPriorityClass.BelowNormal)
            Case "Normal"
                Call curProc.SetPriority(ProcessPriorityClass.Normal)
            Case "AboveNormal"
                Call curProc.SetPriority(ProcessPriorityClass.AboveNormal)
            Case "High"
                Call curProc.SetPriority(ProcessPriorityClass.High)
            Case "RealTime"
                Call curProc.SetPriority(ProcessPriorityClass.RealTime)
        End Select
    End Sub

    Private Sub cmdAffinity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAffinity.Click
        Dim c(0) As cProcess
        c(0) = curProc
        Dim frm As New frmProcessAffinity(c)
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    ' When we've finished to get all non fixed infos
    Private Sub asyncAllNonFixedInfos_HasGotAllNonFixedInfos(ByVal Success As Boolean, ByRef newInfos As Native.Api.NativeStructs.SystemProcessInformation, ByVal msg As String) Handles asyncAllNonFixedInfos.HasGotAllNonFixedInfos
        If Success Then
            curProc.Merge(newInfos)
        Else
            ' ERROR HERE
        End If
    End Sub

    ' Connection
    Public Sub Connect()
        ' Connect providers
        'theConnection.CopyFromInstance(Program.Connection)
        Try
            theConnection = Program.Connection
            Me.lvThreads.ConnectionObj = theConnection
            Me.lvModules.ConnectionObj = theConnection
            Me.lvHandles.ConnectionObj = theConnection
            Me.lvProcMem.ConnectionObj = theConnection
            Me.lvLog.ConnectionObj = theConnection
            Me.lvPrivileges.ConnectionObj = theConnection
            Me.lvProcEnv.ConnectionObj = theConnection
            Me.lvProcServices.ConnectionObj = theConnection
            Me.lvWindows.ConnectionObj = theConnection
            Me.lvHeaps.ConnectionObj = theConnection
            Me.lvProcNetwork.ConnectionObj = theConnection
            theConnection.Connect()
            Me.timerProcPerf.Interval = CInt(My.Settings.ProcessInterval * Program.Connection.RefreshmentCoefficient)
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to connect")
        End Try
    End Sub

    Private Sub theConnection_Connected() Handles theConnection.Connected
        '
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        '
    End Sub

    Private Sub lvProcServices_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcServices.MouseDoubleClick
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            Dim frm As New frmServiceInfo
            frm.SetService(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub lvProcServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcServices.MouseUp
        Me.MenuItemServOpenDir.Enabled = _local
        Me.MenuItemServFileProp.Enabled = _local
        Me.MenuItemServFileDetails.Enabled = _local

        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvProcServices.SelectedItems IsNot Nothing _
                    AndAlso Me.lvProcServices.SelectedItems.Count > 0)

            If lvProcServices.SelectedItems.Count = 1 Then
                Dim cSe As cService = Me.lvProcServices.GetSelectedItem
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
                MenuItem17.Enabled = True
            ElseIf lvProcServices.SelectedItems.Count > 1 Then
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
                MenuItem17.Enabled = True
            ElseIf lvProcServices.SelectedItems.Count = 0 Then
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
                MenuItem17.Enabled = False
            End If

            Me.MenuItemServFileDetails.Enabled = selectionIsNotNothing AndAlso _local AndAlso Me.lvProcServices.SelectedItems.Count = 1
            Me.MenuItemServFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServSearch.Enabled = selectionIsNotNothing
            Me.MenuItemServDepe.Enabled = selectionIsNotNothing
            Me.MenuItemServSelService.Enabled = selectionIsNotNothing
            Me.MenuItemServReanalize.Enabled = selectionIsNotNothing
            Me.MenuItemCopyService.Enabled = selectionIsNotNothing
            Me.MenuItemServDetails.Enabled = selectionIsNotNothing
            Me.MenuItemServDelete.Enabled = selectionIsNotNothing AndAlso _notWMI

            Me.mnuService.Show(Me.lvProcServices, e.Location)
        End If
    End Sub

    Private Sub cmdInspectExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInspectExe.Click
        If System.IO.File.Exists(Me.curProc.Infos.Path) Then
            Dim _depForm As New frmDepViewerMain
            With _depForm
                .OpenReferences(Me.curProc.Infos.Path)
                .HideOpenMenu()
                .TopMost = _frmMain.TopMost
                .Show()
            End With
        End If
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRefresh.Click
        Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemCopyBig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyBig.Click
        My.Computer.Clipboard.SetImage(Me.pctBigIcon.Image)
    End Sub

    Private Sub MenuItemCopySmall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopySmall.Click
        My.Computer.Clipboard.SetImage(Me.pctSmallIcon.Image)
    End Sub

    Private Sub pctBigIcon_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctBigIcon.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.menuCopyPctbig.Show(Me.pctBigIcon, e.Location)
        End If
    End Sub

    Private Sub pctSmallIcon_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctSmallIcon.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.menuCopyPctSmall.Show(Me.pctSmallIcon, e.Location)
        End If
    End Sub

    Private Sub menuViewMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuViewMemory.Click

        If Me.lvProcString.SelectedIndices.Count = 0 Then Exit Sub

        Dim add As IntPtr = CType(Me.lvProcString.Items(Me.lvProcString.SelectedIndices(0)).Tag, IntPtr)
        For Each reg As cMemRegion In Me.lvProcMem.GetAllItems

            If reg.Infos.BaseAddress.IsLowerOrEqualThan(add) AndAlso add.IsLowerOrEqualThan(reg.Infos.BaseAddress.Increment(reg.Infos.RegionSize)) Then
                Dim frm As New frmHexEditor
                Dim regio As New MemoryHexEditor.MemoryRegion(reg.Infos.BaseAddress, reg.Infos.RegionSize)
                frm.SetPidAndRegion(curProc.Infos.ProcessId, regio)
                frm._hex.NavigateToOffset(CLng((add.ToInt64 - regio.BeginningAddress.ToInt64) / 16))
                frm.TopMost = _frmMain.TopMost
                frm.Show()
                Exit For
            End If
        Next

    End Sub

    Private Sub lvProcString_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcString.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.menuViewMemory.Enabled = Me.lvProcString.SelectedIndices.Count > 0
            Me.mnuString.Show(Me.lvProcString, e.Location)
        End If
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCloseTCP.Click
        If WarnDangerousAction("Are you sure you want to close these connections ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
            If it.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                it.CloseTCP()
            End If
        Next
    End Sub

    Private Sub MenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemColumnsNetwork.Click
        Me.lvProcNetwork.ChooseColumns()
    End Sub

    Private Sub lvHandles_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHandles.MouseDoubleClick
        For Each it As cHandle In Me.lvHandles.GetSelectedItems
            Dim frm As New frmHandleInfo
            frm.SetHandle(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub lvHandles_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHandles.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvHandles.SelectedItems IsNot Nothing _
                AndAlso Me.lvHandles.SelectedItems.Count > 0)

            Me.MenuItemCloseHandle.Enabled = selectionIsNotNothing
            Me.MenuItemCopyHandle.Enabled = selectionIsNotNothing

            Me.mnuHandle.Show(Me.lvHandles, e.Location)
        End If

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
            Me.MenuItemUnloadModule.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemCopyModule.Enabled = selectionIsNotNothing
            Me.MenuItemViewModuleMemory.Enabled = selectionIsNotNothing AndAlso _local AndAlso Me.lvProcMem.Items.Count > 0
            Me.mnuModule.Show(Me.lvModules, e.Location)
        End If
    End Sub

    Private Sub lvPrivileges_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvPrivileges.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvPrivileges.SelectedItems IsNot Nothing _
                            AndAlso Me.lvPrivileges.SelectedItems.Count > 0)
            Me.MenuItemPriDisable.Enabled = selectionIsNotNothing
            Me.MenuItemPriEnable.Enabled = selectionIsNotNothing
            Me.MenuItemPriRemove.Enabled = selectionIsNotNothing
            Me.MenuItemCopyPrivilege.Enabled = selectionIsNotNothing
            Me.mnuPrivileges.Show(Me.lvPrivileges, e.Location)
        End If
    End Sub

    Private Sub lvProcMem_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcMem.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvProcMem.SelectedItems IsNot Nothing _
                AndAlso Me.lvProcMem.SelectedItems.Count > 0)
            Me.MenuItemViewMemory.Enabled = selectionIsNotNothing And _local
            Me.MenuItemPEBAddress.Enabled = selectionIsNotNothing And _local
            Me.MenuItemCopyMemory.Enabled = selectionIsNotNothing
            Me.MenuItemMemoryDump.Enabled = selectionIsNotNothing And _local
            Me.MenuItemMemoryChangeProtection.Enabled = selectionIsNotNothing And _notWMI

            Dim memReg As cMemRegion = Me.lvProcMem.GetSelectedItem
            Dim b As Boolean = selectionIsNotNothing AndAlso _notWMI AndAlso _
                (memReg IsNot Nothing) AndAlso _
                (memReg.Infos.State = Native.Api.NativeEnums.MemoryState.Commit And _
                 memReg.Infos.Type = Native.Api.NativeEnums.MemoryType.Private)
            Me.MenuItemMemoryDecommit.Enabled = b
            Me.MenuItemMemoryRelease.Enabled = b

            Me.mnuProcMem.Show(Me.lvProcMem, e.Location)
        End If
    End Sub

    Private Sub lvProcNetwork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcNetwork.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvProcNetwork.SelectedItems IsNot Nothing _
                AndAlso Me.lvProcNetwork.SelectedItems.Count > 0)

            Dim enable As Boolean = False
            For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
                If it.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                    If it.Infos.State <> Native.Api.Enums.MibTcpState.Listening AndAlso it.Infos.State <> Native.Api.Enums.MibTcpState.TimeWait AndAlso it.Infos.State <> Native.Api.Enums.MibTcpState.CloseWait Then
                        enable = True
                        Exit For
                    End If
                End If
            Next
            Me.menuCloseTCP.Enabled = enable
            Me.MenuItemCopyNetwork.Enabled = selectionIsNotNothing

            Dim bTools As Boolean = True
            If Me.lvProcNetwork.SelectedItems.Count = 1 Then
                bTools = (Me.lvProcNetwork.GetSelectedItem.Infos.Remote IsNot Nothing)
            End If
            Me.MenuItemNetworkTools.Enabled = selectionIsNotNothing AndAlso bTools

            Me.mnuNetwork.Show(Me.lvProcNetwork, e.Location)
        End If
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
            Me.MenuItemThNorm.Checked = (p = ThreadPriorityLevel.Normal)
            Me.MenuItemThANorm.Checked = (p = ThreadPriorityLevel.AboveNormal)
            Me.MenuItemThHighest.Checked = (p = ThreadPriorityLevel.Highest)
            Me.MenuItemThTimeCr.Checked = (p = ThreadPriorityLevel.TimeCritical)


            Dim selectionIsNotNothing As Boolean = (Me.lvThreads.SelectedItems IsNot Nothing _
                            AndAlso Me.lvThreads.SelectedItems.Count > 0)

            Me.MenuItemThAffinity.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThSuspend.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThTerm.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemThResu.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItem8.Enabled = selectionIsNotNothing AndAlso _notWMI
            Me.MenuItemCopyThread.Enabled = selectionIsNotNothing

            Me.mnuThread.Show(Me.lvThreads, e.Location)
        End If
    End Sub

    Private Sub lvWindows_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvWindows.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.lvWindows.SelectedItems.Count = 1 Then
                Dim wd As cWindow = Me.lvWindows.GetSelectedItem
                If (wd IsNot Nothing) Then
                    Me.MenuItemWEna.Checked = wd.Infos.Enabled
                    Me.MenuItemWDisa.Checked = (wd.Infos.Enabled = False)
                Else
                    Me.MenuItemWEna.Checked = False
                    Me.MenuItemWDisa.Checked = False
                End If
            Else
                Me.MenuItemWEna.Checked = False
                Me.MenuItemWDisa.Checked = False
            End If
            Me.mnuWindow.Show(Me.lvWindows, e.Location)
        End If
    End Sub

    Private Sub MenuItemCloseHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCloseHandle.Click
        If WarnDangerousAction("Are you sure you want to close these handles ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each ch As cHandle In Me.lvHandles.GetSelectedItems
            ch.CloseHandle()
        Next
    End Sub

    Private Sub MenuItemShowUnnamedHandles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemShowUnnamedHandles.Click
        MenuItemShowUnnamedHandles.Checked = Not (MenuItemShowUnnamedHandles.Checked)
    End Sub

    Private Sub MenuItemChooseColumnsHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemChooseColumnsHandle.Click
        Me.lvHandles.ChooseColumns()
    End Sub

    Private Sub MenuItemViewMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemViewMemory.Click
        Call lvProcMem_DoubleClick(Nothing, Nothing)
    End Sub

    Private Sub MenuItemPEBAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPEBAddress.Click
        Dim peb As IntPtr = curProc.Infos.PebAddress
        For Each reg As cMemRegion In Me.lvProcMem.GetAllItems
            If reg.Infos.BaseAddress.IsLowerOrEqualThan(peb) AndAlso peb.IsLowerOrEqualThan(reg.Infos.BaseAddress.Increment(reg.Infos.RegionSize)) Then
                Dim frm As New frmHexEditor
                Dim regio As New MemoryHexEditor.MemoryRegion(reg.Infos.BaseAddress, reg.Infos.RegionSize)
                frm.SetPidAndRegion(curProc.Infos.ProcessId, regio)
                frm._hex.NavigateToOffset(peb.ToInt64)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub MenuItemColumnsMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemColumnsMemory.Click
        Me.lvProcMem.ChooseColumns()
    End Sub

    Private Sub MenuItemPriEnable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPriEnable.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(Native.Api.NativeEnums.SePrivilegeAttributes.Enabled)
        Next
    End Sub

    Private Sub MenuItemPriDisable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPriDisable.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(Native.Api.NativeEnums.SePrivilegeAttributes.Disabled)
        Next
    End Sub

    Private Sub MenuItemPriRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPriRemove.Click
        If WarnDangerousAction("Are you sure you want to remove this privilege ?" & vbNewLine & "This is a permanent action for all process lifetime.", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(Native.Api.NativeEnums.SePrivilegeAttributes.Removed)
        Next
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
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
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

    Private Sub MenuItemViewModuleMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemViewModuleMemory.Click

        If Me.lvProcMem.Items.Count = 0 Then
            Call ShowRegions()
        End If

        For Each it As cModule In Me.lvModules.GetSelectedItems
            Dim add As IntPtr = it.Infos.BaseAddress

            For Each reg As cMemRegion In Me.lvProcMem.GetAllItems

                If reg.Infos.BaseAddress.IsLowerOrEqualThan(add) AndAlso add.IsLowerThan(reg.Infos.BaseAddress.Increment(reg.Infos.RegionSize)) Then
                    Dim frm As New frmHexEditor
                    Dim regio As New MemoryHexEditor.MemoryRegion(reg.Infos.BaseAddress, reg.Infos.RegionSize)
                    frm.SetPidAndRegion(curProc.Infos.ProcessId, regio)
                    frm._hex.NavigateToOffset(CLng((add.ToInt64 - regio.BeginningAddress.ToInt64) / 16) - 1)
                    frm.TopMost = _frmMain.TopMost
                    frm.Show()
                    Exit For
                End If
            Next

        Next

    End Sub

    Private Sub MenuItemUnloadModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemUnloadModule.Click
        If WarnDangerousAction("Are you sure you want to unload these modules ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cModule In Me.lvModules.GetSelectedItems
            it.UnloadModule()
        Next
    End Sub

    Private Sub MenuItemColumnsModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemColumnsModule.Click
        Me.lvModules.ChooseColumns()
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDetails.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            Dim frm As New frmServiceInfo
            frm.SetService(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemServSelService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSelService.Click
        ' Select services associated to selected process
        If Me.lvProcServices.SelectedItems.Count > 0 Then _frmMain.lvServices.SelectedItems.Clear()

        If _frmMain.lvServices.Items.Count = 0 Then
            ' Refresh list
            Call _frmMain.refreshServiceList()
        End If

        For Each it As cService In Me.lvProcServices.GetSelectedItems
            Dim it2 As ListViewItem
            For Each it2 In _frmMain.lvServices.Items
                Dim cp As cService = _frmMain.lvServices.GetItemByKey(it2.Name)
                If cp IsNot Nothing AndAlso cp.Infos.Name = it.Infos.Name Then
                    it2.Selected = True
                    it2.EnsureVisible()
                End If
            Next
        Next
        _frmMain.Ribbon.ActiveTab = _frmMain.ServiceTab
        Call _frmMain.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServFileProp.Click
        Dim s As String = vbNullString
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            s = it.GetInformation("ImagePath")
            If s <> NO_INFO_RETRIEVED Then
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
                        .TopMost = _frmMain.TopMost
                        .ShowDialog()
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
        For Each it As cService In Me.lvProcServices.GetSelectedItems
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
                        .TopMost = _frmMain.TopMost
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

    Private Sub MenuItemServFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServFileDetails.Click
        If Me.lvProcServices.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvProcServices.GetSelectedItem.GetInformation("ImagePath")
            If IO.File.Exists(s) = False Then
                s = cFile.IntelligentPathRetrieving2(s)
            End If
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub MenuItemServSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSearch.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcServices.SelectedItems
            Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
    End Sub

    Private Sub MenuItemServPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServPause.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            If it.Infos.State = Native.Api.NativeEnums.ServiceState.Running Then
                it.PauseService()
            Else
                it.ResumeService()
            End If
        Next
    End Sub

    Private Sub MenuItemServStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStop.Click
        If WarnDangerousAction("Are you sure you want to stop these services ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.StopService()
        Next
    End Sub

    Private Sub MenuItemServStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStart.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.StartService()
        Next
    End Sub

    Private Sub MenuItemServAutoStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServAutoStart.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.AutoStart)
        Next
    End Sub

    Private Sub MenuItemServOnDemand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServOnDemand.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.DemandStart)
        Next
    End Sub

    Private Sub MenuItemServDisabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDisabled.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.StartDisabled)
        Next
    End Sub

    Private Sub MenuItemServReanalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServReanalize.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.Refresh()
        Next
    End Sub

    Private Sub MenuItemServColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServColumns.Click
        Me.lvProcServices.ChooseColumns()
    End Sub

    Private Sub MenuItemThTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThTerm.Click
        If WarnDangerousAction("Are you sure you want to terminate these threads ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadTerminate()
        Next
    End Sub

    Private Sub MenuItemThSuspend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThSuspend.Click
        If WarnDangerousAction("Are you sure you want to suspend these threads ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadSuspend()
        Next
    End Sub

    Private Sub MenuItemThResu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemThResu.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadResume()
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

    Private Sub MenuItemWShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWShow.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Show()
        Next
    End Sub

    Private Sub MenuItemWShowUn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWShowUn.Click
        MenuItemWShowUn.Checked = Not (MenuItemWShowUn.Checked)
    End Sub

    Private Sub MenuItemWHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWHide.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Hide()
        Next
    End Sub

    Private Sub MenuItemWClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWClose.Click
        If WarnDangerousAction("Are you sure you want to close these windows ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Close()
        Next
    End Sub

    Private Sub MenuItemWFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWFront.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.BringToFront(True)
        Next
    End Sub

    Private Sub MenuItemWNotFront_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWNotFront.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.BringToFront(False)
        Next
    End Sub

    Private Sub MenuItemWActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWActive.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.SetAsActiveWindow()
        Next
    End Sub

    Private Sub MenuItemWForeground_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWForeground.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.SetAsForegroundWindow()
        Next
    End Sub

    Private Sub MenuItemWMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWMin.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Minimize()
        Next
    End Sub

    Private Sub MenuItemWMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWMax.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Maximize()
        Next
    End Sub

    Private Sub MenuItemWPosSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWPosSize.Click
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

    Private Sub MenuItemWEna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWEna.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Enabled = True
        Next
    End Sub

    Private Sub MenuItemWDisa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWDisa.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Enabled = False
        Next
    End Sub

    Private Sub MenuItemWColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWColumns.Click
        Me.lvWindows.ChooseColumns()
    End Sub

    Private Sub MenuItemLogGoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemLogGoto.Click
        ' Select item in associated listview
        With Me.lvLog.GetSelectedItem.Infos
            Select Case .TypeMask
                Case asyncCallbackLogEnumerate.LogItemType.HandleItem
                    For Each it2 As ListViewItem In Me.lvHandles.Items
                        Dim tmp As cHandle = Me.lvHandles.GetItemByKey(it2.Name)
                        If tmp IsNot Nothing AndAlso tmp.Infos.Handle.ToString = .DefKey Then
                            it2.Selected = True
                            it2.EnsureVisible()
                            Me.tabProcess.SelectedTab = TabPageHandles
                            Exit For
                        End If
                    Next
                Case asyncCallbackLogEnumerate.LogItemType.MemoryItem
                    For Each it2 As ListViewItem In Me.lvProcMem.Items
                        Dim tmp As cMemRegion = Me.lvProcMem.GetItemByKey(it2.Name)
                        If tmp IsNot Nothing AndAlso tmp.Infos.BaseAddress.ToString = .DefKey Then
                            it2.Selected = True
                            it2.EnsureVisible()
                            Me.tabProcess.SelectedTab = TabPageMemory
                            Exit For
                        End If
                    Next
                Case asyncCallbackLogEnumerate.LogItemType.ModuleItem
                    For Each it2 As ListViewItem In Me.lvModules.Items
                        Dim tmp As cModule = Me.lvModules.GetItemByKey(it2.Name)
                        If tmp IsNot Nothing AndAlso tmp.Infos.BaseAddress.ToString = .DefKey Then
                            it2.Selected = True
                            it2.EnsureVisible()
                            Me.tabProcess.SelectedTab = TabPageModules
                            Exit For
                        End If
                    Next
                Case asyncCallbackLogEnumerate.LogItemType.NetworkItem
                    ' TODO
                Case asyncCallbackLogEnumerate.LogItemType.ServiceItem
                    For Each it2 As ListViewItem In Me.lvProcServices.Items
                        Dim tmp As cService = Me.lvProcServices.GetItemByKey(it2.Name)
                        If tmp IsNot Nothing AndAlso tmp.Infos.Name = .DefKey Then
                            it2.Selected = True
                            it2.EnsureVisible()
                            Me.tabProcess.SelectedTab = TabPageMemory
                            Exit For
                        End If
                    Next
                Case asyncCallbackLogEnumerate.LogItemType.ThreadItem
                    For Each it2 As ListViewItem In Me.lvThreads.Items
                        Dim tmp As cThread = Me.lvThreads.GetItemByKey(it2.Name)
                        If tmp IsNot Nothing AndAlso tmp.Infos.Id.ToString = .DefKey Then
                            it2.Selected = True
                            it2.EnsureVisible()
                            Me.tabProcess.SelectedTab = TabPageThreads
                            Exit For
                        End If
                    Next
                Case asyncCallbackLogEnumerate.LogItemType.WindowItem
                    For Each it2 As ListViewItem In Me.lvWindows.Items
                        Dim tmp As cWindow = Me.lvWindows.GetItemByKey(it2.Name)
                        If tmp IsNot Nothing AndAlso tmp.Infos.Handle.ToString = .DefKey Then
                            it2.Selected = True
                            it2.EnsureVisible()
                            Me.tabProcess.SelectedTab = TabPageWindows
                            Exit For
                        End If
                    Next
            End Select
        End With
    End Sub

    Private Sub lvLog_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvLog.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.MenuItemLogGoto.Enabled = (Me.lvLog.SelectedItems.Count = 1)
            Me.MenuItemCopyLog.Enabled = (Me.lvLog.SelectedItems.Count > 0)
            Me.mnuLog.Show(Me.lvLog, e.Location)
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

    Private Sub MenuItemCopyMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cMemRegion In Me.lvProcMem.GetSelectedItems
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
        For Each it As cService In Me.lvProcServices.GetSelectedItems
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

    Private Sub MenuItemCopyPrivilege_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
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
        For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cLogItem In Me.lvLog.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyEnvVariable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cEnvVariable In Me.lvProcEnv.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyHeaps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cHeap In Me.lvHeaps.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyString_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        If info = "Position" Then
            For Each it As ListViewItem In Me.lvProcString.SelectedItemsVMode
                toCopy &= it.Text & vbNewLine
            Next
        ElseIf info = "String" Then
            For Each it As ListViewItem In Me.lvProcString.SelectedItemsVMode
                toCopy &= it.SubItems(1).Text & vbNewLine
            Next
        End If
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

#End Region

    Private Sub lvProcEnv_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcEnv.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.MenuItemCopyEnvVariable.Enabled = Me.lvProcEnv.SelectedItems.Count > 0
            Me.mnuEnv.Show(Me.lvProcEnv, e.Location)
        End If
    End Sub

    Private Sub MenuItemServDepe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDepe.Click
        If Me.lvProcServices.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvProcServices.GetSelectedItem.GetInformation("ImagePath")
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

    Private Sub MenuItemMemoryDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMemoryDump.Click
        '
    End Sub

    Private Sub MenuItemMemoryRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMemoryRelease.Click
        If WarnDangerousAction("Are you sure you want to release these memory regions ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cMemRegion In Me.lvProcMem.GetSelectedItems
            it.Release()
        Next
    End Sub

    Private Sub MenuItemMemoryDecommit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMemoryDecommit.Click
        If WarnDangerousAction("Are you sure you want to release these memory regions ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cMemRegion In Me.lvProcMem.GetSelectedItems
            it.Decommit()
        Next
    End Sub

    Private Sub MenuItemMemoryChangeProtection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMemoryChangeProtection.Click
        If Me.lvProcMem.GetSelectedItems IsNot Nothing AndAlso Me.lvProcMem.GetSelectedItems.Count > 0 Then
            Dim frm As New frmChangeMemoryProtection
            If Me.lvProcMem.GetSelectedItems.Count = 1 Then
                ' One mem region selected -> check protection type by default
                ' with current protection type (in form)
                frm.ProtectionType = Me.lvProcMem.GetSelectedItem.Infos.Protection
            End If
            frm.TopMost = _frmMain.TopMost
            frm.ShowDialog()
            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                For Each it As cMemRegion In Me.lvProcMem.GetSelectedItems
                    it.ChangeProtectionType(frm.NewProtectionType)
                Next
            End If
        End If
    End Sub

    Private Sub MenuItemWFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWFlash.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Flash()
        Next
    End Sub

    Private Sub MenuItemWStopFlash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWStopFlash.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.StopFlashing()
        Next
    End Sub

    Private Sub MenuItemWOpacity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWOpacity.Click
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

    Private Sub MenuItemWCaption_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemWCaption.Click
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

    Private Sub lvThreads_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvThreads.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            If WarnDangerousAction("Are you sure you want to terminate these threads ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
            For Each it As cThread In Me.lvThreads.GetSelectedItems
                it.ThreadTerminate()
            Next
        End If
    End Sub

    ' Handler for process termination event
    Public Sub ProcessHasTerminatedHandler(ByVal ntStatus As UInt32)

        ' Defined fixed form caption
        fixedFormCaption = curProc.Infos.Name & " (" & curProc.Infos.ProcessId.ToString _
                    & ") exited with status 0x" & ntStatus.ToString("x") & _
                    " -- " & Native.Api.Win32.GetNtStatusMessageAsString(ntStatus)

    End Sub


#Region "Find panel"

    Private Sub cmdHideFindPanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHideFindPanel.Click
        My.Settings.ShowFindWindowDetailedForm = False
        If My.Settings.FirstTimeShowFindWindowWasClosed Then
            My.Settings.FirstTimeShowFindWindowWasClosed = False
            Misc.ShowMsg("Search panel", "You have closed the search panel.", "Press Ctrl+F on a listview to open it again.", MessageBoxButtons.OK, TaskDialogIcon.Information)
            My.Settings.Save()
        End If
        Call hideFindPanel()
    End Sub

    Private Sub lvProcMem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcMem.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvProcServices_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcServices.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            If _notWMI Then
                Call MenuItemServDelete_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub lvProcNetwork_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcNetwork.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvProcEnv_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcEnv.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvLog_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvLog.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvHeaps_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvHeaps.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        ElseIf e.KeyCode = Keys.Enter Then
            Call Me.lvHeaps_MouseDoubleClick(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvWindows_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvWindows.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvPrivileges_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvPrivileges.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvModules_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvModules.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        End If
    End Sub

    Private Sub lvHandles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvHandles.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            If Me.SplitContainer.Panel2Collapsed Then
                Call showFindPanel()
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            If WarnDangerousAction("Are you sure you want to close these handles ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
            For Each it As cHandle In Me.lvHandles.GetSelectedItems
                it.CloseHandle()
            Next
        ElseIf e.KeyCode = Keys.Enter And Me.lvHandles.SelectedItems.Count > 0 Then
            For Each it As cHandle In Me.lvHandles.GetSelectedItems
                Dim frm As New frmHandleInfo
                frm.SetHandle(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        End If
    End Sub

    ' Update 'Find panel' depending on the selected tab
    Private Sub updateFindPanel()
        Select Case Me.tabProcess.SelectedTab.Text
            Case "Token"
                Me.txtSearch.Enabled = True
                Me.lblResCount.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                listViewForSearch = Me.lvPrivileges
            Case "Modules"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvModules
            Case "Threads"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvThreads
            Case "Windows"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvWindows
            Case "Handles"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvHandles
            Case "Memory"
                Me.lblSearchItemCaption.Enabled = True
                Me.txtSearch.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvProcMem
            Case "Environment"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvProcEnv
            Case "Network"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvProcNetwork
            Case "Services"
                Me.txtSearch.Enabled = True
                Me.lblSearchItemCaption.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvProcServices
            Case "Log"
                Me.lblSearchItemCaption.Enabled = True
                Me.txtSearch.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvLog
            Case "Heaps"
                Me.lblSearchItemCaption.Enabled = True
                Me.txtSearch.Enabled = True
                Me.lblResCount.Enabled = True
                listViewForSearch = Me.lvHeaps
            Case Else
                Me.lblSearchItemCaption.Enabled = False
                Me.txtSearch.Enabled = False
                Me.lblResCount.Enabled = False
        End Select
    End Sub
    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim it As ListViewItem
        Dim comp As String = Me.txtSearch.Text.ToLowerInvariant
        For Each it In Me.listViewForSearch.Items
            Dim add As Boolean = False
            For Each subit As ListViewItem.ListViewSubItem In it.SubItems
                Dim ss As String = subit.Text
                If subit IsNot Nothing Then
                    If InStr(ss.ToLowerInvariant, comp, CompareMethod.Binary) > 0 Then
                        add = True
                        Exit For
                    End If
                End If
            Next
            If add = False Then
                it.Group = listViewForSearch.Groups(0)
            Else
                it.Group = listViewForSearch.Groups(1)
            End If
        Next
        Me.lblResCount.Text = CStr(listViewForSearch.Groups(1).Items.Count) & " result(s)"
    End Sub
    Private Sub lblResCount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblResCount.Click
        If Me.listViewForSearch.Groups(1).Items.Count > 0 Then
            Me.listViewForSearch.Focus()
            Me.listViewForSearch.EnsureVisible(Me.listViewForSearch.Groups(1).Items(0).Index)
            Me.listViewForSearch.SelectedItems.Clear()
            Me.listViewForSearch.Groups(1).Items(0).Selected = True
        End If
    End Sub
    Private Sub showFindPanel()
        Me.SplitContainer.Panel2Collapsed = False
        My.Settings.ShowFindWindowDetailedForm = True
        My.Settings.Save()
        Me.txtSearch.Focus()

        ' Add groups to listviews
        Me.lvThreads.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                    {New ListViewGroup("0", "Items"), _
                     New ListViewGroup("1", "Search results")})
        Me.lvProcServices.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvPrivileges.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvLog.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvProcEnv.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvProcMem.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvWindows.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvHandles.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvModules.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvProcNetwork.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
        Me.lvHeaps.Groups.AddRange(New System.Windows.Forms.ListViewGroup() _
                   {New ListViewGroup("0", "Items"), _
                    New ListViewGroup("1", "Search results")})
    End Sub
    Private Sub hideFindPanel()
        Me.SplitContainer.Panel2Collapsed = True
        ' Remove all groups
        Me.lvPrivileges.Groups.Clear()
        Me.lvProcServices.Groups.Clear()
        Me.lvProcNetwork.Groups.Clear()
        Me.lvModules.Groups.Clear()
        Me.lvThreads.Groups.Clear()
        Me.lvLog.Groups.Clear()
        Me.lvProcMem.Groups.Clear()
        Me.lvWindows.Groups.Clear()
        Me.lvHandles.Groups.Clear()
        Me.lvProcEnv.Groups.Clear()
        Me.lvHeaps.Groups.Clear()
    End Sub

#End Region

    Private Sub MenuItemServDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDelete.Click
        If WarnDangerousAction("Are you sure you want to delete these services ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.DeleteService()
        Next
    End Sub

    Private Sub MenuItemHandleDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemHandleDetails.Click
        Call Me.lvHandles_MouseDoubleClick(Nothing, Nothing)
    End Sub

    Private Sub lvHeaps_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHeaps.MouseDoubleClick
        Dim heap As cHeap = lvHeaps.GetSelectedItem
        If heap IsNot Nothing Then
            Dim frm As New frmHeapBlocks(curProc.Infos.ProcessId, heap.Infos.BaseAddress)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        End If
    End Sub

    Private Sub lvHeaps_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHeaps.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.MenuItemCopyHeaps.Enabled = Me.lvHeaps.SelectedItems.Count > 0
            Me.mnuHeaps.Show(Me.lvHeaps, e.Location)
        End If
    End Sub

    Private Sub MenuItemColumnsHeap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemColumnsHeap.Click
        Me.lvHeaps.ChooseColumns()
    End Sub

    Private Sub cmdGoProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoProcess.Click
        ' Select parent process
        Dim _t As cProcess = cProcess.GetProcessById(curProc.Infos.ParentProcessId)
        If _t IsNot Nothing Then
            Dim frm As New frmProcessInfo
            frm.SetProcess(_t)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        End If
    End Sub

    Private Sub MenuItemNetworkPing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkPing.Click
        For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
            Dim frm As New frmNetworkTool(it, Native.Api.Enums.ToolType.Ping)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemNetworkRoute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkRoute.Click
        For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
            Dim frm As New frmNetworkTool(it, Native.Api.Enums.ToolType.TraceRoute)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemNetworkWhoIs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkWhoIs.Click
        For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
            Dim frm As New frmNetworkTool(it, Native.Api.Enums.ToolType.WhoIs)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub
End Class