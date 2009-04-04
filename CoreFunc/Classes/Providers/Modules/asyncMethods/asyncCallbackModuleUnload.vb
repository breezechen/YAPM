Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackModuleUnload

    Private _id As Integer
    Private _baseA As Integer
    Private _connection As cModuleConnection

    Public Event HasUnloadedModule(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal id As Integer, ByVal address As Integer, ByRef procConnection As cModuleConnection)
        _id = id
        _baseA = address
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
               
        End Select
    End Sub

End Class
