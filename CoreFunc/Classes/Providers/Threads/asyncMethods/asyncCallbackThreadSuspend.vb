﻿Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackThreadSuspend

    Private _id As Integer
    Private _connection As cThreadConnection

    Public Event HasSuspended(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal id As Integer, ByRef procConnection As cThreadConnection)
        _id = id
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As Integer
                Dim r As UInteger = -1
                hProc = API.OpenThread(API.THREAD_RIGHTS.THREAD_SUSPEND_RESUME, 0, _id)
                If hProc > 0 Then
                    r = API.SuspendThread(New IntPtr(hProc))
                    API.CloseHandle(hProc)
                    RaiseEvent HasSuspended(r <> -1, API.GetError)
                Else
                    RaiseEvent HasSuspended(False, API.GetError)
                End If
        End Select
    End Sub

End Class
