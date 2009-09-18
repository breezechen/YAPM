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

Imports YAPM.Native.Api
Imports System.Runtime.InteropServices
Imports System.Net

<Serializable()> Public Class jobInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _name As String

    ' Stats structs
    Private basicAcIoInfo As NativeStructs.JobObjectBasicAndIoAccountingInformation
    Private basicLimitInfo As NativeStructs.JobObjectBasicLimitInformation

    ' Contains list of process Id of the job
    Private _procIds As New List(Of Integer)

#End Region

#Region "Read only properties"

    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property BasicAndIoAccountingInformation() As NativeStructs.JobObjectBasicAndIoAccountingInformation
        Get
            Return basicAcIoInfo
        End Get
    End Property
    Public ReadOnly Property BasicLimitInformation() As NativeStructs.JobObjectBasicLimitInformation
        Get
            Return basicLimitInfo
        End Get
    End Property
    Public ReadOnly Property PidList() As List(Of Integer)
        Get
            Return _procIds
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByVal name As String)
        _name = name
    End Sub

    ' Refresh infos
    Public Sub Refresh()
        ' Here we refreh all informations about the job
        Dim _dico As Dictionary(Of String, processInfos) = Native.Objects.Job.GetProcessesInJobByName(Name)
        Dim tmpProcIds As New List(Of Integer)
        For Each cp As processInfos In _dico.Values
            tmpProcIds.Add(cp.ProcessId)
        Next
        _procIds = tmpProcIds
        basicAcIoInfo = Native.Objects.Job.GetJobBasicAndIoAccountingInformationByName(Name)
        basicLimitInfo = Native.Objects.Job.GetJobBasicLimitInformationByName(Name)
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As jobInfos)
        With newI
            _procIds = .PidList
            basicAcIoInfo = .BasicAndIoAccountingInformation
            basicLimitInfo = .BasicLimitInformation
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
