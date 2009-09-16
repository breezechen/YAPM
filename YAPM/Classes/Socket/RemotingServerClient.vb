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

' This code is based on a work from marcel heeremans : 
' http://www.codeproject.com/KB/IP/TwoWayRemoting.aspx?msg=3199726#xx3199726xx
' which is under the Code Project Open License (CPOL) 1.02
' Please refer to license.rtf for details about this license.

Option Strict On

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.Remoting.Messaging
Imports System.Threading

Namespace RemotingServerClient
    Public Delegate Sub delUserInfo(ByVal UserID As String)
    Public Delegate Sub delCommsInfo(ByVal info As CommsInfo)

    ' This class is created on the server and allows for client to register their existance and
    ' a call-back that the server can use to communicate back.
    Public Class ServerTalk
        Inherits MarshalByRefObject
        Private Shared _NewUser As delUserInfo
        Private Shared _ClientToHost As delCommsInfo
        Private Shared _list As New List(Of ClientWrap)()

        ' Unlimited lifetime
        Public Overrides Function InitializeLifetimeService() As Object
            Return Nothing
        End Function

        Public Sub RegisterHostToClient(ByVal UserID As String, ByVal htc As delCommsInfo)
            _list.Add(New ClientWrap(UserID, htc))
            _NewUser(UserID)
        End Sub

        ''' <summary>
        ''' The host should register a function pointer to which it wants a signal
        ''' send when a User Registers
        ''' </summary>
        Public Shared Property NewUser() As delUserInfo
            Get
                Return _NewUser
            End Get
            Set(ByVal value As delUserInfo)
                _NewUser = value
            End Set
        End Property

        ''' <summary>
        ''' The host should register a function pointer to which it wants the CommsInfo object
        ''' send when the client wants to communicate to the server
        ''' </summary>
        Public Shared Property ClientToHost() As delCommsInfo
            Get
                Return _ClientToHost
            End Get
            Set(ByVal value As delCommsInfo)
                _ClientToHost = value
            End Set
        End Property

        ' the static method that will be invoked by the server when it wants to send a message
        ' to a specific user or all of them.
        Public Shared Sub RaiseHostToClient(ByVal UserID As String, ByVal Message As String)
            For Each client As ClientWrap In _list
                If (client.UserID = UserID OrElse UserID = "*") AndAlso client.HostToClient IsNot Nothing Then
                    Dim D As delCommsInfo = client.HostToClient
                    D(New CommsInfo(Message))
                End If
            Next
        End Sub
        Public Shared Sub RaiseHostToClient(ByVal UserID As String, ByVal data As Byte())
            For Each client As ClientWrap In _list
                If (client.UserID = UserID OrElse UserID = "*") AndAlso client.HostToClient IsNot Nothing Then
                    Dim D As delCommsInfo = client.HostToClient
                    D(New CommsInfo(data))
                End If
            Next
        End Sub

        ' a thread-safe queue that will contain any message objects that should
        ' be send to the server
        Private Shared _ClientToServer As Queue = Queue.Synchronized(New Queue())

        ' this instance method allows a client to send a message to the server
        Public Sub SendMessageToServer(ByVal Message As CommsInfo)
            _ClientToServer.Enqueue(Message)
        End Sub

        Public Shared ReadOnly Property ClientToServerQueue() As Queue
            Get
                Return _ClientToServer
            End Get
        End Property

        ' small private class to wrap the User and the callback together.
        Private Class ClientWrap
            Private _UserID As String = ""
            Private _HostToClient As delCommsInfo = Nothing

            Public Sub New(ByVal UserID As String, ByVal HostToClient As delCommsInfo)
                _UserID = UserID
                _HostToClient = HostToClient
            End Sub

            Public ReadOnly Property UserID() As String
                Get
                    Return _UserID
                End Get
            End Property

            Public ReadOnly Property HostToClient() As delCommsInfo
                Get
                    Return _HostToClient
                End Get
            End Property
        End Class
    End Class

    <Serializable()> _
    Public Class CommsInfo
        Private _Message As String = ""
        Private _Data As Byte()

        Public Sub New(ByVal Message As String)
            _Message = Message
        End Sub
        Public Sub New(ByVal data() As Byte)
            _Data = data
        End Sub

        Public Property Data() As Byte()
            Get
                Return _Data
            End Get
            Set(ByVal value As Byte())
                _Data = value
            End Set
        End Property
        Public Property Message() As String
            Get
                Return _Message
            End Get
            Set(ByVal value As String)
                _Message = value
            End Set
        End Property
    End Class

    ''' <summary>
    ''' This CallbackSink object will be 'anchored' on the client and is used as the target for a callback
    ''' given to the server.
    ''' </summary>
    Public Class CallbackSink
        Inherits MarshalByRefObject
        Public Event OnHostToClient As delCommsInfo

        Public Sub New()
        End Sub

        <OneWay()> _
        Public Sub HandleToClient(ByVal info As CommsInfo)
            RaiseEvent OnHostToClient(info)
        End Sub
    End Class

End Namespace