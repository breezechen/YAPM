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

    Public Class NativeConstants

#Region "Declarations used for processes"

        Public Const DUPLICATE_SAME_ACCESS As Integer = &H2

        Public Const STILL_ACTIVE As Integer = 259

        Public Const PROCESS_SET_INFORMATION As Integer = &H200
        Public Const PROCESS_SUSPEND_RESUME As Integer = &H800
        Public Const PROCESS_QUERY_INFORMATION As Integer = &H400
        Public Const PROCESS_TERMINATE As Integer = &H1
        Public Const PROCESS_SET_QUOTA As Integer = &H100
        Public Const PROCESS_CREATE_THREAD As Integer = &H2
        Public Const PROCESS_VM_OPERATION As Integer = &H8
        Public Const PROCESS_VM_READ As Integer = &H10
        Public Const PROCESS_VM_WRITE As Integer = &H20

#End Region

#Region "Declarations used for tokens & privileges"

        Public Const SE_PRIVILEGE_ENABLED As Integer = &H2
        Public Const SE_PRIVILEGE_ENABLED_BY_DEFAULT As Integer = &H1
        Public Const SE_PRIVILEGE_DISBALED As Integer = &H0
        Public Const SE_PRIVILEGE_REMOVED As Integer = &H4

#End Region

#Region "Declarations used for files"

        Public Const FILE_MAP_READ As Integer = SECTION_MAP_READ
        Public Const FILE_MAP_WRITE As Integer = SECTION_MAP_WRITE
        Public Const PAGE_READWRITE As Integer = &H4
        Public Const SECTION_MAP_READ As Integer = &H4
        Public Const SECTION_MAP_WRITE As Integer = &H2

        Public Const SEE_MASK_INVOKEIDLIST As Integer = &HC
        Public Const SEE_MASK_NOCLOSEPROCESS As Integer = &H40
        Public Const SEE_MASK_FLAG_NO_UI As Integer = &H400

        Public Const SHGFI_ICON As Integer = &H100
        Public Const SHGFI_SMALLICON As Integer = &H1
        Public Const SHGFI_LARGEICON As Integer = &H0

#End Region

        ' OK
#Region "Declarations used for windows (not Windows :-p)"

        Public Const NULL_BRUSH As Integer = 5 ' Stock Object

        Public Const GWL_HWNDPARENT As Integer = -8
        Public Const GW_CHILD As Integer = 5
        Public Const GW_HWNDNEXT As Integer = 2
        Public Const WM_GETICON As Integer = &H7F
        Public Const GCL_HICON As Integer = -14
        Public Const GCL_HICONSM As Integer = -34
        Public Const WM_CLOSE As Integer = &H10
        Public Const SW_HIDE As Integer = 0
        Public Const SW_SHOWNORMAL As Integer = 1
        Public Const SW_SHOWMINIMIZED As Integer = 2
        Public Const SW_SHOWMAXIMIZED As Integer = 3
        Public Const SW_MAXIMIZE As Integer = 3
        Public Const SW_SHOWNOACTIVATE As Integer = 4
        Public Const SW_SHOW As Integer = 5
        Public Const SW_MINIMIZE As Integer = 6
        Public Const SW_SHOWMINNOACTIVE As Integer = 7
        Public Const SW_SHOWNA As Integer = 8
        Public Const SW_RESTORE As Integer = 9
        Public Const SW_SHOWDEFAULT As Integer = 10
        Public Const SWP_NOSIZE As Integer = &H1
        Public Const SWP_NOMOVE As Integer = &H2
        Public Const SWP_NOACTIVATE As Integer = &H10
        Public Const HWND_TOPMOST As Integer = -1
        Public Const SWP_SHOWWINDOW As Integer = &H40
        Public Const HWND_NOTOPMOST As Integer = -2
        Public Const GWL_EXSTYLE As Integer = -20
        Public Const WS_EX_LAYERED As Integer = &H80000
        Public Const LWA_COLORKEY As Integer = &H1
        Public Const LWA_ALPHA As Integer = &H2

#End Region

        ' OK
#Region "Declarations used for services"

        Public Const SERVICE_NO_CHANGE As Integer = &HFFFFFFFF

        Public Const ERROR_MORE_DATA As Integer = 234
        Public Const SC_ENUM_PROCESS_INFO As Integer = &H0
        Public Const SC_MANAGER_ENUMERATE_SERVICE As Integer = &H4

        Public Const SC_STATUS_PROCESS_INFO As Integer = 0
        Public Const SERVICE_ACTIVE As Integer = &H1
        Public Const SERVICE_INACTIVE As Integer = &H2
        Public Const SERVICE_STATE_ALL As Integer = (SERVICE_ACTIVE Or SERVICE_INACTIVE)
        Public Const SERVICE_ADAPTER As Integer = &H4
        Public Const SERVICE_WIN32_OWN_PROCESS As Integer = &H10
        Public Const SERVICE_WIN32_SHARE_PROCESS As Integer = &H20
        Public Const SERVICE_WIN32 As Integer = SERVICE_WIN32_OWN_PROCESS + SERVICE_WIN32_SHARE_PROCESS

        Public Const SERVICE_DRIVER As Integer = &HB
        Public Const SERVICE_INTERACTIVE_PROCESS As Integer = &H100
        Public Const SERVICE_ALL As Integer = SERVICE_DRIVER Or SERVICE_WIN32_OWN_PROCESS Or _
                SERVICE_WIN32_SHARE_PROCESS Or SERVICE_WIN32 Or SERVICE_INTERACTIVE_PROCESS

        Public Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
        Public Const SC_MANAGER_CONNECT As Integer = &H1
        Public Const SC_MANAGER_CREATE_SERVICE As Integer = &H2
        Public Const SC_MANAGER_LOCK As Integer = &H8
        Public Const SC_MANAGER_QUERY_LOCK_STATUS As Integer = &H10
        Public Const SC_MANAGER_MODIFY_BOOT_CONFIG As Integer = &H20
        Public Const SC_MANAGER_ALL_ACCESS As Integer = (STANDARD_RIGHTS_REQUIRED + SC_MANAGER_CONNECT + SC_MANAGER_CREATE_SERVICE + SC_MANAGER_ENUMERATE_SERVICE + SC_MANAGER_LOCK + SC_MANAGER_QUERY_LOCK_STATUS + SC_MANAGER_MODIFY_BOOT_CONFIG)

#End Region

        ' OK
#Region "Declarations used for registry"

        Public Const KEY_NOTIFY As Integer = &H10

#End Region

        ' OK
#Region "General declarations"

        ' Some constants
        Public Const BCM_FIRST As Integer = &H1600
        Public Const BCM_SETSHIELD As Integer = (BCM_FIRST + &HC)

        ' Infinite time for WaitSingleObject
        Public Const WAIT_INFINITE As Integer = &HFFFF

#End Region

        ' OK
#Region "Declarations used for graphical functions"

        ' Some constants
        Public Const SC_CLOSE As Integer = &HF060
        Public Const MF_GRAYED As Integer = &H1
        Public Const LVS_EX_BORDERSELECT As Integer = &H8000
        Public Const LVS_EX_DOUBLEBUFFER As Integer = &H10000

#End Region

        ' OK
#Region "Declarations used for keyboard management"

        Public Const HC_ACTION As Integer = 0

#End Region

        ' OK
#Region "Declarations used for handles"

        Public Shared ReadOnly InvalidHandleValue As New IntPtr(-1)
        Public Const HandleOjectTypeJob As Byte = &H5

#End Region

    End Class

End Namespace
