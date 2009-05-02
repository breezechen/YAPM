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

Imports System.Text

Public Class cWindow
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _windowInfos As windowInfos
    Private Shared WithEvents _connection As cWindowConnection

    Private _oldCaption As String

#Region "Properties"

    Public Shared Property Connection() As cWindowConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cWindowConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As windowInfos)
        _windowInfos = New windowInfos(infos)
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As windowInfos
        Get
            Return _windowInfos
        End Get
    End Property

#End Region

#Region "Other properties"

    Public WriteOnly Property Visible() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                Show()
            Else
                Hide()
            End If
        End Set
    End Property
    Public WriteOnly Property Opacity() As Byte
        Set(ByVal value As Byte)
            If value = 255 Then
                SetOpacity(False)
            Else
                SetOpacity(True, value)
            End If
        End Set
    End Property
    Public WriteOnly Property Enabled() As Boolean
        Set(ByVal value As Boolean)
            SetEnabled(value)
        End Set
    End Property
    Public ReadOnly Property SmallIcon() As System.Drawing.Icon
        Get
            If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
                Dim i As IntPtr = GetWindowSmallIcon()
                If Not (i = IntPtr.Zero) Then
                    Return System.Drawing.Icon.FromHandle(i)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public Property Caption() As String
        Get
            If Me.IsKilledItem = False Then
                Dim length As Integer
                length = API.GetWindowTextLength(Me.Infos.Handle)
                Dim _cap As New StringBuilder(Space(length + 1))
                length = API.GetWindowText(Me.Infos.Handle, _cap, length + 1)
                _oldCaption = _cap.ToString.Substring(0, Len(_cap.ToString))
                Return _oldCaption
            Else
                Return _oldCaption
            End If
        End Get
        Set(ByVal value As String)
            API.SetWindowText(Me.Infos.Handle, New StringBuilder(value))
        End Set
    End Property

#End Region

    ' Merge infos
    Public Sub Refresh()
        Call RefreshSpecialInformations()
    End Sub

#Region "Special informations (GDI, affinity)"

    ' Refresh some non fixed infos
    ' For now IT IS NOT ASYNC
    ' Because create ~50 threads/sec is not really cool
    Private WithEvents asyncNonFixed As asyncCallbackWindowGetNonFixedInfos
    Private Sub RefreshSpecialInformations()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                If asyncNonFixed Is Nothing Then
                    asyncNonFixed = New asyncCallbackWindowGetNonFixedInfos(Me.Infos.Handle, _connection)
                End If
                asyncNonFixed.Process()
        End Select
    End Sub
    Private Sub nonFixedInfosGathered(ByVal infos As asyncCallbackWindowGetNonFixedInfos.TheseInfos) Handles asyncNonFixed.GatheredInfos
        Me.Infos.SetNonFixedInfos(infos)
    End Sub

#End Region

#Region "All actions on window (close, ...)"

    Private Sub actionDone(ByVal Success As Boolean, ByVal action As asyncCallbackWindowAction.ASYNC_WINDOW_ACTION, ByVal handle As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not " & action.ToString & " (window = " & handle.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    Private _theAction As asyncCallbackWindowAction
    Public Function Close() As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.Close, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function


    Public Function Flash() As Boolean
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.Flash, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function StopFlashing() As Boolean
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.StopFlashing, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function BringToFront(ByVal value As Boolean) As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.BringToFront, Me.Infos.Handle, CInt(value), 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function SetAsForegroundWindow() As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.SetAsForegroundWindow, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function SetAsActiveWindow() As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.SetAsActiveWindow, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function Minimize() As Boolean
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.Minimize, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function Maximize() As Boolean
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.Maximize, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function Show() As Boolean
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.Show, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function Hide() As Boolean
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.Hide, Me.Infos.Handle, 0, 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Public Function SetPositions(ByVal r As API.RECT) As Boolean

    End Function
    Public Function SendMessage(ByVal msg As Integer, ByVal param1 As Integer, ByVal param2 As Integer) As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.SendMessage, Me.Infos.Handle, msg, param1, param2, newAction))
        AddPendingTask2(newAction, t)
    End Function

    Public Shared Function IsWindowTask(ByVal hwnd As IntPtr) As Boolean

    End Function
    Public Shared Function MaximizeWindow(ByVal hWnd As IntPtr) As Integer

    End Function
    Public Shared Function MinimizeWindow(ByVal hWnd As IntPtr) As Integer

    End Function
    Public Shared Function HideWindow(ByVal hWnd As IntPtr) As Integer

    End Function
    Public Shared Function CloseWindow(ByVal hWnd As IntPtr) As Integer

    End Function
    Public Shared Function ShowWindow(ByVal hWnd As IntPtr) As Integer

    End Function
    Public Shared Function ShowWindowForeground(ByVal hWnd As IntPtr) As Integer

    End Function
    Public Shared Function GetCaption(ByVal h As IntPtr) As String
        Return ""
    End Function

    Public Shared Sub SetEnabled(ByVal hWnd As IntPtr, ByVal en As Boolean)

    End Sub

    Public Shared Sub SetOpacity(ByVal b As Boolean, Optional ByVal value As Byte = 0)
        If value = 255 Then
            'Call DisableWindowOpacity()
        Else
            'Call EnableWindowOpacity()
            'Call ChangeWindowOpacity(value)
        End If
    End Sub

    Public Shared Function SetWindowPosition(ByVal hWnd As IntPtr, Optional ByVal _Left As Integer = Nothing, _
         Optional ByVal _Top As Integer = Nothing, Optional ByVal _Width As Integer = Nothing, _
         Optional ByVal _Height As Integer = Nothing) As Boolean
        ' ASYNC
        'Dim WndPl As API.WindowPlacement
        'WndPl.Length = CUInt(System.Runtime.InteropServices.Marshal.SizeOf(WndPl))
        'API.GetWindowPlacement(hWnd, WndPl)
        'If (Not (_Left = Nothing)) And IsNumeric(_Left) Then
        '    WndPl.NormalPosition.Right = WndPl.NormalPosition.Right - WndPl.NormalPosition.Left + _Left
        '    WndPl.NormalPosition.Left = _Left
        'End If
        'If (Not (_Top = Nothing)) And IsNumeric(_Top) Then
        '    WndPl.NormalPosition.Bottom = WndPl.NormalPosition.Bottom - WndPl.NormalPosition.Top + _Top
        '    WndPl.NormalPosition.Top = _Top
        'End If
        'If (Not (_Width = Nothing)) And IsNumeric(_Width) Then WndPl.NormalPosition.Right = WndPl.NormalPosition.Left + _Width
        'If (Not (_Height = Nothing)) And IsNumeric(_Height) Then WndPl.NormalPosition.Bottom = WndPl.NormalPosition.Top + _Height
        'Return API.SetWindowPlacement(hWnd, WndPl)
    End Function


    Private Function SetEnabled(ByVal value As Boolean) As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.SetEnabled, Me.Infos.Handle, CInt(value), 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function
    Private Function SetOpacity(ByVal value As Byte) As Integer
        If _theAction Is Nothing Then
            _theAction = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf actionDone), _connection)
        End If
        Dim t As New System.Threading.WaitCallback(AddressOf _theAction.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackWindowAction.poolObj(asyncCallbackWindowAction.ASYNC_WINDOW_ACTION.SetOpacity, Me.Infos.Handle, CInt(value), 0, 0, newAction))
        AddPendingTask2(newAction, t)
    End Function


#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            Return PendingTaskCount.ToString
        End If

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Name", "Caption"
                res = Me.Caption
            Case "Handle", "Id"
                res = Me.Infos.Handle.ToString
            Case "Process"
                res = Me.Infos.ProcessId.ToString & " -- " & Me.Infos.ProcessName
            Case "Caption"
                res = Me.Caption
            Case "IsTask"
                res = Me.Infos.IsTask.ToString
            Case "Enabled"
                res = Me.Infos.Enabled.ToString
            Case "Visible"
                res = Me.Infos.Visible.ToString
            Case "ThreadId"
                res = Me.Infos.ThreadId.ToString
            Case "Opacity"
                res = Me.Infos.Opacity.ToString
            Case "Left"
                res = Me.Infos.Left.ToString
            Case "Height"
                res = Me.Infos.Height.ToString
            Case "Top"
                res = Me.Infos.Top.ToString
            Case "Width"
                res = Me.Infos.Width.ToString
        End Select

        Return res
    End Function


#End Region

    ' Get small icon handle of window
    Private Function GetWindowSmallIcon() As IntPtr
        Dim res As IntPtr
        res = API.SendMessage(_windowInfos.Handle, API.WM_GETICON, API.ICON_SMALL, 0)


        If res = IntPtr.Zero Then
            res = API.GetClassLong(_windowInfos.Handle, API.GCL_HICONSM)
        End If

        If res = IntPtr.Zero Then
            res = API.SendMessage(API.GetWindowLong(_windowInfos.Handle, API.GWL_HWNDPARENT), API.WM_GETICON, API.ICON_SMALL, 0)
        End If

        Return res
    End Function

#Region "Shared functions (local)"

    Public Shared Function GetForegroundAppPID() As Integer

    End Function

    ' Get all windows
    Public Shared Function CurrentLocalWindows(Optional ByVal all As Boolean = True) As Dictionary(Of String, cWindow)
        ' Local
        Dim currWnd As IntPtr
        Dim cpt As Integer

        Dim _dico As New Dictionary(Of String, cWindow)

        currWnd = API.GetWindowAPI(API.GetDesktopWindow(), API.GW_CHILD)
        cpt = 0
        Do While Not (currWnd = IntPtr.Zero)

            ' Get procId from hwnd
            Dim pid As Integer = asyncCallbackWindowEnumerate.GetProcIdFromWindowHandle(currWnd)
            'If all OrElse Array.IndexOf(pObj.pid, pid) >= 0 Then
            ' Then this window belongs to one of our processes
            'If all OrElse asyncCallbackWindowEnumerate.GetCaptionLenght(currWnd) > 0 Then
            Dim tid As Integer = asyncCallbackWindowEnumerate.GetThreadIdFromWindowHandle(currWnd)
            Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString
            If _dico.ContainsKey(key) = False Then
                _dico.Add(key, New cWindow(New windowInfos(pid, tid, currWnd)))
            End If
            'End If
            'End If

            currWnd = API.GetWindowAPI(currWnd, API.GW_HWNDNEXT)
        Loop

        Return _dico
    End Function

#End Region

End Class
