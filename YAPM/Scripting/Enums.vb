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

Namespace Scripting

    Public Class Enums

        ' Operators
        Public Enum [Operator]
            eq          ' =
            neq         ' <>
            gt          ' >
            lt          ' <
            goe         ' >=
            loe         ' <=
            cont        ' contains (for a string)
        End Enum

        ' Objects
        Public Enum [Object]
            Process
            Service
        End Enum

        ' Condition
        Public Enum Condition
            Name
            Pid
        End Enum

        ' Machine types
        Public Enum MachineType
            Local
            Wmi
        End Enum

        ' Process operation
        Public Enum ProcessOperation
            Kill
            KillTree
            Pause
            [Resume]
            SetPriority     ' arg1 (0 (idle), ..., 5 (real time))
            SetAffinity     ' arg1 (1,2...)
        End Enum

        ' Service operation
        Public Enum ServiceOperation
            Start
            [Stop]
            Delete
            Pause
            [Resume]
            SetStartType    ' arg1 (0 (disabled), 1 (on demand), 2 (auto))
        End Enum

    End Class

End Namespace
