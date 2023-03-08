Option Explicit On
Imports System.Text.ASCIIEncoding
Imports System.IO
Imports System.Math

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

        vNumFlight = Form1.vForNumFlight
        Label2.Text = Form1.vStr
        FileRead = File.OpenText(FileFly)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
                If vInStr.Substring(9, 4) = vNumFlight Then
                    Label15.Text = vInStr.Substring(9, 4)
                    Label14.Text = vInStr.Substring(3, 6)
                    vCodePlane = vInStr.Substring(14, 6)
                    Label18.Text = vInStr.Substring(36, 4)
                    vTimeCh = vInStr.Substring(40, 2)
                    vTimeMin = vInStr.Substring(43, 2)
                    Label19.Text = vTimeCh + ":" + vTimeMin
                    vWeek1 = vInStr.Substring(20, 1)
                    vWeek2 = vInStr.Substring(21, 1)
                    vWeek3 = vInStr.Substring(22, 1)
                    vWeek4 = vInStr.Substring(23, 1)
                    vWeek5 = vInStr.Substring(24, 1)
                    vWeek6 = vInStr.Substring(25, 1)
                    vWeek7 = vInStr.Substring(26, 1)

                    If vInStr.Substring(62, 4) = "--- " Then
                        Label23.Text = vInStr.Substring(46, 4)
                        vTimeCh = vInStr.Substring(50, 2)
                        vTimeMin = vInStr.Substring(53, 2)
                        Label24.Text = vTimeCh + ":" + vTimeMin

                    Else
                        Label20.Text = vInStr.Substring(46, 4)
                        vTimeCh = vInStr.Substring(50, 2)
                        vTimeMin = vInStr.Substring(53, 2)
                        Label21.Text = vTimeCh + ":" + vTimeMin
                        vTimeCh = vInStr.Substring(56, 2)
                        vTimeMin = vInStr.Substring(59, 2)
                        Label22.Text = vTimeCh + ":" + vTimeMin
                        Label23.Text = vInStr.Substring(62, 4)
                        vTimeCh = vInStr.Substring(66, 2)
                        vTimeMin = vInStr.Substring(69, 2)
                        Label24.Text = vTimeCh + ":" + vTimeMin
                    End If
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
                If vInStr.Substring(3, 6) = vCodePlane Then
                    Label16.Text = vInStr.Substring(9, 16)
                End If

            Else
                flag1 = 1
            End If
        Loop Until flag1 = 1
        FileRead.Close()



        If vWeek1.Substring(0, 1) = "1" Then
            vVivod = vPn + ","
        Else
            vVivod = ""
        End If
        If vWeek2.Substring(0, 1) = "2" Then
            vVivod += vVt + ","
        Else
            vVivod += ""
        End If
        If vWeek3.Substring(0, 1) = "3" Then
            vVivod += vSr + ","
        Else
            vVivod += ""
        End If
        If vWeek4.Substring(0, 1) = "4" Then
            vVivod += vCht + ","
        Else
            vVivod += ""
        End If
        If vWeek5.Substring(0, 1) = "5" Then
            vVivod += vPt + ","
        Else
            vVivod += ""
        End If
        If vWeek6.Substring(0, 1) = "6" Then
            vVivod += vSub + ","
        Else
            vVivod += ""
        End If
        If vWeek7.Substring(0, 1) = "7" Then
            vVivod += vVs
        Else
            vVivod += ""
        End If
        Label17.Text = vVivod

        vShirotaG = Form1.ShirG
        vShirotaM = Form1.ShirM
        vShirotaGL = Form1.ShirGL
        vShirotaML = Form1.ShirML
        vDolgotaG = Form1.DolgG
        vDolgotaM = Form1.DolgM
        vDolgotaGL = Form1.DolgGL
        vDolgotaML = Form1.DolgML

        vShirotaX = vShirotaG * 3.14 / 180 + vShirotaM * 3.14 / 180 * 60
        vShirotaY = vShirotaGL * 3.14 / 180 + vShirotaML * 3.14 / 180 * 60

        vDolgotaX = vDolgotaG * 3.14 / 180 + vDolgotaM * 3.14 / 180 * 60
        vDolgotaY = vDolgotaGL * 3.14 / 180 + vDolgotaML * 3.14 / 180 * 60

        vRast = (40000 * Acos(Sin(vShirotaX) * Sin(vShirotaY) + Cos(vShirotaX) * Cos(vShirotaY) * Cos(vDolgotaX - vDolgotaY)))
        Label26.Text = vRast
    End Sub
End Class