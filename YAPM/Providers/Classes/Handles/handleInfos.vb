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


Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Api

<Serializable()> Public Class handleInfos
    Inherits generalInfos

#Region "Friend attributes"

    ' Ok, all these attributes are FRIEND because HandleEnumeration needs to access
    ' to them, but let's assume the user won't corrupt these variables...

    Friend _ProcessID As Integer 'ID du processus propriétaire
    Friend _Flags As NativeEnums.HandleFlags  ' 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
    Friend _Handle As IntPtr  'valeur du handle
    Friend _ObjectName As String 'nom de l'objet
    Friend _NameInformation As String 'type de l'ojet
    Friend _ObjectAddress As IntPtr  'adresse de l'objet
    Friend _GrantedAccess As Native.Security.StandardRights  'accès autorisés à l'objet
    Friend _Attributes As NativeEnums.HandleFlags 'attributs
    Friend _HandleCount As Integer 'nombre de handle de ce type dans le système
    Friend _PointerCount As UInteger 'nombre de références pointeurs à cet objet dans le système
    Friend _CreateTime As Decimal 'date de création de l'objet
    Friend _GenericRead As Integer 'accès générique
    Friend _GenericWrite As Integer
    Friend _GenericExecute As Integer
    Friend _GenericAll As Integer
    Friend _ObjectCount As Integer '
    Friend _PeakObjectCount As Integer '
    Friend _PeakHandleCount As Integer '
    Friend _InvalidAttributes As Integer 'définit les attributs invalides pour ce type d'objet
    Friend _ValidAccess As Integer '
    Friend _Unknown As Byte
    Friend _MaintainHandleDatabase As UShort  '
    Friend _PoolType As NativeEnums.PoolType  'type de pool utilisé par l'objet
    Friend _PagedPoolUsage As Integer 'paged pool utilisé
    Friend _NonPagedPoolUsage As Integer 'non-paged pool utilisé

#End Region

#Region "Read only properties"

    Public ReadOnly Property Key() As String
        Get
            Return _ProcessID.ToString & "-" & _Handle.ToString
        End Get
    End Property

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _ProcessID
        End Get
    End Property
    Public ReadOnly Property Handle() As IntPtr
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property Type() As String
        Get
            Return _NameInformation
        End Get
    End Property
    Public ReadOnly Property Name() As String
        Get
            Return _ObjectName
        End Get
    End Property
    Public ReadOnly Property HandleCount() As Integer
        Get
            Return _HandleCount
        End Get
    End Property
    Public ReadOnly Property PointerCount() As UInteger
        Get
            Return _pointerCount
        End Get
    End Property
    Public ReadOnly Property ObjectCount() As Integer
        Get
            Return _objectCount
        End Get
    End Property
    Public ReadOnly Property ObjectAddress() As IntPtr
        Get
            Return _ObjectAddress
        End Get
    End Property
    Public ReadOnly Property GrantedAccess() As Native.Security.StandardRights
        Get
            Return _GrantedAccess
        End Get
    End Property
    Public ReadOnly Property Attributes() As NativeEnums.HandleFlags
        Get
            Return _Attributes
        End Get
    End Property
    Public ReadOnly Property CreateTime() As Decimal
        Get
            Return _CreateTime
        End Get
    End Property
    Public ReadOnly Property PagedPoolUsage() As Integer
        Get
            Return _PagedPoolUsage
        End Get
    End Property
    Public ReadOnly Property NonPagedPoolUsage() As Integer
        Get
            Return _NonPagedPoolUsage
        End Get
    End Property

#End Region


    ' ========================================
    ' Public
    ' ========================================

    ' Merge an old and a new instance
    Public Sub New()
        '
    End Sub
    Public Sub Merge(ByRef newI As handleInfos)
        With newI
            _Handle = .Handle
            _HandleCount = .HandleCount
            _ObjectName = .Name
            _ObjectCount = .ObjectCount
            _PointerCount = .PointerCount
            _ProcessID = .ProcessId
            _NameInformation = .Type
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared  Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(10) As String

        s(0) = "Name"
        s(1) = "HandleCount"
        s(2) = "PointerCount"
        s(3) = "ObjectCount"
        s(4) = "Process"
        s(5) = "ObjectAddress"
        s(6) = "GrantedAccess"
        s(7) = "Attributes"
        s(8) = "CreateTime"
        s(9) = "PagedPoolUsage"
        s(10) = "NonPagedPoolUsage"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Type"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
