Option Strict Off

Public Module mdlMain

    ' Represent options passed with command line
    Public Class ProgramParameters

        ' Available parameters
        Private _autoMode As Boolean
        Private _zipPath As String
        Public ReadOnly Property AutoMode() As Boolean
            Get
                Return _autoMode
            End Get
        End Property
        Public ReadOnly Property ZipPath() As String
            Get
                Return _zipPath
            End Get
        End Property
        Public Sub New(ByRef parameters As String())
            If parameters Is Nothing Then
                Exit Sub
            End If
            For i As Integer = 0 To parameters.Length - 1
                If parameters(i).ToUpperInvariant = "-AUTO" Then
                    If parameters.Length - 1 >= i + 1 Then
                        _zipPath = parameters(i + 1)
                        _autoMode = True
                    End If
                End If
            Next
        End Sub
    End Class

    Public _frmMain As frmMain
    Public progParameters As ProgramParameters

    Public Sub main()

        ' Read commande line
        progParameters = New ProgramParameters(Environment.GetCommandLineArgs)
        _frmMain = New frmMain

        If progParameters.AutoMode Then
            ' Auto mode

        End If

        Application.Run(_frmMain)


    End Sub

End Module
