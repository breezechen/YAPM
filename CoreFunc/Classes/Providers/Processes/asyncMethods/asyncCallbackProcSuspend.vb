Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcSuspend

    Private _pid As Integer
    Private _connection As cProcessConnection

    Public Event HasSuspended(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cProcessConnection)
        _pid = pid
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As Integer
                Dim r As Integer = -1
                hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SUSPEND_RESUME, 0, _pid)
                If hProc > 0 Then
                    r = API.NtSuspendProcess(hProc)
                    API.CloseHandle(hProc)
                    RaiseEvent HasSuspended(r = 0, API.GetError)
                Else
                    RaiseEvent HasSuspended(False, API.GetError)
                End If
        End Select
    End Sub

End Class
