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

Public Class GraphChart
    Inherits PictureBox

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _gridStep As Integer = 13
    Private _values As New List(Of Double)
    Private _values2 As New List(Of Double)
    Private _times As New List(Of Long)
    Private _colorGrid As System.Drawing.Pen = Pens.DarkGreen
    Private _color1 As System.Drawing.Pen = Pens.Yellow
    Private _colorFill1 As System.Drawing.Pen = Pens.Olive
    Private _color2 As System.Drawing.Pen = Pens.Red
    Private _colorFill2 As System.Drawing.Pen = Pens.DarkRed
    Private _textColor As System.Drawing.Pen = Pens.Lime
    Private _enableGraph As Boolean = True
    Private _fill1 As Boolean = True
    Private _fill2 As Boolean = False
    Private _drawGrid As Boolean = True
    Private nCount As Integer = 0
    Private _fixedH As Boolean = False
    Private _secondV As Boolean = False
    Private _text As String
    Private _showToolTip As Boolean
    Private _mouseLoc As Point

    Private numberOfValuesDisplayable As Integer
    Private numberOfValuesHidden As Integer
    Private WithEvents toolTip As New System.Windows.Forms.ToolTip
    Private _yMaxValue As Double = 0

    Private _colorGridEn As System.Drawing.Pen = _colorGrid
    Private _colorEn As System.Drawing.Pen = _color1
    Private _color2En As System.Drawing.Pen = _colorFill1
    Private _color3En As System.Drawing.Pen = _color2
    Private _color4En As System.Drawing.Pen = _colorFill2
    Private _textColorEn As System.Drawing.Pen = _textColor

    Private _lastToolTip As String
    Private _xCoeff As Double = 2   ' One point every 2 pixels (width)

    Public Delegate Sub MouseEnterGraph(ByVal sender As Object, ByVal e As System.EventArgs)
    Public EvMouseEnterGraph As MouseEnterGraph

    Public Delegate Function DegReturnTooltipText(ByVal index As Integer, ByVal time As Long) As String
    Public ReturnTooltipText As DegReturnTooltipText

    ' ========================================
    ' Properties
    ' ========================================
#Region "Properties"
    Public Overloads Property Enabled() As Boolean
        Get
            Return MyBase.Enabled
        End Get
        Set(ByVal value As Boolean)
            MyBase.Enabled = value
            If value = False Then
                _colorGridEn = _colorGrid
                _colorEn = _color1
                _color2En = _colorFill1
                _color3En = _color2
                _color4En = _colorFill2
                _color1 = Pens.Gray
                _colorFill1 = Pens.Gray
                _color2 = Pens.Gray
                _colorFill2 = Pens.Gray
                _colorGrid = Pens.DarkGray
                _textColor = Pens.DarkGray
                Me.BackColor = Drawing.Color.Gray
            Else
                _color1 = _colorEn
                _colorFill1 = _color2En
                _color2 = _color3En
                _colorFill2 = _color4En
                _textColor = _textColorEn
                _colorGrid = _colorGridEn
                Me.BackColor = Drawing.Color.Black
            End If
        End Set
    End Property
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
            _colorGridEn = _colorGrid
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Green")> _
    Public Property TextColor() As Color
        Get
            Return _textColor.Color
        End Get
        Set(ByVal value As Color)
            _textColor = New Pen(value)
            _textColorEn = _textColor
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property Color1() As Color
        Get
            Return _color1.Color
        End Get
        Set(ByVal value As Color)
            _color1 = New Pen(value)
            _colorEn = _color1
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Yellow")> _
    Public Property ColorFill1() As Color
        Get
            Return _colorFill1.Color
        End Get
        Set(ByVal value As Color)
            _colorFill1 = New Pen(value)
            _color2En = _colorFill1
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "Red")> _
    Public Property Color2() As Color
        Get
            Return _color2.Color
        End Get
        Set(ByVal value As Color)
            _color2 = New Pen(value)
            _color3En = _color2
        End Set
    End Property
    <System.ComponentModel.Category("Configuration"), System.ComponentModel.Description("value"), _
    System.ComponentModel.Browsable(True), System.ComponentModel.DefaultValue(GetType(Color), "DarkRed")> _
    Public Property ColorFill2() As Color
        Get
            Return _colorFill2.Color
        End Get
        Set(ByVal value As Color)
            _colorFill2 = New Pen(value)
            _color4En = _colorFill2
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
    Public Property ShowSecondGraph() As Boolean
        Get
            Return _secondV
        End Get
        Set(ByVal value As Boolean)
            _secondV = value
        End Set
    End Property
    Public Property TopText() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property
    Public ReadOnly Property Values() As List(Of Double)
        Get
            Return _values
        End Get
    End Property
    Public ReadOnly Property Values2() As List(Of Double)
        Get
            Return _values2
        End Get
    End Property
    Public ReadOnly Property Times() As List(Of Long)
        Get
            Return _times
        End Get
    End Property
    Public Property DrawTheGrid() As Boolean
        Get
            Return _drawGrid
        End Get
        Set(ByVal value As Boolean)
            _drawGrid = value
        End Set
    End Property
    Public Property Fill1() As Boolean
        Get
            Return _fill1
        End Get
        Set(ByVal value As Boolean)
            _fill1 = value
        End Set
    End Property
    Public Property Fill2() As Boolean
        Get
            Return _fill2
        End Get
        Set(ByVal value As Boolean)
            _fill2 = value
        End Set
    End Property
#End Region


    ' ========================================
    ' Overriden subs
    ' ========================================
    Public Sub New()

        Me.toolTip.AutomaticDelay = 0
        Me.toolTip.ShowAlways = True

        Me.ClearValue()

        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        Me.Refresh()

    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        If Me.EnableGraph Then
            ' Smooth mode
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            ' Draw lines
            Me.DrawValues(e.Graphics)
            ' If necessary, draw second lines
            If Me.ShowSecondGraph Then
                Me.DrawValues2(e.Graphics)
            End If
            ' If necessary, show tooltip
            If _showToolTip Then
                Me.ShowToolTip()
            End If
        End If
        ' Draw the grid
        If Me.DrawTheGrid Then
            Me.DrawGrid(e.Graphics)
        End If
        If Me.EnableGraph Then
            ' Draw legend
            DrawLegend(e.Graphics)
        End If
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        If e.Location <> _mouseLoc Then
            _mouseLoc = e.Location
            Me.ShowToolTip(True)
        Else
            Me.ShowToolTip()
        End If
        'Debug.WriteLine("Mouse move")
    End Sub
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        numberOfValuesDisplayable = Me.Width \ 2
        numberOfValuesHidden = Math.Max(CInt(nCount - numberOfValuesDisplayable), 0)
        Me.Refresh()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        _showToolTip = False
        Me.toolTip.Hide(Me)
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        If EvMouseEnterGraph IsNot Nothing Then
            Try
                Call EvMouseEnterGraph(Me, e)
            Catch ex As Exception
                '
            End Try
        End If
        _showToolTip = True
        Me.toolTip.Hide(Me)
    End Sub


    ' ========================================
    ' Private methods
    ' ========================================

    ' Show tooltip
    Private Sub ShowToolTip()
        Me.ShowToolTip(False)
    End Sub
    Private Sub ShowToolTip(ByVal force As Boolean)

        ' Get the index in the list of the item associated with the mouse location
        Dim index As Integer = _values.Count - CInt((Me.Width - _mouseLoc.X) / _xCoeff) ' - numberOfValuesHidden

        If index >= 0 AndAlso index < _values.Count Then
            ' Now get the tooltip (from delegate)
            Dim _curToolTip As String
            Try
                If Me.ReturnTooltipText IsNot Nothing Then
                    _curToolTip = Me.ReturnTooltipText(index, _times(index))
                Else
                    _curToolTip = index.ToString
                End If
            Catch ex As Exception
                ' FAILED !
                _curToolTip = index.ToString
            End Try

            ' Won't 'Show' tooltip if it has not changed...
            If _curToolTip <> _lastToolTip OrElse force Then
                Me.toolTip.Show(_curToolTip, _
                                Me, _
                                New Point(_mouseLoc.X + 11, _mouseLoc.Y + 11), _
                                Integer.MaxValue)   '  Show always
            End If

            _lastToolTip = _curToolTip
        Else
            toolTip.Hide(Me)
        End If

    End Sub

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
        ' Background rectangle
        If Me.Enabled Then
            Dim sz As SizeF = g.MeasureString(_text, Me.Font)
            Dim textW As Single = sz.Width + 4
            Dim textH As Single = sz.Height + 4
            g.FillRectangle(New SolidBrush(Drawing.Color.Black), New Rectangle(0, 0, CInt(textW), CInt(textH)))
            ' Draw the text
            TextRenderer.DrawText(g, _text, Me.Font, New Point(2, 2), _textColor.Color)
        End If
    End Sub

    ' Draw values (second curve)
    Private Function DrawValues2(ByVal g As Graphics) As Point()

        If _values2.Count < 2 Then
            Return Nothing
        End If

        ' List of PointF to return
        Dim pts() As Point
        ReDim pts(Math.Min(numberOfValuesDisplayable - 1, _values2.Count - 1))

        ' Now calculate the Y coeff (height in pxl = VALUE(%) * _yCoeff)
        Dim _yCoeff As Double = 0
        If Fixedheight = False Then
            If _yMaxValue > 0 Then
                _yCoeff = (Me.Height - 4) / _yMaxValue
            End If
        Else
            _yCoeff = (Me.Height - 4) / 100
        End If

        Dim newPxlX As Integer = 0
        Dim newPxlY As Integer = 0

        ' Establish the list of points
        ' From right to left
        Dim index As Integer = 0
        For x As Integer = numberOfValuesHidden To _values2.Count - 1

            ' Calculate pxl (X)
            newPxlX = CInt(Me.Width - (_values2.Count - x) * _xCoeff)

            ' Calculate pxl (Y)
            newPxlY = CInt(Me.Height - _values2(x) * _yCoeff) - 2

            ' Save it in list of points
            pts(index) = New Point(newPxlX, newPxlY)
            index += 1

        Next

        ' Now add some points to the list of points to close the polygon
        Dim count As Integer = pts.Length - 1
        ReDim Preserve pts(count + 2)
        Dim lastPt As Point = pts(count)
        Dim firstPt As Point = pts(0)
        pts(count + 1) = New Point(lastPt.X, Me.Height)
        pts(count + 2) = New Point(firstPt.X, Me.Height)
        ' Draw the polygon
        If Me.Fill2 Then
            g.FillPolygon(_colorFill2.Brush, pts, Drawing2D.FillMode.Alternate)
        End If

        ' Now draw the "curve"
        g.DrawLines(_color2, pts)

        ' Return list of points
        Return pts

    End Function

    ' Draw values
    Private Function DrawValues(ByVal g As Graphics) As Point()

        If _values.Count < 2 Then
            Return Nothing
        End If

        ' List of PointF to return
        Dim pts() As Point
        ReDim pts(Math.Min(numberOfValuesDisplayable - 1, _values.Count - 1))

        ' Now calculate the Y coeff (height in pxl = VALUE(%) * _yCoeff)
        Dim _yCoeff As Double = 0
        If Fixedheight = False Then
            If _yMaxValue > 0 Then
                _yCoeff = (Me.Height - 4) / _yMaxValue
            End If
        Else
            _yCoeff = (Me.Height - 4) / 100
        End If

        Dim newPxlX As Integer = 0
        Dim newPxlY As Integer = 0

        ' Establish the list of points
        ' From right to left
        Dim index As Integer = 0
        For x As Integer = numberOfValuesHidden To _values.Count - 1

            Dim xReal As Integer = x - numberOfValuesHidden

            ' Calculate pxl (X)
            newPxlX = CInt(Me.Width - (_values.Count - x) * _xCoeff)

            ' Calculate pxl (Y)
            newPxlY = CInt(Me.Height - _values(x) * _yCoeff) - 2

            ' Save it in list of points
            pts(index) = New Point(newPxlX, newPxlY)
            index += 1

        Next

        ' Now add some points to the list of points to close the polygon
        Dim count As Integer = pts.Length - 1
        ReDim Preserve pts(count + 2)
        Dim lastPt As Point = pts(count)
        Dim firstPt As Point = pts(0)
        pts(count + 1) = New Point(lastPt.X, Me.Height)
        pts(count + 2) = New Point(firstPt.X, Me.Height)
        ' Draw the polygon
        If Me.Fill1 Then
            g.FillPolygon(_colorFill1.Brush, pts, Drawing2D.FillMode.Alternate)
        End If

        ' Now draw the "curve"
        g.DrawLines(_color1, pts)

        ' Return list of points
        Return pts

    End Function


    ' ========================================
    ' Public functions
    ' ========================================

    Public Sub AddValue(ByVal value As Integer, Optional ByVal time As Long = -1)
        AddValue(CDbl(value), time)
    End Sub
    Public Sub AddValue(ByVal value As Double, Optional ByVal time As Long = -1)

        Dim theTime As Long
        If time = -1 Then
            theTime = Date.Now.Ticks
        Else
            theTime = time
        End If

        nCount += 1

        ' Double the capacity to prevent excessive memory operations
        If nCount = _values.Capacity Then
            _values.Capacity *= 2
        End If

        _values.Add(value)
        _times.Add(theTime)

        ' Calculate new xMin and xMax
        numberOfValuesDisplayable = Me.Width \ 2
        numberOfValuesHidden = Math.Max(CInt(nCount - numberOfValuesDisplayable), 0)

        ' Calculate new maximum value
        If value > _yMaxValue Then
            _yMaxValue = value
        End If

    End Sub

    Public Sub Add2Values(ByVal value1 As Integer, ByVal value2 As Integer, Optional ByVal time As Long = -1)
        Add2Values(CDbl(value1), CDbl(value2))
    End Sub
    Public Sub Add2Values(ByVal value1 As Double, ByVal value2 As Double, Optional ByVal time As Long = -1)
        nCount += 1

        Dim theTime As Long
        If time = -1 Then
            theTime = Date.Now.Ticks
        Else
            theTime = time
        End If

        ' Double the capacity to prevent excessive memory operations
        If nCount = _values.Capacity Then
            _values.Capacity *= 2
            _values2.Capacity = _values.Capacity
            _times.Capacity = _values.Capacity
        End If

        _values.Add(value1)
        _values2.Add(value2)
        _times.Add(theTime)

        ' Calculate new xMin and xMax
        numberOfValuesDisplayable = Me.Width \ 2
        numberOfValuesHidden = Math.Max(CInt(nCount - numberOfValuesDisplayable), 0)

        ' Calculate new maximum value
        If value1 > _yMaxValue Then
            _yMaxValue = value1
        End If
        If value2 > _yMaxValue Then
            _yMaxValue = value2
        End If

    End Sub

    Public Sub ClearValue()
        _values.Capacity = 200
        _values2.Capacity = 200
        _times.Capacity = 200
        _values.Clear()
        _values2.Clear()
        _times.Clear()
        _yMaxValue = 0
        nCount = 0
    End Sub

End Class
