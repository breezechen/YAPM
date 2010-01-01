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

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms

Public Class asyncCallbackProcGetAllNonFixedInfos

    Public Event HasGotAllNonFixedInfos(ByVal Success As Boolean, ByRef newInfos As Native.Api.Structs.SystemProcessInformation64, ByVal msg As String)

    Private _process As cProcess

    Public Sub New(ByRef process As cProcess)
        _process = process
    End Sub

    ' This function is only called for WMI connexion
    ' It is called when user want to refresh statistics of a process in detailed view
    Public Sub Process(ByVal state As Object)

        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket


            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim msg As String = ""
                Dim _newInfos As New Native.Api.Structs.SystemProcessInformation64
                Dim ret As Boolean = _
                    Wmi.Objects.Process.RefreshProcessInformationsById(_process.Infos.ProcessId, _
                                                                ProcessProvider.wmiSearcher, msg, _newInfos)

                RaiseEvent HasGotAllNonFixedInfos(ret, _newInfos, msg)

            Case Else
                ' Local
                ' OK, normally no call for Process method for a local connection

        End Select
    End Sub

End Class
