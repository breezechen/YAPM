'Option Strict On

'Imports CoreFunc.cProcessConnection
'Imports System.Runtime.InteropServices
'Imports System.Text

'Public Class asyncCallbackGetFixedInfos

'    ' Contains devices (logical drives) and their corresponding path
'    ' e.g. :        /Device/Harddisk1/...       , C:
'    Private Shared _DicoLogicalDrivesNames As New Dictionary(Of String, String)

'    Private Const NO_INFO_RETRIEVED As String = "N/A"

'    Private _pid As Integer
'    Private _peb As Integer
'    Private _connection As cConnection

'    Public Structure TheseInfos
'        Public path As String
'        Public username As String
'        Public commandline As String
'        Public fileinfo As FileVersionInfo
'        Public pebAdd As Integer
'        Public Sub New(ByVal _path As String, ByVal _user As String, _
'                       ByVal _command As String, ByVal _fileinfo As FileVersionInfo, ByVal _peb As Integer)
'            path = _path
'            username = _user
'            commandline = _command
'            fileinfo = _fileinfo
'            pebAdd = _peb
'        End Sub
'    End Structure

'    Public Event GatheredInfos(ByVal infos As TheseInfos)

'    Public Sub New(ByVal pid As Integer, ByVal peb As Integer, ByRef procConnection As cConnection)
'        _pid = pid
'        _peb = peb
'        _connection = procConnection
'    End Sub

'    Public Sub Process()
'        Select Case _connection.ConnectionType
'            Case cconnection.TypeOfConnection.RemoteConnectionViaSocket

'            Case cconnection.TypeOfConnection.RemoteConnectionViaWMI

'            Case Else
'                ' Local
'                Dim _path As String = GetPath()
'                Dim _user As String = GetUser()
'                Dim _command As String = NO_INFO_RETRIEVED
'                Dim _peb As Integer = GetPebAddress(_pid)
'                If _peb > 0 Then
'                    ' _command = GetCommandLine(_pid, _peb)
'                End If
'                Dim _finfo As FileVersionInfo = Nothing
'                Try
'                    _finfo = FileVersionInfo.GetVersionInfo(_path)
'                Catch ex As Exception
'                    ' File not available or ?
'                End Try
'                RaiseEvent GatheredInfos(New TheseInfos(_path, _user, _command, _finfo, _peb))
'        End Select
'    End Sub

'    Private Function GetPath() As String
'        Dim s As String = vbNullString
'        Dim Ret As Integer
'        Dim sResult As String = Space(512)
'        Dim hModule As Integer

'        Dim _hProcess As Integer = API.OpenProcess(&H1000, 0, _pid)
'        If _hProcess > 0 Then
'            Call API.EnumProcessModules(_hProcess, hModule, 4, Ret)
'            sResult = Space(260)
'            Call API.GetModuleFileNameExA(_hProcess, hModule, sResult, 260)
'            s = sResult
'            API.CloseHandle(_hProcess)
'        Else
'            _hProcess = API.OpenProcess(API.PROCESS_QUERY_INFORMATION, 0, _pid)
'            Call API.EnumProcessModules(_hProcess, hModule, 4, Ret)
'            sResult = Space(260)
'            Call API.GetModuleFileNameExA(_hProcess, hModule, sResult, 260)
'            s = sResult
'            API.CloseHandle(_hProcess)
'        End If

'        If InStr(sResult, vbNullChar) > 1 Then
'            sResult = Left(sResult, InStr(sResult, vbNullChar) - 1)
'        Else
'            sResult = NO_INFO_RETRIEVED
'        End If

'        If System.IO.File.Exists(sResult) = False Then
'            Call RefreshLogicalDrives()
'            sResult = GetImageFile(_pid)
'        End If

'        Return sResult
'    End Function

'    Private Function GetUser() As String
'        ' Local
'        Dim retLen As Integer
'        Dim _UserName As String

'        If _pid > 4 Then

'            Dim hToken As Integer
'            Dim _hProcess As Integer = API.OpenProcess(&H1000, 0, _pid)
'            If _hProcess = 0 Then
'                _hProcess = API.OpenProcess(API.PROCESS_QUERY_INFORMATION, 0, _pid)
'            End If

'            If API.OpenProcessToken(_hProcess, &H8, hToken) > 0 Then

'                API.GetTokenInformation(hToken, API.TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, 0, retLen)
'                Dim data As IntPtr = Marshal.AllocHGlobal(retLen)
'                API.GetTokenInformation(hToken, API.TOKEN_INFORMATION_CLASS.TokenUser, data, retLen, retLen)

'                API.CloseHandle(_hProcess)
'                API.CloseHandle(hToken)

'                Dim user As New API.TOKEN_USER
'                user = CType(Marshal.PtrToStructure(data, GetType(API.TOKEN_USER)), API.TOKEN_USER)

'                _UserName = GetAccountName(user.User.Sid, True)
'                If _UserName = vbNullString Then
'                    _UserName = NO_INFO_RETRIEVED
'                End If
'            Else
'                _UserName = NO_INFO_RETRIEVED
'            End If
'            Return _UserName
'        Else
'            Return NO_INFO_RETRIEVED
'        End If
'    End Function

'    ' Return the path of a process from its pid
'    Private Function GetImageFile(ByVal _pid As Integer) As String

'        If _pid > 4 Then

'            ' Have to open a handle
'            Dim _h As Integer = API.OpenProcess(&H1000, 0, _pid) ' Limited rights

'            If _h > 0 Then
'                ' Get size
'                Dim _size As Integer
'                API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessImageFileName, IntPtr.Zero, 0, _size)
'                If _size = 0 Then
'                    ' Try to get more rights (XP)
'                    API.CloseHandle(_h)
'                    _h = API.OpenProcess(API.PROCESS_QUERY_INFORMATION, 0, _pid)
'                    API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessImageFileName, IntPtr.Zero, 0, _size)
'                    If _size = 0 Then
'                        Return "??"
'                    End If
'                End If

'                ' Retrieve unicode string
'                Dim _pt As IntPtr = Marshal.AllocHGlobal(_size)
'                API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessImageFileName, _pt, _size, _size)
'                Dim _str As API.UNICODE_STRING = CType(Marshal.PtrToStructure(_pt, _
'                                                                         GetType(API.UNICODE_STRING)), API.UNICODE_STRING)
'                Marshal.FreeHGlobal(_pt)
'                API.CloseHandle(_h)

'                ' Return string (replace /DEVICE/... by the dos file name)
'                Dim _stemp As String = ReadUnicodeString(_str)
'                If _stemp IsNot Nothing Then
'                    Return DeviceDriveNameToDosDriveName(_stemp)
'                Else
'                    Return "??"
'                End If
'            Else
'                Return "??"
'            End If

'        ElseIf _pid = 4 Then
'            Return "SYSTEM"
'        Else
'            Return "[Idle process]"
'        End If

'    End Function

'    ' Return PEB address
'    Private Function GetPebAddress(ByVal _pid As Integer) As Integer
'        If _pid > 4 Then
'            Dim _h As Integer = API.OpenProcess(&H1000, 0, _pid) ' Limited rights
'            Dim pbi As New API.PROCESS_BASIC_INFORMATION
'            Dim ret As Integer
'            API.ZwQueryInformationProcess(_h, API.PROCESS_INFORMATION_CLASS.ProcessBasicInformation, pbi, Marshal.SizeOf(pbi), ret)
'            API.CloseHandle(_pid)
'            Return pbi.PebBaseAddress
'        Else
'            Return 0
'        End If
'    End Function

'    ' Return dos drive name
'    Private Shared Function DeviceDriveNameToDosDriveName(ByVal drivePath As String) As String

'        For Each pair As System.Collections.Generic.KeyValuePair(Of String, String) In _DicoLogicalDrivesNames
'            If drivePath.StartsWith(pair.Key) Then
'                Return pair.Value + drivePath.Substring(pair.Key.Length)
'            End If
'        Next
'        Return drivePath

'    End Function

'    ' Refresh the dictionnary of logical drives
'    Private Shared Sub RefreshLogicalDrives()

'        Dim _tempDico As Dictionary(Of String, String) = New Dictionary(Of String, String)

'        ' From 'A' to 'Z'
'        ' It also possible to use GetLogicalDriveStringsA
'        For c As Byte = 65 To 90
'            Dim _badPath As New StringBuilder(1024)

'            If API.QueryDosDevice(Char.ConvertFromUtf32(c) & ":", _badPath, 1024) <> 0 Then
'                _tempDico.Add(_badPath.ToString(), Char.ConvertFromUtf32(c).ToString() & ":")
'            End If
'        Next

'        _DicoLogicalDrivesNames = _tempDico
'    End Sub

'    ' Return the command line
'    Private Function GetCommandLine(ByVal _pid As Integer, ByVal PEBAddress As Integer) As String

'        Dim res As String = ""

'        ' Get PEB address of process
'        ' Get PEB address of process
'        Dim __pebAd As Integer = PEBAddress
'        If __pebAd = -1 Then
'            Return ""
'        End If

'        ' Create a processMemRW class to read in memory
'        Dim cR As New cProcessMemRW(_pid)

'        If cR.Handle = 0 Then
'            Return ""           ' Couldn't open a handle
'        End If

'        ' Read first 20 bytes (5 integers) of PEB block
'        ' The fifth integer contains address of ProcessParameters block
'        Dim pebDeb() As Integer = cR.ReadBytesAI(__pebAd, 5)
'        Dim __procParamAd As Integer = pebDeb(4)

'        ' Get unicode string adress
'        ' It's located at offset 0x40 on all NT systems because it's after a fixed structure
'        ' of 64 bytes

'        ' Read length of the unicode string
'        Dim bA() As Short = cR.ReadBytesAS(__procParamAd + 64, 1)
'        Dim __size As Integer = bA(0)      ' Size of string
'        If __size = 0 Then
'            Return "N/A"
'        End If

'        ' Read pointer to the string
'        Dim bA2() As Integer = cR.ReadBytesAI(__procParamAd + 68, 1)
'        Dim __strPtr As Integer = bA2(0)      ' Pointer to string

'        'Trace.WriteLine("before string")
'        ' Gonna get string
'        Dim bS() As Short = cR.ReadBytesAS(__strPtr, __size)

'        ' Allocate unmanaged memory
'        Dim ptr As IntPtr = Marshal.AllocHGlobal(__size)
'        __size = CInt(__size / 2)   ' Because of Unicode String (2 bytes per char)

'        ' Copy from short array to unmanaged memory
'        Marshal.Copy(bS, 0, ptr, __size)

'        ' Convert to string (and copy to __var variable)
'        res = Marshal.PtrToStringUni(ptr, __size)

'        ' Free unmanaged memory
'        Marshal.FreeHGlobal(ptr)

'        Return res

'    End Function

'    ' Get an account name from a SID
'    Private Function GetAccountName(ByVal SID As Integer, ByVal IncludeDomain As Boolean) As String
'        Dim name As New StringBuilder(255)
'        Dim domain As New StringBuilder(255)
'        Dim namelen As Integer = 255
'        Dim domainlen As Integer = 255
'        Dim use As API.SID_NAME_USE = API.SID_NAME_USE.SidTypeUser

'        Try
'            If Not API.LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use) Then
'                name.EnsureCapacity(namelen)
'                domain.EnsureCapacity(domainlen)
'                API.LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use)
'            End If
'        Catch
'            ' return string SID
'            Return New System.Security.Principal.SecurityIdentifier(New IntPtr(SID)).ToString()
'        End Try

'        If IncludeDomain Then
'            Return CStr(IIf(domain.ToString <> "", domain.ToString & "\", "")) & name.ToString
'        Else
'            Return name.ToString()
'        End If
'    End Function

'End Class
