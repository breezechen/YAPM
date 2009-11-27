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
Imports Microsoft.Win32

Public Module Program

    ' Exit code when leave YAPM in ModeRequestReplaceTaskMgr
    Public Enum RequestReplaceTaskMgrResult As Byte
        ReplaceSuccess
        ReplaceFail
        NotReplaceSuccess
        NotReplaceFail
    End Enum

    ' Constants for command line of YAPM
    Public Const PARAM_DO_NOT_CHECK_PREV_INSTANCE As String = " -donotcheckprevinstance"
    Public Const PARAM_CHANGE_TASKMGR As String = " -reptaskmgr"

    ' Represent options passed with command line
    Public Class ProgramParameters

        ' Available parameters
        Private isServerMode As Boolean = False
        Private remPort As Integer = 8081
        Private isAutoConnectMode As Boolean = False
        Private isHidden As Boolean = False
        Private requestReplaceTaskMgr As Boolean = False
        Private replaceTaskMgrValue As Boolean = False
        Private ssFileModeValue As String
        Private oneInstance As Boolean = True
        Private useDriver As Boolean = True
        Private serviceMode As Boolean = False
        Private ssFileMode As Boolean = False
        Public ReadOnly Property ModeServer() As Boolean
            Get
                Return isServerMode
            End Get
        End Property
        Public ReadOnly Property ModeServerService() As Boolean
            Get
                Return serviceMode
            End Get
        End Property
        Public ReadOnly Property AutoConnect() As Boolean
            Get
                Return isAutoConnectMode
            End Get
        End Property
        Public ReadOnly Property ModeHidden() As Boolean
            Get
                Return isHidden
            End Get
        End Property
        Public ReadOnly Property RemotePort() As Integer
            Get
                Return remPort
            End Get
        End Property
        Public ReadOnly Property ModeRequestReplaceTaskMgr() As Boolean
            Get
                Return requestReplaceTaskMgr
            End Get
        End Property
        Public ReadOnly Property ValueReplaceTaskMgr() As Boolean
            Get
                Return replaceTaskMgrValue
            End Get
        End Property
        Public ReadOnly Property ValueCreateSSFile() As String
            Get
                Return ssFileModeValue
            End Get
        End Property
        Public ReadOnly Property OnlyOneInstance() As Boolean
            Get
                Return oneInstance
            End Get
        End Property
        Public ReadOnly Property UseKernelDriver() As Boolean
            Get
                Return useDriver
            End Get
        End Property
        Public ReadOnly Property ModeSnapshotFileCreation() As Boolean
            Get
                Return ssFileMode
            End Get
        End Property
        Public Sub New(ByRef parameters As String())
            If parameters Is Nothing Then
                Exit Sub
            End If
            For i As Integer = 0 To parameters.Length - 1
                If parameters(i).ToUpperInvariant = "-SERVER" Then
                    isServerMode = True
                ElseIf parameters(i).ToUpperInvariant = "-AUTOCONNECT" Then
                    isAutoConnectMode = True
                ElseIf parameters(i).ToUpperInvariant = "-HIDE" Then
                    isHidden = True
                ElseIf parameters(i).ToUpperInvariant = "-PORT" Then
                    If parameters.Length - 1 >= i + 1 Then
                        remPort = CInt(Val(parameters(i + 1)))
                    End If
                ElseIf parameters(i).ToUpperInvariant = "-REPTASKMGR" Then
                    If parameters.Length - 1 >= i + 1 Then
                        replaceTaskMgrValue = CBool(Val(parameters(i + 1)))
                        requestReplaceTaskMgr = True
                    End If
                ElseIf parameters(i).ToUpperInvariant = "-DONOTCHECKPREVINSTANCE" Then
                    oneInstance = False
                ElseIf parameters(i).ToUpperInvariant = "-NODRIVER" Then
                    useDriver = False
                ElseIf parameters(i).ToUpperInvariant = "-SSFILE" Then
                    If parameters.Length - 1 >= i + 1 Then
                        ssFileModeValue = parameters(i + 1)
                        ssFileMode = True
                    End If
                ElseIf parameters(i).ToUpperInvariant = "-SERVERSERVICE" Then
                    serviceMode = True
                End If
            Next
        End Sub
    End Class



    Public _frmMain As frmMain
    Public _frmServer As frmServer

    Private WithEvents _updater As cUpdate
    Private _progParameters As ProgramParameters
    Private WithEvents theConnection As cConnection
    Private _systemInfo As cSystemInfo
    Private _hotkeys As cHotkeys
    Private _pref As Pref
    Private _log As cLog
    Private _isVistaOrAbove As Boolean
    Private _isAdmin As Boolean
    Private _trayIcon As cTrayIcon
    Private _ConnectionForm As frmConnection
    Private _time As Integer
    Private _isElevated As Boolean
    Private _mustCloseWithCloseButton As Boolean = False

    Public ReadOnly Property Parameters() As ProgramParameters
        Get
            Return _progParameters
        End Get
    End Property
    Public ReadOnly Property ElapsedTime() As Integer
        Get
            Return Native.Api.Win32.GetElapsedTime - _time
        End Get
    End Property
    Public ReadOnly Property Connection() As cConnection
        Get
            Return theConnection
        End Get
    End Property
    Public ReadOnly Property SystemInfo() As cSystemInfo
        Get
            Return _systemInfo
        End Get
    End Property
    Public ReadOnly Property Hotkeys() As cHotkeys
        Get
            Return _hotkeys
        End Get
    End Property
    Public ReadOnly Property Preferences() As Pref
        Get
            Return _pref
        End Get
    End Property
    Public ReadOnly Property Log() As cLog
        Get
            Return _log
        End Get
    End Property
    Public ReadOnly Property IsAdministrator() As Boolean
        Get
            Return _isAdmin
        End Get
    End Property
    Public ReadOnly Property TrayIcon() As cTrayIcon
        Get
            Return _trayIcon
        End Get
    End Property
    Public ReadOnly Property ConnectionForm() As frmConnection
        Get
            Return _ConnectionForm
        End Get
    End Property
    Public ReadOnly Property IsElevated() As Boolean
        Get
            Return _isElevated
        End Get
    End Property
    Public ReadOnly Property Updater() As cUpdate
        Get
            Return _updater
        End Get
    End Property
    Public Property MustCloseWithCloseButton() As Boolean
        Get
            Return _mustCloseWithCloseButton
        End Get
        Set(ByVal value As Boolean)
            _mustCloseWithCloseButton = value
        End Set
    End Property


    Public Const HELP_PATH_INTERNET As String = "http://yaprocmon.sourceforge.net/help_static.html"
    Public Const HELP_PATH_DD As String = "\Help\help_static.html"
    Public Const NO_INFO_RETRIEVED As String = "N/A"

    Public NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Public DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)
    Public PROCESSOR_COUNT As Integer



    Sub Main()


        ' ======= Some basic initialisations
        ' /!\ Looks like Comctl32 v6 could not be initialized before
        ' a form is loaded. So as Comctl32 v5 can not display VistaDialogBoxes,
        ' all error messages before instanciation of a form should use classical style.
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)    ' Use GDI, not GDI+



        ' ======= Save time of start
        _time = Native.Api.Win32.GetElapsedTime



        ' ======= Check if framework is 2.0 or above
        If cEnvironment.IsFramework2OrAbove = False Then
            Misc.ShowError(".Net Framework 2.0 must be installed.", True)
            Application.Exit()
        End If



        ' ======= Other init
        _isVistaOrAbove = cEnvironment.IsWindowsVistaOrAbove
        _isAdmin = cEnvironment.IsAdmin
        _isElevated = (cEnvironment.GetElevationType = Native.Api.Enums.ElevationType.Full)



        ' ======= Read parameters
        _progParameters = New ProgramParameters(Environment.GetCommandLineArgs)



        ' ======= We replace Taskmgr if needed. This will end YAPM
        If _progParameters.ModeRequestReplaceTaskMgr Then
            Call safeReplaceTaskMgr(_progParameters.ValueReplaceTaskMgr)
        End If


        ' ======= We create a snapshot file
        If _progParameters.ModeSnapshotFileCreation Then

            ' Request debug privilege (if possible)
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.DebugPrivilege)

            ' New connection
            theConnection = New cConnection

            ' Used for service enumeration. Snapshot enumeration of services
            ' need a cServiceConnection, retrieved as a property in
            ' _frmMain.lvServices
            _frmMain = New frmMain
            _frmMain.lvServices.ConnectionObj = theConnection

            ' This initializes the Handle Enumeration Class (needed to enumerate
            ' handles)
            Native.Objects.Handle.HandleEnumerationClass = _
                    New Native.Objects.HandleEnumeration(_progParameters.UseKernelDriver And _
                                                 cEnvironment.Is32Bits)

            ' Connect to the local machine
            theConnection.Connect()

            Call createSSFile(_progParameters.ValueCreateSSFile)
            Exit Sub
        End If


        ' ======= Close application if there is a previous instance of YAPM running
        If _progParameters.ModeServerService = False Then

            If _progParameters.OnlyOneInstance And cEnvironment.IsAlreadyRunning Then
                Exit Sub
            End If



            ' ======= Set handler for exceptions
            AddHandler Application.ThreadException, AddressOf MYThreadHandler
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf MYExnHandler
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)



            ' ======= Parse port files
            Call cNetwork.ParsePortTextFile()



            ' ======= Enable some privileges
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.DebugPrivilege)
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.ShutdownPrivilege)



            ' ======= Instanciate all classes

            ' Common classes
            theConnection = New cConnection     ' The cConnection instance of the connection
            _systemInfo = New cSystemInfo       ' System informations
            _ConnectionForm = New frmConnection(theConnection)


            ' FOR NOW, we use kernel only on 32 bits systems...
            Native.Objects.Handle.HandleEnumerationClass = _
                        New Native.Objects.HandleEnumeration(_progParameters.UseKernelDriver And _
                                                             cEnvironment.Is32Bits)


            ' Classes for client only
            If _progParameters.ModeServer = False Then
                _pref = New Pref                    ' Preferences
                _hotkeys = New cHotkeys             ' Hotkeys
                _log = New cLog                     ' Log instance
                _trayIcon = New cTrayIcon(2)        ' Tray icons
                _frmMain = New frmMain              ' Main form
                _updater = New cUpdate              ' Updater class
            Else
                _frmMain = New frmMain              ' Main form
                _frmServer = New frmServer          ' Server form (server mode)
            End If



            ' ======= Load preferences
            If My.Settings.ShouldUpgrade Then
                Try
                    ' Try to update settings from a previous version of YAPM
                    My.Settings.Upgrade()
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
                My.Settings.ShouldUpgrade = False
                My.Settings.Save()
            End If
            If _progParameters.ModeServer = False Then
                Try
                    If My.Settings.FirstTime Then
                        Misc.ShowMsg("This is the first time YAPM starts", _
                                     "Please read this :", _
                                     Pref.MessageFirstStartOfYAPM, _
                                     MessageBoxButtons.OK, _
                                     TaskDialogIcon.ShieldWarning, , True)
                        My.Settings.FirstTime = False
                        Program.Preferences.Save()
                    End If
                    Program.Preferences.Apply()
                    cProcess.BuffSize = My.Settings.HistorySize
                Catch ex As Exception
                    ' Preference file corrupted/missing
                    Misc.ShowMsg("Startup error", "Failed to load preferences.", "Preference file is missing or corrupted and will be now recreated.", MessageBoxButtons.OK, TaskDialogIcon.ShieldError, True)
                    Program.Preferences.SetDefault()
                End Try
            End If



            ' ======= Read hotkeys & state based actions from XML files
            If _progParameters.ModeServer = False Then
                Call frmHotkeys.readHotkeysFromXML()
                'Call frmBasedStateAction.readStateBasedActionFromXML()
            End If



            ' ======= Show main form & start application
            If _progParameters.ModeServer Then
                Application.Run(_frmServer)
            Else
                Application.Run(_frmMain)
            End If

        Else
            ' Then YAPM is a service !

            ' ======= Instanciate all classes

            ' Common classes
            theConnection = New cConnection     ' The cConnection instance of the connection
            _systemInfo = New cSystemInfo       ' System informations
            _ConnectionForm = New frmConnection(theConnection)

            ' FOR NOW, we use kernel only on 32 bits systems...
            Native.Objects.Handle.HandleEnumerationClass = _
                        New Native.Objects.HandleEnumeration(False)

            Dim service As New YAPMLauncherService.InteractiveProcess
            ServiceProcess.ServiceBase.Run(service)
        End If

    End Sub

    ' Handler for exceptions
    Private Sub MYExnHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Dim ex As Exception
        ex = CType(e.ExceptionObject, Exception)
        Console.WriteLine(ex.StackTrace)
        Dim t As New frmError(ex)
        t.TopMost = True
#If RELEASE_MODE = 1 Then
        t.ShowDialog()
#End If
    End Sub
    Private Sub MYThreadHandler(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        Console.WriteLine(e.Exception.StackTrace)
        Dim t As New frmError(e.Exception)
        t.TopMost = True
#If RELEASE_MODE = 1 Then
        t.ShowDialog()
#End If
    End Sub




    ' Exit application
    Public Sub ExitYAPM()

        ' Save settings
        Pref.SaveListViewColumns(_frmMain.lvTask, "COLmain_task")
        Pref.SaveListViewColumns(_frmMain.lvServices, "COLmain_service")
        Pref.SaveListViewColumns(_frmMain.lvProcess, "COLmain_process")
        Pref.SaveListViewColumns(_frmMain.lvNetwork, "COLmain_network")

        My.Settings.Save()

        ' Uninstall driver
        Try
            Native.Objects.Handle.HandleEnumerationClass.Close()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

        ' Close forms & exit
        Program.MustCloseWithCloseButton = True
        If _frmMain IsNot Nothing Then
            _frmMain.Close()
        End If
        Application.Exit()

    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        ' Clear list of processes (used to get ParentProcess name)
        Call cProcess.ClearProcessDico()
    End Sub

    ' Create a snapshot file
    Private Function createSSFile(ByVal file As String) As Boolean

        Try
            Dim res As String = Nothing

            ' Create empty snapshot file
            Dim snap As New cSnapshot

            ' Get options
            Dim options As Native.Api.Enums.SnapshotObject = Native.Api.Enums.SnapshotObject.All

            ' Build it
            If snap.CreateSnapshot(Program.Connection, options, res) = False Then
                ' Failed
                'Misc.ShowMsg("Snapshot creation", "Could not build snapshot.", res, MessageBoxButtons.OK, TaskDialogIcon.Error)
                Return False
            End If

            ' Save it
            If snap.SaveSnapshot(file, res) = False Then
                ' Failed
                'Misc.ShowMsg("Snapshot creation", "Could not save snapshot.", res, MessageBoxButtons.OK, TaskDialogIcon.Error)
                Return False
            End If

            Return True

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            Return False
        End Try

    End Function

    ' Replace taskmgr
    ' This function will end YAPM with a specific ExitCode (if fail or not)
    Private Sub safeReplaceTaskMgr(ByVal value As Boolean)
        Try
            Dim regKey As RegistryKey
            regKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", True)

            If value Then
                regKey.CreateSubKey("taskmgr.exe").SetValue("debugger", Application.ExecutablePath)
            Else
                regKey.DeleteSubKey("taskmgr.exe")
            End If

            ' Success
            Dim res As RequestReplaceTaskMgrResult
            If value Then
                res = RequestReplaceTaskMgrResult.ReplaceSuccess
            Else
                res = RequestReplaceTaskMgrResult.NotReplaceSuccess
            End If
            Call Native.Api.NativeFunctions.ExitProcess(res)

        Catch ex As Exception
            ' Could not set key -> failed
            Misc.ShowDebugError(ex)
            Dim res As RequestReplaceTaskMgrResult
            If value Then
                res = RequestReplaceTaskMgrResult.ReplaceFail
            Else
                res = RequestReplaceTaskMgrResult.NotReplaceFail
            End If
            Call Native.Api.NativeFunctions.ExitProcess(res)

        End Try
    End Sub

    Private Sub _updater_FailedToCheckVersion(ByVal silent As Boolean, ByVal msg As String) Handles _updater.FailedToCheckVersion
        ' Failed to check update
        If silent Then
            ' Silent mode -> only displays a tooltip
            If _frmMain IsNot Nothing AndAlso _frmMain.Tray IsNot Nothing Then
                With _frmMain.Tray
                    .BalloonTipText = msg
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "Could not check if YAPM us ip to date."
                    .ShowBalloonTip(3000)
                End With
            End If
        Else
            _frmMain.Invoke(New frmMain.FailedToCheckUpDateNotification(AddressOf impFailedToCheckUpDateNotification), msg)
        End If
    End Sub

    Private Sub _updater_NewVersionAvailable(ByVal silent As Boolean, ByVal release As cUpdate.NewReleaseInfos) Handles _updater.NewVersionAvailable
        ' A new version of YAPM is available
        If silent Then
            ' Silent mode -> only displays a tooltip
            If _frmMain IsNot Nothing AndAlso _frmMain.Tray IsNot Nothing Then
                With _frmMain.Tray
                    .BalloonTipText = release.Infos
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "A new version of YAPM is available !"
                    .ShowBalloonTip(3000)
                End With
            End If
        Else
            _frmMain.Invoke(New frmMain.NewUpdateAvailableNotification(AddressOf impNewUpdateAvailableNotification), release)
        End If
    End Sub

    Private Sub _updater_ProgramUpToDate(ByVal silent As Boolean) Handles _updater.ProgramUpToDate
        ' YAPM is up to date (no new version available)
        If silent Then
            ' Silent mode -> only displays a tooltip
            If _frmMain IsNot Nothing AndAlso _frmMain.Tray IsNot Nothing Then
                With _frmMain.Tray
                    .BalloonTipText = "YAPM is up to date !"
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "Now new version of YAPM is available."
                    .ShowBalloonTip(3000)
                End With
            End If
        Else
            _frmMain.Invoke(New frmMain.NoNewUpdateAvailableNotification(AddressOf impNoNewUpdateAvailableNotification))
        End If
    End Sub

    ' Called when a new update is available
    ' It's here cause of thread safety
    Public Sub impNewUpdateAvailableNotification(ByVal release As cUpdate.NewReleaseInfos)
        Dim frm As New frmNewVersionAvailable(release)
        frm.ShowDialog()
    End Sub

    ' Called when no new update is available
    ' It's here cause of thread safety
    Public Sub impNoNewUpdateAvailableNotification()
        Common.Misc.ShowMsg("YAPM update", _
                          "YAPM is up to date !", _
                          "The current version (" & My.Application.Info.Version.ToString & ") is the latest available for download.", _
                          MessageBoxButtons.OK, _
                          TaskDialogIcon.ShieldOk)
    End Sub

    ' Called when failed to check is YAPM is up to date
    Public Sub impFailedToCheckUpDateNotification(ByVal msg As String)
        Common.Misc.ShowMsg("YAPM update", _
                                "Could not check if YAPM is up to date.", _
                                msg, _
                                MessageBoxButtons.OK, _
                                TaskDialogIcon.ShieldError)
    End Sub
End Module
