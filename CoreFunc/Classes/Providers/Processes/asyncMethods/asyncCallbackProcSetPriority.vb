Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackProcSetPriority

    Private _pid As Integer
    Private _level As ProcessPriorityClass
    Private _connection As cProcessConnection
    Private _deg As HasSetPriority

    Public Delegate Sub HasSetPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal deg As HasSetPriority, ByVal pid As Integer, ByVal level As ProcessPriorityClass, ByRef procConnection As cProcessConnection)
        _pid = pid
        _deg = deg
        _level = level
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim res As Integer = 2        ' Access denied
                    For Each srv As ManagementObject In _connection.wmiSearcher.Get
                        If CInt(srv.GetPropertyValue(API.WMI_INFO_PROCESS.ProcessId.ToString)) = _pid Then
                            Dim inParams As ManagementBaseObject = srv.GetMethodParameters("SetPriority")
                            inParams("Priority") = _level
                            Dim outParams As ManagementBaseObject = srv.InvokeMethod("SetPriority", inParams, Nothing)
                            res = CInt(outParams("ReturnValue"))
                            Exit For
                        End If
                    Next
                    _deg.Invoke(res = 0, CType(res, API.PROCESS_RETURN_CODE_WMI).ToString)
                Catch ex As Exception
                    _deg.Invoke(False, ex.Message)
                End Try

            Case Else
                ' Local
                Dim hProc As Integer
                Dim r As Integer
                hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_SET_INFORMATION, 0, _pid)
                If hProc > 0 Then
                    r = API.SetPriorityClass(hProc, _level)
                    API.CloseHandle(hProc)
                    _deg.Invoke(r <> 0, API.GetError)
                Else
                    _deg.Invoke(False, API.GetError)
                End If
        End Select
    End Sub

End Class
