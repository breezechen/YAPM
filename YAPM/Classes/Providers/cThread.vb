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

Imports System.Runtime.InteropServices

Public Class cThread
    Inherits cGeneralObject

    Implements IDisposable

    ' ========================================
    ' API declarations
    ' ========================================
#Region "API"
    Private Declare Function CloseHandle Lib "Kernel32.dll" (ByVal hObject As Integer) As Integer
    Private Declare Function OpenThread Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwThreadId As Integer) As Integer

    Private Declare Function CreateToolhelp32Snapshot Lib "kernel32.dll" (ByVal dwFlags As Integer, ByVal th32ProcessID As Integer) As Integer
    Private Declare Function Thread32First Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean
    Private Declare Function Thread32Next Lib "kernel32.dll" (ByVal hSnapshot As Integer, ByRef lpte As THREADENTRY32) As Boolean

    <DllImport("kernel32.dll")> _
    Private Shared Function ResumeThread(ByVal hThread As IntPtr) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Private Shared Function SuspendThread(ByVal hThread As IntPtr) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Private Shared Function SetThreadPriority(ByVal hThread As IntPtr, ByVal priority As Integer) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Private Shared Function TerminateThread(ByVal hThread As IntPtr, ByVal exitcode As Integer) As UInt32
    End Function
    <DllImport("kernel32.dll")> _
    Private Shared Function GetThreadPriority(ByVal hThread As Integer) As UInt32
    End Function

    Private Declare Function NtQueryInformationThread Lib "Ntdll.dll" (ByVal hThread As Integer, ByVal ThreadInformationClass As Integer, ByRef ThreadInformation As THREAD_BASIC_INFORMATION, ByVal ThreadInformationLength As Integer, ByVal ReturnLength As Integer) As Integer
    Private Declare Function NtSetInformationThread Lib "Ntdll.dll" (ByVal hThread As Integer, ByVal ThreadInformationClass As Integer, ByVal ThreadInformation As Integer, ByVal ThreadInformationLength As Integer) As Integer

    <DllImport("ntdll.dll", SetLastError:=True)> _
    Private Shared Function ZwQueryInformationThread(ByVal ThreadHandle As Integer, ByVal ThreadInformationClass As THREADINFOCLASS, ByRef ThreadInformation As UInteger, ByVal ThreadInformationLength As Integer, ByRef ReturnLength As Integer) As Integer
    End Function


    ' ========================================
    ' Constants
    ' ========================================
    Private Const TH32CS_SNAPTHREAD As Integer = &H4
    Private Const TERMINATE As Integer = &H1
    Private Const SUSPEND_RESUME As Integer = &H2
    Private Const GET_CONTEXT As Integer = &H8
    Private Const SET_CONTEXT As Integer = &H10
    Private Const SET_INFORMATION As Integer = &H20
    Private Const QUERY_INFORMATION As Integer = &H40
    Private Const SET_THREAD_TOKEN As Integer = &H80
    Private Const IMPERSONATE As Integer = &H100
    Private Const DIRECT_IMPERSONATION As Integer = &H200

    ' ========================================
    ' Structures for API
    ' ========================================
    Private Structure THREADENTRY32
        Dim dwSize As Integer
        Dim cntUsage As Integer
        Dim th32ThreadID As Integer
        Dim th32OwnerProcessID As Integer
        Dim tpBasePri As Integer
        Dim tpDeltaPri As Integer
        Dim dwFlags As Integer
    End Structure
    Private Structure CLIENT_ID
        Dim UniqueProcess As Integer
        Dim UniqueThread As Integer
    End Structure
    Private Structure THREAD_BASIC_INFORMATION
        Dim ExitStatus As Integer
        Dim TebBaseAddress As Integer
        Dim ClientId As CLIENT_ID
        Dim AffinityMask As Integer
        Dim Priority As Integer
        Dim BasePriority As Integer
    End Structure
    Public Enum ThreadPriority As Integer
        Idle = -15
        Lowest = -2
        BelowNormal = -1
        Normal = 0
        AboveNormal = 1
        Highest = 2
        Critical = 15
        Unknow = 13
    End Enum

    Private Enum THREADINFOCLASS
        ThreadBasicInformation
        ThreadTimes
        ThreadPriority
        ThreadBasePriority
        ThreadAffinityMask
        ThreadImpersonationToken
        ThreadDescriptorTableEntry
        ThreadEnableAlignmentFaultFixup
        ThreadEventPair_Reusable
        ThreadQuerySetWin32StartAddress
        ThreadZeroTlsCell
        ThreadPerformanceCount
        ThreadAmILastThread
        ThreadIdealProcessor
        ThreadPriorityBoost
        ThreadSetTlsArrayAddress
        ThreadIsIoPending
        ThreadHideFromDebugger
        ThreadBreakOnTermination
        ThreadSwitchLegacyState
        ThreadIsTerminated
        ThreadLastSystemCall
        ThreadIoPriority
        ThreadCycleTime
        ThreadPagePriority
        ThreadActualBasePriority
        ThreadTebInformation
        ThreadCSwitchMon
        MaxThreadInfoClas
    End Enum

    Public Structure LightThread
        Dim t As System.Diagnostics.ProcessThread
        Dim pid As Integer
        Public Sub New(ByVal apid As Integer, ByRef at As ProcessThread)
            pid = apid
            t = at
        End Sub
    End Structure

#End Region

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _id As Integer                          ' Thread ID
    Private _procId As Integer = 0                  ' Process owner ID
    Private _procName As String                     ' Process owner name
    Private _Thread As ProcessThread
    Private _hThread As Integer
    Private _key As String


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByRef t As LightThread)
        MyBase.New()
        _id = t.t.Id
        _procId = t.pid
        _Thread = t.t
        _hThread = OpenThread(QUERY_INFORMATION, 0, _id)
        _procName = cProcess.GetProcessName(_procId)
        _key = _procId.ToString & "|" & _Thread.Id.ToString
    End Sub
    Protected Overloads Overrides Sub Finalize()
        If _Thread IsNot Nothing Then
            _Thread.Dispose()
        End If
        MyBase.Finalize()
        CloseHandle(_hThread)
    End Sub
    Public Sub Dispose() Implements System.IDisposable.Dispose
        If _Thread IsNot Nothing Then
            _Thread.Dispose()
        End If
        _Thread = Nothing
        CloseHandle(_hThread)
    End Sub


    ' ========================================
    ' Getter and setter
    ' ========================================
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public ReadOnly Property ProcessName() As String
        Get
            Return _procName
        End Get
    End Property
    Public ReadOnly Property BasePriority() As Integer
        Get
            Try
                Return _Thread.BasePriority
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public ReadOnly Property ThreadState() As String
        Get
            Try
                Return _Thread.ThreadState.ToString
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    Public Property Priority() As ThreadPriority
        Get
            Return CType(GetThreadPriority(_hThread), ThreadPriority)
        End Get
        Set(ByVal value As ThreadPriority)
            SetPriority(value)
        End Set
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return _id
        End Get
    End Property

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _procId
        End Get
    End Property

    Public ReadOnly Property StartAddress() As Integer
        Get
            Try
                Return CInt(_Thread.StartAddress)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public ReadOnly Property StartTime() As Date
        Get
            Try
                Return _Thread.StartTime
            Catch ex As Exception
                Return New Date(0)
            End Try
        End Get
    End Property

    Public ReadOnly Property UserProcessorTime() As TimeSpan
        Get
            Try
                Return _Thread.UserProcessorTime
            Catch ex As Exception
                Return New TimeSpan(0)
            End Try
        End Get
    End Property

    Public ReadOnly Property TotalProcessorTime() As TimeSpan
        Get
            Try
                Return _Thread.TotalProcessorTime
            Catch ex As Exception
                Return New TimeSpan(0)
            End Try
        End Get
    End Property

    Public ReadOnly Property PrivilegedProcessorTime() As TimeSpan
        Get
            Try
                Return _Thread.PrivilegedProcessorTime
            Catch ex As Exception
                Return New TimeSpan(0)
            End Try
        End Get
    End Property

    Public Property PriorityBoostEnabled() As Boolean
        Get
            Try
                Return _Thread.PriorityBoostEnabled
            Catch ex As Exception
                Return False
            End Try
        End Get
        Set(ByVal value As Boolean)
            Try
                _Thread.PriorityBoostEnabled = value
            Catch ex As Exception
                '
            End Try
        End Set
    End Property

    Public ReadOnly Property WaitReason() As String
        Get
            Try
                If _Thread.ThreadState = Diagnostics.ThreadState.Wait Then
                    Return _Thread.WaitReason.ToString
                Else
                    Return "Not waiting"
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    '
    Public Property ProcessorAffinity() As Integer
        Get
            Dim _aff As Integer = 0
            Dim TBI As THREAD_BASIC_INFORMATION

            If _hThread > 0 Then
                NtQueryInformationThread(_hThread, 0, TBI, Marshal.SizeOf(TBI), 0)
                _aff = TBI.AffinityMask
            End If

            Return _aff
        End Get
        Set(ByVal value As Integer)
            Try
                _Thread.ProcessorAffinity = CType(value, IntPtr)
            Catch ex As Exception
                '
            End Try
        End Set
    End Property

    Public Property IdealProcessor() As Integer
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            ' _Thread.IdealProcessor = value
        End Set
    End Property

    Public ReadOnly Property ProcessThread() As ProcessThread
        Get
            Return _Thread
        End Get
    End Property

    Public ReadOnly Property PriorityString() As String
        Get
            Return Me.PriorityFromInt(Me.Priority)
        End Get
    End Property


    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Return informations
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Id"
                res = CStr(Me.Id)
            Case "Priority"
                res = Me.PriorityString
            Case "State"
                res = Me.ThreadState
            Case "WaitReason"
                res = Me.WaitReason
            Case "StartTime"
                res = CStr(Me.StartTime.ToLongDateString & " -- " & Me.StartTime.ToLongTimeString)
            Case "TotalProcessorTime"
                res = Me.TotalProcessorTime.ToString
            Case "OwnerProcessId"
                res = Me.ProcessId.ToString
            Case "UserProcessorTime"
                res = Me.UserProcessorTime.ToString
            Case "StartAddress"
                res = "0x" & Me.StartAddress.ToString("x")
            Case "PrivilegedProcessorTime"
                res = Me.PrivilegedProcessorTime.ToString
            Case "OwnerProcessName"
                res = Me.ProcessName
            Case "OwnerProcess"
                res = Me.ProcessName & " -- " & Me.ProcessId.ToString
            Case "PriorityBoost"
                res = Me.PriorityBoostEnabled.ToString
            Case "ProcessorAffinity"
                res = Me.ProcessorAffinity.ToString
            Case "IdealProcessor"
                res = Me.IdealProcessor.ToString
        End Select

        Return res
    End Function

    ' Reset ideal processor
    Public Sub ResetIdealProcessor()
        _Thread.ResetIdealProcessor()
    End Sub

    ' Suspend thread
    Public Function ThreadSuspend() As Integer
        Dim hThread As Integer
        Dim r As Integer = -1

        hThread = OpenThread(SUSPEND_RESUME, 0, _id)

        If hThread > 0 Then
            r = CInt(SuspendThread(CType(hThread, IntPtr)))
            CloseHandle(hThread)
        End If

        Return r

    End Function

    ' Resume thread
    Public Function ThreadResume() As Integer
        Dim hThread As Integer
        Dim r As Integer = -1

        hThread = OpenThread(SUSPEND_RESUME, 0, _id)

        If hThread > 0 Then
            r = CInt(ResumeThread(CType(hThread, IntPtr)))
            CloseHandle(hThread)
        End If

        Return r
    End Function

    ' Terminate thread
    Public Function ThreadTerminate() As Integer
        Dim hThread As Integer
        Dim r As Integer = -1

        hThread = OpenThread(TERMINATE, 0, _id)

        If hThread > 0 Then
            r = CInt(TerminateThread(CType(hThread, IntPtr), 0))
            CloseHandle(hThread)
        End If

        Return r
    End Function





    ' ========================================
    ' Shared functions
    ' ========================================

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(13) As String

        s(0) = "Priority"
        s(1) = "State"
        s(2) = "WaitReason"
        s(3) = "StartTime"
        s(4) = "UserProcessorTime"
        s(5) = "PrivilegedProcessorTime"
        s(6) = "TotalProcessorTime"
        s(7) = "OwnerProcessId"
        s(8) = "OwnerProcessName"
        s(9) = "OwnerProcess"
        s(10) = "StartAddress"
        s(11) = "PriorityBoost"
        s(12) = "ProcessorAffinity"
        s(13) = "IdealProcessor"

        Return s
    End Function

    ' Retrieve thread list
    Public Shared Function Enumerate(ByVal processId As Integer(), ByRef key() As String, _
                                     ByRef _dico As Dictionary(Of String, LightThread)) As Integer

        _dico.Clear()
        ReDim key(0)

        For Each pid As Integer In processId

            Try
                Dim p As Process = Process.GetProcessById(pid)
                Dim tT As ProcessThread
                Dim count As Integer = p.Threads.Count

                Dim i As Integer = key.Length - 1
                ReDim Preserve key(i + count - 1)

                For Each tT In p.Threads
                    key(i) = pid.ToString & "|" & tT.Id.ToString
                    _dico.Add(key(i), New LightThread(pid, tT))
                    i += 1
                Next

            Catch ex As Exception
                ' Process has been killed
                ReDim Preserve key(key.Length - 2)
            End Try

        Next

    End Function
    Public Shared Function Enumerate(ByVal pid As Integer, ByRef key() As String, _
                                     ByRef _dico As Dictionary(Of String, LightThread)) As Integer

        _dico.Clear()
        ReDim key(0)

        Try
            Dim p As Process = Process.GetProcessById(pid)
            Dim tT As ProcessThread
            Dim count As Integer = p.Threads.Count

            Dim i As Integer = key.Length - 1
            ReDim Preserve key(i + count - 1)

            For Each tT In p.Threads
                key(i) = pid.ToString & "|" & tT.Id.ToString
                _dico.Add(key(i), New LightThread(pid, tT))
                i += 1
            Next

        Catch ex As Exception
            ' Process has been killed
            ReDim Preserve key(key.Length - 2)
        End Try

    End Function

    ' ========================================
    ' Private functions
    ' ========================================
    Private Function PriorityFromInt(ByVal i As Integer) As String
        Select Case i
            Case System.Diagnostics.ThreadPriorityLevel.AboveNormal
                Return "AboveNormal"
            Case System.Diagnostics.ThreadPriorityLevel.BelowNormal
                Return "BelowNormal"
            Case System.Diagnostics.ThreadPriorityLevel.Highest
                Return "Highest"
            Case System.Diagnostics.ThreadPriorityLevel.Idle
                Return "Idle"
            Case System.Diagnostics.ThreadPriorityLevel.Lowest
                Return "Lowest"
            Case System.Diagnostics.ThreadPriorityLevel.Normal
                Return "Normal"
            Case System.Diagnostics.ThreadPriorityLevel.TimeCritical
                Return "TimeCritical"
            Case Else
                Return i.ToString
        End Select
    End Function

    ' Set priority
    Private Function SetPriority(ByVal level As ThreadPriority) As Integer
        Dim hThread As Integer
        Dim r As Integer = -1

        hThread = OpenThread(SET_INFORMATION, 0, _id)

        If hThread > 0 Then
            r = CInt(SetThreadPriority(CType(hThread, IntPtr), level))
            CloseHandle(hThread)
        End If

        Return r
    End Function
End Class
