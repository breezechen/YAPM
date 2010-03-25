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
                        ByRef _dico As Dictionary(Of Integer, processInfos), _
                        ByRef errMsg As String) As Boolean

            Try
                ProcessProvider._semProcess.WaitOne()
                Dim res As ManagementObjectCollection = Nothing
                res = objSearcher.Get()

                For Each refProcess As Management.ManagementObject In res

                    ' We use a SystemProcessInformation64 structure,
                    ' as we don't know if remote machine is 64 or 32-bit.
                    ' If it's 64-bits, we potentially won't be able to create
                    ' IntPtrs in our local machine (if 32-bit) as the associated
                    ' value might be > Integer.MaxValue (which is the maximum
                    ' value supported by IntPtr constructor on 32-bit systems).
                    Dim obj As New Native.Api.Structs.SystemProcessInformation64
                    With obj
                        .BasePriority = CInt(refProcess.Item(WmiInfoProcess.Priority.ToString))
                        .HandleCount = CInt(refProcess.Item(WmiInfoProcess.HandleCount.ToString))
                        .InheritedFromProcessId = CInt(refProcess.Item(WmiInfoProcess.ParentProcessId.ToString))
                        Dim _IO As New Native.Api.NativeStructs.IoCounters
                        With _IO
                            .OtherOperationCount = CULng(refProcess.Item(WmiInfoProcess.OtherOperationCount.ToString))
                            .OtherTransferCount = CULng(refProcess.Item(WmiInfoProcess.OtherTransferCount.ToString))
                            .ReadOperationCount = CULng(refProcess.Item(WmiInfoProcess.ReadOperationCount.ToString))
                            .ReadTransferCount = CULng(refProcess.Item(WmiInfoProcess.ReadTransferCount.ToString))
                            .WriteOperationCount = CULng(refProcess.Item(WmiInfoProcess.WriteOperationCount.ToString))
                            .WriteTransferCount = CULng(refProcess.Item(WmiInfoProcess.WriteTransferCount.ToString))
                        End With
                        .IoCounters = _IO
                        .KernelTime = CLng(refProcess.Item(WmiInfoProcess.KernelModeTime.ToString))
                        .NumberOfThreads = CInt(refProcess.Item(WmiInfoProcess.ThreadCount.ToString))
                        .ProcessId = CInt(refProcess.Item(WmiInfoProcess.ProcessId.ToString))
                        '.SessionId                 ' NOT IMPLEMENTED
                        .UserTime = CLng(refProcess.Item(WmiInfoProcess.UserModeTime.ToString))
                        Dim _VM As New Native.Api.Structs.VmCountersEx64
                        With _VM
                            .PageFaultCount = CInt(refProcess.Item(WmiInfoProcess.PageFaults.ToString))
                            .PagefileUsage = CLng(refProcess.Item(WmiInfoProcess.PageFileUsage.ToString))
                            .PeakPagefileUsage = CLng(refProcess.Item(WmiInfoProcess.PeakPageFileUsage.ToString))
                            .PeakVirtualSize = CLng(refProcess.Item(WmiInfoProcess.PeakVirtualSize.ToString))
                            .PeakWorkingSetSize = CLng(refProcess.Item(WmiInfoProcess.PeakWorkingSetSize.ToString))
                            .PrivateBytes = CLng(refProcess.Item(WmiInfoProcess.PrivatePageCount.ToString))
                            .QuotaNonPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaNonPagedPoolUsage.ToString))
                            .QuotaPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaPagedPoolUsage.ToString))
                            .QuotaPeakNonPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaPeakNonPagedPoolUsage.ToString))
                            .QuotaPeakPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaPeakPagedPoolUsage.ToString))
                            .VirtualSize = CLng(refProcess.Item(WmiInfoProcess.VirtualSize.ToString))
                            .WorkingSetSize = CLng(refProcess.Item(WmiInfoProcess.WorkingSetSize.ToString))
                        End With
                        .VirtualMemoryCounters = _VM
                    End With


                    ' Do we have to get fixed infos ?
                    Dim _procInfos As New processInfos(obj, CStr(refProcess.Item("Name")))
                    '_____ ==> We retrieve always all informations (easiest)
                    '_____If Native.Objects.Process.NewProcesses.Contains(obj.ProcessId) = False Then
                    With _procInfos
                        .Path = CStr(refProcess.Item(WmiInfoProcess.ExecutablePath.ToString))

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

                        .CommandLine = CStr(refProcess.Item(WmiInfoProcess.CommandLine.ToString))
                        .FileInfo = Nothing
                        .PebAddress = IntPtr.Zero
                    End With

                    '_____Native.Objects.Process.NewProcesses.Add(obj.ProcessId)

                    ''Trace.WriteLine("Got fixed infos for id = " & obj.ProcessId.ToString)
                    '_____End If

                    If _dico.ContainsKey(obj.ProcessId) = False Then
                        _dico.Add(obj.ProcessId, _procInfos)
                    End If
                Next

                Return True

            Catch ex As Exception
                errMsg = ex.Message
                Return False
            Finally
                ProcessProvider._semProcess.Release()
            End Try

        End Function

        ' Update process informations
        Public Shared Function RefreshProcessInformationsById(ByVal pid As Integer, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef msgError As String, _
                        ByRef _newInfos As Native.Api.Structs.SystemProcessInformation64) As Boolean

            ' Get infos
            Dim refProcess As Management.ManagementObject = Nothing

            Try
                ' Enumerate processes and find current process
                For Each tmpMngObj As Management.ManagementObject In objSearcher.Get
                    If pid = CInt(tmpMngObj.GetPropertyValue(WmiInfoProcess.ProcessId.ToString)) Then
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
                    .BasePriority = CInt(refProcess.Item(WmiInfoProcess.Priority.ToString))
                    .HandleCount = CInt(refProcess.Item(WmiInfoProcess.HandleCount.ToString))
                    '.InheritedFromProcessId = CInt(refProcess.Item(API.WMI_INFO.ParentProcessId.ToString))
                    Dim _IO As New Native.Api.NativeStructs.IoCounters
                    With _IO
                        .OtherOperationCount = CULng(refProcess.Item(WmiInfoProcess.OtherOperationCount.ToString))
                        .OtherTransferCount = CULng(refProcess.Item(WmiInfoProcess.OtherTransferCount.ToString))
                        .ReadOperationCount = CULng(refProcess.Item(WmiInfoProcess.ReadOperationCount.ToString))
                        .ReadTransferCount = CULng(refProcess.Item(WmiInfoProcess.ReadTransferCount.ToString))
                        .WriteOperationCount = CULng(refProcess.Item(WmiInfoProcess.WriteOperationCount.ToString))
                        .WriteTransferCount = CULng(refProcess.Item(WmiInfoProcess.WriteTransferCount.ToString))
                    End With
                    .IoCounters = _IO
                    .KernelTime = CLng(refProcess.Item(WmiInfoProcess.KernelModeTime.ToString))
                    .NumberOfThreads = CInt(refProcess.Item(WmiInfoProcess.ThreadCount.ToString))
                    '.ProcessId = CInt(refProcess.Item(API.WMI_INFO.ProcessId.ToString))
                    '.SessionId                 ' NOT IMPLEMENTED
                    .UserTime = CLng(refProcess.Item(WmiInfoProcess.UserModeTime.ToString))
                    Dim _VM As New Native.Api.Structs.VmCountersEx64
                    With _VM
                        .PageFaultCount = CInt(refProcess.Item(WmiInfoProcess.PageFaults.ToString))
                        .PagefileUsage = CLng(refProcess.Item(WmiInfoProcess.PageFileUsage.ToString))
                        .PeakPagefileUsage = CLng(refProcess.Item(WmiInfoProcess.PeakPageFileUsage.ToString))
                        .PeakVirtualSize = CLng(refProcess.Item(WmiInfoProcess.PeakVirtualSize.ToString))
                        .PeakWorkingSetSize = CLng(refProcess.Item(WmiInfoProcess.PeakWorkingSetSize.ToString))
                        .PrivateBytes = CLng(refProcess.Item(WmiInfoProcess.PrivatePageCount.ToString))
                        .QuotaNonPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaNonPagedPoolUsage.ToString))
                        .QuotaPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaPagedPoolUsage.ToString))
                        .QuotaPeakNonPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaPeakNonPagedPoolUsage.ToString))
                        .QuotaPeakPagedPoolUsage = CLng(refProcess.Item(WmiInfoProcess.QuotaPeakPagedPoolUsage.ToString))
                        .VirtualSize = CLng(refProcess.Item(WmiInfoProcess.VirtualSize.ToString))
                        .WorkingSetSize = CLng(refProcess.Item(WmiInfoProcess.WorkingSetSize.ToString))
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
                Dim res As WmiProcessReturnCode = CType(outParams("ReturnValue"), WmiProcessReturnCode)
                Dim pid As Integer = CType(outParams("ProcessId"), WmiProcessReturnCode)

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
                    If CInt(srv.GetPropertyValue(WmiInfoProcess.ProcessId.ToString)) = pid Then
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
