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

Imports System.Windows.Forms
Imports YAPM.Native.Api

Public Class ProcessRW


    Private Const SIZE_FOR_STRING As Integer = 5

    Public Structure T_RESULT
        Dim curOffset As IntPtr
        Dim strString As String
    End Structure


    ' =======================================================
    ' Private attributes
    ' =======================================================
    Private _pid As Integer
    Private _handle As IntPtr
    Private Shared si As NativeStructs.SystemInfo


    ' =======================================================
    ' Public properties
    ' =======================================================
    Public ReadOnly Property SystemInfo() As NativeStructs.SystemInfo
        Get
            Return si
        End Get
    End Property
    Public ReadOnly Property Handle() As IntPtr
        Get
            Return _handle
        End Get
    End Property

    ' =======================================================
    ' Public functions
    ' =======================================================
    Public Sub New(ByVal processId As Integer)
        _pid = processId
        _handle = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.All, False, processId)
        si = Native.Objects.SystemInfo.GetSystemInfo
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Native.Objects.General.CloseHandle(_handle)
    End Sub


    ' Read in memory
    Public Function ReadInt32Array(ByVal offset As IntPtr, _
                                   ByVal arraySize As Integer) As Integer()

        Dim sBuf() As Integer
        Dim lByte As Integer
        ReDim sBuf(arraySize - 1)

        ' Int32 array -> size*4 to get bytes count
        NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, _
                                                     arraySize * 4, lByte)

        Return sBuf
    End Function

    ' Used for memory search only (because string is crappy for ReadProcMemory)
    Public Function ReadString(ByVal offset As IntPtr, ByVal stringSize As IntPtr) As String

        Dim sBuf() As Byte
        Dim lByte As Integer
        ReDim sBuf(stringSize.ToInt32 - 1)

        NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, stringSize.ToInt32, lByte)
        Return System.Text.Encoding.ASCII.GetString(sBuf)
    End Function

    Public Function ReadIntPtrArray(ByVal offset As IntPtr, _
                                    ByVal arraySize As Integer) As IntPtr()

        Dim sBuf() As IntPtr
        Dim lByte As Integer
        ReDim sBuf(arraySize - 1)

        ' Intptr array -> size*Intptr.size to get bytes count
        NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, _
                                                     arraySize * IntPtr.Size, lByte)

        Return sBuf
    End Function

    Public Function ReadIntPtr(ByVal offset As IntPtr) As IntPtr

        Dim sBuf() As IntPtr
        Dim lByte As Integer
        ReDim sBuf(0)

        ' Intptr array -> size*Intptr.size to get bytes count
        NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, IntPtr.Size, lByte)

        Return sBuf(0)
    End Function

    Public Function ReadByteArray(ByVal offset As IntPtr, ByVal arraySize As Integer, _
                                ByRef ok As Boolean) As Byte()

        Dim sBuf() As Byte
        Dim lByte As Integer
        ReDim sBuf(arraySize - 1)

        ' Byte array -> size*1 to get bytes count
        ok = NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, _
                                                          arraySize, lByte)

        Return sBuf
    End Function

    Public Function ReadInt16Array(ByVal offset As IntPtr, ByVal arraySize As Integer) As Short()

        Dim sBuf() As Short
        Dim lByte As Integer
        ReDim sBuf(arraySize - 1)

        ' Byte array -> size*1 to get bytes count
        NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, _
                                                          arraySize, lByte)

        Return sBuf

        'Dim sBuf() As Short
        'Dim _si As Integer = arraySize
        'Dim _si2 As Integer = 0
        'Dim ret As Boolean
        'Dim lByte As Integer
        'ReDim sBuf(arraySize - 1)

        '' Fragment read
        'Do While _si2 < _si

        '    Dim sBuf2() As Short
        '    ReDim sBuf2(arraySize - 1)

        '    ' Short array -> size*2 to get bytes count
        '    ret = NativeFunctions.ReadProcessMemory(_handle, offset.Increment(_si2), sBuf2, arraySize * 2, lByte)

        '    ' If ret = 0 and err = ERROR_PARTIAL_COPY, we have to reduce block size
        '    Do While (ret = False And (Err.LastDllError = 299))
        '        arraySize -= 2   ' Short <-> 2 bytes
        '        ret = NativeFunctions.ReadProcessMemory(_handle, offset.Increment(_si2), sBuf2, arraySize * 2, lByte)
        '    Loop

        '    sBuf2.CopyTo(sBuf, CInt(_si2 / 2))

        '    _si2 += arraySize
        '    If arraySize = 0 Then
        '        ReDim sBuf(0)
        '        Return sBuf
        '    End If
        '    arraySize = _si - _si2
        'Loop

        'Return sBuf
    End Function

    Public Function ReadByte(ByVal offset As IntPtr) As Byte
        Dim ret(0) As Byte
        Dim lByte As Integer
        ' 1 byte
        NativeFunctions.ReadProcessMemory(_handle, offset, ret, 1, lByte)
        Return ret(0)
    End Function

    Public Function ReadInt16(ByVal offset As IntPtr) As Short
        Dim ret(0) As Short
        Dim lByte As Integer
        ' A short = 2 bytes
        NativeFunctions.ReadProcessMemory(_handle, offset, ret, 2, lByte)
        Return ret(0)
    End Function

    ' Retrieve memory regions (availables for r/w)
    Public Sub RetrieveMemRegions(ByRef regions() As NativeStructs.MemoryBasicInformation, _
                ByVal pid As Integer, Optional ByVal onlyProcessRegions As Boolean = False)

        Dim lPosMem As IntPtr
        Dim lRet As Integer
        Dim lLenMBI As Integer
        Dim mbi As NativeStructs.MemoryBasicInformation

        ReDim regions(1000)     ' Initial buffer

        lLenMBI = System.Runtime.InteropServices.Marshal.SizeOf(mbi)
        If si.lpMaximumApplicationAddress.IsNull Then NativeFunctions.GetSystemInfo(si)
        lPosMem = si.lpMinimumApplicationAddress  ' Start from shorter address

        Dim _xx As Integer = 0

        Do While lPosMem.IsLowerThan(si.lpMaximumApplicationAddress) ' While addresse is lower than max address

            mbi.RegionSize = IntPtr.Zero

            lRet = NativeFunctions.VirtualQueryEx(_handle, lPosMem, mbi, lLenMBI)

            If lRet = lLenMBI Then

                If mbi.RegionSize.IsGreaterThan(0) AndAlso _
                ((Not onlyProcessRegions) OrElse (mbi.Type = NativeEnums.MemoryType.Private And _
                                                  mbi.State = NativeEnums.MemoryState.Commit)) Then
                    ' Here is a region
                    _xx += 1
                    If _xx >= regions.Length Then
                        ReDim Preserve regions(regions.Length * 2)
                    End If
                    regions(_xx - 1) = mbi
                End If

                ' Goes on
                ' (add region size to start addresse ==> get next start addresse)
                lPosMem = mbi.BaseAddress.Increment(mbi.RegionSize)

            Else
                ' Done
                Exit Do
            End If
        Loop

        ' Remove last item
        ReDim Preserve regions(_xx - 1)
    End Sub

    Public Sub RetrieveMemRegions(ByRef lBaseAdress() As IntPtr, _
        ByRef lRegionSize() As IntPtr, Optional ByVal onlyProc As Boolean = False)

        Dim regions() As NativeStructs.MemoryBasicInformation

        ReDim regions(0)
        RetrieveMemRegions(regions, Me._pid, onlyProc)

        ReDim lBaseAdress(regions.Length - 1)
        ReDim lRegionSize(regions.Length - 1)

        For x As Integer = 0 To regions.Length - 1
            lBaseAdress(x) = regions(x).BaseAddress
            lRegionSize(x) = regions(x).RegionSize
        Next

    End Sub

    ' Search a string in memory
    Public Sub SearchForStringMemory(ByVal sMatch As String, ByVal bCasse As Boolean, _
        ByRef tRes() As Long, Optional ByVal PGB As ProgressBar = Nothing)

        Dim x As Integer
        Dim strBufT As String
        Dim LB() As IntPtr
        Dim LS() As IntPtr

        ReDim tRes(0)
        ReDim LB(0)
        ReDim LS(0)

        ' Get memory regions
        RetrieveMemRegions(LB, LS)

        If Not (PGB Is Nothing) Then
            With PGB
                .Minimum = 0
                .Value = 0
                .Maximum = LS.Length
            End With
        End If

        If bCasse = False Then
            sMatch = sMatch.ToLower
        End If

        For x = 1 To LS.Length

            ' Get current region into one string
            ' Not a good idea -> TOCHANGE
            strBufT = ReadString(LB(x), LS(x))

            If bCasse = False Then
                strBufT = strBufT.ToLower
            End If

            ' While match
            While Not (InStr(1, strBufT, sMatch, vbBinaryCompare) = 0)

                ' Found a string
                ReDim Preserve tRes(tRes.Length + 1)

                tRes(tRes.Length) = LB(x).ToInt64 + InStr(1, strBufT, sMatch, CompareMethod.Binary) + LS(x).ToInt64 - Len(strBufT) - 1

                strBufT = Right(strBufT, Len(strBufT) - InStr(1, strBufT, sMatch, CompareMethod.Binary) - Len(sMatch) + 1)
            End While

            If Not (PGB Is Nothing) Then
                PGB.Value = x
            End If

        Next x

        If Not (PGB Is Nothing) Then
            PGB.Value = PGB.Maximum
        End If

        strBufT = vbNullString

    End Sub


    ' Search a complete string
    Private _stringSearchImmediateStop As Boolean
    Public WriteOnly Property StopSearch() As Boolean
        Set(ByVal value As Boolean)
            _stringSearchImmediateStop = value
        End Set
    End Property
    Public Sub SearchEntireStringMemory(ByRef lngRes() As IntPtr, _
            ByRef strRes() As String, Optional ByVal PGB As ProgressBar = Nothing)

        Dim strCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim lngLen As IntPtr
        Dim strBuffer As String
        Dim i As Integer
        Dim tRes() As T_RESULT
        Dim LB() As IntPtr
        Dim LS() As IntPtr
        Dim cArraySizeBef As Integer = 0

        Const BUF_SIZE As Integer = 2000     ' Size of array

        ReDim tRes(BUF_SIZE)

        ReDim LB(0)
        ReDim LS(0)

        ' Get memory regions
        RetrieveMemRegions(LB, LS, True)

        ' Calculate max size
        lngLen = IntPtr.Zero
        For i = 0 To LS.Length - 1
            lngLen.Increment(LS(i))
        Next i

        If Not (PGB Is Nothing) Then
            With PGB
                .Minimum = 0
                .Value = 0
                .Maximum = LS.Length + 1
            End With
        End If


        ' For each region
        For x = 0 To LS.Length - 1

            ' Get entire region
            strBuffer = ReadString(LB(x), LS(x))
            strCtemp = vbNullString

            ' Search in string
            If Not (PGB Is Nothing) Then
                PGB.Value += 1
            End If

            For i = 0 To LS(x).ToInt32 - 1

                If _stringSearchImmediateStop Then
                    ' Exit
                    PGB.Value = PGB.Maximum
                    Exit Sub
                End If

                If isLetter(strBuffer(i)) Then
                    strCtemp &= strBuffer.Chars(i)
                Else
                    'strCtemp = Trim$(strCtemp)
                    If Len(strCtemp) > SIZE_FOR_STRING Then

                        ' Resize only every BUF times
                        If cArraySizeBef = BUF_SIZE Then
                            cArraySizeBef = 0
                            ReDim Preserve tRes(tRes.Length + BUF_SIZE)
                        End If

                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = New IntPtr(i + LB(x).ToInt64 - Len(strCtemp) + 1)
                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp
                        cArraySizeBef += 1

                    End If
                    strCtemp = vbNullString
                End If
            Next i

            If Len(strCtemp) > SIZE_FOR_STRING Then
                ' Resize only every BUF times
                If cArraySizeBef = BUF_SIZE Then
                    cArraySizeBef = 0
                    ReDim Preserve tRes(tRes.Length + BUF_SIZE)
                End If

                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = LB(x).Increment(LS(x).ToInt32 + 1 - Len(strCtemp))
                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp

            End If

        Next x


        If Not (PGB Is Nothing) Then
            PGB.Value = PGB.Maximum
        End If
        strBuffer = vbNullString

        ReDim lngRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1)
        ReDim strRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1)
        For i = 0 To tRes.Length - BUF_SIZE + cArraySizeBef - 1
            lngRes(i) = tRes(i).curOffset
            strRes(i) = tRes(i).strString
        Next i

    End Sub

    Private Function isLetter(ByVal c As Char) As Boolean
        Dim i As Integer = Asc(c)
        ' A-Z [/]_^' space a-z {|}
        Return ((i >= 65 And i <= 125) OrElse (i >= 45 And i <= 57) OrElse i = 32)
    End Function


End Class
