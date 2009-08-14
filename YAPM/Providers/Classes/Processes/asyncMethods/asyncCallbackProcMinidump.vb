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

Public Class asyncCallbackProcMinidump

    Private con As cProcessConnection
    Private _deg As HasCreatedDump

    Public Delegate Sub HasCreatedDump(ByVal Success As Boolean, ByVal pid As Integer, ByVal file As String, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasCreatedDump, ByRef procConnection As cProcessConnection)
        _deg = deg
        con = procConnection
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public file As String
        Public dumpOpt As Native.Api.NativeEnums.MINIDUMPTYPE
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal fil As String, ByVal opt As Native.Api.NativeEnums.MINIDUMPTYPE, ByVal act As Integer)
            newAction = act
            file = fil
            dumpOpt = opt
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

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Try
                    Dim hProc As IntPtr
                    Dim ret As Integer = -1
                    hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.QueryInformation Or _
                                            Native.Security.ProcessAccess.VmRead, _
                                            False, pObj.pid)
                    If hProc <> IntPtr.Zero Then
                        ' Create dump file
                        Dim fs As New System.IO.FileStream(pObj.file, System.IO.FileMode.Create)
                        ' Write dump file
                        Native.Api.NativeFunctions.MiniDumpWriteDump(hProc, pObj.pid, fs.SafeFileHandle.DangerousGetHandle(), pObj.dumpOpt, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero)
                        Native.Api.NativeFunctions.CloseHandle(hProc)
                        fs.Close()
                        _deg.Invoke(ret <> 0, 0, pObj.file, Native.Api.Win32.GetLastError, pObj.newAction)
                    Else
                        _deg.Invoke(False, pObj.pid, pObj.file, Native.Api.Win32.GetLastError, pObj.newAction)
                    End If
                Catch ex As Exception
                    ' Access denied, or...
                    _deg.Invoke(False, pObj.pid, pObj.file, ex.Message, pObj.newAction)
                End Try
        End Select
    End Sub

End Class
