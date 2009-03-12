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
    Inherits cGeneralObject

    Public Structure LightHandle
        Dim handle As Integer
        Dim type As String
        Dim pid As Integer
        Dim name As String
        Dim handleCount As Integer
        Dim pointerCount As Integer
        Dim objectCount As Integer
    End Structure

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
    Private _key As String

    Private _first As Boolean


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal key As String, ByRef ent As LightHandle)
        _key = key
        With ent
            _handle = .handle
            _handleCount = .handleCount
            _pid = .pid
            _type = .type
            _name = .name
            _objectCount = .objectCount
            _pointerCount = .pointerCount
        End With
    End Sub


    ' ========================================
    ' Getter and setter
    ' ========================================
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
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
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Type"
                res = Me.Type
            Case "Name"
                res = Me.Name
            Case "HandleCount"
                res = Me.HandleCount.ToString
            Case "PointerCount"
                res = Me.PointerCount.ToString
            Case "ObjectCount"
                res = Me.ObjectCount.ToString
            Case "Handle"
                res = Me.Handle.ToString
            Case "Process"
                res = _pid.ToString
        End Select

        Return res
    End Function



    ' ========================================
    ' Shared functions
    ' ========================================
    ' Retrieve handle list
    Public Shared Function Enumerate(ByVal processId As Integer(), ByVal showUNN As _
                                     Boolean, ByRef key() As String, ByRef _dico As  _
                                     Dictionary(Of String, cHandle.LightHandle)) As Integer

        _dico.Clear()

        Try
            Call frmMain.handles_Renamed.Refresh(processId)

            ReDim key(frmMain.handles_Renamed.Count)      ' Temporary size
            Dim x As Integer = 0
            For i As Integer = 0 To frmMain.handles_Renamed.Count - 1
                If frmMain.handles_Renamed.GetHandle(i) > 0 Then
                    If showUNN OrElse (Len(frmMain.handles_Renamed.GetObjectName(i)) > 0) Then
                        With frmMain.handles_Renamed
                            key(x) = .GetProcessID(i).ToString & "|" & .GetHandle(i).ToString & "|" & .GetNameInformation(i) & "|" & .GetObjectName(i) & "|"
                            Dim ret As cHandle.LightHandle
                            With frmMain.handles_Renamed
                                ret.handleCount = .GetHandleCount(i)
                                ret.handle = .GetHandle(i)
                                ret.name = .GetObjectName(i)
                                ret.objectCount = .GetObjectCount(i)
                                ret.pid = .GetProcessID(i)
                                ret.pointerCount = .GetPointerCount(i)
                                ret.type = .GetNameInformation(i)
                            End With
                            _dico.Add(key(x), ret)
                        End With
                        x += 1
                    End If
                End If
            Next

            ' Resize array
            ReDim Preserve key(x - 1)
            Return x

        Catch ex As Exception
            ' Process has been killed
            ReDim key(0)
            Return 0
        End Try

    End Function
    Public Shared Function EnumerateAll(ByVal showUNN As Boolean, ByRef key() As String, ByRef _dico As  _
                                     Dictionary(Of String, cHandle.LightHandle)) As Integer

        _dico.Clear()

        Try
            Call frmMain.handles_Renamed.Refresh()

            ReDim key(frmMain.handles_Renamed.Count)      ' Temporary size
            Dim x As Integer = 0
            For i As Integer = 0 To frmMain.handles_Renamed.Count - 1
                If frmMain.handles_Renamed.GetHandle(i) > 0 Then
                    If showUNN OrElse (Len(frmMain.handles_Renamed.GetObjectName(i)) > 0) Then
                        With frmMain.handles_Renamed
                            key(x) = .GetProcessID(i).ToString & "|" & .GetHandle(i).ToString & "|" & .GetNameInformation(i) & "|" & .GetObjectName(i) & "|"
                            Dim ret As cHandle.LightHandle
                            With frmMain.handles_Renamed
                                ret.handleCount = .GetHandleCount(i)
                                ret.handle = .GetHandle(i)
                                ret.name = .GetObjectName(i)
                                ret.objectCount = .GetObjectCount(i)
                                ret.pid = .GetProcessID(i)
                                ret.pointerCount = .GetPointerCount(i)
                                ret.type = .GetNameInformation(i)
                            End With
                            _dico.Add(key(x), ret)
                        End With
                        x += 1
                    End If
                End If
            Next

            ' Resize array
            ReDim Preserve key(x - 1)
            Return x

        Catch ex As Exception
            ' Process has been killed
            ReDim key(0)
            Return 0
        End Try

    End Function

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties() As String()
        Dim s(5) As String

        s(0) = "Type"
        s(1) = "Name"
        s(2) = "HandleCount"
        s(3) = "PointerCount"
        s(4) = "ObjectCount"
        s(5) = "Process"

        Return s
    End Function

    ' Get a key from a light handle
    Public Shared Function GetKeyFromLight(ByRef light As LightHandle) As String
        Return light.pid.ToString & "|" & light.handle.ToString & "|" & light.type & "|" & light.name & "|"
    End Function
End Class
