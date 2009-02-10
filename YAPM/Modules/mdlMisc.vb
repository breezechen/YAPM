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

    Private sizeUnits() As String = {"Bytes", "KB", "MB", "GB", "TB", "PB", "EB"}


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

    ' Replace (or not) taskmgr
    Public Sub ReplaceTaskmgr(ByVal value As Boolean)
        Try
            Dim regKey As RegistryKey
            regKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", True)

            If value Then
                Try
                    regKey.CreateSubKey("taskmgr.exe").SetValue("debugger", Application.ExecutablePath)
                Catch ex As Exception
                    '
                End Try
            Else
                Try
                    regKey.DeleteSubKey("taskmgr.exe")
                Catch ex As Exception
                    '
                End Try
            End If
        Catch ex As Exception
            '
        End Try
    End Sub

    ' Get a formated value as a string (in Bytes, KB, MB or GB) from an Integer
    Public Function GetFormatedSize(ByVal size As Integer, Optional ByVal digits As Integer = 3) As String
        Return GetFormatedSize(New Decimal(size), digits)
    End Function
    Public Function GetFormatedSize(ByVal size As ULong, Optional ByVal digits As Integer = 3) As String
        Return GetFormatedSize(New Decimal(size), digits)
    End Function
    Public Function GetFormatedSize(ByVal size As UInteger, Optional ByVal digits As Integer = 3) As String
        Return GetFormatedSize(New Decimal(size), digits)
    End Function
    Public Function GetFormatedSize(ByVal size As Decimal, Optional ByVal digits As Integer = 3) As String
        Dim t As Decimal = size
        Dim dep As Integer = 0

        While t >= 1024
            t /= 1024
            dep += 1
        End While

        Return CStr(Math.Round(t, digits)) & " " & sizeUnits(dep)

    End Function


    ' Get a formated percentage
    Public Function GetFormatedPercentage(ByVal p As Double, Optional ByVal digits As Integer = 3) As String
        Dim d100 As Double = p * 100
        Dim d As Double = Math.Round(d100, digits)
        Dim tr As Double = Math.Truncate(d)
        Dim lp As Integer = CInt(tr)
        Dim rp As Integer = CInt((d100 - tr) * 10 ^ digits)

        Return CStr(IIf(lp < 10, "0", "")) & CStr(lp) & "." & CStr(IIf(rp < 10, "00", "")) & CStr(IIf(rp < 100 And rp >= 10, "0", "")) & CStr(rp)
    End Function

End Module
