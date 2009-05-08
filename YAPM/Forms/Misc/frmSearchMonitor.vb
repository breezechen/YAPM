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

Public Class frmSearchMonitor

    Private Sub frmSearchMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        closeWithEchapKey(Me)

        API.SetWindowTheme(Me.LV.Handle, "explorer", Nothing)
    End Sub

    ' Launch search
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim s As String = Me.txtToSearch.Text
        If Me.chkCase.Checked Then
            s = s.ToLowerInvariant
        End If
        If Len(s) = 0 Then Exit Sub

        Me.LV.Items.Clear()
        Me.LV.BeginUpdate()

        ' List all categories
        Dim myCat2 As PerformanceCounterCategory()
        myCat2 = PerformanceCounterCategory.GetCategories
        For j As Integer = 0 To myCat2.Length - 1

            ' Found a category
            Dim category As String = myCat2(j).CategoryName
            Dim sComp As String = category
            If Me.chkCase.Checked = False Then
                sComp = sComp.ToLowerInvariant
            End If
            If InStr(sComp, s, CompareMethod.Binary) > 0 Then
                Dim it As New ListViewItem(category)
                it.Group = Me.LV.Groups.Item("groupCategory")
                Me.LV.Items.Add(it)
            End If

            ' List all counters without instance
            Dim myCat As New PerformanceCounterCategory(category)
            Dim mypc() As PerformanceCounter
            Try
                mypc = myCat.GetCounters()
                For i As Integer = 0 To mypc.Length - 1
                    ' Found a counter

                    Dim sComp2 As String = mypc(i).CounterName
                    If Me.chkCase.Checked = False Then
                        sComp2 = sComp2.ToLowerInvariant
                    End If
                    If InStr(sComp2, s, CompareMethod.Binary) > 0 Then
                        Dim it As New ListViewItem(category & "->" & mypc(i).CounterName)
                        it.Group = Me.LV.Groups.Item("groupCounter")
                        Me.LV.Items.Add(it)
                    End If

                Next
            Catch ex As Exception
                '
            End Try

            ' List all counters with an instance
            ' Get first instance of category
            Dim myCat3 As New PerformanceCounterCategory(category)
            Dim myPc2() As String

            Try
                myPc2 = myCat3.GetInstanceNames
                If myPc2.Length > 0 Then
                    Dim instance As String = myPc2(0)
                    ' Now get all counters with this instance
                    Dim myCat4 As New PerformanceCounterCategory(category)
                    Dim mypc4() As PerformanceCounter
                    Try
                        mypc4 = myCat4.GetCounters(instance)
                        For o As Integer = 0 To mypc4.Length - 1
                            Dim scomp3 As String = mypc4(o).CounterName

                            If Me.chkCase.Checked = False Then
                                scomp3 = scomp3.ToLowerInvariant
                            End If
                            If InStr(scomp3, s, CompareMethod.Binary) > 0 Then
                                Dim it As New ListViewItem(category & "->" & mypc4(o).CounterName)
                                it.Group = Me.LV.Groups.Item("groupCounter")
                                Me.LV.Items.Add(it)
                            End If

                        Next
                    Catch ex As Exception
                        '
                    End Try
                End If
            Catch ex As Exception
                '
            End Try

        Next

        Me.LV.EndUpdate()
    End Sub
End Class