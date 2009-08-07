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

Public Class cPrivilege
    Inherits cGeneralObject

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

    ' Change status
    Private _changeST As asyncCallbackPrivilegeChangeStatus
    Public Function ChangeStatus(ByVal status As API.PRIVILEGE_STATUS) As Integer

        If _changeST Is Nothing Then
            _changeST = New asyncCallbackPrivilegeChangeStatus(New asyncCallbackPrivilegeChangeStatus.HasChangedStatus(AddressOf changeStatusDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _changeST.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackPrivilegeChangeStatus.poolObj(Me.Infos.ProcessId, Me.Infos.Name, status, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub changeStatusDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change privilege status " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Local shared method"

    Public Shared Function LocalChangeStatus(ByVal pid As Integer, ByVal name As String, ByVal status As API.PRIVILEGE_STATUS) As Boolean
        Return asyncCallbackPrivilegeChangeStatus.SetPrivilege(pid, name, status)
    End Function

#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Name"
                res = Me.Infos.Name
            Case "Status"
                res = GetStatusString(Me.Infos.Status)
            Case "Description"
                res = Me.Infos.Description
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Name As String = ""
        Static _old_Status As String = ""
        Static _old_Description As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Name"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "Status"
                res = GetStatusString(Me.Infos.Status)
                If res = _old_Status Then
                    hasChanged = False
                Else
                    _old_Status = res
                End If
            Case "Description"
                res = Me.Infos.Description
                If res = _old_Description Then
                    hasChanged = False
                Else
                    _old_Description = res
                End If
        End Select

        Return hasChanged
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
