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

Public Class HJobProp
    Inherits HXXXProp

    ' Associated job
    Private _job As cJob

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()

        ' Try to get the job
        _job = cJob.GetJobByName(Me.TheHandle.Infos.Name)
        
    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Me.cmdOpen.Enabled = (_job IsNot Nothing)
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Dim frm As New frmJobInfo
        frm.SetJob(_job)
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub HKeyProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.SetToolTip(Me.cmdOpen, "Show details about the job")
    End Sub
End Class
