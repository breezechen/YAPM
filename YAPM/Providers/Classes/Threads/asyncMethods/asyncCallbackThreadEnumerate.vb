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

Public Class asyncCallbackThreadEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cThreadConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cThreadConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid() As Integer
        Public forInstanceId As Integer
        Public Sub New(ByVal pi() As Integer, ByVal iid As Integer)
            forInstanceId = iid
            pid = pi
        End Sub
    End Structure

    ' When socket got a list of processes !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, threadInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), threadInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestThreadList, pObj.pid)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim _dico As New Dictionary(Of String, threadInfos)

                For Each id As Integer In pObj.pid

                    Dim res As ManagementObjectCollection = Nothing
                    Try
                        res = con.wmiSearcher.Get()
                    Catch ex As Exception
                        If deg IsNot Nothing AndAlso ctrl.Created Then _
                            ctrl.Invoke(deg, False, Nothing, ex.Message)
                        sem.Release()
                        Exit Sub
                    End Try

                    For Each refThread As Management.ManagementObject In res

                        Dim wmiId As Integer = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ProcessHandle.ToString))
                        Dim ex As Boolean = False
                        For Each ii As Integer In pObj.pid
                            If ii = wmiId Then
                                ex = True
                                Exit For
                            End If
                        Next
                        ' If we have to get threads for this process...
                        If ex Then
                            Dim obj As New API.SYSTEM_THREAD_INFORMATION
                            With obj
                                .BasePriority = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.PriorityBase.ToString))
                                .CreateTime = 0
                                .ClientId = New API.CLIENT_ID(wmiId, _
                                                              CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.Handle.ToString)))
                                .KernelTime = 10000 * CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.KernelModeTime.ToString))
                                .Priority = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.Priority.ToString))
                                Try
                                    .StartAddress = CType(refThread.GetPropertyValue(API.WMI_INFO_THREAD.StartAddress.ToString), IntPtr)
                                Catch ex0 As Exception
                                    .StartAddress = New IntPtr(-1)
                                End Try
                                .State = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ThreadState.ToString))
                                .UserTime = 10000 * CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.UserModeTime.ToString))
                                .WaitReason = CType(CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ThreadWaitReason.ToString)), API.KWAIT_REASON)
                                Try
                                    .WaitTime = 10000 * CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ElapsedTime.ToString))
                                Catch ex1 As Exception
                                    '
                                End Try
                            End With
                            Dim _procInfos As New threadInfos(obj)
                            Dim _key As String = obj.ClientId.UniqueThread.ToString & "-" & obj.ClientId.UniqueProcess.ToString
                            If _dico.ContainsKey(_key) = False Then
                                _dico.Add(_key, _procInfos)
                            End If
                        End If
                    Next
                Next

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Nothing, pObj.forInstanceId)

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, threadInfos)

                Call enumThreads(pObj, _dico)

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub

    ' Enumerate threads (local)
    ' Memory allocation for process 
    Private Shared memAllocForThreadEnum As New Native.Memory.MemoryAlloc(&H1000)
    Friend Shared Sub enumThreads(ByVal pObj As poolObj, ByRef _dico As Dictionary(Of String, threadInfos))

        Dim ret As Integer
        API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessesAndThreadsInformation, _
                        memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)
        If memAllocForThreadEnum.Size < ret Then
            memAllocForThreadEnum.Resize(ret)
        End If
        API.NtQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessesAndThreadsInformation, _
                        memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)

        ' Extract structures from unmanaged memory
        Dim x As Integer = 0
        Dim offset As Integer = 0
        Do While True

            Dim obj As API.SYSTEM_PROCESS_INFORMATION = _
                    memAllocForThreadEnum.ReadStructOffset(Of API.SYSTEM_PROCESS_INFORMATION)(offset)

            ' Do we have do get threads for this process ?
            Dim bHaveToGetThreads As Boolean = False
            For Each tPid As Integer In pObj.pid
                If tPid = obj.ProcessId Then
                    bHaveToGetThreads = True
                    Exit For
                End If
            Next

            If bHaveToGetThreads Then
                For j As Integer = 0 To obj.NumberOfThreads - 1

                    Dim _off As Integer = offset + _
                            Marshal.SizeOf(GetType(API.SYSTEM_PROCESS_INFORMATION)) + j _
                            * Marshal.SizeOf(GetType(API.SYSTEM_THREAD_INFORMATION))

                    Dim thread As API.SYSTEM_THREAD_INFORMATION = _
                        memAllocForThreadEnum.ReadStructOffset(Of API.SYSTEM_THREAD_INFORMATION)(_off)

                    Dim _key As String = thread.ClientId.UniqueThread.ToString & "-" & thread.ClientId.UniqueProcess.ToString
                    Dim _th As New threadInfos(thread)
                    If _dico.ContainsKey(_key) = False Then
                        _dico.Add(_key, _th)
                    End If
                Next
            End If

            offset += obj.NextEntryOffset

            If obj.NextEntryOffset = 0 Then
                Exit Do
            End If
            x += 1
        Loop

    End Sub

End Class
