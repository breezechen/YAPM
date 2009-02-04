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

Imports System.Windows.Forms.ListView

Public Class DoubleBufferedLV
    Inherits System.Windows.Forms.ListView

    ' ========================================
    ' Public
    ' ========================================
    Public Sub New()
        'Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        'Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        'Me.SetStyle(ControlStyles.ResizeRedraw, True)
        'Me.SetStyle(ControlStyles.EnableNotifyMessage, True)
        ' Me.DoubleBuffered = True
    End Sub

    Public Property OverriddenDoubleBuffered() As Boolean
        Get
            Return Me.DoubleBuffered
        End Get
        Set(ByVal value As Boolean)
            Me.DoubleBuffered = value
        End Set
    End Property

    'Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
    '    MyBase.OnPaint(e)
    '    e.Graphics.Dispose()
    'End Sub
    'Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawListViewItemEventArgs)
    '    MyBase.OnDrawItem(e)
    '    e.Graphics.Dispose()
    'End Sub
    'Protected Overrides Sub OnDrawSubItem(ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs)
    '    MyBase.OnDrawSubItem(e)
    '    e.Graphics.Dispose()
    'End Sub
    'Protected Overrides Sub OnNotifyMessage(ByVal m As System.Windows.Forms.Message)
    '    'If m.Msg <> &H14 Then _
    '    MyBase.OnNotifyMessage(m)
    'End Sub

End Class