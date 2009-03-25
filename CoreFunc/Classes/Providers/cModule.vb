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

Public MustInherit Class cModule
    Inherits cGeneralObject

    Friend Const NO_INFO_RETRIEVED As String = "N/A"

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


    ' ========================================
    ' Getter & setter
    ' ========================================
    Public MustOverride ReadOnly Property Name() As String
    Public MustOverride ReadOnly Property Key() As String
    Public MustOverride ReadOnly Property FilePath() As String
    Public MustOverride ReadOnly Property ProcessId() As Integer
    Public MustOverride ReadOnly Property Comments() As String
    Public MustOverride ReadOnly Property CompanyName() As String
    Public MustOverride ReadOnly Property FileBuildPart() As Integer
    Public MustOverride ReadOnly Property FILE_VERSION_INFO() As System.Diagnostics.FileVersionInfo
    Public MustOverride ReadOnly Property FileDescription() As String
    Public MustOverride ReadOnly Property FileMajorPart() As Integer
    Public MustOverride ReadOnly Property FileMinorPart() As Integer
    Public MustOverride ReadOnly Property FileName() As String
    Public MustOverride ReadOnly Property FilePrivatePart() As Integer
    Public MustOverride ReadOnly Property FileVersion() As String
    Public MustOverride ReadOnly Property InternalName() As String
    Public MustOverride ReadOnly Property IsDebug() As Boolean
    Public MustOverride ReadOnly Property IsPatched() As Boolean
    Public MustOverride ReadOnly Property IsPreRelease() As Boolean
    Public MustOverride ReadOnly Property IsPrivateBuild() As Boolean
    Public MustOverride ReadOnly Property IsSpecialBuild() As Boolean
    Public MustOverride ReadOnly Property Language() As String
    Public MustOverride ReadOnly Property LegalCopyright() As String
    Public MustOverride ReadOnly Property LegalTrademarks() As String
    Public MustOverride ReadOnly Property OriginalFilename() As String
    Public MustOverride ReadOnly Property PrivateBuild() As String
    Public MustOverride ReadOnly Property ProductBuildPart() As Integer
    Public MustOverride ReadOnly Property ProductMajorPart() As Integer
    Public MustOverride ReadOnly Property ProductMinorPart() As Integer
    Public MustOverride ReadOnly Property ProductName() As String
    Public MustOverride ReadOnly Property ProductPrivatePart() As Integer
    Public MustOverride ReadOnly Property ProductVersion() As String
    Public MustOverride ReadOnly Property SpecialBuild() As String
    Public MustOverride ReadOnly Property BaseAddress() As Integer
    Public MustOverride ReadOnly Property ModuleMemorySize() As Integer


    ' ========================================
    ' Public functions
    ' ========================================

    ' Unload the specified module
    Public MustOverride Function UnloadModule() As Integer


    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(27) As String

        s(0) = "Version"
        s(1) = "Description"
        s(2) = "CompanyName"
        s(3) = "Path"
        s(4) = "Address"
        s(5) = "Comments"
        s(6) = "FileBuildPart"
        s(7) = "FileMajorPart"
        s(8) = "FileMinorPart"
        s(9) = "FilePrivatePart"
        s(10) = "InternalName"
        s(11) = "IsDebug"
        s(12) = "IsPatched"
        s(13) = "IsPreRelease"
        s(14) = "IsPrivateBuild"
        s(15) = "IsSpecialBuild"
        s(16) = "Language"
        s(17) = "LegalCopyright"
        s(18) = "LegalTrademarks"
        s(19) = "OriginalFilename"
        s(20) = "PrivateBuild"
        s(21) = "ProductBuildPart"
        s(22) = "ProductMajorPart"
        s(23) = "ProductMinorPart"
        s(24) = "ProductName"
        s(25) = "ProductPrivatePart"
        s(26) = "ProductVersion"
        s(27) = "SpecialBuild"

        Return s
    End Function

    ' ========================================
    ' Private functions
    ' ========================================
    Friend Shared Function FormatString(ByRef sString As String) As String
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
