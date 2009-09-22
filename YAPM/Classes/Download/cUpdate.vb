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

Public Class cUpdate


    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================


    ' ========================================
    ' Public properties
    ' ========================================


    ' ========================================
    ' Other public
    ' ========================================

    ' Structure for new versions
    Public Structure NewReleaseInfos
        Public Infos As String
        Public Url As String
        Public Hash As String
        Public Sub New(ByVal aInfos As String, ByVal aUrl As String, ByVal aHash As String)
            Infos = aInfos
            Url = aUrl
            Hash = aHash
        End Sub
    End Structure

    ' Event raised when a new update is available
    Public Event NewVersionAvailable(ByVal silent As Boolean, _
                                     ByVal release As NewReleaseInfos)

    ' Event raised when no new update is available
    Public Event ProgramUpToDate(ByVal silent As Boolean)


    ' ========================================
    ' Public functions
    ' ========================================

    ' Check if YAPM is up to date
    ' This is async. and will raise events.
    Public Sub CheckUpdates(ByVal silentMode As Boolean)
        Dim newRelease As NewReleaseInfos = Nothing
        Dim upToDate As Boolean


        ' Raise event
        If silentMode Then
            If upToDate Then
                RaiseEvent NewVersionAvailable(silentMode, newRelease)
            End If
        Else
            If upToDate Then
                RaiseEvent ProgramUpToDate(silentMode)
            Else
                RaiseEvent NewVersionAvailable(silentMode, newRelease)
            End If
        End If

    End Sub

    ' Download the last version
    ' This is async. and may displays some messageboxes
    Public Sub DownloadRelease(ByVal release As NewReleaseInfos)
        '
    End Sub



    ' ========================================
    ' Private functions
    ' ========================================


End Class
