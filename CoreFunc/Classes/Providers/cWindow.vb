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
Imports System.Drawing
Imports System.Text

Public Class cWindow
    Inherits cGeneralObject

    ' ========================================
    ' API
    ' ========================================
#Region "API declaration"

    ' ========================================
    ' API declarations
    ' ========================================
    'Public Structure PICTDESC
    '    Friend SizeOfStruct As Integer
    '    Friend PicType As Integer
    '    Friend Hbitmap As IntPtr
    '    Friend Hpal As IntPtr
    '    Friend Padding As Integer
    '    Friend Sub New(ByVal hBmp As IntPtr)
    '        Me.SizeOfStruct = Marshal.SizeOf(Me.GetType)
    '        Me.PicType = 1
    '        Me.Hbitmap = hBmp
    '        Me.Hpal = IntPtr.Zero
    '        Me.Padding = 0
    '    End Sub
    'End Structure

    '<DllImport("olepro32.dll")> _
    'Private Shared Function OleCreatePictureIndirect(ByRef pPictDesc As PICTDESC, _
    '   ByRef riid As Guid, ByVal fOwn As Integer, _
    '   <MarshalAs(UnmanagedType.IDispatch)> ByRef ppvObj As Object) As Integer
    'End Function
    Public Structure LightWindow
        Dim handle As IntPtr
        Dim pid As Integer
        Dim threadId As Integer
        Dim procName As String
        Public Sub New(ByVal _handle As IntPtr, ByVal _pid As Integer, ByVal _thread As Integer)
            handle = _handle
            pid = _pid
            threadId = _thread
            procName = cProcess.GetProcessName(pid)
        End Sub
    End Structure

    Private Declare Function GetForegroundWindow Lib "user32" () As IntPtr

    Private Declare Function GetLayeredWindowAttributes Lib "User32.Dll" (ByVal hwnd As IntPtr, ByRef pcrKey As Integer, ByRef pbAlpha As Byte, ByRef pdwFlags As Integer) As Boolean
    Private Declare Auto Function SetLayeredWindowAttributes Lib "User32.Dll" (ByVal hWnd As IntPtr, ByVal crKey As Integer, ByVal Alpha As Byte, ByVal dwFlags As Integer) As Boolean
    <DllImport("user32.dll")> _
    Private Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As IntPtr) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function
    Private Declare Function FlashWindowEx Lib "user32" (ByRef pfwi As FLASHWINFO) As Boolean
    Private Declare Function SetForegroundWindowAPI Lib "user32" Alias "SetForegroundWindow" (ByVal hWnd As IntPtr) As Integer
    Private Declare Function SetActiveWindowAPI Lib "user32.dll" Alias "SetActiveWindow" (ByVal hWnd As IntPtr) As Integer
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Boolean
    End Function
    Private Declare Function EnableWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal fEnable As Integer) As Integer
    Private Declare Function GetClassLong Lib "user32.dll" Alias "GetClassLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
    End Function
    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    Private Declare Function GetWindowAPI Lib "user32" Alias "GetWindow" (ByVal hWnd As IntPtr, ByVal wCmd As Integer) As IntPtr
    Private Declare Auto Function GetDesktopWindow Lib "user32.dll" () As IntPtr
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal lpString As System.Text.StringBuilder) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function GetWindowText(ByVal hwnd As IntPtr, ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function IsWindowVisible(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
    End Sub
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function IsWindowEnabled(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport("user32.dll")> _
    Private Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function
    <DllImport("user32.dll")> _
    Private Shared Function SetWindowPlacement(ByVal hWnd As IntPtr, ByRef lpwndpl As WindowPlacement) As Boolean
    End Function
    <DllImport("user32.dll")> _
    Private Shared Function GetWindowPlacement(ByVal hWnd As IntPtr, ByRef lpwndpl As WindowPlacement) As Boolean
    End Function
    Private Enum ShowState As UInteger
        SW_HIDE = 0
        SW_SHOWNORMAL = 1
        SW_SHOWMINIMIZED = 2
        SW_SHOWMAXIMIZED = 3
        SW_SHOWNOACTIVATE = 4
        SW_SHOW = 5
        SW_MINIMIZE = 6
        SW_SHOWMINNOACTIVE = 7
        SW_SHOWNA = 8
        SW_RESTORE = 9
        SW_SHOWDEFAULT = 10
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure WindowPlacement
        Public Length As UInteger
        Public Flags As UInteger
        Public ShowCmd As ShowState
        Public MinPosition As Point
        Public MaxPosition As Point
        Public NormalPosition As RECT
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure FLASHWINFO
        Public cbSize As Integer
        Public hWnd As Integer
        Public dwFlags As Integer
        Public uCount As Integer
        Public dwTimeout As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    ' ========================================
    ' Constants
    ' ========================================
    Private Const GWL_HWNDPARENT As Integer = -8
    Private Const GW_CHILD As Integer = 5
    Private Const GW_HWNDNEXT As Integer = 2
    Private Const WM_GETICON As Integer = &H7F
    Private Const ICON_BIG As Integer = 1
    Private Const ICON_SMALL As Integer = 0
    Private Const GCL_HICON As Integer = -14
    Private Const GCL_HICONSM As Integer = -34
    Private Const WM_CLOSE As Integer = &H10
    Private Const SW_HIDE As Integer = 0
    Private Const SW_SHOWNORMAL As Integer = 1
    Private Const SW_SHOWMINIMIZED As Integer = 2
    Private Const SW_SHOWMAXIMIZED As Integer = 3
    Private Const SW_MAXIMIZE As Integer = 3
    Private Const SW_SHOWNOACTIVATE As Integer = 4
    Private Const SW_SHOW As Integer = 5
    Private Const SW_MINIMIZE As Integer = 6
    Private Const SW_SHOWMINNOACTIVE As Integer = 7
    Private Const SW_SHOWNA As Integer = 8
    Private Const SW_RESTORE As Integer = 9
    Private Const SW_SHOWDEFAULT As Integer = 10
    Private Const SWP_NOSIZE As Integer = &H1
    Private Const SWP_NOMOVE As Integer = &H2
    Private Const SWP_NOACTIVATE As Integer = &H10
    Private Const HWND_TOPMOST As Integer = -1
    Private Const SWP_SHOWWINDOW As Integer = &H40
    Private Const HWND_NOTOPMOST As Integer = -2
    Private Const GWL_EXSTYLE As Integer = -20
    Private Const WS_EX_LAYERED As Integer = &H80000
    Private Const LWA_COLORKEY As Integer = &H1
    Private Const LWA_ALPHA As Integer = &H2
#End Region

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _handle As IntPtr
    Private _parentProcId As Integer
    Private _parentThreadId As Integer
    Private _parentProcName As String
    Private _displayed As Boolean
    Private _oldCaption As String

    ' ========================================
    ' Getter & setter
    ' ========================================
    Public ReadOnly Property Handle() As IntPtr
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property ParentProcessId() As Integer
        Get
            Return _parentProcId
        End Get
    End Property
    Public ReadOnly Property ParentThreadId() As Integer
        Get
            Return _parentThreadId
        End Get
    End Property
    Public ReadOnly Property ParentProcessName() As String
        Get
            Return _parentProcName
        End Get
    End Property
    Public Property Caption() As String
        Get
            If Me.IsKilledItem = False Then
                Dim length As Integer
                length = GetWindowTextLength(_handle)
                Dim _cap As New StringBuilder(Space(length + 1))
                length = GetWindowText(_handle, _cap, length + 1)
                _oldCaption = _cap.ToString.Substring(0, Len(_cap.ToString))
                Return _oldCaption
            Else
                Return _oldCaption
            End If
        End Get
        Set(ByVal value As String)
            SetWindowText(_handle, New StringBuilder(value))
        End Set
    End Property
    Public ReadOnly Property IsTask() As Boolean
        Get
            Return _isTask(_handle)
        End Get
    End Property
    Public Property Enabled() As Boolean
        Get
            Return IsWindowEnabled(_handle)
        End Get
        Set(ByVal value As Boolean)
            EnableWindow(_handle, CInt(value))
        End Set
    End Property
    Public Property Visible() As Boolean
        Get
            Return IsWindowVisible(_handle)
        End Get
        Set(ByVal value As Boolean)
            If value Then
                Show()
            Else
                Hide()
            End If
        End Set
    End Property
    Public Property Height() As Integer
        Get
            Return GetWindowPosition.Bottom - GetWindowPosition.Top
        End Get
        Set(ByVal value As Integer)
            SetWindowPosition(, , , value)
        End Set
    End Property
    Public Property Width() As Integer
        Get
            Return GetWindowPosition.Right - GetWindowPosition.Left
        End Get
        Set(ByVal value As Integer)
            SetWindowPosition(, , value, )
        End Set
    End Property
    Public Property Top() As Integer
        Get
            Return GetWindowPosition.Top
        End Get
        Set(ByVal value As Integer)
            SetWindowPosition(, value, , )
        End Set
    End Property
    Public Property Left() As Integer
        Get
            Return GetWindowPosition.Left
        End Get
        Set(ByVal value As Integer)
            SetWindowPosition(value, , , )
        End Set
    End Property
    Public Property Opacity() As Byte
        Get
            Return GetWindowOpacity()
        End Get
        Set(ByVal value As Byte)
            If value = 255 Then
                Call DisableWindowOpacity()
            Else
                Call EnableWindowOpacity()
                Call ChangeWindowOpacity(value)
            End If
        End Set
    End Property
    Public ReadOnly Property Positions() As RECT
        Get
            Return GetWindowPosition()
        End Get
    End Property
    Public ReadOnly Property SmallIcon() As System.Drawing.Icon
        Get
            Dim i As IntPtr = GetWindowSmallIcon()
            If Not (i = IntPtr.Zero) Then
                Return System.Drawing.Icon.FromHandle(i)
            Else
                Return Nothing
            End If
        End Get
    End Property


    ' ========================================
    ' Constructor & destructor
    ' ========================================
    Public Sub New(ByRef window As LightWindow)
        MyBase.New()
        _handle = window.handle
        _parentProcId = window.pid
        _parentThreadId = window.threadId
        _parentProcName = window.procName
    End Sub



    ' ========================================
    ' Public functions
    ' ========================================
    Public Overrides Function GetInformation(ByVal info As String) As String
        Select Case info
            Case "Name", "Caption"
                Return Me.Caption
            Case "Handle"
                Return Me.Handle.ToString
            Case "Process"
                Return Me.ParentProcessId.ToString & " -- " & Me.ParentProcessName
            Case "Caption"
                Return Me.Caption
            Case "IsTask"
                Return Me.IsTask.ToString
            Case "Enabled"
                Return Me.Enabled.ToString
            Case "Visible"
                Return Me.Visible.ToString
            Case "ThreadId"
                Return Me.ParentThreadId.ToString
            Case "Opacity"
                Return Me.Opacity.ToString
            Case "Left"
                Return Me.Left.ToString
            Case "Height"
                Return Me.Height.ToString
            Case "Top"
                Return Me.Top.ToString
            Case "Width"
                Return Me.Width.ToString
            Case Else
                Return Me.Handle.ToString
        End Select
    End Function

    Public Function Close() As Integer
        Return CInt(SendMessage(_handle, WM_CLOSE, 0, 0))
    End Function
    Public Function Flash() As Boolean
        Dim FlashInfo As FLASHWINFO

        With FlashInfo
            .cbSize = Marshal.SizeOf(FlashInfo)
            .dwFlags = &H3            ' Flash caption & taskbar
            .dwTimeout = 0
            .hWnd = CInt(_handle)
            .uCount = Integer.MaxValue
        End With

        Return FlashWindowEx(FlashInfo)
    End Function
    Public Function StopFlashing() As Boolean
        Dim FlashInfo As FLASHWINFO

        With FlashInfo
            .cbSize = Marshal.SizeOf(FlashInfo)
            .dwFlags = 0            ' Stop flashing
            .dwTimeout = 0
            .hWnd = CInt(_handle)
            .uCount = 0
        End With

        Return FlashWindowEx(FlashInfo)
    End Function
    Public Function BringToFront(ByVal value As Boolean) As Integer
        If value Then
            Return CInt(SetWindowPos(_handle, HWND_TOPMOST, 0, 0, 0, 0, _
                SWP_NOACTIVATE Or SWP_SHOWWINDOW Or SWP_NOMOVE Or SWP_NOSIZE))
        Else
            Return CInt(SetWindowPos(_handle, HWND_NOTOPMOST, 0, 0, 0, 0, _
                SWP_NOACTIVATE Or SWP_SHOWWINDOW Or SWP_NOMOVE Or SWP_NOSIZE))
        End If
    End Function
    Public Function SetAsForegroundWindow() As Integer
        Return SetForegroundWindowAPI(_handle)
    End Function
    Public Function SetAsActiveWindow() As Integer
        Return SetActiveWindowAPI(_handle)
    End Function
    Public Function Minimize() As Boolean
        Return ShowWindow(_handle, SW_MINIMIZE)
    End Function
    Public Function Maximize() As Boolean
        Return ShowWindow(_handle, SW_MAXIMIZE)
    End Function
    Public Function Show() As Boolean
        Return ShowWindow(_handle, SW_SHOW)
    End Function
    Public Function Hide() As Boolean
        Return ShowWindow(_handle, SW_HIDE)
    End Function
    Public Function SetPositions(ByVal r As RECT) As Boolean
        Return SetWindowPosition(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top)
    End Function
    Public Function SendMessage(ByVal msg As Integer, ByVal param1 As Integer, ByVal param2 As Integer) As Integer
        Return SendMessage(_handle, msg, param1, param2).ToInt32
    End Function


    ' ========================================
    ' Shared functions
    ' ========================================

    ' Get caption
    Public Shared Function GetCaption(ByVal h As IntPtr) As String
        Dim length As Integer
        length = GetWindowTextLength(h)
        Dim _cap As New StringBuilder(Space(length + 1))
        length = GetWindowText(h, _cap, length + 1)
        Return _cap.ToString.Substring(0, Len(_cap.ToString))
    End Function

    ' Retrieve windows list
    Public Shared Function Enumerate(ByVal showAll As Boolean, ByRef pidList() As _
                                     Integer, ByRef key() As Integer, ByRef _dico _
                                     As Dictionary(Of String, LightWindow)) As Integer

        Dim currWnd As IntPtr
        Dim cpt As Integer
        _dico.Clear()

        currWnd = GetWindowAPI(GetDesktopWindow(), GW_CHILD)
        cpt = 0
        ReDim key(0)
        Do While Not (currWnd = IntPtr.Zero)

            ' Get procId from hwnd
            Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)
            If Array.IndexOf(pidList, pid) >= 0 Then
                ' Then this window belongs to one of our processes
                If showAll OrElse GetCaptionLenght(currWnd) > 0 Then
                    key(cpt) = currWnd.ToInt32
                    _dico.Add(key(cpt).ToString, New LightWindow(currWnd, pid, GetThreadIdFromWindowHandle(currWnd)))
                    cpt += 1
                    ReDim Preserve key(cpt)
                End If
            End If

            currWnd = GetWindowAPI(currWnd, GW_HWNDNEXT)
        Loop

        ReDim Preserve key(cpt - 1)

        Return key.Length

    End Function

    ' Retrieve all windows
    Public Shared Function EnumerateAll(ByVal showAll As Boolean, ByRef key() As Integer, _
                                        ByRef _dico As Dictionary(Of String, LightWindow)) As Integer
        Dim currWnd As IntPtr
        Dim cpt As Integer
        _dico.Clear()

        currWnd = GetWindowAPI(GetDesktopWindow(), GW_CHILD)
        cpt = 0
        ReDim key(0)
        Do While Not (currWnd = IntPtr.Zero)

            ' Get procId from hwnd
            Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)

            If showAll OrElse GetCaptionLenght(currWnd) > 0 Then
                ReDim Preserve key(cpt)
                key(cpt) = currWnd.ToInt32
                _dico.Add(key(cpt).ToString, New LightWindow(currWnd, pid, GetThreadIdFromWindowHandle(currWnd)))
                cpt += 1
            End If

            currWnd = GetWindowAPI(currWnd, GW_HWNDNEXT)
        Loop

        Return key.Length

    End Function

    ' Return if a window is a task
    Public Shared Function IsWindowTask(ByVal hwnd As IntPtr) As Boolean
        Return _isTask(hwnd)
    End Function

    Public Shared Function MaximizeWindow(ByVal hWnd As IntPtr) As Integer
        Return CInt(ShowWindow(hWnd, SW_MAXIMIZE))
    End Function
    Public Shared Function MinimizeWindow(ByVal hWnd As IntPtr) As Integer
        Return CInt(ShowWindow(hWnd, SW_MINIMIZE))
    End Function
    Public Shared Function HideWindow(ByVal hWnd As IntPtr) As Integer
        Return CInt(ShowWindow(hWnd, SW_HIDE))
    End Function
    Public Shared Function CloseWindow(ByVal hWnd As Integer) As Integer
        Return CInt(SendMessage(CType(hWnd, IntPtr), WM_CLOSE, 0, 0))
    End Function
    Public Shared Function ShowWindow(ByVal hWnd As Integer) As Integer
        Return CInt(SendMessage(CType(hWnd, IntPtr), SW_SHOW, 0, 0))
    End Function
    Public Shared Function ShowWindowForeground(ByVal hWnd As IntPtr) As Integer
        Return SetForegroundWindowAPI(hWnd)
    End Function

    ' Get processID of foreground window
    Public Shared Function GetForegroundAppPID() As Integer
        Dim l As IntPtr = GetForegroundWindow
        Return GetProcIdFromWindowHandle(l)
    End Function

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(11) As String

        s(0) = "Caption"
        s(1) = "Process"
        s(2) = "Caption"
        s(3) = "IsTask"
        s(4) = "Enabled"
        s(5) = "Visible"
        s(6) = "ThreadId"
        s(7) = "Height"
        s(8) = "Width"
        s(9) = "Top"
        s(10) = "Left"
        s(11) = "Opacity"

        Return s
    End Function

    ' ========================================
    ' Private functions
    ' ========================================

    ' Return true if window is a task
    Friend Shared Function _isTask(ByVal hwnd As IntPtr) As Boolean
        ' Window must be visible
        If IsWindowVisible(hwnd) And (CInt(GetWindowLong(hwnd, GWL_HWNDPARENT)) = 0) And Not _
            (GetWindowTextLength(hwnd) = 0) Then
            ' Must not be taskmgr
            If GetWindowClass(hwnd) <> "Progman" Then
                Return True
            End If
        End If

        Return False
    End Function

    ' Get small icon handle of window
    Private Function GetWindowSmallIcon() As IntPtr
        Dim res As IntPtr
        res = SendMessage(_handle, WM_GETICON, ICON_SMALL, 0)


        If res = IntPtr.Zero Then
            res = GetClassLong(_handle, GCL_HICONSM)
        End If

        If res = IntPtr.Zero Then
            res = SendMessage(GetWindowLong(_handle, GWL_HWNDPARENT), WM_GETICON, ICON_SMALL, 0)
        End If

        Return res
    End Function

    ' Get window class name
    Private Shared Function GetWindowClass(ByVal hWnd As IntPtr) As String
        Dim _class As New StringBuilder(Space(255))
        GetClassName(hWnd, _class, 255)
        Return _class.ToString
    End Function

    ' Get position/size
    Private Function GetWindowPosition() As RECT
        Dim tmpRect As RECT
        GetWindowRect(_handle, tmpRect)
        Return tmpRect
    End Function

    ' Return process id from a handle
    Friend Shared Function GetProcIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
        Dim id As Integer = 0
        GetWindowThreadProcessId(hwnd, id)
        Return id
    End Function

    ' Return thread id from a handle
    Friend Shared Function GetThreadIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
        Return GetWindowThreadProcessId(hwnd, 0)
    End Function

    ' Return caption
    Friend Shared Function GetCaptionLenght(ByVal hwnd As IntPtr) As Integer
        Return GetWindowTextLength(hwnd)
    End Function

    Private Function SetWindowPosition(Optional ByVal _Left As Integer = Nothing, _
         Optional ByVal _Top As Integer = Nothing, Optional ByVal _Width As Integer = Nothing, _
         Optional ByVal _Height As Integer = Nothing) As Boolean

        Dim WndPl As WindowPlacement
        WndPl.Length = CUInt(Marshal.SizeOf(WndPl))
        GetWindowPlacement(_handle, WndPl)
        If (Not (_Left = Nothing)) And IsNumeric(_Left) Then
            WndPl.NormalPosition.Right = WndPl.NormalPosition.Right - WndPl.NormalPosition.Left + _Left
            WndPl.NormalPosition.Left = _Left
        End If
        If (Not (_Top = Nothing)) And IsNumeric(_Top) Then
            WndPl.NormalPosition.Bottom = WndPl.NormalPosition.Bottom - WndPl.NormalPosition.Top + _Top
            WndPl.NormalPosition.Top = _Top
        End If
        If (Not (_Width = Nothing)) And IsNumeric(_Width) Then WndPl.NormalPosition.Right = WndPl.NormalPosition.Left + _Width
        If (Not (_Height = Nothing)) And IsNumeric(_Height) Then WndPl.NormalPosition.Bottom = WndPl.NormalPosition.Top + _Height
        Return SetWindowPlacement(_handle, WndPl)
    End Function

    Private Function EnableWindowOpacity() As Integer
        Return SetWindowLong(_handle, GWL_EXSTYLE, CType(CInt(GetWindowLong(_handle, GWL_EXSTYLE)) Or WS_EX_LAYERED, IntPtr))
    End Function
    Private Function ChangeWindowOpacity(ByVal Alpha As Byte) As Boolean
        Return SetLayeredWindowAttributes(_handle, 0, Alpha, LWA_ALPHA)
    End Function
    Private Function GetWindowOpacity() As Byte
        Dim alpha As Byte
        GetLayeredWindowAttributes(_handle, 0, alpha, LWA_ALPHA)
        Return alpha
    End Function
    Private Function DisableWindowOpacity() As Integer
        Return SetWindowLong(_handle, GWL_EXSTYLE, CType(CInt(GetWindowLong(_handle, GWL_EXSTYLE)) - WS_EX_LAYERED, IntPtr))
    End Function

End Class
