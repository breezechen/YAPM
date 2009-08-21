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
Imports System.Runtime.InteropServices

Public Class clsOpenedHandles
    'dérive du contrôle de driver pour avoir le total des appels à StartService et StopService
    'Inherits clsDriverCtrl
    'module ermettant d'obtenir une liste des handles de tous les processus du systeme

    '==========================================================================================
    'constantes
    '==========================================================================================

    Private Const STATUS_INFO_LENGTH_MISMATCH As UInteger = &HC0000004
    Private Const STATUS_SUCCESS As Integer = &H0

    'constantes d'erreur
    Private Const STATUS_NO_MORE_ENTRIES As Integer = &H8000001A
    Private Const STATUS_BUFFER_TOO_SMALL As Integer = &HC0000023

    'structure contenant des infos sur un handle d'un processus
    Private Structure HandleInfo
        Dim ProcessID As Integer 'ID du processus propriétaire
        Dim Flags As NativeEnums.HandleFlags  ' 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
        Dim Handle As IntPtr  'valeur du handle
        Dim ObjectName As String 'nom de l'objet
        Dim NameInformation As String 'type de l'ojet
        'UPGRADE_NOTE: Objecta été mis à niveau vers Object_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
        Dim Object_Renamed As IntPtr  'adresse de l'objet
        Dim GrantedAccess As Native.Security.StandardRights  'accès autorisés à l'objet
        Dim Attributes As UInteger 'attributs
        Dim HandleCount As Integer 'nombre de handle de ce type dans le système
        Dim PointerCount As UInteger 'nombre de références pointeurs à cet objet dans le système
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
        Dim MaintainHandleDatabase As UShort  '
        Dim PoolType As NativeEnums.PoolType  'type de pool utilisé par l'objet
        Dim PagedPoolUsage As Integer 'paged pool utilisé
        Dim NonPagedPoolUsage As Integer 'non-paged pool utilisé
    End Structure


    'handle du processus actuellement ouvert
    Dim hProcess As IntPtr
    'PID du processus actuellement ouvert
    Dim lastPID As Integer

    'listes des fichiers ouverts du système
    Dim m_Files() As HandleInfo
    'nombres de fichiers ouverts dans le système
    Dim m_cHandles As Integer

    'handle vers le driver
    Dim hDriver As IntPtr
    Dim driver As Native.Driver.DriverCtrl

    'numéro de type des objets de type "File"
    Dim m_ObjectTypeNumber As Long


    '==========================================================================================
    'pour envoyer une requête au driver
    '==========================================================================================

    'permet d'envoyer des requêtes à un pilote
    Private Declare Function DeviceIoControl Lib "kernel32.dll" (ByVal hDevice As IntPtr, ByVal dwIoControlCode As Integer, ByRef lpInBuffer As NativeStructs.SystemHandleInformation, ByVal nInBufferSize As Integer, ByVal lpOutBuffer As IntPtr, ByVal nOutBufferSize As Integer, ByRef lpBytesReturned As Integer, ByVal lpOverlapped As Integer) As Integer


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
    Public Function GetHandle(ByVal dwIndex As Integer) As IntPtr  'valeur du handle
        Return m_Files(dwIndex).Handle
    End Function
    Public Function GetObject_Renamed(ByVal dwIndex As Integer) As IntPtr  'adresse de l'objet
        Return m_Files(dwIndex).Object_Renamed
    End Function
    Public Function GetObjectCount(ByVal dwIndex As Integer) As Integer 'nombre d'objet
        Return m_Files(dwIndex).ObjectCount
    End Function
    Public Function GetGrantedAccess(ByVal dwIndex As Integer) As Native.Security.StandardRights  'accès autorisés à l'objet
        Return m_Files(dwIndex).GrantedAccess
    End Function
    Public Function GetAttributes(ByVal dwIndex As Integer) As UInteger 'attributs
        Return m_Files(dwIndex).Attributes
    End Function
    Public Function GetHandleCount(ByVal dwIndex As Integer) As Integer 'nombre de handle de ce type dans le système
        Return m_Files(dwIndex).HandleCount
    End Function
    Public Function GetPointerCount(ByVal dwIndex As Integer) As UInteger 'nombre de références pointeurs à cet objet dans le système
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
    Private memAllocPID As New Native.Memory.MemoryAlloc(&H100)
    Private Sub CreateQueryHandlesBuffer(Optional ByVal oneProcessId As Integer = -1)
        Dim Length As Integer 'longueur du buffer
        Dim X As Integer 'compteur
        Dim ret As Integer 'valeur de retour des fonctions utilisées
        Dim Handle As NativeStructs.SystemHandleInformation  'un handle

        Length = memAllocPIDs.Size  'longueur minimale du buffer
        'tant que la longueur n'est pas suffisante
        Do While NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, _
                            memAllocPIDs.Pointer, memAllocPIDs.Size, ret) = STATUS_INFO_LENGTH_MISMATCH
            'on multiplie la taille du buffer par 2
            Length = Length * 2
            'on réalloue le buffer
            memAllocPIDs.Resize(Length)
        Loop

        'demande le nombre de handles
        m_cHandles = memAllocPIDs.ReadInt32(0)

        'on fait de la place pour un handle de fichier
        ReDim m_Files(m_cHandles - 1)
        'pour chaque handle
        For X = 0 To m_cHandles - 1
            ' &h4 because of HandleCount on 4 first bytes
            Handle = memAllocPIDs.ReadStruct(Of NativeStructs.SystemHandleInformation)(&H4, X)
            ' Only if handle belongs to specified process
            If oneProcessId = -1 OrElse oneProcessId = Handle.ProcessID Then
                'on demande plus d'informations sur le handle de fichier
                m_Files(X) = RetrieveObject(Handle)
            End If
        Next
        'on ferme le handle du dernier processus ouvert
        CloseProcessForHandle()

    End Sub
    Private memAllocPIDs As New Native.Memory.MemoryAlloc(&H100)
    Private Sub CreateQueryHandlesBuffer(ByVal PIDs As Integer())
        Dim Length As Integer 'longueur du buffer
        Dim X As Integer 'compteur
        Dim ret As Integer 'valeur de retour des fonctions utilisées
        Dim Handle As NativeStructs.SystemHandleInformation  'un handle

        Length = memAllocPIDs.Size  'longueur minimale du buffer
        'tant que la longueur n'est pas suffisante
        Do While NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, _
                            memAllocPIDs.Pointer, memAllocPIDs.Size, ret) = STATUS_INFO_LENGTH_MISMATCH
            'on multiplie la taille du buffer par 2
            Length = Length * 2
            'on réalloue le buffer
            memAllocPIDs.Resize(Length)
        Loop

        'demande le nombre de handles
        m_cHandles = memAllocPIDs.ReadInt32(0)

        'on fait de la place pour un handle de fichier
        ReDim m_Files(m_cHandles - 1)
        'pour chaque handle
        For X = 0 To m_cHandles - 1
            ' &h4 because of HandleCount on 4 first bytes
            Handle = memAllocPIDs.ReadStruct(Of NativeStructs.SystemHandleInformation)(&H4, X)
            ' Only if handle belongs to specified process
            For Each __pid As Integer In PIDs
                If __pid = Handle.ProcessId Then
                    'on demande plus d'informations sur le handle de fichier
                    m_Files(X) = RetrieveObject(Handle)
                End If
            Next
        Next
        'on ferme le handle du dernier processus ouvert
        CloseProcessForHandle()

    End Sub

    'ouvre un handle du processus ProcessID s'il n'est pas déjà ouvert
    Private Sub OpenProcessForHandle(ByVal ProcessID As Integer)
        's'il n'est pas ouvert
        If ProcessID <> lastPID Then
            'on ferme le précédent
            NativeFunctions.CloseHandle(hProcess)
            'on ouvre le processus demandé pour dupliquer des handles
            hProcess = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.DupHandle, _
                                                   False, ProcessID)
            'on stocke le PID du processus ouvert
            lastPID = ProcessID
        End If
    End Sub

    'ferme le handle du dernier processus ouvert
    Private Sub CloseProcessForHandle()
        'on le ferme
        NativeFunctions.CloseHandle(hProcess)
        'on l'indique
        hProcess = IntPtr.Zero
        lastPID = 0
    End Sub

    'remplit un buffer avec les infos sur l'objet spécifié par l'index
    '==============================================================
    Private Function RetrieveObject(ByRef Handle As  _
                                    NativeStructs.SystemHandleInformation) As HandleInfo
        Dim Length As UInteger   'longueur
        Dim ret As Integer 'valeur de retour des fonctions utilisées
        Dim ret2 As UInteger 'valeur de retour des fonctions utilisées
        Dim hHandle As IntPtr  'handle dupliqué dans notre processus
        'contient des infos sur un objet
        Dim ObjBasic As NativeStructs.ObjectBasicInformation
        Dim ObjType As NativeStructs.ObjectTypeInformation
        Dim ObjName As NativeStructs.ObjectNameInformation
        'renvoie le type d'un objet
        Dim m_ObjectTypeName As String
        'renvoie le nom d'un objet
        Dim m_ObjectName As String
        Dim BufferObjType As IntPtr
        Dim BufferObjName As IntPtr
        Dim BufferObjBasic As IntPtr
        Dim h As New HandleInfo

        'on ouvre un handle du processus propriétaire du handle si ce n'est pas déjà fait
        OpenProcessForHandle(Handle.ProcessId)

        ' on copie le handle recherché dans notre processus avec les mêmes droits d'accès
        NativeFunctions.DuplicateHandle(hProcess, New IntPtr(Handle.Handle), New IntPtr(NativeFunctions.GetCurrentProcess), hHandle, 0, False, NativeEnums.DuplicateOptions.SameAccess)

        'si pas de handle
        If hHandle.IsNull Then
            Return h
        End If

        'on fait de la place pour les informations de bases de l'objet
        BufferObjBasic = Marshal.AllocHGlobal(Marshal.SizeOf(ObjBasic))

        'renvoie des infos Basic sur l'objet
        ret2 = NativeFunctions.NtQueryObject(hHandle, NativeEnums.ObjectInformationClass.ObjectAttributes, BufferObjBasic, Marshal.SizeOf(ObjBasic), ret)
        ObjBasic = CType(Marshal.PtrToStructure(BufferObjBasic, ObjBasic.GetType), NativeStructs.ObjectBasicInformation)

        Marshal.FreeHGlobal(BufferObjBasic)

        'alloue un buffer à la taille des données Type
        BufferObjType = Marshal.AllocHGlobal(New IntPtr(ObjBasic.TypeInformationLength + 2))

        'demande les infos Type de l'objet
        ret2 = NativeFunctions.NtQueryObject(hHandle, NativeEnums.ObjectInformationClass.ObjectTypeInformation, BufferObjType, CInt(ObjBasic.TypeInformationLength + 2), ret)
        'copie le descripteur dans le pointeur de tableau
        ObjType = CType(Marshal.PtrToStructure(BufferObjType, ObjType.GetType), NativeStructs.ObjectTypeInformation)

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
            Length = CUInt(ObjBasic.NameInformationLength + 2)
        End If
        'on alloue le buffer pour le nom de l'object
        BufferObjName = Marshal.AllocCoTaskMem(CInt(Length))
        'Dim x As Integer
        'For x = 0 To Length - 1
        'Marshal.WriteByte(BufferObjName, x, CByte(0))
        'Next
        NativeFunctions.ZeroMemory(BufferObjName, New IntPtr(Length))

        'on demande le nom de l'objet
        'si c'est un fichier
        If m_ObjectTypeName = "File" Then
            'on envoit notre requête au driver
            'seul le mode kernel permet d'accéder à la zone mémoire des objets 2K
            ret = DeviceIoControl(hDriver, IOCTL_KERNELMEMORY_GETOBJECTNAME, Handle, 16, BufferObjName, CInt(Length), ret, 0)
            'sinon
        Else
            'on demande le nom du handle directement
            ret2 = NativeFunctions.NtQueryObject(hHandle, NativeEnums.ObjectInformationClass.ObjectNameInformation, BufferObjName, CInt(Length), ret)
        End If
        'on copie les descripteur dans le pointeur de tableau
        ObjName = CType(Marshal.PtrToStructure(BufferObjName, ObjName.GetType), NativeStructs.ObjectNameInformation)

        'convertit le nom en String VB
        'alloue un buffer chaine
        'copie les données UNICODE du nom dans le buffer chaine
        m_ObjectName = Marshal.PtrToStringUni(New IntPtr(BufferObjName.ToInt32 + 8))
        'End If

        Marshal.FreeCoTaskMem(BufferObjName)

        If m_ObjectTypeName = "File" Then
            'si c'est un fichier, on fournit le nom DOS
            m_ObjectName = Common.Misc.DeviceDriveNameToDosDriveName(m_ObjectName)
        ElseIf m_ObjectTypeName = "Key" Then
            'si c'est une clé de registre on fournit son nom classique
            m_ObjectName = GetKeyName(m_ObjectName)
        ElseIf m_ObjectTypeName = "Process" Then
            ' If it's a process, we retrieve processID from handle
            Dim i As Integer = NativeFunctions.GetProcessId(hHandle)
            m_ObjectName = GetProcessNameFromPID(i) & " (" & CStr(i) & ")"
        ElseIf m_ObjectTypeName = "Thread" AndAlso cEnvironment.IsWindowsVistaOrAbove Then
            ' Have to get thread ID, and then, Process ID
            ' These functions are only present in a VISTA OS
            Dim i As Integer = NativeFunctions.GetThreadId(hHandle)
            Dim i2 As Integer = NativeFunctions.GetProcessIdOfThread(hHandle)
            m_ObjectName = GetProcessNameFromPID(i2) & " (" & CStr(i2) & ")" & "  - " & CStr(i)
        End If

        ' on ferme la copie du handle recherché
        NativeFunctions.CloseHandle(hHandle)

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
            .Handle = New IntPtr(Handle.Handle)
            .HandleCount = ObjType.TotalNumberOfHandles
            .InvalidAttributes = ObjType.InvalidAttributes
            .MaintainHandleDatabase = ObjType.MaintainTypeList
            .NameInformation = m_ObjectTypeName
            .NonPagedPoolUsage = ObjType.NonPagedPoolUsage
            .Object_Renamed = Handle.Object
            .ObjectCount = ObjType.TotalNumberOfObjects
            .ObjectName = m_ObjectName
            .PagedPoolUsage = ObjType.PagedPoolUsage
            .PeakHandleCount = ObjType.HighWaterNumberOfHandles
            .PeakObjectCount = ObjType.HighWaterNumberOfObjects
            .PointerCount = ObjBasic.PointerCount
            .PoolType = ObjType.PoolType
            .ProcessID = Handle.ProcessId
            .Unknown = ObjType.MaintainHandleCount
            .ValidAccess = ObjType.ValidAccess
        End With
    End Function

    '==========================================================================================
    'informations sur les objets de l'espace de nom interne de Windows
    '==========================================================================================

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
        Dim ret As Boolean
        driver = New Native.Driver.DriverCtrl
        'autorise le privilège Debug afin de pouvoir ouvrir des handles vers les processus systèmes
        'EnableDebug()
        'init des infos sur le driver
        With driver
            .ServiceDisplayName = "KernelMemory"
            .ServiceErrorType = NativeEnums.ServiceErrorControl.Normal
            .ServiceFileName = My.Application.Info.DirectoryPath & "\KernelMemory.sys"
            .ServiceName = "KernelMemory"
            .ServiceStartType() = NativeEnums.ServiceStartType.DemandStart
            .ServiceType = NativeEnums.ServiceType.KernelDriver

            'enregistre le driver dans le registre si pas déjà enregistré
            ret = .InstallService()
            'If (ret) Then MsgBox(.FormatErrorMessage(ret))
            'démarre le driver
            ret = .StartService()
            'If (ret) Then MsgBox(.FormatErrorMessage(ret))
            'uvre un handle vers le driver
            hDriver = .OpenDriver
        End With
        'demande le numéro du type "File" pour les objets File de cette version système
        'cela permet d'être indépendant de la version de NT/2K/XP : numéros différents suivant la version
        m_ObjectTypeNumber = GetObjectTypeNumber("File")
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    'destructeur
    'UPGRADE_NOTE: Class_Terminatea été mis à niveau vers Class_Terminate_Renamed. Cliquez ici : 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    Private Sub Class_Terminate_Renamed()
        'ferme un éventuel handle de processus ouvert
        CloseProcessForHandle()
        'ferme le handle du driver KernelMemory
        NativeFunctions.CloseHandle(hDriver)
        'arrête le driver KernelMemory
        Try
            If driver IsNot Nothing Then
                driver.StopService()
                driver.RemoveService()
                driver = Nothing
            End If
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
        Dim TypeInfo As NativeStructs.ObjectTypeInformation
        'nom d'un type
        Dim strType As String

        'is vista ??
        If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major >= 6 Then
            Dim b As IntPtr = Marshal.AllocHGlobal(4)
            'on demande la taille requise pour la liste des infos de type
            NativeFunctions.NtQueryObject(IntPtr.Zero, NativeEnums.ObjectInformationClass.ObjectTypesInformation, b, 4&, cbReqLength)
            Marshal.FreeHGlobal(b)
        Else
            'on demande la taille requise pour la liste des infos de type
            NativeFunctions.NtQueryObject(IntPtr.Zero, NativeEnums.ObjectInformationClass.ObjectTypesInformation, IntPtr.Zero, 0&, cbReqLength)
        End If

        'on prépare le buffer
        buffObjTypes = Marshal.AllocHGlobal(cbReqLength)

        'on demande la liste des informations de type
        NativeFunctions.NtQueryObject(IntPtr.Zero, NativeEnums.ObjectInformationClass.ObjectTypesInformation, buffObjTypes, cbReqLength, cbReqLength)

        'on copie le nombre d'informations de type contenu dans la liste : le premier double mot
        cTypeCount = Marshal.ReadInt32(buffObjTypes, 0)

        'on fait pointer la prochaine information de type sur le début de la liste
        lpTypeInfo = New IntPtr(buffObjTypes.ToInt32 + 4)
        'pour chaque info de type
        For X = 0 To cTypeCount - 1
            'on copie les informations sur le type
            TypeInfo = CType(Marshal.PtrToStructure(lpTypeInfo, TypeInfo.GetType), NativeStructs.ObjectTypeInformation)
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

    'permet de fermer un handle hHandle dans le processus dwProcessID
    Public Function CloseProcessLocalHandle(ByVal dwProcessID As Integer, ByVal hHandle As IntPtr) As Integer
        Dim hMod As IntPtr
        Dim lpProc As IntPtr
        Dim hThread As IntPtr
        Dim hProcess As IntPtr
        Dim res As Integer

        hMod = NativeFunctions.GetModuleHandle("kernel32.dll")
        lpProc = NativeFunctions.GetProcAddress(hMod, "CloseHandle")
        hProcess = NativeFunctions.OpenProcess(Native.Security.ProcessAccess.CreateThread Or _
                                            Native.Security.ProcessAccess.VmOperation Or _
                                            Native.Security.ProcessAccess.VmWrite Or _
                                            Native.Security.ProcessAccess.VmRead, False, _
                                            dwProcessID)
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
End Class