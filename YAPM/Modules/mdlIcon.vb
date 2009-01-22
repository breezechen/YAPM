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

' Here is some unsafe code from VB6

Module mdlIcon

    Private Structure SHFILEINFO
        Public hIcon As IntPtr            ' : icon
        Public iIcon As Integer           ' : icondex
        Public dwAttributes As Integer    ' : SFGAO_ flags
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    Private Declare Auto Function SHGetFileInfo Lib "shell32.dll" _
            (ByVal pszPath As String, _
             ByVal dwFileAttributes As Integer, _
             ByRef psfi As SHFILEINFO, _
             ByVal cbFileInfo As Integer, _
             ByVal uFlags As Integer) As IntPtr

    Private Const SHGFI_ICON As Integer = &H100
    Private Const SHGFI_SMALLICON As Integer = &H1
    Private Const SHGFI_LARGEICON As Integer = &H0    ' Large icon
    Private nIndex As Integer


    Public Function GetIcon(ByVal fName As String, Optional ByVal small As Boolean = True) _
            As System.Drawing.Icon

        Dim hImgSmall As IntPtr
        Dim hImgLarge As IntPtr
        Dim shinfo As SHFILEINFO
        shinfo = New SHFILEINFO()

        If System.IO.File.Exists(fName) = False Then Return Nothing

        If small Then
            hImgSmall = SHGetFileInfo(fName, 0, shinfo, _
                Marshal.SizeOf(shinfo), _
                SHGFI_ICON Or SHGFI_SMALLICON)
        Else
            hImgLarge = SHGetFileInfo(fName, 0, _
                shinfo, Marshal.SizeOf(shinfo), _
                SHGFI_ICON Or SHGFI_LARGEICON)
        End If

        Dim img As System.Drawing.Icon = Nothing
        Try
            img = System.Drawing.Icon.FromHandle(shinfo.hIcon)
        Catch ex As Exception
            ' Can't retrieve icon
        End Try

        Return img

    End Function

    Public Function GetIcon2(ByVal fName As String, Optional ByVal small As Boolean = True) _
        As System.Drawing.Icon

        Dim hImgSmall As IntPtr
        Dim hImgLarge As IntPtr
        Dim shinfo As SHFILEINFO
        shinfo = New SHFILEINFO()

        If small Then
            hImgSmall = SHGetFileInfo(fName, 0, shinfo, _
                Marshal.SizeOf(shinfo), _
                SHGFI_ICON Or SHGFI_SMALLICON)
        Else
            hImgLarge = SHGetFileInfo(fName, 0, _
                shinfo, Marshal.SizeOf(shinfo), _
                SHGFI_ICON Or SHGFI_LARGEICON)
        End If

        Dim img As System.Drawing.Icon = Nothing
        Try
            img = System.Drawing.Icon.FromHandle(shinfo.hIcon)
        Catch ex As Exception
            ' Can't retrieve icon
        End Try

        Return img

    End Function

End Module
