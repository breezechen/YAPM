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
' - Declaration of some structures used by NtQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text

<Serializable()> Public Class serviceInfos
    Inherits generalInfos

    Public Shared Operator <>(ByVal m1 As serviceInfos, ByVal m2 As serviceInfos) As Boolean
        Return Not (m1 = m2)
    End Operator
    Public Shared Operator =(ByVal i1 As serviceInfos, ByVal i2 As serviceInfos) As Boolean
        Return (i1.ProcessId = i2.ProcessId AndAlso _
            i1.State = i2.State AndAlso _
            i1.AcceptedControl = i2.AcceptedControl AndAlso _
            i1.CheckPoint = i2.CheckPoint AndAlso _
            i1.ServiceType = i2.ServiceType AndAlso _
            i1.ProcessName = i2.ProcessName AndAlso _
            i1.ServiceFlags = i2.ServiceFlags AndAlso _
            i1.ServiceSpecificExitCode = i2.ServiceSpecificExitCode AndAlso _
            i1.WaitHint = i2.WaitHint AndAlso _
            i1.Win32ExitCode = i2.Win32ExitCode)
    End Operator

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
    Private _acceptedCtrls As Native.Api.NativeEnums.ServiceAccept
    Private _name As String
    <NonSerialized()> Private _fileInfo As FileVersionInfo

    Private _tag As Boolean = False
    Private _Dependencies As String()
    Private _tagID As Integer
    Private _ServiceFlags As API.SERVICE_FLAGS
    Private _WaitHint As Integer
    Private _CheckPoint As Integer
    Private _ServiceSpecificExitCode As Integer
    Private _Win32ExitCode As Integer

    Private _allInformationsRetrieved As Boolean
#End Region

#Region "Read only properties"

    Public Property Tag() As Boolean
        Get
            Return _tag
        End Get
        Set(ByVal value As Boolean)
            _tag = value
        End Set
    End Property
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
    Public ReadOnly Property AcceptedControl() As Native.Api.NativeEnums.ServiceAccept
        Get
            Return _acceptedCtrls
        End Get
    End Property
    Public ReadOnly Property Dependencies() As String()
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
    Public Property FileInfo() As FileVersionInfo
        Get
            Return _fileInfo
        End Get
        Set(ByVal value As FileVersionInfo)
            _fileInfo = value
        End Set
    End Property
#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef Thr As API.ENUM_SERVICE_STATUS_PROCESS, Optional ByVal allInfos As Boolean = False)
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
            _allInformationsRetrieved = allInfos
        End With
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As serviceInfos)

        With newI
            _pid = .ProcessId
            _state = .State
            _acceptedCtrls = .AcceptedControl
            _CheckPoint = .CheckPoint
            _serviceType = .ServiceType
            _processName = .ProcessName
            _ServiceFlags = .ServiceFlags
            _ServiceSpecificExitCode = .ServiceSpecificExitCode
            _WaitHint = .WaitHint
            _Win32ExitCode = .Win32ExitCode

            If _allInformationsRetrieved Then
                ' Then we are in WMI or Socket mode : all informations 
                ' (including startType...etc) have been retrieved
                ' So the merge have to copy all the informations below
                _serviceType = .ServiceType
                _errorControl = .ErrorControl
                _startType = .StartType
                _path = .ImagePath
                _displayName = .DisplayName
                _loadOrderGroup = .LoadOrderGroup
                _startName = .ServiceStartName
                _Dependencies = .Dependencies
                _desc = .Description                 ' UPDATED ONCE (no merge)
                _diagMF = .DiagnosticMessageFile     ' UPDATED ONCE (no merge)
                _objName = .ObjectName                ' UPDATED ONCE (no merge)
                _tagID = .TagID
            End If

            If _fileInfo Is Nothing Then
                _fileInfo = .FileInfo
            End If
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
            _tagID = .TagID

            ' === Get dependencies of service
            If .Dependencies > 0 Then

                ' Get a short array from memory
                ' Delimited by 2 null chars (e.g 4 zero byte as it is unicode)
                Dim res() As Int16
                ReDim res(0)
                Dim size As Integer = 1

                Dim b1 As Short = -1
                Dim b2 As Short = -1

                Do While Not (b1 = 0 And b2 = 0)
                    size += 1
                    ReDim res(size - 1)
                    Marshal.Copy(New IntPtr(.Dependencies), res, 0, res.Length)
                    b1 = res(size - 2)
                    b2 = res(size - 1)
                Loop

                size -= 1
                ReDim Preserve res(size - 1)

                ' Get a string array from this short array
                Dim __var As String
                Dim rr() As String
                ReDim rr(-1)
                Dim xOld As Integer = 0
                Dim y As Integer

                If size > 2 Then

                    For x As Integer = 0 To size - 1

                        If res(x) = 0 Then
                            ' Then it's variable end
                            ReDim Preserve rr(rr.Length)  ' Add one item to list
                            Try
                                ' Parse short array to retrieve an unicode string
                                y = x * 2
                                Dim __size As Integer = CInt((y - xOld) / 2)

                                ' Allocate unmanaged memory
                                Dim ptr As IntPtr = Marshal.AllocHGlobal(y - xOld)

                                ' Copy from short array to unmanaged memory
                                Marshal.Copy(res, CInt(xOld / 2), ptr, __size)

                                ' Convert to string (and copy to __var variable)
                                __var = Marshal.PtrToStringUni(ptr, __size)

                                ' Free unmanaged memory
                                Marshal.FreeHGlobal(ptr)

                            Catch ex As Exception
                                __var = ""
                            End Try

                            ' Insert variable
                            rr(rr.Length - 1) = __var

                            xOld = y + 2
                        End If
                    Next

                    _Dependencies = rr
                End If
            End If

        End With
    End Sub

    Friend Sub SetRealImagePath()
        _path = GetRealPath(_path)
    End Sub

    Friend Sub SetRegInfos(ByVal desc As String, ByVal dmf As String, ByVal obj As String)
        _desc = desc
        _diagMF = dmf
        _objName = obj
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
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

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        Return s
    End Function

End Class
