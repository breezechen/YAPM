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
Imports System.Management

Public Class asyncCallbackProcNewProcess

    Private con As cProcessConnection
    Private _deg As HasCreated

    Public Delegate Sub HasCreated(ByVal Success As Boolean, ByVal path As String, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasCreated, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public path As String
        Public newAction As Integer
        Public Sub New(ByVal s As String, ByVal act As Integer)
            newAction = act
            path = s
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessCreateNew, pObj.path)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim objectGetOptions As New ObjectGetOptions()
                    Dim managementPath As New ManagementPath("Win32_Process")
                    Dim processClass As New ManagementClass(con.wmiSearcher.Scope, managementPath, objectGetOptions)
                    Dim inParams As ManagementBaseObject = processClass.GetMethodParameters("Create")
                    inParams("CommandLine") = pObj.path
                    Dim outParams As ManagementBaseObject = processClass.InvokeMethod("Create", inParams, Nothing)
                    Dim res As Integer = CInt(outParams("ProcessId"))
                    _deg.Invoke(res > 0, pObj.path, CType(res, API.PROCESS_RETURN_CODE_WMI).ToString, pObj.newAction)
                Catch ex As Exception
                    _deg.Invoke(False, pObj.path, ex.Message, pObj.newAction)
                End Try

            Case Else
                ' Local
                ' OK, normally the local startNewProcess is not done here
                ' because of RunBox need
                Dim pid As Integer = Shell(pObj.path)
                _deg.Invoke(pid <> 0, pObj.path, API.GetError, pObj.newAction)
        End Select
    End Sub

End Class
