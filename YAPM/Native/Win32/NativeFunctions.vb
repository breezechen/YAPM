' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' This file uses third-party pieces of code under GNU 
' GPL 3.0 license. See below for details
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
'
'
' This file uses some work under GNU GPL 3.0 license
' The definitions marked with <from PH> are from Process Hacker by Wj32.

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Drawing
Imports System.Net
Imports Native.Api.NativeStructs
Imports Native.Api.NativeEnums

Namespace Native.Api

    Public Class NativeFunctions

        ' OK
#Region "Declarations used for jobs"

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function QueryInformationJobObject(<[In]()> ByVal JobHandle As IntPtr, _
                            <[In]()> ByVal JobInformationClass As JobObjectInformationClass, _
                            <Out()> ByVal JobInformation As IntPtr, _
                            <[In]()> ByVal JobInformationLength As Integer, _
                            <Out()> ByRef ReturnLength As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function OpenJobObject(<[In]()> ByVal DesiredAccess As Security.JobAccess, _
                            <[In]()> ByVal Inherit As Boolean, _
                            <[In]()> ByVal Name As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function TerminateJobObject(<[In]()> ByVal JobHandle As IntPtr, _
                                        <[In]()> ByVal ExitCode As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function AssignProcessToJobObject(<[In]()> ByVal JobHandle As IntPtr, _
                                        <[In]()> ByVal ProcessHandle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function CreateJobObject(<[In]()> ByRef SecurityAttributes As SecurityAttributes, _
                                        <[In]()> ByVal Name As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function SetInformationJobObject(<[In]()> ByVal JobHandle As IntPtr, _
                <[In]()> ByVal JobInformationClass As JobObjectInformationClass, _
                <Out()> ByVal JobInformation As IntPtr, _
                <[In]()> ByVal JobInformationLength As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for processes"

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetCurrentProcessId() As Integer
        End Function

        ' Vista and higher
        ' <from PH>
        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function QueryFullProcessImageName(<[In]()> ByVal ProcessHandle As IntPtr, _
                <[In]()> <MarshalAs(UnmanagedType.Bool)> ByVal UseNativeName As Boolean, _
                <Out()> ByVal ExeName As StringBuilder, _
                ByRef Size As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("ntdll.dll")> _
        Public Shared Function NtQueryInformationProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
                <[In]()> ByVal ProcessInformationClass As ProcessInformationClass, _
                ByVal ProcessInformation As IntPtr, _
                <[In]()> ByVal ProcessInformationLength As Integer, _
                <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQueryInformationProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
                <[In]()> ByVal ProcessInformationClass As ProcessInformationClass, _
                <Out()> ByRef ProcessInformation As IntPtr, _
                <[In]()> ByVal ProcessInformationLength As Integer, _
                <Out()> <[Optional]()> ByRef ReturnLength As UInteger) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQueryInformationProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
                <[In]()> ByVal ProcessInformationClass As ProcessInformationClass, _
                <Out()> ByRef ProcessInformation As ProcessBasicInformation, _
                <[In]()> ByVal ProcessInformationLength As Integer, _
                <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        ' <from PH>
        <DllImport("ntdll.dll")> _
        Public Shared Function NtQuerySystemInformation(<[In]()> ByVal SystemInformationClass As SystemInformationClass, _
                    <Out()> ByRef SystemInformation As SystemBasicInformation, _
                    <[In]()> ByVal SystemInformationLength As Integer, _
                    <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQuerySystemInformation(<[In]()> ByVal SystemInformationClass As SystemInformationClass, _
            <Out()> ByVal SystemInformation As IntPtr, _
            <[In]()> ByVal SystemInformationLength As Integer, _
            <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQuerySystemInformation(<[In]()> ByVal SystemInformationClass As SystemInformationClass, _
            <Out()> ByRef SystemInformation As SystemCacheInformation, _
            <[In]()> ByVal SystemInformationLength As Integer, _
            <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQuerySystemInformation(<[In]()> ByVal SystemInformationClass As SystemInformationClass, _
            <Out()> ByRef SystemInformation As SystemPerformanceInformation, _
            <[In]()> ByVal SystemInformationLength As Integer, _
            <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQuerySystemInformation(<[In]()> ByVal SystemInformationClass As SystemInformationClass, _
            <Out()> ByRef SystemInformation As SystemProcessorPerformanceInformation, _
            <[In]()> ByVal SystemInformationLength As Integer, _
            <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

        <DllImport("kernel32.dll")> _
        Public Shared Function SetProcessWorkingSetSize(ByVal hProcess As IntPtr, _
                            ByVal dwMinimumWorkingSetSize As IntPtr, _
                            ByVal dwMaximumWorkingSetSize As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetProcessIdOfThread(ByVal ThreadHandle As IntPtr) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetThreadId(ByVal ThreadHandle As IntPtr) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetProcessId(ByVal ProcessHandle As IntPtr) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function IsProcessInJob(ByVal ProcessHandle As IntPtr, _
                        ByVal JobHandle As IntPtr, ByRef Result As Boolean) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function CheckRemoteDebuggerPresent(ByVal ProcessHandle As IntPtr, _
                        <MarshalAs(UnmanagedType.Bool)> ByRef DebuggerPresent As Boolean) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetCurrentThreadId() As Integer
        End Function

        <DllImport("dbghelp.dll")> _
        Public Shared Function MiniDumpWriteDump(ByVal hProcess As IntPtr, _
                        ByVal ProcessId As Integer, ByVal hFile As IntPtr, _
                        ByVal DumpType As Integer, ByVal ExceptionParam As IntPtr, _
                        ByVal UserStreamParam As IntPtr, _
                        ByVal CallackParam As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function OpenProcess(ByVal DesiredAccess As Security.ProcessAccess, _
                                           ByVal InheritHandle As Boolean, _
                                           ByVal ProcessId As Integer) As IntPtr
        End Function

        ' Vista and higher
        ' <from PH>
        <DllImport("ntdll.dll")> _
        Public Shared Function NtGetNextProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
                    <[In]()> ByVal DesiredAccess As Security.ProcessAccess, _
                    <[In]()> ByVal HandleAttributes As HandleFlags, _
                    <[In]()> ByVal Flags As Integer, _
                    <Out()> ByRef NewProcessHandle As IntPtr) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtOpenProcess(<Out()> ByRef ProcessHandle As IntPtr, _
                            <[In]()> ByVal DesiredAccess As Security.ProcessAccess, _
                            <[In]()> ByRef ObjectAttributes As ObjectAttributes, _
                            <[In]()> ByRef ClientId As ClientId) As UInteger
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Sub ExitProcess(ByVal ExitCode As Integer)
        End Sub

        <DllImport("ntdll.dll", SetLastError:=True)> _
        Public Shared Function NtTerminateProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
            <[In]()> ByVal ExitCode As Integer) As UInteger
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function OpenProcessToken(<[In]()> ByVal ProcessHandle As IntPtr, _
                        <[In]()> ByVal DesiredAccess As Security.TokenAccess, _
                        <Out()> ByRef TokenHandle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtSuspendProcess(<[In]()> ByVal ProcessHandle As IntPtr) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtResumeProcess(<[In]()> ByVal ProcessHandle As IntPtr) As UInteger
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function SetPriorityClass(<[In]()> ByVal ProcessHandle As IntPtr, _
            <[In]()> ByVal Priority As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function SetProcessAffinityMask(<[In]()> ByVal ProcessHandle As IntPtr, _
            <[In]()> ByVal ProcessAffinityMask As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetGuiResources(<[In]()> ByVal ProcessHandle As IntPtr, _
                <[In]()> ByVal UserObjects As GuiResourceType) As Integer
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Ansi, SetLastError:=True)> _
        Public Shared Function GetProcAddress(<[In]()> ByVal [Module] As IntPtr, _
                                        <[In]()> ByVal ProcOrdinal As UShort) As IntPtr
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Ansi, SetLastError:=True)> _
        Public Shared Function GetProcAddress(<[In]()> ByVal [Module] As IntPtr, _
                                <[In]()> ByVal ProcOrdinal As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetCurrentProcess() As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetExitCodeProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
            <Out()> ByRef ExitCode As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetExitCodeProcess(<[In]()> ByVal ProcessHandle As IntPtr, _
            <Out()> ByRef ExitCode As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetProcessDEPPolicy(<[In]()> ByVal ProcessHandle As IntPtr, _
                <Out()> ByRef Flags As DepFlags, _
                <Out()> <MarshalAs(UnmanagedType.Bool)> ByRef Permanent As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function Heap32ListFirst(<[In]()> ByVal hSnapshot As IntPtr, _
                                                ByRef lphl As HeapList32) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function Heap32ListNext(ByVal hSnapshot As IntPtr, _
                                            ByRef lphl As HeapList32) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function Heap32First(ByRef lphe As HeapEntry32, _
                                           ByVal th32ProcessID As Integer, _
                                           ByVal th32HeapID As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function Heap32Next(ByRef lphe As HeapEntry32) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function CreateToolhelp32Snapshot(ByVal dwFlags As Toolhelp32SnapshotFlags, _
                                        ByVal th32ProcessID As Integer) As IntPtr
        End Function

#End Region

        ' OK
#Region "Declarations used for modules"

        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function GetModuleHandle(ByVal ModuleName As String) As IntPtr
        End Function

        <DllImport("psapi.dll")> _
        Public Shared Function EnumProcessModules(<[In]()> ByVal ProcessHandle As IntPtr, _
                    <Out()> ByVal ModuleHandles As IntPtr(), <[In]()> ByVal Size As Integer, _
                    <Out()> ByRef RequiredSize As Integer) As Boolean
        End Function

        <DllImport("psapi.dll")> _
        Public Shared Function EnumProcessModules(<[In]()> ByVal ProcessHandle As IntPtr, _
                    <Out()> ByVal ModuleHandles As IntPtr, <[In]()> ByVal Size As Integer, _
                    <Out()> ByRef RequiredSize As Integer) As Boolean
        End Function

        <DllImport("psapi.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function GetModuleBaseName(<[In]()> ByVal ProcessHandle As IntPtr, _
                        <[In]()> <[Optional]()> ByVal ModuleHandle As IntPtr, _
                        <Out()> ByVal BaseName As StringBuilder, _
                        <[In]()> ByVal Size As Integer) As Integer
        End Function

        <DllImport("psapi.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function GetModuleFileNameEx(<[In]()> ByVal ProcessHandle As IntPtr, _
                        <[In]()> <[Optional]()> ByVal ModuleHandle As IntPtr, _
                        <Out()> ByVal FileName As StringBuilder, _
                        <[In]()> ByVal Size As Integer) As Integer
        End Function

        ' Unused in non-commented blocks
        '<DllImport("psapi.dll")> _
        'Public Shared Function GetModuleInformation(<[In]()> ByVal ProcessHandle As IntPtr, _
        '            <[In]()> <[Optional]()> ByVal ModuleHandle As IntPtr, _
        '            <Out()> ByVal ModInfo As MODULEINFO, _
        '            <[In]()> ByVal Size As Integer) As Boolean
        'End Function

#End Region

        ' OK
#Region "Declarations used for memory management"

        ' <from PH>
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function VirtualQueryEx(ByVal Process As IntPtr, _
                    ByVal Address As IntPtr, _
                    <MarshalAs(UnmanagedType.Struct)> ByRef Buffer As MemoryBasicInformation, _
                    ByVal Size As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function VirtualFreeEx(ByVal Process As IntPtr, _
                                             ByVal Address As IntPtr, _
                                             ByVal Size As Integer, _
                                             ByVal FreeType As MemoryState) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function VirtualProtectEx(ByVal Process As IntPtr, _
                                             ByVal Address As IntPtr, _
                                             ByVal Size As Integer, _
                                             ByVal NewProtect As MemoryProtectionType, _
                                             ByRef OldProtect As MemoryProtectionType) As Boolean
        End Function

        <DllImport("psapi.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function GetMappedFileName(ByVal ProcessHandle As IntPtr, _
                                                 ByVal Address As IntPtr, _
                                                 ByVal Buffer As StringBuilder, _
                                                 ByVal Size As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function ReadProcessMemory(ByVal Process As IntPtr, _
                                                    ByVal BaseAddress As IntPtr, _
                                                    ByVal Buffer As Byte(), _
                                                    ByVal Size As Integer, _
                                                    ByRef BytesRead As Integer) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function ReadProcessMemory(ByVal Process As IntPtr, _
                                                    ByVal BaseAddress As IntPtr, _
                                                    ByVal Buffer As Short(), _
                                                    ByVal Size As Integer, _
                                                    ByRef BytesRead As Integer) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function ReadProcessMemory(ByVal Process As IntPtr, _
                                            ByVal BaseAddress As IntPtr, _
                                            ByVal Buffer As IntPtr(), _
                                            ByVal Size As Integer, _
                                            ByRef BytesRead As Integer) As Boolean

        End Function
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function ReadProcessMemory(ByVal Process As IntPtr, _
                                                    ByVal BaseAddress As IntPtr, _
                                                    ByVal Buffer As Integer(), _
                                                    ByVal Size As Integer, _
                                                    ByRef BytesRead As Integer) As Boolean
        End Function
#End Region

        ' OK
#Region "Declarations used for threads"

        '<from PH>
        <DllImport("ntdll.dll")> _
        Public Shared Function NtGetNextThread(<[In]()> ByVal ProcessHandle As IntPtr, _
                                <[In]()> ByVal ThreadHandle As IntPtr, _
                                <[In]()> ByVal DesiredAccess As Security.ThreadAccess, _
                                <[In]()> ByVal HandleAttributes As HandleFlags, _
                                <[In]()> ByVal Flags As Integer, _
                                <Out()> ByRef NewThreadHandle As IntPtr) As UInteger
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetExitCodeThread(<[In]()> ByVal ThreadHandle As IntPtr, _
                <Out()> ByRef ExitCode As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll")> _
        Public Shared Function SetThreadAffinityMask(ByVal hThread As IntPtr, _
                                    ByVal dwThreadAffinityMask As IntPtr) As IntPtr
        End Function

        ' <from PH>
        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function CreateRemoteThread(<[In]()> ByVal ProcessHandle As IntPtr, _
                            <[In]()> ByVal ThreadAttributes As IntPtr, _
                            <[In]()> ByVal StackSize As Integer, _
                            <[In]()> ByVal StartAddress As IntPtr, _
                            <[In]()> ByVal Parameter As IntPtr, _
                            <[In]()> ByVal CreationFlags As RemoteThreadCreationFlags, _
                            <Out()> ByRef ThreadId As Integer) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function OpenThread(<[In]()> ByVal DesiredAccess As Security.ThreadAccess, _
                                          <[In]()> ByVal InheritHandle As Boolean, _
                                          <[In]()> ByVal ThreadId As Integer) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function TerminateThread(ByVal ThreadHandle As IntPtr, _
                                               <[In]()> ByVal ExitCode As Integer) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function SuspendThread(<[In]()> ByVal ThreadHandle As IntPtr) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function ResumeThread(<[In]()> ByVal ThreadHandle As IntPtr) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function SetThreadPriority(<[In]()> ByVal ThreadHandle As IntPtr, _
                                                 <[In]()> ByVal Priority As Integer) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function GetThreadPriority(<[In]()> ByVal ThreadHandle As IntPtr) As Integer
        End Function

        ' <from PH>
        <DllImport("ntdll.dll")> _
        Public Shared Function NtQueryInformationThread(<[In]()> ByVal ThreadHandle As IntPtr, _
                    <[In]()> ByVal ThreadInformationClass As ThreadInformationClass, _
                    ByRef ThreadInformation As ThreadBasicInformation, _
                    <[In]()> ByVal ThreadInformationLength As Integer, _
                    <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
        End Function

#End Region

        ' OK
#Region "Declarations used for tokens & privileges"

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function GetTokenInformation(<[In]()> ByVal TokenHandle As IntPtr, _
                <[In]()> ByVal TokenInformationClass As TokenInformationClass, _
                <Out()> <[Optional]()> ByVal TokenInformation As IntPtr, _
                <[In]()> ByVal TokenInformationLength As Integer, _
                <Out()> ByRef ReturnLength As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function GetTokenInformation(<[In]()> ByVal TokenHandle As IntPtr, _
                <[In]()> ByVal TokenInformationClass As TokenInformationClass, _
                <[Optional]()> ByRef TokenInformation As TokenSource, _
                <[In]()> ByVal TokenInformationLength As Integer, _
                <Out()> ByRef ReturnLength As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function GetTokenInformation(<[In]()> ByVal TokenHandle As IntPtr, _
                <[In]()> ByVal TokenInformationClass As TokenInformationClass, _
                <[Optional]()> ByRef TokenInformation As TokenStatistics, _
                <[In]()> ByVal TokenInformationLength As Integer, _
                <Out()> ByRef ReturnLength As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function AdjustTokenPrivileges(<[In]()> ByVal TokenHandle As IntPtr, _
                <[In]()> <MarshalAs(UnmanagedType.Bool)> ByVal DisableAllPrivileges As Boolean, _
                <[In]()> <[Optional]()> ByRef NewState As TokenPrivileges, _
                <[In]()> ByVal BufferLength As Integer, _
                <Out()> <[Optional]()> ByVal PreviousState As IntPtr, _
                <Out()> <[Optional]()> ByVal ReturnLength As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function LookupPrivilegeDisplayName(<[In]()> <[Optional]()> ByVal SystemName As String, _
            <[In]()> ByVal Name As String, _
            <Out()> <[Optional]()> ByVal DisplayName As StringBuilder, _
            ByRef DisplayNameSize As Integer, _
            <Out()> ByRef LanguageId As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function LookupPrivilegeName(<[In]()> <[Optional]()> ByVal SystemName As String, _
                    <[In]()> ByRef Luid As Luid, _
                    <Out()> <[Optional]()> ByVal Name As StringBuilder, _
                    ByRef RequiredSize As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function LookupPrivilegeValue(<[In]()> <[Optional]()> ByVal SystemName As String, _
                    <[In]()> ByVal PrivilegeName As String, _
                    <Out()> ByRef Luid As Luid) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function LookupAccountSid(<[In]()> <[Optional]()> ByVal SystemName As String, _
                <[In]()> ByVal Sid As IntPtr, _
                <Out()> <[Optional]()> ByVal Name As StringBuilder, _
                ByRef NameSize As Integer, _
                <Out()> <[Optional]()> ByVal ReferencedDomainName As StringBuilder, _
                ByRef ReferencedDomainNameSize As Integer, _
                <Out()> ByRef Use As SidNameUse) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for network"

        <DllImport("mpr.dll")> _
        Public Shared Function WNetAddConnection2(ByRef netResource As NetResource, _
                                                  ByVal password As String, _
                                                  ByVal username As String, _
                                                  ByVal flags As AddConnectionFlag) As Integer
        End Function

        <DllImport("mpr.dll")> _
        Public Shared Function WNetCancelConnection2(ByVal lpName As String, _
                                                     ByVal dwFlags As Int32, _
                                                     ByVal bForce As Boolean) As Integer
        End Function

        <DllImport("iphlpapi.dll", SetLastError:=True)> _
        Public Shared Function GetTcpStatistics(ByRef stats As NativeStructs.MibTcpStats) As Integer
        End Function

        <DllImport("iphlpapi.dll", SetLastError:=True)> _
        Public Shared Function GetUdpStatistics(ByRef stats As NativeStructs.MibUdpStats) As Integer
        End Function


        <DllImport("iphlpapi.dll", SetLastError:=True)> _
        Public Shared Function GetExtendedTcpTable(ByVal Table As IntPtr, _
                                                ByRef Size As Integer, _
                                                ByVal Order As Boolean, _
                                                ByVal IpVersion As IpVersion, _
                                                ByVal TableClass As Enums.TcpTableClass, _
                                                ByVal Reserved As Integer) As Integer
        End Function

        <DllImport("iphlpapi.dll", SetLastError:=True)> _
        Public Shared Function GetExtendedUdpTable(ByVal Table As IntPtr, _
                                                ByRef Size As Integer, _
                                                ByVal Order As Boolean, _
                                                ByVal IpVersion As IpVersion, _
                                                ByVal TableClass As Enums.UdpTableClass, _
                                                ByVal Reserved As Integer) As Integer
        End Function

        <DllImport("iphlpapi.dll", SetLastError:=True)> _
        Public Shared Function SetTcpEntry(ByRef TcpRow As MibTcpRow) As Integer
        End Function

#End Region

        ' OK
#Region "Declarations used for files"

        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function CopyFile(ByVal lpExistingFileName As String, _
                            ByVal lpNewFileName As String, _
                            ByVal bFailIfExists As Boolean) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function CreateFileMapping(ByVal hFile As IntPtr, _
                 ByVal lpFileMappingAttributes As IntPtr, _
                 ByVal flProtect As FileMapProtection, _
                 ByVal dwMaximumSizeHigh As UInteger, _
                 ByVal dwMaximumSizeLow As UInteger, _
                 <MarshalAs(UnmanagedType.LPTStr)> ByVal lpName As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function OpenFileMapping(ByVal dwDesiredAccess As UInteger, _
                                ByVal bInheritHandle As Boolean, _
                                ByVal lpName As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function UnmapViewOfFile(ByVal lpBaseAddress As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function MapViewOfFile(ByVal hFileMappingObject As IntPtr, _
                                     ByVal dwDesiredAccess As FileMapAccess, _
                                     ByVal dwFileOffsetHigh As UInteger, _
                                     ByVal dwFileOffsetLow As UInteger, _
                                     ByVal dwNumberOfBytesToMap As UInteger) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function QueryDosDevice(ByVal DeviceName As String, _
                                        ByVal TargetPath As StringBuilder, _
                                        ByVal MaxLength As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function GetShortPathName(ByVal longPath As String, _
              <MarshalAs(UnmanagedType.LPTStr)> ByVal ShortPath As System.Text.StringBuilder, _
              <MarshalAs(Runtime.InteropServices.UnmanagedType.U4)> ByVal bufferSize As Integer) As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function CreateFile(ByVal lpFileName As String, _
                                ByVal dwDesiredAccess As EFileAccess, _
                                ByVal dwShareMode As EFileShare, _
                                ByVal lpSecurityAttributes As IntPtr, _
                                ByVal dwCreationDisposition As ECreationDisposition, _
                                ByVal dwFlagsAndAttributes As EFileAttributes, _
                                ByVal hTemplateFile As IntPtr) As IntPtr
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function GetFileSizeEx(<[In]()> ByVal hFile As IntPtr, _
                                    <[In](), Out()> ByRef lpFileSize As Long) As Boolean
        End Function

        <DllImport("shell32.dll", EntryPoint:="#61", CharSet:=CharSet.Unicode)> _
        Public Shared Function RunFileDlg(<[In]()> ByVal hWnd As IntPtr, _
                                          <[In]()> ByVal Icon As IntPtr, _
                                          <[In]()> ByVal Path As String, _
                                          <[In]()> ByVal Title As String, _
                                          <[In]()> ByVal Prompt As String, _
                                          <[In]()> ByVal Flags As RunFileDialogFlags) As Integer
        End Function

        <DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
        Public Shared Function ShellExecuteEx(ByRef lpExecInfo As ShellExecuteInfo) As Boolean
        End Function

        <DllImport("User32", SetLastError:=True)> _
        Public Shared Function LoadString(ByVal hInstance As IntPtr, _
                                          ByVal uID As UInt32, _
                                          ByVal lpBuffer As System.Text.StringBuilder, _
                                          ByVal nBufferMax As Integer) As Integer
        End Function

        <DllImport("shell32.dll")> _
        Public Shared Function FindExecutable(ByVal lpFile As String, _
                                              ByVal lpDirectory As String, _
                                              ByVal lpResult As StringBuilder) As IntPtr
        End Function

        <DllImport("kernel32.dll")> _
        Public Shared Function GetCompressedFileSize(ByVal lpFileName As String, _
                                                     ByRef lpFileSizeHigh As UInteger) As UInteger
        End Function

        <DllImport("shell32.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function SHFileOperation(<[In]()> ByRef lpFileOp As ShFileOpStruct) As Integer
        End Function

        <DllImport("shell32.dll", CharSet:=CharSet.Auto)> _
       Public Shared Function SHGetFileInfo(ByVal pszPath As String, _
                         ByVal dwFileAttributes As Integer, _
                         ByRef psfi As SHFileInfo, _
                         ByVal cbFileInfo As Integer, _
                         ByVal uFlags As Integer) As Integer
        End Function

        <DllImport("wintrust.dll", ExactSpelling:=True, SetLastError:=False, CharSet:=CharSet.Unicode)> _
        Public Shared Function WinVerifyTrust(<[In]()> ByVal hwnd As IntPtr, _
            <[In]()> <MarshalAs(UnmanagedType.LPStruct)> ByVal pgActionID As Guid, _
            <[In]()> ByVal pWVTData As WinTrustData) As WinVerifyTrustResult
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function WriteFile(ByVal FileHandle As IntPtr, _
                                         ByVal Buffer As Byte(), _
                                         ByVal Bytes As Integer, _
                                         ByRef WrittenBytes As Integer, _
                                         ByVal Overlapped As IntPtr) As Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for system"

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function DeviceIoControl(ByVal FileHandle As IntPtr, _
                                ByVal IoControlCode As Integer, _
                                ByRef InBuffer As NativeStructs.SystemHandleEntry, _
                                ByVal InBufferLength As Integer, _
                                ByVal OutBuffer As IntPtr, _
                                ByVal OutBufferLength As Integer, _
                                ByRef BytesReturned As Integer, _
                                ByVal Overlapped As IntPtr) As Boolean
        End Function

        <DllImport("kernel32.dll")> _
        Public Shared Sub GetSystemInfo(<MarshalAs(UnmanagedType.Struct)> ByRef lpSystemInfo As SystemInfo)
        End Sub

        <DllImport("psapi.dll", SetLastError:=True)> _
        Public Shared Function GetPerformanceInfo(<Out()> ByRef PerformanceInformation As PerformanceInformation, _
                                                  <[In]()> ByVal Size As Integer) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function ExitWindowsEx(<[In]()> ByVal flags As ExitWindowsFlags, _
                <[In]()> ByVal reason As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("powrprof.dll", SetLastError:=True)> _
        Public Shared Function SetSuspendState(<[In]()> ByVal hibernate As Boolean, _
                            <[In]()> ByVal forceCritical As Boolean, _
                            <[In]()> ByVal disableWakeEvent As Boolean) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function LockWorkStation() As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for windows (not Windows :-p)"

        Public Shared Function GetClassLongPtr(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
            If IntPtr.Size = 8 Then
                ' 64 bits
                Return GetClassLongPtr64(hWnd, nIndex)
            Else
                ' 32 bits
                Return New IntPtr(GetClassLongPtr32(hWnd, nIndex))
            End If
        End Function
        <DllImport("user32.dll", EntryPoint:="GetClassLong")> _
        Private Shared Function GetClassLongPtr32(ByVal hWnd As IntPtr, _
                                                ByVal nIndex As Integer) As UInteger
        End Function
        <DllImport("user32.dll", EntryPoint:="GetClassLongPtr")> _
        Private Shared Function GetClassLongPtr64(ByVal hWnd As IntPtr, _
                                                 ByVal nIndex As Integer) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetDesktopWindow() As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetForegroundWindow() As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function WindowFromPoint(<[In]()> ByVal location As Point) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetCursorPos(<Out()> ByRef location As NativeStructs.PointApi) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function Rectangle(<[In]()> ByVal hDC As IntPtr, _
                                <[In]()> ByVal LeftRect As Integer, _
                                <[In]()> ByVal TopRect As Integer, _
                                <[In]()> ByVal RightRect As Integer, _
                                <[In]()> ByVal BottomRect As Integer) As Boolean
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function DeleteObject(<[In]()> ByVal [Object] As IntPtr) As Boolean
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function GetStockObject(<[In]()> ByVal [Object] As GdiStockObject) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function SelectObject(<[In]()> ByVal hDC As IntPtr, _
                                <[In]()> ByVal hGdiObject As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function CreatePen(<[In]()> ByVal PenStyle As GdiPenStyle, _
                                    <[In]()> ByVal Width As Integer, _
                                    <[In]()> ByVal Color As IntPtr) As IntPtr
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function RestoreDC(<[In]()> ByVal hDC As IntPtr, _
                                         <[In]()> ByVal SavedDC As Integer) As Boolean
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function SaveDC(<[In]()> ByVal hDC As IntPtr) As Integer
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function SetROP2(<[In]()> ByVal hDC As IntPtr, _
                            <[In]()> ByVal DrawMode As GdiBlendMode) As GdiBlendMode
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function GetClientRect(<[In]()> ByVal hWnd As IntPtr, _
                    <Out()> ByRef rect As Rect) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function ReleaseDC(<[In]()> ByVal hWnd As IntPtr, _
                                         <[In]()> ByVal hDC As IntPtr) As Integer
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetWindowDC(<[In]()> ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function SetCapture(<[In]()> ByVal handle As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function ReleaseCapture() As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
        <DllImport("user32.dll")> _
        Public Shared Function SetForegroundWindow(<[In]()> ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function SetActiveWindow(<[In]()> ByVal hWnd As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetWindowThreadProcessId(<[In]()> ByVal hWnd As IntPtr, <Out()> ByRef processId As Integer) As Integer
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function GetWindow(ByVal hWnd As IntPtr, _
                                         ByVal uCmd As GetWindowCmd) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function EnableWindow(ByVal hWnd As IntPtr, _
                                            ByVal bEnable As Boolean) As Boolean
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function GetClipRgn(ByVal hdc As IntPtr, _
                                          ByVal hrgn As IntPtr) As Integer
        End Function

        <DllImport("gdi32.dll")> _
        Public Shared Function CreateRectRgn(ByVal nLeftRect As Integer, _
                                            ByVal nTopRect As Integer, _
                                            ByVal nRightRect As Integer, _
                                            ByVal nBottomRect As Integer) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function FlashWindowEx(ByRef pwfi As FlashWInfo) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function SetLayeredWindowAttributes(ByVal hwnd As IntPtr, _
                                                          ByVal crKey As UInteger, _
                                                          ByVal bAlpha As Byte, _
                                                          ByVal dwFlags As UInteger) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function GetLayeredWindowAttributes(ByVal hwnd As IntPtr, _
                                                          ByRef crKey As UInteger, _
                                                          ByRef bAlpha As Byte, _
                                                          ByRef dwFlags As UInteger) As Boolean
        End Function

        '<DllImport("user32.dll", SetLastError:=True, EntryPoint:="SetWindowLongPtr", CharSet:=CharSet.Auto)> _
        'Public Shared Function SetWindowLongPtr(<[In]()> ByVal hWnd As IntPtr, _
        '    <[In]()> ByVal Index As GetWindowLongOffset, _
        '    <[In]()> <MarshalAs(UnmanagedType.FunctionPtr)> ByVal WndProc As IntPtr) As IntPtr
        'End Function

        Public Shared Function SetWindowLongPtr(ByVal hWnd As IntPtr, _
                                                ByVal nIndex As Integer, _
                                                ByVal dwNewLong As IntPtr) As IntPtr
            If IntPtr.Size = 8 Then
                Return SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
            Else
                Return New IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32))
            End If
        End Function
        <DllImport("user32.dll", EntryPoint:="SetWindowLong")> _
        Private Shared Function SetWindowLong32(ByVal hWnd As IntPtr, _
                            ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
        End Function
        <DllImport("user32.dll", EntryPoint:="SetWindowLongPtr")> _
        Private Shared Function SetWindowLongPtr64(ByVal hWnd As IntPtr, _
                            ByVal nIndex As Integer, ByVal dwNewLong As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, _
                    ByVal hWndInsertAfter As IntPtr, _
                    ByVal X As Integer, _
                    ByVal Y As Integer, _
                    ByVal W As Integer, _
                    ByVal H As Integer, _
                    ByVal uFlags As UInt32) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function ShowWindow(ByVal hwnd As IntPtr, _
                                    ByVal nCmdShow As ShowWindowType) As Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, _
                        ByVal Msg As Native.Api.NativeEnums.LVM, _
                        ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, _
                ByVal Msg As Integer, _
                ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, _
                        ByVal Msg As WindowMessage, _
                        ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SendMessageTimeout(ByVal windowHandle As IntPtr, _
                    ByVal Msg As WindowMessage, _
                    ByVal wParam As IntPtr, _
                    ByVal lParam As IntPtr, _
                    ByVal flags As SendMessageTimeoutFlags, _
                    ByVal timeout As Integer, _
                    ByRef result As IntPtr) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function SetWindowText(ByVal hwnd As IntPtr, _
                        ByVal lpString As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function GetWindowText(ByVal hwnd As IntPtr, _
                        ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function IsWindowVisible(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        '<DllImport("user32.dll", SetLastError:=True, EntryPoint:="GetWindowLongPtr", CharSet:=CharSet.Auto)> _
        'Public Shared Function GetWindowLongPtr(<[In]()> ByVal hWnd As IntPtr, _
        '                    <[In]()> ByVal Index As GetWindowLongOffset) As IntPtr
        'End Function

        <DllImport("user32.dll", EntryPoint:="GetWindowLong")> _
        Private Shared Function GetWindowLongPtr32(ByVal hWnd As IntPtr, _
                                                   ByVal nIndex As Integer) As IntPtr
        End Function
        <DllImport("user32.dll", EntryPoint:="GetWindowLongPtr")> _
        Private Shared Function GetWindowLongPtr64(ByVal hWnd As IntPtr, _
                                                   ByVal nIndex As Integer) As IntPtr
        End Function
        ' This static method is required because Win32 does not support
        ' GetWindowLongPtr directly
        Public Shared Function GetWindowLongPtr(ByVal hWnd As IntPtr, _
                                                ByVal nIndex As Integer) As IntPtr
            If IntPtr.Size = 8 Then
                Return GetWindowLongPtr64(hWnd, nIndex)
            Else
                Return GetWindowLongPtr32(hWnd, nIndex)
            End If
        End Function


        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Sub GetClassName(ByVal hWnd As System.IntPtr, _
                    ByVal lpClassName As System.Text.StringBuilder, _
                    ByVal nMaxCount As Integer)
        End Sub

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function IsWindowEnabled(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Shared Function GetWindowRect(<[In]()> ByVal hWnd As IntPtr, _
                <Out()> ByRef rect As Rect) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowPlacement(<[In]()> ByVal hWnd As IntPtr, _
                ByRef WindowPlacement As WindowPlacement) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function GetWindowPlacement(<[In]()> ByVal hWnd As IntPtr, _
                ByRef WindowPlacement As WindowPlacement) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for services"

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function QueryServiceStatus(ByVal Service As IntPtr, _
                                    ByRef ServiceStatus As ServiceStatus) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function ControlService(<[In]()> ByVal Service As IntPtr, _
                                              <[In]()> ByVal Control As ServiceControl, _
                                              <Out()> ByRef ServiceStatus As NativeStructs.ServiceStatusProcess) As Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function QueryServiceStatusEx(<[In]()> ByVal Service As IntPtr, _
                    <[In]()> ByVal InfoLevel As Integer, _
                    <Out()> <[Optional]()> ByRef ServiceStatus As ServiceStatusProcess, _
                    <[In]()> ByVal BufferSize As Integer, _
                    <Out()> ByRef BytesNeeded As Integer) As Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function EnumServicesStatusEx(<[In]()> ByVal SCManager As IntPtr, _
                        <[In]()> ByVal InfoLevel As IntPtr, _
                        <[In]()> ByVal ServiceType As ServiceQueryType, _
                        <[In]()> ByVal ServiceState As ServiceQueryState, _
                        <Out()> <[Optional]()> ByVal Services As IntPtr, _
                        <[In]()> ByVal BufSize As Integer, _
                        <Out()> ByRef BytesNeeded As Integer, _
                        <Out()> ByRef ServicesReturned As Integer, _
                        ByRef ResumeHandle As Integer, _
                        <[In]()> <[Optional]()> ByVal GroupName As String) As Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function DeleteService(<[In]()> ByVal Service As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function CloseServiceHandle(<[In]()> ByVal ServiceHandle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function CreateService(<[In]()> ByVal SCManager As IntPtr, _
                <[In]()> ByVal ServiceName As String, _
                <[In]()> ByVal DisplayName As String, _
                <[In]()> ByVal DesiredAccess As Native.Security.ServiceAccess, _
                <[In]()> ByVal ServiceType As ServiceType, _
                <[In]()> ByVal StartType As ServiceStartType, _
                <[In]()> ByVal ErrorControl As ServiceErrorControl, _
                <[In]()> ByVal BinaryPathName As String, _
                <[In]()> ByVal LoadOrderGroup As String, _
                <Out()> ByVal TagId As IntPtr, _
                <[In]()> ByVal Dependencies As IntPtr, _
                <[In]()> ByVal ServiceStartName As String, _
                <[In]()> ByVal Password As String) As IntPtr
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function OpenSCManager(<[In]()> <[Optional]()> ByVal MachineName As String, _
                <[In]()> <[Optional]()> ByVal DatabaseName As String, _
                <[In]()> ByVal DesiredAccess As Security.ServiceManagerAccess) As IntPtr
        End Function

        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function OpenService(<[In]()> ByVal SCManager As IntPtr, <[In]()> ByVal ServiceName As String, <[In]()> ByVal DesiredAccess As Security.ServiceAccess) As IntPtr
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function StartService(<[In]()> ByVal Service As IntPtr, <[In]()> ByVal NumServiceArgs As Integer, <[In]()> <[Optional]()> ByVal Args As String()) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ' <from PH>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
        Public Shared Function ChangeServiceConfig(<[In]()> ByVal Service As IntPtr, _
                    <[In]()> ByVal ServiceType As ServiceType, _
                    <[In]()> ByVal StartType As ServiceStartType, _
                    <[In]()> ByVal ErrorControl As ServiceErrorControl, _
                    <[In]()> <[Optional]()> ByVal BinaryPath As String, _
                    <[In]()> <[Optional]()> ByVal LoadOrderGroup As String, _
                    <Out()> <[Optional]()> ByVal TagId As IntPtr, _
                    <[In]()> <[Optional]()> ByVal Dependencies As String, _
                    <[In]()> <[Optional]()> ByVal StartName As String, _
                    <[In]()> <[Optional]()> ByVal Password As String, _
                    <[In]()> <[Optional]()> ByVal DisplayName As String) As Boolean
        End Function

        <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function LockServiceDatabase(ByVal hSCManager As IntPtr) As IntPtr
        End Function

        <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function UnlockServiceDatabase(ByVal hSCManager As IntPtr) As Boolean
        End Function

        <DllImport("advapi32.dll", EntryPoint:="QueryServiceConfigW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function QueryServiceConfig(ByVal hService As IntPtr, _
            ByVal pBuffer As IntPtr, _
            ByVal cbBufSize As Integer, _
            ByRef pcbBytesNeeded As Integer) As Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for registry"

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function WaitForSingleObject(ByVal [Object] As IntPtr, _
                                                   ByVal Timeout As Integer) As WaitResult
        End Function

        <DllImport("kernel32.dll", _
         EntryPoint:="CreateEventA")> _
        Public Shared Function CreateEvent( _
            ByVal lpEventAttributes As IntPtr, _
            ByVal bManualReset As Boolean, _
            ByVal bInitialState As Boolean, _
            ByVal lpName As String) As IntPtr
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function RegCloseKey(ByVal hKey As IntPtr) As Integer
        End Function

        <DllImport("advapi32.dll", CharSet:=CharSet.Unicode, EntryPoint:="RegOpenKeyEx")> _
        Public Shared Function RegOpenKeyEx(ByVal hKey As IntPtr, ByVal subKey As String, _
                                            ByVal options As UInteger, ByVal sam As Integer, _
                                            ByRef phkResult As IntPtr) As Integer
        End Function

        <DllImport("advapi32.dll", SetLastError:=True)> _
        Public Shared Function RegNotifyChangeKeyValue(ByVal hKey As IntPtr, _
                                                        ByVal watchSubtree As Boolean, _
                                                        ByVal dwNotifyFilter As Integer, _
                                                        ByVal hEvent As IntPtr, _
                                                        ByVal fAsynchronous As Boolean) As Integer
        End Function

#End Region

        ' OK
#Region "General declarations"

        <DllImport("kernel32.dll")> _
        Public Shared Sub Sleep(ByVal dwMilliseconds As UInteger)
        End Sub

        <DllImport("kernel32.dll")> _
        Public Shared Sub ZeroMemory(ByVal addr As IntPtr, ByVal size As IntPtr)
        End Sub

        <DllImport("kernel32.dll")> _
        Public Shared Function GetTickCount() As Integer
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function CloseHandle(<[In]()> ByVal Handle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> _
        Public Shared Function LoadLibrary(<[In]()> ByVal FileName As String) As IntPtr
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, EntryPoint:="FreeLibrary")> _
        Public Shared Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for graphical functions"

        <DllImport("user32.dll", CallingConvention:=CallingConvention.Cdecl)> _
        Public Shared Function GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function EnableMenuItem(ByVal hMenu As IntPtr, ByVal uIDEnableItem As UInteger, ByVal uEnable As UInteger) As Boolean
        End Function

        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, _
                                              ByVal partList As String) As Integer
        End Function

        <System.Runtime.InteropServices.DllImport("user32.dll")> _
        Public Shared Function DestroyIcon(ByVal Handle As IntPtr) As Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for hooks"

        'Public Delegate Function HookProc(ByVal code As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
        Public Delegate Function HookProcKbd(ByVal code As Integer, ByVal wParam As IntPtr, ByRef lParam As KBDLLHOOKSTRUCT) As Integer
        Public Delegate Function HookProcMouse(ByVal code As Integer, ByVal wParam As IntPtr, ByRef lParam As MOUSEHOOKSTRUCT) As Integer
        Public Delegate Function LowLevelKeyboardProc(ByVal nCode As Integer, ByVal wParam As NativeEnums.WindowMessage, <[In]()> ByVal lParam As KBDLLHOOKSTRUCT) As Integer
        Public Delegate Function LowLevelMouseProc(ByVal code As Integer, ByVal wParam As NativeEnums.WindowMessage, <[In]()> ByVal lParam As MSLLHookStruct) As IntPtr

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowsHookEx(ByVal hook As HookType, ByVal callback As HookProcKbd, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowsHookEx(ByVal hook As HookType, ByVal callback As HookProcMouse, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowsHookEx(ByVal hook As HookType, ByVal callback As LowLevelKeyboardProc, ByVal hMod As IntPtr, ByVal dwThreadId As UInteger) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function SetWindowsHookEx(ByVal code As HookType, ByVal func As LowLevelMouseProc, ByVal hInstance As IntPtr, ByVal threadID As Integer) As IntPtr
        End Function

        <DllImport("user32.dll", SetLastError:=True)> _
        Public Shared Function UnhookWindowsHookEx(ByVal hhk As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer, _
                                        ByVal wParam As IntPtr, _
                                        <[In]()> ByVal lParam As KBDLLHOOKSTRUCT) As Integer
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer, _
                                        ByVal wParam As IntPtr, _
                                        <[In]()> ByVal lParam As MOUSEHOOKSTRUCT) As Integer
        End Function

        <DllImport("user32.dll")> _
        Public Shared Function GetAsyncKeyState(ByVal vKey As Int32) As Short
        End Function

#End Region

        ' OK
#Region "Declarations used for error management"

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function LocalFree(ByVal hMem As IntPtr) As IntPtr
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> _
        Public Shared Function FormatMessage(ByVal Flags As FormatMessageFlags, _
                                ByVal Source As IntPtr, _
                                ByVal MessageId As Integer, _
                                ByVal LanguageId As Integer, _
                                ByRef Buffer As StringBuilder, _
                                ByVal Size As Integer, _
                                ByVal Arguments As IntPtr) As UInteger
        End Function

        <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> _
        Public Shared Function FormatMessage(ByVal Flags As FormatMessageFlags, _
                        ByVal Source As IntPtr, _
                        ByVal MessageId As UInteger, _
                        ByVal LanguageId As Integer, _
                        ByRef Buffer As StringBuilder, _
                        ByVal Size As Integer, _
                        ByVal Arguments As IntPtr) As UInteger
        End Function

#End Region

        ' OK
#Region "Declarations used for handles"

        <DllImport("ntdll.dll")> _
        Public Shared Function NtQueryObject(<[In]()> ByVal Handle As IntPtr, _
                    <[In]()> ByVal ObjectInformationClass As ObjectInformationClass, _
                    <Out()> ByVal ObjectInformation As IntPtr, _
                    <[In]()> ByVal ObjectInformationLength As Integer, _
                    <Out()> ByRef ReturnLength As Integer) As UInteger

        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function NtDuplicateObject(ByVal SourceProcessHandle As IntPtr, _
                                            ByVal SourceHandle As IntPtr, _
                                            ByVal TargetProcessHandle As IntPtr, _
                                            ByRef TargetHandle As IntPtr, _
                                            ByVal DesiredAccess As Integer, _
                                            ByVal Attributes As HandleFlags, _
                                            ByVal Options As DuplicateOptions) As Int32
        End Function

        <DllImport("kernel32.dll", SetLastError:=True)> _
        Public Shared Function DuplicateHandle(ByVal hSourceProcessHandle As IntPtr, _
                                        ByVal hSourceHandle As IntPtr, _
                                        ByVal hTargetProcessHandle As IntPtr, _
                                        ByRef lpTargetHandle As IntPtr, _
                                        ByVal dwDesiredAccess As UInteger, _
                                        ByVal bInheritHandle As Boolean, _
                                        ByVal dwOptions As DuplicateOptions) As Boolean
        End Function

#End Region

        ' OK
#Region "Declarations used for debugging"

        ' From here :
        ' http://www.woodmann.com/forum/blog.php?b=151

        <DllImport("ntdll.dll")> _
        Public Shared Function RtlCreateQueryDebugBuffer(<[In]()> ByVal Size As Integer, _
                            <[In]()> ByVal EventPair As Boolean) As IntPtr
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function RtlDestroyQueryDebugBuffer(<[In]()> ByVal DebugBuffer As IntPtr) As UInteger
        End Function

        <DllImport("ntdll.dll")> _
        Public Shared Function RtlQueryProcessDebugInformation(<[In]()> ByVal ProcessId As IntPtr, _
                                <[In]()> ByVal Flags As NativeEnums.RtlQueryProcessDebugInformationFlags, _
                                <[In]()> ByVal Buffer As IntPtr) As UInteger
        End Function

#End Region

    End Class

End Namespace
