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
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
    Private flag1 As Integer
    Private vCounter As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()

        Using FileRead As StreamReader = New StreamReader(FileSity)
            While Not FileRead.EndOfStream
                Dim vInStr As String = FileRead.ReadLine()
                If vInStr <> "" Then
                    Dim vCity As String = vInStr.Substring(9, 13)
                    ComboBox1.Items.Add(vCity)
                End If
            End While
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Items.Count = 0 Then
            MessageBox.Show("Ошибка! ComboBox пустой.")
        End If

        If ComboBox1.Text <> "" Then
            vCounter = 0
            ListBox1.Items.Clear()
            Call Flight()
        End If
    End Sub
    Private Sub Flight()
        FileRead = File.OpenText(FileFly)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
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
                    If vCityCode3 <> "0" Then
                        ListBox1.Items.Add(vNumFlight.Trim + "  " + vCityCode.Trim + "  " + vCityCode2.Trim + "  " + vCityCode3.Trim)
                    ElseIf vCityCode3 = "0" Then
                        ListBox1.Items.Add(vNumFlight.Trim + "  " + vCityCode.Trim + "  " + vCityCode2.Trim)
                    End If
                End If
                If vInStr.Substring(36, 3) <> vCityCode Then
                    If vInStr.Substring(46, 4) = vCityCode And vInStr.Substring(62, 4) <> "--- " Then
                        vCityCode2 = vInStr.Substring(46, 4)
                        vCityCode3 = vInStr.Substring(62, 4)
                    End If
                End If
            Else
                flag1 = 1
            End If
        Loop Until flag1 = 1
        FileRead.Close()
        LabCounter.Text = vCounter
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        vStr = ListBox1.SelectedItem

        vForNumFlight = vStr.Substring(0, 4)

        Using FileRead As New StreamReader(FileFly)
            Do
                vInStr = FileRead.ReadLine()
                If vInStr <> "" AndAlso vInStr.Substring(9, 4) = vForNumFlight Then
                    vCDop1 = vInStr.Substring(36, 4)
                    vCDop2 = vInStr.Substring(46, 4)
                    vPlaneCodeDop = vInStr.Substring(14, 6)
                    If vInStr.Substring(62, 4) <> "--- " Then
                        vCDop3 = vInStr.Substring(62, 4)
                    Else
                        vCDop3 = "0"
                    End If
                    Exit Do
                End If
            Loop Until vInStr Is Nothing
        End Using

        ' Создаем словарь для хранения информации из файла справочника
        Dim cityDict As New Dictionary(Of String, Tuple(Of String, String))

        ' Открываем файл справочника и читаем его в словарь
        Using sr As New StreamReader(FileSity)
            While Not sr.EndOfStream
                Dim line As String = sr.ReadLine()
                If Not String.IsNullOrEmpty(line) Then
                    Dim code As String = line.Substring(5, 4)
                    Dim location As String = line.Substring(54, 4)
                    Dim wind As String = line.Substring(47, 2)
                    cityDict.Add(code, Tuple.Create(location, wind))
                End If
            End While
        End Using

        ' Ищем информацию о городах в словаре
        If cityDict.ContainsKey(vCDop1) Then
            Dim tuple1 As Tuple(Of String, String) = cityDict(vCDop1)
            vPoloca1 = tuple1.Item1
            vVeter1 = tuple1.Item2
        End If

        If cityDict.ContainsKey(vCDop2) Then
            Dim tuple2 As Tuple(Of String, String) = cityDict(vCDop2)
            vPoloca2 = tuple2.Item1
            vVeter2 = tuple2.Item2
        End If

        If vCDop3 <> "0" AndAlso cityDict.ContainsKey(vCDop3) Then
            Dim tuple3 As Tuple(Of String, String) = cityDict(vCDop3)
            vPoloca3 = tuple3.Item1
            vVeter3 = tuple3.Item2
        End If

        ' Открываем файл с данными о самолетах
        Using FileRead As StreamReader = File.OpenText(FileJets)
            Dim vPlanePoloca As Integer = 0
            Dim vPlaneVeter As Integer = 0

            ' Читаем файл построчно
            Do While Not FileRead.EndOfStream
                Dim vInStr As String = FileRead.ReadLine()

                ' Если строка не пустая
                If vInStr <> "" Then
                    ' Ищем код дополнительного самолета
                    If vInStr.Substring(3, 6) = vPlaneCodeDop Then
                        ' Получаем данные о местоположении и скорости самолета
                        vPlanePoloca = Convert.ToInt32(vInStr.Substring(31, 4))
                        Dim vUg1 As Integer = Convert.ToInt32(vInStr.Substring(36, 2))
                        Dim vUg2 As Integer = Convert.ToInt32(vInStr.Substring(39, 2))
                        vPlaneVeter = (vUg1 + vUg2) / 2
                        Exit Do
                    End If
                Else
                    ' Если строка пустая, то выходим из цикла
                    Exit Do
                End If
            Loop

            ' Закрываем файл
            FileRead.Close()

            ' Проверяем, на какой форме должен быть показан результат
            If vPlanePoloca >= vPoloca1 And vPlanePoloca >= vPoloca2 And vCDop3 = "0" Then
                Warning1.Show()
            ElseIf vPlanePoloca >= vPoloca1 And vPlanePoloca >= vPoloca2 And vCDop3 <> "0" And vPlanePoloca >= vPoloca3 Then
                Warning1.Show()
            ElseIf vVeter1 > vPlaneVeter And vVeter2 > vPlaneVeter And vCDop3 = "0" Then
                Warning2.Show()
            ElseIf vVeter1 > vPlaneVeter And vVeter2 > vPlaneVeter And vCDop3 <> "0" And vVeter3 > vPlaneVeter Then
                Warning2.Show()
            Else
                FormFlight.Show()
            End If
        End Using
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormPlane.Show()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Button2.Enabled = IIf(ListBox1.SelectedItem = "", False, True)
        Button3.Enabled = IIf(ListBox1.SelectedItem = "", False, True)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        vCity = ComboBox1.Text
        Call CityInf()


    End Sub
    Private Sub CityInf()
        vCity = ComboBox1.Text
        Using fileReadSity As New StreamReader(FileSity)
            Do
                vInStr = fileReadSity.ReadLine()
                If Not String.IsNullOrEmpty(vInStr) Then
                    flag1 = 0
                    If vInStr.Substring(9, 13) = vCity Then
                        vCountryCode = vInStr.Substring(22, 2)
                        vCityCode = vInStr.Substring(5, 3)
                        Label24.Text = vCityCode
                        Label3.Text = vCountryCode

                        Label14.Text = vInStr.Substring(26, 2)
                        ShirG = Label14.Text
                        Label15.Text = vInStr.Substring(30, 2)
                        ShirM = Label15.Text
                        DolgG = vInStr.Substring(34, 2)
                        DolgM = vInStr.Substring(38, 2)

                        Label18.Text = vInStr.Substring(44, 2)
                        Label19.Text = vInStr.Substring(47, 2)
                        Label20.Text = vInStr.Substring(50, 2)
                        Label21.Text = vInStr.Substring(54, 4)
                        Label22.Text = vInStr.Substring(59, 3)

                        Using fileReadCountry As New StreamReader(FileCountry)
                            Do
                                vInStr = fileReadCountry.ReadLine()
                                If Not String.IsNullOrEmpty(vInStr) Then
                                    flag1 = 0
                                    If vInStr.Substring(4, 2) = vCountryCode Then
                                        vCountry = vInStr.Substring(7, 14)
                                        Label26.Text = vCountry
                                    End If
                                Else
                                    flag1 = 1
                                End If
                            Loop Until flag1 = 1
                        End Using
                    End If
                End If
            Loop Until flag1 = 1
        End Using
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim vStr As String = ListBox1.SelectedItem
        Dim vLenghtStr As Integer = vStr.Length

        Dim vForNumFlight As String = vStr.Substring(0, 4)
        Dim vCityCodeLast As String = vStr.Substring(vLenghtStr - 3, 3) + " "

        Using fileRead As New StreamReader(FileSity)
            Dim vInStr As String = fileRead.ReadLine()

            While vInStr IsNot Nothing
                If vInStr <> "" Then
                    If vInStr.Substring(5, 4) = vCityCodeLast Then
                        vCityLast = vInStr.Substring(9, 13)
                        TextBox1.Text = vCityLast
                        vCountryCodeLast = vInStr.Substring(22, 2)
                        Label29.Text = vInStr.Substring(5, 3)
                        Label27.Text = vInStr.Substring(22, 2)

                        Label39.Text = vInStr.Substring(26, 2)
                        ShirGL = vInStr.Substring(26, 2)
                        Label38.Text = vInStr.Substring(30, 2)
                        ShirML = vInStr.Substring(30, 2)
                        DolgGL = vInStr.Substring(34, 2)
                        DolgML = vInStr.Substring(38, 2)

                        Label35.Text = vInStr.Substring(44, 2)
                        Label34.Text = vInStr.Substring(47, 2)
                        Label33.Text = vInStr.Substring(50, 2)
                        Label32.Text = vInStr.Substring(54, 4)
                        Label31.Text = vInStr.Substring(59, 3)

                        Using fileReadCountry As New StreamReader(FileCountry)
                            Dim vInStrCountry As String = fileReadCountry.ReadLine()

                            While vInStrCountry IsNot Nothing
                                If vInStrCountry <> "" Then
                                    If vInStrCountry.Substring(4, 2) = vCountryCodeLast Then
                                        vCountryLast = vInStrCountry.Substring(7, 17)
                                        Label40.Text = vInStrCountry.Substring(7, 14)
                                    End If
                                Else
                                    Exit While
                                End If

                                vInStrCountry = fileReadCountry.ReadLine()
                            End While
                        End Using

                        Exit While
                    End If
                End If

                vInStr = fileRead.ReadLine()
            End While
        End Using
    End Sub
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim result As Integer = MessageBox.Show("Вы уверены, что хотите выйти из приложения?", "Выход из приложения", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            End
        End If
        If result = DialogResult.No Then

        End If
    End Sub
    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) AndAlso Not e.KeyChar = ControlChars.Back Then
            e.Handled = True
        End If
    End Sub
End Class
