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
    Private _gridStep As Integer = 10
    Private _values() As Double
    Private _colorGrid As System.Drawing.Pen = Pens.DarkGreen
    Private _color As System.Drawing.Pen = Pens.Yellow
    Private _color2 As System.Drawing.Pen = Pens.Yellow
    Private _enableGraph As Boolean
    Private _mouseY As Integer
    Private _mouseX As Integer
    Private _xMax As Integer = 50
    Private _xMin As Integer = 0
    Private nCount As Integer = 0
    Private _fixedH As Boolean = False

    Private xCoef As Double

    Private numberOfValuesDisplayed As Integer
    Private numberOfValuesHidden As Integer
    Private _yMaxValue As Double = 0


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
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property Color2() As Color
        Get
            Return _color2.Color
        End Get
        Set(ByVal value As Color)
            _color2 = New Pen(value)
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
        DrawValuesDown(e.Graphics)
        DrawGrid(e.Graphics)
        If _enableGraph Then
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
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
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        _xMin = Math.Max(_xMax - Me.Width, 0)
        numberOfValuesDisplayed = CInt(Me.Width / 2)
        numberOfValuesHidden = CInt(nCount - numberOfValuesDisplayed)
    End Sub

    ' ========================================
    ' Private methods
    ' ========================================

    ' Draw grid
    Private Sub DrawGrid(ByVal g As Graphics)

        Dim i As Integer = Me.Width
        Dim j As Integer = Me.Height
        Dim stp As Integer = _gridStep

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

        ' Now calculate the Y coeff (height in pxl = VALUE(%) * _yCoeff
        Dim _yCoeff As Double = 0
        If Fixedheight = False Then
            If _yMaxValue > 0 Then
                _yCoeff = (Me.Height - 4) / _yMaxValue
            End If
        Else
            _yCoeff = (Me.Height - 4) / 100
        End If

        ' Now calculate the X coeff
        Dim _xCoeff As Double = 2

        Dim newPxlX As Integer = 0
        Dim newPxlY As Integer = 0
        Dim oldPxlX As Integer = 0
        Dim oldPxlY As Integer = 0
        Dim x As Integer

        ' Now draw lines (upper lines)
        For x = _xMax To _xMin Step -1

            newPxlX = CInt((x - numberOfValuesHidden) * _xCoeff)
            newPxlY = CInt(Me.Height - _values(x) * _yCoeff) - 2

            ' If first line, old = new
            If x = _xMax Then
                oldPxlX = newPxlX
                oldPxlY = newPxlY
            End If

            ' Draw line
            g.DrawLine(_color, newPxlX, newPxlY, oldPxlX, oldPxlY)

            ' Save old X & Y
            oldPxlX = newPxlX
            oldPxlY = newPxlY
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

    Private Sub DrawValuesDown(ByVal g As Graphics)

        ' Calculate maximum of current view
        Dim x As Integer
        For x = _xMax To _xMin Step -1
            If _values(x) > _yMaxValue Then
                _yMaxValue = _values(x)
            End If
        Next

        ' Now calculate the Y coeff (height in pxl = VALUE(%) * _yCoeff
        Dim _yCoeff As Double = 0
        If Fixedheight = False Then
            If _yMaxValue > 0 Then
                _yCoeff = (Me.Height - 4) / _yMaxValue
            End If
        Else
            _yCoeff = (Me.Height - 4) / 100
        End If

        ' Now calculate the X coeff
        Dim _xCoeff As Double = 2

        Dim newPxlX As Integer = 0
        Dim newPxlY As Integer = 0
        Dim oldPxlX As Integer = 0
        Dim oldPxlY As Integer = 0

        ' Now draw lines (lower lines)
        For x = _xMax To _xMin Step -1

            newPxlX = CInt((x - numberOfValuesHidden) * _xCoeff)
            newPxlY = CInt(Me.Height - _values(x) * _yCoeff) - 2

            ' If first line, old = new
            If x = _xMax Then
                oldPxlX = newPxlX
                oldPxlY = newPxlY
            End If

            ' Draw line
            g.DrawLine(_color2, newPxlX, newPxlY, newPxlX, Me.Height - 1)
            g.DrawLine(_color2, newPxlX + 1, newPxlY, newPxlX + 1, Me.Height - 1)

            ' Save old X & Y
            oldPxlX = newPxlX
            oldPxlY = newPxlY
        Next

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
        numberOfValuesDisplayed = CInt(Me.Width / 2)
        numberOfValuesHidden = CInt(nCount - numberOfValuesDisplayed)

        _xMax = nCount - 1      ' Last item by default
        _xMin = CInt(Math.Max(_xMax - numberOfValuesDisplayed, 0))

    End Sub

    Public Sub ClearValue()
        ReDim _values(200)
        nCount = 0
    End Sub

End Class