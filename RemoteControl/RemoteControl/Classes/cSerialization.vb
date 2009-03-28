' =======================================================
' Yet Another Process Monitor (YAPM)
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
Imports System.Runtime.Serialization.Formatters.Binary

Public Class cSerialization

    ' Return byte array from data class
    Public Shared Function GetSerializedObject(ByVal obj As cSocketData) As Byte()
        Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Using ms As New MemoryStream()
            formatter.Serialize(ms, obj)
            Return ms.ToArray()
        End Using
    End Function

    ' Return data class from byte array
    Public Shared Function DeserializeObject(ByVal dataBytes As Byte()) As cSocketData
        Dim formatter As System.Runtime.Serialization.IFormatter = New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Using ms As New MemoryStream(dataBytes)
            Return DirectCast(formatter.Deserialize(ms), cSocketData)
        End Using
    End Function

End Class
