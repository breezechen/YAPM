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

Public Class cMemRegion
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _memInfos As memRegionInfos
    Private Shared WithEvents _connection As cMemRegionConnection


#Region "Properties"

    Public Shared Property Connection() As cMemRegionConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cMemRegionConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As memRegionInfos)
        _memInfos = infos
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As memRegionInfos
        Get
            Return _memInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As memRegionInfos)
        _memInfos.Merge(Thr)
    End Sub

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        End If

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Type"
                res = Me.Infos.Type.ToString
            Case "Protection"
                res = GetProtectionType(Me.Infos.Protection)
            Case "State"
                res = Me.Infos.State.ToString
            Case "Name"
                res = Me.Infos.Name
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
            Case "Size"
                res = getSizeString()
        End Select

        Return res
    End Function

    ' Get size as a string
    Private Function getSizeString() As String
        Static oldSize As Integer = Me.Infos.RegionSize
        Static _sizeStr As String = GetFormatedSize(Me.Infos.RegionSize)

        If Not (Me.Infos.RegionSize = oldSize) Then
            _sizeStr = GetFormatedSize(Me.Infos.RegionSize)
            oldSize = Me.Infos.RegionSize
        End If

        Return _sizeStr

    End Function
#End Region

    ' Get protection type as string
    Friend Shared Function GetProtectionType(ByVal protec As API.PROTECTION_TYPE) As String
        Dim s As String = ""

        If (protec And API.PROTECTION_TYPE.PAGE_EXECUTE) = API.PROTECTION_TYPE.PAGE_EXECUTE Then
            s &= "E/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_EXECUTE_READ) = API.PROTECTION_TYPE.PAGE_EXECUTE_READ Then
            s &= "ERO/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_EXECUTE_READWRITE) = API.PROTECTION_TYPE.PAGE_EXECUTE_READWRITE Then
            s &= "ERW/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_EXECUTE_WRITECOPY) = API.PROTECTION_TYPE.PAGE_EXECUTE_WRITECOPY Then
            s &= "EWC/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_GUARD) = API.PROTECTION_TYPE.PAGE_GUARD Then
            s &= "G/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_NOACCESS) = API.PROTECTION_TYPE.PAGE_NOACCESS Then
            s &= "NA/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_NOCACHE) = API.PROTECTION_TYPE.PAGE_NOCACHE Then
            s &= "NC"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_READONLY) = API.PROTECTION_TYPE.PAGE_READONLY Then
            s &= "RO/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_READWRITE) = API.PROTECTION_TYPE.PAGE_READWRITE Then
            s &= "RW/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_WRITECOMBINE) = API.PROTECTION_TYPE.PAGE_WRITECOMBINE Then
            s &= "WCOMB/"
        End If
        If (protec And API.PROTECTION_TYPE.PAGE_WRITECOPY) = API.PROTECTION_TYPE.PAGE_WRITECOPY Then
            s &= "WC/"
        End If

        If s.Length > 0 Then
            s = s.Substring(0, s.Length - 1)
        Else
            s = NO_INFO_RETRIEVED
        End If

        Return s
    End Function
End Class
