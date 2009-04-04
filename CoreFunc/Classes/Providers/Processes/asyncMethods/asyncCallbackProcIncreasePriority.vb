Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcIncreasePriority

    Private _pid As Integer
    Private _level As ProcessPriorityClass
    Private _connection As cProcessConnection

    Public Event HasIncreasedPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal level As ProcessPriorityClass, ByRef procConnection As cProcessConnection)
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
                Dim hProc As Integer
                Dim r As Integer
                Dim _newlevel As ProcessPriorityClass
                Select Case _level
                    Case ProcessPriorityClass.AboveNormal
                        _newlevel = ProcessPriorityClass.High
                    Case ProcessPriorityClass.BelowNormal
                        _newlevel = ProcessPriorityClass.Normal
                    Case ProcessPriorityClass.High
                        _newlevel = ProcessPriorityClass.RealTime
                    Case ProcessPriorityClass.Idle
                        _newlevel = ProcessPriorityClass.BelowNormal
                    Case ProcessPriorityClass.Normal
                        _newlevel = ProcessPriorityClass.AboveNormal
                    Case ProcessPriorityClass.RealTime
                        '
                End Select
                hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_INFORMATION, 0, _pid)
                If hProc > 0 Then
                    r = API.SetPriorityClass(hProc, _newlevel)
                    API.CloseHandle(hProc)
                    RaiseEvent HasIncreasedPriority(r <> 0, API.GetError)
                Else
                    RaiseEvent HasIncreasedPriority(False, API.GetError)
                End If
        End Select
    End Sub

End Class
