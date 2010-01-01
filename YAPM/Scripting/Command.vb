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

    Public Class Command

        ' This is an example of command :
        ' process,kill,pid,eq,520
        ' litteral : object,action,condition,operator,name,arg1

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Object
        Private _object As Enums.Object

        ' Operation
        Private _action As String

        ' Condition
        Private _condition As Enums.Condition

        ' Operator
        Private _operator As Enums.Operator

        ' Name
        Private _name As String

        ' Arg1
        Private _arg1 As String



        ' ========================================
        ' Public properties
        ' ========================================

        ' Object
        Public Property [Object]() As Enums.Object
            Get
                Return _object
            End Get
            Set(ByVal value As Enums.Object)
                _object = value
            End Set
        End Property

        ' Action
        Public Property Action() As String
            Get
                Return _action
            End Get
            Set(ByVal value As String)
                _action = value
            End Set
        End Property

        ' User
        Public Property Condition() As Enums.Condition
            Get
                Return _condition
            End Get
            Set(ByVal value As Enums.Condition)
                _condition = value
            End Set
        End Property

        ' Password
        Public Property [Operator]() As Enums.Operator
            Get
                Return _operator
            End Get
            Set(ByVal value As Enums.Operator)
                _operator = value
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

        ' Arg1
        Public Property Arg1() As String
            Get
                Return _arg1
            End Get
            Set(ByVal value As String)
                _arg1 = value
            End Set
        End Property



        ' ========================================
        ' Public functions
        ' ========================================

        ' Constructors
        Public Sub New(ByVal commandLine As String)
            ' Here we parse the line
            ' Example :
            ' process,kill,name,eq,explorer.exe
            ' litteral : object,action,condition,operator,name,arg1
            Try
                Dim s() As String = commandLine.Split(CChar(","))
                Me.Object = DirectCast([Enum].Parse(GetType(Enums.Object), s(0), True), Enums.Object)
                Me.Action = s(1)
                Me.Condition = DirectCast([Enum].Parse(GetType(Enums.Condition), s(2), True), Enums.Condition)
                Me.Operator = DirectCast([Enum].Parse(GetType(Enums.Operator), s(3), True), Enums.Operator)
                Me.Name = s(4)
                Me.Arg1 = s(5)
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End Sub

        ' Execute command
        Public Sub Execute(ByVal connection As cConnection)
            Select Case Me.Object

                Case Enums.Object.Process
                    ' Process
                    Dim sAction As Enums.ProcessOperation = CType([Enum].Parse(GetType(Enums.ProcessOperation), Me.Action, True), Enums.ProcessOperation)
                    Select Case sAction
                        Case Enums.ProcessOperation.Kill
                            Dim pid As Integer = Integer.Parse(Me.Name)
                            Call Me.Kill(pid, connection)
                        Case Enums.ProcessOperation.KillTree

                        Case Enums.ProcessOperation.Pause

                        Case Enums.ProcessOperation.Resume

                        Case Enums.ProcessOperation.SetAffinity

                        Case Enums.ProcessOperation.SetPriority

                    End Select

                Case Enums.Object.Service
                    ' Service

            End Select
        End Sub


        ' ========================================
        ' Private functions
        ' ========================================

#Region "Implementation of actions"

        Private _killP As asyncCallbackProcKill
        Private Function Kill(ByVal pid As Integer, ByVal con As cConnection) As Integer

            If _killP Is Nothing Then
                _killP = New asyncCallbackProcKill(New asyncCallbackProcKill.HasKilled(AddressOf killDone))
            End If

            Dim t As New System.Threading.WaitCallback(AddressOf _killP.Process)
            Dim newAction As Integer = cGeneralObject.GetActionCount

            Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
                asyncCallbackProcKill.poolObj(pid, newAction))

        End Function
        Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
           '
        End Sub


#End Region

    End Class

End Namespace
