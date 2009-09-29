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

Public Class frmNewVersionAvailable

    Private _infos As cUpdate.NewReleaseInfos

    Public Sub New(ByVal infos As cUpdate.NewReleaseInfos)
        InitializeComponent()
        _infos = infos
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmAboutG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        SetToolTip(Me.cmdDownload, "Download the new version now")
        SetToolTip(Me.btnOK, "Close this window")

        With _infos
            Dim desc As String = .Description
            If desc IsNot Nothing Then
                desc = desc.Replace(Chr(10), vbNewLine)
            End If
            Me.lblVersion.Text = .Version
            Me.lblDate.Text = .Date
            Me.txtDesc.Text = desc
            Me.lblCaption.Text = .Caption
            Me.lblType.Text = .Type
        End With

    End Sub

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        cFile.ShellOpenFile(_infos.Url, Me.Handle)
    End Sub
End Class