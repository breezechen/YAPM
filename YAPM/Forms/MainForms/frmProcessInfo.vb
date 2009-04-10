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

Public Class frmProcessInfo

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function
    Private Declare Function GetTickCount Lib "kernel32" () As Integer

    Private WithEvents asyncAllNonFixedInfos As asyncCallbackProcGetAllNonFixedInfos

    Private WithEvents curProc As cProcess
    Private Const NO_INFO_RETRIEVED As String = "N/A"
    Private m_SortingColumn As ColumnHeader
    Private WithEvents _AsyncDownload As cAsyncProcInfoDownload
    Private _asyncDlThread As Threading.Thread

    Private WithEvents theConnection As New cConnection

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
    Private __con As Management.ConnectionOptions


    ' Refresh current tab
    Private Sub refreshProcessTab()

        If curProc Is Nothing Then Exit Sub

        ' General informations
        Select Case Me.tabProcess.SelectedTab.Text

            Case "Token"
                If _local Then _
                Call ShowPrivileges()

            Case "Modules"
                'If _local Then _
                Call ShowModules()

            Case "Threads"
                'If _local Then _
                Call ShowThreads()

            Case "Windows"
                If _local Then _
                Call ShowWindows()

            Case "Handles"
                If _local Then _
                Call ShowHandles()

            Case "Memory"
                If _local Then _
                Call ShowRegions()

            Case "Environment"
                If _local Then _
                Call showenvvariables()

            Case "Network"
                If _local Then _
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
                Me.cbPriority.Text = curProc.Infos.Priority.ToString
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
        Me.lblGDIcount.Text = CStr(curProc.Infos.GdiObjects)
        Me.lblUserObjectsCount.Text = CStr(curProc.Infos.UserObjects)
        Me.lblAverageCPUusage.Text = curProc.GetInformation("AverageCpuUsage")

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

            'If chkModules.Checked Then
            '    ' Retrieve modules
            '    s = s & "\par"
            '    s = s & "  \b Loaded modules\b0\par"
            '    Dim m As ProcessModule
            '    Dim mdl As ProcessModuleCollection = curProc.Modules
            '    s = s & "\tab " & CStr(mdl.Count) & " modules loaded" & "\par"
            '    For Each m In mdl
            '        s = s & "\tab " & Replace(m.FileVersionInfo.FileName, "\", "\\") & "\par"
            '    Next

            '    ' Retrieve threads infos
            '    s = s & "\par"
            '    s = s & "  \b Threads\b0\par"
            '    Dim pt As ProcessThread
            '    Dim thr As System.Diagnostics.ProcessThreadCollection = curProc.Threads
            '    s = s & "\tab " & CStr(thr.Count) & " threads \par"
            '    For Each pt In thr
            '        s = s & "\tab " & CStr(pt.Id) & "\par"
            '        s = s & "\tab\tab " & "Priority level : " & CStr(pt.PriorityLevel.ToString) & "\par"
            '        Dim tsp As TimeSpan = pt.TotalProcessorTime
            '        Dim s2 As String = String.Format("{0:00}", tsp.TotalHours) & ":" & _
            '            String.Format("{0:00}", tsp.Minutes) & ":" & _
            '            String.Format("{0:00}", tsp.Seconds)
            '        s = s & "\tab\tab " & "Start address : " & CStr(pt.StartAddress) & "\par"
            '        s = s & "\tab\tab " & "Start time : " & pt.StartTime.ToLongDateString & " -- " & pt.StartTime.ToLongTimeString & "\par"
            '        s = s & "\tab\tab " & "State : " & CStr(pt.ThreadState.ToString) & "\par"
            '        s = s & "\tab\tab " & "Processor time : " & s2 & "\par"
            '    Next
            'End If

            'If chkHandles.Checked Then
            '    ' Retrieve handles
            '    s = s & "\par"
            '    s = s & "  \b Loaded handles\b0\par"
            '    Dim i As Integer
            '    frmMain.handles_Renamed.Refresh()
            '    For i = 0 To frmMain.handles_Renamed.Count - 1
            '        With frmMain.handles_Renamed
            '            If (.GetProcessID(i) = pid) And (Len(.GetObjectName(i)) > 0) Then
            '                s = s & "\tab " & .GetNameInformation(i) & " : " & Replace(.GetObjectName(i), "\", "\\") & "\par"
            '            End If
            '        End With
            '    Next
            'End If

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

        ' Cool theme
        SetWindowTheme(Me.lvProcString.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvProcEnv.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvProcNetwork.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvProcMem.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvProcServices.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvPrivileges.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvModules.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvHandles.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvThreads.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvWindows.Handle, "explorer", Nothing)
        SetWindowTheme(Me.lvLog.Handle, "explorer", Nothing)

        ' Some tooltips
        frmMain.SetToolTip(Me.chkModules, "Check if you want to retrieve modules and threads infos when you click on listview.")
        frmMain.SetToolTip(Me.chkModules, "Check if you want to retrieve online infos when you click on listview.")
        frmMain.SetToolTip(Me.cmdInfosToClipB, "Copy process informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style).")
        frmMain.SetToolTip(Me.chkHandles, "Check if you want to retrieve handles infos when you click on listview.")
        frmMain.SetToolTip(Me.txtImageVersion, "Version of file")
        frmMain.SetToolTip(Me.txtProcessPath, "Path of file")
        frmMain.SetToolTip(Me.cmdShowFileDetails, "Show file details")
        frmMain.SetToolTip(Me.cmdShowFileProperties, "Open file property window")
        frmMain.SetToolTip(Me.cmdOpenDirectory, "Open directory of file")
        frmMain.SetToolTip(Me.txtParentProcess, "Parent process")
        frmMain.SetToolTip(Me.txtProcessStarted, "Start time")
        frmMain.SetToolTip(Me.txtProcessId, "Process ID")
        frmMain.SetToolTip(Me.txtProcessUser, "Process user")
        frmMain.SetToolTip(Me.txtCommandLine, "Command line which launched process")
        frmMain.SetToolTip(Me.cmdGetOnlineInfos, "Retrieve online information (based on process name)")
        frmMain.SetToolTip(Me.optProcStringImage, "Get strings from image file on disk")
        frmMain.SetToolTip(Me.optProcStringMemory, "Get strings from memory")
        frmMain.SetToolTip(Me.cmdProcStringSave, "Save list in a file")
        frmMain.SetToolTip(Me.pgbString, "Progression. Click to stop.")
        frmMain.SetToolTip(Me.txtSearchProcString, "Search a specific string")
        frmMain.SetToolTip(Me.txtRunTime, "Total run time")
        frmMain.SetToolTip(Me.cmdProcSearchL, "Previous result (F2 on listview also works)")
        frmMain.SetToolTip(Me.cmdProcSearchR, "Next result (F3 on listview also works)")

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

        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)

        Me.cmdAffinity.Enabled = _local
        Me.cmdPause.Enabled = _local
        Me.cmdResume.Enabled = _local
        Me.lvModules.CatchErrors = Not (_local)
        Me.timerProcPerf.Enabled = _local
        Me.lvPrivileges.Enabled = _local
        Me.lvHandles.Enabled = _local
        Me.lvLog.Enabled = _local
        Me.lvProcEnv.Enabled = _local
        Me.lvProcMem.Enabled = _local
        Me.lvProcNetwork.Enabled = _local
        Me.lvProcString.Enabled = _local
        Me.lvWindows.Enabled = _local
        Me.SplitContainerStrings.Enabled = _local
        Me.SplitContainerLog.Enabled = _local
        Me.cmdShowFileDetails.Enabled = _local
        Me.cmdShowFileProperties.Enabled = _local
        Me.cmdOpenDirectory.Enabled = _local
        Me.chkModules.Enabled = _local
        Me.chkHandles.Enabled = _local
        Me.ShowFileDetailsToolStripMenuItem.Enabled = _local
        Me.ToolStripMenuItem36.Enabled = _local
        Me.ViewMemoryToolStripMenuItem.Enabled = _local

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
        Me.graphIO.AddValue(curProc.Infos.IOValues.ReadTransferCount)
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
                    My.Application.DoEvents()
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
        Me.ToolStripMenuItem6.Enabled = (Me.pctBigIcon.Image IsNot Nothing)
    End Sub

    Private Sub pctSmallIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctSmallIcon.MouseDown
        Me.ToolStripMenuItem7.Enabled = (Me.pctSmallIcon.Image IsNot Nothing)
    End Sub

    Private Sub tabProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabProcess.SelectedIndexChanged
        Call Me.refreshProcessTab()
        Call ChangeCaption()
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
        With frmMain.saveDial
            .AddExtension = True
            .Filter = "Txt (*.txt*)|*.txt"
            .InitialDirectory = My.Application.Info.DirectoryPath
            If Not (.ShowDialog = Windows.Forms.DialogResult.OK) Then
                Exit Sub
            End If
        End With

        ' Save our file
        Try
            Dim stream As New System.IO.StreamWriter(frmMain.saveDial.FileName, False)
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
            frmMain.DisplayDetailsFile(s)
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

    Private Sub JumpToPEBAddressToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles JumpToPEBAddressToolStripMenuItem.Click
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

    Private Sub ToolStripMenuItem44_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem44.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(API.PRIVILEGE_STATUS.PRIVILEGE_ENABLED)
        Next
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(API.PRIVILEGE_STATUS.PRIVILEGE_DISBALED)
        Next
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        For Each it As cPrivilege In Me.lvPrivileges.GetSelectedItems
            it.ChangeStatus(API.PRIVILEGE_STATUS.PRIVILEGE_REMOVED)
        Next
    End Sub

    Private Sub ToolStripMenuItem43_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem43.Click
        ' Select services associated to selected process
        If Me.lvProcServices.SelectedItems.Count > 0 Then frmMain.lvServices.SelectedItems.Clear()

        If frmMain.lvServices.Items.Count = 0 Then
            ' Refresh list
            Call frmMain.refreshServiceList()
        End If

        For Each it As cService In Me.lvProcServices.GetSelectedItems
            Dim it2 As ListViewItem
            For Each it2 In frmMain.lvServices.Items
                Dim cp As cService = frmMain.lvServices.GetItemByKey(it2.Name)
                If cp.Infos.Name = it.Infos.Name Then
                    it2.Selected = True
                    it2.EnsureVisible()
                End If
            Next
        Next
        frmMain.Ribbon.ActiveTab = frmMain.ServiceTab
        Call frmMain.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem49_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem49.Click
        Call lvProcMem_DoubleClick(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        My.Computer.Clipboard.SetImage(Me.pctBigIcon.Image)
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        My.Computer.Clipboard.SetImage(Me.pctSmallIcon.Image)
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
        lvProcEnv.peb = curProc.Infos.PEBAddress
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
        Me.lvWindows.ShowUnNamed = Me.ShowUnnamedWindowsToolStripMenuItem.Checked
        Me.lvWindows.UpdateTheItems()

    End Sub

    ' Display handles of process
    Private Sub ShowHandles()

        Me.lvHandles.ShowUnNamed = ShowUnnamedHandlesToolStripMenuItem.Checked
        Dim pids(0) As Integer
        pids(0) = curProc.Infos.Pid
        Me.lvHandles.ProcessId = pids
        Me.lvHandles.UpdateTheItems()

    End Sub

    Private Sub ToolStripMenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem36.Click
        For Each it As cModule In Me.lvModules.GetSelectedItems
            it.UnloadModule()
        Next
    End Sub

    Private Sub ShowFileDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowFileDetailsToolStripMenuItem.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.GetSelectedItem.Infos.Path
            If IO.File.Exists(s) Then
                frmMain.DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub GoogleSearchToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleSearchToolStripMenuItem2.Click
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            My.Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
    End Sub

    Private Sub ToolStripMenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem23.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadTerminate()
        Next
    End Sub

    Private Sub ToolStripMenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem24.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadSuspend()
        Next
    End Sub

    Private Sub ToolStripMenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem25.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.ThreadResume()
        Next
    End Sub

    Private Sub ToolStripMenuItem27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem27.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Idle)
        Next
    End Sub

    Private Sub LowestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LowestToolStripMenuItem.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Lowest)
        Next
    End Sub

    Private Sub ToolStripMenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem28.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.BelowNormal)
        Next
    End Sub

    Private Sub ToolStripMenuItem29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem29.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Normal)
        Next
    End Sub

    Private Sub ToolStripMenuItem30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem30.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.AboveNormal)
        Next
    End Sub

    Private Sub ToolStripMenuItem31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem31.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.Highest)
        Next
    End Sub

    Private Sub ToolStripMenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem32.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.SetPriority(ThreadPriorityLevel.TimeCritical)
        Next
    End Sub

    Private Sub lvThreads_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvThreads.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As System.Diagnostics.ThreadPriorityLevel

            If Me.lvThreads.SelectedItems.Count > 0 Then
                p = Me.lvThreads.GetSelectedItem.Infos.Priority
            End If
            Me.ToolStripMenuItem27.Checked = (p = ThreadPriorityLevel.Idle)
            Me.LowestToolStripMenuItem.Checked = (p = ThreadPriorityLevel.Lowest)
            Me.ToolStripMenuItem28.Checked = (p = ThreadPriorityLevel.BelowNormal)
            Me.ToolStripMenuItem29.Checked = (p = ThreadPriorityLevel.Normal)
            Me.ToolStripMenuItem30.Checked = (p = ThreadPriorityLevel.AboveNormal)
            Me.ToolStripMenuItem31.Checked = (p = ThreadPriorityLevel.Highest)
            Me.ToolStripMenuItem32.Checked = (p = ThreadPriorityLevel.TimeCritical)
        End If
    End Sub

    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Show()
        Next
    End Sub

    Private Sub HideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Hide()
        Next
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Close()
        Next
    End Sub

    Private Sub BringToFrontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BringToFrontToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.BringToFront(True)
        Next
    End Sub

    Private Sub DoNotBringToFrontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DoNotBringToFrontToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.BringToFront(False)
        Next
    End Sub

    Private Sub SetAsActiveWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetAsActiveWindowToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.SetAsActiveWindow()
        Next
    End Sub

    Private Sub SetAsForegroundWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetAsForegroundWindowToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.SetAsForegroundWindow()
        Next
    End Sub

    Private Sub MinimizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizeToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Minimize()
        Next
    End Sub

    Private Sub MaximizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaximizeToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Maximize()
        Next
    End Sub

    Private Sub PositionSizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PositionSizeToolStripMenuItem.Click
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

    Private Sub EnableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableToolStripMenuItem.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Enabled = True
        Next
    End Sub

    Private Sub DisableToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem1.Click
        For Each it As cWindow In Me.lvWindows.GetSelectedItems
            it.Enabled = False
        Next
    End Sub

    Private Sub ShowUnnamedHandlesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowUnnamedHandlesToolStripMenuItem.Click
        ShowUnnamedHandlesToolStripMenuItem.Checked = Not (ShowUnnamedHandlesToolStripMenuItem.Checked)
    End Sub

    Private Sub ShowUnnamedWindowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowUnnamedWindowsToolStripMenuItem.Click
        ShowUnnamedWindowsToolStripMenuItem.Checked = Not (ShowUnnamedWindowsToolStripMenuItem.Checked)
    End Sub

    Private Sub lvProcString_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcString.MouseDown
        menuViewMemoryString.Enabled = optProcStringMemory.Checked
    End Sub

    Private Sub menuViewMemoryString_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuViewMemoryString.Click

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

    Private Sub ViewMemoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewMemoryToolStripMenuItem.Click

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

    Private Sub ToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem22.Click
        If frmMain.Pref.warnDangerous Then
            If MsgBox("Are you sure you want to close these handles ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        For Each ch As cHandle In Me.lvHandles.GetSelectedItems
            Call frmMain.handles_Renamed.CloseProcessLocalHandle(ch.Infos.ProcessID, ch.Infos.Handle)
        Next
    End Sub

    Private Sub ChooseColumnsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChooseColumnsToolStripMenuItem.Click
        Me.lvThreads.ChooseColumns()
    End Sub

    Private Sub ChooseColumnsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChooseColumnsToolStripMenuItem1.Click
        Me.lvHandles.ChooseColumns()
    End Sub

    Private Sub ChooseColumnsToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChooseColumnsToolStripMenuItem2.Click
        Me.lvWindows.ChooseColumns()
    End Sub

    Private Sub ChooseColumnsToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChooseColumnsToolStripMenuItem3.Click
        Me.lvModules.ChooseColumns()
    End Sub

    Private Sub ChooseColumnsToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChooseColumnsToolStripMenuItem4.Click
        Me.lvProcMem.ChooseColumns()
    End Sub

    Private Sub ChooseColumnsToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChooseColumnsToolStripMenuItem5.Click
        Me.lvProcServices.ChooseColumns()
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        Me.lvProcNetwork.ChooseColumns()
    End Sub

    Public Sub StartLog()
        Me.chkLog.Checked = True
        Me.timerLog.Enabled = True
    End Sub

    Public Sub StopLog()
        Me.chkLog.Checked = False
        Me.timerLog.Enabled = False
    End Sub

    Private Sub chkLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLog.CheckedChanged
        Me.timerLog.Enabled = Me.chkLog.Checked
    End Sub

    Private Sub timerLog_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerLog.Tick
        Dim _i As Integer = GetTickCount

        Me.lvLog.BeginUpdate()

        Call _CheckThreads()
        Call _CheckServices()
        Call _CheckModules()
        Call _CheckWindows()
        Call _CheckHandles()
        Call _CheckMemory()
        Call _CheckNetwork()

        Me.lvLog.EndUpdate()

        If _autoScroll AndAlso Me.lvLog.Items.Count > 0 Then
            Me.lvLog.Items(Me.lvLog.Items.Count - 1).EnsureVisible()
        End If

        Call ChangeCaption()
        Trace.WriteLine("Log updated in " & (GetTickCount - _i).ToString & " ms")
    End Sub

    ' Check if there are changes about network
    Private Sub _CheckNetwork()
        ' TODO
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

        Static _dico As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)

        Dim _itemId() As String
        ReDim _itemId(0)
        Call cProcessMemRW.Enumerate(curProc.Infos.Pid, _itemId, _buffDico)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As String In _itemId
                If Not (_dico.ContainsKey(z)) Then
                    ' New item
                    Dim lm As cProcessMemRW.MEMORY_BASIC_INFORMATION = _buffDico.Item(z)
                    Call addToLog("Memory region created (0x" & lm.BaseAddress.ToString("x") & " -- Size : " & GetFormatedSize(lm.RegionSize) & " -- Type : " & lm.lType.ToString & " -- Protection : " & lm.Protect.ToString & ")", LogItemType.MemoryItem, True)
                End If
            Next
        End If

        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As String In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Dim lm As cProcessMemRW.MEMORY_BASIC_INFORMATION = _dico.Item(z)
                    Call addToLog("Memory region deleted (0x" & lm.BaseAddress.ToString("x") & " -- Size : " & GetFormatedSize(lm.RegionSize) & " -- Type : " & lm.lType.ToString & " -- Protection : " & lm.Protect.ToString & ")", LogItemType.MemoryItem, False)
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

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
        'TODO_
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
        frmMain.saveDial.Filter = "Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save log"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    Dim stream As New System.IO.StreamWriter(s, False)
                    'Dim x As Integer = 0
                    For Each cm As ListViewItem In Me.lvLog.Items
                        stream.WriteLine(cm.Text & vbTab & vbTab & cm.SubItems(1).Text)
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

    Private _logCaptureMask As LogItemType = LogItemType.AllItems
    Private _logDisplayMask As LogItemType = LogItemType.AllItems
    Private _autoScroll As Boolean = False
    Private _logDico As New Dictionary(Of Integer, LogItem)
    Public Enum LogItemType As Integer
        ModuleItem = 1
        ThreadItem = 2
        ServiceItem = 4
        WindowItem = 8
        HandleItem = 16
        MemoryItem = 32
        NetworkItem = 64
        DeletedItems = 128
        CreatedItems = 256
        AllItems = ModuleItem Or ThreadItem Or ServiceItem Or WindowItem Or HandleItem _
            Or MemoryItem Or NetworkItem Or DeletedItems Or CreatedItems
    End Enum
    Public Structure LogItem
        Public _date As Date
        Public _desc As String
        Public _type As LogItemType
        Public _created As Boolean
        Public Sub New(ByVal aDesc As String, ByVal aType As LogItemType, _
                       ByVal created As Boolean)
            _date = Date.Now
            _desc = aDesc
            _type = aType
            _created = created
        End Sub
    End Structure
    Public Property LogCaptureMask() As LogItemType
        Get
            Return _logCaptureMask
        End Get
        Set(ByVal value As LogItemType)
            _logCaptureMask = value
        End Set
    End Property
    Public Property LogDisplayMask() As LogItemType
        Get
            Return _logDisplayMask
        End Get
        Set(ByVal value As LogItemType)
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

    Private Sub addToLog(ByVal s As String, ByVal _type As LogItemType, _
                         ByVal created As Boolean)
        Static _number As Integer = 0

        If (_type And _logCaptureMask) = _type Then
            _number += 1
            _logDico.Add(_number, New LogItem(s, _type, created))

            If (_type And _logDisplayMask) = _type Then
                ' Here we add the item to lv
                Dim b As Boolean
                If created Then
                    b = (_logDisplayMask And LogItemType.CreatedItems) = LogItemType.CreatedItems
                Else
                    b = (_logDisplayMask And LogItemType.DeletedItems) = LogItemType.DeletedItems
                End If
                If b Then
                    Dim it As New ListViewItem(Date.Now.ToLongDateString & " -- " & Date.Now.ToLongTimeString)
                    it.SubItems.Add(_type.ToString)
                    it.SubItems.Add(s)
                    Me.lvLog.Items.Add(it)
                End If
            End If
        End If
    End Sub

    Private Sub cmdLogOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLogOptions.Click
        Dim frm As New frmLogOptions
        frm.LogCaptureMask = _logCaptureMask
        frm.LogDisplayMask = _logDisplayMask
        frm.Form = Me
        frm._autoScroll.Checked = _autoScroll

        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            ' Redisplay items
            Me.lvLog.BeginUpdate()
            Me.lvLog.Items.Clear()
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, LogItem) In _logDico
                Dim b As Boolean = False
                If pair.Value._created Then
                    b = ((_logDisplayMask And LogItemType.CreatedItems) = LogItemType.CreatedItems)
                Else
                    b = ((_logDisplayMask And LogItemType.DeletedItems) = LogItemType.DeletedItems)
                End If
                If ((pair.Value._type And _logDisplayMask) = pair.Value._type) AndAlso b Then
                    Dim it As New ListViewItem(pair.Value._date.ToLongDateString & " -- " & pair.Value._date.ToLongTimeString)
                    it.SubItems.Add(pair.Value._type.ToString)
                    it.SubItems.Add(pair.Value._desc)
                    Me.lvLog.Items.Add(it)
                End If
            Next
            Me.lvLog.EndUpdate()
        End If
    End Sub

    Private Sub ToolStripMenuItem33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem33.Click
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
            'For Each _val As Long In curProc.GetHistory(_g.Name)
            '    _g.AddValue(_val)
            'Next'TODO_
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
    'TODO_
    'Private Sub curProc_Refreshed() Handles curProc.Refreshed
    '    ' curProc has been refreshed, so we have to add a value to the different
    '    ' graphs in containerHistory
    '    For Each ct As Control In Me.containerHistory.Panel2.Controls
    '        If TypeOf ct Is Graph2 Then
    '            Dim _tempG As Graph2 = CType(ct, Graph2)
    '            _tempG.AddValue(curProc.GetInformationNumerical(ct.Name))
    '            _tempG.Refresh()
    '        End If
    '    Next
    'End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call Me.tabProcess_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cmdKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKill.Click
        If frmMain.Pref.warnDangerous Then
            If MsgBox("Are you sure you want to kill this process ?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "Dangerous action") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If
        Call curProc.Kill()
    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        If frmMain.Pref.warnDangerous Then
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

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
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
        theConnection.CopyFromInstance(frmMain.theConnection)
        Me.lvThreads.ConnectionObj = theConnection
        Me.lvModules.ConnectionObj = theConnection
        Me.lvHandles.ConnectionObj = theConnection
        Me.lvProcMem.ConnectionObj = theConnection
        Me.lvPrivileges.ConnectionObj = theConnection
        Me.lvProcEnv.ConnectionObj = theConnection
        Me.lvProcServices.ConnectionObj = theConnection
        Me.lvWindows.ConnectionObj = theConnection
        Me.lvProcNetwork.ConnectionObj = theConnection
        theConnection.Connect()
    End Sub

    Private Sub theConnection_Connected() Handles theConnection.Connected
        '
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        '
    End Sub

    Private Sub lvProcServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcServices.MouseUp
        Me.OpenDirToolStripMenuItem35.Enabled = _local
        Me.FilePropToolStripMenuItem34.Enabled = _local
        Me.FileDetailsToolStripMenuItem.Enabled = _local
        If lvProcServices.SelectedItems.Count = 1 Then
            Dim cSe As cService = Me.lvProcServices.GetSelectedItem
            Dim start As API.SERVICE_START_TYPE = cSe.Infos.StartType
            Dim state As API.SERVICE_STATE = cSe.Infos.State
            Dim acc As API.SERVICE_ACCEPT = cSe.Infos.AcceptedControl

            Me.PauseToolStripMenuItem14.Text = IIf(state = API.SERVICE_STATE.Running, "Pause", "Resume").ToString
            PauseToolStripMenuItem14.Enabled = (acc And API.SERVICE_ACCEPT.PauseContinue) = API.SERVICE_ACCEPT.PauseContinue
            Me.StartToolStripMenuItem17.Enabled = Not (state = API.SERVICE_STATE.Running)
            Me.StopToolStripMenuItem16.Enabled = (acc And API.SERVICE_ACCEPT.Stop) = API.SERVICE_ACCEPT.Stop
            ShutdownShutdownToolStripMenuItem.Enabled = (acc And API.SERVICE_ACCEPT.PreShutdown) = API.SERVICE_ACCEPT.PreShutdown

            Me.DisabledToolStripMenuItem19.Checked = (start = API.SERVICE_START_TYPE.StartDisabled)
            DisabledToolStripMenuItem19.Enabled = Not (DisabledToolStripMenuItem19.Checked)
            Me.AutoToolStripMenuItem20.Checked = (start = API.SERVICE_START_TYPE.AutoStart)
            AutoToolStripMenuItem20.Enabled = Not (AutoToolStripMenuItem20.Checked)
            Me.DemandToolStripMenuItem21.Checked = (start = API.SERVICE_START_TYPE.DemandStart)
            DemandToolStripMenuItem21.Enabled = Not (DemandToolStripMenuItem21.Checked)
        ElseIf lvProcServices.SelectedItems.Count > 1 Then
            PauseToolStripMenuItem14.Text = "Pause"
            PauseToolStripMenuItem14.Enabled = True
            StartToolStripMenuItem17.Enabled = True
            StopToolStripMenuItem16.Enabled = True
            ShutdownShutdownToolStripMenuItem.Enabled = True
            DisabledToolStripMenuItem19.Checked = True
            DisabledToolStripMenuItem19.Enabled = True
            AutoToolStripMenuItem20.Checked = True
            AutoToolStripMenuItem20.Enabled = True
            DemandToolStripMenuItem21.Checked = True
            DemandToolStripMenuItem21.Enabled = True
        End If
    End Sub

    Private Sub PauseToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseToolStripMenuItem14.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            If it.Infos.State = API.SERVICE_STATE.Running Then
                it.PauseService()
            Else
                it.ResumeService()
            End If
        Next
    End Sub

    Private Sub StopToolStripMenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem16.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.StopService()
        Next
    End Sub

    Private Sub ShutdownShutdownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutdownShutdownToolStripMenuItem.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.ShutDownService()
        Next
    End Sub

    Private Sub StartToolStripMenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem17.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.StartService()
        Next
    End Sub

    Private Sub ReanalyzeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReanalyzeToolStripMenuItem.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.Refresh()
        Next
    End Sub

    Private Sub DisabledToolStripMenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisabledToolStripMenuItem19.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(API.SERVICE_START_TYPE.StartDisabled)
        Next
    End Sub

    Private Sub AutoToolStripMenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoToolStripMenuItem20.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(API.SERVICE_START_TYPE.AutoStart)
        Next
    End Sub

    Private Sub DemandToolStripMenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DemandToolStripMenuItem21.Click
        For Each it As cService In Me.lvProcServices.GetSelectedItems
            it.SetServiceStartType(API.SERVICE_START_TYPE.DemandStart)
        Next
    End Sub

    Private Sub FilePropToolStripMenuItem34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FilePropToolStripMenuItem34.Click
        Dim s As String = vbNullString
        For Each it As cService In Me.lvProcServices.GetSelectedItems
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
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            If IO.File.Exists(.MsgResult2) Then _
                                cFile.ShowFileProperty(.MsgResult2, Me.Handle)
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub OpenDirToolStripMenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDirToolStripMenuItem35.Click
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

    Private Sub FileDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileDetailsToolStripMenuItem.Click
        If Me.lvProcServices.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvProcServices.GetSelectedItem.GetInformation("ImagePath")
            If IO.File.Exists(s) = False Then
                s = cFile.IntelligentPathRetrieving2(s)
            End If
            If IO.File.Exists(s) Then
                frmMain.DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub GoogleSearchToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleSearchToolStripMenuItem1.Click
        Dim it As ListViewItem
        For Each it In Me.lvProcServices.SelectedItems
            My.Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
    End Sub
End Class