Option Strict On

Public MustInherit Class cGeneralObject

    Private _newItem As Boolean = False
    Private _killedItem As Boolean = False
    Private _isDisplayed As Boolean = False

    Public Property IsDisplayed() As Boolean
        Get
            Return _isDisplayed
        End Get
        Set(ByVal value As Boolean)
            _isDisplayed = value
        End Set
    End Property
    Public Property IsKilledItem() As Boolean
        Get
            Return _killedItem
        End Get
        Set(ByVal value As Boolean)
            _killedItem = value
        End Set
    End Property
    Public Property IsNewItem() As Boolean
        Get
            Return _newItem
        End Get
        Set(ByVal value As Boolean)
            _newItem = value
        End Set
    End Property

    ' Get information by name
    Public MustOverride Function GetInformation(ByVal info As String) As String

    ' General constructor


End Class
