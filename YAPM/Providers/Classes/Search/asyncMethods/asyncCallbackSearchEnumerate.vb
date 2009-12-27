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
Imports Native.Api.Enums

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
        Public includ As GeneralObjectType
        Public forInstanceId As Integer
        Public Sub New(ByRef strToSearch As String, ByVal [case] As Boolean, ByVal include As GeneralObjectType, ByVal ii As Integer)
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
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot

                Dim _dico As New Dictionary(Of String, searchInfos)
                Dim key As Integer = 0

                Dim sToSearch As String = pObj.strSearch
                If pObj.caseSen = False Then
                    sToSearch = sToSearch.ToLowerInvariant
                End If

                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then

                    ' ---- PROCESSES
                    Dim _procs As New Dictionary(Of Integer, processInfos)
                    _procs = snap.Processes
                    If snap.Processes IsNot Nothing Then
                        If _procs IsNot Nothing Then
                            For Each proc As processInfos In _procs.Values
                                Dim cp As New cProcess(proc)
                                For Each field As String In processInfos.GetAvailableProperties(includeFirstProp:=True)
                                    Dim scomp As String = cp.GetInformation(field)
                                    If scomp IsNot Nothing Then
                                        If pObj.caseSen = False Then
                                            scomp = scomp.ToLowerInvariant
                                        End If
                                        If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                            ' Found an item
                                            Dim newItFound As New searchInfos(cp, field, scomp)
                                            key += 1
                                            _dico.Add(key.ToString, newItFound)
                                        End If
                                    End If
                                Next

                                ' ---- MODULES
                                If (pObj.includ And GeneralObjectType.Module) = GeneralObjectType.Module Then
                                    Dim _tmpDico2 As New Dictionary(Of String, moduleInfos)
                                    _tmpDico2 = snap.ModulesByProcessId(cp.Infos.ProcessId)
                                    If _tmpDico2 IsNot Nothing Then
                                        For Each cm As moduleInfos In _tmpDico2.Values
                                            Dim cmm As New cModule(cm)
                                            For Each field2 As String In processInfos.GetAvailableProperties(includeFirstProp:=True)
                                                Dim scomp2 As String = cmm.GetInformation(field2)
                                                If scomp2 IsNot Nothing Then
                                                    If pObj.caseSen = False Then
                                                        scomp2 = scomp2.ToLowerInvariant
                                                    End If
                                                    If InStr(scomp2, sToSearch, CompareMethod.Binary) > 0 Then
                                                        ' Found an item
                                                        Dim newItFound As New searchInfos(cmm, field2, scomp2)
                                                        key += 1
                                                        _dico.Add(key.ToString, newItFound)
                                                    End If
                                                End If
                                            Next
                                        Next
                                    End If
                                End If

                                ' ---- ENVVARIABLES
                                If (pObj.includ And GeneralObjectType.EnvironmentVariable) = GeneralObjectType.EnvironmentVariable Then
                                    Dim _tmpDico2 As New Dictionary(Of String, envVariableInfos)
                                    _tmpDico2 = snap.EnvironnementVariablesByProcessId(proc.ProcessId)
                                    If _tmpDico2 IsNot Nothing Then
                                        For Each cm As envVariableInfos In _tmpDico2.Values
                                            Dim cmm As New cEnvVariable(cm)
                                            For Each field2 As String In envVariableInfos.GetAvailableProperties(includeFirstProp:=True)
                                                Dim scomp2 As String = cmm.GetInformation(field2)
                                                If scomp2 IsNot Nothing Then
                                                    If pObj.caseSen = False Then
                                                        scomp2 = scomp2.ToLowerInvariant
                                                    End If
                                                    If InStr(scomp2, sToSearch, CompareMethod.Binary) > 0 Then
                                                        ' Found an item
                                                        Dim newItFound As New searchInfos(cmm, field2, scomp2)
                                                        key += 1
                                                        ' _dico.Add(key.ToString, newItFound)
                                                    End If
                                                End If
                                            Next
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    ' ---- SERVICES
                    If (pObj.includ And GeneralObjectType.Service) = GeneralObjectType.Service Then
                        Dim _tmpDico As New Dictionary(Of String, serviceInfos)
                        _tmpDico = snap.Services
                        If _tmpDico IsNot Nothing Then
                            For Each cp As serviceInfos In _tmpDico.Values
                                Dim cpp As New cService(cp)
                                For Each field As String In serviceInfos.GetAvailableProperties(includeFirstProp:=True)
                                    Dim scomp As String = cpp.GetInformation(field)
                                    If scomp IsNot Nothing Then
                                        If pObj.caseSen = False Then
                                            scomp = scomp.ToLowerInvariant
                                        End If
                                        If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                            ' Found an item
                                            Dim newItFound As New searchInfos(cpp, field, scomp)
                                            key += 1
                                            _dico.Add(key.ToString, newItFound)
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    End If


                    ' ---- HANDLES
                    If (pObj.includ And GeneralObjectType.Handle) = GeneralObjectType.Handle Then
                        If _procs IsNot Nothing Then
                            For Each theproc As processInfos In _procs.Values
                                Dim _tmpDico As New Dictionary(Of String, handleInfos)
                                _tmpDico = snap.HandlesByProcessId(theproc.ProcessId)
                                For Each cp As handleInfos In _tmpDico.Values
                                    Dim cpp As New cHandle(cp)
                                    For Each field As String In handleInfos.GetAvailableProperties(includeFirstProp:=True)
                                        Dim scomp As String = cpp.GetInformation(field)
                                        If scomp IsNot Nothing Then
                                            If pObj.caseSen = False Then
                                                scomp = scomp.ToLowerInvariant
                                            End If
                                            If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                                ' Found an item
                                                Dim newItFound As New searchInfos(cpp, field, scomp)
                                                key += 1
                                                ' _dico.Add(key.ToString, newItFound)
                                            End If
                                        End If
                                    Next
                                Next
                            Next
                        End If
                    End If


                    ' ---- WINDOWS
                    If (pObj.includ And GeneralObjectType.Window) = GeneralObjectType.Window Then
                        If _procs IsNot Nothing Then
                            For Each theproc As processInfos In _procs.Values
                                Dim _tmpDico As New Dictionary(Of String, windowInfos)
                                _tmpDico = snap.WindowsByProcessId(theproc.ProcessId)
                                For Each cp As windowInfos In _tmpDico.Values
                                    Dim cpp As New cWindow(cp)
                                    For Each field As String In windowInfos.GetAvailableProperties(includeFirstProp:=True)
                                        Dim scomp As String = cpp.GetInformation(field)
                                        If scomp IsNot Nothing Then
                                            If pObj.caseSen = False Then
                                                scomp = scomp.ToLowerInvariant
                                            End If
                                            If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                                ' Found an item
                                                Dim newItFound As New searchInfos(cpp, field, scomp)
                                                key += 1
                                                _dico.Add(key.ToString, newItFound)
                                            End If
                                        End If
                                    Next
                                Next
                            Next
                        End If
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

                Dim _dico As New Dictionary(Of String, searchInfos)
                Dim key As Integer = 0

                Dim sToSearch As String = pObj.strSearch
                If pObj.caseSen = False Then
                    sToSearch = sToSearch.ToLowerInvariant
                End If

                ' ---- PROCESSES
                If (pObj.includ And GeneralObjectType.Process) = GeneralObjectType.Process Then
                    If Native.Objects.Process.CurrentProcesses IsNot Nothing Then
                        Dim _tmpDico As New Dictionary(Of Integer, processInfos)
                        _tmpDico = Native.Objects.Process.CurrentProcesses
                        For Each _cp As processInfos In _tmpDico.Values
                            Dim cp As New cProcess(_cp)
                            For Each field As String In processInfos.GetAvailableProperties(includeFirstProp:=True)
                                Dim scomp As String = cp.GetInformation(field)
                                If scomp IsNot Nothing Then
                                    If pObj.caseSen = False Then
                                        scomp = scomp.ToLowerInvariant
                                    End If
                                    If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                        ' Found an item
                                        Dim newItFound As New searchInfos(cp, field, scomp)
                                        key += 1
                                        _dico.Add(key.ToString, newItFound)
                                    End If
                                End If
                            Next

                            ' ---- MODULES
                            If (pObj.includ And GeneralObjectType.Module) = GeneralObjectType.Module Then
                                Dim _tmpDico2 As Dictionary(Of String, cModule) = cModule.CurrentLocalModules(cp.Infos.ProcessId)
                                For Each cm As cModule In _tmpDico2.Values
                                    For Each field2 As String In processInfos.GetAvailableProperties(includeFirstProp:=True)
                                        Dim scomp2 As String = cm.GetInformation(field2)
                                        If scomp2 IsNot Nothing Then
                                            If pObj.caseSen = False Then
                                                scomp2 = scomp2.ToLowerInvariant
                                            End If
                                            If InStr(scomp2, sToSearch, CompareMethod.Binary) > 0 Then
                                                ' Found an item
                                                Dim newItFound As New searchInfos(cm, field2, scomp2)
                                                key += 1
                                                _dico.Add(key.ToString, newItFound)
                                            End If
                                        End If
                                    Next
                                Next
                            End If

                            ' ---- ENVVARIABLES
                            If (pObj.includ And GeneralObjectType.EnvironmentVariable) = GeneralObjectType.EnvironmentVariable Then
                                Dim _tmpDico2 As Dictionary(Of String, cEnvVariable) = cEnvVariable.CurrentEnvVariables(cp)
                                For Each cm As cEnvVariable In _tmpDico2.Values
                                    For Each field2 As String In envVariableInfos.GetAvailableProperties(includeFirstProp:=True)
                                        Dim scomp2 As String = cm.GetInformation(field2)
                                        If scomp2 IsNot Nothing Then
                                            If pObj.caseSen = False Then
                                                scomp2 = scomp2.ToLowerInvariant
                                            End If
                                            If InStr(scomp2, sToSearch, CompareMethod.Binary) > 0 Then
                                                ' Found an item
                                                Dim newItFound As New searchInfos(cm, field2, scomp2)
                                                key += 1
                                                _dico.Add(key.ToString, newItFound)
                                            End If
                                        End If
                                    Next
                                Next
                            End If

                        Next
                    End If
                End If

                ' ---- SERVICES
                If (pObj.includ And GeneralObjectType.Service) = GeneralObjectType.Service Then
                    If Native.Objects.Service.CurrentServices IsNot Nothing Then
                        Dim _tmpDico As New Dictionary(Of String, serviceInfos)
                        _tmpDico = Native.Objects.Service.CurrentServices
                        For Each _cp As serviceInfos In _tmpDico.Values
                            Dim cp As New cService(_cp)
                            For Each field As String In serviceInfos.GetAvailableProperties(includeFirstProp:=True)
                                Dim scomp As String = cp.GetInformation(field)
                                If scomp IsNot Nothing Then
                                    If pObj.caseSen = False Then
                                        scomp = scomp.ToLowerInvariant
                                    End If
                                    If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                        ' Found an item
                                        Dim newItFound As New searchInfos(cp, field, scomp)
                                        key += 1
                                        _dico.Add(key.ToString, newItFound)
                                    End If
                                End If
                            Next
                        Next
                    End If
                End If


                ' ---- HANDLES
                If (pObj.includ And GeneralObjectType.Handle) = GeneralObjectType.Handle Then
                    Dim _tmpDico As New Dictionary(Of String, cHandle)
                    _tmpDico = Native.Objects.Handle.EnumerateCurrentLocalHandles
                    For Each cp As cHandle In _tmpDico.Values
                        For Each field As String In handleInfos.GetAvailableProperties(includeFirstProp:=True)
                            Dim scomp As String = cp.GetInformation(field)
                            If scomp IsNot Nothing Then
                                If pObj.caseSen = False Then
                                    scomp = scomp.ToLowerInvariant
                                End If
                                If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                    ' Found an item
                                    Dim newItFound As New searchInfos(cp, field, scomp)
                                    key += 1
                                    _dico.Add(key.ToString, newItFound)
                                End If
                            End If
                        Next
                    Next
                End If


                ' ---- WINDOWS
                If (pObj.includ And GeneralObjectType.Window) = GeneralObjectType.Window Then
                    Dim _tmpDico As New Dictionary(Of String, cWindow)
                    _tmpDico = Native.Objects.Window.EnumerateAllWindows
                    For Each cp As cWindow In _tmpDico.Values
                        For Each field As String In windowInfos.GetAvailableProperties(includeFirstProp:=True)
                            Dim scomp As String = cp.GetInformation(field)
                            If scomp IsNot Nothing Then
                                If pObj.caseSen = False Then
                                    scomp = scomp.ToLowerInvariant
                                End If
                                If InStr(scomp, sToSearch, CompareMethod.Binary) > 0 Then
                                    ' Found an item
                                    Dim newItFound As New searchInfos(cp, field, scomp)
                                    key += 1
                                    _dico.Add(key.ToString, newItFound)
                                End If
                            End If
                        Next
                    Next
                End If

                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

        End Select

        sem.Release()

    End Sub

End Class
