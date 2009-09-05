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

Public Class cEnvVariable
    Inherits cGeneralObject

    Private _envInfos As envVariableInfos
    Private Shared WithEvents _connection As cEnvVariableConnection


#Region "Properties"

    Public Shared Property Connection() As cEnvVariableConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cEnvVariableConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As envVariableInfos)
        _envInfos = infos
        _connection = Connection
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.EnvironmentVariable
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As envVariableInfos
        Get
            Return _envInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As envVariableInfos)
        _envInfos.Merge(Thr)
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
            Case "Variable"
                res = Me.Infos.Variable
            Case "Value"
                res = Me.Infos.Value
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Variable As String = ""
        Static _old_Value As String = ""

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
            Case "Variable"
                res = Me.Infos.Variable
                If res = _old_Variable Then
                    hasChanged = False
                Else
                    _old_Variable = res
                End If
            Case "Value"
                res = Me.Infos.Value
                If res = _old_Value Then
                    hasChanged = False
                Else
                    _old_Value = res
                End If
        End Select

        Return hasChanged
    End Function

#End Region

#Region "Shared functions (local)"

    Public Shared Function CurrentEnvVariables(ByVal process As cProcess) As Dictionary(Of String, cEnvVariable)
        Dim _dico As New Dictionary(Of String, cEnvVariable)

        Dim var() As String = Nothing
        Dim val() As String = Nothing
        Native.Objects.EnvVariable.GetEnvironmentVariablesBycProcess(process, var, val)

        For x As Integer = 0 To var.Length - 1
            If _dico.ContainsKey(var(x)) = False Then
                _dico.Add(var(x), New cEnvVariable(New envVariableInfos(var(x), val(x), process.Infos.ProcessId)))
            End If
        Next

        Return _dico
    End Function

#End Region

End Class
