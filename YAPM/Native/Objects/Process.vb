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
Imports YAPM.Native.Api

Namespace Native.Objects

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

        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Set affinity to a process
        Public Shared Function SetProcessAffinityByHandle(ByVal hProc As IntPtr, _
                                           ByVal affinity As Integer) As Boolean
            If hProc <> IntPtr.Zero Then
                Return NativeFunctions.SetProcessAffinityMask(hProc, New IntPtr(affinity))
            Else
                Return False
            End If
        End Function
        Public Shared Function SetProcessAffinityById(ByVal process As Integer, _
                                           ByVal affinity As Integer) As Boolean

            ' Open handle, set affinity and close handle
            Dim hProc As IntPtr = _
                    NativeFunctions.OpenProcess(Security.ProcessAccess.SetInformation, False, process)
            Dim ret As Boolean = SetProcessAffinityByHandle(hProc, affinity)
            NativeFunctions.CloseHandle(hProc)

            Return ret
        End Function

        ' Set priority
        Public Shared Function SetProcessPriorityById(ByVal pid As Integer, _
                    ByVal priority As System.Diagnostics.ProcessPriorityClass) As Boolean

            Dim hProc As IntPtr
            Dim r As Boolean
            hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SetInformation, _
                                    False, pid)

            If hProc <> IntPtr.Zero Then
                r = Native.Api.NativeFunctions.SetPriorityClass(hProc, priority)
                Native.Api.NativeFunctions.CloseHandle(hProc)
                Return r
            Else
                Return False
            End If

        End Function

        ' Resume a process
        Public Shared Function ResumeProcessById(ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr
            Dim r As UInteger
            hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SuspendResume, _
                                    False, pid)
            If hProc <> IntPtr.Zero Then
                r = Native.Api.NativeFunctions.NtResumeProcess(hProc)
                Native.Api.NativeFunctions.CloseHandle(hProc)
                Return (r <> 0)
            Else
                Return False
            End If
        End Function

        ' Suspend a process
        Public Shared Function SuspendProcessById(ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr
            Dim r As UInteger
            hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SuspendResume, _
                                    False, pid)
            If hProc <> IntPtr.Zero Then
                r = Native.Api.NativeFunctions.NtSuspendProcess(hProc)
                Native.Api.NativeFunctions.CloseHandle(hProc)
                Return (r <> 0)
            Else
                Return False
            End If
        End Function

        ' Kill a process
        Public Shared Function KillProcessById(ByVal pid As Integer, _
                                               Optional ByVal exitcode As Integer = 0) As Boolean
            Dim hProc As IntPtr
            Dim ret As Boolean
            hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.Terminate, _
                                                           False, pid)
            If hProc <> IntPtr.Zero Then
                ret = Native.Api.NativeFunctions.TerminateProcess(hProc, exitcode)
                Native.Api.NativeFunctions.CloseHandle(hProc)
                Return ret
            Else
                Return False
            End If
        End Function

        ' Get PIDs of child processes
        Public Shared Function EnumerateChildProcessesById(ByVal pid As Integer) As List(Of Integer)
            Dim ret As Integer
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                                                                IntPtr.Zero, 0, ret)
            Dim size As Integer = ret
            Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                                                                ptr, size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Dim _list As New List(Of Integer)
            Do While True
                Dim obj As Native.Api.NativeStructs.SystemProcessInformation = CType(Marshal.PtrToStructure(ptr.Increment(offset), _
                                                                                                            GetType(Native.Api.NativeStructs.SystemProcessInformation)),  _
                                                                                                            Native.Api.NativeStructs.SystemProcessInformation)
                offset += obj.NextEntryOffset
                If obj.InheritedFromProcessId = pid Then
                    _list.Add(obj.ProcessId)
                End If
                If obj.NextEntryOffset = 0 Then
                    Exit Do
                End If
                x += 1
            Loop
            Marshal.FreeHGlobal(ptr)

            Return _list
        End Function

        ' Create a minidump
        Public Shared Function CreateMiniDumpFileById(ByVal pid As Integer, _
                                                      ByVal file As String, _
                                                      ByVal type As Native.Api.NativeEnums.MiniDumpType) As Boolean
            Dim hProc As IntPtr
            Dim ret As Integer = -1
            hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.QueryInformation Or _
                                    Native.Security.ProcessAccess.VmRead, _
                                    False, pid)
            If hProc <> IntPtr.Zero Then
                ' Create dump file
                Dim fs As New System.IO.FileStream(file, System.IO.FileMode.Create)
                ' Write dump file
                Native.Api.NativeFunctions.MiniDumpWriteDump(hProc, pid, _
                                            fs.SafeFileHandle.DangerousGetHandle(), _
                                            type, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero)
                Native.Api.NativeFunctions.CloseHandle(hProc)
                fs.Close()
                Return ret <> 0
            Else
                Return False
            End If

        End Function



        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
