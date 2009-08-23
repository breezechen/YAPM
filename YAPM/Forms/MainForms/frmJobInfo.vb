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
Imports YAPM.Common.Misc

Public Class frmJobInfo

    Private WithEvents curJob As cJob

    Private WithEvents theConnection As cConnection
    Private _local As Boolean = True
    Private _notWMI As Boolean


    ' Refresh current tab
    Private Sub refreshJobTab()

        If curJob Is Nothing Then Exit Sub

        Select Case Me.tabProcess.SelectedTab.Text

            Case "General"


            Case "Statistics"


            Case "Limitations"


        End Select
    End Sub

    Private Sub frmServiceInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            Call tabProcess_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        ' Some tooltips


        'Select Case My.Settings.ServSelectedTab
        '    Case "General"
        '        Me.tabProcess.SelectedTab = Me.TabPage1
        '    Case "General - 2"
        '        Me.tabProcess.SelectedTab = Me.TabPage2
        '    Case "Dependencies"
        '        Me.tabProcess.SelectedTab = Me.tabDep
        '    Case "Informations"
        '        Me.tabProcess.SelectedTab = Me.TabPage6
        'End Select

        Call Connect()
        Call refreshJobTab()

    End Sub

    ' Get process to monitor
    Public Sub SetJob(ByRef job As cJob)

        curJob = job


        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cProcess.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        Me.Timer.Enabled = _local

    End Sub

    ' Change caption
    Private Sub ChangeCaption()
        'Me.Text = curServ.Infos.Name & " (" & curServ.Infos.DisplayName & ")"
    End Sub

    Private Sub tabProcess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabProcess.SelectedIndexChanged
        Call Me.refreshJobTab()
        Call ChangeCaption()
        'My.Settings.ServSelectedTab = Me.tabProcess.SelectedTab.Text
    End Sub

    ' Connection
    Public Sub Connect()
        ' Connect providers
        'theConnection.CopyFromInstance(Program.Connection)
        Try
            theConnection = Program.Connection
            theConnection.Connect()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Can not connect")
        End Try
    End Sub

    Private Sub theConnection_Connected() Handles theConnection.Connected
        '
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        '
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        ' Refresh informations about process
        Call Me.refreshJobTab()

        ' Display caption
        Call ChangeCaption()
    End Sub

End Class