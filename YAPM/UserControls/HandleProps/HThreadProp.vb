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

Public Class HThreadProp
    Inherits HXXXProp

    ' ProcessId/threadId of the process opened with the handle
    Private _pid As Integer
    Private _proc As cProcess
    Private _tid As Integer

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()

        Try
            ' Extract the processId from the name of the handle
            ' The string is like : processName (processId) - threadId
            Dim nam As String = Me.TheHandle.Infos.Name
            Dim n2 As Integer = nam.IndexOf("(", nam.Length - 17)
            Dim n1 As Integer = nam.IndexOf(")", nam.Length - 13)
            Dim n3 As Integer = nam.IndexOf(" ", nam.Length - 5)

            If n2 > 0 AndAlso n1 > 0 AndAlso n3 > 0 Then
                _pid = Integer.Parse(nam.Substring(n2 + 1, n1 - n2 - 1))
                _tid = Integer.Parse(nam.Substring(n3 + 1, nam.Length - n3 - 1))
                _proc = ProcessProvider.GetProcessById(_pid)
            Else
                _pid = 0
                _proc = Nothing
            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Me.cmdOpen.Enabled = (_proc IsNot Nothing)
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Dim frm As New frmProcessInfo
        frm.SetProcess(_proc)
        frm.TopMost = _frmMain.TopMost
        frm.Show()
        frm.tabProcess.SelectedTab = frm.TabPageThreads

        ' Create a thread which wait for threads to be added in the lvThread
        ' and then select the good thread
        Threading.ThreadPool.QueueUserWorkItem(AddressOf selectThreadImp, New contextObj(_tid, frm))
    End Sub

    Private Sub HKeyProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.SetToolTip(Me.cmdOpen, "Show details about the thread")
    End Sub

    Private Structure contextObj
        Public tid As Integer
        Public frmProcInfo As frmProcessInfo
        Public Sub New(ByVal threadId As Integer, ByVal form As frmProcessInfo)
            tid = threadId
            frmProcInfo = form
        End Sub
    End Structure
    Private Sub selectThreadImp(ByVal context As Object)
        Dim pObj As contextObj = DirectCast(context, contextObj)

        ' Wait for threads to be added
        While pObj.frmProcInfo.lvThreads.Items.Count = 0
            Threading.Thread.Sleep(50)
        End While

        ' Select the good thread
        Async.ListView.EnsureItemVisible(pObj.frmProcInfo.lvThreads, pObj.tid.ToString)
    End Sub

End Class
