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

Imports CoreFunc.cModuleConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackHandleEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cHandleConnection
        Public pid() As Integer
        Public unNamed As Boolean
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], _
                       ByRef co As cHandleConnection, ByVal pi() As Integer, _
                       ByVal unN As Boolean)
            ctrl = ctr
            deg = de
            con = co
            pid = pi
            unNamed = unN
        End Sub
    End Structure

    ' When socket got a list  !
    Private Shared _poolObj As poolObj
    Friend Shared Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, handleInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), handleInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestHandleList)
                    Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                    pObj.con.ConnectionObj.Socket.Send(buff, buff.Length)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, handleInfos)

                Call cHandle.handles_Renamed.Refresh(pObj.pid)

                Dim x As Integer = 0
                For i As Integer = 0 To cHandle.handles_Renamed.Count - 1
                    If cHandle.handles_Renamed.GetHandle(i) > 0 Then
                        If pObj.unNamed OrElse (Len(cHandle.handles_Renamed.GetObjectName(i)) > 0) Then
                            With cHandle.handles_Renamed
                                Dim retHandleCount As Integer
                                Dim retHandle As Integer
                                Dim retName As String
                                Dim retObjectCount As Integer
                                Dim retPid As Integer
                                Dim retPointerCount As Integer
                                Dim retType As String
                                With cHandle.handles_Renamed
                                    retHandleCount = .GetHandleCount(i)
                                    retHandle = .GetHandle(i)
                                    retName = .GetObjectName(i)
                                    retObjectCount = .GetObjectCount(i)
                                    retPid = .GetProcessID(i)
                                    retPointerCount = .GetPointerCount(i)
                                    retType = .GetNameInformation(i)
                                End With
                                Dim _key As String = retPid.ToString & "-" & retHandle.ToString & "-" & retType & "-" & retName
                                Dim ret As New handleInfos(retHandle, retType, retPid, retName, retHandleCount, retPointerCount, retObjectCount)
                                _dico.Add(_key, ret)
                            End With
                        End If
                    End If
                Next

                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

    End Sub

End Class
