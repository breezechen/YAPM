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

' This code comes from here :
' http://www.pinvoke.net/default.aspx/wintrust/WinVerifyTrust.html

Option Strict On

Imports System
Imports System.Runtime.InteropServices
Imports Native.Api

Namespace Security.WinTrust

    Public Class WinTrust

        ' GUID of the action to perform
        Private Shared GuidVerifyAction As Guid = New Guid(NativeConstants.WintrustActionGenericVerify2)

        ' Check file signature
        Public Shared Function VerifyEmbeddedSignature(ByVal fileName As String) As Boolean
            Dim wtd As New NativeStructs.WinTrustData(fileName)
            Dim result As NativeEnums.WinVerifyTrustResult = NativeFunctions.WinVerifyTrust(NativeConstants.InvalidHandleValue, GuidVerifyAction, wtd)
            Return (result = NativeEnums.WinVerifyTrustResult.Trusted)
        End Function
        Public Shared Function VerifyEmbeddedSignature2(ByVal fileName As String) As NativeEnums.WinVerifyTrustResult
            Dim wtd As New NativeStructs.WinTrustData(fileName)
            Return NativeFunctions.WinVerifyTrust(NativeConstants.InvalidHandleValue, GuidVerifyAction, wtd)
        End Function

    End Class

End Namespace