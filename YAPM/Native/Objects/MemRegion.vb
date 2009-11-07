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

    Public Class MemRegion


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Change protection type
        Public Shared Function ChangeMemoryRegionProtectionType(ByVal processId As Integer, _
                                                                ByVal address As IntPtr, _
                                                                ByVal regSize As IntPtr, _
                                                                ByVal newProtection As NativeEnums.MemoryProtectionType) As Boolean
            Dim ret As Boolean
            Dim hProcess As IntPtr
            Dim old As NativeEnums.MemoryProtectionType

            hProcess = Native.Objects.Process.GetProcessHandleById(processId, Security.ProcessAccess.VmOperation)
            If hProcess.IsNotNull Then
                ret = NativeFunctions.VirtualProtectEx(hProcess, address, regSize.ToInt32, newProtection, old)
                Call NativeFunctions.CloseHandle(hProcess)
            End If

            Return ret

        End Function

        ' Free memory (decommit or release)
        Public Shared Function FreeMemory(ByVal processId As Integer, _
                                        ByVal address As IntPtr, _
                                        ByVal regSize As IntPtr, _
                                        ByVal type As NativeEnums.MemoryState) As Boolean

            Dim ret As Boolean
            Dim hProcess As IntPtr

            hProcess = Native.Objects.Process.GetProcessHandleById(processId, Security.ProcessAccess.VmOperation)
            If hProcess.IsNotNull Then
                ret = Native.Api.NativeFunctions.VirtualFreeEx(hProcess, address, regSize.ToInt32, type)
                Call Native.Api.NativeFunctions.CloseHandle(hProcess)
            End If

            Return ret

        End Function

        ' Enumerate memory regions
        Public Shared Sub EnumerateMemoryRegionsByProcessId(ByVal pid As Integer, _
                                    ByRef _dico As Dictionary(Of String, memRegionInfos))
            Dim lHandle As IntPtr
            Dim lPosMem As IntPtr
            Dim mbi As Native.Api.NativeStructs.MemoryBasicInformation
            Dim mbiSize As Integer = Marshal.SizeOf(mbi)

            lHandle = Native.Objects.Process.GetProcessHandleById(pid, Security.ProcessAccess.QueryInformation Or _
                                                                Security.ProcessAccess.VmRead)

            If lHandle.IsNotNull Then
                ' We'll exit when VirtualQueryEx will fail
                Do While True
                    If Native.Api.NativeFunctions.VirtualQueryEx(lHandle, lPosMem, mbi, mbiSize) > 0 Then

                        _dico.Add(mbi.BaseAddress.ToString, _
                                  New memRegionInfos(mbi, pid))

                        lPosMem = lPosMem.Increment(mbi.RegionSize)
                    Else
                        Exit Do
                    End If
                Loop
                Native.Api.NativeFunctions.CloseHandle(lHandle)
            End If

        End Sub



        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
