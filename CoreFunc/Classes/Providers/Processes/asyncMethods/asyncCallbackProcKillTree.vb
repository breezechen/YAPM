Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcKillTree

    Private _pid As Integer
    Private _connection As cProcessConnection

    Public Event HasKilled(ByVal Success As Boolean, ByVal msg As String)

    Public Sub New(ByVal pid As Integer, ByRef procConnection As cProcessConnection)
        _pid = pid
        _connection = procConnection
    End Sub

    Public Sub Process()
        Select Case _connection.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                RaiseEvent HasKilled(recursiveKill(_pid), API.GetError)

        End Select
    End Sub

    ' For 'Kill process tree'
    Private Function recursiveKill(ByVal pid As Integer) As Boolean
        Static success As Boolean = True

        ' Kill process
        success = success And Kill(pid)

        ' Get all items...
        Dim _dico2 As New List(Of Integer)
        _dico2 = EnumParent(pid)

        ' Recursive kill
        For Each t As Integer In _dico2
            recursiveKill(t)
        Next

        Return success
    End Function

    ' Kill a process
    Private Function Kill(ByVal pid As Integer) As Boolean
        Dim hProc As Integer
        Dim ret As Integer = -1
        hProc = API.OpenProcess(API.PROCESS_TERMINATE, 0, pid)
        If hProc > 0 Then
            ret = API.TerminateProcess(hProc, 0)
            API.CloseHandle(hProc)
            Return (ret <> 0)
        Else
            Return False
        End If
    End Function

    ' Simple enumeration of parents
    Private Function EnumParent(ByVal pid As Integer) As List(Of Integer)
        ' Local
        Dim ret As Integer
        API.ZwQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessesAndThreadsInformation, IntPtr.Zero, 0, ret)
        Dim size As Integer = ret
        Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
        API.ZwQuerySystemInformation(API.SYSTEM_INFORMATION_CLASS.SystemProcessesAndThreadsInformation, ptr, size, ret)

        ' Extract structures from unmanaged memory
        Dim structSize As Integer = Marshal.SizeOf(GetType(API.SYSTEM_PROCESS_INFORMATION))
        Dim x As Integer = 0
        Dim offset As Integer = 0
        Dim _list As New List(Of Integer)
        Do While True
            Dim obj As API.SYSTEM_PROCESS_INFORMATION = CType(Marshal.PtrToStructure(New IntPtr(ptr.ToInt32 + _
                offset), GetType(API.SYSTEM_PROCESS_INFORMATION)),  _
                API.SYSTEM_PROCESS_INFORMATION)
            offset += obj.NextEntryOffset
            If obj.InheritedFromProcessId = pid Then
                _list.Add(obj.ProcessId)
            End If
            If obj.NextEntryOffset = 0 Then
                Exit Do
            End If
            x += 1
        Loop
        Marshal.FreeHGlobal(ptr)
        Return _list
    End Function

End Class
