Option Strict Off

Imports System.Xml
Imports System.IO

Public Class frmMain

    Private Const XML_FILE As String = "conf.xml"
    Private Const SOLUTION_NAME As String = "YAPM.sln"

    Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
        ' Go !
        Me.cmdGo.Enabled = False
        Me.cmdBrowse.Enabled = False
        Me.cmdOpen.Enabled = False
        Call go()
        Me.cmdGo.Enabled = True
        Me.cmdBrowse.Enabled = True
        Me.cmdOpen.Enabled = True
    End Sub

    Private Sub addT(ByVal s As String)
        My.Application.DoEvents()
        Me.lv.Items.Add(s)
        Me.lv.Items(Me.lv.Items.Count - 1).EnsureVisible()
        My.Application.DoEvents()
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        With Me.saveDg
            .AddExtension = True
            .AutoUpgradeEnabled = True
            .DefaultExt = "zip"
            .CheckPathExists = True
            .FileName = "YAPM.zip"
            .Filter = "Zip file (*.zip)|*.zip"
            .InitialDirectory = My.Application.Info.DirectoryPath
            .Title = "Save zip file"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtZipPath.Text = .FileName
            End If
        End With
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        With Me.openDg
            .AddExtension = True
            .AutoUpgradeEnabled = True
            .CheckFileExists = True
            .CheckPathExists = True
            .DefaultExt = "sln"
            .FileName = SOLUTION_NAME
            .InitialDirectory = GetParentDir(Me.txtSolution.Text)
            .Multiselect = False
            .ReadOnlyChecked = False
            .RestoreDirectory = True
            .ShowReadOnly = False
            .Title = "Choose solution file"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtSolution.Text = .FileName
            End If
        End With
    End Sub

    ' Get parent dir
    Public Shared Function GetParentDir(ByVal filePath As String) As String
        Dim i As Integer = InStrRev(filePath, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return filePath.Substring(0, i)
        Else
            Return filePath
        End If
    End Function

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtZipPath.Text = My.Application.Info.DirectoryPath & "\YAPM_" & Date.Now.Ticks.ToString & ".zip"
    End Sub

    Public Shared Sub CopyDirectory(ByVal sourcePath As String, ByVal destinationPath As String)
        CopyDirectory(New DirectoryInfo(sourcePath), New DirectoryInfo(destinationPath))
    End Sub
    Private Shared Sub CopyDirectory(ByVal source As DirectoryInfo, ByVal destination As DirectoryInfo)
        destination.Create()

        For Each file As FileInfo In source.GetFiles()
            file.CopyTo(Path.Combine(destination.FullName, file.Name))
        Next

        For Each subDirectory As DirectoryInfo In source.GetDirectories()
            CopyDirectory(subDirectory, destination.CreateSubdirectory(subDirectory.Name))
        Next
    End Sub


    ' Read from XML file
    Public Sub go()

        addT("GO.")

        Dim _refDir As String = GetParentDir(Me.txtSolution.Text)
        Dim _resFile As String = Me.txtZipPath.Text

        ' Create an empty dir in temp dir
        addT("Creating dir...")
        Dim _tempDir As String = My.Computer.FileSystem.SpecialDirectories.Temp
        _tempDir &= "\" & Date.Now.Ticks.ToString
        System.IO.Directory.CreateDirectory(_tempDir)


        Dim XmlDoc As XmlDocument = New XmlDocument
        Dim element As XmlNodeList
        Dim noeud, noeudEnf As XmlNode

        addT("Reading XML...")
        Call XmlDoc.Load(My.Application.Info.DirectoryPath & "\" & XML_FILE)


        ' Copy roots dir
        element = XmlDoc.DocumentElement.GetElementsByTagName("roots")
        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "dir" Then
                    Dim _copyDir As String = _refDir & "\" & noeudEnf.InnerText
                    addT("Copying root dir... " & _tempDir & "\" & noeudEnf.InnerText)
                    CopyDirectory(_copyDir, _tempDir & "\" & noeudEnf.InnerText)
                End If
            Next
        Next

        ' Remove dir
        element = XmlDoc.DocumentElement.GetElementsByTagName("excluded_dir")
        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "dir" Then
                    Dim _all As Boolean = CBool(noeudEnf.Attributes.GetNamedItem("all").InnerText)

                    If _all = False Then
                        ' Remove only one dir
                        addT("Removing single dir... " & _tempDir & "\" & noeudEnf.InnerText)
                        safeDirDel(_tempDir & "\" & noeudEnf.InnerText)
                    Else
                        ' Remove all dir
                        addT("Removing all unwanted dir... " & noeudEnf.InnerText)
                        recursiveRemove(_tempDir, noeudEnf.InnerText)
                    End If
                End If
            Next
        Next


        ' Remove excluded files
        element = XmlDoc.DocumentElement.GetElementsByTagName("excluded_file")
        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "ext" Then
                    Dim _ext As String = noeudEnf.InnerText
                    addT("Removing all unwanted files by ext... " & noeudEnf.InnerText)
                    recursiveFileRemoveExt(_tempDir, _ext)
                End If
            Next
        Next


        ' Add included files
        element = XmlDoc.DocumentElement.GetElementsByTagName("included_file")
        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "file" Then
                    Dim _file As String = _refDir & "\" & noeudEnf.InnerText
                    Dim _dest As String = _tempDir & "\" & noeudEnf.InnerText
                    addT("Adding fixed files... " & noeudEnf.InnerText)
                    System.IO.Directory.CreateDirectory(GetParentDir(_dest))
                    System.IO.File.Copy(_file, _dest)
                End If
            Next
        Next


        ' Patch unwanted project
        element = XmlDoc.DocumentElement.GetElementsByTagName("excluded_project")
        For Each noeud In element
            For Each noeudEnf In noeud.ChildNodes
                If noeudEnf.LocalName = "proj" Then
                    Dim _proj As String = noeudEnf.InnerText
                    addT("Patching main solution...")
                    removeProj(_tempDir & "\" & SOLUTION_NAME, _proj)
                End If
            Next
        Next


        ' Create zip
        addT("Creating ZIP file...")
        zipDir(_tempDir, _resFile)


        ' Kill temp dir
        addT("Removing temp dir...")
        safeDirDel(_tempDir)
        addT("DONE.")

    End Sub

    Private Sub zipDir(ByVal dir As String, ByVal res As String)
        Dim emptyzip() As Byte = New Byte() {80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Try
            Dim fs As FileStream = File.Create(res)
            fs.Write(emptyzip, 0, emptyzip.Length)
            fs.Flush()
            fs.Close()
            fs = Nothing
            Dim sc As Shell32.ShellClass = New Shell32.ShellClass()
            Dim SrcFlder As Shell32.Folder = sc.NameSpace(dir)
            Dim DestFlder As Shell32.Folder = sc.NameSpace(res)
            Dim items As Shell32.FolderItems = SrcFlder.Items()
            DestFlder.CopyHere(items, 20)
            Dim i As Integer = SrcFlder.Items.Count
            Console.WriteLine(i)
            While DestFlder.Items.Count < i
                System.Threading.Thread.Sleep(1000)
            End While
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub recursiveRemove(ByVal dir As String, ByVal forcedName As String)
        If lastDir(dir.ToLowerInvariant) = forcedName.ToLowerInvariant Then
            ' OK, no sub dir, we kill it
            safeDirDel(dir)
        Else
            ' Recursive for sub dir
            For Each s As String In System.IO.Directory.GetDirectories(dir)
                recursiveRemove(s, forcedName)
            Next
        End If
    End Sub

    Private Sub recursiveFileRemoveExt(ByVal dir As String, ByVal ext As String)
        ' Recursive for sub dir
        For Each s As String In System.IO.Directory.GetDirectories(dir)
            For Each ss As String In System.IO.Directory.GetFiles(s)
                If getExt(ss) = ext Then
                    System.IO.File.Delete(ss)
                End If
            Next
            recursiveRemove(s, ext)
        Next
    End Sub

    Private Function lastDir(ByVal path As String) As String
        Dim i As Integer = InStrRev(path, "\")
        If i = 0 Then
            Return ""
        Else
            Return path.Substring(i, path.Length - i)
        End If
    End Function

    Private Function getExt(ByVal path As String) As String
        Dim x As Integer = InStrRev(path, ".", , CompareMethod.Binary)
        If x > 0 Then
            Return path.Substring(x, path.Length - x)
        Else
            Return ""
        End If
    End Function

    Private Sub safeDirDel(ByVal path As String)
        ' No access problem with FSO
        Dim fso As Object = CreateObject("Scripting.FileSystemObject")
        fso.DeleteFolder(path, True)
        fso = Nothing
    End Sub

    Private Sub addFileAndBuildDir(ByVal file As String)
        recursiveBuildDir(GetParentDir(file), GetParentDir(file))
    End Sub
    Private Sub recursiveBuildDir(ByVal dir As String, ByVal greater As String)
        If System.IO.Directory.Exists(dir) = False Then
            Call recursiveBuildDir(System.IO.Directory.GetParent(dir).FullName, dir)
        Else
            System.IO.Directory.CreateDirectory(greater)
            Return
        End If
    End Sub

    Private Sub removeProj(ByVal file As String, ByVal proj As String)
        Dim ss As String() = IO.File.ReadAllLines(file)
        Dim res() As String
        ReDim res(0)

        Dim remo As Boolean = False
        Dim l1 As Integer = 0
        Dim l2 As Integer = 0
        Dim x As Integer = 0
        For Each line As String In ss
            x += 1
            If line.StartsWith("Project(") AndAlso InStr(line, proj) > 0 Then
                remo = True
                l1 = x
            End If
            If remo AndAlso line.StartsWith("EndProject") Then
                l2 = x
                Exit For
            End If
        Next

        If l1 > 0 And l2 > 0 Then
            ReDim res(ss.Length - l2 + l1)
            For y As Integer = 1 To l1 - 2
                res(y - 1) = ss(y)
            Next
            For y As Integer = l2 + 2 To ss.Length - 1
                res(y - l2 + l1 - 4) = ss(y)
            Next
        End If

        System.IO.File.WriteAllLines(file, res)

    End Sub

End Class
