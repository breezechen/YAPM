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
Imports Native.Api

Namespace Native.Objects

    Public Class Handle


        ' ========================================
        ' Private constants
        ' ========================================

        ' Handle enumeration class
        Private Shared hEnum As HandleEnumeration

        ' Protect handle enumeration
        Private Shared semProtectEnum As New System.Threading.Semaphore(1, 1)


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        Public Shared Property HandleEnumerationClass() As HandleEnumeration
            Get
                Return hEnum
            End Get
            Set(ByVal value As HandleEnumeration)
                hEnum = value
            End Set
        End Property


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Close a handle in another process
        Public Shared Function CloseProcessLocalHandle(ByVal dwProcessID As Integer, _
                                                ByVal hHandle As IntPtr) As Integer
            Dim hMod As IntPtr
            Dim lpProc As IntPtr
            Dim hThread As IntPtr
            Dim hProcess As IntPtr
            Dim res As Integer

            hMod = NativeFunctions.GetModuleHandle("kernel32.dll")
            lpProc = NativeFunctions.GetProcAddress(hMod, "CloseHandle")
            hProcess = Native.Objects.Process.GetProcessHandleById(dwProcessID, Native.Security.ProcessAccess.CreateThread Or _
                                                Native.Security.ProcessAccess.VmOperation Or _
                                                Native.Security.ProcessAccess.VmWrite Or _
                                                Native.Security.ProcessAccess.VmRead)
            If hProcess.IsNotNull Then
                hThread = NativeFunctions.CreateRemoteThread(hProcess, IntPtr.Zero, 0, _
                                                             lpProc, hHandle, 0, 0)
                If hThread.IsNotNull Then
                    NativeFunctions.WaitForSingleObject(hThread, NativeConstants.WAIT_INFINITE)
                    NativeFunctions.GetExitCodeThread(hThread, res)
                    NativeFunctions.CloseHandle(hThread)
                End If
                NativeFunctions.CloseHandle(hProcess)
            End If

            Return res
        End Function

        ' Return handles of some processes
        Public Shared Sub EnumerateHandleByProcessIds(ByVal pid() As Integer, _
                                                     ByVal showUnNamed As Boolean, _
                                                     ByRef _dico As Dictionary(Of String, handleInfos))

            ' Handle enumeration class not initialized...
            If hEnum Is Nothing Then
                Exit Sub
            End If

            ' Protection !
            semProtectEnum.WaitOne()

            ' Refresh handles
            Call hEnum.Refresh(pid)

            For i As Integer = 0 To hEnum.Count - 1
                If hEnum.IsNotNull(i) AndAlso hEnum.GetHandle(i).IsNotNull Then
                    If showUnNamed OrElse (Len(hEnum.GetObjectName(i)) > 0) Then

                        Dim _key As String = hEnum.GetHandleInfos(i).Key

                        ' This verification should not be needed, but in reality
                        ' it IS needed
                        ' TOCHECK
                        If _dico.ContainsKey(_key) = False Then
                            _dico.Add(_key, hEnum.GetHandleInfos(i))
                        End If

                    End If
                End If
            Next

            semProtectEnum.Release()

        End Sub

        ' Return all local handles (protected by semaphore)
        Public Shared Function EnumerateCurrentLocalHandles(Optional ByVal all As Boolean = True) As Dictionary(Of String, cHandle)

            Dim _dico As New Dictionary(Of String, cHandle)

            ' Handle enumeration class not initialized...
            If hEnum Is Nothing Then
                Return _dico
            End If

            ' Protection !
            semProtectEnum.WaitOne()

            ' Refresh handles
            Call hEnum.Refresh(-1)    ' Refresh all

            For i As Integer = 0 To hEnum.Count - 1
                If hEnum.IsNotNull(i) AndAlso hEnum.GetHandle(i).IsNotNull Then
                    If all OrElse (Len(hEnum.GetObjectName(i)) > 0) Then

                        Dim _key As String = hEnum.GetHandleInfos(i).Key

                        ' This verification should not be needed, but in reality
                        ' it IS needed
                        ' TOCHECK
                        If _dico.ContainsKey(_key) = False Then
                            _dico.Add(_key, New cHandle(hEnum.GetHandleInfos(i)))
                        End If

                    End If
                End If
            Next

            semProtectEnum.Release()

            Return _dico

        End Function


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
