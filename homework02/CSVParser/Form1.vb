Imports Microsoft.VisualBasic.FileIO

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBox1.EnableAutoDragDrop = True
    End Sub

    Private Sub RichTextBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragDrop
        e.Effect = DragDropEffects.None

        Me.RichTextBox1.Clear()
        Dim files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

        For Each path In files
            Me.processaFile(path)
        Next

    End Sub

    Private Sub RichTextBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Sub processaFile(path As String)
        Me.RichTextBox1.AppendText(path & Environment.NewLine)
    End Sub

    Private Sub ButtonGetFile_Click(sender As Object, e As EventArgs) Handles ButtonGetFile.Click
        Dim o As New OpenFileDialog
        o.ShowDialog()

        If String.IsNullOrWhiteSpace(o.FileName) Then Exit Sub
        Me.RichTextBox1.Text = o.FileName
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Path As String = Me.RichTextBox1.Text.Trim
        Dim ListOfUnits As New List(Of Country)


        ' Using R As New StreamReader(Path)
        Using R As New TextFieldParser(Path)
            Dim NameOfVariables As String = R.ReadLine
            ' R.CommentTokens = New String() {"#"}
            ' R.HasFieldsEnclosedInQuotes = False
            R.Delimiters = New String() {","} ' Solo se uso il parser
            Do While Not R.EndOfData 'R.EndOfStream
                ' Manual paring:
                ' Dim Line As String = R.ReadLine
                ' Dim Values() As String = Line.Split(",".ToCharArray, StringSplitOptions.None)
                ' Parsing done by the parser
                Dim Values() As String = R.ReadFields
                Dim Country As New Country

                Country.Country = Values(0)
                Country.Continent = Values(1)
                Country.DateOfCollection = Values(2)
                If Not String.IsNullOrWhiteSpace(Values(3)) Then
                    Country.GDP = CDbl(Values(3))
                End If
                If Not String.IsNullOrWhiteSpace(Values(4)) Then
                    Country.GNI = CDbl(Values(4))

                End If
                If Not String.IsNullOrWhiteSpace(Values(5)) Then
                    Country.LiveBirthsNumber = CInt(Values(5))
                End If
                Country.LiveBirthsRate = Values(6)
                If Not String.IsNullOrWhiteSpace(Values(7)) Then
                    Country.DeathsNumber = CInt(Values(7))
                End If
                Country.DeathRate = Values(8)
                If Not String.IsNullOrWhiteSpace(Values(9)) Then
                    Country.RateOfIncrease = CDbl(Values(9))
                End If
                If Not String.IsNullOrWhiteSpace(Values(10)) Then
                    Country.InfantDeathsNumber = CInt(Values(10))
                End If
                If Not String.IsNullOrWhiteSpace(Values(11)) Then
                    Country.InfantDeathsRatePer1000Births = CDbl(Values(11))
                End If
                If Not String.IsNullOrWhiteSpace(Values(12)) Then
                    Country.LifeExpectancyAtBirthMale = CDbl(Values(12))
                End If
                If Not String.IsNullOrWhiteSpace(Values(13)) Then
                    Country.LifeExpectancyAtBirthFemale = CDbl(Values(13))
                End If
                If Not String.IsNullOrWhiteSpace(Values(14)) Then
                    Country.TotalFertilityRate = CDbl(Values(14))
                End If

                ListOfUnits.Add(Country)

                ' Me.RichTextBox2.AppendText(Line & Environment.NewLine)
            Loop
        End Using
    End Sub
End Class
