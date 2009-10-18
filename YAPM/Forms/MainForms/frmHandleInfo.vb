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

        'Me.cmdShowFileDetails.Enabled = _local

    End Sub

    ' Connection
    Public Sub Connect()
        ' 
    End Sub

End Class