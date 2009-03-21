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

Public Class Pref

    ' Options
    Public replaceTaskMgr As Boolean
    Public procInterval As Integer
    Public trayInterval As Integer
    Public systemInterval As Integer
    Public serviceInterval As Integer
    Public startup As Boolean
    Public startHidden As Boolean
    Public lang As String
    Public topmost As Boolean
    Public firstTime As Boolean

    Public ribbonStyle As Boolean
    Public newItemsColor As Integer
    Public deletedItemsColor As Integer
    Public closeYAPMWithCloseButton As Boolean
    Public showTrayIcon As Boolean
    Public priority As Integer
    Public taskInterval As Integer
    Public networkInterval As Integer
    Public searchEngine As String
    Public warnDangerous As Boolean
    Public hideMinimized As Boolean

    ' Open XML
    Public Sub Load()
        Dim XmlDoc As XmlDocument = New XmlDocument
        Dim element As XmlNodeList
        Dim noeud, noeudEnf As XmlNode

        Call XmlDoc.Load(frmMain.PREF_PATH)
        element = XmlDoc.DocumentElement.GetElementsByTagName("config")

        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "procinterval" Then
                    procInterval = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "serviceinterval" Then
                    serviceInterval = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "startup" Then
                    startup = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "starthidden" Then
                    startHidden = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "lang" Then
                    lang = noeudEnf.InnerText
                ElseIf noeudEnf.LocalName = "topmost" Then
                    topmost = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "firsttime" Then
                    firstTime = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "replacetaskmgr" Then
                    replaceTaskMgr = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "ribbonstyle" Then
                    ribbonStyle = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "newitemscolor" Then
                    newItemsColor = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "deleteditemscolor" Then
                    deletedItemsColor = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "closeyapmwithclosebutton" Then
                    closeYAPMWithCloseButton = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "showtrayicon" Then
                    showTrayIcon = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "priority" Then
                    priority = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "taskinterval" Then
                    taskInterval = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "networkinterval" Then
                    networkInterval = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "searchengine" Then
                    searchEngine = noeudEnf.InnerText
                ElseIf noeudEnf.LocalName = "warndangerous" Then
                    warnDangerous = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "hideminimized" Then
                    hideMinimized = CBool(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "trayinterval" Then
                    trayInterval = CInt(noeudEnf.InnerText)
                ElseIf noeudEnf.LocalName = "systeminterval" Then
                    systemInterval = CInt(noeudEnf.InnerText)
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
        elemProcIntervall = XmlDoc.CreateElement("procinterval")
        elemProcIntervall.InnerText = CStr(Me.procInterval)
        elemConfig.AppendChild(elemProcIntervall)
        Dim elemServiceIntervall As XmlElement
        elemServiceIntervall = XmlDoc.CreateElement("serviceinterval")
        elemServiceIntervall.InnerText = CStr(Me.serviceInterval)
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
        Dim elemTopMost As XmlElement
        elemTopMost = XmlDoc.CreateElement("topmost")
        elemTopMost.InnerText = CStr(Me.topmost)
        elemConfig.AppendChild(elemTopMost)
        Dim elemFirstTime As XmlElement
        elemFirstTime = XmlDoc.CreateElement("firsttime")
        elemFirstTime.InnerText = CStr(Me.firstTime)
        elemConfig.AppendChild(elemFirstTime)
        Dim elemTaskmgr As XmlElement
        elemTaskmgr = XmlDoc.CreateElement("replacetaskmgr")
        elemTaskmgr.InnerText = CStr(Me.replaceTaskMgr)
        elemConfig.AppendChild(elemTaskmgr)
        Dim elemRibbon As XmlElement
        elemRibbon = XmlDoc.CreateElement("ribbonstyle")
        elemRibbon.InnerText = CStr(Me.ribbonStyle)
        elemConfig.AppendChild(elemRibbon)
        Dim elemEngine As XmlElement
        elemEngine = XmlDoc.CreateElement("searchengine")
        elemEngine.InnerText = CStr(Me.searchEngine)
        elemConfig.AppendChild(elemEngine)
        Dim elemNewColor As XmlElement
        elemNewColor = XmlDoc.CreateElement("newitemscolor")
        elemNewColor.InnerText = CStr(Me.newItemsColor)
        elemConfig.AppendChild(elemNewColor)
        Dim elemDeletedColor As XmlElement
        elemDeletedColor = XmlDoc.CreateElement("deleteditemscolor")
        elemDeletedColor.InnerText = CStr(Me.deletedItemsColor)
        elemConfig.AppendChild(elemDeletedColor)
        Dim elemCloseButton As XmlElement
        elemCloseButton = XmlDoc.CreateElement("closeyapmwithclosebutton")
        elemCloseButton.InnerText = CStr(Me.closeYAPMWithCloseButton)
        elemConfig.AppendChild(elemCloseButton)
        Dim elemShowTray As XmlElement
        elemShowTray = XmlDoc.CreateElement("showtrayicon")
        elemShowTray.InnerText = CStr(Me.showTrayIcon)
        elemConfig.AppendChild(elemShowTray)
        Dim elemPriority As XmlElement
        elemPriority = XmlDoc.CreateElement("priority")
        elemPriority.InnerText = CStr(Me.priority)
        elemConfig.AppendChild(elemPriority)
        Dim elemTaskInterval As XmlElement
        elemTaskInterval = XmlDoc.CreateElement("taskinterval")
        elemTaskInterval.InnerText = CStr(Me.taskInterval)
        elemConfig.AppendChild(elemTaskInterval)
        Dim elemNetworkInterval As XmlElement
        elemNetworkInterval = XmlDoc.CreateElement("networkinterval")
        elemNetworkInterval.InnerText = CStr(Me.networkInterval)
        elemConfig.AppendChild(elemNetworkInterval)
        Dim elemWarnDangerous As XmlElement
        elemWarnDangerous = XmlDoc.CreateElement("warndangerous")
        elemWarnDangerous.InnerText = CStr(Me.warnDangerous)
        elemConfig.AppendChild(elemWarnDangerous)
        Dim elemHideMin As XmlElement
        elemHideMin = XmlDoc.CreateElement("hideminimized")
        elemHideMin.InnerText = CStr(Me.hideMinimized)
        elemConfig.AppendChild(elemHideMin)
        Dim elemTrayInterval As XmlElement
        elemTrayInterval = XmlDoc.CreateElement("trayinterval")
        elemTrayInterval.InnerText = CStr(Me.trayInterval)
        elemConfig.AppendChild(elemTrayInterval)
        Dim elemSystemInterval As XmlElement
        elemSystemInterval = XmlDoc.CreateElement("systeminterval")
        elemSystemInterval.InnerText = CStr(Me.systemInterval)
        elemConfig.AppendChild(elemSystemInterval)

        XmlDoc.DocumentElement.AppendChild(elemConfig)
        XmlDoc.Save(frmMain.PREF_PATH)
    End Sub

    ' Apply pref
    Public Sub Apply()
        Static first As Boolean = True
        frmMain.timerProcess.Interval = CInt(IIf(procInterval > 0, procInterval, frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES))
        frmMain.timerServices.Interval = CInt(IIf(serviceInterval > 0, serviceInterval, frmMain.DEFAULT_TIMER_INTERVAL_SERVICES))
        frmMain.timerNetwork.Interval = CInt(IIf(networkInterval > 0, networkInterval, frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES))
        frmMain.timerTask.Interval = CInt(IIf(taskInterval > 0, taskInterval, frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES))
        frmMain.timerTrayIcon.Interval = CInt(IIf(trayInterval > 0, trayInterval, frmMain.DEFAULT_TIMER_INTERVAL_PROCESSES))
        Select Case priority
            Case 0
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.Idle
            Case 1
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.BelowNormal
            Case 2
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.Normal
            Case 3
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.AboveNormal
            Case 4
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.High
            Case 5
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.RealTime
        End Select
        handleList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        handleList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        memoryList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        memoryList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        windowList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        windowList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        moduleList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        moduleList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        networkList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        networkList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        processList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        processList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        serviceList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        serviceList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        taskList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        taskList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        threadList.NEW_ITEM_COLOR = Color.FromArgb(newItemsColor)
        threadList.DELETED_ITEM_COLOR = Color.FromArgb(deletedItemsColor)
        frmMain.Tray.Visible = showTrayIcon
        If first Then
            Call frmMain.permuteMenuStyle(ribbonStyle)
            first = False
            frmMain.TopMost = topmost
            frmMain.butAlwaysDisplay.Checked = topmost
            If startHidden Then
                frmMain.WindowState = FormWindowState.Minimized
                frmMain.Hide()
            End If
        End If
    End Sub

End Class
