Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackServiceSetStartType

    Private _name As String
    Private _connection As cServiceConnection
    Private _type As API.SERVICE_START_TYPE

    Private Shared _syncLockObj As New Object

    Public Event HasChangedStartType(ByVal Success As Boolean, ByVal name As String, ByVal msg As String)

    Public Sub New(ByVal name As String, ByVal type As API.SERVICE_START_TYPE, ByRef procConnection As cServiceConnection)
        _name = name
        _type = type
        _connection = procConnection
    End Sub

    Public Sub Process()
        SyncLock _syncLockObj
            Select Case _connection.ConnectionObj.ConnectionType
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                Case Else
                    ' Local
                    Dim hLockSCManager As Integer
                    Dim hSCManager As IntPtr = _connection.SCManagerLocalHandle
                    Dim lServ As IntPtr = API.OpenService(hSCManager, _name, API.SERVICE_RIGHTS.SERVICE_CHANGE_CONFIG)
                    Dim ret As Boolean = False

                    hLockSCManager = API.LockServiceDatabase(CInt(hSCManager))

                    If hSCManager <> IntPtr.Zero Then
                        If lServ <> IntPtr.Zero Then
                            ret = API.ChangeServiceConfig(CInt(lServ), API.SERVICE_NO_CHANGE, _type, _
                                API.SERVICE_NO_CHANGE, vbNullString, vbNullString, Nothing, _
                                vbNullString, vbNullString, vbNullString, vbNullString)
                            API.CloseServiceHandle(lServ)
                        End If
                        API.UnlockServiceDatabase(hLockSCManager)
                    End If

                    RaiseEvent HasChangedStartType(ret, _name, API.GetError)
            End Select
        End SyncLock
    End Sub

End Class
