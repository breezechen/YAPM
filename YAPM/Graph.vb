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

    ' ========================================
    ' Private attributes
    ' ========================================
    Private _gridStep As Integer = 20
    Private _values() As ValueItem
    Private _colorGrid As System.Drawing.Pen = Pens.DarkGreen
    Private _colorCPUPercent As System.Drawing.Pen = Pens.Yellow
    Private _colorCPUTime As System.Drawing.Pen = Pens.Yellow
    Private _colorMemory1 As System.Drawing.Pen = Pens.Yellow
    Private _colorMemory2 As System.Drawing.Pen = Pens.Red
    Private _colorMemory3 As System.Drawing.Pen = Pens.Blue
    Private _colorPriority As System.Drawing.Pen = Pens.Yellow
    Private _colorThreads As System.Drawing.Pen = Pens.Yellow
    Private _xMin As Integer
    Private _xMax As Integer


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
#End Region


    ' ========================================
    ' Overriden subs
    ' ========================================
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        DrawGrid(e.Graphics)
        DrawLegend(e.Graphics)
        DrawValues(e.Graphics)
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
        For x = _xMin To _xMax
            If _values(x).y > yMax Then yMax = _values(x).y
        Next
        Dim yCoef As Double = Me.Height / yMax
        Dim xCoef As Double = Me.Width / (_xMax - _xMin)

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
            yy1 = CInt(Me.Height - y1 * yCoef)
            yy2 = CInt(Me.Height - y2 * yCoef)

            g.DrawLine(Pens.Yellow, xx1, yy1, xx2, yy2)

        Next

    End Sub


    ' ========================================
    ' Public functions
    ' ========================================

    Public Sub SetValues(ByVal values() As ValueItem)
        _values = values
    End Sub


End Class