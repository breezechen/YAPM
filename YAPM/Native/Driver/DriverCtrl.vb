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

Namespace Native

    Friend Class DriverCtrl

        'handle du SCM
        Dim hSCM As IntPtr
        'handle du service
        Dim hService As IntPtr

        'variables locales de stockage des valeurs de propriétés
        Private mvarServiceName As String
        Private mvarServiceDisplayName As String
        Private mvarServiceType As NativeEnums.ServiceType
        Private mvarServiceStartType As NativeEnums.ServiceStartType
        Private mvarServiceErrorType As NativeEnums.ServiceErrorControl
        Private mvarServiceFileName As String


        'permet de définir toutes les propriétés du driver
        '=================================================

        Public Property ServiceFileName() As String
            Get
                'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
                'Syntax: Debug.Print X.ServiceFileName
                ServiceFileName = mvarServiceFileName
            End Get
            Set(ByVal Value As String)
                'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
                'Syntax: X.ServiceFileName = 5
                mvarServiceFileName = Value
            End Set
        End Property


        Public Property ServiceErrorType() As NativeEnums.ServiceErrorControl
            Get
                'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
                'Syntax: Debug.Print X.ServiceErrorType
                ServiceErrorType = mvarServiceErrorType
            End Get
            Set(ByVal Value As NativeEnums.ServiceErrorControl)
                'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
                'Syntax: X.ServiceErrorType = 5
                mvarServiceErrorType = Value
            End Set
        End Property


        Public Property ServiceStartType() As NativeEnums.ServiceStartType
            Get
                'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
                'Syntax: Debug.Print X.ServiceStartType
                ServiceStartType = mvarServiceStartType
            End Get
            Set(ByVal Value As NativeEnums.ServiceStartType)
                'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
                'Syntax: X.ServiceStartType = 5
                mvarServiceStartType = Value
            End Set
        End Property


        Public Property ServiceType() As NativeEnums.ServiceType
            Get
                'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
                'Syntax: Debug.Print X.ServiceType
                ServiceType = mvarServiceType
            End Get
            Set(ByVal Value As NativeEnums.ServiceType)
                'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
                'Syntax: X.ServiceType = 5
                mvarServiceType = Value
            End Set
        End Property


        Public Property ServiceDisplayName() As String
            Get
                'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
                'Syntax: Debug.Print X.ServiceDisplayName
                ServiceDisplayName = mvarServiceDisplayName
            End Get
            Set(ByVal Value As String)
                'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
                'Syntax: X.ServiceDisplayName = 5
                mvarServiceDisplayName = Value
            End Set
        End Property


        Public Property ServiceName() As String
            Get
                'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
                'Syntax: Debug.Print X.ServiceName
                ServiceName = mvarServiceName
            End Get
            Set(ByVal Value As String)
                'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
                'Syntax: X.ServiceName = 5
                mvarServiceName = Value
            End Set
        End Property

        'permet d'obtenir un handle du SCM
        Private Sub OpenSCM()
            'si pas déjà ouvert
            If hSCM.IsNull Then
                'on en demande un avec autorisations d'enregistrement
                hSCM = NativeFunctions.OpenSCManager(Nothing, Nothing, _
                                    Native.Security.ServiceManagerAccess.EnumerateService Or _
                                    Native.Security.ServiceManagerAccess.CreateService)
                'si pas l'autorisation nécessaire pour l'utilisateur en cours
                If hSCM.IsNull Then
                    'on demande une ouverture sans ces autorisations
                    hSCM = NativeFunctions.OpenSCManager(Nothing, Nothing, _
                                    Native.Security.ServiceManagerAccess.EnumerateService)
                End If
            End If
        End Sub

        'permet d'obtenir un handle sur le driver
        Public Function OpenDriver() As IntPtr
            Return NativeFunctions.CreateFile("\\.\" & mvarServiceName, _
                             NativeEnums.EFileAccess._GenericRead Or NativeEnums.EFileAccess._GenericWrite, _
                             NativeEnums.EFileShare._Read Or NativeEnums.EFileShare._Write, _
                             IntPtr.Zero, _
                             NativeEnums.ECreationDisposition._OpenExisting, _
                            0, _
                             IntPtr.Zero)
        End Function

        'permet de fermer le handle de SCM ouvert
        Public Sub CloseSCM()
            'on le ferme
            NativeFunctions.CloseServiceHandle(hSCM)
            'on le signale
            hSCM = IntPtr.Zero
        End Sub

        'permet de connaitre l'état du service
        Public Function QueryServiceState() As NativeEnums.ServiceState
            'état du service, retour
            Dim ss As NativeStructs.ServiceStatus
            Dim ret As Boolean

            'on demande l'ouverture du service (si pas déjà ouvert)
            ret = OpenService()

            'sinon pas d'erreur, on demande l'état du service
            NativeFunctions.QueryServiceStatus(hService, ss)

            'on le renvoie
            Return ss.CurrentState
        End Function

        'permet  d'ouvrir un handle du service
        Public Function OpenService() As Boolean
            'si pas déjà ouvert
            If hService.IsNull Then
                'on demande l'ouverture avec désenregistrement autorisés
                hService = NativeFunctions.OpenService(hSCM, mvarServiceName, Native.Security.ServiceAccess.QueryStatus Or Native.Security.ServiceAccess.Start Or Native.Security.ServiceAccess.Stop Or Native.Security.ServiceAccess.Delete)
                'si l'utlisateur en cours n'a pas le droit requis pour
                If hService.IsNull Then
                    'on demande un handle sans cette autorisation
                    hService = NativeFunctions.OpenService(hSCM, mvarServiceName, Native.Security.ServiceAccess.QueryStatus Or Native.Security.ServiceAccess.Start Or Native.Security.ServiceAccess.Stop)
                End If
                Return hService.IsNotNull
            Else
                Return True
            End If
        End Function

        'permet d'enregistrer le service
        Public Function InstallService() As Integer
            'on demande l'enregistrement du service définit par les propriétés de l'objet
            hService = NativeFunctions.CreateService(hSCM, mvarServiceName, mvarServiceDisplayName, _
                    Native.Security.ServiceAccess.QueryStatus Or _
                            Native.Security.ServiceAccess.Start Or _
                            Native.Security.ServiceAccess.Stop, _
                    mvarServiceType, mvarServiceStartType, mvarServiceErrorType, _
                    mvarServiceFileName, Nothing, IntPtr.Zero, Nothing, Nothing, Nothing)
            'conservation du code d'erreur
            InstallService = Err.LastDllError

            'fermeture du handle de service
            CloseService()
            'réouverture avec les autorisations adéquates
            OpenService()
        End Function

        'permet de désenregistrer le service
        Public Function RemoveService() As Integer
            'retour
            Dim ret As Boolean

            'ouverture du handle de service
            ret = OpenService()

            'on supprime le service
            NativeFunctions.DeleteService(hService)
            'on renvoie l'erreur éventuelle
            RemoveService = Err.LastDllError
            'on ferme le handle de service
            CloseService()
        End Function

        'permet de démarrarer le service
        Public Function StartService() As Integer
            'retour
            Dim bResult As Boolean
            Dim ret As Boolean

            'on ouvre un handle su service
            ret = OpenService()

            'on demande le démarrage du service
            bResult = NativeFunctions.StartService(hService, 0, Nothing)
            'si pas erreur
            If bResult Then
                'renvoie de l'éventuel code d'erreur
                StartService = Err.LastDllError
                'on attend que le service soit dans l'état demandé
                Do While QueryServiceState() <> NativeEnums.ServiceState.Running
                    NativeFunctions.Sleep(500)
                Loop
            Else
                'renvoie du code d'erreur
                StartService = Err.LastDllError
            End If
        End Function

        'permet de stopper le service
        Public Function StopService() As Integer
            'état su service
            Dim ss As NativeStructs.ServiceStatusProcess
            'retour
            Dim bResult As Boolean
            Dim ret As Boolean

            'on ouvre un handle du service
            ret = OpenService()

            'on demande l'arrêt du service
            bResult = NativeFunctions.ControlService(hService, NativeEnums.ServiceControl.Stop, ss)
            'on stocke l'éventuel code d'erreur
            StopService = Err.LastDllError
            'si pas d'erreur
            If bResult Then
                'on attend que le service soit dans l'état requis
                Do While QueryServiceState() <> NativeEnums.ServiceState.Stopped
                    NativeFunctions.Sleep(500)
                Loop
            End If
        End Function

        'permet de fermer le handle du service
        Private Sub CloseService()
            'on ferme le handle
            NativeFunctions.CloseServiceHandle(hService)
            'on le signale
            hService = IntPtr.Zero
        End Sub

        'constructeur
        'UPGRADE_NOTE: Class_Initializea été mis à niveau vers Class_Initialize_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Private Sub Class_Initialize_Renamed()
            'on ouvre le SCM
            OpenSCM()
        End Sub
        Public Sub New()
            MyBase.New()
            Class_Initialize_Renamed()
        End Sub

        'destructeur
        'UPGRADE_NOTE: Class_Terminatea été mis à niveau vers Class_Terminate_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Private Sub Class_Terminate_Renamed()
            'on ferme le handle du service
            CloseService()
            'on ferme le handle du SCM
            CloseSCM()
        End Sub
        Protected Overrides Sub Finalize()
            Class_Terminate_Renamed()
            MyBase.Finalize()
        End Sub
    End Class

End Namespace