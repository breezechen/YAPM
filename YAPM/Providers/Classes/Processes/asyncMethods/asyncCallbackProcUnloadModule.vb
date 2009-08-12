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

Public Class asyncCallbackProcUnloadModule

    Private con As cProcessConnection
    Private _deg As HasUnloadedModule

    Public Delegate Sub HasUnloadedModule(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasUnloadedModule, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub
    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public baseA As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal add As Integer, _
                       ByVal act As Integer)
            baseA = add
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ModuleUnload, pObj.pid, pObj.baseA)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As IntPtr = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_CREATE_THREAD, False, pObj.pid)

                If hProc <> IntPtr.Zero Then
                    Dim kernel32 As IntPtr = API.GetModuleHandle("kernel32.dll")
                    Dim freeLibrary As Integer = API.GetProcAddress(kernel32, "FreeLibrary")
                    Dim threadId As Integer
                    Dim ret As Integer = API.CreateRemoteThread(hProc, 0, 0, freeLibrary, pObj.baseA, 0, threadId)
                    _deg.Invoke(ret <> 0, pObj.pid, Native.Api.Win32.GetLastError, pObj.newAction)
                Else
                    _deg.Invoke(False, pObj.pid, Native.Api.Win32.GetLastError, pObj.newAction)
                End If

        End Select
    End Sub

End Class
