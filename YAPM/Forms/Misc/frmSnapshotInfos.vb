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
Imports Common.Misc

Public Class frmSnapshotInfos

    Private Sub frmFileRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdOK, "OK")
        Native.Functions.Misc.SetTheme(Me.tv.Handle)
        If Program.Connection.Snapshot IsNot Nothing Then
            Call Me.BuildTree(Program.Connection.Snapshot)
        End If
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    ' Build the tree with ssfile info
    Private Sub BuildTree(ByVal snap As cSnapshot)
        ' Root nodes
        Dim rootRoot As New TreeNode("Snapshot")
        Dim rootInfo As New TreeNode("Information about the snapshot")
        Dim rootData As New TreeNode("Embedded data")
        Me.tv.Nodes.Add(rootRoot)

        ' Informations about the snapshot
        rootRoot.Nodes.Add(rootInfo)
        rootInfo.Nodes.Add("File type version : " & snap.FileVersion)
        rootInfo.Nodes.Add("Date : " & snap.Date.ToLongDateString & "-" & snap.Date.ToLongTimeString)
        rootInfo.Nodes.Add("ComputerName : " & snap.SystemInformation.ComputerName)
        rootInfo.Nodes.Add("UserName : " & snap.SystemInformation.UserName)
        rootInfo.Nodes.Add("Culture : " & snap.SystemInformation.Culture.ToString)
        rootInfo.Nodes.Add("IntPtrSize : " & snap.SystemInformation.IntPtrSize)
        rootInfo.Nodes.Add("OSFullName : " & snap.SystemInformation.OSFullName)
        rootInfo.Nodes.Add("OSPlatform : " & snap.SystemInformation.OSPlatform)
        rootInfo.Nodes.Add("OSVersion : " & snap.SystemInformation.OSVersion)
        rootInfo.Nodes.Add("AvailablePhysicalMemory : " & GetFormatedSize(snap.SystemInformation.AvailablePhysicalMemory))
        rootInfo.Nodes.Add("AvailableVirtualMemory : " & GetFormatedSize(snap.SystemInformation.AvailableVirtualMemory))
        rootInfo.Nodes.Add("TotalPhysicalMemory : " & GetFormatedSize(snap.SystemInformation.TotalPhysicalMemory))
        rootInfo.Nodes.Add("TotalVirtualMemory : " & GetFormatedSize(snap.SystemInformation.TotalVirtualMemory))

        ' Processes


        ' Services


        ' Network connections


        ' Tasks
        If snap.Tasks IsNot Nothing Then
            Dim rootTask As TreeNode = rootData.Nodes.Add("Tasks")
            For Each pair As Collections.Generic.KeyValuePair(Of String, windowInfos) In snap.Tasks
                Dim n As New TreeNode("Handle : " & pair.Value.Handle.ToString)
                rootTask.Nodes.Add(n)
                For Each info As String In windowInfos.GetAvailableProperties
                    n.Nodes.Add(info & " : " & New cTask(pair.Value).GetInformation(info))
                Next
            Next
        End If

        ' Jobs


    End Sub

End Class