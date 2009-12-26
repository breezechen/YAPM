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

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As windowInfos)
        MyBase.New(infos)
        _pid = infos.ProcessId
        _taskinfos = New taskInfos(infos)
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Task

        ' Get process from process list
        _process = cProcess.GetProcessById(_pid)
    End Sub

#End Region

#Region "Normal properties"

    Public Overloads ReadOnly Property Infos() As taskInfos
        Get
            Return _taskinfos
        End Get
    End Property

#End Region

#Region "Other properties"

    Public ReadOnly Property CpuUsage() As Double
        Get
            If _process IsNot Nothing Then
                Return _process.CpuUsage
            Else
                ' We have a list and we never tried to get _process -> do it
                ' Get it from lvProcess (this is the only list which calls
                ' the Merge() func which calculate CPU usage)
                _process = _frmMain.lvProcess.GetItemByKey(_pid.ToString)
                If _process IsNot Nothing Then
                    Return _process.CpuUsage
                Else
                    Return 0
                End If
            End If
        End Get
    End Property

#End Region


#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return taskInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_CpuUsage As String = ""

        Dim hasChanged As Boolean = True

        Select Case info
            Case "CpuUsage"
                res = Common.Misc.GetFormatedPercentage(Me.CpuUsage)
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
    Public Overrides Function GetInformation(ByVal info As String) As String

        Select Case info
            Case "CpuUsage"
                Return Common.Misc.GetFormatedPercentage(Me.CpuUsage)
            Case Else
                Return MyBase.GetInformation(info)
        End Select

    End Function

#End Region

End Class
