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

Public Class cService
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _serviceInfos As serviceInfos
    Private Shared WithEvents _connection As cServiceConnection

#Region "Properties"

    Public Shared Property Connection() As cServiceConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cServiceConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As serviceInfos)
        _serviceInfos = infos
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As serviceInfos
        Get
            Return _serviceInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As serviceInfos)
        _serviceInfos.Merge(Thr)
    End Sub

#Region "All actions on services (start...)"

    ' Start service
    'Private WithEvents asyncUModule As asyncCallbackModuleUnload
    Public Sub StartService()
        'asyncUModule = New asyncCallbackModuleUnload(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.Name, _connection)
        'Dim t As New Threading.Thread(AddressOf asyncUModule.Process)
        't.Priority = Threading.ThreadPriority.Lowest
        't.Name = "IncreasePriority"
        't.IsBackground = True
        't.Start()
    End Sub
    'Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncUModule.HasUnloadedModule
    '    If Success = False Then
    '        MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
    '               "Could not unload module " & name)
    '    End If
    'End Sub

    ' Pause a service
    'Private WithEvents asyncUModule As asyncCallbackModuleUnload
    Public Sub PauseService()

    End Sub
    'Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncUModule.HasUnloadedModule
    '    If Success = False Then
    '        MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
    '               "Could not unload module " & name)
    '    End If
    'End Sub

    ' Resume a service
    'Private WithEvents asyncUModule As asyncCallbackModuleUnload
    Public Sub ResumeService()

    End Sub
    'Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncUModule.HasUnloadedModule
    '    If Success = False Then
    '        MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
    '               "Could not unload module " & name)
    '    End If
    'End Sub

    ' Stop a service
    'Private WithEvents asyncUModule As asyncCallbackModuleUnload
    Public Sub StopService()

    End Sub
    'Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncUModule.HasUnloadedModule
    '    If Success = False Then
    '        MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
    '               "Could not unload module " & name)
    '    End If
    'End Sub

    ' Shutdown a service
    'Private WithEvents asyncUModule As asyncCallbackModuleUnload
    Public Sub ShutDownService()

    End Sub
    'Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncUModule.HasUnloadedModule
    '    If Success = False Then
    '        MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
    '               "Could not unload module " & name)
    '    End If
    'End Sub

    ' Set service start type
    'Private WithEvents asyncUModule As asyncCallbackModuleUnload
    Public Sub SetServiceStartType(ByVal type As API.SERVICE_START_TYPE)

    End Sub
    'Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String) Handles asyncUModule.HasUnloadedModule
    '    If Success = False Then
    '        MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
    '               "Could not unload module " & name)
    '    End If
    'End Sub

#End Region

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        Select Case info
            Case "Name"
                res = Me.Infos.Name
            Case "DisplayName"
                res = Me.Infos.DisplayName
            Case "ServiceType"
                res = Me.Infos.ServiceType.ToString
            Case "ImagePath"
                res = Me.Infos.ImagePath
            Case "ErrorControl"
                res = Me.Infos.ErrorControl.ToString
            Case "StartType"
                res = Me.Infos.StartType.ToString
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "ProcessName"
                res = Me.Infos.ProcessName
            Case "LoadOrderGroup"
                res = Me.Infos.LoadOrderGroup
            Case "ServiceStartName"
                res = Me.Infos.ServiceStartName
            Case "State"
                res = Me.Infos.State.ToString
            Case "Description"
                res = Me.Infos.Description
            Case "DiagnosticMessageFile"
                res = Me.Infos.DiagnosticMessageFile
            Case "ObjectName"
                res = Me.Infos.ObjectName
            Case "Process"
                If Me.Infos.ProcessId > 0 Then
                    res = cProcess.GetProcessName(Me.Infos.ProcessId) & " -- " & Me.Infos.ProcessId.ToString
                End If
            Case "Dependencies"
                res = Me.Infos.Dependencies
            Case "TagID"
                res = Me.Infos.TagID.ToString
            Case "ServiceFlags"
                res = Me.Infos.ServiceFlags.ToString
            Case "WaitHint"
                res = Me.Infos.WaitHint.ToString
            Case "CheckPoint"
                res = Me.Infos.CheckPoint.ToString
            Case "Win32ExitCode"
                res = Me.Infos.Win32ExitCode.ToString
            Case "ServiceSpecificExitCode"
                res = Me.Infos.ServiceSpecificExitCode.ToString
        End Select

        Return res
    End Function


#End Region

End Class
