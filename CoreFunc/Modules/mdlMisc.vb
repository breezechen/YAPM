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

Imports Microsoft.Win32

Module mdlMisc

    Private sizeUnits() As String = {"Bytes", "KB", "MB", "GB", "TB", "PB", "EB"}

    ' Get a formated value as a string (in Bytes, KB, MB or GB) from an Integer
    Public Function GetFormatedSize(ByVal size As Integer, Optional ByVal digits As Integer = 3) As String
        Return GetFormatedSize(New Decimal(size), digits)
    End Function
    Public Function GetFormatedSize(ByVal size As ULong, Optional ByVal digits As Integer = 3) As String
        Return GetFormatedSize(New Decimal(size), digits)
    End Function
    Public Function GetFormatedSize(ByVal size As UInteger, Optional ByVal digits As Integer = 3) As String
        Return GetFormatedSize(New Decimal(size), digits)
    End Function
    Public Function GetFormatedSize(ByVal size As Decimal, Optional ByVal digits As Integer = 3) As String
        Dim t As Decimal = size
        Dim dep As Integer = 0

        While t >= 1024
            t /= 1024
            dep += 1
        End While

        Return CStr(Math.Round(t, digits)) & " " & sizeUnits(dep)

    End Function
    Public Function GetSizeFromFormatedSize(ByVal _frmtSize As String) As Long
        ' Get position of space
        If _frmtSize Is Nothing OrElse _frmtSize.Length < 4 Then
            Return 0
        End If

        Dim x As Integer = -1
        For Each _unit As String In sizeUnits
            x += 1
            Dim i As Integer = InStrRev(_frmtSize, " " & _unit)
            If i > 0 Then
                Dim z As String = _frmtSize.Substring(0, i - 1)
                Return CLng(Double.Parse(z, Globalization.NumberStyles.Float) * 1024 ^ x)
            End If
        Next

    End Function

    ' Return true if a string is (or seems to be) a formated size
    Public Function IsFormatedSize(ByVal _str As String) As Boolean
        If _str Is Nothing OrElse _str.Length < 4 Then
            Return False
        End If
        ' Try to find " UNIT" in _str
        ' Return true if first char is a numeric value
        For Each _unit As String In sizeUnits
            Dim i As Integer = InStrRev(_str, " " & _unit)
            If i > 0 AndAlso (i + _unit.Length) = _str.Length Then
                Return IsNumeric(_str.Substring(0, 1))
            End If
        Next
        Return False
    End Function

    ' Return true if a string is (or seems to be) a hexadecimal expression
    Public Function IsHex(ByVal _str As String) As Boolean
        If _str Is Nothing OrElse _str.Length < 4 Then
            Return False
        End If
        Return (_str.Substring(0, 2) = "0x")
    End Function

    ' Return long value from hex value
    Public Function HexToLong(ByVal _str As String) As Long
        If _str Is Nothing OrElse _str.Length < 4 Then
            Return 0
        End If
        Return Long.Parse(_str.Substring(2, _str.Length - 2), _
                          Globalization.NumberStyles.AllowHexSpecifier)
    End Function

    ' Get a formated percentage
    Public Function GetFormatedPercentage(ByVal p As Double, Optional ByVal digits As Integer = 3) As String
        Dim d100 As Double = p * 100
        Dim d As Double = Math.Round(d100, digits)
        Dim tr As Double = Math.Truncate(d)
        Dim lp As Integer = CInt(tr)
        Dim rp As Integer = CInt((d100 - tr) * 10 ^ digits)

        Return CStr(IIf(lp < 10, "0", "")) & CStr(lp) & "." & CStr(IIf(rp < 10, "00", "")) & CStr(IIf(rp < 100 And rp >= 10, "0", "")) & CStr(rp)
    End Function

End Module
