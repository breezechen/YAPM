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

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _taskinfos As taskInfos

    Private _oldCaption As String

    Private _pid As Integer
    Private _cpuUsage As Double

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As windowInfos)
        MyBase.New(infos)
        _pid = infos.ProcessId
        _taskinfos = New taskInfos(infos)
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



#End Region


#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        Select Case info
            Case "CpuUsage"
                Return GetFormatedPercentage(_cpuUsage)
            Case Else
                Return MyBase.GetInformation(info)
        End Select

    End Function

#End Region

End Class
