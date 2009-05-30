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

'caractéristique d'une section
Public Enum SectionCharacteristics
    IMAGE_SCN_TYPE_REG = &H0S
    IMAGE_SCN_TYPE_DSECT = &H1S
    IMAGE_SCN_TYPE_NOLOAD = &H2S
    IMAGE_SCN_TYPE_GROUP = &H4S
    IMAGE_SCN_TYPE_NO_PAD = &H8S
    IMAGE_SCN_TYPE_COPY = &H10S
    IMAGE_SCN_CNT_CODE = &H20S
    IMAGE_SCN_CNT_INITIALIZED_DATA = &H40S
    IMAGE_SCN_CNT_UNINITIALIZED_DATA = &H80S
    IMAGE_SCN_LNK_OTHER = &H100S
    IMAGE_SCN_LNK_INFO = &H200S
    IMAGE_SCN_TYPE_OVER = &H400S
    IMAGE_SCN_LNK_REMOVE = &H800S
    IMAGE_SCN_LNK_COMDAT = &H1000S
    IMAGE_SCN_MEM_FARDATA = &H8000S
    IMAGE_SCN_MEM_PURGEABLE = &H20000
    IMAGE_SCN_MEM_16BIT = &H20000
    IMAGE_SCN_MEM_LOCKED = &H40000
    IMAGE_SCN_MEM_PRELOAD = &H80000
    IMAGE_SCN_ALIGN_1BYTES = &H100000
    IMAGE_SCN_ALIGN_2BYTES = &H200000
    IMAGE_SCN_ALIGN_4BYTES = &H300000
    IMAGE_SCN_ALIGN_8BYTES = &H400000
    IMAGE_SCN_ALIGN_16BYTES = &H500000
    IMAGE_SCN_ALIGN_32BYTES = &H600000
    IMAGE_SCN_ALIGN_64BYTES = &H700000
    IMAGE_SCN_ALIGN_128BYTES = &H800000
    IMAGE_SCN_ALIGN_256BYTES = &H900000
    IMAGE_SCN_ALIGN_512BYTES = &HA00000
    IMAGE_SCN_ALIGN_1024BYTES = &HB00000
    IMAGE_SCN_ALIGN_2048BYTES = &HC00000
    IMAGE_SCN_ALIGN_4096BYTES = &HD00000
    IMAGE_SCN_ALIGN_8192BYTES = &HE00000
    IMAGE_SCN_LNK_NRELOC_OVFL = &H1000000
    IMAGE_SCN_MEM_DISCARDABLE = &H2000000
    IMAGE_SCN_MEM_NOT_CACHED = &H4000000
    IMAGE_SCN_MEM_NOT_PAGED = &H8000000
    IMAGE_SCN_MEM_SHARED = &H10000000
    IMAGE_SCN_MEM_EXECUTE = &H20000000
    IMAGE_SCN_MEM_READ = &H40000000
    IMAGE_SCN_MEM_WRITE = &H80000000
End Enum
Public Class Section
    'nom de la section
    Public Name As String
    'taille et adresse virtuelle de la section
    Public VirtualSize As Integer
    Public VirtualAddress As Integer
    'taille brute dans le fichier PE
    Public SizeOfRawData As Integer
    'pointeur vers les données
    Public PointerToRawData As Integer
    Public PointerToRelocations As Integer
    Public PointerToLinenumbers As Integer
    Public NumberOfRelocations As Short
    Public NumberOfLinenumbers As Short
    Public Characteristics As SectionCharacteristics

    Friend Sub New(ByVal PEReader As System.IO.BinaryReader)
        Me.Name = System.Text.Encoding.ASCII.GetString(PEReader.ReadBytes(8))
        Me.VirtualSize = PEReader.ReadInt32()
        Me.VirtualAddress = PEReader.ReadInt32()
        Me.SizeOfRawData = PEReader.ReadInt32()
        Me.PointerToRawData = PEReader.ReadInt32()
        Me.PointerToRelocations = PEReader.ReadInt32()
        Me.PointerToLinenumbers = PEReader.ReadInt32()
        Me.NumberOfRelocations = PEReader.ReadInt16()
        Me.NumberOfLinenumbers = PEReader.ReadInt16()
        Me.Characteristics = CType(PEReader.ReadInt32(), SectionCharacteristics)
    End Sub

    ''renvoie le nom long de la section
    'Private Function GetSectionLongName(ByVal Name As String, ByRef StringTable() As String) As String
    '    'si le nom commence par u "\"
    '    If InStr(Name, "\") Then
    '        'c'est un nom long
    '        Name = Mid(Name, 2)
    '        'on le récupère dans la table des chaines
    '        GetSectionLongName = StringTable(Val(Name))
    '    Else
    '        'sinon c'est un nom court
    '        GetSectionLongName = Name
    '    End If
    'End Function
End Class
