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

Public Class asyncCallbackMemRegionEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cMemRegionConnection
        Public pid As Integer
        Public onlyProcRegion As Boolean
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cMemRegionConnection, ByVal pi As Integer, ByVal onlyPR As Boolean)
            ctrl = ctr
            deg = de
            con = co
            pid = pi
            onlyProcRegion = onlyPR
        End Sub
    End Structure

    ' When socket got a list  !
    Private Shared _poolObj As poolObj
    Friend Shared Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, memRegionInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), memRegionInfos))
            Next
        End If
        _poolObj.ctrl.Invoke(_poolObj.deg, True, dico, Nothing)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Shared Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestMemoryRegionList)
                    Dim buff() As Byte = cSerialization.GetSerializedObject(cDat)
                    pObj.con.ConnectionObj.Socket.Send(buff, buff.Length)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim lHandle As Integer
                Dim lPosMem As Integer = 0
                Dim lRet As Boolean
                Dim mbi As API.MEMORY_BASIC_INFORMATION
                Dim _dico As New Dictionary(Of String, memRegionInfos)

                lHandle = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION Or API.PROCESS_RIGHTS.PROCESS_VM_READ, 0, pObj.pid)

                If lHandle > 0 Then

                    ' We'll exit when VirtualQueryEx wall will fail
                    While True

                        lRet = API.VirtualQueryEx(lHandle, lPosMem, mbi, Marshal.SizeOf(mbi))

                        If lRet Then

                            If mbi.RegionSize > 0 AndAlso _
                            ((Not pObj.onlyProcRegion) OrElse (mbi.lType = API.MEMORY_TYPE.MEM_PRIVATE And _
                                                              mbi.State = API.MEMORY_STATE.MEM_COMMIT)) Then
                                ' Here is a region
                                _dico.Add(mbi.BaseAddress.ToString, _
                                          New memRegionInfos(mbi, pObj.pid))
                            End If

                            lPosMem = mbi.BaseAddress + mbi.RegionSize
                        Else
                            ' Done
                            Exit While
                        End If
                    End While

                    Call API.CloseHandle(lHandle)
                End If

                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

        sem.Release()

    End Sub

End Class
