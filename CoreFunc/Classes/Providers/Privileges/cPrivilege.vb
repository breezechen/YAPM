﻿' =======================================================
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

Option Strict On

Public Class cPrivilege
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _privilegeInfos As privilegeInfos
    Private Shared WithEvents _connection As cPrivilegeConnection


#Region "Properties"

    Public Shared Property Connection() As cPrivilegeConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cPrivilegeConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As privilegeInfos)
        _privilegeInfos = infos
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As privilegeInfos
        Get
            Return _privilegeInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As privilegeInfos)
        _privilegeInfos.Merge(Thr)
    End Sub

#Region "All available actions"

    ' Increase priority
    Private WithEvents asyncPrivStatus As asyncCallbackPrivilegeChangeStatus
    Public Function ChangeStatus(ByVal status As API.PRIVILEGE_STATUS) As Integer
        asyncPrivStatus = New asyncCallbackPrivilegeChangeStatus(Me.Infos.ProcessId, status, Me.Infos.Name, _connection)
        Dim t As New Threading.Thread(AddressOf asyncPrivStatus.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "ChangePrivilegeStatus"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub changeStatusDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncPrivStatus.HasChangedStatus
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change privilege status " & name)
        End If
    End Sub


#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Name"
                res = Me.Infos.Name
            Case "Status"
                res = GetStatusString(Me.Infos.Status)
            Case "Description"
                res = Me.Infos.description
        End Select

        Return res
    End Function

#End Region

    Private Function GetStatusString(ByVal status As API.PRIVILEGE_STATUS) As String
        Select Case CInt(status)
            Case API.PRIVILEGE_STATUS.PRIVILEGE_DISBALED
                Return "Disabled"
            Case API.PRIVILEGE_STATUS.PRIVILEGE_ENABLED
                Return "Enabled"
            Case API.PRIVILEGE_STATUS.PRIVILEGE_REMOVED
                Return "Removed"
            Case 1
                Return "Default Disabled"
            Case 3
                Return "Default enabled"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function

End Class