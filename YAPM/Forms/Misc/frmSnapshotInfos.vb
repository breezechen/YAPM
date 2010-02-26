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
        SetToolTip(Me.cmdBrowseSSFile, "Select the System Snapshot File")
        SetToolTip(Me.cmdGo, "Explore file")
        Native.Functions.Misc.SetTheme(Me.tv.Handle)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        If System.IO.File.Exists(Me.txtSSFile.Text) Then
            Me.Text = "System Snapshot File information - Exploring file..."
            Try
                Dim snap As New cSnapshot250(Me.txtSSFile.Text)
                Me.Text = "System Snapshot File information - Building tree..."
                Call Me.BuildTree(snap)
                Me.Text = "System Snapshot File information"
            Catch ex As Exception
                Misc.ShowError(ex, "Could not explore file !")
                Me.Text = "System Snapshot File information - Failed to explore file"
            End Try
        End If
    End Sub

    Private Sub cmdBrowseSSFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseSSFile.Click
        Dim ret As DialogResult = Me.openFile.ShowDialog
        If ret = Windows.Forms.DialogResult.OK Then
            Me.txtSSFile.Text = Me.openFile.FileName
        End If
    End Sub

    ' Build the tree with ssfile info
    Private Sub BuildTree(ByVal snap As cSnapshot250)

        Me.tv.Nodes.Clear()
        Me.tv.BeginUpdate()

        ' Root nodes
        Dim rootRoot As New TreeNode("Snapshot")
        Dim rootInfo As New TreeNode("Information about the snapshot")
        Dim rootData As New TreeNode("Embedded data")
        rootRoot.Nodes.Add(rootInfo)
        rootRoot.Nodes.Add(rootData)
        Me.tv.Nodes.Add(rootRoot)

        ' Informations about the snapshot
        rootInfo.Nodes.Add("File type version : " & snap.FileVersion)
        rootInfo.Nodes.Add("Date : " & snap.Date.ToLongDateString & "-" & snap.Date.ToLongTimeString)
        If snap.SystemInformation IsNot Nothing Then
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
        End If

        ' Processes
        If snap.Processes IsNot Nothing Then
            Dim rootProcess As TreeNode = rootData.Nodes.Add("Processes")
            For Each pair As Collections.Generic.KeyValuePair(Of Integer, processInfos) In snap.Processes
                Dim n As New TreeNode(pair.Key & " - " & pair.Value.Name)

                ' Env var
                If snap.EnvironnementVariablesByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nEnv As New TreeNode("Environment variables")
                    n.Nodes.Add(nEnv)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, envVariableInfos) In snap.EnvironnementVariablesByProcessId(pair.Value.ProcessId)
                        Dim tEnv As TreeNode = nEnv.Nodes.Add(pair2.Value.Variable)
                        tEnv.Nodes.Add(pair2.Value.Value)
                    Next
                End If

                ' Handles
                If snap.HandlesByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nHandles As New TreeNode("Handles")
                    n.Nodes.Add(nHandles)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, handleInfos) In snap.HandlesByProcessId(pair.Value.ProcessId)
                        Dim sss As String = "0x" & pair2.Value.Handle.ToString("x")
                        If pair2.Value.Name IsNot Nothing AndAlso pair2.Value.Name.Length > 0 Then
                            sss &= " (" & pair2.Value.Name & ")"
                        End If
                        Dim tHandle As TreeNode = nHandles.Nodes.Add(sss)
                        For Each info2 As String In handleInfos.GetAvailableProperties
                            tHandle.Nodes.Add(info2 & " : " & New cHandle(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Heaps
                If snap.HeapsByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nHeaps As New TreeNode("Heaps")
                    n.Nodes.Add(nHeaps)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, heapInfos) In snap.HeapsByProcessId(pair.Value.ProcessId)
                        Dim tHeap As TreeNode = nHeaps.Nodes.Add("0x" & pair2.Value.BaseAddress.ToString("x"))
                        For Each info2 As String In heapInfos.GetAvailableProperties
                            tHeap.Nodes.Add(info2 & " : " & New cHeap(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Mem region
                If snap.MemoryRegionsByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nMemRegions As New TreeNode("Memory regions")
                    n.Nodes.Add(nMemRegions)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, memRegionInfos) In snap.MemoryRegionsByProcessId(pair.Value.ProcessId)
                        Dim tMem As TreeNode = nMemRegions.Nodes.Add("0x" & pair2.Value.BaseAddress.ToString("x"))
                        For Each info2 As String In memRegionInfos.GetAvailableProperties
                            tMem.Nodes.Add(info2 & " : " & New cMemRegion(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Modules
                If snap.ModulesByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nModules As New TreeNode("Modules")
                    n.Nodes.Add(nModules)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, moduleInfos) In snap.ModulesByProcessId(pair.Value.ProcessId)
                        Dim tModule As TreeNode = nModules.Nodes.Add(pair2.Value.Name)
                        For Each info2 As String In moduleInfos.GetAvailableProperties
                            tModule.Nodes.Add(info2 & " : " & New cModule(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Network connections
                If snap.NetworkConnectionsByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nConn As New TreeNode("Network connections")
                    n.Nodes.Add(nConn)
                    Dim i2 As Integer = 0
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, networkInfos) In snap.NetworkConnectionsByProcessId(pair.Value.ProcessId)
                        i2 += 1
                        Dim tNet As TreeNode = nConn.Nodes.Add("Connection " & i2.ToString)
                        For Each info2 As String In networkInfos.GetAvailableProperties
                            tNet.Nodes.Add(info2 & " : " & New cNetwork(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Privileges
                If snap.PrivilegesByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nPriv As New TreeNode("Privileges")
                    n.Nodes.Add(nPriv)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, privilegeInfos) In snap.PrivilegesByProcessId(pair.Value.ProcessId)
                        Dim tPri As TreeNode = nPriv.Nodes.Add(pair2.Value.Name)
                        For Each info2 As String In privilegeInfos.GetAvailableProperties
                            tPri.Nodes.Add(info2 & " : " & New cPrivilege(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Services
                If snap.ServicesByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nServ As New TreeNode("Services")
                    n.Nodes.Add(nServ)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, serviceInfos) In snap.ServicesByProcessId(pair.Value.ProcessId)
                        Dim tServ As TreeNode = nServ.Nodes.Add(pair2.Value.Name)
                        For Each info2 As String In serviceInfos.GetAvailableProperties
                            tServ.Nodes.Add(info2 & " : " & New cService(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Threads
                If snap.ThreadsByProcessId(pair.Value.ProcessId).Count > 0 Then
                    Dim nThreads As New TreeNode("Threads")
                    n.Nodes.Add(nThreads)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, threadInfos) In snap.ThreadsByProcessId(pair.Value.ProcessId)
                        Dim tThread As TreeNode = nThreads.Nodes.Add(pair2.Value.Id.ToString)
                        For Each info2 As String In threadInfos.GetAvailableProperties
                            tThread.Nodes.Add(info2 & " : " & New cThread(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Windows
                Dim _windowsOfProcess As New Dictionary(Of String, windowInfos)
                For Each pair2 As Generic.KeyValuePair(Of String, windowInfos) In snap.Windows
                    If pair2.Value.ProcessId = pair.Value.ProcessId Then
                        _windowsOfProcess.Add(pair2.Key, pair2.Value)
                    End If
                Next
                If _windowsOfProcess.Count > 0 Then
                    Dim nWindows As New TreeNode("Windows")
                    n.Nodes.Add(nWindows)
                    For Each pair2 As Collections.Generic.KeyValuePair(Of String, windowInfos) In _windowsOfProcess
                        Dim tW As TreeNode = nWindows.Nodes.Add(pair2.Value.Handle.ToString)
                        For Each info2 As String In windowInfos.GetAvailableProperties
                            tW.Nodes.Add(info2 & " : " & New cWindow(pair2.Value).GetInformation(info2))
                        Next
                    Next
                End If

                ' Infos about the process
                For Each info As String In processInfos.GetAvailableProperties
                    n.Nodes.Add(info & " : " & New cProcess(pair.Value).GetInformation(info))
                Next
                rootProcess.Nodes.Add(n)

            Next
        End If

        ' Services
        If snap.Services IsNot Nothing Then
            Dim rootService As TreeNode = rootData.Nodes.Add("Services")
            For Each pair As Collections.Generic.KeyValuePair(Of String, serviceInfos) In snap.Services
                Dim n As New TreeNode(pair.Key)
                rootService.Nodes.Add(n)
                For Each info As String In serviceInfos.GetAvailableProperties
                    n.Nodes.Add(info & " : " & New cService(pair.Value).GetInformation(info))
                Next
            Next
        End If

        ' Network connections
        If snap.NetworkConnections IsNot Nothing Then
            Dim rootNetwork As TreeNode = rootData.Nodes.Add("Network connections")
            Dim i As Integer = 0
            For Each pair As Collections.Generic.KeyValuePair(Of String, networkInfos) In snap.NetworkConnections
                i += 1
                Dim n As New TreeNode("Item " & i.ToString)
                rootNetwork.Nodes.Add(n)
                For Each info As String In networkInfos.GetAvailableProperties
                    n.Nodes.Add(info & " : " & New cNetwork(pair.Value).GetInformation(info))
                Next
            Next
        End If

        ' Tasks
        If snap.Windows IsNot Nothing Then
            Dim rootTask As TreeNode = rootData.Nodes.Add("Tasks")
            For Each pair As Collections.Generic.KeyValuePair(Of String, windowInfos) In snap.Windows
                If pair.Value.IsTask Then
                    Dim n As New TreeNode("Handle : " & pair.Value.Handle.ToString)
                    rootTask.Nodes.Add(n)
                    For Each info As String In windowInfos.GetAvailableProperties
                        n.Nodes.Add(info & " : " & New cTask(pair.Value).GetInformation(info))
                    Next
                End If
            Next
        End If

        ' Jobs
        If snap.Jobs IsNot Nothing Then
            Dim rootJob As TreeNode = rootData.Nodes.Add("Jobs")
            For Each pair As Collections.Generic.KeyValuePair(Of String, jobInfos) In snap.Jobs
                Dim n As New TreeNode(pair.Key)
                rootJob.Nodes.Add(n)
                For Each info As String In jobInfos.GetAvailableProperties
                    n.Nodes.Add(info & " : " & New cJob(pair.Value).GetInformation(info))
                Next
                Dim nLimits As New TreeNode("Limits")
                n.Nodes.Add(nLimits)
                For Each pair2 As Collections.Generic.KeyValuePair(Of String, jobLimitInfos) In snap.JobLimitsByJobName(pair.Key)
                    Dim tLimit As TreeNode = nLimits.Nodes.Add(pair2.Value.Name)
                    For Each info2 As String In jobLimitInfos.GetAvailableProperties
                        tLimit.Nodes.Add(info2 & " : " & New cJobLimit(pair2.Value).GetInformation(info2))
                    Next
                Next
            Next
        End If


        Me.tv.EndUpdate()

    End Sub

End Class