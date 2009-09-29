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
Imports Common.Misc

<Serializable()> Public Class logItemInfos
    Inherits generalInfos

    Public Enum CREATED_OR_DELETED As Byte
        created
        deleted
    End Enum

#Region "Private attributes"

    Private _dateTime As Date
    Private _type As String
    Private _description As String
    Private _key As String
    Private _state As CREATED_OR_DELETED
    Private _typeMask As asyncCallbackLogEnumerate.LogItemType
    Private _defKey As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property DateTime() As Date
        Get
            Return _dateTime
        End Get
    End Property
    Public ReadOnly Property Type() As String
        Get
            Return _type
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return _description
        End Get
    End Property
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public ReadOnly Property State() As CREATED_OR_DELETED
        Get
            Return _state
        End Get
    End Property
    Public ReadOnly Property TypeMask() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _typeMask
        End Get
    End Property
    Public ReadOnly Property DefKey() As String
        Get
            Return _defKey
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructors of this class
    Public Sub New(ByVal item As networkInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Network connection"
        _state = type
        _description = "Network connection " & type.ToString & ", protocol = " & item.Protocol.ToString
        If item.Local IsNot Nothing Then
            _description &= ", local = " & item.Local.ToString
        End If
        If item.Remote IsNot Nothing Then
            _description &= ", remote = " & item.Remote.ToString
        End If
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.NetworkItem
        _defKey = ""        ' TODO
    End Sub
    Public Sub New(ByVal item As handleInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Handle"
        _state = type
        _description = "Handle " & type.ToString & ", handle = " & item.Handle.ToString & ", type = " & item.Type & ", name = " & item.Name
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.HandleItem
        _defKey = item.Handle.ToString
    End Sub
    Public Sub New(ByVal item As memRegionInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Memory region"
        _state = type
        _description = "Memory region " & type.ToString & ", address = " & item.BaseAddress.ToString & ", size = " & GetFormatedSize(item.RegionSize) & ", name = " & item.Name
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.MemoryItem
        _defKey = item.BaseAddress.ToString
    End Sub
    Public Sub New(ByVal item As moduleInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Module"
        _state = type
        _description = "Module " & type.ToString & ", address = " & item.BaseAddress.ToString & ", path = " & item.Path
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.ModuleItem
        _defKey = item.BaseAddress.ToString
    End Sub
    Public Sub New(ByVal item As serviceInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Service"
        _state = type
        _description = "Service " & type.ToString & ", name = " & item.Name & ", path = " & item.ImagePath & ", start type = " & item.StartType.ToString
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.ServiceItem
        _defKey = item.Name
    End Sub
    Public Sub New(ByVal item As threadInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Thread"
        _state = type
        _description = "Thread " & type.ToString & ", id = " & item.Id.ToString & ", priority = " & item.Priority.ToString & ", address = " & item.StartAddress.ToString
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.ThreadItem
        _defKey = item.Id.ToString
    End Sub
    Public Sub New(ByVal item As windowInfos, ByVal type As CREATED_OR_DELETED)
        _type = "Window"
        _state = type
        _description = "Window " & type.ToString & ", id = " & item.Handle.ToString & ", thread id = " & item.ThreadId.ToString & ", caption = " & item.Caption
        _dateTime = Date.Now
        _key = _type & "|" & _description & "|" & _dateTime.Ticks.ToString & type.ToString.Substring(1, 1)
        _typeMask = asyncCallbackLogEnumerate.LogItemType.WindowItem
        _defKey = item.Handle.ToString
    End Sub



    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As logItemInfos)

        With newI
            _type = .Type
            _description = .Description
            _dateTime = .DateTime
            _key = .Key
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(1) As String

        s(0) = "Type"
        s(1) = "Description"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Date && Time"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
