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

Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.Reflection


' IMPORTANT NOTE !
' Reference revision number of repository for v2.4.2 is 1180

Public Class cSnap242to250Binder
    Inherits SerializationBinder

    Public Overrides Function BindToType(ByVal assemblyName As String, _
          ByVal typeName As String) As Type

        Dim typeToDeserialize As Type = Nothing

        ' Current type/assembly
        Dim assemVer1 As String = [Assembly].GetExecutingAssembly().FullName
        Dim typeVer1 As String = GetType(cSnapshot242).FullName

        If assemblyName <> assemVer1 Or typeName <> typeVer1 Then
            ' Change the type/assembly
            assemblyName = assemblyName.Replace("2.4.2.0", "2.5.0.0")
            typeName = typeName.Replace("cSnapshot", "cSnapshot242")
        End If

        ' The following code returns the type.
        typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, _
                                         assemblyName))

        Return typeToDeserialize
    End Function

    Public Shared Function Snap242to250(ByRef old As cSnapshot242) As cSnapshot250
        Dim res As New cSnapshot250

        With res
            ' List of processes
            Dim t1 As New Dictionary(Of Integer, processInfos)
            For Each pair As System.Collections.Generic.KeyValuePair(Of String, processInfos) In old.Processes
                Try
                    t1.Add(Integer.Parse(pair.Key), pair.Value)
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next
            res.Processes = t1

            ' List of services
            res.Services = old.Services

            ' List of network connections 
            res.NetworkConnections = old.NetworkConnections

            ' List of jobs
            res.Jobs = old.Jobs

            ' List of windows 
            Dim t2 As New Dictionary(Of String, windowInfos)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, windowInfos)) In old.Windows
                Try
                    For Each pair2 As System.Collections.Generic.KeyValuePair(Of String, windowInfos) In pair.Value
                        t2.Add(pair2.Key, pair2.Value)
                    Next
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next
            ' Tasks are retrieved using old.Tasks (this property does not exist anymore)
            For Each pair As Generic.KeyValuePair(Of String, windowInfos) In old.Tasks
                Try
                    t2.Item(pair.Key).IsTask = True
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next
            res.Windows = t2

            ' List of modules (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, moduleInfos)) In old.Modules
                Try
                    res.ModulesByProcessId(pair.Key) = pair.Value
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of threads (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, threadInfos)) In old.Threads
                Try
                    res.ThreadsByProcessId(pair.Key) = pair.Value
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of privileges (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, privilegeInfos)) In old.Privileges
                Try
                    res.PrivilegesByProcessId(pair.Key) = pair.Value
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of memory regions (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, memRegionInfos)) In old.MemoryRegions
                Try
                    res.MemoryRegionsByProcessId(pair.Key) = pair.Value
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of job limits (jobName <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of String, Dictionary(Of String, jobLimitInfos)) In old.JobLimits
                Try
                    res.JobLimitsByJobName(pair.Key) = pair.Value
                    ' Set jobname property manually, as this is not present in 2.4.2
                    For Each tmp As jobLimitInfos In res.JobLimitsByJobName(pair.Key).Values
                        tmp.JobName = pair.Key
                    Next
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of heaps (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, heapInfos)) In old.Heaps
                Try
                    res.HeapsByProcessId(pair.Key) = pair.Value
                    ' Set processId property manually, as this is not present in 2.4.2
                    For Each tmp As heapInfos In res.HeapsByProcessId(pair.Key).Values
                        tmp.ProcessId = pair.Key
                    Next
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of envvariables (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, envVariableInfos)) In old.EnvironnementVariables
                Try
                    res.EnvironnementVariablesByProcessId(pair.Key) = pair.Value
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' List of handles (PID <-> Dico)
            For Each pair As System.Collections.Generic.KeyValuePair(Of Integer, Dictionary(Of String, handleInfos)) In old.Handles
                Try
                    res.HandlesByProcessId(pair.Key) = pair.Value
                Catch ex As Exception
                    ' Won't catch this
                End Try
            Next

            ' Version of file type
            res.FileVersion = old.FileVersion

            ' Date of snapshot
            res.Date = old.Date

            ' System infos
            res.SystemInformation = old.SystemInformation
        End With

        Return res
    End Function

End Class
