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
Imports System.Management

Public Class asyncCallbackProcIncreasePriority

    Private _deg As HasIncreasedPriority

    Public Delegate Sub HasIncreasedPriority(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasIncreasedPriority)
        _deg = deg
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
        If Program.Connection.IsConnected Then

            Select Case Program.Connection.Type
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessIncreasePriority, pObj.pid)
                        Program.Connection.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
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

                    Dim msg As String = ""
                    Dim ret As Boolean = _
                            Wmi.Objects.Process.SetProcessPriorityById(pObj.pid, _newlevel, _
                                                                       ProcessProvider.wmiSearcher, msg)
                    Try
                        _deg.Invoke(ret, msg, pObj.newAction)
                    Catch ex As Exception
                        _deg.Invoke(False, ex.Message, pObj.newAction)
                    End Try

                Case Else
                    ' Local
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
                    Dim r As Boolean = Native.Objects.Process.SetProcessPriorityById(pObj.pid, _newlevel)
                    _deg.Invoke(r, Native.Api.Win32.GetLastError, pObj.newAction)
            End Select
        End If
    End Sub

End Class
