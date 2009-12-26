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

Public Class cHeap
    Inherits cGeneralObject

    Private _heapInfos As heapInfos
    Private Shared WithEvents _connection As cHeapConnection


#Region "Properties"

    Public Shared Property Connection() As cHeapConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cHeapConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As heapInfos)
        _heapInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Heap
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As heapInfos
        Get
            Return _heapInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As heapInfos)
        _heapInfos.Merge(Thr)
    End Sub

#Region "All actions on heaps"

    '

#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return heapInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function


    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
            Case "MemCommitted"
                res = Misc.GetFormatedSize(Me.Infos.MemCommitted.ToInt64)
            Case "MemAllocated"
                res = Misc.GetFormatedSize(Me.Infos.MemAllocated.ToInt64)
            Case "BlockCount"
                res = Me.Infos.BlockCount.ToString
            Case "Flags"
                res = "0x" & Me.Infos.Flags.ToString("x")
            Case "Granularity"
                res = Me.Infos.Granularity.ToString
            Case "TagCount"
                res = Me.Infos.TagCount.ToString
            Case "Tags"
                res = Me.Infos.Tags.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_MemCommitted As String = ""
        Static _old_MemAllocated As String = ""
        Static _old_BlockCount As String = ""
        Static _old_Flags As String = ""
        Static _old_Granularity As String = ""
        Static _old_TagCount As String = ""
        Static _old_Tags As String = ""
        Static _old_Address As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
                If res = _old_Address Then
                    hasChanged = False
                Else
                    _old_Address = res
                End If
            Case "MemCommitted"
                res = Misc.GetFormatedSize(Me.Infos.MemCommitted.ToInt64)
                If res = _old_MemCommitted Then
                    hasChanged = False
                Else
                    _old_MemCommitted = res
                End If
            Case "MemAllocated"
                res = Misc.GetFormatedSize(Me.Infos.MemAllocated.ToInt64)
                If res = _old_MemAllocated Then
                    hasChanged = False
                Else
                    _old_MemAllocated = res
                End If
            Case "BlockCount"
                res = Me.Infos.BlockCount.ToString
                If res = _old_BlockCount Then
                    hasChanged = False
                Else
                    _old_BlockCount = res
                End If
            Case "Flags"
                res = "0x" & Me.Infos.Flags.ToString("x")
                If res = _old_Flags Then
                    hasChanged = False
                Else
                    _old_Flags = res
                End If
            Case "Granularity"
                res = Me.Infos.Granularity.ToString
                If res = _old_Granularity Then
                    hasChanged = False
                Else
                    _old_Granularity = res
                End If
            Case "TagCount"
                res = Me.Infos.TagCount.ToString
                If res = _old_TagCount Then
                    hasChanged = False
                Else
                    _old_TagCount = res
                End If
            Case "Tags"
                res = Me.Infos.Tags.ToString
                If res = _old_Tags Then
                    hasChanged = False
                Else
                    _old_Tags = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

#Region "Shared function"

    '

#End Region

#Region "Shared functions (local)"

    ' 

#End Region


End Class
