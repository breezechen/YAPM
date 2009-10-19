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
Imports Common.Misc

Public Class frmHandleInfo

    Private WithEvents curHandle As cHandle

    Private WithEvents theConnection As cConnection
    Private _local As Boolean = True
    Private _notWMI As Boolean

    ' Detailed informations' usercontrol
    Private _ctrlDetails As HXXXProp


    ' Refresh current tab
    Private Sub refreshServiceTab()

        If curHandle Is Nothing Then Exit Sub

        Select Case Me.tabProcess.SelectedTab.Text

            Case "Details"
                Me.txtAccess.Text = curHandle.GetInformation("GrantedAccess")
                Me.txtAddress.Text = curHandle.GetInformation("ObjectAddress")
                Me.txtName.Text = curHandle.GetInformation("Name")
                Me.txtType.Text = curHandle.GetInformation("Type")
                Me.lblHandleCount.Text = curHandle.GetInformation("HandleCount")
                Me.lblNonPaged.Text = curHandle.GetInformation("NonPagedPoolUsage")
                Me.lblPaged.Text = curHandle.GetInformation("PagedPoolUsage")
                Me.lblObjectCount.Text = curHandle.GetInformation("ObjectCount")
                Me.lblPointerCount.Text = curHandle.GetInformation("PointerCount")

                ' Refresh infos on the custom usercontrol
                If _ctrlDetails IsNot Nothing Then
                    _ctrlDetails.RefreshInfos()
                Else
                    Me.gpCustomControl.Enabled = False
                End If

            Case Else
                '

        End Select
    End Sub

    Private Sub frmServiceInfo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.F5 Then
            Call refreshServiceTab()
        End If
    End Sub

    Private Sub frmProcessInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        ' Some tooltips


        Call Connect()
        Call refreshServiceTab()

    End Sub

    ' Get process to monitor
    Public Sub SetHandle(ByRef handle As cHandle)

        curHandle = handle

        Me.Text = "Handle " & curHandle.Infos.Handle.ToString

        _local = (cProcess.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (cProcess.Connection.ConnectionObj.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)

        ' Add a custom usercontrol to our form depending of the handle type
        Select Case handle.Infos.Type
            Case "Adapter"

            Case "AlpcPort"

            Case "Callback"

            Case "Controller"

            Case "DebugObject"

            Case "Desktop"

            Case "Device"

            Case "Directory"

            Case "Driver"

            Case "EtwRegistration"

            Case "Event"

            Case "EventPair"

            Case "File"
                _ctrlDetails = New HFileProp(curHandle)
            Case "FilterCommunicationPort"

            Case "FilterConnectionPort"

            Case "IoCompletion"

            Case "Job"
                _ctrlDetails = New HJobProp(curHandle)
            Case "Key"
                _ctrlDetails = New HKeyProp(curHandle)
            Case "KeyedEvent"

            Case "Mutant"

            Case "Process"
                _ctrlDetails = New HProcessProp(curHandle)
            Case "Profile"

            Case "Section"

            Case "Semaphore"

            Case "Session"

            Case "SymbolicLink"

            Case "Thread"
                _ctrlDetails = New HThreadProp(curHandle)
            Case "Timer"

            Case "TmEn"

            Case "TmRm"

            Case "TmTm"

            Case "TmTx"

            Case "Token"

            Case "TpWorkerFactory"

            Case "Type"

            Case "WindowStation"

            Case "WmiGuid"

        End Select

        If _ctrlDetails IsNot Nothing Then
            Me.gpCustomControl.Controls.Add(_ctrlDetails)
            _ctrlDetails.Dock = DockStyle.Fill
        End If

    End Sub

    ' Connection
    Public Sub Connect()
        ' 
    End Sub

End Class