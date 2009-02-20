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

Public Class cHandle

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _handle As Integer
    Private _type As String
    Private _pid As Integer
    Private _name As String
    Private _handleCount As Integer
    Private _pointerCount As Integer
    Private _objectCount As Integer

    Private _isDisplayed As Boolean = False
    Private _killedItem As Boolean
    Private _newItem As Boolean
    Private _first As Boolean


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal procId As Integer, ByVal handle As Integer, ByVal type _
                   As String, ByVal name As String, ByVal hCount As Integer, _
                   ByVal pCount As Integer, ByVal oCount As Integer)

        MyBase.New()

        _pid = procId
        _handle = handle
        _type = type
        _name = name
        _handleCount = hCount
        _pointerCount = pCount
        _objectCount = oCount
        _first = True
    End Sub
    Public Sub New(ByVal handle As cHandle)
        MyBase.New()

        _pid = handle.ProcessID
        _handle = handle.Handle
        _type = handle.Type
        _name = handle.Name
        _handleCount = handle.HandleCount
        _pointerCount = handle.PointerCount
        _objectCount = handle.ObjectCount
        _first = handle.FirstTime

        _newItem = handle.IsNewItem
        _killedItem = handle.IsKilledItem
    End Sub


    ' ========================================
    ' Getter and setter
    ' ========================================
    Public Property isDisplayed() As Boolean
        Get
            Return _isDisplayed
        End Get
        Set(ByVal value As Boolean)
            _isDisplayed = value
        End Set
    End Property
    Public Property IsKilledItem() As Boolean
        Get
            Return _killedItem
        End Get
        Set(ByVal value As Boolean)
            _killedItem = value
        End Set
    End Property
    Public Property IsNewItem() As Boolean
        Get
            Return _newItem
        End Get
        Set(ByVal value As Boolean)
            _newItem = value
        End Set
    End Property
    Public ReadOnly Property ProcessID() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property Handle() As Integer
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property Type() As String
        Get
            Return _type
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property HandleCount() As Integer
        Get
            Return _handleCount
        End Get
    End Property
    Public ReadOnly Property PointerCount() As Integer
        Get
            Return _pointerCount
        End Get
    End Property
    Public ReadOnly Property ObjectCount() As Integer
        Get
            Return _objectCount
        End Get
    End Property
    Public Property FirstTime() As Boolean
        Get
            Return _first
        End Get
        Set(ByVal value As Boolean)
            _first = value
        End Set
    End Property

    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Return informations
    Public Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Type"
                res = Me.Type
            Case "Name"
                res = Me.Name
            Case "HandleCount"
                res = CStr(Me.HandleCount)
            Case "PointerCount"
                res = CStr(Me.PointerCount)
            Case "ObjectCount"
                res = CStr(Me.ObjectCount)
            Case "Handle"
                res = CStr(Me.Handle)
        End Select

        Return res
    End Function



    ' ========================================
    ' Shared functions
    ' ========================================
    ' Retrieve handle list
    Public Shared Function Enumerate(ByVal processId As Integer, ByRef t() As cHandle) As Integer

        Try
            Call frmMain.handles_Renamed.Refresh(processId)

            ReDim t(frmMain.handles_Renamed.Count)      ' Temporary size
            Dim x As Integer = 0
            For i As Integer = 0 To frmMain.handles_Renamed.Count - 1
                If frmMain.handles_Renamed.GetProcessID(i) = processId Then
                    With frmMain.handles_Renamed
                        t(x) = New cHandle(processId, .GetHandle(i), _
                                           .GetNameInformation(i), .GetObjectName(i), _
                                           .GetHandleCount(i), .GetPointerCount(i), _
                                           .GetObjectCount(i))
                    End With
                    x += 1
                End If
            Next

            ' Resize array
            ReDim Preserve t(x - 1)
            Return x

        Catch ex As Exception
            ' Process has been killed
            ReDim t(0)
            Return 0
        End Try

    End Function

End Class
