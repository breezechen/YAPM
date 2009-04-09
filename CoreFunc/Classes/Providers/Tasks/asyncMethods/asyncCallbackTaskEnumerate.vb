Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackTaskEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cWindowConnection
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cWindowConnection)
            ctrl = ctr
            deg = de
            con = co
        End Sub
    End Structure


    Public Shared Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim currWnd As IntPtr
                Dim cpt As Integer

                Dim _dico As New Dictionary(Of String, windowInfos.LightWindow)

                currWnd = API.GetWindowAPI(API.GetDesktopWindow(), API.GW_CHILD)
                cpt = 0
                Do While Not (currWnd = IntPtr.Zero)

                    If _isTask(currWnd) Then
                        Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)
                        Dim tid As Integer = GetThreadIdFromWindowHandle(currWnd)
                        Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString
                        If _dico.ContainsKey(key) = False Then
                            _dico.Add(key, New windowInfos.LightWindow(pid, tid, currWnd))
                        End If
                    End If

                    currWnd = API.GetWindowAPI(currWnd, API.GW_HWNDNEXT)
                Loop

                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

    End Sub


    ' Return process id from a handle
    Friend Shared Function GetProcIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
        Dim id As Integer = 0
        API.GetWindowThreadProcessId(hwnd, id)
        Return id
    End Function

    ' Return caption
    Friend Shared Function GetCaptionLenght(ByVal hwnd As IntPtr) As Integer
        Return API.GetWindowTextLength(hwnd)
    End Function

    ' Return thread id from a handle
    Friend Shared Function GetThreadIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
        Return API.GetWindowThreadProcessId(hwnd, 0)
    End Function

    Private Shared Function _isTask(ByVal hwnd As IntPtr) As Boolean
        ' Window must be visible
        If API.IsWindowVisible(hwnd) AndAlso (CInt(API.GetWindowLong(hwnd, API.GWL_HWNDPARENT)) = 0) AndAlso Not _
            (API.GetWindowTextLength(hwnd) = 0) Then
            ' Must not be taskmgr
            If GetWindowClass(hwnd) <> "Progman" Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Shared Function GetWindowClass(ByVal hWnd As IntPtr) As String
        Dim _class As New StringBuilder(Space(255))
        API.GetClassName(hWnd, _class, 255)
        Return _class.ToString
    End Function

End Class
