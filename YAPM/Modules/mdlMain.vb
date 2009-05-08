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

Public Module Program

    Public _frmMain As frmMain
    Private theConnection As cConnection
    Private _systemInfo As cSystemInfo
    Private _hotkeys As cHotkeys
    Private _pref As Pref
    Private _log As cLog
    Private _isVista As Boolean
    Private _isAdmin As Boolean
    Private _trayIcon As cTrayIcon
    Private _ConnectionForm As frmConnection
    Private _time As Integer

    Public ReadOnly Property ElapsedTime() As Integer
        Get
            Return API.GetTickCount - _time
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
    Public ReadOnly Property IsWindowsVista() As Boolean
        Get
            Return _isVista
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


    ' NOT UP TO DATE : There is a Config.xml file for for each user, but in IDE the file should be located in Config dir
#If Not (CONFIG_INTO_APPDATA) Then
    Public PREF_PATH As String = My.Application.Info.DirectoryPath & "\config.xml"
#Else
    Public PREF_PATH As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\config.xml"
#End If

    Public Const HELP_PATH As String = "http://yaprocmon.sourceforge.net/help.html"
    Public Const NO_INFO_RETRIEVED As String = "N/A"

    Public NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Public DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)
    Public PROCESSOR_COUNT As Integer

    Sub Main()

        ' Save time of start
        _time = API.GetTickCount


        ' Close application if there is a previous instance of YAPM running
        If IsAlreadyRunning() Then
            Exit Sub
        End If


        ' Some basic initialisations
        Application.EnableVisualStyles()


        ' Set handler for exceptions
        AddHandler Application.ThreadException, AddressOf MYThreadHandler
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf MYExnHandler
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)


        ' Instanciate all classes
        _pref = New Pref                    ' Preferences
        theConnection = New cConnection     ' The cConnection instance of the connection
        _systemInfo = New cSystemInfo       ' System informations
        _hotkeys = New cHotkeys             ' Hotkeys
        _log = New cLog                     ' Log instance
        _ConnectionForm = New frmConnection(theConnection)
        _frmMain = New frmMain              ' Main form
        _trayIcon = New cTrayIcon(2)        ' Tray icons


        ' Other init
        _isVista = IsOsWindowsVista()
        _isAdmin = IsUserAdministrator()


        ' Load preferences
        Try
            If My.Settings.FirstTime Then
                MsgBox(Pref.MSGFIRSTTIME, MsgBoxStyle.Information, "Please read this")
                My.Settings.FirstTime = False
                Program.Preferences.Save()
            End If
            Program.Preferences.Apply()
            cProcess.BuffSize = My.Settings.HistorySize
        Catch ex As Exception
            ' Preference file corrupted/missing
            MsgBox("Preference file is missing or corrupted and will be now recreated.", MsgBoxStyle.Critical, "Startup error")
            Program.Preferences.SetDefault()
        End Try


        ' Enable some privileges
        clsOpenedHandles.EnableDebug()
        clsOpenedHandles.EnableShutDown()


        ' Read hotkeys & state based actions from XML files
        Call frmHotkeys.readHotkeysFromXML()
        'Call frmBasedStateAction.readStateBasedActionFromXML()


        ' Show main form & start application
        Application.Run(_frmMain)

    End Sub

    ' Handler for exceptions
    Private Sub MYExnHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Dim ex As Exception
        ex = CType(e.ExceptionObject, Exception)
        Console.WriteLine(ex.StackTrace)
        Dim t As New frmError(ex)
        '    t.ShowDialog()
    End Sub
    Private Sub MYThreadHandler(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        Console.WriteLine(e.Exception.StackTrace)
        Dim t As New frmError(e.Exception)
        '  t.ShowDialog()
    End Sub




    ' Exit application
    Public Sub ExitYAPM()

        ' Save settings
        My.Settings.Save()

        ' Uninstall driver
        Try
            cHandle.GetOpenedHandlesClass.Close()
        Catch ex As Exception
            '
        End Try

        ' Close forms & exit
        My.Settings.HideClose = False
        If _frmMain IsNot Nothing Then
            _frmMain.Close()
        End If
        Application.Exit()

    End Sub

    ' Return true if the application is already running
    Private Function IsAlreadyRunning() As Boolean
        Dim hMap As IntPtr
        Dim pMem As IntPtr
        Dim hPid As Integer

        Const FILE_NAME As String = "YAPM-instanceCheck"

        '# Nous tentons ici d'acceder au mappage (précedemment créé ?)
        hMap = API.OpenFileMapping(API.FILE_MAP_READ, False, FILE_NAME)
        If hMap <> IntPtr.Zero Then
            '# L'application est déjà lancée.
            pMem = API.MapViewOfFile(hMap, API.FileMapAccess.FileMapRead, 0, 0, 0)
            If pMem <> IntPtr.Zero Then
                '# On récupère le handle vers la précédente fenêtre
                hPid = Marshal.ReadInt32(pMem, 0)
                If hPid <> 0 Then
                    '# On active l'instance précedente
                    Try
                        AppActivate(hPid)
                    Catch ex As Exception
                        '
                    End Try
                End If
                API.UnmapViewOfFile(pMem)
            End If
            '# On libère le handle hmap
            API.CloseHandle(hMap)
            '# et on prévient l'appelant que l'application avait dejà été lancée.
            Return True
        Else
            '# Nous sommes dans la première instance de l'application.
            '# Nous allons laisser une marque en mémoire, pour l'indiquer
            hMap = API.CreateFileMapping(New IntPtr(-1), IntPtr.Zero, API.FileMapProtection.PageReadWrite, 0, 4, FILE_NAME)
            If hMap <> IntPtr.Zero Then
                '# On ouvre le 'fichier' en écriture
                pMem = API.MapViewOfFile(hMap, API.FileMapAccess.FileMapWrite, 0, 0, 0)
                If pMem <> IntPtr.Zero Then
                    '# On y écrit l'ID du process courant
                    Marshal.WriteInt32(pMem, 0, API.GetCurrentProcessId)
                    API.UnmapViewOfFile(pMem)
                End If
                '# Pas de CloseHandle hMap ici, sous peine de détruire le mappage lui-même...
            End If
        End If

        Return False

    End Function

End Module
