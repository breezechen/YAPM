Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcEmptyWorkingSet

    Private _pid As Integer
    Private _connection As cProcessConnection

    Public Event HasReducedWorkingSet(ByVal Success As Boolean, ByVal msg As String)

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
                Dim _hHandle As Integer = API.OpenProcess(API.PROCESS_SET_QUOTA, 0, _pid)
                If _hHandle > 0 Then
                    Dim _ret As Integer = API.SetProcessWorkingSetSize(_hHandle, -1, -1)
                    API.CloseHandle(_hHandle)
                    RaiseEvent HasReducedWorkingSet(_ret <> 0, API.GetError)
                Else
                    RaiseEvent HasReducedWorkingSet(False, API.GetError)
                End If
        End Select
    End Sub

End Class
