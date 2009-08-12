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
Imports System.Text

Public Class asyncCallbackProcEmptyWorkingSet

    Private con As cProcessConnection
    Private _deg As HasReducedWorkingSet

    Public Delegate Sub HasReducedWorkingSet(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasReducedWorkingSet, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal act As Integer)
            newAction = act
            pid = pi
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessReduceWorkingSet, pObj.pid)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _hHandle As IntPtr = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_QUOTA, _
                                                         False, pObj.pid)
                If _hHandle <> IntPtr.Zero Then
                    Dim _ret As Integer = API.SetProcessWorkingSetSize(_hHandle, -1, -1)
                    API.CloseHandle(_hHandle)
                    _deg.Invoke(_ret <> 0, Native.Api.Win32.GetLastError, pObj.newAction)
                Else
                    _deg.Invoke(False, Native.Api.Win32.GetLastError, pObj.newAction)
                End If
        End Select
    End Sub

End Class
