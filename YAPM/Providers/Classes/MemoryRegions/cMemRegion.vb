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

        If _connection.ConnectionObj.ConnectionType = cConnection.TypeOfConnection.LocalConnection Then
            If infos.Type = API.MEMORY_TYPE.Image Then
                _moduleFileName = getModuleName(infos.BaseAddress)
            ElseIf infos.Type = API.MEMORY_TYPE.Mapped Then
                If infos.State = API.MEMORY_STATE.Commit Then
                    _moduleFileName = getModuleName(infos.BaseAddress)
                End If
            End If
        End If
    End Sub

    ' Return the name of the mapped file
    Private Function getModuleName(ByVal ad As Integer) As String

        Dim sb As New StringBuilder(1024)
        Dim _h As Integer = API.OpenProcess(API.PROCESS_RIGHTS.PROCESS_QUERY_INFORMATION Or API.PROCESS_RIGHTS.PROCESS_VM_READ, 0, Infos.ProcessId)

        If _h > 0 Then
            Dim leng As Integer = API.GetMappedFileName(_h, ad, sb, sb.Capacity)
            API.CloseHandle(_h)

            If leng > 0 Then
                Dim file As String = sb.ToString(0, leng)
                If file.StartsWith("\") Then
                    file = asyncCallbackProcEnumerate.DeviceDriveNameToDosDriveName(file)
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
    Public Overrides ReadOnly Property ItemHasChanged() As Boolean
        Get
            Static _first As Boolean = True
            If _first Then
                _first = False
                Return True
            Else
                Return _memInfos.ItemHasChanged
            End If
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

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionFree.poolObj(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.RegionSize, API.FreeType.MEM_RELEASE, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Public Function Decommit() As Integer

        If _freeMem Is Nothing Then
            _freeMem = New asyncCallbackMemRegionFree(New asyncCallbackMemRegionFree.HasFreed(AddressOf freedMemoryDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _freeMem.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionFree.poolObj(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.RegionSize, API.FreeType.MEM_DECOMMIT, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub freedMemoryDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not free memory region (" & address.ToString("x") & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Change protection type
    Private _changeProtec As asyncCallbackMemRegionChangeProtection
    Public Function ChangeProtectionType(ByVal newProtection As API.PROTECTION_TYPE) As Integer

        If _changeProtec Is Nothing Then
            _changeProtec = New asyncCallbackMemRegionChangeProtection(New asyncCallbackMemRegionChangeProtection.HasChangedProtection(AddressOf ChangedProtectionDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _changeProtec.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionChangeProtection.poolObj(Me.Infos.ProcessId, Me.Infos.BaseAddress, Me.Infos.RegionSize, newProtection, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub ChangedProtectionDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change protection (" & address.ToString("x") & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Shared functions"

    Private Shared _sharedFree As asyncCallbackMemRegionFree
    Public Shared Function SharedLRFree(ByVal pid As Integer, ByVal address As Integer, ByVal size As Integer, ByVal type As API.FreeType) As Integer

        If _sharedFree Is Nothing Then
            _sharedFree = New asyncCallbackMemRegionFree(New asyncCallbackMemRegionFree.HasFreed(AddressOf freedSharedDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedFree.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionFree.poolObj(pid, address, size, type, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub freedSharedDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not free memory region (" & address.ToString("x") & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

    Private Shared _sharedProtection As asyncCallbackMemRegionChangeProtection
    Public Shared Function SharedLRChangeProtection(ByVal pid As Integer, ByVal address As Integer, ByVal size As Integer, ByVal type As API.PROTECTION_TYPE) As Integer

        If _sharedProtection Is Nothing Then
            _sharedProtection = New asyncCallbackMemRegionChangeProtection(New asyncCallbackMemRegionChangeProtection.HasChangedProtection(AddressOf changedSharedProtectionDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedProtection.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackMemRegionChangeProtection.poolObj(pid, address, size, type, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub changedSharedProtectionDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not change protection (" & address.ToString("x") & ")")
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As memRegionInfos)
        _memInfos.Merge(Thr)
    End Sub

#Region "Get information overriden methods"

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String

        If info = "ObjectCreationDate" Then
            Return _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            Return PendingTaskCount.ToString
        End If

        Dim res As String = NO_INFO_RETRIEVED
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

    ' Get size as a string
    Private Function getSizeString() As String
        Static oldSize As Integer = Me.Infos.RegionSize
        Static _sizeStr As String = GetFormatedSize(Me.Infos.RegionSize)

        If Not (Me.Infos.RegionSize = oldSize) Then
            _sizeStr = GetFormatedSize(Me.Infos.RegionSize)
            oldSize = Me.Infos.RegionSize
        End If

        Return _sizeStr

    End Function
#End Region

    ' Get protection type as string
    Friend Shared Function GetProtectionType(ByVal protec As API.PROTECTION_TYPE) As String
        Dim s As String = ""

        If (protec And API.PROTECTION_TYPE.Execute) = API.PROTECTION_TYPE.Execute Then
            s &= "E/"
        End If
        If (protec And API.PROTECTION_TYPE.ExecuteRead) = API.PROTECTION_TYPE.ExecuteRead Then
            s &= "ERO/"
        End If
        If (protec And API.PROTECTION_TYPE.ExecuteReadWrite) = API.PROTECTION_TYPE.ExecuteReadWrite Then
            s &= "ERW/"
        End If
        If (protec And API.PROTECTION_TYPE.ExecuteWriteCopy) = API.PROTECTION_TYPE.ExecuteWriteCopy Then
            s &= "EWC/"
        End If
        If (protec And API.PROTECTION_TYPE.Guard) = API.PROTECTION_TYPE.Guard Then
            s &= "G/"
        End If
        If (protec And API.PROTECTION_TYPE.NoAccess) = API.PROTECTION_TYPE.NoAccess Then
            s &= "NA/"
        End If
        If (protec And API.PROTECTION_TYPE.NoCache) = API.PROTECTION_TYPE.NoCache Then
            s &= "NC"
        End If
        If (protec And API.PROTECTION_TYPE.[ReadOnly]) = API.PROTECTION_TYPE.[ReadOnly] Then
            s &= "RO/"
        End If
        If (protec And API.PROTECTION_TYPE.ReadWrite) = API.PROTECTION_TYPE.ReadWrite Then
            s &= "RW/"
        End If
        If (protec And API.PROTECTION_TYPE.WriteCombine) = API.PROTECTION_TYPE.WriteCombine Then
            s &= "WCOMB/"
        End If
        If (protec And API.PROTECTION_TYPE.WriteCopy) = API.PROTECTION_TYPE.WriteCopy Then
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
