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
                            memAlloc.Pointer, memAlloc.Size, ret)

            Dim list As NativeStructs.JobObjectBasicProcessIdList = _
                memAlloc.ReadStruct(Of NativeStructs.JobObjectBasicProcessIdList)()

            For i As Integer = 0 To list.ProcessIdsCount - 1
                pids.Add(memAlloc.ReadInt32(&H8, i))     ' &h8 cause of two first Int32
            Next

            memAlloc.Free()

            Return pids
        End Function

        ' Query some informations
        ' These 5 functions use QueryJobInformationByHandle
        Public Shared Function GeJobBasicAndIoAccountingInformationByHandle(ByVal hJob As IntPtr) As NativeStructs.JobObjectBasicAndIoAccountingInformation
            Return QueryJobInformationByHandle(Of NativeStructs.JobObjectBasicAndIoAccountingInformation)(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicAndIoAccountingInformation)
        End Function

        Public Shared Function GeJobBasicAccountingInformationByHandle(ByVal hJob As IntPtr) As NativeStructs.JobObjectBasicAccountingInformation
            Return QueryJobInformationByHandle(Of NativeStructs.JobObjectBasicAccountingInformation)(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicAccountingInformation)
        End Function

        Public Shared Function GeJobBasicUiRestrictionsHandle(ByVal hJob As IntPtr) As NativeStructs.JobObjectBasicUiRestrictions
            Return QueryJobInformationByHandle(Of NativeStructs.JobObjectBasicUiRestrictions)(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicUIRestrictions)
        End Function

        Public Shared Function GeJobBasicLimitInformationByHandle(ByVal hJob As IntPtr) As NativeStructs.JobObjectBasicLimitInformation
            Return QueryJobInformationByHandle(Of NativeStructs.JobObjectBasicLimitInformation)(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicLimitInformation)
        End Function

        Public Shared Function GeJobExtendedLimitInformationsByHandle(ByVal hJob As IntPtr) As NativeStructs.JobObjectExtendedLimitInformation
            Return QueryJobInformationByHandle(Of NativeStructs.JobObjectExtendedLimitInformation)(hJob, NativeEnums.JobObjectInformationClass.JobObjectExtendedLimitInformation)
        End Function

        ' Set some informations
        ' These 5 functions use QueryJobInformationByHandle
        Public Shared Function SeJobBasicLimitInformationByHandle(ByVal hJob As IntPtr, ByVal limit As NativeStructs.JobObjectBasicLimitInformation) As Boolean
            Return SetJobInformationByHandle(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicLimitInformation, limit)
        End Function

        Public Shared Function SeJobBasicUiRestrictionsHandle(ByVal hJob As IntPtr, ByVal limit As NativeStructs.JobObjectBasicUiRestrictions) As Boolean
            Return SetJobInformationByHandle(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicUIRestrictions, limit)
        End Function

        Public Shared Function SeJobExtendedLimitInformationsByHandle(ByVal hJob As IntPtr, ByVal limit As NativeStructs.JobObjectExtendedLimitInformation) As Boolean
            Return SetJobInformationByHandle(hJob, NativeEnums.JobObjectInformationClass.JobObjectExtendedLimitInformation, limit)
        End Function

        Public Shared Function SeJobEndOfTimeInformationByHandle(ByVal hJob As IntPtr, ByVal limit As NativeStructs.JobObjectEndOfJobTimeInformation) As Boolean
            Return SetJobInformationByHandle(hJob, NativeEnums.JobObjectInformationClass.JobObjectEndOfJobTimeInformation, limit)
        End Function




        ' ========================================
        ' Private functions
        ' ========================================

        ' Query a job information struct
        Private Shared Function QueryJobInformationByHandle(Of T)(ByVal handle As IntPtr, _
                                ByVal info As NativeEnums.JobObjectInformationClass) As T
            Dim ret As Integer
            Dim memAlloc As New Memory.MemoryAlloc(Marshal.SizeOf(GetType(T)))

            If Not NativeFunctions.QueryInformationJobObject(handle, info, memAlloc.Pointer, _
                                                             memAlloc.Size, ret) Then
                ' Need a greater mem alloc
                memAlloc.Resize(ret)

                NativeFunctions.QueryInformationJobObject(handle, info, memAlloc.Pointer, _
                                                          memAlloc.Size, ret)
            End If

            Dim struct As T = memAlloc.ReadStruct(Of T)()
            memAlloc.Free()

            Return struct

        End Function

        ' Set a job information struct
        Private Shared Function SetJobInformationByHandle(Of T)(ByVal handle As IntPtr, _
                                ByVal info As NativeEnums.JobObjectInformationClass, _
                                ByVal limit As T) As Boolean

            Dim ret As Boolean
            Dim memAlloc As New Memory.MemoryAlloc(Marshal.SizeOf(GetType(T)))
            memAlloc.WriteStruct(Of T)(limit)
            ret = NativeFunctions.SetInformationJobObject(handle, info, memAlloc.Pointer, _
                                                    memAlloc.Size)

            memAlloc.Free()

            Return ret

        End Function

    End Class

End Namespace
