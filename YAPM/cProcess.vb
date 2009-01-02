Option Strict On

Imports System.Runtime.InteropServices

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

    Private Declare Function NtQueryInformationProcess Lib "Ntdll.dll" (ByVal hProcess As Integer, ByVal ProcessInformationClass As Integer, ByVal ProcessInformation As Integer, ByVal ProcessInformationLength As Integer, ByVal ReturnLength As Integer) As Integer
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

    Private Declare Function GetProcessTimes Lib "kernel32" (ByVal hProcess As Integer, ByRef lpCreationTime As FILETIME2, ByRef lpExitTime As FILETIME2, ByRef lpKernelTime As FILETIME2, ByRef lpUserTime As FILETIME2) As Integer

    Private Declare Function CreateToolhelp32Snapshot Lib "kernel32.dll" (ByVal dwFlags As Integer, ByVal th32ProcessID As Integer) As Integer
    Private Declare Function Thread32First Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean
    Private Declare Function Thread32Next Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean

    Private Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Integer, ByVal uExitCode As Integer) As Integer

    Private Declare Function GetSecurityInfo Lib "advapi32.dll" (ByVal hObject As Integer, ByVal ObjectType As Integer, ByVal SecurityInformation As Integer, ByVal ppsidOwner As Integer, ByVal ppsidGroup As Integer, ByVal ppDacl As Integer, ByVal ppSacl As Integer, ByVal ppSecurityDescriptor As Integer) As Integer
    Private Declare Function LookupAccountSid Lib "advapi32.dll" Alias "LookupAccountSidA" (ByVal lpSystemName As String, ByVal Sid As Integer, ByVal name As String, ByVal cbName As Integer, ByVal ReferencedDomainName As String, ByVal cbReferencedDomainName As Integer, ByVal peUse As Integer) As Integer
    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByVal TokenHandle As Integer) As Integer
    Private Declare Function GetTokenInformation Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByVal TokenInformationClass As Integer, ByVal TokenInformation As TOKEN_GROUPS, ByVal TokenInformationLength As Integer, ByVal ReturnLength As Integer) As Integer

    Private Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    'Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByVal lpThreadId As Integer) As Integer
    Private Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function GetExitCodeThread Lib "kernel32" (ByVal hThread As Integer, ByRef lpExitCode As Integer) As Integer
    Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByRef lpThreadAttributes As SECURITY_ATTRIBUTES, ByVal dwStackSize As Integer, ByRef lpStartAddress As Integer, ByRef lpParameter As Object, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer

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
    Private Const HIGH_PRIORITY_CLASS As Long = &H80 '
    Private Const REALTIME_PRIORITY_CLASS As Integer = &H100

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

    Private Structure SID_AND_ATTRIBUTES
        Dim Sid As Integer
        Dim Attributes As Integer
    End Structure

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
    Public Function GetError(ByVal hError As Integer) As String

        Dim Buffer As String

        Buffer = Space$(1024)
        FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0, hError, LANG_NEUTRAL, Buffer, Len(Buffer), 0)
        Return Trim$(Buffer)

    End Function
    ' ========================================
    ' Private attributes
    ' ========================================
    Private pid As Integer                          ' Process ID
    Private path As String = vbNullString           ' Path of executable
    Private _UserName As String = vbNullString      ' User name
    Private _name As String = vbNullString          ' Name of process
    Private _hProcess As Integer = 0                 ' Handle to the process


    Public isDisplayed As Boolean = False          ' Is displayed

    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal processId As Integer)
        MyBase.New()
        pid = processId
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, pid)
    End Sub

    Public Sub New(ByVal processId As Integer, ByVal processName As String)
        MyBase.New()
        pid = processId
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, pid)
        _name = processName
    End Sub

    Public Sub New(ByVal process As cProcess)
        MyBase.New()
        pid = process.GetPid
        _name = process.GetName
        _hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, pid)
    End Sub

    Protected Overloads Overrides Sub Finalize()
        CloseHandle(_hProcess)
        MyBase.Finalize()
    End Sub

    ' ========================================
    ' Getter and setter
    ' ========================================
    Public Function GetPid() As Integer
        Return pid
    End Function

    Public Function GetPath() As String

        If path = vbNullString Then

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

            path = sResult
        End If

        Return path

    End Function

    Public Function GetUserName() As String

        If _UserName = vbNullString Then

            Dim hToken As Integer
            Dim hProc As Integer
            Dim cbBuff As Integer
            Dim TG As TOKEN_GROUPS
            Dim UserName As String
            Dim DomainName As String
            Dim UserNameLenght As Integer
            Dim DomainNameLenght As Integer
            Dim peUse As Integer
            Dim ppsidGroup As Integer
            Dim res As String = vbNullString

            ReDim TG.Groups(500)

            If _hProcess > 0 Then
                If OpenProcessToken(_hProcess, &H8, hToken) > 0 Then
                    'GetTokenInformation(hToken, TokenUser, TG, 0, cbBuff)
                    If GetTokenInformation(hToken, TokenUser, TG, cbBuff, 0) > 0 Then
                        CloseHandle(hToken)
                        UserNameLenght = 255
                        UserName = Space$(UserNameLenght)
                        DomainName = UserName 'Space$(255)
                        DomainNameLenght = UserNameLenght
                        LookupAccountSid(vbNullString, TG.GroupCount, UserName, UserNameLenght, DomainName, DomainNameLenght, peUse)
                        res = Left$(UserName, UserNameLenght)
                    End If
                Else
                    hProc = OpenProcess(PROCESS_READ_CONTROL, 0, pid)
                    If _hProcess > 0 Then
                        If GetSecurityInfo(_hProcess, SE_KERNEL_OBJECT, GROUP_SECURITY_INFORMATION, 0, ppsidGroup, 0, 0, 0) = 0 Then
                            CloseHandle(hProc)
                            UserNameLenght = 255
                            UserName = Space$(UserNameLenght)
                            DomainName = UserName
                            DomainNameLenght = UserNameLenght
                            LookupAccountSid(vbNullString, ppsidGroup, UserName, UserNameLenght, DomainName, DomainNameLenght, peUse)
                            res = Left$(UserName, UserNameLenght)
                        End If
                    End If
                End If
            End If

            _UserName = res
        End If

        Return _UserName
    End Function

    Public Function GetName() As String

        If _name = vbNullString Then

            Dim sP As String = GetPath()
            Dim i As Integer = InStrRev(sP, "\", , CompareMethod.Binary)

            If i > 0 Then
                _name = Right(sP, sP.Length - i)
            Else
                _name = "N\A"
            End If

        End If

        Return _name

    End Function

    Public Function GetProcessorTime() As TimeSpan

        Dim T0 As FILETIME2
        Dim T1 As FILETIME2
        Dim curTime2 As FILETIME2
        Dim curTime As FILETIME2
        Dim r As TimeSpan
        If _hProcess > 0 Then

            GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
            Dim p1 As New TimeSpan(curTime.dwLowDateTime + curTime.dwHighDateTime)
            Dim p2 As New TimeSpan(curTime2.dwLowDateTime + curTime2.dwHighDateTime)
            ' BUGGY
            p1.Add(p2)
            r = p1

        End If

        Return r

    End Function

    Public Function GetWorkingSet64() As Long
        Return GetMemoryInfos.WorkingSetSize
    End Function

    Public Function GetMemoryInfos() As PROCESS_MEMORY_COUNTERS
        Dim pmc As PROCESS_MEMORY_COUNTERS

        If _hProcess > 0 Then
            pmc.cb = Marshal.SizeOf(pmc)
            GetProcessMemoryInfo(_hProcess, pmc, pmc.cb)
        End If

        Return pmc
    End Function

    Public Function GetThreads() As System.Diagnostics.ProcessThreadCollection
        Dim gProc As Process = Process.GetProcessById(pid)
        Return gProc.Threads
    End Function

    Public Function GetPriorityClass() As String
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
                Return "N/A"
        End Select

    End Function

    Public Function GetStartTime() As TimeSpan
        ' TOCHANGE
        'Dim gProc As Process = Process.GetProcessById(pid)
        'Return gProc.StartTime

        Dim T0 As FILETIME2
        Dim T1 As FILETIME2
        Dim curTime2 As FILETIME2
        Dim curTime As FILETIME2
        Dim r As TimeSpan

        If _hProcess > 0 Then
            'BUGGY
            GetProcessTimes(_hProcess, T0, T1, curTime, curTime2)
            Dim p1 As New TimeSpan(T0.dwLowDateTime + T0.dwHighDateTime)
            r = p1
        End If

        Return r

    End Function

    Public Function GetMainModule() As System.Diagnostics.ProcessModule
        ' TOCHANGE
        Dim gProc As Process = Process.GetProcessById(pid)
        Return gProc.MainModule
    End Function

    Public Function GetModules() As ProcessModuleCollection
        ' TOCHANGE
        Dim gProc As Process = Process.GetProcessById(pid)
        Return gProc.Modules
    End Function


    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Suspend a process
    Public Function SuspendProcess() As Integer
        Dim hProc As Integer
        Dim r As Integer = -1

        hProc = OpenProcess(PROCESS_SUSPEND_RESUME, 0, pid)

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

        hProc = OpenProcess(PROCESS_SUSPEND_RESUME, 0, pid)

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

        hProc = OpenProcess(PROCESS_SET_INFORMATION, 0, pid)
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
        hProc = OpenProcess(PROCESS_TERMINATE, 0, pid)
        If hProc > 0 Then
            ret = TerminateProcess(hProc, 0)
            CloseHandle(hProc)
        End If
        Return ret
    End Function

    ' Unload a module from a process
    Public Function UnLoadModuleFromProcess(ByVal ModulePathName As String) As Integer
        Dim hKernel32 As Integer
        Dim hThread As Integer
        Dim hProc As Integer
        Dim hFunc As Integer
        Dim hVirtual As Integer
        Dim hMod As Integer = 0
        Dim ret As Integer

        hProc = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE Or _
                                PROCESS_VM_READ, 0, pid)

        If hProc > 0 Then

            hKernel32 = GetModuleHandle(KRN_DLL)
            hFunc = GetProcAddress(hKernel32, GET_HMOD)

            ' Get the module handle
            hVirtual = CInt(VirtualAllocEx(CType(hProc, IntPtr), IntPtr.Zero, CUInt(Len(ModulePathName)), _
                MEM_COMMIT, PAGE_READWRITE))

            Dim d() As Byte
            Dim encoding As New System.Text.ASCIIEncoding()
            d = encoding.GetBytes(ModulePathName)

            If WriteProcessMemory(CType(hProc, IntPtr), CType(hVirtual, IntPtr), _
                d, CUInt(d.Length), 0) Then

                hThread = CreateRemoteThread(hProc, Nothing, 0, hFunc, CObj(hVirtual), 0, 0)
                If hThread > 0 Then
                    WaitForSingleObject(hThread, &H9C4)
                    'on recupere le handle du Module dans hMod
                    GetExitCodeThread(hThread, hMod)
                    CloseHandle(hThread)
                End If
            End If

            If hMod > 0 Then
                hFunc = GetProcAddress(hKernel32, KRN_FREE)
                hThread = CreateRemoteThread(hProc, Nothing, 0, hFunc, CObj(hMod), 0, 0)
                If hThread > 0 Then
                    WaitForSingleObject(hThread, &H9C4)
                    'on recupere le code de retour de FreeLibrary (si = 1 le module a bien été dechargé)
                    GetExitCodeThread(hThread, ret)
                    CloseHandle(hThread)
                End If
            End If

            CloseHandle(hProc)
        End If

        Return ret

    End Function




    ' ========================================
    ' Shared functions
    ' ========================================
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
    Public Shared Function UnLoadModuleFromProcess(ByVal ProcessId As Integer, ByVal ModulePathName As String) As Integer
        Dim hKernel32 As Integer
        Dim hThread As Integer
        Dim hProc As Integer
        Dim hFunc As Integer
        Dim hVirtual As Integer
        Dim hMod As Integer = 0
        Dim ret As Integer

        hProc = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE Or _
                                PROCESS_VM_READ, 0, ProcessId)

        If hProc > 0 Then

            hKernel32 = GetModuleHandle(KRN_DLL)
            hFunc = GetProcAddress(hKernel32, GET_HMOD)

            ' Get the module handle
            hVirtual = CInt(VirtualAllocEx(CType(hProc, IntPtr), IntPtr.Zero, CUInt(Len(ModulePathName)), _
                MEM_COMMIT, PAGE_READWRITE))

            Dim d() As Byte
            Dim encoding As New System.Text.ASCIIEncoding()
            d = encoding.GetBytes(ModulePathName)

            If WriteProcessMemory(CType(hProc, IntPtr), CType(hVirtual, IntPtr), _
                d, CUInt(d.Length), 0) Then

                hThread = CreateRemoteThread(hProc, Nothing, 0, hFunc, CObj(hVirtual), 0, 0)
                If hThread > 0 Then
                    WaitForSingleObject(hThread, &H9C4)
                    'on recupere le handle du Module dans hMod
                    GetExitCodeThread(hThread, hMod)
                    CloseHandle(hThread)
                End If
            End If

            If hMod > 0 Then
                hFunc = GetProcAddress(hKernel32, KRN_FREE)
                hThread = CreateRemoteThread(hProc, Nothing, 0, hFunc, CObj(hMod), 0, 0)
                If hThread > 0 Then
                    WaitForSingleObject(hThread, &H9C4)
                    'on recupere le code de retour de FreeLibrary (si = 1 le module a bien été dechargé)
                    GetExitCodeThread(hThread, ret)
                    CloseHandle(hThread)
                End If
            End If

            CloseHandle(hProc)
        End If

        Return ret

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
                s = "N/A"
        End Select

        Return s
    End Function
End Class
