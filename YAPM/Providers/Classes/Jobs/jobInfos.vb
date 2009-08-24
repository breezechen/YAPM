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
' - Declaration of some structures used by NtQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Net

<Serializable()> Public Class jobInfos
    Inherits generalInfos

    ' Handle (query access)
    ' This handle is closed/reopened every time we refresh job list
    ' So we won't interfer with jobs with JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE limit
    Private _handleQuery As IntPtr

    ' True if _handleQuery has not been duplicated but is owned by YAPM
    Private _isHandleOwned As Boolean

#Region "Private attributes"

    Private _name As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property IsHandleOwned() As Boolean
        Get
            Return _isHandleOwned
        End Get
    End Property
    Public ReadOnly Property HandleQuery() As IntPtr
        Get
            Return _handleQuery
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByVal name As String, ByVal handleQuery As IntPtr, _
                   ByVal doWeOwnHandle As Boolean)
        _name = name
        _handleQuery = handleQuery
        _isHandleOwned = doWeOwnHandle
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As jobInfos)
        With newI
            _name = .Name
            _handleQuery = .HandleQuery   ' Merge handle ! Cause it may has been closed/reopened if it is a duplicated one
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(0) As String

        s(0) = "ProcessesCount"

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
