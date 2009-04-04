Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackThreadGetOtherInfos

    Private _id As Integer
    Private _handle As Integer
    Private _connection As cThreadConnection

    Public Structure TheseInfos
        Public affinity As Integer
        Public Sub New(ByVal _affinity As Integer)
            affinity = _affinity
        End Sub
    End Structure

    Public Event GatheredInfos(ByVal infos As TheseInfos)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cThreadConnection, Optional ByVal handle As Integer = 0)
        _id = pid
        _handle = handle
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _affinity As Integer = GetAffinity(_id)

                RaiseEvent GatheredInfos(New TheseInfos(_affinity))
        End Select
    End Sub

    ' Return affinity
    Private Function GetAffinity(ByVal _pid As Integer) As Integer
        Dim infos As New API.THREAD_BASIC_INFORMATION
        Dim ret As Integer
        API.ZwQueryInformationThread(_handle, API.THREAD_INFORMATION_CLASS.ThreadBasicInformation, infos, Marshal.SizeOf(infos), ret)
        Return infos.AffinityMask
    End Function

End Class
