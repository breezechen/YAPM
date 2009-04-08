Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackServicePause

    Private _name As String
    Private _connection As cServiceConnection
    Private _deg As HasPaused

    Public Delegate Sub HasPaused(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal deg As HasPaused, ByVal name As String, ByRef procConnection As cServiceConnection)
        _name = name
        _deg = deg
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim res As Integer = 2        ' Access denied
                    For Each srv As ManagementObject In _connection.wmiSearcher.Get
                        If CStr(srv.GetPropertyValue(API.WMI_INFO_SERVICE.Name.ToString)) = _name Then
                            res = CInt(srv.InvokeMethod("PauseService", Nothing))
                            Exit For
                        End If
                    Next
                    _deg.Invoke(res = 0, _name, CType(res, API.SERVICE_RETURN_CODE_WMI).ToString)
                Catch ex As Exception
                    _deg.Invoke(False, _name, ex.Message)
                End Try

            Case Else
                ' Local
                Dim hSCManager As IntPtr = _connection.SCManagerLocalHandle
                Dim lServ As IntPtr = API.OpenService(hSCManager, _name, API.SERVICE_RIGHTS.SERVICE_PAUSE_CONTINUE)
                Dim res As Boolean = False
                If hSCManager <> IntPtr.Zero Then
                    If lServ <> IntPtr.Zero Then
                        Dim lpss As API.SERVICE_STATUS
                        res = API.ControlService(lServ, API.SERVICE_CONTROL._PAUSE, lpss)
                        API.CloseServiceHandle(lServ)
                    End If
                End If
                _deg.Invoke(res, _name, API.GetError)
        End Select
    End Sub

End Class
