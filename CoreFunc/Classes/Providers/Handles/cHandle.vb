' =======================================================
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

    ' Increase priority
    Private WithEvents asyncUHandle As asyncCallbackHandleUnload
    Public Function UnloadHandle() As Integer
        asyncUHandle = New asyncCallbackHandleUnload(Me.Infos.ProcessId, Me.Infos.handle, _connection)
        Dim t As New Threading.Thread(AddressOf asyncUHandle.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "IncreasePriority"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub unloadHandleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As Integer, ByVal msg As String) Handles asyncUHandle.HasUnloadedHandle
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not unload handle " & handle.ToString)
        End If
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
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
                res = Me.Infos.ProcessId.ToString
        End Select

        Return res
    End Function


#End Region

    ' Return driver control class
    Public Shared Function GetOpenedHandlesClass() As clsOpenedHandles
        Return handles_Renamed
    End Function

End Class
