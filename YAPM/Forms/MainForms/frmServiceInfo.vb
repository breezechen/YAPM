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

Public Class frmServiceInfo

    Private WithEvents curServ As cService
    Private WithEvents _AsyncDownload As cAsyncProcInfoDownload
    Private _asyncDlThread As Threading.Thread

    Private WithEvents theConnection As cConnection
    Private _local As Boolean = True
    Private _notWMI As Boolean
    Private _notSnapshotMode As Boolean = True


    ' Refresh current tab
    Private Sub refreshServiceTab()

        If curServ Is Nothing Then Exit Sub

        Select Case Me.tabProcess.SelectedTab.Text

            Case "General - 1"
                If curServ.Infos.FileInfo IsNot Nothing Then
                    Me.txtImageVersion.Text = curServ.Infos.FileInfo.FileVersion
                    Me.lblCopyright.Text = curServ.Infos.FileInfo.LegalCopyright
                    Me.lblDescription.Text = curServ.Infos.FileInfo.FileDescription
                Else
                    Me.txtImageVersion.Text = NO_INFO_RETRIEVED
                    Me.lblCopyright.Text = NO_INFO_RETRIEVED
                    Me.lblDescription.Text = NO_INFO_RETRIEVED
                End If
                Me.txtName.Text = curServ.Infos.DisplayName
                If Me.curServ.Infos.ProcessId > 0 Then
                    Me.txtProcess.Text = curServ.Infos.ProcessName & " -- " & curServ.Infos.ProcessId
                Else
                    Me.txtProcess.Text = "Not started"
                End If
                Me.txtCommand.Text = curServ.GetInformation("ImagePath")
                Me.txtServicePath.Text = GetPathFromCommand(Me.txtCommand.Text)
                Me.txtStartType.Text = curServ.Infos.StartType.ToString
                Me.txtState.Text = curServ.Infos.State.ToString
                Me.txtType.Text = curServ.Infos.ServiceType.ToString
                Me.cmdGoProcess.Enabled = (Me.curServ.Infos.ProcessId > 0)
                Me.cmdPause.Enabled = _notSnapshotMode AndAlso ((Me.curServ.Infos.AcceptedControl And Native.Api.NativeEnums.ServiceAccept.PauseContinue) = Native.Api.NativeEnums.ServiceAccept.PauseContinue)
                Me.cmdStop.Enabled = _notSnapshotMode AndAlso ((Me.curServ.Infos.AcceptedControl And Native.Api.NativeEnums.ServiceAccept.Stop) = Native.Api.NativeEnums.ServiceAccept.Stop)
                Me.cmdStart.Enabled = _notSnapshotMode AndAlso (Me.curServ.Infos.State = Native.Api.NativeEnums.ServiceState.Stopped)


            Case "General - 2"
                Me.txtCheckPoint.Text = curServ.Infos.CheckPoint.ToString
                Me.txtDiagnosticMessageFile.Text = curServ.Infos.DiagnosticMessageFile
                Me.txtErrorControl.Text = curServ.Infos.ErrorControl.ToString
                Me.txtObjectName.Text = curServ.Infos.ObjectName
                Me.txtLoadOrderGroup.Text = curServ.Infos.LoadOrderGroup
                Me.txtServiceFlags.Text = curServ.Infos.ServiceFlags.ToString
                Me.txtServiceSpecificExitCode.Text = curServ.Infos.ServiceSpecificExitCode.ToString
                Me.txtServiceStartName.Text = curServ.Infos.ServiceStartName
                Me.txtTagID.Text = curServ.Infos.TagID.ToString
                Me.txtWaitHint.Text = curServ.Infos.WaitHint.ToString
                Me.txtWin32ExitCode.Text = curServ.Infos.Win32ExitCode.ToString
                Me.rtbDescription.Text = Me.curServ.Infos.Description


            Case "Dependencies"
                With tv
                    .RootService = curServ.Infos.Name
                    .InfosToGet = cServDepConnection.DependenciesToget.DependenciesOfMe
                    .UpdateItems()
                End With
                With tv2
                    .RootService = curServ.Infos.Name
                    .InfosToGet = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
                    .UpdateItems()
                End With

            Case "Informations"

                ' Description
                Dim s As String = vbNullString
                Dim description As String = vbNullString
                Dim diagnosticsMessageFile As String = curServ.Infos.DiagnosticMessageFile
                Dim group As String = curServ.Infos.LoadOrderGroup
                Dim objectName As String = curServ.Infos.ObjectName
                Dim sp As String = curServ.GetInformation("ImagePath")

                ' OK it's not the best way to retrive the description...
                ' (if @ -> extract string to retrieve description)
                Dim sTemp As String = curServ.Infos.Description
                If InStr(sTemp, "@", CompareMethod.Binary) > 0 Then
                    description = Native.Objects.File.GetResourceStringFromFile(sTemp)
                Else
                    description = sTemp
                End If
                description = Replace(curServ.Infos.Description, "\", "\\")


                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Service properties\b0\par"
                s = s & "\tab Name :\tab\tab\tab " & curServ.Infos.Name & "\par"
                s = s & "\tab Common name :\tab\tab " & curServ.Infos.DisplayName & "\par"
                If Len(sp) > 0 Then s = s & "\tab Path :\tab\tab\tab " & Replace(curServ.GetInformation("ImagePath"), "\", "\\") & "\par"
                If Len(description) > 0 Then s = s & "\tab Description :\tab\tab " & description & "\par"
                If Len(group) > 0 Then s = s & "\tab Group :\tab\tab\tab " & group & "\par"
                If Len(objectName) > 0 Then s = s & "\tab ObjectName :\tab\tab " & objectName & "\par"
                If Len(diagnosticsMessageFile) > 0 Then s = s & "\tab DiagnosticsMessageFile :\tab\tab " & diagnosticsMessageFile & "\par"
                s = s & "\tab State :\tab\tab\tab " & curServ.Infos.State.ToString & "\par"
                s = s & "\tab Startup :\tab\tab " & curServ.Infos.StartType.ToString & "\par"
                If curServ.Infos.ProcessId > 0 Then s = s & "\tab Owner process :\tab\tab " & curServ.Infos.ProcessId & "-- " & cProcess.GetProcessName(curServ.Infos.ProcessId) & "\par"
                s = s & "\tab Service type :\tab\tab " & curServ.Infos.ServiceType.ToString & "\par"

                s = s & "\tab AcceptedControl :\tab " & curServ.Infos.AcceptedControl.ToString & "\par"
                s = s & "\tab CheckPoint :\tab\tab " & curServ.Infos.CheckPoint.ToString & "\par"
                s = s & "\tab ServiceFlags :\tab\tab " & curServ.Infos.ServiceFlags.ToString & "\par"
                s = s & "\tab Win32ExitCode :\tab\tab " & curServ.Infos.Win32ExitCode.ToString & "\par"
                s = s & "\tab WaitHint :\tab\tab " & curServ.Infos.WaitHint.ToString & "\par"
                s = s & "\tab TagID :\tab\tab\tab " & curServ.Infos.TagID.ToString & "\par"
                s = s & "\tab ServiceSpecificExitCode :\tab " & curServ.Infos.ServiceSpecificExitCode.ToString & "\par"

                s = s & "}"

                rtb.Rtf = s
        End Select
    End Sub

    Private Sub frmServiceInfo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Save position & size
        Pref.SaveFormPositionAndSize(Me, "PSfrmServiceInfo")
    End Sub

    Private Sub frmServiceInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        ' Some tooltips
        SetToolTip(Me.txtServicePath, "Path of the image file")
        SetToolTip(Me.txtCheckPoint, "Check point value during when service is starting, stopping, pausing...")
        SetToolTip(Me.txtDiagnosticMessageFile, "Diagnostic message file")
        SetToolTip(Me.txtErrorControl, "Severity error is start failed")
        SetToolTip(Me.txtImageVersion, "Version of the image file")
        SetToolTip(Me.txtLoadOrderGroup, "The name of the load ordering group to which this service belongs")
        SetToolTip(Me.txtName, "Name of the service")
        SetToolTip(Me.txtObjectName, "Object name")
        SetToolTip(Me.txtProcess, "Associated process, if any")
        SetToolTip(Me.txtServiceFlags, "Flags (0 or SERVICE_RUNS_IN_SYSTEM_PROCESS, which means that service" & vbNewLine & "is part of the system and should not be terminated")
        SetToolTip(Me.txtServiceSpecificExitCode, "The service-specific error code that the service returns when an error occurs while the service is starting or stopping")
        SetToolTip(Me.txtServiceStartName, "Name of the account that the service process will be logged on as when it runs.")
        SetToolTip(Me.txtStartType, "Type of start")
        SetToolTip(Me.txtState, "State (running...)")
        SetToolTip(Me.txtTagID, "A unique tag value for this service in the group specified by the lpLoadOrderGroup parameter")
        SetToolTip(Me.txtType, "Type of the service")
        SetToolTip(Me.txtWaitHint, "The estimated time required for a pending start, stop, pause, or continue operation, in milliseconds")
        SetToolTip(Me.txtWin32ExitCode, "The error code that the service uses to report an error that occurs when it is starting or stopping")
        SetToolTip(Me.cmdGetOnlineInfos, "Get online description")
        SetToolTip(Me.cmdInfosToClipB, "Copy services informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style)")
        SetToolTip(Me.cmdGoProcess, "Open details of the associated process")
        SetToolTip(Me.cmdOpenDirectory, "Open direcotry of the image file")
        SetToolTip(Me.cmdInspectExe, "Show dependencies")
        SetToolTip(Me.cmdPause, "Pause service")
        SetToolTip(Me.cmdRefresh, "Refresh informations")
        SetToolTip(Me.cmdResume, "Resume service")
        SetToolTip(Me.cmdServDet1, "Open details of the selected service")
        SetToolTip(Me.cmdServDet2, "Open details of the selected service")
        SetToolTip(Me.cmdSetStartType, "Change start type of the service")
        SetToolTip(Me.cmdShowFileDetails, "Show details of image file")
        SetToolTip(Me.cmdShowFileProperties, "Show file properties")
        SetToolTip(Me.cmdStart, "Start service")
        SetToolTip(Me.cmdStop, "Stop service")
        SetToolTip(Me.cbStart, "Service start type")
        SetToolTip(Me.cmdDelete, "Delete the service")

        Select Case My.Settings.ServSelectedTab
            Case "General - 1"
                Me.tabProcess.SelectedTab = Me.TabPage1
            Case "General - 2"
                Me.tabProcess.SelectedTab = Me.TabPage2
            Case "Dependencies"
                Me.tabProcess.SelectedTab = Me.tabDep
            Case "Informations"
                Me.tabProcess.SelectedTab = Me.TabPage6
        End Select

        ' Init position & size
        Pref.LoadFormPositionAndSize(Me, "PSfrmServiceInfo")

        ' Icons
        Me.tv.ImageList = _frmMain.imgServices
        Me.tv2.ImageList = _frmMain.imgServices
        If cService.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            If pctBigIcon.Image Is Nothing Then
                Try
                    pctBigIcon.Image = GetIcon(Me.txtServicePath.Text, False).ToBitmap
                    pctSmallIcon.Image = GetIcon(Me.txtServicePath.Text, True).ToBitmap
                Catch ex As Exception
                    pctSmallIcon.Image = My.Resources.exe16
                    pctBigIcon.Image = My.Resources.exe32
                End Try
            End If
        Else
            pctSmallIcon.Image = My.Resources.exe16
            pctBigIcon.Image = My.Resources.exe32
        End If

        Call Connect()
        Call refreshServiceTab()

        If My.Settings.AutomaticInternetInfos Then
            Call cmdGetOnlineInfos_Click(Nothing, Nothing)
        End If

    End Sub

    ' Get process to monitor
    Public Sub SetService(ByRef service As cService)

        curServ = service

        Me.Text = curServ.Infos.Name & " (" & curServ.Infos.DisplayName & ")"

        _local = (cService.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cService.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)
        _notSnapshotMode = (cService.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.SnapshotFile)

        Me.cmdShowFileDetails.Enabled = _local
        Me.cmdShowFileProperties.Enabled = _local
        Me.cmdOpenDirectory.Enabled = _local
        Me.cmdInspectExe.Enabled = _local

        Me.txtServicePath.Text = GetPathFromCommand(curServ.GetInformation("ImagePath"))
        Me.cbStart.Text = curServ.Infos.StartType.ToString

        Me.cmdDelete.Enabled = _notSnapshotMode AndAlso _notWMI
        Me.cmdPause.Enabled = _notSnapshotMode
        Me.cmdResume.Enabled = _notSnapshotMode
        Me.cmdSetStartType.Enabled = _notSnapshotMode
        Me.cbStart.Enabled = _notSnapshotMode
        Me.cmdStop.Enabled = _notSnapshotMode

        Me.Timer.Enabled = True ' _local

        If _local Then
            Try
                If IO.File.Exists(Me.txtServicePath.Text) AndAlso curServ.Infos.FileInfo Is Nothing Then
                    curServ.Infos.FileInfo = New SerializableFileVersionInfo(FileVersionInfo.GetVersionInfo(Me.txtServicePath.Text))
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If

        ' Verify file
        If _local Then
            Try
                Dim bVer As Boolean = Security.WinTrust.WinTrust.VerifyEmbeddedSignature(Me.txtServicePath.Text)
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
    End Sub

    ' Change caption
    Private Sub ChangeCaption()
        Me.Text = curServ.Infos.Name & " (" & curServ.Infos.DisplayName & ")"
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

    Private Sub cmdOpenDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpenDirectory.Click
        ' Open directory of selected service
        If Me.txtServicePath.Text <> NO_INFO_RETRIEVED Then
            cFile.OpenDirectory(Me.txtServicePath.Text)
        End If
    End Sub

    Private Sub pctBigIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctBigIcon.MouseDown
        Me.MenuItemCopyBig.Enabled = (Me.pctBigIcon.Image IsNot Nothing)
    End Sub

    Private Sub pctSmallIcon_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctSmallIcon.MouseDown
        Me.MenuItemCopySmall.Enabled = (Me.pctSmallIcon.Image IsNot Nothing)
    End Sub

    Private Sub tabProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabProcess.SelectedIndexChanged
        Call Me.refreshServiceTab()
        Call ChangeCaption()
        My.Settings.ServSelectedTab = Me.tabProcess.SelectedTab.Text
    End Sub

    Private Sub rtb_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.TextChanged
        Me.cmdInfosToClipB.Enabled = (Me.rtb.TextLength > 0)
    End Sub

    Private Sub cmdGetOnlineInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetOnlineInfos.Click
        If _asyncDlThread IsNot Nothing Then
            ' Already trying to get infos
            Exit Sub
        End If
        _AsyncDownload = New cAsyncProcInfoDownload(curServ.Infos.Name)

        ' Start async download of infos
        _asyncDlThread = New Threading.Thread(AddressOf _AsyncDownload.BeginDownload)
        With _asyncDlThread
            .IsBackground = True
            .Priority = Threading.ThreadPriority.Lowest
            .Start()
        End With
    End Sub

    ' Here we finished to download informations from internet
    Private _asyncInfoRes As cAsyncProcInfoDownload.InternetProcessInfo
    Private _asyncDownloadDone As Boolean = False
    Private Sub _AsyncDownload_GotInformations(ByRef result As cAsyncProcInfoDownload.InternetProcessInfo) Handles _AsyncDownload.GotInformations
        _asyncInfoRes = result
        _asyncDownloadDone = True
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call Me.tabProcess_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    ' Connection
    Public Sub Connect()
        ' Connect providers
        Try
            theConnection = Program.Connection
            Me.tv.ConnectionObj = theConnection
            Me.tv2.ConnectionObj = theConnection
            theConnection.Connect()
            Me.Timer.Interval = CInt(1000 * Program.Connection.RefreshmentCoefficient)
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

    Private Sub tv_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv.AfterExpand
        tv.SelectedNode = e.Node.Nodes(0)
    End Sub

    Private Sub servDep_Connected() Handles tv.Connected
        If Me.tabProcess.SelectedTab.Text = "Dependencies" Then
            With tv
                .RootService = curServ.Infos.Name
                .InfosToGet = cServDepConnection.DependenciesToget.DependenciesOfMe
                .UpdateItems()
            End With
        End If
    End Sub

    Private Sub tv2_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tv2.AfterExpand
        tv2.SelectedNode = e.Node.Nodes(0)
    End Sub

    Private Sub servDep2_Connected() Handles tv2.Connected
        If Me.tabProcess.SelectedTab.Text = "Dependencies" Then
            With tv2
                .RootService = curServ.Infos.Name
                .InfosToGet = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
                .UpdateItems()
            End With
        End If
    End Sub

    Private Sub cmdShowFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFileDetails.Click
        Dim s As String = Me.txtServicePath.Text
        If IO.File.Exists(s) Then
            DisplayDetailsFile(s)
        End If
    End Sub

    Private Sub cmdShowFileProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFileProperties.Click
        ' File properties for selected process
        If IO.File.Exists(Me.txtServicePath.Text) Then
            cFile.ShowFileProperty(Me.txtServicePath.Text, Me.Handle)
        End If
    End Sub

    Private Sub cmdPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPause.Click
        curServ.PauseService()
    End Sub

    Private Sub cmdResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResume.Click
        curServ.ResumeService()
    End Sub

    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        curServ.StartService()
    End Sub

    Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
        If WarnDangerousAction("Are you sure you want to stop this service ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        curServ.StopService()
    End Sub

    Private Sub cmdSetStartType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetStartType.Click
        If WarnDangerousAction("Are you sure you want to change the type of start ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        Select Case cbStart.Text
            Case "BootStart"
                curServ.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.BootStart)
            Case "SystemStart"
                curServ.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.SystemStart)
            Case "AutoStart"
                curServ.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.AutoStart)
            Case "DemandStart"
                curServ.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.DemandStart)
            Case "StartDisabled"
                curServ.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.StartDisabled)
        End Select
    End Sub

    Private Sub cmdGoProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoProcess.Click
        Dim _t As cProcess = ProcessProvider.GetProcessById(curServ.Infos.ProcessId)
        If _t IsNot Nothing Then
            Dim frm As New frmProcessInfo
            frm.SetProcess(_t)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        End If
    End Sub

    Private Sub txtServicePath_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServicePath.TextChanged
        Dim s As String = Me.txtServicePath.Text
        Dim b As Boolean = False
        b = (Me._local AndAlso System.IO.File.Exists(s))
        Me.cmdShowFileDetails.Enabled = b
        Me.cmdShowFileProperties.Enabled = b
        Me.cmdOpenDirectory.Enabled = b
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ' Refresh informations about process
        If Not (Me.tabProcess.SelectedTab.Text = "Informations" Or _
                Me.tabProcess.SelectedTab.Text = "Dependencies") Then _
            Call Me.refreshServiceTab()

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

    Private Sub cmdServDet1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdServDet1.Click
        If Me.tv2.SelectedNode IsNot Nothing Then
            Dim s As String = CType(Me.tv2.SelectedNode.Tag, serviceDependenciesList.servTag).name
            Dim it As cService = cService.GetServiceByName(s)
            If it IsNot Nothing Then
                Dim frm As New frmServiceInfo
                frm.SetService(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        End If
    End Sub

    Private Sub cmdServDet2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdServDet2.Click
        If Me.tv.SelectedNode IsNot Nothing Then
            Dim s As String = CType(Me.tv.SelectedNode.Tag, serviceDependenciesList.servTag).name
            Dim it As cService = cService.GetServiceByName(s)
            If it IsNot Nothing Then
                Dim frm As New frmServiceInfo
                frm.SetService(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        End If
    End Sub

    Private Sub cmdInspectExe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInspectExe.Click
        If System.IO.File.Exists(Me.curServ.GetInformation("ImagePath")) Then
            Dim _depForm As New frmDepViewerMain
            With _depForm
                .OpenReferences(Me.curServ.GetInformation("ImagePath"))
                .HideOpenMenu()
                .TopMost = _frmMain.TopMost
                .Show()
            End With
        End If
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyBig.Click
        My.Computer.Clipboard.SetImage(Me.pctBigIcon.Image)
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

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopySmall.Click
        My.Computer.Clipboard.SetImage(Me.pctSmallIcon.Image)
    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If _notWMI Then
            If WarnDangerousAction("Are you sure you want to delete this service ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
            curServ.DeleteService()
        End If
    End Sub
End Class