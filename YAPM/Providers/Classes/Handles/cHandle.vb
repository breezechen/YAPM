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

Public Class cHandle
    Inherits cGeneralObject

    Private _handleInfos As handleInfos
    Private Shared WithEvents _connection As cHandleConnection

#Region "Properties"

    Public Shared Property Connection() As cHandleConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cHandleConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As handleInfos)
        _handleInfos = infos
        _connection = Connection
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Handle
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As handleInfos
        Get
            Return _handleInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As handleInfos)
        _handleInfos.Merge(Thr)
    End Sub

#Region "All actions on handles (unload)"

    ' Unload handle
    Private _closeH As asyncCallbackHandleUnload
    Public Function CloseHandle() As Integer

        If _closeH Is Nothing Then
            _closeH = New asyncCallbackHandleUnload(New asyncCallbackHandleUnload.HasUnloadedHandle(AddressOf unloadHandleDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _closeH.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackHandleUnload.poolObj(Me.Infos.ProcessId, Me.Infos.Handle, newAction))

    End Function
    Private Sub unloadHandleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload handle " & handle.ToString & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return handleInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Type"
                res = Me.Infos.Type
            Case "Name"
                res = Me.Infos.Name
            Case "HandleCount"
                res = Me.Infos.HandleCount.ToString
            Case "PointerCount"
                res = Me.Infos.PointerCount.ToString
            Case "ObjectCount"
                res = Me.Infos.ObjectCount.ToString
            Case "Handle"
                res = Me.Infos.Handle.ToString
            Case "Process"
                res = Me.Infos.ProcessId.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Type As String = ""
        Static _old_Name As String = ""
        Static _old_HandleCount As String = ""
        Static _old_PointerCount As String = ""
        Static _old_ObjectCount As String = ""
        Static _old_Handle As String = ""
        Static _old_Process As String = ""
        Static _old_ObjectAddress As String = ""
        Static _old_GrantedAccess As String = ""
        Static _old_Attributes As String = ""
        Static _old_CreateTime As String = ""
        Static _old_PagedPoolUsage As String = ""
        Static _old_NonPagedPoolUsage As String = ""

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
            Case "Type"
                res = Me.Infos.Type
                If res = _old_Type Then
                    hasChanged = False
                Else
                    _old_Type = res
                End If
            Case "Name"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "HandleCount"
                res = Me.Infos.HandleCount.ToString
                If res = _old_HandleCount Then
                    hasChanged = False
                Else
                    _old_HandleCount = res
                End If
            Case "PointerCount"
                res = Me.Infos.PointerCount.ToString
                If res = _old_PointerCount Then
                    hasChanged = False
                Else
                    _old_PointerCount = res
                End If
            Case "ObjectCount"
                res = Me.Infos.ObjectCount.ToString
                If res = _old_ObjectCount Then
                    hasChanged = False
                Else
                    _old_ObjectCount = res
                End If
            Case "Handle"
                res = Me.Infos.Handle.ToString
                If res = _old_Handle Then
                    hasChanged = False
                Else
                    _old_Handle = res
                End If
            Case "Process"
                res = Me.Infos.ProcessId.ToString
                If res = _old_Process Then
                    hasChanged = False
                Else
                    _old_Process = res
                End If
            Case "ObjectAddress"
                res = "0x" & Me.Infos.ObjectAddress.ToString("x")
                If res = _old_ObjectAddress Then
                    hasChanged = False
                Else
                    _old_ObjectAddress = res
                End If
            Case "GrantedAccess"
                res = Me.Infos.GrantedAccess.ToString
                If res = _old_GrantedAccess Then
                    hasChanged = False
                Else
                    _old_GrantedAccess = res
                End If
            Case "Attributes"
                res = Me.Infos.Attributes.ToString
                If res = _old_Attributes Then
                    hasChanged = False
                Else
                    _old_Attributes = res
                End If
            Case "CreateTime"
                res = Me.Infos.CreateTime.ToString
                If res = _old_CreateTime Then
                    hasChanged = False
                Else
                    _old_CreateTime = res
                End If
            Case "PagedPoolUsage"
                res = Me.Infos.PagedPoolUsage.ToString
                If res = _old_PagedPoolUsage Then
                    hasChanged = False
                Else
                    _old_PagedPoolUsage = res
                End If
            Case "NonPagedPoolUsage"
                res = Me.Infos.NonPagedPoolUsage.ToString
                If res = _old_NonPagedPoolUsage Then
                    hasChanged = False
                Else
                    _old_NonPagedPoolUsage = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

#Region "Shared functions"

    Private Shared _sharedcloseH As asyncCallbackHandleUnload
    Public Shared Function SharedLRCloseHandle(ByVal pid As Integer, ByVal handle As IntPtr) As Integer

        If _sharedcloseH Is Nothing Then
            _sharedcloseH = New asyncCallbackHandleUnload(New asyncCallbackHandleUnload.HasUnloadedHandle(AddressOf unloadsharedHandleDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedcloseH.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackHandleUnload.poolObj(pid, handle, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub unloadsharedHandleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload handle " & handle.ToString & " : " & msg)
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

#End Region

End Class
