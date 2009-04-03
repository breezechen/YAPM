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

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text

Public Class API


#Region "Declarations used for processes"

    ' Const
    Public Const GR_USEROBJECTS As Integer = 1
    Public Const GR_GDIOBJECTS As Integer = 0

    Public Const PROCESS_SET_INFORMATION As Integer = &H200
    Public Const PROCESS_SUSPEND_RESUME As Integer = &H800
    Public Const PROCESS_QUERY_INFORMATION As Integer = &H400
    Public Const PROCESS_TERMINATE As Integer = &H1
    Public Const PROCESS_SET_QUOTA As Integer = &H100
    Public Const PROCESS_CREATE_THREAD As Integer = &H2
    Public Const PROCESS_VM_OPERATION As Integer = &H8
    Public Const PROCESS_VM_READ As Integer = &H10
    Public Const PROCESS_VM_WRITE As Integer = &H20

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
    Public Enum WMI_INFO
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

    Public Structure TOKEN_USER
        Dim User As SID_AND_ATTRIBUTES
    End Structure

    Public Structure SID_AND_ATTRIBUTES
        Dim Sid As Integer
        Dim Attributes As Integer
    End Structure

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function GetTokenInformation(ByVal TokenHandle As Integer, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, ByVal TokenInformation As IntPtr, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    End Function

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationProcess(ByVal ProcessHandle As Integer, ByVal ProcessInformationClass As PROCESS_INFORMATION_CLASS, ByVal ProcessInformation As IntPtr, ByVal ProcessInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function
    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQueryInformationProcess(ByVal ProcessHandle As Integer, ByVal ProcessInformationClass As PROCESS_INFORMATION_CLASS, ByRef ProcessInformation As PROCESS_BASIC_INFORMATION, ByVal ProcessInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function QueryDosDevice(ByVal DeviceName As String, ByVal TargetPath As StringBuilder, ByVal MaxLength As Integer) As Integer
    End Function
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function LookupAccountSid(ByVal SystemName As String, ByVal SID As Integer, ByVal Name As StringBuilder, ByRef NameSize As Integer, ByVal ReferencedDomainName As StringBuilder, ByRef ReferencedDomainNameSize As Integer, _
    ByRef Use As SID_NAME_USE) As Boolean
    End Function

    Public Declare Function GetModuleFileNameExA Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByVal hModule As Integer, ByVal ModuleName As String, ByVal nSize As Integer) As Integer
    Public Declare Function EnumProcessModules Lib "psapi.dll" (ByVal hProcess As Integer, ByVal lphModule As Integer, ByVal cb As Integer, ByVal lpcbNeeded As Integer) As Boolean

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Public Shared Function ZwQuerySystemInformation(ByVal SystemInformationClass As SYSTEM_INFORMATION_CLASS, ByVal SystemInformation As IntPtr, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As UInteger
    End Function

    <DllImport("kernel32.dll")> _
    Public Shared Function SetProcessWorkingSetSize(ByVal hwProc As Integer, ByVal minimumSize As Integer, ByVal maximumSize As Integer) As Integer
    End Function

    <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
    Public Structure UNICODE_STRING
        Public Length As UShort
        Public MaximumLength As UShort
        Public Buffer As Integer
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

    Public Declare Function OpenProcess Lib "Kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
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

End Class
