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
' The following definitions are from Process Hacker by Wj32 :
' - SystemProcessInformation
' - PebLdrData
' - LdrDataTableEntry
' - RtlUserProcessParameters
' - Peb
' - TokenPrivileges
' - TokenSource
' - TokenStatistics
' - SystemPerformanceInformation
' - SystemCacheInformation
' - SystemHandleInformation
' - EnumServiceStatusProcess
' - ServiceStatusProcess
' - ObjectTypeInformation

Option Strict On

Imports System.Runtime.InteropServices
Imports YAPM.Native.Api.NativeEnums

Namespace Native.Api

    Public Class NativeStructs

#Region "Offsets for native structures"

        Public Shared ReadOnly Peb_ProcessHeapOffset As IntPtr = Marshal.OffsetOf(GetType(Peb), "ProcessHeap")
        Public Shared ReadOnly Peb_ProcessParametersOffset As IntPtr = Marshal.OffsetOf(GetType(Peb), "ProcessParameters")
        Public Shared ReadOnly Peb_LoaderDataOffset As IntPtr = Marshal.OffsetOf(GetType(Peb), "LoaderData")
        Public Shared ReadOnly ProcParamBlock_CommandLineOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "CommandLine").ToInt32
        Public Shared ReadOnly ProcParamBlock_EnvOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "Environment").ToInt32

#End Region

#Region "Declarations used for processes"

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemProcessInformation
            Public NextEntryOffset As Integer
            Public NumberOfThreads As Integer
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
            Public Reserved1 As Long()
            Public CreateTime As Long
            Public UserTime As Long
            Public KernelTime As Long
            Public ImageName As UnicodeString
            Public BasePriority As Integer
            ' This two variables are private cause we prefer
            ' access to ProcessId and Inherited...Id as Int32
            Private _ProcessId As IntPtr
            Private _InheritedFromProcessId As IntPtr
            Public HandleCount As Integer
            Public SessionId As Integer
            Public PageDirectoryBase As IntPtr
            Public VirtualMemoryCounters As VmCountersEx
            Public IoCounters As IoCounters

            ' 2 properties to access to private variables
            Public Property ProcessId() As Integer
                Get
                    Return _ProcessId.ToInt32
                End Get
                Set(ByVal value As Integer)
                    _ProcessId = New IntPtr(value)
                End Set
            End Property
            Public Property InheritedFromProcessId() As Integer
                Get
                    Return _InheritedFromProcessId.ToInt32
                End Get
                Set(ByVal value As Integer)
                    _InheritedFromProcessId = New IntPtr(value)
                End Set
            End Property

        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure PebLdrData
            Public Length As Integer
            <MarshalAs(UnmanagedType.I1)> _
            Public Initialized As Boolean
            Public SsHandle As IntPtr
            Public InLoadOrderModuleList As ListEntry
            Public InMemoryOrderModuleList As ListEntry
            Public InInitializationOrderModuleList As ListEntry
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure LdrDataTableEntry
            Public InLoadOrderLinks As ListEntry
            Public InMemoryOrderLinks As ListEntry
            Public InInitializationOrderLinks As ListEntry
            Public DllBase As IntPtr
            Public EntryPoint As IntPtr
            Public SizeOfImage As Integer
            Public FullDllName As UnicodeString
            Public BaseDllName As UnicodeString
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
        Public Structure RtlUserProcessParameters
            Public Shared ReadOnly CurrentDirectoryOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "CurrentDirectory").ToInt32()
            Public Shared ReadOnly DllPathOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "DllPath").ToInt32()
            Public Shared ReadOnly ImagePathNameOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "ImagePathName").ToInt32()
            Public Shared ReadOnly CommandLineOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "CommandLine").ToInt32()
            Public Shared ReadOnly EnvironmentOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "Environment").ToInt32()
            Public Shared ReadOnly WindowTitleOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "WindowTitle").ToInt32()
            Public Shared ReadOnly DesktopInfoOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "DesktopInfo").ToInt32()
            Public Shared ReadOnly ShellInfoOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "ShellInfo").ToInt32()
            Public Shared ReadOnly RuntimeDataOffset As Integer = Marshal.OffsetOf(GetType(RtlUserProcessParameters), "RuntimeData").ToInt32()

            Public Structure CurDir
                Public DosPath As UnicodeString
                Public Handle As IntPtr
            End Structure

            Public Structure RtlDriveLetterCurDir
                Public Flags As UShort
                Public Length As UShort
                Public TimeStamp As UInteger
                Public DosPath As IntPtr
            End Structure

            Public MaximumLength As Integer
            Public Length As Integer

            Public Flags As RtlUserProcessFlags
            Public DebugFlags As Integer

            Public ConsoleHandle As IntPtr
            Public ConsoleFlags As Integer
            Public StandardInput As IntPtr
            Public StandardOutput As IntPtr
            Public StandardError As IntPtr

            Public CurrentDirectory As CurDir
            Public DllPath As UnicodeString
            Public ImagePathName As UnicodeString
            Public CommandLine As UnicodeString
            Public Environment As IntPtr

            Public StartingX As Integer
            Public StartingY As Integer
            Public CountX As Integer
            Public CountY As Integer
            Public CountCharsX As Integer
            Public CountCharsY As Integer
            Public FillAttribute As Integer

            Public WindowFlags As Integer
            Public ShowWindowFlags As Integer
            Public WindowTitle As UnicodeString
            Public DesktopInfo As UnicodeString
            Public ShellInfo As UnicodeString
            Public RuntimeData As UnicodeString

            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)> _
            Public CurrentDirectories As RtlDriveLetterCurDir()
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure Peb
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
            Public LoaderData As IntPtr             ' pointer to ldrData
            Public ProcessParameters As IntPtr      ' pointer to RtlUserProcessParameters struct
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
        Public Structure ProcessBasicInformation
            Public ExitStatus As UInteger
            Public PebBaseAddress As IntPtr
            Public AffinityMask As IntPtr
            Public BasePriority As Integer
            Public UniqueProcessId As IntPtr
            Public InheritedFromUniqueProcessId As IntPtr
        End Structure

        <StructLayout(LayoutKind.Sequential), Serializable()> _
        Public Structure IoCounters
            Public ReadOperationCount As ULong
            Public WriteOperationCount As ULong
            Public OtherOperationCount As ULong
            Public ReadTransferCount As ULong
            Public WriteTransferCount As ULong
            Public OtherTransferCount As ULong
            Public Shared Operator <>(ByVal i1 As IoCounters, ByVal i2 As IoCounters) As Boolean
                Return Not (i1 = i2)
            End Operator
            Public Shared Operator =(ByVal i1 As IoCounters, ByVal i2 As IoCounters) As Boolean
                Return (i1.ReadOperationCount = i2.ReadOperationCount AndAlso _
                    i1.WriteOperationCount = i2.WriteOperationCount AndAlso _
                    i1.OtherOperationCount = i2.OtherOperationCount AndAlso _
                    i1.ReadTransferCount = i2.ReadTransferCount AndAlso _
                    i1.WriteTransferCount = i2.WriteTransferCount AndAlso _
                    i1.OtherTransferCount = i2.OtherTransferCount)
            End Operator
        End Structure

        <StructLayout(LayoutKind.Sequential), Serializable()> _
        Public Structure VmCountersEx
            Public PeakVirtualSize As IntPtr
            Public VirtualSize As IntPtr
            Public PageFaultCount As Integer
            Public PeakWorkingSetSize As IntPtr
            Public WorkingSetSize As IntPtr
            Public QuotaPeakPagedPoolUsage As IntPtr
            Public QuotaPagedPoolUsage As IntPtr
            Public QuotaPeakNonPagedPoolUsage As IntPtr
            Public QuotaNonPagedPoolUsage As IntPtr
            Public PagefileUsage As IntPtr
            Public PeakPagefileUsage As IntPtr
            Public PrivateBytes As IntPtr
            Public Shared Operator <>(ByVal m1 As VmCountersEx, ByVal m2 As VmCountersEx) As Boolean
                Return Not (m1 = m2)
            End Operator
            Public Shared Operator =(ByVal i1 As VmCountersEx, ByVal i2 As VmCountersEx) As Boolean
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

#End Region

        ' OK
#Region "Declarations used for modules"

        '<StructLayout(LayoutKind.Sequential)> _
        'Public Structure MODULEINFO
        '    Public BaseOfDll As IntPtr
        '    Public SizeOfImage As Integer
        '    Public EntryPoint As IntPtr
        'End Structure

#End Region

        ' OK
#Region "Declarations used for memory management"

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure MemoryBasicInformation
            Public BaseAddress As IntPtr
            Public AllocationBase As IntPtr
            Public AllocationProtect As MemoryProtectionType
            Public RegionSize As IntPtr
            Public State As MemoryState
            Public Protect As MemoryProtectionType
            Public Type As MemoryType
        End Structure

#End Region

        ' OK
#Region "Declarations used for threads"

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ThreadBasicInformation
            Public ExitStatus As UInteger
            Public TebBaseAddress As IntPtr
            Public ClientId As ClientId
            Public AffinityMask As IntPtr
            Public Priority As Integer
            Public BasePriority As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemThreadInformation
            Public KernelTime As Long
            Public UserTime As Long
            Public CreateTime As Long
            Public WaitTime As Integer
            Public StartAddress As IntPtr
            Public ClientId As ClientId
            Public Priority As Integer
            Public BasePriority As Integer
            Public ContextSwitchCount As Integer
            Public State As System.Diagnostics.ThreadState
            Public WaitReason As KwaitReason
        End Structure

#End Region

        ' OK
#Region "Declarations used for tokens & privileges"

        Public Structure TokenPrivileges
            Dim PrivilegeCount As Integer
            <MarshalAs(UnmanagedType.ByValArray)> _
            Dim Privileges() As LuidAndAttributes
        End Structure

        Public Structure Luid
            Dim lowpart As Integer
            Dim highpart As Integer
        End Structure

        Public Structure LuidAndAttributes
            Dim pLuid As Luid
            Dim Attributes As NativeEnums.SePrivilegeAttributes
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ClientId
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

        Public Structure TokenUser
            Dim User As SidAndAttributes
        End Structure

        Public Structure SidAndAttributes
            Dim Sid As IntPtr
            Dim Attributes As Integer
        End Structure

        Public Structure PrivilegeInfo
            Dim Name As String
            Dim Status As SePrivilegeAttributes
            Dim pLuid As Luid
        End Structure

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
        Public Structure TokenSource
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)> _
            Public SourceName As String
            Public SourceIdentifier As Luid
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure TokenStatistics
            Public TokenId As Luid
            Public AuthenticationId As Luid
            Public ExpirationTime As Long
            Public TokenType As TokenType
            Public ImpersonationLevel As SecurityImpersonationLevel
            Public DynamicCharged As Integer
            Public DynamicAvailable As Integer
            Public GroupCount As Integer
            Public PrivilegeCount As Integer
            Public ModifiedId As Luid
        End Structure
#End Region

        ' OK
#Region "Declarations used for network"

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

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure MibTcpRow
            Public State As Enums.MibTcpState
            Public LocalAddress As UInteger
            Public LocalPort As Integer
            Public RemoteAddress As UInteger
            Public RemotePort As Integer
        End Structure

#End Region

#Region "Declarations used for files"

        Public Structure FileTime
            Dim dwLowDateTime As Integer
            Dim dwHighDateTime As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
        Public Structure ShFileOpStruct
            Public hwnd As IntPtr
            Public wFunc As FO_Func
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public pFrom As String
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public pTo As String
            Public fFlags As Short
            Public fAnyOperationsAborted As Boolean
            Public hNameMappings As IntPtr
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public lpszProgressTitle As String '  only used if FOF_SIMPLEPROGRESS
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemTime
            <MarshalAs(UnmanagedType.U2)> Public Year As Short
            <MarshalAs(UnmanagedType.U2)> Public Month As Short
            <MarshalAs(UnmanagedType.U2)> Public DayOfWeek As Short
            <MarshalAs(UnmanagedType.U2)> Public Day As Short
            <MarshalAs(UnmanagedType.U2)> Public Hour As Short
            <MarshalAs(UnmanagedType.U2)> Public Minute As Short
            <MarshalAs(UnmanagedType.U2)> Public Second As Short
            <MarshalAs(UnmanagedType.U2)> Public Milliseconds As Short
        End Structure

        Public Structure ByHandleFileInformation
            Dim dwFileAttributes As Long
            Dim ftCreationTime As FileTime
            Dim ftLastAccessTime As FileTime
            Dim ftLastWriteTime As FileTime
            Dim dwVolumeSerialNumber As Integer
            Dim nFileSizeHigh As Integer
            Dim nFileSizeLow As Integer
            Dim nNumberOfLinks As Integer
            Dim nFileIndexHigh As Integer
            Dim nFileIndexLow As Integer
        End Structure

        Public Structure ShellExecuteInfo
            Public cbSize As Integer
            Public fMask As ShellExecuteInfoMask
            Public hwnd As IntPtr
            <MarshalAs(UnmanagedType.LPTStr)> Public lpVerb As String
            <MarshalAs(UnmanagedType.LPTStr)> Public lpFile As String
            <MarshalAs(UnmanagedType.LPTStr)> Public lpParameters As String
            <MarshalAs(UnmanagedType.LPTStr)> Public lpDirectory As String
            Dim nShow As ShowWindowType
            Dim hInstApp As IntPtr
            Dim lpIDList As IntPtr
            <MarshalAs(UnmanagedType.LPTStr)> Public lpClass As String
            Public hkeyClass As IntPtr
            Public dwHotKey As Integer
            Public hIcon As IntPtr
            Public hProcess As IntPtr
        End Structure

        Public Structure SHFileInfo
            Public hIcon As IntPtr            ' : icon
            Public iIcon As IntPtr            ' : icondex
            Public dwAttributes As Integer    ' : SFGAO_ flags
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
            Public szDisplayName As String
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
            Public szTypeName As String
        End Structure

#End Region

#Region "Declarations used for system"

        <StructLayout(LayoutKind.Sequential)> _
        Public Class MSLLHookStruct
            Public pt As Point
            Public mouseData As Integer
            Public flags As Integer
            Public time As Integer
            Public dwExtraInfo As IntPtr
        End Class

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemInfo
            Friend uProcessorInfo As ProcessorInfoUnion
            Public dwPageSize As UInteger
            Public lpMinimumApplicationAddress As IntPtr
            Public lpMaximumApplicationAddress As IntPtr
            Public dwActiveProcessorMask As IntPtr
            Public dwNumberOfProcessors As UInteger
            Public dwProcessorType As UInteger
            Public dwAllocationGranularity As UInteger
            Public dwProcessorLevel As UShort
            Public dwProcessorRevision As UShort
        End Structure

        <StructLayout(LayoutKind.Explicit)> _
        Public Structure ProcessorInfoUnion
            <FieldOffset(0)> _
            Friend dwOemId As UInteger
            <FieldOffset(0)> _
            Friend wProcessorArchitecture As UShort
            <FieldOffset(2)> _
            Friend wReserved As UShort
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure PerformanceInformation
            Public Size As Integer
            Public CommitTotal As Integer
            Public CommitLimit As Integer
            Public CommitPeak As Integer
            Public PhysicalTotal As Integer
            Public PhysicalAvailable As Integer
            Public SystemCache As Integer
            Public KernelTotal As Integer
            Public KernelPaged As Integer
            Public KernelNonPaged As Integer
            Public PageSize As Integer
            Public HandlesCount As Integer
            Public ProcessCount As Integer
            Public ThreadCount As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemPerformanceInformation
            ''' <summary>
            ''' The total idle time of all processors in units of 100-nanoseconds.
            ''' </summary>
            Public IdleTime As Long

            ''' <summary>
            ''' Total bytes read by calls to NtReadFile.
            ''' </summary>
            Public IoReadTransferCount As Long

            ''' <summary>
            ''' Total bytes written by calls to NtWriteFile.
            ''' </summary>
            Public IoWriteTransferCount As Long

            ''' <summary>
            ''' Total bytes transferred by other I/O operations.
            ''' </summary>
            Public IoOtherTransferCount As Long

            ''' <summary>
            ''' Number of calls to NtReadFile.
            ''' </summary>
            Public IoReadOperationCount As Integer

            ''' <summary>
            ''' Number of calls to NtWriteFile.
            ''' </summary>
            Public IoWriteOperationCount As Integer

            ''' <summary>
            ''' Number of calls to other I/O functions.
            ''' </summary>
            Public IoOtherOperationCount As Integer

            ''' <summary>
            ''' The number of pages of physical memory available.
            ''' </summary>
            Public AvailablePages As Integer

            ''' <summary>
            ''' The number of pages of committed virtual memory.
            ''' </summary>
            Public CommittedPages As Integer

            ''' <summary>
            ''' The number of pages of virtual memory that could be committed 
            ''' without extending the system's pagefiles.
            ''' </summary>
            Public CommitLimit As Integer

            ''' <summary>
            ''' The peak number of pages of committed virtual memory.
            ''' </summary>
            Public PeakCommitment As Integer

            ''' <summary>
            ''' The total number of soft and hard page faults.
            ''' </summary>
            Public PageFaults As Integer

            ''' <summary>
            ''' The number of copy-on-write page faults.
            ''' </summary>
            Public CopyOnWriteFaults As Integer

            ''' <summary>
            ''' The number of soft page faults.
            ''' </summary>
            Public TransitionFaults As Integer

            ''' <summary>
            ''' Something that the Native API reference book doesn't have.
            ''' </summary>
            Public CacheTransitionFaults As Integer

            ''' <summary>
            ''' The number of demand zero faults.
            ''' </summary>
            Public DemandZeroFaults As Integer

            ''' <summary>
            ''' The number of pages read from disk to resolve page faults.
            ''' </summary>
            Public PagesRead As Integer

            ''' <summary>
            ''' The number of read operations initiated to resolve page faults.
            ''' </summary>
            Public PagesReadIos As Integer

            Public CacheRead As Integer
            Public CacheReadIos As Integer

            ''' <summary>
            ''' The number of pages written to the system's pagefiles.
            ''' </summary>
            Public PagefilePagesWritten As Integer

            ''' <summary>
            ''' The number of write operations performed on the system's pagefiles.
            ''' </summary>
            Public PagefilePagesWriteIos As Integer

            ''' <summary>
            ''' The number of pages written to mapped files.
            ''' </summary>
            Public MappedFilePagesWritten As Integer

            ''' <summary>
            ''' The number of write operations performed on mapped files.
            ''' </summary>
            Public MappedFilePageWriteIos As Integer

            ''' <summary>
            ''' The number of pages used by the paged pool.
            ''' </summary>
            Public PagedPoolUsage As Integer

            ''' <summary>
            ''' The number of pages used by the non-paged pool.
            ''' </summary>
            Public NonPagedPoolUsage As Integer

            ''' <summary>
            ''' The number of allocations made from the paged pool.
            ''' </summary>
            Public PagedPoolAllocs As Integer

            ''' <summary>
            ''' The number of allocations returned to the paged pool.
            ''' </summary>
            Public PagedPoolFrees As Integer

            ''' <summary>
            ''' The number of allocations made from the non-paged pool.
            ''' </summary>
            Public NonPagedPoolAllocs As Integer

            ''' <summary>
            ''' The number of allocations returned to the non-paged pool.
            ''' </summary>
            Public NonPagedPoolFrees As Integer

            ''' <summary>
            ''' The number of available System Page Table Entries.
            ''' </summary>
            Public FreeSystemPtes As Integer

            ''' <summary>
            ''' The number of pages of pageable OS code and data in physical 
            ''' memory.
            ''' </summary>
            Public SystemCodePages As Integer

            ''' <summary>
            ''' The number of pages of pageable driver code and data.
            ''' </summary>
            Public TotalSystemDriverPages As Integer

            ''' <summary>
            ''' The number of pages of OS driver code and data.
            ''' </summary>
            Public TotalSystemCodePages As Integer

            ''' <summary>
            ''' The number of times an allocation could be statisfied by one of the 
            ''' small non-paged lookaside lists.
            ''' </summary>
            Public SmallNonPagedPoolLookasideListAllocateHits As Integer

            ''' <summary>
            ''' The number of times an allocation could be statisfied by one of the 
            ''' small paged lookaside lists.
            ''' </summary>
            Public SmallPagedPoolLookasideAllocateHits As Integer

            Public Reserved3 As Integer

            ''' <summary>
            ''' The number of pages of the system cache in physical memory.
            ''' </summary>
            Public SystemCachePages As Integer

            ''' <summary>
            ''' The number of pages of the paged pool in physical memory.
            ''' </summary>
            Public PagedPoolPages As Integer

            ''' <summary>
            ''' The number of pages of pageable driver code and data in physical memory.
            ''' </summary>
            Public SystemDriverPages As Integer

            ''' <summary>
            ''' The number of asynchronous fast read operations.
            ''' </summary>
            Public FastReadNoWait As Integer

            ''' <summary>
            ''' The number of synchronous fast read operations.
            ''' </summary>
            Public FastReadWait As Integer

            ''' <summary>
            ''' The number of fast read operations not possible because of resource 
            ''' conflicts.
            ''' </summary>
            Public FastReadResourceMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public FastReadNotPossible As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public FastMdlReadNoWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public FastMdlReadWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public FastMdlReadResourceMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public FastMdlReadNotPossible As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MapDataNoWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MapDataWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MapDataNoWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MapDataWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public PinMappedDataCount As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public PinReadNoWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public PinReadWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public PinReadNoWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public PinReadWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public CopyReadNoWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public CopyReadWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public CopyReadNoWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public CopyReadWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MdlReadNoWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MdlReadWait As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MdlReadNoWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public MdlReadWaitMiss As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public ReadAheadIos As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public LazyWriteIos As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public LazyWritePages As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public DataFlushes As Integer

            ''' <remarks>
            ''' Google Books won't let me read the page containing the description 
            ''' for this field!
            ''' </remarks>
            Public DataPages As Integer

            ''' <summary>
            ''' The total number of context switches.
            ''' </summary>
            Public ContextSwitches As Integer

            ''' <summary>
            ''' The number of first level translation buffer fills.
            ''' </summary>
            Public FirstLevelTbFills As Integer

            ''' <summary>
            ''' The number of second level translation buffer fills.
            ''' </summary>
            Public SecondLevelTbFills As Integer

            ''' <summary>
            ''' The number of system calls executed.
            ''' </summary>
            Public SystemCalls As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemCacheInformation
            Public SystemCacheWsSize As Integer
            Public SystemCacheWsPeakSize As Integer
            Public SystemCacheWsFaults As Integer
            Public SystemCacheWsMinimum As Integer
            Public SystemCacheWsMaximum As Integer
            Public TransitionSharedPages As Integer
            Public TransitionSharedPagesPeak As Integer
            Public Reserved1 As Integer
            Public Reserved2 As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemProcessorPerformanceInformation
            Public IdleTime As Long
            Public KernelTime As Long
            Public UserTime As Long
            Public DpcTime As Long
            Public InterruptTime As Long
            Public InterruptCount As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SystemBasicInformation
            Public Reserved As Integer
            Public TimerResolution As Integer
            Public PageSize As Integer
            Public NumberOfPhysicalPages As Integer
            Public LowestPhysicalPageNumber As Integer
            Public HighestPhysicalPageNumber As Integer
            Public AllocationGranularity As Integer
            Public MinimumUserModeAddress As Integer
            Public MaximumUserModeAddress As Integer
            Public ActiveProcessorsAffinityMask As Integer
            Public NumberOfProcessors As Byte
        End Structure

#End Region

#Region "Declarations used for windows (not Windows :-p)"

        Public Structure PointApi
            Dim X As Integer
            Dim Y As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure WindowPlacement
            Public Length As Integer
            Public Flags As WindowPlacementFlags
            Public ShowState As ShowWindowType
            Public MinPosition As Point
            Public MaxPosition As Point
            Public NormalPosition As Rect
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure FlashWInfo
            Public cbSize As UInt32
            Public hwnd As IntPtr
            Public dwFlags As FlashWInfoFlags
            Public uCount As UInt32
            Public dwTimeout As UInt32
        End Structure

        <StructLayout(LayoutKind.Sequential)> <Serializable()> _
        Public Structure Rect
            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer
        End Structure

#End Region

        ' OK
#Region "Declarations used for services"

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure QueryServiceConfig
            Public ServiceType As ServiceType
            Public StartType As ServiceStartType
            Public ErrorControl As ServiceErrorControl
            <MarshalAs(UnmanagedType.LPTStr)> Public BinaryPathName As String
            <MarshalAs(UnmanagedType.LPTStr)> Public LoadOrderGroup As String
            Public TagID As Integer
            Public Dependencies As Integer
            <MarshalAs(UnmanagedType.LPTStr)> Public ServiceStartName As String
            <MarshalAs(UnmanagedType.LPTStr)> Public DisplayName As String
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ServiceStatus
            Public ServiceType As ServiceType
            Public CurrentState As ServiceState
            Public ControlsAccepted As ServiceAccept
            Public Win32ExitCode As Integer
            Public ServiceSpecificExitCode As Integer
            Public CheckPoint As Integer
            Public WaitHint As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ServiceStatusProcess
            Public ServiceType As ServiceType
            Public CurrentState As ServiceState
            Public ControlsAccepted As ServiceAccept
            Public Win32ExitCode As Integer
            Public ServiceSpecificExitCode As Integer
            Public CheckPoint As Integer
            Public WaitHint As Integer
            Public ProcessID As Integer
            Public ServiceFlags As ServiceFlags
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure EnumServiceStatusProcess
            <MarshalAs(UnmanagedType.LPTStr)> Public ServiceName As String
            <MarshalAs(UnmanagedType.LPTStr)> Public DisplayName As String
            <MarshalAs(UnmanagedType.Struct)> Public ServiceStatusProcess As ServiceStatusProcess
        End Structure

#End Region

        ' OK
#Region "General declarations"

        <System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)> _
        Public Structure UnicodeString
            Public Length As UShort
            Public MaximumLength As UShort
            Public Buffer As IntPtr
        End Structure

#End Region

        ' OK
#Region "Declarations used for keyboard management"

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure KBDLLHOOKSTRUCT
            Public vkCode As Integer
            Public scanCode As Integer
            Public flags As KBDLLHOOKSTRUCTFlags
            Public time As Integer
            Public dwExtraInfo As IntPtr
        End Structure

#End Region

        ' OK
#Region "Declarations used for handles"

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ObjectAttributes
            Dim Length As Integer
            Dim RootDirectoryHandle As IntPtr
            Dim ObjectName As IntPtr 'PUNICODE_STRING
            Dim Attributes As ObjectFlags
            Dim SecurityDescriptor As IntPtr
            Dim SecurityQualityOfService As IntPtr
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure GenericMapping
            Dim GenericRead As Integer
            Dim GenericWrite As Integer
            Dim GenericExecute As Integer
            Dim GenericAll As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ObjectTypeInformation
            Public Name As UnicodeString
            Public TotalNumberOfObjects As Integer
            Public TotalNumberOfHandles As Integer
            Public TotalPagedPoolUsage As Integer
            Public TotalNonPagedPoolUsage As Integer
            Public TotalNamePoolUsage As Integer
            Public TotalHandleTableUsage As Integer
            Public HighWaterNumberOfObjects As Integer
            Public HighWaterNumberOfHandles As Integer
            Public HighWaterPagedPoolUsage As Integer
            Public HighWaterNonPagedPoolUsage As Integer
            Public HighWaterNamePoolUsage As Integer
            Public HighWaterHandleTableUsage As Integer
            Public InvalidAttributes As Integer
            Public GenericMapping As GenericMapping
            Public ValidAccess As Integer
            Public SecurityRequired As Byte
            Public MaintainHandleCount As Byte
            Public MaintainTypeList As UShort
            Public PoolType As PoolType
            Public PagedPoolUsage As Integer
            Public NonPagedPoolUsage As Integer
        End Structure

        Public Structure SystemHandleInformation
            Public ProcessId As Integer
            Public ObjectTypeNumber As Byte
            Public Flags As HandleFlags
            Public Handle As Short
            Public [Object] As IntPtr
            Public GrantedAccess As Security.StandardRights
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ObjectNameInformation
            Dim Name As UnicodeString
        End Structure

        <StructLayout(LayoutKind.Sequential)> _
        Public Structure ObjectBasicInformation
            Dim Attributes As UInteger
            Dim GrantedAccess As Integer
            Dim HandleCount As UInteger
            Dim PointerCount As UInteger
            Dim PagedPoolUsage As UInteger
            Dim NonPagedPoolUsage As UInteger
            Dim Reserved1 As Integer
            Dim Reserved2 As Integer
            Dim Reserved3 As Integer
            Dim NameInformationLength As UInteger
            Dim TypeInformationLength As UInteger
            Dim SecurityDescriptorLength As UInteger
            Dim CreateTime As ULong
        End Structure

#End Region

    End Class

End Namespace
