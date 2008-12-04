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

    Private Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" _
        (ByVal Buffer As String, ByVal Size As Integer) As Integer

    <DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
    End Function

    <DllImport("User32", SetLastError:=True)> _
    Private Function LoadString(ByVal hInstance As IntPtr, ByVal uID As UInt32, ByVal lpBuffer As System.Text.StringBuilder, ByVal nBufferMax As Integer) As Integer
    End Function

    Private Declare Auto Function LoadLibrary Lib "kernel32.dll" (ByVal lpFileName As String) As IntPtr

    <DllImport("kernel32.dll", SetLastError:=True, EntryPoint:="FreeLibrary")> _
    Private Function FreeLibrary(ByVal hModule As IntPtr) As Boolean
    End Function


    Public Const SEE_MASK_INVOKEIDLIST As Integer = &HC
    Public Const SEE_MASK_NOCLOSEPROCESS As Integer = &H40
    Public Const SEE_MASK_FLAG_NO_UI As Integer = &H400


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

End Module
