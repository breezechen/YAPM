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

Public MustInherit Class customLV
    Inherits DoubleBufferedLV

    Public Event HasChangedColumns()

    ' ========================================
    ' Protected
    ' ========================================

    ' General semaphore which protect Dicos, Items...etc.
    Friend generalLvSemaphore As New System.Threading.Semaphore(1, 1)

    Protected _firstItemUpdate As Boolean = True
    Protected _columnsName() As String

    Protected _IMG As ImageList
    Protected m_SortingColumn As ColumnHeader

    Protected _foreColor As Color = Color.FromArgb(30, 30, 30)

    Public Shared NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Public Shared DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)

    Protected Const EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE As Integer = 5
    Protected Const EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE As Integer = 5

    Protected _catchErrors As Boolean = False
    Protected _reorgCol As Boolean = True

    Private _Isconnected As Boolean

    Private _showObjDetailsOnDblClick As Boolean = True


    ' ========================================
    ' Public
    ' ========================================

    Public Enum ProvidersConnectionType
        [Local]
        [RemoteWMI]
        [Remote]
    End Enum

    ' Show object details on double click
    Public Property ShowObjectDetailsOnDoubleClick() As Boolean
        Get
            Return _showObjDetailsOnDblClick
        End Get
        Set(ByVal value As Boolean)
            _showObjDetailsOnDblClick = value
        End Set
    End Property

    ' Catch or not errors
    Public Property CatchErrors() As Boolean
        Get
            Return _catchErrors
        End Get
        Set(ByVal value As Boolean)
            _catchErrors = value
        End Set
    End Property

    ' Reorganize columns ?
    Public Property ReorganizeColumns() As Boolean
        Get
            Return _reorgCol
        End Get
        Set(ByVal value As Boolean)
            _reorgCol = value
        End Set
    End Property

    ' Is control connected ?
    Public Property IsConnected() As Boolean
        Get
            Return _Isconnected
        End Get
        Set(ByVal value As Boolean)
            _Isconnected = value
        End Set
    End Property

    ' Call this to update items in listview
    Public Overridable Sub UpdateItems()
        ' It's overriden, nothing here
    End Sub

    ' Update the items and display an error
    Public Overridable Sub UpdateTheItems()
        If _catchErrors Then
            Try
                Call UpdateItems()
            Catch ex As Exception
                Dim sMessage As String = Nothing
                If InStr(ex.Message, "0x800706BA", CompareMethod.Binary) > 0 Then
                    sMessage = "RPC server is not available. Make sure that WMI is installed, that 'remote procedure call (RPC)' service is started and that no firewall restrict access to RPC service."
                ElseIf InStr(ex.Message, "0x80070005", CompareMethod.Binary) > 0 Then
                    sMessage = "Access is denied. Make sure that you have the rights to access to the remote computer, and that the passwork and login you entered are correct."
                ElseIf InStr(ex.Message, "0x80010108", CompareMethod.Binary) > 0 Then
                    sMessage = "Diconnected. Try to establish connection again."
                End If

                If sMessage IsNot Nothing Then
                    Misc.ShowError(ex, "Could not retieve informations : " & sMessage)
                Else
                    Misc.ShowError(ex, "Could not retieve informations")
                End If

            End Try
        Else
            Call UpdateItems()
        End If
    End Sub

    ' Choose column
    Public Sub ChooseColumns()

        Dim frm As New frmChooseColumns
        frm.ConcernedListView = Me
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()

        ' Recreate subitem buffer and get columns name again
        Call CreateSubItemsBuffer()

        ' Refresh items
        _firstItemUpdate = True
        Me.BeginUpdate()
        Call Me.UpdateItems()
        Me.EndUpdate()

        RaiseEvent HasChangedColumns()
    End Sub

    Protected Overrides Sub OnColumnWidthChanged(ByVal e As System.Windows.Forms.ColumnWidthChangedEventArgs)
        MyBase.OnColumnWidthChanged(e)
        If _reorgCol = False Then
            RaiseEvent HasChangedColumns()
        End If
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.Control AndAlso e.KeyCode = Keys.S Then
            Dim frm As New frmSaveReport
            With frm
                .TopMost = _frmMain.TopMost
                .ListviewToSave = Me
                .ShowDialog()
            End With
        End If
    End Sub

    ' Force refreshing of all items and subitems
    ' Have to NOT USE generalLvSemaphore in this method because it is always
    ' called in a safe context
    Public MustOverride Sub ForceRefreshingOfAllItems()

    ' Connection stuffs
    Protected Overridable Function Connect() As Boolean
        Return Not (_Isconnected)
    End Function
    Protected Overridable Function Disconnect() As Boolean
        Return _Isconnected
    End Function


    ' ========================================
    ' Private
    ' ========================================

    ' Add an item (specific to type of list)
    Friend Overridable Function AddItemWithStyle(ByVal key As String) As ListViewItem
        Return Nothing
    End Function

    ' Create some subitems
    Friend Sub CreateSubItemsBuffer()

        ' Get column names
        Dim _size As Integer = Me.Columns.Count - 1
        ReDim _columnsName(_size)
        For x As Integer = 0 To _size
            _columnsName(x) = Me.Columns.Item(x).Text.Replace("< ", "").Replace("> ", "")
        Next

    End Sub

End Class
