Option Strict On

Imports System.Runtime.InteropServices

Public Class frmMain

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    Dim tree As NativeDependenciesTree

    Public Sub OpenReferences(ByVal asmFile As String)

        Me.Text = "Dependencies - " & asmFile
        Try
            tree = New NativeDependenciesTree(asmFile)
            tvDepends.Nodes.Clear()
            tvDepends.Nodes.Add(Me.CreateExpandableReferenceNode(tree.MainDll))

            lvAllDeps.BeginUpdate()
            lvAllDeps.Items.Clear()
            For Each dep As NativeDependenciesTree.NativeDependency In tree.GetAllDependencies()
                If dep.Resolved Then
                    Dim fvi As FileVersionInfo = FileVersionInfo.GetVersionInfo(dep.PE.FileName)
                    lvAllDeps.Items.Add(New ListViewItem(New String() {System.IO.Path.GetFileName(dep.FileName), dep.PE.TimeStamp.ToString("dd/MM/yyyy HH:mm:ss"), dep.PE.NTHeader.SizeOfImage.ToString(), dep.PE.Machine.ToString(), dep.PE.NTHeader.Subsystem.ToString(), fvi.ProductName, fvi.CompanyName, fvi.FileVersion, fvi.ProductVersion, String.Format("{0}.{1}", dep.PE.OptionalHeader.MajorLinkerVersion, dep.PE.OptionalHeader.MinorLinkerVersion), dep.PE.FileName}, "dll"))
                Else
                    lvAllDeps.Items.Add(New ListViewItem(New String() {System.IO.Path.GetFileName(dep.FileName)}, "unresolved"))
                End If
            Next
            lvAllDeps.EndUpdate()
        Catch ex As Exception
            '
        End Try

    End Sub

    Private Function CreateAssemblyNode(ByVal refNode As NativeDependenciesTree.NativeDependency) As TreeNode
        Dim n As New TreeNode()
        If Not refNode.Resolved Then
            n.Text = System.IO.Path.GetFileName(refNode.FileName)
            n.ToolTipText = refNode.FileName
            n.ImageKey = "unresolved"
            n.SelectedImageKey = "unresolved"
            n.StateImageKey = "unresolved"
        Else
            n.Text = System.IO.Path.GetFileName(refNode.FileName)
            n.ToolTipText = refNode.PE.FileName
            n.ImageKey = "dll"
            n.SelectedImageKey = "dll"
            n.StateImageKey = "dll"
        End If

        n.Tag = refNode

        Return n
    End Function
    Private Function CreateExpandableReferenceNode(ByVal refNode As NativeDependenciesTree.NativeDependency) As TreeNode
        Dim n As TreeNode = Me.CreateAssemblyNode(refNode)
        If refNode.Resolved Then
            Dim dummy As New TreeNode()
            dummy.Tag = "dummy"
            n.Nodes.Add(dummy)
        End If

        Return n
    End Function

    Private Sub FillAssemblyNode(ByVal parent As TreeNode)
        Dim refNode As NativeDependenciesTree.NativeDependency = DirectCast(parent.Tag, NativeDependenciesTree.NativeDependency)

        For Each referencedDll As NativeDependenciesTree.NativeDependency In refNode.Dependencies
            parent.Nodes.Add(Me.CreateExpandableReferenceNode(referencedDll))
        Next
    End Sub

    Private Sub tvDepends_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles tvDepends.NodeMouseClick
        Try
            Dim refNode As NativeDependenciesTree.NativeDependency = DirectCast(e.Node.Tag, NativeDependenciesTree.NativeDependency)

            statusFile.Text = refNode.PE.FileName
            If refNode.Resolved Then Me.ShowAssemblyInfos(refNode)
        Catch ex As Exception
            '
        End Try

    End Sub

    Private Sub ShowAssemblyInfos(ByVal dll As NativeDependenciesTree.NativeDependency)
        lvExports.BeginUpdate()
        lvExports.Items.Clear()
        For Each export As ExportEntry In dll.PE.ExportDirectory.ExportEntries
            lvExports.Items.Add(New ListViewItem(New String() {export.Ordinal.ToString(), export.Hint.ToString(), export.Name, export.ExportRVA.ToString("X8")}, "function"))
        Next
        lvExports.EndUpdate()

        lvImports.BeginUpdate()
        lvImports.Items.Clear()
        For Each refDll As DllImportEntry In dll.PE.ImportDirectory.DllEntries
            For Each import As ImportEntry In refDll.Entries
                lvImports.Items.Add(New ListViewItem(New String() {import.Ordinal.ToString(), import.Hint.ToString(), refDll.DllName, import.Name, import.Address.ToString("X8")}, "function"))
            Next
        Next
        lvImports.EndUpdate()
    End Sub

    Private Sub tvDepends_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles tvDepends.BeforeExpand
        If Not e.Node.FirstNode Is Nothing AndAlso "dummy".Equals(e.Node.FirstNode.Tag) Then
            e.Node.FirstNode.Remove()
            Me.FillAssemblyNode(e.Node)
        End If
    End Sub

    Private Sub AlwaysVisble_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAlwaysVisible.Click
        Me.mnuAlwaysVisible.Checked = Not (Me.mnuAlwaysVisible.Checked)
        Me.TopMost = Me.mnuAlwaysVisible.Checked
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call SetWindowTheme(Me.lvAllDeps.Handle, "explorer", Nothing)
        Call SetWindowTheme(Me.lvExports.Handle, "explorer", Nothing)
        Call SetWindowTheme(Me.lvImports.Handle, "explorer", Nothing)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        CDO.AddExtension = True
        CDO.CheckFileExists = True
        CDO.CheckPathExists = True
        CDO.DereferenceLinks = True
        CDO.Filter = "Assemblies (exe,dll)|*.exe;*.dll|All|*.*"
        CDO.RestoreDirectory = True
        If CDO.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Call OpenReferences(CDO.FileName)
        End If
    End Sub

    Public Sub HideOpenMenu()
        Me.OpenToolStripMenuItem.Visible = False
    End Sub
End Class