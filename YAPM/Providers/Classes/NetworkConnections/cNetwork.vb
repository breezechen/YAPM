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

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private nullAddress As New IPAddress(0)     ' For address comparison

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
        Return asyncCallbackNetworkCloseConnection.CloseTcpConnection(local, remote)
    End Function

    ' Retrieve description of a port
    Public Shared Function GetPortDescription(ByVal port As Integer, ByVal protocol As API.NetworkProtocol) As String
        If port = 0 Then
            Return ""
        End If
        If protocol = API.NetworkProtocol.Tcp Then
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

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackNetworkCloseConnection.poolObj(Me.Infos.Local, Me.Infos.Remote, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub closeTCPDone(ByVal Success As Boolean, ByVal local As IPEndPoint, ByVal remote As IPEndPoint, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            'MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
            '       "Could not close TCP connection " & local.ToString)
        End If
        RemovePendingTask(actionNumber)
    End Sub


#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As API.LightConnection)
        _networkInfos.Merge(Thr)
    End Sub

    ' Parse port text file
    Public Shared Sub ParsePortTextFile()
        Call ParsePortTextFiles(My.Application.Info.DirectoryPath & "\tcp.txt", _
                                My.Application.Info.DirectoryPath & "\udp.txt", _
                                _dicoTcp, _dicoUdp)
    End Sub

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        Static oldRemotePort As Integer = Me.Infos.Remote.Port
        Static oldRemPortD As String = Nothing
        Static oldLocalPort As Integer = Me.Infos.Local.Port
        Static oldLocPortD As String = Nothing

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            Return PendingTaskCount.ToString
        End If

        Dim res As String = NO_INFO_RETRIEVED
        Select Case info
            Case "Local"
                If Me.Infos.Local IsNot Nothing Then
                    If Len(Me.Infos._localString) > 0 Then
                        res = Me.Infos.Local.ToString & "  ----  " & Me.Infos._localString
                    Else
                        res = Me.Infos.Local.ToString
                    End If
                Else
                    res = "0.0.0.0"
                End If
            Case "Remote"
                If Me.Infos.Remote IsNot Nothing Then
                    If Len(Me.Infos._remoteString) > 0 Then
                        res = Me.Infos.Remote.ToString & "  ----  " & Me.Infos._remoteString
                    Else
                        res = Me.Infos.Remote.ToString
                    End If
                End If
            Case "Protocol"
                res = Me.Infos.Protocol.ToString.ToUpperInvariant
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "State"
                If Me.Infos.State > 0 Then
                    res = Me.Infos.State.ToString
                End If
            Case "LocalPortDescription"
                If Me.Infos.Local.Port <> oldLocalPort OrElse oldLocPortD Is Nothing Then
                    res = GetPortDescription(Me.Infos.Local.Port, Me.Infos.Protocol)
                Else
                    res = oldLocPortD
                End If
            Case "RemotePortDescription"
                If Me.Infos.Remote.Port <> oldRemotePort OrElse oldRemPortD Is Nothing Then
                    res = GetPortDescription(Me.Infos.Remote.Port, Me.Infos.Protocol)
                Else
                    res = oldRemPortD
                End If
        End Select

        Return res
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
            Me.Infos._localString = hostEntry.HostName
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
            Me.Infos._remoteString = hostEntry.HostName
        End If
    End Sub
End Class
