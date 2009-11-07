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

    Public Class [Module]


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

        ' Unload a module
        Public Shared Function UnloadModuleByAddress(ByVal address As IntPtr, ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(pid, _
                                                            Security.ProcessAccess.CreateThread)
            ' Create a remote thread a call FreeLibrary
            If hProc .IsNotNull Then
                Dim kernel32 As IntPtr = _
                        Native.Api.NativeFunctions.GetModuleHandle("kernel32.dll")
                Dim freeLibrary As IntPtr = _
                        Native.Api.NativeFunctions.GetProcAddress(kernel32, "FreeLibrary")
                Dim threadId As Integer
                Dim ret As IntPtr = _
                        Native.Api.NativeFunctions.CreateRemoteThread(hProc, _
                                                IntPtr.Zero, 0, freeLibrary, _
                                                address, 0, threadId)
                Return (ret .IsNotNull)
            Else
                Return False
            End If
        End Function

        ' Enumerate modules
        Public Shared Function EnumerateModulesByProcessIds(ByVal pid() As Integer, _
                Optional ByVal noFileInfo As Boolean = False) As Dictionary(Of String, moduleInfos)

            Dim _dico As New Dictionary(Of String, moduleInfos)

            If pid Is Nothing Then
                Return _dico
            End If

            For Each id As Integer In pid
                Dim _md As New Dictionary(Of String, moduleInfos)
                _md = EnumerateModulesByProcessId(id, noFileInfo)
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, moduleInfos) In _md
                    _dico.Add(pair.Key, pair.Value)
                Next
            Next
            Return _dico
        End Function

        ' Enumerate modules
        Public Shared Function EnumerateModulesByProcessId(ByVal pid As Integer, _
                Optional ByVal noFileInfo As Boolean = False) As Dictionary(Of String, moduleInfos)

            ' Retrieve modules of a process (uses PEB_LDR_DATA structs)
            Dim retDico As New Dictionary(Of String, moduleInfos)

            Dim hProc As IntPtr
            Dim peb As IntPtr
            Dim loaderDatePtr As IntPtr

            ' Open a reader to access memory !
            Dim reader As New ProcessMemReader(pid)
            hProc = reader.ProcessHandle

            If hProc.IsNotNull Then

                peb = reader.GetPebAddress

                ' PEB struct documented here
                ' http://undocumented.ntinternals.net/UserMode/Undocumented%20Functions/NT%20Objects/Process/PEB.html

                ' Get address of LoaderData pointer
                peb = peb.Increment(NativeStructs.Peb_LoaderDataOffset)
                loaderDatePtr = reader.ReadIntPtr(peb)

                ' PEB_LDR_DATA documented here
                ' http://msdn.microsoft.com/en-us/library/aa813708(VS.85).aspx
                Dim ldrData As New Native.Api.NativeStructs.PebLdrData
                ldrData = CType(reader.ReadStruct(Of Native.Api.NativeStructs.PebLdrData)(loaderDatePtr),  _
                            Native.Api.NativeStructs.PebLdrData)

                ' Now navigate into structure
                Dim curObj As IntPtr = ldrData.InLoadOrderModuleList.Flink
                Dim firstObj As IntPtr = curObj
                Dim dllName As String
                Dim dllPath As String
                Dim curEntry As Native.Api.NativeStructs.LdrDataTableEntry
                Dim i As Integer = 0

                Do While curObj.IsNotNull

                    If (i > 0 AndAlso curObj = firstObj) Then
                        Exit Do
                    End If

                    ' Read LoaderData entry
                    curEntry = CType(reader.ReadStruct(Of Native.Api.NativeStructs.LdrDataTableEntry)(curObj),  _
                                    Native.Api.NativeStructs.LdrDataTableEntry)

                    If (curEntry.DllBase.IsNotNull) Then

                        ' Retrive the path/name of the dll
                        dllPath = reader.ReadUnicodeString(curEntry.FullDllName)
                        If dllPath Is Nothing Then
                            dllPath = NO_INFO_RETRIEVED
                        End If
                        dllName = reader.ReadUnicodeString(curEntry.BaseDllName)
                        If dllName Is Nothing Then
                            dllName = NO_INFO_RETRIEVED
                        End If

                        ' Add to dico
                        ' Key is path-pid-baseAddress
                        Dim _key As String = dllPath.ToString & "-" & pid.ToString & "-" & curEntry.DllBase.ToString
                        If retDico.ContainsKey(_key) = False Then
                            retDico.Add(_key, New moduleInfos(curEntry, pid, dllPath, dllName, noFileInfo))
                        End If

                    End If

                    ' Next entry
                    curObj = curEntry.InLoadOrderLinks.Flink
                    i += 1
                Loop

            End If

            reader.Dispose()
            Return retDico

        End Function



        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
