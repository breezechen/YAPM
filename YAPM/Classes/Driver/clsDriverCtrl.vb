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

Option Strict Off

Friend Class clsDriverCtrl
	
	'classe de gestion d'un service simple
	
	'état d'un service
	Public Enum SVC_STATUS
		SERVICE_CONTINUE_PENDING = &H5
		SERVICE_PAUSE_PENDING = &H6
		SERVICE_PAUSED = &H7
		SERVICE_RUNNING = &H4
		SERVICE_START_PENDING = &H2
		SERVICE_STOP_PENDING = &H3
		SERVICE_STOPPED = &H1
	End Enum
	
	'structure sur l'état d'un service
	Private Structure SERVICE_STATUS
		Dim dwServiceType As Integer
		Dim dwCurrentState As Integer
		Dim dwControlsAccepted As Integer
		Dim dwWin32ExitCode As Integer
		Dim dwServiceSpecificExitCode As Integer
		Dim dwCheckPoint As Integer
		Dim dwWaitHint As Integer
	End Structure
	
	'autorisation pour l'ouverture d'un handle du SCM
	'autorise l'énumération des services
	Private Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4s
	'autorise l'enregistrement de nouveaux services
	Private Const SC_MANAGER_CREATE_SERVICE As Integer = &H2s
	
	'autorisation pour l'ouverture d'un handle d'un service
	'autorise la demande d'état
	Private Const SERVICE_QUERY_STATUS As Integer = &H4s
	'autorise le démarrage
	Private Const SERVICE_START As Integer = &H10s
	'autorise l'arrêt
	Private Const SERVICE_STOP As Integer = &H20s
	'autorise la pause et la reprise
	Private Const SERVICE_PAUSE_CONTINUE As Integer = &H40s
	'autorise le désenregistrement
	Private Const DELETE As Integer = &H10000
	
	'pour stopper un service
	Private Const SERVICE_CONTROL_STOP As Integer = &H1s
	
	'erreurs éventuelles
	Private Const ERROR_SERVICE_ALREADY_RUNNING As Integer = 1056
	Private Const ERROR_SERVICE_DISABLED As Integer = 1058
	Private Const ERROR_SERVICE_DOES_NOT_EXIST As Integer = 1060
	Private Const ERROR_SERVICE_EXISTS As Integer = 1073
	Private Const ERROR_SERVICE_MARKED_FOR_DELETE As Integer = 1072
	Private Const ERROR_SERVICE_NEVER_STARTED As Integer = 1077
	Private Const ERROR_SERVICE_NOT_ACTIVE As Integer = 1062
	Private Const ERROR_SERVICE_NOT_FOUND As Integer = 1243
	Private Const ERROR_SERVICE_NOT_IN_EXE As Integer = 1083
	
	'permet d'ouvrir un handle pour le SCM
	Private Declare Function OpenSCManager Lib "advapi32.dll"  Alias "OpenSCManagerA"(ByVal lpMachineName As String, ByVal lpDatabaseName As String, ByVal dwDesiredAccess As Integer) As Integer
	'permet de fermer un handle de SCM et de service
	Private Declare Function CloseServiceHandle Lib "advapi32.dll" (ByVal hSCObject As Integer) As Integer
	
	'permet de controler un service
    Private Declare Function ControlService Lib "advapi32.dll" (ByVal hService As Integer, ByVal dwControl As Integer, ByRef lpServiceStatus As SERVICE_STATUS) As Integer
	'permet de démarrer un service
	Private Declare Function apiStartService Lib "advapi32.dll"  Alias "StartServiceA"(ByVal hService As Integer, ByVal dwNumServiceArgs As Integer, ByVal lpServiceArgVectors As Integer) As Integer
	'permet d'obtenir un handle d'un service
	Private Declare Function apiOpenService Lib "advapi32.dll"  Alias "OpenServiceA"(ByVal hSCManager As Integer, ByVal lpServiceName As String, ByVal dwDesiredAccess As Integer) As Integer
	'permet d'obtenir l'état d'un service
    Private Declare Function QueryServiceStatus Lib "advapi32.dll" (ByVal hService As Integer, ByRef lpServiceStatus As SERVICE_STATUS) As Integer
	'permet d'enregistrer un service
    Private Declare Function CreateService Lib "advapi32.dll" Alias "CreateServiceA" (ByVal hSCManager As Integer, ByVal lpServiceName As String, ByVal lpDisplayName As String, ByVal dwDesiredAccess As Integer, ByVal dwServiceType As Integer, ByVal dwStartType As Integer, ByVal dwErrorControl As Integer, ByVal lpBinaryPathName As String, ByVal lpLoadOrderGroup As String, ByVal lpdwTagId As Integer, ByVal lpDependencies As String, ByVal lp As String, ByVal lpPassword As String) As Integer
	'permet de supprimer un service
	Private Declare Function DeleteService Lib "advapi32.dll" (ByVal hService As Integer) As Integer
	
	'permet de faire dormir l'application un certain nombre de ms
	Private Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As Integer)
	
	'permet de formatter un message d'erreur système
    Private Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Integer, ByVal lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, ByVal lpBuffer As String, ByVal nSize As Integer, ByRef Arguments As Integer) As Integer
	
	' du système
	Private Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000s
	' dans la langue du système
	Private Const LANG_NEUTRAL As Short = &H0s
	
	'handle du SCM
	Dim hSCM As Integer
	'handle du service
	Dim hService As Integer
	
	'variables locales de stockage des valeurs de propriétés
	Private mvarServiceName As String 'copie locale
	Private mvarServiceDisplayName As String 'copie locale
	Private mvarServiceType As Integer 'copie locale
	Private mvarServiceStartType As Integer 'copie locale
	Private mvarServiceErrorType As Integer 'copie locale
	Private mvarServiceFileName As String 'copie locale
	
	'permet de formater un message correspondant à un code d'erreur
	'==============================================================
	'ErrCode : code de l'erreur
	'renvoie un chaine descriptive de l'erreur
	Public Function FormatErrorMessage(ByVal ErrCode As Integer) As String
		
		Dim sBuffer As String ' Définit un buffer pour contenir le message
		Dim nBufferSize As Integer ' Taille du buffer
		
		'taille du buffer
		nBufferSize = 1024
		'on fait la place pour 1024 caractères
		sBuffer = New String(Chr(0), nBufferSize)
		
		'demande de la chaine descriptive
		nBufferSize = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0, ErrCode, LANG_NEUTRAL, sBuffer, nBufferSize, 0)
		
		'si erreur connue
		If nBufferSize > 0 Then
			'on supprime les zéros terminaux en trop
            Return Left(sBuffer, nBufferSize)
			'sinon erreur inconnue
		ElseIf nBufferSize = 0 Then 
			'on le signal dans la chaine descriptive
            Return "Erreur " & ErrCode & " non définie."
        Else
            Return ""
        End If
	End Function
	
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
	
	
	Public Property ServiceErrorType() As Integer
		Get
			'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
			'Syntax: Debug.Print X.ServiceErrorType
			ServiceErrorType = mvarServiceErrorType
		End Get
		Set(ByVal Value As Integer)
			'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
			'Syntax: X.ServiceErrorType = 5
			mvarServiceErrorType = Value
		End Set
	End Property
	
	
	Public Property ServiceStartType() As Integer
		Get
			'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
			'Syntax: Debug.Print X.ServiceStartType
			ServiceStartType = mvarServiceStartType
		End Get
		Set(ByVal Value As Integer)
			'utilisé lors de l'affectation d'une valeur à la propriété, du coté gauche de l'affectation.
			'Syntax: X.ServiceStartType = 5
			mvarServiceStartType = Value
		End Set
	End Property
	
	
	Public Property ServiceType() As Integer
		Get
			'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
			'Syntax: Debug.Print X.ServiceType
			ServiceType = mvarServiceType
		End Get
		Set(ByVal Value As Integer)
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
		If hSCM = 0 Then
			'on en demande un avec autorisations d'enregistrement
			hSCM = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ENUMERATE_SERVICE Or SC_MANAGER_CREATE_SERVICE)
			'si pas l'autorisation nécessaire pour l'utilisateur en cours
			If hSCM = 0 Then
				'on demande une ouverture sans ces autorisations
				hSCM = OpenSCManager(vbNullString, vbNullString, SC_MANAGER_ENUMERATE_SERVICE)
			End If
		End If
	End Sub
	
	'permet de fermer le handle de SCM ouvert
	Public Sub CloseSCM()
		'on le ferme
		CloseServiceHandle(hSCM)
		'on le signale
		hSCM = 0
	End Sub
	
	'permet de connaitre l'état du service
	Public Function QueryServiceState() As Integer
		'état du service, retour
		Dim ss As SERVICE_STATUS
		Dim ret As Integer
		
		'on demande l'ouverture du service (si pas déjà ouvert)
		ret = OpenService
		'si erreur, erreur lol
		If ret Then QueryServiceState = ret : Exit Function
		
		'sinon pas d'erreur, on demande l'état du service
		QueryServiceStatus(hService, ss)
		
		'on le renvoie
		QueryServiceState = ss.dwCurrentState
	End Function
	
	'permet  d'ouvrir un handle du service
	Public Function OpenService() As Integer
		'si pas déjà ouvert
		If hService = 0 Then
			'on demande l'ouverture avec désenregistrement autorisés
			hService = apiOpenService(hSCM, mvarServiceName, SERVICE_QUERY_STATUS Or SERVICE_START Or SERVICE_STOP Or DELETE)
			'si l'utlisateur en cours n'a pas le droit requis pour
			If hService = 0 Then
				'on demande un handle sans cette autorisation
				hService = apiOpenService(hSCM, mvarServiceName, SERVICE_QUERY_STATUS Or SERVICE_START Or SERVICE_STOP)
			End If
			'renvoie du code d'erreur dans tous les cas (même ERROR_SUCCESS)
			OpenService = Err.LastDllError
		End If
	End Function
	
	'permet d'enregistrer le service
	Public Function InstallService() As Integer
		'on demande l'enregistrement du service définit par les propriétés de l'objet
		hService = CreateService(hSCM, mvarServiceName, mvarServiceDisplayName, SERVICE_QUERY_STATUS Or SERVICE_START Or SERVICE_STOP, mvarServiceType, mvarServiceStartType, mvarServiceErrorType, mvarServiceFileName, vbNullString, 0, vbNullString, vbNullString, vbNullString)
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
		Dim ret As Integer
		
		'ouverture du handle de service
		ret = OpenService
		'si erreur, erreur lol
		If ret Then RemoveService = ret : Exit Function
		
		'on supprime le service
		DeleteService(hService)
		'on renvoie l'erreur éventuelle
		RemoveService = Err.LastDllError
		'on ferme le handle de service
		CloseService()
	End Function
	
	'permet de démarrarer le service
	Public Function StartService() As Integer
		'retour
		Dim bResult As Boolean
		Dim ret As Integer
		
		'on ouvre un handle su service
		ret = OpenService
		'si erreur, erreur lol
		If ret Then StartService = ret : Exit Function
		
		'on demande le démarrage du service
		bResult = apiStartService(hService, 0, 0)
		'si pas erreur
		If bResult Then
			'renvoie de l'éventuel code d'erreur
			StartService = Err.LastDllError
			'on attend que le service soit dans l'état demandé
			Do While QueryServiceState() <> SVC_STATUS.SERVICE_RUNNING
				Sleep(500)
			Loop 
		Else
			'renvoie du code d'erreur
			StartService = Err.LastDllError
		End If
	End Function
	
	'permet de stopper le service
	Public Function StopService() As Integer
		'état su service
		Dim ss As SERVICE_STATUS
		'retour
		Dim bResult As Boolean
		Dim ret As Integer
		
		'on ouvre un handle du service
		ret = OpenService
		'si erreur, erreur...lol
		If ret Then StopService = ret : Exit Function
		
		'on demande l'arrêt du service
		bResult = ControlService(hService, SERVICE_CONTROL_STOP, ss)
		'on stocke l'éventuel code d'erreur
		StopService = Err.LastDllError
		'si pas d'erreur
		If bResult Then
			'on attend que le service soit dans l'état requis
			Do While QueryServiceState() <> SVC_STATUS.SERVICE_STOPPED
				Sleep(500)
			Loop 
		End If
	End Function
	
	'permet de fermer le handle du service
	Private Sub CloseService()
		'on ferme le handle
		CloseServiceHandle(hService)
		'on le signale
		hService = 0
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