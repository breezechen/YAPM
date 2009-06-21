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
Imports CoreFunc.asyncCallbackLogEnumerate

Public Class frmProcessInfo

    Private WithEvents asyncAllNonFixedInfos As asyncCallbackProcGetAllNonFixedInfos

    Private WithEvents curProc As cProcess
    Private Const NO_INFO_RETRIEVED As String = "N/A"
    Private m_SortingColumn As ColumnHeader
    Private WithEvents _AsyncDownload As cAsyncProcInfoDownload
    Private _asyncDlThread As Threading.Thread

    Private WithEvents theConnection As cConnection

    ' String search (in process image/memory) private attributes
    Private _stringSearchImmediateStop As Boolean   ' Set to true to stop listing of string in process
    Private __sRes() As String
    Private __lRes() As Integer
    Private cRW As cProcessMemRW

    Private NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Private DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)

    Private Const SIZE_FOR_STRING As Integer = 4

    Private _historyGraphNumber As Integer = 0
    Private _local As Boolean = True
    Private _notWMI As Boolean
    Private __con As Management.ConnectionOptions


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


            Case "Strings"
                If _local Then _
                Call getProcString(curProc)

            Case "General"
                Me.txtProcessPath.Text = curProc.Infos.Path
                Me.txtProcessId.Text = curProc.Infos.Pid.ToString
                Me.txtParentProcess.Text = curProc.Infos.ParentProcessId.ToString & " -- " & cProcess.GetProcessName(curProc.Infos.Pid)
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

        Dim mem As API.VM_COUNTERS_EX = curProc.Infos.MemoryInfos
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
            Dim pmc As API.VM_COUNTERS_EX = curProc.Infos.MemoryInfos
            Dim pid As Integer = curProc.Infos.Pid
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
            s = s & "\tab PID :\tab\tab\tab\tab " & CStr(curProc.Infos.Pid) & "\par"
            s = s & "\tab Start time :\tab\tab\tab " & New Date(curProc.Infos.StartTime).ToLongDateString & " -- " & New Date(curProc.Infos.StartTime).ToLongTimeString & "\par"
            s = s & "\tab Priority :\tab\tab\tab\tab " & curProc.Infos.Priority.ToString & "\par"
            s = s & "\tab User :\tab\tab\tab\tab " & curProc.Infos.UserName & "\par"
            Dim ts As Date = New Date(curProc.Infos.ProcessorTime)
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
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        ' Cool theme
        API.SetWindowTheme(Me.lvProcString.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvProcEnv.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvProcNetwork.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvProcMem.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvProcServices.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvPrivileges.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvModules.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvHandles.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvThreads.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvWindows.Handle, "explorer", Nothing)
        API.SetWindowTheme(Me.lvLog.Handle, "explorer", Nothing)

        ' Some tooltips
        _frmMain.SetToolTip(Me.cmdInfosToClipB, "Copy process informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style).")
        _frmMain.SetToolTip(Me.txtImageVersion, "Version of file")
        _frmMain.SetToolTip(Me.txtProcessPath, "Path of file")
        _frmMain.SetToolTip(Me.cmdShowFileDetails, "Show file details")
        _frmMain.SetToolTip(Me.cmdInspectExe, "Show dependencies")
        _frmMain.SetToolTip(Me.cmdShowFileProperties, "Open file property window")
        _frmMain.SetToolTip(Me.cmdOpenDirectory, "Open directory of file")
        _frmMain.SetToolTip(Me.txtParentProcess, "Parent process")
        _frmMain.SetToolTip(Me.txtProcessStarted, "Start time")
        _frmMain.SetToolTip(Me.txtProcessId, "Process ID")
        _frmMain.SetToolTip(Me.txtProcessUser, "Process user")
        _frmMain.SetToolTip(Me.txtCommandLine, "Command line which launched process")
        _frmMain.SetToolTip(Me.cmdGetOnlineInfos, "Retrieve online information (based on process name)")
        _frmMain.SetToolTip(Me.optProcStringImage, "Get strings from image file on disk")
        _frmMain.SetToolTip(Me.optProcStringMemory, "Get strings from memory")
        _frmMain.SetToolTip(Me.cmdProcStringSave, "Save list in a file")
        _frmMain.SetToolTip(Me.pgbString, "Progression. Click to stop.")
        _frmMain.SetToolTip(Me.txtSearchProcString, "Search a specific string")
        _frmMain.SetToolTip(Me.txtRunTime, "Total run time")
        _frmMain.SetToolTip(Me.cmdProcSearchL, "Previous result (F2 on listview also works)")
        _frmMain.SetToolTip(Me.cmdProcSearchR, "Next result (F3 on listview also works)")

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

        Select Case My.Settings.ProcSelectedTab
            Case "Token"
                Me.tabProcess.SelectedTab = Me.TabPage4
            Case "Modules"
                Me.tabProcess.SelectedTab = Me.TabPage10
            Case "Threads"
                Me.tabProcess.SelectedTab = Me.TabPage11
            Case "Windows"
                Me.tabProcess.SelectedTab = Me.TabPage12
            Case "Handles"
                Me.tabProcess.SelectedTab = Me.TabPage13
            Case "Memory"
                Me.tabProcess.SelectedTab = Me.TabPage5
            Case "Environment"
                Me.tabProcess.SelectedTab = Me.TabPage9
            Case "Network"
                Me.tabProcess.SelectedTab = Me.tabNetwork
            Case "Services"
                Me.tabProcess.SelectedTab = Me.TabPage7
            Case "Strings"
                Me.tabProcess.SelectedTab = Me.TabPageString
            Case "General"
                Me.tabProcess.SelectedTab = Me.TabPage1
            Case "Statistics"
                Me.tabProcess.SelectedTab = Me.TabPage2
            Case "Informations"
                Me.tabProcess.SelectedTab = Me.TabPage6
            Case "Performances"
                Me.tabProcess.SelectedTab = Me.TabPage3
            Case "Log"
                Me.tabProcess.SelectedTab = Me.TabPage14
            Case "History"
                Me.tabProcess.SelectedTab = Me.TabPage15
        End Select

        ' Refresh infos
        Me.graphCPU.ClearValue()
        Me.graphIO.ClearValue()
        Me.graphMemory.ClearValue()

        ' Icons
        If pctBigIcon.Image Is Nothing Then
            Try
                pctBigIcon.Image = GetIcon(curProc.Infos.Path, False).ToBitmap
                pctSmallIcon.Image = GetIcon(curProc.Infos.Path, True).ToBitmap
            Catch ex As Exception
                pctSmallIcon.Image = Me.imgProcess.Images("noicon")
                pctBigIcon.Image = Me.imgMain.Images("noicon32")
            End Try
        End If

        ' Show all items
        'Call ShowModules()
        'Call ShowThreads()
        'Call ShowWindows()
        'Call ShowRegions()
        'Call ShowNetwork()

        Call Connect()
        Call refreshProcessTab()

    End Sub

    ' Get process to monitor
    Public Sub SetProcess(ByRef process As cProcess)

        curProc = process
        asyncAllNonFixedInfos = New asyncCallbackProcGetAllNonFixedInfos(cProcess.Connection, curProc)

        Me.Text = curProc.Infos.Name & " (" & CStr(curProc.Infos.Pid) & ")"
        Me.cbPriority.Text = curProc.Infos.Priority.ToString

        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cProcess.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        Me.cmdAffinity.Enabled = _notWMI
        Me.cmdPause.Enabled = _notWMI
        Me.cmdResume.Enabled = _notWMI
        Me.lvModules.CatchErrors = Not (_local)
        Me.timerProcPerf.Enabled = _notWMI
        Me.lvPrivileges.Enabled = _notWMI
        Me.lvHandles.Enabled = _notWMI
        Me.lvLog.Enabled = _notWMI
        Me.lvProcEnv.Enabled = _notWMI
        Me.lvProcMem.Enabled = _notWMI
        Me.lvProcNetwork.Enabled = _notWMI
        Me.lvProcString.Enabled = _notWMI
        Me.lvWindows.Enabled = _notWMI
        Me.SplitContainerStrings.Enabled = _notWMI
        Me.SplitContainerLog.Enabled = _notWMI
        Me.cmdShowFileDetails.Enabled = _local
        Me.cmdInspectExe.Enabled = _local
        Me.cmdShowFileProperties.Enabled = _local
        Me.cmdOpenDirectory.Enabled = _local
        Me.MenuItemModuleFileDetails.Enabled = _notWMI
        Me.MenuItemUnloadModule.Enabled = _notWMI
        Me.MenuItemViewModuleMemory.Enabled = _local
        Me.MenuItemModuleDependencies.Enabled = _local

        Me.MenuItemModuleOpenDir.Enabled = _local
        Me.MenuItemServFileDetails.Enabled = _local
        Me.MenuItemServFileProp.Enabled = _local
        Me.MenuItemModuleFileProp.Enabled = _local
        Me.TabPageString.Enabled = _local

        Me.timerLog.Enabled = Me.timerLog.Enabled And _notWMI
        Me.timerProcPerf.Enabled = _local

        ' Verify file
        If _local Then
            Try
                Dim bVer As Boolean = Security.WinTrust.WinTrust.VerifyEmbeddedSignature(curProc.Infos.Path)
                If bVer Then
                    gpProcGeneralFile.Text = "Image file (successfully verified)"
                Else
                    gpProcGeneralFile.Text = "Image file (not verified)"
                End If
            Catch ex As Exception
                '
            End Try
        Else
            gpProcGeneralFile.Text = "Image file (no verification was made)"
        End If
    End Sub

    Private Sub timerProcPerf_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcPerf.Tick
        Dim z As Double = curProc.CpuUsage
        Dim z2 As Double = curProc.Infos.AverageCpuUsage
        If Double.IsNegativeInfinity(z) Then z = 0
        Me.graphCPU.Add2Values(z * 100, z2 * 100)
        z = curProc.Infos.MemoryInfos.WorkingSetSize / 2147483648 * 100
        Me.graphMemory.AddValue(z)
        Me.graphIO.Add2Values(curProc.IODelta.ReadTransferCount, curProc.IODelta.WriteTransferCount)
        Trace.WriteLine("w  " & curProc.IODelta.WriteTransferCount)
        Trace.WriteLine("r  " & curProc.IODelta.ReadTransferCount)
        Me.graphCPU.Refresh()
        Me.graphIO.Refresh()
        Me.graphMemory.Refresh()


        ' Refresh informations about process
        If Not (Me.tabProcess.SelectedTab.Text = "Informations" Or _
            Me.tabProcess.SelectedTab.Text = "Strings") Then _
            Call Me.refreshProcessTab()

        ' Display caption
        Call ChangeCaption()

        ' If online infos received, display it
        If _asyncDownloadDone Then
            Me.lblSecurityRisk.Text = "Risk : " & _asyncInfoRes._Risk.ToString
            Me.rtbOnlineInfos.Text = _asyncInfoRes._Description
            _asyncDlThread.Abort()
            _asyncInfoRes = Nothing
            _asyncDlThread = Nothing
            _asyncDownloadDone = False
        End If

    End Sub

    ' Change caption
    Private Sub ChangeCaption()
        Me.Text = curProc.Infos.Name & " (" & curProc.Infos.Pid.ToString & ")"
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
                Me.Text &= " - " & Me.lvProcNetwork.Items.Count.ToString & " connexions"
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

    Private Sub getProcString(ByRef curProc As cProcess)

        Static reentrance As Boolean = False
        If reentrance Then Exit Sub
        reentrance = True

        Me.lvProcString.Items.Clear()
        If Me.optProcStringImage.Checked Then
            ' Image
            Call DisplayFileStringsImage(curProc)
        Else
            ' Memory
            cRW = New cProcessMemRW(curProc.Infos.Pid)
            Dim lRes() As Integer
            ReDim lRes(0)
            Dim sRes() As String
            ReDim sRes(0)
            cRW.SearchEntireStringMemory(lRes, sRes, Me.pgbString)

            __sRes = sRes
            __lRes = lRes

            Me.lvProcString.VirtualListSize = sRes.Length

        End If

        reentrance = False
    End Sub

    ' Display file strings
    Public Sub DisplayFileStringsImage(ByRef cp As cProcess)
        Dim s As String = vbNullString
        Dim sCtemp As String = vbNullString
        Dim x As Integer = 0
        Dim bTaille As Integer
        Dim lLen As Integer
        Dim tRes() As cProcessMemRW.T_RESULT
        Dim cArraySizeBef As Integer = 0
        Dim strCtemp As String = vbNullString
        '        Dim strBuffer As String
        Dim curByte As Long = 0

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
            Me.pgbString.Maximum = CInt(lLen / 10000 + 2)
            Me.pgbString.Value = 0

            ' Ok, parse file
            Do Until x >= lLen

                If _stringSearchImmediateStop Then
                    ' Exit
                    Me.pgbString.Value = Me.pgbString.Maximum
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

                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = x
                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp
                        cArraySizeBef += 1

                    End If
                    strCtemp = vbNullString
                End If

                If (x Mod 10000) = 0 Then
                    Me.pgbString.Value += 1
                    Application.DoEvents()
                End If

                x += 1
            Loop

            Me.pgbString.Value = Me.pgbString.Maximum


            ' Last item
            If Len(strCtemp) > SIZE_FOR_STRING Then
                ' Resize only every BUF times
                If cArraySizeBef = BUF_SIZE Then
                    cArraySizeBef = 0
                    ReDim Preserve tRes(tRes.Length + BUF_SIZE)
                End If

                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = lLen
                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp

            End If


            Dim lngRes() As Integer
            Dim strRes() As String
            ReDim lngRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1)
            ReDim strRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1)
            For x = 0 To tRes.Length - BUF_SIZE + cArraySizeBef - 1
                lngRes(x) = tRes(x).curOffset
                strRes(x) = tRes(x).strString
            Next x

            __sRes = strRes
            __lRes = lngRes

            Me.lvProcString.VirtualListSize = tRes.Length - BUF_SIZE + cArraySizeBef - 1

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
        My.Settings.ProcSelectedTab = Me.tabProcess.SelectedTab.Text
    End Sub

    Private Sub cmdProcSearchL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdProcSearchL.Click
        Dim sSearch As String = Me.txtSearchProcString.Text.ToLowerInvariant
        Dim curIndex As Integer = Me.lvProcString.Items.Count

        If Me.lvProcString.SelectedIndices IsNot Nothing AndAlso _
            Me.lvProcString.SelectedIndices.Count > 0 Then _
                curIndex = Me.lvProcString.SelectedIndices(0)

        For z As Integer = curIndex - 1 To 0 Step -1
            Dim sComp As String = Me.lvProcString.Items(z).SubItems(1).Text.ToLowerInvariant
            If InStr(sComp, sSearch, CompareMethod.Binary) > 0 Then
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
            '
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
            Dim reg As New MemoryHexEditor.control.MemoryRegion(it.Infos.BaseAddress, it.Infos.RegionSize)
            frm.SetPidAndRegion(it.Infos.ProcessId, reg)
            frm.Show()
        Next
    End Sub

    Private Sub cmdShowFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowFileDetails.Click
        Dim s As String = curProc.Infos.Path
        If IO.File.Exists(s) Then
            _frmMain.DisplayDetailsFile(s)
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
        Me.lvProcServices.ProcessId = curProc.Infos.Pid
        Me.lvProcServices.UpdateTheItems()

    End Sub

    ' Show modules
    Public Sub ShowModules()

        Dim pid() As Integer
        ReDim pid(0)
        pid(0) = curProc.Infos.Pid
        lvModules.ProcessId = pid
        lvModules.UpdateTheItems()

    End Sub

    ' Show env variables
    Public Sub ShowEnvVariables()

        lvProcEnv.ProcessId = curProc.Infos.Pid
        lvProcEnv.Peb = curProc.Infos.PEBAddress
        lvProcEnv.UpdateTheItems()

    End Sub

    ' Show privileges
    Public Sub ShowPrivileges()

        lvPrivileges.ProcessId = curProc.Infos.Pid
        lvPrivileges.UpdateTheItems()

    End Sub

    ' Show threads
    Public Sub ShowThreads()

        Dim pid() As Integer
        ReDim pid(0)
        pid(0) = curProc.Infos.Pid
        Me.lvThreads.ProcessId = pid
        Me.lvThreads.UpdateTheItems()

    End Sub

    ' Show network connections
    Public Sub ShowNetwork()

        Dim pid(0) As Integer
        pid(0) = curProc.Infos.Pid
        Me.lvProcNetwork.ShowAllPid = False
        Me.lvProcNetwork.ProcessId = pid
        Me.lvProcNetwork.UpdateTheItems()

    End Sub

    ' Show memory regions
    Public Sub ShowRegions()

        Me.lvProcMem.ProcessId = curProc.Infos.Pid
        Me.lvProcMem.UpdateTheItems()

    End Sub

    ' Show threads
    Public Sub ShowWindows(Optional ByVal allWindows As Boolean = True)

        Dim pid(0) As Integer
        pid(0) = curProc.Infos.Pid
        Me.lvWindows.ProcessId = pid
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowUnNamed = Me.MenuItemWShowUn.Checked
        Me.lvWindows.UpdateTheItems()

    End Sub

    ' Display handles of process
    Private Sub ShowHandles()

        Me.lvHandles.ShowUnnamed = Me.MenuItemShowUnnamedHandles.Checked
        Dim pids(0) As Integer
        pids(0) = curProc.Infos.Pid
        Me.lvHandles.ProcessId = pids
        Me.lvHandles.UpdateTheItems()

    End Sub

    ' Update log items
    Private Sub ShowLogItems()

        Me.lvLog.ProcessId = curProc.Infos.Pid
        Me.lvLog.CaptureItems = Me.LogCaptureMask
        Me.lvLog.DisplayItems = Me.LogDisplayMask

        Me.lvLog.UpdateTheItems()

    End Sub

    Private Sub lvProcString_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcString.MouseDown
        Me.menuViewMemory.Enabled = optProcStringMemory.Checked
    End Sub

    Private Sub chkLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLog.CheckedChanged
        Me.timerLog.Enabled = Me.chkLog.Checked
    End Sub

    Private Sub timerLog_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerLog.Tick
        Dim _i As Integer = API.GetTickCount

        Me.lvLog.BeginUpdate()

        'Call _CheckThreads()
        'Call _CheckServices()
        'Call _CheckModules()
        'Call _CheckWindows()
        'Call _CheckHandles()
        'Call _CheckMemory()
        'Call _CheckNetwork()
        Call ShowLogItems()

        Me.lvLog.EndUpdate()

        If _autoScroll AndAlso Me.lvLog.Items.Count > 0 Then
            Me.lvLog.Items(Me.lvLog.Items.Count - 1).EnsureVisible()
        End If

        Call ChangeCaption()
        Trace.WriteLine("Log updated in " & (API.GetTickCount - _i).ToString & " ms")
    End Sub

    ' Check if there are changes about network
    Private Sub _CheckNetwork()
        ' TODO_
        'Static _dico As New Dictionary(Of String, cNetwork.LightConnection)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cNetwork.LightConnection)

        'Dim _itemId() As String
        'ReDim _itemId(0)
        'Dim _pid(0) As Integer
        '_pid(0) = curProc.Infos.Pid
        'Call cNetwork.Enumerate(False, _pid, _itemId, _buffDico)

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As String In _itemId
        '        If Not (_dico.ContainsKey(z)) Then
        '            ' New item
        '            Dim lm As cNetwork.LightConnection = _buffDico.Item(z)
        '            If lm.remote IsNot Nothing Then
        '                Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- Remote : " & lm.remote.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, True)
        '            Else
        '                Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, True)
        '            End If
        '        End If
        '    Next
        'End If


        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As String In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Dim lm As cNetwork.LightConnection = _dico.Item(z)
        '            If lm.remote IsNot Nothing Then
        '                Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- Remote : " & lm.remote.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, False)
        '            Else
        '                Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, False)
        '            End If
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

    End Sub

    ' Check if there are changes about memory areas
    Private Sub _CheckMemory()

        'Static _dico As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)

        'Dim _itemId() As String
        'ReDim _itemId(0)
        'Call cProcessMemRW.Enumerate(curProc.Infos.Pid, _itemId, _buffDico)

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As String In _itemId
        '        If Not (_dico.ContainsKey(z)) Then
        '            ' New item
        '            Dim lm As cProcessMemRW.MEMORY_BASIC_INFORMATION = _buffDico.Item(z)
        '            Call addToLog("Memory region created (0x" & lm.BaseAddress.ToString("x") & " -- Size : " & GetFormatedSize(lm.RegionSize) & " -- Type : " & lm.lType.ToString & " -- Protection : " & lm.Protect.ToString & ")", LogItemType.MemoryItem, True)
        '        End If
        '    Next
        'End If

        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As String In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Dim lm As cProcessMemRW.MEMORY_BASIC_INFORMATION = _dico.Item(z)
        '            Call addToLog("Memory region deleted (0x" & lm.BaseAddress.ToString("x") & " -- Size : " & GetFormatedSize(lm.RegionSize) & " -- Type : " & lm.lType.ToString & " -- Protection : " & lm.Protect.ToString & ")", LogItemType.MemoryItem, False)
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

    End Sub

    ' Check if there are changes about handles
    Private Sub _CheckHandles()
        'TODO_
        'Static _dico As New Dictionary(Of String, cHandle.LightHandle)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cHandle.LightHandle)

        'Dim _itemId() As String
        'ReDim _itemId(0)
        'Dim _pid(0) As Integer
        '_pid(0) = curProc.Infos.Pid
        'Call cHandle.Enumerate(_pid, True, _itemId, _buffDico)

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As String In _itemId
        '        If Not (_dico.ContainsKey(z)) Then
        '            ' New item
        '            Dim lh As cHandle.LightHandle = _buffDico.Item(z)
        '            Call addToLog("Handle created (" & lh.handle.ToString & " -- " & lh.type & " -- " & lh.name & ")", LogItemType.HandleItem, True)
        '        End If
        '    Next
        'End If

        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As String In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Dim lh As cHandle.LightHandle = _dico.Item(z)
        '            Call addToLog("Handle created (" & lh.handle.ToString & " -- " & lh.type & " -- " & lh.name & ")", LogItemType.HandleItem, False)
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

    End Sub

    ' Check if there are changes about threads
    Private Sub _CheckThreads()

        'Static _dico As New Dictionary(Of String, cThread.LightThread)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cThread.LightThread)

        'Dim _itemId() As String
        'ReDim _itemId(0)
        'Dim _pid(0) As Integer
        '_pid(0) = curProc.Infos.Pid
        'Call cThread.Enumerate(_pid, _itemId, _buffDico)

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As String In _itemId
        '        If Not (_dico.ContainsKey(z)) Then
        '            ' New item
        '            Call addToLog("Thread created (" & _buffDico.Item(z).t.Id.ToString & ")", LogItemType.ThreadItem, True)
        '        End If
        '    Next
        'End If

        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As String In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Call addToLog("Thread deleted (" & _dico.Item(z).t.Id.ToString & ")", LogItemType.ThreadItem, False)
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

    End Sub

    ' Check if there are changes about services
    Private Sub _CheckServices()
        'TODO_
        'Static _enum As New cServEnum
        'Static _dico As New Dictionary(Of String, cService.LightService)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cService.LightService)
        'Dim _buffDico2 As New Dictionary(Of String, cService.LightService)  ' Useless

        'Dim _itemId() As String
        'ReDim _itemId(0)

        'Call _enum.EnumerateApi(curProc.Infos.Pid, _itemId, _buffDico, _buffDico2)

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As String In _itemId
        '        If Not (_dico.ContainsKey(z)) Then
        '            ' New item
        '            Call addToLog("Service started (" & _buffDico.Item(z).name & ")", LogItemType.ServiceItem, True)
        '        End If
        '    Next
        'End If

        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As String In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Call addToLog("Service stopped (" & _dico.Item(z).name & ")", LogItemType.ServiceItem, False)
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

    End Sub

    ' Check if there are changes about modules
    Private Sub _CheckModules()
        'TODO_
        'Static _dico As New Dictionary(Of String, cModule.MODULEENTRY32)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cModule.MODULEENTRY32)

        'Dim _itemId() As String
        'ReDim _itemId(0)

        'If _local Then
        '    Call cLocalModule.Enumerate(curProc.Infos.Pid, _itemId, _buffDico)
        'Else
        '    Call cLocalModule.Enumerate(curProc.Infos.Pid, _itemId, _buffDico)
        'End If

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As String In _itemId
        '        If Not (_dico.ContainsKey(z)) Then
        '            ' New item
        '            Call addToLog("Module loaded (" & _buffDico.Item(z).szModule & ")", LogItemType.ModuleItem, True)
        '        End If
        '    Next
        'End If

        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As String In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Call addToLog("Module unloaded (" & _dico.Item(z).szModule & ")", LogItemType.ModuleItem, False)
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

    End Sub

    ' Check if there are changes about windows
    Private Sub _CheckWindows()
        ' TODO_
        'Static _dico As New Dictionary(Of String, cWindow.LightWindow)
        'Static _first As Boolean = True
        'Dim _buffDico As New Dictionary(Of String, cWindow.LightWindow)

        'Dim _itemId() As Integer
        'ReDim _itemId(0)
        'Dim _pid(0) As Integer
        '_pid(0) = curProc.Infos.Pid
        'Call cWindow.Enumerate(True, _pid, _itemId, _buffDico)

        'If _first Then
        '    _dico = _buffDico
        '    _first = False
        'End If

        '' Check if there are new items
        'If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
        '    For Each z As Integer In _itemId
        '        If Not (_dico.ContainsKey(z.ToString)) Then
        '            ' New item
        '            Dim _light As cWindow.LightWindow = _buffDico.Item(z.ToString)
        '            Call addToLog("Window created (" & _light.handle.ToString & "  --  " & cWindow.GetCaption(_light.handle) & ")", LogItemType.WindowItem, True)
        '        End If
        '    Next
        'End If

        '' Check if there are deleted items
        'If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
        '    For Each z As Integer In _dico.Keys
        '        If Array.IndexOf(_itemId, z) < 0 Then
        '            ' Deleted item
        '            Call addToLog("Windows deleted (" & _dico.Item(z.ToString).handle.ToString & ")", LogItemType.WindowItem, False)
        '        End If
        '    Next
        'End If

        '' Save dico
        '_dico = _buffDico

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
                    MsgBox("Save done.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Log save")
                End If
            End If
        Catch ex As Exception
            '
        End Try

    End Sub

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

    'Private Sub addToLog(ByVal s As String, ByVal _type As asyncCallbackLogEnumerate.LogItemType, _
    '                     ByVal created As Boolean)
    '    Static _number As Integer = 0

    '    If (_type And _logCaptureMask) = _type Then
    '        _number += 1
    '        _logDico.Add(_number, New LogItem(s, _type, created))

    '        If (_type And _logDisplayMask) = _type Then
    '            ' Here we add the item to lv
    '            Dim b As Boolean
    '            If created Then
    '                b = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.CreatedItems) = asyncCallbackLogEnumerate.LogItemType.CreatedItems
    '            Else
    '                b = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.DeletedItems) = asyncCallbackLogEnumerate.LogItemType.DeletedItems
    '            End If
    '            If b Then
    '                Dim it As New ListViewItem(Date.Now.ToLongDateString & " -- " & Date.Now.ToLongTimeString)
    '                it.SubItems.Add(_type.ToString)
    '                it.SubItems.Add(s)
    '                Me.lvLog.Items.Add(it)
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub cmdLogOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogOptions.Click
        Dim frm As New frmLogOptions
        frm.LogCaptureMask = _logCaptureMask
        frm.LogDisplayMask = _logDisplayMask
        frm.Form = Me
        frm._autoScroll.Checked = _autoScroll

        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' Redisplay items
            Me.lvLog.CaptureItems = Me.LogCaptureMask
            Me.lvLog.DisplayItems = Me.LogDisplayMask
            Me.lvLog.ReAddItems()
        End If
    End Sub

    ' Here we finished to download informations from internet
    Private _asyncInfoRes As cAsyncProcInfoDownload.InternetProcessInfo
    Private _asyncDownloadDone As Boolean = False
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
            ElseIf InStr(_g.Name, "Transfer") + InStr(_g.Name, "Operation") > 0 Then
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
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to kill this process ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        Call curProc.Kill()
    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to suspend this process ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
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
        frm.ShowDialog()
    End Sub

    ' When we've finished to get all non fixed infos
    Private Sub asyncAllNonFixedInfos_HasGotAllNonFixedInfos(ByVal Success As Boolean, ByRef newInfos As CoreFunc.API.SYSTEM_PROCESS_INFORMATION, ByVal msg As String) Handles asyncAllNonFixedInfos.HasGotAllNonFixedInfos
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
            Me.lvProcNetwork.ConnectionObj = theConnection
            theConnection.Connect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Can not connect")
        End Try
    End Sub

    Private Sub theConnection_Connected() Handles theConnection.Connected
        '
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        '
    End Sub

    Private Sub lvProcServices_HasChangedColumns() Handles lvProcServices.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvProcServices, "COLprocdetail_service")
    End Sub

    Private Sub lvProcServices_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcServices.MouseDoubleClick
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            Dim frm As New frmServiceInfo
            frm.SetService(it)
            frm.Show()
        Next
    End Sub

    Private Sub lvProcServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcServices.MouseUp
        Me.MenuItemServOpenDir.Enabled = _local
        Me.MenuItemServFileProp.Enabled = _local
        Me.MenuItemServFileDetails.Enabled = _local
        If lvProcServices.SelectedItems.Count = 1 Then
            Dim cSe As cService = Me.lvProcServices.GetSelectedItem
            Dim start As API.SERVICE_START_TYPE = cSe.Infos.StartType
            Dim state As API.SERVICE_STATE = cSe.Infos.State
            Dim acc As API.SERVICE_ACCEPT = cSe.Infos.AcceptedControl

            Me.MenuItemServPause.Text = IIf(state = API.SERVICE_STATE.Running, "Pause", "Resume").ToString
            Me.MenuItemServPause.Enabled = (acc And API.SERVICE_ACCEPT.PauseContinue) = API.SERVICE_ACCEPT.PauseContinue
            Me.MenuItemServStart.Enabled = Not (state = API.SERVICE_STATE.Running)
            Me.MenuItemServStop.Enabled = (acc And API.SERVICE_ACCEPT.Stop) = API.SERVICE_ACCEPT.Stop

            Me.MenuItemServDisabled.Checked = (start = API.SERVICE_START_TYPE.StartDisabled)
            MenuItemServDisabled.Enabled = Not (MenuItemServDisabled.Checked)
            Me.MenuItemServAutoStart.Checked = (start = API.SERVICE_START_TYPE.AutoStart)
            MenuItemServAutoStart.Enabled = Not (MenuItemServAutoStart.Checked)
            Me.MenuItemServOnDemand.Checked = (start = API.SERVICE_START_TYPE.DemandStart)
            MenuItemServOnDemand.Enabled = Not (MenuItemServOnDemand.Checked)
        ElseIf lvProcServices.SelectedItems.Count > 1 Then
            Me.MenuItemServPause.Text = "Pause"
            Me.MenuItemServPause.Enabled = True
            Me.MenuItemServStart.Enabled = True
            Me.MenuItemServStop.Enabled = True
            MenuItemServDisabled.Checked = True
            MenuItemServDisabled.Enabled = True
            MenuItemServAutoStart.Checked = True
            MenuItemServAutoStart.Enabled = True
            MenuItemServOnDemand.Checked = True
            MenuItemServOnDemand.Enabled = True
        End If

        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuService.Show(Me.lvProcServices, e.Location)
        End If
    End Sub

    Private Sub lvThreads_HasChangedColumns() Handles lvThreads.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvThreads, "COLprocdetail_thread")
    End Sub

    Private Sub lvThreads_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvThreads.KeyDown
        'Pref.LoadListViewColumns(Me.lvPrivileges, "COLprocdetail_privilege")
        'Pref.LoadListViewColumns(Me.lvProcMem, "COLprocdetail_memory")
        'Pref.LoadListViewColumns(Me.lvProcServices, "COLprocdetail_service")
        'Pref.LoadListViewColumns(Me.lvProcNetwork, "COLprocdetail_network")
        'Pref.LoadListViewColumns(Me.lvHandles, "COLprocdetail_handle")
        'Pref.LoadListViewColumns(Me.lvWindows, "COLprocdetail_window")
        'Pref.LoadListViewColumns(Me.lvThreads, "COLprocdetail_thread")
        'Pref.LoadListViewColumns(Me.lvModules, "COLprocdetail_module")
        'Pref.LoadListViewColumns(Me.lvProcEnv, "COLprocdetail_envvariable")
        'Pref.LoadListViewColumns(Me.lvLog, "COLprocdetail_log")
    End Sub

    Private Sub lvWindows_HasChangedColumns() Handles lvWindows.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvWindows, "COLprocdetail_window")
    End Sub

    Private Sub lvPrivileges_HasChangedColumns() Handles lvPrivileges.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvPrivileges, "COLprocdetail_privilege")
    End Sub

    Private Sub lvProcMem_HasChangedColumns() Handles lvProcMem.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvProcMem, "COLprocdetail_memory")
    End Sub

    Private Sub lvProcNetwork_HasChangedColumns() Handles lvProcNetwork.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvProcNetwork, "COLprocdetail_network")
    End Sub

    Private Sub lvHandles_HasChangedColumns() Handles lvHandles.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvHandles, "COLprocdetail_handle")
    End Sub

    Private Sub lvModules_HasChangedColumns() Handles lvModules.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvModules, "COLprocdetail_module")
    End Sub

    Private Sub lvProcEnv_HasChangedColumns() Handles lvProcEnv.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvProcEnv, "COLprocdetail_envvariable")
    End Sub

    Private Sub cmdInspectExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInspectExe.Click
        Dim _depForm As New DependenciesViewer.frmMain
        With _depForm
            .OpenReferences(Me.curProc.Infos.Path)
            .HideOpenMenu()
            .Show()
        End With
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
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

        Dim add As Integer = CInt(Me.lvProcString.Items(Me.lvProcString.SelectedIndices(0)).Tag)
        For Each reg As cMemRegion In Me.lvProcMem.GetAllItems

            If reg.Infos.BaseAddress <= add AndAlso add <= (reg.Infos.BaseAddress + reg.Infos.RegionSize) Then
                Dim frm As New frmHexEditor
                Dim regio As New MemoryHexEditor.control.MemoryRegion(reg.Infos.BaseAddress, reg.Infos.RegionSize)
                frm.SetPidAndRegion(curProc.Infos.Pid, regio)
                frm._hex.NavigateToOffset(CInt((add - regio.BeginningAddress) / 16))
                frm.Show()
                Exit For
            End If
        Next

    End Sub

    Private Sub lvProcString_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcString.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuString.Show(Me.lvProcString, e.Location)
        End If
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuCloseTCP.Click
        For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
            If it.Infos.Protocol = API.NetworkProtocol.Tcp Then
                it.CloseTCP()
            End If
        Next
    End Sub

    Private Sub MenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem11.Click
        Me.lvProcNetwork.ChooseColumns()
    End Sub

    Private Sub lvHandles_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvHandles.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuHandle.Show(Me.lvHandles, e.Location)
        End If
    End Sub

    Private Sub lvModules_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvModules.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuModule.Show(Me.lvModules, e.Location)
        End If
    End Sub

    Private Sub lvPrivileges_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvPrivileges.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuPrivileges.Show(Me.lvPrivileges, e.Location)
        End If
    End Sub

    Private Sub lvProcMem_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcMem.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuProcMem.Show(Me.lvProcMem, e.Location)
        End If
    End Sub

    Private Sub lvProcNetwork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcNetwork.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim enable As Boolean = False
            For Each it As cNetwork In Me.lvProcNetwork.GetSelectedItems
                If it.Infos.Protocol = API.NetworkProtocol.Tcp Then
                    If it.Infos.State <> API.MIB_TCP_STATE.Listening AndAlso it.Infos.State <> API.MIB_TCP_STATE.TimeWait AndAlso it.Infos.State <> API.MIB_TCP_STATE.CloseWait Then
                        enable = True
                        Exit For
                    End If
                End If
            Next
            Me.menuCloseTCP.Enabled = enable

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

            Me.mnuThread.Show(Me.lvThreads, e.Location)
        End If
    End Sub

    Private Sub lvWindows_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvWindows.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuWindow.Show(Me.lvWindows, e.Location)
        End If
    End Sub

    Private Sub MenuItemCloseHandle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCloseHandle.Click
        If My.Settings.WarnDangerousActions Then
            If MsgBox("Are you sure you want to close these handles ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
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
        Dim peb As Integer = curProc.Infos.PEBAddress
        For Each reg As cMemRegion In Me.lvProcMem.GetAllItems
            If reg.Infos.BaseAddress <= peb AndAlso peb <= (reg.Infos.BaseAddress + reg.Infos.RegionSize) Then
                Dim frm As New frmHexEditor
                Dim regio As New MemoryHexEditor.control.MemoryRegion(reg.Infos.BaseAddress, reg.Infos.RegionSize)
                frm.SetPidAndRegion(curProc.Infos.Pid, regio)
                frm._hex.NavigateToOffset(peb)
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
            it.ChangeStatus(API.PRIVILEGE_STATUS.PRIVILEGE_ENABLED)
        Next
    End Sub

    Private Sub MenuItemPriDisable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPriDisable.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(API.PRIVILEGE_STATUS.PRIVILEGE_DISBALED)
        Next
    End Sub

    Private Sub MenuItemPriRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemPriRemove.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(API.PRIVILEGE_STATUS.PRIVILEGE_REMOVED)
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
                _frmMain.DisplayDetailsFile(s)
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
            Dim _depForm As New DependenciesViewer.frmMain
            With _depForm
                .OpenReferences(s)
                .HideOpenMenu()
                .Show()
            End With
        End If
    End Sub

    Private Sub MenuItemViewModuleMemory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemViewModuleMemory.Click

        If Me.lvProcMem.Items.Count = 0 Then
            Call ShowRegions()
        End If

        For Each it As cModule In Me.lvModules.GetSelectedItems
            Dim add As Integer = it.Infos.BaseAddress

            For Each reg As cMemRegion In Me.lvProcMem.GetAllItems

                If reg.Infos.BaseAddress <= add AndAlso add < (reg.Infos.BaseAddress + reg.Infos.RegionSize) Then
                    Dim frm As New frmHexEditor
                    Dim regio As New MemoryHexEditor.control.MemoryRegion(reg.Infos.BaseAddress, reg.Infos.RegionSize)
                    frm.SetPidAndRegion(curProc.Infos.Pid, regio)
                    frm._hex.NavigateToOffset(CInt((add - regio.BeginningAddress) / 16) - 1)
                    frm.Show()
                    Exit For
                End If
            Next

        Next

    End Sub

    Private Sub MenuItemUnloadModule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemUnloadModule.Click
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
                If cp.Infos.Name = it.Infos.Name Then
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
                _frmMain.DisplayDetailsFile(s)
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
            If it.Infos.State = API.SERVICE_STATE.Running Then
                it.PauseService()
            Else
                it.ResumeService()
            End If
        Next
    End Sub

    Private Sub MenuItemServStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStop.Click
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
            it.SetServiceStartType(API.SERVICE_START_TYPE.AutoStart)
        Next
    End Sub

    Private Sub MenuItemServOnDemand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServOnDemand.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(API.SERVICE_START_TYPE.DemandStart)
        Next
    End Sub

    Private Sub MenuItemServDisabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDisabled.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(API.SERVICE_START_TYPE.StartDisabled)
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
        Dim r As API.RECT

        If Me.lvWindows.SelectedItems.Count > 0 Then

            Dim frm As New frmWindowPosition
            With frm
                .SetCurrentPositions(Me.lvWindows.GetSelectedItem.Infos.Positions)

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

    Private Sub lvLog_HasChangedColumns() Handles lvLog.HasChangedColumns
        Pref.SaveListViewColumns(Me.lvLog, "COLprocdetail_log")
    End Sub
End Class