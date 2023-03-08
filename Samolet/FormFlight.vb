Option Explicit On
Imports System.Text.ASCIIEncoding
Imports System.IO
Imports System.Math
Imports System.Text

Public Class FormFlight
    Private vNumFlight As Integer
    Private FileFly As String = "fly.txt"
    Private FileJets As String = "jets.txt"
    Private FileRead As StreamReader
    Private vInStr As String
    Private FileSity As String = "sity.txt"
    Private flag1 As Integer
    Private vCodePlane As String
    Private vPlane As String
    Private vTimeCh, vTimeMin As String
    Private vPn As String = "Пн"
    Private vVt As String = "Вт"
    Private vSr As String = "Ср"
    Private vCht As String = "Чт"
    Private vPt As String = "Пт"
    Private vSub As String = "Сб"
    Private vVs As String = "Вс"
    Private vPolet As String
    Private vWeek1, vWeek2, vWeek3, vWeek4, vWeek5, vWeek6, vWeek7 As String
    Private vVivod As String
    Private vRast As Integer
    Private vShirotaX, vShirotaY, vShirotaG, vShirotaM, vShirotaGL, vShirotaML As Integer
    Private vDolgotaX, vDolgotaY, vDolgotaG, vDolgotaM, vDolgotaGL, vDolgotaML As Integer
    Private Sub FormFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim flightNumber As String = Form1.vForNumFlight
        Dim flightInfo As String = Form1.vStr
        Dim fileReader As StreamReader = Nothing
        Try
            fileReader = File.OpenText(FileFly)
            Do
                Dim line As String = fileReader.ReadLine()
                If Not String.IsNullOrEmpty(line) Then
                    Dim flightCode As String = line.Substring(14, 6)
                    If line.Substring(9, 4) = flightNumber Then
                        Label15.Text = flightNumber
                        Label14.Text = line.Substring(3, 6)
                        Label18.Text = line.Substring(36, 4)
                        Dim timeCh As String = line.Substring(40, 2)
                        Dim timeMin As String = line.Substring(43, 2)
                        Label19.Text = String.Format("{0}:{1}", timeCh, timeMin)
                        Dim weekDays As String = line.Substring(20, 7)
                        If line.Substring(62, 4) = "--- " Then
                            Label23.Text = line.Substring(46, 4)
                            timeCh = line.Substring(50, 2)
                            timeMin = line.Substring(53, 2)
                            Label24.Text = String.Format("{0}:{1}", timeCh, timeMin)
                        Else
                            Label20.Text = line.Substring(46, 4)
                            timeCh = line.Substring(50, 2)
                            timeMin = line.Substring(53, 2)
                            Label21.Text = String.Format("{0}:{1}", timeCh, timeMin)
                            timeCh = line.Substring(56, 2)
                            timeMin = line.Substring(59, 2)
                            Label22.Text = String.Format("{0}:{1}", timeCh, timeMin)
                            Label23.Text = line.Substring(62, 4)
                            timeCh = line.Substring(66, 2)
                            timeMin = line.Substring(69, 2)
                            Label24.Text = String.Format("{0}:{1}", timeCh, timeMin)
                        End If
                    End If
                Else
                    Exit Do
                End If
            Loop
        Catch ex As Exception
        Finally
            If fileReader IsNot Nothing Then
                fileReader.Close()
            End If
        End Try

        Using FileRead As StreamReader = File.OpenText(FileJets)
            Dim flag1 As Integer = 0
            While flag1 = 0
                Dim vInStr As String = FileRead.ReadLine()
                If vInStr <> "" AndAlso vInStr.Substring(3, 6) = vCodePlane Then
                    Label16.Text = vInStr.Substring(9, 16)
                    flag1 = 1
                End If
            End While
        End Using

        Dim sb As New StringBuilder()
        If vWeek1(0) = "1"c Then
            sb.Append(vPn).Append(",")
        End If
        If vWeek2(0) = "2"c Then
            sb.Append(vVt).Append(",")
        End If
        If vWeek3(0) = "3"c Then
            sb.Append(vSr).Append(",")
        End If
        If vWeek4(0) = "4"c Then
            sb.Append(vCht).Append(",")
        End If
        If vWeek5(0) = "5"c Then
            sb.Append(vPt).Append(",")
        End If
        If vWeek6(0) = "6"c Then
            sb.Append(vSub).Append(",")
        End If
        If vWeek7(0) = "7"c Then
            sb.Append(vVs)
        End If
        Label17.Text = sb.ToString()

        Dim vShirotaG As Double, vShirotaM As Double, vShirotaGL As Double, vShirotaML As Double
        Dim vDolgotaG As Double, vDolgotaM As Double, vDolgotaGL As Double, vDolgotaML As Double
        Double.TryParse(Form1.ShirG, vShirotaG)
        Double.TryParse(Form1.ShirM, vShirotaM)
        Double.TryParse(Form1.ShirGL, vShirotaGL)
        Double.TryParse(Form1.ShirML, vShirotaML)
        Double.TryParse(Form1.DolgG, vDolgotaG)
        Double.TryParse(Form1.DolgM, vDolgotaM)
        Double.TryParse(Form1.DolgGL, vDolgotaGL)
        Double.TryParse(Form1.DolgML, vDolgotaML)

        Dim degToRad As Double = Math.PI / 180
        Dim radToDeg As Double = 180 / Math.PI

        Dim shirotaMins As Double = vShirotaM * 60 + vShirotaML
        Dim dolgotaMins As Double = vDolgotaM * 60 + vDolgotaML

        Dim shirotaX As Double = (vShirotaG + shirotaMins / 60) * degToRad
        Dim shirotaY As Double = (vShirotaGL + vShirotaML / 60) * degToRad
        Dim dolgotaX As Double = (vDolgotaG + dolgotaMins / 60) * degToRad
        Dim dolgotaY As Double = (vDolgotaGL + vDolgotaML / 60) * degToRad

        Dim cosDiff As Double = Math.Cos(dolgotaX - dolgotaY)
        Dim sinShirotaX As Double = Math.Sin(shirotaX)
        Dim sinShirotaY As Double = Math.Sin(shirotaY)
        Dim cosShirotaX As Double = Math.Cos(shirotaX)
        Dim cosShirotaY As Double = Math.Cos(shirotaY)

        Dim rast As Double = 40000 * Math.Acos(sinShirotaX * sinShirotaY + cosShirotaX * cosShirotaY * cosDiff)

        Label26.Text = rast

    End Sub
End Class