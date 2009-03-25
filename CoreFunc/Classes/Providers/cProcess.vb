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
' - GetAccountName function (conversion from SID to username as a string)
' - GetImageFile function, especially DeviceDriveNameToDosDriveName and
'   RefreshLogicalDrives which are inspired by functions from Process Hacker

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Text
Imports CoreFunc.cBasedStateActionState

Public MustInherit Class cProcess
    Inherits cGeneralObject

#Region "API"

    Private Declare Function OpenProcess Lib "Kernel32.dll" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
    Private Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As Integer) As Integer
    Private Declare Function GetModuleFileNameExA Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByVal hModule As Integer, ByVal ModuleName As String, ByVal nSize As Integer) As Integer
    Private Declare Function EnumProcessModules Lib "psapi.dll" (ByVal hProcess As Integer, ByVal lphModule As Integer, ByVal cb As Integer, ByVal lpcbNeeded As Integer) As Boolean
    Private Declare Function TerminateProcess Lib "kernel32" (ByVal hProcess As Integer, ByVal uExitCode As Integer) As Integer
    Private Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Private Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByRef lpThreadId As Integer) As Integer


    ' ========================================
    ' Constants
    ' ========================================
    Private Const PROCESS_QUERY_INFORMATION As Integer = &H400
    Private Const PROCESS_TERMINATE As Integer = &H1
    Private Const PROCESS_CREATE_THREAD As Integer = &H2
    Private Const PROCESS_VM_OPERATION As Integer = &H8
    Private Const PROCESS_VM_READ As Integer = &H10
    Private Const PROCESS_VM_WRITE As Integer = &H20


    ' ========================================
    ' Structures for API
    ' ========================================
    Private Structure LUID
        Public LowPart As Integer
        Public HighPart As Integer
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

    Private Structure PROCESS_BASIC_INFORMATION
        Dim ExitStatus As Integer
        Dim PEBBaseAddress As Integer
        Dim AffinityMask As Integer
        Dim BasePriority As Integer
        Dim UniqueProcessId As Integer
        Dim ParentProcessId As Integer
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


    ' Event released when instance receive a call to Refresh()
    Public Event Refreshed()

    ' ========================================
    ' Private attributes
    ' ========================================
    Friend Const NO_INFO_RETRIEVED As String = "N/A"

    ' Contains list of process names
    Friend Shared _procs As New Dictionary(Of String, String)

    ' Save informations about performance
    Friend _dicoProcMem As New SortedList(Of Integer, PROC_MEM_INFO)
    Friend _dicoProcTimes As New SortedList(Of Integer, PROC_TIME_INFO)
    Friend _dicoProcIO As New SortedList(Of Integer, PROC_IO_INFO)
    Friend _dicoProcMisc As New SortedList(Of Integer, PROC_MISC_INFO)

    Friend _intTag1 As Integer = 0


    ' ========================================
    ' Getter and setter
    ' ========================================
    Public MustOverride ReadOnly Property UserObjectsCount(Optional ByVal force As Boolean = False) As Integer
    Public MustOverride ReadOnly Property GDIObjectsCount(Optional ByVal force As Boolean = False) As Integer
    Public MustOverride ReadOnly Property CommandLine() As String
    Public MustOverride Property ProcessorCount() As Integer
    Public MustOverride ReadOnly Property Pid() As Integer
    Public MustOverride Property AffinityMask() As Integer
    Public MustOverride ReadOnly Property PEBAddress() As Integer
    Public MustOverride ReadOnly Property ParentProcessId() As Integer
    Public MustOverride ReadOnly Property ParentProcessName() As String
    Public MustOverride ReadOnly Property GetIOvalues(Optional ByVal force As Boolean = False) As cProcess.PIO_COUNTERS
    Public MustOverride ReadOnly Property FileVersionInfo(Optional ByVal force As Boolean = False) As FileVersionInfo
    Public MustOverride ReadOnly Property Path() As String
    Public MustOverride ReadOnly Property UserName() As String
    Public MustOverride ReadOnly Property Name() As String
    Public MustOverride ReadOnly Property ProcessorTime(Optional ByVal force As Boolean = False) As Date
    Public MustOverride ReadOnly Property ProcessorTimeLong() As Long
    Public MustOverride ReadOnly Property KernelTime(Optional ByVal force As Boolean = False) As Date
    Public MustOverride ReadOnly Property UserTime(Optional ByVal force As Boolean = False) As Date
    Public MustOverride ReadOnly Property MemoryInfos(Optional ByVal force As Boolean = False) As PROCESS_MEMORY_COUNTERS
    Public MustOverride ReadOnly Property Threads() As System.Diagnostics.ProcessThreadCollection
    Public MustOverride ReadOnly Property PriorityClass() As String
    Public MustOverride ReadOnly Property PriorityClassInt() As Integer
    Public MustOverride ReadOnly Property PriorityLevel() As Integer
    Public MustOverride ReadOnly Property PriorityClassConstant() As ProcessPriorityClass
    Public MustOverride ReadOnly Property AverageCpuUsage(Optional ByVal force As Boolean = False) As Double
    Public MustOverride ReadOnly Property CpuPercentageUsage(Optional ByVal force As Boolean = False) As Double
    Public MustOverride ReadOnly Property HandleCount() As Integer
    Public MustOverride ReadOnly Property StartTime() As Date
    Public MustOverride ReadOnly Property MainModule() As System.Diagnostics.ProcessModule
    Public MustOverride ReadOnly Property Modules() As System.Diagnostics.ProcessModuleCollection
    Public MustOverride ReadOnly Property MngObjProcess() As Management.ManagementObject

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
    Public Property IntTag1() As Integer
        Get
            Return _intTag1
        End Get
        Set(ByVal value As Integer)
            _intTag1 = value
        End Set
    End Property


    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Refresh infos
    Public Overridable Sub Refresh(Optional ByRef tag As Dictionary(Of String, System.Management.ManagementObject) = Nothing)
        RaiseEvent Refreshed()
    End Sub

    ' Set priority
    Public MustOverride Function SetProcessPriority(ByVal level As ProcessPriorityClass) As Integer

    ' Kill a process
    Public MustOverride Function Kill() As Integer

    ' Decrease priority
    Public Function DecreasePriority() As Integer
        Select Case Me.PriorityClassConstant
            Case ProcessPriorityClass.AboveNormal
                Return Me.SetProcessPriority(ProcessPriorityClass.Normal)
            Case ProcessPriorityClass.BelowNormal
                Return Me.SetProcessPriority(ProcessPriorityClass.Idle)
            Case ProcessPriorityClass.High
                Return Me.SetProcessPriority(ProcessPriorityClass.AboveNormal)
            Case ProcessPriorityClass.Normal
                Return Me.SetProcessPriority(ProcessPriorityClass.BelowNormal)
            Case ProcessPriorityClass.RealTime
                Return Me.SetProcessPriority(ProcessPriorityClass.High)
            Case Else
                Return -1
        End Select
    End Function

    ' Increase priority
    Public Function IncreasePriority() As Integer
        Select Case Me.PriorityClassConstant
            Case ProcessPriorityClass.AboveNormal
                Return Me.SetProcessPriority(ProcessPriorityClass.High)
            Case ProcessPriorityClass.BelowNormal
                Return Me.SetProcessPriority(ProcessPriorityClass.Normal)
            Case ProcessPriorityClass.High
                Return Me.SetProcessPriority(ProcessPriorityClass.RealTime)
            Case ProcessPriorityClass.Normal
                Return Me.SetProcessPriority(ProcessPriorityClass.AboveNormal)
            Case ProcessPriorityClass.Idle
                Return Me.SetProcessPriority(ProcessPriorityClass.BelowNormal)
            Case Else
                Return -1
        End Select
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
    Public MustOverride Function GetInformationNumerical(ByVal infoName As String) As Double

    ' Suspend a process
    Public MustOverride Function SuspendProcess() As Integer

    ' Resume a process
    Public MustOverride Function ResumeProcess() As Integer

    ' Kill a process tree
    Public MustOverride Function KillProcessTree() As Integer

    ' Empty working set size
    Public MustOverride Function EmptyWorkingSetSize() As Integer

    ' Get env variables
    Public MustOverride Function GetEnvironmentVariables(ByRef variables() As String, ByRef values() As String) As Integer

    ' ========================================
    ' Shared functions
    ' ========================================

    ' Retrieve all information's names availables
    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(35) As String

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
        s(14) = "HandleCount"
        s(15) = "WorkingSet"
        s(16) = "PeakWorkingSet"
        s(17) = "PageFaultCount"
        s(18) = "PagefileUsage"
        s(19) = "PeakPagefileUsage"
        s(20) = "QuotaPeakPagedPoolUsage"
        s(21) = "QuotaPagedPoolUsage"
        s(22) = "QuotaPeakNonPagedPoolUsage"
        s(23) = "QuotaNonPagedPoolUsage"
        s(24) = "ReadOperationCount"
        s(25) = "WriteOperationCount"
        s(26) = "OtherOperationCount"
        s(27) = "ReadTransferCount "
        s(28) = "WriteTransferCount"
        s(29) = "OtherTransferCount"
        s(30) = "Priority"
        s(31) = "Path"
        s(32) = "CommandLine"
        s(33) = "Description"
        s(34) = "Copyright"
        s(35) = "Version"

        Return s
    End Function

    ' Clear process dico
    Public Shared Sub ClearProcessDico()
        _procs.Clear()
    End Sub

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

End Class
