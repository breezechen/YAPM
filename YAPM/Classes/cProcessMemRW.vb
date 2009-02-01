' =======================================================
' Yet Another Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================


' YAPM is free software; you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation; either version 2 of the License, or
' (at your option) any later version.
'
' YAPM is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with YAPM; if not, write to the Free Software
' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA


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

    Private Enum PROTECTION_TYPE
        PAGE_EXECUTE = &H10
        PAGE_EXECUTE_READ = &H20
        PAGE_EXECUTE_READWRITE = &H40
        PAGE_EXECUTE_WRITECOPY = &H80
        PAGE_NOACCESS = &H1
        PAGE_READONLY = &H2
        PAGE_READWRITE = &H4
        PAGE_WRITECOPY = &H8
        PAGE_GUARD = &H100
        PAGE_NOCACHE = &H200
        PAGE_WRITECOMBINE = &H400
    End Enum

    ' =======================================================
    ' API
    ' =======================================================
    Private Declare Function AdjustTokenPrivileges Lib "advapi32.dll" (ByVal TokenHandle As Integer, ByVal DisableAllPrivileges As Integer, ByVal NewState As TOKEN_PRIVILEGES, ByVal BufferLength As Integer, ByVal PreviousState As TOKEN_PRIVILEGES, ByVal ReturnLength As Integer) As Integer
    Private Declare Function OpenProcessToken Lib "advapi32.dll" (ByVal ProcessHandle As Integer, ByVal DesiredAccess As Integer, ByVal TokenHandle As Integer) As Integer
    Private Declare Function LookupPrivilegeValue Lib "advapi32.dll" Alias "LookupPrivilegeValueA" (ByVal lpSystemName As String, ByVal lpName As String, ByVal lpLuid As LUID) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccessas As Integer, ByVal bInheritHandle As Integer, ByVal dwProcId As Integer) As Integer
    Private Declare Sub GetSystemInfo Lib "kernel32" (ByRef lpSystemInfo As SYSTEM_INFO)
    Private Declare Function GetCurrentProcess Lib "kernel32.dll" () As Integer
    Private Declare Function WriteProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As String, ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByVal lpBuffer As String, ByVal nSize As Integer, ByVal lpNumberOfBytesWritten As Integer) As Integer
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
    Private Structure T_RESULT
        Dim curOffset As Integer
        Dim strString As String
    End Structure
#End Region

    ' =======================================================
    ' Private attributes
    ' =======================================================
    Private _pid As Integer
    Private si As SYSTEM_INFO

    ' =======================================================
    ' Public properties
    ' =======================================================
    Public ReadOnly Property SystemInfo() As SYSTEM_INFO
        Get
            Return si
        End Get
    End Property

    ' =======================================================
    ' Public functions
    ' =======================================================
    Public Sub New(ByVal processId As Integer)
        _pid = processId
        GetSystemInfo(si)
    End Sub

    ' =======================================================
    ' Read bytes from a pid
    ' =======================================================
    Public Function ReadBytes(ByVal offset As Integer, _
        ByVal size As Integer) As String

        Dim sBuf As String
        Dim lByte As Integer
        Dim lHandle As Integer

        '/!\ Integer is enough because of the limitation of 2GB in memory

        sBuf = Space(size)
        lHandle = OpenProcess(PROCESS_ALL_ACCESS, 0, _pid)

        Call ReadProcessMemory(lHandle, offset, sBuf, size, lByte)

        Call CloseHandle(lHandle)

        Return sBuf
    End Function

    ' =======================================================
    ' Write bytes from a pid
    ' =======================================================
    Public Function WriteBytes(ByVal offset As Integer, _
        ByVal strStringToWrite As String) As Integer

        Dim lHandle As Integer
        lHandle = OpenProcess(PROCESS_ALL_ACCESS, 0, _pid)
        WriteBytes = WriteProcessMemory(lHandle, offset, strStringToWrite, Len(strStringToWrite), 0)
        Call CloseHandle(lHandle)
    End Function

    ' =======================================================
    ' Retrieve memory regions (availables for r/w)
    ' =======================================================
    Public Sub RetrieveMemRegions(ByRef regions() As MEMORY_BASIC_INFORMATION)

        Dim lHandle As Integer
        Dim lPosMem As Integer
        Dim lRet As Integer
        Dim lLenMBI As Integer
        Dim mbi As MEMORY_BASIC_INFORMATION

        ReDim regions(0)

        lHandle = OpenProcess(PROCESS_ALL_ACCESS, 0, _pid)

        lLenMBI = System.Runtime.InteropServices.Marshal.SizeOf(mbi)

        lPosMem = si.lpMinimumApplicationAddress  ' Start from shorter address

        Do While lPosMem < si.lpMaximumApplicationAddress ' While addresse is lower than max address

            mbi.RegionSize = 0

            lRet = VirtualQueryEx(lHandle, lPosMem, mbi, lLenMBI)

            If lRet = lLenMBI Then

                If (mbi.lType = MEM_PRIVATE) And (mbi.State = MEM_COMMIT) Then
                    ' Then used by process

                    If mbi.RegionSize > 0 Then
                        ' Here is a region
                        ReDim Preserve regions(UBound(regions) + 1)
                        regions(UBound(regions)) = mbi
                    End If

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
    End Sub
    Public Sub RetrieveMemRegions(ByRef lBaseAdress() As Integer, _
        ByRef lRegionSize() As Integer)

        Dim regions() As MEMORY_BASIC_INFORMATION

        ReDim regions(0)
        RetrieveMemRegions(regions)

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
    Public Sub SearchEntireStringMemory(ByVal lMinimalLength _
        As Long, ByVal bSigns As Boolean, ByVal bMaj As Boolean, ByVal bMin As _
        Boolean, ByVal bNumbers As Boolean, ByVal bAccent As Boolean, _
        ByRef lngRes() As Integer, ByRef strRes() As String, Optional ByVal PGB As ProgressBar = Nothing)

        Dim strCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim lngLen As Integer
        Dim bytAsc As Integer
        Dim strBuffer As String
        Dim curByte As Long = 0
        Dim i As Integer
        Dim tRes() As T_RESULT
        Dim lHandle As Integer
        Dim LB() As Integer
        Dim LS() As Integer


        ReDim LB(0)
        ReDim LS(0)
        Call RetrieveMemRegions(LB, LS)

        ' Calculate max size
        lngLen = 0
        For i = 1 To LS.Length
            lngLen = lngLen + LS(i)
        Next i

        If Not (PGB Is Nothing) Then
            With PGB
                .Minimum = 0
                .Value = 0
                .Maximum = LS.Length
            End With
        End If

        lHandle = GetValidHandle(_pid)
        ReDim tRes(0)

        If lHandle = INVALID_HANDLE_VALUE Then
            ReDim lngRes(0)
            ReDim strRes(0)
            Exit Sub
        End If

        ' For each region
        For x = 1 To LS.Length

            ' Get entire region
            strBuffer = ReadBytesH(lHandle, LB(x), LS(x))

            strCtemp = vbNullString

            ' Search in string
            For i = 0 To LS(x) - 1
                If (i Mod 300000) = 0 Then My.Application.DoEvents()
                bytAsc = Asc(Mid$(strBuffer, i + 1, 1))

                If IsCharConsideredInAString(bytAsc, bSigns, bMaj, bMin, bNumbers, bAccent) Then
                    strCtemp = strCtemp & Chr(bytAsc)
                Else
                    strCtemp = Trim$(strCtemp)
                    If Len(strCtemp) > lMinimalLength Then
                        ReDim Preserve tRes(tRes.Length + 1)
                        tRes(tRes.Length).curOffset = i + LB(x) - Len(strCtemp) + 1
                        tRes(tRes.Length).strString = strCtemp
                    End If
                    strCtemp = vbNullString
                End If
            Next i

            If Len(strCtemp) > lMinimalLength Then
                ReDim Preserve tRes(tRes.Length + 1)
                tRes(tRes.Length).curOffset = LS(x) + LB(x) - Len(strCtemp) + 1
                tRes(tRes.Length).strString = strCtemp
            End If

            If Not (PGB Is Nothing) Then PGB.Value = x
            My.Application.DoEvents()
        Next x


        If Not (PGB Is Nothing) Then PGB.Value = PGB.Maximum
        strBuffer = vbNullString

        Call CloseHandle(lHandle)

        'maintenant, stocke dans les arrays de sortie
        ReDim lngRes(tRes.Length)
        ReDim strRes(tRes.Length)
        For i = 1 To tRes.Length
            If (i Mod 2000) = 0 Then My.Application.DoEvents()
            lngRes(i) = tRes(i).curOffset
            strRes(i) = tRes(i).strString
        Next i

    End Sub



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

    ' =======================================================
    ' Read bytes from a handle
    ' =======================================================
    Public Shared Function ReadBytesH(ByVal lHandle As Integer, ByVal offset As Integer, _
        ByVal size As Integer) As String

        Dim sBuf As String
        Dim lByte As Integer

        '/!\ Integer is enough because of the limitation of 2GB in memory

        sBuf = Space(size)

        Call ReadProcessMemory(lHandle, offset, sBuf, size, lByte)
        Return sBuf
    End Function

    ' Get protection type as string
    Public Shared Function GetProtectionType(ByVal protec As Integer) As String
        Dim s As String = ""

        If (protec And PROTECTION_TYPE.PAGE_EXECUTE) = PROTECTION_TYPE.PAGE_EXECUTE Then
            s &= "E/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_EXECUTE_READ) = PROTECTION_TYPE.PAGE_EXECUTE_READ Then
            s &= "ERO/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_EXECUTE_READWRITE) = PROTECTION_TYPE.PAGE_EXECUTE_READWRITE Then
            s &= "ERW/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_EXECUTE_WRITECOPY) = PROTECTION_TYPE.PAGE_EXECUTE_WRITECOPY Then
            s &= "EWC/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_GUARD) = PROTECTION_TYPE.PAGE_GUARD Then
            s &= "G/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_NOACCESS) = PROTECTION_TYPE.PAGE_NOACCESS Then
            s &= "NA/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_NOCACHE) = PROTECTION_TYPE.PAGE_NOCACHE Then
            s &= "NC"
        End If
        If (protec And PROTECTION_TYPE.PAGE_READONLY) = PROTECTION_TYPE.PAGE_READONLY Then
            s &= "RO/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_READWRITE) = PROTECTION_TYPE.PAGE_READWRITE Then
            s &= "RW/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_WRITECOMBINE) = PROTECTION_TYPE.PAGE_WRITECOMBINE Then
            s &= "WCOMB/"
        End If
        If (protec And PROTECTION_TYPE.PAGE_WRITECOPY) = PROTECTION_TYPE.PAGE_WRITECOPY Then
            s &= "WC/"
        End If

        If s.Length > 0 Then
            s = s.Substring(0, s.Length - 1)
        Else
            s = NO_INFO_RETRIEVED
        End If

        Return s
    End Function

    ' Get state type as string
    Public Shared Function GetStateType(ByVal state As Integer) As String
        Select Case state
            Case MEM_COMMIT
                Return "MEM_COMMIT"
            Case MEM_RESERVE
                Return "MEM_RESERVE"
            Case MEM_FREE
                Return "MEM_FREE"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function

    ' Get type type as string
    Public Shared Function GetTypeType(ByVal type As Integer) As String
        Select Case type
            Case MEM_IMAGE
                Return "MEM_IMAGE"
            Case MEM_PRIVATE
                Return "MEM_PRIVATE"
            Case MEM_MAPPED
                Return "MEM_MAPPED"
            Case Else
                Return NO_INFO_RETRIEVED
        End Select
    End Function


    ' =======================================================
    ' Private functions
    ' =======================================================

    ' =======================================================
    ' Get a handle
    ' =======================================================
    Private Function GetValidHandle(ByVal pid As Integer) As Integer
        Return OpenProcess(PROCESS_ALL_ACCESS, 0, pid)
    End Function

    ' =======================================================
    ' Determine if a byte should be considered as a part of string
    ' (e.g. a displayable char)
    ' =======================================================
    Private Function IsCharConsideredInAString(ByVal bytChar As Integer, _
        ByVal bSigns As Boolean, ByVal bMaj As Boolean, ByVal bMin As Boolean, _
        ByVal bNumbers As Boolean, ByVal bAccent As Boolean) As Boolean

        Dim b As Boolean = False

        If bMaj Then
            b = (bytChar >= 65 And bytChar <= 90)
            If b Then Return True
        End If
        If bMin Then
            b = (bytChar >= 97 And bytChar <= 122)
            If b Then Return True
        End If
        If bNumbers Then
            b = (bytChar >= 48 And bytChar <= 57)
            If b Then Return True
        End If
        If bSigns Then
            b = (bytChar >= 33 And bytChar <= 47) Or _
            (bytChar >= 58 And bytChar <= 64) Or (bytChar >= 91 And bytChar <= 96) Or _
            (bytChar >= 123 And bytChar <= 126)
            If b Then Return True
        End If
        If bytChar = 32 Or bytChar = 39 Then    ' Space or "'"
            b = True
            If b Then Return True
        End If
        If bAccent Then
            b = (bytChar >= 192)
            If b Then Return True
        End If

        Return False
    End Function

End Class
