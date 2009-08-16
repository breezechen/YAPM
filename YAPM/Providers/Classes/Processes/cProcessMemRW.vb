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

Public Class cProcessMemRW

#Region "api"

    ' =======================================================
    ' API declaration
    ' =======================================================

    Private Const SIZE_FOR_STRING As Integer = 5

    Public Structure T_RESULT
        Dim curOffset As IntPtr
        Dim strString As String
    End Structure

#End Region

    ' =======================================================
    ' Private attributes
    ' =======================================================
    Private _pid As Integer
    Private _handle As IntPtr
    Private Shared si As Native.Api.NativeStructs.SystemInfo

    ' =======================================================
    ' Public properties
    ' =======================================================
    Public ReadOnly Property SystemInfo() As Native.Api.NativeStructs.SystemInfo
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
        'ACCESSTOCHANGE
        _handle = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.All, False, processId)
        Native.Api.NativeFunctions.GetSystemInfo(si)
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Call Native.Api.NativeFunctions.CloseHandle(_handle)
    End Sub


    ' =======================================================
    ' Read bytes from a pid
    ' =======================================================
    Public Function ReadBytesAI(ByVal offset As IntPtr, ByVal size As Integer) As Integer()

        Dim sBuf() As Integer
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Integer array -> size*4 to get bytes count
        Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, size * 4, lByte)

        Return sBuf
    End Function
    Public Function ReadBytesAIntPtr(ByVal offset As IntPtr, ByVal size As Integer) As IntPtr()

        Dim sBuf() As IntPtr
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Integer array -> size*4 to get bytes count
        Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, size * 4, lByte)

        Return sBuf
    End Function
    Public Function ReadBytesAB(ByVal offset As IntPtr, ByVal size As Integer, ByRef ok As Boolean) As Byte()

        Dim sBuf() As Byte
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Byte array -> size*1 to get bytes count
        ok = Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset, sBuf, size, lByte)

        Return sBuf
    End Function
    Public Function ReadBytesAS(ByVal offset As IntPtr, ByVal size As Integer) As Short()

        Dim sBuf() As Short
        Dim _si As Integer = size
        Dim _si2 As Integer = 0
        Dim ret As Boolean
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Fragment read
        Do While _si2 < _si

            Dim sBuf2() As Short
            ReDim sBuf2(size - 1)

            ' Short array -> size*2 to get bytes count
            ret = Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset.Increment(_si2), sBuf2, size * 2, lByte)

            ' If ret = 0 and err = ERROR_PARTIAL_COPY, we have to reduce block size
            Do While (ret = False And (Err.LastDllError = 299))
                size -= 2   ' Short <-> 2 bytes
                ret = Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset.Increment(_si2), sBuf2, size * 2, lByte)
            Loop

            sBuf2.CopyTo(sBuf, CInt(_si2 / 2))

            _si2 += size
            If size = 0 Then
                ReDim sBuf(0)
                Return sBuf
            End If
            size = _si - _si2
        Loop

        Return sBuf
    End Function
    Public Function Read1Byte(ByVal offset As IntPtr) As Byte
        Dim ret(0) As Byte
        Dim lByte As Integer
        Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset, ret, 1, lByte)
        Return ret(0)
    End Function
    Public Function Read2Bytes(ByVal offset As IntPtr) As Short
        Dim ret(0) As Short
        Dim lByte As Integer
        Native.Api.NativeFunctions.ReadProcessMemory(_handle, offset, ret, 2, lByte)
        Return ret(0)
    End Function


    ' =======================================================
    ' Retrieve memory regions (availables for r/w)
    ' =======================================================
    Private Shared Sub RetrieveMemRegions(ByRef regions() As Native.Api.NativeStructs.MemoryBasicInformation, _
                                          ByVal pid As Integer, Optional ByVal onlyProcessRegions As Boolean = False)

        Dim lHandle As IntPtr
        Dim lPosMem As IntPtr
        Dim lRet As Integer
        Dim lLenMBI As Integer
        Dim mbi As Native.Api.NativeStructs.MemoryBasicInformation

        ReDim regions(1000)     ' Initial buffer

        lHandle = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.All, False, pid)   'TOCHANGE
        lLenMBI = System.Runtime.InteropServices.Marshal.SizeOf(mbi)
        If si.lpMaximumApplicationAddress.IsNull Then Native.Api.NativeFunctions.GetSystemInfo(si)
        lPosMem = si.lpMinimumApplicationAddress  ' Start from shorter address

        Dim _xx As Integer = 0

        Do While lPosMem.IsLowerThan(si.lpMaximumApplicationAddress) ' While addresse is lower than max address

            mbi.RegionSize = 0

            lRet = Native.Api.NativeFunctions.VirtualQueryEx(lHandle, lPosMem, mbi, lLenMBI)

            If lRet = lLenMBI Then

                If mbi.RegionSize > 0 AndAlso _
                ((Not onlyProcessRegions) OrElse (mbi.Type = Native.Api.NativeEnums.MemoryType.Private And _
                                                  mbi.State = Native.Api.NativeEnums.MemoryState.Commit)) Then
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

        Call Native.Api.NativeFunctions.CloseHandle(lHandle)

        ' Remove last item
        ReDim Preserve regions(_xx - 1)
    End Sub
    Public Sub RetrieveMemRegions(ByRef lBaseAdress() As IntPtr, _
        ByRef lRegionSize() As Integer, Optional ByVal onlyProc As Boolean = False)

        Dim regions() As Native.Api.NativeStructs.MemoryBasicInformation

        ReDim regions(0)
        RetrieveMemRegions(regions, Me._pid, onlyProc)

        ReDim lBaseAdress(regions.Length - 1)
        ReDim lRegionSize(regions.Length - 1)

        For x As Integer = 0 To regions.Length - 1
            lBaseAdress(x) = regions(x).BaseAddress
            lRegionSize(x) = regions(x).RegionSize
        Next

    End Sub

    ' =======================================================
    ' Search a string in memory
    ' =======================================================
    Public Sub SearchForStringMemory(ByVal sMatch As String, ByVal bCasse As Boolean, _
        ByRef tRes() As Long, Optional ByVal PGB As ProgressBar = Nothing)

        Dim x As Integer
        Dim strBufT As String
        Dim lHandle As IntPtr
        Dim LB() As IntPtr
        Dim LS() As Integer

        ReDim tRes(0)
        ReDim LB(0)
        ReDim LS(0)

        Call RetrieveMemRegions(LB, LS)

        If Not (PGB Is Nothing) Then
            With PGB
                .Minimum = 0
                .Value = 0
                .Maximum = LS.Length
            End With
        End If

        ' ACCESSCHANGE
        '64TODO
        lHandle = Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.All, False, _pid)

        If bCasse = False Then sMatch = sMatch.ToLower

        For x = 1 To LS.Length

            ' Get current region
            strBufT = ReadBytesH(lHandle, LB(x), LS(x))

            If bCasse = False Then strBufT = strBufT.ToLower

            ' While match
            While Not (InStr(1, strBufT, sMatch, vbBinaryCompare) = 0)

                ' Found a string
                ReDim Preserve tRes(tRes.Length + 1)

                tRes(tRes.Length) = LB(x).ToInt64 + InStr(1, strBufT, sMatch, CompareMethod.Binary) + LS(x) - Len(strBufT) - 1

                strBufT = Right(strBufT, Len(strBufT) - InStr(1, strBufT, sMatch, CompareMethod.Binary) - Len(sMatch) + 1)
            End While

            If Not (PGB Is Nothing) Then PGB.Value = x

            'Call Application.DoEvents()

        Next x

        If Not (PGB Is Nothing) Then PGB.Value = PGB.Maximum

        strBufT = vbNullString

        Call Native.Api.NativeFunctions.CloseHandle(lHandle)
    End Sub

    ' =======================================================
    ' Search a complete string
    ' =======================================================
    Private _stringSearchImmediateStop As Boolean
    Public WriteOnly Property StopSearch() As Boolean
        Set(ByVal value As Boolean)
            _stringSearchImmediateStop = value
        End Set
    End Property
    Public Sub SearchEntireStringMemory(ByRef lngRes() As IntPtr, _
        ByRef strRes() As String, Optional ByVal PGB As ProgressBar = Nothing)
        '64TODO
        Dim strCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim lngLen As Integer
        Dim strBuffer As String
        Dim i As Integer
        Dim tRes() As T_RESULT
        Dim lHandle As IntPtr
        Dim LB() As IntPtr
        Dim LS() As Integer
        Dim cArraySizeBef As Integer = 0

        Const BUF_SIZE As Integer = 2000     ' Size of array

        ReDim tRes(BUF_SIZE)

        ReDim LB(0)
        ReDim LS(0)
        Call RetrieveMemRegions(LB, LS, True)

        ' Calculate max size
        lngLen = 0
        For i = 0 To LS.Length - 1
            lngLen += LS(i)
        Next i

        If Not (PGB Is Nothing) Then
            With PGB
                .Minimum = 0
                .Value = 0
                .Maximum = LS.Length + 1
            End With
        End If

        lHandle = GetValidHandle(_pid)

        If lHandle = Native.Api.NativeConstants.InvalidHandleValue Then
            ReDim lngRes(0)
            ReDim strRes(0)
            Exit Sub
        End If

        ' For each region
        For x = 0 To LS.Length - 1

            ' Get entire region
            strBuffer = ReadBytesH(lHandle, LB(x), LS(x))
            'Application.DoEvents()

            strCtemp = vbNullString

            ' Search in string
            If Not (PGB Is Nothing) Then PGB.Value += 1

            For i = 0 To LS(x) - 1

                If _stringSearchImmediateStop Then
                    ' Exit
                    PGB.Value = PGB.Maximum
                    Exit Sub
                End If

                If (i Mod 1000) = 0 Then
                    'Application.DoEvents()
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

                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = LB(x).Increment(LS(x) + 1 - Len(strCtemp))
                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp

            End If

        Next x


        If Not (PGB Is Nothing) Then PGB.Value = PGB.Maximum
        strBuffer = vbNullString

        Call Native.Api.NativeFunctions.CloseHandle(lHandle)

        'maintenant, stocke dans les arrays de sortie
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


    ' =======================================================
    ' Shared functions
    ' =======================================================

    ' Enumerate regions
    Public Shared Function Enumerate(ByVal pid As Integer, ByRef add() As String, _
                                     ByRef _dico As Dictionary(Of String, Native.Api.NativeStructs.MemoryBasicInformation)) As Integer

        _dico.Clear()
        Dim r() As Native.Api.NativeStructs.MemoryBasicInformation
        ReDim r(0)

        Call RetrieveMemRegions(r, pid)

        ReDim add(r.Length - 1)

        Dim x As Integer = 0
        For Each t As Native.Api.NativeStructs.MemoryBasicInformation In r
            add(x) = t.BaseAddress.ToString
            _dico.Add(add(x).ToString, t)
            x += 1
        Next


    End Function

    ' =======================================================
    ' Get a handle
    ' =======================================================
    Public Shared Function GetValidHandle(ByVal pid As Integer) As IntPtr
        'TOCHANGE !!
        Return Native.Api.NativeFunctions.OpenProcess(Native.Security.ProcessAccess.All, False, pid)  'TOCHANGE
    End Function



    ' =======================================================
    ' Read bytes from a handle
    ' =======================================================
    Public Shared Function ReadInt32(ByVal hProc As IntPtr, ByVal offset As IntPtr) As Integer
        Dim buffer(0) As Integer
        Dim lByte As Integer
        Native.Api.NativeFunctions.ReadProcessMemory(hProc, offset, buffer, &H4, lByte)
        Return buffer(0)
    End Function

    ' Used for memory search only (because string is crappy for ReadProcMemory)
    Public Shared Function ReadBytesH(ByVal lHandle As IntPtr, ByVal offset As IntPtr, _
    ByVal size As Integer) As String

        Dim sBuf As String
        Dim lByte As Integer

        '/!\ Integer is enough because of the limitation of 2GB in memory

        sBuf = Space(size)

        Native.Api.NativeFunctions.ReadProcessMemory(lHandle, offset, sBuf, size, lByte)
        Return sBuf
    End Function


End Class
