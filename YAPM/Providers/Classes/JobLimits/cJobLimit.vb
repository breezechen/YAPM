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

Imports System.Net
Imports YAPM.Native.Api

Public Class cJobLimit
    Inherits cGeneralObject
    Implements IDisposable

    ' Infos
    Private _jobLimitsInfos As jobLimitInfos

    Private Shared WithEvents _connection As cJobLimitConnection


#Region "Properties"

    Public Shared Property Connection() As cJobLimitConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cJobLimitConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As jobLimitInfos)
        _jobLimitsInfos = infos
        _connection = Connection
        _TypeOfObject = Enums.GeneralObjectType.JobLimit
    End Sub
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' This object will be cleaned up by the Dispose method.
        ' Therefore, you should call GC.SupressFinalize to
        ' take this object off the finalization queue 
        ' and prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not Me.disposed Then
            ' If disposing equals true, dispose all managed 
            ' and unmanaged resources.
            If disposing Then
                ' Dispose managed resources.

            End If

            ' Note disposing has been done.
            disposed = True

        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As jobLimitInfos
        Get
            Return _jobLimitsInfos
        End Get
    End Property

#End Region


    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As jobLimitInfos)
        _jobLimitsInfos.Merge(Thr)
    End Sub


#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Limit"
                res = Me.Infos.Name
            Case "Value"
                res = Me.Infos.Value
            Case "Description"
                res = Me.Infos.Description
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Value As String = ""
        Static _old_Desc As String = ""
        Static _old_Name As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Limit"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "Value"
                res = Me.Infos.Value
                If res = _old_Value Then
                    hasChanged = False
                Else
                    _old_Value = res
                End If
            Case "Description"
                res = Me.Infos.Description
                If res = _old_Desc Then
                    hasChanged = False
                Else
                    _old_Desc = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

End Class
