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

Public Class cLogItem
    Inherits cGeneralObject

    Private _logInfos As logItemInfos
    Private Shared WithEvents _connection As cLogConnection

#Region "Properties"

    Public Shared Property Connection() As cLogConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cLogConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As logItemInfos)
        _logInfos = infos
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As logItemInfos
        Get
            Return _logInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByVal Thr As logItemInfos)
        _logInfos.Merge(Thr)
    End Sub

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            Return PendingTaskCount.ToString
        End If

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Type"
                res = Me.Infos.Type
            Case "Date & Time", "Date && Time"
                res = Me.Infos.DateTime.ToLongDateString & " - " & Me.Infos.DateTime.ToLongTimeString
            Case "Description"
                res = Me.Infos.Description
        End Select

        Return res
    End Function

#End Region

End Class
