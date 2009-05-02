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

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _handleInfos As handleInfos
    Private Shared WithEvents _connection As cHandleConnection

    Public Shared handles_Renamed As New clsOpenedHandles

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

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackHandleUnload.poolObj(Me.Infos.ProcessID, Me.Infos.Handle, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub unloadHandleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not unload handle " & handle.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

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
                res = Me.Infos.ProcessID.ToString
        End Select

        Return res
    End Function


#End Region

    ' Return driver control class
    Public Shared Function GetOpenedHandlesClass() As clsOpenedHandles
        Return handles_Renamed
    End Function

    ' Return all handles
    Public Shared Function CurrentLocalHandles(Optional ByVal all As Boolean = True) As Dictionary(Of String, cHandle)

        Dim _dico As New Dictionary(Of String, cHandle)

        Call cHandle.handles_Renamed.Refresh()

        Dim x As Integer = 0
        For i As Integer = 0 To cHandle.handles_Renamed.Count - 1
            If cHandle.handles_Renamed.GetHandle(i) > 0 Then
                If all OrElse (Len(cHandle.handles_Renamed.GetObjectName(i)) > 0) Then
                    With cHandle.handles_Renamed
                        Dim retHandleCount As Integer
                        Dim retHandle As Integer
                        Dim retName As String
                        Dim retObjectCount As Integer
                        Dim retPid As Integer
                        Dim retPointerCount As Integer
                        Dim retType As String
                        With cHandle.handles_Renamed
                            retHandleCount = .GetHandleCount(i)
                            retHandle = .GetHandle(i)
                            retName = .GetObjectName(i)
                            retObjectCount = .GetObjectCount(i)
                            retPid = .GetProcessID(i)
                            retPointerCount = .GetPointerCount(i)
                            retType = .GetNameInformation(i)
                        End With
                        Dim _key As String = retPid.ToString & "-" & retHandle.ToString & "-" & retType & "-" & retName
                        Dim ret As New handleInfos(retHandle, retType, retPid, retName, retHandleCount, retPointerCount, retObjectCount)
                        _dico.Add(_key, New cHandle(ret))
                    End With
                End If
            End If
        Next

        Return _dico
    End Function

End Class
