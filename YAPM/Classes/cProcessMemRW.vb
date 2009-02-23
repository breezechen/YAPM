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

Public Class cProcessMemRW

#Region "api"

    ' =======================================================
    ' API declaration
    ' =======================================================

    ' =======================================================
    ' Constants
    ' =======================================================
    Private Const SYNCHRONIZE As Integer = &H100000
    Private Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    Private Const PROCESS_ALL_ACCESS As Integer = STANDARD_RIGHTS_REQUIRED + SYNCHRONIZE + &HFFF
    Private Const PROCESS_VM_READ As Integer = 16
    Private Const PROCESS_VM_WRITE As Integer = &H20
    Private Const PROCESS_VM_OPERATION As Integer = &H8
    Private Const PROCESS_QUERY_INFORMATION As Integer = 1024
    Private Const PROCESS_READ_WRITE_QUERY As Integer = PROCESS_VM_READ + PROCESS_VM_WRITE + PROCESS_VM_OPERATION + PROCESS_QUERY_INFORMATION

    Private Const TOKEN_ASSIGN_PRIMARY As Integer = &H1
    Private Const TOKEN_DUPLICATE As Integer = &H2
    Private Const TOKEN_IMPERSONATE As Integer = &H4
    Private Const TOKEN_QUERY As Integer = &H8
    Private Const TOKEN_QUERY_SOURCE As Integer = &H10
    Private Const TOKEN_ADJUST_PRIVILEGES As Integer = &H20
    Private Const TOKEN_ADJUST_GROUPS As Integer = &H40
    Private Const TOKEN_ADJUST_DEFAULT As Integer = &H80
    Private Const TOKEN_ALL_ACCESS As Integer = TOKEN_ASSIGN_PRIMARY + _
                                                TOKEN_DUPLICATE + TOKEN_IMPERSONATE + _
                                                TOKEN_QUERY + TOKEN_QUERY_SOURCE + _
                                                TOKEN_ADJUST_PRIVILEGES + TOKEN_ADJUST_GROUPS + _
                                                TOKEN_ADJUST_DEFAULT

    Private Const SE_DEBUG_NAME As String = "SeDebugPrivilege"
    Private Const SE_PRIVILEGE_ENABLED As Integer = &H2

    Private Const MEM_PRIVATE As Integer = &H20000
    Private Const MEM_FREE As Integer = &H10000
    Private Const MEM_COMMIT As Integer = &H1000
    Private Const MEM_IMAGE As Integer = &H1000000
    Private Const MEM_MAPPED As Integer = &H40000
    Private Const MEM_RESERVE As Integer = &H2000

    Private Const INVALID_HANDLE_VALUE As Integer = -1
    Private Const NO_INFO_RETRIEVED As String = "N/A"
    Private Const SIZE_FOR_STRING As Integer = 5


    ' =======================================================
    ' API
    ' =======================================================
    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByVal TokenHandle As Integer) As Integer
    Private Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByVal lpLuid As LUID) As Integer
    Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccessas As Integer, ByVal bInheritHandle As Integer, ByVal dwProcId As Integer) As Integer
    Private Declare Sub GetSystemInfo Lib "kernel32" (ByRef lpSystemInfo As SYSTEM_INFO)
    Private Declare Function GetCurrentProcess Lib "kernel32.dll" () As Integer
    Private Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As String, ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As String, ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As Short(), ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As Byte(), ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As Integer(), ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function VirtualQueryEx Lib "kernel32" (ByVal hProcess As Integer, ByVal lpAddress As Integer, ByRef lpBuffer As MEMORY_BASIC_INFORMATION, ByVal dwLength As Integer) As Integer


    ' =======================================================
    ' Enums & structures
    ' =======================================================
    Private Structure LUID
        Dim LowPart As Integer
        Dim HighPart As Integer
    End Structure
    Private Structure LUID_AND_ATTRIBUTES
        Dim pLuid As LUID
        Dim Attributes As Integer
    End Structure
    Private Structure TOKEN_PRIVILEGES
        Dim PrivilegeCount As Integer
        Dim TheLuid As LUID
        Dim Attributes As Integer
    End Structure
    Public Structure MEMORY_BASIC_INFORMATION ' 28 bytes
        Dim BaseAddress As Integer
        Dim AllocationBase As Integer
        Dim AllocationProtect As Integer
        Dim RegionSize As Integer
        Dim State As Integer
        Dim Protect As Integer
        Dim lType As Integer
    End Structure
    Public Structure SYSTEM_INFO ' 36 Bytes
        Dim dwOemID As Integer
        Dim dwPageSize As Integer
        Dim lpMinimumApplicationAddress As Integer
        Dim lpMaximumApplicationAddress As Integer
        Dim dwActiveProcessorMask As Integer
        Dim dwNumberOrfProcessors As Integer
        Dim dwProcessorType As Integer
        Dim dwAllocationGranularity As Integer
        Dim wProcessorLevel As Int16
        Dim wProcessorRevision As Int16
    End Structure
    Public Structure T_RESULT
        Dim curOffset As Integer
        Dim strString As String
    End Structure
#End Region

    ' =======================================================
    ' Private attributes
    ' =======================================================
    Private _pid As Integer
    Private _handle As Integer
    Private Shared si As SYSTEM_INFO

    ' =======================================================
    ' Public properties
    ' =======================================================
    Public ReadOnly Property SystemInfo() As SYSTEM_INFO
        Get
            Return si
        End Get
    End Property
    Public ReadOnly Property Handle() As Integer
        Get
            Return _handle
        End Get
    End Property

    ' =======================================================
    ' Public functions
    ' =======================================================
    Public Sub New(ByVal processId As Integer)
        _pid = processId
        _handle = OpenProcess(PROCESS_ALL_ACCESS, 0, processId)
        GetSystemInfo(si)
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Call CloseHandle(_handle)
    End Sub

    ' =======================================================
    ' Read bytes from a pid
    ' =======================================================
    Public Function ReadBytesAI(ByVal offset As Integer, ByVal size As Integer) As Integer()

        Dim sBuf() As Integer
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Integer array -> size*4 to get bytes count
        Call ReadProcessMemory(_handle, offset, sBuf, size * 4, lByte)

        Return sBuf
    End Function
    Public Function ReadBytesAB(ByVal offset As Integer, ByVal size As Integer) As Byte()

        Dim sBuf() As Byte
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Byte array -> size*1 to get bytes count
        Call ReadProcessMemory(_handle, offset, sBuf, size, lByte)

        Return sBuf
    End Function
    Public Function ReadBytesAS(ByVal offset As Integer, ByVal size As Integer) As Short()

        Dim sBuf() As Short
        Dim _si As Integer = size
        Dim _si2 As Integer = 0
        Dim ret As Integer
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Fragment read
        Do While _si2 < _si

            Dim sBuf2() As Short
            ReDim sBuf2(size - 1)

            ' Short array -> size*2 to get bytes count
            ret = ReadProcessMemory(_handle, offset + _si2, sBuf2, size * 2, lByte)

            ' If ret = 0 and err = ERROR_PARTIAL_COPY, we have to reduce block size
            Do While ((ret = 0) And (Err.LastDllError = 299))
                size -= 2   ' Short <-> 2 bytes
                ret = ReadProcessMemory(_handle, offset + _si2, sBuf2, size * 2, lByte)
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
    Public Function Read1Byte(ByVal offset As Integer) As Byte
        Dim ret(0) As Byte
        Dim lByte As Integer
        Call ReadProcessMemory(_handle, offset, ret, 1, lByte)
        Return ret(0)
    End Function
    Public Function Read2Bytes(ByVal offset As Integer) As Short
        Dim ret(0) As Short
        Dim lByte As Integer
        Call ReadProcessMemory(_handle, offset, ret, 2, lByte)
        Return ret(0)
    End Function


    ' =======================================================
    ' Retrieve memory regions (availables for r/w)
    ' =======================================================
    Private Shared Sub RetrieveMemRegions(ByRef regions() As MEMORY_BASIC_INFORMATION, _
                                          ByVal pid As Integer, Optional ByVal onlyProcessRegions As Boolean = False)

        Dim lHandle As Integer
        Dim lPosMem As Integer
        Dim lRet As Integer
        Dim lLenMBI As Integer
        Dim mbi As MEMORY_BASIC_INFORMATION

        ReDim regions(1000)     ' Initial buffer

        lHandle = OpenProcess(PROCESS_ALL_ACCESS, 0, pid)
        lLenMBI = System.Runtime.InteropServices.Marshal.SizeOf(mbi)
        If si.lpMaximumApplicationAddress = 0 Then GetSystemInfo(si)
        lPosMem = si.lpMinimumApplicationAddress  ' Start from shorter address

        Dim _xx As Integer = 0

        Do While lPosMem < si.lpMaximumApplicationAddress ' While addresse is lower than max address

            mbi.RegionSize = 0

            lRet = VirtualQueryEx(lHandle, lPosMem, mbi, lLenMBI)

            If lRet = lLenMBI Then

                If mbi.RegionSize > 0 AndAlso _
                ((Not onlyProcessRegions) OrElse (mbi.lType = MEM_PRIVATE And _
                                                  mbi.State = MEM_COMMIT)) Then
                    ' Here is a region
                    _xx += 1
                    If _xx >= regions.Length Then
                        ReDim Preserve regions(regions.Length * 2)
                    End If
                    regions(_xx - 1) = mbi
                End If

                ' Goes on
                ' (add region size to start addresse ==> get next start addresse)
                lPosMem = mbi.BaseAddress + mbi.RegionSize

            Else
                ' Done
                Exit Do
            End If
        Loop

        Call CloseHandle(lHandle)

        ' Remove last item
        ReDim Preserve regions(_xx - 1)
    End Sub
    Public Sub RetrieveMemRegions(ByRef lBaseAdress() As Integer, _
        ByRef lRegionSize() As Integer, Optional ByVal onlyProc As Boolean = False)

        Dim regions() As MEMORY_BASIC_INFORMATION

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
        Dim lHandle As Integer
        Dim LB() As Integer
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

        lHandle = OpenProcess(PROCESS_ALL_ACCESS, 0, _pid)

        If bCasse = False Then sMatch = sMatch.ToLower

        For x = 1 To LS.Length

            ' Get current region
            strBufT = ReadBytesH(lHandle, LB(x), LS(x))

            If bCasse = False Then strBufT = strBufT.ToLower

            ' While match
            While Not (InStr(1, strBufT, sMatch, vbBinaryCompare) = 0)

                ' Found a string
                ReDim Preserve tRes(tRes.Length + 1)

                tRes(tRes.Length) = LB(x) + InStr(1, strBufT, sMatch, CompareMethod.Binary) + LS(x) - Len(strBufT) - 1

                strBufT = Right(strBufT, Len(strBufT) - InStr(1, strBufT, sMatch, CompareMethod.Binary) - Len(sMatch) + 1)
            End While

            If Not (PGB Is Nothing) Then PGB.Value = x

            Call My.Application.DoEvents()

        Next x

        If Not (PGB Is Nothing) Then PGB.Value = PGB.Maximum

        strBufT = vbNullString

        Call CloseHandle(lHandle)
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
    Public Sub SearchEntireStringMemory(ByRef lngRes() As Integer, _
        ByRef strRes() As String, Optional ByVal PGB As ProgressBar = Nothing)

        Dim strCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim lngLen As Integer
        Dim strBuffer As String
        Dim curByte As Long = 0
        Dim i As Integer
        Dim tRes() As T_RESULT
        Dim lHandle As Integer
        Dim LB() As Integer
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
                .Maximum = CInt(LS.Length + 1)
            End With
        End If

        lHandle = GetValidHandle(_pid)

        If lHandle = INVALID_HANDLE_VALUE Then
            ReDim lngRes(0)
            ReDim strRes(0)
            Exit Sub
        End If

        ' For each region
        For x = 0 To LS.Length - 1

            ' Get entire region
            strBuffer = ReadBytesH(lHandle, LB(x), LS(x))
            My.Application.DoEvents()

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
                    My.Application.DoEvents()
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

                        tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = i + LB(x) - Len(strCtemp) + 1
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

                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).curOffset = LS(x) + LB(x) - Len(strCtemp) + 1
                tRes(tRes.Length - BUF_SIZE + cArraySizeBef - 1).strString = strCtemp

            End If

        Next x


        If Not (PGB Is Nothing) Then PGB.Value = PGB.Maximum
        strBuffer = vbNullString

        Call CloseHandle(lHandle)

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

    ' =======================================================
    ' Write bytes from a handle
    ' =======================================================
    Public Shared Function WriteBytesH(ByVal handle As Integer, ByVal offset As Integer, _
        ByVal strStringToWrite As String) As Integer
        Return WriteProcessMemory(handle, offset, strStringToWrite, Len(strStringToWrite), 0)
    End Function


    ' Enumerate regions
    Public Shared Function Enumerate(ByVal pid As Integer, ByRef add() As String, _
                                     ByRef _dico As Dictionary(Of String, cProcessMemRW.MEMORY_BASIC_INFORMATION)) As Integer

        _dico.Clear()
        Dim r() As MEMORY_BASIC_INFORMATION
        ReDim r(0)

        Call RetrieveMemRegions(r, pid)

        ReDim add(r.Length - 1)

        Dim x As Integer = 0
        For Each t As MEMORY_BASIC_INFORMATION In r
            add(x) = t.BaseAddress.ToString
            _dico.Add(add(x).ToString, t)
            x += 1
        Next


    End Function

    ' =======================================================
    ' Get a handle
    ' =======================================================
    Public Shared Function GetValidHandle(ByVal pid As Integer) As Integer
        Return OpenProcess(PROCESS_ALL_ACCESS, 0, pid)
    End Function

    Public Shared Function ReadBytesH(ByVal lHandle As Integer, ByVal offset As Integer, _
    ByVal size As Integer) As String

        Dim sBuf As String
        Dim lByte As Integer

        '/!\ Integer is enough because of the limitation of 2GB in memory

        sBuf = Space(size)

        Call ReadProcessMemory(lHandle, offset, sBuf, size, lByte)
        Return sBuf
    End Function


End Class
