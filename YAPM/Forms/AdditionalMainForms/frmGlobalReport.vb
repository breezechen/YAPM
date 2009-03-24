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

Public Class frmGlobalReport

    Private Declare Function GetTickCount Lib "kernel32" () As Integer

    Private Sub UpdateProgress(ByVal x As Integer)
        Me.pgb.Value = x
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        frmMain.saveDial.Filter = "Text file (*.txt)|*.txt"
        frmMain.saveDial.Title = "Save report"
        If frmMain.saveDial.ShowDialog = Windows.Forms.DialogResult.OK Then
            Call saveReport(frmMain.saveDial.FileName)
        End If
    End Sub

    ' Change caption
    Private Sub setCaption(ByVal s As String)
        Me.lblState.Text = s
        'Dim x As Integer = CInt((pgb.Width - Me.pgb.CreateGraphics.MeasureString(s, Me.Font).Width) / 2)
        'Dim y As Integer = CInt((pgb.Height - Me.pgb.CreateGraphics.MeasureString(s, Me.Font).Height) / 2)
        'Me.pgb.CreateGraphics.DrawString(s, Me.Font, Brushes.Black, x, y)
        My.Application.DoEvents()
    End Sub

    ' Save global report
    Public Sub saveReport(ByVal _fileToSave As String)

        ' Services
        ' Handles
        ' Windows
        ' Threads
        ' General
        ' Modules
        ' Memory regions

        Dim stream As New System.IO.StreamWriter(_fileToSave, False)
        Dim _test As Integer = GetTickCount

        ' The goal is to retrieve all informations before saving it
        Dim _dicoServices As New Dictionary(Of String, cService)
        Dim _dicoHandles As New Dictionary(Of String, cHandle)
        Dim _dicoWindows As New Dictionary(Of String, cWindow)
        Dim _dicoProcesses As New Dictionary(Of Integer, cProcess)

        ' == Services
        If Me.chkServices.Checked Then
            setCaption("Retrieving running services...")
            Dim _buffServ As New Dictionary(Of String, cService.LightService)
            Dim _buffServ2 As New Dictionary(Of String, cService.LightService)
            Dim _keyServ() As String
            ReDim _keyServ(0)
            Dim _enumServ As New cServEnum
            Dim _servNumber As Integer = _enumServ.EnumerateApi(_keyServ, _
                                                                _buffServ, _buffServ2)
            For Each it As cService.LightService In _buffServ2.Values
                _dicoServices.Add(it.name, New cService(it, it.name, _enumServ.SCManagerHandle))
            Next
        End If


        ' == Processes
        If True Then
            setCaption("Retrieving opened processes...")
            Dim _buffServ As New Dictionary(Of String, cProcess.LightProcess)
            Dim _keyServ() As String
            ReDim _keyServ(0)
            Dim _servNumber As Integer = cLocalProcess.Enumerate(_keyServ, _buffServ)
            For Each it As cProcess.LightProcess In _buffServ.Values
                _dicoProcesses.Add(it.pid, New cLocalProcess(it))
            Next
        End If



        ' ======== Every informations about processes

        ' pid <-> (Xid <-> lightX)
        Dim _dicoThreads As New Dictionary(Of String, Dictionary(Of String, cThread.LightThread))
        Dim _dicoModules As New Dictionary(Of String, Dictionary(Of String, cModule.MODULEENTRY32))
        Dim _dicoMemRegions As New Dictionary(Of String, Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION))


        ' == Threads
        If Me.chkThreads.Checked Then
            setCaption("Retrieving opened threads...")
            For Each pid As Integer In _dicoProcesses.Keys
                Dim _buffServ As New Dictionary(Of String, cThread.LightThread)
                Dim _threadKey() As String
                ReDim _threadKey(0)
                cThread.Enumerate(pid, _threadKey, _buffServ)
                _dicoThreads.Add(pid.ToString, _buffServ)
            Next
        End If


        ' == Modules
        If Me.chkModules.Checked Then
            setCaption("Retrieving loaded modules...")
            For Each pid As Integer In _dicoProcesses.Keys
                Dim _buffServ As New Dictionary(Of String, cModule.MODULEENTRY32)
                Dim _moduleKey() As String
                ReDim _moduleKey(0)
                cModule.Enumerate(pid, _moduleKey, _buffServ)
                _dicoModules.Add(pid.ToString, _buffServ)
            Next
        End If


        ' == Memory regions
        If Me.chkMemory.Checked Then
            setCaption("Retrieving memory regions...")
            For Each pid As Integer In _dicoProcesses.Keys
                Dim _buffServ As New Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)
                Dim _memKey() As String
                ReDim _memKey(0)
                cProcessMemRW.Enumerate(pid, _memKey, _buffServ)
                _dicoMemRegions.Add(pid.ToString, _buffServ)
            Next
        End If


        ' == Write general informations to log
        setCaption("Writing header to report...")
        stream.WriteLine("Report generated by Yet Another Process Monitor")
        stream.WriteLine("File version : " & My.Application.Info.Version.ToString)
        stream.WriteLine(Date.Now.ToLongDateString & " -- " & Date.Now.ToLongTimeString)
        stream.WriteLine()
        stream.WriteLine()
        stream.WriteLine()


        ' == Write informations about system


        ' == Write services informations
        If Me.chkServices.Checked Then
            setCaption("Writing services list to report...")
            stream.WriteLine("================================")
            stream.WriteLine("==      Running services      ==")
            stream.WriteLine("================================")
            stream.WriteLine()
            stream.WriteLine(_dicoServices.Count.ToString & " services running")
            stream.WriteLine()
            For Each it As cService In _dicoServices.Values
                stream.WriteLine("--------------------------------")
                stream.WriteLine("Service name : " & it.Name)
                stream.WriteLine("--------------------------------")
                For Each s As String In cService.GetAvailableProperties
                    stream.WriteLine(s & " : " & it.GetInformation(s))
                Next
                stream.WriteLine()
            Next
            stream.WriteLine()
            stream.WriteLine()
            stream.WriteLine()
        End If


        ' == Write informations about processes
        setCaption("Writing processes infos to report...")
        stream.WriteLine("================================")
        stream.WriteLine("==      Running processes     ==")
        stream.WriteLine("================================")
        stream.WriteLine()
        stream.WriteLine(_dicoProcesses.Count.ToString & " processes running")
        stream.WriteLine()
        Me.pgb.Maximum = _dicoProcesses.Count
        Dim x As Integer = 0


        For Each it As cProcess In _dicoProcesses.Values
            x += 1
            Me.UpdateProgress(x)
            stream.WriteLine()
            stream.WriteLine("--------------------------------")
            stream.WriteLine("Process name : " & it.Name)
            stream.WriteLine("--------------------------------")
            For Each s As String In cProcess.GetAvailableProperties
                stream.WriteLine(s & " : " & it.GetInformation(s))
            Next
            stream.WriteLine()
            stream.WriteLine()

            If Me.chkThreads.Checked Then
                If _dicoThreads.ContainsKey(it.Pid.ToString) Then
                    stream.WriteLine(_dicoThreads(it.Pid.ToString).Count & " threads running")
                    stream.WriteLine()
                    If Me.chkFull.Checked Then
                        For Each it2 As cThread.LightThread In _dicoThreads(it.Pid.ToString).Values
                            stream.WriteLine("---------------")
                            stream.WriteLine("Threads id : " & it2.t.Id.ToString)
                            stream.WriteLine("---------------")
                            Dim __t As cThread = New cThread(it2)
                            For Each s As String In cThread.GetAvailableProperties
                                stream.WriteLine(s & " : " & __t.GetInformation(s))
                            Next
                        Next
                    Else
                        For Each it2 As cThread.LightThread In _dicoThreads(it.Pid.ToString).Values
                            stream.WriteLine("Threads id : " & it2.t.Id.ToString)
                        Next
                    End If
                    stream.WriteLine()
                End If
            End If

            If Me.chkModules.Checked Then
                If _dicoModules.ContainsKey(it.Pid.ToString) Then
                    stream.WriteLine(_dicoModules(it.Pid.ToString).Count & " modules loaded")
                    stream.WriteLine()
                    If Me.chkFull.Checked Then
                        For Each it2 As cModule.MODULEENTRY32 In _dicoModules(it.Pid.ToString).Values
                            stream.WriteLine("---------------")
                            stream.WriteLine("Module name : " & it2.szModule)
                            stream.WriteLine("---------------")
                            Dim __t As cModule = New cModule("no_need", it2)
                            For Each s As String In cModule.GetAvailableProperties
                                stream.WriteLine(s & " : " & __t.GetInformation(s))
                            Next
                        Next
                    Else
                        For Each it2 As cModule.MODULEENTRY32 In _dicoModules(it.Pid.ToString).Values
                            stream.WriteLine("Module : " & it2.szExePath)
                        Next
                    End If
                    stream.WriteLine()
                End If
            End If

            If Me.chkMemory.Checked Then
                If _dicoMemRegions.ContainsKey(it.Pid.ToString) Then
                    stream.WriteLine(_dicoMemRegions(it.Pid.ToString).Count & " memory regions")
                    stream.WriteLine()
                    If Me.chkFull.Checked Then
                        For Each it2 As cProcessMemRW.MEMORY_BASIC_INFORMATION In _dicoMemRegions(it.Pid.ToString).Values
                            stream.WriteLine("---------------")
                            stream.WriteLine("Region address : 0x" & it2.BaseAddress.ToString("x"))
                            stream.WriteLine("---------------")
                            Dim __t As cMemRegion = New cMemRegion("no_need", it2, it.Pid)
                            For Each s As String In cMemRegion.GetAvailableProperties
                                stream.WriteLine(s & " : " & __t.GetInformation(s))
                            Next
                        Next
                    Else
                        For Each it2 As cProcessMemRW.MEMORY_BASIC_INFORMATION In _dicoMemRegions(it.Pid.ToString).Values
                            stream.WriteLine("Address : 0x" & it2.BaseAddress.ToString("x") & " -- Size : " & GetFormatedSize(it2.RegionSize))
                        Next
                    End If
                    stream.WriteLine()
                End If
            End If

            If Me.chkWindows.Checked Then
                Dim _buffServ As New Dictionary(Of String, cWindow.LightWindow)
                Dim _keyServ() As Integer
                ReDim _keyServ(0)
                Dim __pid(0) As Integer
                __pid(0) = it.Pid
                Dim _servNumber As Integer = cWindow.Enumerate(True, __pid, _keyServ, _buffServ)
                _dicoWindows.Clear()
                For Each i0t As cWindow.LightWindow In _buffServ.Values
                    _dicoWindows.Add(i0t.handle.ToString, New cWindow(i0t))
                Next
                stream.WriteLine(_dicoWindows.Count & " opened windows")
                stream.WriteLine()
                If Me.chkFull.Checked Then
                    For Each it2 As cWindow In _dicoWindows.Values
                        If Me.chkAllWindows.Checked OrElse Len(it2.Caption) > 0 Then
                            stream.WriteLine("---------------")
                            stream.WriteLine("Window handle : " & it2.Handle.ToString)
                            stream.WriteLine("---------------")
                            For Each s As String In cWindow.GetAvailableProperties
                                stream.WriteLine(s & " : " & it2.GetInformation(s))
                            Next
                        End If
                    Next
                Else
                    For Each it2 As cWindow In _dicoWindows.Values
                        If Me.chkAllWindows.Checked OrElse Len(it2.Caption) > 0 Then
                            stream.WriteLine("Handle : " & it2.Handle.ToString & " -- Visible : " & it2.Visible.ToString & " -- Task : " & it2.IsTask.ToString & " -- Caption : " & it2.Caption)
                        End If
                    Next
                End If
                stream.WriteLine()
            End If

            If Me.chkHandles.Checked Then
                Dim _buffServ As New Dictionary(Of String, cHandle.LightHandle)
                Dim _keyServ() As String
                ReDim _keyServ(0)
                Dim __pid(0) As Integer
                __pid(0) = it.Pid
                Dim _servNumber As Integer = cHandle.Enumerate(__pid, True, _keyServ, _buffServ)
                _dicoHandles.Clear()
                For Each i0t As cHandle.LightHandle In _buffServ.Values
                    Dim _key As String = cHandle.GetKeyFromLight(i0t)
                    _dicoHandles.Add(_key, New cHandle(_key, i0t))
                Next
                'If _dicoHandles.ContainsKey(it.Pid.ToString) Then
                stream.WriteLine(_dicoHandles.Count & " handles opened")
                stream.WriteLine()
                If Me.chkFull.Checked Then
                    For Each it2 As cHandle In _dicoHandles.Values
                        If Me.chkAllHandles.Checked OrElse Len(it2.Name) > 0 Then
                            stream.WriteLine("---------------")
                            stream.WriteLine("Handle : " & it2.Handle.ToString)
                            stream.WriteLine("---------------")
                            For Each s As String In cHandle.GetAvailableProperties
                                stream.WriteLine(s & " : " & it2.GetInformation(s))
                            Next
                        End If
                    Next
                Else
                    For Each it2 As cHandle In _dicoHandles.Values
                        If Me.chkAllHandles.Checked OrElse Len(it2.Name) > 0 Then
                            stream.WriteLine("Handle : " & it2.Handle.ToString & " -- Type : " & it2.Type & " -- Name : " & it2.Name)
                        End If
                    Next
                End If
                stream.WriteLine()
            End If
            ' End If
        Next
        stream.WriteLine()


        stream.Close()
        _test = GetTickCount - _test
        setCaption("Done in " & _test.ToString & " ms")

        MsgBox("Report saved !", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "General report")

    End Sub

    Private Sub frmGlobalReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        frmMain.SetToolTip(Me.chkAllHandles, "Get all handles (including unnamed handles)")
        frmMain.SetToolTip(Me.chkAllWindows, "Get all windows (including unnamed windows)")
        frmMain.SetToolTip(Me.chkFull, "Write a complet report, with all informations availables. You should NOT use this option because report file can be up to 100MB")
        frmMain.SetToolTip(Me.chkHandles, "Write handles infos")
        frmMain.SetToolTip(Me.chkMemory, "Write memory regions infos")
        frmMain.SetToolTip(Me.chkModules, "Write modules infos")
        frmMain.SetToolTip(Me.chkServices, "Write services infos")
        frmMain.SetToolTip(Me.chkThreads, "Write threads infos")
        frmMain.SetToolTip(Me.chkWindows, "Write windows infos")
        frmMain.SetToolTip(Me.cmdCancel, "Cancel")
        frmMain.SetToolTip(Me.cmdSave, "Save report as a text file")
    End Sub
End Class