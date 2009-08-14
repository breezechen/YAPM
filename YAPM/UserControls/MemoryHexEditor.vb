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

Public Class MemoryHexEditor
    Inherits System.Windows.Forms.Control

#Region "API"
    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function CreateCaret(ByVal hWnd As IntPtr, ByVal hBitmap As IntPtr, ByVal nWidth As Integer, ByVal nHeight As Integer) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function ShowCaret(ByVal hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function DestroyCaret() As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function SetCaretPos(ByVal X As Integer, ByVal Y As Integer) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function InvertRect(ByVal hdc As IntPtr, ByRef lpRect As RECT) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
        Public Sub New(ByVal _left As Integer, ByVal _top As Integer, _
                       ByVal _width As Integer, ByVal _height As Integer)
            Left = _left
            Top = _top
            Bottom = _height + Top
            Right = _width + _left
        End Sub
    End Structure

#End Region



    ' Controls
    Private WithEvents _vs As New VScrollBar

    ' Process attributes
    Private _pid As Integer = 0             ' Process ID
    Private _hProc As IntPtr                ' Handle to process
    Private _rw As cProcessMemRW

    ' Memory informations
    Private _memRegion As MemoryRegion
    Private _physBeg As Integer
    Private _physEnd As Integer
    Private _relativeOffset As Boolean

    ' Informations about current view
    Private _curViewDeb As IntPtr
    Private _linesNumber As Integer

    ' Colors
    Private _OffsetColor As Color = Color.Black
    Private _HValuesColor As Color = Color.Blue
    Private _SValuesColor As Color = Color.Red
    Private _SelectionColor As Color = Color.AliceBlue
    Private _LineColor As Color = Color.DarkGray

    ' Pens
    Private _HValuesPen As Pen = New Pen(_HValuesColor)
    Private _SValuesPen As Pen = New Pen(_SValuesColor)
    Private _SelectionPen As Pen = New Pen(_SelectionColor)
    Private _OffsetPen As Pen = New Pen(_OffsetColor)
    Private _LinePen As Pen = New Pen(_LineColor)

    ' Brushes
    Private _OffsetBrush As Brush = Brushes.Blue
    Private _HexaBrush As Brush = Brushes.Black
    Private _failedBrush As Brush = Brushes.Red

    ' Positions (to draw)
    Private Const CONTROL_WIDTH As Integer = 625
    Private Const LEFT_HEXA As Integer = 100
    Private Const LEFT_OFFSET As Integer = 2
    Private Const LEFT_FIRST_LINE As Integer = 90
    Private Const LEFT_SECOND_LINE As Integer = 460
    Private Const LEFT_STRING As Integer = 470
    Private Const LINE_HEIGHT As Integer = 12
    Private Const TOP_OFFSET As Integer = 3
    Private CHAR_HEIGHT As Integer = 13
    Private CHAR_WIDTH As Integer = 7

    ' Graphic items
    Private _font As New Font("Courier New", 12, FontStyle.Regular, GraphicsUnit.World, 0)

    ' Caret
    Private _xCaret As Integer = LEFT_HEXA + 1      ' pxl
    Private _yCaret As Integer = TOP_OFFSET         ' pxl
    Private _xCar As Integer = 1                    ' 0-15
    Private _yCar As Integer = 1                    ' 0-_linesNumber

    ' Selection
    Private _selection As Boolean = False
    Private _xOld As Integer
    Private _yOld As Integer
    Private _ShiftDown As Boolean = False



    ' ========================================
    ' Structures
    ' ========================================
    Public Structure MemoryRegion
        Dim BeginningAddress As IntPtr
        Dim Size As Integer
        Public Sub New(ByVal _begin As IntPtr, ByVal _size As Integer)
            BeginningAddress = _begin
            Size = _size
        End Sub
    End Structure

    ' ========================================
    ' Properties
    ' ========================================
    Public ReadOnly Property ProcessID() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property MemRegion() As MemoryRegion
        Get
            Return _memRegion
        End Get
    End Property
    Public Property UseRelativeOffset() As Boolean
        Get
            Return _relativeOffset
        End Get
        Set(ByVal value As Boolean)
            _relativeOffset = value
        End Set
    End Property


    ' ========================================
    ' Public functions
    ' ========================================

    ' Navigate to offset
    Public Sub NavigateToOffset(ByVal offset As Long)
        If offset >= _vs.Minimum AndAlso offset <= _vs.Maximum Then
            _vs.Value = CInt(offset)
        End If
    End Sub

    ' Constructor & destructor
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        ''
    End Sub
    Public Sub NewProc(ByVal MemRegion As MemoryRegion, ByVal ProcessId As Integer)

        ' Required
        'InitializeComponent()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        Me.Width = CONTROL_WIDTH
        'CHAR_HEIGHT = CInt(Me.CreateGraphics.MeasureString("0", _font, 0).Height)

        _pid = ProcessId
        _rw = New cProcessMemRW(_pid)
        _memRegion = MemRegion
        _curViewDeb = _memRegion.BeginningAddress

        ' Initialize VS
        With _vs
            .Maximum = CInt(_memRegion.Size / 16)
            .Minimum = 0
            .Value = 0
            .SmallChange = 1
            .LargeChange = CInt(.Maximum / 30 + 1)
            .Top = 0
            .Width = 20
            .Height = Me.Height
            .Left = Me.Width - .Width
            Me.Controls.Add(_vs)
        End With

        ' Initialize caret
        CreateCaret(Me.Handle, IntPtr.Zero, 1, CHAR_HEIGHT)
        Call Me.UpdateCaretPosition()
        _xOld = 1
        _yOld = 1
        ShowCaret(Me.Handle)

    End Sub

    Protected Overrides Sub OnPaint(ByVal pe As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(pe)

        Call DrawOffset(pe.Graphics)
        Call DrawValues(pe.Graphics)
        Call DrawSelection(pe.Graphics)
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================

    ' Draw selection
    Private Sub DrawSelection(ByRef g As Graphics)

        ' Draw 3 invert rectangles
        ' Hexa + String

        ' first -> first incomplete line
        ' second -> full lines
        ' third -> last incomplet line

        Dim hDc As IntPtr = g.GetHdc

        Select Case Math.Abs(_yOld - _yCar)
            Case 0
                ' Only 1 rect (hexa)
                Dim y As Integer = GetYpxlHexa(_yOld)
                Dim x1 As Integer = GetXpxlHexa(_xOld)
                Dim x2 As Integer = GetXpxlHexa(_xCar)
                Dim rect1 As New RECT(Math.Min(x1, x2), y, Math.Abs(x2 - x1) + CHAR_WIDTH, CHAR_HEIGHT)
                InvertRect(hDc, rect1)

                ' String
                y = GetYpxlString(_yOld)
                x1 = GetXpxlString(_xOld)
                x2 = GetXpxlString(_xCar)
                rect1 = New RECT(Math.Min(x1, x2), y, Math.Abs(x2 - x1) + CHAR_WIDTH, CHAR_HEIGHT)
                InvertRect(hDc, rect1)

            Case 1
                ' 2 rects (hexa)
                Dim y1 As Integer = GetYpxlHexa(_yOld)
                Dim y2 As Integer = GetYpxlHexa(_yCar)
                Dim x1 As Integer = GetXpxlHexa(_xOld)
                Dim x2 As Integer = GetXpxlHexa(_xCar)

                Dim rect1 As RECT
                Dim rect2 As RECT

                If y1 > y2 Then
                    Dim t As Integer = GetXpxlHexa(1)
                    rect1 = New RECT(t, y1, x1 - t, CHAR_HEIGHT)
                    rect2 = New RECT(x2, y2 - 1, GetXpxlHexa(32) + CHAR_WIDTH - x2, CHAR_HEIGHT)
                Else
                    Dim t As Integer = GetXpxlHexa(1)
                    rect1 = New RECT(x1, y1, GetXpxlHexa(32) + CHAR_WIDTH - x1, CHAR_HEIGHT)
                    rect2 = New RECT(t, y2 + 1, x2 - t, CHAR_HEIGHT)
                End If

                InvertRect(hDc, rect1)
                InvertRect(hDc, rect2)


                ' String
                y1 = GetYpxlString(_yOld)
                y2 = GetYpxlString(_yCar)
                x1 = GetXpxlString(_xOld)
                x2 = GetXpxlString(_xCar)

                If y1 > y2 Then
                    Dim t As Integer = GetXpxlString(1)
                    rect1 = New RECT(t, y1, x1 - t, CHAR_HEIGHT)
                    rect2 = New RECT(x2, y2 - 1, GetXpxlString(32) + CHAR_WIDTH - x2, CHAR_HEIGHT)
                Else
                    Dim t As Integer = GetXpxlString(1)
                    rect1 = New RECT(x1, y1, GetXpxlString(32) + CHAR_WIDTH - x1, CHAR_HEIGHT)
                    rect2 = New RECT(t, y2 + 1, x2 - t, CHAR_HEIGHT)
                End If

                InvertRect(hDc, rect1)
                InvertRect(hDc, rect2)

            Case Else
                ' 3 rects (hexa)
                Dim y1 As Integer = GetYpxlHexa(_yOld)
                Dim y2 As Integer = GetYpxlHexa(_yCar)
                Dim x1 As Integer = GetXpxlHexa(_xOld)
                Dim x2 As Integer = GetXpxlHexa(_xCar)

                Dim rect1 As RECT
                Dim rect2 As RECT
                Dim rect3 As RECT

                If y1 > y2 Then
                    Dim t As Integer = GetXpxlHexa(1)
                    Dim t2 As Integer = GetXpxlHexa(32) + CHAR_WIDTH
                    rect1 = New RECT(t, y1, x1 - t, CHAR_HEIGHT)
                    rect2 = New RECT(x2, y2, t2 - x2, CHAR_HEIGHT)
                    rect3 = New RECT(t, y2 + CHAR_HEIGHT, t2 - t, y1 - y2 - CHAR_HEIGHT)
                Else
                    Dim t As Integer = GetXpxlHexa(1)
                    Dim t2 As Integer = GetXpxlHexa(32) + CHAR_WIDTH
                    rect1 = New RECT(x1, y1, t2 - x1, CHAR_HEIGHT)
                    rect2 = New RECT(t, y2, x2 - t, CHAR_HEIGHT)
                    rect3 = New RECT(t, y1 + CHAR_HEIGHT, t2 - t, y2 - y1 - CHAR_HEIGHT)
                End If

                InvertRect(hDc, rect1)
                InvertRect(hDc, rect2)
                InvertRect(hDc, rect3)


                ' String
                y1 = GetYpxlString(_yOld)
                y2 = GetYpxlString(_yCar)
                x1 = GetXpxlString(_xOld)
                x2 = GetXpxlString(_xCar)

                If y1 > y2 Then
                    Dim t As Integer = GetXpxlString(1)
                    Dim t2 As Integer = GetXpxlString(32) + CHAR_WIDTH
                    rect1 = New RECT(t, y1, x1 - t, CHAR_HEIGHT)
                    rect2 = New RECT(x2, y2, t2 - x2, CHAR_HEIGHT)
                    rect3 = New RECT(t, y2 + CHAR_HEIGHT, t2 - t, y1 - y2 - CHAR_HEIGHT)
                Else
                    Dim t As Integer = GetXpxlString(1)
                    Dim t2 As Integer = GetXpxlString(32) + CHAR_WIDTH
                    rect1 = New RECT(x1, y1, t2 - x1, CHAR_HEIGHT)
                    rect2 = New RECT(t, y2, x2 - t, CHAR_HEIGHT)
                    rect3 = New RECT(t, y1 + CHAR_HEIGHT, t2 - t, y2 - y1 - CHAR_HEIGHT)
                End If

                InvertRect(hDc, rect1)
                InvertRect(hDc, rect2)
                InvertRect(hDc, rect3)

        End Select

        g.ReleaseHdc()

    End Sub

    ' Draw offsets
    Private Sub DrawOffset(ByRef g As Graphics)

        ' Draw offsets
        For x As Integer = 0 To _linesNumber - 1
            g.DrawString(FormatedOffset(_memRegion.BeginningAddress.Increment((_vs.Value + x) * 16)), _font, _OffsetBrush, LEFT_OFFSET, TOP_OFFSET + x * LINE_HEIGHT)
        Next

        ' Draw 2 lines
        g.DrawLine(_LinePen, LEFT_FIRST_LINE, 0, LEFT_FIRST_LINE, Me.Height)
        g.DrawLine(_LinePen, LEFT_SECOND_LINE, 0, LEFT_SECOND_LINE, Me.Height)

    End Sub

    ' Draw values read in memory
    Private Sub DrawValues(ByRef g As Graphics)

        ' Read all bytes to display from memory
        Dim size As Integer = 16 * _linesNumber
        Dim isOk As Boolean = True
        Dim buff() As Byte = _rw.ReadBytesAB(_curViewDeb, size, isOk)

        Dim theBruh As Brush = _HexaBrush
        If isOk = False Then
            theBruh = _failedBrush
        End If

        ' Display hexa
        For x As Integer = 0 To _linesNumber - 1
            Dim s As String = ""

            For y As Integer = 1 To 16
                Dim s2 As String = buff(x * 16 + y - 1).ToString("x")
                If s2.Length = 1 Then s2 = "0" & s2
                s &= s2 & " "
            Next

            g.DrawString(s, _font, theBruh, LEFT_HEXA, TOP_OFFSET + x * LINE_HEIGHT)
        Next

        ' Display strings
        For x As Integer = 0 To _linesNumber - 1
            Dim s As String = ""

            For y As Integer = 1 To 16
                Dim b As Byte = buff(x * 16 + y - 1)
                If (b > &H1F And Not (b > &H7E And b < &HA0)) Then
                    s &= Convert.ToChar(b).ToString
                Else
                    s &= "."
                End If
            Next

            g.DrawString(s, _font, theBruh, LEFT_STRING, TOP_OFFSET + x * LINE_HEIGHT)
        Next


    End Sub

    ' Get X (pixel) from X (logical)
    Private Function GetXpxlHexa(ByVal x As Integer) As Integer
        Return 1 + LEFT_HEXA + CHAR_WIDTH * (x - 1) + CInt((CHAR_WIDTH + 1.35) * Math.Floor((x - 1) / 2))
    End Function
    ' Get Y (pixel) from Y (logical)
    Private Function GetYpxlHexa(ByVal y As Integer) As Integer
        Return TOP_OFFSET + CInt(CHAR_HEIGHT - 0.5) * (y - 1)
    End Function
    ' Get X (pixel) from X (logical)
    Private Function GetXpxlString(ByVal x As Integer) As Integer
        Return CInt(1 + LEFT_STRING + CHAR_WIDTH * (x - 1) / 2) ' + CInt((CHAR_WIDTH + 1.35) * Math.Floor((x - 1) / 2))
    End Function
    ' Get Y (pixel) from Y (logical)
    Private Function GetYpxlString(ByVal y As Integer) As Integer
        Return GetYpxlHexa(y)
    End Function

    ' Change caret position
    Private Sub UpdateCaretPosition()

        ' Calculate size (pixel) from _xCar and _yCar
        _xCaret = GetXpxlHexa(_xCar) ' 1 + LEFT_HEXA + CHAR_WIDTH * (_xCar - 1) + CInt((CHAR_WIDTH + 1.35) * Math.Floor((_xCar - 1) / 2))
        _yCaret = GetYpxlHexa(_yCar) 'TOP_OFFSET + CInt(CHAR_HEIGHT - 0.5) * (_yCar - 1)

        Call SetCaretPos(_xCaret, _yCaret)
    End Sub

    ' Calculate car positions
    Private Sub ReCalcX(ByVal eX As Integer)
        If eX > LEFT_HEXA And eX < LEFT_SECOND_LINE Then

            ' spaces take 1 CHAR_WIDTH+1 pixels
            Dim x As Integer = eX - LEFT_HEXA
            Dim spaces As Integer = CInt(Math.Floor(x / (3 * CHAR_WIDTH + 1)))
            _xCar = CInt((x - spaces) / CHAR_WIDTH) - spaces
            If _xCar < 1 Then _xCar = 1
            If _xCar > 32 Then _xCar = 32
        End If
    End Sub
    Private Sub ReCalcY(ByVal eY As Integer)
        _yCar = 1 + CInt((eY - TOP_OFFSET) / (CHAR_HEIGHT - 0.5))
    End Sub

    ' Get formated offset
    Private Function FormatedOffset(ByVal address As IntPtr) As String
        Dim s As String = address.ToString("x")
        Return "0x" & New String(Convert.ToChar(48), 8 - s.Length) & s
    End Function

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)

        If _ShiftDown = False Then
            _xCar = _xOld
            _yCar = _yOld
        End If

    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)

        Static shitD As Boolean = False

        If e.Button <> Windows.Forms.MouseButtons.Left Then Exit Sub

        If _ShiftDown Then
            If shitD = False Then
                shitD = True
                _xOld = _xCar
                _yOld = _yCar
            End If
        End If

        ' Calculate _xCar and _yCar
        ReCalcX(e.X)
        ReCalcY(e.Y)

        Call Me.Refresh()
    End Sub

    Protected Overrides Sub OnMouseWheel(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseWheel(e)

        If e.Delta < 0 Then
            If _vs.Value < _vs.Maximum Then
                _vs.Value += 1
            End If
        Else
            If _vs.Value > _vs.Minimum Then
                _vs.Value -= 1
            End If
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)

        Static shitD As Boolean = False

        If _ShiftDown Then
            If shitD = False Then
                shitD = True
                _xOld = _xCar
                _yOld = _yCar
                Trace.WriteLine("changement")
            End If
        End If

        ' Calculate _xCar and _yCar
        ReCalcX(e.X)
        ReCalcY(e.Y)

        If Not (_ShiftDown) Then
            shitD = False
            _xOld = _xCar
            _yOld = _yCar
            Trace.WriteLine("aucun")
            Call UpdateCaretPosition()
        End If

        Trace.WriteLine("old=" & _xOld & " new=" & _xCar)

        Call Me.Refresh()
    End Sub

    Protected Overrides Sub OnKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyUp(e)

        Select Case e.KeyCode

            Case Keys.ShiftKey
                _ShiftDown = False

        End Select
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)

        'Static shitD As Boolean = False

        'If e.Shift Then
        '    If shitD = False Then
        '        shitD = True
        '        _xOld = _xCar
        '        _yOld = _yCar
        '    End If
        'End If


        Select Case e.KeyCode

            Case Keys.Q
                If _xCar > 1 Then
                    _xCar -= 1
                Else
                    _yCar -= 1
                    _xCar = 32
                    If _yCar < 1 Then
                        _yCar = 1
                        _xCar = 1
                    End If
                End If

            Case Keys.D
                If _xCar < 32 Then
                    _xCar += 1
                Else
                    _yCar += 1
                    _xCar = 1
                    If _yCar > _linesNumber - 1 Then
                        _yCar = _linesNumber - 1
                        ' Scroll down
                        If _vs.Value < _vs.Maximum Then _vs.Value += 1
                    End If
                End If

            Case Keys.Z
                _yCar -= 1
                If _yCar < 1 Then
                    _yCar = 1
                    If _vs.Value > 1 Then _vs.Value -= 1
                End If

            Case Keys.S
                _yCar += 1
                If _yCar > _linesNumber - 1 Then
                    _yCar = _linesNumber - 1
                    ' Scroll down
                    If _vs.Value < _vs.Maximum Then _vs.Value += 1
                End If

            Case Keys.ShiftKey
                _ShiftDown = True
                Exit Sub

        End Select


        If e.Shift Then
            Call Me.Refresh()
        Else
            _xOld = _xCar
            _yOld = _yCar
        End If

        Call UpdateCaretPosition()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        _vs.Height = Me.Height
        _linesNumber = CInt((Me.Height - TOP_OFFSET) / CHAR_HEIGHT)  ' Calculate number of lines to display
        Call Me.Refresh()
    End Sub

    Private Sub _vs_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _vs.ValueChanged
        _curViewDeb = _memRegion.BeginningAddress.Increment(_vs.Value * 16)  ' Calculate first address
        Me.Refresh()
    End Sub
End Class
