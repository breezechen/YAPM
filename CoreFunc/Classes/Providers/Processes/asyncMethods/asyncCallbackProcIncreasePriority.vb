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

Public Class asyncCallbackProcIncreasePriority

    Private con As cProcessConnection
    Private _deg As HasIncreasedPriority

    Public Delegate Sub HasIncreasedPriority(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasIncreasedPriority, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public level As Integer
        Public Sub New(ByVal pi As Integer, ByVal _level As Integer, ByVal act As Integer)
            level = _level
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

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim _newlevel As ProcessPriorityClass
                    Select Case pObj.level
                        Case ProcessPriorityClass.AboveNormal
                            _newlevel = ProcessPriorityClass.High
                        Case ProcessPriorityClass.BelowNormal
                            _newlevel = ProcessPriorityClass.Normal
                        Case ProcessPriorityClass.High
                            _newlevel = ProcessPriorityClass.RealTime
                        Case ProcessPriorityClass.Idle
                            _newlevel = ProcessPriorityClass.BelowNormal
                        Case ProcessPriorityClass.Normal
                            _newlevel = ProcessPriorityClass.AboveNormal
                        Case ProcessPriorityClass.RealTime
                            '
                    End Select

                    Dim res As Integer = 2        ' Access denied
                    For Each srv As ManagementObject In con.wmiSearcher.Get
                        If CInt(srv.GetPropertyValue(API.WMI_INFO_PROCESS.ProcessId.ToString)) = pObj.pid Then
                            Dim inParams As ManagementBaseObject = srv.GetMethodParameters("SetPriority")
                            inParams("Priority") = _newlevel
                            Dim outParams As ManagementBaseObject = srv.InvokeMethod("SetPriority", inParams, Nothing)
                            res = CInt(outParams("ReturnValue"))
                            Exit For
                        End If
                    Next
                    _deg.Invoke(res = 0, CType(res, API.PROCESS_RETURN_CODE_WMI).ToString, pObj.newAction)
                Catch ex As Exception
                    _deg.Invoke(False, ex.Message, pObj.newAction)
                End Try

            Case Else
                ' Local
                Dim hProc As Integer
                Dim r As Integer
                Dim _newlevel As ProcessPriorityClass
                Select Case pObj.level
                    Case ProcessPriorityClass.AboveNormal
                        _newlevel = ProcessPriorityClass.High
                    Case ProcessPriorityClass.BelowNormal
                        _newlevel = ProcessPriorityClass.Normal
                    Case ProcessPriorityClass.High
                        _newlevel = ProcessPriorityClass.RealTime
                    Case ProcessPriorityClass.Idle
                        _newlevel = ProcessPriorityClass.BelowNormal
                    Case ProcessPriorityClass.Normal
                        _newlevel = ProcessPriorityClass.AboveNormal
                    Case ProcessPriorityClass.RealTime
                        '
                End Select
                hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_INFORMATION, 0, pObj.pid)
                If hProc > 0 Then
                    r = API.SetPriorityClass(hProc, _newlevel)
                    API.CloseHandle(hProc)
                    _deg.Invoke(r <> 0, API.GetError, pObj.newAction)
                Else
                    _deg.Invoke(False, API.GetError, pObj.newAction)
                End If
        End Select
    End Sub

End Class
