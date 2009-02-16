' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Public Class frmHexEditor

    Private WithEvents _hex As New MemoryHexEditor.control
    Private _pid As Integer
    Private _region As MemoryHexEditor.control.MemoryRegion

    Public Sub SetPidAndRegion(ByVal pid As Integer, ByVal region As MemoryHexEditor.control.MemoryRegion)
        _pid = pid
        _region = region
    End Sub

    Private Sub frmHexEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With _hex
            .BackColor = Color.White
            .Location = New Point(0, 0)
            .Size = New Size(Me.Width, Me.Height)
            .Dock = DockStyle.Left
        End With

        Me.Controls.Add(_hex)

        _hex.NewProc(_region, _pid)
    End Sub
End Class