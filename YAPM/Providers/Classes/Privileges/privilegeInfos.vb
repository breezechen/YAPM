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

<Serializable()> Public Class privilegeInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _procId As Integer
    Private _status As Native.Api.NativeEnums.SePrivilegeAttributes
    Private _description As String
    Private _name As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _procId
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return _description
        End Get
    End Property
    Public ReadOnly Property Status() As Native.Api.NativeEnums.SePrivilegeAttributes
        Get
            Return _status
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
    Public Sub New(ByRef name As String, ByVal pid As Integer, ByVal status As Native.Api.NativeEnums.SePrivilegeAttributes)

        _procId = pid
        _name = name
        _status = status
        _description = Native.Objects.Token.GetPrivilegeDescriptionByName(name)

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As privilegeInfos)

        With newI
            _status = newI.Status
            _description = newI.Description
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(1) As String

        s(0) = "Status"
        s(1) = "Description"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
