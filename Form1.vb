Imports System.Windows.Forms
Public Class Form1
    Dim Buttons() As Button

    '---- Initialize application ----------------------------------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        xtrace_init()
        xtrace_subs("Form1_Load")
        xtrace_header()
        WriteInfo("Log file = " & LogFile)
        xtrace("Initializing")
        ReadDefaults()
        Read_Command_Line_Arg()
        Me.Text = AppName & " V" & AppVer

        Timer1.Interval = 1000
        Timer1.Start()

        xtrace_sube("Form1_Load")
    End Sub

    '---- Initialize after form display ----
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        xtrace_subs("Timer1_Tick (Init part 2)")

        Timer1.Stop()


        'CreateNewButton(0)
        'CreateNewButton(1)

        xtrace_sube("Timer1_Tick")
    End Sub

    '---- Start ----
    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        PS_Get_AppxPackage()
        Dim Nr As Integer = 0

        Dim Hdr As Boolean = True
        For Each Title As String In AppxPackage_list
            If Title.Length < 2 Then Continue For
            If Title.Contains("----") Then
                Hdr = False
                Continue For
            End If
            If Hdr Then Continue For

            xtrace_i("Create button: " & Title)
            CreateNewButton(Nr, Title)
            Nr += 1
        Next
    End Sub

    '---- CreateButton ----
    Private Sub CreateNewButton(ButtonNr As Integer, Title As String)
        xtrace_subs("CreateNewButton" & " " & ButtonNr.ToString)
        Dim BY0 As Integer = 40 ' First Button Y-Pos
        Dim BYD As Integer = 30 ' Botton Y Distance
        Dim BY As Integer = BY0 + (BYD * ButtonNr)

        Dim NewButton As New Button
        With NewButton
            .Name = "Button_" & ButtonNr.ToString
            '.Parent = Me.SplitContainer1.Panel2
            '.Font = New Font("Microsoft Sans Serif", 12)
            .Font = BtnTemplate.Font
            .Height = BtnTemplate.Height
            .Width = BtnTemplate.Width
            .Text = Title
            .Visible = True
            .Location = New Point(BtnTemplate.Left, BY)
            .Anchor = BtnTemplate.Anchor
            .BringToFront()
        End With

        AddHandler NewButton.Click, AddressOf BtnTemplate_Click
        Controls.Add(NewButton)
        Me.SplitContainer1.Panel2.Controls.Add(NewButton)
        'Buttons(ButtonNr) = NewButton

        xtrace_sube("CreateNewButton")
    End Sub

    Private Sub BtnTemplate_Click(sender As Object, e As EventArgs) Handles BtnTemplate.Click
        xtrace_subs("BtnTemplate_Click")

        xtrace_i("Sender = " & sender.ToString, 3)
        Dim BtnTxt = sender.ToString
        BtnTxt = Mid(BtnTxt, InStr(BtnTxt, ",") + 8)
        xtrace_i("Button text = " & BtnTxt)

        xtrace_sube("BtnTemplate_Click")
    End Sub

    '==== Main Menu ===========================================================

    '---- File, Exit
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        xtrace_subs("Menu, File, Exit")
        exit_program()
    End Sub

    '---- Show Settings -------------------------------------------------------
    Private Sub ShowSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowSettingsToolStripMenuItem.Click
        xtrace_subs("Menu, Settings, Show settings")
        If ShowSettingsToolStripMenuItem.Checked Then
            TextBoxInfo.Visible = True
            TextBoxInfo.Dock = DockStyle.Fill
        Else
            TextBoxInfo.Visible = False
        End If
    End Sub

    '---- Show Log
    Private Sub ShowLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowLogToolStripMenuItem.Click
        xtrace_subs("Menu, Settings, Show log file")
        Process.Start(LogFile)
    End Sub

    '---- Show help
    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        xtrace_subs("Menu, Help, Help")
        ShowHelp()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        xtrace_subs("Menu, Help, About")
        ShowHelpAbout()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub

End Class
