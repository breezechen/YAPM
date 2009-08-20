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



' Thanks to ShareVB for the KernelMemory driver.
' http://www.vbfrance.com/codes/LISTER-HANDLES-FICHIERS-CLE-REGISTRES-OUVERTS-PROGRAMME-NT_39333.aspx

Option Strict On

Imports YAPM.Native.Api

Namespace Native.Driver

    Friend Class DriverCtrl

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================

        ' Handle to manager
        Private hSCM As IntPtr

        ' Handle to service
        Private hService As IntPtr

        Private mvarServiceName As String
        Private mvarServiceDisplayName As String
        Private mvarServiceType As NativeEnums.ServiceType
        Private mvarServiceStartType As NativeEnums.ServiceStartType
        Private mvarServiceErrorType As NativeEnums.ServiceErrorControl
        Private mvarServiceFileName As String


        ' ========================================
        ' Public properties
        ' ========================================

        Public Property ServiceFileName() As String
            Get
                ServiceFileName = mvarServiceFileName
            End Get
            Set(ByVal Value As String)
                mvarServiceFileName = Value
            End Set
        End Property


        Public Property ServiceErrorType() As NativeEnums.ServiceErrorControl
            Get
                ServiceErrorType = mvarServiceErrorType
            End Get
            Set(ByVal Value As NativeEnums.ServiceErrorControl)
                mvarServiceErrorType = Value
            End Set
        End Property


        Public Property ServiceStartType() As NativeEnums.ServiceStartType
            Get
                ServiceStartType = mvarServiceStartType
            End Get
            Set(ByVal Value As NativeEnums.ServiceStartType)
                mvarServiceStartType = Value
            End Set
        End Property


        Public Property ServiceType() As NativeEnums.ServiceType
            Get
                ServiceType = mvarServiceType
            End Get
            Set(ByVal Value As NativeEnums.ServiceType)
                mvarServiceType = Value
            End Set
        End Property


        Public Property ServiceDisplayName() As String
            Get
                ServiceDisplayName = mvarServiceDisplayName
            End Get
            Set(ByVal Value As String)
                mvarServiceDisplayName = Value
            End Set
        End Property


        Public Property ServiceName() As String
            Get
                ServiceName = mvarServiceName
            End Get
            Set(ByVal Value As String)
                mvarServiceName = Value
            End Set
        End Property


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Get a handle to our driver
        Public Function OpenDriver() As IntPtr
            Return NativeFunctions.CreateFile("\\.\" & mvarServiceName, _
                             NativeEnums.EFileAccess._GenericRead Or NativeEnums.EFileAccess._GenericWrite, _
                             NativeEnums.EFileShare._Read Or NativeEnums.EFileShare._Write, _
                             IntPtr.Zero, _
                             NativeEnums.ECreationDisposition._OpenExisting, _
                            0, _
                             IntPtr.Zero)
        End Function

        ' Close service manager
        Public Sub CloseSCM()
            NativeFunctions.CloseServiceHandle(hSCM)
            hSCM = IntPtr.Zero
        End Sub

        ' Query state of our service
        Public Function QueryServiceState() As NativeEnums.ServiceState

            Dim ss As NativeStructs.ServiceStatus

            ' Open service
            If OpenService() Then
                NativeFunctions.QueryServiceStatus(hService, ss)
                Return ss.CurrentState
            Else
                Return NativeEnums.ServiceState.Unknown
            End If

        End Function

        ' Open a handle to our service
        Public Function OpenService() As Boolean

            If hService.IsNull Then

                hService = NativeFunctions.OpenService(hSCM, mvarServiceName, _
                            Native.Security.ServiceAccess.QueryStatus Or _
                            Native.Security.ServiceAccess.Start Or _
                            Native.Security.ServiceAccess.Stop Or _
                            Native.Security.ServiceAccess.Delete)

                If hService.IsNull Then
                    ' Try with lower requirements
                    hService = NativeFunctions.OpenService(hSCM, mvarServiceName, _
                                        Native.Security.ServiceAccess.QueryStatus Or _
                                        Native.Security.ServiceAccess.Start Or _
                                        Native.Security.ServiceAccess.Stop)
                End If

                Return hService.IsNotNull
            Else
                Return True
            End If

        End Function

        ' Registrer our service
        Public Function InstallService() As Boolean

            Dim ret As Boolean

            hService = NativeFunctions.CreateService(hSCM, mvarServiceName, _
                                                     mvarServiceDisplayName, _
                    Native.Security.ServiceAccess.QueryStatus Or _
                    Native.Security.ServiceAccess.Start Or _
                    Native.Security.ServiceAccess.Stop, _
                    mvarServiceType, mvarServiceStartType, mvarServiceErrorType, _
                    mvarServiceFileName, Nothing, IntPtr.Zero, Nothing, Nothing, Nothing)
            ret = hService.IsNotNull

            ' Close service handle
            Call CloseService()

            ' Reopen with good access
            Call OpenService()

            Return ret
        End Function

        ' Remove service
        Public Function RemoveService() As Boolean
            Dim ret As Boolean

            ' Open service
            If OpenService() Then
                ' Delete service
                ret = NativeFunctions.DeleteService(hService)
                ' Close handle
                Call CloseService()
                Return ret
            Else
                Return False
            End If
        End Function

        ' Start our service
        Public Function StartService() As Boolean

            ' Open our service
            If OpenService() Then

                ' Start !
                If NativeFunctions.StartService(hService, 0, Nothing) Then
                    ' Wait for the service to be running
                    Do While QueryServiceState() <> NativeEnums.ServiceState.Running
                        NativeFunctions.Sleep(500)
                    Loop
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        ' Stop service
        Public Function StopService() As Boolean

            Dim bResult As Boolean
            Dim ss As NativeStructs.ServiceStatusProcess

            ' Open service
            If OpenService() Then

                'on demande l'arrêt du service
                bResult = NativeFunctions.ControlService(hService, _
                                                         NativeEnums.ServiceControl.Stop, ss)
                If bResult Then
                    ' Wait for the service to be stopped
                    Do While QueryServiceState() <> NativeEnums.ServiceState.Stopped
                        NativeFunctions.Sleep(500)
                    Loop
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        ' Constructor
        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub


        ' ========================================
        ' Private functions
        ' ========================================

        ' Open handle to service manager
        Private Sub OpenSCM()

            If hSCM.IsNull Then

                hSCM = NativeFunctions.OpenSCManager(Nothing, Nothing, _
                                    Native.Security.ServiceManagerAccess.EnumerateService Or _
                                    Native.Security.ServiceManagerAccess.CreateService)

                ' Try with lower requirements
                If hSCM.IsNull Then
                    hSCM = NativeFunctions.OpenSCManager(Nothing, Nothing, _
                                    Native.Security.ServiceManagerAccess.EnumerateService)
                End If
            End If
        End Sub

        ' Close service handle
        Private Sub CloseService()
            NativeFunctions.CloseServiceHandle(hService)
            hService = IntPtr.Zero
        End Sub

        ' Initialization
        Private Sub Class_Initialize_Renamed()
            ' Open SCM
            Call OpenSCM()
        End Sub

        ' Terminate
        Private Sub Class_Terminate_Renamed()
            Call CloseService()
            Call CloseSCM()
        End Sub
        Protected Overrides Sub Finalize()
            Class_Terminate_Renamed()
            MyBase.Finalize()
        End Sub

    End Class

End Namespace