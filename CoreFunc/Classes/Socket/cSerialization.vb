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

Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.Serialization.Formatters.Binary

Public Class cSerialization

    ' Return byte array from data class
    Public Shared Function GetSerializedObject(ByVal obj As cSocketData) As Byte()
        Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Using ms As New MemoryStream()
            formatter.Serialize(ms, obj)
            Return CompressByteArray(ms.ToArray())
        End Using
    End Function

    ' Return data class from byte array
    Public Shared Function DeserializeObject(ByVal dataBytes As Byte()) As cSocketData
        Try
            Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Using ms As New MemoryStream(DeCompressByteArray(dataBytes))
                Return DirectCast(formatter.Deserialize(ms), cSocketData)
            End Using
        Catch ex As Exception
            Trace.WriteLine("Error during serialization : " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Shared Function CompressByteArray(ByRef b() As Byte) As Byte()
        'Return b
        Dim ms As New MemoryStream()
        Dim s As Stream = New GZipStream(ms, CompressionMode.Compress)
        s.Write(b, 0, b.Length)
        s.Close()
        Return DirectCast(ms.ToArray(), Byte())
    End Function

    Private Shared Function DeCompressByteArray(ByRef b() As Byte) As Byte()
        ' Return b
        Dim writeData(4096) As Byte ' = new byte[4096]
        Dim memStream As MemoryStream = New MemoryStream()
        Dim s2 As Stream = New GZipStream(New MemoryStream(b), CompressionMode.Decompress)
        Dim size As Integer = 1

        While (size > 0)
            size = s2.Read(writeData, 0, writeData.Length)
            memStream.Write(writeData, 0, size)
            memStream.Flush()
        End While
        Return memStream.ToArray()

    End Function

End Class
