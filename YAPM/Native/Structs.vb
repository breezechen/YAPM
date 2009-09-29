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

Imports System.Net
Imports Native.Api.Enums

Namespace Native.Api

    Public Class Structs

        ' OK
#Region "Declarations used for network"

        Public Structure LightConnection
            Dim dwState As Integer
            Dim local As IPEndPoint
            Dim remote As IPEndPoint
            Dim dwOwningPid As Integer
            Dim dwType As NetworkProtocol
        End Structure

        Public Structure NicDescription
            Public Name As String
            Public Description As String
            Public Ip As String
            Public Sub New(ByVal aName As String, ByVal aDesc As String, ByVal aIp As String)
                Name = aName
                Description = aDesc
                Ip = aIp
            End Sub
        End Structure

#End Region

        ' OK
#Region "Declarations used for processes"

        Public Structure ProcTimeInfo
            Dim time As Long
            Dim kernel As Long
            Dim user As Long
            Dim total As Long
            Public Sub New(ByVal aTime As Long, ByVal aUser As Long, ByVal aKernel As Long)
                time = aTime
                kernel = aKernel
                user = aUser
                total = user + kernel
            End Sub
        End Structure

        Public Structure ProcMemInfo
            Dim time As Long
            Dim mem As Native.Api.NativeStructs.VmCountersEx
            Public Sub New(ByVal aTime As Long, ByRef aMem As Native.Api.NativeStructs.VmCountersEx)
                time = aTime
                mem = aMem
            End Sub
        End Structure

        Public Structure ProcIoInfo
            Dim time As Long
            Dim io As Native.Api.NativeStructs.IoCounters
            Public Sub New(ByVal aTime As Long, ByRef aIo As Native.Api.NativeStructs.IoCounters)
                time = aTime
                io = aIo
            End Sub
        End Structure

        Public Structure ProcMiscInfo
            Dim time As Long
            Dim gdiO As Integer
            Dim userO As Integer
            Dim cpuUsage As Double
            Dim averageCpuUsage As Double
            Public Sub New(ByVal aTime As Long, ByVal aGdi As Integer, ByVal aUser As _
                           Integer, ByVal aCpu As Double, ByVal aAverage As Double)
                time = aTime
                gdiO = aGdi
                userO = aUser
                cpuUsage = aCpu
                averageCpuUsage = aAverage
            End Sub
        End Structure

#End Region

        ' OK
#Region "Declarations used for services"

        Public Structure ServiceCreationParams
            Public ServiceName As String
            Public DisplayName As String
            Public Type As NativeEnums.ServiceType
            Public StartType As NativeEnums.ServiceStartType
            Public ErrorControl As NativeEnums.ServiceErrorControl
            Public FilePath As String
            Public Arguments As String
            Public RegMachine As String
            Public RegUser As String
            Public RegPassword As System.Security.SecureString
        End Structure

#End Region

    End Class

End Namespace