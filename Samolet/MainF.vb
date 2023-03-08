Imports System.IO
Public Class MainF
    Private FileRead As StreamReader
    Private vInStr As String
    Private FileSity As String = "sity.txt"
    Private FileCountry As String = "country.txt"
    Private FileFly As String = "fly.txt"
    Private FileJets As String = "jets.txt"

    Private vCity As String
    Private vCityLast As String
    Private vCity2 As String
    Private vCity3 As String
    Private vCountry As String
    Private vCountryLast As String
    Private vCityCode As String
    Private vCityCode2 As String
    Private vCityCode3 As String
    Public vCityCodeLast As String
    Private vCityCodeLast2 As String
    Private vCountryCode As String
    Private vCountryCodeLast As String
    Private vNumberFlight As Integer
    Public vPlane As String
    Public vStr As String
    Private vNumFlight As String
    Private vvJ As Integer
    Public vForNumFlight As String
    Private vLenghtStr As Integer
    Private vCDop1, vCDop2, vCDop3 As String
    Private vPoloca1, vPoloca2, vPoloca3 As String
    Private vPlanePoloca As String
    Private vPlaneCodeDop As String
    Private vVeter1, vVeter2, vVeter3 As String
    Private vPlaneVeter As String
    Private vUg1, vUg2 As Integer
    Public ShirG, ShirM, DolgG, DolgM, ShirGL, ShirML, DolgGL, DolgML As Integer

    Private flag1 As Integer
    Private vCounter As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

        Using FileRead As New StreamReader(FileSity)
            Do While Not FileRead.EndOfStream
                vInStr = FileRead.ReadLine()
                If vInStr <> "" Then
                    vCity = vInStr.Substring(9, 13)
                    ComboBox1.Items.Add(vCity)
                End If
            Loop
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Then
            MsgBox("Выберите город отправления!")
        Else
            vCounter = 0
            ListBox1.Items.Clear()
            Call Flight()
        End If
    End Sub
    Private Sub Flight()
        Using FileRead As New System.IO.StreamReader(FileFly)
            Dim vInStr As String
            Dim flag1 As Integer = 0
            Dim vCityCode2 As String = ""
            Dim vPlane As String = ""
            Dim vNumFlight As String = ""
            Dim vCityCode3 As String = ""
            Dim vCounter As Integer = 0

            Do While FileRead.Peek() >= 0
                vInStr = FileRead.ReadLine()
                If Not String.IsNullOrEmpty(vInStr) Then
                    If vInStr.Substring(36, 3) = vCityCode Then
                        vCityCode2 = vInStr.Substring(46, 4)
                        vPlane = vInStr.Substring(14, 6)
                        vNumFlight = vInStr.Substring(9, 5)
                        vCounter += 1
                        If vInStr.Substring(62, 4) <> "--- " Then
                            vCityCode3 = vInStr.Substring(62, 4)
                        Else
                            vCityCode3 = "0"
                        End If
                        Dim itemText As String = vNumFlight.Trim() & "  " & vCityCode.Trim() & "  " & vCityCode2.Trim()
                        If vCityCode3 <> "0" Then
                            itemText &= "  " & vCityCode3.Trim()
                        End If
                        ListBox1.Items.Add(itemText)
                    ElseIf vInStr.Substring(36, 3) <> vCityCode AndAlso vInStr.Substring(46, 4) = vCityCode AndAlso vInStr.Substring(62, 4) <> "--- " Then
                        vCityCode2 = vInStr.Substring(46, 4)
                        vCityCode3 = vInStr.Substring(62, 4)
                    End If
                Else
                    flag1 = 1
                End If
            Loop

            FileRead.Close()
            LabCounter.Text = vCounter.ToString()
        End Using
    End Sub
    Private Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
        target = value
        Return value
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim vStr As String = ListBox1.SelectedItem.ToString()
        Dim vForNumFlight As String = vStr.Substring(0, 4)

        Using FileRead As New StreamReader(FileFly)
            Dim line As String
            While (InlineAssignHelper(line, FileRead.ReadLine())) IsNot Nothing
                If Not String.IsNullOrEmpty(line) Then
                    If line.Substring(9, 4) = vForNumFlight Then
                        Dim vCDop1 As String = line.Substring(36, 4)
                        Dim vCDop2 As String = line.Substring(46, 4)
                        Dim vPlaneCodeDop As String = line.Substring(14, 6)
                        Dim vCDop3 As String = If(line.Substring(62, 4) <> "--- ", line.Substring(62, 4), "0")
                        Exit While
                    End If
                End If
            End While
        End Using

        Dim vPoloca1 As String = String.Empty
        Dim vPoloca2 As String = String.Empty
        Dim vPoloca3 As String = String.Empty
        Dim vVeter1 As String = String.Empty
        Dim vVeter2 As String = String.Empty
        Dim vVeter3 As String = String.Empty

        Using fileRead As New StreamReader(FileSity)
            Dim line As String
            While (InlineAssignHelper(line, fileRead.ReadLine())) IsNot Nothing
                If Not String.IsNullOrEmpty(line) Then
                    If line.Substring(5, 4) = vCDop1 Then
                        vPoloca1 = line.Substring(54, 4)
                        vVeter1 = line.Substring(47, 2)
                        Exit While
                    End If
                Else
                    flag1 = 1
                End If
            End While

            fileRead.DiscardBufferedData()
            fileRead.BaseStream.Seek(0, IO.SeekOrigin.Begin)

            While (InlineAssignHelper(line, fileRead.ReadLine())) IsNot Nothing
                If Not String.IsNullOrEmpty(line) Then
                    If line.Substring(5, 4) = vCDop2 Then
                        vPoloca2 = line.Substring(54, 4)
                        vVeter2 = line.Substring(47, 2)
                        Exit While
                    End If
                Else
                    flag1 = 1
                End If
            End While

            fileRead.DiscardBufferedData()
            fileRead.BaseStream.Seek(0, IO.SeekOrigin.Begin)

            While (InlineAssignHelper(line, fileRead.ReadLine())) IsNot Nothing
                If Not String.IsNullOrEmpty(line) AndAlso vCDop3 <> "0" AndAlso line.Substring(5, 4) = vCDop3 Then
                    vPoloca3 = line.Substring(54, 4)
                    vVeter3 = line.Substring(47, 2)
                    Exit While
                End If
            End While
        End Using

        Using FileRead As New StreamReader(FileJets)
            While Not FileRead.EndOfStream
                Dim vInStr As String = FileRead.ReadLine()
                If Not String.IsNullOrEmpty(vInStr) AndAlso vInStr.Substring(3, 6) = vPlaneCodeDop Then
                    Dim vPlanePoloca As Integer = CInt(vInStr.Substring(31, 4))
                    Dim vUg1 As Integer = CInt(vInStr.Substring(36, 2))
                    Dim vUg2 As Integer = CInt(vInStr.Substring(39, 2))
                    Dim vPlaneVeter As Double = (vUg1 + vUg2) / 2
                    If vPlanePoloca >= vPoloca1 AndAlso vPlanePoloca >= vPoloca2 Then
                        If vCDop3 = "0" OrElse vPlanePoloca >= vPoloca3 Then
                            Warning1.Show()
                            Exit Sub
                        End If
                    ElseIf vVeter1 > vPlaneVeter AndAlso vVeter2 > vPlaneVeter Then
                        If vCDop3 = "0" OrElse vVeter3 > vPlaneVeter Then
                            Warning2.Show()
                            Exit Sub
                        End If
                    End If
                End If
            End While
        End Using
        FormFlight.Show()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormPlane.Show()
    End Sub
    Private Sub ButEnd_Click(sender As Object, e As EventArgs) Handles ButEnd.Click
        End
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Button2.Enabled = Not String.IsNullOrEmpty(ListBox1.SelectedItem)
        Button3.Enabled = Not String.IsNullOrEmpty(ListBox1.SelectedItem)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        vCity = ComboBox1.Text
        Call CityInf()
    End Sub
    Private Sub CityInf()
        vCity = ComboBox1.Text

        Using cityFile As New StreamReader(FileSity)
            Do While Not cityFile.EndOfStream
                Dim line As String = cityFile.ReadLine()
                If Not String.IsNullOrEmpty(line) AndAlso line.Substring(9, 13) = vCity Then
                    vCountryCode = line.Substring(22, 2)
                    vCityCode = line.Substring(5, 3)
                    Label24.Text = vCityCode
                    Label3.Text = vCountryCode

                    Label14.Text = line.Substring(26, 2)
                    ShirG = Label14.Text
                    Label15.Text = line.Substring(30, 2)
                    ShirM = Label15.Text
                    Label16.Text = line.Substring(34, 2)
                    DolgG = Label16.Text
                    Label17.Text = line.Substring(38, 2)
                    DolgM = Label17.Text

                    Label18.Text = line.Substring(44, 2)
                    Label19.Text = line.Substring(47, 2)
                    Label20.Text = line.Substring(50, 2)
                    Label21.Text = line.Substring(54, 4)
                    Label22.Text = line.Substring(59, 3)

                    Using countryFile As New StreamReader(FileCountry)
                        Do While Not countryFile.EndOfStream
                            Dim countryLine As String = countryFile.ReadLine()
                            If Not String.IsNullOrEmpty(countryLine) AndAlso countryLine.Substring(4, 2) = vCountryCode Then
                                vCountry = countryLine.Substring(7, 17)
                                Label26.Text = vCountry
                                Exit Do
                            End If
                        Loop
                    End Using
                    Exit Do
                End If
            Loop
        End Using
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim vStr As String = ListBox1.SelectedItem
        Dim vLenghtStr As Integer = vStr.Length
        Dim vCityCodeLast As String = ""

        Select Case vLenghtStr
            Case 13
                vCityCodeLast = vStr.Substring(10, 3)
            Case 14
                vCityCodeLast = vStr.Substring(11, 3)
            Case 17
                vCityCodeLast = vStr.Substring(14, 3)
            Case 18
                vCityCodeLast = vStr.Substring(15, 3)
        End Select

        vCityCodeLast &= " "

        Using FileRead As New StreamReader(FileSity)
            While Not FileRead.EndOfStream
                vInStr = FileRead.ReadLine()
                If vInStr <> "" AndAlso vInStr.Substring(5, 4) = vCityCodeLast Then
                    vCityLast = vInStr.Substring(9, 13)
                    TextBox1.Text = vCityLast
                    vCountryCodeLast = vInStr.Substring(22, 2)
                    Label29.Text = vInStr.Substring(5, 3)
                    Label27.Text = vCountryCodeLast

                    Label39.Text = vInStr.Substring(26, 2)
                    ShirGL = Label39.Text
                    Label38.Text = vInStr.Substring(30, 2)
                    ShirML = Label38.Text
                    Label37.Text = vInStr.Substring(34, 2)
                    DolgGL = Label37.Text
                    Label36.Text = vInStr.Substring(38, 2)
                    DolgML = Label36.Text

                    Label35.Text = vInStr.Substring(44, 2)
                    Label34.Text = vInStr.Substring(47, 2)
                    Label33.Text = vInStr.Substring(50, 2)
                    Label32.Text = vInStr.Substring(54, 4)
                    Label31.Text = vInStr.Substring(59, 3)

                    Using FileReadCountry As New StreamReader(FileCountry)
                        While Not FileReadCountry.EndOfStream
                            vInStr = FileReadCountry.ReadLine()
                            If vInStr <> "" AndAlso vInStr.Substring(4, 2) = vCountryCodeLast Then
                                vCountryLast = vInStr.Substring(7, 17)
                                Label40.Text = vInStr.Substring(7, 14)
                                Exit While
                            End If
                        End While
                    End Using

                    Exit While
                End If
            End While
        End Using
    End Sub
End Class
