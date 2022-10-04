Imports HomeWork01_VB.net.Form2
Imports HomeWork01_VB.net.HomeWork01_CSharp.Form2

Public Class Form2

    Private students As List(Of Student)
    Private student1 As Student
    Private student2 As Student
    Private student3 As Student

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        student1 = New Student("Rocco", 1.8)
        student2 = New Student("Antonio", 1.6)
        student3 = New Student("Giovanni", 1.7)
        students = New List(Of Student)
        students.Add(student1)
        students.Add(student2)
        students.Add(student3)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim numbersOfStudents = students.Count
        If numbersOfStudents = 0 Then
            RichTextBox1.Text = "There are no students to analyze"
        Else
            Dim weightSum As Double = 0.0
            For Each student As Student In students
                weightSum += student.GetHeight()
            Next
            Dim averageHeight = weightSum / numbersOfStudents
            RichTextBox1.Text = "The students analyzed are: " + vbCrLf + student1.GetName() + vbCrLf + student2.GetName() + vbCrLf + student3.GetName()
            RichTextBox1.AppendText(vbCrLf + "The average height is: " + vbCrLf + averageHeight.ToString() + " meters.")
        End If
    End Sub
    Public Class Student
        Private name As String
        Private height As Double
        Public Function GetName() As String
            Return Me.name
        End Function
        Public Function GetHeight() As Double
            Return Me.height
        End Function
        Public Sub New(ByVal Name As String, ByVal Height As Double)
            Me.name = Name
            Me.height = Height
        End Sub
    End Class

End Class




