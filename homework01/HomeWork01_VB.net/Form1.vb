Public Class Form1
    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtusername.Text = "username" And txtpassword.Text = "password" Then
            Dim form = New Form2
            form.Show()
        Else
            MessageBox.Show("The User name or password you entered is incorrect, try again!")
            txtpassword.Clear()
            txtusername.Clear()
            txtusername.Focus()
        End If
    End Sub

    Private Sub txtpassword_TextChanged(sender As Object, e As EventArgs) Handles txtpassword.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Application.Exit()
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        txtpassword.Clear()
        txtusername.Clear()
        txtusername.Focus()
    End Sub
End Class



