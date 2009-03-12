' =======================================================
' Yet Another Process Monitor (YAPM)
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

Public Class frmBasedStateAction

    Public Const BASED_STATE_ACTION As String = "hotkeys.xml"
    Private atxtKey As Integer = -1

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Private Sub frmBasedStateAction_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' Save to XML
        writeXML()
    End Sub

    Private Sub frmBasedStateAction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetWindowTheme(lv.Handle, "explorer", Nothing)

        Me.cbAction.Items.Clear()
        For Each i As String In frmMain.emStateBasedActions.ActionsAvailable
            If i IsNot Nothing Then
                Me.cbAction.Items.Add(i)
            End If
        Next

        ' Read collection and add items
        'For Each ht As cShortcut In frmMain.emStateBasedActions.StateBasedActionCollection
        '    ' Add hotkey
        '    Dim skeys As String = CType(ht.Key1, cShortcut.ShorcutKeys).ToString & " + " & _
        '        CType(ht.Key2, cShortcut.ShorcutKeys).ToString & " + " & _
        '        CType(ht.Key3, cShortcut.ShorcutKeys).ToString
        '    Dim it As New ListViewItem(skeys)
        '    it.Tag = ht
        '    If ht.Enabled = False Then
        '        it.ForeColor = Color.Gray
        '    End If
        '    it.SubItems.Add(Me.cbAction.Items(ht.Action - 1).ToString)
        '    it.ImageKey = "default"
        '    Me.lv.Items.Add(it)
        'Next

        ' Fill comboboxes
        cbAction.Items.Clear()
        For Each s As String In frmMain.emStateBasedActions.ActionsAvailable
            cbAction.Items.Add(s)
        Next
        cbCounter.Items.Clear()
        For Each s As String In frmMain.emStateBasedActions.CounterAvailables
            cbCounter.Items.Add(s)
        Next

    End Sub

    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
        gp.Visible = True
    End Sub

    Private Sub cmdKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKO.Click
        gp.Visible = False
    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                ' Remove or ?
                Dim sKey As String = CType(it.Tag, cShortcut).Key
                If frmMain.emStateBasedActions.RemoveStateBasedAction(sKey) Then
                    it.Remove()
                End If
            End If
        Next
    End Sub

    Private Sub EnableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                CType(it.Tag, cShortcut).Enabled = True
                it.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                CType(it.Tag, cShortcut).Enabled = False
                it.ForeColor = Color.Gray
            End If
        Next
    End Sub

    ' Read from XML file
    Public Sub readHotkeysFromXML()
        'Dim XmlDoc As XmlDocument = New XmlDocument
        'Dim element As XmlNodeList
        'Dim noeud, noeudEnf As XmlNode

        '<hotkeys>
        '	<key>
        '		<enabled>true</enabled>
        '		<key1>65</key1>
        '		<key2>16</key2>
        '		<key3>17</key3>
        '		<action>2</action>
        '	</key>
        '</hotkeys>

        'Try
        '    Call XmlDoc.Load(My.Application.Info.DirectoryPath & "\" & BASED_STATE_ACTION)
        '    element = XmlDoc.DocumentElement.GetElementsByTagName("key")

        '    For Each noeud In element

        '        Dim key1 As Integer
        '        Dim key2 As Integer
        '        Dim key3 As Integer
        '        Dim action As Integer
        '        Dim enabled As Boolean

        '        For Each noeudEnf In noeud.ChildNodes

        '            If noeudEnf.LocalName = "enabled" Then
        '                enabled = CBool(noeudEnf.InnerText)
        '            ElseIf noeudEnf.LocalName = "key1" Then
        '                key1 = CInt(noeudEnf.InnerText)
        '            ElseIf noeudEnf.LocalName = "key2" Then
        '                key2 = CInt(noeudEnf.InnerText)
        '            ElseIf noeudEnf.LocalName = "key3" Then
        '                key3 = CInt(noeudEnf.InnerText)
        '            ElseIf noeudEnf.LocalName = "action" Then
        '                action = CInt(noeudEnf.InnerText)
        '            End If
        '        Next

        '        Dim ht As New cShortcut(action, key1, key2, key3)
        '        ht.Enabled = enabled

        '        frmMain.emStateBasedActions.AddHotkey(ht)
        '    Next
        'Catch ex As Exception
        '    Trace.WriteLine("XML loading failed : " & ex.Message)
        'End Try
    End Sub

    ' Write to XML file
    Private Sub writeXML()

        '<hotkeys>
        '	<key>
        '		<enabled>true</enabled>
        '		<key1>65</key1>
        '		<key2>16</key2>
        '		<key3>17</key3>
        '		<action>2</action>
        '	</key>
        '</hotkeys>

        'Dim XmlDoc As XmlDocument = New XmlDocument()
        'XmlDoc.LoadXml("<hotkeys></hotkeys>")

        'For Each it As ListViewItem In Me.lv.Items

        '    Dim cs As cShortcut = CType(it.Tag, cShortcut)

        '    Dim elemConfig As XmlElement = XmlDoc.CreateElement("key")

        '    Dim elemEnabled As XmlElement
        '    elemEnabled = XmlDoc.CreateElement("enabled")
        '    elemEnabled.InnerText = CStr(cs.Enabled)
        '    elemConfig.AppendChild(elemEnabled)

        '    Dim elemKey1 As XmlElement
        '    elemKey1 = XmlDoc.CreateElement("key1")
        '    elemKey1.InnerText = CStr(cs.Key1)
        '    elemConfig.AppendChild(elemKey1)

        '    Dim elemKey2 As XmlElement
        '    elemKey2 = XmlDoc.CreateElement("key2")
        '    elemKey2.InnerText = CStr(cs.Key2)
        '    elemConfig.AppendChild(elemKey2)

        '    Dim elemKey3 As XmlElement
        '    elemKey3 = XmlDoc.CreateElement("key3")
        '    elemKey3.InnerText = CStr(cs.Key3)
        '    elemConfig.AppendChild(elemKey3)

        '    Dim elemAction As XmlElement
        '    elemAction = XmlDoc.CreateElement("action")
        '    elemAction.InnerText = CStr(cs.Action)
        '    elemConfig.AppendChild(elemAction)

        '    XmlDoc.DocumentElement.AppendChild(elemConfig)

        'Next

        'XmlDoc.Save(My.Application.Info.DirectoryPath & "\" & BASED_STATE_ACTION)
    End Sub

    ' Check if we can add the action
    Private Sub checkAddState()
        Me.cmdAdd.Enabled = False

        If (Me.chkCheckProcessID.Checked Or Me.chkCheckProcessName.Checked Or _
            Me.chkCheckProcessPath.Checked) = False Then Exit Sub
        If (cbAction.Text = "" Or cbCounter.Text = "" Or cbOperator.Text = "") Then Exit Sub
        If txtThreshold.Text = "" Then Exit Sub

        Me.cmdAdd.Enabled = True
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        'gp.Visible = False

        '' Add shortcut
        'Dim i As Integer = Me.cbAction.SelectedIndex + 1

        'If i <= 0 Then Exit Sub

        'Dim k1 As Integer = -1
        'Dim k2 As Integer = -1
        'Dim k3 As Integer = atxtKey

        'If Me.chkCtrl.Checked Then
        '    k1 = cShortcut.ShorcutKeys.VK_CONTROL
        '    If Me.chkShift.Checked Then
        '        k2 = cShortcut.ShorcutKeys.VK_SHIFT
        '    ElseIf Me.chkAlt.Checked Then
        '        k2 = cShortcut.ShorcutKeys.VK_MENU
        '    End If
        'ElseIf Me.chkShift.Checked Then
        '    k1 = cShortcut.ShorcutKeys.VK_SHIFT
        '    If Me.chkAlt.Checked Then
        '        k2 = cShortcut.ShorcutKeys.VK_MENU
        '    End If
        'ElseIf Me.chkAlt.Checked Then
        '    k1 = cShortcut.ShorcutKeys.VK_MENU
        'End If

        'If k1 + k2 + k3 = -3 Then Exit Sub

        'Dim ht As New cShortcut(i, k1, k2, k3)
        'If frmMain.emStateBasedActions.AddHotkey(ht) Then
        '    ' Add hotkey
        '    Dim skeys As String = CType(k1, cShortcut.ShorcutKeys).ToString & " + " & _
        '        CType(k2, cShortcut.ShorcutKeys).ToString & " + " & _
        '        CType(k3, cShortcut.ShorcutKeys).ToString
        '    Dim it As New ListViewItem(skeys)
        '    it.Tag = ht
        '    it.SubItems.Add(Me.cbAction.Text)
        '    it.ImageKey = "default"
        '    Me.lv.Items.Add(it)
        'End If
    End Sub

    Private Sub chkCheckProcessName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckProcessName.CheckedChanged
        Call checkAddState()
    End Sub

    Private Sub chkCheckProcessID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckProcessID.CheckedChanged
        Call checkAddState()
    End Sub

    Private Sub chkCheckProcessPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckProcessPath.CheckedChanged
        Call checkAddState()
    End Sub

    Private Sub cbOperator_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOperator.SelectedIndexChanged
        Call checkAddState()
    End Sub

    Private Sub txtThreshold_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThreshold.TextChanged
        Call checkAddState()
    End Sub

    Private Sub cbCounter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCounter.SelectedIndexChanged
        Call checkAddState()
    End Sub

    Private Sub cbAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAction.SelectedIndexChanged
        Call checkAddState()
    End Sub

    Private Sub txtParam1Val_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtParam1Val.TextChanged
        Call checkAddState()
    End Sub

    Private Sub txtParam2Val_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtParam2Val.TextChanged
        Call checkAddState()
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class