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
    Private _simulationMode As Boolean = False
    Private frmConsole As New frmSBASimulationConsole


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
    Public ReadOnly Property ThresholdDescription() As String()
        Get
            Dim s(30) As String
            s(0) = "Integer"
            s(1) = "String"
            s(2) = "Integer"
            s(3) = "String"
            s(4) = "Decimal (%)"
            s(5) = "Decimal (%)"
            s(6) = "Decimal (sec)"
            s(7) = "Decimal (sec)"
            s(8) = "Decimal (sec)"
            s(9) = "Date (hh:mm:ss)"
            s(10) = "Date (hh:mm:ss)"
            s(11) = "Integer"
            s(12) = "Integer"
            s(13) = "Integer (mask : Sum(processor_number^2), processor_number start at 0)"
            s(14) = "Decimal (MB)"
            s(15) = "Decimal (MB)"
            s(16) = "Integer"
            s(17) = "Decimal (MB)"
            s(18) = "Decimal (MB)"
            s(19) = "Decimal (MB)"
            s(20) = "Decimal (MB)"
            s(21) = "Decimal (MB)"
            s(22) = "Decimal (MB)"
            s(23) = "Integer"
            s(24) = "Integer"
            s(25) = "Integer"
            s(26) = "Decimal (MB)"
            s(27) = "Decimal (MB)"
            s(28) = "Decimal (MB)"
            s(29) = "String enum (I-BN-N-AN-H-RT)"
            s(30) = "String"
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
            Dim s(30) As String
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
            s(23) = "ReadOperationCount"
            s(24) = "WriteOperationCount"
            s(25) = "OtherOperationCount"
            s(26) = "ReadTransferCount"
            s(27) = "WriteTransferCount"
            s(28) = "OtherTransferCount"
            s(29) = "Priority"
            s(30) = "Path"
            Return s
        End Get
    End Property
    Public Property SimulationMode() As Boolean
        Get
            Return _simulationMode
        End Get
        Set(ByVal value As Boolean)
            _simulationMode = value
        End Set
    End Property
    Public WriteOnly Property ShowConsole() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                frmConsole.Show()
            Else
                frmConsole.Hide()
            End If
        End Set
    End Property


    ' ========================================
    ' Public functions
    ' ========================================

    ' Clear console
    Public Sub ClearConsole()
        Me.frmConsole.lv.Items.Clear()
    End Sub

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

        Dim _dico As Dictionary(Of String, cProcess).ValueCollection = frmMain.lvProcess.GetAllItems

        For Each action As cBasedStateActionState In _col

            If action.Enabled Then

                ' Check if there is a process concerned
                Dim b As Boolean = False
                For Each _p As cProcess In _dico

                    If (action.CheckProcID And action.CheckProcIDS = _p.Pid.ToString) OrElse _
                        (action.CheckProcName And action.CheckProcNameS.ToLower = _p.Name.ToLower) Then
                        b = True
                    ElseIf action.CheckProcPath Then
                        ' Test process path
                        Dim _path As String = _p.Path
                        If action.CheckProcPathS.Length = 0 Then
                            b = True
                        Else
                            If action.CheckProcPathS.Substring(action.CheckProcPathS.Length - 1, 1) = "*" Then
                                b = (InStr(_path.ToLower, action.CheckProcPathS.ToLower.Replace("*", "")) > 0)
                            Else
                                b = (action.CheckProcPathS.ToLower = _path.ToLower)
                            End If
                        End If
                    End If

                    If b Then
                        ' Ok we found a process
                        ' Check state
                        If isStateOk(action, _p) Then
                            If _simulationMode = False Then
                                action.RaiseAction(_p)
                            Else
                                ' Just display an information
                                Dim _it As New ListViewItem(Date.Now.ToLongTimeString)
                                _it.SubItems.Add(_p.Name & " (" & _p.Pid.ToString & ")")
                                _it.SubItems.Add(action.ActionText)
                                _it.SubItems.Add(action.RuleText)
                                Me.frmConsole.lv.Items.Add(_it)
                            End If
                        End If
                            b = False
                        End If
                Next

            End If

        Next

    End Sub

    ' Check if process state is reached
    Private Function isStateOk(ByRef action As cBasedStateActionState, ByRef _p As cProcess) As Boolean

        Dim _currentValue As cBasedStateActionState.StateThreshold = _p.GetInformationAsStateThreshold(action.StateCounter)

        Select Case action.StateOperator
            Case cBasedStateActionState.STATE_OPERATOR.different_from
                Return (_currentValue <> action.Threshold)
            Case cBasedStateActionState.STATE_OPERATOR.equal
                Return (_currentValue = action.Threshold)
            Case cBasedStateActionState.STATE_OPERATOR.greater_or_equal_than
                Return (_currentValue >= action.Threshold)
            Case cBasedStateActionState.STATE_OPERATOR.greater_than
                Return (_currentValue > action.Threshold)
            Case cBasedStateActionState.STATE_OPERATOR.less_or_equal_than
                Return (_currentValue <= action.Threshold)
            Case cBasedStateActionState.STATE_OPERATOR.less_than
                Return (_currentValue < action.Threshold)
            Case Else
                Return False
        End Select

    End Function

End Class
