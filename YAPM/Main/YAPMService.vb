' =======================================================
' Yet Another (remote) Process Monitor (YAPM)
' Copyright (c) 2008-2009 Alain Descotes (violent_ken)
' https://sourceforge.net/projects/yaprocmon/
' =======================================================
'
'
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


Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.ServiceProcess
Imports System.Text
Imports System.Threading
Imports System.Runtime.InteropServices

Namespace YAPMLauncherService

    Partial Class InteractiveProcess
        Inherits ServiceBase

        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overloads Overrides Sub OnStart(ByVal args As String())
            ThreadPool.QueueUserWorkItem(AddressOf LaunchService)
        End Sub

        Public Sub LaunchService(ByVal context As Object)

            ' Parse port text file from resources
            Call cNetwork.ParsePortTextFile()

            ' Enable some privileges
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.DebugPrivilege)
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.ShutdownPrivilege)

            ' Instanciate 'forms'
            _frmServer = New frmServer
            _frmServer.Show()

        End Sub

    End Class

End Namespace