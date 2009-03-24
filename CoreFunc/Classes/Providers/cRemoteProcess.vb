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
' - GetAccountName function (conversion from SID to username as a string)
' - GetImageFile function, especially DeviceDriveNameToDosDriveName and
'   RefreshLogicalDrives which are inspired by functions from Process Hacker

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Management


Public Class cRemoteProcess
    Inherits cProcess

    ' ========================================
    ' API declarations
    ' ========================================

    Private Declare Function GetLastError Lib "kernel32" () As Integer
    Private Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
    Private Const LANG_NEUTRAL As Integer = &H0
    Private Const SUBLANG_DEFAULT As Integer = &H1
    Private Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Integer, _
    ByVal lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, _
    ByVal lpBuffer As String, ByVal nSize As Integer, ByVal Arguments As Integer) As Integer
    Public Shared Function GetError() As String
        Dim Buffer As String
        Buffer = Space$(1024)
        FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0, GetLastError, LANG_NEUTRAL, Buffer, Len(Buffer), 0)
        Return Trim$(Buffer)
    End Function

    ' All informations availables from WMI
    Public Enum WMI_INFO
        'Caption
        CommandLine
        'CreationClassName
        CreationDate
        'CSCreationClassName
        'CSName
        'Description
        ExecutablePath
        'ExecutionState
        'Handle
        HandleCount
        'InstallDate
        KernelModeTime
        MaximumWorkingSetSize
        MinimumWorkingSetSize
        'Name
        'OSCreationClassName
        'OSName
        OtherOperationCount
        OtherTransferCount
        PageFaults
        PageFileUsage
        ParentProcessId
        PeakPageFileUsage
        PeakVirtualSize
        PeakWorkingSetSize
        Priority
        PrivatePageCount
        ProcessId
        QuotaNonPagedPoolUsage
        QuotaPagedPoolUsage
        QuotaPeakNonPagedPoolUsage
        QuotaPeakPagedPoolUsage
        ReadOperationCount
        ReadTransferCount
        'SessionId
        'Status
        TerminationDate
        ThreadCount
        UserModeTime
        VirtualSize
        WindowsVersion
        WorkingSetSize
        WriteOperationCount
        WriteTransferCount
    End Enum


    ' Structure for remote connection
    Public Structure RemoteConnectionInfo
        Dim serverName As String
        Dim password As String
        Dim user As String
        Public Sub New(ByVal aServer As String, ByVal aPassword As String, ByVal aUser As String)
            serverName = aServer
            If Len(serverName) = 0 Or Len(aUser) = 0 Then
                ' Local
                password = Nothing
                user = Nothing
            Else
                ' Remote
                password = aPassword
                user = aUser
            End If
        End Sub
    End Structure




    ' ========================================
    ' Private attributes
    ' ========================================
    Private Shared _connection As RemoteConnectionInfo
    Private Shared _con As ConnectionOptions
    Private Shared _tempProcCol As ManagementObjectCollection

    Private _theProcess As ManagementObject
    Private _pid As Integer
    Private _parentPid As Integer
    Private _parentName As String
    Private _name As String
    Private _processors As Integer
    Private _commandLine As String
    Private _startTime As Date
    Private _path As String
    Private _userName As String


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal process As LightProcess, ByRef connection As RemoteConnectionInfo)
        MyBase.New()
        _pid = process.pid
        _name = process.name
        _parentPid = -1
        _connection = connection
        _con = New ConnectionOptions
        With _con
            .Username = connection.user
            .Password = connection.password
            .Impersonation = ImpersonationLevel.Impersonate
        End With
        _processors = cSystemInfo.GetProcessorCount

        ' Get _theProcess from the collection
        For Each refProcess As Management.ManagementObject In _tempProcCol
            Dim ID As Integer = CInt(refProcess.GetPropertyValue("ProcessId"))
            If ID = _pid Then
                _theProcess = refProcess
                Exit For
            End If
        Next
    End Sub



    ' ========================================
    ' Getter and setter
    ' ========================================   

    Public Overrides ReadOnly Property UserObjectsCount(Optional ByVal force As Boolean = False) As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property GDIObjectsCount(Optional ByVal force As Boolean = False) As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property CommandLine() As String
        Get
            If _commandLine = vbNullString Then
                _commandLine = CStr(Me.GetInformationFromWMICollection(WMI_INFO.CommandLine))
                If _commandLine = vbNullString Then
                    _commandLine = NO_INFO_RETRIEVED
                End If
            End If
            Return _commandLine
        End Get
    End Property
    Public Overrides Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
        Set(ByVal value As Integer)
            _processors = value
        End Set
    End Property
    Public Overrides ReadOnly Property Pid() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public Overrides Property AffinityMask() As Integer
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            '
        End Set
    End Property
    Public Overrides ReadOnly Property PEBAddress() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property ParentProcessId() As Integer
        Get
            If _parentPid = -1 Then
                _parentPid = CInt(Me.GetInformationFromWMICollection(WMI_INFO.ParentProcessId))
            End If
            Return _parentPid
        End Get
    End Property
    Public Overrides ReadOnly Property ParentProcessName() As String
        Get
            If _parentName = vbNullString Then
                If _procs.ContainsKey(Me.ParentProcessId.ToString) Then
                    _parentName = _procs.Item(Me.ParentProcessId.ToString)
                    If _parentName = vbNullString Then
                        _parentName = NO_INFO_RETRIEVED
                    End If
                Else
                    _parentName = NO_INFO_RETRIEVED
                End If
            End If
            Return _parentName
        End Get
    End Property
    Public Overrides ReadOnly Property GetIOvalues(Optional ByVal force As Boolean = False) As cProcess.PIO_COUNTERS
        Get
            Dim io As cProcess.PIO_COUNTERS
            With io
                .OtherOperationCount = CULng(Me.GetInformationFromWMICollection(WMI_INFO.OtherOperationCount))
                .OtherTransferCount = CULng(Me.GetInformationFromWMICollection(WMI_INFO.OtherTransferCount))
                .ReadOperationCount = CULng(Me.GetInformationFromWMICollection(WMI_INFO.ReadOperationCount))
                .ReadTransferCount = CULng(Me.GetInformationFromWMICollection(WMI_INFO.ReadTransferCount))
                .WriteOperationCount = CULng(Me.GetInformationFromWMICollection(WMI_INFO.WriteOperationCount))
                .WriteTransferCount = CULng(Me.GetInformationFromWMICollection(WMI_INFO.WriteTransferCount))
            End With
            Return io
        End Get
    End Property
    Public Overrides ReadOnly Property FileVersionInfo(Optional ByVal force As Boolean = False) As FileVersionInfo
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property Path() As String
        Get
            If _path = vbNullString Then
                _path = CStr(Me.GetInformationFromWMICollection(WMI_INFO.ExecutablePath))
                If _path = vbNullString Then
                    _path = NO_INFO_RETRIEVED
                End If
            End If
            Return _path
        End Get
    End Property
    Public Overrides ReadOnly Property UserName() As String
        Get
            If _userName = vbNullString Then
                Dim s1(1) As String
                _theProcess.InvokeMethod("GetOwner", s1)
                _userName = s1(1) & "\" & s1(0)
                If _userName = vbNullString Then
                    _userName = NO_INFO_RETRIEVED
                End If
            End If
            Return _userName
        End Get
    End Property
    Public Overrides ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public Overrides ReadOnly Property ProcessorTime(Optional ByVal force As Boolean = False) As Date
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property ProcessorTimeLong() As Long
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property KernelTime(Optional ByVal force As Boolean = False) As Date
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property UserTime(Optional ByVal force As Boolean = False) As Date
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property MemoryInfos(Optional ByVal force As Boolean = False) As PROCESS_MEMORY_COUNTERS
        Get
            Dim mem As cProcess.PROCESS_MEMORY_COUNTERS
            With mem
                .PageFaultCount = CInt(Me.GetInformationFromWMICollection(WMI_INFO.PageFaults))
                .PagefileUsage = CInt(Me.GetInformationFromWMICollection(WMI_INFO.PageFileUsage))
                .PeakPagefileUsage = CInt(Me.GetInformationFromWMICollection(WMI_INFO.PeakPageFileUsage))
                .PeakWorkingSetSize = CInt(Me.GetInformationFromWMICollection(WMI_INFO.PeakWorkingSetSize))
                .QuotaNonPagedPoolUsage = CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaNonPagedPoolUsage))
                .QuotaPagedPoolUsage = CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaPagedPoolUsage))
                .QuotaPeakNonPagedPoolUsage = CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaPeakNonPagedPoolUsage))
                .QuotaPeakPagedPoolUsage = CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaPeakPagedPoolUsage))
                .WorkingSetSize = CInt(Me.GetInformationFromWMICollection(WMI_INFO.WorkingSetSize))
            End With
            Return mem
        End Get
    End Property
    Public Overrides ReadOnly Property Threads() As System.Diagnostics.ProcessThreadCollection
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property PriorityClass() As String
        Get
            Return ""
        End Get
    End Property
    Public Overrides ReadOnly Property PriorityClassInt() As Integer
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property PriorityLevel() As Integer
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property PriorityClassConstant() As ProcessPriorityClass
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property AverageCpuUsage(Optional ByVal force As Boolean = False) As Double
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property CpuPercentageUsage(Optional ByVal force As Boolean = False) As Double
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property HandleCount() As Integer
        Get
            Return CInt(Me.GetInformationFromWMICollection(WMI_INFO.HandleCount))
        End Get
    End Property
    Public Overrides ReadOnly Property StartTime() As Date
        Get
            If _startTime = Nothing Then
                '_startTime = New Date(CLng(Me.GetInformationFromWMICollection(WMI_INFO.CreationDate)))
            End If
            Return _startTime
        End Get
    End Property
    Public Overrides ReadOnly Property MainModule() As System.Diagnostics.ProcessModule
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property Modules() As System.Diagnostics.ProcessModuleCollection
        Get
            Return Nothing
        End Get
    End Property





    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Retrieve a long array with all available values from dictionnaries
    Public Overrides Function GetHistory(ByVal infoName As String) As Double()
        Return Nothing
    End Function


    ' NON IMPLEMENTED FUNCTIONS
    Public Overrides Function GetInformationNumerical(ByVal infoName As String) As Double
        '
    End Function
    Public Overrides Function SuspendProcess() As Integer
        '
    End Function
    Public Overrides Function ResumeProcess() As Integer
        '
    End Function
    Public Overrides Function KillProcessTree() As Integer
        '
    End Function
    Public Overrides Function EmptyWorkingSetSize() As Integer
        '
    End Function
    Public Overrides Function GetEnvironmentVariables(ByRef variables() As String, ByRef values() As String) As Integer
        '
    End Function


    Public Overrides Sub Refresh(Optional ByRef tag As Dictionary(Of String, System.Management.ManagementObject) = Nothing)
        ' Refresh _theProcess from the dictionnary we got before
        If tag.ContainsKey(_pid.ToString) Then
            _theProcess = tag.Item(_pid.ToString)
        End If

        MyBase.Refresh()
    End Sub

    ' Set priority
    Public Overrides Function SetProcessPriority(ByVal level As ProcessPriorityClass) As Integer
        Dim i As Integer = 0
        '_theProcess.InvokeMethod("Terminate", Nothing)
    End Function

    ' Kill a process
    Public Overrides Function Kill() As Integer
        _theProcess.InvokeMethod("Terminate", Nothing)
    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal infoName As String) As String
        Dim res As String = NO_INFO_RETRIEVED
        Dim io As cProcess.PIO_COUNTERS = Me.GetIOvalues
        Select Case infoName
            Case "ParentPID"
                res = CStr(Me.ParentProcessId)
            Case "ParentName"
                res = Me.ParentProcessName
            Case "PID"
                res = CStr(Me.Pid)
            Case "UserName"
                res = Me.UserName
            Case "CpuUsage"
                '
            Case "KernelCpuTime"
                Dim ts As Date = Me.KernelTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "UserCpuTime"
                Dim ts As Date = Me.UserTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "TotalCpuTime"
                Dim ts As Date = Me.ProcessorTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "StartTime"
                Dim ts As Date = Me.StartTime
                res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
            Case "WorkingSet"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.WorkingSetSize)))
            Case "PeakWorkingSet"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.PeakWorkingSetSize)))
            Case "PageFaultCount"
                res = CInt(Me.GetInformationFromWMICollection(WMI_INFO.PageFaults)).ToString
            Case "PagefileUsage"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.PageFileUsage)))
            Case "PeakPagefileUsage"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.PeakPageFileUsage)))
            Case "QuotaPeakPagedPoolUsage"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaPeakPagedPoolUsage)))
            Case "QuotaPagedPoolUsage"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaPagedPoolUsage)))
            Case "QuotaPeakNonPagedPoolUsage"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaPeakNonPagedPoolUsage)))
            Case "QuotaNonPagedPoolUsage"
                res = GetFormatedSize(CInt(Me.GetInformationFromWMICollection(WMI_INFO.QuotaNonPagedPoolUsage)))
            Case "Priority"
                res = Me.PriorityClass.ToString
            Case "Path"
                res = Me.Path
            Case "Description"
                '
            Case "Copyright"
                '
            Case "Version"
                '
            Case "Name"
                res = Me.Name
            Case "GdiObjects"
                '
            Case "UserObjects"
                '
            Case "RunTime"
                Dim ts As New Date(Date.Now.Ticks - Me.StartTime.Ticks)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "AffinityMask"
                '
            Case "AverageCpuUsage"
                '
            Case "CommandLine"
                res = Me.CommandLine
            Case "ReadOperationCount"
                res = io.ReadOperationCount.ToString
            Case "WriteOperationCount"
                res = io.WriteOperationCount.ToString
            Case "OtherOperationCount"
                res = io.OtherOperationCount.ToString
            Case "ReadTransferCount "
                res = GetFormatedSize(io.ReadTransferCount)
            Case "WriteTransferCount"
                res = GetFormatedSize(io.WriteTransferCount)
            Case "OtherTransferCount"
                res = GetFormatedSize(io.OtherTransferCount)
            Case "HandleCount"
                res = Me.HandleCount.ToString
        End Select

        Return res
    End Function


    ' ========================================
    ' Shared functions
    ' ========================================

    Public Shared Shadows Function Enumerate(ByRef _remoteCon As cRemoteProcess.RemoteConnectionInfo, ByRef key() As String, ByRef _dico As Dictionary(Of String, LightProcess), ByRef _dicoMng As Dictionary(Of String, Management.ManagementObject)) As Integer

        Dim colProcesses As Management.ManagementObjectSearcher

        ' Launch request
        colProcesses = New Management.ManagementObjectSearcher("SELECT * FROM Win32_Process")
        colProcesses.Scope = New Management.ManagementScope("\\" & _remoteCon.serverName & "\root\cimv2", _con)

        ' Save current collection
        Dim res As ManagementObjectCollection = colProcesses.Get
        _tempProcCol = res

        ' Create dictionnary
        _dico.Clear()
        _dicoMng.Clear()
        Dim refProcess As Management.ManagementObject
        ReDim key(res.Count - 1)
        Dim x As Integer = 0
        For Each refProcess In res
            Dim ID As Integer = CInt(refProcess.GetPropertyValue("ProcessId"))
            Dim NAME As String = CStr(refProcess.GetPropertyValue("Name"))
            _dico.Add(ID.ToString, New LightProcess(ID, NAME))
            _dicoMng.Add(ID.ToString, refProcess)
            key(x) = ID.ToString
            x += 1
        Next

    End Function

    ' New process
    Public Shared Sub StartNewProcess(ByRef connectionOpt As cRemoteProcess.RemoteConnectionInfo, ByVal processPath As String)
        Dim connOptions As ConnectionOptions = New ConnectionOptions()
        connOptions.Impersonation = ImpersonationLevel.Impersonate
        connOptions.Username = connectionOpt.user
        connOptions.Password = connectionOpt.password
        connOptions.EnablePrivileges = True
        Dim manScope As ManagementScope = New ManagementScope([String].Format("\\{0}\ROOT\CIMV2", connectionOpt.serverName), connOptions)
        manScope.Connect()
        Dim objectGetOptions As New ObjectGetOptions()
        Dim managementPath As New ManagementPath("Win32_Process")
        Dim processClass As New ManagementClass(manScope, managementPath, objectGetOptions)
        Dim inParams As ManagementBaseObject = processClass.GetMethodParameters("Create")
        inParams("CommandLine") = processPath
        Dim outParams As ManagementBaseObject = processClass.InvokeMethod("Create", inParams, Nothing)
        'Console.WriteLine("Creation of the process returned: " & outParams("returnValue").ToString)
        'Console.WriteLine("Process ID: " & outParams("processId").ToString)
    End Sub



    ' ========================================
    ' Private functions of this class
    ' ========================================
    Private Function GetInformationFromWMICollection(ByVal infoName As WMI_INFO) As Object
        Return _theProcess.GetPropertyValue(infoName.ToString)
    End Function

End Class
