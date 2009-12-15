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

Imports System.Runtime.InteropServices
Imports Native.Api.NativeStructs
Imports Native.Api.NativeEnums
Imports Native.Api.NativeFunctions

Namespace Native.Functions

    Public Class Misc

        ' ========================================
        ' Private constants
        ' ========================================


        ' ========================================
        ' Private attributes
        ' ========================================


        ' ========================================
        ' Public properties
        ' ========================================


        ' ========================================
        ' Public functions
        ' ========================================

        ' Set 'explorer' theme
        Public Shared Sub SetTheme(ByVal handle As IntPtr)
            Native.Api.NativeFunctions.SetWindowTheme(handle, "explorer", Nothing)
        End Sub

        ' Set a listview as 'double buffered'
        Public Shared Sub SetListViewAsDoubleBuffered(ByRef lv As ListView)
            Dim styles As IntPtr = Native.Api.NativeFunctions.SendMessage(lv.Handle, Native.Api.NativeEnums.LVM.GetExtendedListviewStyle, IntPtr.Zero, IntPtr.Zero)
            styles = CType(styles.ToInt32 Or Native.Api.NativeEnums.LvsEx.DoubleBuffer Or Native.Api.NativeEnums.LvsEx.BorderSelect, IntPtr)
            Native.Api.NativeFunctions.SendMessage(lv.Handle, Native.Api.NativeEnums.LVM.SetExtendedListviewStyle, IntPtr.Zero, styles)
        End Sub


        ' ========================================
        ' Private functions
        ' ========================================


    End Class

End Namespace
