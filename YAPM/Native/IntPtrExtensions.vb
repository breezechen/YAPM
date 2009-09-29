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
Imports System.Runtime.CompilerServices

' http://blogs.msdn.com/jaredpar/archive/2008/11/11/properly-incrementing-an-intptr.aspx

Namespace System.Runtime.CompilerServices

    <AttributeUsage(AttributeTargets.Method Or AttributeTargets.Assembly Or AttributeTargets.Class)> _
    Public Class ExtensionAttribute
        Inherits Attribute
    End Class

End Namespace

Public Module IntPtrExtensions

    ' This module extendes the methods available for IntPtr.
    ' So now we are able to move the pointer pointed by IntPtr using
    ' Increment or Decrement

    ' Increment
    <System.Runtime.CompilerServices.Extension()> _
    Public Function Increment(ByVal ptr As IntPtr, ByVal cbSize As Integer) As IntPtr
        Return New IntPtr(ptr.ToInt64 + cbSize)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Increment(ByVal ptr As IntPtr, ByVal cbSize As IntPtr) As IntPtr
        Return New IntPtr(ptr.ToInt64 + cbSize.ToInt64)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Increment(Of T)(ByVal ptr As IntPtr) As IntPtr
        Return ptr.Increment(Marshal.SizeOf(GetType(T)))
    End Function


    ' Decrement
    <System.Runtime.CompilerServices.Extension()> _
    Public Function Decrement(ByVal ptr As IntPtr, ByVal cbSize As Integer) As IntPtr
        Return New IntPtr(ptr.ToInt64 - cbSize)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Decrement(Of T)(ByVal ptr As IntPtr) As IntPtr
        Return ptr.Decrement(Marshal.SizeOf(GetType(T)))
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Decrement(ByVal ptr As IntPtr, ByVal cbSize As IntPtr) As IntPtr
        Return New IntPtr(ptr.ToInt64 - cbSize.ToInt64)

    End Function


    ' Return element at index
    <System.Runtime.CompilerServices.Extension()> _
    Public Function ElementAt(Of T)(ByVal ptr As IntPtr, ByVal index As Integer) As T
        Dim offset As Integer = Marshal.SizeOf(GetType(T)) * index
        Dim offsetPtr As IntPtr = ptr.Increment(offset)
        Return DirectCast(Marshal.PtrToStructure(offsetPtr, GetType(T)), T)
    End Function


    ' Compare methods
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsNull(ByVal ptr As IntPtr) As Boolean
        Return (ptr = IntPtr.Zero)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsNotNull(ByVal ptr As IntPtr) As Boolean
        Return (ptr <> IntPtr.Zero)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsGreaterThan(ByVal ptr As IntPtr, ByVal ptr2 As IntPtr) As Boolean
        Return (ptr.ToInt64 > ptr2.ToInt64)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsGreaterThan(ByVal ptr As IntPtr, ByVal ptr2 As Integer) As Boolean
        Return (ptr.ToInt64 > ptr2)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsGreaterThan(ByVal ptr As IntPtr, ByVal ptr2 As Long) As Boolean
        Return (ptr.ToInt64 > ptr2)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsLowerThan(ByVal ptr As IntPtr, ByVal ptr2 As IntPtr) As Boolean
        Return (ptr.ToInt64 < ptr2.ToInt64)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsLowerThan(ByVal ptr As IntPtr, ByVal ptr2 As Integer) As Boolean
        Return (ptr.ToInt64 < ptr2)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsLowerThan(ByVal ptr As IntPtr, ByVal ptr2 As Long) As Boolean
        Return (ptr.ToInt64 < ptr2)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsGreaterOrEqualThan(ByVal ptr As IntPtr, ByVal ptr2 As IntPtr) As Boolean
        Return (ptr.ToInt64 >= ptr2.ToInt64)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsGreaterOrEqualThan(ByVal ptr As IntPtr, ByVal ptr2 As Integer) As Boolean
        Return (ptr.ToInt64 >= ptr2)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsGreaterOrEqualThan(ByVal ptr As IntPtr, ByVal ptr2 As Long) As Boolean
        Return (ptr.ToInt64 >= ptr2)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsLowerOrEqualThan(ByVal ptr As IntPtr, ByVal ptr2 As IntPtr) As Boolean
        Return (ptr.ToInt64 <= ptr2.ToInt64)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsLowerOrEqualThan(ByVal ptr As IntPtr, ByVal ptr2 As Integer) As Boolean
        Return (ptr.ToInt64 <= ptr2)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsLowerOrEqualThan(ByVal ptr As IntPtr, ByVal ptr2 As Long) As Boolean
        Return (ptr.ToInt64 <= ptr2)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsEqualTo(ByVal ptr As IntPtr, ByVal ptr2 As IntPtr) As Boolean
        Return (ptr.ToInt64 = ptr2.ToInt64)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsEqualTo(ByVal ptr As IntPtr, ByVal ptr2 As Integer) As Boolean
        Return (ptr.ToInt64 = ptr2)
    End Function
    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsEqualTo(ByVal ptr As IntPtr, ByVal ptr2 As Long) As Boolean
        Return (ptr.ToInt64 = ptr2)
    End Function

End Module