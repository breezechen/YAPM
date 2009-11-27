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

' This code comes directly from here :
' http://www.pinvoke.net/default.aspx/wintrust/WinVerifyTrust.html

Option Strict On

Imports System
Imports System.Runtime.InteropServices

Namespace Security.WinTrust

#Region "WinTrustData struct field enums"
    Enum WinTrustDataUIChoice As UInteger
        All = 1
        None = 2
        NoBad = 3
        NoGood = 4
    End Enum

    Enum WinTrustDataRevocationChecks As UInteger
        None = &H0
        WholeChain = &H1
    End Enum

    Enum WinTrustDataChoice As UInteger
        File = 1
        Catalog = 2
        Blob = 3
        Signer = 4
        Certificate = 5
    End Enum

    Enum WinTrustDataStateAction As UInteger
        Ignore = &H0
        Verify = &H1
        Close = &H2
        AutoCache = &H3
        AutoCacheFlush = &H4
    End Enum

    <FlagsAttribute()> _
    Enum WinTrustDataProvFlags As UInteger
        UseIe4TrustFlag = &H1
        NoIe4ChainFlag = &H2
        NoPolicyUsageFlag = &H4
        RevocationCheckNone = &H10
        RevocationCheckEndCert = &H20
        RevocationCheckChain = &H40
        RevocationCheckChainExcludeRoot = &H80
        SaferFlag = &H100
        HashOnlyFlag = &H200
        UseDefaultOsverCheck = &H400
        LifetimeSigningFlag = &H800
        CacheOnlyUrlRetrieval = &H1000
    End Enum
    ' affects CRL retrieval and AIA retrieval

    Enum WinTrustDataUIContext As UInteger
        Execute = 0
        Install = 1
    End Enum
#End Region

#Region "WinTrust structures"
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Class WinTrustFileInfo
        Private StructSize As Integer = DirectCast(Marshal.SizeOf(GetType(WinTrustFileInfo)), Integer)
        Private pszFilePath As IntPtr
        ' required, file name to be verified
        Private hFile As IntPtr = IntPtr.Zero
        ' optional, open handle to FilePath
        Private pgKnownSubject As IntPtr = IntPtr.Zero
        ' optional, subject type if it is known
        Public Sub New(ByVal _filePath As String)
            pszFilePath = Marshal.StringToCoTaskMemAuto(_filePath)
        End Sub
        Protected Overrides Sub Finalize()
            Try
                Marshal.FreeCoTaskMem(pszFilePath)
            Finally
                MyBase.Finalize()
            End Try
        End Sub
    End Class

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Class WinTrustData
        Private StructSize As Integer = DirectCast(Marshal.SizeOf(GetType(WinTrustData)), Integer)
        Private PolicyCallbackData As IntPtr = IntPtr.Zero
        Private SIPClientData As IntPtr = IntPtr.Zero
        ' required: UI choice
        Private UIChoice As WinTrustDataUIChoice = WinTrustDataUIChoice.None
        ' required: certificate revocation check options
        Private RevocationChecks As WinTrustDataRevocationChecks = WinTrustDataRevocationChecks.None
        ' required: which structure is being passed in?
        Private UnionChoice As WinTrustDataChoice = WinTrustDataChoice.File
        ' individual file
        Private FileInfoPtr As IntPtr
        Private StateAction As WinTrustDataStateAction = WinTrustDataStateAction.Ignore
        Private StateData As IntPtr = IntPtr.Zero
        Private URLReference As String = Nothing
        Private ProvFlags As WinTrustDataProvFlags = WinTrustDataProvFlags.SaferFlag
        Private UIContext As WinTrustDataUIContext = WinTrustDataUIContext.Execute

        ' constructor for silent WinTrustDataChoice.File check
        Public Sub New(ByVal _fileName As String)
            Dim wtfiData As New WinTrustFileInfo(_fileName)
            FileInfoPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(GetType(WinTrustFileInfo)))
            Marshal.StructureToPtr(wtfiData, FileInfoPtr, False)
        End Sub
        Protected Overrides Sub Finalize()
            Try
                Marshal.FreeCoTaskMem(FileInfoPtr)
            Finally
                MyBase.Finalize()
            End Try
        End Sub
    End Class
#End Region

    Enum WinVerifyTrustResult As Integer
        Trusted = 0
        ProviderUnknown = &H800B0001
        ActionUnknown = &H800B0002
        SubjectFormUnknown = &H800B0003
        SubjectNotTrusted = &H800B0004
        NoSignature = &H800B0100
        Expired = &H800B0101
        ValidityPeriodNesting = &H800B0102
        Role = &H800B0103
        PathLenConst = &H800B0104
        Critical = &H800B0105
        Purpose = &H800B0106
        IssuerChaining = &H800B0107
        Malformed = &H800B0108
        UntrustedRoot = &H800B0109
        Chaining = &H800B010A
        Revoked = &H800B010C
        UntrustedTestRoot = &H800B010D
        RevocationFailure = &H800B010E
        CNNotMatch = &H800B010F
        WrongUsage = &H800B0110
        ExplicitDistrust = &H800B0111
        UntrustedCA = &H800B0112
        SecuritySettings = &H80092026
    End Enum

    NotInheritable Class WinTrust
        Private Shared ReadOnly INVALID_HANDLE_VALUE As New IntPtr(-1)
        ' GUID of the action to perform
        Private Const WINTRUST_ACTION_GENERIC_VERIFY_V2 As String = "{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}"

        <DllImport("wintrust.dll", ExactSpelling:=True, SetLastError:=False, CharSet:=CharSet.Unicode)> _
        Private Shared Function WinVerifyTrust(<[In]()> ByVal hwnd As IntPtr, <[In]()> <MarshalAs(UnmanagedType.LPStruct)> ByVal pgActionID As Guid, <[In]()> ByVal pWVTData As WinTrustData) As WinVerifyTrustResult
        End Function

        ' call WinTrust.WinVerifyTrust() to check embedded file signature
        Public Shared Function VerifyEmbeddedSignature(ByVal fileName As String) As Boolean
            Dim wtd As New WinTrustData(fileName)
            Dim guidAction As New Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2)
            Dim result As WinVerifyTrustResult = WinVerifyTrust(INVALID_HANDLE_VALUE, guidAction, wtd)
            Dim ret As Boolean = (result = WinVerifyTrustResult.Trusted)
            Return ret
        End Function
        Public Shared Function VerifyEmbeddedSignature2(ByVal fileName As String) As WinVerifyTrustResult
            Dim wtd As New WinTrustData(fileName)
            Dim guidAction As New Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2)
            Dim result As WinVerifyTrustResult = WinVerifyTrust(INVALID_HANDLE_VALUE, guidAction, wtd)
            Dim ret As Boolean = (result = WinVerifyTrustResult.Trusted)
            Return result
        End Function
        Private Sub New()
        End Sub
    End Class
End Namespace