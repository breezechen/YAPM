Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackThreadDecreasePriority

    Private _id As Integer
    Private _level As Integer
    Private _connection As cThreadConnection

    Public Event HasDecreasedPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal id As Integer, ByVal level As Integer, ByRef procConnection As cThreadConnection)
        _id = id
        _level = level
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _level2 As System.Diagnostics.ThreadPriorityLevel
                Select Case _level
                    Case ThreadPriorityLevel.AboveNormal
                        _level2 = ThreadPriorityLevel.Normal
                    Case ThreadPriorityLevel.BelowNormal
                        _level2 = ThreadPriorityLevel.Lowest
                    Case ThreadPriorityLevel.Highest
                        _level2 = ThreadPriorityLevel.AboveNormal
                    Case ThreadPriorityLevel.Idle
                        '
                    Case ThreadPriorityLevel.Lowest
                        _level2 = ThreadPriorityLevel.Idle
                    Case ThreadPriorityLevel.Normal
                        _level2 = ThreadPriorityLevel.BelowNormal
                    Case ThreadPriorityLevel.TimeCritical
                        _level2 = ThreadPriorityLevel.Highest
                End Select

                Dim hProc As Integer
                Dim r As UInteger = -1
                hProc = API.OpenThread(API.THREAD_RIGHTS.THREAD_SET_INFORMATION, 0, _id)
                If hProc > 0 Then
                    r = API.SetThreadPriority(New IntPtr(hProc), _level2)
                    API.CloseHandle(hProc)
                    RaiseEvent HasDecreasedPriority(r <> 0, API.GetError)
                Else
                    RaiseEvent HasDecreasedPriority(False, API.GetError)
                End If
        End Select
    End Sub

End Class
