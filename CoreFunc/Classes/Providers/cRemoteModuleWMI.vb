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
Imports System.Management

Public Class cRemoteModuleWMI
    Inherits cModule


    ' All informations availables from WMI
    Private Enum WMI_INFO
        AccessMask
        Archive
        'Caption
        Compressed
        CompressionMethod
        'CreationClassName
        CreationDate
        'CSCreationClassName
        'CSName
        'Description
        Drive
        EightDotThreeFileName
        Encrypted
        EncryptionMethod
        Extension
        FileName
        FileSize
        FileType
        'FSCreationClassName
        'FSName
        Hidden
        'InstallDate
        InUseCount
        LastAccessed
        LastModified
        Manufacturer
        Name
        Path
        Readable
        'Status
        System
        Version
        Writeable
    End Enum


    ' ========================================
    ' Private attributes
    ' ========================================
    Private Shared _connection As cRemoteProcessWMI.RemoteConnectionInfoWMI
    Private Shared _con As ConnectionOptions
    Private Shared _tempProcCol As ManagementObjectCollection

    Private _theModule As ManagementObject
    Private _path As String
    Private _name As String
    Private _baseA As Integer
    Private _version As String
    Private _companyName As String
    Private _key As String
    Private _processID As Integer


    ' ========================================
    ' Getter & setter
    ' ========================================
#Region "Gettet & setter"
    Public Overrides ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public Overrides ReadOnly Property FilePath() As String
        Get
            Return _path
        End Get
    End Property
    Public Overrides ReadOnly Property ProcessId() As Integer
        Get
            Return _processID
        End Get
    End Property
    Public Overrides ReadOnly Property Comments() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property CompanyName() As String
        Get
            Return _companyName
        End Get
    End Property
    Public Overrides ReadOnly Property FileBuildPart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property FILE_VERSION_INFO() As System.Diagnostics.FileVersionInfo
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property FileDescription() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property FileMajorPart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property FileMinorPart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property FileName() As String
        Get
            Return _name
        End Get
    End Property
    Public Overrides ReadOnly Property FilePrivatePart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property FileVersion() As String
        Get
            Return _version
        End Get
    End Property
    Public Overrides ReadOnly Property InternalName() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property IsDebug() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property IsPatched() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property IsPreRelease() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property IsPrivateBuild() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property IsSpecialBuild() As Boolean
        Get
            Return False
        End Get
    End Property
    Public Overrides ReadOnly Property Language() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property LegalCopyright() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property LegalTrademarks() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property OriginalFilename() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property PrivateBuild() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property ProductBuildPart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property ProductMajorPart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property ProductMinorPart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property ProductName() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property ProductPrivatePart() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property ProductVersion() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property SpecialBuild() As String
        Get
            Return NO_INFO_RETRIEVED
        End Get
    End Property
    Public Overrides ReadOnly Property BaseAddress() As Integer
        Get
            Return _baseA
        End Get
    End Property
    Public Overrides ReadOnly Property ModuleMemorySize() As Integer
        Get
            Return 0
        End Get
    End Property
#End Region


    ' ========================================
    ' Public functions
    ' ========================================
    Public Sub New(ByVal key As String, ByRef process As MODULEENTRY32, _
                   ByRef connection As cRemoteProcessWMI.RemoteConnectionInfoWMI, ByRef _module As ManagementObject)
        MyBase.New()
        _connection = connection
        _con = New ConnectionOptions
        _key = key
        _name = process.szModule
        _path = process.szExePath
        _baseA = process.modBaseAddr
        _processID = 0      'TODO
        _companyName = CStr(_module.GetPropertyValue("Manufacturer"))
        If _companyName = vbNullString Then
            _companyName = NO_INFO_RETRIEVED
        End If
        _version = CStr(_module.GetPropertyValue("Version"))
        If _version = vbNullString Then
            _version = NO_INFO_RETRIEVED
        End If
        With _con
            .Username = connection.user
            .Password = connection.password
            .Impersonation = ImpersonationLevel.Impersonate
        End With

        ' Get _theProcess from the collection
        _theModule = _module
    End Sub

    ' Unload the specified module
    Public Overrides Function UnloadModule() As Integer
        Return -1
    End Function

    ' List modules of an exe file
    Public Shared Function Enumerate(ByRef _remoteCon As cRemoteProcessWMI.RemoteConnectionInfoWMI, ByRef _process As ManagementObject, ByRef key() As String, _
                                     ByRef _dico As Dictionary(Of String, MODULEENTRY32), ByVal _dicoRemote As Dictionary(Of String, System.Management.ManagementObject)) As Integer

        _dico.Clear()
        _dicoRemote.Clear()

        ' Retrieve base adresses
        Dim _dicoBaseA As New Dictionary(Of String, Integer)
        Dim colMod As ManagementObjectCollection = _process.GetRelationships("CIM_ProcessExecutable")
        For Each refModule As ManagementObject In colMod
            Dim _s As String = CStr(refModule.GetPropertyValue("Antecedent")).ToLowerInvariant
            ' Extract dll path from _s
            Dim i As Integer = InStr(_s, "name=", CompareMethod.Binary)
            Dim __s As String = vbNullString
            If i > 0 Then
                __s = _s.Substring(i + 5, _s.Length - i - 6).Replace("\\", "\")
            End If
            If __s IsNot Nothing Then
                _dicoBaseA.Add(__s, CInt(refModule.GetPropertyValue("BaseAddress")))
            End If
        Next

        ReDim key(0)
        Dim x As Integer = 0
        Dim colModule As ManagementObjectCollection = _process.GetRelated("CIM_DataFile")
        For Each refModule As ManagementObject In colModule
            Dim path As String = CStr(refModule.GetPropertyValue("Name"))
            Dim baseAdd As Integer = 0
            key(x) = path & "|" & baseAdd.ToString
            Dim mm As New MODULEENTRY32
            With mm
                ' Get base address from dico
                If _dicoBaseA.ContainsKey(path.ToLowerInvariant) Then
                    .modBaseAddr = _dicoBaseA.Item(path.ToLowerInvariant)
                Else
                    .modBaseAddr = 0
                End If
                .szExePath = path
                .szModule = cFile.GetFileName(.szExePath)
                .th32ModuleID = 0
                .th32ProcessID = 0
            End With
            _dico.Add(key(x), mm)
            _dicoRemote.Add(key(x), refModule)
            x += 1
            ReDim Preserve key(key.Length)
        Next
        ReDim Preserve key(key.Length - 2)
        Return key.Length

    End Function


    ' Get some informations
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED
        Try
            Select Case info
                Case "Name"
                    res = Me.Name
                Case "Version"
                    res = Me.FileVersion
                Case "Description"
                    '
                Case "CompanyName"
                    res = Me.CompanyName
                Case "Path"
                    res = Me.FilePath
                Case "Address"
                    res = "0x" & Me.BaseAddress.ToString("x")
                Case "Comments"
                    '
                Case "FileBuildPart"
                    '
                Case "FileMajorPart"
                    '
                Case "FileMinorPart"
                    '
                Case "FilePrivatePart"
                    '
                Case "InternalName"
                    '
                Case "IsDebug"
                    '
                Case "IsPatched"
                    '
                Case "IsPreRelease"
                    '
                Case "IsPrivateBuild"
                    '
                Case "IsSpecialBuild"
                    '
                Case "Language"
                    '
                Case "LegalCopyright"
                    '
                Case "LegalTrademarks"
                    '
                Case "OriginalFilename"
                    '
                Case "PrivateBuild"
                    '
                Case "ProductBuildPart"
                    '
                Case "ProductMajorPart"
                    '
                Case "ProductMinorPart"
                    '
                Case "ProductName"
                    '
                Case "ProductPrivatePart"
                    '
                Case "ProductVersion"
                    '
                Case "SpecialBuild"
                    '
            End Select
        Catch ex As Exception
            res = NO_INFO_RETRIEVED
        End Try

        Return res
    End Function


    ' ========================================
    ' Private functions of this class
    ' ========================================
    Private Function GetInformationFromWMICollection(ByVal infoName As WMI_INFO) As Object
        Return _theModule.GetPropertyValue(infoName.ToString)
    End Function

End Class
