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
' - Declaration of some structures used by ZwQuerySystemInformation

Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices

Public Class windowInfos
    Inherits generalInfos

#Region "Private attributes"

    Protected _procName As String
    Protected _processId As Integer
    Protected _handle As IntPtr
    Protected _isTask As Boolean
    Protected _positions As API.RECT
    Protected _enabled As Boolean
    Protected _visible As Boolean
    Protected _threadId As Integer
    Protected _height As Integer
    Protected _width As Integer
    Protected _top As Integer
    Protected _left As Integer
    Protected _opacity As Byte

#End Region

#Region "Properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _processId
        End Get
    End Property
    Public ReadOnly Property IsTask() As Boolean
        Get
            Return _isTask
        End Get
    End Property
    Public ReadOnly Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
    End Property
    Public ReadOnly Property Visible() As Boolean
        Get
            Return _visible
        End Get
    End Property
    Public ReadOnly Property ThreadId() As Integer
        Get
            Return _threadId
        End Get
    End Property
    Public ReadOnly Property Height() As Integer
        Get
            Return _height
        End Get
    End Property
    Public ReadOnly Property Width() As Integer
        Get
            Return _width
        End Get
    End Property
    Public ReadOnly Property Top() As Integer
        Get
            Return _top
        End Get
    End Property
    Public ReadOnly Property Left() As Integer
        Get
            Return _left
        End Get
    End Property
    Public ReadOnly Property Opacity() As Byte
        Get
            Return _opacity
        End Get
    End Property
    Public ReadOnly Property Handle() As IntPtr
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property ProcessName() As String
        Get
            If _procName = vbNullString Then
                _procName = cProcess.GetProcessName(Me.ProcessId)
                If _procName = vbNullString Then
                    _procName = NO_INFO_RETRIEVED
                End If
            End If
            Return _procName
        End Get
    End Property
    Public ReadOnly Property Positions() As API.RECT
        Get
            Return _positions
        End Get
    End Property
#End Region


    ' ========================================
    ' Public
    ' ========================================

    'Public Structure LightWindow
    '    Public pid As Integer
    '    Public tid As Integer
    '    Public handle As IntPtr
    '    Public Sub New(ByVal _pid As Integer, ByVal _tid As Integer, ByVal _handle As IntPtr)
    '        pid = _pid
    '        tid = _tid
    '        handle = _handle
    '    End Sub
    'End Structure

    ' Constructor of this class
    'Public Sub New(ByRef window As LightWindow)
    '    _processId = window.pid
    '    _threadId = window.tid
    '    _handle = window.handle
    'End Sub
    Public Sub New(ByRef window As windowInfos)
        _processId = window.ProcessId
        _threadId = window.ThreadId
        _handle = window.handle
    End Sub
    Public Sub New(ByVal pid As Integer, ByVal tid As Integer, ByVal handle As IntPtr)
        _processId = pid
        _threadId = tid
        _handle = handle
    End Sub

    '' Merge an old and a new instance
    'Public Sub Merge(ByRef newI As windowInfos)

    '    With newI
    '        _enabled = .Enabled
    '        _height = .Height
    '        _isTask = .IsTask
    '        _left = .Left
    '        _opacity = .Opacity
    '        _top = .Top
    '        _visible = .Visible
    '        _width = .Width
    '    End With

    'End Sub
    Friend Sub SetNonFixedInfos(ByRef infos As asyncCallbackWindowGetNonFixedInfos.TheseInfos)
        With infos
            _enabled = .enabled
            _height = .height
            _isTask = .isTask
            _left = .left
            _opacity = .opacity
            _positions = .theRect
            _top = .top
            _visible = .visible
            _width = .width
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(10) As String

        s(0) = "Caption"
        s(1) = "Process"
        s(2) = "IsTask"
        s(3) = "Enabled"
        s(4) = "Visible"
        s(5) = "ThreadId"
        s(6) = "Height"
        s(7) = "Width"
        s(8) = "Top"
        s(9) = "Left"
        s(10) = "Opacity"

        Return s
    End Function

End Class
