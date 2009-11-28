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

Imports System.Runtime.InteropServices
Imports Native.Api
Imports System.Management

Namespace Wmi.Objects

    Public Class Thread



        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' Enumerate threads
        Public Shared Function EnumerateThreadByIds(ByVal pid() As Integer, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, threadInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()
            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

            For Each refThread As Management.ManagementObject In res

                Dim wmiId As Integer = CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.ProcessHandle.ToString))
                Dim ex As Boolean = False
                For Each ii As Integer In pid
                    If ii = wmiId Then
                        ex = True
                        Exit For
                    End If
                Next
                ' If we have to get threads for this process...
                If ex Then
                    Dim obj As New Native.Api.NativeStructs.SystemThreadInformation
                    With obj
                        .BasePriority = CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.PriorityBase.ToString))
                        .CreateTime = 0
                        .ClientId = New Native.Api.NativeStructs.ClientId(wmiId, _
                                                      CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.Handle.ToString)))
                        .KernelTime = 10000 * CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.KernelModeTime.ToString))
                        .Priority = CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.Priority.ToString))
                        Try
                            .StartAddress = CType(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.StartAddress.ToString), IntPtr)
                        Catch ex0 As Exception
                            .StartAddress = NativeConstants.InvalidHandleValue
                        End Try
                        .State = CType(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.ThreadState.ToString), ThreadState)
                        .UserTime = 10000 * CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.UserModeTime.ToString))
                        .WaitReason = CType(CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.ThreadWaitReason.ToString)), Native.Api.NativeEnums.KwaitReason)
                        Try
                            .WaitTime = 10000 * CInt(refThread.GetPropertyValue(Native.Api.Enums.WMI_INFO_THREAD.ElapsedTime.ToString))
                        Catch ex1 As Exception
                            '
                        End Try
                    End With
                    Dim _procInfos As New threadInfos(obj)
                    Dim _key As String = obj.ClientId.UniqueThread.ToString & "-" & obj.ClientId.UniqueProcess.ToString
                    If _dico.ContainsKey(_key) = False Then
                        _dico.Add(_key, _procInfos)
                    End If
                End If
            Next

            Return True
        End Function


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
