Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Management

Public Class asyncCallbackNewProcess

    Private _path As String
    Private _connection As cProcessConnection

    Public Event HasCreated(ByVal Success As Boolean, ByVal path As String)

    Public Sub New(ByVal path As String, ByRef procConnection As cProcessConnection)
        _path = path
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Try
                    Dim objectGetOptions As New ObjectGetOptions()
                    Dim managementPath As New ManagementPath("Win32_Process")
                    Dim processClass As New ManagementClass(_connection.wmiSearcher.Scope, managementPath, objectGetOptions)
                    Dim inParams As ManagementBaseObject = processClass.GetMethodParameters("Create")
                    inParams("CommandLine") = _path
                    Dim outParams As ManagementBaseObject = processClass.InvokeMethod("Create", inParams, Nothing)
                    RaiseEvent HasCreated(CInt(outParams("ProcessId")) > 0, _path)
                Catch ex As Exception
                    RaiseEvent HasCreated(False, _path)
                End Try
            Case Else
                ' Local
                ' OK, normally the local startNewProcess is not done here
                ' because of RunBox need
                Dim pid As Integer = Shell(_path)
                RaiseEvent HasCreated(pid <> 0, _path)
        End Select
    End Sub

End Class
