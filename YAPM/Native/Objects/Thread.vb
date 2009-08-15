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

    Public Class Thread

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Memory alloc for thread enumeration
        Private Shared memAllocForThreadEnum As New Native.Memory.MemoryAlloc(&H1000)


        ' ========================================
        ' Public properties
        ' ========================================

        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Get affinity
        Public Shared Function GetThreadAffinityByHandle(ByVal handle As IntPtr) As IntPtr
            Dim infos As New Native.Api.NativeStructs.ThreadBasicInformation
            Dim ret As Integer
            Native.Api.NativeFunctions.NtQueryInformationThread(handle, _
                        Native.Api.NativeEnums.ThreadInformationClass.ThreadBasicInformation, _
                        infos, Marshal.SizeOf(infos), ret)
            Return infos.AffinityMask
        End Function

        ' Set affinity
        Public Shared Function SetThreadAffinityById(ByVal tid As Integer, ByVal affinity As IntPtr) As Boolean
            Dim hThread As IntPtr
            Dim r As Boolean
            hThread = Native.Api.NativeFunctions.OpenThread(Native.Security.ThreadAccess.QueryInformation Or _
                                                Security.ThreadAccess.SetInformation, _
                                                False, tid)
            If hThread <> IntPtr.Zero Then
                r = (Native.Api.NativeFunctions.SetThreadAffinityMask(hThread, affinity) <> IntPtr.Zero)
                Native.Api.NativeFunctions.CloseHandle(hThread)
            End If
            Return r
        End Function

        ' Set priority
        Public Shared Function SetThreadPriorityById(ByVal tid As Integer, ByVal priority As System.Diagnostics.ThreadPriorityLevel) As Boolean
            Dim hThread As IntPtr
            Dim r As Boolean
            hThread = Native.Api.NativeFunctions.OpenThread(Native.Security.ThreadAccess.SetInformation, _
                                                          False, tid)
            If hThread <> IntPtr.Zero Then
                r = Native.Api.NativeFunctions.SetThreadPriority(hThread, priority)
                Native.Api.NativeFunctions.CloseHandle(hThread)
            End If
            Return r
        End Function

        ' Get priority of a thread
        Public Shared Function GetThreadPriorityByHandle(ByVal handle As IntPtr) As Integer
            Return Native.Api.NativeFunctions.GetThreadPriority(handle)
        End Function

        ' Get a valid handle on a thread
        Public Shared Function GetThreadHandle(ByVal tid As Integer, ByVal access As Native.Security.ThreadAccess) As IntPtr
            Return Native.Api.NativeFunctions.OpenThread(access, False, tid)
        End Function

        ' Resume a thread
        Public Shared Function ResumeThreadByHandle(ByVal hThread As IntPtr) As Boolean
            If hThread <> IntPtr.Zero Then
                Return NativeFunctions.ResumeThread(hThread) > 0
            Else
                Return False
            End If
        End Function
        Public Shared Function ResumeThreadById(ByVal thread As Integer) As Boolean

            ' Open handle, resume thread and close handle
            Dim hThread As IntPtr = _
                    NativeFunctions.OpenThread(Security.ThreadAccess.SuspendResume, False, thread)
            Dim ret As Boolean = ResumeThreadByHandle(hThread)
            NativeFunctions.CloseHandle(hThread)

            Return ret
        End Function


        ' Suspend a thread
        Public Shared Function SuspendThreadByHandle(ByVal hThread As IntPtr) As Boolean
            If hThread <> IntPtr.Zero Then
                Return NativeFunctions.SuspendThread(hThread) > 0
            Else
                Return False
            End If
        End Function
        Public Shared Function SuspendThreadById(ByVal thread As Integer) As Boolean

            ' Open handle, suspend thread and close handle
            Dim hThread As IntPtr = _
                    NativeFunctions.OpenThread(Security.ThreadAccess.SuspendResume, False, thread)
            Dim ret As Boolean = SuspendThreadByHandle(hThread)
            NativeFunctions.CloseHandle(hThread)

            Return ret
        End Function

        ' Kill a thread
        Public Shared Function KillThreadById(ByVal tid As Integer, _
                                              Optional ByVal exitCode As Integer = 0) As Boolean
            Dim hThread As IntPtr
            Dim ret As Boolean
            hThread = Native.Api.NativeFunctions.OpenThread(Native.Security.ThreadAccess.Terminate, _
                                                          False, tid)
            If hThread <> IntPtr.Zero Then
                ret = Native.Api.NativeFunctions.TerminateThread(hThread, exitCode)
                Native.Api.NativeFunctions.CloseHandle(hThread)
            End If

            Return ret
        End Function

        ' Enumerate threads
        Public Shared Sub EnumerateThreadsByProcessId(ByRef _dico As Dictionary(Of String, threadInfos), ByVal pid() As Integer)

            Dim deltaOff As Integer = Marshal.SizeOf(GetType(Native.Api.NativeStructs.SystemProcessInformation))

            Dim ret As Integer
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)
            If memAllocForThreadEnum.Size < ret Then
                memAllocForThreadEnum.Resize(ret)
            End If
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Do While True

                Dim obj As Native.Api.NativeStructs.SystemProcessInformation = _
                        memAllocForThreadEnum.ReadStructOffset(Of Native.Api.NativeStructs.SystemProcessInformation)(offset)

                ' Do we have do get threads for this process ?
                Dim bHaveToGetThreads As Boolean = False
                For Each tPid As Integer In pid
                    If tPid = obj.ProcessId Then
                        bHaveToGetThreads = True
                        Exit For
                    End If
                Next

                If bHaveToGetThreads Then
                    For j As Integer = 0 To obj.NumberOfThreads - 1

                        Dim thread As Native.Api.NativeStructs.SystemThreadInformation = _
                            memAllocForThreadEnum.ReadStruct(Of Native.Api.NativeStructs.SystemThreadInformation)(offset + deltaOff, j)

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



        ' ========================================
        ' Private functions
        ' ========================================

        
    End Class

End Namespace
