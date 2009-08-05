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

Public Class asyncCallbackEnvVariableEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cEnvVariableConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cEnvVariableConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public peb As Integer
        Public forInstanceId As Integer
        Public Sub New(ByVal pi As Integer, ByVal add As Integer, ByVal ii As Integer)
            forInstanceId = ii
            peb = add
            pid = pi
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, envVariableInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), envVariableInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestEnvironmentVariableList, pObj.pid, -1)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, envVariableInfos)

                Dim var() As String = Nothing
                Dim val() As String = Nothing
                Call GetEnvironmentVariables(pObj.peb, pObj.pid, var, val)

                For x As Integer = 0 To var.Length - 1
                    If _dico.ContainsKey(var(x)) = False Then
                        _dico.Add(var(x), New envVariableInfos(var(x), val(x), pObj.pid))
                    End If
                Next

                If deg IsNot Nothing AndAlso ctrl.Created Then _
                    ctrl.Invoke(deg, True, _dico, API.GetError, pObj.forInstanceId)

        End Select

        sem.Release()

    End Sub



    ' Return environment variables
    Friend Shared Function GetEnvironmentVariables(ByRef process As cProcess, ByRef variables() As String, _
                                            ByRef values() As String) As Integer
        Return GetEnvironmentVariables(process.Infos.PEBAddress, process.Infos.Pid, variables, values)
    End Function
    Friend Shared Function GetEnvironmentVariables(ByVal peb As Integer, ByVal pid As Integer, ByRef variables() As String, _
                                            ByRef values() As String) As Integer

        ReDim variables(-1)
        ReDim values(-1)

        ' Get PEB address of process
        Dim __pebAd As Integer = peb
        If __pebAd <= 0 Then
            Return 0
        End If

        ' Create a processMemRW class to read in memory
        Dim cR As New cProcessMemRW(pid)

        If cR.Handle = 0 Then
            Return 0              ' Couldn't open a handle
        End If

        ' Read first 20 bytes (5 integers) of PEB block
        ' The fifth integer contains address of ProcessParameters block
        Dim pebDeb() As Integer = cR.ReadBytesAI(__pebAd, 5)
        Dim __procParamAd As Integer = pebDeb(4)

        ' Get environnement block address
        ' It's located at offset 0x48 on all NT systems because it's after a fixed structure
        ' of 72 bytes
        Dim bA() As Integer = cR.ReadBytesAI(__procParamAd + 72, 1)
        Dim _envDeb As Integer = bA(0)      ' Get address


        ' ======= Read environnement block byte per byte to calculate env. block size
        ' Block is finished by a 4 consecutive null bytes (0)
        ' Short variables because it's unicode coded (2 bytes per char)
        Dim b1 As Short = -1
        Dim b2 As Short = -1
        Dim _size As Integer = 0

        ' Read mem until 4 null char (<==> 2 null shorts)
        Do While Not (b1 = 0 And b2 = 0)
            b1 = cR.Read2Bytes(_envDeb + _size)
            b2 = cR.Read2Bytes(_envDeb + _size + 2)
            _size += 2
        Loop

        ' Now we can get all env. variables from memory
        Dim blockEnv() As Short = cR.ReadBytesAS(_envDeb, _size)

        ' Parse these env. variables
        ' Env. variables are separated by 2 null bytes ( <==> 1 null short)
        Dim _envVar() As String
        Dim __var As String
        Dim xOld As Integer = 0
        ReDim _envVar(0)
        Dim y As Integer

        For x As Integer = 0 To CInt(_size / 2)

            If blockEnv(x) = 0 Then
                ' Then it's variable end
                ReDim Preserve _envVar(_envVar.Length)  ' Add one item to list
                Try
                    ' Parse short array to retrieve an unicode string
                    y = x * 2
                    Dim __size As Integer = CInt((y - xOld) / 2)

                    ' Allocate unmanaged memory
                    Dim ptr As IntPtr = Marshal.AllocHGlobal(y - xOld)

                    ' Copy from short array to unmanaged memory
                    Marshal.Copy(blockEnv, CInt(xOld / 2), ptr, __size)

                    ' Convert to string (and copy to __var variable)
                    __var = Marshal.PtrToStringUni(ptr, __size)

                    ' Free unmanaged memory
                    Marshal.FreeHGlobal(ptr)

                Catch ex As Exception
                    MsgBox(ex.Message)
                    __var = ""
                End Try

                ' Insert variable
                _envVar(_envVar.Length - 2) = __var

                xOld = y + 2
            End If
        Next

        ' Remove useless last nothing item
        ReDim Preserve _envVar(_envVar.Length - 2)

        ' Separate variables and values
        ReDim variables(_envVar.Length - 2)
        ReDim values(_envVar.Length - 2)

        For x As Integer = 0 To _envVar.Length - 2
            Dim i As Integer = InStr(_envVar(x), "=", CompareMethod.Binary)
            If i > 0 Then
                variables(x) = _envVar(x).Substring(0, i - 1)
                values(x) = _envVar(x).Substring(i, _envVar(x).Length - i)
            Else
                variables(x) = ""
                values(x) = ""
            End If
        Next

        Return _envVar.Length

    End Function

End Class
