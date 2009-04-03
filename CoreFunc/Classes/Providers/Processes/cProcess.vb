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

Public Class cProcess
    Inherits cGeneralObject

    ' Contains list of process names
    Friend Shared _procs As New Dictionary(Of String, String)

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _infos As API.SYSTEM_PROCESS_INFORMATION
    Private _processInfos As processInfos
    Private _processors As Integer = 1       ' By default we consider that there is only one processor
    Private Shared WithEvents _connection As cProcessConnection

    Private _parentName As String = vbNullString

    Private _handleQueryInfo As Integer


#Region "Properties"

    Public Shared Property Connection() As cProcessConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cProcessConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As processInfos)
        _processInfos = infos
        _connection = Connection
        _processors = cProcessConnection.ProcessorCount
        ' Get a handle if local
        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            _handleQueryInfo = API.OpenProcess(API.PROCESS_QUERY_INFORMATION, 0, infos.Pid)
        End If
    End Sub

    Protected Overrides Sub Finalize()
        ' Close a handle if local
        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            If _handleQueryInfo > 0 Then
                API.CloseHandle(_handleQueryInfo)
            End If
        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As processInfos
        Get
            Return _processInfos
        End Get
    End Property

    Public ReadOnly Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
    End Property

    ' Different kind of property, because it's changed by the call
    ' of aVariable = aProcess.CpuUsage
    Public ReadOnly Property CpuUsage() As Double
        Get
            Static oldDate As Long = Date.Now.Ticks
            Static oldProcTime As Long = Me.Infos.ProcessorTime

            Dim currDate As Long = Date.Now.Ticks
            Dim proctime As Long = Me.Infos.ProcessorTime

            Dim diff As Long = currDate - oldDate
            Dim procDiff As Long = proctime - oldProcTime

            oldProcTime = proctime
            oldDate = currDate

            If diff > 0 AndAlso _processors > 0 Then
                Return procDiff / diff / _processors
            Else
                Return 0
            End If
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Proc As processInfos)
        _processInfos.Merge(Proc)
        Call RefreshSpecialInformations()
    End Sub
    Public Sub Merge(ByRef Proc As API.SYSTEM_PROCESS_INFORMATION)
        _processInfos.Merge(New processInfos(Proc))
        Call RefreshSpecialInformations()
    End Sub

#Region "Special informations (GDI, affinity) and special refresh"

    ' Refresh some non fixed infos
    ' For now IT IS NOT ASYNC
    ' Because create ~50 threads/sec is not really cool
    Private WithEvents asyncNonFixed As asyncCallbackProcGetNonFixedInfos
    Private Sub RefreshSpecialInformations()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                If asyncNonFixed Is Nothing Then
                    asyncNonFixed = New asyncCallbackProcGetNonFixedInfos(Me.Infos.Pid, _connection, _handleQueryInfo)
                End If
                asyncNonFixed.Process()
        End Select
    End Sub
    Private Sub nonFixedInfosGathered(ByVal infos As asyncCallbackProcGetNonFixedInfos.TheseInfos) Handles asyncNonFixed.GatheredInfos
        Me.Infos.UserObjects = infos.userO
        Me.Infos.GdiObjects = infos.gdiO
        Me.Infos.AffinityMask = infos.affinity
    End Sub

#End Region

#Region "All actions on process (kill, enum...)"

    ' Set priority
    Private WithEvents asyncSetPriority As asyncCallbackProcSetPriority
    Public Function SetPriority(ByVal level As ProcessPriorityClass) As Integer
        asyncSetPriority = New asyncCallbackProcSetPriority(Me.Infos.Pid, level, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "SetPriority"
        t.Start()
    End Function
    Private Sub setPriorityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncSetPriority.HasSetPriority
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    ' Kill a process
    Private WithEvents asyncKill As asyncCallbackProcKill
    Public Function Kill() As Integer
        asyncKill = New asyncCallbackProcKill(Me.Infos.Pid, _connection)
        Dim t As New Threading.Thread(AddressOf asyncKill.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "Kill"
        t.Start()
    End Function
    Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String) Handles asyncKill.HasKilled
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    ' Decrease priority
    Private WithEvents asyncDecPriority As asyncCallbackProcDecreasePriority
    Public Function DecreasePriority() As Integer
        asyncDecPriority = New asyncCallbackProcDecreasePriority(Me.Infos.Pid, Me.Infos.Priority, _connection)
        Dim t As New Threading.Thread(AddressOf asyncDecPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "DecreasePriority"
        t.Start()
    End Function
    Private Sub decreasePriorityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncDecPriority.HasDecreasedPriority
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    ' Increase priority
    Private WithEvents asyncInPriority As asyncCallbackProcIncreasePriority
    Public Function IncreasePriority() As Integer
        asyncInPriority = New asyncCallbackProcIncreasePriority(Me.Infos.Pid, Me.Infos.Priority, _connection)
        Dim t As New Threading.Thread(AddressOf asyncInPriority.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "IncreasePriority"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub increasePriorityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncInPriority.HasIncreasedPriority
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    ' Suspend a process
    Private WithEvents asyncSuspend As asyncCallbackProcSuspend
    Public Function SuspendProcess() As Integer
        asyncSuspend = New asyncCallbackProcSuspend(Me.Infos.Pid, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSuspend.Process)
        t.Name = "SuspendProcess"
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub suspendDone(ByVal success As Boolean, ByVal msg As String) Handles asyncSuspend.HasSuspended
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not suspend process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    ' Resume a process
    Private WithEvents asyncResume As asyncCallbackProcResume
    Public Function ResumeProcess() As Integer
        asyncResume = New asyncCallbackProcResume(Me.Infos.Pid, _connection)
        Dim t As New Threading.Thread(AddressOf asyncResume.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "ResumeProcess"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub resumeDone(ByVal success As Boolean, ByVal msg As String) Handles asyncResume.HasResumed
        If success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    ' Kill a process tree
    Private WithEvents asyncRecursiveKill As asyncCallbackProcKillTree
    Public Function KillProcessTree() As Integer
        asyncRecursiveKill = New asyncCallbackProcKillTree(Me.Infos.Pid, _connection)
        Dim t As New Threading.Thread(AddressOf asyncRecursiveKill.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "KillProcessTree"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub recursiveKillDone(ByVal success As Boolean, ByVal msg As String) Handles asyncRecursiveKill.HasKilled
        If success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    '' Empty working set size
    Private WithEvents asyncEmptyWorkingSetSize As asyncCallbackProcEmptyWorkingSet
    Public Function EmptyWorkingSetSize() As Integer
        asyncEmptyWorkingSetSize = New asyncCallbackProcEmptyWorkingSet(Me.Infos.Pid, _connection)
        Dim t As New Threading.Thread(AddressOf asyncEmptyWorkingSetSize.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.Name = "EmptyWorkingSetSize"
        t.IsBackground = True
        t.Start()
    End Function
    Private Sub emptyWorkingSetSizeDone(ByVal success As Boolean, ByVal msg As String) Handles asyncEmptyWorkingSetSize.HasReducedWorkingSet
        If success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not empty working set" & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

    '' Get env variables
    'Public Function GetEnvironmentVariables(ByRef variables() As String, _
    '                                        ByRef values() As String) As Integer
    '    Dim t As New Threading.Thread(AddressOf asyncCallbackGetEnvironmentVariables)
    '    t.Priority = Threading.ThreadPriority.Lowest
    '    t.IsBackground = True
    '    t.Start()
    'End Function

    '' Unload a module
    'Public Function UnloadModule(ByVal baseAddress As Integer) As Integer
    '    Dim t As New Threading.Thread(AddressOf asyncCallbackProcUnloadModule)
    '    t.Priority = Threading.ThreadPriority.Lowest
    '    t.IsBackground = True
    '    t.Start()
    'End Function

    ' Change affinity
    Private WithEvents asyncSetAffinity As asyncCallbackProcSetAffinity
    Public Function SetAffinity(ByVal affinity As Integer) As Integer
        asyncSetAffinity = New asyncCallbackProcSetAffinity(Me.Infos.Pid, affinity, _connection)
        Dim t As New Threading.Thread(AddressOf asyncSetAffinity.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "SetAffinity"
        t.Start()
    End Function
    Private Sub setAffinityDone(ByVal success As Boolean, ByVal msg As String) Handles asyncSetAffinity.HasSetAffinity
        If success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set affinity " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
    End Sub

#End Region

#Region "Get information overriden methods"
    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal infoName As String) As String
        Dim res As String = NO_INFO_RETRIEVED
        Select Case infoName
            Case "ParentPID"
                res = Me.Infos.ParentProcessId.ToString
            Case "ParentName"
                If _parentName = vbNullString Then
                    Dim _pi As Integer = Me.Infos.ParentProcessId
                    If _pi > 4 Then
                        _parentName = GetProcessName(Me.Infos.ParentProcessId)
                        If Len(_parentName) = 0 Then
                            _parentName = "[Parent killed]"
                        End If
                    ElseIf _pi = 4 Then
                        _parentName = "Idle process"
                    Else
                        _parentName = NO_INFO_RETRIEVED
                    End If
                End If
                res = _parentName
            Case "PID"
                res = Me.Infos.Pid.ToString
            Case "UserName"
                res = Me.Infos.UserName
            Case "CpuUsage"
                res = GetFormatedPercentage(Me.CpuUsage)
            Case "KernelCpuTime"
                Dim ts As Date = New Date(Me.Infos.KernelTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "UserCpuTime"
                Dim ts As Date = New Date(Me.Infos.UserTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "TotalCpuTime"
                Dim ts As Date = New Date(Me.Infos.ProcessorTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "StartTime"
                Dim ts As Date = New Date(Me.Infos.StartTime)
                res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
            Case "WorkingSet"
                res = GetFormatedSize(Me.Infos.MemoryInfos.WorkingSetSize)
            Case "PeakWorkingSet"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PeakWorkingSetSize)
            Case "PageFaultCount"
                res = Me.Infos.MemoryInfos.PageFaultCount.ToString
            Case "PagefileUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PagefileUsage)
            Case "PeakPagefileUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PeakPagefileUsage)
            Case "QuotaPeakPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPeakPagedPoolUsage)
            Case "QuotaPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPagedPoolUsage)
            Case "QuotaPeakNonPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPeakNonPagedPoolUsage)
            Case "QuotaNonPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaNonPagedPoolUsage)
            Case "Priority"
                res = Me.Infos.Priority.ToString
            Case "Path"
                res = Me.Infos.Path
            Case "Description"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileDescription
                End If
            Case "Copyright"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.LegalCopyright
                End If
            Case "Version"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileVersion
                End If
            Case "Name"
                res = Me.Infos.Name
            Case "GdiObjects"
                res = Me.Infos.GdiObjects.ToString
            Case "UserObjects"
                res = Me.Infos.UserObjects.ToString
            Case "RunTime"
                Dim ts As New Date(Date.Now.Ticks - Me.Infos.StartTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "AffinityMask"
                res = Me.Infos.AffinityMask.ToString
            Case "AverageCpuUsage"
                Dim i As Long = Date.Now.Ticks - Me.Infos.StartTime
                If i > 0 AndAlso _processors > 0 Then
                    res = GetFormatedPercentage(Me.Infos.ProcessorTime / i / _processors)
                Else
                    res = GetFormatedPercentage(0)
                End If
            Case "CommandLine"
                res = Me.Infos.CommandLine
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount.ToString
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount.ToString
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount.ToString
            Case "ReadTransferCount "
                res = GetFormatedSize(Me.Infos.IOValues.ReadTransferCount)
            Case "WriteTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.WriteTransferCount)
            Case "OtherTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.OtherTransferCount)
            Case "HandleCount"
                res = Me.Infos.HandleCount.ToString
            Case "ThreadCount"
                res = Me.Infos.ThreadCount.ToString
        End Select

        Return res
    End Function

    ' Retrieve informations by its name (numerical)
    Public Function GetInformationNumerical(ByVal infoName As String) As Double
        Dim res As Double = 0

        Select Case infoName
            Case "KernelCpuTime"
                res = Me.Infos.KernelTime
            Case "UserCpuTime"
                res = Me.Infos.UserTime
            Case "TotalCpuTime"
                res = Me.Infos.ProcessorTime
            Case "WorkingSet"
                res = Me.Infos.MemoryInfos.WorkingSetSize
            Case "PeakWorkingSet"
                res = Me.Infos.MemoryInfos.PeakWorkingSetSize
            Case "PageFaultCount"
                res = Me.Infos.MemoryInfos.PageFaultCount
            Case "PagefileUsage"
                res = Me.Infos.MemoryInfos.PagefileUsage
            Case "PeakPagefileUsage"
                res = Me.Infos.MemoryInfos.PeakPagefileUsage
            Case "QuotaPeakPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaPeakPagedPoolUsage
            Case "QuotaPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaPagedPoolUsage
            Case "QuotaPeakNonPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaPeakNonPagedPoolUsage
            Case "QuotaNonPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaNonPagedPoolUsage
            Case "GdiObjects"
                '
            Case "UserObjects"
                '
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount
            Case "ReadTransferCount "
                res = Me.Infos.IOValues.ReadTransferCount
            Case "WriteTransferCount"
                res = Me.Infos.IOValues.WriteTransferCount
            Case "OtherTransferCount"
                res = Me.Infos.IOValues.OtherTransferCount
            Case "CpuUsage"
                '
            Case "AverageCpuUsage"
                res = 100 * Me.Infos.AverageCpuUsage
            Case "HandleCount"
                res = Me.Infos.HandleCount
        End Select

        Return res
    End Function

#End Region

#Region "Shared function"

    ' Return path
    Public Shared Function GetPath(ByVal pid As Integer) As String
        Return asyncCallbackProcEnumerate.GetImageFile(pid)
    End Function

    ' Return Process name
    Public Shared Function GetProcessName(ByVal pid As Integer) As String
        Select Case pid
            Case 0
                Return "[System Process]"
            Case 4
                Return "System"
            Case Else
                If _procs.ContainsKey(pid.ToString) Then
                    Return _procs.Item(pid.ToString)
                Else
                    Return cFile.GetFileName(asyncCallbackProcEnumerate.GetImageFile(pid))
                End If
        End Select
    End Function

    ' Kill a process
    Private Shared WithEvents asyncKillShared As asyncCallbackProcKill
    Public Shared Function Kill(ByVal pid As Integer) As Integer
        asyncKillShared = New asyncCallbackProcKill(pid, _connection)
        Dim t As New Threading.Thread(AddressOf asyncKillShared.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "Kill"
        t.Start()
    End Function
    Private Shared Sub killDoneShared(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String) Handles asyncKillShared.HasKilled
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill process " & pid.ToString)
        End If
    End Sub

    ' Start a process
    Private Shared WithEvents asyncStartShared As asyncCallbackProcNewProcess
    Public Shared Function StartNewProcess(ByVal path As String) As Integer
        asyncStartShared = New asyncCallbackProcNewProcess(path, _connection)
        Dim t As New Threading.Thread(AddressOf asyncStartShared.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "StartNewProcess"
        t.Start()
    End Function
    Private Shared Sub newProcessDoneShared(ByVal Success As Boolean, ByVal path As String, ByVal msg As String) Handles asyncStartShared.HasCreated
        If Success = False Then
            MsgBox("Error : cannot start process : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not start new process " & path)
        End If
    End Sub


    ' Unload a module from a process
    Private Shared WithEvents asyncUnloadModuleShared As asyncCallbackProcUnloadModule
    Public Shared Function UnLoadModuleFromProcess(ByRef aModule As cModule.MODULEENTRY32) As Integer
        Return UnLoadModuleFromProcess(aModule.th32ProcessID, aModule.modBaseAddr)
    End Function
    Public Shared Function UnLoadModuleFromProcess(ByVal pid As Integer, ByVal ModuleBaseAddress As Integer) As Integer
        asyncUnloadModuleShared = New asyncCallbackProcUnloadModule(pid, ModuleBaseAddress, _connection)
        Dim t As New Threading.Thread(AddressOf asyncUnloadModuleShared.Process)
        t.Priority = Threading.ThreadPriority.Lowest
        t.IsBackground = True
        t.Name = "UnloadModule"
        t.Start()
    End Function
    Private Shared Sub unloadModuleDoneShared(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String) Handles asyncUnloadModuleShared.HasUnloadedModule
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not unload the module from process " & pid.ToString)
        End If
    End Sub

    ' Clear process dico
    Public Shared Sub ClearProcessDico()
        _procs.Clear()
    End Sub

    ' Add/remove a process to dictionnary
    Public Shared Sub AssociatePidAndName(ByVal pid As String, ByVal name As String)
        If _procs.ContainsKey(pid) = False Then
            _procs.Add(pid, name)
        End If
    End Sub
    Public Shared Sub UnAssociatePidAndName(ByVal pid As String)
        _procs.Remove(pid)
    End Sub

#End Region

End Class
