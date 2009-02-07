Option Strict On

Public Class frmProcessAffinity

    Private proc As cProcess()

    Public WriteOnly Property Process() As cProcess()
        Set(ByVal value As cProcess())
            proc = value
        End Set
    End Property
    
    Private Sub frmProcessAffinity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If proc(0) Is Nothing Then Me.Close()

        ' Get number of processor of current machine
        Dim _procCount As Integer = proc(0).ProcessorCount - 1

        ' Set checkboxes enable property
        If _procCount >= 1 Then Me.chk1.Enabled = True
        If _procCount >= 2 Then Me.chk2.Enabled = True
        If _procCount >= 3 Then Me.chk3.Enabled = True
        If _procCount >= 4 Then Me.chk4.Enabled = True
        If _procCount >= 5 Then Me.chk5.Enabled = True
        If _procCount >= 6 Then Me.chk6.Enabled = True
        If _procCount >= 7 Then Me.chk7.Enabled = True
        If _procCount >= 8 Then Me.chk8.Enabled = True
        If _procCount >= 9 Then Me.chk9.Enabled = True
        If _procCount >= 10 Then Me.chk10.Enabled = True
        If _procCount >= 11 Then Me.chk11.Enabled = True
        If _procCount >= 12 Then Me.chk12.Enabled = True
        If _procCount >= 13 Then Me.chk13.Enabled = True
        If _procCount >= 14 Then Me.chk14.Enabled = True
        If _procCount >= 15 Then Me.chk15.Enabled = True
        If _procCount >= 16 Then Me.chk16.Enabled = True
        If _procCount >= 17 Then Me.chk17.Enabled = True
        If _procCount >= 18 Then Me.chk18.Enabled = True
        If _procCount >= 19 Then Me.chk19.Enabled = True
        If _procCount >= 20 Then Me.chk20.Enabled = True
        If _procCount >= 21 Then Me.chk21.Enabled = True
        If _procCount >= 22 Then Me.chk22.Enabled = True
        If _procCount >= 23 Then Me.chk23.Enabled = True
        If _procCount >= 24 Then Me.chk24.Enabled = True
        If _procCount >= 25 Then Me.chk25.Enabled = True
        If _procCount >= 26 Then Me.chk26.Enabled = True
        If _procCount >= 27 Then Me.chk27.Enabled = True
        If _procCount >= 28 Then Me.chk28.Enabled = True
        If _procCount >= 29 Then Me.chk29.Enabled = True
        If _procCount >= 30 Then Me.chk30.Enabled = True
        If _procCount >= 31 Then Me.chk31.Enabled = True

        ' Set checkd property
        If proc.Length > 1 Then
            ' Then all set to true
            For Each obj As Object In Me.Controls
                If TypeOf obj Is CheckBox Then
                    CType(obj, CheckBox).Checked = CType(obj, CheckBox).Enabled
                End If
            Next
        Else
            ' Then only one process
            Dim m As Integer = proc(0).AffinityMask
            Me.chk0.Checked = ((m And 1) = 1)
            Me.chk1.Checked = ((m And 2) = 2)
            Me.chk2.Checked = ((m And 4) = 4)
            Me.chk3.Checked = ((m And 8) = 8)
            Me.chk4.Checked = ((m And 16) = 16)
            Me.chk5.Checked = ((m And 32) = 32)
            Me.chk6.Checked = ((m And 64) = 64)
            Me.chk7.Checked = ((m And 128) = 128)
            Me.chk8.Checked = ((m And 256) = 256)
            Me.chk9.Checked = ((m And 512) = 512)
            Me.chk10.Checked = ((m And 1024) = 1024)
            Me.chk11.Checked = ((m And 2048) = 2048)
            Me.chk12.Checked = ((m And 4096) = 4096)
            Me.chk13.Checked = ((m And 14) = 8192)
            Me.chk14.Checked = ((m And 15) = 16384)
            Me.chk15.Checked = ((m And 16) = 32768)
            Me.chk16.Checked = ((m And 17) = 65536)
            Me.chk17.Checked = ((m And 18) = 131072)
            Me.chk18.Checked = ((m And 19) = 262144)
            Me.chk19.Checked = ((m And 20) = 524288)
            Me.chk20.Checked = ((m And 21) = 1048576)
            Me.chk21.Checked = ((m And 22) = 2097152)
            Me.chk22.Checked = ((m And 23) = 4194304)
            Me.chk23.Checked = ((m And 24) = 8388608)
            Me.chk24.Checked = ((m And 25) = 16777216)
            Me.chk25.Checked = ((m And 26) = 33554432)
            Me.chk26.Checked = ((m And 27) = 67108864)
            Me.chk27.Checked = ((m And 28) = 134217728)
            Me.chk28.Checked = ((m And 29) = 238435456)
            Me.chk29.Checked = ((m And 30) = 536870912)
            Me.chk30.Checked = ((m And 31) = 1073741824)
            Me.chk31.Checked = ((m And 32) = 2147483648)
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        ' Calculate new mask
        Dim m As Integer = 0
        For Each obj As Object In Me.Controls
            If TypeOf obj Is CheckBox Then
                Dim chk As CheckBox = CType(obj, CheckBox)
                Dim number As Integer = CInt(Val(chk.Name.Substring(3, chk.Name.Length - 3)))
                If chk.Enabled And chk.Checked Then
                    m = CInt(m + 2 ^ number)
                End If
            End If
        Next

        If m = 0 Then
            MsgBox("Process must have at least one affinity with a processor.", MsgBoxStyle.Information, "Can't set affinity")
            Exit Sub
        End If


        ' Apply new mask
        For Each c As cProcess In proc
            c.AffinityMask = m
        Next

        Me.Close()
    End Sub
End Class