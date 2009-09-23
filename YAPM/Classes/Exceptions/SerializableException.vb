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

<Serializable()> _
Public Class SerializableException


    ' ========================================
    ' Private constants
    ' ========================================


    ' ========================================
    ' Private attributes
    ' ========================================

    ' All error fields
    Private _customMessage As String
    Private _message As String
    'Private _helpL As String
    'Private _source As String
    'Private _stack As String


    ' ========================================
    ' Public properties
    ' ========================================

    ' Custom message
    Public ReadOnly Property CustomMessage() As String
        Get
            Return _customMessage
        End Get
    End Property

    ' Normal message
    Public ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property


    ' ========================================
    ' Other public
    ' ========================================


    ' ========================================
    ' Public functions
    ' ========================================

    ' Constructors
    Public Sub New(ByVal exception As cError)
        _customMessage = exception.CustomMessage
        _message = exception.Message
        '_helpL = exception.HelpLink
        '_source = exception.Source
        '_stack = exception.StackTrace
    End Sub
    Public Sub New(ByVal exception As Exception)
        _customMessage = Nothing
        _message = exception.Message
        '_helpL = exception.HelpLink
        '_source = exception.Source
        '_stack = exception.StackTrace
    End Sub

    ' Return a standard exception
    Public Function GetException() As Exception
        Return New Exception(_message & vbNewLine & _customMessage)
    End Function



    ' ========================================
    ' Private functions
    ' ========================================


End Class
