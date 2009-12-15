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
Imports Native.Api.Enums

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
                Dim theId As Integer = CInt(refService.GetPropertyValue(WmiInfoService.ProcessId.ToString))

                If all OrElse pid = theId Then

                    With obj
                        .DisplayName = CStr(refService.GetPropertyValue(WmiInfoService.DisplayName.ToString))
                        .ServiceName = CStr(refService.GetPropertyValue(WmiInfoService.Name.ToString))
                        With .ServiceStatusProcess
                            .CheckPoint = CInt(refService.GetPropertyValue(WmiInfoService.CheckPoint.ToString))
                            If CBool(refService.GetPropertyValue(WmiInfoService.AcceptPause.ToString)) Then
                                .ControlsAccepted = .ControlsAccepted Or Native.Api.NativeEnums.ServiceAccept.PauseContinue
                            End If
                            If CBool(refService.GetPropertyValue(WmiInfoService.AcceptStop.ToString)) Then
                                .ControlsAccepted = .ControlsAccepted Or Native.Api.NativeEnums.ServiceAccept.Stop
                            End If
                            .CurrentState = Native.Functions.Service.GetServiceStateFromStringH(CStr(refService.GetPropertyValue(WmiInfoService.State.ToString)))
                            .ProcessID = theId
                            '.ServiceFlags
                            .ServiceSpecificExitCode = CInt(refService.GetPropertyValue(WmiInfoService.ServiceSpecificExitCode.ToString))
                            .ServiceType = Native.Functions.Service.GetServiceTypeFromStringH(CStr(refService.GetPropertyValue(WmiInfoService.ServiceType.ToString)))
                            .WaitHint = CInt(refService.GetPropertyValue(WmiInfoService.WaitHint.ToString))
                            .Win32ExitCode = CInt(refService.GetPropertyValue(WmiInfoService.ExitCode.ToString))
                        End With
                    End With

                    ' Do we have to get fixed infos ?
                    Dim _servInfos As New serviceInfos(obj, True)

                    Dim conf As New Native.Api.NativeStructs.QueryServiceConfig
                    With conf
                        .BinaryPathName = CStr(refService.GetPropertyValue(WmiInfoService.PathName.ToString))
                        '.Dependencies
                        .DisplayName = CStr(refService.GetPropertyValue(WmiInfoService.DisplayName.ToString))
                        .ErrorControl = Native.Functions.Service.GetServiceErrorControlFromStringH(CStr(refService.GetPropertyValue(WmiInfoService.ErrorControl.ToString)))
                        '.LoadOrderGroup 
                        .ServiceStartName = CStr(refService.GetPropertyValue(WmiInfoService.StartName.ToString))
                        .StartType = Native.Functions.Service.GetServiceStartTypeFromStringH(CStr(refService.GetPropertyValue(WmiInfoService.StartMode.ToString)))
                        .TagID = CInt(refService.GetPropertyValue(WmiInfoService.TagId.ToString))
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
                Dim res As WmiServiceReturnCode = Enums.WmiServiceReturnCode.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WmiInfoService.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("PauseService", Nothing), Enums.WmiServiceReturnCode)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.WmiServiceReturnCode.Success)

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
                Dim res As WmiServiceReturnCode = Enums.WmiServiceReturnCode.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WmiInfoService.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("ResumeService", Nothing), Enums.WmiServiceReturnCode)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.WmiServiceReturnCode.Success)

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
                Dim res As WmiServiceReturnCode = Enums.WmiServiceReturnCode.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WmiInfoService.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("StartService", Nothing), Enums.WmiServiceReturnCode)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.WmiServiceReturnCode.Success)

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
                Dim res As WmiServiceReturnCode = Enums.WmiServiceReturnCode.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WmiInfoService.Name.ToString)) = name Then
                        res = CType(srv.InvokeMethod("StopService", Nothing), Enums.WmiServiceReturnCode)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.WmiServiceReturnCode.Success)

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
                Dim res As WmiServiceReturnCode = Enums.WmiServiceReturnCode.AccessDenied
                For Each srv As ManagementObject In objSearcher.Get
                    If CStr(srv.GetPropertyValue(WmiInfoService.Name.ToString)) = name Then
                        Dim inParams As ManagementBaseObject = srv.GetMethodParameters("ChangeStartMode")
                        inParams("StartMode") = GetServiceStartTypeAsString(type)
                        Dim outParams As ManagementBaseObject = srv.InvokeMethod("ChangeStartMode", inParams, Nothing)
                        res = CType(outParams("ReturnValue"), WmiServiceReturnCode)
                        Exit For
                    End If
                Next

                errMsg = res.ToString
                Return (res = Enums.WmiServiceReturnCode.Success)

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
