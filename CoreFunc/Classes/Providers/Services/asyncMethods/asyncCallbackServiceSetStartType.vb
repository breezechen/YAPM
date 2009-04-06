Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackServiceSetStartType

    Private _name As String
    Private _connection As cServiceConnection
    Private _type As API.SERVICE_START_TYPE

    Public Event HasChangedStartType(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal name As String, ByVal type As API.SERVICE_START_TYPE, ByRef procConnection As cServiceConnection)
        _name = name
        _type = type
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
