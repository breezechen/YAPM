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

Imports CoreFunc.cModuleConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackModuleEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cModuleConnection
        Public forInstanceId As Integer
        Public pid() As Integer
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cModuleConnection, ByVal pi() As Integer, ByVal iid As Integer)
            ctrl = ctr
            deg = de
            con = co
            forInstanceId = iid
            pid = pi
        End Sub
    End Structure

    ' When socket got a list  !
    Private Shared _poolObj As poolObj
    Friend Shared Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, moduleInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), moduleInfos))
            Next
        End If
        _poolObj.ctrl.Invoke(_poolObj.deg, True, dico, Nothing)
    End Sub
    Public Shared Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestModuleList, pObj.pid)
                    Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                    pObj.con.ConnectionObj.Socket.Send(buff, buff.Length)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim res As ManagementObjectCollection = Nothing
                Try
                    res = pObj.con.wmiSearcher.Get()
                Catch ex As Exception
                    pObj.ctrl.Invoke(pObj.deg, False, Nothing, ex.Message)
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

                    Dim pid As Integer = CInt(refProcess.GetPropertyValue(API.WMI_INFO_PROCESS.ProcessId.ToString))
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
                            Dim obj As New API.MODULEINFO
                            Dim path As String = CStr(refModule.GetPropertyValue("Name"))

                            With obj
                                ' Get base address from dico
                                ' TOCHANGE
                                .BaseOfDll = CType(0, IntPtr)
                                .EntryPoint = CType(0, IntPtr)
                                .SizeOfImage = 0
                            End With


                            ' Do we have to get fixed infos ?
                            Dim _manuf As String = CStr(refModule.GetPropertyValue("Manufacturer"))
                            Dim _vers As String = CStr(refModule.GetPropertyValue("Version"))
                            Dim _module As New moduleInfos(obj, pid, path, _vers, _manuf)
                            Dim _key As String = path & "-" & pid.ToString & "-" & obj.BaseOfDll.ToString
                            _dico.Add(_key, _module)

                        Next
                    End If
                Next

                pObj.ctrl.Invoke(pObj.deg, True, _dico, Nothing)

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, moduleInfos)
                For Each id As Integer In pObj.pid
                    Dim _md As New Dictionary(Of String, moduleInfos)
                    _md = GetModules(id)
                    For Each pair As System.Collections.Generic.KeyValuePair(Of String, moduleInfos) In _md
                        _dico.Add(pair.Key, pair.Value)
                    Next
                Next
                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

    End Sub

    Private Shared Function GetModules(ByVal pid As Integer) As Dictionary(Of String, moduleInfos)
        Dim size As Integer
        Dim _handles As IntPtr()

        Dim ret As New Dictionary(Of String, moduleInfos)

        ' Get handle
        Dim hProc As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION Or API.PROCESS_RIGHTS.PROCESS_VM_READ, 0, pid)
        If hProc > 0 Then

            ' Get size & number of modules
            API.EnumProcessModules(hProc, Nothing, 0, size)
            Dim count As Integer = CInt(size / 4 - 1)

            If count > 0 Then
                _handles = New IntPtr(count) {}

                ' Get handles
                API.EnumProcessModules(hProc, _handles, size, size)

                ' For each handle, we add the associated module to dico
                For Each z As IntPtr In _handles
                    Dim baseName As New StringBuilder(1024)
                    Dim fileName As New StringBuilder(1024)
                    Dim MI As New API.MODULEINFO

                    API.GetModuleBaseName(hProc, z, baseName, 1024)
                    API.GetModuleFileNameEx(hProc, z, fileName, 1024)
                    API.GetModuleInformation(hProc, z, MI, Marshal.SizeOf(MI))

                    ' path-pid-baseAddress
                    Dim _key As String = fileName.ToString & "-" & pid.ToString & "-" & MI.BaseOfDll.ToString

                    ret.Add(_key, New moduleInfos(MI, pid, fileName.ToString))
                Next
            Else

            End If

        End If

        Return ret
    End Function
End Class
