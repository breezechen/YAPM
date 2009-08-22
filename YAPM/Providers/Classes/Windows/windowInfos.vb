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

<Serializable()> Public Class windowInfos
    Inherits generalInfos

#Region "Private attributes"

    Protected _procName As String
    Protected _processId As Integer
    Protected _handle As IntPtr
    Protected _isTask As Boolean
    Protected _positions As Native.Api.NativeStructs.Rect
    Protected _enabled As Boolean
    Protected _visible As Boolean
    Protected _threadId As Integer
    Protected _height As Integer
    Protected _width As Integer
    Protected _top As Integer
    Protected _left As Integer
    Protected _opacity As Byte
    Protected _caption As String

#End Region

#Region "Properties"

    Friend ReadOnly Property Caption() As String
        Get
            Return _caption
        End Get
    End Property
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
    Public ReadOnly Property Positions() As Native.Api.NativeStructs.Rect
        Get
            Return _positions
        End Get
    End Property
#End Region


    ' ========================================
    ' Public
    ' ========================================
    Public Sub New(ByRef window As windowInfos)
        _processId = window.ProcessId
        _threadId = window.ThreadId
        _handle = window.Handle
        _caption = window.Caption
    End Sub
    Public Sub New(ByVal pid As Integer, ByVal tid As Integer, ByVal handle As IntPtr, ByVal caption As String)
        _processId = pid
        _threadId = tid
        _handle = handle
        _caption = caption
    End Sub

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
            _caption = .caption
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
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

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Id"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
