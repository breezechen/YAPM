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

Public Class asyncCallbackServiceEnumerate

#Region "Shared code (for dico of services)"

    Public Shared Sub ClearDico()
        Native.Objects.Service.ClearNewServicesList()
    End Sub

#End Region

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cServiceConnection
    Private _instanceId As Integer

    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cServiceConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public all As Boolean
        Public forInstanceId As Integer
        Public complete As Boolean
        Public Sub New(ByVal pi As Integer, ByVal al As Boolean, ByVal comp As Boolean, ByVal forII As Integer)
            all = al
            forInstanceId = forII
            pid = pi
            complete = comp
        End Sub
    End Structure


    ' When socket got a list !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, serviceInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), serviceInfos))
            Next
        End If

        Try
            If deg IsNot Nothing AndAlso ctrl.Created Then _
                ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub
    Public Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestServiceList, pObj.pid, pObj.all)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim _dico As New Dictionary(Of String, serviceInfos)
                Dim res As Boolean
                Dim msg As String = ""

                res = Wmi.Objects.Service.EnumerateProcesses(pObj.pid, pObj.all, _
                                                             con.wmiSearcher, _dico, msg)
                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, res, _dico, msg, 0)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot file

                Dim _dico As New Dictionary(Of String, serviceInfos)
                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then
                    If pObj.all Then
                        ' All services
                        _dico = snap.Services
                    Else
                        ' For one process only
                        _dico = snap.ServicesByProcessId(pObj.pid)
                    End If
                End If
                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case Else
                ' Local

                Dim _dico As New Dictionary(Of String, serviceInfos)

                Native.Objects.Service.EnumerateServices(con.SCManagerLocalHandle, _dico, pObj.all, _
                                                 pObj.complete, pObj.pid)

                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

        End Select

        sem.Release()

    End Sub


    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj, ByVal con As cServiceConnection) As Dictionary(Of String, serviceInfos)
        Dim _dico As New Dictionary(Of String, serviceInfos)

        Native.Objects.Service.EnumerateServices(con.SCManagerLocalHandle, _dico, pObj.all, _
                                                 pObj.complete, pObj.pid)

        Return _dico
    End Function

End Class
