﻿Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackServiceStart

    Private _name As String
    Private _connection As cServiceConnection

    Public Event HasStarted(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal name As String, ByRef procConnection As cServiceConnection)
        _name = name
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
                            res = CInt(srv.InvokeMethod("StartService", Nothing))
                            Exit For
                        End If
                    Next
                    RaiseEvent HasStarted(res = 0, _name, CType(res, API.SERVICE_RETURN_CODE_WMI).ToString)
                Catch ex As Exception
                    RaiseEvent HasStarted(False, _name, ex.Message)
                End Try

            Case Else
                ' Local
                Dim hSCManager As IntPtr = _connection.SCManagerLocalHandle
                Dim lServ As IntPtr = API.OpenService(hSCManager, _name, API.SERVICE_RIGHTS.SERVICE_START)
                Dim res As Integer = 0
                If hSCManager <> IntPtr.Zero Then
                    If lServ <> IntPtr.Zero Then
                        res = API.apiStartService(lServ, 0, 0)
                        API.CloseServiceHandle(lServ)
                    End If
                End If
                RaiseEvent HasStarted(res <> 0, _name, API.GetError)
        End Select
    End Sub

End Class