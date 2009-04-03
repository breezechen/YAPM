Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackThreadIncreasePriority

    Private _pid As Integer
    Private _level As Integer
    Private _connection As cThreadConnection

    Public Event HasIncreasedPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal level As Integer, ByRef procConnection As cThreadConnection)
        _pid = pid
        _level = level
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
