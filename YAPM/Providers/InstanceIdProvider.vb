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

Imports System.Management

Public Class InstanceIdProvider

    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' Instance ID 
    Private Shared _instanceId As Integer


    ' ========================================
    ' Public properties
    ' ========================================


    ' ========================================
    ' Other public
    ' ========================================


    ' ========================================
    ' Public functions
    ' ========================================

    ' Get a new unique instance id
    Public Shared Function GetNewInstanceId() As Integer
        System.Threading.Interlocked.Increment(_instanceId)
        Return _instanceId
    End Function


    ' ========================================
    ' Private functions
    ' ========================================


End Class
