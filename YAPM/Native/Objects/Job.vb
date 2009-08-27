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

        ' NtStatus length mismatch
        Private Const STATUS_INFO_LENGTH_MISMATCH As UInteger = &HC0000004


        ' ========================================
        ' Private attributes
        ' ========================================

        ' List of jobs to refresh & sem to protect
        Private Shared jobToRefresh As New List(Of cJob)
        Private Shared semJobToRefresh As New System.Threading.Semaphore(1, 1)

        ' Sem for enumeration
        Private Shared semEnum As New System.Threading.Semaphore(1, 1)

        ' List of created jobs
        Private Shared colJobs As New Dictionary(Of String, cJob)

        ' Some mem allocations
        Private Shared memAllocJobs As New Native.Memory.MemoryAlloc(&H100)
        Private Shared BufferObjNameJob As New Native.Memory.MemoryAlloc(512)


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
                    Dim tJ As New jobInfos(jobName, GetJobHandleByName(jobName, Security.JobAccess.Query), True)
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
                    Dim tJ As New jobInfos(jobName, GetJobHandleByName(jobName, Security.JobAccess.Query), True)
                    colJobs.Add(jobName, New cJob(tJ))
                End If

                Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(processId, _
                                            Security.ProcessAccess.SetQuota Or _
                                            Security.ProcessAccess.Terminate)
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
        Public Shared Function TerminateJobByJobName(ByVal jobName As String, _
            Optional ByVal exitCode As Integer = 0) As Boolean

            Dim hJob As IntPtr
            Dim ret As Boolean

            ' Open job by its name
            hJob = NativeFunctions.OpenJobObject(Security.JobAccess.Terminate, False, jobName)


            If hJob.IsNotNull Then
                ' Then terminate job !
                ret = NativeFunctions.TerminateJobObject(hJob, exitCode)
                Native.Objects.General.CloseHandle(hJob)
                Return ret
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
        Public Shared Function EnumerateCreatedJobs(Optional ByVal refreshNow As Boolean = False) As Dictionary(Of String, cJob)
            Return GetJobList(refreshNow)
        End Function

        ' Enumerate Processes in a job
        Public Shared Function GetProcessesInJobByHandle(ByVal hJob As IntPtr) As List(Of Integer)
            Dim pids As New List(Of Integer)
            Dim ret As Integer

            If hJob.IsNotNull Then

                Dim memAlloc As New Memory.MemoryAlloc(&H1000)

                NativeFunctions.QueryInformationJobObject(hJob, _
                                NativeEnums.JobObjectInformationClass.JobObjectBasicProcessIdList, _
                                memAlloc.Pointer, memAlloc.Size, ret)

                If ret > 0 Then
                    Dim list As NativeStructs.JobObjectBasicProcessIdList = _
                        memAlloc.ReadStruct(Of NativeStructs.JobObjectBasicProcessIdList)()

                    For i As Integer = 0 To list.ProcessIdsCount - 1
                        pids.Add(memAlloc.ReadInt32(&H8, i))     ' &h8 cause of two first Int32
                    Next
                End If

                memAlloc.Free()

            End If

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


        ' Return job handle
        Public Shared Function GetJobHandleByName(ByVal name As String, ByVal access As Security.JobAccess) As IntPtr
            Return NativeFunctions.OpenJobObject(access, True, name)
        End Function

        ' Demand stats refreshment for a cJob
        Public Shared Sub DemandJobRefreshment(ByRef job As cJob)
            semJobToRefresh.WaitOne()
            jobToRefresh.Add(job)
            semJobToRefresh.Release()
        End Sub



        ' ========================================
        ' Private functions
        ' ========================================

        ' Return list of jobs
        Private Shared Function GetJobList(Optional ByVal refreshNow As Boolean = False) As Dictionary(Of String, cJob)
            Dim Length As Integer
            Dim x As Integer
            Dim mCount As Integer
            Dim ret As Integer
            Dim ObjName As NativeStructs.ObjectNameInformation
            Dim Handle As NativeStructs.SystemHandleInformation
            Dim buf As New Dictionary(Of String, cJob)
            Dim hProcess As IntPtr

            semEnum.WaitOne()

            Static _dupHandles As New Dictionary(Of String, IntPtr)

            ' HACK HACK HACK HACK
            ' Here is how we retrieve the job list :
            ' - we enumerate all handles
            ' - we select all handles with type = job
            ' - we duplicate these handles to have access in our application (if they are
            '   not already owned by our application)
            ' This is it !
            ' NOTE : we should NOT keep these handles (if duplicated) opened as it might implies
            ' a change of bevahior in jobs which have JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE limit,
            ' as they terminate only when all opened handles are closed.
            ' SO we save the list of opened handle some time (as we must have the handle
            ' to access to the job !), and we then close/reopen them each enumeration

            ' Closed previously duplicated handles
            ' We refresh the informations (stats) if demanded
            semJobToRefresh.WaitOne()
            For Each Job As cJob In jobToRefresh
                Job.RefreshWithValidHandle()
            Next
            semJobToRefresh.Release()
            For Each ptr As IntPtr In _dupHandles.Values
                If ptr.IsNotNull Then
                    Native.Objects.General.CloseHandle(ptr)
                End If
            Next
            _dupHandles.Clear()

            Length = memAllocJobs.Size
            ' While length is too small
            Do While NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, _
                                memAllocJobs.Pointer, memAllocJobs.Size, ret) = STATUS_INFO_LENGTH_MISMATCH
                ' Resize buffer
                Length = Length * 2
                memAllocJobs.Resize(Length)
            Loop

            ' Get the number of handles (first 4 bytes)
            mCount = memAllocJobs.ReadInt32(0)

            ' Resize our array
            Dim objTypeOffsetInStruct As Integer = Native.Api.NativeStructs.SystemHandleInformation_ObjectTypeOffset
            Dim structSize As Integer = Marshal.SizeOf(GetType(NativeStructs.SystemHandleInformation))
            For x = 0 To mCount - 1
                ' We do not retrieve the entire SystemHandleInformation struct
                ' cause it requires too much CPU time
                ' We just retrieve the Byte which represent the object type

                ' &h4 offset because of HandleCount on 4 first bytes
                Dim type As Integer = memAllocJobs.ReadByte(objTypeOffsetInStruct + _
                                                                    &H4 + x * structSize)
                If type = NativeConstants.HandleOjectTypeJob Then

                    ' This is a job !

                    ' Get entire struct
                    ' &h4 offset because of HandleCount on 4 first bytes
                    Handle = memAllocJobs.ReadStruct(Of NativeStructs.SystemHandleInformation)(&H4, x)

                    ' Retrieve its name
                    Dim theName As String
                    Dim targetHandle As IntPtr

                    ' If this handle belongs to our process, there is no need
                    ' to duplicate it
                    If Handle.ProcessId = NativeFunctions.GetCurrentProcessId Then

                        targetHandle = New IntPtr(Handle.Handle)

                        If targetHandle.IsNotNull Then

                            NativeFunctions.ZeroMemory(BufferObjNameJob, New IntPtr(512))
                            NativeFunctions.NtQueryObject(targetHandle, _
                                            NativeEnums.ObjectInformationClass.ObjectNameInformation, _
                                            BufferObjNameJob.Pointer, 512, ret)
                            ObjName = BufferObjNameJob.ReadStruct(Of NativeStructs.ObjectNameInformation)(0)
                            theName = Marshal.PtrToStringUni(New IntPtr(BufferObjNameJob.Pointer.ToInt32 + 8))

                            ' Add to dico
                            ' The key is the name
                            If Not (String.IsNullOrEmpty(theName)) AndAlso buf.ContainsKey(theName) = False Then
                                Dim jj As cJob = New cJob(New jobInfos(theName, targetHandle, True))
                                If refreshNow Then
                                    jj.RefreshWithValidHandle()
                                End If
                                buf.Add(theName, jj)
                            End If
                        End If
                    Else

                        ' Open an handle to the process which owns our handle
                        hProcess = Native.Objects.Process.GetProcessHandleById(Handle.ProcessId, _
                                                        Security.ProcessAccess.DupHandle)

                        ' Duplicate the handle in our process with same access
                        NativeFunctions.DuplicateHandle(hProcess, New IntPtr(Handle.Handle), _
                                                        New IntPtr(NativeFunctions.GetCurrentProcess), _
                                                        targetHandle, 0, False, _
                                                        NativeEnums.DuplicateOptions.SameAccess)
                        If targetHandle.IsNotNull Then

                            NativeFunctions.ZeroMemory(BufferObjNameJob, New IntPtr(512))
                            NativeFunctions.NtQueryObject(targetHandle, _
                                            NativeEnums.ObjectInformationClass.ObjectNameInformation, _
                                            BufferObjNameJob.Pointer, 512, ret)
                            ObjName = BufferObjNameJob.ReadStruct(Of NativeStructs.ObjectNameInformation)(0)
                            theName = Marshal.PtrToStringUni(New IntPtr(BufferObjNameJob.Pointer.ToInt32 + 8))

                            ' Add to dico only NAMED jobs
                            ' The key is theName
                            If Not (String.IsNullOrEmpty(theName)) AndAlso buf.ContainsKey(theName) = False Then
                                Dim jj As cJob = New cJob(New jobInfos(theName, targetHandle, False))
                                If refreshNow Then
                                    jj.RefreshWithValidHandle()
                                End If
                                buf.Add(theName, jj)

                                ' Add the handle to the list of duplicated handles
                                ' So we will close it just before next enumeration
                                ' to avoid to multiple per 2 each enumeration the number
                                ' of handles... And to avoid JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE limit issue
                                _dupHandles.Add(theName, targetHandle)
                            Else
                                ' Close handle immediately as we can't access it (unnamed)
                                Native.Objects.General.CloseHandle(targetHandle)
                            End If

                        End If
                    End If
                End If

            Next

            semEnum.Release()

            Return buf

        End Function

        ' Query a job information struct
        Private Shared Function QueryJobInformationByHandle(Of T)(ByVal handle As IntPtr, _
                                ByVal info As NativeEnums.JobObjectInformationClass) As T
            Dim ret As Integer

            If handle.IsNotNull Then

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
            Else
                Return Nothing
            End If

        End Function

        ' Set a job information struct
        Private Shared Function SetJobInformationByHandle(Of T)(ByVal handle As IntPtr, _
                                ByVal info As NativeEnums.JobObjectInformationClass, _
                                ByVal limit As T) As Boolean

            Dim ret As Boolean
            If handle.IsNotNull Then
                Dim memAlloc As New Memory.MemoryAlloc(Marshal.SizeOf(GetType(T)))
                memAlloc.WriteStruct(Of T)(limit)
                ret = NativeFunctions.SetInformationJobObject(handle, info, memAlloc.Pointer, _
                                                        memAlloc.Size)

                memAlloc.Free()

                Return ret
            Else
                Return False
            End If

        End Function

    End Class

End Namespace
