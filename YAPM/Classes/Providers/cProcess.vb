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
Imports System.Security.Principal
Imports System.Text

Public Class cProcess
    Inherits cGeneralObject

    ' ========================================
    ' API declarations
    ' ========================================
#Region "API"

    Private Declare Function SetProcessAffinityMask Lib "kernel32" (ByVal hProcess As Integer, ByVal dwProcessAffinityMask As Integer) As Integer
    Private Declare Function GetProcessAffinityMask Lib "kernel32" (ByVal hProcess As Integer, ByRef lpProcessAffinityMask As Integer, ByRef lpSystemAffinityMask As Integer) As Integer

    Private Declare Function GetGuiResources Lib "user32.dll" (ByVal hProcess As Integer, ByVal uiFlags As Integer) As Integer

    Private Declare Function GetProcessIoCounters Lib "kernel32.dll" (ByVal hProcess As Integer, <Out()> ByRef lpIoCounters As PIO_COUNTERS) As Integer

    Private Declare Function CheckRemoteDebuggerPresent Lib "kernel32" (ByVal hProcess As Integer, ByRef DebuggerPresent As Boolean) As Boolean
    Private Declare Function IsProcessInJob Lib "kernel32" (ByVal hProcess As Integer, ByVal JobHandle As Integer, ByRef Result As Boolean) As Boolean

    Private Declare Function CreateToolhelpSnapshot Lib "kernel32" Alias "CreateToolhelp32Snapshot" (ByVal lFlags As Integer, ByVal lProcessID As Integer) As IntPtr
    Private Declare Function ProcessFirst Lib "kernel32" Alias "Process32First" (ByVal hSnapShot As IntPtr, ByRef uProcess As ProcessEntry32) As Integer
    Private Declare Function ProcessNext Lib "kernel32" Alias "Process32Next" (ByVal hSnapShot As IntPtr, ByRef uProcess As ProcessEntry32) As Integer

    Private Declare Function OpenProcess Lib "Kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    Private Declare Function NtSuspendProcess Lib "Ntdll.dll" (ByVal hProc As Integer) As Integer
    Private Declare Function NtResumeProcess Lib "Ntdll.dll" (ByVal hProc As Integer) As Integer
    Private Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As Integer) As Integer
    Private Declare Function SetPriorityClass Lib "kernel32" (ByVal hProcess As Integer, ByVal dwPriorityClass As Integer) As Integer

    'Private Declare Function NtQueryInformationProcess Lib "Ntdll.dll" (ByVal hProcess As Integer, ByVal ProcessInformationClass As Integer, ByRef ProcessInformation As IntPtr, ByVal ProcessInformationLength As Integer, ByVal ReturnLength As Integer) As Integer
    <DllImport("ntdll.dll", SetLastError:=True)> _
    Private Shared Function NtQueryInformationProcess(ByVal ProcessHandle As Integer, _
                                                 ByVal ProcessInformationClass As _
                                                 Integer, ByVal _
                                                 ProcessInformation As IntPtr, _
                                                 ByVal ProcessInformationLength _
                                                 As Integer, ByRef ReturnLength _
                                                 As Integer) As Integer
    End Function

    Private Declare Function NtQueryInformationProcess Lib "Ntdll.dll" (ByVal hProcess As Integer, ByVal ProcessInformationClass As Integer, ByRef ProcessInformation As PROCESS_BASIC_INFORMATION, ByVal ProcessInformationLength As Integer, ByVal ReturnLength As Integer) As Integer
    Private Declare Function NtSetInformationProcess Lib "Ntdll.dll" (ByVal hProcess As Integer, ByVal ProcessInformationClass As Integer, ByVal ProcessInformation As Integer, ByVal ProcessInformationLength As Integer) As Integer

    Private Declare Function NtQueryInformationThread Lib "Ntdll.dll" (ByVal hThread As Integer, ByVal ThreadInformationClass As Integer, ByVal ThreadInformation As Integer, ByVal ThreadInformationLength As Integer, ByVal ReturnLength As Integer) As Integer
    Private Declare Function NtSetInformationThread Lib "Ntdll.dll" (ByVal hThread As Integer, ByVal ThreadInformationClass As Integer, ByVal ThreadInformation As Integer, ByVal ThreadInformationLength As Integer) As Integer
    Private Declare Function OpenThread Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwThreadId As Integer) As Integer
    Private Declare Sub GetSystemInfo Lib "kernel32" (ByVal lpSystemInfo As SYSTEM_INFO)

    Private Declare Function GetModuleFileNameExA Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByVal hModule As Integer, ByVal ModuleName As String, ByVal nSize As Integer) As Integer
    Private Declare Function GetCurrentProcessId Lib "kernel32.dll" () As Integer
    Private Declare Function EnumProcessModules Lib "psapi.dll" (ByVal hProcess As Integer, ByVal lphModule As Integer, ByVal cb As Integer, ByVal lpcbNeeded As Integer) As Boolean

    Private Declare Function GetProcessMemoryInfo Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByRef ppsmemCounters As PROCESS_MEMORY_COUNTERS, ByVal cb As Integer) As Integer
    Private Declare Function GetPriorityClass Lib "kernel32" Alias "GetPriorityClass" (ByVal hProcess As Integer) As Integer

    Private Declare Function GetProcessTimes Lib "kernel32" (ByVal hProcess As Integer, ByRef lpCreationTime As Long, ByRef lpExitTime As Long, ByRef lpKernelTime As Long, ByRef lpUserTime As Long) As Integer

    Private Declare Function CreateToolhelp32Snapshot Lib "kernel32.dll" (ByVal dwFlags As Integer, ByVal th32ProcessID As Integer) As Integer
    Private Declare Function Thread32First Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean
    Private Declare Function Thread32Next Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean

    Private Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Integer, ByVal uExitCode As Integer) As Integer

    Private Declare Function GetSecurityInfo Lib "advapi32.dll" (ByVal hObject As Integer, ByVal ObjectType As Integer, ByVal SecurityInformation As Integer, ByVal ppsidOwner As Integer, ByVal ppsidGroup As Integer, ByVal ppDacl As Integer, ByVal ppSacl As Integer, ByVal ppSecurityDescriptor As Integer) As Integer
    Private Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer           'Returns a valid LUID which is important when making security changes in NT.
    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer

    <DllImport("kernel32.dll")> _
    Private Shared Function SetProcessWorkingSetSize(ByVal hwProc As Integer, ByVal minimumSize As Integer, ByVal maximumSize As Integer) As Integer
    End Function

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function GetTokenInformation(ByVal TokenHandle As Integer, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, ByVal TokenInformation As IntPtr, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    End Function

    Private Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByVal TokenInformationClass As Integer, ByVal TokenInformation As Integer, ByVal TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Boolean
    Private Declare Function LookupPrivilegeNameA Lib "advapi32.dll" (ByVal lpSystemName As String, ByRef lpLuid As LUID, ByVal lpName As String, ByRef cchName As Integer) As Integer                'Used to adjust your program's security privileges, can't restore without it!
    ' Declare Auto Function ConvertSidToStringSid Lib "advapi32.dll" (ByVal pSID() As Byte, _
    'ByRef ptrSid As IntPtr) As Boolean
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Public Shared Function LookupAccountSid(ByVal SystemName As String, ByVal SID As Integer, ByVal Name As StringBuilder, ByRef NameSize As Integer, ByVal ReferencedDomainName As StringBuilder, ByRef ReferencedDomainNameSize As Integer, _
        ByRef Use As SID_NAME_USE) As Boolean
    End Function

    Private Declare Function LookupAccountSid Lib "advapi32.dll" _
           Alias "LookupAccountSidA" ( _
           ByVal systemName As String, _
           ByVal psid As Integer, _
           ByVal accountName As String, _
           ByRef cbAccount As Integer, _
           ByVal domainName As String, _
           ByRef cbDomainName As Integer, _
           ByRef use As Integer) As Boolean
    Private Declare Auto Function ConvertSidToStringSid Lib "advapi32.dll" _
        (ByVal bSID As IntPtr, <System.Runtime.InteropServices.In(), _
        System.Runtime.InteropServices.Out(), _
        System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)> ByRef SIDString As String) As Integer

    Private Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Private Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function GetExitCodeThread Lib "kernel32" (ByVal hThread As Integer, ByRef lpExitCode As Integer) As Integer
    Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer
    Private Declare Auto Function ConvertSidToStringSid Lib "advapi32.dll" ( _
      ByVal Sid As IntPtr, _
      ByRef StringSid As IntPtr _
      ) As Boolean
    <DllImport("kernel32.dll", SetLastError:=True, ExactSpelling:=True)> _
    Private Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, _
        ByVal dwSize As UInteger, ByVal flAllocationType As UInteger, _
        ByVal flProtect As UInteger) As IntPtr
    End Function
    <DllImport("kernel32.dll")> _
    Private Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As Byte(), ByVal nSize As System.UInt32, <Out()> ByRef lpNumberOfBytesWritten As Int32) As Boolean
    End Function
    Private Declare Function ZwQueryInformationProcess Lib "NTDLL.DLL" (ByVal ProcessHandle As Integer, _
        ByVal ProcessInformationClass As PROCESSINFOCLASS, _
        ByRef ProcessInformation As Integer, _
        ByVal ProcessInformationLength As Integer, _
        ByRef ReturnLength As Integer) As Integer

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Private Shared Function QueryDosDevice(ByVal DeviceName As String, ByVal TargetPath As StringBuilder, ByVal MaxLength As Integer) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure UNICODE_STRING
        Public Length As UShort
        Public MaximumLength As UShort
        Public Buffer As Integer
    End Structure

    ' ========================================
    ' Constants
    ' ========================================
    Private Const KRN_LOAD As String = "LoadLibraryA"
    Private Const KRN_FREE As String = "FreeLibrary"
    Private Const KRN_DLL As String = "Kernel32"
    Private Const GET_HMOD As String = "GetModuleHandleA"
    Private Const MEM_RELEASE As UInteger = &H8000
    Private Const MEM_FREE As UInteger = &H10000
    Private Const MEM_COMMIT As UInteger = &H1000
    Private Const PAGE_READWRITE As UInteger = &H4
    Private Const PROCESS_SET_INFORMATION As Integer = &H200
    Private Const PROCESS_SUSPEND_RESUME As Integer = &H800
    Private Const PROCESS_QUERY_INFORMATION As Integer = &H400
    Private Const PROCESS_TERMINATE As Integer = &H1
    Private Const PROCESS_SET_QUOTA As Integer = &H100
    Private Const PROCESS_CREATE_THREAD As Integer = &H2
    Private Const PROCESS_VM_OPERATION As Integer = &H8
    Private Const PROCESS_VM_READ As Integer = &H10
    Private Const PROCESS_VM_WRITE As Integer = &H20
    Private Const THREAD_SET_INFORMATION As Integer = &H20
    Private Const THREAD_QUERY_INFORMATION As Integer = &H40
    Private Const TH32CS_SNAPPROCESS As Integer = &H2
    Private Const TH32CS_SNAPTHREAD As Integer = &H4
    Private Const SE_KERNEL_OBJECT As Integer = 6
    Private Const OWNER_SECURITY_INFORMATION As Integer = 1
    Private Const GROUP_SECURITY_INFORMATION As Integer = 2
    Private Const PROCESS_READ_CONTROL As Integer = &H20000
    Private Const TokenUser As Integer = 1
    Private Const TokenGroups As Integer = 2
    Private Const IDLE_PRIORITY_CLASS As Integer = &H40
    Private Const BELOW_NORMAL_PRIORITY_CLASS As Integer = &H4000
    Private Const NORMAL_PRIORITY_CLASS As Integer = &H20
    Private Const ABOVE_NORMAL_PRIORITY_CLASS As Integer = &H8000
    Private Const HIGH_PRIORITY_CLASS As Integer = &H80 '
    Private Const REALTIME_PRIORITY_CLASS As Integer = &H100
    Private Const DATETIME_DELTA As Long = 504911268000000000

    ' ========================================
    ' Structures for API
    ' ========================================
    Private Structure LUID
        Public LowPart As Integer
        Public HighPart As Integer
    End Structure

    Public Structure LightProcess
        Dim pid As Integer
        Dim name As String
        Dim parentPid As Integer
        Public Sub New(ByVal aPid As Integer, ByVal aName As String)
            pid = aPid
            name = aName
        End Sub
        Public Sub New(ByVal aPid As Integer, ByVal aParentPid As Integer)
            pid = aPid
            parentPid = aParentPid
        End Sub
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure PIO_COUNTERS
        <MarshalAs(UnmanagedType.U8, SizeConst:=8)> _
        Dim ReadOperationCount As UInt64
        <MarshalAs(UnmanagedType.U8, SizeConst:=8)> _
        Dim WriteOperationCount As UInt64
        <MarshalAs(UnmanagedType.U8, SizeConst:=8)> _
        Dim OtherOperationCount As UInt64
        <MarshalAs(UnmanagedType.U8, SizeConst:=8)> _
        Dim ReadTransferCount As UInt64
        <MarshalAs(UnmanagedType.U8, SizeConst:=8)> _
        Dim WriteTransferCount As UInt64
        <MarshalAs(UnmanagedType.U8, SizeConst:=8)> _
        Dim OtherTransferCount As UInt64
    End Structure

    Private Enum PROCESSINFOCLASS
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
        ProcessIoPortHandlers               ' K-mode only
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
        MaxProcessInfoClass             ' MaxProcessInfoClass must be the last one
    End Enum

    Private Structure SECURITY_ATTRIBUTES
        Dim nLength As Integer
        Dim lpSecurityDescriptor As Integer
        Dim bInheritHandle As Integer
    End Structure

    Private Structure THREADENTRY32
        Dim dwSize As Integer
        Dim cntUsage As Integer
        Dim th32ThreadID As Integer
        Dim th32OwnerProcessID As Integer
        Dim tpBasePri As Integer
        Dim tpDeltaPri As Integer
        Dim dwFlags As Integer
    End Structure

    Private Structure FILETIME2
        Dim dwLowDateTime As Integer
        Dim dwHighDateTime As Integer
    End Structure

    Private Structure CLIENT_ID
        Dim UniqueProcess As Integer
        Dim UniqueThread As Integer
    End Structure

    Private Structure PROCESS_BASIC_INFORMATION
        Dim ExitStatus As Integer
        Dim PEBBaseAddress As Integer
        Dim AffinityMask As Integer
        Dim BasePriority As Integer
        Dim UniqueProcessId As Integer
        Dim ParentProcessId As Integer
    End Structure

    Private Structure THREAD_BASIC_INFORMATION
        Dim ExitStatus As Integer
        Dim TebBaseAddress As Integer
        Dim ClientId As CLIENT_ID
        Dim AffinityMask As Integer
        Dim Priority As Integer
        Dim BasePriority As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SYSTEM_INFO
        Dim wProcessorArchitecture As Int16
        Dim wReserved As Int16
        Dim dwPageSize As Integer
        Dim lpMinimumApplicationAddress As Integer
        Dim lpMaximumApplicationAddress As Integer
        Dim dwActiveProcessorMask As Integer
        Dim dwNumberOfProcessors As Integer
        Dim dwProcessorType As Integer
        Dim dwAllocationGranularity As Integer
        Dim wProcessorLevel As Int16
        Dim wProcessorRevision As Int16
    End Structure

    Private Structure ProcessEntry32
        Dim dwSize As Integer
        Dim cntUsage As Integer
        Dim th32ProcessID As Integer
        Dim th32DefaultHeapID As Integer
        Dim th32ModuleID As Integer
        Dim cntThreads As Integer
        Dim th32ParentProcessID As Integer
        Dim pcPriClassBase As Integer
        Dim dwFlags As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public szExeFile As String
    End Structure

    Public Structure PROCESS_MEMORY_COUNTERS
        Dim cb As Integer
        Dim PageFaultCount As Integer
        Dim PeakWorkingSetSize As Integer
        Dim WorkingSetSize As Integer
        Dim QuotaPeakPagedPoolUsage As Integer
        Dim QuotaPagedPoolUsage As Integer
        Dim QuotaPeakNonPagedPoolUsage As Integer
        Dim QuotaNonPagedPoolUsage As Integer
        Dim PagefileUsage As Integer
        Dim PeakPagefileUsage As Integer
    End Structure

    Private Structure TOKEN_USER
        Dim User As SID_AND_ATTRIBUTES
    End Structure

    Private Structure SID_AND_ATTRIBUTES
        Dim Sid As Integer
        Dim Attributes As Integer
    End Structure

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

    Private Structure TOKEN_GROUPS
        Dim GroupCount As Integer
        Dim Groups() As SID_AND_ATTRIBUTES
    End Structure

    Public Structure PROC_TIME_INFO
        Dim time As Long
        Dim kernel As Long
        Dim user As Long
        Dim total As Long
        Public Sub New(ByVal aTime As Long, ByVal aUser As Long, ByVal aKernel As Long)
            time = aTime
            kernel = aKernel
            user = aUser
            total = time + kernel
        End Sub
    End Structure

    Public Structure PROC_MEM_INFO
        Dim time As Long
        Dim mem As cProcess.PROCESS_MEMORY_COUNTERS
        Public Sub New(ByVal aTime As Long, ByRef aMem As cProcess.PROCESS_MEMORY_COUNTERS)
            time = aTime
            mem = aMem
        End Sub
    End Structure

    Public Structure PROC_IO_INFO
        Dim time As Long
        Dim io As PIO_COUNTERS
        Public Sub New(ByVal aTime As Long, ByRef aIo As PIO_COUNTERS)
            time = aTime
            io = aIo
        End Sub
    End Structure

    Public Structure PROC_MISC_INFO
        Dim time As Long
        Dim gdiO As Integer
        Dim userO As Integer
        Dim cpuUsage As Double
        Dim averageCpuUsage As Double
        Public Sub New(ByVal aTime As Long, ByVal aGdi As Integer, ByVal aUser As _
                       Integer, ByVal aCpu As Double, ByVal aAverage As Double)
            time = aTime
            gdiO = aGdi
            userO = aUser
            cpuUsage = aCpu
            averageCpuUsage = aAverage
        End Sub
    End Structure

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

    ' Event released when instance receive a call to Refresh()
    Public Event Refreshed()

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _pebAdd As Integer = 0                  ' PEB base address
    Private _pid As Integer                         ' Process ID
    Private _path As String = vbNullString          ' _path of executable
    Private _UserName As String = vbNullString      ' User name
    Private _name As String = vbNullString          ' Name of process
    Private _hProcess As Integer = 0                ' Handle to the process
    Private _parentId As Integer = -1               ' Parent process ID
    Private _parentName As String = ""              ' Parent process ID
    Private _mainMod As System.Diagnostics.ProcessModule
    Private _intTag1 As Integer = 0
    Private _processors As Integer = 0
    Private _triedPathMem As Boolean = False
    Private _triedCommandLine As Boolean = False
    Private _commandLine As String

    Private _processorTime As Long = 0
    Private _kernelTime As Long = 0
    Private _userTime As Long = 0
    Private _memInfo As cProcess.PROCESS_MEMORY_COUNTERS
    Private _io As PIO_COUNTERS
    Private _gdiO As Integer
    Private _userO As Integer
    Private _fileInfo As FileVersionInfo
    Private _cpuUs As Double
    Private _averageCpuU As Double

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    ' Contains list of process names
    Private Shared _procs As New Dictionary(Of String, String)

    ' Save informations about performance
    Private _dicoProcMem As New SortedList(Of Integer, PROC_MEM_INFO)
    Private _dicoProcTimes As New SortedList(Of Integer, PROC_TIME_INFO)
    Private _dicoProcIO As New SortedList(Of Integer, PROC_IO_INFO)
    Private _dicoProcMisc As New SortedList(Of Integer, PROC_MISC_INFO)

    ' Contains devices (logical drives) and their corresponding path
    ' e.g. :        /Device/Harddisk1/...       , C:
    Private Shared _DicoLogicalDrivesNames As New Dictionary(Of String, String)


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal processId As Integer)
        MyBase.New()
        _pid = processId
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, _pid)
        _path = Me.Path
        If Me.Path = NO_INFO_RETRIEVED Then
            _name = getSimpleName(processId)
        End If
        _fileInfo = Me.FileVersionInfo(True)
        _processors = frmMain.PROCESSOR_COUNT
    End Sub

    Public Sub New(ByRef proc As LightProcess)
        MyBase.New()
        _pid = proc.pid
        _processors = frmMain.PROCESSOR_COUNT
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, _pid)
        If _hProcess = 0 Then
            ' Try to open with limited rights
            _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, 0, _pid)
        End If
        _name = proc.name
        _fileInfo = Me.FileVersionInfo(True)
    End Sub

    Protected Overloads Overrides Sub Finalize()
        CloseHandle(_hProcess)
        MyBase.Finalize()
    End Sub

    ' ========================================
    ' Getter and setter
    ' ========================================
    Public ReadOnly Property UserObjectsCount(Optional ByVal force As Boolean = False) As Integer
        Get
            If force Then
                If _hProcess > 0 Then
                    Return GetGuiResources(_hProcess, 1)     ' GR_USEROBJECTS = 0
                Else
                    Return 0
                End If
            Else
                Return _userO
            End If
        End Get
    End Property
    Public ReadOnly Property GDIObjectsCount(Optional ByVal force As Boolean = False) As Integer
        Get
            If force Then
                If _hProcess > 0 Then
                    Return GetGuiResources(_hProcess, 0)     ' GR_GDIOBJECTS = 0
                Else
                    Return 0
                End If
            Else
                Return _gdiO
            End If
        End Get
    End Property
    Public ReadOnly Property IsDotNet() As Boolean
        Get
            ' Dot net applications have loaded mscoree.dll
            Return False
            'Try
            '    Dim m() As String = cModule.EnumerateSpeed(Me.Pid)
            '    For Each m2 As ProcessModule In Me.Modules
            '        If m2.ModuleName.ToLowerInvariant = "mscoree.dll" Then
            '            Return True
            '        End If
            '    Next
            '    Return False
            'Catch ex As Exception
            '    Return False
            'End Try
        End Get
    End Property
    Public ReadOnly Property CommandLine() As String
        Get
            If _commandLine = vbNullString And _triedCommandLine = False Then
                _commandLine = GetCommandLine(_pid)
                _triedCommandLine = True
            End If
            Return _commandLine
        End Get
    End Property
    Public ReadOnly Property IsInJob() As Boolean
        Get
            Dim ret As Boolean = False
            IsProcessInJob(_hProcess, 0, ret)
            Return ret
        End Get
    End Property
    Public ReadOnly Property IsPacked() As Boolean
        Get
            ' TODO
            Return False
        End Get
    End Property
    Public Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
        Set(ByVal value As Integer)
            _processors = value
        End Set
    End Property

    Public ReadOnly Property IsElevated() As Boolean
        Get
            ' TODO
            Return False
        End Get
    End Property
    Public ReadOnly Property IsDebugged() As Boolean
        Get
            Dim ret As Boolean = False
            CheckRemoteDebuggerPresent(_hProcess, ret)
            Return ret
        End Get
    End Property
    Public ReadOnly Property IsSystem() As Boolean
        Get
            ' TODO
        End Get
    End Property
    Public ReadOnly Property IsService() As Boolean
        Get
            ' TODO
        End Get
    End Property
    Public ReadOnly Property IsOwn() As Boolean
        Get
            ' TODO
        End Get
    End Property

    Public ReadOnly Property Pid() As Integer
        Get
            Return _pid
        End Get
    End Property

    Public Property IntTag1() As Integer
        Get
            Return _intTag1
        End Get
        Set(ByVal value As Integer)
            _intTag1 = value
        End Set
    End Property

    Public Property AffinityMask() As Integer
        Get
            If _hProcess > 0 Then
                Dim pMask As Integer = 0
                Dim sMask As Integer = 0
                GetProcessAffinityMask(_hProcess, pMask, sMask)
                Return pMask
            Else
                Return -1
            End If
        End Get
        Set(ByVal value As Integer)
            Dim __hProcess As Integer = OpenProcess(PROCESS_SET_INFORMATION, 0, _pid)
            If __hProcess > 0 Then
                SetProcessAffinityMask(__hProcess, value)
                CloseHandle(__hProcess)
            End If
        End Set
    End Property

    Public ReadOnly Property PEBAddress() As Integer
        Get
            If _pebAdd = 0 Then
                Dim Pbi As PROCESS_BASIC_INFORMATION
                Dim Ret As Integer

                Dim pt As Integer = 0

                NtQueryInformationProcess(_hProcess, 0, Pbi, 24, Ret)
                _pebAdd = Pbi.PEBBaseAddress
            End If
            Return _pebAdd
        End Get
    End Property

    Public ReadOnly Property ParentProcessId() As Integer
        Get
            If _parentId = -1 Then
                Dim Pbi As PROCESS_BASIC_INFORMATION
                Dim Ret As Integer

                Dim pt As Integer = 0
                'System.Runtime.InteropServices.Marshal.

                NtQueryInformationProcess(_hProcess, 0, Pbi, 24, Ret)
                _parentId = Pbi.ParentProcessId
            End If
            Return _parentId
        End Get
    End Property

    Public ReadOnly Property ParentProcessName() As String
        Get
            If _parentName = "" Then
                _parentName = cProcess.GetProcessName(Me.ParentProcessId)
                If _parentName = "" Then
                    _parentName = NO_INFO_RETRIEVED
                End If
            End If
            Return _parentName
        End Get
    End Property

    ' Get I/O informations
    Public ReadOnly Property GetIOvalues(Optional ByVal force As Boolean = False) As cProcess.PIO_COUNTERS
        Get
            If force Then
                If _hProcess > 0 Then
                    Dim pioc As PIO_COUNTERS
                    GetProcessIoCounters(_hProcess, pioc)
                    Return pioc
                End If
            Else
                Return _io
            End If
        End Get
    End Property

    ' Get fileinfo
    Public ReadOnly Property FileVersionInfo(Optional ByVal force As Boolean = False) As FileVersionInfo
        Get
            If force Then
                If Me.Path = NO_INFO_RETRIEVED Then
                    Return Nothing
                Else
                    Dim tMain As System.Diagnostics.ProcessModule = Me.MainModule
                    If tMain IsNot Nothing Then
                        Return tMain.FileVersionInfo
                    Else
                        Return Nothing
                    End If
                End If
            Else
                Return _fileInfo
            End If
        End Get
    End Property

    ' Get the performance dictionnaries
    Public ReadOnly Property DicoPerfMem() As SortedList(Of Integer, PROC_MEM_INFO)
        Get
            Return _dicoProcMem
        End Get
    End Property
    Public ReadOnly Property DicoPerfIO() As SortedList(Of Integer, PROC_IO_INFO)
        Get
            Return _dicoProcIO
        End Get
    End Property
    Public ReadOnly Property DicoPerfTimes() As SortedList(Of Integer, PROC_TIME_INFO)
        Get
            Return _dicoProcTimes
        End Get
    End Property

    ' Get the process path
    Public ReadOnly Property Path() As String
        Get
            If _path = vbNullString Then

                Dim s As String = vbNullString
                Dim Ret As Integer
                Dim sResult As String = Space(512)
                Dim hModule As Integer

                If _hProcess > 0 Then
                    Call EnumProcessModules(_hProcess, hModule, 4, Ret)
                    sResult = Space(260)
                    Call GetModuleFileNameExA(_hProcess, hModule, sResult, 260)
                    s = sResult
                End If

                If InStr(sResult, vbNullChar) > 1 Then
                    sResult = Left(sResult, InStr(sResult, vbNullChar) - 1)
                Else
                    sResult = NO_INFO_RETRIEVED
                End If

                If System.IO.File.Exists(sResult) = False Then
                    Call RefreshLogicalDrives()
                    sResult = GetImageFile(_pid)
                End If

                _path = sResult

                ' If path does not exist (it sometimes happens, but why ?)
                ' we try to get it from ProcessParameter block (in memory)
                If _triedPathMem = False AndAlso IO.File.Exists(_path) = False Then
                    _triedPathMem = True
                    Try
                        _path = GetImagePath_MemMethod(_pid)
                    Catch ex As Exception
                        _path = NO_INFO_RETRIEVED
                    End Try
                End If

            End If

            Return _path
        End Get
    End Property

    ' Get the process username
    Public ReadOnly Property UserName() As String
        Get
            Dim retLen As Integer

            If _UserName = vbNullString Then

                If Pid > 4 Then
                    Dim hToken As Integer
                    If OpenProcessToken(_hProcess, &H8, hToken) > 0 Then

                        GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, 0, retLen)
                        Dim data As IntPtr = Marshal.AllocHGlobal(retLen)
                        GetTokenInformation(hToken, TOKEN_INFORMATION_CLASS.TokenUser, CInt(data), retLen, retLen)

                        CloseHandle(hToken)

                        Dim user As New TOKEN_USER
                        user = CType(Marshal.PtrToStructure(data, GetType(TOKEN_USER)), TOKEN_USER)

                        Return GetAccountName(user.User.Sid, True)
                    Else
                        Return NO_INFO_RETRIEVED
                    End If

                ElseIf Pid = 4 Then
                    Return "SYSTEM"
                Else
                    Return "SYSTEM"
                End If
            End If

            Return _UserName

        End Get
    End Property


    ' Get process name
    Public ReadOnly Property Name() As String
        Get
            If _name = vbNullString Then
                _name = getSimpleName(_pid)
                If _name = vbNullString Then
                    _name = "N/A"
                End If
            End If
            Return _name
        End Get
    End Property

    ' Get processor time as a TimeSpan
    Public ReadOnly Property ProcessorTime(Optional ByVal force As Boolean = False) As Date
        Get
            If force Then
                Dim T0 As Long
                Dim T1 As Long
                Dim curTime2 As Long
                Dim curTime As Long
                Dim r As Date

                If _hProcess > 0 Then

                    GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                    r = New Date(curTime + curTime2)
                End If

                Return r
            Else
                Return New Date(_processorTime)
            End If
        End Get
    End Property

    ' Get processor time as a long
    ' Used without need for a call to 'refresh' sub
    Public ReadOnly Property ProcessorTimeLong() As Long
        Get
            Dim T0 As Long
            Dim T1 As Long
            Dim curTime2 As Long
            Dim curTime As Long
            Dim r As Long = 0

            If _hProcess > 0 Then

                GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                r = curTime + curTime2

            End If

            Return r
        End Get
    End Property

    ' Get kernel time as a TimeSpan
    Public ReadOnly Property KernelTime(Optional ByVal force As Boolean = False) As Date
        Get
            If force Then
                Dim T0 As Long
                Dim T1 As Long
                Dim curTime2 As Long
                Dim curTime As Long
                Dim r As Date

                If _hProcess > 0 Then

                    GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                    r = New Date(curTime)
                End If

                Return r
            Else
                Return New Date(_kernelTime)
            End If
        End Get
    End Property

    ' Get user time as a TimeSpan
    Public ReadOnly Property UserTime(Optional ByVal force As Boolean = False) As Date
        Get
            If force Then
                Dim T0 As Long
                Dim T1 As Long
                Dim curTime2 As Long
                Dim curTime As Long
                Dim r As Date

                If _hProcess > 0 Then

                    GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                    r = New Date(curTime2)
                End If

                Return r
            Else
                Return New Date(_userTime)
            End If
        End Get
    End Property

    ' Get all memory infos
    Public ReadOnly Property MemoryInfos(Optional ByVal force As Boolean = False) As PROCESS_MEMORY_COUNTERS
        Get
            If force Then
                Dim pmc As PROCESS_MEMORY_COUNTERS

                If _hProcess > 0 Then
                    pmc.cb = Marshal.SizeOf(pmc)
                    GetProcessMemoryInfo(_hProcess, pmc, pmc.cb)
                End If

                Return pmc
            Else
                Return _memInfo
            End If
        End Get
    End Property

    ' Return process threads
    Public ReadOnly Property Threads() As System.Diagnostics.ProcessThreadCollection
        Get
            Dim gProc As Process = Process.GetProcessById(_pid)
            Return gProc.Threads
        End Get
    End Property

    ' Get priority class a as string
    Public ReadOnly Property PriorityClass() As String
        Get
            Dim iP As Integer = 0

            If _hProcess > 0 Then
                iP = GetPriorityClass(_hProcess)
            End If

            Select Case iP
                Case IDLE_PRIORITY_CLASS
                    Return "Idle"
                Case BELOW_NORMAL_PRIORITY_CLASS
                    Return "BelowNormal"
                Case NORMAL_PRIORITY_CLASS
                    Return "Normal"
                Case ABOVE_NORMAL_PRIORITY_CLASS
                    Return "AboveNormal"
                Case HIGH_PRIORITY_CLASS
                    Return "High"
                Case REALTIME_PRIORITY_CLASS
                    Return "RealTime"
                Case Else
                    Return NO_INFO_RETRIEVED
            End Select
        End Get
    End Property

    ' Get priotity as an integer
    Public ReadOnly Property PriorityClassInt() As Integer
        Get
            Dim iP As Integer = 0

            If _hProcess > 0 Then
                iP = GetPriorityClass(_hProcess)
            End If

            Return iP
        End Get
    End Property

    ' Get priority as a level
    Public ReadOnly Property PriorityLevel() As Integer
        Get
            Select Case PriorityClassInt()
                Case 64
                    Return 1
                Case 16384
                    Return 2
                Case 32
                    Return 3
                Case 32768
                    Return 4
                Case 128
                    Return 5
                Case 256
                    Return 6
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    ' Get priority as a constant
    Public ReadOnly Property PriorityClassConstant() As ProcessPriorityClass
        Get
            Return CType(PriorityClassInt(), ProcessPriorityClass)
        End Get
    End Property

    ' Get average CPU usage
    Public ReadOnly Property AverageCpuUsage(Optional ByVal force As Boolean = False) As Double
        Get
            If force Then
                Dim i As Long = Date.Now.Ticks - Me.StartTime.Ticks
                If i > 0 Then
                    Return Me.ProcessorTimeLong / i
                Else
                    Return 0
                End If
            Else
                Return _averageCpuU
            End If
        End Get
    End Property

    ' Get CPU time
    Public ReadOnly Property CpuPercentageUsage(Optional ByVal force As Boolean = False) As Double
        Get
            If force Then
                Static oldDate As Long = Date.Now.Ticks
                Static oldProcTime As Long = Me.ProcessorTimeLong

                Dim currDate As Long = Date.Now.Ticks
                Dim proctime As Long = Me.ProcessorTimeLong

                Dim diff As Long = currDate - oldDate
                Dim procDiff As Long = proctime - oldProcTime

                oldProcTime = proctime
                oldDate = currDate

                If diff > 0 AndAlso _processors > 0 Then
                    Return procDiff / diff / _processors
                Else
                    Return 0
                End If
            Else
                Return _cpuUs
            End If
        End Get
    End Property

    Public ReadOnly Property HandleCount() As Integer
        Get
            If _hProcess > 0 Then
                Dim cnt As Integer
                Dim ret As Integer
                ZwQueryInformationProcess(_hProcess, PROCESSINFOCLASS.ProcessHandleCount, cnt, 4, ret)
                Return cnt
            End If
        End Get
    End Property

    ' Get the start time
    Public ReadOnly Property StartTime() As Date
        Get
            Dim T0 As Long
            Dim T1 As Long
            Dim curTime2 As Long
            Dim curTime As Long
            Dim r As Date

            If _hProcess > 0 Then
                GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                Dim p1 As New Date(T0 + DATETIME_DELTA)
                r = p1
            End If

            Return r
        End Get
    End Property

    Public ReadOnly Property MainModule() As System.Diagnostics.ProcessModule
        Get
            If _mainMod Is Nothing Then
                Try
                    Dim gProc As Process = Process.GetProcessById(_pid)
                    If gProc IsNot Nothing Then
                        _mainMod = gProc.MainModule
                    Else
                        _mainMod = Nothing
                    End If
                Catch ex As Exception
                    _mainMod = Nothing
                End Try
            End If
            Return _mainMod
        End Get
    End Property

    Public ReadOnly Property Modules() As System.Diagnostics.ProcessModuleCollection
        Get
            Dim gProc As Process = Process.GetProcessById(_pid)
            Return gProc.Modules
        End Get
    End Property


    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Refresh infos
    Public Sub Refresh()
        Static _refrehNumber As Integer = 0
        _refrehNumber += 1   ' This is the key

        ' Get date in ms
        Dim _now As Long = Date.Now.Ticks

        ' Refresh processor infos
        Dim T0 As Long
        Dim T1 As Long
        Dim curTime2 As Long
        Dim curTime As Long
        If _hProcess > 0 Then
            GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
            _kernelTime = curTime
            _userTime = curTime2
            _processorTime = _userTime + _kernelTime
        End If

        ' Refresh memory infos
        _memInfo = Me.MemoryInfos(True)

        ' Refresh IO infos
        _io = GetIOvalues(True)

        ' GDI and User objects count
        _userO = Me.UserObjectsCount(True)
        _gdiO = Me.GDIObjectsCount(True)

        ' CpuUsage
        _cpuUs = Me.CpuPercentageUsage(True)
        _averageCpuU = Me.AverageCpuUsage(True)

        ' Store informations
        _dicoProcMem.Add(_refrehNumber, New PROC_MEM_INFO(_now, _memInfo))
        _dicoProcTimes.Add(_refrehNumber, New PROC_TIME_INFO(_now, _userTime, _kernelTime))
        _dicoProcIO.Add(_refrehNumber, New PROC_IO_INFO(_now, _io))
        _dicoProcMisc.Add(_refrehNumber, New PROC_MISC_INFO(_now, _gdiO, _userO, _
                 100 * _cpuUs, 100 * _averageCpuU))

        RaiseEvent Refreshed()
    End Sub


    ' Suspend a process
    Public Function SuspendProcess() As Integer
        Dim hProc As Integer
        Dim r As Integer = -1

        hProc = OpenProcess(PROCESS_SUSPEND_RESUME, 0, _pid)

        If hProc > 0 Then
            r = NtSuspendProcess(hProc)
            CloseHandle(hProc)
        End If

        Return r

    End Function

    ' Resume a process
    Public Function ResumeProcess() As Integer
        Dim hProc As Integer
        Dim r As Integer = -1

        hProc = OpenProcess(PROCESS_SUSPEND_RESUME, 0, _pid)

        If hProc > 0 Then
            r = NtResumeProcess(hProc)
            CloseHandle(hProc)
        End If

        Return r

    End Function

    ' Set priority
    Public Function SetProcessPriority(ByVal level As ProcessPriorityClass) As Integer

        Dim hProc As Integer
        Dim r As Integer

        hProc = OpenProcess(PROCESS_SET_INFORMATION, 0, _pid)
        If hProc > 0 Then
            r = SetPriorityClass(hProc, level)
            CloseHandle(hProc)
        End If

        Return r
    End Function

    ' Kill a process
    Public Function Kill() As Integer
        Dim hProc As Integer
        Dim ret As Integer = -1
        hProc = OpenProcess(PROCESS_TERMINATE, 0, _pid)
        If hProc > 0 Then
            ret = TerminateProcess(hProc, 0)
            CloseHandle(hProc)
        End If
        Return ret
    End Function

    ' Kill a process tree
    Public Function KillProcessTree() As Integer

        ' Kill all childs recursively
        recursiveKill(_pid)

    End Function

    ' Empty working set size
    Public Function EmptyWorkingSetSize() As Integer
        ' Set (and not empty) will be implemented later
        Dim _hHandle As Integer = OpenProcess(PROCESS_SET_QUOTA, 0, _pid)
        Dim _ret As Integer = SetProcessWorkingSetSize(_hHandle, -1, -1)
        CloseHandle(_hHandle)
        Return _ret
    End Function

    ' Return environment variables
    Public Function GetEnvironmentVariables(ByRef variables() As String, _
                                            ByRef values() As String) As Integer

        ReDim variables(0)
        ReDim values(0)

        ' Get PEB address of process
        Dim __pebAd As Integer = Me.PEBAddress
        If __pebAd = -1 Then
            Return 0
        End If

        ' Create a processMemRW class to read in memory
        Dim cR As New cProcessMemRW(Pid)

        If cR.Handle = 0 Then
            Return 0              ' Couldn't open a handle
        End If

        ' Read first 20 bytes (5 integers) of PEB block
        ' The fifth integer contains address of ProcessParameters block
        Dim pebDeb() As Integer = cR.ReadBytesAI(__pebAd, 5)
        Dim __procParamAd As Integer = pebDeb(4)

        ' Get environnement block address
        ' It's located at offset 0x48 on all NT systems because it's after a fixed structure
        ' of 72 bytes
        Dim bA() As Integer = cR.ReadBytesAI(__procParamAd + 72, 1)
        Dim _envDeb As Integer = bA(0)      ' Get address


        ' ======= Read environnement block byte per byte to calculate env. block size
        ' Block is finished by a 4 consecutive null bytes (0)
        ' Short variables because it's unicode coded (2 bytes per char)
        Dim b1 As Short = -1
        Dim b2 As Short = -1
        Dim _size As Integer = 0

        ' Read mem until 4 null char (<==> 2 null shorts)
        Do While Not (b1 = 0 And b2 = 0)
            b1 = cR.Read2Bytes(_envDeb + _size)
            b2 = cR.Read2Bytes(_envDeb + _size + 2)
            _size += 2
        Loop

        ' Now we can get all env. variables from memory
        Dim blockEnv() As Short = cR.ReadBytesAS(_envDeb, _size)

        ' Parse these env. variables
        ' Env. variables are separated by 2 null bytes ( <==> 1 null short)
        Dim _envVar() As String
        Dim __var As String
        Dim xOld As Integer = 0
        ReDim _envVar(0)
        Dim y As Integer

        For x As Integer = 0 To CInt(_size / 2)

            If blockEnv(x) = 0 Then
                ' Then it's variable end
                ReDim Preserve _envVar(_envVar.Length)  ' Add one item to list
                Try
                    ' Parse short array to retrieve an unicode string
                    y = x * 2
                    Dim __size As Integer = CInt((y - xOld) / 2)

                    ' Allocate unmanaged memory
                    Dim ptr As IntPtr = Marshal.AllocHGlobal(y - xOld)

                    ' Copy from short array to unmanaged memory
                    Marshal.Copy(blockEnv, CInt(xOld / 2), ptr, __size)

                    ' Convert to string (and copy to __var variable)
                    __var = Marshal.PtrToStringUni(ptr, __size)

                    ' Free unmanaged memory
                    Marshal.FreeHGlobal(ptr)

                Catch ex As Exception
                    MsgBox(ex.Message)
                    __var = ""
                End Try

                ' Insert variable
                _envVar(_envVar.Length - 2) = __var

                xOld = y + 2
            End If
        Next

        ' Remove useless last nothing item
        ReDim Preserve _envVar(_envVar.Length - 2)

        ' Separate variables and values
        ReDim variables(_envVar.Length - 2)
        ReDim values(_envVar.Length - 2)

        For x As Integer = 0 To _envVar.Length - 2
            Dim i As Integer = InStr(_envVar(x), "=", CompareMethod.Binary)
            If i > 0 Then
                variables(x) = _envVar(x).Substring(0, i - 1)
                values(x) = _envVar(x).Substring(i, _envVar(x).Length - i)
            Else
                variables(x) = ""
                values(x) = ""
            End If
        Next

        Return _envVar.Length

    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal infoName As String) As String
        Dim res As String = NO_INFO_RETRIEVED
        Dim mem As cProcess.PROCESS_MEMORY_COUNTERS = _memInfo
        Select Case infoName
            Case "ParentPID"
                res = CStr(Me.ParentProcessId)
            Case "ParentName"
                res = Me.ParentProcessName
            Case "PID"
                res = CStr(Me.Pid)
            Case "UserName"
                res = Me.UserName
            Case "CpuUsage"
                res = GetFormatedPercentage(Me.CpuPercentageUsage)
            Case "KernelCpuTime"
                Dim ts As Date = Me.KernelTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "UserCpuTime"
                Dim ts As Date = Me.UserTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "TotalCpuTime"
                Dim ts As Date = Me.ProcessorTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "StartTime"
                Dim ts As Date = Me.StartTime
                res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
            Case "WorkingSet"
                res = GetFormatedSize(mem.WorkingSetSize)
            Case "PeakWorkingSet"
                res = GetFormatedSize(mem.PeakWorkingSetSize)
            Case "PageFaultCount"
                res = mem.PageFaultCount.ToString
            Case "PagefileUsage"
                res = GetFormatedSize(mem.PagefileUsage)
            Case "PeakPagefileUsage"
                res = GetFormatedSize(mem.PeakPagefileUsage)
            Case "QuotaPeakPagedPoolUsage"
                res = GetFormatedSize(mem.QuotaPeakPagedPoolUsage)
            Case "QuotaPagedPoolUsage"
                res = GetFormatedSize(mem.QuotaPagedPoolUsage)
            Case "QuotaPeakNonPagedPoolUsage"
                res = GetFormatedSize(mem.QuotaPeakNonPagedPoolUsage)
            Case "QuotaNonPagedPoolUsage"
                res = GetFormatedSize(mem.QuotaNonPagedPoolUsage)
            Case "Priority"
                res = Me.PriorityClass.ToString
            Case "Path"
                res = Me.Path
            Case "Description"
                Dim _fvi As FileVersionInfo = Me.FileVersionInfo
                If _fvi IsNot Nothing Then
                    res = _fvi.FileDescription
                Else
                    res = NO_INFO_RETRIEVED
                End If
            Case "Copyright"
                Dim _fvi As FileVersionInfo = Me.FileVersionInfo
                If _fvi IsNot Nothing Then
                    res = _fvi.LegalCopyright
                Else
                    res = NO_INFO_RETRIEVED
                End If
            Case "Version"
                Dim _fvi As FileVersionInfo = Me.FileVersionInfo
                If _fvi IsNot Nothing Then
                    res = _fvi.FileVersion
                Else
                    res = NO_INFO_RETRIEVED
                End If
            Case "Name"
                res = Me.Name
            Case "GdiObjects"
                res = Me.GDIObjectsCount.ToString
            Case "UserObjects"
                res = Me.UserObjectsCount.ToString
            Case "RunTime"
                Dim ts As New Date(Date.Now.Ticks - Me.StartTime.Ticks)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "AffinityMask"
                res = Me.AffinityMask.ToString
            Case "AverageCpuUsage"
                Dim i As Long = Date.Now.Ticks - Me.StartTime.Ticks
                If i > 0 AndAlso _processors > 0 Then
                    res = GetFormatedPercentage(Me.ProcessorTime.Ticks / i / _processors)
                Else
                    res = GetFormatedPercentage(0)
                End If
            Case "CommandLine"
                res = Me.CommandLine
            Case "ReadOperationCount"
                res = Me.GetIOvalues.ReadOperationCount.ToString
            Case "WriteOperationCount"
                res = Me.GetIOvalues.WriteOperationCount.ToString
            Case "OtherOperationCount"
                res = Me.GetIOvalues.OtherOperationCount.ToString
            Case "ReadTransferCount "
                res = GetFormatedSize(Me.GetIOvalues.ReadTransferCount)
            Case "WriteTransferCount"
                res = GetFormatedSize(Me.GetIOvalues.WriteTransferCount)
            Case "OtherTransferCount"
                res = GetFormatedSize(Me.GetIOvalues.OtherTransferCount)
        End Select

        Return res
    End Function

    ' Retrieve a long array with all available values from dictionnaries
    Public Function GetHistory(ByVal infoName As String) As Double()
        Dim ret() As Double

        Select Case infoName
            Case "KernelCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_TIME_INFO In _dicoProcTimes.Values
                    ret(x) = t.kernel
                    x += 1
                Next
            Case "UserCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_TIME_INFO In _dicoProcTimes.Values
                    ret(x) = t.user
                    x += 1
                Next
            Case "TotalCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_TIME_INFO In _dicoProcTimes.Values
                    ret(x) = t.total
                    x += 1
                Next
            Case "WorkingSet"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.WorkingSetSize
                    x += 1
                Next
            Case "PeakWorkingSet"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PeakWorkingSetSize
                    x += 1
                Next
            Case "PageFaultCount"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PageFaultCount
                    x += 1
                Next
            Case "PagefileUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PagefileUsage
                    x += 1
                Next
            Case "PeakPagefileUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PeakPagefileUsage
                    x += 1
                Next
            Case "QuotaPeakPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPeakPagedPoolUsage
                    x += 1
                Next
            Case "QuotaPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPagedPoolUsage
                    x += 1
                Next
            Case "QuotaPeakNonPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPeakNonPagedPoolUsage
                    x += 1
                Next
            Case "QuotaNonPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaNonPagedPoolUsage
                    x += 1
                Next
            Case "ReadOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.ReadOperationCount)
                    x += 1
                Next
            Case "WriteOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.WriteOperationCount)
                    x += 1
                Next
            Case "OtherOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.OtherOperationCount)
                    x += 1
                Next
            Case "ReadTransferCount "
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.ReadTransferCount)
                    x += 1
                Next
            Case "WriteTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.WriteTransferCount)
                    x += 1
                Next
            Case "OtherTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.OtherTransferCount)
                    x += 1
                Next
            Case "GdiObjects"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.gdiO
                    x += 1
                Next
            Case "UserObjects"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.userO
                    x += 1
                Next
            Case "CpuUsage"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.cpuUsage
                    x += 1
                Next
            Case "AverageCpuUsage"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.averageCpuUsage
                    x += 1
                Next
            Case Else
                ReDim ret(0)
        End Select

        Return ret
    End Function

    ' Retrieve informations by its name (numerical)
    Public Function GetInformationNumerical(ByVal infoName As String) As Double
        Dim mem As cProcess.PROCESS_MEMORY_COUNTERS = _memInfo
        Dim res As Double = 0

        Select Case infoName
            Case "KernelCpuTime"
                res = Me.KernelTime.Ticks
            Case "UserCpuTime"
                res = Me.UserTime.Ticks
            Case "TotalCpuTime"
                res = Me.ProcessorTime.Ticks
            Case "WorkingSet"
                res = mem.WorkingSetSize
            Case "PeakWorkingSet"
                res = mem.PeakWorkingSetSize
            Case "PageFaultCount"
                res = mem.PageFaultCount
            Case "PagefileUsage"
                res = mem.PagefileUsage
            Case "PeakPagefileUsage"
                res = mem.PeakPagefileUsage
            Case "QuotaPeakPagedPoolUsage"
                res = mem.QuotaPeakPagedPoolUsage
            Case "QuotaPagedPoolUsage"
                res = mem.QuotaPagedPoolUsage
            Case "QuotaPeakNonPagedPoolUsage"
                res = mem.QuotaPeakNonPagedPoolUsage
            Case "QuotaNonPagedPoolUsage"
                res = mem.QuotaNonPagedPoolUsage
            Case "GdiObjects"
                res = Me.GDIObjectsCount
            Case "UserObjects"
                res = Me.UserObjectsCount
            Case "ReadOperationCount"
                res = Me.GetIOvalues.ReadOperationCount
            Case "WriteOperationCount"
                res = Me.GetIOvalues.WriteOperationCount
            Case "OtherOperationCount"
                res = Me.GetIOvalues.OtherOperationCount
            Case "ReadTransferCount "
                res = Me.GetIOvalues.ReadTransferCount
            Case "WriteTransferCount"
                res = Me.GetIOvalues.WriteTransferCount
            Case "OtherTransferCount"
                res = Me.GetIOvalues.OtherTransferCount
            Case "CpuUsage"
                res = 100 * Me.CpuPercentageUsage
            Case "AverageCpuUsage"
                res = 100 * Me.AverageCpuUsage
        End Select

        Return res
    End Function


    ' ========================================
    ' Shared functions
    ' ========================================

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(34) As String

        s(0) = "PID"
        s(1) = "UserName"
        s(2) = "ParentPID"
        s(3) = "ParentName"
        s(4) = "CpuUsage"
        s(5) = "AverageCpuUsage"
        s(6) = "KernelCpuTime"
        s(7) = "UserCpuTime"
        s(8) = "TotalCpuTime"
        s(9) = "StartTime"
        s(10) = "RunTime"
        s(11) = "GdiObjects"
        s(12) = "UserObjects"
        s(13) = "AffinityMask"
        s(14) = "WorkingSet"
        s(15) = "PeakWorkingSet"
        s(16) = "PageFaultCount"
        s(17) = "PagefileUsage"
        s(18) = "PeakPagefileUsage"
        s(19) = "QuotaPeakPagedPoolUsage"
        s(20) = "QuotaPagedPoolUsage"
        s(21) = "QuotaPeakNonPagedPoolUsage"
        s(22) = "QuotaNonPagedPoolUsage"
        s(23) = "ReadOperationCount"
        s(24) = "WriteOperationCount"
        s(25) = "OtherOperationCount"
        s(26) = "ReadTransferCount "
        s(27) = "WriteTransferCount"
        s(28) = "OtherTransferCount"
        s(29) = "Priority"
        s(30) = "Path"
        s(31) = "CommandLine"
        s(32) = "Description"
        s(33) = "Copyright"
        s(34) = "Version"

        Return s
    End Function

    ' Retrieve process list
    ' This so much faster than VB.Net methods...
    Public Shared Function Enumerate(ByRef p() As String, ByRef _dico As  _
                                     Dictionary(Of String, LightProcess)) As Integer
        Dim hSnapshot As Integer
        Dim uProcess As ProcessEntry32 = Nothing
        Dim r As Integer
        Dim x As Integer

        _dico.Clear()

        ReDim p(100)    ' Preallocate a buffer of 400 bytes
        x = 0

        hSnapshot = CInt(CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0&))
        If hSnapshot <> 0 Then

            uProcess.dwSize = Marshal.SizeOf(uProcess)

            r = ProcessFirst(CType(hSnapshot, IntPtr), uProcess)

            Do While (r <> 0)

                p(x) = uProcess.th32ProcessID.ToString
                _dico.Add(p(x).ToString, New LightProcess(Integer.Parse(p(x)), uProcess.szExeFile))

                r = ProcessNext(CType(hSnapshot, IntPtr), uProcess)
                If r <> 0 Then
                    x += 1
                    If x > p.Length Then
                        ReDim Preserve p(x)
                    End If
                End If
            Loop

            Call CloseHandle(hSnapshot)

        End If

        ' Truncate array
        ReDim Preserve p(x)

        Return x

    End Function

    Public Shared Function Enumerate(ByRef _dico As Dictionary(Of String,  _
                                                               LightProcess)) As Integer
        Dim hSnapshot As Integer
        Dim uProcess As ProcessEntry32 = Nothing
        Dim r As Integer
        Dim x As Integer

        _dico.Clear()

        x = 0

        hSnapshot = CInt(CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0&))
        If hSnapshot <> 0 Then

            uProcess.dwSize = Marshal.SizeOf(uProcess)

            r = ProcessFirst(CType(hSnapshot, IntPtr), uProcess)

            Do While (r <> 0)
                _dico.Add(uProcess.th32ProcessID.ToString, New LightProcess( _
                          uProcess.th32ProcessID, uProcess.th32ParentProcessID))
                r = ProcessNext(CType(hSnapshot, IntPtr), uProcess)
            Loop

            Call CloseHandle(hSnapshot)

        End If

        Return x

    End Function

    ' Unload a module from a process
    Public Shared Function UnLoadModuleFromProcess(ByRef aModule As cModule.MODULEENTRY32) As Integer
        Return UnLoadModuleFromProcess(aModule.th32ProcessID, aModule.modBaseAddr)
    End Function
    Public Shared Function UnLoadModuleFromProcess(ByVal ProcessId As Integer, ByVal ModuleBaseAddress As Integer) As Integer

        Dim hProc As Integer = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE Or PROCESS_VM_READ, 0, ProcessId)
        Dim kernel32 As Integer = cProcess.GetModuleHandle("kernel32.dll")
        Dim freeLibrary As Integer = cProcess.GetProcAddress(kernel32, "FreeLibrary")
        Dim threadId As Integer

        Return cProcess.CreateRemoteThread(hProc, 0, 0, freeLibrary, ModuleBaseAddress, 0, threadId)
    End Function

    ' Return path
    Public Shared Function GetPath(ByVal pid As Integer) As String
        Dim s As String = vbNullString
        Dim lHprcss As Integer
        Dim Ret As Integer
        Dim sResult As String = Space(512)
        Dim hModule As Integer

        lHprcss = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, pid)

        If lHprcss > 0 Then
            Call EnumProcessModules(lHprcss, hModule, 4, Ret)
            sResult = Space(260)
            Call GetModuleFileNameExA(lHprcss, hModule, sResult, 260)
            CloseHandle(lHprcss)
            s = sResult
        End If

        If InStr(sResult, vbNullChar) > 1 Then
            sResult = Left(sResult, InStr(sResult, vbNullChar) - 1)
        End If

        Return sResult

    End Function

    ' Return Process name
    Public Shared Function GetProcessName(ByVal pid As Integer) As String
        Select Case pid
            Case 0
                Return "[System Process]"
            Case 4
                Return "System"
            Case Else
                If _procs.ContainsKey(pid.ToString) Then
                    Return _procs.Item(pid.ToString)
                Else
                    Return cFile.GetFileName(GetPath(pid))
                End If
        End Select
    End Function

    ' Kill a process
    Public Shared Function Kill(ByVal pid As Integer) As Integer
        Dim hProc As Integer
        Dim ret As Integer = -1
        hProc = OpenProcess(PROCESS_TERMINATE, 0, pid)
        If hProc > 0 Then
            ret = TerminateProcess(hProc, 0)
            CloseHandle(hProc)
        End If
        Return ret
    End Function

    ' Add/remove a process to dictionnary
    Public Shared Sub AssociatePidAndName(ByVal pid As String, ByVal name As String)
        If _procs.ContainsKey(pid) = False Then
            _procs.Add(pid, name)
        End If
    End Sub
    Public Shared Sub UnAssociatePidAndName(ByVal pid As String)
        _procs.Remove(pid)
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================

    ' For 'Kill process tree'
    Private Function recursiveKill(ByVal pid As Integer) As Integer

        ' Kill process
        Call Kill(pid)

        ' Get all items...
        Dim _dicoFull As New Dictionary(Of String, LightProcess)
        Dim _dico2 As New Dictionary(Of String, LightProcess)
        cProcess.Enumerate(_dicoFull)

        ' ... and extract only childs
        For Each t As LightProcess In _dicoFull.Values
            If t.parentPid = pid Then
                _dico2.Add(t.pid.ToString, t)
            End If
        Next

        ' Recursive kill
        For Each t As LightProcess In _dico2.Values
            recursiveKill(t.pid)
        Next

    End Function

    Private Function GetCommandLine(ByVal pid As Integer) As String

        Dim res As String = ""

        ' Get PEB address of process
        ' Get PEB address of process
        Dim __pebAd As Integer = Me.PEBAddress
        If __pebAd = -1 Then
            Return ""
        End If

        ' Create a processMemRW class to read in memory
        Dim cR As New cProcessMemRW(pid)

        If cR.Handle = 0 Then
            Return ""           ' Couldn't open a handle
        End If

        ' Read first 20 bytes (5 integers) of PEB block
        ' The fifth integer contains address of ProcessParameters block
        Dim pebDeb() As Integer = cR.ReadBytesAI(__pebAd, 5)
        Dim __procParamAd As Integer = pebDeb(4)

        ' Get unicode string adress
        ' It's located at offset 0x40 on all NT systems because it's after a fixed structure
        ' of 64 bytes

        ' Read length of the unicode string
        Dim bA() As Short = cR.ReadBytesAS(__procParamAd + 64, 1)
        Dim __size As Integer = bA(0)      ' Size of string
        If __size = 0 Then
            Return "N/A"
        End If

        ' Read pointer to the string
        Dim bA2() As Integer = cR.ReadBytesAI(__procParamAd + 68, 1)
        Dim __strPtr As Integer = bA2(0)      ' Pointer to string

        'Trace.WriteLine("before string")
        ' Gonna get string
        Dim bS() As Short = cR.ReadBytesAS(__strPtr, __size)

        ' Allocate unmanaged memory
        Dim ptr As IntPtr = Marshal.AllocHGlobal(__size)
        __size = CInt(__size / 2)   ' Because of Unicode String (2 bytes per char)

        ' Copy from short array to unmanaged memory
        Marshal.Copy(bS, 0, ptr, __size)

        ' Convert to string (and copy to __var variable)
        res = Marshal.PtrToStringUni(ptr, __size)

        ' Free unmanaged memory
        Marshal.FreeHGlobal(ptr)

        Return res

    End Function

    ' Return process name from pid
    Private Function getSimpleName(ByVal pid As Integer) As String
        Dim hSnapshot As Integer
        Dim uProcess As ProcessEntry32 = Nothing
        Dim r As Integer

        hSnapshot = CInt(CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0&))
        If hSnapshot <> 0 Then

            uProcess.dwSize = Marshal.SizeOf(uProcess)

            r = ProcessFirst(CType(hSnapshot, IntPtr), uProcess)

            Do While (r <> 0)

                If uProcess.th32ProcessID = pid Then
                    Call CloseHandle(hSnapshot)
                    Return CStr(uProcess.szExeFile)
                End If

                r = ProcessNext(CType(hSnapshot, IntPtr), uProcess)
            Loop

            Call CloseHandle(hSnapshot)

        End If

        Return ""
    End Function

    Private Function GetImagePath_MemMethod(ByVal pid As Integer) As String

        Dim res As String = ""

        ' Get PEB address of process
        Dim __pebAd As Integer = Me.PEBAddress
        If __pebAd = -1 Then
            Return ""
        End If

        ' Create a processMemRW class to read in memory
        Dim cR As New cProcessMemRW(pid)

        If cR.Handle = 0 Then
            Return ""           ' Couldn't open a handle
        End If

        ' Read first 20 bytes (5 integers) of PEB block
        ' The fifth integer contains address of ProcessParameters block
        Dim pebDeb() As Integer = cR.ReadBytesAI(__pebAd, 5)
        Dim __procParamAd As Integer = pebDeb(4)


        ' Get unicode string adress
        ' It's located at offset 0x38 on all NT systems because it's after a fixed structure
        ' of 56 bytes

        ' Read length of the unicode string
        Dim bA() As Short = cR.ReadBytesAS(__procParamAd + 56, 1)
        Dim __size As Integer = bA(0)      ' Size of string

        ' Read pointer to the string
        Dim bA2() As Integer = cR.ReadBytesAI(__procParamAd + 60, 1)
        Dim __strPtr As Integer = bA2(0)      ' Pointer to string

        'Trace.WriteLine("before string")
        ' Gonna get string
        Dim bS() As Short = cR.ReadBytesAS(__strPtr, __size)

        ' Allocate unmanaged memory
        Dim ptr As IntPtr = Marshal.AllocHGlobal(__size)
        __size = CInt(__size / 2)   ' Because of Unicode String (2 bytes per char)

        ' Copy from short array to unmanaged memory
        Marshal.Copy(bS, 0, ptr, __size)

        ' Convert to string (and copy to __var variable)
        res = Marshal.PtrToStringUni(ptr, __size)

        ' Free unmanaged memory
        Marshal.FreeHGlobal(ptr)

        Return res

    End Function

    Private Function PriorityFromInt(ByVal i As Integer) As String
        Dim s As String = vbNullString

        Select Case i
            Case 64
                s = "Idle"
            Case 16384
                s = "Below Normal"
            Case 32
                s = "Normal"
            Case 32768
                s = "Above Normal"
            Case 128
                s = "High"
            Case 256
                s = "Real Time"
            Case Else
                s = NO_INFO_RETRIEVED
        End Select

        Return s
    End Function

    ' Get an account name from a SID
    Private Function GetAccountName(ByVal SID As Integer, ByVal IncludeDomain As Boolean) As String
        Dim name As New StringBuilder(255)
        Dim domain As New StringBuilder(255)
        Dim namelen As Integer = 255
        Dim domainlen As Integer = 255
        Dim use As SID_NAME_USE = SID_NAME_USE.SidTypeUser

        Try
            If Not LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use) Then
                name.EnsureCapacity(namelen)
                domain.EnsureCapacity(domainlen)
                LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use)
            End If
        Catch
            ' return string SID
            Return New System.Security.Principal.SecurityIdentifier(New IntPtr(SID)).ToString()
        End Try

        If IncludeDomain Then
            Return CStr(IIf(domain.ToString <> "", domain.ToString & "\", "")) & name.ToString
        Else
            Return name.ToString()
        End If
    End Function

    ' Return the path of a process from its pid
    Private Function GetImageFile(ByVal pid As Integer) As String

        If _pid > 4 Then

            ' Have to open a handle
            Dim _h As Integer = OpenProcess(&H1000, 0, pid) ' Limited rights

            If _h > 0 Then
                ' Get size
                Dim _size As Integer
                NtQueryInformationProcess(_h, 27, IntPtr.Zero, 0, _size)
                If _size = 0 Then
                    ' Try to get more rights (XP)
                    _h = OpenProcess(PROCESS_QUERY_INFORMATION, 0, pid)
                    NtQueryInformationProcess(_h, 27, IntPtr.Zero, 0, _size)
                    If _size = 0 Then
                        Return "??"
                    End If
                End If

                ' Retrieve unicode string
                Dim _pt As IntPtr = Marshal.AllocHGlobal(_size)
                NtQueryInformationProcess(_h, 27, _pt, _size, _size)
                Dim _str As UNICODE_STRING = CType(Marshal.PtrToStructure(_pt, _
                                                                         GetType(UNICODE_STRING)), UNICODE_STRING)
                Marshal.FreeHGlobal(_pt)
                CloseHandle(_h)

                ' Return string (replace /DEVICE/... by the dos file name)
                Dim _stemp As String = GetStringFromUNICODE_STRING(_str)
                If _stemp IsNot Nothing Then
                    Return DeviceDriveNameToDosDriveName(_stemp)
                Else
                    Return "??"
                End If
            Else
                Return "??"
            End If

        ElseIf _pid = 4 Then
            Return "SYSTEM"
        Else
            Return "[System process]"
        End If

    End Function

    ' Return dos drive name
    Private Shared Function DeviceDriveNameToDosDriveName(ByVal drivePath As String) As String

        For Each pair As System.Collections.Generic.KeyValuePair(Of String, String) In _DicoLogicalDrivesNames
            If drivePath.StartsWith(pair.Key) Then
                Return pair.Value + drivePath.Substring(pair.Key.Length)
            End If
        Next
        Return drivePath

    End Function

    ' Refresh the dictionnary of logical drives
    Private Shared Sub RefreshLogicalDrives()

        Dim _tempDico As Dictionary(Of String, String) = New Dictionary(Of String, String)

        ' From 'A' to 'Z'
        ' It also possible to use GetLogicalDriveStringsA
        For c As Byte = 65 To 90
            Dim _badPath As New StringBuilder(1024)

            If QueryDosDevice(Char.ConvertFromUtf32(c) & ":", _badPath, 1024) <> 0 Then
                _tempDico.Add(_badPath.ToString(), Char.ConvertFromUtf32(c).ToString() & ":")
            End If
        Next

        _DicoLogicalDrivesNames = _tempDico
    End Sub

    ' Return a string from a UNICODE_STRING structure
    Private Shared Function GetStringFromUNICODE_STRING(ByVal _str As UNICODE_STRING) As String
        If _str.Length > 0 Then
            Try
                Return Marshal.PtrToStringUni(New IntPtr(_str.Buffer), _
                                              CInt(_str.Length / 2))
            Catch ex As Exception
                Return ex.Message & "   " & _str.Buffer & "   " & _str.Length
            End Try
        Else
            Return Nothing
        End If
    End Function
End Class
