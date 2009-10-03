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

Imports Common.Misc

Public Class frmTracker

    Private Sub frmTracker_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdGoBug, "Navigate to the bug tracker")
        SetToolTip(Me.cmdGoFeed, "Send a feed back")
        SetToolTip(Me.cmdGoSug, "Navigate to the forums of YAPM on sourceforge.net")

        Dim s As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036\deflangfe1036\deftab708{\fonttbl{\f0\fswiss\fprq2\fcharset0 Arial;}}"
        s &= "{\colortbl ;\red255\green0\blue0;\red0\green0\blue255;}"
        s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\lang1033\f0\fs22 If you have any suggestion or question or idea of improvement/new feature about\i  \ul Yet Another (remote) Process Monitor\ulnone\i0 , please feel free to contact me (use one of the method below: tracker, forum, email).\par"
        s &= "\par"
        s &= "\cf1 If you find a bug, \cf0\b PLEASE\b0  use the sourceforge.net tracker and specify, if possible (it would be very helpful for me) these informations :\par"
        s &= "\pard\fi-360\li720 -\tab the version of YAPM you are using\par"
        s &= "-\tab the operating system you are using\par"
        s &= "-\tab\b a description of the bug you found \par"
        s &= "\b0 -\tab\b how to reproduce it\b0  (if possible, it would be so helpful !)\par"
        s &= "\pard\par"
        s &= " Any feed back is appreciated !\par"
        s &= "}"
        Me.rtb.Rtf = s
    End Sub

    Private Sub cmdGoBug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoBug.Click
        cFile.ShellOpenFile(Me.txtBug.Text, Me.Handle)
    End Sub

    Private Sub cmdGoSug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoSug.Click
        cFile.ShellOpenFile(Me.txtSug.Text, Me.Handle)
    End Sub

    Private Sub cmdGoFeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoFeed.Click
        cFile.ShellOpenFile("mailto:YetAnotherProcessMonitor@gmail.com", Me.Handle)
    End Sub
End Class