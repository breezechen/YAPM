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
'
'
' Some pieces of code are inspired by wj32 work (from Process Hacker) :
' - GetAccountName function (conversion from SID to username as a string)
' - GetImageFile function, especially DeviceDriveNameToDosDriveName and
'   RefreshLogicalDrives which are inspired by functions from Process Hacker

Option Strict On

Imports System.Runtime.InteropServices
Imports System.Management
Imports System.Net
Imports System.Net.Sockets


Public Class cRemoteProcess
    Inherits cProcess

    ' ========================================
    ' API declarations
    ' ========================================

    Private Declare Function GetLastError Lib "kernel32" () As Integer
    Private Const FORMAT_MESSAGE_FROM_SYSTEM As Integer = &H1000
    Private Const LANG_NEUTRAL As Integer = &H0
    Private Const SUBLANG_DEFAULT As Integer = &H1
    Private Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Integer, _
    ByVal lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Integer, _
    ByVal lpBuffer As String, ByVal nSize As Integer, ByVal Arguments As Integer) As Integer
    Public Shared Function GetError() As String
        Dim Buffer As String
        Buffer = Space$(1024)
        FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, 0, GetLastError, LANG_NEUTRAL, Buffer, Len(Buffer), 0)
        Return Trim$(Buffer)
    End Function

    ' Structure for remote connection
    Public Structure RemoteConnectionInfo
        Dim ip As IPAddress
        Dim port As Integer
        Dim conn As IPEndPoint
        Public Sub New(ByVal ipAdd As String, ByVal aport As Integer)
            ip = IPAddress.Parse(ipAdd)
            port = aport
            conn = New IPEndPoint(ip, port)
        End Sub
    End Structure


    Private Shared WithEvents sock As RemoteControl.cAsyncSocket
    Private Shared isConnected As Boolean = False
    Private Shared receivedData As RemoteControl.cSocketData
    Private Shared received As Boolean = False

    ' ========================================
    ' Private attributes
    ' ========================================
    Private Shared _connection As RemoteConnectionInfo

    Private _pid As Integer
    Private _parentPid As Integer
    Private _parentName As String
    Private _name As String
    Private _processors As Integer
    Private _commandLine As String
    Private _startTime As Date
    Private _path As String
    Private _userName As String


    ' ========================================
    ' Constructors & destructor
    ' ========================================
    Public Sub New(ByVal process As LightProcess)
        MyBase.New()
        _pid = process.pid
        _name = process.name
        _processors = cSystemInfo.GetProcessorCount
    End Sub



    ' ========================================
    ' Getter and setter
    ' ========================================   
#Region "Properties"
    Public Overrides ReadOnly Property UserObjectsCount(Optional ByVal force As Boolean = False) As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property GDIObjectsCount(Optional ByVal force As Boolean = False) As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property CommandLine() As String
        Get
            If _commandLine = vbNullString Then

            End If
            Return _commandLine
        End Get
    End Property
    Public Overrides Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
        Set(ByVal value As Integer)
            _processors = value
        End Set
    End Property
    Public Overrides ReadOnly Property Pid() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public Overrides Property AffinityMask() As Integer
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            '
        End Set
    End Property
    Public Overrides ReadOnly Property PEBAddress() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property ParentProcessId() As Integer
        Get
            If _parentPid = -1 Then

            End If
            Return _parentPid
        End Get
    End Property
    Public Overrides ReadOnly Property ParentProcessName() As String
        Get
            If _parentName = vbNullString Then

            End If
            Return _parentName
        End Get
    End Property
    Public Overrides ReadOnly Property GetIOvalues(Optional ByVal force As Boolean = False) As cProcess.PIO_COUNTERS
        Get
            Dim io As cProcess.PIO_COUNTERS

            Return io
        End Get
    End Property
    Public Overrides ReadOnly Property FileVersionInfo(Optional ByVal force As Boolean = False) As FileVersionInfo
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property Path() As String
        Get
            If _path = vbNullString Then

            End If
            Return _path
        End Get
    End Property
    Public Overrides ReadOnly Property UserName() As String
        Get
            If _userName = vbNullString Then

            End If
            Return _userName
        End Get
    End Property
    Public Overrides ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public Overrides ReadOnly Property ProcessorTime(Optional ByVal force As Boolean = False) As Date
        Get
            Return New Date(Me.ProcessorTimeLong)
        End Get
    End Property
    Public Overrides ReadOnly Property ProcessorTimeLong() As Long
        Get
            Return KernelTime.Ticks + UserTime.Ticks
        End Get
    End Property
    Public Overrides ReadOnly Property KernelTime(Optional ByVal force As Boolean = False) As Date
        Get
            Return New Date(CLng(0))
        End Get
    End Property
    Public Overrides ReadOnly Property UserTime(Optional ByVal force As Boolean = False) As Date
        Get
            Return New Date(CLng(0))
        End Get
    End Property
    Public Overrides ReadOnly Property MemoryInfos(Optional ByVal force As Boolean = False) As PROCESS_MEMORY_COUNTERS
        Get
            Dim mem As cProcess.PROCESS_MEMORY_COUNTERS

            Return mem
        End Get
    End Property
    Public Overrides ReadOnly Property Threads() As System.Diagnostics.ProcessThreadCollection
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property PriorityClass() As String
        Get
            Return Me.PriorityClassConstant.ToString
        End Get
    End Property
    Public Overrides ReadOnly Property PriorityClassInt() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property PriorityLevel() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property PriorityClassConstant() As ProcessPriorityClass
        Get

        End Get
    End Property
    Public Overrides ReadOnly Property AverageCpuUsage(Optional ByVal force As Boolean = False) As Double
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property CpuPercentageUsage(Optional ByVal force As Boolean = False) As Double
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property HandleCount() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Overrides ReadOnly Property StartTime() As Date
        Get
            If _startTime = Nothing Then

            End If
            Return _startTime
        End Get
    End Property
    Public Overrides ReadOnly Property MainModule() As System.Diagnostics.ProcessModule
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property Modules() As System.Diagnostics.ProcessModuleCollection
        Get
            Return Nothing
        End Get
    End Property
    Public Overrides ReadOnly Property MngObjProcess() As Management.ManagementObject
        Get
            Return Nothing
        End Get
    End Property
#End Region




    ' ========================================
    ' Public functions of this class
    ' ========================================

    Public Overrides Function GetInformationNumerical(ByVal infoName As String) As Double
        '
    End Function
    Public Overrides Function SuspendProcess() As Integer
        '
    End Function
    Public Overrides Function ResumeProcess() As Integer
        '
    End Function
    Public Overrides Function KillProcessTree() As Integer
        '
    End Function
    Public Overrides Function EmptyWorkingSetSize() As Integer
        '
    End Function
    Public Overrides Function GetEnvironmentVariables(ByRef variables() As String, ByRef values() As String) As Integer
        '
    End Function


    Public Overrides Sub Refresh(Optional ByRef tag As System.Management.ManagementObject = Nothing)
        Static _refrehNumber As Integer = 0
        _refrehNumber += 1   ' This is the key

        ' Get date in ms
        Dim _now As Long = Date.Now.Ticks


        MyBase.Refresh()
    End Sub

    ' Set priority
    Public Overrides Function SetProcessPriority(ByVal level As ProcessPriorityClass) As Integer

    End Function

    ' Kill a process
    Public Overrides Function Kill() As Integer

    End Function


    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal infoName As String) As String
        Dim res As String = NO_INFO_RETRIEVED
        Dim io As cProcess.PIO_COUNTERS = Me.GetIOvalues
        Select Case infoName
            Case "ParentPID"
                res = CStr(Me.ParentProcessId)
            Case "ParentName"
                res = Me.ParentProcessName
            Case "PID"
                res = CStr(Me.Pid)
            Case "UserName"
                res = Me.UserName
            Case "CpuUsage"
                '
            Case "KernelCpuTime"
                Dim ts As Date = Me.KernelTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "UserCpuTime"
                Dim ts As Date = Me.UserTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "TotalCpuTime"
                Dim ts As Date = Me.ProcessorTime
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "StartTime"
                Dim ts As Date = Me.StartTime
                res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
            Case "WorkingSet"

            Case "PeakWorkingSet"

            Case "PageFaultCount"

            Case "PagefileUsage"

            Case "PeakPagefileUsage"

            Case "QuotaPeakPagedPoolUsage"

            Case "QuotaPagedPoolUsage"

            Case "QuotaPeakNonPagedPoolUsage"

            Case "QuotaNonPagedPoolUsage"

            Case "Priority"
                res = Me.PriorityClass.ToString
            Case "Path"
                res = Me.Path
            Case "Description"
                '
            Case "Copyright"
                '
            Case "Version"
                '
            Case "Name"
                res = Me.Name
            Case "GdiObjects"
                '
            Case "UserObjects"
                '
            Case "RunTime"
                Dim ts As New Date(Date.Now.Ticks - Me.StartTime.Ticks)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "AffinityMask"
                '
            Case "AverageCpuUsage"
                '
            Case "CommandLine"
                res = Me.CommandLine
            Case "ReadOperationCount"
                res = io.ReadOperationCount.ToString
            Case "WriteOperationCount"
                res = io.WriteOperationCount.ToString
            Case "OtherOperationCount"
                res = io.OtherOperationCount.ToString
            Case "ReadTransferCount "
                res = GetFormatedSize(io.ReadTransferCount)
            Case "WriteTransferCount"
                res = GetFormatedSize(io.WriteTransferCount)
            Case "OtherTransferCount"
                res = GetFormatedSize(io.OtherTransferCount)
            Case "HandleCount"
                res = Me.HandleCount.ToString
        End Select

        Return res
    End Function


    ' ========================================
    ' Shared functions
    ' ========================================

    ' Connect to the selected server
    Public Shared Sub ConnectToServer(ByVal ip As String, ByVal port As Integer)
        sock = New RemoteControl.cAsyncSocket(Nothing)
        _connection = New RemoteConnectionInfo(ip, port)
        sock.Connect(_connection.ip, port)
    End Sub
    Public Shared Sub ConnectToServer(ByVal conn As RemoteConnectionInfo)
        sock = New RemoteControl.cAsyncSocket(Nothing)
        _connection = conn
        sock.Connect(conn.ip, conn.port)
    End Sub

    ' Disconnect from server
    Public Shared Sub DisconnectFromServer()
        If isConnected Then
            sock.Disconnect()
        End If
    End Sub

    Public Shared Function Enumerate(ByRef key() As String, ByRef _dico As Dictionary(Of String, LightProcess)) As Integer

        ReDim key(-1)
        _dico.Clear()
        If isConnected Then

            ' Request a process list
            Dim cDat As New RemoteControl.cSocketData(RemoteControl.cSocketData.DataType.Order, RemoteControl.cSocketData.OrderType.RequestProcessList)
            sock.Send(cDat)
            While Not (received)
                Threading.Thread.Sleep(10)
            End While
            received = False

            ' Ok we got informations
            ReDim key(receivedData.GetDico.Length - 1)
            Dim x As Integer = 0
            For Each proc As RemoteControl.cSocketData.LightProcess In receivedData.GetDico
                key(x) = proc.ID.ToString
                _dico.Add(key(x), New LightProcess(proc.ID, proc.name))
                x += 1
            Next
        End If

    End Function

    ' New process
    Public Shared Sub StartNewProcess(ByVal processPath As String)

    End Sub


    Private Shared Sub sock_Connected() Handles sock.Connected
        isConnected = True
    End Sub

    Private Shared Sub sock_Disconnected() Handles sock.Disconnected
        isConnected = False
    End Sub

    Private Shared Sub sock_ReceivedData(ByRef data() As Byte, ByVal length As Integer) Handles sock.ReceivedData
        receivedData = RemoteControl.cSerialization.DeserializeObject(data)
        received = True
    End Sub

    Private Shared Sub sock_SentData() Handles sock.SentData
        Dim i As Integer = 0
    End Sub
End Class
