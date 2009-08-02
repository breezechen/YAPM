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

Imports System.Runtime.InteropServices

Public Class frmChangeMemoryProtection

    'AccessDenied = 0
    'Execute = &H10
    'ExecuteRead = &H20
    'ExecuteReadWrite = &H40
    'ExecuteWriteCopy = &H80
    'NoAccess = &H1
    '[ReadOnly] = &H2
    'ReadWrite = &H4
    'WriteCopy = &H8
    'Guard = &H100
    'NoCache = &H200
    'WriteCombine = &H400

    Public WriteOnly Property ProtectionType() As API.PROTECTION_TYPE
        Set(ByVal value As API.PROTECTION_TYPE)
            If (value And API.PROTECTION_TYPE.Execute) = API.PROTECTION_TYPE.Execute Then
                Me.optE.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.ExecuteRead) = API.PROTECTION_TYPE.ExecuteRead Then
                Me.optER.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.ExecuteReadWrite) = API.PROTECTION_TYPE.ExecuteReadWrite Then
                Me.optERW.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.ExecuteWriteCopy) = API.PROTECTION_TYPE.ExecuteWriteCopy Then
                Me.optEWC.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.NoAccess) = API.PROTECTION_TYPE.NoAccess Then
                Me.optNA.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.ReadOnly) = API.PROTECTION_TYPE.ReadOnly Then
                Me.optRO.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.ReadWrite) = API.PROTECTION_TYPE.ReadWrite Then
                Me.optRW.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.WriteCopy) = API.PROTECTION_TYPE.WriteCopy Then
                Me.optWC.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.Guard) = API.PROTECTION_TYPE.Guard Then
                Me.chkGuard.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.NoCache) = API.PROTECTION_TYPE.NoCache Then
                Me.chkNoCache.Checked = True
            End If
            If (value And API.PROTECTION_TYPE.WriteCombine) = API.PROTECTION_TYPE.WriteCombine Then
                Me.chkWriteCombine.Checked = True
            End If
        End Set
    End Property

    Public ReadOnly Property NewProtectionType() As API.PROTECTION_TYPE
        Get
            Dim _type As API.PROTECTION_TYPE

            If Me.optE.Checked Then
                _type = _type Or API.PROTECTION_TYPE.Execute
            End If
            If Me.optER.Checked Then
                _type = _type Or API.PROTECTION_TYPE.ExecuteRead
            End If
            If Me.optERW.Checked Then
                _type = _type Or API.PROTECTION_TYPE.ExecuteReadWrite
            End If
            If Me.optEWC.Checked Then
                _type = _type Or API.PROTECTION_TYPE.ExecuteWriteCopy
            End If
            If Me.optNA.Checked Then
                _type = _type Or API.PROTECTION_TYPE.NoAccess
            End If
            If Me.optRO.Checked Then
                _type = _type Or API.PROTECTION_TYPE.ReadOnly
            End If
            If Me.optRW.Checked Then
                _type = _type Or API.PROTECTION_TYPE.ReadWrite
            End If
            If Me.optWC.Checked Then
                _type = _type Or API.PROTECTION_TYPE.WriteCopy
            End If
            If Me.chkGuard.Checked Then
                _type = _type Or API.PROTECTION_TYPE.Guard
            End If
            If Me.chkNoCache.Checked Then
                _type = _type Or API.PROTECTION_TYPE.NoCache
            End If
            If Me.chkWriteCombine.Checked Then
                _type = _type Or API.PROTECTION_TYPE.WriteCombine
            End If

            Return _type
        End Get
    End Property

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        closeWithEchapKey(Me)
        SetToolTip(Me.cmdChangeProtection, "Change protection type now")
        SetToolTip(Me.cmdExit, "Close this window")
        SetToolTip(Me.optE, "Enables execute access to the committed region of pages. An attempt to read from or write to the committed region results in an access violation.")
        SetToolTip(Me.optER, "Enables execute, read-only, or copy-on-write access to the committed region of pages. An attempt to write to the committed region results in an access violation.")
        SetToolTip(Me.optERW, "Enables execute, read-only, read/write, or copy-on-write access to the committed region of pages.")
        SetToolTip(Me.optEWC, "Enables execute, read-only, or copy-on-write access to the committed region of image file code pages. This value is equivalent to PAGE_EXECUTE_READ.")
        SetToolTip(Me.optNA, "Disables all access to the committed region of pages. An attempt to read from, write to, or execute the committed region results in an access violation exception, called a general protection (GP) fault.")
        SetToolTip(Me.optRO, "Enables read-only or copy-on-write access to the committed region of pages. An attempt to write to the committed region results in an access violation. If the system differentiates between read-only access and execute access, an attempt to execute code in the committed region results in an access violation.")
        SetToolTip(Me.optRW, "Enables read-only, read/write, or copy-on-write access to the committed region of pages.")
        SetToolTip(Me.optWC, "Enables read-only or copy-on-write access to the committed region of pages. This value is equivalent to PAGE_READONLY.")
        SetToolTip(Me.chkGuard, "Pages in the region become guard pages. Any attempt to access a guard page causes the system to raise a STATUS_GUARD_PAGE_VIOLATION exception and turn off the guard page status. Guard pages thus act as a one-time access alarm. For more information, see Creating Guard Pages.")
        SetToolTip(Me.chkNoCache, "Sets all pages to be non-cachable. Applications should not use this attribute except when explicitly required for a device. Using the interlocked functions with memory that is mapped with SEC_NOCACHE can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.")
        SetToolTip(Me.chkWriteCombine, "Sets all pages to be write-combined. Applications should not use this attribute except when explicitly required for a device. Using the interlocked functions with memory that is mapped as write-combined can result in an EXCEPTION_ILLEGAL_INSTRUCTION exception.")
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChangeProtection.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#Region "Checkboxes management"

    ' This is NOT possible to combile all flags together with all protection types
    ' See http://msdn.microsoft.com/en-us/library/aa366786(VS.85).aspx
    ' for details about what is permitted and what is not
    Private Sub optNA_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNA.CheckedChanged
        Me.chkGuard.Enabled = (Me.optNA.Checked = False)
        Me.chkNoCache.Enabled = (Me.optNA.Checked = False)
        Me.chkWriteCombine.Enabled = (Me.optNA.Checked = False)
        If Me.optNA.Checked Then
            Me.chkGuard.Checked = False
            Me.chkNoCache.Checked = False
            Me.chkWriteCombine.Checked = False
        End If
    End Sub
    Private Sub chkGuard_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGuard.CheckedChanged
        If chkGuard.Enabled Then
            Me.chkNoCache.Enabled = (Me.chkGuard.Checked = False)
            Me.chkWriteCombine.Enabled = (Me.chkGuard.Checked = False)
            If Me.chkGuard.Checked Then
                Me.chkNoCache.Checked = False
                Me.chkWriteCombine.Checked = False
            End If
        End If
    End Sub
    Private Sub chkNoCache_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNoCache.CheckedChanged
        If chkNoCache.Enabled Then
            Me.chkWriteCombine.Enabled = (Me.chkNoCache.Checked = False)
            Me.chkGuard.Enabled = (Me.chkNoCache.Checked = False)
            If Me.chkNoCache.Checked Then
                Me.chkWriteCombine.Checked = False
                Me.chkGuard.Checked = False
            End If
        End If
    End Sub
    Private Sub chkWriteCombine_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWriteCombine.CheckedChanged
        If chkWriteCombine.Enabled Then
            Me.chkNoCache.Enabled = (Me.chkWriteCombine.Checked = False)
            Me.chkGuard.Enabled = (Me.chkWriteCombine.Checked = False)
            If Me.chkWriteCombine.Checked Then
                Me.chkNoCache.Checked = False
                Me.chkGuard.Checked = False
            End If
        End If
    End Sub

#End Region

End Class