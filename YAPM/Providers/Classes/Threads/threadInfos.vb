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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - Declaration of some structures used by NtQuerySystemInformation

Option Strict On

Imports System.Runtime.InteropServices

<Serializable()> Public Class threadInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _AffinityMask As IntPtr
    Private _KernelTime As Long
    Private _UserTime As Long
    Private _CreateTime As Long
    Private _WaitTime As Integer
    Private _StartAddress As IntPtr
    Private _Id As Integer
    Private _ProcessId As Integer
    Private _Priority As Integer
    Private _BasePriority As Integer
    Private _ContextSwitchCount As Integer
    Private _State As ThreadState
    Private _WaitReason As API.KWAIT_REASON

#End Region

#Region "Read only properties"

    Public ReadOnly Property TotalTime() As Long
        Get
            Return _KernelTime + _UserTime
        End Get
    End Property
    Public ReadOnly Property KernelTime() As Long
        Get
            Return _KernelTime
        End Get
    End Property
    Public ReadOnly Property UserTime() As Long
        Get
            Return _UserTime
        End Get
    End Property
    Public ReadOnly Property CreateTime() As Long
        Get
            Return _CreateTime
        End Get
    End Property
    Public ReadOnly Property WaitTime() As Integer
        Get
            Return _WaitTime
        End Get
    End Property
    Public ReadOnly Property StartAddress() As Integer
        Get
            Return _StartAddress
        End Get
    End Property
    Public ReadOnly Property Id() As Integer
        Get
            Return _Id
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _ProcessId
        End Get
    End Property
    Public ReadOnly Property Priority() As System.Diagnostics.ThreadPriorityLevel
        Get
            Return getPriorityClass(_Priority)
        End Get
    End Property
    Public ReadOnly Property BasePriority() As System.Diagnostics.ThreadPriorityLevel
        Get
            Return getPriorityClass(_BasePriority)
        End Get
    End Property
    Public ReadOnly Property ContextSwitchCount() As Integer
        Get
            Return _ContextSwitchCount
        End Get
    End Property
    Public ReadOnly Property State() As ThreadState
        Get
            Return _State
        End Get
    End Property
    Public ReadOnly Property WaitReason() As API.KWAIT_REASON
        Get
            Return _WaitReason
        End Get
    End Property

    Public ReadOnly Property ContextSwitchDelta() As Integer
        Get
            Static oldCount As Integer = Me.ContextSwitchCount
            Dim res As Integer = Me.ContextSwitchCount - oldCount
            oldCount = Me.ContextSwitchCount
            Return res
        End Get
    End Property

    Friend Sub SetPriority(ByVal i As Integer)
        _Priority = i
    End Sub

#End Region

#Region "Other Non-fixed informations"

    Public Property AffinityMask() As IntPtr
        Get
            Return _AffinityMask
        End Get
        Set(ByVal value As IntPtr)
            _AffinityMask = value
        End Set
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Constructor of this class
    Public Sub New(ByRef Thr As Native.Api.NativeStructs.SYSTEM_THREAD_INFORMATION)

        With Thr
            _AffinityMask = IntPtr.Zero
            _Id = .ClientId.UniqueThread.ToInt32
            _ProcessId = .ClientId.UniqueProcess.ToInt32
            _BasePriority = .BasePriority
            _ContextSwitchCount = .ContextSwitchCount
            _CreateTime = .CreateTime
            _KernelTime = .KernelTime
            _Priority = .Priority
            _StartAddress = .StartAddress
            _State = CType(.State, ThreadState)
            _UserTime = .UserTime
            _WaitReason = .WaitReason
            _WaitTime = .WaitTime
        End With

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As threadInfos)

        With newI
            _AffinityMask = .AffinityMask
            _BasePriority = .BasePriority
            _ContextSwitchCount = .ContextSwitchCount
            _CreateTime = .CreateTime
            _KernelTime = .KernelTime
            _Priority = .Priority
            _State = .State
            _UserTime = .UserTime
            _WaitReason = .WaitReason
            _WaitTime = .WaitTime
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False) As String()
        Dim s(13) As String

        s(0) = "Priority"
        s(1) = "State"
        s(2) = "WaitReason"
        s(3) = "CreateTime"
        s(4) = "KernelTime"
        s(5) = "UserTime"
        s(6) = "WaitTime"
        s(7) = "TotalTime"
        s(8) = "StartAddress"
        s(9) = "BasePriority"
        s(10) = "AffinityMask"
        s(11) = "ContextSwitchCount"
        s(12) = "ContextSwitchDelta"
        s(13) = "ProcessId"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Id"
            s = s2
        End If

        Return s
    End Function

    ' Return a class from an int (concerning priority)
    Friend Shared Function getPriorityClass(ByVal priority As Integer) As System.Diagnostics.ThreadPriorityLevel
        If priority >= 15 Then
            Return ThreadPriorityLevel.TimeCritical
        ElseIf priority >= 2 Then
            Return ThreadPriorityLevel.Highest
        ElseIf priority >= 1 Then
            Return ThreadPriorityLevel.AboveNormal
        ElseIf priority >= 0 Then
            Return ThreadPriorityLevel.Normal
        ElseIf priority >= -1 Then
            Return ThreadPriorityLevel.BelowNormal
        ElseIf priority >= -2 Then
            Return ThreadPriorityLevel.Lowest
        Else
            Return ThreadPriorityLevel.Idle
        End If
    End Function

End Class
