' =======================================================
' Yet Another Process Monitor (YAPM)
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
Imports System.Threading

Public Class cRegMonitor

#Region "API"

    ' Event api
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    <DllImport("kernel32.dll", _
     EntryPoint:="CreateEventA")> _
    Private Shared Function CreateEvent( _
        ByVal lpEventAttributes As IntPtr, _
        ByVal bManualReset As Boolean, _
        ByVal bInitialState As Boolean, _
        ByVal lpName As String) As IntPtr
    End Function

    ' Key api
    Private Declare Auto Function RegCloseKey Lib "advapi32.dll" (ByVal hKey As Integer) As Integer
    Private Declare Auto Function RegOpenKeyEx Lib "advapi32.dll" ( _
       ByVal hKey As IntPtr, _
       ByVal lpSubKey As String, _
       ByVal ulOptions As Integer, _
       ByVal samDesired As Integer, _
       ByRef phkResult As Integer) As Integer
    Private Declare Function RegNotifyChangeKeyValue Lib "advapi32.dll" Alias _
        "RegNotifyChangeKeyValue" (ByVal hKey As Integer, ByVal bWatchSubtree As Integer, _
        ByVal dwNotifyFilter As Integer, ByVal hEvent As Integer, ByVal fAsynchronus As _
        Integer) As Integer


    ' http://msdn.microsoft.com/en-us/library/ms724892(VS.85).aspx
    ' Type of Key
    Public Enum KEY_TYPE
        HKEY_CLASSES_ROOT = &H80000000
        HKEY_CURRENT_USER = &H80000001
        HKEY_LOCAL_MACHINE = &H80000002
        HKEY_USERS = &H80000003
        HKEY_CURRENT_CONFIG = &H80000005
        HKEY_PERFORMANCE_DATA = &H80000004
        HKEY_DYN_DATA = &H80000006
    End Enum

    ' Type of monitoring to apply
    Public Enum KEY_MONITORING_TYPE
        REG_NOTIFY_CHANGE_NAME = &H1            ' Subkey added or deleted
        REG_NOTIFY_CHANGE_ATTRIBUTES = &H2      ' Attributes changed
        REG_NOTIFY_CHANGE_LAST_SET = &H4        ' Value changed (changed, deleted, added)
        REG_NOTIFY_CHANGE_SECURITY = &H8        ' Security descriptor changed
    End Enum


    Private Const WAIT_FAILED As Integer = &HFFFFFFFF
    Private Const INFINITE As Integer = &HFFFF
    Private Const KEY_NOTIFY As Integer = &H10

#End Region

    ' Definition of a key
    Public Structure KeyDefinition
        Dim name As String
        ' Add what you want
    End Structure

    ' Event to raise
    Public Event KeyAdded(ByVal key As KeyDefinition)
    Public Event KeyDeleted(ByVal key As KeyDefinition)

    Private _hEvent As Integer
    Private _hKey As Integer
    Private _type As KEY_MONITORING_TYPE
    Private _keys() As String
    Private _ss() As String
    Private _path As String
    Private _kt As KEY_TYPE
    Public _t As Thread

    ' Constructor
    Public Sub New(ByVal KeyType As KEY_TYPE, ByVal path As String, ByVal monType As _
        KEY_MONITORING_TYPE)

        ' Launch event waiting
        _kt = KeyType
        _type = monType
        _path = path
        _t = New Thread(AddressOf ThreadEvent)
        _t.IsBackground = True                  ' Thread will close when app close
        _t.Priority = ThreadPriority.Highest
        _t.Start()

    End Sub

    Protected Overrides Sub Finalize()
        RegCloseKey(_hKey)
        CloseHandle(_hEvent)
    End Sub

    ' Process thread
    Private Sub ThreadEvent()

        ' Create an event
        Do While True

            Call RegOpenKeyEx(CType(_kt, IntPtr), _path, 0, KEY_NOTIFY, _hKey)

            _hEvent = CInt(CreateEvent(CType(0, IntPtr), True, False, Nothing))

            ' Set monitoring
            Call RegNotifyChangeKeyValue(_hKey, 1, _type, _hEvent, 1)

            ' Get current keys
            _keys = getKeys(_path)

            ' Wait for modification
            If WaitForSingleObject(_hEvent, INFINITE) = WAIT_FAILED Then
                ' Buggy
            Else
                ' Changed
                'Trace.WriteLine("Detected a change")
                _ss = getKeys(_path)
                Call keysChanged()

            End If
            Call CloseHandle(_hEvent)
            Call RegCloseKey(_hKey)
        Loop

    End Sub

    Private Sub keysChanged()

        ' Compare with old list and get differences
        Dim s As String = ""

        '  TODO -> delete this Try-Catch block
        Try

            ' Deleted keys
            For Each s In _keys
                Dim s2 As String = ""
                Dim b As Boolean = False
                For Each s2 In _ss
                    If s2 = s Then
                        b = True
                        Exit For
                    End If
                Next
                If Not (b) Then
                    ' s deleted
                    Dim k As KeyDefinition
                    k.name = s
                    'Trace.WriteLine("Key deleted")
                    RaiseEvent KeyDeleted(k)
                End If
            Next

            ' New keys
            For Each s In _ss
                Dim s2 As String = ""
                Dim b As Boolean = False
                For Each s2 In _keys
                    If s2 = s Then
                        b = True
                        Exit For
                    End If
                Next
                If Not (b) Then
                    ' s added
                    Dim k As KeyDefinition
                    k.name = s
                    'Trace.WriteLine("Key added")
                    RaiseEvent KeyAdded(k)
                End If
            Next

        Catch ex As Exception
            '
        End Try

    End Sub

    ' Get list of all subkeys from registry
    Public Shared Function getKeys(ByVal path As String) As String()
        Dim key As Microsoft.Win32.RegistryKey = _
            My.Computer.Registry.LocalMachine.OpenSubKey(path)
        Try
            Return key.GetSubKeyNames
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class
