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

Imports System.Runtime.InteropServices

<Serializable()> Public Class processInfos
    Inherits generalInfos

    Public Shared Operator <>(ByVal m1 As processInfos, ByVal m2 As processInfos) As Boolean
        Return Not (m1 = m2)
    End Operator
    Public Shared Operator =(ByVal m1 As processInfos, ByVal m2 As processInfos) As Boolean
        Return (m1.KernelTime = m2.KernelTime AndAlso _
            m1.UserTime = m2.UserTime AndAlso _
            m1.Priority = m2.Priority AndAlso _
            m1.MemoryInfos = m2.MemoryInfos AndAlso _
            m1.IOValues = m2.IOValues AndAlso _
            m1.HandleCount = m2.HandleCount AndAlso _
            m1.GdiObjects = m2.GdiObjects AndAlso _
            m1.UserObjects = m2.UserObjects AndAlso _
            m1.AffinityMask = m2.AffinityMask AndAlso _
            m1.IsHidden = m2.IsHidden AndAlso _
            m1.ThreadCount = m2.ThreadCount)
    End Operator


#Region "Private attributes"

    Private _CommandLine As String
    Private _isHidden As Boolean
    Private _Pid As Integer
    Private _AffinityMask As IntPtr
    Private _PebAddress As IntPtr
    Private _fileInfo As SerializableFileVersionInfo
    Private _ParentProcessId As Integer
    Private _IOValues As Native.Api.NativeStructs.IoCounters
    Private _Path As String
    Private _UserName As String
    Private _DomainName As String
    Private _Name As String
    Private _KernelTime As Long
    Private _UserTime As Long
    Private _MemoryInfos As Native.Api.Structs.VmCountersEx64
    Private _HandleCount As Integer
    Private _StartTime As Long
    Private _Priority As ProcessPriorityClass
    Private _gdiObjects As Integer
    Private _userObjects As Integer
    Private _threadCount As Integer

    Private _hasReanalize As Boolean = False

    Private _processors As Integer

#End Region

#Region "Read only properties"

    Public Property HasReanalize() As Boolean
        Get
            Return _hasReanalize
        End Get
        Set(ByVal value As Boolean)
            _hasReanalize = value
        End Set
    End Property

    Public Property IsHidden() As Boolean
        Get
            Return _isHidden
        End Get
        Set(ByVal value As Boolean)
            _isHidden = value
        End Set
    End Property
    Public ReadOnly Property ThreadCount() As Integer
        Get
            Return _threadCount
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _Pid
        End Get
    End Property
    Public ReadOnly Property ParentProcessId() As Integer
        Get
            Return _ParentProcessId
        End Get
    End Property
    Public ReadOnly Property IOValues() As Native.Api.NativeStructs.IoCounters
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
    Public ReadOnly Property MemoryInfos() As Native.Api.Structs.VmCountersEx64
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
            Dim i As Long = Date.Now.Ticks - Me.StartTime
            If i > 0 AndAlso _processors > 0 Then
                Return (Me.ProcessorTime / i / _processors)
            Else
                Return 0
            End If
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

    Public Property PebAddress() As IntPtr
        Get
            Return _PebAddress
        End Get
        Set(ByVal value As IntPtr)
            _PebAddress = value
        End Set
    End Property
    Public Property FileInfo() As SerializableFileVersionInfo
        Get
            Return _fileInfo
        End Get
        Set(ByVal value As SerializableFileVersionInfo)
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
    Public Property DomainName() As String
        Get
            Return _DomainName
        End Get
        Set(ByVal value As String)
            _DomainName = value
        End Set
    End Property

#End Region

#Region "Other Non-fixed informations"

    Public Property AffinityMask() As IntPtr
        Get
            Return _AffinityMask
        End Get
        Set(ByVal value As IntPtr)
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
    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef Proc As Native.Api.Structs.SystemProcessInformation64, Optional ByVal ProcessName As String = Nothing)
        _PebAddress = IntPtr.Zero

        With Proc
            '_AffinityMask = 0
            _UserTime = .UserTime
            _KernelTime = .KernelTime
            Try
                _StartTime = Date.FromFileTime(.CreateTime).Ticks
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
            _MemoryInfos = .VirtualMemoryCounters
            _Priority = getPriorityClass(.BasePriority)
            _IOValues = .IoCounters
            _HandleCount = .HandleCount
            _Pid = .ProcessId
            _isHidden = False
            _threadCount = .NumberOfThreads
            _ParentProcessId = .InheritedFromProcessId
            If _Pid > 0 Then
                If ProcessName IsNot Nothing Then
                    _Name = ProcessName
                Else
                    _Name = Common.Misc.ReadUnicodeString(.ImageName)
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
            _isHidden = .IsHidden
            _threadCount = .ThreadCount

            ' Merge fixed info (in case of 'Reanalize')
            'If _Path <> ._Path OrElse _UserName <> .UserName OrElse _CommandLine <> .CommandLine Then
            If .HasReanalize Then
                _Path = .Path
                _UserName = .UserName
                _DomainName = .DomainName
                _CommandLine = .CommandLine
                _hasReanalize = False
                _fileInfo = .FileInfo
            End If
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared  Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(51) As String

        s(0) = "PID"
        s(1) = "UserName"
        s(2) = "ParentPID"
        s(3) = "CpuUsage"
        s(4) = "AverageCpuUsage"
        s(5) = "KernelCpuTime"
        s(6) = "UserCpuTime"
        s(7) = "TotalCpuTime"
        s(8) = "StartTime"
        s(9) = "RunTime"
        s(10) = "GdiObjects"
        s(11) = "UserObjects"
        s(12) = "AffinityMask"
        s(13) = "HandleCount"
        s(14) = "ThreadCount"
        s(15) = "WorkingSet"
        s(16) = "InJob"
        s(17) = "Elevation"
        s(18) = "BeingDebugged"
        s(19) = "OwnedProcess"
        s(20) = "SystemProcess"
        s(21) = "ServiceProcess"
        s(22) = "CriticalProcess"
        s(23) = "PeakWorkingSet"
        s(24) = "PageFaultCount"
        s(25) = "PagefileUsage"
        s(26) = "PeakPagefileUsage"
        s(27) = "QuotaPeakPagedPoolUsage"
        s(28) = "QuotaPagedPoolUsage"
        s(29) = "QuotaPeakNonPagedPoolUsage"
        s(30) = "QuotaNonPagedPoolUsage"
        s(31) = "ReadOperationCount"
        s(32) = "WriteOperationCount"
        s(33) = "OtherOperationCount"
        s(34) = "ReadTransferCount"
        s(35) = "WriteTransferCount"
        s(36) = "OtherTransferCount"
        s(37) = "ReadOperationCountDelta"
        s(38) = "WriteOperationCountDelta"
        s(39) = "OtherOperationCountDelta"
        s(40) = "ReadTransferCountDelta"
        s(41) = "WriteTransferCountDelta"
        s(42) = "OtherTransferCountDelta"
        s(43) = "TotalIoDelta"
        s(44) = "Priority"
        s(45) = "Path"
        s(46) = "CommandLine"
        s(47) = "Description"
        s(48) = "Copyright"
        s(49) = "Version"
        s(50) = "Company"
        s(51) = "IsWow64"


        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Name"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

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
