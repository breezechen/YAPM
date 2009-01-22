Option Strict On

Public Class frmSaveReport

    Private _repType As String
    Private _path As String

    Public Property ReportType() As String
        Get
            Return _repType
        End Get
        Set(ByVal value As String)
            _repType = value
        End Set
    End Property
    Public Property ReportPath() As String
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property


    ' Public functions to save reports
    Public Sub SaveReportServices()
        frmMain.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    ' Create file report
                    Dim c As String = vbNullString

                    Me.ReportPath = s

                    Me.pgb.Maximum = frmMain.lvServices.Items.Count

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Dim stream As New System.IO.StreamWriter(s, False)
                        ' txt
                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvServices.Items
                            Dim cm As cService = CType(it.Tag, cService)

                            Try
                                ' Try to access to the service (avoid to write lines if service
                                ' is deleted)
                                Dim suseless As String = cm.Group

                                c &= "Name" & vbTab & vbTab
                                c &= cm.Name & vbNewLine
                                c &= "Common name" & vbTab & vbTab
                                c &= cm.LongName & vbNewLine
                                c &= "Path" & vbTab & vbTab
                                c &= cm.ImagePath & vbNewLine
                                c &= "ObjectName" & vbTab & vbTab
                                c &= cm.ObjectName & vbNewLine
                                c &= "State" & vbTab & vbTab
                                c &= cm.Status.ToString & vbNewLine
                                c &= "Startup" & vbTab & vbTab
                                c &= cm.ServiceStartType.ToString & vbNewLine & vbNewLine & vbNewLine

                            Catch ex As Exception
                                '  Call Me.ReportFailed(ex)
                            End Try

                            stream.Write(c)

                            x += 1
                            UpdateProgress(x)
                        Next
                        c = CStr(frmMain.lvServices.Items.Count) & " service(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        Dim title As String = CStr(frmMain.lvServices.Items.Count) & " service(s)"
                        Dim _html As New cHTML2(s, title, 25)

                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvServices.Items
                            Dim cm As cService = CType(it.Tag, cService)
                            Try
                                ' Try to access to the service (avoid to write lines if service
                                ' is deleted)
                                Dim suseless As String = cm.Group

                                _html.AppendTitleLine(cm.Name)
                                Dim _lin(1) As String
                                _lin(0) = "Name"
                                _lin(1) = cm.Name
                                _html.AppendLine(_lin)
                                _lin(0) = "Common name"
                                _lin(1) = cm.LongName
                                _html.AppendLine(_lin)
                                _lin(0) = "Path"
                                _lin(1) = cm.ImagePath
                                _html.AppendLine(_lin)
                                _lin(0) = "ObjectName"
                                _lin(1) = cm.ObjectName
                                _html.AppendLine(_lin)
                                _lin(0) = "State"
                                _lin(1) = cm.Status.ToString
                                _html.AppendLine(_lin)
                                _lin(0) = "Startup"
                                _lin(1) = cm.ServiceStartType.ToString
                                _html.AppendLine(_lin)

                                x += 1
                                UpdateProgress(x)

                            Catch ex As Exception
                                '  Call Me.ReportFailed(ex)
                            End Try
                        Next

                        _html.ExportHTML()
                    End If

                    ReportSaved("services")
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub
    Public Sub SaveReportWindows()
        frmMain.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    ' Create file report
                    Dim c As String = vbNullString

                    Me.ReportPath = s
                    Me.pgb.Maximum = frmMain.lvWindows.Items.Count

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Dim stream As New System.IO.StreamWriter(s, False)
                        ' txt
                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvWindows.Items
                            Dim cm As cWindow = CType(it.Tag, cWindow)

                            Try
                                ' Try to access to the window (avoid to write lines if window
                                ' is deleted)
                                Dim suseless As String = cm.Caption

                                c &= "ParentProcess" & vbTab & vbTab & CStr(cm.ParentProcessId) & " -- " & cm.ParentProcessName & vbNewLine
                                c &= "ThreadId" & vbTab & vbTab & CStr(cm.ParentThreadId) & vbNewLine
                                c &= "Window ID" & vbTab & vbTab
                                c &= CStr(cm.Handle) & vbNewLine
                                c &= "Caption" & vbTab & vbTab
                                c &= cm.Caption & vbNewLine
                                c &= "Enabled" & vbTab & vbTab
                                c &= CStr(cm.Enabled) & vbNewLine
                                c &= "Visible" & vbTab & vbTab
                                c &= CStr(cm.Visible) & vbNewLine
                                c &= "IsTask" & vbTab & vbTab
                                c &= CStr(cm.IsTask) & vbNewLine
                                c &= "Opacity" & vbTab & vbTab
                                c &= CStr(cm.Opacity) & vbNewLine
                                c &= "Height" & vbTab & vbTab
                                c &= CStr(cm.Height) & vbNewLine
                                c &= "Left" & vbTab & vbTab
                                c &= CStr(cm.Left) & vbNewLine
                                c &= "Top" & vbTab & vbTab
                                c &= CStr(cm.Top) & vbNewLine
                                c &= "Width" & vbTab & vbTab
                                c &= CStr(cm.Width) & vbNewLine & vbNewLine & vbNewLine

                            Catch ex As Exception
                                '  Call Me.ReportFailed(ex)
                            End Try

                            stream.Write(c)

                            x += 1
                            UpdateProgress(x)
                        Next
                        c = CStr(frmMain.lvWindows.Items.Count) & " windows(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        Dim title As String = CStr(frmMain.lvWindows.Items.Count) & " windows(s)"
                        Dim _html As New cHTML2(s, title, 25)

                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvWindows.Items
                            Dim cm As cWindow = CType(it.Tag, cWindow)
                            Try
                                ' Try to access to the window (avoid to write lines if window
                                ' is deleted)
                                Dim suseless As String = cm.Caption

                                _html.AppendTitleLine("ParentProcess  " & CStr(cm.ParentProcessId) & "  --  " & cm.ParentProcessName)
                                Dim _lin(1) As String
                                _lin(0) = "ThreadID"
                                _lin(1) = CStr(cm.ParentThreadId)
                                _html.AppendLine(_lin)
                                _lin(0) = "Window ID"
                                _lin(1) = CStr(cm.Handle)
                                _html.AppendLine(_lin)
                                _lin(0) = "Caption"
                                _lin(1) = cm.Caption
                                _html.AppendLine(_lin)
                                _lin(0) = "Enabled"
                                _lin(1) = CStr(cm.Enabled)
                                _html.AppendLine(_lin)
                                _lin(0) = "Visible"
                                _lin(1) = CStr(cm.Visible)
                                _html.AppendLine(_lin)
                                _lin(0) = "IsTask"
                                _lin(1) = CStr(cm.IsTask)
                                _html.AppendLine(_lin)
                                _lin(0) = "Opacity"
                                _lin(1) = CStr(cm.Opacity)
                                _html.AppendLine(_lin)
                                _lin(0) = "Height"
                                _lin(1) = CStr(cm.Height)
                                _html.AppendLine(_lin)
                                _lin(0) = "Left"
                                _lin(1) = CStr(cm.Left)
                                _html.AppendLine(_lin)
                                _lin(0) = "Top"
                                _lin(1) = CStr(cm.Top)
                                _html.AppendLine(_lin)
                                _lin(0) = "Width"
                                _html.AppendLine(_lin)
                                _lin(1) = CStr(cm.Width)

                                x += 1
                                UpdateProgress(x)

                            Catch ex As Exception
                                '  Call Me.ReportFailed(ex)
                            End Try
                        Next

                        _html.ExportHTML()
                    End If

                    ReportSaved("windows")
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub
    Public Sub SaveReportThreads()
        frmMain.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    ' Create file report
                    Dim c As String = vbNullString

                    Me.ReportPath = s
                    Me.pgb.Maximum = frmMain.lvThreads.Items.Count

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Dim stream As New System.IO.StreamWriter(s, False)
                        ' txt
                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvThreads.Items
                            Dim cm As cThread = CType(it.Tag, cThread)

                            Try
                                ' Try to access to the thread (avoid to write lines if thread
                                ' is deleted)
                                Dim suseless As String = cm.PriorityString

                                c = "ProcessId" & CStr(cm.Id) & "   --   thread : " & it.Text & vbNewLine
                                c &= "Priority" & vbTab & vbTab
                                c &= cm.PriorityString & vbNewLine
                                c &= "Base priority " & vbTab & vbTab
                                c &= CStr(cm.BasePriority) & vbNewLine
                                c &= "State" & vbTab & vbTab
                                c &= cm.ThreadState & vbNewLine
                                c &= "Wait reason" & vbTab & vbTab
                                c &= cm.WaitReason & vbNewLine
                                c &= "Start address" & vbTab & vbTab
                                c &= CStr(cm.StartAddress) & vbNewLine
                                c &= "PriorityBoostEnabled" & vbTab & vbTab
                                c &= CStr(cm.PriorityBoostEnabled) & vbNewLine
                                c &= "Start time" & vbTab & vbTab
                                c &= cm.StartTime.ToLongDateString & " -- " & cm.StartTime.ToLongTimeString & vbNewLine
                                c &= "TotalProcessorTime" & vbTab & vbTab
                                c &= cm.TotalProcessorTime.ToString & vbNewLine
                                c &= "PrivilegedProcessorTime" & vbTab & vbTab
                                c &= cm.PrivilegedProcessorTime.ToString & vbNewLine
                                c &= "UserProcessorTime" & vbTab & vbTab
                                c &= CStr(cm.UserProcessorTime.ToString) & vbNewLine
                                c &= "ProcessorAffinity" & vbTab & vbTab
                                c &= CStr(cm.ProcessorAffinity) & vbNewLine & vbNewLine & vbNewLine
                            Catch ex As Exception
                                'Call Me.ReportFailed(ex)
                            End Try

                            stream.Write(c)

                            x += 1
                            UpdateProgress(x)
                        Next
                        c = CStr(frmMain.lvThreads.Items.Count) & " thread(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        Dim title As String = CStr(frmMain.lvThreads.Items.Count) & " thread(s)"
                        Dim _html As New cHTML2(s, title, 25)

                        Dim x As Integer = 0
                        Dim it As ListViewItem
                        For Each it In frmMain.lvThreads.Items
                            Dim cm As cThread = CType(it.Tag, cThread)
                            Try
                                ' Try to access to the thread (avoid to write lines if thread
                                ' is deleted)
                                Dim suseless As String = cm.PriorityString

                                _html.AppendTitleLine("ProcessId" & CStr(cm.Id) & "   --   thread : " & it.Text)
                                Dim _lin(1) As String
                                _lin(0) = "Priority"
                                _lin(1) = cm.PriorityString
                                _html.AppendLine(_lin)
                                _lin(0) = "Base priority "
                                _lin(1) = CStr(cm.BasePriority)
                                _html.AppendLine(_lin)
                                _lin(0) = "State"
                                _lin(1) = cm.ThreadState
                                _html.AppendLine(_lin)
                                _lin(0) = "Wait reason"
                                _lin(1) = cm.WaitReason
                                _html.AppendLine(_lin)
                                _lin(0) = "Start address"
                                _lin(1) = CStr(cm.StartAddress)
                                _html.AppendLine(_lin)
                                _lin(0) = "PriorityBoostEnabled"
                                _lin(1) = CStr(cm.PriorityBoostEnabled)
                                _html.AppendLine(_lin)
                                _lin(0) = "Start time"
                                _lin(1) = cm.StartTime.ToLongDateString & " -- " & cm.StartTime.ToLongTimeString
                                _html.AppendLine(_lin)
                                _lin(0) = "TotalProcessorTime"
                                _lin(1) = cm.TotalProcessorTime.ToString
                                _html.AppendLine(_lin)
                                _lin(0) = "PrivilegedProcessorTime"
                                _lin(1) = cm.PrivilegedProcessorTime.ToString
                                _html.AppendLine(_lin)
                                _lin(0) = "UserProcessorTime"
                                _lin(1) = CStr(cm.UserProcessorTime.ToString)
                                _html.AppendLine(_lin)
                                _lin(0) = "ProcessorAffinity"
                                _lin(1) = CStr(cm.ProcessorAffinity)
                                _html.AppendLine(_lin)

                                x += 1
                                UpdateProgress(x)

                            Catch ex As Exception
                                '  Call Me.ReportFailed(ex)
                            End Try
                        Next

                        _html.ExportHTML()
                    End If

                    ReportSaved("threads")
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub
    Public Sub SaveReportHandles()
        frmMain.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    ' Create file report
                    Dim c As String = vbNullString

                    Me.ReportPath = s
                    Me.pgb.Maximum = frmMain.lvHandles.Items.Count

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Dim stream As New System.IO.StreamWriter(s, False)
                        ' txt
                        Dim x As Integer = 0
                        Dim it As ListViewItem
                        For Each it In frmMain.lvHandles.Items
                            c = "Type : " & it.Text
                            c &= "  Process : " & it.SubItems(6).Text
                            c &= "  Name: " & it.SubItems(1).Text
                            c &= "  Handle : " & it.SubItems(5).Text
                            c &= "  HandleCount : " & it.SubItems(2).Text
                            c &= "  PointerCount : " & it.SubItems(3).Text
                            c &= "  ObjectCount : " & it.SubItems(4).Text & vbNewLine
                            stream.Write(c)
                            x += 1
                            UpdateProgress(x)
                        Next
                        c = CStr(frmMain.lvHandles.Items.Count) & " handle(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        Dim col(6) As cHTML.HtmlColumnStructure
                        col(0).sizePercent = 11
                        col(0).title = "Type"
                        col(1).sizePercent = 12
                        col(1).title = "Process"
                        col(2).sizePercent = 41
                        col(2).title = "Name"
                        col(3).sizePercent = 10
                        col(3).title = "Handle"
                        col(4).sizePercent = 10
                        col(4).title = "HandleCount"
                        col(5).sizePercent = 9
                        col(5).title = "PointerCount"
                        col(6).sizePercent = 9
                        col(6).title = "ObjectCount"

                        Dim title As String = CStr(frmMain.lvHandles.Items.Count) & " handle(s)"
                        Dim _html As New cHTML(col, s, title)

                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvHandles.Items
                            Dim _lin(6) As String
                            _lin(0) = it.Text
                            _lin(1) = it.SubItems(6).Text
                            _lin(2) = it.SubItems(1).Text
                            _lin(3) = it.SubItems(5).Text
                            _lin(4) = it.SubItems(2).Text
                            _lin(5) = it.SubItems(3).Text
                            _lin(6) = it.SubItems(4).Text
                            _html.AppendLine(_lin)
                            x += 1
                            UpdateProgress(x)
                        Next

                        _html.ExportHTML()
                    End If

                    ReportSaved("handles")
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub
    Public Sub SaveReportProcesses()

        ReportSaved("processes")
    End Sub
    Public Sub SaveReportModules()
        frmMain.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    ' Create file report
                    Dim c As String = vbNullString

                    Me.ReportPath = s
                    Me.pgb.Maximum = frmMain.lvModules.Items.Count

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Dim stream As New System.IO.StreamWriter(s, False)
                        ' txt
                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvModules.Items
                            Dim cm As cModule = CType(it.Tag, cModule)
                            c = "Module : " & it.Text & "  --  " & cm.FileName & vbNewLine

                            c &= "Process owner" & vbTab & vbTab
                            c &= CStr(cm.ProcessId) & " -- " & cFile.GetFileName(cProcess.GetPath(cm.ProcessId)) & vbNewLine
                            c &= "Version" & vbTab & vbTab
                            c &= cm.FileVersion & vbNewLine
                            c &= "Comments" & vbTab & vbTab
                            c &= cm.Comments & vbNewLine
                            c &= "CompanyName" & vbTab & vbTab
                            c &= cm.CompanyName & vbNewLine
                            c &= "LegalCopyright" & vbTab & vbTab
                            c &= cm.LegalCopyright & vbNewLine
                            c &= "ProductName" & vbTab & vbTab
                            c &= cm.ProductName & vbNewLine
                            c &= "Language" & vbTab & vbTab
                            c &= cm.Language & vbNewLine
                            c &= "InternalName" & vbTab & vbTab
                            c &= cm.InternalName & vbNewLine
                            c &= "LegalTrademarks" & vbTab & vbTab
                            c &= cm.LegalTrademarks & vbNewLine
                            c &= "OriginalFilename" & vbTab & vbTab
                            c &= cm.OriginalFilename & vbNewLine
                            c &= "FileBuildPart" & vbTab & vbTab
                            c &= CStr(cm.FileBuildPart) & vbNewLine
                            c &= "FileDescription" & vbTab & vbTab
                            c &= cm.FileDescription & vbNewLine
                            c &= "FileMajorPart" & vbTab & vbTab
                            c &= CStr(cm.FileMajorPart) & vbNewLine
                            c &= "FileMinorPart" & vbTab & vbTab
                            c &= CStr(cm.FileMinorPart) & vbNewLine
                            c &= "FilePrivatePart" & vbTab & vbTab
                            c &= CStr(cm.FilePrivatePart) & vbNewLine
                            c &= "IsDebug" & vbTab & vbTab
                            c &= CStr(cm.IsDebug) & vbNewLine
                            c &= "IsPatched" & vbTab & vbTab
                            c &= CStr(cm.IsPatched) & vbNewLine
                            c &= "IsPreRelease" & vbTab & vbTab
                            c &= CStr(cm.IsPreRelease) & vbNewLine
                            c &= "IsPrivateBuild" & vbTab & vbTab
                            c &= CStr(cm.IsPrivateBuild) & vbNewLine
                            c &= "IsSpecialBuild" & vbTab & vbTab
                            c &= CStr(cm.IsSpecialBuild) & vbNewLine
                            c &= "PrivateBuild" & vbTab & vbTab
                            c &= cm.PrivateBuild & vbNewLine
                            c &= "ProductBuildPart" & vbTab & vbTab
                            c &= CStr(cm.ProductBuildPart) & vbNewLine
                            c &= "ProductMajorPart" & vbTab & vbTab
                            c &= CStr(cm.ProductMajorPart) & vbNewLine
                            c &= "ProductMinorPart" & vbTab & vbTab
                            c &= CStr(cm.ProductMinorPart) & vbNewLine
                            c &= "ProductPrivatePart" & vbTab & vbTab
                            c &= CStr(cm.ProductPrivatePart) & vbNewLine
                            c &= "ProductVersion" & vbTab & vbTab
                            c &= cm.ProductVersion & vbNewLine
                            c &= "SpecialBuild" & vbTab & vbTab
                            c &= cm.SpecialBuild & vbNewLine & vbNewLine & vbNewLine

                            stream.Write(c)
                            x += 1
                            UpdateProgress(x)
                        Next
                        c = CStr(frmMain.lvModules.Items.Count) & " modules(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        Dim title As String = CStr(frmMain.lvModules.Items.Count) & " modules(s)"
                        Dim _html As New cHTML2(s, title, 25)

                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvModules.Items
                            Dim cm As cModule = CType(it.Tag, cModule)
                            _html.AppendTitleLine("Module : " & it.Text & "  --  " & cm.FileName)
                            Dim _lin(1) As String
                            _lin(0) = "Process owner"
                            _lin(1) = CStr(cm.ProcessId) & " -- " & cFile.GetFileName(cProcess.GetPath(cm.ProcessId))
                            _html.AppendLine(_lin)
                            _lin(0) = "Version"
                            _lin(1) = cm.FileVersion
                            _html.AppendLine(_lin)
                            _lin(0) = "Comments"
                            _lin(1) = cm.Comments
                            _html.AppendLine(_lin)
                            _lin(0) = "CompanyName"
                            _lin(1) = cm.CompanyName
                            _html.AppendLine(_lin)
                            _lin(0) = "LegalCopyright"
                            _lin(1) = cm.LegalCopyright
                            _html.AppendLine(_lin)
                            _lin(0) = "ProductName"
                            _lin(1) = cm.ProductName
                            _html.AppendLine(_lin)
                            _lin(0) = "Language"
                            _lin(1) = cm.Language
                            _html.AppendLine(_lin)
                            _lin(0) = "InternalName"
                            _lin(1) = cm.InternalName
                            _html.AppendLine(_lin)
                            _lin(0) = "LegalTrademarks"
                            _lin(1) = cm.LegalTrademarks
                            _html.AppendLine(_lin)
                            _lin(0) = "OriginalFilename"
                            _lin(1) = cm.OriginalFilename
                            _html.AppendLine(_lin)
                            _lin(0) = "FileBuildPart"
                            _lin(1) = CStr(cm.FileBuildPart)
                            _html.AppendLine(_lin)
                            _lin(0) = "FileDescription"
                            _lin(1) = cm.FileDescription
                            _html.AppendLine(_lin)
                            _lin(0) = "FileMajorPart"
                            _lin(1) = CStr(cm.FileMajorPart)
                            _html.AppendLine(_lin)
                            _lin(0) = "FileMinorPart"
                            _lin(1) = CStr(cm.FileMinorPart)
                            _html.AppendLine(_lin)
                            _lin(0) = "FilePrivatePart"
                            _lin(1) = CStr(cm.FilePrivatePart)
                            _html.AppendLine(_lin)
                            _lin(0) = "IsDebug"
                            _lin(1) = CStr(cm.IsDebug)
                            _html.AppendLine(_lin)
                            _lin(0) = "IsPatched"
                            _lin(1) = CStr(cm.IsPatched)
                            _html.AppendLine(_lin)
                            _lin(0) = "IsPreRelease"
                            _lin(1) = CStr(cm.IsPreRelease)
                            _html.AppendLine(_lin)
                            _lin(0) = "IsPrivateBuild"
                            _lin(1) = CStr(cm.IsPrivateBuild)
                            _html.AppendLine(_lin)
                            _lin(0) = "IsSpecialBuild"
                            _lin(1) = CStr(cm.IsSpecialBuild)
                            _html.AppendLine(_lin)
                            _lin(0) = "PrivateBuild"
                            _lin(1) = cm.PrivateBuild
                            _html.AppendLine(_lin)
                            _lin(0) = "ProductBuildPart"
                            _lin(1) = CStr(cm.ProductBuildPart)
                            _html.AppendLine(_lin)
                            _lin(0) = "ProductMajorPart"
                            _lin(1) = CStr(cm.ProductMajorPart)
                            _html.AppendLine(_lin)
                            _lin(0) = "ProductMinorPart"
                            _lin(1) = CStr(cm.ProductMinorPart)
                            _html.AppendLine(_lin)
                            _lin(0) = "ProductPrivatePart"
                            _lin(1) = CStr(cm.ProductPrivatePart)
                            _html.AppendLine(_lin)
                            _lin(0) = "ProductVersion"
                            _lin(1) = cm.ProductVersion
                            _html.AppendLine(_lin)
                            _lin(0) = "SpecialBuild"
                            _lin(1) = cm.SpecialBuild
                            _html.AppendLine(_lin)

                            x += 1
                            UpdateProgress(x)
                        Next

                        _html.ExportHTML()
                    End If

                    ReportSaved("modules")
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub
    Public Sub SaveReportSearch()
        frmMain.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        Try
            If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim s As String = frmMain.saveDial.FileName
                If Len(s) > 0 Then
                    ' Create file report
                    Dim c As String = vbNullString

                    Me.ReportPath = s
                    Me.pgb.Maximum = frmMain.lvSearchResults.Items.Count

                    If s.Substring(s.Length - 3, 3).ToLower = "txt" Then
                        Dim stream As New System.IO.StreamWriter(s, False)
                        ' txt
                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvSearchResults.Items
                            c = "Type : " & it.Text
                            c &= "  Result : " & it.SubItems(1).Text
                            c &= "  Field : " & it.SubItems(2).Text & vbNewLine
                            stream.Write(c)
                            x += 1
                            UpdateProgress(x)
                        Next
                        c = CStr(frmMain.lvSearchResults.Items.Count) & " result(s)"
                        stream.Write(c)
                        stream.Close()
                    Else
                        ' HTML
                        Dim col(2) As cHTML.HtmlColumnStructure
                        col(0).sizePercent = 22
                        col(0).title = "Type"
                        col(1).sizePercent = 50
                        col(1).title = "Result"
                        col(2).sizePercent = 28
                        col(2).title = "Field"
                        Dim title As String = "Search result for '" & frmMain.txtSearchString.TextBoxText & "' -- " & CStr(frmMain.lvSearchResults.Items.Count) & " result(s)"
                        Dim _html As New cHTML(col, s, title)

                        Dim it As ListViewItem
                        Dim x As Integer = 0
                        For Each it In frmMain.lvSearchResults.Items
                            Dim _lin(2) As String
                            _lin(0) = it.Text
                            _lin(1) = it.SubItems(1).Text
                            _lin(2) = it.SubItems(2).Text
                            _html.AppendLine(_lin)
                            x += 1
                            UpdateProgress(x)
                        Next

                        _html.ExportHTML()
                    End If

                    ReportSaved("search")
                End If
            End If
        Catch ex As Exception
            Call Me.ReportFailed(ex)
        End Try
        Me.cmdGO.Enabled = True
    End Sub
    Public Sub SaveReportMonitoring()

        ReportSaved("monitoring")
    End Sub

    ' Report has been succesfully saved
    Private Sub ReportSaved(ByVal type As String)
        '_repType = type
        Me.lblProgress.Text = "Saving done."
        Me.pgb.Value = Me.pgb.Maximum
        Me.cmdOK.Enabled = True
        Me.cmdOpenReport.Enabled = True
        Me.cmdGO.Enabled = False
        Me.cmdGO.Enabled = True
        MsgBox("Save is ok !", MsgBoxStyle.Information, "Error")
    End Sub

    ' Report saving failed
    Private Sub ReportFailed(ByVal ex As Exception)
        Me.pgb.Value = Me.pgb.Maximum
        Me.cmdOK.Enabled = True
        Me.cmdGO.Enabled = False
        Me.cmdOK.Text = "Quit"
        Me.lblProgress.Text = "Saving failed"
        Me.cmdOpenReport.Enabled = False
        Me.cmdGO.Enabled = True
        MsgBox("Error while saving report : " & ex.Message, MsgBoxStyle.Critical, "Error")
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub cmdOpenReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenReport.Click
        Call cFile.ShellOpenFile(Me._path)
    End Sub

    Private Sub frmSaveReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblProgress.Text = "Waiting for save..."
        With Me.pgb
            .Maximum = 1
            .Minimum = 0
            .Value = 0
        End With
    End Sub

    Private Sub UpdateProgress(ByVal value As Integer)
        Me.pgb.Value = value
        Me.lblProgress.Text = "Computing item " & CStr(value) & "/" & CStr(Me.pgb.Maximum)
        My.Application.DoEvents()
    End Sub

    Private Sub cmdGO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGO.Click
        Me.cmdGO.Enabled = False
        Select Case Me.ReportType
            Case "services"
                Call Me.SaveReportServices()
            Case "windows"
                Call Me.SaveReportWindows()
            Case "modules"
                Call Me.SaveReportModules()
            Case "threads"
                Call Me.SaveReportThreads()
            Case "handles"
                Call Me.SaveReportHandles()
            Case "monitoring"
                Call Me.SaveReportMonitoring()
            Case "processes"
                Call Me.SaveReportProcesses()
            Case "search"
                Call Me.SaveReportSearch()
        End Select
    End Sub
End Class