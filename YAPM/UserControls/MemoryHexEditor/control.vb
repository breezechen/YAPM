' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


Option Strict On

Imports System.Runtime.InteropServices

Public Class control
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
#End Region



    ' Controls
    Private WithEvents _vs As New VScrollBar

    ' Process attributes
    Private _pid As Integer = 0             ' Process ID
    Private _hProc As Integer = 0           ' Handle to process
    Private _rw As cProcessMemRW

    ' Memory informations
    Private _memRegion As MemoryRegion
    Private _physBeg As Integer
    Private _physEnd As Integer
    Private _relativeOffset As Boolean

    ' Informations about current view
    Private _curViewDeb As Integer
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
    Private _OffsetBrush As Brush = Brushes.Black
    Private _HexaBrush As Brush = Brushes.Black

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
    Private _font As New Font("Courier New", 12, FontStyle.Regular, GraphicsUnit.Pixel, 0)

    ' Caret
    Private _xCaret As Integer = LEFT_HEXA + 1      ' pxl
    Private _yCaret As Integer = TOP_OFFSET         ' pxl
    Private _xCar As Integer = 0                    ' 0-15
    Private _yCar As Integer = 0                    ' 0-_linesNumber




    ' ========================================
    ' Structures
    ' ========================================
    Public Structure MemoryRegion
        Dim BeginningAddress As Integer
        Dim Size As Integer
        Public Sub New(ByVal _begin As Integer, ByVal _size As Integer)
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
        ShowCaret(Me.Handle)

    End Sub

    Protected Overrides Sub OnPaint(ByVal pe As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(pe)

        Call DrawOffset(pe.Graphics)
        Call DrawValues(pe.Graphics)
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================

    ' Draw offsets
    Private Sub DrawOffset(ByRef g As Graphics)

        ' Draw offsets
        For x As Integer = 0 To _linesNumber - 1
            g.DrawString(FormatedOffset(_memRegion.BeginningAddress + (_vs.Value + x) * 16), _font, _OffsetBrush, LEFT_OFFSET, TOP_OFFSET + x * LINE_HEIGHT)
        Next

        ' Draw 2 lines
        g.DrawLine(_LinePen, LEFT_FIRST_LINE, 0, LEFT_FIRST_LINE, Me.Height)
        g.DrawLine(_LinePen, LEFT_SECOND_LINE, 0, LEFT_SECOND_LINE, Me.Height)

    End Sub

    ' Draw values read in memory
    Private Sub DrawValues(ByRef g As Graphics)

        ' Read all bytes to display from memory
        Dim size As Integer = 16 * _linesNumber
        Dim buff() As Byte = _rw.ReadBytesAB(_curViewDeb, size)

        ' Display hexa
        For x As Integer = 0 To _linesNumber - 1
            Dim s As String = ""

            For y As Integer = 1 To 16
                Dim s2 As String = buff(x * 16 + y - 1).ToString("x")
                If s2.Length = 1 Then s2 = "0" & s2
                s &= s2 & " "
            Next

            g.DrawString(s, _font, _HexaBrush, LEFT_HEXA, TOP_OFFSET + x * LINE_HEIGHT)
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

            g.DrawString(s, _font, _HexaBrush, LEFT_STRING, TOP_OFFSET + x * LINE_HEIGHT)
        Next


    End Sub

    ' Change caret position
    Private Sub UpdateCaretPosition()
        Call SetCaretPos(_xCaret, _yCaret)
    End Sub

    ' Get formated offset
    Private Function FormatedOffset(ByVal address As Integer) As String
        Dim s As String = address.ToString("x")
        Return "0x" & New String(Convert.ToChar(48), 8 - s.Length) & s
    End Function

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)

        ' Change _xCaret and _yCaret
        If e.X > LEFT_HEXA And e.X < LEFT_SECOND_LINE Then
            Dim z16 As Integer = CInt((e.X - LEFT_HEXA) / CHAR_WIDTH)
            If ((z16 - 2) Mod 3) = 0 Then z16 += 1 ' Can't stay between hexa values (space char)
            _xCaret = LEFT_HEXA + CHAR_WIDTH * z16
            Trace.WriteLine(z16)
        End If

        _yCaret = TOP_OFFSET + CHAR_HEIGHT * CInt((e.Y - TOP_OFFSET) / CHAR_HEIGHT)

        Call UpdateCaretPosition()
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        Select Case e.KeyCode

            Case Keys.Left
                If _xCar > 0 Then
                    _xCar -= 1
                Else
                    _yCar -= 1
                    If _yCar < 0 Then _yCar = 0
                End If

            Case Keys.Right
                If _xCar < 15 Then
                    _xCar += 1
                Else
                    _yCar += 1
                    If _yCar > _linesNumber - 1 Then
                        _yCar = _linesNumber - 1
                        ' Scroll down
                        If _vs.Value < _vs.Maximum Then _vs.Value += 1
                    End If
                End If

            Case Keys.Up
                _yCar -= 1
                If _yCar < 0 Then _yCar = 0

            Case Keys.Down
                _yCar += 1
                If _yCar > _linesNumber - 1 Then
                    _yCar = _linesNumber - 1
                    ' Scroll down
                    If _vs.Value < _vs.Maximum Then _vs.Value += 1
                End If

        End Select

        ' Calculate size (pixel) from _xCar and _yCar
        _xCaret = CHAR_WIDTH * _xCar + LEFT_HEXA
        _yCaret = CHAR_HEIGHT * _yCar + TOP_OFFSET

        Call UpdateCaretPosition()
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        _linesNumber = CInt((Me.Height - TOP_OFFSET) / CHAR_HEIGHT)  ' Calculate number of lines to display
        Call Me.Refresh()
    End Sub

    Private Sub _vs_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _vs.ValueChanged
        _curViewDeb = _memRegion.BeginningAddress + _vs.Value * 16  ' Calculate first address
        Me.Refresh()
    End Sub
End Class
