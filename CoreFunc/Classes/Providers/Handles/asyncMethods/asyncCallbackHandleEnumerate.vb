Option Strict On

Imports CoreFunc.cModuleConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackHandleEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cHandleConnection
        Public pid() As Integer
        Public unNamed As Boolean
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], _
                       ByRef co As cHandleConnection, ByVal pi() As Integer, _
                       ByVal unN As Boolean)
            ctrl = ctr
            deg = de
            con = co
            pid = pi
            unNamed = unN
        End Sub
    End Structure


    Public Shared Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If pObj.con.ConnectionObj.IsConnected = False Then
            Exit Sub
        End If

        Select Case pObj.con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, handleInfos)

                Call cHandle.handles_Renamed.Refresh(pObj.pid)

                Dim x As Integer = 0
                For i As Integer = 0 To cHandle.handles_Renamed.Count - 1
                    If cHandle.handles_Renamed.GetHandle(i) > 0 Then
                        If pObj.unNamed OrElse (Len(cHandle.handles_Renamed.GetObjectName(i)) > 0) Then
                            With cHandle.handles_Renamed
                                Dim retHandleCount As Integer
                                Dim retHandle As Integer
                                Dim retName As String
                                Dim retObjectCount As Integer
                                Dim retPid As Integer
                                Dim retPointerCount As Integer
                                Dim retType As String
                                With cHandle.handles_Renamed
                                    retHandleCount = .GetHandleCount(i)
                                    retHandle = .GetHandle(i)
                                    retName = .GetObjectName(i)
                                    retObjectCount = .GetObjectCount(i)
                                    retPid = .GetProcessID(i)
                                    retPointerCount = .GetPointerCount(i)
                                    retType = .GetNameInformation(i)
                                End With
                                Dim _key As String = retPid.ToString & "-" & retHandle.ToString & "-" & retType & "-" & retName
                                Dim ret As New handleInfos(retHandle, retType, retPid, retName, retHandleCount, retPointerCount, retObjectCount)
                                _dico.Add(_key, ret)
                            End With
                        End If
                    End If
                Next

                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

    End Sub

End Class
