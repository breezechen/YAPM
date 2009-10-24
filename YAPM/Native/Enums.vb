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
#Region "Declarations used for WMI"

        <Flags()> _
        Public Enum KillMethod
            NtTerminate = &H1
            ThreadTerminate = &H2
            CreateRemoteThread = &H4
            TerminateJob = &H8
            CloseAllHandles = &H10
            CloseAllWindows = &H20
            ThreadTerminate_GetNextThread = &H40
            All = NtTerminate Or ThreadTerminate Or CreateRemoteThread Or TerminateJob Or CloseAllHandles Or CloseAllWindows Or ThreadTerminate_GetNextThread
        End Enum

#End Region

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

        Public Enum TcpTableClass As Integer
            BasicListener
            BasicConnections
            BasicAll
            OwnerPidListener
            OwnerPidConnections
            OwnerPidAll
            OwnerModuleListener
            OwnerModuleConnections
            OwnerModuleAll
        End Enum

        Public Enum UdpTableClass As Integer
            Basic
            OwnerPid
            OwnerModule
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

        Public Enum NetworkProtocol As Integer
            Tcp
            Tcp6
            Udp
            Udp6
        End Enum

#End Region

        ' OK
#Region "Declarations used for WMI"

        Public Enum WmiProcessReturnCode
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

        Public Enum WBEMSTATUS
            WBEM_NO_ERROR = 0
            WBEM_S_NO_ERROR = 0
            WBEM_S_SAME = 0
            WBEM_S_FALSE = 1
            WBEM_S_ALREADY_EXISTS = &H40001
            WBEM_S_RESET_TO_DEFAULT = &H40002
            WBEM_S_DIFFERENT = &H40003
            WBEM_S_TIMEDOUT = &H40004
            WBEM_S_NO_MORE_DATA = &H40005
            WBEM_S_OPERATION_CANCELLED = &H40006
            WBEM_S_PENDING = &H40007
            WBEM_S_DUPLICATE_OBJECTS = &H40008
            WBEM_S_ACCESS_DENIED = &H40009
            WBEM_S_PARTIAL_RESULTS = &H40010
            WBEM_S_SOURCE_NOT_AVAILABLE = &H40017
            WBEM_E_FAILED = &H80041001
            WBEM_E_NOT_FOUND = &H80041002
            WBEM_E_ACCESS_DENIED = &H80041003
            WBEM_E_PROVIDER_FAILURE = &H80041004
            WBEM_E_TYPE_MISMATCH = &H80041005
            WBEM_E_OUT_OF_MEMORY = &H80041006
            WBEM_E_INVALID_CONTEXT = &H80041007
            WBEM_E_INVALID_PARAMETER = &H80041008
            WBEM_E_NOT_AVAILABLE = &H80041009
            WBEM_E_CRITICAL_ERROR = &H8004100A
            WBEM_E_INVALID_STREAM = &H8004100B
            WBEM_E_NOT_SUPPORTED = &H8004100C
            WBEM_E_INVALID_SUPERCLASS = &H8004100D
            WBEM_E_INVALID_NAMESPACE = &H8004100E
            WBEM_E_INVALID_OBJECT = &H8004100F
            WBEM_E_INVALID_CLASS = &H80041010
            WBEM_E_PROVIDER_NOT_FOUND = &H80041011
            WBEM_E_INVALID_PROVIDER_REGISTRATION = &H80041012
            WBEM_E_PROVIDER_LOAD_FAILURE = &H80041013
            WBEM_E_INITIALIZATION_FAILURE = &H80041014
            WBEM_E_TRANSPORT_FAILURE = &H80041015
            WBEM_E_INVALID_OPERATION = &H80041016
            WBEM_E_INVALID_QUERY = &H80041017
            WBEM_E_INVALID_QUERY_TYPE = &H80041018
            WBEM_E_ALREADY_EXISTS = &H80041019
            WBEM_E_OVERRIDE_NOT_ALLOWED = &H8004101A
            WBEM_E_PROPAGATED_QUALIFIER = &H8004101B
            WBEM_E_PROPAGATED_PROPERTY = &H8004101C
            WBEM_E_UNEXPECTED = &H8004101D
            WBEM_E_ILLEGAL_OPERATION = &H8004101E
            WBEM_E_CANNOT_BE_KEY = &H8004101F
            WBEM_E_INCOMPLETE_CLASS = &H80041020
            WBEM_E_INVALID_SYNTAX = &H80041021
            WBEM_E_NONDECORATED_OBJECT = &H80041022
            WBEM_E_READ_ONLY = &H80041023
            WBEM_E_PROVIDER_NOT_CAPABLE = &H80041024
            WBEM_E_CLASS_HAS_CHILDREN = &H80041025
            WBEM_E_CLASS_HAS_INSTANCES = &H80041026
            WBEM_E_QUERY_NOT_IMPLEMENTED = &H80041027
            WBEM_E_ILLEGAL_NULL = &H80041028
            WBEM_E_INVALID_QUALIFIER_TYPE = &H80041029
            WBEM_E_INVALID_PROPERTY_TYPE = &H8004102A
            WBEM_E_VALUE_OUT_OF_RANGE = &H8004102B
            WBEM_E_CANNOT_BE_SINGLETON = &H8004102C
            WBEM_E_INVALID_CIM_TYPE = &H8004102D
            WBEM_E_INVALID_METHOD = &H8004102E
            WBEM_E_INVALID_METHOD_PARAMETERS = &H8004102F
            WBEM_E_SYSTEM_PROPERTY = &H80041030
            WBEM_E_INVALID_PROPERTY = &H80041031
            WBEM_E_CALL_CANCELLED = &H80041032
            WBEM_E_SHUTTING_DOWN = &H80041033
            WBEM_E_PROPAGATED_METHOD = &H80041034
            WBEM_E_UNSUPPORTED_PARAMETER = &H80041035
            WBEM_E_MISSING_PARAMETER_ID = &H80041036
            WBEM_E_INVALID_PARAMETER_ID = &H80041037
            WBEM_E_NONCONSECUTIVE_PARAMETER_IDS = &H80041038
            WBEM_E_PARAMETER_ID_ON_RETVAL = &H80041039
            WBEM_E_INVALID_OBJECT_PATH = &H8004103A
            WBEM_E_OUT_OF_DISK_SPACE = &H8004103B
            WBEM_E_BUFFER_TOO_SMALL = &H8004103C
            WBEM_E_UNSUPPORTED_PUT_EXTENSION = &H8004103D
            WBEM_E_UNKNOWN_OBJECT_TYPE = &H8004103E
            WBEM_E_UNKNOWN_PACKET_TYPE = &H8004103F
            WBEM_E_MARSHAL_VERSION_MISMATCH = &H80041040
            WBEM_E_MARSHAL_INVALID_SIGNATURE = &H80041041
            WBEM_E_INVALID_QUALIFIER = &H80041042
            WBEM_E_INVALID_DUPLICATE_PARAMETER = &H80041043
            WBEM_E_TOO_MUCH_DATA = &H80041044
            WBEM_E_SERVER_TOO_BUSY = &H80041045
            WBEM_E_INVALID_FLAVOR = &H80041046
            WBEM_E_CIRCULAR_REFERENCE = &H80041047
            WBEM_E_UNSUPPORTED_CLASS_UPDATE = &H80041048
            WBEM_E_CANNOT_CHANGE_KEY_INHERITANCE = &H80041049
            WBEM_E_CANNOT_CHANGE_INDEX_INHERITANCE = &H80041050
            WBEM_E_TOO_MANY_PROPERTIES = &H80041051
            WBEM_E_UPDATE_TYPE_MISMATCH = &H80041052
            WBEM_E_UPDATE_OVERRIDE_NOT_ALLOWED = &H80041053
            WBEM_E_UPDATE_PROPAGATED_METHOD = &H80041054
            WBEM_E_METHOD_NOT_IMPLEMENTED = &H80041055
            WBEM_E_METHOD_DISABLED = &H80041056
            WBEM_E_REFRESHER_BUSY = &H80041057
            WBEM_E_UNPARSABLE_QUERY = &H80041058
            WBEM_E_NOT_EVENT_CLASS = &H80041059
            WBEM_E_MISSING_GROUP_WITHIN = &H8004105A
            WBEM_E_MISSING_AGGREGATION_LIST = &H8004105B
            WBEM_E_PROPERTY_NOT_AN_OBJECT = &H8004105C
            WBEM_E_AGGREGATING_BY_OBJECT = &H8004105D
            WBEM_E_UNINTERPRETABLE_PROVIDER_QUERY = &H8004105F
            WBEM_E_BACKUP_RESTORE_WINMGMT_RUNNING = &H80041060
            WBEM_E_QUEUE_OVERFLOW = &H80041061
            WBEM_E_PRIVILEGE_NOT_HELD = &H80041062
            WBEM_E_INVALID_OPERATOR = &H80041063
            WBEM_E_LOCAL_CREDENTIALS = &H80041064
            WBEM_E_CANNOT_BE_ABSTRACT = &H80041065
            WBEM_E_AMENDED_OBJECT = &H80041066
            WBEM_E_CLIENT_TOO_SLOW = &H80041067
            WBEM_E_NULL_SECURITY_DESCRIPTOR = &H80041068
            WBEM_E_TIMED_OUT = &H80041069
            WBEM_E_INVALID_ASSOCIATION = &H8004106A
            WBEM_E_AMBIGUOUS_OPERATION = &H8004106B
            WBEM_E_QUOTA_VIOLATION = &H8004106C
            WBEM_E_RESERVED_001 = &H8004106D
            WBEM_E_RESERVED_002 = &H8004106E
            WBEM_E_UNSUPPORTED_LOCALE = &H8004106F
            WBEM_E_HANDLE_OUT_OF_DATE = &H80041070
            WBEM_E_CONNECTION_FAILED = &H80041071
            WBEM_E_INVALID_HANDLE_REQUEST = &H80041072
            WBEM_E_PROPERTY_NAME_TOO_WIDE = &H80041073
            WBEM_E_CLASS_NAME_TOO_WIDE = &H80041074
            WBEM_E_METHOD_NAME_TOO_WIDE = &H80041075
            WBEM_E_QUALIFIER_NAME_TOO_WIDE = &H80041076
            WBEM_E_RERUN_COMMAND = &H80041077
            WBEM_E_DATABASE_VER_MISMATCH = &H80041078
            WBEM_E_VETO_DELETE = &H80041079
            WBEM_E_VETO_PUT = &H8004107A
            WBEM_E_INVALID_LOCALE = &H80041080
            WBEM_E_PROVIDER_SUSPENDED = &H80041081
            WBEM_E_SYNCHRONIZATION_REQUIRED = &H80041082
            WBEM_E_NO_SCHEMA = &H80041083
            WBEM_E_PROVIDER_ALREADY_REGISTERED = &H80041084
            WBEM_E_PROVIDER_NOT_REGISTERED = &H80041085
            WBEM_E_FATAL_TRANSPORT_ERROR = &H80041086
            WBEM_E_ENCRYPTED_CONNECTION_REQUIRED = &H80041087
            WBEM_E_PROVIDER_TIMED_OUT = &H80041088
            WBEM_E_NO_KEY = &H80041089
            WBEM_E_PROVIDER_DISABLED = &H8004108A
            WBEMESS_E_REGISTRATION_TOO_BROAD = &H80042001
            WBEMESS_E_REGISTRATION_TOO_PRECISE = &H80042002
            WBEMESS_E_AUTHZ_NOT_PRIVILEGED = &H80042003
            WBEMMOF_E_EXPECTED_QUALIFIER_NAME = &H80044001
            WBEMMOF_E_EXPECTED_SEMI = &H80044002
            WBEMMOF_E_EXPECTED_OPEN_BRACE = &H80044003
            WBEMMOF_E_EXPECTED_CLOSE_BRACE = &H80044004
            WBEMMOF_E_EXPECTED_CLOSE_BRACKET = &H80044005
            WBEMMOF_E_EXPECTED_CLOSE_PAREN = &H80044006
            WBEMMOF_E_ILLEGAL_CONSTANT_VALUE = &H80044007
            WBEMMOF_E_EXPECTED_TYPE_IDENTIFIER = &H80044008
            WBEMMOF_E_EXPECTED_OPEN_PAREN = &H80044009
            WBEMMOF_E_UNRECOGNIZED_TOKEN = &H8004400A
            WBEMMOF_E_UNRECOGNIZED_TYPE = &H8004400B
            WBEMMOF_E_EXPECTED_PROPERTY_NAME = &H8004400C
            WBEMMOF_E_TYPEDEF_NOT_SUPPORTED = &H8004400D
            WBEMMOF_E_UNEXPECTED_ALIAS = &H8004400E
            WBEMMOF_E_UNEXPECTED_ARRAY_INIT = &H8004400F
            WBEMMOF_E_INVALID_AMENDMENT_SYNTAX = &H80044010
            WBEMMOF_E_INVALID_DUPLICATE_AMENDMENT = &H80044011
            WBEMMOF_E_INVALID_PRAGMA = &H80044012
            WBEMMOF_E_INVALID_NAMESPACE_SYNTAX = &H80044013
            WBEMMOF_E_EXPECTED_CLASS_NAME = &H80044014
            WBEMMOF_E_TYPE_MISMATCH = &H80044015
            WBEMMOF_E_EXPECTED_ALIAS_NAME = &H80044016
            WBEMMOF_E_INVALID_CLASS_DECLARATION = &H80044017
            WBEMMOF_E_INVALID_INSTANCE_DECLARATION = &H80044018
            WBEMMOF_E_EXPECTED_DOLLAR = &H80044019
            WBEMMOF_E_CIMTYPE_QUALIFIER = &H8004401A
            WBEMMOF_E_DUPLICATE_PROPERTY = &H8004401B
            WBEMMOF_E_INVALID_NAMESPACE_SPECIFICATION = &H8004401C
            WBEMMOF_E_OUT_OF_RANGE = &H8004401D
            WBEMMOF_E_INVALID_FILE = &H8004401E
            WBEMMOF_E_ALIASES_IN_EMBEDDED = &H8004401F
            WBEMMOF_E_NULL_ARRAY_ELEM = &H80044020
            WBEMMOF_E_DUPLICATE_QUALIFIER = &H80044021
            WBEMMOF_E_EXPECTED_FLAVOR_TYPE = &H80044022
            WBEMMOF_E_INCOMPATIBLE_FLAVOR_TYPES = &H80044023
            WBEMMOF_E_MULTIPLE_ALIASES = &H80044024
            WBEMMOF_E_INCOMPATIBLE_FLAVOR_TYPES2 = &H80044025
            WBEMMOF_E_NO_ARRAYS_RETURNED = &H80044026
            WBEMMOF_E_MUST_BE_IN_OR_OUT = &H80044027
            WBEMMOF_E_INVALID_FLAGS_SYNTAX = &H80044028
            WBEMMOF_E_EXPECTED_BRACE_OR_BAD_TYPE = &H80044029
            WBEMMOF_E_UNSUPPORTED_CIMV22_QUAL_VALUE = &H8004402A
            WBEMMOF_E_UNSUPPORTED_CIMV22_DATA_TYPE = &H8004402B
            WBEMMOF_E_INVALID_DELETEINSTANCE_SYNTAX = &H8004402C
            WBEMMOF_E_INVALID_QUALIFIER_SYNTAX = &H8004402D
            WBEMMOF_E_QUALIFIER_USED_OUTSIDE_SCOPE = &H8004402E
            WBEMMOF_E_ERROR_CREATING_TEMP_FILE = &H8004402F
            WBEMMOF_E_ERROR_INVALID_INCLUDE_FILE = &H80044030
            WBEMMOF_E_INVALID_DELETECLASS_SYNTAX = &H80044031
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

        ' OK
#Region "Declarations used for windows"

        Public Enum AsyncWindowAction As Integer
            Close
            Flash
            StopFlashing
            BringToFront
            SetAsForegroundWindow
            SetAsActiveWindow
            Minimize
            Maximize
            Show
            Hide
            SendMessage
            SetOpacity
            SetEnabled
            SetPosition
            SetCaption
        End Enum

#End Region

        ' OK
#Region "Declarations used for handles"

        Public Enum HandleObjectType
            Adapter
            AlpcPort
            Callback
            Controller
            DebugObject
            Desktop
            Device
            Directory
            Driver
            EtwRegistration
            [Event]
            EventPair
            File
            FilterCommunicationPort
            FilterConnectionPort
            IoCompletion
            Job
            Key
            KeyedEvent
            Mutant
            Process
            Profile
            Section
            Semaphore
            Session
            SymbolicLink
            Thread
            Timer
            TmEn
            TmRm
            TmTm
            TmTx
            Token
            TpWorkerFactory
            [Type]
            WindowStation
            WmiGuid
        End Enum

#End Region

        ' OK
#Region "Declarations used for system"

        Public Enum ShutdownType As Byte
            [Restart]
            [Shutdown]
            [Poweroff]
            [Sleep]
            [Hibernate]
            [Logoff]
            [Lock]
        End Enum

#End Region

        ' OK
#Region "Other"

        <Flags()> _
        Public Enum GeneralObjectType
            [Window] = &H1
            [Process] = &H2
            [Service] = &H4
            [Handle] = &H8
            [EnvironmentVariable] = &H10
            [Module] = &H20
            [JobLimit] = &H40
            [Job] = &H80
            [Log] = &H100
            [MemoryRegion] = &H200
            [NetworkConnection] = &H400
            [Privilege] = &H800
            [SearchItem] = &H1000
            [Task] = &H2000
            [Thread] = &H4000
            [Heap] = &H8000
        End Enum

#End Region

    End Class

End Namespace