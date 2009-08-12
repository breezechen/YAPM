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

Public Class asyncCallbackProcGetAllNonFixedInfos

    Public Event HasGotAllNonFixedInfos(ByVal Success As Boolean, ByRef newInfos As API.SYSTEM_PROCESS_INFORMATION, ByVal msg As String)

    Private _connection As cProcessConnection
    Private _process As cProcess

    Public Sub New(ByRef procConnection As cProcessConnection, ByRef process As cProcess)
        _connection = procConnection
        _process = process
    End Sub

    ' For now it is not possible to get here UserObj, GDIObj and Affinity !
    ' TOMODIFY !!
    Public Sub Process(ByVal state As Object)

        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket


            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Get infos
                Dim refProcess As Management.ManagementObject = Nothing
                Try
                    ' Enumerate processes and find current process
                    For Each tmpMngObj As Management.ManagementObject In _connection.wmiSearcher.Get
                        If _process.Infos.Pid = CInt(tmpMngObj.GetPropertyValue(Native.Api.Enums.WMI_INFO_PROCESS.ProcessId.ToString)) Then
                            refProcess = tmpMngObj
                            Exit For
                        End If
                    Next

                    ' Get informations from found process
                    If refProcess IsNot Nothing Then

                        Dim _newInfos As New API.SYSTEM_PROCESS_INFORMATION
                        With _newInfos
                            .BasePriority = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.Priority.ToString))
                            .HandleCount = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.HandleCount.ToString))
                            '.InheritedFromProcessId = CInt(refProcess.Item(API.WMI_INFO.ParentProcessId.ToString))
                            Dim _IO As New API.IO_COUNTERS
                            With _IO
                                .OtherOperationCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.OtherOperationCount.ToString))
                                .OtherTransferCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.OtherTransferCount.ToString))
                                .ReadOperationCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ReadOperationCount.ToString))
                                .ReadTransferCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ReadTransferCount.ToString))
                                .WriteOperationCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.WriteOperationCount.ToString))
                                .WriteTransferCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.WriteTransferCount.ToString))
                            End With
                            .IoCounters = _IO
                            .KernelTime = CLng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.KernelModeTime.ToString))
                            .NumberOfThreads = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ThreadCount.ToString))
                            '.ProcessId = CInt(refProcess.Item(API.WMI_INFO.ProcessId.ToString))
                            '.SessionId                 ' NOT IMPLEMENTED
                            .UserTime = CLng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.UserModeTime.ToString))
                            Dim _VM As New API.VM_COUNTERS_EX
                            With _VM
                                .PageFaultCount = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PageFaults.ToString))
                                .PagefileUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PageFileUsage.ToString))
                                .PeakPagefileUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PeakPageFileUsage.ToString))
                                .PeakVirtualSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PeakVirtualSize.ToString))
                                .PeakWorkingSetSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PeakWorkingSetSize.ToString))
                                .PrivateBytes = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PrivatePageCount.ToString))
                                .QuotaNonPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaNonPagedPoolUsage.ToString))
                                .QuotaPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaPagedPoolUsage.ToString))
                                .QuotaPeakNonPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaPeakNonPagedPoolUsage.ToString))
                                .QuotaPeakPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaPeakPagedPoolUsage.ToString))
                                .VirtualSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.VirtualSize.ToString))
                                .WorkingSetSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.WorkingSetSize.ToString))
                            End With
                            .VirtualMemoryCounters = _VM
                        End With

                        RaiseEvent HasGotAllNonFixedInfos(True, _newInfos, Native.Api.Functions.GetError)
                    Else
                        ' Could not get process
                        RaiseEvent HasGotAllNonFixedInfos(False, Nothing, Native.Api.Functions.GetError)
                    End If

                Catch ex As Exception
                    ' Could not enumerate processes
                    RaiseEvent HasGotAllNonFixedInfos(False, Nothing, ex.Message)
                End Try

            Case Else
                ' Local

                ' OK, normally no call for Process method for a local connection

                'Dim _gdi As Integer = API.GetGuiResources(_handle, API.GR_GDIOBJECTS)
                'Dim _user As Integer = API.GetGuiResources(_handle, API.GR_USEROBJECTS)
                'Dim _affinity As Integer = GetAffinity(_pid)

                'RaiseEvent GatheredInfos(New TheseInfos(_gdi, _user, _affinity))
        End Select
    End Sub

End Class
