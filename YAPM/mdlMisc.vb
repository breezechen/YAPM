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

Module mdlMisc

    ' Copy content of a listview (selected items) into clipboard
    Public Sub CopyLvToClip(ByVal e As MouseEventArgs, ByVal lv As ListView)
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Dim s As String = vbNullString
            Dim it As ListViewItem
            Dim x As Integer = 0
            For Each it In lv.SelectedItems
                s &= it.Text
                Dim it2 As ListViewItem.ListViewSubItem
                For Each it2 In it.SubItems
                    s &= vbTab & vbTab & it2.Text
                Next
                x += 1
                If Not (x = lv.SelectedItems.Count) Then s &= vbNewLine
            Next
            If Not (s = vbNullString) Then My.Computer.Clipboard.SetText(s, TextDataFormat.UnicodeText)
        End If
    End Sub

End Module
