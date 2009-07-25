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

Imports YAPM.frmProcessInfo

Public Class frmLogOptions

    Private _logDisplayMask As asyncCallbackLogEnumerate.LogItemType
    Private _logCaptureMask As asyncCallbackLogEnumerate.LogItemType
    Private _frm As frmProcessInfo

    Public Property LogCaptureMask() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _logCaptureMask
        End Get
        Set(ByVal value As asyncCallbackLogEnumerate.LogItemType)
            _logCaptureMask = value
        End Set
    End Property
    Public Property LogDisplayMask() As asyncCallbackLogEnumerate.LogItemType
        Get
            Return _logDisplayMask
        End Get
        Set(ByVal value As asyncCallbackLogEnumerate.LogItemType)
            _logDisplayMask = value
        End Set
    End Property
    Public Property Form() As frmProcessInfo
        Get
            Return _frm
        End Get
        Set(ByVal value As frmProcessInfo)
            _frm = value
        End Set
    End Property

    Private Sub frmLogOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        Me.captureHandles.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.HandleItem) = asyncCallbackLogEnumerate.LogItemType.HandleItem
        Me.showHandles.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.HandleItem) = asyncCallbackLogEnumerate.LogItemType.HandleItem

        Me.captureMemoryRegions.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.MemoryItem) = asyncCallbackLogEnumerate.LogItemType.MemoryItem
        Me.showMemoryRegions.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.MemoryItem) = asyncCallbackLogEnumerate.LogItemType.MemoryItem

        Me.captureModules.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.ModuleItem) = asyncCallbackLogEnumerate.LogItemType.ModuleItem
        Me.showModules.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.ModuleItem) = asyncCallbackLogEnumerate.LogItemType.ModuleItem

        Me.captureNetwork.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.NetworkItem) = asyncCallbackLogEnumerate.LogItemType.NetworkItem
        Me.showNetwork.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.NetworkItem) = asyncCallbackLogEnumerate.LogItemType.NetworkItem

        Me.captureServices.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.ServiceItem) = asyncCallbackLogEnumerate.LogItemType.ServiceItem
        Me.showServices.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.ServiceItem) = asyncCallbackLogEnumerate.LogItemType.ServiceItem

        Me.captureThreads.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.ThreadItem) = asyncCallbackLogEnumerate.LogItemType.ThreadItem
        Me.showThreads.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.ThreadItem) = asyncCallbackLogEnumerate.LogItemType.ThreadItem

        Me.captureWindows.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.WindowItem) = asyncCallbackLogEnumerate.LogItemType.WindowItem
        Me.showWindows.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.WindowItem) = asyncCallbackLogEnumerate.LogItemType.WindowItem

        Me.captureCreated.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.CreatedItems) = asyncCallbackLogEnumerate.LogItemType.CreatedItems
        Me.showCreated.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.CreatedItems) = asyncCallbackLogEnumerate.LogItemType.CreatedItems

        Me.captureDeleted.Checked = (_logCaptureMask And asyncCallbackLogEnumerate.LogItemType.DeletedItems) = asyncCallbackLogEnumerate.LogItemType.DeletedItems
        Me.showDeleted.Checked = (_logDisplayMask And asyncCallbackLogEnumerate.LogItemType.DeletedItems) = asyncCallbackLogEnumerate.LogItemType.DeletedItems

        Me.logInterval.Value = _frm.timerLog.Interval
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        _logCaptureMask = 0
        _logDisplayMask = 0

        If Me.captureHandles.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.HandleItem
        If Me.captureMemoryRegions.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.MemoryItem
        If Me.captureModules.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.ModuleItem
        If Me.captureNetwork.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.NetworkItem
        If Me.captureServices.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.ServiceItem
        If Me.captureThreads.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.ThreadItem
        If Me.captureWindows.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.WindowItem
        If Me.captureCreated.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.CreatedItems
        If Me.captureDeleted.Checked Then _logCaptureMask = _logCaptureMask Or asyncCallbackLogEnumerate.LogItemType.DeletedItems

        If Me.showHandles.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.HandleItem
        If Me.showMemoryRegions.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.MemoryItem
        If Me.showModules.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.ModuleItem
        If Me.showNetwork.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.NetworkItem
        If Me.showServices.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.ServiceItem
        If Me.showThreads.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.ThreadItem
        If Me.showWindows.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.WindowItem
        If Me.showCreated.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.CreatedItems
        If Me.showDeleted.Checked Then _logDisplayMask = _logDisplayMask Or asyncCallbackLogEnumerate.LogItemType.DeletedItems

        _frm.LogCaptureMask = _logCaptureMask
        _frm.LogDisplayMask = _logDisplayMask
        _frm.timerLog.Interval = CInt(Me.logInterval.Value)
        _frm.LvAutoScroll = Me._autoScroll.Checked

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class