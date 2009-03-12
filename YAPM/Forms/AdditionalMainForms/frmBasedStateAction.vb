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
    Private _desc1() As String
    Private _desc2() As String

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Private Sub frmBasedStateAction_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ' Save to XML
        Call writeXML()
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
        For Each _it As cBasedStateActionState In frmMain.emStateBasedActions.StateBasedActionCollection
            ' Add hotkey
            Dim it As New ListViewItem(_it.ProcessText)
            it.Tag = _it
            If _it.Enabled = False Then
                it.ForeColor = Color.Gray
            End If
            it.SubItems.Add(_it.StateText)
            it.SubItems.Add(_it.ActionText)
            it.ImageKey = "default"
            Me.lv.Items.Add(it)
        Next

        ' Fill comboboxes
        cbAction.Items.Clear()
        For Each s As String In frmMain.emStateBasedActions.ActionsAvailable
            cbAction.Items.Add(s)
        Next
        cbCounter.Items.Clear()
        For Each s As String In frmMain.emStateBasedActions.CounterAvailables
            cbCounter.Items.Add(s)
        Next
        _desc1 = frmMain.emStateBasedActions.Param1Description
        _desc2 = frmMain.emStateBasedActions.Param2Description

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
                Dim sKey As String = CType(it.Tag, cBasedStateActionState).Key
                If frmMain.emStateBasedActions.RemoveStateBasedAction(sKey) Then
                    it.Remove()
                End If
            End If
        Next
    End Sub

    Private Sub EnableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                CType(it.Tag, cBasedStateActionState).Enabled = True
                it.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub DisableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
        For Each it As ListViewItem In Me.lv.SelectedItems
            If it.Selected Then
                CType(it.Tag, cBasedStateActionState).Enabled = False
                it.ForeColor = Color.Gray
            End If
        Next
    End Sub

    ' Read from XML file
    Public Sub readHotkeysFromXML()
        'Dim XmlDoc As XmlDocument = New XmlDocument
        'Dim element As XmlNodeList
        'Dim noeud, noeudEnf As XmlNode

        '<statebasedactions>
        '	<sbaction>
        '		<enabled>true</enabled>
        '		<checkprocname>true</checkprocname>
        '		<checkprocid>true</checkprocid>
        '		<checkprocpath>true</checkprocpath>
        '		<procname>yapm.exe</procname>
        '		<procid>654</procid>
        '		<procpath>c:\*</procpath>
        '		<counter>CpuUsage</counter>
        '		<operator>4</operator>
        '		<threshold>12.5</threshold>
        '		<action>Kill process</action>
        '		<param1></param1>
        '		<param2></param2>
        '	</sbaction>
        '</statebasedactions>

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

        '<statebasedactions>
        '	<sbaction>
        '		<enabled>true</enabled>
        '		<checkprocname>true</checkprocname>
        '		<checkprocid>true</checkprocid>
        '		<checkprocpath>true</checkprocpath>
        '		<procname>yapm.exe</procname>
        '		<procid>654</procid>
        '		<procpath>c:\*</procpath>
        '		<counter>CpuUsage</counter>
        '		<operator>4</operator>
        '		<threshold>12.5</threshold>
        '		<action>Kill process</action>
        '		<param1></param1>
        '		<param2></param2>
        '	</sbaction>
        '</statebasedactions>

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
        gp.Visible = False

        Dim _operator As cBasedStateActionState.STATE_OPERATOR
        Select Case cbOperator.Text
            Case "<"
                _operator = cBasedStateActionState.STATE_OPERATOR.less_than
            Case "<="
                _operator = cBasedStateActionState.STATE_OPERATOR.less_or_equal_than
            Case "="
                _operator = cBasedStateActionState.STATE_OPERATOR.equal
            Case ">"
                _operator = cBasedStateActionState.STATE_OPERATOR.greater_than
            Case ">="
                _operator = cBasedStateActionState.STATE_OPERATOR.greater_or_equal_than
            Case "!="
                _operator = cBasedStateActionState.STATE_OPERATOR.different_from
        End Select
        Dim _it As New cBasedStateActionState(Me.chkCheckProcessName.Checked, _
                                              Me.chkCheckProcessID.Checked, _
                                              Me.chkCheckProcessPath.Checked, _
                                              cbCounter.Text, _operator, txtThreshold.Text, _
                                              cbAction.Text, txtParam1Val.Text, _
                                              txtParam2Val.Text, txtProcessName.Text, _
                                              txtProcessID.Text, txtProcessPath.Text)
        If frmMain.emStateBasedActions.AddStateBasedAction(_it) Then
            ' Add hotkey
            Dim it As New ListViewItem(_it.ProcessText)
            it.Tag = _it.Key
            it.SubItems.Add(_it.StateText)
            it.SubItems.Add(_it.ActionText)
            it.ImageKey = "default"
            Me.lv.Items.Add(it)
        End If
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
End Class