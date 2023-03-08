Imports System.IO

Public Class Form1
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
        MsgBox(IIf(ComboBox1.Text = "", "Выберите город отправления!", ""), vbOK + vbCritical, "Ошибка")

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

        FileRead = File.OpenText(FileFly)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
                If vInStr.Substring(9, 4) = vForNumFlight Then
                    vCDop1 = vInStr.Substring(36, 4)
                    vCDop2 = vInStr.Substring(46, 4)
                    vPlaneCodeDop = vInStr.Substring(14, 6)
                    If vInStr.Substring(62, 4) <> "--- " Then
                        vCDop3 = vInStr.Substring(62, 4)
                    Else
                        vCDop3 = "0"
                    End If
                End If
            Else
                flag1 = 1
            End If
        Loop Until flag1 = 1
        FileRead.Close()

        FileRead = File.OpenText(FileSity)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
                If vInStr.Substring(5, 4) = vCDop1 Then
                    vPoloca1 = vInStr.Substring(54, 4)
                    vVeter1 = vInStr.Substring(47, 2)

                    FileRead = File.OpenText(FileSity)
                    Do
                        vInStr = FileRead.ReadLine()
                        If vInStr <> "" Then
                            flag1 = 0
                            If vInStr.Substring(5, 4) = vCDop2 Then
                                vPoloca2 = vInStr.Substring(54, 4)
                                vVeter2 = vInStr.Substring(47, 2)
                            End If
                        Else
                            flag1 = 1
                        End If
                    Loop Until flag1 = 1

                    FileRead = File.OpenText(FileSity)
                    Do
                        vInStr = FileRead.ReadLine()
                        If vInStr <> "" Then
                            flag1 = 0
                            If vCDop3 <> "0" And vInStr.Substring(5, 4) = vCDop3 Then
                                vPoloca3 = vInStr.Substring(54, 4)
                                vVeter3 = vInStr.Substring(47, 2)
                            End If
                        Else
                            flag1 = 1
                        End If
                    Loop Until flag1 = 1
                End If

            Else
                flag1 = 1
            End If
        Loop Until flag1 = 1
        FileRead.Close()

        FileRead = File.OpenText(FileJets)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
                If vInStr.Substring(3, 6) = vPlaneCodeDop Then
                    vPlanePoloca = vInStr.Substring(31, 4)
                    vUg1 = vInStr.Substring(36, 2)
                    vUg2 = vInStr.Substring(39, 2)
                    vPlaneVeter = (vUg1 + vUg2) / 2
                End If
            Else
                flag1 = 1
            End If
        Loop Until flag1 = 1
        FileRead.Close()

        If vPlanePoloca >= vPoloca1 And vPlanePoloca >= vPoloca2 And vCDop3 = "0" Then
            Form2.Show()
        ElseIf vPlanePoloca >= vPoloca1 And vPlanePoloca >= vPoloca2 And vCDop3 <> "0" And vPlanePoloca >= vPoloca3 Then
            Form2.Show()
        ElseIf vVeter1 > vPlaneVeter And vVeter2 > vPlaneVeter And vCDop3 = "0" Then
            Form3.Show()
        ElseIf vVeter1 > vPlaneVeter And vVeter2 > vPlaneVeter And vCDop3 <> "0" And vVeter3 > vPlaneVeter Then
            Form3.Show()
        Else
            FormFlight.Show()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormPlane.Show()
    End Sub

    Private Sub ButEnd_Click(sender As Object, e As EventArgs) Handles ButEnd.Click
        End
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ListBox1.SelectedItem = "" Then
            Button2.Enabled = False
            Button3.Enabled = False
        Else
            Button2.Enabled = True
            Button3.Enabled = True
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        vCity = ComboBox1.Text
        Call CityInf()
    End Sub

    Private Sub CityInf()
        vCity = ComboBox1.Text
        FileRead = File.OpenText(FileSity)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
                If vInStr.Substring(9, 13) = vCity Then
                    vCountryCode = vInStr.Substring(22, 2)
                    vCityCode = vInStr.Substring(5, 3)
                    Label24.Text = vInStr.Substring(5, 3)
                    Label3.Text = vInStr.Substring(22, 2)

                    Label14.Text = vInStr.Substring(26, 2)
                    ShirG = vInStr.Substring(26, 2)
                    Label15.Text = vInStr.Substring(30, 2)
                    ShirM = vInStr.Substring(30, 2)
                    DolgG = vInStr.Substring(34, 2)
                    DolgM = vInStr.Substring(38, 2)

                    Label18.Text = vInStr.Substring(44, 2)
                    Label19.Text = vInStr.Substring(47, 2)
                    Label20.Text = vInStr.Substring(50, 2)
                    Label21.Text = vInStr.Substring(54, 4)
                    Label22.Text = vInStr.Substring(59, 3)

                    FileRead = File.OpenText(FileCountry)
                    Do
                        vInStr = FileRead.ReadLine()
                        If vInStr <> "" Then
                            flag1 = 0
                            If vInStr.Substring(4, 2) = vCountryCode Then
                                vCountry = vInStr.Substring(7, 17)
                                Label26.Text = vInStr.Substring(7, 14)
                            Else


                            End If
                        Else
                            flag1 = 1
                        End If
                    Loop Until flag1 = 1
                    FileRead.Close()
                End If
            End If
        Loop Until flag1 = 1
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        vStr = ListBox1.SelectedItem
        vLenghtStr = vStr.Length

        vForNumFlight = vStr.Substring(0, 4)
        If vLenghtStr = 17 Then
            vCityCodeLast = vStr.Substring(14, 3)
        ElseIf vLenghtStr = 14 Then
            vCityCodeLast = vStr.Substring(11, 3)
        ElseIf vLenghtStr = 13 Then
            vCityCodeLast = vStr.Substring(10, 3)
        ElseIf vLenghtStr = 18 Then
            vCityCodeLast = vStr.Substring(15, 3)
        End If

        vCityCodeLast = vCityCodeLast + " "


        FileRead = File.OpenText(FileSity)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
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

                    FileRead = File.OpenText(FileCountry)
                    Do
                        vInStr = FileRead.ReadLine()
                        If vInStr <> "" Then
                            flag1 = 0
                            If vInStr.Substring(4, 2) = vCountryCodeLast Then
                                vCountryLast = vInStr.Substring(7, 17)
                                Label40.Text = vInStr.Substring(7, 14)
                            Else


                            End If
                        Else
                            flag1 = 1
                        End If
                    Loop Until flag1 = 1
                    FileRead.Close()
                End If
            End If
        Loop Until flag1 = 1
    End Sub
End Class
