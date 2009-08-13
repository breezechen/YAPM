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

Public Class cProcessMemReader
    Implements IDisposable

    ' ========================================
    ' Private
    ' ========================================
    Private _pid As Integer
    Private _hProc As IntPtr


    ' ========================================
    ' Constructor & destructor
    ' ========================================
    Public Sub New(ByVal pid As Integer)
        _hProc = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.QueryInformation Or _
                                  Native.Security.ProcessAccess.VmRead, False, pid)
    End Sub
    Public Sub Dispose() Implements System.IDisposable.Dispose
        If _hProc <> IntPtr.Zero Then
            Call Native.Api.NativeFunctions.CloseHandle(_hProc)
        End If
    End Sub


    ' ========================================
    ' Properties
    ' ========================================
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public ReadOnly Property ProcessHandle() As IntPtr
        Get
            Return _hProc
        End Get
    End Property


    ' ========================================
    ' Public functions
    ' ========================================

    ' Get PEB
    Public Function GetPEBAddress() As IntPtr
        Dim ret As Integer
        Dim pbi As New Native.Api.NativeStructs.ProcessBasicInformation
        Native.Api.NativeFunctions.NtQueryInformationProcess(_hProc, Native.Api.NativeEnums.ProcessInformationClass.ProcessBasicInformation, _
                                      pbi, Marshal.SizeOf(pbi), ret)
        Return pbi.PebBaseAddress
    End Function

    ' Read an Int32
    Public Function ReadInt32(ByVal offset As IntPtr) As Integer
        Dim buffer(0) As Integer
        Dim lByte As Integer
        ' 4 bytes for an Int32
        Native.Api.NativeFunctions.ReadProcessMemory(_hProc, offset, buffer, &H4, lByte)
        Return buffer(0)
    End Function

    ' Read a pointer
    Public Function ReadIntPtr(ByVal offset As IntPtr) As IntPtr
        Dim buffer(0) As IntPtr
        Dim lByte As Integer
        Native.Api.NativeFunctions.ReadProcessMemory(_hProc, offset, buffer, _
                                                     Marshal.SizeOf(offset), lByte)
        Return buffer(0)
    End Function

    ' Read a byte array
    Public Function ReadByteArray(ByVal offset As IntPtr, ByVal size As Integer) As Byte()
        Dim buffer() As Byte
        Dim lByte As Integer
        ReDim buffer(size - 1)
        Native.Api.NativeFunctions.ReadProcessMemory(_hProc, offset, buffer, size, lByte)
        Return buffer
    End Function

    ' Read a structure
    Public Function ReadStruct(Of T)(ByVal offset As IntPtr) As T

        Dim ret As T

        ' Size of the structure
        Dim structSize As Integer = Marshal.SizeOf(GetType(T))

        ' Buffer of byte which received the data read
        Dim buf() As Byte = ReadByteArray(offset, structSize)

        ' Retrieve a structure
        Dim dataH As GCHandle = GCHandle.Alloc(buf, GCHandleType.Pinned)
        Try
            ret = DirectCast(Marshal.PtrToStructure(dataH.AddrOfPinnedObject, GetType(T)), T)
        Finally
            dataH.Free()
        End Try

        ' Return struct
        Return ret

    End Function

    ' Read an unicode string
    Public Function ReadUnicodeString(ByVal str As Native.Api.NativeStructs.UnicodeString) As String
        If str.Length = 0 Then
            Return Nothing
        End If

        ' Read buffer from memory
        Dim buf() As Byte = ReadByteArray(str.Buffer, str.Length)
        Dim dataH As GCHandle = GCHandle.Alloc(buf, GCHandleType.Pinned)
        Try
            Return Marshal.PtrToStringUni(dataH.AddrOfPinnedObject, CInt(str.Length / 2))
        Finally
            dataH.Free()
        End Try
    End Function

End Class
