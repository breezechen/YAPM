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
        _connection = Connection
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.NetworkConnection

        ' Solve DNS
        Try
            If Me.Infos._Local.Address.Equals(nullAddress) = False Then
                Dim t As New System.Threading.WaitCallback(AddressOf getHostNameLocal)
                Call Threading.ThreadPool.QueueUserWorkItem(t, Me.Infos.Local)
            End If
        Catch ex As Exception
            '
        End Try
        Try
            If Me.Infos._remote IsNot Nothing AndAlso Me.Infos._remote.Address.Equals(nullAddress) = False Then
                Dim t As New System.Threading.WaitCallback(AddressOf getHostNameRemote)
                Call Threading.ThreadPool.QueueUserWorkItem(t, Me.Infos.Remote)
            End If
        Catch ex As Exception
            '
        End Try
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
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not close TCP connection " & s)
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
                End If
                If res = _old_Remote Then
                    hasChanged = False
                Else
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
