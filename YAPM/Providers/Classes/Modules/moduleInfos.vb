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

<Serializable()> Public Class moduleInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _name As String
    Private _path As String
    Private _address As IntPtr
    Private _size As Integer
    Private _entryPoint As IntPtr
    Private _processId As Integer
    Private _flags As Native.Api.NativeEnums.LdrpDataTableEntryFlags
    <NonSerialized()> Private _fileInfo As FileVersionInfo

    Private _manufacturer As String
    Private _version As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property Flags() As Native.Api.NativeEnums.LdrpDataTableEntryFlags
        Get
            Return _flags
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _processId
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Path() As String
        Get
            Return _path
        End Get
    End Property
    Public ReadOnly Property BaseAddress() As IntPtr
        Get
            Return _address
        End Get
    End Property
    Public ReadOnly Property EntryPoint() As IntPtr
        Get
            Return _entryPoint
        End Get
    End Property
    Public ReadOnly Property Size() As Integer
        Get
            Return _size
        End Get
    End Property
    Public ReadOnly Property FileInfo() As FileVersionInfo
        Get
            Return _fileInfo
        End Get
    End Property
    Public ReadOnly Property Manufacturer() As String
        Get
            Return _manufacturer
        End Get
    End Property
    Public ReadOnly Property Version() As String
        Get
            Return _version
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef data As Native.Api.NativeStructs.LdrDataTableEntry, ByVal pid As Integer, _
                   ByVal path As String, ByVal dllName As String, _
                   Optional ByVal noFileInfo As Boolean = False)
        With data
            _size = .SizeOfImage
            _entryPoint = .EntryPoint
            _address = .DllBase
            _flags = .Flags
        End With
        _processId = pid

        If path.ToLowerInvariant.StartsWith("\systemroot\") Then
            path = path.Substring(12, path.Length - 12)
            Dim ii As Integer = InStr(path, "\", CompareMethod.Binary)
            If ii > 0 Then
                path = path.Substring(ii, path.Length - ii)
                path = Environment.SystemDirectory & "\" & path
            End If
        ElseIf path.StartsWith("\??\") Then
            path = path.Substring(4)
        End If
        _path = path
        _name = dllName

        If noFileInfo = False Then
            ' Retrieve infos about file
            ' Retrive infos ONLY ONE time (save path and infos into a dico)
            ' As this constructor is only used for local connection, there is
            ' nothing to check concerning connection
            asyncCallbackModuleEnumerate.semDicoFileInfos.WaitOne()
            If asyncCallbackModuleEnumerate.fileInformations.ContainsKey(_path) = False Then
                Try
                    _fileInfo = FileVersionInfo.GetVersionInfo(path)
                Catch ex As Exception
                    _fileInfo = Nothing
                End Try
                asyncCallbackModuleEnumerate.fileInformations.Add(_path, _fileInfo)
            Else
                _fileInfo = asyncCallbackModuleEnumerate.fileInformations.Item(_path)
            End If
            asyncCallbackModuleEnumerate.semDicoFileInfos.Release()
        Else
            _fileInfo = Nothing
        End If
    End Sub

    Public Sub New(ByRef Thr As Native.Api.NativeStructs.LdrDataTableEntry, _
                   ByVal pid As Integer, _
                   ByVal path As String, _
                   ByVal version As String, _
                   ByVal manufacturer As String)
        ' This constructor is used for WMI
        With Thr
            _size = .SizeOfImage
            _entryPoint = .EntryPoint
            _address = .DllBase
        End With
        _processId = pid

        _path = Common.Misc.GetRealPath(path)
        _name = cFile.GetFileName(_path)
        _manufacturer = manufacturer
        _version = Version
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As moduleInfos)
        With newI
            _size = .Size
            _address = .BaseAddress
            _entryPoint = .EntryPoint
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(30) As String

        s(0) = "Size"
        s(1) = "Address"
        s(2) = "Flags"
        s(3) = "Version"
        s(4) = "Description"
        s(5) = "CompanyName"
        s(6) = "Path"
        s(7) = "Comments"
        s(8) = "FileBuildPart"
        s(9) = "FileMajorPart"
        s(10) = "FileMinorPart"
        s(11) = "FilePrivatePart"
        s(12) = "InternalName"
        s(13) = "IsDebug"
        s(14) = "IsPatched"
        s(15) = "IsPreRelease"
        s(16) = "IsPrivateBuild"
        s(17) = "IsSpecialBuild"
        s(18) = "Language"
        s(19) = "LegalCopyright"
        s(20) = "LegalTrademarks"
        s(21) = "OriginalFilename"
        s(22) = "PrivateBuild"
        s(23) = "ProductBuildPart"
        s(24) = "ProductMajorPart"
        s(25) = "ProductMinorPart"
        s(26) = "ProductName"
        s(27) = "ProductPrivatePart"
        s(28) = "ProductVersion"
        s(29) = "SpecialBuild"
        s(30) = "ProcessId"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
