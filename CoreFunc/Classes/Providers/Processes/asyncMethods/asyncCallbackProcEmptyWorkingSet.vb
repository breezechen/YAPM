Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcEmptyWorkingSet

    Private _pid As Integer
    Private _connection As cProcessConnection
    Private _deg As HasReducedWorkingSet

    Public Delegate Sub HasReducedWorkingSet(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal deg As HasReducedWorkingSet, ByVal pid As Integer, ByRef procConnection As cProcessConnection)
        _pid = pid
        _deg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _hHandle As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_QUOTA, 0, _pid)
                If _hHandle > 0 Then
                    Dim _ret As Integer = API.SetProcessWorkingSetSize(_hHandle, -1, -1)
                    API.CloseHandle(_hHandle)
                    _deg.Invoke(_ret <> 0, API.GetError)
                Else
                    _deg.Invoke(False, API.GetError)
                End If
        End Select
    End Sub

End Class
