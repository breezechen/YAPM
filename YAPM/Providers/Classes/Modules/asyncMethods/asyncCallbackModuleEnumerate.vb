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
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackModuleEnumerate

    ' Some material to retrieve infos about files ONCE
    Friend Shared fileInformations As New Dictionary(Of String, FileVersionInfo)
    Friend Shared semDicoFileInfos As New System.Threading.Semaphore(1, 1)

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cModuleConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cModuleConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public forInstanceId As Integer
        Public pid() As Integer
        Public Sub New(ByVal pi() As Integer, ByVal iid As Integer)
            forInstanceId = iid
            pid = pi
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, moduleInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), moduleInfos))
            Next
        End If
        If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestModuleList, pObj.pid)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim res As ManagementObjectCollection = Nothing
                Try
                    res = con.wmiSearcher.Get()
                Catch ex As Exception
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, False, Nothing, ex.Message, pObj.forInstanceId)
                    sem.Release()
                    Exit Sub
                End Try


                'For Each refProcess As Management.ManagementObject In res
                '    Dim colMod As ManagementObjectCollection = refProcess.GetRelationships("CIM_ProcessExecutable")
                '    Dim _dicoBaseA As New Dictionary(Of String, Integer)
                '    For Each refModule As ManagementObject In colMod
                '        Dim _s As String = CStr(refModule.GetPropertyValue("Antecedent")).ToLowerInvariant
                '        ' Extract dll path from _s
                '        Dim i As Integer = InStr(_s, "name=", CompareMethod.Binary)
                '        Dim __s As String = vbNullString
                '        If i > 0 Then
                '            __s = _s.Substring(i + 5, _s.Length - i - 6).Replace("\\", "\")
                '        End If
                '        If __s IsNot Nothing Then
                '            _dicoBaseA.Add(__s, CInt(refModule.GetPropertyValue("BaseAddress")))
                '        End If
                '    Next
                'Next

                Dim _dico As New Dictionary(Of String, moduleInfos)
                For Each refProcess As Management.ManagementObject In res

                    Dim pid As Integer = CInt(refProcess.GetPropertyValue(Native.Api.Enums.WMI_INFO_PROCESS.ProcessId.ToString))
                    Dim ex As Boolean = False
                    For Each _iii As Integer In pObj.pid
                        If pid = _iii Then
                            ex = True
                            Exit For
                        End If
                    Next

                    ' If ex -> OK, we get modules for this process
                    If ex Then

                        Dim colModule As ManagementObjectCollection = refProcess.GetRelated("CIM_DataFile")
                        For Each refModule As ManagementObject In colModule
                            Dim obj As New Native.Api.NativeStructs.LdrDataTableEntry
                            Dim path As String = CStr(refModule.GetPropertyValue("Name"))

                            With obj
                                ' Get base address from dico
                                ' TOCHANGE
                                .DllBase = CType(0, IntPtr)
                                .EntryPoint = CType(0, IntPtr)
                                .SizeOfImage = 0
                            End With

                            Dim _manuf As String = CStr(refModule.GetPropertyValue("Manufacturer"))
                            Dim _vers As String = CStr(refModule.GetPropertyValue("Version"))
                            Dim _module As New moduleInfos(obj, pid, path, _vers, _manuf)
                            Dim _key As String = path & "-" & pid.ToString & "-" & obj.DllBase.ToString
                            _dico.Add(_key, _module)

                        Next
                    End If
                Next

                ctrl.Invoke(deg, True, _dico, Nothing, pObj.forInstanceId)

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, moduleInfos)

                Call enumModules(pObj, _dico, True)

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)

        End Select


        sem.Release()

    End Sub

    ' Enumerate modules (local)
    Friend Shared Sub enumModules(ByVal pObj As poolObj, ByRef _dico As Dictionary(Of String, moduleInfos), ByVal getFileInfos As Boolean)
        For Each id As Integer In pObj.pid
            Dim _md As New Dictionary(Of String, moduleInfos)
            _md = GetModules(id, Not (getFileInfos))
            For Each pair As System.Collections.Generic.KeyValuePair(Of String, moduleInfos) In _md
                _dico.Add(pair.Key, pair.Value)
            Next
        Next
    End Sub

    ' Retrieve modules of a process (uses EnumProcessModules)
    'Friend Shared Function GetModules(ByVal pid As Integer, Optional ByVal noFileInfo As Boolean = False) As Dictionary(Of String, moduleInfos)
    '    Dim size As Integer
    '    Dim _handles As IntPtr()

    '    Dim ret As New Dictionary(Of String, moduleInfos)

    '    ' Get handle
    '    Dim hProc As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION Or API.PROCESS_RIGHTS.PROCESS_VM_READ, 0, pid)
    '    If hProc > 0 Then

    '        ' Get size & number of modules
    '        API.EnumProcessModules(hProc, Nothing, 0, size)
    '        Dim count As Integer = CInt(size / 4 - 1)

    '        If count > 0 Then
    '            _handles = New IntPtr(count) {}

    '            ' Get handles
    '            API.EnumProcessModules(hProc, _handles, size, size)

    '            ' For each handle, we add the associated module to dico
    '            For Each z As IntPtr In _handles
    '                Dim baseName As New StringBuilder(1024)
    '                Dim fileName As New StringBuilder(1024)
    '                Dim MI As New API.MODULEINFO

    '                API.GetModuleBaseName(hProc, z, baseName, 1024)
    '                API.GetModuleFileNameEx(hProc, z, fileName, 1024)
    '                API.GetModuleInformation(hProc, z, MI, Marshal.SizeOf(MI))

    '                ' path-pid-baseAddress
    '                Dim _key As String = fileName.ToString & "-" & pid.ToString & "-" & MI.BaseOfDll.ToString

    '                ret.Add(_key, New moduleInfos(MI, pid, fileName.ToString, noFileInfo))
    '            Next
    '        Else

    '        End If

    '    End If

    '    Return ret
    'End Function

    ' Retrieve modules of a process (uses PEB_LDR_DATA structs)
    Public Shared Function GetModules(ByVal pid As Integer, Optional ByVal noFileInfo As Boolean = False) As Dictionary(Of String, moduleInfos)
        Dim retDico As New Dictionary(Of String, moduleInfos)

        Dim hProc As IntPtr
        Dim peb As IntPtr
        Dim loaderDatePtr As IntPtr
        '64TODO
        ' Open a reader to access memory !
        Dim reader As New cProcessMemReader(pid)
        hProc = reader.ProcessHandle

        If hProc <> IntPtr.Zero Then

            peb = reader.GetPEBAddress

            ' PEB struct documented here
            ' http://undocumented.ntinternals.net/UserMode/Undocumented%20Functions/NT%20Objects/Process/PEB.html

            ' Get address of LoaderData pointer
            peb.Increment(&HC) ' See structure for details about offsets
            loaderDatePtr = reader.ReadIntPtr(peb)

            ' PEB_LDR_DATA documented here
            ' http://msdn.microsoft.com/en-us/library/aa813708(VS.85).aspx
            Dim ldrData As New Native.Api.NativeStructs.PebLdrData
            ldrData = CType(reader.ReadStruct(Of Native.Api.NativeStructs.PebLdrData)(loaderDatePtr),  _
                        Native.Api.NativeStructs.PebLdrData)

            ' Now navigate into structure
            Dim curObj As IntPtr = ldrData.InLoadOrderModuleList.Flink
            Dim firstObj As IntPtr = curObj
            Dim dllName As String
            Dim dllPath As String
            Dim curEntry As Native.Api.NativeStructs.LdrDataTableEntry
            Dim i As Integer = 0

            Do While curObj <> IntPtr.Zero

                If (i > 0 AndAlso curObj = firstObj) Then
                    Exit Do
                End If

                ' Read LoaderData entry
                curEntry = CType(reader.ReadStruct(Of Native.Api.NativeStructs.LdrDataTableEntry)(curObj),  _
                                Native.Api.NativeStructs.LdrDataTableEntry)

                If (curEntry.DllBase <> IntPtr.Zero) Then

                    ' Retrive the path/name of the dll
                    dllPath = reader.ReadUnicodeString(curEntry.FullDllName)
                    If dllPath Is Nothing Then
                        dllPath = NO_INFO_RETRIEVED
                    End If
                    dllName = reader.ReadUnicodeString(curEntry.BaseDllName)
                    If dllName Is Nothing Then
                        dllName = NO_INFO_RETRIEVED
                    End If

                    ' Add to dico
                    ' Key is path-pid-baseAddress
                    Dim _key As String = dllPath.ToString & "-" & pid.ToString & "-" & curEntry.DllBase.ToString
                    If retDico.ContainsKey(_key) = False Then
                        retDico.Add(_key, New moduleInfos(curEntry, pid, dllPath, dllName, noFileInfo))
                    End If

                End If

                ' Next entry
                curObj = curEntry.InLoadOrderLinks.Flink
                i += 1
            Loop

        End If

        reader.Dispose()
        Return retDico

    End Function

End Class
