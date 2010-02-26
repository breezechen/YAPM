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

Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO

<Serializable()> _
Public Class cSnapshot242

#Region "Private attributes"

    ' List of processes
    Private _processes As New Dictionary(Of String, processInfos)

    ' List of services
    Private _services As New Dictionary(Of String, serviceInfos)

    ' List of network connections 
    Private _networkConnections As New Dictionary(Of String, networkInfos)

    ' List of jobs
    Private _jobs As New Dictionary(Of String, jobInfos)

    ' List of tasks
    Private _tasks As New Dictionary(Of String, windowInfos)

    ' List of modules (PID <-> Dico)
    Private _modules As New Dictionary(Of Integer, Dictionary(Of String, moduleInfos))

    ' List of windows (PID <-> Dico)
    Private _windows As New Dictionary(Of Integer, Dictionary(Of String, windowInfos))

    ' List of threads (PID <-> Dico)
    Private _threads As New Dictionary(Of Integer, Dictionary(Of String, threadInfos))

    ' List of privileges (PID <-> Dico)
    Private _privileges As New Dictionary(Of Integer, Dictionary(Of String, privilegeInfos))

    ' List of memory regions (PID <-> Dico)
    Private _memRegions As New Dictionary(Of Integer, Dictionary(Of String, memRegionInfos))

    ' List of job limits (jobName <-> Dico)
    Private _jobLimits As New Dictionary(Of String, Dictionary(Of String, jobLimitInfos))

    ' List of heaps (PID <-> Dico)
    Private _heaps As New Dictionary(Of Integer, Dictionary(Of String, heapInfos))

    ' List of envvariables (PID <-> Dico)
    Private _envV As New Dictionary(Of Integer, Dictionary(Of String, envVariableInfos))

    ' List of handles (PID <-> Dico)
    Private _handles As New Dictionary(Of Integer, Dictionary(Of String, handleInfos))

    ' Version of file type
    Private _fileVersion As String

    ' Date of snapshot
    Private _date As Date

    ' System infos
    Private _infos As SnapshotSystemInfo

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef ssFile As String)
        ' Not implemented
    End Sub

    Public Sub New()
        ' Empty snapshot file
    End Sub

    Public Overrides Function ToString() As String
        Return "Snapshot, computer : " & Me.SystemInformation.ComputerName & ", date : " & Me.Date.ToLongDateString & "-" & Me.Date.ToLongTimeString
    End Function

#End Region

#Region "Properties about the snapshot"

    ' File type version
    Public Property FileVersion() As String
        Get
            Return _fileVersion
        End Get
        Set(ByVal value As String)
            _fileVersion = value
        End Set
    End Property

    ' System info
    Public Property SystemInformation() As SnapshotSystemInfo
        Get
            Return _infos
        End Get
        Set(ByVal value As SnapshotSystemInfo)
            _infos = value
        End Set
    End Property

    ' File date
    Public Property [Date]() As Date
        Get
            Return _date
        End Get
        Set(ByVal value As Date)
            _date = value
        End Set
    End Property

#End Region

#Region "Other properties"

    ' List of processes
    Public Property Processes() As Dictionary(Of String, processInfos)
        Get
            Return _processes
        End Get
        Set(ByVal value As Dictionary(Of String, processInfos))
            _processes = value
        End Set
    End Property

    ' List of services
    Public Property Services() As Dictionary(Of String, serviceInfos)
        Get
            Return _services
        End Get
        Set(ByVal value As Dictionary(Of String, serviceInfos))
            _services = value
        End Set
    End Property
    Public ReadOnly Property ServicesByProcessId(ByVal processId As Integer) As Dictionary(Of String, serviceInfos)
        Get
            Dim ret As New Dictionary(Of String, serviceInfos)
            If Me.Services IsNot Nothing Then
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, serviceInfos) In Me.Services
                    If pair.Value.ProcessId = processId Then
                        ret.Add(pair.Key, pair.Value)
                    End If
                Next
            End If
            Return ret
        End Get
    End Property

    ' List of network connections
    Public Property NetworkConnections() As Dictionary(Of String, networkInfos)
        Get
            Return _networkConnections
        End Get
        Set(ByVal value As Dictionary(Of String, networkInfos))
            _networkConnections = value
        End Set
    End Property
    Public ReadOnly Property NetworkConnectionsByProcessId(ByVal processId As Integer) As Dictionary(Of String, networkInfos)
        Get
            Dim ret As New Dictionary(Of String, networkInfos)
            If Me.NetworkConnections IsNot Nothing Then
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, networkInfos) In Me.NetworkConnections
                    If pair.Value.ProcessId = processId Then
                        ret.Add(pair.Key, pair.Value)
                    End If
                Next
            End If
            Return ret
        End Get
    End Property

    ' List of jobs
    Public Property Jobs() As Dictionary(Of String, jobInfos)
        Get
            Return _jobs
        End Get
        Set(ByVal value As Dictionary(Of String, jobInfos))
            _jobs = value
        End Set
    End Property

    ' List of tasks
    Public Property Tasks() As Dictionary(Of String, windowInfos)
        Get
            Return _tasks
        End Get
        Set(ByVal value As Dictionary(Of String, windowInfos))
            _tasks = value
        End Set
    End Property

    ' List of modules
    Public Property ModulesByProcessId(ByVal processId As Integer) As Dictionary(Of String, moduleInfos)
        Get
            If _modules.ContainsKey(processId) Then
                Return _modules(processId)
            Else
                Return New Dictionary(Of String, moduleInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, moduleInfos))
            If _modules.ContainsKey(processId) Then
                _modules(processId) = value
            Else
                _modules.Add(processId, value)
            End If
        End Set
    End Property

    ' List of windows
    Public Property WindowsByProcessId(ByVal processId As Integer) As Dictionary(Of String, windowInfos)
        Get
            If _windows.ContainsKey(processId) Then
                Return _windows(processId)
            Else
                Return New Dictionary(Of String, windowInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, windowInfos))
            If _windows.ContainsKey(processId) Then
                _windows(processId) = value
            Else
                _windows.Add(processId, value)
            End If
        End Set
    End Property

    ' List of threads
    Public Property ThreadsByProcessId(ByVal processId As Integer) As Dictionary(Of String, threadInfos)
        Get
            If _threads.ContainsKey(processId) Then
                Return _threads(processId)
            Else
                Return New Dictionary(Of String, threadInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, threadInfos))
            If _threads.ContainsKey(processId) Then
                _threads(processId) = value
            Else
                _threads.Add(processId, value)
            End If
        End Set
    End Property

    ' List of privileges
    Public Property PrivilegesByProcessId(ByVal processId As Integer) As Dictionary(Of String, privilegeInfos)
        Get
            If _privileges.ContainsKey(processId) Then
                Return _privileges(processId)
            Else
                Return New Dictionary(Of String, privilegeInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, privilegeInfos))
            If _privileges.ContainsKey(processId) Then
                _privileges(processId) = value
            Else
                _privileges.Add(processId, value)
            End If
        End Set
    End Property

    ' List of mem regions
    Public Property MemoryRegionsByProcessId(ByVal processId As Integer) As Dictionary(Of String, memRegionInfos)
        Get
            If _memRegions.ContainsKey(processId) Then
                Return _memRegions(processId)
            Else
                Return New Dictionary(Of String, memRegionInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, memRegionInfos))
            If _memRegions.ContainsKey(processId) Then
                _memRegions(processId) = value
            Else
                _memRegions.Add(processId, value)
            End If
        End Set
    End Property

    ' List of job limits
    Public Property JobLimitsByJobName(ByVal jobName As String) As Dictionary(Of String, jobLimitInfos)
        Get
            If _jobLimits.ContainsKey(jobName) Then
                Return _jobLimits(jobName)
            Else
                Return New Dictionary(Of String, jobLimitInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, jobLimitInfos))
            If _jobLimits.ContainsKey(jobName) Then
                _jobLimits(jobName) = value
            Else
                _jobLimits.Add(jobName, value)
            End If
        End Set
    End Property

    ' List of heaps
    Public Property HeapsByProcessId(ByVal processId As Integer) As Dictionary(Of String, heapInfos)
        Get
            If _heaps.ContainsKey(processId) Then
                Return _heaps(processId)
            Else
                Return New Dictionary(Of String, heapInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, heapInfos))
            If _heaps.ContainsKey(processId) Then
                _heaps(processId) = value
            Else
                _heaps.Add(processId, value)
            End If
        End Set
    End Property

    ' List of handles
    Public Property HandlesByProcessId(ByVal processId As Integer) As Dictionary(Of String, handleInfos)
        Get
            If _handles.ContainsKey(processId) Then
                Return _handles(processId)
            Else
                Return New Dictionary(Of String, handleInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, handleInfos))
            If _handles.ContainsKey(processId) Then
                _handles(processId) = value
            Else
                _handles.Add(processId, value)
            End If
        End Set
    End Property

    ' List of env variables
    Public Property EnvironnementVariablesByProcessId(ByVal processId As Integer) As Dictionary(Of String, envVariableInfos)
        Get
            If _envV.ContainsKey(processId) Then
                Return _envV(processId)
            Else
                Return New Dictionary(Of String, envVariableInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, envVariableInfos))
            If _envV.ContainsKey(processId) Then
                _envV(processId) = value
            Else
                _envV.Add(processId, value)
            End If
        End Set
    End Property

#End Region

#Region "Private properties"

    ' List of modules
    Friend Property Modules() As Dictionary(Of Integer, Dictionary(Of String, moduleInfos))
        Get
            Return _modules
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, moduleInfos)))
            _modules = value
        End Set
    End Property

    ' List of windows
    Friend Property Windows() As Dictionary(Of Integer, Dictionary(Of String, windowInfos))
        Get
            Return _windows
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, windowInfos)))
            _windows = value
        End Set
    End Property

    ' List of threads
    Friend Property Threads() As Dictionary(Of Integer, Dictionary(Of String, threadInfos))
        Get
            Return _threads
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, threadInfos)))
            _threads = value
        End Set
    End Property

    ' List of privileges
    Friend Property Privileges() As Dictionary(Of Integer, Dictionary(Of String, privilegeInfos))
        Get
            Return _privileges
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, privilegeInfos)))
            _privileges = value
        End Set
    End Property

    ' List of mem regions
    Friend Property MemoryRegions() As Dictionary(Of Integer, Dictionary(Of String, memRegionInfos))
        Get
            Return _memRegions
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, memRegionInfos)))
            _memRegions = value
        End Set
    End Property

    ' List of mem regions
    Friend Property JobLimits() As Dictionary(Of String, Dictionary(Of String, jobLimitInfos))
        Get
            Return _jobLimits
        End Get
        Set(ByVal value As Dictionary(Of String, Dictionary(Of String, jobLimitInfos)))
            _jobLimits = value
        End Set
    End Property

    ' List of heaps
    Friend Property Heaps() As Dictionary(Of Integer, Dictionary(Of String, heapInfos))
        Get
            Return _heaps
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, heapInfos)))
            _heaps = value
        End Set
    End Property

    ' List of handles
    Friend Property [Handles]() As Dictionary(Of Integer, Dictionary(Of String, handleInfos))
        Get
            Return _handles
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, handleInfos)))
            _handles = value
        End Set
    End Property

    ' List of env variables
    Friend Property EnvironnementVariables() As Dictionary(Of Integer, Dictionary(Of String, envVariableInfos))
        Get
            Return _envV
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, envVariableInfos)))
            _envV = value
        End Set
    End Property

#End Region

#Region "All actions on snapshot"

    ' Save the snapshot as SSFile
    Public Function SaveSnapshot(ByVal ssFile As String, ByRef msg As String) As Boolean
        ' Not implemented
    End Function

    ' Create the snapshot
    Public Function CreateSnapshot(ByVal connection As cConnection, ByVal options As Native.Api.Enums.SnapshotObject, ByRef msg As String) As Boolean
        ' Not implemented
    End Function

#End Region

#Region "Private functions"

    ' Local building
    Private Function LocalBuild(ByRef msg As String, ByVal options As Native.Api.Enums.SnapshotObject) As Boolean
        ' Not implemented
    End Function

    ' WMI building
    Private Function WmiBuild(ByRef msg As String, ByVal options As Native.Api.Enums.SnapshotObject) As Boolean
        ' Not implemented   
    End Function

    ' Remote server building
    Private Function SocketBuild(ByRef msg As String, ByVal options As Native.Api.Enums.SnapshotObject) As Boolean
        ' Not implemented   
    End Function

#End Region

End Class