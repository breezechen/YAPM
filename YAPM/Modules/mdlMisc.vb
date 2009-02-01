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

Imports Microsoft.Win32

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

    ' Copy content of a listbox (selected items) into clipboard
    Public Sub CopyLstToClip(ByVal e As MouseEventArgs, ByVal lv As ListBox)
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Dim s As String = vbNullString
            Dim it As String
            Dim x As Integer = 0
            For Each it In lv.SelectedItems
                s &= it
                x += 1
                If Not (x = lv.SelectedItems.Count) Then s &= vbNewLine
            Next
            If Not (s = vbNullString) Then My.Computer.Clipboard.SetText(s, TextDataFormat.UnicodeText)
        End If
    End Sub

    ' Start (or not) with windows startup
    Public Sub StartWithWindows(ByVal value As Boolean)
        Try
            Dim regKey As RegistryKey
            regKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)

            If value Then
                regKey.SetValue(Application.ProductName, Application.ExecutablePath)
            Else
                regKey.DeleteValue(Application.ProductName)
            End If
        Catch ex As Exception
            '
        End Try
    End Sub

    ' Get a formated value as a string (in Bytes, KB, MB or GB) from an Integer
    Public Function GetFormatedSize(ByVal size As Integer, Optional ByVal digits As Integer = 3) As String
        If size >= 1073741824 Then
            ' In GB
            Return CStr(Math.Round(size / 1073741824, digits)) & " GB"
        ElseIf size >= 1048576 Then
            ' In MB
            Return CStr(Math.Round(size / 1048576, digits)) & " MB"
        ElseIf size >= 1024 Then
            ' In KB
            Return CStr(Math.Round(size / 1024, digits)) & " KB"
        Else
            Return CStr(size) & " Bytes"
        End If
    End Function
    Public Function GetFormatedSize(ByVal size As Long, Optional ByVal digits As Integer = 3) As String
        If size >= 1073741824 Then
            ' In GB
            Return CStr(Math.Round(size / 1073741824, digits)) & " GB"
        ElseIf size >= 1048576 Then
            ' In MB
            Return CStr(Math.Round(size / 1048576, digits)) & " MB"
        ElseIf size >= 1024 Then
            ' In KB
            Return CStr(Math.Round(size / 1024, digits)) & " KB"
        Else
            Return CStr(size) & " Bytes"
        End If
    End Function
    Public Function GetFormatedSize(ByVal size As ULong, Optional ByVal digits As Integer = 3) As String
        If size >= 1073741824 Then
            ' In GB
            Return CStr(Math.Round(size / 1073741824, digits)) & " GB"
        ElseIf size >= 1048576 Then
            ' In MB
            Return CStr(Math.Round(size / 1048576, digits)) & " MB"
        ElseIf size >= 1024 Then
            ' In KB
            Return CStr(Math.Round(size / 1024, digits)) & " KB"
        Else
            Return CStr(size) & " Bytes"
        End If
    End Function
    Public Function GetFormatedSize(ByVal size As UInteger, Optional ByVal digits As Integer = 3) As String
        If size >= 1073741824 Then
            ' In GB
            Return CStr(Math.Round(size / 1073741824, digits)) & " GB"
        ElseIf size >= 1048576 Then
            ' In MB
            Return CStr(Math.Round(size / 1048576, digits)) & " MB"
        ElseIf size >= 1024 Then
            ' In KB
            Return CStr(Math.Round(size / 1024, digits)) & " KB"
        Else
            Return CStr(size) & " Bytes"
        End If
    End Function

End Module
