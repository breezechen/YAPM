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
Imports System.Text

Public Class cFile

    ' ========================================
    ' Private attributes
    ' ========================================
#Region "Attributes"
    Private _tFileVersion As SerializableFileVersionInfo
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
#Region "Getter & setter"
    Public ReadOnly Property FileVersion() As SerializableFileVersionInfo
        Get
            Return _tFileVersion
        End Get
    End Property
    Public ReadOnly Property IsArchive() As Boolean
        Get
            Return _isArchive
        End Get
    End Property
    Public ReadOnly Property IsCompressed() As Boolean
        Get
            Return _isCompressed
        End Get
    End Property
    Public ReadOnly Property IsDevice() As Boolean
        Get
            Return _isDevice
        End Get
    End Property
    Public ReadOnly Property IsDirectory() As Boolean
        Get
            Return _isDirectory
        End Get
    End Property
    Public ReadOnly Property IsEncrypted() As Boolean
        Get
            Return _isEncrypted
        End Get
    End Property
    Public ReadOnly Property IsHidden() As Boolean
        Get
            Return _isHidden
        End Get
    End Property
    Public ReadOnly Property IsNormal() As Boolean
        Get
            Return _isNormal
        End Get
    End Property
    Public ReadOnly Property IsNotContentIndexed() As Boolean
        Get
            Return _isNotContentIndexed
        End Get
    End Property
    Public ReadOnly Property IsOffline() As Boolean
        Get
            Return _isOffline
        End Get
    End Property
    Public ReadOnly Property IsReadOnly() As Boolean
        Get
            Return _isReadOnly
        End Get
    End Property
    Public ReadOnly Property IsReparsePoint() As Boolean
        Get
            Return _isReparsePoint
        End Get
    End Property
    Public ReadOnly Property IsSparseFile() As Boolean
        Get
            Return _isSparseFile
        End Get
    End Property
    Public ReadOnly Property IsSystem() As Boolean
        Get
            Return _isSystem
        End Get
    End Property
    Public ReadOnly Property IsTemporary() As Boolean
        Get
            Return _isTemporary
        End Get
    End Property
    Public ReadOnly Property DateCreated() As String
        Get
            Return _DateCreated
        End Get
    End Property
    Public ReadOnly Property DateLastAccessed() As String
        Get
            Return _DateLastAccessed
        End Get
    End Property
    Public ReadOnly Property DateLastModified() As String
        Get
            Return _DateLastModified
        End Get
    End Property
    Public ReadOnly Property FileSize() As Long
        Get
            Return _FileSize
        End Get
    End Property
    Public ReadOnly Property CompressedFileSize() As Long
        Get
            Return _CompressedFileSize
        End Get
    End Property
    Public ReadOnly Property ParentDirectory() As String
        Get
            Return _ParentDirectory
        End Get
    End Property
    Public ReadOnly Property DirectoryDepth() As Integer
        Get
            Return _DirectoryDepth
        End Get
    End Property
    Public ReadOnly Property FileExtension() As String
        Get
            Return _FileExtension
        End Get
    End Property
    Public ReadOnly Property FileAssociatedProgram() As String
        Get
            Return _FileAssociatedProgram
        End Get
    End Property
    Public ReadOnly Property FileType() As String
        Get
            Return _FileType
        End Get
    End Property
    Public ReadOnly Property FileAvailableForWrite() As Boolean
        Get
            Return _FileAvailableForWrite
        End Get
    End Property
    Public ReadOnly Property FileAvailableForRead() As Boolean
        Get
            Return _FileAvailableForRead
        End Get
    End Property
    Public ReadOnly Property ShortPath() As String
        Get
            Return _ShortPath
        End Get
    End Property
    Public ReadOnly Property ShortName() As String
        Get
            Return _ShortName
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property
    Public ReadOnly Property Path() As String
        Get
            Return _Path
        End Get
    End Property
    Public Property CreationTime() As Date
        Get
            Return IO.File.GetCreationTime(_Path)
        End Get
        Set(ByVal value As Date)
            IO.File.SetCreationTime(_Path, value)
        End Set
    End Property
    Public Property LastAccessTime() As Date
        Get
            Return IO.File.GetLastAccessTime(_Path)
        End Get
        Set(ByVal value As Date)
            IO.File.SetLastAccessTime(_Path, value)
        End Set
    End Property
    Public Property LastWriteTime() As Date
        Get
            Return IO.File.GetLastWriteTime(_Path)
        End Get
        Set(ByVal value As Date)
            IO.File.SetLastWriteTime(_Path, value)
        End Set
    End Property
    Public Property Attributes() As IO.FileAttributes
        Get
            Return IO.File.GetAttributes(_Path)
        End Get
        Set(ByVal value As IO.FileAttributes)
            IO.File.SetAttributes(_Path, value)
        End Set
    End Property
#End Region


    ' ========================================
    ' Public functions
    ' ========================================

    ' Refresh all file informations
    Public Sub Refresh()

        ' Get a handle
        Dim ptr As IntPtr = Native.Api.NativeFunctions.CreateFile(_Path, Native.Api.NativeEnums.EFileAccess.GenericRead, Native.Api.NativeEnums.EFileShare.Read Or _
             Native.Api.NativeEnums.EFileShare.Write, IntPtr.Zero, Native.Api.NativeEnums.ECreationDisposition.OpenExisting, 0, IntPtr.Zero)

        If ptr = CType(-1, IntPtr) Then Exit Sub

        ' Get sizes
        Native.Api.NativeFunctions.GetFileSizeEx(ptr, _FileSize)
        _CompressedFileSize = Native.Api.NativeFunctions.GetCompressedFileSize(_Path, 0)

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
        Native.Api.NativeFunctions.FindExecutable(_Path, vbNullString, sb)
        _FileAssociatedProgram = Replace(sb.ToString, "\", "\\")

        ' File type
        Try
            Dim ft As String = CStr(My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\." & LCase(_FileExtension), "", ""))
            ft = CStr(My.Computer.Registry.GetValue("HKEY_CLASSES_ROOT\" & ft, "", ""))
            _FileType = CStr(IIf(ft = vbNullString, "Unknown", ft))
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

        ' Short name/path
        Dim sb2 As New StringBuilder(80)
        Dim sb3 As New StringBuilder(80)
        Native.Api.NativeFunctions.GetShortPathName(_Path, sb2, 80)
        Native.Api.NativeFunctions.GetShortPathName(_ParentDirectory, sb3, 80)
        _ShortName = Replace(sb2.ToString, "\", "\\")
        _ShortPath = Replace(sb3.ToString, "\", "\\")

        Dim ptrR As IntPtr = Native.Api.NativeFunctions.CreateFile(_Path, Native.Api.NativeEnums.EFileAccess.GenericRead, _
                     Native.Api.NativeEnums.EFileShare.Read Or Native.Api.NativeEnums.EFileShare.Write, IntPtr.Zero, _
                     Native.Api.NativeEnums.ECreationDisposition.OpenExisting, 0, IntPtr.Zero)
        If ptrR.IsNotNull Then
            Native.Api.NativeFunctions.CloseHandle(ptrR)
            _FileAvailableForRead = True
        End If

        Dim ptrW As IntPtr = Native.Api.NativeFunctions.CreateFile(_Path, Native.Api.NativeEnums.EFileAccess.GenericWrite, _
                    Native.Api.NativeEnums.EFileShare.Read Or Native.Api.NativeEnums.EFileShare.Write, IntPtr.Zero, _
                     Native.Api.NativeEnums.ECreationDisposition.OpenExisting, 0, IntPtr.Zero)
        If ptrW.IsNotNull Then
            Native.Api.NativeFunctions.CloseHandle(ptrW)
            _FileAvailableForWrite = True
        End If

        ' Infos about dll/exe
        _tFileVersion = New SerializableFileVersionInfo(System.Diagnostics.FileVersionInfo.GetVersionInfo(_Path))

        sb2 = Nothing
        sb3 = Nothing
        sb = Nothing
        Native.Api.NativeFunctions.CloseHandle(ptr)
    End Sub

    ' Display File Property Box
    Public Function ShowFileProperty(ByVal handle As IntPtr) As Boolean
        Dim SEI As Native.Api.NativeStructs.ShellExecuteInfo = Nothing
        With SEI
            .fMask = Native.Api.NativeEnums.ShellExecuteInfoMask.NoUI Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.InvokeIdList Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.NoCloseProcess
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = handle
            .lpVerb = "properties"
            .lpFile = _Path
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return Native.Api.NativeFunctions.ShellExecuteEx(SEI)
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
    Public Function ShellOpenFile(ByVal handle As IntPtr) As Boolean
        Dim SEI As Native.Api.NativeStructs.ShellExecuteInfo = Nothing
        With SEI
            .fMask = Native.Api.NativeEnums.ShellExecuteInfoMask.NoUI Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.InvokeIdList Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.NoCloseProcess
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = handle
            .lpVerb = vbNullChar
            .lpFile = _Path
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return Native.Api.NativeFunctions.ShellExecuteEx(SEI)
    End Function

    ' Retrieve a good formated path from a bad string
    Public Function IntelligentPathRetrieving() As String
        Dim rootDir As String = System.Environment.GetFolderPath(Environment.SpecialFolder.System)

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
        Return Replace(Native.Objects.File.GetResourceStringFromFile(_Path, iD), "\", "\\")

    End Function

    ' Retrieve a good formated path from a bad string
    Public Function IntelligentPathRetrieving2() As String
        ' Get ID and file
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
            My.Computer.FileSystem.MoveFile(_Path, dest & "\" & Me.Name, FileIO.UIOption.AllDialogs)
            _Path = dest & "\" & Me.Name
        Catch ex As Exception
            Misc.ShowDebugError(ex)
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
            Misc.ShowDebugError(ex)
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

    Public Shared Function ShowRunBox(ByVal hWnd As IntPtr, ByVal Title As String, _
        ByVal Message As String) As Integer

        Return Native.Api.NativeFunctions.RunFileDlg(hWnd, IntPtr.Zero, Nothing, _
                            Title, Message, Native.Api.NativeEnums.RunFileDialogFlags.None)

    End Function

    ' Return basic file size
    Public Shared Function GetFileSize(ByVal filePath As String) As Long
        Dim ret As Long

        Dim ptr As IntPtr = Native.Api.NativeFunctions.CreateFile(filePath, Native.Api.NativeEnums.EFileAccess.GenericRead, _
             Native.Api.NativeEnums.EFileShare.Read Or Native.Api.NativeEnums.EFileShare.Write, IntPtr.Zero, _
             Native.Api.NativeEnums.ECreationDisposition.OpenExisting, 0, IntPtr.Zero)

        If ptr = CType(-1, IntPtr) Then Return -1

        ' Get sizes
        Native.Api.NativeFunctions.GetFileSizeEx(ptr, ret)

        Native.Api.NativeFunctions.CloseHandle(ptr)

        Return ret

    End Function

    ' Open a file/URL
    Public Shared Function ShellOpenFile(ByVal filePath As String, ByVal handle As IntPtr) As Boolean
        Dim SEI As Native.Api.NativeStructs.ShellExecuteInfo = Nothing
        With SEI
            .fMask = Native.Api.NativeEnums.ShellExecuteInfoMask.NoCloseProcess Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.InvokeIdList Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.NoUI
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = handle
            .lpVerb = vbNullChar
            .lpFile = filePath
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return Native.Api.NativeFunctions.ShellExecuteEx(SEI)
    End Function

    ' Display File Property Box
    Public Shared Function ShowFileProperty(ByVal filePath As String, ByVal handle As IntPtr) As Boolean
        Dim SEI As Native.Api.NativeStructs.ShellExecuteInfo = Nothing
        With SEI
            .fMask = Native.Api.NativeEnums.ShellExecuteInfoMask.NoUI Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.InvokeIdList Or _
                    Native.Api.NativeEnums.ShellExecuteInfoMask.NoCloseProcess

            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(SEI)
            .hwnd = handle
            .lpVerb = "properties"
            .lpFile = filePath
            .lpParameters = vbNullChar
            .lpDirectory = vbNullChar
            .nShow = 0
            .hInstApp = IntPtr.Zero
            .lpIDList = IntPtr.Zero
        End With

        Return Native.Api.NativeFunctions.ShellExecuteEx(SEI)
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
    Public Shared Function IntelligentPathRetrieving2(ByVal filePath As String) As String
        ' Get ID and file
        Dim file As String = vbNullString
        Dim i As Integer = InStrRev(filePath, ".exe", , CompareMethod.Binary)
        file = Left(filePath, i + 3)
        Return file
    End Function


End Class
