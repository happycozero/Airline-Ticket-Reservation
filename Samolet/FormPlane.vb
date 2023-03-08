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
        vNumFlight = MainF.vForNumFlight

        Using FileReadFly As New StreamReader(FileFly)
            Do While Not FileReadFly.EndOfStream
                vInStr = FileReadFly.ReadLine()
                If vInStr <> "" AndAlso vInStr.Substring(9, 4) = vNumFlight Then
                    ForPlane = vInStr.Substring(14, 6)
                    Exit Do
                End If
            Loop
        End Using

        Using FileReadJets As New StreamReader(FileJets)
            Do While Not FileReadJets.EndOfStream
                vInStr = FileReadJets.ReadLine()
                If vInStr <> "" AndAlso vInStr.Substring(3, 6) = ForPlane Then
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
                    Exit Do
                End If
            Loop
        End Using

        Label11.Text = ForPlane
        Label1.Text = vPlaneform
        Label12.Text = vPlaneform

        Dim imagesDict As New Dictionary(Of String, String) From {
            {"a3006 ", "AirbusA300-600.jpg"},
            {"a310_ ", "AirbusA310.jpg"},
            {"a3103 ", "AirbusA310-600.jpg"},
            {"a3402 ", "AirbusA340-200.jpg"},
            {"b7672 ", "Boeing777-200.jpg"},
            {"b7772 ", "Boeing777-200.jpg"},
            {"canje ", "Canadairt.jpg"},
            {"il62m ", "il-62.jpg"},
            {"il86_ ", "il-86.jpg"},
            {"il96_ ", "il-96.jpg"},
            {"rj85_ ", "RJ85.jpg"},
            {"tu134 ", "tu-134.jpg"},
            {"tu154 ", "Tu-154.jpg"}
        }

        If imagesDict.ContainsKey(ForPlane) Then
            PictureBox1.BackgroundImage = Image.FromFile(imagesDict(ForPlane))
        End If

    End Sub
End Class