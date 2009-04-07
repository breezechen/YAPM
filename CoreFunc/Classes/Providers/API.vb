' =======================================================
' Yet Another Process Monitor (YAPM)
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

Public Class API


#Region "Declarations used for processes"

    ' Const
    Public Const GR_USEROBJECTS As Integer = 1
    Public Const GR_GDIOBJECTS As Integer = 0

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

    Public Const PROCESS_SET_INFORMATION As Integer = &H200
    Public Const PROCESS_SUSPEND_RESUME As Integer = &H800
    Public Const PROCESS_QUERY_INFORMATION As Integer = &H400
    Public Const PROCESS_TERMINATE As Integer = &H1
    Public Const PROCESS_SET_QUOTA As Integer = &H100
    Public Const PROCESS_CREATE_THREAD As Integer = &H2
    Public Const PROCESS_VM_OPERATION As Integer = &H8
    Public Const PROCESS_VM_READ As Integer = &H10
    Public Const PROCESS_VM_WRITE As Integer = &H20

    Public Const TOKEN_QUERY As Integer = &H8
    Public Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20

    Public Const SE_PRIVILEGE_ENABLED As Integer = &H2
    Public Const SE_PRIVILEGE_ENABLED_BY_DEFAULT As Integer = &H1
    Public Const SE_PRIVILEGE_DISBALED As Integer = &H0
    Public Const SE_PRIVILEGE_REMOVED As Integer = &H4

    Public Const SERVICE_NO_CHANGE As Integer = &HFFFFFFFF

    Public Enum PRIVILEGE_STATUS
        PRIVILEGE_ENABLED = &H2
        PRIVILEGE_DISBALED = &H0
        PRIVILEGE_REMOVED = &H4
    End Enum
    Public Enum PROTECTION_TYPE As Integer
        PAGE_EXECUTE = &H10
        PAGE_EXECUTE_READ = &H20
        PAGE_EXECUTE_READWRITE = &H40
        PAGE_EXECUTE_WRITECOPY = &H80
        PAGE_NOACCESS = &H1
        PAGE_READONLY = &H2
        PAGE_READWRITE = &H4
        PAGE_WRITECOPY = &H8
        PAGE_GUARD = &H100
        PAGE_NOCACHE = &H200
        PAGE_WRITECOMBINE = &H400
    End Enum
    Public Enum MEMORY_STATE As Integer
        MEM_FREE = &H10000
        MEM_COMMIT = &H1000
        MEM_RESERVE = &H2000
    End Enum
    Public Enum MEMORY_TYPE As Integer
        MEM_IMAGE = &H1000000
        MEM_PRIVATE = &H20000
        MEM_MAPPED = &H40000
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
        SystemFileCacheInformation
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
        THREAD_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED Or STANDARD_RIGHTS.SYNCHRONIZE Or &HFFFF
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
        PROCESS_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED Or STANDARD_RIGHTS.SYNCHRONIZE Or &HFFFF
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
    Public Enum SERVICE_START_TYPE As Integer
        BootStart = &H0
        SystemStart = &H1
        AutoStart = &H2
        DemandStart = &H3
        StartDisabled = &H4
        SERVICESTARTTYPE_NO_CHANGE = SERVICE_NO_CHANGE
    End Enum
    Public Enum SERVICE_STATE As Integer
        ContinuePending = &H5
        PausePending = &H6
        Paused = &H7
        Running = &H4
        StartPending = &H2
        StopPending = &H3
        Stopped = &H1
        Unknown = &HF
    End Enum
    Public Enum SERVICE_TYPE As Integer
        FileSystemDriver = &H2
        KernelDriver = &H1
        Adapter = &H4
        RecognizerDriver = &H8
        Win32OwnProcess = &H10
        Win32ShareProcess = &H20
        InteractiveProcess = &H100
    End Enum
    Public Enum SERVICE_ERROR_CONTROL As Integer
        Critical = &H3
        Ignore = &H0
        Normal = &H1
        Severe = &H2
        Unknown = &HF
    End Enum
    Public Enum SERVICE_FLAGS As Integer
        None = 0
        RunsInSystemProcess = &H1
    End Enum
    Public Enum SERVICE_ACCEPT As Integer
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
        SERVICE_ALL_ACCESS = STANDARD_RIGHTS.STANDARD_RIGHTS_REQUIRED Or SERVICE_QUERY_CONFIG Or SERVICE_CHANGE_CONFIG Or SERVICE_QUERY_STATUS Or SERVICE_ENUMERATE_DEPENDENTS Or SERVICE_START Or SERVICE_STOP Or SERVICE_PAUSE_CONTINUE Or SERVICE_INTERROGATE Or SERVICE_USER_DEFINED_CONTROL
    End Enum
    Public Enum PROCESS_RETURN_CODE_WMI
        SuccessfulCompletion = 0
        AccessDenied = 2
        InsufficientPrivilege = 3
        UnknownFailure = 8
        PathNotFound = 9
        InvalidParameter = 21
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

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure CLIENT_ID
        Public UniqueProcess As Integer
        Public UniqueThread As Integer
    End Structure

    Public Structure TOKEN_USER
        Dim User As SID_AND_ATTRIBUTES
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MODULEINFO
        Public BaseOfDll As IntPtr
        Public SizeOfImage As Integer
        Public EntryPoint As IntPtr
    End Structure

    Public Structure SID_AND_ATTRIBUTES
        Dim Sid As Integer
        Dim Attributes As Integer
    End Structure

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function CloseServiceHandle(ByVal serviceHandle As IntPtr) As Boolean
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function EnumServicesStatusEx(ByVal SCManager As IntPtr, ByVal InfoLevel As Integer, ByVal ServiceType As Integer, ByVal ServiceState As Integer, ByVal Services As IntPtr, ByVal BufSize As Integer, _
        ByRef BytesNeeded As Integer, ByRef ServicesReturned As Integer, ByRef ResumeHandle As Integer, ByVal GroupName As Integer) As Integer
    End Function

    <DllImport("psapi.dll")> _
    Public Shared Function EnumProcessModules(ByVal ProcessHandle As Integer, ByVal ModuleHandles As IntPtr(), ByVal Size As Integer, ByRef RequiredSize As Integer) As Boolean
    End Function
    <DllImport("psapi.dll", CharSet:=CharSet.Unicode)> _
    Public Shared Function GetModuleBaseName(ByVal ProcessHandle As Integer, ByVal ModuleHandle As IntPtr, ByVal BaseName As StringBuilder, ByVal Size As Integer) As Integer
    End Function
    <DllImport("psapi.dll", CharSet:=CharSet.Unicode)> _
    Public Shared Function GetModuleFileNameEx(ByVal ProcessHandle As Integer, ByVal ModuleHandle As IntPtr, ByVal FileName As StringBuilder, ByVal Size As Integer) As Integer
    End Function
    <DllImport("psapi.dll")> _
    Public Shared Function GetModuleInformation(ByVal ProcessHandle As Integer, ByVal ModuleHandle As IntPtr, ByRef ModInfo As MODULEINFO, ByVal Size As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function ResumeThread(ByVal hThread As IntPtr) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function SuspendThread(ByVal hThread As IntPtr) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function SetThreadPriority(ByVal hThread As IntPtr, ByVal priority As Integer) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Public Shared Function TerminateThread(ByVal hThread As IntPtr, ByVal exitcode As Integer) As UInt32
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function OpenThread(ByVal DesiredAccess As THREAD_RIGHTS, ByVal InheritHandle As Integer, ByVal ThreadId As Integer) As Integer
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function GetTokenInformation(ByVal TokenHandle As Integer, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, ByVal TokenInformation As IntPtr, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    End Function

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationProcess(ByVal ProcessHandle As Integer, ByVal ProcessInformationClass As PROCESS_INFORMATION_CLASS, ByVal ProcessInformation As IntPtr, ByVal ProcessInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationProcess(ByVal ProcessHandle As Integer, ByVal ProcessInformationClass As PROCESS_INFORMATION_CLASS, ByRef ProcessInformation As PROCESS_BASIC_INFORMATION, ByVal ProcessInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function

    <DllImport("advapi32.dll", SetLastError:=True)> _
    Public Shared Function ControlService(ByVal hService As IntPtr, ByVal dwControl As SERVICE_CONTROL, ByRef lpServiceStatus As SERVICE_STATUS) As Boolean
    End Function

    <DllImport("advapi32.dll", CharSet:=CharSet.Auto, entrypoint:="ChangeServiceConfigA", SetLastError:=True)> _
    Public Shared Function ChangeServiceConfig(ByVal hService As Integer, ByVal dwServiceType As Integer, ByVal dwStartType As SERVICE_START_TYPE, ByVal dwErrorControl As Integer, ByVal lpBinaryPathName As String, ByVal lpLoadOrderGroup As String, ByVal lpdwTagId As Integer, ByVal lpDependencies As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpServiceStartName As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpPassword As String, <MarshalAs(UnmanagedType.LPStr)> ByVal lpDisplayName As String) As Boolean
    End Function

    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function LockServiceDatabase(ByVal hSCManager As Integer) As Integer
    End Function
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto)> _
    Public Shared Function UnlockServiceDatabase(ByVal hSCManager As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function QueryDosDevice(ByVal DeviceName As String, ByVal TargetPath As StringBuilder, ByVal MaxLength As Integer) As Integer
    End Function
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function LookupAccountSid(ByVal SystemName As String, ByVal SID As Integer, ByVal Name As StringBuilder, ByRef NameSize As Integer, ByVal ReferencedDomainName As StringBuilder, ByRef ReferencedDomainNameSize As Integer, ByRef Use As SID_NAME_USE) As Boolean
    End Function

    Public Declare Function GetModuleFileNameExA Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByVal hModule As Integer, ByVal ModuleName As String, ByVal nSize As Integer) As Integer

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQuerySystemInformation(ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByVal SystemInformation As IntPtr, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As UInteger
    End Function

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationThread(ByVal ThreadHandle As Integer, ByVal ThreadInformationClass As THREAD_INFORMATION_CLASS, ByRef ThreadInformation As THREAD_BASIC_INFORMATION, ByVal ThreadInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function SetProcessWorkingSetSize(ByVal hwProc As Integer, ByVal minimumSize As Integer, ByVal maximumSize As Integer) As Integer
    End Function

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
    Public Shared Function _
        QueryServiceConfig(ByVal hService As IntPtr, _
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

    Public Structure TOKEN_PRIVILEGES2
        Dim PrivilegeCount As Integer
        Dim Privileges As LUID_AND_ATTRIBUTES
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure THREAD_BASIC_INFORMATION
        Public ExitStatus As Integer
        Public TebBaseAddress As Integer
        Public ClientId As CLIENT_ID
        Public AffinityMask As Integer
        Public Priority As Integer
        Public BasePriority As Integer
    End Structure

    Public Structure SERVICE_STATUS
        Dim dwServiceType As Integer
        Dim dwCurrentState As Integer
        Dim dwControlsAccepted As Integer
        Dim dwWin32ExitCode As Integer
        Dim dwServiceSpecificExitCode As Integer
        Dim dwCheckPoint As Integer
        Dim dwWaitHint As Integer
    End Structure

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure UNICODE_STRING
        Public Length As UShort
        Public MaximumLength As UShort
        Public Buffer As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SYSTEM_THREAD_INFORMATION
        Public KernelTime As Long
        Public UserTime As Long
        Public CreateTime As Long
        Public WaitTime As Integer
        Public StartAddress As Integer
        Public ClientId As CLIENT_ID
        Public Priority As Integer
        Public BasePriority As Integer
        Public ContextSwitchCount As Integer
        Public State As Integer
        Public WaitReason As KWAIT_REASON
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure QUERY_SERVICE_CONFIG
        Public ServiceType As SERVICE_TYPE
        Public StartType As SERVICE_START_TYPE
        Public ErrorControl As SERVICE_ERROR_CONTROL
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public BinaryPathName As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public LoadOrderGroup As String
        Public TagID As Integer
        Public Dependencies As Integer
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public ServiceStartName As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public DisplayName As String
    End Structure


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
    Public Structure PROCESS_BASIC_INFORMATION
        Public ExitStatus As Integer
        Public PebBaseAddress As Integer
        Public AffinityMask As Integer
        Public BasePriority As Integer
        Public UniqueProcessId As Integer
        Public InheritedFromUniqueProcessId As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure IO_COUNTERS
        Public ReadOperationCount As ULong
        Public WriteOperationCount As ULong
        Public OtherOperationCount As ULong
        Public ReadTransferCount As ULong
        Public WriteTransferCount As ULong
        Public OtherTransferCount As ULong
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
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
    End Structure

    Public Structure MEMORY_BASIC_INFORMATION ' 28 bytes
        Dim BaseAddress As Integer
        Dim AllocationBase As Integer
        Dim AllocationProtect As Integer
        Dim RegionSize As Integer
        Dim State As Integer
        Dim Protect As Integer
        Dim lType As Integer
    End Structure

    Public Structure LUID
        Dim lowpart As Integer
        Dim highpart As Integer
    End Structure

    Public Structure LUID_AND_ATTRIBUTES
        Dim pLuid As LUID
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

    Public Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        '<VBFixedArray(25)> _
        Dim Privileges() As LUID_AND_ATTRIBUTES
    End Structure

    Public Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByVal TokenInformationClass As Integer, ByVal TokenInformation As Integer, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    Public Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer           'Returns a valid LUID which is important when making security changes in NT.
    Public Declare Function LookupPrivilegeNameA Lib "advapi32.dll" (ByVal lpSystemName As String, ByRef lpLuid As LUID, ByVal lpName As String, ByRef cchName As Integer) As Integer                'Used to adjust your program's security privileges, can't restore without it!

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function LookupPrivilegeDisplayName( _
        ByVal SystemName As Integer, ByVal Name As String, ByVal DisplayName As String, _
        ByRef DisplayNameSize As Integer, ByRef LanguageId As Integer) As Boolean
    End Function

    <DllImport("advapi32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> _
    Public Shared Function QueryServiceStatusEx(ByVal serviceHandle As IntPtr, ByVal infoLevel As Integer, ByVal buffer As IntPtr, ByVal bufferSize As Integer, ByRef bytesNeeded As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function VirtualQueryEx(ByVal Process As Integer, ByVal Address As Integer, <MarshalAs(UnmanagedType.Struct)> ByRef Buffer As MEMORY_BASIC_INFORMATION, ByVal Size As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function OpenProcess(ByVal DesiredAccess As PROCESS_RIGHTS, ByVal InheritHandle As Integer, ByVal ProcessId As Integer) As Integer
    End Function

    Public Declare Function EnumProcessModules2 Lib "psapi.dll" Alias "EnumProcessModules" (ByVal hProcess As Integer, ByVal lphModule As Integer, ByVal cb As Integer, ByVal lpcbNeeded As Integer) As Boolean
    Public Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As Integer) As Integer
    Public Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Integer, ByVal uExitCode As Integer) As Integer
    Public Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    Public Declare Function NtSuspendProcess Lib "Ntdll.dll" (ByVal hProc As Integer) As Integer
    Public Declare Function NtResumeProcess Lib "Ntdll.dll" (ByVal hProc As Integer) As Integer
    Public Declare Function SetPriorityClass Lib "kernel32" (ByVal hProcess As Integer, ByVal dwPriorityClass As Integer) As Integer
    Public Declare Function SetProcessAffinityMask Lib "kernel32" (ByVal hProcess As Integer, ByVal dwProcessAffinityMask As Integer) As Integer
    Public Declare Function GetGuiResources Lib "user32.dll" (ByVal hProcess As Integer, ByVal uiFlags As Integer) As Integer
    Public Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Public Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Public Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer
    Public Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As IntPtr
    Public Declare Function lstrlenA Lib "kernel32" (ByVal Ptr As Integer) As Integer
    Public Declare Function lstrcpyA Lib "kernel32" (ByVal RetVal As String, ByVal Ptr As Integer) As Integer
    Public Declare Function OpenService Lib "advapi32.dll" Alias "OpenServiceA" (ByVal hSCManager As IntPtr, ByVal lpServiceName As String, ByVal dwDesiredAccess As SERVICE_RIGHTS) As IntPtr
    Public Declare Function apiStartService Lib "advapi32.dll" Alias "StartServiceA" (ByVal hService As IntPtr, ByVal dwNumServiceArgs As Integer, ByVal lpServiceArgVectors As Integer) As Integer

#End Region


    Private Declare Function GetLastError Lib "kernel32" () As Integer
    Private Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
    Private Const LANG_NEUTRAL As Integer = &H0
    Private Const SUBLANG_DEFAULT As Integer = &H1
    Private Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Integer, _
    ByVal lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, _
    ByVal lpBuffer As String, ByVal nSize As Integer, ByVal Arguments As Integer) As Integer
    Public Shared Function GetError() As String
        Dim Buffer As String
        Buffer = Space$(1024)
        FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0, GetLastError, LANG_NEUTRAL, Buffer, Len(Buffer), 0)
        Return Trim$(Buffer)
    End Function

    ' Return true if it is Vista
    Public Shared Function IsWindowsVista() As Boolean
        Return ((Environment.OSVersion.Platform = PlatformID.Win32NT) And (Environment.OSVersion.Version.Major = 6) And (Environment.OSVersion.Version.Minor = 0))
    End Function

End Class
