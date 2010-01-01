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

Imports System.Net
Imports System.Net.NetworkInformation

Public Class cNetwork
    Inherits cGeneralObject
    Implements IDisposable

    Private Shared _dicoUdp As New Dictionary(Of Integer, String)
    Private Shared _dicoTcp As New Dictionary(Of Integer, String)

    Private nullAddress As New IPAddress(0)     ' For address comparison

    Private _haveResolvedAnAddress As Boolean
    Private _networkInfos As networkInfos
    Private Shared WithEvents _connection As cNetworkConnection

#Region "Properties"

    Public Shared Property Connection() As cNetworkConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cNetworkConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As networkInfos)
        _networkInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.NetworkConnection

        ' Solve DNS (only on local mode)
        If cNetwork.Connection.ConnectionObj.Type = cConnection.TypeOfConnection.LocalConnection Then
            Try
                If Me.Infos._Local.Address.Equals(nullAddress) = False Then
                    Dim t As New System.Threading.WaitCallback(AddressOf getHostNameLocal)
                    Call Threading.ThreadPool.QueueUserWorkItem(t, Me.Infos.Local)
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If

        ' If not Snapshot mode (it has no sense as the snapshot might refer
        ' to a system on another network...)
        If cNetwork.Connection.ConnectionObj.Type <> cConnection.TypeOfConnection.SnapshotFile Then
            Try
                If Me.Infos._remote IsNot Nothing AndAlso Me.Infos._remote.Address.Equals(nullAddress) = False Then
                    Dim t As New System.Threading.WaitCallback(AddressOf getHostNameRemote)
                    Call Threading.ThreadPool.QueueUserWorkItem(t, Me.Infos.Remote)
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If
    End Sub
    Private disposed As Boolean = False
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' This object will be cleaned up by the Dispose method.
        ' Therefore, you should call GC.SupressFinalize to
        ' take this object off the finalization queue 
        ' and prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not Me.disposed Then
            ' If disposing equals true, dispose all managed 
            ' and unmanaged resources.
            If disposing Then
                ' Dispose managed resources.

            End If

            ' Note disposing has been done.
            disposed = True

        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As networkInfos
        Get
            Return _networkInfos
        End Get
    End Property

#End Region

#Region "Local shared method"

    ' Close a TCP connection
    Public Shared Function LocalCloseTCP(ByVal local As IPEndPoint, ByVal remote As IPEndPoint) As Integer
        Return Native.Objects.Network.CloseTcpConnectionByIPEndPoints(local, remote)
    End Function

    ' Retrieve description of a port
    Public Shared Function GetPortDescription(ByVal port As Integer, ByVal protocol As Native.Api.Enums.NetworkProtocol) As String
        If port = 0 Then
            Return ""
        End If
        If protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
            If _dicoTcp.ContainsKey(port) Then
                Return _dicoTcp.Item(port)
            Else
                Return NO_INFO_RETRIEVED
            End If
        Else
            If _dicoUdp.ContainsKey(port) Then
                Return _dicoUdp.Item(port)
            Else
                Return NO_INFO_RETRIEVED
            End If
        End If
    End Function

    ' Ping (sync)
    Public Sub Ping(ByVal lv As ListView)

        If Me.Infos.Remote Is Nothing Then
            ' No IP address
            Async.ListView.AddItem(lv, "Could not ping a null address")
            Exit Sub
        End If

        ' Create ping objects
        Dim pinger As New Ping
        Dim pingOpt As New PingOptions
        Dim pingRep As PingReply = Nothing

        ' Conf
        Dim theIp As IPAddress = Me.Infos.Remote.Address
        Dim pingTimeOut As Integer = 1000
        Dim pingCount As Integer = 4
        Dim pingSize As Integer = 32
        Dim pingBuff(pingSize) As Byte

        ' Results
        Dim pingSent As Integer = 0
        Dim pingLost As Integer = 0
        Dim pingReceived As Integer = 0
        Dim pingRespMin As Long = Long.MaxValue
        Dim pingRespMax As Long = Long.MinValue
        Dim pingRespAvg As Long = 0

        Async.ListView.AddItem(lv, "Pinging [" & theIp.ToString & "] with " & pingSize.ToString & " bytes of data:")

        For pingSent = 0 To pingCount - 1
            Try
                ' Send the "ping command"
                pingRep = pinger.Send(theIp, pingTimeOut, pingBuff, pingOpt)
            Catch ex As Exception
                Async.ListView.AddItem(lv, "Ping error : " & ex.Message)
            End Try

            ' Depending on the rep status...
            If pingRep.Status = IPStatus.Success Then

                If pingRep.Options Is Nothing Then
                    ' Sometimes it's nothing (?)
                    Async.ListView.AddItem(lv, "Reply from " & theIp.ToString & ": bytes=" & pingSize.ToString & " time=" & pingRep.RoundtripTime.ToString & "ms TTL=" & pingOpt.Ttl.ToString & vbNewLine)
                Else
                    Async.ListView.AddItem(lv, "Reply from " & theIp.ToString & ": bytes=" & pingSize.ToString & " time=" & pingRep.RoundtripTime.ToString & "ms TTL=" & pingRep.Options.Ttl.ToString & vbNewLine)
                End If

                If pingRep.RoundtripTime > pingRespMax Then
                    pingRespMax = pingRep.RoundtripTime
                End If
                If pingRep.RoundtripTime < pingRespMin Then
                    pingRespMin = pingRep.RoundtripTime
                End If
                pingRespAvg += pingRep.RoundtripTime

                pingReceived += 1
            Else
                ' Failed
                Async.ListView.AddItem(lv, pingRep.Status.ToString)
                pingLost += 1
            End If
        Next

        ' Average rt
        If pingReceived <> 0 Then
            pingRespAvg = pingRespAvg \ pingReceived
        End If

        Async.ListView.AddItem(lv, "")
        Async.ListView.AddItem(lv, "Ping statistics for " & theIp.ToString)
        Async.ListView.AddItem(lv, "   Packets: Sent = " & pingSent.ToString & ", Received = " & pingReceived.ToString & ", Lost = " & pingLost.ToString & "  (" & Misc.GetFormatedPercentage(pingLost / pingSent, 0, True) & "% loss)")
        If pingReceived > 0 Then
            Async.ListView.AddItem(lv, "Approximate round trip times in milli-seconds:")
            Async.ListView.AddItem(lv, "   Minimum = " & pingRespMin.ToString & "ms, Maximum = " & pingRespMax.ToString & "ms, Average = " & pingRespAvg.ToString & "ms")
        End If

        ' Release ping object
        pinger.Dispose()
    End Sub

    ' WhoIs (sync)
    Public Sub WhoIs(ByVal lv As ListView)
        Async.ListView.AddItem(lv, "Not yet implemented...")
    End Sub

    ' TraceRoute (sync)
    Public Sub TraceRoute(ByVal lv As ListView)
        Async.ListView.AddItem(lv, "Not yet implemented...")
    End Sub

#End Region

#Region "All actions on network (close tcp connection)"

    ' Unload module
    Private _closeTCP As asyncCallbackNetworkCloseConnection
    Public Function CloseTCP() As Integer

        If _closeTCP Is Nothing Then
            _closeTCP = New asyncCallbackNetworkCloseConnection(New asyncCallbackNetworkCloseConnection.HasClosedConnection(AddressOf closeTCPDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _closeTCP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackNetworkCloseConnection.poolObj(Me.Infos.Local, Me.Infos.Remote, newAction))

    End Function
    Private Sub closeTCPDone(ByVal Success As Boolean, ByVal local As IPEndPoint, ByVal remote As IPEndPoint, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Dim s As String = ""
            If local IsNot Nothing Then
                s = local.ToString
            End If
            Misc.ShowError("Could not close TCP connection " & s & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub


#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As networkInfos)
        _networkInfos.Merge(Thr)
    End Sub

    ' Parse port text file
    Public Shared Sub ParsePortTextFile()
        Common.Misc.ParsePortTextFiles(My.Application.Info.DirectoryPath & "\tcp.txt", _
                                My.Application.Info.DirectoryPath & "\udp.txt", _
                                _dicoTcp, _dicoUdp)
    End Sub

#Region "Get information overriden methods"

    Private Function getRemotePort() As Integer
        If Me.Infos.Remote IsNot Nothing Then
            Return Me.Infos.Remote.Port
        Else
            Return 0
        End If
    End Function
    Private Function getLocalPort() As Integer
        If Me.Infos.Local IsNot Nothing Then
            Return Me.Infos.Local.Port
        Else
            Return 0
        End If
    End Function

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return networkInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        Static oldRemotePort As Integer = getRemotePort()
        Static oldRemPortD As String = Nothing
        Static oldLocalPort As Integer = getLocalPort()
        Static oldLocPortD As String = Nothing

        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Local"
                If Me.Infos.Local IsNot Nothing Then
                    If Len(Me.Infos.LocalString) > 0 Then
                        res = Me.Infos.Local.ToString & "  ----  " & Me.Infos.LocalString
                    Else
                        res = Me.Infos.Local.ToString
                    End If
                Else
                    res = "0.0.0.0"
                End If
            Case "Remote"
                If Me.Infos.Remote IsNot Nothing Then
                    If Len(Me.Infos.RemoteString) > 0 Then
                        res = Me.Infos.Remote.ToString & "  ----  " & Me.Infos.RemoteString
                    Else
                        res = Me.Infos.Remote.ToString
                    End If
                End If
            Case "Protocol"
                res = Me.Infos.Protocol.ToString.ToUpperInvariant
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "Process"
                res = Me.Infos.ProcessName & " (" & Me.Infos.ProcessId.ToString & ")"
            Case "State"
                If Me.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                    res = Me.Infos.State.ToString
                End If
            Case "LocalPortDescription"
                If getLocalPort() <> oldLocalPort OrElse oldLocPortD Is Nothing Then
                    oldLocPortD = GetPortDescription(getLocalPort, Me.Infos.Protocol)
                    oldLocalPort = getLocalPort()
                End If
                res = oldLocPortD
            Case "RemotePortDescription"
                If getRemotePort() <> oldRemotePort OrElse oldRemPortD Is Nothing Then
                    oldRemPortD = GetPortDescription(getRemotePort, Me.Infos.Protocol)
                    oldRemotePort = getRemotePort()
                End If
                res = oldRemPortD
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Local As String = ""
        Static _old_Protocol As String = ""
        Static _old_Remote As String = ""
        Static _old_ProcessId As String = ""
        Static _old_State As String = ""
        Static _old_Process As String = ""
        Static _old_LocalPortDescription As String = ""
        Static _old_RemotePortDescription As String = ""
        Static oldRemotePort As Integer = getRemotePort()
        Static oldRemPortD As String = Nothing
        Static oldLocalPort As Integer = getLocalPort()
        Static oldLocPortD As String = Nothing

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Local"
                If Me.Infos.Local IsNot Nothing Then
                    If Len(Me.Infos.LocalString) > 0 Then
                        res = Me.Infos.Local.ToString & "  ----  " & Me.Infos.LocalString
                    Else
                        res = Me.Infos.Local.ToString
                    End If
                Else
                    res = "0.0.0.0"
                End If
                If res = _old_Local Then
                    hasChanged = False
                Else
                    _old_Local = res
                End If
            Case "Remote"
                If Me.Infos.Remote IsNot Nothing Then
                    If Len(Me.Infos.RemoteString) > 0 Then
                        res = Me.Infos.Remote.ToString & "  ----  " & Me.Infos.RemoteString
                    Else
                        res = Me.Infos.Remote.ToString
                    End If
                    If res = _old_Remote Then
                        hasChanged = False
                    Else
                        _old_Remote = res
                    End If
                Else
                    res = ""
                    hasChanged = (res <> _old_Remote)
                    _old_Remote = res
                End If
            Case "Protocol"
                res = Me.Infos.Protocol.ToString.ToUpperInvariant
                If res = _old_Protocol Then
                    hasChanged = False
                Else
                    _old_Protocol = res
                End If
            Case "Process"
                res = Me.Infos.ProcessName & " (" & Me.Infos.ProcessId.ToString & ")"
                If res = _old_Process Then
                    hasChanged = False
                Else
                    _old_Process = res
                End If
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
                If res = _old_ProcessId Then
                    hasChanged = False
                Else
                    _old_ProcessId = res
                End If
            Case "State"
                If Me.Infos.State > 0 Then
                    res = Me.Infos.State.ToString
                Else
                    res = ""
                End If
                If res = _old_State Then
                    hasChanged = False
                Else
                    _old_State = res
                End If
            Case "LocalPortDescription"
                If getLocalPort() <> oldLocalPort OrElse oldLocPortD Is Nothing Then
                    oldLocPortD = GetPortDescription(getLocalPort, Me.Infos.Protocol)
                    oldLocalPort = getLocalPort()
                End If
                res = oldLocPortD
                If res = _old_LocalPortDescription Then
                    hasChanged = False
                Else
                    _old_LocalPortDescription = res
                End If
            Case "RemotePortDescription"
                If getRemotePort() <> oldRemotePort OrElse oldRemPortD Is Nothing Then
                    oldRemPortD = GetPortDescription(getRemotePort, Me.Infos.Protocol)
                    oldRemotePort = getRemotePort()
                End If
                res = oldRemPortD
                If res = _old_RemotePortDescription Then
                    hasChanged = False
                Else
                    _old_RemotePortDescription = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

    Private Sub getHostNameLocal(ByVal obj As Object)
        If obj IsNot Nothing Then
            Dim hostEntry As IPHostEntry
            Dim ip As System.Net.IPEndPoint = DirectCast(obj, IPEndPoint)
            Try
                hostEntry = Dns.GetHostEntry(ip.Address)
            Catch ex As Exception
                Exit Sub
            End Try
            Me.Infos.LocalString = hostEntry.HostName
            _haveResolvedAnAddress = True
        End If
    End Sub
    Private Sub getHostNameRemote(ByVal obj As Object)
        If obj IsNot Nothing Then
            Dim hostEntry As IPHostEntry
            Dim ip As System.Net.IPEndPoint = DirectCast(obj, IPEndPoint)
            Try
                hostEntry = Dns.GetHostEntry(ip.Address)
            Catch ex As Exception
                Exit Sub
            End Try
            Me.Infos.RemoteString = hostEntry.HostName
            _haveResolvedAnAddress = True
        End If
    End Sub
End Class
