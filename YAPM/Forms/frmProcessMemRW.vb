Option Strict On

Imports System.Runtime.InteropServices

Public Class frmProcessMemRW

    Private m_SortingColumn As ColumnHeader
    Private p() As Double                   ' Contain a value (from 0 to 1)
    Private density As Double               ' Number of bytes in memory per picturebox pixel (width)

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
    End Function

    ' Process edited
    Private _pid As Integer
    Private ba() As Integer
    Private rs() As Integer
    Private minA As Integer
    Private maxA As Integer
    Dim cp As cProcessMemRW

    ' Set edited process
    Public Sub SetProcess(ByVal pid As Integer)
        _pid = pid
        ReDim p(Me.pct.Width)
        Call RefreshProcRegions()
        minA = cp.SystemInfo.lpMinimumApplicationAddress
        maxA = cp.SystemInfo.lpMaximumApplicationAddress
        density = Me.pct.Width / (maxA - minA)
    End Sub

    Private Sub frmProcessMemRW_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetWindowTheme(Me.lv.Handle, "explorer", Nothing)
    End Sub

    ' Get process memory regions
    Public Sub RefreshProcRegions()

        cp = New cProcessMemRW(_pid)
        cp.RetrieveMemRegions(ba, rs)

        Me.lv.Items.Clear()

        If ba.Length > 0 Then
            Dim x As Integer = 0
            For Each i As Integer In ba
                Dim it As New ListViewItem
                it.Text = CStr(i)
                it.Group = Me.lv.Groups(0)
                it.SubItems.Add(CStr(rs(x)))
                Me.lv.Items.Add(it)
                x += 1
            Next
        End If

    End Sub

    Private Sub lv_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lv.ColumnClick
        ' Get the new sorting column.
        Dim new_sorting_column As ColumnHeader = _
            lv.Columns(e.Column)

        ' Figure out the new sorting order.
        Dim sort_order As System.Windows.Forms.SortOrder
        If m_SortingColumn Is Nothing Then
            ' New column. Sort ascending.
            sort_order = SortOrder.Ascending
        Else
            ' See if this is the same column.
            If new_sorting_column.Equals(m_SortingColumn) Then
                ' Same column. Switch the sort order.
                If m_SortingColumn.Text.StartsWith("> ") Then
                    sort_order = SortOrder.Descending
                Else
                    sort_order = SortOrder.Ascending
                End If
            Else
                ' New column. Sort ascending.
                sort_order = SortOrder.Ascending
            End If

            ' Remove the old sort indicator.
            m_SortingColumn.Text = m_SortingColumn.Text.Substring(2)
        End If

        ' Display the new sort order.
        m_SortingColumn = new_sorting_column
        If sort_order = SortOrder.Ascending Then
            m_SortingColumn.Text = "> " & m_SortingColumn.Text
        Else
            m_SortingColumn.Text = "< " & m_SortingColumn.Text
        End If

        ' Create a comparer.
        lv.ListViewItemSorter = New ListViewComparer(e.Column, sort_order)

        ' Sort.
        lv.Sort()
    End Sub

    Private Sub drawGraph(ByVal g As Graphics, ByRef ba() As Integer, ByRef rs() As Integer, _
        ByVal minA As Integer, ByVal maxA As Integer)

        If ba Is Nothing Then
            g.DrawString("No region to display", frmMain.Font, Brushes.Yellow, 3, 3)
            Exit Sub
        End If

        ' For display
        Dim ratio As Double = (maxA - minA) / (Me.pct.Width)

        Dim x As Integer = 0
        Dim x1 As Integer = 0
        Dim wid As Integer = 0
        ReDim p(Me.pct.Width)

        For Each address As Integer In ba
            Dim size As Integer = rs(x)

            ' Get desired pixel(s) in memory from address + size
            Dim nbPxls As Integer = CInt((size) * density)
            Dim firstPxl As Integer = CInt(address * density)   ' First pixel
            For z As Integer = firstPxl To firstPxl + nbPxls - 1
                ' Add number of bytes in this pixel to current value
                If (z + 1) = (firstPxl + nbPxls) Then
                    ' Then last pixel -> all rest
                    p(z) += (size - (nbPxls - 1) * density) * density
                Else
                    ' Then not the last pixel -> add max
                    p(z) += 1
                End If
            Next

            x += 1
        Next

        ' Draw p.length lines
        For z As Integer = 0 To p.Length - 1
            p(z) = Math.Min(p(z), 1)

            ' Color of lines
            Dim r As Integer = CInt(255 * p(z))
            Dim v As Integer = 0
            Dim b As Integer = 0

            Dim pe As New System.Drawing.Pen(Color.FromArgb(r, v, b))
            g.DrawLine(pe, z, 0, z, Me.pct.Height)
        Next

        'x1 = CInt((minA + address) / ratio) - 2
        'wid = Math.Max(CInt(size / ratio), 10)

        'Dim rec As New Rectangle
        'With rec
        '    .X = x1
        '    .Y = 0
        '    .Width = wid
        '    .Height = Me.pct.Height
        'End With

        'For o As Integer = x1 To x1 + wid
        '    ' g.DrawRectangle(Pens.Yellow, rec)
        '    g.DrawLine(Pens.Yellow, o, 0, o, Me.pct.Height)
        'Next

    End Sub


    Private Sub pct_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pct.Paint
        ' Draw graph
        Call drawGraph(e.Graphics, ba, rs, minA, maxA)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SetProcess(CInt(Me.TextBox1.Text))
        Call Me.pct.Refresh()
    End Sub
End Class