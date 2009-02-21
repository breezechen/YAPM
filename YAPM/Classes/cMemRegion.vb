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

Public Class cMemRegion
    Inherits cGeneralObject

    ' ========================================
    ' API declarations
    ' ========================================
    Public Enum PROTECTION_TYPE As Integer
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
    Public Enum MEMORY_STATE As Integer
        MEM_FREE = &H10000
        MEM_COMMIT = &H1000
        MEM_RESERVE = &H2000
    End Enum
    Public Enum MEMORY_TYPE As Integer
        MEM_IMAGE = &H1000000
        MEM_PRIVATE = &H20000
        MEM_MAPPED = &H40000
    End Enum
    Public Structure LightMemRegion
        Dim type As MEMORY_TYPE
        Dim protection As PROTECTION_TYPE
        Dim state As MEMORY_STATE
        Dim name As String
        Dim address As Integer
        Dim size As Integer
        Dim procId As Integer
    End Structure

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _type As MEMORY_TYPE
    Private _protection As PROTECTION_TYPE
    Private _state As MEMORY_STATE
    Private _name As String
    Private _address As Integer
    Private _size As Integer
    Private _procId As Integer
    Private _key As String


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal key As String, ByRef ent As  _
                   cProcessMemRW.MEMORY_BASIC_INFORMATION, ByVal pid As Integer)

        _key = key
        With ent
            _address = ent.BaseAddress
            _type = CType(ent.lType, MEMORY_TYPE)
            _procId = pid
            _protection = CType(ent.Protect, PROTECTION_TYPE)
            _state = CType(ent.State, MEMORY_STATE)
            _size = ent.RegionSize
        End With
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
    Public ReadOnly Property State() As MEMORY_STATE
        Get
            Return _state
        End Get
    End Property
    Public ReadOnly Property BaseAddress() As Integer
        Get
            Return _address
        End Get
    End Property
    Public ReadOnly Property Protection() As PROTECTION_TYPE
        Get
            Return _protection
        End Get
    End Property
    Public ReadOnly Property Type() As MEMORY_TYPE
        Get
            Return _type
        End Get
    End Property
    Public ReadOnly Property Key() As String
        Get
            Return _key
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            If _name = vbNullString Then
                _name = getName()
            End If
            Return _name
        End Get
    End Property
    

    ' ========================================
    ' Public functions of this class
    ' ========================================

    ' Return informations
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = ""

        Select Case info
            Case "Type"
                res = Me.Type.ToString
            Case "Protection"
                res = Me.Protection.ToString
            Case "State"
                res = Me.State.ToString
            Case "Name"
                res = Me.Name
            Case "Address"
                res = "0x" & Me.BaseAddress.ToString("x")
            Case "Size"
                res = getSizeString()
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
    Public Shared Function GetProtectionType(ByVal protec As PROTECTION_TYPE) As String
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
    Public Shared Function GetStateType(ByVal state As MEMORY_STATE) As String
        Select Case state
            Case MEMORY_STATE.MEM_COMMIT
                Return "MEM_COMMIT"
            Case MEMORY_STATE.MEM_RESERVE
                Return "MEM_RESERVE"
            Case MEMORY_STATE.MEM_FREE
                Return "MEM_FREE"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function

    ' Get type type as string
    Public Shared Function GetTypeType(ByVal type As MEMORY_TYPE) As String
        Select Case type
            Case MEMORY_TYPE.MEM_IMAGE
                Return "MEM_IMAGE"
            Case MEMORY_TYPE.MEM_PRIVATE
                Return "MEM_PRIVATE"
            Case MEMORY_TYPE.MEM_MAPPED
                Return "MEM_MAPPED"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function


    ' ========================================
    ' Private functions of this class
    ' ========================================
    Private Function getName() As String
        If _state = MEMORY_STATE.MEM_FREE Then
            Return "Free"
        ElseIf _type = MEMORY_TYPE.MEM_IMAGE Then
            ' Should return MODULE name
            Return "Image"
        Else
            Return _type.ToString & " (" & _state.ToString & ")"
        End If
    End Function

    ' Get size as a string
    Private Function getSizeString() As String
        Static oldSize As Integer = _size
        Static _sizeStr As String = GetFormatedSize(_size)

        If Not (_size = oldSize) Then
            _sizeStr = GetFormatedSize(_size)
            oldSize = _size
        End If

        Return _sizeStr

    End Function
End Class
