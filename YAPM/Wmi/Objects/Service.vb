﻿' =======================================================
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
Imports YAPM.Native.Api
Imports System.Management
Imports YAPM.Native.Api.Enums

Namespace Wmi.Objects

    Public Class Service



        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' Enumerate services
        Public Shared Function EnumerateProcesses(ByVal pid As Integer, ByVal all As Boolean, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, serviceInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()
            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try


            For Each refService As Management.ManagementObject In res

                Dim obj As New Native.Api.NativeStructs.EnumServiceStatusProcess
                Dim theId As Integer = CInt(refService.GetPropertyValue(WMI_INFO_SERVICE.ProcessId.ToString))

                If all OrElse pid = theId Then

                    With obj
                        .DisplayName = CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.DisplayName.ToString))
                        .ServiceName = CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.Name.ToString))
                        With .ServiceStatusProcess
                            .CheckPoint = CInt(refService.GetPropertyValue(WMI_INFO_SERVICE.CheckPoint.ToString))
                            If CBool(refService.GetPropertyValue(WMI_INFO_SERVICE.AcceptPause.ToString)) Then
                                .ControlsAccepted = .ControlsAccepted Or Native.Api.NativeEnums.ServiceAccept.PauseContinue
                            End If
                            If CBool(refService.GetPropertyValue(WMI_INFO_SERVICE.AcceptStop.ToString)) Then
                                .ControlsAccepted = .ControlsAccepted Or Native.Api.NativeEnums.ServiceAccept.Stop
                            End If
                            .CurrentState = Native.Functions.Service.GetServiceStateFromString(CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.State.ToString)))
                            .ProcessID = theId
                            '.ServiceFlags
                            .ServiceSpecificExitCode = CInt(refService.GetPropertyValue(WMI_INFO_SERVICE.ServiceSpecificExitCode.ToString))
                            .ServiceType = Native.Functions.Service.GetServiceTypeFromString(CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.ServiceType.ToString)))
                            .WaitHint = CInt(refService.GetPropertyValue(WMI_INFO_SERVICE.WaitHint.ToString))
                            .Win32ExitCode = CInt(refService.GetPropertyValue(WMI_INFO_SERVICE.ExitCode.ToString))
                        End With
                    End With

                    ' Do we have to get fixed infos ?
                    Dim _servInfos As New serviceInfos(obj, True)

                    Dim conf As New Native.Api.NativeStructs.QueryServiceConfig
                    With conf
                        .BinaryPathName = CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.PathName.ToString))
                        '.Dependencies
                        .DisplayName = CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.DisplayName.ToString))
                        .ErrorControl = Native.Functions.Service.GetServiceErrorControlFromString(CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.ErrorControl.ToString)))
                        '.LoadOrderGroup 
                        .ServiceStartName = CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.StartName.ToString))
                        .StartType = Native.Functions.Service.GetServiceStartTypeFromString(CStr(refService.GetPropertyValue(WMI_INFO_SERVICE.StartMode.ToString)))
                        .TagID = CInt(refService.GetPropertyValue(WMI_INFO_SERVICE.TagId.ToString))
                    End With
                    _servInfos.SetConfig(conf)

                    _dico.Add(obj.ServiceName, _servInfos)
                End If
            Next

            Return True

        End Function

        ' Pause a service
        Public Shared Function PauseServiceByName(ByVal name As String, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef errMsg As String) As Boolean

            Try
                Dim res As SERVICE_RETURN_CODE_WMI = Enums.SERVICE_RETURN_CODE_WMI.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WMI_INFO_SERVICE.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("PauseService", Nothing), Enums.SERVICE_RETURN_CODE_WMI)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.SERVICE_RETURN_CODE_WMI.Success)

            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

        End Function

        ' Resume a service
        Public Shared Function ResumeServiceByName(ByVal name As String, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef errMsg As String) As Boolean

            Try
                Dim res As SERVICE_RETURN_CODE_WMI = Enums.SERVICE_RETURN_CODE_WMI.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WMI_INFO_SERVICE.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("ResumeService", Nothing), Enums.SERVICE_RETURN_CODE_WMI)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.SERVICE_RETURN_CODE_WMI.Success)

            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

        End Function

        ' Start a service
        Public Shared Function StartServiceByName(ByVal name As String, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef errMsg As String) As Boolean

            Try
                Dim res As SERVICE_RETURN_CODE_WMI = Enums.SERVICE_RETURN_CODE_WMI.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WMI_INFO_SERVICE.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("StartService", Nothing), Enums.SERVICE_RETURN_CODE_WMI)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.SERVICE_RETURN_CODE_WMI.Success)

            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

        End Function

        ' Stop a service
        Public Shared Function StopServiceByName(ByVal name As String, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef errMsg As String) As Boolean

            Try
                Dim res As SERVICE_RETURN_CODE_WMI = Enums.SERVICE_RETURN_CODE_WMI.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WMI_INFO_SERVICE.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("StopService", Nothing), Enums.SERVICE_RETURN_CODE_WMI)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.SERVICE_RETURN_CODE_WMI.Success)

            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try

        End Function

        ' Change service start type
        Public Shared Function SetServiceStartTypeByName(ByVal name As String, _
                        ByVal type As NativeEnums.ServiceStartType, _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef errMsg As String) As Boolean

            Try
                Dim res As SERVICE_RETURN_CODE_WMI = Enums.SERVICE_RETURN_CODE_WMI.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WMI_INFO_SERVICE.Name.ToString)) = name Then
                        Dim inParams As ManagementBaseObject = srv.GetMethodParameters("ChangeStartMode")
                        inParams("StartMode") = GetServiceStartTypeAsString(type)
                        Dim outParams As ManagementBaseObject = srv.InvokeMethod("ChangeStartMode", inParams, Nothing)
                        res = CType(outParams("ReturnValue"), SERVICE_RETURN_CODE_WMI)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.SERVICE_RETURN_CODE_WMI.Success)

            Catch ex As Exception
                errMsg = ex.Message
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

        ' Return a String which describes start type for service
        Private Shared Function GetServiceStartTypeAsString(ByVal type As NativeEnums.ServiceStartType) As String
            Select Case type
                Case NativeEnums.ServiceStartType.AutoStart
                    Return "Automatic"
                Case NativeEnums.ServiceStartType.BootStart
                    Return "Boot"
                Case NativeEnums.ServiceStartType.StartDisabled
                    Return "Disabled"
                Case NativeEnums.ServiceStartType.SystemStart
                    Return "System"
                Case NativeEnums.ServiceStartType.DemandStart
                    Return "Manual"
            End Select
            Return ""
        End Function


    End Class

End Namespace
