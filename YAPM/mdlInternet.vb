Option Strict On

Module mdlInternet

    Private Declare Function GetTickCount Lib "kernel32" () As Integer

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
                ret._Description = "N/A"
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
