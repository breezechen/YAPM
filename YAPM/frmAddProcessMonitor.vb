Option Strict On

Public Class frmAddProcessMonitor

    ' Process to select by default
    Public _selProcess As Integer

    Private Structure monCounter
        Dim instanceName As String
        Dim counterTypeName As String
        Dim categoryName As String
    End Structure

    Private Sub frmAddProcessMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        With frmMain
            .SetToolTip(Me.butAdd, "Monitor the selected process.")
            .SetToolTip(Me.txtInterval, "Set the refresh interval (milliseconds).")
        End With

        'Call Me.cmdRefresh_Click(Nothing, Nothing)

        '' Select desired process (_selProcess)
        'Dim s As String
        'For Each s In Me.cbProcess.Items
        '    Dim i As Integer = InStr(s, " -- ", CompareMethod.Binary)
        '    Dim _name As String = s.Substring(0, i - 1)
        '    Dim _pid As Integer = CInt(Val(s.Substring(i + 3, s.Length - i - 3)))
        '    If _pid = _selProcess Then
        '        Me.cbProcess.Text = s
        '        Exit For
        '    End If
        'Next

        Dim myCat2 As PerformanceCounterCategory()
        Dim i As Integer
        Me.lstCategory.Items.Clear()
        myCat2 = PerformanceCounterCategory.GetCategories
        For i = 0 To myCat2.Length - 1
            Me.lstCategory.Items.Add(myCat2(i).CategoryName)
        Next

    End Sub

    Private Sub butAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAdd.Click
        ' Monitor our process
        Dim lstIt As ListViewItem

        For Each lstIt In Me.lstToAdd.Items

            Dim obj As monCounter = CType(lstIt.Tag, Global.YAPM.frmAddProcessMonitor.monCounter)

            With obj
                Dim _name As String = .instanceName
                Dim _cat As String = .categoryName
                Dim _count As String = .counterTypeName

                Dim it As New cMonitor(_cat, _count, _name)
                it.Interval = CInt(Val(Me.txtInterval.Text))
                frmMain.AddMonitoringItem(it)
            End With

        Next

        Me.Close()

    End Sub

    Private Sub lstCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCategory.SelectedIndexChanged
        Dim mypc() As String
        Dim i As Integer
        Dim myCat As New PerformanceCounterCategory(lstCategory.SelectedItem.ToString)
        Me.lstInstance.Items.Clear()
        Me.lstCounterType.Items.Clear()
        Try
            mypc = myCat.GetInstanceNames
            For i = 0 To mypc.Length - 1
                Me.lstInstance.Items.Add(mypc(i))
            Next
        Catch ex As Exception
        End Try

        Call lstInstance_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub lstInstance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstInstance.SelectedIndexChanged
        Dim mypc() As PerformanceCounter
        Dim i As Integer
        Me.lstCounterType.Items.Clear()
        If lstInstance.SelectedItem Is Nothing Then
            Dim myCat As New PerformanceCounterCategory(lstCategory.SelectedItem.ToString)
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetCounters()
                For i = 0 To mypc.Length - 1
                    Me.lstCounterType.Items.Add(mypc(i).CounterName)
                Next
            Catch ex As Exception
            End Try
        Else
            Dim myCat As New PerformanceCounterCategory(lstCategory.SelectedItem.ToString)
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetCounters(lstInstance.SelectedItem.ToString)
                For i = 0 To mypc.Length - 1
                    Me.lstCounterType.Items.Add(mypc(i).CounterName)
                Next
            Catch ex As Exception
            End Try
        End If
        Me.butAdd.Enabled = (Me.lstCounterType.SelectedItems.Count > 0)
    End Sub

    Private Sub cmdAddToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddToList.Click
        ' Add selected counters to wish list
        Dim listIt As Object

        Dim _name As String = vbNullString
        Dim _cat As String = vbNullString
        Dim _count As String = vbNullString
        If Me.lstCategory.SelectedItem Is Nothing Then Exit Sub

        For Each listIt In Me.lstCounterType.CheckedItems

            _count = listIt.ToString
            If Me.lstInstance.SelectedItem IsNot Nothing Then _name = Me.lstInstance.SelectedItem.ToString
            If _count = vbNullString And _name = vbNullString Then Exit Sub
            _cat = Me.lstCategory.SelectedItem.ToString

            Dim it As New monCounter

            With it
                .categoryName = _cat
                .counterTypeName = _count
                .instanceName = _name
            End With

            Dim sName As String = _cat & " -- " & CStr(IIf(_name = vbNullString, vbNullString, _name & " -- ")) & _count
            Dim bPresent As Boolean = False

            ' Check if item is already added or not
            Dim lvIt As ListViewItem
            For Each lvIt In Me.lstToAdd.Items
                If lvIt.Text = sName Then
                    bPresent = True
                    Exit For
                End If
            Next

            If bPresent = False Then
                Dim itList As New ListViewItem
                itList.Text = sName
                itList.Tag = it
                Me.lstToAdd.Items.Add(itList)
            End If
        Next

        Me.butAdd.Enabled = (Me.lstCounterType.Items.Count > 0)
    End Sub

    Private Sub cmdRemoveFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveFromList.Click
        Dim it As ListViewItem
        For Each it In Me.lstToAdd.SelectedItems
            it.Remove()
        Next
        Me.butAdd.Enabled = (Me.lstCounterType.Items.Count > 0)
    End Sub
End Class