' =======================================================
' Yet Another Process Monitor (YAPM)
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

Imports System.Runtime.InteropServices

Public Class clsOpenedHandles
    'dérive du contrôle de driver pour avoir le total des appels à StartService et StopService
    'Inherits clsDriverCtrl
    'module ermettant d'obtenir une liste des handles de tous les processus du systeme

    '==========================================================================================
    'constantes
    '==========================================================================================

    'constante pour NtQuerySystemInformation
    Private Const SystemHandleInformation As Integer = 16
    Private Const STATUS_INFO_LENGTH_MISMATCH As Integer = &HC0000004
    Private Const STATUS_SUCCESS As Integer = &H0

    'constante pour NtQueryObject
    Private Const ObjectBasicInformation As Integer = 0 ' 0 Y N
    Private Const ObjectNameInformation As Integer = 1 ' 1 Y N
    Private Const ObjectTypeInformation As Integer = 2 ' 2 Y N
    Private Const ObjectAllTypesInformation As Integer = 3 ' 3 Y N
    Private Const ObjectHandleInformation As Integer = 4 ' 4 Y Y

    'autorise à créer un thread dans le processus
    Private Const PROCESS_CREATE_THREAD As Integer = &H2
    'autorise les opérations sur la mémoire du processus
    Private Const PROCESS_VM_OPERATION As Integer = &H8
    'autorise les lectures sur la mémoire du processus
    Private Const PROCESS_VM_READ As Integer = &H10
    'autorise les écritures sur la mémoire du processus
    Private Const PROCESS_VM_WRITE As Integer = &H20
    'autorise à dupliquer un handle (pour OpenProcess)
    Private Const PROCESS_DUP_HANDLE As Integer = (&H40S)
    'avec le même accès que dans le processus original (pour DuplicateHandle)
    Private Const DUPLICATE_SAME_ACCESS As Integer = &H2S
    'inclure les processus dans la vue (pour CreateToolhelp32Snapshot)
    Private Const TH32CS_SNAPPROCESS As Integer = &H2S

    Private Const PIPE_NOWAIT As Integer = &H1
    Private Const PIPE_WAIT As Integer = &H0
    Private Const PIPE_READMODE_MESSAGE As Integer = &H2
    Private Const PIPE_READMODE_BYTE As Integer = &H0

    'constantes pour OBJECT_ATTRIBUTES
    Private Const OBJ_INHERIT As Integer = &H2
    Private Const OBJ_PERMANENT As Integer = &H10
    Private Const OBJ_EXCLUSIVE As Integer = &H20
    Private Const OBJ_CASE_INSENSITIVE As Integer = &H40
    Private Const OBJ_OPENIF As Integer = &H80
    Private Const OBJ_OPENLINK As Integer = &H100
    Private Const OBJ_KERNEL_HANDLE As Integer = &H200

    'accès aux dossiers
    Private Const DIRECTORY_QUERY As Integer = &H1
    Private Const DIRECTORY_TRAVERSE As Integer = &H2
    Private Const DIRECTORY_CREATE_OBJECT As Integer = &H4
    Private Const DIRECTORY_CREATE_SUBDIRECTORY As Integer = &H8

    'accès au liens symboliques
    Private Const SYMBOLIC_LINK_QUERY As Integer = &H1

    'constantes d'erreur
    Private Const STATUS_NO_MORE_ENTRIES As Integer = &H8000001A
    Private Const STATUS_BUFFER_TOO_SMALL As Integer = &HC0000023

    '==========================================================================================
    'structures perso
    '==========================================================================================

    'structure contenant des infos sur un handle d'un processus
    Private Structure HandleInfo
        Dim ProcessID As Integer 'ID du processus propriétaire
        Dim Flags As Byte ' 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
        Dim Handle As Short 'valeur du handle
        Dim ObjectName As String 'nom de l'objet
        Dim NameInformation As String 'type de l'ojet
        'UPGRADE_NOTE: Objecta été mis à niveau vers Object_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Object_Renamed As Integer 'adresse de l'objet
        Dim GrantedAccess As Integer 'accès autorisés à l'objet
        Dim Attributes As Integer 'attributs
        Dim HandleCount As Integer 'nombre de handle de ce type dans le système
        Dim PointerCount As Integer 'nombre de références pointeurs à cet objet dans le système
        Dim CreateTime As Decimal 'date de création de l'objet
        Dim GenericRead As Integer 'accès générique
        Dim GenericWrite As Integer
        Dim GenericExecute As Integer
        Dim GenericAll As Integer
        Dim ObjectCount As Integer '
        Dim PeakObjectCount As Integer '
        Dim PeakHandleCount As Integer '
        Dim InvalidAttributes As Integer 'définit les attributs invalides pour ce type d'objet
        Dim ValidAccess As Integer '
        Dim Unknown As Byte
        Dim MaintainHandleDatabase As Byte '
        Dim PoolType As Integer 'type de pool utilisé par l'objet
        Dim PagedPoolUsage As Integer 'paged pool utilisé
        Dim NonPagedPoolUsage As Integer 'non-paged pool utilisé
    End Structure

    'une entrée d'un dossier
    Private Structure DirectoryEntry
        Dim ObjectName As String
        Dim ObjectTypeName As String
    End Structure

    '==========================================================================================
    'structures Windows
    '==========================================================================================

    'structure contenant une chaine de caractere UNICODE
    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Private Structure UNICODE_STRING
        Dim Length As Short
        Dim MaximumLength As Short
        Dim Buffer As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure GENERIC_MAPPING
        Dim GenericRead As Integer
        Dim GenericWrite As Integer
        Dim GenericExecute As Integer
        Dim GenericAll As Integer
    End Structure

    '==========================================================================================
    'structures non documentées
    '==========================================================================================

    'structure de donnée de handle renvoyée par NtQuerySystemInformation
    <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Private Structure SYSTEM_HANDLE_INFORMATION ' Information Class 16
        Dim ProcessID As Integer
        Dim ObjectTypeNumber As Byte
        Dim Flags As Byte ' 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
        Dim Handle As Short
        Dim Object_Pointer As Integer
        Dim GrantedAccess As Integer
    End Structure

    'informations sur une entrée du contenu d'un dossier
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure DIRECTORY_BASIC_INFORMATION
        Dim ObjectName As UNICODE_STRING
        Dim ObjectTypeName As UNICODE_STRING
    End Structure

    'structure contenant des infos sur les type d'objet du systeme
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure OBJECT_BASIC_INFORMATION ' Information Class 0
        Dim Attributes As Integer
        Dim GrantedAccess As Integer
        Dim HandleCount As Integer
        Dim PointerCount As Integer
        Dim PagedPoolUsage As Integer
        Dim NonPagedPoolUsage As Integer
        Dim Reserved1 As Integer
        Dim Reserved2 As Integer
        Dim Reserved3 As Integer
        Dim NameInformationLength As Integer
        Dim TypeInformationLength As Integer
        Dim SecurityDescriptorLength As Integer
        Dim CreateTime As Long
    End Structure

    'structure contenant le nom d'un type d'objet
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure OBJECT_NAME_INFORMATION ' Information Class 1
        Dim Name As UNICODE_STRING
    End Structure

    'attributs d'un objet
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure OBJECT_ATTRIBUTES
        Dim Length As Integer
        Dim RootDirectoryHandle As Integer
        Dim ObjectName As IntPtr 'PUNICODE_STRING
        Dim Attributes As Integer
        Dim SecurityDescriptor As Integer
        Dim SecurityQualityOfService As Integer
    End Structure

    'information sur un type d'objet
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure OBJECT_TYPE_INFORMATION ' Information Class 2
        Dim Name As UNICODE_STRING
        Dim ObjectCount As Integer
        Dim HandleCount As Integer
        Dim Reserved1 As Integer
        Dim Reserved2 As Integer
        Dim Reserved3 As Integer
        Dim Reserved4 As Integer
        Dim PeakObjectCount As Integer
        Dim PeakHandleCount As Integer
        Dim Reserved5 As Integer
        Dim Reserved6 As Integer
        Dim Reserved7 As Integer
        Dim Reserved8 As Integer
        Dim InvalidAttributes As Integer
        Dim GenericMapping As GENERIC_MAPPING
        Dim ValidAccess As Integer
        Dim Unknown As Byte
        Dim MaintainHandleDatabase As Byte
        Dim PoolType As Integer
        Dim PagedPoolUsage As Integer
        Dim NonPagedPoolUsage As Integer
    End Structure

    'information sur les types d'objets
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure OBJECT_ALL_TYPES_INFORMATION 'Information Class 3
        Dim NumberOfTypes As Integer
        Dim TypeInformation As OBJECT_TYPE_INFORMATION
    End Structure

    'information sur les attributs de handles
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure OBJECT_HANDLE_ATTRIBUTE_INFORMATION ' Information Class 4
        Dim Inherit As Byte
        Dim ProtectFromClose As Byte
    End Structure

    '==========================================================================================
    'API natives (non documentées)
    '==========================================================================================

    'renvoie des informations sur le systeme
    Private Declare Function NtQuerySystemInformation Lib "ntdll" (ByVal SystemInformationClass As Integer, ByVal SystemInformation As IntPtr, ByVal SystemInformationLength As Integer, ByRef ReturnLength As Integer) As Integer

    'renvoie des infos sur un type d'objet
    Private Declare Function NtQueryObject Lib "ntdll" (ByVal ObjectHandle As Integer, ByVal ObjectInformationClass As Integer, ByVal ObjectInformation As IntPtr, ByVal ObjectInformationLength As Integer, ByRef ReturnLength As Integer) As Integer

    'ouvre un dossier
    Private Declare Function NtOpenDirectoryObject Lib "ntdll" (ByRef hDirectoryHandle As Integer, ByVal DesiredAccess As Integer, ByRef ObjectAttributes As OBJECT_ATTRIBUTES) As Integer

    'liste le contenu d'un dossier
    Private Declare Function NtQueryDirectoryObject Lib "ntdll" (ByVal hDirectoryHandle As Integer, ByVal lpBuffer As IntPtr, ByVal cbBufferLength As Integer, ByVal bReturnSingleEntry As Integer, ByVal bRestartScan As Integer, ByRef lpContext As Integer, ByRef lpReturnLength As Integer) As Integer

    'ouvre un lien symbolique
    Private Declare Function NtOpenSymbolicLinkObject Lib "ntdll" (ByRef hSymbolicLinkHandle As Integer, ByVal DesiredAccess As Integer, ByRef ObjectAttributes As OBJECT_ATTRIBUTES) As Integer

    'permet d'obtenir le nom de l'objet pointé par le lien
    Private Declare Function NtQuerySymbolicLinkObject Lib "ntdll" (ByVal hSymbolicLinkHandle As Integer, ByVal lpTargetName As IntPtr, ByRef lpReturnLength As Integer) As Integer

    'ferme un handle ouvert avec NtOpenXXX
    Private Declare Function NtClose Lib "ntdll" (ByVal hHandle As Integer) As Integer


    '==========================================================================================
    'API documentées
    '==========================================================================================
    Private Declare Function GetNamedPipeHandleState Lib "kernel32.dll" Alias "GetNamedPipeHandleStateA" (ByVal hNamedPipe As Integer, ByRef lpState As Integer, ByVal lpCurInstances As Integer, ByVal lpMaxCollectionCount As Integer, ByVal lpCollectDataTimeout As Integer, ByVal lpUserName As Integer, ByVal nMaxUserNameSize As Integer) As Integer

    'attendre indéfiniment après un handle
    Private Const INFINITE As Integer = &HFFFF
    'attend après un objet
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer

    'renvoie une liste des lecteurs du système
    Private Declare Function GetLogicalDriveStrings Lib "kernel32.dll" Alias "GetLogicalDriveStringsA" (ByVal nBufferLength As Integer, ByVal lpBuffer As String) As Integer
    'copie une zone mémoire dans une autre

    'renvoie le handle d'un module
    Private Declare Function GetModuleHandle Lib "kernel32" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Integer
    'récupère l'adresse d'une API
    Private Declare Function GetProcAddress Lib "kernel32" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer

    'crée un thread dans un autre processus
    Private Declare Function CreateRemoteThread Lib "kernel32" (ByVal hProcess As Integer, ByVal lpThreadAttributes As Integer, ByVal dwStackSize As Integer, ByVal lpStartAddress As Integer, ByVal lpParameter As Integer, ByVal dwCreationFlags As Integer, ByVal lpThreadId As Integer) As Integer
    'renvoit l'ID du thread et du processus de la fenêtre spécifiée
    Private Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As Integer, ByRef lpdwProcessId As Integer) As Integer
    'renvoie le code de retour d'un thread
    Private Declare Function GetExitCodeThread Lib "kernel32" (ByVal hThread As Integer, ByRef lpExitCode As Integer) As Integer
    'renvoie le handle du processus appelant (-1)
    Private Declare Function GetCurrentProcess Lib "kernel32.dll" () As Integer
    'ouvrir le processus
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessID As Integer) As Integer

    'permet de dupliquer un handle d'un prosessus vers un autre
    Private Declare Function DuplicateHandle Lib "kernel32.dll" (ByVal hSourceProcessHandle As Integer, ByVal hSourceHandle As Integer, ByVal hTargetProcessHandle As Integer, ByRef lpTargetHandle As Integer, ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwOptions As Integer) As Integer

    'fermer un handle
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer

    'donne le nom interne d'un lettre de lecteur
    Private Declare Function QueryDosDevice Lib "kernel32.dll" Alias "QueryDosDeviceA" (ByVal lpDeviceName As String, ByVal lpTargetPath As String, ByVal ucchMax As Integer) As Integer

    Private Declare Function GetProcessIdApi Lib "kernel32.dll" Alias "GetProcessId" (ByVal ProcessHandle As Integer) As Integer
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Private Shared Function GetProcessIdOfThread(ByVal ThreadHandle As Integer) As Integer
    End Function
    <DllImport("kernel32.dll", SetLastError:=True)> _
    Private Shared Function GetThreadId(ByVal ThreadHandle As Integer) As Integer
    End Function

    '==========================================================================================
    'Variables globales à la classe
    '==========================================================================================

    'handle du processus actuellement ouvert
    Dim hProcess As Integer
    'PID du processus actuellement ouvert
    Dim lastPID As Integer

    'listes des fichiers ouverts du système
    Dim m_Files() As HandleInfo
    'nombres de fichiers ouverts dans le système
    Dim m_cHandles As Integer

    'handle vers le driver
    Dim hDriver As Integer
    Dim driver As clsDriverCtrl

    'numéro de type des objets de type "File"
    Dim m_ObjectTypeNumber As Long

    ' Vista or later ?
    Dim _isVista As Boolean = False

    '==========================================================================================
    'pour envoyer une requête au driver
    '==========================================================================================

    'crée ou ouvre un fichier ou pilote
    Private Declare Function CreateFile Lib "kernel32.dll" Alias "CreateFileA" (ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, ByVal dwShareMode As Integer, ByVal lpSecurityAttributes As Integer, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As Integer
    'permet d'envoyer des requêtes à un pilote
    Private Declare Function DeviceIoControl Lib "kernel32.dll" (ByVal hDevice As Integer, ByVal dwIoControlCode As Integer, ByRef lpInBuffer As SYSTEM_HANDLE_INFORMATION, ByVal nInBufferSize As Integer, ByVal lpOutBuffer As IntPtr, ByVal nOutBufferSize As Integer, ByRef lpBytesReturned As Integer, ByVal lpOverlapped As Integer) As Integer
    'accès en lecture
    Private Const GENERIC_READ As Integer = &H80000000
    'accès en écriture
    Private Const GENERIC_WRITE As Integer = &H40000000
    'partage de lecture
    Private Const FILE_SHARE_READ As Integer = &H1
    'partage d'écriture
    Private Const FILE_SHARE_WRITE As Integer = &H2
    'ouvrir seulement si existant
    Private Const OPEN_EXISTING As Integer = 3
    ' Version of Windows Vista
    Private Const VISTA_MAJOR_VERSION As Integer = 6


    'requête pour obtenir le nom d'un handle
    'hDevice : handle du driver KernelMemory
    'dwIoControlCode : IOCTL_KERNELMEMORY_GETOBJECTNAME
    'lpInBuffer : une structure SYSTEM_HANDLE_INFORMATION contenant les infos sur le handle
    'nInBufferSize : taille de la structure SYSTEM_HANDLE_INFORMATION
    'lpOutBuffer : tampon d'une taille suffisante pour contenir le nom du handle (au moins MAX_PATH caractères)
    'nOutBufferSize : taille de ce tampon
    'lpBytesReturned : taille des données retournée (sauf erreur : nOutBufferSize)
    'lpOverlapped : nul
    'renvoie ERROR_SUCCESS ou ERROR_BUFFER_TOO_SMALL
    Private Const IOCTL_KERNELMEMORY_GETOBJECTNAME As Integer = &H80002004

    '==========================================================================================
    'pour obtenir le privilège Debug
    '==========================================================================================

    'constantes
    'demande l'ajustement des privilèges
    Private Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
    'demande d'infos sur un token privilege
    Private Const TOKEN_QUERY As Integer = &H8
    'demande l'autorisation d'activer un privilège
    Private Const SE_PRIVILEGE_ENABLED As Integer = &H2

    'type de Local Unique ID pour le token
    Private Structure LUID
        Dim LowPart As Integer
        Dim HighPart As Integer
    End Structure
    'LUID + attributs
    Private Structure LUID_AND_ATTRIBUTES
        Dim pLuid As LUID
        Dim Attributes As Integer
    End Structure
    'structure de privilèges de token
    Private Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        Dim Privileges As LUID_AND_ATTRIBUTES
    End Structure
    'ouvre le token du processus
    Private Declare Function OpenProcessToken Lib "advapi32" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByRef TokenHandle As Integer) As Integer
    'ajuste les privilèges
    Private Declare Function AdjustTokenPrivileges Lib "advapi32" (ByVal TokenHandle As Integer, ByVal DisableAllPrivileges As Integer, ByRef NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByRef PreviousState As TOKEN_PRIVILEGES, ByRef ReturnLength As Integer) As Integer
    'regarde la valeur du privilèges
    Private Declare Function LookupPrivilegeValue Lib "advapi32" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByRef lpLuid As LUID) As Integer

    'on ouvre un handle du driver KernelMemory
    Private Function OpenDriver() As Integer
        OpenDriver = CreateFile("\\.\KernelMemory", _
                        GENERIC_READ Or GENERIC_WRITE, _
                        FILE_SHARE_READ Or FILE_SHARE_WRITE, _
                        0, _
                        OPEN_EXISTING, _
                        0, _
                        0)
    End Function
    Private Declare Sub ZeroMemory Lib "kernel32.dll" Alias "RtlZeroMemory" (ByVal Destination As Integer, ByVal Length As Integer)

    'nombre de handles
    Public ReadOnly Property Count() As Integer
        Get
            'utilisé lors de la lecture de la valeur de la propriété, du coté droit de l'instruction.
            'Syntax: Debug.Print X.Count
            Count = m_cHandles
        End Get
    End Property

    'rafraîchir la liste des handles
    Public Sub Refresh(Optional ByVal oneProcessId As Integer = -1)
        CreateQueryHandlesBuffer(oneProcessId)
    End Sub
    Public Sub Refresh(ByVal PIDs() As Integer)
        CreateQueryHandlesBuffer(PIDs)
    End Sub

    'propriétés d'un handle
    'dwIndex : index du handle
    '==========================================================================================
    Public Function GetObjectName(ByVal dwIndex As Integer) As String 'Nom de l'objet
        Return m_Files(dwIndex).ObjectName
    End Function
    Public Function GetNameInformation(ByVal dwIndex As Integer) As String 'Type de l'objet
        Return m_Files(dwIndex).NameInformation
    End Function
    Public Function GetProcessName(ByVal dwIndex As Integer) As String 'Nom du processus propriétaire
        Return GetProcessNameFromPID(m_Files(dwIndex).ProcessID)
    End Function
    Public Function GetProcessID(ByVal dwIndex As Integer) As Integer 'ID du processus propriétaire
        Return m_Files(dwIndex).ProcessID
    End Function
    Public Function GetHandle(ByVal dwIndex As Integer) As Short 'valeur du handle
        Return m_Files(dwIndex).Handle
    End Function
    Public Function GetObject_Renamed(ByVal dwIndex As Integer) As Integer 'adresse de l'objet
        Return m_Files(dwIndex).Object_Renamed
    End Function
    Public Function GetObjectCount(ByVal dwIndex As Integer) As Integer 'nombre d'objet
        Return m_Files(dwIndex).ObjectCount
    End Function
    Public Function GetGrantedAccess(ByVal dwIndex As Integer) As Integer 'accès autorisés à l'objet
        Return m_Files(dwIndex).GrantedAccess
    End Function
    Public Function GetAttributes(ByVal dwIndex As Integer) As Integer 'attributs
        Return m_Files(dwIndex).Attributes
    End Function
    Public Function GetHandleCount(ByVal dwIndex As Integer) As Integer 'nombre de handle de ce type dans le système
        Return m_Files(dwIndex).HandleCount
    End Function
    Public Function GetPointerCount(ByVal dwIndex As Integer) As Integer 'nombre de références pointeurs à cet objet dans le système
        Return m_Files(dwIndex).PointerCount
    End Function
    Public Function GetCreateTime(ByVal dwIndex As Integer) As Decimal 'date de création de l'objet
        Return m_Files(dwIndex).CreateTime
    End Function
    Public Function GetPagedPoolUsage(ByVal dwIndex As Integer) As Integer 'paged pool utilisé
        Return m_Files(dwIndex).PagedPoolUsage
    End Function
    Public Function GetNonPagedPoolUsage(ByVal dwIndex As Integer) As Integer 'non-paged pool utilisé
        Return m_Files(dwIndex).NonPagedPoolUsage
    End Function

    'crée le buffer contenant les handles
    'doit etre libérer avec DestroyHandlesBuffer avant de quitter l'application
    Private Sub CreateQueryHandlesBuffer(Optional ByVal oneProcessId As Integer = -1)
        Dim Length As Integer 'longueur du buffer
        Dim X As Integer 'compteur
        Dim ret As Integer 'valeur de retour des fonctions utilisées
        Dim lpBufferHandles As IntPtr 'pointeur vers le buffer BufferHandles
        Dim Handle As SYSTEM_HANDLE_INFORMATION 'un handle

        Length = &H100 'longueur minimale du buffer
        lpBufferHandles = Marshal.AllocHGlobal(Length)  'redimensionne le buffer
        'tant que la longueur n'est pas suffisante
        Do While NtQuerySystemInformation(SystemHandleInformation, lpBufferHandles, Length, ret) = STATUS_INFO_LENGTH_MISMATCH
            'on multiplie la taille du buffer par 2
            Length = Length * 2
            'on réalloue le buffer
            Marshal.FreeHGlobal(lpBufferHandles)
            lpBufferHandles = Marshal.AllocHGlobal(Length)
        Loop

        'demande le nombre de handles
        m_cHandles = Marshal.ReadInt32(lpBufferHandles)

        'on fait de la place pour un handle de fichier
        ReDim m_Files(m_cHandles - 1)
        'pour chaque handle
        For X = 0 To m_cHandles - 1
            'on copie les informations sur le handle
            Handle = Marshal.PtrToStructure(New IntPtr(lpBufferHandles.ToInt32 + 4 + 16 * X), Handle.GetType)
            ' Only if handle belongs to specified process
            If oneProcessId = -1 OrElse oneProcessId = Handle.ProcessID Then
                'on demande plus d'informations sur le handle de fichier
                m_Files(X) = RetrieveObject(Handle)
            End If
        Next
        'on ferme le handle du dernier processus ouvert
        CloseProcessForHandle()

        Marshal.FreeHGlobal(lpBufferHandles)
    End Sub
    Private Sub CreateQueryHandlesBuffer(ByVal PIDs As Integer())
        Dim Length As Integer 'longueur du buffer
        Dim X As Integer 'compteur
        Dim ret As Integer 'valeur de retour des fonctions utilisées
        Dim lpBufferHandles As IntPtr 'pointeur vers le buffer BufferHandles
        Dim Handle As SYSTEM_HANDLE_INFORMATION 'un handle

        Length = &H100 'longueur minimale du buffer
        lpBufferHandles = Marshal.AllocHGlobal(Length)  'redimensionne le buffer
        'tant que la longueur n'est pas suffisante
        Do While NtQuerySystemInformation(SystemHandleInformation, lpBufferHandles, Length, ret) = STATUS_INFO_LENGTH_MISMATCH
            'on multiplie la taille du buffer par 2
            Length = Length * 2
            'on réalloue le buffer
            Marshal.FreeHGlobal(lpBufferHandles)
            lpBufferHandles = Marshal.AllocHGlobal(Length)
        Loop

        'demande le nombre de handles
        m_cHandles = Marshal.ReadInt32(lpBufferHandles)

        'on fait de la place pour un handle de fichier
        ReDim m_Files(m_cHandles - 1)
        'pour chaque handle
        For X = 0 To m_cHandles - 1
            'on copie les informations sur le handle
            Handle = Marshal.PtrToStructure(New IntPtr(lpBufferHandles.ToInt32 + 4 + 16 * X), Handle.GetType)
            ' Only if handle belongs to specified process
            For Each __pid As Integer In PIDs
                If __pid = Handle.ProcessID Then
                    'on demande plus d'informations sur le handle de fichier
                    m_Files(X) = RetrieveObject(Handle)
                End If
            Next
        Next
        'on ferme le handle du dernier processus ouvert
        CloseProcessForHandle()

        Marshal.FreeHGlobal(lpBufferHandles)
    End Sub

    'ouvre un handle du processus ProcessID s'il n'est pas déjà ouvert
    Private Sub OpenProcessForHandle(ByVal ProcessID As Integer)
        's'il n'est pas ouvert
        If ProcessID <> lastPID Then
            'on ferme le précédent
            CloseHandle(hProcess)
            'on ouvre le processus demandé pour dupliquer des handles
            hProcess = OpenProcess(PROCESS_DUP_HANDLE, 0, ProcessID)
            'on stocke le PID du processus ouvert
            lastPID = ProcessID
        End If
    End Sub

    'ferme le handle du dernier processus ouvert
    Private Sub CloseProcessForHandle()
        'on le ferme
        CloseHandle(hProcess)
        'on l'indique
        hProcess = 0
        lastPID = 0
    End Sub

    'remplit un buffer avec les infos sur l'objet spécifié par l'index
    '==============================================================
    Private Function RetrieveObject(ByRef Handle As SYSTEM_HANDLE_INFORMATION) As HandleInfo
        Dim Length As Integer 'longueur
        Dim ret As Integer 'valeur de retour des fonctions utilisées
        Dim ret2 As Integer 'valeur de retour des fonctions utilisées
        Dim hHandle As Integer 'handle dupliqué dans notre processus
        'contient des infos sur un objet
        Dim ObjBasic As OBJECT_BASIC_INFORMATION
        Dim ObjType As OBJECT_TYPE_INFORMATION
        Dim ObjName As OBJECT_NAME_INFORMATION
        'renvoie le type d'un objet
        Dim m_ObjectTypeName As String
        'renvoie le nom d'un objet
        Dim m_ObjectName As String
        Dim BufferObjType As IntPtr
        Dim BufferObjName As IntPtr
        Dim BufferObjBasic As IntPtr
        Dim h As New HandleInfo

        'on ouvre un handle du processus propriétaire du handle si ce n'est pas déjà fait
        OpenProcessForHandle(Handle.ProcessID)

        ' on copie le handle recherché dans notre processus avec les mêmes droits d'accès
        DuplicateHandle(hProcess, Handle.Handle, GetCurrentProcess, hHandle, 0, 0, DUPLICATE_SAME_ACCESS)

        'si pas de handle
        If hHandle = 0 Then Return h

        'on fait de la place pour les informations de bases de l'objet
        BufferObjBasic = Marshal.AllocHGlobal(Marshal.SizeOf(ObjBasic))

        'renvoie des infos Basic sur l'objet
        ret2 = NtQueryObject(hHandle, ObjectBasicInformation, BufferObjBasic, Marshal.SizeOf(ObjBasic), ret)
        ObjBasic = Marshal.PtrToStructure(BufferObjBasic, ObjBasic.GetType)

        Marshal.FreeHGlobal(BufferObjBasic)

        'alloue un buffer à la taille des données Type
        BufferObjType = Marshal.AllocHGlobal(ObjBasic.TypeInformationLength + 2)

        'demande les infos Type de l'objet
        ret2 = NtQueryObject(hHandle, ObjectTypeInformation, BufferObjType, ObjBasic.TypeInformationLength + 2, ret)
        'copie le descripteur dans le pointeur de tableau
        ObjType = Marshal.PtrToStructure(BufferObjType, ObjType.GetType)

        'copie les données du nom dans le buffer alloué
        m_ObjectTypeName = Marshal.PtrToStringUni(ObjType.Name.Buffer)

        Marshal.FreeHGlobal(BufferObjType)

        'si la longueur du nom est 0
        If ObjBasic.NameInformationLength = 0 Then
            'on la met à MAX_PATH (en UNICODE = MAX_PATH * 2)
            'cela veut dire que le NtQueryObject ne sait la longueur de la chaine
            Length = 512
        Else
            'si on longueur est spécifié on l'utilise
            Length = ObjBasic.NameInformationLength + 2
        End If
        'on alloue le buffer pour le nom de l'object
        BufferObjName = Marshal.AllocCoTaskMem(Length)
        'Dim x As Integer
        'For x = 0 To Length - 1
        'Marshal.WriteByte(BufferObjName, x, CByte(0))
        'Next
        ZeroMemory(BufferObjName, Length)

        'on demande le nom de l'objet
        'si c'est un fichier
        If m_ObjectTypeName = "File" Then
            'on envoit notre requête au driver
            'seul le mode kernel permet d'accéder à la zone mémoire des objets 2K
            ret2 = DeviceIoControl(hDriver, IOCTL_KERNELMEMORY_GETOBJECTNAME, Handle, 16, BufferObjName, Length, ret, 0)
            'sinon
        Else
            'on demande le nom du handle directement
            ret2 = NtQueryObject(hHandle, ObjectNameInformation, BufferObjName, Length, ret)
        End If
        'on copie les descripteur dans le pointeur de tableau
        ObjName = Marshal.PtrToStructure(BufferObjName, ObjName.GetType)

        'convertit le nom en String VB
        'alloue un buffer chaine
        'copie les données UNICODE du nom dans le buffer chaine
        m_ObjectName = Marshal.PtrToStringUni(New IntPtr(BufferObjName.ToInt32 + 8))
        'End If

        Marshal.FreeCoTaskMem(BufferObjName)

        If m_ObjectTypeName = "File" Then
            'si c'est un fichier, on fournit le nom DOS
            m_ObjectName = GetDosFileName(m_ObjectName)
        ElseIf m_ObjectTypeName = "Key" Then
            'si c'est une clé de registre on fournit son nom classique
            m_ObjectName = GetKeyName(m_ObjectName)
        ElseIf m_ObjectTypeName = "Process" Then
            ' If it's a process, we retrieve processID from handle
            Dim i As Integer = GetProcessIdApi(hHandle)
            m_ObjectName = GetProcessNameFromPID(i) & " (" & CStr(i) & ")"
        ElseIf m_ObjectTypeName = "Thread" AndAlso _isVista Then
            ' Have to get thread ID, and then, Process ID
            ' These functions are only present in a VISTA OS
            Dim i As Integer = GetThreadId(hHandle)
            Dim i2 As Integer = GetProcessIdOfThread(hHandle)
            m_ObjectName = GetProcessNameFromPID(i2) & " (" & CStr(i2) & ")" & "  - " & CStr(i)
        End If

        ' on ferme la copie du handle recherché
        CloseHandle(hHandle)

        'on stocke les infos
        With RetrieveObject
            .Attributes = ObjBasic.Attributes
            .CreateTime = ObjBasic.CreateTime
            .Flags = Handle.Flags
            .GenericAll = ObjType.GenericMapping.GenericAll
            .GenericExecute = ObjType.GenericMapping.GenericExecute
            .GenericRead = ObjType.GenericMapping.GenericRead
            .GenericWrite = ObjType.GenericMapping.GenericWrite
            .GrantedAccess = Handle.GrantedAccess
            .Handle = Handle.Handle
            .HandleCount = ObjType.HandleCount
            .InvalidAttributes = ObjType.InvalidAttributes
            .MaintainHandleDatabase = ObjType.MaintainHandleDatabase
            .NameInformation = m_ObjectTypeName
            .NonPagedPoolUsage = ObjType.NonPagedPoolUsage
            .Object_Renamed = Handle.Object_Pointer
            .ObjectCount = ObjType.ObjectCount
            .ObjectName = m_ObjectName
            .PagedPoolUsage = ObjType.PagedPoolUsage
            .PeakHandleCount = ObjType.PeakHandleCount
            .PeakObjectCount = ObjType.PeakObjectCount
            .PointerCount = ObjBasic.PointerCount
            .PoolType = ObjType.PoolType
            .ProcessID = Handle.ProcessID
            .Unknown = ObjType.Unknown
            .ValidAccess = ObjType.ValidAccess
        End With
    End Function

    '==========================================================================================
    'informations sur les objets de l'espace de nom interne de Windows
    '==========================================================================================
    'indique si un tableau est vide
    Private Function IsTabNothing(ByRef t() As DirectoryEntry) As Boolean
        On Error GoTo Fin
        If UBound(t) >= 0 Then IsTabNothing = False

        Exit Function
Fin:
        IsTabNothing = True
    End Function

    'renvoie le nom DOS d'un nom de fichier interne à Windows
    '========================================================
    'strInternalFilename : nom interne d'un fichier
    'renvoie une chaine
    Public Function GetDosFileName(ByVal strInternalFilename As String) As String
        Dim strBuff As String 'buffer chaine pour les lettres de lecteur du système
        'Dim SymNames() As String
        'Dim ubSym As Integer 'liste de liens symboliques correspondant et taille de cette liste
        Dim X As Integer 'var de contrôle
        'Dim ObjName, objSymName As String 'nom de l'objet et nom du lien
        Dim strLetters() As String, strLetter As String
        Dim strInt As String, ret As Integer
        'si erreur
        On Error GoTo Fin
        'si pas de nom spécifié
        If Len(strInternalFilename) = 0 Then Return ""

        'on prépare le buffer
        strBuff = Space(255)
        'on demande la liste des lecteurs
        GetLogicalDriveStrings(255, strBuff)
        'la liste est séparée par des nul et terminée par deux nuls
        'on les remplace par des "|"
        strLetters = Split(strBuff, vbNullChar)

        For X = 0 To UBound(strLetters)
            strInt = New String(" ", 256)
            strLetter = Mid$(strLetters(X), 1, 2)
            ret = QueryDosDevice(strLetter, strInt, 256)
            strInt = Mid(strInt, 1, InStr(strInt, vbNullChar) - 1)
            If InStr(1, strInternalFilename, strInt, CompareMethod.Text) > 0 Then
                Return Replace(strInternalFilename, strInt & "\", strLetters(X))
            End If
        Next
Fin:
        Return strInternalFilename
    End Function

    'renvoie le nom de clé de registre à partir du nom interne
    '=========================================================
    'strInternalKey : nom interne de la clé
    'renvoie une chaine
    Public Function GetKeyName(ByVal strInternalKey As String) As String
        'HKEY_CURRENT_CONFIG
        strInternalKey = Replace(strInternalKey, "\REGISTRY\MACHINE\SYSTEM\CURRENTCONTROLSET\HARDWARE PROFILES\CURRENT", "HKCC")
        'HKEY_CLASSES_ROOT
        strInternalKey = Replace(strInternalKey, "\REGISTRY\MACHINE\SOFTWARE\CLASSES", "HKCR")
        'HKEY_USERS
        strInternalKey = Replace(strInternalKey, "\REGISTRY\USER\S", "HKU\S")
        'HKEY_LOCAL_MACHINE
        strInternalKey = Replace(strInternalKey, "\REGISTRY\MACHINE", "HKLM")
        'HKEY_CURRENT_USER
        strInternalKey = Replace(strInternalKey, "\REGISTRY\USER", "HKCU")
        'on renvoie
        GetKeyName = strInternalKey
    End Function

    '==========================================================================================
    'informations sur un processus
    '==========================================================================================
    'renvoie le nom du processus "ProcessID"
    '============================================
    'ProcessID : ID du processus dont on veut connaitre le nom d'exe (nom seulement)
    Public Function GetProcessNameFromPID(ByVal ProcessID As Integer) As String
        Return cProcess.GetProcessName(ProcessID)
    End Function

    'constructeur
    'UPGRADE_NOTE: Class_Initializea été mis à niveau vers Class_Initialize_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    Private Sub Class_Initialize_Renamed()
        Dim ret As Integer
        driver = New clsDriverCtrl
        'autorise le privilège Debug afin de pouvoir ouvrir des handles vers les processus systèmes
        'EnableDebug()
        'init des infos sur le driver
        With driver
            .ServiceDisplayName = "KernelMemory"
            .ServiceErrorType = 1
            .ServiceFileName = Application.StartupPath & "\KernelMemory.sys"
            .ServiceName = "KernelMemory"
            .ServiceStartType() = 3 'DEMAND
            .ServiceType = 1 'KERNEL DRIVER

            'enregistre le driver dans le registre si pas déjà enregistré
            ret = .InstallService()
            'If (ret) Then MsgBox(.FormatErrorMessage(ret))
            'démarre le driver
            ret = .StartService()
            'If (ret) Then MsgBox(.FormatErrorMessage(ret))
        End With
        'uvre un handle vers le driver
        hDriver = OpenDriver()
        'demande le numéro du type "File" pour les objets File de cette version système
        'cela permet d'être indépendant de la version de NT/2K/XP : numéros différents suivant la version
        m_ObjectTypeNumber = GetObjectTypeNumber("File")
    End Sub

    Public Sub New()
        MyBase.New()
        _isVista = Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major >= VISTA_MAJOR_VERSION
        Class_Initialize_Renamed()
    End Sub

    'destructeur
    'UPGRADE_NOTE: Class_Terminatea été mis à niveau vers Class_Terminate_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    Private Sub Class_Terminate_Renamed()
        'ferme un éventuel handle de processus ouvert
        CloseProcessForHandle()
        'ferme le handle du driver KernelMemory
        CloseHandle(hDriver)
        'arrête le driver KernelMemory
        Try
            driver.StopService()
            driver.RemoveService()
            driver = Nothing
        Catch ex As Exception
            '
        End Try
    End Sub

    Public Sub Close()
        Class_Terminate_Renamed()
    End Sub

    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub

    'renvoie le numéro de type d'objet pour un nom de type d'objet textuel
    '=====================================================================
    'strTypeName : nom du type d'objet
    'renvoie un long
    Private Function GetObjectTypeNumber(ByVal strTypeName As String) As Integer
        'buffer pour les données de nom de types d'objet, taille requise pour stocker les infos de type
        Dim buffObjTypes As IntPtr, cbReqLength As Integer
        'nombre de type dans la liste
        Dim cTypeCount As Integer
        'var de contrôle
        Dim X As Integer
        'pointeur vers la prochaine information de type
        Dim lpTypeInfo As IntPtr
        'informations sur un type
        Dim TypeInfo As OBJECT_TYPE_INFORMATION
        'nom d'un type
        Dim strType As String

        If IsWindowsVista() Then
            Dim b As IntPtr = Marshal.AllocHGlobal(4)
            'on demande la taille requise pour la liste des infos de type
            NtQueryObject(0&, ObjectAllTypesInformation, b, 4&, cbReqLength)
            Marshal.FreeHGlobal(b)
        Else
            'on demande la taille requise pour la liste des infos de type
            NtQueryObject(0&, ObjectAllTypesInformation, IntPtr.Zero, 0&, cbReqLength)
        End If

        'on prépare le buffer
        buffObjTypes = Marshal.AllocHGlobal(cbReqLength)

        'on demande la liste des informations de type
        NtQueryObject(0, ObjectAllTypesInformation, buffObjTypes, cbReqLength, cbReqLength)

        'on copie le nombre d'informations de type contenu dans la liste : le premier double mot
        cTypeCount = Marshal.ReadInt32(buffObjTypes, 0)

        'on fait pointer la prochaine information de type sur le début de la liste
        lpTypeInfo = New IntPtr(buffObjTypes.ToInt32 + 4)
        'pour chaque info de type
        For X = 0 To cTypeCount - 1
            'on copie les informations sur le type
            TypeInfo = Marshal.PtrToStructure(lpTypeInfo, TypeInfo.GetType)
            'on fait de la place pour le nom du type (en UNICODE)
            'strType = Space(TypeInfo.Name.Length / 2)
            'on copie la chaine UNICODE du nom de type
            strType = Marshal.PtrToStringUni(TypeInfo.Name.Buffer, TypeInfo.Name.Length \ 2)
            'si elle correspond au nom recherché
            If strTypeName = strType Then
                'le numéro de type d'objet est l'index dans la liste de type + 1
                GetObjectTypeNumber = X + 1
                'on a fini
                Exit Function
            End If
            'sinon, on fait pointer la prochaine information de type
            'à un décalage de la taille des informations + la taille du nom de type en UNICODE
            lpTypeInfo = New IntPtr(lpTypeInfo.ToInt32 + 96 + TypeInfo.Name.MaximumLength)
            'et on l'aligne sur le prochain dword
            lpTypeInfo = New IntPtr(lpTypeInfo.ToInt32 + (lpTypeInfo.ToInt32 Mod 4))
        Next
        Marshal.FreeHGlobal(buffObjTypes)

        'si windows 2000 alors bug : NtQueryObject ne renvoie que 23 types au lieu de 26
        If strTypeName = "Driver" Then
            Return 24
        ElseIf strTypeName = "IoCompletion" Then
            Return 25
        ElseIf strTypeName = "File" Then
            Return 26
        End If

        'si on arrive ici, c'est que le type n'a pas été trouvé, alors 0
        Return 0
    End Function

    'autorise le privilège SeDebugPrivilege
    Shared Sub EnableDebug()
        Dim hProc As Integer 'handle du processus actuel
        Dim hToken As Integer 'handle du token
        Dim mLUID As LUID 'LUID du privilège
        Dim mPriv As TOKEN_PRIVILEGES 'privilèges
        Dim mNewPriv As TOKEN_PRIVILEGES 'nouveaux privilèges
        'renvoie le processus actuel
        hProc = GetCurrentProcess()
        'ouvre le token
        OpenProcessToken(hProc, TOKEN_ADJUST_PRIVILEGES + TOKEN_QUERY, hToken)
        'regarde la valeur du privilège
        LookupPrivilegeValue("", "SeDebugPrivilege", mLUID)
        'nombre de privilèges
        mPriv.PrivilegeCount = 1
        'attribut ENABLED
        mPriv.Privileges.Attributes = SE_PRIVILEGE_ENABLED
        'LUID de privilège
        mPriv.Privileges.pLuid = mLUID
        'ajuste le token
        AdjustTokenPrivileges(hToken, False, mPriv, 4 + (12 * mPriv.PrivilegeCount), mNewPriv, 4 + (12 * mNewPriv.PrivilegeCount))
        'ferme le handle de token
        CloseHandle(hToken)
    End Sub

    'indique s'il s'agit de Vista
    Private Function IsWindowsVista() As Boolean
        Return ((Environment.OSVersion.Platform = PlatformID.Win32NT) And (Environment.OSVersion.Version.Major = 6) And (Environment.OSVersion.Version.Minor = 0))
    End Function

    'permet de fermer un handle hHandle dans le processus dwProcessID
    Public Function CloseProcessLocalHandle(ByVal dwProcessID As Integer, ByVal hHandle As Integer) As Integer
        Dim hMod As Integer
        Dim lpProc As Integer
        Dim hThread As Integer
        Dim hProcess As Integer

        hMod = GetModuleHandle("kernel32.dll")
        lpProc = GetProcAddress(hMod, "CloseHandle")
        hProcess = OpenProcess(PROCESS_CREATE_THREAD Or PROCESS_VM_OPERATION Or PROCESS_VM_WRITE Or PROCESS_VM_READ, 0, dwProcessID)
        If hProcess Then
            hThread = CreateRemoteThread(hProcess, 0, 0, lpProc, hHandle, 0, 0)
            If hThread Then
                WaitForSingleObject(hThread, INFINITE)
                GetExitCodeThread(hThread, CloseProcessLocalHandle)
                CloseHandle(hThread)
            End If
            CloseHandle(hProcess)
        End If
    End Function
End Class