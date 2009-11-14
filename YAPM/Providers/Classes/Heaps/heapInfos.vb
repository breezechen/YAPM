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

Imports System.Runtime.InteropServices

<Serializable()> Public Class heapInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _baseAddress As IntPtr
    Private _blockCount As Integer
    Private _flags As Integer
    Private _granularity As Integer
    Private _memAllocated As IntPtr
    Private _memCommitted As IntPtr
    Private _tagCount As Integer
    Private _tags As IntPtr

#End Region

#Region "Read only properties"

    Public ReadOnly Property BaseAddress() As IntPtr
        Get
            Return _baseAddress
        End Get
    End Property
    Public ReadOnly Property BlockCount() As Integer
        Get
            Return _blockCount
        End Get
    End Property
    Public ReadOnly Property Flags() As Integer
        Get
            Return _flags
        End Get
    End Property
    Public ReadOnly Property Granularity() As Integer
        Get
            Return _granularity
        End Get
    End Property
    Public ReadOnly Property MemAllocated() As IntPtr
        Get
            Return _memAllocated
        End Get
    End Property
    Public ReadOnly Property MemCommitted() As IntPtr
        Get
            Return _memCommitted
        End Get
    End Property
    Public ReadOnly Property TagCount() As Integer
        Get
            Return _tagCount
        End Get
    End Property
    Public ReadOnly Property Tags() As IntPtr
        Get
            Return _tags
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef data As Native.Api.NativeStructs.HeapInformation)
        With data
            _baseAddress = .BaseAddress
            _blockCount = .BlockCount
            _flags = .Flags
            _granularity = .Granularity
            _memAllocated = .MemAllocated
            _memCommitted = .MemCommitted
            _tagCount = .TagCount
            _tags = .Tags
        End With
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As heapInfos)
        With newI
            _blockCount = .BlockCount
            _flags = .Flags
            _granularity = .Granularity
            _memAllocated = .MemAllocated
            _memCommitted = .MemCommitted
            _tagCount = .TagCount
            _tags = .Tags
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(6) As String

        s(0) = "MemCommitted"
        s(1) = "MemAllocated"
        s(2) = "BlockCount"
        s(3) = "Flags"
        s(4) = "Granularity"
        s(5) = "TagCount"
        s(6) = "Tags"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Address"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
