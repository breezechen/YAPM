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

'http://www.pscode.com/vb/scripts/ShowCode.asp?txtCodeId=6362&lngWId=1

Imports System.Runtime.InteropServices

Public Class frmFindWindow

#Region "API"

    Private Selecting As Boolean ' Amd I currently selecting a window?
    Private BorderDrawn As Boolean ' Is there a border currently drawn that needs To be undrawn?
    Private Myhwnd As Integer ' The current hWnd that has a border drawn on it

#End Region

    Private Sub Draw()
        Dim Cursor As API.POINTAPI ' Cursor position
        Dim RetVal As Integer ' Dummy returnvalue
        Dim hdc As Integer ' hDC that we're going To be using
        Dim Pen As Integer ' Handle To a GDI Pen object
        Dim Brush As Integer ' Handle To a GDI Brush object
        Dim OldPen As Integer ' Handle To previous Pen object (to restore it)
        Dim OldBrush As Integer ' Handle To previous brush object (to restore it)
        Dim OldROP As Integer ' Value of the previous ROP
        Dim Region As Integer ' Handle To a GDI Region object that I create
        Dim OldRegion As Integer ' Handle To previous Region object For the hDC
        Dim FullWind As API.RECT ' the bounding rectangle of the window in screen coords
        Dim Draw As API.RECT ' The drawing rectangle

        ' Get the cursor
        API.GetCursorPos(Cursor)
        ' Get the window
        RetVal = API.WindowFromPoint(Cursor.X, Cursor.Y)
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
        API.GetWindowRect(Myhwnd, FullWind)
        ' Create a region with width and height of the window
        Region = API.CreateRectRgn(0, 0, FullWind.Right - FullWind.Left, FullWind.Bottom - FullWind.Top)
        ' Create an hDC for the hWnd
        ' Note: GetDC retrieves the CLIENT AREA hDC. We want the WHOLE WINDOW, including Non-Client
        ' stuff like title bar, menu, border, etc.
        hdc = API.GetWindowDC(Myhwnd)
        ' Save the old region
        RetVal = API.GetClipRgn(hdc, OldRegion)
        ' Retval = 0: no region 1: Region copied -1: error
        ' Select the new region
        RetVal = API.SelectObject(hdc, Region)
        ' Create a pen
        Pen = API.CreatePen(0, 6, 0) ' Draw Solid lines, width 6, and color black
        ' Select the pen
        ' A pen draws the lines
        OldPen = API.SelectObject(hdc, Pen)
        ' Create a brush
        ' A brush is the filling for a shape
        ' I need to set it to a null brush so th
        '     at it doesn't edit anything
        Brush = API.GetStockObject(API.NULL_BRUSH)
        ' Select the brush
        OldBrush = API.SelectObject(hdc, Brush)
        ' Select the ROP
        OldROP = API.SetROP2(hdc, 6) ' vbInvert means, whatever is draw,
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
            API.Rectangle(hdc, .Left, .Top, .Right, .Bottom) ' Really easy to understand - draw a rectangle, hDC, and coordinates
        End With

        '
        ' The Washing Up bits
        '
        ' This is a very important part, as it releases memory that has been taken up.
        ' If we don't do this, windows crashes due to a memory leak.
        ' You probably get a blue screen (altohugh I'm not sure)
        '
        ' Get back the old region


        API.SelectObject(hdc, OldRegion)
        ' Return the previous ROP
        API.SetROP2(hdc, OldROP)
        ' Return to the previous brush


        API.SelectObject(hdc, OldBrush)
        ' Return the previous pen


        API.SelectObject(hdc, OldPen)
        ' Delete the Brush I created
        API.DeleteObject(Brush)
        ' Delete the Pen I created
        API.DeleteObject(Pen)
        ' Delete the region I created
        API.DeleteObject(Region)
        ' Release the hDC back to window's resource pool
        API.ReleaseDC(Myhwnd, hdc)
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
        Dim FullWind As API.RECT ' the bounding rectangle of the window in screen coords
        Dim Draw As API.RECT ' The drawing rectangle
        '
        ' Getting all of the ingredients ready
        '
        ' Get the full Rect of the window in screen co-ords
        API.GetWindowRect(Myhwnd, FullWind)
        ' Create a region with width and height of the window
        Region = API.CreateRectRgn(0, 0, FullWind.Right - FullWind.Left, FullWind.Bottom - FullWind.Top)
        ' Create an hDC for the hWnd
        ' Note: GetDC retrieves the CLIENT AREA hDC. We want the WHOLE WINDOW, including Non-Client
        ' stuff like title bar, menu, border, etc.
        hdc = API.GetWindowDC(Myhwnd)
        ' Save the old region
        RetVal = API.GetClipRgn(hdc, OldRegion)
        ' Retval = 0: no region 1: Region copied -1: error
        ' Select the new region
        RetVal = API.SelectObject(hdc, Region)
        ' Create a pen
        Pen = API.CreatePen(0, 6, 0) ' Draw Solid lines, width 6, and color black
        ' Select the pen
        ' A pen draws the lines
        OldPen = API.SelectObject(hdc, Pen)
        ' Create a brush
        ' A brush is the filling for a shape
        ' I need to set it to a null brush so that it doesn't edit anything
        Brush = API.GetStockObject(API.NULL_BRUSH)
        ' Select the brush
        OldBrush = API.SelectObject(hdc, Brush)
        ' Select the ROP
        OldROP = API.SetROP2(hdc, 6) ' vbInvert means, whatever is draw,
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
            API.Rectangle(hdc, .Left, .Top, .Right, .Bottom) ' Really easy to understand - draw a rectangle, hDC, and coordinates
        End With

        '
        ' The Washing Up bits
        '
        ' This is a very important part, as it releases memory that has been taken up.
        ' If we don't do this, windows crashes due to a memory leak.
        ' You probably get a blue screen (altohugh I'm not sure)
        '
        ' Get back the old region


        API.SelectObject(hdc, OldRegion)
        ' Return the previous ROP
        API.SetROP2(hdc, OldROP)
        ' Return to the previous brush


        API.SelectObject(hdc, OldBrush)
        ' Return the previous pen


        API.SelectObject(hdc, OldPen)
        ' Delete the Brush I created
        API.DeleteObject(Brush)
        ' Delete the Pen I created
        API.DeleteObject(Pen)
        ' Delete the region I created
        API.DeleteObject(Region)
        ' Release the hDC back to window's resource pool
        API.ReleaseDC(Myhwnd, hdc)
    End Sub

    Private Sub frmFindWindow_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        _frmMain.WindowState = FormWindowState.Normal
        _frmMain.Show()
    End Sub

    Private Sub Form_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Me.Visible = False
        ' Set the selecting flag
        Selecting = True
        ' Capture all mouse events to this window form
        API.SetCapture(CInt(Me.Handle))
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
        API.ReleaseCapture()
        ' Not selecting
        Selecting = False
        ' Found our handle
        found(Myhwnd)
        ' Reset the variable
        Myhwnd = 0
    End Sub

    Private Sub frmFindWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _frmMain.Hide()
    End Sub

    Private Sub found(ByVal hWnd As Integer)

        ' Get process ID
        Dim pid As Integer
        API.GetWindowThreadProcessId(hWnd, pid)

        _frmMain.lvProcess.SelectedItems.Clear()
        For Each it As ListViewItem In _frmMain.lvProcess.Items
            Dim cp As cProcess = _frmMain.lvProcess.GetItemByKey(it.Name)
            If cp IsNot Nothing AndAlso cp.Infos.Pid = pid Then
                it.Selected = True
                it.EnsureVisible()
                Exit For
            End If
        Next

        _frmMain.Ribbon.ActiveTab = _frmMain.ProcessTab
        Call _frmMain.Ribbon_MouseMove(Nothing, Nothing)
        _frmMain.Show()
        _frmMain.lvProcess.Focus()

        Me.Close()

    End Sub
End Class