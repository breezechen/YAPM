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
Imports Native.Api

Public Class asyncCallbackJobLimitsSetLimits

    Private _deg As HasSetLimits

    Public Delegate Sub HasSetLimits(ByVal Success As Boolean, ByVal jobName As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasSetLimits)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public jobName As String
        Public struct1 As NativeStructs.JobObjectBasicUiRestrictions
        Public struct2 As NativeStructs.JobObjectExtendedLimitInformation
        Public newAction As Integer
        Public Sub New(ByVal name As String, _
                       ByVal s1 As NativeStructs.JobObjectBasicUiRestrictions, _
                       ByVal s2 As NativeStructs.JobObjectExtendedLimitInformation, _
                       ByVal act As Integer)
            newAction = act
            jobName = name
            struct1 = s1
            struct2 = s2
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If Program.Connection.IsConnected = False Then
            Exit Sub
        End If

        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.JobSetLimits, pObj.jobName, pObj.struct1, pObj.struct2)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Job.SetJobCommonLimitsByName(pObj.jobName, _
                                                        pObj.struct1, pObj.struct2)
                _deg.Invoke(ret, pObj.jobName, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
