Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackThreadSetAffinity

    Private _pid As Integer
    Private _level As Integer
    Private _connection As cThreadConnection
    Private _deg As HasSetAffinity

    Public Delegate Sub HasSetAffinity(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal deg As HasSetAffinity, ByVal pid As Integer, ByVal level As Integer, ByRef procConnection As cThreadConnection)
        _pid = pid
        _deg = deg
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
                    _deg.Invoke(ret <> 0, API.GetError)
                Else
                    _deg.Invoke(False, API.GetError)
                End If
        End Select
    End Sub

End Class
