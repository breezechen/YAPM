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

Imports Native.Api.Enums

Public Class cSearchItem
    Inherits cGeneralObject

    Private _info As searchInfos
    Private Shared WithEvents _connection As cSearchConnection

#Region "Properties"

    Public Shared Property Connection() As cSearchConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cSearchConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef item As searchInfos)
        _info = item
        _TypeOfObject = GeneralObjectType.SearchItem
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As searchInfos
        Get
            Return _info
        End Get
    End Property

#End Region


#Region "All actions on search item"

    ' Close/Terminate/Unload
    Private _closeH As asyncCallbackHandleUnload
    Private _closeW As asyncCallbackWindowAction
    Private _closeM As asyncCallbackModuleUnload
    Private _closeS As asyncCallbackServiceStop
    Private _closeP As asyncCallbackProcKill
    Public Function CloseTerminate() As Integer

        Select Case Infos.Type
            Case GeneralObjectType.Handle
                ' Close a handle
                If _closeH Is Nothing Then
                    _closeH = New asyncCallbackHandleUnload(New asyncCallbackHandleUnload.HasUnloadedHandle(AddressOf unloadHandleDone), cHandle.Connection)
                End If

                Dim t As New System.Threading.WaitCallback(AddressOf _closeH.Process)
                Dim newAction As Integer = cGeneralObject.GetActionCount

                AddPendingTask(newAction, t)
                'Dim handle As cHandle = DirectCast(Me.Infos.Object, cHandle)
                'Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
                '    asyncCallbackHandleUnload.poolObj(handle.Infos.ProcessId, handle.Infos.Handle, newAction))

            Case GeneralObjectType.Module
                ' Unload a module
                If _closeM Is Nothing Then
                    _closeM = New asyncCallbackModuleUnload(New asyncCallbackModuleUnload.HasUnloadedModule(AddressOf unloadModuleDone), cModule.Connection)
                End If

                Dim t As New System.Threading.WaitCallback(AddressOf _closeM.Process)
                Dim newAction As Integer = cGeneralObject.GetActionCount

                AddPendingTask(newAction, t)
                'Dim modul As cModule = DirectCast(Me.Infos.Object, cModule)
                'Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
                '    asyncCallbackModuleUnload.poolObj(modul.Infos.ProcessId, modul.Infos.Name, modul.Infos.BaseAddress, newAction))

            Case GeneralObjectType.Process
                ' Kill a process
                If _closeP Is Nothing Then
                    _closeP = New asyncCallbackProcKill(New asyncCallbackProcKill.HasKilled(AddressOf killDone))
                End If

                Dim t As New System.Threading.WaitCallback(AddressOf _closeP.Process)
                Dim newAction As Integer = cGeneralObject.GetActionCount

                AddPendingTask(newAction, t)
                'Dim process As cProcess = DirectCast(Me.Infos.Object, cProcess)
                'Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
                '    asyncCallbackProcKill.poolObj(process.Infos.ProcessId, newAction))

            Case GeneralObjectType.Service
                ' Stop service
                If _closeS Is Nothing Then
                    _closeS = New asyncCallbackServiceStop(New asyncCallbackServiceStop.HasStopped(AddressOf stopServiceDone))
                End If

                Dim t As New System.Threading.WaitCallback(AddressOf _closeS.Process)
                Dim newAction As Integer = cGeneralObject.GetActionCount

                AddPendingTask(newAction, t)
                'Dim service As cService = DirectCast(Me.Infos.Object, cService)
                'Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
                '    asyncCallbackServiceStop.poolObj(service.Infos.Name, newAction))

            Case GeneralObjectType.Window
                ' Close window
                If _closeW Is Nothing Then
                    _closeW = New asyncCallbackWindowAction(New asyncCallbackWindowAction.HasMadeAction(AddressOf windowActionDone), cWindow.Connection)
                End If
                Dim t As New System.Threading.WaitCallback(AddressOf _closeW.Process)
                Dim newAction As Integer = cGeneralObject.GetActionCount

                AddPendingTask(newAction, t)
                'Dim window As cWindow = DirectCast(Me.Infos.Object, cWindow)
                'Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
                '    asyncCallbackWindowAction.poolObj(Native.Api.Enums.AsyncWindowAction.Close, window.Infos.Handle, 0, 0, 0, newAction))

        End Select

    End Function
    Private Sub unloadHandleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload handle " & handle.ToString & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub
    Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload module " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub
    Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not kill process (" & pid.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub
    Private Sub stopServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not stop service " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub
    Private Sub windowActionDone(ByVal Success As Boolean, ByVal action As Native.Api.Enums.AsyncWindowAction, ByVal handle As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not " & action.ToString & " (window = 0x" & handle.ToString("x") & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return searchInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function

    ' Get information as a string
    Public Overrides Function GetInformation(ByVal info As String) As String

        Static _owner As String = Nothing

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Type"
                res = Infos.Type.ToString
            Case "Result"
                res = Infos.Result
            Case "Field"
                res = Infos.Field
            Case "Owner"
                If _owner Is Nothing Then
                    _owner = Infos.Owner
                End If
                Return _owner
        End Select

        Return res
    End Function

    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean
        res = Me.GetInformation(info)
        Return True
    End Function

#End Region

End Class
