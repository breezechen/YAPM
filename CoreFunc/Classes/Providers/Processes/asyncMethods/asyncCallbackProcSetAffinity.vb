Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcSetAffinity

    Private _pid As Integer
    Private _level As Integer
    Private _connection As cProcessConnection

    Public Event HasSetAffinity(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal level As Integer, ByRef procConnection As cProcessConnection)
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
                Dim __hProcess As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_INFORMATION, 0, _pid)
                If __hProcess > 0 Then
                    Dim ret As Integer = API.SetProcessAffinityMask(__hProcess, _level)
                    API.CloseHandle(__hProcess)
                    RaiseEvent HasSetAffinity(ret <> 0, API.GetError)
                Else
                    RaiseEvent HasSetAffinity(False, API.GetError)
                End If
        End Select
    End Sub

End Class
