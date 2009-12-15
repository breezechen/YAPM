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
Imports Native.Api.Enums
Imports System.Management

Namespace Wmi.Objects

    Public Class cSystem


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' Shutdown remote pc
        Public Shared Function ShutdownRemoteComputer(ByVal type As ShutdownType, _
                        ByVal force As Boolean, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef msgError As String) As Boolean

            Try
                Dim res As WBEMStatus
                Dim param As WmiShutdownValues
                If force Then
                    param = param Or WmiShutdownValues.Force
                End If
                Select Case type
                    Case ShutdownType.Logoff
                        param = param Or WmiShutdownValues.LogOff
                    Case ShutdownType.Poweroff
                        param = param Or WmiShutdownValues.PowerOff
                    Case ShutdownType.Restart
                        param = param Or WmiShutdownValues.Reboot
                    Case ShutdownType.Shutdown
                        param = param Or WmiShutdownValues.Shutdown
                End Select
                Dim obj(0) As Object
                obj(0) = CObj(param)
                For Each osObj As ManagementObject In objSearcher.Get
                    res = CType(osObj.InvokeMethod("Win32Shutdown", obj), WBEMStatus)
                    Exit For
                Next

                msgError = res.ToString
                Return (res = WBEMStatus.WBEM_NO_ERROR)
            Catch ex As Exception
                msgError = ex.Message
                Return False
            End Try

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
