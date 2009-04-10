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

Public Delegate Sub DownloadCompleteHandler(ByVal sender As Object, ByVal e As DownloadDataCompletedEventArgs)
Public Delegate Sub DownloadProgressChangeHandler(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)

Public Class cDownload

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _WebClient As System.Net.WebClient
    Private _destination As String
    Private _DownloadURL As String
    Private bStoped As Boolean

    ' ========================================
    ' Public events
    ' ========================================
    Public Event CompleteCallback(ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    Public Event ProgressCallback As DownloadProgressChangeHandler


    ' ========================================
    ' Properties
    ' ========================================
    Public Property DownloadURL() As String
        Get
            Return Me._DownloadURL
        End Get
        Set(ByVal value As String)
            Me._DownloadURL = value
        End Set
    End Property
    Public Property Destination() As String
        Get
            Return Me._destination
        End Get
        Set(ByVal value As String)
            Me._destination = value
        End Set
    End Property

    ' ========================================
    ' Public functions
    ' ========================================
    Public Sub New(ByVal downloadURL As String, ByVal destination As String)

        _WebClient = New WebClient
        _destination = destination
        _DownloadURL = downloadURL
        bStoped = False

        AddHandler _WebClient.DownloadFileCompleted, AddressOf DownloadCompleteHandler
        AddHandler _WebClient.DownloadProgressChanged, AddressOf DownloadProgressChangedHandler

    End Sub

    Public Sub StartDownload()
        _WebClient.DownloadFileAsync(New Uri(_DownloadURL), _destination)
    End Sub

    Public Sub Cancel()
        _WebClient.CancelAsync()
        _WebClient = Nothing
        bStoped = True
    End Sub


    ' ========================================
    ' Public events
    ' ========================================
    Public Sub DownloadCompleteHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If Not (bStoped) Then
            RaiseEvent CompleteCallback(e)
        End If
    End Sub

    Public Sub DownloadProgressChangedHandler(ByVal sender As Object, ByVal e As Net.DownloadProgressChangedEventArgs)
        If Not (bStoped) Then
            RaiseEvent ProgressCallback(sender, e)
        End If
    End Sub

End Class