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


Imports system.Text
Imports System.Runtime.InteropServices
Imports System.Security.Principal

Public Class WindowsSID

    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
    Shared Function LookupAccountSid(ByVal SystemName As String, _
        ByVal SID As Integer, ByVal Name As StringBuilder, ByRef NameSize As Integer, _
        ByVal ReferencedDomainName As StringBuilder, ByRef ReferencedDomainNameSize As Integer, _
        ByRef SID_NAME_USE As Integer) As Boolean
    End Function

    Private _id As SecurityIdentifier
    Private _name As String
    Private _nameNoDomain As String

    Public Sub New(ByVal SID As Integer)
        ' _id = New SecurityIdentifier(New IntPtr(SID))
        ' WellKnownSidType.AccountAdministratorSid()
        _name = GetAccountName(SID, True)
        _nameNoDomain = GetAccountName(SID, False)
    End Sub

    Public Function GetName(ByVal IncludeDomain As Boolean) As String
        If IncludeDomain Then
            Return _name
        Else
            Return _nameNoDomain
        End If
    End Function

    Public Function GetSecurityIdentifier() As SecurityIdentifier
        Return _id
    End Function

    Public Function GetStringSID() As String
        Return _id.ToString()
    End Function

    ' Return account name from SID
    Public Shared Function GetAccountName(ByVal SID As Integer, ByVal IncludeDomain As Boolean) As String
        Dim name As New StringBuilder(255)
        Dim domain As New StringBuilder(255)
        Dim namelen As Integer = 255
        Dim domainlen As Integer = 255
        Dim use As Integer = 1 ' SID_NAME_USE = SID_NAME_USE.SidTypeUser

        Try
            If Not LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use) Then
                ' if the name is longer than 255 characters, increase the capacity.
                name.EnsureCapacity(namelen)
                domain.EnsureCapacity(domainlen)

                If Not LookupAccountSid(Nothing, SID, name, namelen, domain, domainlen, use) Then
                    ' buggy
                End If
            End If
        Catch
            ' if we didn't find a name, then return the string SID version.
            'Return New System.Security.Principal.SecurityIdentifier(CType(SID, IntPtr)).ToString()
            Dim i As WellKnownSidType = CType(SID, WellKnownSidType)
            Return i.ToString
        End Try

        If IncludeDomain Then
            Return CStr(IIf(domain.ToString() <> "", domain.ToString() + "\\", "")) & name.ToString
        Else
            Return name.ToString()
        End If
    End Function

End Class
