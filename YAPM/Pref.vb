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

Public Class Pref

    ' Options
    Public procIntervall As Integer
    Public serviceIntervall As Integer
    Public startup As Boolean
    Public startHidden As Boolean
    Public lang As String
    Public startJobs As Boolean
    Public startChkModules As Boolean
    Public topmost As Boolean
    Public firstTime As Boolean

    ' Open XML
    Public Sub Load()
        Dim XmlDoc As XmlDocument = New XmlDocument
        Dim element As XmlNodeList
        Dim noeud, noeudEnf As XmlNode

        Call XmlDoc.Load(frmMain.PREF_PATH)
        element = XmlDoc.DocumentElement.GetElementsByTagName("config")

        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "procintervall" Then
                    procIntervall = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "serviceintervall" Then
                    serviceIntervall = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "startup" Then
                    startup = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "starthidden" Then
                    startHidden = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "lang" Then
                    lang = noeudEnf.InnerText
                ElseIf noeudEnf.LocalName = "startjobs" Then
                    startJobs = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "startchkmodules" Then
                    startChkModules = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "topmost" Then
                    topmost = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "firsttime" Then
                    firstTime = CBool(noeudEnf.InnerText)
                End If
            Next
        Next

    End Sub

    ' Save XML
    Public Sub Save()

        Dim XmlDoc As XmlDocument = New XmlDocument()
        XmlDoc.LoadXml("<YAPM></YAPM>")
        Dim elemConfig As XmlElement
        elemConfig = XmlDoc.CreateElement("config")

        Dim elemProcIntervall As XmlElement
        elemProcIntervall = XmlDoc.CreateElement("procintervall")
        elemProcIntervall.InnerText = CStr(Me.procIntervall)
        elemConfig.AppendChild(elemProcIntervall)
        Dim elemServiceIntervall As XmlElement
        elemServiceIntervall = XmlDoc.CreateElement("serviceintervall")
        elemServiceIntervall.InnerText = CStr(Me.serviceIntervall)
        elemConfig.AppendChild(elemServiceIntervall)
        Dim elemStartup As XmlElement
        elemStartup = XmlDoc.CreateElement("startup")
        elemStartup.InnerText = CStr(Me.startup)
        elemConfig.AppendChild(elemStartup)
        Dim elemStartHidden As XmlElement
        elemStartHidden = XmlDoc.CreateElement("starthidden")
        elemStartHidden.InnerText = CStr(Me.startHidden)
        elemConfig.AppendChild(elemStartHidden)
        Dim elemLang As XmlElement
        elemLang = XmlDoc.CreateElement("lang")
        elemLang.InnerText = Me.lang
        elemConfig.AppendChild(elemLang)
        Dim elemStartJobs As XmlElement
        elemStartJobs = XmlDoc.CreateElement("startjobs")
        elemStartJobs.InnerText = CStr(Me.startJobs)
        elemConfig.AppendChild(elemStartJobs)
        Dim elemModules As XmlElement
        elemModules = XmlDoc.CreateElement("startchkmodules")
        elemModules.InnerText = CStr(Me.startChkModules)
        elemConfig.AppendChild(elemModules)
        Dim elemTopMost As XmlElement
        elemTopMost = XmlDoc.CreateElement("topmost")
        elemTopMost.InnerText = CStr(Me.topmost)
        elemConfig.AppendChild(elemTopMost)
        Dim elemFirstTime As XmlElement
        elemFirstTime = XmlDoc.CreateElement("firsttime")
        elemFirstTime.InnerText = CStr(Me.firstTime)
        elemConfig.AppendChild(elemFirstTime)
        XmlDoc.DocumentElement.AppendChild(elemConfig)
        XmlDoc.Save(frmMain.PREF_PATH)
    End Sub

    ' Apply pref
    Public Sub Apply()
        Static first As Boolean = True
        frmMain.timerProcess.Interval = CInt(IIf(procIntervall > 0, procIntervall, frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES))
        frmMain.timerServices.Interval = CInt(IIf(serviceIntervall > 0, serviceIntervall, frmMain.DEFAULT_TIMER_INTERVAL_SERVICES))
        If first Then
            first = False
            frmMain.TopMost = topmost
            frmMain.chkModules.Checked = startChkModules
            frmMain.bAlwaysDisplay = topmost
            frmMain.butTopMost.Checked = topmost
            Call mdlMisc.StartWithWindows(Me.startup)
        End If
        If startHidden Then
            frmMain.WindowState = FormWindowState.Minimized
            frmMain.Hide()
        Else
            frmMain.WindowState = FormWindowState.Normal
            frmMain.Show()
            frmMain.BringToFront()
        End If
    End Sub

End Class
