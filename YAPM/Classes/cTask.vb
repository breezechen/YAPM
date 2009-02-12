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


    Private _proc As cProcess

    ' ========================================
    ' Constructors
    ' ========================================
    Public Sub New(ByVal handle As Integer, ByVal threadId As Integer, _
        ByRef proc As cProcess)

        MyBase.New(handle, proc.Pid, threadId, proc.Name)
        _proc = proc
    End Sub
    Public Sub New(ByVal task As cTask)
        MyBase.New(task)
        _proc = task.process
    End Sub


    ' ========================================
    ' Public
    ' ========================================
    Public ReadOnly Property CpuUsageS() As String
        Get
            Return GetFormatedPercentage(_proc.CpuPercentageUsage)
        End Get
    End Property
    Public ReadOnly Property Process() As cProcess
        Get
            Return _proc
        End Get
    End Property

    Public Overloads Function GetInformation(ByVal info As String) As String
        Select Case info
            Case "CpuUsage"
                Return CpuUsageS
            Case  Else
                Return MyBase.GetInformation(info)
        End Select
    End Function

    ' Retrieve all tasks
    Public Shared Function EnumerateAllTasks(ByRef w() As cTask) As Integer
        Dim currWnd As IntPtr
        Dim cpt As Integer

        currWnd = GetWindowAPI(GetDesktopWindow(), GW_CHILD)
        cpt = 0
        ReDim w(0)
        Do While Not (currWnd = IntPtr.Zero)

            If _isTask(CType(currWnd, IntPtr)) Then

                ' Get procId from hwnd
                Dim pid As Integer = GetProcIdFromWindowHandle(currWnd)

                ReDim Preserve w(cpt)
                w(cpt) = New cTask(CInt(currWnd), GetThreadIdFromWindowHandle(currWnd), New cProcess(pid))
                cpt += 1
            End If

            currWnd = GetWindowAPI(currWnd, GW_HWNDNEXT)
        Loop

        Return UBound(w)

    End Function
End Class
