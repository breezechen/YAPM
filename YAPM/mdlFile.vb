Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text

Module mdlFile

    Public Structure SHELLEXECUTEINFO
        Public cbSize As Integer
        Public fMask As Integer
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> Public lpVerb As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpFile As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpParameters As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpDirectory As String
        Dim nShow As Integer
        Dim hInstApp As IntPtr
        Dim lpIDList As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As Integer
        Public hIcon As IntPtr
        Public hProcess As IntPtr
    End Structure

    Public Structure AllFileInfos
        Dim tFileVersion As System.Diagnostics.FileVersionInfo
        Dim isArchive As Boolean
        Dim isCompressed As Boolean
        Dim isDevice As Boolean
        Dim isDirectory As Boolean
        Dim isEncrypted As Boolean
        Dim isHidden As Boolean
        Dim isNormal As Boolean
        Dim isNotContentIndexed As Boolean
        Dim isOffline As Boolean
        Dim isReadOnly As Boolean
        Dim isReparsePoint As Boolean
        Dim isSparseFile As Boolean
        Dim isSystem As Boolean
        Dim isTemporary As Boolean
        Dim DateCreated As String
        Dim DateLastAccessed As String
        Dim DateLastModified As String
        Dim FileSize As Long
        Dim CompressedFileSize As Long
        Dim ParentDirectory As String
        Dim DirectoryDepth As Integer
        Dim FileExtension As String
        Dim FileAssociatedProgram As String
        Dim FileType As String
        Dim FileAvailableForWrite As Boolean
        Dim FileAvailableForRead As Boolean
        Dim ShortPath As String
        Dim ShortName As String
        Dim Name As String
    End Structure

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Function GetShortPathName(ByVal longPath As String, _
          <MarshalAs(UnmanagedType.LPTStr)> ByVal ShortPath As System.Text.StringBuilder, _
          <MarshalAs(Runtime.InteropServices.UnmanagedType.U4)> ByVal bufferSize As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Function CreateFile(ByVal lpFileName As String, ByVal dwDesiredAccess As EFileAccess, ByVal dwShareMode As EFileShare, ByVal lpSecurityAttributes As IntPtr, ByVal dwCreationDisposition As ECreationDisposition, ByVal dwFlagsAndAttributes As EFileAttributes, ByVal hTemplateFile As IntPtr) As IntPtr
    End Function

    Private Declare Function CloseHandle Lib "kernel32" Alias "CloseHandle" (ByVal hObject As IntPtr) As Integer

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
    Private Function GetFileSizeEx(<[In]()> ByVal hFile As IntPtr, <[In](), Out()> ByRef lpFileSize As Long) As Boolean
    End Function

    Private Declare Function SHRunDialog Lib "shell32" Alias "#61" (ByVal hOwner As Integer, ByVal Unknown1 As Integer, ByVal Unknown2 As Integer, ByVal szTitle As System.Text.StringBuilder, ByVal szPrompt As System.Text.StringBuilder, ByVal uFlags As Integer) As Integer

    Private Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" _
        (ByVal Buffer As String, ByVal Size As Integer) As Integer

    <DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
    End Function

    <DllImport("User32", SetLastError:=True)> _
    Private Function LoadString(ByVal hInstance As IntPtr, ByVal uID As UInt32, ByVal lpBuffer As System.Text.StringBuilder, ByVal nBufferMax As Integer) As Integer
    End Function

    <DllImport("shell32.dll")> _
    Private Function FindExecutable(ByVal lpFile As String, ByVal lpDirectory As String, ByVal lpResult As StringBuilder) As IntPtr
    End Function

    Private Declare Auto Function LoadLibrary Lib "kernel32.dll" (ByVal lpFileName As String) As IntPtr

    <DllImport("kernel32.dll", SetLastError:=True, EntryPoint:="FreeLibrary")> _
    Private Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
    End Function

    Private Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA" (ByVal lpFileName As String, ByVal lpFileSizeHigh As Integer) As Integer
    'Private Declare Function GetFileInformationByHandle Lib "kernel32" (ByVal hFile As Integer, ByRef lpFileInformation As BY_HANDLE_FILE_INFORMATION) As Integer

    Private Enum EFileAccess
        _GenericRead = &H80000000
        _GenericWrite = &H40000000
        _GenericExecute = &H20000000
        _GenericAll = &H10000000
    End Enum
    Private Enum EFileShare
        _None = &H0
        _Read = &H1
        _Write = &H2
        _Delete = &H4
    End Enum
    Private Enum ECreationDisposition
        _New = 1
        _CreateAlways = 2
        _OpenExisting = 3
        _OpenAlways = 4
        _TruncateExisting = 5
    End Enum
    Private Enum EFileAttributes
        _Readonly = &H1
        _Hidden = &H2
        _System = &H4
        _Directory = &H10
        _Archive = &H20
        _Device = &H40
        _Normal = &H80
        _Temporary = &H100
        _SparseFile = &H200
        _ReparsePoint = &H400
        _Compressed = &H800
        _Offline = &H1000
        _NotContentIndexed = &H2000
        _Encrypted = &H4000
        _Write_Through = &H80000000
        _Overlapped = &H40000000
        _NoBuffering = &H20000000
        _RandomAccess = &H10000000
        _SequentialScan = &H8000000
        _DeleteOnClose = &H4000000
        _BackupSemantics = &H2000000
        _PosixSemantics = &H1000000
        _OpenReparsePoint = &H200000
        _OpenNoRecall = &H100000
        _FirstPipeInstance = &H80000
    End Enum

    Private Structure _FILETIME
        Dim dwLowDateTime As Integer
        Dim dwHighDateTime As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure SYSTEMTIME
        <MarshalAs(UnmanagedType.U2)> Public Year As Short
        <MarshalAs(UnmanagedType.U2)> Public Month As Short
        <MarshalAs(UnmanagedType.U2)> Public DayOfWeek As Short
        <MarshalAs(UnmanagedType.U2)> Public Day As Short
        <MarshalAs(UnmanagedType.U2)> Public Hour As Short
        <MarshalAs(UnmanagedType.U2)> Public Minute As Short
        <MarshalAs(UnmanagedType.U2)> Public Second As Short
        <MarshalAs(UnmanagedType.U2)> Public Milliseconds As Short
    End Structure

    Private Structure BY_HANDLE_FILE_INFORMATION
        Dim dwFileAttributes As Long
        Dim ftCreationTime As _FILETIME
        Dim ftLastAccessTime As _FILETIME
        Dim ftLastWriteTime As _FILETIME
        Dim dwVolumeSerialNumber As Integer
        Dim nFileSizeHigh As Integer
        Dim nFileSizeLow As Integer
        Dim nNumberOfLinks As Integer
        Dim nFileIndexHigh As Integer
        Dim nFileIndexLow As Integer
    End Structure


    Private Const SEE_MASK_INVOKEIDLIST As Integer = &HC
    Private Const SEE_MASK_NOCLOSEPROCESS As Integer = &H40
    Private Const SEE_MASK_FLAG_NO_UI As Integer = &H400

    ' Display File Property Box
    Public Function ShowFileProperty(ByVal file As String) As Boolean
        Dim SEI As SHELLEXECUTEINFO = Nothing
        With SEI
            .fMask = SEE_MASK_NOCLOSEPROCESS Or SEE_MASK_INVOKEIDLIST Or _
                SEE_MASK_FLAG_NO_UI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = frmMain.Handle
            .lpVerb = "properties"
            .lpFile = file
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return ShellExecuteEx(SEI)
    End Function

    ' Open directory
    Public Function OpenDirectory(ByVal file As String) As Integer
        Dim r As Integer = -1
        If IO.File.Exists(file) Then
            Dim p As String = IO.Directory.GetParent(file).FullName
            r = Shell("explorer.exe " & p, _
                AppWinStyle.NormalFocus) ' This is some kind of shit, but it's the simpliest way
        End If
        Return r
    End Function

    ' Open directory2
    Public Function OpenDirectory2(ByVal dir As String) As Integer
        Dim r As Integer = -1
        If IO.Directory.Exists(dir) Then
            r = Shell("explorer.exe " & dir, _
                AppWinStyle.NormalFocus) ' This is some kind of shit, but it's the simpliest way
        End If
        Return r
    End Function

    ' Get parent dir
    Public Function GetParentDir(ByVal path As String) As String
        Dim i As Integer = InStrRev(path, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return path.Substring(0, i)
        Else
            Return path
        End If
    End Function

    ' Open a file/URL
    Public Function ShellOpenFile(ByVal file As String) As Boolean
        Dim SEI As SHELLEXECUTEINFO = Nothing
        With SEI
            .fMask = SEE_MASK_NOCLOSEPROCESS Or SEE_MASK_INVOKEIDLIST Or _
                SEE_MASK_FLAG_NO_UI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = frmMain.Handle
            .lpVerb = vbNullChar
            .lpFile = file
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return ShellExecuteEx(SEI)
    End Function

    ' Extract an ressource (string) from a file
    Private Function ExtractString(ByVal path As String, ByVal id As UInteger) As String
        Dim hInst As IntPtr = LoadLibrary(path)
        Dim sb As New StringBuilder(1024)
        Dim len As Integer = LoadString(hInst, id, sb, sb.Capacity)
        FreeLibrary(hInst)
        Return sb.ToString
    End Function

    ' Retrieve a good formated path from a bad string
    Public Function IntelligentPathRetrieving(ByVal path As String) As String
        Dim rootDir As String = Space$(256)
        GetWindowsDirectory(rootDir, 256)
        rootDir = Left(rootDir, InStr(rootDir, vbNullChar, CompareMethod.Binary) - 1)

        Dim s As String = Replace(path.ToLowerInvariant, "@%systemroot%", rootDir)
        s = Replace(s, "%systemroot%", rootDir)

        ' Get ID and file
        Dim iD As UInteger = 0
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(s, ".exe", , CompareMethod.Binary)
        If i = 0 Then i = InStrRev(s, ".dll", , CompareMethod.Binary)
        file = Left(s, i + 3)
        iD = CUInt(Val(Right(s, s.Length - i - 5)))

        ' Get ressource
        Return Replace(ExtractString(file, iD), "\", "\\")

    End Function

    ' Retrieve a good formated path from a bad string
    Public Function IntelligentPathRetrieving2(ByVal path As String) As String
        ' Get ID and file
        Dim iD As UInteger = 0
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(path, ".exe", , CompareMethod.Binary)
        file = Left(path, i + 3)
        Return file
    End Function

    Public Function ShowRunBox(ByVal hWnd As Integer, ByVal Title As String, _
        ByVal Message As String) As Integer

        Return SHRunDialog(hWnd, 0, 0, New System.Text.StringBuilder(Title), _
            New System.Text.StringBuilder(Message), 0)

    End Function

    ' Return all availables informations about a file
    Public Function GetAllFileInfos(ByVal file As String) As AllFileInfos
        Dim t As AllFileInfos = Nothing
        Dim isArchive As Boolean = False                        '
        Dim isCompressed As Boolean = False                     '
        Dim isDevice As Boolean = False                         '
        Dim isDirectory As Boolean = False                      '
        Dim isEncrypted As Boolean = False                      '
        Dim isHidden As Boolean = False                         '
        Dim isNormal As Boolean = False                         '
        Dim isNotContentIndexed As Boolean = False              '   
        Dim isOffline As Boolean = False                        '
        Dim isReadOnly As Boolean = False                       '
        Dim isReparsePoint As Boolean = False                   '
        Dim isSparseFile As Boolean = False                     '
        Dim isSystem As Boolean = False                         '
        Dim isTemporary As Boolean = False                      '
        Dim DateCreated As String = vbNullString                '
        Dim DateLastAccessed As String = vbNullString           '
        Dim DateLastModified As String = vbNullString           '
        Dim FileSize As Long = 0                                '
        Dim CompressedFileSize As Long = 0                      '
        Dim ParentDirectory As String = vbNullString            '
        Dim DirectoryDepth As Integer = 0                       '
        Dim FileExtension As String = vbNullString              '
        Dim FileAssociatedProgram As String = vbNullString      '
        Dim FileType As String = vbNullString                   '
        Dim FileAvailableForWrite As Boolean = False            '
        Dim FileAvailableForRead As Boolean = False             '
        Dim ShortPath As String = vbNullString                  '
        Dim ShortName As String = vbNullString                  '
        Dim Name As String = vbNullString                       '

        ' Get a handle
        Dim ptr As IntPtr = CreateFile(file, EFileAccess._GenericRead, _
            EFileShare._Read Or EFileShare._Write, IntPtr.Zero, _
            ECreationDisposition._OpenExisting, 0, IntPtr.Zero)

        If ptr = CType(-1, IntPtr) Then Return Nothing

        ' Get sizes
        GetFileSizeEx(ptr, t.FileSize)
        t.CompressedFileSize = GetCompressedFileSize(file, 0)

        ' Get dates and attributes
        Dim dC As Date = IO.File.GetCreationTime(file)
        Dim dA As Date = IO.File.GetLastAccessTime(file)
        Dim dW As Date = IO.File.GetLastWriteTime(file)
        With t
            .DateLastModified = dW.ToLongDateString & " -- " & dW.ToLongTimeString
            .DateLastAccessed = dA.ToLongDateString & " -- " & dA.ToLongTimeString
            .DateCreated = dC.ToLongDateString & " -- " & dC.ToLongTimeString
        End With

        Dim fA As IO.FileAttributes = IO.File.GetAttributes(file)
        t.isArchive = ((fA And IO.FileAttributes.Archive) = IO.FileAttributes.Archive)
        t.isCompressed = ((fA And IO.FileAttributes.Compressed) = IO.FileAttributes.Compressed)
        t.isDevice = ((fA And IO.FileAttributes.Device) = IO.FileAttributes.Device)
        t.isDirectory = ((fA And IO.FileAttributes.Directory) = IO.FileAttributes.Directory)
        t.isEncrypted = ((fA And IO.FileAttributes.Encrypted) = IO.FileAttributes.Encrypted)
        t.isHidden = ((fA And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden)
        t.isNormal = ((fA And IO.FileAttributes.Normal) = IO.FileAttributes.Normal)
        t.isNotContentIndexed = ((fA And IO.FileAttributes.NotContentIndexed) = IO.FileAttributes.NotContentIndexed)
        t.isOffline = ((fA And IO.FileAttributes.Offline) = IO.FileAttributes.Offline)
        t.isReadOnly = ((fA And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
        t.isReparsePoint = ((fA And IO.FileAttributes.ReparsePoint) = IO.FileAttributes.ReparsePoint)
        t.isSparseFile = ((fA And IO.FileAttributes.SparseFile) = IO.FileAttributes.SparseFile)
        t.isSystem = ((fA And IO.FileAttributes.System) = IO.FileAttributes.System)
        t.isTemporary = ((fA And IO.FileAttributes.Temporary) = IO.FileAttributes.Temporary)

        t.ParentDirectory = Replace$(mdlFile.GetParentDir(file), "\", "\\")
        Dim x As Integer
        For x = 0 To file.Length - 1
            If file.Substring(x, 1) = "\" Then t.DirectoryDepth += 1
        Next
        x = InStrRev(file, ".", , CompareMethod.Binary)
        t.FileExtension = Right(file, file.Length - x)
        x = InStrRev(file, "\", , CompareMethod.Binary)
        t.Name = Right(file, file.Length - x)

        Dim sb As New StringBuilder(255)
        FindExecutable(file, vbNullString, sb)
        t.FileAssociatedProgram = Replace(sb.ToString, "\", "\\")

        ' File type
        Dim ft As String = CStr(My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\." & LCase(t.FileExtension), "", ""))
        ft = CStr(My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & ft, "", ""))
        t.FileType = CStr(IIf(ft = vbNullString, "Unknown", ft))

        ' Short name/path
        Dim sb2 As New StringBuilder(80)
        Dim sb3 As New StringBuilder(80)
        GetShortPathName(file, sb2, 80)
        GetShortPathName(t.ParentDirectory, sb3, 80)
        t.ShortName = Replace(sb2.ToString, "\", "\\")
        t.ShortPath = Replace(sb3.ToString, "\", "\\")

        Dim ptrR As IntPtr = CreateFile(file, EFileAccess._GenericRead, _
                    EFileShare._Read Or EFileShare._Write, IntPtr.Zero, _
                    ECreationDisposition._OpenExisting, 0, IntPtr.Zero)
        If Not (ptrR = IntPtr.Zero) Then
            CloseHandle(ptrR)
            t.FileAvailableForRead = True
        End If

        Dim ptrW As IntPtr = CreateFile(file, EFileAccess._GenericWrite, _
                    EFileShare._Read Or EFileShare._Write, IntPtr.Zero, _
                    ECreationDisposition._OpenExisting, 0, IntPtr.Zero)
        If Not (ptrW = IntPtr.Zero) Then
            CloseHandle(ptrW)
            t.FileAvailableForWrite = True
        End If

        ' Infos about dll/exe
        t.tFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(file)

        sb2 = Nothing
        sb3 = Nothing
        sb = Nothing
        CloseHandle(ptr)
        Return t

    End Function

    ' Return file name from a path
    Public Function GetFileName(ByVal path As String) As String
        Dim i As Integer = InStrRev(path, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return Right(path, path.Length - i)
        Else
            Return vbNullString
        End If
    End Function

End Module
