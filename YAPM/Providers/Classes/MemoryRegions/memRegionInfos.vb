﻿' =======================================================
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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices

<Serializable()> Public Class memRegionInfos
    Inherits generalInfos


    Public Shared Operator <>(ByVal m1 As memRegionInfos, ByVal m2 As memRegionInfos) As Boolean
        Return Not (m1 = m2)
    End Operator
    Public Shared Operator =(ByVal m1 As memRegionInfos, ByVal m2 As memRegionInfos) As Boolean
        Return (m1.BaseAddress = m2.BaseAddress AndAlso _
        m1.RegionSize = m2.RegionSize AndAlso _
        m1.State = m2.State AndAlso _
        m1.Name = m2.Name AndAlso _
        m1.Type = m2.Type AndAlso _
        m1.Protection = m2.Protection AndAlso _
        m1.ProcessId = m2.ProcessId)
    End Operator


#Region "Private attributes"

    Private _procId As Integer
    Private _state As API.MEMORY_STATE
    Private _size As Integer
    Private _address As Integer
    Private _protection As API.PROTECTION_TYPE
    Private _type As API.MEMORY_TYPE

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _procId
        End Get
    End Property
    Public ReadOnly Property RegionSize() As Integer
        Get
            Return _size
        End Get
    End Property
    Public ReadOnly Property State() As API.MEMORY_STATE
        Get
            Return _state
        End Get
    End Property
    Public ReadOnly Property BaseAddress() As Integer
        Get
            Return _address
        End Get
    End Property
    Public ReadOnly Property Protection() As API.PROTECTION_TYPE
        Get
            Return _protection
        End Get
    End Property
    Public ReadOnly Property Type() As API.MEMORY_TYPE
        Get
            Return _type
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return getName()
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef mbi As API.MEMORY_BASIC_INFORMATION, ByVal pid As Integer)

        _procId = pid
        With mbi
            _state = .State
            _size = .RegionSize
            _address = .BaseAddress
            _protection = .AllocationProtect
            _type = .lType
        End With

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As memRegionInfos)
        With newI
            _protection = .Protection
            _size = .RegionSize
            _state = .State
            _type = .Type
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
        Dim s(5) As String

        s(0) = "Type"
        s(1) = "Protection"
        s(2) = "State"
        s(3) = "Address"
        s(4) = "Size"
        s(5) = "File"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        Return s
    End Function

    Private Function getName() As String
        If _state = API.MEMORY_STATE.Free Then
            Return _state.ToString
        ElseIf _type = API.MEMORY_TYPE.Image Then
            Return _type.ToString
        Else
            Return _type.ToString & " (" & _state.ToString & ")"
        End If
    End Function

End Class
