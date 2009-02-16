' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Imports System.Runtime.InteropServices

Public Class cThread

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

#End Region

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _id As Integer                          ' Thread ID
    Private _procId As Integer = 0                  ' Process owner ID
    Private _procName As String                     ' Process owner name
    Private _Thread As ProcessThread
    Private _hThread As Integer


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal procId As Integer, ByVal procName As String, ByVal thread As ProcessThread)
        MyBase.New()
        _id = thread.Id
        _procId = procId
        _Thread = thread
        _hThread = OpenThread(QUERY_INFORMATION, 0, _id)
        _procName = procName
    End Sub
    Public Sub New(ByVal thread As cThread)
        MyBase.New()
        _id = thread.Id
        _procName = thread.ProcessName
        _procId = thread.ProcessId
        _Thread = thread.ProcessThread
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
    Public ReadOnly Property ProcessName() As String
        Get
            Return _procName
        End Get
    End Property
    Public ReadOnly Property BasePriority() As Integer
        Get
            Return _Thread.BasePriority
        End Get
    End Property

    Public ReadOnly Property ThreadState() As String
        Get
            Return _Thread.ThreadState.ToString
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
            Return CInt(_Thread.StartAddress)
        End Get
    End Property

    Public ReadOnly Property StartTime() As Date
        Get
            Return _Thread.StartTime
        End Get
    End Property

    Public ReadOnly Property UserProcessorTime() As TimeSpan
        Get
            Return _Thread.UserProcessorTime
        End Get
    End Property

    Public ReadOnly Property TotalProcessorTime() As TimeSpan
        Get
            Return _Thread.TotalProcessorTime
        End Get
    End Property

    Public ReadOnly Property PrivilegedProcessorTime() As TimeSpan
        Get
            Return _Thread.PrivilegedProcessorTime
        End Get
    End Property

    Public Property PriorityBoostEnabled() As Boolean
        Get
            Return _Thread.PriorityBoostEnabled
        End Get
        Set(ByVal value As Boolean)
            _Thread.PriorityBoostEnabled = value
        End Set
    End Property

    Public ReadOnly Property WaitReason() As String
        Get
            If _Thread.ThreadState = Diagnostics.ThreadState.Wait Then
                Return _Thread.WaitReason.ToString
            Else
                Return "Not waiting"
            End If
        End Get
    End Property
    '
    Public Property ProcessorAffinity() As Integer
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            _Thread.ProcessorAffinity = CType(value, IntPtr)
        End Set
    End Property
    '
    Public Property IdealProcessor() As Integer
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            _Thread.IdealProcessor = value
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
    ' Retrieve thread list
    Public Shared Function Enumerate(ByVal processId As Integer, ByRef t() As cThread) As Integer
        'Dim f As Boolean
        'Dim hSnap As Integer
        'Dim THREAD As THREADENTRY32

        'hSnap = CreateToolhelp32Snapshot(TH32CS_SNAPTHREAD, 0)
        'If hSnap = 0 Then Return -1

        'ReDim t(0)
        'THREAD.dwSize = Len(THREAD)
        'f = Thread32First(hSnap, THREAD)

        'Do While f
        '    ReDim Preserve t(UBound(t) + 1)
        '    f = Thread32Next(hSnap, THREAD)
        'Loop

        'Return UBound(t)

        Try
            Dim p As Process = Process.GetProcessById(processId)
            Dim tT As ProcessThread
            Dim count As Integer = p.Threads.Count
            Dim i As Integer = 0

            ReDim t(count - 1)
            For Each tT In p.Threads
                'Try
                t(i) = New cThread(processId, p.MainModule.ModuleName, tT)
                i += 1
                'Catch ex As Exception
                ''
                'End Try
            Next
            Return count

        Catch ex As Exception
            ' Process has been killed
            ReDim t(0)
            Return 0
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
