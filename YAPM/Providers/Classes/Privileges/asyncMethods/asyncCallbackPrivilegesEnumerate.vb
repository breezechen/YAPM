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

Public Class asyncCallbackPrivilegesEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cPrivilegeConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cPrivilegeConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public forInstanceId As Integer
        Public Sub New(ByVal pi As Integer, ByVal ii As Integer)
            forInstanceId = ii
            pid = pi
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, privilegeInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), privilegeInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestPrivilegesList, pObj.pid)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, privilegeInfos)

                Dim ret As API.PrivilegeInfo() = GetPrivilegesList(pObj.pid)

                For Each tmp As API.PrivilegeInfo In ret
                    _dico.Add(tmp.Name, New privilegeInfos(tmp.Name, pObj.pid))
                Next

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, API.GetError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub



    ' Get privileges list of process
    Private Shared Function GetPrivilegesList(ByVal _pid As Integer) As API.PrivilegeInfo()

        Dim ListPrivileges() As API.PrivilegeInfo
        ReDim ListPrivileges(-1)
        Dim hProcessToken As Integer
        Dim hProcess As Integer
        Dim RetLen As Integer
        Dim TokenPriv As New API.TOKEN_PRIVILEGES
        Dim strBuff As String
        Dim lngBuff As Integer
        Dim i As Integer

        hProcess = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION, 0, _pid)
        If hProcess > 0 Then
            API.OpenProcessToken(hProcess, API.TOKEN_RIGHTS.Query, hProcessToken)
            If hProcessToken > 0 Then

                ' Get tokeninfo length
                API.GetTokenInformation(hProcessToken, API.TOKEN_INFORMATION_CLASS.TokenPrivileges, 0, 0, RetLen)
                Dim TokenInformation As IntPtr = Marshal.AllocHGlobal(RetLen)
                ' Get token information
                API.GetTokenInformation(hProcessToken, API.TOKEN_INFORMATION_CLASS.TokenPrivileges, CInt(TokenInformation), RetLen, 0)
                ' Get a valid structure
                TokenPriv = getTokenPrivilegeStructureFromPointer(TokenInformation, RetLen)

                ReDim ListPrivileges(TokenPriv.PrivilegeCount - 1)
                For i = 0 To TokenPriv.PrivilegeCount - 1
                    API.LookupPrivilegeNameA("", TokenPriv.Privileges(i).pLuid, "", lngBuff)
                    strBuff = Space$(lngBuff - 1)
                    API.LookupPrivilegeNameA("", TokenPriv.Privileges(i).pLuid, strBuff, lngBuff)
                    ListPrivileges(i).Name = strBuff
                    ListPrivileges(i).Status = TokenPriv.Privileges(i).Attributes
                    ListPrivileges(i).pLuid = TokenPriv.Privileges(i).pLuid
                    lngBuff = 0
                Next i
                API.CloseHandle(hProcessToken)
                Marshal.FreeHGlobal(TokenInformation)
            End If
            API.CloseHandle(hProcess)
        End If

        Return ListPrivileges

    End Function


    Private Shared Function getTokenPrivilegeStructureFromPointer(ByVal ptr As IntPtr, _
        ByVal RetLen As Integer) As API.TOKEN_PRIVILEGES

        'Public Structure LUID
        '	Dim lowpart As Integer
        '	Dim highpart As Integer
        'End Structure
        'Private Structure LUID_AND_ATTRIBUTES
        '	Dim pLuid As LUID
        '	Dim Attributes As Integer
        'End Structure
        'Private Structure TOKEN_PRIVILEGES
        '	Dim PrivilegeCount As Integer
        '	Dim Privileges() As LUID_AND_ATTRIBUTES
        'End Structure

        Dim ret As New API.TOKEN_PRIVILEGES

        ' Fill in int array from unmanaged memory
        Dim arr(CInt(RetLen / 4)) As Integer
        Marshal.Copy(ptr, arr, 0, arr.Length - 1)

        ' Get number of privileges
        Dim pCount As Integer = arr(0)     'CInt((RetLen - 4) / 12)
        ReDim ret.Privileges(pCount - 1)
        ret.PrivilegeCount = pCount

        ' Fill Privileges() array of ret
        ' Each item of array composed of three integer (lowPart, highPart and Attributes)
        Dim ep As Integer = 1
        For x As Integer = 0 To pCount - 1
            ret.Privileges(x).pLuid.lowpart = arr(ep)
            ret.Privileges(x).pLuid.highpart = arr(ep + 1)
            ret.Privileges(x).Attributes = arr(ep + 2)
            ep += 3
        Next

        Return ret

    End Function

End Class
