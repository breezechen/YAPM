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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices

<Serializable()> Public Class handleInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _handle As Integer
    Private _type As String
    Private _pid As Integer
    Private _name As String
    Private _handleCount As Integer
    Private _pointerCount As Integer
    Private _objectCount As Integer

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessID() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property Handle() As Integer
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property Type() As String
        Get
            Return _type
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property HandleCount() As Integer
        Get
            Return _handleCount
        End Get
    End Property
    Public ReadOnly Property PointerCount() As Integer
        Get
            Return _pointerCount
        End Get
    End Property
    Public ReadOnly Property ObjectCount() As Integer
        Get
            Return _objectCount
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByVal handle As Integer, ByVal type As String, ByVal pid As Integer, _
        ByVal name As String, ByVal handlecount As Integer, _
        ByVal pointercount As Integer, ByVal objectCount As Integer)

        _handle = handle
        _handleCount = handlecount
        _type = type
        _name = name
        _pid = pid
        _pointerCount = pointercount
        _objectCount = objectCount

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As handleInfos)
        With newI
            _handle = .Handle
            _handleCount = .HandleCount
            _name = .Name
            _objectCount = .ObjectCount
            _pointerCount = .PointerCount
            _pid = .ProcessID
            _type = .Type
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(5) As String

        s(0) = "Type"
        s(1) = "Name"
        s(2) = "HandleCount"
        s(3) = "PointerCount"
        s(4) = "ObjectCount"
        s(5) = "Process"

        Return s
    End Function

End Class
