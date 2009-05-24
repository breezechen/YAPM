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

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackWindowEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cWindowConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cWindowConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid() As Integer
        Public all As Boolean
        Public forInstanceId As Integer
        Public unnamed As Boolean
        Public Sub New(ByRef pi() As Integer, ByVal al As Boolean, ByVal unn As Boolean, ByVal ii As Integer)
            forInstanceId = ii
            pid = pi
            all = al
            unnamed = unn
        End Sub
    End Structure


    ' When socket got a list of processes !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, windowInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), windowInfos))
            Next
        End If
        If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestWindowList, pObj.pid, pObj.unnamed, pObj.all)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim currWnd As IntPtr
                Dim cpt As Integer

                Dim _dico As New Dictionary(Of String, windowInfos)

                currWnd = API.GetWindowAPI(API.GetDesktopWindow(), API.GW_CHILD)
                cpt = 0
                Do While Not (currWnd = IntPtr.Zero)

                    ' Get procId from hwnd
                    Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)
                    If pObj.all OrElse Array.IndexOf(pObj.pid, pid) >= 0 Then
                        ' Then this window belongs to one of our processes
                        Dim sCap As String = GetCaption(currWnd)
                        If pObj.unnamed OrElse sCap.Length > 0 Then
                            Dim tid As Integer = GetThreadIdFromWindowHandle(currWnd)
                            Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString
                            If _dico.ContainsKey(key) = False Then
                                _dico.Add(key, New windowInfos(pid, tid, currWnd, sCap))
                            End If
                        End If
                    End If

                    currWnd = API.GetWindowAPI(currWnd, API.GW_HWNDNEXT)
                Loop

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, API.GetError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub


    ' Return process id from a handle
    Friend Shared Function GetProcIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
        Dim id As Integer = 0
        API.GetWindowThreadProcessId(hwnd, id)
        Return id
    End Function

    ' Return caption
    Friend Shared Function GetCaption(ByVal hWnd As IntPtr) As String
        Dim length As Integer
        length = API.GetWindowTextLength(hWnd)
        If length > 0 Then
            Dim _cap As New StringBuilder(Space(length + 1))
            length = API.GetWindowText(hWnd, _cap, length + 1)
            Return _cap.ToString.Substring(0, Len(_cap.ToString))
        Else
            Return ""
        End If
    End Function

    ' Return thread id from a handle
    Friend Shared Function GetThreadIdFromWindowHandle(ByVal hwnd As IntPtr) As Integer
        Return API.GetWindowThreadProcessId(hwnd, 0)
    End Function

End Class
