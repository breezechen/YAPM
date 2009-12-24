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

Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Api

Namespace Native.Objects

    Public Class Process


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Used to enumerate visible processes (simplified)
        Private Shared memAllocForVSProcesses As New Native.Memory.MemoryAlloc(&H1000)  ' NOTE : never unallocated

        ' Memory alloc for thread enumeration (kill by method)
        Private Shared memAllocForThreadEnum As New Native.Memory.MemoryAlloc(&H1000)   ' NOTE : never unallocated

        ' Used to enumerate visible processes (full)
        Private Shared memAllocForVProcesses As New Native.Memory.MemoryAlloc(&H1000)   ' NOTE : never unallocated

        ' Mem alloc for handle enumeration
        Private Shared memAllocPIDs As New Native.Memory.MemoryAlloc(&H100)             ' NOTE : never unallocated

        ' Protection for _currentProcesses
        Private Shared _semCurrentProcesses As New System.Threading.Semaphore(1, 1)

        ' Current processes running
        Private Shared _currentProcesses As Dictionary(Of String, cProcess)

        ' List of new processes
        Private Shared dicoNewProcesses As New Dictionary(Of Integer, Boolean)

        ' Protection for dicoNewProcesses
        Private Shared _semNewProcesses As New System.Threading.Semaphore(1, 1)

        ' Protection for 'kill by method'
        Private Shared _semKillByMethod As New System.Threading.Semaphore(1, 1)

        ' Delegate for process termination event handler
        Public Delegate Sub ProcessHasTerminatedHandler(ByVal ntStatus As UInt32)
        ' Associated struct
        Public Structure ProcessTerminationStruct
            Dim ProcessHandle As IntPtr
            Dim [Delegate] As ProcessHasTerminatedHandler
            Public Sub New(ByVal hProc As IntPtr, ByVal deg As ProcessHasTerminatedHandler)
                ProcessHandle = hProc
                [Delegate] = deg
            End Sub
        End Structure



        ' ========================================
        ' Public properties
        ' ========================================

        ' Min rights for Query
        Public Shared ReadOnly Property ProcessQueryMinRights() As Native.Security.ProcessAccess
            Get
                Static _minRights As Native.Security.ProcessAccess = Native.Security.ProcessAccess.QueryInformation
                Static checked As Boolean = False
                If checked = False Then
                    checked = True
                    If cEnvironment.SupportsMinRights Then
                        _minRights = Native.Security.ProcessAccess.QueryLimitedInformation
                    End If
                End If
                Return _minRights
            End Get
        End Property

        ' Current processes
        Public Shared Property CurrentProcesses() As Dictionary(Of String, cProcess)
            Get
                Return _currentProcesses
            End Get
            Set(ByVal value As Dictionary(Of String, cProcess))
                _currentProcesses = value
            End Set
        End Property
        Public Shared ReadOnly Property SemCurrentProcesses() As System.Threading.Semaphore
            Get
                Return _semCurrentProcesses
            End Get
        End Property

        ' New processes
        Public Shared Property NewProcesses() As Dictionary(Of Integer, Boolean)
            Get
                Return dicoNewProcesses
            End Get
            Set(ByVal value As Dictionary(Of Integer, Boolean))
                dicoNewProcesses = value
            End Set
        End Property
        Public Shared ReadOnly Property SemNewProcesses() As System.Threading.Semaphore
            Get
                Return _semNewProcesses
            End Get
        End Property

        ' Is a process in job ?
        Public Shared ReadOnly Property IsProcessInJob(ByVal handle As IntPtr) As Boolean
            Get
                Dim res As Boolean
                NativeFunctions.IsProcessInJob(handle, IntPtr.Zero, res)
                Return res
            End Get
        End Property

        ' Debugger present ?
        Public Shared ReadOnly Property IsDebuggerPresent(ByVal handle As IntPtr) As Boolean
            Get
                Dim value As IntPtr
                Dim retLen As UInteger
                NativeFunctions.NtQueryInformationProcess(handle, NativeEnums.ProcessInformationClass.ProcessDebugPort, value, IntPtr.Size, retLen)
                Return value.IsNotNull
            End Get
        End Property

        ' Is Wow64 ?
        Public Shared ReadOnly Property IsWow64Process(ByVal handle As IntPtr) As Boolean
            Get
                Dim value As IntPtr
                Dim retLen As UInteger
                NativeFunctions.NtQueryInformationProcess(handle, NativeEnums.ProcessInformationClass.ProcessWow64Information, value, IntPtr.Size, retLen)
                Return value.IsNotNull
            End Get
        End Property



        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Clear new process dico
        Public Shared Sub ClearNewProcessesDico()
            dicoNewProcesses.Clear()
        End Sub


        ' Set affinity to a process
        Public Shared Function SetProcessAffinityByHandle(ByVal hProc As IntPtr, _
                                           ByVal affinity As Integer) As Boolean
            If hProc.IsNotNull Then
                Return NativeFunctions.SetProcessAffinityMask(hProc, New IntPtr(affinity))
            Else
                Return False
            End If
        End Function
        Public Shared Function SetProcessAffinityById(ByVal process As Integer, _
                                           ByVal affinity As Integer) As Boolean

            ' Open handle, set affinity and close handle
            Dim hProc As IntPtr = _
                    Native.Objects.Process.GetProcessHandleById(process, Security.ProcessAccess.SetInformation)
            Dim ret As Boolean = SetProcessAffinityByHandle(hProc, affinity)
            NativeFunctions.CloseHandle(hProc)

            Return ret
        End Function

        ' Set priority
        Public Shared Function SetProcessPriorityById(ByVal pid As Integer, _
                    ByVal priority As System.Diagnostics.ProcessPriorityClass) As Boolean

            Dim hProc As IntPtr
            Dim r As Boolean
            hProc = Native.Objects.Process.GetProcessHandleById(pid, Security.ProcessAccess.SetInformation)

            If hProc.IsNotNull Then
                r = NativeFunctions.SetPriorityClass(hProc, priority)
                NativeFunctions.CloseHandle(hProc)
                Return r
            Else
                Return False
            End If

        End Function

        ' Resume a process
        Public Shared Function ResumeProcessById(ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr
            Dim r As UInteger
            hProc = Native.Objects.Process.GetProcessHandleById(pid, _
                                                        Security.ProcessAccess.SuspendResume)
            If hProc.IsNotNull Then
                r = NativeFunctions.NtResumeProcess(hProc)
                NativeFunctions.CloseHandle(hProc)
                Return (r <> 0)
            Else
                Return False
            End If
        End Function

        ' Suspend a process
        Public Shared Function SuspendProcessById(ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr
            Dim r As UInteger
            hProc = Native.Objects.Process.GetProcessHandleById(pid, _
                                                        Security.ProcessAccess.SuspendResume)
            If hProc.IsNotNull Then
                r = NativeFunctions.NtSuspendProcess(hProc)
                NativeFunctions.CloseHandle(hProc)
                Return (r <> 0)
            Else
                Return False
            End If
        End Function

        ' Kill a process
        Public Shared Function KillProcessById(ByVal pid As Integer, _
                                               Optional ByVal exitcode As Integer = 0) As Boolean
            Dim hProc As IntPtr
            Dim ret As UInteger
            hProc = Native.Objects.Process.GetProcessHandleById(pid, _
                                                        Security.ProcessAccess.Terminate)
            If hProc.IsNotNull Then
                ret = NativeFunctions.NtTerminateProcess(hProc, exitcode)
                NativeFunctions.CloseHandle(hProc)
                Return ret = 0
            Else
                Return False
            End If
        End Function

        ' Get PIDs of child processes
        Public Shared Function EnumerateChildProcessesById(ByVal pid As Integer) As List(Of Integer)
            Dim ret As Integer
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                                                                IntPtr.Zero, 0, ret)
            Dim size As Integer = ret
            Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                                                                ptr, size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Dim _list As New List(Of Integer)
            Do While True
                Dim obj As NativeStructs.SystemProcessInformation = CType(Marshal.PtrToStructure(ptr.Increment(offset), _
                                                                                                            GetType(NativeStructs.SystemProcessInformation)),  _
                                                                                                            NativeStructs.SystemProcessInformation)
                offset += obj.NextEntryOffset
                If obj.InheritedFromProcessId = pid Then
                    _list.Add(obj.ProcessId)
                End If
                If obj.NextEntryOffset = 0 Then
                    Exit Do
                End If
                x += 1
            Loop
            Marshal.FreeHGlobal(ptr)

            Return _list
        End Function

        ' Create a minidump
        Public Shared Function CreateMiniDumpFileById(ByVal pid As Integer, _
                                                      ByVal file As String, _
                                                      ByVal type As NativeEnums.MiniDumpType) As Boolean
            Dim hProc As IntPtr
            Dim ret As Integer = -1
            hProc = Native.Objects.Process.GetProcessHandleById(pid, _
                                            Security.ProcessAccess.QueryInformation Or Security.ProcessAccess.VmRead)
            If hProc.IsNotNull Then
                ' Create dump file
                Dim fs As New System.IO.FileStream(file, System.IO.FileMode.Create)
                ' Write dump file
                NativeFunctions.MiniDumpWriteDump(hProc, pid, _
                                            fs.SafeFileHandle.DangerousGetHandle(), _
                                            type, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero)
                NativeFunctions.CloseHandle(hProc)
                fs.Close()
                Return ret <> 0
            Else
                Return False
            End If

        End Function

        ' Get process affinity
        Public Shared Function GetProcessAffinityByHandle(ByVal handle As IntPtr) As IntPtr
            Dim pbi As New NativeStructs.ProcessBasicInformation
            Dim ret As Integer
            NativeFunctions.NtQueryInformationProcess(handle, _
                    NativeEnums.ProcessInformationClass.ProcessBasicInformation, _
                    pbi, _
                    Marshal.SizeOf(pbi), _
                    ret)
            Return pbi.AffinityMask
        End Function

        ' Get GUI resource info
        Public Shared Function GetProcessGuiResourceByHandle(ByVal handle As IntPtr, ByVal type As NativeEnums.GuiResourceType) As Integer
            Return NativeFunctions.GetGuiResources(handle, type)
        End Function

        ' Empty WS
        Public Shared Function EmptyProcessWorkingSetById(ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(pid, _
                                            Security.ProcessAccess.SetQuota)
            If hProc.IsNotNull Then
                Dim ret As Boolean = NativeFunctions.SetProcessWorkingSetSize(hProc, _
                                            NativeConstants.InvalidHandleValue, _
                                            NativeConstants.InvalidHandleValue)
                NativeFunctions.CloseHandle(hProc)
                Return ret
            Else
                Return False
            End If
        End Function

        ' Unload a module (by address)
        Public Shared Function UnloadProcessModuleByAddress(ByVal address As IntPtr, _
                                                            ByVal pid As Integer) As Boolean
            Return Objects.Module.UnloadModuleByAddress(address, pid)
        End Function

        ' Return process path
        Public Shared Function GetProcessPathById(ByVal pid As Integer) As String

            ' This is not trivial to get the process path for ALL processes
            ' The first method we'll use is the native method (using NtQueryInformationProcess)
            ' If it fails, we use the new QueryFullProcessImageName function (Vista and above)
            ' If it fails, we try to retrieve file name from module
            ' If it fails, ... we consider it has FAILED -__-

            Dim resFile As String = Nothing
            Dim hProc As IntPtr

            ' 1) Native method
            If pid = 0 Then
                Return "Idle process"
            ElseIf pid = 4 Then
                Return [Module].KernelFilePath
            Else

                ' Have to open a handle
                hProc = Native.Objects.Process.GetProcessHandleById(pid, _
                                                    Process.ProcessQueryMinRights)

                If hProc.IsNotNull Then
                    ' Get size
                    Dim _size As Integer
                    NativeFunctions.NtQueryInformationProcess(hProc, _
                                NativeEnums.ProcessInformationClass.ProcessImageFileName, _
                                IntPtr.Zero, 0, _size)

                    If _size > 0 Then

                        ' Retrieve unicode string
                        Dim _pt As IntPtr = Marshal.AllocHGlobal(_size)
                        NativeFunctions.NtQueryInformationProcess(hProc, _
                                NativeEnums.ProcessInformationClass.ProcessImageFileName, _
                                _pt, _size, _size)

                        ' Read it
                        Dim _str As NativeStructs.UnicodeString = _
                                    CType(Marshal.PtrToStructure(_pt, GetType(NativeStructs.UnicodeString)),  _
                                    NativeStructs.UnicodeString)

                        Marshal.FreeHGlobal(_pt)
                        Dim _stemp As String = Common.Misc.ReadUnicodeString(_str)

                        If _stemp IsNot Nothing Then
                            resFile = Common.Misc.DeviceDriveNameToDosDriveName(_stemp)
                        End If
                    End If
                End If
            End If

            ' If it works, it's OK...
            resFile = Common.Misc.GetWellFormatedPath(resFile)
            If System.IO.File.Exists(resFile) Then
                NativeFunctions.CloseHandle(hProc)
                Return resFile
            End If


            ' 2) QueryFullProcessImageName on Vista and above
            If cEnvironment.SupportsQueryFullProcessImageNameFunction Then
                If hProc.IsNotNull Then
                    Dim length As Integer = &H400
                    Dim sb As New System.Text.StringBuilder(length)
                    NativeFunctions.QueryFullProcessImageName(hProc, False, sb, length)
                    Try
                        resFile = sb.ToString(0, sb.Length)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try
                End If
            End If

            ' If it works, it's OK...
            resFile = Common.Misc.GetWellFormatedPath(resFile)
            If System.IO.File.Exists(resFile) Then
                NativeFunctions.CloseHandle(hProc)
                Return resFile
            End If


            ' 3) Everything failed, we'll use the main module...

            Dim Ret As Integer
            Dim sResult As New System.Text.StringBuilder(&H400)
            Dim hModule As IntPtr

            NativeFunctions.EnumProcessModules(hProc, hModule, 4, Ret)
            NativeFunctions.GetModuleFileNameEx(hProc, hModule, sResult, 260)
            NativeFunctions.CloseHandle(hProc)
            resFile = sResult.ToString

            If InStr(resFile, vbNullChar) > 1 Then
                resFile = Left(resFile, InStr(resFile, vbNullChar) - 1)
            End If

            resFile = Common.Misc.GetWellFormatedPath(resFile)
            If System.IO.File.Exists(resFile) = False Then
                ' EPIC FAIL !
                Return NO_INFO_RETRIEVED
            End If

            Return resFile
        End Function

        ' Return Peb address
        Public Shared Function GetProcessPebAddressById(ByVal pid As Integer) As IntPtr
            If pid > 4 Then
                Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(pid, _
                                            Process.ProcessQueryMinRights)
                Dim pbi As New NativeStructs.ProcessBasicInformation
                Dim ret As Integer
                NativeFunctions.NtQueryInformationProcess(hProc, _
                                NativeEnums.ProcessInformationClass.ProcessBasicInformation, _
                                pbi, Marshal.SizeOf(pbi), ret)
                NativeFunctions.CloseHandle(hProc)
                Return pbi.PebBaseAddress
            Else
                Return IntPtr.Zero
            End If
        End Function

        ' Return user
        Public Shared Function GetProcessUserDomainNameById(ByVal pid As Integer, _
                                            ByRef username As String, _
                                            ByRef domain As String) As Boolean

            Dim retLen As Integer
            Dim _UserName As String

            If pid > 4 Then

                Dim hToken As IntPtr
                Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(pid, _
                                            Process.ProcessQueryMinRights)

                If NativeFunctions.OpenProcessToken(hProc, Native.Security.TokenAccess.Query, hToken) Then

                    NativeFunctions.GetTokenInformation(hToken, _
                                        NativeEnums.TokenInformationClass.TokenUser, _
                                        IntPtr.Zero, 0, retLen)
                    Dim data As IntPtr = Marshal.AllocHGlobal(retLen)
                    NativeFunctions.GetTokenInformation(hToken, _
                                        NativeEnums.TokenInformationClass.TokenUser, _
                                        data, retLen, retLen)

                    NativeFunctions.CloseHandle(hProc)
                    NativeFunctions.CloseHandle(hToken)

                    Dim user As New NativeStructs.TokenUser
                    user = CType(Marshal.PtrToStructure(data, _
                                    GetType(NativeStructs.TokenUser)),  _
                                    NativeStructs.TokenUser)

                    Objects.Token.GetAccountNameFromSid(user.User.Sid, username, domain)
                    Marshal.FreeHGlobal(data)

                    Return True
                Else
                    _UserName = ""
                    Return False
                End If

            Else
                domain = ""
                username = ""
                Return False
            End If
        End Function

        ' Return the command line
        ' Second parameter is optional
        Public Shared Function GetProcessCommandLineById(ByVal pid As Integer, ByVal pebAddress As IntPtr) As String

            Try
                Dim res As String = ""

                ' Get PEB address of process (from parameter or using GetProcessPebAddressById) 
                If pebAddress.IsNull Then
                    pebAddress = GetProcessPebAddressById(pid)
                End If
                If pebAddress.IsNull Then
                    Return ""
                End If

                ' Create a reader class to read in memory
                Using memReader As New ProcessMemReader(pid)

                    If memReader.ProcessHandle.IsNull Then
                        Return NO_INFO_RETRIEVED           ' Couldn't open a handle
                    End If

                    ' Retrieve process parameters block address
                    Dim __procParamAd As IntPtr = memReader.ReadIntPtr(pebAddress.Increment(NativeStructs.Peb_ProcessParametersOffset))

                    ' Get unicode string adress
                    Dim cmdLine As NativeStructs.UnicodeString

                    ' Read length of the unicode string
                    Dim cmdLineOffset As IntPtr = __procParamAd.Increment(NativeStructs.ProcParamBlock_CommandLineOffset)
                    cmdLine.Length = CUShort(memReader.ReadInt32(cmdLineOffset))
                    cmdLine.MaximumLength = CUShort(cmdLine.Length + 2) ' Not used, but...

                    ' Read pointer to the string
                    ' offset = cmdLineOffset + sizeof(IntPtr.Size) for unicode_string.size
                    cmdLine.Buffer = memReader.ReadIntPtr(cmdLineOffset.Increment(IntPtr.Size))

                    ' Read the string
                    res = memReader.ReadUnicodeString(cmdLine)

                End Using

                Return res

            Catch ex As Exception
                Return NO_INFO_RETRIEVED
            End Try

        End Function

        ' Get all visible processes (simplified)
        Public Shared Function EnumerateVisibleProcessesSimplified() As Dictionary(Of String, processInfos)

            ' Refresh list of drives
            Common.Misc.RefreshLogicalDrives()

            Dim ret As Integer
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForVSProcesses.Pointer, memAllocForVSProcesses.Size, ret)
            If memAllocForVSProcesses.Size < ret Then
                memAllocForVSProcesses.Resize(ret)
            End If
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForVSProcesses.Pointer, memAllocForVSProcesses.Size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Dim _dico As New Dictionary(Of String, processInfos)
            Do While True

                Dim obj As NativeStructs.SystemProcessInformation = _
                        memAllocForVSProcesses.ReadStructOffset(Of NativeStructs.SystemProcessInformation)(offset)
                Dim _procInfos As New processInfos(obj.ToSystemProcessInformation64)

                Dim _path As String = GetProcessPathById(obj.ProcessId)
                With _procInfos
                    .Path = _path
                    .UserName = NO_INFO_RETRIEVED
                    .CommandLine = NO_INFO_RETRIEVED
                    .FileInfo = Nothing
                    .PebAddress = IntPtr.Zero
                End With

                offset += obj.NextEntryOffset
                Dim sKey As String = obj.ProcessId.ToString
                If _dico.ContainsKey(sKey) = False Then
                    _dico.Add(sKey, _procInfos)
                End If

                If obj.NextEntryOffset = 0 Then
                    Exit Do
                End If
                x += 1
            Loop

            Return _dico
        End Function

        ' Get visible processes (full)
        ''' <summary>
        ''' Enumerate processes
        ''' MUST BE protected by _semNewProcesses
        ''' </summary>
        Public Shared Function EnumerateVisibleProcesses(Optional ByVal forceAllInfos As Boolean = False) As Dictionary(Of String, processInfos)

            ' Refresh list of drives
            Common.Misc.RefreshLogicalDrives()

            Dim ret As Integer
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForVProcesses.Pointer, memAllocForVProcesses.Size, ret)
            If memAllocForVProcesses.Size < ret Then
                memAllocForVProcesses.Resize(ret)
            End If
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForVProcesses.Pointer, memAllocForVProcesses.Size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Dim _dico As New Dictionary(Of String, processInfos)
            Do While True

                Dim obj As NativeStructs.SystemProcessInformation = _
                        memAllocForVProcesses.ReadStructOffset(Of NativeStructs.SystemProcessInformation)(offset)
                Dim _procInfos As New processInfos(obj.ToSystemProcessInformation64)


                ' Do we have to get fixed infos ?
                If forceAllInfos OrElse dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then

                    Dim _path As String = GetProcessPathById(obj.ProcessId)
                    Dim _domain As String = Nothing
                    Dim _user As String = Nothing
                    GetProcessUserDomainNameById(obj.ProcessId, _user, _domain)
                    Dim _command As String = NO_INFO_RETRIEVED
                    Dim _peb As IntPtr = GetProcessPebAddressById(obj.ProcessId)
                    If _peb.IsNotNull Then
                        _command = GetProcessCommandLineById(obj.ProcessId, _peb)
                    ElseIf obj.ProcessId = 4 Then
                        ' System process -> custom command line
                        _command = "System process"
                    End If
                    Dim _finfo As SerializableFileVersionInfo = Nothing
                    If IO.File.Exists(_path) Then
                        Try
                            _finfo = New SerializableFileVersionInfo(FileVersionInfo.GetVersionInfo(_path))
                        Catch ex As Exception
                            ' File not available or ?
                        End Try
                    End If

                    With _procInfos
                        .Path = _path
                        .UserName = _user
                        .DomainName = _domain
                        .CommandLine = _command
                        .FileInfo = _finfo
                        .PebAddress = _peb
                        .HasReanalize = True
                    End With

                    ' Have to check if key already exists if we force retrieving
                    ' of all informations, else it has already be done before
                    If forceAllInfos = False OrElse dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then
                        dicoNewProcesses.Add(obj.ProcessId, False)
                    End If

                    Trace.WriteLine("Got fixed infos for id = " & obj.ProcessId.ToString)
                End If

                ' Set true so that the process is marked as existing
                dicoNewProcesses(obj.ProcessId) = True

                offset += obj.NextEntryOffset
                Dim sKey As String = obj.ProcessId.ToString
                If _dico.ContainsKey(sKey) = False Then
                    _dico.Add(sKey, _procInfos)
                End If

                If obj.NextEntryOffset = 0 Then
                    Exit Do
                End If
                x += 1
            Loop


            ' Remove all processes that not exist anymore
            Dim _dicoTemp As Dictionary(Of Integer, Boolean) = dicoNewProcesses
            For Each it As System.Collections.Generic.KeyValuePair(Of Integer, Boolean) In _dicoTemp
                If it.Value = False Then
                    dicoNewProcesses.Remove(it.Key)
                End If
            Next

            ' Here we fill _currentProcesses if necessary
            'PERFISSUE
            SemCurrentProcesses.WaitOne()
            If CurrentProcesses Is Nothing Then
                CurrentProcesses = New Dictionary(Of String, cProcess)
            End If
            For Each pc As processInfos In _dico.Values
                If CurrentProcesses.ContainsKey(pc.ProcessId.ToString) = False Then
                    CurrentProcesses.Add(pc.ProcessId.ToString, New cProcess(pc))
                End If
            Next
            SemCurrentProcesses.Release()

            Return _dico
        End Function

        ' Get all hidden processes (handle method)
        Public Shared Function EnumerateHiddenProcessesHandleMethod() As Dictionary(Of String, processInfos)

            ' Refresh list of drives
            Common.Misc.RefreshLogicalDrives()

            ' For each Process Id (PID) possible
            Dim _dico As New Dictionary(Of String, processInfos)

            ' Firstly, we get all instances of csrss.exe process.
            ' We retrieve them from visible list. So if csrss.exe processes are hidden... DAMN !!!
            ' Note : There are more than once instance of csrss.exe on Vista.
            Dim _csrss As New Dictionary(Of Integer, IntPtr)
            For Each proc As processInfos In EnumerateVisibleProcessesSimplified.Values
                If proc.Name.ToLowerInvariant = "csrss.exe" Then
                    Dim _theHandle As IntPtr = Native.Objects.Process.GetProcessHandleById(proc.ProcessId, _
                                            Security.ProcessAccess.DupHandle)
                    _csrss.Add(proc.ProcessId, _theHandle)
                End If
            Next

            ' Now we get all handles from all processes
            Dim _handles As NativeStructs.SystemHandleEntry() = EnumerateProcessHandles()

            ' For handles which belongs to a csrss.exe process
            For Each h As NativeStructs.SystemHandleEntry In _handles
                If _csrss.ContainsKey(h.ProcessId) Then
                    Dim _dup As IntPtr
                    ' ISNEEDED ?
                    If NativeFunctions.DuplicateHandle(_csrss(h.ProcessId), CType(h.Handle, IntPtr), NativeConstants.InvalidHandleValue, _dup, 0, False, NativeEnums.DuplicateOptions.SameAccess) Then
                        Dim pid As Integer = NativeFunctions.GetProcessId(_dup)
                        Dim obj As New NativeStructs.SystemProcessInformation
                        With obj
                            .ProcessId = pid
                        End With
                        Dim _path As String = GetProcessPathById(obj.ProcessId)
                        Dim _procInfos As New processInfos(obj.ToSystemProcessInformation64, cFile.GetFileName(_path))
                        _procInfos.Path = _path
                        If _dico.ContainsKey(pid.ToString) = False Then
                            _dico.Add(pid.ToString, _procInfos)
                        End If
                    End If
                    NativeFunctions.CloseHandle(_dup)
                End If
            Next

            ' Add the two instances of csrss.exe to result
            ' & close previously opened handles
            For Each h As Integer In _csrss.Keys
                Dim obj As New NativeStructs.SystemProcessInformation
                With obj
                    .ProcessId = h
                End With
                Dim _path As String = GetProcessPathById(obj.ProcessId)
                Dim _procInfos As New processInfos(obj.ToSystemProcessInformation64, cFile.GetFileName(_path))
                _procInfos.Path = _path
                If _dico.ContainsKey(h.ToString) = False Then
                    _dico.Add(h.ToString, _procInfos)
                End If

                NativeFunctions.CloseHandle(_csrss(h))
            Next


            ' Get visible processes
            Dim _visible As Dictionary(Of String, processInfos) = EnumerateVisibleProcessesSimplified()

            ' Merge results
            For Each pp As processInfos In _visible.Values
                If _dico.ContainsKey(pp.ProcessId.ToString) = False Then
                    _dico.Add(pp.ProcessId.ToString, pp)
                End If
            Next

            ' Mark processes that are not present in _visible as hidden
            For Each pp As processInfos In _dico.Values
                If _visible.ContainsKey(pp.ProcessId.ToString) = False Then
                    pp.IsHidden = True
                End If
            Next

            Return _dico

        End Function

        ' Get all hidden processes (brute force)
        Public Shared Function EnumerateHiddenProcessesBruteForce() As Dictionary(Of String, processInfos)

            ' Refresh list of drives
            Common.Misc.RefreshLogicalDrives()

            ' For each Process Id (PID) possible
            Dim _dico As New Dictionary(Of String, processInfos)

            ' For each PID...
            For pid As Integer = &H8 To &HFFFF Step 4
                Dim handle As IntPtr = Native.Objects.Process.GetProcessHandleById(pid, _
                                            Process.ProcessQueryMinRights)
                If handle.IsNotNull Then
                    Dim exitcode As Integer
                    Dim res As Boolean = NativeFunctions.GetExitCodeProcess(handle, exitcode)
                    If exitcode = NativeConstants.STILL_ACTIVE Then  ' Process still exists
                        Dim obj As New NativeStructs.SystemProcessInformation
                        With obj
                            .ProcessId = pid
                        End With
                        Dim _path As String = GetProcessPathById(obj.ProcessId)
                        Dim _procInfos As New processInfos(obj.ToSystemProcessInformation64, cFile.GetFileName(_path))
                        _procInfos.Path = _path
                        Dim sKey As String = pid.ToString
                        If _dico.ContainsKey(sKey) = False Then
                            _dico.Add(sKey, _procInfos)
                        End If
                    End If
                    NativeFunctions.CloseHandle(handle)
                End If
            Next

            ' Get visible processes
            Dim _visible As Dictionary(Of String, processInfos) = EnumerateVisibleProcessesSimplified()

            ' Merge results
            For Each pp As processInfos In _visible.Values
                If _dico.ContainsKey(pp.ProcessId.ToString) = False Then
                    _dico.Add(pp.ProcessId.ToString, pp)
                End If
            Next

            ' Mark processes that are not present in _visible as hidden
            For Each pp As processInfos In _dico.Values
                If _visible.ContainsKey(pp.ProcessId.ToString) = False Then
                    pp.IsHidden = True
                End If
            Next

            Return _dico

        End Function

        ' Get a service by name
        Public Shared Function GetProcessById(ByVal id As Integer) As cProcess

            Dim tt As cProcess = Nothing
            Native.Objects.Process.SemCurrentProcesses.WaitOne()
            If _currentProcesses IsNot Nothing Then
                If _currentProcesses.ContainsKey(id.ToString) Then
                    tt = _currentProcesses.Item(id.ToString)
                End If
            End If
            Native.Objects.Process.SemCurrentProcesses.Release()

            Return tt

        End Function

        ' Return a handle for a process
        Public Shared Function GetProcessHandleById(ByVal pid As Integer, ByVal access As Security.ProcessAccess) As IntPtr
            Return NativeFunctions.OpenProcess(access, False, pid)
        End Function

        ' Kill a process by method
        ' Suspend calling thread 3 seconds !!
        Public Shared Function KillProcessByMethod(ByVal pid As Integer, _
                                ByVal method As Enums.KillMethod) As Boolean

            _semKillByMethod.WaitOne()

            If (method And Enums.KillMethod.NtTerminate) = Enums.KillMethod.NtTerminate Then
                KillByMethod_NtTerminateProcess(pid)
            End If
            If (method And Enums.KillMethod.ThreadTerminate) = Enums.KillMethod.ThreadTerminate Then
                KillByMethod_NtTerminateThread(pid)
            End If
            If (method And Enums.KillMethod.ThreadTerminate_GetNextThread) = Enums.KillMethod.ThreadTerminate_GetNextThread Then
                KillByMethod_NtTerminateThreadNt(pid)
            End If
            If (method And Enums.KillMethod.CreateRemoteThread) = Enums.KillMethod.CreateRemoteThread Then
                KillByMethod_CreateRemoteThread(pid)
            End If
            If (method And Enums.KillMethod.CloseAllHandles) = Enums.KillMethod.CloseAllHandles Then
                KillByMethod_CloseAllHandles(pid)
            End If
            If (method And Enums.KillMethod.CloseAllWindows) = Enums.KillMethod.CloseAllWindows Then
                KillByMethod_CloseAllWindows(pid)
            End If
            If (method And Enums.KillMethod.TerminateJob) = Enums.KillMethod.TerminateJob Then
                KillByMethod_TerminateJobObject(pid)
            End If

            ' Wait process to crash...
            System.Threading.Thread.Sleep(3000)

            ' Now retrieve exitCode to see if process is still running or not
            Dim ret As Boolean
            Dim exitC As Integer
            Dim hProc As IntPtr = GetProcessHandleWById(pid, Process.ProcessQueryMinRights)
            If hProc.IsNotNull Then
                NativeFunctions.GetExitCodeProcess(hProc, exitC)
                ret = (exitC <> NativeConstants.STILL_ACTIVE)
                Native.Objects.General.CloseHandle(hProc)
            Else
                ret = False
            End If

            _semKillByMethod.Release()
            Return ret

        End Function


        ' Wait for a process to terminate
        ' Must be called in another thread
        ' Context.ProcessHanlde need Synchronize Or QueryMinInformation access
        Public Shared Sub WaitForProcessToTerminate(ByVal context As Object)

            Dim pObj As ProcessTerminationStruct = CType(context,  _
                                                            ProcessTerminationStruct)

            If pObj.ProcessHandle.IsNotNull Then
                ' Wait process to terminate (infinite timeout)
                ' -1 != INFINITE, but if we use the INFINITE value (Const INFINITE = &HFFFF)
                ' it just waits 65 seconds... Oh yeah -__-
                Dim ret As NativeEnums.WaitResult = _
                        NativeFunctions.WaitForSingleObject(pObj.ProcessHandle, -1)

                ' Get exit code
                Dim exCode As UInteger
                NativeFunctions.GetExitCodeProcess(pObj.ProcessHandle, exCode)

                If pObj.[Delegate] IsNot Nothing Then
                    Try
                        pObj.[Delegate].Invoke(exCode)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try
                End If
            End If

        End Sub





        ' ========================================
        ' Private functions
        ' ========================================

        ' Enumerate all handes opened by all processes
        Private Shared Function EnumerateProcessHandles() As NativeStructs.SystemHandleEntry()
            Dim handleCount As Integer = 0
            Dim retLen As Integer
            Dim _handles As NativeStructs.SystemHandleEntry()

            ' I did not manage to get the good needed size with the first call to
            ' NtQuerySystemInformation with SystemHandleInformation flag when the buffer
            ' is too small. So each time the call to NtQuerySystemInformation fails with
            ' a too small buffer, the size is multiplicated by 2 and I call NtQuerySystemInformation
            ' again. And again, until the return is not STATUS_INFO_LENGTH_MISMATCH.
            ' Strange behavior.
            ' See http://forum.sysinternals.com/forum_posts.asp?TID=3577 for details.
            Const STATUS_INFO_LENGTH_MISMATCH As UInteger = &HC0000004

            Dim size As Integer = &H400
            Using memAlloc As New Memory.MemoryAlloc(size)

                While CUInt(NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, memAlloc.Pointer, size, retLen)) = STATUS_INFO_LENGTH_MISMATCH
                    size *= 2
                    memAlloc.Resize(size)
                End While

                handleCount = memAlloc.ReadStruct(Of NativeStructs.SystemHandleInformation).HandleCount
                _handles = New NativeStructs.SystemHandleEntry(handleCount - 1) {}
                Dim handlesOffset As Integer = NativeStructs.SystemHandleInformation.HandlesOffset

                For x As Integer = 0 To handleCount - 1
                    Dim temp As NativeStructs.SystemHandleEntry = _
                        memAlloc.ReadStruct(Of NativeStructs.SystemHandleEntry)(handlesOffset, x)

                    _handles(x) = temp
                Next

            End Using

            Return _handles

        End Function

        ' Open a handle (another method if OpenProcess failed)
        Private Shared Function GetProcessHandleWById(ByVal pid As Integer, ByVal access As Security.ProcessAccess) As IntPtr

            ' ===== Try standard way
            Dim hProc As IntPtr = GetProcessHandleById(pid, access)
            If hProc.IsNotNull Then
                Return hProc
            End If


            ' ===== Use NtOpenProcess (if OpenProcess is hooked and not NtOpenProcess)
            Dim _oa As NativeStructs.ObjectAttributes
            Dim _clientId As New NativeStructs.ClientId(pid, 0)
            NativeFunctions.NtOpenProcess(hProc, access, _oa, _clientId)
            If hProc.IsNotNull Then
                Return hProc
            End If


            ' ===== Try another way (using NtGetNextProcess, VISTA ONLY)
            If cEnvironment.SupportsGetNextThreadProcessFunctions Then
                ' Open handle to our process

                Dim curHandle As IntPtr = GetProcessHandleById(NativeFunctions.GetCurrentProcessId, access)
                ' Define access to use
                Dim theAccess As Security.ProcessAccess
                If (access And Security.ProcessAccess.QueryLimitedInformation) <> Security.ProcessAccess.QueryLimitedInformation AndAlso _
                        (access And Security.ProcessAccess.QueryInformation) <> Security.ProcessAccess.QueryInformation Then
                    theAccess = access Or Security.ProcessAccess.QueryLimitedInformation
                Else
                    theAccess = access
                End If
                ' Try to find a handle using NtGetNextProcess
                Dim i As Integer = 0        ' Watchdog
                Do While True
                    NativeFunctions.NtGetNextProcess(curHandle, access, 0, 0, curHandle)
                    ' Get process Id of this handle
                    If curHandle.IsNotNull Then
                        Dim thePid As Integer = NativeFunctions.GetProcessId(curHandle)
                        If thePid = pid Then
                            Return curHandle
                        End If
                    End If
                    i += 1
                    ' We assume there are less than 800 processes...
                    If i > 800 Then
                        Exit Do
                    End If
                Loop
            End If

            ' Okay, everything failed....
            Return IntPtr.Zero

        End Function

#Region "Termination methods"

        ' Standard 'NtTerminateProcess' call
        Private Shared Sub KillByMethod_NtTerminateThreadNt(ByVal pid As Integer)

            If cEnvironment.SupportsGetNextThreadProcessFunctions = False Then
                Exit Sub
            End If

            Dim hProc As IntPtr = GetProcessHandleWById(pid, Security.ProcessAccess.QueryInformation)
            If hProc.IsNotNull Then

                ' Try to find a handle using NtGetNextProcess
                Dim curHandle As IntPtr
                Dim i As Integer = 0        ' Watchdog
                Do While True
                    NativeFunctions.NtGetNextThread(hProc, curHandle, Security.ThreadAccess.Terminate, 0, 0, curHandle)
                    ' Get process Id of this handle
                    If curHandle.IsNotNull Then
                        Objects.Thread.KillThreadByHandle(curHandle)
                    End If
                    i += 1
                    ' I assume there are less than 800 threads...
                    If i > 800 Then
                        Exit Do
                    End If
                Loop

                Objects.General.CloseHandle(hProc)
            End If

        End Sub

        ' Kill process using NtTerminateProcess
        Private Shared Function KillByMethod_NtTerminateProcess(ByVal pid As Integer) As Boolean
            Dim ret As Boolean = False
            Dim hProc As IntPtr = GetProcessHandleWById(pid, Security.ProcessAccess.Terminate)
            If hProc.IsNotNull Then
                ret = (Api.NativeFunctions.NtTerminateProcess(hProc, 0) = 0)
                Objects.General.CloseHandle(hProc)
            End If
            Return ret
        End Function

        ' Suspend and then kill all threads
        Private Shared Sub KillByMethod_NtTerminateThread(ByVal pid As Integer)

            Dim deltaOff As Integer = Marshal.SizeOf(GetType(NativeStructs.SystemProcessInformation))

            Dim ret As Integer
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)
            If memAllocForThreadEnum.Size < ret Then
                memAllocForThreadEnum.Resize(ret)
            End If
            NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForThreadEnum.Pointer, memAllocForThreadEnum.Size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Dim listOfThread As New List(Of NativeStructs.SystemThreadInformation)
            Do While True

                Dim obj As NativeStructs.SystemProcessInformation = _
                        memAllocForThreadEnum.ReadStructOffset(Of NativeStructs.SystemProcessInformation)(offset)

                If pid = obj.ProcessId Then

                    For j As Integer = 0 To obj.NumberOfThreads - 1

                        Dim thread As NativeStructs.SystemThreadInformation = _
                            memAllocForThreadEnum.ReadStruct(Of NativeStructs.SystemThreadInformation)(offset + deltaOff, j)
                        listOfThread.Add(thread)
                    Next
                    Exit Do
                End If

                offset += obj.NextEntryOffset

                If obj.NextEntryOffset = 0 Then
                    Exit Do
                End If
                x += 1
            Loop

            ' Now we suspend all threads
            For Each th As NativeStructs.SystemThreadInformation In listOfThread
                Native.Objects.Thread.SuspendThreadById(th.ClientId.UniqueThread.ToInt32)
            Next

            ' Now we terminate all threads
            For Each th As NativeStructs.SystemThreadInformation In listOfThread
                Native.Objects.Thread.KillThreadById(th.ClientId.UniqueThread.ToInt32)
            Next
        End Sub

        ' Create a remote thread and then call ExitProcess
        Private Shared Sub KillByMethod_CreateRemoteThread(ByVal pid As Integer)
            ' NOT YET IMPLEMENTED
        End Sub

        ' Assign to a new job a close the job
        Private Shared Function KillByMethod_TerminateJobObject(ByVal pid As Integer) As Boolean
            Dim ret As Boolean = False
            Dim hProc As IntPtr = GetProcessHandleWById(pid, _
                                Security.ProcessAccess.Terminate Or _
                                Security.ProcessAccess.SetQuota)

            If hProc.IsNotNull Then

                ' Create new job
                Dim hJob As IntPtr
                Dim sa As New NativeStructs.SecurityAttributes
                With sa
                    .nLength = Marshal.SizeOf(sa)
                    .bInheritHandle = True
                    .lpSecurityDescriptor = IntPtr.Zero
                End With
                ' We have all access to the job cause we created it
                ' Random name
                hJob = NativeFunctions.CreateJobObject(sa, Api.Win32.GetElapsedTime.ToString)
                If hJob.IsNotNull Then
                    ' Assign process to the job and terminate it
                    If NativeFunctions.AssignProcessToJobObject(hJob, hProc) Then
                        ret = NativeFunctions.TerminateJobObject(hJob, 0)
                    End If

                    Objects.General.CloseHandle(hJob)
                End If

                Objects.General.CloseHandle(hProc)
            End If
            Return ret
        End Function

        ' Close all handles
        Private Shared Sub KillByMethod_CloseAllHandles(ByVal pid As Integer)
            Dim hProc As IntPtr = GetProcessHandleWById(pid, Security.ProcessAccess.DupHandle)
            If hProc.IsNotNull Then

                ' Close all handles (brute force)
                For i As Integer = 0 To &HFFFF Step 4
                    Dim ptrRet As IntPtr
                    NativeFunctions.NtDuplicateObject(hProc, New IntPtr(i), IntPtr.Zero, _
                                                      ptrRet, 0, 0, _
                                                      NativeEnums.DuplicateOptions.CloseSource)
                Next

                Objects.General.CloseHandle(hProc)

            End If
        End Sub

        ' Close all handles
        Private Shared Sub KillByMethod_CloseAllWindows(ByVal pid As Integer)

            ' Enumerate windows
            Dim _dico As New Dictionary(Of String, windowInfos)
            Objects.Window.EnumerateWindowsByProcessId(pid, False, True, _dico, False)

            For Each w As windowInfos In _dico.Values
                Objects.Window.CloseWindowByHandle(w.Handle)
            Next

        End Sub

#End Region

    End Class

End Namespace
