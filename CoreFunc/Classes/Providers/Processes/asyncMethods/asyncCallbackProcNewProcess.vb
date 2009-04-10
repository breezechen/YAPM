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

    Private _path As String
    Private _connection As cProcessConnection
    Private _deg As HasCreated

    Public Delegate Sub HasCreated(ByVal Success As Boolean, ByVal path As String, ByVal msg As String)

    Public Sub New(ByVal deg As HasCreated, ByVal path As String, ByRef procConnection As cProcessConnection)
        _path = path
        _deg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim objectGetOptions As New ObjectGetOptions()
                    Dim managementPath As New ManagementPath("Win32_Process")
                    Dim processClass As New ManagementClass(_connection.wmiSearcher.Scope, managementPath, objectGetOptions)
                    Dim inParams As ManagementBaseObject = processClass.GetMethodParameters("Create")
                    inParams("CommandLine") = _path
                    Dim outParams As ManagementBaseObject = processClass.InvokeMethod("Create", inParams, Nothing)
                    Dim res As Integer = CInt(outParams("ProcessId"))
                    _deg.invoke(res > 0, _path, CType(res, API.PROCESS_RETURN_CODE_WMI).ToString)
                Catch ex As Exception
                    _deg.invoke(False, _path, ex.Message)
                End Try

            Case Else
                ' Local
                ' OK, normally the local startNewProcess is not done here
                ' because of RunBox need
                Dim pid As Integer = Shell(_path)
                _deg.invoke(pid <> 0, _path, API.GetError)
        End Select
    End Sub

End Class
