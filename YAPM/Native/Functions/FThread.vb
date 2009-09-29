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
Imports Native.Api.NativeStructs
Imports Native.Api.NativeEnums
Imports Native.Api.NativeFunctions

Namespace Native.Functions

    Public Class Thread


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

        ' Return a class from an int (concerning priority)
        Public Shared Function GetThreadPriorityClassFromInt(ByVal priority As Integer) As System.Diagnostics.ThreadPriorityLevel
            If priority >= 15 Then
                Return ThreadPriorityLevel.TimeCritical
            ElseIf priority >= 2 Then
                Return ThreadPriorityLevel.Highest
            ElseIf priority >= 1 Then
                Return ThreadPriorityLevel.AboveNormal
            ElseIf priority >= 0 Then
                Return ThreadPriorityLevel.Normal
            ElseIf priority >= -1 Then
                Return ThreadPriorityLevel.BelowNormal
            ElseIf priority >= -2 Then
                Return ThreadPriorityLevel.Lowest
            Else
                Return ThreadPriorityLevel.Idle
            End If
        End Function


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
