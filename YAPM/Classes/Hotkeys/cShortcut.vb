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

Public Class cShortcut

    ' ========================================
    ' Public Enums
    ' ========================================

    ' Key list (according to GetAsyncKeyState parameters)
    Public Enum ShorcutKeys As Integer
        VK_NO_BUTTON = -1
        VK_LBUTTON = &H1
        VK_RBUTTON = &H2
        VK_CANCEL = &H3
        VK_MBUTTON = &H4
        VK_XBUTTON1 = &H5
        VK_XBUTTON2 = &H6
        VK_BACK = &H8
        VK_TAB = &H9
        VK_CLEAR = &HC
        VK_RETURN = &HD
        VK_SHIFT = &H10
        VK_CONTROL = &H11
        VK_MENU = &H12
        VK_PAUSE = &H13
        VK_CAPITAL = &H14
        VK_KANA = &H15
        VK_HANGUEL = &H15
        VK_HANGUL = &H15
        VK_JUNJA = &H17
        VK_FINAL = &H18
        VK_HANJA = &H19
        VK_KANJI = &H19
        VK_ESCAPE = &H1B
        VK_CONVERT = &H1C
        VK_NONCONVERT = &H1D
        VK_ACCEPT = &H1E
        VK_MODECHANGE = &H1F
        VK_SPACE = &H20
        VK_PRIOR = &H21
        VK_NEXT = &H22
        VK_END = &H23
        VK_HOME = &H24
        VK_LEFT = &H25
        VK_UP = &H26
        VK_RIGHT = &H27
        VK_DOWN = &H28
        VK_SELECT = &H29
        VK_PRINT = &H2A
        VK_EXECUTE = &H2B
        VK_SNAPSHOT = &H2C
        VK_INSERT = &H2D
        VK_DELETE = &H2E
        VK_HELP = &H2F
        VK_0 = &H30
        VK_1 = &H31
        VK_2 = &H32
        VK_3 = &H33
        VK_4 = &H34
        VK_5 = &H35
        VK_6 = &H36
        VK_7 = &H37
        VK_8 = &H38
        VK_9 = &H39
        VK_A = &H41
        VK_B = &H42
        VK_C = &H43
        VK_D = &H44
        VK_E = &H45
        VK_F = &H46
        VK_G = &H47
        VK_H = &H48
        VK_I = &H49
        VK_J = &H4A
        VK_K = &H4B
        VK_L = &H4C
        VK_M = &H4D
        VK_N = &H4E
        VK_O = &H4F
        VK_P = &H50
        VK_Q = &H51
        VK_R = &H52
        VK_S = &H53
        VK_T = &H54
        VK_U = &H55
        VK_V = &H56
        VK_W = &H57
        VK_X = &H58
        VK_Y = &H59
        VK_Z = &H5A
        VK_LWIN = &H5B
        VK_RWIN = &H5C
        VK_APPS = &H5D
        VK_SLEEP = &H5F
        VK_NUMPAD0 = &H60
        VK_NUMPAD1 = &H61
        VK_NUMPAD2 = &H62
        VK_NUMPAD3 = &H63
        VK_NUMPAD4 = &H64
        VK_NUMPAD5 = &H65
        VK_NUMPAD6 = &H66
        VK_NUMPAD7 = &H67
        VK_NUMPAD8 = &H68
        VK_NUMPAD9 = &H69
        VK_MULTIPLY = &H6A
        VK_ADD = &H6B
        VK_SEPARATOR = &H6C
        VK_SUBTRACT = &H6D
        VK_DECIMAL = &H6E
        VK_DIVIDE = &H6F
        VK_F1 = &H70
        VK_F2 = &H71
        VK_F3 = &H72
        VK_F4 = &H73
        VK_F5 = &H74
        VK_F6 = &H75
        VK_F7 = &H76
        VK_F8 = &H77
        VK_F9 = &H78
        VK_F10 = &H79
        VK_F11 = &H7A
        VK_F12 = &H7B
        VK_F13 = &H7C
        VK_F14 = &H7D
        VK_F15 = &H7E
        VK_F16 = &H7F
        VK_F17 = &H80
        VK_F18 = &H81
        VK_F19 = &H82
        VK_F20 = &H83
        VK_F21 = &H84
        VK_F22 = &H85
        VK_F23 = &H86
        VK_F24 = &H87
        VK_NUMLOCK = &H90
        VK_SCROLL = &H91
        VK_LSHIFT = &HA0
        VK_RSHIFT = &HA1
        VK_LCONTROL = &HA2
        VK_RCONTROL = &HA3
        VK_LMENU = &HA4
        VK_RMENU = &HA5
        VK_BROWSER_BACK = &HA6
        VK_BROWSER_FORWARD = &HA7
        VK_BROWSER_REFRESH = &HA8
        VK_BROWSER_STOP = &HA9
        VK_BROWSER_SEARCH = &HAA
        VK_BROWSER_FAVORITES = &HAB
        VK_BROWSER_HOME = &HAC
        VK_VOLUME_MUTE = &HAD
        VK_VOLUME_DOWN = &HAE
        VK_VOLUME_UP = &HAF
        VK_MEDIA_NEXT_TRACK = &HB0
        VK_MEDIA_PREV_TRACK = &HB1
        VK_MEDIA_STOP = &HB2
        VK_MEDIA_PLAY_PAUSE = &HB3
        VK_LAUNCH_MAIL = &HB4
        VK_LAUNCH_MEDIA_SELECT = &HB5
        VK_LAUNCH_APP1 = &HB6
        VK_LAUNCH_APP2 = &HB7
        VK_OEM_1 = &HBA
        VK_OEM_PLUS = &HBB
        VK_OEM_COMMA = &HBC
        VK_OEM_MINUS = &HBD
        VK_OEM_PERIOD = &HBE
        VK_OEM_2 = &HBF
        VK_OEM_3 = &HC0
        VK_OEM_4 = &HDB
        VK_OEM_5 = &HDC
        VK_OEM_6 = &HDD
        VK_OEM_7 = &HDE
        VK_OEM_8 = &HDF
        VK_OEM_102 = &HE2
        VK_PROCESSKEY = &HE5
        VK_PACKET = &HE7
        VK_ATTN = &HF6
        VK_CRSEL = &HF7
        VK_EXSEL = &HF8
        VK_EREOF = &HF9
        VK_PLAY = &HFA
        VK_ZOOM = &HFB
        VK_NONAME = &HFC
        VK_PA1 = &HFD
        VK_OEM_CLEAR = &HFE
    End Enum

    ' Action to proceed when hotkeys pressed
    Public Enum HOTKEYS_ACTIONS As Integer
        KILL_FOREGROUND = 1
        EXIT_YAPM = 2
    End Enum

    ' ========================================
    ' Private variables
    ' ========================================
    Private _Key1 As ShorcutKeys
    Private _Key2 As ShorcutKeys
    Private _Key3 As ShorcutKeys
    Private _action As HOTKEYS_ACTIONS
    Private _enabled As Boolean
    Private _key As String


    ' ========================================
    ' Public properties
    ' ========================================
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property
    Public Property Key1() As ShorcutKeys
        Get
            Return _Key1
        End Get
        Set(ByVal value As ShorcutKeys)
            _Key1 = value
        End Set
    End Property
    Public Property Action() As HOTKEYS_ACTIONS
        Get
            Return _action
        End Get
        Set(ByVal value As HOTKEYS_ACTIONS)
            _action = value
        End Set
    End Property
    Public Property Key2() As ShorcutKeys
        Get
            Return _Key2
        End Get
        Set(ByVal value As ShorcutKeys)
            _Key2 = value
        End Set
    End Property
    Public Property Key3() As ShorcutKeys
        Get
            Return _Key3
        End Get
        Set(ByVal value As ShorcutKeys)
            _Key3 = value
        End Set
    End Property


    ' ========================================
    ' Public functions
    ' ========================================
    Public Sub New(ByVal aAction As HOTKEYS_ACTIONS, ByVal akey1 As ShorcutKeys, _
                   Optional ByVal akey2 As ShorcutKeys = ShorcutKeys.VK_NO_BUTTON, _
                   Optional ByVal akey3 As ShorcutKeys = ShorcutKeys.VK_NO_BUTTON)
        _Key1 = akey1
        _Key2 = akey2
        _Key3 = akey3
        _key = "|" & CStr(CInt(Me.Key1)) & "|" & CStr(CInt(Me.Key2)) & "|" & CStr(CInt(Me.Key3))
        _action = aAction
        _enabled = True
    End Sub
    Public Sub New(ByVal aAction As Integer, ByVal akey1 As Integer, _
               Optional ByVal akey2 As Integer = ShorcutKeys.VK_NO_BUTTON, _
               Optional ByVal akey3 As Integer = ShorcutKeys.VK_NO_BUTTON)
        _Key1 = CType(akey1, ShorcutKeys)
        _Key2 = CType(akey2, ShorcutKeys)
        _Key3 = CType(akey3, ShorcutKeys)
        _key = "|" & CStr(CInt(Me.Key1)) & "|" & CStr(CInt(Me.Key2)) & "|" & CStr(CInt(Me.Key3))
        _action = CType(aAction, HOTKEYS_ACTIONS)
        _enabled = True
    End Sub

    ' Proceed to action !
    Public Sub RaiseAction()
        Static once As Boolean = False

        once = Not (once)

        If once Then
            Select Case _action
                Case HOTKEYS_ACTIONS.KILL_FOREGROUND
                    Call cProcess.LocalKill(cWindow.LocalGetForegroundAppProcessId)
                Case HOTKEYS_ACTIONS.EXIT_YAPM
                    'TODO
                Case Else
                    '
            End Select
        End If
    End Sub

End Class
