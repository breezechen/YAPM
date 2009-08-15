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

Imports System.Runtime.InteropServices
Imports YAPM.Native.Api

Namespace Native.Objects

    Public Class Process


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Used to enumerate visible processes (simplified)
        Private Shared memAllocForVSProcesses As New Native.Memory.MemoryAlloc(&H1000)

        ' Used to enumerate visible processes (full)
        Private Shared memAllocForVProcesses As New Native.Memory.MemoryAlloc(&H1000)

        ' Protection for _currentProcesses
        Private Shared _semCurrentProcesses As New System.Threading.Semaphore(1, 1)

        ' Current processes running
        Private Shared _currentProcesses As Dictionary(Of String, cProcess)

        ' List of new processes
        Private Shared dicoNewProcesses As New Dictionary(Of Integer, Boolean)

        ' Protection for dicoNewProcesses
        Private Shared _semNewProcesses As New System.Threading.Semaphore(1, 1)


        ' ========================================
        ' Public properties
        ' ========================================

        ' Min rights for Query
        Public Shared ReadOnly Property ProcessQueryMinRights() As Native.Security.ProcessAccess
            Get
                Static _minRights As Native.Security.ProcessAccess = Native.Security.ProcessAccess.QueryInformation
                Static checked As Boolean = False
                If checked = False Then
                    If cEnvironment.IsWindowsVistaOrAbove Then
                        checked = True
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
                Native.Api.NativeFunctions.IsProcessInJob(handle, IntPtr.Zero, res)
                Return res
            End Get
        End Property

        ' Debugger present ?
        Public Shared ReadOnly Property IsDebuggerPresent(ByVal handle As IntPtr) As Boolean
            Get
                Dim res As Boolean
                Native.Api.NativeFunctions.CheckRemoteDebuggerPresent(handle, res)
                Return res
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
            If hProc <> IntPtr.Zero Then
                Return NativeFunctions.SetProcessAffinityMask(hProc, New IntPtr(affinity))
            Else
                Return False
            End If
        End Function
        Public Shared Function SetProcessAffinityById(ByVal process As Integer, _
                                           ByVal affinity As Integer) As Boolean

            ' Open handle, set affinity and close handle
            Dim hProc As IntPtr = _
                    NativeFunctions.OpenProcess(Security.ProcessAccess.SetInformation, False, process)
            Dim ret As Boolean = SetProcessAffinityByHandle(hProc, affinity)
            NativeFunctions.CloseHandle(hProc)

            Return ret
        End Function

        ' Set priority
        Public Shared Function SetProcessPriorityById(ByVal pid As Integer, _
                    ByVal priority As System.Diagnostics.ProcessPriorityClass) As Boolean

            Dim hProc As IntPtr
            Dim r As Boolean
            hProc = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SetInformation, _
                                    False, pid)

            If hProc <> IntPtr.Zero Then
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
            hProc = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SuspendResume, _
                                    False, pid)
            If hProc <> IntPtr.Zero Then
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
            hProc = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SuspendResume, _
                                    False, pid)
            If hProc <> IntPtr.Zero Then
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
            Dim ret As Boolean
            hProc = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.Terminate, _
                                                           False, pid)
            If hProc <> IntPtr.Zero Then
                ret = NativeFunctions.TerminateProcess(hProc, exitcode)
                NativeFunctions.CloseHandle(hProc)
                Return ret
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
            hProc = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.QueryInformation Or _
                                    Native.Security.ProcessAccess.VmRead, _
                                    False, pid)
            If hProc <> IntPtr.Zero Then
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
            Dim hProc As IntPtr = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.SetQuota, _
                                                         False, pid)
            If hProc <> IntPtr.Zero Then
                Dim ret As Boolean = NativeFunctions.SetProcessWorkingSetSize(hProc, _
                                                    New IntPtr(-1), New IntPtr(-1))
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
                Return "[Idle process]"
            ElseIf pid = 4 Then
                Return "SYSTEM"
            Else

                ' Have to open a handle
                hProc = NativeFunctions.OpenProcess(Process.ProcessQueryMinRights, _
                                                               False, pid)
                If hProc <> IntPtr.Zero Then
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
            If cEnvironment.IsWindowsVistaOrAbove Then
                If hProc <> IntPtr.Zero Then
                    Dim length As Integer = &H400
                    Dim sb As New System.Text.StringBuilder(length)
                    NativeFunctions.QueryFullProcessImageName(hProc, False, sb, length)
                    resFile = sb.ToString(0, length)
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
                Dim hProc As IntPtr = NativeFunctions.OpenProcess(Process.ProcessQueryMinRights, _
                                                   False, pid)
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
                Dim hProc As IntPtr = NativeFunctions.OpenProcess(Process.ProcessQueryMinRights, _
                                                          False, pid)

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
        Public Shared Function GetProcessCommandLineById(ByVal pid As Integer, Optional ByVal pebAddress As Long = 0) As String

            Try
                Dim res As String = ""

                ' Get PEB address of process
                Dim __pebAd As IntPtr
                If pebAddress > 0 Then
                    __pebAd = New IntPtr(pebAddress)
                Else
                    __pebAd = GetProcessPebAddressById(pid)
                End If
                If __pebAd = IntPtr.Zero Then
                    Return ""
                End If

                ' Create a reader class to read in memory
                Dim memReader As New ProcessMemReader(pid)

                If memReader.ProcessHandle = IntPtr.Zero Then
                    Return NO_INFO_RETRIEVED           ' Couldn't open a handle
                End If

                ' Retrieve process parameters block address
                ' It's located from bytes 16 to 20 after PEB address
                '64TODO
                Dim __procParamAd As IntPtr = memReader.ReadIntPtr(New IntPtr(__pebAd.ToInt64 + 16))


                ' Get unicode string adress
                ' It's located at offset 0x40 on all NT systems because it's 
                ' after a fixed structure of 64 bytes
                Dim cmdLine As NativeStructs.UnicodeString

                ' Read length of the unicode string
                ' TODO
                cmdLine.Length = CUShort(memReader.ReadInt32(New IntPtr(__procParamAd.ToInt64 + &H40)))
                cmdLine.MaximumLength = CUShort(cmdLine.Length + 2) ' Not used, but...

                ' Read pointer to the string
                ' 64TODO
                cmdLine.Buffer = memReader.ReadIntPtr(New IntPtr(__procParamAd.ToInt64 + &H44))

                ' Read the string
                res = memReader.ReadUnicodeString(cmdLine)

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
                Dim _procInfos As New processInfos(obj)

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
        Public Shared Function EnumerateVisibleProcesses() As Dictionary(Of String, processInfos)

            ' Refresh list of drives
            Common.Misc.RefreshLogicalDrives()

            Dim ret As Integer
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForVProcesses.Pointer, memAllocForVProcesses.Size, ret)
            If memAllocForVProcesses.Size < ret Then
                memAllocForVProcesses.Resize(ret)
            End If
            Native.Api.NativeFunctions.NtQuerySystemInformation(Native.Api.NativeEnums.SystemInformationClass.SystemProcessInformation, _
                            memAllocForVProcesses.Pointer, memAllocForVProcesses.Size, ret)

            ' Extract structures from unmanaged memory
            Dim x As Integer = 0
            Dim offset As Integer = 0
            Dim _dico As New Dictionary(Of String, processInfos)
            Do While True

                Dim obj As Native.Api.NativeStructs.SystemProcessInformation = _
                        memAllocForVProcesses.ReadStructOffset(Of Native.Api.NativeStructs.SystemProcessInformation)(offset)
                Dim _procInfos As New processInfos(obj)


                ' Do we have to get fixed infos ?
                If dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then

                    Dim _path As String = GetProcessPathById(obj.ProcessId)
                    Dim _domain As String = Nothing
                    Dim _user As String = Nothing
                    GetProcessUserDomainNameById(obj.ProcessId, _user, _domain)
                    Dim _command As String = NO_INFO_RETRIEVED
                    Dim _peb As IntPtr = GetProcessPebAddressById(obj.ProcessId)
                    If _peb <> IntPtr.Zero Then
                        _command = GetProcessCommandLineById(obj.ProcessId, _peb.ToInt64)
                    End If
                    Dim _finfo As FileVersionInfo = Nothing
                    If IO.File.Exists(_path) Then
                        Try
                            _finfo = FileVersionInfo.GetVersionInfo(_path)
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

                    dicoNewProcesses.Add(obj.ProcessId, False)

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
                If CurrentProcesses.ContainsKey(pc.Pid.ToString) = False Then
                    CurrentProcesses.Add(pc.Pid.ToString, New cProcess(pc))
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
                    Dim _theHandle As IntPtr = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.DupHandle, False, proc.Pid)
                    _csrss.Add(proc.Pid, _theHandle)
                End If
            Next

            ' Now we get all handles from all processes
            Dim _handles As Native.Api.NativeStructs.SystemHandleInformation() = EnumerateProcessHandles()

            ' For handles which belongs to a csrss.exe process
            For Each h As Native.Api.NativeStructs.SystemHandleInformation In _handles
                If _csrss.ContainsKey(h.ProcessId) Then
                    Dim _dup As IntPtr
                    ' ISNEEDED ?
                    If Native.Api.NativeFunctions.DuplicateHandle(_csrss(h.ProcessId), CType(h.Handle, IntPtr), Native.Api.NativeConstants.InvalidHandleValue, _dup, 0, False, Native.Api.NativeEnums.DuplicateOptions.SameAccess) Then
                        Dim pid As Integer = Native.Api.NativeFunctions.GetProcessId(_dup)
                        Dim obj As New Native.Api.NativeStructs.SystemProcessInformation
                        With obj
                            .ProcessId = pid
                        End With
                        Dim _path As String = GetProcessPathById(obj.ProcessId)
                        Dim _procInfos As New processInfos(obj, cFile.GetFileName(_path))
                        _procInfos.Path = _path
                        If _dico.ContainsKey(pid.ToString) = False Then
                            _dico.Add(pid.ToString, _procInfos)
                        End If
                    End If
                    Native.Api.NativeFunctions.CloseHandle(_dup)
                End If
            Next

            ' Add the two instances of csrss.exe to result
            ' & close previously opened handles
            For Each h As Integer In _csrss.Keys
                Dim obj As New Native.Api.NativeStructs.SystemProcessInformation
                With obj
                    .ProcessId = h
                End With
                Dim _path As String = GetProcessPathById(obj.ProcessId)
                Dim _procInfos As New processInfos(obj, cFile.GetFileName(_path))
                _procInfos.Path = _path
                If _dico.ContainsKey(h.ToString) = False Then
                    _dico.Add(h.ToString, _procInfos)
                End If

                Native.Api.NativeFunctions.CloseHandle(_csrss(h))
            Next


            ' Get visible processes
            Dim _visible As Dictionary(Of String, processInfos) = EnumerateVisibleProcessesSimplified()

            ' Merge results
            For Each pp As processInfos In _visible.Values
                If _dico.ContainsKey(pp.Pid.ToString) = False Then
                    _dico.Add(pp.Pid.ToString, pp)
                End If
            Next

            ' Mark processes that are not present in _visible as hidden
            For Each pp As processInfos In _dico.Values
                If _visible.ContainsKey(pp.Pid.ToString) = False Then
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
                Dim handle As IntPtr = Native.Api.NativeFunctions.OpenProcess(Process.ProcessQueryMinRights, False, pid)
                If handle <> IntPtr.Zero Then
                    Dim exitcode As Integer
                    Dim res As Boolean = Native.Api.NativeFunctions.GetExitCodeProcess(handle, exitcode)
                    If exitcode = Native.Api.NativeConstants.STILL_ACTIVE Then  ' Process still exists
                        Dim obj As New Native.Api.NativeStructs.SystemProcessInformation
                        With obj
                            .ProcessId = pid
                        End With
                        Dim _path As String = GetProcessPathById(obj.ProcessId)
                        Dim _procInfos As New processInfos(obj, cFile.GetFileName(_path))
                        _procInfos.Path = _path
                        Dim sKey As String = pid.ToString
                        If _dico.ContainsKey(sKey) = False Then
                            _dico.Add(sKey, _procInfos)
                        End If
                    End If
                    Native.Api.NativeFunctions.CloseHandle(handle)
                End If
            Next

            ' Get visible processes
            Dim _visible As Dictionary(Of String, processInfos) = EnumerateVisibleProcessesSimplified()

            ' Merge results
            For Each pp As processInfos In _visible.Values
                If _dico.ContainsKey(pp.Pid.ToString) = False Then
                    _dico.Add(pp.Pid.ToString, pp)
                End If
            Next

            ' Mark processes that are not present in _visible as hidden
            For Each pp As processInfos In _dico.Values
                If _visible.ContainsKey(pp.Pid.ToString) = False Then
                    pp.IsHidden = True
                End If
            Next

            Return _dico

        End Function

        ' Get a service by name
        Public Shared Function GetProcessById(ByVal id As Integer) As cProcess

            Dim tt As cProcess = Nothing
            Native.Objects.Process.SemCurrentProcesses.WaitOne()
            If _currentProcesses.ContainsKey(id.ToString) Then
                tt = _currentProcesses.Item(id.ToString)
            End If
            Native.Objects.Process.SemCurrentProcesses.Release()

            Return tt

        End Function

        ' Return a handle for a process
        Public Shared Function GetProcessHandleById(ByVal pid As Integer, ByVal access As Security.ProcessAccess) As IntPtr
            Return Native.Api.NativeFunctions.OpenProcess(Process.ProcessQueryMinRights, False, pid)
        End Function



        ' ========================================
        ' Private functions
        ' ========================================

        ' Enumerate all handes opened by all processes
        Private Shared Function EnumerateProcessHandles() As NativeStructs.SystemHandleInformation()
            Dim handleCount As Integer = 0
            Dim retLen As Integer
            Dim _handles As NativeStructs.SystemHandleInformation()

            ' I did not manage to get the good needed size with the first call to
            ' NtQuerySystemInformation with SystemHandleInformation flag when the buffer
            ' is too small. So each time the call to NtQuerySystemInformation fails with
            ' a too small buffer, the size is multiplicated by 2 and I call NtQuerySystemInformation
            ' again. And again, until the return is not STATUS_INFO_LENGTH_MISMATCH.
            ' Strange behavior.
            ' See http://forum.sysinternals.com/forum_posts.asp?TID=3577 for details.
            Const STATUS_INFO_LENGTH_MISMATCH As UInteger = &HC0000004

            Dim size As Integer = &H400
            Dim memAlloc As New Memory.MemoryAlloc(size)

            While CUInt(NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, memAlloc.Pointer, size, retLen)) = STATUS_INFO_LENGTH_MISMATCH
                size *= 2
                memAlloc.Resize(size)
            End While

            ' The handlecount value is the first integer (4 bytes) of the unmanaged memory.
            handleCount = memAlloc.ReadInt32(0)
            _handles = New NativeStructs.SystemHandleInformation(handleCount - 1) {}

            For x As Integer = 0 To handleCount - 1
                'Dim offset As Integer = ptr.ToInt32 + 4 + x * Marshal.SizeOf(GetType(NativeStructs.SystemHandleInformation))
                'Dim temp As NativeStructs.SystemHandleInformation = _
                '    CType(Marshal.PtrToStructure(New IntPtr(offset), _
                '                             GetType(NativeStructs.SystemHandleInformation)),  _
                '                             NativeStructs.SystemHandleInformation)

                Dim temp As NativeStructs.SystemHandleInformation = _
                    memAlloc.ReadStruct(Of NativeStructs.SystemHandleInformation)(4, x)

                _handles(x) = temp
            Next

            memAlloc.Free()

            Return _handles

        End Function

    End Class

End Namespace
