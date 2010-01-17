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


    Public Enum CreatedOrDeleted As Byte
        Created
        Deleted
    End Enum

#Region "Private attributes"

    Private _dateTime As Date
    Private _pid As Integer
    Private _type As String
    Private _description As String
    Private _typeMask As Native.Api.Enums.LogItemType
    Private _defKey As String
    Private _state As CreatedOrDeleted
    Private _key As String

#End Region

#Region "Read only properties"

    Public Property State() As CreatedOrDeleted
        Get
            Return _state
        End Get
        Friend Set(ByVal value As CreatedOrDeleted)
            _state = value
        End Set
    End Property
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
    Public ReadOnly Property TypeMask() As Native.Api.Enums.LogItemType
        Get
            Return _typeMask
        End Get
    End Property
    Public ReadOnly Property DefKey() As String
        Get
            Return _defKey
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructors of this class
    Public Sub New(ByVal item As networkInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Network connection"
        _description = "Network connection " & ", protocol = " & item.Protocol.ToString
        If item.Local IsNot Nothing Then
            _description &= ", local = " & item.Local.ToString
        End If
        If item.Remote IsNot Nothing Then
            _description &= ", remote = " & item.Remote.ToString
        End If
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.NetworkItem
        _defKey = item.Key
        _pid = item.ProcessId
    End Sub
    Public Sub New(ByVal item As handleInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Handle"
        _description = "Handle " & ", handle = " & item.Handle.ToString & ", type = " & item.Type & ", name = " & item.Name
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.HandleItem
        _defKey = item.Key
        _pid = item.ProcessId
    End Sub
    Public Sub New(ByVal item As memRegionInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Memory region"
        _description = "Memory region " & ", address = " & item.BaseAddress.ToString & ", size = " & GetFormatedSize(item.RegionSize) & ", name = " & item.Name
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.MemoryItem
        _defKey = item.Key
        _pid = item.ProcessId
    End Sub
    Public Sub New(ByVal item As moduleInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Module"
        _description = "Module " & ", address = " & item.BaseAddress.ToString & ", path = " & item.Path
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.ModuleItem
        _defKey = item.Key
        _pid = item.ProcessId
    End Sub
    Public Sub New(ByVal item As serviceInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Service"
        _description = "Service " & ", name = " & item.Name & ", path = " & item.ImagePath & ", start type = " & item.StartType.ToString
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.ServiceItem
        _defKey = item.Key
        _pid = item.ProcessId
    End Sub
    Public Sub New(ByVal item As threadInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Thread"
        _description = "Thread " & ", id = " & item.Id.ToString & ", priority = " & item.Priority.ToString & ", address = " & item.StartAddress.ToString
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.ThreadItem
        _defKey = item.Key
        _pid = item.ProcessId
    End Sub
    Public Sub New(ByVal item As windowInfos, ByVal state As CreatedOrDeleted)
        _state = state
        _type = "Window"
        _description = "Window " & ", id = " & item.Handle.ToString & ", thread id = " & item.ThreadId.ToString & ", caption = " & item.Caption
        _dateTime = Date.Now
        _key = _type & "|" & item.Key & "|" & CStr(state)
        _typeMask = Native.Api.Enums.LogItemType.WindowItem
        _defKey = item.Key
        _pid = item.ProcessId
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
