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

<Serializable()> _
Public Class SerializableFileVersionInfo

#Region "Private attributes"
    Private _Comments As String
    Private _CompanyName As String
    Private _FileBuildPart As Integer
    Private _FileDescription As String
    Private _FileMajorPart As Integer
    Private _FileMinorPart As Integer
    Private _FileName As String
    Private _FilePrivatePart As Integer
    Private _FileVersion As String
    Private _InternalName As String
    Private _IsDebug As Boolean
    Private _IsPatched As Boolean
    Private _IsPreRelease As Boolean
    Private _IsPrivateBuild As Boolean
    Private _IsSpecialBuild As Boolean
    Private _Language As String
    Private _LegalCopyright As String
    Private _LegalTrademarks As String
    Private _OriginalFilename As String
    Private _PrivateBuild As String
    Private _ProductBuildPart As Integer
    Private _ProductMajorPart As Integer
    Private _ProductMinorPart As Integer
    Private _ProductName As String
    Private _ProductPrivatePart As Integer
    Private _ProductVersion As String
    Private _SpecialBuild As String
#End Region

#Region "Read only properties"
    Public ReadOnly Property Comments() As String
        Get
            Return _Comments
        End Get
    End Property
    Public ReadOnly Property CompanyName() As String
        Get
            Return _CompanyName
        End Get
    End Property
    Public ReadOnly Property FileBuildPart() As Integer
        Get
            Return _FileBuildPart
        End Get
    End Property
    Public ReadOnly Property FileDescription() As String
        Get
            Return _FileDescription
        End Get
    End Property
    Public ReadOnly Property FileMajorPart() As Integer
        Get
            Return _FileMajorPart
        End Get
    End Property
    Public ReadOnly Property FileMinorPart() As Integer
        Get
            Return _FileMinorPart
        End Get
    End Property
    Public ReadOnly Property FileName() As String
        Get
            Return _FileName
        End Get
    End Property
    Public ReadOnly Property FilePrivatePart() As Integer
        Get
            Return _FilePrivatePart
        End Get
    End Property
    Public ReadOnly Property FileVersion() As String
        Get
            Return _FileVersion
        End Get
    End Property
    Public ReadOnly Property InternalName() As String
        Get
            Return _InternalName
        End Get
    End Property
    Public ReadOnly Property IsDebug() As Boolean
        Get
            Return _IsDebug
        End Get
    End Property
    Public ReadOnly Property IsPatched() As Boolean
        Get
            Return _IsPatched
        End Get
    End Property
    Public ReadOnly Property IsPreRelease() As Boolean
        Get
            Return _IsPreRelease
        End Get
    End Property
    Public ReadOnly Property IsPrivateBuild() As Boolean
        Get
            Return _IsPrivateBuild
        End Get
    End Property
    Public ReadOnly Property IsSpecialBuild() As Boolean
        Get
            Return _IsSpecialBuild
        End Get
    End Property
    Public ReadOnly Property Language() As String
        Get
            Return _Language
        End Get
    End Property
    Public ReadOnly Property LegalCopyright() As String
        Get
            Return _LegalCopyright
        End Get
    End Property
    Public ReadOnly Property LegalTrademarks() As String
        Get
            Return _LegalTrademarks
        End Get
    End Property
    Public ReadOnly Property OriginalFilename() As String
        Get
            Return _OriginalFilename
        End Get
    End Property
    Public ReadOnly Property PrivateBuild() As String
        Get
            Return _PrivateBuild
        End Get
    End Property
    Public ReadOnly Property ProductBuildPart() As Integer
        Get
            Return _ProductBuildPart
        End Get
    End Property
    Public ReadOnly Property ProductMajorPart() As Integer
        Get
            Return _ProductMajorPart
        End Get
    End Property
    Public ReadOnly Property ProductMinorPart() As Integer
        Get
            Return _ProductMinorPart
        End Get
    End Property
    Public ReadOnly Property ProductName() As String
        Get
            Return _ProductName
        End Get
    End Property
    Public ReadOnly Property ProductPrivatePart() As Integer
        Get
            Return _ProductPrivatePart
        End Get
    End Property
    Public ReadOnly Property ProductVersion() As String
        Get
            Return _ProductVersion
        End Get
    End Property
    Public ReadOnly Property SpecialBuild() As String
        Get
            Return _SpecialBuild
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New(ByVal info As System.Diagnostics.FileVersionInfo)
        With info
            _Comments = .Comments
            _CompanyName = .CompanyName
            _FileBuildPart = .FileBuildPart
            _FileDescription = .FileDescription
            _FileMajorPart = FileMajorPart
            _FileMinorPart = .FileMinorPart
            _FileName = .FileName
            _FilePrivatePart = .FilePrivatePart
            _FileVersion = .FileVersion
            _InternalName = .InternalName
            _IsDebug = .IsDebug
            _IsPatched = .IsPatched
            _IsPreRelease = .IsPreRelease
            _IsPrivateBuild = .IsPrivateBuild
            _IsSpecialBuild = .IsSpecialBuild
            _Language = .Language
            _LegalCopyright = .LegalCopyright
            _LegalTrademarks = .LegalTrademarks
            _OriginalFilename = .OriginalFilename
            _PrivateBuild = .PrivateBuild
            _ProductBuildPart = .ProductBuildPart
            _ProductMajorPart = .ProductMajorPart
            _ProductMinorPart = .ProductMinorPart
            _ProductName = .ProductName
            _ProductPrivatePart = .ProductPrivatePart
            _ProductVersion = .ProductVersion
            _SpecialBuild = .SpecialBuild
        End With
    End Sub
#End Region

End Class