' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 3 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, see http://www.gnu.org/licenses/.


Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text

Public Class asyncCallbackProcKillTree

    Private con As cProcessConnection
    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasKilled, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal act As Integer)
            newAction = act
            pid = pi
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessKillTree, pObj.pid)
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                _deg.Invoke(recursiveKill(pObj.pid), Native.Api.Win32.GetLastError, pObj.newAction)

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
        Dim hProc As IntPtr
        Dim ret As Integer = -1
        hProc = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_TERMINATE, False, pid)
        If hProc <> IntPtr.Zero Then
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
        API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemProcessesAndThreadsInformation, IntPtr.Zero, 0, ret)
        Dim size As Integer = ret
        Dim ptr As IntPtr = Marshal.AllocHGlobal(size)
        API.NtQuerySystemInformation(native.api.nativeenums.SystemInformationClass.SystemProcessesAndThreadsInformation, ptr, size, ret)

        ' Extract structures from unmanaged memory
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
