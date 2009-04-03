Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackThreadSetPriority

    Private _pid As Integer
    Private _level As System.Diagnostics.ThreadPriorityLevel
    Private _connection As cThreadConnection

    Public Event HasSetPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal level As System.Diagnostics.ThreadPriorityLevel, ByRef procConnection As cThreadConnection)
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
