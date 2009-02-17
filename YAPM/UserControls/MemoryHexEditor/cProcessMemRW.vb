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
    Public Function ReadBytesAB(ByVal offset As Integer, ByVal size As Integer, ByRef ok As Boolean) As Byte()

        Dim sBuf() As Byte
        Dim lByte As Integer
        ReDim sBuf(size - 1)

        ' Byte array -> size*1 to get bytes count
        ok = (ReadProcessMemory(_handle, offset, sBuf, size, lByte) > 0)

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
                        ReDim Preserve regions(regions.Length)
                        regions(regions.Length - 1) = mbi
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
    Public Sub RetrieveMemRegions(ByRef regions() As MemoryHexEditor.control.MemoryRegion)
        Dim a() As Integer = Nothing
        Dim s() As Integer = Nothing
        RetrieveMemRegions(a, s)
        ReDim regions(a.Length - 1)

        For x As Integer = 0 To regions.Length - 1
            regions(x) = New MemoryHexEditor.control.MemoryRegion(a(x), s(x))
        Next

    End Sub



    ' =======================================================
    ' Shared functions
    ' =======================================================

    ' =======================================================
    ' Write bytes from a handle
    ' =======================================================
    Public Shared Function WriteBytesH(ByVal handle As Integer, ByVal offset As Integer, _
        ByVal strStringToWrite As String) As Integer
        Return WriteProcessMemory(handle, offset, strStringToWrite, strStringToWrite.Length, 0)
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

        sBuf = New String(Convert.ToChar(32), size)

        Call ReadProcessMemory(lHandle, offset, sBuf, size, lByte)
        Return sBuf
    End Function


End Class
