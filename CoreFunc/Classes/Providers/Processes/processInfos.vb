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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices

Public Class processInfos

    Private Const NO_INFO_RETRIEVED As String = "N/A"

#Region "Private attributes"

    Private _CommandLine As String
    Private _Pid As Integer
    Private _AffinityMask As Integer
    Private _PEBAddress As Integer
    Private _ParentProcessId As Integer
    Private _IOValues As API.IO_COUNTERS
    Private _Path As String
    Private _UserName As String
    Private _Name As String
    Private _KernelTime As Long
    Private _UserTime As Long
    Private _MemoryInfos As API.VM_COUNTERS_EX
    Private _AverageCpuUsage As Double
    Private _HandleCount As Integer
    Private _StartTime As Long
    Private _Priority As ProcessPriorityClass
    Private _fileInfo As FileVersionInfo
    Private _gdiObjects As Integer
    Private _userObjects As Integer

    Private _processors As Integer

#End Region

#Region "Read only properties"

    Public ReadOnly Property Pid() As Integer
        Get
            Return _Pid
        End Get
    End Property
    Public ReadOnly Property ParentProcessId() As Integer
        Get
            Return _ParentProcessId
        End Get
    End Property
    Public ReadOnly Property IOValues() As API.IO_COUNTERS
        Get
            Return _IOValues
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property
    Public ReadOnly Property ProcessorTime() As Long
        Get
            Return _UserTime + _KernelTime
        End Get
    End Property
    Public ReadOnly Property KernelTime() As Long
        Get
            Return _KernelTime
        End Get
    End Property
    Public ReadOnly Property UserTime() As Long
        Get
            Return _UserTime
        End Get
    End Property
    Public ReadOnly Property MemoryInfos() As API.VM_COUNTERS_EX
        Get
            Return _MemoryInfos
        End Get
    End Property
    Public ReadOnly Property Priority() As ProcessPriorityClass
        Get
            Return _Priority
        End Get
    End Property
    Public ReadOnly Property AverageCpuUsage() As Double
        Get
            Return _AverageCpuUsage
        End Get
    End Property
    Public ReadOnly Property HandleCount() As Integer
        Get
            Return _HandleCount
        End Get
    End Property
    Public ReadOnly Property StartTime() As Long
        Get
            Return _StartTime
        End Get
    End Property

#End Region

#Region "Other but fixed and non-RO properties"

    Public Property PEBAddress() As Integer
        Get
            Return _PEBAddress
        End Get
        Set(ByVal value As Integer)
            _PEBAddress = value
        End Set
    End Property
    Public Property FileInfo() As FileVersionInfo
        Get
            Return _fileInfo
        End Get
        Set(ByVal value As FileVersionInfo)
            _fileInfo = value
        End Set
    End Property
    Public Property CommandLine() As String
        Get
            Return _CommandLine
        End Get
        Set(ByVal value As String)
            _CommandLine = value
        End Set
    End Property
    Public Property Path() As String
        Get
            Return _Path
        End Get
        Set(ByVal value As String)
            _Path = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property

#End Region

#Region "Other Non-fixed informations"

    Public Property AffinityMask() As Integer
        Get
            Return _AffinityMask
        End Get
        Set(ByVal value As Integer)
            _AffinityMask = value
        End Set
    End Property
    Public Property GdiObjects() As Integer
        Get
            Return _gdiObjects
        End Get
        Set(ByVal value As Integer)
            _gdiObjects = value
        End Set
    End Property
    Public Property UserObjects() As Integer
        Get
            Return _userObjects
        End Get
        Set(ByVal value As Integer)
            _userObjects = value
        End Set
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef Proc As API.SYSTEM_PROCESS_INFORMATION, Optional ByVal ProcessName As String = Nothing)
        _PEBAddress = 0

        With Proc
            '_AffinityMask = 0
            _UserTime = .UserTime
            _KernelTime = .KernelTime
            _StartTime = Date.FromFileTime(.CreateTime).Ticks
            _MemoryInfos = .VirtualMemoryCounters
            _Priority = getPriorityClass(.BasePriority)
            _IOValues = .IoCounters
            _HandleCount = .HandleCount
            _Pid = .ProcessId
            _ParentProcessId = .InheritedFromProcessId
            If _Pid > 0 Then
                If ProcessName IsNot Nothing Then
                    _Name = ProcessName
                Else
                    _Name = ReadUnicodeString(.ImageName)
                End If
            Else
                _Name = "Idle process"
            End If
        End With

        _processors = cSystemInfo.GetProcessorCount
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As processInfos)

        With newI
            _KernelTime = .KernelTime
            _UserTime = .UserTime
            _Priority = .Priority
            _MemoryInfos = .MemoryInfos
            _IOValues = .IOValues
            _HandleCount = .HandleCount
            _gdiObjects = .GdiObjects
            _userObjects = .UserObjects
            _AffinityMask = .AffinityMask
        End With
    End Sub


    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(35) As String

        s(0) = "PID"
        s(1) = "UserName"
        s(2) = "ParentPID"
        s(3) = "ParentName"
        s(4) = "CpuUsage"
        s(5) = "AverageCpuUsage"
        s(6) = "KernelCpuTime"
        s(7) = "UserCpuTime"
        s(8) = "TotalCpuTime"
        s(9) = "StartTime"
        s(10) = "RunTime"
        s(11) = "GdiObjects"
        s(12) = "UserObjects"
        s(13) = "AffinityMask"
        s(14) = "HandleCount"
        s(15) = "WorkingSet"
        s(16) = "PeakWorkingSet"
        s(17) = "PageFaultCount"
        s(18) = "PagefileUsage"
        s(19) = "PeakPagefileUsage"
        s(20) = "QuotaPeakPagedPoolUsage"
        s(21) = "QuotaPagedPoolUsage"
        s(22) = "QuotaPeakNonPagedPoolUsage"
        s(23) = "QuotaNonPagedPoolUsage"
        s(24) = "ReadOperationCount"
        s(25) = "WriteOperationCount"
        s(26) = "OtherOperationCount"
        s(27) = "ReadTransferCount "
        s(28) = "WriteTransferCount"
        s(29) = "OtherTransferCount"
        s(30) = "Priority"
        s(31) = "Path"
        s(32) = "CommandLine"
        s(33) = "Description"
        s(34) = "Copyright"
        s(35) = "Version"

        Return s
    End Function

    ' Return a class from an int (concerning priority)
    Private Function getPriorityClass(ByVal priority As Integer) As ProcessPriorityClass
        If priority >= 24 Then
            Return ProcessPriorityClass.RealTime
        ElseIf priority >= 13 Then
            Return ProcessPriorityClass.High
        ElseIf priority >= 10 Then
            Return ProcessPriorityClass.AboveNormal
        ElseIf priority >= 8 Then
            Return ProcessPriorityClass.Normal
        ElseIf priority >= 6 Then
            Return ProcessPriorityClass.BelowNormal
        Else
            Return ProcessPriorityClass.Idle
        End If
    End Function

End Class
