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

Public Class cAsyncProcInfoDownload

    Public Event GotInformations(ByRef result As InternetProcessInfo)
    Public Enum SecurityRisk As Integer
        Unknow = -1
        Safe = 0
        Caution1 = 1
        Caution2 = 2
        Alert1 = 3
        Alert2 = 4
        Alert3 = 4
    End Enum
    Public Structure InternetProcessInfo
        Dim _Description As String
        Dim _Risk As SecurityRisk
    End Structure

    Private _procName As String
    Private Const NO_INFO_RETRIEVED As String = "N\A"

    Public Property ProcessName() As String
        Get
            Return _procName
        End Get
        Set(ByVal value As String)
            _procName = value
        End Set
    End Property

    Public Sub New(ByVal aProcessName As String)
        _procName = aProcessName
    End Sub

    ' Start the download of informations
    Public Sub BeginDownload()
        Dim ret As InternetProcessInfo = Nothing

        ' Download source page of
        ' http://www.processlibrary.com/directory/files/PROCESSS/
        ' and retrieve security risk from source :
        '<h4 class="red-heading">Security risk (0-5):</h4><p>0</p>

        Dim s As String
        s = Common.Misc.DownloadPage("http://www.processlibrary.com/directory/files/" & _
                         LCase(_procName) & "/")

        Dim i As Integer = InStr(s, "Security risk (0-5)", CompareMethod.Binary)
        Dim d1 As Integer = InStr(s, ">Description</h4>", CompareMethod.Binary)
        Dim d2 As Integer = InStr(d1 + 1, s, "</p>", CompareMethod.Binary)

        If i > 0 Then
            Dim z As String = s.Substring(i + 27, 1)
            ret._Risk = CType(CInt(Val(z)), SecurityRisk)
            If d1 > 0 And d2 > 0 Then
                Dim z2 As String = s.Substring(d1 + 23, d2 - d1 - 24)
                ret._Description = Replace(z2, "<BR><BR>", vbNewLine)
            Else
                ret._Description = NO_INFO_RETRIEVED
            End If
        Else
            ret._Risk = SecurityRisk.Unknow
        End If

        RaiseEvent GotInformations(ret)
    End Sub


End Class
