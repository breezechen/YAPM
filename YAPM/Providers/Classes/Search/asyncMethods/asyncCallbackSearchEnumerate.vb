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

Public Class asyncCallbackSearchEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cSearchConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cSearchConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public strSearch As String
        Public caseSen As Boolean
        Public includ As searchInfos.SearchInclude
        Public forInstanceId As Integer
        Public Sub New(ByRef strToSearch As String, ByVal [case] As Boolean, ByVal include As searchInfos.SearchInclude, ByVal ii As Integer)
            strSearch = strToSearch
            caseSen = [case]
            includ = include
            forInstanceId = ii
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, searchInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), searchInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestSearchList, pObj.strSearch, pObj.includ, pObj.caseSen)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI


            Case Else
                ' Local

                Dim _dico As New Dictionary(Of String, searchInfos)
                Dim key As Integer = 0

                Dim sToSearch As String = pObj.strSearch
                If pObj.caseSen = False Then
                    sToSearch = sToSearch.ToLowerInvariant
                End If

                ' ---- PROCESSES
                If (pObj.includ And searchInfos.SearchInclude.SearchProcesses) = searchInfos.SearchInclude.SearchProcesses Then
                    Native.Objects.Process.SemCurrentProcesses.WaitOne()
                    If Native.Objects.Process.CurrentProcesses IsNot Nothing Then
                        Dim _tmpDico As New Dictionary(Of String, cProcess)
                        _tmpDico = Native.Objects.Process.CurrentProcesses
                        For Each cp As cProcess In _tmpDico.Values
                            For Each field As String In processInfos.GetAvailableProperties
                                Dim scomp As String = cp.GetInformation(field)
                                If scomp IsNot Nothing Then
                                    If pObj.caseSen = False Then
                                        scomp = scomp.ToLowerInvariant
                                    End If
                                    If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                        ' Found an item
                                        Dim newItFound As New searchInfos(cp.Infos.Pid, field, searchInfos.ResultType.Process, scomp)
                                        key += 1
                                        _dico.Add(key.ToString, newItFound)
                                    End If
                                End If
                            Next

                            ' ---- MODULES
                            If (pObj.includ And searchInfos.SearchInclude.SearchModules) = searchInfos.SearchInclude.SearchModules Then
                                Dim _tmpDico2 As Dictionary(Of String, cModule) = cModule.CurrentLocalModules(cp.Infos.Pid)
                                For Each cm As cModule In _tmpDico2.Values
                                    For Each field2 As String In processInfos.GetAvailableProperties
                                        Dim scomp2 As String = cm.GetInformation(field2)
                                        If scomp2 IsNot Nothing Then
                                            If pObj.caseSen = False Then
                                                scomp2 = scomp2.ToLowerInvariant
                                            End If
                                            If InStr(scomp2, sToSearch, CompareMethod.Binary) > 0 Then
                                                ' Found an item
                                                Dim newItFound As New searchInfos(cp.Infos.Pid, field2, searchInfos.ResultType.Module, scomp2, "", IntPtr.Zero, cm.Infos.BaseAddress, cm.Infos.Name)
                                                key += 1
                                                _dico.Add(key.ToString, newItFound)
                                            End If
                                        End If
                                    Next
                                Next
                            End If

                            ' ---- ENVVARIABLES
                            If (pObj.includ And searchInfos.SearchInclude.SearchEnvVar) = searchInfos.SearchInclude.SearchEnvVar Then
                                Dim _tmpDico2 As Dictionary(Of String, cEnvVariable) = cEnvVariable.CurrentEnvVariables(cp)
                                For Each cm As cEnvVariable In _tmpDico2.Values
                                    For Each field2 As String In envVariableInfos.GetAvailableProperties
                                        Dim scomp2 As String = cm.GetInformation(field2)
                                        If scomp2 IsNot Nothing Then
                                            If pObj.caseSen = False Then
                                                scomp2 = scomp2.ToLowerInvariant
                                            End If
                                            If InStr(scomp2, sToSearch, CompareMethod.Binary) > 0 Then
                                                ' Found an item
                                                Dim newItFound As New searchInfos(cp.Infos.Pid, field2, searchInfos.ResultType.EnvironmentVariable, scomp2)
                                                key += 1
                                                _dico.Add(key.ToString, newItFound)
                                            End If
                                        End If
                                    Next
                                Next
                            End If

                        Next
                    End If
                    Native.Objects.Process.SemCurrentProcesses.Release()
                End If

                ' ---- SERVICES
                If (pObj.includ And searchInfos.SearchInclude.SearchServices) = searchInfos.SearchInclude.SearchServices Then
                    Native.Objects.Service.SemCurrentServices.WaitOne()
                    If Native.Objects.Service.CurrentServices IsNot Nothing Then
                        Dim _tmpDico As New Dictionary(Of String, cService)
                        _tmpDico = Native.Objects.Service.CurrentServices
                        For Each cp As cService In _tmpDico.Values
                            For Each field As String In serviceInfos.GetAvailableProperties
                                Dim scomp As String = cp.GetInformation(field)
                                If scomp IsNot Nothing Then
                                    If pObj.caseSen = False Then
                                        scomp = scomp.ToLowerInvariant
                                    End If
                                    If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                        ' Found an item
                                        Dim newItFound As New searchInfos(cp.Infos.ProcessId, field, searchInfos.ResultType.Service, scomp, cp.Infos.Name, IntPtr.Zero, IntPtr.Zero)
                                        key += 1
                                        _dico.Add(key.ToString, newItFound)
                                    End If
                                End If
                            Next
                        Next
                    End If
                    Native.Objects.Service.SemCurrentServices.Release()
                End If


                ' ---- HANDLES
                If (pObj.includ And searchInfos.SearchInclude.SearchHandles) = searchInfos.SearchInclude.SearchHandles Then
                    Dim _tmpDico As New Dictionary(Of String, cHandle)
                    _tmpDico = Native.Objects.Handle.EnumerateCurrentLocalHandles
                    For Each cp As cHandle In _tmpDico.Values
                        For Each field As String In handleInfos.GetAvailableProperties
                            Dim scomp As String = cp.GetInformation(field)
                            If scomp IsNot Nothing Then
                                If pObj.caseSen = False Then
                                    scomp = scomp.ToLowerInvariant
                                End If
                                If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                    ' Found an item
                                    Dim newItFound As New searchInfos(cp.Infos.ProcessID, field, searchInfos.ResultType.Handle, scomp, "", cp.Infos.Handle, IntPtr.Zero)
                                    key += 1
                                    _dico.Add(key.ToString, newItFound)
                                End If
                            End If
                        Next
                    Next
                End If


                ' ---- WINDOWS
                If (pObj.includ And searchInfos.SearchInclude.SearchWindows) = searchInfos.SearchInclude.SearchWindows Then
                    Dim _tmpDico As New Dictionary(Of String, cWindow)
                    _tmpDico = Native.Objects.Window.EnumerateAllWindows
                    For Each cp As cWindow In _tmpDico.Values
                        For Each field As String In windowInfos.GetAvailableProperties
                            Dim scomp As String = cp.GetInformation(field)
                            If scomp IsNot Nothing Then
                                If pObj.caseSen = False Then
                                    scomp = scomp.ToLowerInvariant
                                End If
                                If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                    ' Found an item
                                    Dim newItFound As New searchInfos(cp.Infos.ProcessId, field, searchInfos.ResultType.Window, scomp)
                                    key += 1
                                    _dico.Add(key.ToString, newItFound)
                                End If
                            End If
                        Next
                    Next
                End If

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub

End Class