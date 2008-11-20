Option Strict On

' Here is some unsafe code from VB6

Imports System.Runtime.InteropServices

Module mdlProcess

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

    Private Declare Function GetProcessMemoryInfo Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByVal ppsmemCounters As PROCESS_MEMORY_COUNTERS, ByVal cb As Integer) As Integer
    Private Declare Function GetPriorityClass Lib "kernel32" Alias "GetPriorityClass" (ByVal hProcess As Integer) As Integer

    Private Declare Function GetProcessTimes Lib "kernel32" (ByVal hProcess As Integer, ByVal lpCreationTime As FILETIME2, ByVal lpExitTime As FILETIME2, ByVal lpKernelTime As FILETIME2, ByVal lpUserTime As FILETIME2) As Integer

    Private Declare Function CreateToolhelp32Snapshot Lib "kernel32.dll" (ByVal dwFlags As Integer, ByVal th32ProcessID As Integer) As Integer
    Private Declare Function Thread32First Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean
    Private Declare Function Thread32Next Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean
    Private Declare Function OpenSCManager Lib "advapi32.dll" Alias "OpenSCManagerA" (ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As IntPtr
    Private Declare Function apiStartService Lib "advapi32.dll" Alias "StartServiceA" (ByVal hService As IntPtr, ByVal dwNumServiceArgs As Integer, ByVal lpServiceArgVectors As Integer) As Integer

    <DllImport("AdvApi32", CharSet:=CharSet.Auto, entrypoint:="ChangeServiceConfigA")> _
        Private Function ChangeServiceConfig(ByVal hService As IntPtr, ByVal dwServiceType As Integer, ByVal dwStartType As Integer, ByVal dwErrorControl As Integer, ByVal lpBinaryPathName As String, ByVal lpLoadOrderGroup As String, ByVal lpdwTagId As Integer, ByVal lpDependencies As String, ByVal lpServiceStartName As String, ByVal lpPassword As String, ByVal lpDisplayName As String) As Boolean
    End Function
    <DllImport("advapi32.dll", SetLastError:=True)> _
    Private Function ControlService(ByVal hService As IntPtr, ByVal dwControl As SERVICE_CONTROL, ByRef lpServiceStatus As SERVICE_STATUS) As Boolean
    End Function
    Private Declare Function OpenService Lib "advapi32.dll" Alias "OpenServiceA" (ByVal hSCManager As IntPtr, ByVal lpServiceName As String, ByVal dwDesiredAccess As Integer) As IntPtr
    <DllImport("advapi32.dll", SetLastError:=True)> _
        Private Function CloseServiceHandle(ByVal serviceHandle As IntPtr) As Boolean
    End Function

    Private Enum SERVICE_CONTROL
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

    Private Const PROCESS_SET_INFORMATION As Integer = &H200
    Private Const PROCESS_SUSPEND_RESUME As Integer = &H800
    Private Const PROCESS_QUERY_INFORMATION As Integer = &H400

    Private Const THREAD_SET_INFORMATION As Integer = &H20
    Private Const THREAD_QUERY_INFORMATION As Integer = &H40

    Private Const PROCESS_VM_READ As Integer = 16
    Private Const TH32CS_SNAPPROCESS As Integer = &H2

    Public Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Public Const SERVICE_QUERY_CONFIG As Integer = &H1
    Public Const SERVICE_CHANGE_CONFIG As Integer = &H2
    Public Const SERVICE_QUERY_STATUS As Integer = &H4
    Public Const SERVICE_ENUMERATE_DEPENDENTS As Integer = &H8
    Public Const SERVICE_START As Integer = &H10
    Public Const SERVICE_STOP As Integer = &H20
    Public Const SERVICE_PAUSE_CONTINUE As Integer = &H40
    Public Const SERVICE_INTERROGATE As Integer = &H80
    Public Const SERVICE_USER_DEFINED_CONTROL As Integer = &H100
    Public Const SERVICE_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SERVICE_QUERY_CONFIG + SERVICE_CHANGE_CONFIG + SERVICE_QUERY_STATUS + SERVICE_ENUMERATE_DEPENDENTS + SERVICE_START + SERVICE_STOP + SERVICE_PAUSE_CONTINUE + SERVICE_INTERROGATE + SERVICE_USER_DEFINED_CONTROL)
    Public Const SERVICE_CONTROL_STOP As Integer = &H1
    Public Const SERVICE_CONTROL_CONTINUE As Integer = &H3
    Public Const SERVICE_CONTROL_INTERROGATE As Integer = &H4
    Public Const SERVICE_CONTROL_SHUTDOWN As Integer = &H5
    Public Const SERVICE_CONTROL_PAUSE As Integer = &H2
    Public Const SERVICE_NO_CHANGE As Integer = &HFFFF
    Public Const SC_MANAGER_CONNECT As Integer = &H1
    Public Const SC_MANAGER_CREATE_SERVICE As Integer = &H2
    Public Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4
    Public Const SC_MANAGER_LOCK As Integer = &H8
    Public Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
    Public Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
    Public Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

    Public Structure THREADENTRY32
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
    Public Structure SYSTEM_INFO
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

    Private Structure PROCESS_MEMORY_COUNTERS
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

    Public Structure PROCESS_CHANGEABLES_INFOS
        Dim PageFaultCount As Integer
        Dim PeakWorkingSetSize As Integer
        Dim WorkingSetSize As Integer
        Dim QuotaPeakPagedPoolUsage As Integer
        Dim QuotaPagedPoolUsage As Integer
        Dim QuotaPeakNonPagedPoolUsage As Integer
        Dim QuotaNonPagedPoolUsage As Integer
        Dim PagefileUsage As Integer
        Dim PeakPagefileUsage As Integer
        Dim Priority As String
        Dim ThreadsCount As Integer
        Dim ProcessorTime As String
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

    ' Suspend a process
    Public Function SuspendProcess(ByVal pid As Integer) As Integer
        Dim hProcess As Integer
        Dim r As Integer = -1

        hProcess = OpenProcess(PROCESS_SUSPEND_RESUME, 0, pid)

        If hProcess > 0 Then
            r = NtSuspendProcess(hProcess)
            CloseHandle(hProcess)
        End If

        Return r
    End Function

    ' Resume a process
    Public Function ResumeProcess(ByVal pid As Integer) As Integer
        Dim hProcess As Integer
        Dim r As Integer = -1

        hProcess = OpenProcess(PROCESS_SUSPEND_RESUME, 0, pid)

        If hProcess > 0 Then
            r = NtResumeProcess(hProcess)
            CloseHandle(hProcess)
        End If

        Return r
    End Function

    ' Set priority
    Public Function SetProcessPriority(ByVal pid As Integer, ByVal level As ProcessPriorityClass) As Integer

        Dim hProcess As Integer
        Dim r As Integer

        hProcess = OpenProcess(PROCESS_SET_INFORMATION, 0, pid)
        If hProcess > 0 Then
            r = SetPriorityClass(hProcess, level)
            CloseHandle(hProcess)
        End If

        Return r
    End Function

    ' Retrieve process list
    ' This so much faster than VB.Net methods...
    Public Function Enumerate(ByRef p() As cProc) As Integer
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

                p(x) = New cProc
                With p(x)
                    .Pid = uProcess.th32ProcessID
                    .Name = CStr(uProcess.szExeFile)
                    .IsDisplayed = False
                End With

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

    ' Get process path from ID
    Public Function GetPath(ByVal pid As Integer) As String

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

        Return Left(sResult, InStr(sResult, vbNullChar) - 1)

    End Function

    'Public Function GetProcessChangeableInfos(ByVal pid As Integer) As PROCESS_CHANGEABLES_INFOS
    '    'Dim hProcess As Integer
    '    ''Dim pmc As PROCESS_MEMORY_COUNTERS
    '    'Dim r As PROCESS_CHANGEABLES_INFOS = Nothing
    '    'hProcess = OpenProcess(PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ, 0, pid)

    '    'If hProcess > 0 Then

    '    '    ' Get memory infos
    '    '    'pmc.cb = Marshal.SizeOf(pmc)
    '    '    'GetProcessMemoryInfo(hProcess, pmc, pmc.cb)

    '    '    ' Get processor time info
    '    '    Dim nonused As FILETIME2
    '    '    Dim ct As FILETIME2
    '    '    Dim ct2 As FILETIME2
    '    '    Dim lt As Double = 0
    '    '    Dim lt2 As Double = 0
    '    '    Try
    '    '        Call GetProcessTimes(hProcess, nonused, nonused, ct, ct2)
    '    '    Catch ex As Exception

    '    '    End Try
    '    '    lt = (ct.dwLowDateTime + ct.dwHighDateTime) / 1000
    '    '    lt2 = (ct2.dwLowDateTime + ct2.dwHighDateTime) / 1000
    '    '    lt = lt + lt2
    '    '    r.ProcessorTime = CStr(lt)

    '    '    ' Get threads info
    '    '    'r.ThreadsCount = 56

    '    '    ' Get priority info
    '    '    r.Priority = PriorityFromInt(GetPriorityClass(hProcess))

    '    '    CloseHandle(hProcess)

    '    'End If

    '    'Return r

    'End Function

    Public Function GetThreads(ByRef Thread() As THREADENTRY32, ByVal pid As Integer) As Integer

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
                s = "N/A"
        End Select

        Return s
    End Function

    Public Function GetServiceStartTypeFromInt(ByVal i As Integer) As String
        Dim s As String = vbNullString

        Select Case i
            Case 0  'SERVICE_BOOT_START
                s = "Boot Start"
            Case 1  'SERVICE_SYSTEM_START
                s = "System Start"
            Case 2  'SERVICE_AUTO_START
                s = "Auto Start"
            Case 3  'SERVICE_DEMAND_START
                s = "Demand Start"
            Case 4  'SERVICE_DISABLED
                s = "Disabled"
        End Select

        Return s
    End Function

    ' Retrieve information about a service from registry
    Public Function GetServiceInfo(ByVal service As String, ByVal info As String) As String
        Return CStr(My.Computer.Registry.GetValue( _
                    "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" & service, _
                    info, ""))
    End Function

    ' Retrieve file name from a path with arguments
    Public Function GetFileNameFromSpecial(ByVal path As String) As String
        Return ""
    End Function

    ' Start a service
    Public Sub StartService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                apiStartService(lServ, 0, 0)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If

    End Sub

    ' Pause a service
    Public Sub PauseService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._PAUSE, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Resume a service
    Public Sub ResumeService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._CONTINUE, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Stop a service
    Public Sub StopService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._STOP, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Shutdown a service
    Public Sub ShutDownService(ByVal service As String)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim lpSS As SERVICE_STATUS

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)
        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ControlService(lServ, SERVICE_CONTROL._SHUTDOWN, lpSS)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If
    End Sub

    ' Set service start type
    Public Sub SetServiceStartType(ByVal service As String, ByVal type As ServiceProcess.ServiceStartMode)
        Dim hSCManager As IntPtr
        Dim lServ As IntPtr
        Dim st As Integer

        Select Case type
            Case ServiceProcess.ServiceStartMode.Automatic
                st = 2
            Case ServiceProcess.ServiceStartMode.Disabled
                st = 4
            Case ServiceProcess.ServiceStartMode.Manual
                st = 3
        End Select

        hSCManager = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ALL_ACCESS)

        If hSCManager <> IntPtr.Zero Then
            lServ = OpenService(hSCManager, service, SERVICE_ALL_ACCESS)
            If lServ <> IntPtr.Zero Then
                ChangeServiceConfig(lServ, SERVICE_NO_CHANGE, st, _
                    SERVICE_NO_CHANGE, vbNullString, vbNullString, 0, _
                    vbNullString, vbNullString, vbNullString, vbNullString)
                CloseServiceHandle(lServ)
            End If
            CloseServiceHandle(hSCManager)
        End If

    End Sub

#Region "Functions for process affinity"
    ' Comming from http://www.vbfrance.com/codes/AFFINITE-PROCESSUS-THREADS_42365.aspx
    Public Function GetCpuCount() As Integer
        Dim S_I As SYSTEM_INFO
        GetSystemInfo(S_I)
        Return S_I.dwNumberOfProcessors
    End Function
    Public Function GetAllCoreMask() As Integer
        Dim S_I As SYSTEM_INFO
        GetSystemInfo(S_I)
        Return S_I.dwActiveProcessorMask
    End Function
    Public Function GetThreadMask(ByVal ThreadId As Integer) As Integer

        Dim hThread As Integer
        Dim TBI As THREAD_BASIC_INFORMATION
        Dim r As Integer = -1

        hThread = OpenThread(THREAD_QUERY_INFORMATION, 0, ThreadId)

        If hThread > 0 Then
            Dim ptr As System.IntPtr = System.IntPtr.Zero
            System.Runtime.InteropServices.Marshal.StructureToPtr(TBI, ptr, False)
            NtQueryInformationThread(hThread, 0&, CInt(ptr), Len(TBI), 0&)
            r = TBI.AffinityMask
            CloseHandle(hThread)
        End If

        Return r

    End Function
    Public Function SetThreadMask(ByVal ThreadId As Integer, ByVal Mask As Integer) As Integer

        Dim hThread As Integer
        Dim r As Integer = -1

        hThread = OpenThread(THREAD_SET_INFORMATION, 0, ThreadId)

        If hThread > 0 Then
            Dim ptr As System.IntPtr = System.IntPtr.Zero
            System.Runtime.InteropServices.Marshal.StructureToPtr(Mask, ptr, False)
            r = NtSetInformationThread(hThread, 4&, CInt(ptr), 4)
            CloseHandle(hThread)
        End If

        Return r

    End Function
    Public Function GetProcessMask(ByVal ProcessId As Integer) As Integer

        Dim hProcess As Integer
        Dim r As Integer = -1
        Dim Pbi As PROCESS_BASIC_INFORMATION

        hProcess = OpenProcess(PROCESS_QUERY_INFORMATION, 0, ProcessId)

        If hProcess > 0 Then
            Dim ptr As System.IntPtr = System.IntPtr.Zero
            System.Runtime.InteropServices.Marshal.StructureToPtr(Pbi, ptr, False)
            NtQueryInformationProcess(hProcess, 0&, CInt(ptr), Marshal.SizeOf(Pbi), 0&)
            r = Pbi.AffinityMask
            CloseHandle(hProcess)
        End If

        Return r

    End Function
    Public Function SetProcessMask(ByVal ProcessId As Integer, ByVal Mask As Integer) As Integer

        Dim hProcess As Integer
        Dim r As Integer = -1

        hProcess = OpenProcess(PROCESS_SET_INFORMATION, 0, ProcessId)

        If hProcess > 0 Then
            Dim ptr As System.IntPtr = System.IntPtr.Zero
            System.Runtime.InteropServices.Marshal.StructureToPtr(Mask, ptr, False)
            r = NtSetInformationProcess(hProcess, 21&, CInt(Mask), 4)
            CloseHandle(hProcess)
        End If

        Return r

    End Function
    Public Function GetMaskFromCpuList(ByRef CpuList As String, Optional ByRef Separator As String = ",") As Integer

        Dim s() As String
        Dim i As Integer
        Dim l As Integer

        If CpuList = "" Then
            Return -1
        Else
            s = Split(CpuList, Separator)

            For i = 0 To UBound(s)
                l = l + CInt(2 ^ CInt(s(i)))
            Next i

            Return l
        End If

    End Function
    Public Function GetCpuListFromMask(ByVal Mask As Integer, Optional ByRef Separator As String = ",") As String

        Dim i As Integer
        Dim Value As Integer
        Dim r As String = ""
        Dim sMask As String = ""

        For i = 0 To GetCpuCount() - 1
            Value = CInt(2 ^ i)
            If Mask < Value Then Exit For
            If CBool(Mask And Value) Then
                sMask = sMask & i & Separator
            End If
        Next i

        If sMask <> "" Then
            r = Left(sMask, Len(sMask) - 1)
        End If

        Return r

    End Function
#End Region

End Module
