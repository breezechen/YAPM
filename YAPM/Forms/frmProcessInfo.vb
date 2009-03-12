Option Strict On

Imports System.Runtime.InteropServices

Public Class frmProcessInfo

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function
    Private Declare Function GetTickCount Lib "kernel32" () As Integer

    Private curProc As cProcess
    Private Const NO_INFO_RETRIEVED As String = "N/A"
    Private m_SortingColumn As ColumnHeader
    Private WithEvents _AsyncDownload As cAsyncProcInfoDownload
    Private _asyncDlThread As Threading.Thread

    ' String search (in process image/memory) private attributes
    Private _stringSearchImmediateStop As Boolean   ' Set to true to stop listing of string in process
    Private __sRes() As String
    Private __lRes() As Integer
    Private cRW As cProcessMemRW

    Private NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Private DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)

    Private Const SIZE_FOR_STRING As Integer = 4


    ' Refresh current tab
    Private Sub refreshProcessTab()

        If curProc Is Nothing Then Exit Sub

        ' General informations
        Select Case Me.tabProcess.SelectedTab.Text

            Case "Modules"
                Call ShowModules()

            Case "Threads"
                Call ShowThreads()

            Case "Windows"
                Call ShowWindows()

            Case "Handles"
                Call ShowHandles()

            Case "Memory"
                Call ShowRegions()

            Case "Network"
                Call ShowNetwork()

            Case "Services"
                Try
                    Call ShowServices()
                Catch ex As Exception
                    '
                End Try

            Case "Strings"
                Call getProcString(curProc)

            Case "General"
                Me.txtProcessPath.Text = curProc.Path
                Me.txtProcessId.Text = CStr(curProc.Pid)
                Me.txtParentProcess.Text = CStr(curProc.ParentProcessId) & " -- " & curProc.ParentProcessName
                Me.txtProcessStarted.Text = curProc.StartTime.ToLongDateString & " -- " & curProc.StartTime.ToLongTimeString
                Me.txtProcessUser.Text = curProc.UserName
                Me.txtCommandLine.Text = curProc.CommandLine
                Dim tMain As System.Diagnostics.ProcessModule = curProc.MainModule
                If tMain IsNot Nothing Then
                    Me.txtImageVersion.Text = tMain.FileVersionInfo.FileVersion
                    Me.lblCopyright.Text = tMain.FileVersionInfo.LegalCopyright
                    Me.lblDescription.Text = tMain.FileVersionInfo.FileDescription
                Else
                    Me.txtImageVersion.Text = NO_INFO_RETRIEVED
                    Me.lblCopyright.Text = NO_INFO_RETRIEVED
                    Me.lblDescription.Text = NO_INFO_RETRIEVED
                End If


            Case "Statistics"

                Me.lblProcOther.Text = GetFormatedSize(curProc.GetIOvalues.OtherOperationCount)
                Me.lblProcOtherBytes.Text = GetFormatedSize(curProc.GetIOvalues.OtherTransferCount)
                Me.lblProcReads.Text = GetFormatedSize(curProc.GetIOvalues.ReadOperationCount)
                Me.lblProcReadBytes.Text = GetFormatedSize(curProc.GetIOvalues.ReadTransferCount)
                Me.lblProcWriteBytes.Text = GetFormatedSize(curProc.GetIOvalues.WriteTransferCount)
                Me.lblProcWrites.Text = GetFormatedSize(curProc.GetIOvalues.WriteOperationCount)
                Me.lblGDIcount.Text = CStr(curProc.GDIObjectsCount)
                Me.lblUserObjectsCount.Text = CStr(curProc.UserObjectsCount)
                Me.lblAverageCPUusage.Text = curProc.GetInformation("AverageCpuUsage")

                Dim mem As cProcess.PROCESS_MEMORY_COUNTERS = curProc.MemoryInfos
                Me.lblHandles.Text = CStr(curProc.HandleCount)
                Dim ts As Date = curProc.KernelTime
                Dim s As String = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblKernelTime.Text = s
                Me.lblPageFaults.Text = CStr(mem.PageFaultCount)
                Me.lblPageFileUsage.Text = GetFormatedSize(mem.PagefileUsage)
                Me.lblPeakPageFileUsage.Text = GetFormatedSize(mem.PeakPagefileUsage)
                Me.lblPeakWorkingSet.Text = GetFormatedSize(mem.PeakWorkingSetSize)
                ts = curProc.ProcessorTime
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblTotalTime.Text = s
                ts = curProc.UserTime
                s = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                Me.lblUserTime.Text = s
                Me.lblPriority.Text = curProc.PriorityClass.ToString
                Me.lblWorkingSet.Text = GetFormatedSize(mem.WorkingSetSize)
                Me.lblQuotaNPP.Text = GetFormatedSize(mem.QuotaNonPagedPoolUsage)
                Me.lblQuotaPNPP.Text = GetFormatedSize(mem.QuotaPeakNonPagedPoolUsage)
                Me.lblQuotaPP.Text = GetFormatedSize(mem.QuotaPagedPoolUsage)
                Me.lblQuotaPPP.Text = GetFormatedSize(mem.QuotaPeakPagedPoolUsage)


            Case "Environment"

                Me.lvProcEnv.Items.Clear()
                Dim cVar() As String = Nothing
                Dim cVal() As String = Nothing
                If curProc.GetEnvironmentVariables(cVar, cVal) > 0 Then
                    For x As Integer = 0 To cVar.Length - 1
                        If cVar(x).Length > 0 Then
                            Dim itpr As New ListViewItem(cVar(x))
                            itpr.SubItems.Add(CStr(cVal(x)))
                            Me.lvProcEnv.Items.Add(itpr)
                        End If
                    Next
                End If

            Case "Token"

                ' Privileges
                Dim cPriv As New cPrivileges(curProc.Pid)
                Dim lPriv() As cPrivileges.PrivilegeInfo = cPriv.GetPrivilegesList

                Me.lvPrivileges.Items.Clear()
                If lPriv.Length > 0 Then
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
                End If

            Case "Informations"

                ' Description
                Try
                    Dim pmc As cProcess.PROCESS_MEMORY_COUNTERS = curProc.MemoryInfos
                    Dim pid As Integer = curProc.Pid
                    Dim s As String = ""
                    s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                    s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b File properties\b0\par"
                    s = s & "\tab File name :\tab\tab\tab " & curProc.Name & "\par"
                    s = s & "\tab Path :\tab\tab\tab\tab " & Replace(curProc.Path, "\", "\\") & "\par"
                    Dim mainModule As System.Diagnostics.ProcessModule = curProc.MainModule
                    If mainModule IsNot Nothing Then
                        s = s & "\tab Description :\tab\tab\tab " & mainModule.FileVersionInfo.FileDescription & "\par"
                        s = s & "\tab Company name :\tab\tab\tab " & mainModule.FileVersionInfo.CompanyName & "\par"
                        s = s & "\tab Version :\tab\tab\tab " & mainModule.FileVersionInfo.FileVersion & "\par"
                        s = s & "\tab Copyright :\tab\tab\tab " & mainModule.FileVersionInfo.LegalCopyright & "\par"
                    End If
                    s = s & "\par"
                    s = s & "  \b Process description\b0\par"
                    s = s & "\tab PID :\tab\tab\tab\tab " & CStr(curProc.Pid) & "\par"
                    s = s & "\tab Start time :\tab\tab\tab " & curProc.StartTime.ToLongDateString & " -- " & curProc.StartTime.ToLongTimeString & "\par"
                    s = s & "\tab Priority :\tab\tab\tab\tab " & curProc.PriorityClass.ToString & "\par"
                    s = s & "\tab User :\tab\tab\tab\tab " & curProc.UserName & "\par"
                    Dim ts As Date = curProc.ProcessorTime
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

                        Dim ipi As InternetProcessInfo = mdlInternet.GetInternetInfos(curProc.Name)

                        s = s & "\tab Security risk (0-5) :\tab\tab " & CStr(ipi._Risk) & "\par"
                        s = s & "\tab Description :\tab\tab\tab " & Replace$(ipi._Description, vbNewLine, "\par") & "\par"
                    End If

                    If chkModules.Checked Then
                        ' Retrieve modules
                        s = s & "\par"
                        s = s & "  \b Loaded modules\b0\par"
                        Dim m As ProcessModule
                        Dim mdl As ProcessModuleCollection = curProc.Modules
                        s = s & "\tab " & CStr(mdl.Count) & " modules loaded" & "\par"
                        For Each m In mdl
                            s = s & "\tab " & Replace(m.FileVersionInfo.FileName, "\", "\\") & "\par"
                        Next

                        ' Retrieve threads infos
                        s = s & "\par"
                        s = s & "  \b Threads\b0\par"
                        Dim pt As ProcessThread
                        Dim thr As System.Diagnostics.ProcessThreadCollection = curProc.Threads
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
                        frmMain.handles_Renamed.Refresh()
                        For i = 0 To frmMain.handles_Renamed.Count - 1
                            With frmMain.handles_Renamed
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

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mybase.Load

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
        frmMain.SetToolTip(Me.cmdProcSearchL, "Previous result (F2 on listview also works)")
        frmMain.SetToolTip(Me.cmdProcSearchR, "Next result (F3 on listview also works)")

        ' Refresh infos
        Me.graphCPU.ClearValue()
        Me.graphIO.ClearValue()
        Me.graphMemory.ClearValue()

        ' Icons
        If pctBigIcon.Image Is Nothing Then
            Try
                pctBigIcon.Image = GetIcon(curProc.Path, False).ToBitmap
                pctSmallIcon.Image = GetIcon(curProc.Path, True).ToBitmap
            Catch ex As Exception
                pctSmallIcon.Image = Me.imgProcess.Images("noicon")
                pctBigIcon.Image = Me.imgMain.Images("noicon32")
            End Try
        End If

        ' Show all items
        'Call ShowModules()
        'Call ShowThreads()
        'Call ShowWindows()
        Call ShowRegions()
        'Call ShowNetwork()

        Call refreshProcessTab()

    End Sub

    ' Get process to monitor
    Public Sub SetProcess(ByRef process As cProcess)
        curProc = process
        Me.Text = curProc.Name & " (" & CStr(curProc.Pid) & ")"

        ' Verify file
        Try
            Dim bVer As Boolean = Security.WinTrust.WinTrust.VerifyEmbeddedSignature(curProc.Path)
            If bVer Then
                gpProcGeneralFile.Text = "Image file (successfully verified)"
            Else
                gpProcGeneralFile.Text = "Image file (not verified)"
            End If
        Catch ex As Exception
            '
        End Try
    End Sub

    Private Sub timerProcPerf_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcPerf.Tick
        Dim z As Double = curProc.CpuPercentageUsage
        Dim z2 As Double = curProc.AverageCpuUsage
        If Double.IsNegativeInfinity(z) Then z = 0
        Me.graphCPU.Add2Values(z * 100, z2 * 100)
        z = curProc.MemoryInfos.WorkingSetSize / 2147483648 * 100
        Me.graphMemory.AddValue(z)
        Me.graphIO.AddValue(curProc.GetIOvalues.ReadTransferCount)
        Me.graphCPU.Refresh()
        Me.graphIO.Refresh()
        Me.graphMemory.Refresh()


        ' Refresh informations about process
        If Not (Me.tabProcess.SelectedTab.Text = "Informations" Or _
            Me.tabProcess.SelectedTab.Text = "Token" Or _
            Me.tabProcess.SelectedTab.Text = "Strings" Or _
            Me.tabProcess.SelectedTab.Text = "Environment") Then _
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
        Me.Text = curProc.Name & " (" & curProc.Pid.ToString & ")"
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
            cRW = New cProcessMemRW(curProc.Pid)
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

        Dim file As String = cp.Path

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
        If curProc.Path <> NO_INFO_RETRIEVED Then
            cFile.OpenDirectory(curProc.Path)
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
            it.ShowHexEditor()
        Next
    End Sub

    Private Sub cmdShowFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowFileDetails.Click
        Dim s As String = curProc.Path
        If IO.File.Exists(s) Then
            frmMain.DisplayDetailsFile(s)
        End If
    End Sub

    Private Sub cmdShowFileProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowFileProperties.Click
        ' File properties for selected process
        If IO.File.Exists(curProc.Path) Then
            cFile.ShowFileProperty(curProc.Path)
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
        Dim peb As Integer = curProc.PEBAddress
        For Each reg As cMemRegion In Me.lvProcMem.GetAllItems
            If reg.BaseAddress <= peb AndAlso peb <= (reg.BaseAddress + reg.RegionSize) Then
                Dim frm As New frmHexEditor
                Dim regio As New MemoryHexEditor.control.MemoryRegion(reg.BaseAddress, reg.RegionSize)
                frm.SetPidAndRegion(curProc.Pid, regio)
                frm._hex.NavigateToOffset(peb)
                frm.Show()
                Exit For
            End If
        Next
    End Sub

    Private Sub ToolStripMenuItem44_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem44.Click
        Dim cPriv As New cPrivileges(curProc.Pid)
        Dim it As ListViewItem
        For Each it In Me.lvPrivileges.SelectedItems
            cPriv.Privilege(it.Text) = cPrivileges.PrivilegeStatus.PRIVILEGE_ENABLED
            it.SubItems(1).Text = cPrivileges.PrivilegeStatusToString(cPriv.Privilege(it.Text))
            it.BackColor = cPrivileges.GetColorFromStatus(cPriv.Privilege(it.Text))
        Next
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        Dim cPriv As New cPrivileges(curProc.Pid)
        Dim it As ListViewItem
        For Each it In Me.lvPrivileges.SelectedItems
            cPriv.Privilege(it.Text) = cPrivileges.PrivilegeStatus.PRIVILEGE_DISBALED
            it.SubItems(1).Text = cPrivileges.PrivilegeStatusToString(cPriv.Privilege(it.Text))
            it.BackColor = cPrivileges.GetColorFromStatus(cPriv.Privilege(it.Text))
        Next
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        Dim cPriv As New cPrivileges(curProc.Pid)
        Dim it As ListViewItem
        For Each it In Me.lvPrivileges.SelectedItems
            cPriv.Privilege(it.Text) = cPrivileges.PrivilegeStatus.PRIVILEGE_REMOVED
            it.SubItems(1).Text = cPrivileges.PrivilegeStatusToString(cPriv.Privilege(it.Text))
            it.BackColor = cPrivileges.GetColorFromStatus(cPriv.Privilege(it.Text))
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
                If cp.Name = it.Name Then
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

        _AsyncDownload = New cAsyncProcInfoDownload(curProc.Name)

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
        Me.lvProcServices.ProcessId = curProc.Pid
        Me.lvProcServices.UpdateItems()

    End Sub

    ' Show modules
    Public Sub ShowModules()

        lvModules.ProcessId = curProc.Pid
        lvModules.UpdateItems()

    End Sub

    ' Show threads
    Public Sub ShowThreads()

        Dim pid() As Integer
        ReDim pid(0)
        pid(0) = curProc.Pid
        Me.lvThreads.ProcessId = pid
        Me.lvThreads.UpdateItems()

    End Sub

    ' Show network connections
    Public Sub ShowNetwork()

        Dim pid(0) As Integer
        pid(0) = curProc.Pid
        Me.lvProcNetwork.ShowAllPid = False
        Me.lvProcNetwork.ProcessId = pid
        Me.lvProcNetwork.UpdateItems()

    End Sub

    ' Show memory regions
    Public Sub ShowRegions()

        Me.lvProcMem.ProcessId = curProc.Pid
        Me.lvProcMem.UpdateItems()

    End Sub

    ' Show threads
    Public Sub ShowWindows(Optional ByVal allWindows As Boolean = True)

        Dim pid(0) As Integer
        pid(0) = curProc.Pid
        Me.lvWindows.ProcessId = pid
        Me.lvWindows.ShowAllPid = False
        Me.lvWindows.ShowUnNamed = Me.ShowUnnamedWindowsToolStripMenuItem.Checked
        Me.lvWindows.UpdateItems()

    End Sub

    ' Display handles of process
    Private Sub ShowHandles()

        Me.lvHandles.ShowUnNamed = ShowUnnamedHandlesToolStripMenuItem.Checked
        Dim pids(0) As Integer
        pids(0) = curProc.Pid
        Me.lvHandles.ProcessId = pids
        Me.lvHandles.UpdateItems()
       
    End Sub

    Private Sub ToolStripMenuItem36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem36.Click
        For Each it As cModule In Me.lvModules.GetSelectedItems
            it.UnloadModule()
        Next
    End Sub

    Private Sub ShowFileDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowFileDetailsToolStripMenuItem.Click
        If Me.lvModules.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvModules.GetSelectedItem.FilePath
            If IO.File.Exists(s) Then
                frmMain.DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub GoogleSearchToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoogleSearchToolStripMenuItem2.Click
        Dim it As ListViewItem
        For Each it In Me.lvModules.SelectedItems
            My.Application.DoEvents()
            Call SearchInternet(it.Text)
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
            it.Priority = cThread.ThreadPriority.Idle
        Next
    End Sub

    Private Sub LowestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LowestToolStripMenuItem.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.Priority = cThread.ThreadPriority.Lowest
        Next
    End Sub

    Private Sub ToolStripMenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem28.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.Priority = cThread.ThreadPriority.BelowNormal
        Next
    End Sub

    Private Sub ToolStripMenuItem29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem29.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.Priority = cThread.ThreadPriority.Normal
        Next
    End Sub

    Private Sub ToolStripMenuItem30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem30.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.Priority = cThread.ThreadPriority.AboveNormal
        Next
    End Sub

    Private Sub ToolStripMenuItem31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem31.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.Priority = cThread.ThreadPriority.Highest
        Next
    End Sub

    Private Sub ToolStripMenuItem32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem32.Click
        For Each it As cThread In Me.lvThreads.GetSelectedItems
            it.Priority = cThread.ThreadPriority.Critical
        Next
    End Sub

    Private Sub lvThreads_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvThreads.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As cThread.ThreadPriority = cThread.ThreadPriority.Unknow

            If Me.lvThreads.SelectedItems.Count > 0 Then
                p = Me.lvThreads.GetSelectedItem.Priority
            End If
            Me.ToolStripMenuItem27.Checked = (p = cThread.ThreadPriority.Idle)
            Me.LowestToolStripMenuItem.Checked = (p = cThread.ThreadPriority.Lowest)
            Me.ToolStripMenuItem28.Checked = (p = cThread.ThreadPriority.BelowNormal)
            Me.ToolStripMenuItem29.Checked = (p = cThread.ThreadPriority.Normal)
            Me.ToolStripMenuItem30.Checked = (p = cThread.ThreadPriority.AboveNormal)
            Me.ToolStripMenuItem31.Checked = (p = cThread.ThreadPriority.Highest)
            Me.ToolStripMenuItem32.Checked = (p = cThread.ThreadPriority.Critical)
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
        Dim r As cWindow.RECT

        If Me.lvWindows.SelectedItems.Count > 0 Then

            Dim frm As New frmWindowPosition
            With frm
                .SetCurrentPositions(Me.lvWindows.GetSelectedItem.Positions)

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

            If reg.BaseAddress <= add AndAlso add <= (reg.BaseAddress + reg.RegionSize) Then
                Dim frm As New frmHexEditor
                Dim regio As New MemoryHexEditor.control.MemoryRegion(reg.BaseAddress, reg.RegionSize)
                frm.SetPidAndRegion(curProc.Pid, regio)
                frm._hex.NavigateToOffset(CInt((add - regio.BeginningAddress) / 16))
                frm.Show()
                Exit For
            End If
        Next

    End Sub

    Private Sub ViewMemoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewMemoryToolStripMenuItem.Click

        For Each it As cModule In Me.lvModules.GetSelectedItems
            Dim add As Integer = it.BaseAddress

            For Each reg As cMemRegion In Me.lvProcMem.GetAllItems

                If reg.BaseAddress <= add AndAlso add < (reg.BaseAddress + reg.RegionSize) Then
                    Dim frm As New frmHexEditor
                    Dim regio As New MemoryHexEditor.control.MemoryRegion(reg.BaseAddress, reg.RegionSize)
                    frm.SetPidAndRegion(curProc.Pid, regio)
                    'frm._hex.NavigateToOffset(CInt((add - regio.BeginningAddress) / 16) - 1)
                    frm.Show()
                    Exit For
                End If
            Next

        Next

    End Sub

    Private Sub ToolStripMenuItem22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem22.Click
        For Each ch As cHandle In Me.lvHandles.GetSelectedItems
            Call frmMain.handles_Renamed.CloseProcessLocalHandle(ch.ProcessID, ch.Handle)
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

        If _autoScroll Then
            Me.lvLog.Items(Me.lvLog.Items.Count - 1).EnsureVisible()
        End If

        Call ChangeCaption()
        Trace.WriteLine("Log updated in " & (GetTickCount - _i).ToString & " ms")
    End Sub

    ' Check if there are changes about network
    Private Sub _CheckNetwork()

        Static _dico As New Dictionary(Of String, cNetwork.LightConnection)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cNetwork.LightConnection)

        Dim _itemId() As String
        ReDim _itemId(0)
        Dim _pid(0) As Integer
        _pid(0) = curProc.Pid
        Call cNetwork.Enumerate(False, _pid, _itemId, _buffDico)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As String In _itemId
                If Not (_dico.ContainsKey(z)) Then
                    ' New item
                    Dim lm As cNetwork.LightConnection = _buffDico.Item(z)
                    If lm.remote IsNot Nothing Then
                        Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- Remote : " & lm.remote.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, True)
                    Else
                        Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, True)
                    End If
                End If
            Next
        End If


        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As String In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Dim lm As cNetwork.LightConnection = _dico.Item(z)
                    If lm.remote IsNot Nothing Then
                        Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- Remote : " & lm.remote.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, False)
                    Else
                        Call addToLog("Network connection created (" & lm.dwType.ToString & " -- Local : " & lm.local.ToString & " -- State: " & lm.dwState.ToString & ")", LogItemType.NetworkItem, False)
                    End If
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

    End Sub

    ' Check if there are changes about memory areas
    Private Sub _CheckMemory()

        Static _dico As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)

        Dim _itemId() As String
        ReDim _itemId(0)
        Call cProcessMemRW.Enumerate(curProc.Pid, _itemId, _buffDico)

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

        Static _dico As New Dictionary(Of String, cHandle.LightHandle)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cHandle.LightHandle)

        Dim _itemId() As String
        ReDim _itemId(0)
        Dim _pid(0) As Integer
        _pid(0) = curProc.Pid
        Call cHandle.Enumerate(_pid, True, _itemId, _buffDico)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As String In _itemId
                If Not (_dico.ContainsKey(z)) Then
                    ' New item
                    Dim lh As cHandle.LightHandle = _buffDico.Item(z)
                    Call addToLog("Handle created (" & lh.handle.ToString & " -- " & lh.type & " -- " & lh.name & ")", LogItemType.HandleItem, True)
                End If
            Next
        End If

        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As String In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Dim lh As cHandle.LightHandle = _dico.Item(z)
                    Call addToLog("Handle created (" & lh.handle.ToString & " -- " & lh.type & " -- " & lh.name & ")", LogItemType.HandleItem, False)
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

    End Sub

    ' Check if there are changes about threads
    Private Sub _CheckThreads()

        Static _dico As New Dictionary(Of String, cThread.LightThread)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cThread.LightThread)

        Dim _itemId() As String
        ReDim _itemId(0)
        Dim _pid(0) As Integer
        _pid(0) = curProc.Pid
        Call cThread.Enumerate(_pid, _itemId, _buffDico)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As String In _itemId
                If Not (_dico.ContainsKey(z)) Then
                    ' New item
                    Call addToLog("Thread created (" & _buffDico.Item(z).t.Id.ToString & ")", LogItemType.ThreadItem, True)
                End If
            Next
        End If

        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As String In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Call addToLog("Thread deleted (" & _dico.Item(z).t.Id.ToString & ")", LogItemType.ThreadItem, False)
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

    End Sub

    ' Check if there are changes about services
    Private Sub _CheckServices()

        Static _enum As New cServEnum
        Static _dico As New Dictionary(Of String, cService.LightService)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cService.LightService)
        Dim _buffDico2 As New Dictionary(Of String, cService.LightService)  ' Useless

        Dim _itemId() As String
        ReDim _itemId(0)

        Call _enum.EnumerateApi(curProc.Pid, _itemId, _buffDico, _buffDico2)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As String In _itemId
                If Not (_dico.ContainsKey(z)) Then
                    ' New item
                    Call addToLog("Service started (" & _buffDico.Item(z).name & ")", LogItemType.ServiceItem, True)
                End If
            Next
        End If

        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As String In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Call addToLog("Service stopped (" & _dico.Item(z).name & ")", LogItemType.ServiceItem, False)
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

    End Sub

    ' Check if there are changes about modules
    Private Sub _CheckModules()

        Static _dico As New Dictionary(Of String, cModule.MODULEENTRY32)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cModule.MODULEENTRY32)

        Dim _itemId() As String
        ReDim _itemId(0)
        Call cModule.Enumerate(curProc.Pid, _itemId, _buffDico)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As String In _itemId
                If Not (_dico.ContainsKey(z)) Then
                    ' New item
                    Call addToLog("Module loaded (" & _buffDico.Item(z).szModule & ")", LogItemType.ModuleItem, True)
                End If
            Next
        End If

        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As String In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Call addToLog("Module unloaded (" & _dico.Item(z).szModule & ")", LogItemType.ModuleItem, False)
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

    End Sub

    ' Check if there are changes about windows
    Private Sub _CheckWindows()

        Static _dico As New Dictionary(Of String, cWindow.LightWindow)
        Static _first As Boolean = True
        Dim _buffDico As New Dictionary(Of String, cWindow.LightWindow)

        Dim _itemId() As Integer
        ReDim _itemId(0)
        Dim _pid(0) As Integer
        _pid(0) = curProc.Pid
        Call cWindow.Enumerate(True, _pid, _itemId, _buffDico)

        If _first Then
            _dico = _buffDico
            _first = False
        End If

        ' Check if there are new items
        If (_logCaptureMask And LogItemType.CreatedItems) = LogItemType.CreatedItems Then
            For Each z As Integer In _itemId
                If Not (_dico.ContainsKey(z.ToString)) Then
                    ' New item
                    Dim _light As cWindow.LightWindow = _buffDico.Item(z.ToString)
                    Call addToLog("Window created (" & _light.handle.ToString & "  --  " & cWindow.GetCaption(_light.handle) & ")", LogItemType.WindowItem, True)
                End If
            Next
        End If

        ' Check if there are deleted items
        If (_logCaptureMask And LogItemType.DeletedItems) = LogItemType.DeletedItems Then
            For Each z As Integer In _dico.Keys
                If Array.IndexOf(_itemId, z) < 0 Then
                    ' Deleted item
                    Call addToLog("Windows deleted (" & _dico.Item(z.ToString).handle.ToString & ")", LogItemType.WindowItem, False)
                End If
            Next
        End If

        ' Save dico
        _dico = _buffDico

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
End Class