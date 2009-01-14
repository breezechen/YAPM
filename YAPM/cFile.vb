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
Imports System.Text

Public Class cFile

#Region "API"

    ' ========================================
    ' Strctures
    ' ========================================

    Private Structure _FILETIME
        Dim dwLowDateTime As Integer
        Dim dwHighDateTime As Integer
    End Structure

    '<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    'Private Structure SHFILEOPSTRUCT
    '    Public hwnd As IntPtr
    '    Public wFunc As Integer
    '    <MarshalAs(UnmanagedType.LPWStr)> _
    '    Public pFrom As String
    '    <MarshalAs(UnmanagedType.LPWStr)> _
    '    Public pTo As String
    '    Public fFlags As Integer
    '    Public fAnyOperationsAborted As Boolean
    '    Public hNameMappings As IntPtr
    '    <MarshalAs(UnmanagedType.LPWStr)> _
    '    Public lpszProgressTitle As String '  only used if FOF_SIMPLEPROGRESS
    'End Structure
    <StructLayout(LayoutKind.Explicit, CharSet:=CharSet.Ansi)> _
        Private Structure SHFILEOPSTRUCT
        <FieldOffset(0)> Public hWnd As Integer
        <FieldOffset(4)> Public wFunc As Integer
        <FieldOffset(8)> Public pFrom As String
        <FieldOffset(12)> Public pTo As String
        <FieldOffset(16)> Public fFlags As Short
        <FieldOffset(18)> Public fAnyOperationsAborted As Boolean
        <FieldOffset(20)> Public hNameMappings As Object
        <FieldOffset(24)> Public lpszProgressTitle As String
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

    Private Structure SHELLEXECUTEINFO
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

    ' ========================================
    ' API declaration
    ' ========================================
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function GetShortPathName(ByVal longPath As String, _
          <MarshalAs(UnmanagedType.LPTStr)> ByVal ShortPath As System.Text.StringBuilder, _
          <MarshalAs(Runtime.InteropServices.UnmanagedType.U4)> ByVal bufferSize As Integer) As Integer
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function CreateFile(ByVal lpFileName As String, ByVal dwDesiredAccess As EFileAccess, ByVal dwShareMode As EFileShare, ByVal lpSecurityAttributes As IntPtr, ByVal dwCreationDisposition As ECreationDisposition, ByVal dwFlagsAndAttributes As EFileAttributes, ByVal hTemplateFile As IntPtr) As IntPtr
    End Function

    Private Declare Function CloseHandle Lib "kernel32" Alias "CloseHandle" (ByVal hObject As IntPtr) As Integer

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> _
    Private Shared Function GetFileSizeEx(<[In]()> ByVal hFile As IntPtr, <[In](), Out()> ByRef lpFileSize As Long) As Boolean
    End Function

    Private Declare Function SHRunDialog Lib "shell32" Alias "#61" (ByVal hOwner As Integer, ByVal Unknown1 As Integer, ByVal Unknown2 As Integer, ByVal szTitle As System.Text.StringBuilder, ByVal szPrompt As System.Text.StringBuilder, ByVal uFlags As Integer) As Integer

    Private Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" _
        (ByVal Buffer As String, ByVal Size As Integer) As Integer

    <DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
    End Function

    <DllImport("User32", SetLastError:=True)> _
    Private Shared Function LoadString(ByVal hInstance As IntPtr, ByVal uID As UInt32, ByVal lpBuffer As System.Text.StringBuilder, ByVal nBufferMax As Integer) As Integer
    End Function

    <DllImport("shell32.dll")> _
    Private Shared Function FindExecutable(ByVal lpFile As String, ByVal lpDirectory As String, ByVal lpResult As StringBuilder) As IntPtr
    End Function

    Private Declare Auto Function LoadLibrary Lib "kernel32.dll" (ByVal lpFileName As String) As IntPtr

    <DllImport("kernel32.dll", SetLastError:=True, EntryPoint:="FreeLibrary")> _
    Private Shared Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
    End Function

    Private Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA" (ByVal lpFileName As String, ByVal lpFileSizeHigh As Integer) As Integer
    Private Declare Function SHFileOperation Lib "shell32.dll" Alias "SHFileOperation" (ByRef lpFileOp As SHFILEOPSTRUCT) As Integer
    'Private Declare Function SHFileOperation Lib "shell32.dll" Alias "SHFileOperationW" (ByRef lpFileOp As SHFILEOPSTRUCT) As Integer

    ' ========================================
    ' Enums & constants
    ' ========================================
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

    Private Const SEE_MASK_INVOKEIDLIST As Integer = &HC
    Private Const SEE_MASK_NOCLOSEPROCESS As Integer = &H40
    Private Const SEE_MASK_FLAG_NO_UI As Integer = &H400

#End Region

    ' ========================================
    ' Private attributes
    ' ========================================
#Region "Attributes"
    Private _tFileVersion As System.Diagnostics.FileVersionInfo
    Private _isArchive As Boolean
    Private _isCompressed As Boolean
    Private _isDevice As Boolean
    Private _isDirectory As Boolean
    Private _isEncrypted As Boolean
    Private _isHidden As Boolean
    Private _isNormal As Boolean
    Private _isNotContentIndexed As Boolean
    Private _isOffline As Boolean
    Private _isReadOnly As Boolean
    Private _isReparsePoint As Boolean
    Private _isSparseFile As Boolean
    Private _isSystem As Boolean
    Private _isTemporary As Boolean
    Private _DateCreated As String
    Private _DateLastAccessed As String
    Private _DateLastModified As String
    Private _FileSize As Long
    Private _CompressedFileSize As Long
    Private _ParentDirectory As String
    Private _DirectoryDepth As Integer
    Private _FileExtension As String
    Private _FileAssociatedProgram As String
    Private _FileType As String
    Private _FileAvailableForWrite As Boolean
    Private _FileAvailableForRead As Boolean
    Private _ShortPath As String
    Private _ShortName As String
    Private _Name As String
    Private _Path As String
#End Region

    ' ========================================
    ' Constructor
    ' ========================================
    Public Sub New(ByVal filePath As String, Optional ByVal refreshInfos As Boolean = False)
        MyBase.New()
        _Path = filePath
        If refreshInfos Then
            Refresh()
        End If
    End Sub


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "Getter"
    Public Function GetFileVersion() As System.Diagnostics.FileVersionInfo
        Return _tFileVersion
    End Function
    Public Function GetIsArchive() As Boolean
        Return _isArchive
    End Function
    Public Function GetIsCompressed() As Boolean
        Return _isCompressed
    End Function
    Public Function GetIsDevice() As Boolean
        Return _isDevice
    End Function
    Public Function GetIsDirectory() As Boolean
        Return _isDirectory
    End Function
    Public Function GetIsEncrypted() As Boolean
        Return _isEncrypted
    End Function
    Public Function GetIsHidden() As Boolean
        Return _isHidden
    End Function
    Public Function GetIsNormal() As Boolean
        Return _isNormal
    End Function
    Public Function GetIsNotContentIndexed() As Boolean
        Return _isNotContentIndexed
    End Function
    Public Function GetIsOffline() As Boolean
        Return _isOffline
    End Function
    Public Function GetIsReadOnly() As Boolean
        Return _isReadOnly
    End Function
    Public Function GetIsReparsePoint() As Boolean
        Return _isReparsePoint
    End Function
    Public Function GetIsSparseFile() As Boolean
        Return _isSparseFile
    End Function
    Public Function GetIsSystem() As Boolean
        Return _isSystem
    End Function
    Public Function GetIsTemporary() As Boolean
        Return _isTemporary
    End Function
    Public Function GetDateCreated() As String
        Return _DateCreated
    End Function
    Public Function GetDateLastAccessed() As String
        Return _DateLastAccessed
    End Function
    Public Function GetDateLastModified() As String
        Return _DateLastModified
    End Function
    Public Function GetFileSize() As Long
        Return _FileSize
    End Function
    Public Function GetCompressedFileSize() As Long
        Return _CompressedFileSize
    End Function
    Public Function GetParentDirectory() As String
        Return _ParentDirectory
    End Function
    Public Function GetDirectoryDepth() As Integer
        Return _DirectoryDepth
    End Function
    Public Function GetFileExtension() As String
        Return _FileExtension
    End Function
    Public Function GetFileAssociatedProgram() As String
        Return _FileAssociatedProgram
    End Function
    Public Function GetFileType() As String
        Return _FileType
    End Function
    Public Function GetFileAvailableForWrite() As Boolean
        Return _FileAvailableForWrite
    End Function
    Public Function GetFileAvailableForRead() As Boolean
        Return _FileAvailableForRead
    End Function
    Public Function GetShortPath() As String
        Return _ShortPath
    End Function
    Public Function GetShortName() As String
        Return _ShortName
    End Function
    Public Function GetName() As String
        Return _Name
    End Function
    Public Function GetPath() As String
        Return _Path
    End Function
    Public Function GetCreationTime() As Date
        Return IO.File.GetCreationTime(_Path)
    End Function
    Public Function GetLastAccessTime() As Date
        Return IO.File.GetLastAccessTime(_Path)
    End Function
    Public Function GetLastWriteTime() As Date
        Return IO.File.GetLastWriteTime(_Path)
    End Function
    Public Function GetAttributes() As IO.FileAttributes
        Return IO.File.GetAttributes(_Path)
    End Function
#End Region

    Public Sub SetCreationTime(ByVal theDate As Date)
        IO.File.SetCreationTime(_Path, theDate)
    End Sub
    Public Sub SetLastAccessTime(ByVal theDate As Date)
        IO.File.SetLastAccessTime(_Path, theDate)
    End Sub
    Public Sub SetLastWriteTime(ByVal theDate As Date)
        IO.File.SetLastWriteTime(_Path, theDate)
    End Sub
    Public Sub SetAttributes(ByVal attributes As IO.FileAttributes)
        IO.File.SetAttributes(_Path, attributes)
    End Sub


    ' ========================================
    ' Public functions
    ' ========================================

    ' Refresh all file informations
    Public Sub Refresh()

        ' Get a handle
        Dim ptr As IntPtr = CreateFile(_Path, EFileAccess._GenericRead, EFileShare._Read Or _
            EFileShare._Write, IntPtr.Zero, ECreationDisposition._OpenExisting, 0, IntPtr.Zero)

        If ptr = CType(-1, IntPtr) Then Exit Sub

        ' Get sizes
        GetFileSizeEx(ptr, _FileSize)
        _CompressedFileSize = GetCompressedFileSize(_Path, 0)

        ' Get dates and attributes
        Dim dC As Date = IO.File.GetCreationTime(_Path)
        Dim dA As Date = IO.File.GetLastAccessTime(_Path)
        Dim dW As Date = IO.File.GetLastWriteTime(_Path)
        _DateLastModified = dW.ToLongDateString & " -- " & dW.ToLongTimeString
        _DateLastAccessed = dA.ToLongDateString & " -- " & dA.ToLongTimeString
        _DateCreated = dC.ToLongDateString & " -- " & dC.ToLongTimeString

        Dim fA As IO.FileAttributes = IO.File.GetAttributes(_Path)
        _isArchive = ((fA And IO.FileAttributes.Archive) = IO.FileAttributes.Archive)
        _isCompressed = ((fA And IO.FileAttributes.Compressed) = IO.FileAttributes.Compressed)
        _isDevice = ((fA And IO.FileAttributes.Device) = IO.FileAttributes.Device)
        _isDirectory = ((fA And IO.FileAttributes.Directory) = IO.FileAttributes.Directory)
        _isEncrypted = ((fA And IO.FileAttributes.Encrypted) = IO.FileAttributes.Encrypted)
        _isHidden = ((fA And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden)
        _isNormal = ((fA And IO.FileAttributes.Normal) = IO.FileAttributes.Normal)
        _isNotContentIndexed = ((fA And IO.FileAttributes.NotContentIndexed) = IO.FileAttributes.NotContentIndexed)
        _isOffline = ((fA And IO.FileAttributes.Offline) = IO.FileAttributes.Offline)
        _isReadOnly = ((fA And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
        _isReparsePoint = ((fA And IO.FileAttributes.ReparsePoint) = IO.FileAttributes.ReparsePoint)
        _isSparseFile = ((fA And IO.FileAttributes.SparseFile) = IO.FileAttributes.SparseFile)
        _isSystem = ((fA And IO.FileAttributes.System) = IO.FileAttributes.System)
        _isTemporary = ((fA And IO.FileAttributes.Temporary) = IO.FileAttributes.Temporary)

        _ParentDirectory = Replace$(GetParentDir(_Path), "\", "\\")
        Dim x As Integer
        For x = 0 To _Path.Length - 1
            If _Path.Substring(x, 1) = "\" Then _DirectoryDepth += 1
        Next
        x = InStrRev(_Path, ".", , CompareMethod.Binary)
        _FileExtension = Right(_Path, _Path.Length - x)
        x = InStrRev(_Path, "\", , CompareMethod.Binary)
        _Name = Right(_Path, _Path.Length - x)

        Dim sb As New StringBuilder(255)
        FindExecutable(_Path, vbNullString, sb)
        _FileAssociatedProgram = Replace(sb.ToString, "\", "\\")

        ' File type
        Try
            Dim ft As String = CStr(My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\." & LCase(_FileExtension), "", ""))
            ft = CStr(My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & ft, "", ""))
            _FileType = CStr(IIf(ft = vbNullString, "Unknown", ft))
        Catch ex As Exception
            '
        End Try

        ' Short name/path
        Dim sb2 As New StringBuilder(80)
        Dim sb3 As New StringBuilder(80)
        GetShortPathName(_Path, sb2, 80)
        GetShortPathName(_ParentDirectory, sb3, 80)
        _ShortName = Replace(sb2.ToString, "\", "\\")
        _ShortPath = Replace(sb3.ToString, "\", "\\")

        Dim ptrR As IntPtr = CreateFile(_Path, EFileAccess._GenericRead, _
                    EFileShare._Read Or EFileShare._Write, IntPtr.Zero, _
                    ECreationDisposition._OpenExisting, 0, IntPtr.Zero)
        If Not (ptrR = IntPtr.Zero) Then
            CloseHandle(ptrR)
            _FileAvailableForRead = True
        End If

        Dim ptrW As IntPtr = CreateFile(_Path, EFileAccess._GenericWrite, _
                    EFileShare._Read Or EFileShare._Write, IntPtr.Zero, _
                    ECreationDisposition._OpenExisting, 0, IntPtr.Zero)
        If Not (ptrW = IntPtr.Zero) Then
            CloseHandle(ptrW)
            _FileAvailableForWrite = True
        End If

        ' Infos about dll/exe
        _tFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(_Path)

        sb2 = Nothing
        sb3 = Nothing
        sb = Nothing
        CloseHandle(ptr)
    End Sub

    ' Display File Property Box
    Public Function ShowFileProperty() As Boolean
        Dim SEI As SHELLEXECUTEINFO = Nothing
        With SEI
            .fMask = SEE_MASK_NOCLOSEPROCESS Or SEE_MASK_INVOKEIDLIST Or _
                SEE_MASK_FLAG_NO_UI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = frmMain.Handle
            .lpVerb = "properties"
            .lpFile = _Path
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return ShellExecuteEx(SEI)
    End Function

    ' Open directory
    Public Function OpenDirectory() As Integer
        Dim r As Integer = -1
        If IO.File.Exists(_Path) Then
            Dim p As String = IO.Directory.GetParent(_Path).FullName
            r = Shell("explorer.exe " & p, _
                AppWinStyle.NormalFocus) ' This is some kind of shit, but it's the simpliest way
        End If
        Return r
    End Function

    ' Get parent dir
    Public Function GetParentDir() As String
        Dim i As Integer = InStrRev(_Path, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return _Path.Substring(0, i)
        Else
            Return _Path
        End If
    End Function

    ' Open a file/URL
    Public Function ShellOpenFile() As Boolean
        Dim SEI As SHELLEXECUTEINFO = Nothing
        With SEI
            .fMask = SEE_MASK_NOCLOSEPROCESS Or SEE_MASK_INVOKEIDLIST Or _
                SEE_MASK_FLAG_NO_UI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = frmMain.Handle
            .lpVerb = vbNullChar
            .lpFile = _Path
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return ShellExecuteEx(SEI)
    End Function

    ' Retrieve a good formated path from a bad string
    Public Function IntelligentPathRetrieving() As String
        Dim rootDir As String = Space$(256)
        GetWindowsDirectory(rootDir, 256)
        rootDir = Left(rootDir, InStr(rootDir, vbNullChar, CompareMethod.Binary) - 1)

        Dim s As String = Replace(_Path.ToLowerInvariant, "@%systemroot%", rootDir)
        s = Replace(s, "%systemroot%", rootDir)

        ' Get ID and file
        Dim iD As UInteger = 0
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(s, ".exe", , CompareMethod.Binary)
        If i = 0 Then i = InStrRev(s, ".dll", , CompareMethod.Binary)
        file = Left(s, i + 3)
        iD = CUInt(Val(Right(s, s.Length - i - 5)))

        ' Get ressource
        Return Replace(ExtractString(_Path, iD), "\", "\\")

    End Function

    ' Retrieve a good formated path from a bad string
    Public Function IntelligentPathRetrieving2() As String
        ' Get ID and file
        Dim iD As UInteger = 0
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(_Path, ".exe", , CompareMethod.Binary)
        file = Left(_Path, i + 3)
        Return file
    End Function

    ' Return file name from a path
    Public Function GetFileName() As String
        Dim i As Integer = InStrRev(_Path, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return Right(_Path, _Path.Length - i)
        Else
            Return vbNullString
        End If
    End Function

    ' Move a file to the trash
    Public Function MoveToTrash() As Integer
        Try
            My.Computer.FileSystem.DeleteFile(_Path, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Return 0
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ' Kill a file
    Public Function WindowsKill() As Integer
        Try
            My.Computer.FileSystem.DeleteFile(_Path, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.DeletePermanently)
            Return 0
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ' Encrypt the file
    Public Sub Encrypt()
        IO.File.Encrypt(_Path)
    End Sub

    ' Decrypt the file
    Public Sub Decrypt()
        IO.File.Decrypt(_Path)
    End Sub

    ' Move a file
    Public Function WindowsMove(ByVal dest As String) As String
        Try
            My.Computer.FileSystem.MoveFile(_Path, dest & "\" & Me.GetName, FileIO.UIOption.AllDialogs)
            _Path = dest & "\" & Me.GetName
        Catch ex As Exception
            '
        End Try
        Return _Path
    End Function

    ' Copy a file
    Public Function WindowCopy(ByVal dest As String) As Integer
        Try
            My.Computer.FileSystem.CopyFile(_Path, dest, FileIO.UIOption.AllDialogs)
            Return 0
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ' Rename a file
    Public Function WindowsRename(ByVal newName As String) As String
        Try
            My.Computer.FileSystem.RenameFile(_Path, newName)
            _Path = GetParentDir(_Path) & newName
        Catch ex As Exception
            '
        End Try
        Return _Path
    End Function


    ' ========================================
    ' Shared functions
    ' ========================================

    ' Return file name from a path
    Public Shared Function GetFileName(ByVal filePath As String) As String
        Dim i As Integer = InStrRev(filePath, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return Right(filePath, filePath.Length - i)
        Else
            Return vbNullString
        End If
    End Function

    ' Get parent dir
    Public Shared Function GetParentDir(ByVal filePath As String) As String
        Dim i As Integer = InStrRev(filePath, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return filePath.Substring(0, i)
        Else
            Return filePath
        End If
    End Function

    ' Open a directory specified in parameter
    Public Shared Function OpenADirectory(ByVal dir As String) As Integer
        Dim r As Integer = -1
        If IO.Directory.Exists(dir) Then
            r = Shell("explorer.exe " & dir, _
                AppWinStyle.NormalFocus) ' This is some kind of shit, but it's the simpliest way
        End If
        Return r
    End Function

    Public Shared Function ShowRunBox(ByVal hWnd As Integer, ByVal Title As String, _
        ByVal Message As String) As Integer

        Return SHRunDialog(hWnd, 0, 0, New System.Text.StringBuilder(Title), _
            New System.Text.StringBuilder(Message), 0)

    End Function

    ' Return basic file size
    Public Shared Function GetFileSize(ByVal filePath As String) As Long
        Dim ret As Long

        Dim ptr As IntPtr = CreateFile(filePath, EFileAccess._GenericRead, _
            EFileShare._Read Or EFileShare._Write, IntPtr.Zero, _
            ECreationDisposition._OpenExisting, 0, IntPtr.Zero)

        If ptr = CType(-1, IntPtr) Then Return -1

        ' Get sizes
        GetFileSizeEx(ptr, ret)

        CloseHandle(ptr)

        Return ret

    End Function

    ' Open a file/URL
    Public Shared Function ShellOpenFile(ByVal filePath As String) As Boolean
        Dim SEI As SHELLEXECUTEINFO = Nothing
        With SEI
            .fMask = SEE_MASK_NOCLOSEPROCESS Or SEE_MASK_INVOKEIDLIST Or _
                SEE_MASK_FLAG_NO_UI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = frmMain.Handle
            .lpVerb = vbNullChar
            .lpFile = filePath
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return ShellExecuteEx(SEI)
    End Function

    ' Display File Property Box
    Public Shared Function ShowFileProperty(ByVal filePath As String) As Boolean
        Dim SEI As SHELLEXECUTEINFO = Nothing
        With SEI
            .fMask = SEE_MASK_NOCLOSEPROCESS Or SEE_MASK_INVOKEIDLIST Or _
                SEE_MASK_FLAG_NO_UI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = frmMain.Handle
            .lpVerb = "properties"
            .lpFile = filePath
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return ShellExecuteEx(SEI)
    End Function

    ' Open directory
    Public Shared Function OpenDirectory(ByVal filePath As String) As Integer
        Dim r As Integer = -1
        If IO.File.Exists(filePath) Then
            Dim p As String = IO.Directory.GetParent(filePath).FullName
            r = Shell("explorer.exe " & p, _
                AppWinStyle.NormalFocus) ' This is some kind of shit, but it's the simpliest way
        End If
        Return r
    End Function

    ' Retrieve a good formated path from a bad string
    Public Shared Function IntelligentPathRetrieving(ByVal filePath As String) As String
        Dim rootDir As String = Space$(256)
        GetWindowsDirectory(rootDir, 256)
        rootDir = Left(rootDir, InStr(rootDir, vbNullChar, CompareMethod.Binary) - 1)

        Dim s As String = Replace(filePath.ToLowerInvariant, "@%systemroot%", rootDir)
        s = Replace(s, "%systemroot%", rootDir)

        ' Get ID and file
        Dim iD As UInteger = 0
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(s, ".exe", , CompareMethod.Binary)
        If i = 0 Then i = InStrRev(s, ".dll", , CompareMethod.Binary)
        file = Left(s, i + 3)
        iD = CUInt(Val(Right(s, s.Length - i - 5)))

        ' Get ressource
        Return Replace(ExtractString(filePath, iD), "\", "\\")

    End Function

    ' Retrieve a good formated path from a bad string
    Public Shared Function IntelligentPathRetrieving2(ByVal filePath As String) As String
        ' Get ID and file
        Dim iD As UInteger = 0
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(filePath, ".exe", , CompareMethod.Binary)
        file = Left(filePath, i + 3)
        Return file
    End Function




    ' ========================================
    ' Private functions
    ' ========================================

    ' Extract an ressource (string) from a file
    Private Shared Function ExtractString(ByVal file As String, ByVal id As UInteger) As String
        Dim hInst As IntPtr = LoadLibrary(file)
        Dim sb As New StringBuilder(1024)
        Dim len As Integer = LoadString(hInst, id, sb, sb.Capacity)
        FreeLibrary(hInst)
        Return sb.ToString
    End Function

End Class
