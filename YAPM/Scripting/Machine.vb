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

Namespace Scripting.Items

    Public Class Machine

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Type
        Private _type As Enums.MachineType

        ' Name/IP/local
        Private _name As String

        ' User
        Private _user As String

        ' Password
        Private _pwd As String

        ' Connection
        Private WithEvents _con As New cConnection

        ' Commands
        Private _com As List(Of Scripting.Items.Command)


        ' ========================================
        ' Public properties
        ' ========================================

        ' Connection
        Public Property Connection() As cConnection
            Get
                Return _con
            End Get
            Set(ByVal value As cConnection)
                _con = value
            End Set
        End Property

        ' Type
        Public Property Type() As Enums.MachineType
            Get
                Return _type
            End Get
            Set(ByVal value As Enums.MachineType)
                _type = value
            End Set
        End Property

        ' Name
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        ' User
        Public Property UserName() As String
            Get
                Return _user
            End Get
            Set(ByVal value As String)
                _user = value
            End Set
        End Property

        ' Password
        Public Property Password() As String
            Get
                Return _pwd
            End Get
            Set(ByVal value As String)
                _pwd = value
            End Set
        End Property



        ' ========================================
        ' Public functions
        ' ========================================

        ' Constructors
        Public Sub New(ByVal type As Enums.MachineType, ByVal name As String, _
                       ByVal user As String, ByVal password As String)
            Me.Type = type
            Me.Name = name
            Me.UserName = user
            Me.Password = password
        End Sub
        Public Sub New(ByVal commandLine As String)
            ' Here we parse the line
            Try
                Dim s() As String = commandLine.Split(CChar(","))
                Me.Type = DirectCast([Enum].Parse(GetType(Enums.MachineType), s(0), True), Enums.MachineType)
                If Me.Type <> Enums.MachineType.Local Then
                    Me.Name = s(1)
                    Me.UserName = s(2)
                    Me.Password = s(3)
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End Sub

        ' Execute commands
        Public Sub ExecuteCommands(ByVal commands As Object)

            ' Get commands
            _com = CType(commands, Global.System.Collections.Generic.List(Of Global.Scripting.Items.Command))

            ' Connect to machine
            Select Case Me.Type
                Case Enums.MachineType.Local
                    Me.Connection.ConnectionType = cConnection.TypeOfConnection.LocalConnection
                Case Enums.MachineType.Wmi
                    Me.Connection.ConnectionType = cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    Dim ss As New System.Security.SecureString
                    For Each c As Char In Me.Password
                        ss.AppendChar(c)
                    Next
                    Me.Connection.WmiParameters = New cConnection.WMIConnectionParameters(Me.Name, Me.UserName, ss)
            End Select
            ' Connect now
            Connection.Connect()

        End Sub


        ' ========================================
        ' Private functions
        ' ========================================

        ' Machine is connected
        Private Sub _con_Connected() Handles _con.Connected
            ' Execute the commands now
            For Each com As Scripting.Items.Command In _com
                com.Execute(Me.Connection)
            Next
        End Sub

    End Class

End Namespace
