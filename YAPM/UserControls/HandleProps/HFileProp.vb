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

Public Class HFileProp
    Inherits HXXXProp

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()
    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Dim bFileExists As Boolean = IO.File.Exists(Me.TheHandle.Infos.Name)
        Dim bDirExists As Boolean = IO.Directory.Exists(Me.TheHandle.Infos.Name)
        If bFileExists Then
            Me.lblFileExists.ForeColor = Color.DarkGreen
            Me.lblFileExists.Text = "File exists"
            Me.cmdDetails.Text = "  Show details"
            Me.cmdDetails.Image = My.Resources.magnifier
            Me.cmdDetails.Enabled = True
            Me.cmdOpen.Enabled = True
        Else
            If bDirExists Then
                Me.lblFileExists.ForeColor = Color.DarkGreen
                Me.lblFileExists.Text = "Directory exists"
                Me.cmdDetails.Enabled = True
                Me.cmdDetails.Text = "    Open directory"
                Me.cmdDetails.Image = My.Resources.folder_open
                Me.cmdOpen.Enabled = True
            Else
                Me.lblFileExists.ForeColor = Color.DarkRed
                Me.lblFileExists.Text = "Unknown file"
                Me.cmdDetails.Enabled = False
                Me.cmdDetails.Text = "  Show details"
                Me.cmdDetails.Image = My.Resources.magnifier
                Me.cmdOpen.Enabled = False
            End If
        End If
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        cFile.ShowFileProperty(Me.TheHandle.Infos.Name, Me.Handle)
    End Sub

    Private Sub cmdDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDetails.Click
        If Me.cmdDetails.Text.Contains("directory") = False Then
            ' This is a file
            Common.Misc.DisplayDetailsFile(Me.TheHandle.Infos.Name)
        Else
            ' This is a directory
            cFile.OpenADirectory(Me.TheHandle.Infos.Name)
        End If
    End Sub

    Private Sub HFileProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Common.Misc.SetToolTip(Me.cmdDetails, "Details about the object")
        Common.Misc.SetToolTip(Me.cmdOpen, "Open properties of item")
    End Sub
End Class
