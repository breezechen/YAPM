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
''' Représente une entrée d'export
''' </summary>
''' <remarks></remarks>
Public Structure ExportEntry
    Dim Name As String
    Dim Hint As Integer
    Dim Ordinal As Integer
    Dim ExportRVA As Integer
End Structure
''' <summary>
''' Représente le dossier d'export complet
''' </summary>
''' <remarks></remarks>
Public Class ExportDirectory
    Public DllName As String
    Public ExportFlags As Integer
    Public TimeDateStamp As DateTime
    Public MajorVersion As Short
    Public MinorVersion As Short
    Public NameRVA As Integer
    Public OrdinalBase As Integer
    Public AddressTableEntries As Integer
    Public NumberofNamePointers As Integer
    Public ExportAddressTableRVA As Integer
    Public NamePointerRVA As Integer
    Public OrdinalTableRVA As Integer
    ''' <summary>
    ''' Fonctions exportées
    ''' </summary>
    ''' <remarks></remarks>
    Public ExportEntries As New List(Of ExportEntry)

    ''' <summary>
    ''' Représente les données brutes
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure RawExportDirectory
        Public ExportFlags As Integer
        Public TimeDateStamp As Integer
        Public MajorVersion As Short
        Public MinorVersion As Short
        Public NameRVA As Integer
        Public OrdinalBase As Integer
        Public AddressTableEntries As Integer
        Public NumberofNamePointers As Integer
        Public ExportAddressTableRVA As Integer
        Public NamePointerRVA As Integer
        Public OrdinalTableRVA As Integer
    End Structure

    ''' <summary>
    ''' Récupère les informations sur les exports
    ''' </summary>
    ''' <param name="pe">fichier PE</param>
    ''' <param name="PEReader">données binaire du fichier PE</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pe As PEFile, ByVal PEReader As System.IO.BinaryReader)
        'si le fichier contient un dossier Export
        If pe.ExportTable.RVA <> 0 Then
            'on récupère les infos
            PEReader.BaseStream.Position = pe.RVA2Offset(pe.ExportTable.RVA)
            Me.ExportFlags = PEReader.ReadInt32()
            Me.TimeDateStamp = PEFile.Time_T2DateTime(PEReader.ReadUInt32())
            Me.MajorVersion = PEReader.ReadInt16()
            Me.MinorVersion = PEReader.ReadInt16()
            Me.NameRVA = PEReader.ReadInt32()
            Me.OrdinalBase = PEReader.ReadInt32()
            Me.AddressTableEntries = PEReader.ReadInt32()
            Me.NumberofNamePointers = PEReader.ReadInt32()
            Me.ExportAddressTableRVA = PEReader.ReadInt32()
            Me.NamePointerRVA = PEReader.ReadInt32()
            Me.OrdinalTableRVA = PEReader.ReadInt32()

            'la table des adresses
            PEReader.BaseStream.Position = pe.RVA2Offset(Me.ExportAddressTableRVA)
            Dim exportAddressTable As New List(Of Integer)
            For i As Integer = 0 To Me.AddressTableEntries - 1
                exportAddressTable.Add(PEReader.ReadInt32())
            Next
            'la table des pointeurs de noms
            PEReader.BaseStream.Position = pe.RVA2Offset(Me.NamePointerRVA)
            Dim exportNameTable As New List(Of Integer)
            For i As Integer = 0 To Me.NumberofNamePointers - 1
                exportNameTable.Add(PEReader.ReadInt32())
            Next
            'la table des numéros d'ordre (ordinal)
            PEReader.BaseStream.Position = pe.RVA2Offset(Me.OrdinalTableRVA)
            Dim exportOrdinalTable As New List(Of Short)
            For i As Integer = 0 To Me.NumberofNamePointers - 1
                exportOrdinalTable.Add(PEReader.ReadInt16())
            Next

            Dim c As Byte

            'pour chaque export
            For i As Integer = 0 To Me.NumberofNamePointers - 1
                Dim entry As New ExportEntry
                'on récupère le nom
                PEReader.BaseStream.Position = pe.RVA2Offset(exportNameTable(i))
                Dim sbEntryName As New System.Text.StringBuilder()
                Do
                    c = PEReader.ReadByte()
                    If c <> 0 Then sbEntryName.Append(Chr(c))
                Loop While c <> 0
                entry.Name = sbEntryName.ToString()
                'le numéro d'ordre
                entry.Ordinal = exportOrdinalTable(i) + Me.OrdinalBase
                'l'adresse
                entry.ExportRVA = exportAddressTable(entry.Ordinal - Me.OrdinalBase)
                entry.Hint = i
                Me.ExportEntries.Add(entry)
            Next

            'le nom de la dll
            PEReader.BaseStream.Position = pe.RVA2Offset(Me.NameRVA)
            Dim sbDllName As New System.Text.StringBuilder()
            Do
                c = PEReader.ReadByte()
                If c <> 0 Then sbDllName.Append(Chr(c))
            Loop While c <> 0
            Me.DllName = sbDllName.ToString()
        End If
    End Sub
End Class
