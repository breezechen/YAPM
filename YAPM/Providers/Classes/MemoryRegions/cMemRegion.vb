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

Imports System.Text

Public Class cMemRegion
    Inherits cGeneralObject

    Private _memInfos As memRegionInfos
    Private Shared WithEvents _connection As cMemRegionConnection
    Private _moduleFileName As String

#Region "Properties"

    Public Shared Property Connection() As cMemRegionConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cMemRegionConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As memRegionInfos)
        _memInfos = infos
        _connection = Connection
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.MemoryRegion

        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            If infos.Type = Native.Api.NativeEnums.MemoryType.Image Then
                _moduleFileName = getModuleName(infos.BaseAddress)
            ElseIf infos.Type = Native.Api.NativeEnums.MemoryType.Mapped Then
                If infos.State = Native.Api.NativeEnums.MemoryState.Commit Then
                    _moduleFileName = getModuleName(infos.BaseAddress)
                End If
            End If
        End If
    End Sub

    ' Return the name of the mapped file
    Private Function getModuleName(ByVal ad As IntPtr) As String

        Dim sb As New StringBuilder(1024)
        Dim _h As IntPtr = Native.Objects.Process.GetProcessHandleById(Infos.ProcessId, _
                                            Native.Security.ProcessAccess.QueryInformation Or _
                                            Native.Security.ProcessAccess.VmRead)

        If _h .IsNotNull Then
            Dim leng As Integer = Native.Api.NativeFunctions.GetMappedFileName(_h, ad, sb, sb.Capacity)
            Native.Api.NativeFunctions.CloseHandle(_h)

            If leng > 0 Then
                Dim file As String = sb.ToString(0, leng)
                If file.StartsWith("\") Then
                    file = Common.Misc.DeviceDriveNameToDosDriveName(file)
                End If
                Return file
            Else
                Return NO_INFO_RETRIEVED
            End If
        Else
            Return NO_INFO_RETRIEVED
        End If
    End Function

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As memRegionInfos
        Get
            Return _memInfos
        End Get
    End Property
    Public ReadOnly Property ModuleFileName() As String
        Get
            Return _moduleFileName
        End Get
    End Property

#End Region

#Region "All actions on memory regions"

    ' Release/decommit mem region
    Private _freeMem As asyncCallbackMemRegionFree
    Public Function Release() As Integer

        If _freeMem Is Nothing Then
            _freeMem = New asyncCallbackMemRegionFree(New asyncCallbackMemRegionFree.HasFreed(AddressOf freedMemoryDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _freeMem.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionFree.poolObj(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.RegionSize, Native.Api.NativeEnums.MemoryState.Release, newAction))

    End Function
    Public Function Decommit() As Integer

        If _freeMem Is Nothing Then
            _freeMem = New asyncCallbackMemRegionFree(New asyncCallbackMemRegionFree.HasFreed(AddressOf freedMemoryDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _freeMem.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionFree.poolObj(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.RegionSize, Native.Api.NativeEnums.MemoryState.Decommit, newAction))

        AddPendingTask(newAction, t)
    End Function
    Private Sub freedMemoryDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not free memory region (" & address.ToString("x") & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Change protection type
    Private _changeProtec As asyncCallbackMemRegionChangeProtection
    Public Function ChangeProtectionType(ByVal newProtection As Native.Api.NativeEnums.MemoryProtectionType) As Integer

        If _changeProtec Is Nothing Then
            _changeProtec = New asyncCallbackMemRegionChangeProtection(New asyncCallbackMemRegionChangeProtection.HasChangedProtection(AddressOf ChangedProtectionDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _changeProtec.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionChangeProtection.poolObj(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.RegionSize, newProtection, newAction))

    End Function
    Private Sub ChangedProtectionDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change protection (" & address.ToString("x") & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Shared functions"

    Private Shared _sharedFree As asyncCallbackMemRegionFree
    Public Shared Function SharedLRFree(ByVal pid As Integer, ByVal address As IntPtr, ByVal size As IntPtr, ByVal type As Native.Api.NativeEnums.MemoryState) As Integer

        If _sharedFree Is Nothing Then
            _sharedFree = New asyncCallbackMemRegionFree(New asyncCallbackMemRegionFree.HasFreed(AddressOf freedSharedDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedFree.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionFree.poolObj(pid, address, size, type, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub freedSharedDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not free memory region (" & address.ToString("x") & ")")
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

    Private Shared _sharedProtection As asyncCallbackMemRegionChangeProtection
    Public Shared Function SharedLRChangeProtection(ByVal pid As Integer, ByVal address As IntPtr, ByVal size As IntPtr, ByVal type As Native.Api.NativeEnums.MemoryProtectionType) As Integer

        If _sharedProtection Is Nothing Then
            _sharedProtection = New asyncCallbackMemRegionChangeProtection(New asyncCallbackMemRegionChangeProtection.HasChangedProtection(AddressOf changedSharedProtectionDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedProtection.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionChangeProtection.poolObj(pid, address, size, type, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub changedSharedProtectionDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change protection (" & address.ToString("x") & ")")
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As memRegionInfos)
        _memInfos.Merge(Thr)
    End Sub

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Type"
                res = Me.Infos.Type.ToString
            Case "Protection"
                res = GetProtectionType(Me.Infos.Protection)
            Case "State"
                res = Me.Infos.State.ToString
            Case "Name"
                res = Me.Infos.Name
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
            Case "Size"
                res = getSizeString()
            Case "File"
                res = _moduleFileName
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Type As String = ""
        Static _old_Protection As String = ""
        Static _old_State As String = ""
        Static _old_Name As String = ""
        Static _old_Address As String = ""
        Static _old_Size As String = ""
        Static _old_File As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Type"
                res = Me.Infos.Type.ToString
                If res = _old_Type Then
                    hasChanged = False
                Else
                    _old_Type = res
                End If
            Case "Protection"
                res = GetProtectionType(Me.Infos.Protection)
                If res = _old_Protection Then
                    hasChanged = False
                Else
                    _old_Protection = res
                End If
            Case "State"
                res = Me.Infos.State.ToString
                If res = _old_State Then
                    hasChanged = False
                Else
                    _old_State = res
                End If
            Case "Name"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
                If res = _old_Address Then
                    hasChanged = False
                Else
                    _old_Address = res
                End If
            Case "Size"
                res = getSizeString()
                If res = _old_Size Then
                    hasChanged = False
                Else
                    _old_Size = res
                End If
            Case "File"
                res = _moduleFileName
                If res = _old_File Then
                    hasChanged = False
                Else
                    _old_File = res
                End If
        End Select

        Return hasChanged
    End Function

    ' Get size as a string
    Private Function getSizeString() As String
        Static oldSize As IntPtr = Me.Infos.RegionSize
        Static _sizeStr As String = Common.Misc.GetFormatedSize(Me.Infos.RegionSize)

        If Not (Me.Infos.RegionSize = oldSize) Then
            _sizeStr = Common.Misc.GetFormatedSize(Me.Infos.RegionSize)
            oldSize = Me.Infos.RegionSize
        End If

        Return _sizeStr

    End Function
#End Region

    ' Get protection type as string
    Friend Shared Function GetProtectionType(ByVal protec As Native.Api.NativeEnums.MemoryProtectionType) As String
        Dim s As String = ""

        If (protec And Native.Api.NativeEnums.MemoryProtectionType.Execute) = Native.Api.NativeEnums.MemoryProtectionType.Execute Then
            s &= "E/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.ExecuteRead) = Native.Api.NativeEnums.MemoryProtectionType.ExecuteRead Then
            s &= "ERO/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.ExecuteReadWrite) = Native.Api.NativeEnums.MemoryProtectionType.ExecuteReadWrite Then
            s &= "ERW/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.ExecuteWriteCopy) = Native.Api.NativeEnums.MemoryProtectionType.ExecuteWriteCopy Then
            s &= "EWC/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.Guard) = Native.Api.NativeEnums.MemoryProtectionType.Guard Then
            s &= "G/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.NoAccess) = Native.Api.NativeEnums.MemoryProtectionType.NoAccess Then
            s &= "NA/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.NoCache) = Native.Api.NativeEnums.MemoryProtectionType.NoCache Then
            s &= "NC"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.ReadOnly) = Native.Api.NativeEnums.MemoryProtectionType.ReadOnly Then
            s &= "RO/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.ReadWrite) = Native.Api.NativeEnums.MemoryProtectionType.ReadWrite Then
            s &= "RW/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.WriteCombine) = Native.Api.NativeEnums.MemoryProtectionType.WriteCombine Then
            s &= "WCOMB/"
        End If
        If (protec And Native.Api.NativeEnums.MemoryProtectionType.WriteCopy) = Native.Api.NativeEnums.MemoryProtectionType.WriteCopy Then
            s &= "WC/"
        End If

        If s.Length > 0 Then
            s = s.Substring(0, s.Length - 1)
        Else
            s = NO_INFO_RETRIEVED
        End If

        Return s
    End Function

End Class
