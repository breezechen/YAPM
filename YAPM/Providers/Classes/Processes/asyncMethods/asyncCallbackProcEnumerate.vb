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
Imports Common.Misc

Public Class asyncCallbackProcEnumerate

    Private processMinRights As Native.Security.ProcessAccess = Native.Security.ProcessAccess.QueryInformation

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cProcessConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cProcessConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
        If cEnvironment.SupportsMinRights Then
            processMinRights = Native.Security.ProcessAccess.QueryLimitedInformation
        End If
    End Sub

#Region "Shared code (reanalizing of processes)"

    ' Reanalize a process by removing (or asking to remove) its PID from
    ' the shared dictionnary of known PID
    Public Structure ReanalizeProcessObj
        Public pid() As Integer
        Public con As cProcessConnection
        Public Sub New(ByRef pi() As Integer, ByRef co As cProcessConnection)
            pid = pi
            con = co
        End Sub
    End Structure
    Public Shared Sub ReanalizeProcess(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As ReanalizeProcessObj = DirectCast(thePoolObj, ReanalizeProcessObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessReanalize, pObj.pid)
                    pObj.con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.LocalConnection, cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Native.Objects.Process.RemoveProcessesFromListOfNewProcesses(pObj.pid)

        End Select

        sem.Release()
    End Sub

    ' Called to remove PIDs from shared dico by the server after it receive
    ' a command to reanalize some PIDs
    Public Shared Sub ReanalizeLocalAfterSocket(ByRef pid() As Integer)
        Native.Objects.Process.RemoveProcessesFromListOfNewProcesses(pid)
    End Sub

#End Region

    Public Structure poolObj
        Public method As ProcessEnumMethode
        Public forInstanceId As Integer
        Public force As Boolean
        Public Sub New(ByVal iid As Integer, Optional ByVal tmethod As ProcessEnumMethode = ProcessEnumMethode.VisibleProcesses, Optional ByVal forceAllInfos As Boolean = False)
            forInstanceId = iid
            method = tmethod
            force = forceAllInfos
        End Sub
    End Structure
    Public Enum ProcessEnumMethode As Integer
        VisibleProcesses
        BruteForce
        HandleMethod
    End Enum


    ' When socket got a list of processes !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim _dico As New Dictionary(Of String, processInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), processInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        Native.Objects.Process.CurrentProcesses = _dico

        Try
            If deg IsNot Nothing AndAlso ctrl.Created Then _
                ctrl.Invoke(deg, True, _dico, Nothing, _instanceId)
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Friend Shared sem As New System.Threading.Semaphore(1, 1)
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestProcessList)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim _dico As New Dictionary(Of String, processInfos)
                Dim msg As String = ""
                Dim res As Boolean = _
                    Wmi.Objects.Process.EnumerateProcesses(con.wmiSearcher, _dico, msg)

                ' Save current processes into a dictionary
                Native.Objects.Process.CurrentProcesses = _dico

                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, res, _dico, msg, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot

                Dim _dico As New Dictionary(Of String, processInfos)
                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then
                    _dico = snap.Processes
                End If

                ' Save current processes into a dictionary
                Native.Objects.Process.CurrentProcesses = _dico

                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case Else
                ' Local
                Dim _dico As Dictionary(Of String, processInfos)

                Static hasFailedAtLeastOnce As Boolean = False

                If hasFailedAtLeastOnce = False Then
                    If deg Is Nothing OrElse ctrl.Created = False Then
                        ' We won't be able to invoke method, so we have to clear the
                        ' list of new processes when we will able to invoke it
                        hasFailedAtLeastOnce = True
                    End If
                Else
                    If deg IsNot Nothing AndAlso ctrl.Created Then
                        ' OK, we'll be able to invoke method, and it has failed
                        ' last time, so we clear the dico of new processes
                        ' That's how we'll be able to retrieve and display
                        ' the fixed info
                        Native.Objects.Process.ClearNewProcessesDico()
                        ' Set hasFailedAtLeastOnce = false so we won't clear dico
                        ' each time
                        hasFailedAtLeastOnce = False
                    End If
                End If

                Select Case pObj.method
                    Case ProcessEnumMethode.BruteForce
                        _dico = Native.Objects.Process.EnumerateHiddenProcessesBruteForce
                    Case ProcessEnumMethode.HandleMethod
                        _dico = Native.Objects.Process.EnumerateHiddenProcessesHandleMethod
                    Case Else
                        _dico = Native.Objects.Process.EnumerateVisibleProcesses(pObj.force)
                End Select

                ' Save current processes into a dictionary
                Native.Objects.Process.CurrentProcesses = _dico

                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                    End If
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
        End Select

        sem.Release()

    End Sub

    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate(ByVal forceAllInfos As Boolean, ByVal pObj As poolObj) As Dictionary(Of String, processInfos)
        Dim _dico As Dictionary(Of String, processInfos)

        Select Case pObj.method
            Case ProcessEnumMethode.BruteForce
                _dico = Native.Objects.Process.EnumerateHiddenProcessesBruteForce
            Case ProcessEnumMethode.HandleMethod
                _dico = Native.Objects.Process.EnumerateHiddenProcessesHandleMethod
            Case Else
                _dico = Native.Objects.Process.EnumerateVisibleProcesses(forceAllInfos)
        End Select

        Return _dico
    End Function

End Class
