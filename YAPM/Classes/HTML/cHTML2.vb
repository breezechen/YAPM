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

Imports System.Text

Public Class cHTML2

    ' This class allows to create an HTML file (special array).
    ' ---------------------------------------
    ' |  C:\Windows\Explorer.exe            |       (bold)
    ' ---------------------------------------
    ' | Size      | 1245 MB                 |
    ' ---------------------------------------
    ' | ParentDir | C:\Windows\             |
    ' ---------------------------------------
    ' | Copyright | (c) Microsoft           |
    ' ---------------------------------------

    ' This is supposed to be normalized HTML
    ' An HTML page produced by this class should successfully be checked as XHTML 1.0 Transitional

    ' ========================================
    ' Public declarations
    ' ========================================
    Public Structure HtmlColumnStructure
        Dim sizePercent As Integer
        Dim title As String
    End Structure


    ' ========================================
    ' APIs
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================
    Private _code As StringBuilder
    Private _file As String
    Private _title As String
    Private _firstColSize As Integer


    ' ========================================
    ' Getter & setter
    ' ========================================



    ' ========================================
    ' Public functions
    ' ========================================

    Public Sub New(ByVal destination As String, ByVal title As String, ByVal firstColSize As Integer)

        MyBase.New()

        _firstColSize = firstColSize
        _file = destination
        _title = normaliszeISOhtml(title)

        Call initHTML()

    End Sub

    ' Export (create) the HTML file
    Public Function ExportHTML() As Boolean

        ' Finalize HTML file
        _code.Capacity += 30
        _code.AppendLine("</table>")
        _code.AppendLine("</body>")
        _code.AppendLine("</html>")

        Try
            Dim stream As New System.IO.StreamWriter(_file, False)
            stream.Write(_code.ToString)
            stream.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    ' Reset the HTML code
    Public Sub ResetHTML()
        _code.Remove(0, _code.Length)
    End Sub

    ' Append a title (1-column) line
    Public Sub AppendTitleLine(ByVal line As String)

        Try
            _code.AppendLine("<tr class=" & Chr(34) & "titlecell" & Chr(34) & ">")
            _code.AppendLine("<th class=" & Chr(34) & "titlecellbold" & Chr(34) & " colspan=" & Chr(34) & "2" & Chr(34) & ">" & normaliszeISOhtml(line) & "</th>")
            _code.AppendLine("</tr>")
        Catch ex As Exception
            _code.Capacity += _code.Capacity
            _code.AppendLine("<tr class=" & Chr(34) & "titlecell" & Chr(34) & ">")
            _code.AppendLine("<th class=" & Chr(34) & "titlecellbold" & Chr(34) & " colspan=" & Chr(34) & "2" & Chr(34) & ">" & normaliszeISOhtml(line) & "</th>")
            _code.AppendLine("</tr>")
        End Try

    End Sub

    ' Add a new line (multi columns) in our array
    Public Sub AppendLine(ByVal line() As String)

        If line Is Nothing Then Exit Sub
        If Not (line.Length = 2) Then Exit Sub

        Try
            _code.AppendLine("<tr class=" & Chr(34) & "titlecell" & Chr(34) & ">")
            For x As Integer = 0 To line.Length - 1
                _code.AppendLine("<td class=" & Chr(34) & "normalcell" & Chr(34) & ">" & normaliszeISOhtml(line(x)) & "</td>")
            Next
            _code.AppendLine("</tr>")
        Catch ex As Exception
            _code.Capacity += _code.Capacity
            _code.AppendLine("<tr class=" & Chr(34) & "titlecell" & Chr(34) & ">")
            For x As Integer = 0 To line.Length - 1
                _code.AppendLine("<td class=" & Chr(34) & "normalcell" & Chr(34) & ">" & normaliszeISOhtml(line(x)) & "</td>")
            Next
            _code.AppendLine("</tr>")
        End Try

    End Sub


    ' ========================================
    ' Private functions
    ' ========================================
    Private Sub initHTML()

        ' Here we add beginning of HTML file
        _code = New StringBuilder

        _code.AppendLine("<!DOCTYPE html PUBLIC " & Chr(34) & "-//W3C//DTD XHTML 1.0 Transitional//EN" & Chr(34))
        _code.AppendLine("    " & Chr(34) & "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" & Chr(34) & ">")
        _code.AppendLine("<html xmlns=" & Chr(34) & "http://www.w3.org/1999/xhtml" & Chr(34) & " xml:lang=" & Chr(34) & "en" & Chr(34) & " lang=" & Chr(34) & "en" & Chr(34) & ">")
        _code.AppendLine("	<head>")
        _code.AppendLine("		<title>" & _title & "</title>")
        _code.AppendLine("		<meta http-equiv=" & Chr(34) & "content-type" & Chr(34) & " content=" & Chr(34) & "text/html; charset=UTF-8" & Chr(34) & "/>")
        _code.AppendLine("		<style type=" & Chr(34) & "text/css" & Chr(34) & ">")
        _code.AppendLine("			body {background-color: white; } ")
        _code.AppendLine("			body, table { background-color: #E5E5E5; color: black; font-family: Calibri, Times, courrier ; font-size: 15px; } ")
        _code.AppendLine("			td.normalcell{background-color : white;}")
        _code.AppendLine("			tr.titlecell{background-color : #C5C5C5;}")
        _code.AppendLine("			tr.titlecellbold{background-color : #C5C5C5; font-weight: bold;}")
        _code.AppendLine("			b.title{text-align : center;}")
        _code.AppendLine("		</style>")
        _code.AppendLine("	</head>")
        _code.AppendLine("	<body>")
        _code.AppendLine("		<table width=" & Chr(34) & "100%" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " cellspacing=" & Chr(34) & "1" & Chr(34) & " cellpadding=" & Chr(34) & "0" & Chr(34) & ">")
        _code.AppendLine("			<tr class=" & Chr(34) & "titlecell" & Chr(34) & ">")
        _code.AppendLine("              <td width=" & Chr(34) & CStr(_firstColSize) & "%" & Chr(34) & ">")
        _code.AppendLine("                  <b class=" & Chr(34) & " title" & Chr(34) & "></b>")
        _code.AppendLine("              </td>")
        _code.AppendLine("              <td width=" & Chr(34) & CStr(100 - _firstColSize) & "%" & Chr(34) & ">")
        _code.AppendLine("                  <b class=" & Chr(34) & " title" & Chr(34) & "></b>")
        _code.AppendLine("              </td>")
        _code.AppendLine("          </tr>")
    End Sub

    ' Here we format HTML (ISO norm)
    Private Function normaliszeISOhtml(ByVal innerText As String) As String
        If innerText IsNot Nothing Then
            innerText = innerText.Replace("&", "&#38;")
            innerText = innerText.Replace("<", "&#139;")
            innerText = innerText.Replace(Chr(34), "&#34;")
            innerText = innerText.Replace(">", "&#155;")
            Return innerText
        Else
            Return vbNullString
        End If
    End Function

End Class
