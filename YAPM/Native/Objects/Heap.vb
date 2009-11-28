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
        Public Shared Function EnumerateHeapsByProcessIds(ByVal pid() As Integer) _
                    As Dictionary(Of String, heapInfos)

            Dim _dico As New Dictionary(Of String, heapInfos)

            If pid Is Nothing Then
                Return _dico
            End If

            For Each id As Integer In pid
                Dim _md As New Dictionary(Of String, heapInfos)
                _md = EnumerateHeapsByProcessId(id)
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, heapInfos) In _md
                    _dico.Add(pair.Key, pair.Value)
                Next
            Next
            Return _dico
        End Function

        ' Enumerate modules
        Public Shared Function EnumerateHeapsByProcessId(ByVal pid As Integer) As Dictionary(Of String, heapInfos)

            Dim retDico As New Dictionary(Of String, heapInfos)

            '' SLOW BUT ALWAYS WORKS (but some information about heap nodes couldn't be retrieved...)
            '' Create snapshot
            'Dim snap As IntPtr = NativeFunctions.CreateToolhelp32Snapshot(NativeEnums.Toolhelp32SnapshotFlags.HeapList, pid)
            ''Dim heap As New NativeStructs.HeapEntry32
            'Dim list As New NativeStructs.HeapList32

            'list.Size = Marshal.SizeOf(list)
            ''heap.Size = Marshal.SizeOf(heap)

            'If snap.IsNotNull Then
            '    If NativeFunctions.Heap32ListFirst(snap, list) Then
            '        Do
            '            ' Too much time to count the blocks....
            '            'NativeFunctions.Heap32First(heap, list.ProcessID, list.HeapID)
            '            retDico.Add(list.HeapID.ToString, New heapInfos(list))
            '        Loop While NativeFunctions.Heap32ListNext(snap, list)
            '    End If
            'End If



            ' FAST BUT SOMETIMES BUGGY SOLUTION
            Dim buf2 As New DebugBuffer

            ' Query heaps info
            buf2.Query(pid, NativeEnums.RtlQueryProcessDebugInformationFlags.Heaps Or NativeEnums.RtlQueryProcessDebugInformationFlags.HeapBlocks)

            ' Get debug information
            Dim debugInfo As NativeStructs.DebugInformation = buf2.GetDebugInformation

            If debugInfo.HeapInformation.IsNotNull Then

                Dim heapInfo As New Memory.MemoryAlloc(debugInfo.HeapInformation)

                Dim heaps As NativeStructs.ProcessHeaps
                Try
                    heaps = heapInfo.ReadStruct(Of NativeStructs.ProcessHeaps)()
                Catch ex As Exception
                    ' Unfortunately, System.ExecutionEngineException cannot
                    ' be catched....
                    ' ReadStruct sometimes fails and causes YAPM to crash -___-
                    Misc.ShowDebugError(ex)
                    Return retDico
                End Try

                For i As Integer = 0 To heaps.HeapsCount - 1
                    Dim heap As NativeStructs.HeapInformation = heapInfo.ReadStruct(Of NativeStructs.HeapInformation)(NativeStructs.ProcessHeaps.HeapsOffset, i)
                    Dim key As String = heap.BaseAddress.ToString
                    ' PERFISSUE ??
                    If retDico.ContainsKey(key) = False Then
                        retDico.Add(key, New heapInfos(heap))
                    End If
                Next

                ' heapInfo.Free()

            End If

            buf2.Dispose()

            Return retDico

        End Function

        ' Enumerate heap blocks
        Public Shared Function EnumerateBlocksByNodeAddress(ByVal pid As Integer, _
                                     ByVal nodeAddress As IntPtr) As Dictionary(Of String, NativeStructs.HeapBlock)

            Dim res As New Dictionary(Of String, NativeStructs.HeapBlock)

            Dim hb As NativeStructs.HeapBlock
            With hb
                .Address = IntPtr.Zero
                .Flags = 0
                .Reserved = IntPtr.Zero
                .Size = 0
            End With

            Dim buf As New DebugBuffer

            ' Query heaps info
            buf.Query(pid, NativeEnums.RtlQueryProcessDebugInformationFlags.Heaps Or NativeEnums.RtlQueryProcessDebugInformationFlags.HeapBlocks)

            ' Get debug information
            Dim debugInfo As NativeStructs.DebugInformation = buf.GetDebugInformation
            Dim heapInfo As New Memory.MemoryAlloc(debugInfo.HeapInformation)
            Dim heaps As NativeStructs.ProcessHeaps = heapInfo.ReadStruct(Of NativeStructs.ProcessHeaps)()

            ' Go through each of the heap nodes 
            For i As Integer = 0 To heaps.HeapsCount

                Dim heap As NativeStructs.HeapInformation = heapInfo.ReadStruct(Of NativeStructs.HeapInformation)(NativeStructs.ProcessHeaps.HeapsOffset, i)

                If heap.BaseAddress = nodeAddress Then

                    ' Now enumerate all blocks within this heap node...
                    With hb
                        .Address = IntPtr.Zero
                        .Flags = 0
                        .Reserved = IntPtr.Zero
                        .Size = 0
                    End With

                    If (GetFirstHeapBlock(heap, hb)) Then

                        For c As Integer = 1 To heap.BlockCount
                            ' PERFISSUE ?
                            If res.ContainsKey(hb.Address.ToString) = False Then
                                res.Add(hb.Address.ToString, hb)
                            End If

                            ' Get next block
                            Call GetNextHeapBlock(heap, hb)

                        Next
                    End If
                    Exit For
                End If
            Next

            'heapInfo.Free()

            ' Clean up the buffer
            buf.Dispose()

            Return res
        End Function





        ' ========================================
        ' Private functions
        ' ========================================

        ' This is directly converted from C++
        ' http://securityxploded.com/enumheaps.php
        Private Shared Function GetFirstHeapBlock(ByVal curHeapNode As NativeStructs.HeapInformation, _
                                ByRef hb As NativeStructs.HeapBlock) As Boolean

            Dim block As IntPtr

            With hb
                .Reserved = IntPtr.Zero
                .Address = IntPtr.Zero
                .Flags = 0
            End With

            block = curHeapNode.Blocks

            Do While (Marshal.ReadInt32(block.Increment(IntPtr.Size * 1)) And 2) = 2
                hb.Reserved = hb.Reserved.Increment(IntPtr.Size * 1)
                hb.Address = Marshal.ReadIntPtr(block.Increment(IntPtr.Size * 3)).Increment(curHeapNode.Granularity)
                block = block.Increment(IntPtr.Size * 4)
                hb.Size = Marshal.ReadInt32(block, 0)
            Loop

            ' Update the flags
            Dim flags As UShort = CUShort(Marshal.ReadInt32(block.Increment(IntPtr.Size * 1)))

            If ((flags And &HF1) <> 0 OrElse (flags And &H200) <> 0) Then
                hb.Flags = NativeEnums.HeapBlockFlag.Fixed
            ElseIf ((flags And &H20) <> 0) Then
                hb.Flags = NativeEnums.HeapBlockFlag.Moveable
            ElseIf ((flags And &H100) <> 0) Then
                hb.Flags = NativeEnums.HeapBlockFlag.Free
            End If

            Return True
        End Function

        Private Shared Function GetNextHeapBlock(ByVal curHeapNode As NativeStructs.HeapInformation, _
                            ByRef hb As NativeStructs.HeapBlock) As Boolean

            Dim block As IntPtr

            hb.Reserved = hb.Reserved.Increment(IntPtr.Size * 1)
            block = curHeapNode.Blocks

            ' Make it point to next block address entry
            block = block.Increment(hb.Reserved.ToInt32 * 4)

            If ((Marshal.ReadInt32(block.Increment(IntPtr.Size * 1)) And 2) = 2) Then

                Do While ((Marshal.ReadInt32(block.Increment(IntPtr.Size * 1)) And 2) = 2)

                    ' new address = curBlockAddress + Granularity ;
                    hb.Address = Marshal.ReadIntPtr(block.Increment(IntPtr.Size * 3)).Increment(curHeapNode.Granularity)

                    ' If all the blocks have been enumerated....exit
                    If (hb.Reserved.ToInt64 > curHeapNode.BlockCount) Then
                        Return False
                    End If

                    hb.Reserved = hb.Reserved.Increment(IntPtr.Size * 4)

                    hb.Address = block.Increment(IntPtr.Size) ' move to next block
                    hb.Size = Marshal.ReadInt32(block)

                Loop

            Else
                ' New Address = prev Address + prev block size ;
                hb.Address = (hb.Address.Increment(hb.Size))
                hb.Size = Marshal.ReadInt32(block)
            End If

            ' Update the flags...
            Dim flags As UShort = CUShort(Marshal.ReadInt32(block.Increment(&H4 * 1)))

            If ((flags And &HF1) <> 0 OrElse (flags And &H200) <> 0) Then
                hb.Flags = NativeEnums.HeapBlockFlag.Fixed
            ElseIf ((flags And &H20) <> 0) Then
                hb.Flags = NativeEnums.HeapBlockFlag.Moveable
            ElseIf ((flags And &H100) <> 0) Then
                hb.Flags = NativeEnums.HeapBlockFlag.Free
            End If

            Return True
        End Function

    End Class

End Namespace
