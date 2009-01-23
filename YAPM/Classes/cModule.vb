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

Public Class cModule

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _path As String
    Private _pid As Integer
    Private _mdl As ProcessModule


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "Gettet & setter"
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property Comments() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).Comments
        End Get
    End Property
    Public ReadOnly Property CompanyName() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).CompanyName
        End Get
    End Property
    Public ReadOnly Property FileBuildPart() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileBuildPart
        End Get
    End Property
    Public ReadOnly Property FileDescription() As String
        Get
            Return (System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileDescription)
        End Get
    End Property
    Public ReadOnly Property FileMajorPart() As Integer
        Get
            Return (System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileMajorPart)
        End Get
    End Property
    Public ReadOnly Property FileMinorPart() As Integer
        Get
            Return (System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileMinorPart)
        End Get
    End Property
    Public ReadOnly Property FileName() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileName
        End Get
    End Property
    Public ReadOnly Property FilePrivatePart() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FilePrivatePart
        End Get
    End Property
    Public ReadOnly Property FileVersion() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileVersion
        End Get
    End Property
    Public ReadOnly Property InternalName() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).InternalName
        End Get
    End Property
    Public ReadOnly Property IsDebug() As Boolean
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).IsDebug
        End Get
    End Property
    Public ReadOnly Property IsPatched() As Boolean
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).IsPatched
        End Get
    End Property
    Public ReadOnly Property IsPreRelease() As Boolean
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).IsPreRelease
        End Get
    End Property
    Public ReadOnly Property IsPrivateBuild() As Boolean
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).IsPrivateBuild
        End Get
    End Property
    Public ReadOnly Property IsSpecialBuild() As Boolean
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).IsSpecialBuild
        End Get
    End Property
    Public ReadOnly Property Language() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).Language
        End Get
    End Property
    Public ReadOnly Property LegalCopyright() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).LegalCopyright
        End Get
    End Property
    Public ReadOnly Property LegalTrademarks() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).LegalTrademarks
        End Get
    End Property
    Public ReadOnly Property OriginalFilename() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).OriginalFilename
        End Get
    End Property
    Public ReadOnly Property PrivateBuild() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).PrivateBuild
        End Get
    End Property
    Public ReadOnly Property ProductBuildPart() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).ProductBuildPart
        End Get
    End Property
    Public ReadOnly Property ProductMajorPart() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).ProductMajorPart
        End Get
    End Property
    Public ReadOnly Property ProductMinorPart() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).ProductMinorPart
        End Get
    End Property
    Public ReadOnly Property ProductName() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).ProductName
        End Get
    End Property
    Public ReadOnly Property ProductPrivatePart() As Integer
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).ProductPrivatePart
        End Get
    End Property
    Public ReadOnly Property ProductVersion() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).ProductVersion
        End Get
    End Property
    Public ReadOnly Property SpecialBuild() As String
        Get
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).SpecialBuild
        End Get
    End Property
    Public ReadOnly Property BaseAddress() As Integer
        Get
            Return CInt(_mdl.BaseAddress)
        End Get
    End Property
    Public ReadOnly Property EntryPointAddress() As Integer
        Get
            Return CInt(_mdl.EntryPointAddress)
        End Get
    End Property
    Public ReadOnly Property ModuleMemorySize() As Integer
        Get
            Return _mdl.ModuleMemorySize
        End Get
    End Property
#End Region


    ' ========================================
    ' Public functions
    ' ========================================
    Public Sub New(ByVal pid As Integer, ByVal modulePath As String)
        _path = modulePath
        _pid = pid
    End Sub
    Public Sub New(ByVal pid As Integer, ByRef mdl As ProcessModule)
        _path = mdl.FileName
        _pid = pid
        _mdl = mdl
    End Sub

    ' Unload the specified module
    Public Function UnloadModule() As Integer
        Return cProcess.UnLoadModuleFromProcess(_pid, Me.BaseAddress)
    End Function

    ' List modules of an exe file
    Public Shared Function Enumerate(ByVal pid As Integer, ByRef m() As cModule) As Integer

        Try
            Dim t As ProcessModuleCollection = System.Diagnostics.Process.GetProcessById(pid).Modules
            If t Is Nothing Then
                ReDim m(0)
                Return 0
            End If

            ReDim m(t.Count)
            Dim x As Integer = 0
            Dim it As ProcessModule
            For Each it In t
                m(x) = New cModule(pid, it)
                x += 1
            Next
            Return t.Count
        Catch ex As Exception
            ReDim m(0)
            Return 0
        End Try

    End Function

    ' ========================================
    ' Private functions
    ' ========================================

End Class
