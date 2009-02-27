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

Public Class cTask
    Inherits cWindow

    ' ========================================
    ' API declaration
    ' ========================================
#Region "API"

    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    Private Declare Function GetWindowAPI Lib "user32" Alias "GetWindow" (ByVal hWnd As IntPtr, ByVal wCmd As Integer) As IntPtr
    Private Declare Auto Function GetDesktopWindow Lib "user32.dll" () As IntPtr
    Private Const GW_CHILD As Integer = 5
    Private Const GW_HWNDNEXT As Integer = 2

#End Region

    Private _key As String
    Private _proc As cProcess

    ' ========================================
    ' Constructors
    ' ========================================
    Public Sub New(ByVal task As LightWindow)
        MyBase.New(task)
        _proc = New cProcess(task.pid)
        _key = task.handle.ToString & "|" & task.pid.ToString
        _proc.ProcessorCount = frmMain.cInfo.ProcessorCount
    End Sub

    ' ========================================
    ' Public
    ' ========================================
    Public ReadOnly Property CpuUsageS() As String
        Get
            Dim o As Integer = _proc.ProcessorCount
            Return GetFormatedPercentage(_proc.CpuPercentageUsage)
        End Get
    End Property
    Public ReadOnly Property Process() As cProcess
        Get
            Return _proc
        End Get
    End Property
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property

    Public Overloads Function GetInformation(ByVal info As String) As String
        Select Case info
            Case "CpuUsage"
                Return CpuUsageS
            Case Else
                Return MyBase.GetInformation(info)
        End Select
    End Function

    ' Retrieve all tasks
    Public Overloads Shared Function Enumerate(ByRef key() As String, _
                                               ByRef _dico As Dictionary(Of String, LightWindow)) As Integer
        Dim currWnd As IntPtr
        Dim cpt As Integer

        _dico.Clear()
        currWnd = GetWindowAPI(GetDesktopWindow(), GW_CHILD)
        cpt = 0
        ReDim key(0)
        Do While Not (currWnd = IntPtr.Zero)

            If _isTask(currWnd) Then

                ' Get procId from hwnd
                Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)

                ReDim Preserve key(cpt)
                key(cpt) = currWnd.ToString & "|" & pid.ToString
                _dico.Add(key(cpt), New LightWindow(currWnd, pid, GetThreadIdFromWindowHandle(currWnd)))
                cpt += 1
            End If

            currWnd = GetWindowAPI(currWnd, GW_HWNDNEXT)
        Loop

        Return key.Length

    End Function

    ' Retrieve all information's names availables
    Public Shared Shadows Function GetAvailableProperties() As String()
        Dim s(12) As String

        s(0) = "Caption"
        s(1) = "CpuUsage"
        s(2) = "Process"
        s(3) = "Caption"
        s(4) = "IsTask"
        s(5) = "Enabled"
        s(6) = "Visible"
        s(7) = "ThreadId"
        s(8) = "Height"
        s(9) = "Width"
        s(10) = "Top"
        s(11) = "Left"
        s(12) = "Opacity"

        Return s
    End Function
End Class
