Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackModuleUnload

    Private _pid As Integer
    Private _baseA As Integer
    Private _name As String
    Private _connection As cModuleConnection

    Public Event HasUnloadedModule(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal address As Integer, ByVal name As String, ByRef procConnection As cModuleConnection)
        _pid = pid
        _name = name
        _baseA = address
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim hProc As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_CREATE_THREAD, 0, _pid)

                If hProc > 0 Then
                    Dim kernel32 As Integer = API.GetModuleHandle("kernel32.dll")
                    Dim freeLibrary As Integer = API.GetProcAddress(kernel32, "FreeLibrary")
                    Dim threadId As Integer
                    Dim ret As Integer = API.CreateRemoteThread(hProc, 0, 0, freeLibrary, _baseA, 0, threadId)
                    RaiseEvent HasUnloadedModule(ret <> 0, _pid, _name, API.GetError)
                Else
                    RaiseEvent HasUnloadedModule(False, _pid, _name, API.GetError)
                End If
        End Select
    End Sub

End Class
