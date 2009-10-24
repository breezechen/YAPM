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
Imports Native.Api

Namespace Native.Objects


    Public Class DebugBuffer
        Implements IDisposable

        ' Properties
        Private _buffer As IntPtr = IntPtr.Zero
        Public ReadOnly Property Buffer() As IntPtr
            Get
                Return _buffer
            End Get
        End Property

        ' Constructor
        Public Sub New()
            _buffer = NativeFunctions.RtlCreateQueryDebugBuffer(0, True)
        End Sub

        ' Return memory as DebugInformation
        Public Function GetDebugInformation() As NativeStructs.DebugInformation
            Dim res As NativeStructs.DebugInformation = Nothing
            If _buffer.IsNotNull Then
                Dim mem As New Native.Memory.MemoryAlloc(_buffer)
                res = mem.ReadStruct(Of NativeStructs.DebugInformation)()
            End If
            Return res
        End Function

        ' Query debug information
        Public Sub Query(ByVal pid As Integer, ByVal flags As NativeEnums.RtlQueryProcessDebugInformationFlags)
            If _buffer.IsNotNull Then
                NativeFunctions.RtlQueryProcessDebugInformation(New IntPtr(pid), flags, _buffer)
            End If
        End Sub

        ' Destructor
        Public Sub Dispose() Implements IDisposable.Dispose
            If _buffer.IsNotNull Then
                NativeFunctions.RtlDestroyQueryDebugBuffer(_buffer)
            End If
        End Sub

    End Class


    Public Class Heap


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

        ' Enumerate heaps
        Public Shared Function EnumerateHeapsByProcessIds(ByVal pid() As Integer, _
                                                          ByRef buf As DebugBuffer) _
                    As Dictionary(Of String, heapInfos)

            Dim _dico As New Dictionary(Of String, heapInfos)

            If pid Is Nothing Then
                Return _dico
            End If

            For Each id As Integer In pid
                Dim _md As New Dictionary(Of String, heapInfos)
                _md = EnumerateHeapsByProcessId(id, buf)
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, heapInfos) In _md
                    _dico.Add(pair.Key, pair.Value)
                Next
            Next
            Return _dico
        End Function

        ' Enumerate modules
        Public Shared Function EnumerateHeapsByProcessId(ByVal pid As Integer, _
                                                        ByRef buf As DebugBuffer) As Dictionary(Of String, heapInfos)

            Dim retDico As New Dictionary(Of String, heapInfos)

            ' Query heaps info
            buf.Query(pid, NativeEnums.RtlQueryProcessDebugInformationFlags.Heaps Or NativeEnums.RtlQueryProcessDebugInformationFlags.HeapBlocks)

            ' Get debug information
            Dim debugInfo As NativeStructs.DebugInformation = buf.GetDebugInformation

            If debugInfo.HeapInformation.IsNotNull Then

                Dim heapInfo As New Memory.MemoryAlloc(debugInfo.HeapInformation)
                Dim heaps As NativeStructs.ProcessHeaps = heapInfo.ReadStruct(Of NativeStructs.ProcessHeaps)()

                For i As Integer = 0 To heaps.HeapsCount - 1
                    Dim heap As NativeStructs.HeapInformation = heapInfo.ReadStruct(Of NativeStructs.HeapInformation)(NativeStructs.ProcessHeaps.HeapsOffset, i)
                    Dim key As String = heap.BaseAddress.ToString
                    ' PERFISSUE ??
                    If retDico.ContainsKey(key) = False Then
                        retDico.Add(key, New heapInfos(heap))
                    End If
                Next
            End If

            Return retDico

        End Function



        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
