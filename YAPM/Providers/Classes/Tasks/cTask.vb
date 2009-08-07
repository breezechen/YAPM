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

Imports System.Text

Public Class cTask
    Inherits cWindow

    Private _taskinfos As taskInfos

    Private _pid As Integer
    Private _process As cProcess
    Private _retried As Boolean = False

    Private Shared _procList As Dictionary(Of String, cProcess)

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As windowInfos)
        MyBase.New(infos)
        _pid = infos.ProcessId
        _taskinfos = New taskInfos(infos)

        ' Get process from process list
        If _procList IsNot Nothing Then
            If _procList.Count = 0 Then
                ' When we have disconnected (no more processes)
                _procList = Nothing
            Else
                If _procList.ContainsKey(_pid.ToString) Then
                    _process = _procList(_pid.ToString)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Normal properties"

    Public Overloads ReadOnly Property Infos() As taskInfos
        Get
            Return _taskinfos
        End Get
    End Property

    Public Shared Property ProcessCollection() As Dictionary(Of String, cProcess)
        Get
            Return _procList
        End Get
        Set(ByVal value As Dictionary(Of String, cProcess))
            _procList = value
        End Set
    End Property

#End Region

#Region "Other properties"

    Public ReadOnly Property CpuUsage() As Double
        Get
            If _process IsNot Nothing Then
                Return _process.CpuUsage
            Else
                ' _process does not exist -> we try to get it
                If _procList IsNot Nothing Then
                    If _retried = False Then
                        ' We have a list and we never tried to get _process -> do it
                        If _procList.ContainsKey(_pid.ToString) Then
                            _process = _procList(_pid.ToString)
                        End If
                        _retried = True
                    End If
                End If
            End If

            Return 0
        End Get
    End Property

#End Region


#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_CpuUsage As String = ""

        Dim hasChanged As Boolean = True

        Select Case info
            Case "CpuUsage"
                res = GetFormatedPercentage(Me.CpuUsage)
                If res = _old_CpuUsage Then
                    hasChanged = False
                Else
                    _old_CpuUsage = res
                End If
            Case Else
                Return MyBase.GetInformation(info, res)
        End Select

        Return hasChanged
    End Function

#End Region

End Class
