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

Imports System.Net
Imports YAPM.Native.Api

Public Class cJob
    Inherits cGeneralObject
    Implements IDisposable

    ' Infos
    Private _jobInfos As jobInfos

    Private Shared WithEvents _connection As cJobConnection


#Region "Properties"

    Public Shared Property Connection() As cJobConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cJobConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As jobInfos)
        _jobInfos = infos
        _connection = Connection
        _TypeOfObject = Enums.GeneralObjectType.Job
    End Sub
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' This object will be cleaned up by the Dispose method.
        ' Therefore, you should call GC.SupressFinalize to
        ' take this object off the finalization queue 
        ' and prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not Me.disposed Then
            ' If disposing equals true, dispose all managed 
            ' and unmanaged resources.
            If disposing Then
                ' Dispose managed resources.

            End If

            ' Note disposing has been done.
            disposed = True

        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As jobInfos
        Get
            Return _jobInfos
        End Get
    End Property



#End Region

#Region "Shared method"

    ' Create a job
    Public Shared Function CreateJobByName(ByVal jobName As String) As cJob
        Return Native.Objects.Job.CreateJobByName(jobName)
    End Function

    ' Get a job by its name
    Public Shared Function GetJobByName(ByVal jobName As String) As cJob
        For Each cJ As jobInfos In Native.Objects.Job.EnumerateJobs.Values
            If cJ.Name = jobName Then
                Return New cJob(cJ)
            End If
        Next
        Return Nothing
    End Function

    ' Return job a process (if any)
    Public Shared Function GetProcessJobById(ByVal pid As Integer) As cJob
        For Each cJ As jobInfos In Native.Objects.Job.EnumerateJobs.Values
            If cJ.PidList.Contains(pid) Then
                Return New cJob(cJ)
            End If
        Next
        Return Nothing
    End Function

#End Region

#Region "Shared local functions"

    ' Terminate job
    Private Shared _sharedTermJ As asyncCallbackJobTerminateJob
    Public Shared Function SharedLRTerminateJob(ByVal name As String) As Integer

        If _sharedTermJ Is Nothing Then
            _sharedTermJ = New asyncCallbackJobTerminateJob(New asyncCallbackJobTerminateJob.HasTerminatedJob(AddressOf sharedKillJobDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedTermJ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackJobTerminateJob.poolObj(name, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub sharedKillJobDone(ByVal Success As Boolean, ByVal jobName As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not kill job " & jobName)
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

    ' Add a process to the job
    Private Shared _addedShared As asyncCallbackJobAddProcess
    Public Shared Function SharedLRAddProcess(ByVal jobName As String, ByVal pids() As Integer) As Integer

        If _addedShared Is Nothing Then
            _addedShared = New asyncCallbackJobAddProcess(New asyncCallbackJobAddProcess.HasAddedProcessesToJob(AddressOf sharedAddedJobDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _addedShared.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddSharedPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackJobAddProcess.poolObj(pids, jobName, newAction))

    End Function
    Private Shared Sub sharedAddedJobDone(ByVal Success As Boolean, ByVal pid() As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not add processes to job")
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

#End Region

#Region "All actions on job"

    ' Add a process to the job
    Private _addedP As asyncCallbackJobAddProcess
    Public Function AddProcess(ByVal pid As Integer) As Integer

        If _addedP Is Nothing Then
            _addedP = New asyncCallbackJobAddProcess(New asyncCallbackJobAddProcess.HasAddedProcessesToJob(AddressOf addedProcessDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _addedP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Dim pids() As Integer
        ReDim pids(0)
        pids(0) = pid
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackJobAddProcess.poolObj(pids, Me.Infos.Name, newAction))

    End Function
    Private Sub addedProcessDone(ByVal Success As Boolean, ByVal pid() As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not add processes to job")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Terminate a job
    Private _killedJob As asyncCallbackJobTerminateJob
    Public Function TerminateJob() As Integer

        If _killedJob Is Nothing Then
            _killedJob = New asyncCallbackJobTerminateJob(New asyncCallbackJobTerminateJob.HasTerminatedJob(AddressOf killJobDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killedJob.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackJobTerminateJob.poolObj(Me.Infos.Name, newAction))

    End Function
    Private Sub killJobDone(ByVal Success As Boolean, ByVal jobName As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not terminate job " & jobName)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As jobInfos)
        _jobInfos.Merge(Thr)
    End Sub


#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Name"
                res = Me.Infos.Name
            Case "ProcessesCount"
                res = Me.Infos.PidList.Count.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_JobName As String = ""
        Static _old_JobId As String = ""
        Static _old_ProcessesCount As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Name"
                res = Me.Infos.Name
                If res = _old_JobName Then
                    hasChanged = False
                Else
                    _old_JobName = res
                End If
            Case "JobId"
                res = Me.Infos.Name
                If res = _old_JobId Then
                    hasChanged = False
                Else
                    _old_JobId = res
                End If
            Case "ProcessesCount"
                res = Me.Infos.PidList.Count.ToString
                If res = _old_ProcessesCount Then
                    hasChanged = False
                Else
                    _old_ProcessesCount = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

End Class
