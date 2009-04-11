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
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackThreadEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cThreadConnection
        Public pid() As Integer
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cThreadConnection, ByVal pi() As Integer)
            ctrl = ctr
            deg = de
            con = co
            pid = pi
        End Sub
    End Structure

    ' When socket got a list of processes !
    Private Shared _poolObj As poolObj
    Friend Shared Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, threadInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), threadInfos))
            Next
        End If
        _poolObj.ctrl.Invoke(_poolObj.deg, True, dico, Nothing)
    End Sub
    Public Shared Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestThreadList, pObj.pid)
                    Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                    pObj.con.ConnectionObj.Socket.Send(buff, buff.Length)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim _dico As New Dictionary(Of String, threadInfos)

                For Each id As Integer In pObj.pid

                    Dim res As ManagementObjectCollection = Nothing
                    Try
                        res = pObj.con.wmiSearcher.Get()
                    Catch ex As Exception
                        pObj.ctrl.Invoke(pObj.deg, False, Nothing, ex.Message)
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
                                .ClientId.UniqueProcess = wmiId
                                .ClientId.UniqueThread = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.Handle.ToString))
                                .KernelTime = 10000 * CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.KernelModeTime.ToString))
                                .Priority = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.Priority.ToString))
                                Try
                                    .StartAddress = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.StartAddress.ToString))
                                Catch ex0 As Exception
                                    .StartAddress = -1
                                End Try
                                .State = CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ThreadState.ToString))
                                .UserTime = 10000 * CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.UserModeTime.ToString))
                                .WaitReason = CType(CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ThreadWaitReason.ToString)), API.KWAIT_REASON)
                                .WaitTime = 10000 * CInt(refThread.GetPropertyValue(API.WMI_INFO_THREAD.ElapsedTime.ToString))
                            End With
                            Dim _procInfos As New threadInfos(obj)
                            Dim _key As String = obj.ClientId.UniqueThread.ToString & "-" & obj.ClientId.UniqueProcess.ToString
                            If _dico.ContainsKey(_key) = False Then
                                _dico.Add(_key, _procInfos)
                            End If
                        End If
                    Next
                Next
                pObj.ctrl.Invoke(pObj.deg, True, _dico, Nothing)

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, threadInfos)
                For Each id As Integer In pObj.pid
                    If asyncCallbackProcEnumerate.AvailableThreads.ContainsKey(id) Then
                        For Each pair As System.Collections.Generic.KeyValuePair(Of String, threadInfos) In asyncCallbackProcEnumerate.AvailableThreads(id)
                            _dico.Add(pair.Key, pair.Value)
                        Next
                    End If
                Next
                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

    End Sub

End Class
