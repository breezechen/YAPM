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

Public Class cTrayIcon

    Private _values(,) As Byte
    Private _counterPensLine() As Pen
    Private _counterPensFill() As Pen
    Private _countersCount As Integer
    Private _blackRect As New Rectangle(0, 0, 16, 16)

    Private bm As Bitmap
    Private g As Graphics

    Public Sub New(ByVal countersCount As Byte)
        MyBase.New()

        bm = New Bitmap(16, 16)
        g = Graphics.FromImage(bm)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        countersCount = CByte(countersCount - 1)

        _countersCount = countersCount
        ReDim _values(15, countersCount)
        ReDim _counterPensLine(countersCount)
        ReDim _counterPensFill(countersCount)

        For x As Integer = 0 To 15
            For y As Integer = 0 To countersCount
                _values(x, y) = 16
            Next
        Next
    End Sub

    ' Add a counter
    Public Sub SetCounter(ByVal counter As Byte, ByVal lineColor As Color, ByVal fillColor As Color)
        _counterPensLine(counter - 1) = New Pen(lineColor)
        _counterPensFill(counter - 1) = New Pen(fillColor)
    End Sub

    ' Add a value to list
    Public Sub AddValue(ByVal counter As Byte, ByVal percentage As Double)

        ' Move values in _values array
        For x As Integer = 0 To 14
            _values(x, counter - 1) = _values(x + 1, counter - 1)
        Next

        _values(15, counter - 1) = CByte(16 - percentage * 16)

        ' Refresh values
        Call Refresh()
    End Sub

    Private Sub Refresh()

        Dim oIcon As Icon = Nothing

        Try
            ' Create bitmap and graphics
            g.FillRectangle(Brushes.Black, _blackRect)

            ' Draw all counters values
            For y As Integer = _countersCount To 0 Step -1
                For x As Integer = 14 To 0 Step -1
                    g.DrawLine(_counterPensFill(y), x, _values(x, y), x, 16)
                    g.DrawLine(_counterPensLine(y), x, _values(x, y), x + 1, _values(x + 1, y))
                Next
            Next

            ' Get an icon
            oIcon = Icon.FromHandle(bm.GetHicon())

        Catch ex As Exception
            '
        End Try

        ' MUST destroy previous icon to avoid memory exception after long time...
        If frmMain.Tray.Icon IsNot Nothing Then
            API.DestroyIcon(frmMain.Tray.Icon.Handle)
        End If

        ' Set icon
        frmMain.Tray.Icon = oIcon

    End Sub

End Class
