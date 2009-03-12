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
' aInteger with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Imports System.Runtime.InteropServices

Public Class cStateBasedActions

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _col As New Collection


    ' ========================================
    ' Public properties
    ' ========================================
    Public ReadOnly Property StateBasedActionCollection() As Collection
        Get
            Return _col
        End Get
    End Property
    Public ReadOnly Property ActionsAvailable() As String()
        Get
            Dim s(23) As String
            s(0) = "Kill process"
            s(1) = "Pause process"
            s(2) = "Resume process"
            s(3) = "Change priority"
            s(4) = "Reduce priority"
            s(5) = "Increase priority"
            s(6) = "Activate process log"
            s(7) = "Change affinity"
            s(8) = "Launch a command"
            s(9) = "Restart computer"
            s(10) = "Shutdown computer"
            s(11) = "Poweroff computer"
            s(12) = "Sleep computer"
            s(13) = "Hibernate computer"
            s(14) = "Logoff computer"
            s(15) = "Lock computer"
            s(16) = "Exit YAPM"
            s(17) = "Show process task windows"
            s(18) = "Hide process task windows"
            s(19) = "Maximize process task windows"
            s(20) = "Minimize process task windows"
            s(21) = "Beep"
            s(22) = "Save process list"
            s(23) = "Save service list"
            Return s
        End Get
    End Property
    Public ReadOnly Property Param1Description() As String()
        Get
            Dim s(23) As String
            s(0) = "None"
            s(1) = "None"
            s(2) = "None"
            s(3) = "New priority (I-BN-N-AN-H-RT)"
            s(4) = "None"
            s(5) = "None"
            s(6) = "None"
            s(7) = "New affinity (mask : Sum(processor_number^2), processor_number start at 0)"
            s(8) = "Command to launch"
            s(9) = "None"
            s(10) = "None"
            s(11) = "None"
            s(12) = "None"
            s(13) = "None"
            s(14) = "None"
            s(15) = "None"
            s(16) = "None"
            s(17) = "None"
            s(18) = "None"
            s(19) = "None"
            s(20) = "None"
            s(21) = "Beep time (ms)"
            s(22) = "Report path (path without file name)"
            s(23) = "Report path (path without file name)"
            Return s
        End Get
    End Property
    Public ReadOnly Property Param2Description() As String()
        Get
            Dim s(23) As String
            s(0) = "None"
            s(1) = "None"
            s(2) = "None"
            s(3) = "None"
            s(4) = "None"
            s(5) = "None"
            s(6) = "None"
            s(7) = "None"
            s(8) = "None"
            s(9) = "None"
            s(10) = "None"
            s(11) = "None"
            s(12) = "None"
            s(13) = "None"
            s(14) = "None"
            s(15) = "None"
            s(16) = "None"
            s(17) = "None"
            s(18) = "None"
            s(19) = "None"
            s(20) = "None"
            s(21) = "None"
            s(22) = "Report file name"
            s(23) = "Report file name"
            Return s
        End Get
    End Property
    Public ReadOnly Property CounterAvailables() As String()
        Get
            Dim s(24) As String
            s(0) = "PID"
            s(1) = "UserName"
            s(2) = "ParentPID"
            s(3) = "ParentName"
            s(4) = "CpuUsage"
            s(5) = "AverageCpuUsage"
            s(6) = "KernelCpuTime"
            s(7) = "UserCpuTime"
            s(8) = "TotalCpuTime"
            s(9) = "StartTime"
            s(10) = "RunTime"
            s(11) = "GdiObjects"
            s(12) = "UserObjects"
            s(13) = "AffinityMask"
            s(14) = "WorkingSet"
            s(15) = "PeakWorkingSet"
            s(16) = "PageFaultCount"
            s(17) = "PagefileUsage"
            s(18) = "PeakPagefileUsage"
            s(19) = "QuotaPeakPagedPoolUsage"
            s(20) = "QuotaPagedPoolUsage"
            s(21) = "QuotaPeakNonPagedPoolUsage"
            s(22) = "QuotaNonPagedPoolUsage"
            s(23) = "Priority"
            s(24) = "Path"
            Return s
        End Get
    End Property


    ' ========================================
    ' Public functions
    ' ========================================

    ' Add a key to collection
    Public Function AddStateBasedAction(ByVal action As cBasedStateActionState) As Boolean
        Dim sKey As String = action.Key
        Try
            _col.Add(Key:=sKey, Item:=action)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Remove key from collection
    Public Function RemoveStateBasedAction(ByVal action As cBasedStateActionState) As Boolean
        Dim sKey As String = action.Key
        Try
            _col.Remove(sKey)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RemoveStateBasedAction(ByVal action As String) As Boolean
        Try
            _col.Remove(action)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Process actions
    Public Sub ProcessActions()
        For Each _it As cBasedStateActionState In _col
            If haveToRaise(_it) Then
                _it.RaiseAction()
            End If
        Next
    End Sub

    ' Return true if action must be raised
    Public Function haveToRaise(ByRef action As cBasedStateActionState) As Boolean
        '
        '
        Return False
    End Function
End Class
