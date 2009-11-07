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

Namespace Scripting

    Public Class Engine

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Script (as plain text)
        Private _script As String

        ' Machines
        Private _machines As New List(Of Scripting.Items.Machine)

        ' Commands 
        Private _commands As New List(Of Scripting.Items.Command)

        ' Threads
        Private _threads As New List(Of System.Threading.Thread)



        ' ========================================
        ' Public properties
        ' ========================================

        ' Script (as plain text)
        Public Property Script() As String
            Get
                Return _script
            End Get
            Set(ByVal value As String)
                _script = value
            End Set
        End Property

        ' List of machines
        Public Property Machines() As List(Of Scripting.Items.Machine)
            Get
                Return _machines
            End Get
            Set(ByVal value As List(Of Scripting.Items.Machine))
                _machines = value
            End Set
        End Property

        ' Commands 
        Public Property Commands() As List(Of Scripting.Items.Command)
            Get
                Return _commands
            End Get
            Set(ByVal value As List(Of Scripting.Items.Command))
                _commands = value
            End Set
        End Property



        ' ========================================
        ' Public functions
        ' ========================================

        ' Constructors
        Public Sub New(ByVal scriptFilePath As String)
            Try
                _script = System.IO.File.ReadAllText(scriptFilePath, System.Text.Encoding.Default)
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End Sub
        Public Sub New()
            '
        End Sub

        ' Execute the script
        Public Sub Execute()
            If CheckGrammar() Then
                Call Me.Build()
                Call Me.Launch()
            End If
        End Sub

        ' Verify the grammar
        Public Function CheckGrammar() As Boolean
            Return Me.Verify
        End Function



        ' ========================================
        ' Private functions
        ' ========================================

        ' Check grammar
        Public Function Verify(Optional ByRef msg As String = Nothing) As Boolean
            Return True ' TOCHANGE
        End Function

        ' Parse and build the script
        Private Sub Build()

            ' Get the script as lines
            _script = _script.Replace(vbCr, vbNullString)
            _script = _script.Replace(vbLf, vbNewLine)
            Dim _s() As String = _script.Split(CChar(vbNewLine))
            Dim _lines As New List(Of String)

            ' All to lowercase
            For x As Integer = 0 To _s.Length - 1
                If (_s(x) IsNot Nothing) AndAlso (_s(x).Length > 1) Then
                    _lines.Add(_s(x).ToLowerInvariant.Trim)
                End If
            Next

            ' Retrieve the list of machines
            For x As Integer = 0 To _lines.Count - 1
                If _lines(x) = "with machines" Then
                    x += 1
                    ' Ok, now we create some machines with the different lines
                    Do Until _lines(x) = "end with"
                        _machines.Add(New Items.Machine(_lines(x)))
                        x += 1
                    Loop
                End If
            Next

            ' Retrieve the list of commands
            For x As Integer = 0 To _lines.Count - 1
                If _lines(x) = "end with" Then
                    x += 1
                    ' Ok, create commands
                    Do Until x = _lines.Count
                        _commands.Add(New Items.Command(_lines(x)))
                        x += 1
                    Loop
                End If
            Next

            ' Create a new thread for each machine
            For Each it As Scripting.Items.Machine In _machines
                _threads.Add(New Threading.Thread(AddressOf it.ExecuteCommands))
            Next


        End Sub

        ' Launch now
        Private Sub Launch()
            For Each th As Threading.Thread In _threads
                th.Start(_commands)
            Next
        End Sub

    End Class

End Namespace
