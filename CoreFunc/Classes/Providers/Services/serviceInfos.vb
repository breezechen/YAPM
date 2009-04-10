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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices

Public Class serviceInfos

    Private Const NO_INFO_RETRIEVED As String = "N/A"

#Region "Private attributes"

    Private _pid As Integer
    Private _state As API.SERVICE_STATE
    Private _displayName As String
    Private _startType As API.SERVICE_START_TYPE
    Private _path As String
    Private _serviceType As API.SERVICE_TYPE
    Private _desc As String
    Private _errorControl As Integer
    Private _processName As String
    Private _loadOrderGroup As String
    Private _startName As String
    Private _diagMF As String
    Private _objName As String
    Private _acceptedCtrls As API.SERVICE_ACCEPT
    Private _name As String

    Private _Dependencies As String
    Private _tagID As Integer
    Private _ServiceFlags As API.SERVICE_FLAGS
    Private _WaitHint As Integer
    Private _CheckPoint As Integer
    Private _ServiceSpecificExitCode As Integer
    Private _Win32ExitCode As Integer
#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property DisplayName() As String
        Get
            Return _displayName
        End Get
    End Property
    Public ReadOnly Property State() As API.SERVICE_STATE
        Get
            Return _state
        End Get
    End Property
    Public ReadOnly Property StartType() As API.SERVICE_START_TYPE
        Get
            Return _startType
        End Get
    End Property
    Public ReadOnly Property ImagePath() As String
        Get
            Return _path
        End Get
    End Property
    Public ReadOnly Property ServiceType() As API.SERVICE_TYPE
        Get
            Return _serviceType
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return _desc
        End Get
    End Property
    Public ReadOnly Property ErrorControl() As Integer
        Get
            Return _errorControl
        End Get
    End Property
    Public ReadOnly Property ProcessName() As String
        Get
            Return _processName
        End Get
    End Property
    Public ReadOnly Property LoadOrderGroup() As String
        Get
            Return _loadOrderGroup
        End Get
    End Property
    Public ReadOnly Property ServiceStartName() As String
        Get
            Return _startName
        End Get
    End Property
    Public ReadOnly Property DiagnosticMessageFile() As String
        Get
            Return _diagMF
        End Get
    End Property
    Public ReadOnly Property ObjectName() As String
        Get
            Return _objName
        End Get
    End Property
    Public ReadOnly Property AcceptedControl() As API.SERVICE_ACCEPT
        Get
            Return _acceptedCtrls
        End Get
    End Property
    Public ReadOnly Property Dependencies() As String
        Get
            Return _Dependencies
        End Get
    End Property
    Public ReadOnly Property TagID() As Integer
        Get
            Return _tagID
        End Get
    End Property
    Public ReadOnly Property ServiceFlags() As API.SERVICE_FLAGS
        Get
            Return _ServiceFlags
        End Get
    End Property
    Public ReadOnly Property WaitHint() As Integer
        Get
            Return _WaitHint
        End Get
    End Property
    Public ReadOnly Property CheckPoint() As Integer
        Get
            Return _CheckPoint
        End Get
    End Property
    Public ReadOnly Property ServiceSpecificExitCode() As Integer
        Get
            Return _ServiceSpecificExitCode
        End Get
    End Property
    Public ReadOnly Property Win32ExitCode() As Integer
        Get
            Return _Win32ExitCode
        End Get
    End Property
#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef Thr As API.ENUM_SERVICE_STATUS_PROCESS)
        With Thr
            _displayName = .DisplayName
            _name = .ServiceName
            With .ServiceStatusProcess
                _CheckPoint = .CheckPoint
                _acceptedCtrls = .ControlsAccepted
                _state = .CurrentState
                _pid = .ProcessID
                _ServiceFlags = .ServiceFlags
                _ServiceSpecificExitCode = .ServiceSpecificExitCode
                _serviceType = .ServiceType
                _WaitHint = .WaitHint
                _Win32ExitCode = .Win32ExitCode
            End With
        End With
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As serviceInfos)
        With newI
            _pid = .ProcessId
            _state = .State
            '_serviceType = .ServiceType
            '_errorControl = .ErrorControl
            '_startType = .StartType
            '_path = .ImagePath
            _serviceType = .ServiceType
            '_displayName = .DisplayName
            '_loadOrderGroup = .LoadOrderGroup
            '_startName = .ServiceStartName
            _acceptedCtrls = .AcceptedControl
            _CheckPoint = .CheckPoint
            '_Dependencies = .Dependencies
            '  _desc = .Description                 ' UPDATED ONCE (no merge)
            '  _diagMF = .DiagnosticMessageFile     ' UPDATED ONCE (no merge)
            ' _objName = .ObjectName                ' UPDATED ONCE (no merge)
            _processName = .ProcessName
            _ServiceFlags = .ServiceFlags
            _ServiceSpecificExitCode = .ServiceSpecificExitCode
            '_tagID = .TagID
            _WaitHint = .WaitHint
            _Win32ExitCode = .Win32ExitCode
        End With
    End Sub

    Friend Sub SetConfig(ByRef conf As API.QUERY_SERVICE_CONFIG)
        With conf
            _serviceType = .ServiceType
            _errorControl = .ErrorControl
            _startType = .StartType
            _path = .BinaryPathName
            _displayName = .DisplayName
            _loadOrderGroup = .LoadOrderGroup
            _startName = .ServiceStartName
            _Dependencies = .Dependencies.ToString  'TOCHANGE
            _tagID = .TagID
        End With
    End Sub

    Friend Sub SetRegInfos(ByVal desc As String, ByVal dmf As String, ByVal obj As String)
        _desc = desc
        _diagMF = dmf
        _objName = obj
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(20) As String

        s(0) = "DisplayName"
        s(1) = "State"
        s(2) = "StartType"
        s(3) = "ImagePath"
        s(4) = "ServiceType"
        s(5) = "Description"
        s(6) = "ErrorControl"
        s(7) = "Process"
        s(8) = "ProcessId"
        s(9) = "ProcessName"
        s(10) = "LoadOrderGroup"
        s(11) = "ServiceStartName"
        s(12) = "DiagnosticMessageFile"
        s(13) = "ObjectName"
        s(14) = "Dependencies"
        s(15) = "TagID"
        s(16) = "ServiceFlags"
        s(17) = "WaitHint"
        s(18) = "CheckPoint"
        s(19) = "ServiceSpecificExitCode"
        s(20) = "Win32ExitCode"

        Return s
    End Function

End Class
