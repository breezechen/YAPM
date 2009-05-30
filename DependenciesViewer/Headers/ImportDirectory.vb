''' <summary>
''' Réprésente une entrée d'import
''' </summary>
''' <remarks></remarks>
Public Structure ImportEntry
    Public Name As String
    Public Ordinal As Integer
    Public Address As Integer
    Public Hint As Short
End Structure

''' <summary>
''' Réprésente une dll importée
''' </summary>
''' <remarks></remarks>
Public Structure DllImportEntry
    Public DllName As String
    Public Entries As List(Of ImportEntry)
End Structure

''' <summary>
''' Réprésente le dossier d'import
''' </summary>
''' <remarks></remarks>
Public Class ImportDirectory
    ''' <summary>
    ''' Liste des dlls
    ''' </summary>
    ''' <remarks></remarks>
    Public DllEntries As New List(Of DllImportEntry)

    ''' <summary>
    ''' Réprésente une entrée de dossier d'import
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure RawImportDirectoryEntry
        Public ImportLookupTableRVA As Integer
        Public TimeDateStamp As Integer
        Public FowarderChain As Integer
        Public NameRVA As Integer
        Public ImportAddressTableRVA As Integer
    End Structure

    ''' <summary>
    ''' Récupère les informations sur les imports
    ''' </summary>
    ''' <param name="pe">fichier PE</param>
    ''' <param name="PEReader">données binaire du fichier PE</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pe As PEFile, ByVal PEReader As System.IO.BinaryReader)
        's'il y a un dossier Import
        If pe.ImportTable.RVA <> 0 Then
            Dim indexImport As Integer = 0
            'tant que l'on a une dll importée
            Do
                'entrée d'import suivante
                PEReader.BaseStream.Position = pe.RVA2Offset(pe.ImportTable.RVA) + indexImport * 20

                'on récupère les infos sur chaque Dll importée
                Dim importLookupTableRVA As Integer = PEReader.ReadInt32()
                PEReader.BaseStream.Position += 4
                Dim forwarderChain As Integer = PEReader.ReadInt32()
                Dim nameRVA As Integer = PEReader.ReadInt32()
                Dim importAddressTableRVA As Integer = PEReader.ReadInt32()

                'si on est à la fin de la table des imports
                If (forwarderChain = 0) AndAlso (importAddressTableRVA = 0) AndAlso (importLookupTableRVA = 0) AndAlso (nameRVA = 0) Then Exit Do

                Dim DllImport As New DllImportEntry

                'le nom de dll
                PEReader.BaseStream.Position = pe.RVA2Offset(nameRVA)
                Dim c As Byte, sbDllName As New System.Text.StringBuilder()
                Do
                    c = PEReader.ReadByte()
                    If c <> 0 Then sbDllName.Append(Chr(c))
                Loop While c <> 0

                DllImport.DllName = sbDllName.ToString()
                DllImport.Entries = New List(Of ImportEntry)

                Dim importLookupTable As New List(Of Integer)
                'la table de nom
                PEReader.BaseStream.Position = pe.RVA2Offset(importLookupTableRVA)
                Do
                    Dim lookupEntryRVA As Integer = PEReader.ReadInt32()
                    If lookupEntryRVA = 0 Then Exit Do
                    importLookupTable.Add(lookupEntryRVA)
                Loop
                Dim importAddressTable As New List(Of Integer)
                'la table d'adresse
                PEReader.BaseStream.Position = pe.RVA2Offset(importAddressTableRVA)
                Do
                    Dim addressEntryRVA As Integer = PEReader.ReadInt32()
                    If addressEntryRVA = 0 Then Exit Do
                    importAddressTable.Add(addressEntryRVA)
                Loop

                'pour chaque fonction importée dans la dll
                For i As Integer = 0 To importAddressTable.Count - 1
                    Dim lookupEntry As Integer = importLookupTable(i)
                    Dim addressEntry As Integer = importAddressTable(i)

                    Dim importEntry As New ImportEntry
                    'si l'import est par ordinal
                    If (lookupEntry And &H80000000) = &H80000000 Then
                        importEntry.Ordinal = (lookupEntry And &H7FFFFFFF)
                        importEntry.Name = vbNullString
                        importEntry.Hint = CShort(i)
                        'sinon par nom
                    Else
                        Dim importNameAddress As Integer = lookupEntry And &H7FFFFFFF
                        PEReader.BaseStream.Position = pe.RVA2Offset(importNameAddress)
                        importEntry.Hint = PEReader.ReadInt16()
                        Dim sbImportName As New System.Text.StringBuilder()
                        Do
                            c = PEReader.ReadByte()
                            If c <> 0 Then sbImportName.Append(Chr(c))
                        Loop While c <> 0
                        importEntry.Name = sbImportName.ToString()
                        importEntry.Ordinal = 0
                        'importEntry.Address = pe.RVA2Offset(importNameAddress)
                    End If
                    'et l'adresse dans tous les cas
                    importEntry.Address = pe.RVA2Offset(addressEntry)
                    DllImport.Entries.Add(importEntry)
                Next
                Me.DllEntries.Add(DllImport)
                'entrée suivante
                indexImport += 1
            Loop
        End If
    End Sub
End Class
