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



' Thanks A LOT to ShareVB for the Dependencies viewer.
' http://www.vbfrance.com/codes/UNMANAGED-DEPENDENCY-VIEWER-LISTE-FONCTIONS-IMPORTEES-IAT-EXPORTEES_47594.aspx

Option Strict On

Imports System.Collections.Generic

''' <summary>
''' type de machine cible de l'image exécutable PE
''' </summary>
''' <remarks></remarks>
Public Enum Machine
    IMAGE_FILE_MACHINE_UNKNOWN = &H0S
    IMAGE_FILE_MACHINE_ALPHA = &H184S
    IMAGE_FILE_MACHINE_ARM = &H1C0S
    IMAGE_FILE_MACHINE_ALPHA64 = &H284S
    IMAGE_FILE_MACHINE_I386 = &H14CS
    IMAGE_FILE_MACHINE_IA64 = &H200S
    IMAGE_FILE_MACHINE_M68K = &H268S
    IMAGE_FILE_MACHINE_MIPS16 = &H266S
    IMAGE_FILE_MACHINE_MIPSFPU = &H366S
    IMAGE_FILE_MACHINE_MIPSFPU16 = &H466S
    IMAGE_FILE_MACHINE_POWERPC = &H1F0S
    IMAGE_FILE_MACHINE_R3000 = &H162S
    IMAGE_FILE_MACHINE_R4000 = &H166S
    IMAGE_FILE_MACHINE_R10000 = &H168S
    IMAGE_FILE_MACHINE_SH3 = &H1A2S
    IMAGE_FILE_MACHINE_SH4 = &H1A6S
    IMAGE_FILE_MACHINE_THUMB = &H1C2S
End Enum

''' <summary>
''' caractéristique de l'image exécutable PE
''' </summary>
''' <remarks></remarks>
Public Enum Characteristic
    IMAGE_FILE_RELOCS_STRIPPED = &H1S
    IMAGE_FILE_EXECUTABLE_IMAGE = &H2S
    IMAGE_FILE_LINE_NUMS_STRIPPED = &H4S
    IMAGE_FILE_LOCAL_SYMS_STRIPPED = &H8S
    IMAGE_FILE_AGGRESSIVE_WS_TRIM = &H10S
    IMAGE_FILE_LARGE_ADDRESS_AWARE = &H20S
    IMAGE_FILE_16BIT_MACHINE = &H40S
    IMAGE_FILE_BYTES_REVERSED_LO = &H80S
    IMAGE_FILE_32BIT_MACHINE = &H100S
    IMAGE_FILE_DEBUG_STRIPPED = &H200S
    IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = &H400S
    IMAGE_FILE_SYSTEM = &H1000S
    IMAGE_FILE_DLL = &H2000S
    IMAGE_FILE_UP_SYSTEM_ONLY = &H4000S
    IMAGE_FILE_BYTES_REVERSED_HI = &H8000S
End Enum

Public Class PEFile
    'contenu binaire du fichier PE
    Private PEReader As System.IO.BinaryReader
    'déplacement dans le fichier PE
    Private Sub Seek(ByVal dwPosition As Integer)
        PEReader.BaseStream.Position = dwPosition
    End Sub
    Private Sub SeekRelative(ByVal dwOffset As Integer)
        PEReader.BaseStream.Position += dwOffset
    End Sub

    Private m_Machine As Machine
    ''' <summary>
    ''' Architecture cible du fichier PE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Machine() As Machine
        Get
            Return m_Machine
        End Get
    End Property

    Private m_TimeStamp As DateTime
    ''' <summary>
    ''' Date de création de ce fichier PE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TimeStamp() As DateTime
        Get
            Return m_TimeStamp
        End Get
    End Property

    Private m_Characteristics As Characteristic
    ''' <summary>
    ''' Caractéristiques de l'image exécutable PE
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Characteristics() As Characteristic
        Get
            Return m_Characteristics
        End Get
    End Property

    ''' <summary>
    ''' Structure d'une table contenant par exemple, les imports ou les exports
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure IMAGE_DATA_DIRECTORY
        Public RVA As Integer
        Public Size As Integer
    End Structure

    Private m_Sections As New List(Of Section)
    ''' <summary>
    ''' Liste des sections de l'exécutable (data, rsrc, code)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Sections() As List(Of Section)
        Get
            Return m_Sections
        End Get
    End Property

    Private Directories As New List(Of IMAGE_DATA_DIRECTORY)
    ''' <summary>
    ''' Table d'export
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExportTable() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(0)
        End Get
    End Property
    ''' <summary>
    ''' Table d'import contenant les noms des imports
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ImportTable() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(1)
        End Get
    End Property
    ''' <summary>
    ''' Table des resources
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResourceTable() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(2)
        End Get
    End Property
    ''' <summary>
    ''' Table des gestionnaire d'exception
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExceptionTable() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(3)
        End Get
    End Property
    ''' <summary>
    ''' Table de signature numérique
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CertificateTable() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(4)
        End Get
    End Property
    ''' <summary>
    ''' Table des adresses relatives des imports
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ImportAddressTable() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(12)
        End Get
    End Property
    ''' <summary>
    ''' Table des imports qui ne sont pas chargé tout de suite au chargement de l'exécutable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DelayImportDescriptor() As IMAGE_DATA_DIRECTORY
        Get
            Return Directories(13)
        End Get
    End Property

    ''' <summary>
    ''' Convertit un time_t (décompte en secondes depuis 01/01/1970 en DateTime
    ''' </summary>
    ''' <param name="time_t"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Time_T2DateTime(ByVal time_t As UInt32) As DateTime
        Dim win32FileTime As Long = 10000000L * time_t + 116444736000000000L
        Return DateTime.FromFileTimeUtc(win32FileTime)
    End Function

    ''' <summary>
    ''' Lit les entêtes et les sections
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadHeadersAndSections()
        'on récupère l'offset de l'entête PE à l'offset 0x3C
        Seek(&H3C)
        Seek(PEReader.ReadInt32())
        'on récupère l'entête PE
        SeekRelative(4)
        m_Machine = CType(PEReader.ReadInt16(), Machine)
        Dim NumberOfSections As Integer = PEReader.ReadInt16()
        m_TimeStamp = Time_T2DateTime(PEReader.ReadUInt32())

        SeekRelative(4 + 4 + 4)
        Dim MagicPE As Short = PEReader.ReadInt16()

        'si PE
        If MagicPE = &H10B Then
            'l'entête optionelle
            m_OptionalHeader = New OptionalHeader(PEReader)
            'l'entête NT
            m_NTHeader = New NTHeader(PEReader)
        Else
            'ne supporte pas PE+
            Throw New NotSupportedException()
        End If

        'les dossiers
        For i As Integer = 0 To Me.NTHeader.NumberOfRvaAndSizes - 1
            Dim ImageDir As New IMAGE_DATA_DIRECTORY
            ImageDir.RVA = PEReader.ReadInt32()
            ImageDir.Size = PEReader.ReadInt32()
            Me.Directories.Add(ImageDir)
        Next
        'les sections
        For i As Integer = 0 To NumberOfSections - 1
            Me.Sections.Add(New Section(PEReader))
        Next
    End Sub

    Private m_OptionalHeader As OptionalHeader
    ''' <summary>
    ''' Entête optionnelle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OptionalHeader() As OptionalHeader
        Get
            Return m_OptionalHeader
        End Get
    End Property
    Private m_NTHeader As NTHeader
    ''' <summary>
    ''' Entête NT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property NTHeader() As NTHeader
        Get
            Return m_NTHeader
        End Get
    End Property

    Private m_ExportDirectory As ExportDirectory
    ''' <summary>
    ''' Renvoie les exports de l'exécutable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ExportDirectory() As ExportDirectory
        Get
            Return m_ExportDirectory
        End Get
    End Property
    Private m_ImportDirectory As ImportDirectory
    ''' <summary>
    ''' Renvoie les imports de l'exécutable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ImportDirectory() As ImportDirectory
        Get
            Return m_ImportDirectory
        End Get
    End Property

    ''' <summary>
    ''' Récupère des informations sur un fichier exe, dll, ocx
    ''' </summary>
    ''' <param name="szFileName">nom du fichier à analyser</param>
    ''' <remarks></remarks>
    Private Sub Init(ByRef szFileName As String)
        'on ouvre le fichier en mode binaire pour lire
        PEReader = New System.IO.BinaryReader(New System.IO.FileStream(szFileName, IO.FileMode.Open, IO.FileAccess.Read))

        'on recopie les infos de l'entête PE, l'entête optionnelle PE et l'entête NT
        ReadHeadersAndSections()

        'les exports de l'exécutable
        Me.m_ExportDirectory = New ExportDirectory(Me, PEReader)

        'les imports de l'exécutable
        Me.m_ImportDirectory = New ImportDirectory(Me, PEReader)

        PEReader.Close()
    End Sub

    ''' <summary>
    ''' Convertit une adresse virtuelle relative en un offset depuis le début du fichier
    ''' </summary>
    ''' <param name="dwRVA">(Relative Virtual Address) adresse virtuelle relative à convertir</param>
    ''' <returns>Offset dans le fichier exécutable</returns>
    ''' <remarks></remarks>
    Public Function RVA2Offset(ByVal dwRVA As Integer) As Integer
        'pour chaque section
        For i As Integer = 0 To Me.Sections.Count - 1
            'si l'adresse virtuelle se trouve dans la plage des adresses virtuelles de la sections
            If (dwRVA >= Me.Sections(i).VirtualAddress) And (dwRVA < (Me.Sections(i).VirtualAddress + Me.Sections(i).VirtualSize)) Then
                'alors l'adresse virtuelle appartient à la section
                'l'offset est RVA - VA de base de la section + offset de la section
                '(+ 1 pour VB qui prend ses offsets à partir de 1)
                Return dwRVA - Me.Sections(i).VirtualAddress + Me.Sections(i).PointerToRawData
            End If
        Next
        Return 0
    End Function


    Private m_FileName As String
    ''' <summary>
    ''' Nom du fichier exécutable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName() As String
        Get
            Return m_FileName
        End Get
    End Property

    ''' <summary>
    ''' Récupère les informations du fichier exécutable szFileName
    ''' </summary>
    ''' <param name="szFileName">Nom du fichier à analyser</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal szFileName As String)
        Me.m_FileName = szFileName
        Me.Init(szFileName)
    End Sub
End Class
