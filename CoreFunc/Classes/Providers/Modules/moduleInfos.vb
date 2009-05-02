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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices

<Serializable()> Public Class moduleInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _name As String
    Private _path As String
    Private _address As Integer
    Private _size As Integer
    Private _entryPoint As Integer
    Private _processId As Integer
    <NonSerialized()> Private _fileInfo As FileVersionInfo

    Private _manufacturer As String
    Private _version As String

#End Region

#Region "Read only properties"

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
    Public ReadOnly Property BaseAddress() As Integer
        Get
            Return _address
        End Get
    End Property
    Public ReadOnly Property EntryPoint() As Integer
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
    Public Sub New(ByRef Thr As API.MODULEINFO, ByVal pid As Integer, _
                   ByVal path As String, Optional ByVal noFileInfo As Boolean = False) ', Optional ByVal getFileInfos As Boolean = True)
        With Thr
            _size = .SizeOfImage
            _entryPoint = .EntryPoint.ToInt32
            _address = .BaseOfDll.ToInt32
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
        _name = cFile.GetFileName(_path)

        If noFileInfo = False Then
            Try
                _fileInfo = FileVersionInfo.GetVersionInfo(path)
            Catch ex As Exception
                _fileInfo = Nothing
            End Try
        Else
            _fileInfo = Nothing
        End If

    End Sub
    Public Sub New(ByRef Thr As API.MODULEINFO, ByVal pid As Integer, _
                   ByVal path As String, ByVal version As String, ByVal manufacturer As String)
        With Thr
            _size = .SizeOfImage
            _entryPoint = .EntryPoint.ToInt32
            _address = .BaseOfDll.ToInt32
        End With
        _processId = pid

        _path = mdlMisc.GetRealPath(path)
        _name = cFile.GetFileName(_path)
        _manufacturer = manufacturer
        _version = version
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
    Public Shared Function GetAvailableProperties() As String()
        Dim s(29) As String

        s(0) = "Size"
        s(1) = "Address"
        s(2) = "Version"
        s(3) = "Description"
        s(4) = "CompanyName"
        s(5) = "Path"
        s(6) = "Comments"
        s(7) = "FileBuildPart"
        s(8) = "FileMajorPart"
        s(9) = "FileMinorPart"
        s(10) = "FilePrivatePart"
        s(11) = "InternalName"
        s(12) = "IsDebug"
        s(13) = "IsPatched"
        s(14) = "IsPreRelease"
        s(15) = "IsPrivateBuild"
        s(16) = "IsSpecialBuild"
        s(17) = "Language"
        s(18) = "LegalCopyright"
        s(19) = "LegalTrademarks"
        s(20) = "OriginalFilename"
        s(21) = "PrivateBuild"
        s(22) = "ProductBuildPart"
        s(23) = "ProductMajorPart"
        s(24) = "ProductMinorPart"
        s(25) = "ProductName"
        s(26) = "ProductPrivatePart"
        s(27) = "ProductVersion"
        s(28) = "SpecialBuild"
        s(29) = "ProcessId"

        Return s
    End Function

End Class
