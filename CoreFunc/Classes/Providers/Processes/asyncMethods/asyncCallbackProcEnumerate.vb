Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackProcEnumerate

    ' Contains devices (logical drives) and their corresponding path
    ' e.g. :        /Device/Harddisk1/...       , C:
    Private Shared _DicoLogicalDrivesNames As New Dictionary(Of String, String)

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Shared dicoNewProcesses As New Dictionary(Of Integer, Boolean)
    ' PID <-> (PID-TID <-> THREAD)
    Public Shared AvailableThreads As New Dictionary(Of Integer, Dictionary(Of String, threadInfos))

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cProcessConnection
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cProcessConnection)
            ctrl = ctr
            deg = de
            con = co
        End Sub
    End Structure

    Public Shared Sub ClearDico()
        dicoNewProcesses.Clear()
        AvailableThreads.Clear()
    End Sub

    Public Shared Sub Process(ByVal thePoolObj As Object)
        SyncLock dicoNewProcesses

            AvailableThreads.Clear()

            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If pObj.con.ConnectionObj.IsConnected = False Then
                Exit Sub
            End If

            Select Case pObj.con.ConnectionObj.ConnectionType

                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                    ' Save current collection
                    Dim res As ManagementObjectCollection = Nothing
                    Try
                        res = pObj.con.wmiSearcher.Get()
                    Catch ex As Exception
                        pObj.ctrl.Invoke(pObj.deg, False, Nothing, ex.Message)
                        Exit Sub
                    End Try

                    Dim _dico As New Dictionary(Of String, processInfos)
                    For Each refProcess As Management.ManagementObject In res

                        Dim obj As New API.SYSTEM_PROCESS_INFORMATION
                        With obj
                            .BasePriority = CInt(refProcess.Item(API.WMI_INFO.Priority.ToString))
                            .HandleCount = CInt(refProcess.Item(API.WMI_INFO.HandleCount.ToString))
                            .InheritedFromProcessId = CInt(refProcess.Item(API.WMI_INFO.ParentProcessId.ToString))
                            Dim _IO As New API.IO_COUNTERS
                            With _IO
                                .OtherOperationCount = CULng(refProcess.Item(API.WMI_INFO.OtherOperationCount.ToString))
                                .OtherTransferCount = CULng(refProcess.Item(API.WMI_INFO.OtherTransferCount.ToString))
                                .ReadOperationCount = CULng(refProcess.Item(API.WMI_INFO.ReadOperationCount.ToString))
                                .ReadTransferCount = CULng(refProcess.Item(API.WMI_INFO.ReadTransferCount.ToString))
                                .WriteOperationCount = CULng(refProcess.Item(API.WMI_INFO.WriteOperationCount.ToString))
                                .WriteTransferCount = CULng(refProcess.Item(API.WMI_INFO.WriteTransferCount.ToString))
                            End With
                            .IoCounters = _IO
                            .KernelTime = CLng(refProcess.Item(API.WMI_INFO.KernelModeTime.ToString))
                            .NumberOfThreads = CInt(refProcess.Item(API.WMI_INFO.ThreadCount.ToString))
                            .ProcessId = CInt(refProcess.Item(API.WMI_INFO.ProcessId.ToString))
                            '.SessionId                 ' NOT IMPLEMENTED
                            .UserTime = CLng(refProcess.Item(API.WMI_INFO.UserModeTime.ToString))
                            Dim _VM As New API.VM_COUNTERS_EX
                            With _VM
                                .PageFaultCount = CInt(refProcess.Item(API.WMI_INFO.PageFaults.ToString))
                                .PagefileUsage = CInt(refProcess.Item(API.WMI_INFO.PageFileUsage.ToString))
                                .PeakPagefileUsage = CInt(refProcess.Item(API.WMI_INFO.PeakPageFileUsage.ToString))
                                .PeakVirtualSize = CInt(refProcess.Item(API.WMI_INFO.PeakVirtualSize.ToString))
                                .PeakWorkingSetSize = CInt(refProcess.Item(API.WMI_INFO.PeakWorkingSetSize.ToString))
                                .PrivateBytes = CInt(refProcess.Item(API.WMI_INFO.PrivatePageCount.ToString))
                                .QuotaNonPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaNonPagedPoolUsage.ToString))
                                .QuotaPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaPagedPoolUsage.ToString))
                                .QuotaPeakNonPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaPeakNonPagedPoolUsage.ToString))
                                .QuotaPeakPagedPoolUsage = CInt(refProcess.Item(API.WMI_INFO.QuotaPeakPagedPoolUsage.ToString))
                                .VirtualSize = CInt(refProcess.Item(API.WMI_INFO.VirtualSize.ToString))
                                .WorkingSetSize = CInt(refProcess.Item(API.WMI_INFO.WorkingSetSize.ToString))
                            End With
                            .VirtualMemoryCounters = _VM
                        End With


                        ' Do we have to get fixed infos ?
                        Dim _procInfos As New processInfos(obj, CStr(refProcess.Item("Name")))
                        If dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then
                            With _procInfos
                                .Path = CStr(refProcess.Item(API.WMI_INFO.ExecutablePath.ToString))

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
                        _dico.Add(obj.ProcessId.ToString, _procInfos)
                    Next

                    ' Remove all processes that not exist anymore
                    Dim _dicoTemp As Dictionary(Of Integer, Boolean) = dicoNewProcesses
                    For Each it As System.Collections.Generic.KeyValuePair(Of Integer, Boolean) In _dicoTemp
                        If it.Value = False Then
                            dicoNewProcesses.Remove(it.Key)
                        End If
                    Next
                    pObj.ctrl.Invoke(pObj.deg, True, _dico, Nothing)

                Case Else
                    ' Local
                    Dim ret As Integer
                    API.ZwQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessesAndThreadsInformation, IntPtr.Zero, 0, ret)
                    Dim size As Integer = ret
                    Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
                    API.ZwQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessesAndThreadsInformation, ptr, size, ret)

                    ' Extract structures from unmanaged memory
                    Dim structSize As Integer = Marshal.SizeOf(GetType(API.SYSTEM_PROCESS_INFORMATION))
                    Dim x As Integer = 0
                    Dim offset As Integer = 0
                    Dim _dico As New Dictionary(Of String, processInfos)
                    Do While True
                        Dim obj As API.SYSTEM_PROCESS_INFORMATION = CType(Marshal.PtrToStructure(New IntPtr(ptr.ToInt32 + _
                            offset), GetType(API.SYSTEM_PROCESS_INFORMATION)),  _
                            API.SYSTEM_PROCESS_INFORMATION)

                        Dim _procInfos As New processInfos(obj)

                        For j As Integer = 0 To obj.NumberOfThreads - 1

                            Dim _off As Integer = ptr.ToInt32 + offset + Marshal.SizeOf(GetType(API.SYSTEM_PROCESS_INFORMATION)) + j _
                                    * Marshal.SizeOf(GetType(API.SYSTEM_THREAD_INFORMATION))

                            Dim thread As API.SYSTEM_THREAD_INFORMATION = _
                                CType(Marshal.PtrToStructure(New IntPtr(_off), _
                                    GetType(API.SYSTEM_THREAD_INFORMATION)),  _
                                    API.SYSTEM_THREAD_INFORMATION)

                            Dim _key As String = thread.ClientId.UniqueThread.ToString & "-" & thread.ClientId.UniqueProcess.ToString
                            Dim _th As New threadInfos(thread)
                            If _procInfos.Threads.ContainsKey(_key) = False Then
                                _procInfos.Threads.Add(_key, _th)
                            End If
                        Next
                        AvailableThreads.Add(obj.ProcessId, _procInfos.Threads)


                        ' Do we have to get fixed infos ?
                        If dicoNewProcesses.ContainsKey(obj.ProcessId) = False Then

                            Dim _path As String = GetPath(obj.ProcessId)
                            Dim _user As String = GetUser(obj.ProcessId)
                            Dim _command As String = NO_INFO_RETRIEVED
                            Dim _peb As Integer = GetPebAddress(obj.ProcessId)
                            If _peb > 0 Then
                                _command = GetCommandLine(obj.ProcessId, _peb)
                            End If
                            Dim _finfo As FileVersionInfo = Nothing
                            Try
                                _finfo = FileVersionInfo.GetVersionInfo(_path)
                            Catch ex As Exception
                                ' File not available or ?
                            End Try

                            With _procInfos
                                .Path = _path
                                .UserName = _user
                                .CommandLine = _command
                                .FileInfo = _finfo
                                .PEBAddress = _peb
                            End With

                            dicoNewProcesses.Add(obj.ProcessId, False)

                            Trace.WriteLine("Got fixed infos for id = " & obj.ProcessId.ToString)
                        End If

                        ' Set true so that the process is marked as existing
                        dicoNewProcesses(obj.ProcessId) = True

                        offset += obj.NextEntryOffset
                        _dico.Add(obj.ProcessId.ToString, _procInfos)

                        If obj.NextEntryOffset = 0 Then
                            Exit Do
                        End If
                        x += 1
                    Loop
                    Marshal.FreeHGlobal(ptr)

                    ' Remove all processes that not exist anymore
                    Dim _dicoTemp As Dictionary(Of Integer, Boolean) = dicoNewProcesses
                    For Each it As System.Collections.Generic.KeyValuePair(Of Integer, Boolean) In _dicoTemp
                        If it.Value = False Then
                            dicoNewProcesses.Remove(it.Key)
                        End If
                    Next
                    pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)
            End Select

        End SyncLock

    End Sub


    Private Shared Function GetPath(ByVal _pid As Integer) As String
        Dim s As String = vbNullString
        Dim Ret As Integer
        Dim sResult As String = Space(512)
        Dim hModule As Integer

        Dim _hProcess As Integer = API.OpenProcess(cProcessConnection.ProcessMinRights, 0, _pid)
        Call API.EnumProcessModules2(_hProcess, hModule, 4, Ret)
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

    Private Shared Function GetUser(ByVal _pid As Integer) As String
        ' Local
        Dim retLen As Integer
        Dim _UserName As String

        If _pid > 4 Then

            Dim hToken As Integer
            Dim _hProcess As Integer = API.OpenProcess(cProcessConnection.ProcessMinRights, 0, _pid)


            If API.OpenProcessToken(_hProcess, &H8, hToken) > 0 Then

                API.GetTokenInformation(hToken, API.TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, 0, retLen)
                Dim data As IntPtr = Marshal.AllocHGlobal(retLen)
                API.GetTokenInformation(hToken, API.TOKEN_INFORMATION_CLASS.TokenUser, data, retLen, retLen)

                API.CloseHandle(_hProcess)
                API.CloseHandle(hToken)

                Dim user As New API.TOKEN_USER
                user = CType(Marshal.PtrToStructure(data, GetType(API.TOKEN_USER)), API.TOKEN_USER)

                _UserName = GetAccountName(user.User.Sid, True)
                If _UserName = vbNullString Then
                    _UserName = NO_INFO_RETRIEVED
                End If
            Else
                _UserName = NO_INFO_RETRIEVED
            End If
            Return _UserName
        Else
            Return NO_INFO_RETRIEVED
        End If
    End Function

    ' Return the path of a process from its pid
    Public Shared Function GetImageFile(ByVal _pid As Integer) As String

        If _pid > 4 Then

            ' Have to open a handle
            Dim _h As Integer = API.OpenProcess(cProcessConnection.ProcessMinRights, 0, _pid)

            If _h > 0 Then
                ' Get size
                Dim _size As Integer
                API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessImageFileName, IntPtr.Zero, 0, _size)
                If _size = 0 Then
                    Return "??"
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
                    Return "??"
                End If
            Else
                Return "??"
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
            Dim _h As Integer = API.OpenProcess(cProcessConnection.ProcessMinRights, 0, _pid)
            Dim pbi As New API.PROCESS_BASIC_INFORMATION
            Dim ret As Integer
            API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessBasicInformation, pbi, Marshal.SizeOf(pbi), ret)
            API.CloseHandle(_h)
            Return pbi.PebBaseAddress
        Else
            Return 0
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

            If API.QueryDosDevice(Char.ConvertFromUtf32(c) & ":", _badPath, 1024) <> 0 Then
                _tempDico.Add(_badPath.ToString(), Char.ConvertFromUtf32(c).ToString() & ":")
            End If
        Next

        _DicoLogicalDrivesNames = _tempDico
    End Sub

    ' Return the command line
    Private Shared Function GetCommandLine(ByVal _pid As Integer, ByVal PEBAddress As Integer) As String

        Dim res As String = ""

        ' Get PEB address of process
        ' Get PEB address of process
        Dim __pebAd As Integer = PEBAddress
        If __pebAd = -1 Then
            Return ""
        End If

        ' Create a processMemRW class to read in memory
        Dim cR As New cProcessMemRW(_pid)

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

    ' Get an account name from a SID
    Private Shared Function GetAccountName(ByVal SID As Integer, ByVal IncludeDomain As Boolean) As String
        Dim name As New StringBuilder(255)
        Dim domain As New StringBuilder(255)
        Dim namelen As Integer = 255
        Dim domainlen As Integer = 255
        Dim use As API.SID_NAME_USE = API.SID_NAME_USE.SidTypeUser

        Try
            If Not API.LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use) Then
                name.EnsureCapacity(namelen)
                domain.EnsureCapacity(domainlen)
                API.LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use)
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
End Class
