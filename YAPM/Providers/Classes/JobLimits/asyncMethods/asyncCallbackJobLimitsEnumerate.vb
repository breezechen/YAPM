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
Imports System.Windows.Forms
Imports System.Management
Imports System.Net

Public Class asyncCallbackJobLimitsEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cJobLimitConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cJobLimitConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public JobName As String
        Public forInstanceId As Integer
        Public Sub New(ByVal _jobName As String, ByVal ii As Integer)
            forInstanceId = ii
            JobName = _jobName
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, jobLimitInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), jobLimitInfos))
            Next
        End If
        If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        Try
            sem.WaitOne()

            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If con.ConnectionObj.IsConnected = False Then
                Exit Sub
            End If

            Select Case con.ConnectionObj.ConnectionType

                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    _poolObj = pObj
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestJobLimits, pObj.JobName)
                        cDat.InstanceId = _instanceId   ' Instance which request the list
                        con.ConnectionObj.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    ' TODO

                Case cConnection.TypeOfConnection.SnapshotFile
                    ' Snapshot

                    Dim _dico As New Dictionary(Of String, jobLimitInfos)
                    Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                    If snap IsNot Nothing Then
                        _dico = snap.JobLimitsByJobName(pObj.JobName)
                    End If
                    Try
                        If deg IsNot Nothing AndAlso ctrl.Created Then _
                            ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try

                Case Else
                    ' Local

                    Dim _dico As Dictionary(Of String, jobLimitInfos) = _
                            Native.Objects.Job.EnumerateJobLimitsByJobName(pObj.JobName)

                    Try
                        If deg IsNot Nothing AndAlso ctrl.Created Then _
                            ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try

            End Select

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            sem.Release()
        End Try

    End Sub


    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj) As Dictionary(Of String, jobLimitInfos)
        Dim _dico As Dictionary(Of String, jobLimitInfos) = _
                        Native.Objects.Job.EnumerateJobLimitsByJobName(pObj.JobName)
        Return _dico
    End Function

End Class
