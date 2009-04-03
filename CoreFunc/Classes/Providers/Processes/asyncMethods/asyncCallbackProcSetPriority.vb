Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackProcSetPriority

    Private _pid As Integer
    Private _level As ProcessPriorityClass
    Private _connection As cProcessConnection

    Public Event HasSetPriority(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByVal level As ProcessPriorityClass, ByRef procConnection As cProcessConnection)
        _pid = pid
        _level = level
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
                    ' Error codes available : 
                    '0	Successful completion
                    '2	Access denied
                    '3	Insufficient Privilege
                    '8	Unknown Failure
                    '9	Path Not Found
                    '21	Invalid Parameter
                    If _theProcess IsNot Nothing Then
                        Dim ret As Integer = 0

                        Dim objectGetOptions As New ObjectGetOptions()
                        Dim managementPath As New ManagementPath("Win32_Process")
                        Dim processClass As New ManagementClass(_connection.wmiSearcher.Scope, managementPath, objectGetOptions)
                        Dim inParams As ManagementBaseObject = processClass.GetMethodParameters("SetPriority")
                        inParams("Priority") = CType(_level, System.Int32)
                        Dim outParams As ManagementBaseObject = processClass.InvokeMethod("SetPriority", inParams, Nothing)

                        'Dim inParams As System.Management.ManagementBaseObject = Nothing
                        'Dim EnablePrivileges As Boolean = PrivateLateBoundObject.Scope.Options.EnablePrivileges
                        'PrivateLateBoundObject.Scope.Options.EnablePrivileges = True
                        'inParams = PrivateLateBoundObject.GetMethodParameters("SetPriority")
                        'inParams("Priority") = CType(Priority, System.Int32)
                        'Dim outParams As System.Management.ManagementBaseObject = PrivateLateBoundObject.InvokeMethod("SetPriority", inParams, Nothing)
                        'PrivateLateBoundObject.Scope.Options.EnablePrivileges = EnablePrivileges
                        'Return System.Convert.ToUInt32(outParams.Properties("ReturnValue").Value)


                        RaiseEvent HasSetPriority(CInt(outParams("ReturnValue")) = 0, "")
                    Else
                        RaiseEvent HasSetPriority(False, "Internal error")
                    End If

                Catch ex As Exception
                    RaiseEvent HasSetPriority(False, ex.Message)
                End Try

            Case Else
                ' Local
                Dim hProc As Integer
                Dim r As Integer
                hProc = API.OpenProcess(API.PROCESS_SET_INFORMATION, 0, _pid)
                If hProc > 0 Then
                    r = API.SetPriorityClass(hProc, _level)
                    API.CloseHandle(hProc)
                    RaiseEvent HasSetPriority(r <> 0, API.GetError)
                Else
                    RaiseEvent HasSetPriority(False, API.GetError)
                End If
        End Select
    End Sub

End Class
