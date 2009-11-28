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

Public Class asyncCallbackModuleEnumerate

    ' Some material to retrieve infos about files ONCE
    Friend Shared fileInformations As New Dictionary(Of String, SerializableFileVersionInfo)
    Friend Shared semDicoFileInfos As New System.Threading.Semaphore(1, 1)

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cModuleConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cModuleConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public forInstanceId As Integer
        Public pid() As Integer
        Public Sub New(ByVal pi() As Integer, ByVal iid As Integer)
            forInstanceId = iid
            pid = pi
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, moduleInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), moduleInfos))
            Next
        End If
        If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestModuleList, pObj.pid)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim _dico As New Dictionary(Of String, moduleInfos)
                Dim msg As String = ""
                Dim res As Boolean = _
                        Wmi.Objects.Module.EnumerateModuleByIds(pObj.pid, con.wmiSearcher, _
                                                                _dico, msg)

                ctrl.Invoke(deg, res, _dico, msg, pObj.forInstanceId)

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot

                Dim _dico As New Dictionary(Of String, moduleInfos)
                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then
                    ' For some processes only
                    For Each pid As Integer In pObj.pid
                        Dim _modules As Dictionary(Of String, moduleInfos) = snap.ModulesByProcessId(pid)
                        If _modules IsNot Nothing Then
                            For Each pair As System.Collections.Generic.KeyValuePair(Of String, moduleInfos) In _modules
                                _dico.Add(pair.Key, pair.Value)
                            Next
                        End If
                    Next
                End If
                Try
                    'If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case Else
                ' Local
                Dim _dico As Dictionary(Of String, moduleInfos) = _
                                            SharedLocalSyncEnumerate(pObj)

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)

        End Select


        sem.Release()

    End Sub


    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj) As Dictionary(Of String, moduleInfos)
        Dim _dico As Dictionary(Of String, moduleInfos)

        ' If it's a Wow64 process, module enumeration is made using debug functions
        Dim cProc As cProcess = cProcess.GetProcessById(pObj.pid(0))
        If cProc IsNot Nothing AndAlso cProc.IsWow64Process Then
            _dico = Native.Objects.Module.EnumerateModulesWow64ByProcessId(pObj.pid(0), False)
        Else
            ' Normal native enumeration
            _dico = Native.Objects.Module.EnumerateModulesByProcessIds(pObj.pid, False)
        End If

        Return _dico
    End Function

End Class
