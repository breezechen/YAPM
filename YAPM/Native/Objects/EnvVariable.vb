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
Imports YAPM.Native.Api

Namespace Native.Objects

    Public Class EnvVariable


        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================

        ' Return environment variables
        Public Shared Function GetEnvironmentVariablesBycProcess(ByRef process As cProcess, _
                        ByRef variables() As String, _
                        ByRef values() As String) As Integer
            Return GetEnvironmentVariables(process.Infos.PebAddress, _
                                           process.Infos.ProcessId, variables, values)
        End Function

        ' Return environment variables
        Public Shared Function GetEnvironmentVariables(ByVal peb As IntPtr, _
                                        ByVal pid As Integer, _
                                        ByRef variables() As String, _
                                        ByRef values() As String) As Integer

            ReDim variables(-1)
            ReDim values(-1)

            ' Get PEB address of process
            Dim pebAddress As IntPtr = peb
            If pebAddress.IsNull Then
                Return 0
            End If

            ' Create a processMemRW class to read in memory
            Dim cR As New ProcessRW(pid)

            If cR.Handle.IsNull Then
                Return 0              ' Couldn't open a handle
            End If


            ' Get address of Process parameter block
            Dim procParamAddress As IntPtr = cR.ReadIntPtr( _
                    pebAddress.Increment(Native.Api.NativeStructs.Peb_ProcessParametersOffset))


            ' Get environnement block address
            Dim envAddress As IntPtr = cR.ReadIntPtr(procParamAddress.Increment( _
                    Native.Api.NativeStructs.ProcParamBlock_EnvOffset))


            ' ======= Read environnement block byte per byte to calculate env. block size
            ' Block is finished by a 4 consecutive null bytes (0)
            ' Short variables because it's unicode coded (2 bytes per char)
            Dim b1 As Short = -1
            Dim b2 As Short = -1
            Dim _size As Integer = 0

            ' Read mem until 4 null char (<==> 2 null shorts)
            Do While Not (b1 = 0 And b2 = 0)
                b1 = cR.ReadInt16(envAddress.Increment(_size))
                b2 = cR.ReadInt16(envAddress.Increment(_size + 2))
                _size += 2
            Loop

            ' Now we can get all env. variables from memory
            Dim blockEnv() As Short = cR.ReadInt16Array(envAddress, _size)

            ' Parse these env. variables
            ' Env. variables are separated by 2 null bytes ( <==> 1 null short)
            Dim _envVar() As String
            Dim __var As String
            Dim xOld As Integer = 0
            ReDim _envVar(0)
            Dim y As Integer

            For x As Integer = 0 To _size \ 2

                If blockEnv(x) = 0 Then
                    ' Then it's variable end
                    ReDim Preserve _envVar(_envVar.Length)  ' Add one item to list
                    Try
                        ' Parse short array to retrieve an unicode string
                        y = x * 2
                        Dim __size As Integer = (y - xOld) \ 2

                        ' Allocate unmanaged memory
                        Dim ptr As IntPtr = Marshal.AllocHGlobal(y - xOld)

                        ' Copy from short array to unmanaged memory
                        Marshal.Copy(blockEnv, xOld \ 2, ptr, __size)

                        ' Convert to string (and copy to __var variable)
                        __var = Marshal.PtrToStringUni(ptr, __size)

                        ' Free unmanaged memory
                        Marshal.FreeHGlobal(ptr)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                        __var = ""
                    End Try

                    ' Insert variable
                    _envVar(_envVar.Length - 2) = __var

                    xOld = y + 2
                End If
            Next

            ' Remove useless last nothing item
            ReDim Preserve _envVar(_envVar.Length - 2)

            ' Separate variables and values
            ReDim variables(_envVar.Length - 2)
            ReDim values(_envVar.Length - 2)

            For x As Integer = 0 To _envVar.Length - 2
                Dim i As Integer = InStr(_envVar(x), "=", CompareMethod.Binary)
                If i > 0 Then
                    variables(x) = _envVar(x).Substring(0, i - 1)
                    values(x) = _envVar(x).Substring(i, _envVar(x).Length - i)
                Else
                    variables(x) = ""
                    values(x) = ""
                End If
            Next

            Return _envVar.Length

        End Function


        ' ========================================
        ' Other public
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
