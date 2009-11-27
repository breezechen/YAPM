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

    Public Class Service


        ' ========================================
        ' Private constants
        ' ========================================

        Private Const ServicePathInRegistry As String = _
                            "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\"



        ' ========================================
        ' Private attributes
        ' ========================================

        ' Store new services ( serviceName <-> isNew )
        Private Shared _dicoNewServices As New Dictionary(Of String, Boolean)

        ' Current services running (updated after each enumeration)
        Private Shared _currentServices As Dictionary(Of String, cService)

        ' Used for memory operations
        Private Shared _memBufferEnumServics As New Native.Memory.MemoryAlloc(&H1000)   ' NOTE : never unallocated

        ' Used to protect currentServices dico
        Private Shared _semCurrentServ As New System.Threading.Semaphore(1, 1)




        ' ========================================
        ' Public properties
        ' ========================================

        ''' <summary>
        ' List of current services
        ' Needs to be protected by SemCurrentServices
        ''' </summary>
        Public Shared Property CurrentServices() As Dictionary(Of String, cService)
            Get
                Return _currentServices
            End Get
            Set(ByVal value As Dictionary(Of String, cService))
                _currentServices = value
            End Set
        End Property

        ' Return semaphore used to protect list of current services
        Public Shared ReadOnly Property SemCurrentServices() As System.Threading.Semaphore
            Get
                Return _semCurrentServ
            End Get
        End Property        



        ' ========================================
        ' Public functions
        ' ========================================

        ' Create a service
        Public Shared Function CreateService(ByVal params As Native.Api.Structs.ServiceCreationParams) As Boolean
            Dim ret As Boolean = False
            Dim hServ As IntPtr = CreateService(params, ret)
            If hServ.IsNotNull Then
                Native.Api.NativeFunctions.CloseServiceHandle(hServ)
            End If
            Return ret
        End Function
        Public Shared Function CreateService(ByVal params As Native.Api.Structs.ServiceCreationParams, _
                            ByRef res As Boolean) As IntPtr

            res = False

            If String.IsNullOrEmpty(params.RegMachine) = False Then
                ' Have to connect to the remote machine
                Dim b As Boolean = _
                    Native.Objects.Network.ConnectToRemoteMachine(params.RegMachine, _
                                                                  params.RegUser, _
                                                                  params.RegPassword)
                If b = False Then
                    ' Could not connect to remote machine !
                    Return IntPtr.Zero
                Else
                    ' Then copy file to remote machine...
                    b = Network.SyncCopyFileToRemoteSystem32(params.RegMachine, _
                                    params.FilePath, _
                                    Common.Misc.GetFileName(params.FilePath))
                    If b = False Then
                        ' Could not copy file
                        If String.IsNullOrEmpty(params.RegMachine) = False Then
                            Network.DisconnectFromRemoteMachine(params.RegMachine)
                        End If
                        Return IntPtr.Zero
                    End If

                    ' Now that the file is copied to the remote machine, we update
                    ' the executable path
                    params.FilePath = "\\" & params.RegMachine & _
                                "\ADMIN$\System32\" & _
                                Common.Misc.GetFileName(params.FilePath)
                End If

            End If

            Dim hSCM As IntPtr = _
                    GetSCManagerHandle(Security.ServiceManagerAccess.CreateService, _
                                                    params.RegMachine)

            If hSCM.IsNotNull Then
                Dim hServ As IntPtr = _
                        Native.Api.NativeFunctions.CreateService(hSCM, _
                                        params.ServiceName, _
                                        params.DisplayName, _
                                        Security.ServiceAccess.All, _
                                        params.Type, _
                                        params.StartType, _
                                        params.ErrorControl, _
                                        params.FilePath & " " & params.Arguments, _
                                        Nothing, _
                                        IntPtr.Zero, _
                                        IntPtr.Zero, _
                                        Nothing, _
                                        Nothing)

                CloseSCManagerHandle(hSCM)
                res = (hServ <> IntPtr.Zero)
                If String.IsNullOrEmpty(params.RegMachine) = False Then
                    Network.DisconnectFromRemoteMachine(params.RegMachine)
                End If
                Return hServ
            Else
                If String.IsNullOrEmpty(params.RegMachine) = False Then
                    Network.DisconnectFromRemoteMachine(params.RegMachine)
                End If
                Return IntPtr.Zero
            End If

        End Function


        ' Clear list of new services
        Public Shared Sub ClearNewServicesList()
            _dicoNewServices.Clear()
        End Sub


        ' Enumerate services (local)
        Public Shared Sub EnumerateServices(ByVal hSCM As IntPtr, _
                                       ByRef _dico As Dictionary(Of String, serviceInfos), _
                                       ByVal forAllProcesses As Boolean, _
                                       ByVal completeInfos As Boolean, _
                                       Optional ByVal processId As Integer = 0)

            Dim lBytesNeeded As Integer
            Dim lServicesReturned As Integer
            Dim tServiceStatus() As NativeStructs.EnumServiceStatusProcess
            ReDim tServiceStatus(0)

            If hSCM.IsNotNull Then
                '2nd arg : Api.SC_ENUM_PROCESS_INFO, _
                If Not (NativeFunctions.EnumServicesStatusEx(hSCM, _
                                            IntPtr.Zero, _
                                            NativeEnums.ServiceQueryType.All, _
                                            NativeEnums.ServiceQueryState.All, _
                                            _memBufferEnumServics.Pointer, _
                                            _memBufferEnumServics.Size, _
                                            lBytesNeeded, _
                                            lServicesReturned, _
                                            0, _
                                            Nothing)) Then
                    ' Resize buffer
                    _memBufferEnumServics.IncrementSize(lBytesNeeded)
                End If

                '2nd arg : Api.SC_ENUM_PROCESS_INFO, 
                If NativeFunctions.EnumServicesStatusEx(hSCM, _
                                            IntPtr.Zero, _
                                            NativeEnums.ServiceQueryType.All, _
                                            NativeEnums.ServiceQueryState.All, _
                                            _memBufferEnumServics.Pointer, _
                                            _memBufferEnumServics.Size, _
                                            lBytesNeeded, _
                                            lServicesReturned, _
                                            0, _
                                            Nothing) Then

                    For idx As Integer = 0 To lServicesReturned - 1

                        ' Get structure from memory
                        Dim obj As NativeStructs.EnumServiceStatusProcess = _
                                _memBufferEnumServics.ReadStruct(Of NativeStructs.EnumServiceStatusProcess)(idx)

                        If forAllProcesses OrElse processId = obj.ServiceStatusProcess.ProcessID Then
                            Dim _servINFO As New serviceInfos(obj, completeInfos)

                            If forAllProcesses = False OrElse _dicoNewServices.ContainsKey(obj.ServiceName) = False Or completeInfos Then

                                ' Get infos from registry
                                GetServiceInformationsFromRegistry(obj.ServiceName, _servINFO)

                                'PERFISSUE
                                GetServiceConfigByName(hSCM, obj.ServiceName, _servINFO, True)

                                If forAllProcesses And completeInfos = False Then
                                    _dicoNewServices.Add(obj.ServiceName, False)
                                End If
                            End If

                            _dico.Add(obj.ServiceName, _servINFO)
                        End If
                        If forAllProcesses Then
                            _dicoNewServices(obj.ServiceName) = True
                        End If
                    Next idx

                End If
            End If


            ' Remove all services that not exist anymore
            If forAllProcesses Then
                Dim _dicoTemp As Dictionary(Of String, Boolean) = _dicoNewServices
                For Each it As System.Collections.Generic.KeyValuePair(Of String, Boolean) In _dicoTemp
                    If it.Value = False Then
                        _dicoNewServices.Remove(it.Key)
                    End If
                Next
            End If

            ' Here we fill _currentServices if necessary
            'PERFISSUE
            _semCurrentServ.WaitOne()
            If _currentServices Is Nothing Then
                _currentServices = New Dictionary(Of String, cService)
            End If
            For Each pc As serviceInfos In _dico.Values
                If _currentServices.ContainsKey(pc.Name) = False Then
                    _currentServices.Add(pc.Name, New cService(pc))
                End If
            Next
            _semCurrentServ.Release()
        End Sub


        ' Get config of service
        Public Shared Sub GetServiceConfigByName(ByVal hSCManager As IntPtr, _
                                                 ByVal name As String, _
                                                 ByRef _infos As serviceInfos, _
                                                 ByVal getFileInfo As Boolean)

            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, _
                                                    Security.ServiceAccess.QueryConfig)

            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then

                    ' Get all available informations
                    Dim tt As New NativeStructs.QueryServiceConfig
                    Dim bytesNeeded As Integer = 0
                    Dim fResult As Boolean

                    Dim pt As IntPtr = IntPtr.Zero
                    fResult = NativeFunctions.QueryServiceConfig(lServ, pt, bytesNeeded, _
                                                                 bytesNeeded)
                    pt = Marshal.AllocHGlobal(bytesNeeded)
                    fResult = NativeFunctions.QueryServiceConfig(lServ, pt, bytesNeeded, _
                                                                 bytesNeeded)
                    ' Cast into NativeStructs.QueryServiceConfig unmanaged memory
                    tt = CType(Marshal.PtrToStructure(pt, _
                                        GetType(NativeStructs.QueryServiceConfig)),  _
                                        NativeStructs.QueryServiceConfig)
                    Marshal.FreeHGlobal(pt)

                    ' Set configuration
                    _infos.SetConfig(tt)

                    NativeFunctions.CloseServiceHandle(lServ)
                End If
            End If
        End Sub


        ' Get a service by its name
        Public Shared Function GetServiceByName(ByVal name As String) As cService

            Dim tt As cService = Nothing
            _semCurrentServ.WaitOne()
            If _currentServices.ContainsKey(name) Then
                tt = _currentServices.Item(name)
            End If
            _semCurrentServ.Release()

            Return tt

        End Function

        ' Get services which depends from a specific service
        Public Shared Function GetServiceWhichDependFromByServiceName(ByVal serviceName As String) As Dictionary(Of String, serviceInfos)

            Dim _d As New Dictionary(Of String, serviceInfos)
            Dim dep() As String = Nothing

            _semCurrentServ.WaitOne()

            For Each serv As cService In _currentServices.Values
                dep = serv.Infos.Dependencies
                If dep IsNot Nothing Then
                    For Each s As String In dep
                        If s.ToLowerInvariant = serviceName.ToLowerInvariant Then
                            _d.Add(serv.Infos.Name, serv.Infos)
                            Exit For
                        End If
                    Next
                End If
            Next

            _semCurrentServ.Release()

            Return _d
        End Function

        ' Get dependencies of a service
        Public Shared Function GetServiceDependencies(ByVal serviceName As String) As Dictionary(Of String, serviceInfos)

            Dim _d As New Dictionary(Of String, serviceInfos)
            Dim dep() As String = Nothing

            _semCurrentServ.WaitOne()

            For Each serv As cService In _currentServices.Values
                If serv.Infos.Name.ToLowerInvariant = serviceName.ToLowerInvariant Then
                    dep = serv.Infos.Dependencies
                    Exit For
                End If
            Next

            If dep Is Nothing OrElse dep.Length = 0 Then
                _semCurrentServ.Release()
                Return _d
            End If

            For Each servName As String In dep
                For Each serv As cService In _currentServices.Values
                    If servName.ToLowerInvariant = serv.Infos.Name.ToLowerInvariant Then
                        _d.Add(servName, serv.Infos)
                        Exit For
                    End If
                Next
            Next

            _semCurrentServ.Release()

            Return _d
        End Function

        ' Pause a service
        Public Shared Function PauseServiceByName(ByVal name As String, ByVal hSCManager As IntPtr) As Boolean
            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, Native.Security.ServiceAccess.PauseContinue)
            Dim res As Boolean = False
            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then
                    Dim lpss As NativeStructs.ServiceStatusProcess
                    res = NativeFunctions.ControlService(lServ, NativeEnums.ServiceControl.Pause, lpss)
                    NativeFunctions.CloseServiceHandle(lServ)
                    Return res
                End If
            End If
            Return False
        End Function

        ' Resume a service
        Public Shared Function ResumeServiceByName(ByVal name As String, ByVal hSCManager As IntPtr) As Boolean
            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, Native.Security.ServiceAccess.PauseContinue)
            Dim res As Boolean = False
            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then
                    Dim lpss As NativeStructs.ServiceStatusProcess
                    res = NativeFunctions.ControlService(lServ, NativeEnums.ServiceControl.Continue, lpss)
                    NativeFunctions.CloseServiceHandle(lServ)
                    Return res
                End If
            End If
            Return False
        End Function

        ' Set start type
        Public Shared Function SetServiceStartTypeByName(ByVal name As String, _
                                             ByVal type As NativeEnums.ServiceStartType, _
                                             ByVal hSCManager As IntPtr) As Boolean
            Dim hLockSCManager As IntPtr
            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, Native.Security.ServiceAccess.ChangeConfig)
            Dim ret As Boolean = False

            hLockSCManager = NativeFunctions.LockServiceDatabase(hSCManager)

            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then
                    ret = NativeFunctions.ChangeServiceConfig(lServ, NativeEnums.ServiceType.NoChange, _
                                    type, _
                                    NativeEnums.ServiceErrorControl.NoChange, _
                                    Nothing, Nothing, Nothing, _
                                    Nothing, Nothing, Nothing, Nothing)
                    NativeFunctions.CloseServiceHandle(lServ)
                End If
                NativeFunctions.UnlockServiceDatabase(hLockSCManager)
                Return ret
            End If
            Return False
        End Function

        ' Start service
        Public Shared Function StartServiceByName(ByVal name As String, ByVal hSCManager As IntPtr) As Boolean
            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, _
                                                   Native.Security.ServiceAccess.Start)
            Dim res As Boolean
            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then
                    res = NativeFunctions.StartService(lServ, 0, Nothing)
                    NativeFunctions.CloseServiceHandle(lServ)
                    Return res
                End If
            End If
            Return False
        End Function

        ' Delete service
        Public Shared Function DeleteServiceByName(ByVal name As String, ByVal hSCManager As IntPtr) As Boolean
            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, _
                                                   Native.Security.ServiceAccess.Delete)
            Dim res As Boolean
            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then
                    res = NativeFunctions.DeleteService(lServ)
                    NativeFunctions.CloseServiceHandle(lServ)
                    Return res
                End If
            End If
            Return False
        End Function

        ' Stop service
        Public Shared Function StopServiceByName(ByVal name As String, ByVal hSCManager As IntPtr) As Boolean
            Dim lServ As IntPtr = NativeFunctions.OpenService(hSCManager, name, Native.Security.ServiceAccess.Stop)
            Dim res As Boolean = False
            If hSCManager.IsNotNull Then
                If lServ.IsNotNull Then
                    Dim lpss As NativeStructs.ServiceStatusProcess
                    res = NativeFunctions.ControlService(lServ, _
                                        NativeEnums.ServiceControl.Stop, _
                                        lpss)
                    NativeFunctions.CloseServiceHandle(lServ)
                    Return res
                End If
            End If
            Return False
        End Function

        ' Get SC manager handle
        Public Shared Function GetSCManagerHandle(ByVal access As Native.Security.ServiceManagerAccess) As IntPtr
            Return NativeFunctions.OpenSCManager(Nothing, Nothing, access)
        End Function
        Public Shared Function GetSCManagerHandle(ByVal access As Native.Security.ServiceManagerAccess, ByVal machine As String) As IntPtr
            Return NativeFunctions.OpenSCManager(machine, Nothing, access)
        End Function

        ' Close SC manager handle
        Public Shared Sub CloseSCManagerHandle(ByVal hSCManager As IntPtr)
            Native.Api.NativeFunctions.CloseServiceHandle(hSCManager)
        End Sub

        ' Return dependencies of a service (as a string array)
        Public Shared Function GetServiceDependenciesAsStringArrayFromPtr(ByVal thePtr As IntPtr) As String()

            If thePtr.IsNotNull Then

                Dim res As String = Marshal.PtrToStringUni(thePtr, &H400)

                If res Is Nothing OrElse res.Length = 0 OrElse (Char.IsLetterOrDigit(res.Chars(0)) Or Char.IsSymbol(res.Chars(0))) = False Then
                    Return Nothing
                End If

                ' Find the 2 consecutive NullChars
                For i As Integer = 0 To &H400 - 1
                    If res.Chars(i) = CChar(ChrW(0)) AndAlso res.Chars(i + 1) = CChar(ChrW(0)) Then
                        res = res.Substring(0, i)
                        Return Split(res, vbNullChar)
                    End If
                Next

            End If

            Return Nothing

            '' === Get dependencies of service
            'If thePtr.IsNotNull Then

            '    ' Get a short array from memory
            '    ' Delimited by 2 null chars (e.g 4 zero byte as it is unicode)
            '    Dim res() As Int16
            '    ReDim res(0)
            '    Dim size As Integer = 1

            '    Dim b1 As Short = -1
            '    Dim b2 As Short = -1

            '    Do While Not (b1 = 0 And b2 = 0)
            '        size += 1
            '        ReDim res(size - 1)
            '        Marshal.Copy(thePtr, res, 0, res.Length)
            '        b1 = res(size - 2)
            '        b2 = res(size - 1)
            '    Loop

            '    size -= 1
            '    ReDim Preserve res(size - 1)

            '    ' Get a string array from this short array
            '    Dim __var As String
            '    Dim rr() As String
            '    ReDim rr(-1)
            '    Dim xOld As Integer = 0
            '    Dim y As Integer

            '    If size > 2 Then

            '        For x As Integer = 0 To size - 1

            '            If res(x) = 0 Then
            '                ' Then it's variable end
            '                ReDim Preserve rr(rr.Length)  ' Add one item to list
            '                Try
            '                    ' Parse short array to retrieve an unicode string
            '                    y = x * 2
            '                    Dim __size As Integer = (y - xOld) \ 2

            '                    ' Allocate unmanaged memory
            '                    Dim ptr As IntPtr = Marshal.AllocHGlobal(y - xOld)

            '                    ' Copy from short array to unmanaged memory
            '                    Marshal.Copy(res, xOld \ 2, ptr, __size)

            '                    ' Convert to string (and copy to __var variable)
            '                    __var = Marshal.PtrToStringUni(ptr, __size)

            '                    ' Free unmanaged memory
            '                    Marshal.FreeHGlobal(ptr)

            '                Catch ex As Exception
            '                    __var = ""
            '                End Try

            '                ' Insert variable
            '                rr(rr.Length - 1) = __var

            '                xOld = y + 2
            '            End If
            '        Next

            '        Return rr
            '    End If
            'End If

            'Return Nothing
        End Function



        ' ========================================
        ' Private functions
        ' ========================================


        ' Get infos from registry
        Private Shared Sub GetServiceInformationsFromRegistry(ByVal name As String, _
                                                       ByRef infos As serviceInfos)

            ' Get description
            Dim desc As String = GetServiceInformationFromRegistryByName(name, "Description")

            ' If in starts with @, then it points to a resource in a file
            If InStr(desc, "@", CompareMethod.Binary) > 0 Then
                desc = Native.Objects.File.GetResourceStringFromFile(desc)
            End If

            ' Remove duplicated slashs
            desc = Replace(desc, "\", "\\")

            ' Get other infos
            Dim obj As String = GetServiceInformationFromRegistryByName(name, "ObjectName")
            Dim dmf As String = GetServiceInformationFromRegistryByName(name, "DiagnosticMessageFile")

            ' Set to result
            infos.SetRegInfos(desc, dmf, obj)
        End Sub

        ' Retrieve an information about a service from registry
        Private Shared Function GetServiceInformationFromRegistryByName(ByVal name As String, _
                                                                 ByVal info As String) As String
            Try
                Return CStr(My.Computer.Registry.GetValue(ServicePathInRegistry _
                             & name, info, ""))
            Catch ex As Exception
                Return ""
            End Try
        End Function

    End Class

End Namespace
