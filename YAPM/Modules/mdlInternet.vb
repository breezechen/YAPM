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

Module mdlInternet

    Private Declare Function GetTickCount Lib "kernel32" () As Integer
    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Public Structure InternetProcessInfo
        Dim _Description As String
        Dim _Risk As Integer
    End Structure

    ' Get security risk from internet
    Public Function GetSecurityRisk(ByVal process As String) As Integer
        Dim ret As Integer = 0

        ' Download source page of
        ' http://www.processlibrary.com/directory/files/PROCESSS/
        ' and retrieve security risk from source :
        '<h4 class="red-heading">Security risk (0-5):</h4><p>0</p>

        Dim s As String
        s = DownloadPage("http://www.processlibrary.com/directory/files/" & LCase(process) & "/")

        Dim i As Integer = InStr(s, "Security risk (0-5)", CompareMethod.Binary)

        If i > 0 Then
            Dim z As String = s.Substring(i + 27, 1)
            ret = CInt(Val(z))
        Else
            ret = -1
        End If

        Return ret
    End Function

    ' Get security rick and description from internet
    Public Function GetInternetInfos(ByVal process As String) As InternetProcessInfo
        Dim ret As InternetProcessInfo = Nothing

        ' Download source page of
        ' http://www.processlibrary.com/directory/files/PROCESSS/
        ' and retrieve security risk from source :
        '<h4 class="red-heading">Security risk (0-5):</h4><p>0</p>

        Dim s As String
        s = DownloadPage("http://www.processlibrary.com/directory/files/" & LCase(process) & "/")

        Dim i As Integer = InStr(s, "Security risk (0-5)", CompareMethod.Binary)
        Dim d1 As Integer = InStr(s, ">Description</h4>", CompareMethod.Binary)
        Dim d2 As Integer = InStr(d1 + 1, s, "</p>", CompareMethod.Binary)

        If i > 0 Then
            Dim z As String = s.Substring(i + 27, 1)
            ret._Risk = CInt(Val(z))
            If d1 > 0 And d2 > 0 Then
                Dim z2 As String = s.Substring(d1 + 23, d2 - d1 - 24)
                ret._Description = Replace(z2, "<BR><BR>", vbNewLine)
            Else
                ret._Description = NO_INFO_RETRIEVED
            End If
        Else
            ret._Risk = -1
        End If

        Return ret
    End Function

    ' Download web page
    Public Function DownloadPage(ByVal sURL As String) As String

        Dim objWebRequest As System.Net.WebRequest = System.Net.HttpWebRequest.Create(sURL)
        Dim objWebResponse As System.Net.WebResponse = objWebRequest.GetResponse()
        Dim objStreamReader As System.IO.StreamReader = Nothing
        Dim ret As String = vbNullString

        Try
            objStreamReader = New System.IO.StreamReader(objWebResponse.GetResponseStream())
            ret = objStreamReader.ReadToEnd()
        Finally
            If Not objWebResponse Is Nothing Then
                objWebResponse.Close()
            End If
        End Try

        Return ret
    End Function

End Module
