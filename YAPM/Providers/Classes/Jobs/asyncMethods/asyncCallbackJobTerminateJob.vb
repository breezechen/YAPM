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

Public Class asyncCallbackJobTerminateJob

    Private con As cJobConnection
    Private _deg As HasTerminatedJob

    Public Delegate Sub HasTerminatedJob(ByVal Success As Boolean, ByVal jobName As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasTerminatedJob, ByRef jobConnection As cJobConnection)
        _deg = deg
        con = jobConnection
    End Sub

    Public Structure poolObj
        Public jobName As String
        Public newAction As Integer
        Public Sub New(ByVal name As String, _
                       ByVal act As Integer)
            newAction = act
            jobName = name
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                'Try
                '    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.HandleClose, pObj.pid, pObj.handle)
                '    con.ConnectionObj.Socket.Send(cDat)
                'Catch ex As Exception
                '    MsgBox(ex.Message)
                'End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Job.TerminateJobByJobName(pObj.jobName)
                _deg.Invoke(ret, pObj.jobName, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
