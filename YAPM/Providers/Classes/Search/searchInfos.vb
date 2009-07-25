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
Imports System.Net

<Serializable()> Public Class searchInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _pid As Integer
    Private _handle As Integer
    Private _service As String
    Private _field As String
    Private _type As ResultType
    Private _result As String
    Private _modName As String
    Private _peb As Integer
    'Private _key As String
    'Private Shared _keyI As Integer = 0

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property Handle() As Integer
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property PebAddress() As Integer
        Get
            Return _peb
        End Get
    End Property
    Public ReadOnly Property Field() As String
        Get
            Return _field
        End Get
    End Property
    Public ReadOnly Property ModuleName() As String
        Get
            Return _modName
        End Get
    End Property
    Public ReadOnly Property Type() As ResultType
        Get
            Return _type
        End Get
    End Property
    Public ReadOnly Property Result() As String
        Get
            Return _result
        End Get
    End Property
    Public ReadOnly Property Service() As String
        Get
            Return _service
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    Public Enum SearchInclude As Integer
        SearchWindows = 1
        SearchProcesses = 2
        SearchServices = 4
        SearchHandles = 8
        SearchEnvVar = 16
        SearchModules = 32
    End Enum
    Public Enum ResultType
        [Window] = 1
        [Process] = 2
        [Service] = 4
        [Handle] = 8
        [EnvironmentVariable] = 16
        [Module] = 32
    End Enum

    ' Reinitialize key counter
    'Public Sub ClearKeyGenerator()
    '    _keyI = 0
    'End Sub

    ' Constructor of this class
    Public Sub New(ByVal pid As Integer, ByVal field As String, ByVal type As ResultType, _
                    ByVal res As String, Optional ByVal service As String = Nothing, _
                    Optional ByVal handle As Integer = 0, Optional ByVal peb As Integer = 0, _
                    Optional ByVal modName As String = Nothing)

        '_keyI += 1
        '_key = _keyI.ToString

        _handle = handle
        _service = service
        _pid = pid
        _type = type
        _field = field
        _peb = peb
        _modName = modName
        _result = res

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
        Dim s(3) As String

        s(0) = "Type"
        s(1) = "Result"
        s(2) = "Field"
        s(3) = "Process"

        Return s
    End Function

    ' Get information as a string
    Public Function GetInformation(ByVal info As String) As String

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Type"
                res = _type.ToString
            Case "Result"
                res = _result
            Case "Field"
                res = _field
            Case "Process"
                If _type = ResultType.Service Then
                    res = _service
                Else
                    cProcess.SemCurrentProcesses.WaitOne()
                    If cProcess.CurrentProcesses.ContainsKey(_pid.ToString) Then
                        Dim nn As String = ""
                        nn = cProcess.CurrentProcesses.Item(_pid.ToString).Infos.Name
                        If nn.Length > 0 Then
                            res = nn & " (" & _pid.ToString & ")"
                        Else
                            res = _pid.ToString
                        End If
                    Else
                        res = _pid.ToString
                    End If
                    cProcess.SemCurrentProcesses.Release()
                End If
        End Select

        Return res
    End Function

End Class
