Option Strict On

Imports CoreFunc.cProcessConnection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackThreadEnumerate

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure poolObj
        Public ctrl As Control
        Public deg As [Delegate]
        Public con As cThreadConnection
        Public pid() As Integer
        Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cThreadConnection, ByVal pi() As Integer)
            ctrl = ctr
            deg = de
            con = co
            pid = pi
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

                ' NOT possible to get thread list from WMI               
                ' pObj.ctrl.Invoke(pObj.deg, True, _dico, Nothing)

            Case Else
                ' Local
                Dim _dico As New Dictionary(Of String, threadInfos)
                For Each id As Integer In pObj.pid
                    If asyncCallbackProcEnumerate.AvailableThreads.ContainsKey(id) Then
                        For Each pair As System.Collections.Generic.KeyValuePair(Of String, threadInfos) In asyncCallbackProcEnumerate.AvailableThreads(id)
                            _dico.Add(pair.Key, pair.Value)
                        Next
                    End If
                Next
                pObj.ctrl.Invoke(pObj.deg, True, _dico, API.GetError)

        End Select

    End Sub

End Class
