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

Namespace Native.Api

    Public Class Enums

        ' OK
#Region "Declarations used for tokens & privileges"

        Public Enum ElevationType
            [Default] = 1
            Full = 2
            Limited = 3
        End Enum

#End Region

        ' OK
#Region "Declarations used for network"

        Public Enum TCP_TABLE_CLASS As Integer
            TCP_TABLE_BASIC_LISTENER
            TCP_TABLE_BASIC_CONNECTIONS
            TCP_TABLE_BASIC_ALL
            TCP_TABLE_OWNER_PID_LISTENER
            TCP_TABLE_OWNER_PID_CONNECTIONS
            TCP_TABLE_OWNER_PID_ALL
            TCP_TABLE_OWNER_MODULE_LISTENER
            TCP_TABLE_OWNER_MODULE_CONNECTIONS
            TCP_TABLE_OWNER_MODULE_ALL
        End Enum

        Public Enum UDP_TABLE_CLASS As Integer
            UDP_TABLE_BASIC
            UDP_TABLE_OWNER_PID
            UDP_TABLE_OWNER_MODULE
        End Enum

        Public Enum MibTcpState As Integer
            Closed = 1
            Listening
            SynSent
            SynReceived
            Established
            FinWait1
            FinWait2
            CloseWait
            Closing
            LastAck
            TimeWait
            DeleteTcb
        End Enum

        Public Enum MIB_TCP_STATE As Integer
            Closed = 1
            Listening
            SynSent
            SynReceived
            Established
            FinWait1
            FinWait2
            CloseWait
            Closing
            LastAck
            TimeWait
            DeleteTcb
        End Enum

        Public Enum NetworkProtocol As Integer
            Tcp
            Udp
        End Enum

#End Region

        ' OK
#Region "Declarations used for WMI"

        Public Enum PROCESS_RETURN_CODE_WMI
            SuccessfulCompletion = 0
            AccessDenied = 2
            InsufficientPrivilege = 3
            UnknownFailure = 8
            PathNotFound = 9
            InvalidParameter = 21
        End Enum

        Public Enum WMI_INFO_SERVICE
            AcceptPause
            AcceptStop
            CheckPoint
            DesktopInteract
            DisplayName
            ErrorControl
            ExitCode
            Name
            PathName
            ProcessId
            ServiceSpecificExitCode
            ServiceType
            Started
            StartMode
            StartName
            State
            SystemCreationClassName
            SystemName
            TagId
            WaitHint
        End Enum

        Public Enum SERVICE_RETURN_CODE_WMI
            Success = 0
            NotSupported = 1
            AccessDenied = 2
            DependentServicesRunning = 3
            InvalidServiceControl = 4
            ServiceCannotAcceptControl = 5
            ServiceNotActive = 6
            ServiceRequestTimeout = 7
            UnknownFailure = 8
            PathNotFound = 9
            ServiceAlreadyRunning = 10
            ServiceDatabaseLocked = 11
            ServiceDependencyDeleted = 12
            ServiceDependencyFailure = 13
            ServiceDisabled = 14
            ServiceLogonFailure = 15
            ServiceMarkedForDeletion = 16
            ServiceNoThread = 17
            StatusCircularDependency = 18
            StatusDuplicateName = 19
            StatusInvalidName = 20
            StatusInvalidParameter = 21
            StatusInvalidServiceAccount = 22
            StatusServiceExists = 23
            ServiceAlreadyPaused = 24
        End Enum

        Public Enum WMI_SHUTDOWN_VALUES As Integer
            [LogOff] = 0
            [Shutdown] = 1
            [Reboot] = 2
            [PowerOff] = 8
            [Force] = 4
        End Enum

        Public Enum WMI_INFO_PROCESS
            'Caption
            CommandLine
            'CreationClassName
            CreationDate
            'CSCreationClassName
            'CSName
            'Description
            ExecutablePath
            'ExecutionState
            'Handle
            HandleCount
            'InstallDate
            KernelModeTime
            MaximumWorkingSetSize
            MinimumWorkingSetSize
            'Name
            'OSCreationClassName
            'OSName
            OtherOperationCount
            OtherTransferCount
            PageFaults
            PageFileUsage
            ParentProcessId
            PeakPageFileUsage
            PeakVirtualSize
            PeakWorkingSetSize
            Priority
            PrivatePageCount
            ProcessId
            QuotaNonPagedPoolUsage
            QuotaPagedPoolUsage
            QuotaPeakNonPagedPoolUsage
            QuotaPeakPagedPoolUsage
            ReadOperationCount
            ReadTransferCount
            'SessionId
            'Status
            TerminationDate
            ThreadCount
            UserModeTime
            VirtualSize
            WindowsVersion
            WorkingSetSize
            WriteOperationCount
            WriteTransferCount
        End Enum

        Public Enum WMI_INFO_THREAD
            ElapsedTime
            ExecutionState
            Handle
            KernelModeTime
            Name
            Priority
            PriorityBase
            ProcessHandle
            StartAddress
            ThreadState
            ThreadWaitReason
            UserModeTime
        End Enum

#End Region

    End Class

End Namespace