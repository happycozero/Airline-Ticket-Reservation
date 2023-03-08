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
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function
    Private Sub FormFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim numFlight As String = MainF.vForNumFlight
        Label2.Text = MainF.vStr

        Using sr As New StreamReader(FileFly)
            Dim line As String
            While (InlineAssignHelper(line, sr.ReadLine())) IsNot Nothing
                If line.Substring(9, 4) = numFlight Then
                    Dim codePlane As String = line.Substring(14, 6)
                    Dim timeCh As String = line.Substring(40, 2)
                    Dim timeMin As String = line.Substring(43, 2)
                    Dim weekDays() As String = {line.Substring(20, 1), line.Substring(21, 1), line.Substring(22, 1),
                                                line.Substring(23, 1), line.Substring(24, 1), line.Substring(25, 1), line.Substring(26, 1)}

                    Label15.Text = line.Substring(9, 4)
                    Label14.Text = line.Substring(3, 6)
                    Label18.Text = line.Substring(36, 4)
                    Label19.Text = timeCh & ":" & timeMin

                    If line.Substring(62, 4) = "--- " Then
                        Label23.Text = line.Substring(46, 4)
                        Label24.Text = timeCh & ":" & timeMin
                    Else
                        Label20.Text = line.Substring(46, 4)
                        Label21.Text = timeCh & ":" & timeMin
                        timeCh = line.Substring(56, 2)
                        timeMin = line.Substring(59, 2)
                        Label22.Text = timeCh & ":" & timeMin
                        Label23.Text = line.Substring(62, 4)
                        timeCh = line.Substring(66, 2)
                        timeMin = line.Substring(69, 2)
                        Label24.Text = timeCh & ":" & timeMin
                    End If

                    Exit While
                End If
            End While
        End Using

        Dim fileLines As String() = File.ReadAllLines(FileJets)

        For Each line As String In fileLines
            If line <> "" AndAlso line.Substring(3, 6) = vCodePlane Then
                Label16.Text = line.Substring(9, 16)
                Exit For
            End If
        Next

        If vWeek1(0) = "1" Then vVivod = vPn + ","
        If vWeek2(0) = "2" Then vVivod += vVt + ","
        If vWeek3(0) = "3" Then vVivod += vSr + ","
        If vWeek4(0) = "4" Then vVivod += vCht + ","
        If vWeek5(0) = "5" Then vVivod += vPt + ","
        If vWeek6(0) = "6" Then vVivod += vSub + ","
        If vWeek7(0) = "7" Then vVivod += vVs
        Label17.Text = vVivod

        With MainF
            Dim shirotaX As Double = (.ShirG + .ShirM / 60) * Math.PI / 180
            Dim shirotaY As Double = (.ShirGL + .ShirML / 60) * Math.PI / 180
            Dim dolgotaX As Double = (.DolgG + .DolgM / 60) * Math.PI / 180
            Dim dolgotaY As Double = (.DolgGL + .DolgML / 60) * Math.PI / 180

            Dim distance As Double = 40000 * Math.Acos(Math.Sin(shirotaX) * Math.Sin(shirotaY) + Math.Cos(shirotaX) * Math.Cos(shirotaY) * Math.Cos(dolgotaX - dolgotaY))

            Label26.Text = distance.ToString()
        End With
    End Sub
End Class