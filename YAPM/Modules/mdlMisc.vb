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

    ' Set tool tips
    Public Sub SetToolTip(ByVal ctrl As Control, ByVal text As String)
        Dim tToolTip As ToolTip = New System.Windows.Forms.ToolTip
        With tToolTip
            .SetToolTip(ctrl, text)
            .IsBalloon = False
            .Active = True
        End With
    End Sub

    ' Custom input box
    Public Function CInputBox(ByVal text As String, ByVal title As String, Optional ByVal defaultValue As String = Nothing) As String
        Dim frm As New frmInput
        With frm
            .Text = title
            .lblMessage.Text = text
            .txtRes.Text = defaultValue
            .ShowDialog()
            Return .Result
        End With

    End Function

    ' Search on internet
    Public Sub SearchInternet(ByVal item As String, ByVal handle As IntPtr)
        cFile.ShellOpenFile(My.Settings.SearchEngine.Replace("ITEM", item), handle)
    End Sub

    ' Standard Vista message box
    Public Function ShowVistaMessage(ByVal Owner As IntPtr, Optional ByVal Title As String = "", _
                                     Optional ByVal HeaderMessage As String = "", _
                                     Optional ByVal Content As String = "", Optional ByVal Buttons As  _
                                     TaskDialogCommonButtons = TaskDialogCommonButtons.Ok, Optional _
                                     ByVal Icon As TaskDialogIcon = TaskDialogIcon.Information) _
                                     As Integer
        If IsWindowsVista() Then
            Dim dlg As New TaskDialog
            With dlg
                .WindowTitle = Title
                .Content = Content
                .MainInstruction = HeaderMessage
                .MainIcon = Icon
                .CommonButtons = Buttons
            End With
            Return dlg.Show(Owner)
        Else
            Return -1
        End If
    End Function

    ' Get available IPV-4 IP
    Public Function GetIpv4Ips() As String()
        Dim res() As String
        Dim x As Integer = -1
        ReDim res(x)
        Dim s() As System.Net.IPAddress = System.Net.Dns.GetHostAddresses(My.Computer.Name)
        For Each t As System.Net.IPAddress In s
            If t.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                x += 1
                ReDim Preserve res(x)
                res(x) = t.ToString
            End If
        Next
        Return res
    End Function

    ' Return Assembly GUID
    ' Could be used ??
    Public Function GetAppGuid() As String
        Dim assemblyGuid As Guid = Nothing
        Dim assemblyObjects As Object() = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(GetType(System.Runtime.InteropServices.GuidAttribute), True)
        If assemblyObjects.Length > 0 Then
            assemblyGuid = New Guid(DirectCast(assemblyObjects(0),  _
                                    System.Runtime.InteropServices.GuidAttribute).Value)
        End If
        Return assemblyGuid.ToString
    End Function

End Module
