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
Imports Native.Api

Namespace Native.Objects

    Public Class Task
        Inherits Native.Objects.Window


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


        ' ========================================
        ' Public functions
        ' ========================================

        ' Enumerate tasks (as windowInfos)
        Public Shared Function EnumerateTasks() As Dictionary(Of String, windowInfos)
            Dim currWnd As IntPtr
            Dim cpt As Integer

            Dim _dico As New Dictionary(Of String, windowInfos)

            currWnd = Window.GetWindow(Window.GetDesktopWindow, _
                                        Native.Api.NativeEnums.GetWindowCmd.Child)
            cpt = 0
            Do While currWnd.IsNotNull

                If Window.IsWindowATask(currWnd) Then
                    Dim pid As Integer = Window.GetProcessIdFromWindowHandle(currWnd)
                    Dim tid As Integer = Window.GetThreadIdFromWindowHandle(currWnd)
                    Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString

                    If _dico.ContainsKey(key) = False Then
                        If Program.Parameters.ModeServer Then
                            ' Then we need to retrieve all informations
                            ' (this is server mode)
                            Dim wInfo As windowInfos
                            wInfo = New windowInfos(pid, tid, currWnd, Window.GetWindowCaption(currWnd))
                            wInfo.SetNonFixedInfos(GetNonFixedInfoByHandle(currWnd))
                            _dico.Add(key, wInfo)
                        Else
                            _dico.Add(key, New windowInfos(pid, tid, currWnd, Window.GetWindowCaption(currWnd)))
                        End If
                    End If

                End If

                currWnd = Window.GetWindow(currWnd, _
                                        Native.Api.NativeEnums.GetWindowCmd.[Next])
            Loop

            Return _dico
        End Function


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
