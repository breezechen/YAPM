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
        Dim _local As Boolean = (cHandle.Connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection)

        bFileExists = bFileExists And _local
        bDirExists = bDirExists And _local

        Me.cmdFileDetails.Enabled = bFileExists
        Me.cmdOpenDirectory.Enabled = bDirExists Or bFileExists
        Me.cmdOpen.Enabled = Me.cmdOpenDirectory.Enabled

        If _local Then
            If bFileExists Then
                Me.lblFileExists.ForeColor = Color.DarkGreen
                Me.lblFileExists.Text = "File exists"
            Else
                If bDirExists Then
                    Me.lblFileExists.ForeColor = Color.DarkGreen
                    Me.lblFileExists.Text = "Directory exists"
                Else
                    Me.lblFileExists.ForeColor = Color.DarkRed
                    Me.lblFileExists.Text = "Unknown file"
                End If
            End If
        Else
            Me.lblFileExists.ForeColor = Color.Black
            Me.lblFileExists.Text = "Remote file"
        End If
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        cFile.ShowFileProperty(Me.TheHandle.Infos.Name, Me.Handle)
    End Sub

    Private Sub cmdOpenDirectory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpenDirectory.Click
        If IO.Directory.Exists(Me.TheHandle.Infos.Name) Then
            cFile.OpenADirectory(Me.TheHandle.Infos.Name)
        Else
            cFile.OpenDirectory(Me.TheHandle.Infos.Name)
        End If
    End Sub

    Private Sub HFileProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Common.Misc.SetToolTip(Me.cmdDetails, "Details about the object")
        Common.Misc.SetToolTip(Me.cmdOpen, "Open properties of item")
        Common.Misc.SetToolTip(Me.cmdOpenDirectory, "Open directory")
    End Sub

    Private Sub cmdFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFileDetails.Click
        Common.Misc.DisplayDetailsFile(Me.TheHandle.Infos.Name)
    End Sub
End Class
