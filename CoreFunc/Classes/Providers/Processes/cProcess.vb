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

Public Class cProcess
    Inherits cGeneralObject

#Region "History structure"

    Public Structure PROC_TIME_INFO
        Dim time As Long
        Dim kernel As Long
        Dim user As Long
        Dim total As Long
        Public Sub New(ByVal aTime As Long, ByVal aUser As Long, ByVal aKernel As Long)
            time = aTime
            kernel = aKernel
            user = aUser
            total = user + kernel
        End Sub
    End Structure

    Public Structure PROC_MEM_INFO
        Dim time As Long
        Dim mem As API.VM_COUNTERS_EX
        Public Sub New(ByVal aTime As Long, ByRef aMem As API.VM_COUNTERS_EX)
            time = aTime
            mem = aMem
        End Sub
    End Structure

    Public Structure PROC_IO_INFO
        Dim time As Long
        Dim io As API.IO_COUNTERS
        Public Sub New(ByVal aTime As Long, ByRef aIo As API.IO_COUNTERS)
            time = aTime
            io = aIo
        End Sub
    End Structure

    Public Structure PROC_MISC_INFO
        Dim time As Long
        Dim gdiO As Integer
        Dim userO As Integer
        Dim cpuUsage As Double
        Dim averageCpuUsage As Double
        Public Sub New(ByVal aTime As Long, ByVal aGdi As Integer, ByVal aUser As _
                       Integer, ByVal aCpu As Double, ByVal aAverage As Double)
            time = aTime
            gdiO = aGdi
            userO = aUser
            cpuUsage = aCpu
            averageCpuUsage = aAverage
        End Sub
    End Structure

    Private Const HIST_SIZE As Integer = 226                ' Size of an item of the history
    Private Shared _histBuff As Integer = 1000              ' Number of items to save

    Public Shared WriteOnly Property BuffSize() As Integer
        Set(ByVal value As Integer)
            _histBuff = CInt(value / HIST_SIZE)
        End Set
    End Property

#End Region

    Public Event HasMerged()

    ' Contains list of process names
    Friend Shared _procs As New Dictionary(Of String, String)

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    ' Current processes running
    ' Protected by a semaphore (see property associated below, _semCurrentProcesses)
    Public Shared _currentProcesses As Dictionary(Of String, cProcess)

    Private _infos As API.SYSTEM_PROCESS_INFORMATION
    Private _processInfos As processInfos
    Private _processors As Integer = 1       ' By default we consider that there is only one processor
    Private Shared WithEvents _connection As cProcessConnection

    Private _parentName As String = vbNullString
    Private _cpuUsage As Double
    Private _ioDelta As API.IO_COUNTERS

    ' Save informations about performance
    Friend _dicoProcMem As New SortedList(Of Integer, PROC_MEM_INFO)
    Friend _dicoProcTimes As New SortedList(Of Integer, PROC_TIME_INFO)
    Friend _dicoProcIO As New SortedList(Of Integer, PROC_IO_INFO)
    Friend _dicoProcMisc As New SortedList(Of Integer, PROC_MISC_INFO)
    Friend _dicoProcIODelta As New SortedList(Of Integer, PROC_IO_INFO)

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

    ' Get the performance dictionnaries
    Public ReadOnly Property DicoPerfMem() As SortedList(Of Integer, PROC_MEM_INFO)
        Get
            Return _dicoProcMem
        End Get
    End Property
    Public ReadOnly Property DicoPerfIO() As SortedList(Of Integer, PROC_IO_INFO)
        Get
            Return _dicoProcIO
        End Get
    End Property
    Public ReadOnly Property DicoPerfIODelta() As SortedList(Of Integer, PROC_IO_INFO)
        Get
            Return _dicoProcIODelta
        End Get
    End Property
    Public ReadOnly Property DicoPerfTimes() As SortedList(Of Integer, PROC_TIME_INFO)
        Get
            Return _dicoProcTimes
        End Get
    End Property

    Private Shared _semCurrentProcesses As New System.Threading.Semaphore(1, 1)
    Public Shared Property CurrentProcesses() As Dictionary(Of String, cProcess)
        Get
            Return _currentProcesses
        End Get
        Set(ByVal value As Dictionary(Of String, cProcess))
            _currentProcesses = value
        End Set
    End Property
    Public Shared ReadOnly Property SemCurrentProcesses() As System.Threading.Semaphore
        Get
            Return _semCurrentProcesses
        End Get
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As processInfos)
        _processInfos = infos
        _connection = Connection
        _processors = cProcessConnection.ProcessorCount
        ' Get a handle if local
        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            _handleQueryInfo = API.OpenProcess(cProcessConnection.ProcessMinRights, 0, infos.Pid)
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
            Return _cpuUsage
        End Get
    End Property

    Public ReadOnly Property IODelta() As API.IO_COUNTERS
        Get
            Return _ioDelta
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Private _refrehNumber As Integer = 0

    Public Sub ClearHistory()
        SyncLock _dicoProcIO
            _dicoProcMem.Clear()
            _dicoProcTimes.Clear()
            _dicoProcIO.Clear()
            _dicoProcIODelta.Clear()
            _dicoProcMisc.Clear()
            _refrehNumber = 0
        End SyncLock
    End Sub

    Public Sub Merge(ByRef Proc As processInfos)

        Call refreshCpuUsage()
        Call refreshIOdelta()

        _refrehNumber += 1   ' This is the key for the history

        ' Get date in ms
        Dim _now As Long = Date.Now.Ticks

        _processInfos.Merge(Proc)
        Call RefreshSpecialInformations()

        ' Remove items from history if buffer is full
        Dim d As Integer = _refrehNumber - _histBuff
        If _histBuff > 0 AndAlso d > 0 Then
            _dicoProcMem.Remove(d)
            _dicoProcTimes.Remove(d)
            _dicoProcIO.Remove(d)
            _dicoProcIODelta.Remove(d)
            _dicoProcMisc.Remove(d)
        End If

        ' Store history
        SyncLock _dicoProcIO
            _dicoProcMem.Add(_refrehNumber, New PROC_MEM_INFO(_now, Me.Infos.MemoryInfos))
            _dicoProcTimes.Add(_refrehNumber, New PROC_TIME_INFO(_now, Me.Infos.UserTime, Me.Infos.KernelTime))
            _dicoProcIO.Add(_refrehNumber, New PROC_IO_INFO(_now, Me.Infos.IOValues))
            _dicoProcIODelta.Add(_refrehNumber, New PROC_IO_INFO(_now, _ioDelta))
            _dicoProcMisc.Add(_refrehNumber, New PROC_MISC_INFO(_now, Me.Infos.GdiObjects, Me.Infos.UserObjects, _
                     100 * Me.CpuUsage, 100 * Me.Infos.AverageCpuUsage))
        End SyncLock

        RaiseEvent HasMerged()
    End Sub
    Public Sub Merge(ByRef Proc As API.SYSTEM_PROCESS_INFORMATION)
        _processInfos.Merge(New processInfos(Proc))
        Call RefreshSpecialInformations()
    End Sub

#Region "Special informations (GDI, affinity)"

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
    Private _setPriorityP As asyncCallbackProcSetPriority
    Public Function SetPriority(ByVal level As ProcessPriorityClass) As Integer

        If _setPriorityP Is Nothing Then
            _setPriorityP = New asyncCallbackProcSetPriority(New asyncCallbackProcSetPriority.HasSetPriority(AddressOf setPriorityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setPriorityP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcSetPriority.poolObj(Me.Infos.Pid, level, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub setPriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Kill a process
    Private _killP As asyncCallbackProcKill
    Public Function Kill() As Integer

        If _killP Is Nothing Then
            _killP = New asyncCallbackProcKill(New asyncCallbackProcKill.HasKilled(AddressOf killDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcKill.poolObj(Me.Infos.Pid, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Decrease priority
    Private _decP As asyncCallbackProcDecreasePriority
    Public Function DecreasePriority() As Integer

        If _decP Is Nothing Then
            _decP = New asyncCallbackProcDecreasePriority(New asyncCallbackProcDecreasePriority.HasDecreasedPriority(AddressOf decreasePriorityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _decP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcDecreasePriority.poolObj(Me.Infos.Pid, Me.Infos.Priority, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub decreasePriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Increase priority
    Private _incP As asyncCallbackProcIncreasePriority
    Public Function IncreasePriority() As Integer

        If _incP Is Nothing Then
            _incP = New asyncCallbackProcIncreasePriority(New asyncCallbackProcIncreasePriority.HasIncreasedPriority(AddressOf increasePriorityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _incP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcIncreasePriority.poolObj(Me.Infos.Pid, Me.Infos.Priority, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub increasePriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set priority to process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Suspend a process
    Private _suspP As asyncCallbackProcSuspend
    Public Function SuspendProcess() As Integer

        If _suspP Is Nothing Then
            _suspP = New asyncCallbackProcSuspend(New asyncCallbackProcSuspend.HasSuspended(AddressOf suspendDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _suspP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcSuspend.poolObj(Me.Infos.Pid, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub suspendDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not suspend process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Resume a process
    Private _resuP As asyncCallbackProcResume
    Public Function ResumeProcess() As Integer

        If _resuP Is Nothing Then
            _resuP = New asyncCallbackProcResume(New asyncCallbackProcResume.HasResumed(AddressOf resumeDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _resuP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcResume.poolObj(Me.Infos.Pid, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub resumeDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not resume process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Kill a process tree
    Private _killTP As asyncCallbackProcKillTree
    Public Function KillProcessTree() As Integer

        If _killTP Is Nothing Then
            _killTP = New asyncCallbackProcKillTree(New asyncCallbackProcKillTree.HasKilled(AddressOf recursiveKillDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killTP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcKillTree.poolObj(Me.Infos.Pid, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub recursiveKillDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill process " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Empty working set size
    Private _emptyP As asyncCallbackProcEmptyWorkingSet
    Public Function EmptyWorkingSetSize() As Integer

        If _emptyP Is Nothing Then
            _emptyP = New asyncCallbackProcEmptyWorkingSet(New asyncCallbackProcEmptyWorkingSet.HasReducedWorkingSet(AddressOf emptyWorkingSetSizeDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _emptyP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcEmptyWorkingSet.poolObj(Me.Infos.Pid, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub emptyWorkingSetSizeDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg & " (" & Err.LastDllError.ToString & _
                   ")", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not empty working set" & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    '' Unload a module
    'Public Function UnloadModule(ByVal baseAddress As Integer) As Integer
    '    Dim t As New Threading.Thread(AddressOf asyncCallbackProcUnloadModule)
    '    t.Priority = Threading.ThreadPriority.Lowest
    '    t.IsBackground = True
    '    t.Start()
    'End Function

    ' Change affinity
    Private _setAffinityP As asyncCallbackProcSetAffinity
    Public Function SetAffinity(ByVal affinity As Integer) As Integer

        If _setAffinityP Is Nothing Then
            _setAffinityP = New asyncCallbackProcSetAffinity(New asyncCallbackProcSetAffinity.HasSetAffinity(AddressOf setAffinityDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setAffinityP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcSetAffinity.poolObj(Me.Infos.Pid, affinity, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub setAffinityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not set affinity " & Me.Infos.Name & " (" & Me.Infos.Pid.ToString & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Get information overriden methods"
    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            Return PendingTaskCount.ToString
        End If

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
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
                If Me.Infos.StartTime > 0 Then
                    Dim ts As Date = New Date(Me.Infos.StartTime)
                    res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
                End If
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
                res = GetFormatedPercentage(Me.Infos.AverageCpuUsage)
            Case "CommandLine"
                res = Me.Infos.CommandLine
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount.ToString
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount.ToString
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount.ToString
            Case "ReadTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.ReadTransferCount)
            Case "WriteTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.WriteTransferCount)
            Case "OtherTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.OtherTransferCount)
            Case "ReadOperationCountDelta"
                res = _ioDelta.ReadOperationCount.ToString
            Case "WriteOperationCountDelta"
                res = _ioDelta.WriteOperationCount.ToString
            Case "OtherOperationCountDelta"
                res = _ioDelta.OtherOperationCount.ToString
            Case "ReadTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.ReadTransferCount)
            Case "WriteTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.WriteTransferCount)
            Case "OtherTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.OtherTransferCount)
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
                res = Me.Infos.GdiObjects
            Case "UserObjects"
                res = Me.Infos.UserObjects
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount
            Case "ReadTransferCount"
                res = Me.Infos.IOValues.ReadTransferCount
            Case "WriteTransferCount"
                res = Me.Infos.IOValues.WriteTransferCount
            Case "OtherTransferCount"
                res = Me.Infos.IOValues.OtherTransferCount
            Case "ReadOperationCountDelta"
                res = _ioDelta.ReadOperationCount
            Case "WriteOperationCountDelta"
                res = _ioDelta.WriteOperationCount
            Case "OtherOperationCountDelta"
                res = _ioDelta.OtherOperationCount
            Case "ReadTransferCountDelta"
                res = _ioDelta.ReadTransferCount
            Case "WriteTransferCountDelta"
                res = _ioDelta.WriteTransferCount
            Case "OtherTransferCountDelta"
                res = _ioDelta.OtherTransferCount
            Case "CpuUsage"
                res = 100 * Me.CpuUsage
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
    Private Shared _sharedKillP As asyncCallbackProcKill
    Public Shared Function SharedLRKill(ByVal pid As Integer) As Integer

        If _sharedKillP Is Nothing Then
            _sharedKillP = New asyncCallbackProcKill(New asyncCallbackProcKill.HasKilled(AddressOf killDoneShared), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedKillP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcKill.poolObj(pid, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub killDoneShared(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill process " & pid.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Start a process
    Private Shared _newSharedP As asyncCallbackProcNewProcess
    Public Shared Function SharedRLStartNewProcess(ByVal path As String) As Integer

        If _newSharedP Is Nothing Then
            _newSharedP = New asyncCallbackProcNewProcess(New asyncCallbackProcNewProcess.HasCreated(AddressOf newProcessDoneShared), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _newSharedP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcNewProcess.poolObj(path, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub newProcessDoneShared(ByVal Success As Boolean, ByVal path As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : cannot start process : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not start new process " & path)
        End If
        RemovePendingTask(actionNumber)
    End Sub


    ' Unload a module from a process
    Private Shared _unloadMSharedP As asyncCallbackProcUnloadModule
    Public Shared Function SharedRLUnLoadModuleFromProcess(ByVal pid As Integer, ByVal ModuleBaseAddress As Integer) As Integer

        If _unloadMSharedP Is Nothing Then
            _unloadMSharedP = New asyncCallbackProcUnloadModule(New asyncCallbackProcUnloadModule.HasUnloadedModule(AddressOf unloadModuleDoneShared), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _unloadMSharedP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackProcUnloadModule.poolObj(pid, ModuleBaseAddress, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub unloadModuleDoneShared(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not unload the module from process " & pid.ToString)
        End If
        RemovePendingTask(actionNumber)
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

#Region "Shared functions (local)"

    Public Shared Function LocalKill(ByVal pid As Integer) As Integer
        Dim hProc As Integer
        Dim ret As Integer = -1
        hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_TERMINATE, 0, pid)
        If hProc > 0 Then
            ret = API.TerminateProcess(hProc, 0)
            API.CloseHandle(hProc)
        End If
        Return ret
    End Function

#End Region

    ' Retrieve a long array with all available values from dictionnaries
    Public Function GetHistory(ByVal infoName As String) As Double()
        Dim ret() As Double

        Select Case infoName
            Case "KernelCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_TIME_INFO In _dicoProcTimes.Values
                    ret(x) = t.kernel
                    x += 1
                Next
            Case "UserCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_TIME_INFO In _dicoProcTimes.Values
                    ret(x) = t.user
                    x += 1
                Next
            Case "TotalCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_TIME_INFO In _dicoProcTimes.Values
                    ret(x) = t.total
                    x += 1
                Next
            Case "WorkingSet"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.WorkingSetSize
                    x += 1
                Next
            Case "PeakWorkingSet"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PeakWorkingSetSize
                    x += 1
                Next
            Case "PageFaultCount"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PageFaultCount
                    x += 1
                Next
            Case "PagefileUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PagefileUsage
                    x += 1
                Next
            Case "PeakPagefileUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.PeakPagefileUsage
                    x += 1
                Next
            Case "QuotaPeakPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPeakPagedPoolUsage
                    x += 1
                Next
            Case "QuotaPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPagedPoolUsage
                    x += 1
                Next
            Case "QuotaPeakNonPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPeakNonPagedPoolUsage
                    x += 1
                Next
            Case "QuotaNonPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MEM_INFO In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaNonPagedPoolUsage
                    x += 1
                Next
            Case "ReadOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.ReadOperationCount)
                    x += 1
                Next
            Case "WriteOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.WriteOperationCount)
                    x += 1
                Next
            Case "OtherOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.OtherOperationCount)
                    x += 1
                Next
            Case "ReadTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.ReadTransferCount)
                    x += 1
                Next
            Case "WriteTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.WriteTransferCount)
                    x += 1
                Next
            Case "OtherTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIO.Values
                    ret(x) = CLng(t.io.OtherTransferCount)
                    x += 1
                Next
            Case "GdiObjects"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.gdiO
                    x += 1
                Next
            Case "UserObjects"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.userO
                    x += 1
                Next
            Case "CpuUsage"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.cpuUsage
                    x += 1
                Next
            Case "AverageCpuUsage"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_MISC_INFO In _dicoProcMisc.Values
                    ret(x) = t.averageCpuUsage
                    x += 1
                Next
            Case "ReadOperationCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.ReadOperationCount)
                    x += 1
                Next
            Case "WriteOperationCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.WriteOperationCount)
                    x += 1
                Next
            Case "OtherOperationCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.OtherOperationCount)
                    x += 1
                Next
            Case "ReadTransferCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.ReadTransferCount)
                    x += 1
                Next
            Case "WriteTransferCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.WriteTransferCount)
                    x += 1
                Next
            Case "OtherTransferCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As PROC_IO_INFO In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.OtherTransferCount)
                    x += 1
                Next
            Case Else
                ReDim ret(0)
        End Select

        Return ret
    End Function

    ' Refresh CPU usage once
    Private Sub refreshCpuUsage()
        Static oldDate As Long = Date.Now.Ticks
        Static oldProcTime As Long = Me.Infos.ProcessorTime

        Dim currDate As Long = Date.Now.Ticks
        Dim proctime As Long = Me.Infos.ProcessorTime

        Dim diff As Long = currDate - oldDate
        Dim procDiff As Long = proctime - oldProcTime

        oldProcTime = proctime
        oldDate = currDate

        If diff > 0 AndAlso _processors > 0 Then
            _cpuUsage = procDiff / diff / _processors
        Else
            _cpuUsage = 0
        End If
    End Sub

    ' Refresh IO delta once
    Private Sub refreshIODelta()
        Static oldDate As Long = Date.Now.Ticks
        Static oldIO As API.IO_COUNTERS = Me.Infos.IOValues

        Dim currDate As Long = Date.Now.Ticks
        Dim ioValues As API.IO_COUNTERS = Me.Infos.IOValues

        Dim diff As Long = currDate - oldDate
        Dim ioDiff As API.IO_COUNTERS
        With ioDiff
            .OtherOperationCount = ioValues.OtherOperationCount - oldIO.OtherOperationCount
            .OtherTransferCount = ioValues.OtherTransferCount - oldIO.OtherTransferCount
            .ReadOperationCount = ioValues.ReadOperationCount - oldIO.ReadOperationCount
            .ReadTransferCount = ioValues.ReadTransferCount - oldIO.ReadTransferCount
            .WriteOperationCount = ioValues.WriteOperationCount - oldIO.WriteOperationCount
            .WriteTransferCount = ioValues.WriteTransferCount - oldIO.WriteTransferCount
        End With

        oldIO = ioValues
        oldDate = currDate

        If diff > 0 Then
            With _ioDelta
                .OtherOperationCount = CULng(ioDiff.OtherOperationCount)
                .OtherTransferCount = CULng(10000000 * ioDiff.OtherTransferCount / diff)
                .ReadOperationCount = CULng(ioDiff.ReadOperationCount)
                .ReadTransferCount = CULng(10000000 * ioDiff.ReadTransferCount / diff)
                .WriteOperationCount = CULng(ioDiff.WriteOperationCount)
                .WriteTransferCount = CULng(10000000 * ioDiff.WriteTransferCount / diff)
            End With
        Else
            With _ioDelta
                .OtherOperationCount = 0
                .OtherTransferCount = 0
                .ReadOperationCount = 0
                .ReadTransferCount = 0
                .WriteOperationCount = 0
                .WriteTransferCount = 0
            End With
        End If
    End Sub

    ' Get a service by name
    Public Shared Function GetProcessById(ByVal id As Integer) As cProcess

        Dim tt As cProcess = Nothing
        cProcess.SemCurrentProcesses.WaitOne()
        If _currentProcesses.ContainsKey(id.ToString) Then
            tt = _currentProcesses.Item(id.ToString)
        End If
        cProcess.SemCurrentProcesses.Release()

        Return tt

    End Function
End Class
