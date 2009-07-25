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
''' Entête optionnel d'un fichier PE
''' </summary>
''' <remarks></remarks>
Public Class OptionalHeader
    'version du linker
    Public MajorLinkerVersion As Byte
    Public MinorLinkerVersion As Byte
    'taille du code et des données
    Public SizeOfCode As Integer
    Public SizeOfInitializedData As Integer
    Public SizeOfUninitializedData As Integer
    'adresse du point d'entrée
    Public AddressOfEntryPoint As Integer
    'adresse du code et des données
    Public BaseOfCode As Integer
    Public BaseOfData As Integer

    Friend Sub New(ByVal PEReader As System.IO.BinaryReader)
        Me.MajorLinkerVersion = PEReader.ReadByte()
        Me.MinorLinkerVersion = PEReader.ReadByte()
        Me.SizeOfCode = PEReader.ReadInt32()
        Me.SizeOfInitializedData = PEReader.ReadInt32()
        Me.SizeOfUninitializedData = PEReader.ReadInt32()
        Me.AddressOfEntryPoint = PEReader.ReadInt32()
        Me.BaseOfCode = PEReader.ReadInt32()
        Me.BaseOfData = PEReader.ReadInt32()
    End Sub
End Class
