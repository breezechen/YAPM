Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackProcIncreasePriority

    Private _pid As Integer
    Private _level As ProcessPriorityClass
    Private _connection As cProcessConnection
    Private _deg As HasIncreasedPriority

    Public Delegate Sub HasIncreasedPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal deg As HasIncreasedPriority, ByVal pid As Integer, ByVal level As ProcessPriorityClass, ByRef procConnection As cProcessConnection)
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

                    Dim res As Integer = 2        ' Access denied
                    For Each srv As ManagementObject In _connection.wmiSearcher.Get
                        If CInt(srv.GetPropertyValue(API.WMI_INFO_PROCESS.ProcessId.ToString)) = _pid Then
                            Dim inParams As ManagementBaseObject = srv.GetMethodParameters("SetPriority")
                            inParams("Priority") = _newlevel
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
                    _deg.Invoke(r <> 0, API.GetError)
                Else
                    _deg.Invoke(False, API.GetError)
                End If
        End Select
    End Sub

End Class
