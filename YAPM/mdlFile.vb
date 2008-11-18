Option Strict On

Imports System.Runtime.InteropServices

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

    <DllImport("Shell32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Public Function ShellExecuteEx(ByRef lpExecInfo As SHELLEXECUTEINFO) As Boolean
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
End Module
