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

Imports System.Runtime.InteropServices
Imports YAPM.Native.Api

Namespace Native.Objects

    Public Class Job


        ' ========================================
        ' Private constants
        ' ========================================

        Private Const ERROR_ALREADY_EXISTS As Integer = &HB7


        ' ========================================
        ' Private attributes
        ' ========================================

        ' List of created jobs
        Private Shared colJobs As New Dictionary(Of String, cJob)


        ' ========================================
        ' Public properties
        ' ========================================

        ' Current jobs
        Public Shared ReadOnly Property CreatedJobs() As Dictionary(Of String, cJob)
            Get
                Return colJobs
            End Get
        End Property


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Add a process to a job
        Public Shared Function CreateJobByName(ByVal jobName As String) As cJob
            ' Create a job
            Dim hJob As IntPtr
            Dim sa As New NativeStructs.SecurityAttributes
            With sa
                .nLength = Marshal.SizeOf(sa)
                .bInheritHandle = True
                .lpSecurityDescriptor = IntPtr.Zero
            End With

            hJob = NativeFunctions.CreateJobObject(sa, jobName)

            If hJob.IsNotNull Then
                ' Add process to job
                ' The job created has All access

                If colJobs.ContainsKey(jobName) = False Then
                    ' Add the new job to the dico
                    Dim tJ As New jobInfos(jobName, hJob)
                    Dim theJob As New cJob(tJ)
                    colJobs.Add(jobName, theJob)
                    Return theJob
                Else
                    Return colJobs.Item(jobName)
                End If
            End If

            Return Nothing
        End Function

        ' Add a process to a job
        Public Shared Function AddProcessToJobById(ByVal processId As Integer, _
                                                   ByVal jobName As String) As Boolean
            ' Create (or open existing) job
            Dim hJob As IntPtr
            Dim sa As New NativeStructs.SecurityAttributes
            With sa
                .nLength = Marshal.SizeOf(sa)
                .bInheritHandle = True
                .lpSecurityDescriptor = IntPtr.Zero
            End With

            hJob = NativeFunctions.CreateJobObject(sa, jobName)

            If hJob.IsNotNull Then
                ' Add process to job
                ' The job created has All access

                If colJobs.ContainsKey(jobName) = False Then
                    ' Add the new job to the dico
                    Dim tJ As New jobInfos(jobName, hJob)
                    colJobs.Add(jobName, New cJob(tJ))
                End If

                Dim hProc As IntPtr = NativeFunctions.OpenProcess(Security.ProcessAccess.SetQuota Or Security.ProcessAccess.Terminate, False, processId)
                If hProc.IsNotNull Then
                    Dim ret As Boolean = _
                        NativeFunctions.AssignProcessToJobObject(hJob, hProc)
                    Objects.General.CloseHandle(hProc)
                    If ret Then
                        ' Then a new process has been added to the job

                    End If
                    Return ret
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        ' Terminate a job
        Public Shared Function TerminateJobByJobId(ByVal jobId As Integer, _
                    Optional ByVal exitCode As Integer = 0) As Boolean

            ' NEEDS SEM ? SYNCISSUE ?
            Dim hJob As IntPtr
            For Each cJ As cJob In colJobs.Values
                If cJ.Infos.JobId = jobId Then
                    hJob = cJ.Infos.JobHandle
                    Exit For
                End If
            Next

            If hJob.IsNotNull Then
                ' Then terminate job !
                Return NativeFunctions.TerminateJobObject(hJob, exitCode)
            Else
                Return False
            End If

        End Function

        ' Add some processes to a job
        Public Shared Function AddProcessToJobByIds(ByVal processId() As Integer, _
                                                    ByVal jobName As String) As Boolean
            If processId IsNot Nothing Then
                Dim ret As Boolean = True
                For Each id As Integer In processId
                    ret = ret And AddProcessToJobById(id, jobName)
                Next
                Return ret
            Else
                Return False
            End If
        End Function

        ' Enumerate created jobs
        Public Shared Function EnumerateCreatedJobs() As Dictionary(Of String, cJob)
            Return colJobs
        End Function

        ' Enumerate processes in a job
        Public Shared Function GetProcessesInJobByHandle(ByVal hJob As IntPtr) As List(Of Integer)
            Dim pids As New List(Of Integer)
            Dim ret As Integer
            Dim memAlloc As New Memory.MemoryAlloc(&H1000)

            NativeFunctions.QueryInformationJobObject(hJob, _
                            NativeEnums.JobObjectInformationClass.JobObjectBasicProcessIdList, _
                            memAlloc, memAlloc.Size, ret)

            Dim list As NativeStructs.JobObjectBasicProcessIdList = _
                memAlloc.ReadStruct(Of NativeStructs.JobObjectBasicProcessIdList)()

            For i As Integer = 0 To list.ProcessIdsCount - 1
                pids.Add(memAlloc.ReadInt32(&H8, i))     ' &h8 cause of two first Int32
            Next

            memAlloc.Free()

            Return pids
        End Function


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
