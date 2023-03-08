Imports System.IO

Public Class FormPlane
    Private FileRead As StreamReader
    Private FileJets As String = "jets.txt"
    Private FileFly As String = "fly.txt"
    Private vInStr As String
    Private ForPlane As String
    Private flag1 As Integer
    Private vPlane As String
    Public vPlaneform As String
    Private vSravn As String
    Private vUg1, vUg2, vUg3 As Integer
    Private vNumFlight As Integer

    Private Sub FormPlane_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        vNumFlight = Form1.vForNumFlight

        FileRead = File.OpenText(FileFly)
        Do
            vInStr = FileRead.ReadLine()
            If vInStr <> "" Then
                flag1 = 0
                If vInStr.Substring(9, 4) = vNumFlight Then
                    ForPlane = vInStr.Substring(14, 6)
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
                If vInStr.Substring(3, 6) = ForPlane Then
                    vPlaneform = vInStr.Substring(9, 16)
                    Label13.Text = vInStr.Substring(25, 1)
                    Label14.Text = vInStr.Substring(27, 3)
                    Label15.Text = vInStr.Substring(31, 4)
                    Label17.Text = vInStr.Substring(42, 1)
                    Label18.Text = vInStr.Substring(44, 1)
                    Label19.Text = vInStr.Substring(46, 4)
                    vUg1 = vInStr.Substring(36, 2)
                    vUg2 = vInStr.Substring(39, 2)
                    vUg3 = (vUg1 + vUg2) / 2
                    Label16.Text = vUg3
                End If
            Else
                flag1 = 1
            End If
        Loop Until flag1 = 1
        FileRead.Close()

        Label11.Text = ForPlane
        Label1.Text = vPlaneform
        Label12.Text = vPlaneform
        If ForPlane = "a3006 " Then
        ElseIf ForPlane = "a310_ " Then
        ElseIf ForPlane = "a3103 " Then
        ElseIf ForPlane = "a3402 " Then
        ElseIf ForPlane = "b7672 " Then
        ElseIf ForPlane = "b7772 " Then
        ElseIf ForPlane = "canje " Then
        ElseIf ForPlane = "il62m " Then
        ElseIf ForPlane = "il86_ " Then
        ElseIf ForPlane = "il96_ " Then
        ElseIf ForPlane = "rj85_ " Then
        ElseIf ForPlane = "tu134 " Then
        ElseIf ForPlane = "tu154 " Then
        End If
    End Sub

End Class