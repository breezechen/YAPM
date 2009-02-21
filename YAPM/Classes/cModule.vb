' =======================================================
' Yet Another Process Monitor (YAPM)
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

Public Class cModule
    Inherits cGeneralObject

#Region "API"

    Private Const TH32CS_SNAPMODULE As Integer = &H8

    Private Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccessas As Integer, ByVal bInheritHandle As Integer, ByVal dwProcId As Integer) As Integer
    Private Declare Function CreateToolhelpSnapshot Lib "kernel32" Alias "CreateToolhelp32Snapshot" (ByVal lFlags As Integer, ByVal lProcessID As Integer) As Integer
    Private Declare Function CreateToolhelp32Snapshot Lib "kernel32.dll" (ByVal dwFlags As Integer, ByVal th32ProcessID As Integer) As Integer
    Private Declare Function EnumProcessModules Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByRef lphModule As Integer, ByVal cb As Integer, ByRef cbNeeded As Integer) As Integer
    Private Declare Function GetCurrentProcess Lib "kernel32.dll" () As Integer
    Private Declare Function Module32First Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lppe As MODULEENTRY32) As Integer
    Private Declare Function Module32Next Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpme As MODULEENTRY32) As Integer

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MODULEENTRY32
        Public dwSize As Integer
        Public th32ModuleID As Integer
        Public th32ProcessID As Integer
        Public GlblcntUsage As Integer
        Public ProccntUsage As Integer
        Public modBaseAddr As Integer
        Public modBaseSize As Integer
        Public hModule As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> _
        Public szModule As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szExePath As String
    End Structure

#End Region


    ' ========================================
    ' Private attributes
    ' ========================================
    Private _path As String
    Private _pid As Integer
    Private _name As String
    Private _size As Integer
    Private _baseA As Integer
    Private _fv As FileVersionInfo
    Private _key As String


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "Gettet & setter"
    Public ReadOnly Property Name() As String
        Get
            If _name = vbNullString Then
                _name = cFile.GetFileName(_path)
            End If
            Return _name
        End Get
    End Property
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public ReadOnly Property FilePath() As String
        Get
            Return _path
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property Comments() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.Comments
        End Get
    End Property
    Public ReadOnly Property CompanyName() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.CompanyName
        End Get
    End Property
    Public ReadOnly Property FileBuildPart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FileBuildPart
        End Get
    End Property
    Public ReadOnly Property FILE_VERSION_INFO() As System.Diagnostics.FileVersionInfo
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv
        End Get
    End Property
    Public ReadOnly Property FileDescription() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FileDescription
        End Get
    End Property
    Public ReadOnly Property FileMajorPart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FileMajorPart
        End Get
    End Property
    Public ReadOnly Property FileMinorPart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FileMinorPart
        End Get
    End Property
    Public ReadOnly Property FileName() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FileName
        End Get
    End Property
    Public ReadOnly Property FilePrivatePart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FilePrivatePart
        End Get
    End Property
    Public ReadOnly Property FileVersion() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.FileVersion
        End Get
    End Property
    Public ReadOnly Property InternalName() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.InternalName
        End Get
    End Property
    Public ReadOnly Property IsDebug() As Boolean
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.IsDebug
        End Get
    End Property
    Public ReadOnly Property IsPatched() As Boolean
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.IsPatched
        End Get
    End Property
    Public ReadOnly Property IsPreRelease() As Boolean
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.IsPreRelease
        End Get
    End Property
    Public ReadOnly Property IsPrivateBuild() As Boolean
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.IsPrivateBuild
        End Get
    End Property
    Public ReadOnly Property IsSpecialBuild() As Boolean
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.IsSpecialBuild
        End Get
    End Property
    Public ReadOnly Property Language() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.Language
        End Get
    End Property
    Public ReadOnly Property LegalCopyright() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.LegalCopyright
        End Get
    End Property
    Public ReadOnly Property LegalTrademarks() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.LegalTrademarks
        End Get
    End Property
    Public ReadOnly Property OriginalFilename() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.OriginalFilename
        End Get
    End Property
    Public ReadOnly Property PrivateBuild() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.PrivateBuild
        End Get
    End Property
    Public ReadOnly Property ProductBuildPart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.ProductBuildPart
        End Get
    End Property
    Public ReadOnly Property ProductMajorPart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.ProductMajorPart
        End Get
    End Property
    Public ReadOnly Property ProductMinorPart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.ProductMinorPart
        End Get
    End Property
    Public ReadOnly Property ProductName() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.ProductName
        End Get
    End Property
    Public ReadOnly Property ProductPrivatePart() As Integer
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.ProductPrivatePart
        End Get
    End Property
    Public ReadOnly Property ProductVersion() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.ProductVersion
        End Get
    End Property
    Public ReadOnly Property SpecialBuild() As String
        Get
            If _fv Is Nothing Then
                _fv = System.Diagnostics.FileVersionInfo.GetVersionInfo(_path)
            End If
            Return _fv.SpecialBuild
        End Get
    End Property
    Public ReadOnly Property BaseAddress() As Integer
        Get
            Return _baseA
        End Get
    End Property
    Public ReadOnly Property ModuleMemorySize() As Integer
        Get
            Return _size
        End Get
    End Property
#End Region

    ' ========================================
    ' Public functions
    ' ========================================
    Public Sub New(ByVal key As String, ByRef mdl As MODULEENTRY32)
        _key = key
        _path = FormatString(mdl.szExePath)
        _size = mdl.modBaseSize
        _baseA = mdl.modBaseAddr
        _pid = mdl.th32ProcessID
        _name = cFile.GetFileName(_path)
    End Sub

    ' Unload the specified module
    Public Function UnloadModule() As Integer
        Return cProcess.UnLoadModuleFromProcess(_pid, Me.BaseAddress)
    End Function

    ' List modules of an exe file
    Public Shared Function Enumerate(ByVal pid As Integer, ByRef key() As String, _
                                     ByRef _dico As Dictionary(Of String, MODULEENTRY32)) As Integer

        Dim t() As MODULEENTRY32 = EnumerateSpeed(pid)
        _dico.Clear()

        ReDim key(t.Length - 1)
        Dim x As Integer = 0
        For Each it As MODULEENTRY32 In t
            Dim s As String = FormatString(it.szExePath)
            If Len(s) > 2 Then
                key(x) = s & "|" & it.modBaseAddr.ToString
                _dico.Add(key(x), it)
                x += 1
            End If
        Next

        ReDim Preserve key(x - 1)
        Return t.Length

    End Function

    ' Enumerate 2
    Public Shared Function EnumerateSpeed(ByVal pid As Integer) As MODULEENTRY32()

        Dim lSnap As Integer
        Dim x As Integer = 0
        Dim mdMOD As New MODULEENTRY32
        Dim mdTemp() As MODULEENTRY32
        ReDim mdTemp(0)

        If pid <= 4 Then
            Return mdTemp
        End If

        lSnap = CreateToolhelp32Snapshot(TH32CS_SNAPMODULE, pid)

        mdMOD.dwSize = 548 'System.Runtime.InteropServices.Marshal.SizeOf(mdMOD)

        If Module32First(lSnap, mdMOD) > 0 Then

            mdTemp(0) = mdMOD
            mdMOD.dwSize = 548      'Len(mdMOD)

            Do While Module32Next(lSnap, mdMOD) > 0

                x += 1
                ReDim Preserve mdTemp(x)
                mdTemp(x) = mdMOD

                mdMOD.dwSize = 548  'Len(mdMOD)
            Loop
        Else
            ReDim mdTemp(1)
        End If

        CloseHandle(lSnap)

        Return mdTemp

    End Function

    ' Get some informations
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""
        Try
            Select Case info
                Case "Name"
                    res = Me.Name
                Case "Version"
                    res = Me.FileVersion
                Case "Description"
                    res = Me.FileDescription
                Case "CompanyName"
                    res = Me.CompanyName
                Case "Path"
                    res = _path
                Case "Address"
                    res = "0x" & Me.BaseAddress.ToString("x")
            End Select
        Catch ex As Exception
            res = ""
        End Try

        Return res
    End Function


    ' ========================================
    ' Private functions
    ' ========================================
    Private Shared Function FormatString(ByRef sString As String) As String
        Dim i As Integer = InStr(sString, vbNullChar)
        If i > 0 Then
            Return Trim(Left(sString, i - 1)).ToLowerInvariant
        Else
            If sString IsNot Nothing Then
                Return sString.ToLowerInvariant
            Else
                Return ""
            End If
        End If
    End Function

End Class
