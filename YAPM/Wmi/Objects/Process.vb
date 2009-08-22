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

Imports System.Runtime.InteropServices
Imports YAPM.Native.Api
Imports System.Management

Namespace Wmi.Objects

    Public Class Process



        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' Enumerate processes
        Public Shared Function EnumerateProcesses( _
                        ByVal objSearcher As Management.ManagementObjectSearcher, _
                        ByRef _dico As Dictionary(Of String, processInfos), _
                        ByRef errMsg As String) As Boolean

            Dim res As ManagementObjectCollection = Nothing
            Try
                res = objSearcher.Get()
            Catch ex As Exception
                errMsg = ex.Message
                Return False
            End Try



        End Function


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
