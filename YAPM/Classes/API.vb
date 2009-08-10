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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declarations of Enums and Structures (converted from C# to VB.Net)
'   * xxx
'   * yyy

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Drawing
Imports System.Net

Public Class API

#Region "Declarations used for processes"

    Public Const GR_USEROBJECTS As Integer = 1
    Public Const GR_GDIOBJECTS As Integer = 0

    Public Const DUPLICATE_SAME_ACCESS As Integer = &H2

    Public Const STILL_ACTIVE As Integer = 259

    Public Const PROCESS_SET_INFORMATION As Integer = &H200
    Public Const PROCESS_SUSPEND_RESUME As Integer = &H800
    Public Const PROCESS_QUERY_INFORMATION As Integer = &H400
    Public Const PROCESS_TERMINATE As Integer = &H1
    Public Const PROCESS_SET_QUOTA As Integer = &H100
    Public Const PROCESS_CREATE_THREAD As Integer = &H2
    Public Const PROCESS_VM_OPERATION As Integer = &H8
    Public Const PROCESS_VM_READ As Integer = &H10
    Public Const PROCESS_VM_WRITE As Integer = &H20

    Public Enum MINIDUMPTYPE As Integer
        MiniDumpNormal = &H0
        MiniDumpWithDataSegs = &H1
        MiniDumpWithFullMemory = &H2
        MiniDumpWithHandleData = &H4
        MiniDumpFilterMemory = &H8
        MiniDumpScanMemory = &H10
        MiniDumpWithUnloadedModules = &H20
        MiniDumpWithIndirectlyReferencedMemory = &H40
        MiniDumpFilterModulePaths = &H80
        MiniDumpWithProcessThreadData = &H100
        MiniDumpWithPrivateReadWriteMemory = &H200
        MiniDumpWithoutOptionalData = &H400
        MiniDumpWithFullMemoryInfo = &H800
        MiniDumpWithThreadInfo = &H1000
        MiniDumpWithCodeSegs = &H2000
    End Enum

    ' LdrpDataTableEntryFlags comes from Process Hacker by wj32 (under GNU GPL 3.0)
    <Flags()> _
    Public Enum LdrpDataTableEntryFlags As UInteger
        StaticLink = &H2
        ImageDll = &H4
        Flag0x8 = &H8
        Flag0x10 = &H10
        LoadInProgress = &H1000
        UnloadInProgress = &H2000
        EntryProcessed = &H4000
        EntryInserted = &H8000
        CurrentLoad = &H10000
        FailedBuiltInLoad = &H20000
        DontCallForThreads = &H40000
        ProcessAttachCalled = &H80000
        DebugSymbolsLoaded = &H100000
        ImageNotAtBase = &H200000
        CorImage = &H400000
        CorOwnsUnmap = &H800000
        SystemMapped = &H1000000
        ImageVerifying = &H2000000
        DriverDependentDll = &H4000000
        EntryNative = &H8000000
        Redirected = &H10000000
        NonPagedDebugInfo = &H20000000
        MmLoaded = &H40000000
        CompatDatabaseProcessed = &H80000000
    End Enum

    Public Enum PROCESS_INFORMATION_CLASS As Integer
        ProcessBasicInformation
        ProcessQuotaLimits
        ProcessIoCounters
        ProcessVmCounters
        ProcessTimes
        ProcessBasePriority
        ProcessRaisePriority
        ProcessDebugPort
        ProcessExceptionPort
        ProcessAccessToken
        ProcessLdtInformation
        ProcessLdtSize
        ProcessDefaultHardErrorMode
        ProcessIoPortHandlers
        ProcessPooledUsageAndLimits
        ProcessWorkingSetWatch
        ProcessUserModeIOPL
        ProcessEnableAlignmentFaultFixup
        ProcessPriorityClass
        ProcessWx86Information
        ProcessHandleCount
        ProcessAffinityMask
        ProcessPriorityBoost
        ProcessDeviceMap
        ProcessSessionInformation
        ProcessForegroundInformation
        ProcessWow64Information
        ProcessImageFileName
        ProcessLUIDDeviceMapsEnabled
        ProcessBreakOnTermination
        ProcessDebugObjectHandle
        ProcessDebugFlags
        ProcessHandleTracing
        ProcessIoPriority
        ProcessExecuteFlags
        ProcessResourceManagement
        ProcessCookie
        ProcessImageInformation
        ProcessCycleTime
        ProcessPagePriority
        ProcessInstrumentationCallback
        ProcessThreadStackAllocation
        ProcessWorkingSetWatchEx
        ProcessImageFileNameWin32
        ProcessImageFileMapping
        ProcessAffinityUpdateMode
        ProcessMemoryAllocationMode
        MaxProcessInfoClass
    End Enum

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationProcess(ByVal ProcessHandle As Integer, ByVal ProcessInformationClass As PROCESS_INFORMATION_CLASS, ByVal ProcessInformation As IntPtr, ByVal ProcessInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationProcess(ByVal ProcessHandle As Integer, ByVal ProcessInformationClass As PROCESS_INFORMATION_CLASS, ByRef ProcessInformation As PROCESS_BASIC_INFORMATION, ByVal ProcessInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQuerySystemInformation(ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByVal SystemInformation As IntPtr, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As UInteger
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function SetProcessWorkingSetSize(ByVal hwProc As Integer, ByVal minimumSize As Integer, ByVal maximumSize As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function GetProcessId(ByVal ProcessHandle As IntPtr) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function IsProcessInJob(ByVal ProcessHandle As IntPtr, ByVal JobHandle As IntPtr, ByRef Result As Boolean) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function CheckRemoteDebuggerPresent(ByVal ProcessHandle As IntPtr, ByRef DebuggerPresent As Boolean) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SYSTEM_PROCESS_INFORMATION
        Public NextEntryOffset As Integer
        Public NumberOfThreads As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
        Public Reserved1 As Long()
        Public CreateTime As Long
        Public UserTime As Long
        Public KernelTime As Long
        Public ImageName As UNICODE_STRING
        Public BasePriority As Integer
        Public ProcessId As Integer
        Public InheritedFromProcessId As Integer
        Public HandleCount As Integer
        Public SessionId As Integer
        Public PageDirectoryBase As Integer
        Public VirtualMemoryCounters As VM_COUNTERS_EX
        Public IoCounters As IO_COUNTERS
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure PEB_LDR_DATA
        Public Length As Integer
        <MarshalAs(UnmanagedType.I1)> _
        Public Initialized As Boolean
        Public SsHandle As IntPtr
        Public InLoadOrderModuleList As ListEntry
        Public InMemoryOrderModuleList As ListEntry
        Public InInitializationOrderModuleList As ListEntry
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure LDR_DATA_TABLE_ENTRY
        Public InLoadOrderLinks As ListEntry
        Public InMemoryOrderLinks As ListEntry
        Public InInitializationOrderLinks As ListEntry
        Public DllBase As IntPtr
        Public EntryPoint As IntPtr
        Public SizeOfImage As Integer
        Public FullDllName As UNICODE_STRING
        Public BaseDllName As UNICODE_STRING
        Public Flags As LdrpDataTableEntryFlags
        Public LoadCount As Short
        Public TlsIndex As Short
        Public HashTableEntry As ListEntry
        Public TimeDateStamp As Integer
        Public EntryPointActivationContext As IntPtr
        Public PatchInformation As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ListEntry
        Public Flink As IntPtr
        Public Blink As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure PEB
        <MarshalAs(UnmanagedType.I1)> _
        Public InheritedAddressSpace As Boolean
        <MarshalAs(UnmanagedType.I1)> _
        Public ReadImageFileExecOptions As Boolean
        <MarshalAs(UnmanagedType.I1)> _
        Public BeingDebugged As Boolean
        <MarshalAs(UnmanagedType.I1)> _
        Public Spare As Boolean
        Public Mutant As IntPtr
        Public ImageBaseAddress As IntPtr
        Public LoaderData As IntPtr
        Public ProcessParameters As IntPtr
        Public SubSystemData As IntPtr
        Public ProcessHeap As IntPtr
        Public FastPebLock As IntPtr
        Public FastPebLockRoutine As IntPtr
        Public FastPebUnlockRoutine As IntPtr
        Public EnvironmentUpdateCount As Integer
        Public KernelCallbackTable As IntPtr
        Public EventLogSection As Integer
        Public EventLog As Integer
        Public FreeList As IntPtr
        Public TlsExpansionCounter As Integer
        Public TlsBitmap As IntPtr
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)> _
        Public TlsBitmapBits As Integer()
        Public ReadOnlySharedMemoryBase As IntPtr
        Public ReadOnlySharedMemoryHeap As IntPtr
        Public ReadOnlyStaticServerData As IntPtr
        Public AnsiCodePageData As IntPtr
        Public OemCodePageData As IntPtr
        Public UnicodeCaseTableData As IntPtr
        Public NumberOfProcessors As Integer
        Public NtGlobalFlag As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)> _
        Public Spare2 As Byte()
        Public CriticalSectionTimeout As Long
        Public HeapSegmentReserve As IntPtr
        Public HeapSegmentCommit As IntPtr
        Public HeapDeCommitTotalFreeThreshold As IntPtr
        Public HeapDeCommitFreeBlockThreshold As IntPtr
        Public NumberOfHeaps As Integer
        Public MaximumNumberOfHeaps As Integer
        Public ProcessHeaps As IntPtr
        Public GdiSharedHandleTable As IntPtr
        Public ProcessStarterHelper As IntPtr
        Public GdiDCAttributeList As Integer
        Public LoaderLock As IntPtr
        Public OSMajorVersion As Integer
        Public OSMinorVersion As Integer
        Public OSBuildNumber As Short
        Public OSPlatformId As Short
        Public ImageSubSystem As Integer
        Public ImageSubsystemMajorVersion As Integer
        Public ImageSubsystemMinorVersion As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=22)> _
        Public GdiHandleBuffer As Integer()
        Public PostProcessInitRoutine As IntPtr
        Public TlsExpansionBitmap As IntPtr
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=80)> _
        Public TlsExpansionBitmapBits() As Byte
        Public SessionId As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure PROCESS_BASIC_INFORMATION
        Public ExitStatus As Integer
        Public PebBaseAddress As Integer
        Public AffinityMask As Integer
        Public BasePriority As Integer
        Public UniqueProcessId As Integer
        Public InheritedFromUniqueProcessId As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential), Serializable()> _
    Public Structure IO_COUNTERS
        Public ReadOperationCount As ULong
        Public WriteOperationCount As ULong
        Public OtherOperationCount As ULong
        Public ReadTransferCount As ULong
        Public WriteTransferCount As ULong
        Public OtherTransferCount As ULong
        Public Shared Operator <>(ByVal i1 As IO_COUNTERS, ByVal i2 As IO_COUNTERS) As Boolean
            Return Not (i1 = i2)
        End Operator
        Public Shared Operator =(ByVal i1 As IO_COUNTERS, ByVal i2 As IO_COUNTERS) As Boolean
            Return (i1.ReadOperationCount = i2.ReadOperationCount AndAlso _
                i1.WriteOperationCount = i2.WriteOperationCount AndAlso _
                i1.OtherOperationCount = i2.OtherOperationCount AndAlso _
                i1.ReadTransferCount = i2.ReadTransferCount AndAlso _
                i1.WriteTransferCount = i2.WriteTransferCount AndAlso _
                i1.OtherTransferCount = i2.OtherTransferCount)
        End Operator
    End Structure

    <StructLayout(LayoutKind.Sequential), Serializable()> _
    Public Structure VM_COUNTERS_EX
        Public PeakVirtualSize As Integer
        Public VirtualSize As Integer
        Public PageFaultCount As Integer
        Public PeakWorkingSetSize As Integer
        Public WorkingSetSize As Integer
        Public QuotaPeakPagedPoolUsage As Integer
        Public QuotaPagedPoolUsage As Integer
        Public QuotaPeakNonPagedPoolUsage As Integer
        Public QuotaNonPagedPoolUsage As Integer
        Public PagefileUsage As Integer
        Public PeakPagefileUsage As Integer
        Public PrivateBytes As Integer
        Public Shared Operator <>(ByVal m1 As VM_COUNTERS_EX, ByVal m2 As VM_COUNTERS_EX) As Boolean
            Return Not (m1 = m2)
        End Operator
        Public Shared Operator =(ByVal i1 As VM_COUNTERS_EX, ByVal i2 As VM_COUNTERS_EX) As Boolean
            Return (i1.PeakVirtualSize = i2.PeakVirtualSize AndAlso _
                i1.VirtualSize = i2.VirtualSize AndAlso _
                i1.PageFaultCount = i2.PageFaultCount AndAlso _
                i1.PeakWorkingSetSize = i2.PeakWorkingSetSize AndAlso _
                i1.WorkingSetSize = i2.WorkingSetSize AndAlso _
                i1.QuotaPeakPagedPoolUsage = i2.QuotaPeakPagedPoolUsage AndAlso _
                i1.QuotaPagedPoolUsage = i2.QuotaPagedPoolUsage AndAlso _
                i1.QuotaPeakNonPagedPoolUsage = i2.QuotaPeakNonPagedPoolUsage AndAlso _
                i1.QuotaNonPagedPoolUsage = i2.QuotaNonPagedPoolUsage AndAlso _
                i1.PagefileUsage = i2.PagefileUsage AndAlso _
                i1.PeakPagefileUsage = i2.PeakPagefileUsage AndAlso _
                i1.PrivateBytes = i2.PrivateBytes)
        End Operator
    End Structure

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function GetCurrentThreadId() As Integer
    End Function

    <DllImport("dbghelp.dll")> _
    Public Shared Function MiniDumpWriteDump(ByVal hProcess As IntPtr, ByVal ProcessId As Integer, ByVal hFile As IntPtr, ByVal DumpType As Integer, ByVal ExceptionParam As IntPtr, ByVal UserStreamParam As IntPtr, _
        ByVal CallackParam As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function OpenProcess(ByVal DesiredAccess As PROCESS_RIGHTS, _
                                       ByVal InheritHandle As Boolean, _
                                       ByVal ProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Sub ExitProcess(ByVal ExitCode As Integer)
    End Sub

    Public Declare Function GetModuleFileNameExA Lib "PSAPI.DLL" (ByVal hProcess As IntPtr, ByVal hModule As Integer, ByVal ModuleName As String, ByVal nSize As Integer) As Integer
    Public Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As IntPtr, ByVal uExitCode As Integer) As Integer
    Public Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As IntPtr, ByVal DesiredAccess As Integer, ByRef TokenHandle As IntPtr) As Integer
    Public Declare Function NtSuspendProcess Lib "Ntdll.dll" (ByVal hProc As IntPtr) As Integer
    Public Declare Function NtResumeProcess Lib "Ntdll.dll" (ByVal hProc As IntPtr) As Integer
    Public Declare Function SetPriorityClass Lib "kernel32" (ByVal hProcess As IntPtr, ByVal dwPriorityClass As Integer) As Integer
    Public Declare Function SetProcessAffinityMask Lib "kernel32" (ByVal hProcess As IntPtr, ByVal dwProcessAffinityMask As Integer) As Integer
    Public Declare Function GetGuiResources Lib "user32.dll" (ByVal hProcess As IntPtr, ByVal uiFlags As Integer) As Integer
    Public Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As IntPtr, ByVal lpProcName As String) As Integer
    Public Declare Function GetCurrentProcess Lib "kernel32.dll" () As Integer
    Public Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As IntPtr, ByRef lpExitCode As Integer) As Integer
    Public Declare Function GetCurrentProcessId Lib "kernel32.dll" () As Integer

#End Region

    ' OK
#Region "Declarations used for rights"

    Public Enum THREAD_RIGHTS As UInteger
        THREAD_TERMINATE = &H1
        THREAD_SUSPEND_RESUME = &H2
        THREAD_GET_CONTEXT = &H8
        THREAD_SET_CONTEXT = &H10
        THREAD_QUERY_INFORMATION = &H40
        THREAD_SET_INFORMATION = &H20
        THREAD_SET_THREAD_TOKEN = &H80
        THREAD_IMPERSONATE = &H100
        THREAD_DIRECT_IMPERSONATION = &H200
        THREAD_SET_LIMITED_INFORMATION = &H400
        THREAD_QUERY_LIMITED_INFORMATION = &H800
        THREAD_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED Or _
                            STANDARD_RIGHTS.SYNCHRONIZE Or &HFFFF
    End Enum

    Public Enum TOKEN_RIGHTS As Integer
        AssignPrimary = &H1
        Duplicate = &H2
        Impersonate = &H4
        Query = &H8
        QuerySource = &H10
        AdjustPrivileges = &H20
        AdjustGroups = &H40
        AdjustDefault = &H80
        AdjustSessionId = &H100
        Read = &H20008
        Write = &H200E0
        Execute = &H20000
        All = &HF00FF
        AllPlusSessionId = &HF01FF
        MaximumAllowed = &H2000000
        AccessSystemSecurity = &H1000000
    End Enum

    Public Enum STANDARD_RIGHTS As UInteger
        WRITE_OWNER = &H80000
        WRITE_DAC = &H40000
        READ_CONTROL = &H20000
        DELETE = &H10000
        SYNCHRONIZE = &H100000
        STANDARD_RIGHTS_REQUIRED = &HF0000
        STANDARD_RIGHTS_WRITE = READ_CONTROL
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL
        STANDARD_RIGHTS_READ = READ_CONTROL
        STANDARD_RIGHTS_ALL = &H1F0000
        SPECIFIC_RIGHTS_ALL = &HFFFF
        ACCESS_SYSTEM_SECURITY = &H1000000
        MAXIMUM_ALLOWED = &H2000000
        GENERIC_WRITE = &H40000000
        GENERIC_EXECUTE = &H20000000
        GENERIC_READ = &H80000000
        GENERIC_ALL = &H10000000
    End Enum

    Public Enum PROCESS_RIGHTS As UInteger
        PROCESS_TERMINATE = &H1
        PROCESS_CREATE_THREAD = &H2
        PROCESS_SET_SESSIONID = &H4
        PROCESS_VM_OPERATION = &H8
        PROCESS_VM_READ = &H10
        PROCESS_VM_WRITE = &H20
        PROCESS_DUP_HANDLE = &H40
        PROCESS_CREATE_PROCESS = &H80
        PROCESS_SET_QUOTA = &H100
        PROCESS_SET_INFORMATION = &H200
        PROCESS_QUERY_INFORMATION = &H400
        PROCESS_SUSPEND_RESUME = &H800
        PROCESS_QUERY_LIMITED_INFORMATION = &H1000
        PROCESS_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED Or _
                                STANDARD_RIGHTS.SYNCHRONIZE Or &HFFFF
    End Enum

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

    '<StructLayout(LayoutKind.Sequential)> _
    'Public Structure MODULEINFO
    '    Public BaseOfDll As IntPtr
    '    Public SizeOfImage As Integer
    '    Public EntryPoint As IntPtr
    'End Structure

#End Region

    ' OK
#Region "Declarations used for memory management"

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function VirtualQueryEx(ByVal Process As IntPtr, _
                ByVal Address As IntPtr, _
                <MarshalAs(UnmanagedType.Struct)> ByRef Buffer As MEMORY_BASIC_INFORMATION, _
                ByVal Size As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function VirtualFreeEx(ByVal Process As IntPtr, _
                                         ByVal Address As IntPtr, _
                                         ByVal Size As Integer, _
                                         ByVal FreeType As MEMORY_STATE) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function VirtualProtectEx(ByVal Process As IntPtr, _
                                         ByVal Address As IntPtr, _
                                         ByVal Size As Integer, _
                                         ByVal NewProtect As PROTECTION_TYPE, _
                                         ByRef OldProtect As PROTECTION_TYPE) As Boolean
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

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MEMORY_BASIC_INFORMATION
        Public BaseAddress As IntPtr
        Public AllocationBase As IntPtr
        Public AllocationProtect As PROTECTION_TYPE
        Public RegionSize As Integer
        Public State As MEMORY_STATE
        Public Protect As PROTECTION_TYPE
        Public Type As MEMORY_TYPE
    End Structure

    <Flags()> _
    Public Enum PROTECTION_TYPE As UInteger
        AccessDenied = 0
        Execute = &H10
        ExecuteRead = &H20
        ExecuteReadWrite = &H40
        ExecuteWriteCopy = &H80
        NoAccess = &H1
        [ReadOnly] = &H2
        ReadWrite = &H4
        WriteCopy = &H8
        Guard = &H100
        NoCache = &H200
        WriteCombine = &H400
    End Enum

    <Flags()> _
    Public Enum MEMORY_STATE As UInteger
        Free = &H10000
        Commit = &H1000
        Reserve = &H2000
        Decommit = &H4000
        Release = &H8000
        Reset = &H80000
        TopDown = &H100000
        Physical = &H400000
        LargePages = &H20000000
    End Enum

    Public Enum MEMORY_TYPE As Integer
        Image = &H1000000
        [Private] = &H20000
        Mapped = &H40000
    End Enum

#End Region

    ' OK
#Region "Declarations used for threads"

    Public Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As IntPtr, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure THREAD_BASIC_INFORMATION
        Public ExitStatus As UInteger
        Public TebBaseAddress As IntPtr
        Public ClientId As CLIENT_ID
        Public AffinityMask As IntPtr
        Public Priority As Integer
        Public BasePriority As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SYSTEM_THREAD_INFORMATION
        Public KernelTime As Long
        Public UserTime As Long
        Public CreateTime As Long
        Public WaitTime As Integer
        Public StartAddress As IntPtr
        Public ClientId As CLIENT_ID
        Public Priority As Integer
        Public BasePriority As Integer
        Public ContextSwitchCount As Integer
        Public State As Integer
        Public WaitReason As KWAIT_REASON
    End Structure

    Public Enum KWAIT_REASON As Integer
        Executive = 0
        FreePage = 1
        PageIn = 2
        PoolAllocation = 3
        DelayExecution = 4
        Suspended = 5
        UserRequest = 6
        WrExecutive = 7
        WrFreePage = 8
        WrPageIn = 9
        WrPoolAllocation = 10
        WrDelayExecution = 11
        WrSuspended = 12
        WrUserRequest = 13
        WrEventPair = 14
        WrQueue = 15
        WrLpcReceive = 16
        WrLpcReply = 17
        WrVirtualMemory = 18
        WrPageOut = 19
        WrRendezvous = 20
        Spare2 = 21
        Spare3 = 22
        Spare4 = 23
        Spare5 = 24
        WrCalloutStack = 25
        WrKernel = 26
        WrResource = 27
        WrPushLock = 28
        WrMutex = 29
        WrQuantumEnd = 30
        WrDispatchInt = 31
        WrPreempted = 32
        WrYieldExecution = 33
        WrFastMutex = 34
        WrGuardedMutex = 35
        WrRundown = 36
        MaximumWaitReason = 37
    End Enum

    Public Enum THREAD_INFORMATION_CLASS
        ThreadBasicInformation
        ThreadTimes
        ThreadPriority
        ThreadBasePriority
        ThreadAffinityMask
        ThreadImpersonationToken
        ThreadDescriptorTableEntry
        ThreadEnableAlignmentFaultFixup
        ThreadEventPair
        ThreadQuerySetWin32StartAddress
        ThreadZeroTlsCell
        ThreadPerformanceCount
        ThreadAmILastThread
        ThreadIdealProcessor
        ThreadPriorityBoost
        ThreadSetTlsArrayAddress
        ThreadIsIoPending
        ThreadHideFromDebugger
        ThreadBreakOnTermination
        ThreadSwitchLegacyState
        ThreadIsTerminated
        ThreadLastSystemCall
        ThreadIoPriority
        ThreadCycleTime
        ThreadPagePriority
        ThreadActualBasePriority
        ThreadTebInformation
        ThreadCSwitchMon
        MaxThreadInfoClass
    End Enum

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function OpenThread(<[In]()> ByVal DesiredAccess As THREAD_RIGHTS, _
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

    <DllImport("ntdll.dll")> _
    Public Shared Function NtQueryInformationThread(<[In]()> ByVal ThreadHandle As IntPtr, _
                <[In]()> ByVal ThreadInformationClass As THREAD_INFORMATION_CLASS, _
                ByRef ThreadInformation As THREAD_BASIC_INFORMATION, _
                <[In]()> ByVal ThreadInformationLength As Integer, _
                <Out()> <[Optional]()> ByRef ReturnLength As Integer) As UInteger
    End Function

#End Region

#Region "Declarations used for tokens & privileges"

    Public Const SE_PRIVILEGE_ENABLED As Integer = &H2
    Public Const SE_PRIVILEGE_ENABLED_BY_DEFAULT As Integer = &H1
    Public Const SE_PRIVILEGE_DISBALED As Integer = &H0
    Public Const SE_PRIVILEGE_REMOVED As Integer = &H4

    Public Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        '<VBFixedArray(25)> _
        Dim Privileges() As LUID_AND_ATTRIBUTES
    End Structure

    Public Enum PRIVILEGE_STATUS
        PRIVILEGE_ENABLED = &H2
        PRIVILEGE_DISBALED = &H0
        PRIVILEGE_REMOVED = &H4
    End Enum

    Public Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As IntPtr, ByVal TokenInformationClass As Integer, ByVal TokenInformation As Integer, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    Public Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer           'Returns a valid LUID which is important when making security changes in NT.
    Public Declare Function LookupPrivilegeNameA Lib "advapi32.dll" (ByVal lpSystemName As String, ByRef lpLuid As LUID, ByVal lpName As String, ByRef cchName As Integer) As Integer                'Used to adjust your program's security privileges, can't restore without it!

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function LookupPrivilegeDisplayName( _
        ByVal SystemName As Integer, ByVal Name As String, ByVal DisplayName As String, _
        ByRef DisplayNameSize As Integer, ByRef LanguageId As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function GetTokenInformation(ByVal TokenHandle As IntPtr, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, ByVal TokenInformation As IntPtr, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function LookupAccountSid(ByVal SystemName As String, ByVal SID As Integer, ByVal Name As StringBuilder, ByRef NameSize As Integer, ByVal ReferencedDomainName As StringBuilder, ByRef ReferencedDomainNameSize As Integer, ByRef Use As SID_NAME_USE) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function GetTokenInformation(ByVal TokenHandle As IntPtr, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, ByRef TokenInformation As Integer, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    End Function

    Public Structure TOKEN_PRIVILEGES2
        Dim PrivilegeCount As Integer
        Dim Privileges As LUID_AND_ATTRIBUTES
    End Structure

    Public Structure LUID
        Dim lowpart As Integer
        Dim highpart As Integer
    End Structure

    Public Structure LUID_AND_ATTRIBUTES
        Dim pLuid As LUID
        Dim Attributes As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure CLIENT_ID
        Public UniqueProcess As IntPtr
        Public UniqueThread As IntPtr
        Public Sub New(ByVal processId As IntPtr, ByVal threadId As IntPtr)
            UniqueProcess = processId
            UniqueThread = threadId
        End Sub
        Public Sub New(ByVal processId As Integer, ByVal threadId As Integer)
            UniqueProcess = New IntPtr(processId)
            UniqueThread = New IntPtr(threadId)
        End Sub
    End Structure

    Public Structure TOKEN_USER
        Dim User As SID_AND_ATTRIBUTES
    End Structure

    Public Structure SID_AND_ATTRIBUTES
        Dim Sid As Integer
        Dim Attributes As Integer
    End Structure

    Public Structure PrivilegeInfo
        Dim Name As String
        Dim Status As Integer
        Dim pLuid As LUID
    End Structure

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function AdjustTokenPrivileges( _
        ByVal TokenHandle As Integer, _
        ByVal DisableAllPrivileges As Integer, _
        ByRef NewState As TOKEN_PRIVILEGES, _
        ByVal BufferLength As Integer, _
        ByRef PreviousState As TOKEN_PRIVILEGES, _
        ByRef ReturnLength As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function AdjustTokenPrivileges( _
        ByVal TokenHandle As Integer, _
        ByVal DisableAllPrivileges As Integer, _
        ByRef NewState As TOKEN_PRIVILEGES2, _
        ByVal BufferLength As Integer, _
        ByRef PreviousState As TOKEN_PRIVILEGES2, _
        ByRef ReturnLength As Integer) As Boolean
    End Function

    Public Enum TOKEN_INFORMATION_CLASS
        TokenUser = 1
        TokenGroups
        TokenPrivileges
        TokenOwner
        TokenPrimaryGroup
        TokenDefaultDacl
        TokenSource
        TokenType
        TokenImpersonationLevel
        TokenStatistics
        TokenRestrictedSids
        TokenSessionId
        TokenGroupsAndPrivileges
        TokenSessionReference
        TokenSandBoxInert
        TokenAuditPolicy
        TokenOrigin
        TokenElevationType
        TokenLinkedToken
        TokenElevation
        TokenHasRestrictions
        TokenAccessInformation
        TokenVirtualizationAllowed
        TokenVirtualizationEnabled
        TokenIntegrityLevel
        TokenUIAccess
        TokenMandatoryPolicy
        TokenLogonSid
        MaxTokenInfoClass
    End Enum

    Public Enum SID_NAME_USE As Integer
        SidTypeUser = 1
        SidTypeGroup
        SidTypeDomain
        SidTypeAlias
        SidTypeWellKnownGroup
        SidTypeDeletedAccount
        SidTypeInvalid
        SidTypeUnknown
        SidTypeComputer
        SidTypeLabel
    End Enum

    Public Enum ElevationType
        [Default] = 1
        Full = 2
        Limited = 3
    End Enum

#End Region

    ' OK
#Region "Declarations used for network"

    Public Enum NetworkProtocol As Integer
        Tcp
        Udp
    End Enum

    Public Enum TCP_TABLE_CLASS As Integer
        TCP_TABLE_BASIC_LISTENER
        TCP_TABLE_BASIC_CONNECTIONS
        TCP_TABLE_BASIC_ALL
        TCP_TABLE_OWNER_PID_LISTENER
        TCP_TABLE_OWNER_PID_CONNECTIONS
        TCP_TABLE_OWNER_PID_ALL
        TCP_TABLE_OWNER_MODULE_LISTENER
        TCP_TABLE_OWNER_MODULE_CONNECTIONS
        TCP_TABLE_OWNER_MODULE_ALL
    End Enum

    Public Enum UDP_TABLE_CLASS As Integer
        UDP_TABLE_BASIC
        UDP_TABLE_OWNER_PID
        UDP_TABLE_OWNER_MODULE
    End Enum

    Public Structure MIB_TCPROW_OWNER_PID
        Dim dwState As Integer
        Dim dwLocalAddr As Integer
        Dim dwLocalPort As Integer
        Dim dwRemoteAddr As Integer
        Dim dwRemotePort As Integer
        Dim dwOwningPid As Integer
    End Structure

    Public Structure MIB_UDPROW_OWNER_PID
        'Dim dwState As Integer
        Dim dwLocalAddr As Integer
        Dim dwLocalPort As Integer
        'Dim dwRemoteAddr As Integer
        'Dim dwRemotePort As Integer
        Dim dwOwningPid As Integer
    End Structure

    Public Structure LightConnection
        Dim dwState As Integer
        Dim local As IPEndPoint
        Dim remote As IPEndPoint
        Dim dwOwningPid As Integer
        Dim dwType As NetworkProtocol
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MibTcpRow
        Public State As MibTcpState
        Public LocalAddress As UInteger
        Public LocalPort As Integer
        Public RemoteAddress As UInteger
        Public RemotePort As Integer
    End Structure

    Public Enum MibTcpState As Integer
        Closed = 1
        Listening
        SynSent
        SynReceived
        Established
        FinWait1
        FinWait2
        CloseWait
        Closing
        LastAck
        TimeWait
        DeleteTcb
    End Enum

    Public Enum MIB_TCP_STATE As Integer
        Closed = 1
        Listening
        SynSent
        SynReceived
        Established
        FinWait1
        FinWait2
        CloseWait
        Closing
        LastAck
        TimeWait
        DeleteTcb
    End Enum

    <DllImport("iphlpapi.dll", SetLastError:=True)> _
    Public Shared Function GetExtendedTcpTable(ByVal Table As IntPtr, _
                                            ByRef Size As Integer, _
                                            ByVal Order As Boolean, _
                                            ByVal IpVersion As Integer, _
                                            ByVal TableClass As TCP_TABLE_CLASS, _
                                            ByVal Reserved As Integer) As Integer
    End Function

    <DllImport("iphlpapi.dll", SetLastError:=True)> _
    Public Shared Function GetExtendedUdpTable(ByVal Table As IntPtr, _
                                            ByRef Size As Integer, _
                                            ByVal Order As Boolean, _
                                            ByVal IpVersion As Integer, _
                                            ByVal TableClass As UDP_TABLE_CLASS, _
                                            ByVal Reserved As Integer) As Integer
    End Function

    <DllImport("iphlpapi.dll", SetLastError:=True)> _
    Public Shared Function SetTcpEntry(ByRef TcpRow As MibTcpRow) As Integer
    End Function

#End Region

#Region "Declarations used for files"

    Public Const FILE_MAP_READ As Integer = SECTION_MAP_READ
    Public Const FILE_MAP_WRITE As Integer = SECTION_MAP_WRITE
    Public Const PAGE_READWRITE As Integer = &H4
    Public Const SECTION_MAP_READ As Integer = &H4
    Public Const SECTION_MAP_WRITE As Integer = &H2

    Public Structure _FILETIME
        Dim dwLowDateTime As Integer
        Dim dwHighDateTime As Integer
    End Structure

    <StructLayout(LayoutKind.Explicit, CharSet:=CharSet.Ansi)> _
    Public Structure SHFILEOPSTRUCT
        <FieldOffset(0)> Public hWnd As Integer
        <FieldOffset(4)> Public wFunc As Integer
        <FieldOffset(8)> Public pFrom As String
        <FieldOffset(12)> Public pTo As String
        <FieldOffset(16)> Public fFlags As Short
        <FieldOffset(18)> Public fAnyOperationsAborted As Boolean
        <FieldOffset(20)> Public hNameMappings As Object
        <FieldOffset(24)> Public lpszProgressTitle As String
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SYSTEMTIME
        <MarshalAs(UnmanagedType.U2)> Public Year As Short
        <MarshalAs(UnmanagedType.U2)> Public Month As Short
        <MarshalAs(UnmanagedType.U2)> Public DayOfWeek As Short
        <MarshalAs(UnmanagedType.U2)> Public Day As Short
        <MarshalAs(UnmanagedType.U2)> Public Hour As Short
        <MarshalAs(UnmanagedType.U2)> Public Minute As Short
        <MarshalAs(UnmanagedType.U2)> Public Second As Short
        <MarshalAs(UnmanagedType.U2)> Public Milliseconds As Short
    End Structure

    Public Structure BY_HANDLE_FILE_INFORMATION
        Dim dwFileAttributes As Long
        Dim ftCreationTime As _FILETIME
        Dim ftLastAccessTime As _FILETIME
        Dim ftLastWriteTime As _FILETIME
        Dim dwVolumeSerialNumber As Integer
        Dim nFileSizeHigh As Integer
        Dim nFileSizeLow As Integer
        Dim nNumberOfLinks As Integer
        Dim nFileIndexHigh As Integer
        Dim nFileIndexLow As Integer
    End Structure

    Public Structure SHELLEXECUTEINFO
        Public cbSize As Integer
        Public fMask As Integer
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> Public lpVerb As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpFile As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpParameters As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpDirectory As String
        Dim nShow As API.SHOW_FINDOW_TYPE
        Dim hInstApp As IntPtr
        Dim lpIDList As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As Integer
        Public hIcon As IntPtr
        Public hProcess As IntPtr
    End Structure

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function CreateFileMapping(ByVal hFile As IntPtr, ByVal lpFileMappingAttributes As IntPtr, ByVal flProtect As FileMapProtection, ByVal dwMaximumSizeHigh As UInteger, ByVal dwMaximumSizeLow As UInteger, <MarshalAs(UnmanagedType.LPTStr)> ByVal lpName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function OpenFileMapping(ByVal dwDesiredAccess As UInteger, ByVal bInheritHandle As Boolean, ByVal lpName As String) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function UnmapViewOfFile(ByVal lpBaseAddress As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function MapViewOfFile(ByVal hFileMappingObject As IntPtr, ByVal dwDesiredAccess As FileMapAccess, ByVal dwFileOffsetHigh As UInteger, ByVal dwFileOffsetLow As UInteger, ByVal dwNumberOfBytesToMap As UInteger) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function QueryDosDevice(ByVal DeviceName As String, ByVal TargetPath As StringBuilder, ByVal MaxLength As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetShortPathName(ByVal longPath As String, _
          <MarshalAs(UnmanagedType.LPTStr)> ByVal ShortPath As System.Text.StringBuilder, _
          <MarshalAs(Runtime.InteropServices.UnmanagedType.U4)> ByVal bufferSize As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function CreateFile(ByVal lpFileName As String, ByVal dwDesiredAccess As EFileAccess, ByVal dwShareMode As EFileShare, ByVal lpSecurityAttributes As IntPtr, ByVal dwCreationDisposition As ECreationDisposition, ByVal dwFlagsAndAttributes As EFileAttributes, ByVal hTemplateFile As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function GetFileSizeEx(<[In]()> ByVal hFile As IntPtr, <[In](), Out()> ByRef lpFileSize As Long) As Boolean
    End Function

    Public Declare Unicode Function SHRunDialog Lib "shell32" Alias "#61" (ByVal hwnd As Integer, ByVal dummy1 As Integer, ByVal dummy2 As Integer, ByVal Title As String, ByVal Prompt As String, ByVal Flags As Integer) As Integer

    Public Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" _
        (ByVal Buffer As String, ByVal Size As Integer) As Integer

    <DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Shared Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
    End Function

    <DllImport("User32", SetLastError:=True)> _
    Public Shared Function LoadString(ByVal hInstance As IntPtr, ByVal uID As UInt32, ByVal lpBuffer As System.Text.StringBuilder, ByVal nBufferMax As Integer) As Integer
    End Function

    <DllImport("shell32.dll")> _
    Public Shared Function FindExecutable(ByVal lpFile As String, ByVal lpDirectory As String, ByVal lpResult As StringBuilder) As IntPtr
    End Function

    Public Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA" (ByVal lpFileName As String, ByVal lpFileSizeHigh As Integer) As Integer
    Public Declare Function SHFileOperation Lib "shell32.dll" Alias "SHFileOperation" (ByRef lpFileOp As SHFILEOPSTRUCT) As Integer

    Public Enum EFileAccess
        _GenericRead = &H80000000
        _GenericWrite = &H40000000
        _GenericExecute = &H20000000
        _GenericAll = &H10000000
    End Enum

    Public Enum EFileShare
        _None = &H0
        _Read = &H1
        _Write = &H2
        _Delete = &H4
    End Enum

    Public Enum ECreationDisposition
        _New = 1
        _CreateAlways = 2
        _OpenExisting = 3
        _OpenAlways = 4
        _TruncateExisting = 5
    End Enum

    Public Enum EFileAttributes
        _Readonly = &H1
        _Hidden = &H2
        _System = &H4
        _Directory = &H10
        _Archive = &H20
        _Device = &H40
        _Normal = &H80
        _Temporary = &H100
        _SparseFile = &H200
        _ReparsePoint = &H400
        _Compressed = &H800
        _Offline = &H1000
        _NotContentIndexed = &H2000
        _Encrypted = &H4000
        _Write_Through = &H80000000
        _Overlapped = &H40000000
        _NoBuffering = &H20000000
        _RandomAccess = &H10000000
        _SequentialScan = &H8000000
        _DeleteOnClose = &H4000000
        _BackupSemantics = &H2000000
        _PosixSemantics = &H1000000
        _OpenReparsePoint = &H200000
        _OpenNoRecall = &H100000
        _FirstPipeInstance = &H80000
    End Enum

    <Flags()> _
    Public Enum FileMapAccess As UInteger
        FileMapCopy = &H1
        FileMapWrite = &H2
        FileMapRead = &H4
        FileMapAllAccess = &H1F
        fileMapExecute = &H20
    End Enum

    <Flags()> _
   Public Enum FileMapProtection As UInteger
        PageReadonly = &H2
        PageReadWrite = &H4
        PageWriteCopy = &H8
        PageExecuteRead = &H20
        PageExecuteReadWrite = &H40
        SectionCommit = &H8000000
        SectionImage = &H1000000
        SectionNoCache = &H10000000
        SectionReserve = &H4000000
    End Enum

    Public Const SEE_MASK_INVOKEIDLIST As Integer = &HC
    Public Const SEE_MASK_NOCLOSEPROCESS As Integer = &H40
    Public Const SEE_MASK_FLAG_NO_UI As Integer = &H400

    Public Structure SHFILEINFO
        Public hIcon As IntPtr            ' : icon
        Public iIcon As Integer           ' : icondex
        Public dwAttributes As Integer    ' : SFGAO_ flags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    Public Declare Auto Function SHGetFileInfo Lib "shell32.dll" _
            (ByVal pszPath As String, _
             ByVal dwFileAttributes As Integer, _
             ByRef psfi As SHFILEINFO, _
             ByVal cbFileInfo As Integer, _
             ByVal uFlags As Integer) As IntPtr

    Public Const SHGFI_ICON As Integer = &H100
    Public Const SHGFI_SMALLICON As Integer = &H1
    Public Const SHGFI_LARGEICON As Integer = &H0

#End Region

#Region "Declarations used for system"

    Public Structure PERFORMANCE_INFORMATION
        Dim Size As Integer
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)> _
        Dim noNeed() As Integer          ' No need because informations are retrieved elsewhere
        Dim HandlesCount As Integer
        Dim ProcessCount As Integer
        Dim ThreadCount As Integer
    End Structure

    Public Structure SYSTEM_PERFORMANCE_INFORMATION
        Dim IdleTime As Long
        Dim IoReadTransferCount As Long
        Dim IoWriteTransferCount As Long
        Dim IoOtherTransferCount As Long
        Dim IoReadOperationCount As Integer
        Dim IoWriteOperationCount As Integer
        Dim IoOtherOperationCount As Integer
        Dim AvailablePages As Integer
        Dim CommittedPages As Integer
        Dim CommitLimit As Integer
        Dim PeakCommitment As Integer
        Dim PageFaults As Integer
        Dim CopyOnWriteFaults As Integer
        Dim TransitionFaults As Integer
        Dim CacheTransitionFaults As Integer
        Dim DemandZeroFaults As Integer
        Dim PagesRead As Integer
        Dim PagesReadIos As Integer
        Dim CacheRead As Integer
        Dim CacheReadIos As Integer
        Dim PagefilePagesWritten As Integer
        Dim PagefilePagesWriteIos As Integer
        Dim MappedFilePagesWritten As Integer
        Dim MappedFilePageWriteIos As Integer
        Dim PagedPoolUsage As Integer
        Dim NonPagedPoolUsage As Integer
        Dim PagedPoolAllocs As Integer
        Dim PagedPoolFrees As Integer
        Dim NonPagedPoolAllocs As Integer
        Dim NonPagedPoolFrees As Integer
        Dim FreeSystemPtes As Integer
        Dim SystemCodePages As Integer
        Dim TotalSystemDriverPages As Integer
        Dim TotalSystemCodePages As Integer
        Dim SmallNonPagedPoolLookasideListAllocateHits As Integer
        Dim SmallPagedPoolLookasideAllocateHits As Integer
        Dim Reserved3 As Integer
        Dim SystemCachePages As Integer
        Dim PagedPoolPages As Integer
        Dim SystemDriverPages As Integer
        Dim FastReadNoWait As Integer
        Dim FastReadWait As Integer
        Dim FastReadResourceMiss As Integer
        Dim FastReadNotPossible As Integer
        Dim FastMdlReadNoWait As Integer
        Dim FastMdlReadWait As Integer
        Dim FastMdlReadResourceMiss As Integer
        Dim FastMdlReadNotPossible As Integer
        Dim MapDataNoWait As Integer
        Dim MapDataWait As Integer
        Dim MapDataNoWaitMiss As Integer
        Dim MapDataWaitMiss As Integer
        Dim PinMappedDataCount As Integer
        Dim PinReadNoWait As Integer
        Dim PinReadWait As Integer
        Dim PinReadNoWaitMiss As Integer
        Dim PinReadWaitMiss As Integer
        Dim CopyReadNoWait As Integer
        Dim CopyReadWait As Integer
        Dim CopyReadNoWaitMiss As Integer
        Dim CopyReadWaitMiss As Integer
        Dim MdlReadNoWait As Integer
        Dim MdlReadWait As Integer
        Dim MdlReadNoWaitMiss As Integer
        Dim MdlReadWaitMiss As Integer
        Dim ReadAheadIos As Integer
        Dim LazyWriteIos As Integer
        Dim LazyWritePages As Integer
        Dim DataFlushes As Integer
        Dim DataPages As Integer
        Dim ContextSwitches As Integer
        Dim FirstLevelTbFills As Integer
        Dim SecondLevelTbFills As Integer
        Dim SystemCalls As Integer
    End Structure

    Public Structure SYSTEM_CACHE_INFORMATION
        Dim SystemCacheWsSize As Integer
        Dim SystemCacheWsPeakSize As Integer
        Dim SystemCacheWsFaults As Integer
        Dim SystemCacheWsMinimum As Integer
        Dim SystemCacheWsMaximum As Integer
        Dim TransitionSharedPages As Integer
        Dim TransitionSharedPagesPeak As Integer
        Private Reserved1 As Integer
        Private Reserved2 As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION
        Dim IdleTime As Long
        Dim KernelTime As Long
        Dim UserTime As Long
        Dim DpcTime As Long
        Dim InterruptTime As Long
        Dim InterruptCount As Integer
    End Structure

    Public Structure SYSTEM_BASIC_INFORMATION
        Private Reserved As Integer
        Dim TimerResolution As Integer
        Dim PageSize As Integer
        Dim NumberOfPhysicalPages As Integer
        Dim LowestPhysicalPageNumber As Integer
        Dim HighestPhysicalPageNumber As Integer
        Dim AllocationGranularity As Integer
        Dim MinimumUserModeAddress As Integer
        Dim MaximumUserModeAddress As Integer
        Dim ActiveProcessorsAffinityMask As Integer
        Dim NumberOfProcessors As Byte
    End Structure

    Public Enum SYSTEM_INFORMATION_CLASS As Integer
        SystemBasicInformation
        SystemProcessorInformation
        SystemPerformanceInformation
        SystemTimeOfDayInformation
        SystemNotImplemented1
        SystemProcessesAndThreadsInformation
        SystemCallCounts
        SystemConfigurationInformation
        SystemProcessorTimes
        SystemGlobalFlag
        SystemNotImplemented2
        SystemModuleInformation
        SystemLockInformation
        SystemNotImplemented3
        SystemNotImplemented4
        SystemNotImplemented5
        SystemHandleInformation
        SystemObjectInformation
        SystemPagefileInformation
        SystemInstructionEmulationCounts
        SystemInvalidInfoClass1
        SystemCacheInformation
        SystemPoolTagInformation
        SystemProcessorStatistics
        SystemDpcInformation
        SystemNotImplemented6
        SystemLoadImage
        SystemUnloadImage
        SystemTimeAdjustment
        SystemNotImplemented7
        SystemNotImplemented8
        SystemNotImplemented9
        SystemCrashDumpInformation
        SystemExceptionInformation
        SystemCrashDumpStateInformation
        SystemKernelDebuggerInformation
        SystemContextSwitchInformation
        SystemRegistryQuotaInformation
        SystemLoadAndCallImage
        SystemPrioritySeparation
        SystemNotImplemented10
        SystemNotImplemented11
        SystemInvalidInfoClass2
        SystemInvalidInfoClass3
        SystemTimeZoneInformation
        SystemLookasideInformation
        SystemSetTimeSlipEvent
        SystemCreateSession
        SystemDeleteSession
        SystemInvalidInfoClass4
        SystemRangeStartInformation
        SystemVerifierInformation
        SystemAddVerifier
        SystemSessionProcessesInformation
    End Enum


    <DllImport("psapi.dll", SetLastError:=True)> _
    Public Shared Function GetPerformanceInfo(<Out()> ByRef PerformanceInformation As PERFORMANCE_INFORMATION, _
                                              <[In]()> ByVal Size As Integer) As Boolean
    End Function

    Public Declare Function ZwQuerySystemInformation Lib "ntdll.dll" (ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByRef SystemInformation As SYSTEM_BASIC_INFORMATION, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    Public Declare Function ZwQuerySystemInformation Lib "ntdll.dll" (ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByRef SystemInformation As SYSTEM_CACHE_INFORMATION, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    Public Declare Function ZwQuerySystemInformation Lib "ntdll.dll" (ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByRef SystemInformation As SYSTEM_PERFORMANCE_INFORMATION, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    Public Declare Function ZwQuerySystemInformation Lib "ntdll.dll" (ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByRef SystemInformation As SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As Integer

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function ExitWindowsEx(ByVal flags As ExitFlags, _
                                         ByVal reason As Integer) As Boolean
    End Function

    <DllImport("powrprof.dll", SetLastError:=True)> _
    Public Shared Function SetSuspendState(ByVal Hibernate As Boolean, _
                                           ByVal ForceCritical As Boolean, _
                                           ByVal DisableWakeEvent As Boolean) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function LockWorkStation() As Boolean
    End Function

    Public Enum ExitFlags As Integer
        Logoff = &H0
        Shutdown = &H1
        Reboot = &H2
        Poweroff = &H8
        RestartApps = &H40
        Force = &H4
        ForceIfHung = &H10
    End Enum

#End Region

#Region "Declarations used for windows (not Windows :-p)"

    Public Const NULL_BRUSH As Integer = 5 ' Stock Object

    Public Const GWL_HWNDPARENT As Integer = -8
    Public Const GW_CHILD As Integer = 5
    Public Const GW_HWNDNEXT As Integer = 2
    Public Const WM_GETICON As Integer = &H7F
    Public Const GCL_HICON As Integer = -14
    Public Const GCL_HICONSM As Integer = -34
    Public Const WM_CLOSE As Integer = &H10
    Public Const SW_HIDE As Integer = 0
    Public Const SW_SHOWNORMAL As Integer = 1
    Public Const SW_SHOWMINIMIZED As Integer = 2
    Public Const SW_SHOWMAXIMIZED As Integer = 3
    Public Const SW_MAXIMIZE As Integer = 3
    Public Const SW_SHOWNOACTIVATE As Integer = 4
    Public Const SW_SHOW As Integer = 5
    Public Const SW_MINIMIZE As Integer = 6
    Public Const SW_SHOWMINNOACTIVE As Integer = 7
    Public Const SW_SHOWNA As Integer = 8
    Public Const SW_RESTORE As Integer = 9
    Public Const SW_SHOWDEFAULT As Integer = 10
    Public Const SWP_NOSIZE As Integer = &H1
    Public Const SWP_NOMOVE As Integer = &H2
    Public Const SWP_NOACTIVATE As Integer = &H10
    Public Const HWND_TOPMOST As Integer = -1
    Public Const SWP_SHOWWINDOW As Integer = &H40
    Public Const HWND_NOTOPMOST As Integer = -2
    Public Const GWL_EXSTYLE As Integer = -20
    Public Const WS_EX_LAYERED As Integer = &H80000
    Public Const LWA_COLORKEY As Integer = &H1
    Public Const LWA_ALPHA As Integer = &H2

    Public Enum LVS_EX
        LVS_EX_GRIDLINES = &H1
        LVS_EX_SUBITEMIMAGES = &H2
        LVS_EX_CHECKBOXES = &H4
        LVS_EX_TRACKSELECT = &H8
        LVS_EX_HEADERDRAGDROP = &H10
        LVS_EX_FULLROWSELECT = &H20
        LVS_EX_ONECLICKACTIVATE = &H40
        LVS_EX_TWOCLICKACTIVATE = &H80
        LVS_EX_FLATSB = &H100
        LVS_EX_REGIONAL = &H200
        LVS_EX_INFOTIP = &H400
        LVS_EX_UNDERLINEHOT = &H800
        LVS_EX_UNDERLINECOLD = &H1000
        LVS_EX_MULTIWORKAREAS = &H2000
        LVS_EX_LABELTIP = &H4000
        LVS_EX_BORDERSELECT = &H8000
        LVS_EX_DOUBLEBUFFER = &H10000
        LVS_EX_HIDELABELS = &H20000
        LVS_EX_SINGLEROW = &H40000
        LVS_EX_SNAPTOGRID = &H80000
        LVS_EX_SIMPLESELECT = &H100000
    End Enum

    Public Enum SHOW_FINDOW_TYPE As Integer
        Hide = 0
        ShowNormal = 1
        Normal = 1
        ShowMinimized = 2
        ShowMaximized = 3
        Maximize = 3
        ShowNoActivate = 4
        Show = 5
        Minimize = 6
        ShowMinNoActive = 7
        ShowNa = 8
        Restore = 9
        ShowDefault = 10
        ForceMinimize = 11
    End Enum

    Public Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Integer, ByRef lpdwProcessId As Integer) As Integer
    Public Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer ' Get the cursor position
    Public Declare Function WindowFromPoint Lib "user32" (ByVal xPoint As Integer, ByVal yPoint As Integer) As Integer ' Get the handle of the window that is foremost on a particular X, Y position. Used here To get the window under the cursor
    Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer ' Get the window co-ordinates in a RECT structure
    Public Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Integer) As Integer ' Retrieve a handle For the hDC of a window
    Public Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer ' Release the memory occupied by an hDC
    Public Declare Function CreatePen Lib "gdi32" (ByVal nPenStyle As Integer, ByVal nWidth As Integer, ByVal crColor As Integer) As Integer ' Create a GDI graphics pen object
    Public Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer ' Used to select brushes, pens, and clipping regions
    Public Declare Function GetStockObject Lib "gdi32" (ByVal nIndex As Integer) As Integer ' Get hold of a "stock" object. I use it to get a Null Brush
    Public Declare Function SetROP2 Lib "gdi32" (ByVal hdc As Integer, ByVal nDrawMode As Integer) As Integer ' Used To set the Raster OPeration of a window
    Public Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Integer) As Integer ' Delete a GDI Object
    Public Declare Function Rectangle Lib "gdi32" (ByVal hdc As Integer, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer) As Integer ' GDI Graphics- draw a rectangle using current pen, brush, etc.
    Public Declare Function SetCapture Lib "user32" (ByVal hwnd As Integer) As Integer ' Set mouse events only For one window
    Public Declare Function ReleaseCapture Lib "user32" () As Integer ' Release the mouse capture
    Public Declare Function CreateRectRgn Lib "gdi32" (ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer) As Integer ' Create a rectangular region
    Public Declare Function SelectClipRgn Lib "gdi32" (ByVal hdc As Integer, ByVal hRgn As Integer) As Integer ' Select the clipping region of an hDC
    Public Declare Function GetClipRgn Lib "gdi32" (ByVal hdc As Integer, ByVal hRgn As Integer) As Integer ' Get the Clipping region of an hDC

    Public Declare Function GetForegroundWindow Lib "user32" () As IntPtr
    Public Declare Function GetLayeredWindowAttributes Lib "User32.Dll" (ByVal hwnd As IntPtr, ByRef pcrKey As Integer, ByRef pbAlpha As Byte, ByRef pdwFlags As Integer) As Boolean
    Public Declare Auto Function SetLayeredWindowAttributes Lib "User32.Dll" (ByVal hWnd As IntPtr, ByVal crKey As Integer, ByVal Alpha As Byte, ByVal dwFlags As Integer) As Boolean
    Public Declare Function FlashWindowEx Lib "user32" (ByRef pfwi As FLASHWINFO) As Boolean
    Public Declare Function SetForegroundWindowAPI Lib "user32" Alias "SetForegroundWindow" (ByVal hWnd As IntPtr) As Integer
    Public Declare Function SetActiveWindowAPI Lib "user32.dll" Alias "SetActiveWindow" (ByVal hWnd As IntPtr) As Integer
    Public Declare Function EnableWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal fEnable As Integer) As Integer
    Public Declare Function GetClassLong Lib "user32.dll" Alias "GetClassLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
    Public Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    Public Declare Function GetWindowAPI Lib "user32" Alias "GetWindow" (ByVal hWnd As IntPtr, ByVal wCmd As Integer) As IntPtr
    Public Declare Auto Function GetDesktopWindow Lib "user32.dll" () As IntPtr

    Public Structure POINTAPI
        Dim X As Integer
        Dim Y As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WindowPlacement
        Public Length As UInteger
        Public Flags As UInteger
        Public ShowCmd As ShowState
        Public MinPosition As Point
        Public MaxPosition As Point
        Public NormalPosition As RECT
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure FLASHWINFO
        Public cbSize As Integer
        Public hWnd As Integer
        Public dwFlags As Integer
        Public uCount As Integer
        Public dwTimeout As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> <Serializable()> Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Public Enum ShowState As UInteger
        SW_HIDE = 0
        SW_SHOWNORMAL = 1
        SW_SHOWMINIMIZED = 2
        SW_SHOWMAXIMIZED = 3
        SW_SHOWNOACTIVATE = 4
        SW_SHOW = 5
        SW_MINIMIZE = 6
        SW_SHOWMINNOACTIVE = 7
        SW_SHOWNA = 8
        SW_RESTORE = 9
        SW_SHOWDEFAULT = 10
    End Enum

    Public Enum SendMessageTimeoutFlags As Integer
        SMTO_NORMAL = &H0
        SMTO_BLOCK = &H1
        SMTO_ABORTIFHUNG = &H2
        SMTO_NOTIMEOUTIFNOTHUNG = &H8
    End Enum

    <DllImport("user32.dll")> _
    Public Shared Function SetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function SendMessageTimeout(ByVal windowHandle As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer, ByVal flags As SendMessageTimeoutFlags, ByVal timeout As Integer, ByRef result As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function SetWindowText(ByVal hwnd As IntPtr, ByVal lpString As System.Text.StringBuilder) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetWindowText(ByVal hwnd As IntPtr, ByVal lpString As StringBuilder, ByVal cch As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function IsWindowVisible(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function GetWindowLong(ByVal hWnd As IntPtr, ByVal nIndex As Integer) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
    End Sub

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function IsWindowEnabled(ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function SetWindowPlacement(ByVal hWnd As IntPtr, ByRef lpwndpl As WindowPlacement) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetWindowPlacement(ByVal hWnd As IntPtr, ByRef lpwndpl As WindowPlacement) As Boolean
    End Function

#End Region

    ' OK
#Region "Declarations used for services"

    Public Enum ServiceQueryState As UInteger
        Active = 1
        Inactive = 2
        All = 3
    End Enum

    <Flags()> _
    Public Enum ServiceQueryType As UInteger
        Driver = &HB
        Win32 = &H30
    End Enum

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function ControlService(<[In]()> ByVal Service As IntPtr, _
                                          <[In]()> ByVal Control As SERVICE_CONTROL, _
                                          <Out()> ByRef ServiceStatus As SERVICE_STATUS) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function QueryServiceStatusEx(<[In]()> ByVal Service As IntPtr, _
                <[In]()> ByVal InfoLevel As Integer, _
                <Out()> <[Optional]()> ByRef ServiceStatus As SERVICE_STATUS_PROCESS, _
                <[In]()> ByVal BufferSize As Integer, _
                <Out()> ByRef BytesNeeded As Integer) As Boolean
    End Function

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
    Public Shared Function CloseServiceHandle(<[In]()> ByVal ServiceHandle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function OpenSCManager(<[In]()> <[Optional]()> ByVal MachineName As String, <[In]()> <[Optional]()> ByVal DatabaseName As String, <[In]()> ByVal DesiredAccess As ScManagerAccess) As IntPtr
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function OpenService(<[In]()> ByVal SCManager As IntPtr, <[In]()> ByVal ServiceName As String, <[In]()> ByVal DesiredAccess As ServiceAccess) As IntPtr
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function StartService(<[In]()> ByVal Service As IntPtr, <[In]()> ByVal NumServiceArgs As Integer, <[In]()> <[Optional]()> ByVal Args As String()) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function


    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function ChangeServiceConfig(<[In]()> ByVal Service As IntPtr, _
                <[In]()> ByVal ServiceType As SERVICE_TYPE, _
                <[In]()> ByVal StartType As SERVICE_START_TYPE, _
                <[In]()> ByVal ErrorControl As SERVICE_ERROR_CONTROL, _
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

    Public Structure SERVICE_STATUS
        Dim dwServiceType As Integer
        Dim dwCurrentState As Integer
        Dim dwControlsAccepted As Integer
        Dim dwWin32ExitCode As Integer
        Dim dwServiceSpecificExitCode As Integer
        Dim dwCheckPoint As Integer
        Dim dwWaitHint As Integer
    End Structure

    Public Enum SERVICE_CONTROL
        _STOP = 1
        _PAUSE = 2
        _CONTINUE = 3
        _INTERROGATE = 4
        _SHUTDOWN = 5
        _PARAMCHANGE = 6
        _NETBINDADD = 7
        _NETBINDREMOVE = 8
        _NETBINDENABLE = 9
        _NETBINDDISABLE = 10
        _DEVICEEVENT = 11
        _HARDWAREPROFILECHANGE = 12
        _POWEREVENT = 13
        _SESSIONCHANGE = 14
    End Enum

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure QUERY_SERVICE_CONFIG
        Public ServiceType As SERVICE_TYPE
        Public StartType As SERVICE_START_TYPE
        Public ErrorControl As SERVICE_ERROR_CONTROL
        <MarshalAs(UnmanagedType.LPTStr)> Public BinaryPathName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public LoadOrderGroup As String
        Public TagID As Integer
        Public Dependencies As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public ServiceStartName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public DisplayName As String
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SERVICE_STATUS_PROCESS
        Public ServiceType As SERVICE_TYPE
        Public CurrentState As SERVICE_STATE
        Public ControlsAccepted As SERVICE_ACCEPT
        Public Win32ExitCode As Integer
        Public ServiceSpecificExitCode As Integer
        Public CheckPoint As Integer
        Public WaitHint As Integer
        Public ProcessID As Integer
        Public ServiceFlags As SERVICE_FLAGS
    End Structure

    <DllImport("advapi32.dll", EntryPoint:="QueryServiceConfigW", SetLastError:=True, CharSet:=CharSet.Unicode, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function QueryServiceConfig(ByVal hService As IntPtr, _
        ByVal pBuffer As IntPtr, _
        ByVal cbBufSize As Integer, _
        ByRef pcbBytesNeeded As Integer) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure ENUM_SERVICE_STATUS_PROCESS
        <MarshalAs(UnmanagedType.LPTStr)> Public ServiceName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public DisplayName As String
        <MarshalAs(UnmanagedType.Struct)> Public ServiceStatusProcess As SERVICE_STATUS_PROCESS
    End Structure

    Public Enum SERVICE_START_TYPE As Integer
        BootStart = &H0
        SystemStart = &H1
        AutoStart = &H2
        DemandStart = &H3
        StartDisabled = &H4
        SERVICESTARTTYPE_NO_CHANGE = SERVICE_NO_CHANGE
    End Enum

    Public Enum SERVICE_STATE As UInteger
        ContinuePending = &H5
        PausePending = &H6
        Paused = &H7
        Running = &H4
        StartPending = &H2
        StopPending = &H3
        Stopped = &H1
        Unknown = &HF
    End Enum

    Public Enum SERVICE_TYPE As UInteger
        FileSystemDriver = &H2
        KernelDriver = &H1
        Adapter = &H4
        RecognizerDriver = &H8
        Win32OwnProcess = &H10
        Win32ShareProcess = &H20
        InteractiveProcess = &H100
        NoChange = &HFFFFFFFF
    End Enum

    Public Enum SERVICE_ERROR_CONTROL As Integer
        Critical = &H3
        Ignore = &H0
        Normal = &H1
        Severe = &H2
        Unknown = &HF
        NoChange = &HFFFFFFFF
    End Enum

    Public Enum SERVICE_FLAGS As UInteger
        None = 0
        RunsInSystemProcess = &H1
    End Enum

    Public Enum SERVICE_ACCEPT As UInteger
        [NetBindChange] = &H10
        [ParamChange] = &H8
        [PauseContinue] = &H2
        [PreShutdown] = &H100
        [Shutdown] = &H4
        [Stop] = &H1
        [HardwareProfileChange] = &H20
        [PowerEvent] = &H40
        [SessionChange] = &H80
    End Enum

    Public Enum SERVICE_RIGHTS As UInteger
        SERVICE_QUERY_CONFIG = &H1
        SERVICE_CHANGE_CONFIG = &H2
        SERVICE_QUERY_STATUS = &H4
        SERVICE_ENUMERATE_DEPENDENTS = &H8
        SERVICE_START = &H10
        SERVICE_STOP = &H20
        SERVICE_PAUSE_CONTINUE = &H40
        SERVICE_INTERROGATE = &H80
        SERVICE_USER_DEFINED_CONTROL = &H100
        SERVICE_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED Or _
                SERVICE_QUERY_CONFIG Or SERVICE_CHANGE_CONFIG Or SERVICE_QUERY_STATUS Or _
                SERVICE_ENUMERATE_DEPENDENTS Or SERVICE_START Or SERVICE_STOP Or _
                SERVICE_PAUSE_CONTINUE Or SERVICE_INTERROGATE Or _
                SERVICE_USER_DEFINED_CONTROL
    End Enum




    ' May will remove these constants
    Public Const SERVICE_NO_CHANGE As Integer = &HFFFFFFFF

    Public Const ERROR_MORE_DATA As Integer = 234
    Public Const SC_ENUM_PROCESS_INFO As Integer = &H0
    Public Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4

    Public Const SC_STATUS_PROCESS_INFO As Integer = 0
    Public Const SERVICE_ACTIVE As Integer = &H1
    Public Const SERVICE_INACTIVE As Integer = &H2
    Public Const SERVICE_STATE_ALL As Integer = (SERVICE_ACTIVE Or SERVICE_INACTIVE)
    Public Const SERVICE_ADAPTER As Integer = &H4
    Public Const SERVICE_WIN32_OWN_PROCESS As Integer = &H10
    Public Const SERVICE_WIN32_SHARE_PROCESS As Integer = &H20
    Public Const SERVICE_WIN32 As Integer = SERVICE_WIN32_OWN_PROCESS + SERVICE_WIN32_SHARE_PROCESS

    Public Const SERVICE_DRIVER As Integer = &HB
    Public Const SERVICE_INTERACTIVE_PROCESS As Integer = &H100
    Public Const SERVICE_ALL As Integer = SERVICE_DRIVER Or SERVICE_WIN32_OWN_PROCESS Or _
            SERVICE_WIN32_SHARE_PROCESS Or SERVICE_WIN32 Or SERVICE_INTERACTIVE_PROCESS

    Public Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Public Const SC_MANAGER_CONNECT As Integer = &H1
    Public Const SC_MANAGER_CREATE_SERVICE As Integer = &H2
    Public Const SC_MANAGER_LOCK As Integer = &H8
    Public Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
    Public Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
    Public Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

#End Region

    ' OK
#Region "Declarations used for registry"

    Public Const KEY_NOTIFY As Integer = &H10

    ' http://msdn.microsoft.com/en-us/library/ms724892(VS.85).aspx
    ' Type of Key
    Public Enum KEY_TYPE
        HKEY_CLASSES_ROOT = &H80000000
        HKEY_CURRENT_USER = &H80000001
        HKEY_LOCAL_MACHINE = &H80000002
        HKEY_USERS = &H80000003
        HKEY_CURRENT_CONFIG = &H80000005
        HKEY_PERFORMANCE_DATA = &H80000004
        HKEY_DYN_DATA = &H80000006
    End Enum

    ' Type of monitoring to apply
    Public Enum KEY_MONITORING_TYPE
        REG_NOTIFY_CHANGE_NAME = &H1            ' Subkey added or deleted
        REG_NOTIFY_CHANGE_ATTRIBUTES = &H2      ' Attributes changed
        REG_NOTIFY_CHANGE_LAST_SET = &H4        ' Value changed (changed, deleted, added)
        REG_NOTIFY_CHANGE_SECURITY = &H8        ' Security descriptor changed
    End Enum

    Public Enum WaitResult As UInteger
        INFINITE = &HFFFFFFFF
        WAIT_ABANDONED = &H80
        WAIT_OBJECT_0 = &H0
        WAIT_TIMEOUT = &H102
        WAIT_FAILED = &HFFFFFFFF
    End Enum

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function WaitForSingleObject(ByVal [Object] As IntPtr, _
                                               ByVal Timeout As UInteger) As WaitResult
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

    ' Some constants
    Public Const BCM_FIRST As Integer = &H1600
    Public Const BCM_SETSHIELD As Integer = (BCM_FIRST + &HC)

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

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure UNICODE_STRING
        Public Length As UShort
        Public MaximumLength As UShort
        Public Buffer As IntPtr
    End Structure

#End Region

    ' OK
#Region "Declarations used for WMI"

    Public Enum PROCESS_RETURN_CODE_WMI
        SuccessfulCompletion = 0
        AccessDenied = 2
        InsufficientPrivilege = 3
        UnknownFailure = 8
        PathNotFound = 9
        InvalidParameter = 21
    End Enum

    Public Enum WMI_INFO_SERVICE
        AcceptPause
        AcceptStop
        CheckPoint
        DesktopInteract
        DisplayName
        ErrorControl
        ExitCode
        Name
        PathName
        ProcessId
        ServiceSpecificExitCode
        ServiceType
        Started
        StartMode
        StartName
        State
        SystemCreationClassName
        SystemName
        TagId
        WaitHint
    End Enum

    Public Enum SERVICE_RETURN_CODE_WMI
        Success = 0
        NotSupported = 1
        AccessDenied = 2
        DependentServicesRunning = 3
        InvalidServiceControl = 4
        ServiceCannotAcceptControl = 5
        ServiceNotActive = 6
        ServiceRequestTimeout = 7
        UnknownFailure = 8
        PathNotFound = 9
        ServiceAlreadyRunning = 10
        ServiceDatabaseLocked = 11
        ServiceDependencyDeleted = 12
        ServiceDependencyFailure = 13
        ServiceDisabled = 14
        ServiceLogonFailure = 15
        ServiceMarkedForDeletion = 16
        ServiceNoThread = 17
        StatusCircularDependency = 18
        StatusDuplicateName = 19
        StatusInvalidName = 20
        StatusInvalidParameter = 21
        StatusInvalidServiceAccount = 22
        StatusServiceExists = 23
        ServiceAlreadyPaused = 24
    End Enum

    Public Enum WMI_SHUTDOWN_VALUES As Integer
        [LogOff] = 0
        [Shutdown] = 1
        [Reboot] = 2
        [PowerOff] = 8
        [Force] = 4
    End Enum

    Public Enum WMI_INFO_PROCESS
        'Caption
        CommandLine
        'CreationClassName
        CreationDate
        'CSCreationClassName
        'CSName
        'Description
        ExecutablePath
        'ExecutionState
        'Handle
        HandleCount
        'InstallDate
        KernelModeTime
        MaximumWorkingSetSize
        MinimumWorkingSetSize
        'Name
        'OSCreationClassName
        'OSName
        OtherOperationCount
        OtherTransferCount
        PageFaults
        PageFileUsage
        ParentProcessId
        PeakPageFileUsage
        PeakVirtualSize
        PeakWorkingSetSize
        Priority
        PrivatePageCount
        ProcessId
        QuotaNonPagedPoolUsage
        QuotaPagedPoolUsage
        QuotaPeakNonPagedPoolUsage
        QuotaPeakPagedPoolUsage
        ReadOperationCount
        ReadTransferCount
        'SessionId
        'Status
        TerminationDate
        ThreadCount
        UserModeTime
        VirtualSize
        WindowsVersion
        WorkingSetSize
        WriteOperationCount
        WriteTransferCount
    End Enum

    Public Enum WMI_INFO_THREAD
        ElapsedTime
        ExecutionState
        Handle
        KernelModeTime
        Name
        Priority
        PriorityBase
        ProcessHandle
        StartAddress
        ThreadState
        ThreadWaitReason
        UserModeTime
    End Enum

#End Region

    ' OK
#Region "Declarations used for graphical functions"

    ' Some constants
    Public Const SC_CLOSE As Integer = &HF060
    Public Const MF_GRAYED As Integer = &H1
    Public Const LVS_EX_BORDERSELECT As Integer = &H8000
    Public Const LVS_EX_DOUBLEBUFFER As Integer = &H10000

    Public Enum IconSize As Integer
        ICON_SMALL = &H0
        ICON_BIG = &H1
    End Enum

    Public Enum LVM As UInteger
        LVM_FIRST = &H1000
        LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54)
        LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55)
    End Enum

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
#Region "Declarations used for keyboard management"

    Public Const HC_ACTION As Integer = 0

    Public Enum HookType As Byte
        WH_JOURNALRECORD = 0
        WH_JOURNALPLAYBACK = 1
        WH_KEYBOARD = 2
        WH_GETMESSAGE = 3
        WH_CALLWNDPROC = 4
        WH_CBT = 5
        WH_SYSMSGFILTER = 6
        WH_MOUSE = 7
        WH_HARDWARE = 8
        WH_DEBUG = 9
        WH_SHELL = 10
        WH_FOREGROUNDIDLE = 11
        WH_CALLWNDPROCRET = 12
        WH_KEYBOARD_LL = 13
        WH_MOUSE_LL = 14
    End Enum

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function UnhookWindowsHookEx(ByVal hhk As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function CallNextHookEx(ByVal hhk As IntPtr, ByVal nCode As Integer, _
                                    ByVal wParam As IntPtr, _
                                    <[In]()> ByVal lParam As KBDLLHOOKSTRUCT) As Integer
    End Function

    <DllImport("user32.dll")> _
    Public Shared Function GetAsyncKeyState(ByVal vKey As Int32) As Short
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure KBDLLHOOKSTRUCT
        Public vkCode As Integer
        Public scanCode As Integer
        Public flags As KBDLLHOOKSTRUCTFlags
        Public time As Integer
        Public dwExtraInfo As IntPtr
    End Structure

    <Flags()> _
    Public Enum KBDLLHOOKSTRUCTFlags As Integer
        LLKHF_EXTENDED = &H1
        LLKHF_INJECTED = &H10
        LLKHF_ALTDOWN = &H20
        LLKHF_UP = &H80
    End Enum

#End Region

    ' OK
#Region "Declarations used for error management"

    <Flags()> _
    Public Enum FormatMessageFlags As Integer
        FORMAT_MESSAGE_ALLOCATE_BUFFER = &H100
        FORMAT_MESSAGE_ARGUMENT_ARRAY = &H2000
        FORMAT_MESSAGE_FROM_HMODULE = &H800
        FORMAT_MESSAGE_FROM_STRING = &H400
        FORMAT_MESSAGE_FROM_SYSTEM = &H1000
        FORMAT_MESSAGE_IGNORE_INSERTS = &H200
    End Enum

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Private Shared Function LocalFree(ByVal hMem As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Public Shared Function FormatMessage(ByVal Flags As FormatMessageFlags, _
                            ByVal Source As IntPtr, _
                            ByVal MessageId As Integer, _
                            ByVal LanguageId As Integer, _
                            ByVal Buffer As IntPtr, _
                            ByVal Size As Integer, _
                            ByVal Arguments As IntPtr) As UInteger
    End Function

    Public Shared Function GetError() As String

        Dim lpMsgBuf As IntPtr = IntPtr.Zero
        Dim nLastError As Integer = Marshal.GetLastWin32Error
        Dim dwChars As UInteger = FormatMessage(FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER _
                                                Or FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM _
                                                Or FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS, _
                                    IntPtr.Zero, nLastError, 0, lpMsgBuf, 0, IntPtr.Zero)

        ' Unknown error
        If dwChars = 0 Then
            Return "Unknown error occured (0x" & nLastError.ToString("x") & ")"
        End If

        ' Retrieve string
        Dim sRet As String = Marshal.PtrToStringAnsi(lpMsgBuf)
        lpMsgBuf = LocalFree(lpMsgBuf)
        Return sRet

    End Function

#End Region

    ' OK
#Region "Declarations used for handles"

    Public Shared ReadOnly InvalidHandleValue As New IntPtr(-1)

    <Flags()> _
    Public Enum DuplicateOptions As Integer
        CloseSource = &H1
        SameAccess = &H2
        SameAttributes = &H4
    End Enum

    <Flags()> _
    Public Enum HandleFlags As Byte
        ProtectFromClose = &H1
        Inherit = &H2
        AuditObjectClose = &H4
    End Enum

    Public Structure SystemHandleInformation
        Public ProcessId As Integer
        Public ObjectTypeNumber As Byte
        Public Flags As HandleFlags
        Public Handle As Short
        Public [Object] As IntPtr
        Public GrantedAccess As STANDARD_RIGHTS
    End Structure

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

    ' TOREMOVE
    ' Public Declare Function DuplicateHandle Lib "kernel32" (ByVal hSourceProcessHandle As Integer, ByVal hSourceHandle As Integer, ByVal hTargetProcessHandle As Integer, ByRef lpTargetHandle As Integer, ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwOptions As Integer) As Integer

#End Region

End Class
