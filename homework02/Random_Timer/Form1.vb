Public Class Form1

    Public R As New Random
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Timer1.Stop()
    End Sub

    Public CurrentArithmeticMean As Double = 0

    Public i As Integer
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        i = i + 1
        Dim nameOfExam = "Exam" & i
        Dim GradeOfExam = Me.R.Next(18, 31)
        ' Me.RichTextBox1.AppendText(nameOfExam.PadRight(10) & GradeOfExam & Environment.NewLine)
        CurrentArithmeticMean = CurrentArithmeticMean + (GradeOfExam - CurrentArithmeticMean) / i
        Me.RichTextBox1.AppendText(nameOfExam.PadRight(10) & GradeOfExam & "    " & "Current Mean: " & CurrentArithmeticMean & Environment.NewLine)
    End Sub
End Class
