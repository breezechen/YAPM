' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Imports System.Runtime.InteropServices
Imports System.Security.Principal

Public Class cProcess

    ' ========================================
    ' API declarations
    ' ========================================
#Region "API"
    Private Declare Function CreateToolhelpSnapshot Lib "kernel32" Alias "CreateToolhelp32Snapshot" (ByVal lFlags As Integer, ByVal lProcessID As Integer) As IntPtr
    Private Declare Function ProcessFirst Lib "kernel32" Alias "Process32First" (ByVal hSnapShot As IntPtr, ByRef uProcess As ProcessEntry32) As Integer
    Private Declare Function ProcessNext Lib "kernel32" Alias "Process32Next" (ByVal hSnapShot As IntPtr, ByRef uProcess As ProcessEntry32) As Integer

    Private Declare Function OpenProcess Lib "Kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    Private Declare Function NtSuspendProcess Lib "Ntdll.dll" (ByVal hProc As Integer) As Integer
    Private Declare Function NtResumeProcess Lib "Ntdll.dll" (ByVal hProc As Integer) As Integer
    Private Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As Integer) As Integer
    Private Declare Function SetPriorityClass Lib "kernel32" (ByVal hProcess As Integer, ByVal dwPriorityClass As Integer) As Integer

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
    ' Private Declare Function LookupAccountSid Lib "advapi32.dll" Alias "LookupAccountSidA" (ByVal lpSystemName As String, ByVal Sid As Integer, ByVal name As String, ByVal cbName As Integer, ByVal ReferencedDomainName As String, ByVal cbReferencedDomainName As Integer, ByVal peUse As Integer) As Integer
    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    ' Private Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByVal TokenInformationClass As Integer, ByRef TokenInformation As TOKEN_GROUPS, ByRef TokenInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    Private Declare Function GetTokenInformation Lib "advapi32.dll" ( _
    ByVal TokenHandle As IntPtr, ByVal TokenInformationClass As TOKEN_INFORMATION_CLASS, _
    ByRef TokenInformation As IntPtr, ByVal TokenInformationLength As System.UInt32, _
    ByRef ReturnLength As System.UInt32) As Boolean
    ' Declare Auto Function ConvertSidToStringSid Lib "advapi32.dll" (ByVal pSID() As Byte, _
    'ByRef ptrSid As IntPtr) As Boolean
    Declare Function LookupAccountSid Lib "advapi32.dll" _
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
    'Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByVal lpThreadId As Integer) As Integer
    Private Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function GetExitCodeThread Lib "kernel32" (ByVal hThread As Integer, ByRef lpExitCode As Integer) As Integer
    Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer

    <DllImport("kernel32.dll", SetLastError:=True, ExactSpelling:=True)> _
    Private Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, _
        ByVal dwSize As UInteger, ByVal flAllocationType As UInteger, _
        ByVal flProtect As UInteger) As IntPtr
    End Function
    <DllImport("kernel32.dll")> _
    Private Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As Byte(), ByVal nSize As System.UInt32, <Out()> ByRef lpNumberOfBytesWritten As Int32) As Boolean
    End Function


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

    Private Enum TOKEN_INFORMATION_CLASS
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

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _pid As Integer                         ' Process ID
    Private _path As String = vbNullString          ' _path of executable
    Private _UserName As String = vbNullString      ' User name
    Private _name As String = vbNullString          ' Name of process
    Private _hProcess As Integer = 0                ' Handle to the process
    Private _parentId As Integer = -1               ' Parent process ID
    Private _mainMod As System.Diagnostics.ProcessModule

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public isDisplayed As Boolean = False          ' Is displayed

    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal processId As Integer)
        MyBase.New()
        _pid = processId
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, _pid)
    End Sub

    Public Sub New(ByVal processId As Integer, ByVal processName As String)
        MyBase.New()
        _pid = processId
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, _pid)
        _name = processName
    End Sub

    Public Sub New(ByVal process As cProcess)
        MyBase.New()
        _pid = process.Pid
        _name = process.Name
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, _pid)
    End Sub

    Protected Overloads Overrides Sub Finalize()
        CloseHandle(_hProcess)
        MyBase.Finalize()
    End Sub

    ' ========================================
    ' Getter and setter
    ' ========================================
    Public ReadOnly Property Pid() As Integer
        Get
            Return _pid
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
                End If

                _path = sResult
            End If

            Return _path
        End Get
    End Property

    ' Get the process username
    Public ReadOnly Property UserName() As String
        Get
            If _UserName = vbNullString Then

                Dim hToken As Integer
                Dim hProc As Integer
                '   Dim cbBuff As UInteger
                Dim TG As TOKEN_GROUPS
                Dim __UserName As String
                Dim DomainName As String
                Dim UserNameLenght As Integer
                Dim DomainNameLenght As Integer
                '                Dim peUse As Integer
                Dim ppsidGroup As Integer
                Dim res As String = vbNullString
                Dim tgptr As IntPtr

                ReDim TG.Groups(500)

                tgptr = Marshal.AllocHGlobal(Marshal.SizeOf(TG))

                If _hProcess > 0 Then
                    If OpenProcessToken(_hProcess, &H8, hToken) > 0 Then
                        'GetTokenInformation(hToken, TokenUser, TG, 0, cbBuff)
                        'gettokeninfo(
                        Dim TokenInfLength As UInteger = 0
                        Dim Result As Boolean = GetTokenInformation(CType(hToken, IntPtr), TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, TokenInfLength, TokenInfLength)
                        Dim TokenInformation As IntPtr = Marshal.AllocHGlobal(CInt(TokenInfLength))
                        Result = GetTokenInformation(CType(hToken, IntPtr), TOKEN_INFORMATION_CLASS.TokenUser, TokenInformation, TokenInfLength, TokenInfLength)

                        If Result Then
                            CloseHandle(hToken)

                            'Dim TokenUser As New TOKEN_USER
                            'Marshal.PtrToStructure(TokenInformation, TokenUser)


                            'Dim pstr As IntPtr = IntPtr.Zero
                            '' Dim opop As String = MyLookupAccountSid(
                            'Dim ok As Integer = ConvertSidToStringSid(TokenInformation, opop)
                            'Dim sidstr As String = Marshal.PtrToStringAuto(pstr)
                            Dim account2 As String = New SecurityIdentifier(TokenInformation).Value

                            'Dim curID As WindowsIdentity = WindowsIdentity.GetCurrent()
                            'Dim account As New NTAccount(curID.Name)
                            'Dim sid As SecurityIdentifier = CType(account.Translate(GetType(SecurityIdentifier)), SecurityIdentifier)

                            'UserNameLenght = 255
                            '__UserName = Space$(UserNameLenght)
                            'DomainName = __UserName 'Space$(255)
                            'DomainNameLenght = UserNameLenght
                            'LookupAccountSid(vbNullString, TG.GroupCount, __UserName, UserNameLenght, DomainName, DomainNameLenght, peUse)

                            'res = Left$(__UserName, UserNameLenght)
                            res = account2
                        End If
                    Else
                        hProc = OpenProcess(PROCESS_READ_CONTROL, 0, _pid)
                        If hProc > 0 Then
                            If GetSecurityInfo(hProc, SE_KERNEL_OBJECT, GROUP_SECURITY_INFORMATION, 0, ppsidGroup, 0, 0, 0) = 0 Then
                                CloseHandle(hProc)
                                UserNameLenght = 255
                                __UserName = Space$(UserNameLenght)
                                DomainName = __UserName
                                DomainNameLenght = UserNameLenght
                                ' LookupAccountSid(vbNullString, ppsidGroup, __UserName, UserNameLenght, DomainName, DomainNameLenght, peUse)
                                res = Left$(__UserName, UserNameLenght)
                            End If
                        End If
                    End If
                End If

                _UserName = res
            End If

            Return _UserName
        End Get
    End Property

    Private Shared Function MyLookupAccountSid(ByRef i_Sid As Byte) As String

        'Input format is the format returned from "ConvertStringSidToSid"
        'Note; This function needs some work. For example, checking l_Result for error codes!

        Dim result As String = ""
        Try
            '****************************************************************
            '* Declares
            '****************************************************************

            Dim l_Result As Boolean = False
            Dim l_use As Integer = 0
            Dim l_UserName As String = ""
            Dim l_Domain As String = ""
            Dim l_UserNameLength As Integer = 0
            Dim l_DomainLength As Integer = 0

            '****************************************************************
            '* First call, populate l_UserNameLength and l_DomainLength
            '****************************************************************

            l_Result = LookupAccountSid(Nothing, i_Sid, l_UserName, l_UserNameLength, l_Domain, l_DomainLength, l_use)

            '****************************************************************
            '* Allocate space
            '****************************************************************

            l_UserName = Strings.StrDup(l_UserNameLength + 1, " ")  'Need space for terminating chr(0)?
            l_Domain = Strings.StrDup(l_DomainLength + 1, " ")

            '****************************************************************
            '* Fetch username and domain
            '****************************************************************

            l_Result = LookupAccountSid(Nothing, i_Sid, l_UserName, l_UserNameLength, l_Domain, l_DomainLength, l_use)

            '****************************************************************
            '* Build result
            '****************************************************************

            result = l_Domain.Substring(0, l_DomainLength) & "\" & l_UserName.Substring(0, l_UserNameLength)

        Catch ex As Exception
            result = ""
        End Try

        Return result

    End Function

    ' Get process name
    Public ReadOnly Property Name() As String
        Get
            If _name = vbNullString Then

                Dim sP As String = Path()
                Dim i As Integer = InStrRev(sP, "\", , CompareMethod.Binary)

                If i > 0 Then
                    _name = Right(sP, sP.Length - i)
                Else
                    _name = "N\A"
                End If

            End If

            Return _name
        End Get
    End Property

    ' Get processor time as a TimeSpan
    Public ReadOnly Property ProcessorTime() As Date
        Get
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
        End Get
    End Property

    ' Get processor time as a long
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
    Public ReadOnly Property KernelTime() As Date
        Get
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
        End Get
    End Property

    ' Get kernel time as a long
    Public ReadOnly Property KernelTimeLong() As Long
        Get
            Dim T0 As Long
            Dim T1 As Long
            Dim curTime2 As Long
            Dim curTime As Long
            Dim r As Long = 0

            If _hProcess > 0 Then

                GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                r = curTime

            End If

            Return r
        End Get
    End Property

    ' Get user time as a TimeSpan
    Public ReadOnly Property UserTime() As Date
        Get
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
        End Get
    End Property

    ' Get user time as a long
    Public ReadOnly Property UserTimeLong() As Long
        Get
            Dim T0 As Long
            Dim T1 As Long
            Dim curTime2 As Long
            Dim curTime As Long
            Dim r As Long = 0

            If _hProcess > 0 Then

                GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
                r = curTime2

            End If

            Return r
        End Get
    End Property

    ' Get the WorkingSet64
    Public ReadOnly Property WorkingSet64() As Long
        Get
            Return MemoryInfos.WorkingSetSize
        End Get
    End Property

    ' Get all memory infos
    Public ReadOnly Property MemoryInfos() As PROCESS_MEMORY_COUNTERS
        Get
            Dim pmc As PROCESS_MEMORY_COUNTERS

            If _hProcess > 0 Then
                pmc.cb = Marshal.SizeOf(pmc)
                GetProcessMemoryInfo(_hProcess, pmc, pmc.cb)
            End If

            Return pmc
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

    ' Get CPU time
    Public ReadOnly Property CpuPercentageUsage() As Double
        Get
            Static oldDate As Long = Date.Now.Ticks
            Static oldProcTime As Long = Me.ProcessorTimeLong

            Dim currDate As Long = Date.Now.Ticks
            Dim proctime As Long = Me.ProcessorTimeLong

            Dim diff As Long = currDate - oldDate
            Dim procDiff As Long = proctime - oldProcTime

            oldProcTime = proctime
            oldDate = currDate

            Return procDiff / diff
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
                'BUGGY
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
                Dim gProc As Process = Process.GetProcessById(_pid)
                _mainMod = gProc.MainModule
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

    ' Retrieve informations by its name
    Public Function GetInformation(ByVal infoName As String) As String
        Dim res As String = NO_INFO_RETRIEVED
        Dim mem As cProcess.PROCESS_MEMORY_COUNTERS = Me.MemoryInfos
        Select Case infoName
            Case "PID"
                res = CStr(Me.Pid)
            Case "UserName"
                res = Me.UserName
            Case "CpuUage"
                res = CStr(Math.Round(Me.CpuPercentageUsage, 4))
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
                res = CStr(mem.WorkingSetSize)
            Case "PeakWorkingSet"
                res = CStr(mem.PeakWorkingSetSize)
            Case "PageFaultCount"
                res = CStr(mem.PageFaultCount)
            Case "PagefileUsage"
                res = CStr(mem.PagefileUsage)
            Case "PeakPagefileUsage"
                res = CStr(mem.PeakPagefileUsage)
            Case "QuotaPeakPagedPoolUsage"
                res = CStr(mem.QuotaPeakPagedPoolUsage)
            Case "QuotaPagedPoolUsage"
                res = CStr(mem.QuotaPagedPoolUsage)
            Case "QuotaPeakNonPagedPoolUsage"
                res = CStr(mem.QuotaPeakNonPagedPoolUsage)
            Case "QuotaNonPagedPoolUsage"
                res = CStr(mem.QuotaNonPagedPoolUsage)
            Case "Priority"
                res = Me.PriorityClass.ToString
            Case "Path"
                res = Me.Path
            Case "Description"
                res = Me.MainModule.FileVersionInfo.FileDescription
            Case "Copyright"
                res = Me.MainModule.FileVersionInfo.LegalCopyright
            Case "Version"
                res = Me.MainModule.FileVersionInfo.FileVersion
            Case "Name"
                res = Me.Name
        End Select
        Return res
    End Function



    ' ========================================
    ' Shared functions
    ' ========================================

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(20) As String

        s(0) = "PID"
        s(1) = "UserName"
        s(2) = "CpuUage"
        s(3) = "KernelCpuTime"
        s(4) = "UserCpuTime"
        s(5) = "TotalCpuTime"
        s(6) = "StartTime"
        s(7) = "WorkingSet"
        s(8) = "PeakWorkingSet"
        s(9) = "PageFaultCount"
        s(10) = "PagefileUsage"
        s(11) = "PeakPagefileUsage"
        s(12) = "QuotaPeakPagedPoolUsage"
        s(13) = "QuotaPagedPoolUsage"
        s(14) = "QuotaPeakNonPagedPoolUsage"
        s(15) = "QuotaNonPagedPoolUsage"
        s(16) = "Priority"
        s(17) = "Path"
        s(18) = "Description"
        s(19) = "Copyright"
        s(20) = "Version"

        Return s
    End Function

    ' Retrieve process list
    ' This so much faster than VB.Net methods...
    Public Shared Function Enumerate(ByRef p() As cProcess) As Integer
        Dim hSnapshot As Integer
        Dim uProcess As ProcessEntry32 = Nothing
        Dim r As Integer
        Dim x As Integer

        ReDim p(0)
        x = 0

        hSnapshot = CInt(CreateToolhelpSnapshot(TH32CS_SNAPPROCESS, 0&))
        If hSnapshot <> 0 Then

            uProcess.dwSize = Marshal.SizeOf(uProcess)

            r = ProcessFirst(CType(hSnapshot, IntPtr), uProcess)

            Do While (r <> 0)

                p(x) = New cProcess(uProcess.th32ProcessID, CStr(uProcess.szExeFile))
                p(x).isDisplayed = False

                r = ProcessNext(CType(hSnapshot, IntPtr), uProcess)
                If r <> 0 Then
                    ReDim Preserve p(UBound(p) + 1)
                    x += 1
                End If
            Loop

            Call CloseHandle(hSnapshot)

        End If

        Return x

    End Function

    ' Unload a module from a process
    Public Shared Function UnLoadModuleFromProcess(ByVal ProcessId As Integer, ByVal ModuleBaseAddress As Integer) As Integer

        Dim hProc As Integer = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE Or PROCESS_VM_READ, 0, ProcessId)
        Dim kernel32 As Integer = cProcess.GetModuleHandle("kernel32.dll")
        Dim freeLibrary As Integer = cProcess.GetProcAddress(kernel32, "FreeLibrary")
        Dim threadId As Integer

        cProcess.CreateRemoteThread(hProc, 0, 0, freeLibrary, ModuleBaseAddress, 0, threadId)
    End Function

    ' Return path
    Public Shared Function GetPath(ByVal _pid As Integer) As String
        Dim s As String = vbNullString
        Dim lHprcss As Integer
        Dim Ret As Integer
        Dim sResult As String = Space(512)
        Dim hModule As Integer

        lHprcss = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, _pid)

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

    ' Kill a process
    Public Shared Function Kill(ByVal _pid As Integer) As Integer
        Dim hProc As Integer
        Dim ret As Integer = -1
        hProc = OpenProcess(PROCESS_TERMINATE, 0, _pid)
        If hProc > 0 Then
            ret = TerminateProcess(hProc, 0)
            CloseHandle(hProc)
        End If
        Return ret
    End Function


    ' ========================================
    ' Private functions
    ' ========================================
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
End Class
