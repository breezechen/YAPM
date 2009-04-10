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

Imports System.Windows.Forms.PictureBox

Public Class Graph
    Inherits System.Windows.Forms.PictureBox

    ' ========================================
    ' Public structure
    ' ========================================
    Public Structure ValueItem
        Dim x As Long
        Dim y As Long
    End Structure

    Public Event OnZoom(ByVal leftVal As Integer, ByVal rightVal As Integer)

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _gridStep As Integer = 20
    Private _values() As ValueItem
    Private _values2() As ValueItem
    Private _values3() As ValueItem
    Private _colorGrid As System.Drawing.Pen = Pens.DarkGreen
    Private _colorCPUPercent As System.Drawing.Pen = Pens.Yellow
    Private _colorCPUTime As System.Drawing.Pen = Pens.Yellow
    Private _colorMemory1 As System.Drawing.Pen = Pens.Yellow
    Private _colorMemory3 As System.Drawing.Pen = Pens.Orange
    Private _colorMemory2 As System.Drawing.Pen = Pens.Blue
    Private _colorPriority As System.Drawing.Pen = Pens.Yellow
    Private _colorThreads As System.Drawing.Pen = Pens.Yellow
    Private _colorLegend As System.Drawing.Pen = Pens.Red
    Private _xMin As Integer
    Private _xMax As Integer
    Private _date As Date

    Private _xDown As Integer
    Private _yDown As Integer
    Private _down As Boolean
    Private _mouseY As Integer
    Private _mouseX As Integer
    Private _mouseCurrentDate As Integer
    Private _zoomRect As Rectangle
    Private _enableGraph As Boolean

    Private xCoef As Double



    ' ========================================
    ' Properties
    ' ========================================
#Region "Properties"
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(20)> _
    Public Property GridStep() As Integer
        Get
            Return _gridStep
        End Get
        Set(ByVal value As Integer)
            If value > 0 Then
                _gridStep = value
            End If
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "DarkGreen")> _
    Public Property ColorGrid() As Color
        Get
            Return _colorGrid.Color
        End Get
        Set(ByVal value As Color)
            _colorGrid = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property ColorCPUPercent() As Color
        Get
            Return _colorCPUPercent.Color
        End Get
        Set(ByVal value As Color)
            _colorCPUPercent = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property ColorCPUTime() As Color
        Get
            Return _colorCPUTime.Color
        End Get
        Set(ByVal value As Color)
            _colorCPUTime = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Red")> _
    Public Property ColorLegend() As Color
        Get
            Return _colorLegend.Color
        End Get
        Set(ByVal value As Color)
            _colorLegend = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property ColorPriority() As Color
        Get
            Return _colorPriority.Color
        End Get
        Set(ByVal value As Color)
            _colorPriority = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property ColorThreads() As Color
        Get
            Return _colorThreads.Color
        End Get
        Set(ByVal value As Color)
            _colorThreads = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property ColorMemory1() As Color
        Get
            Return _colorMemory1.Color
        End Get
        Set(ByVal value As Color)
            _colorMemory1 = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Red")> _
    Public Property ColorMemory2() As Color
        Get
            Return _colorMemory2.Color
        End Get
        Set(ByVal value As Color)
            _colorMemory2 = New Pen(value)
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Blue")> _
    Public Property ColorMemory3() As Color
        Get
            Return _colorMemory3.Color
        End Get
        Set(ByVal value As Color)
            _colorMemory3 = New Pen(value)
        End Set
    End Property
    Public Property ViewMax() As Integer
        Get
            Return _xMax
        End Get
        Set(ByVal value As Integer)
            _xMax = value
        End Set
    End Property
    Public Property ViewMin() As Integer
        Get
            Return _xMin
        End Get
        Set(ByVal value As Integer)
            _xMin = value
        End Set
    End Property
    Public Property dDate() As Date
        Get
            Return _date
        End Get
        Set(ByVal value As Date)
            _date = value
        End Set
    End Property
    Public Property EnableGraph() As Boolean
        Get
            Return _enableGraph
        End Get
        Set(ByVal value As Boolean)
            _enableGraph = value
        End Set
    End Property
#End Region


    ' ========================================
    ' Overriden subs
    ' ========================================
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        If _enableGraph Then
            DrawGrid(e.Graphics)
            DrawValues(e.Graphics)
            If _values2 IsNot Nothing Then DrawValues2(e.Graphics)
            If _values3 IsNot Nothing Then DrawValues3(e.Graphics)
            DrawLegend(e.Graphics)
        End If
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            _xDown = e.X
            _yDown = e.Y
            _down = True
        End If
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        _mouseX = e.X
        _mouseY = e.Y
        If _down = True Then
            Me.Refresh()
            ' Draw a zoom rectangle
            With _zoomRect
                .X = Math.Min(_xDown, e.X)
                .Y = Math.Min(_yDown, e.Y)
                .Width = Math.Abs(_xDown - e.X)
                .Height = Math.Abs(_yDown - e.Y)
            End With
            Me.CreateGraphics.DrawRectangle(Pens.WhiteSmoke, _zoomRect)
        End If

        ' Get time (milliseconds) of current x point
        If _values IsNot Nothing Then
            Dim h As Integer = CInt(_xMin + e.X / Me.Width * (_values.Length - _xMin))
            h = Math.Min(_values.Length - 1, h)
            _mouseCurrentDate = 10000 * CInt(_values(h).x)
        End If
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        ' We have to zoom
        If _down And e.Button = Windows.Forms.MouseButtons.Right Then
            _down = False
            Me.Refresh()        ' Delete rectangle

            If _values Is Nothing Then
                _down = False
                Exit Sub
            End If

            Dim ul As Integer = CInt(_xMin + Math.Min(e.X, _xDown) / Me.Width * (_values.Length - _xMin))
            Dim ur As Integer = CInt(_xMin + Math.Max(e.X, _xDown) / Me.Width * (_values.Length - _xMin))
            ur = Math.Min(ur, _values.Length - 1)
            Dim l As Integer = 0
            Dim r As Integer = 0

            ' Find times from ul and ur (positions in array)
            l = CInt(_values(ul).x)
            r = CInt(_values(ur).x)

            RaiseEvent OnZoom(l, r)
        End If
    End Sub

    ' ========================================
    ' Private methods
    ' ========================================

    ' Draw grid
    Private Sub DrawGrid(ByVal g As Graphics)

        Dim i As Integer = Me.Width
        Dim j As Integer = Me.Height
        Dim stp As Integer = 20

        Dim x, y As Integer

        ' Vertical lines
        For x = i To 0 Step -stp
            g.DrawLine(_colorGrid, x, 0, x, j)
        Next

        ' Horizontal lines
        For y = j To 0 Step -stp
            g.DrawLine(_colorGrid, 0, y, i, y)
        Next
    End Sub

    ' Draw legend
    Private Sub DrawLegend(ByVal g As Graphics)
        '
    End Sub

    ' Draw values
    Private Sub DrawValues(ByVal g As Graphics)
        Dim x As Integer

        If _values Is Nothing Then Exit Sub

        ' Get the max (height)
        Dim yMax As Long = 0
        For x = 0 To (_values.Length - 1)
            If _values(x).y > yMax Then yMax = _values(x).y
        Next

        If yMax = 0 Then Exit Sub
        yMax += 1

        Dim yCoef As Double = (Me.Height - 25) / yMax
        xCoef = Me.Width / (_xMax - _xMin)

        Dim x1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y1 As Double = yMax
        Dim y2 As Double = y1
        Dim v As Integer

        Dim xx1 As Integer = 0
        Dim xx2 As Integer = 0
        Dim yy1 As Integer = 0
        Dim yy2 As Integer = 0

        For x = _xMin + 1 To _xMax
            ' v start at 0
            v = x - _xMin

            If (x1 = 0 And y1 = yMax) Then
                x1 = 0
                y1 = _values(x - 1).y
            Else
                x1 = x2
                y1 = y2
            End If
            x2 = v
            y2 = _values(x).y

            xx1 = CInt(x1 * xCoef)
            xx2 = CInt(x2 * xCoef)
            yy1 = CInt(Me.Height - y1 * yCoef) + 10
            yy2 = CInt(Me.Height - y2 * yCoef) + 10
            g.DrawLine(_colorMemory1, xx1, yy1, xx2, yy2)
        Next

        ' Draw zoom rectangle
        If _down = True Then g.DrawRectangle(Pens.WhiteSmoke, _zoomRect)

        ' Calcule current value
        Dim _mouseCurrentValue As Long
        Dim h As Integer
        If _values IsNot Nothing Then
            h = CInt(_xMin + _mouseX / Me.Width * (_xMax - _xMin))
            h = Math.Min(_values.Length - 1, h)
            _mouseCurrentValue = _values(h).y
        End If

        ' Draw current date
        Dim d As New Date(_date.Ticks + _mouseCurrentDate)
        g.DrawString(d.ToLongDateString & " -- " & d.ToLongTimeString, frmMain.Font, Brushes.Beige, 0, 0)
        g.DrawString("Value : " & _mouseCurrentValue.ToString, frmMain.Font, Brushes.Beige, 200, 0)
    End Sub

    Private Sub DrawValues2(ByVal g As Graphics)
        Dim x As Integer

        ' Get the max (height)
        Dim yMax As Long = 0
        For x = 0 To (_values2.Length - 1)
            If _values2(x).y > yMax Then yMax = _values2(x).y
        Next

        If yMax = 0 Then Exit Sub
        yMax += 1

        Dim yCoef As Double = (Me.Height - 25) / yMax

        Dim x1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y1 As Double = yMax
        Dim y2 As Double = y1
        Dim v As Integer

        Dim xx1 As Integer = 0
        Dim xx2 As Integer = 0
        Dim yy1 As Integer = 0
        Dim yy2 As Integer = 0

        For x = _xMin + 1 To _xMax
            ' v start at 0
            v = x - _xMin

            If (x1 = 0 And y1 = yMax) Then
                x1 = 0
                y1 = _values2(x - 1).y
            Else
                x1 = x2
                y1 = y2
            End If
            x2 = v
            y2 = _values2(x).y

            xx1 = CInt(x1 * xCoef)
            xx2 = CInt(x2 * xCoef)
            yy1 = CInt(Me.Height - y1 * yCoef) + 10
            yy2 = CInt(Me.Height - y2 * yCoef) + 10
            g.DrawLine(_colorMemory2, xx1, yy1, xx2, yy2)
        Next

    End Sub

    Private Sub DrawValues3(ByVal g As Graphics)
        Dim x As Integer

        ' Get the max (height)
        Dim yMax As Long = 0
        For x = 0 To (_values3.Length - 1)
            If _values3(x).y > yMax Then yMax = _values3(x).y
        Next

        If yMax = 0 Then Exit Sub
        yMax += 1

        Dim yCoef As Double = (Me.Height - 25) / yMax

        Dim x1 As Integer = 0
        Dim x2 As Integer = 0
        Dim y1 As Double = yMax
        Dim y2 As Double = y1
        Dim v As Integer

        Dim xx1 As Integer = 0
        Dim xx2 As Integer = 0
        Dim yy1 As Integer = 0
        Dim yy2 As Integer = 0

        For x = _xMin + 1 To _xMax
            ' v start at 0
            v = x - _xMin

            If (x1 = 0 And y1 = yMax) Then
                x1 = 0
                y1 = _values2(x - 1).y
            Else
                x1 = x2
                y1 = y2
            End If
            x2 = v
            y2 = _values3(x).y

            xx1 = CInt(x1 * xCoef)
            xx2 = CInt(x2 * xCoef)
            yy1 = CInt(Me.Height - y1 * yCoef) + 10
            yy2 = CInt(Me.Height - y2 * yCoef) + 10
            g.DrawLine(_colorMemory3, xx1, yy1, xx2, yy2)
        Next

    End Sub


    ' ========================================
    ' Public functions
    ' ========================================

    Public Sub SetValues(ByVal values() As ValueItem)
        _values = values
    End Sub
    Public Sub SetValues2(ByVal values() As ValueItem)
        _values2 = values
    End Sub
    Public Sub SetValues3(ByVal values() As ValueItem)
        _values3 = values
    End Sub

End Class