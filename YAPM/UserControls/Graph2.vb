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

Imports System.Windows.Forms.PictureBox

Public Class Graph2
    Inherits System.Windows.Forms.PictureBox

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _gridStep As Integer = 20
    Private _values() As Double
    Private _colorGrid As System.Drawing.Pen = Pens.DarkGreen
    Private _color As System.Drawing.Pen = Pens.Yellow
    Private _enableGraph As Boolean
    Private _mouseY As Integer
    Private _mouseX As Integer
    Private _xMax As Integer = 50
    Private _xMin As Integer = 0
    Private nCount As Integer = 0
    Private _fixedH As Boolean = False

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
    Public Property Color() As Color
        Get
            Return _color.Color
        End Get
        Set(ByVal value As Color)
            _color = New Pen(value)
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
    Public Property Fixedheight() As Boolean
        Get
            Return _fixedH
        End Get
        Set(ByVal value As Boolean)
            _fixedH = value
        End Set
    End Property
#End Region


    ' ========================================
    ' Overriden subs
    ' ========================================
    Public Sub New()
        ReDim _values(200)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        If _enableGraph Then
            DrawGrid(e.Graphics)
            Try
                DrawValues(e.Graphics)
            Catch ex As Exception
                '
            End Try
            DrawLegend(e.Graphics)
        End If
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        _mouseX = e.X
        _mouseY = e.Y

        '' Get time (milliseconds) of current x point
        'If _values IsNot Nothing Then
        '    Dim h As Integer = CInt(_xMin + e.X / Me.Width * (_values.Length - _xMin))
        '    h = Math.Min(_values.Length - 1, h)
        '    _mouseCurrentDate = 10000 * CInt(_values(h))
        'End If
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
        Dim yMax As Double = 0
        For x = 0 To (_values.Length - 1)
            'For x = _xMin To _xMax
            If _values(x) > yMax Then yMax = _values(x)
        Next

        If yMax = 0 And _fixedH = False Then Exit Sub
        yMax += 1
        Dim yCoef As Double
        If _fixedH = False Then
            yCoef = (Me.Height - 1) / yMax
        Else
            yCoef = (Me.Height - 1) / 100
        End If
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
                y1 = _values(x - 1)
            Else
                x1 = x2
                y1 = y2
            End If
            x2 = v
            y2 = _values(x)

            xx1 = CInt(x1 * xCoef)
            xx2 = CInt(x2 * xCoef)
            yy1 = CInt(Me.Height - y1 * yCoef) + 1
            yy2 = CInt(Me.Height - y2 * yCoef) + 1
            Try
                g.DrawLine(_color, xx1, yy1, xx2, yy2)
            Catch ex As Exception
                '
            End Try
        Next

        '' Calcule current value
        'Dim _mouseCurrentValue As Long
        'Dim h As Integer
        'If _values IsNot Nothing Then
        '    h = CInt(_xMin + _mouseX / Me.Width * (_xMax - _xMin))
        '    h = Math.Min(_values.Length - 1, h)
        '    _mouseCurrentValue = CLng(_values(h))
        'End If

        '' Draw current date
        'g.DrawString("Value : " & _mouseCurrentValue.ToString, frmMain.Font, Brushes.Beige, 200, 0)
    End Sub



    ' ========================================
    ' Public functions
    ' ========================================

    Public Sub AddValue(ByVal value As Integer)
        AddValue(CDbl(value))
    End Sub
    Public Sub AddValue(ByVal value As Double)
        nCount += 1
        If _values.Length < nCount Then
            ReDim Preserve _values(2 * _values.Length)
        End If
        _values(nCount - 1) = value

        ' Calculate new xMin and xMax
        _xMax = nCount - 1
        _xMin = Math.Max(_xMax - 100, 2)

    End Sub

    Public Sub ClearValue()
        ReDim _values(200)
        nCount = 0
    End Sub

End Class