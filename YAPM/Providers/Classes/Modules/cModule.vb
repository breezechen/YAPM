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

Public Class cModule
    Inherits cGeneralObject

    Private Const NO_INFO_RETRIEVED As String = "N/A"

    Private _moduleInfos As moduleInfos
    Private Shared WithEvents _connection As cModuleConnection

#Region "Properties"

    Public Shared Property Connection() As cModuleConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As cModuleConnection)
            _connection = value
        End Set
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As moduleInfos)
        _moduleInfos = infos
        _connection = Connection
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As moduleInfos
        Get
            Return _moduleInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As moduleInfos)
        _moduleInfos.Merge(Thr)
    End Sub

#Region "All actions on modules (unload)"

    ' Unload module
    Private _closeM As asyncCallbackModuleUnload
    Public Function UnloadModule() As Integer

        If _closeM Is Nothing Then
            _closeM = New asyncCallbackModuleUnload(New asyncCallbackModuleUnload.HasUnloadedModule(AddressOf unloadModuleDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _closeM.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackModuleUnload.poolObj(Me.Infos.ProcessId, Me.Infos.Name, Me.Infos.BaseAddress, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Sub unloadModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not unload module " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub


#End Region

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
            Case "EntryPoint"
                res = Me.Infos.EntryPoint.ToString
            Case "Size"
                res = Me.Infos.Size.ToString
            Case "Name"
                res = Me.Infos.Name
            Case "Version"
                If Me.Infos.Version Is Nothing Then
                    If Me.Infos.FileInfo IsNot Nothing Then _
                    res = Me.Infos.FileInfo.FileVersion
                Else
                    res = Me.Infos.Version
                End If
            Case "Description"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileDescription
            Case "CompanyName"
                If Me.Infos.Manufacturer Is Nothing Then
                    If Me.Infos.FileInfo IsNot Nothing Then _
                    res = Me.Infos.FileInfo.CompanyName
                Else
                    res = Me.Infos.Manufacturer
                End If
            Case "Path"
                res = Me.Infos.Path
            Case "Address"
                res = "0x" & Me.Infos.BaseAddress.ToString("x")
            Case "Comments"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.Comments
            Case "FileBuildPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileBuildPart.ToString
            Case "FileMajorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileMajorPart.ToString
            Case "FileMinorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FileMinorPart.ToString
            Case "FilePrivatePart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.FilePrivatePart.ToString
            Case "InternalName"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.InternalName
            Case "IsDebug"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsDebug.ToString
            Case "IsPatched"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPatched.ToString
            Case "IsPreRelease"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPreRelease.ToString
            Case "IsPrivateBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsPrivateBuild.ToString
            Case "IsSpecialBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.IsSpecialBuild.ToString
            Case "Language"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.Language
            Case "LegalCopyright"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.LegalCopyright
            Case "LegalTrademarks"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.LegalTrademarks
            Case "OriginalFilename"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.OriginalFilename
            Case "PrivateBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.PrivateBuild
            Case "ProductBuildPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductBuildPart.ToString
            Case "ProductMajorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductMajorPart.ToString
            Case "ProductMinorPart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductMinorPart.ToString
            Case "ProductName"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductName
            Case "ProductPrivatePart"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductPrivatePart.ToString
            Case "ProductVersion"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.ProductVersion
            Case "SpecialBuild"
                If Me.Infos.FileInfo IsNot Nothing Then _
                res = Me.Infos.FileInfo.SpecialBuild
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
        End Select
 
        Return res
    End Function


#End Region

#Region "Shared function"

    Private Shared _sharedcloseM As asyncCallbackModuleUnload
    Public Shared Function SharedLRUnloadModule(ByVal pid As Integer, ByVal name As String, ByVal baseAddress As Integer) As Integer

        If _sharedcloseM Is Nothing Then
            _sharedcloseM = New asyncCallbackModuleUnload(New asyncCallbackModuleUnload.HasUnloadedModule(AddressOf unloadsharedModuleDone), _connection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedcloseM.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New  _
            asyncCallbackModuleUnload.poolObj(pid, name, baseAddress, newAction))

        AddPendingTask2(newAction, t)
    End Function
    Private Shared Sub unloadsharedModuleDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            MsgBox("Error : " & msg, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, _
                   "Could not unload module " & name)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Shared functions (local)"

    ' Return opened modules
    Public Shared Function CurrentLocalModules(ByVal pid As Integer) As Dictionary(Of String, cModule)
        Dim _d As Dictionary(Of String, moduleInfos) = asyncCallbackModuleEnumerate.GetModules(pid)
        Dim _res As New Dictionary(Of String, cModule)

        For Each pair As System.Collections.Generic.KeyValuePair(Of String, moduleInfos) In _d
            _res.Add(pair.Key, New cModule(pair.Value))
        Next

        Return _res
    End Function

#End Region

End Class
