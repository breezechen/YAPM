Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcKill

    Private _pid As Integer
    Private _connection As cProcessConnection

    Public Event HasKilled(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cProcessConnection)
        _pid = pid
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim _theProcess As Management.ManagementObject = Nothing
                    For Each pp As Management.ManagementObject In _connection.wmiSearcher.Get
                        If CInt(pp("ProcessId")) = _pid Then
                            _theProcess = pp
                            Exit For
                        End If
                    Next
                    If _theProcess IsNot Nothing Then
                        Dim ret As Integer = 0
                        ret = CInt(_theProcess.InvokeMethod("Terminate", Nothing))
                        RaiseEvent HasKilled(ret = 0, _pid, CType(ret, API.PROCESS_RETURN_CODE_WMI).ToString)
                    Else
                        RaiseEvent HasKilled(False, _pid, "Internal error")
                    End If
                Catch ex As Exception
                    RaiseEvent HasKilled(False, _pid, ex.Message)
                End Try

            Case Else
                ' Local
                Dim hProc As Integer
                Dim ret As Integer = -1
                hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_TERMINATE, 0, _pid)
                If hProc > 0 Then
                    ret = API.TerminateProcess(hProc, 0)
                    API.CloseHandle(hProc)
                    RaiseEvent HasKilled(ret <> 0, 0, API.GetError)
                Else
                    RaiseEvent HasKilled(False, _pid, API.GetError)
                End If
        End Select
    End Sub

End Class
