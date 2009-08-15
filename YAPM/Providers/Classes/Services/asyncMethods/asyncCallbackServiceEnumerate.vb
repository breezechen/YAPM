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
            'If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
        Catch ex As Exception
            '
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
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim res As ManagementObjectCollection = Nothing
                Try
                    res = con.wmiSearcher.Get()
                Catch ex As Exception
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, False, Nothing, ex.Message, pObj.forInstanceId)
                    sem.Release()
                    Exit Sub
                End Try

                Dim _dico As New Dictionary(Of String, serviceInfos)
                For Each refService As Management.ManagementObject In res

                    Dim obj As New Native.Api.NativeStructs.EnumServiceStatusProcess
                    With obj
                        .DisplayName = CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.DisplayName.ToString))
                        .ServiceName = CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.Name.ToString))
                        With .ServiceStatusProcess
                            .CheckPoint = CInt(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.CheckPoint.ToString))
                            If CBool(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.AcceptPause.ToString)) Then
                                .ControlsAccepted = .ControlsAccepted Or Native.Api.NativeEnums.ServiceAccept.PauseContinue
                            End If
                            If CBool(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.AcceptStop.ToString)) Then
                                .ControlsAccepted = .ControlsAccepted Or Native.Api.NativeEnums.ServiceAccept.Stop
                            End If
                            .CurrentState = Native.Functions.Service.GetServiceStateFromString(CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.State.ToString)))
                            .ProcessID = CInt(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.ProcessId.ToString))
                            '.ServiceFlags
                            .ServiceSpecificExitCode = CInt(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.ServiceSpecificExitCode.ToString))
                            .ServiceType = Native.Functions.Service.GetServiceTypeFromString(CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.ServiceType.ToString)))
                            .WaitHint = CInt(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.WaitHint.ToString))
                            .Win32ExitCode = CInt(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.ExitCode.ToString))
                        End With
                    End With


                    ' Do we have to get fixed infos ?
                    Dim _servInfos As New serviceInfos(obj, True)

                    Dim conf As New Native.Api.NativeStructs.QueryServiceConfig
                    With conf
                        .BinaryPathName = CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.PathName.ToString))
                        '.Dependencies
                        .DisplayName = CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.DisplayName.ToString))
                        .ErrorControl = Native.Functions.Service.GetServiceErrorControlFromString(CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.ErrorControl.ToString)))
                        '.LoadOrderGroup 
                        .ServiceStartName = CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.StartName.ToString))
                        .StartType = Native.Functions.Service.GetServiceStartTypeFromString(CStr(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.StartMode.ToString)))
                        .TagID = CInt(refService.GetPropertyValue(Native.Api.Enums.WMI_INFO_SERVICE.TagId.ToString))
                    End With
                    _servInfos.SetConfig(conf)

                    _dico.Add(obj.ServiceName, _servInfos)
                Next

                Try
                    ctrl.Invoke(deg, True, _dico, Nothing, 0)
                Catch ex As Exception
                    '
                End Try

            Case Else
                ' Local

                Dim _dico As New Dictionary(Of String, serviceInfos)

                Native.Objects.Service.EnumerateServices(con.SCManagerLocalHandle, _dico, pObj.all, _
                                                 pObj.complete, pObj.pid)

                Try
                    'If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    '
                End Try

        End Select

        sem.Release()

    End Sub

    
End Class
