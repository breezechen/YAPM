﻿' =======================================================
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

Public Class cMemRegion

    ' ========================================
    ' API declarations
    ' ========================================
    Private Enum PROTECTION_TYPE
        PAGE_EXECUTE = &H10
        PAGE_EXECUTE_READ = &H20
        PAGE_EXECUTE_READWRITE = &H40
        PAGE_EXECUTE_WRITECOPY = &H80
        PAGE_NOACCESS = &H1
        PAGE_READONLY = &H2
        PAGE_READWRITE = &H4
        PAGE_WRITECOPY = &H8
        PAGE_GUARD = &H100
        PAGE_NOCACHE = &H200
        PAGE_WRITECOMBINE = &H400
    End Enum
    Private Const NO_INFO_RETRIEVED As String = "N/A"
    Private Const MEM_PRIVATE As Integer = &H20000
    Private Const MEM_FREE As Integer = &H10000
    Private Const MEM_COMMIT As Integer = &H1000
    Private Const MEM_IMAGE As Integer = &H1000000
    Private Const MEM_MAPPED As Integer = &H40000
    Private Const MEM_RESERVE As Integer = &H2000


    ' ========================================
    ' Private attributes
    ' ========================================
    Private _type As Integer
    Private _protection As Integer
    Private _state As Integer
    Private _name As String
    Private _address As Integer
    Private _size As Integer
    Private _procId As Integer

    Private _isDisplayed As Boolean = False
    Private _killedItem As Boolean
    Private _newItem As Boolean


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal pid As Integer, ByVal m As cProcessMemRW.MEMORY_BASIC_INFORMATION)
        MyBase.New()
        _type = m.lType
        _protection = m.Protect
        _name = "NAME"
        _state = m.State
        _address = m.BaseAddress
        _size = m.RegionSize
        _procId = pid
    End Sub
    Public Sub New(ByVal memReg As cMemRegion)
        MyBase.New()
        _newItem = memReg.IsNewItem
        _killedItem = memReg.IsKilledItem
        _type = memReg.type
        _protection = memReg.protection
        _name = "NAME"
        _state = memReg.State
        _address = memReg.BaseAddress
        _size = memReg.RegionSize
        _procId = memReg.ProcessId
    End Sub


    ' ========================================
    ' Getter and setter
    ' ========================================
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _procId
        End Get
    End Property
    Public ReadOnly Property RegionSize() As Integer
        Get
            Return _size
        End Get
    End Property
    Public ReadOnly Property State() As Integer
        Get
            Return _state
        End Get
    End Property
    Public ReadOnly Property BaseAddress() As Integer
        Get
            Return _address
        End Get
    End Property
    Public ReadOnly Property Protection() As Integer
        Get
            Return _protection
        End Get
    End Property
    Public ReadOnly Property Type() As Integer
        Get
            Return _type
        End Get
    End Property
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


    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Return informations
    Public Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Type"
                res = GetTypeType(Me.Type)
            Case "Protection"
                res = GetProtectionType(Me.Protection)
            Case "State"
                res = GetStateType(Me.State)
            Case "Name"
                res = "NAME"
            Case "Address"
                res = "0x" & Me.BaseAddress.ToString("x")
            Case "Size"
                res = "0x" & Me.RegionSize.ToString("x")
        End Select

        Return res
    End Function

    ' Show a hax editor
    Public Sub ShowHexEditor()
        Dim frm As New frmHexEditor

        Dim reg As New MemoryHexEditor.control.MemoryRegion(Me.BaseAddress, Me.RegionSize)
        frm.SetPidAndRegion(_procId, reg)
        frm.Show()

    End Sub

    ' Get protection type as string
    Public Shared Function GetProtectionType(ByVal protec As Integer) As String
        Dim s As String = ""

        If (protec And PROTECTION_TYPE.PAGE_EXECUTE) = PROTECTION_TYPE.PAGE_EXECUTE Then
            s &= "E/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_EXECUTE_READ) = PROTECTION_TYPE.PAGE_EXECUTE_READ Then
            s &= "ERO/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_EXECUTE_READWRITE) = PROTECTION_TYPE.PAGE_EXECUTE_READWRITE Then
            s &= "ERW/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_EXECUTE_WRITECOPY) = PROTECTION_TYPE.PAGE_EXECUTE_WRITECOPY Then
            s &= "EWC/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_GUARD) = PROTECTION_TYPE.PAGE_GUARD Then
            s &= "G/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_NOACCESS) = PROTECTION_TYPE.PAGE_NOACCESS Then
            s &= "NA/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_NOCACHE) = PROTECTION_TYPE.PAGE_NOCACHE Then
            s &= "NC"
        End If
        If (protec And PROTECTION_TYPE.PAGE_READONLY) = PROTECTION_TYPE.PAGE_READONLY Then
            s &= "RO/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_READWRITE) = PROTECTION_TYPE.PAGE_READWRITE Then
            s &= "RW/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_WRITECOMBINE) = PROTECTION_TYPE.PAGE_WRITECOMBINE Then
            s &= "WCOMB/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_WRITECOPY) = PROTECTION_TYPE.PAGE_WRITECOPY Then
            s &= "WC/"
        End If

        If s.Length > 0 Then
            s = s.Substring(0, s.Length - 1)
        Else
            s = NO_INFO_RETRIEVED
        End If

        Return s
    End Function

    ' Get state type as string
    Public Shared Function GetStateType(ByVal state As Integer) As String
        Select Case state
            Case MEM_COMMIT
                Return "MEM_COMMIT"
            Case MEM_RESERVE
                Return "MEM_RESERVE"
            Case MEM_FREE
                Return "MEM_FREE"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function

    ' Get type type as string
    Public Shared Function GetTypeType(ByVal type As Integer) As String
        Select Case type
            Case MEM_IMAGE
                Return "MEM_IMAGE"
            Case MEM_PRIVATE
                Return "MEM_PRIVATE"
            Case MEM_MAPPED
                Return "MEM_MAPPED"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function

End Class
