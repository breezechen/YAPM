Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackHandleUnload

    Private _pid As Integer
    Private _handle As Integer
    Private _connection As cHandleConnection

    Public Event HasUnloadedHandle(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As Integer, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal handle As Integer, ByRef procConnection As cHandleConnection)
        _pid = pid
        _handle = handle
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
