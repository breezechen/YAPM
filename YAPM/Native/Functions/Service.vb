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
Imports YAPM.Native.Api.NativeStructs
Imports YAPM.Native.Api.NativeEnums
Imports YAPM.Native.Api.NativeFunctions

Namespace Native.Functions

    Public Class Service

        ' Get state/errorcontrol/starttype from a string as a type
        Public Shared Function GetServiceErrorControlFromString(ByVal s As String) As Native.Api.NativeEnums.ServiceErrorControl
            Select Case s
                Case "Ignore"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Ignore
                Case "Normal"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Normal
                Case "Severe"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Severe
                Case "Critical"
                    Return Native.Api.NativeEnums.ServiceErrorControl.Critical
                Case Else
                    Return Native.Api.NativeEnums.ServiceErrorControl.Unknown
            End Select
        End Function

        Public Shared Function GetServiceStartTypeFromString(ByVal s As String) As Native.Api.NativeEnums.ServiceStartType
            Select Case s
                Case "Boot"
                    Return Native.Api.NativeEnums.ServiceStartType.BootStart
                Case "System"
                    Return Native.Api.NativeEnums.ServiceStartType.SystemStart
                Case "Auto"
                    Return Native.Api.NativeEnums.ServiceStartType.AutoStart
                Case "Manual"
                    Return Native.Api.NativeEnums.ServiceStartType.DemandStart
                Case "Disabled"
                    Return Native.Api.NativeEnums.ServiceStartType.StartDisabled
            End Select
        End Function

        Public Shared Function GetServiceStateFromString(ByVal s As String) As Native.Api.NativeEnums.ServiceState
            Select Case s
                Case "Stopped"
                    Return Native.Api.NativeEnums.ServiceState.Stopped
                Case "Start Pending"
                    Return Native.Api.NativeEnums.ServiceState.StartPending
                Case "Stop Pending"
                    Return Native.Api.NativeEnums.ServiceState.StopPending
                Case "Running"
                    Return Native.Api.NativeEnums.ServiceState.Running
                Case "Continue Pending"
                    Return Native.Api.NativeEnums.ServiceState.ContinuePending
                Case "Pause Pending"
                    Return Native.Api.NativeEnums.ServiceState.PausePending
                Case "Paused"
                    Return Native.Api.NativeEnums.ServiceState.Paused
                Case Else
                    Return Native.Api.NativeEnums.ServiceState.Unknown
            End Select
        End Function

        Public Shared Function GetServiceTypeFromString(ByVal s As String) As Native.Api.NativeEnums.ServiceType
            Select Case s
                Case "Kernel Driver"
                    Return Native.Api.NativeEnums.ServiceType.KernelDriver
                Case "File System Driver"
                    Return Native.Api.NativeEnums.ServiceType.FileSystemDriver
                Case "Adapter"
                    Return Native.Api.NativeEnums.ServiceType.Adapter
                Case "Recognizer Driver"
                    Return Native.Api.NativeEnums.ServiceType.RecognizerDriver
                Case "Own Process"
                    Return Native.Api.NativeEnums.ServiceType.Win32OwnProcess
                Case "Share Process"
                    Return Native.Api.NativeEnums.ServiceType.Win32ShareProcess
                Case "Interactive Process"
                    Return Native.Api.NativeEnums.ServiceType.InteractiveProcess
            End Select
        End Function

    End Class

End Namespace
