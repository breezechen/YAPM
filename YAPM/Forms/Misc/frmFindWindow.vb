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

'http://www.pscode.com/vb/scripts/ShowCode.asp?txtCodeId=6362&lngWId=1

Imports System.Runtime.InteropServices

Public Class frmFindWindow

#Region "API"

    Private Structure POINTAPI
        Dim X As Integer
        Dim Y As Integer
    End Structure

    Private Structure RECT
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure

    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hwnd As Integer, ByRef lpdwProcessId As Integer) As Integer
    Private Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer ' Get the cursor position
    Private Declare Function WindowFromPoint Lib "user32" (ByVal xPoint As Integer, ByVal yPoint As Integer) As Integer ' Get the handle of the window that is foremost on a particular X, Y position. Used here To get the window under the cursor
    Private Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Integer, ByRef lpRect As RECT) As Integer ' Get the window co-ordinates in a RECT structure
    Private Declare Function GetWindowDC Lib "user32" (ByVal hwnd As Integer) As Integer ' Retrieve a handle For the hDC of a window
    Private Declare Function ReleaseDC Lib "user32" (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer ' Release the memory occupied by an hDC
    Private Declare Function CreatePen Lib "gdi32" (ByVal nPenStyle As Integer, ByVal nWidth As Integer, ByVal crColor As Integer) As Integer ' Create a GDI graphics pen object
    Private Declare Function SelectObject Lib "gdi32" (ByVal hdc As Integer, ByVal hObject As Integer) As Integer ' Used to select brushes, pens, and clipping regions
    Private Declare Function GetStockObject Lib "gdi32" (ByVal nIndex As Integer) As Integer ' Get hold of a "stock" object. I use it to get a Null Brush
    Private Declare Function SetROP2 Lib "gdi32" (ByVal hdc As Integer, ByVal nDrawMode As Integer) As Integer ' Used To set the Raster OPeration of a window
    Private Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Integer) As Integer ' Delete a GDI Object
    Private Declare Function Rectangle Lib "gdi32" (ByVal hdc As Integer, ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer) As Integer ' GDI Graphics- draw a rectangle using current pen, brush, etc.
    Private Declare Function SetCapture Lib "user32" (ByVal hwnd As Integer) As Integer ' Set mouse events only For one window
    Private Declare Function ReleaseCapture Lib "user32" () As Integer ' Release the mouse capture
    Private Declare Function CreateRectRgn Lib "gdi32" (ByVal X1 As Integer, ByVal Y1 As Integer, ByVal X2 As Integer, ByVal Y2 As Integer) As Integer ' Create a rectangular region
    Private Declare Function SelectClipRgn Lib "gdi32" (ByVal hdc As Integer, ByVal hRgn As Integer) As Integer ' Select the clipping region of an hDC
    Private Declare Function GetClipRgn Lib "gdi32" (ByVal hdc As Integer, ByVal hRgn As Integer) As Integer ' Get the Clipping region of an hDC

    Private Const NULL_BRUSH As Integer = 5 ' Stock Object
    Private Selecting As Boolean ' Amd I currently selecting a window?
    Private BorderDrawn As Boolean ' Is there a border currently drawn that needs To be undrawn?
    Private Myhwnd As Integer ' The current hWnd that has a border drawn on it

#End Region

    Private Sub Draw()
        Dim Cursor As POINTAPI ' Cursor position
        Dim RetVal As Integer ' Dummy returnvalue
        Dim hdc As Integer ' hDC that we're going To be using
        Dim Pen As Integer ' Handle To a GDI Pen object
        Dim Brush As Integer ' Handle To a GDI Brush object
        Dim OldPen As Integer ' Handle To previous Pen object (to restore it)
        Dim OldBrush As Integer ' Handle To previous brush object (to restore it)
        Dim OldROP As Integer ' Value of the previous ROP
        Dim Region As Integer ' Handle To a GDI Region object that I create
        Dim OldRegion As Integer ' Handle To previous Region object For the hDC
        Dim FullWind As RECT ' the bounding rectangle of the window in screen coords
        Dim Draw As RECT ' The drawing rectangle

        ' Get the cursor
        GetCursorPos(Cursor)
        ' Get the window
        RetVal = WindowFromPoint(Cursor.X, Cursor.Y)
        ' If the new hWnd is the same as the old
        '     one, skip drawing it, so to avoid flicker
        If RetVal = Myhwnd Then Exit Sub
        ' New hWnd. If there is currently a border drawn, undraw it.
        If BorderDrawn = True Then UnDraw()
        ' Set the BorderDrawn property to true, as we're just about to draw it.
        BorderDrawn = True
        ' And set the hWnd to the new value.
        ' Note, I didn't do it before, because the UnDraw routine uses the Myhwnd variable
        Myhwnd = RetVal
        ' You could extract other information from the window, such as window title,
        ' class name, parent, etc., and print it here, too.
        ' Get the full Rect of the window in screen co-ords
        GetWindowRect(Myhwnd, FullWind)
        ' Create a region with width and height of the window
        Region = CreateRectRgn(0, 0, FullWind.Right - FullWind.Left, FullWind.Bottom - FullWind.Top)
        ' Create an hDC for the hWnd
        ' Note: GetDC retrieves the CLIENT AREA hDC. We want the WHOLE WINDOW, including Non-Client
        ' stuff like title bar, menu, border, etc.
        hdc = GetWindowDC(Myhwnd)
        ' Save the old region
        RetVal = GetClipRgn(hdc, OldRegion)
        ' Retval = 0: no region 1: Region copied -1: error
        ' Select the new region
        RetVal = SelectObject(hdc, Region)
        ' Create a pen
        Pen = CreatePen(0, 6, 0) ' Draw Solid lines, width 6, and color black
        ' Select the pen
        ' A pen draws the lines
        OldPen = SelectObject(hdc, Pen)
        ' Create a brush
        ' A brush is the filling for a shape
        ' I need to set it to a null brush so th
        '     at it doesn't edit anything
        Brush = GetStockObject(NULL_BRUSH)
        ' Select the brush
        OldBrush = SelectObject(hdc, Brush)
        ' Select the ROP
        OldROP = SetROP2(hdc, 6) ' vbInvert means, whatever is draw,
        ' invert those pixels. This means that I can undraw it by doing the same.
        '
        ' The Drawing Bits
        '
        ' Put a box around the outside of the window, using the current hDC.
        ' These coords are in device co-ordinates, i.e., of the hDC.


        With Draw
            .Left = 0
            .Top = 0
            .Bottom = FullWind.Bottom - FullWind.Top
            .Right = FullWind.Right - FullWind.Left
            Rectangle(hdc, .Left, .Top, .Right, .Bottom) ' Really easy to understand - draw a rectangle, hDC, and coordinates
        End With

        '
        ' The Washing Up bits
        '
        ' This is a very important part, as it releases memory that has been taken up.
        ' If we don't do this, windows crashes due to a memory leak.
        ' You probably get a blue screen (altohugh I'm not sure)
        '
        ' Get back the old region


        SelectObject(hdc, OldRegion)
        ' Return the previous ROP
        SetROP2(hdc, OldROP)
        ' Return to the previous brush


        SelectObject(hdc, OldBrush)
        ' Return the previous pen


        SelectObject(hdc, OldPen)
        ' Delete the Brush I created
        DeleteObject(Brush)
        ' Delete the Pen I created
        DeleteObject(Pen)
        ' Delete the region I created
        DeleteObject(Region)
        ' Release the hDC back to window's resource pool
        ReleaseDC(Myhwnd, hdc)
    End Sub



    Private Sub UnDraw()

        '
        ' Note, this sub is almost identical to the other one, except it doesn't go looking
        ' for the hWnd, it accesses the old one. Also, it doesn't clear the form.
        ' Otherwise, it just draws on top of the old one with an invert pen.
        ' 2 inverts = original
        '
        ' If there hasn't been a border drawn, then get out of here.
        If BorderDrawn = False Then Exit Sub
        ' Now set it
        BorderDrawn = False
        ' If there isn't a current hWnd, then exit.
        ' That's why in the mouseup event we get out, because otherwise a border would be draw
        ' around the old window
        If Myhwnd = 0 Then Exit Sub
        Dim RetVal As Integer ' Dummy returnvalue
        Dim hdc As Integer ' hDC that we're going To be using
        Dim Pen As Integer ' Handle To a GDI Pen object
        Dim Brush As Integer ' Handle To a GDI Brush object
        Dim OldPen As Integer ' Handle To previous Pen object (to restore it)
        Dim OldBrush As Integer ' Handle To previous brush object (to restore it)
        Dim OldROP As Integer ' Value of the previous ROP
        Dim Region As Integer ' Handle To a GDI Region object that I create
        Dim OldRegion As Integer ' Handle To previous Region object For the hDC
        Dim FullWind As RECT ' the bounding rectangle of the window in screen coords
        Dim Draw As RECT ' The drawing rectangle
        '
        ' Getting all of the ingredients ready
        '
        ' Get the full Rect of the window in screen co-ords
        GetWindowRect(Myhwnd, FullWind)
        ' Create a region with width and height of the window
        Region = CreateRectRgn(0, 0, FullWind.Right - FullWind.Left, FullWind.Bottom - FullWind.Top)
        ' Create an hDC for the hWnd
        ' Note: GetDC retrieves the CLIENT AREA hDC. We want the WHOLE WINDOW, including Non-Client
        ' stuff like title bar, menu, border, etc.
        hdc = GetWindowDC(Myhwnd)
        ' Save the old region
        RetVal = GetClipRgn(hdc, OldRegion)
        ' Retval = 0: no region 1: Region copied -1: error
        ' Select the new region
        RetVal = SelectObject(hdc, Region)
        ' Create a pen
        Pen = CreatePen(0, 6, 0) ' Draw Solid lines, width 6, and color black
        ' Select the pen
        ' A pen draws the lines
        OldPen = SelectObject(hdc, Pen)
        ' Create a brush
        ' A brush is the filling for a shape
        ' I need to set it to a null brush so that it doesn't edit anything
        Brush = GetStockObject(NULL_BRUSH)
        ' Select the brush
        OldBrush = SelectObject(hdc, Brush)
        ' Select the ROP
        OldROP = SetROP2(hdc, 6) ' vbInvert means, whatever is draw,
        ' invert those pixels. This means that I can undraw it by doing the same.
        '
        ' The Drawing Bits
        '
        ' Put a box around the outside of the window, using the current hDC.
        ' These coords are in device co-ordinates, i.e., of the hDC.


        With Draw
            .Left = 0
            .Top = 0
            .Bottom = FullWind.Bottom - FullWind.Top
            .Right = FullWind.Right - FullWind.Left
            Rectangle(hdc, .Left, .Top, .Right, .Bottom) ' Really easy to understand - draw a rectangle, hDC, and coordinates
        End With

        '
        ' The Washing Up bits
        '
        ' This is a very important part, as it releases memory that has been taken up.
        ' If we don't do this, windows crashes due to a memory leak.
        ' You probably get a blue screen (altohugh I'm not sure)
        '
        ' Get back the old region


        SelectObject(hdc, OldRegion)
        ' Return the previous ROP
        SetROP2(hdc, OldROP)
        ' Return to the previous brush


        SelectObject(hdc, OldBrush)
        ' Return the previous pen


        SelectObject(hdc, OldPen)
        ' Delete the Brush I created
        DeleteObject(Brush)
        ' Delete the Pen I created
        DeleteObject(Pen)
        ' Delete the region I created
        DeleteObject(Region)
        ' Release the hDC back to window's resource pool
        ReleaseDC(Myhwnd, hdc)
    End Sub

    Private Sub frmFindWindow_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        frmMain.Show()
    End Sub

    Private Sub Form_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Me.Visible = False
        ' Set the selecting flag
        Selecting = True
        ' Capture all mouse events to this window form
        SetCapture(CInt(Me.Handle))
        ' Simulate a mouse movement event to draw the border when the mouse button goes down
        Form_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub Form_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        ' Security catch to make sure that the graphics don't get mucked up when not selecting
        If Selecting = False Then Exit Sub
        ' Call the "Draw" subroutine
        Draw()
    End Sub

    Private Sub Form_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        Me.Visible = True
        ' If not selecting, then skip
        If Selecting = False Then Exit Sub
        ' Clean up the graphics drawn
        UnDraw()
        ' Release mouse capture
        ReleaseCapture()
        ' Not selecting
        Selecting = False
        ' Found our handle
        found(Myhwnd)
        ' Reset the variable
        Myhwnd = 0
    End Sub

    Private Sub frmFindWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmMain.Hide()
    End Sub

    Private Sub found(ByVal hWnd As Integer)

        ' Get process ID
        Dim pid As Integer
        GetWindowThreadProcessId(hWnd, pid)

        frmMain.lvProcess.SelectedItems.Clear()
        For Each it As ListViewItem In frmMain.lvProcess.Items
            If frmMain.lvProcess.GetItemByKey(it.Name).Infos.Pid = pid Then
                it.Selected = True
                it.EnsureVisible()
                Exit For
            End If
        Next

        frmMain.Ribbon.ActiveTab = frmMain.ProcessTab
        Call frmMain.Ribbon_MouseMove(Nothing, Nothing)
        frmMain.Show()
        frmMain.lvProcess.Focus()

        Me.Close()

    End Sub
End Class