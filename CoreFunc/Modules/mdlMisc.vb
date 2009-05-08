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

Imports Microsoft.Win32

Public Module mdlMisc

    Private Const NO_INFO_RETRIEVED As String = "N/A"
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

    ' Get formated size per second
    Public Function GetFormatedSizePerSecond(ByVal size As Decimal, Optional ByVal digits As Integer = 3) As String
        Dim t As Decimal = size
        Dim dep As Integer = 0

        While t >= 1024
            t /= 1024
            dep += 1
        End While

        Return CStr(Math.Round(t, digits)) & " " & sizeUnits(dep) & "/s"

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

    ' Convert a DMTF datetime to a valid Date
    Public Function DMTFDateToDateTime(ByVal dmtfDate As String) As Date
        Try
            Dim initializer As Date = Date.MinValue
            Dim year As Integer = initializer.Year
            Dim month As Integer = initializer.Month
            Dim day As Integer = initializer.Day
            Dim hour As Integer = initializer.Hour
            Dim minute As Integer = initializer.Minute
            Dim second As Integer = initializer.Second
            Dim ticks As Long = 0
            Dim dmtf As String = dmtfDate
            Dim datetime As Date = Date.MinValue
            Dim tempString As String = String.Empty
            tempString = dmtf.Substring(0, 4)
            If ("****" <> tempString) Then
                year = Integer.Parse(tempString)
            End If
            tempString = dmtf.Substring(4, 2)
            If ("**" <> tempString) Then
                month = Integer.Parse(tempString)
            End If
            tempString = dmtf.Substring(6, 2)
            If ("**" <> tempString) Then
                day = Integer.Parse(tempString)
            End If
            tempString = dmtf.Substring(8, 2)
            If ("**" <> tempString) Then
                hour = Integer.Parse(tempString)
            End If
            tempString = dmtf.Substring(10, 2)
            If ("**" <> tempString) Then
                minute = Integer.Parse(tempString)
            End If
            tempString = dmtf.Substring(12, 2)
            If ("**" <> tempString) Then
                second = Integer.Parse(tempString)
            End If
            tempString = dmtf.Substring(15, 6)
            If ("******" <> tempString) Then
                ticks = (Long.Parse(tempString) * CType((System.TimeSpan.TicksPerMillisecond / 1000), Long))
            End If

            datetime = New Date(year, month, day, hour, minute, second, 0)
            datetime = datetime.AddTicks(ticks)
            Dim tickOffset As System.TimeSpan = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime)
            Dim UTCOffset As Integer = 0
            Dim OffsetToBeAdjusted As Integer = 0
            Dim OffsetMins As Long = CType((tickOffset.Ticks / System.TimeSpan.TicksPerMinute), Long)
            tempString = dmtf.Substring(22, 3)
            If (tempString <> "******") Then
                tempString = dmtf.Substring(21, 4)
                UTCOffset = Integer.Parse(tempString)
                OffsetToBeAdjusted = CType((OffsetMins - UTCOffset), Integer)
                datetime = datetime.AddMinutes(CType(OffsetToBeAdjusted, Double))
            End If
            Return datetime
        Catch ex As Exception
            Return New Date(0)
        End Try
    End Function

    Public Function ReadUnicodeString(ByVal str As API.UNICODE_STRING) As String
        If str.Length = 0 Then
            Return Nothing
        End If
        Return System.Runtime.InteropServices.Marshal.PtrToStringUni(New IntPtr(str.Buffer), CInt(str.Length / 2))
    End Function

    ' Get a good path
    Public Function GetRealPath(ByVal path As String) As String
        If Len(path) > 0 Then
            If path.ToLowerInvariant.StartsWith("\systemroot\") Then
                path = path.Substring(12, path.Length - 12)
                Dim ii As Integer = InStr(path, "\", CompareMethod.Binary)
                If ii > 0 Then
                    path = path.Substring(ii, path.Length - ii)
                    path = Environment.SystemDirectory & "\" & path
                End If
            ElseIf path.StartsWith("\??\") Then
                path = path.Substring(4)
            ElseIf path.StartsWith(Char.ConvertFromUtf32(34)) Then
                If path.Length > 2 Then
                    path = path.Substring(1, path.Length - 2)
                End If
            End If
        Else
            path = NO_INFO_RETRIEVED
        End If
        Return path
    End Function

    ' Get path from a command
    Public Function GetPathFromCommand(ByVal path As String) As String
        If IO.File.Exists(path) Then
            Return path
        Else
            Return cFile.IntelligentPathRetrieving2(path)
        End If
    End Function

    ' Get a file name from a path
    Public Function GetFileName(ByVal _path As String) As String
        Dim i As Integer = InStrRev(_Path, "\", , CompareMethod.Binary)
        If i > 0 Then
            Return Right(_Path, _Path.Length - i)
        Else
            Return vbNullString
        End If
    End Function

End Module
