﻿' =======================================================
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
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackProcEnumerate

    Private PROCESS_MIN_RIGHTS As API.PROCESS_RIGHTS = API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cProcessConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cProcessConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
        If cEnvironment.IsWindowsVistaOrAbove Then
            PROCESS_MIN_RIGHTS = API.PROCESS_RIGHTS.PROCESS_QUERY_LIMITED_INFORMATION
        End If
    End Sub

#Region "Shared code"

    ' Contains devices (logical drives) and their corresponding path
    ' e.g. :        /Device/Harddisk1/...       , C:
    Private Shared _DicoLogicalDrivesNames As New Dictionary(Of String, String)

    ' List of new processes
    Public Shared dicoNewProcesses As New Dictionary(Of Integer, Boolean)

    Public Shared Sub ClearDico()
        dicoNewProcesses.Clear()
    End Sub

    ' Reanalize a process by removing (or asking to remove) its PID from
    ' the shared dictionnary of known PID
    Public Structure ReanalizeProcessObj
        Public pid() As Integer
        Public con As cProcessConnection
        Public Sub New(ByRef pi() As Integer, ByRef co As cProcessConnection)
            pid = pi
            con = co
        End Sub
    End Structure
    Public Shared Sub ReanalizeProcess(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As ReanalizeProcessObj = DirectCast(thePoolObj, ReanalizeProcessObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessReanalize, pObj.pid)
                    pObj.con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.LocalConnection, cConnection.TypeOfConnection.RemoteConnectionViaWMI
                SyncLock dicoNewProcesses
                    For Each id As Integer In pObj.pid
                        If dicoNewProcesses.ContainsKey(id) Then
                            dicoNewProcesses.Remove(id)
                        End If
                    Next
                End SyncLock

        End Select

        sem.Release()
    End Sub

    ' Called to remove PIDs from shared dico by the server after it receive
    ' a command to reanalize some PIDs
    Public Shared Sub ReanalizeLocalAfterSocket(ByRef pid() As Integer)
        SyncLock dicoNewProcesses
            For Each id As Integer In pid
                If dicoNewProcesses.ContainsKey(id) Then
                    dicoNewProcesses.Remove(id)
                End If
            Next
        End SyncLock
    End Sub

#End Region

    Public Structure poolObj
        Public method As ProcessEnumMethode
        Public forInstanceId As Integer
        Public Sub New(ByVal iid As Integer, Optional ByVal tmethod As ProcessEnumMethode = ProcessEnumMethode.VisibleProcesses)
            forInstanceId = iid
            method = tmethod
        End Sub
    End Structure
    Public Enum ProcessEnumMethode As Integer
        VisibleProcesses
        BruteForce
        HandleMethod
    End Enum


    ' When socket got a list of processes !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, processInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If dico.ContainsKey(keys(x)) = False Then
                    dico.Add(keys(x), DirectCast(lst(x), processInfos))
                End If
            Next
        End If
        Try
            'If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
        Catch ex As Exception
            '
        End Try
    End Sub

    Friend Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        SyncLock dicoNewProcesses

            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If con.ConnectionObj.IsConnected = False Then
                sem.Release()
                Exit Sub
            End If

            Select Case con.ConnectionObj.ConnectionType

                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    _poolObj = pObj
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestProcessList)
                        cDat.InstanceId = _instanceId   ' Instance which request the list
                        con.ConnectionObj.Socket.Send(cDat)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                    ' Save current collection
                    Dim res As ManagementObjectCollection = Nothing
                    Try
                        res = con.wmiSearcher.Get()
                    Catch ex As Exception
                        If deg IsNot Nothing AndAlso ctrl.Created Then _
                            ctrl.Invoke(deg, False, Nothing, ex.Message, pObj.forInstanceId)
                        sem.Release()
                        Exit Sub
                    End Try

                    Dim _dico As New Dictionary(Of String, processInfos)
                    For Each refProcess As Management.ManagementObject In res

                        Dim obj As New Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION
                        With obj
                            .BasePriority = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.Priority.ToString))
                            .HandleCount = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.HandleCount.ToString))
                            .InheritedFromProcessId = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ParentProcessId.ToString))
                            Dim _IO As New Native.Api.NativeStructs.IO_COUNTERS
                            With _IO
                                .OtherOperationCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.OtherOperationCount.ToString))
                                .OtherTransferCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.OtherTransferCount.ToString))
                                .ReadOperationCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ReadOperationCount.ToString))
                                .ReadTransferCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ReadTransferCount.ToString))
                                .WriteOperationCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.WriteOperationCount.ToString))
                                .WriteTransferCount = CULng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.WriteTransferCount.ToString))
                            End With
                            .IoCounters = _IO
                            .KernelTime = CLng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.KernelModeTime.ToString))
                            .NumberOfThreads = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ThreadCount.ToString))
                            .ProcessId = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ProcessId.ToString))
                            '.SessionId                 ' NOT IMPLEMENTED
                            .UserTime = CLng(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.UserModeTime.ToString))
                            Dim _VM As New Native.Api.NativeStructs.VM_COUNTERS_EX
                            With _VM
                                .PageFaultCount = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PageFaults.ToString))
                                .PagefileUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PageFileUsage.ToString))
                                .PeakPagefileUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PeakPageFileUsage.ToString))
                                .PeakVirtualSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PeakVirtualSize.ToString))
                                .PeakWorkingSetSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PeakWorkingSetSize.ToString))
                                .PrivateBytes = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.PrivatePageCount.ToString))
                                .QuotaNonPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaNonPagedPoolUsage.ToString))
                                .QuotaPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaPagedPoolUsage.ToString))
                                .QuotaPeakNonPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaPeakNonPagedPoolUsage.ToString))
                                .QuotaPeakPagedPoolUsage = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.QuotaPeakPagedPoolUsage.ToString))
                                .VirtualSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.VirtualSize.ToString))
                                .WorkingSetSize = CInt(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.WorkingSetSize.ToString))
                            End With
                            .VirtualMemoryCounters = _VM
                        End With


                        ' Do we have to get fixed infos ?
                        Dim _procInfos As New processInfos(obj, CStr(refProcess.Item("Name")))
                        If dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then
                            With _procInfos
                                .Path = CStr(refProcess.Item(Native.Api.Enums.WMI_INFO_PROCESS.ExecutablePath.ToString))

                                Dim s1(1) As String
                                Try
                                    Call refProcess.InvokeMethod("GetOwner", s1)
                                    If Len(s1(0)) + Len(s1(1)) > 0 Then
                                        .UserName = s1(1) & "\" & s1(0)
                                    Else
                                        .UserName = NO_INFO_RETRIEVED
                                    End If
                                Catch ex As Exception
                                    .UserName = NO_INFO_RETRIEVED
                                End Try

                                .CommandLine = NO_INFO_RETRIEVED
                                .FileInfo = Nothing
                                .PEBAddress = 0
                            End With

                            dicoNewProcesses.Add(obj.ProcessId, False)

                            Trace.WriteLine("Got fixed infos for id = " & obj.ProcessId.ToString)
                        End If

                        ' Set true so that the process is marked as existing
                        dicoNewProcesses(obj.ProcessId) = True
                        Dim sKey As String = obj.ProcessId.ToString
                        If _dico.ContainsKey(sKey) = False Then
                            _dico.Add(sKey, _procInfos)
                        End If
                    Next

                    ' Remove all processes that not exist anymore
                    Dim _dicoTemp As Dictionary(Of Integer, Boolean) = dicoNewProcesses
                    For Each it As System.Collections.Generic.KeyValuePair(Of Integer, Boolean) In _dicoTemp
                        If it.Value = False Then
                            dicoNewProcesses.Remove(it.Key)
                        End If
                    Next
                    Try
                        'If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Nothing, pObj.forInstanceId)
                    Catch ex As Exception
                        '
                    End Try

                Case Else
                    ' Local
                    Dim _dico As Dictionary(Of String, processInfos)

                    Select Case pObj.method
                        Case ProcessEnumMethode.BruteForce
                            _dico = getHiddenProcessesBruteForce()
                        Case ProcessEnumMethode.HandleMethod
                            _dico = getHiddenProcessesHandleMethod()
                        Case Else
                            _dico = getVisibleProcesses()

                            ' Here we fill _currentProcesses if necessary
                            'PERFISSUE
                            cProcess.SemCurrentProcesses.WaitOne()
                            If cProcess.CurrentProcesses Is Nothing Then
                                cProcess.CurrentProcesses = New Dictionary(Of String, cProcess)
                            End If
                            For Each pc As processInfos In _dico.Values
                                If cProcess.CurrentProcesses.ContainsKey(pc.Pid.ToString) = False Then
                                    cProcess.CurrentProcesses.Add(pc.Pid.ToString, New cProcess(pc))
                                End If
                            Next
                            cProcess.SemCurrentProcesses.Release()

                    End Select

                    Try
                        'If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                    Catch ex As Exception
                        '
                    End Try
            End Select

        End SyncLock

        sem.Release()

    End Sub


    Private Shared Function GetPath(ByVal _pid As Integer) As String
        Dim s As String = vbNullString
        Dim Ret As Integer
        Dim sResult As String = Space(512)
        Dim hModule As Integer

        Dim _hProcess As IntPtr = API.OpenProcess(cProcessConnection.ProcessMinRights, _
                                                  False, _pid)
        Call API.EnumProcessModules(_hProcess, hModule, 4, Ret)
        sResult = Space(260)
        Call API.GetModuleFileNameExA(_hProcess, hModule, sResult, 260)
        s = sResult
        API.CloseHandle(_hProcess)

        If InStr(sResult, vbNullChar) > 1 Then
            sResult = Left(sResult, InStr(sResult, vbNullChar) - 1)
        Else
            sResult = NO_INFO_RETRIEVED
        End If

        If System.IO.File.Exists(sResult) = False Then
            Call RefreshLogicalDrives()
            sResult = GetImageFile(_pid)
        End If

        Return sResult
    End Function

    Private Shared Function GetUser(ByVal _pid As Integer, ByRef username As String, ByRef domain As String) As Boolean
        ' Local
        Dim retLen As Integer
        Dim _UserName As String

        If _pid > 4 Then

            Dim hToken As IntPtr
            Dim _hProcess As IntPtr = API.OpenProcess(cProcessConnection.ProcessMinRights, _
                                                      False, _pid)


            If API.OpenProcessToken(_hProcess, &H8, hToken) > 0 Then

                API.GetTokenInformation(hToken, API.TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, 0, retLen)
                Dim data As IntPtr = Marshal.AllocHGlobal(retLen)
                API.GetTokenInformation(hToken, API.TOKEN_INFORMATION_CLASS.TokenUser, data, retLen, retLen)

                API.CloseHandle(_hProcess)
                API.CloseHandle(hToken)

                Dim user As New API.TOKEN_USER
                user = CType(Marshal.PtrToStructure(data, GetType(API.TOKEN_USER)), API.TOKEN_USER)

                Call GetAccountName(user.User.Sid, username, domain)
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

    ' Return the path of a process from its pid
    Public Shared Function GetImageFile(ByVal _pid As Integer) As String

        If _pid > 4 Then

            ' Have to open a handle
            Dim _h As IntPtr
            _h = API.OpenProcess(cProcessConnection.ProcessMinRights, False, _pid)

            If _h <> IntPtr.Zero Then
                ' Get size
                Dim _size As Integer
                API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessImageFileName, IntPtr.Zero, 0, _size)
                If _size = 0 Then
                    Return NO_INFO_RETRIEVED
                End If

                ' Retrieve unicode string
                Dim _pt As IntPtr = Marshal.AllocHGlobal(_size)
                API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessImageFileName, _pt, _size, _size)
                Dim _str As API.UNICODE_STRING = CType(Marshal.PtrToStructure(_pt, _
                                                                         GetType(API.UNICODE_STRING)), API.UNICODE_STRING)
                Marshal.FreeHGlobal(_pt)
                API.CloseHandle(_h)

                ' Return string (replace /DEVICE/... by the dos file name)
                Dim _stemp As String = ReadUnicodeString(_str)
                If _stemp IsNot Nothing Then
                    Return DeviceDriveNameToDosDriveName(_stemp)
                Else
                    Return NO_INFO_RETRIEVED
                End If
            Else
                Return NO_INFO_RETRIEVED
            End If

        ElseIf _pid = 4 Then
            Return "SYSTEM"
        Else
            Return "[Idle process]"
        End If

    End Function

    ' Return PEB address
    Private Shared Function GetPebAddress(ByVal _pid As Integer) As Integer
        If _pid > 4 Then
            Dim _h As IntPtr = API.OpenProcess(cProcessConnection.ProcessMinRights, _
                                               False, _pid)
            Dim pbi As New Native.Api.NativeStructs.ProcessBasicInformation
            Dim ret As Integer
            API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessBasicInformation, pbi, Marshal.SizeOf(pbi), ret)
            API.CloseHandle(_h)
            Return pbi.PebBaseAddress
        Else
            Return 0
        End If
    End Function

    ' Return dos drive name
    Public Shared Function DeviceDriveNameToDosDriveName(ByVal drivePath As String) As String

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

            If API.QueryDosDevice(Char.ConvertFromUtf32(c) & ":", _badPath, 1024) <> 0 Then
                _tempDico.Add(_badPath.ToString(), Char.ConvertFromUtf32(c).ToString() & ":")
            End If
        Next

        _DicoLogicalDrivesNames = _tempDico
    End Sub

    ' Return the command line
    Private Shared Function GetCommandLine(ByVal _pid As Integer, ByVal PEBAddress As Integer) As String

        Try
            Dim res As String = ""

            ' Get PEB address of process
            ' Get PEB address of process
            Dim __pebAd As Integer = PEBAddress
            If __pebAd <= 0 Then
                Return ""
            End If

            ' Create a reader class to read in memory
            Dim memReader As New cProcessMemReader(_pid)

            If memReader.ProcessHandle = 0 Then
                Return NO_INFO_RETRIEVED           ' Couldn't open a handle
            End If

            ' Retrieve process parameters block address
            ' It's located from bytes 16 to 20 after PEB address
            Dim __procParamAd As Integer = memReader.ReadInt32(__pebAd + 16)


            ' Get unicode string adress
            ' It's located at offset 0x40 on all NT systems because it's 
            ' after a fixed structure of 64 bytes
            Dim cmdLine As API.UNICODE_STRING

            ' Read length of the unicode string
            cmdLine.Length = CUShort(memReader.ReadInt32(__procParamAd + &H40))
            cmdLine.MaximumLength = CUShort(cmdLine.Length + 2) ' Not used, but...

            ' Read pointer to the string
            cmdLine.Buffer = memReader.ReadInt32(__procParamAd + &H44)

            ' Read the string
            res = memReader.ReadUnicodeString(cmdLine)

            Return res

        Catch ex As Exception
            Return NO_INFO_RETRIEVED
        End Try

    End Function

    ' Get an account name from a SID
    Private Shared Function GetAccountName(ByVal SID As Integer, ByRef userName As String, ByRef domainName As String) As Boolean
        Dim name As New StringBuilder(255)
        Dim domain As New StringBuilder(255)
        Dim namelen As Integer = 255
        Dim domainlen As Integer = 255
        Dim use As API.SID_NAME_USE = API.SID_NAME_USE.SidTypeUser

        domainName = ""
        userName = ""

        Try
            If Not API.LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use) Then
                name.EnsureCapacity(namelen)
                domain.EnsureCapacity(domainlen)
                API.LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use)
            End If
            userName = name.ToString
        Catch
            ' return string SID
            userName = New System.Security.Principal.SecurityIdentifier(New IntPtr(SID)).ToString
            Return False
        End Try

        domainName = domain.ToString
        Return True

    End Function

    ' Enumerate all handes opened by all processes
    Private Function EnumerateHandles() As API.SystemHandleInformation()
        Dim handleCount As Integer = 0
        Dim retLen As Integer
        Dim _handles As API.SystemHandleInformation()

        ' I did not manage to get the good needed size with the first call to
        ' NtQuerySystemInformation with SystemHandleInformation flag when the buffer
        ' is too small. So each time the call to NtQuerySystemInformation fails with
        ' a too small buffer, the size is multiplicated by 2 and I call NtQuerySystemInformation
        ' again. And again, until the return is not STATUS_INFO_LENGTH_MISMATCH.
        ' Strange behavior.
        ' See http://forum.sysinternals.com/forum_posts.asp?TID=3577 for details.
        Const STATUS_INFO_LENGTH_MISMATCH As UInteger = &HC0000004

        Dim size As Integer = 1024
        Dim ptr As IntPtr = Marshal.AllocHGlobal(size)

        While CUInt(API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemHandleInformation, ptr, size, retLen)) = STATUS_INFO_LENGTH_MISMATCH

            size *= 2
            Marshal.FreeHGlobal(ptr)
            ptr = Marshal.AllocHGlobal(size)

        End While

        ' The handlecount value is the first integer (4 bytes) of the unmanaged memory.
        handleCount = Marshal.ReadInt32(ptr, 0)
        _handles = New API.SystemHandleInformation(handleCount - 1) {}

        For x As Integer = 0 To handleCount - 1
            Dim offset As Integer = ptr.ToInt32 + 4 + x * Marshal.SizeOf(GetType(API.SystemHandleInformation))
            Dim temp As API.SystemHandleInformation = _
                CType(Marshal.PtrToStructure(New IntPtr(offset), _
                                         GetType(API.SystemHandleInformation)),  _
                                         API.SystemHandleInformation)
            _handles(x) = temp
        Next

        Marshal.FreeHGlobal(ptr)

        Return _handles

    End Function

    ' Get all hidden processes (handle method)
    Private Function getHiddenProcessesHandleMethod() As Dictionary(Of String, processInfos)

        ' Refresh list of drives
        RefreshLogicalDrives()

        ' For each Process Id (PID) possible
        Dim _dico As New Dictionary(Of String, processInfos)

        ' Firstly, we get all instances of csrss.exe process.
        ' We retrieve them from visible list. So if csrss.exe processes are hidden... DAMN !!!
        ' Note : There are more than once instance of csrss.exe on Vista.
        Dim _csrss As New Dictionary(Of Integer, IntPtr)
        For Each proc As processInfos In getVisibleProcesses.Values
            If proc.Name.ToLowerInvariant = "csrss.exe" Then
                Dim _theHandle As IntPtr = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_DUP_HANDLE, False, proc.Pid)
                _csrss.Add(proc.Pid, _theHandle)
            End If
        Next

        ' Now we get all handles from all processes
        Dim _handles As API.SystemHandleInformation() = EnumerateHandles()

        ' For handles which belongs to a csrss.exe process
        For Each h As API.SystemHandleInformation In _handles
            If _csrss.ContainsKey(h.ProcessId) Then
                Dim _dup As IntPtr
                ' ISNEEDED ?
                If (API.DuplicateHandle(_csrss(h.ProcessId), CType(h.Handle, IntPtr), API.InvalidHandleValue, _dup, 0, False, API.DUPLICATE_SAME_ACCESS)) <> 0 Then
                    Dim pid As Integer = API.GetProcessId(_dup)
                    Dim obj As New Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION
                    With obj
                        .ProcessId = pid
                    End With
                    Dim _path As String = GetImageFile(obj.ProcessId)
                    Dim _procInfos As New processInfos(obj, GetFileName(_path))
                    _procInfos.Path = _path
                    If _dico.ContainsKey(pid.ToString) = False Then
                        _dico.Add(pid.ToString, _procInfos)
                    End If
                End If
                API.CloseHandle(_dup)
            End If
        Next

        ' Add the two instances of csrss.exe to result
        ' & close previously opened handles
        For Each h As Integer In _csrss.Keys
            Dim obj As New Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION
            With obj
                .ProcessId = h
            End With
            Dim _path As String = GetImageFile(obj.ProcessId)
            Dim _procInfos As New processInfos(obj, GetFileName(_path))
            _procInfos.Path = _path
            If _dico.ContainsKey(h.ToString) = False Then
                _dico.Add(h.ToString, _procInfos)
            End If

            API.CloseHandle(_csrss(h))
        Next


        ' Get visible processes
        Dim _visible As Dictionary(Of String, processInfos) = getVisibleProcessesSimp()

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
    Private Function getHiddenProcessesBruteForce() As Dictionary(Of String, processInfos)

        ' Refresh list of drives
        RefreshLogicalDrives()

        ' For each Process Id (PID) possible
        Dim _dico As New Dictionary(Of String, processInfos)

        ' We could stop before &hffff....
        For pid As Integer = &H8 To &HFFFF Step 4
            Dim handle As IntPtr = API.OpenProcess(PROCESS_MIN_RIGHTS, False, pid)
            If handle <> IntPtr.Zero Then
                Dim exitcode As Integer
                Dim res As Integer = API.GetExitCodeProcess(handle, exitcode)
                If exitcode = API.STILL_ACTIVE Then  ' Process still exists
                    Dim obj As New Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION
                    With obj
                        .ProcessId = pid
                    End With
                    Dim _path As String = GetImageFile(obj.ProcessId)
                    Dim _procInfos As New processInfos(obj, GetFileName(_path))
                    _procInfos.Path = _path
                    Dim sKey As String = pid.ToString
                    If _dico.ContainsKey(sKey) = False Then
                        _dico.Add(sKey, _procInfos)
                    End If
                End If
                API.CloseHandle(handle)
            End If
        Next

        ' Get visible processes
        Dim _visible As Dictionary(Of String, processInfos) = getVisibleProcessesSimp()

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


    ' Get all visible processes
    ' Memory allocation for process 
    Private memAllocForVProcesses As New Native.Memory.MemoryAlloc(&H1000)
    Private Function getVisibleProcesses() As Dictionary(Of String, processInfos)

        Dim ret As Integer
        API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemProcessesAndThreadsInformation, _
                        memAllocForVProcesses.Pointer, memAllocForVProcesses.Size, ret)
        If memAllocForVProcesses.Size < ret Then
            memAllocForVProcesses.Resize(ret)
        End If
        API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemProcessesAndThreadsInformation, _
                        memAllocForVProcesses.Pointer, memAllocForVProcesses.Size, ret)

        ' Extract structures from unmanaged memory
        Dim x As Integer = 0
        Dim offset As Integer = 0
        Dim _dico As New Dictionary(Of String, processInfos)
        Do While True

            Dim obj As Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION = _
                    memAllocForVProcesses.ReadStructOffset(Of Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION)(offset)
            Dim _procInfos As New processInfos(obj)


            ' Do we have to get fixed infos ?
            If dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then

                Dim _path As String = GetPath(obj.ProcessId)
                Dim _domain As String = Nothing
                Dim _user As String = Nothing
                Call GetUser(obj.ProcessId, _user, _domain)
                Dim _command As String = NO_INFO_RETRIEVED
                Dim _peb As Integer = GetPebAddress(obj.ProcessId)
                If _peb > 0 Then
                    _command = GetCommandLine(obj.ProcessId, _peb)
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
                    .PEBAddress = _peb
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

        Return _dico
    End Function

    ' Get all visible processes (simplified)
    Private memAllocForVSProcesses As New Native.Memory.MemoryAlloc(&H1000)
    Private Function getVisibleProcessesSimp() As Dictionary(Of String, processInfos)

        Dim ret As Integer
        API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemProcessesAndThreadsInformation, _
                        memAllocForVSProcesses.Pointer, memAllocForVSProcesses.Size, ret)
        If memAllocForVSProcesses.Size < ret Then
            memAllocForVSProcesses.Resize(ret)
        End If
        API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemProcessesAndThreadsInformation, _
                        memAllocForVSProcesses.Pointer, memAllocForVSProcesses.Size, ret)

        ' Extract structures from unmanaged memory
        Dim x As Integer = 0
        Dim offset As Integer = 0
        Dim _dico As New Dictionary(Of String, processInfos)
        Do While True

            Dim obj As Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION = _
                    memAllocForVProcesses.ReadStructOffset(Of Native.Api.NativeStructs.SYSTEM_PROCESS_INFORMATION)(offset)
            Dim _procInfos As New processInfos(obj)

            Dim _path As String = GetPath(obj.ProcessId)
            With _procInfos
                .Path = _path
                .UserName = NO_INFO_RETRIEVED
                .CommandLine = NO_INFO_RETRIEVED
                .FileInfo = Nothing
                .PEBAddress = 0
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

End Class
