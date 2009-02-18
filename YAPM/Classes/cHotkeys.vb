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
' aInteger with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Imports System.Runtime.InteropServices

Public Class cHotkeys

#Region "API"

    Private Const HC_ACTION As Integer = 0

    Enum HookType
        WH_JOURNALRECORD = 0
        WH_JOURNALPLAYBACK = 1
        WH_KEYBOARD = 2
        WH_GETMESSAGE = 3
        WH_CALLWNDPROC = 4
        WH_CBT = 5
        WH_SYSMSGFILTER = 6
        WH_MOUSE = 7
        WH_HARDWARE = 8
        WH_DEBUG = 9
        WH_SHELL = 10
        WH_FOREGROUNDIDLE = 11
        WH_CALLWNDPROCRET = 12
        WH_KEYBOARD_LL = 13
        WH_MOUSE_LL = 14
    End Enum

    <DllImport("user32.dll", SetLastError:=True)> _
Private Shared Function SetWindowsHookEx(ByVal hook As HookType, ByVal callback As HookProc, ByVal hMod As Integer, ByVal dwThreadId As UInteger) As IntPtr
    End Function

    '<DllImport("user32.dll", SetLastError:=True)> _
    'Private Shared Function SetWindowsHookEx(ByVal hook As HookType, ByVal callback As LowLevelKeyboardProc, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
    'End Function

    '<DllImport("user32.dll", SetLastError:=True)> _
    'Private Shared Function SetWindowsHookEx(ByVal code As HookType, ByVal func As LowLevelMouseProc, ByVal hInstance As IntPtr, ByVal threadID As Integer) As IntPtr
    'End Function

    ' Private Declare Function SetWindowsHookEx Lib "user32" Alias "SetWindowsHookExA" (ByVal idHook As Integer, ByVal lpfn As Integer, ByVal hmod As Integer, ByVal dwThreadId As Integer) As Integer
    Private Declare Function UnhookWindowsHookEx Lib "user32" (ByVal hHook As Integer) As Integer
    Private Declare Function CallNextHookEx Lib "user32" (ByVal hHook As Integer, ByVal nCode As Integer, ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) As Integer
    Private Declare Function GetCurrentThreadId Lib "kernel32" () As Integer
    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Integer
    Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal pDst As Object, ByVal pSrc As Object, ByVal ByteLen As Integer)
    'Private Declare Function GetForegroundWindow Lib "user32" () As Integer

    <StructLayout(LayoutKind.Sequential)> _
Public Structure KBDLLHOOKSTRUCT
        Public vkCode As Integer
        Public scanCode As Integer
        Public flags As KBDLLHOOKSTRUCTFlags
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure

    <Flags()> _
    Public Enum KBDLLHOOKSTRUCTFlags As Integer
        LLKHF_EXTENDED = &H1
        LLKHF_INJECTED = &H10
        LLKHF_ALTDOWN = &H20
        LLKHF_UP = &H80
    End Enum

#End Region


    Private hKeyHook As Integer
    Public colShortCut As New Collection            ' Collection of shortcuts
    Private boolStopHooking As Boolean              ' Private !

    Private _col As New Collection

    Public Enum HOTKEYS_ACTIONS
        KILL_FOREGROUND
        KILL_MAX_CPU_USAGE
    End Enum

    Public Structure HotkeyStruct
        Dim action As HOTKEYS_ACTIONS
        Dim keys As cShortcut
    End Structure



    Delegate Function HookProc(ByVal code As Integer, ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) As Integer
    Private myCallbackDelegate As HookProc = Nothing



    ' Add a key to collection
    Public Sub AddHotkey(ByVal hotkey As HotkeyStruct)

        Dim sKey As String = hotkey.action.ToString
        Try
            _col.Add(Key:=sKey, Item:=hotkey)
        Catch ex As Exception
            '
        End Try
    End Sub

    ' Remove key from collection
    Public Sub RemoveHotKey(ByVal hotkey As HotkeyStruct)
        Dim sKey As String = hotkey.action.ToString
        Try
            _col.Remove(sKey)
        Catch ex As Exception
            '
        End Try
    End Sub

    Public Sub New()
        AttachKeyboardHook()
    End Sub
    Protected Overrides Sub Finalize()
        DetachKeyboardHook()
    End Sub

    '=======================================================
    ' Start hooking of keyboard
    '=======================================================
    Public Sub AttachKeyboardHook()
        Dim lpfn As Integer

        If lpfn = 0 Then

            ' initialize our delegate
            Me.myCallbackDelegate = New HookProc(AddressOf Me.KeyboardFilter)


            hKeyHook = CInt(SetWindowsHookEx(HookType.WH_KEYBOARD_LL, Me.myCallbackDelegate, 0, 0)) 'CUInt(GetCurrentThreadId())))
        End If

    End Sub
    '=======================================================
    ' Stop hooking of keyboard
    '=======================================================
    Public Sub DetachKeyboardHook()

        If (hKeyHook <> 0) Then
            Call UnhookWindowsHookEx(hKeyHook)
            hKeyHook = 0
        End If

    End Sub
    Private Function HookAddress(ByVal lPtr As Integer) As Integer
        ' Yeah, assuming AddressOf is usable only with functions, it's the best way to retrieve a proc adress
        HookAddress = lPtr
    End Function

    Private Const vbKeyShift As Integer = 16
    Private Const vbKeyControl As Integer = 17
    Private Const vbShiftMask As Integer = vbKeyShift
    Private Const vbCtrlMask As Integer = vbKeyControl
    Private Const vbAltMask As Integer = 4

    '=======================================================
    ' This function is called each time a key is pressed
    '=======================================================
    Private Function KeyboardFilter(ByVal nCode As Integer, ByVal wParam As Integer, ByRef lParam As KBDLLHOOKSTRUCT) As Integer
        Dim bAlt As Boolean
        Dim bCtrl As Boolean
        Dim bShift As Boolean
        Dim cSs As Object


        If nCode = HC_ACTION And Not boolStopHooking Then

            bShift = (GetAsyncKeyState(vbKeyShift) <> 0)
            'bAlt = ((lParam.vkCode And &H20000000) = &H20000000)
            bCtrl = (GetAsyncKeyState(vbKeyControl) <> 0)

            ' Check for each of our cShortCut if the shortcut is activated
            For Each cSs In _col

                Dim cs As cShortcut = CType(cSs, HotkeyStruct).keys

                If (cs.Key1 = -1) Or (cs.Key1 = vbShiftMask And bShift) Or (cs.Key1 = vbAltMask And bAlt) Or _
                    (cs.Key1 = vbCtrlMask And bCtrl) Then

                    ' Then the first of the 3 keys is pressed
                    ' Check the second one
                    If (cs.Key2 = -1) Or (cs.Key2 = vbShiftMask And bShift) Or (cs.Key2 = vbAltMask And bAlt) Or _
                        (cs.Key2 = vbCtrlMask And bCtrl) Then

                        ' The the second of the 3 keys is pressed
                        ' Check this last one
                        If (lParam.vkCode = cs.Key3) Then

                            ' The third of the 3 keys is pressed
                            ' Okay :-)
                            ' Here we raise our event
                            boolStopHooking = True                      ' We stop hooking shortcuts
                            'MsgBox(cs.Key1)
                            frmMain.Text = "OKOKOKO"
                            'Dim frm As Form
                            'frm = GetFormFromPtr(cS.FormEvent)      ' We retrieve the form which implements our interface
                            'If frm.hWnd <> GetForegroundWindow Then Exit For
                            'IEvent = frm
                            'Call IEvent.ShortCutActivated(cS.Tag)       ' Raise our event


                            boolStopHooking = False                     ' Now we can hook another shortcuts
                            Exit For

                        End If

                    End If

                End If

            Next cSs

        End If

        ' Next hook
        KeyboardFilter = CallNextHookEx(hKeyHook, nCode, wParam, lParam)

    End Function
End Class
