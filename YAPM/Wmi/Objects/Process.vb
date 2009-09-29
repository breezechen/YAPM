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

Imports System.Runtime.InteropServices
Imports Native.Api
Imports System.Management
Imports Native.Api.Enums

Namespace Wmi.Objects

    Public Class Process



        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' Enumerate processes
        Public Shared Function EnumerateProcesses( _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, processInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()

                For Each refProcess As Management.ManagementObject In res

                    Dim obj As New Native.Api.NativeStructs.SystemProcessInformation
                    With obj
                        .BasePriority = CInt(refProcess.Item(WMI_INFO_PROCESS.Priority.ToString))
                        .HandleCount = CInt(refProcess.Item(WMI_INFO_PROCESS.HandleCount.ToString))
                        .InheritedFromProcessId = CInt(refProcess.Item(WMI_INFO_PROCESS.ParentProcessId.ToString))
                        Dim _IO As New Native.Api.NativeStructs.IoCounters
                        With _IO
                            .OtherOperationCount = CULng(refProcess.Item(WMI_INFO_PROCESS.OtherOperationCount.ToString))
                            .OtherTransferCount = CULng(refProcess.Item(WMI_INFO_PROCESS.OtherTransferCount.ToString))
                            .ReadOperationCount = CULng(refProcess.Item(WMI_INFO_PROCESS.ReadOperationCount.ToString))
                            .ReadTransferCount = CULng(refProcess.Item(WMI_INFO_PROCESS.ReadTransferCount.ToString))
                            .WriteOperationCount = CULng(refProcess.Item(WMI_INFO_PROCESS.WriteOperationCount.ToString))
                            .WriteTransferCount = CULng(refProcess.Item(WMI_INFO_PROCESS.WriteTransferCount.ToString))
                        End With
                        .IoCounters = _IO
                        .KernelTime = CLng(refProcess.Item(WMI_INFO_PROCESS.KernelModeTime.ToString))
                        .NumberOfThreads = CInt(refProcess.Item(WMI_INFO_PROCESS.ThreadCount.ToString))
                        .ProcessId = CInt(refProcess.Item(WMI_INFO_PROCESS.ProcessId.ToString))
                        '.SessionId                 ' NOT IMPLEMENTED
                        .UserTime = CLng(refProcess.Item(WMI_INFO_PROCESS.UserModeTime.ToString))
                        Dim _VM As New Native.Api.NativeStructs.VmCountersEx
                        With _VM
                            .PageFaultCount = CInt(refProcess.Item(WMI_INFO_PROCESS.PageFaults.ToString))
                            .PagefileUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PageFileUsage.ToString)))
                            .PeakPagefileUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PeakPageFileUsage.ToString)))
                            .PeakVirtualSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PeakVirtualSize.ToString)))
                            .PeakWorkingSetSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PeakWorkingSetSize.ToString)))
                            .PrivateBytes = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PrivatePageCount.ToString)))
                            .QuotaNonPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaNonPagedPoolUsage.ToString)))
                            .QuotaPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaPagedPoolUsage.ToString)))
                            .QuotaPeakNonPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaPeakNonPagedPoolUsage.ToString)))
                            .QuotaPeakPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaPeakPagedPoolUsage.ToString)))
                            .VirtualSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.VirtualSize.ToString)))
                            .WorkingSetSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.WorkingSetSize.ToString)))
                        End With
                        .VirtualMemoryCounters = _VM
                    End With


                    ' Do we have to get fixed infos ?
                    Dim _procInfos As New processInfos(obj, CStr(refProcess.Item("Name")))
                    If Native.Objects.Process.NewProcesses.ContainsKey(obj.ProcessId) = False Then
                        With _procInfos
                            .Path = CStr(refProcess.Item(WMI_INFO_PROCESS.ExecutablePath.ToString))

                            Dim s1(1) As String
                            Try
                                Call refProcess.InvokeMethod("GetOwner", s1)
                                If Len(s1(0)) + Len(s1(1)) > 0 Then
                                    .UserName = s1(1) & "\" & s1(0)
                                Else
                                    .UserName = NO_INFO_RETRIEVED
                                End If
                            Catch ex As Exception
                                .UserName = NO_INFO_RETRIEVED
                            End Try

                            .CommandLine = NO_INFO_RETRIEVED
                            .FileInfo = Nothing
                            .PebAddress = IntPtr.Zero
                        End With

                        Native.Objects.Process.NewProcesses.Add(obj.ProcessId, False)

                        Trace.WriteLine("Got fixed infos for id = " & obj.ProcessId.ToString)
                    End If

                    ' Set true so that the process is marked as existing
                    Native.Objects.Process.NewProcesses(obj.ProcessId) = True
                    Dim sKey As String = obj.ProcessId.ToString
                    If _dico.ContainsKey(sKey) = False Then
                        _dico.Add(sKey, _procInfos)
                    End If
                Next

                ' Remove all processes that not exist anymore
                Dim _dicoTemp As Dictionary(Of Integer, Boolean) = Native.Objects.Process.NewProcesses
                For Each it As System.Collections.Generic.KeyValuePair(Of Integer, Boolean) In _dicoTemp
                    If it.Value = False Then
                        Native.Objects.Process.NewProcesses.Remove(it.Key)
                    End If
                Next

                Return True

            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

        End Function

        ' Update process informations
        Public Shared Function RefreshProcessInformationsById(ByVal pid As Integer, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef msgError As String, _
                        ByRef _newInfos As Native.Api.NativeStructs.SystemProcessInformation) As Boolean

            ' Get infos
            Dim refProcess As Management.ManagementObject = Nothing

            Try
                ' Enumerate processes and find current process
                For Each tmpMngObj As Management.ManagementObject In objSearcher.Get
                    If pid = CInt(tmpMngObj.GetPropertyValue(WMI_INFO_PROCESS.ProcessId.ToString)) Then
                        refProcess = tmpMngObj
                        Exit For
                    End If
                Next
            Catch ex As Exception
                msgError = ex.Message
                Return False
            End Try


            ' Get informations from found process
            If refProcess IsNot Nothing Then

                With _newInfos
                    .BasePriority = CInt(refProcess.Item(WMI_INFO_PROCESS.Priority.ToString))
                    .HandleCount = CInt(refProcess.Item(WMI_INFO_PROCESS.HandleCount.ToString))
                    '.InheritedFromProcessId = CInt(refProcess.Item(API.WMI_INFO.ParentProcessId.ToString))
                    Dim _IO As New Native.Api.NativeStructs.IoCounters
                    With _IO
                        .OtherOperationCount = CULng(refProcess.Item(WMI_INFO_PROCESS.OtherOperationCount.ToString))
                        .OtherTransferCount = CULng(refProcess.Item(WMI_INFO_PROCESS.OtherTransferCount.ToString))
                        .ReadOperationCount = CULng(refProcess.Item(WMI_INFO_PROCESS.ReadOperationCount.ToString))
                        .ReadTransferCount = CULng(refProcess.Item(WMI_INFO_PROCESS.ReadTransferCount.ToString))
                        .WriteOperationCount = CULng(refProcess.Item(WMI_INFO_PROCESS.WriteOperationCount.ToString))
                        .WriteTransferCount = CULng(refProcess.Item(WMI_INFO_PROCESS.WriteTransferCount.ToString))
                    End With
                    .IoCounters = _IO
                    .KernelTime = CLng(refProcess.Item(WMI_INFO_PROCESS.KernelModeTime.ToString))
                    .NumberOfThreads = CInt(refProcess.Item(WMI_INFO_PROCESS.ThreadCount.ToString))
                    '.ProcessId = CInt(refProcess.Item(API.WMI_INFO.ProcessId.ToString))
                    '.SessionId                 ' NOT IMPLEMENTED
                    .UserTime = CLng(refProcess.Item(WMI_INFO_PROCESS.UserModeTime.ToString))
                    Dim _VM As New Native.Api.NativeStructs.VmCountersEx
                    With _VM
                        .PageFaultCount = CInt(refProcess.Item(WMI_INFO_PROCESS.PageFaults.ToString))
                        .PagefileUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PageFileUsage.ToString)))
                        .PeakPagefileUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PeakPageFileUsage.ToString)))
                        .PeakVirtualSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PeakVirtualSize.ToString)))
                        .PeakWorkingSetSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PeakWorkingSetSize.ToString)))
                        .PrivateBytes = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.PrivatePageCount.ToString)))
                        .QuotaNonPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaNonPagedPoolUsage.ToString)))
                        .QuotaPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaPagedPoolUsage.ToString)))
                        .QuotaPeakNonPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaPeakNonPagedPoolUsage.ToString)))
                        .QuotaPeakPagedPoolUsage = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.QuotaPeakPagedPoolUsage.ToString)))
                        .VirtualSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.VirtualSize.ToString)))
                        .WorkingSetSize = New IntPtr(CInt(refProcess.Item(WMI_INFO_PROCESS.WorkingSetSize.ToString)))
                    End With
                    .VirtualMemoryCounters = _VM
                End With

                Return True
            Else
                msgError = "Internal error"
                Return False
            End If

        End Function

        ' Kill a process
        Public Shared Function KillProcessById(ByVal pid As Integer, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef msgError As String) As Boolean
            Try
                Dim _theProcess As Management.ManagementObject = Nothing
                For Each pp As Management.ManagementObject In objSearcher.Get
                    If CInt(pp("ProcessId")) = pid Then
                        _theProcess = pp
                        Exit For
                    End If
                Next
                If _theProcess IsNot Nothing Then
                    Dim ret As WmiProcessReturnCode
                    ret = CType(_theProcess.InvokeMethod("Terminate", Nothing), WmiProcessReturnCode)
                    msgError = ret.ToString
                    Return (ret = WmiProcessReturnCode.SuccessfulCompletion)
                Else
                    msgError = "Internal error"
                    Return False
                End If
            Catch ex As Exception
                msgError = ex.Message
                Return False
            End Try

        End Function

        ' Create new process
        Public Shared Function CreateNewProcessByPath(ByVal path As String, _
                ByVal objSearcher As Management.ManagementObjectSearcher, _
                ByRef msgError As String) As Boolean
            Try
                Dim objectGetOptions As New ObjectGetOptions()
                Dim managementPath As New ManagementPath("Win32_Process")
                Dim processClass As New ManagementClass(objSearcher.Scope, managementPath, _
                                                        objectGetOptions)
                Dim inParams As ManagementBaseObject = processClass.GetMethodParameters("Create")
                inParams("CommandLine") = path
                Dim outParams As ManagementBaseObject = processClass.InvokeMethod("Create", inParams, Nothing)
                Dim res As WmiProcessReturnCode = CType(outParams("ProcessId"), WmiProcessReturnCode)

                msgError = res.ToString
                Return (res = WmiProcessReturnCode.SuccessfulCompletion)

            Catch ex As Exception
                msgError = ex.Message
                Return False
            End Try
        End Function

        ' Set process priority
        Public Shared Function SetProcessPriorityById(ByVal pid As Integer, _
                                ByVal lvl As ProcessPriorityClass, _
                                ByVal objSearcher As Management.ManagementObjectSearcher, _
                                ByRef msgError As String) As Boolean

            Try
                Dim res As WmiProcessReturnCode
                For Each srv As ManagementObject In objSearcher.Get
                    If CInt(srv.GetPropertyValue(WMI_INFO_PROCESS.ProcessId.ToString)) = pid Then
                        Dim inParams As ManagementBaseObject = srv.GetMethodParameters("SetPriority")
                        inParams("Priority") = lvl
                        Dim outParams As ManagementBaseObject = srv.InvokeMethod("SetPriority", inParams, Nothing)
                        res = CType(outParams("ReturnValue"), WmiProcessReturnCode)
                        Exit For
                    End If
                Next

                msgError = res.ToString
                Return (res = WmiProcessReturnCode.SuccessfulCompletion)
            Catch ex As Exception
                msgError = ex.Message
                Return False
            End Try

        End Function


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
