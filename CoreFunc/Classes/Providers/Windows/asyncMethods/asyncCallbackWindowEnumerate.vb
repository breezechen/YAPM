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

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cWindowConnection
        Public pid() As Integer
        Public all As Boolean
        Public unnamed As Boolean
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cWindowConnection, ByVal pi() As Integer, ByVal un As Boolean, ByVal al As Boolean)
            ctrl = ctr
            deg = de
            con = co
            pid = pi
            unnamed = un
            all = al
        End Sub
    End Structure

    ' When socket got a list of processes !
    Private Shared _poolObj As poolObj
    Friend Shared Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, windowInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), windowInfos))
            Next
        End If
        _poolObj.ctrl.Invoke(_poolObj.deg, True, dico, Nothing)
    End Sub

    Public Shared Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestWindowList)
                    Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                    pObj.con.ConnectionObj.Socket.Send(buff, buff.Length)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim currWnd As IntPtr
                Dim cpt As Integer

                Dim _dico As New Dictionary(Of String, windowInfos.LightWindow)

                currWnd = API.GetWindowAPI(API.GetDesktopWindow(), API.GW_CHILD)
                cpt = 0
                Do While Not (currWnd = IntPtr.Zero)

                    ' Get procId from hwnd
                    Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)
                    If pObj.all OrElse Array.IndexOf(pObj.pid, pid) >= 0 Then
                        ' Then this window belongs to one of our processes
                        If pObj.unnamed OrElse GetCaptionLenght(currWnd) > 0 Then
                            Dim tid As Integer = GetThreadIdFromWindowHandle(currWnd)
                            Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString
                            If _dico.ContainsKey(key) = False Then
                                _dico.Add(key, New windowInfos.LightWindow(pid, tid, currWnd))
                            End If
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

End Class
