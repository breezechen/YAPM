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

        ' ObjectTypeNumer for Job
        Private Shared jobTypeNumber As Integer = 0

        ' List of valid handles (name <-> handle) + sem to protect access
        Private Shared _dupHandles As New Dictionary(Of String, IntPtr)
        Private Shared _ownHandles As New Dictionary(Of String, IntPtr)
        Private Shared semDupHandles As New System.Threading.Semaphore(1, 1)

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

        ' Call this function to start executin a code which uses a valid handle to a named job
        Public Shared Function BeginUsingValidJobHandle(ByVal name As String) As IntPtr

            If name Is Nothing Then
                Return IntPtr.Zero
            End If

            ' Wait sem
            semDupHandles.WaitOne()

            If _dupHandles.ContainsKey(name) Then
                ' Then it's a duplicated handle
                Return _dupHandles(name)
            ElseIf _ownHandles.ContainsKey(name) Then
                ' Then it's an owned handle
                Return _ownHandles(name)
            Else
                Return IntPtr.Zero
            End If

        End Function

        ' Same to finish
        Public Shared Sub EndUsingValidJobHandle()

            ' Simply release semaphore, so the enumeration func will close the handle
            ' next time
            semDupHandles.Release()

        End Sub


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
                    Dim tJ As New jobInfos(jobName)
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
            Dim ret As Boolean

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
                    Dim tJ As New jobInfos(jobName)
                    colJobs.Add(jobName, New cJob(tJ))
                End If

                Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(processId, _
                                            Security.ProcessAccess.SetQuota Or _
                                            Security.ProcessAccess.Terminate)
                If hProc.IsNotNull Then
                    ret = NativeFunctions.AssignProcessToJobObject(hJob, hProc)
                    Objects.General.CloseHandle(hProc)
                End If
            End If

            Return ret
        End Function

        ' Terminate a job
        Public Shared Function TerminateJobByJobName(ByVal jobName As String, _
            Optional ByVal exitCode As Integer = 0) As Boolean

            Dim hJob As IntPtr
            Dim ret As Boolean

            ' Open job by its name
            ' Query a valid handle (all acces)
            hJob = BeginUsingValidJobHandle(jobName)

            If hJob.IsNotNull Then
                ' Then terminate job !
                ret = NativeFunctions.TerminateJobObject(hJob, exitCode)

                If ret Then
                    ' Successfully terminated job
                    ' Now close the handle YAPM has opened to the job
                    Objects.General.CloseHandle(hJob)
                End If

            End If

            ' End using the valid handle
            EndUsingValidJobHandle()

            Return ret

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
        Public Shared Function EnumerateJobLimitsByJobName(ByVal jobName As String) As Dictionary(Of String, jobLimitInfos)
            Dim ret As New Dictionary(Of String, jobLimitInfos)

            ' First thing : we get a VALID handle to the job
            Dim hJob As IntPtr = BeginUsingValidJobHandle(jobName)

            If hJob.IsNotNull Then

                ' UiRestrictions
                Dim struct1 As NativeStructs.JobObjectBasicUiRestrictions = _
                    QueryJobInformationByHandle(Of NativeStructs.JobObjectBasicUiRestrictions)(hJob, NativeEnums.JobObjectInformationClass.JobObjectBasicUIRestrictions)
                Dim flag1 As NativeEnums.JobObjectBasicUiRestrictions = struct1.UIRestrictionsClass

                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.Desktop) = NativeEnums.JobObjectBasicUiRestrictions.Desktop Then
                    ret.Add("Desktop", New jobLimitInfos("Desktop", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.DisplaySettings) = NativeEnums.JobObjectBasicUiRestrictions.DisplaySettings Then
                    ret.Add("DisplaySettings", New jobLimitInfos("DisplaySettings", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.ExitWindows) = NativeEnums.JobObjectBasicUiRestrictions.ExitWindows Then
                    ret.Add("ExitWindows", New jobLimitInfos("ExitWindows", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.GlobalAtoms) = NativeEnums.JobObjectBasicUiRestrictions.GlobalAtoms Then
                    ret.Add("GlobalAtoms", New jobLimitInfos("GlobalAtoms", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.Handles) = NativeEnums.JobObjectBasicUiRestrictions.Handles Then
                    ret.Add("Handles", New jobLimitInfos("Handles", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.ReadClipboard) = NativeEnums.JobObjectBasicUiRestrictions.ReadClipboard Then
                    ret.Add("ReadClipboard", New jobLimitInfos("ReadClipboard", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.SystemParameters) = NativeEnums.JobObjectBasicUiRestrictions.SystemParameters Then
                    ret.Add("SystemParameters", New jobLimitInfos("SystemParameters", "Limited"))
                End If
                If (flag1 And NativeEnums.JobObjectBasicUiRestrictions.WriteClipboard) = NativeEnums.JobObjectBasicUiRestrictions.WriteClipboard Then
                    ret.Add("WriteClipboard", New jobLimitInfos("WriteClipboard", "Limited"))
                End If

                ' Other limitations
                Dim struct2 As NativeStructs.JobObjectExtendedLimitInformation = _
                    QueryJobInformationByHandle(Of NativeStructs.JobObjectExtendedLimitInformation)(hJob, NativeEnums.JobObjectInformationClass.JobObjectExtendedLimitInformation)
                Dim flag2 As NativeEnums.JobObjectLimitFlags = struct2.BasicLimitInformation.LimitFlags

                If (flag2 And NativeEnums.JobObjectLimitFlags.ActiveProcess) = NativeEnums.JobObjectLimitFlags.ActiveProcess Then
                    ret.Add("Active processes", New jobLimitInfos("Active processes", struct2.BasicLimitInformation.ActiveProcessLimit.ToString))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.Affinity) = NativeEnums.JobObjectLimitFlags.Affinity Then
                    ret.Add("Affinity", New jobLimitInfos("Affinity", struct2.BasicLimitInformation.Affinity.ToString))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.BreakawayOk) = NativeEnums.JobObjectLimitFlags.BreakawayOk Then
                    ret.Add("Breakaway OK", New jobLimitInfos("Breakaway OK", "Enabled"))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.DieOnUnhandledException) = NativeEnums.JobObjectLimitFlags.DieOnUnhandledException Then
                    ret.Add("Die on unhandled exception", New jobLimitInfos("Die on unhandled exception", "Enabled"))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.JobMemory) = NativeEnums.JobObjectLimitFlags.JobMemory Then
                    ret.Add("Committed memory for job", New jobLimitInfos("Committed memory for job", Common.Misc.GetFormatedSize(struct2.JobMemoryLimit)))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.JobTime) = NativeEnums.JobObjectLimitFlags.JobTime Then
                    ret.Add("Usermode time for job", New jobLimitInfos("Usermode time for job", struct2.BasicLimitInformation.PerJobUserTimeLimit.ToString))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.KillOnJobClose) = NativeEnums.JobObjectLimitFlags.KillOnJobClose Then
                    ret.Add("Kill on job close", New jobLimitInfos("Kill on job close", "Enabled"))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.PreserveJobTime) = NativeEnums.JobObjectLimitFlags.PreserveJobTime Then
                    ret.Add("Preserve job time", New jobLimitInfos("Preserve job time", "Enabled"))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.PriorityClass) = NativeEnums.JobObjectLimitFlags.PriorityClass Then
                    ret.Add("Priority class", New jobLimitInfos("Priority class", CType(struct2.BasicLimitInformation.PriorityClass, System.Diagnostics.ProcessPriorityClass).ToString))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.ProcessMemory) = NativeEnums.JobObjectLimitFlags.ProcessMemory Then
                    ret.Add("Committed memory for each process", New jobLimitInfos("Committed memory for each process", Common.Misc.GetFormatedSize(struct2.ProcessMemoryLimit)))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.ProcessTime) = NativeEnums.JobObjectLimitFlags.ProcessTime Then
                    ret.Add("Usermode time for each process", New jobLimitInfos("Usermode time for each process", struct2.BasicLimitInformation.PerProcessUserTimeLimit.ToString))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.SchedulingClass) = NativeEnums.JobObjectLimitFlags.SchedulingClass Then
                    ret.Add("Scheduling class", New jobLimitInfos("Scheduling class", struct2.BasicLimitInformation.SchedulingClass.ToString))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.SilentBreakawayOk) = NativeEnums.JobObjectLimitFlags.SilentBreakawayOk Then
                    ret.Add("Slient breakaway OK", New jobLimitInfos("Slient breakaway OK", "Enabled"))
                End If
                If (flag2 And NativeEnums.JobObjectLimitFlags.WorkingSet) = NativeEnums.JobObjectLimitFlags.WorkingSet Then
                    ret.Add("Minimum working set size per process", New jobLimitInfos("Minimum working set size per process", Common.Misc.GetFormatedSize(struct2.BasicLimitInformation.MinimumWorkingSetSize)))
                    ret.Add("Maximum working set size per process", New jobLimitInfos("Maximum working set size per process", Common.Misc.GetFormatedSize(struct2.BasicLimitInformation.MaximumWorkingSetSize)))
                End If

            End If

            ' End using valid handle
            EndUsingValidJobHandle()

            Return ret
        End Function

        ' Enumerate created jobs
        Public Shared Function EnumerateJobs(Optional ByVal refreshNow As Boolean = False) As Dictionary(Of String, cJob)
            Dim ret As Dictionary(Of String, cJob) = GetJobList()

            ' Refresh all infos on job
            If refreshNow Then
                For Each j As cJob In ret.Values
                    j.Refresh()
                Next
            End If

            Return ret
        End Function

        ' Enumerate Processes in a job
        Public Shared Function GetProcessesInJobByName(ByVal jobName As String) As List(Of Integer)
            Dim pids As New List(Of Integer)
            Dim ret As Integer

            ' Query valid handle
            Dim hJob As IntPtr = BeginUsingValidJobHandle(jobName)

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

            ' End using valid handle
            EndUsingValidJobHandle()

            Return pids
        End Function

        ' Query some informations
        ' These 5 functions use QueryJobInformationByHandle
        Public Shared Function GetJobBasicAndIoAccountingInformationByName(ByVal jobName As String) As NativeStructs.JobObjectBasicAndIoAccountingInformation
            Return QueryJobInformationByName(Of NativeStructs.JobObjectBasicAndIoAccountingInformation)(jobName, NativeEnums.JobObjectInformationClass.JobObjectBasicAndIoAccountingInformation)
        End Function

        Public Shared Function GetJobBasicAccountingInformationByName(ByVal jobName As String) As NativeStructs.JobObjectBasicAccountingInformation
            Return QueryJobInformationByName(Of NativeStructs.JobObjectBasicAccountingInformation)(jobName, NativeEnums.JobObjectInformationClass.JobObjectBasicAccountingInformation)
        End Function

        Public Shared Function GetJobBasicUiRestrictionsName(ByVal jobName As String) As NativeStructs.JobObjectBasicUiRestrictions
            Return QueryJobInformationByName(Of NativeStructs.JobObjectBasicUiRestrictions)(jobName, NativeEnums.JobObjectInformationClass.JobObjectBasicUIRestrictions)
        End Function

        Public Shared Function GetJobBasicLimitInformationByName(ByVal jobName As String) As NativeStructs.JobObjectBasicLimitInformation
            Return QueryJobInformationByName(Of NativeStructs.JobObjectBasicLimitInformation)(jobName, NativeEnums.JobObjectInformationClass.JobObjectBasicLimitInformation)
        End Function

        Public Shared Function GetJobExtendedLimitInformationsByName(ByVal jobName As String) As NativeStructs.JobObjectExtendedLimitInformation
            Return QueryJobInformationByName(Of NativeStructs.JobObjectExtendedLimitInformation)(jobName, NativeEnums.JobObjectInformationClass.JobObjectExtendedLimitInformation)
        End Function

        ' Set some informations
        ' These 5 functions use QueryJobInformationByHandle
        Public Shared Function SetJobBasicLimitInformationByName(ByVal jobName As String, ByVal limit As NativeStructs.JobObjectBasicLimitInformation) As Boolean
            Return SetJobInformationByName(jobName, NativeEnums.JobObjectInformationClass.JobObjectBasicLimitInformation, limit)
        End Function

        Public Shared Function SetJobBasicUiRestrictionsName(ByVal jobName As String, ByVal limit As NativeStructs.JobObjectBasicUiRestrictions) As Boolean
            Return SetJobInformationByName(jobName, NativeEnums.JobObjectInformationClass.JobObjectBasicUIRestrictions, limit)
        End Function

        Public Shared Function SetJobExtendedLimitInformationsByName(ByVal jobName As String, ByVal limit As NativeStructs.JobObjectExtendedLimitInformation) As Boolean
            Return SetJobInformationByName(jobName, NativeEnums.JobObjectInformationClass.JobObjectExtendedLimitInformation, limit)
        End Function

        Public Shared Function SetJobEndOfTimeInformationByName(ByVal jobName As String, ByVal limit As NativeStructs.JobObjectEndOfJobTimeInformation) As Boolean
            Return SetJobInformationByName(jobName, NativeEnums.JobObjectInformationClass.JobObjectEndOfJobTimeInformation, limit)
        End Function


        ' Return job handle
        Public Shared Function GetJobHandleByName(ByVal name As String, ByVal access As Security.JobAccess) As IntPtr
            Return NativeFunctions.OpenJobObject(access, True, name)
        End Function



        ' ========================================
        ' Private functions
        ' ========================================

        ' Return list of jobs
        Private Shared Function GetJobList() As Dictionary(Of String, cJob)
            Dim Length As Integer
            Dim x As Integer
            Dim mCount As Integer
            Dim ret As Integer
            Dim ObjName As NativeStructs.ObjectNameInformation
            Dim Handle As NativeStructs.SystemHandleEntry
            Dim buf As New Dictionary(Of String, cJob)
            Dim hProcess As IntPtr
            Dim noNameJobIndex As Integer = 0

            semDupHandles.WaitOne()

            ' We have to get jobTypeNumber
            If jobTypeNumber = 0 Then
                jobTypeNumber = GetObjectTypeNumberByName("Job")
            End If

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

            ' Get the number of handles 
            mCount = memAllocJobs.ReadStruct(Of NativeStructs.SystemHandleInformation).HandleCount

            ' Resize our array
            Dim objTypeOffsetInStruct As Integer = NativeStructs.SystemHandleInformation_ObjectTypeOffset
            Dim structSize As Integer = Marshal.SizeOf(GetType(NativeStructs.SystemHandleEntry))
            Dim handlesOffset As Integer = NativeStructs.SystemHandleInformation.HandlesOffset

            For x = 0 To mCount - 1
                ' We do not retrieve the entire SystemHandleInformation struct
                ' cause it requires too much CPU time
                ' We just retrieve the Byte which represent the object type

                ' &h4 offset because of HandleCount on 4 first bytes
                Dim type As Integer = memAllocJobs.ReadByte(objTypeOffsetInStruct + _
                                                                    handlesOffset + _
                                                                    x * structSize)
                If type = jobTypeNumber Then

                    ' This is a job !

                    ' Get entire struct
                    ' &h4 offset because of HandleCount on 4 first bytes
                    Handle = memAllocJobs.ReadStruct(Of NativeStructs.SystemHandleEntry)(handlesOffset, x)

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
                            theName = Marshal.PtrToStringUni(ObjName.Name.Buffer)

                            ' Add to dico
                            ' The key is the name
                            If Not (String.IsNullOrEmpty(theName)) AndAlso buf.ContainsKey(theName) = False Then
                                Dim jj As cJob = New cJob(New jobInfos(theName))
                                buf.Add(theName, jj)
                            End If

                            ' Add handle to list
                            If _ownHandles.ContainsKey(theName) = False Then
                                _ownHandles.Add(theName, targetHandle)
                            End If

                        End If
                    Else

                        ' Open an handle to the process which owns our handle
                        hProcess = Native.Objects.Process.GetProcessHandleById(Handle.ProcessId, _
                                                        Security.ProcessAccess.DupHandle)

                        ' Duplicate the handle in our process with same access
                        NativeFunctions.DuplicateHandle(hProcess, New IntPtr(Handle.Handle), _
                                                        New IntPtr(NativeFunctions.GetCurrentProcess), _
                                                        targetHandle, Security.JobAccess.All, False, _
                                                        0)
                        ' Close process' handle
                        Objects.General.CloseHandle(hProcess)

                        If targetHandle.IsNotNull Then

                            NativeFunctions.ZeroMemory(BufferObjNameJob, New IntPtr(512))
                            NativeFunctions.NtQueryObject(targetHandle, _
                                            NativeEnums.ObjectInformationClass.ObjectNameInformation, _
                                            BufferObjNameJob.Pointer, 512, ret)
                            ObjName = BufferObjNameJob.ReadStruct(Of NativeStructs.ObjectNameInformation)(0)
                            theName = Marshal.PtrToStringUni(BufferObjNameJob.Pointer.Increment(8))

                            ' Add to dico only NAMED jobs
                            ' The key is theName
                            If String.IsNullOrEmpty(theName) = False Then
                                If buf.ContainsKey(theName) = False Then
                                    Dim jj As cJob = New cJob(New jobInfos(theName))
                                    buf.Add(theName, jj)
                                End If

                                ' Add the handle to the list of duplicated handles
                                ' So we will close it just before next enumeration
                                ' to avoid to multiple per 2 each enumeration the number
                                ' of handles... And to avoid JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE limit issue
                                If _dupHandles.ContainsKey(theName) = False Then
                                    _dupHandles.Add(theName, targetHandle)
                                Else
                                    Objects.General.CloseHandle(targetHandle)
                                End If
                            Else
                                ' Then the job has no name...
                                ' We will create a name for this job, as a name
                                ' is expected as a primary key for dictionaries.

                                noNameJobIndex += 1
                                theName = "(no name)[" & noNameJobIndex.ToString & "]"

                                If buf.ContainsKey(theName) = False Then
                                    Dim jj As cJob = New cJob(New jobInfos(theName))
                                    buf.Add(theName, jj)
                                End If

                                ' Add the handle to the list of duplicated handles
                                ' So we will close it just before next enumeration
                                ' to avoid to multiple per 2 each enumeration the number
                                ' of handles... And to avoid JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE limit issue
                                If _dupHandles.ContainsKey(theName) = False Then
                                    _dupHandles.Add(theName, targetHandle)
                                End If

                            End If

                        End If
                    End If
                End If

            Next

            semDupHandles.Release()

            Return buf

        End Function

        ' Query a job information struct
        Friend Shared Function QueryJobInformationByName(Of T)(ByVal name As String, _
                                ByVal info As NativeEnums.JobObjectInformationClass) As T
            Dim ret As Integer
            Dim retStruct As T = Nothing

            ' Query valid handle
            Dim handle As IntPtr = BeginUsingValidJobHandle(name)

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

                retStruct = struct
            End If

            ' End using handle
            EndUsingValidJobHandle()

            Return retStruct

        End Function
        Friend Shared Function QueryJobInformationByHandle(Of T)(ByVal handle As IntPtr, _
                        ByVal info As NativeEnums.JobObjectInformationClass) As T
            Dim ret As Integer
            Dim retStruct As T = Nothing

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

                retStruct = struct
            End If

            Return retStruct

        End Function

        ' Set a job information struct
        Friend Shared Function SetJobInformationByName(Of T)(ByVal name As String, _
                                ByVal info As NativeEnums.JobObjectInformationClass, _
                                ByVal limit As T) As Boolean

            Dim ret As Boolean

            ' Query valid handle
            Dim handle As IntPtr = BeginUsingValidJobHandle(name)

            If handle.IsNotNull Then
                Dim memAlloc As New Memory.MemoryAlloc(Marshal.SizeOf(GetType(T)))
                memAlloc.WriteStruct(Of T)(limit)
                ret = NativeFunctions.SetInformationJobObject(handle, info, memAlloc.Pointer, _
                                                        memAlloc.Size)
                memAlloc.Free()
            End If

            ' End using handle
            EndUsingValidJobHandle()

            Return ret

        End Function
        ' By handle (USE WITH CREATED JOBS ONLY !)
        Friend Shared Function SetJobInformationByHandle(Of T)(ByVal handle As IntPtr, _
                        ByVal info As NativeEnums.JobObjectInformationClass, _
                        ByVal limit As T) As Boolean

            Dim ret As Boolean

            If Handle.IsNotNull Then
                Dim memAlloc As New Memory.MemoryAlloc(Marshal.SizeOf(GetType(T)))
                memAlloc.WriteStruct(Of T)(limit)
                ret = NativeFunctions.SetInformationJobObject(Handle, info, memAlloc.Pointer, _
                                                        memAlloc.Size)
                memAlloc.Free()
            End If

            Return ret

        End Function

        ' Return ObjectTypeNumber associated to a ObjectType defined by its name
        Private Shared Function GetObjectTypeNumberByName(ByVal typeName As String) As Integer
            Dim cbReqLength As Integer
            Dim cTypeCount As Integer
            Dim x As Integer
            Dim TypeInfo As NativeStructs.ObjectTypeInformation
            Dim strType As String
            Dim offset As Integer

            ' Request size for types informations
            Dim memAlloc As New Memory.MemoryAlloc(&H100)
            NativeFunctions.NtQueryObject(IntPtr.Zero, NativeEnums.ObjectInformationClass.ObjectTypesInformation, memAlloc.Pointer, memAlloc.Size, cbReqLength)
            memAlloc.Resize(cbReqLength)

            ' Retrive list of types
            NativeFunctions.NtQueryObject(IntPtr.Zero, _
                            NativeEnums.ObjectInformationClass.ObjectTypesInformation, _
                            memAlloc.Pointer, cbReqLength, cbReqLength)

            ' Get number of struct to read (first 4 bytes)
            cTypeCount = memAlloc.ReadInt32(0)

            ' First 4 bytes are used for number of items to read
            offset = &H4
            For x = 0 To cTypeCount - 1
                ' Retrieve name of type
                TypeInfo = memAlloc.ReadStruct(Of NativeStructs.ObjectTypeInformation)(offset, x)
                strType = Common.Misc.ReadUnicodeString(TypeInfo.Name)
                If typeName = strType Then
                    Return x + 1
                End If
                offset += TypeInfo.Name.MaximumLength
                ' DWORD alignment
                offset += offset Mod 4
            Next
            memAlloc.Free()

            If typeName = "Driver" Then
                Return 24
            ElseIf typeName = "IoCompletion" Then
                Return 25
            ElseIf typeName = "File" Then
                Return 26
            Else
                Return -1
            End If

        End Function

    End Class

End Namespace
