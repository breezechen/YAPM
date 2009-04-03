Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms

Public Class asyncCallbackGetAllNonFixedInfos

    Public Event HasGotAllNonFixedInfos(ByVal Success As Boolean, ByRef newInfos As API.SYSTEM_PROCESS_INFORMATION)

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
                        If _process.Infos.Pid = CInt(tmpMngObj.GetPropertyValue(API.WMI_INFO.ProcessId.ToString)) Then
                            refProcess = tmpMngObj
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                    ' Could not enumerate processes
                    RaiseEvent HasGotAllNonFixedInfos(False, Nothing)
                End Try
                ' Get informations from found process
                If refProcess IsNot Nothing Then

                    Dim _newInfos As New API.SYSTEM_PROCESS_INFORMATION
                    With _newInfos
                        .BasePriority = CInt(refProcess.Item(API.WMI_INFO.Priority.ToString))
                        .HandleCount = CInt(refProcess.Item(API.WMI_INFO.HandleCount.ToString))
                        '.InheritedFromProcessId = CInt(refProcess.Item(API.WMI_INFO.ParentProcessId.ToString))
                        Dim _IO As New API.IO_COUNTERS
                        With _IO
                            .OtherOperationCount = CULng(refProcess.Item(API.WMI_INFO.OtherOperationCount.ToString))
                            .OtherTransferCount = CULng(refProcess.Item(API.WMI_INFO.OtherTransferCount.ToString))
                            .ReadOperationCount = CULng(refProcess.Item(API.WMI_INFO.ReadOperationCount.ToString))
                            .ReadTransferCount = CULng(refProcess.Item(API.WMI_INFO.ReadTransferCount.ToString))
                            .WriteOperationCount = CULng(refProcess.Item(API.WMI_INFO.WriteOperationCount.ToString))
                            .WriteTransferCount = CULng(refProcess.Item(API.WMI_INFO.WriteTransferCount.ToString))
                        End With
                        .IoCounters = _IO
                        .KernelTime = CLng(refProcess.Item(API.WMI_INFO.KernelModeTime.ToString))
                        .NumberOfThreads = CInt(refProcess.Item(API.WMI_INFO.ThreadCount.ToString))
                        '.ProcessId = CInt(refProcess.Item(API.WMI_INFO.ProcessId.ToString))
                        '.SessionId                 ' NOT IMPLEMENTED
                        .UserTime = CLng(refProcess.Item(API.WMI_INFO.UserModeTime.ToString))
                        Dim _VM As New API.VM_COUNTERS_EX
                        With _VM
                            .PageFaultCount = CInt(refProcess.Item(API.WMI_INFO.PageFaults.ToString))
                            .PagefileUsage = CInt(refProcess.Item(API.WMI_INFO.PageFileUsage.ToString))
                            .PeakPagefileUsage = CInt(refProcess.Item(API.WMI_INFO.PeakPageFileUsage.ToString))
                            .PeakVirtualSize = CInt(refProcess.Item(API.WMI_INFO.PeakVirtualSize.ToString))
                            .PeakWorkingSetSize = CInt(refProcess.Item(API.WMI_INFO.PeakWorkingSetSize.ToString))
                            .PrivateBytes = CInt(refProcess.Item(API.WMI_INFO.PrivatePageCount.ToString))
                            .QuotaNonPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaNonPagedPoolUsage.ToString))
                            .QuotaPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaPagedPoolUsage.ToString))
                            .QuotaPeakNonPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaPeakNonPagedPoolUsage.ToString))
                            .QuotaPeakPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaPeakPagedPoolUsage.ToString))
                            .VirtualSize = CInt(refProcess.Item(API.WMI_INFO.VirtualSize.ToString))
                            .WorkingSetSize = CInt(refProcess.Item(API.WMI_INFO.WorkingSetSize.ToString))
                        End With
                        .VirtualMemoryCounters = _VM
                    End With

                    RaiseEvent HasGotAllNonFixedInfos(True, _newInfos)
                Else
                    ' Could not get process
                    RaiseEvent HasGotAllNonFixedInfos(False, Nothing)
                End If


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
