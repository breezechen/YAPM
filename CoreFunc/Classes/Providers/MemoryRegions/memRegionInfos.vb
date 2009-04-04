' =======================================================
' Yet Another Process Monitor (YAPM)
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

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices

Public Class memRegionInfos

    Private Const NO_INFO_RETRIEVED As String = "N/A"

#Region "Private attributes"

    Private _procId As Integer
    Private _state As API.MEMORY_STATE
    Private _size As Integer
    Private _address As Integer
    Private _protection As API.PROTECTION_TYPE
    Private _type As API.MEMORY_TYPE
    Private _name As String

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
            If _name = vbNullString Then
                _name = getName()
            End If
            Return _name
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
            _state = CType(.State, API.MEMORY_STATE)
            _size = .RegionSize
            _address = .BaseAddress
            _protection = CType(.AllocationProtect, API.PROTECTION_TYPE)
            _type = CType(.lType, API.MEMORY_TYPE)
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
    Public Shared Function GetAvailableProperties() As String()
        Dim s(4) As String

        s(0) = "Type"
        s(1) = "Protection"
        s(2) = "State"
        s(3) = "Address"
        s(4) = "Size"

        Return s
    End Function

    Private Function getName() As String
        If _state = API.MEMORY_STATE.MEM_FREE Then
            Return "Free"
        ElseIf _Type = API.MEMORY_TYPE.MEM_IMAGE Then
            ' TOCHANGE
            ' Should return MODULE name
            Return "Image"
        Else
            Return _Type.ToString & " (" & _state.ToString & ")"
        End If
    End Function

End Class
