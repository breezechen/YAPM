Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackServicePause

    Private _name As String
    Private _connection As cServiceConnection

    Public Event HasPaused(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal name As String, ByRef procConnection As cServiceConnection)
        _name = name
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hSCManager As IntPtr = _connection.SCManagerLocalHandle
                Dim lServ As IntPtr = API.OpenService(hSCManager, _name, API.SERVICE_RIGHTS.SERVICE_PAUSE_CONTINUE)
                Dim res As Boolean = False
                If hSCManager <> IntPtr.Zero Then
                    If lServ <> IntPtr.Zero Then
                        Dim lpss As API.SERVICE_STATUS
                        res = API.ControlService(lServ, API.SERVICE_CONTROL._PAUSE, lpss)
                        API.CloseServiceHandle(lServ)
                    End If
                End If
                RaiseEvent HasPaused(res, _name, API.GetError)
        End Select
    End Sub

End Class
