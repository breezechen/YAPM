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

''' <summary>
''' Type de sous système NT attendu par l'exécutable
''' </summary>
''' <remarks></remarks>
Public Enum NTSubSystem
    IMAGE_SUBSYSTEM_UNKNOWN = 0
    IMAGE_SUBSYSTEM_NATIVE = 1
    IMAGE_SUBSYSTEM_WINDOWS_GUI = 2
    IMAGE_SUBSYSTEM_WINDOWS_CUI = 3
    IMAGE_SUBSYSTEM_POSIX_CUI = 7
    IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9
    IMAGE_SUBSYSTEM_EFI_APPLICATION = 10
    IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11
    IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12
End Enum

''' <summary>
''' Caractéristique d'un exécutable
''' </summary>
''' <remarks></remarks>
Public Enum DLLCharacteristic
    IMAGE_DLLCHARACTERISTICS_NO_BIND = &H800S
    IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = &H2000S
    IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = &H8000S
End Enum

''' <summary>
''' Entête du sous sytème NT
''' </summary>
''' <remarks></remarks>
Public Class NTHeader
    'base des adresses virtuelles relatives
    Public ImageBase As Integer
    'alignements
    Public SectionAlignment As Integer
    Public FileAlignment As Integer
    'système d'exploitation cible
    Public MajorOperatingSystemVersion As Short
    Public MinorOperatingSystemVersion As Short
    'version de l'image de l'exécutable
    Public MajorImageVersion As Short
    Public MinorImageVersion As Short
    'version du sous système cible
    Public MajorSubsystemVersion As Short
    Public MinorSubsystemVersion As Short
    'taille de l'image et des entêtes
    Public SizeOfImage As Integer
    Public SizeOfHeaders As Integer
    'checksum
    Public CheckSum As Integer
    'sous système
    Public Subsystem As NTSubSystem
    'caractéristiques
    Public DLLCharacteristics As DLLCharacteristic
    'taille des piles et tas
    Public SizeOfStackReserve As Integer
    Public SizeOfStackCommit As Integer
    Public SizeOfHeapReserve As Integer
    Public SizeOfHeapCommit As Integer
    'flags
    Public LoaderFlags As Integer
    Public NumberOfRvaAndSizes As Integer

    Friend Sub New(ByVal PEReader As System.IO.BinaryReader)
        Me.ImageBase = PEReader.ReadInt32()
        Me.SectionAlignment = PEReader.ReadInt32()
        Me.FileAlignment = PEReader.ReadInt32()
        Me.MajorOperatingSystemVersion = PEReader.ReadInt16()
        Me.MinorOperatingSystemVersion = PEReader.ReadInt16()
        Me.MajorImageVersion = PEReader.ReadInt16()
        Me.MinorImageVersion = PEReader.ReadInt16()
        Me.MajorSubsystemVersion = PEReader.ReadInt16()
        Me.MinorSubsystemVersion = PEReader.ReadInt16()
        PEReader.BaseStream.Position += 4
        Me.SizeOfImage = PEReader.ReadInt32()
        Me.SizeOfHeaders = PEReader.ReadInt32()
        Me.CheckSum = PEReader.ReadInt32()
        Me.Subsystem = CType(PEReader.ReadInt16(), NTSubSystem)
        Me.DLLCharacteristics = CType(PEReader.ReadInt16(), DLLCharacteristic)
        Me.SizeOfStackReserve = PEReader.ReadInt32()
        Me.SizeOfStackCommit = PEReader.ReadInt32()
        Me.SizeOfHeapReserve = PEReader.ReadInt32()
        Me.SizeOfHeapCommit = PEReader.ReadInt32()
        Me.LoaderFlags = PEReader.ReadInt32()
        Me.NumberOfRvaAndSizes = PEReader.ReadInt32()
    End Sub
End Class
